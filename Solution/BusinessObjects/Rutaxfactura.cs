using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Rutaxfactura
    {
        #region Properties

        [Data(key = true)]
        public Int32 rfac_empresa { get; set; }
        [Data(originalkey = true)]
        public Int32 rfac_empresa_key { get; set; }
        [Data(key = true)]
        public Int64 rfac_comprobanteruta { get; set; }
        [Data(originalkey = true)]
        public Int64 rfac_comprobanteruta_key { get; set; }
        [Data(key = true)]
        public Int64 rfac_comprobantefac { get; set; }
        [Data(originalkey = true)]
        public Int64 rfac_comprobantefac_key { get; set; }
        public Int32? rfac_estado { get; set; }
        [Data(noupdate = true)]
        public String crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }

        public String mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }


        [Data(nosql = true, tablaref = "comprobante", camporef = "com_doctran", foreign = "rfac_empresa, rfac_comprobanteruta", keyref = "com_empresa, com_codigo")]
        public string rfac_comprobanterutadoctran { get; set; }

       

        /*

        [Data(nosql = true, tablaref = "total", camporef = "tot_total", foreign = "rfac_comprobantefac", keyref = "tot_comprobante")]
        public Decimal? rfac_fac_total { get; set; }
        [Data(nosql = true, tablaref = "total", camporef = "tot_transporte", foreign = "rfac_comprobantefac", keyref = "tot_comprobante")]
        public Decimal? rfac_fac_transporte { get; set; }
        [Data(nosql = true, tablaref = "total", camporef = "tot_timpuesto", foreign = "rfac_comprobantefac", keyref = "tot_comprobante")]
        public Decimal? rfac_fac_timpuesto { get; set; }
        [Data(nosql = true, tablaref = "total", camporef = "tot_tseguro", foreign = "rfac_comprobantefac", keyref = "tot_comprobante")]
        public Decimal? rfac_fac_tseguro { get; set; }
        [Data(nosql = true, tablaref = "comprobante", camporef = "com_doctran", foreign = "rfac_comprobantefac", keyref = "com_codigo")]
        public String rfac_fac_comdoctran { get; set; }

        [Data(nosql = true, tablaref = "ccomenv", camporef = "cenv_nombres_rem", foreign = "rfac_comprobantefac", keyref = "cenv_comprobante")]
        public string rfac_fac_nombres_rem{ get; set; }
        [Data(nosql = true, tablaref = "ccomenv", camporef = "cenv_apellidos_rem", foreign = "rfac_comprobantefac", keyref = "cenv_comprobante")]
        public string rfac_fac_apellidos_rem { get; set; }
        [Data(nosql = true, tablaref = "ccomenv", camporef = "cenv_nombres_des", foreign = "rfac_comprobantefac", keyref = "cenv_comprobante")]
        public string rfac_fac_nombres_des { get; set; }
        [Data(nosql = true, tablaref = "ccomenv", camporef = "cenv_apellidos_des", foreign = "rfac_comprobantefac", keyref = "cenv_comprobante")]
        public string rfac_fac_apellidos_des { get; set; }
        */
        #endregion

        #region Constructors


        public Rutaxfactura()
        {
        }

        public Rutaxfactura(Int32 rfac_empresa, Int64 rfac_comprobanteruta, Int64 rfac_comprobantefac, Int32 rfac_estado, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha)
        {
            this.rfac_empresa = rfac_empresa;
            this.rfac_comprobanteruta = rfac_comprobanteruta;
            this.rfac_comprobantefac = rfac_comprobantefac;
            this.rfac_empresa_key = rfac_empresa;
            this.rfac_comprobanteruta_key = rfac_comprobanteruta;
            this.rfac_comprobantefac_key = rfac_comprobantefac;
            this.rfac_estado = rfac_estado;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;


        }

        public Rutaxfactura(IDataReader reader)
        {
            this.rfac_empresa = (Int32)reader["rfac_empresa"];
            this.rfac_comprobanteruta = (Int64)reader["rfac_comprobanteruta"];
            this.rfac_comprobantefac = (Int64)reader["rfac_comprobantefac"];
            this.rfac_empresa_key = (Int32)reader["rfac_empresa"];
            this.rfac_comprobanteruta_key = (Int64)reader["rfac_comprobanteruta"];
            this.rfac_comprobantefac_key = (Int64)reader["rfac_comprobantefac"];
            this.rfac_estado = (reader["rfac_estado"] != DBNull.Value) ? (Int32?)reader["rfac_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;


            this.rfac_comprobanterutadoctran = reader["rfac_comprobanterutadoctran"].ToString();
            

            /*    this.rfac_fac_total = (reader["rfac_fac_total"] != DBNull.Value) ? (Decimal?)reader["rfac_fac_total"] : null;
                this.rfac_fac_transporte = (reader["rfac_fac_transporte"] != DBNull.Value) ? (Decimal?)reader["rfac_fac_transporte"] : null;
                this.rfac_fac_timpuesto = (reader["rfac_fac_timpuesto"] != DBNull.Value) ? (Decimal?)reader["rfac_fac_timpuesto"] : null;
                this.rfac_fac_tseguro = (reader["rfac_fac_tseguro"] != DBNull.Value) ? (Decimal?)reader["rfac_fac_tseguro"] : null;
                this.rfac_fac_comdoctran = reader["rfac_fac_comdoctran"].ToString();
                this.rfac_fac_nombres_rem = reader["rfac_fac_nombres_rem"].ToString();
                this.rfac_fac_apellidos_rem = reader["rfac_fac_apellidos_rem"].ToString();
                this.rfac_fac_nombres_des = reader["rfac_fac_nombres_des"].ToString();
                this.rfac_fac_apellidos_des = reader["rfac_fac_apellidos_des"].ToString();        */
        }


        public Rutaxfactura(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;

                object rfac_empresa = null;
                object rfac_comprobanteruta = null;
                object rfac_comprobantefac = null;
                object rfac_empresa_key = null;
                object rfac_comprobanteruta_key = null;
                object rfac_comprobantefac_key = null;
                object rfac_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("rfac_empresa", out rfac_empresa);
                tmp.TryGetValue("rfac_comprobanteruta", out rfac_comprobanteruta);
                tmp.TryGetValue("rfac_comprobantefac", out rfac_comprobantefac);
                tmp.TryGetValue("rfac_empresa_key", out rfac_empresa_key);
                tmp.TryGetValue("rfac_comprobanteruta_key", out rfac_comprobanteruta_key);
                tmp.TryGetValue("rfac_comprobantefac_key", out rfac_comprobantefac_key);
                tmp.TryGetValue("rfac_estado", out rfac_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.rfac_empresa = (Int32)Conversiones.GetValueByType(rfac_empresa, typeof(Int32));
                this.rfac_comprobanteruta = (Int64)Conversiones.GetValueByType(rfac_comprobanteruta, typeof(Int64));
                this.rfac_comprobantefac = (Int64)Conversiones.GetValueByType(rfac_comprobantefac, typeof(Int64));
                this.rfac_empresa_key = (Int32)Conversiones.GetValueByType(rfac_empresa_key, typeof(Int32));
                this.rfac_comprobanteruta_key = (Int64)Conversiones.GetValueByType(rfac_comprobanteruta_key, typeof(Int64));
                this.rfac_comprobantefac_key = (Int64)Conversiones.GetValueByType(rfac_comprobantefac_key, typeof(Int64));
                this.rfac_estado = (Int32?)Conversiones.GetValueByType(rfac_estado, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));


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
