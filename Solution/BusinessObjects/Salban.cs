using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Salban
    {
        #region Properties

    	[Data(key = true)]
	public Int32 slb_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 slb_empresa_key { get; set; }
	[Data(key = true)]
	public Int32 slb_banco { get; set; }
	[Data(originalkey = true)]
	public Int32 slb_banco_key { get; set; }
	[Data(key = true)]
	public Int32 slb_almacen { get; set; }
	[Data(originalkey = true)]
	public Int32 slb_almacen_key { get; set; }
	[Data(key = true)]
	public Int32 slb_transacc { get; set; }
	[Data(originalkey = true)]
	public Int32 slb_transacc_key { get; set; }
	[Data(key = true)]
	public Int32 slb_periodo { get; set; }
	[Data(originalkey = true)]
	public Int32 slb_periodo_key { get; set; }
	[Data(key = true)]
	public Int32 slb_mes { get; set; }
	[Data(originalkey = true)]
	public Int32 slb_mes_key { get; set; }
	public Decimal? slb_debito { get; set; }
	public Decimal? slb_credito { get; set; }
	public Decimal? slb_debext { get; set; }
	public Decimal? slb_creext { get; set; }

              
        #endregion

        #region Constructors


        public  Salban()
        {
        }

        public  Salban( Int32 slb_empresa,Int32 slb_banco,Int32 slb_almacen,Int32 slb_transacc,Int32 slb_periodo,Int32 slb_mes,Decimal slb_debito,Decimal slb_credito,Decimal slb_debext,Decimal slb_creext)
        {                
    	this.slb_empresa = slb_empresa;
	this.slb_banco = slb_banco;
	this.slb_almacen = slb_almacen;
	this.slb_transacc = slb_transacc;
	this.slb_periodo = slb_periodo;
	this.slb_mes = slb_mes;

    this.slb_empresa_key = slb_empresa;
    this.slb_banco_key = slb_banco;
    this.slb_almacen_key = slb_almacen;
    this.slb_transacc_key = slb_transacc;
    this.slb_periodo_key = slb_periodo;
    this.slb_mes_key = slb_mes;


	this.slb_debito = slb_debito;
	this.slb_credito = slb_credito;
	this.slb_debext = slb_debext;
	this.slb_creext = slb_creext;

           
       }

        public  Salban(IDataReader reader)
        {
    	this.slb_empresa = (Int32)reader["slb_empresa"];
	this.slb_banco = (Int32)reader["slb_banco"];
	this.slb_almacen = (Int32)reader["slb_almacen"];
	this.slb_transacc = (Int32)reader["slb_transacc"];
	this.slb_periodo = (Int32)reader["slb_periodo"];
	this.slb_mes = (Int32)reader["slb_mes"];
    this.slb_empresa_key = (Int32)reader["slb_empresa"];
    this.slb_banco_key = (Int32)reader["slb_banco"];
    this.slb_almacen_key = (Int32)reader["slb_almacen"];
    this.slb_transacc_key = (Int32)reader["slb_transacc"];
    this.slb_periodo_key = (Int32)reader["slb_periodo"];
    this.slb_mes_key = (Int32)reader["slb_mes"];
	this.slb_debito = (reader["slb_debito"] != DBNull.Value) ? (Decimal?)reader["slb_debito"] : null;
	this.slb_credito = (reader["slb_credito"] != DBNull.Value) ? (Decimal?)reader["slb_credito"] : null;
	this.slb_debext = (reader["slb_debext"] != DBNull.Value) ? (Decimal?)reader["slb_debext"] : null;
	this.slb_creext = (reader["slb_creext"] != DBNull.Value) ? (Decimal?)reader["slb_creext"] : null;

        }


        public Salban(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object slb_empresa = null;
	object slb_banco = null;
	object slb_almacen = null;
	object slb_transacc = null;
	object slb_periodo = null;
	object slb_mes = null;

    object slb_empresa_key = null;
    object slb_banco_key = null;
    object slb_almacen_key = null;
    object slb_transacc_key = null;
    object slb_periodo_key = null;
    object slb_mes_key = null;

	object slb_debito = null;
	object slb_credito = null;
	object slb_debext = null;
	object slb_creext = null;


                	tmp.TryGetValue("slb_empresa", out slb_empresa);
	tmp.TryGetValue("slb_banco", out slb_banco);
	tmp.TryGetValue("slb_almacen", out slb_almacen);
	tmp.TryGetValue("slb_transacc", out slb_transacc);
	tmp.TryGetValue("slb_periodo", out slb_periodo);
	tmp.TryGetValue("slb_mes", out slb_mes);

    tmp.TryGetValue("slb_empresa_key", out slb_empresa_key);
    tmp.TryGetValue("slb_banco_key", out slb_banco_key);
    tmp.TryGetValue("slb_almacen_key", out slb_almacen_key);
    tmp.TryGetValue("slb_transacc_key", out slb_transacc_key);
    tmp.TryGetValue("slb_periodo_key", out slb_periodo_key);
    tmp.TryGetValue("slb_mes_key", out slb_mes_key);

	tmp.TryGetValue("slb_debito", out slb_debito);
	tmp.TryGetValue("slb_credito", out slb_credito);
	tmp.TryGetValue("slb_debext", out slb_debext);
	tmp.TryGetValue("slb_creext", out slb_creext);


                	this.slb_empresa = (Int32)Conversiones.GetValueByType(slb_empresa, typeof(Int32));
	this.slb_banco = (Int32)Conversiones.GetValueByType(slb_banco, typeof(Int32));
	this.slb_almacen = (Int32)Conversiones.GetValueByType(slb_almacen, typeof(Int32));
	this.slb_transacc = (Int32)Conversiones.GetValueByType(slb_transacc, typeof(Int32));
	this.slb_periodo = (Int32)Conversiones.GetValueByType(slb_periodo, typeof(Int32));
	this.slb_mes = (Int32)Conversiones.GetValueByType(slb_mes, typeof(Int32));

    this.slb_empresa_key = (Int32)Conversiones.GetValueByType(slb_empresa_key, typeof(Int32));
    this.slb_banco_key = (Int32)Conversiones.GetValueByType(slb_banco_key, typeof(Int32));
    this.slb_almacen_key = (Int32)Conversiones.GetValueByType(slb_almacen_key, typeof(Int32));
    this.slb_transacc_key = (Int32)Conversiones.GetValueByType(slb_transacc_key, typeof(Int32));
    this.slb_periodo_key = (Int32)Conversiones.GetValueByType(slb_periodo_key, typeof(Int32));
    this.slb_mes_key = (Int32)Conversiones.GetValueByType(slb_mes_key, typeof(Int32));

	this.slb_debito = (Decimal?)Conversiones.GetValueByType(slb_debito, typeof(Decimal?));
	this.slb_credito = (Decimal?)Conversiones.GetValueByType(slb_credito, typeof(Decimal?));
	this.slb_debext = (Decimal?)Conversiones.GetValueByType(slb_debext, typeof(Decimal?));
	this.slb_creext = (Decimal?)Conversiones.GetValueByType(slb_creext, typeof(Decimal?));

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
