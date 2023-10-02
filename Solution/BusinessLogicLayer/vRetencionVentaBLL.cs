using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vRetencionVentaBLL
    {

        public static List<vRetencionVenta> GetAll(WhereParams parametros, string OrderBy)
        {
            return vRetencionVentaDAL.GetAll(parametros, OrderBy);
        }

        public static List<vRetencionVenta> GetAllCom(WhereParams parametros, string OrderBy)
        {
            return vRetencionVentaDAL.GetAllCom(parametros, OrderBy);
        }

    }
}
