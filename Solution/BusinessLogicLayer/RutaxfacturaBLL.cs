

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class RutaxfacturaBLL
    {
        #region Constructor

        public RutaxfacturaBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Rutaxfactura obj)
        {
            return RutaxfacturaDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Rutaxfactura obj)
        {
            return RutaxfacturaDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Rutaxfactura obj)
        {
            return RutaxfacturaDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Rutaxfactura obj)
        {
            return RutaxfacturaDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Rutaxfactura obj)
        {
            return RutaxfacturaDAL.Update(obj);
        }
        public static int Update(BLL bll, Rutaxfactura obj)
        {
            return RutaxfacturaDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Rutaxfactura obj)
        {
            return RutaxfacturaDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Rutaxfactura obj)
        {
            return RutaxfacturaDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Rutaxfactura GetByPK(Rutaxfactura obj)
        {
            return RutaxfacturaDAL.GetByPK(obj);
        }
        public static List<Rutaxfactura> GetAll(string WhereClause, string OrderBy)
        {
            return RutaxfacturaDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Rutaxfactura> GetAll(WhereParams parametros, string OrderBy)
        {
            return RutaxfacturaDAL.GetAll(parametros, OrderBy);
        }

         public static List<Rutaxfactura> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return RutaxfacturaDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Rutaxfactura> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return RutaxfacturaDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Rutaxfactura> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return RutaxfacturaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return RutaxfacturaDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return RutaxfacturaDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return RutaxfacturaDAL.GetMax(campo);
        }
        #endregion
    }
}
