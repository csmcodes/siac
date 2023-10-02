using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjectsMetro
{
    public class Textarea
    {
        public string id { get; set; }
        public string placeholder { get; set; }
        public object valor { get; set; }
        public string clase { get; set; }
        public bool obligatorio { get; set; }
        public bool habilitado { get; set; }
        public int? largo { get; set; }
        public int? cols { get; set; }
        public int? rows { get; set; }

        public string ayuda { get; set; }
        public bool visible { get; set; }
        public string data { get; set; }


        public Textarea()
        {
            obligatorio = false;
            habilitado = true;
            visible = true;            
        }

        public string GetValor()
        {

            return (valor != null) ? valor.ToString() : "";
        }

        public override string ToString()
        {


            //METRONIC CLASS
            clase += " form-control ";

            StringBuilder html = new StringBuilder();

            //Input
            html.AppendFormat("<textarea id = '{0}'", id);            
            html.AppendFormat(" placeholder='{0}' ", placeholder);
            if (cols.HasValue)
                html.AppendFormat(" cols='{0}'", cols);
            if (rows.HasValue)
                html.AppendFormat(" rows='{0}'", rows);            
            html.AppendFormat(" class='{0}' ", clase);
            html.AppendFormat(" data-required='{0}' ", obligatorio ? "true" : "false");
            html.AppendFormat(" {0} ", data);
            html.AppendFormat(" {0} ", ((!habilitado) ? "disabled='disabled'" : "")); //disabled
            html.AppendFormat(" {0} ", ((largo.HasValue) ? "maxlength='" + largo.Value + "'" : "")); //maxlength
            html.AppendFormat(">{0}</textarea>", GetValor());
            return html.ToString();
        }

    
    }
}
