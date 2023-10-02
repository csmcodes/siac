using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Ccomdoc
    {
        #region Properties

        [Data(key = true)]
        public Int32 cdoc_empresa { get; set; }
        [Data(originalkey = true)]
        public Int32 cdoc_empresa_key { get; set; }
        [Data(key = true)]
        public Int64 cdoc_comprobante { get; set; }
        [Data(originalkey = true)]
        public Int64 cdoc_comprobante_key { get; set; }
        public Int64? cdoc_pedido { get; set; }
        public Int64? cdoc_sol_comprobante { get; set; }
        public Int64? cdoc_cobranza { get; set; }
        public Int64? cdoc_factura { get; set; }
        public DateTime? cdoc_vigencia { get; set; }
        public Int32? cdoc_autoriza { get; set; }
        public Int32? cdoc_politica { get; set; }
        public Int32? cdoc_listaprecio { get; set; }
        public Int32? cdoc_est_entrega { get; set; }
        public String cdoc_nombre { get; set; }
        public String cdoc_direccion { get; set; }
        public String cdoc_telefono { get; set; }
        public String cdoc_ced_ruc { get; set; }
        public Int32? cdoc_pais { get; set; }
        public Int32? cdoc_provincia { get; set; }
        public Int32? cdoc_canton { get; set; }
        public Int32? cdoc_prioridad { get; set; }
        public Int32? cdoc_empleado { get; set; }
        public Int32? cdoc_departamento { get; set; }
        public String cdoc_observeaciones { get; set; }
        public Int32? cdoc_impuesto { get; set; }
        public Decimal? cdoc_porc_impuesto { get; set; }
        public String cdoc_acl_nroautoriza { get; set; }
        public Int32? cdoc_acl_retdato { get; set; }
        public Int32? cdoc_acl_tablacoa { get; set; }
        public Int32? cdoc_control_temp { get; set; }
        public Int32? cdoc_usr_liquida { get; set; }
        public String cdoc_formapago { get; set; }
        public Int32? cdoc_pagoexterior { get; set; }
        public Int32? cdoc_paispago { get; set; }
        [Data(noupdate = true)]
        public String crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
         public String  cdoc_aut_factura{ get; set; }
        public DateTime? cdoc_aut_fecha{ get; set; }
        public Int32? cdoc_transacc { get; set; }


        [Data(nosql = true, tablaref = "politica", camporef = "pol_nombre", foreign = "cdoc_empresa, cdoc_politica", keyref = "pol_empresa, pol_codigo", join = "left")]
        public string cdoc_politicanombre{ get; set; }

        [Data(noprop = true)]
        public List<Dcomdoc> detalle { get; set; }


        [Data(noprop = true)]
        public string cdoc_tipoid { get; set; }



        #endregion

        #region Constructors


        public Ccomdoc()
        {
        }

        public Ccomdoc(Int32 cdoc_empresa, Int64 cdoc_comprobante, Int64 cdoc_pedido, Int64 cdoc_sol_comprobante, Int64 cdoc_cobranza, Int64 cdoc_factura, DateTime cdoc_vigencia, Int32 cdoc_autoriza, Int32 cdoc_politica, Int32 cdoc_listaprecio, Int32 cdoc_est_entrega, String cdoc_nombre, String cdoc_direccion, String cdoc_telefono, String cdoc_ced_ruc, Int32 cdoc_pais, Int32 cdoc_provincia, Int32 cdoc_canton, Int32 cdoc_prioridad, Int32 cdoc_empleado, Int32 cdoc_departamento, String cdoc_observeaciones, Int32 cdoc_impuesto, Decimal cdoc_porc_impuesto, String cdoc_acl_nroautoriza, Int32 cdoc_acl_retdato, Int32 cdoc_acl_tablacoa, Int32 cdoc_control_temp, Int32 cdoc_usr_liquida, String cdoc_formapago, Int32 cdoc_pagoexterior, Int32 cdoc_paispago, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha)
        {
            this.cdoc_empresa = cdoc_empresa;
            this.cdoc_comprobante = cdoc_comprobante;
            this.cdoc_empresa_key = cdoc_empresa;
            this.cdoc_comprobante_key = cdoc_comprobante;
            this.cdoc_pedido = cdoc_pedido;
            this.cdoc_sol_comprobante = cdoc_sol_comprobante;
            this.cdoc_cobranza = cdoc_cobranza;
            this.cdoc_factura = cdoc_factura;
            this.cdoc_vigencia = cdoc_vigencia;
            this.cdoc_autoriza = cdoc_autoriza;
            this.cdoc_politica = cdoc_politica;
            this.cdoc_listaprecio = cdoc_listaprecio;
            this.cdoc_est_entrega = cdoc_est_entrega;
            this.cdoc_nombre = cdoc_nombre;
            this.cdoc_direccion = cdoc_direccion;
            this.cdoc_telefono = cdoc_telefono;
            this.cdoc_ced_ruc = cdoc_ced_ruc;
            this.cdoc_pais = cdoc_pais;
            this.cdoc_provincia = cdoc_provincia;
            this.cdoc_canton = cdoc_canton;
            this.cdoc_prioridad = cdoc_prioridad;
            this.cdoc_empleado = cdoc_empleado;
            this.cdoc_departamento = cdoc_departamento;
            this.cdoc_observeaciones = cdoc_observeaciones;
            this.cdoc_impuesto = cdoc_impuesto;
            this.cdoc_porc_impuesto = cdoc_porc_impuesto;
            this.cdoc_acl_nroautoriza = cdoc_acl_nroautoriza;
            this.cdoc_acl_retdato = cdoc_acl_retdato;
            this.cdoc_acl_tablacoa = cdoc_acl_tablacoa;
            this.cdoc_control_temp = cdoc_control_temp;
            this.cdoc_usr_liquida = cdoc_usr_liquida;
            this.cdoc_formapago = cdoc_formapago;
            this.cdoc_pagoexterior = cdoc_pagoexterior;
            this.cdoc_paispago = cdoc_paispago;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;

            this.cdoc_aut_factura = cdoc_aut_factura;
            this.cdoc_aut_fecha = cdoc_aut_fecha;
            this.cdoc_transacc = cdoc_transacc;
        }

        public Ccomdoc(IDataReader reader)
        {
            this.cdoc_empresa = (Int32)reader["cdoc_empresa"];
            this.cdoc_comprobante = (Int64)reader["cdoc_comprobante"];
            this.cdoc_empresa_key = (Int32)reader["cdoc_empresa"];
            this.cdoc_comprobante_key = (Int64)reader["cdoc_comprobante"];
            this.cdoc_pedido = (reader["cdoc_pedido"] != DBNull.Value) ? (Int64?)reader["cdoc_pedido"] : null;
            this.cdoc_sol_comprobante = (reader["cdoc_sol_comprobante"] != DBNull.Value) ? (Int64?)reader["cdoc_sol_comprobante"] : null;
            this.cdoc_cobranza = (reader["cdoc_cobranza"] != DBNull.Value) ? (Int64?)reader["cdoc_cobranza"] : null;
            this.cdoc_factura = (reader["cdoc_factura"] != DBNull.Value) ? (Int64?)reader["cdoc_factura"] : null;
            this.cdoc_vigencia = (reader["cdoc_vigencia"] != DBNull.Value) ? (DateTime?)reader["cdoc_vigencia"] : null;
            this.cdoc_autoriza = (reader["cdoc_autoriza"] != DBNull.Value) ? (Int32?)reader["cdoc_autoriza"] : null;
            this.cdoc_politica = (reader["cdoc_politica"] != DBNull.Value) ? (Int32?)reader["cdoc_politica"] : null;
            this.cdoc_listaprecio = (reader["cdoc_listaprecio"] != DBNull.Value) ? (Int32?)reader["cdoc_listaprecio"] : null;
            this.cdoc_est_entrega = (reader["cdoc_est_entrega"] != DBNull.Value) ? (Int32?)reader["cdoc_est_entrega"] : null;
            this.cdoc_nombre = reader["cdoc_nombre"].ToString();
            this.cdoc_direccion = reader["cdoc_direccion"].ToString();
            this.cdoc_telefono = reader["cdoc_telefono"].ToString();
            this.cdoc_ced_ruc = reader["cdoc_ced_ruc"].ToString();
            this.cdoc_pais = (reader["cdoc_pais"] != DBNull.Value) ? (Int32?)reader["cdoc_pais"] : null;
            this.cdoc_provincia = (reader["cdoc_provincia"] != DBNull.Value) ? (Int32?)reader["cdoc_provincia"] : null;
            this.cdoc_canton = (reader["cdoc_canton"] != DBNull.Value) ? (Int32?)reader["cdoc_canton"] : null;
            this.cdoc_prioridad = (reader["cdoc_prioridad"] != DBNull.Value) ? (Int32?)reader["cdoc_prioridad"] : null;
            this.cdoc_empleado = (reader["cdoc_empleado"] != DBNull.Value) ? (Int32?)reader["cdoc_empleado"] : null;
            this.cdoc_departamento = (reader["cdoc_departamento"] != DBNull.Value) ? (Int32?)reader["cdoc_departamento"] : null;
            this.cdoc_observeaciones = reader["cdoc_observeaciones"].ToString();
            this.cdoc_impuesto = (reader["cdoc_impuesto"] != DBNull.Value) ? (Int32?)reader["cdoc_impuesto"] : null;
            this.cdoc_porc_impuesto = (reader["cdoc_porc_impuesto"] != DBNull.Value) ? (Decimal?)reader["cdoc_porc_impuesto"] : null;
            this.cdoc_acl_nroautoriza = reader["cdoc_acl_nroautoriza"].ToString();
            this.cdoc_acl_retdato = (reader["cdoc_acl_retdato"] != DBNull.Value) ? (Int32?)reader["cdoc_acl_retdato"] : null;
            this.cdoc_acl_tablacoa = (reader["cdoc_acl_tablacoa"] != DBNull.Value) ? (Int32?)reader["cdoc_acl_tablacoa"] : null;
            this.cdoc_control_temp = (reader["cdoc_control_temp"] != DBNull.Value) ? (Int32?)reader["cdoc_control_temp"] : null;
            this.cdoc_usr_liquida = (reader["cdoc_usr_liquida"] != DBNull.Value) ? (Int32?)reader["cdoc_usr_liquida"] : null;
            this.cdoc_formapago = reader["cdoc_formapago"].ToString();
            this.cdoc_pagoexterior = (reader["cdoc_pagoexterior"] != DBNull.Value) ? (Int32?)reader["cdoc_pagoexterior"] : null;
            this.cdoc_paispago = (reader["cdoc_paispago"] != DBNull.Value) ? (Int32?)reader["cdoc_paispago"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;


            this.cdoc_aut_factura = reader["cdoc_aut_factura"].ToString();
            this.cdoc_aut_fecha = (reader["cdoc_aut_fecha"] != DBNull.Value) ? (DateTime?)reader["cdoc_aut_fecha"] : null;
            this.cdoc_transacc = (reader["cdoc_transacc"] != DBNull.Value) ? (Int32?)reader["cdoc_transacc"] : null;
            this.cdoc_politicanombre = reader["cdoc_politicanombre"].ToString();

        }

        public Ccomdoc(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;

                object cdoc_empresa = null;
                object cdoc_comprobante = null;
                object cdoc_pedido = null;
                object cdoc_sol_comprobante = null;
                object cdoc_cobranza = null;
                object cdoc_factura = null;
                object cdoc_vigencia = null;
                object cdoc_autoriza = null;
                object cdoc_politica = null;
                object cdoc_listaprecio = null;
                object cdoc_est_entrega = null;
                object cdoc_nombre = null;
                object cdoc_direccion = null;
                object cdoc_telefono = null;
                object cdoc_ced_ruc = null;
                object cdoc_pais = null;
                object cdoc_provincia = null;
                object cdoc_canton = null;
                object cdoc_prioridad = null;
                object cdoc_empleado = null;
                object cdoc_departamento = null;
                object cdoc_observeaciones = null;
                object cdoc_impuesto = null;
                object cdoc_porc_impuesto = null;
                object cdoc_acl_nroautoriza = null;
                object cdoc_acl_retdato = null;
                object cdoc_acl_tablacoa = null;
                object cdoc_control_temp = null;
                object cdoc_usr_liquida = null;
                object cdoc_formapago = null;
                object cdoc_pagoexterior = null;
                object cdoc_paispago = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;

                object cdoc_aut_factura = null;
                object cdoc_aut_fecha = null;
                object cdoc_transacc = null;

                object detalle = null;

                tmp.TryGetValue("cdoc_empresa", out cdoc_empresa);
                tmp.TryGetValue("cdoc_comprobante", out cdoc_comprobante);
                tmp.TryGetValue("cdoc_pedido", out cdoc_pedido);
                tmp.TryGetValue("cdoc_sol_comprobante", out cdoc_sol_comprobante);
                tmp.TryGetValue("cdoc_cobranza", out cdoc_cobranza);
                tmp.TryGetValue("cdoc_factura", out cdoc_factura);
                tmp.TryGetValue("cdoc_vigencia", out cdoc_vigencia);
                tmp.TryGetValue("cdoc_autoriza", out cdoc_autoriza);
                tmp.TryGetValue("cdoc_politica", out cdoc_politica);
                tmp.TryGetValue("cdoc_listaprecio", out cdoc_listaprecio);
                tmp.TryGetValue("cdoc_est_entrega", out cdoc_est_entrega);
                tmp.TryGetValue("cdoc_nombre", out cdoc_nombre);
                tmp.TryGetValue("cdoc_direccion", out cdoc_direccion);
                tmp.TryGetValue("cdoc_telefono", out cdoc_telefono);
                tmp.TryGetValue("cdoc_ced_ruc", out cdoc_ced_ruc);
                tmp.TryGetValue("cdoc_pais", out cdoc_pais);
                tmp.TryGetValue("cdoc_provincia", out cdoc_provincia);
                tmp.TryGetValue("cdoc_canton", out cdoc_canton);
                tmp.TryGetValue("cdoc_prioridad", out cdoc_prioridad);
                tmp.TryGetValue("cdoc_empleado", out cdoc_empleado);
                tmp.TryGetValue("cdoc_departamento", out cdoc_departamento);
                tmp.TryGetValue("cdoc_observeaciones", out cdoc_observeaciones);
                tmp.TryGetValue("cdoc_impuesto", out cdoc_impuesto);
                tmp.TryGetValue("cdoc_porc_impuesto", out cdoc_porc_impuesto);
                tmp.TryGetValue("cdoc_acl_nroautoriza", out cdoc_acl_nroautoriza);
                tmp.TryGetValue("cdoc_acl_retdato", out cdoc_acl_retdato);
                tmp.TryGetValue("cdoc_acl_tablacoa", out cdoc_acl_tablacoa);
                tmp.TryGetValue("cdoc_control_temp", out cdoc_control_temp);
                tmp.TryGetValue("cdoc_usr_liquida", out cdoc_usr_liquida);
                tmp.TryGetValue("cdoc_formapago", out cdoc_formapago);
                tmp.TryGetValue("cdoc_pagoexterior", out cdoc_pagoexterior);
                tmp.TryGetValue("cdoc_paispago", out cdoc_paispago);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);

                tmp.TryGetValue("cdoc_aut_factura", out cdoc_aut_factura);
                tmp.TryGetValue("cdoc_aut_fecha", out cdoc_aut_fecha);
                tmp.TryGetValue("cdoc_transacc ", out cdoc_transacc);


                tmp.TryGetValue("detalle", out detalle);

                this.cdoc_empresa = (Int32)Conversiones.GetValueByType(cdoc_empresa, typeof(Int32));
                this.cdoc_comprobante = (Int64)Conversiones.GetValueByType(cdoc_comprobante, typeof(Int64));
                this.cdoc_pedido = (Int64?)Conversiones.GetValueByType(cdoc_pedido, typeof(Int64?));
                this.cdoc_sol_comprobante = (Int64?)Conversiones.GetValueByType(cdoc_sol_comprobante, typeof(Int64?));
                this.cdoc_cobranza = (Int64?)Conversiones.GetValueByType(cdoc_cobranza, typeof(Int64?));
                this.cdoc_factura = (Int64?)Conversiones.GetValueByType(cdoc_factura, typeof(Int64?));
                this.cdoc_vigencia = (DateTime?)Conversiones.GetValueByType(cdoc_vigencia, typeof(DateTime?));
                this.cdoc_autoriza = (Int32?)Conversiones.GetValueByType(cdoc_autoriza, typeof(Int32?));
                this.cdoc_politica = (Int32?)Conversiones.GetValueByType(cdoc_politica, typeof(Int32?));
                this.cdoc_listaprecio = (Int32?)Conversiones.GetValueByType(cdoc_listaprecio, typeof(Int32?));
                this.cdoc_est_entrega = (Int32?)Conversiones.GetValueByType(cdoc_est_entrega, typeof(Int32?));
                this.cdoc_nombre = (String)Conversiones.GetValueByType(cdoc_nombre, typeof(String));
                this.cdoc_direccion = (String)Conversiones.GetValueByType(cdoc_direccion, typeof(String));
                this.cdoc_telefono = (String)Conversiones.GetValueByType(cdoc_telefono, typeof(String));
                this.cdoc_ced_ruc = (String)Conversiones.GetValueByType(cdoc_ced_ruc, typeof(String));
                this.cdoc_pais = (Int32?)Conversiones.GetValueByType(cdoc_pais, typeof(Int32?));
                this.cdoc_provincia = (Int32?)Conversiones.GetValueByType(cdoc_provincia, typeof(Int32?));
                this.cdoc_canton = (Int32?)Conversiones.GetValueByType(cdoc_canton, typeof(Int32?));
                this.cdoc_prioridad = (Int32?)Conversiones.GetValueByType(cdoc_prioridad, typeof(Int32?));
                this.cdoc_empleado = (Int32?)Conversiones.GetValueByType(cdoc_empleado, typeof(Int32?));
                this.cdoc_departamento = (Int32?)Conversiones.GetValueByType(cdoc_departamento, typeof(Int32?));
                this.cdoc_observeaciones = (String)Conversiones.GetValueByType(cdoc_observeaciones, typeof(String));
                this.cdoc_impuesto = (Int32?)Conversiones.GetValueByType(cdoc_impuesto, typeof(Int32?));
                this.cdoc_porc_impuesto = (Decimal?)Conversiones.GetValueByType(cdoc_porc_impuesto, typeof(Decimal?));
                this.cdoc_acl_nroautoriza = (String)Conversiones.GetValueByType(cdoc_acl_nroautoriza, typeof(String));
                this.cdoc_acl_retdato = (Int32?)Conversiones.GetValueByType(cdoc_acl_retdato, typeof(Int32?));
                this.cdoc_acl_tablacoa = (Int32?)Conversiones.GetValueByType(cdoc_acl_tablacoa, typeof(Int32?));
                this.cdoc_control_temp = (Int32?)Conversiones.GetValueByType(cdoc_control_temp, typeof(Int32?));
                this.cdoc_usr_liquida = (Int32?)Conversiones.GetValueByType(cdoc_usr_liquida, typeof(Int32?));
                this.cdoc_formapago = (String)Conversiones.GetValueByType(cdoc_formapago, typeof(String));
                this.cdoc_pagoexterior = (Int32?)Conversiones.GetValueByType(cdoc_pagoexterior, typeof(Int32?));
                this.cdoc_paispago = (Int32?)Conversiones.GetValueByType(cdoc_paispago, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));

                this.cdoc_aut_factura = (String)Conversiones.GetValueByType(cdoc_aut_factura, typeof(String));
                this.cdoc_aut_fecha = (DateTime?)Conversiones.GetValueByType(cdoc_aut_fecha, typeof(DateTime?));
                this.cdoc_transacc = (Int32?)Conversiones.GetValueByType(cdoc_transacc, typeof(Int32?));

                this.detalle =  GetDetalleObj(detalle);


            }
            
        }
        #endregion

        #region Methods


        public List<Dcomdoc> GetDetalleObj(object arrayobj)
        {
            List<Dcomdoc> detalle = new List<Dcomdoc>();
            if (arrayobj != null)
            {
                Array array = (Array)arrayobj;
                foreach (Object item in array)
                {
                    if (item != null)
                        detalle.Add(new Dcomdoc(item));
                }
            }
            return detalle;

        }

        public PropertyInfo[] GetProperties()
        {
            return this.GetType().GetProperties();
        }
        #endregion


    }
}
