using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Cmovinv
    {
        #region Properties

    	[Data(key = true)]
	public Int32 cmo_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 cmo_empresa_key { get; set; }
	[Data(key = true)]
	public Int64 cmo_cco_comproba { get; set; }
	[Data(originalkey = true)]
	public Int64 cmo_cco_comproba_key { get; set; }
	public Int32? cmo_transacc { get; set; }
	public Int64? cmo_cco_factura { get; set; }
	public Int64? cmo_cco_pedido { get; set; }
	public Int64? cmo_cco_referencia { get; set; }
	public Int32? cmo_bodini { get; set; }
	public Int32? cmo_bodfin { get; set; }
	public Int32? cmo_est_entrega { get; set; }
	public Int32? cmo_liquida { get; set; }
	public String cmo_referencia { get; set; }
	public Int32? cmo_control_temp { get; set; }
	public Int32? cmo_usr_liquida { get; set; }
	public Int32? cmo_empleado { get; set; }
	public Int64? cmo_cco_importacion { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }
	public String crea_ip { get; set; }
	public String mod_ip { get; set; }

              
        #endregion

        #region Constructors


        public  Cmovinv()
        {
        }

        public  Cmovinv( Int32 cmo_empresa,Int64 cmo_cco_comproba,Int32 cmo_transacc,Int64 cmo_cco_factura,Int64 cmo_cco_pedido,Int64 cmo_cco_referencia,Int32 cmo_bodini,Int32 cmo_bodfin,Int32 cmo_est_entrega,Int32 cmo_liquida,String cmo_referencia,Int32 cmo_control_temp,Int32 cmo_usr_liquida,Int32 cmo_empleado,Int64 cmo_cco_importacion,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha,String crea_ip,String mod_ip)
        {                
    	this.cmo_empresa = cmo_empresa;
	this.cmo_cco_comproba = cmo_cco_comproba;
	this.cmo_transacc = cmo_transacc;
	this.cmo_cco_factura = cmo_cco_factura;
	this.cmo_cco_pedido = cmo_cco_pedido;
	this.cmo_cco_referencia = cmo_cco_referencia;
	this.cmo_bodini = cmo_bodini;
	this.cmo_bodfin = cmo_bodfin;
	this.cmo_est_entrega = cmo_est_entrega;
	this.cmo_liquida = cmo_liquida;
	this.cmo_referencia = cmo_referencia;
	this.cmo_control_temp = cmo_control_temp;
	this.cmo_usr_liquida = cmo_usr_liquida;
	this.cmo_empleado = cmo_empleado;
	this.cmo_cco_importacion = cmo_cco_importacion;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;
	this.crea_ip = crea_ip;
	this.mod_ip = mod_ip;

           
       }

        public  Cmovinv(IDataReader reader)
        {
    	this.cmo_empresa = (Int32)reader["cmo_empresa"];
	this.cmo_cco_comproba = (Int64)reader["cmo_cco_comproba"];
	this.cmo_transacc = (reader["cmo_transacc"] != DBNull.Value) ? (Int32?)reader["cmo_transacc"] : null;
	this.cmo_cco_factura = (reader["cmo_cco_factura"] != DBNull.Value) ? (Int64?)reader["cmo_cco_factura"] : null;
	this.cmo_cco_pedido = (reader["cmo_cco_pedido"] != DBNull.Value) ? (Int64?)reader["cmo_cco_pedido"] : null;
	this.cmo_cco_referencia = (reader["cmo_cco_referencia"] != DBNull.Value) ? (Int64?)reader["cmo_cco_referencia"] : null;
	this.cmo_bodini = (reader["cmo_bodini"] != DBNull.Value) ? (Int32?)reader["cmo_bodini"] : null;
	this.cmo_bodfin = (reader["cmo_bodfin"] != DBNull.Value) ? (Int32?)reader["cmo_bodfin"] : null;
	this.cmo_est_entrega = (reader["cmo_est_entrega"] != DBNull.Value) ? (Int32?)reader["cmo_est_entrega"] : null;
	this.cmo_liquida = (reader["cmo_liquida"] != DBNull.Value) ? (Int32?)reader["cmo_liquida"] : null;
	this.cmo_referencia = reader["cmo_referencia"].ToString();
	this.cmo_control_temp = (reader["cmo_control_temp"] != DBNull.Value) ? (Int32?)reader["cmo_control_temp"] : null;
	this.cmo_usr_liquida = (reader["cmo_usr_liquida"] != DBNull.Value) ? (Int32?)reader["cmo_usr_liquida"] : null;
	this.cmo_empleado = (reader["cmo_empleado"] != DBNull.Value) ? (Int32?)reader["cmo_empleado"] : null;
	this.cmo_cco_importacion = (reader["cmo_cco_importacion"] != DBNull.Value) ? (Int64?)reader["cmo_cco_importacion"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
	this.crea_ip = reader["crea_ip"].ToString();
	this.mod_ip = reader["mod_ip"].ToString();

        }


        public Cmovinv(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object cmo_empresa = null;
	object cmo_cco_comproba = null;
	object cmo_transacc = null;
	object cmo_cco_factura = null;
	object cmo_cco_pedido = null;
	object cmo_cco_referencia = null;
	object cmo_bodini = null;
	object cmo_bodfin = null;
	object cmo_est_entrega = null;
	object cmo_liquida = null;
	object cmo_referencia = null;
	object cmo_control_temp = null;
	object cmo_usr_liquida = null;
	object cmo_empleado = null;
	object cmo_cco_importacion = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;
	object crea_ip = null;
	object mod_ip = null;


                	tmp.TryGetValue("cmo_empresa", out cmo_empresa);
	tmp.TryGetValue("cmo_cco_comproba", out cmo_cco_comproba);
	tmp.TryGetValue("cmo_transacc", out cmo_transacc);
	tmp.TryGetValue("cmo_cco_factura", out cmo_cco_factura);
	tmp.TryGetValue("cmo_cco_pedido", out cmo_cco_pedido);
	tmp.TryGetValue("cmo_cco_referencia", out cmo_cco_referencia);
	tmp.TryGetValue("cmo_bodini", out cmo_bodini);
	tmp.TryGetValue("cmo_bodfin", out cmo_bodfin);
	tmp.TryGetValue("cmo_est_entrega", out cmo_est_entrega);
	tmp.TryGetValue("cmo_liquida", out cmo_liquida);
	tmp.TryGetValue("cmo_referencia", out cmo_referencia);
	tmp.TryGetValue("cmo_control_temp", out cmo_control_temp);
	tmp.TryGetValue("cmo_usr_liquida", out cmo_usr_liquida);
	tmp.TryGetValue("cmo_empleado", out cmo_empleado);
	tmp.TryGetValue("cmo_cco_importacion", out cmo_cco_importacion);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);
	tmp.TryGetValue("crea_ip", out crea_ip);
	tmp.TryGetValue("mod_ip", out mod_ip);


                	this.cmo_empresa = (Int32)Conversiones.GetValueByType(cmo_empresa, typeof(Int32));
	this.cmo_cco_comproba = (Int64)Conversiones.GetValueByType(cmo_cco_comproba, typeof(Int64));
	this.cmo_transacc = (Int32?)Conversiones.GetValueByType(cmo_transacc, typeof(Int32?));
	this.cmo_cco_factura = (Int64?)Conversiones.GetValueByType(cmo_cco_factura, typeof(Int64?));
	this.cmo_cco_pedido = (Int64?)Conversiones.GetValueByType(cmo_cco_pedido, typeof(Int64?));
	this.cmo_cco_referencia = (Int64?)Conversiones.GetValueByType(cmo_cco_referencia, typeof(Int64?));
	this.cmo_bodini = (Int32?)Conversiones.GetValueByType(cmo_bodini, typeof(Int32?));
	this.cmo_bodfin = (Int32?)Conversiones.GetValueByType(cmo_bodfin, typeof(Int32?));
	this.cmo_est_entrega = (Int32?)Conversiones.GetValueByType(cmo_est_entrega, typeof(Int32?));
	this.cmo_liquida = (Int32?)Conversiones.GetValueByType(cmo_liquida, typeof(Int32?));
	this.cmo_referencia = (String)Conversiones.GetValueByType(cmo_referencia, typeof(String));
	this.cmo_control_temp = (Int32?)Conversiones.GetValueByType(cmo_control_temp, typeof(Int32?));
	this.cmo_usr_liquida = (Int32?)Conversiones.GetValueByType(cmo_usr_liquida, typeof(Int32?));
	this.cmo_empleado = (Int32?)Conversiones.GetValueByType(cmo_empleado, typeof(Int32?));
	this.cmo_cco_importacion = (Int64?)Conversiones.GetValueByType(cmo_cco_importacion, typeof(Int64?));
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
