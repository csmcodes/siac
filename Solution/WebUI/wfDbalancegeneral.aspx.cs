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
    public partial class wfDbalancegeneral : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "com_fecha,com_doctran";
        protected static string WhereClause = "";
        protected static WhereParams parametros;
        protected static decimal total;
        protected static decimal total2;
        protected static Int32 codigocue;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
                txtcodigocue.Text = (Request.QueryString["codigocue"] != null) ? Request.QueryString["codigocue"].ToString() : "-1";
                codigocue = Convert.ToInt32(txtcodigocue.Text);
                total = 0;
                total2 = 0;
            }
        }

        [WebMethod]
        public static string GetCabecera(object objeto)
        {
            Empresa emp = new Empresa(objeto);
            emp.emp_codigo_key = emp.emp_codigo;
            emp = EmpresaBLL.GetByPK(emp);
            
            Cuenta cuenta = new Cuenta();
            cuenta.cue_codigo_key = codigocue;
            cuenta.cue_empresa_key = emp.emp_codigo;
            cuenta = CuentaBLL.GetByPK(cuenta);
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(4, 2);
            tdatos.rows[0].cells[0].valor = "Cuenta:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtCUENTA", autocomplete = "GetCuentaObj", obligatorio = true, valor = cuenta.cue_nombre, clase = Css.medium, placeholder = "Cuenta" }.ToString() + " " + new Input { id = "txtIDCUENTA", obligatorio = false, valor = cuenta.cue_id, clase = Css.medium, placeholder = "Id Cuenta ", habilitado = false }.ToString() + " " + new Input { id = "txtCODCCUENTA", visible = false, valor = cuenta.cue_codigo }.ToString();
            tdatos.rows[1].cells[0].valor = "Cliente:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtPERSON", autocomplete = "GetClienteObj", obligatorio = true, clase = Css.medium, placeholder = "Cliente" }.ToString() + " " + new Input { id = "txtCODPER", visible = false }.ToString();
            tdatos.rows[2].cells[0].valor = "Almacen:";
            tdatos.rows[2].cells[1].valor = new Select { id = "cmbALMACEN_S", clase = Css.large, diccionario = Dictionaries.GetAlmacen(), withempty = true }.ToString();
            tdatos.rows[3].cells[0].valor = "Deb/Cre:";
            tdatos.rows[3].cells[1].valor = new Select { id = "cmbDEBCRE_S", clase = Css.large, diccionario = Dictionaries.GetDebCre(), withempty = true }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdtra = new HtmlTable();
            tdtra.CreteEmptyTable(3, 2);
            tdtra.rows[0].cells[0].valor = "Fecha Inicial";
            tdtra.rows[0].cells[1].valor = new Input { id = "cmbFECHAINI_C", datepicker = true, datetimevalor = DateTime.Now, clase = Css.large}.ToString();
            tdtra.rows[1].cells[0].valor = "Fecha Final";
            tdtra.rows[1].cells[1].valor = new Input { id = "cmbFECHAFIN_C", datepicker = true, datetimevalor = DateTime.Now, clase = Css.large }.ToString();
            tdtra.rows[2].cells[0].valor = "Saldo inicial:";
            tdtra.rows[2].cells[1].valor = new Input { id = "txtSALDOINICIAL_S", clase = Css.medium, placeholder = "Saldo inicial", habilitado = false }.ToString();
            html.AppendLine(tdtra.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }

        [WebMethod]
        public static string GetPie()
        {
            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(1, 2);
            tdatos.rows[0].cells[0].valor = "Resultado del periodo:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtTOTALCOM", clase = Css.medium + Css.totalamount, habilitado = false, valor = Formatos.CurrencyFormat(0) }.ToString();
            html.AppendLine(tdatos.ToString());
            return html.ToString();
        }

        [WebMethod]
        public static string GetDetalle()
        {
            pageIndex = 1;
            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tddatos";
            tdatos.invoice = true;
            tdatos.clase = "scrolltable";
            tdatos.AddColumn("Fecha", "", "", "");
            tdatos.AddColumn("Comprobante", "", "", "");
            tdatos.AddColumn("Concepto", "", "", "");
            tdatos.AddColumn("Debito", "", "", "");
            tdatos.AddColumn("Credito", "", "", "");
            tdatos.AddColumn("Saldo Final", "", "", "");
            tdatos.editable = false;
            html.AppendLine(tdatos.ToString());
            total = 0;
            return html.ToString();
        }


        [WebMethod]
        public static string GetDetalleTotal(object objeto)
        {
            Dcontable salsoini = new Dcontable(objeto);
            Empresa emp = new Empresa();
            emp.emp_codigo_key = salsoini.dco_empresa;
            emp = EmpresaBLL.GetByPK(emp);
            if (salsoini.dco_almacen == 0 || !salsoini.dco_almacen.HasValue)
            {
                salsoini.dco_almacen = 0;
            }
            if (!salsoini.crea_fecha.HasValue)
            {
                salsoini.crea_fecha = DateTime.Now;
            }
            if (!salsoini.mod_fecha.HasValue)
            {
                salsoini.mod_fecha = DateTime.Now;
            }
            else
                salsoini.mod_fecha = salsoini.mod_fecha.Value.AddDays(1).Subtract(new TimeSpan(0, 0, 1));
            SetWhereClause(salsoini);
            int desde = (pageIndex * pageSize) - pageSize + 1;            
            if (desde == 1)
            {                
                //  List<Cuenta> lst = CNT.getBalance(emp.emp_codigo, salsoini.dco_almacen.Value, null, null, salsoini.crea_fecha.Value, 1);
                if (salsoini.dco_cuenta > 0)
                {
                 /*   DateTime ini = new DateTime(salsoini.crea_fecha.Value.Year, salsoini.crea_fecha.Value.Month, 1);
                    ini = ini.AddDays(-1);
                    */
                    //List<Cuenta> lst = CNT.getBalance(emp.emp_codigo, salsoini.dco_almacen.Value, null, null, salsoini.crea_fecha.Value.AddMilliseconds(-1), "a", 3);
                    //List<Cuenta> lst = CNT.getBalance(emp.emp_codigo, salsoini.dco_almacen.Value, null, null, salsoini.crea_fecha.Value, "a", 3);
                    List<Cuenta> lst = CNT.getBalanceNew(emp.emp_codigo, salsoini.dco_almacen.Value, null, null, salsoini.crea_fecha.Value, "i", 3,true,false);

                    //List<Cuenta> lst = CNT.getBalance(emp.emp_codigo, salsoini.dco_almacen.Value, null, null, salsoini.crea_fecha.Value, salsoini.mod_fecha.Value, "a", 3);
                    Cuenta cta = lst.Find(delegate(Cuenta c) { return c.cue_codigo == salsoini.dco_cuenta; });
                    total = cta.final;
                    total2 = cta.final;
                    List<Dcontable> lst2 = DcontableBLL.GetAll(parametros, OrderByClause);
                    foreach (Dcontable item in lst2)
                    {
                        if (item.dco_debcre == Constantes.cCredito)
                        {
                            total2 -= item.dco_valor_nac;
                        }
                        if (item.dco_debcre == Constantes.cDebito)
                        {
                            total2 += item.dco_valor_nac;
                        }
                    }                    
                }
                /*
                total = General.SaldoCuenta("a", 3, 1, emp.emp_codigo, salsoini.dco_cuenta, 0, salsoini.dco_almacen.Value, 0, salsoini.crea_fecha.Value);
                decimal total2 = General.SaldoCuenta("a", 3, 1, emp.emp_codigo, salsoini.dco_cuenta, 0, salsoini.dco_almacen.Value, 0, salsoini.mod_fecha.Value);*/
            }
            Object[] tot = { total, total2 };
            return new JavaScriptSerializer().Serialize(tot);
        }


        [WebMethod]
        public static string GetDetalleData(object objeto)
        {
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            List<Dcontable> lst = DcontableBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            StringBuilder html = new StringBuilder();
            foreach (Dcontable item in lst)
            {
                ArrayList array = new ArrayList();
                array.Add(item.dco_comprobantefecha);
                array.Add(item.dco_compdoctran);
                array.Add(item.dco_compconcepto);
                if (item.dco_debcre == Constantes.cCredito)
                {
                    array.Add(0);
                    array.Add(Formatos.CurrencyFormat(item.dco_valor_nac));
                    total -= item.dco_valor_nac;
                }
                if (item.dco_debcre == Constantes.cDebito)
                {
                    array.Add(Formatos.CurrencyFormat(item.dco_valor_nac));
                    array.Add(0);
                    total += item.dco_valor_nac;
                }
                array.Add(total);
                string strid = "{\"dco_empresa\":\"" + item.dco_empresa + "\", \"dco_comprobante\":\"" + item.dco_comprobante + "\", \"dco_secuencia\":\"" + item.dco_secuencia + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));
            }
            return html.ToString();
        }

        public static void SetWhereClause(Dcontable obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (obj.dco_cuenta > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dco_cuenta = {" + contador + "} ";
                valores.Add(obj.dco_cuenta);
                contador++;
            }
            if (obj.dco_almacen > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dco_almacen = {" + contador + "} ";
                valores.Add(obj.dco_almacen);
                contador++;
            }
            if (obj.dco_cliente > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dco_cliente = {" + contador + "} ";
                valores.Add(obj.dco_cliente);
                contador++;
            }
            if (obj.dco_debcre > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dco_debcre = {" + contador + "} ";
                valores.Add(obj.dco_debcre);
                contador++;
            }

            if (obj.dco_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dco_empresa = {" + contador + "} ";
                valores.Add(obj.dco_empresa);
                contador++;
            }

            parametros.where += ((parametros.where != "") ? " and " : "") + " com_fecha between {" + contador + "} ";
            valores.Add(obj.crea_fecha);
            contador++;
            parametros.where += ((parametros.where != "") ? "  " : "") + " and {" + contador + "} ";
            valores.Add(obj.mod_fecha);
            contador++;
            parametros.where += ((parametros.where != "") ? " and " : "") + " com_estado = {" + contador + "} ";
            valores.Add(Constantes.cEstadoMayorizado);
            contador++;
            parametros.valores = valores.ToArray();
        }
    }
}