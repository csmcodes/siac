using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class vDdocumentoBLL
    {
        #region GetAll
        public static List<vDdocumento> GetAll(WhereParams parametros, string OrderBy)
        {
            return vDdocumentoDAL.GetAll(parametros, OrderBy);

        }


        public static List<vDdocumento> GetAll1(WhereParams parametros, string OrderBy)
        {
            return vDdocumentoDAL.GetAll1(parametros, OrderBy);

        }
        #endregion
    }
}
