

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class TotalBLL
    {
        #region Constructor

        public TotalBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Total obj)
        {
            return TotalDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Total obj)
        {
            return TotalDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Total obj)
        {
            return TotalDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Total obj)
        {
            return TotalDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Total obj)
        {
            return TotalDAL.Update(obj);
        }
        public static int Update(BLL bll, Total obj)
        {
            return TotalDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Total obj)
        {
            return TotalDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Total obj)
        {
            return TotalDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Total GetByPK(Total obj)
        {
            return TotalDAL.GetByPK(obj);
        }
        public static List<Total> GetAll(string WhereClause, string OrderBy)
        {
            return TotalDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Total> GetAll(WhereParams parametros, string OrderBy)
        {
            return TotalDAL.GetAll(parametros, OrderBy);
        }

         public static List<Total> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return TotalDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Total> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return TotalDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Total> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return TotalDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return TotalDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return TotalDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return TotalDAL.GetMax(campo);
        }
        #endregion
    }
}
