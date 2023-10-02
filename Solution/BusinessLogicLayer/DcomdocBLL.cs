

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DcomdocBLL
    {
        #region Constructor

        public DcomdocBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Dcomdoc obj)
        {
            return DcomdocDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Dcomdoc obj)
        {
            return DcomdocDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Dcomdoc obj)
        {
            return DcomdocDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Dcomdoc obj)
        {
            return DcomdocDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Dcomdoc obj)
        {
            return DcomdocDAL.Update(obj);
        }
        public static int Update(BLL bll, Dcomdoc obj)
        {
            return DcomdocDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Dcomdoc obj)
        {
            return DcomdocDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Dcomdoc obj)
        {
            return DcomdocDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Dcomdoc GetByPK(Dcomdoc obj)
        {
            return DcomdocDAL.GetByPK(obj);
        }
        public static List<Dcomdoc> GetAll(string WhereClause, string OrderBy)
        {
            return DcomdocDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Dcomdoc> GetAll(WhereParams parametros, string OrderBy)
        {
            return DcomdocDAL.GetAll(parametros, OrderBy);
        }

         public static List<Dcomdoc> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DcomdocDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Dcomdoc> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DcomdocDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Dcomdoc> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DcomdocDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DcomdocDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DcomdocDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DcomdocDAL.GetMax(campo);
        }
        #endregion
    }
}
