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
    public partial class wfListaComprobantes : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "t.com_fecha DESC";
        protected static string WhereClause = "";
        //protected static WhereParams parametros;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 100;
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
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(4, 2);
            tdatos.rows[0].cells[0].valor = "Usuario:";
            tdatos.rows[0].cells[1].valor = new Select { id = "cmbUSUARIO", clase = Css.large, diccionario = Dictionaries.GetUsuario(), withempty = true }.ToString();
            //tdatos.rows[0].cells[1].valor = new Select { id = "cmbUSUARIO", clase = Css.large, diccionario = Dictionaries.GetUsuario(), withempty = true, valor= uxe.uxe_usuario }.ToString();
            //tdatos.rows[0].cells[0].valor = "Periodo";
            //tdatos.rows[0].cells[1].valor = ;
            tdatos.rows[1].cells[0].valor = "Periodo/Mes:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtPERIODO", placeholder = "Periodo", clase = Css.mini }.ToString()+" "+new Input { id = "txtMES", placeholder = "Mes", clase = Css.small }.ToString();
            tdatos.rows[2].cells[0].valor = "Fecha";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtFECHA", placeholder = "Fecha", clase = Css.small, datepicker = true}.ToString();
            tdatos.rows[3].cells[0].valor = "Tipo:";
            tdatos.rows[3].cells[1].valor = new Select { id = "cmbTIPODOC", clase = Css.large, diccionario = Dictionaries.GetUsrdoc(uxe.uxe_usuario), multiselect = true, data="data-placeholder=\"Seleccione Tipo...\"", withempty = true }.ToString();            
            //tdatos.rows[4].cells[0].valor = "";
            //tdatos.rows[4].cells[1].valor = "";
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span4-->");
            html.AppendLine("<div class=\"span4\">");
            HtmlTable tdtra = new HtmlTable();
            tdtra.CreteEmptyTable(4, 2);
            tdtra.rows[0].cells[0].valor = "Almacen:";
            //tdtra.rows[0].cells[1].valor = new Select { id = "cmbALMACEN", diccionario = Dictionaries.GetIDAlmacen(), valor = uxe.uxe_almacen, clase = Css.large, withempty=true }.ToString();
            tdtra.rows[0].cells[1].valor = new Select { id = "cmbALMACEN", diccionario = Dictionaries.GetIDAlmacen(),  clase = Css.large, withempty = true }.ToString();
            tdtra.rows[1].cells[0].valor = "Punto Venta:";
            tdtra.rows[1].cells[1].valor = new Select { id = "cmbPVENTA",diccionario = Dictionaries.Empty(), clase = Css.large }.ToString();
            tdtra.rows[2].cells[0].valor = "Numero:";
            tdtra.rows[2].cells[1].valor = new Input { id = "txtNUMERO", placeholder = "Numero", clase = Css.large }.ToString();
            tdtra.rows[3].cells[0].valor = "Estado:";
            tdtra.rows[3].cells[1].valor = new Select { id = "cmbESTADO", clase = Css.large, diccionario = Dictionaries.GetEstadoComprobante(), withempty = true }.ToString(); //new Select { id = "cmbALMACEN_S", clase = Css.large, diccionario = Dictionaries.GetAlmacen(), withempty = true }.ToString();            html.AppendLine(tdtra.ToString());
            html.AppendLine(tdtra.ToString());
            html.AppendLine(" </div><!--span4-->");
            html.AppendLine("<div class=\"span4\">");
            HtmlTable tdright = new HtmlTable();
            tdright.CreteEmptyTable(5, 2);
            tdright.rows[0].cells[0].valor = "Persona:";
            tdright.rows[0].cells[1].valor = new Input { id = "txtPERSONA", placeholder = "Código, CI/RUC, Nombres", clase = Css.large }.ToString();
            tdright.rows[1].cells[0].valor = "Vehiculo:";
            tdright.rows[1].cells[1].valor = new Input { id = "txtVEHICULO", placeholder = "Placa, Disco", clase = Css.large }.ToString();
            tdright.rows[2].cells[0].valor = "Estado Envío";
            tdright.rows[2].cells[1].valor = new Select { id = "cmbESTADOENVIO", clase = Css.large, withempty=true, diccionario = Dictionaries.GetEstadosEnvio()}.ToString();
            tdright.rows[3].cells[0].valor = "Politica:";
            tdright.rows[3].cells[1].valor = new Select { id = "cmbPOLITICA", clase = Css.large, diccionario = Dictionaries.GetPolitica(), withempty = true }.ToString();
            tdright.rows[4].cells[0].valor = "Detalle:";
            tdright.rows[4].cells[1].valor = new Input { id = "txtDETALLE", placeholder = "Items enviados", clase = Css.large }.ToString(); ;


            /*tdright.rows[0].cells[1].valor = new Input { id = "txtPERSONA", placeholder = "Código, CI/RUC, Nombres", clase = Css.large }.ToString();
            tdright.rows[1].cells[0].valor = "Remitente:";
            tdright.rows[1].cells[1].valor = new Input { id = "txtREMITENTE", placeholder = "Código, CI/RUC, Nombres", clase = Css.large }.ToString();
            tdright.rows[2].cells[0].valor = "Destinatario:";
            tdright.rows[2].cells[1].valor = new Input { id = "txtDESTINATARIO", placeholder = "Código, CI/RUC, Nombres", clase = Css.large }.ToString();
            tdright.rows[3].cells[0].valor = "Socio";
            tdright.rows[3].cells[1].valor = new Input { id = "txtSOCIO", placeholder = "Código,Nombres", clase = Css.large }.ToString();
            tdright.rows[4].cells[0].valor = "Vehiculo";
            tdright.rows[4].cells[1].valor = new Input { id = "txtVEHICULO", placeholder = "Placa, Disco", clase = Css.large }.ToString(); */
            html.AppendLine(tdright.ToString());
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
            if (!string.IsNullOrEmpty((string) almacen))
            {
                Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = int.Parse(empresa.ToString()), uxe_empresa_key = int.Parse(empresa.ToString()), uxe_usuario = usuario.ToString(), uxe_usuario_key = usuario.ToString() });

                //return new Select { id = "cmbPVENTA_P", diccionario = Dictionaries.GetPuntoVenta(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())),clase = Css.large, valor= uxe.uxe_puntoventa }.ToString();
                return new Select { id = id.ToString(), diccionario = Dictionaries.GetPuntoVenta(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())), clase = Css.large, valor = uxe.uxe_puntoventa, withempty = true }.ToString();
            }
            return new Select { id = id.ToString(), diccionario = Dictionaries.Empty(), clase = Css.large }.ToString();

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
            tdatos.AddColumn("Comprobante ", "", "");
            //tdatos.AddColumn("Periodo", "", "", "");
            tdatos.AddColumn("Fecha", "", "", "");
            //tdatos.AddColumn("Electrónico", "", "", "");
            //tdatos.AddColumn("Estado", "", "", "");
            //tdatos.AddColumn("Tipo", "", "", "");
            tdatos.AddColumn("Cliente/Proveedor/Socio", "", "", "");
            tdatos.AddColumn("Datos Envío", "", "", "");
            tdatos.AddColumn("Datos Transporte", "", "", "");            
            tdatos.AddColumn("Total", "", "", "");
            tdatos.AddColumn("Datos", "", "", "");
            tdatos.editable = false;
            html.AppendLine(tdatos.ToString());
            return html.ToString();
        }
        /*
        [WebMethod]
        public static string GetDetalleData(object objeto)
        {

            int tipodocfac = Constantes.cFactura.tpd_codigo;
            int tipodocgui = Constantes.cGuia.tpd_codigo;
            int tipodocpagban = Constantes.cPagoBan.tpd_codigo;
            int tipodocplacli = Constantes.cPlanillaClientes.tpd_codigo;
            string perfiladm = Constantes.cPerfilAdministrador;

            Usuario usr = new Usuario(objeto);

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object detalle = null;
            tmp.TryGetValue("detalle", out detalle);

            WhereParams parametros =  SetWhereClause(new vComprobante(objeto), usr, tipodocfac, tipodocgui,(string)detalle);
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;

            
            usr.usr_id_key = usr.usr_id;
            usr = UsuarioBLL.GetByPK(usr);

            bool IsAudit = false;
            if (usr.usr_perfil == Constantes.cPerfilAuditoria)
                IsAudit = true;


            List<vComprobante> lista = vComprobanteBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            //List<Comprobante> lista = ComprobanteBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            StringBuilder html = new StringBuilder();

            if (usr.usr_id == "admin")
            {
                HtmlRow fr = new HtmlRow();
                string sqlwhere = parametros.where;
                for (int i = 0; i < parametros.valores.Length; i++)
                {
                    sqlwhere = sqlwhere.Replace("{" + i.ToString() + "}", parametros.valores[i].ToString());
                }

                fr.cells.Add(new HtmlCell { valor = sqlwhere, colspan = 8, clase = "oculto" });
                html.AppendLine(fr.ToString());
                //html.AppendFormat("<div id='where' style='display:none'>{0}</div>",parametros.where);
            }

            foreach (vComprobante item in lista)
            {

                if (!string.IsNullOrEmpty(item.doctran))
                {


                    HtmlRow row = new HtmlRow();
                    row.data = "data-codigo='" + item.codigo + "' ";
                    row.removable = false;
                    //row.clickevent = "EditCan(this)";




                    string opc = "<div class=\"btn-group\"> " +
                                "<button data-toggle=\"dropdown\" class=\"btn btn-inverse dropdown-toggle\"><span class=\"caret\"></span></button> " +
                                "<ul class=\"dropdown-menu\">";

                    if (!IsAudit)
                    {


                        if (item.tipodoc == tipodocfac)
                        {
                            if (item.monto > item.cancela)
                                opc += "<li><a href=\"#\" onclick=\"Cobrar('" + item.codigo + "')\">Cobrar</a></li>";//COBRAR
                                                                                                                     //NUEVA OPCION DE GUIA REMISION PARA FACTURAS
                            opc += "<li><a href=\"#\" onclick=\"GuiaRemision('" + item.codigo + "')\">Guia Remisión</a></li>";//
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
                        if (item.tipodoc == tipodocgui || item.tipodoc == tipodocfac)
                        {
                            if (usr.usr_perfil == perfiladm || usr.usr_perfil == Constantes.cPerfilAsistente)
                                opc += "<li><a href=\"#\" onclick=\"Modificar('" + item.codigo + "')\">Modificar</a></li>";//Modificacion de Guias                                                 

                            if (!item.vehiculo.HasValue)
                            {
                                opc += "<li><a href=\"#\" onclick=\"AsignarSocio('" + item.codigo + "')\">Asignar Socio</a></li>";//Asignacion socio
                            }
                        }
                    }
                    if (item.tipodoc == tipodocgui || item.tipodoc == tipodocfac || item.tipodoc == 6 || item.tipodoc == 14 || item.tipodoc == 16 || item.tipodoc == 22 || item.tipodoc == 15 || item.tipodoc == 26)
                    {
                        opc += "<li><a href=\"#\" onclick=\"Informacion('" + item.codigo + "')\">Informacion</a></li>";//Modificacion de Guias                                                 
                    }
                    //if (item.tipodoc == tipodocfac)
                    //{
                    //    if (usr.usr_perfil== perfiladm)
                    //        opc += "<li><a href=\"#\" onclick=\"Mayorizar('" + item.codigo + "')\">Mayorizar</a></li>";//Mayorizar comprobantes        
                    //}


                    if (!IsAudit)
                    {
                        if (item.tipodoc == 14)//OBLIGACION
                        {

                            opc += "<li><a href=\"#\" onclick=\"GeneraRetencion('" + item.codigo + "')\">Retención</a></li>";//Modificacion de Guias                                                 
                        }

                        if (item.tipodoc == tipodocplacli)
                        {
                            opc += "<li><a href=\"#\" onclick=\"Liquidar('" + item.codigo + "')\">Liquidar</a></li>";//Modificacion de Guias                   
                            opc += "<li><a href=\"#\" onclick=\"Valores('" + item.codigo + "')\">Valores Socios</a></li>";//Gestion de valores socios
                            if (usr.usr_perfil == perfiladm || usr.usr_perfil == Constantes.cPerfilAsistente)
                                opc += "<li><a href=\"#\" onclick=\"AsignarFacturaPlanilla('" + item.codigo + "')\">Asignar Factura</a></li>";//Asigna Factura Planilla

                        }
                        if (usr.usr_perfil == perfiladm || usr.usr_perfil == Constantes.cPerfilAsistente)
                        {
                            opc += "<li><a href=\"#\" onclick=\"Anular('" + item.codigo + "')\">Anular</a></li>";//ANULAR                
                            opc += "<li><a href=\"#\" onclick=\"ModificarDatos('" + item.codigo + "')\">Modificar Datos</a></li>";//ANULAR                
                            opc += "<li><a href=\"#\" onclick=\"Mayorizar('" + item.codigo + "')\">Mayorizar</a></li>";//Mayorizar comprobantes        
                            opc += "<li><a href=\"#\" onclick=\"Recontabilizar('" + item.codigo + "')\">Recontabilzar</a></li>";//Re Mayorizar comprobantes        
                            if (item.tipodoc == 15 || item.tipodoc == 19 || item.tipodoc == 20 || item.tipodoc == 21 || item.tipodoc == 22)
                            {
                                opc += "<li><a href=\"#\" onclick=\"ModificarPago('" + item.codigo + "')\">Modificar Pago</a></li>";//Modificacion Pago
                            }
                            if (item.tipodoc == 3 || item.tipodoc == 6 || item.tipodoc == 14 || item.tipodoc == 16 || item.tipodoc == 17 || item.tipodoc == 18 || item.tipodoc == 24 || item.tipodoc == 25 || item.tipodoc == 26)
                            {
                                opc += "<li><a href=\"#\" onclick=\"ModificarPago('" + item.codigo + "')\">Modificar</a></li>";//Modificacion Otros comprobantes
                            }

                            if (item.tipodoc == 6 || item.tipodoc == 15)//RECIBOS Y PAGOS
                            {
                                opc += "<li><a href=\"#\" onclick=\"AfectaDeudas('" + item.codigo + "')\">Afectaciones</a></li>";//NUEVA OPCION PARA VER AFECTACIONES Y MODIFICAR SOLO DOCUMENTOS
                            }
                        }


                        //PARA COMPROBANTES ELECTRONICOS
                        if (!string.IsNullOrEmpty(item.claveelec))
                        {
                            if (item.estadoelec != "AUTORIZADO")
                            {
                                opc += "<li><a href=\"#\" onclick=\"GenerarElectronico('" + item.codigo + "')\">Generar Electrónico</a></li>";
                                opc += "<li><a href=\"#\" onclick=\"UpdateElectronico('" + item.codigo + "')\">Verificar Autorización</a></li>";
                            }
                            opc += "<li><a href=\"#\" onclick=\"ElectronicRide('" + item.codigo + "')\">Ver RIDE</a></li>";//NUEVA OPCION PARA VER AFECTACIONES Y MODIFICAR SOLO DOCUMENTOS
                        }
                        else
                        {
                            if (usr.usr_perfil == perfiladm || usr.usr_perfil == Constantes.cPerfilAsistente)
                            {
                                if (item.estadoelec != "AUTORIZADO")
                                {
                                    opc += "<li><a href=\"#\" onclick=\"GenerarElectronico('" + item.codigo + "')\">Generar Electrónico</a></li>";
                                    opc += "<li><a href=\"#\" onclick=\"UpdateElectronico('" + item.codigo + "')\">Verificar Autorización</a></li>";
                                }
                            }
                        }
                    }

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

                    row.cells.Add(new HtmlCell { valor = "<a href='#' onclick='CallFormulario(" + item.codigo + ");'>" + item.doctran + "</a><div class='claveelec'>"+item.claveelec+ "</div><div class='estadoelec'>" + item.estadoelec + "</div>" });
                    row.cells.Add(new HtmlCell { valor =  item.fecha + "<br><a href='#' onclick='ViewAuditoria(" + item.codigo + ")'>" + item.crea_usr + "</a><br><div class='estadoelec'>" + item.codigo + "</div>" });
                    

                    if (item.tipodoc == tipodocpagban)
                    {
                        item.nombres = item.beneficiario;
                        item.total = item.montoban;
                    }
                    row.cells.Add(new HtmlCell { valor = item.nombres });
                    row.cells.Add(new HtmlCell { valor = (!string.IsNullOrEmpty(item.nombres_rem) ? "<b>REMITENTE: </b>" + item.nombres_rem : "") + (!string.IsNullOrEmpty(item.nombres_des) ? "<br><b>DESTINATARIO: </b>" + item.nombres_des : "") + (!string.IsNullOrEmpty(item.nombreruta) ? "<br><b>RUTA: </b>" + item.nombreruta : "") });
                    row.cells.Add(new HtmlCell { valor = (!string.IsNullOrEmpty(item.nombres_soc) ? "<b>SOCIO: </b> " + item.nombres_soc : "") + (item.vehiculo.HasValue ? "<br><b>VEHICULO: </b> " + item.placa + " <b>DISCO: </b>" + item.disco : "") + (!string.IsNullOrEmpty(item.hojaruta) ? "<br><b>HOJA RUTA: </b> " + item.hojaruta : "") });
                    //row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.total) });

                    if (item.total.HasValue)
                        row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.total) });
                    else if (item.debito.HasValue)
                        row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.debito) });
                    else 
                        row.cells.Add(new HtmlCell { valor = "" });

                    //row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.total) + "<br>Deb: " + Formatos.CurrencyFormat(item.debito) + "<br>Cre:" + Formatos.CurrencyFormat(item.credito) });

                    row.cells.Add(new HtmlCell { valor = item.politica + ((item.despachado.HasValue) ? ((item.despachado.Value == 1) ? "<br/>DESPACHADO" : "") : "") + "<br>" + Constantes.GetEstadoName(item.estado.Value) });

                    html.AppendLine(row.ToString());
                }
            }
            return html.ToString();
        }
        */

        [WebMethod]
        public static string GetDetalleData(object objeto)
        {

            int tipodocfac = Constantes.cFactura.tpd_codigo;
            int tipodocgui = Constantes.cGuia.tpd_codigo;
            int tipodocpagban = Constantes.cPagoBan.tpd_codigo;
            int tipodocplacli = Constantes.cPlanillaClientes.tpd_codigo;
            string perfiladm = Constantes.cPerfilAdministrador;
            string lockmod = Constantes.GetParameter("lockmod");

            Usuario usr = new Usuario(objeto);

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object detalle = null;
            tmp.TryGetValue("detalle", out detalle);

            //WhereParams parametros = SetWhereClause(new vComprobante(objeto), usr, tipodocfac, tipodocgui, (string)detalle);
            //int desde = (pageIndex * pageSize) - pageSize + 1;
            int desde = (pageIndex * pageSize) - pageSize;
            int hasta = (pageIndex * pageSize);
            pageIndex++;


            usr.usr_id_key = usr.usr_id;
            usr = UsuarioBLL.GetByPK(usr);

            bool IsAudit = false;
            if (usr.usr_perfil == Constantes.cPerfilAuditoria)
                IsAudit = true;


            //List<vComprobante> lista = vComprobanteBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            List<vComprobante> lista = Packages.General.GetComprobantes(new vComprobante(objeto), usr, tipodocfac, tipodocgui, (string)detalle, hasta, desde);
            StringBuilder html = new StringBuilder();

            //if (usr.usr_id == "admin")
            //{
            //    HtmlRow fr = new HtmlRow();
            //    string sqlwhere = parametros.where;
            //    for (int i = 0; i < parametros.valores.Length; i++)
            //    {
            //        sqlwhere = sqlwhere.Replace("{" + i.ToString() + "}", parametros.valores[i].ToString());
            //    }

            //    fr.cells.Add(new HtmlCell { valor = sqlwhere, colspan = 8, clase = "oculto" });
            //    html.AppendLine(fr.ToString());
            //    //html.AppendFormat("<div id='where' style='display:none'>{0}</div>",parametros.where);
            //}

            foreach (vComprobante item in lista)
            {

                if (!string.IsNullOrEmpty(item.doctran))
                {


                    HtmlRow row = new HtmlRow();
                    row.data = "data-codigo='" + item.codigo + "' ";
                    row.removable = false;
                    //row.clickevent = "EditCan(this)";




                    string opc = "<div class=\"btn-group\"> " +
                                "<button data-toggle=\"dropdown\" class=\"btn btn-inverse dropdown-toggle\"><span class=\"caret\"></span></button> " +
                                "<ul class=\"dropdown-menu\">";

                    if (!IsAudit)
                    {


                        if (item.tipodoc == tipodocfac)
                        {
                            if (item.monto > item.cancela)
                                opc += "<li><a href=\"#\" onclick=\"Cobrar('" + item.codigo + "')\">Cobrar</a></li>";//COBRAR
                                                                                                                     //NUEVA OPCION DE GUIA REMISION PARA FACTURAS
                            opc += "<li><a href=\"#\" onclick=\"GuiaRemision('" + item.codigo + "')\">Guia Remisión</a></li>";//
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
                        if (item.tipodoc == tipodocgui || item.tipodoc == tipodocfac)
                        {
                            //23 Ago 2022
                            //Se eliminan las opciones de asignacion de socio y modificacion a peticion del Ing. Rayner 
                            //Se agrega el parametro lockmod en parametros con valor =1
                            if (string.IsNullOrEmpty(lockmod))
                            {
                                if (usr.usr_perfil == perfiladm || usr.usr_perfil == Constantes.cPerfilAsistente)
                                    opc += "<li><a href=\"#\" onclick=\"Modificar('" + item.codigo + "')\">Modificar</a></li>";//Modificacion de Guias                                                 


                                if (!item.vehiculo.HasValue)
                                {
                                    opc += "<li><a href=\"#\" onclick=\"AsignarSocio('" + item.codigo + "')\">Asignar Socio</a></li>";//Asignacion socio
                                }
                            }
                        }
                    }
                    if (item.tipodoc == tipodocgui || item.tipodoc == tipodocfac || item.tipodoc == 6 || item.tipodoc == 14 || item.tipodoc == 16 || item.tipodoc == 22 || item.tipodoc == 15 || item.tipodoc == 26)
                    {
                        opc += "<li><a href=\"#\" onclick=\"Informacion('" + item.codigo + "')\">Informacion</a></li>";//Modificacion de Guias                                                 
                    }
                    //if (item.tipodoc == tipodocfac)
                    //{
                    //    if (usr.usr_perfil== perfiladm)
                    //        opc += "<li><a href=\"#\" onclick=\"Mayorizar('" + item.codigo + "')\">Mayorizar</a></li>";//Mayorizar comprobantes        
                    //}


                    if (!IsAudit)
                    {
                        if (item.tipodoc == 14)//OBLIGACION
                        {

                            opc += "<li><a href=\"#\" onclick=\"GeneraRetencion('" + item.codigo + "')\">Retención</a></li>";//Modificacion de Guias                                                 
                        }

                        if (item.tipodoc == tipodocplacli)
                        {
                            opc += "<li><a href=\"#\" onclick=\"Liquidar('" + item.codigo + "')\">Liquidar</a></li>";//Modificacion de Guias                   
                            opc += "<li><a href=\"#\" onclick=\"Valores('" + item.codigo + "')\">Valores Socios</a></li>";//Gestion de valores socios
                            if (usr.usr_perfil == perfiladm || usr.usr_perfil == Constantes.cPerfilAsistente)
                                opc += "<li><a href=\"#\" onclick=\"AsignarFacturaPlanilla('" + item.codigo + "')\">Asignar Factura</a></li>";//Asigna Factura Planilla

                        }
                        if (usr.usr_perfil == perfiladm || usr.usr_perfil == Constantes.cPerfilAsistente)
                        {
                            opc += "<li><a href=\"#\" onclick=\"Anular('" + item.codigo + "')\">Anular</a></li>";//ANULAR                

                            if (string.IsNullOrEmpty(lockmod))
                                opc += "<li><a href=\"#\" onclick=\"ModificarDatos('" + item.codigo + "')\">Modificar Datos</a></li>";//ANULAR                

                            opc += "<li><a href=\"#\" onclick=\"Mayorizar('" + item.codigo + "')\">Mayorizar</a></li>";//Mayorizar comprobantes        
                            opc += "<li><a href=\"#\" onclick=\"Recontabilizar('" + item.codigo + "')\">Recontabilzar</a></li>";//Re Mayorizar comprobantes        
                            if (item.tipodoc == 15 || item.tipodoc == 19 || item.tipodoc == 20 || item.tipodoc == 21 || item.tipodoc == 22)
                            {
                                opc += "<li><a href=\"#\" onclick=\"ModificarPago('" + item.codigo + "')\">Modificar Pago</a></li>";//Modificacion Pago
                            }
                            if (item.tipodoc == 3 || item.tipodoc == 6 || item.tipodoc == 14 || item.tipodoc == 16 || item.tipodoc == 17 || item.tipodoc == 18 || item.tipodoc == 24 || item.tipodoc == 25 || item.tipodoc == 26)
                            {
                                opc += "<li><a href=\"#\" onclick=\"ModificarPago('" + item.codigo + "')\">Modificar</a></li>";//Modificacion Otros comprobantes
                            }

                            if (item.tipodoc == 6 || item.tipodoc == 15)//RECIBOS Y PAGOS
                            {
                                opc += "<li><a href=\"#\" onclick=\"AfectaDeudas('" + item.codigo + "')\">Afectaciones</a></li>";//NUEVA OPCION PARA VER AFECTACIONES Y MODIFICAR SOLO DOCUMENTOS
                            }                            
                        }


                        //PARA COMPROBANTES ELECTRONICOS
                        if (!string.IsNullOrEmpty(item.claveelec))
                        {
                            if (item.estadoelec != "AUTORIZADO")
                            {
                                opc += "<li><a href=\"#\" onclick=\"GenerarElectronico('" + item.codigo + "')\">Generar Electrónico</a></li>";
                                opc += "<li><a href=\"#\" onclick=\"UpdateElectronico('" + item.codigo + "')\">Verificar Autorización</a></li>";
                            }
                            opc += "<li><a href=\"#\" onclick=\"ElectronicRide('" + item.codigo + "')\">Ver RIDE</a></li>";//NUEVA OPCION PARA VER AFECTACIONES Y MODIFICAR SOLO DOCUMENTOS
                        }
                        else
                        {
                            if (usr.usr_perfil == perfiladm || usr.usr_perfil == Constantes.cPerfilAsistente)
                            {
                                if (item.estadoelec != "AUTORIZADO")
                                {
                                    opc += "<li><a href=\"#\" onclick=\"GenerarElectronico('" + item.codigo + "')\">Generar Electrónico</a></li>";
                                    opc += "<li><a href=\"#\" onclick=\"UpdateElectronico('" + item.codigo + "')\">Verificar Autorización</a></li>";
                                }
                            }
                        }
                    }

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

                    row.cells.Add(new HtmlCell { valor = "<a href='#' onclick='CallFormulario(" + item.codigo + ");'>" + item.doctran + "</a><div class='claveelec'>" + item.claveelec + "</div><div class='estadoelec'>" + item.estadoelec + "</div>" });
                    row.cells.Add(new HtmlCell { valor = item.fecha + "<br><a href='#' onclick='ViewAuditoria(" + item.codigo + ")'>" + item.crea_usr + "</a><br><div class='estadoelec'>" + item.codigo + "</div>" });


                    if (item.tipodoc == tipodocpagban)
                    {
                        item.nombres = item.beneficiario;
                        item.total = item.montoban;
                    }
                    row.cells.Add(new HtmlCell { valor = item.nombres });
                    row.cells.Add(new HtmlCell { valor = (!string.IsNullOrEmpty(item.nombres_rem) ? "<b>REMITENTE: </b>" + item.nombres_rem : "") + (!string.IsNullOrEmpty(item.nombres_des) ? "<br><b>DESTINATARIO: </b>" + item.nombres_des : "") + (!string.IsNullOrEmpty(item.nombreruta) ? "<br><b>RUTA: </b>" + item.nombreruta : "") });
                    row.cells.Add(new HtmlCell { valor = (!string.IsNullOrEmpty(item.nombres_soc) ? "<b>SOCIO: </b> " + item.nombres_soc : "") + (item.vehiculo.HasValue ? "<br><b>VEHICULO: </b> " + item.placa + " <b>DISCO: </b>" + item.disco : "") + (!string.IsNullOrEmpty(item.hojaruta) ? "<br><b>HOJA RUTA: </b> " + item.hojaruta : "") });
                    //row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.total) });

                    if (item.total.HasValue)
                        row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.total) });
                    else if (item.debito.HasValue)
                        row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.debito) });
                    else
                        row.cells.Add(new HtmlCell { valor = "" });

                    //row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.total) + "<br>Deb: " + Formatos.CurrencyFormat(item.debito) + "<br>Cre:" + Formatos.CurrencyFormat(item.credito) });

                    row.cells.Add(new HtmlCell { valor = item.politica + ((item.despachado.HasValue) ? ((item.despachado.Value == 1) ? "<br/>DESPACHADO" : "") : "") + "<br>" + Constantes.GetEstadoName(item.estado.Value) });

                    html.AppendLine(row.ToString());
                }
            }
            return html.ToString();
        }


        public static WhereParams SetWhereClause(vComprobante obj, Usuario usr, int tipodocfac, int tipodocgui,string detalle)
        {
            
            bool vacio = true;
            int contador = 0;
            WhereParams parametros = new WhereParams();
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
                    vacio = false;
                }
            }
            if (obj.mes.HasValue)
            {
                if (obj.mes.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_mes = {" + contador + "} ";
                    valores.Add(obj.mes);
                    contador++;
                    vacio = false;

                }
            }
            if (obj.ctipocom.HasValue)
            {
                if (obj.ctipocom.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_ctipocom = {" + contador + "} ";
                    valores.Add(obj.ctipocom);
                    contador++;
                    vacio = false;
                }
            }
            if (obj.almacen.HasValue)
            {
                if (obj.almacen.Value>0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                    valores.Add(obj.almacen);
                    contador++;
                    vacio = false;
                }
            }
            if (obj.pventa.HasValue)
            {
                if (obj.pventa.Value> 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                    valores.Add(obj.pventa);
                    contador++;
                    vacio = false;
                }
            }
            if (obj.numero.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_numero = {" + contador + "} ";
                valores.Add(obj.numero);
                contador++;
                vacio = false;
            }
            if (obj.estado.HasValue)
            {
                if (obj.estado.Value > -1)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
                    valores.Add(obj.estado);
                    contador++;
                    vacio = false;
                }

            }
            if (obj.tipodoc.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc = {" + contador + "} ";
                valores.Add(obj.tipodoc);
                contador++;
                vacio = false;
            }

            
            if (!string.IsNullOrEmpty(obj.politica))
            {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " pol_nombre LIKE {" + contador + "} ";
                    valores.Add("%" + obj.politica + "%");
                    contador++;
                    vacio = false;
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
                vacio = false;

            }

            if (!string.IsNullOrEmpty(obj.nombres))//CLIENTE O PROVEEDOR
            {

                string[] arraynombres = obj.nombres.Split(' ');
                for (int i = 0; i < arraynombres.Length; i++)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " (p.per_ciruc ILIKE {" + contador + "} or p.per_nombres ILIKE {" + contador + "} or p.per_apellidos ILIKE{" + contador + "} or e.cenv_ciruc_rem ILIKE {" + contador + "} or e.cenv_nombres_rem ILIKE {" + contador + "} or e.cenv_apellidos_rem ILIKE{" + contador + "} or e.cenv_ciruc_des ILIKE {" + contador + "} or e.cenv_nombres_des ILIKE {" + contador + "} or e.cenv_apellidos_des ILIKE{" + contador + "} or p1.per_apellidos ILIKE {" + contador + "} or p1.per_nombres ILIKE{" + contador + "} or dban_beneficiario ILIKE{" + contador + "}) ";
                    valores.Add("%" + arraynombres[i] + "%");
                    contador++;
                    
                }

                
                vacio = false;
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
                valores.Add("%" + obj.nombres_soc+ "%");
                contador++;
            }

            if (!string.IsNullOrEmpty(obj.placa)) //VEHICULO
            {

                parametros.where += ((parametros.where != "") ? " and " : "") + " (e.cenv_placa ILIKE {" + contador + "} or e.cenv_disco ILIKE {" + contador + "}) ";
                valores.Add("%" + obj.placa+ "%");
                contador++;
            }

          
            if (!string.IsNullOrEmpty(usr.usr_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc IN (SELECT udo_tipodoc FROM usrdoc WHERE udo_usuario = {"+contador+"}) ";
                valores.Add(usr.usr_id);
                contador++;
            }


            parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = " + obj.empresa + " ";
           


            string wheredocs = "";
            foreach (object item in obj.tipos)
            {
                if (item.ToString() != "")
                {
                    wheredocs += ((wheredocs != "") ? " or " : "") + " c.com_tipodoc = " + item.ToString();
                    vacio = false;
                }
            }

            string wheredetalles = "";
            if (!string.IsNullOrEmpty(detalle))
            {
                string[] arraydetalle = detalle.Split(',');
                for (int i = 0; i < arraydetalle.Length; i++)
                {
                    if (arraydetalle[i] != "") {
                        wheredetalles += ((wheredetalles != "") ? " or " : "") + " ddoc_observaciones ilike '%" + arraydetalle[i] + "%'  ";
                        // wheredetalles += ((wheredetalles != "") ? " or " : "") + " c.com_codigo IN (select ddoc_comprobante from dcomdoc where ddoc_observaciones ilike '%" + arraydetalle[i] + "%')  ";
                        vacio = false;
                    }
                }
            }


            if (vacio)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
                valores.Add(DateTime.Now.AddDays(-3).Date);
                contador++;
                parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
                valores.Add(DateTime.Now.AddDays(1).Date);
                contador++;
                //parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = " + obj.empresa + " ";
              

            }


            parametros.where += (wheredocs != "") ? " and (" + wheredocs + ")" : "";
            parametros.where += (wheredetalles!= "") ? "  and  c.com_codigo IN (select ddoc_comprobante from dcomdoc where "+ wheredetalles+ ")" : "";
            parametros.valores = valores.ToArray();

            return parametros;
        }
    }
}