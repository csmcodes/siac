
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vPlanillaClienteBLL
    {
        #region GetAll
        public static List<vPlanillaCliente> GetAll(WhereParams parametros, string OrderBy)
        {
            return vPlanillaClienteDAL.GetAll(parametros, OrderBy);
        }

        public static List<vPlanillaCliente> GetAllDet(WhereParams parametros, string OrderBy)
        {
            return vPlanillaClienteDAL.GetAllDet(parametros, OrderBy);
        }
        #endregion
    }
}
