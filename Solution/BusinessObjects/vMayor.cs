using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace BusinessObjects
{
    public class vMayor
    {
        public DateTime? fecha { get; set; }
        public string comprobante { get; set; }
        public string concepto { get; set; }
        public decimal credito { get; set; }
        public decimal debito { get; set; }
        public decimal final { get; set; }



        #region Methods
        public PropertyInfo[] GetProperties()
        {
            return this.GetType().GetProperties();
        }

        public List<vMayor> GetStruc()
        {
            return new List<vMayor>();
        }

        #endregion
    }
}
