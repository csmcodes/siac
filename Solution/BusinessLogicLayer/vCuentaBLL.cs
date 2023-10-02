using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vCuentasBLL
    {
        #region GetAll
        public static List<vCuentas> GetAll(WhereParams parametros, string OrderBy)
        {
            return vCuentasDAL.GetAll(parametros, OrderBy);
        }

        public static List<vCuentas> GetAllByPlanilla(WhereParams parametros, string OrderBy)
        {
            return vCuentasDAL.GetAllByPlanilla(parametros, OrderBy);
        }
        #endregion
    }
}
