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
    public partial class wfPagoSocio : System.Web.UI.Page
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
                txtorigen.Text = (Request.QueryString["origen"] != null) ? Request.QueryString["origen"].ToString() : "";
                txtcodsocio.Text = (Request.QueryString["codsocio"] != null) ? Request.QueryString["codsocio"].ToString() : "-1";
                txtcodigocomp.Text = (Request.QueryString["codigocomp"] != null) ? Request.QueryString["codigocomp"].ToString() : "-1";
                tipodoc = Convert.ToInt32(txttipodoc.Text);
            }
        }

        [WebMethod]
        public static string GetCabecera(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);
            Persona cli = new Persona();
            cli.per_empresa = comprobante.com_empresa;
            cli.per_empresa_key = comprobante.com_empresa;
            if (comprobante.com_codclipro.HasValue)
            {
                cli.per_codigo = comprobante.com_codclipro.Value;
                cli.per_codigo_key = comprobante.com_codclipro.Value;
                cli = PersonaBLL.GetByPK(cli);
            }



            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(2, 2);
            tdatos.rows[0].cells[0].valor = "Socio:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtIDPER", valor = cli.per_id, autocomplete = "GetProveedorObj", obligatorio = true, habilitado = ((comprobante.com_estado == Constantes.cEstadoMayorizado) ? false : true), clase = Css.medium, placeholder = "Cliente" }.ToString() + " " + new Input { id = "txtNOMBRES", clase = Css.large, habilitado = false, valor = cli.per_apellidos + " " + cli.per_nombres }.ToString() + new Input { id = "txtCODPER", visible = false, valor = cli.per_codigo }.ToString();
            tdatos.rows[1].cells[0].valor = "CI-RUC/Razón Social:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtRUC", clase = Css.medium, habilitado = false, valor = cli.per_ciruc }.ToString() + " " + new Input { id = "txtRAZON", clase = Css.large, habilitado = false, valor = cli.per_razon }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdobs = new HtmlTable();
            tdobs.CreteEmptyTable(1, 2);
            tdobs.rows[0].cells[0].valor = "Observaciones:";
            tdobs.rows[0].cells[1].valor = new Textarea { id = "txtCONCEPTO", valor = comprobante.com_concepto, habilitado = true, clase = Css.blocklevel, rows = 4 }.ToString();
            html.AppendLine(tdobs.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");
            html.AppendLine(new Input { id = "txtESTADO", valor = comprobante.com_estado, visible = false }.ToString());
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
                row.data = "data-codtipo='" + item.dfp_tipopago + "' data-nrodocumento='" + item.dfp_nro_documento + "' data-nrocuenta='" + item.dfp_nro_cuenta + "' data-emisor='" + item.dfp_emisor + "' data-banco='" + item.dfp_banco + "' data-nrocheque='" + item.dfp_nro_cheque + "' data-beneficiario='" + item.dfp_beneficiario + "' data-fecha='" + item.dfp_fecha_ven + "'";
                //row.data = "{\"data-codtipo\":\"" + item.dfp_tipopago + "\", \"data-nrodocumentos\":\"" + item.dfp_nro_documento + "\"}' ";
                row.removable = true;

                row.cells.Add(new HtmlCell { valor = item.dfp_tipopagoid });
                row.cells.Add(new HtmlCell { valor = item.dfp_tipopagonombre });
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

            
            try
            {


                CXCP.account_pagosocio(obj);
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
            object objetoafec = null;

            tmp.TryGetValue("recibo", out objetocomp);
            tmp.TryGetValue("afectacion", out objetoafec);

            Comprobante obj = new Comprobante(objetocomp);
            List<Dcancelacion> detalle = new List<Dcancelacion>();
            
            foreach (Drecibo item in obj.recibos)
            {
                if (item.dfp_nro_documento == "" && (item.dfp_tipopago == 7 || item.dfp_tipopago == 8 || item.dfp_tipopago == 16))
                {
                    throw new System.ArgumentException("Es necesario ingresar el numero de retención", "(Nro Documento)");
                }
              

            }

            try
            {

                if (obj.com_codigo > 0)
                    obj = CXCP.update_pagosocio(obj);
                else
                    obj = CXCP.save_pagosocio(obj);
                CXCP.account_pagosocio(obj);
                return obj.com_codigo.ToString();

            }
            catch (Exception ex)
            {
                ExceptionHandling.Log.AddExepcion(ex);
                throw ex;
                //return obj.com_codigo.ToString();
            }


            /*if (obj.com_codigo == 0)
            {
                string mensaje = "";
                obj = InsertComprobante(obj, ref mensaje);

                return mensaje;
            }
            else
            {
                return UpdateComprobante(obj);
            }*/




        }


       
    }
}