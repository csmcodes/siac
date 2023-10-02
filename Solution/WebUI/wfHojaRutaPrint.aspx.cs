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
    public partial class wfHojaRutaPrint : System.Web.UI.Page
    {
        LocalReport rep = new LocalReport();
        private static ReportPrintDocument rp;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtcodigocomp.Text = (Request.QueryString["codigocomp"] != null) ? Request.QueryString["codigocomp"].ToString() : "-1";
                string detalle = Request.QueryString["detalle"];
                if (detalle == "1" || detalle == "2")
                    GenReportComprobantes(txtcodigocomp.Text, detalle);
                else
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
            
            rep.ReportPath = "reports/Report1.rdlc";//TRANSORTIZ
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


        private void GenReportComprobantes(string codigo, string tipo)
        {

            string empresa = Request.QueryString["empresa"];
            string usuario = Request.QueryString["usuario"];

            Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = int.Parse(empresa), uxe_empresa_key = int.Parse(empresa), uxe_usuario = usuario, uxe_usuario_key = usuario });

            Impresion imp = new Impresion();

            imp.imp_empresa = int.Parse(empresa);
            imp.imp_empresa_key = int.Parse(empresa);
            imp.imp_almacen = uxe.uxe_almacen.Value;
            imp.imp_almacen_key = uxe.uxe_almacen.Value;
            imp.imp_pventa = uxe.uxe_puntoventa.Value;
            imp.imp_pventa_key = uxe.uxe_puntoventa.Value;
            imp.imp_tipodoc = 5;
            imp.imp_tipodoc_key = 5;

            imp = ImpresionBLL.GetByPK(imp);

            string impresora = imp.imp_impresora;



            List<Constante> lst = ConstanteBLL.GetAll("con_usuario='" + usuario + "' and con_nombre='TICKETDIM'", "");
            string dimensiones = "";
            if (lst.Count > 0)
                dimensiones = lst[0].con_valor;


            //string  impresora =  Services.Constantes.GetPrinter(usuario, int.Parse(empresa), uxe.uxe_almacen, uxe.uxe_puntoventa, int.Parse(tipodoc));
            string path = Server.MapPath("pdf");


            string filename = Services.Pdf.ComprobantesHojaRutaPDF(int.Parse(empresa), codigo, path, true, impresora, tipo, dimensiones);
            string newFile = path + @"\" + filename;
            ShowPdf(newFile);
            

        }

        public void ShowPdf(string filename)
        {
            //Services.PdfPrinter.PrintPDFs(filename);

            //Clears all content output from Buffer Stream
            Response.ClearContent();
            //Clears all headers from Buffer Stream
            Response.ClearHeaders();
            //Adds an HTTP header to the output stream
            Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
            //Gets or Sets the HTTP MIME type of the output stream
            Response.ContentType = "application/pdf";
            //Writes the content of the specified file directory to an HTTP response output stream as a file block
            Response.WriteFile(filename);
            //sends all currently buffered output to the client
            Response.Flush();
            //Clears all content output from Buffer Stream
            Response.Clear();
        }

    }
}