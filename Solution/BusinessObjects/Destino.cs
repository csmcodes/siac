using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Destino
    {
        #region Properties

    	[Data(key = true)]
	public Int32 des_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 des_empresa_key { get; set; }
	[Data(key = true, auto =true)]
	public Int64 des_codigo { get; set; }
	[Data(originalkey = true)]
	public Int64 des_codigo_key { get; set; }
	public Int32? des_persona { get; set; }
	public String des_destino { get; set; }
	public Int32? des_estado { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Destino()
        {
        }

        public  Destino( Int32 des_empresa,Int64 des_codigo,Int32 des_persona,String des_destino,Int32 des_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.des_empresa = des_empresa;
	this.des_codigo = des_codigo;
	this.des_persona = des_persona;
	this.des_destino = des_destino;
	this.des_estado = des_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Destino(IDataReader reader)
        {
    	this.des_empresa = (Int32)reader["des_empresa"];
	this.des_codigo = (Int64)reader["des_codigo"];
	this.des_persona = (reader["des_persona"] != DBNull.Value) ? (Int32?)reader["des_persona"] : null;
	this.des_destino = reader["des_destino"].ToString();
	this.des_estado = (reader["des_estado"] != DBNull.Value) ? (Int32?)reader["des_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Destino(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object des_empresa = null;
	object des_codigo = null;
	object des_persona = null;
	object des_destino = null;
	object des_estado = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("des_empresa", out des_empresa);
	tmp.TryGetValue("des_codigo", out des_codigo);
	tmp.TryGetValue("des_persona", out des_persona);
	tmp.TryGetValue("des_destino", out des_destino);
	tmp.TryGetValue("des_estado", out des_estado);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.des_empresa = (Int32)Conversiones.GetValueByType(des_empresa, typeof(Int32));
	this.des_codigo = (Int64)Conversiones.GetValueByType(des_codigo, typeof(Int64));
	this.des_persona = (Int32?)Conversiones.GetValueByType(des_persona, typeof(Int32?));
	this.des_destino = (String)Conversiones.GetValueByType(des_destino, typeof(String));
	this.des_estado = (Int32?)Conversiones.GetValueByType(des_estado, typeof(Int32?));
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
