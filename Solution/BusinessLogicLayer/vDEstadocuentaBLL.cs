using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vDEstadoCuentaBLL
    {
        #region GetAll
        public static List<vDEstadoCuenta> GetAll(WhereParams parametros, string OrderBy)
        {
            return vDEstadoCuentaDAL.GetAll(parametros, OrderBy);
        }

        public static List<vDEstadoCuenta> GetAllC(WhereParams parametros, string OrderBy)
        {
            return vDEstadoCuentaDAL.GetAllC(parametros, OrderBy);
        }

        public static List<vDEstadoCuenta> GetAllCA(WhereParams parametros, string OrderBy)
        {
            return vDEstadoCuentaDAL.GetAllCA(parametros, OrderBy);
        }

        public static List<vDEstadoCuenta> GetAllByPlanilla(WhereParams parametros, string OrderBy)
        {
            return vDEstadoCuentaDAL.GetAllByPlanilla(parametros, OrderBy);
        }
        #endregion
    }
}
