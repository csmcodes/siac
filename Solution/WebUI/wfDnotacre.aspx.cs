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
    public partial class wfDnotacre : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "dcab_nombre";
        protected static string WhereClause = "";
        protected static WhereParams parametros;
        protected static int? tclipro;
        protected static int? tipodoc; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txttipodoc.Text = (Request.QueryString["tipodoc"] != null) ? Request.QueryString["tipodoc"].ToString() : "-1";
                txtcodigocomp.Text = (Request.QueryString["codigocomp"] != null) ? Request.QueryString["codigocomp"].ToString() : "-1";
                pageIndex = 1;
                pageSize = 20;
                txttclipro.Text = (Request.QueryString["tclipro"] != null) ? Request.QueryString["tclipro"].ToString() : Constantes.cCliente+"";
                tclipro = Convert.ToInt32(txttclipro.Text);
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

            Ccomdoc ccomdoc = new Ccomdoc();
            ccomdoc.cdoc_empresa = obj.com_empresa;
            ccomdoc.cdoc_empresa_key = obj.com_empresa;
            if (obj.com_codigo > 0)
            {
                ccomdoc.cdoc_comprobante = obj.com_codigo;
                ccomdoc.cdoc_comprobante_key = obj.com_codigo;
                ccomdoc = CcomdocBLL.GetByPK(ccomdoc);
            }


            Comprobante comprabante = new Comprobante();
            comprabante.com_empresa = obj.com_empresa;
            comprabante.com_empresa_key = obj.com_empresa;
            if (ccomdoc.cdoc_factura > 0)
            {
                comprabante.com_codigo = ccomdoc.cdoc_factura.Value;
                comprabante.com_codigo_key = ccomdoc.cdoc_factura.Value;
                comprabante = ComprobanteBLL.GetByPK(comprabante);
            }

            List<Tab> tabs = new List<Tab>();


            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(3, 2);
            if (tclipro == Constantes.cProveedor)
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
            tdatos1.rows[0].cells[0].valor = "Concepto:";
            tdatos1.rows[0].cells[1].valor = new Textarea { id = "txtCONCEPTO", clase = Css.xlarge, valor = obj.com_concepto, habilitado = true }.ToString() + new Input { id = "txtTCLIPRO", visible = false, valor =   (obj.com_tclipro.HasValue) ? obj.com_tclipro.Value : tclipro }.ToString();

            tdatos1.rows[1].cells[0].valor = "Agente:";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtCODVEN", autocomplete = "GetPersonaObj", clase = Css.small, habilitado = false }.ToString() + " " + new Input { id = "txtVENDEDOR", clase = Css.large, habilitado = false }.ToString();

            tdatos1.rows[2].cells[0].valor = "Comprobante:";
          
            //tdatos1.rows[2].cells[0].valor = "Bodega:";
            //tdatos1.rows[2].cells[1].valor = new Input { id = "txtCODBOD", autocomplete = "GetBodegaObj", clase = Css.small }.ToString() + " " + new Input { id = "txtBODEGA", clase = Css.large, habilitado = false }.ToString();

            if (tclipro == Constantes.cProveedor)
            {
                tdatos1.rows[2].cells[1].valor = new Input { id = "txtCODCOM", valor = comprabante.com_doctran, autocomplete = "GetObligacionObjParam", clase = Css.large, habilitado = true, obligatorio = true }.ToString() + " " + new Input { id = "txtCOMPROBANTE", visible = false, valor = ccomdoc.cdoc_factura }.ToString();
            }
            else
            {
                tdatos1.rows[2].cells[1].valor = new Input { id = "txtCODCOM", valor = comprabante.com_doctran, autocomplete = "GetFacturaObjParam", clase = Css.large, habilitado = true, obligatorio = true }.ToString() + " " + new Input { id = "txtCOMPROBANTE", visible = false, valor = ccomdoc.cdoc_factura }.ToString();
            }

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
        public static string GetDetalle(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            comprobante.notascre = DnotacreBLL.GetAll(new WhereParams("dnc_empresa={0} and dnc_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "dnc_secuencia");

            int tipodocumento = (comprobante.com_tipodoc > 0) ? comprobante.com_tipodoc : tipodoc.Value;
            int tipopersona = (comprobante.com_tclipro.HasValue) ? comprobante.com_tclipro.Value : tclipro.Value;

            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdinvoice";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";

            if (tipodocumento == Constantes.cNotacre.tpd_codigo || tipodocumento == Constantes.cNotacrePro.tpd_codigo)
            {
                if (tipopersona == Constantes.cCliente)
                {
                    tdatos.AddColumn("Tipo", "width10", "", new Input() { id = "txtIDTIPO", placeholder = "TIPO", autocomplete = "GetTipoNotaCreObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODTIPO", visible = false }.ToString());
                }
                if (tipopersona == Constantes.cProveedor)
                {
                    tdatos.AddColumn("Tipo", "width10", "", new Input() { id = "txtIDTIPO", placeholder = "TIPO", autocomplete = "GetTipoNotaCreProObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODTIPO", visible = false }.ToString());
                }
            }
            else if (tipodocumento == Constantes.cNotadeb.tpd_codigo || tipodocumento == Constantes.cNotadebPro.tpd_codigo)
            {
                if (tipopersona == Constantes.cCliente)
                {
                    tdatos.AddColumn("Tipo", "width10", "", new Input() { id = "txtIDTIPO", placeholder = "TIPO", autocomplete = "GetTipoNotaDebObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODTIPO", visible = false }.ToString());
                }
                if (tipopersona == Constantes.cProveedor)
                {
                    tdatos.AddColumn("Tipo", "width10", "", new Input() { id = "txtIDTIPO", placeholder = "TIPO", autocomplete = "GetTipoNotaDebProObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODTIPO", visible = false }.ToString());
                }
            }





           
            tdatos.AddColumn("Descripción", "width30", "", new Input() { id = "txtNOMBRETIPO", placeholder = "DESCRIPCION", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("Valor", "width10", Css.right, new Input() { id = "txtVALOR", placeholder = "VALOR", clase = Css.blocklevel + Css.amount }.ToString());
            tdatos.AddColumn("IVA", "width5", Css.center, new Check() { id = "chkIVA", clase = Css.blocklevel + Css.cantidades, valor = 0 }.ToString());
            if (comprobante.com_estado != Constantes.cEstadoMayorizado)
            tdatos.AddColumn("", "width5", Css.center, new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());

            tdatos.editable = true;

            foreach (Dnotacre item in comprobante.notascre)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-codtipo=" + item.dnc_tiponc;
                row.removable = true;

                row.cells.Add(new HtmlCell { valor = item.dnc_tiponcid });
                row.cells.Add(new HtmlCell { valor = item.dnc_tiponcnombre });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.dnc_valor), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = ((item.dnc_cheque.HasValue) ? ((item.dnc_cheque.Value == 1) ?"SI":"NO"):"NO"), clase = Css.center });
                if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString(), clase = Css.center });
                tdatos.AddRow(row);

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

            tdatos1.rows[0].cells[0].valor = "IVA " + valoriva + "%:";
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
            obj.com_tclipro = tclipro;
            try
            {


                    if (obj.com_codigo > 0)
                        obj = BAN.update_notacredeb(obj);
                    else
                        obj = BAN.save_notacredeb(obj);
                    
                    obj = BAN.account_notacredeb(obj);
                    return "OK";
                }
                catch (Exception ex)
                {
                    return "ERROR";
                }
          



        }
        [WebMethod]
        public static string CloseObject(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object objetocomp = null;
            object objetoafec = null;
            object objetorfac = null;
            tmp.TryGetValue("recibo", out objetocomp);
            tmp.TryGetValue("afectacion", out objetoafec);
            tmp.TryGetValue("rutaxfactura", out objetorfac);

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


                obj = BAN.account_notacredeb(obj);
                return obj.com_codigo.ToString();
            }
            catch (Exception ex)
            {
                return "-1";
            }
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

                List<Dnotacre> lst = DnotacreBLL.GetAll(new WhereParams("dnc_empresa = {0} and dnc_comprobante = {1}", obj.com_empresa, obj.com_codigo), "");
                foreach (Dnotacre item in lst)
                {
                    DnotacreBLL.Delete(transaction, item);
                }

                decimal totalrecibo = 0;
                int contador = 0;
                foreach (Dnotacre item in obj.notascre)
                {
                    item.dnc_comprobante = obj.com_codigo;
                    item.dnc_secuencia = contador;
                    // item.deb = debcredoc;
                    totalrecibo += item.dnc_valor ?? 0;
                    DnotacreBLL.Insert(transaction, item);
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