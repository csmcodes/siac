using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
  public class vVentasRetenciones
    {

      public Int32? per_codigo { get; set; }
      public string per_ciruc { get; set; }
      public string per_razon { get; set; }
        public long? com_codigo { get; set; }
        public DateTime? com_fecha { get; set; }
      public string com_doctran { get; set; }
      public string nro_documento { get; set; }
      public Decimal? iva { get; set; }
      public Decimal? ret { get; set; }
      public string crea_user { get; set; }
        public string factura { get; set; }

        public vVentasRetenciones()
      {

      }


      public vVentasRetenciones(IDataReader reader)
      {

          this.per_codigo = (reader["per_codigo"] != DBNull.Value) ? (Int32?)reader["per_codigo"] : null;
          this.per_ciruc = (reader["per_ciruc"] != DBNull.Value) ? (string)reader["per_ciruc"] : null;
          this.per_razon = (reader["per_razon"] != DBNull.Value) ? (string)reader["per_razon"] : null;
            this.com_codigo= (reader["com_codigo"] != DBNull.Value) ? (long?)reader["com_codigo"] : null;
            this.com_fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
          this.com_doctran = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
          this.nro_documento = (reader["dfp_nro_documento"] != DBNull.Value) ? (string)reader["dfp_nro_documento"] : null;
          
          this.iva = (reader["iva"] != DBNull.Value) ? (Decimal?)reader["iva"] : null;
          this.ret = (reader["ret"] != DBNull.Value) ? (Decimal?)reader["ret"] : null;
          this.crea_user = (reader["crea_usr"] != DBNull.Value) ? (string)reader["crea_usr"] : null;
         

      }



      public string GetSQL()
      {

          string sql = " select per_codigo, per_ciruc, per_razon,com_codigo, com_fecha, com_doctran,dfp_nro_documento, " +

                       " CASE WHEN tpa_iva =1 THEN dfp_monto ELSE  0 END as iva, " +

                       " CASE WHEN tpa_ret =1 THEN dfp_monto ELSE  0 END as ret, comprobante.crea_usr " +
                       " from comprobante " +
                       " inner join drecibo on dfp_comprobante = com_codigo and dfp_empresa = com_empresa " +
                       " inner join tipopago on dfp_tipopago = tpa_codigo and dfp_empresa = tpa_empresa " +
                       " inner join persona on per_codigo = com_codclipro and per_empresa = com_empresa ";
                            
          return sql;



      }
      public List<vVentasRetenciones> GetStruc()
      {
          return new List<vVentasRetenciones>();
      }

          }
    
      }
  