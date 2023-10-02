

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class RetdatoBLL
    {
        #region Constructor

        public RetdatoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Retdato obj)
        {
            return RetdatoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Retdato obj)
        {
            return RetdatoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Retdato obj)
        {
            return RetdatoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Retdato obj)
        {
            return RetdatoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Retdato obj)
        {
            return RetdatoDAL.Update(obj);
        }
        public static int Update(BLL bll, Retdato obj)
        {
            return RetdatoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Retdato obj)
        {
            return RetdatoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Retdato obj)
        {
            return RetdatoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Retdato GetByPK(Retdato obj)
        {
            return RetdatoDAL.GetByPK(obj);
        }
        public static List<Retdato> GetAll(string WhereClause, string OrderBy)
        {
            return RetdatoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Retdato> GetAll(WhereParams parametros, string OrderBy)
        {
            return RetdatoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Retdato> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return RetdatoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Retdato> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return RetdatoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Retdato> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return RetdatoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return RetdatoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return RetdatoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return RetdatoDAL.GetMax(campo);
        }
        #endregion
    }
}
