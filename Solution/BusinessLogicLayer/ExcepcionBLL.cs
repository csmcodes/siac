

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class ExcepcionBLL
    {
        #region Constructor

        public ExcepcionBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Excepcion obj)
        {
            return ExcepcionDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Excepcion obj)
        {
            return ExcepcionDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Excepcion obj)
        {
            return ExcepcionDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Excepcion obj)
        {
            return ExcepcionDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Excepcion obj)
        {
            return ExcepcionDAL.Update(obj);
        }
        public static int Update(BLL bll, Excepcion obj)
        {
            return ExcepcionDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Excepcion obj)
        {
            return ExcepcionDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Excepcion obj)
        {
            return ExcepcionDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Excepcion GetByPK(Excepcion obj)
        {
            return ExcepcionDAL.GetByPK(obj);
        }
        public static List<Excepcion> GetAll(string WhereClause, string OrderBy)
        {
            return ExcepcionDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Excepcion> GetAll(WhereParams parametros, string OrderBy)
        {
            return ExcepcionDAL.GetAll(parametros, OrderBy);
        }

         public static List<Excepcion> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return ExcepcionDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Excepcion> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return ExcepcionDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Excepcion> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return ExcepcionDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return ExcepcionDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return ExcepcionDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return ExcepcionDAL.GetMax(campo);
        }
        #endregion
    }
}
