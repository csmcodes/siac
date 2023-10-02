

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DtipodocBLL
    {
        #region Constructor

        public DtipodocBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Dtipodoc obj)
        {
            return DtipodocDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Dtipodoc obj)
        {
            return DtipodocDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Dtipodoc obj)
        {
            return DtipodocDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Dtipodoc obj)
        {
            return DtipodocDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Dtipodoc obj)
        {
            return DtipodocDAL.Update(obj);
        }
        public static int Update(BLL bll, Dtipodoc obj)
        {
            return DtipodocDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Dtipodoc obj)
        {
            return DtipodocDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Dtipodoc obj)
        {
            return DtipodocDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Dtipodoc GetByPK(Dtipodoc obj)
        {
            return DtipodocDAL.GetByPK(obj);
        }
        public static List<Dtipodoc> GetAll(string WhereClause, string OrderBy)
        {
            return DtipodocDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Dtipodoc> GetAll(WhereParams parametros, string OrderBy)
        {
            return DtipodocDAL.GetAll(parametros, OrderBy);
        }

         public static List<Dtipodoc> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DtipodocDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Dtipodoc> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DtipodocDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Dtipodoc> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DtipodocDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DtipodocDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DtipodocDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DtipodocDAL.GetMax(campo);
        }
        #endregion
    }
}
