using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vGuiaPlanillaBLL
    {
        #region GetAll
        public static List<vGuiaPlanilla> GetAll(WhereParams parametros, string OrderBy)
        {
            return vGuiaPlanillaDAL.GetAll(parametros, OrderBy);
        }

        
        #endregion
    }
}
