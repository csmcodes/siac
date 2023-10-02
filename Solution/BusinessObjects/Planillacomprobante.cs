using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Planillacomprobante
    {
        #region Properties

    	[Data(key = true)]
	public Int64 pco_comprobante_fac { get; set; }
	[Data(originalkey = true)]
	public Int64 pco_comprobante_fac_key { get; set; }
	[Data(key = true)]
	public Int64 pco_comprobante_pla { get; set; }
	[Data(originalkey = true)]
	public Int64 pco_comprobante_pla_key { get; set; }
	[Data(key = true)]
	public Int32 pco_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 pco_empresa_key { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Planillacomprobante()
        {
        }

        public  Planillacomprobante( Int64 pco_comprobante_fac,Int64 pco_comprobante_pla,Int32 pco_empresa,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.pco_comprobante_fac = pco_comprobante_fac;
	this.pco_comprobante_pla = pco_comprobante_pla;
	this.pco_empresa = pco_empresa;
    this.pco_comprobante_fac_key = pco_comprobante_fac;
    this.pco_comprobante_pla_key= pco_comprobante_pla;
    this.pco_empresa_key = pco_empresa;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Planillacomprobante(IDataReader reader)
        {
    	this.pco_comprobante_fac = (Int64)reader["pco_comprobante_fac"];
	this.pco_comprobante_pla = (Int64)reader["pco_comprobante_pla"];
	this.pco_empresa = (Int32)reader["pco_empresa"];
    this.pco_comprobante_fac_key = (Int64)reader["pco_comprobante_fac"];
    this.pco_comprobante_pla_key = (Int64)reader["pco_comprobante_pla"];
    this.pco_empresa_key = (Int32)reader["pco_empresa"];
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Planillacomprobante(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object pco_comprobante_fac = null;
	object pco_comprobante_pla = null;
	object pco_empresa = null;
    object pco_comprobante_fac_key = null;
    object pco_comprobante_pla_key = null;
    object pco_empresa_key = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("pco_comprobante_fac", out pco_comprobante_fac);
	tmp.TryGetValue("pco_comprobante_pla", out pco_comprobante_pla);
	tmp.TryGetValue("pco_empresa", out pco_empresa);
    tmp.TryGetValue("pco_comprobante_fac_key", out pco_comprobante_fac_key);
    tmp.TryGetValue("pco_comprobante_pla_key", out pco_comprobante_pla_key);
    tmp.TryGetValue("pco_empresa_key", out pco_empresa_key);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.pco_comprobante_fac = (Int64)Conversiones.GetValueByType(pco_comprobante_fac, typeof(Int64));
	this.pco_comprobante_pla = (Int64)Conversiones.GetValueByType(pco_comprobante_pla, typeof(Int64));
	this.pco_empresa = (Int32)Conversiones.GetValueByType(pco_empresa, typeof(Int32));
    this.pco_comprobante_fac_key = (Int64)Conversiones.GetValueByType(pco_comprobante_fac_key, typeof(Int64));
    this.pco_comprobante_pla_key = (Int64)Conversiones.GetValueByType(pco_comprobante_pla_key, typeof(Int64));
    this.pco_empresa_key = (Int32)Conversiones.GetValueByType(pco_empresa_key, typeof(Int32));
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
