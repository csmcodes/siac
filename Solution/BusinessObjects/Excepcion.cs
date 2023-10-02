using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Excepcion
    {
        #region Properties

        [Data(key = true, auto = true)]
        public Int64 exc_codigo { get; set; }
        [Data(originalkey = true)]
        public Int64 exc_codigo_key { get; set; }
        public DateTime? exc_fecha { get; set; }
        public String exc_descripcion { get; set; }
        public String exc_stack { get; set; }
        public String exc_origen { get; set; }
        [Data(noupdate = true)]
        public String crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }


        #endregion

        #region Constructors


        public Excepcion()
        {
        }

        public Excepcion(Int64 exc_codigo, DateTime exc_fecha, String exc_descripcion, String exc_stack, String exc_origen, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha)
        {
            this.exc_codigo = exc_codigo;
            this.exc_fecha = exc_fecha;
            this.exc_descripcion = exc_descripcion;
            this.exc_stack = exc_stack;
            this.exc_origen = exc_origen;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;


        }

        public Excepcion(IDataReader reader)
        {
            this.exc_codigo = (Int64)reader["exc_codigo"];
            this.exc_fecha = (reader["exc_fecha"] != DBNull.Value) ? (DateTime?)reader["exc_fecha"] : null;
            this.exc_descripcion = reader["exc_descripcion"].ToString();
            this.exc_stack = reader["exc_stack"].ToString();
            this.exc_origen = reader["exc_origen"].ToString();
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Excepcion(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object exc_codigo = null;
                object exc_fecha = null;
                object exc_descripcion = null;
                object exc_stack = null;
                object exc_origen = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("exc_codigo", out exc_codigo);
                tmp.TryGetValue("exc_fecha", out exc_fecha);
                tmp.TryGetValue("exc_descripcion", out exc_descripcion);
                tmp.TryGetValue("exc_stack", out exc_stack);
                tmp.TryGetValue("exc_origen", out exc_origen);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.exc_codigo = (Int64)Conversiones.GetValueByType(exc_codigo, typeof(Int64));
                this.exc_fecha = (DateTime?)Conversiones.GetValueByType(exc_fecha, typeof(DateTime?));
                this.exc_descripcion = (String)Conversiones.GetValueByType(exc_descripcion, typeof(String));
                this.exc_stack = (String)Conversiones.GetValueByType(exc_stack, typeof(String));
                this.exc_origen = (String)Conversiones.GetValueByType(exc_origen, typeof(String));
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
