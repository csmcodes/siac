using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vRetencionBLL
    {




        #region Select





       
        public static List<vRetencion> GetAll(WhereParams parametros, string OrderBy)
        {
            return vRetencionDAL.GetAll(parametros, OrderBy);
        }

        #endregion


    }
}
