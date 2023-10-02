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
    public partial class wfHojaRutaCTPrint : System.Web.UI.Page
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

            fac.rutafactura = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobanteruta = {0} and rfac_empresa = {1}", fac.com_codigo, fac.com_empresa), "");

            List<vHojadeRuta> planillas = vHojadeRutaBLL.GetAll(new WhereParams(" cabecera.com_codigo={0}", fac.com_codigo), "");

            decimal valorfp = 0;
            foreach (vHojadeRuta item in planillas)
            {
                if (item.idpolitica == "FP")
                    valorfp += item.totaldetalle.Value;
            }



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
            
            rep.ReportPath = "reports/hojarutaCT.rdlc";//COMYTRANS
            rep.DataSources.Add(new ReportDataSource("DataSet1", planillas));
            rep.SetParameters(new ReportParameter("str_fp", valorfp.ToString("0.00")));
            rep.SetParameters(new ReportParameter("observacion", fac.com_concepto));
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