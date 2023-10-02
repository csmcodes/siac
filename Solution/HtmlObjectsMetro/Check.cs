using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjectsMetro
{
    public class Check
    {
        public string id { get; set; }
        public bool? valor { get; set; }
        public string clase { get; set; }
        public bool obligatorio { get; set; }
        public bool habilitado { get; set; }
        public string ayuda { get; set; }
        public bool visible { get; set; }
        public bool info { get; set; }
        public string format { get; set; }
        public string data { get; set; }
        public bool makeswitch { get; set; }

        public Check()
        {
            obligatorio = false;
            habilitado = true;
            visible = true;
        }

        public string GetValor()
        {
            if (valor.HasValue)
            {
                return valor.Value ? "checked" : "";
            }
            return "";

        }

        public override string ToString()
        {


            //METRONIC CLASS
            if (makeswitch)
            {
                clase += " make-switch";
                data += "data-on-text=\"Si\" data-off-text=\"No\"";
            }

            StringBuilder html = new StringBuilder();

            //Input
            html.AppendFormat("<input id = '{0}'", id);
            html.AppendFormat(" type='{0}' ", "checkbox");                        
            html.AppendFormat(" class='{0}' ", clase);
            html.AppendFormat(" data-required='{0}' ", obligatorio);
            html.AppendFormat(" {0} ", data);
            html.AppendFormat(" {0} ", GetValor());
            html.AppendFormat(" {0} ", ((!habilitado) ? "disabled='disabled'" : "")); //disabled
            html.Append(" />");
       
            return html.ToString();
        }
    }
}
