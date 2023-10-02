using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Functions;
using System.Reflection;

namespace BusinessObjects
{
    public class vRetencion
    {
        public Int64? codigoret { get; set; }
        public DateTime? fecharet { get; set; }
        public string doctranret { get; set; }
        public int? numeroret { get; set; }
        public string ciruc { get; set; }
        public string nombres { get; set; }
        public Int64? codigofac { get; set; }
        public DateTime? fechafac { get; set; }
        public string doctranfac { get; set; }
        public string impuestoid { get; set; }
        public string impuestonombre { get; set; }
        public string conceptoid { get; set; }
        public decimal? baseimp { get; set; }
        public decimal? porcentaje { get; set; }
        public decimal? valor { get; set; }

        public decimal? subtotal0 { get; set; }
        public decimal? subtotalimp { get; set; }


        public string nfactura  { get; set; }
        public vRetencion()
       {

       }

       public vRetencion(object objeto)
       {
           if (objeto != null)
           {
               Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;

               object fecharet = null;
               object doctranret = null;
               object numeroret = null;
               object ciruc = null;
               object nombres = null;
               object fechafac = null;
               object doctranfac = null;
               object impuestoid = null;
               object impuestonombre = null;
               object conceptoid = null;
               
               object baseimp = null;
               object porcentaje = null;
               object valor = null;

                object nfactura = null;

                tmp.TryGetValue("fecharet", out fecharet);
               tmp.TryGetValue("doctranret", out doctranret);
               tmp.TryGetValue("numeroret", out numeroret);
               tmp.TryGetValue("ciruc", out ciruc);
               tmp.TryGetValue("nombres", out nombres);
               tmp.TryGetValue("fechafac", out fechafac);
               tmp.TryGetValue("doctranfac", out doctranfac);
               tmp.TryGetValue("impuestoid", out impuestoid);
               tmp.TryGetValue("impuestonombre", out impuestonombre);
               tmp.TryGetValue("conceptoid", out conceptoid);
               tmp.TryGetValue("baseimp", out baseimp);
               tmp.TryGetValue("porcentaje", out porcentaje);
               tmp.TryGetValue("valor", out valor);

                tmp.TryGetValue("nfactura", out nfactura);



                this.fecharet = (DateTime?)Conversiones.GetValueByType(fecharet, typeof(DateTime?));
               this.doctranret = (String)Conversiones.GetValueByType(doctranret, typeof(String));
               this.numeroret = (int?)Conversiones.GetValueByType(numeroret, typeof(int?));
               this.ciruc = (string)Conversiones.GetValueByType(ciruc, typeof(string));
               this.nombres = (string)Conversiones.GetValueByType(nombres, typeof(string));
               this.fechafac = (DateTime?)Conversiones.GetValueByType(fechafac, typeof(DateTime?));
               this.doctranfac = (string)Conversiones.GetValueByType(doctranfac, typeof(string));
               this.impuestoid = (string)Conversiones.GetValueByType(impuestoid, typeof(string));
               this.impuestonombre = (string)Conversiones.GetValueByType(impuestonombre, typeof(string));
               this.conceptoid= (string)Conversiones.GetValueByType(conceptoid, typeof(string));
               this.baseimp = (Decimal?)Conversiones.GetValueByType(baseimp, typeof(Decimal?));
               this.porcentaje = (Decimal?)Conversiones.GetValueByType(porcentaje, typeof(Decimal?));
               this.valor = (Decimal?)Conversiones.GetValueByType(valor, typeof(Decimal?));
                this.nfactura = (string)Conversiones.GetValueByType(nfactura, typeof(string));
            }


       }


       public vRetencion(IDataReader reader)
       {
           this.codigoret = (reader["codigoret"] != DBNull.Value) ? (Int64?)reader["codigoret"] : null;
           this.fecharet = (reader["fecharet"] != DBNull.Value) ? (DateTime?)reader["fecharet"] : null;
           this.doctranret = (reader["doctranret"] != DBNull.Value) ? (string)reader["doctranret"] : null;
           this.numeroret = (reader["numeroret"] != DBNull.Value) ? (int?)reader["numeroret"] : null;
           this.ciruc = (reader["cdoc_ced_ruc"] != DBNull.Value) ? (string)reader["cdoc_ced_ruc"] : null;
           this.nombres = (reader["cdoc_nombre"] != DBNull.Value) ? (string)reader["cdoc_nombre"] : null;
           this.codigofac= (reader["codigofac"] != DBNull.Value) ? (Int64?)reader["codigofac"] : null;
           this.fechafac = (reader["fechafac"] != DBNull.Value) ? (DateTime?)reader["fechafac"] : null;
           this.doctranfac = (reader["doctranfac"] != DBNull.Value) ? (string)reader["doctranfac"] : null;
           this.impuestoid = (reader["imp_id"] != DBNull.Value) ? (string)reader["imp_id"] : null;
           this.impuestonombre = (reader["imp_nombre"] != DBNull.Value) ? (string)reader["imp_nombre"] : null;
           this.conceptoid = (reader["con_id"] != DBNull.Value) ? (string)reader["con_id"] : null;
           this.baseimp = (reader["drt_valor"] != DBNull.Value) ? (Decimal?)reader["drt_valor"] : null;
           this.porcentaje = (reader["drt_porcentaje"] != DBNull.Value) ? (Decimal?)reader["drt_porcentaje"] : null;
           this.valor = (reader["drt_total"] != DBNull.Value) ? (Decimal?)reader["drt_total"] : null;
           this.subtotal0 = (reader["tot_subtot_0"] != DBNull.Value) ? (Decimal?)reader["tot_subtot_0"] : null;
           this.subtotalimp = (reader["tot_subtotal"] != DBNull.Value) ? (Decimal?)reader["tot_subtotal"] : null;

            this.nfactura = (reader["nfactura"] != DBNull.Value) ? (string)reader["nfactura"] : null;
            decimal baseimpfac = subtotal0.Value + subtotalimp.Value;
           /*if (baseimpfac != baseimp.Value)
           {
               conceptoid = "332";
               porcentaje = 0;
           }*/
           
       }


       public string GetSQLALL()
       {

           string sql = "SELECT " +
	                    "   r.com_fecha fecharet, " +
	                    "    r.com_codigo codigoret, " +
                        "    r.com_numero numeroret, " +
	                    "    r.com_doctran doctranret, " +
	                    "    r.com_concepto conceptoret, " +
                        "    cr.cdoc_ced_ruc, " +
	                    "    cr.cdoc_nombre, " +
	                    "    cr.cdoc_factura, " +
	                    "    imp_id, " +
	                    "    imp_nombre, " +
                        "    con_id, " +
	                    "    drt_impuesto, " +
	                    "    drt_valor, " +
	                    "    drt_porcentaje, " +
	                    "    drt_total, " +
	                    "    drt_factura, " +
                        "    f.com_fecha fechafac, " +
                        "    f.com_codigo codigofac, " +
	                    "    f.com_doctran doctranfac, " +
                        "    tf.tot_subtot_0, tf.tot_subtotal, " +
                        "    cf.cdoc_aut_factura as nfactura,"+
	                    "    ''	 " +
                        " FROM  " +
	                    "    comprobante r " +
	                    "    LEFT JOIN ccomdoc cr  ON r.com_empresa = cr.cdoc_empresa and r.com_codigo = cr.cdoc_comprobante " +
	                    "    LEFT JOIN dretencion ON drt_empresa = r.com_empresa and drt_comprobante = r.com_codigo " +
	                    "    LEFT JOIN impuesto ON drt_empresa = imp_empresa and drt_impuesto = imp_codigo " +
                        "    LEFT JOIN concepto ON imp_empresa = con_empresa and imp_concepto = con_codigo " +
	                    "    LEFT JOIN comprobante f ON cdoc_empresa = f.com_empresa and cdoc_factura = f.com_codigo " +
                        "    LEFT JOIN ccomdoc cf ON cf.cdoc_empresa = f.com_empresa and cf.cdoc_comprobante = f.com_codigo " +
                        "    LEFT JOIN total tf ON tf.tot_empresa = f.com_empresa and tf.tot_comprobante = f.com_codigo " +
                        "";
                        //" WHERE  " +
	                    //"    r.com_tipodoc = 16 " +
                        //" ORDER BY r.com_doctran" ;




           return sql;
       }
       public List<vRetencion> GetStruc()
       {
           return new List<vRetencion>();
       }

       public PropertyInfo[] GetProperties()
       {
           return this.GetType().GetProperties();
       }    

    }
}
