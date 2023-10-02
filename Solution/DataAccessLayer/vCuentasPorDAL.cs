using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using SqlDataBase;
using System.Data;


namespace DataAccessLayer
{
    public class vCuentasPorDAL
    {
        #region Get All

        public static List<vCuentasPor> GetAll(WhereParams parwhere, WhereParams parhaving, string OrderBy)
        {
            List<vCuentasPor> list = new List<vCuentasPor>();
            vCuentasPor obj = new vCuentasPor();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllWhereHaving(transaction, parwhere,parhaving, OrderBy, obj.GetSQL());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vCuentasPor(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllWhereHaving(transaction, parwhere, parhaving, OrderBy, obj.GetSQL());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vCuentasPor(reader));

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

        
        public static List<vCuentasPor> GetAll1(WhereParams parwhere, WhereParams parrango, string OrderBy)
        {
            List<vCuentasPor> list = new List<vCuentasPor>();
            vCuentasPor obj = new vCuentasPor();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllWhereRango(transaction, parwhere, parrango, OrderBy, obj.GetSQL1());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vCuentasPor(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllWhereRango(transaction, parwhere, parrango, OrderBy, obj.GetSQL1());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vCuentasPor(reader));

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


        public static List<vCuentasPor> GetAllDoc(WhereParams parwhere, string OrderBy)
        {
            List<vCuentasPor> list = new List<vCuentasPor>();
            vCuentasPor obj = new vCuentasPor();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllGroup(transaction, parwhere,  OrderBy, obj.GetSQLDoc());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vCuentasPor(reader,true));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllGroup(transaction, parwhere, OrderBy, obj.GetSQLDoc());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vCuentasPor(reader,true));

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

        public static List<vCuentasPor> GetAllCan(WhereParams parwhere, string OrderBy)
        {
            List<vCuentasPor> list = new List<vCuentasPor>();
            vCuentasPor obj = new vCuentasPor();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllGroup(transaction, parwhere, OrderBy, obj.GetSQLCan());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vCuentasPor(reader, false));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllGroup(transaction, parwhere, OrderBy, obj.GetSQLCan());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vCuentasPor(reader, false));

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

        public static List<vCuentasPor> GetAllCanDet(WhereParams parwhere, string OrderBy)
        {
            List<vCuentasPor> list = new List<vCuentasPor>();
            vCuentasPor obj = new vCuentasPor();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllGroup(transaction, parwhere, OrderBy, obj.GetSQLCanDet());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vCuentasPor(reader, false));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllGroup(transaction, parwhere, OrderBy, obj.GetSQLCanDet());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vCuentasPor(reader, false));

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


        public static List<vCuentasPor> GetFull(WhereParams parwhere, string OrderBy)
        {
            List<vCuentasPor> list = new List<vCuentasPor>();
            vCuentasPor obj = new vCuentasPor();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllGroup(transaction, parwhere, OrderBy, obj.GetSQLFull());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vCuentasPor(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllGroup(transaction, parwhere, OrderBy, obj.GetSQLFull());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vCuentasPor(reader));

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
