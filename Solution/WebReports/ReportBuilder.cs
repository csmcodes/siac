using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using System.Data; 

namespace WebReports
{
    public class ReportBuilder
    {         

        protected static string reportfolder = "reports/";

        public static void BuildReport(ref ReportViewer reportviewer, string reportcode, params object[] parameters)
        {
            LocalReport rep = reportviewer.LocalReport;
            switch (reportcode)
            {
                case "FAC":
                    rep.ReportPath = reportfolder + "Factura.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.FAC.getFacturaDataTable(long.Parse(parameters[0].ToString()))));
                    break;
                case "RUT":
                    rep.ReportPath = reportfolder + "Report1.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getHojaRutaDataTable(long.Parse(parameters[0].ToString()))));
                    break;
                case "NOTCRE":
                    rep.ReportPath = reportfolder + "Notacre.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getNotaCreditoDataTable(long.Parse(parameters[0].ToString()))));
                    break;
                case "LPER":
                    rep.ReportPath = reportfolder + "Personas.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getPersonas("RUC")));
                   
                    break;
            }
            


        }
    }
}