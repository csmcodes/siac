
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vDcancelacionBLL
    {
        #region GetAll
        public static List<vDcancelacion> GetAll(WhereParams parametros, string OrderBy)
        {
            return vDcancelacionDAL.GetAll(parametros, OrderBy);
        }

        public static List<vDcancelacion> GetAll1(WhereParams parametros, string OrderBy)
        {
            return vDcancelacionDAL.GetAll1(parametros, OrderBy);
        }
        #endregion
    }
}
