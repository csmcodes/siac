using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
   public  class vObligacion
    {

       public Int64? com_codigo { set; get; }
       public DateTime? fecha { get; set; }
       public String com_doctran { get; set; }
       public Int32? com_codigocli { set; get; }
       public String com_concepto { get; set; }
       public String autorizacion { get; set; }
       public String factura { get; set; }
       public DateTime? cdoc_fecha { get; set; }


       public vObligacion()
       {


       }

       public vObligacion(IDataReader reader)
       {


           this.com_codigo = (reader["com_codigo"] != DBNull.Value) ? (Int64?)reader["com_codigo"] : null;
           this.fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
           this.com_doctran = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
           this.com_codigocli = (reader["com_codclipro"] != DBNull.Value) ? (Int32?)reader["com_codclipro"] : null;
           this.com_concepto = (reader["com_concepto"] != DBNull.Value) ? (string)reader["com_concepto"] : null;
           this.autorizacion = (reader["cdoc_acl_nroautoriza"] != DBNull.Value) ? (string)reader["cdoc_acl_nroautoriza"] : null;
           this.factura = (reader["cdoc_aut_factura"] != DBNull.Value) ? (string)reader["cdoc_aut_factura"] : null;
           this.cdoc_fecha = (reader["cdoc_aut_fecha"] != DBNull.Value) ? (DateTime?)reader["cdoc_aut_fecha"] : null;
       }

       public string GetSQLQALL()
       {


           string sql = "select " +
                          "com_codigo,"+
                          "com_fecha, "+
                          "com_doctran, "+
                          "com_codclipro, "+
                          "com_concepto, "+
                          "cdoc_acl_nroautoriza, "+
                          "cdoc_aut_factura, "+
                          "cdoc_aut_fecha "+
                          "from ccomdoc  "+
                          "inner join comprobante on cdoc_comprobante= com_codigo and cdoc_empresa= com_empresa"+
                          "";

           return sql;

       }
    }
}
