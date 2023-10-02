

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class CatclienteBLL
    {
        #region Constructor

        public CatclienteBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Catcliente obj)
        {
            return CatclienteDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Catcliente obj)
        {
            return CatclienteDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Catcliente obj)
        {
            return CatclienteDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Catcliente obj)
        {
            return CatclienteDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Catcliente obj)
        {
            return CatclienteDAL.Update(obj);
        }
        public static int Update(BLL bll, Catcliente obj)
        {
            return CatclienteDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Catcliente obj)
        {
            return CatclienteDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Catcliente obj)
        {
            return CatclienteDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Catcliente GetByPK(Catcliente obj)
        {
            return CatclienteDAL.GetByPK(obj);
        }
        public static Catcliente GetById(Catcliente obj)
        {
            return CatclienteDAL.GetById(obj);
        }
        public static List<Catcliente> GetAll(string WhereClause, string OrderBy)
        {
            return CatclienteDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Catcliente> GetAll(WhereParams parametros, string OrderBy)
        {
            return CatclienteDAL.GetAll(parametros, OrderBy);
        }

         public static List<Catcliente> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return CatclienteDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Catcliente> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return CatclienteDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Catcliente> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return CatclienteDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return CatclienteDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return CatclienteDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return CatclienteDAL.GetMax(campo);
        }
        #endregion
    }
}
