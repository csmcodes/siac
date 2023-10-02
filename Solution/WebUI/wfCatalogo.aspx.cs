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



namespace WebUI
{
    public partial class wfCatalogo : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "cat_codigo";
        protected static string WhereClause = "";
        protected static List<Catalogo> lstcatalogo = new List<Catalogo>();
        protected static Dictionary<string, string> dicCatalogo = new Dictionary<string, string>();
        protected static WhereParams parametros = new WhereParams();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
                lstcatalogo = CatalogoBLL.GetAll("", "");
                dicCatalogo = lstcatalogo.ToDictionary(p => p.cat_codigo.ToString(), p => p.cat_nombre);
            }
        }


        public static string ShowData(Catalogo obj)
        {
           
            return new HtmlObjects.ListItem { titulo = new string[] { obj.cat_nombre }, descripcion = new string[] {  }, logico = new LogicItem[] { new LogicItem("Activos", obj.cat_estado) } }.ToString();              
        }

        public static string ShowObject(Catalogo obj)
        {
            Catalogo padre = new Catalogo();
            padre.cat_codigo_key = obj.cat_padre??0;
            padre = CatalogoBLL.GetByPK(padre);

            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.cat_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.cat_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.cat_nombre, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtTIPO", etiqueta = "Tipo", placeholder = "Tipo", valor = obj.cat_tipo, clase = Css.large }.ToString());
     //       html.AppendLine(new Input { id = "cmbPADRE", etiqueta = "Padre", placeholder = "Padre", valor = obj.cat_padre.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtCODPADRE", etiqueta = "Padre", valor = padre.cat_nombre, autocomplete = "GetCatalogoObj",  clase = Css.medium, placeholder = "Padre" }.ToString() + " " + new Input { id = "cmbPADRE", visible = false, valor = obj.cat_padre }.ToString());
            // new Input() { id = "txtIDPRO", placeholder = "ID", autocomplete = "GetProductoObj", valor = dlistaprecio.dlpr_nombreproducto, clase = Css.blocklevel, habilitado = (dlistaprecio.dlpr_empresa > 0) ? false : true }.ToString() + new Input() { id = "cmbPRODUCTO", valor = dlistaprecio.dlpr_producto, visible = false }.ToString();
            
            
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.cat_estado }.ToString());

            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();

        }


        public static void SetWhereClause(Catalogo obj)
        {

            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();  
           
            if (obj.cat_codigo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cat_codigo = {" + contador + "} ";
                valores.Add(obj.cat_codigo);
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.cat_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cat_nombre like {" + contador + "} ";
                valores.Add("%" + obj.cat_nombre + "%");
                contador++;
            }

            if (!string.IsNullOrEmpty(obj.cat_tipo))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cat_tipo like {" + contador + "} ";
                valores.Add("%" + obj.cat_tipo + "%");
                contador++;
            }
            if (obj.cat_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cat_estado = {" + contador + "} ";
                valores.Add(obj.cat_estado.Value);
                contador++;

            }
            parametros.valores = valores.ToArray();      
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Catalogo(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Catalogo> lst = CatalogoBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Catalogo obj in lst)
            {
              
                html.AppendLine(new HtmlList { id = obj.cat_codigo.ToString(), content = ShowData(obj) }.ToString());
            }

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
            return ShowObject(new Catalogo());
        }


        [WebMethod]
        public static string GetObject(string id)
        {
            Catalogo obj = new Catalogo();
            obj.cat_codigo = int.Parse(id.ToString());
            obj.cat_codigo_key = int.Parse(id.ToString());
            obj = CatalogoBLL.GetByPK(obj);

            Catalogo padre = new Catalogo();
            padre.cat_codigo_key = obj.cat_padre ?? 0;
            padre = CatalogoBLL.GetByPK(padre);
            obj.cat_padre_nombre = padre.cat_nombre;
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Catalogo GetObjeto(object objeto)
        //{
        //    Catalogo obj = new Catalogo();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object codigokey = null;
        //        object nombre = null;
        //        object padre = null;
        //        object id = null;
        //        object activo = null;
        //        tmp.TryGetValue("cat_codigo", out codigo);
        //        tmp.TryGetValue("cat_codigo_key", out codigokey);
        //        tmp.TryGetValue("cat_tipo", out id);
        //        tmp.TryGetValue("cat_nombre", out nombre);
        //        tmp.TryGetValue("cat_padre", out padre);
        //        tmp.TryGetValue("cat_estado", out activo);
        //        if (codigo != null && !codigo.Equals(""))
        //        {
        //            obj.cat_codigo = int.Parse(codigo.ToString());

        //        }
        //        if (codigokey != null && !codigokey.Equals(""))
        //        {
        //            obj.cat_codigo_key = int.Parse(codigokey.ToString()); ;

        //        }
        //        if (padre != null && !padre.Equals(""))
        //        {
        //            obj.cat_padre = int.Parse(padre.ToString()); ;

        //        }
        //        obj.cat_nombre = (string)nombre;
        //        obj.cat_tipo = (string)id;
              
        //        obj.cat_estado = (int?)activo;
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
            html.AppendLine("<div class='pull-left'>");              
            html.AppendLine(new Input { id = "txtNOMBRE_S", placeholder = "Nombre", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtTIPO_S", placeholder = "Tipo", clase = Css.large }.ToString());            
            html.AppendLine(new Select { id = "cmbESTADO_S", clase = Css.medium, diccionario = Dictionaries.GetEstado() }.ToString());
            html.AppendLine("</div>");
            html.AppendLine("<div class='pull-right'>");
            html.AppendLine(new Boton { refresh=true }.ToString());
            html.AppendLine(new Boton { clean = true }.ToString());
            html.AppendLine("</div>");
            return html.ToString();
        }

        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Catalogo());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Catalogo obj = new Catalogo(objeto);
            obj.cat_codigo_key = obj.cat_codigo;
            
            if (obj.cat_codigo_key > 0)
            {
                if (CatalogoBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (CatalogoBLL.Insert(obj) > 0)
                {
                    return "OK";
                }
                else
                    return "ERROR";
            }

        }

        [WebMethod]
        public static string DeleteObject(object objeto)
        {
            Catalogo obj = new Catalogo(objeto);
            obj.cat_codigo_key = obj.cat_codigo;
            if (CatalogoBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}