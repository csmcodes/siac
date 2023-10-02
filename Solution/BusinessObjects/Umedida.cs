using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Umedida
    {
        #region Properties

        [Data(key = true, auto = true)]
        public int umd_codigo { get; set; }
        [Data(originalkey = true)]
        public int umd_codigo_key { get; set; }      
        [Data(key = true)]
        public int umd_empresa { get; set; }
        [Data(originalkey = true)]
        public int umd_empresa_key { get; set; }
        public string umd_id { get; set; }
        public string umd_nombre { get; set; }        
        public int? umd_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
              
        #endregion

        #region Constructors


        public Umedida()
        {
        }

        public Umedida(int codigo ,int empresa  , string id ,  string nombre ,  int? estado ,    string crea_usr ,     DateTime? crea_fecha ,   string mod_usr ,     DateTime? mod_fecha )
        {                
            this.umd_codigo =codigo;    	
            this.umd_codigo_key =codigo;       
            this.umd_empresa =empresa;    
            this.umd_empresa_key =empresa;
            this.umd_id =id;
            this.umd_nombre =nombre;           
            this.umd_estado =estado;
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr =mod_usr;
            this.mod_fecha =mod_fecha;
       }

        public Umedida(IDataReader reader)
        {
            this.umd_codigo = (int)reader["umd_codigo"];
            this.umd_codigo_key = (int)reader["umd_codigo"];
            this.umd_empresa = (int)reader["umd_empresa"];
            this.umd_empresa_key = (int)reader["umd_empresa"];
            this.umd_id = reader["umd_id"].ToString();
            this.umd_nombre = reader["umd_nombre"].ToString();           
            this.umd_estado = (reader["umd_estado"] != DBNull.Value) ? (int?)reader["umd_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }



        public Umedida(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object umd_empresa = null;
                object umd_codigo = null;
                object umd_id = null;
                object umd_nombre = null;
                object umd_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("umd_empresa", out umd_empresa);
                tmp.TryGetValue("umd_codigo", out umd_codigo);
                tmp.TryGetValue("umd_id", out umd_id);
                tmp.TryGetValue("umd_nombre", out umd_nombre);
                tmp.TryGetValue("umd_estado", out umd_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.umd_empresa = (Int32)Conversiones.GetValueByType(umd_empresa, typeof(Int32));
                this.umd_codigo = (Int32)Conversiones.GetValueByType(umd_codigo, typeof(Int32));
                this.umd_id = (String)Conversiones.GetValueByType(umd_id, typeof(String));
                this.umd_nombre = (String)Conversiones.GetValueByType(umd_nombre, typeof(String));
                this.umd_estado = (Int32?)Conversiones.GetValueByType(umd_estado, typeof(Int32?));
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
