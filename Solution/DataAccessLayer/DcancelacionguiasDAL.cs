
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
    public class DcancelacionguiasDAL
    {
        #region Insert
        public static int Insert(Dcancelacionguias obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.InsertSQL(obj.GetProperties(), "dcancelacionguias", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.InsertSQL(obj.GetProperties(), "dcancelacionguias", obj);
            else
                return 0;
        }
        public static int Insert(DAL dal, Dcancelacionguias obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.InsertSQL(dal.transaccion, obj.GetProperties(), "dcancelacionguias", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.InsertSQL(dal.transaccionpg, obj.GetProperties(), "dcancelacionguias", obj);
            else
                return 0;
        }
        public static int InsertIdentity(Dcancelacionguias obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.InsertIdentitySQL(obj.GetProperties(), "dcancelacionguias", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.InsertIdentitySQL(obj.GetProperties(), "dcancelacionguias", obj);
            else
                return 0;
        }
        public static int InsertIdentity(DAL dal, Dcancelacionguias obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.InsertIdentitySQL(dal.transaccion, obj.GetProperties(), "dcancelacionguias", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.InsertIdentitySQL(dal.transaccionpg, obj.GetProperties(), "dcancelacionguias", obj);
            else
                return 0;
        }
        #endregion

        #region Update
        public static int Update(Dcancelacionguias obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.UpdateSQL(obj.GetProperties(), "dcancelacionguias", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.UpdateSQL(obj.GetProperties(), "dcancelacionguias", obj);
            else
                return 0;
        }
        public static int Update(DAL dal, Dcancelacionguias obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.UpdateSQL(dal.transaccion, obj.GetProperties(), "dcancelacionguias", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.UpdateSQL(dal.transaccionpg, obj.GetProperties(), "dcancelacionguias", obj);
            else
                return 0;
        }
        #endregion

        #region Delete
        public static int Delete(Dcancelacionguias obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.DeleteSQL(obj.GetProperties(), "dcancelacionguias", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.DeleteSQL(obj.GetProperties(), "dcancelacionguias", obj);
            else
                return 0;
        }
        

        public static int Delete(DAL dal, Dcancelacionguias obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.DeleteSQL(dal.transaccion, obj.GetProperties(), "dcancelacionguias", obj);
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.DeleteSQL(dal.transaccionpg, obj.GetProperties(), "dcancelacionguias", obj);
            else
                return 0;
        }
        #endregion

        #region Get By Primary Key

        public static Dcancelacionguias GetByPK(Dcancelacionguias obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetByPKSql(transaction, obj.GetProperties(), "dcancelacionguias", obj);
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        obj = new Dcancelacionguias(reader);

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
                    IDataReader reader = SqlDataBasePG.DB.GetByPKSql(transaction, obj.GetProperties(), "dcancelacionguias", obj);
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        obj = new Dcancelacionguias(reader);

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
        
        public static Dcancelacionguias GetById(Dcancelacionguias obj)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();                    
                    IDataReader reader = SqlDataBase.DB.GetById(transaction, obj.GetProperties(), "dcancelacionguias", obj);
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        obj = new Dcancelacionguias(reader);

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
                    IDataReader reader = SqlDataBasePG.DB.GetById(transaction, obj.GetProperties(), "dcancelacionguias", obj);
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        obj = new Dcancelacionguias(reader);

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

        public static List<Dcancelacionguias> GetAll(string WhereClause, string OrderBy)
        {
            List<Dcancelacionguias> list = new List<Dcancelacionguias>();
            Dcancelacionguias obj = new Dcancelacionguias();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAll(transaction, WhereClause, OrderBy, obj.GetProperties(), "dcancelacionguias");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Dcancelacionguias(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, WhereClause, OrderBy, obj.GetProperties(), "dcancelacionguias");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Dcancelacionguias(reader));

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

        public static List<Dcancelacionguias> GetAll(WhereParams parametros, string OrderBy)
        {
            List<Dcancelacionguias> list = new List<Dcancelacionguias>();
            Dcancelacionguias obj = new Dcancelacionguias();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAll(transaction, parametros, OrderBy, obj.GetProperties(), "dcancelacionguias");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Dcancelacionguias(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAll(transaction, parametros, OrderBy, obj.GetProperties(), "dcancelacionguias");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Dcancelacionguias(reader));

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

        public static List<Dcancelacionguias> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            List<Dcancelacionguias> list = new List<Dcancelacionguias>();
            Dcancelacionguias obj = new Dcancelacionguias();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllTop(transaction, parametros, OrderBy, Top, obj.GetProperties(), "dcancelacionguias");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Dcancelacionguias(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllTop(transaction, parametros, OrderBy, Top, obj.GetProperties(), "dcancelacionguias");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Dcancelacionguias(reader));

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

        public static List<Dcancelacionguias> GetAllbyPage(string WhereClause, string OrderBy, int Desde, int Hasta)
        {
            List<Dcancelacionguias> list = new List<Dcancelacionguias>();
            Dcancelacionguias obj = new Dcancelacionguias();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllByPage(transaction, WhereClause, OrderBy, Desde, Hasta, obj.GetProperties(), "dcancelacionguias");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Dcancelacionguias(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllByPage(transaction, WhereClause, OrderBy, Desde, Hasta, obj.GetProperties(), "dcancelacionguias");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Dcancelacionguias(reader));

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

        public static List<Dcancelacionguias> GetAllbyPage(WhereParams parametros, string OrderBy, int Desde, int Hasta)
        {
            List<Dcancelacionguias> list = new List<Dcancelacionguias>();
            Dcancelacionguias obj = new Dcancelacionguias();
            if (DAL.GetProvider() == Provider.SqlServer)
            {
                SqlDataBase.TransactionManager transaction = new SqlDataBase.TransactionManager();
                try
                {
                    transaction.BeginTransaction();
                    IDataReader reader = SqlDataBase.DB.GetAllByPage(transaction, parametros, OrderBy, Desde, Hasta, obj.GetProperties(), "dcancelacionguias");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Dcancelacionguias(reader));

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
                    IDataReader reader = SqlDataBasePG.DB.GetAllByPage(transaction, parametros, OrderBy, Desde, Hasta, obj.GetProperties(), "dcancelacionguias");
                    do
                    {
                        if (!reader.Read())
                            break; // we are done
                        list.Add(new Dcancelacionguias(reader));

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
                return SqlDataBase.DB.GetRecordCount(WhereClause, OrderBy,  "dcancelacionguias");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetRecordCount(WhereClause, OrderBy,  "dcancelacionguias");
            else
                return 0;


        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.GetRecordCount(parametros, OrderBy, "dcancelacionguias");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetRecordCount(parametros, OrderBy, "dcancelacionguias");
            else
                return 0;
        }

        #endregion

        #region Get Max

        public static int GetMax(string campo)
        {
            if (DAL.GetProvider() == Provider.SqlServer)
                return SqlDataBase.DB.GetMax(campo, "dcancelacionguias");
            else if (DAL.GetProvider() == Provider.PostgreSQL)
                return SqlDataBasePG.DB.GetMax(campo, "dcancelacionguias");
            else
                return 0;

        }

        #endregion
    }
}