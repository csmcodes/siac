using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
	public class Dbancario
	{
		#region Properties

		[Data(key = true)]
		public Int32 dban_empresa { get; set; }
		[Data(originalkey = true)]
		public Int32 dban_empresa_key { get; set; }
		[Data(key = true)]
		public Int64 dban_cco_comproba { get; set; }
		[Data(originalkey = true)]
		public Int64 dban_cco_comproba_key { get; set; }
		[Data(key = true)]
		public Int32 dban_secuencia { get; set; }
		[Data(originalkey = true)]
		public Int32 dban_secuencia_key { get; set; }
		public Int32 dban_banco { get; set; }
		public Int32 dban_transacc { get; set; }
		public String dban_documento { get; set; }
		public Int32 dban_debcre { get; set; }
		public Decimal dban_valor_nac { get; set; }
		public Int32 dban_conciliacion { get; set; }
		public DateTime? dban_fechacsc { get; set; }
		public DateTime? dban_fechaant { get; set; }
		public Decimal? dban_valor_ext { get; set; }
		public Int32? dban_impreso { get; set; }
		public Int32? dban_cruzado { get; set; }
		public String crea_usr { get; set; }
		[Data(noupdate = true)]
		public DateTime? crea_fecha { get; set; }
		[Data(noupdate = true)]
		public String mod_usr { get; set; }
		public DateTime? mod_fecha { get; set; }
		public Int32? dban_cliente { get; set; }
		public String dban_beneficiario { get; set; }

		[Data(noprop = true)]
		public Decimal? dban_saldo { get; set; }

		[Data(nosql = true, tablaref = "banco", camporef = "ban_id", foreign = "dban_empresa, dban_banco", keyref = "ban_empresa, ban_codigo", join = "left")]
		public string dban_bancoid { get; set; }
		[Data(nosql = true, tablaref = "banco", camporef = "ban_nombre", foreign = "dban_empresa, dban_banco", keyref = "ban_empresa, ban_codigo", join = "left")]
		public string dban_banconombre { get; set; }
		[Data(nosql = true, tablaref = "banco", camporef = "ban_cuenta", foreign = "dban_empresa, dban_banco", keyref = "ban_empresa, ban_codigo", join = "left")]
		public string dban_bancocuenta { get; set; }


		[Data(nosql = true, tablaref = "comprobante", camporef = "com_fecha", foreign = "dban_empresa, dban_cco_comproba", keyref = "com_empresa, com_codigo", join = "inner")]
		public DateTime? dban_comprobantefecha { get; set; }
		[Data(nosql = true, tablaref = "comprobante", camporef = "com_concepto", foreign = "dban_empresa, dban_cco_comproba", keyref = "com_empresa, com_codigo", join = "inner")]
		public string dban_compconcepto { get; set; }
		[Data(nosql = true, tablaref = "comprobante", camporef = "com_doctran", foreign = "dban_empresa, dban_cco_comproba", keyref = "com_empresa, com_codigo", join = "inner")]
		public string dban_compdoctran { get; set; }


		#endregion

		#region Constructors


		public Dbancario()
		{
		}

		public Dbancario(Int32 dban_empresa, Int64 dban_cco_comproba, Int32 dban_secuencia, Int32 dban_banco, Int32 dban_transacc, String dban_documento, Int32 dban_debcre, Decimal dban_valor_nac, Int32 dban_conciliacion, DateTime dban_fechacsc, DateTime dban_fechaant, Decimal dban_valor_ext, Int32 dban_impreso, Int32 dban_cruzado, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha, Int32 dban_cliente, String dban_beneficiario)
		{
			this.dban_empresa = dban_empresa;
			this.dban_cco_comproba = dban_cco_comproba;
			this.dban_secuencia = dban_secuencia;
			this.dban_empresa_key = dban_empresa;
			this.dban_cco_comproba_key = dban_cco_comproba;
			this.dban_secuencia_key = dban_secuencia;
			this.dban_banco = dban_banco;
			this.dban_transacc = dban_transacc;
			this.dban_documento = dban_documento;
			this.dban_debcre = dban_debcre;
			this.dban_valor_nac = dban_valor_nac;
			this.dban_conciliacion = dban_conciliacion;
			this.dban_fechacsc = dban_fechacsc;
			this.dban_fechaant = dban_fechaant;
			this.dban_valor_ext = dban_valor_ext;
			this.dban_impreso = dban_impreso;
			this.dban_cruzado = dban_cruzado;
			this.crea_usr = crea_usr;
			this.crea_fecha = crea_fecha;
			this.mod_usr = mod_usr;
			this.mod_fecha = mod_fecha;
			this.dban_cliente = dban_cliente;
			this.dban_beneficiario = dban_beneficiario;



		}

		public Dbancario(IDataReader reader)
		{
			this.dban_empresa = (Int32)reader["dban_empresa"];
			this.dban_cco_comproba = (Int64)reader["dban_cco_comproba"];
			this.dban_secuencia = (Int32)reader["dban_secuencia"];
			this.dban_empresa_key = (Int32)reader["dban_empresa"];
			this.dban_cco_comproba_key = (Int64)reader["dban_cco_comproba"];
			this.dban_secuencia_key = (Int32)reader["dban_secuencia"];
			this.dban_banco = (Int32)reader["dban_banco"];
			this.dban_transacc = (Int32)reader["dban_transacc"];
			this.dban_documento = reader["dban_documento"].ToString();
			this.dban_debcre = (Int32)reader["dban_debcre"];
			this.dban_valor_nac = (Decimal)reader["dban_valor_nac"];
			this.dban_conciliacion = (Int32)reader["dban_conciliacion"];
			this.dban_fechacsc = (reader["dban_fechacsc"] != DBNull.Value) ? (DateTime?)reader["dban_fechacsc"] : null;
			this.dban_fechaant = (reader["dban_fechaant"] != DBNull.Value) ? (DateTime?)reader["dban_fechaant"] : null;
			this.dban_valor_ext = (reader["dban_valor_ext"] != DBNull.Value) ? (Decimal?)reader["dban_valor_ext"] : null;
			this.dban_impreso = (reader["dban_impreso"] != DBNull.Value) ? (Int32?)reader["dban_impreso"] : null;
			this.dban_cruzado = (reader["dban_cruzado"] != DBNull.Value) ? (Int32?)reader["dban_cruzado"] : null;
			this.crea_usr = reader["crea_usr"].ToString();
			this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
			this.mod_usr = reader["mod_usr"].ToString();
			this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
			this.dban_cliente = (reader["dban_cliente"] != DBNull.Value) ? (Int32?)reader["dban_cliente"] : null;
			this.dban_beneficiario = reader["dban_beneficiario"].ToString();
			this.dban_bancoid = reader["dban_bancoid"].ToString();
			this.dban_banconombre = reader["dban_banconombre"].ToString();
			this.dban_bancocuenta = reader["dban_bancocuenta"].ToString();

			this.dban_comprobantefecha = (reader["dban_comprobantefecha"] != DBNull.Value) ? (DateTime?)reader["dban_comprobantefecha"] : null;
			this.dban_compconcepto = (reader["dban_compconcepto"] != DBNull.Value) ? (String)reader["dban_compconcepto"] : null;
			this.dban_compdoctran = (reader["dban_compdoctran"] != DBNull.Value) ? (String)reader["dban_compdoctran"] : null;

		}


		public Dbancario(object objeto)
		{
			if (objeto != null)
			{
				Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
				object dban_empresa = null;
				object dban_cco_comproba = null;
				object dban_secuencia = null;

				object dban_empresa_key = null;
				object dban_cco_comproba_key = null;
				object dban_secuencia_key = null;
				object dban_banco = null;
				object dban_transacc = null;
				object dban_documento = null;
				object dban_debcre = null;
				object dban_valor_nac = null;
				object dban_conciliacion = null;
				object dban_fechacsc = null;
				object dban_fechaant = null;
				object dban_valor_ext = null;
				object dban_impreso = null;
				object dban_cruzado = null;
				object crea_usr = null;
				object crea_fecha = null;
				object mod_usr = null;
				object mod_fecha = null;
				object dban_cliente = null;
				object dban_beneficiario = null;


				tmp.TryGetValue("dban_empresa", out dban_empresa);
				tmp.TryGetValue("dban_cco_comproba", out dban_cco_comproba);
				tmp.TryGetValue("dban_secuencia", out dban_secuencia);
				tmp.TryGetValue("dban_empresa_key", out dban_empresa_key);
				tmp.TryGetValue("dban_cco_comproba_key", out dban_cco_comproba_key);
				tmp.TryGetValue("dban_secuencia_key", out dban_secuencia_key);
				tmp.TryGetValue("dban_banco", out dban_banco);
				tmp.TryGetValue("dban_transacc", out dban_transacc);
				tmp.TryGetValue("dban_documento", out dban_documento);
				tmp.TryGetValue("dban_debcre", out dban_debcre);
				tmp.TryGetValue("dban_valor_nac", out dban_valor_nac);
				tmp.TryGetValue("dban_conciliacion", out dban_conciliacion);
				tmp.TryGetValue("dban_fechacsc", out dban_fechacsc);
				tmp.TryGetValue("dban_fechaant", out dban_fechaant);
				tmp.TryGetValue("dban_valor_ext", out dban_valor_ext);
				tmp.TryGetValue("dban_impreso", out dban_impreso);
				tmp.TryGetValue("dban_cruzado", out dban_cruzado);
				tmp.TryGetValue("crea_usr", out crea_usr);
				tmp.TryGetValue("crea_fecha", out crea_fecha);
				tmp.TryGetValue("mod_usr", out mod_usr);
				tmp.TryGetValue("mod_fecha", out mod_fecha);
				tmp.TryGetValue("dban_cliente", out dban_cliente);
				tmp.TryGetValue("dban_beneficiario", out dban_beneficiario);


				this.dban_empresa = (Int32)Conversiones.GetValueByType(dban_empresa, typeof(Int32));
				this.dban_cco_comproba = (Int64)Conversiones.GetValueByType(dban_cco_comproba, typeof(Int64));
				this.dban_secuencia = (Int32)Conversiones.GetValueByType(dban_secuencia, typeof(Int32));
				this.dban_empresa_key = (Int32)Conversiones.GetValueByType(dban_empresa_key, typeof(Int32));
				this.dban_cco_comproba_key = (Int64)Conversiones.GetValueByType(dban_cco_comproba_key, typeof(Int64));
				this.dban_secuencia_key = (Int32)Conversiones.GetValueByType(dban_secuencia_key, typeof(Int32));
				this.dban_banco = (Int32)Conversiones.GetValueByType(dban_banco, typeof(Int32));
				this.dban_transacc = (Int32)Conversiones.GetValueByType(dban_transacc, typeof(Int32));
				this.dban_documento = (String)Conversiones.GetValueByType(dban_documento, typeof(String));
				this.dban_debcre = (Int32)Conversiones.GetValueByType(dban_debcre, typeof(Int32));
				this.dban_valor_nac = (Decimal)Conversiones.GetValueByType(dban_valor_nac, typeof(Decimal));
				this.dban_conciliacion = (Int32)Conversiones.GetValueByType(dban_conciliacion, typeof(Int32));
				this.dban_fechacsc = (DateTime?)Conversiones.GetValueByType(dban_fechacsc, typeof(DateTime?));
				this.dban_fechaant = (DateTime?)Conversiones.GetValueByType(dban_fechaant, typeof(DateTime?));
				this.dban_valor_ext = (Decimal?)Conversiones.GetValueByType(dban_valor_ext, typeof(Decimal?));
				this.dban_impreso = (Int32?)Conversiones.GetValueByType(dban_impreso, typeof(Int32?));
				this.dban_cruzado = (Int32?)Conversiones.GetValueByType(dban_cruzado, typeof(Int32?));
				this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
				this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
				this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
				this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
				this.dban_cliente = (Int32?)Conversiones.GetValueByType(dban_cliente, typeof(Int32?));
				this.dban_beneficiario = (String)Conversiones.GetValueByType(dban_beneficiario, typeof(String));

			}
		}
		#endregion

		#region Methods
		public PropertyInfo[] GetProperties()
		{
			return this.GetType().GetProperties();
		}

		public List<Dbancario> GetStruc()
		{
			return new List<Dbancario>();
		}
		#endregion


	}
}
