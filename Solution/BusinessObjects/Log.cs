using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Log
    {
        #region Properties

    [Data(key = true)]
	public Int32 log_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 log_empresa_key { get; set; }
	[Data(key = true )]
	public DateTime log_fecha { get; set; }
	[Data(originalkey = true)]
	public DateTime log_fecha_key { get; set; }
	public String log_data { get; set; }
              
        #endregion

        #region Constructors


        public  Log()
        {
        }

        public Log(Int32 log_empresa, DateTime log_fecha, String log_data)
        {                
    	this.log_empresa = log_empresa;
	this.log_fecha= log_fecha;
    this.log_empresa_key = log_empresa;
    this.log_fecha_key= log_fecha;
	this.log_data= log_data;	
           
       }

        public  Log(IDataReader reader)
        {
    	this.log_empresa = (Int32)reader["log_empresa"];
	this.log_fecha = (DateTime)reader["log_fecha"];
    this.log_empresa_key = (Int32)reader["log_empresa"];
    this.log_fecha_key = (DateTime)reader["log_fecha"];
	this.log_data = reader["log_data"].ToString();
	        
        }

        public  Log (object objeto)
        {
           
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object fecha = null;
                object empresa = null;
                object empresakey = null;
                object fechakey = null;
                object data = null;
                

                tmp.TryGetValue("log_fecha", out fecha);
                tmp.TryGetValue("log_empresa", out empresa);
                tmp.TryGetValue("log_fecha_key", out fechakey);
                tmp.TryGetValue("log_empresa_key", out empresakey);
                tmp.TryGetValue("log_data", out data);
                if (empresa != null && !empresa.Equals(""))
                {
                    this.log_empresa = int.Parse(empresa.ToString());
                }
                if (empresakey != null && !empresakey.Equals(""))
                {
                    this.log_empresa_key = int.Parse(empresakey.ToString());
                }
                if (fecha != null && !fecha.Equals(""))
                {
                    this.log_fecha= DateTime.Parse(fecha.ToString());
                }
                if (fechakey != null && !fechakey.Equals(""))
                {
                    this.log_fecha_key = DateTime.Parse(fechakey.ToString());
                }

                this.log_data = (string)data;
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
