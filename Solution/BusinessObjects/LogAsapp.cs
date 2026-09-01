using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class LogAsapp
    {
        #region Properties

        [Data(key = true, auto = true)]
        public Int64 log_codigo { get; set; }
        [Data(originalkey = true)]
        public Int64 log_codigo_key { get; set; }
        public Int32 log_empresa { get; set; }
        public Int64? log_comprobante { get; set; }
        public String log_operacion { get; set; }
        public String log_endpoint { get; set; }
        public Int32? log_httpstatus { get; set; }
        public Int32? log_exitoso { get; set; }
        public String log_request { get; set; }
        public String log_response { get; set; }
        public String log_mensaje { get; set; }
        public Int32? log_duracionms { get; set; }
        public DateTime? crea_fecha { get; set; }

        #endregion

        #region Constructors

        public LogAsapp()
        {
        }

        public LogAsapp(IDataReader reader)
        {
            this.log_codigo = (Int64)reader["log_codigo"];
            this.log_empresa = (Int32)reader["log_empresa"];
            this.log_comprobante = (reader["log_comprobante"] != DBNull.Value) ? (Int64?)reader["log_comprobante"] : null;
            this.log_operacion = reader["log_operacion"].ToString();
            this.log_endpoint = reader["log_endpoint"].ToString();
            this.log_httpstatus = (reader["log_httpstatus"] != DBNull.Value) ? (Int32?)reader["log_httpstatus"] : null;
            this.log_exitoso = (reader["log_exitoso"] != DBNull.Value) ? (Int32?)reader["log_exitoso"] : null;
            this.log_request = reader["log_request"].ToString();
            this.log_response = reader["log_response"].ToString();
            this.log_mensaje = reader["log_mensaje"].ToString();
            this.log_duracionms = (reader["log_duracionms"] != DBNull.Value) ? (Int32?)reader["log_duracionms"] : null;
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
        }

        public LogAsapp(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object log_codigo = null;
                object log_empresa = null;
                object log_comprobante = null;
                object log_operacion = null;
                object log_endpoint = null;
                object log_httpstatus = null;
                object log_exitoso = null;
                object log_request = null;
                object log_response = null;
                object log_mensaje = null;
                object log_duracionms = null;
                object crea_fecha = null;

                tmp.TryGetValue("log_codigo", out log_codigo);
                tmp.TryGetValue("log_empresa", out log_empresa);
                tmp.TryGetValue("log_comprobante", out log_comprobante);
                tmp.TryGetValue("log_operacion", out log_operacion);
                tmp.TryGetValue("log_endpoint", out log_endpoint);
                tmp.TryGetValue("log_httpstatus", out log_httpstatus);
                tmp.TryGetValue("log_exitoso", out log_exitoso);
                tmp.TryGetValue("log_request", out log_request);
                tmp.TryGetValue("log_response", out log_response);
                tmp.TryGetValue("log_mensaje", out log_mensaje);
                tmp.TryGetValue("log_duracionms", out log_duracionms);
                tmp.TryGetValue("crea_fecha", out crea_fecha);

                this.log_codigo = (Int64)Conversiones.GetValueByType(log_codigo, typeof(Int64));
                this.log_empresa = (Int32)Conversiones.GetValueByType(log_empresa, typeof(Int32));
                this.log_comprobante = (Int64?)Conversiones.GetValueByType(log_comprobante, typeof(Int64?));
                this.log_operacion = (String)Conversiones.GetValueByType(log_operacion, typeof(String));
                this.log_endpoint = (String)Conversiones.GetValueByType(log_endpoint, typeof(String));
                this.log_httpstatus = (Int32?)Conversiones.GetValueByType(log_httpstatus, typeof(Int32?));
                this.log_exitoso = (Int32?)Conversiones.GetValueByType(log_exitoso, typeof(Int32?));
                this.log_request = (String)Conversiones.GetValueByType(log_request, typeof(String));
                this.log_response = (String)Conversiones.GetValueByType(log_response, typeof(String));
                this.log_mensaje = (String)Conversiones.GetValueByType(log_mensaje, typeof(String));
                this.log_duracionms = (Int32?)Conversiones.GetValueByType(log_duracionms, typeof(Int32?));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
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
