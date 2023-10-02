

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class ComprobantehistorialBLL
    {
        #region Constructor

        public ComprobantehistorialBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Comprobantehistorial obj)
        {
            return ComprobantehistorialDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Comprobantehistorial obj)
        {
            return ComprobantehistorialDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Comprobantehistorial obj)
        {
            return ComprobantehistorialDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Comprobantehistorial obj)
        {
            return ComprobantehistorialDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Comprobantehistorial obj)
        {
            return ComprobantehistorialDAL.Update(obj);
        }
        public static int Update(BLL bll, Comprobantehistorial obj)
        {
            return ComprobantehistorialDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Comprobantehistorial obj)
        {
            return ComprobantehistorialDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Comprobantehistorial obj)
        {
            return ComprobantehistorialDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Comprobantehistorial GetByPK(Comprobantehistorial obj)
        {
            return ComprobantehistorialDAL.GetByPK(obj);
        }
        public static List<Comprobantehistorial> GetAll(string WhereClause, string OrderBy)
        {
            return ComprobantehistorialDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Comprobantehistorial> GetAll(WhereParams parametros, string OrderBy)
        {
            return ComprobantehistorialDAL.GetAll(parametros, OrderBy);
        }

         public static List<Comprobantehistorial> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return ComprobantehistorialDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Comprobantehistorial> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return ComprobantehistorialDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Comprobantehistorial> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return ComprobantehistorialDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return ComprobantehistorialDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return ComprobantehistorialDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return ComprobantehistorialDAL.GetMax(campo);
        }
        #endregion
    }
}
