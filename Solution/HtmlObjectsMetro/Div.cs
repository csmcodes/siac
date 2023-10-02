using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjectsMetro
{
    public class Div
    {
        public string id { get; set; }
        public string clase { get; set; }
        public List<object> content { get; set; }
        public string data { get; set; }


        public Div()
        {
            content = new List<object>(); 
        }

        public Div(string clase)
        {
            this.clase = clase;
            content = new List<object>();           


        }


        public Div(string clase, params object[] objetos)
        {
            this.clase = clase;
            content = new List<object>();
            foreach (object item in objetos)
            {
                content.Add(item);
            }


        }

        public Div(params object[] objetos)
        {
            content = new List<object>();
            foreach (object item in objetos)
            {
                content.Add(item);
            }


        }

        public void AddContent(object objeto)
        {
            if (content == null)
                content = new List<object>();
            content.Add(objeto); 
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

            //Input
            html.Append("<div ");
            if (!string.IsNullOrEmpty(id))
                html.AppendFormat(" id='{0}' ", id);
            html.AppendFormat(" class='{0}' ", clase);
            html.Append(data);
            html.Append(">");
            html.Append(GetValor());
            html.Append("</div>");

            return html.ToString();
        }
    }
}
