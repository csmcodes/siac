

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class BodegaBLL
    {
        #region Constructor

        public BodegaBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Bodega obj)
        {
            return BodegaDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Bodega obj)
        {
            return BodegaDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Bodega obj)
        {
            return BodegaDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Bodega obj)
        {
            return BodegaDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Bodega obj)
        {
            return BodegaDAL.Update(obj);
        }
        public static int Update(BLL bll, Bodega obj)
        {
            return BodegaDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Bodega obj)
        {
            return BodegaDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Bodega obj)
        {
            return BodegaDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Bodega GetByPK(Bodega obj)
        {
            return BodegaDAL.GetByPK(obj);
        }
        public static List<Bodega> GetAll(string WhereClause, string OrderBy)
        {
            return BodegaDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Bodega> GetAll(WhereParams parametros, string OrderBy)
        {
            return BodegaDAL.GetAll(parametros, OrderBy);
        }

         public static List<Bodega> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return BodegaDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Bodega> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return BodegaDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Bodega> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return BodegaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return BodegaDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return BodegaDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return BodegaDAL.GetMax(campo);
        }
        #endregion
    }
}
