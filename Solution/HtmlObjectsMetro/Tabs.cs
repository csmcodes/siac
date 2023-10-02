using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjectsMetro
{
    public class Tabs
    {
        public string id { get; set; }
        public string clase { get; set; }
        public List<Tab> content { get; set; }

        public Tabs()
        {


        }

        public Tabs(string id, params Tab[] objetos)
        {
            this.id = id;
            content = new List<Tab>();
            foreach (Tab item in objetos)
            {
                content.Add(item);
            }
        }

        public void AddTab(Tab tab)
        {
            if (content == null)
                content = new List<Tab>(); 
            content.Add(tab);
        }

        public override string ToString()
        {

            StringBuilder htmltitulos = new StringBuilder();
            StringBuilder htmlcontent = new StringBuilder();
            foreach (Tab tab in content)
            {
                htmltitulos.AppendFormat("<li class='{2}'><a href=\"#{0}\" data-toggle='tab' {3}>{1}</a></li>", tab.id, tab.titulo, tab.activo ? "active" : "", tab.activo ? "aria-expanded='true'" : "aria-expanded='false'");
                htmlcontent.AppendFormat(tab.ToString());

            }

            return string.Format("<ul class='nav nav-tabs'>{0}</ul>{1}", htmltitulos.ToString(), new Div("tab-content", htmlcontent));
            

        }

    }
}
