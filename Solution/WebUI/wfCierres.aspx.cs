using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using BusinessLogicLayer;
using System.Web.Services;
using System.Text;
using System.Data;
using Services;
using System.Web.Script.Serialization;
using System.Collections;
using HtmlObjects;
using Functions;
namespace WebUI
{
    public partial class wfCierres : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod]
        public static string GetFiltros(object objeto)
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(4, 2);
            tdatos.rows[0].cells[0].valor = "Periodo";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtPERIODO", placeholder = "Periodo", clase = Css.small }.ToString();
            tdatos.rows[1].cells[0].valor = "Cuenta Cierre:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtCUENTA", autocomplete = "GetCuentaObj", obligatorio = true, clase = Css.xlarge, placeholder = "Cuenta Cierre" }.ToString() + " " + new Input { id = "txtCODCUENTA", visible = false}.ToString();
            tdatos.rows[2].cells[0].valor = "Fecha Cierre:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtFECHA", placeholder = "Hasta", clase = Css.small, datepicker = true, datetimevalor= DateTime.Now }.ToString();
            tdatos.rows[3].cells[0].valor = ":";
            tdatos.rows[3].cells[1].valor = new Boton { click = "ConfirmaCerrar();return false;", valor = "Cerrar" }.ToString();

            //tdatos.rows[4].cells[0].valor = "";
            //tdatos.rows[4].cells[1].valor = "";
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }
    }
}