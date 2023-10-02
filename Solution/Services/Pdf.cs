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
using System.Globalization;
using Functions;

namespace Services
{
    public class Pdf
    {

        private static string GetAutoPrintJs(string printername)
        {
            var script = new StringBuilder();
            //script.Append("app.execMenuItem('FullScreen');");
            script.Append("app.fs.isFullScreen = true;");
            script.Append("var pp = this.getPrintParams();");
            //script.Append("pp.interactive= pp.constants.interactionLevel.full;");
            script.Append("pp.interactive = pp.constants.interactionLevel.silent;");
            // Select which printer to print to
            if (!string.IsNullOrEmpty(printername))
                script.Append(@"pp.printerName = '" + printername + "';");
            script.Append("this.print(pp);");
            script.Append("this.closeDoc(true);");
            return script.ToString();
        }

        public static string FacturaTicketPDF(Comprobante fac, string hojaruta, string path, bool autoprint, string impresora, string dimensiones)
        {
            //string pdfTemplate = path + @"\FAC.pdf";
            //string pdfTemplate = path + @"\" + formato.for_pdf;
            string newFile = path + @"\" + fac.com_doctran + ".pdf";
            Empresa emp = EmpresaBLL.GetByPK(new Empresa() { emp_codigo = fac.com_empresa, emp_codigo_key = fac.com_empresa });
            Persona per = PersonaBLL.GetByPK(new Persona { per_empresa = fac.com_empresa, per_empresa_key = fac.com_empresa, per_codigo = fac.com_codclipro.Value, per_codigo_key = fac.com_codclipro.Value });

            string ticketcab = Constantes.GetParameter("ticketcab");
            string ticketfont = Constantes.GetParameter("ticketfont");


            float ancho = 300;
            float alto = 600;
            float mleft = 9;
            float mright = 13;
            float mtop = 2;
            float mbottom = 2;

            if (dimensiones != "")
            {
                string[] arraydimensiones = dimensiones.Split(',');
                ancho = float.Parse(arraydimensiones[0]);
                alto = float.Parse(arraydimensiones[1]);
                mleft = float.Parse(arraydimensiones[2]);
                mright = float.Parse(arraydimensiones[3]);
                mtop = float.Parse(arraydimensiones[4]);
                mbottom = float.Parse(arraydimensiones[5]);

            }

            float bigger = 14;
            float big = 10;
            float standar = 10;
            float small = 10;
            if (!string.IsNullOrEmpty(ticketfont))
            {
                string[] arrayfont= ticketfont.Split(',');
                bigger = float.Parse(arrayfont[0]);
                big= float.Parse(arrayfont[1]);
                standar = float.Parse(arrayfont[2]);
                small = float.Parse(arrayfont[3]);
            }



            var pgSize = new iTextSharp.text.Rectangle(ancho, alto);
            Document document = new Document(pgSize,mleft,mright,mtop,mbottom);
            //Document document = new Document(pgSize, 9, 13, 2, 2);



            try
            {



                var pdfWriter = PdfWriter.GetInstance(document, new FileStream(newFile, FileMode.Create));



                //pdfWriter.PageEvent = new ITextEvents();


                document.Open();

                PdfAction js = PdfAction.JavaScript(GetAutoPrintJs(impresora), pdfWriter);
                pdfWriter.AddJavaScript(js);

                iTextSharp.text.Font _biggerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, bigger, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _biggerboldFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, bigger, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                iTextSharp.text.Font _bigFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, big, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _bigboldFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, big, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, standar, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _boldFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, standar, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                iTextSharp.text.Font _smallFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, small, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _smallboldFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, small, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                Phrase phrase = new Phrase();

                PdfPTable tdatos = new PdfPTable(1);
                tdatos.WidthPercentage = 100;

                PdfPCell cdatos = new PdfPCell();
                cdatos.Border = 0;

                    
                phrase = new Phrase();
                phrase.Add(new Chunk(emp.emp_nombre.ToUpper(), _biggerboldFont));
                cdatos.AddElement(phrase);                
                if (!string.IsNullOrEmpty(ticketcab))
                {
                    phrase = new Phrase();                     
                    phrase.Add(new Chunk(ticketcab, _standardFont));
                    cdatos.AddElement(phrase);
                }
                tdatos.AddCell(cdatos);


                cdatos = new PdfPCell();
                cdatos.Border = 0;
                phrase = new Phrase();
                //phrase.Add(new Chunk("FACTURA Nro: " + fac.com_almacenid+ "-" + fac.com_pventaid+ "-" + fac.com_numero, _boldFont));
                phrase.Add(new Chunk(fac.com_doctran, _biggerboldFont));
                cdatos.AddElement(phrase);
                if (!string.IsNullOrEmpty(fac.com_claveelec))
                {
                    phrase = new Phrase();
                    phrase.Add(new Chunk(fac.com_claveelec, _smallboldFont));
                    cdatos.AddElement(phrase);
                }
                tdatos.AddCell(cdatos);

                cdatos = new PdfPCell();
                cdatos.Border = 0;


                phrase = new Phrase();
                phrase.Add(new Chunk("FECHA:", _boldFont));
                phrase.Add(new Chunk(fac.com_fechastr, _standardFont));
                cdatos.AddElement(phrase);

                phrase = new Phrase();
                phrase.Add(new Chunk("CLIENTE:", _boldFont));
                phrase.Add(new Chunk(fac.ccomdoc.cdoc_nombre, _standardFont));
                cdatos.AddElement(phrase);


                phrase = new Phrase();
                phrase.Add(new Chunk("RUC:", _boldFont));
                phrase.Add(new Chunk(fac.ccomdoc.cdoc_ced_ruc, _standardFont));
                cdatos.AddElement(phrase);

                phrase = new Phrase();
                phrase.Add(new Chunk("DIRECCIÓN:", _boldFont));
                phrase.Add(new Chunk(fac.ccomdoc.cdoc_direccion, _standardFont));
                cdatos.AddElement(phrase);


                phrase = new Phrase();
                phrase.Add(new Chunk("TELÉFONO:", _boldFont));
                phrase.Add(new Chunk(fac.ccomdoc.cdoc_telefono, _standardFont));
                cdatos.AddElement(phrase);

                phrase = new Phrase();
                phrase.Add(new Chunk("EMAIL:", _boldFont));
                phrase.Add(new Chunk(per.per_mail, _standardFont));
                cdatos.AddElement(phrase);
                

                tdatos.AddCell(cdatos);

                cdatos = new PdfPCell();                
                cdatos.Border = 0;
                cdatos.BorderWidthTop = 1;

                phrase = new Phrase();
                phrase.Add(new Chunk("DESTINO:", _boldFont));
                phrase.Add(new Chunk(fac.ccomenv.cenv_apellidos_des + " " + fac.ccomenv.cenv_nombres_des, _standardFont));
                cdatos.AddElement(phrase);

                phrase = new Phrase();
                phrase.Add(new Chunk("DIR. DESTINO:", _boldFont));
                phrase.Add(new Chunk(fac.ccomenv.cenv_direccion_des, _standardFont));
                cdatos.AddElement(phrase);

                phrase = new Phrase();
                phrase.Add(new Chunk("CIUDAD:", _boldFont));
                phrase.Add(new Chunk(fac.ccomenv.cenv_rutadestino, _standardFont));
                cdatos.AddElement(phrase);

                phrase = new Phrase();
                phrase.Add(new Chunk("REMITENTE:", _boldFont));
                phrase.Add(new Chunk(fac.ccomenv.cenv_apellidos_rem + " " + fac.ccomenv.cenv_nombres_rem, _standardFont));
                cdatos.AddElement(phrase);

                phrase = new Phrase();
                phrase.Add(new Chunk("ORIGEN:", _boldFont));
                phrase.Add(new Chunk(fac.ccomenv.cenv_rutaorigen, _standardFont));
                cdatos.AddElement(phrase);

                phrase = new Phrase();
                phrase.Add(new Chunk("POLITICA:", _boldFont));
                phrase.Add(new Chunk(fac.ccomdoc.cdoc_politicanombre, _standardFont));
                cdatos.AddElement(phrase);

                phrase = new Phrase();
                phrase.Add(new Chunk("HOJA RUTA:", _boldFont));
                phrase.Add(new Chunk(hojaruta, _standardFont));
                cdatos.AddElement(phrase);

                phrase = new Phrase();
                phrase.Add(new Chunk("SOCIO:", _boldFont));
                phrase.Add(new Chunk(fac.ccomenv.cenv_nombres_soc, _standardFont));
                cdatos.AddElement(phrase);

                if ((fac.total.tot_vseguro ?? 0) > 0)
                {
                    phrase = new Phrase();
                    phrase.Add(new Chunk("VALOR ASEGURADO:", _boldFont));
                    phrase.Add(new Chunk(Formatos.CurrencyFormat(fac.total.tot_vseguro), _standardFont));
                    cdatos.AddElement(phrase);
                }



                tdatos.AddCell(cdatos);

                document.Add(tdatos);


             

                #region Tabla Detalle 

                float[] columnWidthsdet = { 40, 15, 15, 15, 15 };
                PdfPTable tdetalle = new PdfPTable(5);

                ///tdetalle.PaddingTop = 5;
                tdetalle.WidthPercentage = 100;
                tdetalle.SetWidths(columnWidthsdet);

                PdfPCell ccodigodet = new PdfPCell();
                ccodigodet.AddElement(new Paragraph("PRODUCTO", _boldFont));
                ccodigodet.FixedHeight = 25;
                ccodigodet.Border = 0;
                ccodigodet.BorderWidthBottom = 1;


                PdfPCell ccantidaddet = new PdfPCell();
                ccantidaddet.AddElement(new Paragraph("CANT.", _boldFont) { Alignment = Element.ALIGN_CENTER });
                ccantidaddet.Border = 0;
                ccantidaddet.BorderWidthBottom = 1;
                //ccantidaddet.VerticalAlignment = Element.ALIGN_MIDDLE;


                PdfPCell cpreciodet = new PdfPCell();
                cpreciodet.AddElement(new Paragraph("V.UNI.", _boldFont) { Alignment = Element.ALIGN_CENTER });
                cpreciodet.Border = 0;
                cpreciodet.BorderWidthBottom = 1;
                //cpreciodet.VerticalAlignment = Element.ALIGN_MIDDLE;

                PdfPCell cdescuentodet = new PdfPCell();
                cdescuentodet.AddElement(new Paragraph("DESC.", _boldFont) { Alignment = Element.ALIGN_CENTER });
                cdescuentodet.Border = 0;
                cdescuentodet.BorderWidthBottom = 1;
                //cdescuentodet.VerticalAlignment = Element.ALIGN_MIDDLE;

                PdfPCell ctotaldet = new PdfPCell();
                ctotaldet.AddElement(new Paragraph("TOTAL", _boldFont) { Alignment = Element.ALIGN_CENTER });
                ctotaldet.Border = 0;
                ctotaldet.BorderWidthBottom = 1;
                //ctotaldet.VerticalAlignment = Element.ALIGN_MIDDLE;

                tdetalle.AddCell(ccodigodet);
                tdetalle.AddCell(ccantidaddet);
                tdetalle.AddCell(cpreciodet);
                tdetalle.AddCell(cdescuentodet);
                tdetalle.AddCell(ctotaldet);


                for (int i = 0; i < fac.ccomdoc.detalle.Count; i++)
                {
                    Dcomdoc det = fac.ccomdoc.detalle[i];

                    //tdetalle.AddCell(new PdfPCell(new Paragraph(det.codigoaux, _standardFont)) { FixedHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    //

                    string desc = det.ddoc_productonombre;
                    if (!string.IsNullOrEmpty(det.ddoc_observaciones))
                        desc += " " + det.ddoc_observaciones;

                    tdetalle.AddCell(new PdfPCell(new Paragraph(desc, _standardFont)) { Colspan = 5, Border = 0 });
                    tdetalle.AddCell(new PdfPCell(new Paragraph("", _standardFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                    tdetalle.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(det.ddoc_cantidad), _standardFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Border = 0 });
                    tdetalle.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(det.ddoc_precio), _standardFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                    tdetalle.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(det.ddoc_porc_desc), _standardFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                    tdetalle.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(det.ddoc_total), _standardFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                }

                tdetalle.AddCell(new PdfPCell(new Paragraph("", _standardFont)) { Colspan = 5, Border = 0, BorderWidthTop = 1 });

                tdetalle.SpacingAfter = 5;
                document.Add(tdetalle);

                #endregion


                float[] columnWidthspie = { 70, 30 };

                PdfPTable tdatospie = new PdfPTable(2);
                tdatospie.WidthPercentage = 100;
                tdatospie.SetWidths(columnWidthspie);
                

                tdatospie.AddCell(new PdfPCell(new Paragraph("SEGURO", _boldFont)) { Border = 0 });
                tdatospie.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(fac.total.tot_tseguro), _standardFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                tdatospie.AddCell(new PdfPCell(new Paragraph("TRANSPORTE", _boldFont)) { Border = 0 });
                tdatospie.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(fac.total.tot_transporte), _standardFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                tdatospie.AddCell(new PdfPCell(new Paragraph("SUBTOTAL 0%", _boldFont)) { Border = 0 });
                tdatospie.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(fac.total.tot_subtot_0), _standardFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                tdatospie.AddCell(new PdfPCell(new Paragraph("SUBTOTAL " + fac.total.tot_porc_impuesto+ "%", _boldFont)) { Border = 0 });
                tdatospie.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(fac.total.tot_subtotal), _standardFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                tdatospie.AddCell(new PdfPCell(new Paragraph("IVA " + fac.total.tot_porc_impuesto+ "%", _boldFont)) { Border = 0 });
                tdatospie.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(fac.total.tot_timpuesto), _standardFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });


                tdatospie.AddCell(new PdfPCell(new Paragraph("TOTAL ", _boldFont)) { Border = 0 });
                tdatospie.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(fac.total.tot_total), _boldFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                tdatospie.AddCell(new PdfPCell(new Paragraph("", _standardFont)) { Colspan = 2, Border = 0, BorderWidthTop = 1 });

                tdatospie.SpacingAfter = 5;
                document.Add(tdatospie);




                PdfPTable tdatosadi = new PdfPTable(1);
                tdatosadi.WidthPercentage = 100;

                PdfPCell cdatosadi = new PdfPCell();
                cdatosadi.Border = 0;


                phrase = new Phrase();
                phrase.Add(new Chunk("Documento sin validez tributaria", _standardFont));
                cdatosadi.AddElement(phrase);

                //phrase = new Phrase();
                //phrase.Add(new Chunk("Para descargar su documento ingresar en ", _standardFont));
                //phrase.Add(new Chunk("www.tao.com.ec", _boldFont));
                //cdatosadi.AddElement(phrase);

                phrase = new Phrase();
                phrase.Add(new Chunk("Usuario:", _boldFont));
                phrase.Add(new Chunk(fac.crea_usr, _boldFont));
                phrase.Add(new Chunk(" ", _boldFont));
                phrase.Add(new Chunk(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), _boldFont));
                cdatosadi.AddElement(phrase);


                tdatosadi.AddCell(cdatosadi);
                document.Add(tdatosadi);




                document.Close();

            }
            catch (Exception ex)
            {
                document.Close();
            }


            return fac.com_doctran + ".pdf";
        }


        public static PdfPCell CabeceraCell()
        {
            return CabeceraCell(null, null);
        }

        public static  PdfPCell CabeceraCell(int? colspan, int? rowspan)
        {
            PdfPCell cell = new PdfPCell();
            cell.Border = 0;
            cell.Padding = 0;            
            //cell.Height = 15;
            if (colspan.HasValue)
                cell.Colspan = colspan.Value;
            if (rowspan.HasValue)
                cell.Rowspan = rowspan.Value;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            
            return cell;
            
        }

        public static PdfPCell DetalleCell()
        {
            return DetalleCell(null, null);
        }

        public static PdfPCell DetalleCell(int? colspan, int? rowspan)
        {
            PdfPCell cell = new PdfPCell();
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight= 0;
            //cell.Padding = 0;
            //cell.Height = 15;
            if (colspan.HasValue)
                cell.Colspan = colspan.Value;
            if (rowspan.HasValue)
                cell.Rowspan = rowspan.Value;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            return cell;

        }


        public static string FacturaPDF(Empresa emp, List<Comprobante> facs, string[] hojasrutas, string path, bool autoprint, string impresora, string filename)
        {
            //string pdfTemplate = path + @"\FAC.pdf";
            //string pdfTemplate = path + @"\" + formato.for_pdf;
            string newFile = path + @"\" + filename + ".pdf";
            
            //Persona per = PersonaBLL.GetByPK(new Persona { per_empresa = fac.com_empresa, per_empresa_key = fac.com_empresa, per_codigo = fac.com_codclipro.Value, per_codigo_key = fac.com_codclipro.Value });



            Document document = new Document(PageSize.A5.Rotate(), 10, 10, 10, 10);

           

            try
            {

                float fixedleading = 9;
                float multipleleading = 0;

                var pdfWriter = PdfWriter.GetInstance(document, new FileStream(newFile, FileMode.Create));


                //pdfWriter.PageEvent = new ITextEvents();


                document.Open();

                PdfAction js = PdfAction.JavaScript(GetAutoPrintJs(impresora), pdfWriter);
                pdfWriter.AddJavaScript(js);

                iTextSharp.text.Font _biggerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _biggerboldFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                iTextSharp.text.Font _bigFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _bigboldFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _boldFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                Paragraph paragraph = new Paragraph();
                Phrase phrase = new Phrase();
                PdfPCell cell = new PdfPCell();

                int h = 0;
                foreach (Comprobante fac in facs)
                {


                    #region Cabecera 

                    //TABLA CABECERA                
                    PdfPTable tcabecera = new PdfPTable(2);
                    tcabecera.WidthPercentage = 100;
                    tcabecera.SetWidths(new float[] { 50, 50 });



                    //FILA 1
                    cell = CabeceraCell(null, 2);
                    cell.AddElement(new Paragraph(emp.emp_nombre.ToUpper(), _biggerboldFont) { Alignment = Element.ALIGN_LEFT });
                    tcabecera.AddCell(cell);


                    cell = CabeceraCell();
                    cell.AddElement(new Paragraph(fac.com_doctran, _biggerboldFont) { Alignment = Element.ALIGN_LEFT });
                    tcabecera.AddCell(cell);

                    if (string.IsNullOrEmpty(fac.com_claveelec))
                        fac.com_claveelec = " ";

                    cell = CabeceraCell();
                    cell.AddElement(new Paragraph(fac.com_claveelec, _standardFont) { Alignment = Element.ALIGN_LEFT });
                    tcabecera.AddCell(cell);


                    //FILA 2

                    phrase = new Phrase();
                    phrase.Add(new Chunk("FECHA:", _boldFont));
                    phrase.Add(new Chunk(fac.com_fechastr, _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.AddElement(phrase);
                    tcabecera.AddCell(cell);


                    phrase = new Phrase();
                    phrase.Add(new Chunk("DESTINO:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomenv.cenv_apellidos_des + " " + fac.ccomenv.cenv_nombres_des, _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.NoWrap = true;
                    cell.AddElement(phrase);
                    tcabecera.AddCell(cell);

                    //FILA 3

                    phrase = new Phrase();
                    phrase.Add(new Chunk("CLIENTE:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomdoc.cdoc_nombre, _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.NoWrap = true;
                    cell.AddElement(phrase);
                    tcabecera.AddCell(cell);


                    phrase = new Phrase();
                    phrase.Add(new Chunk("DIR. DESTINO:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomenv.cenv_direccion_des, _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.NoWrap = true;
                    cell.AddElement(phrase);
                    tcabecera.AddCell(cell);

                    //FILA 4

                    phrase = new Phrase();
                    phrase.Add(new Chunk("RUC:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomdoc.cdoc_ced_ruc, _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.AddElement(phrase);
                    tcabecera.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("CIUDAD:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomenv.cenv_rutadestino, _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.AddElement(phrase);
                    tcabecera.AddCell(cell);

                    //FILA 5

                    phrase = new Phrase();
                    phrase.Add(new Chunk("DIRECCIÓN:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomdoc.cdoc_direccion, _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.NoWrap = true;
                    cell.AddElement(phrase);
                    tcabecera.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("REMITENTE:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomenv.cenv_apellidos_rem + " " + fac.ccomenv.cenv_nombres_rem, _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.NoWrap = true;
                    cell.AddElement(phrase);
                    tcabecera.AddCell(cell);

                    //FILA 6

                    phrase = new Phrase();
                    phrase.Add(new Chunk("TELF:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomdoc.cdoc_telefono, _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.AddElement(phrase);
                    tcabecera.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("TELF:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomenv.cenv_telefono_des, _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.AddElement(phrase);
                    tcabecera.AddCell(cell);

                    //FILA 7
                    phrase = new Phrase();
                    phrase.Add(new Chunk("EMAIL:", _boldFont));
                    phrase.Add(new Chunk("", _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.AddElement(phrase);
                    tcabecera.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("ORIGEN:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomenv.cenv_rutaorigen, _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.AddElement(phrase);
                    tcabecera.AddCell(cell);

                    document.Add(tcabecera);

                    #endregion

                    #region Detalle 

                    //TABLA CABECERA                
                    PdfPTable tdetalle = new PdfPTable(5);
                    tdetalle.WidthPercentage = 100;
                    tdetalle.SetWidths(new float[] { 10, 60, 10, 10, 10 });
                    tdetalle.SplitRows = true;
                    tdetalle.SpacingBefore = 10;

                    cell = DetalleCell();
                    paragraph = new Paragraph("CANT", _boldFont) { Alignment = Element.ALIGN_CENTER };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tdetalle.AddCell(cell);

                    cell = DetalleCell();
                    paragraph = new Paragraph("DESCRIPCIÓN", _boldFont) { Alignment = Element.ALIGN_CENTER };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tdetalle.AddCell(cell);

                    cell = DetalleCell();
                    paragraph = new Paragraph("PESO", _boldFont) { Alignment = Element.ALIGN_CENTER };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tdetalle.AddCell(cell);

                    cell = DetalleCell();
                    paragraph = new Paragraph("P.UNIT", _boldFont) { Alignment = Element.ALIGN_CENTER };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tdetalle.AddCell(cell);

                    cell = DetalleCell();
                    paragraph = new Paragraph("TOTAL", _boldFont) { Alignment = Element.ALIGN_CENTER };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tdetalle.AddCell(cell);

                    int detallemax = 12;

                    for (int i = 0; i < fac.ccomdoc.detalle.Count; i++)
                    {
                        Dcomdoc det = fac.ccomdoc.detalle[i];

                        //tdetalle.AddCell(new PdfPCell(new Paragraph(det.codigoaux, _standardFont)) { FixedHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                        //

                        string desc = det.ddoc_productonombre;
                        if (!string.IsNullOrEmpty(det.ddoc_observaciones))
                            desc += " " + det.ddoc_observaciones;

                        if (desc.Length > 100)
                            detallemax--;
                        if (desc.Length > 200)
                            detallemax--;

                        tdetalle.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(det.ddoc_cantidad), _standardFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Border = 0 });
                        tdetalle.AddCell(new PdfPCell(new Paragraph(desc, _standardFont)) { Border = 0 });
                        tdetalle.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(det.ddoc_precio), _standardFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                        tdetalle.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(det.ddoc_porc_desc), _standardFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                        tdetalle.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(det.ddoc_total), _standardFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                    }

                    detallemax = detallemax - fac.ccomdoc.detalle.Count;
                    if (detallemax > 0)
                    {
                        for (int i = 0; i < detallemax; i++)
                        {
                            tdetalle.AddCell(new PdfPCell(new Paragraph(" ")) { Border = 0 });
                            tdetalle.AddCell(new PdfPCell(new Paragraph(" ")) { Border = 0 });
                            tdetalle.AddCell(new PdfPCell(new Paragraph(" ")) { Border = 0 });
                            tdetalle.AddCell(new PdfPCell(new Paragraph(" ")) { Border = 0 });
                            tdetalle.AddCell(new PdfPCell(new Paragraph(" ")) { Border = 0 });


                        }
                    }




                    document.Add(tdetalle);
                    #endregion

                    #region Pie


                    //TABLA PIE      
                    PdfPTable tpie = new PdfPTable(4);
                    tpie.WidthPercentage = 100;
                    tpie.SetWidths(new float[] { 35, 35, 20, 10 });
                    tpie.SplitRows = true;
                    tpie.SpacingBefore = 5;


                    phrase = new Phrase();
                    phrase.Add(new Chunk("BULTOS:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomdoc.detalle.Sum(s => s.ddoc_cantidad).ToString(), _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.BorderWidthTop = 1;
                    cell.AddElement(phrase);
                    tpie.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("VALOR DECLARADO:", _boldFont));
                    phrase.Add(new Chunk(Formatos.CurrencyFormat(fac.total.tot_vseguro), _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.BorderWidthTop = 1;
                    cell.AddElement(phrase);
                    tpie.AddCell(cell);

                    cell = CabeceraCell();
                    paragraph = new Paragraph("SEGURO:", _boldFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    cell.BorderWidthTop = 1;
                    tpie.AddCell(cell);

                    cell = CabeceraCell();
                    paragraph = new Paragraph(Formatos.CurrencyFormat(fac.total.tot_tseguro), _standardFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    cell.BorderWidthTop = 1;
                    tpie.AddCell(cell);

                    //FILA 2

                    phrase = new Phrase();
                    phrase.Add(new Chunk("FORMA PAGO:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomdoc.cdoc_politicanombre, _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.AddElement(phrase);
                    tpie.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("GUIA REMISIÓN:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomenv.cenv_guia1 + "-" + fac.ccomenv.cenv_guia2 + "-" + fac.ccomenv.cenv_guia3, _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.AddElement(phrase);
                    tpie.AddCell(cell);

                    cell = CabeceraCell();
                    paragraph = new Paragraph("TRANSPORTE:", _boldFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);

                    cell = CabeceraCell();
                    paragraph = new Paragraph(Formatos.CurrencyFormat(fac.total.tot_transporte), _standardFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);


                    //FILA 3

                    phrase = new Phrase();
                    phrase.Add(new Chunk("HOJA DE RUTA:", _boldFont));
                    phrase.Add(new Chunk(hojasrutas[h], _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.AddElement(phrase);
                    tpie.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("PLACA:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomenv.cenv_placa, _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.AddElement(phrase);
                    tpie.AddCell(cell);

                    cell = CabeceraCell();
                    paragraph = new Paragraph("SUBTOTAL 0%:", _boldFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);

                    cell = CabeceraCell();
                    paragraph = new Paragraph(Formatos.CurrencyFormat(fac.total.tot_subtot_0), _standardFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);


                    //FILA 3

                    phrase = new Phrase();
                    phrase.Add(new Chunk("SOCIO:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomenv.cenv_nombres_soc, _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.AddElement(phrase);
                    tpie.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("CHOFER:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomenv.cenv_nombres_soc, _standardFont));
                    phrase.SetLeading(fixedleading, multipleleading);
                    cell = CabeceraCell();
                    cell.AddElement(phrase);
                    tpie.AddCell(cell);


                    cell = CabeceraCell();
                    paragraph = new Paragraph("SUBTOTAL " + fac.total.tot_porc_impuesto.ToString() + "%:", _boldFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);

                    cell = CabeceraCell();
                    paragraph = new Paragraph(Formatos.CurrencyFormat(fac.total.tot_subtotal), _standardFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);

                    //FILA 4

                    cell = CabeceraCell();
                    cell.Colspan = 2;
                    paragraph = new Paragraph("", _boldFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);


                    cell = CabeceraCell();
                    paragraph = new Paragraph("SUBTOTAL:", _boldFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);

                    cell = CabeceraCell();
                    paragraph = new Paragraph(Formatos.CurrencyFormat(fac.total.tot_subtotal + fac.total.tot_subtot_0), _standardFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);

                    //FILA 5

                    cell = CabeceraCell();
                    cell.Colspan = 2;
                    paragraph = new Paragraph("", _boldFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);


                    cell = CabeceraCell();
                    paragraph = new Paragraph("IVA  " + fac.total.tot_porc_impuesto.ToString() + "%:", _boldFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);

                    cell = CabeceraCell();
                    paragraph = new Paragraph(Formatos.CurrencyFormat(fac.total.tot_timpuesto), _standardFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);


                    //FILA 6

                    cell = CabeceraCell();
                    cell.Colspan = 2;
                    paragraph = new Paragraph("", _boldFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);


                    cell = CabeceraCell();
                    paragraph = new Paragraph("TOTAL", _bigboldFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);

                    cell = CabeceraCell();
                    paragraph = new Paragraph(Formatos.CurrencyFormat(fac.total.tot_total), _bigboldFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);


                    //FILA 7
                    cell = CabeceraCell();
                    cell.Colspan = 4;
                    paragraph = new Paragraph(" ", _boldFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);
                    //FILA 8
                    cell = CabeceraCell();
                    cell.Colspan = 4;
                    paragraph = new Paragraph(" ", _boldFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);

                    //FILA 9

                    cell = CabeceraCell();
                    //cell.BorderWidthTop = 1;                
                    paragraph = new Paragraph("ENTREGADO POR", _boldFont) { Alignment = Element.ALIGN_CENTER };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);

                    cell = CabeceraCell();
                    //cell.BorderWidthTop = 1;                 
                    paragraph = new Paragraph("RECIBIDO POR", _boldFont) { Alignment = Element.ALIGN_CENTER };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);

                    cell = CabeceraCell();
                    paragraph = new Paragraph(DateTime.Now.ToString(), _standardFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);

                    cell = CabeceraCell();
                    paragraph = new Paragraph(fac.crea_usr, _standardFont) { Alignment = Element.ALIGN_RIGHT };
                    paragraph.SetLeading(fixedleading, multipleleading);
                    cell.AddElement(paragraph);
                    tpie.AddCell(cell);


                    h++;

                    document.Add(tpie);

                    #endregion

                    document.NewPage();
                }


                document.Close();

            }
            catch (Exception ex)
            {
                document.Close();
            }


            return filename + ".pdf";
        }

        public static string FacturaTicketPDF(Empresa emp, List<Comprobante> facs, string[] hojasrutas, string path, bool autoprint, string impresora, string filename, string dimensiones)
        {

            //string pdfTemplate = path + @"\FAC.pdf";
            string newFile = path + @"\" + filename + ".pdf";

            float ancho = 300;
            float alto = 600;
            float mleft = 9;
            float mright = 13;
            float mtop = 2;
            float mbottom = 2;

            if (dimensiones != "")
            {
                string[] arraydimensiones = dimensiones.Split(',');
                ancho = float.Parse(arraydimensiones[0]);
                alto = float.Parse(arraydimensiones[1]);
                mleft = float.Parse(arraydimensiones[2]);
                mright = float.Parse(arraydimensiones[3]);
                mtop = float.Parse(arraydimensiones[4]);
                mbottom = float.Parse(arraydimensiones[5]);

            }



            var pgSize = new iTextSharp.text.Rectangle(ancho, alto);
            Document document = new Document(pgSize, mleft, mright, mtop, mbottom);





            try
            {


                var pdfWriter = PdfWriter.GetInstance(document, new FileStream(newFile, FileMode.Create));
                
                document.Open();

                iTextSharp.text.Font _biggerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _biggerboldFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                iTextSharp.text.Font _bigFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _bigboldFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _boldFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                PdfAction js = PdfAction.JavaScript(GetAutoPrintJs(impresora), pdfWriter);
                pdfWriter.AddJavaScript(js);

                Phrase phrase = new Phrase();

                
                


                int h = 0;
                foreach (Comprobante fac in facs)
                {
                                      

                    PdfPTable tdatos = new PdfPTable(1);
                    tdatos.WidthPercentage = 100;

                    PdfPCell cdatos = new PdfPCell();
                    cdatos.Border = 0;


                    phrase = new Phrase();
                    phrase.Add(new Chunk(emp.emp_nombre.ToUpper(), _biggerboldFont));
                    cdatos.AddElement(phrase);
                    phrase = new Phrase();
                    //phrase.Add(new Chunk("FACTURA Nro: " + fac.com_almacenid+ "-" + fac.com_pventaid+ "-" + fac.com_numero, _boldFont));
                    phrase.Add(new Chunk(fac.com_doctran, _biggerboldFont));
                    cdatos.AddElement(phrase);
                    if (!string.IsNullOrEmpty(fac.com_claveelec))
                    {
                        phrase = new Phrase();
                        phrase.Add(new Chunk(fac.com_claveelec, _standardFont));
                        cdatos.AddElement(phrase);
                    }
                    tdatos.AddCell(cdatos);

                    cdatos = new PdfPCell();
                    cdatos.Border = 0;


                    phrase = new Phrase();
                    phrase.Add(new Chunk("FECHA:", _boldFont));
                    phrase.Add(new Chunk(fac.com_fechastr, _standardFont));
                    cdatos.AddElement(phrase);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("CLIENTE:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomdoc.cdoc_nombre, _standardFont));
                    cdatos.AddElement(phrase);


                    phrase = new Phrase();
                    phrase.Add(new Chunk("RUC:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomdoc.cdoc_ced_ruc, _standardFont));
                    cdatos.AddElement(phrase);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("DIRECCIÓN:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomdoc.cdoc_direccion, _standardFont));
                    cdatos.AddElement(phrase);


                    phrase = new Phrase();
                    phrase.Add(new Chunk("TELÉFONO:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomdoc.cdoc_telefono, _standardFont));
                    cdatos.AddElement(phrase);

                    //phrase = new Phrase();
                    //phrase.Add(new Chunk("EMAIL:", _boldFont));
                    //phrase.Add(new Chunk(per.per_mail, _standardFont));
                    //cdatos.AddElement(phrase);


                    tdatos.AddCell(cdatos);

                    cdatos = new PdfPCell();
                    cdatos.Border = 0;
                    cdatos.BorderWidthTop = 1;

                    phrase = new Phrase();
                    phrase.Add(new Chunk("DESTINO:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomenv.cenv_apellidos_des + " " + fac.ccomenv.cenv_nombres_des, _standardFont));
                    cdatos.AddElement(phrase);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("DIR. DESTINO:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomenv.cenv_direccion_des, _standardFont));
                    cdatos.AddElement(phrase);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("CIUDAD:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomenv.cenv_rutadestino, _standardFont));
                    cdatos.AddElement(phrase);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("REMITENTE:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomenv.cenv_apellidos_rem + " " + fac.ccomenv.cenv_nombres_rem, _standardFont));
                    cdatos.AddElement(phrase);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("ORIGEN:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomenv.cenv_rutaorigen, _standardFont));
                    cdatos.AddElement(phrase);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("POLITICA:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomdoc.cdoc_politicanombre, _standardFont));
                    cdatos.AddElement(phrase);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("HOJA RUTA:", _boldFont));
                    phrase.Add(new Chunk(hojasrutas[h], _standardFont));
                    cdatos.AddElement(phrase);


                    phrase = new Phrase();
                    phrase.Add(new Chunk("SOCIO:", _boldFont));
                    phrase.Add(new Chunk(fac.ccomenv.cenv_nombres_soc, _standardFont));
                    cdatos.AddElement(phrase);

                    if ((fac.total.tot_vseguro ?? 0) > 0)
                    {
                        phrase = new Phrase();
                        phrase.Add(new Chunk("VALOR ASEGURADO:", _boldFont));
                        phrase.Add(new Chunk(Formatos.CurrencyFormat(fac.total.tot_vseguro), _standardFont));
                        cdatos.AddElement(phrase);
                    }



                    tdatos.AddCell(cdatos);

                    document.Add(tdatos);




                    #region Tabla Detalle 

                    float[] columnWidthsdet = { 40, 15, 15, 15, 15 };
                    PdfPTable tdetalle = new PdfPTable(5);

                    ///tdetalle.PaddingTop = 5;
                    tdetalle.WidthPercentage = 100;
                    tdetalle.SetWidths(columnWidthsdet);

                    PdfPCell ccodigodet = new PdfPCell();
                    ccodigodet.AddElement(new Paragraph("PRODUCTO", _boldFont));
                    ccodigodet.FixedHeight = 25;
                    ccodigodet.Border = 0;
                    ccodigodet.BorderWidthBottom = 1;


                    PdfPCell ccantidaddet = new PdfPCell();
                    ccantidaddet.AddElement(new Paragraph("CANT.", _boldFont) { Alignment = Element.ALIGN_CENTER });
                    ccantidaddet.Border = 0;
                    ccantidaddet.BorderWidthBottom = 1;
                    //ccantidaddet.VerticalAlignment = Element.ALIGN_MIDDLE;


                    PdfPCell cpreciodet = new PdfPCell();
                    cpreciodet.AddElement(new Paragraph("V.UNI.", _boldFont) { Alignment = Element.ALIGN_CENTER });
                    cpreciodet.Border = 0;
                    cpreciodet.BorderWidthBottom = 1;
                    //cpreciodet.VerticalAlignment = Element.ALIGN_MIDDLE;

                    PdfPCell cdescuentodet = new PdfPCell();
                    cdescuentodet.AddElement(new Paragraph("DESC.", _boldFont) { Alignment = Element.ALIGN_CENTER });
                    cdescuentodet.Border = 0;
                    cdescuentodet.BorderWidthBottom = 1;
                    //cdescuentodet.VerticalAlignment = Element.ALIGN_MIDDLE;

                    PdfPCell ctotaldet = new PdfPCell();
                    ctotaldet.AddElement(new Paragraph("TOTAL", _boldFont) { Alignment = Element.ALIGN_CENTER });
                    ctotaldet.Border = 0;
                    ctotaldet.BorderWidthBottom = 1;
                    //ctotaldet.VerticalAlignment = Element.ALIGN_MIDDLE;

                    tdetalle.AddCell(ccodigodet);
                    tdetalle.AddCell(ccantidaddet);
                    tdetalle.AddCell(cpreciodet);
                    tdetalle.AddCell(cdescuentodet);
                    tdetalle.AddCell(ctotaldet);


                    for (int i = 0; i < fac.ccomdoc.detalle.Count; i++)
                    {
                        Dcomdoc det = fac.ccomdoc.detalle[i];

                        //tdetalle.AddCell(new PdfPCell(new Paragraph(det.codigoaux, _standardFont)) { FixedHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                        //

                        string desc = det.ddoc_productonombre;
                        if (!string.IsNullOrEmpty(det.ddoc_observaciones))
                            desc += " " + det.ddoc_observaciones;

                        tdetalle.AddCell(new PdfPCell(new Paragraph(desc, _standardFont)) { Colspan = 5, Border = 0 });
                        tdetalle.AddCell(new PdfPCell(new Paragraph("", _standardFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                        tdetalle.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(det.ddoc_cantidad), _standardFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Border = 0 });
                        tdetalle.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(det.ddoc_precio), _standardFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                        tdetalle.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(det.ddoc_porc_desc), _standardFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                        tdetalle.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(det.ddoc_total), _standardFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                    }

                    tdetalle.AddCell(new PdfPCell(new Paragraph("", _standardFont)) { Colspan = 5, Border = 0, BorderWidthTop = 1 });

                    tdetalle.SpacingAfter = 5;
                    document.Add(tdetalle);

                    #endregion


                    float[] columnWidthspie = { 70, 30 };

                    PdfPTable tdatospie = new PdfPTable(2);
                    tdatospie.WidthPercentage = 100;
                    tdatospie.SetWidths(columnWidthspie);


                    tdatospie.AddCell(new PdfPCell(new Paragraph("SEGURO", _boldFont)) { Border = 0 });
                    tdatospie.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(fac.total.tot_tseguro), _standardFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                    tdatospie.AddCell(new PdfPCell(new Paragraph("TRANSPORTE", _boldFont)) { Border = 0 });
                    tdatospie.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(fac.total.tot_transporte), _standardFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                    tdatospie.AddCell(new PdfPCell(new Paragraph("SUBTOTAL 0%", _boldFont)) { Border = 0 });
                    tdatospie.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(fac.total.tot_subtot_0), _standardFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                    tdatospie.AddCell(new PdfPCell(new Paragraph("SUBTOTAL " + fac.total.tot_porc_impuesto + "%", _boldFont)) { Border = 0 });
                    tdatospie.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(fac.total.tot_subtotal), _standardFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                    tdatospie.AddCell(new PdfPCell(new Paragraph("IVA " + fac.total.tot_porc_impuesto + "%", _boldFont)) { Border = 0 });
                    tdatospie.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(fac.total.tot_timpuesto), _standardFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });


                    tdatospie.AddCell(new PdfPCell(new Paragraph("TOTAL ", _boldFont)) { Border = 0 });
                    tdatospie.AddCell(new PdfPCell(new Paragraph(Formatos.CurrencyFormat(fac.total.tot_total), _boldFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                    tdatospie.AddCell(new PdfPCell(new Paragraph("", _standardFont)) { Colspan = 2, Border = 0, BorderWidthTop = 1 });

                    tdatospie.SpacingAfter = 5;
                    document.Add(tdatospie);




                    PdfPTable tdatosadi = new PdfPTable(1);
                    tdatosadi.WidthPercentage = 100;

                    PdfPCell cdatosadi = new PdfPCell();
                    cdatosadi.Border = 0;


                    phrase = new Phrase();
                    phrase.Add(new Chunk("Documento sin validez tributaria", _standardFont));
                    cdatosadi.AddElement(phrase);

                    //phrase = new Phrase();
                    //phrase.Add(new Chunk("Para descargar su documento ingresar en ", _standardFont));
                    //phrase.Add(new Chunk("www.tao.com.ec", _boldFont));
                    //cdatosadi.AddElement(phrase);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("Usuario:", _boldFont));
                    phrase.Add(new Chunk(fac.crea_usr, _boldFont));
                    phrase.Add(new Chunk(" ", _boldFont));
                    phrase.Add(new Chunk(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), _boldFont));
                    cdatosadi.AddElement(phrase);


                    tdatosadi.AddCell(cdatosadi);
                    document.Add(tdatosadi);

                    h++;

                    document.NewPage();
                }


                document.Close();

            }
            catch (Exception ex)
            {
                document.Close();
            }


            return filename + ".pdf";
        }





        public static string PdfFAC(Comprobante fac, Formato formato, string hojaruta,string path,  bool autoprint,  string impresora,int format)
        {
            //string pdfTemplate = path + @"\FAC.pdf";
            string pdfTemplate = path + @"\" + formato.for_pdf;
            string newFile = path + @"\" + fac.com_doctran + ".pdf";


            iTextSharp.text.Font fontnormal = FontFactory.GetFont(BaseFont.COURIER, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font fontbold = FontFactory.GetFont(BaseFont.COURIER, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font fontbold1 = FontFactory.GetFont(BaseFont.COURIER, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font fontbig = FontFactory.GetFont(BaseFont.COURIER, 20, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            //if (format == 4 )
            //{
            //    fontnormal = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            //    fontbold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            //    fontbig = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 24, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            //}

            Persona per = PersonaBLL.GetByPK(new Persona { per_empresa = fac.com_empresa, per_empresa_key = fac.com_empresa, per_codigo = fac.com_codclipro.Value, per_codigo_key = fac.com_codclipro.Value });


            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;

            if (format > 2)
            {
                foreach (KeyValuePair<string, AcroFields.Item> kvp in pdfFormFields.Fields)
                {
                    if (format==3)
                        pdfFormFields.SetFieldProperty(kvp.Key, "textfont", fontnormal.BaseFont, null);
                    if (format==4)
                        pdfFormFields.SetFieldProperty(kvp.Key, "textfont", fontbold.BaseFont, null);
                    pdfFormFields.RegenerateField(kvp.Key);
                }
            }

            if (autoprint)
            {
                var writer = pdfStamper.Writer;
                PdfAction js = PdfAction.JavaScript(GetAutoPrintJs(impresora), writer);
                writer.AddJavaScript(js);          //IMPRESION AUTOMATICA
            }

            string parametrourl = Constantes.GetParameter("electronicurl");

            // set form pdfFormFields
            // The first worksheet and W-4 form
            pdfFormFields.SetFieldProperty("numero", "textfont", fontbold1.BaseFont, null);
            pdfFormFields.SetField("numero", fac.com_doctran);

            bool electronico = !string.IsNullOrEmpty(fac.com_claveelec);

            if (electronico)
            {                               
                pdfFormFields.SetField("clave", fac.com_claveelec);
                pdfFormFields.SetField("url", parametrourl);
            }









            DateTime ahora = DateTime.Now;
            pdfFormFields.SetField("fechacrea", string.Format("{0:00}/{1:00}/{2:0000} {3:00}:{4:00}:{5:00}", ahora.Day, ahora.Month, ahora.Year, ahora.Hour, ahora.Minute, ahora.Second));
            pdfFormFields.SetField("usuario", fac.crea_usr);
            pdfFormFields.SetField("nombreusuario", fac.crea_usrnombres);

            pdfFormFields.SetField("fecha", fac.com_fecha.ToLongDateString());
            pdfFormFields.SetField("cliente", fac.ccomdoc.cdoc_nombre);
            pdfFormFields.SetField("ruc", fac.ccomdoc.cdoc_ced_ruc);
            pdfFormFields.SetField("direccion", fac.ccomdoc.cdoc_direccion);
            pdfFormFields.SetField("telefono", fac.ccomdoc.cdoc_telefono);            
            pdfFormFields.SetField("email", per.per_mail);

            pdfFormFields.SetField("destinatario", fac.ccomenv.cenv_apellidos_des + " " + fac.ccomenv.cenv_nombres_des);
            pdfFormFields.SetField("direcciondes", fac.ccomenv.cenv_direccion_des);
            pdfFormFields.SetField("ciudaddes", fac.ccomenv.cenv_rutadestino);
            pdfFormFields.SetField("remitente", fac.ccomenv.cenv_apellidos_rem + " " + fac.ccomenv.cenv_nombres_rem);
            pdfFormFields.SetField("propietario", fac.ccomenv.cenv_nombres_soc);
            pdfFormFields.SetField("conductor", fac.ccomenv.cenv_nombres_cho);
            pdfFormFields.SetField("origen", fac.ccomenv.cenv_rutaorigen);
            pdfFormFields.SetField("placa", "PLACA:"+  fac.ccomenv.cenv_placa);

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
            pdfFormFields.SetField("transporte", fac.total.tot_transporte.ToString("0.00"));
            pdfFormFields.SetField("guia", fac.ccomenv.cenv_guia1 + "-" + fac.ccomenv.cenv_guia2 + "-" + fac.ccomenv.cenv_guia3);
            pdfFormFields.SetField("politica", fac.ccomdoc.cdoc_politicanombre);
            pdfFormFields.SetField("entrega", fac.ccomenv.cenv_observacion);
            pdfFormFields.SetField("hojaruta", hojaruta);


            decimal subtotal = fac.total.tot_subtot_0 + fac.total.tot_subtotal;

            pdfFormFields.SetField("subtotal0", fac.total.tot_subtot_0.ToString("0.00"));
            pdfFormFields.SetField("subtotal12", fac.total.tot_subtotal.ToString("0.00"));
            pdfFormFields.SetField("subtotalimp", fac.total.tot_subtotal.ToString("0.00"));
            pdfFormFields.SetField("subtotal", subtotal.ToString("0.00"));
            pdfFormFields.SetField("iva", fac.total.tot_timpuesto.ToString("0.00"));
            pdfFormFields.SetField("total", fac.total.tot_total.ToString("0.00"));
            pdfFormFields.SetFieldProperty("total", "textfont", fontbold.BaseFont, null);
            pdfFormFields.SetField("imp", fac.total.tot_porc_impuesto.Value.ToString("0.0"));

            pdfFormFields.SetField("s12", "SUBTOTAL " + fac.total.tot_porc_impuesto.Value.ToString("0.0") + "%:");
            pdfFormFields.SetField("iv", "IVA " + fac.total.tot_porc_impuesto.Value.ToString("0.0") + "%:");
            // report by reading values from completed PDF
            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();
            return fac.com_doctran + ".pdf";
        }


        public static string PdfFACTC(Comprobante fac, Formato formato, string hojaruta, string path, bool autoprint, string impresora)
        {
            string pdfTemplate = path + @"\" + formato.for_pdf;
            string newFile = path + @"\" + fac.com_doctran + ".pdf";

            FileStream fs = new FileStream(newFile, FileMode.Create, FileAccess.Write, FileShare.None);

            Persona pe = PersonaBLL.GetByPK(new Persona { per_codigo = int.Parse((fac.com_agente.HasValue) ? fac.com_agente.ToString() : "0"), per_empresa = int.Parse(fac.com_empresa.ToString()), per_codigo_key = int.Parse((fac.com_agente.HasValue) ? fac.com_agente.ToString() : "0"), per_empresa_key = int.Parse(fac.com_empresa.ToString()) });
            PdfReader pdfReader = new PdfReader(pdfTemplate);
            // PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            PdfStamper pdfStamper = new PdfStamper(pdfReader, fs);
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            //inicio creacion de la tabla

            foreach (KeyValuePair<string, AcroFields.Item> kvp in pdfFormFields.Fields)
            {

                pdfFormFields.SetFieldProperty(kvp.Key, "textsize", 7f, null);

                pdfFormFields.SetFieldProperty(kvp.Key, "setfflags", PdfFormField.MK_CAPTION_LEFT, null);

                pdfFormFields.SetFieldProperty(kvp.Key, "setfflags", PdfFormField.FF_READ_ONLY, null);

                if (kvp.Key.IndexOf("DescripciónRow") >= 0)
                {

                    //pdfFormFields.SetFieldProperty(kvp.Key, "textsize", f, null);

                    pdfFormFields.SetFieldProperty(kvp.Key, "fflags", PdfFormField.FF_MULTILINE, null);

                }
            }
            iTextSharp.text.Font fntTableFontHdr = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font fntTableFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);




            PdfPTable Table = new PdfPTable(5);
            Table.WidthPercentage = 100;
            Table.HorizontalAlignment = 0;
            Table.SpacingAfter = 10;

            float[] sglTblHdWidths = new float[5];
            sglTblHdWidths[0] = 300f;
            sglTblHdWidths[1] = 300f;
            sglTblHdWidths[2] = 100f;
            sglTblHdWidths[3] = 100f;
            sglTblHdWidths[4] = 100f;
            Table.SetWidths(sglTblHdWidths);

            //ENCABEZADO
            PdfPCell CellOneHdr = new PdfPCell(new Phrase("PRODUCTO", fntTableFontHdr));

            Table.AddCell(CellOneHdr);
            PdfPCell CellTwoHdr = new PdfPCell(new Phrase("ORIGEN Y DESTINO", fntTableFontHdr));

            Table.AddCell(CellTwoHdr);
            PdfPCell CellTreeHdr = new PdfPCell(new Phrase("VOLUMEN", fntTableFontHdr));
            CellTreeHdr.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            Table.AddCell(CellTreeHdr);
            PdfPCell CellOne = new PdfPCell(new Phrase("VALOR", fntTableFontHdr));
            CellOne.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            Table.AddCell(CellOne);
            PdfPCell CellTwo = new PdfPCell(new Phrase("TOTAL", fntTableFontHdr));
            CellTwo.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            Table.AddCell(CellTwo);
            //FIN DE ENCABEZADO








            //fin de creacion de tabla


            if (autoprint)
            {
                var writer = pdfStamper.Writer;
                PdfAction js = PdfAction.JavaScript(GetAutoPrintJs(impresora), writer);
                writer.AddJavaScript(js);          //IMPRESION AUTOMATICA
            }

            // set form pdfFormFields
            // The first worksheet and W-4 form
            //pdfFormFields.SetField("numero", fac.com_doctran);

            DateTime ahora = DateTime.Now;
            pdfFormFields.SetField("lugarfecha","Cuenca, "+ string.Format("{0} {1:00} del {2:0000}",  CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ahora.Month),ahora.Day, ahora.Year));
           // pdfFormFields.SetField("fechacrea", string.Format("{0:00}/{1:00}/{2:0000} {3:00}:{4:00}:{5:00}", ahora.Day, ahora.Month, ahora.Year, ahora.Hour, ahora.Minute, ahora.Second));
          //  pdfFormFields.SetField("usuario", fac.crea_usr);
            //pdfFormFields.SetField("nombreusuario", fac.crea_usrnombres);
            pdfFormFields.SetField("placas", fac.com_concepto);
            pdfFormFields.SetField("fecha", fac.com_fecha.ToLongDateString());
            pdfFormFields.SetField("cliente", fac.ccomdoc.cdoc_nombre);
            pdfFormFields.SetField("ruc", fac.ccomdoc.cdoc_ced_ruc);
            pdfFormFields.SetField("direccion", fac.ccomdoc.cdoc_direccion);
            pdfFormFields.SetField("telefono", fac.ccomdoc.cdoc_telefono);
            pdfFormFields.SetField("nroguia", fac.ccomenv.cenv_guias.ToString());
            pdfFormFields.SetField("destinatario", fac.ccomenv.cenv_apellidos_des + " " + fac.ccomenv.cenv_nombres_des);
            pdfFormFields.SetField("direcciondes", fac.ccomenv.cenv_direccion_des);
            pdfFormFields.SetField("ciudaddes", fac.ccomenv.cenv_rutadestino);
            pdfFormFields.SetField("remitente", fac.ccomenv.cenv_apellidos_rem + " " + fac.ccomenv.cenv_nombres_rem);
            pdfFormFields.SetField("propietario", fac.ccomenv.cenv_nombres_soc);
            pdfFormFields.SetField("conductor", fac.ccomenv.cenv_nombres_cho);
            pdfFormFields.SetField("vendedor", "CLIENTE: " + pe.per_apellidos + " " + pe.per_nombres + Environment.NewLine + "ROL: " + pe.per_id + Environment.NewLine + "FECHA: " + string.Format("{0} {1:00} del {2:0000}", CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ahora.Month), ahora.Day, ahora.Year));







            decimal total = 0;
            decimal totalcantidad = 0;
            foreach (Dcomdoc item in fac.ccomdoc.detalle)
            {

                total = total + item.ddoc_total;
                totalcantidad = totalcantidad + item.ddoc_cantidad;
                PdfPCell Cell1 = new PdfPCell(new Phrase(item.ddoc_productonombre, fntTableFont));
                Cell1.FixedHeight = 25.0f;
                Table.AddCell(Cell1);
                PdfPCell Cell2 = new PdfPCell(new Phrase(item.ddoc_observaciones, fntTableFont));
                Cell2.FixedHeight = 25.0f;
                Table.AddCell(Cell2);
                PdfPCell Cell3 = new PdfPCell(new Phrase(item.ddoc_cantidad.ToString("0.00"), fntTableFont));
                Cell3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                Cell3.FixedHeight = 25.0f;
                Table.AddCell(Cell3);
                PdfPCell Cell4 = new PdfPCell(new Phrase(item.ddoc_precio.ToString("0.0000000").Replace(',', '.'), fntTableFont));
                Cell4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                Cell4.FixedHeight = 25.0f;
                Table.AddCell(Cell4);
                PdfPCell Cell5 = new PdfPCell(new Phrase(item.ddoc_total.ToString("0.00"), fntTableFont));
                Cell5.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                Cell5.FixedHeight = 25.0f;
                Table.AddCell(Cell5);


            }


            PdfPCell Cellsub = new PdfPCell(new Phrase("SUBTOTAL 0% ", fntTableFontHdr));
            Cellsub.Colspan = 4;
            Cellsub.Border = 0;
            Cellsub.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            Cellsub.FixedHeight = 15.0f;
            Table.AddCell(Cellsub);

            PdfPCell Cell5sub = new PdfPCell(new Phrase(fac.total.tot_subtot_0.ToString("0.00"), fntTableFontHdr));
            Cell5sub.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            Cell5sub.FixedHeight = 15.0f;
            Table.AddCell(Cell5sub);

            PdfPCell Cellsubiva = new PdfPCell(new Phrase("SUBTOTAL I.V.A. ", fntTableFontHdr));
            Cellsubiva.Colspan = 4;
            Cellsubiva.Border = 0;
            Cellsubiva.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            Cellsubiva.FixedHeight = 15.0f;
            Table.AddCell(Cellsubiva);

            PdfPCell Cell5subiva = new PdfPCell(new Phrase(fac.total.tot_subtotal.ToString("0.00"), fntTableFontHdr));
            Cell5subiva.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            Cell5subiva.FixedHeight = 15.0f;
            Table.AddCell(Cell5subiva);

            PdfPCell Celltotaliva = new PdfPCell(new Phrase("TOTAL I.V.A. ", fntTableFontHdr));
            Celltotaliva.Colspan = 4;
            Celltotaliva.Border = 0;
            Celltotaliva.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            Celltotaliva.FixedHeight = 15.0f;
            Table.AddCell(Celltotaliva);

            PdfPCell Cell5totaliva = new PdfPCell(new Phrase(fac.total.tot_timpuesto.ToString("0.00"), fntTableFontHdr));

            Cell5totaliva.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            Cell5totaliva.FixedHeight = 15.0f;
            Table.AddCell(Cell5totaliva);

            PdfPCell Celltotalpag = new PdfPCell(new Phrase("TOTAL $ ", fntTableFontHdr));
            Celltotalpag.Colspan = 4;
            Celltotalpag.Border = 0;
            Celltotalpag.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            Celltotalpag.FixedHeight = 15.0f;
            Table.AddCell(Celltotalpag);

            PdfPCell Cell5totalpag = new PdfPCell(new Phrase(fac.total.tot_total.ToString("0.00"), fntTableFontHdr));
            Cell5totalpag.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            Cell5totalpag.FixedHeight = 15.0f;
            Table.AddCell(Cell5totalpag);






            //PdfPCell Cellt = new PdfPCell(new Phrase("TOTAL", fntTableFontHdr));
            //Cellt.FixedHeight = 25.0f;
            //Table.AddCell(Cellt);
            //PdfPCell Cell2t = new PdfPCell(new Phrase("", fntTableFontHdr));
            //Cell2t.FixedHeight = 25.0f;
            //Table.AddCell(Cell2t);
            //PdfPCell Cell3t = new PdfPCell(new Phrase(totalcantidad.ToString("0.00"), fntTableFontHdr));
            //Cell3t.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            //Cell3t.FixedHeight = 25.0f;
            //Table.AddCell(Cell3t);
            //PdfPCell Cell4t = new PdfPCell(new Phrase("", fntTableFontHdr));
            //Cell4t.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            //Cell4t.FixedHeight = 25.0f;
            //Table.AddCell(Cell4t);
            //PdfPCell Cell5t = new PdfPCell(new Phrase(total.ToString("0.00"), fntTableFontHdr));
            //Cell5t.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            //Cell5t.FixedHeight = 25.0f;
            //Table.AddCell(Cell5t);



            var cb = pdfStamper.GetOverContent(1);
            var ct = new ColumnText(cb);
            ct.Alignment = Element.ALIGN_CENTER;
            ct.SetSimpleColumn(36, 36, PageSize.A4.Width - 36, PageSize.A4.Height - 330);
            ct.AddElement(Table);
            ct.Go();
            pdfFormFields.SetField("totalcantidad", totalcantidad.ToString("0"));
            pdfFormFields.SetField("valordeclarado", (fac.total.tot_vseguro.HasValue) ? fac.total.tot_vseguro.Value.ToString("0.00") : "");
            pdfFormFields.SetField("valorseguro", (fac.total.tot_tseguro.HasValue) ? fac.total.tot_tseguro.Value.ToString("0.00") : "");
            pdfFormFields.SetField("transporte", fac.total.tot_transporte.ToString("0.00"));
            pdfFormFields.SetField("guia", fac.ccomenv.cenv_guia1 + "-" + fac.ccomenv.cenv_guia2 + "-" + fac.ccomenv.cenv_guia3);
            pdfFormFields.SetField("politica", fac.ccomdoc.cdoc_politicanombre);
            pdfFormFields.SetField("entrega", fac.ccomenv.cenv_observacion);
            pdfFormFields.SetField("hojaruta", hojaruta);


            decimal subtotal = fac.total.tot_subtot_0 + fac.total.tot_subtotal;

            pdfFormFields.SetField("subtotal0", fac.total.tot_subtot_0.ToString("0.00"));
            pdfFormFields.SetField("subtotal12", fac.total.tot_subtotal.ToString("0.00"));
            pdfFormFields.SetField("subtotal", subtotal.ToString("0.00"));
            pdfFormFields.SetField("iva", fac.total.tot_porc_impuesto.ToString());
            pdfFormFields.SetField("valoriva", fac.total.tot_timpuesto.ToString("0.00"));
            pdfFormFields.SetField("total", fac.total.tot_total.ToString("0.00"));






            // report by reading values from completed PDF
            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();
            return fac.com_doctran + ".pdf";
        }



        public static string PdfFACTC1(Comprobante fac, Formato formato, string hojaruta, string path, bool autoprint, string impresora)
        {
            string pdfTemplate = path + @"\" + formato.for_pdf;
            string newFile = path + @"\" + fac.com_doctran + ".pdf";

            FileStream fs = new FileStream(newFile, FileMode.Create, FileAccess.Write, FileShare.None);

            Persona pe = PersonaBLL.GetByPK(new Persona { per_codigo = int.Parse((fac.com_agente.HasValue) ? fac.com_agente.ToString() : "0"), per_empresa = int.Parse(fac.com_empresa.ToString()), per_codigo_key = int.Parse((fac.com_agente.HasValue) ? fac.com_agente.ToString() : "0"), per_empresa_key = int.Parse(fac.com_empresa.ToString()) });
            PdfReader pdfReader = new PdfReader(pdfTemplate);
            // PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            PdfStamper pdfStamper = new PdfStamper(pdfReader, fs);
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            //inicio creacion de la tabla

            foreach (KeyValuePair<string, AcroFields.Item> kvp in pdfFormFields.Fields)
            {

                pdfFormFields.SetFieldProperty(kvp.Key, "textsize", 7f, null);

                pdfFormFields.SetFieldProperty(kvp.Key, "setfflags", PdfFormField.MK_CAPTION_LEFT, null);

                pdfFormFields.SetFieldProperty(kvp.Key, "setfflags", PdfFormField.FF_READ_ONLY, null);

                if (kvp.Key.IndexOf("DescripciónRow") >= 0)
                {

                    //pdfFormFields.SetFieldProperty(kvp.Key, "textsize", f, null);

                    pdfFormFields.SetFieldProperty(kvp.Key, "fflags", PdfFormField.FF_MULTILINE, null);

                }
            }
            iTextSharp.text.Font fntTableFontHdr = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font fntTableFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);


            PdfPTable Table = new PdfPTable(5);
            Table.WidthPercentage = 100;
            Table.HorizontalAlignment = 0;
            Table.SpacingAfter = 10;

            float[] sglTblHdWidths = new float[5];
            sglTblHdWidths[0] = 180f;
            sglTblHdWidths[1] = 400f;
            sglTblHdWidths[2] = 100f;
            sglTblHdWidths[3] = 100f;
            sglTblHdWidths[4] = 100f;
            Table.SetWidths(sglTblHdWidths);

            //ENCABEZADO
            PdfPCell CellOneHdr = new PdfPCell(new Phrase("CODIGO", fntTableFontHdr));

            Table.AddCell(CellOneHdr);
            PdfPCell CellTwoHdr = new PdfPCell(new Phrase("DESCRIPCION", fntTableFontHdr));
            CellTwoHdr.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            Table.AddCell(CellTwoHdr);
            PdfPCell CellTreeHdr = new PdfPCell(new Phrase("CANTIDAD", fntTableFontHdr));
            CellTreeHdr.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            Table.AddCell(CellTreeHdr);
            PdfPCell CellOne = new PdfPCell(new Phrase("V.UNITARIO", fntTableFontHdr));
            CellOne.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            Table.AddCell(CellOne);
            PdfPCell CellTwo = new PdfPCell(new Phrase("VALOR TOTAL", fntTableFontHdr));
            CellTwo.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            Table.AddCell(CellTwo);
            //FIN DE ENCABEZADO








            //fin de creacion de tabla


            if (autoprint)
            {
                var writer = pdfStamper.Writer;
                PdfAction js = PdfAction.JavaScript(GetAutoPrintJs(impresora), writer);
                writer.AddJavaScript(js);          //IMPRESION AUTOMATICA
            }

            // set form pdfFormFields
            // The first worksheet and W-4 form
            //pdfFormFields.SetField("numero", fac.com_doctran);

            DateTime ahora = DateTime.Now;
            pdfFormFields.SetField("lugarfecha", string.Format("{0:00}/{1:00}/{2:0000}", ahora.Day, ahora.Month, ahora.Year));
            // pdfFormFields.SetField("fechacrea", string.Format("{0:00}/{1:00}/{2:0000} {3:00}:{4:00}:{5:00}", ahora.Day, ahora.Month, ahora.Year, ahora.Hour, ahora.Minute, ahora.Second));
            //  pdfFormFields.SetField("usuario", fac.crea_usr);
            //pdfFormFields.SetField("nombreusuario", fac.crea_usrnombres);
            pdfFormFields.SetField("placas", fac.com_concepto);
            pdfFormFields.SetField("fecha", fac.com_fecha.ToLongDateString());
            pdfFormFields.SetField("cliente", fac.ccomdoc.cdoc_nombre);
            pdfFormFields.SetField("ruc", fac.ccomdoc.cdoc_ced_ruc);
            pdfFormFields.SetField("direccion", fac.ccomdoc.cdoc_direccion);
            pdfFormFields.SetField("telefono", fac.ccomdoc.cdoc_telefono);
            pdfFormFields.SetField("nroguia", fac.ccomenv.cenv_guias.ToString());
            pdfFormFields.SetField("destinatario", fac.ccomenv.cenv_apellidos_des + " " + fac.ccomenv.cenv_nombres_des);
            pdfFormFields.SetField("direcciondes", fac.ccomenv.cenv_direccion_des);
            pdfFormFields.SetField("ciudaddes", fac.ccomenv.cenv_rutadestino);
            pdfFormFields.SetField("remitente", fac.ccomenv.cenv_apellidos_rem + " " + fac.ccomenv.cenv_nombres_rem);
            pdfFormFields.SetField("propietario", fac.ccomenv.cenv_nombres_soc);
            pdfFormFields.SetField("conductor", fac.ccomenv.cenv_nombres_cho);
            pdfFormFields.SetField("vendedor", pe.per_nombres + " " + pe.per_apellidos);
            pdfFormFields.SetField("ciudad", "Cuenca");







            decimal total = 0;
            decimal totalcantidad = 0;
            foreach (Dcomdoc item in fac.ccomdoc.detalle)
            {

                total = total + item.ddoc_total;
                totalcantidad = totalcantidad + item.ddoc_cantidad;
                PdfPCell Cell1 = new PdfPCell(new Phrase(item.ddoc_productoid, fntTableFont));
                Cell1.FixedHeight = 15.0f;
                Table.AddCell(Cell1);
                PdfPCell Cell2 = new PdfPCell(new Phrase(item.ddoc_productonombre, fntTableFont));
                Cell2.FixedHeight = 15.0f;
                Table.AddCell(Cell2);
                PdfPCell Cell3 = new PdfPCell(new Phrase(item.ddoc_cantidad.ToString("0.00"), fntTableFont));
                Cell3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                Cell3.FixedHeight = 15.0f;
                Table.AddCell(Cell3);
                PdfPCell Cell4 = new PdfPCell(new Phrase(item.ddoc_precio.ToString().Replace(',', '.'), fntTableFont));
                Cell4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                Cell4.FixedHeight = 15.0f;
                Table.AddCell(Cell4);
                PdfPCell Cell5 = new PdfPCell(new Phrase(item.ddoc_total.ToString("0.00"), fntTableFont));
                Cell5.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                Cell5.FixedHeight = 15.0f;
                Table.AddCell(Cell5);


            }

            PdfPCell Cellsub = new PdfPCell(new Phrase("SUBTOTAL 0% ", fntTableFontHdr));
            Cellsub.Colspan = 4;
            Cellsub.Border = 0;
            Cellsub.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            Cellsub.FixedHeight = 15.0f;
            Table.AddCell(Cellsub);
            
            PdfPCell Cell5sub = new PdfPCell(new Phrase(fac.total.tot_subtot_0.ToString("0.00"), fntTableFontHdr));
            Cell5sub.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            Cell5sub.FixedHeight = 15.0f;
            Table.AddCell(Cell5sub);

            PdfPCell Cellsubiva = new PdfPCell(new Phrase("SUBTOTAL I.V.A. ", fntTableFontHdr));
            Cellsubiva.Colspan = 4;
            Cellsubiva.Border = 0;
            Cellsubiva.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            Cellsubiva.FixedHeight = 15.0f;
            Table.AddCell(Cellsubiva);

            PdfPCell Cell5subiva = new PdfPCell(new Phrase(fac.total.tot_subtotal.ToString("0.00"), fntTableFontHdr));
            Cell5subiva.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            Cell5subiva.FixedHeight = 15.0f;
            Table.AddCell(Cell5subiva);

            PdfPCell Celltotaliva = new PdfPCell(new Phrase("TOTAL I.V.A. ", fntTableFontHdr));
            Celltotaliva.Colspan = 4;
            Celltotaliva.Border = 0;
            Celltotaliva.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            Celltotaliva.FixedHeight = 15.0f;
            Table.AddCell(Celltotaliva);

            PdfPCell Cell5totaliva = new PdfPCell(new Phrase(fac.total.tot_timpuesto.ToString("0.00"), fntTableFontHdr));
           
            Cell5totaliva.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            Cell5totaliva.FixedHeight = 15.0f;
            Table.AddCell(Cell5totaliva);

            PdfPCell Celltotalpag = new PdfPCell(new Phrase("TOTAL PAGADO $ ", fntTableFontHdr));
            Celltotalpag.Colspan = 4;
            Celltotalpag.Border = 0;
            Celltotalpag.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            Celltotalpag.FixedHeight = 15.0f;
            Table.AddCell(Celltotalpag);

            PdfPCell Cell5totalpag = new PdfPCell(new Phrase(fac.total.tot_total.ToString("0.00"), fntTableFontHdr));
            Cell5totalpag.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            Cell5totalpag.FixedHeight = 15.0f;
            Table.AddCell(Cell5totalpag);
            


            var cb = pdfStamper.GetOverContent(1);
            var ct = new ColumnText(cb);
            ct.Alignment = Element.ALIGN_CENTER;
            ct.SetSimpleColumn(36, 36, PageSize.A4.Width - 36, PageSize.A4.Height - 330);
            ct.AddElement(Table);
            ct.Go();
            pdfFormFields.SetField("totalcantidad", totalcantidad.ToString("0"));
            pdfFormFields.SetField("valordeclarado", (fac.total.tot_vseguro.HasValue) ? fac.total.tot_vseguro.Value.ToString("0.00") : "");
            pdfFormFields.SetField("valorseguro", (fac.total.tot_tseguro.HasValue) ? fac.total.tot_tseguro.Value.ToString("0.00") : "");
            pdfFormFields.SetField("transporte", fac.total.tot_transporte.ToString("0.00"));
            pdfFormFields.SetField("guia", fac.ccomenv.cenv_guia1 + "-" + fac.ccomenv.cenv_guia2 + "-" + fac.ccomenv.cenv_guia3);
            pdfFormFields.SetField("politica", fac.ccomdoc.cdoc_politicanombre);
            pdfFormFields.SetField("entrega", fac.ccomenv.cenv_observacion);
            pdfFormFields.SetField("hojaruta", hojaruta);


            decimal subtotal = fac.total.tot_subtot_0 + fac.total.tot_subtotal;

           /* pdfFormFields.SetField("subtotal0", fac.total.tot_subtot_0.ToString("0.00"));
            pdfFormFields.SetField("subiva", fac.total.tot_subtotal.ToString("0.00"));
            pdfFormFields.SetField("subtotal", subtotal.ToString("0.00"));
            pdfFormFields.SetField("iva", fac.total.tot_porc_impuesto.ToString());
            pdfFormFields.SetField("totaliva", fac.total.tot_timpuesto.ToString("0.00"));
            pdfFormFields.SetField("total", fac.total.tot_total.ToString("0.00"));
            */





            // report by reading values from completed PDF
            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();
            return fac.com_doctran + ".pdf";
        }




        public static string PdfFACGT(Comprobante fac, Formato formato, string hojaruta, string path, bool autoprint, string impresora)
        {
            Document doc = new Document(PageSize.A4);
            

            string pdfTemplate = path + @"\" + formato.for_pdf;
            string newFile = path + @"\" + fac.com_doctran + ".pdf";

            var output = new FileStream(newFile, FileMode.Create);
            var writer = PdfWriter.GetInstance(doc, output);


            doc.Open();

            iTextSharp.text.Font fntTableFontBold = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font fntTableFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);


            PdfPTable table1 = new PdfPTable(2);
            table1.DefaultCell.Border = 0;
            table1.WidthPercentage = 80;


            PdfPCell cell11 = new PdfPCell();
            cell11.Colspan = 1;
            cell11.AddElement(new Paragraph("Cliente",fntTableFontBold));
            cell11.AddElement(new Paragraph(fac.ccomdoc.cdoc_nombre, fntTableFont));            

            table1.AddCell(cell11);

            doc.Add(table1);

            doc.Close();
            
            return fac.com_doctran + ".pdf";
        }

        public static string PdfFACCOMY(Comprobante fac, Formato formato, string hojaruta, string path, bool autoprint, string impresora, int format)
        {
            string pdfTemplate = path + @"\" + formato.for_pdf;
            string newFile = path + @"\" + fac.com_doctran + ".pdf";

            iTextSharp.text.Font fontnormal = FontFactory.GetFont(BaseFont.COURIER, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font fontbold = FontFactory.GetFont(BaseFont.COURIER, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font fontbig = FontFactory.GetFont(BaseFont.COURIER, 20, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            if (format == 2 || format == 5 || format==8 ||format==9)
            {
                fontnormal = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                fontbold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                fontbig = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 24, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            }
            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;

            foreach (KeyValuePair<string, AcroFields.Item> kvp in pdfFormFields.Fields)
            {
                pdfFormFields.SetFieldProperty(kvp.Key, "textfont", fontnormal.BaseFont, null);
                pdfFormFields.RegenerateField(kvp.Key);               
            }


            if (autoprint)
            {
                var writer = pdfStamper.Writer;
                PdfAction js = PdfAction.JavaScript(GetAutoPrintJs(impresora), writer);
                writer.AddJavaScript(js);          //IMPRESION AUTOMATICA
            }


           


            // set form pdfFormFields
            // The first worksheet and W-4 form
            if (fac.com_tipodoc == Constantes.cFactura.tpd_codigo)
            {
                pdfFormFields.SetField("numerofac", fac.com_doctran);
            }
            else
            {
                pdfFormFields.SetField("numerogui", fac.com_doctran);
                pdfFormFields.SetFieldProperty("numerogui", "textfont", fontbig.BaseFont, null);
                pdfFormFields.RegenerateField("numerogui");
            }


            bool electronico = !string.IsNullOrEmpty(fac.com_claveelec);

            if (electronico)
            {
                pdfFormFields.SetField("numerofac", "");
                pdfFormFields.SetField("electronico", fac.com_doctran);
                pdfFormFields.SetFieldProperty("electronico", "textfont", fontbig.BaseFont, null);
                pdfFormFields.RegenerateField("electronico");
                pdfFormFields.SetField("clave", fac.com_claveelec);
                pdfFormFields.SetField("siac", "");
                pdfFormFields.SetField("almacen", fac.com_almacennombre.ToUpper() + " " + fac.crea_usr);
                pdfFormFields.SetFieldProperty("almacen", "textfont", fontbig.BaseFont, null);
                pdfFormFields.RegenerateField("almacen");

                //pdfFormFields.SetField("propietario", "PROPIETARIO");
                //pdfFormFields.SetField("remite", "REMITENTE");
                //pdfFormFields.SetField("recibi", "RECIBÍ CONFORME");


            }


            DateTime ahora = DateTime.Now;
            pdfFormFields.SetField("fechacrea", string.Format("{0:00}/{1:00}/{2:0000} {3:00}:{4:00}:{5:00}", ahora.Day, ahora.Month, ahora.Year, ahora.Hour, ahora.Minute, ahora.Second));
            pdfFormFields.SetField("usuario", fac.crea_usr);
            pdfFormFields.SetField("nombreusuario", fac.crea_usrnombres);

            pdfFormFields.SetField("fecha", fac.com_fecha.ToShortDateString());
            pdfFormFields.SetFieldProperty("fecha", "textfont", fontbold.BaseFont, null);
            pdfFormFields.RegenerateField("fecha");

            pdfFormFields.SetField("cliente", fac.ccomdoc.cdoc_nombre);
            pdfFormFields.SetField("ruc", fac.ccomdoc.cdoc_ced_ruc);
            pdfFormFields.SetField("direccion", fac.ccomdoc.cdoc_direccion);
            pdfFormFields.SetField("telefono", fac.ccomdoc.cdoc_telefono);

            pdfFormFields.SetField("destinatario", fac.ccomenv.cenv_apellidos_des + " " + fac.ccomenv.cenv_nombres_des);
            pdfFormFields.SetField("rucdes", fac.ccomenv.cenv_ciruc_des);
            pdfFormFields.SetField("telefonodes", fac.ccomenv.cenv_telefono_des);
            //pdfFormFields.SetFieldProperty("destinatario", "textfont", fontnormal.BaseFont, null);
            //pdfFormFields.RegenerateField("destinatario");
            pdfFormFields.SetField("direcciondes", fac.ccomenv.cenv_direccion_des);
            pdfFormFields.SetField("ciudaddes", fac.ccomenv.cenv_rutadestino.ToUpper());
            pdfFormFields.SetFieldProperty("ciudaddes", "textfont", fontbold.BaseFont, null);
            pdfFormFields.RegenerateField("ciudaddes");

            pdfFormFields.SetField("remitente", fac.ccomenv.cenv_apellidos_rem + " " + fac.ccomenv.cenv_nombres_rem);
            pdfFormFields.SetField("rucrem", fac.ccomenv.cenv_ciruc_rem);
            pdfFormFields.SetField("telefonorem", fac.ccomenv.cenv_telefono_rem);
            pdfFormFields.SetField("direccionrem", fac.ccomenv.cenv_direccion_rem);
            pdfFormFields.SetField("origen", fac.ccomenv.cenv_rutaorigen.ToUpper());
            pdfFormFields.SetFieldProperty("origen", "textfont", fontbold.BaseFont, null);
            pdfFormFields.RegenerateField("origen");

            //pdfFormFields.SetFieldProperty("remitente", "textfont", fontnormal.BaseFont, null);
            //pdfFormFields.RegenerateField("remitente");
            if(!string.IsNullOrEmpty(fac.ccomenv.cenv_nombres_soc))
                pdfFormFields.SetField("propietario", fac.ccomenv.cenv_nombres_soc);
            pdfFormFields.SetField("conductor", fac.ccomenv.cenv_nombres_cho);

            decimal totalcantidad = 0;
            string newline = "";
            int cantidadcaracteres = 60;
            foreach (Dcomdoc item in fac.ccomdoc.detalle)
            {
                
                totalcantidad += item.ddoc_cantidad;
                pdfFormFields.SetField("cantidad", pdfFormFields.GetField("cantidad") + newline + item.ddoc_cantidad.ToString("0") + Environment.NewLine);
                string observaciones = item.ddoc_observaciones.Replace('\n',' ');

                int cantlineas = observaciones.Length / cantidadcaracteres;
                pdfFormFields.SetField("descripcion", pdfFormFields.GetField("descripcion") +  observaciones +  Environment.NewLine);

                //int largo = 0;
                //do
                //{
                //    string objsimp = observaciones;
                //    largo = objsimp.Length;
                //    if (largo > 50)
                //    {
                //        objsimp = observaciones.Substring(0, 50);
                //        observaciones = observaciones.Replace(objsimp, "");
                //    }
                //    //pdfFormFields.SetField("descripcion", pdfFormFields.GetField("descripcion") + item.ddoc_productonombre + " " + item.ddoc_observaciones + Environment.NewLine);
                //    pdfFormFields.SetField("descripcion", pdfFormFields.GetField("descripcion") + objsimp + Environment.NewLine);
                //}
                //while (largo > 50);

                bool printprecio = true;
                if (item.ddoc_productototal.HasValue)
                {
                    if (item.ddoc_productototal.Value == 1)
                        printprecio = false;
                }
                if (printprecio)
                {

                    List<Dcalculoprecio> lstdc = DcalculoprecioBLL.GetAll(new WhereParams("dcpr_empresa={0} and dcpr_comprobante={1} and dcpr_dcomdoc={2}", item.ddoc_empresa, item.ddoc_comprobante, item.ddoc_secuencia), "");

                    string peso = "";
                    string preciou = item.ddoc_precio.ToString("0.00");
                    foreach (Dcalculoprecio dc in lstdc)
                    {
                        peso += dc.dcpr_indicedigitado + Environment.NewLine;
                        if (dc.dcpr_valor.HasValue)
                            preciou = dc.dcpr_valor.Value.ToString("0.00") + Environment.NewLine;
                    }


                    pdfFormFields.SetField("peso", pdfFormFields.GetField("peso") + newline + peso + Environment.NewLine);
                    //pdfFormFields.SetField("valorunitario", pdfFormFields.GetField("valorunitario") + item.ddoc_precio.ToString("0.00") + Environment.NewLine);

                    pdfFormFields.SetField("valorunitario", pdfFormFields.GetField("valorunitario") + newline + preciou + Environment.NewLine);
                }
                else
                {
                    pdfFormFields.SetField("valorunitario", pdfFormFields.GetField("valorunitario") + newline  + Environment.NewLine);
                }
                pdfFormFields.SetField("valortotal", pdfFormFields.GetField("valortotal") + newline + item.ddoc_total.ToString("0.00") +Environment.NewLine);

                newline = "";
                int largo = 0;
                do
                {
                    string objsimp = observaciones;
                    largo = objsimp.Length;
                    if (largo > cantidadcaracteres)
                    {
                        objsimp = observaciones.Substring(0, cantidadcaracteres);
                        observaciones = observaciones.Replace(objsimp, "");
                        newline += Environment.NewLine;                        
                    }
                }
                while (largo > cantidadcaracteres);

            }


            //NUEVO CODIGO PARA AGREGAR EL DOMICILIO COMO UN ITEM DEL DETALLE
            if (fac.total.tot_transporte>0 && (format == 8 || format == 9))
            {
                pdfFormFields.SetField("descripcion", pdfFormFields.GetField("descripcion") + "TRASBORDO Y/O ENTREGA DOMICILIO" + Environment.NewLine);
                pdfFormFields.SetField("valorunitario", pdfFormFields.GetField("valorunitario") + newline + fac.total.tot_transporte.ToString("0.00") + Environment.NewLine);
                pdfFormFields.SetField("valortotal", pdfFormFields.GetField("valortotal") + newline + fac.total.tot_transporte.ToString("0.00") + Environment.NewLine);
            }

            pdfFormFields.SetField("totalcantidad", totalcantidad.ToString("0"));
            pdfFormFields.SetField("valordeclarado", (fac.total.tot_vseguro.HasValue) ? fac.total.tot_vseguro.Value.ToString("0.00") : "");

            decimal? valorseguro = null;
            if (fac.total.tot_vseguro.HasValue && fac.total.tot_porc_seguro.HasValue)
                valorseguro = fac.total.tot_vseguro.Value * (fac.total.tot_porc_seguro.Value/100);

            //pdfFormFields.SetField("valorseguro", (fac.total.tot_tseguro.HasValue) ? fac.total.tot_tseguro.Value.ToString("0.00") : "");
            pdfFormFields.SetField("valorseguro", (valorseguro.HasValue) ? valorseguro.Value.ToString("0.00") : "");
            pdfFormFields.SetField("transporte", fac.total.tot_transporte.ToString("0.00"));
            pdfFormFields.SetField("guia", fac.ccomenv.cenv_guia1 + "-" + fac.ccomenv.cenv_guia2 + "-" + fac.ccomenv.cenv_guia3);
            pdfFormFields.SetField("politica", fac.ccomdoc.cdoc_politicanombre);
            pdfFormFields.SetField("monto", fac.total.tot_total.ToString("0.00"));
            pdfFormFields.SetField("entrega", fac.ccomenv.cenv_observacion);
            pdfFormFields.SetField("hojaruta", hojaruta);


            decimal valoriva = Constantes.GetValorIVA(fac.com_fecha);
            if (fac.total.tot_porc_impuesto.HasValue)
                valoriva = fac.total.tot_porc_impuesto.Value;
            pdfFormFields.SetField("imp",valoriva.ToString("0.0") + "%:");
            pdfFormFields.SetFieldProperty("imp", "textfont", fontbold.BaseFont, null);
            decimal subtotal = fac.total.tot_subtot_0 + fac.total.tot_subtotal;

            pdfFormFields.SetField("subtotal0", fac.total.tot_subtot_0.ToString("0.00"));
            pdfFormFields.SetField("subtotal12", fac.total.tot_subtotal.ToString("0.00"));
            pdfFormFields.SetField("subtotal", subtotal.ToString("0.00"));
            pdfFormFields.SetField("iva", fac.total.tot_timpuesto.ToString("0.00"));
            pdfFormFields.SetField("total", fac.total.tot_total.ToString("0.00"));
            pdfFormFields.SetFieldProperty("total", "textfont", fontbold.BaseFont, null);
            pdfFormFields.RegenerateField("total");

            // report by reading values from completed PDF
            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();
            return fac.com_doctran + ".pdf";          
        }


        public static string PdfFACStd(Comprobante fac, Formato formato, string path, bool autoprint, string impresora)
        {
            string pdfTemplate = path + @"\" + formato.for_pdf;
            string newFile = path + @"\" + fac.com_doctran + ".pdf";

            iTextSharp.text.Font fontnormal = FontFactory.GetFont(BaseFont.COURIER, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font fontbold = FontFactory.GetFont(BaseFont.COURIER, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);            
            if (formato.for_pdf.IndexOf("C12")>=0)            
            {
                fontnormal = FontFactory.GetFont(BaseFont.COURIER, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                fontbold = FontFactory.GetFont(BaseFont.COURIER, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            }
            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;

            foreach (KeyValuePair<string, AcroFields.Item> kvp in pdfFormFields.Fields)
            {
                pdfFormFields.SetFieldProperty(kvp.Key, "textfont", fontnormal.BaseFont, null);
                pdfFormFields.RegenerateField(kvp.Key);
            }


            if (autoprint)
            {
                var writer = pdfStamper.Writer;
                PdfAction js = PdfAction.JavaScript(GetAutoPrintJs(impresora), writer);
                writer.AddJavaScript(js);          //IMPRESION AUTOMATICA
            }

            // set form pdfFormFields
            // The first worksheet and W-4 form
            pdfFormFields.SetField("numerofac", fac.com_doctran);
            DateTime ahora = DateTime.Now;
            pdfFormFields.SetField("fechacrea", string.Format("{0:00}/{1:00}/{2:0000} {3:00}:{4:00}:{5:00}", ahora.Day, ahora.Month, ahora.Year, ahora.Hour, ahora.Minute, ahora.Second));
            pdfFormFields.SetField("usuario", fac.crea_usr);

            pdfFormFields.SetField("nombreusuario", fac.crea_usrnombres);

            pdfFormFields.SetField("fecha", fac.com_fecha.ToShortDateString());
            pdfFormFields.SetField("razon", fac.ccomdoc.cdoc_nombre);
            pdfFormFields.SetField("ruc", fac.ccomdoc.cdoc_ced_ruc);
            pdfFormFields.SetField("direccion", fac.ccomdoc.cdoc_direccion);
            pdfFormFields.SetField("telefono", fac.ccomdoc.cdoc_telefono);

            decimal totalcantidad = 0;
            string newline = "";
            int cantidadcaracteres = 60;
            foreach (Dcomdoc item in fac.ccomdoc.detalle)
            {
                pdfFormFields.SetField("codigo", pdfFormFields.GetField("codigo") + newline + item.ddoc_productoid + Environment.NewLine);
                totalcantidad += item.ddoc_cantidad;
                pdfFormFields.SetField("cantidad", pdfFormFields.GetField("cantidad") + newline + item.ddoc_cantidad.ToString("0") + Environment.NewLine);                
                string observaciones = item.ddoc_productonombre + " " + item.ddoc_observaciones.Replace('\n', ' ');
                int cantlineas = observaciones.Length / cantidadcaracteres;
                pdfFormFields.SetField("descripcion", pdfFormFields.GetField("descripcion") + observaciones + Environment.NewLine);
              

                bool printprecio = true;
                //if (item.ddoc_productototal.HasValue)
                //{
                //    if (item.ddoc_productototal.Value == 1)
                //        printprecio = false;
                //}
                if (printprecio)
                {

                    List<Dcalculoprecio> lstdc = DcalculoprecioBLL.GetAll(new WhereParams("dcpr_empresa={0} and dcpr_comprobante={1} and dcpr_dcomdoc={2}", item.ddoc_empresa, item.ddoc_comprobante, item.ddoc_secuencia), "");

                    string peso = "";
                    string preciou = item.ddoc_precio.ToString("0.00");
                    foreach (Dcalculoprecio dc in lstdc)
                    {
                        peso += dc.dcpr_indicedigitado + Environment.NewLine;
                        if (dc.dcpr_valor.HasValue)
                            preciou = dc.dcpr_valor.Value.ToString("0.00") + Environment.NewLine;
                    }


                    pdfFormFields.SetField("peso", pdfFormFields.GetField("peso") + newline + peso + Environment.NewLine);
                    //pdfFormFields.SetField("valorunitario", pdfFormFields.GetField("valorunitario") + item.ddoc_precio.ToString("0.00") + Environment.NewLine);

                    pdfFormFields.SetField("unitario", pdfFormFields.GetField("unitario") + newline + preciou + Environment.NewLine);
                }
                else
                {
                    pdfFormFields.SetField("unitario", pdfFormFields.GetField("unitario") + newline + Environment.NewLine);
                }
                pdfFormFields.SetField("valor", pdfFormFields.GetField("valor") + newline + item.ddoc_total.ToString("0.00") + Environment.NewLine);

                newline = "";
                int largo = 0;
                do
                {
                    string objsimp = observaciones;
                    largo = objsimp.Length;
                    if (largo > cantidadcaracteres)
                    {
                        objsimp = observaciones.Substring(0, cantidadcaracteres);
                        observaciones = observaciones.Replace(objsimp, "");
                        newline += Environment.NewLine;
                    }
                }
                while (largo > cantidadcaracteres);

            }

           

            decimal valoriva = Constantes.GetValorIVA(fac.com_fecha);
            if (fac.total.tot_porc_impuesto.HasValue)
                valoriva = fac.total.tot_porc_impuesto.Value;
            pdfFormFields.SetField("imp", valoriva.ToString("0.0") + "%:");
            pdfFormFields.SetFieldProperty("imp", "textfont", fontbold.BaseFont, null);

            decimal subtotal = fac.total.tot_subtot_0 + fac.total.tot_subtotal;

            pdfFormFields.SetField("subtotal0", fac.total.tot_subtot_0.ToString("0.00"));
            pdfFormFields.SetField("subtotaliva", fac.total.tot_subtotal.ToString("0.00"));
            pdfFormFields.SetField("subtotal", subtotal.ToString("0.00"));
            pdfFormFields.SetField("iva", fac.total.tot_timpuesto.ToString("0.00"));
            pdfFormFields.SetField("total", fac.total.tot_total.ToString("0.00"));
            pdfFormFields.SetFieldProperty("total", "textfont", fontbold.BaseFont, null);
            pdfFormFields.RegenerateField("total");

            // report by reading values from completed PDF
            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();
            return fac.com_doctran + ".pdf";
        }


        public static string PdfFACORTIZ(Comprobante fac, Formato formato, string hojaruta, string path, bool autoprint, string impresora, int format)
        {
            //string pdfTemplate = path + @"\FAC.pdf";
            string pdfTemplate = path + @"\" + formato.for_pdf;
            string newFile = path + @"\" + fac.com_doctran + ".pdf";


            iTextSharp.text.Font fontnormal = FontFactory.GetFont(BaseFont.COURIER, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font fontbold = FontFactory.GetFont(BaseFont.COURIER, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font fontbold1 = FontFactory.GetFont(BaseFont.COURIER, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font fontbig = FontFactory.GetFont(BaseFont.COURIER, 25, iTextSharp.text.Font.BOLD, BaseColor.BLACK);


            Persona per = PersonaBLL.GetByPK(new Persona { per_empresa = fac.com_empresa, per_empresa_key = fac.com_empresa, per_codigo = fac.com_codclipro.Value, per_codigo_key = fac.com_codclipro.Value });

            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;

            foreach (KeyValuePair<string, AcroFields.Item> kvp in pdfFormFields.Fields)
            {
                pdfFormFields.SetFieldProperty(kvp.Key, "textfont", fontnormal.BaseFont, null);
                pdfFormFields.RegenerateField(kvp.Key);
            }
            

            if (autoprint)
            {
                var writer = pdfStamper.Writer;
                PdfAction js = PdfAction.JavaScript(GetAutoPrintJs(impresora), writer);
                writer.AddJavaScript(js);          //IMPRESION AUTOMATICA
            }

            string parametrourl = Constantes.GetParameter("electronicurl");

            // set form pdfFormFields
            // The first worksheet and W-4 form            
            pdfFormFields.SetField("numero", fac.com_doctran);
            pdfFormFields.SetFieldProperty("numero", "textfont", fontbold1.BaseFont, null);
            pdfFormFields.RegenerateField("numero");

            bool electronico = !string.IsNullOrEmpty(fac.com_claveelec);

            if (electronico)
            {
                pdfFormFields.SetField("clave", fac.com_claveelec);
                pdfFormFields.SetField("url", parametrourl);
            }









            DateTime ahora = DateTime.Now;
            pdfFormFields.SetField("fechacrea", string.Format("{0:00}/{1:00}/{2:0000} {3:00}:{4:00}:{5:00}", ahora.Day, ahora.Month, ahora.Year, ahora.Hour, ahora.Minute, ahora.Second));
            pdfFormFields.SetField("usuario", fac.crea_usr);
            pdfFormFields.SetField("nombreusuario", fac.crea_usrnombres);

            pdfFormFields.SetField("fecha", fac.com_fecha.ToLongDateString());
            pdfFormFields.SetField("cliente", fac.ccomdoc.cdoc_nombre);
            pdfFormFields.SetField("ruc", fac.ccomdoc.cdoc_ced_ruc);
            pdfFormFields.SetField("direccion", fac.ccomdoc.cdoc_direccion);
            pdfFormFields.SetField("telefono", fac.ccomdoc.cdoc_telefono);
            pdfFormFields.SetField("email", per.per_mail);

            pdfFormFields.SetField("destinatario", fac.ccomenv.cenv_apellidos_des + " " + fac.ccomenv.cenv_nombres_des);
            pdfFormFields.SetField("direcciondes", fac.ccomenv.cenv_direccion_des);
            pdfFormFields.SetField("ciudaddes", fac.ccomenv.cenv_rutadestino.ToUpper());
            pdfFormFields.SetField("telefonodes", fac.ccomenv.cenv_telefono_des);
            pdfFormFields.SetField("remitente", fac.ccomenv.cenv_apellidos_rem + " " + fac.ccomenv.cenv_nombres_rem);
            pdfFormFields.SetField("propietario", fac.ccomenv.cenv_nombres_soc);
            pdfFormFields.SetField("conductor", fac.ccomenv.cenv_nombres_cho);
            pdfFormFields.SetField("origen", fac.ccomenv.cenv_rutaorigen.ToUpper());

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
            pdfFormFields.SetField("transporte", fac.total.tot_transporte.ToString("0.00"));
            pdfFormFields.SetField("guia", fac.ccomenv.cenv_guia1 + "-" + fac.ccomenv.cenv_guia2 + "-" + fac.ccomenv.cenv_guia3);
            pdfFormFields.SetField("politica", fac.ccomdoc.cdoc_politicanombre);
            pdfFormFields.SetField("entrega", fac.ccomenv.cenv_observacion);
            pdfFormFields.SetField("hojaruta", hojaruta);


            decimal subtotal = fac.total.tot_subtot_0 + fac.total.tot_subtotal;

            pdfFormFields.SetField("subtotal0", fac.total.tot_subtot_0.ToString("0.00"));
            pdfFormFields.SetField("subtotal12", fac.total.tot_subtotal.ToString("0.00"));
            pdfFormFields.SetField("subtotalimp", fac.total.tot_subtotal.ToString("0.00"));
            pdfFormFields.SetField("subtotal", subtotal.ToString("0.00"));
            pdfFormFields.SetField("iva", fac.total.tot_timpuesto.ToString("0.00"));
            pdfFormFields.SetField("total", fac.total.tot_total.ToString("0.00"));
            pdfFormFields.SetFieldProperty("total", "textfont", fontbold1.BaseFont, null);
            pdfFormFields.RegenerateField("total");

            pdfFormFields.SetField("imp", fac.total.tot_porc_impuesto.Value.ToString("0.0"));

            pdfFormFields.SetField("s12", "SUBTOTAL " + fac.total.tot_porc_impuesto.Value.ToString("0.0") + "%:");
            pdfFormFields.SetField("iv", "IVA " + fac.total.tot_porc_impuesto.Value.ToString("0.0") + "%:");
            // report by reading values from completed PDF
            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();
            return fac.com_doctran + ".pdf";
        }



        public static string TicketPDF(int empresa, string comprobante, string path, bool autoprint, string impresora, string dimensiones)
        {
            Comprobante fac = new Comprobante();
            fac.com_empresa_key = empresa;
            fac.com_codigo_key = Int64.Parse(comprobante);
            fac.com_empresa = empresa;
            fac.com_codigo = Int64.Parse(comprobante);
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

            fac.total.tot_subtot_0 += fac.total.tot_transporte;
            fac.total.tot_subtotal += (fac.total.tot_tseguro.HasValue) ? fac.total.tot_tseguro.Value : 0;

            //int codigofac = 1;//Formato para Factura
            //if (format.HasValue)
            //    codigofac = format.Value;

            List<Planillacomprobante> planillacomp = PlanillacomprobanteBLL.GetAll(new WhereParams("pco_empresa={0} and pco_comprobante_fac={1}", fac.com_empresa, fac.com_codigo), "");
            if (planillacomp.Count > 0)
            {
                fac.planillacomp = planillacomp[0];
                //codigofac = 2; //Formato para factura de planilla
            }
            //if (fac.com_tipodoc == Constantes.cGuia.tpd_codigo)
            //    codigofac = 3;//Formato para guia

            //Formato formato = FormatoBLL.GetByPK(new Formato { for_empresa = fac.com_empresa, for_empresa_key = fac.com_empresa, for_codigo = codigofac, for_codigo_key = codigofac });

            fac.rutafactura = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobanteruta = {0} and rfac_empresa = {1}", fac.com_codigo, fac.com_empresa), "");
            fac.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_comprobante = {0} and ddoc_empresa = {1}", fac.com_codigo, fac.com_empresa), "ddoc_secuencia");

            fac.rutafactura = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobantefac = {0} and rfac_empresa = {1}", fac.com_codigo, fac.com_empresa), "");

            string hojaruta = "";
            if (fac.rutafactura.Count > 0)
            {
                Comprobante hr = new Comprobante();
                hr.com_empresa = fac.com_empresa;
                hr.com_empresa_key = fac.com_empresa;
                hr.com_codigo = fac.rutafactura[0].rfac_comprobanteruta;
                hr.com_codigo_key = fac.rutafactura[0].rfac_comprobanteruta;
                hr = ComprobanteBLL.GetByPK(hr);
                hojaruta = hr.com_doctran;
            }

            return  FacturaTicketPDF(fac, hojaruta, path, autoprint, impresora, dimensiones);
            
        }


        public static string ComprobantePDF(int empresa, string comprobante, string path, bool autoprint, string impresora, int? format)
        {

            Comprobante fac = new Comprobante();
            fac.com_empresa_key = empresa;
            fac.com_codigo_key = Int64.Parse(comprobante);
            fac.com_empresa = empresa;
            fac.com_codigo = Int64.Parse(comprobante);
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

            fac.total.tot_subtot_0 += fac.total.tot_transporte;
            fac.total.tot_subtotal += (fac.total.tot_tseguro.HasValue) ? fac.total.tot_tseguro.Value : 0;



            int codigofac = 1;//Formato para Factura
            if (format.HasValue)
                codigofac = format.Value;
            
            List<Planillacomprobante> planillacomp = PlanillacomprobanteBLL.GetAll(new WhereParams("pco_empresa={0} and pco_comprobante_fac={1}", fac.com_empresa, fac.com_codigo), "");
            if (planillacomp.Count > 0)
            {
                fac.planillacomp = planillacomp[0];
                //codigofac = 2; //Formato para factura de planilla
            }
            //if (fac.com_tipodoc == Constantes.cGuia.tpd_codigo)
            //    codigofac = 3;//Formato para guia

            Formato formato = FormatoBLL.GetByPK(new Formato { for_empresa = fac.com_empresa, for_empresa_key = fac.com_empresa, for_codigo = codigofac, for_codigo_key = codigofac });

            fac.rutafactura = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobanteruta = {0} and rfac_empresa = {1}", fac.com_codigo, fac.com_empresa), "");
            fac.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_comprobante = {0} and ddoc_empresa = {1}", fac.com_codigo, fac.com_empresa), "ddoc_secuencia");

            fac.rutafactura = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobantefac = {0} and rfac_empresa = {1}", fac.com_codigo, fac.com_empresa), "");

            string hojaruta = "";
            if (fac.rutafactura.Count > 0)
            {
                Comprobante hr = new Comprobante();
                hr.com_empresa = fac.com_empresa;
                hr.com_empresa_key = fac.com_empresa;
                hr.com_codigo = fac.rutafactura[0].rfac_comprobanteruta;
                hr.com_codigo_key = fac.rutafactura[0].rfac_comprobanteruta;
                hr = ComprobanteBLL.GetByPK(hr);
                hojaruta = hr.com_doctran;
            }




            //if (formato.for_pdf == "FAC.pdf")
            if (formato.for_pdf.IndexOf("TORTIZ") >= 0)
            {
               

                if (formato.for_pdf.IndexOf("3TORTIZ") >= 0)
                    return PdfFACORTIZ(fac, formato, hojaruta, path, autoprint, impresora, formato.for_codigo);
                else
                    return PdfFAC(fac, formato, hojaruta, path, autoprint, impresora, formato.for_codigo);
            }
            


            if (formato.for_pdf == "FACTC.pdf")//Planilla
                return PdfFACTC(fac, formato, hojaruta, path, autoprint, impresora);
            if (formato.for_pdf == "FACTC1.pdf")//Factura normal
                return PdfFACTC1(fac, formato, hojaruta, path, autoprint, impresora);
            if (formato.for_pdf == "FACGT.pdf")
                return PdfFACGT(fac, formato, hojaruta, path, autoprint, impresora);
            //if (formato.for_pdf == "FACCOMY.pdf" || formato.for_pdf=="GUICOMY.pdf")
            if (formato.for_pdf.IndexOf("COMY")>=0 || formato.for_pdf.Contains("TMC"))
                return PdfFACCOMY(fac, formato, hojaruta, path, autoprint, impresora,formato.for_codigo);

            //IMPRESION ESTANDARD
            if (formato.for_pdf == "FACA4C12.pdf" || formato.for_pdf == "FACA4C10.pdf" || formato.for_pdf == "FACA5C12.pdf" || formato.for_pdf == "FACA5C10.pdf" ||  formato.for_pdf == "FACTC2.pdf")
                return PdfFACStd(fac, formato, path, autoprint, impresora);            
            return "";
           


        }


        public static string RetencionPDF(int empresa, string comprobante, string path, bool autoprint, string impresora)
        {

            Comprobante fac = new Comprobante();
            fac.com_empresa_key = empresa;
            fac.com_codigo_key = Int64.Parse(comprobante);
            fac.com_empresa = empresa;
            fac.com_codigo = Int64.Parse(comprobante);
            fac = ComprobanteBLL.GetByPK(fac);

            Persona per = PersonaBLL.GetByPK(new Persona { per_empresa = fac.com_empresa, per_empresa_key = fac.com_empresa, per_codigo = fac.com_codclipro.Value, per_codigo_key = fac.com_codclipro.Value });


            List<vDretencion> lst = vDretencionBLL.GetAll(new WhereParams(" drt_comprobante={0}", fac.com_codigo), "");

            int codigoformato = 4;//Formato para la retencion

            Formato formato = FormatoBLL.GetByPK(new Formato { for_empresa = fac.com_empresa, for_empresa_key = fac.com_empresa, for_codigo = codigoformato, for_codigo_key = codigoformato});
            //string pdfTemplate = path + @"\FAC.pdf";
            string pdfTemplate = path + @"\" + formato.for_pdf;
            string newFile = path + @"\" + fac.com_doctran + ".pdf";

            iTextSharp.text.Font fontnormal = FontFactory.GetFont(BaseFont.COURIER, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font fontbold = FontFactory.GetFont(BaseFont.COURIER, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font fontbig = FontFactory.GetFont(BaseFont.COURIER, 20, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;

            foreach (KeyValuePair<string, AcroFields.Item> kvp in pdfFormFields.Fields)
            {
                pdfFormFields.SetFieldProperty(kvp.Key, "textfont", fontnormal.BaseFont, null);
                pdfFormFields.RegenerateField(kvp.Key);
            }

            if (autoprint)
            {
                var writer = pdfStamper.Writer;
                PdfAction js = PdfAction.JavaScript(GetAutoPrintJs(impresora), writer);
                writer.AddJavaScript(js);          //IMPRESION AUTOMATICA
            }

            // set form pdfFormFields
            // The first worksheet and W-4 form
            pdfFormFields.SetField("numero", fac.com_doctran);

            DateTime ahora = DateTime.Now;
            pdfFormFields.SetField("fechacrea", string.Format("{0:00}/{1:00}/{2:0000} {3:00}:{4:00}:{5:00}", ahora.Day, ahora.Month, ahora.Year, ahora.Hour, ahora.Minute, ahora.Second));
            pdfFormFields.SetField("usuario", fac.crea_usr);
            pdfFormFields.SetField("nombreusuario", fac.crea_usrnombres);

            pdfFormFields.SetField("nombre", per.per_razon);
            pdfFormFields.SetField("ruc", per.per_ciruc);
            pdfFormFields.SetField("direccion", per.per_direccion);


            pdfFormFields.SetField("fecha", fac.com_fecha.ToShortDateString());
            pdfFormFields.SetField("tipo", "FACTURA");
            //pdfFormFields.SetField("nro", "FACTURA");

            string nro = "";
            decimal total = 0;
            foreach (vDretencion item in lst)
            {
                total += item.drt_total.Value;
                nro = item.drt_factura;
                pdfFormFields.SetField("ejercicio", pdfFormFields.GetField("ejercicio") + fac.com_periodo + Environment.NewLine + Environment.NewLine);
                pdfFormFields.SetField("base", pdfFormFields.GetField("base") + item.drt_valor.Value.ToString("0.00") + Environment.NewLine + Environment.NewLine);
                pdfFormFields.SetField("impuesto", pdfFormFields.GetField("impuesto") + ((item.imp_nombre.Length > 25) ? item.imp_nombre.Substring(0, 25) : item.imp_nombre) + Environment.NewLine + Environment.NewLine);
                pdfFormFields.SetField("codigoimp", pdfFormFields.GetField("codigoimp") + item.imp_id+ Environment.NewLine  + Environment.NewLine);
                pdfFormFields.SetField("porcentaje", pdfFormFields.GetField("porcentaje") + item.drt_porcentaje.Value.ToString("0.00") + Environment.NewLine+ Environment.NewLine);
                pdfFormFields.SetField("valor", pdfFormFields.GetField("valor") + item.drt_total.Value.ToString("0.00") + Environment.NewLine+ Environment.NewLine);
                
            }
            pdfFormFields.SetField("nro", nro);
            pdfFormFields.SetField("total", total.ToString("0.00"));
            pdfFormFields.SetFieldProperty("total", "textfont", fontbold.BaseFont, null);
            pdfFormFields.RegenerateField("total");

            // report by reading values from completed PDF
            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();
            return fac.com_doctran + ".pdf";

        }

        public static string GuiaRemisionPDF(int empresa, string comprobante, string path, bool autoprint, string impresora)
        {


            Comprobante grem = new Comprobante();
            grem.com_empresa_key = empresa;
            grem.com_codigo_key = Int64.Parse(comprobante);
            grem.com_empresa = empresa;
            grem.com_codigo = Int64.Parse(comprobante);
            grem = ComprobanteBLL.GetByPK(grem);

            Ccomrem ccomrem = new Ccomrem();
            ccomrem.crem_empresa = grem.com_empresa;
            ccomrem.crem_empresa_key = grem.com_empresa;
            ccomrem.crem_comprobante = grem.com_codigo;
            ccomrem.crem_comprobante_key = grem.com_codigo;
            ccomrem = CcomremBLL.GetByPK(ccomrem);

            Persona per = new Persona();
            Comprobante factura = new Comprobante();
            Ruta ruta = new Ruta();
            if (ccomrem.crem_factura.HasValue)
            {
                factura.com_empresa = grem.com_empresa;
                factura.com_empresa_key = grem.com_empresa;
                factura.com_codigo = ccomrem.crem_factura.Value;
                factura.com_codigo_key = ccomrem.crem_factura.Value;
                factura = ComprobanteBLL.GetByPK(factura);

                per = PersonaBLL.GetByPK(new Persona { per_empresa = factura.com_empresa, per_empresa_key = factura.com_empresa, per_codigo = factura.com_codclipro.Value, per_codigo_key = factura.com_codclipro.Value });

                factura.ccomenv = new Ccomenv();
                factura.ccomenv.cenv_empresa = factura.com_empresa;
                factura.ccomenv.cenv_empresa_key = factura.com_empresa;
                factura.ccomenv.cenv_comprobante = factura.com_codigo;
                factura.ccomenv.cenv_comprobante_key = factura.com_codigo;
                factura.ccomenv = CcomenvBLL.GetByPK(factura.ccomenv);

               
                ruta.rut_empresa = factura.com_empresa;
                ruta.rut_empresa_key = factura.com_empresa;
                ruta.rut_codigo = factura.ccomenv.cenv_ruta.Value;
                ruta.rut_codigo_key = factura.ccomenv.cenv_ruta.Value;
                ruta = RutaBLL.GetByPK(ruta);
            }

            List<Dcomrem> detalle = DcomremBLL.GetAll(new WhereParams("drem_empresa={0} and drem_comprobante={1}", grem.com_empresa, grem.com_codigo), "drem_secuencia");





            //string pdfTemplate = path + @"\FAC.pdf";
            string pdfTemplate = path + @"\GREM.pdf";
            string newFile = path + @"\" + grem.com_doctran + ".pdf";

            iTextSharp.text.Font fontnormal = FontFactory.GetFont(BaseFont.COURIER, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font fontbold = FontFactory.GetFont(BaseFont.COURIER, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font fontbig = FontFactory.GetFont(BaseFont.COURIER, 20, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;

            foreach (KeyValuePair<string, AcroFields.Item> kvp in pdfFormFields.Fields)
            {
                pdfFormFields.SetFieldProperty(kvp.Key, "textfont", fontnormal.BaseFont, null);
                pdfFormFields.RegenerateField(kvp.Key);
            }

            if (autoprint)
            {
                var writer = pdfStamper.Writer;
                PdfAction js = PdfAction.JavaScript(GetAutoPrintJs(impresora), writer);
                writer.AddJavaScript(js);          //IMPRESION AUTOMATICA
            }

            // set form pdfFormFields
            // The first worksheet and W-4 form
            pdfFormFields.SetField("numero", grem.com_doctran);

            DateTime ahora = DateTime.Now;
            pdfFormFields.SetField("fechacrea", string.Format("{0:00}/{1:00}/{2:0000} {3:00}:{4:00}:{5:00}", ahora.Day, ahora.Month, ahora.Year, ahora.Hour, ahora.Minute, ahora.Second));
            pdfFormFields.SetField("usuario", grem.crea_usr);

            pdfFormFields.SetField("cliente", per.per_razon);
            pdfFormFields.SetField("ruc", per.per_ciruc);
            pdfFormFields.SetField("fecha", grem.com_fecha.ToShortDateString());

            pdfFormFields.SetField("destino", ruta.rut_destino);

            pdfFormFields.SetField("fechaini", string.Format("{0:dd/MM/yyyy}", ccomrem.crem_trasladoini));
            pdfFormFields.SetField("fechafin", string.Format("{0:dd/MM/yyyy}", ccomrem.crem_trasladofin));
            pdfFormFields.SetField("comprobante", factura.com_doctran);
            pdfFormFields.SetField("motivo", ccomrem.crem_motivo);




            pdfFormFields.SetField("total", string.Format("{0:0}", detalle.Sum(d => d.drem_cantidad)));
            pdfFormFields.SetFieldProperty("total", "textfont", fontbold.BaseFont, null);
            pdfFormFields.RegenerateField("total");

            // report by reading values from completed PDF
            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();
            return grem.com_doctran + ".pdf";


        }


        public static string ComprobantesHojaRutaPDF(int empresa, string comprobante, string path, bool autoprint, string impresora, string tipo, string dimensiones)
        {
            Empresa emp = EmpresaBLL.GetByPK(new Empresa() { emp_codigo =  empresa, emp_codigo_key = empresa });
            Comprobante hr = ComprobanteBLL.GetByPK(new Comprobante() { com_empresa = empresa, com_empresa_key = empresa, com_codigo = Int64.Parse(comprobante), com_codigo_key = Int64.Parse(comprobante) });

            List<Rutaxfactura> lstrfac = RutaxfacturaBLL.GetAll("rfac_empresa=" + empresa + " and rfac_comprobanteruta=" + comprobante, "");

            string wherein = "";
            foreach (Rutaxfactura item in lstrfac)
            {
                wherein += (wherein != "" ? "," : "") + item.rfac_comprobantefac;
            }


            List<Comprobante> lstcom = ComprobanteBLL.GetAll("com_empresa=" + empresa + " and com_codigo in (" + wherein + ")", "");
            List<Ccomdoc> lstcdoc = CcomdocBLL.GetAll("cdoc_empresa=" + empresa + " and cdoc_comprobante in (" + wherein + ")", "");
            List<Ccomenv> lstcenv = CcomenvBLL.GetAll("cenv_empresa=" + empresa + " and cenv_comprobante in (" + wherein + ")", "");
            List<Dcomdoc> lstddoc = DcomdocBLL.GetAll("ddoc_empresa=" + empresa + " and ddoc_comprobante in (" + wherein + ")", "");
            List<Total> lsttot = TotalBLL.GetAll("tot_empresa=" + empresa + " and tot_comprobante in (" + wherein + ")", "");

            //List<Comprobante> lstcom = ComprobanteBLL.GetAll("com_empresa=" + empresa + " and com_codigo in (select rfac_comprobantefac from rutaxfactura where rfac_comprobanteruta=" + comprobante + ")", "");
            //List<Ccomdoc> lstcdoc = CcomdocBLL.GetAll("cdoc_empresa=" + empresa + " and cdoc_comprobante in (select rfac_comprobantefac from rutaxfactura where rfac_comprobanteruta=" + comprobante + ")", "");
            //List<Ccomenv> lstcenv = CcomenvBLL.GetAll("cenv_empresa=" + empresa + " and cenv_comprobante in (select rfac_comprobantefac from rutaxfactura where rfac_comprobanteruta=" + comprobante + ")", "");
            //List<Dcomdoc> lstddoc = DcomdocBLL.GetAll("ddoc_empresa=" + empresa + " and ddoc_comprobante in (select rfac_comprobantefac from rutaxfactura where rfac_comprobanteruta=" + comprobante + ")", "");
            //List<Total> lsttot = TotalBLL.GetAll("tot_empresa=" + empresa + " and tot_comprobante in (select rfac_comprobantefac from rutaxfactura where rfac_comprobanteruta=" + comprobante + ")", "");

            List<String> hojas = new List<string>(); 
            foreach (Comprobante fac in lstcom)
            {
                fac.ccomdoc = lstcdoc.Find(delegate(Ccomdoc c) { return c.cdoc_comprobante == fac.com_codigo; });
                fac.ccomenv = lstcenv.Find(delegate (Ccomenv c) { return c.cenv_comprobante == fac.com_codigo; });
                fac.total = lsttot.Find(delegate (Total t) { return t.tot_comprobante == fac.com_codigo; });
                fac.ccomdoc.detalle = lstddoc.FindAll(delegate (Dcomdoc d) { return d.ddoc_comprobante == fac.com_codigo; });
                fac.total.tot_subtot_0 += fac.total.tot_transporte;
                fac.total.tot_subtotal += (fac.total.tot_tseguro.HasValue) ? fac.total.tot_tseguro.Value : 0;
                hojas.Add(hr.com_doctran);
            }

            if (tipo=="1")
                return FacturaPDF(emp, lstcom, hojas.ToArray(), path, autoprint, impresora, hr.com_doctran);
            else
                return FacturaTicketPDF(emp,lstcom, hojas.ToArray(), path, autoprint, impresora, hr.com_doctran,dimensiones);







            ////int codigofac = 1;//Formato para Factura
            ////if (format.HasValue)
            ////    codigofac = format.Value;

            //List<Planillacomprobante> planillacomp = PlanillacomprobanteBLL.GetAll(new WhereParams("pco_empresa={0} and pco_comprobante_fac={1}", fac.com_empresa, fac.com_codigo), "");
            //if (planillacomp.Count > 0)
            //{
            //    fac.planillacomp = planillacomp[0];
            //    //codigofac = 2; //Formato para factura de planilla
            //}
            ////if (fac.com_tipodoc == Constantes.cGuia.tpd_codigo)
            ////    codigofac = 3;//Formato para guia

            ////Formato formato = FormatoBLL.GetByPK(new Formato { for_empresa = fac.com_empresa, for_empresa_key = fac.com_empresa, for_codigo = codigofac, for_codigo_key = codigofac });

            //fac.rutafactura = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobanteruta = {0} and rfac_empresa = {1}", fac.com_codigo, fac.com_empresa), "");
            //fac.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_comprobante = {0} and ddoc_empresa = {1}", fac.com_codigo, fac.com_empresa), "ddoc_secuencia");

            //fac.rutafactura = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobantefac = {0} and rfac_empresa = {1}", fac.com_codigo, fac.com_empresa), "");

            //string hojaruta = "";
            //if (fac.rutafactura.Count > 0)
            //{
            //    Comprobante hr = new Comprobante();
            //    hr.com_empresa = fac.com_empresa;
            //    hr.com_empresa_key = fac.com_empresa;
            //    hr.com_codigo = fac.rutafactura[0].rfac_comprobanteruta;
            //    hr.com_codigo_key = fac.rutafactura[0].rfac_comprobanteruta;
            //    hr = ComprobanteBLL.GetByPK(hr);
            //    hojaruta = hr.com_doctran;
            //}

            ////return  FacturaTicketPDF(fac, hojaruta, path, autoprint, impresora, dimensiones);
            //return FacturaPDF(fac, hojaruta, path, autoprint, impresora);
        }

    }
}
