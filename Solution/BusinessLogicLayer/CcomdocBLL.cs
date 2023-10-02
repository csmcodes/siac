

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class CcomdocBLL
    {
        #region Constructor

        public CcomdocBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Ccomdoc obj)
        {
            return CcomdocDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Ccomdoc obj)
        {
            return CcomdocDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Ccomdoc obj)
        {
            return CcomdocDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Ccomdoc obj)
        {
            return CcomdocDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Ccomdoc obj)
        {
            return CcomdocDAL.Update(obj);
        }
        public static int Update(BLL bll, Ccomdoc obj)
        {
            return CcomdocDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Ccomdoc obj)
        {
            return CcomdocDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Ccomdoc obj)
        {
            return CcomdocDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Ccomdoc GetByPK(Ccomdoc obj)
        {
            return CcomdocDAL.GetByPK(obj);
        }
        public static List<Ccomdoc> GetAll(string WhereClause, string OrderBy)
        {
            return CcomdocDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Ccomdoc> GetAll(WhereParams parametros, string OrderBy)
        {
            return CcomdocDAL.GetAll(parametros, OrderBy);
        }

         public static List<Ccomdoc> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return CcomdocDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Ccomdoc> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return CcomdocDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Ccomdoc> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return CcomdocDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return CcomdocDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return CcomdocDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return CcomdocDAL.GetMax(campo);
        }
        #endregion
    }
}
