

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DdocumentoBLL
    {
        #region Constructor

        public DdocumentoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Ddocumento obj)
        {
            return DdocumentoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Ddocumento obj)
        {
            return DdocumentoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Ddocumento obj)
        {
            return DdocumentoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Ddocumento obj)
        {
            return DdocumentoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Ddocumento obj)
        {
            return DdocumentoDAL.Update(obj);
        }
        public static int Update(BLL bll, Ddocumento obj)
        {
            return DdocumentoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Ddocumento obj)
        {
            return DdocumentoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Ddocumento obj)
        {
            return DdocumentoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Ddocumento GetByPK(Ddocumento obj)
        {
            return DdocumentoDAL.GetByPK(obj);
        }
        public static List<Ddocumento> GetAll(string WhereClause, string OrderBy)
        {
            return DdocumentoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Ddocumento> GetAll(WhereParams parametros, string OrderBy)
        {
            return DdocumentoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Ddocumento> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DdocumentoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Ddocumento> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DdocumentoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Ddocumento> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DdocumentoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DdocumentoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DdocumentoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DdocumentoDAL.GetMax(campo);
        }
        #endregion
    }
}
