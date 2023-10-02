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
    public partial class wfReporteVentasSocioTipo : System.Web.UI.Page
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
            tdatos.CreteEmptyTable(8, 2);
            tdatos.rows[0].cells[0].valor = "Almacen";
            tdatos.rows[0].cells[1].valor = new Select { id = "cmbALMACEN", diccionario = Dictionaries.GetIDAlmacen(), clase = Css.large, withempty = true }.ToString();
            tdatos.rows[1].cells[0].valor = "Punto Venta";
            tdatos.rows[1].cells[1].valor = new Select { id = "cmbPVENTA", diccionario = new Dictionary<string, string>(), clase = Css.large, withempty = true }.ToString();
            tdatos.rows[2].cells[0].valor = "Periodo";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtDESDE", placeholder = "PERIODO", clase = Css.small }.ToString();
            tdatos.rows[3].cells[0].valor = "Mes:";
            tdatos.rows[3].cells[1].valor = new Input { id = "txtHASTA", placeholder = "MES", clase = Css.small }.ToString();
            tdatos.rows[4].cells[0].valor = "Socio:";
            tdatos.rows[4].cells[1].valor = new Select { id = "cmbUSUARIO", clase = Css.large, diccionario = Dictionaries.GetSocios(), withempty = true }.ToString();
            tdatos.rows[5].cells[0].valor = ":";
            tdatos.rows[5].cells[1].valor = new Boton { click = "LoadReporte();return false;", valor = "Reporte Detallado" }.ToString();
            tdatos.rows[6].cells[0].valor = ":";
            tdatos.rows[6].cells[1].valor = new Boton { click = "LoadReporteTotales();return false;", valor = "Reporte Total" }.ToString();
            tdatos.rows[7].cells[0].valor = ":";
            tdatos.rows[7].cells[1].valor = new Boton { click = "LoadReporteTotalesCho();return false;", valor = "Reporte Choferes Total" }.ToString();

            //tdatos.rows[4].cells[0].valor = "";
            //tdatos.rows[4].cells[1].valor = "";
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }

        [WebMethod]
        public static string GetPuntoVenta(object objeto)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object id = null;
            object empresa = null;
            object almacen = null;
            object usuario = null;
            tmp.TryGetValue("id", out id);
            tmp.TryGetValue("empresa", out empresa);
            tmp.TryGetValue("almacen", out almacen);
            tmp.TryGetValue("usuario", out usuario);
            if (!string.IsNullOrEmpty((string)almacen))
            {
                Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = int.Parse(empresa.ToString()), uxe_empresa_key = int.Parse(empresa.ToString()), uxe_usuario = usuario.ToString(), uxe_usuario_key = usuario.ToString() });

                //return new Select { id = "cmbPVENTA_P", diccionario = Dictionaries.GetPuntoVenta(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())),clase = Css.large, valor= uxe.uxe_puntoventa }.ToString();
                return new Select { id = id.ToString(), diccionario = Dictionaries.GetPuntoVenta(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())), clase = Css.large, valor = uxe.uxe_puntoventa }.ToString();
            }
            return new Select { id = id.ToString(), diccionario = Dictionaries.Empty(), clase = Css.large }.ToString();

        }
    }
}