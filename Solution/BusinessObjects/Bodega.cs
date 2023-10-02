using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Bodega
    {
        #region Properties

    	[Data(key = true)]
	public Int32 bod_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 bod_empresa_key { get; set; }
	[Data(key = true, auto =true)]
	public Int32 bod_codigo { get; set; }
	[Data(originalkey = true)]
	public Int32 bod_codigo_key { get; set; }
	public String bod_id { get; set; }
	public String bod_nombre { get; set; }
	public Int32? bod_pais { get; set; }
	public Int32? bod_provincia { get; set; }
	public Int32? bod_canton { get; set; }
	public Int32? bod_custodio { get; set; }
	public Int32? bod_consignacion { get; set; }
	public Int32? bod_temporal { get; set; }
	public Int32? bod_almacen { get; set; }
	public Int32? bod_estado { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }
	public String crea_ip { get; set; }
	public String mod_ip { get; set; }

              
        #endregion

        #region Constructors


        public  Bodega()
        {
        }

        public  Bodega( Int32 bod_empresa,Int32 bod_codigo,String bod_id,String bod_nombre,Int32 bod_pais,Int32 bod_provincia,Int32 bod_canton,Int32 bod_custodio,Int32 bod_consignacion,Int32 bod_temporal,Int32 bod_almacen,Int32 bod_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha,String crea_ip,String mod_ip)
        {                
    	this.bod_empresa = bod_empresa;
	this.bod_codigo = bod_codigo;
	this.bod_id = bod_id;
	this.bod_nombre = bod_nombre;
	this.bod_pais = bod_pais;
	this.bod_provincia = bod_provincia;
	this.bod_canton = bod_canton;
	this.bod_custodio = bod_custodio;
	this.bod_consignacion = bod_consignacion;
	this.bod_temporal = bod_temporal;
	this.bod_almacen = bod_almacen;
	this.bod_estado = bod_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;
	this.crea_ip = crea_ip;
	this.mod_ip = mod_ip;

           
       }

        public  Bodega(IDataReader reader)
        {
    	this.bod_empresa = (Int32)reader["bod_empresa"];
	this.bod_codigo = (Int32)reader["bod_codigo"];
	this.bod_id = reader["bod_id"].ToString();
	this.bod_nombre = reader["bod_nombre"].ToString();
	this.bod_pais = (reader["bod_pais"] != DBNull.Value) ? (Int32?)reader["bod_pais"] : null;
	this.bod_provincia = (reader["bod_provincia"] != DBNull.Value) ? (Int32?)reader["bod_provincia"] : null;
	this.bod_canton = (reader["bod_canton"] != DBNull.Value) ? (Int32?)reader["bod_canton"] : null;
	this.bod_custodio = (reader["bod_custodio"] != DBNull.Value) ? (Int32?)reader["bod_custodio"] : null;
	this.bod_consignacion = (reader["bod_consignacion"] != DBNull.Value) ? (Int32?)reader["bod_consignacion"] : null;
	this.bod_temporal = (reader["bod_temporal"] != DBNull.Value) ? (Int32?)reader["bod_temporal"] : null;
	this.bod_almacen = (reader["bod_almacen"] != DBNull.Value) ? (Int32?)reader["bod_almacen"] : null;
	this.bod_estado = (reader["bod_estado"] != DBNull.Value) ? (Int32?)reader["bod_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
	this.crea_ip = reader["crea_ip"].ToString();
	this.mod_ip = reader["mod_ip"].ToString();

        }


        public Bodega(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object bod_empresa = null;
	object bod_codigo = null;
	object bod_id = null;
	object bod_nombre = null;
	object bod_pais = null;
	object bod_provincia = null;
	object bod_canton = null;
	object bod_custodio = null;
	object bod_consignacion = null;
	object bod_temporal = null;
	object bod_almacen = null;
	object bod_estado = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;
	object crea_ip = null;
	object mod_ip = null;


                	tmp.TryGetValue("bod_empresa", out bod_empresa);
	tmp.TryGetValue("bod_codigo", out bod_codigo);
	tmp.TryGetValue("bod_id", out bod_id);
	tmp.TryGetValue("bod_nombre", out bod_nombre);
	tmp.TryGetValue("bod_pais", out bod_pais);
	tmp.TryGetValue("bod_provincia", out bod_provincia);
	tmp.TryGetValue("bod_canton", out bod_canton);
	tmp.TryGetValue("bod_custodio", out bod_custodio);
	tmp.TryGetValue("bod_consignacion", out bod_consignacion);
	tmp.TryGetValue("bod_temporal", out bod_temporal);
	tmp.TryGetValue("bod_almacen", out bod_almacen);
	tmp.TryGetValue("bod_estado", out bod_estado);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);
	tmp.TryGetValue("crea_ip", out crea_ip);
	tmp.TryGetValue("mod_ip", out mod_ip);


                	this.bod_empresa = (Int32)Conversiones.GetValueByType(bod_empresa, typeof(Int32));
	this.bod_codigo = (Int32)Conversiones.GetValueByType(bod_codigo, typeof(Int32));
	this.bod_id = (String)Conversiones.GetValueByType(bod_id, typeof(String));
	this.bod_nombre = (String)Conversiones.GetValueByType(bod_nombre, typeof(String));
	this.bod_pais = (Int32?)Conversiones.GetValueByType(bod_pais, typeof(Int32?));
	this.bod_provincia = (Int32?)Conversiones.GetValueByType(bod_provincia, typeof(Int32?));
	this.bod_canton = (Int32?)Conversiones.GetValueByType(bod_canton, typeof(Int32?));
	this.bod_custodio = (Int32?)Conversiones.GetValueByType(bod_custodio, typeof(Int32?));
	this.bod_consignacion = (Int32?)Conversiones.GetValueByType(bod_consignacion, typeof(Int32?));
	this.bod_temporal = (Int32?)Conversiones.GetValueByType(bod_temporal, typeof(Int32?));
	this.bod_almacen = (Int32?)Conversiones.GetValueByType(bod_almacen, typeof(Int32?));
	this.bod_estado = (Int32?)Conversiones.GetValueByType(bod_estado, typeof(Int32?));
	this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
	this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
	this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
	this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
	this.crea_ip = (String)Conversiones.GetValueByType(crea_ip, typeof(String));
	this.mod_ip = (String)Conversiones.GetValueByType(mod_ip, typeof(String));

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
