using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;


namespace BusinessLogicLayer
{
   public  class vHojaRutaReporteBLL
    {

       #region GetAll
       public static List<vHojaRutaReporte> GetAll(WhereParams parametros, string OrderBy)
       {
           return vHojaRutaReporteDAL.GetAll(parametros, OrderBy);
       }
       #endregion
    }
}
