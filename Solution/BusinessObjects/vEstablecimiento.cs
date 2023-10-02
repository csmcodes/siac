using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace BusinessObjects
{
    public class vEstablecimiento
    {
        public string id { get; set; }
        public decimal total { get; set; }

        public List<vEstablecimiento> GetStruc()
        {
            return new List<vEstablecimiento>();
        }
        public PropertyInfo[] GetProperties()
        {
            return this.GetType().GetProperties();
        }
    }

     
}
