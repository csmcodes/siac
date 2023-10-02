
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vDcontableBLL
    {
        #region GetAll
        public static List<vDcontable> GetAll(WhereParams parametros, string OrderBy)
        {
            return vDcontableDAL.GetAll(parametros, OrderBy);
        }      
        #endregion
    }
}
