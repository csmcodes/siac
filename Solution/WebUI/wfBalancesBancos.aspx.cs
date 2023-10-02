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
    public partial class wfBalancesBancos : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "cue_id";
        protected static string WhereClause = "";
        protected static WhereParams parametros;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
            }
        }

        [WebMethod]
        public static string GetCabecera()
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(2, 2);
            tdatos.rows[0].cells[0].valor = "Cuenta Bancos:";
            tdatos.rows[0].cells[1].valor = new Select { id = "cmbCUENTABANCOS_S", clase = Css.large, diccionario = Dictionaries.GetBancos(), withempty = true }.ToString();        
            tdatos.rows[1].cells[0].valor = "Fecha";
            tdatos.rows[1].cells[1].valor = new Input { id = "cmbFECHA_S", datepicker = true, datetimevalor = DateTime.Now, clase = Css.large }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdtra = new HtmlTable();
            tdtra.CreteEmptyTable(2, 2);
            tdtra.rows[0].cells[0].valor = "Transacción:";
            tdtra.rows[0].cells[1].valor = new Select { id = "cmbTRANSACION_S", clase = Css.large, diccionario = Dictionaries.GetTransacc(Constantes.cBancos.mod_codigo), withempty = true }.ToString();
            tdtra.rows[1].cells[0].valor = "Almacen:";
            tdtra.rows[1].cells[1].valor = new Select { id = "cmbALMACEN_S", clase = Css.large, diccionario = Dictionaries.GetAlmacen(), withempty = true }.ToString();
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
            tdatos.AddColumn("ID", "", "", "");
            tdatos.AddColumn("Banco", "", "", "");
            tdatos.AddColumn("Numero", "", "", "");
            tdatos.AddColumn("Tipo", "", "", "");
            tdatos.AddColumn("Saldo", "", "", "");          
            tdatos.editable = false;
            html.AppendLine(tdatos.ToString());
            return html.ToString();
        }

        [WebMethod]
        public static string GetDetalleData(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object banco = null;            
            object salban= null;            
            object empresa = null;
            tmp.TryGetValue("banco", out banco);
            tmp.TryGetValue("salban", out salban);
            tmp.TryGetValue("empresa", out empresa);
            Banco bancos = new Banco(banco);           
            Salban saldosban = new Salban(salban);
           

            Empresa emp = new Empresa();
            emp.emp_codigo_key = (Int32)Conversiones.GetValueByType(empresa, typeof(Int32));
            emp = EmpresaBLL.GetByPK(emp);

            if (saldosban.slb_almacen == 0)
            {               
                saldosban.slb_almacen = 0;
            }
            if (!bancos.ban_ultcsc.HasValue)
            {
                bancos.ban_ultcsc = DateTime.Now;
            }           
            SetWhereClause(bancos);
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            List<Banco> lst = BancoBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            StringBuilder html = new StringBuilder();
            foreach (Banco item in lst)
            {
                ArrayList array = new ArrayList();
                array.Add(item.ban_id);
                array.Add(item.ban_nombre);             
                array.Add(item.ban_numero);
                array.Add((item.ban_tipo==1)?"Corriente":"Ahorros");
                decimal credito = General.SaldoBancos("a", 3, 1, emp.emp_codigo, item.ban_codigo, saldosban.slb_almacen, saldosban.slb_transacc, bancos.ban_ultcsc.Value);
             //   decimal debito = General.SaldoBancos("a", 3, 2, emp.emp_codigo, item.ban_codigo, saldosban.slb_almacen, saldosban.slb_transacc, bancos.ban_ultcsc.Value);
                array.Add(credito  );
                string strid = "{\"ban_codigo\":\"" + item.ban_codigo + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));
            }
            return html.ToString();
        }

        public static void SetWhereClause(Banco obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();                
                if (!string.IsNullOrEmpty(obj.ban_nombre))              
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " ban_nombre like {" + contador + "} ";
                    valores.Add("%" + obj.ban_nombre + "%");
                }
                if (obj.ban_codigo > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " ban_codigo = {" + contador + "} ";
                    valores.Add(obj.ban_codigo);
                    contador++;
                }
            parametros.valores = valores.ToArray();
        }
    }     
}