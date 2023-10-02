

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class PuntoventaBLL
    {
        #region Constructor

        public PuntoventaBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Puntoventa obj)
        {
            return PuntoventaDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Puntoventa obj)
        {
            return PuntoventaDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Puntoventa obj)
        {
            return PuntoventaDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Puntoventa obj)
        {
            return PuntoventaDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Puntoventa obj)
        {
            return PuntoventaDAL.Update(obj);
        }
        public static int Update(BLL bll, Puntoventa obj)
        {
            return PuntoventaDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Puntoventa obj)
        {
            return PuntoventaDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Puntoventa obj)
        {
            return PuntoventaDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Puntoventa GetByPK(Puntoventa obj)
        {
            return PuntoventaDAL.GetByPK(obj);
        }
        public static List<Puntoventa> GetAll(string WhereClause, string OrderBy)
        {
            return PuntoventaDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Puntoventa> GetAll(WhereParams parametros, string OrderBy)
        {
            return PuntoventaDAL.GetAll(parametros, OrderBy);
        }

         public static List<Puntoventa> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return PuntoventaDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Puntoventa> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return PuntoventaDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Puntoventa> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return PuntoventaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return PuntoventaDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return PuntoventaDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return PuntoventaDAL.GetMax(campo);
        }
        #endregion
    }
}
