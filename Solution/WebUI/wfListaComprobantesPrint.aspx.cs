﻿using System;
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
using Services;


namespace WebUI
{
    public partial class wfListaComprobantesPrint : System.Web.UI.Page
    {
        LocalReport rep = new LocalReport();
        private static ReportPrintDocument rp;
        protected static string OrderByClause = "c.com_fecha DESC";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenReport();
            }
        }


        private void GenReport()
        {


            string empresap = Request.QueryString["empresa"] == "NaN" ? "" : Request.QueryString["empresa"];

            Empresa emp = EmpresaBLL.GetByPK(new Empresa { emp_codigo = int.Parse(empresap), emp_codigo_key = int.Parse(empresap) });
            WhereParams parametros = SetWhereClause();
            
            List<vComprobante> lista = vComprobanteBLL.GetAll(parametros, OrderByClause);

            string usr_id = Request.QueryString["usr_id"] == "NaN" ? "" : Request.QueryString["usr_id"];
            string periodo = Request.QueryString["periodo"] == "NaN" ? "" : Request.QueryString["periodo"];
            string mes = Request.QueryString["mes"] == "NaN" ? "" : Request.QueryString["mes"];
            string fecha = Request.QueryString["fecha"] == "NaN" ? "" : Request.QueryString["fecha"];
            string tipos = Request.QueryString["tipos"] == "NaN" ? "" : Request.QueryString["tipos"];

            string wheredocs = "";
            string[] arraytipos = tipos.Split(',');
            foreach (string item in arraytipos)
            {
                if (item.ToString() != "")
                {
                    wheredocs += ((wheredocs != "") ? " or " : "") + " tpd_codigo = " + item.ToString();
                }
            }
            string strdocs = "";
            if (wheredocs != "")
            {

                List<Tipodoc> lst = TipodocBLL.GetAll(wheredocs, "");

                foreach (Tipodoc item in lst)
                {
                    strdocs += ((strdocs != "") ? "," : "") + item.tpd_id;
                }
            }
            else
                strdocs = "TODOS";

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

            rep.ReportPath = "reports/ListadoComprobante.rdlc";
            rep.DataSources.Add(new ReportDataSource("DataSet1", lista));
            rep.SetParameters(new ReportParameter("fecha", fecha));
            rep.SetParameters(new ReportParameter("usuario", usr_id));
            rep.SetParameters(new ReportParameter("comprobantes", strdocs));
            rep.SetParameters(new ReportParameter("periodo", periodo));
            rep.SetParameters(new ReportParameter("mes", mes));
            rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));         
         
            rp = new ReportPrintDocument(rep);
        }

        protected void print(object sender, EventArgs e)
        {
            // ReportPrintDocument rp = new ReportPrintDocument(rep);
            rp.Print();
        }
        //where


        public WhereParams SetWhereClause()
        {
            int tipodocfac = Constantes.cFactura.tpd_codigo;
            int tipodocgui = Constantes.cGuia.tpd_codigo;


            string periodo = Request.QueryString["periodo"] == "NaN" ? "" : Request.QueryString["periodo"];
            string mes = Request.QueryString["mes"] == "NaN" ? "" : Request.QueryString["mes"];
            string fecha = Request.QueryString["fecha"] == "NaN" ? "" : Request.QueryString["fecha"];
            string almacen = Request.QueryString["almacen"] == "NaN" ? "" : Request.QueryString["almacen"];
            string pventa = Request.QueryString["pventa"] == "NaN" ? "" : Request.QueryString["pventa"];
            string numero = Request.QueryString["numero"] == "NaN" ? "" : Request.QueryString["numero"];
            string estado = Request.QueryString["estado"] == "NaN" ? "" : Request.QueryString["estado"];
            string tipodoc = Request.QueryString["tipodoc"] == "NaN" ? "" : Request.QueryString["tipodoc"];
            string concepto = Request.QueryString["concepto"] == "NaN" ? "" : Request.QueryString["concepto"];
            string politica = Request.QueryString["politica"] == "NaN" ? "" : Request.QueryString["politica"];
            string nombres = Request.QueryString["nombres"] == "NaN" ? "" : Request.QueryString["nombres"];
            string placa = Request.QueryString["placa"] == "NaN" ? "" : Request.QueryString["placa"];
            string total = Request.QueryString["total"] == "NaN" ? "" : Request.QueryString["total"];
            string operador = Request.QueryString["operador"] == "NaN" ? "" : Request.QueryString["operador"];
            string estadoenvio = Request.QueryString["estadoenvio"] == "NaN" ? "" : Request.QueryString["estadoenvio"];
            string crea_usr = Request.QueryString["crea_usr"] == "NaN" ? "" : Request.QueryString["crea_usr"];
            string usr_id = Request.QueryString["usr_id"] == "NaN" ? "" : Request.QueryString["usr_id"];            
            string tipos = Request.QueryString["tipos"] == "NaN" ? "" : Request.QueryString["tipos"];
            string empresa = Request.QueryString["empresa"] == "NaN" ? "" : Request.QueryString["empresa"];
            int contador = 0;


            Usuario usr = UsuarioBLL.GetByPK(new Usuario { usr_id = usr_id, usr_id_key = usr_id });
            if (usr.usr_perfil != Constantes.cPerfilAdministrador)
                crea_usr = usr_id;



            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (!string.IsNullOrEmpty(crea_usr))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.crea_usr = {" + contador + "} ";
                valores.Add(crea_usr);
                contador++;
            }

            if (!string.IsNullOrEmpty(periodo))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_periodo = {" + contador + "} ";
                valores.Add(int.Parse(periodo));
                contador++;
            }
            if (!string.IsNullOrEmpty(mes))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_mes = {" + contador + "} ";
                valores.Add(int.Parse(mes));
                contador++;
            }


            if (!string.IsNullOrEmpty(almacen))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                valores.Add(int.Parse(almacen));
                contador++;
            }

            if (!string.IsNullOrEmpty(pventa))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                valores.Add(int.Parse(pventa));
                contador++;

            }
            if (!string.IsNullOrEmpty(numero))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_numero = {" + contador + "} ";
                valores.Add(int.Parse(numero));
                contador++;
            }
            if (!string.IsNullOrEmpty(estado))
            {
                if (int.Parse(estado) > -1)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
                    valores.Add(int.Parse(estado));
                    contador++;
                }
            }
            if (!string.IsNullOrEmpty(tipodoc))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc = {" + contador + "} ";
                valores.Add(int.Parse(tipodoc));
                contador++;
            }

            if (!string.IsNullOrEmpty(politica))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " pol_nombre LIKE {" + contador + "} ";
                valores.Add("%" + politica + "%");
                contador++;
            }

            if (!string.IsNullOrEmpty(concepto))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_concepto like {" + contador + "} ";
                valores.Add("%" + concepto + "%");
                contador++;
            }

            if (!string.IsNullOrEmpty(fecha))
            {
                DateTime f = DateTime.Parse(fecha);
                if (f > DateTime.MinValue)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
                    valores.Add(f);
                    contador++;
                    parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
                    valores.Add(f.AddDays(1));
                    contador++;
                }
            }

            if (!string.IsNullOrEmpty(nombres))//CLIENTE O PROVEEDOR
            {

                //parametros.where += ((parametros.where != "") ? " and " : "") + " (p.per_ciruc ILIKE {" + contador + "} or p.per_nombres ILIKE {" + contador + "} or p.per_apellidos ILIKE{" + contador + "}) ";
                parametros.where += ((parametros.where != "") ? " and " : "") + " (p.per_ciruc ILIKE {" + contador + "} or p.per_nombres ILIKE {" + contador + "} or p.per_apellidos ILIKE{" + contador + "} or e.cenv_ciruc_rem ILIKE {" + contador + "} or e.cenv_nombres_rem ILIKE {" + contador + "} or e.cenv_apellidos_rem ILIKE{" + contador + "} or e.cenv_ciruc_des ILIKE {" + contador + "} or e.cenv_nombres_des ILIKE {" + contador + "} or e.cenv_apellidos_des ILIKE{" + contador + "} or p1.per_apellidos ILIKE {" + contador + "} or p1.per_nombres ILIKE{" + contador + "}) ";
                valores.Add("%" + nombres + "%");
                contador++;
            }

            //if (obj.total.HasValue)
            //{                
            //    parametros.where += ((parametros.where != "") ? " and " : "") + " tot_total "+obj.operador+" {" + contador + "} ";
            //    valores.Add(obj.total);
            //    contador++;
            //}

            if (!string.IsNullOrEmpty(estadoenvio))//ESTADO ENVIO
            {
                if (estadoenvio == "1") //POR COBRAR
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " SUM(ddo_monto) > SUM(ddo_cancela) and c.com_tipodoc= " + tipodocfac;
                }
                if (estadoenvio == "2") //POR DESPACHAR
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " SUM(ddo_monto) = SUM(ddo_cancela) and e.cenv_despachado_ret IS NULL";
                }
                if (estadoenvio == "3")//DESPACHADOS
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " e.cenv_despachado_ret = 1 ";
                }
            }


            if (!string.IsNullOrEmpty(placa)) //VEHICULO
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (e.cenv_placa ILIKE {" + contador + "} or e.cenv_disco ILIKE {" + contador + "}) ";
                valores.Add("%" + placa + "%");
                contador++;
            }

            if (!string.IsNullOrEmpty(usr_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc IN (SELECT udo_tipodoc FROM usrdoc WHERE udo_usuario = {" + contador + "}) ";
                valores.Add(usr_id);
                contador++;
            }
            parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = " + empresa + " ";

            string wheredocs = "";
            string[] arraytipos = tipos.Split(',');
            foreach (string item in arraytipos)
            {
                if (item.ToString() != "")
                {
                    wheredocs += ((wheredocs != "") ? " or " : "") + " c.com_tipodoc = " + item.ToString();
                }
            }
            parametros.where += (wheredocs != "") ? " and (" + wheredocs + ")" : "";

            parametros.valores = valores.ToArray();
            return parametros;
        }











    }
}