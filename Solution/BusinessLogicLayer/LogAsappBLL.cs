
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class LogAsappBLL
    {
        public static int Insert(LogAsapp obj)
        {
            return LogAsappDAL.Insert(obj);
        }

        public static int Purgar(int meses)
        {
            return LogAsappDAL.Purgar(meses);
        }

        public static List<LogAsapp> GetAll(WhereParams parametros, string OrderBy)
        {
            return LogAsappDAL.GetAll(parametros, OrderBy);
        }
    }
}
