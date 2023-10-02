

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class TproductoBLL
    {
        #region Constructor

        public TproductoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Tproducto obj)
        {
            return TproductoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Tproducto obj)
        {
            return TproductoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Tproducto obj)
        {
            return TproductoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Tproducto obj)
        {
            return TproductoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Tproducto obj)
        {
            return TproductoDAL.Update(obj);
        }
        public static int Update(BLL bll, Tproducto obj)
        {
            return TproductoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Tproducto obj)
        {
            return TproductoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Tproducto obj)
        {
            return TproductoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Tproducto GetByPK(Tproducto obj)
        {
            return TproductoDAL.GetByPK(obj);
        }
        public static List<Tproducto> GetAll(string WhereClause, string OrderBy)
        {
            return TproductoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Tproducto> GetAll(WhereParams parametros, string OrderBy)
        {
            return TproductoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Tproducto> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return TproductoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Tproducto> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return TproductoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Tproducto> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return TproductoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return TproductoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return TproductoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return TproductoDAL.GetMax(campo);
        }
        #endregion
    }
}
