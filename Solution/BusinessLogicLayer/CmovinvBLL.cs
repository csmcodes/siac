

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class CmovinvBLL
    {
        #region Constructor

        public CmovinvBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Cmovinv obj)
        {
            return CmovinvDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Cmovinv obj)
        {
            return CmovinvDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Cmovinv obj)
        {
            return CmovinvDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Cmovinv obj)
        {
            return CmovinvDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Cmovinv obj)
        {
            return CmovinvDAL.Update(obj);
        }
        public static int Update(BLL bll, Cmovinv obj)
        {
            return CmovinvDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Cmovinv obj)
        {
            return CmovinvDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Cmovinv obj)
        {
            return CmovinvDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Cmovinv GetByPK(Cmovinv obj)
        {
            return CmovinvDAL.GetByPK(obj);
        }
        public static List<Cmovinv> GetAll(string WhereClause, string OrderBy)
        {
            return CmovinvDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Cmovinv> GetAll(WhereParams parametros, string OrderBy)
        {
            return CmovinvDAL.GetAll(parametros, OrderBy);
        }

         public static List<Cmovinv> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return CmovinvDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Cmovinv> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return CmovinvDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Cmovinv> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return CmovinvDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return CmovinvDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return CmovinvDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return CmovinvDAL.GetMax(campo);
        }
        #endregion
    }
}
