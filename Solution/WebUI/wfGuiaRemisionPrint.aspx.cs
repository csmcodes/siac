using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI
{
    public partial class wfGuiaRemisionPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                GenGuiaRem();
            }
        }


        private void GenGuiaRem()
        {
            string codigo = Request.QueryString["codigo"];
            string usuario = Request.QueryString["usuario"];
            string empresa = Request.QueryString["empresa"];
            string tipodoc = Request.QueryString["tipodoc"];
            string formato = Request.QueryString["formato"];


            string impresora = "";

            //string  impresora =  Services.Constantes.GetPrinter(usuario, int.Parse(empresa), uxe.uxe_almacen, uxe.uxe_puntoventa, int.Parse(tipodoc));
            string path = Server.MapPath("pdf");

          

            string filename = Services.Pdf.GuiaRemisionPDF(int.Parse(empresa), codigo, path, true, impresora);

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