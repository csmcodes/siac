using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;
using System.Collections;

namespace BusinessObjects
{
    public class Comprobante
    {
        #region Properties

        [Data(key = true)]
        public Int32 com_empresa { get; set; }
        [Data(originalkey = true)]
        public Int32 com_empresa_key { get; set; }
        [Data(key = true, auto = true)]
        public Int64 com_codigo { get; set; }
        [Data(originalkey = true)]
        public Int64 com_codigo_key { get; set; }
        public Int32 com_periodo { get; set; }
        public Int32 com_tipodoc { get; set; }
        public Int32 com_ctipocom { get; set; }
        public String com_concepto { get; set; }
        public Int32 com_modulo { get; set; }
        public Int32 com_transacc { get; set; }
        public Int32 com_nocontable { get; set; }
        public Int32 com_estado { get; set; }
        public Int32 com_descuadre { get; set; }
        public Int32 com_adestino { get; set; }
        public DateTime com_fecha { get; set; }
        public Int32 com_numero { get; set; }
        public String com_doctran { get; set; }
        public DateTime? com_fechafin { get; set; }
        public Int32? com_dia { get; set; }
        public Int32? com_mes { get; set; }
        public Int32? com_anio { get; set; }
        public Decimal? com_tipo_cambio { get; set; }
        [Data(noupdate = true)]
        public String crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
        public Int32? com_pventa { get; set; }
        public Int32? com_centro { get; set; }
        public Int32? com_tclipro { get; set; }
        public Int32? com_codclipro { get; set; }
        public Int32? com_agente { get; set; }
        public Int32? com_cuenta { get; set; }
        public Int64? com_cie_comprobante { get; set; }
        public Int64? com_ref_comprobante { get; set; }
        public Int32? com_anulado { get; set; }
        public Int64? com_anu_comprobante { get; set; }
        public Int32? com_aut_tipo { get; set; }
        public Int32? com_nivel_aprobacion { get; set; }
        public Int32? com_bodega { get; set; }
        public Int32? com_almacen { get; set; }
        public Int32? com_serie { get; set; }
        public Int32? com_vehiculo { get; set; }
        public Int32? com_ruta { get; set; }
        public Int64? com_token{ get; set; }

        public string com_claveelec { get; set; }
        public string com_estadoelec { get; set; }
        public string com_mensajeelec { get; set; }
        public string com_emision { get; set; }
        public string com_ambiente { get; set; }


        [Data(nosql = true, tablaref = "ctipocom", camporef = "cti_id", foreign = "com_empresa, com_ctipocom", keyref = "cti_empresa, cti_codigo", join = "inner")]
        public string com_ctipocomid { get; set; }

        [Data(nosql = true, tablaref = "ctipocom", camporef = "cti_autoriza", foreign = "com_empresa, com_ctipocom", keyref = "cti_empresa, cti_codigo", join = "inner")]
        public Int32? com_ctipocomautoriza { get; set; }


        [Data(nosql = true, tablaref = "almacen", camporef = "alm_id", foreign = "com_empresa, com_almacen", keyref = "alm_empresa, alm_codigo", join = "inner")]
        public string com_almacenid { get; set; }

        [Data(nosql = true, tablaref = "almacen", camporef = "alm_nombre", foreign = "com_empresa, com_almacen", keyref = "alm_empresa, alm_codigo", join = "inner")]
        public string com_almacennombre { get; set; }

        [Data(nosql = true, tablaref = "puntoventa", camporef = "pve_id", foreign = "com_empresa, com_almacen, com_pventa", keyref = "pve_empresa, pve_almacen, pve_secuencia", join = "left")]
        public string com_pventaid { get; set; }

        [Data(nosql = true, tablaref = "puntoventa", camporef = "pve_nombre", foreign = "com_empresa, com_almacen, com_pventa", keyref = "pve_empresa, pve_almacen, pve_secuencia", join = "left")]
        public string com_pventanombre { get; set; }

        [Data(nosql = true, tablaref = "bodega", camporef = "bod_id", foreign = "com_empresa, com_bodega", keyref = "bod_empresa, bod_codigo", join = "left")]
        public string com_bodegaid { get; set; }

        [Data(nosql = true, tablaref = "bodega", camporef = "bod_nombre", foreign = "com_empresa, com_bodega", keyref = "bod_empresa, bod_codigo", join = "left")]
        public string com_bodeganombre { get; set; }




        [Data(nosql = true, tablaref = "persona", camporef = "per_nombres||' '||per_apellidos", foreign = "com_codclipro", keyref = "per_codigo", join = "left")]
        public string com_nombresocio { get; set; }
        [Data(nosql = true, tablaref = "persona", camporef = "per_ciruc", foreign = "com_codclipro", keyref = "per_codigo", join = "left")]
        public string com_ciruc{ get; set; }

        [Data(nosql = true, tablaref = "ruta", camporef = "rut_nombre", foreign = "com_ruta", keyref = "rut_codigo", join = "left")]
        public string com_nombreruta { get; set; }
        [Data(nosql = true, tablaref = "vehiculo", camporef = "veh_disco", foreign = "com_vehiculo", keyref = "veh_codigo", join = "left")]
        public string com_nrodisco { get; set; }
        [Data(nosql = true, tablaref = "vehiculo", camporef = "veh_placa", foreign = "com_vehiculo", keyref = "veh_codigo", join = "left")]
        public string com_placa { get; set; }


        [Data(nosql = true, tablaref = "ccomenv", camporef = "cenv_socio", foreign = "com_codigo", keyref = "cenv_comprobante", join = "left")]
        public Int32? com_socio { get; set; }


        [Data(nosql = true, tablaref = "ccomdoc", camporef = "cdoc_aut_factura", foreign = "com_empresa,com_codigo", keyref = "cdoc_empresa,cdoc_comprobante", join = "left")]
        public string com_documento_obli { get; set; }



        [Data(nosql = true, tablaref = "modulo", camporef = "mod_id", foreign = "com_modulo", keyref = "mod_codigo", join = "left")]
        public string com_modulo_id { get; set; }
        [Data(nosql = true, tablaref = "tipodoc", camporef = "tpd_id", foreign = "com_tipodoc", keyref = "tpd_codigo", join = "left")]
        public string com_tipo_doc { get; set; }

        [Data(nosql = true, tablaref = "usuario", camporef = "usr_nombres", foreign = "crea_usr", keyref = "usr_id", join = "left")]
        public string crea_usrnombres { get; set; }
        [Data(nosql = true, tablaref = "usuario", camporef = "usr_nombres", foreign = "mod_usr", keyref = "usr_id", join = "left")]
        public string mod_usrnombres { get; set; }
        [Data(nosql = true, tablaref = "total", camporef = "tot_total", foreign = "com_empresa,com_codigo", keyref = "tot_empresa,tot_comprobante", join = "left")]
        public decimal? com_total { get; set; }



        [Data(noprop = true)]
        public Ccomdoc ccomdoc { get; set; }

        [Data(noprop = true)]
        public Ccomenv ccomenv { get; set; }

        [Data(noprop = true)]
        public Total total { get; set; }

        [Data(noprop = true)]
        public List<Rutaxfactura> rutafactura { get; set; }

        [Data(noprop = true)]
        public List<Ddocumento> documentos { get; set; }

        [Data(noprop = true)]
        public List<Drecibo> recibos { get; set; }
        

        [Data(noprop = true)]
        public List<Dcancelacion> cancelaciones { get; set; }

        [Data(noprop = true)]
        public List<Planillacli> planillas { get; set; }


        [Data(noprop = true)]
        public List<Dretencion> retenciones { get; set; }

        [Data(noprop = true)]
        public List<Dnotacre> notascre { get; set; }

        [Data(noprop = true)]
        public Planillacomprobante planillacomp { get; set; }

        [Data(noprop = true)]
        public List<Dcontable> contables { get; set; }
        [Data(noprop = true)]
        public List<Dbancario> bancario { get; set; }

        [Data(noprop = true)]
        public List<Rubrosplanilla> rubros { get; set; }

        [Data(noprop = true)]
        public string impresora { get; set; }


        [Data(noprop = true)]
        public string hojaruta { get; set; }

        [Data(noprop = true)]
        public Int32? despachado { get; set; }


        [Data(noprop = true)]
        public string com_fechastr { get; set; }
        [Data(noprop = true)]
        public string crea_fechastr { get; set; }
        [Data(noprop = true)]
        public string mod_fechastr { get; set; }


        #endregion

        #region Constructors


        public Comprobante()
        {

        }

        public Comprobante(Int32 com_empresa, Int64 com_codigo, Int32 com_periodo, Int32 com_tipodoc, Int32 com_ctipocom, String com_concepto, Int32 com_modulo, Int32 com_transacc, Int32 com_nocontable, Int32 com_estado, Int32 com_descuadre, Int32 com_adestino, DateTime com_fecha, Int32 com_numero, String com_doctran, DateTime com_fechafin, Int32 com_dia, Int32 com_mes, Int32 com_anio, Decimal com_tipo_cambio, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha, Int32 com_pventa, Int32 com_centro, Int32 com_tclipro, Int32 com_codclipro, Int32 com_agente, Int32 com_cuenta, Int64 com_cie_comprobante, Int64 com_ref_comprobante, Int32 com_anulado, Int64 com_anu_comprobante, Int32 com_aut_tipo, Int32 com_nivel_aprobacion, Int32 com_bodega, Int32 com_almacen, Int32 com_serie)
        {
            this.com_empresa = com_empresa;
            this.com_codigo = com_codigo;
            this.com_periodo = com_periodo;
            this.com_tipodoc = com_tipodoc;
            this.com_ctipocom = com_ctipocom;
            this.com_concepto = com_concepto;
            this.com_modulo = com_modulo;
            this.com_transacc = com_transacc;
            this.com_nocontable = com_nocontable;
            this.com_estado = com_estado;
            this.com_descuadre = com_descuadre;
            this.com_adestino = com_adestino;
            this.com_fecha = com_fecha;
            this.com_numero = com_numero;
            this.com_doctran = com_doctran;
            this.com_fechafin = com_fechafin;
            this.com_dia = com_dia;
            this.com_mes = com_mes;
            this.com_anio = com_anio;
            this.com_tipo_cambio = com_tipo_cambio;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;
            this.com_pventa = com_pventa;
            this.com_centro = com_centro;
            this.com_tclipro = com_tclipro;
            this.com_codclipro = com_codclipro;
            this.com_agente = com_agente;
            this.com_cuenta = com_cuenta;
            this.com_cie_comprobante = com_cie_comprobante;
            this.com_ref_comprobante = com_ref_comprobante;
            this.com_anulado = com_anulado;
            this.com_anu_comprobante = com_anu_comprobante;
            this.com_aut_tipo = com_aut_tipo;
            this.com_nivel_aprobacion = com_nivel_aprobacion;
            this.com_bodega = com_bodega;
            this.com_almacen = com_almacen;
            this.com_serie = com_serie;


        }

        public Comprobante(IDataReader reader)
        {
            this.com_empresa = (Int32)reader["com_empresa"];
            this.com_codigo = (Int64)reader["com_codigo"];
            this.com_periodo = (Int32)reader["com_periodo"];
            this.com_tipodoc = (Int32)reader["com_tipodoc"];
            this.com_ctipocom = (Int32)reader["com_ctipocom"];
            this.com_concepto = (String)reader["com_concepto"];
            this.com_modulo = (Int32)reader["com_modulo"];
            this.com_transacc = (Int32)reader["com_transacc"];
            this.com_nocontable = (Int32)reader["com_nocontable"];
            this.com_estado = (Int32)reader["com_estado"];
            this.com_descuadre = (Int32)reader["com_descuadre"];
            this.com_adestino = (Int32)reader["com_adestino"];
            this.com_fecha = (DateTime)reader["com_fecha"];
            this.com_numero = (Int32)reader["com_numero"];
            this.com_doctran = reader["com_doctran"].ToString();
            this.com_fechafin = (reader["com_fechafin"] != DBNull.Value) ? (DateTime?)reader["com_fechafin"] : null;
            this.com_dia = (reader["com_dia"] != DBNull.Value) ? (Int32?)reader["com_dia"] : null;
            this.com_mes = (reader["com_mes"] != DBNull.Value) ? (Int32?)reader["com_mes"] : null;
            this.com_anio = (reader["com_anio"] != DBNull.Value) ? (Int32?)reader["com_anio"] : null;
            this.com_tipo_cambio = (reader["com_tipo_cambio"] != DBNull.Value) ? (Decimal?)reader["com_tipo_cambio"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.com_pventa = (reader["com_pventa"] != DBNull.Value) ? (Int32?)reader["com_pventa"] : null;
            this.com_centro = (reader["com_centro"] != DBNull.Value) ? (Int32?)reader["com_centro"] : null;
            this.com_tclipro = (reader["com_tclipro"] != DBNull.Value) ? (Int32?)reader["com_tclipro"] : null;
            this.com_codclipro = (reader["com_codclipro"] != DBNull.Value) ? (Int32?)reader["com_codclipro"] : null;
            this.com_agente = (reader["com_agente"] != DBNull.Value) ? (Int32?)reader["com_agente"] : null;
            this.com_cuenta = (reader["com_cuenta"] != DBNull.Value) ? (Int32?)reader["com_cuenta"] : null;
            this.com_cie_comprobante = (reader["com_cie_comprobante"] != DBNull.Value) ? (Int64?)reader["com_cie_comprobante"] : null;
            this.com_ref_comprobante = (reader["com_ref_comprobante"] != DBNull.Value) ? (Int64?)reader["com_ref_comprobante"] : null;
            this.com_anulado = (reader["com_anulado"] != DBNull.Value) ? (Int32?)reader["com_anulado"] : null;
            this.com_anu_comprobante = (reader["com_anu_comprobante"] != DBNull.Value) ? (Int64?)reader["com_anu_comprobante"] : null;
            this.com_aut_tipo = (reader["com_aut_tipo"] != DBNull.Value) ? (Int32?)reader["com_aut_tipo"] : null;
            this.com_nivel_aprobacion = (reader["com_nivel_aprobacion"] != DBNull.Value) ? (Int32?)reader["com_nivel_aprobacion"] : null;
            this.com_bodega = (reader["com_bodega"] != DBNull.Value) ? (Int32?)reader["com_bodega"] : null;
            this.com_almacen = (reader["com_almacen"] != DBNull.Value) ? (Int32?)reader["com_almacen"] : null;
            this.com_serie = (reader["com_serie"] != DBNull.Value) ? (Int32?)reader["com_serie"] : null;
            this.com_vehiculo = (reader["com_vehiculo"] != DBNull.Value) ? (Int32?)reader["com_vehiculo"] : null;
            this.com_ruta = (reader["com_ruta"] != DBNull.Value) ? (Int32?)reader["com_ruta"] : null;
            this.com_token= (reader["com_token"] != DBNull.Value) ? (Int64?)reader["com_token"] : null;

            this.com_claveelec = (reader["com_claveelec"] != DBNull.Value) ? reader["com_claveelec"].ToString() : null;
            this.com_estadoelec = (reader["com_estadoelec"] != DBNull.Value) ? reader["com_estadoelec"].ToString() : null;
            this.com_mensajeelec = (reader["com_mensajeelec"] != DBNull.Value) ? reader["com_mensajeelec"].ToString() : null;
            this.com_emision = (reader["com_emision"] != DBNull.Value) ? reader["com_emision"].ToString() : null;
            this.com_ambiente= (reader["com_ambiente"] != DBNull.Value) ? reader["com_ambiente"].ToString() : null;

            this.com_total= (reader["com_total"] != DBNull.Value) ? (Decimal?)reader["com_total"] : null;

            this.com_socio = (reader["com_socio"] != DBNull.Value) ? (Int32?)reader["com_socio"] : null;
            this.com_ctipocomid = reader["com_ctipocomid"].ToString();
            this.com_ctipocomautoriza = (reader["com_ctipocomautoriza"] != DBNull.Value) ? (Int32?)reader["com_ctipocomautoriza"] : null;
            this.com_almacenid = reader["com_almacenid"].ToString();
            this.com_pventaid = reader["com_pventaid"].ToString();
            this.com_bodegaid = reader["com_bodegaid"].ToString();
            this.com_almacennombre = reader["com_almacennombre"].ToString();
            this.com_pventanombre = reader["com_pventanombre"].ToString();
            this.com_bodeganombre = reader["com_bodeganombre"].ToString();
            this.com_nombresocio = reader["com_nombresocio"].ToString();
            this.com_ciruc= reader["com_ciruc"].ToString();
            this.com_nombreruta = reader["com_nombreruta"].ToString();
            this.com_nrodisco = reader["com_nrodisco"].ToString();
            this.com_placa = reader["com_placa"].ToString();
            this.com_modulo_id = reader["com_modulo_id"].ToString();
            this.com_tipo_doc = reader["com_tipo_doc"].ToString();
            this.com_documento_obli = reader["com_documento_obli"].ToString();

            this.crea_usrnombres = reader["crea_usrnombres"].ToString();
            this.mod_usrnombres = reader["mod_usrnombres"].ToString();

            this.com_fechastr =  com_fecha.ToString();
            this.crea_fechastr = crea_fecha.HasValue ? crea_fecha.ToString() : "";
            this.mod_fechastr = mod_fecha.HasValue ? mod_fecha.ToString() : "";

        }

        public Comprobante(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;

                object com_fecha = null;
                object com_concepto = null;
                object com_modulo = null;
                object com_transacc = null;
                object com_nocontable = null;
                object com_estado = null;
                object com_descuadre = null;
                object com_adestino = null;
                object com_numero = null;
                object com_empresa = null;
                object com_codigo = null;
                object com_periodo = null;
                object com_tipodoc = null;
                object com_ctipocom = null;
                object com_almacen = null;
                object com_serie = null;
                object com_doctran = null;
                object com_pventa = null;
                object com_centro = null;
                object com_tclipro = null;
                object com_codclipro = null;
                object com_agente = null;
                object com_cuenta = null;
                object com_cie_comprobante = null;
                object com_ref_comprobante = null;
                object com_anulado = null;
                object com_anu_comprobante = null;
                object com_aut_tipo = null;
                object com_nivel_aprobacion = null;
                object com_bodega = null;
                object com_fechafin = null;
                object com_dia = null;
                object com_mes = null;
                object com_anio = null;
                object com_tipo_cambio = null;
                object com_ruta = null;
                object com_vehiculo = null;
                object com_token= null;

                object com_claveelec = null;
                object com_estadoelec = null;
                object com_mensajeelec = null;
                object com_emision = null;
                object com_ambiente = null;

                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;

                object ccomdoc = null;
                object retencion = null;
                object notascre = null;
                object ccomenv = null;
                object total = null;
                object rutafactura = null;
                object documentos = null;
                object recibos = null;
                object cancelaciones = null;
                object planillas = null;
                object planillacomp = null;
                object contables = null;
                object bancario = null;
                object rubros = null;
                

                tmp.TryGetValue("com_fecha", out com_fecha);
                tmp.TryGetValue("com_concepto", out com_concepto);
                tmp.TryGetValue("com_modulo", out com_modulo);
                tmp.TryGetValue("com_transacc", out com_transacc);
                tmp.TryGetValue("com_nocontable", out com_nocontable);
                tmp.TryGetValue("com_estado", out com_estado);
                tmp.TryGetValue("com_descuadre", out com_descuadre);
                tmp.TryGetValue("com_adestino", out com_adestino);
                tmp.TryGetValue("com_numero", out com_numero);
                tmp.TryGetValue("com_empresa", out com_empresa);
                tmp.TryGetValue("com_codigo", out com_codigo);
                tmp.TryGetValue("com_periodo", out com_periodo);
                tmp.TryGetValue("com_tipodoc", out com_tipodoc);
                tmp.TryGetValue("com_ctipocom", out com_ctipocom);
                tmp.TryGetValue("com_almacen", out com_almacen);
                tmp.TryGetValue("com_serie", out com_serie);
                tmp.TryGetValue("com_doctran", out com_doctran);
                tmp.TryGetValue("com_pventa", out com_pventa);
                tmp.TryGetValue("com_centro", out com_centro);
                tmp.TryGetValue("com_tclipro", out com_tclipro);
                tmp.TryGetValue("com_codclipro", out com_codclipro);
                tmp.TryGetValue("com_agente", out com_agente);
                tmp.TryGetValue("com_cuenta", out com_cuenta);
                tmp.TryGetValue("com_cie_comprobante", out com_cie_comprobante);
                tmp.TryGetValue("com_ref_comprobante", out com_ref_comprobante);
                tmp.TryGetValue("com_anulado", out com_anulado);
                tmp.TryGetValue("com_anu_comprobante", out com_anu_comprobante);
                tmp.TryGetValue("com_aut_tipo", out com_aut_tipo);
                tmp.TryGetValue("com_nivel_aprobacion", out com_nivel_aprobacion);
                tmp.TryGetValue("com_bodega", out com_bodega);
                tmp.TryGetValue("com_fechafin", out com_fechafin);
                tmp.TryGetValue("com_dia", out com_dia);
                tmp.TryGetValue("com_mes", out com_mes);
                tmp.TryGetValue("com_anio", out com_anio);
                tmp.TryGetValue("com_tipo_cambio", out com_tipo_cambio);
                tmp.TryGetValue("com_ruta", out com_ruta);
                tmp.TryGetValue("com_vehiculo", out com_vehiculo);
                tmp.TryGetValue("com_token", out com_token);

                tmp.TryGetValue("com_claveelec", out com_claveelec);
                tmp.TryGetValue("com_estadoelec", out com_estadoelec);
                tmp.TryGetValue("com_mensajeelec", out com_mensajeelec);
                tmp.TryGetValue("com_emision", out com_emision);
                tmp.TryGetValue("com_ambiente", out com_ambiente);

                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);

                tmp.TryGetValue("retenciones", out retencion);
                tmp.TryGetValue("notascre", out notascre);
                
                tmp.TryGetValue("ccomdoc", out ccomdoc);
                tmp.TryGetValue("ccomenv", out ccomenv);
                tmp.TryGetValue("total", out total);
                tmp.TryGetValue("rutafactura", out rutafactura);
                tmp.TryGetValue("documentos", out documentos);

                tmp.TryGetValue("recibos", out recibos);
                tmp.TryGetValue("cancelaciones", out cancelaciones);
                tmp.TryGetValue("planillas", out planillas);
                tmp.TryGetValue("planillacomp", out planillacomp);
                tmp.TryGetValue("contables", out contables);
                tmp.TryGetValue("bancario", out bancario);
                tmp.TryGetValue("rubros", out rubros);

                this.com_fecha = (DateTime)Conversiones.GetValueByType(com_fecha, typeof(DateTime));
                this.com_concepto = (String)Conversiones.GetValueByType(com_concepto, typeof(String));
                this.com_modulo = (Int32)Conversiones.GetValueByType(com_modulo, typeof(Int32));
                this.com_transacc = (Int32)Conversiones.GetValueByType(com_transacc, typeof(Int32));
                this.com_nocontable = (Int32)Conversiones.GetValueByType(com_nocontable, typeof(Int32));
                this.com_estado = (Int32)Conversiones.GetValueByType(com_estado, typeof(Int32));
                this.com_descuadre = (Int32)Conversiones.GetValueByType(com_descuadre, typeof(Int32));
                this.com_adestino = (Int32)Conversiones.GetValueByType(com_adestino, typeof(Int32));
                this.com_numero = (Int32)Conversiones.GetValueByType(com_numero, typeof(Int32));
                this.com_empresa = (Int32)Conversiones.GetValueByType(com_empresa, typeof(Int32));
                this.com_codigo = (Int64)Conversiones.GetValueByType(com_codigo, typeof(Int64));
                this.com_periodo = (Int32)Conversiones.GetValueByType(com_periodo, typeof(Int32));
                this.com_tipodoc = (Int32)Conversiones.GetValueByType(com_tipodoc, typeof(Int32));
                this.com_ctipocom = (Int32)Conversiones.GetValueByType(com_ctipocom, typeof(Int32));
                this.com_almacen = (Int32?)Conversiones.GetValueByType(com_almacen, typeof(Int32?));
                this.com_serie = (Int32?)Conversiones.GetValueByType(com_serie, typeof(Int32?));
                this.com_doctran = (String)Conversiones.GetValueByType(com_doctran, typeof(String));
                this.com_pventa = (Int32?)Conversiones.GetValueByType(com_pventa, typeof(Int32?));
                this.com_centro = (Int32?)Conversiones.GetValueByType(com_centro, typeof(Int32?));
                this.com_tclipro = (Int32?)Conversiones.GetValueByType(com_tclipro, typeof(Int32?));
                this.com_codclipro = (Int32?)Conversiones.GetValueByType(com_codclipro, typeof(Int32?));
                this.com_agente = (Int32?)Conversiones.GetValueByType(com_agente, typeof(Int32?));
                this.com_cuenta = (Int32?)Conversiones.GetValueByType(com_cuenta, typeof(Int32?));
                this.com_cie_comprobante = (Int64?)Conversiones.GetValueByType(com_cie_comprobante, typeof(Int64?));
                this.com_ref_comprobante = (Int64?)Conversiones.GetValueByType(com_ref_comprobante, typeof(Int64?));
                this.com_anulado = (Int32?)Conversiones.GetValueByType(com_anulado, typeof(Int32?));
                this.com_anu_comprobante = (Int64?)Conversiones.GetValueByType(com_anu_comprobante, typeof(Int64?));
                this.com_aut_tipo = (Int32?)Conversiones.GetValueByType(com_aut_tipo, typeof(Int32?));
                this.com_nivel_aprobacion = (Int32?)Conversiones.GetValueByType(com_nivel_aprobacion, typeof(Int32?));
                this.com_bodega = (Int32?)Conversiones.GetValueByType(com_bodega, typeof(Int32?));
                this.com_fechafin = (DateTime?)Conversiones.GetValueByType(com_fechafin, typeof(DateTime?));
                if (this.com_fecha > DateTime.MinValue)
                {
                    this.com_dia = this.com_fecha.Day;
                    this.com_mes = this.com_fecha.Month;
                    this.com_anio = this.com_fecha.Year;
                }
                else
                {
                    this.com_dia = (Int32?)Conversiones.GetValueByType(com_dia, typeof(Int32?));
                    this.com_mes = (Int32?)Conversiones.GetValueByType(com_mes, typeof(Int32?));
                    this.com_anio = (Int32?)Conversiones.GetValueByType(com_anio, typeof(Int32?));
                }
                this.com_tipo_cambio = (Decimal?)Conversiones.GetValueByType(com_tipo_cambio, typeof(Decimal?));
                this.com_ruta = (Int32?)Conversiones.GetValueByType(com_ruta, typeof(Int32?));
                this.com_vehiculo = (Int32?)Conversiones.GetValueByType(com_vehiculo, typeof(Int32?));
                this.com_token= (Int64?)Conversiones.GetValueByType(com_token, typeof(Int64?));

                this.com_claveelec= (String)Conversiones.GetValueByType(com_claveelec, typeof(String));
                this.com_estadoelec= (String)Conversiones.GetValueByType(com_estadoelec, typeof(String));
                this.com_mensajeelec = (String)Conversiones.GetValueByType(com_mensajeelec, typeof(String));
                this.com_emision= (String)Conversiones.GetValueByType(com_emision, typeof(String));
                this.com_ambiente= (String)Conversiones.GetValueByType(com_ambiente, typeof(String));

                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));

                this.ccomdoc = new Ccomdoc(ccomdoc);
                this.ccomenv = new Ccomenv(ccomenv);
                this.retenciones = GetDretencionObj(retencion);
                this.notascre = GetDnotacreObj(notascre);
                this.total = new Total(total);
                this.rutafactura = GetRutaFacturaObj(rutafactura);
                this.documentos = GetDdocumentoObj(documentos);
                this.recibos = GetDreciboObj(recibos);
                this.cancelaciones = GetDcancelacionObj(cancelaciones);
                this.planillas = GetPlanillasObj(planillas);
                this.planillacomp = new Planillacomprobante(planillacomp);
                this.contables = GetDcontables(contables);
                this.bancario = GetDbancario(bancario);
                this.rubros = GetRubros(rubros);

                this.com_fechastr = this.com_fecha.ToString();
                this.crea_fechastr = this.crea_fecha.HasValue ? this.crea_fecha.ToString() : "";
                this.mod_fechastr = this.mod_fecha.HasValue ? this.mod_fecha.ToString() : "";

            }


        }
        #endregion

        #region Methods
        public List<Rutaxfactura> GetRutaFacturaObj(object arrayobj)
        {
            List<Rutaxfactura> detalle = new List<Rutaxfactura>();
            if (arrayobj != null)
            {
                Array array = (Array)arrayobj;
                foreach (Object item in array)
                {
                    if (item != null)
                        detalle.Add(new Rutaxfactura(item));
                }
            }
            return detalle;
        }

        public List<Dnotacre> GetDnotacreObj(object arrayobj)
        {
            List<Dnotacre> detalle = new List<Dnotacre>();
            if (arrayobj != null)
            {
                Array array = (Array)arrayobj;
                foreach (Object item in array)
                {
                    if (item != null)
                        detalle.Add(new Dnotacre(item));
                }
            }
            return detalle;
        }

        public List<Dretencion> GetDretencionObj(object arrayobj)
        {
            List<Dretencion> detalle = new List<Dretencion>();
            if (arrayobj != null)
            {
                Array array = (Array)arrayobj;
                foreach (Object item in array)
                {
                    if (item != null)
                        detalle.Add(new Dretencion(item));
                }
            }
            return detalle;
        }


        public List<Ddocumento> GetDdocumentoObj(object arrayobj)
        {
            List<Ddocumento> detalle = new List<Ddocumento>();
            if (arrayobj != null)
            {
                Array array = (Array)arrayobj;
                foreach (Object item in array)
                {
                    if (item != null)
                        detalle.Add(new Ddocumento(item));
                }
            }
            return detalle;
        }

        public List<Drecibo> GetDreciboObj(object arrayobj)
        {
            List<Drecibo> detalle = new List<Drecibo>();
            if (arrayobj != null)
            {
                Array array = (Array)arrayobj;
                foreach (Object item in array)
                {
                    if (item != null)
                        detalle.Add(new Drecibo(item));
                }
            }
            return detalle;
        }

        public List<Dcancelacion> GetDcancelacionObj(object arrayobj)
        {
            List<Dcancelacion> detalle = new List<Dcancelacion>();
            if (arrayobj != null)
            {
                Array array = (Array)arrayobj;
                foreach (Object item in array)
                {
                    if (item != null)
                        detalle.Add(new Dcancelacion(item));
                }
            }
            return detalle;
        }


        public List<Planillacli> GetPlanillasObj(object arrayobj)
        {
            List<Planillacli> detalle = new List<Planillacli>();
            if (arrayobj != null)
            {
                Array array = (Array)arrayobj;
                foreach (Object item in array)
                {
                    if (item != null)
                        detalle.Add(new Planillacli(item));
                }
            }
            return detalle;
        }


        public List<Dcontable> GetDcontables(object arrayobj)
        {
            List<Dcontable> detalle = new List<Dcontable>();
            if (arrayobj != null)
            {
                Array array = (Array)arrayobj;
                foreach (Object item in array)
                {
                    if (item != null)
                        detalle.Add(new Dcontable(item));
                }
            }
            return detalle;
        }

        public List<Dbancario> GetDbancario(object arrayobj)
        {
            List<Dbancario> detalle = new List<Dbancario>();
            if (arrayobj != null)
            {
                Array array = (Array)arrayobj;
                foreach (Object item in array)
                {
                    if (item != null)
                        detalle.Add(new Dbancario(item));
                }
            }
            return detalle;
        }

        public List<Rubrosplanilla> GetRubros(object arrayobj)
        {
            List<Rubrosplanilla> detalle = new List<Rubrosplanilla>();
            if (arrayobj != null)
            {
                Array array = (Array)arrayobj;
                foreach (Object item in array)
                {
                    if (item != null)
                        detalle.Add(new Rubrosplanilla(item));
                }
            }
            return detalle;
        }


        public PropertyInfo[] GetProperties()
        {
            return this.GetType().GetProperties();
        }
        public List<Comprobante> GetEnum()
        {
            return new List<Comprobante>();
        }
        public DataTable GetTable()
        {
            DataTable dt = new DataTable();
            PropertyInfo[] properties = this.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                bool flag = true;
                object[] attributes = property.GetCustomAttributes(typeof(Data), true);
                if (attributes.Length > 0)
                    foreach (Data dato in attributes)
                    {
                        Type t = property.PropertyType;
                        if (dato.noprop)
                        {
                            flag = false;
                        }
                    }

                if (flag)
                {
                    Type t = property.PropertyType;
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                        t = property.PropertyType.GetGenericArguments()[0];
                    }
                    dt.Columns.Add(new DataColumn("Cabecera_" + property.Name, t));
                    dt.Columns["Cabecera_" + property.Name].AllowDBNull = true;
                }
            }



            properties = typeof(Ccomenv).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                bool flag = true;
                object[] attributes = property.GetCustomAttributes(typeof(Data), true);
                if (attributes.Length > 0)
                    foreach (Data dato in attributes)
                    {
                        Type t = property.PropertyType;
                        if (dato.noprop)
                        {
                            flag = false;
                        }
                    }

                if (flag)
                {
                    Type t = property.PropertyType;
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                        t = property.PropertyType.GetGenericArguments()[0];
                    }
                    dt.Columns.Add(new DataColumn("Ccomenv_" + property.Name, t));
                    dt.Columns["Ccomenv_" + property.Name].AllowDBNull = true;
                }
            }

            properties = typeof(Rutaxfactura).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                bool flag = true;
                object[] attributes = property.GetCustomAttributes(typeof(Data), true);
                if (attributes.Length > 0)
                    foreach (Data dato in attributes)
                    {
                        Type t = property.PropertyType;
                        if (dato.noprop)
                        {
                            flag = false;
                        }
                    }

                if (flag)
                {
                    Type t = property.PropertyType;
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                        t = property.PropertyType.GetGenericArguments()[0];
                    }
                    dt.Columns.Add(new DataColumn("Rutaxfactura_" + property.Name, t));
                    dt.Columns["Rutaxfactura_" + property.Name].AllowDBNull = true;
                }
            }


            properties = typeof(Dcomdoc).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                bool flag = true;
                object[] attributes = property.GetCustomAttributes(typeof(Data), true);
                if (attributes.Length > 0)
                    foreach (Data dato in attributes)
                    {
                        Type t = property.PropertyType;
                        if (dato.noprop)
                        {
                            flag = false;
                        }
                    }

                if (flag)
                {
                    Type t = property.PropertyType;
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                        t = property.PropertyType.GetGenericArguments()[0];
                    }
                    dt.Columns.Add(new DataColumn("Dcomdoc_" + property.Name, t));
                    dt.Columns["Dcomdoc_" + property.Name].AllowDBNull = true;
                }
            }



            properties = typeof(Ccomdoc).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                bool flag = true;
                object[] attributes = property.GetCustomAttributes(typeof(Data), true);
                if (attributes.Length > 0)
                    foreach (Data dato in attributes)
                    {
                        Type t = property.PropertyType;
                        if (dato.noprop)
                        {
                            flag = false;
                        }
                    }

                if (flag)
                {
                    Type t = property.PropertyType;
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                        t = property.PropertyType.GetGenericArguments()[0];
                    }
                    dt.Columns.Add(new DataColumn("Ccomdoc_" + property.Name, t));
                    dt.Columns["Ccomdoc_" + property.Name].AllowDBNull = true;
                }
            }



            properties = typeof(Total).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                bool flag = true;
                object[] attributes = property.GetCustomAttributes(typeof(Data), true);
                if (attributes.Length > 0)
                    foreach (Data dato in attributes)
                    {
                        Type t = property.PropertyType;
                        if (dato.noprop)
                        {
                            flag = false;
                        }
                    }

                if (flag)
                {
                    Type t = property.PropertyType;
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                        t = property.PropertyType.GetGenericArguments()[0];
                    }
                    dt.Columns.Add(new DataColumn("Total_" + property.Name, t));
                    dt.Columns["Total_" + property.Name].AllowDBNull = true;
                }
            }
            return dt;
        }
        public DataTable ToDataTable()
        {
            DataTable dt = GetTable();

            DataTable dtcab = new DataTable("Cabecera");
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                Type propType = Nullable.GetUnderlyingType(property.PropertyType);
                bool isNullable = (propType != null);
                //if (!isNullable)

                Type t = property.PropertyType;
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                    t = property.PropertyType.GetGenericArguments()[0];
                }


                dtcab.Columns.Add(new DataColumn("Cabecera_" + property.Name, t));
                dtcab.Columns["Cabecera_" + property.Name].AllowDBNull = true;

            }


            DataTable dtrutfac = new DataTable("Rutafactura");
            foreach (PropertyInfo property in typeof(Rutaxfactura).GetProperties())
            {
                Type propType = Nullable.GetUnderlyingType(property.PropertyType);
                bool isNullable = (propType != null);
                //  if (!isNullable)
                Type t = property.PropertyType;
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                    t = property.PropertyType.GetGenericArguments()[0];
                }
                dtrutfac.Columns.Add(new DataColumn("Rutafactura_" + property.Name, t));
                dtrutfac.Columns["Rutafactura_" + property.Name].AllowDBNull = true;
            }


            DataTable dtdcomdoc = new DataTable("Dcomdoc");
            foreach (PropertyInfo property in typeof(Dcomdoc).GetProperties())
            {
                Type propType = Nullable.GetUnderlyingType(property.PropertyType);
                bool isNullable = (propType != null);
                // if (!isNullable)
                Type t = property.PropertyType;
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                    t = property.PropertyType.GetGenericArguments()[0];
                }
                dtdcomdoc.Columns.Add(new DataColumn("Dcomdoc_" + property.Name, t));
                dtdcomdoc.Columns["Dcomdoc_" + property.Name].AllowDBNull = true;
            }


            DataTable dtccomenv = new DataTable("Ccomenv");
            foreach (PropertyInfo property in typeof(Ccomenv).GetProperties())
            {
                Type propType = Nullable.GetUnderlyingType(property.PropertyType);
                bool isNullable = (propType != null);
                //  if (!isNullable)
                Type t = property.PropertyType;
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                    t = property.PropertyType.GetGenericArguments()[0];
                }
                dtccomenv.Columns.Add(new DataColumn("Ccomenv_" + property.Name, t));
                dtccomenv.Columns["Ccomenv_" + property.Name].AllowDBNull = true;
            }

            DataTable dtccomdoc = new DataTable("Ccomdoc");
            foreach (PropertyInfo property in typeof(Ccomdoc).GetProperties())
            {
                Type propType = Nullable.GetUnderlyingType(property.PropertyType);
                bool isNullable = (propType != null);
                //    if (!isNullable)
                Type t = property.PropertyType;
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                    t = property.PropertyType.GetGenericArguments()[0];
                }
                dtccomdoc.Columns.Add(new DataColumn("Ccomdoc_" + property.Name, t));
                dtccomdoc.Columns["Ccomdoc_" + property.Name].AllowDBNull = true;

            }

            DataTable dttot = new DataTable("Total");
            foreach (PropertyInfo property in typeof(Total).GetProperties())
            {
                Type propType = Nullable.GetUnderlyingType(property.PropertyType);
                bool isNullable = (propType != null);
                Type t = property.PropertyType;
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                    t = property.PropertyType.GetGenericArguments()[0];
                }
                dttot.Columns.Add(new DataColumn("Total_" + property.Name, t));
                dttot.Columns["Total_" + property.Name].AllowDBNull = true;
            }

            DataRow row = dtcab.NewRow();
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                if (property.GetValue(this, null) == null)
                    row["Cabecera_" + property.Name] = DBNull.Value;
                else
                    row["Cabecera_" + property.Name] = property.GetValue(this, null);
            }
            dtcab.Rows.Add(row);

            DataRow rowcomenv = dtccomenv.NewRow();
            foreach (PropertyInfo property in this.ccomenv.GetProperties())
            {
                if (property.GetValue(ccomenv, null) == null)
                    rowcomenv["Ccomenv_" + property.Name] = DBNull.Value;
                else
                    rowcomenv["Ccomenv_" + property.Name] = property.GetValue(ccomenv, null);
            }
            dtccomenv.Rows.Add(rowcomenv);

            DataRow rowcomdoc = dtccomdoc.NewRow();
            foreach (PropertyInfo property in this.ccomdoc.GetProperties())
            {
                if (property.GetValue(ccomdoc, null) == null)
                    rowcomdoc["Ccomdoc_" + property.Name] = DBNull.Value;
                else
                    rowcomdoc["Ccomdoc_" + property.Name] = property.GetValue(ccomdoc, null);
            }
            dtccomdoc.Rows.Add(rowcomdoc);


            foreach (var item in rutafactura)
            {
                DataRow rowrutafac = dtrutfac.NewRow();
                foreach (PropertyInfo property in item.GetProperties())
                {
                    if (property.GetValue(item, null) == null)
                        rowrutafac["Rutafactura_" + property.Name] = DBNull.Value;
                    else
                        rowrutafac["Rutafactura_" + property.Name] = property.GetValue(item, null);
                }
                dtrutfac.Rows.Add(rowrutafac);
            }

            foreach (var item in this.ccomdoc.detalle)
            {
                DataRow rowdcomdoc = dtdcomdoc.NewRow();
                foreach (PropertyInfo property in item.GetProperties())
                {
                    if (property.GetValue(item, null) == null)
                        rowdcomdoc["Dcomdoc_" + property.Name] = DBNull.Value;
                    else
                        rowdcomdoc["Dcomdoc_" + property.Name] = property.GetValue(item, null);
                }
                dtdcomdoc.Rows.Add(rowdcomdoc);
            }






            DataRow rowt = dttot.NewRow();
            foreach (PropertyInfo property in this.total.GetProperties())
            {
                if (property.GetValue(total, null) == null)
                    rowt["Total_" + property.Name] = DBNull.Value;
                else
                    rowt["Total_" + property.Name] = property.GetValue(total, null);
            }
            dttot.Rows.Add(rowt);
            DataTable dt2 = new DataTable();

            dt2.Columns.Add("Cabecera_com_codigo");
            dt2.Columns.Add("Cabecera_com_fecha");
            dt2.Columns.Add("Cabecera_com_empresa");

            dt2.Columns.Add("Ccomenv_cenv_comprobante");
            dt2.Columns.Add("Ccomenv_cenv_nombres_rem");
            dt2.Columns.Add("Ccomenv_cenv_apellidos_rem");


            dt2.Columns.Add("Ccomdoc_cdoc_nombre");
            dt2.Columns.Add("Ccomdoc_cdoc_ced_ruc");
            dt2.Columns.Add("Ccomdoc_cdoc_direccion");


            dt2.Columns.Add("Ccomenv_cenv_nombres_des");
            dt2.Columns.Add("Ccomenv_cenv_apellidos_des");
            dt2.Columns.Add("Ccomenv_cenv_direccion_des");
            dt2.Columns.Add("Ccomenv_cenv_observacion");

            dt2.Columns.Add("Dcomdoc_ddoc_comprobante");
            dt2.Columns["Dcomdoc_ddoc_comprobante"].AllowDBNull = true;
            dt2.Columns.Add("Dcomdoc_ddoc_cantidad");
            dt2.Columns["Dcomdoc_ddoc_cantidad"].AllowDBNull = true;
            dt2.Columns.Add("Dcomdoc_ddoc_productonombre");
             dt2.Columns.Add("Dcomdoc_ddoc_observaciones");
            
            dt2.Columns.Add("Dcomdoc_ddoc_precio");
            dt2.Columns["Dcomdoc_ddoc_precio"].AllowDBNull = true;
            dt2.Columns.Add("Dcomdoc_ddoc_total");
            dt2.Columns["Dcomdoc_ddoc_total"].AllowDBNull = true;

            dt2.Columns.Add("Total_tot_comprobante");
            dt2.Columns["Total_tot_comprobante"].AllowDBNull = true;
            dt2.Columns.Add("Total_tot_subtotal");
            dt2.Columns["Total_tot_subtotal"].AllowDBNull = true;
            dt2.Columns.Add("Total_tot_subtot_0");
            dt2.Columns["Total_tot_subtot_0"].AllowDBNull = true;
            dt2.Columns.Add("Total_tot_timpuesto");
            dt2.Columns["Total_tot_timpuesto"].AllowDBNull = true;
            dt2.Columns.Add("Total_tot_total");
            dt2.Columns["Total_tot_total"].AllowDBNull = true;
            dt2.Columns.Add("Total_tot_tseguro");
            dt2.Columns["Total_tot_tseguro"].AllowDBNull = true;
            dt2.Columns.Add("Total_tot_vseguro");
            dt2.Columns["Total_tot_vseguro"].AllowDBNull = true;
            dt2.Columns.Add("Total_tot_transporte");
            dt2.Columns["Total_tot_transporte"].AllowDBNull = true;


            //====== Wrting query for join between two data tables ===========================

            var result = from dataRows1 in dtcab.AsEnumerable()
                         join dataRows2 in dtccomdoc.AsEnumerable()
                         on dataRows1.Field<long>("Cabecera_com_codigo") equals dataRows2.Field<long>("Ccomdoc_cdoc_comprobante")
                         join dataRows3 in dttot.AsEnumerable()
                         on dataRows1.Field<long>("Cabecera_com_codigo") equals dataRows3.Field<long>("Total_tot_comprobante")
                         join dataRows4 in dtccomenv.AsEnumerable()
                         on dataRows1.Field<long>("Cabecera_com_codigo") equals dataRows4.Field<long>("Ccomenv_cenv_comprobante")
                         join dataRows5 in dtdcomdoc.AsEnumerable()
                         on dataRows1.Field<long>("Cabecera_com_codigo") equals dataRows5.Field<long>("Dcomdoc_ddoc_comprobante")
                         /*       join dataRows6 in dtrutfac.AsEnumerable()
                                 on dataRows1.Field<long>("Cabecera_com_codigo") equals dataRows6.Field<long>("Rutafactura_rfac_comprobanteruta")
                           */
                         select dt2.LoadDataRow(new object[]
                        { 
  
                          dataRows1.Field<long>("Cabecera_com_codigo"),
                          dataRows1.Field<DateTime>("Cabecera_com_fecha"),
                           dataRows1.Field<int>("Cabecera_com_empresa"),                            
                         
                           
                           
                         dataRows4.Field<long>("Ccomenv_cenv_comprobante"),
                          dataRows4.Field<string>("Ccomenv_cenv_nombres_rem"),
                       dataRows4.Field<string>("Ccomenv_cenv_apellidos_rem"),
                        

                        dataRows2.Field<string>("Ccomdoc_cdoc_nombre"),
                         dataRows2.Field<string>("Ccomdoc_cdoc_ced_ruc"),
                          dataRows2.Field<string>("Ccomdoc_cdoc_direccion"),
                         


                        dataRows4.Field<string>("Ccomenv_cenv_nombres_des"),
                       dataRows4.Field<string>("Ccomenv_cenv_apellidos_des"),
                        dataRows4.Field<string>("Ccomenv_cenv_direccion_des"),

                      dataRows4.Field<string>("Ccomenv_cenv_observacion"),

                         dataRows5.Field<long>("Dcomdoc_ddoc_comprobante"),
                       dataRows5.Field<decimal>("Dcomdoc_ddoc_cantidad"),
                         dataRows5.Field<string>("Dcomdoc_ddoc_productonombre"),
                         dataRows5.Field<string>("Dcomdoc_ddoc_observaciones"),
                          dataRows5.Field<decimal>("Dcomdoc_ddoc_precio"),
                         dataRows5.Field<decimal>("Dcomdoc_ddoc_total"),
                     
                            
                       dataRows3.Field<long>("Total_tot_comprobante"),
                         dataRows3.Field<decimal>("Total_tot_subtotal"),                           
                        dataRows3.Field<decimal>("Total_tot_subtot_0"),
                       dataRows3.Field<decimal>("Total_tot_timpuesto"),
                       dataRows3.Field<decimal>("Total_tot_total"),                                              

                         dataRows3.Field<decimal>("Total_tot_tseguro"),
                       dataRows3.Field<decimal>("Total_tot_vseguro"),                                              
                       dataRows3.Field<decimal>("Total_tot_transporte"), 

                       }, false);




            //==== copy output of result into datatable named 'dt' =============================

            return result.CopyToDataTable(); // This point records will be loaded in dt data table



        }
        public DataTable ToDataTableHR()
        {
            DataTable dt = GetTable();

            DataTable dtcab = new DataTable("Cabecera");
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                Type propType = Nullable.GetUnderlyingType(property.PropertyType);
                bool isNullable = (propType != null);
                //if (!isNullable)

                Type t = property.PropertyType;
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                    t = property.PropertyType.GetGenericArguments()[0];
                }


                dtcab.Columns.Add(new DataColumn("Cabecera_" + property.Name, t));
                dtcab.Columns["Cabecera_" + property.Name].AllowDBNull = true;

            }


            DataTable dtrutfac = new DataTable("Rutaxfactura");
            foreach (PropertyInfo property in typeof(Rutaxfactura).GetProperties())
            {
                Type propType = Nullable.GetUnderlyingType(property.PropertyType);
                bool isNullable = (propType != null);
                //  if (!isNullable)
                Type t = property.PropertyType;
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                    t = property.PropertyType.GetGenericArguments()[0];
                }
                dtrutfac.Columns.Add(new DataColumn("Rutaxfactura_" + property.Name, t));
                dtrutfac.Columns["Rutaxfactura_" + property.Name].AllowDBNull = true;
            }


            DataTable dtdcomdoc = new DataTable("Dcomdoc");
            foreach (PropertyInfo property in typeof(Dcomdoc).GetProperties())
            {
                Type propType = Nullable.GetUnderlyingType(property.PropertyType);
                bool isNullable = (propType != null);
                // if (!isNullable)
                Type t = property.PropertyType;
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                    t = property.PropertyType.GetGenericArguments()[0];
                }
                dtdcomdoc.Columns.Add(new DataColumn("Dcomdoc_" + property.Name, t));
                dtdcomdoc.Columns["Dcomdoc_" + property.Name].AllowDBNull = true;
            }


            DataTable dtccomenv = new DataTable("Ccomenv");
            foreach (PropertyInfo property in typeof(Ccomenv).GetProperties())
            {
                Type propType = Nullable.GetUnderlyingType(property.PropertyType);
                bool isNullable = (propType != null);
                //  if (!isNullable)
                Type t = property.PropertyType;
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                    t = property.PropertyType.GetGenericArguments()[0];
                }
                dtccomenv.Columns.Add(new DataColumn("Ccomenv_" + property.Name, t));
                dtccomenv.Columns["Ccomenv_" + property.Name].AllowDBNull = true;
            }

            DataTable dtccomdoc = new DataTable("Ccomdoc");
            foreach (PropertyInfo property in typeof(Ccomdoc).GetProperties())
            {
                Type propType = Nullable.GetUnderlyingType(property.PropertyType);
                bool isNullable = (propType != null);
                //    if (!isNullable)
                Type t = property.PropertyType;
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                    t = property.PropertyType.GetGenericArguments()[0];
                }
                dtccomdoc.Columns.Add(new DataColumn("Ccomdoc_" + property.Name, t));
                dtccomdoc.Columns["Ccomdoc_" + property.Name].AllowDBNull = true;

            }

            DataTable dttot = new DataTable("Total");
            foreach (PropertyInfo property in typeof(Total).GetProperties())
            {
                Type propType = Nullable.GetUnderlyingType(property.PropertyType);
                bool isNullable = (propType != null);
                Type t = property.PropertyType;
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                    t = property.PropertyType.GetGenericArguments()[0];
                }
                dttot.Columns.Add(new DataColumn("Total_" + property.Name, t));
                dttot.Columns["Total_" + property.Name].AllowDBNull = true;
            }

            DataRow row = dtcab.NewRow();
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                if (property.GetValue(this, null) == null)
                    row["Cabecera_" + property.Name] = DBNull.Value;
                else
                    row["Cabecera_" + property.Name] = property.GetValue(this, null);
            }
            dtcab.Rows.Add(row);






            foreach (var item in rutafactura)
            {
                DataRow rowrutafac = dtrutfac.NewRow();
                foreach (PropertyInfo property in item.GetProperties())
                {
                    if (property.GetValue(item, null) == null)
                        rowrutafac["Rutaxfactura_" + property.Name] = DBNull.Value;
                    else
                        rowrutafac["Rutaxfactura_" + property.Name] = property.GetValue(item, null);
                }
                dtrutfac.Rows.Add(rowrutafac);
            }








            DataRow rowt = dttot.NewRow();
            foreach (PropertyInfo property in this.total.GetProperties())
            {
                if (property.GetValue(total, null) == null)
                    rowt["Total_" + property.Name] = DBNull.Value;
                else
                    rowt["Total_" + property.Name] = property.GetValue(total, null);
            }
            dttot.Rows.Add(rowt);
            DataTable dt2 = new DataTable();

            dt2.Columns.Add("Cabecera_com_codigo");
            dt2.Columns["Cabecera_com_codigo"].AllowDBNull = true;
            dt2.Columns.Add("Cabecera_com_fecha");
            dt2.Columns["Cabecera_com_fecha"].AllowDBNull = true;
            dt2.Columns.Add("Cabecera_com_doctran");
            dt2.Columns["Cabecera_com_doctran"].AllowDBNull = true;
            dt2.Columns.Add("Cabecera_com_empresa");
            dt2.Columns["Cabecera_com_empresa"].AllowDBNull = true;
            dt2.Columns.Add("Cabecera_com_nombresocio");
            dt2.Columns.Add("Cabecera_com_nombreruta");
            dt2.Columns.Add("Cabecera_com_nrodisco");
            dt2.Columns.Add("Cabecera_com_placa");

            dt2.Columns.Add("Rutaxfactura_rfac_fac_total");
            dt2.Columns["Rutaxfactura_rfac_fac_total"].AllowDBNull = true;
            dt2.Columns.Add("Rutaxfactura_rfac_fac_transporte");
            dt2.Columns["Rutaxfactura_rfac_fac_transporte"].AllowDBNull = true;
            dt2.Columns.Add("Rutaxfactura_rfac_fac_timpuesto");
            dt2.Columns["Rutaxfactura_rfac_fac_timpuesto"].AllowDBNull = true;
            dt2.Columns.Add("Rutaxfactura_rfac_fac_tseguro");
            dt2.Columns["Rutaxfactura_rfac_fac_tseguro"].AllowDBNull = true;
            dt2.Columns.Add("Rutaxfactura_rfac_fac_comdoctran");
            dt2.Columns.Add("Rutaxfactura_rfac_fac_nombres_rem");
            dt2.Columns.Add("Rutaxfactura_rfac_fac_nombres_des");
            dt2.Columns.Add("Total_tot_total");
            dt2.Columns["Total_tot_total"].AllowDBNull = true;
            dt2.Columns.Add("Total_tot_transporte");
            dt2.Columns["Total_tot_transporte"].AllowDBNull = true;
            dt2.Columns.Add("Total_tot_tseguro");
            dt2.Columns["Total_tot_tseguro"].AllowDBNull = true;
            dt2.Columns.Add("Total_tot_timpuesto");
            dt2.Columns["Total_tot_timpuesto"].AllowDBNull = true;

            //====== Wrting query for join between two data tables ===========================

            var result = from dataRows1 in dtcab.AsEnumerable()
                         join dataRows3 in dttot.AsEnumerable()
                         on dataRows1.Field<long>("Cabecera_com_codigo") equals dataRows3.Field<long>("Total_tot_comprobante")
                         join dataRows6 in dtrutfac.AsEnumerable()
                         on dataRows1.Field<long>("Cabecera_com_codigo") equals dataRows6.Field<long>("Rutaxfactura_rfac_comprobanteruta")
                         select dt2.LoadDataRow(new object[]
                        { 
  
                        dataRows1.Field<long?>("Cabecera_com_codigo"),
                        dataRows1.Field<DateTime?>("Cabecera_com_fecha"),
                        dataRows1.Field<string>("Cabecera_com_doctran"),
                        dataRows1.Field<int?>("Cabecera_com_empresa"),  



                        dataRows1.Field<string>("Cabecera_com_nombresocio"),
                        dataRows1.Field<string>("Cabecera_com_nombreruta"),
                        dataRows1.Field<string>("Cabecera_com_nrodisco"),
                        dataRows1.Field<string>("Cabecera_com_placa"),



                        dataRows6.Field<decimal?>("Rutaxfactura_rfac_fac_total"),
                        dataRows6.Field<decimal?>("Rutaxfactura_rfac_fac_transporte"),
                        dataRows6.Field<decimal?>("Rutaxfactura_rfac_fac_timpuesto"),
                        dataRows6.Field<decimal?>("Rutaxfactura_rfac_fac_tseguro"),
                        dataRows6.Field<string>("Rutaxfactura_rfac_fac_comdoctran"),
                        dataRows6.Field<string>("Rutaxfactura_rfac_fac_nombres_rem"),
                        dataRows6.Field<string>("Rutaxfactura_rfac_fac_nombres_des"),
                        dataRows3.Field<decimal?>("Total_tot_total"),
                         dataRows3.Field<decimal?>("Total_tot_transporte"),
                          dataRows3.Field<decimal?>("Total_tot_timpuesto"),
                           dataRows3.Field<decimal?>("Total_tot_tseguro"),
                                             
                       }, false);




            //==== copy output of result into datatable named 'dt' =============================

            return result.CopyToDataTable(); // This point records will be loaded in dt data table



        }

        public List<Comprobante> GetStruc()
        {
            return new List<Comprobante>();
        }


        #endregion


    }


}
