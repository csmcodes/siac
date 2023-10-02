using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using SqlDataBase;
using System.Data;

namespace DataAccessLayer
{
   public class vRetencionesDAL
    {

        #region Get All

        public static List<vRetenciones> GetAllbyPage(WhereParams parametros, string OrderBy, int Desde, int Hasta)
        {
            List<vRetenciones> list = new List<vRetenciones>();
            vComprobante obj = new vComprobante();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllByPageHaving(transaction, parametros, OrderBy, Desde, Hasta, obj.GetSQL());
                    //  IDataReader reader = SqlDataBase.DB.GetAll(transaction, parametros, OrderBy, obj.GetSQL());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vRetenciones(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllByPageHaving(transaction, parametros, OrderBy, Desde, Hasta, obj.GetSQL());
                    //     IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, parametros, OrderBy, obj.GetSQL());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vRetenciones(reader));

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

        public static List<vRetenciones> GetAll(WhereParams parametros, string OrderBy)
        {
            List<vRetenciones> list = new List<vRetenciones>();
            vRetenciones obj = new vRetenciones();
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
                        list.Add(new vRetenciones(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllHaving(transaction, parametros, OrderBy, obj.GetSQLALL());

                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vRetenciones(reader));

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


        #endregion
    }
}
