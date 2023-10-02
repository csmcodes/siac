using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class vCuentasPorBLL
    {
        #region GetAll
        public static List<vCuentasPor> GetAll(WhereParams parwhere, WhereParams parhaving, string OrderBy)
        {

            return vCuentasPorDAL.GetAll(parwhere, parhaving, OrderBy);
        }


        public static List<vCuentasPor> GetAll1(WhereParams parwhere, WhereParams parrango, string OrderBy)
        {

            return vCuentasPorDAL.GetAll1(parwhere, parrango, OrderBy);
        }

        public static List<vCuentasPor> GetAllDoc(WhereParams parwhere, string OrderBy)
        {

            return vCuentasPorDAL.GetAllDoc(parwhere, OrderBy);
        }

        public static List<vCuentasPor> GetAllCan(WhereParams parwhere, string OrderBy)
        {

            return vCuentasPorDAL.GetAllCan(parwhere, OrderBy);
        }

        public static List<vCuentasPor> GetAllCanDet(WhereParams parwhere, string OrderBy)
        {

            return vCuentasPorDAL.GetAllCanDet(parwhere, OrderBy);
        }

        public static List<vCuentasPor> GetFull(WhereParams parwhere, string OrderBy)
        {

            return vCuentasPorDAL.GetFull(parwhere, OrderBy);
        }
        #endregion
    }
}
