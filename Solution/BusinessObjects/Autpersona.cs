using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Autpersona
    {
        #region Properties

    	[Data(key = true)]
	public Int32 ape_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 ape_empresa_key { get; set; }
	[Data(key = true)]
	public String ape_nro_autoriza { get; set; }
	[Data(originalkey = true)]
	public String ape_nro_autoriza_key { get; set; }
	[Data(key = true)]
	public string ape_fac1 { get; set; }
	[Data(originalkey = true)]
	public string ape_fac1_key { get; set; }
	[Data(key = true)]
	public string ape_fac2 { get; set; }
	[Data(originalkey = true)]
	public string ape_fac2_key { get; set; }
	[Data(key = true)]
	public Int32 ape_retdato { get; set; }
	[Data(originalkey = true)]
	public Int32 ape_retdato_key { get; set; }
	public Int32? ape_tablacoa { get; set; }
	public Int32? ape_estado { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }
	public string ape_fac3 { get; set; }
	public string ape_fact1 { get; set; }
	public string ape_fact2 { get; set; }
	public string ape_fact3 { get; set; }
	public Int32? ape_persona { get; set; }
	public Int32? ape_tclipro { get; set; }
	public DateTime? ape_val_fecha { get; set; }

    //[Data(nosql = true, tablaref = "persona", camporef = "per_nombres||' '|| per_apellidos", foreign = "ape_persona", keyref = "per_codigo", join = "left")]
        [Data(nosql = true, tablaref = "persona", camporef = "per_razon", foreign = "ape_persona", keyref = "per_codigo", join = "left")]
    public string ape_nombrepersona { get; set; }


        [Data(nosql = true, tablaref = "retdato", camporef = "rtd_campo", foreign = "ape_empresa, ape_tablacoa, ape_retdato", keyref = "rtd_empresa, rtd_tablacoa, rtd_codigo", join = "left")]
        public string ape_retdatocampo { get; set; }

        /*
            [Data(nosql = true, tablaref = "puntoventa", camporef = "pve_id", foreign = "ape_empresa, ape_fac1,ape_fac2", keyref = "pve_empresa, pve_almacen, pve_secuencia", join = "inner")]
            public string ape_pventaid { get; set; }*/

        #endregion

        #region Constructors


        public Autpersona()
        {
        }

        public Autpersona(Int32 ape_empresa, String ape_nro_autoriza, string ape_fac1, string ape_fac2, Int32 ape_retdato, Int32 ape_tablacoa, Int32 ape_estado, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha, string ape_fac3, string ape_fact1, string ape_fact2, string ape_fact3, Int32 ape_persona, Int32 ape_tclipro, DateTime ape_val_fecha)
        {                
    	this.ape_empresa = ape_empresa;
	this.ape_nro_autoriza = ape_nro_autoriza;
	this.ape_fac1 = ape_fac1;
	this.ape_fac2 = ape_fac2;
	this.ape_retdato = ape_retdato;
    this.ape_empresa_key = ape_empresa;
    this.ape_nro_autoriza_key = ape_nro_autoriza;
    this.ape_fac1_key = ape_fac1;
    this.ape_fac2_key = ape_fac2;
    this.ape_retdato_key = ape_retdato;
	this.ape_tablacoa = ape_tablacoa;
	this.ape_estado = ape_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;
	this.ape_fac3 = ape_fac3;
	this.ape_fact1 = ape_fact1;
	this.ape_fact2 = ape_fact2;
	this.ape_fact3 = ape_fact3;
	this.ape_persona = ape_persona;
	this.ape_tclipro = ape_tclipro;
	this.ape_val_fecha = ape_val_fecha;

           
       }

        public  Autpersona(IDataReader reader)
        {
    	this.ape_empresa = (Int32)reader["ape_empresa"];
	this.ape_nro_autoriza = reader["ape_nro_autoriza"].ToString();
    this.ape_fac1 = reader["ape_fac1"].ToString();
    this.ape_fac2 = reader["ape_fac2"].ToString();
	this.ape_retdato = (Int32)reader["ape_retdato"];
    this.ape_empresa_key = (Int32)reader["ape_empresa"];
    this.ape_nro_autoriza_key = reader["ape_nro_autoriza"].ToString();
    this.ape_fac1_key = reader["ape_fac1"].ToString();
    this.ape_fac2_key = reader["ape_fac2"].ToString();
    this.ape_retdato_key = (Int32)reader["ape_retdato"];
	this.ape_tablacoa = (reader["ape_tablacoa"] != DBNull.Value) ? (Int32?)reader["ape_tablacoa"] : null;
	this.ape_estado = (reader["ape_estado"] != DBNull.Value) ? (Int32?)reader["ape_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
    this.ape_fac3 = (reader["ape_fac3"] != DBNull.Value) ? (string)reader["ape_fac3"] : null;
    this.ape_fact1 = (reader["ape_fact1"] != DBNull.Value) ? (string)reader["ape_fact1"] : null;
    this.ape_fact2 = (reader["ape_fact2"] != DBNull.Value) ? (string)reader["ape_fact2"] : null;
    this.ape_fact3 = (reader["ape_fact3"] != DBNull.Value) ? (string)reader["ape_fact3"] : null;
	this.ape_persona = (reader["ape_persona"] != DBNull.Value) ? (Int32?)reader["ape_persona"] : null;
	this.ape_tclipro = (reader["ape_tclipro"] != DBNull.Value) ? (Int32?)reader["ape_tclipro"] : null;
	this.ape_val_fecha = (reader["ape_val_fecha"] != DBNull.Value) ? (DateTime?)reader["ape_val_fecha"] : null;
    //this.ape_almacenid = reader["ape_almacenid"].ToString();
    //this.ape_pventaid = reader["ape_pventaid"].ToString();
    this.ape_nombrepersona = reader["ape_nombrepersona"].ToString();
            this.ape_retdatocampo= reader["ape_retdatocampo"].ToString();

        }


        public Autpersona(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object ape_empresa = null;
	object ape_nro_autoriza = null;
	object ape_fac1 = null;
	object ape_fac2 = null;
	object ape_retdato = null;
    object ape_empresa_key = null;
    object ape_nro_autoriza_key = null;
    object ape_fac1_key = null;
    object ape_fac2_key = null;
    object ape_retdato_key = null;
	object ape_tablacoa = null;
	object ape_estado = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;
	object ape_fac3 = null;
	object ape_fact1 = null;
	object ape_fact2 = null;
	object ape_fact3 = null;
	object ape_persona = null;
	object ape_tclipro = null;
	object ape_val_fecha = null;
    object ape_almacenid = null;
    object ape_pventaid = null;

                	tmp.TryGetValue("ape_empresa", out ape_empresa);
	tmp.TryGetValue("ape_nro_autoriza", out ape_nro_autoriza);
	tmp.TryGetValue("ape_fac1", out ape_fac1);
	tmp.TryGetValue("ape_fac2", out ape_fac2);
	tmp.TryGetValue("ape_retdato", out ape_retdato);
    tmp.TryGetValue("ape_empresa_key", out ape_empresa_key);
    tmp.TryGetValue("ape_nro_autoriza_key", out ape_nro_autoriza_key);
    tmp.TryGetValue("ape_fac1_key", out ape_fac1_key);
    tmp.TryGetValue("ape_fac2_key", out ape_fac2_key);
    tmp.TryGetValue("ape_retdato_key", out ape_retdato_key);
	tmp.TryGetValue("ape_tablacoa", out ape_tablacoa);
	tmp.TryGetValue("ape_estado", out ape_estado);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);
	tmp.TryGetValue("ape_fac3", out ape_fac3);
	tmp.TryGetValue("ape_fact1", out ape_fact1);
	tmp.TryGetValue("ape_fact2", out ape_fact2);
	tmp.TryGetValue("ape_fact3", out ape_fact3);
	tmp.TryGetValue("ape_persona", out ape_persona);
	tmp.TryGetValue("ape_tclipro", out ape_tclipro);
	tmp.TryGetValue("ape_val_fecha", out ape_val_fecha);
    tmp.TryGetValue("ape_almacenid", out ape_almacenid);
    tmp.TryGetValue("ape_pventaid", out ape_pventaid);


                	this.ape_empresa = (Int32)Conversiones.GetValueByType(ape_empresa, typeof(Int32));
	this.ape_nro_autoriza = (String)Conversiones.GetValueByType(ape_nro_autoriza, typeof(String));
	this.ape_fac1 = (string)Conversiones.GetValueByType(ape_fac1, typeof(string));
	this.ape_fac2 = (string)Conversiones.GetValueByType(ape_fac2, typeof(string));
	this.ape_retdato = (Int32)Conversiones.GetValueByType(ape_retdato, typeof(Int32));
    this.ape_empresa_key = (Int32)Conversiones.GetValueByType(ape_empresa_key, typeof(Int32));
    this.ape_nro_autoriza_key = (String)Conversiones.GetValueByType(ape_nro_autoriza_key, typeof(String));
    this.ape_fac1_key = (string)Conversiones.GetValueByType(ape_fac1_key, typeof(string));
    this.ape_fac2_key = (string)Conversiones.GetValueByType(ape_fac2_key, typeof(string));
    this.ape_retdato_key = (Int32)Conversiones.GetValueByType(ape_retdato_key, typeof(Int32));
	this.ape_tablacoa = (Int32?)Conversiones.GetValueByType(ape_tablacoa, typeof(Int32?));
	this.ape_estado = (Int32?)Conversiones.GetValueByType(ape_estado, typeof(Int32?));
	this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
	this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
	this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
	this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
	this.ape_fac3 = (string)Conversiones.GetValueByType(ape_fac3, typeof(string));
	this.ape_fact1 = (string)Conversiones.GetValueByType(ape_fact1, typeof(string));
	this.ape_fact2 = (string)Conversiones.GetValueByType(ape_fact2, typeof(string));
	this.ape_fact3 = (string)Conversiones.GetValueByType(ape_fact3, typeof(string));
	this.ape_persona = (Int32?)Conversiones.GetValueByType(ape_persona, typeof(Int32?));
	this.ape_tclipro = (Int32?)Conversiones.GetValueByType(ape_tclipro, typeof(Int32?));
	this.ape_val_fecha = (DateTime?)Conversiones.GetValueByType(ape_val_fecha, typeof(DateTime?));
    //this.ape_almacenid = (String)Conversiones.GetValueByType(ape_almacenid, typeof(String));
    //this.ape_pventaid = (String)Conversiones.GetValueByType(ape_pventaid, typeof(String));

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
