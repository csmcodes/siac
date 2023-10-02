using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Retdato
    {
        #region Properties

    	[Data(key = true)]
	public Int32 rtd_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 rtd_empresa_key { get; set; }
	[Data(key = true)]
	public Int32 rtd_tablacoa { get; set; }
	[Data(originalkey = true)]
	public Int32 rtd_tablacoa_key { get; set; }
      [Data(key = true, auto = true)]
	public Int32 rtd_codigo { get; set; }
	[Data(originalkey = true)]
	public Int32 rtd_codigo_key { get; set; }
	public String rtd_id { get; set; }
	public String rtd_campo { get; set; }
	public Int32? rtd_estado { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Retdato()
        {
        }

        public  Retdato( Int32 rtd_empresa,Int32 rtd_tablacoa,Int32 rtd_codigo,String rtd_id,String rtd_campo,Int32 rtd_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.rtd_empresa = rtd_empresa;
	this.rtd_tablacoa = rtd_tablacoa;
	this.rtd_codigo = rtd_codigo;
    this.rtd_empresa_key = rtd_empresa;
    this.rtd_tablacoa_key = rtd_tablacoa;
    this.rtd_codigo_key = rtd_codigo;
	this.rtd_id = rtd_id;
	this.rtd_campo = rtd_campo;
	this.rtd_estado = rtd_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Retdato(IDataReader reader)
        {
    	this.rtd_empresa = (Int32)reader["rtd_empresa"];
	this.rtd_tablacoa = (Int32)reader["rtd_tablacoa"];
	this.rtd_codigo = (Int32)reader["rtd_codigo"];
    this.rtd_empresa_key = (Int32)reader["rtd_empresa"];
    this.rtd_tablacoa_key = (Int32)reader["rtd_tablacoa"];
    this.rtd_codigo_key = (Int32)reader["rtd_codigo"];
	this.rtd_id = reader["rtd_id"].ToString();
	this.rtd_campo = reader["rtd_campo"].ToString();
	this.rtd_estado = (reader["rtd_estado"] != DBNull.Value) ? (Int32?)reader["rtd_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Retdato(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object rtd_empresa = null;
	object rtd_tablacoa = null;
	object rtd_codigo = null;
    object rtd_empresa_key = null;
    object rtd_tablacoa_key = null;
    object rtd_codigo_key = null;
	object rtd_id = null;
	object rtd_campo = null;
	object rtd_estado = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("rtd_empresa", out rtd_empresa);
	tmp.TryGetValue("rtd_tablacoa", out rtd_tablacoa);
	tmp.TryGetValue("rtd_codigo", out rtd_codigo);
    tmp.TryGetValue("rtd_empresa_key", out rtd_empresa_key);
    tmp.TryGetValue("rtd_tablacoa_key", out rtd_tablacoa_key);
    tmp.TryGetValue("rtd_codigo_key", out rtd_codigo_key);
	tmp.TryGetValue("rtd_id", out rtd_id);
	tmp.TryGetValue("rtd_campo", out rtd_campo);
	tmp.TryGetValue("rtd_estado", out rtd_estado);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.rtd_empresa = (Int32)Conversiones.GetValueByType(rtd_empresa, typeof(Int32));
	this.rtd_tablacoa = (Int32)Conversiones.GetValueByType(rtd_tablacoa, typeof(Int32));
	this.rtd_codigo = (Int32)Conversiones.GetValueByType(rtd_codigo, typeof(Int32));
    this.rtd_empresa_key = (Int32)Conversiones.GetValueByType(rtd_empresa_key, typeof(Int32));
    this.rtd_tablacoa_key = (Int32)Conversiones.GetValueByType(rtd_tablacoa_key, typeof(Int32));
    this.rtd_codigo_key = (Int32)Conversiones.GetValueByType(rtd_codigo_key, typeof(Int32));
	this.rtd_id = (String)Conversiones.GetValueByType(rtd_id, typeof(String));
	this.rtd_campo = (String)Conversiones.GetValueByType(rtd_campo, typeof(String));
	this.rtd_estado = (Int32?)Conversiones.GetValueByType(rtd_estado, typeof(Int32?));
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
