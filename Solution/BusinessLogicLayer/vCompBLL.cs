using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vCompBLL
    {
        public static List<vComp> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return vCompDAL.GetAllTop(parametros, OrderBy,Top);
        }
    }
}
