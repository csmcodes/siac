﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;


namespace BusinessLogicLayer
{
    public class vCancelacionBLL
    {
        #region GetAll
        public static List<vCancelacion> GetAll(WhereParams parametros, string OrderBy)
        {
            return vCancelacionDAL.GetAll(parametros, OrderBy);
        }
        #endregion

        public static List<vCancelacion> GetAllT(WhereParams parametros, string OrderBy)
        {
            return vCancelacionDAL.GetAllT(parametros, OrderBy);
        }
        public static List<vCancelacion> GetAll1(WhereParams parametros, string OrderBy)
        {
            return vCancelacionDAL.GetAll1(parametros, OrderBy);
        }
    }
}