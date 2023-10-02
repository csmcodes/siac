

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class SocioempleadoBLL
    {
        #region Constructor

        public SocioempleadoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Socioempleado obj)
        {
            return SocioempleadoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Socioempleado obj)
        {
            return SocioempleadoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Socioempleado obj)
        {
            return SocioempleadoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Socioempleado obj)
        {
            return SocioempleadoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Socioempleado obj)
        {
            return SocioempleadoDAL.Update(obj);
        }
        public static int Update(BLL bll, Socioempleado obj)
        {
            return SocioempleadoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Socioempleado obj)
        {
            return SocioempleadoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Socioempleado obj)
        {
            return SocioempleadoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Socioempleado GetByPK(Socioempleado obj)
        {
            return SocioempleadoDAL.GetByPK(obj);
        }
        public static List<Socioempleado> GetAll(string WhereClause, string OrderBy)
        {
            return SocioempleadoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Socioempleado> GetAll(WhereParams parametros, string OrderBy)
        {
            return SocioempleadoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Socioempleado> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return SocioempleadoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Socioempleado> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return SocioempleadoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Socioempleado> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return SocioempleadoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return SocioempleadoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return SocioempleadoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return SocioempleadoDAL.GetMax(campo);
        }
        #endregion
    }
}
