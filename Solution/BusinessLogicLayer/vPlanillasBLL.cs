
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vPlanillasBLL
    {
        #region GetAll
        public static List<vPlanillas> GetAll(WhereParams parametros, string OrderBy)
        {
            return vPlanillasDAL.GetAll(parametros, OrderBy);
        }
        #endregion
    }
}
