using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Electronicdet
    {
        #region Properties

        [Data(key = true)]
        public Int32 eled_empresa { get; set; }
        [Data(originalkey = true)]
        public Int32 eled_empresa_key { get; set; }
        [Data(key = true)]
        public Int64 eled_comprobante { get; set; }
        [Data(originalkey = true)]
        public Int64 eled_comprobante_key { get; set; }
        [Data(key = true)]
        public Int32 eled_secuencia { get; set; }
        [Data(originalkey = true)]
        public Int32 eled_secuencia_key { get; set; }
        public Int32? eled_producto { get; set; }
        public String eled_codigo { get; set; }
        public String eled_codigoaux { get; set; }
        public String eled_descripcion { get; set; }
        public Decimal? eled_cantidad { get; set; }
        public Decimal? eled_precio { get; set; }
        public Decimal? eled_descuento { get; set; }
        public Decimal? eled_totalsinimp { get; set; }
        public Decimal? eled_iva { get; set; }
        public Int32? eled_codigoiva { get; set; }
        public Decimal? eled_porciva { get; set; }
        public Decimal? eled_iva0 { get; set; }
        public Int32? eled_codigoiva0 { get; set; }
        public Decimal? eled_valoriva { get; set; }
        public Decimal? eled_ice { get; set; }
        public Int32? eled_codigoice { get; set; }
        public Decimal? eled_porcice { get; set; }

        public Decimal? eled_baseimp { get; set; }
        public Decimal? eled_porcret { get; set; }
        public Decimal? eled_valorret { get; set; }
        public String eled_coddocsustento { get; set; }
        public String eled_numdocsustento { get; set; }
        public String eled_fechadocsustento { get; set; }




        public String eled_adicional1 { get; set; }
        public String eled_adicional2 { get; set; }
        public String eled_adicional3 { get; set; }
        public String crea_usr { get; set; }
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
        public String crea_ip { get; set; }
        public String mod_ip { get; set; }


        #endregion

        #region Constructors


        public Electronicdet()
        {
        }

        public Electronicdet(Int32 eled_empresa, Int64 eled_comprobante, Int32 eled_secuencia, Int32 eled_producto, String eled_codigo, String eled_codigoaux, String eled_descripcion, Decimal eled_cantidad, Decimal eled_precio, Decimal eled_descuento, Decimal eled_totalsinimp, Decimal eled_iva, Int32 eled_codigoiva, Decimal eled_porciva, Decimal eled_iva0, Int32 eled_codigoiva0, Decimal eled_ice, Int32 eled_codigoice, Decimal eled_porcice, String eled_adicional1, String eled_adicional2, String eled_adicional3, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha, String crea_ip, String mod_ip)
        {
            this.eled_empresa = eled_empresa;
            this.eled_comprobante = eled_comprobante;
            this.eled_secuencia = eled_secuencia;
            this.eled_producto = eled_producto;
            this.eled_codigo = eled_codigo;
            this.eled_codigoaux = eled_codigoaux;
            this.eled_descripcion = eled_descripcion;
            this.eled_cantidad = eled_cantidad;
            this.eled_precio = eled_precio;
            this.eled_descuento = eled_descuento;
            this.eled_totalsinimp = eled_totalsinimp;
            this.eled_iva = eled_iva;
            this.eled_codigoiva = eled_codigoiva;
            this.eled_porciva = eled_porciva;
            this.eled_iva0 = eled_iva0;
            this.eled_codigoiva0 = eled_codigoiva0;
            this.eled_ice = eled_ice;
            this.eled_codigoice = eled_codigoice;
            this.eled_porcice = eled_porcice;
            this.eled_adicional1 = eled_adicional1;
            this.eled_adicional2 = eled_adicional2;
            this.eled_adicional3 = eled_adicional3;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;
            this.crea_ip = crea_ip;
            this.mod_ip = mod_ip;


        }

        public Electronicdet(IDataReader reader)
        {
            this.eled_empresa = (Int32)reader["eled_empresa"];
            this.eled_comprobante = (Int64)reader["eled_comprobante"];
            this.eled_secuencia = (Int32)reader["eled_secuencia"];
            this.eled_producto = (reader["eled_producto"] != DBNull.Value) ? (Int32?)reader["eled_producto"] : null;
            this.eled_codigo = reader["eled_codigo"].ToString();
            this.eled_codigoaux = reader["eled_codigoaux"].ToString();
            this.eled_descripcion = reader["eled_descripcion"].ToString();
            this.eled_cantidad = (reader["eled_cantidad"] != DBNull.Value) ? (Decimal?)reader["eled_cantidad"] : null;
            this.eled_precio = (reader["eled_precio"] != DBNull.Value) ? (Decimal?)reader["eled_precio"] : null;
            this.eled_descuento = (reader["eled_descuento"] != DBNull.Value) ? (Decimal?)reader["eled_descuento"] : null;
            this.eled_totalsinimp = (reader["eled_totalsinimp"] != DBNull.Value) ? (Decimal?)reader["eled_totalsinimp"] : null;
            this.eled_iva = (reader["eled_iva"] != DBNull.Value) ? (Decimal?)reader["eled_iva"] : null;
            this.eled_codigoiva = (reader["eled_codigoiva"] != DBNull.Value) ? (Int32?)reader["eled_codigoiva"] : null;
            this.eled_porciva = (reader["eled_porciva"] != DBNull.Value) ? (Decimal?)reader["eled_porciva"] : null;
            this.eled_iva0 = (reader["eled_iva0"] != DBNull.Value) ? (Decimal?)reader["eled_iva0"] : null;
            this.eled_codigoiva0 = (reader["eled_codigoiva0"] != DBNull.Value) ? (Int32?)reader["eled_codigoiva0"] : null;
            this.eled_valoriva = (reader["eled_valoriva"] != DBNull.Value) ? (Decimal?)reader["eled_valoriva"] : null;
            this.eled_ice = (reader["eled_ice"] != DBNull.Value) ? (Decimal?)reader["eled_ice"] : null;
            this.eled_codigoice = (reader["eled_codigoice"] != DBNull.Value) ? (Int32?)reader["eled_codigoice"] : null;
            this.eled_porcice = (reader["eled_porcice"] != DBNull.Value) ? (Decimal?)reader["eled_porcice"] : null;
            this.eled_adicional1 = reader["eled_adicional1"].ToString();
            this.eled_adicional2 = reader["eled_adicional2"].ToString();
            this.eled_adicional3 = reader["eled_adicional3"].ToString();
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.crea_ip = reader["crea_ip"].ToString();
            this.mod_ip = reader["mod_ip"].ToString();

        }


        public Electronicdet(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object eled_empresa = null;
                object eled_comprobante = null;
                object eled_secuencia = null;
                object eled_producto = null;
                object eled_codigo = null;
                object eled_codigoaux = null;
                object eled_descripcion = null;
                object eled_cantidad = null;
                object eled_precio = null;
                object eled_descuento = null;
                object eled_totalsinimp = null;
                object eled_iva = null;
                object eled_codigoiva = null;
                object eled_porciva = null;
                object eled_iva0 = null;
                object eled_codigoiva0 = null;
                object eled_valoriva = null;
                object eled_ice = null;
                object eled_codigoice = null;
                object eled_porcice = null;
                object eled_adicional1 = null;
                object eled_adicional2 = null;
                object eled_adicional3 = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object crea_ip = null;
                object mod_ip = null;


                tmp.TryGetValue("eled_empresa", out eled_empresa);
                tmp.TryGetValue("eled_comprobante", out eled_comprobante);
                tmp.TryGetValue("eled_secuencia", out eled_secuencia);
                tmp.TryGetValue("eled_producto", out eled_producto);
                tmp.TryGetValue("eled_codigo", out eled_codigo);
                tmp.TryGetValue("eled_codigoaux", out eled_codigoaux);
                tmp.TryGetValue("eled_descripcion", out eled_descripcion);
                tmp.TryGetValue("eled_cantidad", out eled_cantidad);
                tmp.TryGetValue("eled_precio", out eled_precio);
                tmp.TryGetValue("eled_descuento", out eled_descuento);
                tmp.TryGetValue("eled_totalsinimp", out eled_totalsinimp);
                tmp.TryGetValue("eled_iva", out eled_iva);
                tmp.TryGetValue("eled_codigoiva", out eled_codigoiva);
                tmp.TryGetValue("eled_porciva", out eled_porciva);
                tmp.TryGetValue("eled_iva0", out eled_iva0);
                tmp.TryGetValue("eled_codigoiva0", out eled_codigoiva0);
                tmp.TryGetValue("eled_valoriva", out eled_valoriva);
                tmp.TryGetValue("eled_ice", out eled_ice);
                tmp.TryGetValue("eled_codigoice", out eled_codigoice);
                tmp.TryGetValue("eled_porcice", out eled_porcice);
                tmp.TryGetValue("eled_adicional1", out eled_adicional1);
                tmp.TryGetValue("eled_adicional2", out eled_adicional2);
                tmp.TryGetValue("eled_adicional3", out eled_adicional3);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("crea_ip", out crea_ip);
                tmp.TryGetValue("mod_ip", out mod_ip);


                this.eled_empresa = (Int32)Conversiones.GetValueByType(eled_empresa, typeof(Int32));
                this.eled_comprobante = (Int64)Conversiones.GetValueByType(eled_comprobante, typeof(Int64));
                this.eled_secuencia = (Int32)Conversiones.GetValueByType(eled_secuencia, typeof(Int32));
                this.eled_producto = (Int32?)Conversiones.GetValueByType(eled_producto, typeof(Int32?));
                this.eled_codigo = (String)Conversiones.GetValueByType(eled_codigo, typeof(String));
                this.eled_codigoaux = (String)Conversiones.GetValueByType(eled_codigoaux, typeof(String));
                this.eled_descripcion = (String)Conversiones.GetValueByType(eled_descripcion, typeof(String));
                this.eled_cantidad = (Decimal?)Conversiones.GetValueByType(eled_cantidad, typeof(Decimal?));
                this.eled_precio = (Decimal?)Conversiones.GetValueByType(eled_precio, typeof(Decimal?));
                this.eled_descuento = (Decimal?)Conversiones.GetValueByType(eled_descuento, typeof(Decimal?));
                this.eled_totalsinimp = (Decimal?)Conversiones.GetValueByType(eled_totalsinimp, typeof(Decimal?));
                this.eled_iva = (Decimal?)Conversiones.GetValueByType(eled_iva, typeof(Decimal?));
                this.eled_codigoiva = (Int32?)Conversiones.GetValueByType(eled_codigoiva, typeof(Int32?));
                this.eled_porciva = (Decimal?)Conversiones.GetValueByType(eled_porciva, typeof(Decimal?));
                this.eled_iva0 = (Decimal?)Conversiones.GetValueByType(eled_iva0, typeof(Decimal?));
                this.eled_codigoiva0 = (Int32?)Conversiones.GetValueByType(eled_codigoiva0, typeof(Int32?));
                this.eled_valoriva = (Decimal?)Conversiones.GetValueByType(eled_valoriva, typeof(Decimal?));
                this.eled_ice = (Decimal?)Conversiones.GetValueByType(eled_ice, typeof(Decimal?));
                this.eled_codigoice = (Int32?)Conversiones.GetValueByType(eled_codigoice, typeof(Int32?));
                this.eled_porcice = (Decimal?)Conversiones.GetValueByType(eled_porcice, typeof(Decimal?));
                this.eled_adicional1 = (String)Conversiones.GetValueByType(eled_adicional1, typeof(String));
                this.eled_adicional2 = (String)Conversiones.GetValueByType(eled_adicional2, typeof(String));
                this.eled_adicional3 = (String)Conversiones.GetValueByType(eled_adicional3, typeof(String));
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

