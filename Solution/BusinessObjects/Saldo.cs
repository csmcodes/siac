using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Saldo
    {
        #region Properties

    	[Data(key = true)]
	public Int32 sal_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 sal_empresa_key { get; set; }
	[Data(key = true)]
	public Int32 sal_cuenta { get; set; }
	[Data(originalkey = true)]
	public Int32 sal_cuenta_key { get; set; }
	[Data(key = true)]
	public Int32 sal_centro { get; set; }
	[Data(originalkey = true)]
	public Int32 sal_centro_key { get; set; }
	[Data(key = true)]
	public Int32 sal_almacen { get; set; }
	[Data(originalkey = true)]
	public Int32 sal_almacen_key { get; set; }
	[Data(key = true)]
	public Int32 sal_transacc { get; set; }
	[Data(originalkey = true)]
	public Int32 sal_transacc_key { get; set; }
	[Data(key = true)]
	public Int32 sal_periodo { get; set; }
	[Data(originalkey = true)]
	public Int32 sal_periodo_key { get; set; }
	[Data(key = true)]
	public Int32 sal_mes { get; set; }
	[Data(originalkey = true)]
	public Int32 sal_mes_key { get; set; }
	public Decimal sal_debito { get; set; }
	public Decimal sal_credito { get; set; }
	public Decimal sal_debext { get; set; }
	public Decimal sal_creext { get; set; }


    [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "sal_empresa, sal_cuenta", keyref = "cue_empresa, cue_codigo", join = "left")]
    public string sal_cuenombre{ get; set; }
    [Data(nosql = true, tablaref = "cuenta", camporef = "cue_genero", foreign = "sal_empresa, sal_cuenta", keyref = "cue_empresa, cue_codigo", join = "left")]
    public Int32? sal_cuegenero{ get; set; }

        #endregion

        #region Constructors


        public  Saldo()
        {
        }

        public  Saldo( Int32 sal_empresa,Int32 sal_cuenta,Int32 sal_centro,Int32 sal_almacen,Int32 sal_transacc,Int32 sal_periodo,Int32 sal_mes,Decimal sal_debito,Decimal sal_credito,Decimal sal_debext,Decimal sal_creext)
        {                
    	this.sal_empresa = sal_empresa;
	this.sal_cuenta = sal_cuenta;
	this.sal_centro = sal_centro;
	this.sal_almacen = sal_almacen;
	this.sal_transacc = sal_transacc;
	this.sal_periodo = sal_periodo;
	this.sal_mes = sal_mes;

    this.sal_empresa_key= sal_empresa;
    this.sal_cuenta_key = sal_cuenta;
    this.sal_centro_key = sal_centro;
    this.sal_almacen_key = sal_almacen;
    this.sal_transacc_key = sal_transacc;
    this.sal_periodo_key = sal_periodo;
    this.sal_mes_key = sal_mes;

	this.sal_debito = sal_debito;
	this.sal_credito = sal_credito;
	this.sal_debext = sal_debext;
	this.sal_creext = sal_creext;

           
       }

        public  Saldo(IDataReader reader)
        {
    	this.sal_empresa = (Int32)reader["sal_empresa"];
	this.sal_cuenta = (Int32)reader["sal_cuenta"];
	this.sal_centro = (Int32)reader["sal_centro"];
	this.sal_almacen = (Int32)reader["sal_almacen"];
	this.sal_transacc = (Int32)reader["sal_transacc"];
	this.sal_periodo = (Int32)reader["sal_periodo"];
	this.sal_mes = (Int32)reader["sal_mes"];

    this.sal_empresa_key = (Int32)reader["sal_empresa"];
    this.sal_cuenta_key = (Int32)reader["sal_cuenta"];
    this.sal_centro_key = (Int32)reader["sal_centro"];
    this.sal_almacen_key = (Int32)reader["sal_almacen"];
    this.sal_transacc_key = (Int32)reader["sal_transacc"];
    this.sal_periodo_key = (Int32)reader["sal_periodo"];
    this.sal_mes_key = (Int32)reader["sal_mes"];

	this.sal_debito = (Decimal)reader["sal_debito"];
	this.sal_credito = (Decimal)reader["sal_credito"];
	this.sal_debext = (Decimal)reader["sal_debext"];
	this.sal_creext = (Decimal)reader["sal_creext"];

    this.sal_creext = (Decimal)reader["sal_creext"];
    this.sal_creext = (Decimal)reader["sal_creext"];

    this.sal_cuenombre = (reader["sal_cuenombre"] != DBNull.Value) ? (string)reader["sal_cuenombre"] : null;
    this.sal_cuegenero= (reader["sal_cuegenero"] != DBNull.Value) ? (Int32?)reader["sal_cuegenero"] : null;       

        }


        public Saldo(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object sal_empresa = null;
	object sal_cuenta = null;
	object sal_centro = null;
	object sal_almacen = null;
	object sal_transacc = null;
	object sal_periodo = null;
	object sal_mes = null;
    object sal_empresa_key = null;
    object sal_cuenta_key = null;
    object sal_centro_key = null;
    object sal_almacen_key = null;
    object sal_transacc_key = null;
    object sal_periodo_key = null;
    object sal_mes_key = null;



	object sal_debito = null;
	object sal_credito = null;
	object sal_debext = null;
	object sal_creext = null;


                	tmp.TryGetValue("sal_empresa", out sal_empresa);
	tmp.TryGetValue("sal_cuenta", out sal_cuenta);
	tmp.TryGetValue("sal_centro", out sal_centro);
	tmp.TryGetValue("sal_almacen", out sal_almacen);
	tmp.TryGetValue("sal_transacc", out sal_transacc);
	tmp.TryGetValue("sal_periodo", out sal_periodo);
	tmp.TryGetValue("sal_mes", out sal_mes);

    tmp.TryGetValue("sal_empresa_key", out sal_empresa_key);
    tmp.TryGetValue("sal_cuenta_key", out sal_cuenta_key);
    tmp.TryGetValue("sal_centro_key", out sal_centro_key);
    tmp.TryGetValue("sal_almacen_key", out sal_almacen_key);
    tmp.TryGetValue("sal_transacc_key", out sal_transacc_key);
    tmp.TryGetValue("sal_periodo_key", out sal_periodo_key);
    tmp.TryGetValue("sal_mes_key", out sal_mes_key);




	tmp.TryGetValue("sal_debito", out sal_debito);
	tmp.TryGetValue("sal_credito", out sal_credito);
	tmp.TryGetValue("sal_debext", out sal_debext);
	tmp.TryGetValue("sal_creext", out sal_creext);


                	this.sal_empresa = (Int32)Conversiones.GetValueByType(sal_empresa, typeof(Int32));
	this.sal_cuenta = (Int32)Conversiones.GetValueByType(sal_cuenta, typeof(Int32));
	this.sal_centro = (Int32)Conversiones.GetValueByType(sal_centro, typeof(Int32));
	this.sal_almacen = (Int32)Conversiones.GetValueByType(sal_almacen, typeof(Int32));
	this.sal_transacc = (Int32)Conversiones.GetValueByType(sal_transacc, typeof(Int32));
	this.sal_periodo = (Int32)Conversiones.GetValueByType(sal_periodo, typeof(Int32));
	this.sal_mes = (Int32)Conversiones.GetValueByType(sal_mes, typeof(Int32));

    this.sal_empresa_key = (Int32)Conversiones.GetValueByType(sal_empresa_key, typeof(Int32));
    this.sal_cuenta_key = (Int32)Conversiones.GetValueByType(sal_cuenta_key, typeof(Int32));
    this.sal_centro_key = (Int32)Conversiones.GetValueByType(sal_centro_key, typeof(Int32));
    this.sal_almacen_key = (Int32)Conversiones.GetValueByType(sal_almacen_key, typeof(Int32));
    this.sal_transacc_key = (Int32)Conversiones.GetValueByType(sal_transacc_key, typeof(Int32));
    this.sal_periodo_key = (Int32)Conversiones.GetValueByType(sal_periodo_key, typeof(Int32));
    this.sal_mes_key = (Int32)Conversiones.GetValueByType(sal_mes_key, typeof(Int32));



	this.sal_debito = (Decimal)Conversiones.GetValueByType(sal_debito, typeof(Decimal));
	this.sal_credito = (Decimal)Conversiones.GetValueByType(sal_credito, typeof(Decimal));
	this.sal_debext = (Decimal)Conversiones.GetValueByType(sal_debext, typeof(Decimal));
	this.sal_creext = (Decimal)Conversiones.GetValueByType(sal_creext, typeof(Decimal));

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
