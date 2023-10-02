

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class CalculoprecioBLL
    {
        #region Constructor

        public CalculoprecioBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Calculoprecio obj)
        {
            return CalculoprecioDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Calculoprecio obj)
        {
            return CalculoprecioDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Calculoprecio obj)
        {
            return CalculoprecioDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Calculoprecio obj)
        {
            return CalculoprecioDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Calculoprecio obj)
        {
            return CalculoprecioDAL.Update(obj);
        }
        public static int Update(BLL bll, Calculoprecio obj)
        {
            return CalculoprecioDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Calculoprecio obj)
        {
            return CalculoprecioDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Calculoprecio obj)
        {
            return CalculoprecioDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Calculoprecio GetByPK(Calculoprecio obj)
        {
            return CalculoprecioDAL.GetByPK(obj);
        }
        public static List<Calculoprecio> GetAll(string WhereClause, string OrderBy)
        {
            return CalculoprecioDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Calculoprecio> GetAll(WhereParams parametros, string OrderBy)
        {
            return CalculoprecioDAL.GetAll(parametros, OrderBy);
        }

         public static List<Calculoprecio> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return CalculoprecioDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Calculoprecio> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return CalculoprecioDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Calculoprecio> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return CalculoprecioDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return CalculoprecioDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return CalculoprecioDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return CalculoprecioDAL.GetMax(campo);
        }
        #endregion
    }
}
