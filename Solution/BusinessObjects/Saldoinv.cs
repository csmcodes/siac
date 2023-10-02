using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Saldoinv
    {
        #region Properties

    	[Data(key = true)]
	public Int32 sai_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 sai_empresa_key { get; set; }
	[Data(key = true)]
	public Int32 sai_producto { get; set; }
	[Data(originalkey = true)]
	public Int32 sai_producto_key { get; set; }
	[Data(key = true)]
	public Int32 sai_bodega { get; set; }
	[Data(originalkey = true)]
	public Int32 sai_bodega_key { get; set; }
	[Data(key = true)]
	public Int32 sai_periodo { get; set; }
	[Data(originalkey = true)]
	public Int32 sai_periodo_key { get; set; }
	[Data(key = true)]
	public Int32 sai_mes { get; set; }
	[Data(originalkey = true)]
	public Int32 sai_mes_key { get; set; }
	public Decimal? sai_debito { get; set; }
	public Decimal? sai_credito { get; set; }
	public Decimal? sai_deb_valor { get; set; }
	public Decimal? sai_cre_valor { get; set; }
	public Decimal? sai_fisico { get; set; }
	public Decimal? sai_transito { get; set; }
	public Decimal? sai_consigna { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }
	public String crea_ip { get; set; }
	public String mod_ip { get; set; }

              
        #endregion

        #region Constructors


        public  Saldoinv()
        {
        }

        public  Saldoinv( Int32 sai_empresa,Int32 sai_producto,Int32 sai_bodega,Int32 sai_periodo,Int32 sai_mes,Decimal sai_debito,Decimal sai_credito,Decimal sai_deb_valor,Decimal sai_cre_valor,Decimal sai_fisico,Decimal sai_transito,Decimal sai_consigna,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha,String crea_ip,String mod_ip)
        {                
    	this.sai_empresa = sai_empresa;
	this.sai_producto = sai_producto;
	this.sai_bodega = sai_bodega;
	this.sai_periodo = sai_periodo;
	this.sai_mes = sai_mes;
	this.sai_debito = sai_debito;
	this.sai_credito = sai_credito;
	this.sai_deb_valor = sai_deb_valor;
	this.sai_cre_valor = sai_cre_valor;
	this.sai_fisico = sai_fisico;
	this.sai_transito = sai_transito;
	this.sai_consigna = sai_consigna;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;
	this.crea_ip = crea_ip;
	this.mod_ip = mod_ip;

           
       }

        public  Saldoinv(IDataReader reader)
        {
    	this.sai_empresa = (Int32)reader["sai_empresa"];
	this.sai_producto = (Int32)reader["sai_producto"];
	this.sai_bodega = (Int32)reader["sai_bodega"];
	this.sai_periodo = (Int32)reader["sai_periodo"];
	this.sai_mes = (Int32)reader["sai_mes"];
	this.sai_debito = (reader["sai_debito"] != DBNull.Value) ? (Decimal?)reader["sai_debito"] : null;
	this.sai_credito = (reader["sai_credito"] != DBNull.Value) ? (Decimal?)reader["sai_credito"] : null;
	this.sai_deb_valor = (reader["sai_deb_valor"] != DBNull.Value) ? (Decimal?)reader["sai_deb_valor"] : null;
	this.sai_cre_valor = (reader["sai_cre_valor"] != DBNull.Value) ? (Decimal?)reader["sai_cre_valor"] : null;
	this.sai_fisico = (reader["sai_fisico"] != DBNull.Value) ? (Decimal?)reader["sai_fisico"] : null;
	this.sai_transito = (reader["sai_transito"] != DBNull.Value) ? (Decimal?)reader["sai_transito"] : null;
	this.sai_consigna = (reader["sai_consigna"] != DBNull.Value) ? (Decimal?)reader["sai_consigna"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
	this.crea_ip = reader["crea_ip"].ToString();
	this.mod_ip = reader["mod_ip"].ToString();

        }


        public Saldoinv(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object sai_empresa = null;
	object sai_producto = null;
	object sai_bodega = null;
	object sai_periodo = null;
	object sai_mes = null;
	object sai_debito = null;
	object sai_credito = null;
	object sai_deb_valor = null;
	object sai_cre_valor = null;
	object sai_fisico = null;
	object sai_transito = null;
	object sai_consigna = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;
	object crea_ip = null;
	object mod_ip = null;


                	tmp.TryGetValue("sai_empresa", out sai_empresa);
	tmp.TryGetValue("sai_producto", out sai_producto);
	tmp.TryGetValue("sai_bodega", out sai_bodega);
	tmp.TryGetValue("sai_periodo", out sai_periodo);
	tmp.TryGetValue("sai_mes", out sai_mes);
	tmp.TryGetValue("sai_debito", out sai_debito);
	tmp.TryGetValue("sai_credito", out sai_credito);
	tmp.TryGetValue("sai_deb_valor", out sai_deb_valor);
	tmp.TryGetValue("sai_cre_valor", out sai_cre_valor);
	tmp.TryGetValue("sai_fisico", out sai_fisico);
	tmp.TryGetValue("sai_transito", out sai_transito);
	tmp.TryGetValue("sai_consigna", out sai_consigna);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);
	tmp.TryGetValue("crea_ip", out crea_ip);
	tmp.TryGetValue("mod_ip", out mod_ip);


                	this.sai_empresa = (Int32)Conversiones.GetValueByType(sai_empresa, typeof(Int32));
	this.sai_producto = (Int32)Conversiones.GetValueByType(sai_producto, typeof(Int32));
	this.sai_bodega = (Int32)Conversiones.GetValueByType(sai_bodega, typeof(Int32));
	this.sai_periodo = (Int32)Conversiones.GetValueByType(sai_periodo, typeof(Int32));
	this.sai_mes = (Int32)Conversiones.GetValueByType(sai_mes, typeof(Int32));
	this.sai_debito = (Decimal?)Conversiones.GetValueByType(sai_debito, typeof(Decimal?));
	this.sai_credito = (Decimal?)Conversiones.GetValueByType(sai_credito, typeof(Decimal?));
	this.sai_deb_valor = (Decimal?)Conversiones.GetValueByType(sai_deb_valor, typeof(Decimal?));
	this.sai_cre_valor = (Decimal?)Conversiones.GetValueByType(sai_cre_valor, typeof(Decimal?));
	this.sai_fisico = (Decimal?)Conversiones.GetValueByType(sai_fisico, typeof(Decimal?));
	this.sai_transito = (Decimal?)Conversiones.GetValueByType(sai_transito, typeof(Decimal?));
	this.sai_consigna = (Decimal?)Conversiones.GetValueByType(sai_consigna, typeof(Decimal?));
	this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
	this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
	this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
	this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
	this.crea_ip = (String)Conversiones.GetValueByType(crea_ip, typeof(String));
	this.mod_ip = (String)Conversiones.GetValueByType(mod_ip, typeof(String));

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
