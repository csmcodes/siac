using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Functions;


namespace BusinessObjects
{
    public class vRetencionVenta
    {
        public Int64? comprobante { get; set; }
        public Int32? per_codigo { get; set; }
        public string per_ciruc { get; set; }
        public string per_razon { get; set; }
        public Decimal? iva { get; set; }
        public Decimal? ret { get; set; }

        public Int64? refcomprobante { get; set; }

        public vRetencionVenta()
        {

        }



        public vRetencionVenta(IDataReader reader)
        {
            this.comprobante = (reader["comprobante"] != DBNull.Value) ? (Int64?)reader["comprobante"] : null;
            this.per_codigo = (reader["per_codigo"] != DBNull.Value) ? (Int32?)reader["per_codigo"] : null;
            this.per_ciruc = (reader["per_ciruc"] != DBNull.Value) ? (string)reader["per_ciruc"] : null;
            this.per_razon = (reader["per_razon"] != DBNull.Value) ? (string)reader["per_razon"] : null;
            this.iva = (reader["iva"] != DBNull.Value) ? (Decimal?)reader["iva"] : null;
            this.ret = (reader["ret"] != DBNull.Value) ? (Decimal?)reader["ret"] : null;         
        }


        public string GetSQLALL()
        {

            string sql = "select " +
                         "    per_codigo, per_ciruc, per_razon, null as comprobante,  " +                         
                         "    sum(CASE WHEN tpa_iva =1 THEN dfp_monto ELSE  0 END) as iva, " +
                         "    sum(CASE WHEN tpa_ret =1 THEN dfp_monto ELSE  0 END) as ret " +
                         
                         "from comprobante  " +
                         "    inner join drecibo on dfp_comprobante = com_codigo and dfp_empresa = com_empresa " +
                         "    inner join tipopago on dfp_tipopago = tpa_codigo and dfp_empresa = tpa_empresa " +
                         "    inner join persona on per_codigo = com_codclipro and per_empresa = com_empresa " +
                         "where  " +
                         "    (tpa_iva=1 or tpa_ret = 1) " +
                         "group by " +
                         "    per_codigo, per_ciruc, per_razon, com_estado, com_periodo, com_mes, com_fecha " +
                         " %whereclause%  %orderby% ";
            //"having " +
            //"    com_estado = 2 " +
            //"    and com_periodo = 2014 " +
            //"    and com_mes = 9	 " +
            //"order by per_razon ";

            return sql;
        }


        public string GetSQLCom()
        {

            string sql = "select " +
                         "    per_codigo, per_ciruc, per_razon, com_codigo as comprobante,  " +                         
                         "    sum(CASE WHEN tpa_iva =1 THEN dfp_monto ELSE  0 END) as iva, " +
                         "    sum(CASE WHEN tpa_ret =1 THEN dfp_monto ELSE  0 END) as ret " +
                         "from comprobante  " +
                         "    inner join drecibo on dfp_comprobante = com_codigo and dfp_empresa = com_empresa " +
                         "    inner join tipopago on dfp_tipopago = tpa_codigo and dfp_empresa = tpa_empresa " +
                         "    inner join persona on per_codigo = com_codclipro and per_empresa = com_empresa " +
                         "where  " +
                         "    (tpa_iva=1 or tpa_ret = 1) " +
                         "group by " +
                         "    per_codigo, per_ciruc, per_razon, com_estado, com_fecha, com_codigo " +
                         " %whereclause%  %orderby% ";
            //"having " +
            //"    com_estado = 2 " +
            //"    and com_periodo = 2014 " +
            //"    and com_mes = 9	 " +
            //"order by per_razon ";

            return sql;
        }
        public List<vRetencionVenta> GetStruc()
        {
            return new List<vRetencionVenta>();
        }
    }
}
