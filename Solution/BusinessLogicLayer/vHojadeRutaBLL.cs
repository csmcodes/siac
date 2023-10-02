
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vHojadeRutaBLL
    {
        #region GetAll
        public static List<vHojadeRuta> GetAll(WhereParams parametros, string OrderBy)
        {
            return vHojadeRutaDAL.GetAll(parametros, OrderBy);
        }
        #endregion
    }
}
