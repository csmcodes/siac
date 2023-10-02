

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class GproductoBLL
    {
        #region Constructor

        public GproductoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Gproducto obj)
        {
            return GproductoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Gproducto obj)
        {
            return GproductoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Gproducto obj)
        {
            return GproductoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Gproducto obj)
        {
            return GproductoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Gproducto obj)
        {
            return GproductoDAL.Update(obj);
        }
        public static int Update(BLL bll, Gproducto obj)
        {
            return GproductoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Gproducto obj)
        {
            return GproductoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Gproducto obj)
        {
            return GproductoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Gproducto GetByPK(Gproducto obj)
        {
            return GproductoDAL.GetByPK(obj);
        }
        public static List<Gproducto> GetAll(string WhereClause, string OrderBy)
        {
            return GproductoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Gproducto> GetAll(WhereParams parametros, string OrderBy)
        {
            return GproductoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Gproducto> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return GproductoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Gproducto> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return GproductoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Gproducto> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return GproductoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return GproductoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return GproductoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return GproductoDAL.GetMax(campo);
        }
        #endregion
    }
}
