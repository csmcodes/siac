using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public class vValoresSocios
    {
        public int? codigo { get; set; }
        public string id { get; set; }
        public string socio { get; set; }
        public decimal monto { get; set; }
        public decimal cancelado { get; set; }
        public decimal saldo { get; set; }

        public int? vehiculo { get; set; }
        public string disco { get; set; }

    }

       
}
