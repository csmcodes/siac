

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class CpersonaBLL
    {
        #region Constructor

        public CpersonaBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Cpersona obj)
        {
            return CpersonaDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Cpersona obj)
        {
            return CpersonaDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Cpersona obj)
        {
            return CpersonaDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Cpersona obj)
        {
            return CpersonaDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Cpersona obj)
        {
            return CpersonaDAL.Update(obj);
        }
        public static int Update(BLL bll, Cpersona obj)
        {
            return CpersonaDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Cpersona obj)
        {
            return CpersonaDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Cpersona obj)
        {
            return CpersonaDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Cpersona GetByPK(Cpersona obj)
        {
            return CpersonaDAL.GetByPK(obj);
        }
        public static List<Cpersona> GetAll(string WhereClause, string OrderBy)
        {
            return CpersonaDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Cpersona> GetAll(WhereParams parametros, string OrderBy)
        {
            return CpersonaDAL.GetAll(parametros, OrderBy);
        }

         public static List<Cpersona> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return CpersonaDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Cpersona> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return CpersonaDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Cpersona> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return CpersonaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return CpersonaDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return CpersonaDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return CpersonaDAL.GetMax(campo);
        }
        #endregion
    }
}
