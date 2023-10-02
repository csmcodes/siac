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
    public partial class wfDbancario : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string WhereClause = "";
        protected static WhereParams parametros;
        protected static int? tclipro;
        protected static int? debcre;        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txttipodoc.Text = (Request.QueryString["tipodoc"] != null) ? Request.QueryString["tipodoc"].ToString() : "-1";
                txtcodigocomp.Text = (Request.QueryString["codigocomp"] != null) ? Request.QueryString["codigocomp"].ToString() : "-1";
                pageIndex = 1;
                pageSize = 20;
                txttclipro.Text = (Request.QueryString["tclipro"] != null) ? Request.QueryString["tclipro"].ToString() : Constantes.cCliente + "";
                tclipro = Convert.ToInt32(txttclipro.Text);
                txtdebcre.Text = (Request.QueryString["debcre"] != null) ? Request.QueryString["debcre"].ToString() : Constantes.cCredito + "";
                debcre = Convert.ToInt32(txtdebcre.Text);
                txtmodify.Text = (Request.QueryString["modify"] != null) ? Request.QueryString["modify"].ToString(): null;

                /**
                 * 
                 * 
                 * Se  
                 * 
                 * */
            }
        }



        public static string ShowObject(Comprobante obj, object modify)
        {              

            bool habilitado = true;
            if (obj.com_estado == Constantes.cEstadoEliminado || obj.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;
            if (!string.IsNullOrEmpty((string)modify))
                habilitado = bool.Parse(modify.ToString());
            
            Persona persona = new Persona();
            persona.per_empresa = obj.com_empresa;
            persona.per_empresa_key = obj.com_empresa;
            if (obj.com_codclipro.HasValue)
            {
                persona.per_codigo = obj.com_codclipro.Value;
                persona.per_codigo_key = obj.com_codclipro.Value;
                persona = PersonaBLL.GetByPK(persona);
            }
            Ccomdoc ccomdoc = new Ccomdoc();
            ccomdoc.cdoc_empresa = obj.com_empresa;
            ccomdoc.cdoc_empresa_key = obj.com_empresa;
            if (obj.com_codigo > 0)
            {
                ccomdoc.cdoc_comprobante = obj.com_codigo;
                ccomdoc.cdoc_comprobante_key = obj.com_codigo;
                ccomdoc = CcomdocBLL.GetByPK(ccomdoc);
            }
            Comprobante comprobante = new Comprobante();
            comprobante.com_empresa = obj.com_empresa;
            comprobante.com_empresa_key = obj.com_empresa;
            if (ccomdoc.cdoc_factura > 0)
            {
                comprobante.com_codigo = ccomdoc.cdoc_factura.Value;
                comprobante.com_codigo_key = ccomdoc.cdoc_factura.Value;
                comprobante = ComprobanteBLL.GetByPK(comprobante);
            }


            Centro centro = new Centro();
            centro.cen_empresa = comprobante.com_empresa;
            centro.cen_empresa_key = comprobante.com_empresa;
            if (comprobante.com_centro.HasValue)
            {
                centro.cen_codigo = comprobante.com_centro.Value;
                centro.cen_codigo_key = comprobante.com_centro.Value;
                centro = CentroBLL.GetByPK(centro);
            }
            else
            {
                centro = Constantes.GetSinCentro();
            }

            List<Tab> tabs = new List<Tab>();
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(2, 2);
            tdatos.rows[0].cells[0].valor = "Concepto:";
            tdatos.rows[0].cells[1].valor = new Textarea { id = "txtCONCEPTO", valor = obj.com_concepto, obligatorio = true, clase = Css.xxlarge, placeholder = "Concepto", habilitado= habilitado }.ToString();
            tdatos.rows[1].cells[0].valor = "Centro:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtIDCEN", autocomplete = "GetCentroObj", clase = Css.small, valor = centro.cen_id, habilitado = false }.ToString() + " " + new Input { id = "txtCENTRO", clase = Css.large, habilitado = false, valor = centro.cen_nombre }.ToString() + " " + new Input { id = "txtCODCEN", visible = false, valor = centro.cen_codigo }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");
          /*  HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(3, 2);
            tdatos1.rows[0].cells[0].valor = "Concepto:";
            tdatos1.rows[0].cells[1].valor = new Textarea { id = "txtCONCEPTO", clase = Css.xlarge, valor = obj.com_concepto, habilitado = true }.ToString() + new Input { id = "txtTCLIPRO", visible = false, valor = (obj.com_tclipro.HasValue) ? obj.com_tclipro.Value : tclipro }.ToString();
            tdatos1.rows[1].cells[0].valor = "Agente:";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtCODVEN", autocomplete = "GetPersonaObj", clase = Css.small, habilitado = false }.ToString() + " " + new Input { id = "txtVENDEDOR", clase = Css.large, habilitado = false }.ToString();
            tdatos1.rows[2].cells[0].valor = "Comprobante:";
            tdatos1.rows[2].cells[1].valor = new Input { id = "txtCODCOM", valor = comprobante.com_doctran, autocomplete = "GetComprobanteObj", clase = Css.large, habilitado = true, obligatorio = true }.ToString() + " " + new Input { id = "txtCOMPROBANTE", visible = false, valor = ccomdoc.cdoc_factura }.ToString();
            //tdatos1.rows[2].cells[0].valor = "Bodega:";
            //tdatos1.rows[2].cells[1].valor = new Input { id = "txtCODBOD", autocomplete = "GetBodegaObj", clase = Css.small }.ToString() + " " + new Input { id = "txtBODEGA", clase = Css.large, habilitado = false }.ToString();
            html.AppendLine(tdatos1.ToString());*/
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

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object modify = null;
            tmp.TryGetValue("modify", out modify);          


            /*comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);
            */
            return ShowObject(comprobante, modify);
            //return ShowObject(new Comprobante());
        }


        [WebMethod]
        public static string GetDetalle(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);
            comprobante.bancario = DbancarioBLL.GetAll(new WhereParams("dban_empresa={0} and dban_cco_comproba={1}", comprobante.com_empresa, comprobante.com_codigo), "dban_secuencia");

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object modify = null;
            tmp.TryGetValue("modify", out modify);          

            bool habilitado = true;
            if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;
            if (!string.IsNullOrEmpty((string)modify))
                habilitado = bool.Parse(modify.ToString());

            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdinvoice";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";        
            tdatos.AddColumn("Id", "width10", "", new Input() { id = "txtIDBANCO", placeholder = "ID", autocomplete = "GetBancoObj", clase = Css.blocklevel, obligatorio = true }.ToString() + new Input() { id = "txtCODBANCO", visible = false }.ToString());
            tdatos.AddColumn("Banco", "width20", "", new Input() { id = "txtNOMBREBANCO", placeholder = "BANCO", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("Cuenta", "width10", "", new Input() { id = "txtCUENTA", placeholder = "CUENTA", clase = Css.blocklevel, habilitado = false }.ToString() );                                
            tdatos.AddColumn("Documento", "width10", "", new Input() { id = "txtDOCUMENTO", placeholder = "DOCUMENTO", clase = Css.blocklevel + Css.amount,obligatorio=true }.ToString());
            tdatos.AddColumn("Beneficiario", "width20", "", new Input() { id = "txtBENEFICIARIO", placeholder = "BENEFICIARIO", clase = Css.blocklevel, obligatorio = true }.ToString());
            if (debcre == Constantes.cCredito)
            {
                tdatos.AddColumn("Debito", "width10", Css.right, new Input() { id = "txtDEBITO", clase = Css.blocklevel + Css.amount, valor = Formatos.CurrencyFormat(0), habilitado = false, numeric = true }.ToString());
                tdatos.AddColumn("Credito", "width10", Css.right, new Input() { id = "txtCREDITO", clase = Css.blocklevel + Css.amount, valor = Formatos.CurrencyFormat(0), habilitado = true, obligatorio = true, numeric = true }.ToString());
            }
            else
            {
                tdatos.AddColumn("Debito", "width10", Css.right, new Input() { id = "txtDEBITO", clase = Css.blocklevel + Css.amount, valor = Formatos.CurrencyFormat(0), habilitado = true, obligatorio = true, numeric = true }.ToString());
                tdatos.AddColumn("Credito", "width10", Css.right, new Input() { id = "txtCREDITO", clase = Css.blocklevel + Css.amount, valor = Formatos.CurrencyFormat(0), habilitado = false, numeric = true }.ToString());
            }
            if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                tdatos.AddColumn("", "width5", Css.center, new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());
            else
                tdatos.AddColumn("", "width5", Css.center, "");
            tdatos.editable = habilitado;
            foreach (Dbancario item in comprobante.bancario)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-codcue=" + item.dban_banco + " data-beneficiario=" + item.dban_beneficiario;
             //   row.data = "data-beneficiario=" + item.dban_beneficiario;
                row.removable = true;
                row.cells.Add(new HtmlCell { valor = item.dban_bancoid });
                row.cells.Add(new HtmlCell { valor = item.dban_banconombre });
                row.cells.Add(new HtmlCell { valor = item.dban_bancocuenta });
                row.cells.Add(new HtmlCell { valor = item.dban_documento });
                row.cells.Add(new HtmlCell { valor = item.dban_beneficiario});
                if (item.dban_debcre == Constantes.cCredito)
                {
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(0), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.dban_valor_nac), clase = Css.right });
                }
                else
                {                  
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.dban_valor_nac), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(0), clase = Css.right });
                }
               // row.cells.Add(new HtmlCell { valor = ((item.dnc_cheque.HasValue) ? ((item.dnc_cheque.Value == 1) ? "SI" : "NO") : "NO"), clase = Css.center });            
                if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                    row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString(), clase = Css.center });
                else
                    row.cells.Add(new HtmlCell { valor = "", clase = Css.center });


                if (habilitado)
                    row.clickevent = "Edit(this)";

                tdatos.AddRow(row);
            }
            html.AppendLine(tdatos.ToString());
            return html.ToString();
        }

        [WebMethod]
        public static string GetDetalleDiario(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);


            comprobante.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa={0} and dco_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "dco_secuencia");
            List<Modulo> lstmodulos = ModuloBLL.GetAll("", "");


            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdinvoicediario";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";

            tdatos.AddColumn("Id", "width10", "", new Input() { id = "txtIDCUE_D", placeholder = "CTA", autocomplete = "GetCuentaObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODCUE_D", visible = false }.ToString());
            tdatos.AddColumn("Cuenta", "width20", "", new Input() { id = "txtCUENTA_D", placeholder = "DESCRIPCION", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("Cliente/Prov", "width10", "", new Input() { id = "txtIDPER_D", autocomplete = "GetPersonaObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODPER_D", visible = false }.ToString());
            tdatos.AddColumn("Nombres", "width20", "", new Input() { id = "txtNOMBRES_D", placeholder = "CLIENTE/PROVEEDOR", habilitado = false, clase = Css.blocklevel }.ToString());
            tdatos.AddColumn("Modulo", "width10", "", new Input() { id = "txtMODULO_D", placeholder = "MOD", habilitado = false, clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODMOD_D", visible = false }.ToString());
            tdatos.AddColumn("Débito", "width10", Css.right, new Input() { id = "txtDEBE_D", placeholder = "DEBE", clase = Css.blocklevel + Css.amount, numeric = true }.ToString());
            tdatos.AddColumn("Crédito", "width10", Css.right, new Input() { id = "txtHABER_D", placeholder = "HABER", clase = Css.blocklevel + Css.amount, numeric = true }.ToString());
            tdatos.AddColumn("D/C", "width5", Css.center, new Input() { id = "txtDC", clase = Css.blocklevel, habilitado = false }.ToString());
            if (comprobante.com_estado != Constantes.cEstadoMayorizado)
            tdatos.AddColumn("", "width5", Css.center, new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());

            tdatos.editable = true;

            foreach (Dcontable item in comprobante.contables)
            {

                Modulo mod = lstmodulos.Find(delegate(BusinessObjects.Modulo m) { return m.mod_codigo == item.dco_cuentamodulo; });

                HtmlRow row = new HtmlRow();

                row.data = "data-codcue='" + item.dco_cuenta + "' " +
                    " data-codper='" + item.dco_cliente + "' " +
                    " data-codmod='" + mod.mod_codigo + "' " +
                    " data-concepto='" + item.dco_concepto + "' " +
                    " data-idalmacen'" + item.dco_almacenid + "' " +
                    " data-nombrealmacen'" + item.dco_almacennombre + "' " +
                    " data-codalmacen'" + item.dco_almacen + "' " +
                    " data-idcentro'" + item.dco_centroid + "' " +
                    " data-nombrecentro'" + item.dco_centronombre + "' " +
                    " data-codcentro'" + item.dco_centro + "' " +
                    " data-idtransacc'" + item.dco_transacc + "' " +//FALTA EL CAMPO
                    " data-nombretransacc'" + item.dco_transacc + "' " +//FALTA EL CAMPO
                    " data-codtransacc'" + item.dco_transacc + "' " +
                    " data-ddocomproba= '" + item.dco_ddo_comproba + "' " +
                    " data-doctran= '" + item.dco_doctran + "' " +
                    //" data-ddotransacc= '" + item.dco_ddo_transacc+ "' " +
                    " data-nropago= '" + item.dco_nropago + "' " +
                    " data-fechavence= '" + item.dco_fecha_vence + "' " +
                    "";
                row.removable = true;

                row.cells.Add(new HtmlCell { valor = item.dco_cuentaid });//ID CUENTA
                row.cells.Add(new HtmlCell { valor = item.dco_cuentanombre });//NOMBRE CUENTA
                row.cells.Add(new HtmlCell { valor = item.dco_clienteid });
                row.cells.Add(new HtmlCell { valor = item.dco_clienteapellidos + " " + item.dco_clientenombres });



                row.cells.Add(new HtmlCell { valor = mod.mod_id });
                if (item.dco_debcre == Constantes.cDebito)
                {
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.dco_valor_nac), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(0), clase = Css.right });
                }
                else
                {
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(0), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.dco_valor_nac), clase = Css.right });
                }
                row.cells.Add(new HtmlCell { valor = item.dco_debcre, clase = Css.center });
                if (comprobante.com_estado != Constantes.cEstadoMayorizado)
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
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object modify = null;
            tmp.TryGetValue("modify", out modify);    

            bool habilitado = true;
            if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;
            if (!string.IsNullOrEmpty((string)modify))
                habilitado = bool.Parse(modify.ToString());

            comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);


            StringBuilder html = new StringBuilder();


            /*HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tddatos";
            tdatos.CreteEmptyTable(1, 2);
            tdatos.rows[0].cells[0].valor = "Beneficiario:";
            tdatos.rows[0].cells[1].valor = new Textarea() { id = "txtBENEFICIARIO", placeholder = "BENEFICIARIO", clase = Css.blocklevel }.ToString();
            
            tdatos.rows[1].cells[0].valor = "Almacen:";
            tdatos.rows[1].cells[1].valor = new Select() { id = "cmbALMACEN_D", clase = Css.large, diccionario = Dictionaries.GetAlmacen() }.ToString();  //new Input() { id = "txtIDALM_D", autocomplete = "GetAlmacenObj", placeholder = "ID ALM", clase = Css.mini }.ToString() + " " + new Input() { id = "txtNOMBREALM_D", placeholder = "ALMACEN", clase = Css.medium, habilitado = false }.ToString() + new Input() { id = "txtCODALM_D", visible = false }.ToString();
            tdatos.rows[2].cells[0].valor = "Centro:";
            tdatos.rows[2].cells[1].valor = new Select() { id = "cmbCENTRO_D", clase = Css.large, diccionario = Dictionaries.GetCentro(), habilitado = false }.ToString();//new Input() { id = "txtIDCEN_D", autocomplete = "GetCentroObj", placeholder = "ID CENTRO", clase = Css.mini, habilitado = false }.ToString() + " " + new Input() { id = "txtNOMBRECEN_D", placeholder = "CENTRO", clase = Css.medium, habilitado = false }.ToString() + new Input() { id = "txtCODCEN_D", visible = false }.ToString(); 
            tdatos.rows[3].cells[0].valor = "Transacción:";
            //tdatos.rows[3].cells[1].valor = new Input() { id = "txtIDTRA_D", autocomplete = "GetTransaccionObj", placeholder = "ID TRANS", clase = Css.mini, habilitado=false }.ToString() + " " + new Input() { id = "txtNOMBRETRA_D", placeholder = "TRANSACCION", clase = Css.medium, habilitado = false }.ToString() + new Input() { id = "txtCODTRA_D", visible = false }.ToString();
            tdatos.rows[3].cells[1].valor = new Select() { id = "cmbTRANSACC_D", clase = Css.large, diccionario = Dictionaries.Empty() }.ToString();

            tdatos.rows[4].cells[0].valor = "Referencia:";
            tdatos.rows[4].cells[1].valor = new Input() { id = "txtREF_D", placeholder = "REFERENCIA", clase = Css.medium }.ToString();
            tdatos.rows[5].cells[0].valor = "O. Pago:";
            tdatos.rows[5].cells[1].valor = new Input() { id = "txtOPAGO_D", placeholder = "O.PAGO", clase = Css.medium }.ToString();
            tdatos.rows[6].cells[0].valor = "Cuota /Vence:";
            tdatos.rows[6].cells[1].valor = new Input() { id = "txtNROPAGO_D", placeholder = "NRO PAGO", clase = Css.mini }.ToString() + " " + new Input() { id = "txtFECHAVENCE_D", datepicker = true, datetimevalor = DateTime.Now, clase = Css.small }.ToString();
            //tdatos.rows[7].cells[0].valor = "Saldo:";
            //tdatos.rows[7].cells[1].valor = new Input() { id = "txtSALDO_D",  clase = Css.medium }.ToString();

            //tdatos.rows[8].cells[0].valor = "";
            //tdatos.rows[8].cells[1].valor = "";
            
            html.AppendLine(tdatos.ToString());*/



            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(2, 2);

            tdatos1.rows[0].cells[0].valor = "TOTAL DÉBITO:";
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtTOTDEBITO", clase = Css.medium + Css.amount, habilitado = false }.ToString();
            tdatos1.rows[1].cells[0].valor = "TOTAL CRÉDITO:";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtTOTCREDITO", clase = Css.medium + Css.amount, habilitado = false }.ToString();
           
            //tdatos1.rows[3].cells[0].valor = "";
            //tdatos1.rows[3].cells[1].valor = "";

            html.AppendLine(tdatos1.ToString());

            return html.ToString();
        }

        [WebMethod]
        public static string GetCuentas(object objeto)
        {
            Dbancario dban = new Dbancario(objeto);

            Banco banco = BancoBLL.GetByPK(new Banco() { ban_codigo=dban.dban_banco, ban_codigo_key=dban.dban_banco, ban_empresa=dban.dban_empresa, ban_empresa_key=dban.dban_empresa});
            Cuenta cuenta = CuentaBLL.GetByPK(new Cuenta() { cue_codigo= banco.ban_cuenta.Value, cue_codigo_key= banco.ban_cuenta.Value,cue_empresa=banco.ban_empresa, cue_empresa_key=banco.ban_empresa});
            Modulo mod = ModuloBLL.GetByPK(new Modulo() { mod_codigo = cuenta.cue_modulo.Value, mod_codigo_key = cuenta.cue_modulo.Value });
            List<Transacc> lst = TransaccBLL.GetAll(new WhereParams("tra_modulo = {0}", cuenta.cue_modulo), "tra_secuencia");
            Dcontable item = new Dcontable();
            item.dco_cuenta = cuenta.cue_codigo;
            item.dco_cuentanombre = cuenta.cue_nombre;
            item.dco_cuentamodulo = cuenta.cue_modulo;
            if (lst.Count > 0)
            {
                item.dco_transacc = lst[0].tra_secuencia;
            }
            item.dco_clienteid = mod.mod_id;
            item.dco_cuentaid = cuenta.cue_id;
            item.dco_concepto =  "BENEFICIARIO:"+ dban.dban_beneficiario + " DOCUMENTO:" + dban.dban_documento; 
            item.dco_debcre = dban.dban_debcre;//(dban.dban_debcre debito > credito) ? Constantes.cDebito : Constantes.cCredito;
            item.dco_valor_nac = dban.dban_valor_nac;// (debito > credito) ? debito : credito;
            item.dco_valor_ext = dban.dban_valor_ext;

            return new JavaScriptSerializer().Serialize(item);

            /*Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object ban_empresa = null;
            object ban_codigo = null;
            object ban_debito = null;
            object ban_credito = null;
            tmp.TryGetValue("ban_empresa", out ban_empresa);
            tmp.TryGetValue("ban_codigo", out ban_codigo);
            tmp.TryGetValue("ban_debito", out ban_debito);
            tmp.TryGetValue("ban_credito", out ban_credito);
            decimal debito = (decimal)Conversiones.GetValueByType(ban_debito, typeof(decimal));
            decimal credito = (decimal)Conversiones.GetValueByType(ban_credito, typeof(decimal));
            Banco banco = new Banco();
            banco.ban_codigo = banco.ban_codigo_key = (Int32)Conversiones.GetValueByType(ban_codigo, typeof(Int32));
            banco.ban_empresa = banco.ban_empresa_key = (Int32)Conversiones.GetValueByType(ban_empresa, typeof(Int32));
            banco = BancoBLL.GetByPK(banco);

            Cuenta cuenta = new Cuenta();
            cuenta.cue_codigo = cuenta.cue_codigo_key = (Int32)Conversiones.GetValueByType(banco.ban_cuenta.Value, typeof(Int32));
            cuenta.cue_empresa= cuenta.cue_empresa_key = (Int32)Conversiones.GetValueByType(ban_empresa, typeof(Int32));
            cuenta = CuentaBLL.GetByPK(cuenta);
            Modulo mod = new Modulo();
            mod.mod_codigo = mod.mod_codigo_key = cuenta.cue_modulo.Value;
            mod = ModuloBLL.GetByPK(mod);

            List<Transacc> lst = TransaccBLL.GetAll(new WhereParams("tra_modulo = {0}", cuenta.cue_modulo), "tra_secuencia");


            Dcontable item = new Dcontable();
            item.dco_cuenta = cuenta.cue_codigo;
            item.dco_cuentanombre = cuenta.cue_nombre;
            item.dco_cuentamodulo = cuenta.cue_modulo;
            if (lst.Count > 0)
            {
                item.dco_transacc = lst[0].tra_secuencia;                
            }
            item.dco_clienteid = mod.mod_id;
            item.dco_cuentaid = cuenta.cue_id;
            item.dco_debcre = (debito > credito) ? Constantes.cDebito : Constantes.cCredito;
            item.dco_valor_nac = (debito > credito) ? debito : credito;
            
            return new JavaScriptSerializer().Serialize(item);*/
        }

        [WebMethod]
        public static string SaveObject(object objeto)
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

            //Nueva Validacion Documentos Bancarios
            foreach (Dbancario item in obj.bancario)
            {
                //if (item.dban_documento == "")
                //{
                //    throw new System.ArgumentException("Es necesario ingresar el numero de documento", "(Nro Documento)");
                //}



                if (item.dban_documento != "")
                {

                    Banco banco = BancoBLL.GetByPK(new Banco { ban_empresa = obj.com_empresa, ban_empresa_key = obj.com_empresa, ban_codigo = item.dban_banco, ban_codigo_key = item.dban_banco });
                    List<Dbancario> lst = DbancarioBLL.GetAll("dban_empresa=" + obj.com_empresa + " and dban_banco=" + item.dban_banco + " and dban_documento='" + item.dban_documento + "'", "");
                    if (lst.Count > 0)
                    {
                        bool repetido = true;
                        foreach (Dbancario dban in lst)
                        {
                            if (dban.dban_cco_comproba == obj.com_codigo)
                                repetido = false;

                        }
                        if (repetido)
                            throw new System.ArgumentException("El documento Nro. " + item.dban_documento + " del banco " + banco.ban_nombre + " ya ha sido emitido anteriormente", "(Nro Cheque)");
                    }

                }
            }




            try
            {
                if (obj.com_codigo > 0)
                    obj = BAN.update_bancario(obj);
                else
                    obj = BAN.save_bancario(obj);


                obj = CNT.account_diario(obj);
                return obj.com_codigo.ToString();
            }
            catch (Exception ex)
            {
                ExceptionHandling.Log.AddExepcion(ex);
                return obj.com_codigo.ToString();
            }
            



            
        }

        [WebMethod]
        public static string CloseObject(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object objetocomp = null;
            object objetodrec = null;
            object objetorfac = null;
            tmp.TryGetValue("recibo", out objetocomp);
            tmp.TryGetValue("afectacion", out objetodrec);
            tmp.TryGetValue("rutaxfactura", out objetorfac);

            Comprobante obj = new Comprobante(objetocomp);

      /*      List<Drecibo> detalle = new List<Drecibo>();
            if (objetodrec != null)
            {
                Array array = (Array)objetodrec;
                foreach (Object item in array)
                {
                    if (item != null)
                        detalle.Add(new Drecibo(item));
                }

            }

            Rutaxfactura rfac = new Rutaxfactura(objetorfac);
            */
            try
            {


                obj = CNT.account_diario(obj);
                return obj.com_codigo.ToString();
            }
            catch (Exception ex)
            {
                return "-1";
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
            obj.com_concepto = obj.com_concepto;
            obj.com_modulo = General.GetModulo(obj.com_tipodoc); ;
            obj.com_transacc = General.GetTransacc(obj.com_tipodoc);

            obj.com_estado = Constantes.cEstadoGrabado;
            obj.com_descuadre = 0;
            obj.com_adestino = 0;
            obj.com_doctran = General.GetNumeroComprobante(obj);
            //     int debcrecan = (obj.com_tipodoc == Constantes.cRecibo.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                obj.com_codigo = ComprobanteBLL.InsertIdentity(transaction, obj);
                //int codigo = ComprobanteBLL.InsertIdentity(transaction, obj);
                obj.ccomdoc.cdoc_empresa = obj.com_empresa;
                obj.ccomdoc.cdoc_comprobante = obj.com_codigo;
             //   CcomdocBLL.Insert(transaction, obj.ccomdoc);
                decimal totalrecibo = 0;
                int contador = 0;
                foreach (Dbancario item in obj.bancario)
                {
                    item.dban_cco_comproba = obj.com_codigo;
                    item.dban_secuencia = contador;
                    item.dban_transacc = obj.com_transacc;
                    // item.deb = debcredoc;
                    totalrecibo += item.dban_valor_nac;
                    DbancarioBLL.Insert(transaction, item);
                    contador++;
                }

               contador = 0;
                foreach (Dcontable item in obj.contables)
                {
                    item.dco_empresa = obj.com_empresa;
                    item.dco_comprobante = obj.com_codigo;
                    item.dco_secuencia = contador;
                    DcontableBLL.Insert(transaction, item);
                    contador++;
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
                objU.com_concepto = "Nota de Credito " + obj.ccomdoc.cdoc_nombre;
                ComprobanteBLL.Update(transaction, objU);
                obj.ccomdoc.cdoc_empresa_key = obj.ccomdoc.cdoc_empresa;
                obj.ccomdoc.cdoc_comprobante_key = obj.ccomdoc.cdoc_comprobante;
                CcomdocBLL.Update(transaction, obj.ccomdoc);
                List<Dbancario> lst = DbancarioBLL.GetAll(new WhereParams("dnc_empresa = {0} and dnc_comprobante = {1}", obj.com_empresa, obj.com_codigo), "");
                foreach (Dbancario item in lst)
                {
                    DbancarioBLL.Delete(transaction, item);
                }
                decimal totalrecibo = 0;
                int contador = 0;
                foreach (Dbancario item in obj.bancario)
                {
                    item.dban_cco_comproba = obj.com_codigo;
                    item.dban_secuencia = contador;
                    // item.deb = debcredoc;
                    totalrecibo += item.dban_valor_nac ;
                    DbancarioBLL.Insert(transaction, item);
                    contador++;
                }
                List<Ddocumento> lst3 = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa = {0} and ddo_comprobante = {1}", obj.com_empresa, obj.ccomdoc.cdoc_factura), "");
                foreach (Ddocumento item in lst3)
                {
                    //  DdocumentoBLL.Delete(transaction, item);
                    List<Dcancelacion> lst2 = DcancelacionBLL.GetAll(new WhereParams("dca_empresa = {0} and dca_comprobante_can = {1} and dca_comprobante={2}", item.ddo_empresa, obj.com_codigo, item.ddo_comprobante), "");
                    foreach (Dcancelacion item2 in lst2)
                    {
                        item.ddo_cancela = ((item.ddo_cancela.HasValue) ? item.ddo_cancela.Value : 0) - item2.dca_monto;
                        if (item.ddo_monto >= item.ddo_cancela)
                            item.ddo_cancelado = 0;
                        DcancelacionBLL.Delete(transaction, item2);
                    }
                    Dcancelacion dca = new Dcancelacion();
                    //dca.dca_empresa = doc.ddo_empresa;
                    //dca.dca_comprobante = doc.ddo_comprobante;
                    dca.dca_empresa = obj.com_empresa;
                    dca.dca_comprobante = item.ddo_comprobante;
                    dca.dca_transacc = item.ddo_transacc;
                    dca.dca_doctran = item.ddo_doctran;
                    dca.dca_pago = item.ddo_pago;
                    dca.dca_comprobante_can = obj.com_codigo;
                    dca.dca_secuencia = 0;
                    dca.dca_debcre = (item.ddo_debcre == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
                    dca.dca_transacc_can = obj.com_transacc;
                    dca.dca_tipo_cambio = item.ddo_tipo_cambio;
                    if (totalrecibo > item.ddo_monto.Value)
                    {
                        dca.dca_monto = item.ddo_monto;
                        dca.dca_monto_ext = item.ddo_monto;
                    }
                    else
                    {
                        dca.dca_monto = totalrecibo;
                        dca.dca_monto_ext = totalrecibo;
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
                    if (totalrecibo > item.ddo_monto.Value)
                        totalrecibo = totalrecibo - item.ddo_monto.Value;
                    else
                        break;
                }

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