using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vGuiasBLL
    {
        #region GetAll
        public static List<vGuias> GetAll(WhereParams parametros, string OrderBy)
        {
            return vGuiasDAL.GetAll(parametros, OrderBy);
        }

        public static List<vGuias> GetAllByPlanilla(WhereParams parametros, string OrderBy)
        {
            return vGuiasDAL.GetAllByPlanilla(parametros, OrderBy);
        }
        #endregion
    }
}
