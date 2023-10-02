using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vDretencionBLL
    {
        #region GetAll
        public static List<vDretencion> GetAll(WhereParams parametros, string OrderBy)
        {
            return vDretencionDAL.GetAll(parametros, OrderBy);
        }

      
        #endregion
    }
}
