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
    public partial class wfPlanillaSocioPrint : System.Web.UI.Page
    {
        LocalReport rep = new LocalReport();
        string empresa;
        private static ReportPrintDocument rp;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                empresa = Request.QueryString["empresa"];
                txtcodigocomp.Text = (Request.QueryString["codigocomp"] != null) ? Request.QueryString["codigocomp"].ToString() : "-1";
                GenReport(txtcodigocomp.Text,empresa);
            }
        }


        private void GenReport(String codigo,String empresa)
        {
            Comprobante planilla = new Comprobante();
            Empresa emp = EmpresaBLL.GetByPK(new Empresa { emp_codigo = int.Parse(empresa), emp_codigo_key = int.Parse(empresa) });
            planilla.com_empresa_key = emp.emp_codigo_key;
            planilla.com_codigo_key = Int64.Parse(codigo);
            planilla.com_empresa = emp.emp_codigo;
            planilla.com_codigo = Int64.Parse(codigo);
            planilla = ComprobanteBLL.GetByPK(planilla);

            Persona cli = new Persona();
            cli.per_empresa = planilla.com_empresa;
            cli.per_empresa_key = planilla.com_empresa;
            if (planilla.com_codclipro.HasValue)
            {
                cli.per_codigo = planilla.com_codclipro.Value;
                cli.per_codigo_key = planilla.com_codclipro.Value;
                cli = PersonaBLL.GetByPK(cli);
            }
            planilla.total = new Total();
            planilla.total.tot_empresa = planilla.com_empresa;
            planilla.total.tot_empresa_key = planilla.com_empresa;
            planilla.total.tot_comprobante = planilla.com_codigo;
            planilla.total.tot_comprobante_key = planilla.com_codigo;
            planilla.total = TotalBLL.GetByPK(planilla.total);


            List<vCancelacion> lst = vCancelacionBLL.GetAll(new WhereParams("dca_empresa={0} and dca_planilla={1}", planilla.com_empresa, planilla.com_codigo), "ddo_doctran");
         //   List<vDcancelacion> planillas = vDcancelacionBLL.GetAllBlock(new WhereParams("dca_planilla={0}", planilla.com_codigo), "");
            //List<vDcancelacion> planillas = vDcancelacionBLL.GetAll(new WhereParams("dca_empresa={0} and dca_planilla={1}",planilla.com_empresa, planilla.com_codigo), "");

            List<Rubrosplanilla> lstpla = RubrosplanillaBLL.GetAll(new WhereParams("rpl_empresa= {0} and rpl_comprobante={1} and rpl_valor>0", planilla.com_empresa, planilla.com_codigo), "");

            decimal ingresos = 0;
            decimal egresos = 0;

            foreach (Rubrosplanilla item in lstpla)
            {
                if (item.rub_tipo == "I")
                    ingresos += item.rpl_valor.Value;
                if (item.rub_tipo == "E")
                    egresos += item.rpl_valor.Value;

            }


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

            rep.ReportPath = "reports/PlanillaSocio.rdlc";
            rep.DataSources.Add(new ReportDataSource("DataSet1", lst));
            rep.DataSources.Add(new ReportDataSource("DataSet2", lstpla));
            rep.SetParameters(new ReportParameter("fecha", planilla.com_fecha.ToShortDateString()));
            rep.SetParameters(new ReportParameter("doctran", planilla.com_doctran));
            rep.SetParameters(new ReportParameter("socio", cli.per_apellidos + " " + cli.per_nombres));
            rep.SetParameters(new ReportParameter("ingresos", ingresos.ToString()));
            rep.SetParameters(new ReportParameter("egresos", egresos.ToString()));
            rep.SetParameters(new ReportParameter("total", planilla.total.tot_total.ToString()));
            rep.SetParameters(new ReportParameter("usuario", planilla.crea_usrnombres));
            rep.SetParameters(new ReportParameter("concepto", planilla.com_concepto));
            rep.SetParameters(new ReportParameter("empresa",emp.emp_nombre));
            rep.SetParameters(new ReportParameter("vehiculo", lst.Count > 0 ? "Placa:" + lst[0].placa + " Disco:" + lst[0].disco : ""));

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