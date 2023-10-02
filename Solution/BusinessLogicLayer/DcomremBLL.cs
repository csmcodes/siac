

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DcomremBLL
    {
        #region Constructor

        public DcomremBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Dcomrem obj)
        {
            return DcomremDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Dcomrem obj)
        {
            return DcomremDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Dcomrem obj)
        {
            return DcomremDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Dcomrem obj)
        {
            return DcomremDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Dcomrem obj)
        {
            return DcomremDAL.Update(obj);
        }
        public static int Update(BLL bll, Dcomrem obj)
        {
            return DcomremDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Dcomrem obj)
        {
            return DcomremDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Dcomrem obj)
        {
            return DcomremDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Dcomrem GetByPK(Dcomrem obj)
        {
            return DcomremDAL.GetByPK(obj);
        }
        public static List<Dcomrem> GetAll(string WhereClause, string OrderBy)
        {
            return DcomremDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Dcomrem> GetAll(WhereParams parametros, string OrderBy)
        {
            return DcomremDAL.GetAll(parametros, OrderBy);
        }

         public static List<Dcomrem> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DcomremDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Dcomrem> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DcomremDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Dcomrem> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DcomremDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DcomremDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DcomremDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DcomremDAL.GetMax(campo);
        }
        #endregion
    }
}
