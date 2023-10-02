using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vDretencion
    {
        public Int64? codigocabecera { get; set; }
        public string doctrancabecera { get; set; }
        public DateTime? fechacabecera { get; set; }
        public string per_apellidos { get; set; }
        public string per_nombres { get; set; }
        public string per_direccion { get; set; }
        public string per_ciruc { get; set; }
        public Int32? com_periodo { get; set; }
        public decimal? drt_valor { get; set; }
        public decimal? drt_total { get; set; }
        public string drt_factura { get; set; }
        public decimal? drt_porcentaje { get; set; }
        public string imp_id { get; set; }
        public string imp_nombre { get; set; }
        public Int32? imp_codigo { get; set; }
        public Int32? com_numero { get; set; }
        

        #region Constructors


        public vDretencion()
        {

        }



        public vDretencion(IDataReader reader)
        {
            this.codigocabecera = (reader["codigocabecera"] != DBNull.Value) ? (Int64?)reader["codigocabecera"] : null;          
            this.doctrancabecera = (reader["doctrancabecera"] != DBNull.Value) ? (string)reader["doctrancabecera"] : null;
            this.fechacabecera = (reader["fechacabecera"] != DBNull.Value) ? (DateTime?)reader["fechacabecera"] : null;
            this.per_apellidos = (reader["per_apellidos"] != DBNull.Value) ? (string)reader["per_apellidos"] : null;
            this.per_nombres = (reader["per_nombres"] != DBNull.Value) ? (string)reader["per_nombres"] : null;
            this.per_direccion = (reader["per_direccion"] != DBNull.Value) ? (string)reader["per_direccion"] : null;
            this.per_ciruc = (reader["per_ciruc"] != DBNull.Value) ? (string)reader["per_ciruc"] : null;
            this.com_periodo = (reader["com_periodo"] != DBNull.Value) ? (Int32?)reader["com_periodo"] : null;
            this.com_numero = (reader["com_numero"] != DBNull.Value) ? (Int32?)reader["com_numero"] : null;
            this.drt_factura = (reader["drt_factura"] != DBNull.Value) ? (string)reader["drt_factura"] : null;
            this.imp_id= (reader["imp_id"] != DBNull.Value) ? (string)reader["imp_id"] : null;
            this.imp_nombre = (reader["imp_nombre"] != DBNull.Value) ? (string)reader["imp_nombre"] : null;
            this.imp_codigo = (reader["imp_codigo"] != DBNull.Value) ? (Int32?)reader["imp_codigo"] : null;
            this.drt_valor = (reader["drt_valor"] != DBNull.Value) ? (decimal?)reader["drt_valor"] : null;
            this.drt_total = (reader["drt_total"] != DBNull.Value) ? (decimal?)reader["drt_total"] : null;
            this.drt_porcentaje = (reader["drt_porcentaje"] != DBNull.Value) ? (decimal?)reader["drt_porcentaje"] : null;
        }

        #endregion

        public string GetSQL()
        {
            string sql = "select " +
                            "com_codigo codigocabecera," +
                            "com_doctran  doctrancabecera," +
                            "com_fecha		fechacabecera," +
                            "com_numero," +
                            "per_apellidos, " +
                            "per_nombres, " +
                            "per_direccion, " +
                            "per_ciruc, " +
                            "com_periodo," +
                            "drt_valor," +
                            "drt_total," +
                            "drt_factura," +
                            "drt_porcentaje," +
                            "imp_id," +
                            "imp_nombre," +
                            "imp_codigo " +
                 "from dretencion   " +
                 "inner join comprobante on com_codigo =dretencion.drt_comprobante  and  com_empresa =dretencion.drt_empresa " +
                 "inner join persona  on per_codigo=com_codclipro and per_empresa =com_empresa " +
                 "inner join impuesto  on drt_impuesto=imp_codigo and drt_empresa =imp_empresa ";


            return sql;
        }
        

        public List<vDretencion> GetStruc()
        {
            return new List<vDretencion>();
        }


    }
}
