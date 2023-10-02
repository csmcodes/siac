
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vVehiculoBLL
    {
        #region GetAll
        public static List<vVehiculo> GetAll(WhereParams parametros, string OrderBy)
        {
            return vVehiculoDAL.GetAll(parametros, OrderBy);
        }
        #endregion
    }
}
