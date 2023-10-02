

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class SaldoinvBLL
    {
        #region Constructor

        public SaldoinvBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Saldoinv obj)
        {
            return SaldoinvDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Saldoinv obj)
        {
            return SaldoinvDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Saldoinv obj)
        {
            return SaldoinvDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Saldoinv obj)
        {
            return SaldoinvDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Saldoinv obj)
        {
            return SaldoinvDAL.Update(obj);
        }
        public static int Update(BLL bll, Saldoinv obj)
        {
            return SaldoinvDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Saldoinv obj)
        {
            return SaldoinvDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Saldoinv obj)
        {
            return SaldoinvDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Saldoinv GetByPK(Saldoinv obj)
        {
            return SaldoinvDAL.GetByPK(obj);
        }
        public static List<Saldoinv> GetAll(string WhereClause, string OrderBy)
        {
            return SaldoinvDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Saldoinv> GetAll(WhereParams parametros, string OrderBy)
        {
            return SaldoinvDAL.GetAll(parametros, OrderBy);
        }

         public static List<Saldoinv> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return SaldoinvDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Saldoinv> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return SaldoinvDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Saldoinv> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return SaldoinvDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return SaldoinvDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return SaldoinvDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return SaldoinvDAL.GetMax(campo);
        }
        #endregion
    }
}
