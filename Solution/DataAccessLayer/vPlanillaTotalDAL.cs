using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using SqlDataBase;
using System.Data;

namespace DataAccessLayer
{
    public class vPlanillaTotalDAL
    {
        public static List<vPlanillaTotal> GetAll(WhereParams parametros, string OrderBy)
        {
            List<vPlanillaTotal> list = new List<vPlanillaTotal>();
            vPlanillaTotal obj = new vPlanillaTotal();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAll(transaction, parametros, OrderBy, obj.getSQL());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vPlanillaTotal(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, parametros, OrderBy, obj.getSQL());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vPlanillaTotal(reader));

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






        public static List<vPlanillaTotal> GetAllS(WhereParams parametros, string OrderBy)
        {
            List<vPlanillaTotal> list = new List<vPlanillaTotal>();
            vPlanillaTotal obj = new vPlanillaTotal();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAll(transaction, parametros, OrderBy, obj.getSQLS());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vPlanillaTotal(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, parametros, OrderBy, obj.getSQLS());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vPlanillaTotal(reader));

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




        public static List<vPlanillaTotal> GetAllN(WhereParams parametros, string OrderBy)
        {
            List<vPlanillaTotal> list = new List<vPlanillaTotal>();
            vPlanillaTotal obj = new vPlanillaTotal();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAll(transaction, parametros, OrderBy, obj.getSQLN());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vPlanillaTotal(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, parametros, OrderBy, obj.getSQLN());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vPlanillaTotal(reader));

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


        public static List<vPlanillaTotal> GetAllNP(WhereParams parametros, string OrderBy)
        {
            List<vPlanillaTotal> list = new List<vPlanillaTotal>();
            vPlanillaTotal obj = new vPlanillaTotal();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAll(transaction, parametros, OrderBy, obj.getSQLNV());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vPlanillaTotal(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, parametros, OrderBy, obj.getSQLNV());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vPlanillaTotal(reader));

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
