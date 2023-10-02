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
    public partial class wfFormulario : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "for_codigo";
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


        public static string ShowData(Formulario obj)
        {           
            return new HtmlObjects.ListItem { titulo = new string[] { obj.for_id }, descripcion = new string[] { obj.for_nombre }, logico = new LogicItem[] { new LogicItem("Activos", obj.for_estado) } }.ToString();              
        }

        public static string ShowObject(Formulario obj)
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.for_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.for_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.for_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.for_nombre, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtDESCRIPCION", etiqueta = "Descripcion", placeholder = "Descripcion", valor = obj.for_descripcion, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtAYUDA", etiqueta = "Ayuda", placeholder = "Ayuda", valor = obj.for_ayuda, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtCLASE", etiqueta = "Clase", placeholder = "Clase", valor = obj.for_clase, clase = Css.large }.ToString());

            html.AppendLine(new Select { id = "cmbMODULO", etiqueta = "Modulo", valor = obj.for_modulo.ToString(), clase = Css.large, diccionario = Dictionaries.GetModulos(), obligatorio = true }.ToString());

            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.for_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Formulario obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();

            if (obj.for_codigo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " for_codigo = {" + contador + "} ";
                valores.Add(obj.for_codigo);
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.for_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " for_nombre like {" + contador + "} ";
                valores.Add("%" + obj.for_nombre + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.for_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " for_id like {" + contador + "} ";
                valores.Add("%" + obj.for_id + "%");
                contador++;
            }
         
            if (obj.for_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " for_estado = {" + contador + "} ";
                valores.Add(obj.for_estado.Value);
                contador++;

            }
            parametros.valores = valores.ToArray();





        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Formulario(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Formulario> lst = FormularioBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Formulario obj in lst)
            {
               // string id = "{\"for_codigo\":\"" + obj.for_codigo + "\", \"for_empresa\":\"" + obj.for_empresa + "\"}";//ID COMPUESTO
              //  html.AppendLine(HtmlElements.HtmlList(obj.for_codigo.ToString(), ShowData(obj)));
                html.AppendLine(new HtmlList { id = obj.for_codigo.ToString(), content = ShowData(obj) }.ToString());
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
            return ShowObject(new Formulario());
        }


        [WebMethod]
        public static string GetObject(string id)
        {
            Formulario obj = new Formulario();
            if (id != null && !id.Equals(""))
            {
                obj.for_codigo = int.Parse(id);
                obj.for_codigo_key = int.Parse(id);
            }

            obj = FormularioBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Formulario GetObjeto(object objeto)
        //{
        //    Formulario obj = new Formulario();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
               
        //        object codigokey = null;
        //        object id = null;
        //        object nombre = null;
        //        object descripcion = null;
        //        object ayuda = null;
        //        object clase = null;
        //        object modulo = null;
        //        object activo = null;
        //        tmp.TryGetValue("for_codigo", out codigo);                
        //        tmp.TryGetValue("for_codigo_key", out codigokey);              
        //        tmp.TryGetValue("for_id", out id);
        //        tmp.TryGetValue("for_nombre", out nombre);
        //        tmp.TryGetValue("for_descripcion", out descripcion);
        //        tmp.TryGetValue("for_ayuda", out ayuda);
        //        tmp.TryGetValue("for_clase", out clase);
        //        tmp.TryGetValue("for_modulo", out modulo);                
        //        tmp.TryGetValue("for_estado", out activo);
               
        //        if (codigo != null && !codigo.Equals(""))
        //        {
        //            obj.for_codigo = int.Parse(codigo.ToString());                    
        //        }
        //        if (codigokey != null && !codigokey.Equals(""))
        //        {
        //            obj.for_codigo_key = int.Parse(codigokey.ToString());
        //        }

        //        obj.for_id = (string)id;
        //        obj.for_nombre = (string)nombre;
        //        obj.for_descripcion = (string)descripcion;
        //        obj.for_ayuda = (string)ayuda;
        //        obj.for_clase = (string)clase;
        //        obj.for_modulo = Convert.ToInt32(modulo);
        //        obj.for_estado = (int?)activo;
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
            //     html.AppendLine(HtmlElements.Input("Id", "txtCODIGO_S", "", HtmlElements.small, false));
           
            
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
            return ShowObject(new Formulario());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Formulario obj = new Formulario(objeto);
            obj.for_codigo_key = obj.for_codigo;
          
            if (obj.for_codigo_key > 0)
            {
                if (FormularioBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (FormularioBLL.Insert(obj) > 0)
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
            Formulario obj = new Formulario(objeto);
            obj.for_codigo_key = obj.for_codigo;
           
            if (FormularioBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}