using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjectsMetro
{
    public class Icon
    {
        public string clase { get; set; }
        public string id { get; set; }

        public Icon(string clase)
        {
            this.clase = clase;
        }

        public override string ToString()
        {
            StringBuilder html = new StringBuilder();

            //Input
            html.Append("<i ");
            if (!string.IsNullOrEmpty(id))
                html.AppendFormat(" id='{0}' ", id);
            html.AppendFormat(" class='fa {0}' ", clase);
            html.Append(">");            
            html.Append("</i>");

            return html.ToString();
        }

    }
}
