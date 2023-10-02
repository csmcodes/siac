﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using SqlDataBase;
using System.Data;

namespace DataAccessLayer
{
    public class vEstadoCuentaDAL
    {
        #region Get All

        public static List<vEstadoCuenta> GetAllSumDoc(WhereParams parametros, string OrderBy)
        {
            List<vEstadoCuenta> list = new List<vEstadoCuenta>();
            vEstadoCuenta obj = new vEstadoCuenta();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllGroup(transaction, parametros, OrderBy, obj.GetSqlSumDoc());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vEstadoCuenta(reader,0));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllGroup(transaction, parametros, OrderBy, obj.GetSqlSumDoc());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vEstadoCuenta(reader,0));

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

        public static List<vEstadoCuenta> GetAllSumCan(WhereParams parametros, string OrderBy)
        {
            List<vEstadoCuenta> list = new List<vEstadoCuenta>();
            vEstadoCuenta obj = new vEstadoCuenta();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllGroup(transaction, parametros, OrderBy, obj.GetSqlSumCan());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vEstadoCuenta(reader, 0));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllGroup(transaction, parametros, OrderBy, obj.GetSqlSumCan());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vEstadoCuenta(reader, 0));

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


        public static List<vEstadoCuenta> GetAllDoc(WhereParams parametros, string OrderBy)
        {
            List<vEstadoCuenta> list = new List<vEstadoCuenta>();
            vEstadoCuenta obj = new vEstadoCuenta();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAll(transaction, parametros, OrderBy, obj.GetSQLDoc());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vEstadoCuenta(reader, 1));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, parametros, OrderBy, obj.GetSQLDoc());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vEstadoCuenta(reader, 1));

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


        public static List<vEstadoCuenta> GetAllCan(WhereParams parametros, string OrderBy)
        {
            List<vEstadoCuenta> list = new List<vEstadoCuenta>();
            vEstadoCuenta obj = new vEstadoCuenta();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAll(transaction, parametros, OrderBy, obj.GetSQLCan());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vEstadoCuenta(reader, 2));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, parametros, OrderBy, obj.GetSQLCan());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vEstadoCuenta(reader, 2));

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



        public static List<vEstadoCuenta> GetAllDoc1(WhereParams parametros, string OrderBy)
        {
            List<vEstadoCuenta> list = new List<vEstadoCuenta>();
            vEstadoCuenta obj = new vEstadoCuenta();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllGroup(transaction, parametros, OrderBy, obj.GetSQLDoc1());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vEstadoCuenta(reader, 1));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllGroup(transaction, parametros, OrderBy, obj.GetSQLDoc1());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vEstadoCuenta(reader, 1));

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


        public static List<vEstadoCuenta> GetAllCan1(WhereParams parametros, string OrderBy)
        {
            List<vEstadoCuenta> list = new List<vEstadoCuenta>();
            vEstadoCuenta obj = new vEstadoCuenta();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllGroup(transaction, parametros, OrderBy, obj.GetSQLCan1());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vEstadoCuenta(reader, 2));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllGroup(transaction, parametros, OrderBy, obj.GetSQLCan1());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vEstadoCuenta(reader, 2));

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

        /*
        public static List<vEstadoCuenta> GetAll(WhereParams parametros, string OrderBy)
        {
            List<vEstadoCuenta> list = new List<vEstadoCuenta>();
            vEstadoCuenta obj = new vEstadoCuenta();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllGroup(transaction, parametros, OrderBy, obj.GetSQL());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vEstadoCuenta(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllGroup(transaction, parametros, OrderBy, obj.GetSQL());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vEstadoCuenta(reader));

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

        public static List<vEstadoCuenta> GetAllByPlanilla(WhereParams parametros, string OrderBy)
        {
            List<vEstadoCuenta> list = new List<vEstadoCuenta>();
            vEstadoCuenta obj = new vEstadoCuenta();
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
                        list.Add(new vEstadoCuenta(reader));

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
                        list.Add(new vEstadoCuenta(reader));

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

        */
        #endregion
    }
}
