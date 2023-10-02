

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class MensajedestinoBLL
    {
        #region Constructor

        public MensajedestinoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Mensajedestino obj)
        {
            return MensajedestinoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Mensajedestino obj)
        {
            return MensajedestinoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Mensajedestino obj)
        {
            return MensajedestinoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Mensajedestino obj)
        {
            return MensajedestinoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Mensajedestino obj)
        {
            return MensajedestinoDAL.Update(obj);
        }
        public static int Update(BLL bll, Mensajedestino obj)
        {
            return MensajedestinoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Mensajedestino obj)
        {
            return MensajedestinoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Mensajedestino obj)
        {
            return MensajedestinoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Mensajedestino GetByPK(Mensajedestino obj)
        {
            return MensajedestinoDAL.GetByPK(obj);
        }
        public static List<Mensajedestino> GetAll(string WhereClause, string OrderBy)
        {
            return MensajedestinoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Mensajedestino> GetAll(WhereParams parametros, string OrderBy)
        {
            return MensajedestinoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Mensajedestino> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return MensajedestinoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Mensajedestino> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return MensajedestinoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Mensajedestino> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return MensajedestinoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return MensajedestinoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return MensajedestinoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return MensajedestinoDAL.GetMax(campo);
        }
        #endregion
    }
}
