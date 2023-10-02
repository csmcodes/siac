using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjectsMetro
{
    public class Span
    {
        public string id { get; set; }
        public string clase { get; set; }
        public List<object> content { get; set; }

    
        public Span(string clase)
        {
            this.clase = clase;
            content = new List<object>();
        }

        public void AddContent(object objeto)
        {
            content.Add(objeto);
        }

        public Span(string clase, params object[] objetos)
        {
            this.clase = clase;
            content = new List<object>();
            foreach (object item in objetos)
            {
                content.Add(item);
            }


        }

        public Span(params object[] objetos)
        {
            content = new List<object>();
            foreach (object item in objetos)
            {
                content.Add(item);
            }


        }

        public string GetValor()
        {
            StringBuilder html = new StringBuilder();
            foreach (object item in content)
            {
                html.Append(item.ToString());
            }
            return html.ToString();
        }


        public override string ToString()
        {

            StringBuilder html = new StringBuilder();
            html.Append("<span ");
            if (!string.IsNullOrEmpty(id))
                html.AppendFormat(" id='{0}' ", id);
            html.AppendFormat(" class='{0}' ", clase);
            html.Append(">");
            html.Append(GetValor());
            html.Append("</span>");

            return html.ToString();
        }
    }
}

