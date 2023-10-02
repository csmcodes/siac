using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
  public class vObligacionBLL
    {


      public static List<vObligacion> GetAll(WhereParams parametros, string OrderBy)
      {
          return vObligacionDAL.GetAll(parametros, OrderBy);
      }







    }
}
