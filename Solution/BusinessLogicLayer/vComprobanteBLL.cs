using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vComprobanteBLL
    {
        #region Select





        public static List<vComprobante> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return vComprobanteDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static List<vComprobante> GetAll(WhereParams parametros, string OrderBy)
        {
            return vComprobanteDAL.GetAll(parametros, OrderBy );
        }

        public static List<vComprobante> GetAllRange(WhereParams parametros, string OrderBy, int limit, int offset)
        {
            return vComprobanteDAL.GetAllRange(parametros, OrderBy, limit, offset);
        }

        public static List<vComprobante> GetAllTop(WhereParams parametros, string OrderBy)
        {
            return vComprobanteDAL.GetAllTop(parametros, OrderBy, 200);
        }

        public static List<vComprobante> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return vComprobanteDAL.GetAllTop(parametros, OrderBy,Top);
        }

        #endregion
    }
}
