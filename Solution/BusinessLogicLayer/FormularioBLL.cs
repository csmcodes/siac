

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class FormularioBLL
    {
        #region Constructor

        public FormularioBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Formulario obj)
        {
            return FormularioDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Formulario obj)
        {
            return FormularioDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Formulario obj)
        {
            return FormularioDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Formulario obj)
        {
            return FormularioDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Formulario obj)
        {
            return FormularioDAL.Update(obj);
        }
        public static int Update(BLL bll, Formulario obj)
        {
            return FormularioDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Formulario obj)
        {
            return FormularioDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Formulario obj)
        {
            return FormularioDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Formulario GetByPK(Formulario obj)
        {
            return FormularioDAL.GetByPK(obj);
        }
        public static List<Formulario> GetAll(string WhereClause, string OrderBy)
        {
            return FormularioDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Formulario> GetAll(WhereParams parametros, string OrderBy)
        {
            return FormularioDAL.GetAll(parametros, OrderBy);
        }

         public static List<Formulario> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return FormularioDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Formulario> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return FormularioDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Formulario> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return FormularioDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return FormularioDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return FormularioDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return FormularioDAL.GetMax(campo);
        }
        #endregion
    }
}
