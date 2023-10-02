using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vCancelacionDetalleBLL
    {
         #region Select

      /*  public static List<vCancelacionDetalle> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return vCancelacionDetalleDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
           
        }*/

             public static List<vCancelacionDetalle> GetAll(WhereParams parametros, string OrderBy)
             {
                 return vCancelacionDetalleDAL.GetAll(parametros, OrderBy );
             }

        public static List<vCancelacionDetalle> GetAll1(WhereParams parametros, string OrderBy)
        {
            return vCancelacionDetalleDAL.GetAll1(parametros, OrderBy);
        }

        #endregion
    }
}

