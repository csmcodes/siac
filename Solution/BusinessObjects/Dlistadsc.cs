using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace BusinessObjects
{
    public class Dlistadsc
    {
        #region Properties

    	[Data(key = true)]
	public Int32 dlde_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 dlde_empresa_key { get; set; }
	[Data(key = true)]
	public Int32 dlde_listaprecio { get; set; }
	[Data(originalkey = true)]
	public Int32 dlde_listaprecio_key { get; set; }
	[Data(key = true)]
	public Int32 dlde_codigo { get; set; }
	[Data(originalkey = true)]
	public Int32 dlde_codigo_key { get; set; }
	public DateTime dlde_fecha_ini { get; set; }
	public DateTime? dlde_fecha_fin { get; set; }
	public Decimal? dlde_desc_con { get; set; }
	public Decimal? dlde_desc_cre { get; set; }
	public Decimal? dlde_promocion { get; set; }
	public Int32? dlde_estado { get; set; }
         [Data(noupdate = true)]
	public String crea_usr { get; set; }
         [Data(noupdate = true)]
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }
	public Int32? dlde_almacen { get; set; }
	public Int32? dlde_tproducto { get; set; }
	public Int32? dlde_producto { get; set; }
	public Int32? dlde_umedida { get; set; }

              
        #endregion

        #region Constructors


        public  Dlistadsc()
        {
        }

        public  Dlistadsc( Int32 dlde_empresa,Int32 dlde_listaprecio,Int32 dlde_codigo,DateTime dlde_fecha_ini,DateTime dlde_fecha_fin,Decimal dlde_desc_con,Decimal dlde_desc_cre,Decimal dlde_promocion,Int32 dlde_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha,Int32 dlde_almacen,Int32 dlde_tproducto,Int32 dlde_producto,Int32 dlde_umedida)
        {                
    	this.dlde_empresa = dlde_empresa;
	this.dlde_listaprecio = dlde_listaprecio;
	this.dlde_codigo = dlde_codigo;
    this.dlde_empresa_key = dlde_empresa;
    this.dlde_listaprecio_key = dlde_listaprecio;
    this.dlde_codigo_key = dlde_codigo;
	this.dlde_fecha_ini = dlde_fecha_ini;
	this.dlde_fecha_fin = dlde_fecha_fin;
	this.dlde_desc_con = dlde_desc_con;
	this.dlde_desc_cre = dlde_desc_cre;
	this.dlde_promocion = dlde_promocion;
	this.dlde_estado = dlde_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;
	this.dlde_almacen = dlde_almacen;
	this.dlde_tproducto = dlde_tproducto;
	this.dlde_producto = dlde_producto;
	this.dlde_umedida = dlde_umedida;

           
       }

        public  Dlistadsc(IDataReader reader)
        {
    	this.dlde_empresa = (Int32)reader["dlde_empresa"];
	this.dlde_listaprecio = (Int32)reader["dlde_listaprecio"];
	this.dlde_codigo = (Int32)reader["dlde_codigo"];
    this.dlde_empresa_key = (Int32)reader["dlde_empresa"];
    this.dlde_listaprecio_key = (Int32)reader["dlde_listaprecio"];
    this.dlde_codigo_key = (Int32)reader["dlde_codigo"];
	this.dlde_fecha_ini = (DateTime)reader["dlde_fecha_ini"];
	this.dlde_fecha_fin = (reader["dlde_fecha_fin"] != DBNull.Value) ? (DateTime?)reader["dlde_fecha_fin"] : null;
	this.dlde_desc_con = (reader["dlde_desc_con"] != DBNull.Value) ? (Decimal?)reader["dlde_desc_con"] : null;
	this.dlde_desc_cre = (reader["dlde_desc_cre"] != DBNull.Value) ? (Decimal?)reader["dlde_desc_cre"] : null;
	this.dlde_promocion = (reader["dlde_promocion"] != DBNull.Value) ? (Decimal?)reader["dlde_promocion"] : null;
	this.dlde_estado = (reader["dlde_estado"] != DBNull.Value) ? (Int32?)reader["dlde_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
	this.dlde_almacen = (reader["dlde_almacen"] != DBNull.Value) ? (Int32?)reader["dlde_almacen"] : null;
	this.dlde_tproducto = (reader["dlde_tproducto"] != DBNull.Value) ? (Int32?)reader["dlde_tproducto"] : null;
	this.dlde_producto = (reader["dlde_producto"] != DBNull.Value) ? (Int32?)reader["dlde_producto"] : null;
	this.dlde_umedida = (reader["dlde_umedida"] != DBNull.Value) ? (Int32?)reader["dlde_umedida"] : null;

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
