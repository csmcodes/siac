using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
	public class Dcancelacionsocio
	{
		#region Properties

		[Data(key = true)]
		public Int32 dcs_empresa { get; set; }
		[Data(originalkey = true)]
		public Int32 dcs_empresa_key { get; set; }
		[Data(key = true, auto = true)]
		public Int32 dcs_codigo { get; set; }
		[Data(originalkey = true)]
		public Int32 dcs_codigo_key { get; set; }
		public DateTime? dcs_fecha { get; set; }
		public Int64 dcs_comprobante { get; set; }
		public Int32 dcs_transacc { get; set; }
		public String dcs_doctran { get; set; }
		public Int32 dcs_pago { get; set; }
		public Int32? dcs_socio { get; set; }
		public Decimal? dcs_monto { get; set; }
		public String dcs_tipo { get; set; }
		public String dcs_nrodoc { get; set; }
		public String dcs_observacion { get; set; }
		public String crea_usr { get; set; }
		public DateTime? crea_fecha { get; set; }
		public String mod_usr { get; set; }
		public DateTime? mod_fecha { get; set; }



		[Data(nosql = true, tablaref = "comprobante", camporef = "com_fecha", foreign = "dcs_empresa, dcs_comprobante", keyref = "com_empresa, com_codigo", join = "inner")]
		public DateTime? dcs_comprobantecanfecha { get; set; }








		#endregion

		#region Constructors


		public Dcancelacionsocio()
		{
		}


		public Dcancelacionsocio(IDataReader reader)
		{
			this.dcs_empresa = (Int32)reader["dcs_empresa"];
			this.dcs_empresa_key = (Int32)reader["dcs_empresa"];
			this.dcs_codigo = (Int32)reader["dcs_codigo"];
			this.dcs_codigo_key = (Int32)reader["dcs_codigo"];
			this.dcs_comprobante = (Int64)reader["dcs_comprobante"];
			this.dcs_fecha= (reader["dcs_fecha"] != DBNull.Value) ? (DateTime?)reader["dcs_fecha"] : null;
			this.dcs_transacc = (Int32)reader["dcs_transacc"];
			this.dcs_doctran = reader["dcs_doctran"].ToString();
			this.dcs_pago = (Int32)reader["dcs_pago"];			
			this.dcs_socio = (reader["dcs_socio"] != DBNull.Value) ? (Int32?)reader["dcs_socio"] : null;
			this.dcs_monto = (reader["dcs_monto"] != DBNull.Value) ? (Decimal?)reader["dcs_monto"] : null;
			this.dcs_tipo	 = reader["dcs_tipo"].ToString();
			this.dcs_nrodoc = reader["dcs_nrodoc"].ToString();
			this.dcs_observacion = reader["dcs_observacion"].ToString();
			this.crea_usr = reader["crea_usr"].ToString();
			this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
			this.mod_usr = reader["mod_usr"].ToString();
			this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;


		}


		public Dcancelacionsocio(object objeto)
		{
			if (objeto != null)
			{
				Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
				object dcs_empresa = null;
				object dcs_codigo = null;
				object dcs_comprobante = null;
				object dcs_fecha = null;
				object dcs_transacc = null;
				object dcs_doctran = null;
				object dcs_pago = null;				
				object dcs_socio = null;
				object dcs_monto = null;
				object dcs_tipo = null;				
				object dcs_nrodoc = null;
				object dcs_observacion = null;
				object crea_usr = null;
				object crea_fecha = null;
				object mod_usr = null;
				object mod_fecha = null;


				tmp.TryGetValue("dcs_empresa", out dcs_empresa);
				tmp.TryGetValue("dcs_codigo", out dcs_codigo);
				tmp.TryGetValue("dcs_comprobante", out dcs_comprobante);
				tmp.TryGetValue("dcs_fecha", out dcs_fecha);
				tmp.TryGetValue("dcs_transacc", out dcs_transacc);
				tmp.TryGetValue("dcs_doctran", out dcs_doctran);
				tmp.TryGetValue("dcs_pago", out dcs_pago);
				
				tmp.TryGetValue("dcs_socio", out dcs_socio);
				tmp.TryGetValue("dcs_monto", out dcs_monto);
				tmp.TryGetValue("dcs_tipo", out dcs_tipo);
				tmp.TryGetValue("dcs_nrodoc", out dcs_nrodoc);
				tmp.TryGetValue("dcs_observacion", out dcs_observacion);
				tmp.TryGetValue("crea_usr", out crea_usr);
				tmp.TryGetValue("crea_fecha", out crea_fecha);
				tmp.TryGetValue("mod_usr", out mod_usr);
				tmp.TryGetValue("mod_fecha", out mod_fecha);


				this.dcs_empresa = (Int32)Conversiones.GetValueByType(dcs_empresa, typeof(Int32));
				this.dcs_codigo = (Int32)Conversiones.GetValueByType(dcs_codigo, typeof(Int32));
				this.dcs_comprobante = (Int64)Conversiones.GetValueByType(dcs_comprobante, typeof(Int64));
				this.dcs_fecha = (DateTime?)Conversiones.GetValueByType(dcs_fecha, typeof(DateTime?));
				this.dcs_transacc = (Int32)Conversiones.GetValueByType(dcs_transacc, typeof(Int32));
				this.dcs_doctran = (String)Conversiones.GetValueByType(dcs_doctran, typeof(String));
				this.dcs_pago = (Int32)Conversiones.GetValueByType(dcs_pago, typeof(Int32));
				
				this.dcs_socio = (Int32)Conversiones.GetValueByType(dcs_socio, typeof(Int32));
				this.dcs_monto = (Decimal?)Conversiones.GetValueByType(dcs_monto, typeof(Decimal?));
				this.dcs_tipo = (String)Conversiones.GetValueByType(dcs_tipo, typeof(String));
				this.dcs_nrodoc = (String)Conversiones.GetValueByType(dcs_nrodoc, typeof(String));
				this.dcs_observacion = (String)Conversiones.GetValueByType(dcs_observacion, typeof(String));
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
