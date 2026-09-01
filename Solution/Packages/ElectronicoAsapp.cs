using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using RestSharp;
using Newtonsoft.Json;
using BusinessObjects;
using BusinessLogicLayer;
using Services;

namespace Packages
{
    // Integracion con Asapp Electronic API v2 (reemplazo de SICE). Ver reference_sice_integracion / reference_asapp_api_v2
    // en la memoria del proyecto para el detalle de la spec y las decisiones de alcance.
    //
    // Estado de cobertura por tipo de comprobante (actualizado 2026-07-10):
    //   - factura, nota_credito, retencion, nota_debito: probados end-to-end contra el sandbox de Asapp (envio,
    //     consulta de estado, RIDE) - los 4 quedaron AUTORIZADO. Nota de Debito reutiliza la misma fuente de datos
    //     que Nota de Credito (Dnotacre) via una rama nueva en Electronico.LoadElectronico, y SOLO puede ir por
    //     Asapp (el router bloquea explicitamente que se intente por SICE, que no tiene GenerarND()).
    //   - guia_remision, liquidacion_compra: NO implementados todavia (BuildPayload retorna null -> se registra como NOSOPORTADO-ASAPP).
    //     Guia de remision depende de definir de donde sale el "transportista" como empresa (hoy Ccomrem solo tiene chofer/persona).
    public class ElectronicoAsapp
    {
        private const string BaseUrlPruebas = "https://electronic-api-dev.asapp.com.ec";
        private const string BaseUrlProduccion = "https://electronic-api.asapp.com.ec";
        private const string Endpoint = "/api/v2/api/comprobantes";

        public class AsappResponse
        {
            public long comprobanteId { get; set; }
            public string claveAcceso { get; set; }
            public string numeroComprobante { get; set; }
            public string tipoDocumento { get; set; }
            public string estado { get; set; }
            public string mensaje { get; set; }
        }

        public class AsappError
        {
            public string error { get; set; }
            public string mensaje { get; set; }
        }

        // Forma real de GET /api/v1/comprobantes/{id}/estado, confirmada contra el sandbox el 2026-07-10.
        // Distinta a AsappResponse (la del POST) - no tiene "estado" ni "mensaje", el estado esta en
        // estadoWorkflow y numeroAutorizacion es la misma clave de acceso que devuelve el POST.
        public class AsappEstadoResponse
        {
            public long comprobanteId { get; set; }
            public string estadoWorkflow { get; set; }
            public string estadoGeneral { get; set; }
            public string numeroAutorizacion { get; set; }
            public string fechaAutorizacion { get; set; }
            public string orchestrationId { get; set; }
            public string motivoRechazo { get; set; }
        }

        // Forma real de GET /api/v1/comprobantes/{id}/ride y /xml, confirmada contra el sandbox el 2026-07-10:
        // devuelven una URL firmada (SAS) de Azure Blob Storage que expira (~1 hora vista en la prueba) - por eso
        // hay que pedirla fresca en cada click, nunca guardarla. Encaja con el JS existente (ElectronicRideResult
        // en functions.js hace window.open(data.d) siempre con lo que devuelve el server en el momento).
        public class AsappUrlResponse
        {
            public string rideUrl { get; set; }
            public string xmlUrl { get; set; }
        }

        // Log dedicado de la integracion Asapp (tabla log_asapp) - un registro por cada llamada HTTP, exitosa o no,
        // para poder ver en produccion si se estan enviando los comprobantes, errores de conexion, y las respuestas
        // reales. Separado de ExceptionHandling.Log/excepcion (esa sigue siendo el mecanismo general del resto de
        // la app) para no mezclar telemetria de negocio con excepciones .NET no relacionadas. Retencion: 12 meses,
        // ver LogAsappBLL.Purgar.
        private static void Log(Comprobante com, string operacion, string endpoint, RestResponse response, bool exitoso, string request, string mensaje, DateTime inicio)
        {
            try
            {
                LogAsapp log = new LogAsapp();
                log.log_empresa = com.com_empresa;
                log.log_comprobante = com.com_codigo;
                log.log_operacion = operacion;
                log.log_endpoint = endpoint;
                log.log_httpstatus = response != null ? (int?)response.StatusCode : null;
                log.log_exitoso = exitoso ? 1 : 0;
                log.log_request = Truncar(request, 4000);
                log.log_response = Truncar(response != null ? response.Content : null, 4000);
                log.log_mensaje = Truncar(mensaje, 500);
                log.log_duracionms = (int)(DateTime.Now - inicio).TotalMilliseconds;
                log.crea_fecha = DateTime.Now;
                LogAsappBLL.Insert(log);
            }
            catch (Exception ex)
            {
                ExceptionHandling.Log.AddExepcion(ex);
            }
        }

        private static string Truncar(string texto, int max)
        {
            if (string.IsNullOrEmpty(texto)) return texto;
            return texto.Length > max ? texto.Substring(0, max) : texto;
        }

        // Consulta por clave de acceso (no por comprobanteId): funciona tanto para comprobantes enviados por
        // Asapp como para historicos migrados desde SICE (2022+), que nunca van a tener com_asappid en SIAC
        // pero siempre tienen com_claveelec. Endpoint confirmado por Asapp 2026-07-10.
        public static string GetRideUrl(Comprobante com)
        {
            DateTime inicio = DateTime.Now;
            string endpoint = "";
            try
            {
                if (string.IsNullOrEmpty(com.com_claveelec))
                    return "";

                Empresa empresa = new Empresa { emp_codigo = com.com_empresa, emp_codigo_key = com.com_empresa };
                empresa = EmpresaBLL.GetByPK(empresa);

                bool produccion = com.com_ambiente == "2";
                string apiKey = produccion ? empresa.emp_asappapikeyprod : empresa.emp_asappapikeypruebas;
                if (string.IsNullOrEmpty(apiKey))
                    return "";

                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
                var client = new RestClient(produccion ? BaseUrlProduccion : BaseUrlPruebas);
                endpoint = "/api/v1/comprobantes/clave/" + com.com_claveelec + "/ride";
                var request = new RestRequest(endpoint, Method.Get);
                request.AddHeader("X-Api-Key", apiKey);
                RestResponse response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                {
                    AsappUrlResponse result = JsonConvert.DeserializeObject<AsappUrlResponse>(response.Content);
                    Log(com, "RIDE", endpoint, response, true, null, "OK", inicio);
                    return result.rideUrl;
                }

                Log(com, "RIDE", endpoint, response, false, null, "HTTP " + (int)response.StatusCode, inicio);
                ExceptionHandling.Log.AddExepcion(new Exception("Asapp - error consultando RIDE comprobante " + com.com_codigo + " (clave=" + com.com_claveelec + "): HTTP " + (int)response.StatusCode + " - " + response.Content));
            }
            catch (Exception ex)
            {
                ExceptionHandling.Log.AddExepcion(ex);
                Log(com, "RIDE", endpoint, null, false, null, "Excepcion: " + ex.Message, inicio);
            }
            return "";
        }

        public static bool GenerateElectronico(Comprobante com)
        {
            if (com.com_estado != Constantes.cEstadoMayorizado)
            {
                MarcarNoEnviado(com, "PENDIENTE-DESCUADRE", "Comprobante no esta mayorizado (revisar posible descuadre)");
                return false;
            }

            // Guia de Remision no usa Electronico.LoadElectronico (esa funcion es compartida con SICE y no tiene
            // rama para este tipo - agregarle una implicaria el mismo riesgo de "XML vacio a SICE" que se mitigo
            // para ND/LC). Ccomrem/Dcomrem tampoco encajan bien en la forma del objeto Electronic generico
            // (piensa en terminos de items/impuestos, no de traslado/transporte). Camino propio y autocontenido.
            if (com.com_tipodoc == Constantes.cGuiaRemision.tpd_codigo)
                return GenerateElectronicoGuiaRemision(com);

            DateTime inicio = DateTime.Now;
            try
            {
                // Electronico.LoadElectronico no recibe la empresa por parametro: internamente lee el campo de
                // instancia empresa (asi lo hace tambien GenerateElectronicoSICE). Hay que setearlo antes de
                // llamarla. Se usa una instancia propia de Electronico (ya no es static) para que este request
                // no comparta estado con ningun otro comprobante procesandose al mismo tiempo.
                Electronico electronicoHelper = new Electronico();
                electronicoHelper.empresa = new Empresa { emp_codigo = com.com_empresa, emp_codigo_key = com.com_empresa };
                electronicoHelper.empresa = EmpresaBLL.GetByPK(electronicoHelper.empresa);
                // Campo scratch [Data(noprop=true)], no viene de la BD - GenerateElectronicoSICE tambien lo carga asi.
                // Usado para el campoAdicional "Agente de Retencion" (ver BuildInfoAdicional).
                electronicoHelper.empresa.emp_agenteretxml = Constantes.GetParameter("agenteretxml");
                Empresa empresa = electronicoHelper.empresa;

                Comprobante comprobante = new Comprobante { com_empresa = com.com_empresa, com_empresa_key = com.com_empresa, com_codigo = com.com_codigo, com_codigo_key = com.com_codigo };
                comprobante = ComprobanteBLL.GetByPK(comprobante);
                comprobante.com_empresa_key = com.com_empresa;
                comprobante.com_codigo_key = com.com_codigo;

                Ccomdoc ccomdoc = new Ccomdoc { cdoc_empresa = comprobante.com_empresa, cdoc_empresa_key = comprobante.com_empresa, cdoc_comprobante = comprobante.com_codigo, cdoc_comprobante_key = comprobante.com_codigo };
                ccomdoc = CcomdocBLL.GetByPK(ccomdoc);
                if (string.IsNullOrEmpty(ccomdoc.cdoc_direccion))
                    ccomdoc.cdoc_direccion = "S/D";
                ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa={0} and ddoc_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "ddoc_secuencia");
                comprobante.ccomdoc = ccomdoc;

                Total total = new Total { tot_empresa = comprobante.com_empresa, tot_empresa_key = comprobante.com_empresa, tot_comprobante = comprobante.com_codigo, tot_comprobante_key = comprobante.com_codigo };
                comprobante.total = TotalBLL.GetByPK(total);

                Ccomenv ccomenv = new Ccomenv { cenv_empresa = comprobante.com_empresa, cenv_empresa_key = comprobante.com_empresa, cenv_comprobante = comprobante.com_codigo, cenv_comprobante_key = comprobante.com_codigo };
                comprobante.ccomenv = CcomenvBLL.GetByPK(ccomenv);

                comprobante.retenciones = DretencionBLL.GetAll(new WhereParams("drt_empresa={0} and drt_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "drt_secuencia");
                comprobante.notascre = DnotacreBLL.GetAll(new WhereParams("dnc_empresa={0} and dnc_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "dnc_secuencia");

                List<Drecibo> detalleformas = DreciboBLL.GetAll(new WhereParams("dfp_empresa={0} and dfp_ref_comprobante={1} and com_estado=2", comprobante.com_empresa, comprobante.com_codigo), "dfp_secuencia");

                Electronic electronic = electronicoHelper.LoadElectronico(comprobante, detalleformas);
                if (electronic == null)
                {
                    MarcarNoEnviado(com, "NOCONFIG", "LoadElectronico no genero datos (revisar parametro de sistema 'electronicos')");
                    return false;
                }

                Comprobante docSustento = null;
                if (comprobante.ccomdoc.cdoc_factura.HasValue)
                    docSustento = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = comprobante.com_empresa, com_empresa_key = comprobante.com_empresa, com_codigo = comprobante.ccomdoc.cdoc_factura.Value, com_codigo_key = comprobante.ccomdoc.cdoc_factura.Value });

                object payload = BuildPayload(comprobante, electronic, docSustento, empresa);
                if (payload == null)
                {
                    MarcarNoEnviado(com, "NOSOPORTADO-ASAPP", "Tipo de documento (com_tipodoc=" + comprobante.com_tipodoc + ") aun no implementado para Asapp");
                    return false;
                }

                bool produccion = electronic.ele_ambiente == 2;
                string apiKey = produccion ? empresa.emp_asappapikeyprod : empresa.emp_asappapikeypruebas;
                if (string.IsNullOrEmpty(apiKey))
                {
                    MarcarNoEnviado(com, "NOCONFIG-ASAPP", "Empresa " + empresa.emp_codigo + " sin X-Api-Key de Asapp para ambiente " + (produccion ? "produccion" : "pruebas"));
                    return false;
                }

                // Primera llamada HTTPS saliente de este codebase (SICE/SICEAzure usan http:// plano). .NET Framework 4.8 no
                // siempre negocia TLS 1.2 con el default de ServicePointManager - forzarlo evita fallos de conexion silenciosos (HTTP 0).
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

                var client = new RestClient(produccion ? BaseUrlProduccion : BaseUrlPruebas);
                var request = new RestRequest(Endpoint, Method.Post);
                request.AddHeader("X-Api-Key", apiKey);
                request.AddJsonBody(payload);
                string requestJson = JsonConvert.SerializeObject(payload);
                RestResponse response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                {
                    AsappResponse result = JsonConvert.DeserializeObject<AsappResponse>(response.Content);
                    comprobante.com_claveelec = result.claveAcceso;
                    // Asapp devuelve el estado en formato mixto ("Autorizado"), SICE siempre lo guarda en mayusculas
                    // (ver UpdateElectronicoDataSICE). Se normaliza aca para que las comparaciones existentes contra
                    // "AUTORIZADO" (ej. Metodos.asmx.cs al abrir un comprobante) funcionen igual sin importar el proveedor.
                    comprobante.com_estadoelec = result.estado != null ? result.estado.ToUpper() : result.estado;
                    comprobante.com_asappid = result.comprobanteId.ToString();
                    comprobante.com_ambiente = electronic.ele_ambiente.ToString();
                    comprobante.com_emision = electronic.ele_emision.ToString();
                    comprobante.com_provider = "ASAPP";
                    comprobante.com_mensajeelec = result.mensaje;
                    ComprobanteBLL.Update(comprobante);
                    Log(com, "ENVIO", Endpoint, response, true, requestJson, "Enviado OK, estado=" + comprobante.com_estadoelec, inicio);
                    return true;
                }

                AsappError error = SafeDeserializeError(response.Content);

                // Ya existe en Asapp con ese secuencial: no es un fallo real, se recupera y no se reintenta.
                if (error != null && error.error == "SECUENCIAL_DUPLICADO")
                {
                    comprobante.com_estadoelec = "DUPLICADO-ASAPP";
                    comprobante.com_mensajeelec = error.mensaje;
                    comprobante.com_provider = "ASAPP";
                    ComprobanteBLL.Update(comprobante);
                    Log(com, "ENVIO", Endpoint, response, false, requestJson, "SECUENCIAL_DUPLICADO: " + error.mensaje, inicio);
                    ExceptionHandling.Log.AddExepcion(new Exception("Asapp SECUENCIAL_DUPLICADO comprobante " + com.com_codigo + ": " + error.mensaje));
                    return false;
                }

                string mensajeError = error != null ? (error.error + ": " + error.mensaje) : ("HTTP " + (int)response.StatusCode + " - " + response.Content);
                comprobante.com_estadoelec = "ERROR-ASAPP";
                comprobante.com_mensajeelec = mensajeError.Length > 500 ? mensajeError.Substring(0, 500) : mensajeError;
                comprobante.com_provider = "ASAPP";
                ComprobanteBLL.Update(comprobante);
                Log(com, "ENVIO", Endpoint, response, false, requestJson, mensajeError, inicio);
                ExceptionHandling.Log.AddExepcion(new Exception("Asapp error al enviar comprobante " + com.com_codigo + ": " + mensajeError));
                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandling.Log.AddExepcion(ex);
                Log(com, "ENVIO", Endpoint, null, false, null, "Excepcion: " + ex.Message, inicio);
                return false;
            }
        }

        // Guia de Remision (SRI 06) - camino independiente, no pasa por Electronico.LoadElectronico. Carga directo
        // de Ccomrem/Dcomrem. Transportista: SIAC no modela una "empresa transportista" distinta del chofer
        // (Ccomrem solo tiene crem_chofer/crem_ciruc_cho/crem_nombres_cho) - se usa el chofer, que ademas es el
        // UNICO identificador de transportista que pide el SRI/Asapp aca (rucTransportista/nombreTransportista,
        // sin campo separado para chofer), asi que no es una aproximacion, es el dato correcto.
        // Ambiente/api key salen de la config actual (Electronico.GetElectronicoConfig), no de un objeto Electronic
        // (que no se construye para este tipo).
        private static bool GenerateElectronicoGuiaRemision(Comprobante com)
        {
            DateTime inicio = DateTime.Now;
            try
            {
                Electronicos config = Electronico.GetElectronicoConfig(com);
                if (config == null)
                {
                    MarcarNoEnviado(com, "NOCONFIG", "Sin configuracion en 'electronicos' para este tipodoc");
                    return false;
                }

                Empresa empresa = new Empresa { emp_codigo = com.com_empresa, emp_codigo_key = com.com_empresa };
                empresa = EmpresaBLL.GetByPK(empresa);

                Comprobante comprobante = new Comprobante { com_empresa = com.com_empresa, com_empresa_key = com.com_empresa, com_codigo = com.com_codigo, com_codigo_key = com.com_codigo };
                comprobante = ComprobanteBLL.GetByPK(comprobante);
                comprobante.com_empresa_key = com.com_empresa;
                comprobante.com_codigo_key = com.com_codigo;

                Ccomrem ccomrem = new Ccomrem { crem_empresa = comprobante.com_empresa, crem_empresa_key = comprobante.com_empresa, crem_comprobante = comprobante.com_codigo, crem_comprobante_key = comprobante.com_codigo };
                ccomrem = CcomremBLL.GetByPK(ccomrem);

                List<Dcomrem> detalle = DcomremBLL.GetAll(new WhereParams("drem_empresa={0} and drem_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "drem_secuencia");

                string emailDestinatario = "";
                if (ccomrem.crem_destinatario.HasValue)
                {
                    Persona destinatario = new Persona { per_empresa = comprobante.com_empresa, per_empresa_key = comprobante.com_empresa, per_codigo = ccomrem.crem_destinatario.Value, per_codigo_key = ccomrem.crem_destinatario.Value };
                    destinatario = PersonaBLL.GetByPK(destinatario);
                    emailDestinatario = destinatario.per_mail;
                }

                Comprobante docSustento = null;
                if (ccomrem.crem_factura.HasValue)
                    docSustento = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = comprobante.com_empresa, com_empresa_key = comprobante.com_empresa, com_codigo = ccomrem.crem_factura.Value, com_codigo_key = ccomrem.crem_factura.Value });

                // Defensivo: no hay validador obligatorio en wfGuiaRemision.aspx para estas fechas (confirmado por
                // investigacion 2026-07-10), tratarlas como opcionales y no crashear si vienen vacias.
                DateTime fechaInicioTraslado = ccomrem.crem_trasladoini ?? comprobante.com_fecha;
                DateTime fechaFinTraslado = ccomrem.crem_trasladofin ?? comprobante.com_fecha;

                List<object> items = new List<object>();
                if (detalle != null)
                {
                    foreach (Dcomrem item in detalle)
                    {
                        items.Add(new
                        {
                            // Dcomrem no tiene un codigo de producto en string (solo drem_producto, un id numerico) -
                            // se usa ese id como codigo, o "SN" (el mismo default que usa Asapp) si no hay producto.
                            codigo = item.drem_producto.HasValue ? item.drem_producto.Value.ToString() : "SN",
                            descripcion = item.drem_descripcion,
                            cantidad = item.drem_cantidad ?? 0,
                            precioUnitario = item.drem_precio ?? 0,
                            // La guia de remision no tributa (el IVA ya se declaro en la factura sustento) - Dcomrem
                            // no tiene desglose de IVA por linea, se manda "0" siempre.
                            iva = "0"
                        });
                    }
                }

                object payload = new
                {
                    tipo = "guia_remision",
                    ambiente = config.ambiente == 2 ? "produccion" : "pruebas",
                    establecimiento = comprobante.com_almacenid,
                    puntoEmision = comprobante.com_pventaid,
                    fecha = FechaIso(comprobante.com_fecha),
                    secuencial = comprobante.com_numero,
                    destinatario = new
                    {
                        identificacion = ccomrem.crem_ciruc_des,
                        nombre = ccomrem.crem_nombres_des,
                        direccion = ccomrem.crem_direccion_des,
                        email = emailDestinatario
                    },
                    transporte = new
                    {
                        dirPartida = ccomrem.crem_direccion_rem,
                        motivoTraslado = ccomrem.crem_motivo,
                        fechaInicio = FechaIso(fechaInicioTraslado),
                        fechaFin = FechaIso(fechaFinTraslado),
                        rucTransportista = ccomrem.crem_ciruc_cho,
                        nombreTransportista = ccomrem.crem_nombres_cho,
                        placa = ccomrem.crem_placa,
                        tipoDocSustento = "factura",
                        numeroDocSustento = docSustento != null ? string.Format("{0:000}-{1:000}-{2:000000000}", docSustento.com_almacenid, docSustento.com_pventaid, docSustento.com_numero) : "",
                        numeroAutorizacionSustento = docSustento != null ? docSustento.com_claveelec : "",
                        fechaDocSustento = docSustento != null ? FechaIso(docSustento.com_fecha) : ""
                    },
                    items = items
                };

                bool produccion = config.ambiente == 2;
                string apiKey = produccion ? empresa.emp_asappapikeyprod : empresa.emp_asappapikeypruebas;
                if (string.IsNullOrEmpty(apiKey))
                {
                    MarcarNoEnviado(com, "NOCONFIG-ASAPP", "Empresa " + empresa.emp_codigo + " sin X-Api-Key de Asapp para ambiente " + (produccion ? "produccion" : "pruebas"));
                    return false;
                }

                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
                var client = new RestClient(produccion ? BaseUrlProduccion : BaseUrlPruebas);
                var request = new RestRequest(Endpoint, Method.Post);
                request.AddHeader("X-Api-Key", apiKey);
                request.AddJsonBody(payload);
                string requestJson = JsonConvert.SerializeObject(payload);
                RestResponse response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                {
                    AsappResponse result = JsonConvert.DeserializeObject<AsappResponse>(response.Content);
                    comprobante.com_claveelec = result.claveAcceso;
                    comprobante.com_estadoelec = result.estado != null ? result.estado.ToUpper() : result.estado;
                    comprobante.com_asappid = result.comprobanteId.ToString();
                    comprobante.com_ambiente = config.ambiente.ToString();
                    comprobante.com_emision = "1";
                    comprobante.com_provider = "ASAPP";
                    comprobante.com_mensajeelec = result.mensaje;
                    ComprobanteBLL.Update(comprobante);
                    Log(com, "ENVIO", Endpoint, response, true, requestJson, "Enviado OK, estado=" + comprobante.com_estadoelec, inicio);
                    return true;
                }

                AsappError error = SafeDeserializeError(response.Content);

                if (error != null && error.error == "SECUENCIAL_DUPLICADO")
                {
                    comprobante.com_estadoelec = "DUPLICADO-ASAPP";
                    comprobante.com_mensajeelec = error.mensaje;
                    comprobante.com_provider = "ASAPP";
                    ComprobanteBLL.Update(comprobante);
                    Log(com, "ENVIO", Endpoint, response, false, requestJson, "SECUENCIAL_DUPLICADO: " + error.mensaje, inicio);
                    ExceptionHandling.Log.AddExepcion(new Exception("Asapp SECUENCIAL_DUPLICADO comprobante " + com.com_codigo + ": " + error.mensaje));
                    return false;
                }

                string mensajeError = error != null ? (error.error + ": " + error.mensaje) : ("HTTP " + (int)response.StatusCode + " - " + response.Content);
                comprobante.com_estadoelec = "ERROR-ASAPP";
                comprobante.com_mensajeelec = mensajeError.Length > 500 ? mensajeError.Substring(0, 500) : mensajeError;
                comprobante.com_provider = "ASAPP";
                ComprobanteBLL.Update(comprobante);
                Log(com, "ENVIO", Endpoint, response, false, requestJson, mensajeError, inicio);
                ExceptionHandling.Log.AddExepcion(new Exception("Asapp error al enviar comprobante " + com.com_codigo + ": " + mensajeError));
                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandling.Log.AddExepcion(ex);
                Log(com, "ENVIO", Endpoint, null, false, null, "Excepcion: " + ex.Message, inicio);
                return false;
            }
        }

        // Asapp procesa de forma asincrona: el POST devuelve "Grabado" de inmediato y hay que consultar este endpoint
        // hasta llegar a un estado terminal (Autorizado/Devuelto/NoAutorizado/Timeout). No hay job/cron que la llame
        // todavia - se invoca on-demand desde Electronico.UpdateElectronicoData (mismo punto de entrada que SICE).
        // Forma de la respuesta de /estado inferida por simetria con la respuesta del POST (el spec no trae un ejemplo
        // explicito de este endpoint) - confirmar contra el sandbox si el parseo no calza.
        // Consulta por clave de acceso (no por comprobanteId) - mismo motivo que GetRideUrl: funciona para
        // historicos migrados desde SICE que nunca van a tener com_asappid en SIAC. Endpoint confirmado por
        // Asapp 2026-07-10.
        public static Comprobante UpdateElectronicoData(Comprobante com)
        {
            DateTime inicio = DateTime.Now;
            string endpoint = "";
            try
            {
                if (string.IsNullOrEmpty(com.com_claveelec))
                    return com;

                Empresa empresa = new Empresa { emp_codigo = com.com_empresa, emp_codigo_key = com.com_empresa };
                empresa = EmpresaBLL.GetByPK(empresa);

                bool produccion = com.com_ambiente == "2";
                string apiKey = produccion ? empresa.emp_asappapikeyprod : empresa.emp_asappapikeypruebas;
                if (string.IsNullOrEmpty(apiKey))
                    return com;

                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
                var client = new RestClient(produccion ? BaseUrlProduccion : BaseUrlPruebas);
                endpoint = "/api/v1/comprobantes/clave/" + com.com_claveelec + "/estado";
                var request = new RestRequest(endpoint, Method.Get);
                request.AddHeader("X-Api-Key", apiKey);
                RestResponse response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                {
                    AsappEstadoResponse result = JsonConvert.DeserializeObject<AsappEstadoResponse>(response.Content);

                    Comprobante upd = new Comprobante { com_empresa = com.com_empresa, com_empresa_key = com.com_empresa, com_codigo = com.com_codigo, com_codigo_key = com.com_codigo };
                    upd = ComprobanteBLL.GetByPK(upd);
                    upd.com_empresa_key = com.com_empresa;
                    upd.com_codigo_key = com.com_codigo;
                    // No pisar un estado bueno anterior si esta respuesta no trae "estadoWorkflow" reconocible.
                    if (!string.IsNullOrEmpty(result.estadoWorkflow))
                        upd.com_estadoelec = result.estadoWorkflow.ToUpper();
                    if (!string.IsNullOrEmpty(result.motivoRechazo))
                        upd.com_mensajeelec = result.motivoRechazo;
                    if (!string.IsNullOrEmpty(result.numeroAutorizacion))
                        upd.com_claveelec = result.numeroAutorizacion;
                    ComprobanteBLL.Update(upd);
                    Log(com, "CONSULTA_ESTADO", endpoint, response, true, null, "Estado=" + upd.com_estadoelec, inicio);
                    return upd;
                }

                Log(com, "CONSULTA_ESTADO", endpoint, response, false, null, "HTTP " + (int)response.StatusCode, inicio);
                ExceptionHandling.Log.AddExepcion(new Exception("Asapp - error consultando estado comprobante " + com.com_codigo + " (clave=" + com.com_claveelec + "): HTTP " + (int)response.StatusCode + " - " + response.Content));
            }
            catch (Exception ex)
            {
                ExceptionHandling.Log.AddExepcion(ex);
                Log(com, "CONSULTA_ESTADO", endpoint, null, false, null, "Excepcion: " + ex.Message, inicio);
            }
            return com;
        }

        private static AsappError SafeDeserializeError(string content)
        {
            try { return string.IsNullOrEmpty(content) ? null : JsonConvert.DeserializeObject<AsappError>(content); }
            catch { return null; }
        }

        // Deja rastro explicito de por que NO se intento el envio (a diferencia del comportamiento silencioso de SICE) para que
        // no haga falta descubrirlo manualmente. Ver project_sice_envio_silencioso en memoria.
        private static void MarcarNoEnviado(Comprobante com, string estado, string mensaje)
        {
            try
            {
                Comprobante upd = new Comprobante { com_empresa = com.com_empresa, com_empresa_key = com.com_empresa, com_codigo = com.com_codigo, com_codigo_key = com.com_codigo };
                upd = ComprobanteBLL.GetByPK(upd);
                upd.com_empresa_key = com.com_empresa;
                upd.com_codigo_key = com.com_codigo;
                upd.com_estadoelec = estado;
                upd.com_mensajeelec = mensaje;
                upd.com_provider = "ASAPP";
                ComprobanteBLL.Update(upd);
            }
            catch (Exception ex)
            {
                ExceptionHandling.Log.AddExepcion(ex);
            }
            ExceptionHandling.Log.AddExepcion(new Exception("Asapp - comprobante " + com.com_codigo + " no enviado: " + estado + " - " + mensaje));
        }

        private static object BuildPayload(Comprobante comprobante, Electronic electronic, Comprobante docSustento, Empresa empresa)
        {
            if (comprobante.com_tipodoc == Constantes.cFactura.tpd_codigo)
                return BuildFactura(comprobante, electronic, empresa);
            if (comprobante.com_tipodoc == Constantes.cNotacre.tpd_codigo)
                return BuildNotaCredito(comprobante, electronic, docSustento, empresa);
            if (comprobante.com_tipodoc == Constantes.cNotadeb.tpd_codigo)
                return BuildNotaDebito(comprobante, electronic, docSustento);
            if (comprobante.com_tipodoc == Constantes.cRetencion.tpd_codigo)
                return BuildRetencion(comprobante, electronic, docSustento, empresa);
            if (comprobante.com_tipodoc == Constantes.cLiquidacionCompra.tpd_codigo)
                return BuildLiquidacionCompra(comprobante, electronic);
            // Constantes.cGuia (guia_remision) tiene su propio flujo completo, ver GenerateElectronicoGuiaRemision -
            // no pasa por LoadElectronico/BuildPayload (Ccomrem/Dcomrem no encajan en el objeto Electronic generico).
            return null;
        }

        // LIQCOM usa la misma forma que factura (cliente+items+pagos, sin documentoModificado) - reutiliza
        // Electronico.LoadElectronico via su propia rama "LIQUIDACION DE COMPRA" (misma logica de items/persona/pagos
        // que FACTURA, sin los adicionales de envio que no aplican a una compra a un individuo).
        private static object BuildLiquidacionCompra(Comprobante comprobante, Electronic electronic)
        {
            return new
            {
                tipo = "liquidacion_compra",
                ambiente = Ambiente(electronic),
                establecimiento = comprobante.com_almacenid,
                puntoEmision = comprobante.com_pventaid,
                fecha = FechaIso(comprobante.com_fecha),
                secuencial = comprobante.com_numero,
                cliente = new
                {
                    identificacion = electronic.ele_idcomprador,
                    nombre = electronic.ele_razonsocial,
                    email = electronic.ele_email,
                    direccion = electronic.ele_dircomprador
                },
                items = BuildItems(electronic),
                pagos = BuildPagos(electronic)
            };
        }

        private static string Ambiente(Electronic electronic)
        {
            return electronic.ele_ambiente == 2 ? "produccion" : "pruebas";
        }

        private static string FechaIso(DateTime fecha)
        {
            // Ecuador no maneja horario de verano: offset -05:00 todo el ano.
            return fecha.ToString("yyyy-MM-ddTHH:mm:ss") + "-05:00";
        }

        private static string MetodoPago(string codigoSRI)
        {
            switch (codigoSRI)
            {
                case "01": return "efectivo";
                case "16": return "tarjeta_debito";
                case "17": return "dinero_electronico";
                case "19": return "tarjeta_credito";
                case "20": return "transferencia";
                default: return "efectivo"; // codigo SRI no mapeado - revisar (queda igual visible via com_mensajeelec si Asapp lo rechaza)
            }
        }

        private static string Iva(decimal? porciva)
        {
            decimal tarifa = porciva ?? 0;
            if (tarifa == 15) return "15";
            if (tarifa == 5) return "5";
            return "0"; // no distingue exento/no_objeto (mismo % de impuesto); ver GetCodigoImp en Electronico.cs si se requiere paridad exacta con SRI
        }

        private static List<object> BuildItems(Electronic electronic)
        {
            List<object> items = new List<object>();
            if (electronic.detalle != null)
            {
                foreach (Electronicdet det in electronic.detalle)
                {
                    items.Add(new
                    {
                        codigo = string.IsNullOrEmpty(det.eled_codigo) ? "SN" : det.eled_codigo,
                        descripcion = det.eled_descripcion,
                        cantidad = det.eled_cantidad ?? 0,
                        precioUnitario = det.eled_precio ?? 0,
                        descuento = det.eled_descuento ?? 0,
                        iva = Iva(det.eled_porciva)
                    });
                }
            }
            return items;
        }

        private static List<object> BuildPagos(Electronic electronic)
        {
            List<object> pagos = new List<object>();
            if (electronic.formas != null)
            {
                foreach (Formapago pago in electronic.formas)
                {
                    Dictionary<string, object> item = new Dictionary<string, object>
                    {
                        { "metodo", MetodoPago(pago.codigo) },
                        { "total", pago.valor ?? 0 }
                    };
                    if (pago.plazo.HasValue && pago.plazo.Value > 0)
                    {
                        item["plazo"] = pago.plazo.Value;
                        item["unidadTiempo"] = string.IsNullOrEmpty(pago.tiempo) ? "dias" : pago.tiempo;
                    }
                    pagos.Add(item);
                }
            }
            return pagos;
        }

        // Replica los campoAdicional que arman las plantillas legacy WebUI/xml/{fac,nc,ret}/*.xml (nombresource/source
        // ele_nomadicionalN/ele_adicionalN, mas "Agente de Retencion" fijo con empresa.emp_agenteretxml). Cada plantilla
        // usa empty="no": si el valor viene vacio, el campo se omite entero - mismo criterio aca.
        private static List<object> BuildInfoAdicional(Electronic electronic, Empresa empresa)
        {
            List<object> info = new List<object>();

            void Add(string nombre, string valor)
            {
                // IsNullOrWhiteSpace, no IsNullOrEmpty: varios campos (Remitente/Destinatario) se arman concatenando
                // 2-3 valores con espacios (ej. "ciruc + ' ' + apellidos + ' ' + nombres") - si esos vienen vacios,
                // el resultado es "  " (solo espacios), no "" - confirmado con un caso real (NC 3130521, 2026-07-10).
                if (!string.IsNullOrWhiteSpace(nombre) && !string.IsNullOrWhiteSpace(valor))
                    info.Add(new { nombre = nombre, valor = valor.Trim() });
            }

            Add(electronic.ele_nomadicional1, electronic.ele_adicional1);
            Add(electronic.ele_nomadicional2, electronic.ele_adicional2);
            Add(electronic.ele_nomadicional3, electronic.ele_adicional3);
            Add(electronic.ele_nomadicional4, electronic.ele_adicional4);
            Add(electronic.ele_nomadicional5, electronic.ele_adicional5);
            Add(electronic.ele_nomadicional6, electronic.ele_adicional6);
            Add("Agente de Retención", empresa.emp_agenteretxml);

            return info;
        }

        private static object BuildFactura(Comprobante comprobante, Electronic electronic, Empresa empresa)
        {
            return new
            {
                tipo = "factura",
                ambiente = Ambiente(electronic),
                establecimiento = comprobante.com_almacenid,
                puntoEmision = comprobante.com_pventaid,
                fecha = FechaIso(comprobante.com_fecha),
                secuencial = comprobante.com_numero,
                cliente = new
                {
                    identificacion = electronic.ele_idcomprador,
                    nombre = electronic.ele_razonsocial,
                    email = electronic.ele_email,
                    direccion = electronic.ele_dircomprador
                },
                items = BuildItems(electronic),
                pagos = BuildPagos(electronic),
                infoAdicional = BuildInfoAdicional(electronic, empresa)
            };
        }

        // NC y RET no cargan cliente en el objeto Electronic (asi lo hace tambien la plantilla XML legacy,
        // WebUI/xml/nc/notacredito.xml y ret/retencion.xml) - el dato sale directo de Ccomdoc.
        private static object BuildNotaCredito(Comprobante comprobante, Electronic electronic, Comprobante docSustento, Empresa empresa)
        {
            return new
            {
                tipo = "nota_credito",
                ambiente = Ambiente(electronic),
                establecimiento = comprobante.com_almacenid,
                puntoEmision = comprobante.com_pventaid,
                fecha = FechaIso(comprobante.com_fecha),
                secuencial = comprobante.com_numero,
                cliente = new
                {
                    identificacion = comprobante.ccomdoc.cdoc_ced_ruc,
                    nombre = comprobante.ccomdoc.cdoc_nombre,
                    email = electronic.ele_email,
                    direccion = comprobante.ccomdoc.cdoc_direccion
                },
                motivo = string.IsNullOrEmpty(comprobante.com_concepto) ? "Nota de credito" : comprobante.com_concepto,
                documentoModificado = new
                {
                    tipo = "factura",
                    numero = docSustento != null ? string.Format("{0:000}-{1:000}-{2:000000000}", docSustento.com_almacenid, docSustento.com_pventaid, docSustento.com_numero) : "",
                    fechaEmision = docSustento != null ? FechaIso(docSustento.com_fecha) : ""
                },
                items = BuildItems(electronic),
                pagos = BuildPagos(electronic),
                infoAdicional = BuildInfoAdicional(electronic, empresa)
            };
        }

        // Misma forma que nota_credito segun el spec de Asapp (sin "pagos"). SICE nunca implemento este tipo,
        // por lo que no hay plantilla XML legacy para contrastar el mapeo - validar contra el sandbox de Asapp.
        private static object BuildNotaDebito(Comprobante comprobante, Electronic electronic, Comprobante docSustento)
        {
            return new
            {
                tipo = "nota_debito",
                ambiente = Ambiente(electronic),
                establecimiento = comprobante.com_almacenid,
                puntoEmision = comprobante.com_pventaid,
                fecha = FechaIso(comprobante.com_fecha),
                secuencial = comprobante.com_numero,
                cliente = new
                {
                    identificacion = comprobante.ccomdoc.cdoc_ced_ruc,
                    nombre = comprobante.ccomdoc.cdoc_nombre,
                    email = electronic.ele_email,
                    direccion = comprobante.ccomdoc.cdoc_direccion
                },
                motivo = string.IsNullOrEmpty(comprobante.com_concepto) ? "Nota de debito" : comprobante.com_concepto,
                documentoModificado = new
                {
                    tipo = "factura",
                    numero = docSustento != null ? string.Format("{0:000}-{1:000}-{2:000000000}", docSustento.com_almacenid, docSustento.com_pventaid, docSustento.com_numero) : "",
                    fechaEmision = docSustento != null ? FechaIso(docSustento.com_fecha) : ""
                },
                items = BuildItems(electronic)
            };
        }

        private static object BuildRetencion(Comprobante comprobante, Electronic electronic, Comprobante docSustento, Empresa empresa)
        {
            List<object> documentosSustento = new List<object>();
            if (electronic.detalle != null)
            {
                var grupos = electronic.detalle.GroupBy(d => new { d.eled_numdocsustento, d.eled_fechadocsustento });
                foreach (var grupo in grupos)
                {
                    List<object> retenciones = new List<object>();
                    foreach (Electronicdet det in grupo)
                    {
                        retenciones.Add(new
                        {
                            tipoImpuesto = det.eled_codigo == "2" ? "iva" : "renta",
                            codigoRetencion = det.eled_codigoaux,
                            baseImponible = det.eled_baseimp ?? 0,
                            porcentaje = det.eled_porcret ?? 0
                        });
                    }
                    documentosSustento.Add(new
                    {
                        tipoDocumento = "factura",
                        numeroDocumento = grupo.Key.eled_numdocsustento,
                        // eled_fechadocsustento ya viene en dd/MM/yyyy (formato legacy); se reconvierte desde docSustento.com_fecha si esta disponible para asegurar ISO 8601.
                        fechaEmision = docSustento != null ? FechaIso(docSustento.com_fecha) : "",
                        // numeroAutorizacion = clave de acceso ya autorizada del documento sustento (docSustento.com_claveelec).
                        numeroAutorizacion = docSustento != null ? docSustento.com_claveelec : "",
                        formaPago = "efectivo",
                        retenciones = retenciones
                    });
                }
            }

            return new
            {
                tipo = "retencion",
                ambiente = Ambiente(electronic),
                establecimiento = comprobante.com_almacenid,
                puntoEmision = comprobante.com_pventaid,
                fecha = FechaIso(comprobante.com_fecha),
                secuencial = comprobante.com_numero,
                retencion = new
                {
                    mes = comprobante.com_fecha.Month,
                    anio = comprobante.com_fecha.Year,
                    identificacion = comprobante.ccomdoc.cdoc_ced_ruc,
                    nombre = comprobante.ccomdoc.cdoc_nombre,
                    email = electronic.ele_email,
                    documentosSustento = documentosSustento
                },
                infoAdicional = BuildInfoAdicional(electronic, empresa)
            };
        }
    }
}
