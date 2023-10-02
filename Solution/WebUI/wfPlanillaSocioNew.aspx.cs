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
    public partial class wfPlanillaSocioNew : System.Web.UI.Page
    {

        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "ddo_doctran";//"dcab_nombre";
        protected static string WhereClause = "";
        protected static WhereParams parametros; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txttipodoc.Text = (Request.QueryString["tipodoc"] != null) ? Request.QueryString["tipodoc"].ToString() : "-1";
                txtorigen.Text = (Request.QueryString["origen"] != null) ? Request.QueryString["origen"].ToString() : "";
                txtcodsocio.Text = (Request.QueryString["codsocio"] != null) ? Request.QueryString["codsocio"].ToString() : "-1";
                txtcodigocomp.Text = (Request.QueryString["codigocomp"] != null) ? Request.QueryString["codigocomp"].ToString() : "-1";
                txtvehiculo.Text = (Request.QueryString["vehiculo"] != null) ? Request.QueryString["vehiculo"].ToString() : "-1";
                pageIndex = 1;
                pageSize = 20;
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
            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Cliente:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtIDPER", valor = cli.per_id, autocomplete = "GetsocioObj", obligatorio = true, habilitado = ((comprobante.com_estado == Constantes.cEstadoMayorizado) ? false : true), clase = Css.medium, placeholder = "Cliente" }.ToString() + " " + new Input { id = "txtNOMBRES", clase = Css.large, habilitado = false, valor = cli.per_apellidos + " " + cli.per_nombres }.ToString() + new Input { id = "txtCODPER", visible = false, valor = cli.per_codigo }.ToString();
            tdatos.rows[1].cells[0].valor = "CI-RUC/Razón Social:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtRUC", clase = Css.medium, habilitado = false, valor = cli.per_ciruc }.ToString() + " " + new Input { id = "txtRAZON", clase = Css.large, habilitado = false, valor = cli.per_razon }.ToString();
            tdatos.rows[2].cells[0].valor = "Teléfono/Dirección:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtTELEFONO", clase = Css.medium, habilitado = false, valor = cli.per_telefono }.ToString() + " " + new Input { id = "txtDIRECCION", clase = Css.large, habilitado = false, valor = cli.per_direccion }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdobs = new HtmlTable();
            tdobs.CreteEmptyTable(2, 2);
            tdobs.rows[0].cells[0].valor = "Vehiculo:";
            tdobs.rows[0].cells[1].valor = new Input { id = "txtVEHICULO", clase = Css.medium, habilitado = false, valor = "" }.ToString();
            tdobs.rows[1].cells[0].valor = "Observaciones:";
            tdobs.rows[1].cells[1].valor = new Textarea { id = "txtOBSERVACIONES", valor = comprobante.com_concepto, habilitado = true, clase = Css.blocklevel, rows = 4 }.ToString();
            html.AppendLine(tdobs.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");
            html.AppendLine(new Input { id = "txtESTADO", valor = comprobante.com_estado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCERRADO", valor = Constantes.cEstadoMayorizado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCREADO", valor = Constantes.cEstadoProceso, visible = false }.ToString());
            
            return html.ToString();            
        }

        [WebMethod]
        public static string GetCabeceraFromSocio(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;            
            Persona cli = new Persona();
            cli.per_empresa = comprobante.com_empresa;
            cli.per_empresa_key = comprobante.com_empresa;
            if (comprobante.com_codclipro.HasValue)
            {
                cli.per_codigo = comprobante.com_codclipro.Value;
                cli.per_codigo_key = comprobante.com_codclipro.Value;
                cli = PersonaBLL.GetByPK(cli);
            }


            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object vehiculo = null;
            tmp.TryGetValue("vehiculo", out vehiculo);

            


            //Vehiculo veh = new Vehiculo();
            //veh.veh_empresa = comprobante.com_empresa;
            //veh.veh_empresa_key = comprobante.com_empresa;
            //veh.veh_codigo = Functions.Conversiones.ObjectToIntNull(vehiculo).Value;
            //veh.veh_codigo_key = veh.veh_codigo;
            //veh = VehiculoBLL.GetByPK(veh);





            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Cliente:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtIDPER", valor = cli.per_id, autocomplete = "GetsocioObj", obligatorio = true, habilitado = ((comprobante.com_estado == Constantes.cEstadoMayorizado) ? false : true), clase = Css.medium, placeholder = "Cliente" }.ToString() + " " + new Input { id = "txtNOMBRES", clase = Css.large, habilitado = false, valor = cli.per_apellidos + " " + cli.per_nombres }.ToString() + new Input { id = "txtCODPER", visible = false, valor = cli.per_codigo }.ToString();
            tdatos.rows[1].cells[0].valor = "CI-RUC/Razón Social:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtRUC", clase = Css.medium, habilitado = false, valor = cli.per_ciruc }.ToString() + " " + new Input { id = "txtRAZON", clase = Css.large, habilitado = false, valor = cli.per_razon }.ToString();
            tdatos.rows[2].cells[0].valor = "Teléfono/Dirección:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtTELEFONO", clase = Css.medium, habilitado = false, valor = cli.per_telefono }.ToString() + " " + new Input { id = "txtDIRECCION", clase = Css.large, habilitado = false, valor = cli.per_direccion }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdobs = new HtmlTable();
            tdobs.CreteEmptyTable(2, 2);
            tdobs.rows[0].cells[0].valor = "Vehiculo:";
            tdobs.rows[0].cells[1].valor = new Input { id = "txtVEHICULO", clase = Css.medium, habilitado = false, valor = ""}.ToString();
            tdobs.rows[1].cells[0].valor = "Observaciones:";
            tdobs.rows[1].cells[1].valor = new Textarea { id = "txtOBSERVACIONES", valor = comprobante.com_concepto, habilitado = true, clase = Css.blocklevel, rows = 4 }.ToString();
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
            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tddatos";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";
            tdatos.AddColumn("Fecha", "width10", "", "");
            tdatos.AddColumn("Documento", "width10", "", "");
            tdatos.AddColumn("Guia", "width10", "", "");
            tdatos.AddColumn("Cancelacion", "width10", "", "");
            //tdatos.AddColumn("Agencia", "width20", "", "");
            //tdatos.AddColumn("Cliente", "width20", "", "");
            tdatos.AddColumn("Valor", "width5", "", "");
            /*tdatos.AddColumn("Monto", "width5", "", "");
            tdatos.AddColumn("Cancelado", "width5", "", "");
            tdatos.AddColumn("Saldo", "width5", "", "");*/
            tdatos.editable = false;         

            List<vCancelacion> lst = vCancelacionBLL.GetAll(new WhereParams("dca_empresa={0} and dca_planilla={1}", comprobante.com_empresa, comprobante.com_codigo), OrderByClause);

            //List<vDcancelacion> lst = vDcancelacionBLL.GetAll(new WhereParams("dca_empresa={0} and dca_planilla={1}", comprobante.com_empresa, comprobante.com_codigo), "fechadetalle");   
            foreach (vCancelacion item in lst)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-comprobante=" + item.ddo_comprobante + " data-dca_pago=" + item.ddo_pago + " data-dca_transacc=" + item.ddo_transacc + " data-dca_doctran=" + item.ddo_doctran + " data-dca_comprobante_can=" + item.dca_comprobante_can + " data-dca_secuencia=" + item.dca_secuencia + " data-vehiculo='Placa:" + item.placa + " Disco:" + item.disco + "'";
                //if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                //   row.markable = true;

                row.markable = true;

                row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi.Value.ToShortDateString() });
                //row.cells.Add(new HtmlCell { valor = item.ddo_doctran });
                row.cells.Add(new HtmlCell { valor = new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.ddo_comprobante+ ")" }.ToString() + " " + item.ddo_doctran});
                //row.cells.Add(new HtmlCell { valor = item.ddo_comprobante_guia });
                row.cells.Add(new HtmlCell { valor = (item.ddo_comprobante_guia.HasValue)? new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.ddo_comprobante_guia + ")" }.ToString() + " " + item.doctran_guia:""});
                //row.cells.Add(new HtmlCell { valor = item.razoncliente });
                row.cells.Add(new HtmlCell { valor = new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.dca_comprobante_can + ")" }.ToString() + " " + item.doctran_can });
                //row.cells.Add(new HtmlCell { valor = item.alm_nombre });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.dca_monto_pla), clase = Css.right });

                /*row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_monto), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_cancela), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_monto - item.ddo_cancela), clase = Css.right });*/
                tdatos.AddRow(row);
            }
            html.AppendLine(tdatos.ToString());
            return html.ToString();
        }

        [WebMethod]
        public static string GetDetalleFromSocio(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);


            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object vehiculo = null;
            tmp.TryGetValue("vehiculo", out vehiculo);



            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tddatos";
            tdatos.invoice = true;            
            //tdatos.titulo = "Factura";
            tdatos.AddColumn("Fecha", "width10", "", "");
            tdatos.AddColumn("Documento", "width10", "", "");
            tdatos.AddColumn("Guia", "width10", "", "");
            tdatos.AddColumn("Cancelacion", "width10", "", "");
            //tdatos.AddColumn("Agencia", "width20", "", "");
            //tdatos.AddColumn("Cliente", "width20", "", "");
            tdatos.AddColumn("Valor", "width5", "", "");
            /*tdatos.AddColumn("Monto", "width5", "", "");
            tdatos.AddColumn("Cancelado", "width5", "", "");
            tdatos.AddColumn("Saldo", "width5", "", "");*/
            tdatos.editable = false;


            WhereParams parametros = new WhereParams();
            parametros.where = "dca_monto_pla>0 and f.com_tipodoc = 4 AND dca_planilla is null and (cf.cenv_socio = {0} or cg.cenv_socio = {0}) and (f.com_codigo IN (select pco_comprobante_fac FROM planillacomprobante ) or  f.com_codigo IN (select plc_comprobante  FROM planillacli ))";
            List<object> valores = new List<object>();
            valores.Add(comprobante.com_codclipro);

            if (vehiculo != null)
            {
                if (vehiculo.ToString() == "")
                {
                    parametros.where += " and (cf.cenv_vehiculo is null and cg.cenv_vehiculo is null)";                    
                }
                else if (vehiculo.ToString()!="-1")
                {
                    parametros.where += " and (cf.cenv_vehiculo = {" + valores.Count + "} or cg.cenv_vehiculo = {" + valores.Count + "})";
                    valores.Add(int.Parse(vehiculo.ToString()));
                }
            }

            parametros.valores = valores.ToArray();

            //List<vCancelacion> lst= v
            //List<vCancelacion> lst = vCancelacionBLL.GetAll(new WhereParams("ddo_cancela>0 AND tpa_transacc = 1 and f.com_tipodoc = 4 AND dca_planilla is null and (cf.cenv_socio = {0} or cg.cenv_socio = {0}) and f.com_codigo IN (select pco_comprobante_fac FROM planillacomprobante )", comprobante.com_codclipro), OrderByClause);



            //List<vCancelacion> lst = vCancelacionBLL.GetAll(new WhereParams("dca_monto_pla>0 and f.com_tipodoc = 4 AND dca_planilla is null and (cf.cenv_socio = {0} or cg.cenv_socio = {0}) and (f.com_codigo IN (select pco_comprobante_fac FROM planillacomprobante ) or  f.com_codigo IN (select plc_comprobante  FROM planillacli ))", comprobante.com_codclipro), OrderByClause);
            List<vCancelacion> lst = vCancelacionBLL.GetAll(parametros, OrderByClause);
            foreach (vCancelacion item in lst)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-comprobante=" + item.ddo_comprobante + " data-dca_pago=" + item.ddo_pago + " data-dca_transacc=" + item.ddo_transacc + " data-dca_doctran=" + item.ddo_doctran + " data-dca_comprobante_can=" + item.dca_comprobante_can + " data-dca_secuencia=" + item.dca_secuencia + " data-vehiculo='Placa:" + item.placa + " Disco:" + item.disco + "'";
                //if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                //   row.markable = true;
                row.markable = true;
                row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi.Value.ToShortDateString() });
                //row.cells.Add(new HtmlCell { valor = item.ddo_doctran});
                row.cells.Add(new HtmlCell { valor = new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.ddo_comprobante + ")" }.ToString() + " " + item.ddo_doctran });
                //row.cells.Add(new HtmlCell { valor = item.ddo_comprobante_guia });
                row.cells.Add(new HtmlCell { valor = (item.ddo_comprobante_guia.HasValue) ? new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.ddo_comprobante_guia + ")" }.ToString() + " " + item.doctran_guia : "" });
                //row.cells.Add(new HtmlCell { valor = item.ddo_pago});
                //row.cells.Add(new HtmlCell { valor = item.alm_nombre });
                row.cells.Add(new HtmlCell { valor = new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.dca_comprobante_can+ ")" }.ToString() + " " + item.doctran_can});

                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.dca_monto_pla), clase = Css.right });

                /*row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_monto), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_cancela), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_monto - item.ddo_cancela), clase = Css.right });*/
                tdatos.AddRow(row);
            }
            



            //List<vDcancelacion> lst = vDcancelacionBLL.GetAll(new WhereParams("dca_empresa={0} and dca_planilla={1}", comprobante.com_empresa, comprobante.com_codigo), "fechadetalle");
            /*foreach (vDcancelacion item in lst)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-comprobante=" + item.dca_comprobante + " data-dca_pago=" + item.dca_pago + " data-dca_transacc=" + item.dca_transacc + " data-dca_doctran=" + item.doctrandetalle + " data-dca_comprobante_can=" + item.dca_comprobante_can + " data-dca_secuencia=" + item.dca_secuencia;
                if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                    row.markable = true;
                row.cells.Add(new HtmlCell { valor = item.fechadetalle.Value.ToShortDateString() });
                row.cells.Add(new HtmlCell { valor = item.doctrandetalle });
                row.cells.Add(new HtmlCell { valor = item.doctranguia });
                row.cells.Add(new HtmlCell { valor = item.dca_pago });
                row.cells.Add(new HtmlCell { valor = item.alm_nombre });

                if (item.cenv_sociofac != null)
                {
                    row.cells.Add(new HtmlCell { valor = item.nombres_remfac + " " + item.apellidos_remfac });

                }
                else
                {
                    row.cells.Add(new HtmlCell { valor = item.nombres_remguia + " " + item.apellidos_remguia });

                }
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.montodocumento), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.montocancela), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.montodocumento - item.ddo_cancela), clase = Css.right });
                tdatos.AddRow(row);
            }*/
            html.AppendLine(tdatos.ToString());
            return html.ToString();
        }

        
        [WebMethod]
        public static string GetGuias(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);            
            string whereguias = "";
          
            foreach (Dcancelacion pls in comprobante.cancelaciones)
            {
                whereguias += ((whereguias != "") ? " and " : "") + " CAST(dca_comprobante AS varchar )+CAST(dca_comprobante_can AS varchar)+CAST(dca_pago AS varchar )+CAST(dca_secuencia AS varchar)!= '" + pls.dca_comprobante + pls.dca_comprobante_can + pls.dca_pago + pls.dca_secuencia + "' ";
             
            }
            if (whereguias != "")
                whereguias = " and " + whereguias;

            Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = comprobante.com_empresa, uxe_empresa_key = comprobante.com_empresa, uxe_usuario = comprobante.crea_usr, uxe_usuario_key = comprobante.crea_usr });
            
            StringBuilder html = new StringBuilder();
            
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span4 popupcontiner\">");
            HtmlTable td = new HtmlTable();
            td.CreteEmptyTable(3, 2);
            td.rows[0].cells[0].valor = "Almacen";
            td.rows[0].cells[1].valor = new Select { id = "cmbALMACEN_B", diccionario = Dictionaries.GetIDAlmacen(), valor = uxe.uxe_almacen.Value, clase = Css.medium }.ToString();
            td.rows[1].cells[0].valor = "P.Venta";
            td.rows[1].cells[1].valor = new Select { id = "cmbPVENTA_B", diccionario = new Dictionary<string, string>(), clase = Css.medium }.ToString();
            td.rows[2].cells[0].valor = "Número";
            td.rows[2].cells[1].valor = new Input { id = "txtNUMERO_B", clase = Css.small }.ToString();
            html.AppendLine(td.ToString());
            html.AppendLine(" </div><!--span4-->");
            html.AppendLine("<div class=\"span4 popupcontiner\">");
            HtmlTable td1 = new HtmlTable();
            td1.CreteEmptyTable(2, 2);
            td1.rows[0].cells[0].valor = "Fecha";
            td1.rows[0].cells[1].valor = new Input { id = "txtFECHA_B", datepicker = true, datetimevalor = DateTime.Now, clase = Css.small }.ToString();
            td1.rows[1].cells[0].valor = "";
            td1.rows[1].cells[1].valor = new Boton { click = "LoadDataBusquedaComprobante();return false;", valor = "Buscar" }.ToString();
            html.AppendLine(td1.ToString());
            html.AppendLine(new Input { id = "txtTIPODOC_B", valor = comprobante.com_tipodoc, visible = false }.ToString());

            html.AppendLine(" </div><!--span4-->");
            html.AppendLine("<div class=\"span4 popupcontiner\">");
            html.AppendLine("<ul class=\"list-nostyle list-inline\">");
            html.AppendLine("<li><div class=\"btn\" id=\"alldet_P\"><i class=\"iconfa-check\"></i> &nbsp;Todos</div></li>");
            html.AppendLine("<li><div class=\"btn\" id=\"nonedet_P\"><i class=\"iconfa-check-empty\"></i> &nbsp;Selección</div></li>");
            html.AppendLine("</ul>");
            html.AppendLine("<div id='msg_B'></div>");
            html.AppendLine("</div><!--span4-->");

            html.AppendLine("</div><!--row-fluid-->");

            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div id='detallegui' class=\"span11\">");

            html.AppendLine(" </div><!--span11-->");
            html.AppendLine("</div><!--row-fluid-->");



            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span11\">");
            HtmlTable tdatos = new HtmlTable();            
            tdatos.id = "tddatos_P";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";
            tdatos.AddColumn("Fecha", "width10", "", "");
            tdatos.AddColumn("Documento", "width10", "", "");
            tdatos.AddColumn("Guia", "width10", "", "");
            tdatos.AddColumn("Cancelacion", "width10", "", "");            
            tdatos.AddColumn("Monto", "width5", "", "");
            tdatos.AddColumn("Cancelado", "width5", "", "");
            tdatos.AddColumn("Saldo", "width5", "", "");  
            tdatos.editable = false;

            List<vCancelacion> lst = vCancelacionBLL.GetAll(new WhereParams("f.com_tipodoc = 4 AND dca_planilla is null and (cf.cenv_socio = {0} or cg.cenv_socio = {0}) " +whereguias, comprobante.com_codclipro), OrderByClause);

            //List<vDcancelacion> lst = vDcancelacionBLL.GetAll(new WhereParams("dca_empresa={0} and (envioguias.cenv_socio={1} or enviofacturas.cenv_socio={1}) and dca_planilla IS NULL "+whereguias, comprobante.com_empresa, comprobante.com_codclipro), "fechadetalle");   

            foreach (vCancelacion item in lst)
            {
                
                HtmlRow row = new HtmlRow();
                row.data = "data-comprobante=" + item.ddo_comprobante + " data-dca_pago=" + item.ddo_pago + " data-dca_transacc=" + item.ddo_transacc + " data-dca_doctran=" + item.ddo_doctran + " data-dca_comprobante_can=" + item.dca_comprobante_can + " data-dca_secuencia=" + item.dca_secuencia;
                //if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                //   row.markable = true;
                row.markable = true;
                row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi.Value.ToShortDateString() });
                //row.cells.Add(new HtmlCell { valor = item.ddo_doctran});
                row.cells.Add(new HtmlCell { valor = new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.ddo_comprobante + ")" }.ToString() + " " + item.ddo_doctran });
                //row.cells.Add(new HtmlCell { valor = item.ddo_comprobante_guia });
                row.cells.Add(new HtmlCell { valor = (item.ddo_comprobante_guia.HasValue) ? new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.ddo_comprobante_guia + ")" }.ToString() + " " + item.doctran_guia : "" });
                //row.cells.Add(new HtmlCell { valor = item.ddo_pago});
                //row.cells.Add(new HtmlCell { valor = item.alm_nombre });
                row.cells.Add(new HtmlCell { valor = new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.dca_comprobante_can + ")" }.ToString() + " " + item.doctran_can });

                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_monto), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_cancela), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_monto - item.ddo_cancela), clase = Css.right });
                tdatos.AddRow(row);
            }
            
           
            //foreach (vDcancelacion item in lst)
            //{
            //    HtmlRow row = new HtmlRow();
            //    row.data = "data-comprobante=" + item.dca_comprobante + " data-dca_pago=" + item.dca_pago + " data-dca_transacc=" + item.dca_transacc + " data-dca_doctran=" + item.doctrandetalle + " data-dca_comprobante_can=" + item.dca_comprobante_can + " data-dca_secuencia=" + item.dca_secuencia;
            //    if (comprobante.com_estado != Constantes.cEstadoMayorizado)
            //        row.markable = true;
            //    row.cells.Add(new HtmlCell { valor = item.fechadetalle.Value.ToShortDateString() });
            //    row.cells.Add(new HtmlCell { valor = item.doctrandetalle });
            //    row.cells.Add(new HtmlCell { valor = item.doctranguia });
            //    row.cells.Add(new HtmlCell { valor = item.dca_pago });
            //    row.cells.Add(new HtmlCell { valor = item.alm_nombre });

            //    if (item.cenv_sociofac != null)
            //    {
            //        row.cells.Add(new HtmlCell { valor = item.nombres_remfac + " " + item.apellidos_remfac });
                   
            //    }
            //    else {
            //        row.cells.Add(new HtmlCell { valor = item.nombres_remguia + " " + item.apellidos_remguia });
                   
            //    }
            //    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.montodocumento), clase = Css.right });
            //    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.montocancela), clase = Css.right });
            //    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.montodocumento - item.ddo_cancela), clase = Css.right });                
            //    tdatos.AddRow(row);
           
            //}
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span11-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }


        [WebMethod]
        public static string GetGuiasData(object objeto)
        {
            
            Comprobante comp = new Comprobante(objeto);

            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            int tipodocfac = Constantes.cFactura.tpd_codigo;
            int tipodocgui = Constantes.cGuia.tpd_codigo;

            if (comp.com_almacen.HasValue)
            {
                if (comp.com_almacen.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_almacen = {" + contador + "} ";
                    valores.Add(comp.com_almacen);
                    contador++;
                }
            }
            if (comp.com_pventa.HasValue)
            {
                if (comp.com_pventa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_pventa = {" + contador + "} ";
                    valores.Add(comp.com_pventa);
                    contador++;
                }
            }
            if (comp.com_numero > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (f.com_numero = {" + contador + "} or g.com_numero = {" + contador + "}) ";
                valores.Add(comp.com_numero);
                contador++;
            }
            

            string whereguias = "";

            foreach (Dcancelacion pls in comp.cancelaciones)
            {
                whereguias += ((whereguias != "") ? " and " : "") + " CAST(dca_comprobante AS varchar )+CAST(dca_comprobante_can AS varchar)+CAST(dca_pago AS varchar )+CAST(dca_secuencia AS varchar)!= '" + pls.dca_comprobante + pls.dca_comprobante_can + pls.dca_pago + pls.dca_secuencia + "' ";

            }
            if (whereguias != "")
                whereguias = " and " + whereguias;


            StringBuilder html = new StringBuilder();

           
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tddatos_P";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";
            tdatos.AddColumn("Fecha", "width10", "", "");
            tdatos.AddColumn("Factura", "width10", "", "");
            tdatos.AddColumn("Guia", "width10", "", "");
            tdatos.AddColumn("Cancelacion", "width10", "", "");
            tdatos.AddColumn("Monto", "width5", "", "");
            tdatos.AddColumn("Cancelado", "width5", "", "");
            tdatos.AddColumn("Saldo", "width5", "", "");
            tdatos.editable = false;

            List<vCancelacion> lst = vCancelacionBLL.GetAll(new WhereParams("f.com_tipodoc = 4 AND dca_planilla is null and (cf.cenv_socio = {0} or cg.cenv_socio = {0}) " + whereguias, comp.com_codclipro), OrderByClause);

            //List<vDcancelacion> lst = vDcancelacionBLL.GetAll(new WhereParams("dca_empresa={0} and (envioguias.cenv_socio={1} or enviofacturas.cenv_socio={1}) and dca_planilla IS NULL "+whereguias, comprobante.com_empresa, comprobante.com_codclipro), "fechadetalle");   

            foreach (vCancelacion item in lst)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-comprobante=" + item.ddo_comprobante + " data-dca_pago=" + item.ddo_pago + " data-dca_transacc=" + item.ddo_transacc + " data-dca_doctran=" + item.ddo_doctran + " data-dca_comprobante_can=" + item.dca_comprobante_can + " data-dca_secuencia=" + item.dca_secuencia;
                //if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                //   row.markable = true;
                row.markable = true;
                row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi.Value.ToShortDateString() });
                //row.cells.Add(new HtmlCell { valor = item.ddo_doctran});
                row.cells.Add(new HtmlCell { valor = new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.ddo_comprobante + ")" }.ToString() + " " + item.ddo_doctran });
                //row.cells.Add(new HtmlCell { valor = item.ddo_comprobante_guia });
                row.cells.Add(new HtmlCell { valor = (item.ddo_comprobante_guia.HasValue) ? new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.ddo_comprobante_guia + ")" }.ToString() + " " + item.doctran_guia : "" });
                //row.cells.Add(new HtmlCell { valor = item.ddo_pago});
                //row.cells.Add(new HtmlCell { valor = item.alm_nombre });
                row.cells.Add(new HtmlCell { valor = new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.dca_comprobante_can + ")" }.ToString() + " " + item.doctran_can });

                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_monto), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_cancela), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_monto - item.ddo_cancela), clase = Css.right });
                tdatos.AddRow(row);
            }


            //foreach (vDcancelacion item in lst)
            //{
            //    HtmlRow row = new HtmlRow();
            //    row.data = "data-comprobante=" + item.dca_comprobante + " data-dca_pago=" + item.dca_pago + " data-dca_transacc=" + item.dca_transacc + " data-dca_doctran=" + item.doctrandetalle + " data-dca_comprobante_can=" + item.dca_comprobante_can + " data-dca_secuencia=" + item.dca_secuencia;
            //    if (comprobante.com_estado != Constantes.cEstadoMayorizado)
            //        row.markable = true;
            //    row.cells.Add(new HtmlCell { valor = item.fechadetalle.Value.ToShortDateString() });
            //    row.cells.Add(new HtmlCell { valor = item.doctrandetalle });
            //    row.cells.Add(new HtmlCell { valor = item.doctranguia });
            //    row.cells.Add(new HtmlCell { valor = item.dca_pago });
            //    row.cells.Add(new HtmlCell { valor = item.alm_nombre });

            //    if (item.cenv_sociofac != null)
            //    {
            //        row.cells.Add(new HtmlCell { valor = item.nombres_remfac + " " + item.apellidos_remfac });

            //    }
            //    else {
            //        row.cells.Add(new HtmlCell { valor = item.nombres_remguia + " " + item.apellidos_remguia });

            //    }
            //    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.montodocumento), clase = Css.right });
            //    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.montocancela), clase = Css.right });
            //    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.montodocumento - item.ddo_cancela), clase = Css.right });                
            //    tdatos.AddRow(row);

            //}
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
            comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);
            StringBuilder html = new StringBuilder();

            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdrubros";
            tdatos.invoice = true;
            tdatos.clase = "rubros";
            //tdatos.titulo = "Factura";
            tdatos.AddColumn("Rubro", "", "", "");
            //tdatos.AddColumn("Observacion", "", "", "");
            tdatos.AddColumn("Ingreso", Css.mini, Css.right, "");
            tdatos.AddColumn("Egreso", Css.mini, Css.right, "");            
            
            tdatos.editable = true;

            List<Rubro> lst = RubroBLL.GetAll(new WhereParams("rub_empresa={0} and rub_estado=1", comprobante.com_empresa), "rub_nombre");
            List<Rubrosplanilla> lstpla = RubrosplanillaBLL.GetAll(new WhereParams("rpl_empresa= {0} and rpl_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "");
            foreach (Rubro item in lst)
            {
                Rubrosplanilla rpl = lstpla.Find(delegate(Rubrosplanilla r) { return r.rpl_rubro == item.rub_codigo; });
                HtmlRow row = new HtmlRow();
                row.data = "data-rubro=" + item.rub_codigo;
                //if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                //   row.markable = true;
                row.cells.Add(new HtmlCell { valor = item.rub_nombre });
                //row.cells.Add(new HtmlCell { valor = new Input { valor = (rpl != null) ? rpl.rpl_observacion : "" }.ToString() });
                row.cells.Add(new HtmlCell { valor = (item.rub_tipo == "I") ? new Input { valor = (rpl != null) ? Formatos.CurrencyFormat(rpl.rpl_valor) : "", clase=Css.mini }.ToString() : "" });
                row.cells.Add(new HtmlCell { valor = (item.rub_tipo == "E") ? new Input { valor = (rpl != null) ? Formatos.CurrencyFormat(rpl.rpl_valor): "", clase=Css.mini }.ToString() : "" });
                tdatos.AddRow(row);
            }
            html.AppendLine("<div class='rubros'>");
            html.AppendLine(tdatos.ToString());
            html.AppendLine("</div>");                                                                                                                     

            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(5, 2);
            tdatos1.rows[0].cells[0].valor = "Registros:";
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtREGISTROS", clase = Css.medium + Css.amount, habilitado = false , valor = 0 }.ToString();
            tdatos1.rows[1].cells[0].valor = "TOTAL:";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtTOTAL", clase = Css.medium + Css.amount, habilitado = false, valor = 0}.ToString();
            tdatos1.rows[2].cells[0].valor = "INGRESOS:";
            tdatos1.rows[2].cells[1].valor = new Input { id = "txtINGRESOS", clase = Css.medium + Css.amount, habilitado = false, valor = 0 }.ToString();
            tdatos1.rows[3].cells[0].valor = "EGRESOS:";
            tdatos1.rows[3].cells[1].valor = new Input { id = "txtEGRESOS", clase = Css.medium + Css.amount, habilitado = false, valor = 0 }.ToString();
            tdatos1.rows[4].cells[0].valor = "TOTAL LIQUIDAR:";
            tdatos1.rows[4].cells[1].valor = new Input { id = "txtTOTALCOM", clase = Css.medium + Css.totalamount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_total) }.ToString();
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
            if (string.IsNullOrEmpty(obj.com_concepto))
                obj.com_concepto = "PLANILLA SOCIOS" +per.per_apellidos + " " + per.per_nombres;
            obj.com_modulo = General.GetModulo(obj.com_tipodoc); ;
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

                foreach (Dcancelacion item in obj.cancelaciones)
                {
                    Dcancelacion dcancelacion = new Dcancelacion();
                    dcancelacion.dca_comprobante = item.dca_comprobante;
                    dcancelacion.dca_empresa = item.dca_empresa;
                    dcancelacion.dca_comprobante_can = item.dca_comprobante_can;
                    dcancelacion.dca_doctran = item.dca_doctran;
                    dcancelacion.dca_pago = item.dca_pago;
                    dcancelacion.dca_transacc = item.dca_transacc;
                    dcancelacion.dca_secuencia = item.dca_secuencia;
                    dcancelacion.dca_comprobante_key = item.dca_comprobante;
                    dcancelacion.dca_empresa_key = item.dca_empresa;
                    dcancelacion.dca_comprobante_can_key = item.dca_comprobante_can;
                    dcancelacion.dca_doctran_key = item.dca_doctran;
                    dcancelacion.dca_pago_key = item.dca_pago;
                    dcancelacion.dca_transacc_key = item.dca_transacc;
                    dcancelacion.dca_secuencia_key = item.dca_secuencia;
                    dcancelacion = DcancelacionBLL.GetByPK(dcancelacion);
                    dcancelacion.dca_planilla = obj.com_codigo;
                    DcancelacionBLL.Update(transaction, dcancelacion);
                }

                foreach (Rubrosplanilla item in obj.rubros)
                {
                    item.rpl_comprobante = obj.com_codigo;
                    RubrosplanillaBLL.Insert(transaction, item);
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

            Persona per = PersonaBLL.GetByPK(new Persona { per_empresa = obj.com_empresa, per_empresa_key = obj.com_empresa, per_codigo = obj.com_codclipro.Value, per_codigo_key = obj.com_codclipro.Value });

            objU.com_fecha = obj.com_fecha;
            objU.com_periodo = obj.com_fecha.Year;
            objU.com_codclipro = obj.com_codclipro;
            objU.com_agente = obj.com_agente;
            objU.mod_usr = obj.mod_usr;
            objU.mod_fecha = obj.mod_fecha;
            objU.com_concepto = !string.IsNullOrEmpty(obj.com_concepto) ? obj.com_concepto : "PLANILLA SOCIOS" + per.per_apellidos + " " + per.per_nombres;
            objU.com_estado = obj.com_estado;//ACTUALIZA EL ESTADO DEL COMPROBANTE



            Dtipocom dti = new Dtipocom();
            if (string.IsNullOrEmpty(objU.com_doctran))
            {
                objU.com_modulo = General.GetModulo(obj.com_tipodoc); ;
                objU.com_transacc = General.GetTransacc(obj.com_tipodoc);
                objU.com_nocontable = 1;//HAY QUE DEFINIR
                objU.com_descuadre = 0;
                objU.com_adestino = 0;
                objU.com_doctran = obj.com_doctran;
                objU.com_numero = obj.com_numero;
                dti = General.GetDtipocom(objU.com_empresa, objU.com_fecha.Year, objU.com_ctipocom, objU.com_almacen.Value, objU.com_pventa.Value);
                dti.dti_numero = dti.dti_numero + 1;

                if (objU.com_numero < dti.dti_numero)
                {
                    objU.com_numero = dti.dti_numero.Value;
                    objU.com_doctran = General.GetNumeroComprobante(objU);
                }

            }
            Total totaldel = TotalBLL.GetByPK(new Total { tot_empresa = obj.com_empresa, tot_empresa_key = obj.com_empresa, tot_comprobante = obj.com_codigo, tot_comprobante_key = obj.com_codigo });


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();                                                               


                ComprobanteBLL.Update(transaction, objU);

                obj.total.tot_empresa_key = obj.total.tot_empresa;
                obj.total.tot_comprobante_key = obj.total.tot_comprobante;

                TotalBLL.Delete(transaction, totaldel);
                TotalBLL.Insert(transaction, obj.total);


                List<Dcancelacion> lst = DcancelacionBLL.GetAll(new WhereParams("dca_empresa={0} and dca_planilla={1}", obj.com_empresa, obj.com_codigo), "");
                List<Rubrosplanilla> lstr = RubrosplanillaBLL.GetAll(new WhereParams("rpl_empresa={0} and rpl_comprobante={1}", obj.com_empresa, obj.com_codigo), "");

                foreach (Dcancelacion item in lst)
                {
                    item.dca_planilla = null;                    
                }   

                foreach (Dcancelacion item in obj.cancelaciones)
                {
                    Dcancelacion dcancelacion = new Dcancelacion();
                    dcancelacion.dca_comprobante = item.dca_comprobante;
                    dcancelacion.dca_empresa = item.dca_empresa;
                    dcancelacion.dca_comprobante_can = item.dca_comprobante_can;
                    dcancelacion.dca_doctran = item.dca_doctran;
                    dcancelacion.dca_pago = item.dca_pago;
                    dcancelacion.dca_transacc = item.dca_transacc;
                    dcancelacion.dca_secuencia = item.dca_secuencia;
                    dcancelacion.dca_comprobante_key = item.dca_comprobante;
                    dcancelacion.dca_empresa_key = item.dca_empresa;
                    dcancelacion.dca_comprobante_can_key = item.dca_comprobante_can;
                    dcancelacion.dca_doctran_key = item.dca_doctran;
                    dcancelacion.dca_pago_key = item.dca_pago;
                    dcancelacion.dca_transacc_key = item.dca_transacc;
                    dcancelacion.dca_secuencia_key = item.dca_secuencia;
                    dcancelacion = DcancelacionBLL.GetByPK(dcancelacion);
                    dcancelacion.dca_planilla = obj.com_codigo;
                    lst.Add(dcancelacion);
                }

                foreach (Dcancelacion item in lst)
                {                    
                    DcancelacionBLL.Update(transaction, item);
                }


                foreach (Rubrosplanilla item in lstr)
                {
                    RubrosplanillaBLL.Delete(transaction, item);
                }   

                foreach (Rubrosplanilla item in obj.rubros)
                {
                    item.rpl_comprobante = obj.com_codigo;
                    RubrosplanillaBLL.Insert(transaction, item);
                }
                if (dti.dti_numero.HasValue)
                    DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE

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