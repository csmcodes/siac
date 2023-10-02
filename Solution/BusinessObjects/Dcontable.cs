using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Dcontable
    {
        #region Properties

        [Data(key = true)]
        public Int32 dco_empresa { get; set; }
        [Data(originalkey = true)]
        public Int32 dco_empresa_key { get; set; }
        [Data(key = true)]
        public Int64 dco_comprobante { get; set; }
        [Data(originalkey = true)]
        public Int64 dco_comprobante_key { get; set; }
        [Data(key = true)]
        public Int32 dco_secuencia { get; set; }
        [Data(originalkey = true)]
        public Int32 dco_secuencia_key { get; set; }
        public Int32 dco_cuenta { get; set; }
        public Int32 dco_centro { get; set; }
        public Int32 dco_transacc { get; set; }
        public Int32 dco_debcre { get; set; }
        public Decimal dco_valor_nac { get; set; }
        public Decimal? dco_valor_ext { get; set; }
        public Decimal? dco_tipo_cambio { get; set; }
        public String dco_concepto { get; set; }
        public Int32? dco_almacen { get; set; }
        public Int32? dco_cliente { get; set; }
        public Int32? dco_agente { get; set; }
        public String dco_doctran { get; set; }
        public Int32? dco_nropago { get; set; }
        public DateTime? dco_fecha_vence { get; set; }
        public Int32? dco_ddo_comproba { get; set; }
        public Int32? dco_ddo_transacc { get; set; }
        public Int32? dco_producto { get; set; }
        public Int32? dco_bodega { get; set; }
        public String crea_usr { get; set; }
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

        [Data(nosql = true, tablaref = "cuenta", camporef = "cue_id", foreign = "dco_empresa, dco_cuenta", keyref = "cue_empresa, cue_codigo", join = "left")]
        public string dco_cuentaid { get; set; }
        [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "dco_empresa, dco_cuenta", keyref = "cue_empresa, cue_codigo", join = "left")]
        public string dco_cuentanombre { get; set; }
        [Data(nosql = true, tablaref = "cuenta", camporef = "cue_modulo", foreign = "dco_empresa, dco_cuenta", keyref = "cue_empresa, cue_codigo", join = "left")]
        public Int32? dco_cuentamodulo { get; set; }


        [Data(nosql = true, tablaref = "comprobante", camporef = "com_fecha", foreign = "dco_empresa, dco_comprobante", keyref = "com_empresa, com_codigo", join = "inner")]
        public DateTime? dco_comprobantefecha { get; set; }
        [Data(nosql = true, tablaref = "comprobante", camporef = "com_concepto", foreign = "dco_empresa, dco_comprobante", keyref = "com_empresa, com_codigo", join = "inner")]
        public string dco_compconcepto { get; set; }
        [Data(nosql = true, tablaref = "comprobante", camporef = "com_doctran", foreign = "dco_empresa, dco_comprobante", keyref = "com_empresa, com_codigo", join = "inner")]
        public string dco_compdoctran { get; set; }




        [Data(nosql = true, tablaref = "persona", camporef = "per_id", foreign = "dco_empresa, dco_cliente", keyref = "per_empresa, per_codigo", join = "left")]
        public string dco_clienteid { get; set; }
        [Data(nosql = true, tablaref = "persona", camporef = "per_nombres", foreign = "dco_empresa, dco_cliente", keyref = "per_empresa, per_codigo", join = "left")]
        public string dco_clientenombres { get; set; }
        [Data(nosql = true, tablaref = "persona", camporef = "per_apellidos", foreign = "dco_empresa, dco_cliente", keyref = "per_empresa, per_codigo", join = "left")]
        public string dco_clienteapellidos { get; set; }

        [Data(nosql = true, tablaref = "almacen", camporef = "alm_id", foreign = "dco_empresa, dco_almacen", keyref = "alm_empresa, alm_codigo", join = "left")]
        public string dco_almacenid { get; set; }
        [Data(nosql = true, tablaref = "almacen", camporef = "alm_nombre", foreign = "dco_empresa, dco_almacen", keyref = "alm_empresa, alm_codigo", join = "left")]
        public string dco_almacennombre { get; set; }

        [Data(nosql = true, tablaref = "centro", camporef = "cen_id", foreign = "dco_empresa, dco_centro", keyref = "cen_empresa, cen_codigo", join = "left")]
        public string dco_centroid { get; set; }
        [Data(nosql = true, tablaref = "centro", camporef = "cen_nombre", foreign = "dco_empresa, dco_centro", keyref = "cen_empresa, cen_codigo", join = "left")]
        public string dco_centronombre { get; set; }

        #endregion

        #region Constructors


        public Dcontable()
        {
        }

        public Dcontable(Int32 dco_empresa, Int64 dco_comprobante, Int32 dco_secuencia, Int32 dco_cuenta, Int32 dco_centro, Int32 dco_transacc, Int32 dco_debcre, Decimal dco_valor_nac, Decimal dco_valor_ext, Decimal dco_tipo_cambio, String dco_concepto, Int32 dco_almacen, Int32 dco_cliente, Int32 dco_agente, String dco_doctran, Int32 dco_nropago, DateTime dco_fecha_vence, Int32 dco_ddo_comproba, Int32 dco_ddo_transacc, Int32 dco_producto, Int32 dco_bodega, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha)
        {
            this.dco_empresa = dco_empresa;
            this.dco_comprobante = dco_comprobante;
            this.dco_secuencia = dco_secuencia;
            this.dco_cuenta = dco_cuenta;
            this.dco_centro = dco_centro;
            this.dco_transacc = dco_transacc;
            this.dco_debcre = dco_debcre;
            this.dco_valor_nac = dco_valor_nac;
            this.dco_valor_ext = dco_valor_ext;
            this.dco_tipo_cambio = dco_tipo_cambio;
            this.dco_concepto = dco_concepto;
            this.dco_almacen = dco_almacen;
            this.dco_cliente = dco_cliente;
            this.dco_agente = dco_agente;
            this.dco_doctran = dco_doctran;
            this.dco_nropago = dco_nropago;
            this.dco_fecha_vence = dco_fecha_vence;
            this.dco_ddo_comproba = dco_ddo_comproba;
            this.dco_ddo_transacc = dco_ddo_transacc;
            this.dco_producto = dco_producto;
            this.dco_bodega = dco_bodega;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;


        }

        public Dcontable(IDataReader reader)
        {
            this.dco_empresa = (Int32)reader["dco_empresa"];
            this.dco_comprobante = (Int64)reader["dco_comprobante"];
            this.dco_secuencia = (Int32)reader["dco_secuencia"];
            this.dco_cuenta = (Int32)reader["dco_cuenta"];
            this.dco_centro = (Int32)reader["dco_centro"];
            this.dco_transacc = (Int32)reader["dco_transacc"];
            this.dco_debcre = (Int32)reader["dco_debcre"];
            this.dco_valor_nac = (Decimal)reader["dco_valor_nac"];
            this.dco_valor_ext = (reader["dco_valor_ext"] != DBNull.Value) ? (Decimal?)reader["dco_valor_ext"] : null;
            this.dco_tipo_cambio = (reader["dco_tipo_cambio"] != DBNull.Value) ? (Decimal?)reader["dco_tipo_cambio"] : null;
            this.dco_concepto = reader["dco_concepto"].ToString();
            this.dco_almacen = (reader["dco_almacen"] != DBNull.Value) ? (Int32?)reader["dco_almacen"] : null;
            this.dco_cliente = (reader["dco_cliente"] != DBNull.Value) ? (Int32?)reader["dco_cliente"] : null;
            this.dco_agente = (reader["dco_agente"] != DBNull.Value) ? (Int32?)reader["dco_agente"] : null;
            this.dco_doctran = reader["dco_doctran"].ToString();
            this.dco_nropago = (reader["dco_nropago"] != DBNull.Value) ? (Int32?)reader["dco_nropago"] : null;
            this.dco_fecha_vence = (reader["dco_fecha_vence"] != DBNull.Value) ? (DateTime?)reader["dco_fecha_vence"] : null;
            this.dco_ddo_comproba = (reader["dco_ddo_comproba"] != DBNull.Value) ? (Int32?)reader["dco_ddo_comproba"] : null;
            this.dco_ddo_transacc = (reader["dco_ddo_transacc"] != DBNull.Value) ? (Int32?)reader["dco_ddo_transacc"] : null;
            this.dco_producto = (reader["dco_producto"] != DBNull.Value) ? (Int32?)reader["dco_producto"] : null;
            this.dco_bodega = (reader["dco_bodega"] != DBNull.Value) ? (Int32?)reader["dco_bodega"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

            this.dco_cuentaid = (reader["dco_cuentaid"] != DBNull.Value) ? (String)reader["dco_cuentaid"] : null;
            this.dco_cuentanombre = (reader["dco_cuentanombre"] != DBNull.Value) ? (String)reader["dco_cuentanombre"] : null;
            this.dco_cuentamodulo = (reader["dco_cuentamodulo"] != DBNull.Value) ? (Int32?)reader["dco_cuentamodulo"] : null;

            this.dco_comprobantefecha = (reader["dco_comprobantefecha"] != DBNull.Value) ? (DateTime?)reader["dco_comprobantefecha"] : null;
            this.dco_compconcepto = (reader["dco_compconcepto"] != DBNull.Value) ? (String)reader["dco_compconcepto"] : null;
            this.dco_compdoctran = (reader["dco_compdoctran"] != DBNull.Value) ? (String)reader["dco_compdoctran"] : null;

            this.dco_clienteid = (reader["dco_clienteid"] != DBNull.Value) ? (String)reader["dco_clienteid"] : null;
            this.dco_clientenombres = (reader["dco_clientenombres"] != DBNull.Value) ? (String)reader["dco_clientenombres"] : null;
            this.dco_clienteapellidos = (reader["dco_clienteapellidos"] != DBNull.Value) ? (String)reader["dco_clienteapellidos"] : null;




        }


        public Dcontable(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object dco_empresa = null;
                object dco_comprobante = null;
                object dco_secuencia = null;
                object dco_cuenta = null;
                object dco_centro = null;
                object dco_transacc = null;
                object dco_debcre = null;
                object dco_valor_nac = null;
                object dco_valor_ext = null;
                object dco_tipo_cambio = null;
                object dco_concepto = null;
                object dco_almacen = null;
                object dco_cliente = null;
                object dco_agente = null;
                object dco_doctran = null;
                object dco_nropago = null;
                object dco_fecha_vence = null;
                object dco_ddo_comproba = null;
                object dco_ddo_transacc = null;
                object dco_producto = null;
                object dco_bodega = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;

                object dco_cuentaid = null;
                object dco_cuentanombre = null;
                object dco_cuentamodulo = null;

                object dco_clienteid = null;
                object dco_clientenombres = null;
                object dco_clienteapellidos = null;


                tmp.TryGetValue("dco_empresa", out dco_empresa);
                tmp.TryGetValue("dco_comprobante", out dco_comprobante);
                tmp.TryGetValue("dco_secuencia", out dco_secuencia);
                tmp.TryGetValue("dco_cuenta", out dco_cuenta);
                tmp.TryGetValue("dco_centro", out dco_centro);
                tmp.TryGetValue("dco_transacc", out dco_transacc);
                tmp.TryGetValue("dco_debcre", out dco_debcre);
                tmp.TryGetValue("dco_valor_nac", out dco_valor_nac);
                tmp.TryGetValue("dco_valor_ext", out dco_valor_ext);
                tmp.TryGetValue("dco_tipo_cambio", out dco_tipo_cambio);
                tmp.TryGetValue("dco_concepto", out dco_concepto);
                tmp.TryGetValue("dco_almacen", out dco_almacen);
                tmp.TryGetValue("dco_cliente", out dco_cliente);
                tmp.TryGetValue("dco_agente", out dco_agente);
                tmp.TryGetValue("dco_doctran", out dco_doctran);
                tmp.TryGetValue("dco_nropago", out dco_nropago);
                tmp.TryGetValue("dco_fecha_vence", out dco_fecha_vence);
                tmp.TryGetValue("dco_ddo_comproba", out dco_ddo_comproba);
                tmp.TryGetValue("dco_ddo_transacc", out dco_ddo_transacc);
                tmp.TryGetValue("dco_producto", out dco_producto);
                tmp.TryGetValue("dco_bodega", out dco_bodega);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);

                tmp.TryGetValue("dco_cuentaid", out dco_cuentaid);
                tmp.TryGetValue("dco_cuentanombre", out dco_cuentanombre);
                tmp.TryGetValue("dco_cuentamodulo", out dco_cuentamodulo);
                tmp.TryGetValue("dco_clienteid", out dco_clienteid);
                tmp.TryGetValue("dco_clientenombres", out dco_clientenombres);
                tmp.TryGetValue("dco_clienteapellidos", out dco_clienteapellidos);


                this.dco_empresa = (Int32)Conversiones.GetValueByType(dco_empresa, typeof(Int32));
                this.dco_comprobante = (Int64)Conversiones.GetValueByType(dco_comprobante, typeof(Int64));
                this.dco_secuencia = (Int32)Conversiones.GetValueByType(dco_secuencia, typeof(Int32));
                this.dco_cuenta = (Int32)Conversiones.GetValueByType(dco_cuenta, typeof(Int32));
                this.dco_centro = (Int32)Conversiones.GetValueByType(dco_centro, typeof(Int32));
                this.dco_transacc = (Int32)Conversiones.GetValueByType(dco_transacc, typeof(Int32));
                this.dco_debcre = (Int32)Conversiones.GetValueByType(dco_debcre, typeof(Int32));
                this.dco_valor_nac = (Decimal)Conversiones.GetValueByType(dco_valor_nac, typeof(Decimal));
                this.dco_valor_ext = (Decimal?)Conversiones.GetValueByType(dco_valor_ext, typeof(Decimal?));
                this.dco_tipo_cambio = (Decimal?)Conversiones.GetValueByType(dco_tipo_cambio, typeof(Decimal?));
                this.dco_concepto = (String)Conversiones.GetValueByType(dco_concepto, typeof(String));
                this.dco_almacen = (Int32?)Conversiones.GetValueByType(dco_almacen, typeof(Int32?));
                this.dco_cliente = (Int32?)Conversiones.GetValueByType(dco_cliente, typeof(Int32?));
                this.dco_agente = (Int32?)Conversiones.GetValueByType(dco_agente, typeof(Int32?));
                this.dco_doctran = (String)Conversiones.GetValueByType(dco_doctran, typeof(String));
                this.dco_nropago = (Int32?)Conversiones.GetValueByType(dco_nropago, typeof(Int32?));
                this.dco_fecha_vence = (DateTime?)Conversiones.GetValueByType(dco_fecha_vence, typeof(DateTime?));
                this.dco_ddo_comproba = (Int32?)Conversiones.GetValueByType(dco_ddo_comproba, typeof(Int32?));
                this.dco_ddo_transacc = (Int32?)Conversiones.GetValueByType(dco_ddo_transacc, typeof(Int32?));
                this.dco_producto = (Int32?)Conversiones.GetValueByType(dco_producto, typeof(Int32?));
                this.dco_bodega = (Int32?)Conversiones.GetValueByType(dco_bodega, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));

                this.dco_cuentaid = (String)Conversiones.GetValueByType(dco_cuentaid, typeof(String));
                this.dco_cuentanombre = (String)Conversiones.GetValueByType(dco_cuentanombre, typeof(String));
                this.dco_cuentamodulo = (Int32?)Conversiones.GetValueByType(dco_cuentamodulo, typeof(Int32?));

                this.dco_clienteid = (String)Conversiones.GetValueByType(dco_clienteid, typeof(String));
                this.dco_clientenombres = (String)Conversiones.GetValueByType(dco_clientenombres, typeof(String));
                this.dco_clienteapellidos = (String)Conversiones.GetValueByType(dco_clienteapellidos, typeof(String));




            }
        }
        #endregion

        #region Methods
        public PropertyInfo[] GetProperties()
        {
            return this.GetType().GetProperties();
        }

        public List<Dcontable> GetStruc()
        {
            return new List<Dcontable>();
        }

        #endregion


    }
}
