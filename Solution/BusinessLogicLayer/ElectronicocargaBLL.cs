

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class ElectronicocargaBLL
    {
        #region Constructor

        public ElectronicocargaBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Electronicocarga obj)
        {
            return ElectronicocargaDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Electronicocarga obj)
        {
            return ElectronicocargaDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Electronicocarga obj)
        {
            return ElectronicocargaDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Electronicocarga obj)
        {
            return ElectronicocargaDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Electronicocarga obj)
        {
            return ElectronicocargaDAL.Update(obj);
        }
        public static int Update(BLL bll, Electronicocarga obj)
        {
            return ElectronicocargaDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Electronicocarga obj)
        {
            return ElectronicocargaDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Electronicocarga obj)
        {
            return ElectronicocargaDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Electronicocarga GetByPK(Electronicocarga obj)
        {
            return ElectronicocargaDAL.GetByPK(obj);
        }
        public static List<Electronicocarga> GetAll(string WhereClause, string OrderBy)
        {
            return ElectronicocargaDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Electronicocarga> GetAll(WhereParams parametros, string OrderBy)
        {
            return ElectronicocargaDAL.GetAll(parametros, OrderBy);
        }

         public static List<Electronicocarga> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return ElectronicocargaDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Electronicocarga> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return ElectronicocargaDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Electronicocarga> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return ElectronicocargaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return ElectronicocargaDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return ElectronicocargaDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return ElectronicocargaDAL.GetMax(campo);
        }
        #endregion
    }
}
