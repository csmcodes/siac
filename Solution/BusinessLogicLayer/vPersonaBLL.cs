
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vPersonaBLL
    {


        #region Select


        public static List<Persona> GetAll(WhereParams parametros, string OrderBy)
        {
            return vPersonaDAL.GetAll(parametros, OrderBy);
        }

   

        public static List<Persona> GetAllByPage(WhereParams parametros, string OrderBy, int desde, int hasta)
        {
            return vPersonaDAL.GetAllbyPage(parametros, OrderBy, desde, hasta);
        }

        public static List<Persona> GetAllTop(WhereParams parametros, string OrderBy, int Top)
        {
            return vPersonaDAL.GetAllTop(parametros, OrderBy, Top);
        }
        #endregion


    }
}
