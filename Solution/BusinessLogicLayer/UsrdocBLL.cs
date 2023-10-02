

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class UsrdocBLL
    {
        #region Constructor

        public UsrdocBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Usrdoc obj)
        {
            return UsrdocDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Usrdoc obj)
        {
            return UsrdocDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Usrdoc obj)
        {
            return UsrdocDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Usrdoc obj)
        {
            return UsrdocDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Usrdoc obj)
        {
            return UsrdocDAL.Update(obj);
        }
        public static int Update(BLL bll, Usrdoc obj)
        {
            return UsrdocDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Usrdoc obj)
        {
            return UsrdocDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Usrdoc obj)
        {
            return UsrdocDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Usrdoc GetByPK(Usrdoc obj)
        {
            return UsrdocDAL.GetByPK(obj);
        }
        public static List<Usrdoc> GetAll(string WhereClause, string OrderBy)
        {
            return UsrdocDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Usrdoc> GetAll(WhereParams parametros, string OrderBy)
        {
            return UsrdocDAL.GetAll(parametros, OrderBy);
        }

         public static List<Usrdoc> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return UsrdocDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Usrdoc> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return UsrdocDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Usrdoc> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return UsrdocDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return UsrdocDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return UsrdocDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return UsrdocDAL.GetMax(campo);
        }
        #endregion
    }
}
