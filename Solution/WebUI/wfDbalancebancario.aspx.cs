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
    public partial class wfDbalancebancario : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "com_fecha,com_doctran";
        protected static string WhereClause = "";
        protected static WhereParams parametros;
        protected static decimal total;
        protected static decimal total2;
        protected static Int32 codigoban;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
                txtcodigobanco.Text = (Request.QueryString["codigoban"] != null) ? Request.QueryString["codigoban"].ToString() : "-1";
                codigoban = Convert.ToInt32(txtcodigobanco.Text);
            }
        }

        [WebMethod]
        public static string GetCabecera(object objeto)
        {
            Empresa emp = new Empresa(objeto);
            emp.emp_codigo_key = emp.emp_codigo;
            emp = EmpresaBLL.GetByPK(emp);

            Banco banco = new Banco();
            banco.ban_codigo_key = codigoban;
            banco.ban_empresa_key = emp.emp_codigo;
            banco = BancoBLL.GetByPK(banco);
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(4, 2);
            tdatos.rows[0].cells[0].valor = "Banco:";
            tdatos.rows[0].cells[1].valor = new Input() { id = "txtIDBANCO", placeholder = "ID", autocomplete = "GetBancoObj", valor = banco.ban_nombre, clase = Css.blocklevel, obligatorio = true }.ToString() + new Input() { id = "txtCODBANCO", valor = banco.ban_codigo, visible = false }.ToString();
            tdatos.rows[1].cells[0].valor = "Beneficiario/Concepto";
            tdatos.rows[1].cells[1].valor = new Input { id = "cmbBENEFICIARIO_C", placeholder = "Beneficiario/Concepto", clase = Css.large }.ToString();
            tdatos.rows[2].cells[0].valor = "Transacción:";
            tdatos.rows[2].cells[1].valor = new Select { id = "cmbTRANSACION_S", clase = Css.large, diccionario = Dictionaries.GetTransacc(Constantes.cBancos.mod_codigo), withempty = true }.ToString();
            tdatos.rows[3].cells[0].valor = "Almacen:";
            tdatos.rows[3].cells[1].valor = new Select { id = "cmbALMACEN_S", clase = Css.large, diccionario = Dictionaries.GetAlmacen(), withempty = true }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdtra = new HtmlTable();
            tdtra.CreteEmptyTable(3, 2);
            tdtra.rows[0].cells[0].valor = "Fecha Inicial";
            tdtra.rows[0].cells[1].valor = new Input { id = "cmbFECHAINI_C", datepicker = true, datetimevalor = DateTime.Now, clase = Css.large }.ToString();
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
            tdatos.AddColumn("Documento", "", "", "");
            tdatos.AddColumn("Transaccion", "", "", "");
            tdatos.AddColumn("Valor", "", "", "");
            tdatos.AddColumn("Saldo ", "", "", "");
            tdatos.editable = false;
            html.AppendLine(tdatos.ToString());
            total = 0;
            return html.ToString();
        }


        [WebMethod]
        public static string GetDetalleTotal(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object dbancario = null;
            object salban = null;
            object emp_codigo = null;
            tmp.TryGetValue("dbancario", out dbancario);
            tmp.TryGetValue("emp_codigo", out emp_codigo);
            tmp.TryGetValue("salban", out salban);

            Dbancario bancos = new Dbancario(dbancario);
            Salban saldosban = new Salban(salban);
            Empresa emp = new Empresa();
            emp.emp_codigo_key = (Int32)Conversiones.GetValueByType(emp_codigo, typeof(Int32));
            emp = EmpresaBLL.GetByPK(emp);

            bancos.dban_transacc = saldosban.slb_transacc;
            if (saldosban.slb_almacen == 0)
            {
                saldosban.slb_almacen = 0;
            }
            if (!bancos.dban_fechacsc.HasValue)
            {
                bancos.dban_fechacsc = DateTime.Now;
            }
            if (!bancos.dban_fechaant.HasValue)
            {
                bancos.dban_fechaant = DateTime.Now;
            }
            SetWhereClause(bancos);
            int desde = (pageIndex * pageSize) - pageSize + 1;
            if (desde == 1)
            {



                //bancos.dban_fechacsc = bancos.dban_fechacsc.Value.AddDays(-1);
                bancos.dban_fechacsc = bancos.dban_fechacsc.Value.AddSeconds(-1);
                total = General.SaldoBancos("a", 3, 1, emp.emp_codigo, bancos.dban_banco, saldosban.slb_almacen, bancos.dban_transacc, bancos.dban_fechacsc.Value);
                List<Dbancario> lst2 = DbancarioBLL.GetAll(parametros, OrderByClause);
                total2 = 0;
                foreach (Dbancario item in lst2)
                {
                    if (item.dban_debcre == Constantes.cCredito)
                    {
                        total2 -= item.dban_valor_nac;
                    }
                    if (item.dban_debcre == Constantes.cDebito)
                    {
                        total2 += item.dban_valor_nac;
                    }
                }                    
            }
            Object[] tot = { total, total2 };
            return new JavaScriptSerializer().Serialize(tot);
        }




        [WebMethod]
        public static string GetDetalleData(object objeto)
        {
            GetDetalleTotal(objeto);
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            List<Dbancario> lst = DbancarioBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            StringBuilder html = new StringBuilder();
            foreach (Dbancario item in lst)
            {
                ArrayList array = new ArrayList();
                array.Add(item.dban_comprobantefecha);
                array.Add(item.dban_compdoctran);
                array.Add(item.dban_compconcepto);
                array.Add(item.dban_documento);
                array.Add(item.dban_transacc);
                array.Add(Formatos.CurrencyFormat(item.dban_valor_nac));
                if (item.dban_debcre == Constantes.cCredito)
                {
                    total -= item.dban_valor_nac;
                }
                if (item.dban_debcre == Constantes.cDebito)
                {
                    total += item.dban_valor_nac;
                }
                array.Add(total);
                string strid = "{\"dban_empresa\":\"" + item.dban_empresa + "\", \"dban_cco_comproba\":\"" + item.dban_cco_comproba + "\", \"dban_secuencia\":\"" + item.dban_secuencia + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));
            }
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

        public static void SetWhereClause(Dbancario obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (obj.dban_banco > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dban_banco = {" + contador + "} ";
                valores.Add(obj.dban_banco);
                contador++;
            }
            
            if (obj.dban_transacc > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dban_transacc = {" + contador + "} ";
                valores.Add(obj.dban_transacc);
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.dban_beneficiario))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + "( dban_beneficiario like  {" + contador + "} or  com_concepto like  {" + contador + "}  )";
                valores.Add("%" + obj.dban_beneficiario + "%");
                contador++;
            }
            parametros.where += ((parametros.where != "") ? " and " : "") + " com_fecha between {" + contador + "} ";
            valores.Add(obj.dban_fechacsc);
            contador++;
            parametros.where += ((parametros.where != "") ? "  " : "") + " and {" + contador + "} ";
            valores.Add(obj.dban_fechaant.Value.AddDays(1).AddSeconds(-1));
            contador++;
            
               parametros.where += ((parametros.where != "") ? " and " : "") + " (com_estado = {" + contador + "} ";
            valores.Add( Constantes.cEstadoPorAutorizar);
            contador++;
            parametros.where += ((parametros.where != "") ? "  " : "") + " or com_estado= {" + contador + "}) ";
            valores.Add(Constantes.cEstadoMayorizado);
            contador++;

            parametros.valores = valores.ToArray();
        }
    }
}