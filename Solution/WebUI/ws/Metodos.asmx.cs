using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text;
using Services;
using BusinessObjects;
using BusinessLogicLayer;
using System.Web.Script.Serialization;
using HtmlObjects;
using Functions;
using System.Collections;
using Packages;
using Services;

namespace WebUI.ws
{




    /// <summary>
    /// Descripción breve de Metodos
    /// </summary>    ]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Metodos : System.Web.Services.WebService
    {
        protected string OrderByClause = "men_orden";
        protected string WhereClause = "";
        int empresa = 0;
        [WebMethod]

        public string HelloWorld()
        {
            return "Hello World";
        }



        public static string GetMenuOptions(List<BusinessObjects.Menu> lst, int? padre, bool addstruc)
        {

            List<BusinessObjects.Menu> lsthijos = lst.FindAll(delegate (BusinessObjects.Menu m) { return m.men_padre == padre; });
            StringBuilder html = new StringBuilder();
            if (lsthijos.Count > 0)
            {
                if (addstruc)
                    html.AppendLine("<ul >");

                foreach (BusinessObjects.Menu obj in lsthijos)
                {
                    bool hijos = false;
                    string htmlhijos = GetMenuOptions(lst, obj.men_id, true);
                    if (htmlhijos != "")
                        hijos = true;

                    html.AppendLine(HtmlElements.HtmlMenuItem(obj.men_id.ToString(), obj.men_formulario, obj.men_nombre, obj.men_imagen, hijos));
                    html.AppendLine(htmlhijos);

                }
                if (addstruc)
                    html.AppendLine("</ul>");
            }
            return html.ToString();
        }

        public static string GetMenuOptions(List<vMenuUsuario> lst, int? padre, bool addstruc)
        {

            List<vMenuUsuario> lsthijos = lst.FindAll(delegate (vMenuUsuario m) { return m.men_padre == padre; });
            StringBuilder html = new StringBuilder();
            if (lsthijos.Count > 0)
            {
                if (addstruc)
                    html.AppendLine("<ul >");

                foreach (vMenuUsuario obj in lsthijos)
                {
                    bool hijos = false;
                    string htmlhijos = GetMenuOptions(lst, obj.men_id, true);
                    if (htmlhijos != "")
                        hijos = true;

                    html.AppendLine(HtmlElements.HtmlMenuItem(obj.men_id.ToString(), obj.men_formulario, obj.men_nombre, obj.men_imagen, hijos));
                    html.AppendLine(htmlhijos);

                }
                if (addstruc)
                    html.AppendLine("</ul>");
            }
            return html.ToString();
        }



        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetMenu()
        {
            StringBuilder html = new StringBuilder();

            html.AppendLine("<div class=\"leftmenu\"><ul class=\"nav nav-tabs nav-stacked\">");
            html.AppendLine("<li class=\"nav-header\">Menu</li>");

            List<BusinessObjects.Menu> lst = MenuBLL.GetAll(WhereClause, OrderByClause);

            html.AppendLine(GetMenuOptions(lst, null, false));

            html.AppendLine("</ul></div>");
            return html.ToString();
        }

        [WebMethod]
        public string GetMenuByUser(object objeto)
        {
            StringBuilder html = new StringBuilder();
            if (objeto != null)
            {
                Usuario usr = new Usuario(objeto);

                List<vMenuUsuario> lst = vMenuUsuarioBLL.GetAll(new WhereParams("usr_id = {0} and men_estado =1", usr.usr_id), "men_orden");

                html.AppendLine("<div class=\"leftmenu\"><ul class=\"nav nav-tabs nav-stacked\">");
                html.AppendLine("<li class=\"nav-header\">Menu</li>");

                html.AppendLine(GetMenuOptions(lst, null, false));

                html.AppendLine("</ul></div>");

            }
            return html.ToString();
        }







        public Usuario GetObjUsuario(object objeto)
        {
            Usuario obj = new Usuario();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object id = null;
                object idkey = null;
                object password = null;
                object mail = null;
                object nombres = null;
                object perfil = null;
                object estado = null;
                tmp.TryGetValue("ID", out id);
                tmp.TryGetValue("ID_key", out idkey);
                tmp.TryGetValue("PASSWORD", out password);
                tmp.TryGetValue("MAIL", out mail);
                tmp.TryGetValue("NOMBRES", out nombres);
                tmp.TryGetValue("PERFIL", out perfil);
                tmp.TryGetValue("ESTADO", out estado);
                obj.usr_id = (string)id;
                obj.usr_id_key = (string)idkey;
                obj.usr_password = (string)password;
                obj.usr_mail = (string)mail;
                obj.usr_nombres = (string)nombres;
                obj.usr_perfil = (string)perfil;
                obj.usr_estado = (int?)estado;
                /*obj.crea_usr = "admin";
                obj.crea_fecha = DateTime.Now;
                obj.mod_usr = "admin";
                obj.mod_fecha = DateTime.Now;*/

            }

            return obj;
        }

        [WebMethod]
        public string VerificaUsuario(object objeto)
        {
            Usuario usuario = GetObjUsuario(objeto);
            string pass = usuario.usr_password;
            usuario = UsuarioBLL.GetByPK(usuario);
            if (usuario.usr_estado.HasValue)
            {
                if (usuario.usr_password == pass)
                    return "OK";
                else
                    return "Contraseña incorrecta";
            }
            else
                return "No existe el usuario";

        }

        [WebMethod]
        public string GetUsuario(string id)
        {
            Usuario usuario = new Usuario();
            usuario.usr_id = id;
            usuario.usr_id_key = id;
            usuario = UsuarioBLL.GetByPK(usuario);
            return new JavaScriptSerializer().Serialize(usuario);
        }

        [WebMethod]
        public string GetConstantes(object objeto)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("cCredito", Constantes.cCredito);
            dic.Add("cDebito", Constantes.cDebito);
            dic.Add("cCliente", Constantes.cCliente);
            dic.Add("cProveedor", Constantes.cProveedor);
            dic.Add("cSocio", Constantes.cSocio);
            dic.Add("cChofer", Constantes.cChofer);
            dic.Add("cAyudante", Constantes.cAyudante);
            return new JavaScriptSerializer().Serialize(dic);
        }

        #region Signed Objects

        [WebMethod(EnableSession = true)]
        public string SetSignedEmpresa(object objeto)
        {
            if (objeto != null)
            {
                Empresa emp = new Empresa(objeto);
                //Session["empresa"] = emp;
                Dictionaries.cod_empresa = emp.emp_codigo;//SETE LOS DICCIONARIOS


            }
            return "Ok";
        }

        [WebMethod(EnableSession = true)]
        public string SetSignedUsuario(object objeto)
        {
            if (objeto != null)
            {
                Usuario usr = new Usuario(objeto);
                Session["usuario"] = usr;
                //System.Web.Security.FormsAuthentication.SetAuthCookie(usr.usr_id, true);
            }
            return "Ok";
        }

        #endregion


        #region Comprobantes
        [WebMethod]
        public string GetNumeroComprobante(object objeto)
        {
            Comprobante comp = new Comprobante(objeto);

            Usuario usr = UsuarioBLL.GetByPK(new Usuario { usr_id = comp.crea_usr, usr_id_key = comp.crea_usr });

            return General.GetNextNumeroComprobante(comp.com_empresa, comp.com_periodo, comp.com_ctipocom, comp.com_almacen.Value, comp.com_pventa.Value).ToString();

        }

        [WebMethod]
        public string ValidaNumeroComprobante(object objeto)
        {
            Comprobante comp = new Comprobante(objeto);
            comp.com_doctran = General.GetNumeroComprobante(comp);
            List<Comprobante> lst = ComprobanteBLL.GetAll("com_doctran='" + comp.com_doctran + "' and com_empresa=" + comp.com_empresa, "");
            if (lst.Count > 0)
                return "El comprobante " + comp.com_doctran + " ya existe...";
            return "ok";


        }




        [WebMethod]
        public string GetNextNumeroComprobante(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object empresa = null;
                object periodo = null;
                object ctipocom = null;
                object almacen = null;
                object pventa = null;

                tmp.TryGetValue("empresa", out empresa);
                tmp.TryGetValue("periodo", out periodo);
                tmp.TryGetValue("ctipocom", out ctipocom);
                tmp.TryGetValue("almacen", out almacen);
                tmp.TryGetValue("pventa", out pventa);

                //return General.GetNextNumeroComprobante(int.Parse(empresa.ToString()), int.Parse(periodo.ToString()), int.Parse(ctipocom.ToString()), int.Parse(almacen.ToString()), int.Parse(pventa.ToString()));

            }
            return "SIN NUMERO";
        }

        [WebMethod]
        public string GetCabeceraComprobante(object objeto)
        {

            if (objeto != null)
            {

                bool IsOpen = true;

                bool IsAudit = false;
                Comprobante comp = new Comprobante(objeto);
                empresa = comp.com_empresa;


                Usuario usr = UsuarioBLL.GetByPK(new Usuario { usr_id = comp.crea_usr, usr_id_key = comp.crea_usr });
                if (usr.usr_perfil == Constantes.cPerfilAuditoria)
                    IsAudit = true;

                string mensajeaut = "";
                Autpersona aut = new Autpersona();

                if (comp.com_codigo == 0)
                {
                   
                    //comp = General.NuevoComprobante(comp);
                    //comp.com_numero = 0;
                    comp.com_concepto = "";
                    comp.com_estado = Constantes.cEstadoProceso;


                    Almacen alm = AlmacenBLL.GetByPK(new Almacen { alm_empresa = comp.com_empresa, alm_empresa_key = comp.com_empresa, alm_codigo = comp.com_almacen.Value, alm_codigo_key = comp.com_almacen.Value });
                    comp.com_almacenid = alm.alm_id;
                    comp.com_almacennombre = alm.alm_nombre;

                    if (comp.com_pventa.HasValue)
                    {
                        if (comp.com_numero==0)
                            comp.com_numero = General.GetNextNumeroComprobante(comp.com_empresa, comp.com_periodo, comp.com_ctipocom, comp.com_almacen.Value, comp.com_pventa.Value);
                        Puntoventa pve = PuntoventaBLL.GetByPK(new Puntoventa { pve_empresa = comp.com_empresa, pve_empresa_key = comp.com_empresa, pve_almacen = comp.com_almacen.Value, pve_almacen_key = comp.com_almacen.Value, pve_secuencia = comp.com_pventa.Value, pve_secuencia_key = comp.com_pventa.Value });
                        comp.com_pventaid = pve.pve_id;
                        comp.com_pventanombre = pve.pve_nombre;
                    }
                    if (comp.com_bodega.HasValue)
                    {
                        if (comp.com_numero == 0)
                            comp.com_numero = General.GetNextNumeroComprobante(comp.com_empresa, comp.com_periodo, comp.com_ctipocom, comp.com_almacen.Value, comp.com_bodega.Value);
                        Bodega bod = BodegaBLL.GetByPK(new Bodega { bod_empresa = comp.com_empresa, bod_empresa_key = comp.com_empresa, bod_codigo = comp.com_bodega.Value, bod_codigo_key = comp.com_bodega.Value });
                        comp.com_bodegaid = bod.bod_id;
                        comp.com_bodeganombre = bod.bod_nombre;
                    }
                    comp.com_doctran = General.GetNumeroComprobante(comp);

                    //NUEVO CONTROL DE TOKENS
                    //comp.com_token = General.GetNewTokenComprobante(comp.com_empresa, comp.crea_usr);

                }
                else
                {
                    comp.com_empresa_key = comp.com_empresa;
                    comp.com_codigo_key = comp.com_codigo;
                    comp = ComprobanteBLL.GetByPK(comp);
                    //NUEVO CONTROL QUE MAYORIZA GUIAS EXISTENTES EN PLANILLAS
                    comp = FAC.close_comprobanteplanilla(comp);
                    comp = FAC.close_comprobantehojaruta(comp);
                }


                IsOpen = Contabilidad.PeriodoIsOpen(comp.com_fecha.Year, comp.com_fecha.Month, comp.crea_usr);

                if (comp.com_tipodoc == Constantes.cFactura.tpd_codigo)
                {
                    if (comp.com_codigo == 0)
                    {
                        if (!Packages.Electronico.IsElectronic(comp))
                            aut = General.GetAutorizacion(comp, ref mensajeaut);
                    }

                    else
                    {
                        if (!Packages.Electronico.IsElectronic(comp))
                            aut = General.GetAutorizacionComprobante(comp);


                    }
                    /*else//DEBE CARGAR LA AUTORIZACION UTILIZADA
                    {
                        aut
                    }*/
                }

                bool print = true;                
                Tipodoc tpd = TipodocBLL.GetByPK(new Tipodoc { tpd_codigo = comp.com_tipodoc, tpd_codigo_key = comp.com_tipodoc });
                if (tpd.tpd_nocontable == 0)
                {
                    if (comp.com_estado != Constantes.cEstadoMayorizado)
                        print = false;
                }
                // to handle tipodoc enabled
                bool isEnabled = (tpd.tpd_estado ?? 0) == (int)Enums.EstadoRegistro.ACTIVO;


                //return General.GetNextNumeroComprobante(int.Parse(empresa.ToString()), int.Parse(periodo.ToString()), int.Parse(ctipocom.ToString()), int.Parse(almacen.ToString()), int.Parse(pventa.ToString()));
                StringBuilder html = new StringBuilder();

                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span6\">");
                html.AppendFormat("<h3 id=\"numerocomp\">{0} {1}</h3> ", comp.com_doctran, !IsOpen? "  <i class=\"iconfa-lock\" title='Periodo y mes cerrado...'></i>" : "");
                html.AppendLine(" </div><!--span6-->");
                html.AppendLine("<div class=\"span6\">");
                html.AppendLine("<div id=\"barracomp\">");
                html.AppendLine("<ul class=\"list-nostyle list-inline\">");
                html.AppendLine("<li><div class=\"btn\" onclick='window.location.reload(true);' title='Refrescar'><i class=\"iconsweets-refresh\"></i></div></li>");
                html.AppendLine("<li><div class=\"btn\" id=\"help\" href=\"#myModal\" data-toggle=\"modal\"><i class=\"iconsweets-info\"></i> &nbsp; Ayuda</div></li>");

                if (comp.com_estado != Constantes.cEstadoEliminado)
                {
                    if (IsOpen && !IsAudit)
                        html.AppendLine("<li><div class=\"btn\" id=\"view\" style=\"display:none;\"><i class=\"iconfa-search\"></i> &nbsp; Consultar</div></li>");

                    if (comp.com_estado != Constantes.cEstadoMayorizado && IsOpen && !IsAudit && isEnabled)
                        html.AppendLine("<li><div class=\"btn\" id=\"save\"><i class=\"iconfa-save\"></i> &nbsp; Guardar </div></li>");


                    if (print)
                    {
                        html.AppendLine("<li><div class=\"btn\" id=\"print\"><i class=\"iconfa-print\"></i> &nbsp; Imprimir </div></li>");                        
                    }
                    if (comp.com_tipodoc == Constantes.cHojaRuta.tpd_codigo)
                    {
                        html.AppendLine("<li><div class=\"btn\" id=\"printdet\"><i class=\"iconfa-print\"></i> &nbsp; Imprimir Detalle</div></li>");
                        html.AppendLine("<li><div class=\"btn\" id=\"printtic\"><i class=\"iconfa-print\"></i> &nbsp; Imprimir Tickets</div></li>");
                        html.AppendLine("<li><div class=\"btn\" id=\"getcsv\"><i class=\"iconfa-download\"></i> &nbsp; Descargar CSV</div></li>");
                    }
                    if (IsOpen && !IsAudit)
                        html.AppendLine("<li><div class=\"btn\" id=\"invo\"><i class=\"iconfa-money\"></i> &nbsp; Factura </div></li>");
                    if (comp.com_estado != Constantes.cEstadoMayorizado && IsOpen && !IsAudit)
                        html.AppendLine("<li><div class=\"btn\" id=\"close\"><i class=\"iconfa-lock\"></i> &nbsp; Mayorizar </div></li>");
                    if (comp.com_estado == Constantes.cEstadoMayorizado)
                        html.AppendLine("<li><div class=\"btn\" id=\"contabilizacion\"><i class=\"iconfa-lock\"></i> &nbsp; Contabilizacion </div></li>");
                    //SE DEBE MOSTRAR SOLO SI YA ESTA PAGADA
                    if (IsOpen && !IsAudit)
                        html.AppendLine("<li><div class=\"btn\" id=\"despachar\"><i class=\"iconfa-lock\"></i> &nbsp; Despachar </div></li>");
                }
                html.AppendLine("</ul>");
                html.AppendLine("</div>");
                html.AppendLine(" </div><!--span6-->");
                html.AppendLine("</div><!--row-fluid-->");

                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span6\">");
                HtmlTable tdcom = new HtmlTable();
                tdcom.CreteEmptyTable(3, 2);
                tdcom.rows[0].cells[0].valor = "Fecha/Estado:";
                //tdcom.rows[0].cells[1].valor = new Input { id = "txtFECHACOMP", valor = comp.com_fecha.ToShortDateString(), clase = Css.medium, habilitado = false }.ToString();
                tdcom.rows[0].cells[1].valor = new Input { id = "txtFECHACOMP", valor = comp.com_fecha.ToString("dd/MM/yyyy HH:mm"), clase = Css.medium, habilitado = false }.ToString() + " " + new Input { id = "txtESTADOCOMP", valor = Constantes.GetEstadoName(comp.com_estado), clase = Css.small, habilitado = false }.ToString();
                tdcom.rows[1].cells[0].valor = "Almacen:";
                tdcom.rows[1].cells[1].valor = new Input { id = "txtIDALMACEN", clase = Css.mini, habilitado = false, valor = comp.com_almacenid }.ToString() + " " + new Input { id = "txtALMACEN", clase = Css.medium, habilitado = false, valor = comp.com_almacennombre }.ToString() + " " + new Input { id = "txtCODALMACEN", visible = false, valor = comp.com_almacen }.ToString();
                if (comp.com_pventa.HasValue)
                {
                    tdcom.rows[2].cells[0].valor = "Punto Venta:";
                    tdcom.rows[2].cells[1].valor = new Input { id = "txtIDPVENTA", clase = Css.mini, habilitado = false, valor = comp.com_pventaid }.ToString() + " " + new Input { id = "txtPVENTA", clase = Css.medium, habilitado = false, valor = comp.com_pventanombre }.ToString() + " " + new Input { id = "txtCODPVENTA", visible = false, valor = comp.com_pventa }.ToString();
                }
                if (comp.com_bodega.HasValue)
                {
                    tdcom.rows[2].cells[0].valor = "Bodega:";
                    tdcom.rows[2].cells[1].valor = new Input { id = "txtIDBODEGA", clase = Css.mini, habilitado = false, valor = comp.com_bodegaid }.ToString() + " " + new Input { id = "txtBODEGA", clase = Css.medium, habilitado = false, valor = comp.com_bodeganombre }.ToString() + " " + new Input { id = "txtCODBODEGA", visible = false, valor = comp.com_bodega }.ToString();
                }
                html.AppendLine(tdcom.ToString());
                html.AppendLine(" </div><!--span6-->");

                //if (comp.com_tipodoc == Constantes.cFactura.tpd_codigo)
                // {

                if (!Packages.Electronico.IsElectronic(comp))
                {

                    if (aut != null)
                    {
                        html.AppendLine("<div class=\"span6\">");
                        HtmlTable tdaut = new HtmlTable();
                        tdaut.CreteEmptyTable(3, 2);
                        tdaut.rows[0].cells[0].valor = "AUTORIZACIÓN:";
                        tdaut.rows[0].cells[1].valor = new Input { id = "txtNROAUT", valor = aut.ape_nro_autoriza, clase = Css.medium, habilitado = false }.ToString();
                        tdaut.rows[1].cells[0].valor = "VALIDA HASTA:";
                        tdaut.rows[1].cells[1].valor = new Input { id = "txtFECHAAUT", clase = Css.medium, habilitado = false, datepicker = true, datetimevalor = (aut.ape_val_fecha.HasValue) ? aut.ape_val_fecha.Value : new DateTime() }.ToString();
                        tdaut.rows[2].cells[0].valor = "";
                        tdaut.rows[2].cells[1].valor = "<div class='blink_me'>" + mensajeaut + "</div> " + new Input { id = "txtCREADO", visible = false, valor = Constantes.cEstadoProceso }.ToString() + new Input { id = "txtCONTABILIZADO", visible = false, valor = Constantes.cEstadoGrabado }.ToString() + new Input { id = "txtCERRADO", visible = false, valor = Constantes.cEstadoMayorizado }.ToString() + "<div id='mensaje'></div> ";
                        html.AppendLine(tdaut.ToString());
                        html.AppendLine(" </div><!--span6-->");
                    }
                }
                else //SECCION DE COMPROBANTES ELECTRONICOS
                {
                    Electronicos ele = Packages.Electronico.GetElectronicoConfig(comp);
                    if (comp.com_codigo > 0)
                    {
                        if (comp.com_estadoelec != "AUTORIZADO")
                            comp = Packages.Electronico.UpdateElectronicoData(comp);
                        
                        if (comp.com_estadoelec == "AUTORIZADO")
                            comp.com_mensajeelec = ""; //limpia cualquier mensaje anterior
                    }
                    else
                    {
                        comp.com_emision = "1"; //1 SIEMPRE EMISION NORMAL
                        comp.com_ambiente = ele.ambiente.ToString();
                    }



                    html.AppendLine("<div class=\"span6\">");
                    HtmlTable tdaut = new HtmlTable();
                    tdaut.CreteEmptyTable(3, 2);
                    tdaut.rows[0].cells[0].valor = "CLAVE:";
                    tdaut.rows[0].cells[1].valor = new Input { id = "txtCLAVEELEC", valor = comp.com_claveelec, clase = Css.xxlarge, habilitado = false }.ToString();
                    tdaut.rows[1].cells[0].valor = "ESTADO/EMISION/AMBIENTE:";
                    tdaut.rows[1].cells[1].valor = new Input { id = "txtESTADOELEC", clase = Css.medium, habilitado = false, valor = comp.com_estadoelec }.ToString() + " " + new Input { id = "txtEMISION", valor = comp.com_emision == "1" ? "NORMAL" : "CONTINGENCIA", clase = Css.small, habilitado = false }.ToString() + " " + new Input { id = "txtAMBIENTE", clase = Css.small, habilitado = false, valor = comp.com_ambiente == "2" ? "PRODUCCION" : "PRUEBAS" }.ToString() + " " + new Boton { small = true, id = "btncallelec", tooltip = "Verificar Autorización", clase = "iconsweets-key", click = "CallElectronico(" + comp.com_codigo + ")" }.ToString() + " " + new Boton { small = true, id = "btnrideelec", tooltip = "Ver RIDE", clase = "iconsweets-pdf2", click = "ElectronicRide(" + comp.com_codigo + ")" }.ToString();
                    tdaut.rows[2].cells[0].valor = "MENSAJE";
                    tdaut.rows[2].cells[1].valor = new Input { id = "txtMENSAJEELEC", clase = Css.xxlarge, habilitado = false, valor = comp.com_mensajeelec }.ToString();
                    html.AppendLine(tdaut.ToString());
                    html.AppendLine(" </div><!--span6-->");
                }
                //return mensajeaut;
                //}
                html.AppendLine("</div><!--row-fluid-->");


                //CAMPOS OCULTOS
                html.Append(Document.Input("txtCTIPOCOM", comp.com_ctipocom.ToString(), "", "", false, false, ElementEnums.InputType.hidden));
                html.Append(Document.Input("txtNOCONTABLE", comp.com_nocontable.ToString(), "", "", false, false, ElementEnums.InputType.hidden));
                html.Append(Document.Input("txtNUMERO", comp.com_numero.ToString(), "", "", false, false, ElementEnums.InputType.hidden));

                //CONTROL TOKEN
                html.AppendLine(new Input { id = "txtTOKEN", valor = comp.com_token, visible = false }.ToString());

                object[] retorno = new object[2];
                retorno[0] = comp.com_codigo;
                retorno[1] = html.ToString();
                return new JavaScriptSerializer().Serialize(retorno);

                //return html.ToString();


            }
            return "SIN NUMERO";
        }


        [WebMethod]
        public string GetComprobante(object objeto)
        {
            if (objeto != null)
            {

                Comprobante comp = new Comprobante(objeto);

                comp.com_empresa_key = comp.com_empresa;
                comp.com_codigo_key = comp.com_codigo; ;
                comp = ComprobanteBLL.GetByPK(comp);

                comp.ccomdoc = new Ccomdoc();
                comp.ccomenv = new Ccomenv();
                comp.total = new Total();

                comp.ccomdoc.cdoc_comprobante = comp.com_codigo;
                comp.ccomdoc.cdoc_empresa = comp.com_empresa;
                comp.ccomdoc.cdoc_comprobante_key = comp.com_codigo;
                comp.ccomdoc.cdoc_empresa_key = comp.com_empresa;
                comp.ccomdoc = CcomdocBLL.GetByPK(comp.ccomdoc);

                comp.ccomenv.cenv_comprobante = comp.com_codigo;
                comp.ccomenv.cenv_empresa = comp.com_empresa;
                comp.ccomenv.cenv_comprobante_key = comp.com_codigo;
                comp.ccomenv.cenv_empresa_key = comp.com_empresa;
                comp.ccomenv = CcomenvBLL.GetByPK(comp.ccomenv);

                comp.total.tot_comprobante = comp.com_codigo;
                comp.total.tot_empresa = comp.com_empresa;
                comp.total.tot_comprobante_key = comp.com_codigo;
                comp.total.tot_empresa_key = comp.com_empresa;
                comp.total = TotalBLL.GetByPK(comp.total);

                comp.rutafactura = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobanteruta = {0} and rfac_empresa = {1}", comp.com_codigo, comp.com_empresa), "");
                comp.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_comprobante = {0} and ddoc_empresa = {1}", comp.com_codigo, comp.com_empresa), "ddoc_secuencia");

                return new JavaScriptSerializer().Serialize(comp);


            }
            return "SIN NUMERO";
        }



        #endregion

        #region Empresas

        /// <summary>
        /// Obtiene todas las empresas
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetEmpresas()
        {
            return new Select { id = "empresas", diccionario = Dictionaries.GetEmpresa() }.ToString();
        }


        #endregion
        #region Pais Provincia Canton


        /// <summary>
        /// Obtiene todas las provincias del pais indicado
        /// </summary>
        /// <param name="pais">codigo del pais del que se desean obtener sus provincias</param>
        /// <param name="provincia">codigo de la provincia que desea aparezca seleccionada</param>
        /// <returns></returns>
        [WebMethod]
        public string GetProvincia(object objeto)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object id = null;
            object pais = null;
            object provincia = null;
            tmp.TryGetValue("id", out id);
            tmp.TryGetValue("pais", out pais);
            tmp.TryGetValue("provincia", out provincia);

            int? codpais = null;
            int cod = 0;
            if (pais != null)
            {
                if (int.TryParse(pais.ToString(), out cod))
                    codpais = cod;
            }
            return HtmlElements.Select(id.ToString(), provincia.ToString(), Dictionaries.GetProvincias(codpais));


        }

        /// <summary>
        /// Obtiene todos los cantones de una provincia
        /// </summary>
        /// <param name="provincia">codigo de la provincia de la que se desean obtener todos los cantones</param>
        /// <param name="canton">codigo del canton que se desea aparezca seleccionado</param>
        /// <returns></returns>
        [WebMethod]
        public string GetCanton(object objeto)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object id = null;
            object canton = null;
            object provincia = null;
            tmp.TryGetValue("id", out id);
            tmp.TryGetValue("provincia", out provincia);
            tmp.TryGetValue("canton", out canton);

            int? codpro = null;

            int cod = 0;
            if (provincia != null)
            {
                if (int.TryParse(provincia.ToString(), out cod))
                    codpro = cod;
            }
            return HtmlElements.Select(id.ToString(), canton.ToString(), Dictionaries.GetCantones(codpro));
        }

        #endregion
        #region autocomplete

        #region Almacen


        [WebMethod]
        public List<Almacen> GetAlmacenObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("alm_id like {0} or alm_nombre like {0}", "%" + filterKey + "%");
            List<Almacen> lst = AlmacenBLL.GetAllTop(whereparams, "alm_nombre", 10);
            return lst;
        }

        [WebMethod]
        public List<AutocompleteItem> GetAlmacen(string filterKey)
        {
            List<Almacen> lstA = GetAlmacenObj(filterKey);
            List<AutocompleteItem> lst = new List<AutocompleteItem>();
            foreach (Almacen item in lstA)
            {
                lst.Add(new AutocompleteItem(item.alm_id + " " + item.alm_nombre, item.alm_id, item.alm_id + " " + item.alm_nombre));
            }
            return lst;
        }






        #endregion

        #region Bodega

        [WebMethod]
        public List<Bodega> GetBodegaObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("bod_id like {0} or bod_nombre like {0}  ", "%" + filterKey + "%");
            List<Bodega> lst = BodegaBLL.GetAllTop(whereparams, "bod_nombre", 5);
            return lst;
        }

        [WebMethod]
        public string GetBodega(object objeto)
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

            Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = int.Parse(empresa.ToString()), uxe_empresa_key = int.Parse(empresa.ToString()), uxe_usuario = usuario.ToString(), uxe_usuario_key = usuario.ToString() });

            //return new Select { id = "cmbPVENTA_P", diccionario = Dictionaries.GetPuntoVenta(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())),clase = Css.large, valor= uxe.uxe_puntoventa }.ToString();
            return new Select { id = id.ToString(), diccionario = Dictionaries.GetBodega(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())), clase = Css.large, valor = uxe.uxe_puntoventa }.ToString();

        }

        [WebMethod]
        public string GetBodegaEmpty(object objeto)
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

            //Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = int.Parse(empresa.ToString()), uxe_empresa_key = int.Parse(empresa.ToString()), uxe_usuario = usuario.ToString(), uxe_usuario_key = usuario.ToString() });

            //return new Select { id = "cmbPVENTA_P", diccionario = Dictionaries.GetPuntoVenta(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())),clase = Css.large, valor= uxe.uxe_puntoventa }.ToString();
            return new Select { id = id.ToString(), diccionario = (!string.IsNullOrEmpty((string)almacen)) ? Dictionaries.GetBodega(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())) : Dictionaries.Empty(), clase = Css.large, withempty = true }.ToString();

        }

        #endregion

        #region Cuentas Contables


        [WebMethod]
        public List<Cuenta> GetCuentaObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("(cue_id like {0} or cue_nombre like {0}) and cue_movimiento=1", "%" + filterKey + "%");
            List<Cuenta> lst = CuentaBLL.GetAllTop(whereparams, "cue_nombre", 10);
            return lst;
        }

        [WebMethod]
        public List<AutocompleteItem> GetCuenta(string filterKey)
        {
            List<Cuenta> lstCuenta = GetCuentaObj(filterKey);
            List<AutocompleteItem> lst = new List<AutocompleteItem>();
            foreach (Cuenta item in lstCuenta)
            {
                lst.Add(new AutocompleteItem(item.cue_id + " " + item.cue_nombre, item.cue_id, item.cue_id + " " + item.cue_nombre));
            }
            return lst;
        }






        #endregion

        #region Lista Precios


        [WebMethod]
        public List<Listaprecio> GetListaObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("lpr_id like {0} or lpr_nombre like {0}", "%" + filterKey + "%");
            List<Listaprecio> lst = ListaprecioBLL.GetAllTop(whereparams, "lpr_nombre", 5);
            return lst;
        }

        [WebMethod]
        public List<AutocompleteItem> GetLista(string filterKey)
        {
            List<Listaprecio> lstObj = GetListaObj(filterKey);
            List<AutocompleteItem> lst = new List<AutocompleteItem>();
            foreach (Listaprecio item in lstObj)
            {
                lst.Add(new AutocompleteItem(item.lpr_id + " " + item.lpr_nombre, item.lpr_id, item.lpr_nombre));
            }
            return lst;
        }

        #endregion
        #region Comprobante


        [WebMethod]
        public List<Comprobante> GetComprobanteObj(string filterKey)
        {

            WhereParams whereparams = new WhereParams("com_doctran like {0} ", "%" + filterKey + "%");
            List<Comprobante> lstPersona = ComprobanteBLL.GetAllByPage(whereparams, "com_codigo", 0, 5);
            return lstPersona;
        }

        [WebMethod]
        public List<Comprobante> GetFacturaObj(string filterKey)
        {

            WhereParams whereparams = new WhereParams("com_doctran like {0} and com_tipodoc={1}", "%" + filterKey + "%", Constantes.cFactura.tpd_codigo);
            List<Comprobante> lstPersona = ComprobanteBLL.GetAllByPage(whereparams, "com_codigo", 0, 5);
            return lstPersona;
        }

        [WebMethod]
        public List<Comprobante> GetFacturaObjParam(object parametros)
        {
                Dictionary<string, object> tmp = (Dictionary<string, object>)parametros;
            object com_codclipro = null;
            object filterKey = null;
            tmp.TryGetValue("com_codclipro", out com_codclipro);
            tmp.TryGetValue("filterKey", out filterKey);
            //WhereParams whereparams = new WhereParams("c.com_doctran like {0} and c.com_tipodoc={1} and c.com_codclipro={2}", "%" + filterKey.ToString() + "%", Constantes.cFactura.tpd_codigo, int.Parse(com_codclipro.ToString()));

            //List<vComprobante> lst = vComprobanteBLL.GetAllByPage(whereparams, "t.com_doctran", 0, 5);
            //return lst;

            int? codclipro = (int?)Dictionaries.GetObject(parametros, "com_codclipro", typeof(int?));

            WhereParams whereparams = new WhereParams("com_doctran like {0} and com_tipodoc={1} and com_codclipro={2}", "%" + filterKey.ToString() + "%", Constantes.cFactura.tpd_codigo,  codclipro);    
            List<Comprobante> lstPersona = ComprobanteBLL.GetAllByPage(whereparams, "com_codigo", 0, 5);
            return lstPersona;
        }



        [WebMethod]
        public List<Comprobante> GetObligacionObj(string filterKey)
        {

            WhereParams whereparams = new WhereParams("(com_doctran like {0} or cdoc_aut_factura like {0}) and com_tipodoc={1} and com_estado!={2}", "%" + filterKey + "%", Constantes.cObligacion.tpd_codigo, Constantes.cEstadoEliminado);
            List<Comprobante> lstPersona = ComprobanteBLL.GetAllByPage(whereparams, "com_codigo", 0, 5);
            return lstPersona;
        }

        [WebMethod]
        public List<Comprobante> GetObligacionObjParam(object parametros)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)parametros;
            object com_codclipro = null;
            object filterKey = null;
            tmp.TryGetValue("com_codclipro", out com_codclipro);
            tmp.TryGetValue("filterKey", out filterKey);


            WhereParams whereparams = new WhereParams("com_doctran like {0} and com_tipodoc={1} and com_codclipro={2}", "%" + filterKey.ToString() + "%", Constantes.cObligacion.tpd_codigo, com_codclipro);
            List<Comprobante> lstPersona = ComprobanteBLL.GetAllByPage(whereparams, "com_codigo", 0, 5);
            return lstPersona;
        }



        [WebMethod]
        public List<vComprobante> GetHojaRutaObj(object parametros)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)parametros;
            object empresa = null;
            object almacen = null;
            object filterKey = null;
            tmp.TryGetValue("empresa", out empresa);
            tmp.TryGetValue("almacen", out almacen);
            tmp.TryGetValue("filterKey", out filterKey);

            WhereParams whereparams = new WhereParams("c.com_empresa = {0} and c.com_almacen={1} and (c.com_doctran like {2} or p.per_nombres ilike {2} or p.per_apellidos ilike {2} ) and c.com_tipodoc={3} and (c.com_estado = {4} or c.com_estado={5})", empresa, almacen, "%" + filterKey + "%", 5, Constantes.cEstadoGrabado, Constantes.cEstadoProceso);
            List<vComprobante> lst = vComprobanteBLL.GetAllByPage(whereparams, "t.com_doctran DESC", 0, 10);
            return lst;


        }

        #endregion
        #region Persona
        [WebMethod]
        public List<Persona> GetPersonaObj(string filterKey)
        {
            int empresa = Dictionaries.cod_empresa;
            WhereParams whereparams = new WhereParams("(per_id like {0} or per_razon like {0} or per_ciruc like {0} or per_nombres like {0} or per_apellidos like {0}) and per_estado={1} and per_empresa={2}", "%" + filterKey + "%", 1, empresa);
            List<Persona> lstPersona = vPersonaBLL.GetAllTop(whereparams, "per_razon", 5);
            return lstPersona;
        }

        [WebMethod]
        public List<Persona> GetClienteObj(string filterKey)
        {
            int empresa = Dictionaries.cod_empresa;
            WhereParams whereparams = new WhereParams("(per_id like {0} or per_razon like {0} or per_ciruc like {0} or per_nombres like {0} or per_apellidos like {0}) and per_estado={1} and pxt_tipo={2} and per_empresa={3}", "%" + filterKey + "%", 1, Constantes.cCliente, empresa);
            List<Persona> lstPersona = vPersonaBLL.GetAllTop(whereparams, "per_razon", 5);
            return lstPersona;
        }

        [WebMethod]
        public List<Persona> GetProveedorObj(string filterKey)
        {
            int empresa = Dictionaries.cod_empresa;
            WhereParams whereparams = new WhereParams("(per_id like {0} or per_razon like {0} or per_ciruc like {0} or per_nombres like {0} or per_apellidos like {0}) and per_estado={1} and pxt_tipo={2} and per_empresa={3}", "%" + filterKey + "%", 1, Constantes.cProveedor, empresa);
            List<Persona> lstPersona = vPersonaBLL.GetAllTop(whereparams, "per_razon", 5);
            return lstPersona;
        }

        [WebMethod]
        public List<Persona> GetsocioObj(string filterKey)
        {
            int empresa = Dictionaries.cod_empresa;
            WhereParams whereparams = new WhereParams("(per_id like {0} or per_razon like {0} or per_ciruc like {0} or per_nombres like {0} or per_apellidos like {0}) and per_estado={1} and pxt_tipo={2} and per_empresa={3}", "%" + filterKey + "%", 1, Constantes.cSocio, empresa);
            List<Persona> lstPersona = vPersonaBLL.GetAllTop(whereparams, "per_razon", 5);
            return lstPersona;
        }


        [WebMethod]
        public List<Persona> GetChoferObj(string filterKey)
        {
            int empresa = Dictionaries.cod_empresa;
            WhereParams whereparams = new WhereParams("(per_id like {0} or per_razon like {0} or per_ciruc like {0} or per_nombres like {0} or per_apellidos like {0}) and per_estado={1} and pxt_tipo={2} and per_empresa={3}", "%" + filterKey + "%", 1, Constantes.cChofer, empresa);
            List<Persona> lstPersona = vPersonaBLL.GetAllTop(whereparams, "per_razon", 5);
            return lstPersona;
        }




        [WebMethod]
        public List<AutocompleteItem> GetPersona(string filterKey)
        {
            List<Persona> lstPersona = GetPersonaObj(filterKey);
            List<AutocompleteItem> lst = new List<AutocompleteItem>();
            foreach (Persona item in lstPersona)
            {
                lst.Add(new AutocompleteItem(item.per_id + " " + item.per_ciruc + " " + item.per_razon, item.per_id, item.per_ciruc + " " + item.per_razon));
            }
            return lst;
        }


        [WebMethod]
        public List<Destino> GetDestinoObj(object parametros)
        {

            IDictionary<string, object> tmp = (Dictionary<string, object>)parametros;
            object des_persona = null;
            object filterKey = null;
            tmp.TryGetValue("des_persona", out des_persona);
            tmp.TryGetValue("filterKey", out filterKey);

            List<Destino> lst = new List<Destino>();
            if (des_persona != null)
            {
                if (des_persona.ToString() != "")
                {
                    WhereParams whereparams = new WhereParams("des_destino ilike {0} and des_persona={1}", "%" + filterKey.ToString() + "%", int.Parse(des_persona.ToString()));
                    lst = DestinoBLL.GetAllByPage(whereparams, "des_destino", 0, 5);
                }
            }

            return lst;
        }


        #endregion
        #region Catalogo


        [WebMethod]
        public List<Catalogo> GetCatalogoObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("cat_nombre like {0}  ", "%" + filterKey + "%");
            List<Catalogo> lst = CatalogoBLL.GetAllTop(whereparams, "cat_nombre", 5);
            return lst;
        }

        #endregion

        #region Politica


        [WebMethod]
        public List<Politica> GetPoliticaObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("pol_id like {0} or pol_nombre like {0}", "%" + filterKey + "%");
            List<Politica> lst = PoliticaBLL.GetAllTop(whereparams, "pol_nombre", 5);
            return lst;
        }

        [WebMethod]
        public List<AutocompleteItem> GetPolitica(string filterKey)
        {
            List<Politica> lstObj = GetPoliticaObj(filterKey);
            List<AutocompleteItem> lst = new List<AutocompleteItem>();
            foreach (Politica item in lstObj)
            {
                lst.Add(new AutocompleteItem(item.pol_nombre, item.pol_id, item.pol_nombre));
            }
            return lst;
        }

        #endregion
        #region Banco


        [WebMethod]
        public List<Banco> GetBancoObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("ban_id like {0} or ban_nombre like {0}  ", "%" + filterKey + "%");
            List<Banco> lst = BancoBLL.GetAllTop(whereparams, "ban_nombre", 5);
            return lst;
        }

        [WebMethod]
        public List<AutocompleteItem> GetBanco(string filterKey)
        {
            List<Banco> lstObj = GetBancoObj(filterKey);
            List<AutocompleteItem> lst = new List<AutocompleteItem>();
            foreach (Banco item in lstObj)
            {
                lst.Add(new AutocompleteItem(item.ban_nombre, item.ban_id, item.ban_nombre));
            }
            return lst;
        }

        #endregion

        #region Producti


        [WebMethod]
        public List<Producto> GetProductoObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("pro_id like {0} or pro_nombre like {0}", "%" + filterKey + "%");
            List<Producto> lst = ProductoBLL.GetAllTop(whereparams, "pro_nombre", 5);
            return lst;
        }

        [WebMethod]
        public List<AutocompleteItem> GetProducto(string filterKey)
        {
            List<Producto> lstProducto = GetProductoObj(filterKey);
            List<AutocompleteItem> lst = new List<AutocompleteItem>();
            foreach (Producto item in lstProducto)
            {
                lst.Add(new AutocompleteItem(item.pro_id + " " + item.pro_nombre, item.pro_id, item.pro_id + " " + item.pro_nombre));
            }
            return lst;
        }

        #endregion

        #region Perfil


        [WebMethod]
        public List<AutocompleteItem> GetPerfil(string filterKey)
        {
            WhereParams whereparams = new WhereParams("per_descripcion like {0} or per_id like {1}", "%" + filterKey + "%", "%" + filterKey + "%");
            List<Perfil> lstPerfil = PerfilBLL.GetAllTop(whereparams, "per_descripcion", 5);
            List<AutocompleteItem> lst = new List<AutocompleteItem>();
            foreach (Perfil item in lstPerfil)
            {
                lst.Add(new AutocompleteItem(item.per_id + " " + item.per_descripcion, item.per_id, item.per_descripcion));
            }
            return lst;

            /*var filteredPersonas = from c in lstPersona
                                   where c.per_nombres.StartsWith(filterKey)
                                   select c;
            //
            return filteredPersonas.ToList();*/
        }

        #endregion


        #region Impuesto


        [WebMethod]
        public List<Impuesto> GetImpuestoObj(string filterKey)
        {

            WhereParams whereparams = new WhereParams("imp_id like {0} or imp_nombre like {0} ", "%" + filterKey + "%");
            List<Impuesto> lstPersona = ImpuestoBLL.GetAllTop(whereparams, "imp_nombre", 5);
            return lstPersona;
        }


        [WebMethod]
        public List<Impuesto> GetImpuestoRentenObj(string filterKey)
        {

            WhereParams whereparams = new WhereParams("imp_ivafuente =1 and (imp_id like {0} or imp_nombre like {0}) ", "%" + filterKey + "%");
            List<Impuesto> lstPersona = ImpuestoBLL.GetAllTop(whereparams, "imp_nombre", 5);
            return lstPersona;
        }

        #endregion


        #region Punto Venta
        /// <summary>
        /// Obtiene todos los cantones de una provincia
        /// </summary>
        /// <param name="provincia">codigo de la provincia de la que se desean obtener todos los cantones</param>
        /// <param name="canton">codigo del canton que se desea aparezca seleccionado</param>
        /// <returns></returns>
        /// 
        [WebMethod]
        public string GetPuntoVenta(object objeto)
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

            Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = int.Parse(empresa.ToString()), uxe_empresa_key = int.Parse(empresa.ToString()), uxe_usuario = usuario.ToString(), uxe_usuario_key = usuario.ToString() });

            //return new Select { id = "cmbPVENTA_P", diccionario = Dictionaries.GetPuntoVenta(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())),clase = Css.large, valor= uxe.uxe_puntoventa }.ToString();
            return new Select { id = id.ToString(), diccionario = Dictionaries.GetPuntoVenta(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())), clase = Css.large, valor = uxe.uxe_puntoventa }.ToString();

        }

        [WebMethod]
        public string GetPuntoVentaEmpty(object objeto)
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

            //Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = int.Parse(empresa.ToString()), uxe_empresa_key = int.Parse(empresa.ToString()), uxe_usuario = usuario.ToString(), uxe_usuario_key = usuario.ToString() });

            //return new Select { id = "cmbPVENTA_P", diccionario = Dictionaries.GetPuntoVenta(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())),clase = Css.large, valor= uxe.uxe_puntoventa }.ToString();
            return new Select { id = id.ToString(), diccionario = (!string.IsNullOrEmpty((string)almacen)) ? Dictionaries.GetPuntoVenta(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())) : Dictionaries.Empty(), clase = Css.large, withempty = true }.ToString();

        }

        #endregion

        [WebMethod]
        public string GetPuntoVentaAdu(object objeto)
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

            if (empresa == null && almacen == null)
            {

                return new Select { id = "cmbPVENTA_P", diccionario = Dictionaries.Empty() }.ToString();
            }

            id = 1;
            usuario = 1;
            Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = int.Parse(empresa.ToString()), uxe_empresa_key = int.Parse(empresa.ToString()), uxe_usuario = usuario.ToString(), uxe_usuario_key = usuario.ToString() });

            return new Select { id = "cmbPVENTA_P", diccionario = Dictionaries.GetPuntoVenta(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())), clase = Css.large, valor = uxe.uxe_puntoventa }.ToString();

        }



        #region Vehiculo

        [WebMethod]
        public List<vVehiculo> GetvVehiculoObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("veh_id like {0} or veh_nombre like {0} or veh_disco like {0} or socio.per_nombres like {0} or socio.per_apellidos like {0} or veh_placa like {0}", "%" + filterKey + "%");
            List<vVehiculo> lst = vVehiculoBLL.GetAll(whereparams, "veh_nombre");
            return lst;
        }

        [WebMethod]
        public List<Vehiculo> GetVehiculoObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("veh_id like {0} or veh_nombre like {0} or veh_disco like {0} or veh_placa like {0}", "%" + filterKey + "%");
            List<Vehiculo> lst = VehiculoBLL.GetAllTop(whereparams, "veh_nombre", 5);
            return lst;
        }

        [WebMethod]
        public List<AutocompleteItem> GetVehiculo(string filterKey)
        {
            List<Vehiculo> lstVehiculos = GetVehiculoObj(filterKey);
            List<AutocompleteItem> lst = new List<AutocompleteItem>();
            foreach (Vehiculo item in lstVehiculos)
            {
                lst.Add(new AutocompleteItem(item.veh_nombre, item.veh_id, item.veh_nombre));
            }
            return lst;
        }

        #endregion

        #region Ctipocom
        [WebMethod]
        public List<Ctipocom> GetCtipocomObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("cti_id like {0} or cti_nombre like {0}", "%" + filterKey + "%");
            List<Ctipocom> lst = CtipocomBLL.GetAllTop(whereparams, "cti_nombre", 5);
            return lst;
        }
        #endregion


        #region Ruta


        [WebMethod]
        public List<Ruta> GetRutaObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("rut_id like {0} or rut_nombre like {0} or rut_origen like {0} or rut_destino like {0}", "%" + filterKey + "%");
            List<Ruta> lst = RutaBLL.GetAllTop(whereparams, "rut_nombre", 10);
            return lst;
        }

        [WebMethod]
        public List<AutocompleteItem> GetRuta(string filterKey)
        {
            List<Ruta> lstVehiculos = GetRutaObj(filterKey);
            List<AutocompleteItem> lst = new List<AutocompleteItem>();
            foreach (Ruta item in lstVehiculos)
            {
                lst.Add(new AutocompleteItem(item.rut_nombre + "" + item.rut_origen + "" + item.rut_destino, item.rut_id, item.rut_nombre));
            }
            return lst;
        }

        #endregion
        #region Tipo Notacre
        [WebMethod]
        public List<Tiponc> GetTipoNotaDebObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("tnc_tclipro={1} and tnc_cuentand>0 and  (tnc_id like {0} or tnc_nombre like {0})", "%" + filterKey + "%", Constantes.cCliente);
            List<Tiponc> lst = TiponcBLL.GetAllTop(whereparams, "tnc_nombre", 5);
            return lst;
        }


        [WebMethod]
        public List<Tiponc> GetTipoNotaCreObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("tnc_tclipro={1} and tnc_cuentanc>0 and  (tnc_id like {0} or tnc_nombre like {0})", "%" + filterKey + "%", Constantes.cCliente);
            List<Tiponc> lst = TiponcBLL.GetAllTop(whereparams, "tnc_nombre", 5);
            return lst;
        }

        [WebMethod]
        public List<Tiponc> GetTipoNotaDebProObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("tnc_tclipro={1} and tnc_cuentand>0 and  (tnc_id like {0} or tnc_nombre like {0})", "%" + filterKey + "%", Constantes.cProveedor);
            List<Tiponc> lst = TiponcBLL.GetAllTop(whereparams, "tnc_nombre", 5);
            return lst;
        }


        [WebMethod]
        public List<Tiponc> GetTipoNotaCreProObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("tnc_tclipro={1} and tnc_cuentanc>0 and  (tnc_id like {0} or tnc_nombre like {0})", "%" + filterKey + "%", Constantes.cProveedor);
            List<Tiponc> lst = TiponcBLL.GetAllTop(whereparams, "tnc_nombre", 5);
            return lst;
        }




        [WebMethod]
        public List<AutocompleteItem> GetTipoNotacre(string filterKey)
        {
            List<Tiponc> lsttpa = GetTipoNotaCreObj(filterKey);
            List<AutocompleteItem> lst = new List<AutocompleteItem>();
            foreach (Tiponc item in lsttpa)
            {
                lst.Add(new AutocompleteItem(item.tnc_nombre, item.tnc_id, item.tnc_nombre));
            }
            return lst;
        }


        #endregion

        #region Chofer
        /*[WebMethod]
        public List<Chofer> GetChoferObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("cho_nrolicencia like {0} or per_nombres like {0} or per_apellidos like {0} ", "%" + filterKey + "%");
            List<Chofer> lst = ChoferBLL.GetAllTop(whereparams, "cho_nrolicencia", 5);
            return lst;
        }

        [WebMethod]
        public List<AutocompleteItem> GetChofer(string filterKey)
        {
            List<Chofer> lstVehiculos = GetChoferObj(filterKey);
            List<AutocompleteItem> lst = new List<AutocompleteItem>();
            foreach (Chofer item in lstVehiculos)
            {
                lst.Add(new AutocompleteItem(item.cho_nrolicencia + "" + item.cho_persona, item.cho_persona.ToString(), item.cho_nrolicencia));
            }
            return lst;
        }*/
        #endregion

        #region Tipo Pago

        [WebMethod]
        public List<Tipopago> GetTipoPagoObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("(tpa_id like {0} or tpa_nombre like {0}) and tpa_estado=1", "%" + filterKey + "%");
            List<Tipopago> lst = TipopagoBLL.GetAllTop(whereparams, "tpa_nombre", 5);
            return lst;
        }

        [WebMethod]
        public List<Tipopago> GetTipoPagoCliObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("(tpa_id like {0} or tpa_nombre like {0}) and tpa_tclipro={1} and tpa_estado=1", "%" + filterKey + "%", Constantes.cCliente);
            List<Tipopago> lst = TipopagoBLL.GetAllTop(whereparams, "tpa_nombre", 5);
            return lst;

        }

        [WebMethod]
        public List<Tipopago> GetTipoPagoProvObj(string filterKey)
        {
            WhereParams whereparams = new WhereParams("(tpa_id like {0} or tpa_nombre like {0}) and tpa_tclipro={1} and tpa_estado=1", "%" + filterKey + "%", Constantes.cProveedor);
            List<Tipopago> lst = TipopagoBLL.GetAllTop(whereparams, "tpa_nombre", 5);
            return lst;

        }
        [WebMethod]
        public List<AutocompleteItem> GetTipoPago(string filterKey)
        {
            List<Tipopago> lsttpa = GetTipoPagoObj(filterKey);
            List<AutocompleteItem> lst = new List<AutocompleteItem>();
            foreach (Tipopago item in lsttpa)
            {
                lst.Add(new AutocompleteItem(item.tpa_nombre, item.tpa_id, item.tpa_nombre));
            }
            return lst;
        }


        #endregion
        #endregion
        #region Interfaces Comunes

        #region Preview Factura - Guia

        [WebMethod]
        public string ViewFacturaGuia(object objeto)
        {

            Comprobante comp = new Comprobante(objeto);

            comp = Packages.FAC.getFacturaGuia(comp.com_codigo);

            Persona persona = new Persona();
            persona.per_empresa = comp.com_empresa;
            persona.per_empresa_key = comp.com_empresa;
            if (comp.com_codclipro.HasValue)
            {
                persona.per_codigo = comp.com_codclipro.Value;
                persona.per_codigo_key = comp.com_codclipro.Value;
                persona = PersonaBLL.GetByPK(persona);
            }


            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span5\">");

            HtmlTable td = new HtmlTable();
            td.CreteEmptyTable(4, 2);
            td.rows[0].cells[0].valor = "FECHA:";
            td.rows[0].cells[1].valor = comp.com_fecha.ToShortDateString();
            td.rows[1].cells[0].valor = "CLIENTE:";
            td.rows[1].cells[1].valor = (string.IsNullOrEmpty(persona.per_razon) ? persona.per_apellidos + " " + persona.per_nombres : persona.per_razon);
            td.rows[2].cells[0].valor = "RUC:";
            td.rows[2].cells[1].valor = persona.per_ciruc;
            td.rows[3].cells[0].valor = "DIRECCION:";
            td.rows[3].cells[1].valor = persona.per_direccion;

            html.Append(td.ToString());
            html.AppendLine(" </div><!--span5-->");
            html.AppendLine("<div class=\"span5\">");


            HtmlTable td1 = new HtmlTable();
            td1.CreteEmptyTable(4, 2);
            td1.rows[0].cells[0].valor = "DESTINATARIO:";
            td1.rows[0].cells[1].valor = comp.ccomenv.cenv_nombres_des;
            td1.rows[1].cells[0].valor = "DIRECCION:";
            td1.rows[1].cells[1].valor = comp.ccomenv.cenv_direccion_des;
            td1.rows[2].cells[0].valor = "CIUDAD:";
            td1.rows[2].cells[1].valor = comp.ccomenv.cenv_rutadestino;
            td1.rows[3].cells[0].valor = "REMITENTE:";
            td1.rows[3].cells[1].valor = comp.ccomenv.cenv_nombres_rem;

            html.Append(td1.ToString());
            html.AppendLine(" </div><!--span5-->");
            html.AppendLine("</div><!--row-fluid-->");

            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span8\">");

            HtmlTable tdatos = new HtmlTable();

            tdatos.id = "tdatos_P2";
            tdatos.invoice = true;

            tdatos.AddColumn("Cant.", "", "", "");
            tdatos.AddColumn("Contenido", "", "", "");
            tdatos.AddColumn("V. Unit.", "", "", "");
            tdatos.AddColumn("V. TOTAL", "", "");

            foreach (Dcomdoc item in comp.ccomdoc.detalle)
            {
                HtmlRow row = new HtmlRow();
                row.cells.Add(new HtmlCell { valor = item.ddoc_cantidad });
                row.cells.Add(new HtmlCell { valor = item.ddoc_productonombre + " " + item.ddoc_observaciones });
                row.cells.Add(new HtmlCell { valor = item.ddoc_precio.ToString("0.00") });
                row.cells.Add(new HtmlCell { valor = item.ddoc_total.ToString("0.00") });
                //  row.cells.Add(new HtmlCell { valor = item.cenv_total });
                tdatos.AddRow(row);
            }
            html.Append(tdatos.ToString());
            html.AppendLine(" </div><!--span8-->");
            html.AppendLine("<div class=\"span3\">");

            HtmlTable tdpie = new HtmlTable();
            tdpie.CreteEmptyTable(5, 2);
            tdpie.rows[0].cells[0].valor = "SUBTOTAL 0%";
            tdpie.rows[0].cells[1].valor = Formatos.CurrencyFormat(comp.total.tot_subtot_0);
            tdpie.rows[1].cells[0].valor = "SUBTOTAL 12%";
            tdpie.rows[1].cells[1].valor = Formatos.CurrencyFormat(comp.total.tot_subtotal);
            decimal sub = comp.total.tot_subtot_0 + comp.total.tot_subtotal;
            tdpie.rows[2].cells[0].valor = "SUBTOTAL";
            tdpie.rows[2].cells[1].valor = Formatos.CurrencyFormat(sub);
            tdpie.rows[3].cells[0].valor = "IVA 12%";
            tdpie.rows[3].cells[1].valor = Formatos.CurrencyFormat(comp.total.tot_timpuesto);
            tdpie.rows[4].cells[0].valor = "VALOR TOTAL";
            tdpie.rows[4].cells[1].clase = Css.totalamountssmall;
            tdpie.rows[4].cells[1].valor = Formatos.CurrencyFormat(comp.total.tot_total);

            html.Append(tdpie.ToString());
            html.AppendLine(" </div><!--span3-->");
            html.AppendLine("</div><!--row-fluid-->");

            string[] retorno = new string[2];
            retorno[0] = comp.com_doctran;
            retorno[1] = html.ToString();
            return new JavaScriptSerializer().Serialize(retorno);

            //html.AppendLine(tdatos1.ToString());



            //return html.ToString();
        }

        public string ViewRecibo(Comprobante comp)
        {

            comp.recibos = DreciboBLL.GetAll(new WhereParams("dfp_empresa={0} and dfp_comprobante={1}", comp.com_empresa, comp.com_codigo), "dfp_secuencia");

            Persona persona = new Persona();
            persona.per_empresa = comp.com_empresa;
            persona.per_empresa_key = comp.com_empresa;
            if (comp.com_codclipro.HasValue)
            {
                persona.per_codigo = comp.com_codclipro.Value;
                persona.per_codigo_key = comp.com_codclipro.Value;
                persona = PersonaBLL.GetByPK(persona);
            }

            comp.total = new Total();
            comp.total.tot_empresa = comp.com_empresa;
            comp.total.tot_empresa_key = comp.com_empresa;
            comp.total.tot_comprobante = comp.com_codigo;
            comp.total.tot_comprobante_key = comp.com_codigo;
            comp.total = TotalBLL.GetByPK(comp.total);


            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span5\">");

            HtmlTable td = new HtmlTable();
            td.CreteEmptyTable(4, 2);
            td.rows[0].cells[0].valor = "FECHA:";
            td.rows[0].cells[1].valor = comp.com_fecha.ToShortDateString();
            td.rows[1].cells[0].valor = "CLIENTE:";
            td.rows[1].cells[1].valor = (string.IsNullOrEmpty(persona.per_razon) ? persona.per_apellidos + " " + persona.per_nombres : persona.per_razon);
            td.rows[2].cells[0].valor = "RUC:";
            td.rows[2].cells[1].valor = persona.per_ciruc;
            td.rows[3].cells[0].valor = "DIRECCION:";
            td.rows[3].cells[1].valor = persona.per_direccion;

            html.Append(td.ToString());
            html.AppendLine(" </div><!--span5-->");
            html.AppendLine("<div class=\"span5\">");


            HtmlTable td1 = new HtmlTable();
            td1.CreteEmptyTable(1, 2);
            td1.rows[0].cells[0].valor = "CONCEPTO:";
            td1.rows[0].cells[1].valor = comp.com_concepto;
            html.Append(td1.ToString());
            html.AppendLine(" </div><!--span5-->");
            html.AppendLine("</div><!--row-fluid-->");

            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span8\">");

            HtmlTable tdatos = new HtmlTable();

            tdatos.id = "tdatos_P2";
            tdatos.invoice = true;

            tdatos.AddColumn("Tipo.", "", "", "");
            tdatos.AddColumn("Banco", "", "", "");
            tdatos.AddColumn("Documento.", "", "", "");
            tdatos.AddColumn("Valor", "", "");



            foreach (Drecibo item in comp.recibos)
            {
                HtmlRow row = new HtmlRow();
                row.cells.Add(new HtmlCell { valor = item.dfp_tipopagonombre });
                row.cells.Add(new HtmlCell { valor = item.dfp_banco });
                row.cells.Add(new HtmlCell { valor = item.dfp_nro_documento });
                row.cells.Add(new HtmlCell { valor = item.dfp_monto.ToString("0.00") });
                //  row.cells.Add(new HtmlCell { valor = item.cenv_total });
                tdatos.AddRow(row);
            }
            html.Append(tdatos.ToString());
            html.AppendLine(" </div><!--span8-->");
            html.AppendLine("<div class=\"span3\">");

            HtmlTable tdpie = new HtmlTable();
            tdpie.CreteEmptyTable(1, 2);
            tdpie.rows[0].cells[0].valor = "TOTAL";
            tdpie.rows[0].cells[1].clase = Css.totalamountssmall;
            tdpie.rows[0].cells[1].valor = Formatos.CurrencyFormat(comp.total.tot_total);

            html.Append(tdpie.ToString());
            html.AppendLine(" </div><!--span3-->");
            html.AppendLine("</div><!--row-fluid-->");

            string[] retorno = new string[2];
            retorno[0] = comp.com_doctran;
            retorno[1] = html.ToString();
            return new JavaScriptSerializer().Serialize(retorno);

            //html.AppendLine(tdatos1.ToString());



            //return html.ToString();
        }

        [WebMethod]
        public string ViewComprobante(object objeto)
        {

            Comprobante comp = new Comprobante(objeto);

            comp.com_codigo_key = comp.com_codigo;
            comp.com_empresa_key = comp.com_empresa;
            comp = ComprobanteBLL.GetByPK(comp);
            if (comp.com_tipodoc == Constantes.cFactura.tpd_codigo || comp.com_tipodoc == Constantes.cGuia.tpd_codigo)
            {
                return ViewFacturaGuia(objeto);
            }
            if (comp.com_tipodoc == Constantes.cRecibo.tpd_codigo)
            {
                return ViewRecibo(comp);
            }
            return "";
        }

        #endregion

        #region Datos Comprobante
        [WebMethod]
        public string CrearComprobante(object objeto)
        {
            if (objeto != null)
            {

                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object empresa = null;
                object usuario = null;
                object tipodoc = null;

                tmp.TryGetValue("empresa", out empresa);
                tmp.TryGetValue("usuario", out usuario);
                tmp.TryGetValue("tipodoc", out tipodoc);

                Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = int.Parse(empresa.ToString()), uxe_empresa_key = int.Parse(empresa.ToString()), uxe_usuario = usuario.ToString(), uxe_usuario_key = usuario.ToString() });

                Tipodoc tdoc = TipodocBLL.GetByPK(new Tipodoc { tpd_codigo = int.Parse(tipodoc.ToString()), tpd_codigo_key = int.Parse(tipodoc.ToString()) });


                TipoDocOpc tdocopc = new JavaScriptSerializer().Deserialize<TipoDocOpc>(tdoc.tpd_opciones);
                if (tdocopc == null)
                    tdocopc = new TipoDocOpc();

                StringBuilder html = new StringBuilder();
                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span11 popupcontiner\">");
                HtmlTable td = new HtmlTable();

                if (tdocopc.numeromanual)
                    td.CreteEmptyTable(5, 2);
                else
                    td.CreteEmptyTable(4, 2);
                td.rows[0].cells[0].valor = "Sigla";
                td.rows[0].cells[1].valor = new Select { id = "cmbSIGLA_P", diccionario = Dictionaries.GetDtipodocByTipodoc(int.Parse(tipodoc.ToString())), obligatorio = true, clase = Css.large }.ToString();
                td.rows[1].cells[0].valor = "Almacen";
                td.rows[1].cells[1].valor = new Select { id = "cmbALMACEN_P", diccionario = Dictionaries.GetIDAlmacen(), valor = uxe.uxe_almacen.Value, clase = Css.large }.ToString();
                if (tdoc.tpd_modulo == Constantes.cInventario.mod_codigo)
                {
                    td.rows[2].cells[0].valor = "Bodega";
                    td.rows[2].cells[1].valor = new Select { id = "cmbBODEGA_P", diccionario = new Dictionary<string, string>(), clase = Css.large }.ToString();
                }
                else
                {
                    td.rows[2].cells[0].valor = "Punto Venta";
                    td.rows[2].cells[1].valor = new Select { id = "cmbPVENTA_P", diccionario = new Dictionary<string, string>(), clase = Css.large }.ToString();
                }


                if (tdocopc.numeromanual)
                {
                    td.rows[3].cells[0].valor = "Número";
                    td.rows[3].cells[1].valor = new Input { id = "txtNUMERO_P", valor = "", clase = Css.medium }.ToString();

                    td.rows[4].cells[0].valor = "Fcha/Hora";
                    td.rows[4].cells[1].valor = new Input { id = "txtFECHA_P", datepicker = true, datetimevalor = DateTime.Now, clase = Css.small, habilitado = tdocopc.lockfecha.HasValue ? !tdocopc.lockfecha.Value : true }.ToString() + " " + new Input { id = "txtHORA_P", valor = DateTime.Now.ToString("HH:mm"), clase = Css.mini, habilitado = false };
                }
                else
                {

                    td.rows[3].cells[0].valor = "Fcha/Hora";
                    td.rows[3].cells[1].valor = new Input { id = "txtFECHA_P", datepicker = true, datetimevalor = DateTime.Now, clase = Css.small, habilitado = tdocopc.lockfecha.HasValue ? !tdocopc.lockfecha.Value : true }.ToString() + " " + new Input { id = "txtHORA_P", valor = DateTime.Now.ToString("HH:mm"), clase = Css.mini, habilitado = false };
                }

                html.AppendLine(td.ToString());
                html.AppendLine(new Input { id = "txtTIPODOC_P", valor = tipodoc.ToString(), visible = false }.ToString());
                html.AppendLine(" </div><!--span11-->");
                html.AppendLine("</div><!--row-fluid-->");


                /*html.AppendLine(new Select { id = "cmbSIGLA_P", etiqueta = "Sigla", diccionario = Dictionaries.GetDtipodocByTipodoc(int.Parse(tipodoc.ToString())), obligatorio = true }.ToString());
                html.AppendLine(new Select { id = "cmbALMACEN_P", diccionario = Dictionaries.GetIDAlmacen(), etiqueta = "Almacen", valor = uxe.uxe_almacen.Value,  clase = Css.large }.ToString());
                html.AppendLine(new Select { id = "cmbPVENTA_P", etiqueta = "Punto Venta", diccionario = new Dictionary<string, string>(), clase = Css.large  }.ToString());
                html.AppendLine(new Input { id = "txtFECHA_P", etiqueta = "Fecha", datepicker = true, datetimevalor = DateTime.Now, clase = Css.small }.ToString() + " " + new Input { id = "txtHORA_P", valor = DateTime.Now.ToString("HH:mm"), clase= Css.mini });
                html.AppendLine(new Input { id = "txtTIPODOC_P", valor = tipodoc.ToString(), visible = false }.ToString());*/
                return html.ToString();
            }
            return "";
        }

        #endregion



        #region despachar

        [WebMethod]
        public string CrearDespachar(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            comprobante.ccomenv = new Ccomenv();
            comprobante.ccomenv.cenv_comprobante = comprobante.com_codigo;
            comprobante.ccomenv.cenv_empresa = comprobante.com_empresa;
            comprobante.ccomenv.cenv_comprobante_key = comprobante.com_codigo;
            comprobante.ccomenv.cenv_empresa_key = comprobante.com_empresa;

            comprobante.ccomenv = CcomenvBLL.GetByPK(comprobante.ccomenv);

            bool habilitado = comprobante.ccomenv.cenv_despachado_ret.HasValue ? (comprobante.ccomenv.cenv_despachado_ret.Value == 1 ? false : true) : true;

            StringBuilder htmlr = new StringBuilder();
            htmlr.AppendLine("<div class=\"row-fluid\">");
            htmlr.AppendLine("<div class=\"span12 popupcontiner\">");
            HtmlTable tdret = new HtmlTable();
            tdret.CreteEmptyTable(7, 2);
            tdret.rows[0].cells[0].valor = "Numero";
            tdret.rows[0].cells[1].valor = new Input { id = "txtDOCTRANRED", clase = Css.large, habilitado = false, valor = comprobante.com_doctran }.ToString() + new Input { id = "txtCODIGOCOMPRED", clase = Css.large, habilitado = false, valor = comprobante.com_codigo, visible = false }.ToString();
            tdret.rows[1].cells[0].valor = "Retira:";
            tdret.rows[1].cells[1].valor = new Input { id = "txtIDRED", clase = Css.medium, visible = false, valor = comprobante.ccomenv.cenv_retira }.ToString() + " " + new Input { id = "txtNOMBRESRED", autocomplete = "GetPersonaObj", clase = Css.xlarge, obligatorio = true, valor = comprobante.ccomenv.cenv_nombres_ret, habilitado = habilitado }.ToString() + new Input { id = "txtCODRED", visible = false }.ToString();
            tdret.rows[2].cells[0].valor = "Cedula";
            tdret.rows[2].cells[1].valor = new Input { id = "txtCIRUCRED", clase = Css.xlarge, valor = comprobante.ccomenv.cenv_ciruc_ret, habilitado = habilitado }.ToString();
            tdret.rows[3].cells[0].valor = "Teléfono";
            tdret.rows[3].cells[1].valor = new Input { id = "txtTELEFONORED", clase = Css.xlarge, valor = comprobante.ccomenv.cenv_telefono_ret, habilitado = habilitado }.ToString();
            tdret.rows[4].cells[0].valor = "Direccion";
            tdret.rows[4].cells[1].valor = new Input { id = "txtDIRECCIONRED", clase = Css.xlarge, valor = comprobante.ccomenv.cenv_direccion_ret, habilitado = habilitado }.ToString();
            tdret.rows[5].cells[0].valor = "Fecha/Hora:";
            tdret.rows[5].cells[1].valor = new Input { id = "txtFECHARED", datepicker = true, datetimevalor = (comprobante.ccomenv.cenv_fecha_ret.HasValue) ? comprobante.ccomenv.cenv_fecha_ret.Value : DateTime.Now, clase = Css.small, obligatorio = true, habilitado = habilitado }.ToString() + " " + new Input { id = "txtHORARED", clase = Css.mini, valor = (comprobante.ccomenv.cenv_fecha_ret.HasValue) ? comprobante.ccomenv.cenv_fecha_ret.Value.Hour + ":" + comprobante.ccomenv.cenv_fecha_ret.Value.Minute : "" + DateTime.Now.Hour + ":" + DateTime.Now.Minute, obligatorio = true, habilitado = habilitado }.ToString();
            tdret.rows[6].cells[0].valor = "Observacion";
            tdret.rows[6].cells[1].valor = new Textarea { id = "txtOBSERVACIONRED", clase = Css.xlarge, valor = comprobante.ccomenv.cenv_observaciones_ret, habilitado = habilitado }.ToString();
            htmlr.AppendLine(tdret.ToString());
            htmlr.AppendLine(" </div><!--span6-->");

            return htmlr.ToString();

        }



        //metodo que actualiza el despacho

        [WebMethod]
        public string ActualizarDespachar(object objeto)
        {


            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);
            Ccomenv c = new Ccomenv(objeto);



            comprobante.ccomenv = new Ccomenv();
            comprobante.ccomenv.cenv_comprobante = comprobante.com_codigo;
            comprobante.ccomenv.cenv_empresa = comprobante.com_empresa;
            comprobante.ccomenv.cenv_comprobante_key = comprobante.com_codigo;
            comprobante.ccomenv.cenv_empresa_key = comprobante.com_empresa;

            comprobante.ccomenv = CcomenvBLL.GetByPK(comprobante.ccomenv);
            comprobante.ccomenv.cenv_comprobante = comprobante.com_codigo;
            comprobante.ccomenv.cenv_empresa = comprobante.com_empresa;
            comprobante.ccomenv.cenv_comprobante_key = comprobante.com_codigo;
            comprobante.ccomenv.cenv_empresa_key = comprobante.com_empresa;


            comprobante.ccomenv.cenv_empresa_ret = c.cenv_empresa_ret;
            comprobante.ccomenv.cenv_retira = c.cenv_retira;
            comprobante.ccomenv.cenv_ciruc_ret = c.cenv_ciruc_ret;
            comprobante.ccomenv.cenv_nombres_ret = c.cenv_nombres_ret;
            comprobante.ccomenv.cenv_direccion_ret = c.cenv_direccion_ret;
            comprobante.ccomenv.cenv_telefono_ret = c.cenv_telefono_ret;
            comprobante.ccomenv.cenv_fecha_ret = c.cenv_fecha_ret;
            comprobante.ccomenv.cenv_observaciones_ret = c.cenv_observaciones_ret;
            comprobante.ccomenv.cenv_despachado_ret = 1;

            if (CcomenvBLL.Update(comprobante.ccomenv) > 0)
                return "OK";
            else
                return "ERROR";
        }

        //fin del metodo Despachar


        #endregion 

        #region Asignar Socio

        [WebMethod]
        public string AsingarSocio(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            comprobante.ccomenv = new Ccomenv();
            comprobante.ccomenv.cenv_comprobante = comprobante.com_codigo;
            comprobante.ccomenv.cenv_empresa = comprobante.com_empresa;
            comprobante.ccomenv.cenv_comprobante_key = comprobante.com_codigo;
            comprobante.ccomenv.cenv_empresa_key = comprobante.com_empresa;

            comprobante.ccomenv = CcomenvBLL.GetByPK(comprobante.ccomenv);

            bool habilitado = true;

            StringBuilder htmlr = new StringBuilder();
            htmlr.AppendLine("<div class=\"row-fluid\">");
            htmlr.AppendLine("<div class=\"span12 popupcontiner\">");
            HtmlTable tdret = new HtmlTable();
            tdret.CreteEmptyTable(2, 2);
            tdret.rows[0].cells[0].valor = "Numero";
            tdret.rows[0].cells[1].valor = new Input { id = "txtDOCTRANASO", clase = Css.large, habilitado = false, valor = comprobante.com_doctran }.ToString() + new Input { id = "txtCODIGOCOMPASO", clase = Css.large, habilitado = false, valor = comprobante.com_codigo, visible = false }.ToString();
            tdret.rows[1].cells[0].valor = "Socio:";
            tdret.rows[1].cells[1].valor = new Select { id = "cmbSOCIOASO", clase = Css.large, diccionario = Dictionaries.GetSocios(), valor = comprobante.ccomenv.cenv_socio, withempty = true, habilitado = habilitado }.ToString();
            htmlr.AppendLine(tdret.ToString());
            htmlr.AppendLine(" </div><!--span6-->");

            return htmlr.ToString();

        }



        //metodo que actualiza el despacho

        [WebMethod]
        public string ActualizarAsignarSocio(object objeto)
        {


            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);
            Ccomenv c = new Ccomenv(objeto);



            comprobante.ccomenv = new Ccomenv();
            comprobante.ccomenv.cenv_comprobante = comprobante.com_codigo;
            comprobante.ccomenv.cenv_empresa = comprobante.com_empresa;
            comprobante.ccomenv.cenv_comprobante_key = comprobante.com_codigo;
            comprobante.ccomenv.cenv_empresa_key = comprobante.com_empresa;

            comprobante.ccomenv = CcomenvBLL.GetByPK(comprobante.ccomenv);
            comprobante.ccomenv.cenv_comprobante = comprobante.com_codigo;
            comprobante.ccomenv.cenv_empresa = comprobante.com_empresa;
            comprobante.ccomenv.cenv_comprobante_key = comprobante.com_codigo;
            comprobante.ccomenv.cenv_empresa_key = comprobante.com_empresa;

            comprobante.ccomenv.cenv_socio = c.cenv_socio;

            
            if (CcomenvBLL.Update(comprobante.ccomenv) > 0)
                return "OK";
            else
                return "ERROR";
        }

        //fin del metodo Despachar


        #endregion 

        #region Asignar Factura Planilla

        [WebMethod]
        public string AsignarFacturaPlanilla(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            Comprobante fac = new Comprobante();
            bool habilitado = true;

            List<Planillacomprobante> lst = PlanillacomprobanteBLL.GetAll(new WhereParams("pco_comprobante_pla={0}", comprobante.com_codigo), "");
            if (lst.Count > 0)
            {
                fac.com_empresa = lst[0].pco_empresa;
                fac.com_empresa_key = lst[0].pco_empresa;
                fac.com_codigo = lst[0].pco_comprobante_fac;
                fac.com_codigo_key = lst[0].pco_comprobante_fac;
                fac = ComprobanteBLL.GetByPK(fac);
                habilitado = false;
            }

            StringBuilder htmlr = new StringBuilder();
            htmlr.AppendLine("<div class=\"row-fluid\">");
            htmlr.AppendLine("<div class=\"span12 popupcontiner\">");
            HtmlTable tdret = new HtmlTable();
            tdret.CreteEmptyTable(2, 2);
            tdret.rows[0].cells[0].valor = "Planilla:";
            tdret.rows[0].cells[1].valor = new Input { id = "txtDOCTRANPLA_PF", clase = Css.large, habilitado = false, valor = comprobante.com_doctran }.ToString() + new Input { id = "txtCODIGOPLA_PF", clase = Css.large, habilitado = false, valor = comprobante.com_codigo, visible = false }.ToString() + new Input { id = "txtCODIGOFAC_PF", valor = fac.com_codigo, visible = false }.ToString();
            tdret.rows[1].cells[0].valor = "Factura:";
            tdret.rows[1].cells[1].valor = new Input { id = "txtDOCTRANFAC_PF", autocomplete = "GetFacturaObj", clase = Css.large, habilitado = habilitado, obligatorio = true, valor = fac.com_doctran }.ToString() + new Boton { small = true, id = "btnRefFac", tooltip = "Recrear Factura", clase = "iconsweets-refresh", click = "RefFacPla()" }.ToString() + new Boton { small = true, id = "btnDelFac", tooltip = "Quitar Factura", clase = "iconsweets-trashcan", click = "DelFacPla()" }.ToString();
            htmlr.AppendLine(tdret.ToString());
            htmlr.AppendLine(new Input { id = "txtPERMITE_PF", visible = false, valor = habilitado }.ToString());
            htmlr.AppendLine(" </div><!--span6-->");

            return htmlr.ToString();

        }



        //metodo que actualiza el despacho

        [WebMethod]
        public string SaveAsignarFacturaPlanilla(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object doctranfac = null;
            tmp.TryGetValue("doctranfac", out doctranfac);

            List<vComprobante> lst = vComprobanteBLL.GetAll(new WhereParams("c.com_doctran ={0}", doctranfac.ToString()), "");
            if (lst.Count > 0)
            {
                Planillacomprobante plc = new Planillacomprobante(objeto);
                plc.pco_comprobante_fac = lst[0].codigo.Value;
                if (PlanillacomprobanteBLL.Insert(plc) > 0)
                    return "OK";
            }

            return "ERRROR";
        }



        [WebMethod]
        public string DeleteAsignarFacturaPlanilla(object objeto)
        {
            Planillacomprobante plc = new Planillacomprobante(objeto);
            plc.pco_empresa_key = plc.pco_empresa;
            plc.pco_comprobante_pla_key = plc.pco_comprobante_pla;
            plc.pco_comprobante_fac_key = plc.pco_comprobante_fac;

            PlanillacomprobanteBLL.Delete(plc);
            return "OK";


        }


        [WebMethod]
        public string RecreateFacturaPlanilla(object objeto)
        {
            Planillacomprobante plc = new Planillacomprobante(objeto);

            Comprobante planilla = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = plc.pco_empresa, com_empresa_key = plc.pco_empresa, com_codigo = plc.pco_comprobante_pla, com_codigo_key = plc.pco_comprobante_pla });
            planilla.total = TotalBLL.GetByPK(new Total { tot_empresa = planilla.com_empresa, tot_empresa_key = planilla.com_empresa, tot_comprobante = planilla.com_codigo, tot_comprobante_key = planilla.com_codigo });

            Comprobante factura = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = plc.pco_empresa, com_empresa_key = plc.pco_empresa, com_codigo = plc.pco_comprobante_fac, com_codigo_key = plc.pco_comprobante_fac });
            factura.total = TotalBLL.GetByPK(new Total { tot_empresa = factura.com_empresa, tot_empresa_key = factura.com_empresa, tot_comprobante = factura.com_codigo, tot_comprobante_key = factura.com_codigo });
            factura.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = factura.com_empresa, cdoc_empresa_key = factura.com_empresa, cdoc_comprobante = factura.com_codigo, cdoc_comprobante_key = factura.com_codigo });
            factura.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa ={0} and ddoc_comprobante={1}", factura.com_empresa, factura.com_codigo), "");
            factura.ccomdoc.detalle[0].ddoc_precio = planilla.total.tot_subtot_0 + planilla.total.tot_subtotal;
            factura.ccomdoc.detalle[0].ddoc_total = planilla.total.tot_subtot_0 + planilla.total.tot_subtotal;



            string[] arrayporcseguro = Constantes.cPorcSeguro.Split('|');
            decimal valorporcseguro = 0;

            if (arrayporcseguro.Length > 0)
                valorporcseguro = decimal.Parse(arrayporcseguro[0]);
            if (arrayporcseguro.Length > 1)
            {
                if (planilla.com_almacen.HasValue && planilla.com_pventa.HasValue)
                {
                    for (int i = 1; i < arrayporcseguro.Length; i++)
                    {
                        string[] arrayvalores = arrayporcseguro[i].Split(';');

                        if (planilla.com_almacen.Value.ToString() == arrayvalores[0] && planilla.com_pventa.Value.ToString() == arrayvalores[1])
                        {
                            valorporcseguro = decimal.Parse(arrayvalores[2]);
                            break;
                        }

                    }
                }

            }

            decimal valordeclarado = 0;
            if (planilla.total.tot_tseguro.HasValue)
                valordeclarado = (planilla.total.tot_tseguro.Value * 100) / valorporcseguro;

            factura.total.tot_subtot_0 = planilla.total.tot_subtot_0;
            factura.total.tot_transporte = planilla.total.tot_transporte;
            factura.total.tot_vseguro = planilla.total.tot_vseguro;
            factura.total.tot_subtotal = planilla.total.tot_subtotal;
            factura.total.tot_total = planilla.total.tot_total;

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                factura.com_estado = Constantes.cEstadoGrabado;
                factura.com_empresa_key = factura.com_empresa;
                factura.com_codigo = factura.com_codigo;
                ComprobanteBLL.Update(transaction, factura);

                factura.ccomdoc.detalle[0].ddoc_empresa_key = factura.ccomdoc.detalle[0].ddoc_empresa;
                factura.ccomdoc.detalle[0].ddoc_comprobante_key = factura.ccomdoc.detalle[0].ddoc_comprobante;
                factura.ccomdoc.detalle[0].ddoc_secuencia_key = factura.ccomdoc.detalle[0].ddoc_secuencia;
                DcomdocBLL.Update(transaction, factura.ccomdoc.detalle[0]);

                factura.total.tot_empresa_key = factura.total.tot_empresa;
                factura.total.tot_comprobante_key = factura.total.tot_comprobante;
                TotalBLL.Update(transaction, factura.total);

                transaction.Commit();

                FAC.account_factura(factura);

                return "OK";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }




        }



        //fin del metodo Despachar


        #endregion 


        public bool ComprobanteCerrado(Comprobante comprobante)
        {
            string bloqueoanular = Constantes.GetParameter("bloqueoanular");
            bool bloquear = true;
            if (!string.IsNullOrEmpty(bloqueoanular))
                bool.TryParse(bloqueoanular, out bloquear);

            bool cerrado = false;
            if (bloquear)
            {

                if (comprobante.com_tipodoc == Constantes.cFactura.tpd_codigo)
                {
                    DateTime hoy = DateTime.Now;
                    if (hoy.Month > comprobante.com_fecha.Month)
                    {
                        if (hoy.Month == (comprobante.com_fecha.Month + 1))
                        {
                            if (hoy.Day > 18)
                            {
                                cerrado = true;
                            }
                        }
                        else
                        {
                            cerrado = true;
                        }
                    }
                }
            }
            return cerrado;
        }

        #region Anular Comprobantes

        [WebMethod]
        public string AnularComprobante(object objeto)
        {
            Usuario usr = new Usuario(objeto);
            usr.usr_id_key = usr.usr_id;
            usr = UsuarioBLL.GetByPK(usr);

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);
            ///VERIFICAR SI EL COMPROBANTE CORRESPONDE AL USUARIO O ES EL ADMINISTRADOR

            bool permite = true;
            string mensaje = "";

            if (!Contabilidad.PeriodoIsOpen(comprobante.com_fecha.Year,comprobante.com_fecha.Month,usr.usr_id))
            {
                permite = false;
                mensaje = "NO SE PUEDE ANULAR, EL COMPROBANTE PERTENECE A UN PERIODO Y MES CERRADO...";
            }




            //if (comprobante.crea_usr != usr.usr_id)
            //{
            //    if (usr.usr_perfil != Constantes.cPerfilAdministrador)
            //    {
            //        permite = false;
            //        mensaje = "NO POSEE AUTORIZACIÓN PARA ANULAR EL COMPROBANTE";
            //    }

            //}

            //if (ComprobanteCerrado(comprobante))
            //{
            //    permite = false;
            //    mensaje = "NO SE PUEDE ANULAR COMPROBANTES DE MESES CERRADOS";
            //}






            StringBuilder htmlr = new StringBuilder();
            htmlr.AppendLine("<div class=\"row-fluid\">");
            htmlr.AppendLine("<div class=\"span12 popupcontiner\">");
            HtmlTable tdret = new HtmlTable();
            tdret.CreteEmptyTable(3, 2);
            tdret.rows[0].cells[0].valor = "Número";
            tdret.rows[0].cells[1].valor = new Input { id = "txtDOCTRANANU", clase = Css.large, habilitado = false, valor = comprobante.com_doctran }.ToString() + new Input { id = "txtCODIGOCOMPANU", clase = Css.large, habilitado = false, valor = comprobante.com_codigo, visible = false }.ToString();
            tdret.rows[1].cells[0].valor = "Observación";
            tdret.rows[1].cells[1].valor = new Textarea { id = "txtOBSERVACIONANU", clase = Css.xlarge, rows = 5, valor = comprobante.com_concepto, habilitado = permite }.ToString();
            tdret.rows[2].cells[0].valor = "";
            tdret.rows[2].cells[1].valor = new Input { id = "txtPERMITEANU", visible = false, valor = permite }.ToString() + ((!permite) ? "<div class='blink_me'>" + mensaje + "</div>" : "AUTORIZADO PARA ANULAR");
            htmlr.AppendLine(tdret.ToString());
            htmlr.AppendLine(" </div><!--span6-->");

            return htmlr.ToString();

        }

        [WebMethod]
        public string SaveAnularComprobante(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante = General.AnulaComprobante(comprobante);

            return "OK";

        }

        #endregion

        [WebMethod]
        public string Informacion(object objeto)
        {


            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);
            List<Comprobante> list = new List<Comprobante>();
            List<vCancelacion> listac = new List<vCancelacion>();

            Auto.actualiza_documentos(comprobante.com_empresa, null, null, null, comprobante.com_codigo, null, null, 0);


            if (comprobante.com_tipodoc == 14 || comprobante.com_tipodoc == 16 || comprobante.com_tipodoc == 15 || comprobante.com_tipodoc == 22)
            {

                return Informacionobl(objeto);

            }
            else
            {

                String doctran = "";
                String hojaruta = "";
                String planillacli = "";
                String factura = "";
                List<Dcancelacion> listc = DcancelacionBLL.GetAll("dca_comprobante_can=" + comprobante.com_codigo, "");
                List<Rutaxfactura> listr = RutaxfacturaBLL.GetAll("rfac_comprobantefac=" + comprobante.com_codigo, "");
                List<Planillacli> listp = PlanillacliBLL.GetAll("plc_comprobante=" + comprobante.com_codigo, "");
                List<Ddocumento> listsd = DdocumentoBLL.GetAll("ddo_comprobante_guia=" + comprobante.com_codigo, "");
                List<Planillacomprobante> listpl = PlanillacomprobanteBLL.GetAll("pco_comprobante_fac=" + comprobante.com_codigo, "");
                if (listc.Count > 0)
                {
                    list = ComprobanteBLL.GetAll("com_codigo=" + listc[0].dca_comprobante, "");
                    for (int i = 0; i < list.Count; i++)
                    {
                        doctran = list[i].com_doctran;
                    }
                }
                if (listr.Count > 0)
                {
                    list = ComprobanteBLL.GetAll("com_codigo=" + listr[0].rfac_comprobanteruta, "");

                    hojaruta = list[0].com_doctran;

                }

                if (listp.Count > 0)
                {
                    list = ComprobanteBLL.GetAll("com_codigo=" + listp[0].plc_comprobante_pla, "");

                    planillacli = list[0].com_doctran;

                }
                if (listsd.Count > 0)
                {
                    factura = listsd[0].ddo_doctran;

                }
                if (listpl.Count > 0)
                {
                    list = ComprobanteBLL.GetAll("com_codigo=" + listpl[0].pco_comprobante_pla, "");

                    planillacli = list[0].com_doctran;

                }
                StringBuilder htmlr = new StringBuilder();

                htmlr.AppendLine("<div class=\"row-fluid\">");
                htmlr.AppendLine("<div class=\"span11\">");

                HtmlTable tdret = new HtmlTable();
                tdret.CreteEmptyTable(4, 2);
                tdret.rows[0].cells[0].valor = "Número";
                tdret.rows[0].cells[1].valor = new Input { id = "txtDOCTRANAINF", clase = Css.large, habilitado = false, valor = comprobante.com_doctran }.ToString();
                tdret.rows[1].cells[0].valor = "Hoja de Ruta";
                tdret.rows[1].cells[1].valor = new Input { id = "txtHOJARUTAINF", clase = Css.large, habilitado = false, valor = hojaruta }.ToString();
                tdret.rows[2].cells[0].valor = "Planilla Cliente";
                tdret.rows[2].cells[1].valor = new Input { id = "txtPLANILLACLINF", clase = Css.large, habilitado = false, valor = planillacli }.ToString();
                tdret.rows[3].cells[0].valor = "Factura";
                tdret.rows[3].cells[1].valor = new Input { id = "txtFACTURAINF", clase = Css.large, habilitado = false, valor = (comprobante.com_tipodoc == 6) ? doctran : factura }.ToString();
                // tdret.rows[3].cells[0].valor = "Documentos:";
                // tdret.rows[3].cells[1].valor = "";
                htmlr.AppendLine(tdret.ToString());





                // table de la informacion 

                //  StringBuilder htmld = new StringBuilder();

                HtmlTable tdred = new HtmlTable();
                tdred.id = "tddatos2";
                tdred.invoice = true;
                tdred.clase = "scrolltable";
                //  tdred.AddColumn("Documento", "", "", "");
                tdred.AddColumn("Guia", "", "", "");
                tdred.AddColumn("Nro", "", "", "");
                tdred.AddColumn("Emision", "", "", "");
                tdred.AddColumn("Recibo", "", "", "");
                tdred.AddColumn("Monto", "", "", "");
                tdred.AddColumn("Cancelado", "", "", "");
                tdred.AddColumn("Saldo", "", "", "");
                tdred.AddColumn("Planilla Socios", "", "", "");
                tdred.AddColumn("Socio", "", "", "");
                tdred.AddColumn("Valor Socio", "", "", "");




                if (comprobante.com_tipodoc == 6 && listc.Count > 0)
                {
                    listac = vCancelacionBLL.GetAllT(new WhereParams("f.com_codigo={0} ", listc[0].dca_comprobante), "");

                }


                else if (comprobante.com_tipodoc == 13 && listsd.Count > 0)
                {
                    listac = vCancelacionBLL.GetAllT(new WhereParams("f.com_codigo={0} and ddo_comprobante_guia = {1}", listsd[0].ddo_comprobante, comprobante.com_codigo), "");

                }
                else
                {
                    listac = vCancelacionBLL.GetAllT(new WhereParams("f.com_codigo=" + comprobante.com_codigo), "");
                }

                foreach (vCancelacion item in listac)
                {

                    HtmlRow row = new HtmlRow();
                    row.markable = true;

                    // row.cells.Add(new HtmlCell { valor = item.ddo_doctran });
                    row.cells.Add(new HtmlCell { valor = item.doctran_guia });
                    row.cells.Add(new HtmlCell { valor = item.ddo_pago });
                    row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi.HasValue ? item.ddo_fecha_emi.Value.Day + "/" + item.ddo_fecha_emi.Value.Month + "/" + item.ddo_fecha_emi.Value.Year : "" });
                    row.cells.Add(new HtmlCell { valor = item.doctran_can });
                    row.cells.Add(new HtmlCell { valor = decimal.Round(item.ddo_monto.Value, 2) });
                    row.cells.Add(new HtmlCell { valor = (item.dca_monto == null) ? 0 : decimal.Round(item.dca_monto.Value, 2) });
                    //row.cells.Add(new HtmlCell { valor = (item.ddo_cancela == null) ? item.ddo_cancela = 0 : decimal.Round(item.ddo_cancela.Value, 2) });
                    row.cells.Add(new HtmlCell { valor = decimal.Round((item.ddo_monto - item.ddo_cancela).Value, 2) });
                    row.cells.Add(new HtmlCell { valor = item.ddo_doctranpla });
                    row.cells.Add(new HtmlCell { valor = (item.nombressocio + " " + item.apellidossocio) });
                    row.cells.Add(new HtmlCell { valor = (item.dca_monto_pla == null) ? item.dca_monto_pla = 0 : decimal.Round(item.dca_monto_pla.Value, 2) });
                    tdred.AddRow(row);

                }


                htmlr.AppendLine(tdred.ToString());

                htmlr.AppendLine("</div> <!--span11-->");
                htmlr.AppendLine("</div> <!--row-fluid-->");



                return htmlr.ToString();
            }

        }
        [WebMethod]

        public string Informacionobl(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);
            List<Comprobante> list = new List<Comprobante>();
            List<vCancelacion> listac = new List<vCancelacion>();

            String obligacion = "";
            String factura = "";
            Decimal valor = 0;

            List<Dcancelacion> listc = DcancelacionBLL.GetAll("dca_comprobante_can=" + comprobante.com_codigo, "");

            List<Ccomdoc> listd = CcomdocBLL.GetAll("cdoc_comprobante=" + comprobante.com_codigo, "");

            List<Dretencion> listr = DretencionBLL.GetAll("drt_comprobante=" + comprobante.com_codigo, "");

            if (listd.Count > 0 && listd[0].cdoc_factura != null)
            {


                list = ComprobanteBLL.GetAll("com_codigo=" + listd[0].cdoc_factura, "");
                obligacion = list[0].com_doctran;

            }

            if (listr.Count > 0)
            {

                factura = listr[0].drt_factura;
                valor = (Decimal)listr[0].drt_valor;

            }

            StringBuilder htmlr = new StringBuilder();

            htmlr.AppendLine("<div class=\"row-fluid\">");
            htmlr.AppendLine("<div class=\"span11\">");
            HtmlTable tdret = new HtmlTable();
            tdret.CreteEmptyTable(4, 2);
            tdret.rows[0].cells[0].valor = "Número";
            tdret.rows[0].cells[1].valor = new Input { id = "txtDOCTRANAINF", clase = Css.large, habilitado = false, valor = comprobante.com_doctran }.ToString();
            tdret.rows[1].cells[0].valor = "Obligacion";
            tdret.rows[1].cells[1].valor = new Input { id = "txtOBLIGACIONINF", clase = Css.large, habilitado = false, valor = obligacion }.ToString();
            tdret.rows[2].cells[0].valor = "Factura  Proveedor";
            tdret.rows[2].cells[1].valor = new Input { id = "txtFACTURAINF", clase = Css.large, habilitado = false, valor = factura }.ToString();
            if (comprobante.com_tipodoc == 16)
            {

                tdret.rows[3].cells[0].valor = "Valor";
                tdret.rows[3].cells[1].valor = new Input { id = "txtVALORINF", clase = Css.large, habilitado = false, valor = decimal.Round(valor, 2) }.ToString();
            }
            else
            {

                tdret.rows[3].cells[0].valor = "Valor";
                tdret.rows[3].cells[1].valor = new Input { id = "txtVALORINF", clase = Css.large, habilitado = false }.ToString();
            }





            htmlr.AppendLine(tdret.ToString());
            HtmlTable tdred = new HtmlTable();
            tdred.id = "tddatos2";
            tdred.invoice = true;
            tdred.clase = "scrolltable";
            //  tdred.AddColumn("Documento", "", "", "");
            tdred.AddColumn("Documento", "", "", "");
            tdred.AddColumn("Nro", "", "", "");
            tdred.AddColumn("Fecha", "", "", "");
            tdred.AddColumn("Pago/Retencion", "", "", "");
            tdred.AddColumn("Monto", "", "", "");
            tdred.AddColumn("Cancelado", "", "", "");
            tdred.AddColumn("Saldo", "", "", "");
            if (comprobante.com_tipodoc == 15 || comprobante.com_tipodoc == 22)
            {
                listac = vCancelacionBLL.GetAllT(new WhereParams("dca_comprobante_can={0} ", comprobante.com_codigo), "");
            }
            else
            {
                listac = vCancelacionBLL.GetAllT(new WhereParams("f.com_codigo={0} ", comprobante.com_codigo), "");
            }

            decimal cancelado = 0;
            foreach (vCancelacion item in listac)
            {

                HtmlRow row = new HtmlRow();
                row.markable = true;

                // row.cells.Add(new HtmlCell { valor = item.ddo_doctran });
                row.cells.Add(new HtmlCell { valor = item.ddo_doctran });
                row.cells.Add(new HtmlCell { valor = item.ddo_pago });
                row.cells.Add(new HtmlCell { valor = item.fecha_can.HasValue ? item.fecha_can.Value.ToShortDateString() : "" });
                row.cells.Add(new HtmlCell { valor = item.doctran_can });
                row.cells.Add(new HtmlCell { valor = decimal.Round(item.ddo_monto.Value, 2), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = item.dca_monto.HasValue ? decimal.Round(item.dca_monto.Value, 2) : 0, clase = Css.right });

                cancelado += (item.dca_monto.HasValue ? item.dca_monto.Value : 0);
                decimal saldo = item.ddo_monto.Value - cancelado;

                row.cells.Add(new HtmlCell { valor = decimal.Round(saldo, 2), clase = Css.right });

                tdred.AddRow(row);

            }

            // tdret.rows[3].cells[0].valor = "Documentos:";
            // tdret.rows[3].cells[1].valor = "";
            htmlr.AppendLine(tdred.ToString());
            htmlr.AppendLine("</div> <!--span11-->");
            htmlr.AppendLine("</div> <!--row-fluid-->");



            return htmlr.ToString();

        }




        #region Activar Comprobantes

        [WebMethod]
        public string ActivarComprobante(object objeto)
        {
            Usuario usr = new Usuario(objeto);
            usr.usr_id_key = usr.usr_id;
            usr = UsuarioBLL.GetByPK(usr);

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);
            ///VERIFICAR SI EL COMPROBANTE CORRESPONDE AL USUARIO O ES EL ADMINISTRADOR



            bool permite = true;
            string mensaje = "";

            if (!Contabilidad.PeriodoIsOpen(comprobante.com_fecha.Year, comprobante.com_fecha.Month, usr.usr_id))
            {
                permite = false;
                mensaje = "NO SE PUEDE ACTIVAR, EL COMPROBANTE PERTENECE A UN PERIODO Y MES CERRADO...";
            }

            if (permite)
            {
                if (comprobante.com_tipodoc == Constantes.cFactura.tpd_codigo || comprobante.com_tipodoc == Constantes.cGuia.tpd_codigo || comprobante.com_tipodoc == Constantes.cRecibo.tpd_codigo)
                {
                    permite = true;
                }
                else
                {
                    permite = false;
                    mensaje = "NO POSEE AUTORIZACIÓN PARA ACTIVAR EL COMPROBANTE";
                }
            }


            //if (comprobante.crea_usr != usr.usr_id)
            //{
            if (usr.usr_perfil != Constantes.cPerfilAdministrador)
                permite = false;

            //}



            StringBuilder htmlr = new StringBuilder();
            htmlr.AppendLine("<div class=\"row-fluid\">");
            htmlr.AppendLine("<div class=\"span12 popupcontiner\">");
            HtmlTable tdret = new HtmlTable();
            tdret.CreteEmptyTable(3, 2);
            tdret.rows[0].cells[0].valor = "Número";
            tdret.rows[0].cells[1].valor = new Input { id = "txtDOCTRANACT", clase = Css.large, habilitado = false, valor = comprobante.com_doctran }.ToString() + new Input { id = "txtCODIGOCOMPACT", clase = Css.large, habilitado = false, valor = comprobante.com_codigo, visible = false }.ToString();
            tdret.rows[1].cells[0].valor = "Observación";
            tdret.rows[1].cells[1].valor = new Textarea { id = "txtOBSERVACIONACT", clase = Css.xlarge, rows = 5, valor = comprobante.com_concepto, habilitado = permite }.ToString();
            tdret.rows[2].cells[0].valor = "";
            tdret.rows[2].cells[1].valor = new Input { id = "txtPERMITEACT", visible = false, valor = permite }.ToString() + ((!permite) ? "<div class='blink_me'>"+mensaje+"</div>" : "AUTORIZADO PARA ACTIVAR");
            htmlr.AppendLine(tdret.ToString());
            htmlr.AppendLine(" </div><!--span6-->");

            return htmlr.ToString();

        }

        [WebMethod]
        public string SaveActivarComprobante(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante = General.ActivarComprobante(comprobante);

            return "OK";

        }

        #endregion

        #region Mayorizar Comprobantes

        [WebMethod]
        public string MayorizarAllComprobante(object objeto)
        {


            List<Comprobante> lst = ComprobanteBLL.GetAll(new WhereParams("com_estado ={0} and com_codclipro >0 and (com_tipodoc ={1} or com_tipodoc={2}) ", 1, Constantes.cFactura.tpd_codigo, Constantes.cGuia.tpd_codigo), "");
            foreach (Comprobante item in lst)
            {
                FAC.account_factura(item);
            }


            return "OK";

        }

        [WebMethod]
        public string RecontabilizarAllComprobante(object objeto)
        {
            Comprobante com = new Comprobante(objeto);

            int tipodocfac = Constantes.cFactura.tpd_codigo;
            int tipodocrec = Constantes.cRecibo.tpd_codigo;


            int i = 0;
            //List<vComprobanteDescuadrado> lst = vComprobanteDescuadradoBLL.GetAll(new WhereParams("com_estado ={0} and  com_tipodoc ={1} and com_periodo={2} and com_mes ={3} and (sum(CASE WHEN dco_debcre =1  THEN dco_valor_nac ELSE 0 END) - sum(CASE WHEN dco_debcre =2  THEN dco_valor_nac ELSE 0 END)) <> 0", Constantes.cEstadoMayorizado, Constantes.cFactura.tpd_codigo, com.com_periodo, com.com_mes), "");
            List<vComprobanteDescuadrado> lst = vComprobanteDescuadradoBLL.GetAll(new WhereParams("com_estado ={0} and com_periodo={1} and com_mes ={2} and (sum(CASE WHEN dco_debcre =1  THEN dco_valor_nac ELSE 0 END) - sum(CASE WHEN dco_debcre =2  THEN dco_valor_nac ELSE 0 END)) <> 0", Constantes.cEstadoMayorizado, com.com_periodo, com.com_mes), "");
            foreach (vComprobanteDescuadrado item in lst)
            {

                //if (item.credito < item.total)
                //{
                //    Dcomdoc detalle = new Dcomdoc();
                //    detalle.ddoc_empresa = item.empresa.Value;
                //    detalle.ddoc_comprobante = item.codigo.Value;
                //    detalle.ddoc_secuencia = 99;
                //    detalle.ddoc_producto = 112;
                //    detalle.ddco_udigitada = 1;
                //    detalle.ddoc_cantidad = 1;
                //    detalle.ddoc_precio = item.total.Value - item.credito.Value;
                //    detalle.ddoc_total = detalle.ddoc_precio;
                //    detalle.ddoc_grabaiva = 0;
                //    DcomdocBLL.Insert(detalle);
                //}

                Comprobante c = new Comprobante();
                c.com_codigo = item.codigo.Value;
                c.com_empresa = item.empresa.Value;
                if (item.tipodoc == tipodocfac)
                    FAC.reaccount_factura(c);
                if (item.tipodoc == tipodocrec)
                    FAC.account_recibo(c);
                i++;
            }


            return "OK";

        }


        [WebMethod]
        public string RecontabilizarComprobantes(object objeto)
        {
            Comprobante com = new Comprobante(objeto);

            int tipodocfac = Constantes.cFactura.tpd_codigo;
            int tipodocrec = Constantes.cRecibo.tpd_codigo;


            int i = 0;

            List<Comprobante> lst = ComprobanteBLL.GetAll(new WhereParams("com_estado ={0} and com_periodo={1} and com_mes ={2}", Constantes.cEstadoMayorizado, com.com_periodo, com.com_mes), "");
            foreach (Comprobante item in lst)
            {
                if (item.com_tipodoc == tipodocfac)
                    FAC.reaccount_factura(item);
                //if (item.com_tipodoc == tipodocrec)
                //    FAC.account_recibo(item);
                i++;
            }


            return "OK";

        }



        [WebMethod]
        public string MayorizarComprobante(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);




            bool permite = true;
            string mensaje = "";

            if (!Contabilidad.PeriodoIsOpen(comprobante.com_fecha.Year, comprobante.com_fecha.Month, comprobante.crea_usr))
            {
                permite = false;
                mensaje = "NO SE PUEDE REMAYORIZAR, EL COMPROBANTE PERTENECE A UN PERIODO Y MES CERRADO...";
            }



            if (permite)
            {

                comprobante.com_empresa_key = comprobante.com_empresa;
                comprobante.com_codigo_key = comprobante.com_codigo;
                comprobante.com_estado = Constantes.cEstadoGrabado;

                ComprobanteBLL.Update(comprobante);




                if (comprobante.com_tipodoc == Constantes.cFactura.tpd_codigo)
                    comprobante = FAC.account_factura(comprobante);
                //comprobante = FAC.reaccount_factura(comprobante);
                if (comprobante.com_tipodoc == Constantes.cDiario.tpd_codigo)
                    comprobante = CNT.account_diario(comprobante);
                if (comprobante.com_tipodoc == Constantes.cNotacre.tpd_codigo)
                    comprobante = BAN.account_notacredeb(comprobante);
                if (comprobante.com_tipodoc == Constantes.cRecibo.tpd_codigo)
                    comprobante = FAC.account_recibo(comprobante);
                return "OK";
            }
            else
                return mensaje;
        }

        [WebMethod]
        public string RecontabilizarComprobante(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);


            bool permite = true;
            string mensaje = "";

            if (!Contabilidad.PeriodoIsOpen(comprobante.com_fecha.Year, comprobante.com_fecha.Month, comprobante.crea_usr))
            {
                permite = false;
                mensaje = "NO SE PUEDE RECONTABILIZAR, EL COMPROBANTE PERTENECE A UN PERIODO Y MES CERRADO...";
            }



            if (permite)
            {

                comprobante.com_empresa_key = comprobante.com_empresa;
                comprobante.com_codigo_key = comprobante.com_codigo;
                comprobante.com_estado = Constantes.cEstadoGrabado;

                ComprobanteBLL.Update(comprobante);




                if (comprobante.com_tipodoc == Constantes.cFactura.tpd_codigo)
                    comprobante = FAC.reaccount_factura(comprobante);
                if (comprobante.com_tipodoc == Constantes.cRecibo.tpd_codigo)
                    comprobante = FAC.account_recibo(comprobante);
                    /*if (comprobante.com_tipodoc == Constantes.cDiario.tpd_codigo)
                        comprobante = CNT.account_diario(comprobante);*/
                    return "OK";
            }
            else
                return mensaje;
        }




        #endregion

        #region Modifcar Datos Comprobante 

        [WebMethod]
        public string ModicarDatosComprobante(object objeto)
        {
            Usuario usr = new Usuario(objeto);
            usr.usr_id_key = usr.usr_id;
            usr = UsuarioBLL.GetByPK(usr);

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);
            comprobante = FAC.close_comprobanteplanilla(comprobante);
            comprobante = FAC.close_comprobantehojaruta(comprobante);

            bool permite = true;
            string mensaje = "";

            if (!Contabilidad.PeriodoIsOpen(comprobante.com_fecha.Year, comprobante.com_fecha.Month, usr.usr_id))
            {
                permite = false;
                mensaje = "NO SE PUEDE MODIFICAR DATOS, EL COMPROBANTE PERTENECE A UN PERIODO Y MES CERRADO...";
            }

            ///VERIFICAR SI TIENE AUTORIZACION

            StringBuilder htmlr = new StringBuilder();
            htmlr.AppendLine("<div class=\"row-fluid\">");
            htmlr.AppendLine("<div class=\"span12 popupcontiner\">");
            HtmlTable tdret = new HtmlTable();
            tdret.CreteEmptyTable(5, 2);
            tdret.rows[0].cells[0].valor = "Numero";
            tdret.rows[0].cells[1].valor = new Input { id = "txtDOCTRANMOD", clase = Css.large, habilitado = false, valor = comprobante.com_doctran }.ToString() + new Input { id = "txtCODIGOCOMPMOD", clase = Css.large, habilitado = false, valor = comprobante.com_codigo, visible = false }.ToString();
            tdret.rows[1].cells[0].valor = "Nuevo Numero";
            tdret.rows[1].cells[1].valor = new Input { id = "txtNUMEROMOD", clase = Css.small, habilitado = true, valor = comprobante.com_numero }.ToString();
            tdret.rows[2].cells[0].valor = "Nueva Fecha";
            tdret.rows[2].cells[1].valor = new Input { id = "txtFECHAMOD", clase = Css.small, datepicker = true, datetimevalor = comprobante.com_fecha, habilitado = true }.ToString();
            tdret.rows[3].cells[0].valor = "Observacion";
            tdret.rows[3].cells[1].valor = new Textarea { id = "txtOBSERVACIONMOD", clase = Css.xlarge, rows = 5, valor = comprobante.com_concepto }.ToString();
            tdret.rows[4].cells[0].valor = "";
            tdret.rows[4].cells[1].valor = new Input { id = "txtPERMITEMOD", visible = false, valor = permite }.ToString() + ((!permite) ? "<div class='blink_me'>"+mensaje+"</div>" : "AUTORIZADO PARA MODIFICAR");
            htmlr.AppendLine(tdret.ToString());
            htmlr.AppendLine(" </div><!--span6-->");

            return htmlr.ToString();

        }

        [WebMethod]
        public string SaveModicarDatosComprobante(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);

            comprobante = General.ModificaDatosComprobante(comprobante);
            return "OK";
        }

        #endregion

        #region Modifcar Guía

        [WebMethod]
        public string ModicarGuia(object objeto)
        {
            Usuario usr = new Usuario(objeto);
            usr.usr_id_key = usr.usr_id;
            usr = UsuarioBLL.GetByPK(usr);

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

          
            //bool permite = true;
            //if (comprobante.com_tipodoc == Constantes.cFactura.tpd_codigo)
            //{
            //    if (usr.usr_perfil != Constantes.cPerfilAdministrador)
            //        permite = false;
            //}


            bool permite = true;
            string mensaje = "";

            if (!Contabilidad.PeriodoIsOpen(comprobante.com_fecha.Year, comprobante.com_fecha.Month, usr.usr_id))
            {
                permite = false;
                mensaje = "NO SE PUEDE MODIFICAR, EL COMPROBANTE PERTENECE A UN PERIODO Y MES CERRADO...";
            }


            ///VERIFICAR SI TIENE AUTORIZACION

            StringBuilder htmlr = new StringBuilder();
            htmlr.AppendLine("<div class=\"row-fluid\">");
            htmlr.AppendLine("<div class=\"span12 popupcontiner\">");
            HtmlTable tdret = new HtmlTable();
            tdret.CreteEmptyTable(3, 2);
            tdret.rows[0].cells[0].valor = "Numero";
            tdret.rows[0].cells[1].valor = new Input { id = "txtDOCTRANMOD", clase = Css.large, habilitado = false, valor = comprobante.com_doctran }.ToString() + new Input { id = "txtCODIGOCOMPMOD", clase = Css.large, habilitado = false, valor = comprobante.com_codigo, visible = false }.ToString();
            tdret.rows[1].cells[0].valor = "Observacion";
            tdret.rows[1].cells[1].valor = new Textarea { id = "txtOBSERVACIONMOD", clase = Css.xlarge, rows = 5, valor = comprobante.com_concepto }.ToString();
            tdret.rows[2].cells[0].valor = "";
            tdret.rows[2].cells[1].valor = new Input { id = "txtPERMITEMOD", visible = false, valor = permite }.ToString() + ((!permite) ? "<div class='blink_me'>" + mensaje + "</div>" : "AUTORIZADO PARA MODIFICAR");
            htmlr.AppendLine(tdret.ToString());
            htmlr.AppendLine(" </div><!--span6-->");

            return htmlr.ToString();

        }

        [WebMethod]
        public string SaveModicarGuia(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);

            comprobante = FAC.modifyGuia(comprobante);
            return "OK";
        }

        #endregion

        #region Modifcar Pagos

        [WebMethod]
        public string ModicarPago(object objeto)
        {
            Usuario usr = new Usuario(objeto);
            usr.usr_id_key = usr.usr_id;
            usr = UsuarioBLL.GetByPK(usr);

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            bool permite = true;
            string mensaje = "";

            if (!Contabilidad.PeriodoIsOpen(comprobante.com_fecha.Year, comprobante.com_fecha.Month, usr.usr_id))
            {
                permite = false;
                mensaje = "NO SE PUEDE MODIFICAR, EL COMPROBANTE PERTENECE A UN PERIODO Y MES CERRADO...";
            }

            ///VERIFICAR SI TIENE AUTORIZACION

            StringBuilder htmlr = new StringBuilder();
            htmlr.AppendLine("<div class=\"row-fluid\">");
            htmlr.AppendLine("<div class=\"span12 popupcontiner\">");
            HtmlTable tdret = new HtmlTable();
            tdret.CreteEmptyTable(3, 2);
            tdret.rows[0].cells[0].valor = "Numero";
            tdret.rows[0].cells[1].valor = new Input { id = "txtDOCTRANMOD", clase = Css.large, habilitado = false, valor = comprobante.com_doctran }.ToString() + new Input { id = "txtCODIGOCOMPMOD", clase = Css.large, habilitado = false, valor = comprobante.com_codigo, visible = false }.ToString();
            tdret.rows[1].cells[0].valor = "Observacion";
            tdret.rows[1].cells[1].valor = new Textarea { id = "txtOBSERVACIONMOD", clase = Css.xlarge, rows = 5, valor = comprobante.com_concepto }.ToString();
            tdret.rows[2].cells[0].valor = "";
            tdret.rows[2].cells[1].valor = new Input { id = "txtPERMITEMOD", visible = false, valor = permite }.ToString() + ((!permite) ? "<div class='blink_me'>" + mensaje + "</div>" : "AUTORIZADO PARA MODIFICAR");
            htmlr.AppendLine(tdret.ToString());
            htmlr.AppendLine(" </div><!--span6-->");

            return htmlr.ToString();

        }

        [WebMethod]
        public string SaveModicarPago(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);

            comprobante = CNT.modify_diario(comprobante);
            return "OK";
        }

        #endregion

        #region View Auditoria 

        [WebMethod]
        public string ViewAuditoria(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);

            StringBuilder htmlr = new StringBuilder();
            htmlr.AppendLine("<div class=\"row-fluid\">");
            htmlr.AppendLine("<div class=\"span12 popupcontiner\">");
            HtmlTable tdret = new HtmlTable();
            tdret.CreteEmptyTable(3, 2);
            tdret.rows[0].cells[0].valor = "Comprobante";
            tdret.rows[0].cells[1].valor = new Input { id = "txtDOCTRAN_A", clase = Css.large, habilitado = false, valor = comprobante.com_doctran }.ToString() + " " + new Input { id = "txtCODIGO_A", clase = Css.small, habilitado = false, valor = comprobante.com_codigo }.ToString();
            tdret.rows[1].cells[0].valor = "Creación";
            tdret.rows[1].cells[1].valor = comprobante.crea_usrnombres + "-" + comprobante.crea_fecha;
            tdret.rows[2].cells[0].valor = "Modificación";
            tdret.rows[2].cells[1].valor = comprobante.mod_usr + "-" + comprobante.mod_fecha;
            htmlr.AppendLine(tdret.ToString());
            htmlr.AppendLine(" </div><!--span6-->");

            return htmlr.ToString();

        }

        #endregion


        #region Seleccionar Empresa
        [WebMethod]
        public string SelectEmpresa(object objeto)
        {
            if (objeto != null)
            {
                return new Select { id = "cmbempresa", etiqueta = "Seleccione empresa", diccionario = Dictionaries.GetEmpresasUsuario(objeto.ToString()) }.ToString();
            }
            return "";
        }

        #endregion

        #region Agregar Persona


        [WebMethod]
        public string AddPersona(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object id = null;
                object tclipro = null;
                //object periodo = null;
                //object ctipocom = null;
                //object almacen = null;
                //object pventa = null;

                tmp.TryGetValue("id", out id);
                tmp.TryGetValue("tclipro", out tclipro);
                //tmp.TryGetValue("periodo", out periodo);
                //tmp.TryGetValue("ctipocom", out ctipocom);
                //tmp.TryGetValue("almacen", out almacen);
                //tmp.TryGetValue("pventa", out pventa);


                StringBuilder html = new StringBuilder();

                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span10\">");

                HtmlTable tdatos = new HtmlTable();
                tdatos.id = "tdpersona_P";
                tdatos.CreteEmptyTable(9, 2);
                tdatos.rows[0].cells[0].valor = "";
                tdatos.rows[0].cells[1].valor = new Input { id = "txtCODIGO_P", visible = false }.ToString() + new Input { id = "txtID_P", visible = false }.ToString() + new Input { id = "txtIDCONTROL_P", valor = id, visible = false }.ToString() + new Input { id = "txtTCLIPRO", valor = tclipro, visible = false }.ToString() + new Boton { id = "btnclean", small = true, tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersonaObject()" }.ToString();
                tdatos.rows[1].cells[0].valor = "RUC/CI:";
                if (Convert.ToInt32(tclipro) == Constantes.cCliente)
                    tdatos.rows[1].cells[1].valor = new Input { id = "txtCIRUC_P", placeholder = "RUC / CI", autocomplete = "GetClienteObj" }.ToString();
                if (Convert.ToInt32(tclipro) == Constantes.cProveedor)
                    tdatos.rows[1].cells[1].valor = new Input { id = "txtCIRUC_P", placeholder = "RUC / CI", clase = Css.large, autocomplete = "GetProveedorObj" }.ToString();
                tdatos.rows[2].cells[0].valor = "Tipo Identifación:";
                tdatos.rows[2].cells[1].valor = new Select { id = "cmbTIPOID_P", clase = Css.large, diccionario = Dictionaries.GetTipoIdentificacion() }.ToString();
                tdatos.rows[3].cells[0].valor = "Nombres:";
                tdatos.rows[3].cells[1].valor = new Input { id = "txtNOMBRES_P", placeholder = "Nombres", clase = Css.large }.ToString();

                tdatos.rows[4].cells[0].valor = "Apellidos:";
                tdatos.rows[4].cells[1].valor = new Input { id = "txtAPELLIDOS_P", placeholder = "Apellidos", clase = Css.large }.ToString();

                tdatos.rows[5].cells[0].valor = "Razón social:";
                tdatos.rows[5].cells[1].valor = new Input { id = "txtRAZON_P", placeholder = "Razón social", clase = Css.large }.ToString();
                tdatos.rows[6].cells[0].valor = "Dirección:";
                tdatos.rows[6].cells[1].valor = new Input { id = "txtDIRECCION_P", placeholder = "Dirección", clase = Css.large }.ToString();
                tdatos.rows[7].cells[0].valor = "Teléfono:";
                tdatos.rows[7].cells[1].valor = new Input { id = "txtTELEFONO_P", placeholder = "Teléfono", clase = Css.large }.ToString();
                tdatos.rows[8].cells[0].valor = "Email:";
                tdatos.rows[8].cells[1].valor = new Input { id = "txtMAIL_P", placeholder = "Email", clase = Css.large }.ToString();


                html.Append(tdatos.ToString());

                html.AppendLine(" </div><!--span10-->");
                html.AppendLine("</div><!--row-fluid-->");

                /*

                html.AppendLine(new Boton { id = "btnclean", small = true, tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersonaObject()" }.ToString());
                html.AppendLine(new Input { id = "txtCODIGO_P", visible = false }.ToString());
                html.AppendLine(new Input { id = "txtID_P", visible = false }.ToString());
                if (Convert.ToInt32(tclipro) == Constantes.cCliente)
                { html.AppendLine(new Input { id = "txtCIRUC_P", etiqueta = "RUC / CI", placeholder = "RUC / CI", clase = Css.large, autocomplete = "GetClienteObj" }.ToString()); }
                if (Convert.ToInt32(tclipro) == Constantes.cProveedor)
                { html.AppendLine(new Input { id = "txtCIRUC_P", etiqueta = "RUC / CI", placeholder = "RUC / CI", clase = Css.large, autocomplete = "GetProveedorObj" }.ToString()); }
                
                html.AppendLine(new Select { id = "cmbTIPOID_P", etiqueta = "Tipo Identificación", clase = Css.large, diccionario = Dictionaries.GetTipoIdentificacion() }.ToString());
                html.AppendLine(new Input { id = "txtNOMBRES_P", etiqueta = "Nombres", placeholder = "Nombres", clase = Css.large }.ToString());
                html.AppendLine(new Input { id = "txtAPELLIDOS_P", etiqueta = "Apellidos", placeholder = "Apellidos", clase = Css.large }.ToString());
                html.AppendLine(new Input { id = "txtRAZON_P", etiqueta = "Razón social", placeholder = "Razón social", clase = Css.xlarge }.ToString());
                html.AppendLine(new Input { id = "txtDIRECCION_P", etiqueta = "Dirección", placeholder = "Dirección", clase = Css.large }.ToString());
                html.AppendLine(new Input { id = "txtTELEFONO_P", etiqueta = "Teléfono", placeholder = "Teléfono", clase = Css.large }.ToString());
                html.AppendLine(new Input { id = "txtIDCONTROL_P", valor = id, visible = false }.ToString());
                html.AppendLine(new Input { id = "txtTCLIPRO", valor = tclipro, visible = false }.ToString());//html.AppendLine(new Input { id = "txtCELULAR", etiqueta = "Celular", placeholder = "Celular", clase = Css.large }.ToString());
                //html.AppendLine(new Input { id = "txtMAIL", etiqueta = "Mail", placeholder = "Mail", clase = Css.large }.ToString());
                //html.AppendLine(new Textarea { id = "txtOBSERVACION", etiqueta = "Observación", placeholder = "Observación", clase = Css.large }.ToString());
                //html.AppendLine(new Select { id = "cmbTIPO", etiqueta = "Tipo", placeholder = "Tipo", diccionario = Dictionaries.GetTipoPersonas(), multiselect = true, obligatorio = true }.ToString());
                */
                return html.ToString();


            }
            return "";
        }


        [WebMethod]
        public string SavePersona(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object persona_get = null;
            object pxt_get = null;
            //object periodo = null;
            //object ctipocom = null;
            object almacen = null;
            //object pventa = null;

            tmp.TryGetValue("persona", out persona_get);
            tmp.TryGetValue("pxt", out pxt_get);
            tmp.TryGetValue("almacen", out almacen);


            Persona persona = new Persona(persona_get);
            if (Functions.Validaciones.valida_cedularuc(persona.per_ciruc))
            {
                if (string.IsNullOrEmpty(persona.per_razon))
                    persona.per_razon = persona.per_apellidos + " " + persona.per_nombres;
                BLL transaction = new BLL();
                transaction.CreateTransaction();

                try
                {
                    transaction.BeginTransaction();
                    if (persona.per_codigo > 0)
                    {
                        Persona personaorg = new Persona();
                        personaorg.per_codigo = persona.per_codigo;
                        personaorg.per_codigo_key = persona.per_codigo;
                        personaorg.per_empresa = persona.per_empresa;
                        personaorg.per_empresa_key = persona.per_empresa;
                        personaorg = PersonaBLL.GetByPK(personaorg);
                        personaorg.per_ciruc = persona.per_ciruc;
                        personaorg.per_nombres = persona.per_nombres;
                        personaorg.per_apellidos = persona.per_apellidos;
                        personaorg.per_razon = persona.per_razon;
                        personaorg.per_direccion = persona.per_direccion;
                        personaorg.per_telefono = persona.per_telefono;
                        personaorg.per_mail = persona.per_mail;
                        personaorg.per_tipoid = persona.per_tipoid;
                        PersonaBLL.Update(personaorg);
                        persona = personaorg;

                    }
                    else
                    {
                        Listaprecio lista = Constantes.GetListaPrecio();//Obtiene lista de precio por defecto
                        persona.per_listaprecio = lista.lpr_codigo;
                        persona.per_listanombre = lista.lpr_nombre;
                        persona.per_listaid = lista.lpr_id;

                        tmp = (Dictionary<string, object>)pxt_get;
                        object pxt_tipo = null;

                        //object periodo = null;
                        //object ctipocom = null;
                        //object almacen = null;
                        //object pventa = null;

                        tmp.TryGetValue("pxt_tipo", out pxt_tipo);
                        Politica politica = Constantes.GetPoliticacli();//Obtiene politica por defecto
                        if (Convert.ToInt32(pxt_tipo) == Constantes.cCliente)
                        {
                            politica = Constantes.GetPoliticacli();//Obtiene politica por defecto


                        }
                        if (Convert.ToInt32(pxt_tipo) == Constantes.cProveedor)
                        {
                            politica = Constantes.GetPoliticaProv();//Obtiene politica por defecto


                        }

                        persona.per_politica = politica.pol_codigo;
                        persona.per_politicaid = politica.pol_id;
                        persona.per_politicanombre = politica.pol_nombre;
                        persona.per_politicadesc = politica.pol_porc_desc;
                        persona.per_politicanropagos = politica.pol_nro_pagos;
                        persona.per_politicadiasplazo = politica.pol_dias_plazo;
                        persona.per_politicaporpagocon = politica.pol_porc_pago_con;


                        //FALTA VENDEDOR
                        Empresa emp = new Empresa();
                        emp.emp_codigo_key = persona.per_empresa;
                        emp = EmpresaBLL.GetByPK(emp);
                        Usuario usr = new Usuario();
                        usr.usr_id_key = persona.crea_usr;
                        usr = UsuarioBLL.GetByPK(usr);
                        persona.per_id = General.GetIdPersona(emp, usr);
                        persona.per_retfuente = Constantes.GetImpRteFte().imp_codigo;
                        persona.per_retiva = Constantes.GetImpRteIVA().imp_codigo;
                        persona.per_estado = Constantes.cEstadoGrabado;
                        persona.per_codigo = PersonaBLL.InsertIdentity(transaction, persona);

                        Personaxtipo pxt = new Personaxtipo();
                        pxt.pxt_persona = persona.per_codigo;
                        pxt.pxt_empresa = persona.per_empresa;
                        pxt.pxt_estado = Constantes.cEstadoGrabado;
                        pxt.pxt_tipo = Convert.ToInt32(pxt_tipo);
                        Catcliente catcliente = Constantes.GetCatcliente();//Obtiene politica por defecto
                        if (Convert.ToInt32(pxt_tipo) == Constantes.cCliente)
                        {
                            if (almacen != null)
                            {
                                Almacen alm = AlmacenBLL.GetByPK(new Almacen { alm_empresa = persona.per_empresa, alm_empresa_key = persona.per_empresa, alm_codigo = int.Parse(almacen.ToString()), alm_codigo_key = int.Parse(almacen.ToString()) });
                                if (alm.alm_cat_cliente.HasValue)
                                    catcliente = CatclienteBLL.GetByPK(new Catcliente { cat_empresa = persona.per_empresa, cat_empresa_key = persona.per_empresa, cat_codigo = alm.alm_cat_cliente.Value, cat_codigo_key = alm.alm_cat_cliente.Value });
                                else
                                    catcliente = Constantes.GetCatcliente();//Obtiene politica por defecto
                            }
                            else
                                catcliente = Constantes.GetCatcliente();//Obtiene politica por defecto


                        }
                        if (Convert.ToInt32(pxt_tipo) == Constantes.cProveedor)
                        {
                            catcliente = Constantes.GetCatProv();//Obtiene politica por defecto


                        }


                        pxt.pxt_politicas = politica.pol_codigo;
                        pxt.pxt_cat_persona = catcliente.cat_codigo;
                        pxt.crea_usr = persona.crea_usr;
                        pxt.crea_fecha = persona.crea_fecha;
                        pxt.mod_usr = persona.mod_usr;
                        pxt.mod_fecha = persona.mod_fecha;
                        PersonaxtipoBLL.Insert(transaction, pxt);
                    }
                    transaction.Commit();
                    return new JavaScriptSerializer().Serialize(persona);
                    //return codigo.ToString();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return "ERROR: " + ex.Message;
                }
            }
            else
                return "ERROR: Cédula/RUC incorrecto";
            //throw new ArgumentException("Cédula/RUC incorrecto");

        }

        #endregion

        #region Recibo Pago

        [WebMethod]
        public string CrearRecibo(object objeto)
        {
            if (objeto != null)
            {

                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object empresa = null;
                object usuario = null;
                object total = null;
                object politica = null;

                tmp.TryGetValue("empresa", out empresa);
                tmp.TryGetValue("usuario", out usuario);
                tmp.TryGetValue("total", out total);
                tmp.TryGetValue("politica", out politica);

                List<PoliticaTipoPago> lst = Constantes.cPoliticaTipoPago;
                PoliticaTipoPago poltpa = null;
                if (politica != null)
                    poltpa = lst.Find(delegate (PoliticaTipoPago p) { return p.politica == politica.ToString(); });


                StringBuilder html = new StringBuilder();
                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span7\">");


                HtmlTable tddet = new HtmlTable();
                tddet.id = "tdpago_P";
                tddet.invoice = true;
                //tdatos.titulo = "Factura";            

                tddet.AddColumn("Tipo", "width10", "", new Input() { id = "txtIDTIPO_P", placeholder = "TIPO", autocomplete = "GetTipoPagoCliObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODTIPO_P", visible = false }.ToString());
                tddet.AddColumn("Descripción", "width30", "", new Input() { id = "txtNOMBRETIPO_P", placeholder = "DESCRIPCION", clase = Css.blocklevel, habilitado = false }.ToString());
                tddet.AddColumn("Valor", "width10", Css.right, new Input() { id = "txtVALOR_P", placeholder = "VALOR", clase = Css.blocklevel + Css.amount }.ToString());
                tddet.AddColumn("", "width5", Css.center, new Boton { small = true, clase = "icon-trash", tooltip = "Eliminar registro", click = "RemoveRowCan(this)" }.ToString());

                tddet.editable = true;

                HtmlRow row = new HtmlRow();

                if (poltpa != null)
                {
                    Tipopago tpa = TipopagoBLL.GetByPK(new Tipopago { tpa_empresa = int.Parse(empresa.ToString()), tpa_empresa_key = int.Parse(empresa.ToString()), tpa_codigo = int.Parse(poltpa.tipopago), tpa_codigo_key = int.Parse(poltpa.tipopago) });

                    row.data = "data-codtipo='" + tpa.tpa_codigo + "' data-emisor='' data-nrodocumento='' data-nrocuenta='' data-banco='' data-nrocheque='' data-beneficiario='' data-fecha='' data-cuenta='' data-cuentanombre='' ";
                    row.removable = true;
                    row.clickevent = "EditCan(this)";

                    row.cells.Add(new HtmlCell { valor = tpa.tpa_id });
                    row.cells.Add(new HtmlCell { valor = tpa.tpa_nombre });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat((Decimal?)Conversiones.GetValueByType(total, typeof(Decimal?))), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = new Boton { small = true, clase = "icon-trash", tooltip = "Eliminar registro", click = "RemoveRowCan(this)" }.ToString(), clase = Css.center });

                }
                else
                {
                    row.data = "data-codtipo='1' data-emisor='' data-nrodocumento='' data-nrocuenta='' data-banco='' data-nrocheque='' data-beneficiario='' data-fecha='' data-cuenta='' data-cuentanombre='' ";
                    row.removable = true;
                    row.clickevent = "EditCan(this)";

                    row.cells.Add(new HtmlCell { valor = "TP001" });
                    row.cells.Add(new HtmlCell { valor = "EFECTIVO" });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat((Decimal?)Conversiones.GetValueByType(total, typeof(Decimal?))), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = new Boton { small = true, clase = "icon-trash", tooltip = "Eliminar registro", click = "RemoveRowCan(this)" }.ToString(), clase = Css.center });
                }
                tddet.AddRow(row);

                /*foreach (Drecibo item in comprobante.recibos)
                {
                    HtmlRow row = new HtmlRow();
                    row.data = "data-codtipo=" + item.dfp_tipopago;
                    row.removable = true;

                    row.cells.Add(new HtmlCell { valor = item.dfp_tipopagoid });
                    row.cells.Add(new HtmlCell { valor = item.dfp_tipopagonombre });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.dfp_monto), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString(), clase = Css.center });
                    tdatos.AddRow(row);

                }*/

                html.AppendLine(tddet.ToString());

                html.AppendLine(" </div><!--span7-->");
                html.AppendLine("<div class=\"span3\">");


                HtmlTable tdatos = new HtmlTable();
                tdatos.id = "tddatos_P";
                tdatos.CreteEmptyTable(8, 2);
                tdatos.rows[0].cells[0].valor = "Emisor:";
                tdatos.rows[0].cells[1].valor = new Input() { id = "txtEMISOR_P", placeholder = "EMISOR", autocomplete = "GetEmisorObj", clase = Css.large }.ToString();

                tdatos.rows[1].cells[0].valor = "Nro. Documento:";
                tdatos.rows[1].cells[1].valor = new Input() { id = "txtNRODOCUMENTO_P", placeholder = "NRO DOCUMENTO", clase = Css.medium }.ToString();
                tdatos.rows[2].cells[0].valor = "Nro. Cuenta:";
                tdatos.rows[2].cells[1].valor = new Input() { id = "txtNROCUENTA_P", placeholder = "NRO CUENTA", clase = Css.medium }.ToString();
                tdatos.rows[3].cells[0].valor = "Banco:";
                tdatos.rows[3].cells[1].valor = new Select() { id = "cmbBANCO_P", diccionario = Dictionaries.GetBancos(), clase = Css.medium }.ToString();
                tdatos.rows[4].cells[0].valor = "Nro. Cheque:";
                tdatos.rows[4].cells[1].valor = new Input() { id = "txtNROCHEQUE_P", placeholder = "NRO CHEQUE", clase = Css.medium }.ToString();
                tdatos.rows[5].cells[0].valor = "Beneficiario:";
                tdatos.rows[5].cells[1].valor = new Input() { id = "txtBENEFICIARIO_P", placeholder = "BENEFICIARIO", clase = Css.medium }.ToString();
                tdatos.rows[6].cells[0].valor = "Fecha Vence:";
                tdatos.rows[6].cells[1].valor = new Input() { id = "txtFECHAVENCE_P", datepicker = true, datetimevalor = DateTime.Now, clase = Css.medium }.ToString();

                tdatos.rows[7].cells[0].valor = "Cuenta:";
                tdatos.rows[7].cells[1].valor = new Input() { id = "txtIDCUENTA_P", placeholder = "ID CUENTA", clase = Css.small }.ToString() + " " + new Input() { id = "txtNOMBRECUENTA_P", placeholder = "CUENTA", clase = Css.medium, habilitado = false }.ToString();

                html.AppendLine(tdatos.ToString());

                HtmlTable tdatos1 = new HtmlTable();
                tdatos1.CreteEmptyTable(1, 2);

                tdatos1.rows[0].cells[0].valor = "TOTAL";
                tdatos1.rows[0].cells[1].valor = new Input { id = "txtTOTALCOM_P", clase = Css.medium + Css.totalamount, habilitado = false, valor = Formatos.CurrencyFormat((Decimal?)Conversiones.GetValueByType(total, typeof(Decimal?))) }.ToString() + new Input { id = "txtTOTAL_P", visible = false, valor = Formatos.CurrencyFormat((Decimal?)Conversiones.GetValueByType(total, typeof(Decimal?))) }.ToString();

                html.AppendLine(tdatos1.ToString());


                html.AppendLine(" </div><!--span3-->");
                html.AppendLine("</div><!--row-fluid-->");

                return html.ToString();
            }
            return "";
        }

        #endregion

        #region Afectaciones

        [WebMethod]
        public string GetPrevAfectaciones(object objeto)
        {


            StringBuilder html = new StringBuilder();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object tipo = null;

                tmp.TryGetValue("tipo", out tipo);


                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span8\">");
                HtmlTable tdatos = new HtmlTable();
                tdatos.CreteEmptyTable(3, 2);
                tdatos.rows[0].cells[0].valor = "Cliente/Proveedor:";
                tdatos.rows[0].cells[1].valor = new Input { id = "txtIDPER_PR", clase = Css.mini, habilitado = true, autocomplete = "GetPersonaObj" }.ToString() + " " + new Input { id = "txtNOMBRES_PR", clase = Css.large, habilitado = false }.ToString() + new Input { id = "txtCODPER_PR", visible = false }.ToString();
                tdatos.rows[1].cells[0].valor = "Documento:";
                tdatos.rows[1].cells[1].valor = new Input { id = "txtDOC_PR", clase = Css.medium, habilitado = false }.ToString() + new Input { id = "txtTIPO_PR", visible = false, valor = tipo };
                tdatos.rows[2].cells[0].valor = "Cuenta/Módulo:";
                tdatos.rows[2].cells[1].valor = new Input() { id = "txtIDCUE_PR", placeholder = "CTA", autocomplete = "GetCuentaObj", clase = Css.mini }.ToString() + new Input() { id = "txtCODCUE_PR", visible = false }.ToString() + " " + new Input() { id = "txtCUENTA_PR", placeholder = "DESCRIPCION", clase = Css.large, habilitado = false }.ToString() + " " + new Input() { id = "txtMODULO_PR", placeholder = "MOD", habilitado = false, clase = Css.mini }.ToString() + new Input() { id = "txtCODMOD_PR", visible = false }.ToString();


                html.AppendLine(tdatos.ToString());
                html.AppendLine(" </div><!--span8-->");
                html.AppendLine("<div class=\"span3\">");
                HtmlTable tdatos1 = new HtmlTable();
                tdatos1.CreteEmptyTable(2, 2);
                tdatos1.rows[0].cells[0].valor = "Valor:";
                tdatos1.rows[0].cells[1].valor = new Input { id = "txtVALOR_PR", clase = Css.small, habilitado = true, numeric = true }.ToString();

                tdatos1.rows[1].cells[0].valor = "Saldo:";
                tdatos1.rows[1].cells[1].valor = new Input { id = "txtSALDO_PR", clase = Css.small, valor = "0", habilitado = false }.ToString();

                html.AppendLine(tdatos1.ToString());
                html.AppendLine(" </div><!--span3-->");
                html.AppendLine("</div><!--row-fluid-->");
            }
            return html.ToString();

        }

        [WebMethod]
        public string GetAfectaciones(object objeto)
        {
            StringBuilder html = new StringBuilder();
            if (objeto != null)
            {
                Comprobante obj = new Comprobante(objeto);
                string obliga = "S";
                long? codcomp = null;

                if (obj.ccomdoc.cdoc_factura.HasValue)
                    codcomp = obj.ccomdoc.cdoc_factura;

                Persona persona = new Persona();
                persona.per_empresa = obj.com_empresa;
                persona.per_empresa_key = obj.com_empresa;
                if (obj.com_codclipro.HasValue)
                {
                    persona.per_codigo = obj.com_codclipro.Value;
                    persona.per_codigo_key = obj.com_codclipro.Value;
                    persona = PersonaBLL.GetByPK(persona);
                }

                decimal total = obj.recibos.Sum(pkg => pkg.dfp_monto);
                if (total == 0)
                    total = obj.total.tot_total;//.notascre.Sum(pkg => pkg.dnc_valor)??0;

                int debcre = 0;
                if (obj.com_tipodoc == Constantes.cRecibo.tpd_codigo)
                {
                    debcre = Constantes.cDebito;
                }

                else if (obj.com_tipodoc == Constantes.cPago.tpd_codigo)
                {
                    debcre = Constantes.cCredito;
                }

                else if (obj.com_tipodoc == Constantes.cNotacre.tpd_codigo)
                {
                    debcre = Constantes.cDebito;
                }
                else if (obj.com_tipodoc == Constantes.cNotadeb.tpd_codigo)
                {
                    debcre = Constantes.cCredito;
                }
                else if (obj.com_tipodoc == Constantes.cNotacrePro.tpd_codigo)
                {
                    debcre = Constantes.cCredito;
                    total = obj.total.tot_total;
                }
                else if (obj.com_tipodoc == Constantes.cNotadebPro.tpd_codigo)
                {
                    debcre = Constantes.cDebito;
                }


                if (obj.com_tipodoc == Constantes.cDiario.tpd_codigo || obj.com_tipodoc == Constantes.cPagoBan.tpd_codigo || obj.com_tipodoc == Constantes.cDeposito.tpd_codigo || obj.com_tipodoc == Constantes.cNotaCreditoBan.tpd_codigo || obj.com_tipodoc == Constantes.cNotaDebitoBan.tpd_codigo)
                {

                    debcre = (obj.com_tclipro == Constantes.cCliente) ? Constantes.cDebito : Constantes.cCredito;
                    total = obj.total.tot_total;
                }



                if (obj.com_tipodoc == Constantes.cObligacion.tpd_codigo || obj.com_tipodoc == Constantes.cLiquidacionCompra.tpd_codigo || obj.com_tipodoc == Constantes.cNotacre.tpd_codigo)
                {
                    obliga = "N";//NO OBLIGA A REQUERIR QUE SE DISTRIBUYAN LOS VALORES
                    debcre = Constantes.cDebito;
                    total = obj.total.tot_total;
                }

                Auto.actualiza_documentos(obj.com_empresa, null, null, obj.com_codclipro, codcomp, null, null, 1);


                List<vDdocumento> lista = new List<vDdocumento>();
                if (codcomp.HasValue)
                    lista = vDdocumentoBLL.GetAll(new WhereParams("ddo_empresa={0} AND ddo_cancelado=0 AND ddo_codclipro={1} AND ddo_debcre ={2} and ddo_comprobante={3}", obj.com_empresa, obj.com_codclipro, debcre, codcomp), "");
                else
                    lista = vDdocumentoBLL.GetAll(new WhereParams("ddo_empresa={0} AND ddo_cancelado=0 AND ddo_codclipro={1} AND ddo_debcre ={2}", obj.com_empresa, obj.com_codclipro, debcre), "");
                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span8\">");
                HtmlTable tdatos = new HtmlTable();
                tdatos.CreteEmptyTable(2, 2);
                tdatos.rows[0].cells[0].valor = "Cliente/Proveedor:";
                tdatos.rows[0].cells[1].valor = new Input { id = "txtCODCLIPRO_P", valor = persona.per_id, clase = Css.mini, habilitado = false }.ToString() + " " + new Input { id = "txtNOMBRES_P", clase = Css.large, habilitado = false, valor = persona.per_apellidos + " " + persona.per_nombres }.ToString() + new Input { id = "txtCODPER_P", visible = false, valor = persona.per_codigo }.ToString();
                tdatos.rows[1].cells[0].valor = "Documento:";
                tdatos.rows[1].cells[1].valor = new Input { id = "txtDOCUMETO_P", clase = Css.medium, habilitado = false, valor = obj.com_doctran }.ToString() + new Input { id = "txtDEBCRE_P", visible = false, valor = debcre }.ToString();

                html.AppendLine(tdatos.ToString());
                html.AppendLine(" </div><!--span8-->");
                html.AppendLine("<div class=\"span3\">");
                HtmlTable tdatos1 = new HtmlTable();
                tdatos1.CreteEmptyTable(2, 2);
                tdatos1.rows[0].cells[0].valor = "Valor:";
                tdatos1.rows[0].cells[1].valor = new Input { id = "txtVALOR_P", clase = Css.small, valor = total.ToString().Replace(",", "."), habilitado = false }.ToString();

                tdatos1.rows[1].cells[0].valor = "Saldo:";
                tdatos1.rows[1].cells[1].valor = new Input { id = "txtSALDO_P", clase = Css.small, valor = total.ToString().Replace(",", "."), habilitado = false }.ToString() + new Input { id = "txtOBLIGA_P", visible = false, valor = obliga }.ToString();

                html.AppendLine(tdatos1.ToString());
                html.AppendLine(" </div><!--span3-->");
                html.AppendLine("</div><!--row-fluid-->");
                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span11\">");


                HtmlTable tddet = new HtmlTable();
                tddet.id = "tdafec_P";
                tddet.invoice = true;
                tddet.clase = "scrolltable";
                tddet.footer = true;

                /*tddet.AddColumn("D", "", "", "");
                tddet.AddColumn("N", "", "", "");
                tddet.AddColumn("E", "","", "");
                tddet.AddColumn("V", "","", "");
                tddet.AddColumn("S", "", "", "");
                tddet.AddColumn("M", "",Css.right, "");
                tddet.AddColumn("C", "",Css.right, "");
                tddet.AddColumn("S", "", Css.right, "");
                tddet.AddColumn("V", Css.mini, Css.right, "");*/
                tddet.AddColumn("Documento", "", "", "");
                if (obj.com_tipodoc == Constantes.cPago.tpd_codigo || obj.com_tipodoc == Constantes.cPagoBan.tpd_codigo || obj.com_tipodoc == Constantes.cDiario.tpd_codigo)
                {

                    tddet.AddColumn("Factura", "", "", "");
                    tddet.AddColumn("Emisión", "", "", "");
                }
                else
                {
                    tddet.AddColumn("Guia", "", "", "");
                    tddet.AddColumn("Nro", "", "", "");
                }
                //tddet.AddColumn("Emisión", "", "", "");
                //tddet.AddColumn("Vence", "", "", "");
                tddet.AddColumn("Socio", "", "", "");
                tddet.AddColumn("Sub 0", "", Css.right, "");
                tddet.AddColumn("Sub IVA", "", Css.right, "");
                tddet.AddColumn("IVA", "", Css.right, "");
                tddet.AddColumn("Monto", "", Css.right, "");
                tddet.AddColumn("Cancel", "", Css.right, "");
                tddet.AddColumn("Saldo", "", Css.right, "");
                tddet.AddColumn("Valor", Css.mini, Css.right, "");

                foreach (vDdocumento item in lista)
                {
                    decimal saldo = item.ddo_monto.Value - item.ddo_cancela.Value;
                    if (Math.Round(saldo,2) > 0)
                    {
                        HtmlRow row = new HtmlRow();
                        row.data = "data-comprobante=" + item.ddo_comprobante + " data-transac=" + item.ddo_transacc + " data-doctran= " + item.ddo_doctran + " data-pago=" + item.ddo_pago;
                        row.clickevent = "EditAfec(this)";

                        row.cells.Add(new HtmlCell { valor = item.ddo_doctran });

                        if (obj.com_tipodoc == Constantes.cPago.tpd_codigo || obj.com_tipodoc == Constantes.cPagoBan.tpd_codigo || obj.com_tipodoc == Constantes.cDiario.tpd_codigo)
                        {
                            row.cells.Add(new HtmlCell { valor = item.cdoc_aut_factura });
                            row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi });

                        }
                        else
                        {
                            row.cells.Add(new HtmlCell { valor = item.com_doctran_guia });
                            row.cells.Add(new HtmlCell { valor = item.ddo_pago });
                        }


                        if (item.com_doctran_guia != null)
                            row.cells.Add(new HtmlCell { valor = item.per_razon_guia });
                        else
                            row.cells.Add(new HtmlCell { valor = item.per_razon });


                        row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.tot_subtot_0 + item.tot_transporte), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(item.tot_subtot_0 + item.tot_transporte) + "'" });
                        row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.tot_subtotal + item.tot_tseguro), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(item.tot_subtotal + item.tot_tseguro) + "'" });
                        row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.tot_impuesto), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(item.tot_impuesto) + "'" });

                        row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_monto), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(item.ddo_monto) + "'" });
                        row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_cancela), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(item.ddo_cancela) + "'" });


                        //row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(saldo), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(saldo) + "'" });
                        row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(saldo) + new Boton { small = true, id = "btnvsaldo_P", tooltip = "Enviar Saldo ", clase = "iconsweets-arrowright", click = "(parseFloat($(\"#txtSALDO_P\").val()) >= parseFloat(\"" + Formatos.CurrencyFormat(saldo) + "\")?$(this).closest(\"td\").next().find(\"input\").val(\"" + Formatos.CurrencyFormat(saldo) + "\"):$(this).closest(\"td\").next().find(\"input\").val($(\"#txtSALDO_P\").val()));CalculaAfectacion();" }.ToString(), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(saldo) + "'" });
                        row.cells.Add(new HtmlCell { valor = new Input() { clase = Css.mini + Css.amount, valor = 0.00 }.ToString(), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(saldo) + "' onkeydown=\"CalculaAfectacion()\"" });
                        tddet.AddRow(row);
                    }


                    /* row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi.Value.ToShortDateString() });
                     row.cells.Add(new HtmlCell { valor = item.ddo_fecha_ven.Value.ToShortDateString() });
                     if (item.com_doctran_guia != null)
                         row.cells.Add(new HtmlCell { valor = item.per_razon_guia });
                     else
                         row.cells.Add(new HtmlCell { valor = item.per_razon });
                     row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_monto), clase = Css.right });
                     row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_cancela), clase = Css.right });
                     decimal saldo = item.ddo_monto.Value - item.ddo_cancela.Value;
                     row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(saldo), clase = Css.right });
                     row.cells.Add(new HtmlCell { valor = new Input() { clase = Css.mini + Css.amount, valor = 0.00 }.ToString(), clase = Css.right, data = "onkeydown=\"CalculaAfectacion()\"" });

                     */


                    /*row.cells.Add(new HtmlCell { valor = item.ddo_doctran, ancho = 150 });
                    row.cells.Add(new HtmlCell { valor = item.com_doctran_guia, ancho = 150 });
                    row.cells.Add(new HtmlCell { valor = item.ddo_pago, ancho = 40 });
                    row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi.Value.ToShortDateString(), ancho = 60 });
                    row.cells.Add(new HtmlCell { valor = item.ddo_fecha_ven.Value.ToShortDateString(), ancho = 60 });
                    if (item.com_doctran_guia != null)
                        row.cells.Add(new HtmlCell { valor = item.per_razon_guia, ancho = 200 });
                    else
                        row.cells.Add(new HtmlCell { valor = item.per_razon, ancho = 200 });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_monto), clase = Css.right, ancho = 60 });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_cancela), clase = Css.right, ancho = 60 });
                    decimal saldo = item.ddo_monto.Value - item.ddo_cancela.Value;
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(saldo), clase = Css.right, ancho = 60 });
                    row.cells.Add(new HtmlCell { valor = new Input() { clase = Css.mini + Css.amount, valor = 0.00 }.ToString(), clase = Css.right, ancho = 80 });*/
                  
                    //tdatos.AddRow(new HtmlRow(item.ddoc_productoid, item.ddoc_productonombre, item.ddoc_observaciones, item.ddoc_productounidad, item.ddoc_cantidad, item.ddoc_precio, item.ddoc_dscitem, item.ddoc_total, item.ddoc_productoiva) { data = "data-codpro=" + item.ddoc_producto });   

                }
                html.AppendLine(tddet.ToString());
                html.AppendLine(" </div><!--span11-->");
                html.AppendLine("</div><!--row-fluid-->");


            }

            return html.ToString();

        }


        [WebMethod]
        public string GetAfectacionesFromFAC(object objeto)
        {
            StringBuilder html = new StringBuilder();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object compcan = null;
                object compref = null;
                string obliga = "S";

                tmp.TryGetValue("compcan", out compcan);
                tmp.TryGetValue("compref", out compref);


                Comprobante obj = new Comprobante(compcan);
                Comprobante objref = new Comprobante(compref);
                objref.total = new Total();
                objref.total.tot_empresa = objref.com_empresa;
                objref.total.tot_empresa_key = objref.com_empresa;
                objref.total.tot_comprobante = objref.com_codigo;
                objref.total.tot_comprobante_key = objref.com_codigo;
                objref.total = TotalBLL.GetByPK(objref.total);


                Persona persona = new Persona();
                persona.per_empresa = obj.com_empresa;
                persona.per_empresa_key = obj.com_empresa;
                if (obj.com_codclipro.HasValue)
                {
                    persona.per_codigo = obj.com_codclipro.Value;
                    persona.per_codigo_key = obj.com_codclipro.Value;
                    persona = PersonaBLL.GetByPK(persona);
                }

                decimal total = obj.recibos.Sum(pkg => pkg.dfp_monto);
                if (total == 0)
                    total = obj.notascre.Sum(pkg => pkg.dnc_valor) ?? 0;

                int debcre = 0;
                if (obj.com_tipodoc == Constantes.cRecibo.tpd_codigo)
                {
                    debcre = Constantes.cDebito;
                }

                else if (obj.com_tipodoc == Constantes.cPago.tpd_codigo)
                {
                    debcre = Constantes.cCredito;
                }

                else if (obj.com_tipodoc == Constantes.cNotacre.tpd_codigo)
                {
                    debcre = Constantes.cDebito;
                }
                else if (obj.com_tipodoc == Constantes.cNotadeb.tpd_codigo)
                {
                    debcre = Constantes.cCredito;
                }
                else if (obj.com_tipodoc == Constantes.cNotacrePro.tpd_codigo)
                {
                    debcre = Constantes.cCredito;
                }
                else if (obj.com_tipodoc == Constantes.cNotadebPro.tpd_codigo)
                {
                    debcre = Constantes.cDebito;
                }

                if (obj.com_tipodoc == Constantes.cDiario.tpd_codigo)
                {

                    debcre = (obj.com_tclipro == Constantes.cCliente) ? Constantes.cDebito : Constantes.cCredito;
                    total = obj.total.tot_total;
                }

                Auto.actualiza_documentos(obj.com_empresa, null, null, obj.com_codclipro, objref.com_codigo, null, null, 0);

                List<vDdocumento> lista = vDdocumentoBLL.GetAll(new WhereParams("ddo_empresa={0} AND ddo_cancelado=0 AND ddo_codclipro={1} AND ddo_debcre ={2} AND ddo_comprobante ={3}", obj.com_empresa, obj.com_codclipro, debcre, objref.com_codigo), "");
                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span5\">");
                HtmlTable tdatos = new HtmlTable();
                tdatos.CreteEmptyTable(2, 2);
                tdatos.rows[0].cells[0].valor = "Cliente/Proveedor:";
                tdatos.rows[0].cells[1].valor = new Input { id = "txtCODCLIPRO_P", valor = persona.per_id, clase = Css.mini, habilitado = false }.ToString() + " " + new Input { id = "txtNOMBRES_P", clase = Css.large, habilitado = false, valor = persona.per_apellidos + " " + persona.per_nombres }.ToString() + new Input { id = "txtCODPER_P", visible = false, valor = persona.per_codigo }.ToString();
                tdatos.rows[1].cells[0].valor = "Documento:";
                tdatos.rows[1].cells[1].valor = new Input { id = "txtDOCUMETO_P", clase = Css.medium, habilitado = false, valor = obj.com_doctran }.ToString() + new Input { id = "txtDEBCRE_P", visible = false, valor = debcre }.ToString();

                html.AppendLine(tdatos.ToString());
                html.AppendLine(" </div><!--span5-->");


                html.AppendLine("<div class=\"span3\">");
                HtmlTable tdatos1 = new HtmlTable();
                tdatos1.CreteEmptyTable(3, 2);
                tdatos1.rows[0].cells[0].valor = "Valor:";
                //tdatos1.rows[0].cells[1].valor = new Input { id = "txtVALOR_P", clase = Css.small, valor = planilla.total.tot_total.ToString().Replace(",", "."), habilitado = false }.ToString();
                tdatos1.rows[0].cells[1].valor = new Input { id = "txtVALOR_P", clase = Css.small, valor = total.ToString().Replace(",", "."), habilitado = false }.ToString();

                tdatos1.rows[1].cells[0].valor = "Saldo:";
                tdatos1.rows[1].cells[1].valor = new Input { id = "txtSALDO_P", clase = Css.small, valor = total.ToString().Replace(",", "."), habilitado = false }.ToString() + new Input { id = "txtOBLIGA_P", visible = false, valor = obliga }.ToString();

                tdatos1.rows[2].cells[0].valor = "";
                tdatos1.rows[2].cells[1].valor = new Boton { click = "CleanValores();return false;", valor = "Limpiar" }.ToString();

                html.AppendLine(tdatos1.ToString());
                html.AppendLine(" </div><!--span3-->");


                html.AppendLine("<div class=\"span3\">");
                HtmlTable tdatos2 = new HtmlTable();
                tdatos2.CreteEmptyTable(2, 3);
                tdatos2.rows[0].cells[0].valor = "% :";
                tdatos2.rows[0].cells[1].valor = new Input { id = "txtDESCPORCENTAJE_P", clase = Css.mini }.ToString();
                tdatos2.rows[0].cells[2].valor = new Select { id = "cmbDESCPORCENTAJE_P", clase = Css.mini, diccionario = Dictionaries.GetTotales(), withempty = true } + new Boton { small = true, id = "btnpall_P", tooltip = "Decontar % a todos ", clase = "iconsweets-arrowdown", click = "CalcPorcentaje();" }.ToString();
                tdatos2.rows[1].cells[0].valor = "$ :";
                tdatos2.rows[1].cells[1].valor = new Input { id = "txtDESCVALOR_P", clase = Css.mini }.ToString();
                tdatos2.rows[1].cells[2].valor = new Select { id = "cmbDESCVALOR_P", clase = Css.mini, diccionario = Dictionaries.GetTotales(), withempty = true } + new Boton { small = true, id = "btnvall_P", tooltip = "Decontar $ a todos ", clase = "iconsweets-arrowdown", click = "CalcValor();" }.ToString();

                html.AppendLine(tdatos2.ToString());
                html.AppendLine(" </div><!--span3-->");


                html.AppendLine("</div><!--row-fluid-->");
                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span11\">");


                HtmlTable tddet = new HtmlTable();
                tddet.id = "tdafec_P";
                tddet.invoice = true;
                tddet.clase = "scrolltable";
                tddet.footer = true;

                /*tddet.AddColumn("D", "", "", "");
                tddet.AddColumn("N", "", "", "");
                tddet.AddColumn("E", "","", "");
                tddet.AddColumn("V", "","", "");
                tddet.AddColumn("S", "", "", "");
                tddet.AddColumn("M", "",Css.right, "");
                tddet.AddColumn("C", "",Css.right, "");
                tddet.AddColumn("S", "", Css.right, "");
                tddet.AddColumn("V", Css.mini, Css.right, "");*/

                tddet.AddColumn("Documento", "", "", "");
                if (obj.com_tipodoc == Constantes.cPago.tpd_codigo || obj.com_tipodoc == Constantes.cPagoBan.tpd_codigo)
                {

                    tddet.AddColumn("Factura", "", "", "");
                    tddet.AddColumn("Emisión", "", "", "");
                }
                else
                {
                    tddet.AddColumn("Guia", "", "", "");
                    tddet.AddColumn("Nro", "", "", "");
                }

                //tddet.AddColumn("Emisión", "", "", "");
                //tddet.AddColumn("Vence", "", "", "");
                tddet.AddColumn("Socio", "", "", "");
                tddet.AddColumn("Sub 0", "", Css.right, "");
                tddet.AddColumn("Sub IVA", "", Css.right, "");
                tddet.AddColumn("IVA", "", Css.right, "");
                tddet.AddColumn("Monto", "", Css.right, "");
                tddet.AddColumn("Cancel", "", Css.right, "");
                tddet.AddColumn("Saldo", "", Css.right, "");
                tddet.AddColumn("Valor", Css.mini, Css.right, "");

                decimal valor = objref.total.tot_total;

                foreach (vDdocumento item in lista)
                {
                    HtmlRow row = new HtmlRow();
                    //row.data = "data-comprobante=" + item.ddo_comprobante + " data-transac=" + item.ddo_transacc;
                    row.data = "data-comprobante=" + item.ddo_comprobante + " data-transac=" + item.ddo_transacc + " data-doctran= " + item.ddo_doctran + " data-pago=" + item.ddo_pago;
                    row.clickevent = "EditAfec(this)";

                    row.cells.Add(new HtmlCell { valor = item.ddo_doctran });

                    if (obj.com_tipodoc == Constantes.cPago.tpd_codigo || obj.com_tipodoc == Constantes.cPagoBan.tpd_codigo)
                    {
                        row.cells.Add(new HtmlCell { valor = item.cdoc_aut_factura });
                        row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi });

                    }
                    else
                    {
                        row.cells.Add(new HtmlCell { valor = item.com_doctran_guia });
                        row.cells.Add(new HtmlCell { valor = item.ddo_pago });
                    }


                    //row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi.Value.ToShortDateString() });
                    //row.cells.Add(new HtmlCell { valor = item.ddo_fecha_ven.Value.ToShortDateString() });
                    if (item.com_doctran_guia != null)
                        row.cells.Add(new HtmlCell { valor = item.per_razon_guia });
                    else
                        row.cells.Add(new HtmlCell { valor = item.per_razon });


                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.tot_subtot_0 + item.tot_transporte), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(item.tot_subtot_0 + item.tot_transporte) + "'" });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.tot_subtotal + item.tot_tseguro), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(item.tot_subtotal + item.tot_tseguro) + "'" });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.tot_impuesto), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(item.tot_impuesto) + "'" });

                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_monto), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(item.ddo_monto) + "'" });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_cancela), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(item.ddo_cancela) + "'" });
                    //decimal saldo = item.ddo_monto.Value - item.ddo_cancela.Value;
                    //row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(saldo), clase = Css.right });
                    //row.cells.Add(new HtmlCell { valor = new Input() { clase = Css.mini + Css.amount, valor = Formatos.CurrencyFormat(valor) }.ToString(), clase = Css.right });


                    decimal saldo = item.ddo_monto.Value - item.ddo_cancela.Value;
                    //row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(saldo), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(saldo) + "'" });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(saldo) + new Boton { small = true, id = "btnvsaldo_P", tooltip = "Enviar Saldo ", clase = "iconsweets-arrowright", click = "(parseFloat($(\"#txtSALDO_P\").val()) >= parseFloat(\"" + Formatos.CurrencyFormat(saldo) + "\")?$(this).closest(\"td\").next().find(\"input\").val(\"" + Formatos.CurrencyFormat(saldo) + "\"):$(this).closest(\"td\").next().find(\"input\").val($(\"#txtSALDO_P\").val()));CalculaAfectacion();" }.ToString(), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(saldo) + "'" });
                    row.cells.Add(new HtmlCell { valor = new Input() { clase = Css.mini + Css.amount, valor = Formatos.CurrencyFormat(saldo) }.ToString(), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(saldo) + "'" });

                    /*row.cells.Add(new HtmlCell { valor = item.ddo_doctran, ancho = 150 });
                    row.cells.Add(new HtmlCell { valor = item.com_doctran_guia, ancho = 150 });
                    row.cells.Add(new HtmlCell { valor = item.ddo_pago, ancho = 40 });
                    row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi.Value.ToShortDateString(), ancho = 60 });
                    row.cells.Add(new HtmlCell { valor = item.ddo_fecha_ven.Value.ToShortDateString(), ancho = 60 });
                    if (item.com_doctran_guia != null)
                        row.cells.Add(new HtmlCell { valor = item.per_razon_guia, ancho = 200 });
                    else
                        row.cells.Add(new HtmlCell { valor = item.per_razon, ancho = 200 });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_monto), clase = Css.right, ancho = 60 });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_cancela), clase = Css.right, ancho = 60 });
                    decimal saldo = item.ddo_monto.Value - item.ddo_cancela.Value;
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(saldo), clase = Css.right, ancho = 60 });
                    row.cells.Add(new HtmlCell { valor = new Input() { clase = Css.mini + Css.amount, valor = 0.00 }.ToString(), clase = Css.right, ancho = 80 });*/
                    tddet.AddRow(row);
                    //tdatos.AddRow(new HtmlRow(item.ddoc_productoid, item.ddoc_productonombre, item.ddoc_observaciones, item.ddoc_productounidad, item.ddoc_cantidad, item.ddoc_precio, item.ddoc_dscitem, item.ddoc_total, item.ddoc_productoiva) { data = "data-codpro=" + item.ddoc_producto });   

                }
                html.AppendLine(tddet.ToString());
                html.AppendLine(" </div><!--span11-->");
                html.AppendLine("</div><!--row-fluid-->");


            }

            return html.ToString();

        }

        [WebMethod]
        public string GetAfectacionesFromLGC(object objeto)
        {
            StringBuilder html = new StringBuilder();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object compcan = null;
                object compref = null;
                string obliga = "S";

                tmp.TryGetValue("compcan", out compcan);
                tmp.TryGetValue("compref", out compref);

                int tipofac = Constantes.cFactura.tpd_codigo;

                Comprobante obj = new Comprobante(compcan);
                decimal total = obj.recibos.Sum(pkg => pkg.dfp_monto);
                //if (total == 0)
                //    total = obj.notascre.Sum(pkg => pkg.dnc_valor) ?? 0;

                Comprobante planilla = new Comprobante(compref);
                planilla.com_empresa_key = planilla.com_empresa;
                planilla.com_codigo_key = planilla.com_codigo;
                planilla = ComprobanteBLL.GetByPK(planilla);

                planilla.total = new Total();
                planilla.total.tot_empresa = planilla.com_empresa;
                planilla.total.tot_empresa_key = planilla.com_empresa;
                planilla.total.tot_comprobante = planilla.com_codigo;
                planilla.total.tot_comprobante_key = planilla.com_codigo;
                planilla.total = TotalBLL.GetByPK(planilla.total);

                Persona persona = new Persona();
                persona.per_empresa = obj.com_empresa;
                persona.per_empresa_key = obj.com_empresa;
                if (obj.com_codclipro.HasValue)
                {
                    persona.per_codigo = obj.com_codclipro.Value;
                    persona.per_codigo_key = obj.com_codclipro.Value;
                    persona = PersonaBLL.GetByPK(persona);
                }
                int debcre = Constantes.cDebito;

                //Auto.actualiza_documentos(obj.com_empresa, null, null, obj.com_codclipro, null, null, null, 1);

                List<vDdocumento> lista = new List<vDdocumento>();
                List<Planillacomprobante> lst = PlanillacomprobanteBLL.GetAll(new WhereParams("pco_empresa ={0} and pco_comprobante_pla={1}", planilla.com_empresa, planilla.com_codigo), "");
                if (lst.Count > 0)
                {
                    Comprobante comprobante = new Comprobante();
                    comprobante.com_empresa = lst[0].pco_empresa;
                    comprobante.com_empresa_key = lst[0].pco_empresa;
                    comprobante.com_codigo = lst[0].pco_comprobante_fac;
                    comprobante.com_codigo_key = lst[0].pco_comprobante_fac;
                    comprobante = ComprobanteBLL.GetByPK(comprobante);

                    comprobante.total = new Total();
                    comprobante.total.tot_empresa = comprobante.com_empresa;
                    comprobante.total.tot_empresa_key = comprobante.com_empresa;
                    comprobante.total.tot_comprobante = comprobante.com_codigo;
                    comprobante.total.tot_comprobante_key = comprobante.com_codigo;
                    comprobante.total = TotalBLL.GetByPK(comprobante.total);



                    lista = vDdocumentoBLL.GetAll(new WhereParams("ddo_empresa={0} AND ddo_cancelado=0 AND ddo_codclipro={1} AND ddo_debcre ={2} AND ddo_comprobante ={3}", obj.com_empresa, obj.com_codclipro, debcre, comprobante.com_codigo), "");
                }
                else
                {
                    List<Planillacli> lstcli = PlanillacliBLL.GetAll(new WhereParams("plc_empresa={0} and plc_comprobante_pla={1}", planilla.com_empresa, planilla.com_codigo), "");
                    //WhereParams parametros = new WhereParams("ddo_empresa={0} AND ddo_cancelado=0 AND ddo_codclipro={1} AND ddo_debcre ={2} ", planilla.com_empresa, planilla.com_codclipro, debcre);
                    WhereParams parametros = new WhereParams("ddo_empresa={0} AND ddo_cancelado=0 AND ddo_debcre ={1} ", planilla.com_empresa, debcre);
                    string where = "";
                    foreach (Planillacli item in lstcli)
                    {
                        if (item.plc_comprobantetipodoc.Value == tipofac)
                            where += ((where != "") ? " or " : "") + " ddo_comprobante = " + item.plc_comprobante;
                    }
                    if (where != "")
                    {
                        parametros.where += "and (" + where + ")";
                        lista = vDdocumentoBLL.GetAll(parametros, "");
                    }

                }
                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span5\">");
                HtmlTable tdatos = new HtmlTable();
                tdatos.CreteEmptyTable(2, 2);
                tdatos.rows[0].cells[0].valor = "Cliente/Proveedor:";
                tdatos.rows[0].cells[1].valor = new Input { id = "txtCODCLIPRO_P", valor = persona.per_id, clase = Css.mini, habilitado = false }.ToString() + " " + new Input { id = "txtNOMBRES_P", clase = Css.large, habilitado = false, valor = persona.per_apellidos + " " + persona.per_nombres }.ToString() + new Input { id = "txtCODPER_P", visible = false, valor = persona.per_codigo }.ToString();
                tdatos.rows[1].cells[0].valor = "Documento:";
                tdatos.rows[1].cells[1].valor = new Input { id = "txtDOCUMETO_P", clase = Css.medium, habilitado = false, valor = obj.com_doctran }.ToString() + new Input { id = "txtDEBCRE_P", visible = false, valor = debcre }.ToString();

                html.AppendLine(tdatos.ToString());
                html.AppendLine(" </div><!--span5-->");
                html.AppendLine("<div class=\"span3\">");
                HtmlTable tdatos1 = new HtmlTable();
                tdatos1.CreteEmptyTable(3, 2);
                tdatos1.rows[0].cells[0].valor = "Valor:";
                //tdatos1.rows[0].cells[1].valor = new Input { id = "txtVALOR_P", clase = Css.small, valor = planilla.total.tot_total.ToString().Replace(",", "."), habilitado = false }.ToString();
                tdatos1.rows[0].cells[1].valor = new Input { id = "txtVALOR_P", clase = Css.small, valor = total.ToString().Replace(",", "."), habilitado = false }.ToString();

                tdatos1.rows[1].cells[0].valor = "Saldo:";
                tdatos1.rows[1].cells[1].valor = new Input { id = "txtSALDO_P", clase = Css.small, valor = total.ToString().Replace(",", "."), habilitado = false }.ToString() + new Input { id = "txtOBLIGA_P", visible = false, valor = obliga }.ToString();

                tdatos1.rows[2].cells[0].valor = "";
                tdatos1.rows[2].cells[1].valor = new Boton { click = "CleanValores();return false;", valor = "Limpiar" }.ToString();

                html.AppendLine(tdatos1.ToString());
                html.AppendLine(" </div><!--span3-->");


                html.AppendLine("<div class=\"span3\">");
                HtmlTable tdatos2 = new HtmlTable();
                tdatos2.CreteEmptyTable(2, 3);
                tdatos2.rows[0].cells[0].valor = "% :";
                tdatos2.rows[0].cells[1].valor = new Input { id = "txtDESCPORCENTAJE_P", clase = Css.mini }.ToString();
                tdatos2.rows[0].cells[2].valor = new Select { id = "cmbDESCPORCENTAJE_P", clase = Css.mini, diccionario = Dictionaries.GetTotales(), withempty = true } + new Boton { small = true, id = "btnpall_P", tooltip = "Decontar % a todos ", clase = "iconsweets-arrowdown", click = "CalcPorcentaje();" }.ToString();
                tdatos2.rows[1].cells[0].valor = "$ :";
                tdatos2.rows[1].cells[1].valor = new Input { id = "txtDESCVALOR_P", clase = Css.mini }.ToString();
                tdatos2.rows[1].cells[2].valor = new Select { id = "cmbDESCVALOR_P", clase = Css.mini, diccionario = Dictionaries.GetTotales(), withempty = true } + new Boton { small = true, id = "btnvall_P", tooltip = "Decontar $ a todos ", clase = "iconsweets-arrowdown", click = "CalcValor();" }.ToString();

                html.AppendLine(tdatos2.ToString());
                html.AppendLine(" </div><!--span3-->");




                html.AppendLine("</div><!--row-fluid-->");
                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span11\">");
                HtmlTable tddet = new HtmlTable();
                tddet.id = "tdafec_P";
                tddet.invoice = true;
                tddet.clase = "scrolltable";
                tddet.footer = true;


                tddet.AddColumn("Documento", "", "", "");
                tddet.AddColumn("Guia", "", "", "");
                tddet.AddColumn("Nro", "", "", "");
                //tddet.AddColumn("Emisión", "", "", "");
                //tddet.AddColumn("Vence", "", "", "");
                tddet.AddColumn("Socio", "", "", "");
                tddet.AddColumn("Subtot 0", "", Css.right, "");
                tddet.AddColumn("Subtot IVA", "", Css.right, "");
                tddet.AddColumn("IVA", "", Css.right, "");
                //tddet.AddColumn("TOTAL", "", Css.right, "");

                tddet.AddColumn("Monto", "", Css.right, "");
                tddet.AddColumn("Cancel", "", Css.right, "");
                tddet.AddColumn("Saldo", "", Css.right, "");
                tddet.AddColumn("Valor", Css.mini, Css.right, "");

                //decimal valor = comprobante.total.tot_total;

                foreach (vDdocumento item in lista)
                {
                    HtmlRow row = new HtmlRow();
                    //row.data = "data-comprobante=" + item.ddo_comprobante + " data-transac=" + item.ddo_transacc 
                    row.data = "data-comprobante=" + item.ddo_comprobante + " data-transac=" + item.ddo_transacc + " data-doctran= " + item.ddo_doctran + " data-pago=" + item.ddo_pago + " data-subtotal='" + Formatos.CurrencyFormat(item.tot_subtot_0) + "'";
                    row.clickevent = "EditAfec(this)";

                    row.cells.Add(new HtmlCell { valor = item.ddo_doctran });
                    row.cells.Add(new HtmlCell { valor = item.com_doctran_guia });
                    row.cells.Add(new HtmlCell { valor = item.ddo_pago });
                    //row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi.Value.ToShortDateString() });
                    //row.cells.Add(new HtmlCell { valor = item.ddo_fecha_ven.Value.ToShortDateString() });


                    if (item.com_doctran_guia != null)
                        row.cells.Add(new HtmlCell { valor = item.per_razon_guia });
                    else
                        row.cells.Add(new HtmlCell { valor = item.per_razon });

                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.tot_subtot_0 + item.tot_transporte), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(item.tot_subtot_0 + item.tot_transporte) + "'" });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.tot_subtotal + item.tot_tseguro), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(item.tot_subtotal + item.tot_tseguro) + "'" });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.tot_impuesto), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(item.tot_impuesto) + "'" });
                    //row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.total), clase = Css.right });


                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_monto), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(item.ddo_monto) + "'" });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_cancela), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(item.ddo_cancela) + "'" });
                    decimal saldo = item.ddo_monto.Value - item.ddo_cancela.Value;
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(saldo) + new Boton { small = true, id = "btnvsaldo_P", tooltip = "Enviar Saldo ", clase = "iconsweets-arrowright", click = "(parseFloat($(\"#txtSALDO_P\").val()) >= parseFloat(\"" + Formatos.CurrencyFormat(saldo) + "\")?$(this).closest(\"td\").next().find(\"input\").val(\"" + Formatos.CurrencyFormat(saldo) + "\"):$(this).closest(\"td\").next().find(\"input\").val($(\"#txtSALDO_P\").val()));CalculaAfectacion();" }.ToString(), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(saldo) + "'" });
                    row.cells.Add(new HtmlCell { valor = new Input() { clase = Css.mini + Css.amount, valor = Formatos.CurrencyFormat(saldo) }.ToString(), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(saldo) + "'" });


                    tddet.AddRow(row);


                }
                html.AppendLine(tddet.ToString());
                html.AppendLine(" </div><!--span11-->");
                html.AppendLine("</div><!--row-fluid-->");



            }

            return html.ToString();

        }

        [WebMethod]
        public string GetDeudas(object objeto)
        {
            StringBuilder html = new StringBuilder();
            if (objeto != null)
            {
                Comprobante obj = new Comprobante(objeto);

                Persona persona = new Persona();
                persona.per_empresa = obj.com_empresa;
                persona.per_empresa_key = obj.com_empresa;
                if (obj.com_codclipro.HasValue)
                {
                    persona.per_codigo = obj.com_codclipro.Value;
                    persona.per_codigo_key = obj.com_codclipro.Value;
                    persona = PersonaBLL.GetByPK(persona);
                }

                int debcre = 0;
                if (obj.com_tipodoc == Constantes.cRecibo.tpd_codigo)
                {
                    debcre = Constantes.cDebito;
                }
                else if (obj.com_tipodoc == Constantes.cPago.tpd_codigo)
                {
                    debcre = Constantes.cCredito;
                }


                else if (obj.com_tipodoc == Constantes.cNotacre.tpd_codigo)
                {
                    debcre = Constantes.cDebito;
                }
                else if (obj.com_tipodoc == Constantes.cNotadeb.tpd_codigo)
                {
                    debcre = Constantes.cCredito;
                }
                else if (obj.com_tipodoc == Constantes.cNotacrePro.tpd_codigo)
                {
                    debcre = Constantes.cCredito;
                }
                else if (obj.com_tipodoc == Constantes.cNotadebPro.tpd_codigo)
                {
                    debcre = Constantes.cDebito;
                }
                else if (obj.com_tipodoc == Constantes.cDiario.tpd_codigo)
                {

                    debcre = (obj.com_tclipro == Constantes.cCliente) ? Constantes.cDebito : Constantes.cCredito;

                }
                Auto.actualiza_documentos(obj.com_empresa, null, null, obj.com_codclipro, null, null, null, 1);
                List<vDdocumento> lista = vDdocumentoBLL.GetAll(new WhereParams("ddo_empresa={0} AND ddo_cancelado=0 AND ddo_codclipro={1} AND ddo_debcre ={2}", obj.com_empresa, obj.com_codclipro, debcre), "");


                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span8\">");
                HtmlTable tdatos = new HtmlTable();
                tdatos.CreteEmptyTable(2, 2);
                tdatos.rows[0].cells[0].valor = "Cliente/Proveedor:";
                tdatos.rows[0].cells[1].valor = new Input { id = "txtCODCLIPRO_P", valor = persona.per_id, clase = Css.mini, habilitado = false }.ToString() + " " + new Input { id = "txtNOMBRES_P", clase = Css.large, habilitado = false, valor = persona.per_apellidos + " " + persona.per_nombres }.ToString() + new Input { id = "txtCODPER_P", visible = false, valor = persona.per_codigo }.ToString();
                tdatos.rows[1].cells[0].valor = "Documento:";
                tdatos.rows[1].cells[1].valor = new Input { id = "txtDOCUMETO_P", clase = Css.medium, habilitado = false, valor = obj.com_doctran }.ToString();

                html.AppendLine(tdatos.ToString());
                html.AppendLine(" </div><!--span8-->");                
                html.AppendLine("<div class=\"span3\">");
                HtmlTable tdatos1 = new HtmlTable();
                tdatos1.CreteEmptyTable(1, 2);
                tdatos1.rows[0].cells[0].valor = "Total:";
                tdatos1.rows[0].cells[1].valor = new Input { id = "txtVALOR_P", clase = Css.small, valor = Formatos.CurrencyFormat(lista.Sum(s=>s.ddo_monto - s.ddo_cancela)), habilitado = false }.ToString();

                html.AppendLine(tdatos1.ToString());
                html.AppendLine(" </div><!--span3-->");
                html.AppendLine("</div><!--row-fluid-->");



                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span11\">");


                HtmlTable tddet = new HtmlTable();
                tddet.id = "tdafec1_P";
                tddet.invoice = true;
                tddet.clase = "scrolltable";
                tddet.footer = true;

                tddet.AddColumn("Documento", "", "", "");
                if (obj.com_tipodoc == Constantes.cPago.tpd_codigo || obj.com_tipodoc == Constantes.cPagoBan.tpd_codigo)
                {

                    tddet.AddColumn("Factura", "", "", "");
                    tddet.AddColumn("Emisión", "", "", "");
                }
                else
                {
                    tddet.AddColumn("Guia", "", "", "");
                    tddet.AddColumn("Nro", "", "", "");
                }

                //tddet.AddColumn("Guia", "", "", "");
                //tddet.AddColumn("Nro", "", "", "");

                tddet.AddColumn("Emisión", "", "", "");
                tddet.AddColumn("Vence", "", "", "");
                tddet.AddColumn("Socio", "", "", "");
                tddet.AddColumn("Monto", "", Css.right, "");
                tddet.AddColumn("Cancel", "", Css.right, "");
                tddet.AddColumn("Saldo", "", Css.right, "");

                foreach (vDdocumento item in lista)
                {

                    decimal saldo = item.ddo_monto.Value - item.ddo_cancela.Value;

                    if (Math.Round(saldo, 2) > 0)
                    {

                        HtmlRow row = new HtmlRow();
                        //row.data = "data-comprobante=" + item.ddo_comprobante + " data-transac=" + item.ddo_transacc;
                        row.data = "data-comprobante=" + item.ddo_comprobante + " data-transac=" + item.ddo_transacc + " data-doctran= " + item.ddo_doctran + " data-pago=" + item.ddo_pago;

                        row.cells.Add(new HtmlCell { valor = item.ddo_doctran });

                        if (obj.com_tipodoc == Constantes.cPago.tpd_codigo || obj.com_tipodoc == Constantes.cPagoBan.tpd_codigo)
                        {
                            row.cells.Add(new HtmlCell { valor = item.cdoc_aut_factura });
                            row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi });

                        }
                        else
                        {
                            row.cells.Add(new HtmlCell { valor = item.com_doctran_guia });
                            row.cells.Add(new HtmlCell { valor = item.ddo_pago });
                        }

                        //row.cells.Add(new HtmlCell { valor = item.com_doctran_guia });
                        //row.cells.Add(new HtmlCell { valor = item.ddo_pago });
                        row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi.Value.ToShortDateString() });
                        row.cells.Add(new HtmlCell { valor = item.ddo_fecha_ven.Value.ToShortDateString() });
                        if (item.com_doctran_guia != null)
                            row.cells.Add(new HtmlCell { valor = item.per_razon_guia });
                        else
                            row.cells.Add(new HtmlCell { valor = item.per_razon });
                        row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_monto), clase = Css.right });
                        row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_cancela), clase = Css.right });

                        row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(saldo), clase = Css.right });
                        tddet.AddRow(row);

                    }
                   

                }
                html.AppendLine(tddet.ToString());
                html.AppendLine(" </div><!--span11-->");
                html.AppendLine("</div><!--row-fluid-->");
            }

            return html.ToString();

        }



        [WebMethod]
        public string GetAfectaDeudas(object objeto)
        {
            StringBuilder html = new StringBuilder();
            if (objeto != null)
            {
                Comprobante obj = new Comprobante(objeto);
                obj.com_empresa_key = obj.com_empresa;
                obj.com_codigo_key = obj.com_codigo;
                obj = ComprobanteBLL.GetByPK(obj);

                Persona persona = new Persona();
                persona.per_empresa = obj.com_empresa;
                persona.per_empresa_key = obj.com_empresa;
                if (obj.com_codclipro.HasValue)
                {
                    persona.per_codigo = obj.com_codclipro.Value;
                    persona.per_codigo_key = obj.com_codclipro.Value;
                    persona = PersonaBLL.GetByPK(persona);
                }

                int debcre = 0;
                if (obj.com_tipodoc == Constantes.cRecibo.tpd_codigo)
                {
                    debcre = Constantes.cDebito;
                }
                else if (obj.com_tipodoc == Constantes.cPago.tpd_codigo)
                {
                    debcre = Constantes.cCredito;
                }


                else if (obj.com_tipodoc == Constantes.cNotacre.tpd_codigo)
                {
                    debcre = Constantes.cDebito;
                }
                else if (obj.com_tipodoc == Constantes.cNotadeb.tpd_codigo)
                {
                    debcre = Constantes.cCredito;
                }
                else if (obj.com_tipodoc == Constantes.cNotacrePro.tpd_codigo)
                {
                    debcre = Constantes.cCredito;
                }
                else if (obj.com_tipodoc == Constantes.cNotadebPro.tpd_codigo)
                {
                    debcre = Constantes.cDebito;
                }
                else if (obj.com_tipodoc == Constantes.cDiario.tpd_codigo)
                {

                    debcre = (obj.com_tclipro == Constantes.cCliente) ? Constantes.cDebito : Constantes.cCredito;

                }

                Auto.actualiza_documentos(obj.com_empresa, null, null, obj.com_codclipro, null, null, null, 1);
                List<vDdocumento> lista = vDdocumentoBLL.GetAll(new WhereParams("ddo_empresa={0} AND ddo_cancelado=0 AND ddo_codclipro={1} AND ddo_debcre ={2}", obj.com_empresa, obj.com_codclipro, debcre), "");


                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span11\">");
                HtmlTable tdatos = new HtmlTable();
                tdatos.CreteEmptyTable(2, 2);
                tdatos.rows[0].cells[0].valor = "Cliente/Proveedor:";
                tdatos.rows[0].cells[1].valor = new Input { id = "txtCODCLIPRO_P", valor = persona.per_id, clase = Css.mini, habilitado = false }.ToString() + " " + new Input { id = "txtNOMBRES_P", clase = Css.large, habilitado = false, valor = persona.per_apellidos + " " + persona.per_nombres }.ToString() + new Input { id = "txtCODPER_P", visible = false, valor = persona.per_codigo }.ToString();
                tdatos.rows[1].cells[0].valor = "Documento:";
                tdatos.rows[1].cells[1].valor = new Input { id = "txtDOCUMETO_P", clase = Css.medium, habilitado = false, valor = obj.com_doctran }.ToString();

                html.AppendLine(tdatos.ToString());
                html.AppendLine(" </div><!--span11-->");
                html.AppendLine("</div><!--row-fluid-->");
                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span11\">");


                HtmlTable tddet = new HtmlTable();
                tddet.id = "tdafec1_P";
                tddet.invoice = true;
                tddet.clase = "scrolltable";
                tddet.footer = true;

                tddet.AddColumn("Documento", "", "", "");
                if (obj.com_tipodoc == Constantes.cPago.tpd_codigo || obj.com_tipodoc == Constantes.cPagoBan.tpd_codigo)
                {

                    tddet.AddColumn("Factura", "", "", "");
                    tddet.AddColumn("Emisión", "", "", "");
                }
                else
                {
                    tddet.AddColumn("Guia", "", "", "");
                    tddet.AddColumn("Nro", "", "", "");
                }

                //tddet.AddColumn("Guia", "", "", "");
                //tddet.AddColumn("Nro", "", "", "");

                tddet.AddColumn("Emisión", "", "", "");
                tddet.AddColumn("Vence", "", "", "");
                tddet.AddColumn("Socio", "", "", "");
                tddet.AddColumn("Monto", "", Css.right, "");
                tddet.AddColumn("Cancel", "", Css.right, "");
                tddet.AddColumn("Saldo", "", Css.right, "");

                foreach (vDdocumento item in lista)
                {
                    HtmlRow row = new HtmlRow();
                    //row.data = "data-comprobante=" + item.ddo_comprobante + " data-transac=" + item.ddo_transacc;
                    row.data = "data-comprobante=" + item.ddo_comprobante + " data-transac=" + item.ddo_transacc + " data-doctran= " + item.ddo_doctran + " data-pago=" + item.ddo_pago;

                    row.cells.Add(new HtmlCell { valor = item.ddo_doctran });

                    if (obj.com_tipodoc == Constantes.cPago.tpd_codigo || obj.com_tipodoc == Constantes.cPagoBan.tpd_codigo)
                    {
                        row.cells.Add(new HtmlCell { valor = item.cdoc_aut_factura });
                        row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi });

                    }
                    else
                    {
                        row.cells.Add(new HtmlCell { valor = item.com_doctran_guia });
                        row.cells.Add(new HtmlCell { valor = item.ddo_pago });
                    }

                    //row.cells.Add(new HtmlCell { valor = item.com_doctran_guia });
                    //row.cells.Add(new HtmlCell { valor = item.ddo_pago });
                    row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi.Value.ToShortDateString() });
                    row.cells.Add(new HtmlCell { valor = item.ddo_fecha_ven.Value.ToShortDateString() });
                    if (item.com_doctran_guia != null)
                        row.cells.Add(new HtmlCell { valor = item.per_razon_guia });
                    else
                        row.cells.Add(new HtmlCell { valor = item.per_razon });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_monto), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddo_cancela), clase = Css.right });
                    decimal saldo = item.ddo_monto.Value - item.ddo_cancela.Value;
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(saldo), clase = Css.right });
                    tddet.AddRow(row);

                }
                html.AppendLine(tddet.ToString());
                html.AppendLine(" </div><!--span11-->");
                html.AppendLine("</div><!--row-fluid-->");


            }

            return html.ToString();

        }





        #endregion

        #region Diario


        [WebMethod]
        public string GetBarraDiario(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object modify = null;
            tmp.TryGetValue("modify", out modify);

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            bool habilitado = true;
            if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;
            if (!string.IsNullOrEmpty((string)modify))
                habilitado = bool.Parse(modify.ToString());

            StringBuilder html = new StringBuilder();
            if (habilitado)
            {
                html.AppendLine("<ul class=\"list-nostyle list-inline\">");
                html.AppendLine("<li><div class=\"btn\" id=\"cancli\"><i class=\"iconfa-briefcase\"></i> &nbsp; Cancelaciones Cliente</div></li>");
                html.AppendLine("<li><div class=\"btn\" id=\"canpro\"><i class=\"iconfa-briefcase\"></i> &nbsp; Cancelaciones Proveedor</div></li>");
                html.AppendLine("</ul>");
            }
            return html.ToString();
        }

        [WebMethod]
        public string GetTablaDiario(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object modify = null;
            tmp.TryGetValue("modify", out modify);

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            bool habilitado = true;
            if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;
            if (!string.IsNullOrEmpty((string)modify))
                habilitado = bool.Parse(modify.ToString());

            comprobante.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa={0} and dco_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "dco_secuencia");
            List<Modulo> lstmodulos = ModuloBLL.GetAll("", "");


            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdinvoicediario";
            tdatos.invoice = true;

            tdatos.AddColumn("Id", "width10", "", new Input() { id = "txtIDCUE_D", placeholder = "CTA", autocomplete = "GetCuentaObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODCUE_D", visible = false }.ToString());
            tdatos.AddColumn("Cuenta", "width20", "", new Input() { id = "txtCUENTA_D", placeholder = "DESCRIPCION", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("Cliente/Prov", "width10", "", new Input() { id = "txtIDPER_D", autocomplete = "GetPersonaObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODPER_D", visible = false }.ToString());
            tdatos.AddColumn("Nombres", "width20", "", new Input() { id = "txtNOMBRES_D", placeholder = "CLIENTE/PROVEEDOR", habilitado = false, clase = Css.blocklevel }.ToString());
            tdatos.AddColumn("Modulo", "width10", "", new Input() { id = "txtMODULO_D", placeholder = "MOD", habilitado = false, clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODMOD_D", visible = false }.ToString());
            tdatos.AddColumn("Débito", "width10", Css.right, new Input() { id = "txtDEBE_D", placeholder = "DEBE", clase = Css.blocklevel + Css.amount, numeric = true }.ToString());
            tdatos.AddColumn("Crédito", "width10", Css.right, new Input() { id = "txtHABER_D", placeholder = "HABER", clase = Css.blocklevel + Css.amount, numeric = true }.ToString());
            tdatos.AddColumn("D/C", "width5", Css.center, new Input() { id = "txtDC_D", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("", "width5", Css.center, new Boton { small = true, clase = "iconsweets-trashcan", click = "RemoveRowDiario(this)", tooltip = "Eliminar registro" }.ToString());

            tdatos.editable = habilitado;
            tdatos.selectable = true;

            foreach (Dcontable item in comprobante.contables)
            {

                Modulo mod = lstmodulos.Find(delegate (BusinessObjects.Modulo m) { return m.mod_codigo == item.dco_cuentamodulo; });
                if (mod == null)
                    mod = new Modulo();

                HtmlRow row = new HtmlRow();

                row.data = "data-codcue='" + item.dco_cuenta + "' " +
                    " data-codper='" + item.dco_cliente + "' " +
                    " data-codmod='" + mod.mod_codigo + "' " +
                    " data-concepto='" + item.dco_concepto + "' " +
                    " data-idalmacen='" + item.dco_almacenid + "' " +
                    " data-nombrealmacen='" + item.dco_almacennombre + "' " +
                    " data-codalmacen='" + item.dco_almacen + "' " +
                    " data-idcentro='" + item.dco_centroid + "' " +
                    " data-nombrecentro='" + item.dco_centronombre + "' " +
                    " data-codcentro='" + item.dco_centro + "' " +
                    " data-idtransacc='" + item.dco_transacc + "' " +//FALTA EL CAMPO
                    " data-nombretransacc='" + item.dco_transacc + "' " +//FALTA EL CAMPO
                    " data-codtransacc_can='" + item.dco_transacc + "' " +
                    " data-codtransacc='" + item.dco_ddo_transacc + "' " +
                    " data-ddocomproba= '" + item.dco_ddo_comproba + "' " +
                    " data-doctran= '" + item.dco_doctran + "' " +
                    //" data-ddotransacc= '" + item.dco_ddo_transacc+ "' " +
                    " data-nropago= '" + item.dco_nropago + "' " +
                    " data-fechavence= '" + item.dco_fecha_vence + "' " +
                    "";
                row.removable = true;

                if (!habilitado)
                {
                    row.selectevent = "SelectDiario(this)";
                }
                else

                    row.clickevent = "EditDiario(this)";

                row.cells.Add(new HtmlCell { valor = item.dco_cuentaid });//ID CUENTA
                row.cells.Add(new HtmlCell { valor = item.dco_cuentanombre });//NOMBRE CUENTA
                row.cells.Add(new HtmlCell { valor = item.dco_clienteid });
                row.cells.Add(new HtmlCell { valor = item.dco_clienteapellidos + " " + item.dco_clientenombres });



                row.cells.Add(new HtmlCell { valor = mod.mod_id });
                if (item.dco_debcre == Constantes.cDebito)
                {
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.dco_valor_nac), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(0), clase = Css.right });
                }
                else
                {
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(0), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.dco_valor_nac), clase = Css.right });
                }
                row.cells.Add(new HtmlCell { valor = item.dco_debcre, clase = Css.center });
                if (habilitado)
                    row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, click = "RemoveRowDiario(this);", tooltip = "Eliminar registro" }.ToString(), clase = Css.center });
                else
                    row.cells.Add(new HtmlCell { valor = "", clase = Css.center });
                tdatos.AddRow(row);
                //tdatos.AddRow(new HtmlRow(item.ddoc_productoid, item.ddoc_productonombre, item.ddoc_observaciones, item.ddoc_productounidad, item.ddoc_cantidad, item.ddoc_precio, item.ddoc_dscitem, item.ddoc_total, item.ddoc_productoiva) { data = "data-codpro=" + item.ddoc_producto });   

            }

            html.AppendLine(tdatos.ToString());



            return html.ToString();
        }

        [WebMethod]
        public string GetPieDiario(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object modify = null;
            tmp.TryGetValue("modify", out modify);


            comprobante = ComprobanteBLL.GetByPK(comprobante);

            bool habilitado = true;
            if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;
            if (!string.IsNullOrEmpty((string)modify))
                habilitado = bool.Parse(modify.ToString());

            comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);


            StringBuilder html = new StringBuilder();


            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tddatosdiario";
            tdatos.CreteEmptyTable(7, 2);
            tdatos.rows[0].cells[0].valor = "Concepto:";
            tdatos.rows[0].cells[1].valor = new Textarea() { id = "txtCONCEPTO_D", placeholder = "CONCEPTO", clase = Css.blocklevel, habilitado = habilitado }.ToString();

            tdatos.rows[1].cells[0].valor = "Almacen:";
            tdatos.rows[1].cells[1].valor = new Select() { id = "cmbALMACEN_D", clase = Css.large, diccionario = Dictionaries.GetAlmacen(), habilitado = habilitado }.ToString();  //new Input() { id = "txtIDALM_D", autocomplete = "GetAlmacenObj", placeholder = "ID ALM", clase = Css.mini }.ToString() + " " + new Input() { id = "txtNOMBREALM_D", placeholder = "ALMACEN", clase = Css.medium, habilitado = false }.ToString() + new Input() { id = "txtCODALM_D", visible = false }.ToString();
            tdatos.rows[2].cells[0].valor = "Centro:";
            tdatos.rows[2].cells[1].valor = new Select() { id = "cmbCENTRO_D", clase = Css.large, diccionario = Dictionaries.GetCentro(), habilitado = false }.ToString();//new Input() { id = "txtIDCEN_D", autocomplete = "GetCentroObj", placeholder = "ID CENTRO", clase = Css.mini, habilitado = false }.ToString() + " " + new Input() { id = "txtNOMBRECEN_D", placeholder = "CENTRO", clase = Css.medium, habilitado = false }.ToString() + new Input() { id = "txtCODCEN_D", visible = false }.ToString(); 
            tdatos.rows[3].cells[0].valor = "Transacción:";
            //tdatos.rows[3].cells[1].valor = new Input() { id = "txtIDTRA_D", autocomplete = "GetTransaccionObj", placeholder = "ID TRANS", clase = Css.mini, habilitado=false }.ToString() + " " + new Input() { id = "txtNOMBRETRA_D", placeholder = "TRANSACCION", clase = Css.medium, habilitado = false }.ToString() + new Input() { id = "txtCODTRA_D", visible = false }.ToString();

            tdatos.rows[3].cells[1].valor = new Select() { id = "cmbTRANSACC_D", clase = Css.large, diccionario = Dictionaries.Empty(), habilitado = habilitado }.ToString();

            tdatos.rows[4].cells[0].valor = "Referencia:";
            tdatos.rows[4].cells[1].valor = new Input() { id = "txtREF_D", placeholder = "REFERENCIA", clase = Css.medium, habilitado = habilitado }.ToString();
            tdatos.rows[5].cells[0].valor = "O. Pago:";
            tdatos.rows[5].cells[1].valor = new Input() { id = "txtOPAGO_D", placeholder = "O.PAGO", clase = Css.medium, habilitado = false }.ToString();
            tdatos.rows[6].cells[0].valor = "Cuota /Vence:";
            tdatos.rows[6].cells[1].valor = new Input() { id = "txtNROPAGO_D", placeholder = "NRO PAGO", clase = Css.mini, habilitado = habilitado }.ToString() + " " + new Input() { id = "txtFECHAVENCE_D", datepicker = true, datetimevalor = DateTime.Now, clase = Css.small, habilitado = habilitado }.ToString();
            //tdatos.rows[7].cells[0].valor = "Saldo:";
            //tdatos.rows[7].cells[1].valor = new Input() { id = "txtSALDO_D",  clase = Css.medium }.ToString();

            //tdatos.rows[8].cells[0].valor = "";
            //tdatos.rows[8].cells[1].valor = "";

            html.AppendLine(tdatos.ToString());



            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(3, 2);

            tdatos1.rows[0].cells[0].valor = "TOTAL DÉBITO:";
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtTOTDEBITO_D", clase = Css.medium + Css.amount, habilitado = false }.ToString();
            tdatos1.rows[1].cells[0].valor = "TOTAL CRÉDITO:";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtTOTCREDITO_D", clase = Css.medium + Css.amount, habilitado = false }.ToString();
            tdatos1.rows[2].cells[0].valor = "DIFERENCIA:";
            tdatos1.rows[2].cells[1].valor = new Input { id = "txtDIFERENCIA_D", clase = Css.medium + Css.amount, habilitado = false }.ToString();
            //tdatos1.rows[3].cells[0].valor = "";
            //tdatos1.rows[3].cells[1].valor = "";

            html.AppendLine(tdatos1.ToString());

            return html.ToString();
        }


        [WebMethod]
        public string GetModulo(object objeto)
        {
            Modulo mod = new Modulo();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object empresa = null;
                object cuenta = null;

                tmp.TryGetValue("empresa", out empresa);
                tmp.TryGetValue("cuenta", out cuenta);

                Cuenta cta = CuentaBLL.GetByPK(new Cuenta { cue_empresa = int.Parse(empresa.ToString()), cue_empresa_key = int.Parse(empresa.ToString()), cue_codigo = int.Parse(cuenta.ToString()), cue_codigo_key = int.Parse(cuenta.ToString()) });
                mod = ModuloBLL.GetByPK(new Modulo { mod_codigo = cta.cue_modulo.Value, mod_codigo_key = cta.cue_modulo.Value });

            }
            return new JavaScriptSerializer().Serialize(mod);
        }

        [WebMethod]
        public string GetTransacc(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object empresa = null;
                object modulo = null;
                object transacc = null;

                tmp.TryGetValue("empresa", out empresa);
                tmp.TryGetValue("modulo", out modulo);
                tmp.TryGetValue("transacc", out transacc);


                if (!string.IsNullOrEmpty(modulo.ToString()))
                    return new Select() { id = "cmbTRANSACC_D", clase = Css.large, diccionario = Dictionaries.GetTransacc(int.Parse(modulo.ToString())), valor = transacc }.ToString();
                else
                    return new Select() { id = "cmbTRANSACC_D", clase = Css.large, diccionario = Dictionaries.Empty() }.ToString();

            }
            return "";
        }


        [WebMethod]
        public string GetPersonaCuenta(object objeto)
        {
            Comprobante comp = new Comprobante(objeto);



            //int cuenta = CXCP.cuenta_persona(comp.com_empresa, comp.com_tclipro.Value == Constantes.cTipoCliente ? 1 : 7, comp.com_codclipro.Value, comp.com_tclipro.Value);
            int cuenta = CXCP.cuenta_persona(comp.com_empresa, 1, comp.com_codclipro.Value, comp.com_tclipro.Value);



            Cuenta cta = CuentaBLL.GetByPK(new Cuenta { cue_empresa = comp.com_empresa, cue_empresa_key = comp.com_empresa, cue_codigo = cuenta, cue_codigo_key = cuenta });

            return new JavaScriptSerializer().Serialize(cta);

            
        }


        #endregion


        #region Buscar Comprobantes Hoja de Ruta y Planillas

        [WebMethod]
        public string BuscarComprobantes(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);

            int tipodocHR = Constantes.cHojaRuta.tpd_codigo;
            int tipodocLGC = Constantes.cPlanillaClientes.tpd_codigo;

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object nombres = null;

            tmp.TryGetValue("nombres", out nombres);


            Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = comprobante.com_empresa, uxe_empresa_key = comprobante.com_empresa, uxe_usuario = comprobante.crea_usr, uxe_usuario_key = comprobante.crea_usr });

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span4 popupcontiner\">");
            HtmlTable td = new HtmlTable();
            td.CreteEmptyTable(4, 2);
            td.rows[0].cells[0].valor = "Almacen";
            td.rows[0].cells[1].valor = new Select { id = "cmbALMACEN_B", diccionario = Dictionaries.GetIDAlmacen(), valor = uxe.uxe_almacen.Value, clase = Css.medium }.ToString();
            td.rows[1].cells[0].valor = "P.Venta";
            td.rows[1].cells[1].valor = new Select { id = "cmbPVENTA_B", diccionario = new Dictionary<string, string>(), clase = Css.medium }.ToString();
            td.rows[2].cells[0].valor = "Número";
            td.rows[2].cells[1].valor = new Input { id = "txtNUMERO_B", clase = Css.small }.ToString();
            td.rows[3].cells[0].valor = "Tipo";
            td.rows[3].cells[1].valor = new Select { id = "cmbTIPODOC_B", clase = Css.medium, diccionario = Dictionaries.GetTiposFacGui(), withempty = true }.ToString();
            html.AppendLine(td.ToString());
            html.AppendLine(" </div><!--span4-->");
            html.AppendLine("<div class=\"span4 popupcontiner\">");
            HtmlTable td1 = new HtmlTable();
            //NUEVO CODIGO PARA BUSQUEDA            
            td1.CreteEmptyTable(4, 2);
            td1.rows[0].cells[0].valor = "Fecha";

            if (comprobante.com_tipodoc == tipodocHR)
            {
                td1.rows[0].cells[1].valor = new Input { id = "txtFECHA_B", datepicker = true, datetimevalor = DateTime.Now, clase = Css.small }.ToString();
                td1.rows[1].cells[0].valor = "Ruta";
                td1.rows[1].cells[1].valor = new Select { id = "cmbRUTA_B", diccionario = Dictionaries.GetRuta(), valor = comprobante.com_ruta, clase = Css.medium, withempty = true }.ToString();
            }
            else if (comprobante.com_tipodoc == tipodocLGC)
            {
                td1.rows[0].cells[1].valor = new Input { id = "txtFECHA_B", datepicker = true, clase = Css.small, placeholder = "DESDE" }.ToString() + " " + new Input { id = "txtFECHAH_B", datepicker = true, clase = Css.small, placeholder = "HASTA" }.ToString();
                td1.rows[1].cells[0].valor = "Cliente";
                td1.rows[1].cells[1].valor = new Input { id = "txtCLIENTE_B", valor = nombres.ToString().Trim(), clase = Css.medium }.ToString();

            }
            td1.rows[2].cells[0].valor = "Socio";
            td1.rows[2].cells[1].valor = new Select { id = "txtSOCIO_B", valor = "", withempty = true, clase = Css.medium, diccionario = Dictionaries.GetSocios() }.ToString();

            //NUEVO CODIGO PARA BUSQUEDA
            if (comprobante.com_tipodoc == tipodocLGC)
            {
                td1.rows[3].cells[0].valor = "Ruta";
                td1.rows[3].cells[1].valor = new Select { id = "cmbRUTA_B", valor = "", withempty = true, clase = Css.medium, diccionario = Dictionaries.GetRuta() }.ToString();
            }
            else
            {
                td1.rows[3].cells[0].valor = "";
                td1.rows[3].cells[1].valor = "";

            }
            html.AppendLine(td1.ToString());
            html.AppendLine(new Input { id = "txtTIPODOC_B", valor = comprobante.com_tipodoc, visible = false }.ToString());

            html.AppendLine(" </div><!--span4-->");

            html.AppendLine("<div class=\"span4 popupcontiner\">");
            html.AppendLine("<ul class=\"list-nostyle list-inline\">");
            html.AppendLine("<li><div class=\"btn\" id=\"alldet_P\"><i class=\"iconfa-check\"></i> &nbsp;Todos</div></li>");
            html.AppendLine("<li><div class=\"btn\" id=\"nonedet_P\"><i class=\"iconfa-check-empty\"></i> &nbsp;Selección</div></li>");
            html.AppendLine("</ul>");
            html.AppendLine(new Boton { click = "LoadDataBusquedaComprobante(); return false; ", valor = "Buscar" }.ToString());
            html.AppendLine("<div id='msg_B'></div>");
            html.AppendLine("</div><!--span4-->");



            html.AppendLine("</div><!--row-fluid-->");

            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div id='detallegui' class=\"span11\">");

            html.AppendLine(" </div><!--span11-->");
            html.AppendLine("</div><!--row-fluid-->");


            return html.ToString();

        }
        [WebMethod]
        public string BuscarComprobantesData(object objeto)
        {

            Comprobante comp = new Comprobante(objeto);
            //List<vPlanillaCliente> lst = vPlanillaClienteBLL.GetAll(new WhereParams("detalle.com_empresa={0} ", comp.com_empresa), "detalle_apellidos,detalle_nombres, detalle_fecha");   
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object nombres = null;
            object socio = null;
            object tipo = null;
            object fh = null;
            tmp.TryGetValue("nombres", out nombres);
            tmp.TryGetValue("socio", out socio);
            tmp.TryGetValue("tipo", out tipo);
            tmp.TryGetValue("hasta", out fh);

            DateTime? fechahasta = null;
            DateTime d = new DateTime();
            if (fh != null)
            {
                if (DateTime.TryParse(fh.ToString(), out d))
                    fechahasta = d;
            }
            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            int tipodocfac = Constantes.cFactura.tpd_codigo;
            int tipodocgui = Constantes.cGuia.tpd_codigo;
            int tipodocHR = Constantes.cHojaRuta.tpd_codigo;
            int tipodocLGC = Constantes.cPlanillaClientes.tpd_codigo;

            if (comp.com_almacen.HasValue)
            {
                if (comp.com_almacen.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                    valores.Add(comp.com_almacen);
                    contador++;
                }
            }
            if (comp.com_pventa.HasValue)
            {
                if (comp.com_pventa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                    valores.Add(comp.com_pventa);
                    contador++;
                }
            }
            if (comp.com_numero > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_numero = {" + contador + "} ";
                valores.Add(comp.com_numero);
                contador++;
            }
            if (comp.com_fecha > DateTime.MinValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
                valores.Add(comp.com_fecha);
                contador++;

                if (fechahasta.HasValue)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
                    valores.Add(fechahasta);
                    contador++;
                }
                else
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
                    valores.Add(comp.com_fecha.AddDays(1));
                    contador++;
                }
            }
            if (comp.com_ruta.HasValue)
            {
                if (comp.com_ruta.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " e.cenv_ruta = {" + contador + "} ";
                    valores.Add(comp.com_ruta);
                    contador++;
                }
            }
            if (!string.IsNullOrEmpty((string)nombres))//CLIENTE O PROVEEDOR
            {

                //parametros.where += ((parametros.where != "") ? " and " : "") + " (p.per_ciruc ILIKE {" + contador + "} or p.per_nombres ILIKE {" + contador + "} or p.per_apellidos ILIKE{" + contador + "} or e.cenv_ciruc_rem ILIKE {" + contador + "} or e.cenv_nombres_rem ILIKE {" + contador + "} or e.cenv_apellidos_rem ILIKE{" + contador + "} or e.cenv_ciruc_des ILIKE {" + contador + "} or e.cenv_nombres_des ILIKE {" + contador + "} or e.cenv_apellidos_des ILIKE{" + contador + "} or p1.per_apellidos ILIKE {" + contador + "} or p1.per_nombres ILIKE{" + contador + "}) ";
                parametros.where += ((parametros.where != "") ? " and " : "") + " (p.per_ciruc ILIKE {" + contador + "} or p.per_nombres ILIKE {" + contador + "} or p.per_apellidos ILIKE{" + contador + "} or p.per_razon ILIKE {"+contador+"}) ";
                valores.Add("%" + nombres.ToString() + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty((string)socio))//CODIGO SOCIO
            {

                parametros.where += ((parametros.where != "") ? " and " : "") + " e.cenv_socio = {" + contador + "}  ";
                valores.Add(int.Parse(socio.ToString()));
                //parametros.where += ((parametros.where != "") ? " and " : "") + " (p1.per_apellidos ILIKE {" + contador + "} or p1.per_nombres ILIKE{" + contador + "}) ";
                //valores.Add("%" + socio.ToString() + "%");
                contador++;
            }



            if (comp.com_tipodoc == tipodocHR)
            {
                if (!string.IsNullOrEmpty((string)tipo))
                    parametros.where += ((parametros.where != "") ? " and " : "") + " (c.com_tipodoc = " + tipo.ToString() + ") and c.com_estado<>" + Constantes.cEstadoEliminado + " and (e.cenv_socio is null or e.cenv_socio =0)";
                else
                    parametros.where += ((parametros.where != "") ? " and " : "") + " (c.com_tipodoc = " + tipodocfac + " or c.com_tipodoc = " + tipodocgui + ") and c.com_estado<>" + Constantes.cEstadoEliminado + " and (e.cenv_socio is null or e.cenv_socio =0)";

                parametros.valores = valores.ToArray();
                foreach (Rutaxfactura rxf in comp.rutafactura)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_codigo != " + rxf.rfac_comprobantefac;
                }
            }


            if (comp.com_tipodoc == tipodocLGC)
            {
                if (!string.IsNullOrEmpty((string)tipo))
                    parametros.where += ((parametros.where != "") ? " and " : "") + " (c.com_tipodoc = " + tipo.ToString() + ") and c.com_estado<>" + Constantes.cEstadoEliminado;
                else
                    parametros.where += ((parametros.where != "") ? " and " : "") + " (c.com_tipodoc = " + tipodocfac + " or c.com_tipodoc = " + tipodocgui + ") and c.com_estado<>" + Constantes.cEstadoEliminado;


                parametros.valores = valores.ToArray();

                foreach (Planillacli plc in comp.planillas)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_codigo != " + plc.plc_comprobante;
                }
                //NUEVO CODIGO PARA QUE NO AGREGE GUIAS DE OTRAS PLANILLAS
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_codigo not in (select plc_comprobante from planillacli)";
            }




            int desde = 0;
            int hasta = 999;
            //List<vComprobante> lista = vComprobanteBLL.GetAllByPage(parametros, "t.com_fecha DESC", desde, hasta);
            List<vComprobante> lista = vComprobanteBLL.GetAllByPage(parametros, "t.com_numero", desde, hasta);




            // List<Ccomenv> lst = CcomenvBLL.GetAll(new WhereParams("cenv_empresa={0} and  cenv_ruta ={1} and cenv_estado_ruta={2} and cenv_socio ={2}" + whereguias, comprobante.com_empresa, comprobante.com_ruta, 0), "com_fecha");
            StringBuilder html = new StringBuilder();


            HtmlTable tdatos = new HtmlTable();

            tdatos.id = "tddatos_P";
            tdatos.invoice = true;

            if (comp.com_tipodoc == tipodocHR)
            {
                tdatos.AddColumn("Comprobante", "", "", "");
                tdatos.AddColumn("Remitente", "", "", "");
                tdatos.AddColumn("Destinatario", "", "", "");
                tdatos.AddColumn("Valor", "", "");

                //tdatos.editable = true;
                //foreach (Ccomenv item in lst)
                foreach (vComprobante item in lista)
                {
                    HtmlRow row = new HtmlRow();
                    row.markable = true;
                    row.data = "data-comprobante=" + item.codigo;
                    //row.cells.Add(new HtmlCell { valor = "<a href='#' onclick='ViewComprobante(" + item.cenv_comprobante + ");' >" + item.cenv_doctran + "</a>" });
                    row.cells.Add(new HtmlCell { valor = new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.codigo + ")" }.ToString() + " " + item.doctran });
                    row.cells.Add(new HtmlCell { valor = item.nombres_rem });
                    row.cells.Add(new HtmlCell { valor = item.nombres_des });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.total), clase = Css.right });

                    tdatos.AddRow(row);

                }
            }
            if (comp.com_tipodoc == tipodocLGC)
            {
                tdatos.AddColumn("Fecha", "", "", "");
                tdatos.AddColumn("Socio", "", "", "");
                tdatos.AddColumn("Comprobante", "", "", "");
                tdatos.AddColumn("Subtot0", "", "");
                tdatos.AddColumn("SubtotIVA", "", "");
                tdatos.AddColumn("IVA", "", "");
                tdatos.AddColumn("Seguro", "", "");
                tdatos.AddColumn("Transporte", "", "");
                tdatos.AddColumn("Valor", "", "");

                //tdatos.editable = true;
                //foreach (Ccomenv item in lst)
                foreach (vComprobante item in lista)
                {
                    HtmlRow row = new HtmlRow();
                    row.markable = true;
                    row.data = "data-comprobante=" + item.codigo;
                    //row.cells.Add(new HtmlCell { valor = "<a href='#' onclick='ViewComprobante(" + item.cenv_comprobante + ");' >" + item.cenv_doctran + "</a>" });
                    row.cells.Add(new HtmlCell { valor = item.fecha });
                    row.cells.Add(new HtmlCell { valor = item.nombres_soc });
                    row.cells.Add(new HtmlCell { valor = new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.codigo + ")" }.ToString() + " " + item.doctran });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.subtotal), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.subimpuesto), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.impuesto), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.seguro), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.transporte), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.total), clase = Css.right });

                    tdatos.AddRow(row);

                }
            }
            html.AppendLine(tdatos.ToString());

            return html.ToString();




        }

        [WebMethod]
        public string BuscarCancelaciones(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);

            Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = comprobante.com_empresa, uxe_empresa_key = comprobante.com_empresa, uxe_usuario = comprobante.crea_usr, uxe_usuario_key = comprobante.crea_usr });

            StringBuilder html = new StringBuilder();

            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span4 popupcontiner\">");
            HtmlTable td = new HtmlTable();
            td.CreteEmptyTable(3, 2);
            td.rows[0].cells[0].valor = "Almacen";
            td.rows[0].cells[1].valor = new Select { id = "cmbALMACEN_B", diccionario = Dictionaries.GetIDAlmacen(), withempty = true, clase = Css.medium }.ToString();
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
            td1.rows[0].cells[1].valor = new Input { id = "txtFECHA_B", datepicker = true, clase = Css.small }.ToString();
            td1.rows[1].cells[0].valor = "";
            td1.rows[1].cells[1].valor = new Boton { click = "LoadDataBusquedaCancelacion();return false;", valor = "Buscar" }.ToString();
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



            /*html.AppendLine("<div class=\"row-fluid\">");
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

            List<vCancelacion> lst = vCancelacionBLL.GetAll(new WhereParams("f.com_tipodoc = 4 AND dca_planilla is null and (cf.cenv_socio = {0} or cg.cenv_socio = {0}) " + whereguias, comprobante.com_codclipro), OrderByClause);

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
            html.AppendLine("</div><!--row-fluid-->");*/
            return html.ToString();
        }


        [WebMethod]
        public string BuscarCancelacionesData(object objeto)
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
            if (comp.com_fecha > DateTime.MinValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_fecha between {" + contador + "} ";
                valores.Add(comp.com_fecha);
                contador++;
                parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
                valores.Add(comp.com_fecha.AddDays(1));
                contador++;
            }
            parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_tipodoc = 4 AND dca_planilla is null and (cf.cenv_socio = " + comp.com_codclipro + " or cg.cenv_socio = " + comp.com_codclipro + ") ";
            parametros.valores = valores.ToArray();
            foreach (Dcancelacion pls in comp.cancelaciones)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " CAST(dca_comprobante AS varchar )+CAST(dca_comprobante_can AS varchar)+CAST(dca_pago AS varchar )+CAST(dca_secuencia AS varchar)!= '" + pls.dca_comprobante + pls.dca_comprobante_can + pls.dca_pago + pls.dca_secuencia + "' ";
            }


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

            List<vCancelacion> lst = vCancelacionBLL.GetAll(parametros, "ddo_doctran");

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


        #endregion


        #endregion


        #region Formulario

        [WebMethod]
        public string GetFormulario(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object empresa = null;
                object comprobante = null;
                tmp.TryGetValue("com_empresa", out empresa);
                tmp.TryGetValue("com_codigo", out comprobante);
                Comprobante obj = new Comprobante();
                obj.com_codigo_key = (Int64)Conversiones.GetValueByType(comprobante, typeof(Int64));
                obj.com_empresa_key = (Int32)Conversiones.GetValueByType(empresa, typeof(Int32));
                obj = ComprobanteBLL.GetByPK(obj);
                Tipodoc tpd = new Tipodoc();
                tpd.tpd_codigo_key = obj.com_tipodoc;
                tpd = TipodocBLL.GetByPK(tpd);
                Formulario frm = new Formulario();
                frm.for_codigo_key = tpd.tpd_for_eje ?? 0;
                frm = FormularioBLL.GetByPK(frm);
                if (frm.for_id.IndexOf('?') > 0)
                    return frm.for_id + "&codigocomp=" + obj.com_codigo + "&tipodoc=" + tpd.tpd_codigo;
                else
                    return frm.for_id + "?codigocomp=" + obj.com_codigo + "&tipodoc=" + tpd.tpd_codigo;
            }

            return new JavaScriptSerializer().Serialize(0);
        }



        [WebMethod]
        public string GetFormularioByTipoDoc(object objeto)
        {
            Tipodoc tipo = new Tipodoc(objeto);
            tipo.tpd_codigo_key = tipo.tpd_codigo;
            tipo = TipodocBLL.GetByPK(tipo);
            Formulario result = new Formulario();
            result.for_codigo = tipo.tpd_for_eje ?? 0;
            result.for_codigo_key = tipo.tpd_for_eje ?? 0;
            result = FormularioBLL.GetByPK(result);
            return new JavaScriptSerializer().Serialize(result);
        }
        #endregion



        #region Retenciones

        [WebMethod(EnableSession = true)]
        public string GeneraRetencion(object objeto)
        {

            Comprobante obj = new Comprobante(objeto);
            string mensaje = "";
            Comprobante ret = General.GenRetencion(obj.com_empresa, obj.com_codigo, ref mensaje);
            if (mensaje == "OK")
                return ret.com_codigo.ToString();
            else
                return "0";


        }

        #endregion


        #region Print

        [WebMethod]
        public string GetPrintVersion(object objeto)
        {
            //Comprobante comprobante = GetComprobanteObj(objeto);
            Comprobante comprobante = new Comprobante(objeto);

            Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = comprobante.com_empresa, uxe_empresa_key = comprobante.com_empresa, uxe_usuario = comprobante.crea_usr, uxe_usuario_key = comprobante.crea_usr });

            string printversion = Constantes.GetPrintVersion(uxe.uxe_usuario, uxe.uxe_empresa, uxe.uxe_almacen, uxe.uxe_puntoventa, comprobante.com_tipodoc);

            string[] array = printversion.Split('|');

            for (int i = 0; i < array.Length; i++)
            {
                string[] arraydoc = array[i].Split(',');
                if (arraydoc.Length > 1)
                {
                    if (comprobante.com_tipodoc.ToString() == arraydoc[0])
                        return arraydoc[1];
                }
            }


            return printversion;

        }
        [WebMethod]
        public string GetPrintHTML(object objeto)
        {
            //Comprobante comprobante = GetComprobanteObj(objeto);
            Comprobante comprobante = new Comprobante(objeto);

            Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = comprobante.com_empresa, uxe_empresa_key = comprobante.com_empresa, uxe_usuario = comprobante.crea_usr, uxe_usuario_key = comprobante.crea_usr });

            string printhtml = Constantes.GetPrintHTML(uxe.uxe_usuario, uxe.uxe_empresa, uxe.uxe_almacen, uxe.uxe_puntoventa);
            string[] array = printhtml.Split('|');

            for (int i = 0; i < array.Length; i++)
            {
                string[] arraydoc = array[i].Split(',');
                if (arraydoc.Length > 1)
                {
                    if (comprobante.com_tipodoc.ToString() == arraydoc[0])
                        return arraydoc[1];
                }
            }


            return printhtml;

        }

        [WebMethod]
        public string GetPrintFormat(object objeto)
        {
            //Comprobante comprobante = GetComprobanteObj(objeto);
            Comprobante comprobante = new Comprobante(objeto);

            Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = comprobante.com_empresa, uxe_empresa_key = comprobante.com_empresa, uxe_usuario = comprobante.crea_usr, uxe_usuario_key = comprobante.crea_usr });

            string printformat = Constantes.GetPrintFormat(uxe.uxe_usuario, uxe.uxe_empresa, uxe.uxe_almacen, uxe.uxe_puntoventa);
            string[] array = printformat.Split('|');

            for (int i = 0; i < array.Length; i++)
            {
                string[] arraydoc = array[i].Split(',');
                if (arraydoc.Length > 1)
                {
                    if (comprobante.com_tipodoc.ToString() == arraydoc[0])
                        return arraydoc[1];
                }
            }
            return printformat;
        }

        [WebMethod]
        public string GetPrintFormat1(object objeto)
        {
            //Comprobante comprobante = GetComprobanteObj(objeto);
            Comprobante comprobante = new Comprobante(objeto);

            Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = comprobante.com_empresa, uxe_empresa_key = comprobante.com_empresa, uxe_usuario = comprobante.crea_usr, uxe_usuario_key = comprobante.crea_usr });

            string printformat = Constantes.GetPrintFormat(comprobante.crea_usr, comprobante.com_empresa, comprobante.com_almacen, comprobante.com_pventa);
            string[] array = printformat.Split('|');

            for (int i = 0; i < array.Length; i++)
            {
                string[] arraydoc = array[i].Split(',');
                if (arraydoc.Length > 1)
                {
                    if (comprobante.com_tipodoc.ToString() == arraydoc[0])
                        return arraydoc[1];
                }
            }
            return printformat;
        }



        #endregion

        #region Procesos
        [WebMethod(EnableSession = true)]
        public string FixPersona(object objeto)
        {

            int tipocliente = 4;
            int catcodigo = 6;
            int polcodigo = 4;

            WhereParams whereparams = new WhereParams("per_estado={0} and pxt_tipo={1}", 1, Constantes.cProveedor);
            List<Persona> lst = vPersonaBLL.GetAllTop(whereparams, "per_razon", 999999);

            //List<Persona> lst = PersonaBLL.GetAll("", "");

            List<Personaxtipo> lsttipos = PersonaxtipoBLL.GetAll("pxt_tipo=" + tipocliente, "");

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            transaction.BeginTransaction();
            try
            {

                foreach (Persona persona in lst)
                {
                    if (string.IsNullOrEmpty(persona.per_razon))
                        persona.per_razon = persona.per_apellidos + " " + persona.per_nombres;
                    persona.per_codigo_key = persona.per_codigo;
                    persona.per_empresa_key = persona.per_empresa;
                    PersonaBLL.Update(transaction, persona);

                    Personaxtipo pxt = lsttipos.Find(delegate (Personaxtipo p) { return p.pxt_empresa == persona.per_empresa && p.pxt_persona == persona.per_codigo; });
                    if (pxt == null)
                    {
                        pxt = new Personaxtipo();
                        pxt.pxt_persona = persona.per_codigo;
                        pxt.pxt_empresa = persona.per_empresa;
                        pxt.pxt_estado = Constantes.cEstadoGrabado;
                        pxt.pxt_tipo = tipocliente;
                        pxt.pxt_politicas = polcodigo;
                        pxt.pxt_cat_persona = catcodigo;
                        pxt.crea_usr = persona.crea_usr;
                        pxt.crea_fecha = persona.crea_fecha;
                        pxt.mod_usr = persona.mod_usr;
                        pxt.mod_fecha = persona.mod_fecha;
                        PersonaxtipoBLL.Insert(transaction, pxt);
                    }
                }



                transaction.Commit();
                return "OK";
                //return codigo.ToString();
            }
            catch
            {
                transaction.Rollback();
                return "ERROR";
            }



        }

        [WebMethod(EnableSession = true)]
        public string ActualizaSaldos(object objeto)
        {


            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object empresa = null;
            object periodo = null;
            object mes = null;
            object cuenta = null;

            tmp.TryGetValue("empresa", out empresa);
            tmp.TryGetValue("periodo", out periodo);
            tmp.TryGetValue("mes", out mes);
            tmp.TryGetValue("cuenta", out cuenta);

            if (string.IsNullOrEmpty(cuenta.ToString()))
                cuenta = null;



            BLL transaction = new BLL();
            transaction.CreateTransaction();
            transaction.BeginTransaction();
            try
            {

                if (Auto.actualizar_saldos(transaction, int.Parse(empresa.ToString()), int.Parse(periodo.ToString()), int.Parse(mes.ToString()), (cuenta != null ? (int?)int.Parse(cuenta.ToString()) : null), 1))
                    transaction.Commit();
                return "OK";
                //return codigo.ToString();
            }
            catch
            {
                transaction.Rollback();
                return "ERROR";
            }



        }

        [WebMethod(EnableSession = true)]
        public string PasarSaldos(object objeto)
        {


            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object empresa = null;
            object deperiodo = null;
            object aperiodo = null;

            tmp.TryGetValue("empresa", out empresa);
            tmp.TryGetValue("deperiodo", out deperiodo);
            tmp.TryGetValue("aperiodo", out aperiodo);




            BLL transaction = new BLL();
            transaction.CreateTransaction();
            transaction.BeginTransaction();
            try
            {

                if (Auto.pasar_saldos(transaction, int.Parse(empresa.ToString()), int.Parse(deperiodo.ToString()), int.Parse(aperiodo.ToString())))
                    transaction.Commit();
                return "OK";
                //return codigo.ToString();
            }
            catch
            {
                transaction.Rollback();
                return "ERROR";
            }



        }


        [WebMethod(EnableSession = true)]
        public string PasarSecuencias(object objeto)
        {


            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object empresa = null;
            object deperiodo = null;
            object aperiodo = null;

            tmp.TryGetValue("empresa", out empresa);
            tmp.TryGetValue("deperiodo", out deperiodo);
            tmp.TryGetValue("aperiodo", out aperiodo);




            BLL transaction = new BLL();
            transaction.CreateTransaction();
            transaction.BeginTransaction();
            try
            {

                if (Auto.pasar_secuencias(transaction, int.Parse(empresa.ToString()), int.Parse(deperiodo.ToString()), int.Parse(aperiodo.ToString())))
                    transaction.Commit();
                return "OK";
                //return codigo.ToString();
            }
            catch (Exception ex)
            { 
                transaction.Rollback();
                return "ERROR " + ex.Message;
            }



        }

        [WebMethod(EnableSession = true)]
        public string FixComprobantes(object objeto)
        {

            Comprobante comp = new Comprobante(objeto);
            comp.com_empresa = 1;

            //List<vComprobante> lst = vComprobanteBLL.GetAll(new WhereParams("com_estado ={0} and com_tipodoc = {1} and com_periodo = {2}	and com_mes = {3}	and (tot_total- (tot_subtot_0+ tot_transporte+ tot_subtotal+ tot_tseguro+ tot_timpuesto))<>0", Constantes.cEstadoMayorizado, Constantes.cFactura.tpd_codigo, comp.com_periodo, comp.com_mes), "");
            List<vComprobante> lst = vComprobanteBLL.GetAll(new WhereParams("c.com_estado ={0} and (c.com_tipodoc = {1} or c.com_tipodoc = {2}) and c.com_periodo = {3}	and c.com_mes = {4} ", Constantes.cEstadoMayorizado, Constantes.cFactura.tpd_codigo, Constantes.cGuia.tpd_codigo, comp.com_periodo, comp.com_mes), "");

            foreach (vComprobante item in lst)
            {
                //decimal calculado = item.subtotal.Value + item.transporte.Value + item.subimpuesto.Value + ((item.seguro.HasValue) ? item.seguro.Value : 0) + (item.impuesto.HasValue ? item.impuesto.Value : 0);
                decimal calculado = item.subtotal.Value + item.transporte.Value + item.subimpuesto.Value + ((item.tseguro.HasValue) ? item.tseguro.Value : 0) + (item.impuesto.HasValue ? item.impuesto.Value : 0);
                decimal diferencia = item.total.Value - calculado;
                if (diferencia != 0)
                {
                    Total tot = TotalBLL.GetByPK(new Total { tot_empresa = comp.com_empresa, tot_empresa_key = comp.com_empresa, tot_comprobante = item.codigo.Value, tot_comprobante_key = item.codigo.Value });
                    tot.tot_empresa_key = tot.tot_empresa;
                    tot.tot_comprobante_key = tot.tot_comprobante;
                    if (!item.tseguro.HasValue)
                        item.tseguro = 0;
                    if (item.tseguro.Value > 0)
                        tot.tot_tseguro = item.tseguro.Value + diferencia;
                    else if (item.subtotal.Value > 0)
                        tot.tot_subtot_0 = item.subtotal.Value + diferencia;

                    TotalBLL.Update(tot);

                }

            }




            return "OK";



        }


        [WebMethod(EnableSession = true)]
        public string FixDocumentos(object objeto)
        {

            Comprobante comp = new Comprobante(objeto);

            //DateTime desde = new DateTime(comp.com_periodo, comp.com_mes.Value, 1);
            //DateTime hasta = desde.AddMonths(1).AddSeconds(-1);

            DateTime desde = new DateTime(2019,1,1);
            DateTime hasta = DateTime.Now;

            Auto.actualiza_documentos(comp.com_empresa, null, null, null, null, desde, hasta, 0);

            //Auto.actualiza_saldos_documentos(comp.com_empresa, comp.com_periodo, comp.com_mes);



            return "OK";



        }

        [WebMethod(EnableSession = true)]
        public string ImportClientes(object objeto)
        {

            Comprobante comp = new Comprobante(objeto);
            Auto.actualiza_saldos_documentos(comp.com_empresa, comp.com_periodo, comp.com_mes);



            return "OK";



        }



        [WebMethod(EnableSession = true)]
        public string ActualizarDocumentos(object objeto)
        {
            /*DateTime? desde = (DateTime?)Dictionaries.GetObject(objeto, "desde", typeof(DateTime?));
            DateTime? hasta = (DateTime?)Dictionaries.GetObject(objeto, "hasta", typeof(DateTime?));
            string ruc = (string)Dictionaries.GetObject(objeto, "ruc", typeof(string));

            List<Persona> personas = PersonaBLL.GetAll("per_ciruc='" + ruc + "'", "");
            if (personas.Count > 0)
            {
                Auto.actualiza_documentos(1, null, null, personas[0].per_codigo, null, desde, hasta, 0);
                return "OK";
            }
            return "NO";

            */

            Auto.actualiza_documentos(1, null, null);


            return "OK";




        }


        [WebMethod(EnableSession = true)]
        public string ActualizarCancelaciones(object objeto)
        {
            /*DateTime? desde = (DateTime?)Dictionaries.GetObject(objeto, "desde", typeof(DateTime?));
            DateTime? hasta = (DateTime?)Dictionaries.GetObject(objeto, "hasta", typeof(DateTime?));
            string ruc = (string)Dictionaries.GetObject(objeto, "ruc", typeof(string));

            List<Persona> personas = PersonaBLL.GetAll("per_ciruc='" + ruc + "'", "");
            if (personas.Count > 0)
            {
                Auto.actualiza_documentos(1, null, null, personas[0].per_codigo, null, desde, hasta, 0);
                return "OK";
            }
            return "NO";

            */

            return Auto.actualiza_cancelaciones(1, null, null);


            //return "OK";




        }




        [WebMethod(EnableSession = true)]
        public string CierreAutomatico(object objeto)
        {


            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object empresa = null;
            object periodo = null;
            object cierre = null;
            object fecha = null;
            object usuario = null;

            tmp.TryGetValue("empresa", out empresa);
            tmp.TryGetValue("periodo", out periodo);
            tmp.TryGetValue("cierre", out cierre);
            tmp.TryGetValue("fecha", out fecha);
            tmp.TryGetValue("usuario", out usuario);


            return Auto.cierre_automatico(int.Parse(empresa.ToString()), int.Parse(periodo.ToString()), int.Parse(cierre.ToString()), DateTime.Parse(fecha.ToString()), usuario.ToString());


            //BLL transaction = new BLL();
            //transaction.CreateTransaction();
            //transaction.BeginTransaction();
            //try
            //{

            //    if (Auto.actualizar_saldos(transaction, int.Parse(empresa.ToString()), int.Parse(periodo.ToString()), int.Parse(mes.ToString()), (cuenta != null ? (int?)int.Parse(cuenta.ToString()) : null), 1))
            //        transaction.Commit();
            //    return "OK";
            //    //return codigo.ToString();
            //}
            //catch
            //{
            //    transaction.Rollback();
            //    return "ERROR";
            //}



        }


        [WebMethod]
        public string CancelFacturasCobro(object objeto)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object empresa = null;
            object periodo = null;
            object mes = null;

            tmp.TryGetValue("empresa", out empresa);
            tmp.TryGetValue("periodo", out periodo);
            tmp.TryGetValue("mes", out mes);

            int? p = Conversiones.StringToIntNull((string)periodo);
            int? m = Conversiones.StringToIntNull((string)mes);


            return Auto.cancel_facturas_cobro(int.Parse(empresa.ToString()), p, m);


            //BLL transaction = new BLL();
            //transaction.CreateTransaction();
            //transaction.BeginTransaction();
            //try
            //{

            //    if (Auto.actualizar_saldos(transaction, int.Parse(empresa.ToString()), int.Parse(periodo.ToString()), int.Parse(mes.ToString()), (cuenta != null ? (int?)int.Parse(cuenta.ToString()) : null), 1))
            //        transaction.Commit();
            //    return "OK";
            //    //return codigo.ToString();
            //}
            //catch
            //{
            //    transaction.Rollback();
            //    return "ERROR";
            //}



        }


        #endregion

        #region Tools

        [WebMethod(EnableSession = true)]
        public string SaveFacturaGuia(string xml)
        {

            return Packages.External.SaveFacturaGuia(xml);





        }

        [WebMethod(EnableSession = true)]
        public string CuadrarPlanilla(object objeto)
        {
            Comprobante planilla = new Comprobante(objeto);

            Total totpla = TotalBLL.GetByPK(new Total { tot_empresa = planilla.com_empresa, tot_empresa_key = planilla.com_empresa, tot_comprobante = planilla.com_codigo, tot_comprobante_key = planilla.com_codigo });

            List<Total> lsttot = TotalBLL.GetAll("tot_comprobante in (select com_codigo from comprobante inner join total on tot_comprobante = com_codigo and com_empresa = tot_empresa inner join  planillacli on com_empresa = plc_empresa and com_codigo = plc_comprobante where plc_comprobante_pla = " + planilla.com_codigo + ")", "");

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            decimal valiva = 12;

            try
            {
                transaction.BeginTransaction();

                foreach (Total item in lsttot)
                {
                    if (item.tot_tseguro.HasValue)
                    {
                        if (item.tot_tseguro.Value > 0)
                        {
                            decimal iva = item.tot_tseguro.Value * (valiva / 100);
                            decimal descuadre = item.tot_timpuesto - iva;
                            if (descuadre > 0)
                            {

                                item.tot_tseguro = item.tot_tseguro.Value + descuadre;
                                item.tot_timpuesto = item.tot_tseguro.Value * (valiva / 100);
                                item.tot_total = item.tot_subtot_0 + item.tot_subtotal + item.tot_transporte + item.tot_tseguro.Value + item.tot_timpuesto;
                                item.tot_comprobante_key = item.tot_comprobante;
                                item.tot_empresa_key = item.tot_empresa;
                                TotalBLL.Update(transaction, item);
                            }


                        }
                    }
                }


                decimal tseguro = lsttot.Sum(s => s.tot_tseguro ?? 0);
                decimal descuadretot = (totpla.tot_tseguro ?? 0) - tseguro;

                if (descuadretot > 0)
                {
                    foreach (Total item in lsttot)
                    {
                        if (item.tot_tseguro.HasValue)
                        {
                            if (item.tot_tseguro.Value > 0)
                            {
                                if (descuadretot > (decimal)0.01)
                                {
                                    item.tot_tseguro -= (decimal)0.01;
                                    descuadretot -= (decimal)0.01;
                                }
                                else
                                {
                                    item.tot_tseguro -= descuadretot;
                                    descuadretot = 0;
                                }

                                item.tot_timpuesto = item.tot_tseguro.Value * (valiva / 100);
                                item.tot_total = item.tot_subtot_0 + item.tot_subtotal + item.tot_transporte + item.tot_tseguro.Value + item.tot_timpuesto;
                                item.tot_comprobante_key = item.tot_comprobante;
                                item.tot_empresa_key = item.tot_empresa;
                                TotalBLL.Update(transaction, item);

                                if (descuadretot == 0)
                                    break;

                            }
                        }
                    }
                }
                else
                {
                    descuadretot = descuadretot * -1;

                    foreach (Total item in lsttot)
                    {
                        if (item.tot_tseguro.HasValue)
                        {
                            if (item.tot_tseguro.Value > 0)
                            {
                                if (descuadretot > (decimal)0.01)
                                {
                                    item.tot_tseguro += (decimal)0.01;
                                    descuadretot -= (decimal)0.01;
                                }
                                else
                                {
                                    item.tot_tseguro += descuadretot;
                                    descuadretot = 0;
                                }

                                item.tot_timpuesto = item.tot_tseguro.Value * (valiva / 100);
                                item.tot_total = item.tot_subtot_0 + item.tot_subtotal + item.tot_transporte + item.tot_tseguro.Value + item.tot_timpuesto;
                                item.tot_comprobante_key = item.tot_comprobante;
                                item.tot_empresa_key = item.tot_empresa;
                                TotalBLL.Update(transaction, item);

                                if (descuadretot == 0)
                                    break;

                            }
                        }
                    }
                }



                transaction.Commit();
                return "OK";
                //return codigo.ToString();
            }
            catch
            {
                transaction.Rollback();
                return "ERROR";
            }

        }


        [WebMethod(EnableSession = true)]
        public string CancelacionesNegativas(object objeto)
        {
            return General.CancelacionesNegativas();
        }


        [WebMethod(EnableSession = true)]
        public string SerieDuplicados(object objeto)
        {
            object doctrans = Dictionaries.GetObject(objeto, "doctrans", typeof(object));
            object[] array = (object[])doctrans;
            return Tools.AddSerieDuplicados(Array.ConvertAll(array, x => x.ToString()));


         



        }

        [WebMethod(EnableSession = true)]
        public string GetCancelacionesNoPlanilla(object objeto)
        {
            DateTime? desde = (DateTime?)Dictionaries.GetObject(objeto, "desde", typeof(DateTime?));
            DateTime? hasta = (DateTime?)Dictionaries.GetObject(objeto, "hasta", typeof(DateTime?));
            
            return Tools.GetCancelacionesNoPlanilla(1,desde, hasta);






        }


        [WebMethod(EnableSession = true)]
        public string RemoveRetDuplicadas(object objeto)
        {
            DateTime? desde = (DateTime?)Dictionaries.GetObject(objeto, "desde", typeof(DateTime?));
            DateTime? hasta = (DateTime?)Dictionaries.GetObject(objeto, "hasta", typeof(DateTime?));
            string tipos = (string)Dictionaries.GetObject(objeto, "tipos", typeof(string));
            //Tools.RemoveDuplcateRecibosAsync(1, desde, hasta, tipos);
            return Tools.RemoveDuplcateRecibos(1, desde, hasta, tipos);            

        }

        [WebMethod(EnableSession = true)]
        public string FixRetDuplicadas(object objeto)
        {          
            string clave = (string)Dictionaries.GetObject(objeto, "clave", typeof(string));
            Tools.FixDuplicadosElectronicos(1, clave,null);
            return "ok";

        }

        [WebMethod(EnableSession = true)]
        public string GetErroresPlanillas(object objeto)
        {
            DateTime? desde = (DateTime?)Dictionaries.GetObject(objeto, "desde", typeof(DateTime?));
            DateTime? hasta = (DateTime?)Dictionaries.GetObject(objeto, "hasta", typeof(DateTime?));

            return Tools.GetErroresPlanillas(1, desde, hasta, null, null);
            
           

        }

        [WebMethod(EnableSession = true)]
        public string CloseCarteraCli(object objeto)
        {
            DateTime? desde = (DateTime?)Dictionaries.GetObject(objeto, "desde", typeof(DateTime?));
            DateTime? hasta = (DateTime?)Dictionaries.GetObject(objeto, "hasta", typeof(DateTime?));
            bool? all = (bool?)Dictionaries.GetObject(objeto, "all", typeof(bool?));
            return Migration.CerrarCarteraClientes(1, desde, hasta,all??true);



        }

        [WebMethod(EnableSession = true)]
        public string CuadreElectronico(object objeto)
        {
            DateTime? desde = (DateTime?)Dictionaries.GetObject(objeto, "desde", typeof(DateTime?));
            DateTime? hasta = (DateTime?)Dictionaries.GetObject(objeto, "hasta", typeof(DateTime?));

            return Packages.Electronico.CuadreElectronico(1, desde.Value, hasta.Value);            



        }

        #endregion

        #region Valores Socios

        [WebMethod]
        public string GetValoresSocios(object objeto)
        {
            StringBuilder html = new StringBuilder();

            Comprobante planilla = new Comprobante(objeto);
            planilla.com_empresa_key = planilla.com_empresa;
            planilla.com_codigo_key = planilla.com_codigo;
            planilla = ComprobanteBLL.GetByPK(planilla);

            planilla.total = new Total();
            planilla.total.tot_empresa = planilla.com_empresa;
            planilla.total.tot_empresa_key = planilla.com_empresa;
            planilla.total.tot_comprobante = planilla.com_codigo;
            planilla.total.tot_comprobante_key = planilla.com_codigo;
            planilla.total = TotalBLL.GetByPK(planilla.total);

            int tipofac = Constantes.cFactura.tpd_codigo;

            List<vCancelacion> lista = new List<vCancelacion>();
            List<Planillacomprobante> lst = PlanillacomprobanteBLL.GetAll(new WhereParams("pco_empresa ={0} and pco_comprobante_pla={1}", planilla.com_empresa, planilla.com_codigo), "");
            if (lst.Count > 0)
            {
                Comprobante comprobante = new Comprobante();
                comprobante.com_empresa = lst[0].pco_empresa;
                comprobante.com_empresa_key = lst[0].pco_empresa;
                comprobante.com_codigo = lst[0].pco_comprobante_fac;
                comprobante.com_codigo_key = lst[0].pco_comprobante_fac;
                comprobante = ComprobanteBLL.GetByPK(comprobante);

                comprobante.total = new Total();
                comprobante.total.tot_empresa = comprobante.com_empresa;
                comprobante.total.tot_empresa_key = comprobante.com_empresa;
                comprobante.total.tot_comprobante = comprobante.com_codigo;
                comprobante.total.tot_comprobante_key = comprobante.com_codigo;
                comprobante.total = TotalBLL.GetByPK(comprobante.total);



                lista = vCancelacionBLL.GetAll1(new WhereParams("ddo_empresa={0} AND ddo_comprobante ={1}", comprobante.com_empresa, comprobante.com_codigo), "");
            }
            else
            {
                List<Planillacli> lstcli = PlanillacliBLL.GetAll(new WhereParams("plc_empresa={0} and plc_comprobante_pla={1}", planilla.com_empresa, planilla.com_codigo), "");
                //WhereParams parametros = new WhereParams("ddo_empresa={0} AND ddo_cancelado=0 AND ddo_codclipro={1} AND ddo_debcre ={2} ", planilla.com_empresa, planilla.com_codclipro, debcre);
                WhereParams parametros = new WhereParams("ddo_empresa={0} ", planilla.com_empresa);
                string where = "";
                foreach (Planillacli item in lstcli)
                {
                    if (item.plc_comprobantetipodoc.Value == tipofac)
                        where += ((where != "") ? " or " : "") + " ddo_comprobante = " + item.plc_comprobante;
                }
                if (where != "")
                {
                    parametros.where += "and (" + where + ")";
                    lista = vCancelacionBLL.GetAll1(parametros, "");
                }

            }





            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span8\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(2, 2);
            tdatos.rows[0].cells[0].valor = "Planilla:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtDOCTRAN_DS", valor = planilla.com_doctran, clase = Css.medium, habilitado = false }.ToString() + new Input { id = "txtCODIGO_DS", visible = false, valor = planilla.com_codigo }.ToString();
            tdatos.rows[1].cells[0].valor = "";
            tdatos.rows[1].cells[1].valor = new Boton { click = "CalcularValores();return false;", valor = "Calcular" }.ToString();


            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span8-->");
            html.AppendLine("<div class=\"span3\">");
            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(2, 2);
            tdatos1.rows[0].cells[0].valor = "% Desc planilla:";
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtDESCPORCENTAJE_DS", clase = Css.mini }.ToString() + new Boton { small = true, id = "btnpall", tooltip = "Decontar % a todos ", clase = "iconsweets-arrowdown", click = "AddPorcentaje();" }.ToString();
            tdatos1.rows[1].cells[0].valor = "$ Desc planilla::";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtDESCVALOR_DS", clase = Css.mini }.ToString() + new Boton { small = true, id = "btnvall", tooltip = "Decontar $ a todos ", clase = "iconsweets-arrowdown", click = "AddValor();" }.ToString();



            html.AppendLine(tdatos1.ToString());
            html.AppendLine(" </div><!--span3-->");
            html.AppendLine("</div><!--row-fluid-->");
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span11\">");
            HtmlTable tddet = new HtmlTable();
            tddet.id = "td_DS";
            tddet.invoice = true;
            tddet.clase = "scrolltable";
            tddet.footer = true;


            tddet.AddColumn("Documento", "", "", "");
            tddet.AddColumn("Guia", "", "", "");
            tddet.AddColumn("Nro", "", "", "");
            tddet.AddColumn("Emisión", "", "", "");
            tddet.AddColumn("Recibo", "", "", "");
            tddet.AddColumn("Socio", "", "", "");
            tddet.AddColumn("Valor", "", Css.right, "");
            tddet.AddColumn("Valor Subtotal", "", Css.right, "");
            tddet.AddColumn("Valor Planilla", Css.mini, Css.right, "");
            tddet.AddColumn("% Desc", Css.mini, Css.right, "");
            tddet.AddColumn("$ Desc", Css.mini, Css.right, "");

            //decimal valor = comprobante.total.tot_total;

            foreach (vCancelacion item in lista)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-comprobante=" + item.ddo_comprobante + " data-transac=" + item.ddo_transacc + " data-comprobantecan=" + item.dca_comprobante_can + " data-secuencia=" + item.dca_secuencia;
                //row.clickevent = "EditAfec(this)";

                row.cells.Add(new HtmlCell { valor = item.ddo_doctran });
                row.cells.Add(new HtmlCell { valor = item.doctran_guia });
                row.cells.Add(new HtmlCell { valor = item.ddo_pago });
                row.cells.Add(new HtmlCell { valor = item.ddo_fecha_emi.Value.ToShortDateString() });
                //row.cells.Add(new HtmlCell { valor = item.ddo_fecha_ven.Value.ToShortDateString() });                
                row.cells.Add(new HtmlCell { valor = item.doctran_can });
                row.cells.Add(new HtmlCell { valor = item.apellidossocio + " " + item.nombressocio });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.dca_monto), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.subtotal), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = new Input() { clase = Css.mini + Css.amount, valor = Formatos.CurrencyFormat(item.dca_monto_pla), habilitado = (item.dca_planilla.HasValue ? false : true) }.ToString(), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = new Input() { clase = Css.mini + Css.amount, habilitado = (item.dca_planilla.HasValue ? false : true) }.ToString(), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = new Input() { clase = Css.mini + Css.amount, habilitado = (item.dca_planilla.HasValue ? false : true) }.ToString(), clase = Css.right });


                tddet.AddRow(row);


            }
            html.AppendLine(tddet.ToString());
            html.AppendLine(" </div><!--span11-->");
            html.AppendLine("</div><!--row-fluid-->");





            return html.ToString();

        }

        [WebMethod]
        public string SaveValoresSocios(object objeto)
        {
            List<Dcancelacion> detalle = new List<Dcancelacion>();
            if (objeto != null)
            {
                Array array = (Array)objeto;
                foreach (Object item in array)
                {
                    if (item != null)
                        detalle.Add(new Dcancelacion(item));
                }
            }
            BLL transaction = new BLL();
            transaction.CreateTransaction();

            try
            {
                transaction.BeginTransaction();

                foreach (Dcancelacion item in detalle)
                {
                    if (item.dca_monto_pla > item.dca_monto)
                        throw new Exception("ERROR no se puede asginar un valor mayor al cancelado");
                    
                    Dcancelacion itemup = new Dcancelacion();
                    itemup.dca_empresa_key = item.dca_empresa;
                    itemup.dca_comprobante_key = item.dca_comprobante;
                    itemup.dca_transacc_key = item.dca_transacc;
                    itemup.dca_doctran_key = item.dca_doctran.Trim();
                    itemup.dca_pago_key = item.dca_pago;
                    itemup.dca_comprobante_can_key = item.dca_comprobante_can;
                    itemup.dca_secuencia_key = item.dca_secuencia;
                    itemup = DcancelacionBLL.GetByPK(itemup);
                    itemup.dca_empresa_key = item.dca_empresa;
                    itemup.dca_comprobante_key = item.dca_comprobante;
                    itemup.dca_transacc_key = item.dca_transacc;
                    itemup.dca_doctran_key = item.dca_doctran.Trim();
                    itemup.dca_pago_key = item.dca_pago;
                    itemup.dca_comprobante_can_key = item.dca_comprobante_can;
                    itemup.dca_secuencia_key = item.dca_secuencia;
                    itemup.dca_monto_pla = item.dca_monto_pla;
                    DcancelacionBLL.Update(transaction, itemup);
                }
                transaction.Commit();
                return "OK";
                //return codigo.ToString();
            }
            catch
            {
                transaction.Rollback();
                return "ERROR";
            }


        }

        #endregion


        #region Electronico

        [WebMethod]
        public string GetElectronicoData(object objeto)
        {

            Comprobante com = new Comprobante(objeto);
            com.com_empresa_key = com.com_empresa;
            com.com_codigo_key = com.com_codigo;

            com = ComprobanteBLL.GetByPK(com);
            com = Packages.Electronico.UpdateElectronicoData(com);


            string[] retorno = new string[2];
            retorno[0] = com.com_estadoelec;
            retorno[1] = com.com_mensajeelec;
            return new JavaScriptSerializer().Serialize(retorno);

        }

        [WebMethod]
        public string GenerarElectronico(object objeto)
        {

            Comprobante com = new Comprobante(objeto);
            com.com_empresa_key = com.com_empresa;
            com.com_codigo_key = com.com_codigo;
            com = ComprobanteBLL.GetByPK(com);




            bool ret = Packages.Electronico.GenerateElectronico(com);
            return ret ? "ok" : "error";

        }

        [WebMethod]
        public string ElectronicRIDE(object objeto)
        {
            Comprobante com = new Comprobante(objeto);
            com.com_empresa_key = com.com_empresa;
            com.com_codigo_key = com.com_codigo;
            com = ComprobanteBLL.GetByPK(com);
            return Packages.Electronico.ElectronicRIDE(com);

        }

        #endregion

    }
}
