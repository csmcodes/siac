

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class PoliticaBLL
    {
        #region Constructor

        public PoliticaBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Politica obj)
        {
            return PoliticaDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Politica obj)
        {
            return PoliticaDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Politica obj)
        {
            return PoliticaDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Politica obj)
        {
            return PoliticaDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Politica obj)
        {
            return PoliticaDAL.Update(obj);
        }
        public static int Update(BLL bll, Politica obj)
        {
            return PoliticaDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Politica obj)
        {
            return PoliticaDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Politica obj)
        {
            return PoliticaDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Politica GetByPK(Politica obj)
        {
            return PoliticaDAL.GetByPK(obj);
        }
        public static Politica GetById(Politica obj)
        {
            return PoliticaDAL.GetById(obj);
        }
        public static Catcliente GetById(Catcliente obj)
        {
            return CatclienteDAL.GetById(obj);
        }
        public static List<Politica> GetAll(string WhereClause, string OrderBy)
        {
            return PoliticaDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Politica> GetAll(WhereParams parametros, string OrderBy)
        {
            return PoliticaDAL.GetAll(parametros, OrderBy);
        }

         public static List<Politica> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return PoliticaDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Politica> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return PoliticaDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Politica> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return PoliticaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return PoliticaDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return PoliticaDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return PoliticaDAL.GetMax(campo);
        }
        #endregion
    }
}
