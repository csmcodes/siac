using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Tproducto
    {
        #region Properties

        [Data(key = true, auto = true)]
        public int tpr_codigo { get; set; }
        [Data(originalkey = true)]
        public int tpr_codigo_key { get; set; }      
        [Data(key = true)]
        public int tpr_empresa { get; set; }
        [Data(originalkey = true)]
        public int tpr_empresa_key { get; set; }
        public string tpr_id { get; set; }
        public string tpr_nombre { get; set; }        
        public int? tpr_reporta { get; set; }
        public int? tpr_orden { get; set; }
        public int? tpr_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

        [Data(noprop = true)]
        public List<Calculoprecio> calculaprecio { get; set; }
              
        #endregion

        #region Constructors


        public Tproducto()
        {
        }

        public Tproducto(int codigo ,int empresa  , string id ,  string nombre , int? reporta ,     int? orden ,  int? estado ,    string crea_usr ,     DateTime? crea_fecha ,   string mod_usr ,     DateTime? mod_fecha )
        {                
            this.tpr_codigo =codigo;    	
            this.tpr_codigo_key =codigo;       
            this.tpr_empresa =empresa;    
            this.tpr_empresa_key =empresa;
            this.tpr_id =id;
            this.tpr_nombre =nombre;           
            this.tpr_reporta =reporta;
            this.tpr_orden =orden;
            this.tpr_estado =estado;
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr =mod_usr;
            this.mod_fecha =mod_fecha;
       }

        public Tproducto(IDataReader reader)
        {
            this.tpr_codigo = (int)reader["tpr_codigo"];
            this.tpr_codigo_key = (int)reader["tpr_codigo"];
            this.tpr_empresa = (int)reader["tpr_empresa"];
            this.tpr_empresa_key = (int)reader["tpr_empresa"];
            this.tpr_id = reader["tpr_id"].ToString();
            this.tpr_nombre = reader["tpr_nombre"].ToString();
            this.tpr_reporta = (reader["tpr_reporta"] != DBNull.Value) ? (int?)reader["tpr_reporta"] : null;
            this.tpr_orden = (reader["tpr_orden"] != DBNull.Value) ? (int?)reader["tpr_orden"] : null;
            this.tpr_estado = (reader["tpr_estado"] != DBNull.Value) ? (int?)reader["tpr_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }

        public Tproducto(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object tpr_empresa = null;
                object tpr_codigo = null;
                object tpr_nombre = null;
                object tpr_reporta = null;
                object tpr_orden = null;
                object tpr_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object tpr_id = null;


                tmp.TryGetValue("tpr_empresa", out tpr_empresa);
                tmp.TryGetValue("tpr_codigo", out tpr_codigo);
                tmp.TryGetValue("tpr_nombre", out tpr_nombre);
                tmp.TryGetValue("tpr_reporta", out tpr_reporta);
                tmp.TryGetValue("tpr_orden", out tpr_orden);
                tmp.TryGetValue("tpr_estado", out tpr_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("tpr_id", out tpr_id);


                this.tpr_empresa = (Int32)Conversiones.GetValueByType(tpr_empresa, typeof(Int32));
                this.tpr_codigo = (Int32)Conversiones.GetValueByType(tpr_codigo, typeof(Int32));
                this.tpr_nombre = (String)Conversiones.GetValueByType(tpr_nombre, typeof(String));
                this.tpr_reporta = (Int32?)Conversiones.GetValueByType(tpr_reporta, typeof(Int32?));
                this.tpr_orden = (Int32?)Conversiones.GetValueByType(tpr_orden, typeof(Int32?));
                this.tpr_estado = (Int32?)Conversiones.GetValueByType(tpr_estado, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.tpr_id = (String)Conversiones.GetValueByType(tpr_id, typeof(String));

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
