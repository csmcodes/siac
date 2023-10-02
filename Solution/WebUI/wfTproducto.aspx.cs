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
    public partial class wfTproducto : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "tpr_orden";
        protected static string WhereClause = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
            }
        }


        public static string ShowData(Tproducto obj)
        {

            return "";
        }

        public static string ShowObject(Tproducto obj)
        {
            StringBuilder html = new StringBuilder();


            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.tpr_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.tpr_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.tpr_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.tpr_nombre, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.tpr_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Tproducto obj)
        {
            WhereClause = "";
        }

        public static string GetMenuOptions(List<Tproducto> lst, int? padre, bool addstruc)
        {

            List<Tproducto> lsthijos = lst.FindAll(delegate(Tproducto m) { return m.tpr_reporta == padre; });
            StringBuilder html = new StringBuilder();
            if (lsthijos.Count > 0)
            {
                if (addstruc)
                    html.AppendLine("<ul style=\"display: block;\">");

                foreach (Tproducto obj in lsthijos)
                {
                    bool hijos = false;
                    //List<Tproducto> lsthijos = lst.FindAll(delegate(Tproducto m) { return m.PADRE == obj.CODIGO; });
                    //WhereClause = "PADRE ="+obj.CODIGO;
                    //List<Tproducto> lsthijos = TproductoBLL.GetAll(WhereClause, OrderByClause);
                    //if (lsthijos.Count > 0)
                    //    hijos = true;
                    string htmlhijos = GetMenuOptions(lst, obj.tpr_codigo, true);
                    if (htmlhijos != "")
                        hijos = true;
                    string id = "{\"tpr_codigo\":\"" + obj.tpr_codigo + "\", \"tpr_empresa\":\"" + obj.tpr_empresa + "\"}";//ID COMPUESTO

                    html.AppendLine(HtmlElements.MenuItem(id, obj.tpr_nombre, "", hijos));
                    html.AppendLine(htmlhijos);

                    /*if (hijos)
                    {
                        html.AppendLine("<ul style=\"display: block;\">");
                        html.AppendLine(GetTproductoOptions(lsthijos));   
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
            html.AppendLine("<li class=\"nav-header\">Tproducto</li>");

            List<Tproducto> lst = TproductoBLL.GetAll(WhereClause, OrderByClause);

            //WhereClause = "PADRE IS NULL";
            //List<Tproducto> lst = TproductoBLL.GetAll(WhereClause, OrderByClause);
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
            return ShowObject(new Tproducto());
        }


        [WebMethod]
        public static string GetObject(object id)
        {


            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("tpr_codigo", out codigo);
            tmp.TryGetValue("tpr_empresa", out empresa);

            Tproducto obj = new Tproducto();

            if (empresa != null && !empresa.Equals(""))
            {
                obj.tpr_empresa = int.Parse(empresa.ToString());
                obj.tpr_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.tpr_codigo = int.Parse(codigo.ToString());
                obj.tpr_codigo_key = int.Parse(codigo.ToString());
            }




            obj = TproductoBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
            //return ShowObject(obj);

        }


        //public static Tproducto GetObjeto(object objeto)
        //{
        //    Tproducto obj = new Tproducto();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object codigokey = null;
        //        object empresa = null;
        //        object empresakey = null;
        //        object nombre = null;
        //        object id = null;

        //        object activo = null;
        //        tmp.TryGetValue("tpr_codigo", out codigo);
        //        tmp.TryGetValue("tpr_codigo_key", out codigokey);
        //        tmp.TryGetValue("tpr_empresa", out empresa);
        //        tmp.TryGetValue("tpr_empresa_key", out empresakey);
        //        tmp.TryGetValue("tpr_nombre", out nombre);
        //        tmp.TryGetValue("tpr_id", out id);
        //        tmp.TryGetValue("tpr_estado", out activo);
        //        if (codigo != null)
        //        {
        //            obj.tpr_codigo = int.Parse(codigo.ToString());
        //            obj.tpr_codigo_key = int.Parse(codigokey.ToString());
        //        }


        //        if (empresa != null)
        //        {
        //            obj.tpr_empresa = int.Parse(empresa.ToString());
        //            obj.tpr_empresa_key = int.Parse(empresakey.ToString());
        //        }

        //        obj.tpr_nombre = (string)nombre;
        //        obj.tpr_id = (string)id;
        //        obj.tpr_estado = (int?)activo;
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
            return ShowObject(new Tproducto());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Tproducto obj = new Tproducto(objeto);            
            obj.tpr_codigo_key = obj.tpr_codigo;            
            obj.tpr_empresa_key = obj.tpr_empresa;


            Tproducto opt = new Tproducto();
            opt.tpr_codigo = obj.tpr_codigo;
            opt.tpr_codigo_key = obj.tpr_codigo_key;
            opt.tpr_empresa = obj.tpr_empresa;
            opt.tpr_empresa_key = obj.tpr_empresa_key;
            opt = TproductoBLL.GetByPK(opt);
            opt.tpr_nombre = obj.tpr_nombre;

            opt.tpr_estado = obj.tpr_estado;





            opt.tpr_id = obj.tpr_id;



            if (TproductoBLL.Update(opt) > 0)
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
            Tproducto obj = new Tproducto(objeto);
            obj.tpr_codigo_key = obj.tpr_codigo_key;
            obj.tpr_empresa_key = obj.tpr_empresa_key;
            if (TproductoBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }




        [WebMethod]
        public static string AddOption()
        {

            int max = TproductoBLL.GetMax("tpr_codigo") + 1;


            Tproducto obj = new Tproducto();
            
            obj.tpr_empresa = 1;

            obj.tpr_nombre = "Nueva Opción";
            obj.tpr_orden = max;
            obj.tpr_estado = 1;
            if (TproductoBLL.Insert(obj) > 0)
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
            tmp.TryGetValue("tpr_codigo", out codigo);
            tmp.TryGetValue("tpr_empresa", out empresa);

            Tproducto parentobj = new Tproducto();

            if (empresa != null && !empresa.Equals(""))
            {
                parentobj.tpr_empresa = int.Parse(empresa.ToString());
                parentobj.tpr_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                parentobj.tpr_codigo = int.Parse(codigo.ToString());
                parentobj.tpr_codigo_key = int.Parse(codigo.ToString());
            }
            parentobj = TproductoBLL.GetByPK(parentobj);












            int max = TproductoBLL.GetMax("tpr_codigo") + 1;


            Tproducto obj = new Tproducto();
            
            obj.tpr_empresa = 1;

            obj.tpr_nombre = "Nueva Opción";
            obj.tpr_orden = max;
            obj.tpr_reporta = int.Parse(codigo.ToString());
            obj.tpr_estado = 1;
            if (TproductoBLL.Insert(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";

        }
    }
}