using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Dmovinv
    {
        #region Properties

    	[Data(key = true)]
	public Int32 dmo_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 dmo_empresa_key { get; set; }
	[Data(key = true)]
	public Int64 dmo_cco_comproba { get; set; }
	[Data(originalkey = true)]
	public Int64 dmo_cco_comproba_key { get; set; }
	[Data(key = true)]
	public Int32 dmo_secuencia { get; set; }
	[Data(originalkey = true)]
	public Int32 dmo_secuencia_key { get; set; }
	public Int32? dmo_producto { get; set; }
	public Int32? dmo_catproducto { get; set; }
	public Int32? dmo_transaccion { get; set; }
	public Int32? dmo_debcre { get; set; }
	public Int32? dmo_bodega { get; set; }
	public Decimal? dmo_cantidad { get; set; }
	public Decimal? dmo_devuelta { get; set; }
	public Decimal? dmo_costo { get; set; }
	public Decimal? dmo_total { get; set; }
	public Decimal? dmo_cant_fisica { get; set; }
	public Decimal? dmo_cant_consigna { get; set; }
	public Decimal? dmo_cant_transito { get; set; }
	public Decimal? dmo_cdigitada { get; set; }
	public Decimal? dmo_pdigitada { get; set; }
	public Int32? dmo_udigitada { get; set; }
	public Int32? dmo_conajuste { get; set; }
	public Int32? dmo_centro { get; set; }
	public Int32? dmo_cuenta { get; set; }
	public Int32? dmo_ctainv { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }
	public String crea_ip { get; set; }
	public String mod_ip { get; set; }


        [Data(nosql = true, tablaref = "producto", camporef = "pro_id", foreign = "dmo_empresa, dmo_producto", keyref = "pro_empresa, pro_codigo", join = "left")]
        public string dmo_productoid { get; set; }
        [Data(nosql = true, tablaref = "producto", camporef = "pro_nombre", foreign = "dmo_empresa, dmo_producto", keyref = "pro_empresa, pro_codigo", join = "left")]
        public string dmo_productonombre { get; set; }

        #endregion

        #region Constructors


        public  Dmovinv()
        {
        }

        public  Dmovinv( Int32 dmo_empresa,Int64 dmo_cco_comproba,Int32 dmo_secuencia,Int32 dmo_producto,Int32 dmo_catproducto,Int32 dmo_transaccion,Int32 dmo_debcre,Int32 dmo_bodega,Decimal dmo_cantidad,Decimal dmo_devuelta,Decimal dmo_costo,Decimal dmo_total,Decimal dmo_cant_fisica,Decimal dmo_cant_consigna,Decimal dmo_cant_transito,Decimal dmo_cdigitada,Decimal dmo_pdigitada,Int32 dmo_udigitada,Int32 dmo_conajuste,Int32 dmo_centro,Int32 dmo_cuenta,Int32 dmo_ctainv,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha,String crea_ip,String mod_ip)
        {                
    	this.dmo_empresa = dmo_empresa;
	this.dmo_cco_comproba = dmo_cco_comproba;
	this.dmo_secuencia = dmo_secuencia;
	this.dmo_producto = dmo_producto;
	this.dmo_catproducto = dmo_catproducto;
	this.dmo_transaccion = dmo_transaccion;
	this.dmo_debcre = dmo_debcre;
	this.dmo_bodega = dmo_bodega;
	this.dmo_cantidad = dmo_cantidad;
	this.dmo_devuelta = dmo_devuelta;
	this.dmo_costo = dmo_costo;
	this.dmo_total = dmo_total;
	this.dmo_cant_fisica = dmo_cant_fisica;
	this.dmo_cant_consigna = dmo_cant_consigna;
	this.dmo_cant_transito = dmo_cant_transito;
	this.dmo_cdigitada = dmo_cdigitada;
	this.dmo_pdigitada = dmo_pdigitada;
	this.dmo_udigitada = dmo_udigitada;
	this.dmo_conajuste = dmo_conajuste;
	this.dmo_centro = dmo_centro;
	this.dmo_cuenta = dmo_cuenta;
	this.dmo_ctainv = dmo_ctainv;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;
	this.crea_ip = crea_ip;
	this.mod_ip = mod_ip;

           
       }

        public Dmovinv(IDataReader reader)
        {
            this.dmo_empresa = (Int32)reader["dmo_empresa"];
            this.dmo_cco_comproba = (Int64)reader["dmo_cco_comproba"];
            this.dmo_secuencia = (Int32)reader["dmo_secuencia"];
            this.dmo_producto = (reader["dmo_producto"] != DBNull.Value) ? (Int32?)reader["dmo_producto"] : null;
            this.dmo_catproducto = (reader["dmo_catproducto"] != DBNull.Value) ? (Int32?)reader["dmo_catproducto"] : null;
            this.dmo_transaccion = (reader["dmo_transaccion"] != DBNull.Value) ? (Int32?)reader["dmo_transaccion"] : null;
            this.dmo_debcre = (reader["dmo_debcre"] != DBNull.Value) ? (Int32?)reader["dmo_debcre"] : null;
            this.dmo_bodega = (reader["dmo_bodega"] != DBNull.Value) ? (Int32?)reader["dmo_bodega"] : null;
            this.dmo_cantidad = (reader["dmo_cantidad"] != DBNull.Value) ? (Decimal?)reader["dmo_cantidad"] : null;
            this.dmo_devuelta = (reader["dmo_devuelta"] != DBNull.Value) ? (Decimal?)reader["dmo_devuelta"] : null;
            this.dmo_costo = (reader["dmo_costo"] != DBNull.Value) ? (Decimal?)reader["dmo_costo"] : null;
            this.dmo_total = (reader["dmo_total"] != DBNull.Value) ? (Decimal?)reader["dmo_total"] : null;
            this.dmo_cant_fisica = (reader["dmo_cant_fisica"] != DBNull.Value) ? (Decimal?)reader["dmo_cant_fisica"] : null;
            this.dmo_cant_consigna = (reader["dmo_cant_consigna"] != DBNull.Value) ? (Decimal?)reader["dmo_cant_consigna"] : null;
            this.dmo_cant_transito = (reader["dmo_cant_transito"] != DBNull.Value) ? (Decimal?)reader["dmo_cant_transito"] : null;
            this.dmo_cdigitada = (reader["dmo_cdigitada"] != DBNull.Value) ? (Decimal?)reader["dmo_cdigitada"] : null;
            this.dmo_pdigitada = (reader["dmo_pdigitada"] != DBNull.Value) ? (Decimal?)reader["dmo_pdigitada"] : null;
            this.dmo_udigitada = (reader["dmo_udigitada"] != DBNull.Value) ? (Int32?)reader["dmo_udigitada"] : null;
            this.dmo_conajuste = (reader["dmo_conajuste"] != DBNull.Value) ? (Int32?)reader["dmo_conajuste"] : null;
            this.dmo_centro = (reader["dmo_centro"] != DBNull.Value) ? (Int32?)reader["dmo_centro"] : null;
            this.dmo_cuenta = (reader["dmo_cuenta"] != DBNull.Value) ? (Int32?)reader["dmo_cuenta"] : null;
            this.dmo_ctainv = (reader["dmo_ctainv"] != DBNull.Value) ? (Int32?)reader["dmo_ctainv"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.crea_ip = reader["crea_ip"].ToString();
            this.mod_ip = reader["mod_ip"].ToString();

            this.dmo_productoid = reader["dmo_productoid"].ToString();
            this.dmo_productonombre = reader["dmo_productonombre"].ToString();



        }


        public Dmovinv(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object dmo_empresa = null;
	object dmo_cco_comproba = null;
	object dmo_secuencia = null;
	object dmo_producto = null;
	object dmo_catproducto = null;
	object dmo_transaccion = null;
	object dmo_debcre = null;
	object dmo_bodega = null;
	object dmo_cantidad = null;
	object dmo_devuelta = null;
	object dmo_costo = null;
	object dmo_total = null;
	object dmo_cant_fisica = null;
	object dmo_cant_consigna = null;
	object dmo_cant_transito = null;
	object dmo_cdigitada = null;
	object dmo_pdigitada = null;
	object dmo_udigitada = null;
	object dmo_conajuste = null;
	object dmo_centro = null;
	object dmo_cuenta = null;
	object dmo_ctainv = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;
	object crea_ip = null;
	object mod_ip = null;


                	tmp.TryGetValue("dmo_empresa", out dmo_empresa);
	tmp.TryGetValue("dmo_cco_comproba", out dmo_cco_comproba);
	tmp.TryGetValue("dmo_secuencia", out dmo_secuencia);
	tmp.TryGetValue("dmo_producto", out dmo_producto);
	tmp.TryGetValue("dmo_catproducto", out dmo_catproducto);
	tmp.TryGetValue("dmo_transaccion", out dmo_transaccion);
	tmp.TryGetValue("dmo_debcre", out dmo_debcre);
	tmp.TryGetValue("dmo_bodega", out dmo_bodega);
	tmp.TryGetValue("dmo_cantidad", out dmo_cantidad);
	tmp.TryGetValue("dmo_devuelta", out dmo_devuelta);
	tmp.TryGetValue("dmo_costo", out dmo_costo);
	tmp.TryGetValue("dmo_total", out dmo_total);
	tmp.TryGetValue("dmo_cant_fisica", out dmo_cant_fisica);
	tmp.TryGetValue("dmo_cant_consigna", out dmo_cant_consigna);
	tmp.TryGetValue("dmo_cant_transito", out dmo_cant_transito);
	tmp.TryGetValue("dmo_cdigitada", out dmo_cdigitada);
	tmp.TryGetValue("dmo_pdigitada", out dmo_pdigitada);
	tmp.TryGetValue("dmo_udigitada", out dmo_udigitada);
	tmp.TryGetValue("dmo_conajuste", out dmo_conajuste);
	tmp.TryGetValue("dmo_centro", out dmo_centro);
	tmp.TryGetValue("dmo_cuenta", out dmo_cuenta);
	tmp.TryGetValue("dmo_ctainv", out dmo_ctainv);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);
	tmp.TryGetValue("crea_ip", out crea_ip);
	tmp.TryGetValue("mod_ip", out mod_ip);


                	this.dmo_empresa = (Int32)Conversiones.GetValueByType(dmo_empresa, typeof(Int32));
	this.dmo_cco_comproba = (Int64)Conversiones.GetValueByType(dmo_cco_comproba, typeof(Int64));
	this.dmo_secuencia = (Int32)Conversiones.GetValueByType(dmo_secuencia, typeof(Int32));
	this.dmo_producto = (Int32?)Conversiones.GetValueByType(dmo_producto, typeof(Int32?));
	this.dmo_catproducto = (Int32?)Conversiones.GetValueByType(dmo_catproducto, typeof(Int32?));
	this.dmo_transaccion = (Int32?)Conversiones.GetValueByType(dmo_transaccion, typeof(Int32?));
	this.dmo_debcre = (Int32?)Conversiones.GetValueByType(dmo_debcre, typeof(Int32?));
	this.dmo_bodega = (Int32?)Conversiones.GetValueByType(dmo_bodega, typeof(Int32?));
	this.dmo_cantidad = (Decimal?)Conversiones.GetValueByType(dmo_cantidad, typeof(Decimal?));
	this.dmo_devuelta = (Decimal?)Conversiones.GetValueByType(dmo_devuelta, typeof(Decimal?));
	this.dmo_costo = (Decimal?)Conversiones.GetValueByType(dmo_costo, typeof(Decimal?));
	this.dmo_total = (Decimal?)Conversiones.GetValueByType(dmo_total, typeof(Decimal?));
	this.dmo_cant_fisica = (Decimal?)Conversiones.GetValueByType(dmo_cant_fisica, typeof(Decimal?));
	this.dmo_cant_consigna = (Decimal?)Conversiones.GetValueByType(dmo_cant_consigna, typeof(Decimal?));
	this.dmo_cant_transito = (Decimal?)Conversiones.GetValueByType(dmo_cant_transito, typeof(Decimal?));
	this.dmo_cdigitada = (Decimal?)Conversiones.GetValueByType(dmo_cdigitada, typeof(Decimal?));
	this.dmo_pdigitada = (Decimal?)Conversiones.GetValueByType(dmo_pdigitada, typeof(Decimal?));
	this.dmo_udigitada = (Int32?)Conversiones.GetValueByType(dmo_udigitada, typeof(Int32?));
	this.dmo_conajuste = (Int32?)Conversiones.GetValueByType(dmo_conajuste, typeof(Int32?));
	this.dmo_centro = (Int32?)Conversiones.GetValueByType(dmo_centro, typeof(Int32?));
	this.dmo_cuenta = (Int32?)Conversiones.GetValueByType(dmo_cuenta, typeof(Int32?));
	this.dmo_ctainv = (Int32?)Conversiones.GetValueByType(dmo_ctainv, typeof(Int32?));
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
