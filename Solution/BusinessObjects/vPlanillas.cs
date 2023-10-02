using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vPlanillas
    {
        public int? com_empresa { get; set; }
        public Int64? com_codigo { get; set; }
        public int? com_transacc { get; set; }
        public string com_doctran { get; set; }        
        public int? com_codclipro { get; set; }
        public DateTime? com_fecha { get; set; }        
        public int? cenv_socio { get; set; }
        public string per_ciruc { get; set; }
        public string per_nombres { get; set; }
        public string per_apellidos { get; set; }
        public string per_razon { get; set; }
        public decimal tot_total { get; set; }
        

           #region Constructors


        public vPlanillas()
        {

        }



        public vPlanillas(IDataReader reader)
        {
            this.com_empresa = (reader["com_empresa"] != DBNull.Value) ? (int?)reader["com_empresa"] : null;
            this.com_codigo = (reader["com_codigo"] != DBNull.Value) ? (Int64?)reader["com_codigo"] : null;
            this.com_transacc = (reader["com_transacc"] != DBNull.Value) ? (int?)reader["com_transacc"] : null;
            this.com_doctran = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
            this.com_codclipro = (reader["com_codclipro"] != DBNull.Value) ? (int?)reader["com_codclipro"] : null;
            this.com_fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
            this.cenv_socio = (reader["cenv_socio"] != DBNull.Value) ? (int?)reader["cenv_socio"] : null;
            this.per_ciruc = (reader["per_ciruc"] != DBNull.Value) ? (string)reader["per_ciruc"] : null;
            this.per_nombres = (reader["per_nombres"] != DBNull.Value) ? (string)reader["per_nombres"] : null;
            this.per_apellidos = (reader["per_apellidos"] != DBNull.Value) ? (string)reader["per_apellidos"] : null;
            this.per_razon = (reader["per_razon"] != DBNull.Value) ? (string)reader["per_razon"] : null;
            this.tot_total =  (decimal)reader["tot_total"] ;
        }

        #endregion

        public string GetSQL()
        {
            string sql = "SELECT "+ 
        "com_empresa, "+  
        "com_codigo, "+
        "com_transacc,  "+ 
        "com_doctran,  "+                               
        "com_codclipro,  "+         
        "com_fecha,  "+                      
        "cenv_socio,  "+ 
        "per_ciruc,  "+ 
        "per_nombres,  "+ 
        "per_apellidos,  "+ 
        "per_razon,  "+
        "tot_total "+
        
         
     "FROM  "+
         "comprobante  "+  
         "INNER JOIN ccomenv ON com_empresa = cenv_empresa AND com_codigo = cenv_comprobante  "+ 
         "INNER JOIN total ON com_empresa = tot_empresa AND com_codigo = tot_comprobante  "+ 
         "LEFT JOIN persona ON cenv_empresa = per_empresa AND cenv_socio = per_codigo  ";                      

            return sql;
        }


        public List<vPlanillas> GetStruc()
        {
            return new List<vPlanillas>();
        }
	
    }
}
