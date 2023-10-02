using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Ddocumento
    {
        #region Properties

        [Data(key = true)]
        public Int32 ddo_empresa { get; set; }
        [Data(originalkey = true)]
        public Int32 ddo_empresa_key { get; set; }
        [Data(key = true)]
        public Int64 ddo_comprobante { get; set; }
        [Data(originalkey = true)]
        public Int64 ddo_comprobante_key { get; set; }
        [Data(key = true)]
        public Int32 ddo_transacc { get; set; }
        [Data(originalkey = true)]
        public Int32 ddo_transacc_key { get; set; }
        [Data(key = true)]
        public String ddo_doctran { get; set; }
        [Data(originalkey = true)]
        public String ddo_doctran_key { get; set; }
        [Data(key = true)]
        public Int32 ddo_pago { get; set; }
        [Data(originalkey = true)]
        public Int32 ddo_pago_key { get; set; }
        public Int32? ddo_codclipro { get; set; }
        public Int32? ddo_debcre { get; set; }
        public Int32? ddo_tipo_cambio { get; set; }
        public DateTime? ddo_fecha_emi { get; set; }
        public DateTime? ddo_fecha_ven { get; set; }
        public Decimal? ddo_monto { get; set; }
        public Decimal? ddo_monto_ext { get; set; }
        public Decimal? ddo_cancela { get; set; }
        public Decimal? ddo_cancela_ext { get; set; }
        public Int32? ddo_cancelado { get; set; }
        public Int32? ddo_agente { get; set; }
        public Int32? ddo_cuenta { get; set; }
        public Int32? ddo_modulo { get; set; }
        [Data(noupdate = true)]
        public String crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
        public Int64? ddo_comprobante_guia { get; set; }


        [Data(nosql = true, tablaref = "cuenta", camporef = "cue_id", foreign = "ddo_empresa, ddo_cuenta", keyref = "cue_empresa, cue_codigo", join = "left")]
        public string ddo_cuentaid { get; set; }
        [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "ddo_empresa, ddo_cuenta", keyref = "cue_empresa, cue_codigo", join = "left")]
        public string ddo_cuentanombre { get; set; }
        [Data(nosql = true, tablaref = "cuenta", camporef = "cue_modulo", foreign = "ddo_empresa, ddo_cuenta", keyref = "cue_empresa, cue_codigo", join = "left")]
        public Int32? ddo_cuentamodulo { get; set; }


        [Data(nosql = true, tablaref = "comprobante", camporef = "com_fecha", foreign = "ddo_empresa, ddo_comprobante", keyref = "com_empresa, com_codigo", join = "inner")]
        public DateTime? ddo_comprobantefecha { get; set; }
        [Data(nosql = true, tablaref = "comprobante", camporef = "com_doctran", foreign = "ddo_empresa, ddo_comprobante", keyref = "com_empresa, com_codigo", join = "inner")]
        public string ddo_compdoctran { get; set; }




        [Data(nosql = true, tablaref = "persona", camporef = "per_id", foreign = "ddo_empresa, ddo_codclipro", keyref = "per_empresa, per_codigo", join = "left")]
        public string ddo_clienteid { get; set; }
        [Data(nosql = true, tablaref = "persona", camporef = "per_nombres", foreign = "ddo_empresa, ddo_codclipro", keyref = "per_empresa, per_codigo", join = "left")]
        public string ddo_clientenombres { get; set; }
        [Data(nosql = true, tablaref = "persona", camporef = "per_apellidos", foreign = "ddo_empresa, ddo_codclipro", keyref = "per_empresa, per_codigo", join = "left")]
        public string ddo_clienteapellidos { get; set; }

        //[Data(nosql = true, tablaref = "almacen", camporef = "alm_id", foreign = "dco_empresa, dco_almacen", keyref = "alm_empresa, alm_codigo", join = "left")]
        //public string dco_almacenid { get; set; }
        //[Data(nosql = true, tablaref = "almacen", camporef = "alm_nombre", foreign = "dco_empresa, dco_almacen", keyref = "alm_empresa, alm_codigo", join = "left")]
        //public string dco_almacennombre { get; set; }

        //[Data(nosql = true, tablaref = "centro", camporef = "cen_id", foreign = "dco_empresa, dco_centro", keyref = "cen_empresa, cen_codigo", join = "left")]
        //public string dco_centroid { get; set; }
        //[Data(nosql = true, tablaref = "centro", camporef = "cen_nombre", foreign = "dco_empresa, dco_centro", keyref = "cen_empresa, cen_codigo", join = "left")]
        //public string dco_centronombre { get; set; }



        /*[Data(nosql = true, tablaref = "cuenta", camporef = "cue_id", foreign = "ddo_empresa, ddo_cuenta", keyref = "cue_empresa, cue_codigo", join = "left")]
        public string ddo_cuentaid { get; set; }
        [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "dco_empresa, dco_cuenta", keyref = "cue_empresa, cue_codigo", join = "left")]
        public string ddo_cuentanombre { get; set; }
        [Data(nosql = true, tablaref = "cuenta", camporef = "cue_modulo", foreign = "dco_empresa, dco_cuenta", keyref = "cue_empresa, cue_codigo", join = "left")]
        public Int32? ddo_cuentamodulo { get; set; }*/

        #endregion

        #region Constructors


        public Ddocumento()
        {
        }

        public Ddocumento(Int32 ddo_empresa, Int64 ddo_comprobante, Int32 ddo_transacc, String ddo_doctran, Int32 ddo_pago, Int32 ddo_codclipro, Int32 ddo_debcre, Int32 ddo_tipo_cambio, DateTime ddo_fecha_emi, DateTime ddo_fecha_ven, Decimal ddo_monto, Decimal ddo_monto_ext, Decimal ddo_cancela, Decimal ddo_cancela_ext, Int32 ddo_cancelado, Int32 ddo_agente, Int32 ddo_cuenta, Int32 ddo_modulo,Int64 ddo_comprobante_guia,String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha)
        {
            this.ddo_empresa = ddo_empresa;
            this.ddo_comprobante = ddo_comprobante;
            this.ddo_transacc = ddo_transacc;
            this.ddo_doctran = ddo_doctran;
            this.ddo_pago = ddo_pago;
            this.ddo_empresa_key = ddo_empresa;
            this.ddo_comprobante_key = ddo_comprobante;
            this.ddo_transacc_key = ddo_transacc;
            this.ddo_doctran_key = ddo_doctran;
            this.ddo_pago_key = ddo_pago;
            this.ddo_codclipro = ddo_codclipro;
            this.ddo_debcre = ddo_debcre;
            this.ddo_tipo_cambio = ddo_tipo_cambio;
            this.ddo_fecha_emi = ddo_fecha_emi;
            this.ddo_fecha_ven = ddo_fecha_ven;
            this.ddo_monto = ddo_monto;
            this.ddo_monto_ext = ddo_monto_ext;
            this.ddo_cancela = ddo_cancela;
            this.ddo_cancela_ext = ddo_cancela_ext;
            this.ddo_cancelado = ddo_cancelado;
            this.ddo_agente = ddo_agente;
            this.ddo_cuenta = ddo_cuenta;
            this.ddo_modulo = ddo_modulo;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;
            this.ddo_comprobante_guia = ddo_comprobante_guia;

        }

        public Ddocumento(IDataReader reader)
        {
            this.ddo_empresa = (Int32)reader["ddo_empresa"];
            this.ddo_comprobante = (Int64)reader["ddo_comprobante"];
            this.ddo_transacc = (Int32)reader["ddo_transacc"];
            this.ddo_doctran = reader["ddo_doctran"].ToString();
            this.ddo_pago = (Int32)reader["ddo_pago"];
            this.ddo_empresa_key = (Int32)reader["ddo_empresa"];
            this.ddo_comprobante_key = (Int64)reader["ddo_comprobante"];
            this.ddo_transacc_key = (Int32)reader["ddo_transacc"];
            this.ddo_doctran_key = reader["ddo_doctran"].ToString();
            this.ddo_pago_key = (Int32)reader["ddo_pago"];
            this.ddo_codclipro = (reader["ddo_codclipro"] != DBNull.Value) ? (Int32?)reader["ddo_codclipro"] : null;
            this.ddo_debcre = (reader["ddo_debcre"] != DBNull.Value) ? (Int32?)reader["ddo_debcre"] : null;
            this.ddo_tipo_cambio = (reader["ddo_tipo_cambio"] != DBNull.Value) ? (Int32?)reader["ddo_tipo_cambio"] : null;
            this.ddo_fecha_emi = (reader["ddo_fecha_emi"] != DBNull.Value) ? (DateTime?)reader["ddo_fecha_emi"] : null;
            this.ddo_fecha_ven = (reader["ddo_fecha_ven"] != DBNull.Value) ? (DateTime?)reader["ddo_fecha_ven"] : null;
            this.ddo_monto = (reader["ddo_monto"] != DBNull.Value) ? (Decimal?)reader["ddo_monto"] : null;
            this.ddo_monto_ext = (reader["ddo_monto_ext"] != DBNull.Value) ? (Decimal?)reader["ddo_monto_ext"] : null;
            this.ddo_cancela = (reader["ddo_cancela"] != DBNull.Value) ? (Decimal?)reader["ddo_cancela"] : null;
            this.ddo_cancela_ext = (reader["ddo_cancela_ext"] != DBNull.Value) ? (Decimal?)reader["ddo_cancela_ext"] : null;
            this.ddo_cancelado = (reader["ddo_cancelado"] != DBNull.Value) ? (Int32?)reader["ddo_cancelado"] : null;
            this.ddo_agente = (reader["ddo_agente"] != DBNull.Value) ? (Int32?)reader["ddo_agente"] : null;
            this.ddo_cuenta = (reader["ddo_cuenta"] != DBNull.Value) ? (Int32?)reader["ddo_cuenta"] : null;
            this.ddo_modulo = (reader["ddo_modulo"] != DBNull.Value) ? (Int32?)reader["ddo_modulo"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.ddo_comprobante_guia = (reader["ddo_comprobante_guia"] != DBNull.Value) ? (Int64?)reader["ddo_comprobante_guia"] : null;


            this.ddo_cuentaid = (reader["ddo_cuentaid"] != DBNull.Value) ? (String)reader["ddo_cuentaid"] : null;
            this.ddo_cuentanombre = (reader["ddo_cuentanombre"] != DBNull.Value) ? (String)reader["ddo_cuentanombre"] : null;
            this.ddo_cuentamodulo = (reader["ddo_cuentamodulo"] != DBNull.Value) ? (Int32?)reader["ddo_cuentamodulo"] : null;

            this.ddo_comprobantefecha = (reader["ddo_comprobantefecha"] != DBNull.Value) ? (DateTime?)reader["ddo_comprobantefecha"] : null;
            this.ddo_compdoctran = (reader["ddo_compdoctran"] != DBNull.Value) ? (String)reader["ddo_compdoctran"] : null;

            this.ddo_clienteid = (reader["ddo_clienteid"] != DBNull.Value) ? (String)reader["ddo_clienteid"] : null;
            this.ddo_clientenombres = (reader["ddo_clientenombres"] != DBNull.Value) ? (String)reader["ddo_clientenombres"] : null;
            this.ddo_clienteapellidos = (reader["ddo_clienteapellidos"] != DBNull.Value) ? (String)reader["ddo_clienteapellidos"] : null;


        }


        public Ddocumento(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object ddo_empresa = null;
                object ddo_comprobante = null;
                object ddo_transacc = null;
                object ddo_doctran = null;
                object ddo_pago = null;
                object ddo_empresa_key = null;
                object ddo_comprobante_key = null;
                object ddo_transacc_key = null;
                object ddo_doctran_key = null;
                object ddo_pago_key = null;
                object ddo_codclipro = null;
                object ddo_debcre = null;
                object ddo_tipo_cambio = null;
                object ddo_fecha_emi = null;
                object ddo_fecha_ven = null;
                object ddo_monto = null;
                object ddo_monto_ext = null;
                object ddo_cancela = null;
                object ddo_cancela_ext = null;
                object ddo_cancelado = null;
                object ddo_agente = null;
                object ddo_cuenta = null;
                object ddo_modulo = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object ddo_comprobante_guia = null;

                tmp.TryGetValue("ddo_empresa", out ddo_empresa);
                tmp.TryGetValue("ddo_comprobante", out ddo_comprobante);
                tmp.TryGetValue("ddo_transacc", out ddo_transacc);
                tmp.TryGetValue("ddo_doctran", out ddo_doctran);
                tmp.TryGetValue("ddo_pago", out ddo_pago);
                tmp.TryGetValue("ddo_empresa_key", out ddo_empresa_key);
                tmp.TryGetValue("ddo_comprobante_key", out ddo_comprobante_key);
                tmp.TryGetValue("ddo_transacc_key", out ddo_transacc_key);
                tmp.TryGetValue("ddo_doctran_key", out ddo_doctran_key);
                tmp.TryGetValue("ddo_pago_key", out ddo_pago_key);
                tmp.TryGetValue("ddo_codclipro", out ddo_codclipro);
                tmp.TryGetValue("ddo_debcre", out ddo_debcre);
                tmp.TryGetValue("ddo_tipo_cambio", out ddo_tipo_cambio);
                tmp.TryGetValue("ddo_fecha_emi", out ddo_fecha_emi);
                tmp.TryGetValue("ddo_fecha_ven", out ddo_fecha_ven);
                tmp.TryGetValue("ddo_monto", out ddo_monto);
                tmp.TryGetValue("ddo_monto_ext", out ddo_monto_ext);
                tmp.TryGetValue("ddo_cancela", out ddo_cancela);
                tmp.TryGetValue("ddo_cancela_ext", out ddo_cancela_ext);
                tmp.TryGetValue("ddo_cancelado", out ddo_cancelado);
                tmp.TryGetValue("ddo_agente", out ddo_agente);
                tmp.TryGetValue("ddo_cuenta", out ddo_cuenta);
                tmp.TryGetValue("ddo_modulo", out ddo_modulo);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("ddo_comprobante_guia", out ddo_comprobante_guia);
                

                this.ddo_empresa = (Int32)Conversiones.GetValueByType(ddo_empresa, typeof(Int32));
                this.ddo_comprobante = (Int64)Conversiones.GetValueByType(ddo_comprobante, typeof(Int64));
                this.ddo_transacc = (Int32)Conversiones.GetValueByType(ddo_transacc, typeof(Int32));
                this.ddo_doctran = (String)Conversiones.GetValueByType(ddo_doctran, typeof(String));
                this.ddo_pago = (Int32)Conversiones.GetValueByType(ddo_pago, typeof(Int32));
                this.ddo_empresa_key = (Int32)Conversiones.GetValueByType(ddo_empresa_key, typeof(Int32));
                this.ddo_comprobante_key = (Int64)Conversiones.GetValueByType(ddo_comprobante_key, typeof(Int64));
                this.ddo_transacc_key = (Int32)Conversiones.GetValueByType(ddo_transacc_key, typeof(Int32));
                this.ddo_doctran_key = (String)Conversiones.GetValueByType(ddo_doctran_key, typeof(String));
                this.ddo_pago_key = (Int32)Conversiones.GetValueByType(ddo_pago_key, typeof(Int32));
                this.ddo_codclipro = (Int32?)Conversiones.GetValueByType(ddo_codclipro, typeof(Int32?));
                this.ddo_debcre = (Int32?)Conversiones.GetValueByType(ddo_debcre, typeof(Int32?));
                this.ddo_tipo_cambio = (Int32?)Conversiones.GetValueByType(ddo_tipo_cambio, typeof(Int32?));
                this.ddo_fecha_emi = (DateTime?)Conversiones.GetValueByType(ddo_fecha_emi, typeof(DateTime?));
                this.ddo_fecha_ven = (DateTime?)Conversiones.GetValueByType(ddo_fecha_ven, typeof(DateTime?));
                this.ddo_monto = (Decimal?)Conversiones.GetValueByType(ddo_monto, typeof(Decimal?));
                this.ddo_monto_ext = (Decimal?)Conversiones.GetValueByType(ddo_monto_ext, typeof(Decimal?));
                this.ddo_cancela = (Decimal?)Conversiones.GetValueByType(ddo_cancela, typeof(Decimal?));
                this.ddo_cancela_ext = (Decimal?)Conversiones.GetValueByType(ddo_cancela_ext, typeof(Decimal?));
                this.ddo_cancelado = (Int32?)Conversiones.GetValueByType(ddo_cancelado, typeof(Int32?));
                this.ddo_agente = (Int32?)Conversiones.GetValueByType(ddo_agente, typeof(Int32?));
                this.ddo_cuenta = (Int32?)Conversiones.GetValueByType(ddo_cuenta, typeof(Int32?));
                this.ddo_modulo = (Int32?)Conversiones.GetValueByType(ddo_modulo, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.ddo_comprobante_guia = (Int64?)Conversiones.GetValueByType(ddo_comprobante_guia, typeof(Int64?));
                
            }
        }
        #endregion

        #region Methods
        public PropertyInfo[] GetProperties()
        {
            return this.GetType().GetProperties();
        }
        #endregion


    }
}
