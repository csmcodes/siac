

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class PerfilxmenuBLL
    {
        #region Constructor

        public PerfilxmenuBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Perfilxmenu obj)
        {
            return PerfilxmenuDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Perfilxmenu obj)
        {
            return PerfilxmenuDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Perfilxmenu obj)
        {
            return PerfilxmenuDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Perfilxmenu obj)
        {
            return PerfilxmenuDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Perfilxmenu obj)
        {
            return PerfilxmenuDAL.Update(obj);
        }
        public static int Update(BLL bll, Perfilxmenu obj)
        {
            return PerfilxmenuDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Perfilxmenu obj)
        {
            return PerfilxmenuDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Perfilxmenu obj)
        {
            return PerfilxmenuDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Perfilxmenu GetByPK(Perfilxmenu obj)
        {
            return PerfilxmenuDAL.GetByPK(obj);
        }
        public static List<Perfilxmenu> GetAll(string WhereClause, string OrderBy)
        {
            return PerfilxmenuDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Perfilxmenu> GetAll(WhereParams parametros, string OrderBy)
        {
            return PerfilxmenuDAL.GetAll(parametros, OrderBy);
        }

         public static List<Perfilxmenu> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return PerfilxmenuDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Perfilxmenu> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return PerfilxmenuDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Perfilxmenu> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return PerfilxmenuDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return PerfilxmenuDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return PerfilxmenuDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return PerfilxmenuDAL.GetMax(campo);
        }
        #endregion
    }
}
