

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class MonedaBLL
    {
        #region Constructor

        public MonedaBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Moneda obj)
        {
            return MonedaDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Moneda obj)
        {
            return MonedaDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Moneda obj)
        {
            return MonedaDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Moneda obj)
        {
            return MonedaDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Moneda obj)
        {
            return MonedaDAL.Update(obj);
        }
        public static int Update(BLL bll, Moneda obj)
        {
            return MonedaDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Moneda obj)
        {
            return MonedaDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Moneda obj)
        {
            return MonedaDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Moneda GetByPK(Moneda obj)
        {
            return MonedaDAL.GetByPK(obj);
        }
        public static List<Moneda> GetAll(string WhereClause, string OrderBy)
        {
            return MonedaDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Moneda> GetAll(WhereParams parametros, string OrderBy)
        {
            return MonedaDAL.GetAll(parametros, OrderBy);
        }

         public static List<Moneda> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return MonedaDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Moneda> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return MonedaDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Moneda> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return MonedaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return MonedaDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return MonedaDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return MonedaDAL.GetMax(campo);
        }
        #endregion
    }
}
