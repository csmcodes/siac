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
    public partial class wfReportePlanillaSocioTot : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod]
        public static string GetFiltros(object objeto)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object tipo = null;

            tmp.TryGetValue("tipo", out tipo);



            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(4, 2);
            //tdatos.rows[0].cells[0].valor = "Almacen:";
            //tdatos.rows[0].cells[1].valor = new Select { id = "cmbALMACEN", clase = Css.large, diccionario = Dictionaries.GetAlmacen(), withempty = true }.ToString();
            //tdatos.rows[1].cells[0].valor = "Punto Venta";
            //tdatos.rows[1].cells[1].valor = new Select { id = "cmbPVENTA", clase = Css.large, diccionario = Dictionaries.Empty(), withempty = true }.ToString();
            tdatos.rows[0].cells[0].valor = "Desde:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtDESDE", placeholder = "DESDE", clase = Css.small, datepicker = true }.ToString();
            tdatos.rows[1].cells[0].valor = "Hasta:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtHASTA", placeholder = "HASTA", clase = Css.small, datepicker = true }.ToString();
            tdatos.rows[2].cells[0].valor = "Cliente:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtPERSONA", placeholder = "SOCIO", clase = Css.medium }.ToString();
            //tdatos.rows[5].cells[0].valor = "Politica:";
            //tdatos.rows[5].cells[1].valor = new Select { id = "cmbPOLITICA", clase = Css.large, diccionario = Dictionaries.GetPolitica(), withempty = true }.ToString();
            tdatos.rows[3].cells[0].valor = "";
            tdatos.rows[3].cells[1].valor = new Boton { click = "LoadReporte(1);return false;", valor = "Reporte Detallado" }.ToString() + " "  + new Boton { click = "LoadReporte(2);return false;", valor = "Reporte Consolidado" }.ToString();


            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }
    }
}