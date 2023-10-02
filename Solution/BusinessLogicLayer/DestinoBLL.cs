

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class DestinoBLL
    {
        #region Constructor

        public DestinoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Destino obj)
        {
            return DestinoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Destino obj)
        {
            return DestinoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Destino obj)
        {
            return DestinoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Destino obj)
        {
            return DestinoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Destino obj)
        {
            return DestinoDAL.Update(obj);
        }
        public static int Update(BLL bll, Destino obj)
        {
            return DestinoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Destino obj)
        {
            return DestinoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Destino obj)
        {
            return DestinoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Destino GetByPK(Destino obj)
        {
            return DestinoDAL.GetByPK(obj);
        }
        public static List<Destino> GetAll(string WhereClause, string OrderBy)
        {
            return DestinoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Destino> GetAll(WhereParams parametros, string OrderBy)
        {
            return DestinoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Destino> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return DestinoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Destino> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return DestinoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Destino> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return DestinoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return DestinoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return DestinoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return DestinoDAL.GetMax(campo);
        }
        #endregion
    }
}
