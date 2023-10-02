

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DcalculoprecioBLL
    {
        #region Constructor

        public DcalculoprecioBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Dcalculoprecio obj)
        {
            return DcalculoprecioDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Dcalculoprecio obj)
        {
            return DcalculoprecioDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Dcalculoprecio obj)
        {
            return DcalculoprecioDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Dcalculoprecio obj)
        {
            return DcalculoprecioDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Dcalculoprecio obj)
        {
            return DcalculoprecioDAL.Update(obj);
        }
        public static int Update(BLL bll, Dcalculoprecio obj)
        {
            return DcalculoprecioDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Dcalculoprecio obj)
        {
            return DcalculoprecioDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Dcalculoprecio obj)
        {
            return DcalculoprecioDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Dcalculoprecio GetByPK(Dcalculoprecio obj)
        {
            return DcalculoprecioDAL.GetByPK(obj);
        }
        public static List<Dcalculoprecio> GetAll(string WhereClause, string OrderBy)
        {
            return DcalculoprecioDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Dcalculoprecio> GetAll(WhereParams parametros, string OrderBy)
        {
            return DcalculoprecioDAL.GetAll(parametros, OrderBy);
        }

         public static List<Dcalculoprecio> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DcalculoprecioDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Dcalculoprecio> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DcalculoprecioDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Dcalculoprecio> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DcalculoprecioDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DcalculoprecioDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DcalculoprecioDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DcalculoprecioDAL.GetMax(campo);
        }
        #endregion
    }
}
