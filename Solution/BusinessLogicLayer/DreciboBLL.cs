

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DreciboBLL
    {
        #region Constructor

        public DreciboBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Drecibo obj)
        {
            return DreciboDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Drecibo obj)
        {
            return DreciboDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Drecibo obj)
        {
            return DreciboDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Drecibo obj)
        {
            return DreciboDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Drecibo obj)
        {
            return DreciboDAL.Update(obj);
        }
        public static int Update(BLL bll, Drecibo obj)
        {
            return DreciboDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Drecibo obj)
        {
            return DreciboDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Drecibo obj)
        {
            return DreciboDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Drecibo GetByPK(Drecibo obj)
        {
            return DreciboDAL.GetByPK(obj);
        }
        public static List<Drecibo> GetAll(string WhereClause, string OrderBy)
        {
            return DreciboDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Drecibo> GetAll(WhereParams parametros, string OrderBy)
        {
            return DreciboDAL.GetAll(parametros, OrderBy);
        }

         public static List<Drecibo> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DreciboDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Drecibo> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DreciboDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Drecibo> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DreciboDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DreciboDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DreciboDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DreciboDAL.GetMax(campo);
        }
        #endregion
    }
}
