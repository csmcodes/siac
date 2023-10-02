using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using SqlDataBase;
using System.Data;

namespace DataAccessLayer
{
    public class vPlanillaSocioTotDAL
    {
        #region Get All

        public static List<vPlanillaSocioTot> GetAll(WhereParams parametros, string OrderBy)
        {
            List<vPlanillaSocioTot> list = new List<vPlanillaSocioTot>();
            vPlanillaSocioTot obj = new vPlanillaSocioTot();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllGroup(transaction, parametros, OrderBy, obj.getSQL());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vPlanillaSocioTot(reader,1));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllGroup(transaction, parametros, OrderBy, obj.getSQL());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vPlanillaSocioTot(reader,1));

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

        public static List<vPlanillaSocioTot> GetAllRub(WhereParams parametros, string OrderBy)
        {
            List<vPlanillaSocioTot> list = new List<vPlanillaSocioTot>();
            vPlanillaSocioTot obj = new vPlanillaSocioTot();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllGroup(transaction, parametros, OrderBy, obj.getSQLRub());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vPlanillaSocioTot(reader, 2));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllGroup(transaction, parametros, OrderBy, obj.getSQLRub());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vPlanillaSocioTot(reader, 2));

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
