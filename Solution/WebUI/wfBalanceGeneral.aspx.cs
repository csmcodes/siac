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
    public partial class wfBalanceGeneral : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "cue_id";
        protected static string WhereClause = "";
        protected static WhereParams parametros;
        protected static int? debcre;
        protected static decimal total;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtdebcre.Text = (Request.QueryString["debcre"] != null) ? Request.QueryString["debcre"].ToString() : "-1";
                debcre = Convert.ToInt32(txtdebcre.Text);
                pageIndex = 1;
                pageSize = 20;
            }
        }

        [WebMethod]
        public static string GetCabecera()
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span4\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Almacen:";
            tdatos.rows[0].cells[1].valor = new Select { id = "cmbALMACEN_S", clase = Css.large, diccionario = Dictionaries.GetAlmacen(), withempty = true }.ToString();
            tdatos.rows[1].cells[0].valor = "Centro:";
            tdatos.rows[1].cells[1].valor = new Select { id = "cmbCENTRO_S", clase = Css.large, diccionario = Dictionaries.GetCentro(), valor = 1, withempty = true, habilitado = false }.ToString();
            tdatos.rows[2].cells[0].valor = "Fecha";
            tdatos.rows[2].cells[1].valor = new Input { id = "cmbFECHA_C", datepicker = true, datetimevalor = DateTime.Now, clase = Css.large }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span4-->");
            html.AppendLine("<div class=\"span4\">");
            HtmlTable tdtra = new HtmlTable();
            tdtra.CreteEmptyTable(3, 2);
            tdtra.rows[0].cells[0].valor = "Tipo";
            tdtra.rows[0].cells[1].valor = new Select { id = "cmbTIPO_C", diccionario = Dictionaries.GetTipoInforme(), clase = Css.large, valor = "a" }.ToString();
            tdtra.rows[1].cells[0].valor = "Nivel:";
            tdtra.rows[1].cells[1].valor = new Input { id = "cmbNIVEL_S", clase = Css.medium, placeholder = "Nivel" }.ToString();
            tdtra.rows[2].cells[0].valor = "Modulo:";
            tdtra.rows[2].cells[1].valor = new Select { id = "cmbMODULO_S", clase = Css.large, diccionario = Dictionaries.GetModulos(), withempty = true }.ToString();                        
           
            html.AppendLine(tdtra.ToString());
            html.AppendLine(" </div><!--span4-->");

            html.AppendLine("<div class=\"span4\">");
            HtmlTable tdopc = new HtmlTable();
            tdopc.CreteEmptyTable(3, 2);
            tdopc.rows[0].cells[0].valor = "Movimiento:";
            tdopc.rows[0].cells[1].valor = new Check { id = "chkMOVIMIENTO_S", clase = Css.medium }.ToString();
            tdopc.rows[0].cells[1].ancho = 200;
            tdopc.rows[1].cells[0].valor = "Ver todas:";
            tdopc.rows[1].cells[1].valor = new Check { id = "chkTODAS_S", clase = Css.medium }.ToString();
            tdopc.rows[2].cells[0].valor = "Con saldo final";
            tdopc.rows[2].cells[1].valor = new Check { id = "chkSALDO_S", clase = Css.medium }.ToString();
            html.AppendLine(tdopc.ToString());
            html.AppendLine(" </div><!--span4-->");

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
            tdatos.AddColumn("ID", "", "", "");
            tdatos.AddColumn("Cuenta", "", "", "");
            tdatos.AddColumn("Saldo inicial", Css.right, "", "");
            tdatos.AddColumn("Debito", Css.right, "", "");
            tdatos.AddColumn("Credito", Css.right, "", "");
            tdatos.AddColumn("Saldo Final", Css.right, "", "");
            tdatos.editable = false;
            html.AppendLine(tdatos.ToString());
            total = 0;
            return html.ToString();
        }

        [WebMethod]
        public static string CalTotales(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object cuenta = null;
            object dcontable = null;
            object empresa = null;
            object tipo = null;
            tmp.TryGetValue("cuenta", out cuenta);
            tmp.TryGetValue("empresa", out empresa);
            tmp.TryGetValue("dcontable", out dcontable);
            tmp.TryGetValue("tipo", out tipo);
            if (tipo == null)
                tipo = "a";
            Cuenta cuentas = new Cuenta(cuenta);
            Dcontable dcontables = new Dcontable(dcontable);
            cuentas.cue_movimiento = 1;
            cuentas.cue_nivel = 0;
            SetWhereClause(cuentas);
            Empresa emp = new Empresa();            
            emp.emp_codigo_key = (Int32)Conversiones.GetValueByType(empresa, typeof(Int32));
            emp = EmpresaBLL.GetByPK(emp);
            if (dcontables.dco_almacen == 0 || !dcontables.dco_almacen.HasValue)
            {
                dcontables.dco_almacen = 0;
            }
            if (!dcontables.dco_fecha_vence.HasValue)
            {
                dcontables.dco_fecha_vence = DateTime.Now;
            }
            total = 0;

            List<Cuenta> lst = new List<Cuenta>();// CNT.getBalance(emp.emp_codigo, dcontables.dco_almacen.Value, null, null, dcontables.dco_fecha_vence.Value, tipo.ToString(), 3);

            //   List<Cuenta> lst = CuentaBLL.GetAll(parametros, OrderByClause);
            //   var date1 = new DateTime(dcontables.dco_fecha_vence.Value.Year, dcontables.dco_fecha_vence.Value.Month, 1, dcontables.dco_fecha_vence.Value.Hour, dcontables.dco_fecha_vence.Value.Minute, dcontables.dco_fecha_vence.Value.Second);
            //   date1 = date1.AddDays(-1);
            foreach (Cuenta item in lst)
            {
                /*  decimal inicial = General.SaldoCuenta("m", 3, 1, emp.emp_codigo, item.cue_codigo, 0, dcontables.dco_almacen.Value,0, date1);
                  decimal debito = General.SaldoCuenta("m", Constantes.cDebito, 1, emp.emp_codigo, item.cue_codigo, 0, dcontables.dco_almacen.Value, 0, dcontables.dco_fecha_vence.Value);
                  decimal credito = General.SaldoCuenta("m", Constantes.cCredito, 1, emp.emp_codigo, item.cue_codigo, 0, dcontables.dco_almacen.Value, 0, dcontables.dco_fecha_vence.Value);
                  decimal final = inicial + debito - credito;*/

                if (debcre == 1)
                {
                    if (item.cue_movimiento == 1 && (item.cue_genero == 1 || item.cue_genero == 3 ))
                    {
                        total += item.final;
                    }

                    if (item.cue_movimiento == 1 && (item.cue_genero == 2))
                    {
                        total -= item.final;
                    }


                }
                if (debcre == 2)
                {
                    if (item.cue_movimiento == 1 && item.cue_genero == 4)
                    {
                        total += item.final;
                    }

                    if (item.cue_movimiento == 1 && ( item.cue_genero == 5 || item.cue_genero == 6 || item.cue_genero == 7))
                    {
                        total -= item.final;
                    }
                }




            }
            return new JavaScriptSerializer().Serialize(total);

        }

        [WebMethod]
        public static string GetDetalleData(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object cuenta = null;
            object dcontable = null;
            object empresa = null;
            object tipo = null;
            object todas = null;
            object saldo = null;
            tmp.TryGetValue("cuenta", out cuenta);
            tmp.TryGetValue("empresa", out empresa);
            tmp.TryGetValue("dcontable", out dcontable);
            tmp.TryGetValue("tipo", out tipo);
            tmp.TryGetValue("todas", out todas);
            tmp.TryGetValue("saldo", out saldo);
            if (tipo == null)
                tipo = "a";

            bool all = false;
            bool.TryParse(todas.ToString(), out all);

            bool sal = false;
            bool.TryParse(saldo.ToString(), out sal);

            Cuenta cuentas = new Cuenta(cuenta);
            Dcontable dcontables = new Dcontable(dcontable);
            Empresa emp = new Empresa();
            emp.emp_codigo_key = (Int32)Conversiones.GetValueByType(empresa, typeof(Int32));
            emp = EmpresaBLL.GetByPK(emp);



            


            if (dcontables.dco_almacen == 0 || !dcontables.dco_almacen.HasValue)
            {
                dcontables.dco_almacen = 0;
            }
            if (!dcontables.dco_fecha_vence.HasValue)
            {
                dcontables.dco_fecha_vence = DateTime.Now;
            }
            SetWhereClause(cuentas);
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            decimal ctotal = 0;
            //List<Cuenta> lst = CNT.getBalance(emp.emp_codigo, dcontables.dco_almacen.Value, null, null, dcontables.dco_fecha_vence.Value, "m", 3);
            List<Cuenta> lst = CNT.getBalanceNew(emp.emp_codigo, dcontables.dco_almacen.Value, null, null, dcontables.dco_fecha_vence.Value, tipo.ToString(), 3,all,sal);
            StringBuilder html = new StringBuilder();
            foreach (Cuenta item in lst)
            {
                bool flag = true;
                if (debcre == 1)//BALANCE GENERAL
                {
                    //if (item.cue_genero > 3 || item.cue_genero < 1)
                    if (item.cue_genero > 0 && item.cue_genero < 4)
                    {
                        
                        if (item.cue_movimiento == 1)
                            ctotal += item.final;
                    }
                    else
                        flag = false;

                    /*if (item.cue_movimiento == 1 && (item.cue_genero == 1 || item.cue_genero == 3))
                        ctotal += item.final;

                    if (item.cue_movimiento == 1 && (item.cue_genero == 2))
                        ctotal -= item.final;*/
                }
                if (debcre == 2)//ESTADO DE RESULTADOS
                {
                    //if (item.cue_genero > 7 || item.cue_genero < 4)
                    if (item.cue_genero > 3 && item.cue_genero < 8)
                    {
                        
                        if (item.cue_movimiento == 1)
                            ctotal += item.final;
                    }
                    else
                        flag = false;
                    /*if (item.cue_movimiento == 1 && item.cue_genero == 4)
                        ctotal += item.final;

                    if (item.cue_movimiento == 1 && (item.cue_genero == 5 || item.cue_genero == 6 || item.cue_genero == 7))
                        ctotal -= item.final;*/

                }

                if (cuentas.cue_nivel > 0 && item.cue_nivel > cuentas.cue_nivel)
                {
                    flag = false;
                }

                if (cuentas.cue_movimiento > 0 && item.cue_movimiento != cuentas.cue_movimiento)
                {
                    flag = false;
                }
                if (cuentas.cue_modulo > 0 && item.cue_modulo != cuentas.cue_modulo)
                {
                    flag = false;
                }
                if (flag)
                {
                    HtmlRow row = new HtmlRow();
                    row.data = "data-codigo='" + item.cue_codigo+ "' ";
                    row.cells.Add(new HtmlCell { valor = item.cue_id});
                    row.removable = false;
                    for (int i = 1; i <= item.cue_nivel; i++)
                    {
                        item.cue_nombre = "     " + item.cue_nombre;
                    }
                    row.cells.Add(new HtmlCell { valor = item.cue_nombre});
                    row.cells.Add(new HtmlCell { valor = Formatos.BalanceFormat(item.inicial), clase=Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.BalanceFormat(item.debito), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.BalanceFormat(item.credito), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.BalanceFormat(item.final), clase = Css.right  });
                    html.AppendLine(row.ToString());


                    /*ArrayList array = new ArrayList();
                    array.Add(item.cue_id);
                    for (int i = 1; i <= item.cue_nivel; i++)
                    {
                        item.cue_nombre = "     " + item.cue_nombre;
                    }
                    array.Add(item.cue_nombre);
                    array.Add(Formatos.CurrencyFormat(item.inicial));
                    array.Add(Formatos.CurrencyFormat(item.debito));
                    array.Add(Formatos.CurrencyFormat(item.credito));
                    array.Add(Formatos.CurrencyFormat(item.final));
                    string strid = "{\"cue_codigo\":\"" + item.cue_codigo + "\"}";//ID COMPUESTO
                    html.AppendLine(HtmlElements.TablaRow(array, strid));*/
                }
            }
            //List<Cuenta> lst = CuentaBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            //StringBuilder html = new StringBuilder();
            //foreach (Cuenta item in lst)
            //{
            //    ArrayList array = new ArrayList();
            //    array.Add(item.cue_id);
            //    for (int i = 1; i <= item.cue_nivel; i++)
            //    {
            //        item.cue_nombre = "     " + item.cue_nombre;
            //    }
            //    array.Add(item.cue_nombre);
            //    var date1 = new DateTime(dcontables.dco_fecha_vence.Value.Year, dcontables.dco_fecha_vence.Value.Month, 1, dcontables.dco_fecha_vence.Value.Hour, dcontables.dco_fecha_vence.Value.Minute, dcontables.dco_fecha_vence.Value.Second);
            //    date1 = date1.AddDays(-1);
            //    decimal inicial = General.SaldoCuenta("m", 3, 1, emp.emp_codigo, item.cue_codigo, 1, dcontables.dco_almacen.Value, 1, date1);
            //    decimal debito = General.SaldoCuenta("m", Constantes.cDebito, 1, emp.emp_codigo, item.cue_codigo, 1, dcontables.dco_almacen.Value, 1, dcontables.dco_fecha_vence.Value);
            //    decimal credito = General.SaldoCuenta("m", Constantes.cCredito, 1, emp.emp_codigo, item.cue_codigo, 1, dcontables.dco_almacen.Value, 1, dcontables.dco_fecha_vence.Value);
            //    decimal final = inicial + debito - credito;
            //    array.Add(inicial);
            //    array.Add(debito);
            //    array.Add(credito);
            //    array.Add(final);
            //    string strid = "{\"cue_codigo\":\"" + item.cue_codigo + "\"}";//ID COMPUESTO
            //    html.AppendLine(HtmlElements.TablaRow(array, strid));
            //}

            ArrayList retorno = new ArrayList();
            retorno.Add(ctotal);
            retorno.Add(html.ToString());

            return new JavaScriptSerializer().Serialize(retorno);
            //return html.ToString();
        }

        public static void SetWhereClause(Cuenta obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (debcre == 1)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (cue_genero = {" + contador + "} ";
                valores.Add(1);
                contador++;
                parametros.where += ((parametros.where != "") ? " or " : "") + " cue_genero = {" + contador + "} ";
                valores.Add(2);
                contador++;
                parametros.where += ((parametros.where != "") ? " or " : "") + " cue_genero = {" + contador + "} )";
                valores.Add(3);
                contador++;
            }
            if (debcre == 2)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (cue_genero = {" + contador + "} ";
                valores.Add(4);
                contador++;
                parametros.where += ((parametros.where != "") ? " or " : "") + " cue_genero = {" + contador + "} ";
                valores.Add(5);
                contador++;
                parametros.where += ((parametros.where != "") ? " or " : "") + " cue_genero = {" + contador + "} ";
                valores.Add(6);
                contador++;
                parametros.where += ((parametros.where != "") ? " or " : "") + " cue_genero = {" + contador + "} )";
                valores.Add(7);
                contador++;
            }
            if (obj.cue_nivel > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cue_nivel <= {" + contador + "} ";
                valores.Add(obj.cue_nivel);
                contador++;
            }
            if (obj.cue_movimiento > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cue_movimiento = {" + contador + "} ";
                valores.Add(obj.cue_movimiento);
                contador++;
            }
            if (obj.cue_modulo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cue_modulo = {" + contador + "} ";
                valores.Add(obj.cue_modulo);
                contador++;
            }
            parametros.valores = valores.ToArray();
        }
    }
}