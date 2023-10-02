using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
  public   class vDocumentoVentaBLL
    {

      public static List<vDocumentoVenta> GetAll(WhereParams parametros, string OrderBy)
      {
          return vDocumentoVentaDAL.GetAll(parametros, OrderBy);
      }
    }
}
