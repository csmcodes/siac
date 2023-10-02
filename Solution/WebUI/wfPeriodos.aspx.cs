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
using System.Globalization;

namespace WebUI
{
    public partial class wfPeriodos : System.Web.UI.Page
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
            tdatos.CreteEmptyTable(2, 2);
            tdatos.rows[0].cells[0].valor = "PERIODO";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtPERIODO", placeholder = "Periodo", clase = Css.small, valor = DateTime.Now.Year }.ToString();
            tdatos.rows[1].cells[0].valor = "";
            tdatos.rows[1].cells[1].valor = new Boton { click = "Actualiza();return false;", valor = "Actualizar" }.ToString();

            //tdatos.rows[4].cells[0].valor = "";
            //tdatos.rows[4].cells[1].valor = "";
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }



        [WebMethod]
        public static string GetPeriodos(object objeto)
        {

            StringBuilder html = new StringBuilder();

            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object empresa = null;
                object periodo = null;
                object user = null;
                object audit = null;
                tmp.TryGetValue("empresa", out empresa);
                tmp.TryGetValue("periodo", out periodo);
                tmp.TryGetValue("usuario", out user);
                tmp.TryGetValue("audit", out audit);

                if (periodo == null)
                    periodo = DateTime.Now.Year.ToString();

                bool auditoria = false;
                if (audit != null)
                {
                    if (audit.ToString() == "1")
                        auditoria = true;
                }
                List<PeriodoContable> lst = Packages.Contabilidad.GetPeriodosContables(int.Parse(periodo.ToString()), DateTime.Now.Month, user.ToString());


                html.AppendLine("<div class=\"span6\">");

                HtmlTable tdatos = new HtmlTable();

                tdatos.id = "tperiodo";
                //tdatos.invoice = true;

                tdatos.AddColumn("Mes", "", "", "");
                tdatos.AddColumn("Estado", "", "", "");
                tdatos.AddColumn("", "", "", "");
                if (auditoria)
                    tdatos.AddColumn("Auditoria", "", "", "");

                List<PeriodoContable> lstperiodo = lst.FindAll(delegate (PeriodoContable pc) { return pc.periodo == int.Parse(periodo.ToString()); });

                foreach (PeriodoContable item in lstperiodo)
                {
                    HtmlRow row = new HtmlRow();

                    string fullMonthName = new DateTime(2015, item.mes, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));

                    row.cells.Add(new HtmlCell { valor = fullMonthName });
                    row.cells.Add(new HtmlCell { valor = item.estado });
                    row.cells.Add(new HtmlCell { valor = "<a href='javascript:OpenClose(" + item.mes + ",\"" + item.estado + "\")' >" + (item.estado == "open" ? "Cerrar" : "Abrir") + "</a> " });
                    if (auditoria)
                        row.cells.Add(new HtmlCell { valor = item.audit });
                    tdatos.AddRow(row);
                }
                html.Append(tdatos.ToString());
                html.AppendLine(" </div><!--span6-->");

                //return General.GetNextNumeroComprobante(int.Parse(empresa.ToString()), int.Parse(periodo.ToString()), int.Parse(ctipocom.ToString()), int.Parse(almacen.ToString()), int.Parse(pventa.ToString()));

            }

            return html.ToString();
        }

        [WebMethod]
        public static string OpenClosePeriodo(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object empresa = null;
                object periodo = null;
                object mes = null;
                object estado = null;
                object usuario = null;
                tmp.TryGetValue("empresa", out empresa);
                tmp.TryGetValue("periodo", out periodo);
                tmp.TryGetValue("mes", out mes);
                tmp.TryGetValue("estado", out estado);
                tmp.TryGetValue("usuario", out usuario);

                if (estado.ToString() == "close")
                    Packages.Contabilidad.OpenPeriodo(int.Parse(periodo.ToString()), int.Parse(mes.ToString()), usuario.ToString());
                else
                    Packages.Contabilidad.ClosePeriodo(int.Parse(periodo.ToString()), int.Parse(mes.ToString()), usuario.ToString());


            }
            return "";
        }
    }
}