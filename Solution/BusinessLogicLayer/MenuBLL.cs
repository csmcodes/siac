

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class MenuBLL
    {
        #region Constructor

        public MenuBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Menu obj)
        {
            return MenuDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Menu obj)
        {
            return MenuDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Menu obj)
        {
            return MenuDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Menu obj)
        {
            return MenuDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Menu obj)
        {
            return MenuDAL.Update(obj);
        }
        public static int Update(BLL bll, Menu obj)
        {
            return MenuDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Menu obj)
        {
            return MenuDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Menu obj)
        {
            return MenuDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Menu GetByPK(Menu obj)
        {
            return MenuDAL.GetByPK(obj);
        }
        public static List<Menu> GetAll(string WhereClause, string OrderBy)
        {
            return MenuDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Menu> GetAll(WhereParams parametros, string OrderBy)
        {
            return MenuDAL.GetAll(parametros, OrderBy);
        }

         public static List<Menu> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return MenuDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Menu> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return MenuDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Menu> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return MenuDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return MenuDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return MenuDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return MenuDAL.GetMax(campo);
        }
        #endregion
    }
}
