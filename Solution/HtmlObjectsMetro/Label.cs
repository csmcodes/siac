using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjectsMetro
{
    public class Label
    {
        public string id { get; set; }
        public string para { get; set; }
        public string clase { get; set; }
        public string texto { get; set; }

        public Label()
        {
            
        }


        public override string ToString()
        {
            clase += " control-label";

            StringBuilder html = new StringBuilder();

            //Input
            html.Append("<label ");
            if (!string.IsNullOrEmpty(id))
                html.AppendFormat(" id='{0}' ", id);
            html.AppendFormat(" class='{0}' ", clase);
            if (!string.IsNullOrEmpty(para))
                html.AppendFormat(" for='{0}' ", id);
            html.Append(">");
            html.Append(texto);
            html.Append("</label>");

            return html.ToString();
        }
    }
}
