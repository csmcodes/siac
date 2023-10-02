using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Dcancelacionguias
    {
        #region Properties

    	[Data(key = true)]
	public Int32 dcg_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 dcg_empresa_key { get; set; }
	[Data(key = true)]
	public Int64 dcg_comprobante { get; set; }
	[Data(originalkey = true)]
	public Int64 dcg_comprobante_key { get; set; }
	[Data(key = true)]
	public Int32 dcg_transsac { get; set; }
	[Data(originalkey = true)]
	public Int32 dcg_transsac_key { get; set; }
	[Data(key = true)]
	public String dcg_doctran { get; set; }
	[Data(originalkey = true)]
	public String dcg_doctran_key { get; set; }
	[Data(key = true)]
	public Int32 dcg_pago { get; set; }
	[Data(originalkey = true)]
	public Int32 dcg_pago_key { get; set; }
	[Data(key = true)]
	public Int64 dcg_comprobante_can { get; set; }
	[Data(originalkey = true)]
	public Int64 dcg_comprobante_can_key { get; set; }
	[Data(key = true)]
	public Int32 dcg_secuencia { get; set; }
	[Data(originalkey = true)]
	public Int32 dcg_secuencia_key { get; set; }
	[Data(key = true)]
	public Int64 dcg_comprobante_guia { get; set; }
	[Data(originalkey = true)]
    public Int64 dcg_comprobante_guia_key { get; set; }
    public Int64? dcg_planilla { get; set; }
	public Decimal? dcg_monto { get; set; }
	public Int32? dcg_estado { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Dcancelacionguias()
        {
        }

        public  Dcancelacionguias( Int32 dcg_empresa,Int64 dcg_comprobante,Int32 dcg_transsac,String dcg_doctran,Int32 dcg_pago,Int64 dcg_comprobante_can,Int32 dcg_secuencia,Int32 dcg_comprobante_guia,Decimal dcg_monto,Int32 dcg_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.dcg_empresa = dcg_empresa;
	this.dcg_comprobante = dcg_comprobante;
	this.dcg_transsac = dcg_transsac;
	this.dcg_doctran = dcg_doctran;
	this.dcg_pago = dcg_pago;
	this.dcg_comprobante_can = dcg_comprobante_can;
	this.dcg_secuencia = dcg_secuencia;
	this.dcg_comprobante_guia = dcg_comprobante_guia;
	this.dcg_monto = dcg_monto;
	this.dcg_estado = dcg_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Dcancelacionguias(IDataReader reader)
        {
    	this.dcg_empresa = (Int32)reader["dcg_empresa"];
	this.dcg_comprobante = (Int64)reader["dcg_comprobante"];
	this.dcg_transsac = (Int32)reader["dcg_transsac"];
	this.dcg_doctran = reader["dcg_doctran"].ToString();
	this.dcg_pago = (Int32)reader["dcg_pago"];
	this.dcg_comprobante_can = (Int64)reader["dcg_comprobante_can"];
	this.dcg_secuencia = (Int32)reader["dcg_secuencia"];
    this.dcg_comprobante_guia = (Int64)reader["dcg_comprobante_guia"];
    this.dcg_planilla = (Int64)reader["dcg_planilla"];
    this.dcg_empresa_key = (Int32)reader["dcg_empresa"];
    this.dcg_comprobante_key = (Int64)reader["dcg_comprobante"];
    this.dcg_transsac_key = (Int32)reader["dcg_transsac"];
    this.dcg_doctran_key = reader["dcg_doctran"].ToString();
    this.dcg_pago_key = (Int32)reader["dcg_pago"];
    this.dcg_comprobante_can_key = (Int64)reader["dcg_comprobante_can"];
    this.dcg_secuencia_key = (Int32)reader["dcg_secuencia"];
    this.dcg_comprobante_guia_key = (Int64)reader["dcg_comprobante_guia"];




	this.dcg_monto = (reader["dcg_monto"] != DBNull.Value) ? (Decimal?)reader["dcg_monto"] : null;
	this.dcg_estado = (reader["dcg_estado"] != DBNull.Value) ? (Int32?)reader["dcg_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Dcancelacionguias(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object dcg_empresa = null;
	object dcg_comprobante = null;
	object dcg_transsac = null;
	object dcg_doctran = null;
	object dcg_pago = null;
	object dcg_comprobante_can = null;
	object dcg_secuencia = null;
	object dcg_comprobante_guia = null;
    object dcg_planilla = null;
    object dcg_empresa_key = null;
    object dcg_comprobante_key = null;
    object dcg_transsac_key = null;
    object dcg_doctran_key = null;
    object dcg_pago_key = null;
    object dcg_comprobante_can_key = null;
    object dcg_secuencia_key = null;
    object dcg_comprobante_guia_key = null;




	object dcg_monto = null;
	object dcg_estado = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("dcg_empresa", out dcg_empresa);
	tmp.TryGetValue("dcg_comprobante", out dcg_comprobante);
	tmp.TryGetValue("dcg_transsac", out dcg_transsac);
	tmp.TryGetValue("dcg_doctran", out dcg_doctran);
	tmp.TryGetValue("dcg_pago", out dcg_pago);
	tmp.TryGetValue("dcg_comprobante_can", out dcg_comprobante_can);
	tmp.TryGetValue("dcg_secuencia", out dcg_secuencia);
	tmp.TryGetValue("dcg_comprobante_guia", out dcg_comprobante_guia);
    tmp.TryGetValue("dcg_planilla", out dcg_planilla);

    tmp.TryGetValue("dcg_empresa_key", out dcg_empresa_key);
    tmp.TryGetValue("dcg_comprobante_key", out dcg_comprobante_key);
    tmp.TryGetValue("dcg_transsac_key", out dcg_transsac_key);
    tmp.TryGetValue("dcg_doctran_key", out dcg_doctran_key);
    tmp.TryGetValue("dcg_pago_key", out dcg_pago_key);
    tmp.TryGetValue("dcg_comprobante_can_key", out dcg_comprobante_can_key);
    tmp.TryGetValue("dcg_secuencia_key", out dcg_secuencia_key);
    tmp.TryGetValue("dcg_comprobante_guia_key", out dcg_comprobante_guia_key);



	tmp.TryGetValue("dcg_monto", out dcg_monto);
	tmp.TryGetValue("dcg_estado", out dcg_estado);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.dcg_empresa = (Int32)Conversiones.GetValueByType(dcg_empresa, typeof(Int32));
	this.dcg_comprobante = (Int64)Conversiones.GetValueByType(dcg_comprobante, typeof(Int64));
	this.dcg_transsac = (Int32)Conversiones.GetValueByType(dcg_transsac, typeof(Int32));
	this.dcg_doctran = (String)Conversiones.GetValueByType(dcg_doctran, typeof(String));
	this.dcg_pago = (Int32)Conversiones.GetValueByType(dcg_pago, typeof(Int32));
	this.dcg_comprobante_can = (Int64)Conversiones.GetValueByType(dcg_comprobante_can, typeof(Int64));
	this.dcg_secuencia = (Int32)Conversiones.GetValueByType(dcg_secuencia, typeof(Int32));
    this.dcg_comprobante_guia = (Int64)Conversiones.GetValueByType(dcg_comprobante_guia, typeof(Int64));
    this.dcg_planilla = (Int64?)Conversiones.GetValueByType(dcg_estado, typeof(Int64?));
    this.dcg_empresa_key = (Int32)Conversiones.GetValueByType(dcg_empresa_key, typeof(Int32));
    this.dcg_comprobante_key = (Int64)Conversiones.GetValueByType(dcg_comprobante_key, typeof(Int64));
    this.dcg_transsac_key = (Int32)Conversiones.GetValueByType(dcg_transsac_key, typeof(Int32));
    this.dcg_doctran_key = (String)Conversiones.GetValueByType(dcg_doctran_key, typeof(String));
    this.dcg_pago_key = (Int32)Conversiones.GetValueByType(dcg_pago_key, typeof(Int32));
    this.dcg_comprobante_can_key = (Int64)Conversiones.GetValueByType(dcg_comprobante_can_key, typeof(Int64));
    this.dcg_secuencia_key = (Int32)Conversiones.GetValueByType(dcg_secuencia_key, typeof(Int32));
    this.dcg_comprobante_guia_key = (Int64)Conversiones.GetValueByType(dcg_comprobante_guia_key, typeof(Int64));




	this.dcg_monto = (Decimal?)Conversiones.GetValueByType(dcg_monto, typeof(Decimal?));
	this.dcg_estado = (Int32?)Conversiones.GetValueByType(dcg_estado, typeof(Int32?));
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
