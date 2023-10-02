using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vDEstadoCuenta
    {

        public Int32? com_empresa { get; set; }
        public Int64?com_codigo{ get; set; }
        public DateTime? com_fecha { get; set; }
        public string com_doctran   { get; set; }
        public Int32? com_tipodoc { get; set; }
        public Int32? ddo_codclipro { get; set; }
        public DateTime? ddo_fecha_ven { get; set; }
        public string ddo_doctran{ get; set; }
        public Int32? ddo_pago{ get; set; }
        public Decimal? monto{ get; set; }
        public Int32? com_tclipro { get; set; }
        public Int32? ddo_debcre{ get; set; }
        public Decimal? saldo { get; set; }
        public string per_nombres { get; set; }
        public string per_apellidos { get; set; }
        public string alm_nombre { get; set; }
        public Int32? com_almacen { get; set; }
        public DateTime? ddo_fecha_emi   { get; set; }
        public Decimal? valor { get; set; }
        public Int64? codigo_can { get; set; }





        #region Constructors


        public vDEstadoCuenta()
        {

        }



        public vDEstadoCuenta(IDataReader reader)
        {   
            this.com_empresa = (reader["com_empresa"] != DBNull.Value) ? (Int32?)reader["com_empresa"] : null;
            this.com_codigo = (reader["com_codigo"] != DBNull.Value) ? (Int64?)reader["com_codigo"] : null;
            this.com_fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
            this.com_doctran = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
            this.com_tipodoc = (reader["com_tipodoc"] != DBNull.Value) ? (Int32?)reader["com_tipodoc"] : null;
            this.ddo_codclipro = (reader["ddo_codclipro"] != DBNull.Value) ? (Int32?)reader["ddo_codclipro"] : null;
            this.ddo_fecha_ven = (reader["ddo_fecha_ven"] != DBNull.Value) ? (DateTime?)reader["ddo_fecha_ven"] : null;
            this.ddo_doctran = (reader["ddo_doctran"] != DBNull.Value) ? (string)reader["ddo_doctran"] : null;
            this.ddo_pago = (reader["ddo_pago"] != DBNull.Value) ? (Int32?)reader["ddo_pago"] : null;
            this.monto = (reader["monto"] != DBNull.Value) ? (Decimal?)reader["monto"] : null;
           
            this.ddo_debcre = (reader["ddo_debcre"] != DBNull.Value) ? (Int32?)reader["ddo_debcre"] : null;
            this.saldo = (reader["saldo"] != DBNull.Value) ? (Decimal?)reader["saldo"] : null;            
            this.per_nombres = (reader["per_nombres"] != DBNull.Value) ? (string)reader["per_nombres"] : null;
            this.per_apellidos = (reader["per_apellidos"] != DBNull.Value) ? (string)reader["per_apellidos"] : null;
            this.alm_nombre = (reader["alm_nombre"] != DBNull.Value) ? (string)reader["alm_nombre"] : null;
            this.com_almacen = (reader["com_almacen"] != DBNull.Value) ? (Int32?)reader["com_almacen"] : null;
            this.ddo_fecha_emi = (reader["ddo_fecha_emi"] != DBNull.Value) ? (DateTime?)reader["ddo_fecha_emi"] : null;
            this.valor = (reader["valor"] != DBNull.Value) ? (Decimal?)reader["valor"] : null;
            this.com_tclipro = (reader["com_tclipro"] != DBNull.Value) ? (Int32?)reader["com_tclipro"] : null;
            //this.codigo_can = (reader["dca_comprobante"] != DBNull.Value) ? (Int64?)reader["dca_comprobante"] : null;



        }
        #endregion

        public string GetSQL()
        {
            string sql = "select * " +
                          "   from( " +
                          "  select com_codigo,com_empresa,com_fecha, com_tipodoc,per_nombres,per_apellidos,alm_nombre,com_doctran,ddo_codclipro,ddo_fecha_ven,ddo_doctran,ddo_pago,ddo_monto as monto ,ddo_debcre,  case when ddo_debcre =1 then ddo_monto-ddo_cancela else -(ddo_monto-ddo_cancela) END AS saldo,com_almacen,ddo_fecha_emi  ,  case when ddo_debcre =1 then ddo_monto else -ddo_monto END AS valor,com_tclipro from ddocumento" +
                          "  inner join comprobante on ddo_comprobante=com_codigo and  ddo_empresa=com_empresa " +
                          //"  inner join persona on com_codclipro=per_codigo and  per_empresa=com_empresa " +
                          "  inner join persona on ddo_codclipro=per_codigo and  per_empresa=ddo_empresa   "+
                          "  inner join almacen on alm_empresa=com_empresa and com_almacen=alm_codigo " +
                          "  union  " +
                          "  select com_codigo,com_empresa,com_fecha, com_tipodoc,per_nombres,per_apellidos,alm_nombre,com_doctran,ddo_codclipro,null,dca_doctran,dca_pago,dca_monto ,dca_debcre,null,com_almacen,ddo_fecha_emi , case when dca_debcre =1 then dca_monto else -dca_monto END,com_tclipro from dcancelacion    " +
                          "  inner join comprobante cancela on dca_comprobante_can=com_codigo and  dca_empresa=com_empresa " +
                          "  inner join ddocumento on ddo_empresa = dca_empresa and ddo_comprobante=dca_comprobante and ddo_transacc=dca_transacc and ddo_doctran=dca_doctran and ddo_pago=dca_pago " +
                          "  inner join almacen on alm_empresa=com_empresa and com_almacen=alm_codigo " +
                          //"  left join persona on com_codclipro=per_codigo and  per_empresa=com_empresa   " +
                          "  left join persona on ddo_codclipro=per_codigo and  per_empresa=ddo_empresa   " +
                       ")as t  ";
                    
            return sql;
        }

        public string GetSQLC()
        {
            string sql = " select com_codigo,com_empresa,com_fecha, com_tipodoc,per_nombres,per_apellidos,alm_nombre,com_doctran,ddo_codclipro,null as dca_comprobante, ddo_fecha_ven,ddo_doctran,ddo_pago,ddo_monto as monto ,ddo_debcre,  case when ddo_debcre =1 then ddo_monto-ddo_cancela else -(ddo_monto-ddo_cancela) END AS saldo,com_almacen,ddo_fecha_emi  ,  case when ddo_debcre =1 then ddo_monto else -ddo_monto END AS valor,com_tclipro from ddocumento " +
                         " inner join comprobante on ddo_comprobante=com_codigo and  ddo_empresa=com_empresa "+
                         " inner join persona on ddo_codclipro=per_codigo and  per_empresa=ddo_empresa " +
                         " inner join almacen on alm_empresa=com_empresa and com_almacen=alm_codigo ";
                     






            return sql;



        }

        public string GetSqlCa()
        {

            string sql = " select com_codigo,com_empresa,com_fecha, com_tipodoc,per_nombres,per_apellidos,null as ddo_fecha_ven,null as ddo_doctran, null as saldo,  alm_nombre,com_doctran,ddo_codclipro,dca_doctran,dca_comprobante,dca_pago as  ddo_pago,dca_monto as monto,dca_debcre as ddo_debcre,com_almacen,ddo_fecha_emi , case when dca_debcre =1 then dca_monto else -dca_monto END as valor,com_tclipro from dcancelacion   " +
                         " inner join comprobante cancela on dca_comprobante_can=com_codigo and  dca_empresa=com_empresa  " +
                         " inner join ddocumento on ddo_empresa = dca_empresa and ddo_comprobante=dca_comprobante and ddo_transacc=dca_transacc and ddo_doctran=dca_doctran and ddo_pago=dca_pago "+
                         " inner join almacen on alm_empresa=com_empresa and com_almacen=alm_codigo " +
                         " left join persona on ddo_codclipro=per_codigo and  per_empresa=ddo_empresa ";






            return sql;

        }




        public List<vDEstadoCuenta> GetStruc()
        {
            return new List<vDEstadoCuenta>();
        }


    }
}
