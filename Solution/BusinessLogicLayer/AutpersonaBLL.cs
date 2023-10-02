

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class AutpersonaBLL
    {
        #region Constructor

        public AutpersonaBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Autpersona obj)
        {
            return AutpersonaDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Autpersona obj)
        {
            return AutpersonaDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Autpersona obj)
        {
            return AutpersonaDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Autpersona obj)
        {
            return AutpersonaDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Autpersona obj)
        {
            return AutpersonaDAL.Update(obj);
        }
        public static int Update(BLL bll, Autpersona obj)
        {
            return AutpersonaDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Autpersona obj)
        {
            return AutpersonaDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Autpersona obj)
        {
            return AutpersonaDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Autpersona GetByPK(Autpersona obj)
        {
            return AutpersonaDAL.GetByPK(obj);
        }
        public static List<Autpersona> GetAll(string WhereClause, string OrderBy)
        {
            return AutpersonaDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Autpersona> GetAll(WhereParams parametros, string OrderBy)
        {
            return AutpersonaDAL.GetAll(parametros, OrderBy);
        }

         public static List<Autpersona> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return AutpersonaDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Autpersona> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return AutpersonaDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Autpersona> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return AutpersonaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return AutpersonaDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return AutpersonaDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return AutpersonaDAL.GetMax(campo);
        }
        #endregion
    }
}
