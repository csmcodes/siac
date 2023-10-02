

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class PersonaBLL
    {
        #region Constructor

        public PersonaBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Persona obj)
        {
            return PersonaDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Persona obj)
        {
            return PersonaDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Persona obj)
        {
            return PersonaDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Persona obj)
        {
            return PersonaDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Persona obj)
        {
            return PersonaDAL.Update(obj);
        }
        public static int Update(BLL bll, Persona obj)
        {
            return PersonaDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Persona obj)
        {
            return PersonaDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Persona obj)
        {
            return PersonaDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Persona GetByPK(Persona obj)
        {
            return PersonaDAL.GetByPK(obj);
        }
        public static List<Persona> GetAll(string WhereClause, string OrderBy)
        {
            return PersonaDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Persona> GetAll(WhereParams parametros, string OrderBy)
        {
            return PersonaDAL.GetAll(parametros, OrderBy);
        }

         public static List<Persona> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return PersonaDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Persona> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return PersonaDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Persona> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return PersonaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return PersonaDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return PersonaDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return PersonaDAL.GetMax(campo);
        }
        #endregion
    }
}
