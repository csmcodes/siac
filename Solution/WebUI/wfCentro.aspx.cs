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
    public partial class wfCentro : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "cen_orden";
        protected static string WhereClause = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
            }
        }


        public static string ShowData(Centro obj)
        {
            
            return "";
        }

        public static string ShowObject(Centro obj)
        {
            StringBuilder html = new StringBuilder();


            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.cen_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.cen_codigo_key.ToString(), visible = false }.ToString());
           html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.cen_id, clase = Css.large ,obligatorio=true}.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.cen_nombre, clase = Css.large, obligatorio = true }.ToString());           
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Centro obj)
        {
            WhereClause = "";
        }

        public static string GetMenuOptions(List<Centro> lst, int? padre, bool addstruc)
        {

            List<Centro> lsthijos = lst.FindAll(delegate(Centro m) { return m.cen_reporta == padre; });
            StringBuilder html = new StringBuilder();
            if (lsthijos.Count > 0)
            {
                if (addstruc)
                    html.AppendLine("<ul style=\"display: block;\">");

                foreach (Centro obj in lsthijos)
                {
                    bool hijos = false;
                    //List<Centro> lsthijos = lst.FindAll(delegate(Centro m) { return m.PADRE == obj.CODIGO; });
                    //WhereClause = "PADRE ="+obj.CODIGO;
                    //List<Centro> lsthijos = CentroBLL.GetAll(WhereClause, OrderByClause);
                    //if (lsthijos.Count > 0)
                    //    hijos = true;
                    string htmlhijos = GetMenuOptions(lst, obj.cen_codigo, true);
                    if (htmlhijos != "")
                        hijos = true;
                    string id = "{\"cen_codigo\":\"" + obj.cen_codigo + "\", \"cen_empresa\":\"" + obj.cen_empresa + "\"}";//ID COMPUESTO

                    html.AppendLine(HtmlElements.MenuItem(id, obj.cen_nombre, "", hijos));
                    html.AppendLine(htmlhijos);

                    /*if (hijos)
                    {
                        html.AppendLine("<ul style=\"display: block;\">");
                        html.AppendLine(GetCentroOptions(lsthijos));   
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
            html.AppendLine("<li class=\"nav-header\">Centro</li>");

            List<Centro> lst = CentroBLL.GetAll(WhereClause, OrderByClause);

            //WhereClause = "PADRE IS NULL";
            //List<Centro> lst = CentroBLL.GetAll(WhereClause, OrderByClause);
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
            return ShowObject(new Centro());
        }


        [WebMethod]
        public static string GetObject(object id)
        {


            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("cen_codigo", out codigo);
            tmp.TryGetValue("cen_empresa", out empresa);

            Centro obj = new Centro();

            if (empresa != null && !empresa.Equals(""))
            {
                obj.cen_empresa = int.Parse(empresa.ToString());
                obj.cen_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.cen_codigo = int.Parse(codigo.ToString());
                obj.cen_codigo_key = int.Parse(codigo.ToString());
            }




            obj = CentroBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
            //return ShowObject(obj);

        }


        //public static Centro GetObjeto(object objeto)
        //{
        //    Centro obj = new Centro();
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
        //        tmp.TryGetValue("cen_codigo", out codigo);
        //        tmp.TryGetValue("cen_codigo_key", out codigokey);
        //        tmp.TryGetValue("cen_empresa", out empresa);
        //        tmp.TryGetValue("cen_empresa_key", out empresakey);
        //        tmp.TryGetValue("cen_nombre", out nombre);
        //        tmp.TryGetValue("cen_id", out id);       
        //        tmp.TryGetValue("cen_estado", out activo);
        //        if (codigo != null)
        //        {
        //            obj.cen_codigo = int.Parse(codigo.ToString());
        //            obj.cen_codigo_key = int.Parse(codigokey.ToString());
        //        }


        //        if (empresa != null)
        //        {
        //            obj.cen_empresa = int.Parse(empresa.ToString());
        //            obj.cen_empresa_key = int.Parse(empresakey.ToString());
        //        }

        //        obj.cen_nombre = (string)nombre;
        //        obj.cen_id = (string)id;                
        //        obj.cen_estado = (int?)activo;
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
            return ShowObject(new Centro());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
           
            Centro obj = new Centro(objeto);
            obj.cen_codigo_key = obj.cen_codigo;
            obj.cen_empresa_key = obj.cen_empresa;
            Centro opt = obj;
            obj = CentroBLL.GetByPK(obj);
            opt.cen_orden = obj.cen_orden;
            opt.cen_reporta = obj.cen_reporta;
          


          /*  Centro opt = new Centro();
            opt.cen_nombre = obj.cen_nombre;
            opt.cen_estado = obj.cen_estado;            
            opt.cen_id = obj.cen_id;

            obj.cen_codigo_key = obj.cen_codigo;
            obj.cen_empresa_key = obj.cen_empresa;
            obj = CentroBLL.GetByPK(obj);
            obj.cen_codigo_key = obj.cen_codigo;
            obj.cen_empresa_key = obj.cen_empresa;
            obj.cen_nombre = opt.cen_nombre;
            obj.cen_estado = opt.cen_estado;
            obj.cen_id = opt.cen_id;


            */



            if (CentroBLL.Update(opt) > 0)
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
            Centro obj = new Centro(objeto);
            obj.cen_codigo_key = obj.cen_codigo;
            obj.cen_empresa_key = obj.cen_empresa;
            if (CentroBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }




        [WebMethod]
        public static string AddOption()
        {

            int max = CentroBLL.GetMax("cen_codigo") + 1;


            Centro obj = new Centro();
            
            obj.cen_empresa = 1;
           
            obj.cen_nombre = "Nueva Opción";
            obj.cen_orden = max;
            obj.cen_estado = 1;
            if (CentroBLL.Insert(obj) > 0)
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
            tmp.TryGetValue("cen_codigo", out codigo);
            tmp.TryGetValue("cen_empresa", out empresa);

            Centro parentobj = new Centro();

            if (empresa != null && !empresa.Equals(""))
            {
                parentobj.cen_empresa = int.Parse(empresa.ToString());
                parentobj.cen_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                parentobj.cen_codigo = int.Parse(codigo.ToString());
                parentobj.cen_codigo_key = int.Parse(codigo.ToString());
            }
            parentobj = CentroBLL.GetByPK(parentobj);












            int max = CentroBLL.GetMax("cen_codigo") + 1;


            Centro obj = new Centro();
           
            obj.cen_empresa = 1;
          
            obj.cen_nombre = "Nueva Opción";
            obj.cen_orden = max;
            obj.cen_reporta = int.Parse(codigo.ToString());
            obj.cen_estado = 1;
            if (CentroBLL.Insert(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";

        }
    }
}