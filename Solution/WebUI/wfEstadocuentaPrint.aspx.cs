using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Reflection;
using System.Data;
using BusinessObjects;
using BusinessLogicLayer;
using Services;
using PrintReportSample;

namespace WebUI
{
    public partial class wfEstadocuentaPrint : System.Web.UI.Page
    {
        LocalReport rep = new LocalReport();
        private static ReportPrintDocument rp;
        protected static WhereParams parametros = new WhereParams();
        protected static string OrderByClause = "ddo_doctran,ddo_pago,ddo_fecha_ven,com_fecha";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtcodpersona.Text = (Request.QueryString["codpersona"] != null) ? Request.QueryString["codpersona"].ToString() : "-1";
                txtdebcre.Text = (Request.QueryString["debcre"] != null) ? Request.QueryString["debcre"].ToString() : "-1";
                txtfechacorte.Text = (Request.QueryString["fechacort"] != null) ? Request.QueryString["fechacort"].ToString() : "-1";
                txtcodigoalm.Text = (Request.QueryString["codalmacen"] != null) ? Request.QueryString["codalmacen"].ToString() : "-1";
                GenReport(txtcodpersona.Text, txtdebcre.Text, txtcodigoalm.Text, txtfechacorte.Text);
            }
        }

        public static void SetWhereClause(vDEstadoCuenta obj)
        {
            if (obj != null)
            {
                int contador = 0;
                parametros = new WhereParams();
                List<object> valores = new List<object>();
                if (obj.ddo_codclipro > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " ddo_codclipro = {" + contador + "} ";
                    valores.Add(obj.ddo_codclipro);
                    contador++;
                }
                if (obj.com_almacen > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_almacen = {" + contador + "} ";
                    valores.Add(obj.com_almacen);
                    contador++;
                }
                if (obj.ddo_fecha_emi.HasValue)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " ddo_fecha_emi <= {" + contador + "} ";
                    valores.Add(obj.ddo_fecha_emi);
                    contador++;
                }
                if (obj.ddo_debcre.Value == Constantes.cCredito)
                {
                 //   parametros.where += ((parametros.where != "") ? " and " : "") + " com_tclipro = {" + contador + "} ";
                  //  valores.Add(5);
                   // contador++;
                    parametros.where += ((parametros.where != "") ? " and " : "") + "  com_tipodoc <> 4 and com_tipodoc <>6 and com_tipodoc <> 13 and com_tipodoc<> 17 and com_tipodoc <> 18 ";

                }

                if (obj.ddo_debcre.Value == Constantes.cDebito)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_tclipro = {" + contador + "} ";
                    valores.Add(4);
                    contador++;
                }



                parametros.valores = valores.ToArray();
            }
        }
        private void GenReport(String codigo, String debcre, String codalmacen, String fechacort)
        {
            vDEstadoCuenta estadocuenta = new vDEstadoCuenta();
            estadocuenta.ddo_codclipro = Int32.Parse(codigo);
            estadocuenta.ddo_debcre = Int32.Parse(debcre);
            estadocuenta.com_almacen =Convert.ToInt32(codalmacen);
            estadocuenta.ddo_fecha_emi = DateTime.Parse(fechacort);
            SetWhereClause(estadocuenta);
            List<vDEstadoCuenta> planillas = vDEstadoCuentaBLL.GetAll(parametros, OrderByClause);

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.Reset();
            ReportViewer1.Visible = true;
            rep = this.ReportViewer1.LocalReport;
            rep.ReportPath = "reports/EstadoCuenta.rdlc";
            rep.DataSources.Add(new ReportDataSource("DataSet1", planillas));
            ReportViewer1.LocalReport.Refresh();
            //rp = new ReportPrintDocument(rep);
        }
        
    }
}