using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjectsMetro
{
    public class FormInput
    {
        public Label label { get; set; }
        public Input input { get; set; }
        public string size { get; set; }
        public string faicon { get; set; }


        public FormInput(string id, string valor, string etiqueta, string placeholder, bool obligatorio, bool habilitado, string size, int? largo)
        {
            SetProperties(id, valor, etiqueta, placeholder, obligatorio, habilitado, size, largo, "");
        }

        public FormInput(string id, string valor, string etiqueta, string placeholder, bool obligatorio, bool habilitado, string size, int? largo, string faicon)
        {
            SetProperties(id, valor, etiqueta, placeholder, obligatorio, habilitado, size, largo, faicon);
        }

        public FormInput(string id, DateTime? valor, string etiqueta, string placeholder, bool obligatorio, bool habilitado, string size, string faicon)
        {
            SetPropertiesDateTime(id, valor, etiqueta, placeholder, obligatorio, habilitado, size, faicon);
        }

        public void SetProperties(string id, string valor, string etiqueta, string placeholder, bool obligatorio, bool habilitado, string size, int? largo, string faicon)
        {
            label = new Label() { texto = etiqueta };
            input = new Input() { id = id, placeholder = placeholder, valor = valor, obligatorio = obligatorio, habilitado = habilitado, largo = largo };
            this.faicon = faicon;
            this.size = size;
        }

        public void SetPropertiesDateTime(string id, DateTime? valor, string etiqueta, string placeholder, bool obligatorio, bool habilitado, string size, string faicon)
        {
            label = new Label() { texto = etiqueta };
            input = new Input() { id = id, placeholder = placeholder, datepicker=true,  datetimevalor = valor, obligatorio = obligatorio, habilitado = habilitado, largo = 10 };
            this.faicon = faicon;
            this.size = size;
        }




        public override string ToString()
        {
            StringBuilder html = new StringBuilder();

            html.Append(label.ToString());
            if (string.IsNullOrEmpty(faicon))
                html.Append(new Div(size ,input).ToString());
            else
            {
                html.Append(new Div(size+" input-group ", new Span("input-group-addon", new Icon(faicon)), input).ToString());
            }

            return new Div("form-group", html).ToString();

        }

    }
}
