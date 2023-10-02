

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class ElectronicoBLL
    {
        #region Constructor

        public ElectronicoBLL()
        {

        }

        #endregion

        #region Insert

        public static int Insert(Electronico obj)
        {
            return ElectronicoDAL.Insert(obj);
        }
        public static int Insert(BLL bll, Electronico obj)
        {
            return ElectronicoDAL.Insert(bll.transaction, obj);
        }

        public static int InsertIdentity(Electronico obj)
        {
            return ElectronicoDAL.InsertIdentity(obj);
        }
        public static int InsertIdentity(BLL bll, Electronico obj)
        {
            return ElectronicoDAL.InsertIdentity(bll.transaction, obj);
        }

        #endregion

        #region Update

        public static int Update(Electronico obj)
        {
            return ElectronicoDAL.Update(obj);
        }
        public static int Update(BLL bll, Electronico obj)
        {
            return ElectronicoDAL.Update(bll.transaction, obj);
        }
        #endregion

        #region Delete

        public static int Delete(Electronico obj)
        {
            return ElectronicoDAL.Delete(obj);
        }
       

        public static int Delete(BLL bll, Electronico obj)
        {
            return ElectronicoDAL.Delete(bll.transaction, obj);
        }

        #endregion

        #region Select

        public static Electronico GetByPK(Electronico obj)
        {
            return ElectronicoDAL.GetByPK(obj);
        }
        public static List<Electronico> GetAll(string WhereClause, string OrderBy)
        {
            return ElectronicoDAL.GetAll(WhereClause, OrderBy);
        }

        public static List<Electronico> GetAll(WhereParams parametros, string OrderBy)
        {
            return ElectronicoDAL.GetAll(parametros, OrderBy);
        }

         public static List<Electronico> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return ElectronicoDAL.GetAllTop(parametros, OrderBy, Top);
        }

        public static List<Electronico> GetAllByPage(string WhereClause, string OrderBy, int desde, int hasta)
        {
            return ElectronicoDAL.GetAllbyPage(WhereClause, OrderBy, desde, hasta);
        }

        public static List<Electronico> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return ElectronicoDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static int GetRecordCount(string WhereClause, string OrderBy)
        {
            return ElectronicoDAL.GetRecordCount(WhereClause, OrderBy);
        }

        public static int GetRecordCount(WhereParams parametros, string OrderBy)
        {
            return ElectronicoDAL.GetRecordCount(parametros, OrderBy);
        }

        public static int GetMax(string campo)
        {
            return ElectronicoDAL.GetMax(campo);
        }
        #endregion
    }
}
