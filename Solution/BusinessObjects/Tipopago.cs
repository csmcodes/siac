using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Tipopago
    {
        #region Properties

    	public Int32 tpa_transacc { get; set; }
	public Int32 tpa_contabiliza { get; set; }
	[Data(key = true)]
	public Int32 tpa_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 tpa_empresa_key { get; set; }
    [Data(key = true, auto = true)]
	public Int32 tpa_codigo { get; set; }
	[Data(originalkey = true)]
	public Int32 tpa_codigo_key { get; set; }
	public Int32 tpa_tclipro { get; set; }
	public String tpa_id { get; set; }
	public String tpa_nombre { get; set; }
	public Int32? tpa_cuenta { get; set; }
	public Int32? tpa_codclipro { get; set; }
	public Int32? tpa_detalle { get; set; }
        public Int32? tpa_iva{ get; set; }
        public Int32? tpa_ret { get; set; }
		public Decimal? tpa_porcentaje { get; set; }
		public String tpa_codigoxml { get; set; }
		public String tpa_aplicaret { get; set; }
		public Int32? tpa_estado { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

    [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "tpa_empresa, tpa_cuenta", keyref = "cue_empresa, cue_codigo", join = "left")]
    public string tpa_nombrecuenta { get; set; }



        #endregion

        #region Constructors


        public  Tipopago()
        {
        }

        public  Tipopago( Int32 tpa_transacc,Int32 tpa_contabiliza,Int32 tpa_empresa,Int32 tpa_codigo,Int32 tpa_tclipro,String tpa_id,String tpa_nombre,Int32 tpa_cuenta,Int32 tpa_codclipro,Int32 tpa_detalle,Int32 tpa_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.tpa_transacc = tpa_transacc;
	this.tpa_contabiliza = tpa_contabiliza;
	this.tpa_empresa = tpa_empresa;
	this.tpa_codigo = tpa_codigo;
    this.tpa_empresa_key = tpa_empresa;
    this.tpa_codigo_key = tpa_codigo;
	this.tpa_tclipro = tpa_tclipro;
	this.tpa_id = tpa_id;
	this.tpa_nombre = tpa_nombre;
	this.tpa_cuenta = tpa_cuenta;
	this.tpa_codclipro = tpa_codclipro;
	this.tpa_detalle = tpa_detalle;
	this.tpa_estado = tpa_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Tipopago(IDataReader reader)
        {
    	this.tpa_transacc = (Int32)reader["tpa_transacc"];
	this.tpa_contabiliza = (Int32)reader["tpa_contabiliza"];
	this.tpa_empresa = (Int32)reader["tpa_empresa"];
	this.tpa_codigo = (Int32)reader["tpa_codigo"];
    this.tpa_empresa_key = (Int32)reader["tpa_empresa"];
    this.tpa_codigo_key = (Int32)reader["tpa_codigo"];
	this.tpa_tclipro = (Int32)reader["tpa_tclipro"];
	this.tpa_id = reader["tpa_id"].ToString();
	this.tpa_nombre = reader["tpa_nombre"].ToString();
	this.tpa_cuenta = (reader["tpa_cuenta"] != DBNull.Value) ? (Int32?)reader["tpa_cuenta"] : null;
	this.tpa_codclipro = (reader["tpa_codclipro"] != DBNull.Value) ? (Int32?)reader["tpa_codclipro"] : null;
	this.tpa_detalle = (reader["tpa_detalle"] != DBNull.Value) ? (Int32?)reader["tpa_detalle"] : null;
            this.tpa_iva= (reader["tpa_iva"] != DBNull.Value) ? (Int32?)reader["tpa_iva"] : null;
            this.tpa_ret= (reader["tpa_ret"] != DBNull.Value) ? (Int32?)reader["tpa_ret"] : null;
			this.tpa_porcentaje= (reader["tpa_porcentaje"] != DBNull.Value) ? (Decimal?)reader["tpa_porcentaje"] : null;
			this.tpa_codigoxml = reader["tpa_codigoxml"].ToString();
			this.tpa_aplicaret = reader["tpa_aplicaret"].ToString();
			this.tpa_estado = (reader["tpa_estado"] != DBNull.Value) ? (Int32?)reader["tpa_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            
                this.tpa_nombrecuenta = reader["tpa_nombrecuenta"].ToString();
        }


        public Tipopago(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object tpa_transacc = null;
	object tpa_contabiliza = null;
	object tpa_empresa = null;
	object tpa_codigo = null;
    object tpa_empresa_key = null;
    object tpa_codigo_key = null;
	object tpa_tclipro = null;
	object tpa_id = null;
	object tpa_nombre = null;
	object tpa_cuenta = null;
	object tpa_codclipro = null;
	object tpa_detalle = null;
                object tpa_iva = null;
                object tpa_ret = null;
				object tpa_porcentaje = null;
				object tpa_codigoxml = null;
				object tpa_aplicaret = null;
				object tpa_estado = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("tpa_transacc", out tpa_transacc);
	tmp.TryGetValue("tpa_contabiliza", out tpa_contabiliza);
	tmp.TryGetValue("tpa_empresa", out tpa_empresa);
	tmp.TryGetValue("tpa_codigo", out tpa_codigo);
    tmp.TryGetValue("tpa_empresa_key", out tpa_empresa_key);
    tmp.TryGetValue("tpa_codigo_key", out tpa_codigo_key);
	tmp.TryGetValue("tpa_tclipro", out tpa_tclipro);
	tmp.TryGetValue("tpa_id", out tpa_id);
	tmp.TryGetValue("tpa_nombre", out tpa_nombre);
	tmp.TryGetValue("tpa_cuenta", out tpa_cuenta);
	tmp.TryGetValue("tpa_codclipro", out tpa_codclipro);
	tmp.TryGetValue("tpa_detalle", out tpa_detalle);
                tmp.TryGetValue("tpa_iva", out tpa_iva);
				tmp.TryGetValue("tpa_ret", out tpa_ret);
				tmp.TryGetValue("tpa_porcentaje", out tpa_porcentaje);
				tmp.TryGetValue("tpa_codigoxml", out tpa_codigoxml);
				tmp.TryGetValue("tpa_aplicaret", out tpa_aplicaret);
				tmp.TryGetValue("tpa_estado", out tpa_estado);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.tpa_transacc = (Int32)Conversiones.GetValueByType(tpa_transacc, typeof(Int32));
	this.tpa_contabiliza = (Int32)Conversiones.GetValueByType(tpa_contabiliza, typeof(Int32));
	this.tpa_empresa = (Int32)Conversiones.GetValueByType(tpa_empresa, typeof(Int32));
	this.tpa_codigo = (Int32)Conversiones.GetValueByType(tpa_codigo, typeof(Int32));
    this.tpa_empresa_key = (Int32)Conversiones.GetValueByType(tpa_empresa_key, typeof(Int32));
    this.tpa_codigo_key = (Int32)Conversiones.GetValueByType(tpa_codigo_key, typeof(Int32));
	this.tpa_tclipro = (Int32)Conversiones.GetValueByType(tpa_tclipro, typeof(Int32));
	this.tpa_id = (String)Conversiones.GetValueByType(tpa_id, typeof(String));
	this.tpa_nombre = (String)Conversiones.GetValueByType(tpa_nombre, typeof(String));
	this.tpa_cuenta = (Int32?)Conversiones.GetValueByType(tpa_cuenta, typeof(Int32?));
	this.tpa_codclipro = (Int32?)Conversiones.GetValueByType(tpa_codclipro, typeof(Int32?));
	this.tpa_detalle = (Int32?)Conversiones.GetValueByType(tpa_detalle, typeof(Int32?));
                this.tpa_iva= (Int32?)Conversiones.GetValueByType(tpa_iva, typeof(Int32?));
                this.tpa_ret= (Int32?)Conversiones.GetValueByType(tpa_ret, typeof(Int32?));
				this.tpa_porcentaje= (Decimal?)Conversiones.GetValueByType(tpa_porcentaje, typeof(Int32?));
				this.tpa_codigoxml = (String)Conversiones.GetValueByType(tpa_codigoxml, typeof(String));
				this.tpa_aplicaret = (String)Conversiones.GetValueByType(tpa_aplicaret, typeof(String));
				this.tpa_estado = (Int32?)Conversiones.GetValueByType(tpa_estado, typeof(Int32?));
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
