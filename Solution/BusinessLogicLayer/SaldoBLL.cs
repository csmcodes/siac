

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class SaldoBLL
    {
        #region Constructor

        public SaldoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Saldo obj)
        {
            return SaldoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Saldo obj)
        {
            return SaldoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Saldo obj)
        {
            return SaldoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Saldo obj)
        {
            return SaldoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Saldo obj)
        {
            return SaldoDAL.Update(obj);
        }
        public static int Update(BLL bll, Saldo obj)
        {
            return SaldoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Saldo obj)
        {
            return SaldoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Saldo obj)
        {
            return SaldoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Saldo GetByPK(Saldo obj)
        {
            return SaldoDAL.GetByPK(obj);
        }
        public static List<Saldo> GetAll(string WhereClause, string OrderBy)
        {
            return SaldoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Saldo> GetAll(WhereParams parametros, string OrderBy)
        {
            return SaldoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Saldo> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return SaldoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Saldo> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return SaldoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Saldo> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return SaldoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return SaldoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return SaldoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return SaldoDAL.GetMax(campo);
        }
        #endregion


        #region Get Sum

        public static decimal GetSum(string campo, string WhereClause, string OrderBy)
        {
            return SaldoDAL.GetSum(campo, WhereClause, OrderBy);
        }

        public static decimal GetSum(string campo, WhereParams parametros, string OrderBy)
        {
            return SaldoDAL.GetSum(campo, parametros, OrderBy);
        }
        #endregion
    }
}
