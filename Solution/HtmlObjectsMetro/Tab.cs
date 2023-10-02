using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjectsMetro
{
    public class Tab
    {
        public string id { get; set; }
        public string titulo { get; set; }
        public bool activo { get; set; }
        public List<object> content { get; set; }

        public Tab(params object[] objetos)
        {
            content = new List<object>();
            foreach (object item in objetos)
            {
                content.Add(item);
            }
        }

        public Tab(string id, string titulo, params object[] objetos)
        {            
            this.id = id;
            this.titulo = titulo;
            this.activo = false;
            content = new List<object>();
            foreach (object item in objetos)
            {
                content.Add(item);
            }
        }

        public Tab(string id, string titulo, bool activo, params object[] objetos)
        {
            this.id = id;
            this.titulo = titulo;
            this.activo = activo;
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
            string claseactivo = activo ? " active in " : "";
            

            StringBuilder html = new StringBuilder();

            //Input
            html.Append("<div ");
            if (!string.IsNullOrEmpty(id))
                html.AppendFormat(" id='{0}' ", id);
            html.AppendFormat(" class='tab-pane fade {0}' ", claseactivo);
            html.Append(">");
            html.Append(GetValor());
            html.Append("</div>");

            return html.ToString();
        }
    }
}
