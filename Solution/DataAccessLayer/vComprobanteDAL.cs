using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using SqlDataBase;
using System.Data;

namespace DataAccessLayer
{
    public class vComprobanteDAL
    {
        #region Get All

        public static List<vComprobante> GetAllbyPage(WhereParams parametros, string OrderBy, int Desde, int Hasta)
        {
            List<vComprobante> list = new List<vComprobante>();
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
                        list.Add(new vComprobante(reader));

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
                        list.Add(new vComprobante(reader));

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


        public static List<vComprobante> GetAll(WhereParams parametros, string OrderBy)
        {
            List<vComprobante> list = new List<vComprobante>();
            vComprobante obj = new vComprobante();
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
                        list.Add(new vComprobante(reader));

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
                        list.Add(new vComprobante(reader));

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

        public static List<vComprobante> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            List<vComprobante> list = new List<vComprobante>();
            vComprobante obj = new vComprobante();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllTopHaving(transaction, parametros, OrderBy, Top, obj.GetSQLALL());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vComprobante(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllTopHaving(transaction, parametros, OrderBy, 200, obj.GetSQLALL());

                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vComprobante(reader));

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



        public static List<vComprobante> GetAllRange(WhereParams parametros, string OrderBy, int limit, int offset)
        {
            List<vComprobante> list = new List<vComprobante>();
            vComprobante obj = new vComprobante();
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
                        list.Add(new vComprobante(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllRange(transaction, parametros, OrderBy, obj.GetSQLRange(), limit, offset);

                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vComprobante(reader, true));

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
