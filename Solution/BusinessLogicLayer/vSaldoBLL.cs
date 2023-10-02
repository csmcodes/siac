using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vSaldoBLL
    {
        public static List<vSaldo> GetAll(WhereParams parametros, string OrderBy)
        {
            return vSaldoDAL.GetAll(parametros, OrderBy);
        }

        public static List<vSaldo> GetAll1(WhereParams parametros, string OrderBy)
        {
            return vSaldoDAL.GetAll1(parametros, OrderBy);
        }

    }
}
