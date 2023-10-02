using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vVentaBLL
    {
        #region GetAll
        public static List<vVenta> GetAll(WhereParams parametros, string OrderBy)
        {
            return vVentaDAL.GetAll(parametros, OrderBy);

        }
        public static List<vVenta> GetAll1(WhereParams parametros, string OrderBy)
        {
            return vVentaDAL.GetAll1(parametros, OrderBy);

        }
        #endregion
    }
}
