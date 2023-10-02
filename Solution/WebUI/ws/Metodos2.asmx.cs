using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Text;
using Services;
using BusinessObjects;
using BusinessLogicLayer;
using System.IO;
using Functions;
using Packages;
using HtmlObjects;
using System.Collections;

namespace WebUI.ws
{
    /// <summary>
    /// Summary description for Metodos2
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Metodos2 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        #region Menu

        public string ActiveMenuOption(string path, string opcion, bool padre)
        {
            if (path.IndexOf(opcion) >= 0)
            {
                return (padre) ? "active open" : "active";
            }
            return "";
        }

        public static string GetMenuOptions(List<Menu> lst, int? padre, int idmenu, bool submenu, ref string clase)
        {

            List<Menu> lsthijos = lst.FindAll(delegate (Menu m) { return m.men_padre == padre; });
            StringBuilder html = new StringBuilder();
            if (lsthijos.Count > 0)
            {
                //clase = "";
                if (submenu)
                    html.AppendLine("<ul class='sub-menu'>");

                foreach (Menu obj in lsthijos)
                {

                    string clasemenu = "";
                    string htmlhijos = GetMenuOptions(lst, obj.men_id, idmenu, true, ref clase);
                    //if (htmlhijos != "")
                    //    hijos = true;
                    if (obj.men_id == idmenu)
                    {
                        clasemenu = "active";
                        clase = "active open";
                    }


                    if (htmlhijos == "")
                    {
                        html.AppendFormat("<li class='{0}' >", clasemenu);
                    
                        html.AppendFormat("<a href='javascript:SelectOptionMenu(\"{2}\",\"{0}\");'><i class='{1}'></i> {2}</a>", obj.men_formulario, obj.men_imagen, obj.men_nombre);
                        //html.AppendFormat("<a href='{0}'><i class='{1}'></i> {2}</a>", obj.men_formulario, obj.men_imagen, obj.men_nombre);
                    }
                    else
                    {
                        html.AppendFormat("<li class='{0}' >", clase);
                        html.AppendFormat("<a href='{0}'><i class='{1}'></i><span class='title'>{2}</span><span class='{3}'></span><span class='arrow {4} '></span></a>", obj.men_formulario, obj.men_imagen, obj.men_nombre, clase != "" ? "selected" : "", clase != "" ? "open" : "");
                        clase = "";
                    }


                    html.AppendLine(htmlhijos);
                    html.Append("</li>");

                }
                if (submenu)
                    html.AppendLine("</ul>");
            }

            return html.ToString();
        }

        public static string GetMenuOptions(List<Perfilxmenu> lst, int? padre, int idmenu, bool submenu, ref string clase)
        {

            List<Perfilxmenu> lsthijos = lst.FindAll(delegate (Perfilxmenu m) { return m.pxm_menupadre == padre; });
            StringBuilder html = new StringBuilder();
            if (lsthijos.Count > 0)
            {
                //clase = "";
                if (submenu)
                    html.AppendLine("<ul class='sub-menu'>");

                foreach (Perfilxmenu obj in lsthijos)
                {

                    string clasemenu = "";
                    string htmlhijos = GetMenuOptions(lst, obj.pxm_menu, idmenu, true, ref clase);
                    //if (htmlhijos != "")
                    //    hijos = true;
                    if (obj.pxm_menu == idmenu)
                    {
                        clasemenu = "active";
                        clase = "active open";
                    }


                    if (htmlhijos == "")
                    {
                        html.AppendFormat("<li class='{0}' >", clasemenu);

                        html.AppendFormat("<a href='javascript:SelectOptionMenu(\"{2}\",\"{0}\");'><i class='{1}'></i> {2}</a>", obj.pxm_menformualrio, obj.pxm_menuorden, obj.pxm_nombremenu);
                        //html.AppendFormat("<a href='{0}'><i class='{1}'></i> {2}</a>", obj.men_formulario, obj.men_imagen, obj.men_nombre);
                    }
                    else
                    {
                        html.AppendFormat("<li class='{0}' >", clase);
                        html.AppendFormat("<a href='{0}'><i class='{1}'></i><span class='title'>{2}</span><span class='{3}'></span><span class='arrow {4} '></span></a>", obj.pxm_menformualrio, obj.pxm_menuimagen, obj.pxm_nombremenu, clase != "" ? "selected" : "", clase != "" ? "open" : "");
                        clase = "";
                    }


                    html.AppendLine(htmlhijos);
                    html.Append("</li>");

                }
                if (submenu)
                    html.AppendLine("</ul>");
            }

            return html.ToString();
        }

        public static string GetMenuOptions(List<vMenuUsuario> lst, int? padre, int idmenu, bool submenu, ref string clase)
        {

            List<vMenuUsuario> lsthijos = lst.FindAll(delegate (vMenuUsuario m) { return m.men_padre == padre; });
            StringBuilder html = new StringBuilder();
            if (lsthijos.Count > 0)
            {
                //clase = "";
                if (submenu)
                    html.AppendLine("<ul class='sub-menu'>");

                foreach (vMenuUsuario obj in lsthijos)
                {

                    string clasemenu = "";
                    string htmlhijos = GetMenuOptions(lst, obj.men_id, idmenu, true, ref clase);
                    //if (htmlhijos != "")
                    //    hijos = true;
                    if (obj.men_id == idmenu)
                    {
                        clasemenu = "active";
                        clase = "active open";
                    }


                    if (htmlhijos == "")
                    {
                        html.AppendFormat("<li class='{0}' >", clasemenu);

                        html.AppendFormat("<a href='javascript:SelectOptionMenu(\"{2}\",\"{0}\");'><i class='{1}'></i> {2}</a>", obj.men_formulario, obj.men_orden, obj.men_nombre);
                        //html.AppendFormat("<a href='{0}'><i class='{1}'></i> {2}</a>", obj.men_formulario, obj.men_imagen, obj.men_nombre);
                    }
                    else
                    {
                        html.AppendFormat("<li class='{0}' >", clase);
                        html.AppendFormat("<a href='{0}'><i class='{1}'></i><span class='title'>{2}</span><span class='{3}'></span><span class='arrow {4} '></span></a>", obj.men_formulario, obj.men_imagen, obj.men_nombre, clase != "" ? "selected" : "", clase != "" ? "open" : "");
                        clase = "";
                    }


                    html.AppendLine(htmlhijos);
                    html.Append("</li>");

                }
                if (submenu)
                    html.AppendLine("</ul>");
            }

            return html.ToString();
        }



        public static string GetMenuOptions(List<Perfilxmenu> lst, int? padre, int? idmenu, bool submenu, ref string clase)
        {

            List<Perfilxmenu> lsthijos = lst.FindAll(delegate (Perfilxmenu m) { return m.pxm_menupadre == padre; });
            StringBuilder html = new StringBuilder();
            if (lsthijos.Count > 0)
            {
                //clase = "";
                if (submenu)
                    html.AppendLine("<ul class='sub-menu'>");

                foreach (Perfilxmenu obj in lsthijos)
                {

                    string clasemenu = "";
                    string htmlhijos = GetMenuOptions(lst, obj.pxm_menu, idmenu, true, ref clase);
                    //if (htmlhijos != "")
                    //    hijos = true;
                    if (idmenu.HasValue)
                    {
                        if (obj.pxm_menu == idmenu.Value)
                        {
                            clasemenu = "active";
                            clase = "active open";
                        }
                    }


                    if (htmlhijos == "")
                    {
                        html.AppendFormat("<li class='nav-item {0}' >", clasemenu);

                        html.AppendFormat("<a href='javascript:SelectOptionMenu(\"{2}\",\"{0}\");' class='nav-link' ><i class='{1}'></i><span class='title'>{2}</span></a>", obj.pxm_menformualrio, obj.pxm_menuimagen, obj.pxm_nombremenu);
                        //html.AppendFormat("<a href='javascript:SelectOptionMenu(\"{2}\",\"{0}\");' class='nav-link'><i class='{1}'></i> {2}</a>", obj.pxm_menformualrio, obj.pxm_menuimagen, obj.pxm_nombremenu);
                    }
                    else
                    {
                        html.AppendFormat("<li class='nav-item {0}' >", clase);
                        html.AppendFormat("<a href='{0}' class='nav-link nav-toggle'><i class='{1}'></i><span class='title'>{2}</span><span class='{3}'></span><span class='arrow {4} '></span></a>", obj.pxm_menformualrio, obj.pxm_menuimagen, obj.pxm_nombremenu, clase != "" ? "selected" : "", clase != "" ? "open" : "");
                        clase = "";
                    }


                    html.AppendLine(htmlhijos);
                    html.Append("</li>");

                }
                if (submenu)
                    html.AppendLine("</ul>");
            }

            return html.ToString();
        }




        public static bool IsInPath(int id, string path)
        {
            string[] arraypath = path.Split(',');
            foreach (string item in arraypath)
            {
                if (item == id.ToString())
                    return true;
            }
            return false;
        }

        public string GetMenuPath(int id, List<Menu> menu)
        {
            Menu m = menu.Find(delegate (Menu me) { return me.men_id == id; });
            if (m.men_padre.HasValue)
                return id + "," + GetMenuPath(m.men_padre.Value, menu);
            else
                return id.ToString();

        }

        [WebMethod]
        public string GetMenuByUser(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object page = null;
            object query = null;
            tmp.TryGetValue("page", out page);
            tmp.TryGetValue("query", out query);
            List<Menu> menu = MenuBLL.GetAll("", "");

            string pagina = page.ToString();
            if (query != null)
            {
                if (query.ToString() != "")
                    pagina = page + "?" + query.ToString();
            }


            Menu m = menu.Find(delegate (Menu me) { return me.men_formulario.Contains(pagina); });
            if (m == null)
                m = menu.Find(delegate (Menu me) { return me.men_formulario.Contains(page.ToString()); });
            string path = "";
            if (m != null)
                path = GetMenuPath(m.men_id, menu);

            Usuario usr = new Usuario(objeto);
            usr.usr_id = usr.crea_usr;
            usr.usr_id_key = usr.crea_usr;
            usr = UsuarioBLL.GetByPK(usr);


            List<Perfilxmenu> lst = PerfilxmenuBLL.GetAll(new WhereParams("men_estado=1 and pxm_perfil={0}", usr.usr_perfil), "pxm_menuorden");

            StringBuilder html = new StringBuilder();

            //html.Append("<li class='sidebar-toggler-wrapper'><div class='sidebar-toggler'></div></li>");
            html.Append("<li class='heading'><h3 class='uppercase'>Menú</h3></li>");
            string clase = "";
            html.Append(GetMenuOptions(lst, null, m != null ? m.men_id : (int?)null, false, ref clase));



            //return html.ToString();

            string[] retorno = new string[4];
            retorno[0] = html.ToString();
            retorno[1] = "";
            retorno[2] = "<li><i class='fa fa-home'></i><a href = 'inicio.html'> Inicio</a><i class='fa fa-angle-right'></i></li>";
            retorno[3] = "";

            if (m != null)
            {
                //GET PAGE TIITLE
                retorno[1] = string.Format("{0} <small>{1}</small>", m.men_titulo, m.men_descripcion);

                //GET NAVMENU
                StringBuilder htmlnav = new StringBuilder();
                htmlnav.Append("<li><i class='fa fa-home'></i><a href = 'inicio.html'> Inicio</ a ><i class='fa fa-angle-right'></i></li>");
                htmlnav.Append(GetNavMenu(m));
                retorno[2] = htmlnav.ToString();


                //GET OPCIONES
                StringBuilder htmlopc = new StringBuilder();

                if (!string.IsNullOrEmpty(m.men_opciones))
                {
                    var serializer = new JavaScriptSerializer();
                    List<OpcionPagina> lstopc = serializer.Deserialize<List<OpcionPagina>>(m.men_opciones);
                    if (lstopc != null)
                    {
                        if (lstopc.Count > 0)
                        {
                            foreach (OpcionPagina opc in lstopc)
                            {
                                htmlopc.AppendFormat("<li><a href='#' onclick = '{0}'>{1}</a></li>", opc.onclick, opc.opcion);
                            }
                            htmlopc.Append("<li class='divider'></li>");
                        }
                    }
                }
                htmlopc.AppendFormat("<li><a href='#' onclick = '{0}'>{1}</a></li>", "", "Ayuda");
                retorno[3] = htmlopc.ToString();

            }


            return new JavaScriptSerializer().Serialize(retorno);



        }



        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetMenu(object objeto)
        {
            JsonObj obj = new JsonObj(objeto); 

            Menu menu = new Menu(objeto);

            menu.men_id_key = menu.men_id;
            menu = MenuBLL.GetByPK(menu);




            List<vMenuUsuario> lst = vMenuUsuarioBLL.GetAll(new WhereParams("usr_id = {0} and men_estado =1", obj.crea_usr), "men_orden");
            StringBuilder html = new StringBuilder();

            html.Append("<li class='sidebar-toggler-wrapper'><div class='sidebar-toggler'></div></li>");

            string clase = "";

                html.Append(GetMenuOptions(lst, null, menu.men_id, false, ref clase));

            return html.ToString();



            //Usuario usr = new Usuario(objeto);
            //usr.usr_perfil = usr.usr_id;
            ////usr.usr_id_key = usr.usr_id;
            ////usr = UsuarioBLL.GetByPK(usr);



            //WhereParams parametros = new WhereParams();
            //List<object> valores = new List<object>();
            //parametros.where = "men_estado=1";

            //if (!string.IsNullOrEmpty(usr.usr_perfil))
            //{
            //    parametros.where += ((parametros.where != "") ? " and " : "") + " pxm_perfil = {" + valores.Count + "} ";
            //    valores.Add(usr.usr_perfil);


            //}
            //parametros.valores = valores.ToArray();

            //List<Perfilxmenu> lst = PerfilxmenuBLL.GetAll(parametros, "pxm_menuorden");
            //List<Menu> lstmenu = MenuBLL.GetAll("men_estado=1", "men_orden");


            //StringBuilder html = new StringBuilder();

            //html.Append("<li class='sidebar-toggler-wrapper'><div class='sidebar-toggler'></div></li>");

            //string clase = "";
            //if (usr.usr_perfil == "admin")

            //    html.Append(GetMenuOptions(lstmenu, null, menu.men_id, false, ref clase));
            //else
            //    html.Append(GetMenuOptions(lst, null, menu.men_id, false, ref clase));



            //return html.ToString();
        }





        #endregion

        #region Pages

        [WebMethod]
        public string GetPageTitle(object objeto)
        {
            Menu menu = new Menu(objeto);
            menu.men_id_key = menu.men_id;
            menu = MenuBLL.GetByPK(menu);

            StringBuilder html = new StringBuilder();
            html.AppendFormat("{0} <small>{1}</small>", menu.men_titulo, menu.men_descripcion);
            return html.ToString();


        }


        public string GetNavMenu(Menu men)
        {
            string html = "";
            if (men.men_padre.HasValue)            
            {
                Menu padre = MenuBLL.GetByPK(new Menu { men_id = men.men_padre.Value, men_id_key = men.men_padre.Value });
                html += GetNavMenu(padre);
            }

            html += string.Format("<li><a href = '{0}'>{1}</a><i class='fa fa-angle-right'></i></li>", !string.IsNullOrEmpty(men.men_formulario) ? men.men_formulario: "#", men.men_nombre);
            return html;

        }


        [WebMethod]
        public string GetPageNav(object objeto)
        {
            Menu menu = new Menu(objeto);
            menu.men_id_key = menu.men_id;
            menu = MenuBLL.GetByPK(menu);




            StringBuilder html = new StringBuilder();


            html.Append("<li><i class='fa fa-home'></i><a href = 'index.html'> Inicio</ a ><i class='fa fa-angle-right'></i></li>");
            html.Append(GetNavMenu(menu));
            return html.ToString();


        }

        [WebMethod]
        public string GetPageOpc(object objeto)
        {
            Menu menu = new Menu(objeto);
            menu.men_id_key = menu.men_id;
            menu = MenuBLL.GetByPK(menu);

            StringBuilder html = new StringBuilder();

            if (!string.IsNullOrEmpty(menu.men_opciones))
            {
                var serializer = new JavaScriptSerializer();

                List<OpcionPagina> lstopc = serializer.Deserialize<List<OpcionPagina>>(menu.men_opciones);



                if (lstopc != null)
                {
                    if (lstopc.Count > 0)
                    {

                        foreach (OpcionPagina opc in lstopc)
                        {
                            html.AppendFormat("<li><a href='#' onclick = '{0}'>{1}</a></li>", opc.onclick, opc.opcion);
                        }
                        html.Append("<li class='divider'></li>");
                        html.AppendFormat("<li><a href='#' onclick = '{0}'>{1}</a></li>", "", "Ayuda");


                    }
                }
            }
            return html.ToString();


        }


        #endregion

        #region Login

        [WebMethod]
        public string ValidaIngreso(Usuario usr)
        {
            string retorono = "OK";

            return retorono;
        }

        [WebMethod]
        public string Login(object objeto)
        {

            Usuario usuario = new Usuario(objeto);
            usuario.usr_id_key = usuario.usr_id;
            string pass = usuario.usr_password;
            usuario = UsuarioBLL.GetByPK(usuario);

            object[] retorno = new object[2];
            if (usuario.usr_estado.HasValue)
            {
                if (usuario.usr_password == pass)
                {
                    retorno[0] = ValidaIngreso(usuario);
                    retorno[1] = usuario;
                }
                else
                    retorno[0] = "Contraseña incorrecta";
            }

            else
                retorno[0] = "No existe el usuario";

            return new JavaScriptSerializer().Serialize(retorno);
        }

        [WebMethod]
        public string SelectEmpresasUsuario(object objeto)
        {
            Usuario usr = new Usuario(objeto);
            usr.usr_id_key = usr.usr_id;
            usr = UsuarioBLL.GetByPK(usr);

            string codigo = "";

            StringBuilder html = new StringBuilder();
            object[] retorno = new object[3];

            html.AppendLine("<div class=\"portlet-body form\">");

            html.Append("<form role=\"form\">");
            html.Append("<div class=\"form-body\">");


            html.Append("<div class=\"form-group\">");
            html.Append("<label style='color:Black;'>Empresa</label>");
            html.AppendLine("<select id='cmbempresa_sel' class=\"form-control input-large\">");

            WhereParams parametros = new WhereParams();
            if (usr.usr_perfil != Constantes.cPerfilRoot)
            {
                parametros = new WhereParams(" uxe_usuario={0}", usr.usr_id);
                List<Usuarioxempresa> lst = UsuarioxempresaBLL.GetAll(parametros, "");
                if (lst.Count > 1)
                {
                    retorno[0] = "html";
                    foreach (Usuarioxempresa item in lst)
                    {
                        html.AppendFormat("<option value={0}>{1}</option>", item.uxe_empresa, item.uxe_nombreempresa);
                    }
                }
                else
                {
                    retorno[0] = "obj";
                    retorno[1] = lst[0].uxe_empresa;
                    retorno[2] = lst[0].uxe_nombreempresa;
                }
            }
            else
            {
                List<Empresa> lst = EmpresaBLL.GetAll("", "");
                if (lst.Count > 0)
                {
                    retorno[0] = "html";
                    foreach (Empresa item in lst)
                    {
                        html.AppendFormat("<option value={0}>{1}</option>", item.emp_codigo, item.emp_nombre);
                    }
                }
                else
                {
                    retorno[0] = "obj";
                    retorno[1] = lst[0].emp_codigo;
                    retorno[2] = lst[0].emp_nombre;
                }
            }
            html.AppendLine("</select>");
            html.Append("</div>");



            html.Append("</div>");
            html.Append("</form>");
            html.Append("</div>");

            if (retorno[0] == "html")
                retorno[1] = html.ToString();
            return new JavaScriptSerializer().Serialize(retorno);



        }

        #endregion

        #region Cuentas Contables


        public string GetTreeNivel(Cuenta cuenta, List<Cuenta> lst)
        {
            List<Cuenta> lstnivel = new List<Cuenta>();
            if (cuenta != null)
                lstnivel = lst.FindAll(delegate (Cuenta c) { return c.cue_reporta == cuenta.cue_codigo; });
            else
                lstnivel = lst.FindAll(delegate (Cuenta c) { return c.cue_reporta == null; });

            StringBuilder html = new StringBuilder();
            html.Append("<ul>");
            if (lstnivel.Count > 0)
            {
                foreach (Cuenta cue in lstnivel)
                {

                    bool negrita = true;
                    if (cue.cue_movimiento.HasValue)
                        if (cue.cue_movimiento.Value == 1)
                            negrita = false;

                    bool habilitado = false;
                    if (cue.cue_estado.HasValue)
                        if (cue.cue_estado == (int)Enums.EstadoRegistro.ACTIVO)
                            habilitado = true;

                    html.AppendFormat("<li class='{1}' data-codigo='{0}'>", cue.cue_codigo, habilitado ? " ctaactiva " : "ctainactiva");
                    if (negrita)
                        html.AppendFormat("<b>{0}.{1}</b>", cue.cue_id, cue.cue_nombre);
                    else
                        html.AppendFormat("{0}.{1}", cue.cue_id, cue.cue_nombre);

                    html.Append(GetTreeNivel(cue, lst));
                    html.Append("</li>");
                }
            }
            html.Append("</ul>");
            return html.ToString();
        }


        [WebMethod]
        public string GetCuentasTree(object objeto)
        {
            JsonObj obj = new JsonObj(objeto);
            List<Cuenta> lst = CuentaBLL.GetAll("cue_empresa=" + obj.empresa, "cue_id");

            List<Cuenta> lstniv0 = lst.FindAll(delegate (Cuenta c) { return c.cue_reporta == null; });


            StringBuilder html = new StringBuilder();

            html.Append("<div id=\"tree\" class=\"tree-demo\" >");
            html.Append(GetTreeNivel(null, lst));

            html.AppendLine("</div>");

            return html.ToString();


        }


        [WebMethod]
        public string EditCuenta(object objeto)
        {

            Cuenta cuenta = new Cuenta(objeto);

            Cuenta padre = CuentaBLL.GetByPK(new Cuenta { cue_empresa = cuenta.cue_empresa, cue_empresa_key = cuenta.cue_empresa, cue_codigo = cuenta.cue_reporta.Value, cue_codigo_key = cuenta.cue_reporta.Value });

            cuenta.cue_codigo_key = cuenta.cue_codigo;
            cuenta.cue_empresa_key = cuenta.cue_empresa;
            cuenta = CuentaBLL.GetByPK(cuenta);

            if (cuenta.cue_codigo == 0)
                cuenta.cue_id = Contabilidad.GetIdCuenta(padre);


            StringBuilder html = new StringBuilder();

            html.Append("<div id='editcta' data-codigo='" + cuenta.cue_codigo + "' class='row form-group'>");

            html.Append(Document.LabelInput("txtID_CUE", cuenta.cue_id, "Id", "", "", "col-md-4", false, false, ElementEnums.InputType.text));
            html.Append(Document.LabelInput("txtCODIGO_CUE", cuenta.cue_codigo.ToString(), "Código", "", "", "col-md-4", true, false, ElementEnums.InputType.text));
            html.Append(Document.LabelInput("txtNOMBRE_CUE", cuenta.cue_nombre, "Cuenta", "", "", "col-md-10", false, false, ElementEnums.InputType.text));
            html.Append(Document.LabelSelect("cmbGENERO_CUE", cuenta.cue_genero.HasValue ? cuenta.cue_genero.Value.ToString() : "", Dictionaries.GetGeneros(), "Genero", "", "", "", "col-md-8", false, false, false, true));
            html.Append(Document.LabelSelect("cmbMODULO_CUE", cuenta.cue_modulo.HasValue ? cuenta.cue_modulo.Value.ToString() : "", Dictionaries.GetModulos(), "Módulo", "", "", "", "col-md-8", false, false, false, true));
            html.Append(Document.LabelInput("chkMOVIMIENTO_CUE", cuenta.cue_movimiento.HasValue ? (cuenta.cue_movimiento.Value == 1) ? "true" : "false" : "false", "Movimiento", "data-on-text='SI' data-off-text='NO'", "make-switch", "col-md-4", false, true, ElementEnums.InputType.checkbox));
            html.Append(Document.LabelInput("chkVISUALIZA_CUE", cuenta.cue_visualiza.HasValue ? (cuenta.cue_visualiza.Value == 1) ? "true" : "false" : "false", "Visualiza", "data-on-text='SI' data-off-text='NO'", "make-switch", "col-md-4", false, true, ElementEnums.InputType.checkbox));
            html.Append(Document.LabelInput("chkNEGRITA_CUE", cuenta.cue_negrita.HasValue ? (cuenta.cue_negrita.Value == 1) ? "true" : "false" : "false", "Negrita", "data-on-text='SI' data-off-text='NO'", "make-switch", "col-md-4", false, true, ElementEnums.InputType.checkbox));
            html.Append(Document.LabelSelect("cmbESTADO_CUE", cuenta.cue_estado.HasValue ? cuenta.cue_estado.Value.ToString() : "1", Dictionaries.GetEstadosRegistro(), "Estado", "", "", "", "col-md-6", false, false, false, true));
            
            html.Append("</div>");
            
           
            html.AppendLine(string.Format("<p><small class='desc' id='lblcrea'>Creado por:{0} {1:dd/MM/yyyy HH:ss}</small><br><small class='desc' id='lblmod'>Modificado por:{2} {3:dd/MM/yyyy HH:ss}</small></p>", cuenta.crea_usr, cuenta.crea_fecha, cuenta.mod_usr, cuenta.mod_fecha));




            return html.ToString();



        }

        [WebMethod]
        public string SaveCuenta(object objeto)
        {
            Cuenta cue = new Cuenta(objeto);
            Cuenta cueUP = new Cuenta();
            cueUP.cue_empresa = cue.cue_empresa;
            cueUP.cue_empresa_key = cue.cue_empresa;
            cueUP.cue_codigo = cue.cue_codigo;
            cueUP.cue_codigo_key = cue.cue_codigo;
            cueUP = CuentaBLL.GetByPK(cueUP);

            Cuenta padre = new Cuenta();
            if (cue.cue_reporta.HasValue)
            {
                if (cue.cue_reporta.Value > 0)
                {
                    padre.cue_empresa = cue.cue_empresa;
                    padre.cue_empresa_key = cue.cue_empresa;
                    padre.cue_codigo = cue.cue_reporta.Value;
                    padre.cue_codigo_key = cue.cue_reporta.Value;
                    padre = CuentaBLL.GetByPK(padre);
                }
            }

            if (cueUP.crea_fecha.HasValue)
            {
                cueUP.cue_empresa_key = cue.cue_empresa;
                cueUP.cue_codigo_key = cue.cue_codigo;
                cueUP.cue_id = cue.cue_id;
                cueUP.cue_nombre = cue.cue_nombre;
                cueUP.cue_genero = cue.cue_genero;
                cueUP.cue_movimiento = cue.cue_movimiento;
                cueUP.cue_modulo = cue.cue_modulo;
                cueUP.cue_visualiza = cue.cue_visualiza;
                cueUP.cue_nivel = cue.cue_nivel;
                cueUP.cue_negrita = cue.cue_negrita;
                cueUP.cue_reporta = padre.cue_codigo;//padre.cue_nivel.Value + 1;
                cueUP.cue_estado = cue.cue_estado;
                cueUP.mod_usr = cue.mod_usr;
                cueUP.mod_fecha = DateTime.Now;
                CuentaBLL.Update(cueUP);
                //return new JavaScriptSerializer().Serialize(apeUP);
            }
            else
            {
                int max = CuentaBLL.GetMax("cue_codigo") + 1;
                cue.cue_nivel = padre.cue_nivel + 1;
                cue.cue_orden = max;
                cue.cue_codigo = CuentaBLL.InsertIdentity(cue);
                //return new JavaScriptSerializer().Serialize(ape);
            }

            object[] retorno = new object[2];
            retorno[0] = "OK";
            retorno[1] = cue;

            return new JavaScriptSerializer().Serialize(retorno);


        }


        [WebMethod]
        public string RemoveCuenta(object objeto)
        {
            Cuenta cuenta = new Cuenta(objeto);
            try
            {

                CuentaBLL.Delete(cuenta);
                return "OK";
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }



        }

        #endregion

        #region Producto Saldo

        [WebMethod]
        public string GetFiltrosProductoSaldo(object objeto)
        {

            Producto obj = new Producto(objeto);
                        
            Usuario usr = UsuarioBLL.GetByPK(new Usuario { usr_id = obj.crea_usr, usr_id_key = obj.crea_usr });
            

            StringBuilder html = new StringBuilder();
            //html.AppendLine("<form role = \"form\" action = \"#\" >");
            //INICIO PRIMER FORM GRUOUP

            html.AppendLine("<div class=\"row form-group\">");
            html.AppendLine("</div>");

            return html.ToString();
        }

        #endregion

        #region Cerrar Hojas de Ruta
        

        [WebMethod]
        public string GetFiltrosCerrarHR(object objeto)
        {

            Comprobante obj = new Comprobante(objeto);

            Usuario usr = UsuarioBLL.GetByPK(new Usuario { usr_id = obj.crea_usr, usr_id_key = obj.crea_usr });


            StringBuilder html = new StringBuilder();
            //html.AppendLine("<form role = \"form\" action = \"#\" >");
            //INICIO PRIMER FORM GRUOUP

            html.AppendLine("<div class=\"row form-group\">");            
            html.Append(Document.LabelIconSelect("cmbalmacen_f", "", Dictionaries.GetIDAlmacen(), "Almacen", "fa-building", "", "", "col-md-4", true, false, false, false).ToString());
            html.Append(Document.LabelIconSelect("cmbpventa_f", "", Dictionaries.Empty(), "Punto venta", "fa-laptop", "", "", "col-md-4", true, false, false, false).ToString());
            html.Append(Document.LabelIconInput("txtnumero_f", "", "Numero", "fa-barcode", "", "", "col-md-4", ElementEnums.InputType.text).ToString());
            
            html.AppendLine("</div>");

            html.AppendLine("<div class=\"row form-group\">");
            html.Append(Document.LabelIconDateRange("txtfecha", null, null, "Fecha", "fa-calendar", "", "", "col-md-4", false, false).ToString());
            html.Append(Document.LabelIconInput("txtpersona_f", "", "Socio", "fa-user", "", "", "col-md-4", ElementEnums.InputType.text).ToString());
            html.Append(Document.LabelIconSelect("cmbestado_f", "", Dictionaries.GetEstadoComprobante(), "Estado", "fa-tag", "", "", "col-md-4", false, true, false, false).ToString());
            html.AppendLine("</div>");

            html.AppendLine("<button class=\"btn blue btn-block margin-top-20\" id='btnsearch'>BUSCAR<i class=\"m-icon-swapright m-icon-white\"></i></button>");

            return html.ToString();
        }

        [WebMethod]
        public string GetListadoCerrarHR(object objeto)
        {
            JsonObj jobj = new JsonObj(objeto);
            Comprobante obj = new Comprobante(objeto);

            int? almacen = (int?)Dictionaries.GetObject(objeto, "almacen", typeof(int?));
            int? pventa = (int?)Dictionaries.GetObject(objeto, "pventa", typeof(int?));
            int? numero = (int?)Dictionaries.GetObject(objeto, "numero", typeof(int?));
            DateTime? desde = (DateTime?)Dictionaries.GetObject(objeto, "desde", typeof(DateTime?));
            DateTime? hasta = (DateTime?)Dictionaries.GetObject(objeto, "hasta", typeof(DateTime?));
            string socio = (string) Dictionaries.GetObject(objeto, "persona", typeof(string));
            object[] estados = (object[])Dictionaries.GetObject(objeto, "estados", typeof(object[]));


            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();

            parametros.where = "com_empresa=" + jobj.empresa + " and com_tipodoc=" + Constantes.cHojaRuta.tpd_codigo;
            if ((almacen??0)>0)
            {
                parametros.where += " and com_almacen={" + valores.Count + "}";
                valores.Add(almacen.Value);
            }
            if ((pventa ?? 0) > 0)
            {
                parametros.where += " and com_pventa={" + valores.Count + "}";
                valores.Add(pventa.Value);
            }
            if ((numero ?? 0) > 0)
            {
                parametros.where += " and com_numero={" + valores.Count + "}";
                valores.Add(numero.Value);
            }
            if ((desde?? new DateTime()) > new DateTime())
            {
                parametros.where += " and com_fecha>={" + valores.Count + "}";
                valores.Add(desde.Value);
            }

            if ((hasta ?? new DateTime()) > new DateTime())
            {
                parametros.where += " and com_fecha<={" + valores.Count + "}";
                valores.Add(hasta.Value);
            }

            if (estados != null)
            {
                string whereestados = "";                
                foreach (object item in estados)
                {
                    if (item.ToString() != "")
                    {
                        whereestados += ((whereestados != "") ? " or " : "") + " com_estado = " + item.ToString();
                    }
                }
                parametros.where += (whereestados != "") ? " and (" + whereestados + ")" : "";
            }

           


            if (!string.IsNullOrEmpty(socio))
            {
                string[] arraynombres = socio.ToString().Split(' ');
                for (int i = 0; i < arraynombres.Length; i++)
                {
                    parametros.where += " and (per_ciruc ILIKE {" + valores.Count + "} or per_nombres ILIKE {" + valores.Count + "} or per_apellidos ILIKE{" + valores.Count + "}) ";
                    valores.Add("%" + arraynombres[i] + "%");

                }

            }


            parametros.valores = valores.ToArray();
            List<Comprobante> lst = ComprobanteBLL.GetAll(parametros, "com_fecha DESC");

            //List<vListado> lst = General.GetComprobantesGeneral(Conversiones.ObjectToDateTimeNull(desde), Conversiones.ObjectToDateTimeNull(hasta), jobj.empresa, Conversiones.ObjectToIntNull(almacen), Conversiones.ObjectToIntNull(pventa), Conversiones.ObjectToIntNull(bodega), (object[])tipo, (object[])estado, (string)numero, (string)persona, (string)detalle, (string)placa, (object[])usuario, true, usr.usr_id);



            StringBuilder html = new StringBuilder();

            foreach (Comprobante item in lst)
            {

                html.AppendFormat("<tr data-codigo='{0}'>", item.com_codigo);
                html.Append("<td>");
                if (item.com_estado == 0 || item.com_estado == 1)//Abiertos
                    html.Append(Document.CircleSmallButton("btnclose", "", "Cerrar Hoja de Ruta", "fa fa-close", "red", "CambiarEstado(" + item.com_codigo + ",2)"));
                if (item.com_estado == 2)//Cerrados
                    html.Append(Document.CircleSmallButton("btnopen", "", "Abrir Hoja de Ruta", "fa fa-check", "blue", "CambiarEstado(" + item.com_codigo + ",1)"));
                //html.Append(Document.DropdownButtons("cmbopc", "", "fa-angle-down", Dictionaries.GetOpcionesByTipoDoc(item.tipodoc.Value, item.estado.Value, usr.usr_perfil), "", "blue btn-sm", "opciones").ToString());
                html.Append("</td>");
                html.AppendFormat("<td><div class='textolistado'><a href = '#' onclick = 'CallFormulario({2});' >{0}</a></div><div class='textolistadomin estadonom'>{1}</div><div class='textolistadomin'>{2}</div></td>", item.com_doctran, Constantes.GetEstadoName(item.com_estado), item.com_codigo);

                html.AppendFormat("<td><div class='textolistado'>{0}</div><div class='textolistadomin'>{1} {2}<div><div class='textolistadomin'>{3} {4}</div></td>", item.com_fecha, item.crea_usr, item.crea_fecha, item.mod_usr, item.mod_fecha);
                html.AppendFormat("<td><div class='textolistado'>{0}</div><div class='textolistadomin'>{1}<div></td>", item.com_nombresocio, item.com_ciruc);
                html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", item.com_nombreruta);
                html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", item.com_total);
                html.AppendLine("</tr>");
            }

            

            return html.ToString();
        }


        [WebMethod]
        public string CambiarEstadoHR(object objeto)
        {
            JsonObj jobj = new JsonObj(objeto);
            Comprobante obj = new Comprobante(objeto);
            int? estado = (int?)Dictionaries.GetObject(objeto, "estado", typeof(int?));
            int? almacen = (int?)Dictionaries.GetObject(objeto, "almacen", typeof(int?));
            int? pventa = (int?)Dictionaries.GetObject(objeto, "pventa", typeof(int?));
            int? numero = (int?)Dictionaries.GetObject(objeto, "numero", typeof(int?));
            DateTime? desde = (DateTime?)Dictionaries.GetObject(objeto, "desde", typeof(DateTime?));
            DateTime? hasta = (DateTime?)Dictionaries.GetObject(objeto, "hasta", typeof(DateTime?));
            string socio = (string)Dictionaries.GetObject(objeto, "persona", typeof(string));
            object[] estados = (object[])Dictionaries.GetObject(objeto, "estados", typeof(object[]));

            List<Comprobante> lst = new List<Comprobante>();
            if (obj.com_codigo > 0)
            {
                obj.com_empresa_key = obj.com_empresa;
                obj.com_codigo_key = obj.com_codigo;
                Comprobante hr = ComprobanteBLL.GetByPK(obj);
                lst.Add(hr);
            }
            else
            {


                WhereParams parametros = new WhereParams();
                List<object> valores = new List<object>();

                parametros.where = "com_empresa=" + jobj.empresa + " and com_tipodoc=" + Constantes.cHojaRuta.tpd_codigo;
                if ((almacen ?? 0) > 0)
                {
                    parametros.where += " and com_almacen={" + valores.Count + "}";
                    valores.Add(almacen.Value);
                }
                if ((pventa ?? 0) > 0)
                {
                    parametros.where += " and com_pventa={" + valores.Count + "}";
                    valores.Add(pventa.Value);
                }
                if ((numero ?? 0) > 0)
                {
                    parametros.where += " and com_numero={" + valores.Count + "}";
                    valores.Add(numero.Value);
                }
                if ((desde ?? new DateTime()) > new DateTime())
                {
                    parametros.where += " and com_fecha>={" + valores.Count + "}";
                    valores.Add(desde.Value);
                }

                if ((hasta ?? new DateTime()) > new DateTime())
                {
                    parametros.where += " and com_fecha<={" + valores.Count + "}";
                    valores.Add(hasta.Value);
                }

                if (estados != null)
                {
                    string whereestados = "";
                    foreach (object item in estados)
                    {
                        if (item.ToString() != "")
                        {
                            whereestados += ((whereestados != "") ? " or " : "") + " com_estado = " + item.ToString();
                        }
                    }
                    parametros.where += (whereestados != "") ? " and (" + whereestados + ")" : "";
                }




                if (!string.IsNullOrEmpty(socio))
                {
                    string[] arraynombres = socio.ToString().Split(' ');
                    for (int i = 0; i < arraynombres.Length; i++)
                    {
                        parametros.where += " and (per_ciruc ILIKE {" + valores.Count + "} or per_nombres ILIKE {" + valores.Count + "} or per_apellidos ILIKE{" + valores.Count + "}) ";
                        valores.Add("%" + arraynombres[i] + "%");

                    }

                }

                parametros.valores = valores.ToArray();
                lst = ComprobanteBLL.GetAll(parametros, "com_fecha DESC");
            }
            //List<vListado> lst = General.GetComprobantesGeneral(Conversiones.ObjectToDateTimeNull(desde), Conversiones.ObjectToDateTimeNull(hasta), jobj.empresa, Conversiones.ObjectToIntNull(almacen), Conversiones.ObjectToIntNull(pventa), Conversiones.ObjectToIntNull(bodega), (object[])tipo, (object[])estado, (string)numero, (string)persona, (string)detalle, (string)placa, (object[])usuario, true, usr.usr_id);





            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                foreach (Comprobante item in lst)
                {
                    if ((item.com_estado == 0 || item.com_estado == 1) && estado == 2) //CIERRA
                    {
                        item.com_empresa_key = item.com_empresa;
                        item.com_codigo_key = item.com_codigo;
                        item.mod_fecha = DateTime.Now;
                        item.mod_usr = obj.mod_usr;
                        item.com_estado = estado.Value;
                        ComprobanteBLL.Update(transaction, item);
                    }
                    if ((item.com_estado == 2) && estado == 1) //ABRE
                    {
                        item.com_empresa_key = item.com_empresa;
                        item.com_codigo_key = item.com_codigo;
                        item.mod_fecha = DateTime.Now;
                        item.mod_usr = obj.mod_usr;
                        item.com_estado = estado.Value;
                        ComprobanteBLL.Update(transaction, item);
                    }



                }
                
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return "-1";
            }



            return new JavaScriptSerializer().Serialize(lst);
        }






        #endregion

        #region Punto Venta

        [WebMethod]
        public string GetPuntoVenta(object objeto)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object id = null;
            object empresa = null;
            object almacen = null;
            object usuario = null;
            object empty = null;
            tmp.TryGetValue("id", out id);
            tmp.TryGetValue("empresa", out empresa);
            tmp.TryGetValue("almacen", out almacen);
            tmp.TryGetValue("usuario", out usuario);
            tmp.TryGetValue("empty", out empty);

            bool withempty = false;
            if (empty != null)
                bool.TryParse(empty.ToString(), out withempty);

            Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = int.Parse(empresa.ToString()), uxe_empresa_key = int.Parse(empresa.ToString()), uxe_usuario = usuario.ToString(), uxe_usuario_key = usuario.ToString() });
            return Document.LabelIconSelect(id.ToString(), uxe.uxe_puntoventa.ToString(), Dictionaries.GetPuntoVenta(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())), "Punto Venta", "fa-cube", "", "", "col-md-8", withempty, false, false, false).ToString();

        }


        [WebMethod]
        public string GetPuntosAlmacen(object objeto)
        {
            Puntoventa pve = new Puntoventa(objeto);

            List<Puntoventa> lst = PuntoventaBLL.GetAll(new WhereParams("pve_empresa={0} and pve_almacen={1}", pve.pve_empresa, pve.pve_almacen), "pve_id");
            StringBuilder html = new StringBuilder();

            html.AppendFormat("<option></option>");
            foreach (Puntoventa item in lst)
            {
                html.AppendFormat("<option value='{0}' {2}>{1}</option>", item.pve_secuencia, item.pve_id + " " + item.pve_nombre, item.pve_secuencia == pve.pve_secuencia ? " selected " : "");
            }
            return html.ToString();



        }






        #endregion

        #region Vehiculos
        [WebMethod]
        public string GetVehiculosSocio(object objeto)
        {
            Vehiculo veh = new Vehiculo(objeto);

            List<Vehiculo> lst = VehiculoBLL.GetAll(new WhereParams("veh_empresa={0} and veh_duenio={1}", veh.veh_empresa, veh.veh_duenio), "");
            StringBuilder html = new StringBuilder();

            html.AppendFormat("<option></option>");
            foreach (Vehiculo item in lst)
            {
                html.AppendFormat("<option value='{0}' {1}>{2}</option>", item.veh_codigo, item.veh_codigo == veh.veh_codigo ? " selected " : "",  "Placa:" + item.veh_placa + " Disco:" + item.veh_disco);
            }
            return html.ToString();



        }

        #endregion

        #region Cobros

        [WebMethod]
        public string GetFormCobros(object objeto)
        {

            Comprobante obj = new Comprobante(objeto);

            Usuario usr = UsuarioBLL.GetByPK(new Usuario { usr_id = obj.crea_usr, usr_id_key = obj.crea_usr });

            



            StringBuilder html = new StringBuilder();
            //html.AppendLine("<form role = \"form\" action = \"#\" >");
            //INICIO PRIMER FORM GRUOUP

            html.AppendLine("<div class=\"row form-group\">");
            html.Append(Document.LabelIconDateRange("txtfecha", DateTime.Now.AddDays(-10), DateTime.Now, "Fecha", "fa-calendar", "", "", "col-md-4", false, false).ToString());
            html.Append(Document.LabelIconSelect("cmbalmacen_f", "", Dictionaries.GetIDAlmacen(), "Almacen", "fa-building", "", "", "col-md-4", true, false, false, false).ToString());
            html.Append(Document.LabelIconInput("txthoja_f", "", "Hoja Ruta", "fa-user", "", "", "col-md-4", ElementEnums.InputType.text).ToString());
            
            
            html.AppendLine("</div>");

            html.AppendLine("<div class=\"row form-group\">");            
            html.Append(Document.LabelIconSelect("cmbsocio_f", usr.usr_persona.HasValue?usr.usr_persona.ToString():"", Dictionaries.GetSocios(), "Socio", "fa-user", "", "", "col-md-4", true,false, usr.usr_persona.HasValue, false).ToString());
            html.Append(Document.LabelIconSelect("cmbpventa_f", "", Dictionaries.Empty(), "Punto venta", "fa-laptop", "", "", "col-md-4", true, false, false, false).ToString());
            html.Append(Document.LabelIconInput("txtdocumento_f", "", "Documento", "fa-file", "", "", "col-md-4", ElementEnums.InputType.text).ToString());

            
            
            html.AppendLine("</div>");


            
            html.AppendLine("<div class=\"row form-group\">");
            html.Append(Document.LabelIconInput("txtpersona_f", "", "Cliente", "fa-user", "", "", "col-md-4", ElementEnums.InputType.text).ToString());
            html.Append(Document.LabelIconInput("txtnumero_f", "", "Numero", "fa-barcode", "", "", "col-md-4", ElementEnums.InputType.text).ToString());
            html.Append(Document.LabelIconSelect("cmbestado_f", "", Dictionaries.GetEstadoCobro(), "Estado", "fa-tag", "", "", "col-md-4", true, false, false, false).ToString());
            
            //if (Constantes.ListadoLock(usr.usr_id))//Si tiene bloqueo solo puede ver los comprobantes creados por su usuario
            //    html.Append(Document.LabelIconSelect("cmbusuario_f", usr.usr_id, Dictionaries.GetUsuario(), "Usuario", "fa-users", "", "", "col-md-4", false, true, true, false).ToString());
            //else
            //    html.Append(Document.LabelIconSelect("cmbusuario_f", "", Dictionaries.GetUsuario(), "Usuario", "fa-users", "", "", "col-md-4", false, true, false, false).ToString());
            html.AppendLine("</div>");            
            html.AppendLine("<button class=\"btn blue btn-block margin-top-20\" id='btnsearch'>BUSCAR<i class=\"m-icon-swapright m-icon-white\"></i></button>");


            html.AppendLine("<table class='table table-bordered table-bordered table-hover' id='tdlistado'>");
            html.AppendLine("<thead>");
            html.AppendLine("<tr>");
            html.AppendLine("<th></th>");
            html.AppendLine("<th>Fecha</th>");
            html.AppendLine("<th>Factura</th>");
            html.AppendLine("<th>Cliente</th>");
            html.AppendLine("<th>Subtotal</th>");
            html.AppendLine("<th>IVA</th>");
            html.AppendLine("<th>TOTAL</th>");
            html.AppendLine("<th>Ret IVA</th>");
            html.AppendLine("<th>Ret RENTA</th>");
            html.AppendLine("<th>CANCELADO</th>");
            html.AppendLine("<th>SALDO</th>");
            html.AppendLine("</tr>");
            html.AppendLine("</thead>");
            html.AppendLine("<tbody></tbody>");
            html.AppendLine("</table>");
            return html.ToString();
        }

        [WebMethod]
        public string GetDataCobros(object objeto)
        {
            JsonObj obj = new JsonObj(objeto);
            Usuario usr = UsuarioBLL.GetByPK(new Usuario { usr_id = obj.crea_usr, usr_id_key = obj.crea_usr });

            vComprobante vcom = new vComprobante(objeto);
            vcom.tipos = new object[1] { 4 };
            DateTime? desde = (DateTime?)Dictionaries.GetObject(objeto, "desde", typeof(DateTime?));
            DateTime? hasta = (DateTime?)Dictionaries.GetObject(objeto, "hasta", typeof(DateTime?));
            int? estadocobro = (int?)Dictionaries.GetObject(objeto, "estadocobro", typeof(int?));
            List<vComprobante> lista = Packages.General.GetComprobantesRet(vcom, usr, desde,hasta,estadocobro, 500, 0);

            

            StringBuilder html = new StringBuilder();

            foreach (vComprobante item in lista)
            {
                if (!string.IsNullOrEmpty(item.doctran))
                {
                    decimal saldo = (item.total??0) - (item.cancela??0);

                    html.AppendFormat("<tr data-codigo='{0}' class='{1}'>", item.codigo, saldo > 0 ? "pendiente" : "");
                    html.Append("<td>");
                    if (saldo>0)
                        html.Append(Document.CircleSmallButton("btnpay", "", "Cobrar", "fa-usd", "blue", "Cobrar("+item.codigo+");"));
                    html.Append(Document.CircleSmallButton("btninf", "", "Información", "fa-info-circle", "green", "Detalle(" + item.codigo + ");"));
                    //html.Append(Document.DropdownButtons("cmbopc", "", "fa-angle-down", Dictionaries.GetOpcionesByTipoDoc(item.tipodoc.Value, item.estado.Value, usr.usr_perfil), "", "blue btn-sm", "opciones").ToString());
                    html.Append("</td>");
                    html.AppendFormat("<td><div class='textolistado'>{0}</div><div class='textolistadomin'>{1} {2}<div></td>", item.fecha, item.crea_usr, item.crea_fecha);
                    html.AppendFormat("<td><div class='textolistado'><a href = '#' onclick = 'CallFormulario({2});' >{0}</a></div><div class='textolistadomin'>{1}<div><div class='textolistadomin'>{2}</div></td>", item.doctran, Constantes.GetEstadoName(item.estado.Value), item.codigo);                    
                    html.AppendFormat("<td><div class='textolistado'>{0}</div><div class='textolistadomin'>{1}<div></td>", item.nombres, item.ciruc);                    
                    html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.subtotal));
                    html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.impuesto));
                    html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.total));
                    html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.retiva));
                    html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.retren));
                    html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.cancela));
                    html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(saldo));
                    html.AppendLine("</tr>");
                }

            }

            return html.ToString();
        }


        [WebMethod]
        public string EditDetalleCobro(object objeto)
        {
            Comprobante fac = new Comprobante(objeto);
            fac = General.GetComprobante(fac.com_empresa, fac.com_codigo);


            decimal? subtotal = fac.total.tot_subtotal + fac.total.tot_subtot_0;
            decimal? iva = fac.total.tot_timpuesto;
            decimal? ice = fac.total.tot_ice;
            decimal? total = fac.total.tot_total;

            decimal? entregado = fac.cancelaciones.Sum(s => s.dca_monto ?? 0);


            List<TipoPagoCob> lst = new JavaScriptSerializer().Deserialize<List<TipoPagoCob>>(Constantes.GetParameter("tipospago"));
            

            
            StringBuilder html = new StringBuilder();

            html.Append("<div id='editpago' class='row form-group' data-comprobante='" + fac.com_codigo + "'>");

            //html.Append("<div class='contado col-md-3' style='margin-top:10px;'>");
            html.Append("<div class='contado col-md-3'>");




            html.Append(Document.LabelIconInput("txtSUBTOTAL_P", Formatos.CurrencyFormat(subtotal), "SUBTOTAL", "fa-dollar", "", "right", "col-md-12", true, false, ElementEnums.InputType.text));
            html.Append(Document.LabelIconInput("txtICE_P", Formatos.CurrencyFormat(ice), "ICE", "fa-dollar", "", "right", "col-md-12", true, false, ElementEnums.InputType.text));
            html.Append(Document.LabelIconInput("txtIVA_P", Formatos.CurrencyFormat(iva), "IVA", "fa-dollar", "", "right", "col-md-12", true, false, ElementEnums.InputType.text));
            html.Append(Document.LabelIconInput("txtTOTAL_P", Formatos.CurrencyFormat(total), "TOTAL", "fa-dollar", "", "total right", "col-md-12", true, false, ElementEnums.InputType.text));
            html.Append(Document.LabelIconInput("txtPAGADO_P", Formatos.CurrencyFormat(entregado), "PAGADO", "fa-dollar", "", "right", "col-md-12", true, false, ElementEnums.InputType.text));

            html.Append(Document.LabelIconInput("txtPENDIENTE_P", Formatos.CurrencyFormat(0), "Pendiente", "fa-dollar", "", "right", "col-md-12", true, false, ElementEnums.InputType.text));
            html.Append(Document.LabelIconInput("txtENTREGADO_P", Formatos.CurrencyFormat(0), "Entregado", "fa-dollar", "", "right", "col-md-12", true, false, ElementEnums.InputType.text));
            //html.Append(Document.LabelIconInput("txtOBSERVACION_P", "", "Observación", "fa-tag", "", "", "col-md-12", false, false, ElementEnums.InputType.text));
            html.Append(Document.LabelIconInput("txtCAMBIO_P", Formatos.CurrencyFormat(0), "Cambio", "fa-dollar", "", "right", "col-md-12", true, false, ElementEnums.InputType.text));
            html.Append("</div>");

            //html.Append("<div class='contado col-md-9' style='margin-top:10px;'>");
            html.Append("<div class='contado col-md-9'>");
            html.Append("<table id='tdpago'  class='table table-bordered table-stripped table-condensed'>");
            html.Append("<thead>");
            html.Append("<tr>");
            html.Append("<th class=''></th>");
            html.Append("<th class='col-md-3'>Tipo</th>");
            html.Append("<th class='col-md-3'>Datos</th>");
            html.Append("<th class='col-md-3'>Nro</th>");
            html.Append("<th class='col-md-3'>Valor</th>");
            html.Append("</tr>");
            html.Append("</thead>");
            html.Append("<tbody>");

            foreach (TipoPagoCob item in lst)
            {
                html.Append("<tr data-id='" + item.id + "' data-nombre='" + item.nombre + "'>");
                //ADD
                if (item.add)
                    html.AppendFormat("<td class='addtipo'>{0}</td>", "<i class='fa fa-plus-circle'></i>");
                else
                    html.AppendFormat("<td>{0}</td>", "");

                //NOMBRe
                html.AppendFormat("<td class='{1}'>{0} {2}</td>", item.nombre,  item.porcentajes==null? "seltipo" : "", item.porcentajes==null ? "<i class='fa fa-arrow-circle-right'></i>" : "");


                //DATOS
                if (item.emisor)
                    html.AppendFormat("<td>{0}</td>", Document.Input("", "", "placeholder='EMISOR'", "emisor", false, false, ElementEnums.InputType.text));
                else if (item.banco)
                    html.AppendFormat("<td>{0}</td>", Document.Select("", "", Dictionaries.GetBancos(), "", "banco", true, false, false, false));
                //html.AppendFormat("<td>{0}</td>", Document.Input("", "", "placeholder='BANCO'", "banco", false, false, ElementEnums.InputType.text));
                else if (item.porcentajes != null)
                    html.AppendFormat("<td>{0}</td>", Document.Select("", "", item.porcentajes.ToDictionary(x => x.porcentaje.ToString(), x => x.porcentaje.ToString()), "", " porcentaje " + item.tiporet, true, false, false, false));
                //html.AppendFormat("<td>{0}</td>", Document.Select("", "", Dictionaries.GetPorcentajesRetencion(item.tiporet), "", " porcentaje " + item.tiporet, true, false, false, false));
                else
                    html.AppendFormat("<td>{0}</td>", "");
                //NRO
                if (item.nro)
                    html.AppendFormat("<td>{0}</td>", Document.Input("", "", "placeholder='NRO'", "nro", false, false, ElementEnums.InputType.text));
                else
                    html.AppendFormat("<td>{0}</td>", "");
                //VALOR
                if (item.porcentajes!=null)
                    html.AppendFormat("<td>{0}</td>", Document.Input("", "", "", "cobro valor" + item.tiporet, true, false, ElementEnums.InputType.text));
                else
                    html.AppendFormat("<td>{0}</td>", Document.Input("", "", "", "cobro ", false, false, ElementEnums.InputType.number));
                html.Append("</tr>");
            }


            html.Append("</tbody>");
            html.Append("</table>");
            html.Append("</div>");

            html.Append("</div>");


            object[] retorno = new object[2];
            retorno[0] = "COBRO FACTURA " + fac.com_doctran;
            retorno[1] = html.ToString();
            return new JavaScriptSerializer().Serialize(retorno);



        }

        [WebMethod]
        public string SaveCobro(object objeto)
        {

            JsonObj jobj = new JsonObj(objeto);
            List<TipoPagoCob> lst = new JavaScriptSerializer().Deserialize<List<TipoPagoCob>>(Constantes.GetParameter("tipospago"));
            List<Tipopago> lstipos = TipopagoBLL.GetAll("tpa_empresa=" + jobj.empresa, "");
            Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = jobj.empresa, uxe_empresa_key = jobj.empresa, uxe_usuario = jobj.crea_usr, uxe_usuario_key = jobj.crea_usr });

            object objetocomp = Dictionaries.GetObject(objeto, "comprobante", typeof(object));
            object objetodetalle = Dictionaries.GetObject(objeto, "detalle", typeof(object));

            List<Drecibo> recibos = new List<Drecibo>();


            List<Detallerec> detalle = new List<Detallerec>();
            if (objetodetalle != null)
            {
                foreach (Object item in (Array)objetodetalle)
                {
                    if (item != null)
                    {
                        Detallerec dre = new Detallerec(item);
                        if ((dre.dre_valor ?? 0) > 0)
                        {

                            TipoPagoCob tcob = lst.Find(delegate (TipoPagoCob t) { return t.id == dre.dre_id; });

                            Tipopago tpa = new Tipopago();
                            if (tcob.codigo.HasValue)
                                tpa = lstipos.Find(delegate (Tipopago t) { return t.tpa_codigo == tcob.codigo; });
                            if (tcob.porcentajes!=null)
                            {
                                foreach (PorcentajeRetencion porcret in tcob.porcentajes)
                                {
                                    if (porcret.porcentaje == dre.dre_porcentaje)
                                    {
                                        tpa = lstipos.Find(delegate (Tipopago t) { return t.tpa_codigo == porcret.codigo; });
                                        break;
                                    }
                                }
                            }


                            Drecibo dfp = new Drecibo();
                            dfp.dfp_empresa = dre.dre_empresa;
                            dfp.dfp_tipopago = tpa.tpa_codigo;
                            dfp.dfp_tipopagoid = tpa.tpa_id;
                            dfp.dfp_tipopagonombre = tpa.tpa_nombre;
                            dfp.dfp_monto = dre.dre_valor.Value;
                            dfp.dfp_nro_documento = dre.dre_nro;
                            dfp.dfp_emisor = dre.dre_emisor;
                            dfp.dfp_banco = dre.dre_banco;
                            dfp.dfp_nro_cheque = dre.dre_nro;
                            dfp.dfp_debcre = Constantes.cDebito;

                            dfp.crea_usr = jobj.crea_usr;
                            dfp.crea_fecha = DateTime.Now;


                            recibos.Add(dfp);
                        }
                    }
                }
            }

                     



            Comprobante fac = new Comprobante(objetocomp);
            fac = General.GetComprobante(fac.com_empresa, fac.com_codigo);
            fac.crea_usr = jobj.crea_usr;
            Comprobante can = FAC.save_cancelacion_factura(fac, recibos, DateTime.Now);
            FAC.account_recibo(can);

            fac = General.GetComprobante(fac.com_empresa, fac.com_codigo);
            decimal? subtotal = fac.total.tot_subtotal + fac.total.tot_subtot_0;
            decimal? iva = fac.total.tot_timpuesto;
            decimal? ice = fac.total.tot_ice;
            decimal? total = fac.total.tot_total;
            decimal? entregado = fac.cancelaciones.Sum(s => s.dca_monto ?? 0);
            decimal saldo = (total ?? 0) - (entregado ?? 0);
            decimal retiva = fac.recibos.Where(w => (w.dfp_tipopagoiva ?? 0) == 1).Sum(s => s.dfp_monto);
            decimal retren = fac.recibos.Where(w => (w.dfp_tipopagoret ?? 0) == 1).Sum(s => s.dfp_monto);
            StringBuilder html = new StringBuilder();
                        
            html.AppendFormat("<tr data-codigo='{0}' class='{1}'>", fac.com_codigo, saldo > 0 ? "pendiente" : "");
            html.Append("<td>");
            if (saldo > 0)
                html.Append(Document.CircleSmallButton("btnpay", "", "Cobrar", "fa-usd", "blue", "Cobrar(" + fac.com_codigo + ");"));
            html.Append(Document.CircleSmallButton("btninf", "", "Información", "fa-info-circle", "green", "Detalle(" + fac.com_codigo  + ");"));
            //html.Append(Document.DropdownButtons("cmbopc", "", "fa-angle-down", Dictionaries.GetOpcionesByTipoDoc(item.tipodoc.Value, item.estado.Value, usr.usr_perfil), "", "blue btn-sm", "opciones").ToString());
            html.Append("</td>");
            html.AppendFormat("<td><div class='textolistado'>{0}</div><div class='textolistadomin'>{1} {2}<div></td>", fac.com_fecha, fac.crea_usr, fac.crea_fecha);
            html.AppendFormat("<td><div class='textolistado'><a href = '#' onclick = 'CallFormulario({2});' >{0}</a></div><div class='textolistadomin'>{1}<div><div class='textolistadomin'>{2}</div></td>", fac.com_doctran, Constantes.GetEstadoName(fac.com_estado), fac.com_codigo);
            html.AppendFormat("<td><div class='textolistado'>{0}</div><div class='textolistadomin'>{1}<div></td>", fac.ccomdoc.cdoc_nombre, fac.ccomdoc.cdoc_ced_ruc);
            html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(subtotal));
            html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(iva));
            html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(total));
            html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(retiva));
            html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(retren));
            html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(entregado));
            html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(saldo));
            html.AppendLine("</tr>");



            object[] retorno = new object[2];
            retorno[0] = html.ToString();
            retorno[1] = fac.com_codigo.ToString();



            return new JavaScriptSerializer().Serialize(retorno);
        }

        [WebMethod]
        public string GetDetalleCobros(object objeto)
        {
            Comprobante fac = new Comprobante(objeto);
            fac = General.GetComprobante(fac.com_empresa, fac.com_codigo);


            decimal? entregado = fac.cancelaciones.Sum(s => s.dca_monto ?? 0);

            StringBuilder html = new StringBuilder();

            html.Append("<div id='editpago' class='row form-group' data-comprobante='" + fac.com_codigo + "'>");

            html.Append("<div class='contado col-md-12'>");

            html.Append(Document.LabelIconInput("txtCOMPROBANTE_P", fac.com_doctran, "FACTURA", "fa-file-invoice-dollar", "", "", "col-md-8", true, false, ElementEnums.InputType.text));
            html.Append(Document.LabelIconInput("txtTOTAL_P", Formatos.CurrencyFormat(fac.total.tot_total), "TOTAL", "fa-dollar", "", "right", "col-md-4", true, false, ElementEnums.InputType.text));

            html.Append(Document.LabelIconInput("txtCLIENTE_P", fac.ccomdoc.cdoc_nombre, "CLIENTE", "fa-user", "", "", "col-md-8", true, false, ElementEnums.InputType.text));
            html.Append(Document.LabelIconInput("txtSALDO_P", Formatos.CurrencyFormat(fac.total.tot_total - entregado), "SALDO", "fa-dollar", "", "right", "col-md-4", true, false, ElementEnums.InputType.text));

            html.Append("</div>");

            html.Append("<div class='contado col-md-12' style='margin-top:10px;'>");

            html.Append("<table id='tdcobros'  class='table table-bordered table-striped table-condensed'>");
            html.Append("<thead>");
            html.Append("<tr>");
            //html.Append("<th class='col-md-2'>Nro</th>");
            //html.Append("<th class='col-md-2'>Fecha</th>");
            html.Append("<th class='col-md-4'>Cancelación</th>");
            html.Append("<th class='col-md-4'>Tipo pago</th>");
            html.Append("<th class='col-md-2'>Nro</th>");
            html.Append("<th class='col-md-1'>Valor</th>");
            html.Append("<th class='col-md-1'>Total</th>");
            html.Append("</tr>");
            html.Append("</thead>");
            html.Append("<tbody>");


            foreach (Dcancelacion item in fac.cancelaciones)
            {
                html.Append("<tr>");

                //html.AppendFormat("<td>{0}</td>", item.can_secuencia);


                List<Drecibo> recibos = fac.recibos.FindAll(delegate (Drecibo d) { return d.dfp_comprobante == item.dca_comprobante_can; });


                //html.AppendFormat("<td>{0:dd/MM/yyyy HH:mm}</td>", item.can_comprobantefecha);
                html.AppendFormat("<td rowspan='" + recibos.Count + "'><div class='textolistado'>{0}</div><div class='textolistadomin'>{1:dd/MM/yyyy HH:mm}<div></td>", item.dca_compcandoctran , item.dca_comprobantecanfecha);

                int r = 0;

                foreach (Drecibo dre in recibos)
                {
                    if (r > 0)
                        html.Append("<tr>");


                    string datos = "";
                    if (!string.IsNullOrEmpty(dre.dfp_emisor))
                        datos = dre.dfp_emisor;
                    else if ((dre.dfp_banco??0)>0)
                        datos = dre.dfp_banco.ToString();
                    //else if ((dre.dre_porcentaje ?? 0) > 0)
                     //   datos = "%" + dre.dre_porcentaje;

                    html.AppendFormat("<td> <div class='textolistadomin'>{0} {1}</div></td>", dre.dfp_tipopagonombre, datos);

                    if (!string.IsNullOrEmpty(dre.dfp_nro_documento))
                        html.AppendFormat("<td><div class='textolistadomin'>{0}</div></td>", dre.dfp_nro_documento);
                    else if (!string.IsNullOrEmpty(dre.dfp_nro_cheque))
                        html.AppendFormat("<td><div class='textolistadomin'>{0}</div></td>", dre.dfp_nro_cheque);
                    else
                        html.AppendFormat("<td>{0}</td>", "");

                    html.AppendFormat("<td class='right'><div class='textolistadomin'>{0}</div></td>", dre.dfp_monto);



                    if (r == 0)
                    {
                        html.AppendFormat("<td rowspan='" + recibos.Count + "' class='right'><div class='textolistado'>${0}</div>{1}</td>", Formatos.CurrencyFormat(item.dca_monto), recibos.Sum(s => s.dfp_monto) > item.dca_monto? "<div class='textolistadomin'>Cambio:" + Formatos.CurrencyFormat(recibos.Sum(s => s.dfp_monto) - item.dca_monto) + "<div>" : "");
                    }
                    r++;
                    html.Append("</tr>");

                }








                //html.AppendFormat("<td>");
                //html.Append("<table class='table table-bordered table-stripped table-condensed'>");







                // html.Append("</tr>");
            }


            html.Append("</tbody>");
            html.Append("</table>");
            html.Append("</div>");

            html.Append("</div>");


            object[] retorno = new object[2];
            retorno[0] = "DETALLE COBROS";
            retorno[1] = html.ToString();
            return new JavaScriptSerializer().Serialize(retorno);



        }

        #endregion

        #region Cobros Socios

        [WebMethod]
        public string GetFormCobrosSocios(object objeto)
        {

            Comprobante obj = new Comprobante(objeto);
            Usuario usr = UsuarioBLL.GetByPK(new Usuario { usr_id = obj.crea_usr, usr_id_key = obj.crea_usr });
            if (obj.com_codigo>0)
            {
                obj.com_empresa_key = obj.com_empresa;
                obj.com_codigo_key = obj.com_codigo;
                obj = ComprobanteBLL.GetByPK(obj);
            }




            

            StringBuilder html = new StringBuilder();
            //html.AppendLine("<form role = \"form\" action = \"#\" >");
            //INICIO PRIMER FORM GRUOUP

            html.AppendLine("<div class=\"row form-group\">");
            html.Append(Document.LabelIconDateRange("txtfecha", DateTime.Now.AddDays(-10), DateTime.Now, "Fecha", "fa-calendar", "", "", "col-md-4", false, false).ToString());
            html.Append(Document.LabelIconSelect("cmbalmacen_f", "", Dictionaries.GetIDAlmacen(), "Almacen", "fa-building", "", "", "col-md-4", true, false, false, false).ToString());
            html.Append(Document.LabelIconInput("txthoja_f", obj.com_codigo>0?obj.com_codigo.ToString(): "", "Hoja Ruta", "fa-user", "", "", "col-md-4", ElementEnums.InputType.text).ToString());
            html.AppendLine("</div>");

            html.AppendLine("<div class=\"row form-group\">");
            html.Append(Document.LabelIconSelect("cmbsocio_f", usr.usr_persona.HasValue ? usr.usr_persona.ToString() : "", Dictionaries.GetSocios(), "Socio", "fa-user", "", "", "col-md-4", true, false, usr.usr_persona.HasValue, false).ToString());
            html.Append(Document.LabelIconSelect("cmbpventa_f", "", Dictionaries.Empty(), "Punto venta", "fa-laptop", "", "", "col-md-4", true, false, false, false).ToString());
            html.Append(Document.LabelIconSelect("cmbestado_f", "", Dictionaries.GetEstadoCobro(), "Estado", "fa-tag", "", "", "col-md-4", true, false, false, false).ToString());            
            html.AppendLine("</div>");



            html.AppendLine("<div class=\"row form-group\">");
            html.Append(Document.LabelIconInput("txtpersona_f", "", "Cliente", "fa-user", "", "", "col-md-4", ElementEnums.InputType.text).ToString());
            html.Append(Document.LabelIconInput("txtnumero_f", "", "Numero", "fa-barcode", "", "", "col-md-4", ElementEnums.InputType.text).ToString());
            html.Append(Document.LabelIconSelect("cmbagrupado_f", "", Dictionaries.GetAgrupadoPor(), "Agrupado por", "fa-cog", "", "", "col-md-4", true, false, false, false).ToString());
            
            html.AppendLine("</div>");
            html.AppendLine("<button class=\"btn blue btn-block margin-top-20\" id='btnsearch'>BUSCAR<i class=\"m-icon-swapright m-icon-white\"></i></button>");


            html.AppendLine("<table class='table table-bordered table-bordered table-hover' id='tdlistado'>");
            html.AppendLine("<thead>");          
            html.AppendLine("</thead>");
            html.AppendLine("<tbody></tbody>");
            html.AppendLine("</table>");
            return html.ToString();
        }

        [WebMethod]
        public string GetDataCobrosSocios(object objeto)
        {
            JsonObj obj = new JsonObj(objeto);
            Usuario usr = UsuarioBLL.GetByPK(new Usuario { usr_id = obj.crea_usr, usr_id_key = obj.crea_usr });

            vComprobante vcom = new vComprobante(objeto);
            
            DateTime? desde = (DateTime?)Dictionaries.GetObject(objeto, "desde", typeof(DateTime?));
            DateTime? hasta = (DateTime?)Dictionaries.GetObject(objeto, "hasta", typeof(DateTime?));
            int? estadocobro = (int?)Dictionaries.GetObject(objeto, "estadocobro", typeof(int?));
            long? hojaruta = (long?)Dictionaries.GetObject(objeto, "hojaruta", typeof(long?));
            string agrupado = (string)Dictionaries.GetObject(objeto, "agrupado", typeof(string));

            if (agrupado == "HR")
                vcom.tipos = new object[1] { 5 }; // Hoja Ruta
            else
                vcom.tipos = new object[2] { 4, 13 };//Facturas / Guias

            List<vComprobante> lista = Packages.General.GetComprobantesCobroSocio(vcom, usr, desde, hasta, hojaruta, estadocobro, 500, 0);

            StringBuilder htmlhead = new StringBuilder();            
            htmlhead.AppendLine("<tr>");
            if (agrupado == "HR")
            {
                htmlhead.AppendLine("<th></th>");
                htmlhead.AppendLine("<th>Fecha</th>");
                htmlhead.AppendLine("<th>Hoja Ruta</th>");
                htmlhead.AppendLine("<th>Datos</th>");                
                htmlhead.AppendLine("<th>Subtotal</th>");
                htmlhead.AppendLine("<th>IVA</th>");
                htmlhead.AppendLine("<th>TOTAL</th>");
                htmlhead.AppendLine("<th>CANCELADO</th>");
                htmlhead.AppendLine("<th>SALDO</th>");
                htmlhead.AppendLine("</tr>");
            }
            else
            {
                htmlhead.AppendLine("<th></th>");
                htmlhead.AppendLine("<th>Fecha</th>");
                htmlhead.AppendLine("<th>Comprobante</th>");
                htmlhead.AppendLine("<thCliente</th>");
                htmlhead.AppendLine("<th>Hoja Ruta</th>");
                htmlhead.AppendLine("<th>Subtotal</th>");
                htmlhead.AppendLine("<th>IVA</th>");
                htmlhead.AppendLine("<th>TOTAL</th>");
                htmlhead.AppendLine("<th>CANCELADO</th>");
                htmlhead.AppendLine("<th>SALDO</th>");
                htmlhead.AppendLine("</tr>");
            }
            StringBuilder html = new StringBuilder();

            

            foreach (vComprobante item in lista)
            {
                if (!string.IsNullOrEmpty(item.doctran))
                {
                    decimal saldo = (item.total ?? 0) - (item.cancela ?? 0);
                    if (agrupado == "HR")
                    {

                        html.AppendFormat("<tr data-codigo='{0}' class='{1}'>", item.codigo, saldo > 0 ? "pendiente" : "");
                        html.Append("<td>");
                        //if (saldo > 0)
                        //    html.Append(Document.CircleSmallButton("btnpay", "", "Cobrar", "fa-usd", "blue", "Cobrar(" + item.codigo + ");"));
                        html.Append(Document.CircleSmallButton("btninf", "", "Información", "fa-info-circle", "green", "Detalle(" + item.codigo + ");"));                        
                        html.Append("</td>");
                        html.AppendFormat("<td><div class='textolistado'>{0}</div><div class='textolistadomin'>{1} {2}<div></td>", item.fecha, item.crea_usr, item.crea_fecha);
                        html.AppendFormat("<td><div class='textolistado'><a href = '#' onclick = 'CallFormulario({2});' >{0}</a></div><div class='textolistadomin'>{1}<div><div class='textolistadomin'>{2}</div></td>", item.doctran, Constantes.GetEstadoName(item.estado.Value), item.codigo);
                        html.AppendFormat("<td><div class='textolistado'>Ruta:{0}</div><div class='textolistadomin'>Disco:{1} Placa:{2}<div></td>", item.nombreruta, item.disco, item.placa);
                        html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.subtotal));
                        html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.impuesto));
                        html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.total));
                        //html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.retiva));
                        //html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.retren));
                        html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.cancela));
                        html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(saldo));
                        html.AppendLine("</tr>");
                    }
                    else
                    {



                       

                        html.AppendFormat("<tr data-codigo='{0}' class='{1}'>", item.codigo, saldo > 0 ? "pendiente" : "");
                        html.Append("<td>");
                        if (saldo > 0)
                            html.Append(Document.CircleSmallButton("btnpay", "", "Cobrar", "fa-usd", "blue", "Cobrar(" + item.codigo + ");"));
                        html.Append(Document.CircleSmallButton("btninf", "", "Información", "fa-info-circle", "green", "Detalle(" + item.codigo + ");"));
                        //html.Append(Document.DropdownButtons("cmbopc", "", "fa-angle-down", Dictionaries.GetOpcionesByTipoDoc(item.tipodoc.Value, item.estado.Value, usr.usr_perfil), "", "blue btn-sm", "opciones").ToString());
                        html.Append("</td>");
                        html.AppendFormat("<td><div class='textolistado'>{0}</div><div class='textolistadomin'>{1} {2}<div></td>", item.fecha, item.crea_usr, item.crea_fecha);
                        html.AppendFormat("<td><div class='textolistado'><a href = '#' onclick = 'CallFormulario({2});' >{0}</a></div><div class='textolistadomin'>{1}<div><div class='textolistadomin'>{2}</div></td>", item.doctran, Constantes.GetEstadoName(item.estado.Value), item.codigo);
                        html.AppendFormat("<td><div class='textolistado'>{0}</div><div class='textolistadomin'>{1}<div></td>", item.nombres, item.ciruc);
                        html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.subtotal));
                        html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.impuesto));
                        html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.total));
                        html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.retiva));
                        html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.retren));
                        html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.cancela));
                        html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(saldo));
                        html.AppendLine("</tr>");
                    }
                }

            }


            object[] retorno = new object[2];
            retorno[0] = htmlhead.ToString();
            retorno[1] = html.ToString();
            return new JavaScriptSerializer().Serialize(retorno);
            
        }




        #endregion

        #region HR Socios

        [WebMethod]
        public string GetFormHRSocios(object objeto)
        {

            Comprobante obj = new Comprobante(objeto);

            Usuario usr = UsuarioBLL.GetByPK(new Usuario { usr_id = obj.crea_usr, usr_id_key = obj.crea_usr });

            StringBuilder html = new StringBuilder();
            //html.AppendLine("<form role = \"form\" action = \"#\" >");
            //INICIO PRIMER FORM GRUOUP

            html.AppendLine("<div class=\"row form-group\">");
            html.Append(Document.LabelIconDateRange("txtfecha", DateTime.Now.AddDays(-10), DateTime.Now, "Fecha", "fa-calendar", "", "", "col-md-4", false, false).ToString());
            html.Append(Document.LabelIconSelect("cmbalmacen_f", "", Dictionaries.GetIDAlmacen(), "Almacen", "fa-building", "", "", "col-md-4", true, false, false, false).ToString());
            html.Append(Document.LabelIconSelect("cmbruta_f", "", Dictionaries.GetRuta(), "Ruta", "fa-search", "", "", "col-md-4", true, false, false, false).ToString());
            html.AppendLine("</div>");

            html.AppendLine("<div class=\"row form-group\">");
            html.Append(Document.LabelIconSelect("cmbsocio_f", usr.usr_persona.HasValue ? usr.usr_persona.ToString() : "", Dictionaries.GetSocios(), "Socio", "fa-user", "", "", "col-md-4", true, false, usr.usr_persona.HasValue, false).ToString());
            html.Append(Document.LabelIconSelect("cmbpventa_f", "", Dictionaries.Empty(), "Punto venta", "fa-laptop", "", "", "col-md-4", true, false, false, false).ToString());
            html.Append(Document.LabelIconSelect("cmbestado_f", "", Dictionaries.GetEstadoCobro(), "Estado", "fa-tag", "", "", "col-md-4", true, false, false, false).ToString());

            html.AppendLine("</div>");



            html.AppendLine("<div class=\"row form-group\">");
            html.Append(Document.LabelIconSelect("cmbvehiculo_f", "", Dictionaries.Empty(), "Vehiculo", "fa-car", "", "", "col-md-4", true, false, false, false).ToString());
            html.Append(Document.LabelIconInput("txtnumero_f", "", "Numero", "fa-barcode", "", "", "col-md-4", ElementEnums.InputType.text).ToString());
            

            html.AppendLine("</div>");
            html.AppendLine("<button class=\"btn blue btn-block margin-top-20\" id='btnsearch'>BUSCAR<i class=\"m-icon-swapright m-icon-white\"></i></button>");


            html.AppendLine("<table class='table table-bordered table-bordered table-hover' id='tdlistado'>");
            html.AppendLine("<thead>");
            html.AppendLine("<th></th>");
            html.AppendLine("<th>Fecha</th>");
            html.AppendLine("<th>Hoja Ruta</th>");
            html.AppendLine("<th>Datos</th>");
            html.AppendLine("<th>Subtotal</th>");
            html.AppendLine("<th>IVA</th>");
            html.AppendLine("<th>TOTAL</th>");
            html.AppendLine("<th>CANCELADO</th>");
            html.AppendLine("<th>CANCELA SOCIO</th>");
            html.AppendLine("<th>SALDO</th>");
            html.AppendLine("</tr>");
            html.AppendLine("</thead>");
            html.AppendLine("<tbody></tbody>");
            html.AppendLine("</table>");
            return html.ToString();
        }

        [WebMethod]
        public string GetDataHRSocios(object objeto)
        {
            JsonObj obj = new JsonObj(objeto);
            Usuario usr = UsuarioBLL.GetByPK(new Usuario { usr_id = obj.crea_usr, usr_id_key = obj.crea_usr });

            vComprobante vcom = new vComprobante(objeto);

            DateTime? desde = (DateTime?)Dictionaries.GetObject(objeto, "desde", typeof(DateTime?));
            DateTime? hasta = (DateTime?)Dictionaries.GetObject(objeto, "hasta", typeof(DateTime?));
            int? estadocobro = (int?)Dictionaries.GetObject(objeto, "estadocobro", typeof(int?));                    
            
            vcom.tipos = new object[1] { 5 }; // Hoja Ruta

            List<vComprobante> lista = Packages.General.GetComprobantesHRSocio(vcom, usr, desde, hasta, estadocobro, 1500, 0);

            StringBuilder html = new StringBuilder();

            foreach (vComprobante item in lista)
            {
                if (!string.IsNullOrEmpty(item.doctran))
                {

                    decimal cancelado = (item.cancela ?? 0) + (item.cancelasocio ?? 0);
                    decimal saldo = (item.total ?? 0) - cancelado;
                    if (saldo < 0)
                        saldo = 0;


                    html.AppendFormat("<tr data-codigo='{0}' class='{1}'>", item.codigo, saldo > 0 ? "pendiente" : "");
                    html.Append("<td>");
                    //if (saldo > 0)
                    //html.Append(Document.CircleSmallButton("btnpay", "", "Cobrar", "fa-usd", "blue", "Cobrar(" + item.codigo + ");"));
                    html.Append(Document.CircleSmallButton("btninf", "", "Detalle Hoja Ruta", "fa-folder-open", "blue", "Detalle(" + item.codigo + ");"));
                    html.Append("</td>");
                    html.AppendFormat("<td><div class='textolistado'>{0}</div><div class='textolistadomin'>{1} {2}<div></td>", item.fecha, item.crea_usr, item.crea_fecha);
                    html.AppendFormat("<td><div class='textolistado'><a href = '#' onclick = 'CallFormulario({2});' >{0}</a></div><div class='textolistadomin'>{1}<div><div class='textolistadomin'>{2}</div></td>", item.doctran, Constantes.GetEstadoName(item.estado.Value), item.codigo);
                    html.AppendFormat("<td><div class='textolistado'>{0}</div><div class='textolistadomin'>Disco:{1} Placa:{2}<div></td>", item.nombreruta, item.disco, item.placa);
                    html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.subtotal));
                    html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.impuesto));
                    html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.total));
                    //html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.retiva));
                    //html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.retren));
                    html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.cancela));
                    html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.cancelasocio));
                    html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(saldo));
                    html.AppendLine("</tr>");
                }

            }

            return html.ToString();


        }




        #endregion

        #region HR Detalle Socios

        [WebMethod]
        public string GetFormHRDetSocios(object objeto)
        {

            Comprobante obj = new Comprobante(objeto);
            Usuario usr = UsuarioBLL.GetByPK(new Usuario { usr_id = obj.crea_usr, usr_id_key = obj.crea_usr });
            if (obj.com_codigo > 0)
            {
                obj.com_empresa_key = obj.com_empresa;
                obj.com_codigo_key = obj.com_codigo;
                obj = ComprobanteBLL.GetByPK(obj);
            }
            

            StringBuilder html = new StringBuilder();
            //html.AppendLine("<form role = \"form\" action = \"#\" >");
            //INICIO PRIMER FORM GRUOUP

            html.AppendLine("<div class=\"row form-group\">");
            html.Append(Document.LabelIconInput("txthojaruta_f",obj.com_doctran.ToString(), "Numero", "fa-barcode", "", "", "col-md-4", ElementEnums.InputType.text).ToString());
            html.Append(Document.LabelIconInput("txtnumero_f", "", "Numero", "fa-barcode", "", "", "col-md-4", ElementEnums.InputType.text).ToString());
            html.Append(Document.LabelIconSelect("cmbestado_f", "", Dictionaries.GetEstadoCobro(), "Estado", "fa-tag", "", "", "col-md-4", true, false, false, false).ToString());
            html.AppendLine("</div>");

     

            html.AppendLine("</div>");
            html.AppendLine("<button class=\"btn blue btn-block margin-top-20\" id='btnsearch'>BUSCAR<i class=\"m-icon-swapright m-icon-white\"></i></button>");


            html.AppendLine("<table class='table table-bordered table-bordered table-hover' id='tdlistado'>");
            html.AppendLine("<thead>");
            html.AppendLine("<th></th>");
            html.AppendLine("<th>Fecha</th>");
            html.AppendLine("<th>Comprobante</th>");
            html.AppendLine("<th>Cliente</th>");
            //html.AppendLine("<th>Datos</th>");
            html.AppendLine("<th>Subtotal</th>");
            html.AppendLine("<th>IVA</th>");
            html.AppendLine("<th>TOTAL<br>(<span class='lbltotal'></span>)</th>");
            html.AppendLine("<th>CANCELADO<br>(<span class='lblcancela'></span>)</th>");
            html.AppendLine("<th>CANCELA SOCIO<br>(<span class='lblcancelasocio'></span>)</th>");
            html.AppendLine("<th>SALDO<br>(<span class='lblsaldo'></span>)</th>");
            html.AppendLine("<th>COBRAR</th>");
            html.AppendLine("</tr>");
            html.AppendLine("</thead>");
            html.AppendLine("<tbody></tbody>");
            html.AppendLine("</table>");
            return html.ToString();
        }

        [WebMethod]
        public string GetDataHRDetSocios(object objeto)
        {

            JsonObj obj = new JsonObj(objeto);
            Usuario usr = UsuarioBLL.GetByPK(new Usuario { usr_id = obj.crea_usr, usr_id_key = obj.crea_usr });
            Comprobante comprobante = new Comprobante(objeto);

            List<vHojadeRuta> lst = General.GetComprobantesHRDetSocio(comprobante.com_empresa, comprobante.com_codigo, comprobante.com_numero, null);


            StringBuilder html = new StringBuilder();
            decimal saldototal = 0;

            foreach (vHojadeRuta item in lst)
            {

                decimal cancelado = (item.cancela ?? 0) + (item.cancelasocio ?? 0);
                decimal saldo = (item.totaldetalle ?? 0) - cancelado;
                if (saldo < 0)
                    saldo = 0;
                saldototal += saldo;

                html.AppendFormat("<tr data-codigo='{0}' data-doctran='{2}' class='{1}'>", item.codigodetalle, saldo > 0 ? "pendiente" : "", item.doctrandetalle);
                html.Append("<td>");
                //if (saldo > 0)
                //html.Append(Document.CircleSmallButton("btnpay", "", "Cobrar", "fa-usd", "blue", "Cobrar(" + item.codigo + ");"));                
                html.Append(Document.CircleSmallButton("btninf", "", "Información", "fa-info-circle", "green", "Detalle(" + item.codigodetalle + ");"));
                html.Append("</td>");
                html.AppendFormat("<td><div class='textolistado'>{0}</div><div class='textolistadomin'>{1} {2}<div></td>", item.fechadetalle, item.usrid, "");
                html.AppendFormat("<td><div class='textolistado'><a href = '#' onclick = 'CallFormulario({2});' >{0}</a></div><div class='textolistadomin'>{1}<div><div class='textolistadomin'>{2}</div></td>", item.doctrandetalle, Constantes.GetEstadoName(1), item.codigodetalle);
                html.AppendFormat("<td><div class='textolistado'>{0} {1}</div><div class='textolistadomin'>{2}</td>", item.nombrecliente, item.apellidocliente, item.ciruccliente);
                //html.AppendFormat("<td><div class='textolistado'>{0}</div><div class='textolistadomin'>Remite:{1}<div></td>", item.nombredestinatario, item.nombreremitente);
                html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.subtotaldetalle + item.subtotal12detalle));
                html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.impuestocabecera));
                html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.totaldetalle));
                //html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.retiva));
                //html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.retren));
                html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.cancela));
                html.AppendFormat("<td><div class='textolistado cancelasocio'>{0}</div></td>", Formatos.CurrencyFormat(item.cancelasocio));
                html.AppendFormat("<td><div class='textolistado saldo'>{0}</div>{1} </td>", Formatos.CurrencyFormat(saldo), saldo>0?new Boton { small = true, id = "btnvsaldo_P", tooltip = "Enviar Saldo ", clase = "fa fa-arrow-right", click = "$(this).closest(\"td\").next().find(\"input\").val(\"" + Formatos.CurrencyFormat(saldo) + "\");" }.ToString():"");
                html.Append("<td class='cobro'>");

                //row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(saldo) + new Boton { small = true, id = "btnvsaldo_P", tooltip = "Enviar Saldo ", clase = "iconsweets-arrowright", click = "(parseFloat($(\"#txtSALDO_P\").val()) >= parseFloat(\"" + Formatos.CurrencyFormat(saldo) + "\")?$(this).closest(\"td\").next().find(\"input\").val(\"" + Formatos.CurrencyFormat(saldo) + "\"):$(this).closest(\"td\").next().find(\"input\").val($(\"#txtSALDO_P\").val()));CalculaAfectacion();" }.ToString(), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(saldo) + "'" });
                //row.cells.Add(new HtmlCell { valor = new Input() { clase = Css.mini + Css.amount, valor = Formatos.CurrencyFormat(saldo) }.ToString(), clase = Css.right, data = "data-valor='" + Formatos.CurrencyFormatAll(saldo) + "'" });

                if (saldo > 0)
                {
                    html.Append("<div style='display:flex; flex-direction:row'>");
                    html.AppendFormat("<div class='btn' class='smallbtn' style='display:flex' id='btnclean' onclick='$(this).parents(\"td\").find(\"input\").val(\"\");' title='Limpiar'><i class='fa fa-eraser'></i></div> ");
                    html.Append(Document.Input("", "", "style='width:80px;display:flex'", "", false, false, ElementEnums.InputType.text));                   
                    html.AppendFormat("<div class='btn' class='smallbtn' style='display:flex' id='btnsave' onclick='SaveCobro({0},\"{1}\",$(this).parents(\"td\").find(\"input\").val());' title='Guardar'><i class='fa fa-save fa-lg'></i></div> ", item.codigodetalle, item.doctrandetalle);
                    html.Append("</div>");
                    //html.Append(new Boton { small = true, id = "btnclean", tooltip = "Limpiar ", clase = "fa fa-eraser", click = "$(this).parents(\"td\").find(\"input\").val(\"\");" }.ToString());
                    //html.Append(new Boton { small = true, id = "btnsave", tooltip = "Cobrar ", clase = "fa fa-save", click = "$(this).parents(\"td\").find(\"input\").val(\"\");" }.ToString());

                }
                html.Append("</td>");
                html.AppendLine("</tr>");


            }

            //return html.ToString();

            object[] retorno = new object[5];            
            retorno[0] = html.ToString();
            retorno[1] = Formatos.CurrencyFormat(lst.Sum(s=>s.totaldetalle));
            retorno[2] = Formatos.CurrencyFormat(lst.Sum(s => s.cancela));
            retorno[3] = Formatos.CurrencyFormat(lst.Sum(s => s.cancelasocio));
            retorno[4] = Formatos.CurrencyFormat(saldototal);

            return new JavaScriptSerializer().Serialize(retorno);



        }

        [WebMethod]
        public string SaveCancelaSocio(object objeto)
        {
            JsonObj obj = new JsonObj(objeto);
            Dcancelacionsocio dcs = new Dcancelacionsocio(objeto);
            dcs.crea_usr = obj.crea_usr;
            dcs.crea_fecha = DateTime.Now;
            List<Dcancelacionsocio> lst = General.SaveCancelaSocio(dcs);
            return new JavaScriptSerializer().Serialize(dcs);
        }

        [WebMethod]
        public string GetCancelacionesSocio(object objeto)
        {
            Comprobante fac = new Comprobante(objeto);
            fac = General.GetComprobante(fac.com_empresa, fac.com_codigo);

            List<Dcancelacion> dcancelaciones = new List<Dcancelacion>();
            List<Dcancelacionsocio> dcancelacionsocios = new List<Dcancelacionsocio>();

            List<Ddocumento> lstddo = General.GetCancelacionesSocios(fac.com_empresa, fac.com_codigo, fac.com_tipodoc, out dcancelaciones, out dcancelacionsocios);

            decimal? cancelacionessum = fac.cancelaciones.Sum(s => s.dca_monto ?? 0);
            decimal? cancelacionessociosum = dcancelacionsocios.Sum(s => s.dcs_monto ?? 0);
            decimal? entregado = cancelacionessum + cancelacionessociosum;


            StringBuilder html = new StringBuilder();

            html.Append("<div id='editpago' class='row form-group' data-comprobante='" + fac.com_codigo + "'>");

            html.Append("<div class='contado col-md-12'>");

            //html.Append(Document.LabelIconInput("txtCOMPROBANTE_P", fac.com_doctran, "FACTURA", "fa-file-invoice-dollar", "", "", "col-md-8", true, false, ElementEnums.InputType.text));
            html.Append(Document.LabelIconInput("txtCLIENTE_P", fac.ccomdoc.cdoc_nombre, "CLIENTE", "fa-user", "", "", "col-md-8", true, false, ElementEnums.InputType.text));
            html.Append(Document.LabelIconInput("txtTOTAL_P", Formatos.CurrencyFormat(fac.total.tot_total), "TOTAL", "fa-dollar", "", "right", "col-md-4", true, false, ElementEnums.InputType.text));

            html.Append(Document.LabelIconInput("txtCANCELA_P", Formatos.CurrencyFormat(cancelacionessum), "CANCELACIONES", "fa-dollar", "", "right", "col-md-4", true, false, ElementEnums.InputType.text));
            html.Append(Document.LabelIconInput("txtSALDO_P", Formatos.CurrencyFormat(cancelacionessociosum), "CANCELA SOCIO", "fa-dollar", "", "right", "col-md-4", true, false, ElementEnums.InputType.text));
            html.Append(Document.LabelIconInput("txtSALDO_P", Formatos.CurrencyFormat(fac.total.tot_total - entregado), "SALDO", "fa-dollar", "", "right", "col-md-4", true, false, ElementEnums.InputType.text));

            html.Append("</div>");

            html.Append("<div class='contado col-md-12' style='margin-top:10px;'>");

            html.Append("<table id='tdcobros'  class='table table-bordered table-striped table-condensed'>");
            html.Append("<thead>");
            html.Append("<tr>");
            //html.Append("<th class='col-md-2'>Nro</th>");
            html.Append("<th class='col-md-3'>Fecha</th>");
            html.Append("<th class='col-md-6'>Cancelación</th>");            
            //html.Append("<th class='col-md-2'>Nro</th>");
            html.Append("<th class='col-md-3'>Valor</th>");
            //html.Append("<th class='col-md-1'>Total</th>");
            html.Append("</tr>");
            html.Append("</thead>");
            html.Append("<tbody>");


            foreach (Dcancelacion item in dcancelaciones)
            {
                html.Append("<tr>");

                html.AppendFormat("<td><div class='textolistado'>{0:dd/MM/yyyy HH:mm}</div><div class='textolistadomin'>{1}<div></td>", item.dca_comprobantecanfecha, item.crea_usr);
                html.AppendFormat("<td>{0}</td>", item.dca_compcandoctran);
                html.AppendFormat("<td>{0}</td>", Formatos.CurrencyFormat(item.dca_monto));
                html.Append("<tr>");                
            }

            foreach (Dcancelacionsocio item in dcancelacionsocios)
            {
                html.Append("<tr>");

                html.AppendFormat("<td><div class='textolistado'>{0:dd/MM/yyyy HH:mm}</div><div class='textolistadomin'>{1}<div></td>", item.dcs_fecha, item.crea_usr);
                html.AppendFormat("<td>SOCIO-{0}</td>", item.dcs_codigo);
                html.AppendFormat("<td>{0}</td>", Formatos.CurrencyFormat(item.dcs_monto));
                html.Append("<tr>");
            }


            html.Append("</tbody>");
            html.Append("</table>");
            html.Append("</div>");

            html.Append("</div>");


            object[] retorno = new object[2];
            retorno[0] = fac.com_doctran + " <small>"+fac.com_codigo+"</small>";
            retorno[1] = html.ToString();
            return new JavaScriptSerializer().Serialize(retorno);



        }


        [WebMethod]
        public string SaveAllCancelaSocio(object objeto)
        {
            JsonObj obj = new JsonObj(objeto);
            object objcobros = Dictionaries.GetObject(objeto, "cobros", typeof(object));

            List<Dcancelacionsocio> lst = new List<Dcancelacionsocio>();
            try
            {
                if (objcobros != null)
                {
                    Array array = (Array)objcobros;
                    foreach (Object item in array)
                    {
                        if (item != null)
                        {
                            Dcancelacionsocio dcs = new Dcancelacionsocio(item);
                            dcs.crea_usr = obj.crea_usr;
                            dcs.crea_fecha = DateTime.Now;
                            General.SaveCancelaSocio(dcs);
                        }
                    }
                }
                return "ok";
            }
            catch(Exception ex)
            {
                return "Error, no se puede registrar el cobro socio...";
            }



            
        }

        #endregion

        #region Tools

        [WebMethod]
        public string CancelaFacturas(object objeto)
        {


            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object empresa = null;
            object numeros = null;

            tmp.TryGetValue("empresa", out empresa);
            tmp.TryGetValue("numeros", out numeros);



            string[] nums = numeros.ToString().Split(',');

            return Auto.CancelaFacturas(1, 13, nums, DateTime.Now, "admin");


        }

        [WebMethod]
        public string SetDuplicados(object objeto)
        {

         

            return Auto.SetDuplicados(1);


        }

        [WebMethod]
        public string AnularRETDuplicadas(object objeto)
        {
            return Auto.AnularRetencionesDuplicadas(1);


        }

        #endregion

        #region Load Obligaciones XML


        [WebMethod]
        public string GetLoadObligacion(object objeto)
        {
            JsonObj obj = new JsonObj(objeto);
            Usuario usr = UsuarioBLL.GetByPK(new Usuario { usr_id = obj.crea_usr, usr_id_key = obj.crea_usr });


            StringBuilder html = new StringBuilder();
            //html.AppendLine("<form role = \"form\" action = \"#\" >");
            //INICIO PRIMER FORM GRUOUP

            DateTime hasta = DateTime.Now;
            DateTime desde = new DateTime(hasta.Year, hasta.Month, 1);



            html.AppendLine("<div class=\"row form-group\">");
            html.Append(Document.Input("txtfiles", "", "multiple", "", false, false, ElementEnums.InputType.file));
            html.AppendLine("</div>");
            html.AppendLine("<button class=\"btn green btn-block margin-top-20\" id='btnsearch' onclick='Report();'>REPORTE<i class=\"m-icon-swapright m-icon-white\"></i></button>");
            html.AppendLine("<button class=\"btn yellow btn-block margin-top-20\" id='btnload' onclick='Cargar();'>CARGAR<i class=\"m-icon-swapright m-icon-white\"></i></button>");

            return html.ToString();
        }

        #endregion

        #region Migracion

        [WebMethod]
        public string UploadClientes(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object tipo = null;
            tmp.TryGetValue("tipo", out tipo);
            //string path = Server.MapPath("../data/data.txt");
            string path = Server.MapPath("../data/CLIENTES.csv");
            //return Packages.Migration.MigrateClientes(path);

            return Packages.Migration.MigrateClientesSICE(21);
        }


        [WebMethod]
        public string UploadProveedores(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object tipo = null;
            tmp.TryGetValue("tipo", out tipo);
            //string path = Server.MapPath("../data/data.txt");
            string path = Server.MapPath("../data/PROVEEDORES.csv");
            return Packages.Migration.MigrateProveedores(path);



        }


        [WebMethod]
        public string UploadObligaciones(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object tipo = null;
            tmp.TryGetValue("tipo", out tipo);
            //string path = Server.MapPath("../data/data.txt");
            string path = Server.MapPath("../data/OBLIGACIONES.csv");
            return Packages.Migration.MigrateObligaciones(path);



        }

        [WebMethod]
        public string UploadFacturas(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object tipo = null;
            tmp.TryGetValue("tipo", out tipo);
            //string path = Server.MapPath("../data/data.txt");
            string path = Server.MapPath("../data/FACTURAS.csv");
            return Packages.Migration.MigrateFacturas(path);



        }



        [WebMethod]
        public string UploadComprobantes(object objeto)
        {
            JsonObj obj = new JsonObj(objeto);

            //string path = Server.MapPath("../data/data.txt");
            string path = Server.MapPath("../data/PLANCUENTAS.csv");
            return Packages.Migration.MigrateComprobantesSICE(21,1);



        }

        [WebMethod]
        public string UploadRet(object objeto)
        {
            JsonObj obj = new JsonObj(objeto);

            //23BRYSEAR
            //21REDESK
            //20TMC
            //22TRANADECA

            return Packages.Migration.MigrateComprobantesSICE(22,3);



        }


        [WebMethod]
        public string UploadNC(object objeto)
        {
            JsonObj obj = new JsonObj(objeto);

            //23BRYSEAR
            //21REDESK
            //20TMC
            //22TRANADECA
            return Packages.Migration.MigrateComprobantesSICE(22, 2);



        }



        [WebMethod]
        public string UploadGuias(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object tipo = null;
            tmp.TryGetValue("tipo", out tipo);
            //string path = Server.MapPath("../data/data.txt");
            string path = Server.MapPath("../data/GUIAS.csv");
            return Packages.Migration.MigrateGuias(path);



        }


        [WebMethod]
        public string UploadHR(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object tipo = null;
            tmp.TryGetValue("tipo", out tipo);
            

            //string path = Server.MapPath("../data/HRCUE.csv");
            //string path = Server.MapPath("../data/HRQUINOR.csv");
            //string path = Server.MapPath("../data/HRQUISUR.csv");
            //string path = Server.MapPath("../data/HRGYQ.csv");
            string path = Server.MapPath("../data/HRAMB.csv");
            




            return Packages.Migration.MigrateHRBRY(path);



        }


        [WebMethod]
        public string UpdateHR(object objeto)
        {


            //string path = Server.MapPath("../data/HRCUE.csv");
            //string path = Server.MapPath("../data/HRQUINOR.csv");
            //string path = Server.MapPath("../data/HRQUISUR.csv");
            //string path = Server.MapPath("../data/HRGYQ.csv");
            string path = Server.MapPath("../data/HRAMB.csv");

                       
            return Packages.Migration.UpdateHRBRY1(path,"Ambato");



        }

        [WebMethod]
        public string UpdateCCOMENV(object objeto)
        {


           
            return Packages.Migration.UpdateCcomenv();



        }


        [WebMethod]
        public string UploadProductos(object objeto)
        {
            JsonObj obj = new JsonObj(objeto);

            return Packages.Migration.MigrateProductosSICE(21, 1);



        }

        [WebMethod]
        public string RemoveREC(object objeto)
        {
            JsonObj obj = new JsonObj(objeto);

            DateTime? desde = (DateTime?)Dictionaries.GetObject(objeto, "desde", typeof(DateTime?));
            DateTime? hasta = (DateTime?)Dictionaries.GetObject(objeto, "hasta", typeof(DateTime?));
            //DateTime desde = new DateTime(2019, 10, 1);
            //DateTime hasta = new DateTime(2019, 11, 1);

            return Packages.General.RemoveRecibos(obj.empresa, desde, hasta, "1,18");



        }

        #endregion

        #region

        [WebMethod]
        public string GetFormCuadres(object objeto)
        {



            StringBuilder html = new StringBuilder();
            //html.AppendLine("<form role = \"form\" action = \"#\" >");
            //INICIO PRIMER FORM GRUOUP

            html.AppendLine("<div class=\"row form-group\">");
            html.Append(Document.LabelIconDateRange("txtfecha", DateTime.Now.AddDays(-10), DateTime.Now, "Fecha", "fa-calendar", "", "", "col-md-4", false, false).ToString());
            html.Append(Document.Label("lblmensaje", "", "", "col-md-8"));
            html.AppendLine("</div>");

            html.AppendLine("<button class=\"btn blue btn-block margin-top-20\" id='btnsearch'>BUSCAR<i class=\"m-icon-swapright m-icon-white\"></i></button>");


            html.AppendLine("<table class='table table-bordered table-bordered table-hover' id='tdlistado'>");
            html.AppendLine("<thead>");
            html.AppendLine("<tr>");
            html.AppendLine("<th></th>");
            html.AppendLine("<th>Fecha</th>");
            html.AppendLine("<th>Comprobante</th>");
            html.AppendLine("<th>Cliente</th>");
            html.AppendLine("<th>TOTAL</th>");
            html.AppendLine("</tr>");
            html.AppendLine("</thead>");
            html.AppendLine("<tbody></tbody>");
            html.AppendLine("</table>");
            return html.ToString();
        }

        [WebMethod]
        public string GetComprobantesCuadre(object objeto)
        {
            JsonObj obj = new JsonObj(objeto);
            Usuario usr = UsuarioBLL.GetByPK(new Usuario { usr_id = obj.crea_usr, usr_id_key = obj.crea_usr });
            
            DateTime? desde = (DateTime?)Dictionaries.GetObject(objeto, "desde", typeof(DateTime?));
            DateTime? hasta = (DateTime?)Dictionaries.GetObject(objeto, "hasta", typeof(DateTime?));

            WhereParams where = new WhereParams();
            List<object> valores = new List<object>();          
            where.where = "com_empresa=" + obj.empresa + " and com_fecha between {0} and {1} and com_tipodoc in (4,17) and com_estado=2 and com_claveelec is null";
            valores.Add(desde);
            valores.Add(hasta);
            where.valores = valores.ToArray();

            List<Comprobante> lst = ComprobanteBLL.GetAll(where, "com_fecha");
            

            StringBuilder html = new StringBuilder();

            foreach (Comprobante item in lst)
            {


                html.AppendFormat("<tr data-codigo='{0}' class=''>", item.com_codigo);
                html.Append("<td>");
                html.Append(Document.CircleSmallButton("btngenerar", "", "Generar", "fa-cog", "blue", "GenerarElectronico(" + item.com_codigo + ");"));
                //html.Append(Document.CircleSmallButton("btninf", "", "Información", "fa-info-circle", "green", "Detalle(" + item.codigo + ");"));                
                html.Append("</td>");
                html.AppendFormat("<td><div class='textolistado'>{0}</div><div class='textolistadomin'>{1} {2}<div></td>", item.com_fecha, item.crea_usr, item.crea_fecha);
                html.AppendFormat("<td><div class='textolistado'><a href = '#' onclick = 'CallFormulario({2});' >{0}</a></div><div class='textolistadomin'>{1}<div><div class='textolistadomin'>{2}</div></td>", item.com_doctran, Constantes.GetEstadoName(item.com_estado), item.com_codigo);
                html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", item.com_ciruc);
                html.AppendFormat("<td><div class='textolistado'>{0}</div></td>", Formatos.CurrencyFormat(item.com_total));
                html.AppendLine("</tr>");


            }

            object[] retorno = new object[2];
            retorno[0] =  lst.Count + " comprobantes...";
            retorno[1] = html.ToString();
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
        #endregion


        #region Carga Electronico

        [WebMethod]
        public string GetLastCargas(object objeto)
        {
            int? top = (int?)Dictionaries.GetObject(objeto, "top", typeof(int?));

            return Packages.Electronico.GetLastElectronicoCargas(1, top);

        }


        [WebMethod]
        public string GetCargaElectronico(object objeto)
        {
            Electronicocarga electronicocarga = new Electronicocarga(objeto);
            electronicocarga.eca_empresa_key = electronicocarga.eca_empresa;
            electronicocarga.eca_id_key = electronicocarga.eca_id;
            electronicocarga = ElectronicocargaBLL.GetByPK(electronicocarga);
            StringBuilder html = new StringBuilder();

            TimeSpan tiempo = new TimeSpan();
            if (electronicocarga.eca_fin.HasValue)
                tiempo = electronicocarga.eca_fin.Value.Subtract(electronicocarga.eca_inicio.Value);
            else
                tiempo = DateTime.Now.Subtract(electronicocarga.eca_inicio.Value);

            html.AppendLine("<table class='table table-bordered table-bordered table-hover' id='tdelectronico'>");                        
            html.AppendLine("<tr>");
            html.AppendLine("<td>Id:</td>");
            html.AppendFormat("<td>{0}</td>", electronicocarga.eca_id);
            html.AppendLine("</tr>");
            html.AppendLine("<tr>");
            html.AppendLine("<td>Inicio:</td>");
            html.AppendFormat("<td>{0}</td>", electronicocarga.eca_inicio.Value.ToString("dd/MM/yyyy HH:mm:ss"));
            html.AppendLine("</tr>");
            html.AppendLine("<td>Fin:</td>");
            html.AppendFormat("<td>{0}</td>", electronicocarga.eca_fin.HasValue ? electronicocarga.eca_fin.Value.ToString("dd/MM/yyyy HH:mm:ss") : "");
            html.AppendLine("</tr>");
            html.AppendLine("<tr>");
            html.AppendLine("<td>Tiempo Carga:</td>");
            html.AppendFormat("<td>{0:0} minutos</td>", tiempo.TotalMinutes);
            html.AppendLine("</tr>");

            html.AppendLine("<tr>");
            html.AppendLine("<td>Registros:</td>");
            html.AppendFormat("<td>{0}</td>", electronicocarga.eca_registros);
            html.AppendLine("</tr>");

            html.AppendLine("<tr>");
            html.AppendLine("<td>Cargados:</td>");
            html.AppendFormat("<td>{0}</td>", electronicocarga.eca_descargados??0);
            html.AppendLine("</tr>");

            html.AppendLine("<tr>");
            html.AppendLine("<td>Creados:</td>");
            html.AppendFormat("<td><a href='#' data-id='{1}' onclick='GetCargaDetalleOK($(this).data());'>{0}</a></td>", electronicocarga.eca_creados??0, electronicocarga.eca_id);
            html.AppendLine("</tr>");

            html.AppendLine("<tr>");
            html.AppendLine("<td>Errores:</td>");            
            html.AppendFormat("<td><a href='#' data-id='{1}' onclick='GetCargaDetalleERROR($(this).data());'>{0}</a></td>", electronicocarga.eca_error, electronicocarga.eca_id); ;
            html.AppendLine("</tr>");           
            html.AppendLine("</table>");

            
            



            object[] retorno = new object[2];
            retorno[0] = electronicocarga;
            retorno[1] = html.ToString();
            return new JavaScriptSerializer().Serialize(retorno);


        }


        [WebMethod]
        public string GetDetalleElectronico(object objeto)
        {
            string tipo = (string)Dictionaries.GetObject(objeto, "tipo", typeof(string));

            Electronicocarga electronicocarga = new Electronicocarga(objeto);

            string where = "ele_empresa=" + electronicocarga.eca_empresa + " and ele_carga='" + electronicocarga.eca_id + "'";
            if (tipo == "OK")
                where += " and ele_comprobante>0";
            else if (tipo=="ERROR")
                where += " and ele_comprobante<0";
            List<BusinessObjects.Electronico> lst = BusinessLogicLayer.ElectronicoBLL.GetAll(where, "ele_fecha_carga");
            
            StringBuilder html = new StringBuilder();

            html.Append("<div class='row'><div class='col-md-12'>");
            html.AppendLine("<table class='table table-bordered table-bordered table-hover table-sm' id='tdelectronico'>");
            html.Append("<tr>");
            html.Append("<th>Electronico</th>");
            html.Append("<th>Respuesta</th>");
            html.Append("</tr>");

            foreach (var item in lst)
            {
                html.AppendLine("<tr>");                
                html.AppendFormat("<td>{0}<small><br>{1}<br>{2}<br>{3}</small></td>", item.ele_clave,item.ele_documento,item.ele_fecha_autoriza.Value.ToString("dd/MM/yyyy"), item.ele_estadoelectronico); 
                html.AppendFormat("<td><small>{0}</small></td>", item.ele_respuesta);
                html.AppendLine("</tr>");

            }
            html.AppendLine("</table>");
            html.Append("</div></div>");
            return html.ToString();


        }

        [WebMethod]
        public string AnulateCargaElectronico(object objeto)
        {
            
            Electronicocarga electronicocarga = new Electronicocarga(objeto);

            return Packages.Electronico.AnulateElectronicoCarga(electronicocarga.eca_empresa, electronicocarga.eca_id);


        }

        #endregion
    }
}
