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
using Packages;
using Functions;
using PrintReportSample;

namespace WebUI
{
    public partial class wfBalanceGeneralPrint : System.Web.UI.Page
    {
        LocalReport rep = new LocalReport();
        protected static WhereParams parametros = new WhereParams();
        protected static string OrderByClause = "cue_id";
        protected static int debcre =0;
        private static ReportPrintDocument rp;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtcodpersona.Text = (Request.QueryString["codcuenta"] != null) ? Request.QueryString["codcuenta"].ToString() : "-1";
                txtdebcre.Text = (Request.QueryString["debcre"] != null) ? Request.QueryString["debcre"].ToString() : "-1";
                txtfechacorte.Text = (Request.QueryString["fechacort"] != null) ? Request.QueryString["fechacort"].ToString() : "-1";
                txtcodigoalm.Text = (Request.QueryString["codalmacen"] != null) ? Request.QueryString["codalmacen"].ToString() : "-1";
                txtnivel.Text = (Request.QueryString["nivel"] != null) ? Request.QueryString["nivel"].ToString() : "-1";
                txtmovimiento.Text = (Request.QueryString["movimiento"] != null) ? Request.QueryString["movimiento"].ToString() : "-1";
                txtmodulo.Text = (Request.QueryString["modulo"] != null) ? Request.QueryString["modulo"].ToString() : "-1";
                debcre = (Int32)Conversiones.GetValueByType(txtdebcre.Text, typeof(Int32));
                string emp = (Request.QueryString["empresa"] != null) ? Request.QueryString["empresa"].ToString() : "-1";
                //Empresa emp = (Empresa)HttpContext.Current.Session["empresa"];
                GenReport(txtcodpersona.Text, txtmodulo.Text, txtmovimiento.Text, txtnivel.Text, txtcodigoalm.Text, txtfechacorte.Text,int.Parse(emp));
            }
        }

        public static void SetWhereClause(vCuentasSaldos obj)
        {
            if (obj != null)
            {
                int contador = 0;
                parametros = new WhereParams();
                List<object> valores = new List<object>();
                if (debcre == 1)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " (cue_genero >= {" + contador + "} ";
                    valores.Add(1);
                    contador++;
                    
                    parametros.where += ((parametros.where != "") ? " and " : "") + " cue_genero <= {" + contador + "} )";
                    valores.Add(3);
                    contador++;
                }
                if (debcre == 2)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " (cue_genero >= {" + contador + "} ";
                    valores.Add(4);
                    contador++;                  
                    parametros.where += ((parametros.where != "") ? " and " : "") + " cue_genero <= {" + contador + "} )";
                    valores.Add(7);
                    contador++;
                }
                if (obj.cue_nivel > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " cue_nivel <= {" + contador + "} ";
                    valores.Add(obj.cue_nivel);
                    contador++;
                }
                if (obj.cue_movimiento > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " cue_movimiento = {" + contador + "} ";
                    valores.Add(obj.cue_movimiento);
                    contador++;
                }
                if (obj.cue_modulo > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " cue_modulo = {" + contador + "} ";
                    valores.Add(obj.cue_modulo);
                    contador++;
                }
                if (obj.cue_empresa > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " cue_empresa = {" + contador + "} ";
                    valores.Add(obj.cue_empresa);
                    contador++;
                }       
                parametros.valores = valores.ToArray();
            }
        }
        private void GenReport(String codigo, String modulo, String movimiento, String nivel,String almacen,String fecha,int emp_codigo)
        {
            vCuentasSaldos estadocuenta = new vCuentasSaldos();
            estadocuenta.cue_codigo =(Int32)Conversiones.GetValueByType(codigo, typeof(Int32));
            estadocuenta.cue_modulo =  (Int32)Conversiones.GetValueByType(modulo, typeof(Int32));
            estadocuenta.cue_movimiento =  (Int32)Conversiones.GetValueByType(movimiento, typeof(Int32));
            estadocuenta.cue_nivel = (Int32)Conversiones.GetValueByType(nivel, typeof(Int32));
            estadocuenta.cue_empresa = emp_codigo;
            int? codalm = (Int32)Conversiones.GetValueByType(almacen, typeof(Int32));
            DateTime? fechbalance = (String.IsNullOrEmpty(fecha))?DateTime.Now:Convert.ToDateTime(fecha);              
            SetWhereClause(estadocuenta);
            List<vCuentasSaldos> planillas = vCuentasSaldosBLL.GetAll(parametros, OrderByClause);
            //List<Cuenta> lst = CNT.getBalance(emp_codigo, codalm.Value, null, null, fechbalance.Value, "m", 3);
            List<Cuenta> lst = CNT.getBalanceNew(emp_codigo, codalm.Value, null, null, fechbalance.Value, "m", 3,true,false);
            foreach (vCuentasSaldos item in planillas)
            {
                for (int i = 1; i <= item.cue_nivel; i++)
                {
                    item.cue_nombre = "     " + item.cue_nombre;
                }
                Cuenta cta = lst.Find(delegate(Cuenta c) { return c.cue_codigo == item.cue_codigo; });            
                item.cue_saldo = cta.final;              
            }
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.Reset();
            ReportViewer1.Visible = true;
            rep = this.ReportViewer1.LocalReport;
            rep.ReportPath = "reports/Estado de resultados.rdlc";
            rep.DataSources.Add(new ReportDataSource("DataSet1", planillas));
            rp = new ReportPrintDocument(rep);
            //    rp.Print(); 
        }


        protected void print(object sender, EventArgs e)
        {
            // ReportPrintDocument rp = new ReportPrintDocument(rep);
            rp.Print();
        }
    }
}