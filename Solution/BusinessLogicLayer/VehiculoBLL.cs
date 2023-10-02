

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class VehiculoBLL
    {
        #region Constructor

        public VehiculoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Vehiculo obj)
        {
            return VehiculoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Vehiculo obj)
        {
            return VehiculoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Vehiculo obj)
        {
            return VehiculoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Vehiculo obj)
        {
            return VehiculoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Vehiculo obj)
        {
            return VehiculoDAL.Update(obj);
        }
        public static int Update(BLL bll, Vehiculo obj)
        {
            return VehiculoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Vehiculo obj)
        {
            return VehiculoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Vehiculo obj)
        {
            return VehiculoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Vehiculo GetByPK(Vehiculo obj)
        {
            return VehiculoDAL.GetByPK(obj);
        }
        public static List<Vehiculo> GetAll(string WhereClause, string OrderBy)
        {
            return VehiculoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Vehiculo> GetAll(WhereParams parametros, string OrderBy)
        {
            return VehiculoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Vehiculo> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return VehiculoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Vehiculo> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return VehiculoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Vehiculo> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return VehiculoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return VehiculoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return VehiculoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return VehiculoDAL.GetMax(campo);
        }
        #endregion
    }
}
