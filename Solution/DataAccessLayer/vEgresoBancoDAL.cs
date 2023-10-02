using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using SqlDataBase;
using System.Data;


namespace DataAccessLayer
{
 public    class vEgresoBancoDAL
    {

     public static List<vEgresoBanco> GetAll(WhereParams parametros, string OrderBy)
     {
         List<vEgresoBanco> list = new List<vEgresoBanco>();
         vEgresoBanco obj = new vEgresoBanco();
         if (DAL.GetProvider() == Provider.SqlServer)
         {
             SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
             try
             {
                 transaction.BeginTransaction();
                 IDataReader reader = SqlDataBase.DB.GetAll(transaction, parametros, OrderBy, obj.GetSQLALL());
                 do
                 {
                     if (!reader.Read())
                         break; // we are done
                     list.Add(new vEgresoBanco(reader));

                 } while (true);
                 reader.Close();
                 transaction.Commit();

             }
             catch (Exception ex)
             {
                 transaction.Rollback();
                 throw ex;
             }
         }
         else if (DAL.GetProvider() == Provider.PostgreSQL)
         {
             SqlDataBasePG.TransactionManager transaction = new SqlDataBasePG.TransactionManager();
             try
             {
                 transaction.BeginTransaction();
                 IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, parametros, OrderBy, obj.GetSQLALL());

                 do
                 {
                     if (!reader.Read())
                         break; // we are done
                     list.Add(new vEgresoBanco(reader));

                 } while (true);
                 reader.Close();
                 transaction.Commit();

             }
             catch (Exception ex)
             {
                 transaction.Rollback();
                 throw ex;
             }
         }
         return list;


     }
    }
}
