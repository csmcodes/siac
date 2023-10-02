using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
  public  class Catalogo
    {
        #region Properties

        [Data(key = true, auto = true)]
        public int cat_codigo { get; set; }
        [Data(originalkey = true)]
        public int cat_codigo_key { get; set; }
        public string cat_tipo { get; set; }
        public int? cat_padre { get; set; }
        public  string cat_nombre { get; set; }
        public int? cat_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

        [Data(noprop = true)]
 
        public string cat_padre_nombre { get; set; }

        #endregion
       #region Constructors
       
        public Catalogo()
        {
        }

        public Catalogo(int codigo, string nombre, string tipo, int? padre,int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)        
        {
            this.cat_codigo=codigo;       
            this.cat_codigo_key=codigo;
            this.cat_nombre = nombre;  
            this.cat_padre=padre;            
            this.cat_tipo=tipo;
            this.cat_estado = estado;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;
        }

        public Catalogo(IDataReader reader)
        {
            this.cat_codigo = (int)reader["cat_codigo"];
            this.cat_codigo_key = (int)reader["cat_codigo"];
            this.cat_nombre = reader["cat_nombre"].ToString();
            this.cat_tipo = reader["cat_tipo"].ToString();
            this.cat_padre = (reader["cat_padre"] != DBNull.Value) ? (int?)reader["cat_padre"] : null;
            this.cat_estado = (reader["cat_estado"] != DBNull.Value) ? (int?)reader["cat_estado"] : null;          
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }
        public Catalogo(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object cat_codigo = null;
                object cat_id = null;
                object cat_nombre = null;
                object cat_tipo = null;
                object cat_padre = null;
                object cat_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("cat_codigo", out cat_codigo);
                tmp.TryGetValue("cat_id", out cat_id);
                tmp.TryGetValue("cat_nombre", out cat_nombre);
                tmp.TryGetValue("cat_tipo", out cat_tipo);
                tmp.TryGetValue("cat_padre", out cat_padre);
                tmp.TryGetValue("cat_estado", out cat_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.cat_codigo = (Int32)Conversiones.GetValueByType(cat_codigo, typeof(Int32));
        //        this.cat_id = (String)Conversiones.GetValueByType(cat_id, typeof(String));
                this.cat_nombre = (String)Conversiones.GetValueByType(cat_nombre, typeof(String));
                this.cat_tipo = (String)Conversiones.GetValueByType(cat_tipo, typeof(String));
                this.cat_padre = (Int32?)Conversiones.GetValueByType(cat_padre, typeof(Int32?));
                this.cat_estado = (Int32?)Conversiones.GetValueByType(cat_estado, typeof(Int32?));
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
