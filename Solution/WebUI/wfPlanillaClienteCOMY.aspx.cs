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
    public partial class wfPlanillaClienteCOMY : System.Web.UI.Page
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
                txtcodigocomp.Text = (Request.QueryString["codigocomp"] != null) ? Request.QueryString["codigocomp"].ToString() : "-1";
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

            List<Planillacomprobante> lstplanillacomp = PlanillacomprobanteBLL.GetAll(new WhereParams("pco_empresa ={0} and pco_comprobante_pla={1}", comprobante.com_empresa, comprobante.com_codigo), "");




            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Cliente:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtIDPER", valor = cli.per_id, autocomplete = "GetClienteObj", obligatorio = true, habilitado = ((comprobante.com_estado == Constantes.cEstadoMayorizado) ? false : true), clase = Css.medium, placeholder = "Cliente" }.ToString() + " " + new Input { id = "txtNOMBRES", clase = Css.large, habilitado = false, valor = cli.per_apellidos + " " + cli.per_nombres }.ToString() + new Input { id = "txtCODPER", visible = false, valor = cli.per_codigo }.ToString();
            tdatos.rows[1].cells[0].valor = "CI-RUC/Razón Social:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtRUC", clase = Css.medium, habilitado = false, valor = cli.per_ciruc }.ToString() + " " + new Input { id = "txtRAZON", clase = Css.large, habilitado = false, valor = cli.per_razon }.ToString();
            tdatos.rows[2].cells[0].valor = "Teléfono/Dirección:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtTELEFONO", clase = Css.medium, habilitado = false, valor = cli.per_telefono }.ToString() + " " + new Input { id = "txtDIRECCION", clase = Css.large, habilitado = false, valor = cli.per_direccion }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");
            html.AppendLine(new Input { id = "txtESTADO", valor = comprobante.com_estado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCERRADO", valor = Constantes.cEstadoMayorizado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCREADO", valor = Constantes.cEstadoProceso, visible = false }.ToString());

            string factura = "";
            if (lstplanillacomp.Count > 0)
                factura = lstplanillacomp[0].pco_comprobante_fac.ToString();

            html.AppendLine(new Input { id = "txtFACTURA", valor = factura, visible = false }.ToString());

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
            tdatos.AddColumn("Socio", "width20", "", "");
            tdatos.AddColumn("Comprobante", "width20", "", "");
            tdatos.AddColumn("Subtotal 0", "width5", "");
            tdatos.AddColumn("Subtotal IVA", "width5", "");
            tdatos.AddColumn("IVA", "width5", "");
            tdatos.AddColumn("Seguro", "width5", "");
            tdatos.AddColumn("Transporte", "width5", "");
            tdatos.AddColumn("Valor", "width5", "");

            tdatos.editable = false;

            //List<vPlanillaCliente> lst = vPlanillaClienteBLL.GetAll(new WhereParams("cabecera.com_empresa={0} and cabecera.com_codigo={1}", comprobante.com_empresa, comprobante.com_codigo), "detalle_apellidos,detalle_nombres, detalle_fecha");
            List<vPlanillaCliente> lst = vPlanillaClienteBLL.GetAll(new WhereParams("cabecera.com_empresa={0} and cabecera.com_codigo={1}", comprobante.com_empresa, comprobante.com_codigo), "detalle.com_numero");

            foreach (vPlanillaCliente item in lst)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-comprobante=" + item.detalle_codigo;

                if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                    row.markable = true;

                row.cells.Add(new HtmlCell { valor = item.detalle_fecha.Value.ToShortDateString() });
                //row.cells.Add(new HtmlCell { valor = item.detalle_apellidos+ " " + item.detalle_nombres});
                row.cells.Add(new HtmlCell { valor = new Boton { small = true, id = "btnaso", tooltip = "Asignar Socio", clase = "iconsweets-magnifying", click = "CallAsignarSocio(" + item.detalle_codigo + ")" }.ToString() + " " + item.detalle_apellidos + " " + item.detalle_nombres });
                row.cells.Add(new HtmlCell { valor = new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.detalle_codigo + ")" }.ToString() + " " + item.detalle_doctran });
                //row.cells.Add(new HtmlCell { valor = item.detalle_doctran });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.detalle_subtot_0), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.detalle_subtotal), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.detalle_iva), clase = Css.right });
                decimal valorseguro = 0;
                if (item.detalle_declarado.HasValue)
                    valorseguro = item.detalle_declarado.Value * (item.detalle_porcseguro.Value / 100);
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(valorseguro), clase = Css.right });
                //row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.detalle_seguro), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.detalle_transporte), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.detalle_total), clase = Css.right });


                tdatos.AddRow(row);

            }

            html.AppendLine(tdatos.ToString());



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
            html.AppendLine("<li><div class=\"btn\" id=\"alldet_P\"><i class=\"iconfa-check\"></i> &nbsp; Seleccionar Todos</div></li>");
            html.AppendLine("<li><div class=\"btn\" id=\"nonedet_P\"><i class=\"iconfa-check-empty\"></i> &nbsp; Limpiar Selección</div></li>");
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
            tdatos.AddColumn("Subtot0", "width5", "");
            tdatos.AddColumn("SubtotIVA", "width5", "");
            tdatos.AddColumn("Valor", "width5", "");

            //tdatos.editable = true;
            List<vGuias> lst = vGuiasBLL.GetAll(new WhereParams("com_empresa={0} and com_codclipro={1} and (com_tipodoc ={2} or com_tipodoc ={3}) and plc_empresa is null " + whereguias, comprobante.com_empresa, comprobante.com_codclipro, 13, 4), "com_fecha");
            foreach (vGuias item in lst)
            {
                HtmlRow row = new HtmlRow();
                row.markable = true;
                row.data = "data-comprobante=" + item.com_codigo;
                row.cells.Add(new HtmlCell { valor = item.com_fecha.Value.ToShortDateString() });
                row.cells.Add(new HtmlCell { valor = item.per_apellidos + " " + item.per_nombres });
                row.cells.Add(new HtmlCell { valor = item.com_doctran });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.tot_subtot_0), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.tot_subtotal), clase = Css.right });
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

            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(3, 2);
            tdatos1.rows[0].cells[0].valor = "Registros:";
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtREGISTROS", clase = Css.medium + Css.amount, habilitado = false, valor = 0 }.ToString();
            tdatos1.rows[1].cells[0].valor = "SEGURO:";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtSEGURO", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_tseguro) }.ToString();
            tdatos1.rows[2].cells[0].valor = "TRANSPORTE:";
            tdatos1.rows[2].cells[1].valor = new Input { id = "txtTRANSPORTE", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_transporte) }.ToString();
            html.AppendLine(tdatos1.ToString());

            HtmlTable tdatos2 = new HtmlTable();
            tdatos2.CreteEmptyTable(4, 2);
            tdatos2.rows[0].cells[0].valor = "SUBTOTAL 0:";
            tdatos2.rows[0].cells[1].valor = new Input { id = "txtSUBTOTAL0", clase = Css.small + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_subtot_0) }.ToString();
            tdatos2.rows[1].cells[0].valor = "SUBTOTAL IVA:";
            tdatos2.rows[1].cells[1].valor = new Input { id = "txtSUBTOTALIVA", clase = Css.small + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_subtotal) }.ToString();

           

            decimal valoriva = Constantes.GetValorIVA(DateTime.Now);
            if (comprobante.com_estado == Constantes.cEstadoMayorizado)
            {
                if (comprobante.total.tot_porc_impuesto.HasValue)
                    valoriva = comprobante.total.tot_porc_impuesto.Value;
            }

            tdatos2.rows[2].cells[0].valor = "IVA " + valoriva + "%:";            
            tdatos2.rows[2].cells[1].valor = new Input { id = "txtIVAPORCENTAJE", visible = false, valor = valoriva.ToString().Replace(",", ".") }.ToString() + new Input { id = "txtIVA", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_timpuesto) }.ToString();


            tdatos2.rows[3].cells[0].valor = "TOTAL:";
            tdatos2.rows[3].cells[1].valor = new Input { id = "txtTOTALCOM", clase = Css.medium + Css.totalamount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_total) }.ToString();

            html.AppendLine(tdatos2.ToString());

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
            Dtipocom dti = General.GetDtipocom(obj.com_empresa, obj.com_fecha.Year, obj.com_ctipocom, obj.com_almacen.Value, obj.com_pventa.Value);
            //dti.dti_numero = dti.dti_numero.Value + 1;
            obj.com_numero = General.GetNumeroLibre(dti).dti_numero.Value;
            #endregion

            Persona per = PersonaBLL.GetByPK(new Persona { per_empresa = obj.com_empresa, per_empresa_key = obj.com_empresa, per_codigo = obj.com_codclipro.Value, per_codigo_key = obj.com_codclipro.Value });

            obj.com_numero = dti.dti_numero.Value;
            obj.com_concepto = "PLANILLA CLIENTE " + per.per_apellidos + " " + per.per_nombres;
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

                foreach (Planillacli item in obj.planillas)
                {
                    item.plc_comprobante_pla = obj.com_codigo;
                    PlanillacliBLL.Insert(transaction, item);
                }
                DtipocomBLL.Update(transaction, dti);
                transaction.Commit();
            }
            catch (Exception ex)
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
            objU.com_concepto = !string.IsNullOrEmpty(obj.com_concepto) ? obj.com_concepto : "PLANILLA CLIENTE " + per.per_apellidos + " " + per.per_nombres;
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