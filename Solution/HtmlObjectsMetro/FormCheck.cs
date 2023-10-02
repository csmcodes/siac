using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjectsMetro
{
    public class FormCheck
    {
        public Label label { get; set; }
        public Check check { get; set; }
        public string faicon { get; set; }


        public FormCheck(string id, bool? valor, string etiqueta, bool obligatorio, bool habilitado, string size,bool makeswitch)
        {
            SetProperties(id, valor, etiqueta, obligatorio, habilitado, size, makeswitch, "");
        }

        public FormCheck(string id, bool? valor, string etiqueta, bool obligatorio, bool habilitado, string size, bool makeswitch, string faicon)
        {
            SetProperties(id, valor, etiqueta, obligatorio, habilitado, size, makeswitch, faicon);
        }

        public void SetProperties(string id, bool? valor, string etiqueta,  bool obligatorio, bool habilitado, string size, bool makeswitch, string faicon)
        {
            label = new Label() { texto = etiqueta };
            check = new Check() { id = id, valor = valor, obligatorio = obligatorio, habilitado = habilitado, clase = size, makeswitch= makeswitch};
            this.faicon = faicon;
        }


        public override string ToString()
        {
            StringBuilder html = new StringBuilder();

            html.Append(label.ToString());
            if (string.IsNullOrEmpty(faicon))
                html.Append(new Div(check).ToString());
            else
            {
                html.Append(new Div("input-group ", new Span("input-group-addon", new Icon(faicon)), check).ToString());
            }

            return new Div("form-group", html).ToString();

        }
    }
}
