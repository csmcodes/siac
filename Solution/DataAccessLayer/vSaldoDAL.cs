using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using SqlDataBase;
using System.Data;

namespace DataAccessLayer
{
    public class vSaldoDAL
    {
        #region Get All


        public static List<vSaldo> GetAll(WhereParams parametros, string OrderBy)
        {
            List<vSaldo> list = new List<vSaldo>();
            vSaldo obj = new vSaldo ();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllGroup(transaction, parametros, OrderBy, obj.GetSQLAll());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vSaldo(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllGroup(transaction, parametros, OrderBy, obj.GetSQLAll());

                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vSaldo(reader));

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


        public static List<vSaldo> GetAll1(WhereParams parametros, string OrderBy)
        {
            List<vSaldo> list = new List<vSaldo>();
            vSaldo obj = new vSaldo();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllGroup(transaction, parametros, OrderBy, obj.GetSQLAll1());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vSaldo(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllGroup(transaction, parametros, OrderBy, obj.GetSQLAll1());

                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vSaldo(reader));

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
