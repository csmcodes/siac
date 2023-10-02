using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Formulario
    {
        #region Properties

       
        [Data(key = true, auto = true)]
        public int for_codigo { get; set; }
        [Data(originalkey = true)]
        public int for_codigo_key { get; set; }
        public string for_id { get; set; }
        public string for_nombre { get; set; }
        public string for_descripcion { get; set; }
        public string for_ayuda { get; set; }
        public int? for_modulo { get; set; }
        public string for_clase { get; set; }     
        public int? for_estado { get; set; }       
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
              
        #endregion

        #region Constructors


        public Formulario()
        {
        }

        public Formulario(int codigo,  string id, string nombre,  string descripcion, string ayuda,  int? modulo,string clase,  int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)
        {       
           
            this.for_codigo = codigo;
            this.for_codigo_key =codigo;
            this.for_id =id;
            this.for_nombre =nombre;
            this.for_descripcion =descripcion;
            this.for_ayuda =ayuda;
            this.for_modulo =modulo;
            this.for_clase =clase;
            this.for_estado =estado;
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr =mod_usr;
            this.mod_fecha =mod_fecha;
       }

        public Formulario(IDataReader reader)
        {
            this.for_codigo = (int)reader["for_codigo"];
            this.for_codigo_key = (int)reader["for_codigo"];
            this.for_id = reader["for_id"].ToString();
            this.for_nombre = reader["for_nombre"].ToString();
            this.for_descripcion = reader["for_descripcion"].ToString();
            this.for_ayuda = reader["for_ayuda"].ToString();
            this.for_modulo = (reader["for_modulo"] != DBNull.Value) ? (int?)reader["for_modulo"] : null;
            this.for_clase = reader["for_clase"].ToString();
            this.for_estado = (reader["for_estado"] != DBNull.Value) ? (int?)reader["for_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }



        public Formulario(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object for_codigo = null;
                object for_id = null;
                object for_nombre = null;
                object for_descripcion = null;
                object for_modulo = null;
                object for_clase = null;
                object for_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object for_ayuda = null;


                tmp.TryGetValue("for_codigo", out for_codigo);
                tmp.TryGetValue("for_id", out for_id);
                tmp.TryGetValue("for_nombre", out for_nombre);
                tmp.TryGetValue("for_descripcion", out for_descripcion);
                tmp.TryGetValue("for_modulo", out for_modulo);
                tmp.TryGetValue("for_clase", out for_clase);
                tmp.TryGetValue("for_estado", out for_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("for_ayuda", out for_ayuda);


                this.for_codigo = (Int32)Conversiones.GetValueByType(for_codigo, typeof(Int32));
                this.for_id = (String)Conversiones.GetValueByType(for_id, typeof(String));
                this.for_nombre = (String)Conversiones.GetValueByType(for_nombre, typeof(String));
                this.for_descripcion = (String)Conversiones.GetValueByType(for_descripcion, typeof(String));
                this.for_modulo = (Int32)Conversiones.GetValueByType(for_modulo, typeof(Int32));
                this.for_clase = (String)Conversiones.GetValueByType(for_clase, typeof(String));
                this.for_estado = (Int32?)Conversiones.GetValueByType(for_estado, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.for_ayuda = (String)Conversiones.GetValueByType(for_ayuda, typeof(String));

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
