using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
 public   class vRetencionesBLL
    {




        #region Select





        public static List<vRetenciones> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return vRetencionesDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static List<vRetenciones> GetAll(WhereParams parametros, string OrderBy)
        {
            return vRetencionesDAL.GetAll(parametros, OrderBy);
        }

        #endregion


    }
}
