using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Token
    {
        #region Properties

        [Data(key = true)]
        public Int32 tok_empresa { get; set; }
        [Data(originalkey = true)]
        public Int32 tok_empresa_key { get; set; }
        [Data(key = true, auto = true)]
        public Int64 tok_codigo { get; set; }
        [Data(originalkey = true)]
        public Int64 tok_codigo_key { get; set; }
        public Int64? tok_comprobante { get; set; }
        public String tok_doctran { get; set; }
        public String tok_data { get; set; }
        public String crea_usr { get; set; }
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }


        #endregion

        #region Constructors


        public Token()
        {
        }

        public Token(Int32 tok_empresa, Int64 tok_codigo, Int64 tok_comprobante, String tok_doctran, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha)
        {
            this.tok_empresa = tok_empresa;
            this.tok_codigo = tok_codigo;
            this.tok_comprobante = tok_comprobante;
            this.tok_doctran = tok_doctran;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;


        }

        public Token(IDataReader reader)
        {
            this.tok_empresa = (Int32)reader["tok_empresa"];
            this.tok_codigo = (Int64)reader["tok_codigo"];
            this.tok_comprobante = (reader["tok_comprobante"] != DBNull.Value) ? (Int64?)reader["tok_comprobante"] : null;
            this.tok_doctran = reader["tok_doctran"].ToString();
            this.tok_data= reader["tok_data"].ToString();
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Token(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object tok_empresa = null;
                object tok_codigo = null;
                object tok_comprobante = null;
                object tok_doctran = null;
                object tok_data = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("tok_empresa", out tok_empresa);
                tmp.TryGetValue("tok_codigo", out tok_codigo);
                tmp.TryGetValue("tok_comprobante", out tok_comprobante);
                tmp.TryGetValue("tok_doctran", out tok_doctran);
                tmp.TryGetValue("tok_data", out tok_data);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.tok_empresa = (Int32)Conversiones.GetValueByType(tok_empresa, typeof(Int32));
                this.tok_codigo = (Int64)Conversiones.GetValueByType(tok_codigo, typeof(Int64));
                this.tok_comprobante = (Int64?)Conversiones.GetValueByType(tok_comprobante, typeof(Int64?));
                this.tok_doctran = (String)Conversiones.GetValueByType(tok_doctran, typeof(String));
                this.tok_data = (String)Conversiones.GetValueByType(tok_data, typeof(String));
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
