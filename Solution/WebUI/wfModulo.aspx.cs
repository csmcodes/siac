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

    public partial class wfModulo : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "mod_codigo";
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


        public static string ShowData(Modulo obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.mod_id, "-", obj.mod_nombre }, descripcion = new string[] { }, logico = new LogicItem[] { new LogicItem("Activos", obj.mod_estado) } }.ToString();
        }

        public static string ShowObject(Modulo obj)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.mod_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.mod_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.mod_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.mod_nombre, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.mod_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Modulo obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (obj.mod_codigo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " mod_codigo = {" + contador + "} ";
                valores.Add(obj.mod_codigo);
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.mod_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " mod_nombre like {" + contador + "} ";
                valores.Add("%" + obj.mod_nombre + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.mod_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " mod_id like {" + contador + "} ";
                valores.Add("%" + obj.mod_id + "%");
                contador++;
            }
            if (obj.mod_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " mod_estado = {" + contador + "} ";
                valores.Add(obj.mod_estado.Value);
                contador++;
            }
            parametros.valores = valores.ToArray();
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Modulo(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Modulo> lst = ModuloBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Modulo obj in lst)
            {
                //    string id = "{\"mod_codigo\":\"" + obj.mod_codigo + "\", \"mod_empresa\":\"" + obj.mod_empresa + "\"}";//ID COMPUESTO
                //   html.AppendLine(HtmlElements.HtmlList(obj.mod_codigo.ToString(), ShowData(obj)));
                html.AppendLine(new HtmlList { id = obj.mod_codigo.ToString(), content = ShowData(obj) }.ToString());
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
            return ShowObject(new Modulo());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            Modulo obj = new Modulo();
            if (id != null && !id.Equals(""))
            {
                obj.mod_codigo = int.Parse(id.ToString());
                obj.mod_codigo_key = int.Parse(id.ToString());
            }
            obj = ModuloBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Modulo GetObjeto(object objeto)
        //{
        //    Modulo obj = new Modulo();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object codigokey = null;
        //        object id = null;
        //        object nombre = null;
        //        object activo = null;
        //        tmp.TryGetValue("mod_codigo", out codigo);
        //        tmp.TryGetValue("mod_codigo_key", out codigokey);
        //        tmp.TryGetValue("mod_id", out id);
        //        tmp.TryGetValue("mod_nombre", out nombre);
        //        tmp.TryGetValue("mod_estado", out activo);
        //        if (codigo != null && !codigo.Equals(""))
        //        {
        //            obj.mod_codigo = int.Parse(codigo.ToString());
        //            obj.mod_codigo_key = int.Parse(codigo.ToString());
        //        }
        //        obj.mod_id = (string)id;
        //        obj.mod_nombre = (string)nombre;
        //        obj.mod_estado = (int?)activo;
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
            html.AppendLine(new Input { id = "txtID_S", placeholder = "Id", clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbESTADO_S", clase = Css.medium, diccionario = Dictionaries.GetEstado() }.ToString());
            html.AppendLine("</div>");
            html.AppendLine("<div class='pull-right'>");
            html.AppendLine(new Boton { refresh = true }.ToString());
            html.AppendLine(new Boton { clean = true }.ToString());
            html.AppendLine("</div>");
            return html.ToString();
        }

        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Modulo());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Modulo obj = new Modulo(objeto);
            obj.mod_codigo_key = obj.mod_codigo;
            if (obj.mod_codigo_key > 0)
            {
                if (ModuloBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (ModuloBLL.Insert(obj) > 0)
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
            Modulo obj = new Modulo(objeto);
            obj.mod_codigo_key = obj.mod_codigo;
            


            if (ModuloBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}