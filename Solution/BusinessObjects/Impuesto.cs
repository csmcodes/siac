using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Impuesto
    {
        #region Properties

        [Data(key = true, auto = true)]
        public int imp_codigo { get; set; }
        [Data(originalkey = true)]
        public int imp_codigo_key { get; set; }      
        [Data(key = true)]
        public int imp_empresa { get; set; }
        [Data(originalkey = true)]
        public int imp_empresa_key { get; set; }
        public string imp_id { get; set; }
        public string imp_nombre { get; set; }
        public int? imp_concepto { get; set; }
        public int? imp_cuenta { get; set; }
        public int? imp_ivafuente { get; set; }
        public int? imp_servicio { get; set; }
        public decimal? imp_porcentaje { get; set; }
        public int? imp_iva { get; set; }
        public int? imp_ret{ get; set; }
        public int? imp_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

        [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "imp_empresa, imp_cuenta", keyref = "cue_empresa, cue_codigo", join = "left")]
        public string imp_nombrecuenta { get; set; }
        #endregion

        #region Constructors


        public Impuesto()
        {
        }

        public Impuesto(int codigo, int empresa, string id, string nombre, int? concepto, int? cuenta, int? ivafuente, int? servicio, int? porcentaje, int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)
        {                
            this.imp_codigo =codigo;    	
            this.imp_codigo_key =codigo;       
            this.imp_empresa =empresa;    
            this.imp_empresa_key =empresa;
            this.imp_id =id;
            this.imp_nombre =nombre;
            this.imp_concepto = concepto;
            this.imp_cuenta = cuenta;
            this.imp_ivafuente = ivafuente;
            this.imp_servicio = servicio;
            this.imp_porcentaje = porcentaje;
            this.imp_estado =estado;
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr =mod_usr;
            this.mod_fecha =mod_fecha;
       }

        public Impuesto(IDataReader reader)
        {
            this.imp_codigo = (int)reader["imp_codigo"];
            this.imp_codigo_key = (int)reader["imp_codigo"];
            this.imp_empresa = (int)reader["imp_empresa"];
            this.imp_empresa_key = (int)reader["imp_empresa"];
            this.imp_id = reader["imp_id"].ToString();
            this.imp_nombre = reader["imp_nombre"].ToString();
            this.imp_concepto = (reader["imp_concepto"] != DBNull.Value) ? (int?)reader["imp_concepto"] : null; ;
            this.imp_cuenta = (reader["imp_cuenta"] != DBNull.Value) ? (int?)reader["imp_cuenta"] : null; ;
            this.imp_ivafuente = (reader["imp_ivafuente"] != DBNull.Value) ? (int?)reader["imp_ivafuente"] : null; ;
            this.imp_servicio = (reader["imp_servicio"] != DBNull.Value) ? (int?)reader["imp_servicio"] : null; ;
            this.imp_porcentaje = (reader["imp_porcentaje"] != DBNull.Value) ? (decimal?)reader["imp_porcentaje"] : null; ;
            this.imp_iva= (reader["imp_iva"] != DBNull.Value) ? (int?)reader["imp_iva"] : null;
            this.imp_ret= (reader["imp_ret"] != DBNull.Value) ? (int?)reader["imp_ret"] : null;
            this.imp_estado = (reader["imp_estado"] != DBNull.Value) ? (int?)reader["imp_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.imp_nombrecuenta = reader["imp_nombrecuenta"].ToString();
            
        }
        public Impuesto(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object imp_empresa = null;
                object imp_codigo = null;
                object imp_id = null;
                object imp_nombre = null;
                object imp_porcentaje = null;
                object imp_iva = null;
                object imp_ret= null;
                object imp_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object imp_concepto = null;
                object imp_cuenta = null;
                object imp_ivafuente = null;
                object imp_servicio = null;


                tmp.TryGetValue("imp_empresa", out imp_empresa);
                tmp.TryGetValue("imp_codigo", out imp_codigo);
                tmp.TryGetValue("imp_id", out imp_id);
                tmp.TryGetValue("imp_nombre", out imp_nombre);
                tmp.TryGetValue("imp_porcentaje", out imp_porcentaje);
                tmp.TryGetValue("imp_iva", out imp_iva);
                tmp.TryGetValue("imp_ret", out imp_ret);
                tmp.TryGetValue("imp_estado", out imp_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("imp_concepto", out imp_concepto);
                tmp.TryGetValue("imp_cuenta", out imp_cuenta);
                tmp.TryGetValue("imp_ivafuente", out imp_ivafuente);
                tmp.TryGetValue("imp_servicio", out imp_servicio);


                this.imp_empresa = (Int32)Conversiones.GetValueByType(imp_empresa, typeof(Int32));
                this.imp_codigo = (Int32)Conversiones.GetValueByType(imp_codigo, typeof(Int32));
                this.imp_id = (String)Conversiones.GetValueByType(imp_id, typeof(String));
                this.imp_nombre = (String)Conversiones.GetValueByType(imp_nombre, typeof(String));
                this.imp_porcentaje = (Decimal)Conversiones.GetValueByType(imp_porcentaje, typeof(Decimal));
                this.imp_iva = (Int32?)Conversiones.GetValueByType(imp_estado, typeof(Int32?));
                this.imp_ret= (Int32?)Conversiones.GetValueByType(imp_iva, typeof(Int32?));
                this.imp_estado = (Int32?)Conversiones.GetValueByType(imp_ret, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.imp_concepto = (Int32?)Conversiones.GetValueByType(imp_concepto, typeof(Int32?));
                this.imp_cuenta = (Int32?)Conversiones.GetValueByType(imp_cuenta, typeof(Int32?));
                this.imp_ivafuente = (Int32?)Conversiones.GetValueByType(imp_ivafuente, typeof(Int32?));
                this.imp_servicio = (Int32?)Conversiones.GetValueByType(imp_servicio, typeof(Int32?));

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
