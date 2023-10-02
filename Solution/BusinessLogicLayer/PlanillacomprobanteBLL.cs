

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class PlanillacomprobanteBLL
    {
        #region Constructor

        public PlanillacomprobanteBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Planillacomprobante obj)
        {
            return PlanillacomprobanteDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Planillacomprobante obj)
        {
            return PlanillacomprobanteDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Planillacomprobante obj)
        {
            return PlanillacomprobanteDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Planillacomprobante obj)
        {
            return PlanillacomprobanteDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Planillacomprobante obj)
        {
            return PlanillacomprobanteDAL.Update(obj);
        }
        public static int Update(BLL bll, Planillacomprobante obj)
        {
            return PlanillacomprobanteDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Planillacomprobante obj)
        {
            return PlanillacomprobanteDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Planillacomprobante obj)
        {
            return PlanillacomprobanteDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Planillacomprobante GetByPK(Planillacomprobante obj)
        {
            return PlanillacomprobanteDAL.GetByPK(obj);
        }
        public static List<Planillacomprobante> GetAll(string WhereClause, string OrderBy)
        {
            return PlanillacomprobanteDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Planillacomprobante> GetAll(WhereParams parametros, string OrderBy)
        {
            return PlanillacomprobanteDAL.GetAll(parametros, OrderBy);
        }

         public static List<Planillacomprobante> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return PlanillacomprobanteDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Planillacomprobante> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return PlanillacomprobanteDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Planillacomprobante> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return PlanillacomprobanteDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return PlanillacomprobanteDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return PlanillacomprobanteDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return PlanillacomprobanteDAL.GetMax(campo);
        }
        #endregion
    }
}
