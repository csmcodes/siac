using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Drecibo
    {
        #region Properties

        [Data(key = true)]
        public Int32 dfp_empresa { get; set; }
        [Data(originalkey = true)]
        public Int32 dfp_empresa_key { get; set; }
        [Data(key = true)]
        public Int64 dfp_comprobante { get; set; }
        [Data(originalkey = true)]
        public Int64 dfp_comprobante_key { get; set; }
        [Data(key = true)]
        public Int32 dfp_secuencia { get; set; }
        [Data(originalkey = true)]
        public Int32 dfp_secuencia_key { get; set; }
        public Int32 dfp_tipopago { get; set; }
        public Decimal dfp_monto { get; set; }
        public Decimal dfp_monto_ext { get; set; }
        public Decimal dfp_tipo_cambio { get; set; }
        public Int32 dfp_debcre { get; set; }
        public Int32? dfp_tarjeta { get; set; }
        public Int32? dfp_banco { get; set; }
        public String dfp_nro_cheque { get; set; }
        public String dfp_beneficiario { get; set; }
        public DateTime? dfp_fecha_ven { get; set; }
        public Int32? dfp_cuenta { get; set; }
        public Int64? dfp_ref_comprobante { get; set; }
         [Data(noupdate = true)]
        public String crea_usr { get; set; }
         [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
        public String dfp_nro_documento { get; set; }
        public String dfp_nro_cuenta { get; set; }
        public String dfp_emisor { get; set; }
        public Int32? dfp_tclipro { get; set; }



        [Data(nosql = true, tablaref = "tipopago", camporef = "tpa_id", foreign = "dfp_empresa, dfp_tipopago", keyref = "tpa_empresa, tpa_codigo", join = "left")]
        public string dfp_tipopagoid { get; set; }
        [Data(nosql = true, tablaref = "tipopago", camporef = "tpa_nombre", foreign = "dfp_empresa, dfp_tipopago", keyref = "tpa_empresa, tpa_codigo", join = "left")]
        public string dfp_tipopagonombre { get; set; }
        [Data(nosql = true, tablaref = "tipopago", camporef = "tpa_iva", foreign = "dfp_empresa, dfp_tipopago", keyref = "tpa_empresa, tpa_codigo", join = "left")]
        public int? dfp_tipopagoiva { get; set; }
        [Data(nosql = true, tablaref = "tipopago", camporef = "tpa_ret", foreign = "dfp_empresa, dfp_tipopago", keyref = "tpa_empresa, tpa_codigo", join = "left")]
        public int? dfp_tipopagoret { get; set; }



        
        [Data(nosql = true, tablaref = "comprobante", camporef = "com_doctran", foreign = "dfp_empresa, dfp_comprobante", keyref = "com_empresa, com_codigo", join = "left")]
        public string dfp_comprobantedoctran { get; set; }
        [Data(nosql = true, tablaref = "comprobante", camporef = "com_claveelec", foreign = "dfp_empresa, dfp_comprobante", keyref = "com_empresa, com_codigo", join = "left")]
        public string dfp_comprobanteclaveelec { get; set; }


        #endregion

        #region Constructors


        public Drecibo()
        {
        }

        public Drecibo(Int32 dfp_empresa, Int64 dfp_comprobante, Int32 dfp_secuencia, Int32 dfp_tipopago, Decimal dfp_monto, Decimal dfp_monto_ext, Decimal dfp_tipo_cambio, Int32 dfp_debcre, Int32 dfp_tarjeta, Int32 dfp_banco, String dfp_nro_cheque, String dfp_beneficiario, DateTime dfp_fecha_ven, Int32 dfp_cuenta, Int64 dfp_ref_comprobante, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha, String dfp_nro_documento, String dfp_nro_cuenta, String dfp_emisor, Int32 dfp_tclipro)
        {
            this.dfp_empresa = dfp_empresa;
            this.dfp_comprobante = dfp_comprobante;
            this.dfp_secuencia = dfp_secuencia;
            this.dfp_empresa_key = dfp_empresa;
            this.dfp_comprobante_key = dfp_comprobante;
            this.dfp_secuencia_key = dfp_secuencia;
            this.dfp_tipopago = dfp_tipopago;
            this.dfp_monto = dfp_monto;
            this.dfp_monto_ext = dfp_monto_ext;
            this.dfp_tipo_cambio = dfp_tipo_cambio;
            this.dfp_debcre = dfp_debcre;
            this.dfp_tarjeta = dfp_tarjeta;
            this.dfp_banco = dfp_banco;
            this.dfp_nro_cheque = dfp_nro_cheque;
            this.dfp_beneficiario = dfp_beneficiario;
            this.dfp_fecha_ven = dfp_fecha_ven;
            this.dfp_cuenta = dfp_cuenta;
            this.dfp_ref_comprobante = dfp_ref_comprobante;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;
            this.dfp_nro_documento = dfp_nro_documento;
            this.dfp_nro_cuenta = dfp_nro_cuenta;
            this.dfp_emisor = dfp_emisor;
            this.dfp_tclipro = dfp_tclipro;


        }

        public Drecibo(IDataReader reader)
        {
            this.dfp_empresa = (Int32)reader["dfp_empresa"];
            this.dfp_comprobante = (Int64)reader["dfp_comprobante"];
            this.dfp_secuencia = (Int32)reader["dfp_secuencia"];
            this.dfp_empresa_key = (Int32)reader["dfp_empresa"];
            this.dfp_comprobante_key = (Int64)reader["dfp_comprobante"];
            this.dfp_secuencia_key = (Int32)reader["dfp_secuencia"];
            this.dfp_tipopago = (Int32)reader["dfp_tipopago"];
            this.dfp_monto = (Decimal)reader["dfp_monto"];
            this.dfp_monto_ext = (Decimal)reader["dfp_monto_ext"];
            this.dfp_tipo_cambio = (Decimal)reader["dfp_tipo_cambio"];
            this.dfp_debcre = (Int32)reader["dfp_debcre"];
            this.dfp_tarjeta = (reader["dfp_tarjeta"] != DBNull.Value) ? (Int32?)reader["dfp_tarjeta"] : null;
            this.dfp_banco = (reader["dfp_banco"] != DBNull.Value) ? (Int32?)reader["dfp_banco"] : null;
            this.dfp_nro_cheque = reader["dfp_nro_cheque"].ToString();
            this.dfp_beneficiario = reader["dfp_beneficiario"].ToString();
            this.dfp_fecha_ven = (reader["dfp_fecha_ven"] != DBNull.Value) ? (DateTime?)reader["dfp_fecha_ven"] : null;
            this.dfp_cuenta = (reader["dfp_cuenta"] != DBNull.Value) ? (Int32?)reader["dfp_cuenta"] : null;
            this.dfp_ref_comprobante = (reader["dfp_ref_comprobante"] != DBNull.Value) ? (Int64?)reader["dfp_ref_comprobante"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.dfp_nro_documento = reader["dfp_nro_documento"].ToString();
            this.dfp_nro_cuenta = reader["dfp_nro_cuenta"].ToString();
            this.dfp_emisor = reader["dfp_emisor"].ToString();
            this.dfp_tclipro = (reader["dfp_tclipro"] != DBNull.Value) ? (Int32?)reader["dfp_tclipro"] : null;

            this.dfp_tipopagoid = reader["dfp_tipopagoid"].ToString();
            this.dfp_tipopagonombre = reader["dfp_tipopagonombre"].ToString();
            this.dfp_tipopagoiva= (reader["dfp_tipopagoiva"] != DBNull.Value) ? (Int32?)reader["dfp_tipopagoiva"] : null;
            this.dfp_tipopagoret= (reader["dfp_tipopagoret"] != DBNull.Value) ? (Int32?)reader["dfp_tipopagoret"] : null;

            this.dfp_comprobantedoctran= reader["dfp_comprobantedoctran"].ToString();
            this.dfp_comprobanteclaveelec = reader["dfp_comprobanteclaveelec"].ToString();
        }


        public Drecibo(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object dfp_empresa = null;
                object dfp_comprobante = null;
                object dfp_secuencia = null;
                object dfp_empresa_key = null;
                object dfp_comprobante_key = null;
                object dfp_secuencia_key = null;
                object dfp_tipopago = null;
                object dfp_monto = null;
                object dfp_monto_ext = null;
                object dfp_tipo_cambio = null;
                object dfp_debcre = null;
                object dfp_tarjeta = null;
                object dfp_banco = null;
                object dfp_nro_cheque = null;
                object dfp_beneficiario = null;
                object dfp_fecha_ven = null;
                object dfp_cuenta = null;
                object dfp_ref_comprobante = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object dfp_nro_documento = null;
                object dfp_nro_cuenta = null;
                object dfp_emisor = null;
                object dfp_tclipro = null;


                tmp.TryGetValue("dfp_empresa", out dfp_empresa);
                tmp.TryGetValue("dfp_comprobante", out dfp_comprobante);
                tmp.TryGetValue("dfp_secuencia", out dfp_secuencia);
                tmp.TryGetValue("dfp_empresa_key", out dfp_empresa_key);
                tmp.TryGetValue("dfp_comprobante_key", out dfp_comprobante_key);
                tmp.TryGetValue("dfp_secuencia_key", out dfp_secuencia_key);
                tmp.TryGetValue("dfp_tipopago", out dfp_tipopago);
                tmp.TryGetValue("dfp_monto", out dfp_monto);
                tmp.TryGetValue("dfp_monto_ext", out dfp_monto_ext);
                tmp.TryGetValue("dfp_tipo_cambio", out dfp_tipo_cambio);
                tmp.TryGetValue("dfp_debcre", out dfp_debcre);
                tmp.TryGetValue("dfp_tarjeta", out dfp_tarjeta);
                tmp.TryGetValue("dfp_banco", out dfp_banco);
                tmp.TryGetValue("dfp_nro_cheque", out dfp_nro_cheque);
                tmp.TryGetValue("dfp_beneficiario", out dfp_beneficiario);
                tmp.TryGetValue("dfp_fecha_ven", out dfp_fecha_ven);
                tmp.TryGetValue("dfp_cuenta", out dfp_cuenta);
                tmp.TryGetValue("dfp_ref_comprobante", out dfp_ref_comprobante);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("dfp_nro_documento", out dfp_nro_documento);
                tmp.TryGetValue("dfp_nro_cuenta", out dfp_nro_cuenta);
                tmp.TryGetValue("dfp_emisor", out dfp_emisor);
                tmp.TryGetValue("dfp_tclipro", out dfp_tclipro);


                this.dfp_empresa = (Int32)Conversiones.GetValueByType(dfp_empresa, typeof(Int32));
                this.dfp_comprobante = (Int64)Conversiones.GetValueByType(dfp_comprobante, typeof(Int64));
                this.dfp_secuencia = (Int32)Conversiones.GetValueByType(dfp_secuencia, typeof(Int32));
                this.dfp_empresa_key = (Int32)Conversiones.GetValueByType(dfp_empresa_key, typeof(Int32));
                this.dfp_comprobante_key = (Int64)Conversiones.GetValueByType(dfp_comprobante_key, typeof(Int64));
                this.dfp_secuencia_key = (Int32)Conversiones.GetValueByType(dfp_secuencia_key, typeof(Int32));
                this.dfp_tipopago = (Int32)Conversiones.GetValueByType(dfp_tipopago, typeof(Int32));
                this.dfp_monto = (Decimal)Conversiones.GetValueByType(dfp_monto, typeof(Decimal));
                this.dfp_monto_ext = (Decimal)Conversiones.GetValueByType(dfp_monto_ext, typeof(Decimal));
                this.dfp_tipo_cambio = (Decimal)Conversiones.GetValueByType(dfp_tipo_cambio, typeof(Decimal));
                this.dfp_debcre = (Int32)Conversiones.GetValueByType(dfp_debcre, typeof(Int32));
                this.dfp_tarjeta = (Int32?)Conversiones.GetValueByType(dfp_tarjeta, typeof(Int32?));
                this.dfp_banco = (Int32?)Conversiones.GetValueByType(dfp_banco, typeof(Int32?));
                this.dfp_nro_cheque = (String)Conversiones.GetValueByType(dfp_nro_cheque, typeof(String));
                this.dfp_beneficiario = (String)Conversiones.GetValueByType(dfp_beneficiario, typeof(String));
                this.dfp_fecha_ven = (DateTime?)Conversiones.GetValueByType(dfp_fecha_ven, typeof(DateTime?));
                this.dfp_cuenta = (Int32?)Conversiones.GetValueByType(dfp_cuenta, typeof(Int32?));
                this.dfp_ref_comprobante = (Int64?)Conversiones.GetValueByType(dfp_ref_comprobante, typeof(Int64?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.dfp_nro_documento = (String)Conversiones.GetValueByType(dfp_nro_documento, typeof(String));
                this.dfp_nro_cuenta = (String)Conversiones.GetValueByType(dfp_nro_cuenta, typeof(String));
                this.dfp_emisor = (String)Conversiones.GetValueByType(dfp_emisor, typeof(String));
                this.dfp_tclipro = (Int32?)Conversiones.GetValueByType(dfp_tclipro, typeof(Int32?));

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
