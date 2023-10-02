using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Impresion
    {
        #region Properties

    	[Data(key = true)]
	public Int32 imp_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 imp_empresa_key { get; set; }
	[Data(key = true)]
	public Int32 imp_almacen { get; set; }
	[Data(originalkey = true)]
	public Int32 imp_almacen_key { get; set; }
	[Data(key = true)]
	public Int32 imp_pventa { get; set; }
	[Data(originalkey = true)]
	public Int32 imp_pventa_key { get; set; }
	[Data(key = true)]
	public Int32 imp_tipodoc { get; set; }
	[Data(originalkey = true)]
	public Int32 imp_tipodoc_key { get; set; }
	public String imp_impresora { get; set; }
	public Int32? imp_confirmacion { get; set; }
	public Int32? imp_estado { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Impresion()
        {
        }

        public  Impresion( Int32 imp_empresa,Int32 imp_almacen,Int32 imp_pventa,Int32 imp_tipodoc,String imp_impresora,Int32 imp_confirmacion,Int32 imp_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.imp_empresa = imp_empresa;
	this.imp_almacen = imp_almacen;
	this.imp_pventa = imp_pventa;
	this.imp_tipodoc = imp_tipodoc;
	this.imp_impresora = imp_impresora;
	this.imp_confirmacion = imp_confirmacion;
	this.imp_estado = imp_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Impresion(IDataReader reader)
        {
    	this.imp_empresa = (Int32)reader["imp_empresa"];
	this.imp_almacen = (Int32)reader["imp_almacen"];
	this.imp_pventa = (Int32)reader["imp_pventa"];
	this.imp_tipodoc = (Int32)reader["imp_tipodoc"];
	this.imp_impresora = reader["imp_impresora"].ToString();
	this.imp_confirmacion = (reader["imp_confirmacion"] != DBNull.Value) ? (Int32?)reader["imp_confirmacion"] : null;
	this.imp_estado = (reader["imp_estado"] != DBNull.Value) ? (Int32?)reader["imp_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Impresion(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object imp_empresa = null;
	object imp_almacen = null;
	object imp_pventa = null;
	object imp_tipodoc = null;
	object imp_impresora = null;
	object imp_confirmacion = null;
	object imp_estado = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("imp_empresa", out imp_empresa);
	tmp.TryGetValue("imp_almacen", out imp_almacen);
	tmp.TryGetValue("imp_pventa", out imp_pventa);
	tmp.TryGetValue("imp_tipodoc", out imp_tipodoc);
	tmp.TryGetValue("imp_impresora", out imp_impresora);
	tmp.TryGetValue("imp_confirmacion", out imp_confirmacion);
	tmp.TryGetValue("imp_estado", out imp_estado);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.imp_empresa = (Int32)Conversiones.GetValueByType(imp_empresa, typeof(Int32));
	this.imp_almacen = (Int32)Conversiones.GetValueByType(imp_almacen, typeof(Int32));
	this.imp_pventa = (Int32)Conversiones.GetValueByType(imp_pventa, typeof(Int32));
	this.imp_tipodoc = (Int32)Conversiones.GetValueByType(imp_tipodoc, typeof(Int32));
	this.imp_impresora = (String)Conversiones.GetValueByType(imp_impresora, typeof(String));
	this.imp_confirmacion = (Int32?)Conversiones.GetValueByType(imp_confirmacion, typeof(Int32?));
	this.imp_estado = (Int32?)Conversiones.GetValueByType(imp_estado, typeof(Int32?));
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
