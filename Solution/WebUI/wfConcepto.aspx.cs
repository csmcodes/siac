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
    public partial class wfConcepto : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "con_codigo";
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


        public static string ShowData(Concepto obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.con_id }, descripcion = new string[] { obj.con_nombre }, logico = new LogicItem[] { new LogicItem("Activos", obj.con_estado) } }.ToString();              
        }

        public static string ShowObject(Concepto obj)
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.con_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.con_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.con_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.con_nombre, clase = Css.large, obligatorio = true }.ToString());
           html.AppendLine(new Input { id = "txtTIPO", etiqueta = "Tipo", placeholder = "Tipo", valor = obj.con_tipo, clase = Css.large }.ToString());
          //  html.AppendLine(new Select { id = "txtTIPO", etiqueta = "Tipo", placeholder = "Tipo", valor = obj.con_tipo, clase = Css.large }.ToString());
        //    html.AppendLine(new Select { id = "txtTIPO", diccionario = Dictionaries.GetGenCuenta(), etiqueta = "Tipo", clase = Css.small }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.con_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Concepto obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();

            if (obj.con_codigo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " con_codigo = {" + contador + "} ";
                valores.Add(obj.con_codigo);
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.con_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " con_nombre like {" + contador + "} ";
                valores.Add("%" + obj.con_nombre + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.con_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " con_id like {" + contador + "} ";
                valores.Add("%" + obj.con_id + "%");
                contador++;
            }
            if (obj.con_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " con_empresa = {" + contador + "} ";
                valores.Add(obj.con_empresa);
                contador++;

            }
            parametros.valores = valores.ToArray();





        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Concepto(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Concepto> lst = ConceptoBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Concepto obj in lst)
            {
                string id = "{\"con_codigo\":\"" + obj.con_codigo + "\", \"con_empresa\":\"" + obj.con_empresa + "\"}";//ID COMPUESTO
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
            return ShowObject(new Concepto());
        }


        [WebMethod]
        public static string GetObject(object id)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("con_codigo", out codigo);
            tmp.TryGetValue("con_empresa", out empresa);

            Concepto obj = new Concepto();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.con_empresa = int.Parse(empresa.ToString());
                obj.con_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.con_codigo = int.Parse(codigo.ToString());
                obj.con_codigo_key = int.Parse(codigo.ToString());
            }

            obj = ConceptoBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Concepto GetObjeto(object objeto)
        //{
        //    Concepto obj = new Concepto();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object empresa = null;
        //        object empresakey = null;
        //        object codigokey = null;
        //        object id = null;
        //        object nombre = null;
        //        object tipo = null;
               
        //        object activo = null;

        //        tmp.TryGetValue("con_codigo", out codigo);
        //        tmp.TryGetValue("con_empresa", out empresa);
        //        tmp.TryGetValue("con_codigo_key", out codigokey);
        //        tmp.TryGetValue("con_empresa_key", out empresakey);
        //        tmp.TryGetValue("con_id", out id);
        //        tmp.TryGetValue("con_nombre", out nombre);
        //        tmp.TryGetValue("con_tipo", out tipo);
               
        //        tmp.TryGetValue("con_estado", out activo);
        //        if (empresa != null && !empresa.Equals(""))
        //        {
        //            obj.con_empresa = int.Parse(empresa.ToString());
        //        }
        //        if (empresakey != null && !empresakey.Equals(""))
        //        {
        //            obj.con_empresa_key = int.Parse(empresakey.ToString());
        //        }
        //        if (codigo != null && !codigo.Equals(""))
        //        {
        //            obj.con_codigo = int.Parse(codigo.ToString());
        //        }
        //        if (codigokey != null && !codigokey.Equals(""))
        //        {
        //            obj.con_codigo_key = int.Parse(codigokey.ToString());
        //        }

        //        obj.con_id = (string)id;
        //        obj.con_nombre = (string)nombre;
        //        obj.con_tipo = (string)tipo;
                
        //        obj.con_estado = (int?)activo;
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
            return ShowObject(new Concepto());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Concepto obj = new Concepto(objeto);
            obj.con_codigo_key = obj.con_codigo;
            obj.con_empresa_key = obj.con_empresa;
            
            if (obj.con_codigo_key > 0)
            {
                if (ConceptoBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (ConceptoBLL.Insert(obj) > 0)
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
            Concepto obj = new Concepto(objeto);
            obj.con_codigo_key = obj.con_codigo;
            obj.con_empresa_key = obj.con_empresa;
            
            if (ConceptoBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}