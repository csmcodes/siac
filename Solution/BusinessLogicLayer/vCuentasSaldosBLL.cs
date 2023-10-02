using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vCuentasSaldosBLL
    {
        #region GetAll
        public static List<vCuentasSaldos> GetAll(WhereParams parametros, string OrderBy)
        {
            return vCuentasSaldosDAL.GetAll(parametros, OrderBy);
        }

        public static List<vCuentasSaldos> GetAllByPlanilla(WhereParams parametros, string OrderBy)
        {
            return vCuentasSaldosDAL.GetAllByPlanilla(parametros, OrderBy);
        }
        #endregion
    }
}
