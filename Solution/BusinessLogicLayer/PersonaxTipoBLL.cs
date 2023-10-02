

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class PersonaxtipoBLL
    {
        #region Constructor

        public PersonaxtipoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Personaxtipo obj)
        {
            return PersonaxtipoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Personaxtipo obj)
        {
            return PersonaxtipoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Personaxtipo obj)
        {
            return PersonaxtipoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Personaxtipo obj)
        {
            return PersonaxtipoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Personaxtipo obj)
        {
            return PersonaxtipoDAL.Update(obj);
        }
        public static int Update(BLL bll, Personaxtipo obj)
        {
            return PersonaxtipoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Personaxtipo obj)
        {
            return PersonaxtipoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Personaxtipo obj)
        {
            return PersonaxtipoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Personaxtipo GetByPK(Personaxtipo obj)
        {
            return PersonaxtipoDAL.GetByPK(obj);
        }
        public static List<Personaxtipo> GetAll(string WhereClause, string OrderBy)
        {
            return PersonaxtipoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Personaxtipo> GetAll(WhereParams parametros, string OrderBy)
        {
            return PersonaxtipoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Personaxtipo> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return PersonaxtipoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Personaxtipo> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return PersonaxtipoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Personaxtipo> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return PersonaxtipoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return PersonaxtipoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return PersonaxtipoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return PersonaxtipoDAL.GetMax(campo);
        }
        #endregion
    }
}
