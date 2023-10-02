

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DlistadscBLL
    {
        #region Constructor

        public DlistadscBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Dlistadsc obj)
        {
            return DlistadscDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Dlistadsc obj)
        {
            return DlistadscDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Dlistadsc obj)
        {
            return DlistadscDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Dlistadsc obj)
        {
            return DlistadscDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Dlistadsc obj)
        {
            return DlistadscDAL.Update(obj);
        }
        public static int Update(BLL bll, Dlistadsc obj)
        {
            return DlistadscDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Dlistadsc obj)
        {
            return DlistadscDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Dlistadsc obj)
        {
            return DlistadscDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Dlistadsc GetByPK(Dlistadsc obj)
        {
            return DlistadscDAL.GetByPK(obj);
        }
        public static List<Dlistadsc> GetAll(string WhereClause, string OrderBy)
        {
            return DlistadscDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Dlistadsc> GetAll(WhereParams parametros, string OrderBy)
        {
            return DlistadscDAL.GetAll(parametros, OrderBy);
        }

         public static List<Dlistadsc> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DlistadscDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Dlistadsc> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DlistadscDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Dlistadsc> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DlistadscDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DlistadscDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DlistadscDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DlistadscDAL.GetMax(campo);
        }
        #endregion
    }
}
