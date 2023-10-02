using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{

    public class Menu
    {
        #region Properties


        [Data(key = true, auto = true)]
        public int men_id { get; set; }
        [Data(originalkey = true)]
        public int men_id_key { get; set; }        
        public string men_nombre { get; set; }
        public string men_formulario { get; set; }
        public int? men_padre { get; set; }
        public string men_imagen { get; set; }
        public int? men_orden{ get; set; }
        public String men_titulo { get; set; }
        public String men_descripcion { get; set; }
        public String men_opciones { get; set; }
        public int? men_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

        #endregion
        
         #region Constructors
       
        public Menu()
        {
        }

        public Menu(int codigo, string nombre, string formulario, string imagen, int? padre, int? orden, int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)        
        {
            this.men_id=codigo;       
            this.men_id_key=codigo;    
            this.men_nombre=nombre; 
            this.men_formulario=formulario; 
            this.men_padre=padre;
            this.men_imagen=imagen;
            this.men_orden=orden;
            this.men_estado = estado;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;

        }

        public  Menu(IDataReader reader)
        {
            this.men_id = (int)reader["men_id"];
            this.men_id_key = (int)reader["men_id"];
            this.men_nombre = reader["men_nombre"].ToString();
            this.men_formulario = reader["men_formulario"].ToString();
            this.men_padre = (reader["men_padre"] != DBNull.Value) ? (int?)reader["men_padre"] : null;
            this.men_imagen = reader["men_imagen"].ToString();

            this.men_orden= (reader["men_orden"] != DBNull.Value) ? (int?)reader["men_orden"] : null;
            this.men_titulo = reader["men_titulo"].ToString();
            this.men_descripcion = reader["men_descripcion"].ToString();
            this.men_opciones = reader["men_opciones"].ToString();

            this.men_estado = (reader["men_estado"] != DBNull.Value) ? (int?)reader["men_estado"] : null;               
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }


        public Menu(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object men_id = null;
                object men_nombre = null;
                object men_formulario = null;
                object men_imagen = null;
                object men_padre = null;
                object men_orden = null;
                object men_titulo = null;
                object men_descripcion = null;
                object men_opciones = null;
                object men_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("men_id", out men_id);
                tmp.TryGetValue("men_nombre", out men_nombre);
                tmp.TryGetValue("men_formulario", out men_formulario);
                tmp.TryGetValue("men_imagen", out men_imagen);
                tmp.TryGetValue("men_padre", out men_padre);
                tmp.TryGetValue("men_orden", out men_orden);
                tmp.TryGetValue("men_titulo", out men_titulo);
                tmp.TryGetValue("men_descripcion", out men_descripcion);
                tmp.TryGetValue("men_opciones", out men_opciones);
                tmp.TryGetValue("men_estado", out men_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.men_id = (Int32)Conversiones.GetValueByType(men_id, typeof(Int32));
                this.men_nombre = (String)Conversiones.GetValueByType(men_nombre, typeof(String));
                this.men_formulario = (String)Conversiones.GetValueByType(men_formulario, typeof(String));
                this.men_imagen = (String)Conversiones.GetValueByType(men_imagen, typeof(String));
                this.men_padre = (Int32?)Conversiones.GetValueByType(men_padre, typeof(Int32?));
                this.men_orden = (Int32?)Conversiones.GetValueByType(men_orden, typeof(Int32?));
                this.men_titulo = (String)Conversiones.GetValueByType(men_titulo, typeof(String));
                this.men_descripcion = (String)Conversiones.GetValueByType(men_descripcion, typeof(String));
                this.men_opciones = (String)Conversiones.GetValueByType(men_opciones, typeof(String));
                this.men_estado = (Int32?)Conversiones.GetValueByType(men_estado, typeof(Int32?));
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
