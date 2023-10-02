using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjectsMetro
{
    public class Select
    {
        public string id { get; set; }
        public object valor { get; set; }
        public string clase { get; set; }
        public bool obligatorio { get; set; }
        public bool habilitado { get; set; }
        public string ayuda { get; set; }
        public bool visible { get; set; }
        public bool info { get; set; }
        public string format { get; set; }
        public string data { get; set; }

        public Dictionary<string, string> diccionario { get; set; }

        public bool multiselect { get; set; }
        public bool withempty { get; set; }

        public string orderby { get; set; }


        public Select()
        {
            obligatorio = false;
            habilitado = true;
            multiselect = false;
        }
        public string GetValor()
        {
            return (valor != null) ? valor.ToString() : "";
        }

        public override string ToString()
        {

            //METRONIC CLASS
            clase += " form-control ";
            //if (multiselect)
            //    clase = "chzn-select " + clase;

            StringBuilder html = new StringBuilder();

            //html.AppendFormat("<select id='{0}' name='select' class='uniformselect {1}' data-obligatorio='{2}' {3} {4} placeholder='{5}'>", id, clase, obligatorio, ((!habilitado) ? "disabled='disabled'" : ""), ((multiselect) ? " multiple='multiple'" : ""),placeholder);
            html.AppendFormat("<select id='{0}' name='select' ", id);
            html.AppendFormat(" class='{0}' ", clase);
            html.AppendFormat(" data-required='{0}' ", obligatorio);
            html.AppendFormat(" {0} ", ((!habilitado) ? "disabled='disabled'" : ""));
            html.AppendFormat(" {0} ", ((multiselect) ? " multiple " : ""));                        
            html.AppendFormat(" {0} ", data);            
            html.Append(">");
            if (withempty)
            {
                string selected = "";
                if (GetValor() == string.Empty)
                    selected = "selected";
                html.AppendFormat("<option {0}></option>", selected);
            }



            if (string.IsNullOrEmpty(orderby))
            {
                foreach (KeyValuePair<string, string> entry in diccionario)
                {
                    string selected = "";
                    if (GetValor() == entry.Key)
                        selected = "selected";
                    html.AppendFormat("<option value='{0}' {2}>{1}</option>", entry.Key, entry.Value, selected);
                }
            }
            else
            {
                foreach (KeyValuePair<string, string> entry in diccionario.OrderBy(key => (string.IsNullOrEmpty(orderby) ? "" : (orderby.ToUpper() == "KEY") ? key.Key : key.Value)))
                {
                    string selected = "";
                    if (GetValor() == entry.Key)
                        selected = "selected";
                    html.AppendFormat("<option value='{0}' {2}>{1}</option>", entry.Key, entry.Value, selected);
                }
            }
            html.AppendLine("</select>");
           

            return html.ToString();
        }

    }
}
