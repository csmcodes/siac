

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class CuentaBLL
    {
        #region Constructor

        public CuentaBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Cuenta obj)
        {
            return CuentaDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Cuenta obj)
        {
            return CuentaDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Cuenta obj)
        {
            return CuentaDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Cuenta obj)
        {
            return CuentaDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Cuenta obj)
        {
            return CuentaDAL.Update(obj);
        }
        public static int Update(BLL bll, Cuenta obj)
        {
            return CuentaDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Cuenta obj)
        {
            return CuentaDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Cuenta obj)
        {
            return CuentaDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Cuenta GetByPK(Cuenta obj)
        {
            return CuentaDAL.GetByPK(obj);
        }
        public static List<Cuenta> GetAll(string WhereClause, string OrderBy)
        {
            return CuentaDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Cuenta> GetAll(WhereParams parametros, string OrderBy)
        {
            return CuentaDAL.GetAll(parametros, OrderBy);
        }

         public static List<Cuenta> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return CuentaDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Cuenta> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return CuentaDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Cuenta> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return CuentaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return CuentaDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return CuentaDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return CuentaDAL.GetMax(campo);
        }
        #endregion
    }
}
