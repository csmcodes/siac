

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class MensajeBLL
    {
        #region Constructor

        public MensajeBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Mensaje obj)
        {
            return MensajeDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Mensaje obj)
        {
            return MensajeDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Mensaje obj)
        {
            return MensajeDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Mensaje obj)
        {
            return MensajeDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Mensaje obj)
        {
            return MensajeDAL.Update(obj);
        }
        public static int Update(BLL bll, Mensaje obj)
        {
            return MensajeDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Mensaje obj)
        {
            return MensajeDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Mensaje obj)
        {
            return MensajeDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Mensaje GetByPK(Mensaje obj)
        {
            return MensajeDAL.GetByPK(obj);
        }
        public static List<Mensaje> GetAll(string WhereClause, string OrderBy)
        {
            return MensajeDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Mensaje> GetAll(WhereParams parametros, string OrderBy)
        {
            return MensajeDAL.GetAll(parametros, OrderBy);
        }

         public static List<Mensaje> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return MensajeDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Mensaje> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return MensajeDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Mensaje> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return MensajeDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return MensajeDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return MensajeDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return MensajeDAL.GetMax(campo);
        }
        #endregion
    }
}
