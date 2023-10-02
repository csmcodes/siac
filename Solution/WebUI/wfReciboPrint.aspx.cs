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
    public partial class wfReciboPrint : System.Web.UI.Page
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
           
            planilla.com_empresa_key = 1;
            planilla.com_codigo_key = Int64.Parse(codigo);
            planilla.com_empresa = 1;
            planilla.com_codigo = Int64.Parse(codigo);
            planilla = ComprobanteBLL.GetByPK(planilla);
         //   List<vDcancelacion> planillas = vDcancelacionBLL.GetAllBlock(new WhereParams("dca_planilla={0}", planilla.com_codigo), "");
            List<vRecibo> planillas = vReciboBLL.GetAll(new WhereParams("dca_empresa={0} and dca_comprobante_can={1}", planilla.com_empresa, planilla.com_codigo), "");
            

         



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

            rep.ReportPath = "reports/Recibo.rdlc";
            rep.DataSources.Add(new ReportDataSource("DataSet1", planillas));
            //rep.DataSources.Add( new ReportDataSource("Cabecera",cabs ));
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