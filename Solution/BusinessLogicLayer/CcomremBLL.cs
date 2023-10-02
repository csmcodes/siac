

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class CcomremBLL
    {
        #region Constructor

        public CcomremBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Ccomrem obj)
        {
            return CcomremDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Ccomrem obj)
        {
            return CcomremDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Ccomrem obj)
        {
            return CcomremDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Ccomrem obj)
        {
            return CcomremDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Ccomrem obj)
        {
            return CcomremDAL.Update(obj);
        }
        public static int Update(BLL bll, Ccomrem obj)
        {
            return CcomremDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Ccomrem obj)
        {
            return CcomremDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Ccomrem obj)
        {
            return CcomremDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Ccomrem GetByPK(Ccomrem obj)
        {
            return CcomremDAL.GetByPK(obj);
        }
        public static List<Ccomrem> GetAll(string WhereClause, string OrderBy)
        {
            return CcomremDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Ccomrem> GetAll(WhereParams parametros, string OrderBy)
        {
            return CcomremDAL.GetAll(parametros, OrderBy);
        }

         public static List<Ccomrem> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return CcomremDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Ccomrem> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return CcomremDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Ccomrem> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return CcomremDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return CcomremDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return CcomremDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return CcomremDAL.GetMax(campo);
        }
        #endregion
    }
}
