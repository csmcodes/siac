using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vPlanillaSocioTotBLL
    {
        #region GetAll
        public static List<vPlanillaSocioTot> GetAll(WhereParams parametros, string OrderBy)
        {
            return vPlanillaSocioTotDAL.GetAll(parametros, OrderBy);
        }

        public static List<vPlanillaSocioTot> GetAllRub(WhereParams parametros, string OrderBy)
        {
            return vPlanillaSocioTotDAL.GetAllRub(parametros, OrderBy);
        }

        #endregion
    }
}
