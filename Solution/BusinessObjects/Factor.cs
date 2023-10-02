using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Factor
    {
        #region Properties

    	[Data(key = true)]
	public Int32 fac_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 fac_empresa_key { get; set; }
	[Data(key = true)]
	public Int32 fac_producto { get; set; }
	[Data(originalkey = true)]
	public Int32 fac_producto_key { get; set; }
	[Data(key = true)]
	public Int32 fac_unidad { get; set; }
	[Data(originalkey = true)]
	public Int32 fac_unidad_key { get; set; }
	public Decimal? fac_factor { get; set; }
	public Int32? fac_default { get; set; }
	public Int32? fac_estado { get; set; }
    [Data(noupdate = true)]
	public String crea_usr { get; set; }
    [Data(noupdate = true)]
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

    [Data(nosql = true, tablaref = "umedida", camporef = "umd_nombre", foreign = "fac_unidad", keyref = "umd_codigo")]
    public string fac_nombreunidad { get; set; }
        #endregion

        #region Constructors


        public  Factor()
        {
        }

        public  Factor( Int32 fac_empresa,Int32 fac_producto,Int32 fac_unidad,Decimal fac_factor,Int32 fac_default,Int32 fac_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.fac_empresa = fac_empresa;
	this.fac_producto = fac_producto;
    this.fac_unidad = fac_unidad;
    this.fac_empresa_key = fac_empresa;
    this.fac_producto_key = fac_producto;
	this.fac_unidad_key = fac_unidad;
	this.fac_factor = fac_factor;
	this.fac_default = fac_default;
	this.fac_estado = fac_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Factor(IDataReader reader)
        {
    	this.fac_empresa = (Int32)reader["fac_empresa"];
	this.fac_producto = (Int32)reader["fac_producto"];
    this.fac_unidad = (Int32)reader["fac_unidad"];
    this.fac_empresa_key = (Int32)reader["fac_empresa"];
    this.fac_producto_key = (Int32)reader["fac_producto"];
    this.fac_unidad_key = (Int32)reader["fac_unidad"];
	this.fac_factor = (reader["fac_factor"] != DBNull.Value) ? (Decimal?)reader["fac_factor"] : null;
	this.fac_default = (reader["fac_default"] != DBNull.Value) ? (Int32?)reader["fac_default"] : null;
	this.fac_estado = (reader["fac_estado"] != DBNull.Value) ? (Int32?)reader["fac_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.fac_nombreunidad = reader["fac_nombreunidad"].ToString();
            
        }



        public Factor(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object fac_empresa = null;
                object fac_producto = null;
                object fac_unidad = null;
                object fac_factor = null;
                object fac_default = null;
                object fac_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("fac_empresa", out fac_empresa);
                tmp.TryGetValue("fac_producto", out fac_producto);
                tmp.TryGetValue("fac_unidad", out fac_unidad);
                tmp.TryGetValue("fac_factor", out fac_factor);
                tmp.TryGetValue("fac_default", out fac_default);
                tmp.TryGetValue("fac_estado", out fac_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.fac_empresa = (Int32)Conversiones.GetValueByType(fac_empresa, typeof(Int32));
                this.fac_producto = (Int32)Conversiones.GetValueByType(fac_producto, typeof(Int32));
                this.fac_unidad = (Int32)Conversiones.GetValueByType(fac_unidad, typeof(Int32));
                this.fac_factor = (Decimal?)Conversiones.GetValueByType(fac_factor, typeof(Decimal?));
                this.fac_default = (Int32?)Conversiones.GetValueByType(fac_default, typeof(Int32?));
                this.fac_estado = (Int32?)Conversiones.GetValueByType(fac_estado, typeof(Int32?));
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
