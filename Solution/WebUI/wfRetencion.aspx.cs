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
    public partial class wfRetencion : System.Web.UI.Page
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

                ValidaOrigen();

                pageIndex = 1;
                pageSize = 20;
            }
        }

        private void ValidaOrigen()
        {
            //Empresa emp = (Empresa)Session["empresa"]; 
            if (txtorigen.Text == "OBL")
            {
                int codigoemp = int.Parse(Request.QueryString["codigoempresa"]);

                List<Ccomdoc> lst = CcomdocBLL.GetAll(new WhereParams("cdoc_empresa={0} and cdoc_factura={1}", codigoemp, Convert.ToInt64(txtcodigocompref.Text)), "");
                if (lst.Count > 0)
                {
                    txtorigen.Text = "";
                    txtcodigocomp.Text = lst[0].cdoc_comprobante.ToString();
                    //Response.Redirect("wfInfo.aspx?msg=La planilla ya posee una factura generada");
                }



                //Comprobante com = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = codigoemp, com_empresa_key = codigoemp, com_codigo = Convert.ToInt64(txtcodigocompref.Text), com_codigo_key = Convert.ToInt64(txtcodigocompref.Text) });
                //com.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = codigoemp, cdoc_empresa_key = codigoemp, cdoc_comprobante = com.com_codigo, cdoc_comprobante_key = com.com_codigo });
                //if (com.ccomdoc.cdoc_factura.HasValue)
                //{
                //    txtorigen.Text = "";
                //    txtcodigocomp.Text = com.ccomdoc.cdoc_factura.ToString();
                //}

            }
        }

        [WebMethod]
        public static string GetAutorizaciones(object objeto)
        {
            Autpersona aut = new Autpersona(objeto);
            return new Select { id = "cmbAUTORIZACION", clase = Css.medium, diccionario = (aut.ape_persona.HasValue) ? Dictionaries.GetAutorizacionesPersona(aut.ape_empresa, aut.ape_persona.Value) : Dictionaries.Empty() }.ToString();
        }

        [WebMethod]
        public static string GetAutorizacionData(object objeto)
        {
            Autpersona aut = new Autpersona(objeto);

            List<Autpersona> autorizaciones = AutpersonaBLL.GetAll(new WhereParams("ape_empresa={0} and ape_persona={1} and ape_nro_autoriza= {2}", aut.ape_empresa, aut.ape_persona, aut.ape_nro_autoriza), "");
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


            comprobante.retenciones = DretencionBLL.GetAll(new WhereParams("drt_empresa={0} and drt_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "drt_secuencia");



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

            List<Tab> tabs = new List<Tab>();

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();

            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Proveedor:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtCODCLIPRO", valor = persona.per_id, autocomplete = "GetProveedorObj", obligatorio = true, clase = Css.medium, placeholder = "Proveedor" }.ToString() + " " + new Input { id = "txtNOMBRES", clase = Css.large, habilitado = false, valor = persona.per_apellidos + " " + persona.per_nombres }.ToString() + new Input { id = "txtCODPER", visible = false, valor = persona.per_codigo }.ToString() + new Boton { small = true, id = "btncallper", tooltip = "Agregar cliente", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cProveedor + ")" }.ToString() + new Boton { small = true, id = "btncleanper", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();
            tdatos.rows[1].cells[0].valor = "CI-RUC/Razón Social:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtRUC", clase = Css.medium, habilitado = false, valor = persona.per_ciruc }.ToString() + " " + new Input { id = "txtRAZON", clase = Css.large, habilitado = false, valor = persona.per_razon }.ToString();
            tdatos.rows[2].cells[0].valor = "Teléfono/Dirección:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtTELEFONO", clase = Css.medium, habilitado = false, valor = persona.per_telefono }.ToString() + " " + new Input { id = "txtDIRECCION", clase = Css.large, habilitado = false, valor = persona.per_direccion }.ToString();

            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");

            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(2, 2);

            tdatos1.rows[0].cells[0].valor = "Tipo Comprobante:";            
            tdatos1.rows[0].cells[1].valor = new Select { id = "cmbTIPCOM", clase = Css.medium, diccionario = Dictionaries.GetTipoComprobanteRetencion(), obligatorio = true, valor = comprobante.ccomdoc.cdoc_formapago }.ToString(); // new Input { id = "txtTIPCOM", clase = Css.medium, habilitado = false, valor = "FACTURA" }.ToString();
            tdatos1.rows[1].cells[0].valor = "Nº de Comprobante:";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtNUMCOMPROBANTE", clase = Css.medium, valor = comprobante.retenciones[0].drt_factura, habilitado = false }.ToString() + new Input { id = "txtFACTURAOBL", visible = false, valor = comprobante.ccomdoc.cdoc_factura }.ToString();
            html.AppendLine(tdatos1.ToString());

           // html.AppendLine(tdatos1.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");
          
            StringBuilder htmlobs = new StringBuilder();
            htmlobs.AppendLine("<div class=\"row-fluid\">");
            htmlobs.AppendLine("<div class=\"span12\">");
            HtmlTable tdobs = new HtmlTable();
            tdobs.CreteEmptyTable(1, 2);
            tdobs.rows[0].cells[0].valor = "Observaciónes:";
            tdobs.rows[0].cells[1].valor = new Textarea { id = "txtOBSERVACIONES", clase = Css.xxlarge, valor = comprobante.com_concepto }.ToString();
            htmlobs.AppendLine(tdobs.ToString());
            htmlobs.AppendLine(" </div><!--span12-->");
            htmlobs.AppendLine("<!--row-fluid-->");

            tabs.Add(new Tab("tab1", "Datos Comprobante", html.ToString()));
            tabs.Add(new Tab("tab2", "Observaciones", htmlobs.ToString()));
            tabs.Add(new Tab("tab3", "&nbsp;", ""));

            return new Tabs { id = "tabcomprobante", tabs = tabs }.ToString();



        }


        [WebMethod]
        public static string GetCabeceraFromOBL(object objeto)
        {



            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);

            comprobante.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = comprobante.com_empresa, cdoc_empresa_key = comprobante.com_empresa, cdoc_comprobante = comprobante.com_codigo, cdoc_comprobante_key = comprobante.com_codigo });
            comprobante.total = TotalBLL.GetByPK(new Total { tot_empresa = comprobante.com_empresa, tot_empresa_key = comprobante.com_empresa, tot_comprobante = comprobante.com_codigo, tot_comprobante_key = comprobante.com_codigo });


            

            comprobante.retenciones = DretencionBLL.GetAll(new WhereParams("drt_empresa={0} and drt_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "drt_secuencia");



            Persona persona = new Persona();
            persona.per_empresa = comprobante.com_empresa;
            persona.per_empresa_key = comprobante.com_empresa;
            if (comprobante.com_codclipro.HasValue)
            {
                persona.per_codigo = comprobante.com_codclipro.Value;
                persona.per_codigo_key = comprobante.com_codclipro.Value;
                persona = PersonaBLL.GetByPK(persona);
            }

         

            List<Tab> tabs = new List<Tab>();

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();

            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Proveedor:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtCODCLIPRO", valor = persona.per_id, autocomplete = "GetProveedorObj", obligatorio = true, clase = Css.medium, placeholder = "Proveedor" }.ToString() + " " + new Input { id = "txtNOMBRES", clase = Css.large, habilitado = false, valor = persona.per_apellidos + " " + persona.per_nombres }.ToString() + new Input { id = "txtCODPER", visible = false, valor = persona.per_codigo }.ToString() + new Boton { small = true, id = "btncallper", tooltip = "Agregar cliente", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cProveedor + ")" }.ToString() + new Boton { small = true, id = "btncleanper", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();
            tdatos.rows[1].cells[0].valor = "CI-RUC/Razón Social:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtRUC", clase = Css.medium, habilitado = false, valor = persona.per_ciruc }.ToString() + " " + new Input { id = "txtRAZON", clase = Css.large, habilitado = false, valor = persona.per_razon }.ToString();
            tdatos.rows[2].cells[0].valor = "Teléfono/Dirección:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtTELEFONO", clase = Css.medium, habilitado = false, valor = persona.per_telefono }.ToString() + " " + new Input { id = "txtDIRECCION", clase = Css.large, habilitado = false, valor = persona.per_direccion }.ToString();

            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");

            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(2, 2);

            tdatos1.rows[0].cells[0].valor = "Tipo Comprobante:";
            tdatos1.rows[0].cells[1].valor = new Select { id = "cmbTIPCOM", clase = Css.medium, diccionario = Dictionaries.GetTipoComprobanteRetencion(), obligatorio = true, valor = comprobante.ccomdoc.cdoc_formapago }.ToString(); // new Input { id = "txtTIPCOM", clase = Css.medium, habilitado = false, valor = "FACTURA" }.ToString();
            tdatos1.rows[1].cells[0].valor = "Nº de Comprobante:";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtNUMCOMPROBANTE", clase = Css.medium, valor = comprobante.ccomdoc.cdoc_aut_factura, habilitado = false }.ToString() + new Input { id = "txtFACTURAOBL", visible = false, valor = comprobante.com_codigo}.ToString();
            html.AppendLine(tdatos1.ToString());

            // html.AppendLine(tdatos1.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");

            StringBuilder htmlobs = new StringBuilder();
            htmlobs.AppendLine("<div class=\"row-fluid\">");
            htmlobs.AppendLine("<div class=\"span12\">");
            HtmlTable tdobs = new HtmlTable();
            tdobs.CreteEmptyTable(1, 2);
            tdobs.rows[0].cells[0].valor = "Observaciónes:";
            //tdobs.rows[0].cells[1].valor = new Textarea { id = "txtOBSERVACIONES", clase = Css.xxlarge, valor = "RETENCIÓN COMPROBANTE "+ comprobante.ccomdoc.cdoc_aut_factura}.ToString();
            tdobs.rows[0].cells[1].valor = new Textarea { id = "txtOBSERVACIONES", clase = Css.xxlarge, valor = "RETENCIÓN " + comprobante.ccomdoc.cdoc_nombre + " FACTURA:" + comprobante.ccomdoc.cdoc_aut_factura }.ToString();
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
            bool habilitado = true;
            if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;
            comprobante.ccomdoc = new Ccomdoc();
            comprobante.ccomdoc.cdoc_empresa = comprobante.com_empresa;
            comprobante.ccomdoc.cdoc_empresa_key = comprobante.com_empresa;
            comprobante.ccomdoc.cdoc_comprobante = comprobante.com_codigo;
            comprobante.ccomdoc.cdoc_comprobante_key = comprobante.com_codigo;
            comprobante.ccomdoc = CcomdocBLL.GetByPK(comprobante.ccomdoc);
            comprobante.retenciones = DretencionBLL.GetAll(new WhereParams("drt_empresa={0} and drt_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "drt_secuencia");
            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdinvoice";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";
            tdatos.AddColumn("Id", "width10", "", new Input() { id = "txtIDIMP", placeholder = "ID", autocomplete = "GetImpuestoRentenObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODIMP", visible = false }.ToString() + new Input() { id = "txtIMPCUENTA", visible = false }.ToString());
            tdatos.AddColumn("Impuesto", "width20", "", new Input() { id = "txtIMPUESTO", placeholder = "IMPUESTO", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("Concepto", "width10", "", new Select() { id = "cmbCONCEPTO", placeholder = "CONCEPTO", diccionario = Dictionaries.GetConcepto(), clase = Css.large }.ToString() + new Input() { id = "txtFACTOR", visible = false });
         //   tdatos.AddColumn("Comprobante", "width20", "", new Textarea() { id = "txtCOMPROBANTE", placeholder = "COMPROBANTE", clase = Css.blocklevel }.ToString());
            tdatos.AddColumn("% Retencion", "width20", "", new Input() { id = "txtPORCTJRETENCION", placeholder = "% RETENCION", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("Base imponible", "width20", "", new Textarea() { id = "txtBASE", placeholder = "BASE IMPONIBLE", clase = Css.blocklevel + Css.amount }.ToString());
            //tdatos.AddColumn("Peso(lbs)", "width5", Css.right, new Input() { id = "txtPESO", placeholder = "Peso", clase = Css.blocklevel + Css.amount }.ToString());           
            tdatos.AddColumn("TOTAL", "width10", Css.right, new Input() { id = "txtTOTAL", placeholder = "TOTAL", clase = Css.blocklevel + Css.amount, habilitado = false }.ToString());
            tdatos.AddColumn("", "width5", Css.center, new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());
            tdatos.editable = habilitado;

            foreach (Dretencion item in comprobante.retenciones)
            {
                HtmlRow row = new HtmlRow();

                row.data = "data-codpro=" + item.drt_impuesto;
                row.removable = true;
                row.cells.Add(new HtmlCell { valor = item.drt_impuestoid, data = "data-cueimp=" + item.drt_cuenta });//ID CUENTA
                row.cells.Add(new HtmlCell { valor = item.drt_impuestonombre });//NOMBRE CUENTA
                row.cells.Add(new HtmlCell { valor = item.drt_conceptonombre, data = "data-concod=" + item.drt_con_codigo});
                //  row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.drt_con_codigo), clase = Css.center });
              //  row.cells.Add(new HtmlCell { valor = item.drt_factura, clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.drt_porcentaje), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.drt_valor), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.drt_total), clase = Css.right });
                // row.cells.Add(new HtmlCell { valor = ((item.ddoc_productoiva.HasValue) ? ((item.ddoc_productoiva.Value == 1) ? "SI" : "NO") : "NO"), clase = Css.center });
                row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString(), clase = Css.center });

                if (habilitado)
                    row.clickevent = "Edit(this)";
                tdatos.AddRow(row);
                //tdatos.AddRow(new HtmlRow(item.ddoc_productoid, item.ddoc_productonombre, item.ddoc_observaciones, item.ddoc_productounidad, item.ddoc_cantidad, item.ddoc_precio, item.ddoc_dscitem, item.ddoc_total, item.ddoc_productoiva) { data = "data-codpro=" + item.ddoc_producto });   

            }
            html.AppendLine(tdatos.ToString());
            html.AppendLine(new Input { id = "txtESTADO", valor = comprobante.com_estado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCERRADO", valor = Constantes.cEstadoMayorizado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCREADO", valor = Constantes.cEstadoProceso, visible = false }.ToString());
            return html.ToString();
        }


        [WebMethod]
        public static string GetDetalleFromOBL(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            comprobante.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = comprobante.com_empresa, cdoc_empresa_key = comprobante.com_empresa, cdoc_comprobante = comprobante.com_codigo, cdoc_comprobante_key = comprobante.com_codigo });
            comprobante.total = TotalBLL.GetByPK(new Total { tot_empresa = comprobante.com_empresa, tot_empresa_key = comprobante.com_empresa, tot_comprobante = comprobante.com_codigo, tot_comprobante_key = comprobante.com_codigo });
            Persona persona = new Persona();
            persona.per_empresa = comprobante.com_empresa;
            persona.per_empresa_key = comprobante.com_empresa;
            if (comprobante.com_codclipro.HasValue)
            {
                persona.per_codigo = comprobante.com_codclipro.Value;
                persona.per_codigo_key = comprobante.com_codclipro.Value;
                persona = PersonaBLL.GetByPK(persona);
            }

            Impuesto impiva = ImpuestoBLL.GetByPK(new Impuesto { imp_empresa = comprobante.com_empresa, imp_empresa_key = comprobante.com_empresa, imp_codigo = persona.per_retiva.Value, imp_codigo_key = persona.per_retiva.Value });
            Impuesto impfue = ImpuestoBLL.GetByPK(new Impuesto { imp_empresa = comprobante.com_empresa, imp_empresa_key = comprobante.com_empresa, imp_codigo = persona.per_retfuente.Value, imp_codigo_key = persona.per_retfuente.Value });



            bool habilitado = true;
            //if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
            //    habilitado = false;


            comprobante.retenciones = DretencionBLL.GetAll(new WhereParams("drt_empresa={0} and drt_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "drt_secuencia");
            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdinvoice";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";
            tdatos.AddColumn("Id", "width10", "", new Input() { id = "txtIDIMP", placeholder = "ID", autocomplete = "GetImpuestoRentenObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODIMP", visible = false }.ToString() + new Input() { id = "txtIMPCUENTA", visible = false }.ToString());
            tdatos.AddColumn("Impuesto", "width20", "", new Input() { id = "txtIMPUESTO", placeholder = "IMPUESTO", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("Concepto", "width10", "", new Select() { id = "cmbCONCEPTO", placeholder = "CONCEPTO", diccionario = Dictionaries.GetConcepto(), clase = Css.large }.ToString() + new Input() { id = "txtFACTOR", visible = false });
            //   tdatos.AddColumn("Comprobante", "width20", "", new Textarea() { id = "txtCOMPROBANTE", placeholder = "COMPROBANTE", clase = Css.blocklevel }.ToString());
            tdatos.AddColumn("% Retencion", "width20", "", new Input() { id = "txtPORCTJRETENCION", placeholder = "% RETENCION", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("Base imponible", "width20", "", new Textarea() { id = "txtBASE", placeholder = "BASE IMPONIBLE", clase = Css.blocklevel + Css.amount }.ToString());
            //tdatos.AddColumn("Peso(lbs)", "width5", Css.right, new Input() { id = "txtPESO", placeholder = "Peso", clase = Css.blocklevel + Css.amount }.ToString());           
            tdatos.AddColumn("TOTAL", "width10", Css.right, new Input() { id = "txtTOTAL", placeholder = "TOTAL", clase = Css.blocklevel + Css.amount, habilitado = false }.ToString());
            tdatos.AddColumn("", "width5", Css.center, new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());
            tdatos.editable = habilitado;




            //IVA

            HtmlRow row = new HtmlRow();

            row.data = "data-codpro=" + impiva.imp_codigo;
            row.removable = true;
            row.cells.Add(new HtmlCell { valor = impiva.imp_id, data = "data-cueimp=" + impiva.imp_cuenta });//ID CUENTA
            row.cells.Add(new HtmlCell { valor = impiva.imp_nombre });//NOMBRE CUENTA
            row.cells.Add(new HtmlCell { valor = "", data = "data-concod=" });
            //  row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.drt_con_codigo), clase = Css.center });
            //  row.cells.Add(new HtmlCell { valor = item.drt_factura, clase = Css.right });
            row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(impiva.imp_porcentaje), clase = Css.right });
            row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(comprobante.total.tot_timpuesto), clase = Css.right });
            row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(comprobante.total.tot_timpuesto * (impiva.imp_porcentaje / 100)), clase = Css.right });
            // row.cells.Add(new HtmlCell { valor = ((item.ddoc_productoiva.HasValue) ? ((item.ddoc_productoiva.Value == 1) ? "SI" : "NO") : "NO"), clase = Css.center });
            row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString(), clase = Css.center });

            if (habilitado)
                row.clickevent = "Edit(this)";
            tdatos.AddRow(row);


            //RENTA
            row = new HtmlRow();

            row.data = "data-codpro=" + impfue.imp_codigo;
            row.removable = true;
            row.cells.Add(new HtmlCell { valor = impfue.imp_id, data = "data-cueimp=" + impfue.imp_cuenta });//ID CUENTA
            row.cells.Add(new HtmlCell { valor = impfue.imp_nombre });//NOMBRE CUENTA
            row.cells.Add(new HtmlCell { valor = "", data = "data-concod=" });
            //  row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.drt_con_codigo), clase = Css.center });
            //  row.cells.Add(new HtmlCell { valor = item.drt_factura, clase = Css.right });
            row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(impfue.imp_porcentaje), clase = Css.right });
            row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(comprobante.total.tot_subtot_0), clase = Css.right });
            row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(comprobante.total.tot_subtot_0 * (impfue.imp_porcentaje / 100)), clase = Css.right });
            // row.cells.Add(new HtmlCell { valor = ((item.ddoc_productoiva.HasValue) ? ((item.ddoc_productoiva.Value == 1) ? "SI" : "NO") : "NO"), clase = Css.center });
            row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString(), clase = Css.center });

            if (habilitado)
                row.clickevent = "Edit(this)";
            tdatos.AddRow(row);
            //tdatos.AddRow(new HtmlRow(item.ddoc_productoid, item.ddoc_productonombre, item.ddoc_observaciones, item.ddoc_productounidad, item.ddoc_cantidad, item.ddoc_precio, item.ddoc_dscitem, item.ddoc_total, item.ddoc_productoiva) { data = "data-codpro=" + item.ddoc_producto });   





            html.AppendLine(tdatos.ToString());
            html.AppendLine(new Input { id = "txtESTADO", valor = Constantes.cEstadoProceso, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCERRADO", valor = Constantes.cEstadoMayorizado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCREADO", valor = Constantes.cEstadoProceso, visible = false }.ToString());
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
           

            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(1, 2);
            decimal valoriva = 12;

            tdatos1.rows[0].cells[0].valor = "TOTAL:";
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtTOTALCOM", clase = Css.medium + Css.totalamount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_total) }.ToString();

            html.AppendLine(tdatos1.ToString());

            return html.ToString();

        }



        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object objetocomp = null;
            tmp.TryGetValue("comprobante", out objetocomp);
            Comprobante obj = new Comprobante(objetocomp);
            obj.com_estado = (obj.com_estado == Constantes.cEstadoProceso) ? Constantes.cEstadoGrabado : obj.com_estado;



            string mensaje = "";


            //if (obj.com_codigo > 0)
            //{
            try
            {
                if (obj.com_codigo > 0)
                    obj = CXCP.update_retencion(obj);
                else
                    obj = CXCP.save_retencion(obj);
                obj = CXCP.account_retencion(obj);
                mensaje = "OK";
                //return obj.com_codigo.ToString();
            }
            catch (Exception ex)
            {
                mensaje = "ERROR";
                ExceptionHandling.Log.AddExepcion(ex);
                //return obj.com_codigo.ToString();
            }


            //}
            //else
            //{
            //    try
            //    {
            //        obj = CXCP.save_retencion(obj);
            //        obj = CXCP.account_retencion(obj);
            //        //return obj.com_codigo.ToString();

            //    }
            //    catch (Exception ex)
            //    {
            //        ExceptionHandling.Log.AddExepcion(ex);
            //        //return obj.com_codigo.ToString();
            //    }
            //    //string mensaje = "";
            //    //obj = InsertComprobante(obj, ref mensaje);
            //    //return mensaje;
            //}

            object[] retorno = new object[2];
            retorno[0] = mensaje;
            retorno[1] = obj;
            return new JavaScriptSerializer().Serialize(retorno);







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
                obj = CXCP.account_retencion(obj);
                //Electronico.GenerateElectronico(obj,null);
                return obj.com_codigo.ToString();
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }








        public static List<Ddocumento> GetDocumentos(Comprobante obj)
        {
            DateTime fecha = obj.com_fecha;
            List<Ddocumento> lista = new List<Ddocumento>();

            decimal valor = obj.total.tot_total / (decimal)Functions.Conversiones.GetValueByType(obj.total.tot_nro_pagos.Value, typeof(decimal));
            for (int i = 0; i < obj.total.tot_nro_pagos.Value; i++)
            {
                fecha = fecha.AddDays(obj.total.tot_dias_plazo.Value);
                Ddocumento doc = new Ddocumento();
                doc.ddo_empresa = obj.com_empresa;
                doc.ddo_comprobante = obj.com_codigo;
                doc.ddo_transacc = General.GetTransacc(obj.com_tipodoc);
                doc.ddo_doctran = obj.com_doctran;
                doc.ddo_pago = i + 1;
                doc.ddo_codclipro = obj.com_codclipro;
                doc.ddo_debcre = Constantes.cCredito;
                //doc.ddo_tipo_cambio = 
                doc.ddo_fecha_emi = obj.com_fecha;
                doc.ddo_fecha_ven = fecha;
                doc.ddo_monto = valor;
                //doc.ddo_monto_ext = 
                doc.ddo_cancela = 0;
                //doc.ddo_cancela_ext =
                doc.ddo_cancelado = 0;
                doc.ddo_agente = obj.com_agente;
                //doc.ddo_cuenta = 
                doc.ddo_modulo = obj.com_modulo;
                lista.Add(doc);
            }

            return lista;
        }


        public static Comprobante InsertComprobante(Comprobante obj, ref string mensaje)
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

            //obj.com_empresa = 1;
            //obj.com_periodo = fecha.Year;
            //obj.com_tipodoc = 4;
            //obj.com_ctipocom = 2; //FACT

            obj.com_numero = dti.dti_numero.Value;
            obj.com_concepto = "RETENCION " + obj.ccomdoc.cdoc_nombre;
            obj.com_modulo = General.GetModulo(obj.com_tipodoc); ;
            obj.com_transacc = General.GetTransacc(obj.com_tipodoc);

            obj.com_estado =  Constantes.cEstadoGrabado; 
            obj.com_descuadre = 0;
            obj.com_adestino = 0;
            obj.com_doctran = General.GetNumeroComprobante(obj);

            //obj.com_fecha = fecha;


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                obj.com_codigo = ComprobanteBLL.InsertIdentity(transaction, obj);
                //int codigo = ComprobanteBLL.InsertIdentity(transaction, obj);

                obj.ccomdoc.cdoc_empresa = obj.com_empresa;
                obj.ccomdoc.cdoc_comprobante = obj.com_codigo;
               
                CcomdocBLL.Insert(transaction, obj.ccomdoc);


                int contador = 0;
                foreach (Dretencion item in obj.retenciones)
                {
                    item.drt_empresa = obj.com_empresa;
                    item.drt_comprobante = obj.com_codigo;
                    item.drt_secuencia = contador;
                    item.drt_debcre = Constantes.cCredito;
                    DretencionBLL.Insert(transaction, item);
                    contador++;
                }
/*
                obj.total.tot_comprobante = obj.com_codigo;
                TotalBLL.Insert(transaction, obj.total);*/


                DtipocomBLL.Update(transaction, dti);

                transaction.Commit();
                mensaje = "OK";
            }
            catch
            {
                transaction.Rollback();
                mensaje = "ERROR";
            }

            return obj;

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
           //     objU.com_concepto = "RETENCION " + obj.ccomdoc.cdoc_nombre;
                objU.com_concepto = "RETENCION " + obj.ccomdoc.cdoc_nombre ; 
                ComprobanteBLL.Update(transaction, objU);
                obj.ccomdoc.cdoc_empresa_key = obj.ccomdoc.cdoc_empresa;
                obj.ccomdoc.cdoc_comprobante_key = obj.ccomdoc.cdoc_comprobante;
                objU.ccomdoc = CcomdocBLL.GetByPK(obj.ccomdoc);

             

                obj.ccomdoc.cdoc_factura = objU.ccomdoc.cdoc_factura;
                CcomdocBLL.Update(transaction, obj.ccomdoc);
                List<Dretencion> lst = DretencionBLL.GetAll(new WhereParams("drt_empresa = {0} and drt_comprobante = {1}", obj.com_empresa, obj.com_codigo), "");
                foreach (Dretencion item in lst)
                {
                    DretencionBLL.Delete(transaction, item);
                }
                int contador = 0;
                decimal totalcancela = 0;
                foreach (Dretencion item in obj.retenciones)
                {
                    item.drt_empresa = obj.com_empresa;
                    item.drt_comprobante = obj.com_codigo;
                    item.drt_secuencia = contador;
                    item.drt_debcre = Constantes.cCredito;
                    totalcancela += item.drt_total.Value;
                    DretencionBLL.Insert(transaction, item);
                    contador++;
                }


                

                Comprobante comp = new Comprobante();
                comp.com_empresa_key = obj.com_empresa;
                comp.com_codigo_key = obj.ccomdoc.cdoc_factura.Value;
                comp = ComprobanteBLL.GetByPK(comp);

                List<Ddocumento> lst3 = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa = {0} and ddo_comprobante = {1}", obj.com_empresa, obj.ccomdoc.cdoc_factura), "");
                foreach (Ddocumento item in lst3)
                {
                  //  DdocumentoBLL.Delete(transaction, item);
                    List<Dcancelacion> lst2 = DcancelacionBLL.GetAll(new WhereParams("dca_empresa = {0} and dca_comprobante = {1}", item.ddo_empresa, item.ddo_comprobante), "");
                    foreach (Dcancelacion item2 in lst2)
                    {
                        item.ddo_cancela = ((item.ddo_cancela.HasValue) ? item.ddo_cancela.Value : 0) - item2.dca_monto;
                        if (item.ddo_monto >= item.ddo_cancela)
                            item.ddo_cancelado =0;
                        DcancelacionBLL.Delete(transaction, item2);
                    }



                    Dcancelacion dca = new Dcancelacion();
                    //dca.dca_empresa = doc.ddo_empresa;
                    //dca.dca_comprobante = doc.ddo_comprobante;
                    dca.dca_empresa = obj.com_empresa;
                    dca.dca_comprobante =  comp.com_codigo;
                    dca.dca_transacc = item.ddo_transacc;
                    dca.dca_doctran = item.ddo_doctran;
                    dca.dca_pago = item.ddo_pago;
                    dca.dca_comprobante_can = obj.com_codigo;
                    dca.dca_secuencia = 0;
                    dca.dca_debcre = (item.ddo_debcre == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
                    dca.dca_transacc = comp.com_transacc;
                    dca.dca_tipo_cambio = item.ddo_tipo_cambio;
                    if (totalcancela > item.ddo_monto.Value)
                    {
                        dca.dca_monto = item.ddo_monto;
                        dca.dca_monto_ext = item.ddo_monto;
                    }
                    else
                    {
                        dca.dca_monto = totalcancela;
                        dca.dca_monto_ext = totalcancela;
                    }

                    DcancelacionBLL.Insert(transaction, dca);
                    item.ddo_empresa_key = item.ddo_empresa;
                    item.ddo_comprobante_key = item.ddo_comprobante;
                    item.ddo_transacc_key = item.ddo_transacc;
                    item.ddo_doctran_key = item.ddo_doctran;
                    item.ddo_pago_key = item.ddo_pago;
                    item.ddo_cancela = ((item.ddo_cancela.HasValue) ? item.ddo_cancela.Value : 0) + dca.dca_monto;
                    if (item.ddo_cancela >= item.ddo_monto)
                        item.ddo_cancelado = 1;
                    DdocumentoBLL.Update(transaction, item);// ACTUALIZA EL DOCUMENTO CANCELANDOLO   
                    if (totalcancela > item.ddo_monto.Value)
                        totalcancela = totalcancela - item.ddo_monto.Value;
                    else
                        break;
                }


               







                transaction.Commit();

            }
            catch
            {
                transaction.Rollback();
                return "ERROR";
            }

            return "OK";
        }
    }
}