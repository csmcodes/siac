

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DmovinvBLL
    {
        #region Constructor

        public DmovinvBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Dmovinv obj)
        {
            return DmovinvDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Dmovinv obj)
        {
            return DmovinvDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Dmovinv obj)
        {
            return DmovinvDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Dmovinv obj)
        {
            return DmovinvDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Dmovinv obj)
        {
            return DmovinvDAL.Update(obj);
        }
        public static int Update(BLL bll, Dmovinv obj)
        {
            return DmovinvDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Dmovinv obj)
        {
            return DmovinvDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Dmovinv obj)
        {
            return DmovinvDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Dmovinv GetByPK(Dmovinv obj)
        {
            return DmovinvDAL.GetByPK(obj);
        }
        public static List<Dmovinv> GetAll(string WhereClause, string OrderBy)
        {
            return DmovinvDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Dmovinv> GetAll(WhereParams parametros, string OrderBy)
        {
            return DmovinvDAL.GetAll(parametros, OrderBy);
        }

         public static List<Dmovinv> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DmovinvDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Dmovinv> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DmovinvDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Dmovinv> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DmovinvDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DmovinvDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DmovinvDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DmovinvDAL.GetMax(campo);
        }
        #endregion
    }
}
