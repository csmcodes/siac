using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Planillacli
    {
        #region Properties

    	[Data(key = true)]
	public Int32 plc_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 plc_empresa_key { get; set; }
	[Data(key = true)]
	public Int64 plc_comprobante_pla { get; set; }
	[Data(originalkey = true)]
	public Int64 plc_comprobante_pla_key { get; set; }
	[Data(key = true)]
	public Int64 plc_comprobante { get; set; }
	[Data(originalkey = true)]
	public Int64 plc_comprobante_key { get; set; }
	[Data(key = true)]
	
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }


    [Data(nosql = true, tablaref = "comprobante", camporef = "com_tipodoc", foreign = "plc_empresa,plc_comprobante", keyref = "com_empresa,com_codigo", join = "left")]
    public Int32? plc_comprobantetipodoc { get; set; }

              
        #endregion

        #region Constructors


        public  Planillacli()
        {
        }

        public  Planillacli( Int32 plc_empresa,Int64 plc_comprobante_pla,Int64 plc_comprobante,Int32 plc_transacc,String plc_doctran,Int32 plc_pago,Int32 plc_secuencia,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.plc_empresa = plc_empresa;
	this.plc_comprobante_pla = plc_comprobante_pla;
	this.plc_comprobante = plc_comprobante;
	
    this.plc_empresa_key = plc_empresa;
    this.plc_comprobante_pla_key = plc_comprobante_pla;
    this.plc_comprobante_key = plc_comprobante;
   
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Planillacli(IDataReader reader)
        {
    	this.plc_empresa = (Int32)reader["plc_empresa"];
	this.plc_comprobante_pla = (Int64)reader["plc_comprobante_pla"];
	this.plc_comprobante = (Int64)reader["plc_comprobante"];
	
    this.plc_empresa_key = (Int32)reader["plc_empresa"];
    this.plc_comprobante_pla_key = (Int64)reader["plc_comprobante_pla"];
    this.plc_comprobante_key = (Int64)reader["plc_comprobante"];
  





	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

    this.plc_comprobantetipodoc = (reader["plc_comprobantetipodoc"] != DBNull.Value) ? (Int32?)reader["plc_comprobantetipodoc"] : null;


        }


        public Planillacli(object objeto)
        {            
            if (objeto != null)
            {
    Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
   	object plc_empresa = null;
	object plc_comprobante_pla = null;
	object plc_comprobante = null;
	
    object plc_empresa_key = null;
    object plc_comprobante_pla_key = null;
    object plc_comprobante_key = null;
    

	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;

    tmp.TryGetValue("plc_empresa", out plc_empresa);
	tmp.TryGetValue("plc_comprobante_pla", out plc_comprobante_pla);
	tmp.TryGetValue("plc_comprobante", out plc_comprobante);
	

    tmp.TryGetValue("plc_empresa_key", out plc_empresa_key);
    tmp.TryGetValue("plc_comprobante_pla_key", out plc_comprobante_pla_key);
    tmp.TryGetValue("plc_comprobante_key", out plc_comprobante_key);
    

	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);

    this.plc_empresa = (Int32)Conversiones.GetValueByType(plc_empresa, typeof(Int32));
	this.plc_comprobante_pla = (Int64)Conversiones.GetValueByType(plc_comprobante_pla, typeof(Int64));
	this.plc_comprobante = (Int64)Conversiones.GetValueByType(plc_comprobante, typeof(Int64));

    this.plc_empresa_key = (Int32)Conversiones.GetValueByType(plc_empresa_key, typeof(Int32));
    this.plc_comprobante_pla_key = (Int64)Conversiones.GetValueByType(plc_comprobante_pla_key, typeof(Int64));
    this.plc_comprobante_key = (Int64)Conversiones.GetValueByType(plc_comprobante_key, typeof(Int64));
   

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
