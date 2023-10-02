
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
    public class CcomenvDAL
    {
        #region Insert
        public static int Insert(Ccomenv obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.InsertSQL(obj.GetProperties(), "ccomenv", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.InsertSQL(obj.GetProperties(), "ccomenv", obj);
            else
                return 0;
        }
        public static int Insert(DAL dal, Ccomenv obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.InsertSQL(dal.transaccion, obj.GetProperties(), "ccomenv", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.InsertSQL(dal.transaccionpg, obj.GetProperties(), "ccomenv", obj);
            else
                return 0;
        }
        public static int InsertIdentity(Ccomenv obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.InsertIdentitySQL(obj.GetProperties(), "ccomenv", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.InsertIdentitySQL(obj.GetProperties(), "ccomenv", obj);
            else
                return 0;
        }
        public static int InsertIdentity(DAL dal, Ccomenv obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.InsertIdentitySQL(dal.transaccion, obj.GetProperties(), "ccomenv", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.InsertIdentitySQL(dal.transaccionpg, obj.GetProperties(), "ccomenv", obj);
            else
                return 0;
        }
        #endregion

        #region Update
        public static int Update(Ccomenv obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.UpdateSQL(obj.GetProperties(), "ccomenv", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.UpdateSQL(obj.GetProperties(), "ccomenv", obj);
            else
                return 0;
        }
        public static int Update(DAL dal, Ccomenv obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.UpdateSQL(dal.transaccion, obj.GetProperties(), "ccomenv", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.UpdateSQL(dal.transaccionpg, obj.GetProperties(), "ccomenv", obj);
            else
                return 0;
        }
        #endregion

        #region Delete
        public static int Delete(Ccomenv obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.DeleteSQL(obj.GetProperties(), "ccomenv", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.DeleteSQL(obj.GetProperties(), "ccomenv", obj);
            else
                return 0;
        }
        

        public static int Delete(DAL dal, Ccomenv obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.DeleteSQL(dal.transaccion, obj.GetProperties(), "ccomenv", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.DeleteSQL(dal.transaccionpg, obj.GetProperties(), "ccomenv", obj);
            else
                return 0;
        }
        #endregion

        #region Get By Primary Key

        public static Ccomenv GetByPK(Ccomenv obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetByPKSql(transaction, obj.GetProperties(), "ccomenv", obj);
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        obj = new Ccomenv(reader);

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
                    IDataReader reader = SqlDataBasePG.DB.GetByPKSql(transaction, obj.GetProperties(), "ccomenv", obj);
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        obj = new Ccomenv(reader);

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
        
        public static Ccomenv GetById(Ccomenv obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();                    
                    IDataReader reader = SqlDataBase.DB.GetById(transaction, obj.GetProperties(), "ccomenv", obj);
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        obj = new Ccomenv(reader);

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
                    IDataReader reader = SqlDataBasePG.DB.GetById(transaction, obj.GetProperties(), "ccomenv", obj);
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        obj = new Ccomenv(reader);

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

        public static List<Ccomenv> GetAll(string WhereClause, string OrderBy)
        {
            List<Ccomenv> list = new List<Ccomenv>();
            Ccomenv obj = new Ccomenv();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAll(transaction, WhereClause, OrderBy, obj.GetProperties(), "ccomenv");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Ccomenv(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, WhereClause, OrderBy, obj.GetProperties(), "ccomenv");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Ccomenv(reader));

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

        public static List<Ccomenv> GetAll(WhereParams parametros, string OrderBy)
        {
            List<Ccomenv> list = new List<Ccomenv>();
            Ccomenv obj = new Ccomenv();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAll(transaction, parametros, OrderBy, obj.GetProperties(), "ccomenv");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Ccomenv(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, parametros, OrderBy, obj.GetProperties(), "ccomenv");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Ccomenv(reader));

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

        public static List<Ccomenv> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            List<Ccomenv> list = new List<Ccomenv>();
            Ccomenv obj = new Ccomenv();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllTop(transaction, parametros, OrderBy, Top, obj.GetProperties(), "ccomenv");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Ccomenv(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllTop(transaction, parametros, OrderBy, Top, obj.GetProperties(), "ccomenv");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Ccomenv(reader));

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

        public static List<Ccomenv> GetAllbyPage(string WhereClause, string OrderBy, int Desde, int Hasta)
        {
            List<Ccomenv> list = new List<Ccomenv>();
            Ccomenv obj = new Ccomenv();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllByPage(transaction, WhereClause, OrderBy, Desde, Hasta, obj.GetProperties(), "ccomenv");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Ccomenv(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllByPage(transaction, WhereClause, OrderBy, Desde, Hasta, obj.GetProperties(), "ccomenv");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Ccomenv(reader));

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

        public static List<Ccomenv> GetAllbyPage(WhereParams parametros, string OrderBy, int Desde, int Hasta)
        {
            List<Ccomenv> list = new List<Ccomenv>();
            Ccomenv obj = new Ccomenv();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllByPage(transaction, parametros, OrderBy, Desde, Hasta, obj.GetProperties(), "ccomenv");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Ccomenv(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllByPage(transaction, parametros, OrderBy, Desde, Hasta, obj.GetProperties(), "ccomenv");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Ccomenv(reader));

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
                return SqlDataBase.DB.GetRecordCount(WhereClause, OrderBy,  "ccomenv");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetRecordCount(WhereClause, OrderBy,  "ccomenv");
            else
                return 0;


        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.GetRecordCount(parametros, OrderBy, "ccomenv");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetRecordCount(parametros, OrderBy, "ccomenv");
            else
                return 0;
        }

        #endregion

        #region Get Max

        public static int GetMax(string campo)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.GetMax(campo, "ccomenv");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetMax(campo, "ccomenv");
            else
                return 0;

        }

		   public static int GetMax(string campo,string whereclause)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.GetMax(campo,whereclause, "ccomenv");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetMax(campo, whereclause,"ccomenv");
            else
                return 0;

        }
        public static int GetMax(string campo, WhereParams parametros)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.GetMax(campo, parametros,"ccomenv");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetMax(campo, parametros, "ccomenv");
            else
                return 0;

        }


        #endregion


		#region Get Sum

        public static decimal GetSum(string campo, string WhereClause, string OrderBy)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.GetSum(campo,WhereClause, OrderBy,  "ccomenv");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetSum(campo,WhereClause, OrderBy,  "ccomenv");
            else
                return 0;


        }

        public static decimal GetSum(string campo, WhereParams parametros, string OrderBy)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.GetSum(campo, parametros, OrderBy,  "ccomenv");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetSum(campo, parametros, OrderBy,  "ccomenv");
            else
                return 0;
        }

        #endregion
    }
}