

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class RubrosplanillaBLL
    {
        #region Constructor

        public RubrosplanillaBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Rubrosplanilla obj)
        {
            return RubrosplanillaDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Rubrosplanilla obj)
        {
            return RubrosplanillaDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Rubrosplanilla obj)
        {
            return RubrosplanillaDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Rubrosplanilla obj)
        {
            return RubrosplanillaDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Rubrosplanilla obj)
        {
            return RubrosplanillaDAL.Update(obj);
        }
        public static int Update(BLL bll, Rubrosplanilla obj)
        {
            return RubrosplanillaDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Rubrosplanilla obj)
        {
            return RubrosplanillaDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Rubrosplanilla obj)
        {
            return RubrosplanillaDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Rubrosplanilla GetByPK(Rubrosplanilla obj)
        {
            return RubrosplanillaDAL.GetByPK(obj);
        }
        public static List<Rubrosplanilla> GetAll(string WhereClause, string OrderBy)
        {
            return RubrosplanillaDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Rubrosplanilla> GetAll(WhereParams parametros, string OrderBy)
        {
            return RubrosplanillaDAL.GetAll(parametros, OrderBy);
        }

         public static List<Rubrosplanilla> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return RubrosplanillaDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Rubrosplanilla> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return RubrosplanillaDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Rubrosplanilla> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return RubrosplanillaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return RubrosplanillaDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return RubrosplanillaDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return RubrosplanillaDAL.GetMax(campo);
        }
        #endregion
    }
}
