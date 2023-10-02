using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Functions;
using System.Reflection;

namespace BusinessObjects
{
    public class vComp
    {
        public Int32? com_empresa { get; set; }
        public Int64? com_codigo { get; set; }
        public DateTime? com_fecha { get; set; }
        public Int32? com_tipodoc { get; set; }
        public Int32? com_numero { get; set; }
        public Int32? com_estado { get; set; }


        public vComp()
        { }

        public vComp (IDataReader reader)
        {
            this.com_empresa = (reader["com_empresa"] != DBNull.Value) ? (Int32?)reader["com_empresa"] : null;
            this.com_codigo = (reader["com_codigo"] != DBNull.Value) ? (Int64?)reader["com_codigo"] : null;
            this.com_fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
            this.com_tipodoc = (reader["com_tipodoc"] != DBNull.Value) ? (Int32?)reader["com_tipodoc"] : null;
            this.com_numero = (reader["com_numero"] != DBNull.Value) ? (Int32?)reader["com_numero"] : null;
            this.com_estado = (reader["com_estado"] != DBNull.Value) ? (Int32?)reader["com_estado"] : null;
        }



        public string GetSQL()
        {
            string sql = "SELECT com_empresa, com_codigo, com_fecha, com_tipodoc, com_numero,com_estado, crea_usr, crea_fecha " +
                         " FROM comprobante "+
                         "";

       

            return sql;
        }
    }
}
