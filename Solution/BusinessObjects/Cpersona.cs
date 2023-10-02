using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;
namespace BusinessObjects
{
    public class Cpersona
    {
        #region Properties

    	[Data(key = true)]
	public Int32 cper_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 cper_empresa_key { get; set; }
    [Data(key = true, auto = true)]
	public Int32 cper_codigo { get; set; }
	[Data(originalkey = true)]
	public Int32 cper_codigo_key { get; set; }
	public String cper_id { get; set; }
	public String cper_nombre { get; set; }
	public Int32? cper_reporta { get; set; }
	public Int32? cper_orden { get; set; }
	public String cper_tipo { get; set; }
	public Int32? cper_estado { get; set; }
         [Data(noupdate = true)]
	public String crea_usr { get; set; }
         [Data(noupdate = true)]
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Cpersona()
        {
        }

        public  Cpersona( Int32 cper_empresa,Int32 cper_codigo,String cper_id,String cper_nombre,Int32 cper_reporta,Int32 cper_orden,String cper_tipo,Int32 cper_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.cper_empresa = cper_empresa;
	this.cper_codigo = cper_codigo;
    this.cper_empresa_key = cper_empresa;
    this.cper_codigo_key = cper_codigo;
	this.cper_id = cper_id;
	this.cper_nombre = cper_nombre;
	this.cper_reporta = cper_reporta;
	this.cper_orden = cper_orden;
	this.cper_tipo = cper_tipo;
	this.cper_estado = cper_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Cpersona(IDataReader reader)
        {
    	this.cper_empresa = (Int32)reader["cper_empresa"];
	this.cper_codigo = (Int32)reader["cper_codigo"];
    this.cper_empresa_key = (Int32)reader["cper_empresa"];
    this.cper_codigo_key = (Int32)reader["cper_codigo"];
    this.cper_id = reader["cper_id"].ToString(); 
    this.cper_nombre = reader["cper_nombre"].ToString(); 
	this.cper_reporta = (reader["cper_reporta"] != DBNull.Value) ? (Int32?)reader["cper_reporta"] : null;
	this.cper_orden = (reader["cper_orden"] != DBNull.Value) ? (Int32?)reader["cper_orden"] : null;
    this.cper_tipo = reader["cper_tipo"].ToString(); 
	this.cper_estado = (reader["cper_estado"] != DBNull.Value) ? (Int32?)reader["cper_estado"] : null;
    this.crea_usr = reader["crea_usr"].ToString(); 
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
    this.mod_usr = reader["mod_usr"].ToString(); 
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }



        public Cpersona(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object cper_nombre = null;
                object cper_empresa = null;
                object cper_codigo = null;
                object cper_id = null;
                object cper_reporta = null;
                object cper_orden = null;
                object cper_tipo = null;
                object cper_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("cper_nombre", out cper_nombre);
                tmp.TryGetValue("cper_empresa", out cper_empresa);
                tmp.TryGetValue("cper_codigo", out cper_codigo);
                tmp.TryGetValue("cper_id", out cper_id);
                tmp.TryGetValue("cper_reporta", out cper_reporta);
                tmp.TryGetValue("cper_orden", out cper_orden);
                tmp.TryGetValue("cper_tipo", out cper_tipo);
                tmp.TryGetValue("cper_estado", out cper_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.cper_nombre = (String)Conversiones.GetValueByType(cper_nombre, typeof(String));
                this.cper_empresa = (Int32)Conversiones.GetValueByType(cper_empresa, typeof(Int32));
                this.cper_codigo = (Int32)Conversiones.GetValueByType(cper_codigo, typeof(Int32));
                this.cper_id = (String)Conversiones.GetValueByType(cper_id, typeof(String));
                this.cper_reporta = (Int32?)Conversiones.GetValueByType(cper_reporta, typeof(Int32?));
                this.cper_orden = (Int32?)Conversiones.GetValueByType(cper_orden, typeof(Int32?));
                this.cper_tipo = (String)Conversiones.GetValueByType(cper_tipo, typeof(String));
                this.cper_estado = (Int32?)Conversiones.GetValueByType(cper_estado, typeof(Int32?));
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
