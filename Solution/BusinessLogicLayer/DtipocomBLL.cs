

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DtipocomBLL
    {
        #region Constructor

        public DtipocomBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Dtipocom obj)
        {
            return DtipocomDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Dtipocom obj)
        {
            return DtipocomDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Dtipocom obj)
        {
            return DtipocomDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Dtipocom obj)
        {
            return DtipocomDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Dtipocom obj)
        {
            return DtipocomDAL.Update(obj);
        }
        public static int Update(BLL bll, Dtipocom obj)
        {
            return DtipocomDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Dtipocom obj)
        {
            return DtipocomDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Dtipocom obj)
        {
            return DtipocomDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Dtipocom GetByPK(Dtipocom obj)
        {
            return DtipocomDAL.GetByPK(obj);
        }
        public static List<Dtipocom> GetAll(string WhereClause, string OrderBy)
        {
            return DtipocomDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Dtipocom> GetAll(WhereParams parametros, string OrderBy)
        {
            return DtipocomDAL.GetAll(parametros, OrderBy);
        }

         public static List<Dtipocom> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DtipocomDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Dtipocom> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DtipocomDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Dtipocom> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DtipocomDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DtipocomDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DtipocomDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DtipocomDAL.GetMax(campo);
        }
        #endregion
    }
}
