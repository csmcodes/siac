using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vCuentas
    {


        public string cue_id { get; set; }
        public int? cue_codigo { get; set; }
        public string cue_nombre { get; set; }       
       
        #region Constructors


        public vCuentas()
        {

        }



        public vCuentas(IDataReader reader)
        {
            this.cue_id = (reader["cue_id"] != DBNull.Value) ? (string)reader["cue_id"] : null;
            this.cue_codigo = (reader["cue_codigo"] != DBNull.Value) ? (int?)reader["cue_codigo"] : null;
            this.cue_nombre = (reader["cue_nombre"] != DBNull.Value) ? (string)reader["cue_nombre"] : null;
                 
        }

        #endregion

        public string GetSQL()
        {
            string sql = " WITH  RECURSIVE n(cue_id,cue_codigo, cue_nombre,cue_empresa) AS " +
                        " (SELECT cue_id,cue_codigo, cue_nombre,cue_empresa " +
                        " FROM cuenta "+
                        " %whereclause% " +
                        " UNION ALL "+
                        " SELECT nplus1.cue_id,nplus1.cue_codigo, nplus1.cue_nombre,nplus1.cue_empresa " +
                        " FROM cuenta as nplus1,n "+
                        " WHERE  nplus1.cue_reporta=n.cue_codigo and nplus1.cue_empresa=n.cue_empresa ) " +
                        " SELECT cue_codigo,cue_id,cue_nombre FROM n " ;
            return sql;
        }

        public List<vCuentas> GetStruc()
        {
            return new List<vCuentas>();
        }


    }
}
