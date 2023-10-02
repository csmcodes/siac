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

namespace WebUI
{
    public partial class wfValoresSocios : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = " apellidossocio, nombressocio";
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
        public static string GetCabecera(object objeto)
        {
            Usuarioxempresa uxe = new Usuarioxempresa(objeto);
            uxe.uxe_empresa_key = uxe.uxe_empresa;
            uxe.uxe_usuario_key = uxe.uxe_usuario;
            uxe = UsuarioxempresaBLL.GetByPK(uxe);


            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span4\">");
            
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }

        [WebMethod]
        public static string GetPuntoVenta(object objeto)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object id = null;
            object empresa = null;
            object almacen = null;
            object usuario = null;
            tmp.TryGetValue("id", out id);
            tmp.TryGetValue("empresa", out empresa);
            tmp.TryGetValue("almacen", out almacen);
            tmp.TryGetValue("usuario", out usuario);
            if (!string.IsNullOrEmpty((string)almacen))
            {
                Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = int.Parse(empresa.ToString()), uxe_empresa_key = int.Parse(empresa.ToString()), uxe_usuario = usuario.ToString(), uxe_usuario_key = usuario.ToString() });

                //return new Select { id = "cmbPVENTA_P", diccionario = Dictionaries.GetPuntoVenta(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())),clase = Css.large, valor= uxe.uxe_puntoventa }.ToString();
                return new Select { id = id.ToString(), diccionario = Dictionaries.GetPuntoVenta(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())), clase = Css.large, valor = uxe.uxe_puntoventa, withempty = true }.ToString();
            }
            return new Select { id = id.ToString(), diccionario = Dictionaries.Empty(), clase = Css.large }.ToString();

        }

        public static List<vValoresSocios> GetValoresSocios(List<vCancelacion> lst)
        {
            List<vValoresSocios> lista = new List<vValoresSocios>();

            int cont = 0;
            decimal valor = 0;
            string docs = "";
            foreach (vCancelacion item in lst)
            {
                vValoresSocios reg = lista.Find(delegate(vValoresSocios v) { return v.codigo == item.socio; });
                if (item.socio == null)
                {
                    cont++;
                    valor += item.dca_monto_pla.HasValue ? item.dca_monto_pla.Value : 0;
                    docs += item.ddo_doctran + ";" + item.doctran_guia + ";" + item.doctran_can + ";" + item.dca_monto_pla + Environment.NewLine;
                }
                if (reg != null)
                {
                    //reg.monto +=  (item.dca_monto_pla.HasValue)? Math.Round(item.dca_monto_pla.Value,2):0;
                    reg.monto += (item.dca_monto_pla.HasValue) ? item.dca_monto_pla.Value : 0;
                    //reg.cancelado += item.ddo_cancela.Value;
                    //reg.saldo = reg.monto - reg.cancelado;
                }
                else
                {
                    reg = new vValoresSocios();
                    reg.codigo = item.socio;
                    reg.id = item.idsocio;                    
                    reg.socio = (item.socio.HasValue)? item.apellidossocio + " " + item.nombressocio:"***SIN ASIGNAR***";
                    //reg.monto = (item.dca_monto_pla.HasValue) ? Math.Round(item.dca_monto_pla.Value,2) : 0;
                    //reg.monto = (item.dca_monto_pla.HasValue) ? decimal.Parse(string.Format("{0:0.00}", item.dca_monto_pla)) : 0;

                    reg.monto = (item.dca_monto_pla.HasValue) ? item.dca_monto_pla.Value : 0;

                    //reg.cancelado = item.ddo_cancela.Value;
                    //reg.saldo = reg.monto - reg.cancelado;
                    lista.Add(reg);
                }



            }


            return lista;
        }


        public static List<vValoresSocios> GetValoresSociosVehiculo(List<vCancelacion> lst)
        {
            List<vValoresSocios> lista = new List<vValoresSocios>();

            int cont = 0;
            decimal valor = 0;
            string docs = "";
            foreach (vCancelacion item in lst)
            {
                vValoresSocios reg = new vValoresSocios();

                if (item.vehiculo.HasValue)
                    reg = lista.Find(delegate (vValoresSocios v) {return v.codigo == item.socio && v.vehiculo == item.vehiculo; });
                else
                    reg = lista.Find(delegate (vValoresSocios v) { return v.codigo == item.socio && v.vehiculo == null; });
                
                if (item.socio == null)
                {
                    cont++;
                    valor += item.dca_monto_pla.HasValue ? item.dca_monto_pla.Value : 0;
                    docs += item.ddo_doctran + ";" + item.doctran_guia + ";" + item.doctran_can + ";" + item.dca_monto_pla + Environment.NewLine;
                }
                if (reg != null)
                {
                    //reg.monto +=  (item.dca_monto_pla.HasValue)? Math.Round(item.dca_monto_pla.Value,2):0;
                    reg.monto += (item.dca_monto_pla.HasValue) ? item.dca_monto_pla.Value : 0;
                    //reg.cancelado += item.ddo_cancela.Value;
                    //reg.saldo = reg.monto - reg.cancelado;
                }
                else
                {
                    reg = new vValoresSocios();
                    reg.codigo = item.socio;
                    reg.id = item.idsocio;
                    reg.socio = (item.socio.HasValue) ? item.apellidossocio + " " + item.nombressocio : "***SIN ASIGNAR***";

                    reg.vehiculo = item.vehiculo;
                    reg.disco = (item.vehiculo.HasValue) ? item.disco : "S/V";
                    //reg.monto = (item.dca_monto_pla.HasValue) ? Math.Round(item.dca_monto_pla.Value,2) : 0;
                    //reg.monto = (item.dca_monto_pla.HasValue) ? decimal.Parse(string.Format("{0:0.00}", item.dca_monto_pla)) : 0;

                    reg.monto = (item.dca_monto_pla.HasValue) ? item.dca_monto_pla.Value : 0;

                    //reg.cancelado = item.ddo_cancela.Value;
                    //reg.saldo = reg.monto - reg.cancelado;
                    lista.Add(reg);
                }



            }


            return lista;
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


            tdatos.AddColumn("", "", "");
            tdatos.AddColumn("Id", "", "");
            tdatos.AddColumn("Socio", "", "");
            tdatos.AddColumn("Valor", "", "", "");
            //tdatos.AddColumn("Monto", "", "", "");
            //tdatos.AddColumn("Cancelado", "", "", "");
            //tdatos.AddColumn("Saldo", "", "", "");            
            tdatos.editable = false;

            //List<vCancelacion> lst = vCancelacionBLL.GetAll(new WhereParams("ddo_cancela>0  AND tpa_transacc = 1  and f.com_tipodoc = 4 AND dca_planilla is null and f.com_codigo IN (select pco_comprobante_fac FROM planillacomprobante )"), OrderByClause);
            //List<vCancelacion> lst = vCancelacionBLL.GetAll(new WhereParams("ddo_cancela>0   and f.com_tipodoc = 4 AND dca_planilla is null and f.com_codigo IN (select pco_comprobante_fac FROM planillacomprobante )"), OrderByClause);
            List<vCancelacion> lst = vCancelacionBLL.GetAll(new WhereParams("dca_monto_pla>0   and f.com_tipodoc = 4 AND dca_planilla is null and (f.com_codigo IN (select pco_comprobante_fac FROM planillacomprobante ) or  f.com_codigo IN (select plc_comprobante  FROM planillacli ))"), OrderByClause);

            //List<vValoresSocios> l = GetValoresSocios(lst);
            List<vValoresSocios> l = GetValoresSociosVehiculo(lst);


            ArrayList socios = new ArrayList();

            foreach (vValoresSocios item in l)
            {
                if (!socios.Contains(item.codigo))
                    socios.Add(item.codigo);
            }


            foreach (int? socio in socios)
            {
                HtmlRow row = new HtmlRow();
                string opc = "";
                string id = "";
                string nombres = "";
                decimal monto = 0;

                if (socio.HasValue)
                {
                    List<vValoresSocios> ls = l.FindAll(delegate (vValoresSocios v) { return v.codigo == socio; });

                    id = ls[0].id;
                    nombres = ls[0].socio;
                    monto = ls.Sum(s => s.monto);

                    opc = "<div class=\"btn-group\"> " +
                        "<button data-toggle=\"dropdown\" class=\"btn btn-inverse dropdown-toggle\"><span class=\"caret\"></span></button> " +
                        "<ul class=\"dropdown-menu\">" +
                        "<li><a href=\"#\" onclick=\"Planillar('" + socio + "')\">Planillar <b>TOTAL</b> --> $" + Formatos.CurrencyFormat(monto)+"</a></li>";

                    foreach (vValoresSocios v in ls)
                    {
                        if (v.vehiculo.HasValue)
                            opc += "<li><a href=\"#\" onclick=\"Planillar('" + socio + "'," + v.vehiculo + ")\">Planillar <b>Disco " + v.disco + "</b> --> $" + Formatos.CurrencyFormat(v.monto) + "</a></li>";
                        else
                            opc += "<li><a href=\"#\" onclick=\"Planillar('" + socio + "','')\">Planillar Sin Vehiculo --> $" + Formatos.CurrencyFormat(v.monto) + "</a></li>";

                    }

                    opc += "<li><a href=\"#\" onclick=\"ViewValores('" + socio + "')\">Ver valores</a></li>" +
                           "</ul></div> ";


                  


                }
                else
                {

                    List<vValoresSocios> ls = l.FindAll(delegate (vValoresSocios v) { return v.codigo == socio; });

                    id = ls[0].id;
                    nombres = ls[0].socio;
                    monto = ls.Sum(s => s.monto);

                    opc = "<div class=\"btn-group\"> " +
                         "<button data-toggle=\"dropdown\" class=\"btn btn-inverse dropdown-toggle\"><span class=\"caret\"></span></button> " +
                         "<ul class=\"dropdown-menu\">" +
                         "<li><a href=\"#\" onclick=\"ViewValores(null)\">Ver valores</a></li>" +
                          "</ul></div> ";
                }
                row.cells.Add(new HtmlCell { valor = opc });
                row.cells.Add(new HtmlCell { valor = id });
                row.cells.Add(new HtmlCell { valor = nombres });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(monto), clase = Css.center });
                //row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.cancelado), clase = Css.right });
                //row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.saldo), clase = Css.right });                
                tdatos.AddRow(row);
            }



            
            /*
            foreach (vValoresSocios item in l)
            {

                if (item.codigo!= socio)
                {
                    socio = item.codigo;

                }

                if (socio == -1)
                    socio = item.codigo;







                HtmlRow row = new HtmlRow();

                string opc = "";
                if (item.codigo.HasValue)
                {
                    List<vValoresSocios> lvs = lv.FindAll(delegate (vValoresSocios v) { return v.codigo == item.codigo; });


                    opc = "<div class=\"btn-group\"> " +
                           "<button data-toggle=\"dropdown\" class=\"btn btn-inverse dropdown-toggle\"><span class=\"caret\"></span></button> " +
                           "<ul class=\"dropdown-menu\">" +
                           "<li><a href=\"#\" onclick=\"Planillar('" + item.codigo + "')\">Planillar Total</a></li>";

                    foreach (vValoresSocios v in lvs)
                    {
                        if (v.vehiculo.HasValue)
                            opc += "<li><a href=\"#\" onclick=\"Planillar('" + item.codigo + "'," + v.vehiculo + ")\">Planillar Disco " + v.disco + "</a></li>";
                        else
                            opc += "<li><a href=\"#\" onclick=\"Planillar('" + item.codigo + "','')\">Planillar sin vehiculo</a></li>";

                    }

                    opc +=  "<li><a href=\"#\" onclick=\"ViewValores('" + item.codigo + "')\">Ver valores</a></li>" +
                           "</ul></div> ";
                }
                else
                {
                    opc = "<div class=\"btn-group\"> " +
                          "<button data-toggle=\"dropdown\" class=\"btn btn-inverse dropdown-toggle\"><span class=\"caret\"></span></button> " +
                          "<ul class=\"dropdown-menu\">" +
                          "<li><a href=\"#\" onclick=\"ViewValores(null)\">Ver valores</a></li>" +
                           "</ul></div> ";
                }
                
                row.cells.Add(new HtmlCell { valor = opc});
                row.cells.Add(new HtmlCell { valor = item.id});
                row.cells.Add(new HtmlCell { valor = item.socio});
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.monto), clase = Css.center });
                //row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.cancelado), clase = Css.right });
                //row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.saldo), clase = Css.right });                
                tdatos.AddRow(row);  
                
            }*/
            html.AppendLine(tdatos.ToString());
            return html.ToString();
        }
        [WebMethod]
        public static string GetDetalleData(object objeto)
        {

            int tipodocfac = Constantes.cFactura.tpd_codigo;
            int tipodocgui = Constantes.cGuia.tpd_codigo;
            int tipodocpagban = Constantes.cPagoBan.tpd_codigo;



            SetWhereClause(new vComprobante(objeto), new Usuario(objeto), tipodocfac, tipodocgui);
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            List<vComprobante> lista = vComprobanteBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            //List<Comprobante> lista = ComprobanteBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            StringBuilder html = new StringBuilder();
            foreach (vComprobante item in lista)
            {



                HtmlRow row = new HtmlRow();
                row.data = "data-codigo='" + item.codigo + "' ";
                row.removable = false;
                //row.clickevent = "EditCan(this)";




                string opc = "<div class=\"btn-group\"> " +
                            "<button data-toggle=\"dropdown\" class=\"btn btn-inverse dropdown-toggle\"><span class=\"caret\"></span></button> " +
                            "<ul class=\"dropdown-menu\">";

                if (item.tipodoc == tipodocfac)
                {
                    if (item.monto > item.cancela)
                        opc += "<li><a href=\"#\" onclick=\"Cobrar('" + item.codigo + "')\">Cobrar</a></li>";//COBRAR
                }



                if (item.tipodoc == tipodocfac || item.tipodoc == tipodocgui)
                {
                    if (item.monto <= item.cancela)
                    {
                        bool despacho = true;
                        if (item.despachado.HasValue)
                            if (item.despachado.Value == 1)
                                despacho = false;
                        if (despacho)
                        {
                            opc += "<li><a href=\"#\" onclick=\"Despachar('" + item.codigo + "')\">Despachar</a></li>";//DESPACHAR
                        }
                    }
                }
                if (item.tipodoc == tipodocgui)
                {
                    opc += "<li><a href=\"#\" onclick=\"Modificar('" + item.codigo + "')\">Modificar</a></li>";//Modificacion de Guias                   
                }
                if (item.tipodoc == 15 || item.tipodoc == 19 || item.tipodoc == 20 || item.tipodoc == 21 || item.tipodoc == 22)
                {
                    opc += "<li><a href=\"#\" onclick=\"ModificarPago('" + item.codigo + "')\">Modificar Pago</a></li>";//Modificacion Pago
                }
                opc += "<li><a href=\"#\" onclick=\"Anular('" + item.codigo + "')\">Anular</a></li>";//ANULAR                
                opc += "</ul></div> ";

                if (item.estado != Constantes.cEstadoEliminado)
                    row.cells.Add(new HtmlCell { valor = opc });
                else
                {
                    opc = "<div class=\"btn-group\"> " +
                             "<button data-toggle=\"dropdown\" class=\"btn btn-inverse dropdown-toggle\"><span class=\"caret\"></span></button> " +
                             "<ul class=\"dropdown-menu\">" +
                                 "<li><a href=\"#\" onclick=\"Activar('" + item.codigo + "')\">Activar</a></li>" +
                             "</ul></div> ";
                    row.cells.Add(new HtmlCell { valor = opc });
                }

                row.cells.Add(new HtmlCell { valor = "<a href='#' onclick='CallFormulario(" + item.codigo + ");'>" + item.doctran + "</a><br><a href='#' onclick='ViewAuditoria(" + item.codigo + ")'>" + item.crea_usr + "</a>" });
                row.cells.Add(new HtmlCell { valor = item.fecha });

                row.cells.Add(new HtmlCell { valor = item.nombres });
                row.cells.Add(new HtmlCell { valor = (!string.IsNullOrEmpty(item.nombres_rem) ? "<b>REMITENTE: </b>" + item.nombres_rem : "") + (!string.IsNullOrEmpty(item.nombres_des) ? "<br><b>DESTINATARIO: </b>" + item.nombres_des : "") });
                row.cells.Add(new HtmlCell { valor = (!string.IsNullOrEmpty(item.nombres_soc) ? "<b>SOCIO: </b> " + item.nombres_soc : "") + (item.vehiculo.HasValue ? "<br><b>VEHICULO: </b> " + item.placa + " <b>DISCO: </b>" + item.disco : "") });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.total) });

                row.cells.Add(new HtmlCell { valor = item.politica + ((item.despachado.HasValue) ? ((item.despachado.Value == 1) ? "<br/>DESPACHADO" : "") : "") + "<br>" + Constantes.GetEstadoName(item.estado.Value) });

                html.AppendLine(row.ToString());
            }
            return html.ToString();
        }


        public static void SetWhereClause(vComprobante obj, Usuario usr, int tipodocfac, int tipodocgui)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (!string.IsNullOrEmpty(obj.crea_usr))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.crea_usr = {" + contador + "} ";
                valores.Add(obj.crea_usr);
                contador++;
            }

            if (obj.periodo.HasValue)
            {
                if (obj.periodo.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_periodo = {" + contador + "} ";
                    valores.Add(obj.periodo.Value);
                    contador++;
                }
            }
            if (obj.mes.HasValue)
            {
                if (obj.mes.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_mes = {" + contador + "} ";
                    valores.Add(obj.mes);
                    contador++;
                }
            }
            if (obj.ctipocom.HasValue)
            {
                if (obj.ctipocom.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_ctipocom = {" + contador + "} ";
                    valores.Add(obj.ctipocom);
                    contador++;
                }
            }
            if (obj.almacen.HasValue)
            {
                if (obj.almacen.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                    valores.Add(obj.almacen);
                    contador++;
                }
            }
            if (obj.pventa.HasValue)
            {
                if (obj.pventa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                    valores.Add(obj.pventa);
                    contador++;
                }
            }
            if (obj.numero.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_numero = {" + contador + "} ";
                valores.Add(obj.numero);
                contador++;
            }
            if (obj.estado.HasValue)
            {
                if (obj.estado.Value > -1)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
                    valores.Add(obj.estado);
                    contador++;
                }

            }
            if (obj.tipodoc.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc = {" + contador + "} ";
                valores.Add(obj.tipodoc);
                contador++;
            }


            if (!string.IsNullOrEmpty(obj.politica))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " pol_nombre LIKE {" + contador + "} ";
                valores.Add("%" + obj.politica + "%");
                contador++;
            }
            /*if (obj.com_aut_tipo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_aut_tipo = {" + contador + "} ";
                valores.Add(obj.com_aut_tipo);
                contador++;
            }
            if (obj.com_nocontable > -1)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_nocontable != {" + contador + "} ";
                valores.Add(obj.com_nocontable);
                contador++;
            }*/
            if (!string.IsNullOrEmpty(obj.concepto))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_concepto like {" + contador + "} ";
                valores.Add("%" + obj.concepto + "%");
                contador++;
            }
            /*if (obj.com_ref_comprobante > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_ref_comprobante = {" + contador + "} ";
                valores.Add(obj.com_ref_comprobante);
                contador++;
            }*/
            if (obj.fecha.Value > DateTime.MinValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
                valores.Add(obj.fecha);
                contador++;
                parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
                valores.Add(obj.fecha.Value.AddDays(1));
                contador++;
            }

            if (!string.IsNullOrEmpty(obj.nombres))//CLIENTE O PROVEEDOR
            {

                //parametros.where += ((parametros.where != "") ? " and " : "") + " (p.per_ciruc ILIKE {" + contador + "} or p.per_nombres ILIKE {" + contador + "} or p.per_apellidos ILIKE{" + contador + "}) ";
                parametros.where += ((parametros.where != "") ? " and " : "") + " (p.per_ciruc ILIKE {" + contador + "} or p.per_nombres ILIKE {" + contador + "} or p.per_apellidos ILIKE{" + contador + "} or e.cenv_ciruc_rem ILIKE {" + contador + "} or e.cenv_nombres_rem ILIKE {" + contador + "} or e.cenv_apellidos_rem ILIKE{" + contador + "} or e.cenv_ciruc_des ILIKE {" + contador + "} or e.cenv_nombres_des ILIKE {" + contador + "} or e.cenv_apellidos_des ILIKE{" + contador + "} or p1.per_apellidos ILIKE {" + contador + "} or p1.per_nombres ILIKE{" + contador + "}) ";
                valores.Add("%" + obj.nombres + "%");
                contador++;
            }

            //if (obj.total.HasValue)
            //{                
            //    parametros.where += ((parametros.where != "") ? " and " : "") + " tot_total "+obj.operador+" {" + contador + "} ";
            //    valores.Add(obj.total);
            //    contador++;
            //}

            if (!string.IsNullOrEmpty(obj.estadoenvio))//ESTADO ENVIO
            {
                if (obj.estadoenvio == "1") //POR COBRAR
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " SUM(ddo_monto) > SUM(ddo_cancela) and c.com_tipodoc= " + tipodocfac;
                }
                if (obj.estadoenvio == "2") //POR DESPACHAR
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " SUM(ddo_monto) = SUM(ddo_cancela) and e.cenv_despachado_ret IS NULL";
                }
                if (obj.estadoenvio == "3")//DESPACHADOS
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " e.cenv_despachado_ret = 1 ";
                }
            }

            if (!string.IsNullOrEmpty(obj.nombres_rem)) //REMITENTE
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (e.cenv_ciruc_rem ILIKE {" + contador + "} or e.cenv_nombres_rem ILIKE {" + contador + "} or e.cenv_apellidos_rem ILIKE{" + contador + "}) ";
                valores.Add("%" + obj.nombres_rem + "%");
                contador++;
            }

            if (!string.IsNullOrEmpty(obj.nombres_des)) //DESTINATARIO
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (e.cenv_ciruc_des ILIKE {" + contador + "} or e.cenv_nombres_des ILIKE {" + contador + "} or e.cenv_apellidos_des ILIKE{" + contador + "}) ";
                valores.Add("%" + obj.nombres_des + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.nombres_soc)) //SOCIO
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (p1.per_apellidos ILIKE {" + contador + "} or p1.per_nombres ILIKE{" + contador + "}) ";
                valores.Add("%" + obj.nombres_soc + "%");
                contador++;
            }

            if (!string.IsNullOrEmpty(obj.placa)) //VEHICULO
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (e.cenv_placa ILIKE {" + contador + "} or e.cenv_disco ILIKE {" + contador + "}) ";
                valores.Add("%" + obj.placa + "%");
                contador++;
            }

            if (!string.IsNullOrEmpty(usr.usr_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc IN (SELECT udo_tipodoc FROM usrdoc WHERE udo_usuario = {" + contador + "}) ";
                valores.Add(usr.usr_id);
                contador++;
            }

            string wheredocs = "";
            foreach (object item in obj.tipos)
            {
                if (item.ToString() != "")
                {
                    wheredocs += ((wheredocs != "") ? " or " : "") + " c.com_tipodoc = " + item.ToString();
                }
            }
            parametros.where += (wheredocs != "") ? " and (" + wheredocs + ")" : "";

            parametros.valores = valores.ToArray();
        }
    }
}