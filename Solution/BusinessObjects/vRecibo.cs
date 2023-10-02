using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vRecibo
    {
        public Int64? codigocabecera { get; set; }
        public string doctrancabecera { get; set; }
        public DateTime? fechacabecera { get; set; }
        public string com_concepto { get; set; }
        public Int32? com_numero { get; set; }
        public string per_nombres { get; set; }
        public string per_apellidos { get; set; }
        public string dca_doctran { get; set; }
        public Int32? dca_pago { get; set; }
        public decimal? dca_monto { get; set; }
        public decimal? ddo_monto { get; set; }

        


        #region Constructors


        public vRecibo()
        {

        }



        public vRecibo(IDataReader reader)
        {
            this.codigocabecera = (reader["codigocabecera"] != DBNull.Value) ? (Int64?)reader["codigocabecera"] : null;          
            this.doctrancabecera = (reader["doctrancabecera"] != DBNull.Value) ? (string)reader["doctrancabecera"] : null;
            this.fechacabecera = (reader["fechacabecera"] != DBNull.Value) ? (DateTime?)reader["fechacabecera"] : null;
            this.per_apellidos = (reader["per_apellidos"] != DBNull.Value) ? (string)reader["per_apellidos"] : null;
            this.per_nombres = (reader["per_nombres"] != DBNull.Value) ? (string)reader["per_nombres"] : null;
            this.com_numero = (reader["com_numero"] != DBNull.Value) ? (Int32?)reader["com_numero"] : null;
            this.com_concepto = (reader["com_concepto"] != DBNull.Value) ? (string)reader["com_concepto"] : null;
            this.dca_monto = (reader["dca_monto"] != DBNull.Value) ? (decimal?)reader["dca_monto"] : null;
            this.ddo_monto = (reader["ddo_monto"] != DBNull.Value) ? (decimal?)reader["ddo_monto"] : null;
            this.dca_pago = (reader["dca_pago"] != DBNull.Value) ? (Int32?)reader["dca_pago"] : null;
             this.dca_doctran = (reader["dca_doctran"] != DBNull.Value) ? (string)reader["dca_doctran"] : null;
           
        }

        #endregion

        public string GetSQL()
        {
            string sql = "select     "+
                            "comprobante.com_codigo codigocabecera,"+
                            "comprobante.com_doctran  doctrancabecera,"+
                            "comprobante.com_fecha		fechacabecera,"+
                            "comprobante.com_numero,"+
                            "comprobante.com_concepto,"+
                            "per_nombres,"+
                            "per_apellidos,"+
                            "dca_doctran,"+
                            "dca_pago,"+
                            "dca_monto,"+
                            "ddo_monto "+
                        "from dcancelacion "+
                        "inner join comprobante on com_codigo=dca_comprobante_can  and  com_empresa=dca_empresa "+
                        "inner join persona on per_empresa=dca_empresa  and  per_codigo=com_codclipro "+
                        "left join ddocumento on ddo_empresa=com_empresa and ddo_comprobante=com_codigo ";
            return sql;
        }


        public List<vRecibo> GetStruc()
        {
            return new List<vRecibo>();
        }


    }
}
