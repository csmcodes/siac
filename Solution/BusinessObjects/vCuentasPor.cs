using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public class vCuentasPor
    {
        public Int64? com_codigo { get; set; }
        public DateTime? com_fecha { get; set; }
        public string com_doctran { get; set; }

        public Int64? com_cancela { get; set; }
        //public Int32? com_tipodoc { get; set; }

        public Int32? com_codclipro { get; set; }
        public DateTime? fechaven { get; set; }
        public Decimal? monto { get; set; }
        public Decimal? cancela { get; set; }
        public Decimal? saldo { get; set; }
        public string per_ciruc{ get; set; }
        public string per_razon { get; set; }
        public string entrega { get; set; }

        public int uso { get; set; }

        public int diasvence { get; set; }
        
        public decimal? monto1 { get; set; }
        public decimal? monto31 { get; set; }
        public decimal? monto61 { get; set; }
        public decimal? monto91 { get; set; }
        public decimal? monto121 { get; set; }


        //public Int32? debcre { get; set; }


        #region Constructors


        public vCuentasPor()
        {

        }



        public vCuentasPor(IDataReader reader)
        {
            this.com_codigo = (reader["com_codigo"] != DBNull.Value) ? (Int64?)reader["com_codigo"] : null;
            this.com_fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
            this.com_doctran = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
            //this.com_tipodoc = (reader["com_tipodoc"] != DBNull.Value) ? (Int32?)reader["com_tipodoc"] : null;
            this.com_codclipro = (reader["com_codclipro"] != DBNull.Value) ? (Int32?)reader["com_codclipro"] : null;
            this.per_ciruc= (reader["per_ciruc"] != DBNull.Value) ? (string)reader["per_ciruc"] : null;
            this.per_razon = (reader["per_razon"] != DBNull.Value) ? (string)reader["per_razon"] : null;
            this.monto = (reader["monto"] != DBNull.Value) ? (Decimal?)reader["monto"] : null;
            this.cancela = (reader["cancela"] != DBNull.Value) ? (Decimal?)reader["cancela"] : null;
            this.saldo = (reader["saldo"] != DBNull.Value) ? (Decimal?)reader["saldo"] : null;
            this.entrega = (reader["entrega"] != DBNull.Value) ? (string)reader["entrega"] : null;
            this.fechaven = (reader["fechaven"] != DBNull.Value) ? (DateTime?)reader["fechaven"] : null;
        }


        public vCuentasPor(IDataReader reader, bool doc)
        {
            if (doc)
            {
                this.com_codigo = (reader["com_codigo"] != DBNull.Value) ? (Int64?)reader["com_codigo"] : null;
                this.com_fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
                this.com_doctran = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
                //this.com_tipodoc = (reader["com_tipodoc"] != DBNull.Value) ? (Int32?)reader["com_tipodoc"] : null;

                this.com_codclipro = (reader["com_codclipro"] != DBNull.Value) ? (Int32?)reader["com_codclipro"] : null;
                this.per_ciruc = (reader["per_ciruc"] != DBNull.Value) ? (string)reader["per_ciruc"] : null;
                this.per_razon = (reader["per_razon"] != DBNull.Value) ? (string)reader["per_razon"] : null;
                this.monto = (reader["monto"] != DBNull.Value) ? (Decimal?)reader["monto"] : null;                
                //this.debcre = (reader["debcre"] != DBNull.Value) ? (Int32?)reader["debcre"] : null;
                //this.cancela = (reader["cancela"] != DBNull.Value) ? (Decimal?)reader["cancela"] : null;
                //this.saldo = (reader["saldo"] != DBNull.Value) ? (Decimal?)reader["saldo"] : null;
            }
            else
            {
                this.com_codigo = (reader["dca_comprobante"] != DBNull.Value) ? (Int64?)reader["dca_comprobante"] : null;
                this.cancela = (reader["cancela"] != DBNull.Value) ? (Decimal?)reader["cancela"] : null;
                this.com_cancela = (reader["dca_comprobante_can"] != DBNull.Value) ? (Int64?)reader["dca_comprobante_can"] : null;
                //this.debcre = (reader["debcre"] != DBNull.Value) ? (Int32?)reader["debcre"] : null;
            }
        }
        #endregion

        public string GetSQL()
        {
            string sql = "select com_codigo, com_fecha, com_doctran,com_codclipro,per_ciruc, per_razon, sum(ddo_monto) as monto, sum(ddo_cancela) as cancela, sum(ddo_monto) - sum(ddo_cancela) as saldo " +
                         "   from comprobante " +
                         "       inner join ddocumento on ddo_empresa = com_empresa and ddo_comprobante = com_codigo " +
                         "       left join persona on com_codclipro = per_codigo " +
                         "   %whereclause% " +
                         "   group by com_codigo, com_fecha, com_doctran, com_codclipro,per_ciruc, per_razon " +
                         "   %having% " +
                         "   %orderby%";

            return sql;
        }


        public string GetSQL1()
        {
            string sql = "select com_codigo, com_fecha, com_doctran,com_codclipro,per_ciruc, per_razon, sum(ddo_monto) as monto, " +
                         " coalesce((select sum(dca_monto) from dcancelacion inner join comprobante can on dca_empresa = can.com_empresa and dca_comprobante_can = can.com_codigo  where dca_comprobante = ddo_comprobante and dca_transacc = ddo_transacc and dca_doctran = ddo_doctran %rango%), 0) as cancela, null as saldo " + 
                         " from ddocumento "+
                         "    inner join comprobante com on ddo_empresa = com.com_empresa and ddo_comprobante = com.com_codigo " +
                         "    left join persona on com_codclipro = per_codigo " +
                         "   %whereclause% " +
                         " group by com_codigo, com_fecha, com_doctran, com_codclipro,per_ciruc, per_razon, ddo_comprobante, ddo_transacc,ddo_doctran " + 
                         " having sum(ddo_monto) <> coalesce((select sum(dca_monto) from dcancelacion inner join comprobante can on dca_empresa = can.com_empresa and dca_comprobante_can = can.com_codigo where dca_comprobante = ddo_comprobante and dca_transacc = ddo_transacc and dca_doctran = ddo_doctran %rango%), 0) " +
                         " %orderby%";

            return sql;

        }


        public string GetSQLDoc()
        {
            string sql = " select com_codigo, com_fecha, com_doctran,com_codclipro,per_ciruc, per_nombres, per_apellidos, per_razon, " +
                         " sum(ddo_monto) as monto " +
                         " from ddocumento " +
                         "      inner join comprobante com on ddo_empresa = com.com_empresa and ddo_comprobante = com.com_codigo " +
                         "      left join persona on com_empresa = per_empresa and com_codclipro = per_codigo " +
                         "   %whereclause% " +
                         " group by com_codigo, com_fecha, com_doctran, com_codclipro,per_ciruc, per_nombres, per_apellidos, per_razon " +
                         "";
            return sql;
        }

        public string GetSQLCan()
        {
            string sql = " select dca_comprobante, sum(dca_monto) as cancela, null as  dca_comprobante_can" +
                         " from dcancelacion " +
                         "      inner join comprobante com on dca_empresa = com.com_empresa and dca_comprobante_can = com.com_codigo " +
                         "   %whereclause% " +
                         " group by dca_comprobante " +
                         "";             
            return sql;
        }

        public string GetSQLCanDet()
        {
            string sql = " select dca_comprobante, sum(dca_monto) as cancela, dca_comprobante_can " +
                         " from dcancelacion " +
                         "      inner join comprobante com on dca_empresa = com.com_empresa and dca_comprobante_can = com.com_codigo " +
                         "      inner join ddocumento on dca_empresa = ddo_empresa and dca_comprobante= ddo_comprobante and dca_transacc = ddo_transacc and dca_doctran = ddo_doctran and dca_pago = ddo_pago  " +
                         "   %whereclause% " +
                         " group by dca_comprobante, dca_comprobante_can " +
                         "";
            return sql;
        }


        public string GetSQLFull()
        {
            string sql = " select fac.com_codigo com_codigo, fac.com_fecha com_fecha, fac.com_doctran com_doctran, fac.com_codclipro com_codclipro, per_ciruc, per_nombres, per_apellidos, per_razon, max(cenv_direccion_des) entrega, " +
                         " (select max(ddo_fecha_ven) from ddocumento where ddo_empresa = fac.com_empresa and ddo_comprobante = fac.com_codigo) fechaven, " +
                         " (select sum(ddo_monto) from ddocumento where ddo_empresa = fac.com_empresa and ddo_comprobante = fac.com_codigo) monto, sum(COALESCE(dca_monto, 0)) cancela, (select sum(ddo_monto) from ddocumento where ddo_empresa = fac.com_empresa and ddo_comprobante = fac.com_codigo) - sum(COALESCE(dca_monto, 0)) saldo " +
                         " from comprobante fac " +
                         "        left join ccomdoc on fac.com_empresa = cdoc_empresa and fac.com_codigo = cdoc_comprobante " +
                         "        left join ccomenv on fac.com_empresa = cenv_empresa and fac.com_codigo = cenv_comprobante " +
                         "        left join persona on fac.com_empresa = per_empresa and fac.com_codclipro = per_codigo " +
                         "        left join ddocumento on ddo_empresa = fac.com_empresa and ddo_comprobante = fac.com_codigo " +
                         "        left join (dcancelacion  inner join comprobante can on dca_empresa = can.com_empresa and dca_comprobante_can = can.com_codigo and  can.com_estado = 2 and can.com_fecha <= @par0) on dca_empresa = ddo_empresa and dca_comprobante = ddo_comprobante and dca_transacc = ddo_transacc and dca_doctran = ddo_doctran and dca_pago = ddo_pago " +
                         " %whereclause% " +
                         " group by fac.com_empresa,fac.com_codigo, fac.com_fecha, fac.com_doctran, fac.com_codclipro,per_ciruc, per_nombres, per_apellidos, per_razon " +
                         " having (select sum(ddo_monto) from ddocumento where ddo_empresa = fac.com_empresa and ddo_comprobante = fac.com_codigo)-sum(COALESCE(dca_monto, 0)) <> 0 " +
                         "";

            return sql;
        }



        public List<vCuentasPor> GetStruc()
        {
            return new List<vCuentasPor>();
        }
    }
}
