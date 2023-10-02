

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class ConceptoBLL
    {
        #region Constructor

        public ConceptoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Concepto obj)
        {
            return ConceptoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Concepto obj)
        {
            return ConceptoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Concepto obj)
        {
            return ConceptoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Concepto obj)
        {
            return ConceptoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Concepto obj)
        {
            return ConceptoDAL.Update(obj);
        }
        public static int Update(BLL bll, Concepto obj)
        {
            return ConceptoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Concepto obj)
        {
            return ConceptoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Concepto obj)
        {
            return ConceptoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Concepto GetByPK(Concepto obj)
        {
            return ConceptoDAL.GetByPK(obj);
        }
        public static List<Concepto> GetAll(string WhereClause, string OrderBy)
        {
            return ConceptoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Concepto> GetAll(WhereParams parametros, string OrderBy)
        {
            return ConceptoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Concepto> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return ConceptoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Concepto> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return ConceptoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Concepto> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return ConceptoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return ConceptoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return ConceptoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return ConceptoDAL.GetMax(campo);
        }
        #endregion
    }
}
