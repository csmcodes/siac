using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
   public  class vVentasRetencionesBLL
    {

       public static List<vVentasRetenciones> GetAll(WhereParams parametros, string OrderBy)
       {
           return vVentasRetencionesDAL.GetAll(parametros, OrderBy);
       }

    }
}
