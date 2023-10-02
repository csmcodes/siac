

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class RubroBLL
    {
        #region Constructor

        public RubroBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Rubro obj)
        {
            return RubroDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Rubro obj)
        {
            return RubroDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Rubro obj)
        {
            return RubroDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Rubro obj)
        {
            return RubroDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Rubro obj)
        {
            return RubroDAL.Update(obj);
        }
        public static int Update(BLL bll, Rubro obj)
        {
            return RubroDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Rubro obj)
        {
            return RubroDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Rubro obj)
        {
            return RubroDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Rubro GetByPK(Rubro obj)
        {
            return RubroDAL.GetByPK(obj);
        }
        public static List<Rubro> GetAll(string WhereClause, string OrderBy)
        {
            return RubroDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Rubro> GetAll(WhereParams parametros, string OrderBy)
        {
            return RubroDAL.GetAll(parametros, OrderBy);
        }

         public static List<Rubro> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return RubroDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Rubro> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return RubroDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Rubro> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return RubroDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return RubroDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return RubroDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return RubroDAL.GetMax(campo);
        }
        #endregion
    }
}
