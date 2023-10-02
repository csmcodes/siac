using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjects
{
    public class Element
    {
        public string id { get; set; }
        public ElementEnums.ElementType tipo { get; set; }
        public ElementEnums.InputType tipoinput { get; set; }
        public object valor { get; set; }
        public string data { get; set; }
        public string clase { get; set; }

        public int? largo { get; set; }
        public int? rows { get; set; }
        public int? cols { get; set; }


        public bool desabilitado { get; set; }
        public bool lectura { get; set; }
        public bool obligatorio { get; set; }
        public bool withempty { get; set; }
        public bool multiple { get; set; }

        public string onclick { get; set; }
        public string onchange { get; set; }
        public string onclickdropdown { get; set; }

        public Dictionary<string, string> diccionario { get; set; }

        public List<object> content { get; set; }


        public string Mark(bool open)
        {
            string html = "";
            switch (tipo)
            {
                case ElementEnums.ElementType.input:
                    string strtipo = tipoinput.ToString();
                    if (tipoinput == ElementEnums.InputType.date)
                        strtipo = ElementEnums.InputType.text.ToString();
                    html = open ? "<input type='" + strtipo + "' " : "</input>";
                    break;
                case ElementEnums.ElementType.div:
                    html = open ? "<div " : "</div>";
                    break;
                case ElementEnums.ElementType.select:
                    html = open ? "<select " : "</select>";
                    break;
                case ElementEnums.ElementType.span:
                    html = open ? "<span " : "</span>";
                    break;
                case ElementEnums.ElementType.textarea:
                    html = open ? "<textarea " : "</textarea>";
                    break;
                case ElementEnums.ElementType.label:
                    html = open ? "<label " : "</label>";
                    break;
                case ElementEnums.ElementType.icon:
                    html = open ? "<i " : "</i>";
                    break;
                case ElementEnums.ElementType.link:
                    html = open ? "<a " : "</a>";
                    break;
                case ElementEnums.ElementType.button:
                    html = open ? "<button " : "</button>";
                    break;
                case ElementEnums.ElementType.ul:
                    html = open ? "<ul " : "</ul>";
                    break;

            }
            return html;
        }

        public string GetValor()
        {
            if (tipo == ElementEnums.ElementType.input)
            {
                if (tipoinput == ElementEnums.InputType.date)
                {
                    DateTime datetimevalor;
                    if (DateTime.TryParse(valor.ToString(), out datetimevalor))
                        return datetimevalor.ToShortDateString();
                }
                if (tipoinput == ElementEnums.InputType.checkbox)
                {
                    bool booleanvalor;
                    if (bool.TryParse(valor.ToString(), out booleanvalor))
                        return booleanvalor ? " checked " : "";
                    else
                        return "";
                }
            }
            else if (tipo == ElementEnums.ElementType.div || tipo == ElementEnums.ElementType.span || tipo == ElementEnums.ElementType.link || tipo == ElementEnums.ElementType.button || tipo == ElementEnums.ElementType.ul)
            {
                StringBuilder html = new StringBuilder();
                if (content != null)
                {
                    foreach (object item in content)
                    {
                        html.Append(item.ToString());
                    }
                }
                return html.ToString();
            }
            return (valor != null) ? valor.ToString() : "";
        }

        public override string ToString()
        {

            if (tipoinput == ElementEnums.InputType.date)
            {
                clase += " date-picker";
                data += " data-date-format=\"dd-mm-yyyy\" data-date-viewmode=\"years\"";
            }
            if (tipo == ElementEnums.ElementType.button)
                data += " type='button' data-toggle='dropdown' aria-expanded='false' ";

            if (tipo == ElementEnums.ElementType.icon)
                clase = "fa " + clase;
            if (multiple)
                clase += " select2 ";

            StringBuilder html = new StringBuilder();


            html.Append(Mark(true));
            if (!string.IsNullOrEmpty(id))
                html.AppendFormat(" id='{0}' ", id);
            html.AppendFormat(" class='{0}' ", clase);
            //EVENTOS

            if (!string.IsNullOrEmpty(onclick))
                html.AppendFormat(" onclick='{0}' ", onclick);
            if (!string.IsNullOrEmpty(onchange))
                html.AppendFormat(" onchange='{0}' ", onchange);

            if (obligatorio)
                html.AppendFormat(" data-required='true'");
            html.AppendFormat(" {0} ", data);

            if (multiple)
                html.Append(" multiple ");

            if (desabilitado)
                html.AppendFormat(" disabled ");

            string strvalor = GetValor();
            if (tipo == ElementEnums.ElementType.label || tipo == ElementEnums.ElementType.textarea || tipo == ElementEnums.ElementType.div || tipo == ElementEnums.ElementType.span || tipo == ElementEnums.ElementType.select || tipo == ElementEnums.ElementType.icon || tipo == ElementEnums.ElementType.link || tipo == ElementEnums.ElementType.button || tipo == ElementEnums.ElementType.ul)
                html.AppendFormat(">{0}", strvalor);
            if (tipo == ElementEnums.ElementType.input)
            {
                if (tipoinput == ElementEnums.InputType.checkbox)
                    html.AppendFormat(" {0}> ", strvalor);
                else
                    html.AppendFormat(" value='{0}'> ", strvalor);
            }




            if (tipo == ElementEnums.ElementType.select)
            {
                html.Append(">");
                if (withempty)
                {
                    string selected = "";
                    if (string.IsNullOrEmpty(GetValor()))
                        selected = "selected";
                    html.AppendFormat("<option {0}></option>", selected);
                }

                foreach (KeyValuePair<string, string> entry in diccionario)
                {
                    string selected = "";
                    if (GetValor() == entry.Key)
                        selected = "selected";
                    html.AppendFormat("<option value='{0}' {2}>{1}</option>", entry.Key, entry.Value, selected);
                }
            }

            if (tipo == ElementEnums.ElementType.ul)
            {

                foreach (KeyValuePair<string, string> entry in diccionario)
                {
                    html.AppendFormat("<li><a id='{0}'", entry.Key);
                    if (onclickdropdown != "")
                        html.AppendFormat(" onclick='{0}' ", onclickdropdown);
                    html.AppendFormat(">{0}</a>", entry.Value);

                    html.Append("</li>");
                }
            }


            html.Append(Mark(false));


            return html.ToString();
        }

        public void AddContent(object objeto)
        {
            if (content == null)
                content = new List<object>();
            content.Add(objeto);
        }


    }
}
