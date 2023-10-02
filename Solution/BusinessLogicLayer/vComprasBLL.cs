using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;


namespace BusinessLogicLayer
{
  public class vComprasBLL
    {


      public static List<vCompras> GetAll(WhereParams parametros, string OrderBy)
      {
          return vComprasDAL.GetAll(parametros, OrderBy);
      }


    }
}
