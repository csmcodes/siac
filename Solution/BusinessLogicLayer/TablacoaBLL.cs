

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class TablacoaBLL
    {
        #region Constructor

        public TablacoaBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Tablacoa obj)
        {
            return TablacoaDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Tablacoa obj)
        {
            return TablacoaDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Tablacoa obj)
        {
            return TablacoaDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Tablacoa obj)
        {
            return TablacoaDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Tablacoa obj)
        {
            return TablacoaDAL.Update(obj);
        }
        public static int Update(BLL bll, Tablacoa obj)
        {
            return TablacoaDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Tablacoa obj)
        {
            return TablacoaDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Tablacoa obj)
        {
            return TablacoaDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Tablacoa GetByPK(Tablacoa obj)
        {
            return TablacoaDAL.GetByPK(obj);
        }
        public static List<Tablacoa> GetAll(string WhereClause, string OrderBy)
        {
            return TablacoaDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Tablacoa> GetAll(WhereParams parametros, string OrderBy)
        {
            return TablacoaDAL.GetAll(parametros, OrderBy);
        }

         public static List<Tablacoa> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return TablacoaDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Tablacoa> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return TablacoaDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Tablacoa> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return TablacoaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return TablacoaDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return TablacoaDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return TablacoaDAL.GetMax(campo);
        }
        #endregion
    }
}
