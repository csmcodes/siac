using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;


namespace WebReports
{
    public class PrintReport
    {
        public string deviceInfo { get; set; }
        public LocalReport report { get; set; }

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding,
                             string mimeType, bool willSeek)
        {
            Stream stream = new FileStream(name + "." + fileNameExtension, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }

        public void Export(LocalReport report)
        {
            
            string deviceInfo =
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
              "  <PageWidth>8.5in</PageWidth>" +
              "  <PageHeight>11in</PageHeight>" +
              "  <MarginTop>0.25in</MarginTop>" +
              "  <MarginLeft>0.25in</MarginLeft>" +
              "  <MarginRight>0.25in</MarginRight>" +
              "  <MarginBottom>0.25in</MarginBottom>" +
              "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);

            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }


        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);

            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public void Print()
        {
            const string printerName = "Microsoft Office Document Image Writer";

            if (m_streams == null || m_streams.Count == 0)
                return;

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = printerName;
            if (!printDoc.PrinterSettings.IsValid)
            {
                string msg = String.Format("Can't find printer \"{0}\".", printerName);
                Console.WriteLine(msg);
                return;
            }
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.Print();
        }

    }
}