

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class UmedidaBLL
    {
        #region Constructor

        public UmedidaBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Umedida obj)
        {
            return UmedidaDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Umedida obj)
        {
            return UmedidaDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Umedida obj)
        {
            return UmedidaDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Umedida obj)
        {
            return UmedidaDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Umedida obj)
        {
            return UmedidaDAL.Update(obj);
        }
        public static int Update(BLL bll, Umedida obj)
        {
            return UmedidaDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Umedida obj)
        {
            return UmedidaDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Umedida obj)
        {
            return UmedidaDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Umedida GetByPK(Umedida obj)
        {
            return UmedidaDAL.GetByPK(obj);
        }
        public static List<Umedida> GetAll(string WhereClause, string OrderBy)
        {
            return UmedidaDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Umedida> GetAll(WhereParams parametros, string OrderBy)
        {
            return UmedidaDAL.GetAll(parametros, OrderBy);
        }

         public static List<Umedida> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return UmedidaDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Umedida> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return UmedidaDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Umedida> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return UmedidaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return UmedidaDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return UmedidaDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return UmedidaDAL.GetMax(campo);
        }
        #endregion
    }
}
