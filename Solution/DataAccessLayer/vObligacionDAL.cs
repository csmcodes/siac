
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using SqlDataBase;
using System.Data;

namespace DataAccessLayer
{
   public class vObligacionDAL
    {


       public static List<vObligacion> GetAll(WhereParams parametros, string OrderBy)
       {
           List<vObligacion> list = new List<vObligacion>();
           vObligacion obj = new vObligacion();
           if (DAL.GetProvider() == Provider.SqlServer)
           {
               SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
               try
               {
                   transaction.BeginTransaction();
                   IDataReader reader = SqlDataBase.DB.GetAll(transaction, parametros, OrderBy, obj.GetSQLQALL());
                   do
                   {
                       if (!reader.Read())
                           break; // we are done
                       list.Add(new vObligacion(reader));

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
                   IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, parametros, OrderBy, obj.GetSQLQALL());
                   do
                   {
                       if (!reader.Read())
                           break; // we are done
                       list.Add(new vObligacion(reader));

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
