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
using HtmlObjects;


namespace WebUI
{
    public partial class wfCpersona : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "cper_orden";
        protected static string WhereClause = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
            }
        }


        public static string ShowData(Cpersona obj)
        {

            return "";
        }

        public static string ShowObject(Cpersona obj)
        {
            StringBuilder html = new StringBuilder();


            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.cper_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.cper_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.cper_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtTIPO", etiqueta = "Tipo", placeholder = "Tipo", valor = obj.cper_tipo, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.cper_nombre, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Cpersona obj)
        {
            WhereClause = "";
        }

        public static string GetMenuOptions(List<Cpersona> lst, int? padre, bool addstruc)
        {

            List<Cpersona> lsthijos = lst.FindAll(delegate(Cpersona m) { return m.cper_reporta == padre; });
            StringBuilder html = new StringBuilder();
            if (lsthijos.Count > 0)
            {
                if (addstruc)
                    html.AppendLine("<ul style=\"display: block;\">");

                foreach (Cpersona obj in lsthijos)
                {
                    bool hijos = false;
                    //List<Cpersona> lsthijos = lst.FindAll(delegate(Cpersona m) { return m.PADRE == obj.CODIGO; });
                    //WhereClause = "PADRE ="+obj.CODIGO;
                    //List<Cpersona> lsthijos = CpersonaBLL.GetAll(WhereClause, OrderByClause);
                    //if (lsthijos.Count > 0)
                    //    hijos = true;
                    string htmlhijos = GetMenuOptions(lst, obj.cper_codigo, true);
                    if (htmlhijos != "")
                        hijos = true;
                    string id = "{\"cper_codigo\":\"" + obj.cper_codigo + "\", \"cper_empresa\":\"" + obj.cper_empresa + "\"}";//ID COMPUESTO

                    html.AppendLine(HtmlElements.MenuItem(id, obj.cper_nombre, "", hijos));
                    html.AppendLine(htmlhijos);

                    /*if (hijos)
                    {
                        html.AppendLine("<ul style=\"display: block;\">");
                        html.AppendLine(GetCpersonaOptions(lsthijos));   
                        html.AppendLine("</ul>");
                    }*/

                }
                if (addstruc)
                    html.AppendLine("</ul>");
            }
            return html.ToString();
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            StringBuilder html = new StringBuilder();

            html.AppendLine("<div class=\"leftmenu\"><ul class=\"nav nav-tabs nav-stacked\">");
            html.AppendLine("<li class=\"nav-header\">Cpersona</li>");

            List<Cpersona> lst = CpersonaBLL.GetAll(WhereClause, OrderByClause);

            //WhereClause = "PADRE IS NULL";
            //List<Cpersona> lst = CpersonaBLL.GetAll(WhereClause, OrderByClause);
            html.AppendLine(GetMenuOptions(lst, null, false));

            html.AppendLine("</ul></div>");




            return html.ToString();
        }

        [WebMethod]
        public static string ReloadData(object objeto)
        {
            pageIndex = 1;
            return GetData(objeto);
        }



        [WebMethod]
        public static string AddObject()
        {
            return ShowObject(new Cpersona());
        }


        [WebMethod]
        public static string GetObject(object id)
        {


            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("cper_codigo", out codigo);
            tmp.TryGetValue("cper_empresa", out empresa);

            Cpersona obj = new Cpersona();

            if (empresa != null && !empresa.Equals(""))
            {
                obj.cper_empresa = int.Parse(empresa.ToString());
                obj.cper_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.cper_codigo = int.Parse(codigo.ToString());
                obj.cper_codigo_key = int.Parse(codigo.ToString());
            }




            obj = CpersonaBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
            //return ShowObject(obj);

        }


        //public static Cpersona GetObjeto(object objeto)
        //{
        //    Cpersona obj = new Cpersona();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object codigokey = null;
        //        object empresa = null;
        //        object empresakey = null;
        //        object nombre = null;
        //        object tipo = null;
        //        object id = null;

        //        object activo = null;
        //        tmp.TryGetValue("cper_codigo", out codigo);
        //        tmp.TryGetValue("cper_codigo_key", out codigokey);
        //        tmp.TryGetValue("cper_empresa", out empresa);
        //        tmp.TryGetValue("cper_empresa_key", out empresakey);
        //        tmp.TryGetValue("cper_nombre", out nombre);
        //        tmp.TryGetValue("cper_tipo", out tipo);
        //        tmp.TryGetValue("cper_id", out id);
        //        tmp.TryGetValue("cper_estado", out activo);
        //        if (codigo != null)
        //        {
        //            obj.cper_codigo = int.Parse(codigo.ToString());
        //            obj.cper_codigo_key = int.Parse(codigokey.ToString());
        //        }


        //        if (empresa != null)
        //        {
        //            obj.cper_empresa = int.Parse(empresa.ToString());
        //            obj.cper_empresa_key = int.Parse(empresakey.ToString());
        //        }

        //        obj.cper_nombre = (string)nombre;
        //        obj.cper_tipo = (string)tipo;
        //        obj.cper_id = (string)id;
        //        obj.cper_estado = (int?)activo;
        //        obj.crea_usr = "admin";
        //        obj.crea_fecha = DateTime.Now;
        //        obj.mod_usr = "admin";
        //        obj.mod_fecha = DateTime.Now;

        //    }

        //    return obj;
        //}


        [WebMethod]
        public static string GetSearch()
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"pull-left\">");


            html.AppendLine(new Input { id = "txtNOMBRE_S", placeholder = "Nombre", clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbESTADO_S", clase = Css.medium, diccionario = Dictionaries.GetEstado() }.ToString());

            html.AppendLine("</div>");
            html.AppendLine("<div class=\"pull-right\">");
            html.AppendLine(new Boton { refresh = true }.ToString());
            html.AppendLine(new Boton { clean = true }.ToString());
            html.AppendLine("</div>");
            return html.ToString();
        }

        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Cpersona());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Cpersona obj = new Cpersona(objeto);
            obj.cper_codigo_key = obj.cper_codigo;
            obj.cper_empresa_key = obj.cper_empresa;
            Cpersona opt = obj;
            obj = CpersonaBLL.GetByPK(obj);
            opt.cper_orden = obj.cper_orden;
            opt.cper_reporta = obj.cper_reporta;


            if (CpersonaBLL.Update(opt) > 0)
            {
                //return ShowData(obj);
                return "OK";
            }
            else
                return "ERROR";

        }
        [WebMethod]
        public static string DeleteObject(object objeto)
        {
            Cpersona obj = new Cpersona(objeto);
            obj.cper_codigo_key = obj.cper_codigo;
            obj.cper_empresa_key = obj.cper_empresa;
           

            if (CpersonaBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }




        [WebMethod]
        public static string AddOption()
        {

            int max = CpersonaBLL.GetMax("cper_codigo") + 1;


            Cpersona obj = new Cpersona();
            
            obj.cper_empresa = 1;

            obj.cper_nombre = "Nueva Opción";
            obj.cper_orden = max;
            obj.cper_estado = 1;
            if (CpersonaBLL.Insert(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";

        }

        [WebMethod]
        public static string AddChildOption(object id)
        {


            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("cper_codigo", out codigo);
            tmp.TryGetValue("cper_empresa", out empresa);

            Cpersona parentobj = new Cpersona();

            if (empresa != null && !empresa.Equals(""))
            {
                parentobj.cper_empresa = int.Parse(empresa.ToString());
                parentobj.cper_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                parentobj.cper_codigo = int.Parse(codigo.ToString());
                parentobj.cper_codigo_key = int.Parse(codigo.ToString());
            }
            parentobj = CpersonaBLL.GetByPK(parentobj);












            int max = CpersonaBLL.GetMax("cper_codigo") + 1;


            Cpersona obj = new Cpersona();
           
            obj.cper_empresa = 1;

            obj.cper_nombre = "Nueva Opción";
            obj.cper_orden = max;
            obj.cper_reporta = int.Parse(codigo.ToString());
            obj.cper_estado = 1;
            if (CpersonaBLL.Insert(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";

        }
    }
}