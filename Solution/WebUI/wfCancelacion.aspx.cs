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
    public partial class wfCancelacion : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "dcab_nombre";
        protected static string WhereClause = "";
        protected static WhereParams parametros;
        protected static int? tipodoc; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txttipodoc.Text = (Request.QueryString["tipodoc"] != null) ? Request.QueryString["tipodoc"].ToString() : "-1";
                txtcodigocomp.Text = (Request.QueryString["codigocomp"] != null) ? Request.QueryString["codigocomp"].ToString() : "-1";
                txtorigen.Text = (Request.QueryString["origen"] != null) ? Request.QueryString["origen"].ToString() : "";
                txtcodigocompref.Text = (Request.QueryString["codigocompref"] != null) ? Request.QueryString["codigocompref"].ToString() : "-1";
                pageIndex = 1;
                pageSize = 20;
                tipodoc = Convert.ToInt32(txttipodoc.Text);
            }
        }

        
        
        public static string ShowObject(Comprobante obj)
        {

            Persona persona = new Persona();
            persona.per_empresa = obj.com_empresa;
            persona.per_empresa_key = obj.com_empresa;
            if (obj.com_codclipro.HasValue)
            {
                persona.per_codigo = obj.com_codclipro.Value;
                persona.per_codigo_key = obj.com_codclipro.Value;
                persona = PersonaBLL.GetByPK(persona);
            }


            bool habilitado = true;
            if (obj.com_estado == Constantes.cEstadoEliminado || obj.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;

            List<Tab> tabs = new List<Tab>();


            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(3, 2);
            if (obj.com_tipodoc == Constantes.cPago.tpd_codigo)
            {
                tdatos.rows[0].cells[0].valor = "Proveedor:";
                tdatos.rows[0].cells[1].valor = new Input { id = "txtCODCLIPRO", valor = persona.per_id, autocomplete = "GetProveedorObj", obligatorio = true, clase = Css.medium, placeholder = "Proveedor" , habilitado=habilitado }.ToString() + " " + new Input { id = "txtNOMBRES", clase = Css.large, habilitado = false, valor = persona.per_apellidos + " " + persona.per_nombres }.ToString() + new Input { id = "txtCODPER", visible = false, valor = persona.per_codigo }.ToString() + new Boton { small = true, id = "btncallper", tooltip = "Agregar cliente", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cProveedor + ")" }.ToString() + new Boton { small = true, id = "btncleanper", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();
            }
            else
            {
                tdatos.rows[0].cells[0].valor = "Cliente:";
                tdatos.rows[0].cells[1].valor = new Input { id = "txtCODCLIPRO", valor = persona.per_id, autocomplete = "GetClienteObj", obligatorio = true, clase = Css.medium, placeholder = "Cliente", habilitado = habilitado }.ToString() + " " + new Input { id = "txtNOMBRES", clase = Css.large, habilitado = false, valor = persona.per_apellidos + " " + persona.per_nombres }.ToString() + new Input { id = "txtCODPER", visible = false, valor = persona.per_codigo }.ToString() + new Boton { small = true, id = "btncallper", tooltip = "Agregar cliente", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cCliente + ")" }.ToString() + new Boton { small = true, id = "btncleanper", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();
            }
            tdatos.rows[1].cells[0].valor = "CI-RUC/Razón Social:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtRUC", clase = Css.medium, habilitado = false, valor = persona.per_ciruc }.ToString() + " " + new Input { id = "txtRAZON", clase = Css.large, habilitado = false, valor = persona.per_razon }.ToString();
            tdatos.rows[2].cells[0].valor = "Teléfono/Dirección:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtTELEFONO", clase = Css.medium, habilitado = false, valor = persona.per_telefono }.ToString() + " " + new Input { id = "txtDIRECCION", clase = Css.large, habilitado = false, valor = persona.per_direccion }.ToString();
            //tdatos.rows[3].cells[0].valor = "Ubicación:";
            //tdatos.rows[3].cells[1].valor = new Input { id = "txtUBICA", clase = Css.large, habilitado = false }.ToString();

            

            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(2, 2);
            tdatos1.rows[0].cells[0].valor = "Concepto:";
            tdatos1.rows[0].cells[1].valor = new Textarea { id = "txtCONCEPTO", clase = Css.xlarge, valor = obj.com_concepto ,  habilitado=habilitado}.ToString() ;
            
            tdatos1.rows[1].cells[0].valor = "Agente:";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtCODVEN", autocomplete = "GetPersonaObj", clase = Css.small, habilitado = false }.ToString() + " " + new Input { id = "txtVENDEDOR", clase = Css.large, habilitado = false }.ToString();

            //tdatos1.rows[2].cells[0].valor = "Bodega:";
            //tdatos1.rows[2].cells[1].valor = new Input { id = "txtCODBOD", autocomplete = "GetBodegaObj", clase = Css.small }.ToString() + " " + new Input { id = "txtBODEGA", clase = Css.large, habilitado = false }.ToString();



            html.AppendLine(tdatos1.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");


            html.AppendLine(new Input { id = "txtESTADO", valor = obj.com_estado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCERRADO", valor = Constantes.cEstadoMayorizado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCREADO", valor = Constantes.cEstadoProceso, visible = false }.ToString());


            return html.ToString();





        }


        [WebMethod]
        public static string GetCabecera(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            
            /*comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);
            */
            return ShowObject(comprobante);
            //return ShowObject(new Comprobante());
        }



        [WebMethod]
        public static string GetCabeceraFromFAC(object objeto)
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
            if (comprobante.com_tipodoc == Constantes.cObligacion.tpd_codigo)
            {
                tdatos.rows[0].cells[0].valor = "Proveedor:";
                tdatos.rows[0].cells[1].valor = new Input { id = "txtCODCLIPRO", valor = persona.per_id, autocomplete = "GetProveedorObj", obligatorio = true, clase = Css.medium, placeholder = "Proveedor" }.ToString() + " " + new Input { id = "txtNOMBRES", clase = Css.large, habilitado = false, valor = persona.per_apellidos + " " + persona.per_nombres }.ToString() + new Input { id = "txtCODPER", visible = false, valor = persona.per_codigo }.ToString() + new Boton { small = true, id = "btncallper", tooltip = "Agregar cliente", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cProveedor + ")" }.ToString() + new Boton { small = true, id = "btncleanper", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();
            }
            else
            {
                tdatos.rows[0].cells[0].valor = "Cliente:";
                tdatos.rows[0].cells[1].valor = new Input { id = "txtCODCLIPRO", valor = persona.per_id, autocomplete = "GetClienteObj", obligatorio = true, clase = Css.medium, placeholder = "Cliente" }.ToString() + " " + new Input { id = "txtNOMBRES", clase = Css.large, habilitado = false, valor = persona.per_apellidos + " " + persona.per_nombres }.ToString() + new Input { id = "txtCODPER", visible = false, valor = persona.per_codigo }.ToString() + new Boton { small = true, id = "btncallper", tooltip = "Agregar cliente", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cCliente + ")" }.ToString() + new Boton { small = true, id = "btncleanper", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();
            }
            tdatos.rows[1].cells[0].valor = "CI-RUC/Razón Social:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtRUC", clase = Css.medium, habilitado = false, valor = persona.per_ciruc }.ToString() + " " + new Input { id = "txtRAZON", clase = Css.large, habilitado = false, valor = persona.per_razon }.ToString();
            tdatos.rows[2].cells[0].valor = "Teléfono/Dirección:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtTELEFONO", clase = Css.medium, habilitado = false, valor = persona.per_telefono }.ToString() + " " + new Input { id = "txtDIRECCION", clase = Css.large, habilitado = false, valor = persona.per_direccion }.ToString();
            //tdatos.rows[3].cells[0].valor = "Ubicación:";
            //tdatos.rows[3].cells[1].valor = new Input { id = "txtUBICA", clase = Css.large, habilitado = false }.ToString();



            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(3, 2);
            tdatos1.rows[0].cells[0].valor = "Descuento Planilla:";
            tdatos1.rows[0].cells[1].valor = "%" + new Input { id = "txtPORCENTAJEPLA", clase = Css.mini, valor = 0,  habilitado = true }.ToString() + " $ " + new Input { id = "txtVALORPLA", clase = Css.mini, valor = 0, habilitado = true }.ToString();
            tdatos1.rows[1].cells[0].valor = "Concepto:";
            tdatos1.rows[1].cells[1].valor = new Textarea { id = "txtCONCEPTO", clase = Css.xlarge, valor = "CANCELA COMPROBANTE "+ comprobante.com_doctran,  habilitado = true }.ToString();

            tdatos1.rows[2].cells[0].valor = "Agente:";
            tdatos1.rows[2].cells[1].valor = new Input { id = "txtCODVEN", autocomplete = "GetPersonaObj", clase = Css.small, habilitado = false }.ToString() + " " + new Input { id = "txtVENDEDOR", clase = Css.large, habilitado = false }.ToString();

            //tdatos1.rows[2].cells[0].valor = "Bodega:";
            //tdatos1.rows[2].cells[1].valor = new Input { id = "txtCODBOD", autocomplete = "GetBodegaObj", clase = Css.small }.ToString() + " " + new Input { id = "txtBODEGA", clase = Css.large, habilitado = false }.ToString();



            html.AppendLine(tdatos1.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");


            html.AppendLine(new Input { id = "txtESTADO", valor = "", visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCERRADO", valor = Constantes.cEstadoMayorizado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCREADO", valor = Constantes.cEstadoProceso, visible = false }.ToString());


            return html.ToString();


        }

        [WebMethod]
        public static string GetCabeceraFromLGC(object objeto)
        {

            Comprobante planilla = new Comprobante(objeto);
            planilla.com_empresa_key = planilla.com_empresa;
            planilla.com_codigo_key = planilla.com_codigo;
            planilla = ComprobanteBLL.GetByPK(planilla);

            Persona persona = new Persona();

            List<Planillacomprobante> lst = PlanillacomprobanteBLL.GetAll(new WhereParams("pco_empresa ={0} and pco_comprobante_pla={1}", planilla.com_empresa, planilla.com_codigo), "");
            if (lst.Count > 0)
            {


                Comprobante comprobante = new Comprobante();
                comprobante.com_empresa = lst[0].pco_empresa;
                comprobante.com_empresa_key = lst[0].pco_empresa;
                comprobante.com_codigo = lst[0].pco_comprobante_fac;
                comprobante.com_codigo_key = lst[0].pco_comprobante_fac;
                comprobante = ComprobanteBLL.GetByPK(comprobante);


                comprobante.total = new Total();
                comprobante.total.tot_empresa = comprobante.com_empresa;
                comprobante.total.tot_empresa_key = comprobante.com_empresa;
                comprobante.total.tot_comprobante = comprobante.com_codigo;
                comprobante.total.tot_comprobante_key = comprobante.com_codigo;
                comprobante.total = TotalBLL.GetByPK(comprobante.total);

                persona = new Persona();
                persona.per_empresa = comprobante.com_empresa;
                persona.per_empresa_key = comprobante.com_empresa;
                if (comprobante.com_codclipro.HasValue)
                {
                    persona.per_codigo = comprobante.com_codclipro.Value;
                    persona.per_codigo_key = comprobante.com_codclipro.Value;
                    persona = PersonaBLL.GetByPK(persona);
                }
            }
            else
            {
                persona = new Persona();
                persona.per_empresa = planilla.com_empresa;
                persona.per_empresa_key = planilla.com_empresa;
                if (planilla.com_codclipro.HasValue)
                {
                    persona.per_codigo = planilla.com_codclipro.Value;
                    persona.per_codigo_key = planilla.com_codclipro.Value;
                    persona = PersonaBLL.GetByPK(persona);
                }

            }

            List<Tab> tabs = new List<Tab>();


            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Cliente:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtCODCLIPRO", valor = persona.per_id, autocomplete = "GetClienteObj", obligatorio = true, clase = Css.medium, placeholder = "Cliente" }.ToString() + " " + new Input { id = "txtNOMBRES", clase = Css.large, habilitado = false, valor = persona.per_apellidos + " " + persona.per_nombres }.ToString() + new Input { id = "txtCODPER", visible = false, valor = persona.per_codigo }.ToString() + new Boton { small = true, id = "btncallper", tooltip = "Agregar cliente", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cCliente + ")" }.ToString() + new Boton { small = true, id = "btncleanper", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();

            tdatos.rows[1].cells[0].valor = "CI-RUC/Razón Social:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtRUC", clase = Css.medium, habilitado = false, valor = persona.per_ciruc }.ToString() + " " + new Input { id = "txtRAZON", clase = Css.large, habilitado = false, valor = persona.per_razon }.ToString();
            tdatos.rows[2].cells[0].valor = "Teléfono/Dirección:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtTELEFONO", clase = Css.medium, habilitado = false, valor = persona.per_telefono }.ToString() + " " + new Input { id = "txtDIRECCION", clase = Css.large, habilitado = false, valor = persona.per_direccion }.ToString();
            //tdatos.rows[3].cells[0].valor = "Ubicación:";
            //tdatos.rows[3].cells[1].valor = new Input { id = "txtUBICA", clase = Css.large, habilitado = false }.ToString();



            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(3, 2);
            tdatos1.rows[0].cells[0].valor = "Descuento Planilla:";
            tdatos1.rows[0].cells[1].valor = "%" + new Input { id = "txtPORCENTAJEPLA", clase = Css.mini, valor = 0, habilitado = true }.ToString() + " $ " + new Input { id = "txtVALORPLA", clase = Css.mini, valor = 0, habilitado = true }.ToString();
            tdatos1.rows[1].cells[0].valor = "Concepto:";
            tdatos1.rows[1].cells[1].valor = new Textarea { id = "txtCONCEPTO", clase = Css.xlarge, valor = "CANCELA PLANILLA CLIENTES " + planilla.com_doctran, habilitado = true }.ToString();

            tdatos1.rows[2].cells[0].valor = "Agente:";
            tdatos1.rows[2].cells[1].valor = new Input { id = "txtCODVEN", autocomplete = "GetPersonaObj", clase = Css.small, habilitado = false }.ToString() + " " + new Input { id = "txtVENDEDOR", clase = Css.large, habilitado = false }.ToString();

            //tdatos1.rows[2].cells[0].valor = "Bodega:";
            //tdatos1.rows[2].cells[1].valor = new Input { id = "txtCODBOD", autocomplete = "GetBodegaObj", clase = Css.small }.ToString() + " " + new Input { id = "txtBODEGA", clase = Css.large, habilitado = false }.ToString();



            html.AppendLine(tdatos1.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");


            html.AppendLine(new Input { id = "txtESTADO", valor = "", visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCERRADO", valor = Constantes.cEstadoMayorizado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCREADO", valor = Constantes.cEstadoProceso, visible = false }.ToString());


            return html.ToString();




        }


        [WebMethod]
        public static string GetDetalle(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            comprobante.recibos = DreciboBLL.GetAll(new WhereParams("dfp_empresa={0} and dfp_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "dfp_secuencia");

            bool habilitado = true;
            if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;


            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdinvoice";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura"; 
            if (tipodoc == Constantes.cPago.tpd_codigo)
            {
                tdatos.AddColumn("Tipo", "width10", "", new Input() { id = "txtIDTIPO", placeholder = "TIPO", autocomplete = "GetTipoPagoProvObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODTIPO", visible = false }.ToString());
            }
            else
            {
                tdatos.AddColumn("Tipo", "width10", "", new Input() { id = "txtIDTIPO", placeholder = "TIPO", autocomplete = "GetTipoPagoCliObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODTIPO", visible = false }.ToString());
            }
        //    tdatos.AddColumn("Tipo", "width10", "", new Input() { id = "txtIDTIPO", placeholder = "TIPO", autocomplete = "GetTipoPagoObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODTIPO", visible = false }.ToString());
            tdatos.AddColumn("Descripción", "width30", "", new Input() { id = "txtNOMBRETIPO", placeholder = "DESCRIPCION", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("Valor", "width10", Css.right, new Input() { id = "txtVALOR", placeholder = "VALOR", clase = Css.blocklevel + Css.amount }.ToString());
            if (comprobante.com_estado != Constantes.cEstadoMayorizado)
            tdatos.AddColumn("", "width5", Css.center, new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());

            //tdatos.editable = true;
            tdatos.editable = habilitado;
            tdatos.selectable = true;

            foreach (Drecibo item in comprobante.recibos)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-codtipo='" + item.dfp_tipopago + "' data-nrodocumento='" + item.dfp_nro_documento + "' data-nrocuenta='" + item.dfp_nro_cuenta + "' data-emisor='" + item.dfp_emisor + "' data-banco='" + item.dfp_banco + "' data-nrocheque='" + item.dfp_nro_cheque + "' data-beneficiario='" + item.dfp_beneficiario + "' data-fecha='" + item.dfp_fecha_ven+"'";
                //row.data = "{\"data-codtipo\":\"" + item.dfp_tipopago + "\", \"data-nrodocumentos\":\"" + item.dfp_nro_documento + "\"}' ";
                row.removable = true;

                row.cells.Add(new HtmlCell { valor = item.dfp_tipopagoid});
                row.cells.Add(new HtmlCell { valor = item.dfp_tipopagonombre});
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.dfp_monto), clase = Css.right });
                if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString(), clase = Css.center });

                if (!habilitado)
                {
                    row.selectevent = "SelectRow(this)";
                }
                else

                    row.clickevent = "Edit(this)";

                tdatos.AddRow(row);

            }

            html.AppendLine(tdatos.ToString());



            return html.ToString();
        }

        [WebMethod]
        public static string GetDetalleFromFAC(object objeto)
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
            tdatos.id = "tdinvoice";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura"; 
            if (tipodoc == Constantes.cPago.tpd_codigo)
            {
                tdatos.AddColumn("Tipo", "width10", "", new Input() { id = "txtIDTIPO", placeholder = "TIPO", autocomplete = "GetTipoPagoProvObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODTIPO", visible = false }.ToString());
            }
            else
            {
                tdatos.AddColumn("Tipo", "width10", "", new Input() { id = "txtIDTIPO", placeholder = "TIPO", autocomplete = "GetTipoPagoCliObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODTIPO", visible = false }.ToString());
            }
            //    tdatos.AddColumn("Tipo", "width10", "", new Input() { id = "txtIDTIPO", placeholder = "TIPO", autocomplete = "GetTipoPagoObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODTIPO", visible = false }.ToString());
            tdatos.AddColumn("Descripción", "width30", "", new Input() { id = "txtNOMBRETIPO", placeholder = "DESCRIPCION", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("Valor", "width10", Css.right, new Input() { id = "txtVALOR", placeholder = "VALOR", clase = Css.blocklevel + Css.amount }.ToString());
            //if (comprobante.com_estado != Constantes.cEstadoMayorizado)
            tdatos.AddColumn("", "width5", Css.center, new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());

            tdatos.editable = true;

            Tipopago tpa = TipopagoBLL.GetByPK(new Tipopago { tpa_codigo = 1, tpa_codigo_key = 1, tpa_empresa = comprobante.com_empresa, tpa_empresa_key = comprobante.com_empresa });

            HtmlRow row = new HtmlRow();
            row.data = "data-codtipo='" + tpa.tpa_codigo+ "' data-nrodocumento='' data-nrocuenta='' data-emisor='' data-banco='' data-nrocheque='' data-beneficiario='' data-fecha=''";
            row.removable = true;

            row.cells.Add(new HtmlCell { valor = tpa.tpa_id });
            row.cells.Add(new HtmlCell { valor = tpa.tpa_nombre});
            row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(comprobante.total.tot_total), clase = Css.right });
            //if (comprobante.com_estado != Constantes.cEstadoMayorizado)
            row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString(), clase = Css.center });
            row.clickevent = "Edit(this)";
            tdatos.AddRow(row);

            
            //tdatos.AddRow(new HtmlRow(item.ddoc_productoid, item.ddoc_productonombre, item.ddoc_observaciones, item.ddoc_productounidad, item.ddoc_cantidad, item.ddoc_precio, item.ddoc_dscitem, item.ddoc_total, item.ddoc_productoiva) { data = "data-codpro=" + item.ddoc_producto });   



            html.AppendLine(tdatos.ToString());



            return html.ToString();
        }

        [WebMethod]
        public static string GetDetalleFromLGC(object objeto)
        {

            Comprobante planilla = new Comprobante(objeto);
            planilla.com_empresa_key = planilla.com_empresa;
            planilla.com_codigo_key = planilla.com_codigo;
            planilla = ComprobanteBLL.GetByPK(planilla);

            string tpaseg = Constantes.GetParameter("cobroseguro");

            List<Planillacomprobante> lst = PlanillacomprobanteBLL.GetAll(new WhereParams("pco_empresa ={0} and pco_comprobante_pla={1}", planilla.com_empresa, planilla.com_codigo), "");
            if (lst.Count > 0)
            {


                Comprobante comprobante = new Comprobante();
                comprobante.com_empresa = lst[0].pco_empresa;
                comprobante.com_empresa_key = lst[0].pco_empresa;
                comprobante.com_codigo = lst[0].pco_comprobante_fac;
                comprobante.com_codigo_key = lst[0].pco_comprobante_fac;
                comprobante = ComprobanteBLL.GetByPK(comprobante);



                comprobante.total = new Total();
                comprobante.total.tot_empresa = comprobante.com_empresa;
                comprobante.total.tot_empresa_key = comprobante.com_empresa;
                comprobante.total.tot_comprobante = comprobante.com_codigo;
                comprobante.total.tot_comprobante_key = comprobante.com_codigo;
                comprobante.total = TotalBLL.GetByPK(comprobante.total);


                StringBuilder html = new StringBuilder();
                HtmlTable tdatos = new HtmlTable();
                tdatos.id = "tdinvoice";
                tdatos.invoice = true;
                //tdatos.titulo = "Factura"; 
                tdatos.AddColumn("Tipo", "width10", "", new Input() { id = "txtIDTIPO", placeholder = "TIPO", autocomplete = "GetTipoPagoCliObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODTIPO", visible = false }.ToString());
                //    tdatos.AddColumn("Tipo", "width10", "", new Input() { id = "txtIDTIPO", placeholder = "TIPO", autocomplete = "GetTipoPagoObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODTIPO", visible = false }.ToString());
                tdatos.AddColumn("Descripción", "width30", "", new Input() { id = "txtNOMBRETIPO", placeholder = "DESCRIPCION", clase = Css.blocklevel, habilitado = false }.ToString());
                tdatos.AddColumn("Valor", "width10", Css.right, new Input() { id = "txtVALOR", placeholder = "VALOR", clase = Css.blocklevel + Css.amount }.ToString());
                //if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                tdatos.AddColumn("", "width5", Css.center, new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());

                tdatos.editable = true;

                Tipopago tpa = TipopagoBLL.GetByPK(new Tipopago { tpa_codigo = 1, tpa_codigo_key = 1, tpa_empresa = comprobante.com_empresa, tpa_empresa_key = comprobante.com_empresa });
                decimal totalefec = comprobante.total.tot_total;                
                
                HtmlRow row = new HtmlRow();
                if (!string.IsNullOrEmpty(tpaseg) && (comprobante.total.tot_tseguro ?? 0) > 0)
                {
                    decimal totalseguro = comprobante.total.tot_tseguro.Value;
                    totalefec = totalefec - totalseguro;

                    Tipopago tpas = TipopagoBLL.GetByPK(new Tipopago { tpa_codigo = int.Parse(tpaseg), tpa_codigo_key = int.Parse(tpaseg), tpa_empresa = comprobante.com_empresa, tpa_empresa_key = comprobante.com_empresa });
                    row = new HtmlRow();
                    row.data = "data-codtipo='" + tpas.tpa_codigo + "' data-nrodocumento='' data-nrocuenta='' data-emisor='' data-banco='' data-nrocheque='' data-beneficiario='' data-fecha=''";
                    row.removable = true;

                    row.cells.Add(new HtmlCell { valor = tpas.tpa_id });
                    row.cells.Add(new HtmlCell { valor = tpas.tpa_nombre });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(totalseguro), clase = Css.right });
                    //if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                    row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString(), clase = Css.center });
                    row.clickevent = "Edit(this)";
                    tdatos.AddRow(row);


                }

                row = new HtmlRow();
                row.data = "data-codtipo='" + tpa.tpa_codigo + "' data-nrodocumento='' data-nrocuenta='' data-emisor='' data-banco='' data-nrocheque='' data-beneficiario='' data-fecha=''";
                row.removable = true;

                row.cells.Add(new HtmlCell { valor = tpa.tpa_id });
                row.cells.Add(new HtmlCell { valor = tpa.tpa_nombre });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(totalefec), clase = Css.right });
                //if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString(), clase = Css.center });
                row.clickevent = "Edit(this)";
                tdatos.AddRow(row);


                //tdatos.AddRow(new HtmlRow(item.ddoc_productoid, item.ddoc_productonombre, item.ddoc_observaciones, item.ddoc_productounidad, item.ddoc_cantidad, item.ddoc_precio, item.ddoc_dscitem, item.ddoc_total, item.ddoc_productoiva) { data = "data-codpro=" + item.ddoc_producto });   



                html.AppendLine(tdatos.ToString());



                return html.ToString();
            }
            else
            {
                planilla.total = new Total();
                planilla.total.tot_empresa = planilla.com_empresa;
                planilla.total.tot_empresa_key = planilla.com_empresa;
                planilla.total.tot_comprobante = planilla.com_codigo;
                planilla.total.tot_comprobante_key = planilla.com_codigo;
                planilla.total = TotalBLL.GetByPK(planilla.total);


                StringBuilder html = new StringBuilder();
                HtmlTable tdatos = new HtmlTable();
                tdatos.id = "tdinvoice";
                tdatos.invoice = true;
                //tdatos.titulo = "Factura"; 
                tdatos.AddColumn("Tipo", "width10", "", new Input() { id = "txtIDTIPO", placeholder = "TIPO", autocomplete = "GetTipoPagoCliObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODTIPO", visible = false }.ToString());
                //    tdatos.AddColumn("Tipo", "width10", "", new Input() { id = "txtIDTIPO", placeholder = "TIPO", autocomplete = "GetTipoPagoObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODTIPO", visible = false }.ToString());
                tdatos.AddColumn("Descripción", "width30", "", new Input() { id = "txtNOMBRETIPO", placeholder = "DESCRIPCION", clase = Css.blocklevel, habilitado = false }.ToString());
                tdatos.AddColumn("Valor", "width10", Css.right, new Input() { id = "txtVALOR", placeholder = "VALOR", clase = Css.blocklevel + Css.amount }.ToString());
                //if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                tdatos.AddColumn("", "width5", Css.center, new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());

                tdatos.editable = true;

                Tipopago tpa = TipopagoBLL.GetByPK(new Tipopago { tpa_codigo = 1, tpa_codigo_key = 1, tpa_empresa = planilla.com_empresa, tpa_empresa_key = planilla.com_empresa });
                decimal totalefec = planilla.total.tot_total;
                HtmlRow row = new HtmlRow();


                if (!string.IsNullOrEmpty(tpaseg) && (planilla.total.tot_tseguro ?? 0) > 0)
                {
                    decimal totalseguro = planilla.total.tot_tseguro.Value;
                    totalefec = totalefec - totalseguro;

                    Tipopago tpas = TipopagoBLL.GetByPK(new Tipopago { tpa_codigo = int.Parse(tpaseg), tpa_codigo_key = int.Parse(tpaseg), tpa_empresa = planilla.com_empresa, tpa_empresa_key = planilla.com_empresa });
                    row = new HtmlRow();
                    row.data = "data-codtipo='" + tpas.tpa_codigo + "' data-nrodocumento='' data-nrocuenta='' data-emisor='' data-banco='' data-nrocheque='' data-beneficiario='' data-fecha=''";
                    row.removable = true;

                    row.cells.Add(new HtmlCell { valor = tpas.tpa_id });
                    row.cells.Add(new HtmlCell { valor = tpas.tpa_nombre });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(totalseguro), clase = Css.right });
                    //if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                    row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString(), clase = Css.center });
                    row.clickevent = "Edit(this)";
                    tdatos.AddRow(row);


                }


                row = new HtmlRow();

                row.data = "data-codtipo='" + tpa.tpa_codigo + "' data-nrodocumento='' data-nrocuenta='' data-emisor='' data-banco='' data-nrocheque='' data-beneficiario='' data-fecha=''";
                row.removable = true;

                row.cells.Add(new HtmlCell { valor = tpa.tpa_id });
                row.cells.Add(new HtmlCell { valor = tpa.tpa_nombre });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(totalefec), clase = Css.right });
                //if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString(), clase = Css.center });
                row.clickevent = "Edit(this)";
                tdatos.AddRow(row);


                //tdatos.AddRow(new HtmlRow(item.ddoc_productoid, item.ddoc_productonombre, item.ddoc_observaciones, item.ddoc_productounidad, item.ddoc_cantidad, item.ddoc_precio, item.ddoc_dscitem, item.ddoc_total, item.ddoc_productoiva) { data = "data-codpro=" + item.ddoc_producto });   



                html.AppendLine(tdatos.ToString());



                return html.ToString();
           
            }
            return "";
        }

        [WebMethod]
        public static string GetPie(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            bool habilitado = true;
            if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;

            comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);


            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tddatos";
            tdatos.CreteEmptyTable(8, 2);
            tdatos.rows[0].cells[0].valor = "Emisor:";
            tdatos.rows[0].cells[1].valor = new Input() { id = "txtEMISOR", placeholder = "EMISOR", autocomplete = "GetEmisorObj", clase = Css.large, habilitado=habilitado}.ToString();

            tdatos.rows[1].cells[0].valor = "Nro. Documento:";
            tdatos.rows[1].cells[1].valor = new Input() { id = "txtNRODOCUMENTO", placeholder = "NRO DOCUMENTO", clase = Css.medium, habilitado = habilitado }.ToString();
            tdatos.rows[2].cells[0].valor = "Nro. Cuenta:";
            tdatos.rows[2].cells[1].valor = new Input() { id = "txtNROCUENTA", placeholder = "NRO CUENTA", clase = Css.medium, habilitado = habilitado }.ToString();
            tdatos.rows[3].cells[0].valor = "Banco:";
            tdatos.rows[3].cells[1].valor = new Select() { id = "cmbBANCO", diccionario = Dictionaries.GetBancos(), clase = Css.medium, habilitado = habilitado }.ToString();
            tdatos.rows[4].cells[0].valor = "Nro. Cheque:";
            tdatos.rows[4].cells[1].valor = new Input() { id = "txtNROCHEQUE", placeholder = "NRO CHEQUE", clase = Css.medium, habilitado = habilitado }.ToString();
            tdatos.rows[5].cells[0].valor = "Beneficiario:";
            tdatos.rows[5].cells[1].valor = new Input() { id = "txtBENEFICIARIO", placeholder = "BENEFICIARIO", clase = Css.medium, habilitado = habilitado }.ToString();
            tdatos.rows[6].cells[0].valor = "Fecha Vence:";
            tdatos.rows[6].cells[1].valor = new Input() { id = "txtFECHAVENCE", datepicker = true, datetimevalor = DateTime.Now, clase = Css.medium, habilitado = habilitado }.ToString();

            tdatos.rows[7].cells[0].valor = "Cuenta:";
            tdatos.rows[7].cells[1].valor = new Input() { id = "txtIDCUENTA", placeholder = "ID CUENTA", clase = Css.small, habilitado = habilitado }.ToString() + " " + new Input() { id = "txtNOMBRECUENTA", placeholder = "CUENTA", clase = Css.medium, habilitado = false }.ToString();

            html.AppendLine(tdatos.ToString());

            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(1, 2);
            
            tdatos1.rows[0].cells[0].valor = "TOTAL";            
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtTOTALCOM", clase = Css.medium + Css.totalamount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_total) }.ToString();

            html.AppendLine(tdatos1.ToString());

            return html.ToString();

        }

        [WebMethod]
        public static string GetPieFrom(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            bool habilitado = true;
            
            comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);


            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tddatos";
            tdatos.CreteEmptyTable(8, 2);
            tdatos.rows[0].cells[0].valor = "Emisor:";
            tdatos.rows[0].cells[1].valor = new Input() { id = "txtEMISOR", placeholder = "EMISOR", autocomplete = "GetEmisorObj", clase = Css.large, habilitado = habilitado }.ToString();

            tdatos.rows[1].cells[0].valor = "Nro. Documento:";
            tdatos.rows[1].cells[1].valor = new Input() { id = "txtNRODOCUMENTO", placeholder = "NRO DOCUMENTO", clase = Css.medium, habilitado = habilitado }.ToString();
            tdatos.rows[2].cells[0].valor = "Nro. Cuenta:";
            tdatos.rows[2].cells[1].valor = new Input() { id = "txtNROCUENTA", placeholder = "NRO CUENTA", clase = Css.medium, habilitado = habilitado }.ToString();
            tdatos.rows[3].cells[0].valor = "Banco:";
            tdatos.rows[3].cells[1].valor = new Select() { id = "cmbBANCO", diccionario = Dictionaries.GetBancos(), clase = Css.medium, habilitado = habilitado }.ToString();
            tdatos.rows[4].cells[0].valor = "Nro. Cheque:";
            tdatos.rows[4].cells[1].valor = new Input() { id = "txtNROCHEQUE", placeholder = "NRO CHEQUE", clase = Css.medium, habilitado = habilitado }.ToString();
            tdatos.rows[5].cells[0].valor = "Beneficiario:";
            tdatos.rows[5].cells[1].valor = new Input() { id = "txtBENEFICIARIO", placeholder = "BENEFICIARIO", clase = Css.medium, habilitado = habilitado }.ToString();
            tdatos.rows[6].cells[0].valor = "Fecha Vence:";
            tdatos.rows[6].cells[1].valor = new Input() { id = "txtFECHAVENCE", datepicker = true, datetimevalor = DateTime.Now, clase = Css.medium, habilitado = habilitado }.ToString();

            tdatos.rows[7].cells[0].valor = "Cuenta:";
            tdatos.rows[7].cells[1].valor = new Input() { id = "txtIDCUENTA", placeholder = "ID CUENTA", clase = Css.small, habilitado = habilitado }.ToString() + " " + new Input() { id = "txtNOMBRECUENTA", placeholder = "CUENTA", clase = Css.medium, habilitado = false }.ToString();

            html.AppendLine(tdatos.ToString());

            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(1, 2);

            tdatos1.rows[0].cells[0].valor = "TOTAL";
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtTOTALCOM", clase = Css.medium + Css.totalamount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_total) }.ToString();

            html.AppendLine(tdatos1.ToString());

            return html.ToString();

        }

     


        [WebMethod]
        public static string CloseObject(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object objetocomp = null;
            object objetoafec = null;

            tmp.TryGetValue("recibo", out objetocomp);
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

            try
            {

               
                FAC.account_recibo(obj);
                //obj = FAC.account_factura(obj); //FALTA LA CONTABILIDAD DE LA CANCELACION


                return "OK";
            }
            catch (Exception ex)
            {
                return "ERROR";
            }
        }

        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object objetocomp = null;
            object objetoafec= null;
        
            tmp.TryGetValue("recibo", out objetocomp);
            tmp.TryGetValue("afectacion", out objetoafec);
            
           
            Comprobante obj = new Comprobante(objetocomp);
            
          
            List<Dcancelacion> detalle = new List<Dcancelacion>();
             
          
            if (objetoafec!= null)
            {
                Array array = (Array)objetoafec;
                
                foreach (Object item in array)
                {
                    if (item != null)
                        
                        detalle.Add(new Dcancelacion(item));
                }
            }
            obj.cancelaciones = detalle;

            foreach (Drecibo item in obj.recibos)
            {

                //CONTROLE DE RETENCIONES
                //Formas de pago con retencion
                if (item.dfp_tipopago == 7 || item.dfp_tipopago == 8 || item.dfp_tipopago == 16)
                {
                    if (item.dfp_nro_documento == "")
                        throw new System.ArgumentException("Es necesario ingresar el numero de retención", "(Nro Documento)");

                    if (detalle.Count > 0)
                    {
                        List<vCancelacionDetalle> list = vCancelacionDetalleBLL.GetAll(new WhereParams("f.com_codigo={0} and f.com_estado={1} and dfp_nro_documento={2} ", detalle[0].dca_comprobante, 2, item.dfp_nro_documento), "");
                        if (list.Count > 0 && item.dfp_nro_documento != "")
                        {
                            throw new System.ArgumentException("El número de retención " + item.dfp_nro_documento + " ya ha sido ingresado anteriormente", "(Nro Documento)");

                            //return "-1";
                        }
                    }
                }

                //CONTROL DE CHEQUES
                //Formas de pago con cheque
                if (item.dfp_tipopago == 2 || item.dfp_tipopago == 12 || item.dfp_tipopago == 14 || item.dfp_tipopago == 24)
                {
                    if (item.dfp_nro_cheque == "")
                    {
                        throw new System.ArgumentException("Es necesario ingresar el numero de cheque", "(Nro Cheque)");
                    }

                    

                     
                    if (item.dfp_tipopago == 2 || item.dfp_tipopago == 14) //Cheques cliente
                    {
                        List<vCancelacionDetalle> list = vCancelacionDetalleBLL.GetAll(new WhereParams("f.com_codclipro={0} and f.com_estado={1} and dfp_nro_cheque={2} and dfp_emisor={3} ", obj.com_codclipro, 2, item.dfp_nro_cheque, item.dfp_emisor), "");
                        if (list.Count > 0)
                        {
                            bool repetido = true;
                            foreach (vCancelacionDetalle dca in list)
                            {
                                if (dca.dca_comprobante_can == obj.com_codigo)
                                    repetido = false;

                            }
                            if (repetido)
                                throw new System.ArgumentException("El cheque Nro. " + item.dfp_nro_cheque + " del emisor " + item.dfp_emisor + " ya ha sido ingresado anteriormente", "(Nro Cheque)");
                        }



                    }
                    if (item.dfp_tipopago == 12 || item.dfp_tipopago == 24) //Cheques proveedor
                    {

                        Banco banco = BancoBLL.GetByPK(new Banco { ban_empresa = obj.com_empresa, ban_empresa_key = obj.com_empresa, ban_codigo = item.dfp_banco.Value, ban_codigo_key = item.dfp_banco.Value });
                        List<Dbancario> lst = DbancarioBLL.GetAll("dban_empresa=" + obj.com_empresa + " and dban_banco=" + item.dfp_banco + " and dban_documento='" + item.dfp_nro_cheque + "'", "");
                        if (lst.Count > 0)
                        {
                            bool repetido = true;
                            foreach (Dbancario dban in lst)
                            {
                                if (dban.dban_cco_comproba == obj.com_codigo)
                                    repetido = false;

                            }
                            if (repetido)
                                throw new System.ArgumentException("El cheque Nro. " + item.dfp_nro_cheque + " del banco " + banco.ban_nombre + " ya ha sido emitido anteriormente", "(Nro Cheque)");
                        }

                    }


                }


                    
                    

            }
          
            try
            {
               
                    if (obj.com_codigo > 0)
                        obj = FAC.update_cancelacion(obj);
                    else
                        obj = FAC.save_cancelacion(obj,false);                 
                    FAC.account_recibo(obj);
                    return obj.com_codigo.ToString();
                
            }
            catch (Exception ex)
            {
                ExceptionHandling.Log.AddExepcion(ex);
                throw ex;
                //return obj.com_codigo.ToString();
            }


         



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

            obj.com_modulo = General.GetModulo(obj.com_tipodoc); ;
            obj.com_transacc = General.GetTransacc(obj.com_tipodoc);
            obj.com_nocontable = 0;
            obj.com_estado = Constantes.cEstadoGrabado;
            obj.com_descuadre = 0;
            obj.com_adestino = 0;
            obj.com_doctran = General.GetNumeroComprobante(obj);

            obj.com_tclipro = (obj.com_tipodoc == Constantes.cRecibo.tpd_codigo) ? Constantes.cCliente : Constantes.cProveedor; 

            int debcredoc = (obj.com_tipodoc == Constantes.cRecibo.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;
            int debcrecan = (obj.com_tipodoc == Constantes.cRecibo.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                obj.com_codigo = ComprobanteBLL.InsertIdentity(transaction, obj);
                //int codigo = ComprobanteBLL.InsertIdentity(transaction, obj);

                decimal totalrecibo = 0;
                int contador = 0;
                foreach (Drecibo item in obj.recibos)
                {
                    item.dfp_comprobante = obj.com_codigo;
                    item.dfp_secuencia = contador;
                    item.dfp_debcre = debcredoc;
                    totalrecibo += item.dfp_monto; 
                    DreciboBLL.Insert(transaction, item);
                    contador++;
                }


                decimal totalcancela = 0;
                contador = 0;
                foreach (Dcancelacion dca in obj.cancelaciones)
                {
                    if (dca.dca_monto > 0)
                    {
                        dca.dca_comprobante_can = obj.com_codigo;
                        dca.dca_transacc_can = obj.com_transacc;
                        dca.dca_secuencia = contador;
                        dca.dca_debcre = debcrecan;
                        DcancelacionBLL.Insert(transaction, dca);   //GUARDA EL DETALLE DE CANCELACION
                        contador++;

                        //Cambia el estado del documento que tambien se debe actualizar
                        Ddocumento doc = new Ddocumento();
                        doc.ddo_empresa = dca.dca_empresa;
                        doc.ddo_empresa_key = dca.dca_empresa;
                        doc.ddo_comprobante = dca.dca_comprobante;
                        doc.ddo_comprobante_key = dca.dca_comprobante;
                        doc.ddo_transacc = dca.dca_transacc;
                        doc.ddo_transacc_key = dca.dca_transacc;
                        doc.ddo_doctran = dca.dca_doctran;
                        doc.ddo_doctran_key = dca.dca_doctran;
                        doc.ddo_pago = dca.dca_pago;
                        doc.ddo_pago_key = dca.dca_pago;


                        doc = DdocumentoBLL.GetByPK(doc);
                        doc.ddo_cancela = ((doc.ddo_cancela.HasValue) ? doc.ddo_cancela.Value : 0) + dca.dca_monto;
                        if (doc.ddo_cancela >= doc.ddo_monto)
                            doc.ddo_cancelado = 1;
                        totalcancela += dca.dca_monto.Value;
                        DdocumentoBLL.Update(transaction, doc);// ACTUALIZA EL DOCUMENTO CANCELANDOLO   
                    }
                    //
                }

                if (totalrecibo > totalcancela)//CREA UN DOCUMENTO LIGADO AL RECIBO EN CASO DE EXISTIR VALORES A FAVOR
                {
                    decimal valordoc = totalrecibo - totalcancela;
                    Ddocumento doc = new Ddocumento();
                    doc.ddo_empresa = obj.com_empresa;
                    doc.ddo_comprobante = obj.com_codigo;
                    doc.ddo_transacc = General.GetTransacc(obj.com_tipodoc);
                    doc.ddo_doctran = obj.com_doctran;
                    doc.ddo_pago = 1;
                    doc.ddo_codclipro = obj.com_codclipro;
                    doc.ddo_debcre = debcrecan;
                    //doc.ddo_tipo_cambio = 
                    doc.ddo_fecha_emi = obj.com_fecha;
                    doc.ddo_fecha_ven = fecha;
                    doc.ddo_monto = valordoc;
                    //doc.ddo_monto_ext = 
                    doc.ddo_cancela = 0;
                    //doc.ddo_cancela_ext =
                    doc.ddo_cancelado = 0;
                    doc.ddo_agente = obj.com_agente;
                    //doc.ddo_cuenta = 
                    doc.ddo_modulo = obj.com_modulo;
                    DdocumentoBLL.Insert(transaction, doc);
                }


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


            //obj.com_empresa = 1;
            //obj.com_periodo = fecha.Year;
            //obj.com_tipodoc = 4;
            //obj.com_ctipocom = 2; //FACT

           
            //obj.com_anulado =
            //obj.com_fecha = fecha;


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                obj.com_empresa_key = obj.com_empresa;
                obj.com_codigo_key = obj.com_codigo;
                ComprobanteBLL.Update(transaction, obj);

                obj.ccomdoc.cdoc_empresa_key = obj.ccomdoc.cdoc_empresa;
                obj.ccomdoc.cdoc_comprobante_key = obj.ccomdoc.cdoc_comprobante;
                CcomdocBLL.Update(transaction, obj.ccomdoc);

                List<Dcomdoc> lst = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa = {0} and ddoc_comprobante = {1}", obj.com_empresa, obj.com_codigo), "");
                foreach (Dcomdoc item in lst)
                {
                    DcomdocBLL.Delete(transaction, item);
                }

                int contador = 0;
                foreach (Dcomdoc item in obj.ccomdoc.detalle)
                {
                    item.ddoc_empresa = obj.com_empresa;
                    item.ddoc_comprobante = obj.com_codigo;
                    item.ddoc_secuencia = contador;
                    DcomdocBLL.Insert(transaction, item);
                    contador++;
                }


                obj.ccomenv.cenv_empresa_key = obj.ccomenv.cenv_empresa;
                obj.ccomenv.cenv_comprobante_key = obj.ccomenv.cenv_comprobante;
                CcomenvBLL.Update(transaction, obj.ccomenv);

                obj.total.tot_empresa_key = obj.total.tot_empresa;
                obj.total.tot_comprobante_key = obj.total.tot_comprobante;
                TotalBLL.Update(transaction, obj.total);

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
