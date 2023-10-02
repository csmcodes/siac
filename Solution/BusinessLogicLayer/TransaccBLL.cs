

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class TransaccBLL
    {
        #region Constructor

        public TransaccBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Transacc obj)
        {
            return TransaccDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Transacc obj)
        {
            return TransaccDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Transacc obj)
        {
            return TransaccDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Transacc obj)
        {
            return TransaccDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Transacc obj)
        {
            return TransaccDAL.Update(obj);
        }
        public static int Update(BLL bll, Transacc obj)
        {
            return TransaccDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Transacc obj)
        {
            return TransaccDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Transacc obj)
        {
            return TransaccDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Transacc GetByPK(Transacc obj)
        {
            return TransaccDAL.GetByPK(obj);
        }
        public static List<Transacc> GetAll(string WhereClause, string OrderBy)
        {
            return TransaccDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Transacc> GetAll(WhereParams parametros, string OrderBy)
        {
            return TransaccDAL.GetAll(parametros, OrderBy);
        }

         public static List<Transacc> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return TransaccDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Transacc> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return TransaccDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Transacc> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return TransaccDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return TransaccDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return TransaccDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return TransaccDAL.GetMax(campo);
        }
        #endregion
    }
}
