using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using SqlDataBase;
using System.Data;

namespace DataAccessLayer
{
   public  class vVentasRetencionesDAL
    {

       public static List<vVentasRetenciones> GetAll(WhereParams parametros, string OrderBy)
       {
           List<vVentasRetenciones> list = new List<vVentasRetenciones>();
           vVentasRetenciones obj = new vVentasRetenciones();
           if (DAL.GetProvider() == Provider.SqlServer)
           {
               SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
               try
               {
                   transaction.BeginTransaction();
                   IDataReader reader = SqlDataBase.DB.GetAll(transaction, parametros, OrderBy, obj.GetSQL());
                   do
                   {
                       if (!reader.Read())
                           break; // we are done
                       list.Add(new vVentasRetenciones(reader));

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
                   IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, parametros, OrderBy, obj.GetSQL());
                   do
                   {
                       if (!reader.Read())
                           break; // we are done
                       list.Add(new vVentasRetenciones(reader));

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
