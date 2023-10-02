using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace BusinessObjects
{
    public class Mensajedestino
    {
        #region Properties

    	[Data(key = true)]
	public Int32 msd_mensaje { get; set; }
	[Data(originalkey = true)]
	public Int32 msd_mensaje_key { get; set; }
	[Data(key = true)]
	public String msd_usuario { get; set; }
	[Data(originalkey = true)]
	public String msd_usuario_key { get; set; }
	[Data(key = true)]
	public Int32 msd_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 msd_empresa_key { get; set; }
	public Int32? msd_estado { get; set; }
        [Data(noupdate = true)]
	public String crea_usr { get; set; }
        [Data(noupdate = true)]
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Mensajedestino()
        {
        }

        public  Mensajedestino( Int32 msd_mensaje,String msd_usuario,Int32 msd_empresa,Int32 msd_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.msd_mensaje = msd_mensaje;
	this.msd_usuario = msd_usuario;
	this.msd_empresa = msd_empresa;
    this.msd_mensaje_key= msd_mensaje;
    this.msd_usuario_key = msd_usuario;
    this.msd_empresa_key = msd_empresa;
	this.msd_estado = msd_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Mensajedestino(IDataReader reader)
        {
    	this.msd_mensaje = (Int32)reader["msd_mensaje"];
	this.msd_usuario = reader["msd_usuario"].ToString();
	this.msd_empresa = (Int32)reader["msd_empresa"];
    this.msd_mensaje_key = (Int32)reader["msd_mensaje"];
    this.msd_usuario_key = reader["msd_usuario"].ToString();
    this.msd_empresa_key = (Int32)reader["msd_empresa"];
	this.msd_estado = (reader["msd_estado"] != DBNull.Value) ? (Int32?)reader["msd_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

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
