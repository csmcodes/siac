using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Functions;


namespace BusinessObjects
{
   public  class vEgresoBanco
    {

       public DateTime? fecha { get; set; }
       public string doctran { get; set; }
       public string beneficiario { get; set; }
       public string cheque { get; set; }
       public Decimal? monto { get; set; }
       public Decimal? monto_0 { get; set; }
       public string nombres { get; set; }
       public string apellidos { get; set; }
       public vEgresoBanco()
       {



       }

       public vEgresoBanco(object objeto)
       {

           if (objeto != null)
           {
               Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;

               object fecha = null;
               object doctran = null;
               object beneficiario = null;
               object cheque = null;
               object monto = null;
               object monto_0 = null;
               object nombres = null;
               object apellidos = null;

               tmp.TryGetValue("fecha", out fecha);
               tmp.TryGetValue("doctran", out doctran);
               tmp.TryGetValue("beneficiario", out beneficiario);
               tmp.TryGetValue("cheque", out cheque);
               tmp.TryGetValue("monto", out monto);
               tmp.TryGetValue("monto_0 ", out monto_0);
               tmp.TryGetValue("nombres", out nombres);
               tmp.TryGetValue("apellidos", out apellidos);

               this.doctran = (String)Conversiones.GetValueByType(doctran, typeof(String));
               this.fecha = (DateTime?)Conversiones.GetValueByType(fecha, typeof(DateTime?));
               this.beneficiario = (String)Conversiones.GetValueByType(beneficiario, typeof(String));
               this.cheque = (String)Conversiones.GetValueByType(cheque, typeof(String));
               this.monto = (Decimal?)Conversiones.GetValueByType(monto, typeof(Decimal?));
               this.monto_0 = (Decimal?)Conversiones.GetValueByType(monto_0, typeof(Decimal?));
               this.nombres = (String)Conversiones.GetValueByType(nombres, typeof(String));
               this.apellidos = (String)Conversiones.GetValueByType(apellidos, typeof(String));
           }


        
       }
       public vEgresoBanco(IDataReader reader)
       {
           this.fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
           this.doctran = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
           this.beneficiario = (reader["dban_beneficiario"] != DBNull.Value) ? (string)reader["dban_beneficiario"] : null;
           this.cheque = (reader["dban_documento"] != DBNull.Value) ? (string)reader["dban_documento"] : null;
           this.monto = (reader["dban_valor_ext"] != DBNull.Value) ? (Decimal?)reader["dban_valor_ext"] : null;
           this.monto_0 = (reader["dban_valor_nac"] != DBNull.Value) ? (Decimal?)reader["dban_valor_nac"] : null;
           this.nombres = (reader["per_nombres"] != DBNull.Value) ? (string)reader["per_nombres"] : null;
           this.apellidos = (reader["per_apellidos"] != DBNull.Value) ? (string)reader["per_apellidos"] : null;




       }

       public string GetSQLALL()
       {

           string sql = "SELECT  " +

               "  c.com_fecha ,c.com_doctran,db.dban_beneficiario,db.dban_documento,db.dban_valor_ext,db.dban_valor_nac , p.per_nombres,p.per_apellidos " +
               "from  comprobante c " +
               " inner join dbancario db on c.com_empresa  = db.dban_empresa and c.com_codigo = db.dban_cco_comproba " +
              " left join persona p on c.com_empresa = p.per_empresa and c.com_codclipro = p.per_codigo ";



           return sql;


       }

       public List<vEgresoBanco> GetStruc()
       {
           return new List<vEgresoBanco>();
       }


    }
}
