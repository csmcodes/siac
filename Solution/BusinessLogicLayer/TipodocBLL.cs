

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class TipodocBLL
    {
        #region Constructor

        public TipodocBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Tipodoc obj)
        {
            return TipodocDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Tipodoc obj)
        {
            return TipodocDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Tipodoc obj)
        {
            return TipodocDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Tipodoc obj)
        {
            return TipodocDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Tipodoc obj)
        {
            return TipodocDAL.Update(obj);
        }
        public static int Update(BLL bll, Tipodoc obj)
        {
            return TipodocDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Tipodoc obj)
        {
            return TipodocDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Tipodoc obj)
        {
            return TipodocDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Tipodoc GetByPK(Tipodoc obj)
        {
            return TipodocDAL.GetByPK(obj);
        }
        public static Tipodoc GetById(Tipodoc obj)
        {
            return TipodocDAL.GetById(obj);
        }
        public static List<Tipodoc> GetAll(string WhereClause, string OrderBy)
        {
            return TipodocDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Tipodoc> GetAll(WhereParams parametros, string OrderBy)
        {
            return TipodocDAL.GetAll(parametros, OrderBy);
        }

         public static List<Tipodoc> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return TipodocDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Tipodoc> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return TipodocDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Tipodoc> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return TipodocDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return TipodocDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return TipodocDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return TipodocDAL.GetMax(campo);
        }
        #endregion
    }
}
