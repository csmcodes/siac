using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Centro
    {
        #region Properties

        [Data(key = true, auto = true)]
        public int cen_codigo { get; set; }
        [Data(originalkey = true)]
        public int cen_codigo_key { get; set; }      
        [Data(key = true)]
        public int cen_empresa { get; set; }
        [Data(originalkey = true)]
        public int cen_empresa_key { get; set; }
        public string cen_id { get; set; }
        public string cen_nombre { get; set; }
        public int? cen_reporta { get; set; }
        public int? cen_orden { get; set; }      
        public int? cen_estado { get; set; }
       
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
              
        #endregion

        #region Constructors


        public Centro()
        {
        }

        public Centro(int codigo, int empresa, string id, string nombre, int? modulo, int? genero, int? movimiento, int? reporta, int? orden, int? visualiza, int? negrita,  int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)
        {       
            this.cen_codigo =codigo;        
            this.cen_codigo_key =codigo;
            this.cen_empresa =empresa;      
            this.cen_empresa_key=empresa;
            this.cen_id = id;
            this.cen_nombre = nombre;  
            this.cen_reporta = reporta;
            this.cen_orden = orden;
           
            this.cen_estado = estado;
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr =mod_usr;
            this.mod_fecha =mod_fecha;
       }

        public Centro(IDataReader reader)
        {
            this.cen_codigo = (int)reader["cen_codigo"];
            this.cen_codigo_key = (int)reader["cen_codigo"];
            this.cen_empresa = (int)reader["cen_empresa"];
            this.cen_empresa_key = (int)reader["cen_empresa"]; 
            this.cen_id = reader["cen_id"].ToString();
            this.cen_nombre = reader["cen_nombre"].ToString();           
            this.cen_reporta = (reader["cen_reporta"] != DBNull.Value) ? (int?)reader["cen_reporta"] : null;
            this.cen_orden = (reader["cen_orden"] != DBNull.Value) ? (int?)reader["cen_orden"] : null;            
            
            this.cen_estado = (reader["cen_estado"] != DBNull.Value) ? (int?)reader["cen_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }

        public Centro(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object cen_empresa = null;
                object cen_codigo = null;
                object cen_nombre = null;
                object cen_reporta = null;
                object cen_orden = null;
                object cen_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object cen_id = null;


                tmp.TryGetValue("cen_empresa", out cen_empresa);
                tmp.TryGetValue("cen_codigo", out cen_codigo);
                tmp.TryGetValue("cen_nombre", out cen_nombre);
                tmp.TryGetValue("cen_reporta", out cen_reporta);
                tmp.TryGetValue("cen_orden", out cen_orden);
                tmp.TryGetValue("cen_estado", out cen_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("cen_id", out cen_id);


                this.cen_empresa = (Int32)Conversiones.GetValueByType(cen_empresa, typeof(Int32));
                this.cen_codigo = (Int32)Conversiones.GetValueByType(cen_codigo, typeof(Int32));
                this.cen_nombre = (String)Conversiones.GetValueByType(cen_nombre, typeof(String));
                this.cen_reporta = (Int32?)Conversiones.GetValueByType(cen_reporta, typeof(Int32?));
                this.cen_orden = (Int32?)Conversiones.GetValueByType(cen_orden, typeof(Int32?));
                this.cen_estado = (Int32?)Conversiones.GetValueByType(cen_estado, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.cen_id = (String)Conversiones.GetValueByType(cen_id, typeof(String));

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
