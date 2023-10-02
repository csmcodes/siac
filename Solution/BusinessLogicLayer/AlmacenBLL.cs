

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class AlmacenBLL
    {
        #region Constructor

        public AlmacenBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Almacen obj)
        {
            return AlmacenDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Almacen obj)
        {
            return AlmacenDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Almacen obj)
        {
            return AlmacenDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Almacen obj)
        {
            return AlmacenDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Almacen obj)
        {
            return AlmacenDAL.Update(obj);
        }
        public static int Update(BLL bll, Almacen obj)
        {
            return AlmacenDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Almacen obj)
        {
            return AlmacenDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Almacen obj)
        {
            return AlmacenDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Almacen GetByPK(Almacen obj)
        {
            return AlmacenDAL.GetByPK(obj);
        }
        public static List<Almacen> GetAll(string WhereClause, string OrderBy)
        {
            return AlmacenDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Almacen> GetAll(WhereParams parametros, string OrderBy)
        {
            return AlmacenDAL.GetAll(parametros, OrderBy);
        }

         public static List<Almacen> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return AlmacenDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Almacen> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return AlmacenDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Almacen> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return AlmacenDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return AlmacenDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return AlmacenDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return AlmacenDAL.GetMax(campo);
        }
        #endregion
    }
}
