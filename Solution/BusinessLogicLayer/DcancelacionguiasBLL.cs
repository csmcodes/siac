

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DcancelacionguiasBLL
    {
        #region Constructor

        public DcancelacionguiasBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Dcancelacionguias obj)
        {
            return DcancelacionguiasDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Dcancelacionguias obj)
        {
            return DcancelacionguiasDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Dcancelacionguias obj)
        {
            return DcancelacionguiasDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Dcancelacionguias obj)
        {
            return DcancelacionguiasDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Dcancelacionguias obj)
        {
            return DcancelacionguiasDAL.Update(obj);
        }
        public static int Update(BLL bll, Dcancelacionguias obj)
        {
            return DcancelacionguiasDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Dcancelacionguias obj)
        {
            return DcancelacionguiasDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Dcancelacionguias obj)
        {
            return DcancelacionguiasDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Dcancelacionguias GetByPK(Dcancelacionguias obj)
        {
            return DcancelacionguiasDAL.GetByPK(obj);
        }
        public static List<Dcancelacionguias> GetAll(string WhereClause, string OrderBy)
        {
            return DcancelacionguiasDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Dcancelacionguias> GetAll(WhereParams parametros, string OrderBy)
        {
            return DcancelacionguiasDAL.GetAll(parametros, OrderBy);
        }

         public static List<Dcancelacionguias> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DcancelacionguiasDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Dcancelacionguias> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DcancelacionguiasDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Dcancelacionguias> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DcancelacionguiasDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DcancelacionguiasDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DcancelacionguiasDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DcancelacionguiasDAL.GetMax(campo);
        }
        #endregion
    }
}
