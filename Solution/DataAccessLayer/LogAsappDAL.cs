
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using SqlDataBase;
using SqlDataBasePG;
using System.Data;

namespace DataAccessLayer
{
    public class LogAsappDAL
    {
        #region Insert
        public static int Insert(LogAsapp obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.InsertSQL(obj.GetProperties(), "log_asapp", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.InsertSQL(obj.GetProperties(), "log_asapp", obj);
            else
                return 0;
        }
        #endregion

        #region Purgar

        // Borra registros mas viejos que "meses" - usado por la retencion de 12 meses acordada con el usuario.
        public static int Purgar(int meses)
        {
            WhereParams parametros = new WhereParams();
            parametros.where = "crea_fecha < {0}";
            parametros.valores = new object[] { DateTime.Now.AddMonths(-meses) };

            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    int filas = SqlDataBase.DB.DeleteAll(transaction, parametros, "log_asapp");
                    transaction.Commit();
                    return filas;
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
                    int filas = SqlDataBasePG.DB.DeleteAll(transaction, parametros, "log_asapp");
                    transaction.Commit();
                    return filas;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return 0;
        }

        #endregion

        #region Get All

        public static List<LogAsapp> GetAll(WhereParams parametros, string OrderBy)
        {
            List<LogAsapp> list = new List<LogAsapp>();
            LogAsapp obj = new LogAsapp();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAll(transaction, parametros, OrderBy, obj.GetProperties(), "log_asapp");
                    do
                    {
                        if (!reader.Read())
                            break;
                        list.Add(new LogAsapp(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, parametros, OrderBy, obj.GetProperties(), "log_asapp");
                    do
                    {
                        if (!reader.Read())
                            break;
                        list.Add(new LogAsapp(reader));

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
