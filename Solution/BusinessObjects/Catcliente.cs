using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Catcliente
    {
        #region Properties

    	[Data(key = true)]
	public Int32 cat_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 cat_empresa_key { get; set; }
    [Data(key = true, auto = true)]
	public Int32 cat_codigo { get; set; }
	[Data(originalkey = true)]
	public Int32 cat_codigo_key { get; set; }
	public String cat_id { get; set; }
	public String cat_nombre { get; set; }
	public Int32 cat_tipo { get; set; }
	public Int32? cat_estado { get; set; }
        [Data(noupdate = true)]
	public String crea_usr { get; set; }
        [Data(noupdate = true)]
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Catcliente()
        {
        }

        public  Catcliente( Int32 cat_empresa,Int32 cat_codigo,String cat_id,String cat_nombre,Int32 cat_tipo,Int32 cat_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.cat_empresa = cat_empresa;
	this.cat_codigo = cat_codigo;
    this.cat_empresa_key = cat_empresa;
    this.cat_codigo_key = cat_codigo;
	this.cat_id = cat_id;
	this.cat_nombre = cat_nombre;
	this.cat_tipo = cat_tipo;
	this.cat_estado = cat_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Catcliente(IDataReader reader)
        {
    	this.cat_empresa = (Int32)reader["cat_empresa"];
	this.cat_codigo = (Int32)reader["cat_codigo"];
    this.cat_empresa_key = (Int32)reader["cat_empresa"];
    this.cat_codigo_key = (Int32)reader["cat_codigo"];
	this.cat_id = reader["cat_id"].ToString();
	this.cat_nombre = reader["cat_nombre"].ToString();
	this.cat_tipo = (Int32)reader["cat_tipo"];
	this.cat_estado = (reader["cat_estado"] != DBNull.Value) ? (Int32?)reader["cat_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Catcliente(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object cat_empresa = null;
	object cat_codigo = null;
    object cat_empresa_key = null;
    object cat_codigo_key = null;
	object cat_id = null;
	object cat_nombre = null;
	object cat_tipo = null;
	object cat_estado = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("cat_empresa", out cat_empresa);
	tmp.TryGetValue("cat_codigo", out cat_codigo);
    tmp.TryGetValue("cat_empresa_key", out cat_empresa);
    tmp.TryGetValue("cat_codigo_key", out cat_codigo);
	tmp.TryGetValue("cat_id", out cat_id);
	tmp.TryGetValue("cat_nombre", out cat_nombre);
	tmp.TryGetValue("cat_tipo", out cat_tipo);
	tmp.TryGetValue("cat_estado", out cat_estado);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.cat_empresa = (Int32)Conversiones.GetValueByType(cat_empresa, typeof(Int32));
	this.cat_codigo = (Int32)Conversiones.GetValueByType(cat_codigo, typeof(Int32));
    this.cat_empresa_key = (Int32)Conversiones.GetValueByType(cat_empresa_key, typeof(Int32));
    this.cat_codigo_key = (Int32)Conversiones.GetValueByType(cat_codigo_key, typeof(Int32));
	this.cat_id = (String)Conversiones.GetValueByType(cat_id, typeof(String));
	this.cat_nombre = (String)Conversiones.GetValueByType(cat_nombre, typeof(String));
	this.cat_tipo = (Int32)Conversiones.GetValueByType(cat_tipo, typeof(Int32));
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
