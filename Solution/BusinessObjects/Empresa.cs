using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
  public  class Empresa
    {
        #region Properties

        [Data(key = true, auto = true)]
        public int emp_codigo { get; set; }
        [Data(originalkey = true)]
        public int emp_codigo_key { get; set; }
        public string emp_id { get; set; } 
        public string emp_nombre { get; set; }
        public int? emp_imp_venta { get; set; }
        public int? emp_imp_compra { get; set; }
        public int? emp_informante { get; set; }
        public string emp_opciones { get; set; }
        public int? emp_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

        [Data(noprop =true)]
        public string emp_agenteretxml { get; set; }
        [Data(noprop = true)]
        public string emp_regimenmicro{ get; set; }
        [Data(noprop = true)]
        public string emp_regimenrimpe { get; set; }


        #endregion
        #region Constructors

        public Empresa()
        {
        }

        public Empresa(int codigo, string nombre, string id, int? imp_venta, int? imp_compra, int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)        
        {
            this.emp_codigo=codigo;       
            this.emp_codigo_key=codigo;
            this.emp_id = id;  
            this.emp_nombre=nombre;            
           this.emp_imp_venta=imp_venta;
           this.emp_imp_compra = imp_compra;
            this.emp_estado=estado;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;
        }

        public Empresa(IDataReader reader)
        {
            this.emp_codigo = (int)reader["emp_codigo"];
            this.emp_codigo_key = (int)reader["emp_codigo"];
            this.emp_nombre = reader["emp_nombre"].ToString();
            this.emp_id = reader["emp_id"].ToString();
            this.emp_imp_venta = (reader["emp_imp_venta"] != DBNull.Value) ? (int?)reader["emp_imp_venta"] : null;
            this.emp_imp_compra = (reader["emp_imp_compra"] != DBNull.Value) ? (int?)reader["emp_imp_compra"] : null;
            this.emp_informante = (reader["emp_informante"] != DBNull.Value) ? (int?)reader["emp_informante"] : null;
            this.emp_opciones= reader["emp_opciones"].ToString();
            this.emp_estado = (reader["emp_estado"] != DBNull.Value) ? (int?)reader["emp_estado"] : null;                     
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }
       

        public Empresa(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object emp_codigo = null;
                object emp_id = null;
                object emp_nombre = null;
                object emp_imp_venta = null;
                object emp_imp_compra = null;
                object emp_informante = null;
                object emp_opciones = null;
                object emp_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("emp_codigo", out emp_codigo);
                tmp.TryGetValue("emp_id", out emp_id);
                tmp.TryGetValue("emp_nombre", out emp_nombre);
                tmp.TryGetValue("emp_imp_venta", out emp_imp_venta);
                tmp.TryGetValue("emp_imp_compra", out emp_imp_compra);
                tmp.TryGetValue("emp_informante", out emp_informante);
                tmp.TryGetValue("emp_opciones", out emp_opciones);
                tmp.TryGetValue("emp_estado", out emp_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.emp_codigo = (Int32)Conversiones.GetValueByType(emp_codigo, typeof(Int32));
                this.emp_id = (String)Conversiones.GetValueByType(emp_id, typeof(String));
                this.emp_nombre = (String)Conversiones.GetValueByType(emp_nombre, typeof(String));
                this.emp_imp_venta = (Int32?)Conversiones.GetValueByType(emp_imp_venta, typeof(Int32?));
                this.emp_imp_compra = (Int32?)Conversiones.GetValueByType(emp_imp_compra, typeof(Int32?));
                this.emp_informante = (Int32?)Conversiones.GetValueByType(emp_informante, typeof(Int32?));
                this.emp_opciones = (String)Conversiones.GetValueByType(emp_opciones, typeof(String));
                this.emp_estado = (Int32?)Conversiones.GetValueByType(emp_estado, typeof(Int32?));
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
