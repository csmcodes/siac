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
using Services;
using PrintReportSample;

namespace WebReports
{
    public partial class repPrint : System.Web.UI.Page
    {

        protected string report;
        

        private static ReportPrintDocument rp;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                report = Request.QueryString["report"];

                bool autoprint = false;
                bool.TryParse(Request.QueryString["autoprint"], out autoprint);                  
                txtparameter1.Text = (Request.QueryString["parameter1"] != null) ? Request.QueryString["parameter1"].ToString() : "";               
                txtparameter2.Text = (Request.QueryString["parameter2"] != null) ? Request.QueryString["parameter2"].ToString() : "";
                txtparameter3.Text = (Request.QueryString["parameter3"] != null) ? Request.QueryString["parameter3"].ToString() : "";

                GenReport(report, autoprint);
            }
        }


        private void GenReport(String codigo, bool autoprint)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.Reset();          
            ReportViewer1.Visible = true;
            ReportBuilder.BuildReport(ref ReportViewer1, codigo, txtparameter1.Text,txtparameter2.Text, txtparameter3.Text);
            rp = new ReportPrintDocument(ReportViewer1.LocalReport);

            
            //if (autoprint)
                rp.Print();
           
        }


        protected void print(object sender, EventArgs e)
        {
            rp.Print(); 
        }
    }
}