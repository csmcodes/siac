

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DcancelacionBLL
    {
        #region Constructor

        public DcancelacionBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Dcancelacion obj)
        {
            return DcancelacionDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Dcancelacion obj)
        {
            return DcancelacionDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Dcancelacion obj)
        {
            return DcancelacionDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Dcancelacion obj)
        {
            return DcancelacionDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Dcancelacion obj)
        {
            return DcancelacionDAL.Update(obj);
        }
        public static int Update(BLL bll, Dcancelacion obj)
        {
            return DcancelacionDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Dcancelacion obj)
        {
            return DcancelacionDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Dcancelacion obj)
        {
            return DcancelacionDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Dcancelacion GetByPK(Dcancelacion obj)
        {
            return DcancelacionDAL.GetByPK(obj);
        }
        public static List<Dcancelacion> GetAll(string WhereClause, string OrderBy)
        {
            return DcancelacionDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Dcancelacion> GetAll(WhereParams parametros, string OrderBy)
        {
            return DcancelacionDAL.GetAll(parametros, OrderBy);
        }

         public static List<Dcancelacion> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DcancelacionDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Dcancelacion> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DcancelacionDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Dcancelacion> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DcancelacionDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DcancelacionDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DcancelacionDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DcancelacionDAL.GetMax(campo);
        }
        #endregion
    }
}
