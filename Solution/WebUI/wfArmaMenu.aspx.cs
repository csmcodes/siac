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
    public partial class wfArmaMenu : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "men_orden";
        protected static string WhereClause = "";        
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
            }
        }


        public static string ShowData(BusinessObjects.Menu obj)
        {
            
            return "";
        }

        public static string ShowObject(BusinessObjects.Menu obj)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.men_id.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.men_id.ToString(), visible = false }.ToString());      
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.men_nombre, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtFORMULARIO", etiqueta = "Formulario", placeholder = "Formulario", valor = obj.men_formulario, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtIMAGEN", etiqueta = "Clase", placeholder = "Clase", valor = obj.men_imagen, clase = Css.large }.ToString());
          
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(BusinessObjects.Menu obj)
        {
            WhereClause = "";
        }

        public static string GetMenuOptions(List<BusinessObjects.Menu> lst, int? padre, bool addstruc)
        {

            List<BusinessObjects.Menu> lsthijos = lst.FindAll(delegate(BusinessObjects.Menu m) { return m.men_padre == padre; });            
            StringBuilder html = new StringBuilder();
            if (lsthijos.Count > 0)
            {
                if (addstruc)
                    html.AppendLine("<ul style=\"display: block;\">");

                foreach (BusinessObjects.Menu obj in lsthijos)
                {
                    bool hijos = false;
                    //List<BusinessObjects.Menu> lsthijos = lst.FindAll(delegate(BusinessObjects.Menu m) { return m.PADRE == obj.CODIGO; });
                    //WhereClause = "PADRE ="+obj.CODIGO;
                    //List<BusinessObjects.Menu> lsthijos = MenuBLL.GetAll(WhereClause, OrderByClause);
                    //if (lsthijos.Count > 0)
                    //    hijos = true;
                    string htmlhijos = GetMenuOptions(lst, obj.men_id, true);
                    if (htmlhijos != "")
                        hijos = true;

                    html.AppendLine(HtmlElements.MenuItem(obj.men_id.ToString(), obj.men_nombre, obj.men_imagen, hijos));
                    html.AppendLine(htmlhijos);

                    /*if (hijos)
                    {
                        html.AppendLine("<ul style=\"display: block;\">");
                        html.AppendLine(GetMenuOptions(lsthijos));   
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
            html.AppendLine("<li class=\"nav-header\">Menu</li>");

            List<BusinessObjects.Menu> lst = MenuBLL.GetAll(WhereClause, OrderByClause);
             
            //WhereClause = "PADRE IS NULL";
            //List<BusinessObjects.Menu> lst = MenuBLL.GetAll(WhereClause, OrderByClause);
            html.AppendLine(GetMenuOptions(lst,null,false));  
        
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
            return ShowObject(new BusinessObjects.Menu());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            BusinessObjects.Menu obj = new BusinessObjects.Menu();
            obj.men_id  = (int)id;
            obj.men_id_key = (int)id;
            obj = MenuBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
            //return ShowObject(obj);

        }


        public static BusinessObjects.Menu GetObjeto(object objeto)
        {
            BusinessObjects.Menu obj = new BusinessObjects.Menu();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object codigo = null;
                object codigokey = null;
                object nombre = null;
                object formulario = null;
                object imagen = null;                
             //   object activo = null;
                tmp.TryGetValue("men_id", out codigo);
                tmp.TryGetValue("men_id_key", out codigokey);
                tmp.TryGetValue("men_nombre", out nombre);
                tmp.TryGetValue("men_formulario", out formulario);
                tmp.TryGetValue("men_imagen", out imagen);
               // tmp.TryGetValue("men_estado", out activo);
                if (codigo != null)
                {
                    obj.men_id = int.Parse(codigo.ToString());
                    obj.men_id_key = int.Parse(codigokey.ToString());
                }
                obj.men_nombre = (string)nombre;
                obj.men_formulario = (string)formulario;
                obj.men_imagen = (string)imagen;
             //   obj.men_estado = (int?)activo;
                obj.crea_usr = "admin";
                obj.crea_fecha = DateTime.Now;
                obj.mod_usr = "admin";
                obj.mod_fecha = DateTime.Now;

            }

            return obj;
        }


        [WebMethod]
        public static string GetSearch()
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"pull-left\">");



            html.AppendLine(new Input { id = "txtFORMULARIO_S", placeholder = "Codigo", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE_S", placeholder = "Nombre", clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbESTADO_S", clase = Css.medium, diccionario = Dictionaries.GetEstado() }.ToString());

            html.AppendLine("</div>");
            html.AppendLine("<div class=\"pull-right\">");
            html.AppendLine(new Boton { refresh=true }.ToString());
            html.AppendLine(new Boton { clean = true }.ToString());
            html.AppendLine("</div>");
            return html.ToString();
        }

        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new BusinessObjects.Menu());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            BusinessObjects.Menu obj = new BusinessObjects.Menu(objeto);
            obj.men_id_key = obj.men_id;

            BusinessObjects.Menu opt = new BusinessObjects.Menu();
            opt.men_id = obj.men_id;
            opt.men_id_key = obj.men_id_key;
            opt= MenuBLL.GetByPK(opt);
            opt.men_nombre = obj.men_nombre;
            opt.men_formulario = obj.men_formulario;
            opt.men_imagen = obj.men_imagen;
         //   opt.men_estado = obj.men_estado;

               if (MenuBLL.Update(opt) > 0)
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
          
            BusinessObjects.Menu obj = new BusinessObjects.Menu(objeto);
            obj.men_id_key = obj.men_id;
            if (MenuBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }

     


        [WebMethod]
        public static string AddOption()
        {

            int max = MenuBLL.GetMax("men_id") + 1;
            

            BusinessObjects.Menu obj = new BusinessObjects.Menu();
            obj.men_nombre = "Nueva Opción";
            obj.men_orden= max;
            obj.men_estado = 1;
            if (MenuBLL.Insert(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";            

        }

        [WebMethod]
        public static string AddChildOption(object id)
        {            
            BusinessObjects.Menu parentobj = new BusinessObjects.Menu();
            parentobj.men_id = (int)id;
            parentobj.men_id_key = (int)id;
            parentobj = MenuBLL.GetByPK(parentobj);



            int max = MenuBLL.GetMax("men_id") + 1;


            BusinessObjects.Menu obj = new BusinessObjects.Menu();
            obj.men_nombre = "Nueva Opción";
            obj.men_orden= max;
            obj.men_padre = int.Parse(id.ToString());  
            obj.men_estado = 1;
            if (MenuBLL.Insert(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";

        }
    }
}