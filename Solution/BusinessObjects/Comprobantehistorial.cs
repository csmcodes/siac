using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Comprobantehistorial
    {
        #region Properties

    	[Data(key = true)]
	public Int32 coh_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 coh_empresa_key { get; set; }
	[Data(key = true, auto =true)]
	public Int64 coh_serial { get; set; }
	[Data(originalkey = true)]
	public Int64 coh_serial_key { get; set; }
	public Int64 coh_codigo { get; set; }
	public DateTime coh_fecha { get; set; }
	public String coh_stack { get; set; }
	public String coh_data { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Comprobantehistorial()
        {
        }

        public  Comprobantehistorial( Int32 coh_empresa,Int64 coh_serial,Int64 coh_codigo,DateTime coh_fecha,String coh_stack,String coh_data,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.coh_empresa = coh_empresa;
	this.coh_serial = coh_serial;
	this.coh_codigo = coh_codigo;
	this.coh_fecha = coh_fecha;
	this.coh_stack = coh_stack;
	this.coh_data = coh_data;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Comprobantehistorial(IDataReader reader)
        {
    	this.coh_empresa = (Int32)reader["coh_empresa"];
	this.coh_serial = (Int64)reader["coh_serial"];
	this.coh_codigo = (Int64)reader["coh_codigo"];
	this.coh_fecha = (DateTime)reader["coh_fecha"];
	this.coh_stack = reader["coh_stack"].ToString();
	this.coh_data = reader["coh_data"].ToString();
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Comprobantehistorial(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object coh_empresa = null;
	object coh_serial = null;
	object coh_codigo = null;
	object coh_fecha = null;
	object coh_stack = null;
	object coh_data = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("coh_empresa", out coh_empresa);
	tmp.TryGetValue("coh_serial", out coh_serial);
	tmp.TryGetValue("coh_codigo", out coh_codigo);
	tmp.TryGetValue("coh_fecha", out coh_fecha);
	tmp.TryGetValue("coh_stack", out coh_stack);
	tmp.TryGetValue("coh_data", out coh_data);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.coh_empresa = (Int32)Conversiones.GetValueByType(coh_empresa, typeof(Int32));
	this.coh_serial = (Int64)Conversiones.GetValueByType(coh_serial, typeof(Int64));
	this.coh_codigo = (Int64)Conversiones.GetValueByType(coh_codigo, typeof(Int64));
	this.coh_fecha = (DateTime)Conversiones.GetValueByType(coh_fecha, typeof(DateTime));
	this.coh_stack = (String)Conversiones.GetValueByType(coh_stack, typeof(String));
	this.coh_data = (String)Conversiones.GetValueByType(coh_data, typeof(String));
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