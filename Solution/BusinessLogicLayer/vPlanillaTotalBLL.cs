using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
   public  class vPlanillaTotalBLL
    {

       #region GetAll
       public static List<vPlanillaTotal> GetAll(WhereParams parametros, string OrderBy)
       {
           return vPlanillaTotalDAL.GetAll(parametros, OrderBy);
       }
       #endregion

       public static List<vPlanillaTotal> GetAllS(WhereParams parametros, string OrderBy)
       {
           return vPlanillaTotalDAL.GetAllS(parametros, OrderBy);
       }


       public static List<vPlanillaTotal> GetAllN(WhereParams parametros, string OrderBy)
       {
           return vPlanillaTotalDAL.GetAllN(parametros, OrderBy);
       }

       public static List<vPlanillaTotal> GetAllNP(WhereParams parametros, string OrderBy)
       {
           return vPlanillaTotalDAL.GetAllNP(parametros, OrderBy);
       }

    }
}
