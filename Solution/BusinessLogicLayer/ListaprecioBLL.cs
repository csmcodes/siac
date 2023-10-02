

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class ListaprecioBLL
    {
        #region Constructor

        public ListaprecioBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Listaprecio obj)
        {
            return ListaprecioDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Listaprecio obj)
        {
            return ListaprecioDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Listaprecio obj)
        {
            return ListaprecioDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Listaprecio obj)
        {
            return ListaprecioDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Listaprecio obj)
        {
            return ListaprecioDAL.Update(obj);
        }
        public static int Update(BLL bll, Listaprecio obj)
        {
            return ListaprecioDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Listaprecio obj)
        {
            return ListaprecioDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Listaprecio obj)
        {
            return ListaprecioDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Listaprecio GetByPK(Listaprecio obj)
        {
            return ListaprecioDAL.GetByPK(obj);
        }

        public static Listaprecio GetById(Listaprecio obj)
        {
            return ListaprecioDAL.GetById(obj);
        }

        public static List<Listaprecio> GetAll(string WhereClause, string OrderBy)
        {
            return ListaprecioDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Listaprecio> GetAll(WhereParams parametros, string OrderBy)
        {
            return ListaprecioDAL.GetAll(parametros, OrderBy);
        }

         public static List<Listaprecio> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return ListaprecioDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Listaprecio> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return ListaprecioDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Listaprecio> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return ListaprecioDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return ListaprecioDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return ListaprecioDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return ListaprecioDAL.GetMax(campo);
        }
        #endregion
    }
}
