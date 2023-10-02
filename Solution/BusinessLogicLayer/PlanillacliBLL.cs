

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class PlanillacliBLL
    {
        #region Constructor

        public PlanillacliBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Planillacli obj)
        {
            return PlanillacliDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Planillacli obj)
        {
            return PlanillacliDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Planillacli obj)
        {
            return PlanillacliDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Planillacli obj)
        {
            return PlanillacliDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Planillacli obj)
        {
            return PlanillacliDAL.Update(obj);
        }
        public static int Update(BLL bll, Planillacli obj)
        {
            return PlanillacliDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Planillacli obj)
        {
            return PlanillacliDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Planillacli obj)
        {
            return PlanillacliDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Planillacli GetByPK(Planillacli obj)
        {
            return PlanillacliDAL.GetByPK(obj);
        }
        public static List<Planillacli> GetAll(string WhereClause, string OrderBy)
        {
            return PlanillacliDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Planillacli> GetAll(WhereParams parametros, string OrderBy)
        {
            return PlanillacliDAL.GetAll(parametros, OrderBy);
        }

         public static List<Planillacli> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return PlanillacliDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Planillacli> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return PlanillacliDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Planillacli> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return PlanillacliDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return PlanillacliDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return PlanillacliDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return PlanillacliDAL.GetMax(campo);
        }
        #endregion
    }
}
