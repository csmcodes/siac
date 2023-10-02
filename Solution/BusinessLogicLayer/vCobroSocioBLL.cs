using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vCobroSocioBLL
    {
        public static List<vCobroSocio> GetAll(WhereParams parametros, string OrderBy)
        {
            return vCobroSocioDAL.GetAll(parametros, OrderBy);
        }
    }
}
