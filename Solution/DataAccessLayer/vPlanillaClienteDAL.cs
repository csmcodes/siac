
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using SqlDataBase;
using System.Data;

namespace DataAccessLayer
{
    public class vPlanillaClienteDAL
    {

        #region Get All

        public static List<vPlanillaCliente> GetAll(WhereParams parametros, string OrderBy)
        {
            List<vPlanillaCliente> list = new List<vPlanillaCliente>();
            vPlanillaCliente obj = new vPlanillaCliente();
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
                    list.Add(new vPlanillaCliente(reader));

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
                        list.Add(new vPlanillaCliente(reader));

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

        public static List<vPlanillaCliente> GetAllDet(WhereParams parametros, string OrderBy)
        {
            List<vPlanillaCliente> list = new List<vPlanillaCliente>();
            vPlanillaCliente obj = new vPlanillaCliente();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAll(transaction, parametros, OrderBy, obj.GetSQLDet());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vPlanillaCliente(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, parametros, OrderBy, obj.GetSQLDet());
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new vPlanillaCliente(reader));

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
