
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vDetalleBLL
    {
        #region GetAll
        public static List<vDetalle> GetAll(WhereParams parametros, string OrderBy)
        {
            return vDetalleDAL.GetAll(parametros, OrderBy);
        }
        public static List<vDetalle> GetAllDet(WhereParams parametros, string OrderBy)
        {
            return vDetalleDAL.GetAllDet(parametros, OrderBy);
        }
        #endregion
    }
}
