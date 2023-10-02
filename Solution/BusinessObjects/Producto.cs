using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Producto
    {
        #region Properties

        [Data(key = true, auto = true)]
        public int pro_codigo { get; set; }
        [Data(originalkey = true)]
        public int pro_codigo_key { get; set; }      
        [Data(key = true)]
        public int pro_empresa { get; set; }
        [Data(originalkey = true)]
        public int pro_empresa_key { get; set; }
        public string pro_id { get; set; }
        public string pro_nombre { get; set; }        
        public int? pro_tproducto { get; set; }
        public int? pro_inventario { get; set; }
        public int? pro_iva { get; set; }
        public int? pro_unidad { get; set; }
        public int? pro_estado { get; set; }
        public int? pro_calcula { get; set; }
        public int? pro_grupo { get; set; }
        public int? pro_total { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }


        [Data(nosql = true, tablaref = "tproducto", camporef = "tpr_nombre", foreign = "pro_tproducto", keyref = "tpr_codigo")]
        public string pro_tproductonombre { get; set; }
        [Data(noprop = true)]
        public List<Dlistaprecio> dlistaprecio { get; set; }
        
        [Data(noprop = true)]
        public List<Factor> factores{ get; set; }

        [Data(noprop = true)]
        public Tproducto tproducto { get; set; }

              
        #endregion

        #region Constructors


        public Producto()
        {
        }

        public Producto(int codigo ,int empresa  , string id ,  string nombre , int? tproducto ,     int? inventario , int? iva, int? unidad,  int? estado, int? calcula ,    string crea_usr ,     DateTime? crea_fecha ,   string mod_usr ,     DateTime? mod_fecha )
        {                
            this.pro_codigo =codigo;    	
            this.pro_codigo_key =codigo;       
            this.pro_empresa =empresa;    
            this.pro_empresa_key =empresa;
            this.pro_id =id;
            this.pro_nombre =nombre;           
            this.pro_tproducto =tproducto;
            this.pro_inventario =inventario;
            this.pro_unidad = unidad;
            this.pro_iva = iva;
            this.pro_estado =estado;
            this.pro_calcula = calcula;
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr =mod_usr;
            this.mod_fecha =mod_fecha;
       }

        public Producto(IDataReader reader)
        {
            this.pro_codigo = (int)reader["pro_codigo"];
            this.pro_codigo_key = (int)reader["pro_codigo"];
            this.pro_empresa = (int)reader["pro_empresa"];
            this.pro_empresa_key = (int)reader["pro_empresa"];
            this.pro_id = reader["pro_id"].ToString();
            this.pro_nombre = reader["pro_nombre"].ToString();
            this.pro_tproducto = (reader["pro_tproducto"] != DBNull.Value) ? (int?)reader["pro_tproducto"] : null;
            this.pro_inventario = (reader["pro_inventario"] != DBNull.Value) ? (int?)reader["pro_inventario"] : null;
            this.pro_unidad = (reader["pro_unidad"] != DBNull.Value) ? (int?)reader["pro_unidad"] : null;
            this.pro_iva = (reader["pro_iva"] != DBNull.Value) ? (int?)reader["pro_iva"] : null;
            this.pro_estado = (reader["pro_estado"] != DBNull.Value) ? (int?)reader["pro_estado"] : null;
            this.pro_calcula= (reader["pro_calcula"] != DBNull.Value) ? (int?)reader["pro_calcula"] : null;
            this.pro_grupo = (reader["pro_grupo"] != DBNull.Value) ? (int?)reader["pro_grupo"] : null;
            this.pro_total= (reader["pro_total"] != DBNull.Value) ? (int?)reader["pro_total"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }






        public Producto(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object pro_empresa = null;
                object pro_codigo = null;
                object pro_id = null;
                object pro_nombre = null;
                object pro_grupo = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object pro_tproducto = null;
                object pro_inventario = null;
                object pro_estado = null;
                object pro_unidad = null;
                object pro_iva = null;
                object pro_calcula = null;
                object pro_total = null;

                tmp.TryGetValue("pro_empresa", out pro_empresa);
                tmp.TryGetValue("pro_codigo", out pro_codigo);
                tmp.TryGetValue("pro_id", out pro_id);
                tmp.TryGetValue("pro_nombre", out pro_nombre);
                tmp.TryGetValue("pro_grupo", out pro_grupo);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("pro_tproducto", out pro_tproducto);
                tmp.TryGetValue("pro_inventario", out pro_inventario);
                tmp.TryGetValue("pro_estado", out pro_estado);
                tmp.TryGetValue("pro_unidad", out pro_unidad);
                tmp.TryGetValue("pro_iva", out pro_iva);
                tmp.TryGetValue("pro_calcula", out pro_calcula);
                tmp.TryGetValue("pro_total", out pro_total);


                this.pro_empresa = (Int32)Conversiones.GetValueByType(pro_empresa, typeof(Int32));
                this.pro_codigo = (Int32)Conversiones.GetValueByType(pro_codigo, typeof(Int32));
                this.pro_id = (String)Conversiones.GetValueByType(pro_id, typeof(String));
                this.pro_nombre = (String)Conversiones.GetValueByType(pro_nombre, typeof(String));
                this.pro_grupo = (Int32)Conversiones.GetValueByType(pro_grupo, typeof(Int32));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.pro_tproducto = (Int32?)Conversiones.GetValueByType(pro_tproducto, typeof(Int32?));
                this.pro_inventario = (Int32?)Conversiones.GetValueByType(pro_inventario, typeof(Int32?));
                this.pro_estado = (Int32?)Conversiones.GetValueByType(pro_estado, typeof(Int32?));
                this.pro_unidad = (Int32?)Conversiones.GetValueByType(pro_unidad, typeof(Int32?));
                this.pro_iva = (Int32?)Conversiones.GetValueByType(pro_iva, typeof(Int32?));
                this.pro_calcula = (Int32?)Conversiones.GetValueByType(pro_calcula, typeof(Int32?));
                this.pro_total= (Int32?)Conversiones.GetValueByType(pro_total, typeof(Int32?));
                List<Factor> lista = new List<Factor>();
                Factor t = new Factor();
                t.fac_empresa = Convert.ToInt32(pro_empresa);
                t.fac_empresa_key = Convert.ToInt32(pro_empresa);

                t.fac_unidad = Convert.ToInt32(pro_unidad);
                t.fac_unidad_key = Convert.ToInt32(pro_unidad);
                t.fac_factor = 1;
                t.fac_estado = 1;
                t.fac_default = 1;
                t.crea_usr = "admin";
                t.crea_fecha = DateTime.Now;
                t.mod_usr = "admin";
                t.mod_fecha = DateTime.Now;
                lista.Add(t);

                this.factores = lista;




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
