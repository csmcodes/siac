

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DcontableBLL
    {
        #region Constructor

        public DcontableBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Dcontable obj)
        {
            return DcontableDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Dcontable obj)
        {
            return DcontableDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Dcontable obj)
        {
            return DcontableDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Dcontable obj)
        {
            return DcontableDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Dcontable obj)
        {
            return DcontableDAL.Update(obj);
        }
        public static int Update(BLL bll, Dcontable obj)
        {
            return DcontableDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Dcontable obj)
        {
            return DcontableDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Dcontable obj)
        {
            return DcontableDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Dcontable GetByPK(Dcontable obj)
        {
            return DcontableDAL.GetByPK(obj);
        }
        public static List<Dcontable> GetAll(string WhereClause, string OrderBy)
        {
            return DcontableDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Dcontable> GetAll(WhereParams parametros, string OrderBy)
        {
            return DcontableDAL.GetAll(parametros, OrderBy);
        }

         public static List<Dcontable> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DcontableDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Dcontable> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DcontableDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Dcontable> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DcontableDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DcontableDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DcontableDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DcontableDAL.GetMax(campo);
        }
     
        #endregion

        #region Get Sum

        public static decimal GetSum(string campo, string WhereClause, string OrderBy)
        {
            return DcontableDAL.GetSum(campo,WhereClause, OrderBy);
        }

        public static decimal GetSum(string campo, WhereParams parametros, string OrderBy)
        {
            return DcontableDAL.GetSum(campo,parametros, OrderBy);
        }
        #endregion
    }
}
