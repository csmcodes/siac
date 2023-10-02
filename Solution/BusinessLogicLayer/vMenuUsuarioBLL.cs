using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
    public class vMenuUsuarioBLL
    {
        #region GetAll
        public static List<vMenuUsuario> GetAll(WhereParams parametros, string OrderBy)
        {
            return vMenuUsuarioDAL.GetAll(parametros, OrderBy);
        }
        #endregion
    }
}
