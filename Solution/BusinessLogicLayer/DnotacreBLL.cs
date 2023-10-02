

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DnotacreBLL
    {
        #region Constructor

        public DnotacreBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Dnotacre obj)
        {
            return DnotacreDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Dnotacre obj)
        {
            return DnotacreDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Dnotacre obj)
        {
            return DnotacreDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Dnotacre obj)
        {
            return DnotacreDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Dnotacre obj)
        {
            return DnotacreDAL.Update(obj);
        }
        public static int Update(BLL bll, Dnotacre obj)
        {
            return DnotacreDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Dnotacre obj)
        {
            return DnotacreDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Dnotacre obj)
        {
            return DnotacreDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Dnotacre GetByPK(Dnotacre obj)
        {
            return DnotacreDAL.GetByPK(obj);
        }
        public static List<Dnotacre> GetAll(string WhereClause, string OrderBy)
        {
            return DnotacreDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Dnotacre> GetAll(WhereParams parametros, string OrderBy)
        {
            return DnotacreDAL.GetAll(parametros, OrderBy);
        }

         public static List<Dnotacre> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DnotacreDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Dnotacre> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DnotacreDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Dnotacre> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DnotacreDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DnotacreDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DnotacreDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DnotacreDAL.GetMax(campo);
        }
        #endregion
    }
}
