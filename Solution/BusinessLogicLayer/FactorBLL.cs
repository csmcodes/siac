

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class FactorBLL
    {
        #region Constructor

        public FactorBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Factor obj)
        {
            return FactorDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Factor obj)
        {
            return FactorDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Factor obj)
        {
            return FactorDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Factor obj)
        {
            return FactorDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Factor obj)
        {
            return FactorDAL.Update(obj);
        }
        public static int Update(BLL bll, Factor obj)
        {
            return FactorDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Factor obj)
        {
            return FactorDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Factor obj)
        {
            return FactorDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Factor GetByPK(Factor obj)
        {
            return FactorDAL.GetByPK(obj);
        }
        public static List<Factor> GetAll(string WhereClause, string OrderBy)
        {
            return FactorDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Factor> GetAll(WhereParams parametros, string OrderBy)
        {
            return FactorDAL.GetAll(parametros, OrderBy);
        }

         public static List<Factor> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return FactorDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Factor> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return FactorDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Factor> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return FactorDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return FactorDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return FactorDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return FactorDAL.GetMax(campo);
        }
        #endregion
    }
}
