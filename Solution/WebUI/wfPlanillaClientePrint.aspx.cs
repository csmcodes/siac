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
using PrintReportSample;

namespace WebUI
{
    public partial class wfPlanillaClientePrint : System.Web.UI.Page
    {
        LocalReport rep = new LocalReport();
        private static ReportPrintDocument rp;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtcodigocomp.Text = (Request.QueryString["codigocomp"] != null) ? Request.QueryString["codigocomp"].ToString() : "-1";
                GenReport(txtcodigocomp.Text);
            }
        }


        private void GenReport(String codigo)
        {
            Comprobante planilla = new Comprobante();
            //codigo = "349";
            planilla.com_empresa_key = 1;
            planilla.com_codigo_key = Int64.Parse(codigo);
            planilla.com_empresa = 1;
            planilla.com_codigo = Int64.Parse(codigo);
            planilla = ComprobanteBLL.GetByPK(planilla);

            planilla.total = new Total();
            planilla.total.tot_empresa = planilla.com_empresa;
            planilla.total.tot_empresa_key = planilla.com_empresa;
            planilla.total.tot_comprobante = planilla.com_codigo;
            planilla.total.tot_comprobante_key = planilla.com_codigo;
            planilla.total = TotalBLL.GetByPK(planilla.total);


            List<vPlanillaCliente> planillas = vPlanillaClienteBLL.GetAll(new WhereParams("cabecera.com_codigo={0}", planilla.com_codigo), "");
          

            Empresa emp = EmpresaBLL.GetByPK(new Empresa { emp_codigo = planilla.com_empresa, emp_codigo_key = planilla.com_empresa });



       //     DataTable dt = fac.ToDataTableHR();
            /*Reports report = new Reports();
            report.datasource = dt;
            report.datasourcename = "Factura";
            report.reporte = "Report1.rdlc";
            ReportViewer rv = report.GetReport(); 
            divrep.Controls.Add(rv);
            rv.LocalReport.Refresh();  */
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.Reset();
            ReportViewer1.Visible = true;
            rep = this.ReportViewer1.LocalReport;

            rep.ReportPath = "reports/Planilla cliente.rdlc";
            rep.DataSources.Add(new ReportDataSource("DataSet1", planillas));

            decimal subtotal = planilla.total.tot_subtot_0+planilla.total.tot_transporte+planilla.total.tot_subtotal+planilla.total.tot_tseguro.Value;

            rep.SetParameters(new ReportParameter("str_subtotal", subtotal.ToString("#.##")));
            rep.SetParameters(new ReportParameter("str_iva", planilla.total.tot_timpuesto.ToString("#.##")));
            rep.SetParameters(new ReportParameter("str_total", planilla.total.tot_total.ToString("#.##")));
            rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
            //rep.DataSources.Add(new ReportDataSource("Detalle",lista));
            //rep.Refresh(); 
            ReportViewer1.LocalReport.Refresh();
            //rp = new ReportPrintDocument(rep);
        }
        protected void print(object sender, EventArgs e)
        {
            // ReportPrintDocument rp = new ReportPrintDocument(rep);
            rp.Print();
        }
    }
}