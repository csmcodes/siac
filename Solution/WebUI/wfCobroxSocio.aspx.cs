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
    public partial class wfCobroxSocio : System.Web.UI.Page
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
            tdatos.rows[0].cells[0].valor = "Socio:";
            tdatos.rows[0].cells[1].valor = new Select { id = "cmbUSUARIO", clase = Css.large, diccionario = Dictionaries.GetSocios(), withempty = true }.ToString();
            tdatos.rows[1].cells[0].valor = "Desde";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtDESDE", placeholder = "DESDE", clase = Css.small, datepicker = true }.ToString();
            tdatos.rows[2].cells[0].valor = "Hasta:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtHASTA", placeholder = "HASTA", clase = Css.small, datepicker = true }.ToString();
            tdatos.rows[3].cells[0].valor = ":";
            tdatos.rows[3].cells[1].valor = new Boton { click = "LoadReporte();return false;", valor = "Generar" }.ToString();
            
            //tdatos.rows[4].cells[0].valor = "";
            //tdatos.rows[4].cells[1].valor = "";
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");           
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }
        

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    DateTime desde = DateTime.Parse(txtdesde.Text);
        //    DateTime hasta = DateTime.Parse(txthasta.Text);
        //    Response.Redirect("./reports/wfReportPrint.aspx?report=CXS&parameter1="+desde.ToShortDateString()+"&parameter2="+hasta.ToShortDateString());
        //}
    }
}