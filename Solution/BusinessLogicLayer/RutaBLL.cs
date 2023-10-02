

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class RutaBLL
    {
        #region Constructor

        public RutaBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Ruta obj)
        {
            return RutaDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Ruta obj)
        {
            return RutaDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Ruta obj)
        {
            return RutaDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Ruta obj)
        {
            return RutaDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Ruta obj)
        {
            return RutaDAL.Update(obj);
        }
        public static int Update(BLL bll, Ruta obj)
        {
            return RutaDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Ruta obj)
        {
            return RutaDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Ruta obj)
        {
            return RutaDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Ruta GetByPK(Ruta obj)
        {
            return RutaDAL.GetByPK(obj);
        }
        public static List<Ruta> GetAll(string WhereClause, string OrderBy)
        {
            return RutaDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Ruta> GetAll(WhereParams parametros, string OrderBy)
        {
            return RutaDAL.GetAll(parametros, OrderBy);
        }

         public static List<Ruta> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return RutaDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Ruta> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return RutaDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Ruta> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return RutaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return RutaDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return RutaDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return RutaDAL.GetMax(campo);
        }
        #endregion
    }
}
