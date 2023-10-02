

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class ProductoBLL
    {
        #region Constructor

        public ProductoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Producto obj)
        {
            return ProductoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Producto obj)
        {
            return ProductoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Producto obj)
        {
            return ProductoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Producto obj)
        {
            return ProductoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Producto obj)
        {
            return ProductoDAL.Update(obj);
        }
        public static int Update(BLL bll, Producto obj)
        {
            return ProductoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Producto obj)
        {
            return ProductoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Producto obj)
        {
            return ProductoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Producto GetByPK(Producto obj)
        {
            return ProductoDAL.GetByPK(obj);
        }

        public static Producto GetById(Producto obj)
        {
            return ProductoDAL.GetById(obj);
        }
        public static List<Producto> GetAll(string WhereClause, string OrderBy)
        {
            return ProductoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Producto> GetAll(WhereParams parametros, string OrderBy)
        {
            return ProductoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Producto> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return ProductoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Producto> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return ProductoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Producto> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return ProductoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return ProductoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return ProductoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return ProductoDAL.GetMax(campo);
        }
        #endregion
    }
}
