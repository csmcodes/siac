using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Electronico
    {
        #region Properties

    	[Data(key = true)]
	public Int32 ele_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 ele_empresa_key { get; set; }
	[Data(key = true)]
	public String ele_clave { get; set; }
	[Data(originalkey = true)]
	public String ele_clave_key { get; set; }
	public DateTime? ele_fecha_autoriza { get; set; }
	public DateTime? ele_fecha_carga { get; set; }
	public String ele_estadoelectronico { get; set; }
	public String ele_ambiente { get; set; }
	public String ele_xml { get; set; }
	public String ele_mensaje { get; set; }
	public Int64? ele_comprobante { get; set; }
	public String ele_respuesta { get; set; }
	public String ele_carga { get; set; }
		public String ele_documento { get; set; }
		public Int32? ele_estado { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Electronico()
        {
        }

        public  Electronico( Int32 ele_empresa,String ele_clave,DateTime ele_fecha_autoriza,DateTime ele_fecha_carga,String ele_estadoelectronico,String ele_ambiente,String ele_xml,String ele_mensaje,Int64 ele_comprobante,String ele_respuesta,String ele_carga,Int32 ele_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.ele_empresa = ele_empresa;
	this.ele_clave = ele_clave;
	this.ele_fecha_autoriza = ele_fecha_autoriza;
	this.ele_fecha_carga = ele_fecha_carga;
	this.ele_estadoelectronico = ele_estadoelectronico;
	this.ele_ambiente = ele_ambiente;
	this.ele_xml = ele_xml;
	this.ele_mensaje = ele_mensaje;
	this.ele_comprobante = ele_comprobante;
	this.ele_respuesta = ele_respuesta;
	this.ele_carga = ele_carga;
	this.ele_estado = ele_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Electronico(IDataReader reader)
        {
    	this.ele_empresa = (Int32)reader["ele_empresa"];
	this.ele_clave = reader["ele_clave"].ToString();
	this.ele_fecha_autoriza = (reader["ele_fecha_autoriza"] != DBNull.Value) ? (DateTime?)reader["ele_fecha_autoriza"] : null;
	this.ele_fecha_carga = (reader["ele_fecha_carga"] != DBNull.Value) ? (DateTime?)reader["ele_fecha_carga"] : null;
	this.ele_estadoelectronico = reader["ele_estadoelectronico"].ToString();
	this.ele_ambiente = reader["ele_ambiente"].ToString();
	this.ele_xml = reader["ele_xml"].ToString();
	this.ele_mensaje = reader["ele_mensaje"].ToString();
	this.ele_comprobante = (reader["ele_comprobante"] != DBNull.Value) ? (Int64?)reader["ele_comprobante"] : null;
	this.ele_respuesta = reader["ele_respuesta"].ToString();
	this.ele_carga = reader["ele_carga"].ToString();
			this.ele_documento = reader["ele_documento"].ToString();
			this.ele_estado = (reader["ele_estado"] != DBNull.Value) ? (Int32?)reader["ele_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Electronico(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object ele_empresa = null;
	object ele_clave = null;
	object ele_fecha_autoriza = null;
	object ele_fecha_carga = null;
	object ele_estadoelectronico = null;
	object ele_ambiente = null;
	object ele_xml = null;
	object ele_mensaje = null;
	object ele_comprobante = null;
	object ele_respuesta = null;
	object ele_carga = null;
				object ele_documento = null;
				object ele_estado = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("ele_empresa", out ele_empresa);
	tmp.TryGetValue("ele_clave", out ele_clave);
	tmp.TryGetValue("ele_fecha_autoriza", out ele_fecha_autoriza);
	tmp.TryGetValue("ele_fecha_carga", out ele_fecha_carga);
	tmp.TryGetValue("ele_estadoelectronico", out ele_estadoelectronico);
	tmp.TryGetValue("ele_ambiente", out ele_ambiente);
	tmp.TryGetValue("ele_xml", out ele_xml);
	tmp.TryGetValue("ele_mensaje", out ele_mensaje);
	tmp.TryGetValue("ele_comprobante", out ele_comprobante);
	tmp.TryGetValue("ele_respuesta", out ele_respuesta);
	tmp.TryGetValue("ele_carga", out ele_carga);
				tmp.TryGetValue("ele_documento", out ele_documento);
				tmp.TryGetValue("ele_estado", out ele_estado);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.ele_empresa = (Int32)Conversiones.GetValueByType(ele_empresa, typeof(Int32));
	this.ele_clave = (String)Conversiones.GetValueByType(ele_clave, typeof(String));
	this.ele_fecha_autoriza = (DateTime?)Conversiones.GetValueByType(ele_fecha_autoriza, typeof(DateTime?));
	this.ele_fecha_carga = (DateTime?)Conversiones.GetValueByType(ele_fecha_carga, typeof(DateTime?));
	this.ele_estadoelectronico = (String)Conversiones.GetValueByType(ele_estadoelectronico, typeof(String));
	this.ele_ambiente = (String)Conversiones.GetValueByType(ele_ambiente, typeof(String));
	this.ele_xml = (String)Conversiones.GetValueByType(ele_xml, typeof(String));
	this.ele_mensaje = (String)Conversiones.GetValueByType(ele_mensaje, typeof(String));
	this.ele_comprobante = (Int64?)Conversiones.GetValueByType(ele_comprobante, typeof(Int64?));
	this.ele_respuesta = (String)Conversiones.GetValueByType(ele_respuesta, typeof(String));
	this.ele_carga = (String)Conversiones.GetValueByType(ele_carga, typeof(String));
				this.ele_documento = (String)Conversiones.GetValueByType(ele_documento, typeof(String));
				this.ele_estado = (Int32?)Conversiones.GetValueByType(ele_estado, typeof(Int32?));
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
