

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class TipopagoBLL
    {
        #region Constructor

        public TipopagoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Tipopago obj)
        {
            return TipopagoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Tipopago obj)
        {
            return TipopagoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Tipopago obj)
        {
            return TipopagoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Tipopago obj)
        {
            return TipopagoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Tipopago obj)
        {
            return TipopagoDAL.Update(obj);
        }
        public static int Update(BLL bll, Tipopago obj)
        {
            return TipopagoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Tipopago obj)
        {
            return TipopagoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Tipopago obj)
        {
            return TipopagoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Tipopago GetByPK(Tipopago obj)
        {
            return TipopagoDAL.GetByPK(obj);
        }
        public static List<Tipopago> GetAll(string WhereClause, string OrderBy)
        {
            return TipopagoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Tipopago> GetAll(WhereParams parametros, string OrderBy)
        {
            return TipopagoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Tipopago> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return TipopagoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Tipopago> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return TipopagoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Tipopago> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return TipopagoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return TipopagoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return TipopagoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return TipopagoDAL.GetMax(campo);
        }
        #endregion
    }
}
