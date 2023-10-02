using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vComprobanteDescuadradoBLL
    {
        public static List<vComprobanteDescuadrado> GetAll(WhereParams parametros, string OrderBy)
        {
            return vComprobanteDescuadradoDAL.GetAll(parametros, OrderBy);
        }

        public static List<vComprobanteDescuadrado> GetAll1(WhereParams parametros, string OrderBy)
        {
            return vComprobanteDescuadradoDAL.GetAll1(parametros, OrderBy);
        }

    }
}
