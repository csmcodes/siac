

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class ImpuestoBLL
    {
        #region Constructor

        public ImpuestoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Impuesto obj)
        {
            return ImpuestoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Impuesto obj)
        {
            return ImpuestoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Impuesto obj)
        {
            return ImpuestoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Impuesto obj)
        {
            return ImpuestoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Impuesto obj)
        {
            return ImpuestoDAL.Update(obj);
        }
        public static int Update(BLL bll, Impuesto obj)
        {
            return ImpuestoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Impuesto obj)
        {
            return ImpuestoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Impuesto obj)
        {
            return ImpuestoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Impuesto GetByPK(Impuesto obj)
        {
            return ImpuestoDAL.GetByPK(obj);
        }
        public static Impuesto GetById(Impuesto obj)
        {
            return ImpuestoDAL.GetById(obj);
        }
        public static List<Impuesto> GetAll(string WhereClause, string OrderBy)
        {
            return ImpuestoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Impuesto> GetAll(WhereParams parametros, string OrderBy)
        {
            return ImpuestoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Impuesto> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return ImpuestoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Impuesto> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return ImpuestoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Impuesto> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return ImpuestoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return ImpuestoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return ImpuestoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return ImpuestoDAL.GetMax(campo);
        }
        #endregion
    }
}
