using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace BusinessObjects
{
    public class vSaldo
    {
        public Int32? sal_empresa { get; set; }
        public Int32? sal_cuenta { get; set; }
        public Int32? sal_centro { get; set; }
        public Int32? sal_almacen { get; set; }
        public Int32? sal_transacc { get; set; }
        public Int32? sal_periodo { get; set; }
        public Int32? sal_mes { get; set; }
        public Decimal? sal_debito { get; set; }
        public Decimal? sal_credito { get; set; }
        public Decimal? sal_debext { get; set; }
        public Decimal? sal_creext { get; set; }


           #region Constructors


        public vSaldo()
        {

        }



        public vSaldo(IDataReader reader)
        {
            this.sal_empresa = (reader["sal_empresa"] != DBNull.Value) ? (Int32?)reader["sal_empresa"] : null;
            this.sal_cuenta = (reader["sal_cuenta"] != DBNull.Value) ? (Int32?)reader["sal_cuenta"] : null;
            this.sal_centro = (reader["sal_centro"] != DBNull.Value) ? (Int32?)reader["sal_centro"] : null;
            this.sal_almacen = (reader["sal_almacen"] != DBNull.Value) ? (Int32?)reader["sal_almacen"] : null;
            this.sal_transacc = (reader["sal_transacc"] != DBNull.Value) ? (Int32?)reader["sal_transacc"] : null;
            this.sal_periodo = (reader["sal_periodo"] != DBNull.Value) ? (Int32?)reader["sal_periodo"] : null;
            this.sal_mes = (reader["sal_mes"] != DBNull.Value) ? (Int32?)reader["sal_mes"] : null;
            this.sal_debito = (reader["sal_debito"] != DBNull.Value) ? (Decimal?)reader["sal_debito"] : null;
            this.sal_credito = (reader["sal_credito"] != DBNull.Value) ? (Decimal?)reader["sal_credito"] : null;
            this.sal_debext = (reader["sal_debext"] != DBNull.Value) ? (Decimal?)reader["sal_debext"] : null;
            this.sal_creext = (reader["sal_creext"] != DBNull.Value) ? (Decimal?)reader["sal_creext"] : null;           
        }

        #endregion

        public string GetSQLAll()
        {
            string sql = "select sal_empresa, sal_cuenta,sal_centro, sal_almacen, sal_transacc, null as sal_periodo, null as sal_mes, " +
                        " sum(sal_debito) as sal_debito, sum(sal_credito) as sal_credito, sum(sal_debext) as sal_debext, sum(sal_creext) as sal_creext" +
                        " from saldo inner join cuenta on sal_empresa = cue_empresa and sal_cuenta = cue_codigo " +
                        " %whereclause% " +
                        " group by sal_empresa, sal_cuenta,sal_centro, sal_almacen, sal_transacc " +
                        " ";            
            
            return sql;
        }


        public string GetSQLAll1()
        {
            string sql = "select sal_empresa, sal_cuenta, null as sal_centro, null as sal_almacen, null as sal_transacc, null as sal_periodo, null as sal_mes, " +
                        " sum(sal_debito) as sal_debito, sum(sal_credito) as sal_credito, sum(sal_debext) as sal_debext, sum(sal_creext) as sal_creext" +
                        " from saldo inner join cuenta on sal_empresa = cue_empresa and sal_cuenta = cue_codigo " +
                        " %whereclause% " +
                        " group by sal_empresa, sal_cuenta" +
                        " ";

            return sql;
        }


        public List<vSaldo> GetStruc()
        {
            return new List<vSaldo>();
        }

    }
}
