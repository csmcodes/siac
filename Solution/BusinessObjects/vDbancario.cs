using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vDbancario
    {
        public Int64? codigocabecera { get; set; }
        public string doctrancabecera { get; set; }
        public DateTime? fechacabecera { get; set; }       
        public string com_concepto { get; set; }
        public Int32? com_numero { get; set; }
        public Int32? com_almacen { get; set; }        
        public Int32? dban_transacc { get; set; }
        public decimal? dban_valor_nac { get; set; }
        public decimal? dban_valor_ext { get; set; }

        public Int32? ban_codigo { get; set; }
        public string ban_nombre { get; set; }
        public string dban_beneficiario { get; set; }
        public string dban_documento { get; set; }
        public string ban_id { get; set; }
        public string cue_nombre { get; set; }
        public Int32? ban_numero { get; set; }
        
        public Int32? dban_debcre { get; set; }


        #region Constructors


        public vDbancario()
        {

        }



        public vDbancario(IDataReader reader)
        {
            this.codigocabecera = (reader["codigocabecera"] != DBNull.Value) ? (Int64?)reader["codigocabecera"] : null;          
            this.doctrancabecera = (reader["doctrancabecera"] != DBNull.Value) ? (string)reader["doctrancabecera"] : null;
            this.fechacabecera = (reader["fechacabecera"] != DBNull.Value) ? (DateTime?)reader["fechacabecera"] : null;
            this.dban_beneficiario = (reader["dban_beneficiario"] != DBNull.Value) ? (string)reader["dban_beneficiario"] : null;
            this.dban_documento = (reader["dban_documento"] != DBNull.Value) ? (string)reader["dban_documento"] : null;                 
            this.com_numero = (reader["com_numero"] != DBNull.Value) ? (Int32?)reader["com_numero"] : null;
            this.com_almacen = (reader["com_almacen"] != DBNull.Value) ? (Int32?)reader["com_almacen"] : null;           
            this.com_concepto = (reader["com_concepto"] != DBNull.Value) ? (string)reader["com_concepto"] : null;
            this.dban_transacc = (reader["dban_transacc"] != DBNull.Value) ? (Int32?)reader["dban_transacc"] : null;           
            this.dban_valor_nac = (reader["dban_valor_nac"] != DBNull.Value) ? (decimal?)reader["dban_valor_nac"] : null;
            this.dban_valor_ext = (reader["dban_valor_ext"] != DBNull.Value) ? (decimal?)reader["dban_valor_ext"] : null;
            this.ban_codigo= (reader["ban_codigo"] != DBNull.Value) ? (Int32?)reader["ban_codigo"] : null;
            this.ban_numero = (reader["ban_numero"] != DBNull.Value) ? (Int32?)reader["ban_numero"] : null;
            this.ban_id = (reader["ban_id"] != DBNull.Value) ? (string)reader["ban_id"] : null;
            this.ban_nombre = (reader["ban_nombre"] != DBNull.Value) ? (string)reader["ban_nombre"] : null;
            this.cue_nombre = (reader["cue_nombre"] != DBNull.Value) ? (string)reader["cue_nombre"] : null;
            this.dban_debcre = (reader["dban_debcre"] != DBNull.Value) ? (Int32?)reader["dban_debcre"] : null;
        }

        #endregion

        public string GetSQL()
        {
            string sql = "select    " +
"	comprobante.com_codigo codigocabecera,     "+
"	comprobante.com_doctran  doctrancabecera,   "+  
"	comprobante.com_fecha		fechacabecera,   "  +
"	comprobante.com_numero,    "+
"	comprobante.com_concepto,   "+
"	comprobante.com_almacen,   " +
"	dban_transacc, " +
"	dban_valor_nac, "+
"	dban_valor_ext, "+
"	ban_codigo, " +
"	ban_numero, "+
"	dban_debcre, "+
"	dban_beneficiario, "+
"	dban_documento, "+
"	ban_id, "+
"	ban_nombre, "+
"	cuentadbancario.cue_nombre "+
"from dbancario       "+
"inner join comprobante on com_codigo=dban_cco_comproba  and  com_empresa=dban_empresa     "+
"inner join banco on ban_empresa=dban_empresa  and  ban_codigo=dban_banco     "+
"inner join cuenta cuentadbancario on cuentadbancario.cue_empresa=ban_empresa  and  cuentadbancario.cue_codigo=ban_cuenta ";
            return sql;
        }


        public List<vDbancario> GetStruc()
        {
            return new List<vDbancario>();
        }


    }
}
