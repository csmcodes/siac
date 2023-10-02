using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Electronicocarga
    {
        #region Properties

    	[Data(key = true)]
	public Int32 eca_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 eca_empresa_key { get; set; }
	[Data(key = true)]
	public String eca_id { get; set; }
	[Data(originalkey = true)]
	public String eca_id_key { get; set; }
	public DateTime? eca_inicio { get; set; }
	public DateTime? eca_fin { get; set; }
	public String eca_archivo { get; set; }
	public String eca_claves { get; set; }
	public Int32? eca_registros { get; set; }
	public Int32? eca_descargados { get; set; }
	public Int32? eca_creados { get; set; }
	public Int32? eca_error { get; set; }
	public Int32? eca_estado { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Electronicocarga()
        {
        }

        public  Electronicocarga( Int32 eca_empresa,String eca_id,DateTime eca_inicio,DateTime eca_fin,String eca_archivo,String eca_claves,Int32 eca_registros,Int32 eca_descargados,Int32 eca_creados,Int32 eca_error,Int32 eca_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.eca_empresa = eca_empresa;
	this.eca_id = eca_id;
	this.eca_inicio = eca_inicio;
	this.eca_fin = eca_fin;
	this.eca_archivo = eca_archivo;
	this.eca_claves = eca_claves;
	this.eca_registros = eca_registros;
	this.eca_descargados = eca_descargados;
	this.eca_creados = eca_creados;
	this.eca_error = eca_error;
	this.eca_estado = eca_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Electronicocarga(IDataReader reader)
        {
    	this.eca_empresa = (Int32)reader["eca_empresa"];
	this.eca_id = reader["eca_id"].ToString();
	this.eca_inicio = (reader["eca_inicio"] != DBNull.Value) ? (DateTime?)reader["eca_inicio"] : null;
	this.eca_fin = (reader["eca_fin"] != DBNull.Value) ? (DateTime?)reader["eca_fin"] : null;
	this.eca_archivo = reader["eca_archivo"].ToString();
	this.eca_claves = reader["eca_claves"].ToString();
	this.eca_registros = (reader["eca_registros"] != DBNull.Value) ? (Int32?)reader["eca_registros"] : null;
	this.eca_descargados = (reader["eca_descargados"] != DBNull.Value) ? (Int32?)reader["eca_descargados"] : null;
	this.eca_creados = (reader["eca_creados"] != DBNull.Value) ? (Int32?)reader["eca_creados"] : null;
	this.eca_error = (reader["eca_error"] != DBNull.Value) ? (Int32?)reader["eca_error"] : null;
	this.eca_estado = (reader["eca_estado"] != DBNull.Value) ? (Int32?)reader["eca_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Electronicocarga(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object eca_empresa = null;
	object eca_id = null;
	object eca_inicio = null;
	object eca_fin = null;
	object eca_archivo = null;
	object eca_claves = null;
	object eca_registros = null;
	object eca_descargados = null;
	object eca_creados = null;
	object eca_error = null;
	object eca_estado = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("eca_empresa", out eca_empresa);
	tmp.TryGetValue("eca_id", out eca_id);
	tmp.TryGetValue("eca_inicio", out eca_inicio);
	tmp.TryGetValue("eca_fin", out eca_fin);
	tmp.TryGetValue("eca_archivo", out eca_archivo);
	tmp.TryGetValue("eca_claves", out eca_claves);
	tmp.TryGetValue("eca_registros", out eca_registros);
	tmp.TryGetValue("eca_descargados", out eca_descargados);
	tmp.TryGetValue("eca_creados", out eca_creados);
	tmp.TryGetValue("eca_error", out eca_error);
	tmp.TryGetValue("eca_estado", out eca_estado);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.eca_empresa = (Int32)Conversiones.GetValueByType(eca_empresa, typeof(Int32));
	this.eca_id = (String)Conversiones.GetValueByType(eca_id, typeof(String));
	this.eca_inicio = (DateTime?)Conversiones.GetValueByType(eca_inicio, typeof(DateTime?));
	this.eca_fin = (DateTime?)Conversiones.GetValueByType(eca_fin, typeof(DateTime?));
	this.eca_archivo = (String)Conversiones.GetValueByType(eca_archivo, typeof(String));
	this.eca_claves = (String)Conversiones.GetValueByType(eca_claves, typeof(String));
	this.eca_registros = (Int32?)Conversiones.GetValueByType(eca_registros, typeof(Int32?));
	this.eca_descargados = (Int32?)Conversiones.GetValueByType(eca_descargados, typeof(Int32?));
	this.eca_creados = (Int32?)Conversiones.GetValueByType(eca_creados, typeof(Int32?));
	this.eca_error = (Int32?)Conversiones.GetValueByType(eca_error, typeof(Int32?));
	this.eca_estado = (Int32?)Conversiones.GetValueByType(eca_estado, typeof(Int32?));
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
