

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class CuetransaccBLL
    {
        #region Constructor

        public CuetransaccBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Cuetransacc obj)
        {
            return CuetransaccDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Cuetransacc obj)
        {
            return CuetransaccDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Cuetransacc obj)
        {
            return CuetransaccDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Cuetransacc obj)
        {
            return CuetransaccDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Cuetransacc obj)
        {
            return CuetransaccDAL.Update(obj);
        }
        public static int Update(BLL bll, Cuetransacc obj)
        {
            return CuetransaccDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Cuetransacc obj)
        {
            return CuetransaccDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Cuetransacc obj)
        {
            return CuetransaccDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Cuetransacc GetByPK(Cuetransacc obj)
        {
            return CuetransaccDAL.GetByPK(obj);
        }
        public static List<Cuetransacc> GetAll(string WhereClause, string OrderBy)
        {
            return CuetransaccDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Cuetransacc> GetAll(WhereParams parametros, string OrderBy)
        {
            return CuetransaccDAL.GetAll(parametros, OrderBy);
        }

         public static List<Cuetransacc> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return CuetransaccDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Cuetransacc> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return CuetransaccDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Cuetransacc> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return CuetransaccDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return CuetransaccDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return CuetransaccDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return CuetransaccDAL.GetMax(campo);
        }
        #endregion
    }
}
