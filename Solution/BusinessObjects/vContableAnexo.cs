using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Functions;
using System.Reflection;

namespace BusinessObjects
{
    public class vContableAnexo
    {



        public int cuenta { get; set; }
        public string cuentanombre { get; set; }

        public int? almacen { get; set; }
        public long? ddo_comproba { get; set; }


        public DateTime? fecha { get; set; }
        public long codigo { get; set; }
        public string doctran { get; set; }
        public int? cliente { get; set; }
        public string razon { get; set; }
        public int? debcre { get; set; }
        public decimal? debito { get; set; }
        public decimal? credito { get; set; }
        public string coddoc { get; set; }
        public string documento { get; set; }
        public decimal? montodoc { get; set; }
        public decimal? saldo { get; set; }
        public string codcan { get; set; }
        public string cancelacion { get; set; }
        public decimal? montocan { get; set; }

        public decimal? montodocdeb { get; set; }
        public decimal? canceldocdeb { get; set; }
        public decimal? saldodocdeb { get; set; }

        public decimal? montodoccre { get; set; }
        public decimal? canceldoccre { get; set; }
        public decimal? saldodoccre { get; set; }


        public decimal? montocandeb { get; set; }
        public decimal? montocancre { get; set; }





        public List<vContableAnexo> GetStruc()
        {
            return new List<vContableAnexo>();
        }
        public PropertyInfo[] GetProperties()
        {
            return this.GetType().GetProperties();
        }

    }
}
