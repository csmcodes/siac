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

    public partial class wfUmedida : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "umd_codigo";
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


        public static string ShowData(Umedida obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.umd_id, "-", obj.umd_nombre }, descripcion = new string[] { }, logico = new LogicItem[] { new LogicItem("Activos", obj.umd_estado) } }.ToString();
        }

        public static string ShowObject(Umedida obj)
        {

            StringBuilder html = new StringBuilder();


            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.umd_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.umd_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.umd_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.umd_nombre, clase = Css.large, obligatorio = true }.ToString());

            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.umd_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());


            return html.ToString();
        }


        public static void SetWhereClause(Umedida obj)
        {
            int contador = 0;
            parametros = new WhereParams(); 
            List<object> valores= new List<object>();  

            if (obj.umd_codigo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " umd_codigo = {" + contador + "} ";
                valores.Add(obj.umd_codigo);  
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.umd_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " umd_nombre like {" + contador + "} ";
                valores.Add("%" + obj.umd_nombre + "%");
                contador++;                
            }
            if (!string.IsNullOrEmpty(obj.umd_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " umd_id like {" + contador + "} ";
                valores.Add("%" + obj.umd_id + "%");
                contador++;                                
            }
            
            if (obj.umd_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " umd_estado = {" + contador + "} ";
                valores.Add(obj.umd_estado.Value);
                contador++;
                
            }
            if (obj.umd_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " umd_empresa = {" + contador + "} ";
                valores.Add(obj.umd_empresa);
                contador++;

            }
            parametros.valores = valores.ToArray(); 



     
            
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Umedida(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Umedida> lst = UmedidaBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Umedida obj in lst)
            {
                string id = "{\"umd_codigo\":\"" + obj.umd_codigo + "\", \"umd_empresa\":\"" + obj.umd_empresa + "\"}";//ID COMPUESTO
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
            return ShowObject(new Umedida());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("umd_codigo", out codigo);
            tmp.TryGetValue("umd_empresa", out empresa);

            Umedida obj = new Umedida();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.umd_empresa = int.Parse(empresa.ToString());
                obj.umd_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.umd_codigo = int.Parse(codigo.ToString());
                obj.umd_codigo_key = int.Parse(codigo.ToString());
            }

            obj = UmedidaBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Umedida GetObjeto(object objeto)
        //{
        //    Umedida obj = new Umedida();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object empresa= null;
        //        object empresakey = null;
        //        object codigokey= null;
        //        object id= null;
        //        object nombre= null;
              
        //        object activo = null;

        //        tmp.TryGetValue("umd_codigo", out codigo);
        //        tmp.TryGetValue("umd_empresa", out empresa);
        //        tmp.TryGetValue("umd_codigo_key", out codigokey);
        //        tmp.TryGetValue("umd_empresa_key", out empresakey);
        //        tmp.TryGetValue("umd_id", out id);
        //        tmp.TryGetValue("umd_nombre", out nombre);
                
        //        tmp.TryGetValue("umd_estado", out activo);
        //        if (empresa != null && !empresa.Equals(""))
        //        {
        //            obj.umd_empresa = int.Parse(empresa.ToString());
        //            obj.umd_empresa_key = int.Parse(empresa.ToString());
        //        }
        //        if (codigo != null && !codigo.Equals(""))
        //        {
        //            obj.umd_codigo = int.Parse(codigo.ToString());
        //            obj.umd_codigo_key = int.Parse(codigo.ToString());
        //        }

        //        obj.umd_id = (string)id;
        //        obj.umd_nombre = (string)nombre;
               
        //        obj.umd_estado = (int?)activo;
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
            return ShowObject(new Umedida());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Umedida obj = new Umedida(objeto);
            obj.umd_codigo_key = obj.umd_codigo;
            obj.umd_empresa_key = obj.umd_empresa;
          
            if (obj.umd_codigo_key > 0)
            {
                if (UmedidaBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (UmedidaBLL.Insert(obj) > 0)
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
       

            Umedida obj = new Umedida(objeto);
            obj.umd_codigo_key = obj.umd_codigo_key;
            obj.umd_empresa_key = obj.umd_empresa_key;

            if (UmedidaBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}