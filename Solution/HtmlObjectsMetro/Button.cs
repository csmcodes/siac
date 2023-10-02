using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjectsMetro
{
  
      public class Button
    {
        public string id { get; set; }
        public string faicon { get; set; }
        public string click{ get; set; }
        public string clase { get; set; }
        public string tooltip { get; set; }
        public string texto { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public bool circle { get; set; }

        public bool list { get; set; }
        public string[] opciones { get; set; }

        public bool edit { get; set; }
        public bool remove { get; set; }
        public bool add { get; set; }
        public bool cancel { get; set; }

        public Button()
        {

        }

        

       
        public override string ToString()
        {
            clase += " btn ";
            if (circle)
                clase += " btn-circle";

            if (!string.IsNullOrEmpty(size))
                clase += " btn-" + size;
            if (string.IsNullOrEmpty(texto))
                clase += " btn-icon-only";

            if (!string.IsNullOrEmpty(color))
                clase += " " + color;
            
            
            StringBuilder html = new StringBuilder();

            if (list)
            {
                clase += " dropdown-toggle";
                html.Append("<button type='button' data-toggle='dropdown' aria-expanded='false' ");
                if (!string.IsNullOrEmpty(id))
                    html.AppendFormat(" id='{0}' ", id);
                html.AppendFormat(" class='{0}' ", clase);
                html.Append(">");                
                html.Append("<span>"+ texto +  "</span> ");
                html.Append(new Icon(faicon));
                html.Append("</button>");
                html.Append("<ul class='dropdown-menu' role='menu'>");
                foreach (string item in opciones)
                {
                    html.AppendFormat("<li><a href='#' data-opcion='{0}' onclick='{1}'>{0}</a></li>", item, click);
                }
                html.Append("</ul>");

            }
            else
            {

                html.Append("<a href='#' ");

                if (!string.IsNullOrEmpty(id))
                    html.AppendFormat(" id='{0}' ", id);
                html.AppendFormat(" title='{0}' ", tooltip);
                html.AppendFormat(" class='{0}' ", clase);
                if (!string.IsNullOrEmpty(click))
                    html.AppendFormat(" onclick='{0}' ", click);
                html.Append(">");
                html.Append(new Icon(faicon));
                html.Append(texto);
                html.Append("</a>");
            }

            return html.ToString();
        }




    }
}
