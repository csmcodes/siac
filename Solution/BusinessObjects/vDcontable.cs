using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vDcontable
    {
        public Int64? codigocabecera { get; set; }
        public string doctrancabecera { get; set; }
        public DateTime? fechacabecera { get; set; }
        public string per_nombres { get; set; }
        public string per_apellidos { get; set; }
        public string com_concepto { get; set; }
        public Int32? com_numero { get; set; }
        public Int32? com_almacen { get; set; }        
        public Int32? dco_centro { get; set; }
        public Int32? dco_almacen { get; set; }
        public Int32? dco_transacc{ get; set; }
        public decimal? dco_valor_nac { get; set; }
        public decimal? dco_valor_ext { get; set; }
        public string dco_concepto { get; set; }
        public Int32? cue_codigo { get; set; }
        public string cue_id { get; set; }
        public string cue_nombre { get; set; }
        public string mod_id { get; set; }
        public string per_id { get; set; }
        public Int32? dco_debcre { get; set; }
        public string dco_doctran{ get; set; }

        #region Constructors


        public vDcontable()
        {

        }



        public vDcontable(IDataReader reader)
        {
            this.codigocabecera = (reader["codigocabecera"] != DBNull.Value) ? (Int64?)reader["codigocabecera"] : null;          
            this.doctrancabecera = (reader["doctrancabecera"] != DBNull.Value) ? (string)reader["doctrancabecera"] : null;
            this.fechacabecera = (reader["fechacabecera"] != DBNull.Value) ? (DateTime?)reader["fechacabecera"] : null;
            this.per_apellidos = (reader["per_apellidos"] != DBNull.Value) ? (string)reader["per_apellidos"] : null;
            this.per_nombres = (reader["per_nombres"] != DBNull.Value) ? (string)reader["per_nombres"] : null;                 
            this.com_numero = (reader["com_numero"] != DBNull.Value) ? (Int32?)reader["com_numero"] : null;           
            this.com_concepto = (reader["com_concepto"] != DBNull.Value) ? (string)reader["com_concepto"] : null;
            this.com_almacen= (reader["com_almacen"] != DBNull.Value) ? (Int32?)reader["com_almacen"] : null;
            this.dco_almacen= (reader["dco_almacen"] != DBNull.Value) ? (Int32?)reader["dco_almacen"] : null;
            this.dco_centro = (reader["dco_centro"] != DBNull.Value) ? (Int32?)reader["dco_centro"] : null;
            this.dco_transacc = (reader["dco_transacc"] != DBNull.Value) ? (Int32?)reader["dco_transacc"] : null;           
            this.dco_valor_nac = (reader["dco_valor_nac"] != DBNull.Value) ? (decimal?)reader["dco_valor_nac"] : null;
            this.dco_valor_ext = (reader["dco_valor_ext"] != DBNull.Value) ? (decimal?)reader["dco_valor_ext"] : null;
            this.dco_concepto = (reader["dco_concepto"] != DBNull.Value) ? (string)reader["dco_concepto"] : null;
            this.cue_codigo = (reader["cue_codigo"] != DBNull.Value) ? (Int32?)reader["cue_codigo"] : null;
            this.cue_id = (reader["cue_id"] != DBNull.Value) ? (string)reader["cue_id"] : null;
            this.cue_nombre = (reader["cue_nombre"] != DBNull.Value) ? (string)reader["cue_nombre"] : null;
            this.mod_id = (reader["mod_id"] != DBNull.Value) ? (string)reader["mod_id"] : null;
            this.per_id = (reader["per_id"] != DBNull.Value) ? (string)reader["per_id"] : null;
            this.dco_debcre = (reader["dco_debcre"] != DBNull.Value) ? (Int32?)reader["dco_debcre"] : null;
            this.dco_doctran= (reader["dco_doctran"] != DBNull.Value) ? (string)reader["dco_doctran"] : null;
        }

        #endregion

        public string GetSQL()
        {
            string sql = "select  "+   
	                        "comprobante.com_codigo codigocabecera,  "   +
	                        "comprobante.com_doctran  doctrancabecera,  " +  
	                        "comprobante.com_fecha		fechacabecera, "   + 
	                        "comprobante.com_numero,    "+
	                        "comprobante.com_concepto,    "+
                            "comprobante.com_almacen,    " +
                            "dco_almacen, " +
                            "dco_centro, " +
                            "dco_transacc, " +
	                        "dco_valor_nac, "+
                            "dco_valor_ext, " +
	                        "dco_concepto," +
	                        "dco_debcre,"+
                            "dco_doctran," +
                            "cue_codigo," +
	                        "cue_id,"+
	                        "cue_nombre,"+
	                        "mod_id,"+
	                        "per_id,"+
	                        "per_nombres,"+
	                        "per_apellidos "	+
                        "from dcontable       "+
                        "inner join comprobante on com_codigo=dco_comprobante  and  com_empresa=dco_empresa     "   +     
                        "inner join cuenta  on cue_empresa=com_empresa  and  cue_codigo=dco_cuenta    " +
                        "inner join modulo on mod_codigo=cue_modulo "+
                        "left join persona on per_empresa=dco_empresa  and  per_codigo=dco_cliente";
            return sql;
        }


        public List<vDcontable> GetStruc()
        {
            return new List<vDcontable>();
        }


    }
}
