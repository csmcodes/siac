using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace BusinessObjects
{
    public class vVenta
    {
        public Int64? codigo { get; set; }
        public DateTime? fecha { get; set; }
        public string doctran { get; set; }
        public Int32? tipodoc { get; set; }
        public string tipoats { get; set; }

        public string tipoid { get; set; }        
        public string ruc { get; set; }
        public string razon { get; set; }
        public string parterel { get; set; }

        public Decimal? total { get; set; }
        public Decimal? subtotal { get; set; }
        public Decimal? subimpuesto { get; set; }
        public Decimal? ice { get; set; }
        public Decimal? impuesto { get; set; }
        public Decimal? seguro { get; set; }
        public Decimal? transporte { get; set; }

        public Decimal? monto { get; set; }
        public Decimal? retiva { get; set; }
        public Decimal? retfue { get; set; }

        public Decimal? desc0 { get; set; }
        

        public int comprobantes { get; set; }


          #region Constructors


        public vVenta()
        {

        }



        public vVenta(IDataReader reader)
        {
            this.codigo = (reader["com_codigo"] != DBNull.Value) ? (Int64?)reader["com_codigo"] : null;
            this.fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
            this.doctran = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
            this.tipodoc= (reader["com_tipodoc"] != DBNull.Value) ? (Int32?)reader["com_tipodoc"] : null;
            this.tipoats = (this.tipodoc == 17) ? "04" : "370"; //"18"--> BRYSEAR
            this.tipoid = (reader["per_tipoid"] != DBNull.Value) ? (string)reader["per_tipoid"] : null;
            this.ruc = (reader["per_ciruc"] != DBNull.Value) ? (string)reader["per_ciruc"] : null;
            this.razon = (reader["per_razon"] != DBNull.Value) ? (string)reader["per_razon"] : null;

            this.total = (reader["tot_total"] != DBNull.Value) ? (Decimal?)reader["tot_total"] : null;
            this.subtotal = (reader["tot_subtot_0"] != DBNull.Value) ? (Decimal?)reader["tot_subtot_0"] : null;
            this.subimpuesto = (reader["tot_subtotal"] != DBNull.Value) ? (Decimal?)reader["tot_subtotal"] : null;
            this.seguro = (reader["tot_tseguro"] != DBNull.Value) ? (Decimal?)reader["tot_tseguro"] : null;
            this.ice= (reader["tot_ice"] != DBNull.Value) ? (Decimal?)reader["tot_ice"] : null;
            this.impuesto = (reader["tot_timpuesto"] != DBNull.Value) ? (Decimal?)reader["tot_timpuesto"] : null;
            this.transporte = (reader["tot_transporte"] != DBNull.Value) ? (Decimal?)reader["tot_transporte"] : null;
            this.desc0= (reader["tot_desc1_0"] != DBNull.Value) ? (Decimal?)reader["tot_desc1_0"] : null;

            this.monto = (reader["monto"] != DBNull.Value) ? (Decimal?)reader["monto"] : null;
            this.retiva = (reader["retiva"] != DBNull.Value) ? (Decimal?)reader["retiva"] : null;
            this.retfue = (reader["retfue"] != DBNull.Value) ? (Decimal?)reader["retfue"] : null;


            

        }

        #endregion

        public string GetSQLALL()
        {
            string sql = "SELECT f.com_codigo, f.com_tipodoc, f.com_fecha, f.com_doctran, ft.tot_total, ft.tot_subtot_0, ft.tot_subtotal, ft.tot_timpuesto, ft.tot_ice, ft.tot_tseguro, ft.tot_transporte, ft.tot_desc1_0, fp.per_ciruc, fp.per_tipoid, fp.per_razon," +
                         //"   SUM(dfp_monto) as monto, " +
                         //"   SUM(case when tpa_iva = 1 then dfp_monto else 0 end) as retiva, " +
                         //"   SUM(case when tpa_ret = 1 then dfp_monto else 0 end)as retfue " +
                         "   SUM(dca_monto) as monto, " +
                         "   SUM(case when tpa_iva = 1 then dca_monto else 0 end) as retiva, " +
                         "   SUM(case when tpa_ret = 1 then dca_monto else 0 end)as retfue " +
                         "   FROM comprobante f " +
	                     "       LEFT JOIN total ft ON ft.tot_empresa = f.com_empresa and ft.tot_comprobante = f.com_codigo " +
                         "       LEFT JOIN persona fp ON fp.per_empresa = f.com_empresa and fp.per_codigo = f.com_codclipro " +
	                     "       LEFT JOIN ccomdoc ON cdoc_empresa= f.com_empresa and cdoc_comprobante = f.com_codigo " +
	                     //"       LEFT JOIN dcancelacion ON dca_empresa = ddo_empresa and dca_comprobante = ddo_comprobante and dca_transacc = ddo_transacc and dca_doctran = ddo_doctran and dca_pago=ddo_pago " +
                         "       LEFT JOIN dcancelacion ON dca_empresa = f.com_empresa and dca_comprobante = f.com_codigo and dca_transacc = f.com_transacc and dca_doctran = f.com_doctran "+
                         "       LEFT JOIN comprobante r ON r.com_empresa = dca_empresa and r.com_codigo = dca_comprobante_can " +
	                     "       LEFT JOIN drecibo ON dfp_empresa =r.com_empresa and dfp_comprobante =r.com_codigo  " +
	                     "       LEFT JOIN tipopago ON tpa_empresa = dfp_empresa and tpa_codigo = dfp_tipopago " +
                         "   GROUP BY f.com_codigo, f.com_numero , f.com_fecha, f.com_periodo, f.com_mes, f.com_doctran, f.com_almacen, f.com_pventa, ft.tot_total, ft.tot_subtot_0, ft.tot_subtotal, ft.tot_timpuesto, ft.tot_ice, ft.tot_tseguro, ft.tot_transporte,ft.tot_desc1_0, fp.per_ciruc, fp.per_tipoid, fp.per_razon, f.com_tipodoc, f.com_estado, f.com_fecha,f.com_empresa " +
                         //"   HAVING " +
	                     //"       f.com_tipodoc = 4 and f.com_estado = 2 and f.com_fecha BETWEEN '01/08/2014' and '01/09/2014' " +
                         " %whereclause%   %orderby% ";

            return sql;
        }

        public string GetSQL1()
        {
            string sql = "SELECT f.com_codigo, f.com_tipodoc, f.com_fecha, f.com_doctran, ft.tot_total, ft.tot_subtot_0, ft.tot_subtotal, ft.tot_timpuesto, ft.tot_ice, ft.tot_tseguro, ft.tot_transporte, ft.tot_desc1_0, fp.per_ciruc, fp.per_tipoid,fp.per_razon, " +
                         "   0.0 as monto, " +
                         "   0.0 as retiva, " +
                         "   0.0 as retfue " +
                         "   FROM comprobante f " +
                         "       LEFT JOIN total ft ON ft.tot_empresa = f.com_empresa and ft.tot_comprobante = f.com_codigo " +
                         "       LEFT JOIN persona fp ON fp.per_empresa = f.com_empresa and fp.per_codigo = f.com_codclipro " +
                         "       LEFT JOIN ccomdoc ON cdoc_empresa= f.com_empresa and cdoc_comprobante = f.com_codigo " +
                         //"       LEFT JOIN ddocumento ON ddo_empresa = f.com_empresa and ddo_comprobante = f.com_codigo " +
                         //"       LEFT JOIN dcancelacion ON dca_empresa = ddo_empresa and dca_comprobante = ddo_comprobante and dca_transacc = ddo_transacc and dca_doctran = ddo_doctran and dca_pago=ddo_pago " +
                         //"       LEFT JOIN dcancelacion ON dca_empresa = f.com_empresa and dca_comprobante = f.com_codigo and dca_transacc = f.com_transacc and dca_doctran = f.com_doctran " +
                         //"       LEFT JOIN comprobante r ON r.com_empresa = dca_empresa and r.com_codigo = dca_comprobante_can " +
                         //"       LEFT JOIN drecibo ON dfp_empresa =r.com_empresa and dfp_comprobante =r.com_codigo  " +
                         //"       LEFT JOIN tipopago ON tpa_empresa = dfp_empresa and tpa_codigo = dfp_tipopago " +
                         //"   GROUP BY f.com_codigo, f.com_numero , f.com_fecha, f.com_periodo, f.com_mes, f.com_doctran, f.com_almacen, f.com_pventa, ft.tot_total, ft.tot_subtot_0, ft.tot_subtotal, ft.tot_timpuesto, ft.tot_ice, ft.tot_tseguro, ft.tot_transporte, fp.per_ciruc, fp.per_tipoid, f.com_tipodoc, f.com_estado, f.com_fecha,f.com_empresa " +
                         //"   HAVING " +
                         //"       f.com_tipodoc = 4 and f.com_estado = 2 and f.com_fecha BETWEEN '01/08/2014' and '01/09/2014' " +
                         "  ";

            return sql;
        }


        public List<vVenta> GetStruc()
        {
            return new List<vVenta>();
        }

        public PropertyInfo[] GetProperties()
        {
            return this.GetType().GetProperties();
        }    

    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   