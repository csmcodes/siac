using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Gproducto
    {
        #region Properties

    	[Data(key = true)]
	public Int32 gpr_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 gpr_empresa_key { get; set; }
	[Data(key = true, auto=true) ]
	public Int32 gpr_codigo { get; set; }
	[Data(originalkey = true)]
	public Int32 gpr_codigo_key { get; set; }
	public String gpr_id { get; set; }
	public String gpr_nombre { get; set; }
	public Int32? gpr_cta_costo { get; set; }
	public Int32? gpr_cta_inv { get; set; }
	public Int32? gpr_cta_venta { get; set; }
	public Int32? gpr_cta_des { get; set; }
	public Int32? gpr_cta_dev { get; set; }
	public Int32? gpr_estado { get; set; }
        [Data(noupdate = true)]
	public String crea_usr { get; set; }
        [Data(noupdate = true)]
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }


    [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "gpr_empresa, gpr_cta_costo", keyref = "cue_empresa, cue_codigo", join = "left")]
    public string gpr_nombrecta_costo { get; set; }
    [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "gpr_empresa, gpr_cta_inv", keyref = "cue_empresa, cue_codigo", join = "left")]
    public string gpr_nombrecta_inv { get; set; }

    [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "gpr_empresa, gpr_cta_des", keyref = "cue_empresa, cue_codigo", join = "left")]
    public string gpr_nombrecta_des { get; set; }
    [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "gpr_empresa, gpr_cta_dev", keyref = "cue_empresa, cue_codigo", join = "left")]
    public string gpr_nombrecta_dev { get; set; }
    [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "gpr_empresa, gpr_cta_venta", keyref = "cue_empresa, cue_codigo", join = "left")]
    public string gpr_nombrecta_venta { get; set; }
    
              
        #endregion

        #region Constructors


        public  Gproducto()
        {
        }

        public  Gproducto( Int32 gpr_empresa,Int32 gpr_codigo,String gpr_id,String gpr_nombre,Int32 gpr_cta_costo,Int32 gpr_cta_inv,Int32 gpr_cta_venta,Int32 gpr_cta_des,Int32 gpr_cta_dev,Int32 gpr_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.gpr_empresa = gpr_empresa;
	this.gpr_codigo = gpr_codigo;
    this.gpr_empresa_key = gpr_empresa;
    this.gpr_codigo_key = gpr_codigo;
	this.gpr_id = gpr_id;
	this.gpr_nombre = gpr_nombre;
	this.gpr_cta_costo = gpr_cta_costo;
	this.gpr_cta_inv = gpr_cta_inv;
	this.gpr_cta_venta = gpr_cta_venta;
	this.gpr_cta_des = gpr_cta_des;
	this.gpr_cta_dev = gpr_cta_dev;
	this.gpr_estado = gpr_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Gproducto(IDataReader reader)
        {
    	this.gpr_empresa = (Int32)reader["gpr_empresa"];
	this.gpr_codigo = (Int32)reader["gpr_codigo"];
    this.gpr_empresa_key = (Int32)reader["gpr_empresa"];
    this.gpr_codigo_key = (Int32)reader["gpr_codigo"];
	this.gpr_id = reader["gpr_id"].ToString();
	this.gpr_nombre = reader["gpr_nombre"].ToString();
	this.gpr_cta_costo = (reader["gpr_cta_costo"] != DBNull.Value) ? (Int32?)reader["gpr_cta_costo"] : null;
	this.gpr_cta_inv = (reader["gpr_cta_inv"] != DBNull.Value) ? (Int32?)reader["gpr_cta_inv"] : null;
	this.gpr_cta_venta = (reader["gpr_cta_venta"] != DBNull.Value) ? (Int32?)reader["gpr_cta_venta"] : null;
	this.gpr_cta_des = (reader["gpr_cta_des"] != DBNull.Value) ? (Int32?)reader["gpr_cta_des"] : null;
	this.gpr_cta_dev = (reader["gpr_cta_dev"] != DBNull.Value) ? (Int32?)reader["gpr_cta_dev"] : null;
	this.gpr_estado = (reader["gpr_estado"] != DBNull.Value) ? (Int32?)reader["gpr_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
    this.gpr_nombrecta_costo = reader["gpr_nombrecta_costo"].ToString();
    this.gpr_nombrecta_inv = reader["gpr_nombrecta_inv"].ToString();
    this.gpr_nombrecta_des = reader["gpr_nombrecta_des"].ToString();
    this.gpr_nombrecta_dev = reader["gpr_nombrecta_dev"].ToString();
    this.gpr_nombrecta_venta = reader["gpr_nombrecta_venta"].ToString();
    
        }


        public Gproducto(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object gpr_empresa = null;
	object gpr_codigo = null;
    object gpr_empresa_key = null;
    object gpr_codigo_key = null;
	object gpr_id = null;
	object gpr_nombre = null;
	object gpr_cta_costo = null;
	object gpr_cta_inv = null;
	object gpr_cta_venta = null;
	object gpr_cta_des = null;
	object gpr_cta_dev = null;
	object gpr_estado = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("gpr_empresa", out gpr_empresa);
	tmp.TryGetValue("gpr_codigo", out gpr_codigo);
    tmp.TryGetValue("gpr_empresa_key", out gpr_empresa);
    tmp.TryGetValue("gpr_codigo_key", out gpr_codigo);
	tmp.TryGetValue("gpr_id", out gpr_id);
	tmp.TryGetValue("gpr_nombre", out gpr_nombre);
	tmp.TryGetValue("gpr_cta_costo", out gpr_cta_costo);
	tmp.TryGetValue("gpr_cta_inv", out gpr_cta_inv);
	tmp.TryGetValue("gpr_cta_venta", out gpr_cta_venta);
	tmp.TryGetValue("gpr_cta_des", out gpr_cta_des);
	tmp.TryGetValue("gpr_cta_dev", out gpr_cta_dev);
	tmp.TryGetValue("gpr_estado", out gpr_estado);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.gpr_empresa = (Int32)Conversiones.GetValueByType(gpr_empresa, typeof(Int32));
	this.gpr_codigo = (Int32)Conversiones.GetValueByType(gpr_codigo, typeof(Int32));
    this.gpr_empresa_key = (Int32)Conversiones.GetValueByType(gpr_empresa_key, typeof(Int32));
    this.gpr_codigo_key = (Int32)Conversiones.GetValueByType(gpr_codigo_key, typeof(Int32));
	this.gpr_id = (String)Conversiones.GetValueByType(gpr_id, typeof(String));
	this.gpr_nombre = (String)Conversiones.GetValueByType(gpr_nombre, typeof(String));
	this.gpr_cta_costo = (Int32?)Conversiones.GetValueByType(gpr_cta_costo, typeof(Int32?));
	this.gpr_cta_inv = (Int32?)Conversiones.GetValueByType(gpr_cta_inv, typeof(Int32?));
	this.gpr_cta_venta = (Int32?)Conversiones.GetValueByType(gpr_cta_venta, typeof(Int32?));
	this.gpr_cta_des = (Int32?)Conversiones.GetValueByType(gpr_cta_des, typeof(Int32?));
	this.gpr_cta_dev = (Int32?)Conversiones.GetValueByType(gpr_cta_dev, typeof(Int32?));
	this.gpr_estado = (Int32?)Conversiones.GetValueByType(gpr_estado, typeof(Int32?));
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
