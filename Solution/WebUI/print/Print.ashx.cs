using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
using Neodynamic.SDK.Web;

namespace WebUI.print
{
    /// <summary>
    /// Descripción breve de Print
    /// </summary>
    public class Print : IHttpHandler
    {

        /*
        private string ComprobantePDF(string codigo, string path)
        {

            Comprobante fac = new Comprobante();
            fac.com_empresa_key = 1;
            fac.com_codigo_key = Int64.Parse(codigo);
            fac.com_empresa = 1;
            fac.com_codigo = Int64.Parse(codigo);
            fac = ComprobanteBLL.GetByPK(fac);

            fac.ccomdoc = new Ccomdoc();
            fac.ccomenv = new Ccomenv();
            fac.total = new Total();

            fac.ccomdoc.cdoc_comprobante = fac.com_codigo;
            fac.ccomdoc.cdoc_empresa = fac.com_empresa;
            fac.ccomdoc.cdoc_comprobante_key = fac.com_codigo;
            fac.ccomdoc.cdoc_empresa_key = fac.com_empresa;
            fac.ccomdoc = CcomdocBLL.GetByPK(fac.ccomdoc);

            fac.ccomenv.cenv_comprobante = fac.com_codigo;
            fac.ccomenv.cenv_empresa = fac.com_empresa;
            fac.ccomenv.cenv_comprobante_key = fac.com_codigo;
            fac.ccomenv.cenv_empresa_key = fac.com_empresa;
            fac.ccomenv = CcomenvBLL.GetByPK(fac.ccomenv);

            fac.total.tot_comprobante = fac.com_codigo;
            fac.total.tot_empresa = fac.com_empresa;
            fac.total.tot_comprobante_key = fac.com_codigo;
            fac.total.tot_empresa_key = fac.com_empresa;
            fac.total = TotalBLL.GetByPK(fac.total);

            fac.rutafactura = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobanteruta = {0} and rfac_empresa = {1}", fac.com_codigo, fac.com_empresa), "");
            fac.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_comprobante = {0} and ddoc_empresa = {1}", fac.com_codigo, fac.com_empresa), "");

            fac.rutafactura = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobantefac = {0} and rfac_empresa = {1}", fac.com_codigo, fac.com_empresa), "");

            string hojaruta ="";
            if (fac.rutafactura.Count>0)
            {
                Comprobante hr = new Comprobante();
                hr.com_empresa = fac.com_empresa;
                hr.com_empresa_key = fac.com_empresa;
                hr.com_codigo = fac.rutafactura[0].rfac_comprobanteruta;
                hr.com_codigo_key = fac.rutafactura[0].rfac_comprobanteruta;
                hr = ComprobanteBLL.GetByPK(hr); 
                hojaruta = hr.com_doctran;
            }

            

            

            
            string pdfTemplate = path + @"\FAC.pdf";
            string newFile = path + @"\" + fac.com_doctran + ".pdf";


            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;

            //var writer = pdfStamper.Writer;
            //PdfAction js = PdfAction.JavaScript(GetAutoPrintJs(imp.imp_impresora), writer);
            //writer.AddJavaScript(js);          //IMPRESION AUTOMATICA

            // set form pdfFormFields
            // The first worksheet and W-4 form
            pdfFormFields.SetField("numero", fac.com_doctran);

            DateTime ahora = DateTime.Now;
            pdfFormFields.SetField("fechacrea", string.Format("{0:00}/{1:00}/{2:0000} {3:00}:{4:00}:{5:00}", ahora.Day, ahora.Month, ahora.Year, ahora.Hour, ahora.Minute, ahora.Second));
            pdfFormFields.SetField("usuario", fac.crea_usr);

            pdfFormFields.SetField("fecha", fac.com_fecha.ToLongDateString());
            pdfFormFields.SetField("cliente", fac.ccomdoc.cdoc_nombre);
            pdfFormFields.SetField("ruc", fac.ccomdoc.cdoc_ced_ruc);
            pdfFormFields.SetField("direccion", fac.ccomdoc.cdoc_direccion);
            pdfFormFields.SetField("telefono", fac.ccomdoc.cdoc_telefono);

            pdfFormFields.SetField("destinatario", fac.ccomenv.cenv_apellidos_des + " " + fac.ccomenv.cenv_nombres_des);
            pdfFormFields.SetField("direcciondes", fac.ccomenv.cenv_direccion_des);
            pdfFormFields.SetField("ciudaddes", fac.ccomenv.cenv_rutadestino);
            pdfFormFields.SetField("remitente", fac.ccomenv.cenv_apellidos_rem + " " + fac.ccomenv.cenv_nombres_rem);
            pdfFormFields.SetField("propietario", fac.ccomenv.cenv_nombres_soc);
            pdfFormFields.SetField("conductor", fac.ccomenv.cenv_nombres_cho);

            decimal totalcantidad = 0;
            foreach (Dcomdoc item in fac.ccomdoc.detalle)
            {
                totalcantidad += item.ddoc_cantidad;
                pdfFormFields.SetField("cantidad", pdfFormFields.GetField("cantidad") + item.ddoc_cantidad.ToString("0") + Environment.NewLine);
                string observaciones = item.ddoc_productonombre + " " + item.ddoc_observaciones;
                int largo = 0;
                do
                {
                    string objsimp = observaciones;
                    largo = objsimp.Length;
                    if (largo > 200)
                    {
                        objsimp = observaciones.Substring(0, 200);
                        observaciones = observaciones.Replace(objsimp, "");
                    }
                    //pdfFormFields.SetField("descripcion", pdfFormFields.GetField("descripcion") + item.ddoc_productonombre + " " + item.ddoc_observaciones + Environment.NewLine);
                    pdfFormFields.SetField("descripcion", pdfFormFields.GetField("descripcion") + objsimp + Environment.NewLine);
                }
                while (largo > 200);

                List<Dcalculoprecio> lstdc = DcalculoprecioBLL.GetAll(new WhereParams("dcpr_empresa={0} and dcpr_comprobante={1} and dcpr_dcomdoc={2}", item.ddoc_empresa, item.ddoc_comprobante, item.ddoc_secuencia), "");

                string peso = "";
                string preciou = item.ddoc_precio.ToString("0.00");
                foreach (Dcalculoprecio dc in lstdc)
                {
                    peso += dc.dcpr_indicedigitado + Environment.NewLine;
                    if (dc.dcpr_valor.HasValue)
                        preciou = dc.dcpr_valor.Value.ToString("0.00") + Environment.NewLine;
                }


                pdfFormFields.SetField("peso", pdfFormFields.GetField("peso") + peso + Environment.NewLine);
                //pdfFormFields.SetField("valorunitario", pdfFormFields.GetField("valorunitario") + item.ddoc_precio.ToString("0.00") + Environment.NewLine);
                pdfFormFields.SetField("valorunitario", pdfFormFields.GetField("valorunitario") + preciou + Environment.NewLine);
                pdfFormFields.SetField("valortotal", pdfFormFields.GetField("valortotal") + item.ddoc_total.ToString("0.00") + Environment.NewLine);
            }

            pdfFormFields.SetField("totalcantidad", totalcantidad.ToString("0"));
            pdfFormFields.SetField("valordeclarado", (fac.total.tot_vseguro.HasValue) ? fac.total.tot_vseguro.Value.ToString("0.00") : "");
            pdfFormFields.SetField("valorseguro", (fac.total.tot_tseguro.HasValue) ? fac.total.tot_tseguro.Value.ToString("0.00") : "");
            pdfFormFields.SetField("guia", fac.ccomenv.cenv_guia1 + "-" + fac.ccomenv.cenv_guia2 + "-" + fac.ccomenv.cenv_guia3);
            pdfFormFields.SetField("politica", fac.ccomdoc.cdoc_politicanombre);
            pdfFormFields.SetField("entrega", fac.ccomenv.cenv_observacion);
            pdfFormFields.SetField("hojaruta", hojaruta);


            decimal subtotal = fac.total.tot_subtot_0 + fac.total.tot_subtotal;
            pdfFormFields.SetField("subtotal0", fac.total.tot_subtot_0.ToString("0.00"));
            pdfFormFields.SetField("subtotal12", fac.total.tot_subtotal.ToString("0.00"));
            pdfFormFields.SetField("subtotal", subtotal.ToString("0.00"));
            pdfFormFields.SetField("iva", fac.total.tot_timpuesto.ToString("0.00"));
            pdfFormFields.SetField("total", fac.total.tot_total.ToString("0.00"));


            // report by reading values from completed PDF
            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();
            return fac.com_doctran + ".pdf";
            

        }
        */

        public void ProcessRequest(HttpContext context)
        {
            if (WebClientPrint.ProcessPrintJob(context.Request))
            {


                //bool useDefaultPrinter = (context.Request["useDefaultPrinter"] == "checked");
                string printerName = context.Server.UrlDecode(context.Request["printerName"]);
                string filename = context.Server.UrlDecode(context.Request["fileName"]);
                string path = context.Server.UrlDecode(context.Request["path"]);


                PrintFile file = new PrintFile(path+ @"\" +filename, filename);
                ClientPrintJob cpj = new ClientPrintJob();
                cpj.PrintFile = file;
                if (string.IsNullOrEmpty(printerName))
                    cpj.ClientPrinter = new DefaultPrinter();
                else
                    cpj.ClientPrinter = new InstalledPrinter(printerName);
                cpj.SendToClient(context.Response);


            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}