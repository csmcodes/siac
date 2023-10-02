using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Dcomdoc
    {
        #region Properties

        public Int32 ddoc_bodega { get; set; }
        public Int32 ddco_udigitada { get; set; }
        public Decimal ddoc_cantidad { get; set; }
        public Decimal ddoc_precio { get; set; }
        [Data(key = true)]
        public Int32 ddoc_empresa { get; set; }
        [Data(originalkey = true)]
        public Int32 ddoc_empresa_key { get; set; }
        [Data(key = true)]
        public Int64 ddoc_comprobante { get; set; }
        [Data(originalkey = true)]
        public Int64 ddoc_comprobante_key { get; set; }
        [Data(key = true)]
        public Int32 ddoc_secuencia { get; set; }
        [Data(originalkey = true)]
        public Int32 ddoc_secuencia_key { get; set; }
        public Decimal ddoc_total { get; set; }
        public Decimal? ddoc_porc_cab { get; set; }
        public Decimal? ddoc_dsccab { get; set; }
        public Decimal? ddoc_traitem { get; set; }
        public Decimal? ddoc_ivaitem { get; set; }
        public Decimal? ddoc_iceitem { get; set; }
        public Int32? ddoc_grabaiva { get; set; }
        public Decimal? ddoc_candev { get; set; }
        public Decimal? ddoc_cdigitada { get; set; }
        public Decimal? ddoc_pdigitado { get; set; }
        public String ddoc_observaciones { get; set; }
        [Data(noupdate = true)]
        public String crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
        public Int32? ddoc_producto { get; set; }
        public Int32? ddoc_cuenta { get; set; }
        public Int32? ddoc_ctadoc { get; set; }
        public Int32? ddoc_activo { get; set; }
        public Decimal? ddoc_porc_desc { get; set; }
        public Decimal? ddoc_dscitem { get; set; }
        public Decimal? ddoc_canapr { get; set; }
        public Decimal? ddoc_peso { get; set; }

        [Data(nosql = true, tablaref = "producto", camporef = "pro_id", foreign = "ddoc_empresa, ddoc_producto", keyref = "pro_empresa, pro_codigo", join = "left")]
        public string ddoc_productoid { get; set; }
        [Data(nosql = true, tablaref = "producto", camporef = "pro_nombre", foreign = "ddoc_empresa, ddoc_producto", keyref = "pro_empresa, pro_codigo", join = "left")]
        public string ddoc_productonombre { get; set; }
        [Data(nosql = true, tablaref = "producto", camporef = "pro_iva", foreign = "ddoc_empresa, ddoc_producto", keyref = "pro_empresa, pro_codigo", join = "left")]
        public int? ddoc_productoiva { get; set; }
        [Data(nosql = true, tablaref = "producto", camporef = "pro_calcula", foreign = "ddoc_empresa, ddoc_producto", keyref = "pro_empresa, pro_codigo", join = "left")]
        public int? ddoc_productocalcula { get; set; }
        [Data(nosql = true, tablaref = "producto", camporef = "pro_total", foreign = "ddoc_empresa, ddoc_producto", keyref = "pro_empresa, pro_codigo", join = "left")]
        public int? ddoc_productototal { get; set; }
        [Data(nosql = true, tablaref = "producto", camporef = "pro_grupo", foreign = "ddoc_empresa, ddoc_producto", keyref = "pro_empresa, pro_codigo", join = "left")]
        public int? ddoc_productogrupo { get; set; }

        [Data(nosql = true, tablaref = "producto", camporef = "pro_inventario", foreign = "ddoc_empresa, ddoc_producto", keyref = "pro_empresa, pro_codigo", join = "left")]
        public int? ddoc_productoinv { get; set; }

        [Data(nosql = true, tablaref = "umedida", camporef = "umd_nombre", foreign = "ddoc_empresa, ddco_udigitada", keyref = "umd_empresa, umd_codigo", join = "left")]
        public string ddoc_productounidad { get; set; }


        [Data(nosql = true, tablaref = "cuenta", camporef = "cue_id", foreign = "ddoc_empresa, ddoc_cuenta", keyref = "cue_empresa, cue_codigo", join = "left")]
        public string ddoc_cuentaid { get; set; }
        [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "ddoc_empresa, ddoc_cuenta", keyref = "cue_empresa, cue_codigo", join = "left")]
        public string ddoc_cuentanombre { get; set; }

        [Data(noprop = true)]
        public List<Dcalculoprecio> detallecalculo { get; set; }
     

        #endregion

        #region Constructors


        public Dcomdoc()
        {
        }

        public Dcomdoc(Int32 ddoc_bodega, Int32 ddco_udigitada, Decimal ddoc_cantidad, Decimal ddoc_precio, Int32 ddoc_empresa, Int64 ddoc_comprobante, Int32 ddoc_secuencia, Decimal ddoc_total, Decimal ddoc_porc_cab, Decimal ddoc_dsccab, Decimal ddoc_traitem, Decimal ddoc_ivaitem, Decimal ddoc_iceitem, Int32 ddoc_grabaiva, Decimal ddoc_candev, Decimal ddoc_cdigitada, Decimal ddoc_pdigitado, String ddoc_observaciones, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha, Int32 ddoc_producto, Int32 ddoc_cuenta, Int32 ddoc_ctadoc, Int32 ddoc_activo, Decimal ddoc_porc_desc, Decimal ddoc_dscitem, Decimal ddoc_canapr, Decimal ddoc_peso)
        {
            this.ddoc_bodega = ddoc_bodega;
            this.ddco_udigitada = ddco_udigitada;
            this.ddoc_cantidad = ddoc_cantidad;
            this.ddoc_precio = ddoc_precio;
            this.ddoc_empresa = ddoc_empresa;
            this.ddoc_comprobante = ddoc_comprobante;
            this.ddoc_secuencia = ddoc_secuencia;
            this.ddoc_empresa_key = ddoc_empresa;
            this.ddoc_comprobante_key = ddoc_comprobante;
            this.ddoc_secuencia_key = ddoc_secuencia;
            this.ddoc_total = ddoc_total;
            this.ddoc_porc_cab = ddoc_porc_cab;
            this.ddoc_dsccab = ddoc_dsccab;
            this.ddoc_traitem = ddoc_traitem;
            this.ddoc_ivaitem = ddoc_ivaitem;
            this.ddoc_iceitem = ddoc_iceitem;
            this.ddoc_grabaiva = ddoc_grabaiva;
            this.ddoc_candev = ddoc_candev;
            this.ddoc_cdigitada = ddoc_cdigitada;
            this.ddoc_pdigitado = ddoc_pdigitado;
            this.ddoc_observaciones = ddoc_observaciones;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;
            this.ddoc_producto = ddoc_producto;
            this.ddoc_cuenta = ddoc_cuenta;
            this.ddoc_ctadoc = ddoc_ctadoc;
            this.ddoc_activo = ddoc_activo;
            this.ddoc_porc_desc = ddoc_porc_desc;
            this.ddoc_dscitem = ddoc_dscitem;
            this.ddoc_canapr = ddoc_canapr;
            this.ddoc_peso = ddoc_peso;


        }

        public Dcomdoc(IDataReader reader)
        {
            this.ddoc_bodega = (Int32)reader["ddoc_bodega"];
            this.ddco_udigitada = (Int32)reader["ddco_udigitada"];
            this.ddoc_cantidad = (Decimal)reader["ddoc_cantidad"];
            this.ddoc_precio = (Decimal)reader["ddoc_precio"];
            this.ddoc_empresa = (Int32)reader["ddoc_empresa"];
            this.ddoc_comprobante = (Int64)reader["ddoc_comprobante"];
            this.ddoc_secuencia = (Int32)reader["ddoc_secuencia"];
            this.ddoc_empresa_key = (Int32)reader["ddoc_empresa"];
            this.ddoc_comprobante_key = (Int64)reader["ddoc_comprobante"];
            this.ddoc_secuencia_key = (Int32)reader["ddoc_secuencia"];
            this.ddoc_total = (Decimal)reader["ddoc_total"];
            this.ddoc_porc_cab = (reader["ddoc_porc_cab"] != DBNull.Value) ? (Decimal?)reader["ddoc_porc_cab"] : null;
            this.ddoc_dsccab = (reader["ddoc_dsccab"] != DBNull.Value) ? (Decimal?)reader["ddoc_dsccab"] : null;
            this.ddoc_traitem = (reader["ddoc_traitem"] != DBNull.Value) ? (Decimal?)reader["ddoc_traitem"] : null;
            this.ddoc_ivaitem = (reader["ddoc_ivaitem"] != DBNull.Value) ? (Decimal?)reader["ddoc_ivaitem"] : null;
            this.ddoc_iceitem = (reader["ddoc_iceitem"] != DBNull.Value) ? (Decimal?)reader["ddoc_iceitem"] : null;
            this.ddoc_grabaiva = (reader["ddoc_grabaiva"] != DBNull.Value) ? (Int32?)reader["ddoc_grabaiva"] : null;
            this.ddoc_candev = (reader["ddoc_candev"] != DBNull.Value) ? (Decimal?)reader["ddoc_candev"] : null;
            this.ddoc_cdigitada = (reader["ddoc_cdigitada"] != DBNull.Value) ? (Decimal?)reader["ddoc_cdigitada"] : null;
            this.ddoc_pdigitado = (reader["ddoc_pdigitado"] != DBNull.Value) ? (Decimal?)reader["ddoc_pdigitado"] : null;
            this.ddoc_observaciones = reader["ddoc_observaciones"].ToString();
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.ddoc_producto = (reader["ddoc_producto"] != DBNull.Value) ? (Int32?)reader["ddoc_producto"] : null;
            this.ddoc_cuenta = (reader["ddoc_cuenta"] != DBNull.Value) ? (Int32?)reader["ddoc_cuenta"] : null;
            this.ddoc_ctadoc = (reader["ddoc_ctadoc"] != DBNull.Value) ? (Int32?)reader["ddoc_ctadoc"] : null;
            this.ddoc_activo = (reader["ddoc_activo"] != DBNull.Value) ? (Int32?)reader["ddoc_activo"] : null;
            this.ddoc_porc_desc = (reader["ddoc_porc_desc"] != DBNull.Value) ? (Decimal?)reader["ddoc_porc_desc"] : null;
            this.ddoc_dscitem = (reader["ddoc_dscitem"] != DBNull.Value) ? (Decimal?)reader["ddoc_dscitem"] : null;
            this.ddoc_canapr = (reader["ddoc_canapr"] != DBNull.Value) ? (Decimal?)reader["ddoc_canapr"] : null;
            this.ddoc_peso = (reader["ddoc_peso"] != DBNull.Value) ? (Decimal?)reader["ddoc_peso"] : null;

            this.ddoc_productoid = reader["ddoc_productoid"].ToString();
            this.ddoc_productonombre = reader["ddoc_productonombre"].ToString();
            this.ddoc_productoiva = (reader["ddoc_productoiva"] != DBNull.Value) ? (Int32?)reader["ddoc_productoiva"] : null;
            this.ddoc_productounidad = reader["ddoc_productounidad"].ToString();
            this.ddoc_productocalcula = (reader["ddoc_productocalcula"] != DBNull.Value) ? (Int32?)reader["ddoc_productocalcula"] : null;
            this.ddoc_productototal = (reader["ddoc_productototal"] != DBNull.Value) ? (Int32?)reader["ddoc_productototal"] : null;
            this.ddoc_productoinv = (reader["ddoc_productoinv"] != DBNull.Value) ? (Int32?)reader["ddoc_productoinv"] : null;


            this.ddoc_productogrupo = (reader["ddoc_productogrupo"] != DBNull.Value) ? (Int32?)reader["ddoc_productogrupo"] : null;

            this.ddoc_cuentaid = reader["ddoc_cuentaid"].ToString();
            this.ddoc_cuentanombre = reader["ddoc_cuentanombre"].ToString();



        }


        public Dcomdoc(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;

                object ddoc_empresa = null;
                object ddoc_comprobante = null;
                object ddoc_secuencia = null;
                object ddoc_producto = null;
                object ddoc_cuenta = null;
                object ddoc_ctadoc = null;
                object ddoc_activo = null;
                object ddoc_bodega = null;
                object ddco_udigitada = null;
                object ddoc_cantidad = null;
                object ddoc_canapr = null;
                object ddoc_precio = null;
                object ddoc_porc_desc = null;
                object ddoc_dscitem = null;
                object ddoc_total = null;
                object ddoc_porc_cab = null;
                object ddoc_dsccab = null;
                object ddoc_traitem = null;
                object ddoc_ivaitem = null;
                object ddoc_iceitem = null;
                object ddoc_grabaiva = null;
                object ddoc_candev = null;
                object ddoc_cdigitada = null;
                object ddoc_pdigitado = null;
                object ddoc_peso = null;
                object ddoc_observaciones = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;

                object detallecalculo = null;

                tmp.TryGetValue("ddoc_empresa", out ddoc_empresa);
                tmp.TryGetValue("ddoc_comprobante", out ddoc_comprobante);
                tmp.TryGetValue("ddoc_secuencia", out ddoc_secuencia);
                tmp.TryGetValue("ddoc_producto", out ddoc_producto);
                tmp.TryGetValue("ddoc_cuenta", out ddoc_cuenta);
                tmp.TryGetValue("ddoc_ctadoc", out ddoc_ctadoc);
                tmp.TryGetValue("ddoc_activo", out ddoc_activo);
                tmp.TryGetValue("ddoc_bodega", out ddoc_bodega);
                tmp.TryGetValue("ddco_udigitada", out ddco_udigitada);
                tmp.TryGetValue("ddoc_cantidad", out ddoc_cantidad);
                tmp.TryGetValue("ddoc_canapr", out ddoc_canapr);
                tmp.TryGetValue("ddoc_precio", out ddoc_precio);
                tmp.TryGetValue("ddoc_porc_desc", out ddoc_porc_desc);
                tmp.TryGetValue("ddoc_dscitem", out ddoc_dscitem);
                tmp.TryGetValue("ddoc_total", out ddoc_total);
                tmp.TryGetValue("ddoc_porc_cab", out ddoc_porc_cab);
                tmp.TryGetValue("ddoc_dsccab", out ddoc_dsccab);
                tmp.TryGetValue("ddoc_traitem", out ddoc_traitem);
                tmp.TryGetValue("ddoc_ivaitem", out ddoc_ivaitem);
                tmp.TryGetValue("ddoc_iceitem", out ddoc_iceitem);
                tmp.TryGetValue("ddoc_grabaiva", out ddoc_grabaiva);
                tmp.TryGetValue("ddoc_candev", out ddoc_candev);
                tmp.TryGetValue("ddoc_cdigitada", out ddoc_cdigitada);
                tmp.TryGetValue("ddoc_pdigitado", out ddoc_pdigitado);
                tmp.TryGetValue("ddoc_peso", out ddoc_peso);
                tmp.TryGetValue("ddoc_observaciones", out ddoc_observaciones);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("detallecalculo", out detallecalculo);


                this.ddoc_empresa = (int)Conversiones.GetValueByType(ddoc_empresa, typeof(int));
                this.ddoc_comprobante = (int)Conversiones.GetValueByType(ddoc_comprobante, typeof(int));
                this.ddoc_secuencia = (int)Conversiones.GetValueByType(ddoc_secuencia, typeof(int));
                this.ddoc_producto = (int?)Conversiones.GetValueByType(ddoc_producto, typeof(int?));
                this.ddoc_cuenta = (int?)Conversiones.GetValueByType(ddoc_cuenta, typeof(int?));
                this.ddoc_ctadoc = (int?)Conversiones.GetValueByType(ddoc_ctadoc, typeof(int?));
                this.ddoc_activo = (int?)Conversiones.GetValueByType(ddoc_activo, typeof(int?));
                this.ddoc_bodega = (int)Conversiones.GetValueByType(ddoc_bodega, typeof(int));
                this.ddco_udigitada = (int)Conversiones.GetValueByType(ddco_udigitada, typeof(int));
                this.ddoc_cantidad = (decimal)Conversiones.GetValueByType(ddoc_cantidad, typeof(decimal));
                this.ddoc_canapr = (decimal?)Conversiones.GetValueByType(ddoc_canapr, typeof(decimal?));
                this.ddoc_precio = (decimal)Conversiones.GetValueByType(ddoc_precio, typeof(decimal));
                this.ddoc_porc_desc = (decimal?)Conversiones.GetValueByType(ddoc_porc_desc, typeof(decimal?));
                this.ddoc_dscitem = (decimal?)Conversiones.GetValueByType(ddoc_dscitem, typeof(decimal?));
                this.ddoc_total = (decimal)Conversiones.GetValueByType(ddoc_total, typeof(decimal));
                this.ddoc_porc_cab = (decimal?)Conversiones.GetValueByType(ddoc_porc_cab, typeof(decimal?));
                this.ddoc_dsccab = (decimal?)Conversiones.GetValueByType(ddoc_dsccab, typeof(decimal?));
                this.ddoc_traitem = (decimal?)Conversiones.GetValueByType(ddoc_traitem, typeof(decimal?));
                this.ddoc_ivaitem = (decimal?)Conversiones.GetValueByType(ddoc_ivaitem, typeof(decimal?));
                this.ddoc_iceitem = (decimal?)Conversiones.GetValueByType(ddoc_iceitem, typeof(decimal?));
                this.ddoc_grabaiva = (int?)Conversiones.GetValueByType(ddoc_grabaiva, typeof(int?));
                this.ddoc_candev = (decimal?)Conversiones.GetValueByType(ddoc_candev, typeof(decimal?));
                this.ddoc_cdigitada = (decimal?)Conversiones.GetValueByType(ddoc_cdigitada, typeof(decimal?));
                this.ddoc_pdigitado = (decimal?)Conversiones.GetValueByType(ddoc_pdigitado, typeof(decimal?));
                this.ddoc_peso= (decimal?)Conversiones.GetValueByType(ddoc_peso, typeof(decimal?));
                this.ddoc_observaciones = (string)Conversiones.GetValueByType(ddoc_observaciones, typeof(string));


                this.detallecalculo = GetDetalleCalculoObj(detallecalculo);





                /*obj.crea_usr = (crea_usr != null) ? (int)crea_usr : obj.crea_usr;
                obj.crea_fecha = (crea_fecha != null) ? (int)crea_fecha : obj.crea_fecha;
                obj.mod_usr = (mod_usr != null) ? (int)mod_usr : obj.mod_usr;
                obj.mod_fecha = (mod_fecha != null) ? (int)mod_fecha : obj.mod_fecha;*/


            }
           
        }

        #endregion

        #region Methods

        public List<Dcalculoprecio> GetDetalleCalculoObj(object arrayobj)
        {
            List<Dcalculoprecio> detalle = new List<Dcalculoprecio>();
            if (arrayobj != null)
            {
                Array array = (Array)arrayobj;
                foreach (Object item in array)
                {
                    if (item != null)
                        detalle.Add(new Dcalculoprecio(item));
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
