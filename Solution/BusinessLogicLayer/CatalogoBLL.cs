

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class CatalogoBLL
    {
        #region Constructor

        public CatalogoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Catalogo obj)
        {
            return CatalogoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Catalogo obj)
        {
            return CatalogoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Catalogo obj)
        {
            return CatalogoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Catalogo obj)
        {
            return CatalogoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Catalogo obj)
        {
            return CatalogoDAL.Update(obj);
        }
        public static int Update(BLL bll, Catalogo obj)
        {
            return CatalogoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Catalogo obj)
        {
            return CatalogoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Catalogo obj)
        {
            return CatalogoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Catalogo GetByPK(Catalogo obj)
        {
            return CatalogoDAL.GetByPK(obj);
        }
        public static List<Catalogo> GetAll(string WhereClause, string OrderBy)
        {
            return CatalogoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Catalogo> GetAll(WhereParams parametros, string OrderBy)
        {
            return CatalogoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Catalogo> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return CatalogoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Catalogo> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return CatalogoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Catalogo> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return CatalogoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return CatalogoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return CatalogoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return CatalogoDAL.GetMax(campo);
        }
        #endregion
    }
}
