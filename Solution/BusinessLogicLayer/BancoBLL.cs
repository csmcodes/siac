

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class BancoBLL
    {
        #region Constructor

        public BancoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Banco obj)
        {
            return BancoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Banco obj)
        {
            return BancoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Banco obj)
        {
            return BancoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Banco obj)
        {
            return BancoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Banco obj)
        {
            return BancoDAL.Update(obj);
        }
        public static int Update(BLL bll, Banco obj)
        {
            return BancoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Banco obj)
        {
            return BancoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Banco obj)
        {
            return BancoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Banco GetByPK(Banco obj)
        {
            return BancoDAL.GetByPK(obj);
        }
        public static List<Banco> GetAll(string WhereClause, string OrderBy)
        {
            return BancoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Banco> GetAll(WhereParams parametros, string OrderBy)
        {
            return BancoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Banco> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return BancoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Banco> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return BancoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Banco> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return BancoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return BancoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return BancoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return BancoDAL.GetMax(campo);
        }
        #endregion
    }
}
