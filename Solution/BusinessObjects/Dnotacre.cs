using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Dnotacre
    {
        #region Properties

    	[Data(key = true)]
	public Int32 dnc_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 dnc_empresa_key { get; set; }
	[Data(key = true)]
	public Int64 dnc_comprobante { get; set; }
	[Data(originalkey = true)]
	public Int64 dnc_comprobante_key { get; set; }
	[Data(key = true)]
	public Int32 dnc_secuencia { get; set; }
	[Data(originalkey = true)]
	public Int32 dnc_secuencia_key { get; set; }
	public Int32? dnc_tiponc { get; set; }
	public Decimal? dnc_valor { get; set; }
	public Decimal? dnc_valor_ext { get; set; }
	public Decimal? dnc_tipo_cambio { get; set; }
	public Int32? dnc_banco { get; set; }
	public String dnc_documento { get; set; }
	public String dnc_nrocheque { get; set; }
	public Int32? dnc_emisor { get; set; }
	public String dnc_nro_cuenta { get; set; }
	public Int32? dnc_cheque { get; set; }
	public Int32? dnc_cliente { get; set; }
	public DateTime? dnc_fecha_che { get; set; }
        [Data(noupdate = true)]
	public String crea_usr { get; set; }
        [Data(noupdate = true)]
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }


    [Data(nosql = true, tablaref = "tiponc", camporef = "tnc_id", foreign = "dnc_empresa, dnc_tiponc", keyref = "tnc_empresa, tnc_codigo", join = "left")]
    public string dnc_tiponcid { get; set; }
    [Data(nosql = true, tablaref = "tiponc", camporef = "tnc_nombre", foreign = "dnc_empresa, dnc_tiponc", keyref = "tnc_empresa, tnc_codigo", join = "left")]
    public string dnc_tiponcnombre { get; set; }



        #endregion

        #region Constructors


        public  Dnotacre()
        {
        }

        public  Dnotacre( Int32 dnc_empresa,Int64 dnc_comprobante,Int32 dnc_secuencia,Int32 dnc_tiponc,Decimal dnc_valor,Decimal dnc_valor_ext,Decimal dnc_tipo_cambio,Int32 dnc_banco,String dnc_documento,String dnc_nrocheque,Int32 dnc_emisor,String dnc_nro_cuenta,Int32 dnc_cheque,Int32 dnc_cliente,DateTime dnc_fecha_che,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.dnc_empresa = dnc_empresa;
	this.dnc_comprobante = dnc_comprobante;
	this.dnc_secuencia = dnc_secuencia;
	this.dnc_tiponc = dnc_tiponc;
	this.dnc_valor = dnc_valor;
	this.dnc_valor_ext = dnc_valor_ext;
	this.dnc_tipo_cambio = dnc_tipo_cambio;
	this.dnc_banco = dnc_banco;
	this.dnc_documento = dnc_documento;
	this.dnc_nrocheque = dnc_nrocheque;
	this.dnc_emisor = dnc_emisor;
	this.dnc_nro_cuenta = dnc_nro_cuenta;
	this.dnc_cheque = dnc_cheque;
	this.dnc_cliente = dnc_cliente;
	this.dnc_fecha_che = dnc_fecha_che;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Dnotacre(IDataReader reader)
        {
    	this.dnc_empresa = (Int32)reader["dnc_empresa"];
	this.dnc_comprobante = (Int64)reader["dnc_comprobante"];
	this.dnc_secuencia = (Int32)reader["dnc_secuencia"];
	this.dnc_tiponc = (reader["dnc_tiponc"] != DBNull.Value) ? (Int32?)reader["dnc_tiponc"] : null;
	this.dnc_valor = (reader["dnc_valor"] != DBNull.Value) ? (Decimal?)reader["dnc_valor"] : null;
	this.dnc_valor_ext = (reader["dnc_valor_ext"] != DBNull.Value) ? (Decimal?)reader["dnc_valor_ext"] : null;
	this.dnc_tipo_cambio = (reader["dnc_tipo_cambio"] != DBNull.Value) ? (Decimal?)reader["dnc_tipo_cambio"] : null;
	this.dnc_banco = (reader["dnc_banco"] != DBNull.Value) ? (Int32?)reader["dnc_banco"] : null;
	this.dnc_documento = reader["dnc_documento"].ToString();
	this.dnc_nrocheque = reader["dnc_nrocheque"].ToString();
	this.dnc_emisor = (reader["dnc_emisor"] != DBNull.Value) ? (Int32?)reader["dnc_emisor"] : null;
	this.dnc_nro_cuenta = reader["dnc_nro_cuenta"].ToString();
	this.dnc_cheque = (reader["dnc_cheque"] != DBNull.Value) ? (Int32?)reader["dnc_cheque"] : null;
	this.dnc_cliente = (reader["dnc_cliente"] != DBNull.Value) ? (Int32?)reader["dnc_cliente"] : null;
	this.dnc_fecha_che = (reader["dnc_fecha_che"] != DBNull.Value) ? (DateTime?)reader["dnc_fecha_che"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
    this.dnc_tiponcid = reader["dnc_tiponcid"].ToString();
    this.dnc_tiponcnombre = reader["dnc_tiponcnombre"].ToString();
        }


        public Dnotacre(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object dnc_empresa = null;
	object dnc_comprobante = null;
	object dnc_secuencia = null;
	object dnc_tiponc = null;
	object dnc_valor = null;
	object dnc_valor_ext = null;
	object dnc_tipo_cambio = null;
	object dnc_banco = null;
	object dnc_documento = null;
	object dnc_nrocheque = null;
	object dnc_emisor = null;
	object dnc_nro_cuenta = null;
	object dnc_cheque = null;
	object dnc_cliente = null;
	object dnc_fecha_che = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("dnc_empresa", out dnc_empresa);
	tmp.TryGetValue("dnc_comprobante", out dnc_comprobante);
	tmp.TryGetValue("dnc_secuencia", out dnc_secuencia);
	tmp.TryGetValue("dnc_tiponc", out dnc_tiponc);
	tmp.TryGetValue("dnc_valor", out dnc_valor);
	tmp.TryGetValue("dnc_valor_ext", out dnc_valor_ext);
	tmp.TryGetValue("dnc_tipo_cambio", out dnc_tipo_cambio);
	tmp.TryGetValue("dnc_banco", out dnc_banco);
	tmp.TryGetValue("dnc_documento", out dnc_documento);
	tmp.TryGetValue("dnc_nrocheque", out dnc_nrocheque);
	tmp.TryGetValue("dnc_emisor", out dnc_emisor);
	tmp.TryGetValue("dnc_nro_cuenta", out dnc_nro_cuenta);
	tmp.TryGetValue("dnc_cheque", out dnc_cheque);
	tmp.TryGetValue("dnc_cliente", out dnc_cliente);
	tmp.TryGetValue("dnc_fecha_che", out dnc_fecha_che);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.dnc_empresa = (Int32)Conversiones.GetValueByType(dnc_empresa, typeof(Int32));
	this.dnc_comprobante = (Int64)Conversiones.GetValueByType(dnc_comprobante, typeof(Int64));
	this.dnc_secuencia = (Int32)Conversiones.GetValueByType(dnc_secuencia, typeof(Int32));
	this.dnc_tiponc = (Int32?)Conversiones.GetValueByType(dnc_tiponc, typeof(Int32?));
	this.dnc_valor = (Decimal?)Conversiones.GetValueByType(dnc_valor, typeof(Decimal?));
	this.dnc_valor_ext = (Decimal?)Conversiones.GetValueByType(dnc_valor_ext, typeof(Decimal?));
	this.dnc_tipo_cambio = (Decimal?)Conversiones.GetValueByType(dnc_tipo_cambio, typeof(Decimal?));
	this.dnc_banco = (Int32?)Conversiones.GetValueByType(dnc_banco, typeof(Int32?));
	this.dnc_documento = (String)Conversiones.GetValueByType(dnc_documento, typeof(String));
	this.dnc_nrocheque = (String)Conversiones.GetValueByType(dnc_nrocheque, typeof(String));
	this.dnc_emisor = (Int32?)Conversiones.GetValueByType(dnc_emisor, typeof(Int32?));
	this.dnc_nro_cuenta = (String)Conversiones.GetValueByType(dnc_nro_cuenta, typeof(String));
	this.dnc_cheque = (Int32?)Conversiones.GetValueByType(dnc_cheque, typeof(Int32?));
	this.dnc_cliente = (Int32?)Conversiones.GetValueByType(dnc_cliente, typeof(Int32?));
	this.dnc_fecha_che = (DateTime?)Conversiones.GetValueByType(dnc_fecha_che, typeof(DateTime?));
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
