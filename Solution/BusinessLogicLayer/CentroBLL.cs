

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class CentroBLL
    {
        #region Constructor

        public CentroBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Centro obj)
        {
            return CentroDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Centro obj)
        {
            return CentroDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Centro obj)
        {
            return CentroDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Centro obj)
        {
            return CentroDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Centro obj)
        {
            return CentroDAL.Update(obj);
        }
        public static int Update(BLL bll, Centro obj)
        {
            return CentroDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Centro obj)
        {
            return CentroDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Centro obj)
        {
            return CentroDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Centro GetByPK(Centro obj)
        {
            return CentroDAL.GetByPK(obj);
        }
        public static Centro GetById(Centro obj)
        {
            return CentroDAL.GetById(obj);
        }
        public static List<Centro> GetAll(string WhereClause, string OrderBy)
        {
            return CentroDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Centro> GetAll(WhereParams parametros, string OrderBy)
        {
            return CentroDAL.GetAll(parametros, OrderBy);
        }

         public static List<Centro> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return CentroDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Centro> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return CentroDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Centro> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return CentroDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return CentroDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return CentroDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return CentroDAL.GetMax(campo);
        }
        #endregion
    }
}
