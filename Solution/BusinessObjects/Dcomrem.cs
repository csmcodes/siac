using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Dcomrem
    {
        #region Properties

    	[Data(key = true)]
	public Int32 drem_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 drem_empresa_key { get; set; }
	[Data(key = true)]
	public Int64 drem_comprobante { get; set; }
	[Data(originalkey = true)]
	public Int64 drem_comprobante_key { get; set; }
	[Data(key = true)]
	public Int32 drem_secuencia { get; set; }
	[Data(originalkey = true)]
	public Int32 drem_secuencia_key { get; set; }
	public Int32? drem_producto { get; set; }
	public Decimal? drem_precio { get; set; }
	public Decimal? drem_total { get; set; }
	public Decimal? drem_cantidad { get; set; }
	public Decimal? drem_peso { get; set; }
	public String drem_descripcion { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Dcomrem()
        {
        }

        public  Dcomrem( Int32 drem_empresa,Int64 drem_comprobante,Int32 drem_secuencia,Int32 drem_producto,Decimal drem_precio,Decimal drem_total,Decimal drem_cantidad,Decimal drem_peso,String drem_descripcion,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.drem_empresa = drem_empresa;
	this.drem_comprobante = drem_comprobante;
	this.drem_secuencia = drem_secuencia;
	this.drem_producto = drem_producto;
	this.drem_precio = drem_precio;
	this.drem_total = drem_total;
	this.drem_cantidad = drem_cantidad;
	this.drem_peso = drem_peso;
	this.drem_descripcion = drem_descripcion;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Dcomrem(IDataReader reader)
        {
    	this.drem_empresa = (Int32)reader["drem_empresa"];
	this.drem_comprobante = (Int64)reader["drem_comprobante"];
	this.drem_secuencia = (Int32)reader["drem_secuencia"];
	this.drem_producto = (reader["drem_producto"] != DBNull.Value) ? (Int32?)reader["drem_producto"] : null;
	this.drem_precio = (reader["drem_precio"] != DBNull.Value) ? (Decimal?)reader["drem_precio"] : null;
	this.drem_total = (reader["drem_total"] != DBNull.Value) ? (Decimal?)reader["drem_total"] : null;
	this.drem_cantidad = (reader["drem_cantidad"] != DBNull.Value) ? (Decimal?)reader["drem_cantidad"] : null;
	this.drem_peso = (reader["drem_peso"] != DBNull.Value) ? (Decimal?)reader["drem_peso"] : null;
	this.drem_descripcion = reader["drem_descripcion"].ToString();
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Dcomrem(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object drem_empresa = null;
	object drem_comprobante = null;
	object drem_secuencia = null;
	object drem_producto = null;
	object drem_precio = null;
	object drem_total = null;
	object drem_cantidad = null;
	object drem_peso = null;
	object drem_descripcion = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("drem_empresa", out drem_empresa);
	tmp.TryGetValue("drem_comprobante", out drem_comprobante);
	tmp.TryGetValue("drem_secuencia", out drem_secuencia);
	tmp.TryGetValue("drem_producto", out drem_producto);
	tmp.TryGetValue("drem_precio", out drem_precio);
	tmp.TryGetValue("drem_total", out drem_total);
	tmp.TryGetValue("drem_cantidad", out drem_cantidad);
	tmp.TryGetValue("drem_peso", out drem_peso);
	tmp.TryGetValue("drem_descripcion", out drem_descripcion);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.drem_empresa = (Int32)Conversiones.GetValueByType(drem_empresa, typeof(Int32));
	this.drem_comprobante = (Int64)Conversiones.GetValueByType(drem_comprobante, typeof(Int64));
	this.drem_secuencia = (Int32)Conversiones.GetValueByType(drem_secuencia, typeof(Int32));
	this.drem_producto = (Int32?)Conversiones.GetValueByType(drem_producto, typeof(Int32?));
	this.drem_precio = (Decimal?)Conversiones.GetValueByType(drem_precio, typeof(Decimal?));
	this.drem_total = (Decimal?)Conversiones.GetValueByType(drem_total, typeof(Decimal?));
	this.drem_cantidad = (Decimal?)Conversiones.GetValueByType(drem_cantidad, typeof(Decimal?));
	this.drem_peso = (Decimal?)Conversiones.GetValueByType(drem_peso, typeof(Decimal?));
	this.drem_descripcion = (String)Conversiones.GetValueByType(drem_descripcion, typeof(String));
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
