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
    public partial class wfListaprecio : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "lpr_codigo";
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


        public static string ShowData(Listaprecio obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.lpr_id, "-", obj.lpr_nombre }, descripcion = new string[] { }, logico = new LogicItem[] { new LogicItem("Activos", obj.lpr_estado) } }.ToString();              
        }

        public static string ShowObject(Listaprecio obj)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.lpr_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.lpr_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.lpr_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.lpr_nombre, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Select { id = "cmbMONEDA", etiqueta = "Moneda", valor = obj.lpr_moneda.ToString(), clase = Css.large, diccionario = Dictionaries.GetMoneda() }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo", valor = obj.lpr_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Listaprecio obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();

            if (obj.lpr_codigo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " lpr_codigo = {" + contador + "} ";
                valores.Add(obj.lpr_codigo);
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.lpr_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " lpr_nombre like {" + contador + "} ";
                valores.Add("%" + obj.lpr_nombre + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.lpr_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " lpr_id like {" + contador + "} ";
                valores.Add("%" + obj.lpr_id + "%");
                contador++;
            }
           
            
            if (obj.lpr_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " lpr_estado = {" + contador + "} ";
                valores.Add(obj.lpr_estado.Value);
                contador++;

            }


            if (obj.lpr_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " lpr_empresa = {" + contador + "} ";
                valores.Add(obj.lpr_empresa);
                contador++;

            }
            parametros.valores = valores.ToArray();





        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Listaprecio(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Listaprecio> lst = ListaprecioBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Listaprecio obj in lst)
            {
                string id = "{\"lpr_codigo\":\"" + obj.lpr_codigo + "\", \"lpr_empresa\":\"" + obj.lpr_empresa + "\"}";//ID COMPUESTO
               
                html.AppendLine(new HtmlList { id = id, content = ShowData(obj) }.ToString());
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
            return ShowObject(new Listaprecio());
        }


        [WebMethod]
        public static string GetObject(object id)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("lpr_codigo", out codigo);
            tmp.TryGetValue("lpr_empresa", out empresa);
            Listaprecio obj = new Listaprecio();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.lpr_empresa = int.Parse(empresa.ToString());
                obj.lpr_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.lpr_codigo = int.Parse(codigo.ToString());
                obj.lpr_codigo_key = int.Parse(codigo.ToString());
            }

            obj = ListaprecioBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Listaprecio GetObjeto(object objeto)
        //{
        //    Listaprecio obj = new Listaprecio();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object empresa = null;
        //        object empresakey = null;
        //        object codigokey = null;
        //        object id = null;
        //        object nombre = null;               
        //        object moneda = null;
        //        object activo = null;
        //        tmp.TryGetValue("lpr_codigo", out codigo);
        //        tmp.TryGetValue("lpr_empresa", out empresa);
        //        tmp.TryGetValue("lpr_codigo_key", out codigokey);
        //        tmp.TryGetValue("lpr_empresa_key", out empresakey);
        //        tmp.TryGetValue("lpr_id", out id);
        //        tmp.TryGetValue("lpr_nombre", out nombre);
        //        tmp.TryGetValue("lpr_moneda", out moneda);
        //        tmp.TryGetValue("lpr_estado", out activo);
        //        if (empresa != null && !empresa.Equals(""))
        //        {
        //            obj.lpr_empresa = int.Parse(empresa.ToString());
        //            obj.lpr_empresa_key = int.Parse(empresa.ToString());
        //        }
        //        if (codigo != null && !codigo.Equals(""))
        //        {
        //            obj.lpr_codigo = int.Parse(codigo.ToString());
        //            obj.lpr_codigo_key = int.Parse(codigo.ToString());
        //        }

        //        obj.lpr_id = (string)id;
        //        obj.lpr_nombre = (string)nombre;
        //        obj.lpr_moneda = Convert.ToInt32(moneda);
        //        obj.lpr_estado = (int?)activo;
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

            html.AppendLine(new Input { id = "txtCODIGO_S", placeholder = "Codigo", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE_S", placeholder = "Nombre", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtID_S", placeholder = "Id", clase = Css.large }.ToString());
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
            return ShowObject(new Listaprecio());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Listaprecio obj = new Listaprecio(objeto);
            obj.lpr_codigo_key = obj.lpr_codigo;
            obj.lpr_empresa_key = obj.lpr_empresa;
            if (obj.lpr_codigo_key > 0)
            {
                if (ListaprecioBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (ListaprecioBLL.Insert(obj) > 0)
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
            Listaprecio obj = new Listaprecio(objeto);
            obj.lpr_codigo_key = obj.lpr_codigo;
            obj.lpr_empresa_key = obj.lpr_empresa;
            if (ListaprecioBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}