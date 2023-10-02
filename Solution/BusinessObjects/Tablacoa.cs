using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Tablacoa
    {
        #region Properties

    	[Data(key = true)]
	public Int32 tab_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 tab_empresa_key { get; set; }
	[Data(key = true)]
	public Int32 tab_codigo { get; set; }
	[Data(originalkey = true)]
	public Int32 tab_codigo_key { get; set; }
	public String tab_id { get; set; }
	public String tab_nombre { get; set; }
	public Int32? tab_estado { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Tablacoa()
        {
        }

        public  Tablacoa( Int32 tab_empresa,Int32 tab_codigo,String tab_id,String tab_nombre,Int32 tab_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.tab_empresa = tab_empresa;
	this.tab_codigo = tab_codigo;
    this.tab_empresa_key = tab_empresa;
    this.tab_codigo_key = tab_codigo;
	this.tab_id = tab_id;
	this.tab_nombre = tab_nombre;
	this.tab_estado = tab_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Tablacoa(IDataReader reader)
        {
    	this.tab_empresa = (Int32)reader["tab_empresa"];
	this.tab_codigo = (Int32)reader["tab_codigo"];
    this.tab_empresa_key = (Int32)reader["tab_empresa"];
    this.tab_codigo_key = (Int32)reader["tab_codigo"];
	this.tab_id = reader["tab_id"].ToString();
	this.tab_nombre = reader["tab_nombre"].ToString();
	this.tab_estado = (reader["tab_estado"] != DBNull.Value) ? (Int32?)reader["tab_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Tablacoa(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object tab_empresa = null;
	object tab_codigo = null;
    object tab_empresa_key = null;
    object tab_codigo_key = null;
	object tab_id = null;
	object tab_nombre = null;
	object tab_estado = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("tab_empresa", out tab_empresa);
	tmp.TryGetValue("tab_codigo", out tab_codigo);
    tmp.TryGetValue("tab_empresa_key", out tab_empresa_key);
    tmp.TryGetValue("tab_codigo_key", out tab_codigo_key);
	tmp.TryGetValue("tab_id", out tab_id);
	tmp.TryGetValue("tab_nombre", out tab_nombre);
	tmp.TryGetValue("tab_estado", out tab_estado);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.tab_empresa = (Int32)Conversiones.GetValueByType(tab_empresa, typeof(Int32));
	this.tab_codigo = (Int32)Conversiones.GetValueByType(tab_codigo, typeof(Int32));
    this.tab_empresa_key = (Int32)Conversiones.GetValueByType(tab_empresa_key, typeof(Int32));
    this.tab_codigo_key = (Int32)Conversiones.GetValueByType(tab_codigo_key, typeof(Int32));
	this.tab_id = (String)Conversiones.GetValueByType(tab_id, typeof(String));
	this.tab_nombre = (String)Conversiones.GetValueByType(tab_nombre, typeof(String));
	this.tab_estado = (Int32?)Conversiones.GetValueByType(tab_estado, typeof(Int32?));
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
