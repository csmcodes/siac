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
    public partial class wfTpersona : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "tper_orden";
        protected static string WhereClause = "";
        protected static WhereParams parametros = new WhereParams();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
            }
        }


        public static string ShowData(Tpersona obj)
        {

            return "";
        }

        public static string ShowObject(Tpersona obj)
        {
            StringBuilder html = new StringBuilder();


            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.tper_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.tper_codigo_key.ToString(), visible = false }.ToString());
          html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.tper_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtTIPO", etiqueta = "Tipo", placeholder = "Tipo", valor = obj.tper_tipo, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.tper_nombre, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Tpersona obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (!string.IsNullOrEmpty(obj.tper_tipo))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tper_tipo like {" + contador + "} ";
                valores.Add("%" + obj.tper_tipo + "%");
                contador++;
            }
        }

        public static string GetMenuOptions(List<Tpersona> lst, int? padre, bool addstruc)
        {

            List<Tpersona> lsthijos = lst.FindAll(delegate(Tpersona m) { return m.tper_reporta == padre; });
            StringBuilder html = new StringBuilder();
            if (lsthijos.Count > 0)
            {
                if (addstruc)
                    html.AppendLine("<ul style=\"display: block;\">");

                foreach (Tpersona obj in lsthijos)
                {
                    bool hijos = false;
                    //List<Tpersona> lsthijos = lst.FindAll(delegate(Tpersona m) { return m.PADRE == obj.CODIGO; });
                    //WhereClause = "PADRE ="+obj.CODIGO;
                    //List<Tpersona> lsthijos = TpersonaBLL.GetAll(WhereClause, OrderByClause);
                    //if (lsthijos.Count > 0)
                    //    hijos = true;
                    string htmlhijos = GetMenuOptions(lst, obj.tper_codigo, true);
                    if (htmlhijos != "")
                        hijos = true;
                    string id = "{\"tper_codigo\":\"" + obj.tper_codigo + "\", \"tper_empresa\":\"" + obj.tper_empresa + "\"}";//ID COMPUESTO

                    html.AppendLine(HtmlElements.MenuItem(id, obj.tper_nombre, "", hijos));
                    html.AppendLine(htmlhijos);

                    /*if (hijos)
                    {
                        html.AppendLine("<ul style=\"display: block;\">");
                        html.AppendLine(GetTpersonaOptions(lsthijos));   
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
            html.AppendLine("<li class=\"nav-header\">Tpersona</li>");

            List<Tpersona> lst = TpersonaBLL.GetAll(WhereClause, OrderByClause);

            //WhereClause = "PADRE IS NULL";
            //List<Tpersona> lst = TpersonaBLL.GetAll(WhereClause, OrderByClause);
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
            return ShowObject(new Tpersona());
        }


        [WebMethod]
        public static string GetObject(object id)
        {


            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("tper_codigo", out codigo);
            tmp.TryGetValue("tper_empresa", out empresa);

            Tpersona obj = new Tpersona();

            if (empresa != null && !empresa.Equals(""))
            {
                obj.tper_empresa = int.Parse(empresa.ToString());
                obj.tper_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.tper_codigo = int.Parse(codigo.ToString());
                obj.tper_codigo_key = int.Parse(codigo.ToString());
            }




            obj = TpersonaBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
            //return ShowObject(obj);

        }


        //public static Tpersona GetObjeto(object objeto)
        //{
        //    Tpersona obj = new Tpersona();
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
        //        tmp.TryGetValue("tper_codigo", out codigo);
        //        tmp.TryGetValue("tper_codigo_key", out codigokey);
        //        tmp.TryGetValue("tper_empresa", out empresa);
        //        tmp.TryGetValue("tper_empresa_key", out empresakey);
        //        tmp.TryGetValue("tper_nombre", out nombre);
        //        tmp.TryGetValue("tper_tipo", out tipo);
        //        tmp.TryGetValue("tper_id", out id);
        //        tmp.TryGetValue("tper_estado", out activo);
        //        if (codigo != null)
        //        {
        //            obj.tper_codigo = int.Parse(codigo.ToString());
        //            obj.tper_codigo_key = int.Parse(codigokey.ToString());
        //        }


        //        if (empresa != null)
        //        {
        //            obj.tper_empresa = int.Parse(empresa.ToString());
        //            obj.tper_empresa_key = int.Parse(empresakey.ToString());
        //        }

        //        obj.tper_nombre = (string)nombre;
        //        obj.tper_tipo = (string)tipo;
        //        obj.tper_id = (string)id;
        //        obj.tper_estado = (int?)activo;
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
            return ShowObject(new Tpersona());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
           
            Tpersona obj = new Tpersona(objeto);
            obj.tper_codigo_key = obj.tper_codigo;
            obj.tper_empresa_key = obj.tper_empresa_key;
            Tpersona opt = obj;
            obj = TpersonaBLL.GetByPK(obj);
            opt.tper_orden = obj.tper_orden;
            opt.tper_reporta = obj.tper_reporta;



            if (TpersonaBLL.Update(opt) > 0)
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
            Tpersona obj = new Tpersona(objeto);
            obj.tper_codigo_key = obj.tper_codigo_key;
            obj.tper_empresa_key = obj.tper_empresa_key;
            if (TpersonaBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }




        [WebMethod]
        public static string AddOption()
        {

            int max = TpersonaBLL.GetMax("tper_codigo") + 1;


            Tpersona obj = new Tpersona();
            
            obj.tper_empresa = 1;

            obj.tper_nombre = "Nueva Opción";
            obj.tper_orden = max;
            obj.tper_estado = 1;
            if (TpersonaBLL.Insert(obj) > 0)
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
            tmp.TryGetValue("tper_codigo", out codigo);
            tmp.TryGetValue("tper_empresa", out empresa);

            Tpersona parentobj = new Tpersona();

            if (empresa != null && !empresa.Equals(""))
            {
                parentobj.tper_empresa = int.Parse(empresa.ToString());
                parentobj.tper_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                parentobj.tper_codigo = int.Parse(codigo.ToString());
                parentobj.tper_codigo_key = int.Parse(codigo.ToString());
            }
            parentobj = TpersonaBLL.GetByPK(parentobj);












            int max = TpersonaBLL.GetMax("tper_codigo") + 1;


            Tpersona obj = new Tpersona();
        
            obj.tper_empresa = 1;

            obj.tper_nombre = "Nueva Opción";
            obj.tper_orden = max;
            obj.tper_reporta = int.Parse(codigo.ToString());
            obj.tper_estado = 1;
            if (TpersonaBLL.Insert(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";

        }
    }
}