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
    public partial class wfDnotacrePrint : System.Web.UI.Page
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
            
            Comprobante fac = new Comprobante();
            fac.com_empresa_key = 1;
            fac.com_codigo_key = Int64.Parse(codigo);
            fac.com_empresa = 1;
            fac.com_codigo = Int64.Parse(codigo);
            fac = ComprobanteBLL.GetByPK(fac);
            fac.total = new Total();

            fac.total.tot_comprobante = fac.com_codigo;
            fac.total.tot_empresa = fac.com_empresa;
            fac.total.tot_comprobante_key = fac.com_codigo;
            fac.total.tot_empresa_key = fac.com_empresa;
            fac.total = TotalBLL.GetByPK(fac.total);

            fac.notascre = DnotacreBLL.GetAll(new WhereParams("dnc_comprobante = {0} and dnc_empresa = {1}", fac.com_codigo, fac.com_empresa), "");

            List<vDnotacre> planillas = vDnotacreBLL.GetAll(new WhereParams(" comprobante.com_codigo={0}", fac.com_codigo), "");



//            DataTable dt = fac.ToDataTableHR();

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

            rep.ReportPath = "reports/Notacre.rdlc";
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