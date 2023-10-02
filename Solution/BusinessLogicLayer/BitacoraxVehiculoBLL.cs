

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class BitacoraxvehiculoBLL
    {
        #region Constructor

        public BitacoraxvehiculoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Bitacoraxvehiculo obj)
        {
            return BitacoraxvehiculoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Bitacoraxvehiculo obj)
        {
            return BitacoraxvehiculoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Bitacoraxvehiculo obj)
        {
            return BitacoraxvehiculoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Bitacoraxvehiculo obj)
        {
            return BitacoraxvehiculoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Bitacoraxvehiculo obj)
        {
            return BitacoraxvehiculoDAL.Update(obj);
        }
        public static int Update(BLL bll, Bitacoraxvehiculo obj)
        {
            return BitacoraxvehiculoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Bitacoraxvehiculo obj)
        {
            return BitacoraxvehiculoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Bitacoraxvehiculo obj)
        {
            return BitacoraxvehiculoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Bitacoraxvehiculo GetByPK(Bitacoraxvehiculo obj)
        {
            return BitacoraxvehiculoDAL.GetByPK(obj);
        }
        public static List<Bitacoraxvehiculo> GetAll(string WhereClause, string OrderBy)
        {
            return BitacoraxvehiculoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Bitacoraxvehiculo> GetAll(WhereParams parametros, string OrderBy)
        {
            return BitacoraxvehiculoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Bitacoraxvehiculo> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return BitacoraxvehiculoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Bitacoraxvehiculo> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return BitacoraxvehiculoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Bitacoraxvehiculo> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return BitacoraxvehiculoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return BitacoraxvehiculoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return BitacoraxvehiculoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return BitacoraxvehiculoDAL.GetMax(campo);
        }
        #endregion
    }
}
