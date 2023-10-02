

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class TokenBLL
    {
        #region Constructor

        public TokenBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Token obj)
        {
            return TokenDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Token obj)
        {
            return TokenDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Token obj)
        {
            return TokenDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Token obj)
        {
            return TokenDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Token obj)
        {
            return TokenDAL.Update(obj);
        }
        public static int Update(BLL bll, Token obj)
        {
            return TokenDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Token obj)
        {
            return TokenDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Token obj)
        {
            return TokenDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Token GetByPK(Token obj)
        {
            return TokenDAL.GetByPK(obj);
        }
        public static List<Token> GetAll(string WhereClause, string OrderBy)
        {
            return TokenDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Token> GetAll(WhereParams parametros, string OrderBy)
        {
            return TokenDAL.GetAll(parametros, OrderBy);
        }

         public static List<Token> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return TokenDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Token> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return TokenDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Token> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return TokenDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return TokenDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return TokenDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return TokenDAL.GetMax(campo);
        }
        #endregion
    }
}
