

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class CtipocomBLL
    {
        #region Constructor

        public CtipocomBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Ctipocom obj)
        {
            return CtipocomDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Ctipocom obj)
        {
            return CtipocomDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Ctipocom obj)
        {
            return CtipocomDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Ctipocom obj)
        {
            return CtipocomDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Ctipocom obj)
        {
            return CtipocomDAL.Update(obj);
        }
        public static int Update(BLL bll, Ctipocom obj)
        {
            return CtipocomDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Ctipocom obj)
        {
            return CtipocomDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Ctipocom obj)
        {
            return CtipocomDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Ctipocom GetByPK(Ctipocom obj)
        {
            return CtipocomDAL.GetByPK(obj);
        }
        public static Ctipocom GetById(Ctipocom obj)
        {
            return CtipocomDAL.GetById(obj);
        }
        public static List<Ctipocom> GetAll(string WhereClause, string OrderBy)
        {
            return CtipocomDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Ctipocom> GetAll(WhereParams parametros, string OrderBy)
        {
            return CtipocomDAL.GetAll(parametros, OrderBy);
        }

         public static List<Ctipocom> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return CtipocomDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Ctipocom> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return CtipocomDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Ctipocom> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return CtipocomDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return CtipocomDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return CtipocomDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return CtipocomDAL.GetMax(campo);
        }
        #endregion
    }
}
