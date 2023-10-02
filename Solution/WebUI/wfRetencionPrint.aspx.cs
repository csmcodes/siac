using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Data;
using BusinessObjects;
using BusinessLogicLayer;
using Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;
using System.Collections;
using System.Threading;
using System.Diagnostics;
//using Neodynamic.SDK.Web;

namespace WebUI
{
    public partial class wfRetencionPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string codigo = (Request.QueryString["codigo"] != null) ? Request.QueryString["codigo"].ToString() : "-1";
                GenReport(codigo);
            }
        }


        private void GenReport(string codigo)
        {

            string usuario = Request.QueryString["usuario"];
            string empresa = Request.QueryString["empresa"];
            string tipodoc = Request.QueryString["tipodoc"];

            Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = int.Parse(empresa), uxe_empresa_key = int.Parse(empresa), uxe_usuario = usuario, uxe_usuario_key = usuario });

            Impresion imp = new Impresion();

            imp.imp_empresa = int.Parse(empresa);
            imp.imp_empresa_key = int.Parse(empresa);
            imp.imp_almacen = uxe.uxe_almacen.Value;
            imp.imp_almacen_key = uxe.uxe_almacen.Value;
            imp.imp_pventa = uxe.uxe_puntoventa.Value;
            imp.imp_pventa_key = uxe.uxe_puntoventa.Value;
            imp.imp_tipodoc = int.Parse(tipodoc);
            imp.imp_tipodoc_key = int.Parse(tipodoc);

            imp = ImpresionBLL.GetByPK(imp);

            string impresora = imp.imp_impresora;

            //string  impresora =  Services.Constantes.GetPrinter(usuario, int.Parse(empresa), uxe.uxe_almacen, uxe.uxe_puntoventa, int.Parse(tipodoc));
            string path = Server.MapPath("pdf");

            string filename = Services.Pdf.RetencionPDF(int.Parse(empresa), codigo, path, true, impresora);

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