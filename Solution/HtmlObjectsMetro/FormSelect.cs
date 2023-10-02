using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjectsMetro
{
    public class FormSelect
    {
        public Label label { get; set; }
        public Select select { get; set; }
        public string faicon { get; set; }


        public FormSelect(string id, string valor, Dictionary<string, string> dictionary, string etiqueta, bool obligatorio, bool habilitado, string size, bool withempty )
        {
            SetProperties(id, valor, dictionary, etiqueta,  obligatorio, habilitado, size,withempty,  "",null);
        }

        public FormSelect(string id, string valor, Dictionary<string, string> dictionary, string etiqueta, bool obligatorio, bool habilitado, string size, bool withempty, string faicon)
        {
            SetProperties(id, valor, dictionary, etiqueta, obligatorio, habilitado, size,withempty, faicon,null);
        }
        public FormSelect(string id, string valor, Dictionary<string, string> dictionary, string etiqueta, bool obligatorio, bool habilitado, string size, bool withempty, string faicon, string data)
        {
            SetProperties(id, valor, dictionary, etiqueta, obligatorio, habilitado, size, withempty, faicon, data);
        }

        public void SetProperties(string id, string valor, Dictionary<string, string> dictionary, string etiqueta, bool obligatorio, bool habilitado, string size, bool withempty, string faicon, string data)
        {
            label = new Label() { texto = etiqueta };
            select = new Select() { id = id, valor = valor, diccionario = dictionary, obligatorio = obligatorio, habilitado = habilitado, clase = size, withempty = withempty, data=data };
            this.faicon = faicon;
        }


        public override string ToString()
        {
            StringBuilder html = new StringBuilder();

            html.Append(label.ToString());
            if (string.IsNullOrEmpty(faicon))
                html.Append(new Div(select).ToString());
            else
            {
                html.Append(new Div("input-group ", new Span("input-group-addon", new Icon(faicon)), select).ToString());
            }

            return new Div("form-group", html).ToString();

        }

    }
}
