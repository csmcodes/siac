
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vDnotacreBLL
    {
        #region GetAll
        public static List<vDnotacre> GetAll(WhereParams parametros, string OrderBy)
        {
            return vDnotacreDAL.GetAll(parametros, OrderBy);
        }      
        #endregion
    }
}
