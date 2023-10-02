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
    public partial class wfDcontablePrint : System.Web.UI.Page
    {
        private static ReportPrintDocument rp;
        protected string empresa;
        LocalReport rep = new LocalReport();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtcodigocomp.Text = (Request.QueryString["codigocomp"] != null) ? Request.QueryString["codigocomp"].ToString() : "-1";
                empresa = Request.QueryString["empresa"];
                GenReport(txtcodigocomp.Text,empresa);
            }
        }


        private void GenReport(String codigo,String empresa)
        {
            Empresa emp = EmpresaBLL.GetByPK(new Empresa { emp_codigo = int.Parse(empresa), emp_codigo_key = int.Parse(empresa) });
            Comprobante planilla = new Comprobante();

            planilla.com_empresa_key = int.Parse(empresa);
            planilla.com_codigo_key = Int64.Parse(codigo);
            planilla.com_empresa = int.Parse(empresa);
            planilla.com_codigo = Int64.Parse(codigo);
            planilla = ComprobanteBLL.GetByPK(planilla);
         //   List<vDcancelacion> planillas = vDcancelacionBLL.GetAllBlock(new WhereParams("dca_planilla={0}", planilla.com_codigo), "");
            List<vDcontable> planillas = vDcontableBLL.GetAll(new WhereParams("dco_empresa={0} and dco_comprobante={1}", planilla.com_empresa, planilla.com_codigo), "");

        



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

            rep.ReportPath = "reports/Dcontable.rdlc";
            rep.DataSources.Add(new ReportDataSource("DataSet1", planillas));


            string usuario = "";
            if (!string.IsNullOrEmpty(planilla.crea_usrnombres))
                usuario = planilla.crea_usrnombres.ToUpper();


            rep.SetParameters(new ReportParameter("usuario", usuario));
            rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

            //rep.DataSources.Add( new ReportDataSource("Cabecera",cabs ));
            //rep.DataSources.Add(new ReportDataSource("Detalle",lista));
            //rep.Refresh(); 
            //ReportViewer1.LocalReport.Refresh();
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            // byte[] bytes = viewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.          
            // System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("Content-Disposition", "inline;filename= filename" + "." + extension);
            //Response.AddHeader("content-disposition", "attachment; filename= filename" + "." + extension);
            Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
            Response.Flush(); // send it to the client to download  
            Response.End();
            //rp = new ReportPrintDocument(rep);
        }
        protected void print(object sender, EventArgs e)
        {
            // ReportPrintDocument rp = new ReportPrintDocument(rep);
            rp.Print();
        }
    }
}