using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using BusinessLogicLayer;
using Services;
using System.Web.Script.Serialization;
using System.Xml;
using System.Reflection;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using HtmlObjects;

namespace Packages
{
    public class Electronico
    {

        // Estado por-instancia (antes eran campos static compartidos por todo el AppDomain - causaba que dos
        // comprobantes procesandose casi al mismo tiempo se pisaran los datos entre si sin ninguna excepcion).
        // Cada llamada a GenerateElectronico crea su propia instancia de Electronico, asi que estos campos
        // quedan aislados por request.
        public XmlDocument xmldoc = new XmlDocument();
        public Comprobante comprobante = new Comprobante();
        public Ccomdoc ccomdoc = new Ccomdoc();
        public List<Formapago> formas = new List<Formapago>();
        public Electronic electronic = new Electronic();
        public List<Electronicdet> electronicdet = new List<Electronicdet>();
        public Empresa empresa = new Empresa();
        public string path = "";

        // Cachés de reflexión - estos SI pueden ser static de verdad: son metadata del TIPO (PropertyInfo[]),
        // no de una instancia particular, y no cambian entre requests.
        private static readonly PropertyInfo[] ComprobanteProps = typeof(Comprobante).GetProperties();
        private static readonly PropertyInfo[] CcomdocProps = typeof(Ccomdoc).GetProperties();
        private static readonly PropertyInfo[] FormapagoProps = typeof(Formapago).GetProperties();
        private static readonly PropertyInfo[] ElectronicProps = typeof(Electronic).GetProperties();
        private static readonly PropertyInfo[] ElectronicdetProps = typeof(Electronicdet).GetProperties();
        private static readonly PropertyInfo[] EmpresaProps = typeof(Empresa).GetProperties();


        public static bool IsElectronicUser(string usuarios, Comprobante com)
        {
            if (!string.IsNullOrEmpty(usuarios))
            {
                string[] arrayusuarios = usuarios.Split(',');

                for (int i = 0; i < arrayusuarios.Length; i++)
                {
                    if (arrayusuarios[i] == com.crea_usr)
                        return true;
                }
                return false;

            }
            return true;            
        }


        public static bool IsElectronic(Comprobante com)
        {

            string parelectronicos = Constantes.GetParameter("electronicos");
            var serializer = new JavaScriptSerializer();
            List<Electronicos> lst = serializer.Deserialize<List<Electronicos>>(parelectronicos);

            Electronicos ele = lst.Find(delegate (Electronicos e) { return e.empresa == com.com_empresa && e.tipodoc == com.com_tipodoc; });
            if (ele != null)
            {
                if (ele.desde.HasValue || ele.hasta.HasValue)
                {
                    if (ele.desde.HasValue && ele.hasta.HasValue)
                    {
                        if (com.com_fecha >= ele.desde.Value && com.com_fecha <= ele.hasta.Value)
                            return IsElectronicUser(ele.usuarios,com);
                        else
                            return false;
                    }
                    else if (ele.desde.HasValue)
                    {
                        if (com.com_fecha >= ele.desde.Value)
                            return IsElectronicUser(ele.usuarios, com);
                        else
                            return false;
                    }
                    else if (ele.hasta.HasValue)
                    {
                        if (com.com_fecha <= ele.hasta.Value)
                            return IsElectronicUser(ele.usuarios, com);
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return true;
            }
            else
                return false;
        }

        public static string ElectronicRIDE(Comprobante com)
        {
            if (ResolveProvider(com) == "ASAPP")
                return ElectronicoAsapp.GetRideUrl(com);

            string parelectronicride = Constantes.GetParameter("electronicride");
            return parelectronicride + com.com_claveelec;


        }


        public static string GetTipoId(string tipoid, string id, List<ElectronicIds> lst)
        {
            int idLength = (id ?? "").Trim().Length;
            if (tipoid == "Cédula" && idLength == 13)
                tipoid = "RUC";
            if (tipoid == "RUC" && idLength == 10)
                tipoid = "Cédula";


            ElectronicIds tipo = lst.Find(delegate (ElectronicIds e) { return e.tipo == tipoid; });
            if (tipo != null)
            {
                if (id.LastIndexOf("9999999999") >= 0)
                    tipo = lst.Find(delegate (ElectronicIds e) { return e.tipo == "Consumidor Final"; });
                return tipo.codigo;
            }
            return "";
        }

        public static int GetCodigoImp(decimal? porcentaje, List<ElectronicImp> lst)
        {
            if (porcentaje.HasValue)
            {
                ElectronicImp imp = lst.Find(delegate (ElectronicImp e) { return e.porcentaje == porcentaje; });
                if (imp != null)
                    return imp.codigo;
            }
            return 0;

        }

        public static int GetCodigoRet(Dretencion dret, List<ElectronicRet> lst, List<ElectronicPorcRet> lstporcret, List<Impuesto> lstimpuestos, List<Concepto> lstconceptos, out string codigoaux)
        {
            Impuesto imp = lstimpuestos.Find(delegate (Impuesto e) { return e.imp_codigo == dret.drt_impuesto.Value; });

            string tipo = "";
            codigoaux = "";

            if ((imp.imp_iva ?? 0) == 1)
            {
                tipo = "IVA";
                codigoaux = GetCodigoPorcRet(dret.drt_porcentaje, lstporcret).ToString();
            }

            if ((imp.imp_ret ?? 0) == 1)
            {
                tipo = "RENTA";
                Concepto con = lstconceptos.Find(delegate (Concepto c) { return c.con_codigo == dret.drt_con_codigo; });
                codigoaux = con.con_id;
            }






            ElectronicRet impret = lst.Find(delegate (ElectronicRet e) { return e.impuesto == tipo; });
            if (impret != null)
                return impret.codigo;
            return 0;

        }

        public static int GetCodigoPorcRet(decimal? porcentaje, List<ElectronicPorcRet> lst)
        {
            if (porcentaje.HasValue)
            {
                ElectronicPorcRet imp = lst.Find(delegate (ElectronicPorcRet e) { return e.porcentaje == porcentaje; });
                if (imp != null)
                    return imp.codigo;
            }
            return 0;

        }




        public static string GetCheckDigit(string number)
        {
            int Sum = 0;
            for (int i = number.Length - 1, Multiplier = 2; i >= 0; i--)
            {
                Sum += (int)char.GetNumericValue(number[i]) * Multiplier;

                if (++Multiplier == 8) Multiplier = 2;
            }
            string Validator = (11 - (Sum % 11)).ToString();

            if (Validator == "11") Validator = "0";
            else if (Validator == "10") Validator = "1";

            return Validator;
        }

        // Tabla 33 (Anexo 25, ficha tecnica): sin espacios, y si el segmento numerico final tiene
        // solo 3 digitos se le antepone un cero (ABC123 -> ABC0123).
        private static string FormatPlacaSri(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                return "";
            string limpio = placa.Trim().ToUpper().Replace(" ", "").Replace("-", "");
            var m = System.Text.RegularExpressions.Regex.Match(limpio, @"^([A-Z]+)(\d{3})$");
            return m.Success ? m.Groups[1].Value + "0" + m.Groups[2].Value : limpio;
        }

        public static string GetClave(Comprobante comp, string tipocomprobante, Empresa empresa, Electronic ele)
        {
            string clave = "";
            clave += comp.com_fecha.ToString("ddMMyyyy"); //FECHA EMISION
            clave += tipocomprobante; //TIPO (01-FACTURA  04-NOTA DE CREDITO 05-NOTA DE DEBITO 06-GUIAREMISION 07-RETENCION)
            clave += empresa.emp_id; //RUC DE LA EMPRESA EMISORA
            clave += ele.ele_ambiente.ToString(); //TIPO DE AMBIENTE (1 PRUEBAS   2 PRODUCCION)
            clave += ele.ele_almacen.ToString() + ele.ele_pventa.ToString();
            clave += ele.ele_secuencia;
            clave += comp.com_codigo.ToString("00000000");
            clave += ele.ele_emision;
            clave += GetCheckDigit(clave);

            return clave;



        }

        public static Electronicos GetElectronicoConfig(Comprobante com)
        {
            string parelectronicos = Constantes.GetParameter("electronicos");
            var serializer = new JavaScriptSerializer();
            List<Electronicos> lst = serializer.Deserialize<List<Electronicos>>(parelectronicos);
            return  lst.Find(delegate (Electronicos e) { return e.empresa == com.com_empresa && e.tipodoc == com.com_tipodoc; });
        }

        // Decide a que proveedor consultar/enviar. Prioridad:
        // 1) com.com_provider == "ASAPP": este comprobante puntual ya se envio por Asapp, siempre se consulta ahi
        //    (no depende de la config actual, por si se revierte el switch para documentos nuevos).
        // 2) Config actual del switch (parametro "electronicos") para esta empresa+tipodoc: permite que comprobantes
        //    historicos migrados a Asapp (enviados originalmente por SICE, sin com_provider) tambien se consulten
        //    ahi una vez activado el switch - Asapp expone endpoints por clave de acceso (no por su id interno)
        //    que funcionan para cualquier comprobante con com_claveelec, sea de SICE o de Asapp.
        // 3) Default SICE.
        private static string ResolveProvider(Comprobante com)
        {
            if (string.Equals(com.com_provider, "ASAPP", StringComparison.OrdinalIgnoreCase))
                return "ASAPP";

            Electronicos config = GetElectronicoConfig(com);
            return (config != null && !string.IsNullOrEmpty(config.provider)) ? config.provider.Trim().ToUpper() : "SICE";
        }


        public static string GetString(string texto, int max)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                if (texto.Length > max)
                    return texto.Substring(0, max).Replace(Environment.NewLine, "");
                else
                    return texto.Replace('\n', ' ');
            }
            return "";
        }


        public static string GetNumDocSustento(string doc)
        {
            string[] arraydoc = doc.Split('-');
            if (arraydoc.Length > 2)
            {
                int alm = int.Parse(arraydoc[0]);
                int pve = int.Parse(arraydoc[1]);
                int sec = int.Parse(arraydoc[2]);

                return string.Format("{0:000}{1:000}{2:000000000}", alm, pve, sec);

            }
            return doc;


        }

        public Electronic LoadElectronico(Comprobante com, List<Drecibo> detallerecibo)
        {

            string parelectronicos = Constantes.GetParameter("electronicos");
            string parelectronicids = Constantes.GetParameter("electronicids");
            string parelectronicimp = Constantes.GetParameter("electronicimp");
            string parelectronicpagos = Constantes.GetParameter("electronicpagos");
            string parelectronicret = Constantes.GetParameter("electronicret");
            string parelectronicporcret= Constantes.GetParameter("electronicporcret");
           


            var serializer = new JavaScriptSerializer();
            List<Electronicos> lst = serializer.Deserialize<List<Electronicos>>(parelectronicos);
            List<ElectronicIds> lstids = serializer.Deserialize<List<ElectronicIds>>(parelectronicids);
            List<ElectronicImp> lstimp = serializer.Deserialize<List<ElectronicImp>>(parelectronicimp);
            List<ElectronicPago> lstpag = serializer.Deserialize<List<ElectronicPago>>(parelectronicpagos);
            List<ElectronicRet> lstret= serializer.Deserialize<List<ElectronicRet>>(parelectronicret);
            List<ElectronicPorcRet> lstporcret = serializer.Deserialize<List<ElectronicPorcRet>>(parelectronicporcret);


            Electronicos ele = lst.Find(delegate (Electronicos e) { return e.empresa == com.com_empresa && e.tipodoc == com.com_tipodoc; });
            if (ele != null)
            {
                Persona persona = new Persona();
                persona.per_empresa = com.com_empresa;
                persona.per_empresa_key = com.com_empresa;
                if (com.com_codclipro.HasValue)
                {
                    persona.per_codigo = com.com_codclipro.Value;
                    persona.per_codigo_key = com.com_codclipro.Value;
                    persona = PersonaBLL.GetByPK(persona);
                }

                Persona informante = new Persona();
                informante.per_empresa = com.com_empresa;
                informante.per_empresa_key = com.com_empresa;
                informante.per_codigo = empresa.emp_informante.Value;
                informante.per_codigo_key = empresa.emp_informante.Value;
                informante = PersonaBLL.GetByPK(informante);

                if (string.IsNullOrEmpty(persona.per_mail))
                    persona.per_mail = informante.per_mail;


                List<Almacen> almacenes = AlmacenBLL.GetAll("alm_empresa=" + com.com_empresa, "");

                Almacen matriz = almacenes.Find(delegate (Almacen a) { return a.alm_matriz == 1; });
                Almacen sucursal = almacenes.Find(delegate (Almacen a) { return a.alm_codigo == com.com_almacen; });

                #region FACTURA

                if (ele.tipodoc == Constantes.cFactura.tpd_codigo)//GUARDA TIPODOC PARA FACTURA
                {
                    Electronic electronic = new Electronic();


                    electronic.ele_empresa = com.com_empresa;
                    electronic.ele_comprobante = com.com_codigo;
                    electronic.ele_almacen = com.com_almacenid;
                    electronic.ele_pventa = com.com_pventaid;
                    electronic.ele_secuencia = com.com_numero.ToString("000000000");
                    electronic.ele_ambiente = ele.ambiente; // 1=PRUEBAS 2=PRODUCCION
                    electronic.ele_emision = 1; //    AQUI SIEMPRES SERIA 1=EMISION NORMAL, 2=EMISION POR INDISPONIBILIDAD NO SE MANEJARIA
                    electronic.ele_tipo = com.com_tipodoc;
                    electronic.ele_email =  persona.per_mail;                    
                    electronic.ele_dirmatriz = matriz.alm_direccion;
                    electronic.ele_dirsucursal = sucursal.alm_direccion;
                    electronic.ele_especial = ele.especial;
                    electronic.ele_contabilidad = ele.contabilidad;
                    electronic.ele_tipoid = GetTipoId(persona.per_tipoid, persona.per_ciruc, lstids);
                    electronic.ele_placa = FormatPlacaSri(com.com_placasri);
                    string nombres = persona.per_apellidos + " " + persona.per_nombres;
                    electronic.ele_razonsocial = nombres.Trim();
                    electronic.ele_idcomprador = persona.per_ciruc;
                    electronic.ele_dircomprador = !string.IsNullOrEmpty(persona.per_direccion) ? persona.per_direccion : "S/D";
                    electronic.ele_totaldesc = com.total.tot_desc1_0 + com.total.tot_desc2_0;
                    electronic.ele_totalsinimp = com.total.tot_subtotal + com.total.tot_subtot_0 + com.total.tot_transporte + (com.total.tot_tseguro ?? 0) - (com.total.tot_desc1_0 + com.total.tot_desc2_0);
                    //electronic.ele_propina = 
                    electronic.ele_total = com.total.tot_total;


                    //IMPUESTOS

                    decimal porciva =  Constantes.GetValorIVA(com.com_fecha);

                    //IVA
                    electronic.ele_iva = com.total.tot_subtotal + (com.total.tot_tseguro??0);
                    electronic.ele_porciva = com.total.tot_porc_impuesto.HasValue ? com.total.tot_porc_impuesto.Value : porciva;
                    electronic.ele_codigoiva = GetCodigoImp(electronic.ele_porciva, lstimp);
                    electronic.ele_valoriva = com.total.tot_timpuesto;

                    //IVA 0
                    electronic.ele_iva0 = com.total.tot_subtot_0 + com.total.tot_transporte - (com.total.tot_desc1_0 + com.total.tot_desc2_0);
                    electronic.ele_codigoiva0 = GetCodigoImp(0, lstimp);

                    //ICE


                    electronic.ele_ice = com.total.tot_ice;
                    //FALTA LOS PORCENTAJES Y CODIGOS

                    //ele_guiaremision = com.ccomenv.cenv_guia1

                    electronic.ele_adicional1 = com.ccomdoc.cdoc_politicanombre;//ENVIO LA POLITICA DE VENTA
                    electronic.ele_nomadicional1 = "Fpago";
                    electronic.ele_adicional2 = com.ccomenv.cenv_ciruc_rem + " " + com.ccomenv.cenv_apellidos_rem+ " " +com.ccomenv.cenv_nombres_rem;
                    electronic.ele_nomadicional2 = "Remitente";
                    electronic.ele_adicional3 = com.ccomenv.cenv_ciruc_des + " " + com.ccomenv.cenv_apellidos_des+ " " + com.ccomenv.cenv_nombres_des;
                    electronic.ele_nomadicional3 = "Destinatario";
                    electronic.ele_adicional4 = com.ccomenv.cenv_rutadestino;
                    electronic.ele_nomadicional4 = "Ciudad";
                    electronic.ele_adicional5 = com.ccomdoc.cdoc_telefono;//ENVIO LA POLITICA DE VENTA
                    electronic.ele_nomadicional5 = "Telefono";
                    if ((com.total.tot_vseguro ?? 0) > 0)
                    {
                        electronic.ele_adicional6 = Functions.Formatos.CurrencyFormat(com.total.tot_vseguro);//ENVIO LA POLITICA DE VENTA
                        electronic.ele_nomadicional6 = "Valor declarado";
                    }
                    else
                    {
                        electronic.ele_adicional6 = "Cliente no acepta seguro, viaja sin responsabilidad";
                        electronic.ele_nomadicional6 = "Aviso";
                    }







                    electronic.ele_formato = ele.formato;
                    electronic.ele_clave = GetClave(com, "01", empresa, electronic);

                    List<Electronicdet> detalle = new List<Electronicdet>();

                    int secuencia = 0;
                    foreach (Dcomdoc item in com.ccomdoc.detalle)
                    {
                        secuencia++;
                        Electronicdet det = new Electronicdet();
                        det.eled_empresa = com.com_empresa;
                        det.eled_comprobante = com.com_codigo;
                        det.eled_secuencia = secuencia;
                        det.eled_producto = item.ddoc_producto.Value;
                        det.eled_codigo =  GetString( item.ddoc_productoid,25);
                        det.eled_codigoaux = GetString(item.ddoc_productocodigoaux,25); // NO SE ESTA USANDO CODIGO AUX
                        det.eled_descripcion = GetString(item.ddoc_productonombre + " " + item.ddoc_observaciones, 300);
                        if (item.ddoc_productototal.HasValue && item.ddoc_productototal.Value == 1)
                        {
                            // Producto "total por producto" (producto.pro_total=1, ej. codigo 252 "TOTAL" en
                            // Carlogistica - ver ValidarCuadreDetalle en FAC.cs para el mismo criterio): el precio
                            // unitario es SIEMPRE derivado (total/cantidad, redondeado a 2 decimales solo para
                            // mostrar), asi que cantidad*precio casi nunca reproduce el total exacto - puede dar
                            // descuento negativo (Asapp lo rechaza entero) o positivo (se pierde el centavo igual
                            // por el clamping de abajo). Caso real Carlogistica FAC-001-001-0117236, 04-nov-2026:
                            // SIAC $32.83, autorizado por Asapp en $32.80 (3 centavos perdidos). Fix definitivo para
                            // estas lineas: reportar cantidad=1, precio=total exacto, descuento=0 - igual al patron
                            // ya usado abajo para TRANSPORTE A DOMICILIO/SEGURO. Nunca mas hay rounding que perder.
                            det.eled_cantidad = 1;
                            det.eled_precio = item.ddoc_total;
                            det.eled_descuento = 0;
                        }
                        else
                        {
                            det.eled_cantidad = item.ddoc_cantidad;
                            det.eled_precio = item.ddoc_precio;
                            // Antes solo calculaba el descuento si ddoc_dscitem>0 - si el total de la linea se
                            // sobreescribio manualmente (ej. item de cortesia/sin costo) sin tocar ddoc_dscitem, el
                            // descuento quedaba en 0 aunque cantidad*precio no coincidiera con el total real. SICE nunca
                            // lo noto porque usa directamente precioTotalSinImpuesto (eled_totalsinimp); Asapp si, porque
                            // calcula el total de cada item como cantidad*precioUnitario-descuento, de su lado.
                            // Nunca negativo: el XSD del SRI exige descuento >= 0 (MinInclusive). Un total que
                            // redondeo 1 centavo POR ENCIMA de cantidad*precio (normal, ej. 13*9.23=119.99 pero
                            // ddoc_total=120.00) daria un "descuento" negativo (-0.01) - Asapp lo rechaza entero
                            // ("The value '-0.01' is invalid... MinInclusive constraint failed", caso real
                            // Carlogistica FAC-001-001-0116246, 2026-10-05). Clamping a 0 en vez de negativo: el
                            // peor caso es un redondeo de centavo no reflejado en el item individual, muy por
                            // debajo de lo que vale bloquear el envio entero.
                            det.eled_descuento = Math.Max(0, (item.ddoc_cantidad * item.ddoc_precio) - item.ddoc_total);
                        }
                        det.eled_totalsinimp = item.ddoc_total;
                        det.eled_adicional1 = item.ddoc_observaciones;

                        //IMPUESTOS
                        if (item.ddoc_grabaiva.Value == 1)
                        {

                            //IVA
                            det.eled_porciva = com.total.tot_porc_impuesto;
                            det.eled_codigoiva = GetCodigoImp(det.eled_porciva, lstimp);
                            det.eled_iva = Math.Round((decimal)(det.eled_totalsinimp * (det.eled_porciva / 100)),2); //item.ddoc_ivaitem;


                        }
                        else
                        {
                            //IVA 0
                            det.eled_iva0 = item.ddoc_total;
                            det.eled_codigoiva0 = GetCodigoImp(0, lstimp);
                        }


                        if (item.ddoc_iceitem.HasValue)
                        {
                            det.eled_ice = item.ddoc_iceitem;
                            //FALTA LOS PORCENTAJES Y CODIGOS
                        }

                        det.crea_usr = item.crea_usr;
                        det.crea_fecha = DateTime.Now;
                        detalle.Add(det);

                    }


                    if (com.total.tot_transporte>0)
                    {
                        secuencia++;
                        Electronicdet det = new Electronicdet();
                        det.eled_empresa = com.com_empresa;
                        det.eled_comprobante = com.com_codigo;
                        det.eled_secuencia = secuencia;
                        det.eled_producto = 999;
                        det.eled_codigo = "00009999";
                        det.eled_codigoaux = "00009999"; // NO SE ESTA USANDO CODIGO AUX
                        det.eled_descripcion = "TRANSPORTE A DOMICILIO";
                        det.eled_cantidad = 1;
                        det.eled_precio = com.total.tot_transporte;
                        det.eled_descuento = 0;
                        det.eled_totalsinimp = com.total.tot_transporte;
                        //det.eled_adicional1 = item.ddoc_observaciones;                        
                        det.eled_iva0 = com.total.tot_transporte;
                        det.eled_codigoiva0 = GetCodigoImp(0, lstimp);                                                
                        detalle.Add(det);
                    }

                    if (com.total.tot_tseguro> 0)
                    {
                        secuencia++;
                        Electronicdet det = new Electronicdet();
                        det.eled_empresa = com.com_empresa;
                        det.eled_comprobante = com.com_codigo;
                        det.eled_secuencia = secuencia;
                        det.eled_producto = 998;
                        det.eled_codigo = "00009998";
                        det.eled_codigoaux = "00009998"; // NO SE ESTA USANDO CODIGO AUX
                        det.eled_descripcion = "SEGURO";
                        det.eled_cantidad = 1;
                        det.eled_precio = com.total.tot_tseguro;
                        det.eled_descuento = 0;
                        det.eled_totalsinimp = com.total.tot_tseguro;

                        det.eled_porciva = com.total.tot_porc_impuesto;
                        det.eled_codigoiva = GetCodigoImp(det.eled_porciva, lstimp);
                        det.eled_iva = Math.Round((decimal)(det.eled_totalsinimp * (det.eled_porciva / 100)), 2); //item.ddoc_ivaitem;
                        //det.eled_adicional1 = item.ddoc_observaciones;                                                
                        detalle.Add(det);
                    }

                    electronic.detalle = detalle;


                    //////FORMAS DE PAGO/////

                    List<Formapago> lstformas = new List<Formapago>();
                   
                   


                    /*if (detallerecibo != null)
                    {
                        foreach (Drecibo item in detallerecibo)
                        {
                            if (item.dfp_monto > 0)
                            {
                                foreach (ElectronicPago p in lstpag)
                                {
                                    if (!string.IsNullOrEmpty(p.tipopago))
                                    {
                                        string[] arraytipos = p.tipopago.Split(',');
                                        for (int t = 0; t < arraytipos.Length; t++)
                                        {
                                            if (arraytipos[t] == item.dfp_tipopago.ToString())
                                            {
                                                Formapago fp = lstformas.Find(delegate (Formapago f) { return f.codigo == p.codigo; });
                                                if (fp != null)
                                                {
                                                    fp.valor += item.dfp_monto;                                                    
                                                }
                                                else
                                                {
                                                    fp = new Formapago();
                                                    fp.codigo = p.codigo;
                                                    fp.forma = p.forma;
                                                    fp.valor = item.dfp_monto;
                                                    lstformas.Add(fp);

                                                }

                                            }
                                        }
                                    }

                                }
                            }

                        }
                    }*/
                    Politica politica = PoliticaBLL.GetByPK(new Politica() { pol_empresa = com.com_empresa, pol_empresa_key = com.com_empresa, pol_codigo = com.ccomdoc.cdoc_politica ?? 0, pol_codigo_key = com.ccomdoc.cdoc_politica ?? 0 });
                    decimal totalpago = lstformas.Sum(s => s.valor ?? 0);
                    if (electronic.ele_total>totalpago || electronic.ele_total==0)
                    {


                        ElectronicPago ep = lstpag.Find(delegate (ElectronicPago p) { return p.defecto== "si"; });
                        if (ep == null)
                            ep = lstpag[0];

                        decimal saldo = electronic.ele_total.Value - totalpago;
                        Formapago fp = new Formapago();
                        fp.codigo = ep.codigo;
                        fp.forma = ep.forma;
                        fp.valor = saldo;
                        fp.plazo = politica.pol_dias_plazo;
                        fp.tiempo = "dias";
                        lstformas.Add(fp);

                    }


                    electronic.formas = lstformas;


                    return electronic;

                }

                #endregion

                #region LIQUIDACION DE COMPRA

                // No hay pagina propia: wfObligacion.aspx maneja Liquidacion de Compra igual que otras obligaciones
                // de CxP, diferenciada por com_tipodoc. El contraparte (persona/agricultor) sale de com_codclipro,
                // igual que en FACTURA (persona/informante ya resueltos en el preambulo de este metodo). Items desde
                // Dcomdoc (com.ccomdoc.detalle), igual que FACTURA. No se arman los "adicionales" de envio
                // (Remitente/Destinatario/Ciudad/Seguro) ni las lineas extra de transporte/seguro: no aplican a una
                // compra a un individuo (esto es distinto de FACTURA, que si es una venta con posible despacho).
                if (ele.tipodoc == Constantes.cLiquidacionCompra.tpd_codigo)
                {
                    Electronic electronic = new Electronic();

                    electronic.ele_empresa = com.com_empresa;
                    electronic.ele_comprobante = com.com_codigo;
                    electronic.ele_almacen = com.com_almacenid;
                    electronic.ele_pventa = com.com_pventaid;
                    electronic.ele_secuencia = com.com_numero.ToString("000000000");
                    electronic.ele_ambiente = ele.ambiente; // 1=PRUEBAS 2=PRODUCCION
                    electronic.ele_emision = 1;
                    electronic.ele_tipo = com.com_tipodoc;
                    electronic.ele_email = persona.per_mail;
                    electronic.ele_dirmatriz = matriz.alm_direccion;
                    electronic.ele_dirsucursal = sucursal.alm_direccion;
                    electronic.ele_especial = ele.especial;
                    electronic.ele_contabilidad = ele.contabilidad;
                    electronic.ele_tipoid = GetTipoId(persona.per_tipoid, persona.per_ciruc, lstids);
                    string nombresLc = persona.per_apellidos + " " + persona.per_nombres;
                    electronic.ele_razonsocial = nombresLc.Trim();
                    electronic.ele_idcomprador = persona.per_ciruc;
                    electronic.ele_dircomprador = !string.IsNullOrEmpty(persona.per_direccion) ? persona.per_direccion : "S/D";
                    electronic.ele_totaldesc = com.total.tot_desc1_0 + com.total.tot_desc2_0;
                    electronic.ele_totalsinimp = com.total.tot_subtotal + com.total.tot_subtot_0 - (com.total.tot_desc1_0 + com.total.tot_desc2_0);
                    electronic.ele_total = com.total.tot_total;

                    //IMPUESTOS
                    decimal porcivaLc = Constantes.GetValorIVA(com.com_fecha);

                    //IVA
                    electronic.ele_iva = com.total.tot_subtotal;
                    electronic.ele_porciva = com.total.tot_porc_impuesto.HasValue ? com.total.tot_porc_impuesto.Value : porcivaLc;
                    electronic.ele_codigoiva = GetCodigoImp(electronic.ele_porciva, lstimp);
                    electronic.ele_valoriva = com.total.tot_timpuesto;

                    //IVA 0
                    electronic.ele_iva0 = com.total.tot_subtot_0 - (com.total.tot_desc1_0 + com.total.tot_desc2_0);
                    electronic.ele_codigoiva0 = GetCodigoImp(0, lstimp);

                    electronic.ele_ice = com.total.tot_ice;

                    electronic.ele_formato = ele.formato;
                    // Codigo SRI 03 = Liquidacion de Compra (ver GetClave, contempla 01/03/04/05/06/07)
                    electronic.ele_clave = GetClave(com, "03", empresa, electronic);

                    List<Electronicdet> detalle = new List<Electronicdet>();

                    int secuenciaLc = 0;
                    foreach (Dcomdoc item in com.ccomdoc.detalle)
                    {
                        secuenciaLc++;
                        Electronicdet det = new Electronicdet();
                        det.eled_empresa = com.com_empresa;
                        det.eled_comprobante = com.com_codigo;
                        det.eled_secuencia = secuenciaLc;
                        det.eled_producto = item.ddoc_producto.Value;
                        det.eled_codigo = GetString(item.ddoc_productoid, 25);
                        det.eled_codigoaux = GetString(item.ddoc_productocodigoaux, 25);
                        det.eled_descripcion = GetString(item.ddoc_productonombre + " " + item.ddoc_observaciones, 300);
                        if (item.ddoc_productototal.HasValue && item.ddoc_productototal.Value == 1)
                        {
                            // Mismo fix que en la rama de Factura mas arriba - ver el comentario largo ahi.
                            // Producto "total por producto" (pro_total=1): reportar cantidad=1, precio=total
                            // exacto, descuento=0, para que nunca se pierda un centavo por redondeo del precio
                            // unitario derivado.
                            det.eled_cantidad = 1;
                            det.eled_precio = item.ddoc_total;
                            det.eled_descuento = 0;
                        }
                        else
                        {
                            det.eled_cantidad = item.ddoc_cantidad;
                            det.eled_precio = item.ddoc_precio;
                            // Antes solo calculaba el descuento si ddoc_dscitem>0 - si el total de la linea se
                            // sobreescribio manualmente (ej. item de cortesia/sin costo) sin tocar ddoc_dscitem, el
                            // descuento quedaba en 0 aunque cantidad*precio no coincidiera con el total real. SICE nunca
                            // lo noto porque usa directamente precioTotalSinImpuesto (eled_totalsinimp); Asapp si, porque
                            // calcula el total de cada item como cantidad*precioUnitario-descuento, de su lado.
                            // Nunca negativo: el XSD del SRI exige descuento >= 0 (MinInclusive). Un total que
                            // redondeo 1 centavo POR ENCIMA de cantidad*precio (normal, ej. 13*9.23=119.99 pero
                            // ddoc_total=120.00) daria un "descuento" negativo (-0.01) - Asapp lo rechaza entero
                            // ("The value '-0.01' is invalid... MinInclusive constraint failed", caso real
                            // Carlogistica FAC-001-001-0116246, 2026-10-05). Clamping a 0 en vez de negativo: el
                            // peor caso es un redondeo de centavo no reflejado en el item individual, muy por
                            // debajo de lo que vale bloquear el envio entero.
                            det.eled_descuento = Math.Max(0, (item.ddoc_cantidad * item.ddoc_precio) - item.ddoc_total);
                        }
                        det.eled_totalsinimp = item.ddoc_total;
                        det.eled_adicional1 = item.ddoc_observaciones;

                        //IMPUESTOS
                        if (item.ddoc_grabaiva.Value == 1)
                        {
                            det.eled_porciva = com.total.tot_porc_impuesto;
                            det.eled_codigoiva = GetCodigoImp(det.eled_porciva, lstimp);
                            det.eled_iva = Math.Round((decimal)(det.eled_totalsinimp * (det.eled_porciva / 100)), 2);
                        }
                        else
                        {
                            det.eled_iva0 = item.ddoc_total;
                            det.eled_codigoiva0 = GetCodigoImp(0, lstimp);
                        }

                        det.crea_usr = item.crea_usr;
                        det.crea_fecha = DateTime.Now;
                        detalle.Add(det);
                    }

                    electronic.detalle = detalle;

                    //////FORMAS DE PAGO/////
                    List<Formapago> lstformasLc = new List<Formapago>();
                    Politica politicaLc = PoliticaBLL.GetByPK(new Politica() { pol_empresa = com.com_empresa, pol_empresa_key = com.com_empresa, pol_codigo = com.ccomdoc.cdoc_politica ?? 0, pol_codigo_key = com.ccomdoc.cdoc_politica ?? 0 });
                    ElectronicPago epLc = lstpag.Find(delegate (ElectronicPago p) { return p.defecto == "si"; });
                    if (epLc == null)
                        epLc = lstpag[0];

                    Formapago fpLc = new Formapago();
                    fpLc.codigo = epLc.codigo;
                    fpLc.forma = epLc.forma;
                    fpLc.valor = electronic.ele_total;
                    fpLc.plazo = politicaLc.pol_dias_plazo;
                    fpLc.tiempo = "dias";
                    lstformasLc.Add(fpLc);

                    electronic.formas = lstformasLc;

                    return electronic;
                }

                #endregion

                #region RETENCIÓN

                if (ele.tipodoc == Constantes.cRetencion.tpd_codigo)//GUARDA TIPODOC PARA RETENCION
                {

                    Comprobante obligacion = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = com.com_empresa, com_empresa_key = com.com_empresa, com_codigo = com.ccomdoc.cdoc_factura.Value, com_codigo_key = com.ccomdoc.cdoc_factura.Value });
                    List<Impuesto> lstimpuestos = ImpuestoBLL.GetAll("imp_empresa=" + com.com_empresa, "");
                    List<Concepto> lstconceptos= ConceptoBLL.GetAll("con_empresa=" + com.com_empresa, "");

                    Electronic electronic = new Electronic();


                    electronic.ele_empresa = com.com_empresa;
                    electronic.ele_comprobante = com.com_codigo;
                    electronic.ele_almacen = com.com_almacenid;
                    electronic.ele_pventa = com.com_pventaid;
                    electronic.ele_secuencia = com.com_numero.ToString("000000000");
                    electronic.ele_ambiente = ele.ambiente; // 1=PRUEBAS 2=PRODUCCION
                    electronic.ele_emision = 1; //    AQUI SIEMPRES SERIA 1=EMISION NORMAL, 2=EMISION POR INDISPONIBILIDAD NO SE MANEJARIA
                    electronic.ele_tipo = com.com_tipodoc;
                    electronic.ele_email = persona.per_mail;
                    electronic.ele_dirmatriz = matriz.alm_direccion;
                    electronic.ele_dirsucursal = sucursal.alm_direccion;
                    electronic.ele_especial = ele.especial;
                    electronic.ele_contabilidad = ele.contabilidad;
                    electronic.ele_tipoid = GetTipoId(persona.per_tipoid, persona.per_ciruc, lstids);
                    electronic.ele_periodo = obligacion.com_fecha.ToString("MM/yyyy");
                    //electronic.ele_totalsinimp = com.total.tot_subtotal + com.total.tot_subtot_0 + com.total.tot_transporte + (com.total.tot_tseguro ?? 0);                    
                    //electronic.ele_total = com.total.tot_total;

                    electronic.ele_adicional1 = com.ccomdoc.cdoc_direccion;//ENVIO LA POLITICA DE VENTA
                    electronic.ele_nomadicional1 = "Direccion";
                    electronic.ele_adicional2 = persona.per_mail;//ENVIO LA POLITICA DE VENTA
                    electronic.ele_nomadicional2 = "Email";

                    electronic.ele_adicional3 = com.ccomdoc.cdoc_telefono;//ENVIO LA POLITICA DE VENTA
                    electronic.ele_nomadicional3 = "Telefono";
                    
                    electronic.ele_formato = ele.formato;
                    electronic.ele_clave = GetClave(com, "07", empresa, electronic);

                    List<Electronicdet> detalle = new List<Electronicdet>();

                    int secuencia = 0;
                    foreach (Dretencion item in com.retenciones)
                    {
                        secuencia++;
                        if (item.drt_impuesto.HasValue && item.drt_valor.HasValue)
                        {
                            Electronicdet det = new Electronicdet();
                            det.eled_empresa = com.com_empresa;
                            det.eled_comprobante = com.com_codigo;
                            det.eled_secuencia = secuencia;


                            string codigoaux = "";

                            det.eled_codigo = GetCodigoRet(item, lstret, lstporcret, lstimpuestos, lstconceptos, out codigoaux).ToString();
                            det.eled_codigoaux = codigoaux;

                            det.eled_baseimp = item.drt_valor;
                            det.eled_porcret = item.drt_porcentaje;
                            det.eled_valorret = item.drt_total;
                            //det.eled_coddocsustento = "01";
                            det.eled_coddocsustento = string.IsNullOrEmpty(com.ccomdoc.cdoc_formapago) ? "01" : com.ccomdoc.cdoc_formapago;
                            det.eled_numdocsustento = GetNumDocSustento(obligacion.com_documento_obli);
                            det.eled_fechadocsustento = obligacion.com_fecha.ToString("dd/MM/yyyy");

                            det.crea_usr = item.crea_usr;
                            det.crea_fecha = DateTime.Now;
                            detalle.Add(det);
                        }

                    }


                    electronic.detalle = detalle;


                   


                    return electronic;

                }

                #endregion

                #region NOTA DE CRÉDITO

                if (ele.tipodoc == Constantes.cNotacre.tpd_codigo)//GUARDA TIPODOC PARA FACTURA
                {

                    Comprobante factura  = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = com.com_empresa, com_empresa_key = com.com_empresa, com_codigo = com.ccomdoc.cdoc_factura.Value, com_codigo_key = com.ccomdoc.cdoc_factura.Value });

                    Electronic electronic = new Electronic();


                    electronic.ele_empresa = com.com_empresa;
                    electronic.ele_comprobante = com.com_codigo;
                    electronic.ele_almacen = com.com_almacenid;
                    electronic.ele_pventa = com.com_pventaid;
                    electronic.ele_secuencia = com.com_numero.ToString("000000000");
                    electronic.ele_ambiente = ele.ambiente; // 1=PRUEBAS 2=PRODUCCION
                    electronic.ele_emision = 1; //    AQUI SIEMPRES SERIA 1=EMISION NORMAL, 2=EMISION POR INDISPONIBILIDAD NO SE MANEJARIA
                    electronic.ele_tipo = com.com_tipodoc;
                    electronic.ele_email = persona.per_mail;
                    electronic.ele_dirmatriz = matriz.alm_direccion;
                    electronic.ele_dirsucursal = sucursal.alm_direccion;
                    electronic.ele_especial = ele.especial;
                    electronic.ele_contabilidad = ele.contabilidad;
                    electronic.ele_tipoid = GetTipoId(persona.per_tipoid, persona.per_ciruc, lstids);

                    electronic.ele_totalsinimp = com.total.tot_subtotal + com.total.tot_subtot_0 + com.total.tot_transporte + (com.total.tot_tseguro ?? 0);
                    //electronic.ele_totaldesc = com.total.des
                    //electronic.ele_propina = 
                    electronic.ele_total = com.total.tot_total;


                    electronic.ele_coddocsustento = "01";
                    electronic.ele_numdocsustento = string.Format("{0:000}-{1:000}-{2:000000000}", factura.com_almacenid, factura.com_pventaid, factura.com_numero);//  factura.al .com_doctran.Replace("FAC-", "");
                    electronic.ele_fechadocsustento = factura.com_fecha.ToString("dd/MM/yyyy");


                    //IMPUESTOS

                    //IVA
                    electronic.ele_iva = com.total.tot_subtotal + (com.total.tot_tseguro ?? 0);
                    electronic.ele_porciva = com.total.tot_porc_impuesto;
                    electronic.ele_codigoiva = GetCodigoImp(electronic.ele_porciva, lstimp);
                    electronic.ele_valoriva = com.total.tot_timpuesto;

                    //IVA 0
                    electronic.ele_iva0 = com.total.tot_subtot_0 + com.total.tot_transporte;
                    electronic.ele_codigoiva0 = GetCodigoImp(0, lstimp);

                    //ICE


                    electronic.ele_ice = com.total.tot_ice;
                    //FALTA LOS PORCENTAJES Y CODIGOS

                    //ele_guiaremision = com.ccomenv.cenv_guia1

                    electronic.ele_adicional1 = com.ccomdoc.cdoc_politicanombre;//ENVIO LA POLITICA DE VENTA
                    electronic.ele_nomadicional1 = "Fpago";
                    electronic.ele_adicional2 = com.ccomenv.cenv_ciruc_rem + " " + com.ccomenv.cenv_apellidos_rem + " " + com.ccomenv.cenv_nombres_rem;
                    electronic.ele_nomadicional2 = "Remitente";
                    electronic.ele_adicional3 = com.ccomenv.cenv_ciruc_des + " " + com.ccomenv.cenv_apellidos_des + " " + com.ccomenv.cenv_nombres_des;
                    electronic.ele_nomadicional3 = "Destinatario";
                    electronic.ele_adicional4 = com.ccomenv.cenv_rutadestino;
                    electronic.ele_nomadicional4 = "Ciudad";
                    electronic.ele_adicional5 = com.ccomdoc.cdoc_telefono;//ENVIO LA POLITICA DE VENTA
                    electronic.ele_nomadicional5 = "Telefono";
                    if ((com.total.tot_vseguro ?? 0) > 0)
                    {
                        electronic.ele_adicional6 = Functions.Formatos.CurrencyFormat(com.total.tot_vseguro);//ENVIO LA POLITICA DE VENTA
                        electronic.ele_nomadicional6 = "Valor declarado";
                    }







                    electronic.ele_formato = ele.formato;
                    electronic.ele_clave = GetClave(com, "04", empresa, electronic);

                    List<Electronicdet> detalle = new List<Electronicdet>();

                    int secuencia = 0;
                    foreach (Dnotacre item in com.notascre)
                    {
                        secuencia++;
                        Electronicdet det = new Electronicdet();
                        det.eled_empresa = com.com_empresa;
                        det.eled_comprobante = com.com_codigo;
                        det.eled_secuencia = secuencia;
                        //det.eled_producto = item..ddoc_producto.Value;
                        det.eled_codigo = item.dnc_tiponc.ToString();
                        det.eled_codigoaux = item.dnc_tiponcid; // NO SE ESTA USANDO CODIGO AUX
                        det.eled_descripcion = item.dnc_tiponcnombre;
                        det.eled_cantidad = 1;
                        det.eled_precio = item.dnc_valor;
                        det.eled_descuento = 0;
                        det.eled_totalsinimp = item.dnc_valor;
                        //det.eled_adicional1 = item.dn;

                        //IMPUESTOS
                        if (item.dnc_cheque.Value == 1)
                        {

                            //IVA
                            det.eled_porciva = com.total.tot_porc_impuesto;
                            det.eled_codigoiva = GetCodigoImp(det.eled_porciva, lstimp);                            
                            det.eled_iva = Math.Round((decimal)(det.eled_totalsinimp * (det.eled_porciva / 100)), 2); //item.ddoc_ivaitem;



                        }
                        else
                        {
                            //IVA 0
                            det.eled_iva0 = item.dnc_valor;
                            det.eled_codigoiva0 = GetCodigoImp(0, lstimp);
                        }


                        //if (item.ddoc_iceitem.HasValue)
                        //{
                        //    det.eled_ice = item.ddoc_iceitem;
                        //    //FALTA LOS PORCENTAJES Y CODIGOS
                        //}

                        det.crea_usr = item.crea_usr;
                        det.crea_fecha = DateTime.Now;
                        detalle.Add(det);

                    }


                    //if (com.total.tot_transporte > 0)
                    //{
                    //    secuencia++;
                    //    Electronicdet det = new Electronicdet();
                    //    det.eled_empresa = com.com_empresa;
                    //    det.eled_comprobante = com.com_codigo;
                    //    det.eled_secuencia = secuencia;
                    //    det.eled_producto = 999;
                    //    det.eled_codigo = "00009999";
                    //    det.eled_codigoaux = "00009999"; // NO SE ESTA USANDO CODIGO AUX
                    //    det.eled_descripcion = "TRANSPORTE A DOMICILIO";
                    //    det.eled_cantidad = 1;
                    //    det.eled_precio = com.total.tot_transporte;
                    //    det.eled_descuento = 0;
                    //    det.eled_totalsinimp = com.total.tot_transporte;
                    //    //det.eled_adicional1 = item.ddoc_observaciones;                        
                    //    det.eled_iva0 = com.total.tot_transporte;
                    //    det.eled_codigoiva0 = GetCodigoImp(0, lstimp);
                    //    detalle.Add(det);
                    //}

                    electronic.detalle = detalle;


                    //////FORMAS DE PAGO/////

                   

                    return electronic;

                }

                #endregion

                #region NOTA DE DÉBITO

                // Misma fuente de datos que Nota de Credito (tabla/BusinessObject Dnotacre, com.notascre), solo
                // diferenciada por com_tipodoc - Nota de Debito nunca tuvo plantilla XML propia para SICE, asi que
                // esta rama nunca se ejecuto antes de agregarla (2026-07-10, para habilitar el envio via Asapp).
                if (ele.tipodoc == Constantes.cNotadeb.tpd_codigo)
                {

                    Comprobante factura = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = com.com_empresa, com_empresa_key = com.com_empresa, com_codigo = com.ccomdoc.cdoc_factura.Value, com_codigo_key = com.ccomdoc.cdoc_factura.Value });

                    Electronic electronic = new Electronic();


                    electronic.ele_empresa = com.com_empresa;
                    electronic.ele_comprobante = com.com_codigo;
                    electronic.ele_almacen = com.com_almacenid;
                    electronic.ele_pventa = com.com_pventaid;
                    electronic.ele_secuencia = com.com_numero.ToString("000000000");
                    electronic.ele_ambiente = ele.ambiente; // 1=PRUEBAS 2=PRODUCCION
                    electronic.ele_emision = 1;
                    electronic.ele_tipo = com.com_tipodoc;
                    electronic.ele_email = persona.per_mail;
                    electronic.ele_dirmatriz = matriz.alm_direccion;
                    electronic.ele_dirsucursal = sucursal.alm_direccion;
                    electronic.ele_especial = ele.especial;
                    electronic.ele_contabilidad = ele.contabilidad;
                    electronic.ele_tipoid = GetTipoId(persona.per_tipoid, persona.per_ciruc, lstids);

                    electronic.ele_totalsinimp = com.total.tot_subtotal + com.total.tot_subtot_0 + com.total.tot_transporte + (com.total.tot_tseguro ?? 0);
                    electronic.ele_total = com.total.tot_total;

                    electronic.ele_coddocsustento = "01";
                    electronic.ele_numdocsustento = string.Format("{0:000}-{1:000}-{2:000000000}", factura.com_almacenid, factura.com_pventaid, factura.com_numero);
                    electronic.ele_fechadocsustento = factura.com_fecha.ToString("dd/MM/yyyy");

                    //IMPUESTOS

                    //IVA
                    electronic.ele_iva = com.total.tot_subtotal + (com.total.tot_tseguro ?? 0);
                    electronic.ele_porciva = com.total.tot_porc_impuesto;
                    electronic.ele_codigoiva = GetCodigoImp(electronic.ele_porciva, lstimp);
                    electronic.ele_valoriva = com.total.tot_timpuesto;

                    //IVA 0
                    electronic.ele_iva0 = com.total.tot_subtot_0 + com.total.tot_transporte;
                    electronic.ele_codigoiva0 = GetCodigoImp(0, lstimp);

                    //ICE
                    electronic.ele_ice = com.total.tot_ice;

                    electronic.ele_adicional1 = com.ccomdoc.cdoc_politicanombre;
                    electronic.ele_nomadicional1 = "Fpago";
                    electronic.ele_adicional2 = com.ccomenv.cenv_ciruc_rem + " " + com.ccomenv.cenv_apellidos_rem + " " + com.ccomenv.cenv_nombres_rem;
                    electronic.ele_nomadicional2 = "Remitente";
                    electronic.ele_adicional3 = com.ccomenv.cenv_ciruc_des + " " + com.ccomenv.cenv_apellidos_des + " " + com.ccomenv.cenv_nombres_des;
                    electronic.ele_nomadicional3 = "Destinatario";
                    electronic.ele_adicional4 = com.ccomenv.cenv_rutadestino;
                    electronic.ele_nomadicional4 = "Ciudad";
                    electronic.ele_adicional5 = com.ccomdoc.cdoc_telefono;
                    electronic.ele_nomadicional5 = "Telefono";
                    if ((com.total.tot_vseguro ?? 0) > 0)
                    {
                        electronic.ele_adicional6 = Functions.Formatos.CurrencyFormat(com.total.tot_vseguro);
                        electronic.ele_nomadicional6 = "Valor declarado";
                    }

                    electronic.ele_formato = ele.formato;
                    // Codigo SRI 05 = Nota de Debito (ver GetClave, contempla 01/04/05/06/07)
                    electronic.ele_clave = GetClave(com, "05", empresa, electronic);

                    List<Electronicdet> detalle = new List<Electronicdet>();

                    int secuencia = 0;
                    foreach (Dnotacre item in com.notascre)
                    {
                        secuencia++;
                        Electronicdet det = new Electronicdet();
                        det.eled_empresa = com.com_empresa;
                        det.eled_comprobante = com.com_codigo;
                        det.eled_secuencia = secuencia;
                        det.eled_codigo = item.dnc_tiponc.ToString();
                        det.eled_codigoaux = item.dnc_tiponcid;
                        det.eled_descripcion = item.dnc_tiponcnombre;
                        det.eled_cantidad = 1;
                        det.eled_precio = item.dnc_valor;
                        det.eled_descuento = 0;
                        det.eled_totalsinimp = item.dnc_valor;

                        //IMPUESTOS
                        if (item.dnc_cheque.Value == 1)
                        {
                            //IVA
                            det.eled_porciva = com.total.tot_porc_impuesto;
                            det.eled_codigoiva = GetCodigoImp(det.eled_porciva, lstimp);
                            det.eled_iva = Math.Round((decimal)(det.eled_totalsinimp * (det.eled_porciva / 100)), 2);
                        }
                        else
                        {
                            //IVA 0
                            det.eled_iva0 = item.dnc_valor;
                            det.eled_codigoiva0 = GetCodigoImp(0, lstimp);
                        }

                        det.crea_usr = item.crea_usr;
                        det.crea_fecha = DateTime.Now;
                        detalle.Add(det);
                    }

                    electronic.detalle = detalle;

                    return electronic;

                }

                #endregion


            }

            return null;
        }


        public static string GetElectronicoData(Comprobante com)
        {
            if (!string.IsNullOrEmpty(com.com_claveelec))
            {
                SICE.Metodos ws = new SICE.Metodos();

                string data = ws.GetComprobanteData(com.com_claveelec);

                return data;
            }
            return "";


        }

        delegate Comprobante DelegadoUpdateElectronicoData(Comprobante comprobante);

        public static void UpdateElectronicoDataAsync(Comprobante comprobante)
        {
            DelegadoUpdateElectronicoData delegado = new DelegadoUpdateElectronicoData(UpdateElectronicoData);
            IAsyncResult result = delegado.BeginInvoke(comprobante, null, null);
        }


        public static Comprobante UpdateElectronicoData(Comprobante com)
        {
            if (ResolveProvider(com) == "ASAPP")
                return ElectronicoAsapp.UpdateElectronicoData(com);
            return UpdateElectronicoDataSICE(com);
        }

        // Integracion legacy SICE (SOAP) - logica intacta, solo renombrada.
        public static Comprobante UpdateElectronicoDataSICE(Comprobante com)
        {
            string data = GetElectronicoData(com);

            if (!string.IsNullOrEmpty(data))
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(data);

                string clave = xml.SelectSingleNode("/data/clave").InnerText;
                string estado = xml.SelectSingleNode("/data/estado").InnerText.ToUpper();                
                string mensaje = xml.SelectSingleNode("/data/mensaje").InnerText;



                com.com_empresa = com.com_empresa;
                com.com_empresa_key = com.com_empresa;
                com.com_codigo = com.com_codigo;
                com.com_codigo_key = com.com_codigo;

                com = ComprobanteBLL.GetByPK(com);
                com.com_empresa_key = com.com_empresa;
                com.com_codigo_key = com.com_codigo;
                com.com_estadoelec = estado;
                com.com_mensajeelec = mensaje;
                ComprobanteBLL.Update(com);

            }
            return com;
        }


        public static string CuadreElectronico(int empresa,DateTime desde, DateTime hasta)
        {

            WhereParams where = new WhereParams();
            List<object> valores = new List<object>();
            where.where = "com_empresa=" + empresa + " and com_estado=2 and com_tipodoc=4";
            where.where += " and com_fecha between {0} and {1}";
            valores.Add(desde);
            valores.Add(hasta);

            where.valores = valores.ToArray();

            List<Comprobante> comprobantes = ComprobanteBLL.GetAll(where, "");

            StringBuilder mensaje = new StringBuilder();
            foreach (var item in comprobantes)
            {

                string data = GetElectronicoData(item);



            }
            return "";


        }
    


        public static bool GenerateElectronico(Comprobante com)
        {
            if (!IsElectronic(com))
                return false;

            // Envio "sombra" (opt-in via parametro "AsappConfig", campos enable+enable_shadow) - SICE sigue siendo
            // el proveedor real sin ningun cambio; esto solo dispara en paralelo, de forma asincrona y sin
            // bloquear, una copia del comprobante hacia el ambiente configurado en AsappConfig para validar su
            // comportamiento con datos reales. No toca el comprobante real, solo deja registro en log_asapp.
            // Cubre los 6 tipos soportados por Asapp - se dispara ANTES de cualquier logica de proveedor/SICE
            // (incluido el guard de abajo que bloquea SICE para ND/LC/GR) para que tambien aplique a esos 3 tipos.
            // Ver ElectronicoAsapp.ShadowSendComprobanteAsync (ahi vive tambien el guard de dominio de staging).
            bool tipoSoportadoAsapp = com.com_tipodoc == Constantes.cFactura.tpd_codigo
                || com.com_tipodoc == Constantes.cNotacre.tpd_codigo
                || com.com_tipodoc == Constantes.cNotadeb.tpd_codigo
                || com.com_tipodoc == Constantes.cRetencion.tpd_codigo
                || com.com_tipodoc == Constantes.cLiquidacionCompra.tpd_codigo
                || com.com_tipodoc == Constantes.cGuiaRemision.tpd_codigo;
            if (tipoSoportadoAsapp)
            {
                AsappConfig shadowConfig = Constantes.cAsappConfig;
                if (shadowConfig != null && shadowConfig.enable && shadowConfig.enable_shadow)
                    ElectronicoAsapp.ShadowSendComprobanteAsync(com);
            }

            if (ResolveProvider(com) == "ASAPP")
                return ElectronicoAsapp.GenerateElectronico(com);

            // Nota de Debito y Liquidacion de Compra ahora tienen rama en LoadElectronico (agregadas 2026-07-10,
            // solo para habilitar Asapp), pero SICE nunca tuvo plantilla XML para ninguna de las tres (ni tampoco
            // para Guia de Remision) - GenerateElectronicoSICE no tiene GenerarND()/GenerarLC()/GenerarGR(). Sin
            // este guard, configurar alguno de estos tres sin "provider":"ASAPP" haria que GenerateElectronicoSICE
            // mande un XML vacio a la produccion real de SICE. Los tres SOLO se soportan via Asapp.
            if (com.com_tipodoc == Constantes.cNotadeb.tpd_codigo || com.com_tipodoc == Constantes.cLiquidacionCompra.tpd_codigo || com.com_tipodoc == Constantes.cGuiaRemision.tpd_codigo)
            {
                ExceptionHandling.Log.AddExepcion(new Exception("Comprobante " + com.com_codigo + " (tipodoc=" + com.com_tipodoc + ") solo esta soportado via Asapp - configurar \"provider\":\"ASAPP\" en el parametro 'electronicos' para este tipodoc."));
                return false;
            }

            return new Electronico().GenerateElectronicoSICE(com);
        }

        // Mapeo al codigo de tipo de documento SRI (2 digitos) usado en la clave de acceso - mismo mapeo que ya
        // usa LoadElectronico internamente al llamar GetClave para cada rama (01/03/04/05/07), ver esos 5 call
        // sites. Devuelve null para tipos no soportados por Asapp via este payload generico (ej. Guia de Remision,
        // que tiene su propio flujo en ElectronicoAsapp.GenerateElectronicoGuiaRemision).
        private static string GetTipoDocSri(int tipodoc)
        {
            if (tipodoc == Constantes.cFactura.tpd_codigo) return "01";
            if (tipodoc == Constantes.cLiquidacionCompra.tpd_codigo) return "03";
            if (tipodoc == Constantes.cNotacre.tpd_codigo) return "04";
            if (tipodoc == Constantes.cNotadeb.tpd_codigo) return "05";
            if (tipodoc == Constantes.cRetencion.tpd_codigo) return "07";
            return null;
        }

        // Calcula la clave de acceso ANTES de enviar nada a Asapp y la guarda de una vez (UPDATE rapido, sin
        // esperar respuesta de red) - asi la impresion (wfComprobantePrint.aspx.cs) puede leer com_claveelec
        // apenas se guarda el comprobante, sin depender de que el envio en segundo piano (GenerateElectronicoAsync)
        // haya terminado. Reutiliza GetClave/GetCheckDigit, el mismo algoritmo ya usado por el flujo legacy SICE
        // (validado contra 40 claves reales ya autorizadas antes de habilitar esto, ver memoria del proyecto).
        //
        // Idempotente a proposito: si com_claveelec ya tiene valor (reintento de un comprobante ya precalculado,
        // o uno recuperado via TryRecuperarClaveDeDuplicado), no se recalcula ni se vuelve a escribir - la clave
        // y los datos que dependen de ella (fecha/ambiente/secuencial) deben quedar fijos desde el primer calculo,
        // Asapp rechaza (CLAVE_ACCESO_INVALIDA) cualquier payload cuya clave no coincida exactamente con esos datos.
        private static void PrecalcularClaveAcceso(Comprobante com)
        {
            try
            {
                Comprobante comp = new Comprobante { com_empresa = com.com_empresa, com_empresa_key = com.com_empresa, com_codigo = com.com_codigo, com_codigo_key = com.com_codigo };
                comp = ComprobanteBLL.GetByPK(comp);

                if (!string.IsNullOrEmpty(comp.com_claveelec))
                    return; // ya calculada (reintento) - nunca recalcular

                string tipoDocSri = GetTipoDocSri(comp.com_tipodoc);
                if (tipoDocSri == null)
                    return; // tipo no soportado por este payload (ej. Guia de Remision) - sigue su propio camino

                Electronicos config = GetElectronicoConfig(comp);
                if (config == null)
                    return; // sin configuracion para este empresa+tipodoc - GenerateElectronico ya lo va a marcar NOCONFIG

                Empresa empresa = EmpresaBLL.GetByPK(new Empresa { emp_codigo = comp.com_empresa, emp_codigo_key = comp.com_empresa });

                Electronic ele = new Electronic
                {
                    ele_almacen = comp.com_almacenid,
                    ele_pventa = comp.com_pventaid,
                    ele_secuencia = comp.com_numero.ToString("000000000"),
                    ele_ambiente = config.ambiente,
                    ele_emision = 1
                };

                comp.com_claveelec = GetClave(comp, tipoDocSri, empresa, ele);
                comp.com_ambiente = config.ambiente.ToString();
                comp.com_emision = "1";
                comp.com_empresa_key = comp.com_empresa;
                comp.com_codigo_key = comp.com_codigo;
                ComprobanteBLL.Update(comp);
            }
            catch (Exception ex)
            {
                // Nunca debe tumbar el guardado/envio por esto - si falla, GenerateElectronico sigue su camino
                // normal (Asapp genera su propia clave, como antes de este cambio).
                ExceptionHandling.Log.AddExepcion(ex);
            }
        }

        // Dispara el envio en segundo plano (no bloquea el request de guardado de Factura/Guia). El gatillo
        // IsElectronic/ResolveProvider es sincrono y sin red (solo lee el parametro "electronicos"), asi que se
        // evalua aca mismo antes de encolar nada - evita que un comprobante que ni siquiera aplica a electronico
        // quede "esperando" del lado del navegador. Solo se encola para Asapp: GenerateElectronicoSICE depende de
        // HttpContext.Current (Server.MapPath), que no esta disponible en un QueueBackgroundWorkItem, asi que SICE
        // conserva el comportamiento sincrono de siempre si algun dia se revierte el proveedor.
        // Ver [[reference_async_envio_electronico]] en memoria: desde que Asapp soporta recibir la clave de acceso
        // ya calculada (claveAcceso en el payload, confirmado en PROD 06-nov-2026), PrecalcularClaveAcceso corre
        // SINCRONO aca mismo (antes de encolar el envio real) - la impresion ya no espera nada de Asapp para tener
        // la clave, solo el calculo local (~2ms medido en pruebas de estres contra staging).
        public static void GenerateElectronicoAsync(Comprobante com)
        {
            if (!IsElectronic(com))
                return;

            if (ResolveProvider(com) != "ASAPP")
            {
                GenerateElectronico(com);
                return;
            }

            PrecalcularClaveAcceso(com);

            // BUG REAL encontrado en produccion 30-oct-2026 (CL): Web.config fija <globalization culture="es-EC" />,
            // pero eso solo aplica al hilo de la peticion ASP.NET - un hilo nuevo de QueueBackgroundWorkItem toma
            // la cultura POR DEFECTO DEL SERVIDOR, no "es-EC". GenerateElectronico (via ResolveProvider/
            // GetElectronicoConfig) parsea el parametro "electronicos" con JavaScriptSerializer, que tiene fechas
            // como texto plano (ej. "desde":"2018-03-22") - en la cultura del servidor eso tiro
            // "String was not recognized as a valid DateTime" y ningun envio llegaba a completarse (com_estadoelec
            // se quedaba vacio en TODOS los comprobantes nuevos). Fix: fijar la cultura explicitamente adentro del
            // hilo de fondo, igual a la que ya tenia el hilo de la peticion.
            System.Web.Hosting.HostingEnvironment.QueueBackgroundWorkItem(ct =>
            {
                System.Globalization.CultureInfo cultura = new System.Globalization.CultureInfo("es-EC");
                System.Threading.Thread.CurrentThread.CurrentCulture = cultura;
                System.Threading.Thread.CurrentThread.CurrentUICulture = cultura;
                GenerateElectronico(com);
            });
        }

        // Interruptor del log de diagnostico GUARDADO_TOTAL/IMPRESION (no afecta el log "ENVIO" de Asapp, que
        // es permanente). Pensado para prender/apagar sin otro deploy: parametro "DiagnosticoGuardado" en la
        // tabla parametro, valor "0" lo apaga. Si el parametro no existe, queda HABILITADO por default (asi
        // funciona apenas se despliega esto, sin un paso extra de configuracion). Cuando se decida que ya se
        // midio lo suficiente, insertar/actualizar el parametro a "0" alcanza para apagarlo sin tocar codigo.
        private static bool DiagnosticoGuardadoHabilitado()
        {
            string valor = Constantes.GetParameter("DiagnosticoGuardado");
            return valor != "0";
        }

        // Mide el tiempo TOTAL de guardado (SaveObject completo, no solo el envio a Asapp) y lo deja en la
        // misma tabla log_asapp (log_operacion="GUARDADO_TOTAL") para poder comparar antes/despues del cambio
        // a GenerateElectronicoAsync con la misma herramienta que ya usamos para medir Asapp - evita crear una
        // tabla nueva solo para esto. No tiene nada que ver con Asapp en si (corre para cualquier guardado,
        // electronico o no), solo reusa el mismo mecanismo de logging ya probado.
        // La duracion se calcula ACA (sincrono, instantaneo - solo resta dos DateTime), pero el INSERT real a
        // la BD (incluido el chequeo del interruptor) se manda en segundo plano: la idea es medir el guardado
        // sin que la propia medicion le agregue tiempo al guardado que se esta midiendo.
        // Para ver el tiempo de Asapp junto a este: JOIN contra log_asapp log_operacion='ENVIO' por
        // log_comprobante (ya existe, no hace falta duplicarlo aca).
        public static void LogDuracionGuardado(Comprobante com, DateTime inicio)
        {
            int duracionMs = (int)(DateTime.Now - inicio).TotalMilliseconds;
            int empresa = com.com_empresa;
            long codigo = com.com_codigo;
            System.Web.Hosting.HostingEnvironment.QueueBackgroundWorkItem(ct =>
            {
                try
                {
                    if (!DiagnosticoGuardadoHabilitado())
                        return;

                    LogAsapp log = new LogAsapp();
                    log.log_empresa = empresa;
                    log.log_comprobante = codigo;
                    log.log_operacion = "GUARDADO_TOTAL";
                    log.log_exitoso = 1;
                    log.log_duracionms = duracionMs;
                    log.crea_fecha = DateTime.Now;
                    LogAsappBLL.Insert(log);
                }
                catch (Exception ex)
                {
                    ExceptionHandling.Log.AddExepcion(ex);
                }
            });
        }

        // Registra, al momento de imprimir (wfComprobantePrint.aspx), si el comprobante YA tenia la clave
        // electronica puesta o no - para saber que tan seguido el envio en segundo plano (GenerateElectronicoAsync)
        // alcanza a terminar antes de que el usuario imprima. com_estado se usa como el booleano "tenia clave"
        // (1=si, 0=no) reusando el campo existente de la tabla, igual que log_exitoso en los demas logs de
        // log_asapp. log_duracionms aca es el tiempo transcurrido desde que se creo/modifico el comprobante
        // hasta el momento de imprimir (proxy de "cuanto tardo en llegar a la pantalla de impresion"), no el
        // tiempo de Asapp (eso ya esta en log_operacion='ENVIO', se compara por log_comprobante).
        public static void LogEstadoAlImprimir(Comprobante com)
        {
            if (com == null)
                return;
            try
            {
                if (!DiagnosticoGuardadoHabilitado())
                    return;

                bool teniaClave = !string.IsNullOrEmpty(com.com_claveelec);
                DateTime? referencia = com.mod_fecha.HasValue && com.mod_fecha.Value != default(DateTime) ? com.mod_fecha : com.crea_fecha;
                int msDesdeGuardado = (referencia.HasValue && referencia.Value != default(DateTime)) ? (int)(DateTime.Now - referencia.Value).TotalMilliseconds : 0;

                int empresa = com.com_empresa;
                long codigo = com.com_codigo;
                System.Web.Hosting.HostingEnvironment.QueueBackgroundWorkItem(ct =>
                {
                    try
                    {
                        LogAsapp log = new LogAsapp();
                        log.log_empresa = empresa;
                        log.log_comprobante = codigo;
                        log.log_operacion = "IMPRESION";
                        log.log_exitoso = teniaClave ? 1 : 0;
                        log.log_duracionms = msDesdeGuardado;
                        log.log_mensaje = teniaClave ? "CON CLAVE AL IMPRIMIR" : "SIN CLAVE AL IMPRIMIR";
                        log.crea_fecha = DateTime.Now;
                        LogAsappBLL.Insert(log);
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandling.Log.AddExepcion(ex);
                    }
                });
            }
            catch (Exception ex)
            {
                ExceptionHandling.Log.AddExepcion(ex);
            }
        }

        // Integracion legacy SICE (SOAP) - logica intacta, solo renombrada. No modificar sin coordinar rollback del switch "provider".
        // Ya no es static: cada llamada crea su propia instancia de Electronico (ver GenerateElectronico arriba),
        // asi que los campos de instancia de abajo (empresa/comprobante/ccomdoc/electronic/etc.) quedan aislados
        // por request y no se pisan entre dos comprobantes procesandose al mismo tiempo.
        public bool GenerateElectronicoSICE(Comprobante com)
        {


            if (IsElectronic(com))
            {
                if (com.com_estado == Constantes.cEstadoMayorizado)//Nuevo control para emitir electronicos solo cuando esta mayorizado
                {
                    try

                    {

                        path = HttpContext.Current.Server.MapPath("~");

                        empresa.emp_codigo = com.com_empresa;
                        empresa.emp_codigo_key = com.com_empresa;
                        empresa = EmpresaBLL.GetByPK(empresa);

                        empresa.emp_agenteretxml = Constantes.GetParameter("agenteretxml");
                        empresa.emp_regimenmicro = Constantes.GetParameter("regimenmicroxml");
                        empresa.emp_regimenrimpe = Constantes.GetParameter("regimenrimpe");

                        comprobante.com_empresa = com.com_empresa;
                        comprobante.com_empresa_key = com.com_empresa;
                        comprobante.com_codigo = com.com_codigo;
                        comprobante.com_codigo_key = com.com_codigo;

                        comprobante = ComprobanteBLL.GetByPK(comprobante);
                        comprobante.com_empresa_key = com.com_empresa;
                        comprobante.com_codigo_key = com.com_codigo;

                        comprobante.ccomdoc = new Ccomdoc();
                        comprobante.ccomdoc.cdoc_empresa = comprobante.com_empresa;
                        comprobante.ccomdoc.cdoc_empresa_key = comprobante.com_empresa;
                        comprobante.ccomdoc.cdoc_comprobante = comprobante.com_codigo;
                        comprobante.ccomdoc.cdoc_comprobante_key = comprobante.com_codigo;
                        comprobante.ccomdoc = CcomdocBLL.GetByPK(comprobante.ccomdoc);

                        if (string.IsNullOrEmpty(comprobante.ccomdoc.cdoc_direccion))
                            comprobante.ccomdoc.cdoc_direccion = "S/D";

                        comprobante.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa={0} and ddoc_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "ddoc_secuencia");


                        comprobante.ccomenv = new Ccomenv();
                        comprobante.ccomenv.cenv_empresa = comprobante.com_empresa;
                        comprobante.ccomenv.cenv_empresa_key = comprobante.com_empresa;
                        comprobante.ccomenv.cenv_comprobante = comprobante.com_codigo;
                        comprobante.ccomenv.cenv_comprobante_key = comprobante.com_codigo;
                        comprobante.ccomenv = CcomenvBLL.GetByPK(comprobante.ccomenv);

                        comprobante.total = new Total();
                        comprobante.total.tot_empresa = comprobante.com_empresa;
                        comprobante.total.tot_empresa_key = comprobante.com_empresa;
                        comprobante.total.tot_comprobante = comprobante.com_codigo;
                        comprobante.total.tot_comprobante_key = comprobante.com_codigo;
                        comprobante.total = TotalBLL.GetByPK(comprobante.total);


                        comprobante.retenciones = DretencionBLL.GetAll(new WhereParams("drt_empresa={0} and drt_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "drt_secuencia");
                        comprobante.notascre = DnotacreBLL.GetAll(new WhereParams("dnc_empresa={0} and dnc_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "dnc_secuencia");


                        List<Drecibo> detalle = DreciboBLL.GetAll(new WhereParams("dfp_empresa={0} and dfp_ref_comprobante={1} and com_estado=2", comprobante.com_empresa, comprobante.com_codigo), "dfp_secuencia");

                        ccomdoc = comprobante.ccomdoc;

                        electronic = LoadElectronico(comprobante, detalle);

                        if (electronic != null)
                        {
                            comprobante.com_emision = electronic.ele_emision.ToString();
                            comprobante.com_ambiente = electronic.ele_ambiente.ToString();
                            comprobante.com_claveelec = electronic.ele_clave;
                            ComprobanteBLL.Update(comprobante);

                            electronicdet = electronic.detalle;

                            formas = electronic.formas;



                            string xml = "";
                            if (comprobante.com_tipodoc == Constantes.cFactura.tpd_codigo)
                            {
                                XmlDocument xmldoc = GenerarFAC();
                                xml = xmldoc.ToString();
                                using (var stringWriter = new StringWriter())
                                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                                {
                                    xmldoc.WriteTo(xmlTextWriter);
                                    xmlTextWriter.Flush();
                                    xml = stringWriter.GetStringBuilder().ToString();
                                }
                            }


                            if (comprobante.com_tipodoc == Constantes.cRetencion.tpd_codigo)
                            {
                                XmlDocument xmldoc = GenerarRET();
                                xml = xmldoc.ToString();
                                using (var stringWriter = new StringWriter())
                                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                                {
                                    xmldoc.WriteTo(xmlTextWriter);
                                    xmlTextWriter.Flush();
                                    xml = stringWriter.GetStringBuilder().ToString();
                                }
                            }

                            if (comprobante.com_tipodoc == Constantes.cNotacre.tpd_codigo)
                            {
                                XmlDocument xmldoc = GenerarNC();
                                xml = xmldoc.ToString();
                                using (var stringWriter = new StringWriter())
                                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                                {
                                    xmldoc.WriteTo(xmlTextWriter);
                                    xmlTextWriter.Flush();
                                    xml = stringWriter.GetStringBuilder().ToString();
                                }
                            }




                            SICE.Metodos ws = new SICE.Metodos();
                            string ret = ws.RecibirComprobante(xml, electronic.ele_email, electronic.ele_formato);


                            return true;

                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandling.Log.AddExepcion(ex);
                        return false;
                    }
                }
            }
            return false;
        }


        public XmlDocument GenerarFAC()
        {

            xmldoc = new XmlDocument();



            XmlDocument xmltemp = new XmlDocument();
            xmltemp.Load(path + "\\xml\\fac\\factura.xml");


            foreach (XmlNode nod in xmltemp.ChildNodes)
            {
                CreateXmlNode(null, nod, null, electronic);
            }





            return xmldoc;

        }

        public XmlDocument GenerarRET()
        {

            xmldoc = new XmlDocument();



            XmlDocument xmltemp = new XmlDocument();
            xmltemp.Load(path + "\\xml\\ret\\retencion.xml");


            foreach (XmlNode nod in xmltemp.ChildNodes)
            {
                CreateXmlNode(null, nod, null, electronic);
            }





            return xmldoc;

        }


        public XmlDocument GenerarNC()
        {

            xmldoc = new XmlDocument();



            XmlDocument xmltemp = new XmlDocument();
            xmltemp.Load(path + "\\xml\\nc\\notacredito.xml");


            foreach (XmlNode nod in xmltemp.ChildNodes)
            {
                CreateXmlNode(null, nod, null, electronic);
            }





            return xmldoc;

        }

        public void CreateXmlNode(XmlNode parent, XmlNode nodtemp, Int64? codigo, object datasource)
        {


            switch (nodtemp.NodeType)
            {
                case XmlNodeType.XmlDeclaration:
                    XmlNode xmldec = xmldoc.CreateXmlDeclaration(((XmlDeclaration)nodtemp).Version, ((XmlDeclaration)nodtemp).Encoding, ((XmlDeclaration)nodtemp).Standalone);
                    xmldoc.AppendChild(xmldec);
                    break;
                case XmlNodeType.Text:
                    XmlNode xmltex = xmldoc.CreateTextNode(nodtemp.InnerText);
                    if (parent == null)
                        xmldoc.AppendChild(xmltex);
                    else
                        parent.AppendChild(xmltex);
                    break;
                default:

                    bool add = true;
                    XmlNode xmlnode = xmldoc.CreateElement(nodtemp.Name);

                    string source = "";
                    string function = "";
                    string filter = "";
                    string empty = "";
                    string valor = "";
                    string nombre = "";

                    foreach (XmlAttribute attribute in nodtemp.Attributes)
                    {
                        if (attribute.Name == "source")
                            source = attribute.Value;
                        else if (attribute.Name == "nombresource")
                            nombre = attribute.Value;
                        else if (attribute.Name == "valor")
                            valor = attribute.Value;
                        else if (attribute.Name == "function")
                            function = attribute.Value;
                        else if (attribute.Name == "filter")
                            filter = attribute.Value;
                        else if (attribute.Name == "empty")
                            empty = attribute.Value;

                        else
                        {
                            XmlAttribute attr = xmldoc.CreateAttribute(attribute.Name);
                            attr.Value = attribute.Value;
                            xmlnode.Attributes.Append(attr);
                        }
                    }
                    if (source != "")
                    {
                        xmlnode.InnerText = GetSourceValue(source, function, filter, datasource);
                        // IsNullOrWhiteSpace (no IsNullOrEmpty): concatenaciones tipo "a + ' ' + b + ' ' + c" con
                        // todas las partes vacias/null dan " " (no ""), lo cual el SRI rechaza igual por no cumplir
                        // minLength=1 real - hay que tratarlas como vacias para que "empty=no" las omita.
                        if (empty == "no" && string.IsNullOrWhiteSpace(xmlnode.InnerText))
                            add = false;
                    }
                    if (nombre!="")
                    {
                        string strvalor = GetSourceValue(nombre, function, filter, datasource);
                        XmlAttribute attr = xmldoc.CreateAttribute("nombre");
                        attr.Value = strvalor;
                        xmlnode.Attributes.Append(attr);
                        if (empty == "no" && string.IsNullOrWhiteSpace(strvalor))
                            add = false;
                    }

                    if (valor != "")
                    {
                        string strvalor = GetSourceValue(valor, function, filter, datasource);

                        XmlAttribute attr = xmldoc.CreateAttribute("valor");
                        attr.Value = strvalor;
                        xmlnode.Attributes.Append(attr);
                        if (empty == "no" && string.IsNullOrWhiteSpace(strvalor))
                            add = false;
                    }



                    //if (nodtemp.Attributes["source"] != null)
                    //    {
                    //        string source = (nodtemp.Attributes.Count > 0) ? nodtemp.Attributes[0].Value : "";
                    //        string function = (nodtemp.Attributes.Count > 1) ? nodtemp.Attributes[1].Value : "";
                    //        string filter = (nodtemp.Attributes.Count > 2) ? nodtemp.Attributes[2].Value : "";

                    //        string empty = (nodtemp.Attributes["empty"] != null) ? nodtemp.Attributes["empty"].Value : "";


                    //    xmlnode.InnerText = GetSourceValue(source, function, filter, datasource);
                    //    if (empty == "no" && string.IsNullOrEmpty(xmlnode.InnerText))
                    //        add = false;
                    //}


                    foreach (XmlNode nod in nodtemp.ChildNodes)
                    {
                        CreateXmlNode(xmlnode, nod, codigo, datasource);
                    }

                    if (nodtemp.Name == "totalConImpuestos")
                        GetTotal(xmlnode);


                    if (nodtemp.Name == "pagos")
                        GetPagos(xmlnode);
                    if (nodtemp.Name == "detalles")
                    {
                        Electronic elec = (Electronic)datasource;
                        if (elec.ele_tipo == 17)
                            GetDetallesNC(xmlnode);
                        else
                            GetDetalles(xmlnode);
                    }
                    if (nodtemp.Name == "impuestos")
                        GetImpuestos(xmlnode, datasource);
                    if (nodtemp.Name == "detallesAdicionales")
                        add = DetallesAdicionales(datasource);
                    /*if (nodtemp.Name == "anulados")
                        GetAnulados(xmlnode);
                    if (nodtemp.Name == "formasDePago")
                    {

                        add = GetFormasPago(xmlnode, datasource);
                    }*/

                    // Contenedor opcional (empty="no" en si mismo, sin source/nombre/valor propios, ej.
                    // <infoAdicional empty="no">) que termino sin ningun hijo real tras la poda individual
                    // de sus hijos - omitirlo entero. Sin esto, un contenedor sin caso especial en este switch
                    // (a diferencia de detallesAdicionales, que si lo tiene via DetallesAdicionales()) siempre
                    // se agrega (add=true por defecto, linea ~1479) aunque termine vacio, violando el XSD del
                    // SRI (si el contenedor esta presente, exige >=1 hijo) - mismo bug que detallesAdicionales,
                    // version generica para cubrir cualquier contenedor de este tipo sin repetir la funcion.
                    if (add && empty == "no" && source == "" && nombre == "" && valor == "" && !xmlnode.HasChildNodes)
                        add = false;

                    if (add)
                    {
                        if (parent == null)
                            xmldoc.AppendChild(xmlnode);
                        else
                            parent.AppendChild(xmlnode);
                    }
                    break;
            }

        }

        public string GetSourceValue(string source, string function, string filter, object datasource)
        {
            string valor = "";
            string[] arraysource = source.Split('.');
            if (arraysource.Length > 1)
            {
                string table = arraysource[0];
                string field = arraysource[1];
                return GetData(table, field, function, filter, datasource);

            }
            return valor;

        }

        public static string GetValueByType(object valor)
        {
            if (valor != null)
            {
                Type tipo = valor.GetType();
                if (tipo == typeof(DateTime))
                {
                    //return ((DateTime)valor).ToShortDateString();
                    return ((DateTime)valor).ToString("dd/MM/yyyy");

                }
                if (tipo == typeof(decimal) || tipo == typeof(float))
                {
                    //return ((decimal)valor).ToString("0.00").Replace(",", ".");
                    return ((decimal)valor).ToString().Replace(",", ".");

                }
                return valor.ToString();
            }
            else
                return "";


        }

        public string GetData(string table, string field, string function, string filter, object datasource)
        {

            switch (table.ToUpper())
            {
                case "ELECTRONICO":
                    foreach (PropertyInfo property in ElectronicProps)
                    {
                        if (property.Name == field)
                        {
                            return GetValueByType(property.GetValue(datasource, null));
                        }

                    }

                    break;
                case "EMPRESA":
                    foreach (PropertyInfo property in EmpresaProps)
                    {
                        if (property.Name == field)
                        {
                            return GetValueByType(property.GetValue(empresa, null));
                        }

                    }

                    break;
                case "COMPROBANTE":
                    foreach (PropertyInfo property in ComprobanteProps)
                    {
                        if (property.Name == field)
                        {
                            return GetValueByType(property.GetValue(comprobante, null));
                        }

                    }

                    break;
                case "CCOMDOC":
                    foreach (PropertyInfo property in CcomdocProps)
                    {
                        if (property.Name == field)
                        {
                            return GetValueByType(property.GetValue(ccomdoc, null));
                        }

                    }

                    break;
                case "ELECTRONICODET":
                    foreach (PropertyInfo property in ElectronicdetProps)
                    {
                        if (property.Name == field)
                        {
                            return GetValueByType(property.GetValue(datasource, null));
                        }

                    }

                    break;
                case "PAGO":
                    foreach (PropertyInfo property in FormapagoProps)
                    {
                        if (property.Name == field)
                        {
                            return GetValueByType(property.GetValue(datasource, null));
                        }

                    }

                    break;
                    /* case "PARAMS":
                         if (field == "anio")
                             return periodo.ToString("0000");
                         if (field == "mes")
                             return mes.ToString("00");
                         break;
                     case "VENTAS":

                         if (function == "sum")
                         {
                             decimal valor = 0;
                             foreach (vVenta item in ventas)
                             {
                                 decimal? v = item.subtotal + item.subimpuesto + item.transporte + item.seguro;
                                 valor += (v.HasValue) ? v.Value : 0;
                             }
                             foreach (vVenta item in notascc)
                             {
                                 decimal? v = item.subtotal + item.subimpuesto + item.transporte + item.seguro;
                                 valor -= (v.HasValue) ? v.Value : 0;
                             }




                             return valor.ToString("0.00").Replace(",", ".");
                         }
                         else
                         {
                             foreach (PropertyInfo property in ventasprop)
                             {
                                 if (property.Name == field)
                                 {
                                     return GetValueByType(property.GetValue(datasource, null));
                                     //return property.GetValue(datasource, null).ToString();
                                 }

                             }
                         }
                         break;

                     case "COMPRAS":
                         foreach (PropertyInfo property in comprasprop)
                         {
                             if (property.Name == field)
                             {
                                 return GetValueByType(property.GetValue(datasource, null));
                                 //object retorno = property.GetValue(datasource, null);

                                 //return (retorno != null) ? retorno.ToString() : "";
                             }

                         }
                         break;
                     case "RETENCION":
                         foreach (PropertyInfo property in retencionesprop)
                         {
                             if (property.Name == field)
                             {
                                 return GetValueByType(property.GetValue(datasource, null));
                                 //object retorno = property.GetValue(datasource, null);

                                 //return (retorno != null) ? retorno.ToString() : "";
                             }

                         }
                         break;
                     case "ESTABLECIMIENTO":
                         foreach (PropertyInfo property in establecimientosprop)
                         {
                             if (property.Name == field)
                             {
                                 return GetValueByType(property.GetValue(datasource, null));
                                 //object retorno = property.GetValue(datasource, null);

                                 //return (retorno != null) ? retorno.ToString() : "";
                             }

                         }
                         break;
                     case "ANULADOS":

                         object valorret = ((DataRow)datasource)[field];
                         return (valorret != null) ? valorret.ToString() : "";
                         break;*/


            }
            return "";
        }


        public void GetTotal(XmlNode nodototal)
        {



            if (electronic.ele_iva.HasValue)
            {
                XmlDocument xmltotalimpiva = new XmlDocument();
                xmltotalimpiva.Load(path + "\\xml\\fac\\totalimpiva.xml");

                if (electronic.ele_iva.Value > 0)
                {
                    foreach (XmlNode nod in xmltotalimpiva.ChildNodes)
                    {
                        CreateXmlNode(nodototal, nod, null, electronic);
                    }
                }
            }

            if (electronic.ele_iva0.HasValue)
            {
                XmlDocument xmltotalimpcero = new XmlDocument();
                xmltotalimpcero.Load(path + "\\xml\\fac\\totalimpcero.xml");

                if (electronic.ele_iva0.Value > 0 || electronic.ele_total==0)
                {
                    foreach (XmlNode nod in xmltotalimpcero.ChildNodes)
                    {
                        CreateXmlNode(nodototal, nod, null, electronic);
                    }
                }
                
            }

            if (electronic.ele_ice.HasValue)
            {
                XmlDocument xmltotalimpice = new XmlDocument();
                xmltotalimpice.Load(path + "\\xml\\fac\\totalimpcero.xml");

                if (electronic.ele_ice.Value > 0)
                {
                    foreach (XmlNode nod in xmltotalimpice.ChildNodes)
                    {
                        CreateXmlNode(nodototal, nod, null, electronic);
                    }
                }
            }






        }

        public void GetDetalles(XmlNode nododetalles)
        {

            XmlDocument xmldetalles = new XmlDocument();
            xmldetalles.Load(path + "\\xml\\fac\\detalles.xml");
            foreach (Electronicdet item in electronicdet)
            {
                foreach (XmlNode nod in xmldetalles.ChildNodes)
                {
                    CreateXmlNode(nododetalles, nod, item.eled_secuencia, item);
                }

            }
        }

        public void GetDetallesNC(XmlNode nododetalles)
        {

            XmlDocument xmldetalles = new XmlDocument();
            xmldetalles.Load(path + "\\xml\\nc\\detalles.xml");
            foreach (Electronicdet item in electronicdet)
            {
                foreach (XmlNode nod in xmldetalles.ChildNodes)
                {
                    CreateXmlNode(nododetalles, nod, item.eled_secuencia, item);
                }

            }
        }


        public void GetImpuestos(XmlNode nododetalle, object datasource)
        {

            if (datasource.GetType() == typeof(Electronic))
            {

                XmlDocument xmldetalle = new XmlDocument();
                xmldetalle.Load(path + "\\xml\\ret\\impuestos.xml");


                foreach (Electronicdet item in electronicdet)
                {
                    foreach (XmlNode nod in xmldetalle.ChildNodes)
                    {
                        CreateXmlNode(nododetalle, nod, item.eled_secuencia, item);
                    }

                }

            }
            else
            {

                Electronicdet det = (Electronicdet)datasource;

                if (det.eled_iva.HasValue)
                {
                    XmlDocument xmlimpiva = new XmlDocument();
                    xmlimpiva.Load(path + "\\xml\\fac\\impiva.xml");

                    if (det.eled_iva.Value > 0)
                    {
                        foreach (XmlNode nod in xmlimpiva.ChildNodes)
                        {
                            CreateXmlNode(nododetalle, nod, null, det);
                        }
                    }
                }

                if (det.eled_ice.HasValue)
                {
                    XmlDocument xmlimpice = new XmlDocument();
                    xmlimpice.Load(path + "\\xml\\fac\\impice.xml");

                    if (det.eled_ice.Value > 0)
                    {
                        foreach (XmlNode nod in xmlimpice.ChildNodes)
                        {
                            CreateXmlNode(nododetalle, nod, null, det);
                        }
                    }
                }

                if (det.eled_iva0.HasValue)
                {
                    XmlDocument xmlimpcero = new XmlDocument();
                    xmlimpcero.Load(path + "\\xml\\fac\\impcero.xml");

                    if (det.eled_iva0.Value >= 0)
                    {
                        foreach (XmlNode nod in xmlimpcero.ChildNodes)
                        {
                            CreateXmlNode(nododetalle, nod, null, det);
                        }
                    }
                }
            }
            



        }

        public void GetPagos(XmlNode nodopagos)
        {

            XmlDocument xmldetalles = new XmlDocument();
            xmldetalles.Load(path + "\\xml\\fac\\pagos.xml");
            foreach (Formapago item in formas)
            {
                foreach (XmlNode nod in xmldetalles.ChildNodes)
                {
                    CreateXmlNode(nodopagos, nod, item.secuencia, item);
                }

            }
        }

        public static bool DetallesAdicionales(object datasource)
        {
            Electronicdet det = (Electronicdet)datasource;

            // Debe usar el mismo criterio que el gate empty="no" de cada <detAdicional> hijo
            // (CreateXmlNode, mas abajo) - si no, el padre se agrega aunque los 3 hijos se
            // terminen podando por ser solo whitespace (ej. ddoc_observaciones = "\n"),
            // dejando un <detallesAdicionales /> vacio que el XSD del SRI rechaza.
            bool existe = false;
            if (!string.IsNullOrWhiteSpace(det.eled_adicional1) || !string.IsNullOrWhiteSpace(det.eled_adicional2) || !string.IsNullOrWhiteSpace(det.eled_adicional3))
                existe = true;
            return existe;
        }

        public static Comprobante ReadFacturaCompra(string file, string user, List<string> claves)
        {
            //string plantillaobl = Constantes.GetParameter("plantillaobl");
            //var serializer = new JavaScriptSerializer();
            //Comprobante comprobante = serializer.Deserialize<Comprobante>(plantillaobl);
            Comprobante comprobante = new Comprobante(); 

            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(file);



                XmlNodeList elemList = xmldoc.GetElementsByTagName("autorizacion");
                for (int i = 0; i < elemList.Count; i++)
                {

                    string nroautoriza = XmlReader.GetString(elemList[i].SelectSingleNode("numeroAutorizacion"));
                    DateTime? fechaautoriza = XmlReader.GetDateTime(elemList[i].SelectSingleNode("fechaAutorizacion"));

                    string strxml = XmlReader.GetString(elemList[i].SelectSingleNode("comprobante"));

                    if (!claves.Contains(nroautoriza))
                    {
                        //Comprobante obl = XmlReader.CargarObligacion(strxml.Trim(), comprobante, nroautoriza, fechaautoriza);
                        Comprobante com = XmlReader.CargarComprobante(strxml.Trim(), nroautoriza, fechaautoriza);
                        com.crea_fecha = DateTime.Now;
                        com.crea_usr = user;
                        com.com_periodo = com.com_fecha.Year;
                        com.com_mes = com.com_fecha.Month;
                        com.com_dia = com.com_fecha.Day;
                        com.com_anio = com.com_anio;
                        //if (obl.com_codigo < 0)
                        //    return obl;
                        //else
                        //   return FAC.create_obligacion(obl);
                        if (com.com_codigo < 0)
                            return com;
                        else
                        {
                            if (com.com_tipodoc == 14)//OBL
                                return FAC.create_obligacion(com);
                            if (com.com_tipodoc == 6)//REC -->Retencion
                            {
                                com = FAC.save_cancelacion(com, true);
                                return FAC.account_recibo(com);
                            }
                        }
                    }
                    else
                    {
                        comprobante.com_doctran = "El archivo con clave " + nroautoriza + " ya fue previamente cargado...";                        
                    }

                }

            }
            catch (Exception ex)
            {
                comprobante.com_doctran = ex.Message;
            }




            return comprobante;
        }



        public static List<ElectronicoClave> GetNoCargados(List<ElectronicoClave> lst, int codempresa)
        {
            string wherein = string.Join("','", lst.Select(s => s.clave).ToArray());
            WhereParams where = new WhereParams("ele_empresa = {0} and ele_clave in ('" + wherein + "') and ele_estado <> 9", codempresa);
            List<BusinessObjects.Electronico> comprobantes = ElectronicoBLL.GetAll(where, "");
           
            List<string> clavesexisten = comprobantes.Select(s => s.ele_clave).ToList();

            return  lst.Where(w => !clavesexisten.Contains(w.clave)).ToList();
            //return lst.Except(clavesexisten).ToList();

        }
      

        public static void SaveElectronico(SICEAzure.SICEAutorizacion autorizacion, int codempresa, string crea_usr, string carga, ElectronicoClave clave)
        {

            if (autorizacion != null)
            {
                BusinessObjects.Electronico electronico = new BusinessObjects.Electronico();
                electronico.ele_empresa = codempresa;
                electronico.ele_clave = autorizacion.numeroAutorizacion;
                electronico.ele_documento = clave.numero;
                electronico.ele_fecha_autoriza = autorizacion.fechaAutorizacion;
                electronico.ele_fecha_carga = DateTime.Now;
                electronico.ele_estadoelectronico = autorizacion.estado;
                electronico.ele_ambiente = autorizacion.ambiente;
                electronico.ele_xml = autorizacion.comprobante;
                electronico.ele_mensaje = JsonConvert.SerializeObject(autorizacion.mensajes);
                electronico.ele_carga = carga;
                electronico.ele_estado = autorizacion.fechaAutorizacion.HasValue?0:2;//0 pendiente de creacion como comprobante 2:Sin autorizacion
                electronico.crea_usr = crea_usr;
                electronico.crea_fecha = DateTime.Now;
                Comprobante comp = XmlReader.CargarComprobante(electronico.ele_xml, autorizacion.numeroAutorizacion, autorizacion.fechaAutorizacion);
                comp.crea_fecha = DateTime.Now;
                comp.crea_usr = crea_usr;
                comp.com_periodo = comp.com_fecha.Year;
                comp.com_mes = comp.com_fecha.Month;
                comp.com_dia = comp.com_fecha.Day;
                comp.com_anio = comp.com_anio;
                if (comp.com_codigo >=0)
                { 
                    if (comp.com_tipodoc == 14)//OBL
                        comp = FAC.create_obligacion(comp);
                    if (comp.com_tipodoc == 6)//REC -->Retencion
                    {
                        comp = FAC.save_cancelacion(comp, true);
                        comp = FAC.account_recibo(comp);
                    }
                }

                electronico.ele_comprobante = comp.com_codigo;
                electronico.ele_respuesta = comp.com_doctran;
                ElectronicoBLL.Insert(electronico);

                UpdateCargaElectronico(electronico);                


            }



        }

        public static Electronicocarga CreateCargaElectronico(int empresa, string usr, List<ElectronicoClave> claves)
        {
            List<ElectronicoClave> clavesLoad = GetNoCargados(claves, empresa);

            Electronicocarga electronicocarga = new Electronicocarga();
            electronicocarga.eca_empresa = empresa;
            electronicocarga.eca_id = Guid.NewGuid().ToString();
            electronicocarga.eca_inicio = DateTime.Now;
            electronicocarga.eca_archivo = String.Join(",", claves);
            electronicocarga.eca_claves = String.Join(",", clavesLoad);
            electronicocarga.eca_registros = claves.Count;
            electronicocarga.eca_descargados = clavesLoad.Count;
            electronicocarga.eca_estado = 0; //0: Creado 1: Finalizado
            electronicocarga.crea_usr = usr;
            electronicocarga.crea_fecha = DateTime.Now;
            ElectronicocargaBLL.Insert(electronicocarga);
            _ = Task.Run(() => SICEAzure.GetAutorizacion(clavesLoad, empresa, usr, electronicocarga.eca_id));
            return electronicocarga;
        }


        public static Electronicocarga UpdateCargaElectronico(BusinessObjects.Electronico electronico)
        {

            Electronicocarga electronicocarga = ElectronicocargaBLL.GetByPK(new Electronicocarga() { eca_empresa = electronico.ele_empresa, eca_empresa_key = electronico.ele_empresa, eca_id =electronico.ele_carga, eca_id_key = electronico.ele_carga});
            if (electronicocarga.crea_fecha.HasValue)
            {
                //electronicocarga.eca_descargados = (electronicocarga.eca_descargados ?? 0) + 1;
                if ((electronico.ele_comprobante ?? 0) > 0)
                    electronicocarga.eca_creados = (electronicocarga.eca_creados ?? 0) + 1;
                else
                    electronicocarga.eca_error = (electronicocarga.eca_error ?? 0) + 1;

                electronicocarga.mod_fecha = DateTime.Now;                
                electronicocarga.eca_empresa_key = electronicocarga.eca_empresa;
                electronicocarga.eca_id_key = electronicocarga.eca_id;
                ElectronicocargaBLL.Update(electronicocarga);

            }
            return electronicocarga;
        }

        public static Electronicocarga EndCargaElectronico(int empresa, string id)
        {
            Electronicocarga electronicocarga = ElectronicocargaBLL.GetByPK(new Electronicocarga() { eca_empresa = empresa, eca_empresa_key = empresa, eca_id = id, eca_id_key = id });
            if (electronicocarga.crea_fecha.HasValue)
            {
                electronicocarga.eca_fin = DateTime.Now;
                electronicocarga.eca_estado = 1; //finalizado
                electronicocarga.eca_empresa_key = electronicocarga.eca_empresa;
                electronicocarga.eca_id_key = electronicocarga.eca_id;
                ElectronicocargaBLL.Update(electronicocarga);

            }         
            return electronicocarga;
        }

        public static List<Electronicocarga> GetElectronicoCargas(int empresa, int? top)
        {

            WhereParams where = new WhereParams();
            where.where = "eca_empresa=" + empresa;

            List<Electronicocarga> lst = ElectronicocargaBLL.GetAllTop(where, "eca_inicio desc", top ?? 100);
            return lst;
        }


        public static string GetLastElectronicoCargas(int empresa, int? top)
        {
            List<Electronicocarga> lst = GetElectronicoCargas(empresa, top);


            StringBuilder html = new StringBuilder();
            //html.Append("<table class='electronicores'><tr>");
            html.Append("<tr>");
            html.Append("<th>Id</th>");
            html.Append("<th>Inicio</th>");
            html.Append("<th>Fin</th>");
            html.Append("<th>Registros</th>");
            html.Append("<th>Descargados</th>");
            html.Append("<th>Creados</th>");
            html.Append("<th>Error</th>");
            html.Append("<th></th>");
            html.Append("</tr>");
            foreach (Electronicocarga item in lst)
            {
                html.Append("<tr>");
                html.AppendFormat("<td><a href='#' data-id='{0}' onclick='ShowCargaElectronico($(this).data());'>{0}</a><i</td>", item.eca_id);
                html.AppendFormat("<td>{0}</td>", item.eca_inicio.Value.ToString("dd/MM/yyyy HH:mm:ss"));
                html.AppendFormat("<td>{0}</td>", item.eca_fin.HasValue ? item.eca_fin.Value.ToString("dd/MM/yyyy HH:mm:ss") : "");
                html.AppendFormat("<td>{0}</td>", item.eca_registros??0);
                html.AppendFormat("<td>{0}</td>", item.eca_descargados??0);
                html.AppendFormat("<td><a href='#' data-id='{1}' onclick='GetCargaDetalleOK($(this).data());'>{0}</a></td>", item.eca_creados??0, item.eca_id);
                html.AppendFormat("<td><a href='#' data-id='{1}' onclick='GetCargaDetalleERROR($(this).data());'>{0}</a></td>", item.eca_error??0, item.eca_id); ;
                html.AppendFormat("<td ><i data-id='{0}' class='fa fa-plus-circle' onclick='AnulateCarga($(this).data());'></i></td>", item.eca_id);
                html.Append("</tr>");
            }
            //html.Append("</tbody></table>");

            return html.ToString();

        }

        public static string AnulateElectronicoCarga(int empresa, string id)
        {
            Electronicocarga electronicocarga = ElectronicocargaBLL.GetByPK(new Electronicocarga() { eca_empresa = empresa, eca_empresa_key = empresa, eca_id = id, eca_id_key = id });
            string where = "ele_empresa=" + empresa + " and ele_carga='" + id + "'";            
            List<BusinessObjects.Electronico> lst = BusinessLogicLayer.ElectronicoBLL.GetAll(where, "");

            foreach (var item in lst)
            {
                General.AnulaComprobante(new Comprobante() { com_empresa = item.ele_empresa, com_codigo = item.ele_comprobante ?? 0 });
                //item.ele_empresa_key = item.ele_empresa;
                //item.ele_clave_key = item.ele_clave;
                //item.ele_estado = (int)Enums.EstadoRegistro.ANULADO;
                //ElectronicoBLL.Update(item);
                ElectronicoBLL.Delete(item);
            }
            electronicocarga.eca_empresa_key = electronicocarga.eca_empresa;
            electronicocarga.eca_id_key = electronicocarga.eca_id;
            electronicocarga.eca_creados = 0;
            electronicocarga.eca_error = 0;
            electronicocarga.eca_estado = (int)Enums.EstadoRegistro.ANULADO;
            ElectronicocargaBLL.Update(electronicocarga);
            return "Carga Anulada...";




        }

    }
}