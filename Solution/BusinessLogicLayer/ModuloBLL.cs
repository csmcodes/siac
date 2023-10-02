

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class ModuloBLL
    {
        #region Constructor

        public ModuloBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Modulo obj)
        {
            return ModuloDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Modulo obj)
        {
            return ModuloDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Modulo obj)
        {
            return ModuloDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Modulo obj)
        {
            return ModuloDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Modulo obj)
        {
            return ModuloDAL.Update(obj);
        }
        public static int Update(BLL bll, Modulo obj)
        {
            return ModuloDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Modulo obj)
        {
            return ModuloDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Modulo obj)
        {
            return ModuloDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Modulo GetByPK(Modulo obj)
        {
            return ModuloDAL.GetByPK(obj);
        }
        public static Modulo GetById(Modulo obj)
        {
            return ModuloDAL.GetById(obj);
        }
        public static List<Modulo> GetAll(string WhereClause, string OrderBy)
        {
            return ModuloDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Modulo> GetAll(WhereParams parametros, string OrderBy)
        {
            return ModuloDAL.GetAll(parametros, OrderBy);
        }

         public static List<Modulo> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return ModuloDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Modulo> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return ModuloDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Modulo> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return ModuloDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return ModuloDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return ModuloDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return ModuloDAL.GetMax(campo);
        }
        #endregion
    }
}
