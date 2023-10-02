using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Dcancelacion
    {
        #region Properties

    	[Data(key = true)]
	public Int32 dca_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 dca_empresa_key { get; set; }
	[Data(key = true)]
	public Int64 dca_comprobante { get; set; }
	[Data(originalkey = true)]
	public Int64 dca_comprobante_key { get; set; }
	[Data(key = true)]
	public Int32 dca_transacc { get; set; }
	[Data(originalkey = true)]
	public Int32 dca_transacc_key { get; set; }
	[Data(key = true)]
	public String dca_doctran { get; set; }
	[Data(originalkey = true)]
	public String dca_doctran_key { get; set; }
	[Data(key = true)]
	public Int32 dca_pago { get; set; }
	[Data(originalkey = true)]
	public Int32 dca_pago_key { get; set; }
	[Data(key = true)]
	public Int64 dca_comprobante_can { get; set; }
	[Data(originalkey = true)]
	public Int64 dca_comprobante_can_key { get; set; }
	[Data(key = true)]
	public Int32 dca_secuencia { get; set; }
	[Data(originalkey = true)]
	public Int32 dca_secuencia_key { get; set; }
	public Int32? dca_debcre { get; set; }
	public Int32? dca_transacc_can { get; set; }
	public Int32? dca_tipo_cambio { get; set; }
	public Decimal? dca_monto { get; set; }
	public Decimal? dca_monto_ext { get; set; }
    public Decimal? dca_monto_pla { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

    public Int64? dca_planilla { get; set; }



        

        [Data(nosql = true, tablaref = "comprobante", camporef = "com_fecha", foreign = "dca_empresa, dca_comprobante_can", keyref = "com_empresa, com_codigo", join = "inner")]
        public DateTime? dca_comprobantecanfecha { get; set; }
        [Data(nosql = true, tablaref = "comprobante", camporef = "com_doctran", foreign = "dca_empresa, dca_comprobante_can", keyref = "com_empresa, com_codigo", join = "inner")]
        public string dca_compcandoctran { get; set; }


		[Data(nosql = true, tablaref = "ddocumento", camporef = "ddo_doctran", foreign = "dca_empresa, dca_comprobante, dca_transacc, dca_doctran, dca_pago", keyref = "ddo_empresa, ddo_comprobante, ddo_transacc, ddo_doctran, ddo_pago", join = "left")]
		public string dca_ddo_doctran { get; set; }




		#endregion

		#region Constructors


		public  Dcancelacion()
        {
        }

        public  Dcancelacion( Int32 dca_empresa,Int64 dca_comprobante,Int32 dca_transacc,String dca_doctran,Int32 dca_pago,Int64 dca_comprobante_can,Int32 dca_secuencia,Int32 dca_debcre,Int32 dca_transacc_can,Int32 dca_tipo_cambio,Decimal dca_monto,Decimal dca_monto_ext, Decimal dca_monto_pla,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.dca_empresa = dca_empresa;
	this.dca_comprobante = dca_comprobante;
	this.dca_transacc = dca_transacc;
	this.dca_doctran = dca_doctran;
	this.dca_pago = dca_pago;
	this.dca_comprobante_can = dca_comprobante_can;
	this.dca_secuencia = dca_secuencia;
	this.dca_debcre = dca_debcre;
	this.dca_transacc_can = dca_transacc_can;
	this.dca_tipo_cambio = dca_tipo_cambio;
	this.dca_monto = dca_monto;
	this.dca_monto_ext = dca_monto_ext;
    this.dca_monto_pla = dca_monto_pla;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public Dcancelacion(IDataReader reader)
        {
            this.dca_empresa = (Int32)reader["dca_empresa"];
            this.dca_comprobante = (Int64)reader["dca_comprobante"];
            this.dca_transacc = (Int32)reader["dca_transacc"];
            this.dca_doctran = reader["dca_doctran"].ToString();
            this.dca_pago = (Int32)reader["dca_pago"];
            this.dca_comprobante_can = (Int64)reader["dca_comprobante_can"];
            this.dca_secuencia = (Int32)reader["dca_secuencia"];

            this.dca_empresa_key = (Int32)reader["dca_empresa"];
            this.dca_comprobante_key = (Int64)reader["dca_comprobante"];
            this.dca_transacc_key = (Int32)reader["dca_transacc"];
            this.dca_doctran_key = reader["dca_doctran"].ToString();
            this.dca_pago_key = (Int32)reader["dca_pago"];
            this.dca_comprobante_can_key = (Int64)reader["dca_comprobante_can"];
            this.dca_secuencia_key = (Int32)reader["dca_secuencia"];



            this.dca_debcre = (reader["dca_debcre"] != DBNull.Value) ? (Int32?)reader["dca_debcre"] : null;
            this.dca_transacc_can = (reader["dca_transacc_can"] != DBNull.Value) ? (Int32?)reader["dca_transacc_can"] : null;
            this.dca_tipo_cambio = (reader["dca_tipo_cambio"] != DBNull.Value) ? (Int32?)reader["dca_tipo_cambio"] : null;
            this.dca_monto = (reader["dca_monto"] != DBNull.Value) ? (Decimal?)reader["dca_monto"] : null;
            this.dca_monto_ext = (reader["dca_monto_ext"] != DBNull.Value) ? (Decimal?)reader["dca_monto_ext"] : null;
            this.dca_monto_pla = (reader["dca_monto_pla"] != DBNull.Value) ? (Decimal?)reader["dca_monto_pla"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.dca_planilla = (reader["dca_planilla"] != DBNull.Value) ? (Int64?)reader["dca_planilla"] : null;

            this.dca_comprobantecanfecha = (reader["dca_comprobantecanfecha"] != DBNull.Value) ? (DateTime?)reader["dca_comprobantecanfecha"] : null;
            this.dca_compcandoctran = (reader["dca_compcandoctran"] != DBNull.Value) ? (String)reader["dca_compcandoctran"] : null;

			this.dca_ddo_doctran = (reader["dca_ddo_doctran"] != DBNull.Value) ? (String)reader["dca_ddo_doctran"] : null;


		}


        public Dcancelacion(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object dca_empresa = null;
	object dca_comprobante = null;
	object dca_transacc = null;
	object dca_doctran = null;
	object dca_pago = null;
	object dca_comprobante_can = null;
	object dca_secuencia = null;
	object dca_debcre = null;
	object dca_transacc_can = null;
	object dca_tipo_cambio = null;
	object dca_monto = null;
	object dca_monto_ext = null;
    object dca_monto_pla = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("dca_empresa", out dca_empresa);
	tmp.TryGetValue("dca_comprobante", out dca_comprobante);
	tmp.TryGetValue("dca_transacc", out dca_transacc);
	tmp.TryGetValue("dca_doctran", out dca_doctran);
	tmp.TryGetValue("dca_pago", out dca_pago);
	tmp.TryGetValue("dca_comprobante_can", out dca_comprobante_can);
	tmp.TryGetValue("dca_secuencia", out dca_secuencia);
	tmp.TryGetValue("dca_debcre", out dca_debcre);
	tmp.TryGetValue("dca_transacc_can", out dca_transacc_can);
	tmp.TryGetValue("dca_tipo_cambio", out dca_tipo_cambio);
	tmp.TryGetValue("dca_monto", out dca_monto);
	tmp.TryGetValue("dca_monto_ext", out dca_monto_ext);
    tmp.TryGetValue("dca_monto_pla", out dca_monto_pla);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.dca_empresa = (Int32)Conversiones.GetValueByType(dca_empresa, typeof(Int32));
	this.dca_comprobante = (Int64)Conversiones.GetValueByType(dca_comprobante, typeof(Int64));
	this.dca_transacc = (Int32)Conversiones.GetValueByType(dca_transacc, typeof(Int32));
	this.dca_doctran = (String)Conversiones.GetValueByType(dca_doctran, typeof(String));
	this.dca_pago = (Int32)Conversiones.GetValueByType(dca_pago, typeof(Int32));
	this.dca_comprobante_can = (Int64)Conversiones.GetValueByType(dca_comprobante_can, typeof(Int64));
	this.dca_secuencia = (Int32)Conversiones.GetValueByType(dca_secuencia, typeof(Int32));
	this.dca_debcre = (Int32?)Conversiones.GetValueByType(dca_debcre, typeof(Int32?));
	this.dca_transacc_can = (Int32?)Conversiones.GetValueByType(dca_transacc_can, typeof(Int32?));
	this.dca_tipo_cambio = (Int32?)Conversiones.GetValueByType(dca_tipo_cambio, typeof(Int32?));
	this.dca_monto = (Decimal?)Conversiones.GetValueByType(dca_monto, typeof(Decimal?));
	this.dca_monto_ext = (Decimal?)Conversiones.GetValueByType(dca_monto_ext, typeof(Decimal?));
    this.dca_monto_pla= (Decimal?)Conversiones.GetValueByType(dca_monto_pla, typeof(Decimal?));
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
