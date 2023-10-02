using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vEstadoCuentaBLL
    {
        #region GetAll
        public static List<vEstadoCuenta> GetAllSumDoc(WhereParams parametros, string OrderBy)
        {
            return vEstadoCuentaDAL.GetAllSumDoc(parametros, OrderBy);
        }

        public static List<vEstadoCuenta> GetAllSumCan(WhereParams parametros, string OrderBy)
        {
            return vEstadoCuentaDAL.GetAllSumCan(parametros, OrderBy);
        }


        public static List<vEstadoCuenta> GetAllDoc(WhereParams parametros, string OrderBy)
        {
            return vEstadoCuentaDAL.GetAllDoc(parametros, OrderBy);
        }

        public static List<vEstadoCuenta> GetAllCan(WhereParams parametros, string OrderBy)
        {
            return vEstadoCuentaDAL.GetAllCan(parametros, OrderBy);
        }


        public static List<vEstadoCuenta> GetAllDoc1(WhereParams parametros, string OrderBy)
        {
            return vEstadoCuentaDAL.GetAllDoc1(parametros, OrderBy);
        }

        public static List<vEstadoCuenta> GetAllCan1(WhereParams parametros, string OrderBy)
        {
            return vEstadoCuentaDAL.GetAllCan1(parametros, OrderBy);
        }

        #endregion
    }
}
