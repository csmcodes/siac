using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using SqlDataBase;
using System.Data;

namespace DataAccessLayer
{
  public   class vDocumentoVentaDAL
    {

      public static List<vDocumentoVenta> GetAll(WhereParams parametros, string OrderBy)
      {
          List<vDocumentoVenta> list = new List<vDocumentoVenta>();
          vDocumentoVenta obj = new vDocumentoVenta();
          if (DAL.GetProvider() == Provider.SqlServer)
          {
              SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
              try
              {
                  transaction.BeginTransaction();
                  IDataReader reader = SqlDataBase.DB.GetAllHaving(transaction, parametros, OrderBy, obj.GetSQLALL());
                  do
                  {
                      if (!reader.Read())
                          break; // we are done
                      list.Add(new vDocumentoVenta(reader));

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
                      list.Add(new vDocumentoVenta(reader));

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
