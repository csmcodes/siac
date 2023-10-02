using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Functions;
using System.Reflection;

namespace BusinessObjects
{
    public class vCompras
    {

        public Int64? codigo { get; set; }
        public DateTime? fecha { get; set; }
        public string doctran { get; set; }
        public Int32? tipodoc { get; set; }
        public Decimal? total { get; set; }
        public Decimal? subtotal0 { get; set; }
        public Decimal? subtotal { get; set; }
        public Decimal? ice { get; set; }
        public Decimal? subtotalice { get; set; }
        public Decimal? impuesto { get; set; }
        public Decimal? seguro { get; set; }
        public Decimal? transporte { get; set; }
        public string doctranret { get; set; }
        public string almacenret { get; set; }
        public string pventaret { get; set; }
        public string numeroret { get; set; }
        public string autorizacionret { get; set; }
        public DateTime? fecharet { get; set; }


        
        public DateTime? fechafac { get; set; }
        public string almacenfac { get; set; }
        public string pventafac { get; set; }
        public string numerofac { get; set; }
        public string autorizacionfac { get; set; }
        public string observacionfac { get; set; }
        public string formapagofac { get; set; }

        public string retid { get; set; }

        public string nfactura { get; set; }
        public string razon { get; set; }
        public string tipoid { get; set; }
        
        public string ruc { get; set; }
        public Decimal? retiva { get; set; }
        public Decimal? retiva10 { get; set; }
        public Decimal? retiva20 { get; set; }
        public Decimal? retiva30 { get; set; }
        public Decimal? retiva50 { get; set; }
        public Decimal? retiva70 { get; set; }
        public Decimal? retiva100 { get; set; }
        public Decimal? retfue { get; set; }

        public string codsustentoats { get; set; }
        public string tipoidats { get; set; }
        public string tipocomprobanteats { get; set; }

        public string docmod { get; set; }
        public string almacenmod{ get; set; }
        public string pventamod{ get; set; }
        public string numeromod{ get; set; }
        public string autorizacionmod{ get; set; }




        public vCompras()
        {


        }

        public vCompras(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object fecha = null;
                object doctran = null;
                object tipodoc = null;
                object total = null;
                object subtotal0 = null;
                object subtotal = null;
                object impuesto = null;
                object ice = null;
                object seguro = null;
                object transporte = null;
                object doctranret = null;
                object nfactura = null;
                object razon = null;
                object ruc = null;
                object retiva = null;
                object retfue = null;
                tmp.TryGetValue("fecha", out fecha);
                tmp.TryGetValue("doctran", out doctran);
                tmp.TryGetValue("tipodoc", out tipodoc);
                tmp.TryGetValue("total", out total);
                tmp.TryGetValue("subtotal0", out subtotal0);
                tmp.TryGetValue("subtotal", out subtotal);
                tmp.TryGetValue("impuesto", out impuesto);
                tmp.TryGetValue("ice", out ice);
                tmp.TryGetValue("seguro", out seguro);
                tmp.TryGetValue("transporte", out transporte);
                tmp.TryGetValue("doctranret", out doctranret);
                tmp.TryGetValue("nfactura", out nfactura);
                tmp.TryGetValue("razon", out razon);
                tmp.TryGetValue("ruc", out ruc);
                tmp.TryGetValue("retiva", out retiva);
                tmp.TryGetValue("retfue", out retfue);

                this.fecha = (DateTime?)Conversiones.GetValueByType(fecha, typeof(DateTime?));
                this.doctran = (String)Conversiones.GetValueByType(doctran, typeof(String));
                this.tipodoc = (Int32?)Conversiones.GetValueByType(doctran, typeof(Int32?));
                this.total = (Decimal?)Conversiones.GetValueByType(total, typeof(Decimal?));
                this.subtotal0 = (Decimal?)Conversiones.GetValueByType(subtotal0, typeof(Decimal?));
                this.subtotal = (Decimal?)Conversiones.GetValueByType(subtotal, typeof(Decimal?));
                this.impuesto = (Decimal?)Conversiones.GetValueByType(impuesto, typeof(Decimal?));
                this.ice = (Decimal?)Conversiones.GetValueByType(ice, typeof(Decimal?));
                this.seguro = (Decimal?)Conversiones.GetValueByType(seguro, typeof(Decimal?));
                this.transporte = (Decimal?)Conversiones.GetValueByType(transporte, typeof(Decimal?));
                this.doctranret = (String)Conversiones.GetValueByType(doctranret, typeof(String));
                this.nfactura = (String)Conversiones.GetValueByType(nfactura, typeof(String));
                this.razon = (String)Conversiones.GetValueByType(razon, typeof(String));
                this.ruc = (String)Conversiones.GetValueByType(ruc, typeof(String));
                this.retiva = (Decimal?)Conversiones.GetValueByType(retiva, typeof(Decimal?));
                this.retfue = (Decimal?)Conversiones.GetValueByType(retfue, typeof(Decimal?));



            }
        }


        public vCompras(IDataReader reader)
        {
            this.codigo = (reader["com_codigo"] != DBNull.Value) ? (Int64?)reader["com_codigo"] : null;
            this.fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
            this.doctran = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
            this.tipodoc = (reader["com_tipodoc"] != DBNull.Value) ? (Int32?)reader["com_tipodoc"] : null;
            this.total = (reader["tot_total"] != DBNull.Value) ? (Decimal?)reader["tot_total"] : null;
            this.subtotal0 = (reader["tot_subtot_0"] != DBNull.Value) ? (Decimal?)reader["tot_subtot_0"] : null;
            this.subtotal = (reader["tot_subtotal"] != DBNull.Value) ? (Decimal?)reader["tot_subtotal"] : null;
            this.seguro = (reader["tot_tseguro"] != DBNull.Value) ? (Decimal?)reader["tot_tseguro"] : null;
            this.impuesto = (reader["tot_timpuesto"] != DBNull.Value) ? (Decimal?)reader["tot_timpuesto"] : null;
            this.ice= (reader["tot_ice"] != DBNull.Value) ? (Decimal?)reader["tot_ice"] : null;
            this.transporte = (reader["tot_transporte"] != DBNull.Value) ? (Decimal?)reader["tot_transporte"] : null;
            this.nfactura = (reader["cdoc_aut_factura"] != DBNull.Value) ? (string)reader["cdoc_aut_factura"] : null;
            this.fechafac = (reader["cdoc_aut_fecha"] != DBNull.Value) ? (DateTime?)reader["cdoc_aut_fecha"] : null;
            string[] arraynumero = nfactura.Split('-');
            if (arraynumero.Length > 2)
            {
                this.almacenfac = arraynumero[0];
                this.pventafac= arraynumero[1];
                this.numerofac = arraynumero[2];
            }


            this.subtotalice = (subtotal.HasValue ? subtotal.Value : 0) + (ice.HasValue ? ice.Value : 0);

            this.autorizacionfac = (reader["cdoc_acl_nroautoriza"] != DBNull.Value) ? (string)reader["cdoc_acl_nroautoriza"] : null;
            this.observacionfac= (reader["cdoc_observeaciones"] != DBNull.Value) ? (string)reader["cdoc_observeaciones"] : null;
            this.formapagofac = (reader["cdoc_formapago"] != DBNull.Value) ? (string)reader["cdoc_formapago"] : null;
            this.retid = (reader["rtd_id"] != DBNull.Value) ? (string)reader["rtd_id"] : null;
            this.doctranret = (reader["doctranret"] != DBNull.Value) ? (string)reader["doctranret"] : null;
            this.razon = (reader["per_razon"] != DBNull.Value) ? (string)reader["per_razon"] : null;
            this.ruc = (reader["per_ciruc"] != DBNull.Value) ? (string)reader["per_ciruc"] : null;
            this.tipoid = (reader["per_tipoid"] != DBNull.Value) ? (string)reader["per_tipoid"] : null;

            this.codsustentoats = "02"; //(this.tipoid.Substring(0, 1).ToUpper() == "R") ? "01" : "02";
            this.tipoidats = this.ruc.Length == 13 ? "01" : "02"; //(this.tipoid.Substring(0, 1).ToUpper() == "R") ? "01" : "02";

            
            switch (this.tipodoc)
            {
                case 14:
                    this.tipocomprobanteats = "01";
                    break;
                case 26:
                    this.tipocomprobanteats = "03";
                    break;
                case 24:
                    this.tipocomprobanteats = "04";
                    break;

            }

            
            if (!string.IsNullOrEmpty(this.retid) && this.tipodoc!=24)//Esto no lo hace para notas de credito proveedor
                this.tipocomprobanteats = this.retid;

            //this.tipocomprobanteats = this.ruc.Length == 13 ? "01" : "03"; //(this.tipoid.Substring(0, 1).ToUpper() == "R") ? "01" : "02";

            this.retiva = (reader["retiva"] != DBNull.Value) ? (Decimal?)reader["retiva"] : null;
            this.retiva10 = (reader["retiva10"] != DBNull.Value) ? (Decimal?)reader["retiva10"] : null;
            this.retiva20 = (reader["retiva20"] != DBNull.Value) ? (Decimal?)reader["retiva20"] : null;
            this.retiva30 = (reader["retiva30"] != DBNull.Value) ? (Decimal?)reader["retiva30"] : null;
            this.retiva50 = (reader["retiva50"] != DBNull.Value) ? (Decimal?)reader["retiva50"] : null;
            this.retiva70 = (reader["retiva70"] != DBNull.Value) ? (Decimal?)reader["retiva70"] : null;
            this.retiva100 = (reader["retiva100"] != DBNull.Value) ? (Decimal?)reader["retiva100"] : null;
            this.retfue = (reader["retfue"] != DBNull.Value) ? (Decimal?)reader["retfue"] : null;

            this.fecharet = (reader["fecharet"] != DBNull.Value) ? (DateTime?)reader["fecharet"] : null;
            if (!string.IsNullOrEmpty(doctranret))
            {
                string[] arraynumeroret = doctranret.Split('-');
                if (arraynumeroret.Length > 3)
                {
                    this.almacenret = arraynumeroret[1];
                    this.pventaret = arraynumeroret[2];
                    this.numeroret = arraynumeroret[3];
                    this.autorizacionret = "000000000";
                }
            }



            if (tipodoc==24)//Nota de credito proveedor
            {
                string[] arraynumeroncp = observacionfac.Split('-');

                this.docmod = "01";
                this.almacenmod= arraynumeroncp[0];
                this.pventamod = arraynumeroncp[1];
                this.numeromod = arraynumeroncp[2];
                //this.numeromod = observacionfac;
                this.autorizacionmod = autorizacionfac;
            }
            


        }

        public string GetSQLALL()
        {

            string sql = " SELECT  DISTINCT " +

                "o.com_codigo, o.com_fecha, o.com_doctran, o.com_tipodoc, " +
                " ot.tot_total, ot.tot_subtot_0, ot.tot_subtotal," +
                "ot.tot_timpuesto, ot.tot_ice, ot.tot_tseguro, ot.tot_transporte, " +
                "r.com_fecha as fecharet, r.com_doctran as doctranret, "+
                "oc.cdoc_aut_factura, oc.cdoc_acl_nroautoriza, oc.cdoc_aut_fecha, oc.cdoc_observeaciones, oc.cdoc_formapago, rd.rtd_id," +
                "per_razon,per_ciruc, per_tipoid, " +
                "SUM(case when imp_iva = 1 then drt_total else 0 end) as retiva," +
                "SUM(case when imp_ret = 1 then drt_total else 0 end)as retfue, " +
                "SUM(case when imp_iva = 1 and imp_porcentaje=10 then drt_total else 0 end) as retiva10, " +
                "SUM(case when imp_iva = 1 and imp_porcentaje=20 then drt_total else 0 end) as retiva20, " +
                "SUM(case when imp_iva = 1 and imp_porcentaje=30 then drt_total else 0 end) as retiva30, " +
                "SUM(case when imp_iva = 1 and imp_porcentaje=50 then drt_total else 0 end) as retiva50, " +
                "SUM(case when imp_iva = 1 and imp_porcentaje=70 then drt_total else 0 end) as retiva70, " +
                "SUM(case when imp_iva = 1 and imp_porcentaje=100 then drt_total else 0 end) as retiva100 " +               
                " FROM total ot " +
                "INNER join comprobante o ON o.com_empresa = ot.tot_empresa and o.com_codigo = ot.tot_comprobante " +
                "LEFT join persona  ON o.com_empresa = per_empresa and o.com_codclipro = per_codigo " +
                "LEFT join ccomdoc oc ON oc.cdoc_comprobante = o.com_codigo and oc.cdoc_empresa = o.com_empresa " +
                "LEFT join retdato rd ON rd.rtd_empresa = oc.cdoc_empresa and rd.rtd_tablacoa =  oc.cdoc_acl_tablacoa and rd.rtd_codigo =  oc.cdoc_acl_retdato " +
                "LEFT join ccomdoc rc ON rc.cdoc_factura = o.com_codigo and rc.cdoc_empresa = o.com_empresa " +
                "LEFT join comprobante r ON rc.cdoc_empresa = r.com_empresa and rc.cdoc_comprobante = r.com_codigo and r.com_estado = 2 " +
                "LEFT join dretencion ON drt_empresa = r.com_empresa and drt_comprobante = r.com_codigo " +
                "LEFT join impuesto ON drt_empresa = imp_empresa and drt_impuesto = imp_codigo " +
                " GROUP BY " +
                "o.com_codigo, o.com_fecha, o.com_periodo, o.com_mes, o.com_doctran, ot.tot_total, ot.tot_subtot_0, ot.tot_subtotal, ot.tot_timpuesto, ot.tot_ice, ot.tot_tseguro, ot.tot_transporte, r.com_fecha, r.com_doctran, o.com_estado, o.com_tipodoc, oc.cdoc_aut_factura, oc.cdoc_acl_nroautoriza, oc.cdoc_aut_fecha,oc.cdoc_observeaciones, oc.cdoc_formapago, rd.rtd_id,per_razon,per_ciruc,per_tipoid,drt_comprobante,o.com_almacen, o.com_pventa, r.com_estado,o.com_empresa  " +
                 "    %whereclause%  %orderby%  ";













             return sql;

        }

        public List<vCompras> GetStruc()
        {
            return new List<vCompras>();
        }
        public PropertyInfo[] GetProperties()
        {
            return this.GetType().GetProperties();
        }
    }
}
