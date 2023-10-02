

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class ChoferBLL
    {
        #region Constructor

        public ChoferBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Chofer obj)
        {
            return ChoferDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Chofer obj)
        {
            return ChoferDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Chofer obj)
        {
            return ChoferDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Chofer obj)
        {
            return ChoferDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Chofer obj)
        {
            return ChoferDAL.Update(obj);
        }
        public static int Update(BLL bll, Chofer obj)
        {
            return ChoferDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Chofer obj)
        {
            return ChoferDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Chofer obj)
        {
            return ChoferDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Chofer GetByPK(Chofer obj)
        {
            return ChoferDAL.GetByPK(obj);
        }
        public static List<Chofer> GetAll(string WhereClause, string OrderBy)
        {
            return ChoferDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Chofer> GetAll(WhereParams parametros, string OrderBy)
        {
            return ChoferDAL.GetAll(parametros, OrderBy);
        }

         public static List<Chofer> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return ChoferDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Chofer> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return ChoferDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Chofer> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return ChoferDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return ChoferDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return ChoferDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return ChoferDAL.GetMax(campo);
        }
        #endregion
    }
}
