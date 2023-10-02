
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vDbancarioBLL
    {
        #region GetAll
        public static List<vDbancario> GetAll(WhereParams parametros, string OrderBy)
        {
            return vDbancarioDAL.GetAll(parametros, OrderBy);
        }      
        #endregion
    }
}
