

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class LogBLL
    {
        #region Constructor

        public LogBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Log obj)
        {
            return LogDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Log obj)
        {
            return LogDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Log obj)
        {
            return LogDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Log obj)
        {
            return LogDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Log obj)
        {
            return LogDAL.Update(obj);
        }
        public static int Update(BLL bll, Log obj)
        {
            return LogDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Log obj)
        {
            return LogDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Log obj)
        {
            return LogDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Log GetByPK(Log obj)
        {
            return LogDAL.GetByPK(obj);
        }
        public static List<Log> GetAll(string WhereClause, string OrderBy)
        {
            return LogDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Log> GetAll(WhereParams parametros, string OrderBy)
        {
            return LogDAL.GetAll(parametros, OrderBy);
        }

         public static List<Log> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return LogDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Log> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return LogDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Log> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return LogDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return LogDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return LogDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return LogDAL.GetMax(campo);
        }
        #endregion
    }
}
