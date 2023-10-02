using System;
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
    public partial class wfNotaCredito : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "dcab_nombre";
        protected static string WhereClause = "";
        protected static WhereParams parametros;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txttipodoc.Text = (Request.QueryString["tipodoc"] != null) ? Request.QueryString["tipodoc"].ToString() : "-1";
                Tipodoc tdoc = TipodocBLL.GetByPK(new Tipodoc { tpd_codigo = int.Parse(txttipodoc.Text), tpd_codigo_key = int.Parse(txttipodoc.Text) });
                if (tdoc.tpd_nocontable.HasValue)
                    txtnocontable.Text = tdoc.tpd_nocontable.Value.ToString();
                else
                    txtnocontable.Text = "0";
                txtcodigocomp.Text = (Request.QueryString["codigocomp"] != null) ? Request.QueryString["codigocomp"].ToString() : "-1";
                txtorigen.Text = (Request.QueryString["origen"] != null) ? Request.QueryString["origen"].ToString() : "";
                txtcodigocompref.Text = (Request.QueryString["codigocompref"] != null) ? Request.QueryString["codigocompref"].ToString() : "-1";
                pageIndex = 1;
                pageSize = 20;
            }

        }


        [WebMethod]
        public static string GetAutorizaciones(object objeto)
        {
            Comprobante comp = new Comprobante(objeto);

            if (comp.com_tipodoc == Constantes.cLiquidacionCompra.tpd_codigo)
            {
                Empresa emp = new Empresa();
                emp.emp_codigo_key = comp.com_empresa;
                emp = EmpresaBLL.GetByPK(emp);

                Ctipocom ctipocom = new Ctipocom();
                ctipocom.cti_empresa = comp.com_empresa;
                ctipocom.cti_empresa_key = comp.com_empresa;
                ctipocom.cti_codigo = comp.com_ctipocom;
                ctipocom.cti_codigo_key = comp.com_ctipocom;

                ctipocom = CtipocomBLL.GetByPK(ctipocom);

                return new Select { id = "cmbAUTORIZACION", clase = Css.medium, diccionario = (emp.emp_informante.HasValue) ? Dictionaries.GetAutorizacionesPersonaByRetdato(comp.com_empresa, emp.emp_informante.Value, ctipocom.cti_retdato.Value) : Dictionaries.Empty() }.ToString();
            }
            else
                return new Select { id = "cmbAUTORIZACION", clase = Css.medium, diccionario = (comp.com_codclipro.HasValue) ? Dictionaries.GetAutorizacionesPersona(comp.com_empresa, comp.com_codclipro.Value) : Dictionaries.Empty() }.ToString();
            
        }

        [WebMethod]
        public static string GetAutorizacionData(object objeto)
        {
            Comprobante comp = new Comprobante(objeto);
            Autpersona aut = new Autpersona(objeto);

            if (comp.com_tipodoc == Constantes.cLiquidacionCompra.tpd_codigo)
            {
                Empresa emp = new Empresa();
                emp.emp_codigo_key = comp.com_empresa;
                emp = EmpresaBLL.GetByPK(emp);
                aut.ape_persona = emp.emp_informante;
            }

            List<Autpersona> autorizaciones = AutpersonaBLL.GetAll(new WhereParams("ape_empresa={0} and ape_persona={1} and ape_nro_autoriza= {2} and ape_fact1 = {3} and ape_fact2 = {4} and ape_retdato = {5} ", aut.ape_empresa, aut.ape_persona, aut.ape_nro_autoriza, aut.ape_fact1, aut.ape_fact2, aut.ape_retdato), "");
            if (autorizaciones.Count > 0)
                return new JavaScriptSerializer().Serialize(autorizaciones[0]);
            else
                return "";

        }

        [WebMethod]
        public static string GetCabecera(object objeto)
        {


            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);

            comprobante.ccomdoc = new Ccomdoc();
            comprobante.ccomdoc.cdoc_empresa = comprobante.com_empresa;
            comprobante.ccomdoc.cdoc_empresa_key = comprobante.com_empresa;
            comprobante.ccomdoc.cdoc_comprobante = comprobante.com_codigo;
            comprobante.ccomdoc.cdoc_comprobante_key = comprobante.com_codigo;
            comprobante.ccomdoc = CcomdocBLL.GetByPK(comprobante.ccomdoc);

            comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);

            Persona persona = new Persona();
            persona.per_empresa = comprobante.com_empresa;
            persona.per_empresa_key = comprobante.com_empresa;
            if (comprobante.com_codclipro.HasValue)
            {
                persona.per_codigo = comprobante.com_codclipro.Value;
                persona.per_codigo_key = comprobante.com_codclipro.Value;
                persona = PersonaBLL.GetByPK(persona);
            }

            Politica politica = new Politica();

            politica.pol_empresa = comprobante.com_empresa;
            politica.pol_empresa_key = comprobante.com_empresa;
            if (comprobante.ccomdoc.cdoc_politica.HasValue)
            {
                politica.pol_codigo = comprobante.ccomdoc.cdoc_politica.Value;
                politica.pol_codigo_key = comprobante.ccomdoc.cdoc_politica.Value;
                politica = PoliticaBLL.GetByPK(politica);
            }


            bool habilitado = true;
            if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;


            List<Tab> tabs = new List<Tab>();

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Proveedor:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtCODCLIPRO", valor = persona.per_id, autocomplete = "GetProveedorObj", obligatorio = true, clase = Css.medium, placeholder = "Proveedor", habilitado = habilitado }.ToString() + " " + new Input { id = "txtNOMBRES", clase = Css.large, habilitado = false, valor = persona.per_apellidos + " " + persona.per_nombres }.ToString() + new Input { id = "txtCODPER", visible = false, valor = persona.per_codigo }.ToString() + new Boton { small = true, id = "btncallper", tooltip = "Agregar cliente", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cProveedor + ")" }.ToString() + new Boton { small = true, id = "btncleanper", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();
            tdatos.rows[1].cells[0].valor = "CI-RUC/Razón Social:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtRUC", clase = Css.medium, habilitado = false, valor = persona.per_ciruc }.ToString() + " " + new Input { id = "txtRAZON", clase = Css.large, habilitado = false, valor = persona.per_razon }.ToString();
            //tdatos.rows[2].cells[0].valor = "Política Compra:";
            //tdatos.rows[2].cells[1].valor = new Input { id = "txtIDPOL", autocomplete = "GetPoliticaObj", clase = Css.small, valor = politica.pol_id, obligatorio = true, placeholder = "Política", habilitado = habilitado }.ToString() + " " + new Input { id = "txtPOLITICA", clase = Css.large, habilitado = false, valor = politica.pol_nombre }.ToString() + " " + new Input { id = "txtPORCENTAJE", clase = Css.mini, numeric = true, valor = politica.pol_porc_desc, habilitado = false }.ToString() + "% Desc" + new Input { id = "txtCODPOL", visible = false, valor = politica.pol_codigo }.ToString() + new Input { id = "txtNROPAGOS", visible = false, valor = politica.pol_nro_pagos }.ToString() + new Input { id = "txtDIASPLAZO", visible = false, valor = politica.pol_dias_plazo }.ToString() + new Input { id = "txtPORCPAGOCON", visible = false, valor = politica.pol_porc_pago_con }.ToString();
            tdatos.rows[2].cells[0].valor = "Fecha Entrega:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtFECHAENTPRO", clase = Css.small, datepicker = true, datetimevalor = (comprobante.ccomdoc.cdoc_aut_fecha.HasValue) ? comprobante.ccomdoc.cdoc_aut_fecha.Value : DateTime.Now }.ToString();
            



            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(3, 2);
            if (!string.IsNullOrEmpty(comprobante.ccomdoc.cdoc_acl_nroautoriza))
            {

                string documento = comprobante.ccomdoc.cdoc_aut_factura;
                string alamcen = "";
                string puntoventa = "";
                string factura = "";
                Autpersona aut = new Autpersona();
                if (documento.Length > 15)
                {
                    alamcen = documento.Substring(0, 3);
                    puntoventa = documento.Substring(4, 3);
                    factura = documento.Substring(8, 9);                    
                    aut = AutpersonaBLL.GetByPK(new Autpersona { ape_empresa_key = comprobante.com_empresa, ape_nro_autoriza_key = comprobante.ccomdoc.cdoc_acl_nroautoriza, ape_fac1_key = alamcen, ape_fac2_key = puntoventa, ape_retdato_key = comprobante.ccomdoc.cdoc_acl_retdato.Value });
                }



                tdatos1.rows[0].cells[0].valor = "Autorización/Válida:";
                tdatos1.rows[0].cells[1].valor = new Input { id = "cmbAUTORIZACION", clase = Css.medium, valor = aut.ape_nro_autoriza, habilitado = false }.ToString() + " " + new Input { id = "txtFECHAAUT", clase = Css.medium, valor = aut.ape_val_fecha, habilitado = false }.ToString() + " " + new Input { id = "txtRETAUT", visible = false, valor = aut.ape_retdato }.ToString() + " " + new Input { id = "txtTABCOAUT", visible = false, valor = aut.ape_tablacoa }.ToString() + new Input { id = "txtDESDEAUT", visible = false, valor = aut.ape_fac3 }.ToString() + new Input { id = "txtHASTAAUT", visible = false, valor = aut.ape_fact3 }.ToString();
                //if (comprobante.com_tipodoc == Constantes.cNotacrePro.tpd_codigo)
                //{
                    //tdatos1.rows[1].cells[0].valor = "Documento/Factura:";
                    //tdatos1.rows[1].cells[1].valor = new Input { id = "txtALMACENPRO", valor = aut.ape_fac1, clase = Css.mini, habilitado = false }.ToString() + "-" + new Input { id = "txtPVENTAPRO", valor = aut.ape_fac2, clase = Css.mini, habilitado = false }.ToString() + "-" + new Input { id = "txtNUMEROPRO", valor = factura, clase = Css.small, obligatorio = true, largo = 9 }.ToString() + " / " + new Input { id = "txtFACTURAREF", clase = Css.medium, obligatorio = true, placeholder = "FACTURA" }.ToString();                
                //}
                //else
                //{
                    tdatos1.rows[1].cells[0].valor = "Documento:";
                    tdatos1.rows[1].cells[1].valor = new Input { id = "txtALMACENPRO", valor = aut.ape_fac1, clase = Css.mini, habilitado = false }.ToString() + "-" + new Input { id = "txtPVENTAPRO", valor = aut.ape_fac2, clase = Css.mini, habilitado = false }.ToString() + "-" + new Input { id = "txtNUMEROPRO", valor = factura, clase = Css.small, obligatorio = true, largo = 9 }.ToString();
                //}
                tdatos1.rows[2].cells[0].valor = "Factura:";
                tdatos1.rows[2].cells[1].valor = new Input { id = "txtCOMPROBANTE", clase = Css.medium, valor = comprobante.ccomdoc.cdoc_observeaciones}.ToString();

            }
            else
            {


                tdatos1.rows[0].cells[0].valor = "Autorización/Válida:";
                tdatos1.rows[0].cells[1].valor = new Select { id = "cmbAUTORIZACION", clase = Css.medium, diccionario = Dictionaries.Empty(), obligatorio = true }.ToString() + " " + new Input { id = "txtFECHAAUT", clase = Css.medium, habilitado = false }.ToString() + " " + new Input { id = "txtRETAUT", visible = false }.ToString() + " " + new Input { id = "txtTABCOAUT", visible = false }.ToString() + new Input { id = "txtDESDEAUT", visible = false }.ToString() + new Input { id = "txtHASTAAUT", visible = false }.ToString();
                tdatos1.rows[1].cells[0].valor = "Documento:";
                tdatos1.rows[1].cells[1].valor = new Input { id = "txtALMACENPRO", clase = Css.mini, habilitado = false }.ToString() + "-" + new Input { id = "txtPVENTAPRO", clase = Css.mini, habilitado = false }.ToString() + "-" + new Input { id = "txtNUMEROPRO", clase = Css.small, obligatorio = true, largo = 9 }.ToString();
                tdatos1.rows[2].cells[0].valor = "Factura:";
                tdatos1.rows[2].cells[1].valor = new Input { id = "txtCOMPROBANTE", clase = Css.medium }.ToString();



            }


            //tdatos1.rows[0].cells[1].valor = new Select { id = "cmbLISTAPRECIO", diccionario = Dictionaries.GetListaprecio(), clase = Css.medium}.ToString();



            html.AppendLine(tdatos1.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");

            html.AppendLine(new Input { id = "txtESTADO", valor = comprobante.com_estado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCERRADO", valor = Constantes.cEstadoMayorizado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCREADO", valor = Constantes.cEstadoProceso, visible = false }.ToString());

            StringBuilder htmlobs = new StringBuilder();
            htmlobs.AppendLine("<div class=\"row-fluid\">");
            htmlobs.AppendLine("<div class=\"span12\">");
            HtmlTable tdobs = new HtmlTable();
            tdobs.CreteEmptyTable(1, 2);
            tdobs.rows[0].cells[0].valor = "Observaciónes:";
            tdobs.rows[0].cells[1].valor = new Textarea { id = "txtOBSERVACIONES", clase = Css.xxlarge, valor = comprobante.com_concepto, habilitado = habilitado }.ToString();
            htmlobs.AppendLine(tdobs.ToString());
            htmlobs.AppendLine(" </div><!--span12-->");
            htmlobs.AppendLine("<!--row-fluid-->");

            tabs.Add(new Tab("tab1", "Datos Comprobante", html.ToString()));
            tabs.Add(new Tab("tab2", "Observaciones", htmlobs.ToString()));
            tabs.Add(new Tab("tab3", "&nbsp;", ""));

            return new Tabs { id = "tabcomprobante", tabs = tabs }.ToString();



        }


        [WebMethod]
        public static string GetDetalle(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            comprobante.ccomdoc = new Ccomdoc();
            comprobante.ccomdoc.cdoc_empresa = comprobante.com_empresa;
            comprobante.ccomdoc.cdoc_empresa_key = comprobante.com_empresa;
            comprobante.ccomdoc.cdoc_comprobante = comprobante.com_codigo;
            comprobante.ccomdoc.cdoc_comprobante_key = comprobante.com_codigo;
            comprobante.ccomdoc = CcomdocBLL.GetByPK(comprobante.ccomdoc);

            comprobante.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa={0} and ddoc_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "ddoc_secuencia");



            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdinvoice";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";

            tdatos.AddColumn("Id", "width10", "", new Input() { id = "txtIDCUE", placeholder = "CTA", autocomplete = "GetCuentaObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODCUE", visible = false }.ToString());
            tdatos.AddColumn("Cuenta", "width20", "", new Input() { id = "txtCUENTA", placeholder = "DESCRIPCION", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("Observación", "width20", "", new Textarea() { id = "txtOBSERVACION", placeholder = "OBSERVACIÓN", clase = Css.blocklevel }.ToString());
            tdatos.AddColumn("Cant.", "width5", Css.center, new Input() { id = "txtCANTIDAD", placeholder = "CANT", clase = Css.blocklevel + Css.cantidades, numeric = true }.ToString());
            tdatos.AddColumn("Valor", "width10", Css.right, new Input() { id = "txtVALOR", placeholder = "VALOR", clase = Css.blocklevel + Css.amount, numeric = true }.ToString());
            tdatos.AddColumn("Desc.%", "width5", Css.right, new Input() { id = "txtDESC", placeholder = "DESC", clase = Css.blocklevel + Css.amount }.ToString());
            tdatos.AddColumn("TOTAL", "width10", Css.right, new Input() { id = "txtTOTAL", placeholder = "TOTAL", clase = Css.blocklevel + Css.amount, habilitado = false }.ToString());
            tdatos.AddColumn("IVA", "width5", Css.center, new Check() { id = "chkIVA", clase = Css.blocklevel + Css.cantidades, valor = 0 }.ToString());
            tdatos.AddColumn("", "width5", Css.center, new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());

            tdatos.editable = true;

            foreach (Dcomdoc item in comprobante.ccomdoc.detalle)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-codcue=" + item.ddoc_cuenta;
                row.removable = true;

                row.cells.Add(new HtmlCell { valor = item.ddoc_cuentaid });//ID CUENTA
                row.cells.Add(new HtmlCell { valor = item.ddoc_cuentanombre });//NOMBRE CUENTA
                row.cells.Add(new HtmlCell { valor = item.ddoc_observaciones });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddoc_cantidad), clase = Css.center });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddoc_precio), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddoc_dscitem), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddoc_total), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = ((item.ddoc_grabaiva.HasValue) ? ((item.ddoc_grabaiva.Value == 1) ? "SI" : "NO") : "NO"), clase = Css.center });
                row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString(), clase = Css.center });
                tdatos.AddRow(row);
                //tdatos.AddRow(new HtmlRow(item.ddoc_productoid, item.ddoc_productonombre, item.ddoc_observaciones, item.ddoc_productounidad, item.ddoc_cantidad, item.ddoc_precio, item.ddoc_dscitem, item.ddoc_total, item.ddoc_productoiva) { data = "data-codpro=" + item.ddoc_producto });   

            }

            html.AppendLine(tdatos.ToString());



            return html.ToString();
        }


        [WebMethod]
        public static string GetPie(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);
            DateTime fecha = comprobante.com_fecha;
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
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(4, 3);
            tdatos.rows[0].cells[0].valor = "";
            tdatos.rows[0].cells[1].valor = "0%";
            tdatos.rows[0].cells[1].clase = Css.center;
            tdatos.rows[0].cells[2].valor = "IVA";
            tdatos.rows[0].cells[2].clase = Css.center;

            tdatos.rows[1].cells[0].valor = "Subtotal:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtSUBTOTAL0", clase = Css.small + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_subtot_0) }.ToString();
            tdatos.rows[1].cells[2].valor = new Input { id = "txtSUBTOTALIVA", clase = Css.small + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_subtotal) }.ToString();
            tdatos.rows[2].cells[0].valor = "Desc:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtDESC0", clase = Css.small + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_desc1_0) }.ToString();
            tdatos.rows[2].cells[2].valor = new Input { id = "txtDESCIVA", clase = Css.small + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_descuento1) }.ToString();
            tdatos.rows[3].cells[0].valor = "Descuento:";
            tdatos.rows[3].cells[1].valor = new Input { id = "txtDESCUENTO0", clase = Css.small + Css.amount, numeric = true, valor = Formatos.CurrencyFormat(comprobante.total.tot_desc2_0) }.ToString();
            tdatos.rows[3].cells[2].valor = new Input { id = "txtDESCUENTOIVA", clase = Css.small + Css.amount, numeric = true, valor = Formatos.CurrencyFormat(comprobante.total.tot_descuento2) }.ToString();
            html.AppendLine(tdatos.ToString());

            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(4, 2);
            decimal valoriva = Constantes.GetValorIVA(fecha);
            if (comprobante.total.tot_porc_impuesto.HasValue)
                valoriva = comprobante.total.tot_porc_impuesto.Value;

            //tdatos1.rows[0].cells[0].valor = "IVA " + valoriva + "%:" + new Select {id = "cmbIMPUESTO", diccionario=Dictionaries.GetImpuesto(), clase= Css.small }.ToString();
            tdatos1.rows[0].cells[0].valor = new Select { id = "cmbIMPUESTO", diccionario = Dictionaries.GetImpuestoIVA(), clase = Css.medium, valor = 1 }.ToString();
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtIVAPORCENTAJE", visible = false, valor = valoriva.ToString().Replace(",", ".") }.ToString() + new Input { id = "txtIVA", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_timpuesto) }.ToString();
            tdatos1.rows[1].cells[0].valor = "SEGURO:";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtSEGURO", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_tseguro) }.ToString();
            tdatos1.rows[2].cells[0].valor = "TRANSPORTE:";
            tdatos1.rows[2].cells[1].valor = new Input { id = "txtTRANSPORTE", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_transporte) }.ToString();
            tdatos1.rows[3].cells[0].valor = "TOTAL:";
            tdatos1.rows[3].cells[1].valor = new Input { id = "txtTOTALCOM", clase = Css.medium + Css.totalamount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_total) }.ToString();

            html.AppendLine(tdatos1.ToString());

            return html.ToString();

        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object objetocomp = null;
            object objetoafec = null;

            tmp.TryGetValue("comprobante", out objetocomp);
            tmp.TryGetValue("afectacion", out objetoafec);

            Comprobante obj = new Comprobante(objetocomp);


            List<Dcancelacion> detalle = new List<Dcancelacion>();


            if (objetoafec != null)
            {
                Array array = (Array)objetoafec;

                foreach (Object item in array)
                {
                    if (item != null)

                        detalle.Add(new Dcancelacion(item));
                }
            }
            obj.cancelaciones = detalle;

            List<vObligacion> list = vObligacionBLL.GetAll(new WhereParams("cdoc_aut_factura={0} and com_estado ={1} and (com_tipodoc ={2}) and com_codclipro={3}", obj.ccomdoc.cdoc_aut_factura, Constantes.cEstadoMayorizado, 24, obj.com_codclipro), "");




            try
            {
                if (list.Count > 0)
                {

                    throw new ArgumentException("La factura " + obj.ccomdoc.cdoc_aut_factura + " ya fue ingresada");
                    //return null;

                }
                else
                {

                    if (obj.com_codigo > 0)
                        obj = FAC.update_notacreditopro(obj);
                    else
                        obj = FAC.save_notacreditopro(obj);

                    obj = FAC.account_notacreditopro(obj);



                    return obj.com_codigo.ToString();
                }




            }
            catch (Exception ex)
            {
                ExceptionHandling.Log.AddExepcion(ex);
                throw ex;
                //return obj.com_codigo.ToString();
                //return "-1";
            }






        }


        [WebMethod]
        public static string CloseObject(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object objetocomp = null;

            tmp.TryGetValue("comprobante", out objetocomp);

            Comprobante obj = new Comprobante(objetocomp);

            try
            {
                if (obj.com_codigo == 0)
                    obj = FAC.save_obligacion(obj);
                obj = FAC.account_obligacion(obj);
                return obj.com_codigo.ToString();
            }
            catch (Exception ex)
            {
                return "-1";
            }


        }
    }
}