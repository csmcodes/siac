

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class TpersonaBLL
    {
        #region Constructor

        public TpersonaBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Tpersona obj)
        {
            return TpersonaDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Tpersona obj)
        {
            return TpersonaDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Tpersona obj)
        {
            return TpersonaDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Tpersona obj)
        {
            return TpersonaDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Tpersona obj)
        {
            return TpersonaDAL.Update(obj);
        }
        public static int Update(BLL bll, Tpersona obj)
        {
            return TpersonaDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Tpersona obj)
        {
            return TpersonaDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Tpersona obj)
        {
            return TpersonaDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Tpersona GetByPK(Tpersona obj)
        {
            return TpersonaDAL.GetByPK(obj);
        }
        public static List<Tpersona> GetAll(string WhereClause, string OrderBy)
        {
            return TpersonaDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Tpersona> GetAll(WhereParams parametros, string OrderBy)
        {
            return TpersonaDAL.GetAll(parametros, OrderBy);
        }

         public static List<Tpersona> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return TpersonaDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Tpersona> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return TpersonaDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Tpersona> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return TpersonaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return TpersonaDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return TpersonaDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return TpersonaDAL.GetMax(campo);
        }
        #endregion
    }
}
