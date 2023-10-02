
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vReciboBLL
    {
        #region GetAll
        public static List<vRecibo> GetAll(WhereParams parametros, string OrderBy)
        {
            return vReciboDAL.GetAll(parametros, OrderBy);
        }      
        #endregion
    }
}
