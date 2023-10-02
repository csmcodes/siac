

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DretencionBLL
    {
        #region Constructor

        public DretencionBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Dretencion obj)
        {
            return DretencionDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Dretencion obj)
        {
            return DretencionDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Dretencion obj)
        {
            return DretencionDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Dretencion obj)
        {
            return DretencionDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Dretencion obj)
        {
            return DretencionDAL.Update(obj);
        }
        public static int Update(BLL bll, Dretencion obj)
        {
            return DretencionDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Dretencion obj)
        {
            return DretencionDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Dretencion obj)
        {
            return DretencionDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Dretencion GetByPK(Dretencion obj)
        {
            return DretencionDAL.GetByPK(obj);
        }
        public static List<Dretencion> GetAll(string WhereClause, string OrderBy)
        {
            return DretencionDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Dretencion> GetAll(WhereParams parametros, string OrderBy)
        {
            return DretencionDAL.GetAll(parametros, OrderBy);
        }

         public static List<Dretencion> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DretencionDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Dretencion> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DretencionDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Dretencion> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DretencionDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DretencionDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DretencionDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DretencionDAL.GetMax(campo);
        }
        #endregion
    }
}
