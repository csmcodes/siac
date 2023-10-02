using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vGuiasFacturasBLL
    {
        #region GetAll
        public static List<vGuiasFacturas> GetAll(WhereParams parametros, string OrderBy)
        {
            return vGuiasFacturasDAL.GetAll(parametros, OrderBy);
        }

        
        #endregion
    }
}
