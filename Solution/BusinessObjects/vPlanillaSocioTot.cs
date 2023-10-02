using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vPlanillaSocioTot
    {
        public int? com_empresa { get; set; }
        public Int64? com_codigo { get; set; }
        public string com_doctran { get; set; }
        public DateTime? com_fecha { get; set; }

        public int? per_codigo { get; set; }
        public string per_ciruc{ get; set; }
        public string per_razon{ get; set; }

        public decimal? tot_total { get; set; }

        public Int64? registros { get; set; }
        public decimal? cancelaciones { get; set; }
        public decimal? ingresos { get; set; }
        public decimal? egresos { get; set; }

        public vPlanillaSocioTot()
        {

        }

        public vPlanillaSocioTot(IDataReader reader, int tipo)
        {
            if (tipo == 1)
            {
                this.com_empresa = (reader["com_empresa"] != DBNull.Value) ? (int?)reader["com_empresa"] : null;
                this.com_codigo = (reader["com_codigo"] != DBNull.Value) ? (Int64?)reader["com_codigo"] : null;
                this.com_doctran = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
                this.com_fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;

                this.per_codigo = (reader["per_codigo"] != DBNull.Value) ? (int?)reader["per_codigo"] : null;
                this.per_ciruc = (reader["per_ciruc"] != DBNull.Value) ? (string)reader["per_ciruc"] : null;
                this.per_razon = (reader["per_razon"] != DBNull.Value) ? (string)reader["per_razon"] : null;

                this.tot_total = (reader["tot_total"] != DBNull.Value) ? (decimal?)reader["tot_total"] : null;
                this.registros = (reader["registros"] != DBNull.Value) ? (Int64?)reader["registros"] : null;
                this.cancelaciones = (reader["cancelaciones"] != DBNull.Value) ? (decimal?)reader["cancelaciones"] : null;
            }
            else
            {
                this.com_empresa = (reader["com_empresa"] != DBNull.Value) ? (int?)reader["com_empresa"] : null;
                this.com_codigo = (reader["com_codigo"] != DBNull.Value) ? (Int64?)reader["com_codigo"] : null;
                this.ingresos = (reader["ingresos"] != DBNull.Value) ? (decimal?)reader["ingresos"] : null;
                this.egresos = (reader["egresos"] != DBNull.Value) ? (decimal?)reader["egresos"] : null;
            }

        }

        public string getSQL()
        {
            string sql = " select com_empresa, com_codigo, com_doctran, com_fecha, tot_total, per_codigo,per_ciruc, per_razon,  " +
                        " sum(dca_monto_pla) cancelaciones, count(dca_comprobante) registros " +
                        " from comprobante " +
                        " left join dcancelacion on dca_empresa = com_empresa and dca_planilla = com_codigo " +
                        " left join total on tot_empresa = com_empresa and tot_comprobante = com_codigo " +
                        " left join persona on per_empresa = com_empresa and per_codigo = com_codclipro  " +
                        "   %whereclause% " +
                        "group by com_empresa,com_codigo, com_doctran, com_fecha, tot_total,per_codigo, per_ciruc, per_razon";
            return sql;


        }
        public string getSQLRub()
        {
            string sql = " select com_empresa, com_codigo, " + 
                        " sum(CASE WHEN rub_tipo = 'I' THEN rpl_valor ELSE  0 END) as ingresos, "+
                        " sum(CASE WHEN rub_tipo = 'E' THEN rpl_valor ELSE  0 END) as egresos " +
                        " from comprobante " +
                        " left join rubrosplanilla on rpl_empresa = com_empresa and rpl_comprobante = com_codigo " +
                        " left join rubro on rub_empresa = rpl_empresa  and rub_codigo = rpl_rubro"+
                        "   %whereclause% " +
                        "group by com_empresa, com_codigo";

            return sql;



        }
        public List<vPlanillaSocioTot> GetStruc()
        {
            return new List<vPlanillaSocioTot>();
        }



    }
}
