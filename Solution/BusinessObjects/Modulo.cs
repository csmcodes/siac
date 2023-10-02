using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;
namespace BusinessObjects
{
    public class Modulo
    {
        #region Properties


        [Data(key = true, auto = true)]
        public int mod_codigo { get; set; }
        [Data(originalkey = true)]
        public int mod_codigo_key { get; set; }
        public string mod_id { get; set; }
        public string mod_nombre { get; set; }
        public int? mod_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

        #endregion

        #region Constructors


        public Modulo()
        {
        }

        public Modulo(int codigo, string id, string nombre, int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)
        {
            this.mod_codigo = codigo;
            this.mod_codigo_key = codigo;
            this.mod_id = id;
            this.mod_nombre = nombre;
            this.mod_estado = estado;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;
        }

        public Modulo(IDataReader reader)
        {
            this.mod_codigo = (int)reader["mod_codigo"];
            this.mod_codigo_key = (int)reader["mod_codigo"];
            this.mod_id = reader["mod_id"].ToString();
            this.mod_nombre = reader["mod_nombre"].ToString();
            this.mod_estado = (reader["mod_estado"] != DBNull.Value) ? (int?)reader["mod_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }

        public Modulo(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object mod_codigo = null;
                object mod_id = null;
                object mod_nombre = null;
                object mod_estado = null;
                object crea_fecha = null;
                object crea_usr = null;
                object mod_fecha = null;
                object mod_usr = null;


                tmp.TryGetValue("mod_codigo", out mod_codigo);
                tmp.TryGetValue("mod_id", out mod_id);
                tmp.TryGetValue("mod_nombre", out mod_nombre);
                tmp.TryGetValue("mod_estado", out mod_estado);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);


                this.mod_codigo = (Int32)Conversiones.GetValueByType(mod_codigo, typeof(Int32));
                this.mod_id = (String)Conversiones.GetValueByType(mod_id, typeof(String));
                this.mod_nombre = (String)Conversiones.GetValueByType(mod_nombre, typeof(String));
                this.mod_estado = (Int32?)Conversiones.GetValueByType(mod_estado, typeof(Int32?));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));

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
