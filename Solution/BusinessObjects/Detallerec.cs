using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;


/// <summary>
/// Clase usada para registro de cobros, 
/// Solo es un Business Object, no tien clases de DAL y BLL, pues no esta en BD
/// </summary>

namespace BusinessObjects
{
	public class Detallerec
	{
		#region Properties

		[Data(key = true)]
		public Int32 dre_empresa { get; set; }
		[Data(originalkey = true)]
		public Int32 dre_empresa_key { get; set; }
		[Data(key = true)]
		public Int64 dre_comprobante { get; set; }
		[Data(originalkey = true)]
		public Int64 dre_comprobante_key { get; set; }
		[Data(key = true)]
		public Int32 dre_secuencia { get; set; }
		[Data(originalkey = true)]
		public Int32 dre_secuencia_key { get; set; }
		public String dre_id { get; set; }
		public String dre_tipo { get; set; }
		public String dre_emisor { get; set; }
		public Int32? dre_banco { get; set; }
		public String dre_nro { get; set; }
		public Decimal? dre_porcentaje { get; set; }
		public String dre_tiporet { get; set; }
		public String dre_observacion { get; set; }
		public Decimal? dre_valor { get; set; }
		public Int32? det_estado { get; set; }
		public String crea_usr { get; set; }
		public DateTime? crea_fecha { get; set; }
		public String mod_usr { get; set; }
		public DateTime? mod_fecha { get; set; }


		#endregion

		#region Constructors


		public Detallerec()
		{
		}

		public Detallerec(Int32 dre_empresa, Int64 dre_comprobante, Int32 dre_secuencia, String dre_id, String dre_tipo, String dre_emisor, Int32? dre_banco, String dre_nro, Decimal dre_porcentaje, String dre_tiporet, String dre_observacion, Decimal dre_valor, Int32 det_estado, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha)
		{
			this.dre_empresa = dre_empresa;
			this.dre_comprobante = dre_comprobante;
			this.dre_secuencia = dre_secuencia;
			this.dre_id = dre_id;
			this.dre_tipo = dre_tipo;
			this.dre_emisor = dre_emisor;
			this.dre_banco = dre_banco;
			this.dre_nro = dre_nro;
			this.dre_porcentaje = dre_porcentaje;
			this.dre_tiporet = dre_tiporet;
			this.dre_observacion = dre_observacion;
			this.dre_valor = dre_valor;
			this.det_estado = det_estado;
			this.crea_usr = crea_usr;
			this.crea_fecha = crea_fecha;
			this.mod_usr = mod_usr;
			this.mod_fecha = mod_fecha;


		}

		public Detallerec(IDataReader reader)
		{
			this.dre_empresa = (Int32)reader["dre_empresa"];
			this.dre_comprobante = (Int64)reader["dre_comprobante"];
			this.dre_secuencia = (Int32)reader["dre_secuencia"];
			this.dre_id = reader["dre_id"].ToString();
			this.dre_tipo = reader["dre_tipo"].ToString();
			this.dre_emisor = reader["dre_emisor"].ToString();
			this.dre_banco = (reader["dre_banco"] != DBNull.Value) ? (Int32?)reader["dre_banco"] : null;
			this.dre_nro = reader["dre_nro"].ToString();
			this.dre_porcentaje = (reader["dre_porcentaje"] != DBNull.Value) ? (Decimal?)reader["dre_porcentaje"] : null;
			this.dre_tiporet = reader["dre_tiporet"].ToString();
			this.dre_observacion = reader["dre_observacion"].ToString();
			this.dre_valor = (reader["dre_valor"] != DBNull.Value) ? (Decimal?)reader["dre_valor"] : null;
			this.det_estado = (reader["det_estado"] != DBNull.Value) ? (Int32?)reader["det_estado"] : null;
			this.crea_usr = reader["crea_usr"].ToString();
			this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
			this.mod_usr = reader["mod_usr"].ToString();
			this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

		}


		public Detallerec(object objeto)
		{
			if (objeto != null)
			{
				Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
				object dre_empresa = null;
				object dre_comprobante = null;
				object dre_secuencia = null;
				object dre_id = null;
				object dre_tipo = null;
				object dre_emisor = null;
				object dre_banco = null;
				object dre_nro = null;
				object dre_porcentaje = null;
				object dre_tiporet = null;
				object dre_observacion = null;
				object dre_valor = null;
				object det_estado = null;
				object crea_usr = null;
				object crea_fecha = null;
				object mod_usr = null;
				object mod_fecha = null;


				tmp.TryGetValue("dre_empresa", out dre_empresa);
				tmp.TryGetValue("dre_comprobante", out dre_comprobante);
				tmp.TryGetValue("dre_secuencia", out dre_secuencia);
				tmp.TryGetValue("dre_id", out dre_id);
				tmp.TryGetValue("dre_tipo", out dre_tipo);
				tmp.TryGetValue("dre_emisor", out dre_emisor);
				tmp.TryGetValue("dre_banco", out dre_banco);
				tmp.TryGetValue("dre_nro", out dre_nro);
				tmp.TryGetValue("dre_porcentaje", out dre_porcentaje);
				tmp.TryGetValue("dre_tiporet", out dre_tiporet);
				tmp.TryGetValue("dre_observacion", out dre_observacion);
				tmp.TryGetValue("dre_valor", out dre_valor);
				tmp.TryGetValue("det_estado", out det_estado);
				tmp.TryGetValue("crea_usr", out crea_usr);
				tmp.TryGetValue("crea_fecha", out crea_fecha);
				tmp.TryGetValue("mod_usr", out mod_usr);
				tmp.TryGetValue("mod_fecha", out mod_fecha);


				this.dre_empresa = (Int32)Conversiones.GetValueByType(dre_empresa, typeof(Int32));
				this.dre_comprobante = (Int64)Conversiones.GetValueByType(dre_comprobante, typeof(Int64));
				this.dre_secuencia = (Int32)Conversiones.GetValueByType(dre_secuencia, typeof(Int32));
				this.dre_id = (String)Conversiones.GetValueByType(dre_id, typeof(String));
				this.dre_tipo = (String)Conversiones.GetValueByType(dre_tipo, typeof(String));
				this.dre_emisor = (String)Conversiones.GetValueByType(dre_emisor, typeof(String));
				this.dre_banco = (Int32?)Conversiones.GetValueByType(dre_banco, typeof(Int32?));
				this.dre_nro = (String)Conversiones.GetValueByType(dre_nro, typeof(String));
				this.dre_porcentaje = (Decimal?)Conversiones.GetValueByType(dre_porcentaje, typeof(Decimal?));
				this.dre_tiporet = (String)Conversiones.GetValueByType(dre_tiporet, typeof(String));
				this.dre_observacion = (String)Conversiones.GetValueByType(dre_observacion, typeof(String));
				this.dre_valor = (Decimal?)Conversiones.GetValueByType(dre_valor, typeof(Decimal?));
				this.det_estado = (Int32?)Conversiones.GetValueByType(det_estado, typeof(Int32?));
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
