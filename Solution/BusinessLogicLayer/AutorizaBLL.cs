

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class AutorizaBLL
    {
        #region Constructor

        public AutorizaBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Autoriza obj)
        {
            return AutorizaDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Autoriza obj)
        {
            return AutorizaDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Autoriza obj)
        {
            return AutorizaDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Autoriza obj)
        {
            return AutorizaDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Autoriza obj)
        {
            return AutorizaDAL.Update(obj);
        }
        public static int Update(BLL bll, Autoriza obj)
        {
            return AutorizaDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Autoriza obj)
        {
            return AutorizaDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Autoriza obj)
        {
            return AutorizaDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Autoriza GetByPK(Autoriza obj)
        {
            return AutorizaDAL.GetByPK(obj);
        }
        public static List<Autoriza> GetAll(string WhereClause, string OrderBy)
        {
            return AutorizaDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Autoriza> GetAll(WhereParams parametros, string OrderBy)
        {
            return AutorizaDAL.GetAll(parametros, OrderBy);
        }

         public static List<Autoriza> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return AutorizaDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Autoriza> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return AutorizaDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Autoriza> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return AutorizaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return AutorizaDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return AutorizaDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return AutorizaDAL.GetMax(campo);
        }
        public static int GetMax(string campo,string whereclause)
        {
            return AutorizaDAL.GetMax(campo, whereclause);
        }
        public static int GetMax(string campo, WhereParams parametros)
        {
            return AutorizaDAL.GetMax(campo, parametros);
        }



        #endregion
    }
}
