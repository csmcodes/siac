

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class CcomenvBLL
    {
        #region Constructor

        public CcomenvBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Ccomenv obj)
        {
            return CcomenvDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Ccomenv obj)
        {
            return CcomenvDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Ccomenv obj)
        {
            return CcomenvDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Ccomenv obj)
        {
            return CcomenvDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Ccomenv obj)
        {
            return CcomenvDAL.Update(obj);
        }
        public static int Update(BLL bll, Ccomenv obj)
        {
            return CcomenvDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Ccomenv obj)
        {
            return CcomenvDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Ccomenv obj)
        {
            return CcomenvDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Ccomenv GetByPK(Ccomenv obj)
        {
            return CcomenvDAL.GetByPK(obj);
        }
        public static List<Ccomenv> GetAll(string WhereClause, string OrderBy)
        {
            return CcomenvDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Ccomenv> GetAll(WhereParams parametros, string OrderBy)
        {
            return CcomenvDAL.GetAll(parametros, OrderBy);
        }

         public static List<Ccomenv> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return CcomenvDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Ccomenv> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return CcomenvDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Ccomenv> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return CcomenvDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return CcomenvDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return CcomenvDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return CcomenvDAL.GetMax(campo);
        }
        #endregion
    }
}
