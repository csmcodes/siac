﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using SqlDataBase;
using System.Data;

namespace DataAccessLayer
{
    public class vRetencionVentaDAL
    {
        #region Get All


        public static List<vRetencionVenta> GetAll(WhereParams parametros, string OrderBy)
        {
            List<vRetencionVenta> list = new List<vRetencionVenta>();
            vRetencionVenta obj = new vRetencionVenta();
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
                        list.Add(new vRetencionVenta(reader));

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
                        list.Add(new vRetencionVenta(reader));

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


        public static List<vRetencionVenta> GetAllCom(WhereParams parametros, string OrderBy)
        {
            List<vRetencionVenta> list = new List<vRetencionVenta>();
            vRetencionVenta obj = new vRetencionVenta();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllHaving(transaction, parametros, OrderBy, obj.GetSQLCom());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vRetencionVenta(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllHaving(transaction, parametros, OrderBy, obj.GetSQLCom());

                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vRetencionVenta(reader));

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
