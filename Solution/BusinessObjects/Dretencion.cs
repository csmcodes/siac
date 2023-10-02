using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Dretencion
    {
        #region Properties

    	[Data(key = true)]
	public Int32 drt_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 drt_empresa_key { get; set; }
	[Data(key = true)]
	public Int64 drt_comprobante { get; set; }
	[Data(originalkey = true)]
	public Int64 drt_comprobante_key { get; set; }
    [Data(key = true, auto = true)]
	public Int32 drt_secuencia { get; set; }
	[Data(originalkey = true)]
	public Int32 drt_secuencia_key { get; set; }
	public Int32? drt_impuesto { get; set; }
	public Int32? drt_cuenta { get; set; }
	public Decimal? drt_valor { get; set; }
	public Decimal? drt_porcentaje { get; set; }
	public Decimal? drt_total { get; set; }
	public Int32? drt_debcre { get; set; }
	public Int32? drt_cuenta_transacc { get; set; }
	public Int32? drt_con_codigo { get; set; }
	public String drt_factura { get; set; }
         [Data(noupdate = true)]
	public String crea_usr { get; set; }
         [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }
    [Data(nosql = true, tablaref = "impuesto", camporef = "imp_id", foreign = "drt_empresa, drt_impuesto", keyref = "imp_empresa, imp_codigo", join = "left")]
    public string drt_impuestoid { get; set; }
    [Data(nosql = true, tablaref = "impuesto", camporef = "imp_nombre", foreign = "drt_empresa, drt_impuesto", keyref = "imp_empresa, imp_codigo", join = "left")]
 /* public Int32? drt_impuestocuenta { get; set; }
    [Data(nosql = true, tablaref = "impuesto", camporef = "imp_cuenta", foreign = "drt_empresa, drt_impuesto", keyref = "imp_empresa, imp_codigo", join = "left")]
 */

    public string drt_impuestonombre { get; set; }
    [Data(nosql = true, tablaref = "concepto", camporef = "con_nombre", foreign = "drt_empresa, drt_con_codigo", keyref = "con_empresa, con_codigo", join = "left")]
    public string drt_conceptonombre { get; set; }
              
        #endregion

        #region Constructors


        public  Dretencion()
        {
        }

        public  Dretencion( Int32 drt_empresa,Int64 drt_comprobante,Int32 drt_secuencia,Int32 drt_impuesto,Int32 drt_cuenta,Decimal drt_valor,Decimal drt_porcentaje,Decimal drt_total,Int32 drt_debcre,Int32 drt_cuenta_transacc,Int32 drt_con_codigo,String drt_factura,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.drt_empresa = drt_empresa;
	this.drt_comprobante = drt_comprobante;
	this.drt_secuencia = drt_secuencia;
	this.drt_impuesto = drt_impuesto;
	this.drt_cuenta = drt_cuenta;
	this.drt_valor = drt_valor;
	this.drt_porcentaje = drt_porcentaje;
	this.drt_total = drt_total;
	this.drt_debcre = drt_debcre;
	this.drt_cuenta_transacc = drt_cuenta_transacc;
	this.drt_con_codigo = drt_con_codigo;
	this.drt_factura = drt_factura;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Dretencion(IDataReader reader)
        {
    	this.drt_empresa = (Int32)reader["drt_empresa"];
	this.drt_comprobante = (Int64)reader["drt_comprobante"];
	this.drt_secuencia = (Int32)reader["drt_secuencia"];
	this.drt_impuesto = (reader["drt_impuesto"] != DBNull.Value) ? (Int32?)reader["drt_impuesto"] : null;
	this.drt_cuenta = (reader["drt_cuenta"] != DBNull.Value) ? (Int32?)reader["drt_cuenta"] : null;
	this.drt_valor = (reader["drt_valor"] != DBNull.Value) ? (Decimal?)reader["drt_valor"] : null;
	this.drt_porcentaje = (reader["drt_porcentaje"] != DBNull.Value) ? (Decimal?)reader["drt_porcentaje"] : null;
	this.drt_total = (reader["drt_total"] != DBNull.Value) ? (Decimal?)reader["drt_total"] : null;
	this.drt_debcre = (reader["drt_debcre"] != DBNull.Value) ? (Int32?)reader["drt_debcre"] : null;
	this.drt_cuenta_transacc = (reader["drt_cuenta_transacc"] != DBNull.Value) ? (Int32?)reader["drt_cuenta_transacc"] : null;
	this.drt_con_codigo = (reader["drt_con_codigo"] != DBNull.Value) ? (Int32?)reader["drt_con_codigo"] : null;
	this.drt_factura = reader["drt_factura"].ToString();
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;


    this.drt_impuestoid = reader["drt_impuestoid"].ToString();
    this.drt_impuestonombre = reader["drt_impuestonombre"].ToString();
    this.drt_conceptonombre = reader["drt_conceptonombre"].ToString();
  //  this.drt_impuestocuenta= (reader["drt_impuestocuenta"] != DBNull.Value) ? (Int32?)reader["drt_impuestocuenta"] : null;

  

        }


        public Dretencion(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object drt_empresa = null;
	object drt_comprobante = null;
	object drt_secuencia = null;
	object drt_impuesto = null;
	object drt_cuenta = null;
	object drt_valor = null;
	object drt_porcentaje = null;
	object drt_total = null;
	object drt_debcre = null;
	object drt_cuenta_transacc = null;
	object drt_con_codigo = null;
	object drt_factura = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("drt_empresa", out drt_empresa);
	tmp.TryGetValue("drt_comprobante", out drt_comprobante);
	tmp.TryGetValue("drt_secuencia", out drt_secuencia);
	tmp.TryGetValue("drt_impuesto", out drt_impuesto);
	tmp.TryGetValue("drt_cuenta", out drt_cuenta);
	tmp.TryGetValue("drt_valor", out drt_valor);
	tmp.TryGetValue("drt_porcentaje", out drt_porcentaje);
	tmp.TryGetValue("drt_total", out drt_total);
	tmp.TryGetValue("drt_debcre", out drt_debcre);
	tmp.TryGetValue("drt_cuenta_transacc", out drt_cuenta_transacc);
	tmp.TryGetValue("drt_con_codigo", out drt_con_codigo);
	tmp.TryGetValue("drt_factura", out drt_factura);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.drt_empresa = (Int32)Conversiones.GetValueByType(drt_empresa, typeof(Int32));
	this.drt_comprobante = (Int64)Conversiones.GetValueByType(drt_comprobante, typeof(Int64));
	this.drt_secuencia = (Int32)Conversiones.GetValueByType(drt_secuencia, typeof(Int32));
	this.drt_impuesto = (Int32?)Conversiones.GetValueByType(drt_impuesto, typeof(Int32?));
	this.drt_cuenta = (Int32?)Conversiones.GetValueByType(drt_cuenta, typeof(Int32?));
	this.drt_valor = (Decimal?)Conversiones.GetValueByType(drt_valor, typeof(Decimal?));
	this.drt_porcentaje = (Decimal?)Conversiones.GetValueByType(drt_porcentaje, typeof(Decimal?));
	this.drt_total = (Decimal?)Conversiones.GetValueByType(drt_total, typeof(Decimal?));
	this.drt_debcre = (Int32?)Conversiones.GetValueByType(drt_debcre, typeof(Int32?));
	this.drt_cuenta_transacc = (Int32?)Conversiones.GetValueByType(drt_cuenta_transacc, typeof(Int32?));
	this.drt_con_codigo = (Int32?)Conversiones.GetValueByType(drt_con_codigo, typeof(Int32?));
	this.drt_factura = (String)Conversiones.GetValueByType(drt_factura, typeof(String));
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
