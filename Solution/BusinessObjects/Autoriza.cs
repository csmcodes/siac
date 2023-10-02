using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Autoriza
    {
        #region Properties

    	[Data(key = true)]
	public Int32 aut_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 aut_empresa_key { get; set; }
	[Data(key = true)]
	public Int64 aut_cco_comproba { get; set; }
	[Data(originalkey = true)]
	public Int64 aut_cco_comproba_key { get; set; }
	[Data(key = true)]
	public Int32 aut_secuencia { get; set; }
	[Data(originalkey = true)]
	public Int32 aut_secuencia_key { get; set; }
	public Int32 aut_tipo { get; set; }
	public String aut_usuario { get; set; }
	public DateTime aut_fecha { get; set; }
	public String aut_usu_autoriza { get; set; }
	public String aut_usu_modifica { get; set; }
	public DateTime? aut_usu_fecha { get; set; }
	public String aut_concepto { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }
	public Int32? aut_estado { get; set; }
    public string crea_ip { get; set; }
    public string mod_ip  { get; set; }

              
        #endregion

        #region Constructors


        public  Autoriza()
        {
        }

        public  Autoriza( Int32 aut_empresa,Int64 aut_cco_comproba,Int32 aut_secuencia,Int32 aut_tipo,String aut_usuario,DateTime aut_fecha,String aut_usu_autoriza,String aut_usu_modifica,DateTime aut_usu_fecha,String aut_concepto,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha,Int32 aut_estado)
        {                
    	this.aut_empresa = aut_empresa;
	this.aut_cco_comproba = aut_cco_comproba;
	this.aut_secuencia = aut_secuencia;
    this.aut_empresa_key = aut_empresa;
    this.aut_cco_comproba_key = aut_cco_comproba;
    this.aut_secuencia_key = aut_secuencia;
	this.aut_tipo = aut_tipo;
	this.aut_usuario = aut_usuario;
	this.aut_fecha = aut_fecha;
	this.aut_usu_autoriza = aut_usu_autoriza;
	this.aut_usu_modifica = aut_usu_modifica;
	this.aut_usu_fecha = aut_usu_fecha;
	this.aut_concepto = aut_concepto;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;
	this.aut_estado = aut_estado;

           
       }

        public  Autoriza(IDataReader reader)
        {
    	this.aut_empresa = (Int32)reader["aut_empresa"];
	this.aut_cco_comproba = (Int64)reader["aut_cco_comproba"];
	this.aut_secuencia = (Int32)reader["aut_secuencia"];
    this.aut_empresa_key = (Int32)reader["aut_empresa"];
    this.aut_cco_comproba_key = (Int64)reader["aut_cco_comproba"];
    this.aut_secuencia_key = (Int32)reader["aut_secuencia"];
	this.aut_tipo = (Int32)reader["aut_tipo"];
	this.aut_usuario = reader["aut_usuario"].ToString();
	this.aut_fecha = (DateTime)reader["aut_fecha"];
	this.aut_usu_autoriza = reader["aut_usu_autoriza"].ToString();
	this.aut_usu_modifica = reader["aut_usu_modifica"].ToString();
	this.aut_usu_fecha = (reader["aut_usu_fecha"] != DBNull.Value) ? (DateTime?)reader["aut_usu_fecha"] : null;
	this.aut_concepto = reader["aut_concepto"].ToString();
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
	this.aut_estado = (reader["aut_estado"] != DBNull.Value) ? (Int32?)reader["aut_estado"] : null;

        }


        public Autoriza(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object aut_empresa = null;
	object aut_cco_comproba = null;
	object aut_secuencia = null;

    object aut_empresa_key = null;
    object aut_cco_comproba_key = null;
    object aut_secuencia_key = null;

	object aut_tipo = null;
	object aut_usuario = null;
	object aut_fecha = null;
	object aut_usu_autoriza = null;
	object aut_usu_modifica = null;
	object aut_usu_fecha = null;
	object aut_concepto = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;
	object aut_estado = null;


                	tmp.TryGetValue("aut_empresa", out aut_empresa);
	tmp.TryGetValue("aut_cco_comproba", out aut_cco_comproba);
	tmp.TryGetValue("aut_secuencia", out aut_secuencia);

    tmp.TryGetValue("aut_empresa_key", out aut_empresa_key);
    tmp.TryGetValue("aut_cco_comproba_key", out aut_cco_comproba_key);
    tmp.TryGetValue("aut_secuencia_key", out aut_secuencia_key);

	tmp.TryGetValue("aut_tipo", out aut_tipo);
	tmp.TryGetValue("aut_usuario", out aut_usuario);
	tmp.TryGetValue("aut_fecha", out aut_fecha);
	tmp.TryGetValue("aut_usu_autoriza", out aut_usu_autoriza);
	tmp.TryGetValue("aut_usu_modifica", out aut_usu_modifica);
	tmp.TryGetValue("aut_usu_fecha", out aut_usu_fecha);
	tmp.TryGetValue("aut_concepto", out aut_concepto);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);
	tmp.TryGetValue("aut_estado", out aut_estado);


                	this.aut_empresa = (Int32)Conversiones.GetValueByType(aut_empresa, typeof(Int32));
	this.aut_cco_comproba = (Int64)Conversiones.GetValueByType(aut_cco_comproba, typeof(Int64));
	this.aut_secuencia = (Int32)Conversiones.GetValueByType(aut_secuencia, typeof(Int32));

    this.aut_empresa_key = (Int32)Conversiones.GetValueByType(aut_empresa_key, typeof(Int32));
    this.aut_cco_comproba_key = (Int64)Conversiones.GetValueByType(aut_cco_comproba_key, typeof(Int64));
    this.aut_secuencia_key = (Int32)Conversiones.GetValueByType(aut_secuencia_key, typeof(Int32));

	this.aut_tipo = (Int32)Conversiones.GetValueByType(aut_tipo, typeof(Int32));
	this.aut_usuario = (String)Conversiones.GetValueByType(aut_usuario, typeof(String));
	this.aut_fecha = (DateTime)Conversiones.GetValueByType(aut_fecha, typeof(DateTime));
	this.aut_usu_autoriza = (String)Conversiones.GetValueByType(aut_usu_autoriza, typeof(String));
	this.aut_usu_modifica = (String)Conversiones.GetValueByType(aut_usu_modifica, typeof(String));
	this.aut_usu_fecha = (DateTime?)Conversiones.GetValueByType(aut_usu_fecha, typeof(DateTime?));
	this.aut_concepto = (String)Conversiones.GetValueByType(aut_concepto, typeof(String));
	this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
	this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
	this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
	this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
	this.aut_estado = (Int32?)Conversiones.GetValueByType(aut_estado, typeof(Int32?));

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