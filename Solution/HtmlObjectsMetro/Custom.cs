using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Functions;

namespace HtmlObjectsMetro
{
    public class Custom
    {

        public static string HtmlAudit(string creausr, DateTime? creafecha, string modusr, DateTime? modfecha)
        {
            return new Span("audit", string.Format("<b>Creación:</b> {0} {1}  <b>Modificación:</b> {2} {3}", creausr, creafecha, modusr, modfecha)).ToString();
        }


        public static string AddButton()
        {
            return EditButton("AddNew()");
        }


        public static string AddButton(string click)
        {
            Button boton = new Button();
            boton.circle = true;
            boton.click = click;
            boton.faicon = "fa-plus-circle";
            boton.color = "green";
            boton.tooltip = "Agregar";

            return boton.ToString();
        }

        public static string EditButton()
        {
            return EditButton("Edit($(this.parentNode.parentNode).data())");
        }

        public static string EditButton(string click)
        {
            Button boton = new Button();
            boton.circle = true;
            boton.click = click;
            boton.faicon = "fa-edit";
            boton.color = "blue";
            boton.tooltip = "Editar";

            return boton.ToString();
        }

        public static string RemoveButton()
        {
            return RemoveButton("Remove($(this.parentNode.parentNode).data())");
        }

        public static string RemoveButton(string click)
        {
            Button boton = new Button();
            boton.circle = true;
            boton.click = click;
            boton.faicon = "fa-trash-o";
            boton.color = "default";
            boton.tooltip = "Remover";

            return boton.ToString();
        }

        public static string CircleButton(string click, string faicon, string color, string tooltip)
        {
            Button boton = new Button();
            boton.circle = true;
            boton.click = click + "($(this.parentNode.parentNode).data())";
            boton.faicon = faicon;
            boton.color = color;
            boton.tooltip = tooltip;

            return boton.ToString();
        }

        public static string IconButton(string click, string faicon, string tooltip)
        {
            Button boton = new Button();                        
            boton.click = click + "($(this.parentNode.parentNode).data())";
            boton.faicon = faicon;            
            boton.tooltip = tooltip;            
            return boton.ToString();
        }

        public static string SmallButton(string click, string faicon, string texto ,string color, string tooltip)
        {
            Button boton = new Button();            
            boton.click = click + "($(this.parentNode.parentNode).data())";
            boton.faicon = faicon;
            boton.color = color;
            boton.tooltip = tooltip;
            boton.size = "xs";
            boton.texto = texto;            
            return boton.ToString();
        }


        public static string DropdownButton(string click, string texto, string  clase, string[] opciones)
        {
            Button boton = new Button();
            boton.click = click + "(this)";
            boton.clase = clase;
            boton.faicon = "fa-angle-down";
            boton.list = true;
            boton.texto = texto;
            boton.opciones = opciones;
            return new Div("btn-group", boton).ToString();


        }


        public static string HtmlEstado(int? estado)
        {
            return estado.HasValue ? estado.Value == 1 ? "ACTIVO" : "INACTIVO" : "";
        }

        public static string FormTextarea(string id, string valor, string etiqueta, bool obligatorio, bool habilitado, string size, int? cols, int? rows)
        {
            Label label = new Label() { texto = etiqueta };
            Textarea area = new Textarea() { id = id, valor = valor, obligatorio = obligatorio, habilitado = habilitado, cols = cols, rows = rows };
            StringBuilder html = new StringBuilder();

            html.Append(label.ToString());
            html.Append(new Div(size, area).ToString());

            return new Div("form-group", html).ToString();

        }

        public static  string FormInputFile(string id, string valor, string etiqueta, bool obligatorio, bool habilitado, string size)
        {
            Label label = new Label() { texto = etiqueta };
            Div divfile = new Div() { clase = "fileinput fileinput-new", data = "data-provides='fileinput'" };
            Div divinput = new Div() { clase = "input-group input-large"};
            Div divedit = new Div() { clase = "form-control uneditable-input input-fixed input-medium", data = "data-trigger='fileinput'" };
            divedit.AddContent(new Icon("fa-file fileinput-exists"));
            divedit.AddContent("&nbsp;");
            divedit.AddContent(new Span("fileinput-filename"));
            Span spagrp = new Span("input-group-addon btn default btn-file");
            spagrp.AddContent(new Span("fileinput-new", "Seleccionar archivo"));
            spagrp.AddContent(new Span("fileinput-exists", "Cambiar"));
            spagrp.AddContent("<input id='" + id + "' type='file' name='...'>");

            divinput.AddContent(divedit);
            divinput.AddContent(spagrp);
            divinput.AddContent("<a href='javascript:;' class='input-group-addon btn red fileinput-exists' data-dismiss='fileinput'>Quitar</a>");

            divfile.AddContent(divinput);


            StringBuilder html = new StringBuilder();

            html.Append(label.ToString());
            html.Append(divfile.ToString());
            return html.ToString();
        }


        public static  string FormTimePicker(string id, string valor, string etiqueta, string placeholder, bool obligatorio, bool habilitado, string size)
        {
            Label label = new Label() { texto = etiqueta };
            Input input = new Input() { id = id, valor = valor, placeholder = placeholder, obligatorio = obligatorio, habilitado = habilitado, clase = "timepicker timepicker-24" };
            StringBuilder html = new StringBuilder();

            html.Append(label.ToString());
            html.Append(new Div(size, input).ToString());
            return new Div("form-group", html).ToString();
        }


        public static string FormDayTimePicker(string id, DateTime? valor, string etiqueta, string placeholder, bool obligatorio, bool habilitado, string size)
        {
            Label label = new Label() { texto = etiqueta };
                                 
            StringBuilder html = new StringBuilder();
            html.Append(label.ToString());
            html.Append(new Div(size, DayTimePicker(id, valor, placeholder, obligatorio, habilitado)));
            return new Div("form-group", html).ToString();
        }



        public static Div DayTimePicker(string id, DateTime? valor, string placeholder, bool obligatorio, bool habilitado)
        {
            Button boton = new Button();
            //boton.click = "alert(this.parentNode.parentNode.innerHtml);$(this.parentNode.parentNode).val($(this).data(\"opcion\"));";
            boton.click = "Change(this);";
            //boton.clase = clase;
            boton.faicon = "fa-angle-down";
            boton.list = true;
            boton.texto = Formatos.GetDia(valor.HasValue ? valor : DateTime.Now);
            boton.opciones = new string[] { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };

            Input input = TimePicker(id, valor, placeholder, obligatorio, habilitado);

            return new Div("input-group", new Div("input-group-btn", boton), input);

        }

        public static Input TimePicker(string id, DateTime? valor, string placeholder, bool obligatorio, bool habilitado)
        {
            return new Input() { id = id, valor = valor.HasValue ? valor.Value.Hour + ":" + valor.Value.Minute : "", placeholder = placeholder, obligatorio = obligatorio, habilitado = habilitado, clase = "timepicker timepicker-24" };

        }



        public static string LabelInput(string id, string valor, string etiqueta, string placeholder, bool obligatorio, bool habilitado, string size, int? largo, string faicon)
        {
            Label label = new Label() { texto = etiqueta };
            Input input = new Input() { id = id, valor = valor, placeholder = placeholder, obligatorio = obligatorio, habilitado = habilitado, largo = largo };
            StringBuilder html = new StringBuilder();
            html.Append(label.ToString());
            if (!string.IsNullOrEmpty(faicon))
                html.Append(new Div("input-group", new Span("input-group-addon", new Icon(faicon)), input).ToString());
            else
                html.Append(input).ToString();
            return html.ToString();


        }

        public static string LabelSelect(string id, string valor, Dictionary<string, string> dictionary, string etiqueta, bool obligatorio, bool habilitado, string size, bool withempty, bool multiple,string faicon)
        {
            Label label = new Label() { texto = etiqueta };
            Select select = new Select() { id = id, valor = valor, diccionario = dictionary, obligatorio = obligatorio, habilitado = habilitado, withempty = withempty, multiselect=multiple };            
            StringBuilder html = new StringBuilder();
            html.Append(label.ToString());
            if (!string.IsNullOrEmpty(faicon))
                html.Append(new Div("input-group", new Span("input-group-addon", new Icon(faicon)), select).ToString());
            else
                html.Append(select).ToString();
            return html.ToString();
        }

        public static string RangoFechas(string iddesde, string idhasta, DateTime? desde, DateTime? hasta, string etiqueta, bool obligatorio, bool habilitado, string size)
        {
            Label label = new Label() { texto = etiqueta };

            Input inputdesde = new Input() { id = iddesde, datetimevalor = desde, datepicker = true, habilitado = habilitado };
            Input inputhasta= new Input() { id = idhasta, datetimevalor = hasta, datepicker = true, habilitado = habilitado };

            StringBuilder html = new StringBuilder();

            html.Append(label.ToString());
            html.Append(new Div("input-group", new Span("input-group-addon", new Icon("fa-calendar")), inputdesde, new Span("input-group-addon", new Icon("fa-calendar")), inputhasta).ToString());

            return new Div("form-group", html).ToString();

        }


        public static string Portlet(string id, string clase, string icono, string titulo, string[] botones, string[] opciones, params object[] objetos)
        {
            Div caption = new Div("caption");
            if (!string.IsNullOrEmpty(icono))
                caption.AddContent(new Icon(icono));
            caption.AddContent(titulo);

            Div actions = new Div("actions");
            if (botones != null)
            {
                foreach (string item in botones)
                {
                    actions.AddContent("<a href=\"javascript:PortletActions('" + item + "','" + id + "');\" data-opcion='" + item + "' class=\"btn btn-circle btn-default\">" + item + " </a>");
                }
            }
            actions.AddContent("<a class=\"btn btn-circle btn-icon-only btn-default fullscreen\" href=\"javascript:;\"></a>");
            Div porlettitle = new Div("portlet-title", caption, actions);
            Div porletbody = new Div("portlet-body", objetos);
            Div porlet = new Div("portlet box " + clase, porlettitle, porletbody);
            porlet.id = id;

            return porlet.ToString();

        }


        public static string GetCalendar(int year, int month)
        {
            StringBuilder html = new StringBuilder();

            DateTime inicio = new DateTime(year, month, 1);
            DateTime fin = inicio.AddMonths(1).AddSeconds(-1);

            string name = inicio.ToString("MMMM") + " " + inicio.Year.ToString();


            html.Append("<table class='calendario' data-year='"+year+"' data-month='"+month+"' data-name='"+name+"'>");
            html.Append("<thead>");
            html.Append("<tr>");
            html.Append("<th>DOM</th>");
            html.Append("<th>LUN</th>");
            html.Append("<th>MAR</th>");
            html.Append("<th>MIE</th>");
            html.Append("<th>JUE</th>");
            html.Append("<th>VIE</th>");
            html.Append("<th>SAB</th>");
            html.Append("</tr>");
            html.Append("</thead>");
            html.Append("<tbody>");


            if (inicio.DayOfWeek == DayOfWeek.Monday)
                html.AppendFormat("<tr><td></td>");
            if (inicio.DayOfWeek == DayOfWeek.Tuesday)
                html.Append("<tr><td></td><td></td>");
            if (inicio.DayOfWeek == DayOfWeek.Wednesday)
                html.Append("<tr><td></td><td></td><td></td>");
            if (inicio.DayOfWeek == DayOfWeek.Thursday)
                html.Append("<tr><td></td><td></td><td></td><td></td>");
            if (inicio.DayOfWeek == DayOfWeek.Friday)
                html.Append("<tr><td></td><td></td><td></td><td></td><td></td>");
            if (inicio.DayOfWeek == DayOfWeek.Saturday)
            {
                html.Append("<tr><td></td><td></td><td></td><td></td><td></td><td></td>");                
            }






            do
            {
                if (inicio.DayOfWeek == DayOfWeek.Sunday)
                {
                    html.Append("<tr>");
                }
                html.AppendFormat("<td class='activeday' data-date='{1}'><div class='numday {2}'>{0}</div><div class='workday'></div>", inicio.Day, inicio, inicio == DateTime.Now.Date ? "today" : "");
                if (inicio.DayOfWeek == DayOfWeek.Saturday)
                {
                    html.Append("</tr>");
                }
                inicio =  inicio.AddDays(1);
                
            } while (inicio < fin);


            if (fin.DayOfWeek == DayOfWeek.Sunday)
                html.Append("<td></td><td></td><td></td><td></td><td></td><td></td></tr>");
            if (fin.DayOfWeek == DayOfWeek.Monday)
                html.Append("<td></td><td></td><td></td><td></td><td></td></tr>");
            
            if (fin.DayOfWeek == DayOfWeek.Tuesday)
                html.Append("<td></td><td></td><td></td><td></td></tr>");
            
            if (fin.DayOfWeek == DayOfWeek.Wednesday)
                html.Append("<td></td><td></td><td></td></tr>");
            
            if (fin.DayOfWeek == DayOfWeek.Thursday)
                html.Append("<td></td><td></td></tr>");
            if (fin.DayOfWeek == DayOfWeek.Friday)
                html.Append("<td></td></tr>");
            if (fin.DayOfWeek == DayOfWeek.Saturday)
                html.Append("</tr>");
            
            html.Append("</tbody>");
            html.Append("</table>");
            return html.ToString();
        }

    }
}
