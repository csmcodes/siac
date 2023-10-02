using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Rubro
    {
        #region Properties

    	[Data(key = true)]
	public Int32 rub_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 rub_empresa_key { get; set; }
	[Data(key = true, auto=true)]
	public Int32 rub_codigo { get; set; }
	[Data(originalkey = true)]
	public Int32 rub_codigo_key { get; set; }
	public String rub_id { get; set; }
	public String rub_nombre { get; set; }
	public String rub_tipo { get; set; }
	public Int32? rub_estado { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Rubro()
        {
        }

        public  Rubro( Int32 rub_empresa,Int32 rub_codigo,String rub_id,String rub_nombre,String rub_tipo,Int32 rub_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.rub_empresa = rub_empresa;
	this.rub_codigo = rub_codigo;
	this.rub_id = rub_id;
	this.rub_nombre = rub_nombre;
	this.rub_tipo = rub_tipo;
	this.rub_estado = rub_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Rubro(IDataReader reader)
        {
    	this.rub_empresa = (Int32)reader["rub_empresa"];
	this.rub_codigo = (Int32)reader["rub_codigo"];
	this.rub_id = reader["rub_id"].ToString();
	this.rub_nombre = reader["rub_nombre"].ToString();
	this.rub_tipo = reader["rub_tipo"].ToString();
	this.rub_estado = (reader["rub_estado"] != DBNull.Value) ? (Int32?)reader["rub_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Rubro(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object rub_empresa = null;
	object rub_codigo = null;
	object rub_id = null;
	object rub_nombre = null;
	object rub_tipo = null;
	object rub_estado = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("rub_empresa", out rub_empresa);
	tmp.TryGetValue("rub_codigo", out rub_codigo);
	tmp.TryGetValue("rub_id", out rub_id);
	tmp.TryGetValue("rub_nombre", out rub_nombre);
	tmp.TryGetValue("rub_tipo", out rub_tipo);
	tmp.TryGetValue("rub_estado", out rub_estado);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.rub_empresa = (Int32)Conversiones.GetValueByType(rub_empresa, typeof(Int32));
	this.rub_codigo = (Int32)Conversiones.GetValueByType(rub_codigo, typeof(Int32));
	this.rub_id = (String)Conversiones.GetValueByType(rub_id, typeof(String));
	this.rub_nombre = (String)Conversiones.GetValueByType(rub_nombre, typeof(String));
	this.rub_tipo = (String)Conversiones.GetValueByType(rub_tipo, typeof(String));
	this.rub_estado = (Int32?)Conversiones.GetValueByType(rub_estado, typeof(Int32?));
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
