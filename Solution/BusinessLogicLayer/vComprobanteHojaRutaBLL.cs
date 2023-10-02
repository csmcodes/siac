using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class vComprobanteHojaRutaBLL
    {
        #region GetAll
        public static List<vComprobanteHojaRuta> GetAll(WhereParams parametros, string OrderBy)
        {
            return vComprobanteHojaRutaDAL.GetAll(parametros, OrderBy);
        }


        #endregion
    }
}
