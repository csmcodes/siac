

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DcancelacionsocioBLL
    {
        #region Constructor

        public DcancelacionsocioBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Dcancelacionsocio obj)
        {
            return DcancelacionsocioDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Dcancelacionsocio obj)
        {
            return DcancelacionsocioDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Dcancelacionsocio obj)
        {
            return DcancelacionsocioDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Dcancelacionsocio obj)
        {
            return DcancelacionsocioDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Dcancelacionsocio obj)
        {
            return DcancelacionsocioDAL.Update(obj);
        }
        public static int Update(BLL bll, Dcancelacionsocio obj)
        {
            return DcancelacionsocioDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Dcancelacionsocio obj)
        {
            return DcancelacionsocioDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Dcancelacionsocio obj)
        {
            return DcancelacionsocioDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Dcancelacionsocio GetByPK(Dcancelacionsocio obj)
        {
            return DcancelacionsocioDAL.GetByPK(obj);
        }
        public static List<Dcancelacionsocio> GetAll(string WhereClause, string OrderBy)
        {
            return DcancelacionsocioDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Dcancelacionsocio> GetAll(WhereParams parametros, string OrderBy)
        {
            return DcancelacionsocioDAL.GetAll(parametros, OrderBy);
        }

         public static List<Dcancelacionsocio> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DcancelacionsocioDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Dcancelacionsocio> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DcancelacionsocioDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Dcancelacionsocio> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DcancelacionsocioDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DcancelacionsocioDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DcancelacionsocioDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DcancelacionsocioDAL.GetMax(campo);
        }
        #endregion
    }
}
