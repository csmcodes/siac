using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Concepto
    {
        #region Properties

        [Data(key = true, auto = true)]
        public int con_codigo { get; set; }
        [Data(originalkey = true)]
        public int con_codigo_key { get; set; }      
        [Data(key = true)]
        public int con_empresa { get; set; }
        [Data(originalkey = true)]
        public int con_empresa_key { get; set; }
        public string con_id { get; set; }
        public string con_nombre { get; set; }
        public string con_tipo { get; set; }       
        public int? con_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
              
        #endregion

        #region Constructors


        public Concepto()
        {
        }

        public Concepto(int codigo, int empresa, string id, string nombre, string tipo,int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)
        {                
            this.con_codigo =codigo;    	
            this.con_codigo_key =codigo;       
            this.con_empresa =empresa;    
            this.con_empresa_key =empresa;
            this.con_id =id;
            this.con_nombre =nombre;
            this.con_tipo =tipo;            
            this.con_estado =estado;
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr =mod_usr;
            this.mod_fecha =mod_fecha;
       }

        public Concepto(IDataReader reader)
        {
            this.con_codigo = (int)reader["con_codigo"];
            this.con_codigo_key = (int)reader["con_codigo"];
            this.con_empresa = (int)reader["con_empresa"];
            this.con_empresa_key = (int)reader["con_empresa"];
            this.con_id = reader["con_id"].ToString();
            this.con_nombre = reader["con_nombre"].ToString();
            this.con_tipo = reader["con_tipo"].ToString();           
            this.con_estado = (reader["con_estado"] != DBNull.Value) ? (int?)reader["con_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }


        public Concepto(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object con_empresa = null;
                object con_codigo = null;
                object con_id = null;
                object con_nombre = null;
                object con_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object con_tipo = null;


                tmp.TryGetValue("con_empresa", out con_empresa);
                tmp.TryGetValue("con_codigo", out con_codigo);
                tmp.TryGetValue("con_id", out con_id);
                tmp.TryGetValue("con_nombre", out con_nombre);
                tmp.TryGetValue("con_estado", out con_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("con_tipo", out con_tipo);


                this.con_empresa = (Int32)Conversiones.GetValueByType(con_empresa, typeof(Int32));
                this.con_codigo = (Int32)Conversiones.GetValueByType(con_codigo, typeof(Int32));
                this.con_id = (String)Conversiones.GetValueByType(con_id, typeof(String));
                this.con_nombre = (String)Conversiones.GetValueByType(con_nombre, typeof(String));
                this.con_estado = (Int32?)Conversiones.GetValueByType(con_estado, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.con_tipo = (String)Conversiones.GetValueByType(con_tipo, typeof(String));

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
