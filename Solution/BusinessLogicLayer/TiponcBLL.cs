

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class TiponcBLL
    {
        #region Constructor

        public TiponcBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Tiponc obj)
        {
            return TiponcDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Tiponc obj)
        {
            return TiponcDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Tiponc obj)
        {
            return TiponcDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Tiponc obj)
        {
            return TiponcDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Tiponc obj)
        {
            return TiponcDAL.Update(obj);
        }
        public static int Update(BLL bll, Tiponc obj)
        {
            return TiponcDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Tiponc obj)
        {
            return TiponcDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Tiponc obj)
        {
            return TiponcDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Tiponc GetByPK(Tiponc obj)
        {
            return TiponcDAL.GetByPK(obj);
        }
        public static List<Tiponc> GetAll(string WhereClause, string OrderBy)
        {
            return TiponcDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Tiponc> GetAll(WhereParams parametros, string OrderBy)
        {
            return TiponcDAL.GetAll(parametros, OrderBy);
        }

         public static List<Tiponc> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return TiponcDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Tiponc> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return TiponcDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Tiponc> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return TiponcDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return TiponcDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return TiponcDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return TiponcDAL.GetMax(campo);
        }
        #endregion
    }
}
