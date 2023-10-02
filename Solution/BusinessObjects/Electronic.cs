using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Electronic
    {
        #region Properties

        [Data(key = true)]
        public Int32 ele_empresa { get; set; }
        [Data(originalkey = true)]
        public Int32 ele_empresa_key { get; set; }
        [Data(key = true)]
        public Int64 ele_comprobante { get; set; }
        [Data(originalkey = true)]
        public Int64 ele_comprobante_key { get; set; }
        public String ele_almacen { get; set; }
        public String ele_pventa { get; set; }
        public String ele_secuencia { get; set; }
        public Int32? ele_ambiente { get; set; }
        public Int32? ele_emision { get; set; }
        public Int32? ele_tipo { get; set; }
        public String ele_clave { get; set; }
        public String ele_autorizacion { get; set; }
        public String ele_fechaautorizacion { get; set; }
        public String ele_email { get; set; }
        public String ele_dirmatriz { get; set; }
        public String ele_dirsucursal { get; set; }
        public String ele_especial { get; set; }
        public String ele_contabilidad { get; set; }
        public String ele_tipoid { get; set; }
        public String ele_guiaremision { get; set; }

        public String ele_razonsocial{ get; set; }
        public String ele_idcomprador { get; set; }
        public String ele_dircomprador { get; set; }


        public Decimal? ele_totalsinimp { get; set; }
        public Decimal? ele_totaldesc { get; set; }
        public Decimal? ele_propina { get; set; }
        public Decimal? ele_total { get; set; }
        public Decimal? ele_iva { get; set; }
        public Decimal? ele_porciva { get; set; }
        public Int32? ele_codigoiva { get; set; }
        public Decimal? ele_iva0 { get; set; }
        public Int32? ele_codigoiva0 { get; set; }
        public Decimal? ele_valoriva { get; set; }
        public Decimal? ele_ice { get; set; }
        public Decimal? ele_porcice { get; set; }
        public Int32? ele_codigoice { get; set; }

        public String ele_coddocsustento { get; set; }
        public String ele_numdocsustento { get; set; }
        public String ele_fechadocsustento { get; set; }


        public String ele_adicional1 { get; set; }
        public String ele_adicional2 { get; set; }
        public String ele_adicional3 { get; set; }
        public String ele_adicional4 { get; set; }
        public String ele_adicional5 { get; set; }
        public String ele_adicional6 { get; set; }

        public String ele_nomadicional1 { get; set; }
        public String ele_nomadicional2 { get; set; }
        public String ele_nomadicional3 { get; set; }
        public String ele_nomadicional4 { get; set; }
        public String ele_nomadicional5 { get; set; }
        public String ele_nomadicional6 { get; set; }
        public String ele_periodo { get; set; }
        public Int32? ele_formato { get; set; }
        public String ele_xml { get; set; }
        public String crea_usr { get; set; }
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
        public String crea_ip { get; set; }
        public String mod_ip { get; set; }

        [Data(noprop = true)]
        public List<Electronicdet> detalle { get; set; }
        [Data(noprop = true)]
        public List<Formapago> formas { get; set; }



        #endregion

        #region Constructors


        public Electronic()
        {
        }

        public Electronic(Int32 ele_empresa, Int64 ele_comprobante, String ele_almacen, String ele_pventa, String ele_secuencia, Int32 ele_ambiente, Int32 ele_emision, Int32 ele_tipo, String ele_clave, String ele_autorizacion, String ele_fechaautorizacion, String ele_email, String ele_dirmatriz, String ele_dirsucursal, String ele_especial, String ele_contabilidad, String ele_tipoid, String ele_guiaremision, Decimal ele_totalsinimp, Decimal ele_totaldesc, Decimal ele_propina, Decimal ele_total, Decimal ele_iva, Decimal ele_porciva, Int32 ele_codigoiva, Decimal ele_iva0, Int32 ele_codigoiva0, Decimal ele_ice, Decimal ele_porcice, Int32 ele_codigoice, String ele_adicional1, String ele_adicional2, String ele_adicional3, String ele_adicional4, String ele_adicional5, String ele_adicional6, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha, String crea_ip, String mod_ip)
        {
            this.ele_empresa = ele_empresa;
            this.ele_comprobante = ele_comprobante;
            this.ele_almacen = ele_almacen;
            this.ele_pventa = ele_pventa;
            this.ele_secuencia = ele_secuencia;
            this.ele_ambiente = ele_ambiente;
            this.ele_emision = ele_emision;
            this.ele_tipo = ele_tipo;
            this.ele_clave = ele_clave;
            this.ele_autorizacion = ele_autorizacion;
            this.ele_fechaautorizacion = ele_fechaautorizacion;
            this.ele_email = ele_email;
            this.ele_dirmatriz = ele_dirmatriz;
            this.ele_dirsucursal = ele_dirsucursal;
            this.ele_especial = ele_especial;
            this.ele_contabilidad = ele_contabilidad;
            this.ele_tipoid = ele_tipoid;
            this.ele_guiaremision = ele_guiaremision;
            this.ele_totalsinimp = ele_totalsinimp;
            this.ele_totaldesc = ele_totaldesc;
            this.ele_propina = ele_propina;
            this.ele_total = ele_total;
            this.ele_iva = ele_iva;
            this.ele_porciva = ele_porciva;
            this.ele_codigoiva = ele_codigoiva;
            this.ele_iva0 = ele_iva0;
            this.ele_codigoiva0 = ele_codigoiva0;
            this.ele_ice = ele_ice;
            this.ele_porcice = ele_porcice;
            this.ele_codigoice = ele_codigoice;
            this.ele_adicional1 = ele_adicional1;
            this.ele_adicional2 = ele_adicional2;
            this.ele_adicional3 = ele_adicional3;
            this.ele_adicional4 = ele_adicional4;
            this.ele_adicional5 = ele_adicional5;
            this.ele_adicional6 = ele_adicional6;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;
            this.crea_ip = crea_ip;
            this.mod_ip = mod_ip;


        }

        public Electronic(IDataReader reader)
        {
            this.ele_empresa = (Int32)reader["ele_empresa"];
            this.ele_comprobante = (Int64)reader["ele_comprobante"];
            this.ele_almacen = reader["ele_almacen"].ToString();
            this.ele_pventa = reader["ele_pventa"].ToString();
            this.ele_secuencia = reader["ele_secuencia"].ToString();
            this.ele_ambiente = (reader["ele_ambiente"] != DBNull.Value) ? (Int32?)reader["ele_ambiente"] : null;
            this.ele_emision = (reader["ele_emision"] != DBNull.Value) ? (Int32?)reader["ele_emision"] : null;
            this.ele_tipo = (reader["ele_tipo"] != DBNull.Value) ? (Int32?)reader["ele_tipo"] : null;
            this.ele_clave = reader["ele_clave"].ToString();
            this.ele_autorizacion = reader["ele_autorizacion"].ToString();
            this.ele_fechaautorizacion = reader["ele_fechaautorizacion"].ToString();
            this.ele_email = reader["ele_email"].ToString();
            this.ele_dirmatriz = reader["ele_dirmatriz"].ToString();
            this.ele_dirsucursal = reader["ele_dirsucursal"].ToString();
            this.ele_especial = reader["ele_especial"].ToString();
            this.ele_contabilidad = reader["ele_contabilidad"].ToString();
            this.ele_tipoid = reader["ele_tipoid"].ToString();
            this.ele_guiaremision = reader["ele_guiaremision"].ToString();
            this.ele_totalsinimp = (reader["ele_totalsinimp"] != DBNull.Value) ? (Decimal?)reader["ele_totalsinimp"] : null;
            this.ele_totaldesc = (reader["ele_totaldesc"] != DBNull.Value) ? (Decimal?)reader["ele_totaldesc"] : null;
            this.ele_propina = (reader["ele_propina"] != DBNull.Value) ? (Decimal?)reader["ele_propina"] : null;
            this.ele_total = (reader["ele_total"] != DBNull.Value) ? (Decimal?)reader["ele_total"] : null;
            this.ele_iva = (reader["ele_iva"] != DBNull.Value) ? (Decimal?)reader["ele_iva"] : null;
            this.ele_porciva = (reader["ele_porciva"] != DBNull.Value) ? (Decimal?)reader["ele_porciva"] : null;
            this.ele_codigoiva = (reader["ele_codigoiva"] != DBNull.Value) ? (Int32?)reader["ele_codigoiva"] : null;
            this.ele_iva0 = (reader["ele_iva0"] != DBNull.Value) ? (Decimal?)reader["ele_iva0"] : null;
            this.ele_codigoiva0 = (reader["ele_codigoiva0"] != DBNull.Value) ? (Int32?)reader["ele_codigoiva0"] : null;
            this.ele_valoriva = (reader["ele_valoriva"] != DBNull.Value) ? (Decimal?)reader["ele_valoriva"] : null;
            this.ele_ice = (reader["ele_ice"] != DBNull.Value) ? (Decimal?)reader["ele_ice"] : null;
            this.ele_porcice = (reader["ele_porcice"] != DBNull.Value) ? (Decimal?)reader["ele_porcice"] : null;
            this.ele_codigoice = (reader["ele_codigoice"] != DBNull.Value) ? (Int32?)reader["ele_codigoice"] : null;
            this.ele_adicional1 = reader["ele_adicional1"].ToString();
            this.ele_adicional2 = reader["ele_adicional2"].ToString();
            this.ele_adicional3 = reader["ele_adicional3"].ToString();
            this.ele_adicional4 = reader["ele_adicional4"].ToString();
            this.ele_adicional5 = reader["ele_adicional5"].ToString();
            this.ele_adicional6 = reader["ele_adicional6"].ToString();


            this.ele_nomadicional1 = reader["ele_nomadicional1"].ToString();
            this.ele_nomadicional2 = reader["ele_nomadicional2"].ToString();
            this.ele_nomadicional3 = reader["ele_nomadicional3"].ToString();
            this.ele_nomadicional4 = reader["ele_nomadicional4"].ToString();
            this.ele_nomadicional5 = reader["ele_nomadicional5"].ToString();
            this.ele_nomadicional6 = reader["ele_nomadicional6"].ToString();
            this.ele_periodo= reader["ele_periodo"].ToString();

            this.ele_xml = reader["ele_xml"].ToString();
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.crea_ip = reader["crea_ip"].ToString();
            this.mod_ip = reader["mod_ip"].ToString();

        }


        public Electronic(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object ele_empresa = null;
                object ele_comprobante = null;
                object ele_almacen = null;
                object ele_pventa = null;
                object ele_secuencia = null;
                object ele_ambiente = null;
                object ele_emision = null;
                object ele_tipo = null;
                object ele_clave = null;
                object ele_autorizacion = null;
                object ele_fechaautorizacion = null;
                object ele_email = null;
                object ele_dirmatriz = null;
                object ele_dirsucursal = null;
                object ele_especial = null;
                object ele_contabilidad = null;
                object ele_tipoid = null;
                object ele_guiaremision = null;
                object ele_totalsinimp = null;
                object ele_totaldesc = null;
                object ele_propina = null;
                object ele_total = null;
                object ele_iva = null;
                object ele_porciva = null;
                object ele_codigoiva = null;
                object ele_iva0 = null;
                object ele_codigoiva0 = null;
                object ele_valoriva = null;
                object ele_ice = null;
                object ele_porcice = null;
                object ele_codigoice = null;
                object ele_adicional1 = null;
                object ele_adicional2 = null;
                object ele_adicional3 = null;
                object ele_adicional4 = null;
                object ele_adicional5 = null;
                object ele_adicional6 = null;
                object ele_xml = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object crea_ip = null;
                object mod_ip = null;


                tmp.TryGetValue("ele_empresa", out ele_empresa);
                tmp.TryGetValue("ele_comprobante", out ele_comprobante);
                tmp.TryGetValue("ele_almacen", out ele_almacen);
                tmp.TryGetValue("ele_pventa", out ele_pventa);
                tmp.TryGetValue("ele_secuencia", out ele_secuencia);
                tmp.TryGetValue("ele_ambiente", out ele_ambiente);
                tmp.TryGetValue("ele_emision", out ele_emision);
                tmp.TryGetValue("ele_tipo", out ele_tipo);
                tmp.TryGetValue("ele_clave", out ele_clave);
                tmp.TryGetValue("ele_autorizacion", out ele_autorizacion);
                tmp.TryGetValue("ele_fechaautorizacion", out ele_fechaautorizacion);
                tmp.TryGetValue("ele_email", out ele_email);
                tmp.TryGetValue("ele_dirmatriz", out ele_dirmatriz);
                tmp.TryGetValue("ele_dirsucursal", out ele_dirsucursal);
                tmp.TryGetValue("ele_especial", out ele_especial);
                tmp.TryGetValue("ele_contabilidad", out ele_contabilidad);
                tmp.TryGetValue("ele_tipoid", out ele_tipoid);
                tmp.TryGetValue("ele_guiaremision", out ele_guiaremision);
                tmp.TryGetValue("ele_totalsinimp", out ele_totalsinimp);
                tmp.TryGetValue("ele_totaldesc", out ele_totaldesc);
                tmp.TryGetValue("ele_propina", out ele_propina);
                tmp.TryGetValue("ele_total", out ele_total);
                tmp.TryGetValue("ele_iva", out ele_iva);
                tmp.TryGetValue("ele_porciva", out ele_porciva);
                tmp.TryGetValue("ele_codigoiva", out ele_codigoiva);
                tmp.TryGetValue("ele_iva0", out ele_iva0);
                tmp.TryGetValue("ele_codigoiva0", out ele_codigoiva0);
                tmp.TryGetValue("ele_valoriva", out ele_valoriva);
                tmp.TryGetValue("ele_ice", out ele_ice);
                tmp.TryGetValue("ele_porcice", out ele_porcice);
                tmp.TryGetValue("ele_codigoice", out ele_codigoice);
                tmp.TryGetValue("ele_adicional1", out ele_adicional1);
                tmp.TryGetValue("ele_adicional2", out ele_adicional2);
                tmp.TryGetValue("ele_adicional3", out ele_adicional3);
                tmp.TryGetValue("ele_adicional4", out ele_adicional4);
                tmp.TryGetValue("ele_adicional5", out ele_adicional5);
                tmp.TryGetValue("ele_adicional6", out ele_adicional6);
                tmp.TryGetValue("ele_xml", out ele_xml);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("crea_ip", out crea_ip);
                tmp.TryGetValue("mod_ip", out mod_ip);


                this.ele_empresa = (Int32)Conversiones.GetValueByType(ele_empresa, typeof(Int32));
                this.ele_comprobante = (Int64)Conversiones.GetValueByType(ele_comprobante, typeof(Int64));
                this.ele_almacen = (String)Conversiones.GetValueByType(ele_almacen, typeof(String));
                this.ele_pventa = (String)Conversiones.GetValueByType(ele_pventa, typeof(String));
                this.ele_secuencia = (String)Conversiones.GetValueByType(ele_secuencia, typeof(String));
                this.ele_ambiente = (Int32?)Conversiones.GetValueByType(ele_ambiente, typeof(Int32?));
                this.ele_emision = (Int32?)Conversiones.GetValueByType(ele_emision, typeof(Int32?));
                this.ele_tipo = (Int32?)Conversiones.GetValueByType(ele_tipo, typeof(Int32?));
                this.ele_clave = (String)Conversiones.GetValueByType(ele_clave, typeof(String));
                this.ele_autorizacion = (String)Conversiones.GetValueByType(ele_autorizacion, typeof(String));
                this.ele_fechaautorizacion = (String)Conversiones.GetValueByType(ele_fechaautorizacion, typeof(String));
                this.ele_email = (String)Conversiones.GetValueByType(ele_email, typeof(String));
                this.ele_dirmatriz = (String)Conversiones.GetValueByType(ele_dirmatriz, typeof(String));
                this.ele_dirsucursal = (String)Conversiones.GetValueByType(ele_dirsucursal, typeof(String));
                this.ele_especial = (String)Conversiones.GetValueByType(ele_especial, typeof(String));
                this.ele_contabilidad = (String)Conversiones.GetValueByType(ele_contabilidad, typeof(String));
                this.ele_tipoid = (String)Conversiones.GetValueByType(ele_tipoid, typeof(String));
                this.ele_guiaremision = (String)Conversiones.GetValueByType(ele_guiaremision, typeof(String));
                this.ele_totalsinimp = (Decimal?)Conversiones.GetValueByType(ele_totalsinimp, typeof(Decimal?));
                this.ele_totaldesc = (Decimal?)Conversiones.GetValueByType(ele_totaldesc, typeof(Decimal?));
                this.ele_propina = (Decimal?)Conversiones.GetValueByType(ele_propina, typeof(Decimal?));
                this.ele_total = (Decimal?)Conversiones.GetValueByType(ele_total, typeof(Decimal?));
                this.ele_iva = (Decimal?)Conversiones.GetValueByType(ele_iva, typeof(Decimal?));
                this.ele_porciva = (Decimal?)Conversiones.GetValueByType(ele_porciva, typeof(Decimal?));
                this.ele_codigoiva = (Int32?)Conversiones.GetValueByType(ele_codigoiva, typeof(Int32?));
                this.ele_iva0 = (Decimal?)Conversiones.GetValueByType(ele_iva0, typeof(Decimal?));
                this.ele_codigoiva0 = (Int32?)Conversiones.GetValueByType(ele_codigoiva0, typeof(Int32?));
                this.ele_valoriva = (Decimal?)Conversiones.GetValueByType(ele_valoriva, typeof(Decimal?));
                this.ele_ice = (Decimal?)Conversiones.GetValueByType(ele_ice, typeof(Decimal?));
                this.ele_porcice = (Decimal?)Conversiones.GetValueByType(ele_porcice, typeof(Decimal?));
                this.ele_codigoice = (Int32?)Conversiones.GetValueByType(ele_codigoice, typeof(Int32?));
                this.ele_adicional1 = (String)Conversiones.GetValueByType(ele_adicional1, typeof(String));
                this.ele_adicional2 = (String)Conversiones.GetValueByType(ele_adicional2, typeof(String));
                this.ele_adicional3 = (String)Conversiones.GetValueByType(ele_adicional3, typeof(String));
                this.ele_adicional4 = (String)Conversiones.GetValueByType(ele_adicional4, typeof(String));
                this.ele_adicional5 = (String)Conversiones.GetValueByType(ele_adicional5, typeof(String));
                this.ele_adicional6 = (String)Conversiones.GetValueByType(ele_adicional6, typeof(String));
                this.ele_xml = (String)Conversiones.GetValueByType(ele_xml, typeof(String));
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
