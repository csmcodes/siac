

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class SalbanBLL
    {
        #region Constructor

        public SalbanBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Salban obj)
        {
            return SalbanDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Salban obj)
        {
            return SalbanDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Salban obj)
        {
            return SalbanDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Salban obj)
        {
            return SalbanDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Salban obj)
        {
            return SalbanDAL.Update(obj);
        }
        public static int Update(BLL bll, Salban obj)
        {
            return SalbanDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Salban obj)
        {
            return SalbanDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Salban obj)
        {
            return SalbanDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Salban GetByPK(Salban obj)
        {
            return SalbanDAL.GetByPK(obj);
        }
        public static List<Salban> GetAll(string WhereClause, string OrderBy)
        {
            return SalbanDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Salban> GetAll(WhereParams parametros, string OrderBy)
        {
            return SalbanDAL.GetAll(parametros, OrderBy);
        }

         public static List<Salban> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return SalbanDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Salban> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return SalbanDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Salban> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return SalbanDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return SalbanDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return SalbanDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return SalbanDAL.GetMax(campo);
        }
        #endregion

        #region Get Sum

        public static decimal GetSum(string campo, string WhereClause, string OrderBy)
        {
            return SalbanDAL.GetSum(campo, WhereClause, OrderBy);
        }

        public static decimal GetSum(string campo, WhereParams parametros, string OrderBy)
        {
            return SalbanDAL.GetSum(campo, parametros, OrderBy);
        }
        #endregion
    }
}
