using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjects
{
    public class Document
    {

        public static Element Input(string id, string valor, string data, string clase, bool deshabilitado, bool obligatorio, ElementEnums.InputType tipo)
        {
            clase += " form-control";
            Element element = new Element() { id = id, valor = valor, clase = clase, desabilitado = deshabilitado, obligatorio = obligatorio, tipo = ElementEnums.ElementType.input, tipoinput = tipo, data = data };
            return element;
        }

        public static Element Label(string id, string texto, string data, string clase)
        {
            clase += " control-label";
            Element element = new Element() { id = id, valor = texto, clase = clase, tipo = ElementEnums.ElementType.label, data = data };
            return element;
        }

        public static Element Select(string id, string valor, Dictionary<string, string> dictionary, string data, string clase, bool withempty, bool multiple, bool deshabilitado, bool obligatorio)
        {
            clase += " form-control";
            Element element = new Element() { id = id, valor = valor, diccionario = dictionary, clase = clase, tipo = ElementEnums.ElementType.select, data = data, withempty = withempty, multiple = multiple, desabilitado = deshabilitado, obligatorio = obligatorio };
            return element;
        }


        public static Element TextArea(string id, string valor, string data, string clase, bool deshabilitado, bool obligatorio)
        {
            clase += " form-control";
            Element element = new Element() { id = id, valor = valor, clase = clase, desabilitado = deshabilitado, obligatorio = obligatorio, tipo = ElementEnums.ElementType.textarea, data = data };
            return element;
        }


        public static Element LabelInput(string id, string valor, string etiqueta, string data, string clase, string ancho, ElementEnums.InputType tipo)
        {
            return LabelInput(id, valor, etiqueta, data, clase, ancho, false, false, tipo);
        }

        public static Element LabelInput(string id, string valor, string etiqueta, string data, string clase, string ancho, bool deshabilitado, bool obligatorio, ElementEnums.InputType tipo)
        {
            Element div = new Element() { clase = ancho, tipo = ElementEnums.ElementType.div };
            div.AddContent(Label("", etiqueta, "", ""));
            if (tipo == ElementEnums.InputType.checkbox)
                div.AddContent("<br>");
            div.AddContent(Input(id, valor, data, clase, deshabilitado, obligatorio, tipo));
            return div;
        }

        public static Element LabelSelect(string id, string valor, Dictionary<string, string> dictionary, string etiqueta, string icono, string data, string clase, string ancho, bool withempty, bool multiple, bool deshabilitado, bool obligatorio)
        {
            Element div = new Element() { clase = ancho, tipo = ElementEnums.ElementType.div };
            div.AddContent(Label("", etiqueta, "", ""));
            div.AddContent(Select(id, valor, dictionary, data, clase, withempty, multiple, deshabilitado, obligatorio));
            return div;
        }

        public static Element LabelTextarea(string id, string valor, string etiqueta, string data, string clase, string ancho, bool deshabilitado, bool obligatorio)
        {
            Element div = new Element() { clase = ancho, tipo = ElementEnums.ElementType.div };
            div.AddContent(Label("", etiqueta, "", ""));
            div.AddContent(TextArea(id, valor, data, clase, deshabilitado, obligatorio));
            return div;
        }

        public static Element LabelIconInput(string id, string valor, string etiqueta, string icono, string data, string clase, string ancho, ElementEnums.InputType tipo)
        {
            return LabelIconInput(id, valor, etiqueta, icono, data, clase, ancho, false, false, tipo);
        }

        public static Element LabelIconInput(string id, string valor, string etiqueta, string icono, string data, string clase, string ancho, bool deshabilitado, bool obligatorio, ElementEnums.InputType tipo)
        {
            Element div = new Element() { clase = ancho, tipo = ElementEnums.ElementType.div };
            Element divgr = new Element() { clase = " input-group", tipo = ElementEnums.ElementType.div };
            Element span = new Element() { clase = "input-group-addon", tipo = ElementEnums.ElementType.span };
            Element icon = new Element() { clase = icono, tipo = ElementEnums.ElementType.icon };
            span.AddContent(icon);
            divgr.AddContent(span);
            divgr.AddContent(Input(id, valor, data, clase, deshabilitado, obligatorio, tipo));

            div.AddContent(Label("", etiqueta, "", ""));
            div.AddContent(divgr);
            return div;
        }


        public static Element LabelInputGroup(string id, string valor, string etiqueta, string data, string clase, string ancho, bool deshabilitado, bool obligatorio, ElementEnums.InputType tipo)
        {
            Element div = new Element() { clase = ancho, tipo = ElementEnums.ElementType.div };
            Element divgr = new Element() { clase = " input-group", tipo = ElementEnums.ElementType.div };
            Element span = new Element() { clase = "input-group-addon", tipo = ElementEnums.ElementType.span };
            span.AddContent(etiqueta);
            divgr.AddContent(span);
            divgr.AddContent(Input(id, valor, data, clase, deshabilitado, obligatorio, tipo));
            div.AddContent(divgr);
            return div;
        }

        public static Element LabelTextAreaGroup(string id, string valor, string etiqueta, string data, string clase, string ancho, bool deshabilitado, bool obligatorio)
        {
            Element div = new Element() { clase = ancho, tipo = ElementEnums.ElementType.div };
            Element divgr = new Element() { clase = " input-group", tipo = ElementEnums.ElementType.div };
            Element span = new Element() { clase = "input-group-addon", tipo = ElementEnums.ElementType.span };
            span.AddContent(etiqueta);
            divgr.AddContent(span);
            divgr.AddContent(TextArea(id, valor, data, clase, deshabilitado, obligatorio));
            div.AddContent(divgr);
            return div;
        }

        public static Element LabelIconSelect(string id, string valor, Dictionary<string, string> dictionary, string etiqueta, string icono, string data, string clase, string ancho, bool withempty, bool multiple, bool deshabilitado, bool obligatorio)
        {
            Element div = new Element() { clase = ancho, tipo = ElementEnums.ElementType.div };
            Element divgr = new Element() { clase = " input-group", tipo = ElementEnums.ElementType.div };
            Element span = new Element() { clase = "input-group-addon", tipo = ElementEnums.ElementType.span };
            Element icon = new Element() { clase = icono, tipo = ElementEnums.ElementType.icon };
            span.AddContent(icon);
            divgr.AddContent(span);
            divgr.AddContent(Select(id, valor, dictionary, data, clase, withempty, multiple, deshabilitado, obligatorio));

            div.AddContent(Label("", etiqueta, "", ""));
            div.AddContent(divgr);
            return div;
        }

        public static Element LabelSelectGroup(string id, string valor, Dictionary<string, string> dictionary, string etiqueta, string icono, string data, string clase, string ancho, bool withempty, bool multiple, bool deshabilitado, bool obligatorio)
        {
            Element div = new Element() { clase = ancho, tipo = ElementEnums.ElementType.div };
            Element divgr = new Element() { clase = " input-group", tipo = ElementEnums.ElementType.div };
            Element span = new Element() { clase = "input-group-addon", tipo = ElementEnums.ElementType.span };
            span.AddContent(etiqueta);
            divgr.AddContent(span);
            divgr.AddContent(Select(id, valor, dictionary, data, clase, withempty, multiple, deshabilitado, obligatorio));

            div.AddContent(Label("", etiqueta, "", ""));
            div.AddContent(divgr);
            return div;
        }

        public static Element SmallButton(string id, string texto, string icono, string color, string click)
        {
            Element link = new Element { id = id, clase = "btn btn-sm " + color, tipo = ElementEnums.ElementType.link };
            Element icon = new Element() { clase = icono, tipo = ElementEnums.ElementType.icon };
            link.AddContent(icon);
            if (!string.IsNullOrEmpty(texto))
                link.AddContent("&nbsp;" + texto);
            return link;
        }

        public static Element CircleButton(string id, string tooltip, string icono, string color, string click)
        {
            Element link = new Element { id = id, clase = "btn btn-icon-only btn-circle " + color, onclick = click, tipo = ElementEnums.ElementType.link };
            Element icon = new Element() { clase = icono, tipo = ElementEnums.ElementType.icon };
            link.AddContent(icon);
            //if (!string.IsNullOrEmpty(texto))
            //    link.AddContent("&nbsp;" + texto);
            return link;
        }

        public static Element CircleSmallButton(string id, string texto, string tooltip, string icono, string color, string click)
        {
            string btncolor = "btn-default";
            if (!string.IsNullOrEmpty(color))
                btncolor = color;
            Element link = new Element { id = id, clase = "btn btn-circle  btn-sm " + btncolor, onclick = click, tipo = ElementEnums.ElementType.link };
            Element icon = new Element() { clase = icono, tipo = ElementEnums.ElementType.icon };
            link.AddContent(icon);
            if (!string.IsNullOrEmpty(texto))
                link.AddContent("&nbsp;" + texto);
            return link;
        }


        public static Element FormLabelInput(string id, string valor, string etiqueta, string data, string clase, string ancho, bool deshabilitado, bool obligatorio, ElementEnums.InputType tipo)
        {
            Element div = new Element() { clase = ancho, tipo = ElementEnums.ElementType.div };
            //Element divgr = new Element() { clase = " input-group", tipo = ElementEnums.ElementType.div };
            //Element span = new Element() { clase = "input-group-addon", tipo = ElementEnums.ElementType.span };
            div.AddContent(etiqueta);
            //divgr.AddContent(span);
            div.AddContent(Input(id, valor, data, clase, deshabilitado, obligatorio, tipo));
            //div.AddContent(divgr);
            return div;
        }

        public static Element FormLabelSelect(string id, string valor, Dictionary<string, string> dictionary, string etiqueta, string icono, string data, string clase, string ancho, bool withempty, bool multiple, bool deshabilitado, bool obligatorio)
        {
            Element div = new Element() { clase = ancho, tipo = ElementEnums.ElementType.div };
            //Element divgr = new Element() { clase = " input-group", tipo = ElementEnums.ElementType.div };
            //Element span = new Element() { clase = "input-group-addon", tipo = ElementEnums.ElementType.span };
            div.AddContent(etiqueta);
            //divgr.AddContent(span);
            div.AddContent(Select(id, valor, dictionary, data, clase, withempty, multiple, deshabilitado, obligatorio));

            //div.AddContent(Label("", etiqueta, "", ""));
            //div.AddContent(divgr);
            return div;
        }


        public static Element LabelIconDateRange(string id, DateTime? desde, DateTime? hasta, string etiqueta, string icono, string data, string clase, string ancho, bool deshabilitado, bool obligatorio)
        {
            Element div = new Element() { clase = ancho, tipo = ElementEnums.ElementType.div };
            Element divgr = new Element() { clase = " input-group", tipo = ElementEnums.ElementType.div };
            Element span = new Element() { clase = "input-group-addon", tipo = ElementEnums.ElementType.span };
            Element icon = new Element() { clase = icono, tipo = ElementEnums.ElementType.icon };
            span.AddContent(icon);
            divgr.AddContent(span);
            divgr.AddContent(Input(id + "desde", desde.HasValue?desde.Value.ToString("dd/MM/yyyy"):"", data, clase, deshabilitado, obligatorio, ElementEnums.InputType.date));
            divgr.AddContent(span);
            //divgr.AddContent(new Element() { clase = "input-group-addon", tipo = ElementEnums.ElementType.span});
            divgr.AddContent(Input(id + "hasta", hasta.HasValue ? hasta.Value.ToString("dd/MM/yyyy") : "", data, clase, deshabilitado, obligatorio, ElementEnums.InputType.date));
            div.AddContent(Label("", etiqueta, "", ""));
            div.AddContent(divgr);
            return div;
        }

        public static Element DropdownButtons(string id, string texto, string icono, Dictionary<string, string> opciones, string data, string clase, string claseul)
        {
            Element div = new Element() { clase = "btn-group", tipo = ElementEnums.ElementType.div };
            Element icon = new Element() { clase = icono, tipo = ElementEnums.ElementType.icon };
            Element button = new Element() { clase = "btn " + clase + " dropdown-toggle", tipo = ElementEnums.ElementType.button, data = "toogle='dropdown'" };

            if (!string.IsNullOrEmpty(texto))
                button.AddContent(texto);
            button.AddContent("&nbsp;");
            if (!string.IsNullOrEmpty(icono))
                button.AddContent(icon);


            Element ul = new Element() { clase = "dropdown-menu " + claseul, data = "role='menu'", diccionario = opciones, tipo = ElementEnums.ElementType.ul };

            div.AddContent(button);
            div.AddContent(ul);
            return div;
        }


        public static Element ImageUpload(string id, string url, string clase, string ancho, bool deshabilitado, bool obligatorio)
        {
            Element div = new Element() { clase = ancho, tipo = ElementEnums.ElementType.div };
            Element div1 = new Element() { data = "data-provides='fileinput'", clase = "fileinput fileinput-new", tipo = ElementEnums.ElementType.div };
            Element div2 = new Element() { data = "style='width: 400px; height: 300px; '", clase = "fileinput-new thumbnail", tipo = ElementEnums.ElementType.div };
            if (string.IsNullOrEmpty(url))
                url = "http://www.placehold.it/400x350/EFEFEF/AAAAAA&amp;text=sin+imagen";
            div2.AddContent("<img src='" + url + "' alt='' />");
            Element div3 = new Element() { data = "style='max-width:400px;max-height:300px;'", clase = "fileinput-preview fileinput-exists thumbnail", tipo = ElementEnums.ElementType.div };

            Element div4 = new Element() { tipo = ElementEnums.ElementType.div };
            Element span = new Element() { tipo = ElementEnums.ElementType.span, clase = "btn default btn-file" };

            Element span1 = new Element() { tipo = ElementEnums.ElementType.span, clase = "fileinput-new" };
            span1.AddContent("Seleccione imágen");
            Element span2 = new Element() { tipo = ElementEnums.ElementType.span, clase = "fileinput-exists" };
            span2.AddContent("Cambiar");

            span.AddContent(span1);
            span.AddContent(span2);
            span.AddContent("<input id='" + id + "' type='file' name='...'>");

            Element link = new Element() { tipo = ElementEnums.ElementType.link, clase = "btn red fileinput-exists", data = "data-dismiss='fileinput'" };
            link.AddContent("Quitar");

            div4.AddContent(span);
            div4.AddContent(link);


            div1.AddContent(div2);
            div1.AddContent(div3);
            div1.AddContent(div4);
            div.AddContent(div1);

            Element div5 = new Element() { clase = "clearfix margin-top-10", tipo = ElementEnums.ElementType.div };
            Element span3 = new Element() { tipo = ElementEnums.ElementType.span, clase = "label label-danger" };
            span3.AddContent("NOTA!");
            div5.AddContent(span3);
            div5.AddContent("  Imagenes de máximo 3Mb");
            div.AddContent(div5);

            return div;

        }


    }
}
