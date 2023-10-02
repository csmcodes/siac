using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Tiponc
    {
        #region Properties

    	[Data(key = true)]
	public Int32 tnc_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 tnc_empresa_key { get; set; }
	[Data(key = true, auto=true)]
	public Int32 tnc_codigo { get; set; }
	[Data(originalkey = true)]
	public Int32 tnc_codigo_key { get; set; }
	public Int32 tnc_tclipro { get; set; }
	public String tnc_id { get; set; }
	public String tnc_nombre { get; set; }
	public Int32? tnc_cuentanc { get; set; }
	public Int32? tnc_cuentand { get; set; }
	public Int32? tnc_estado { get; set; }
         [Data(noupdate = true)]
	public String crea_usr { get; set; }
         [Data(noupdate = true)]
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

    [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "tnc_empresa, tnc_cuentanc", keyref = "cue_empresa, cue_codigo", join = "left")]
    public string tnc_nombrecuentanc { get; set; }
    [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "tnc_empresa, tnc_cuentand", keyref = "cue_empresa, cue_codigo", join = "left")]
    public string tnc_nombrecuentand { get; set; }
              
        #endregion

        #region Constructors


        public  Tiponc()
        {
        }

        public  Tiponc( Int32 tnc_empresa,Int32 tnc_codigo,Int32 tnc_tclipro,String tnc_id,String tnc_nombre,Int32 tnc_cuentanc,Int32 tnc_cuentand,Int32 tnc_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.tnc_empresa = tnc_empresa;
	this.tnc_codigo = tnc_codigo;
    this.tnc_empresa_key = tnc_empresa;
    this.tnc_codigo_key = tnc_codigo;
	this.tnc_tclipro = tnc_tclipro;
	this.tnc_id = tnc_id;
	this.tnc_nombre = tnc_nombre;
	this.tnc_cuentanc = tnc_cuentanc;
	this.tnc_cuentand = tnc_cuentand;
	this.tnc_estado = tnc_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Tiponc(IDataReader reader)
        {
    	this.tnc_empresa = (Int32)reader["tnc_empresa"];
	this.tnc_codigo = (Int32)reader["tnc_codigo"];
    this.tnc_empresa_key = (Int32)reader["tnc_empresa"];
    this.tnc_codigo_key = (Int32)reader["tnc_codigo"];
	this.tnc_tclipro = (Int32)reader["tnc_tclipro"];
	this.tnc_id = reader["tnc_id"].ToString();
	this.tnc_nombre = reader["tnc_nombre"].ToString();
	this.tnc_cuentanc = (reader["tnc_cuentanc"] != DBNull.Value) ? (Int32?)reader["tnc_cuentanc"] : null;
	this.tnc_cuentand = (reader["tnc_cuentand"] != DBNull.Value) ? (Int32?)reader["tnc_cuentand"] : null;
	this.tnc_estado = (reader["tnc_estado"] != DBNull.Value) ? (Int32?)reader["tnc_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
    this.tnc_nombrecuentanc = reader["tnc_nombrecuentanc"].ToString();
    this.tnc_nombrecuentand = reader["tnc_nombrecuentand"].ToString();
        }


        public Tiponc(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object tnc_empresa = null;
	object tnc_codigo = null;
    object tnc_empresa_key = null;
    object tnc_codigo_key = null;
	object tnc_tclipro = null;
	object tnc_id = null;
	object tnc_nombre = null;
	object tnc_cuentanc = null;
	object tnc_cuentand = null;
	object tnc_estado = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("tnc_empresa", out tnc_empresa);
	tmp.TryGetValue("tnc_codigo", out tnc_codigo);
    tmp.TryGetValue("tnc_empresa_key", out tnc_empresa_key);
    tmp.TryGetValue("tnc_codigo_key", out tnc_codigo_key);

	tmp.TryGetValue("tnc_tclipro", out tnc_tclipro);
	tmp.TryGetValue("tnc_id", out tnc_id);
	tmp.TryGetValue("tnc_nombre", out tnc_nombre);
	tmp.TryGetValue("tnc_cuentanc", out tnc_cuentanc);
	tmp.TryGetValue("tnc_cuentand", out tnc_cuentand);
	tmp.TryGetValue("tnc_estado", out tnc_estado);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.tnc_empresa = (Int32)Conversiones.GetValueByType(tnc_empresa, typeof(Int32));
	this.tnc_codigo = (Int32)Conversiones.GetValueByType(tnc_codigo, typeof(Int32));
    this.tnc_empresa_key = (Int32)Conversiones.GetValueByType(tnc_empresa_key, typeof(Int32));
    this.tnc_codigo_key = (Int32)Conversiones.GetValueByType(tnc_codigo_key, typeof(Int32));
	this.tnc_tclipro = (Int32)Conversiones.GetValueByType(tnc_tclipro, typeof(Int32));
	this.tnc_id = (String)Conversiones.GetValueByType(tnc_id, typeof(String));
	this.tnc_nombre = (String)Conversiones.GetValueByType(tnc_nombre, typeof(String));
	this.tnc_cuentanc = (Int32?)Conversiones.GetValueByType(tnc_cuentanc, typeof(Int32?));
	this.tnc_cuentand = (Int32?)Conversiones.GetValueByType(tnc_cuentand, typeof(Int32?));
	this.tnc_estado = (Int32?)Conversiones.GetValueByType(tnc_estado, typeof(Int32?));
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
