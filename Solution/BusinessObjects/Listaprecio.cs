using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Listaprecio
    {
        #region Properties

        [Data(key = true, auto = true)]
        public int lpr_codigo { get; set; }
        [Data(originalkey = true)]
        public int lpr_codigo_key { get; set; }      
        [Data(key = true)]
        public int lpr_empresa { get; set; }
        [Data(originalkey = true)]
        public int lpr_empresa_key { get; set; }
        public string lpr_id { get; set; }
        public string lpr_nombre { get; set; }
        public int? lpr_moneda { get; set; }
        public int? lpr_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
              
        #endregion

        #region Constructors


        public Listaprecio()
        {
        }

        public Listaprecio(int codigo ,int empresa  , string id ,  string nombre ,int? moneda ,  int? estado ,    string crea_usr ,     DateTime? crea_fecha ,   string mod_usr ,     DateTime? mod_fecha )
        {                
            this.lpr_codigo =codigo;    	
            this.lpr_codigo_key =codigo;       
            this.lpr_empresa =empresa;    
            this.lpr_empresa_key =empresa;
            this.lpr_id =id;
            this.lpr_nombre =nombre;            
            this.lpr_moneda =moneda;
            this.lpr_estado =estado;
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr =mod_usr;
            this.mod_fecha =mod_fecha;
       }

        public Listaprecio(IDataReader reader)
        {
            this.lpr_codigo = (int)reader["lpr_codigo"];
            this.lpr_codigo_key = (int)reader["lpr_codigo"];
            this.lpr_empresa = (int)reader["lpr_empresa"];
            this.lpr_empresa_key = (int)reader["lpr_empresa"];
            this.lpr_id = reader["lpr_id"].ToString();
            this.lpr_nombre = reader["lpr_nombre"].ToString();
            this.lpr_moneda = (reader["lpr_moneda"] != DBNull.Value) ? (int?)reader["lpr_moneda"] : null;
            this.lpr_estado = (reader["lpr_estado"] != DBNull.Value) ? (int?)reader["lpr_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }



        public Listaprecio(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object lpr_empresa = null;
                object lpr_codigo = null;
                object lpr_id = null;
                object lpr_nombre = null;
                object lpr_moneda = null;
                object lpr_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("lpr_empresa", out lpr_empresa);
                tmp.TryGetValue("lpr_codigo", out lpr_codigo);
                tmp.TryGetValue("lpr_id", out lpr_id);
                tmp.TryGetValue("lpr_nombre", out lpr_nombre);
                tmp.TryGetValue("lpr_moneda", out lpr_moneda);
                tmp.TryGetValue("lpr_estado", out lpr_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.lpr_empresa = (Int32)Conversiones.GetValueByType(lpr_empresa, typeof(Int32));
                this.lpr_codigo = (Int32)Conversiones.GetValueByType(lpr_codigo, typeof(Int32));
                this.lpr_id = (String)Conversiones.GetValueByType(lpr_id, typeof(String));
                this.lpr_nombre = (String)Conversiones.GetValueByType(lpr_nombre, typeof(String));
                this.lpr_moneda = (Int32?)Conversiones.GetValueByType(lpr_moneda, typeof(Int32?));
                this.lpr_estado = (Int32?)Conversiones.GetValueByType(lpr_estado, typeof(Int32?));
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
