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
    public partial class wfEstadoCuenta : System.Web.UI.Page
    {    
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "";
        protected static string WhereClause = "";
        protected static WhereParams parametros = new WhereParams();
        protected static int? debcre;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtdebcre.Text = (Request.QueryString["debcre"] != null) ? Request.QueryString["debcre"].ToString() : "-1";
                txtcodigoper.Text = Request.QueryString["codigo"];
                debcre = Convert.ToInt32(txtdebcre.Text);
            }
        }

       
        [WebMethod]
        public static string GetCabecera(object objeto)
        {

            Persona per = new Persona(objeto);
            per.per_empresa_key = per.per_empresa;
            per.per_codigo_key = per.per_codigo;
            per = PersonaBLL.GetByPK(per);


             
                     
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span4\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Almacén:";
            tdatos.rows[0].cells[1].valor = new Select { id = "cmbALMACEN", clase = Css.large, withempty = true, diccionario = Dictionaries.GetAlmacen() }.ToString();
            if (debcre == 1)
            {
                tdatos.rows[1].cells[0].valor = "Cliente:";
                tdatos.rows[1].cells[1].valor = new Input { id = "cmbNOMBRE", clase = Css.large, autocomplete = "GetClienteObj" }.ToString() + "" + new Input { id = "txtCODPROVEE", visible = false }.ToString(); ;
            }
            else
            {
                tdatos.rows[1].cells[0].valor = "Provedor:";
                tdatos.rows[1].cells[1].valor = new Input { id = "cmbNOMBRE", clase = Css.large, autocomplete = "GetProveedorObj" }.ToString() + "" + new Input { id = "txtCODPROVEE", visible = false }.ToString(); ;
            }
            tdatos.rows[2].cells[0].valor = "Ordenado por:";
            tdatos.rows[2].cells[1].valor = new Select { id = "cmbORDEN",clase = Css.large, withempty=false, diccionario = Dictionaries.GetEstadoCuentaOrden() }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span4-->");

            html.AppendLine("<div class=\"span4\">");
            tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(2, 2);

            DateTime hasta = DateTime.Now;
            DateTime desde = new DateTime(hasta.Year, hasta.Month, 1); 


            tdatos.rows[0].cells[0].valor = "Fecha inicio:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtINICIO", datepicker = true, datetimevalor = desde, clase = Css.large }.ToString();
            tdatos.rows[1].cells[0].valor = "Fecha fin:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtFIN", datepicker = true, datetimevalor = hasta, clase = Css.large }.ToString();

            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span4-->");

            html.AppendLine("<div class=\"span4\">");
            tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(2, 2);

            tdatos.rows[0].cells[0].valor = "Saldo Inicial:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtSALDOINI", clase = Css.medium, placeholder = "Saldo inicial", habilitado = false }.ToString();
            tdatos.rows[1].cells[0].valor = "Saldo Final:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtSALDOFIN", clase=Css.medium , habilitado=false, placeholder="Saldo Final"}.ToString();

            
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span4-->");




            html.AppendLine("</div><!--row-fluid-->");
           return html.ToString(); 
        }
        

        [WebMethod]
        public static string GetDetalle(object objeto)
        {

            Persona per = new Persona(objeto);
            Almacen alm = new Almacen(objeto);
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object inicio = null;
            object fin = null;
            object orden = null;
            tmp.TryGetValue("inicio", out inicio);
            tmp.TryGetValue("fin", out fin);
            tmp.TryGetValue("orden", out orden);

            string ordenestado = (string)Conversiones.GetValueByType(orden, typeof(string));


            DateTime? fechainicio = (DateTime?)Conversiones.GetValueByType(inicio, typeof(DateTime?));
            DateTime? fechafin= (DateTime?)Conversiones.GetValueByType(fin, typeof(DateTime?));
            if (fechafin.HasValue)
                fechafin = fechafin.Value.AddDays(1).AddSeconds(-1);


            string parctas = "";
            if (debcre == 1)//Clientes
                parctas = Constantes.GetParameter("ctasclientes");
            else//Proveedores
                parctas = Constantes.GetParameter("ctasproveedores");

            


            decimal saldoinicial = 0;
            decimal saldofinal= 0;

            //Auto.actualiza_documentos(per.per_empresa, null, null,per.per_codigo, null, null, null, 0);

            List<vEstadoCuenta> lstestado = Packages.General.EstadoCuentaDetalle(fechainicio, fechafin, per.per_empresa, alm.alm_codigo, null, per.per_codigo, parctas, debcre.Value, ordenestado, ref saldoinicial, ref saldofinal);

            StringBuilder html = new StringBuilder();

            html.Append("<table class='table table-bordered table-invoice-full scrolltable'>");
            html.Append("<thead>");
            html.Append("<tr>");
            html.Append("<th>FECHA</th>");
            html.Append("<th>DOCUMENTO</th>");
            html.Append("<th>AFECTA A</th>");
            html.Append("<th>VENCIMIENTO</th>");
            html.Append("<th>DEBITO</th>");
            html.Append("<th>CREDITO</th>");
            html.Append("<th>SALDO</th>");
            html.Append("<th>TOTAL</th>");
            html.Append("</tr>");
            html.Append("</thead>");
            html.Append("<tbody>");


            foreach (vEstadoCuenta item in lstestado)
            {


                html.AppendFormat("<tr class='{0}'>", item.ddo_fecha_ven.HasValue ? "rowoscura" : "");
                html.AppendFormat("<td>{0}</td>", item.com_fecha);
                html.AppendFormat("<td>{0}</td>", item.com_doctran);
                html.AppendFormat("<td>{0}</td>", item.ddo_doctran);
                html.AppendFormat("<td>{0}</td>", item.ddo_fecha_ven);
                html.AppendFormat("<td>{0}</td>", item.valordebito);
                html.AppendFormat("<td>{0}</td>", item.valorcredito);
                html.AppendFormat("<td>{0}</td>", item.valorsaldo);
                html.AppendFormat("<td>{0}</td>", item.valortotal);
                html.Append("</tr>");


            }








            /*
            WhereParams parsumdoc = new WhereParams();
            List<object> valsumdoc = new List<object>();
            WhereParams parsumcan = new WhereParams();
            List<object> valsumcan = new List<object>();

            WhereParams pardoc = new WhereParams();
            List<object> valdoc = new List<object>();
            WhereParams parcan = new WhereParams();
            List<object> valcan = new List<object>();

            parsumdoc.where = "com_empresa = " + per.per_empresa + " and com_estado= " + Constantes.cEstadoMayorizado + " and per_codigo=" + per.per_codigo + " and ddo_cuenta in ("+parctas+") ";
            parsumcan.where = "cc.com_empresa = " + per.per_empresa + " and cc.com_estado= " + Constantes.cEstadoMayorizado + " and per_codigo=" + per.per_codigo + " and ddo_cuenta in (" + parctas + ") ";

            pardoc.where = "com_empresa = " + per.per_empresa + " and com_estado= " + Constantes.cEstadoMayorizado + " and per_codigo=" + per.per_codigo + " and ddo_cuenta in (" + parctas + ") ";
            parcan.where = "cc.com_empresa = " + per.per_empresa + " and cc.com_estado= " + Constantes.cEstadoMayorizado + " and per_codigo=" + per.per_codigo + " and ddo_cuenta in (" + parctas + ") ";

            
            
                if (alm.alm_codigo>0)
            {
                parsumdoc.where += " and com_almacen = {" + valsumdoc.Count + "} ";
                valsumdoc.Add(alm.alm_codigo);
                parsumcan.where += " and cd.com_almacen = {" + valsumcan.Count + "} ";
                valsumcan.Add(alm.alm_codigo);

                pardoc.where += " and com_almacen = {" + valdoc.Count + "} ";
                valdoc.Add(alm.alm_codigo);
                parcan.where += " and cd.com_almacen = {" + valcan.Count + "} ";
                valcan.Add(alm.alm_codigo);

            }
            parsumdoc.where += " and com_fecha<{" + valsumdoc.Count + "} ";
            valsumdoc.Add(fechainicio);
            parsumcan.where += " and cc.com_fecha<{" + valsumcan.Count + "} ";
            valsumcan.Add(fechainicio);

            pardoc.where += " and com_fecha between {" + valdoc.Count + "} and {" + (valdoc.Count + 1) + "}";
            valdoc.Add(fechainicio);
            valdoc.Add(fechafin);

            parcan.where += " and cc.com_fecha between {" + valcan.Count + "} and {" + (valcan.Count + 1) + "}";
            valcan.Add(fechainicio);
            valcan.Add(fechafin);

            parsumdoc.valores = valsumdoc.ToArray();
            parsumcan.valores = valsumcan.ToArray();

            pardoc.valores = valdoc.ToArray();
            parcan.valores = valcan.ToArray();


            List<vEstadoCuenta> lstsumdoc = vEstadoCuentaBLL.GetAllSumDoc(parsumdoc, "");
            List<vEstadoCuenta> lstsumcan = vEstadoCuentaBLL.GetAllSumCan(parsumcan, "");

            List<vEstadoCuenta> lstdoc = vEstadoCuentaBLL.GetAllDoc1(pardoc, "com_fecha");
            List<vEstadoCuenta> lstcan = vEstadoCuentaBLL.GetAllCan1(parcan, "cc.com_fecha");

            List<vEstadoCuenta> lstall = new List<vEstadoCuenta>();
            lstall.AddRange(lstdoc);
            lstall.AddRange(lstcan);

            lstall.Sort(delegate (vEstadoCuenta x, vEstadoCuenta y)
            {
                return x.com_fecha.Value.CompareTo(y.com_fecha.Value);
            });


            decimal saldoinicial = 0;
            if (debcre == 1)
            {
                saldoinicial += lstsumdoc.Sum(s => s.valordebito ?? 0);
                saldoinicial -= lstsumcan.Sum(s => s.valorcredito ?? 0);
            }
            else

            {
                if (lstsumdoc.Count > 0)
                    saldoinicial += lstsumdoc[0].valorcredito.Value;
                if (lstsumcan.Count > 0)
                    saldoinicial -= lstsumcan[0].valordebito.Value;
            }

            decimal saldofinal = saldoinicial;
            saldofinal += lstdoc.Sum(s => s.ddo_monto ?? 0);
            saldofinal -= lstcan.Sum(s => s.dca_monto ?? 0);

            decimal total = saldoinicial;

            StringBuilder html = new StringBuilder();

            html.Append("<table class='table table-bordered table-invoice-full scrolltable'>");
            html.Append("<thead>");
            html.Append("<tr>");
            html.Append("<th>FECHA</th>");
            html.Append("<th>DOCUMENTO</th>");
            html.Append("<th>AFECTA A</th>");
            html.Append("<th>VENCIMIENTO</th>");
            html.Append("<th>DEBITO</th>");
            html.Append("<th>CREDITO</th>");
            html.Append("<th>SALDO</th>");
            html.Append("<th>TOTAL</th>");
            html.Append("</tr>");
            html.Append("</thead>");
            html.Append("<tbody>");

            if (string.IsNullOrEmpty(ordenestado) || ordenestado == "1")
            {

                foreach (vEstadoCuenta item in lstall)
                {
                    decimal debito = 0;
                    decimal credito = 0;
                    decimal saldo = 0;
                    if (item.ddo_comprobante.HasValue)
                    {
                        debito = item.ddo_debcre == Constantes.cDebito ? item.ddo_monto.Value : 0;
                        credito = item.ddo_debcre == Constantes.cCredito ? item.ddo_monto.Value : 0;
                        saldo = debito - credito;
                        total += item.ddo_monto ?? 0;

                        html.Append("<tr class='rowoscura'>");
                        html.AppendFormat("<td>{0}</td>", item.com_fecha);
                        html.AppendFormat("<td>{0}</td>", item.com_doctran);
                        html.AppendFormat("<td>{0}</td>", "");
                        html.AppendFormat("<td>{0}</td>", item.ddo_fecha_ven);
                        html.AppendFormat("<td>{0}</td>", debito);
                        html.AppendFormat("<td>{0}</td>", credito);
                        html.AppendFormat("<td>{0}</td>", saldo);
                        html.AppendFormat("<td>{0}</td>", total);
                        html.Append("</tr>");
                    }
                    else
                    {
                        debito = item.dca_debcre == Constantes.cDebito ? item.dca_monto.Value : 0;
                        credito = item.dca_debcre == Constantes.cCredito ? item.dca_monto.Value : 0;
                        saldo = debito - credito;
                        total -= item.dca_monto ?? 0;

                        html.Append("<tr class=''>");
                        html.AppendFormat("<td>{0}</td>", item.com_fecha);
                        html.AppendFormat("<td>{0}</td>", item.com_doctran);
                        html.AppendFormat("<td>{0}</td>", item.ddo_doctran);
                        html.AppendFormat("<td>{0}</td>", "");
                        html.AppendFormat("<td>{0}</td>", debito);
                        html.AppendFormat("<td>{0}</td>", credito);
                        html.AppendFormat("<td>{0}</td>", saldo);
                        html.AppendFormat("<td>{0}</td>", total);
                        html.Append("</tr>");


                    }

                }
            }
            if (ordenestado == "2")
            {

                foreach (vEstadoCuenta item in lstdoc)
                {
                    decimal debito = item.ddo_debcre == Constantes.cDebito ? item.ddo_monto.Value : 0;
                    decimal credito = item.ddo_debcre == Constantes.cCredito ? item.ddo_monto.Value : 0;
                    decimal saldo = debito - credito;
                    total += item.ddo_monto ?? 0;

                    List<vEstadoCuenta> lstc = lstcan.FindAll(delegate (vEstadoCuenta v) { return v.dca_comprobante == item.ddo_comprobante && v.dca_transacc == item.ddo_transacc && v.dca_doctran == item.ddo_doctran && v.dca_pago == item.ddo_pago; });

                    html.Append("<tr class='rowoscura'>");
                    html.AppendFormat("<td>{0}</td>", item.com_fecha);
                    html.AppendFormat("<td>{0}</td>", item.com_doctran);
                    html.AppendFormat("<td>{0}</td>", "");
                    html.AppendFormat("<td>{0}</td>", item.ddo_fecha_ven);
                    html.AppendFormat("<td>{0}</td>", debito);
                    html.AppendFormat("<td>{0}</td>", credito);
                    html.AppendFormat("<td>{0}</td>", saldo);
                    html.AppendFormat("<td>{0}</td>", total);
                    html.Append("</tr>");

                    //vEstadoCuenta vec = lstall.Find(delegate (vEstadoCuenta v) { return v.ddo_empresa == item.ddo_empresa && v.ddo_comprobante == item.ddo_comprobante && v.ddo_transacc == item.ddo_transacc && v.ddo_doctran == item.ddo_doctran && v.ddo_pago == item.ddo_pago; });
                    vEstadoCuenta vec = lstall.Find(delegate (vEstadoCuenta v) { return v.ddo_empresa == item.ddo_empresa && v.ddo_comprobante == item.ddo_comprobante && v.ddo_transacc == item.ddo_transacc && v.ddo_doctran == item.ddo_doctran; });

                    lstall.Remove(vec);


                    foreach (vEstadoCuenta dca in lstc)
                    {
                        decimal dcadebito = dca.dca_debcre == Constantes.cDebito ? dca.dca_monto.Value : 0;
                        decimal dcacredito = dca.dca_debcre == Constantes.cCredito ? dca.dca_monto.Value : 0;
                        //decimal dcasaldo = dcadebito - dcacredito;
                        saldo += dcadebito - dcacredito;

                        total -= dca.dca_monto ?? 0;

                        html.Append("<tr class=''>");
                        html.AppendFormat("<td>{0}</td>", dca.com_fecha);
                        html.AppendFormat("<td>{0}</td>", dca.com_doctran);
                        html.AppendFormat("<td>{0}</td>", dca.ddo_doctran);
                        html.AppendFormat("<td>{0}</td>", "");
                        html.AppendFormat("<td>{0}</td>", dcadebito);
                        html.AppendFormat("<td>{0}</td>", dcacredito);
                        html.AppendFormat("<td>{0}</td>", saldo);
                        html.AppendFormat("<td>{0}</td>", total);
                        html.Append("</tr>");

                        //vec = lstall.Find(delegate (vEstadoCuenta v) { return v.dca_empresa == dca.dca_empresa && v.dca_comprobante == dca.dca_comprobante && v.dca_transacc == dca.dca_transacc && v.dca_doctran == dca.dca_doctran && v.dca_pago == dca.dca_pago && v.dca_comprobante_can == dca.dca_comprobante_can && v.dca_secuencia == dca.dca_secuencia; });
                        vec = lstall.Find(delegate (vEstadoCuenta v) { return v.dca_empresa == dca.dca_empresa && v.dca_comprobante == dca.dca_comprobante && v.dca_transacc == dca.dca_transacc && v.dca_doctran == dca.dca_doctran && v.dca_comprobante_can == dca.dca_comprobante_can; });
                        lstall.Remove(vec);
                    }
                }

                if (lstall.Count>0)
                {
                    html.Append("<tr class='rowoscura'>");
                    html.Append("<td colspan='8'>CON COMPROBANTE FUERA DEL RANGO DE FECHAS</td>");
                    html.Append("</tr>");
                    foreach (vEstadoCuenta dca in lstall)
                    {
                        decimal dcadebito = dca.dca_debcre == Constantes.cDebito ? dca.dca_monto.Value : 0;
                        decimal dcacredito = dca.dca_debcre == Constantes.cCredito ? dca.dca_monto.Value : 0;
                        decimal dcasaldo = dcadebito - dcacredito;

                        total -= dca.dca_monto ?? 0;

                        html.Append("<tr class=''>");
                        html.AppendFormat("<td>{0}</td>", dca.com_fecha);
                        html.AppendFormat("<td>{0}</td>", dca.com_doctran);
                        html.AppendFormat("<td>{0}</td>", dca.ddo_doctran);
                        html.AppendFormat("<td>{0}</td>", "");
                        html.AppendFormat("<td>{0}</td>", dcadebito);
                        html.AppendFormat("<td>{0}</td>", dcacredito);
                        html.AppendFormat("<td>{0}</td>", dcasaldo);
                        html.AppendFormat("<td>{0}</td>", total);
                        html.Append("</tr>");
                    }
                }

            }*/
            html.Append("</tbody>");
            html.Append("</table>");
            string[] retorno = new string[4];
            retorno[0] = saldoinicial.ToString("0.00");
            retorno[1] = saldofinal.ToString("0.00");
            retorno[2] = html.ToString();
            return new JavaScriptSerializer().Serialize(retorno);


           



        }

   

        public static void SetWhereClause(object obj)
        {
            if (obj != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)obj;
                object obj_per_codigo = null;
                object obj_com_almacen = null;
                object obj_ddo_fecha_emi = null;
                object obj_ddo_debcre = null;


                tmp.TryGetValue("per_codigo", out obj_per_codigo);
                tmp.TryGetValue("com_almacen", out obj_com_almacen);
                tmp.TryGetValue("ddo_fecha_emi", out obj_ddo_fecha_emi);
                tmp.TryGetValue("ddo_debcre", out obj_ddo_debcre);
                


                int? per_codigo = Convert.ToInt32(obj_per_codigo);
                int? com_almacen = Convert.ToInt32(obj_com_almacen);
                DateTime? ddo_fecha_emi = Convert.ToDateTime(obj_ddo_fecha_emi);
                int? ddo_debcre = Convert.ToInt32(obj_ddo_debcre);



                int contador = 0;
                parametros = new WhereParams();
                List<object> valores = new List<object>();
                if (per_codigo > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " per_codigo = {" + contador + "} ";
                    valores.Add(per_codigo);
                    contador++;
                }
                if (com_almacen > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_almacen = {" + contador + "} ";
                    valores.Add(com_almacen);
                    contador++;
                }
                if (ddo_fecha_emi.HasValue)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " ddo_fecha_emi <= {" + contador + "} ";
                    valores.Add(ddo_fecha_emi);
                    contador++;
                }
                if (ddo_debcre.HasValue)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " ddo_debcre = {" + contador + "} ";
                    valores.Add(ddo_debcre);
                    contador++;
                }




               


                parametros.valores = valores.ToArray();
            }

        }
    
        /*
        public static vEstadoCuenta GetObjeto(object objeto)
        {
            vEstadoCuenta obj = new vEstadoCuenta();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object per_codigo = null;
                object per_nombres = null;
                object per_apellidos = null;
                object per_cupo = null;
                object valor = null;

                tmp.TryGetValue("per_codigo", out per_codigo);
                tmp.TryGetValue("per_nombres", out per_nombres);
                tmp.TryGetValue("per_apellidos", out per_apellidos);
                tmp.TryGetValue("per_cupo", out per_cupo);
                tmp.TryGetValue("valor", out valor);


                obj.per_codigo = Convert.ToInt32(per_codigo);
                obj.per_nombres = Convert.ToInt32(per_nombres)+"";
                obj.per_apellidos = (string)per_apellidos;
                obj.per_cupo = Convert.ToDecimal(per_cupo);
                obj.valor = Convert.ToDecimal(valor);
                

            }

            return obj;
        }
    */
    }
}