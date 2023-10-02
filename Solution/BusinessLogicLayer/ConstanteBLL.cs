

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class ConstanteBLL
    {
        #region Constructor

        public ConstanteBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Constante obj)
        {
            return ConstanteDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Constante obj)
        {
            return ConstanteDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Constante obj)
        {
            return ConstanteDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Constante obj)
        {
            return ConstanteDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Constante obj)
        {
            return ConstanteDAL.Update(obj);
        }
        public static int Update(BLL bll, Constante obj)
        {
            return ConstanteDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Constante obj)
        {
            return ConstanteDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Constante obj)
        {
            return ConstanteDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Constante GetByPK(Constante obj)
        {
            return ConstanteDAL.GetByPK(obj);
        }
        public static List<Constante> GetAll(string WhereClause, string OrderBy)
        {
            return ConstanteDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Constante> GetAll(WhereParams parametros, string OrderBy)
        {
            return ConstanteDAL.GetAll(parametros, OrderBy);
        }

         public static List<Constante> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return ConstanteDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Constante> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return ConstanteDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Constante> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return ConstanteDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return ConstanteDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return ConstanteDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return ConstanteDAL.GetMax(campo);
        }
        #endregion
    }
}
