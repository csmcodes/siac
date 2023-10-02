

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class ImpresionBLL
    {
        #region Constructor

        public ImpresionBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Impresion obj)
        {
            return ImpresionDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Impresion obj)
        {
            return ImpresionDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Impresion obj)
        {
            return ImpresionDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Impresion obj)
        {
            return ImpresionDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Impresion obj)
        {
            return ImpresionDAL.Update(obj);
        }
        public static int Update(BLL bll, Impresion obj)
        {
            return ImpresionDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Impresion obj)
        {
            return ImpresionDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Impresion obj)
        {
            return ImpresionDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Impresion GetByPK(Impresion obj)
        {
            return ImpresionDAL.GetByPK(obj);
        }
        public static List<Impresion> GetAll(string WhereClause, string OrderBy)
        {
            return ImpresionDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Impresion> GetAll(WhereParams parametros, string OrderBy)
        {
            return ImpresionDAL.GetAll(parametros, OrderBy);
        }

         public static List<Impresion> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return ImpresionDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Impresion> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return ImpresionDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Impresion> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return ImpresionDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return ImpresionDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return ImpresionDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return ImpresionDAL.GetMax(campo);
        }
        #endregion
    }
}
