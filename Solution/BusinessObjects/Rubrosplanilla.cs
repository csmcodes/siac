using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Rubrosplanilla
    {
        #region Properties

    	[Data(key = true)]
	public Int32 rpl_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 rpl_empresa_key { get; set; }
	[Data(key = true)]
	public Int64 rpl_comprobante { get; set; }
	[Data(originalkey = true)]
	public Int64 rpl_comprobante_key { get; set; }
	[Data(key = true)]
	public Int32 rpl_rubro { get; set; }
	[Data(originalkey = true)]
	public Int32 rpl_rubro_key { get; set; }
	public String rpl_observacion { get; set; }
	public Decimal? rpl_valor { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

    [Data(nosql = true, tablaref = "rubro", camporef = "rub_tipo", foreign = "rpl_empresa, rpl_rubro", keyref = "rub_empresa, rub_codigo", join = "inner")]
    public string rub_tipo { get; set; }
    [Data(nosql = true, tablaref = "rubro", camporef = "rub_nombre", foreign = "rpl_empresa, rpl_rubro", keyref = "rub_empresa, rub_codigo", join = "inner")]
    public string rub_nombre { get; set; }

              
        #endregion

        #region Constructors


        public  Rubrosplanilla()
        {
        }

        public  Rubrosplanilla( Int32 rpl_empresa,Int64 rpl_comprobante,Int32 rpl_rubro,String rpl_observacion,Decimal rpl_valor,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.rpl_empresa = rpl_empresa;
	this.rpl_comprobante = rpl_comprobante;
	this.rpl_rubro = rpl_rubro;
	this.rpl_observacion = rpl_observacion;
	this.rpl_valor = rpl_valor;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Rubrosplanilla(IDataReader reader)
        {
    	this.rpl_empresa = (Int32)reader["rpl_empresa"];
	this.rpl_comprobante = (Int64)reader["rpl_comprobante"];
	this.rpl_rubro = (Int32)reader["rpl_rubro"];
	this.rpl_observacion = reader["rpl_observacion"].ToString();
	this.rpl_valor = (reader["rpl_valor"] != DBNull.Value) ? (Decimal?)reader["rpl_valor"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
    this.rub_tipo= reader["rub_tipo"].ToString();
    this.rub_nombre = reader["rub_nombre"].ToString();
        }


        public Rubrosplanilla(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object rpl_empresa = null;
	object rpl_comprobante = null;
	object rpl_rubro = null;
	object rpl_observacion = null;
	object rpl_valor = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("rpl_empresa", out rpl_empresa);
	tmp.TryGetValue("rpl_comprobante", out rpl_comprobante);
	tmp.TryGetValue("rpl_rubro", out rpl_rubro);
	tmp.TryGetValue("rpl_observacion", out rpl_observacion);
	tmp.TryGetValue("rpl_valor", out rpl_valor);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.rpl_empresa = (Int32)Conversiones.GetValueByType(rpl_empresa, typeof(Int32));
	this.rpl_comprobante = (Int64)Conversiones.GetValueByType(rpl_comprobante, typeof(Int64));
	this.rpl_rubro = (Int32)Conversiones.GetValueByType(rpl_rubro, typeof(Int32));
	this.rpl_observacion = (String)Conversiones.GetValueByType(rpl_observacion, typeof(String));
	this.rpl_valor = (Decimal?)Conversiones.GetValueByType(rpl_valor, typeof(Decimal?));
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

        public List<Rubrosplanilla> GetStruc()
        {
            return new List<Rubrosplanilla>();
        }
        #endregion


    }
}
