using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;
namespace BusinessObjects
{
    public class Tpersona
    {
        #region Properties

    	[Data(key = true)]
	public Int32 tper_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 tper_empresa_key { get; set; }
    [Data(key = true, auto = true)]
	public Int32 tper_codigo { get; set; }
	[Data(originalkey = true)]
	public Int32 tper_codigo_key { get; set; }
	public String tper_id { get; set; }
	public String tper_nombre { get; set; }
	public Int32? tper_reporta { get; set; }
	public Int32? tper_orden { get; set; }
	public String tper_tipo { get; set; }
	public Int32? tper_estado { get; set; }
         [Data(noupdate = true)]
	public String crea_usr { get; set; }
         [Data(noupdate = true)]
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Tpersona()
        {
        }

        public  Tpersona( Int32 tper_empresa,Int32 tper_codigo,String tper_id,String tper_nombre,Int32 tper_reporta,Int32 tper_orden,String tper_tipo,Int32 tper_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.tper_empresa = tper_empresa;
	this.tper_codigo = tper_codigo;
    this.tper_empresa_key = tper_empresa;
    this.tper_codigo_key = tper_codigo;
	this.tper_id = tper_id;
	this.tper_nombre = tper_nombre;
	this.tper_reporta = tper_reporta;
	this.tper_orden = tper_orden;
	this.tper_tipo = tper_tipo;
	this.tper_estado = tper_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Tpersona(IDataReader reader)
        {
    	this.tper_empresa = (Int32)reader["tper_empresa"];
	this.tper_codigo = (Int32)reader["tper_codigo"];
    this.tper_empresa_key = (Int32)reader["tper_empresa"];
    this.tper_codigo_key = (Int32)reader["tper_codigo"];
	this.tper_id = reader["tper_id"].ToString();
    this.tper_nombre = reader["tper_nombre"].ToString();
	this.tper_reporta = (reader["tper_reporta"] != DBNull.Value) ? (Int32?)reader["tper_reporta"] : null;
	this.tper_orden = (reader["tper_orden"] != DBNull.Value) ? (Int32?)reader["tper_orden"] : null;
    this.tper_tipo = reader["tper_tipo"].ToString();
	this.tper_estado = (reader["tper_estado"] != DBNull.Value) ? (Int32?)reader["tper_estado"] : null;
    this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
    this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }

        public Tpersona(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object tper_empresa = null;
                object tper_codigo = null;
                object tper_nombre = null;
                object tper_reporta = null;
                object tper_orden = null;
                object tper_tipo = null;
                object tper_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object tper_id = null;


                tmp.TryGetValue("tper_empresa", out tper_empresa);
                tmp.TryGetValue("tper_codigo", out tper_codigo);
                tmp.TryGetValue("tper_nombre", out tper_nombre);
                tmp.TryGetValue("tper_reporta", out tper_reporta);
                tmp.TryGetValue("tper_orden", out tper_orden);
                tmp.TryGetValue("tper_tipo", out tper_tipo);
                tmp.TryGetValue("tper_estado", out tper_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("tper_id", out tper_id);


                this.tper_empresa = (Int32)Conversiones.GetValueByType(tper_empresa, typeof(Int32));
                this.tper_codigo = (Int32)Conversiones.GetValueByType(tper_codigo, typeof(Int32));
                this.tper_nombre = (String)Conversiones.GetValueByType(tper_nombre, typeof(String));
                this.tper_reporta = (Int32?)Conversiones.GetValueByType(tper_reporta, typeof(Int32?));
                this.tper_orden = (Int32?)Conversiones.GetValueByType(tper_orden, typeof(Int32?));
                this.tper_tipo = (String)Conversiones.GetValueByType(tper_tipo, typeof(String));
                this.tper_estado = (Int32?)Conversiones.GetValueByType(tper_estado, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.tper_id = (String)Conversiones.GetValueByType(tper_id, typeof(String));

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
