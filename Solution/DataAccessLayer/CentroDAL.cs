
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
    public class CentroDAL
    {
        #region Insert
        public static int Insert(Centro obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.InsertSQL(obj.GetProperties(), "centro", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.InsertSQL(obj.GetProperties(), "centro", obj);
            else
                return 0;
        }
        public static int Insert(DAL dal, Centro obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.InsertSQL(dal.transaccion, obj.GetProperties(), "centro", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.InsertSQL(dal.transaccionpg, obj.GetProperties(), "centro", obj);
            else
                return 0;
        }
        public static int InsertIdentity(Centro obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.InsertIdentitySQL(obj.GetProperties(), "centro", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.InsertIdentitySQL(obj.GetProperties(), "centro", obj);
            else
                return 0;
        }
        public static int InsertIdentity(DAL dal, Centro obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.InsertIdentitySQL(dal.transaccion, obj.GetProperties(), "centro", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.InsertIdentitySQL(dal.transaccionpg, obj.GetProperties(), "centro", obj);
            else
                return 0;
        }
        #endregion

        #region Update
        public static int Update(Centro obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.UpdateSQL(obj.GetProperties(), "centro", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.UpdateSQL(obj.GetProperties(), "centro", obj);
            else
                return 0;
        }
        public static int Update(DAL dal, Centro obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.UpdateSQL(dal.transaccion, obj.GetProperties(), "centro", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.UpdateSQL(dal.transaccionpg, obj.GetProperties(), "centro", obj);
            else
                return 0;
        }
        #endregion

        #region Delete
        public static int Delete(Centro obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.DeleteSQL(obj.GetProperties(), "centro", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.DeleteSQL(obj.GetProperties(), "centro", obj);
            else
                return 0;
        }
        

        public static int Delete(DAL dal, Centro obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.DeleteSQL(dal.transaccion, obj.GetProperties(), "centro", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.DeleteSQL(dal.transaccionpg, obj.GetProperties(), "centro", obj);
            else
                return 0;
        }
        #endregion

        #region Get By Primary Key

        public static Centro GetByPK(Centro obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetByPKSql(transaction, obj.GetProperties(), "centro", obj);
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        obj = new Centro(reader);

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
                    IDataReader reader = SqlDataBasePG.DB.GetByPKSql(transaction, obj.GetProperties(), "centro", obj);
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        obj = new Centro(reader);

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
                
            return obj;


        }

        #endregion


        #region Get By Id
        
        public static Centro GetById(Centro obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();                    
                    IDataReader reader = SqlDataBase.DB.GetById(transaction, obj.GetProperties(), "centro", obj);
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        obj = new Centro(reader);

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
                    IDataReader reader = SqlDataBasePG.DB.GetById(transaction, obj.GetProperties(), "centro", obj);
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        obj = new Centro(reader);

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
            return obj;


        }

        #endregion

        #region Get All

        public static List<Centro> GetAll(string WhereClause, string OrderBy)
        {
            List<Centro> list = new List<Centro>();
            Centro obj = new Centro();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAll(transaction, WhereClause, OrderBy, obj.GetProperties(), "centro");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Centro(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, WhereClause, OrderBy, obj.GetProperties(), "centro");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Centro(reader));

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

        public static List<Centro> GetAll(WhereParams parametros, string OrderBy)
        {
            List<Centro> list = new List<Centro>();
            Centro obj = new Centro();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAll(transaction, parametros, OrderBy, obj.GetProperties(), "centro");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Centro(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, parametros, OrderBy, obj.GetProperties(), "centro");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Centro(reader));

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

        public static List<Centro> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            List<Centro> list = new List<Centro>();
            Centro obj = new Centro();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllTop(transaction, parametros, OrderBy, Top, obj.GetProperties(), "centro");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Centro(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllTop(transaction, parametros, OrderBy, Top, obj.GetProperties(), "centro");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Centro(reader));

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

        #region Get All By Page

        public static List<Centro> GetAllbyPage(string WhereClause, string OrderBy, int Desde, int Hasta)
        {
            List<Centro> list = new List<Centro>();
            Centro obj = new Centro();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllByPage(transaction, WhereClause, OrderBy, Desde, Hasta, obj.GetProperties(), "centro");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Centro(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllByPage(transaction, WhereClause, OrderBy, Desde, Hasta, obj.GetProperties(), "centro");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Centro(reader));

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

        public static List<Centro> GetAllbyPage(WhereParams parametros, string OrderBy, int Desde, int Hasta)
        {
            List<Centro> list = new List<Centro>();
            Centro obj = new Centro();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllByPage(transaction, parametros, OrderBy, Desde, Hasta, obj.GetProperties(), "centro");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Centro(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllByPage(transaction, parametros, OrderBy, Desde, Hasta, obj.GetProperties(), "centro");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Centro(reader));

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

        #region Get Record Count

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.GetRecordCount(WhereClause, OrderBy,  "centro");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetRecordCount(WhereClause, OrderBy,  "centro");
            else
                return 0;


        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.GetRecordCount(parametros, OrderBy, "centro");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetRecordCount(parametros, OrderBy, "centro");
            else
                return 0;
        }

        #endregion

        #region Get Max

        public static int GetMax(string campo)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.GetMax(campo, "centro");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetMax(campo, "centro");
            else
                return 0;

        }

		   public static int GetMax(string campo,string whereclause)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.GetMax(campo,whereclause, "centro");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetMax(campo, whereclause,"centro");
            else
                return 0;

        }
        public static int GetMax(string campo, WhereParams parametros)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.GetMax(campo, parametros,"centro");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetMax(campo, parametros, "centro");
            else
                return 0;

        }


        #endregion


		#region Get Sum

        public static decimal GetSum(string campo, string WhereClause, string OrderBy)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.GetSum(campo,WhereClause, OrderBy,  "centro");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetSum(campo,WhereClause, OrderBy,  "centro");
            else
                return 0;


        }

        public static decimal GetSum(string campo, WhereParams parametros, string OrderBy)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.GetSum(campo, parametros, OrderBy,  "centro");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetSum(campo, parametros, OrderBy,  "centro");
            else
                return 0;
        }

        #endregion
    }
}