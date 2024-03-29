﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using BusinessLogicLayer;
using System.Web.Services;
using System.Text;
using System.Data;
using Services;
using System.Web.Script.Serialization;
using System.Collections;
using HtmlObjects;
using Functions;
using Packages;
namespace WebUI
{
    public partial class wfEstadoCuentanew : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "cpr_nombre";
        protected static string WhereClause = "";
        protected static WhereParams parametros;
        protected static int? debcre; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
                txtdebcre.Text = (Request.QueryString["debcre"] != null) ? Request.QueryString["debcre"].ToString() : "-1";
                debcre = Convert.ToInt32(txtdebcre.Text);
            }
        }


        [WebMethod]
        public static string GetCabecera()
        {
            
            
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(2, 2);

            tdatos.rows[0].cells[0].valor = "Alamcen:";
            tdatos.rows[0].cells[1].valor = new Select { id = "cmbALMACEN", clase = Css.large, withempty = true, diccionario = Dictionaries.GetAlmacen() }.ToString();
            if (debcre == 1)
            {
                tdatos.rows[1].cells[0].valor = "Cliente:";
                tdatos.rows[1].cells[1].valor = new Input { id = "cmbNOMBRE", clase = Css.large, autocomplete = "GetClienteObj" }.ToString() + "" + new Input { id = "txtCODPROVEE", visible = false }.ToString(); ;
            }
            else
            {
                tdatos.rows[1].cells[0].valor = "Provedor:";
                tdatos.rows[1].cells[1].valor = new Input { id = "cmbNOMBRE", clase = Css.large, autocomplete = "GetProveedorObj" }.ToString() + "" + new Input { id = "txtCODPROVEE", visible = false }.ToString(); ;
            }
            
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");

            html.AppendLine("<div class=\"span6\">");
            tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(1, 2);
            tdatos.rows[0].cells[0].valor = "Fecha Corte:";
            tdatos.rows[0].cells[1].valor = new Input { id = "cmbCORTE", datepicker = true, datetimevalor = DateTime.Now, clase = Css.large }.ToString() + " " + new Input { id = "txtDEBCRE", visible = false }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");

            html.AppendLine("</div><!--row-fluid-->");
            

            return html.ToString();
        }


        [WebMethod]
        public static string GetDetalle()
        {
            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tddatos";

            tdatos.invoice = true;
            tdatos.clase = "scrolltable";
            if (debcre == Constantes.cDebito)
            {
                tdatos.AddColumn("ID Cliente", "", "", "");
                tdatos.AddColumn("Cliente", "", "", "");
            }
            else
            {
                tdatos.AddColumn("ID Provedor", "", "", "");
                tdatos.AddColumn("Provedor", "", "", "");
            }
                

            tdatos.AddColumn("Cupo", "", "", "");
            tdatos.AddColumn("Valor", "", Css.right, "");            

            tdatos.editable = false;
           
            html.AppendLine(tdatos.ToString());
            return html.ToString();
        }

        [WebMethod]
        public static string GetDetalleData(object objeto)
        {

            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object obj_per_codigo = null;
                object obj_com_almacen = null;
                object obj_ddo_fecha_emi = null;
                object obj_ddo_debcre = null;
                object obj_empresa = null;


                tmp.TryGetValue("per_codigo", out obj_per_codigo);
                tmp.TryGetValue("com_almacen", out obj_com_almacen);
                tmp.TryGetValue("ddo_fecha_emi", out obj_ddo_fecha_emi);
                tmp.TryGetValue("ddo_debcre", out obj_ddo_debcre);
                tmp.TryGetValue("empresa", out obj_empresa);



                int? per_codigo = Convert.ToInt32(obj_per_codigo);
                int? com_almacen = Convert.ToInt32(obj_com_almacen);
                DateTime? ddo_fecha_emi = Convert.ToDateTime(obj_ddo_fecha_emi);
                int? ddo_debcre = Convert.ToInt32(obj_ddo_debcre);
                int? empresa = Convert.ToInt32(obj_empresa);


                int contador = 0;
                parametros = new WhereParams();
                List<object> valores = new List<object>();
                if (per_codigo > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " per_codigo = {" + contador + "} ";
                    valores.Add(per_codigo);
                    contador++;
                }
                if (com_almacen > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_almacen = {" + contador + "} ";
                    valores.Add(com_almacen);
                    contador++;
                }
                if (ddo_fecha_emi.HasValue)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " ddo_fecha_emi <= {" + contador + "} ";
                    valores.Add(ddo_fecha_emi);
                    contador++;
                }
                if (empresa>0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + "ddo_empresa = {" + contador + "} ";
                    valores.Add(empresa);
                    contador++;
                }
                if (ddo_debcre.Value==Constantes.cDebito)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " pxt_tipo = {" + contador + "} ";
                    valores.Add(4);
                    contador++;

                    //parametros.where += ((parametros.where != "") ? " and " : "") + " com_tclipro = {" + contador + "} ";
                    //valores.Add(4);
                    //contador++;

                }

                if (ddo_debcre.Value == Constantes.cCredito)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " pxt_tipo = {" + contador + "} ";
                    valores.Add(5);
                    contador++;

                    //parametros.where += ((parametros.where != "") ? " and " : "") + " com_tclipro = {" + contador + "} ";
                    //valores.Add(5);
                    //contador++;


                }


               


                parametros.valores = valores.ToArray();
            }

            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;

            StringBuilder html = new StringBuilder();

            /*List<vEstadoCuenta> lista  =  vEstadoCuentaBLL.GetAll( parametros, "");
            
            

              foreach (vEstadoCuenta item in lista)
            {
                ArrayList array = new ArrayList();
         
                array.Add(item.per_id);
                array.Add(item.per_razon);
                array.Add(item.per_cupo);
                array.Add(item.valor);
                string strid = "{\"per_codigo\":\"" + item.per_codigo + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));
            }         */

            return html.ToString();
        }


        [WebMethod]
        public static string GetGuias(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);

            string whereguias = "";
            foreach (Planillacli plc in comprobante.planillas)
            {
                whereguias += ((whereguias != "") ? " and " : "") + " com_codigo != " + plc.plc_comprobante;
            }
            if (whereguias != "")
                whereguias = " and " + whereguias;


            StringBuilder html = new StringBuilder();

            html.AppendLine("<div id=\"barracomp\">");
            html.AppendLine("<ul class=\"list-nostyle list-inline\">");
           // html.AppendLine("<li><div class=\"btn\" id=\"alldet_P\"><i class=\"iconfa-check\"></i> &nbsp; Seleccionar Todos</div></li>");
        //    html.AppendLine("<li><div class=\"btn\" id=\"nonedet_P\"><i class=\"iconfa-check-empty\"></i> &nbsp; Limpiar Selección</div></li>");
            html.AppendLine("</ul>");
            html.AppendLine("</div>");



            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span11\">");

            HtmlTable tdatos = new HtmlTable();


            tdatos.id = "tddatos_P";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";

            tdatos.AddColumn("Fecha", "width10", "", "");
            tdatos.AddColumn("Socio", "width20", "", "");
            tdatos.AddColumn("Comprobante", "width20", "", "");
            tdatos.AddColumn("Valor", "width5", "");

            //tdatos.editable = true;
            List<vGuias> lst = vGuiasBLL.GetAll(new WhereParams("com_empresa={0} and com_codclipro={1} and com_tipodoc ={2} and plc_empresa is null " + whereguias, comprobante.com_empresa, comprobante.com_codclipro, 13), "com_fecha");
            foreach (vGuias item in lst)
            {
                HtmlRow row = new HtmlRow();
                row.markable = true;
                row.data = "data-comprobante=" + item.com_codigo;
                row.cells.Add(new HtmlCell { valor = item.com_fecha.Value.ToShortDateString() });
                row.cells.Add(new HtmlCell { valor = item.per_apellidos + " " + item.per_nombres });
                row.cells.Add(new HtmlCell { valor = item.com_doctran });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.tot_total), clase = Css.right });
                tdatos.AddRow(row);

            }
            html.AppendLine(tdatos.ToString());

            html.AppendLine(" </div><!--span11-->");
            html.AppendLine("</div><!--row-fluid-->");


            return html.ToString();
        }

        [WebMethod]
        public static string GetPie(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);


            StringBuilder html = new StringBuilder();

            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(2, 2);
            tdatos1.rows[0].cells[0].valor = "Registros:";
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtREGISTROS", clase = Css.medium + Css.amount, habilitado = false, valor = 0 }.ToString();
            tdatos1.rows[1].cells[0].valor = "TOTAL:";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtTOTALCOM", clase = Css.medium + Css.totalamount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_total) }.ToString();

            html.AppendLine(tdatos1.ToString());

            return html.ToString();

        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Comprobante obj = new Comprobante(objeto);
            if (obj.com_codigo > 0)
            {
                return UpdateComprobante(obj);
            }
            else
            {
                return InsertComprobante(obj);
                
            }
        }

        public static string InsertComprobante(Comprobante obj)
        {
            DateTime fecha = DateTime.Now;
            #region Actualiza el numero de comprobante en 1
            Dtipocom dti = new Dtipocom();
            dti.dti_empresa = obj.com_empresa;
            dti.dti_empresa_key = obj.com_empresa;
            dti.dti_periodo = fecha.Year;
            dti.dti_periodo_key = fecha.Year;
            dti.dti_ctipocom = obj.com_ctipocom;
            dti.dti_ctipocom_key = obj.com_ctipocom;
            dti.dti_almacen = obj.com_almacen.Value;
            dti.dti_almacen_key = obj.com_almacen.Value;
            dti.dti_puntoventa = obj.com_pventa.Value;
            dti.dti_puntoventa_key = obj.com_pventa.Value;
            dti = DtipocomBLL.GetByPK(dti);
            dti.dti_numero = dti.dti_numero.Value + 1;
            #endregion

            Persona per = PersonaBLL.GetByPK(new Persona { per_empresa = obj.com_empresa, per_empresa_key = obj.com_empresa, per_codigo = obj.com_codclipro.Value, per_codigo_key = obj.com_codclipro.Value });

            obj.com_numero = dti.dti_numero.Value;
            obj.com_concepto = "PLANILLA CLIENTE " + per.per_apellidos + " " + per.per_nombres;
            obj.com_modulo = General.GetModulo(obj.com_tipodoc); 
            obj.com_transacc = General.GetTransacc(obj.com_tipodoc);
            obj.com_nocontable = 1;//HAY QUE DEFINIR

            obj.com_descuadre = 0;
            obj.com_adestino = 0;
            obj.com_doctran = General.GetNumeroComprobante(obj);


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                obj.com_codigo = ComprobanteBLL.InsertIdentity(transaction, obj);

                obj.total.tot_comprobante = obj.com_codigo;
                TotalBLL.Insert(transaction, obj.total);

                foreach (Planillacli item in obj.planillas)
                {
                    item.plc_comprobante_pla = obj.com_codigo;
                    PlanillacliBLL.Insert(transaction, item);
                }
                DtipocomBLL.Update(transaction, dti);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return "-1";
            }
            return obj.com_codigo.ToString();
        }


        public static string UpdateComprobante(Comprobante obj)
        {

            DateTime fecha = DateTime.Now;

            obj.com_empresa_key = obj.com_empresa;
            obj.com_codigo_key = obj.com_codigo;
            Comprobante objU = ComprobanteBLL.GetByPK(obj);
            objU.com_empresa_key = objU.com_empresa;
            objU.com_codigo_key = objU.com_codigo;

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                objU.com_fecha = obj.com_fecha;
                objU.com_codclipro = obj.com_codclipro;
                objU.com_agente = obj.com_agente;
                objU.com_estado = obj.com_estado;//ACTUALIZA EL ESTADO DEL COMPROBANTE

                Persona per = PersonaBLL.GetByPK(new Persona { per_empresa = obj.com_empresa, per_empresa_key = obj.com_empresa, per_codigo = obj.com_codclipro.Value, per_codigo_key = obj.com_codclipro.Value });
                objU.com_concepto = "PLANILLA CLIENTE " + per.per_apellidos + " " + per.per_nombres;


                ComprobanteBLL.Update(transaction, objU);

                TotalBLL.Update(transaction, obj.total);

                List<Planillacli> lst = PlanillacliBLL.GetAll(new WhereParams("plc_empresa ={0} and plc_comprobante_pla = {1} ", obj.com_empresa, obj.com_codigo), "");
                foreach (Planillacli item in lst)
                {
                    PlanillacliBLL.Delete(transaction, item);
                }

                foreach (Planillacli item in obj.planillas)
                {
                    item.plc_comprobante_pla = obj.com_codigo;
                    PlanillacliBLL.Insert(transaction, item);
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return "-1";
            }
            return obj.com_codigo.ToString();
        }
    }
}