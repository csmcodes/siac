

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DbancarioBLL
    {
        #region Constructor

        public DbancarioBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Dbancario obj)
        {
            return DbancarioDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Dbancario obj)
        {
            return DbancarioDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Dbancario obj)
        {
            return DbancarioDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Dbancario obj)
        {
            return DbancarioDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Dbancario obj)
        {
            return DbancarioDAL.Update(obj);
        }
        public static int Update(BLL bll, Dbancario obj)
        {
            return DbancarioDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Dbancario obj)
        {
            return DbancarioDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Dbancario obj)
        {
            return DbancarioDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Dbancario GetByPK(Dbancario obj)
        {
            return DbancarioDAL.GetByPK(obj);
        }
        public static List<Dbancario> GetAll(string WhereClause, string OrderBy)
        {
            return DbancarioDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Dbancario> GetAll(WhereParams parametros, string OrderBy)
        {
            return DbancarioDAL.GetAll(parametros, OrderBy);
        }

         public static List<Dbancario> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DbancarioDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Dbancario> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DbancarioDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Dbancario> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DbancarioDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DbancarioDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DbancarioDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DbancarioDAL.GetMax(campo);
        }
        #endregion
    }
}
