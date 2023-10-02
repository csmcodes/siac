

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DlistaprecioBLL
    {
        #region Constructor

        public DlistaprecioBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Dlistaprecio obj)
        {
            return DlistaprecioDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Dlistaprecio obj)
        {
            return DlistaprecioDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Dlistaprecio obj)
        {
            return DlistaprecioDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Dlistaprecio obj)
        {
            return DlistaprecioDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Dlistaprecio obj)
        {
            return DlistaprecioDAL.Update(obj);
        }
        public static int Update(BLL bll, Dlistaprecio obj)
        {
            return DlistaprecioDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Dlistaprecio obj)
        {
            return DlistaprecioDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Dlistaprecio obj)
        {
            return DlistaprecioDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Dlistaprecio GetByPK(Dlistaprecio obj)
        {
            return DlistaprecioDAL.GetByPK(obj);
        }
        public static List<Dlistaprecio> GetAll(string WhereClause, string OrderBy)
        {
            return DlistaprecioDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Dlistaprecio> GetAll(WhereParams parametros, string OrderBy)
        {
            return DlistaprecioDAL.GetAll(parametros, OrderBy);
        }

         public static List<Dlistaprecio> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DlistaprecioDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Dlistaprecio> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DlistaprecioDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Dlistaprecio> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DlistaprecioDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DlistaprecioDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DlistaprecioDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DlistaprecioDAL.GetMax(campo);
        }
        #endregion
    }
}
