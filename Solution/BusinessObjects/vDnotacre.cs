using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vDnotacre
    {
        public Int64? codigocabecera { get; set; }
        public string doctrancabecera { get; set; }
        public DateTime? fechacabecera { get; set; }
        public string per_apellidos { get; set; }
        public string per_nombres { get; set; }
        public string per_direccion { get; set; }
        public string per_ciruc { get; set; }        
        public decimal? dnc_valor { get; set; }
        public Int32? com_numero { get; set; }
        public string com_concepto { get; set; }
        public string doctrandetalle { get; set; }
        public string tnc_nombre { get; set; }


        public decimal? tot_total { get; set; }
        public decimal? tot_subtotal { get; set; }
        public decimal? tot_subtot_0 { get; set; }
        public decimal? tot_timpuesto { get; set; }


        #region Constructors


        public vDnotacre()
        {

        }



        public vDnotacre(IDataReader reader)
        {
            this.codigocabecera = (reader["codigocabecera"] != DBNull.Value) ? (Int64?)reader["codigocabecera"] : null;          
            this.doctrancabecera = (reader["doctrancabecera"] != DBNull.Value) ? (string)reader["doctrancabecera"] : null;
            this.fechacabecera = (reader["fechacabecera"] != DBNull.Value) ? (DateTime?)reader["fechacabecera"] : null;
            this.per_apellidos = (reader["per_apellidos"] != DBNull.Value) ? (string)reader["per_apellidos"] : null;
            this.per_nombres = (reader["per_nombres"] != DBNull.Value) ? (string)reader["per_nombres"] : null;
            this.per_direccion = (reader["per_direccion"] != DBNull.Value) ? (string)reader["per_direccion"] : null;
            this.per_ciruc = (reader["per_ciruc"] != DBNull.Value) ? (string)reader["per_ciruc"] : null;            
            this.com_numero = (reader["com_numero"] != DBNull.Value) ? (Int32?)reader["com_numero"] : null;
            this.dnc_valor = (reader["dnc_valor"] != DBNull.Value) ? (decimal?)reader["dnc_valor"] : null;
            this.com_concepto = (reader["com_concepto"] != DBNull.Value) ? (string)reader["com_concepto"] : null;
            this.doctrandetalle = (reader["doctrandetalle"] != DBNull.Value) ? (string)reader["doctrandetalle"] : null;
            this.tnc_nombre = (reader["tnc_nombre"] != DBNull.Value) ? (string)reader["tnc_nombre"] : null;

            this.tot_total = (reader["tot_total"] != DBNull.Value) ? (decimal?)reader["tot_total"] : null;
            this.tot_subtotal = (reader["tot_subtotal"] != DBNull.Value) ? (decimal?)reader["tot_subtotal"] : null;
            this.tot_subtot_0 = (reader["tot_subtot_0"] != DBNull.Value) ? (decimal?)reader["tot_subtot_0"] : null;
            this.tot_timpuesto = (reader["tot_timpuesto"] != DBNull.Value) ? (decimal?)reader["tot_timpuesto"] : null;

        }

        #endregion

        public string GetSQL()
        {
            string sql = "select     "+
                            "comprobante.com_codigo codigocabecera,     "+
                            "comprobante.com_doctran  doctrancabecera,     "+
                            "comprobante.com_fecha		fechacabecera,     "+
                            "comprobante.com_numero,    "+
                            "comprobante.com_concepto,    "+
                            "per_apellidos,      "+
                            "per_nombres,      "+
                            "per_direccion,      "+
                            "per_ciruc,     "+
                            "dnc_valor,     "+
                            "detalle.com_doctran doctrandetalle,  "+
                            "tnc_nombre, "+ 
                            "tot_total, "+
		                    "tot_subtotal,   "+
		                    "tot_subtot_0, "+
		                    "tot_timpuesto "+
                        "from dnotacre      "+
                        "inner join comprobante on comprobante.com_codigo =dnotacre.dnc_comprobante  and  comprobante.com_empresa =dnotacre.dnc_empresa        "+
                        "inner join persona  on per_codigo=com_codclipro and per_empresa =comprobante.com_empresa        "+
                        "inner join ccomdoc  on cdoc_comprobante= comprobante.com_codigo and        comprobante.com_empresa =cdoc_empresa                              "+
                        "inner join comprobante detalle on detalle.com_codigo =cdoc_factura  and  detalle.com_empresa =dnotacre.dnc_empresa  "+
                        "inner join tiponc on tnc_codigo =dnc_tiponc  and  tnc_empresa =dnotacre.dnc_empresa "+   
                        "inner join total on dnc_comprobante=tot_comprobante  and  dnc_empresa=tot_empresa";
            return sql;
        }
        

        public List<vDnotacre> GetStruc()
        {
            return new List<vDnotacre>();
        }


    }
}
