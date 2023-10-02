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
    public partial class wfCatcliente : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "cat_nombre";
        protected static string WhereClause = "";
        protected static WhereParams parametros = new WhereParams();
        protected static int? tclipro; 
        
        
         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
                txttclipro.Text = (Request.QueryString["tclipro"] != null) ? Request.QueryString["tclipro"].ToString() : Constantes.cCliente + "";
                tclipro = Convert.ToInt32(txttclipro.Text);
            }
        }


        public static string ShowData(Catcliente obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.cat_nombre }, descripcion = new string[] {  }, logico = new LogicItem[] { new LogicItem("Activos", obj.cat_estado) } }.ToString();
        }

        public static string ShowObject(Catcliente obj)
        {  

          
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.cat_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.cat_codigo_key.ToString(), visible = false }.ToString());            
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.cat_nombre, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.cat_id, clase = Css.large }.ToString());         
            //html.AppendLine(new Select { id = "cmbTIPO", etiqueta = "TIPO", valor = obj.cat_tipo.ToString(), clase = Css.large, diccionario = Dictionaries.GetTipoPersonas() }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.cat_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Catcliente obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (!string.IsNullOrEmpty(obj.cat_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cat_nombre like {" + contador + "} ";
                valores.Add("%" + obj.cat_nombre + "%");
                contador++;
            }

            if (obj.cat_tipo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cat_tipo = {" + contador + "} ";
                valores.Add(obj.cat_tipo);
                contador++;
            }
            if (obj.cat_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cat_estado = {" + contador + "} ";
                valores.Add(obj.cat_estado.Value);
                contador++;
            }
            if (obj.cat_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cat_empresa = {" + contador + "} ";
                valores.Add(obj.cat_empresa);
                contador++;
            }
            parametros.valores = valores.ToArray();
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Catcliente(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Catcliente> lst = CatclienteBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Catcliente obj in lst)
            {
                string id = "{\"cat_codigo\":\"" + obj.cat_codigo + "\", \"cat_empresa\":\"" + obj.cat_empresa + "\"}";//ID COMPUESTO
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
            return ShowObject(new Catcliente());
        }

        [WebMethod]
        public static string GetObject(object id)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("cat_codigo", out codigo);
            tmp.TryGetValue("cat_empresa", out empresa);
            Catcliente obj = new Catcliente();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.cat_empresa = int.Parse(empresa.ToString());
                obj.cat_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.cat_codigo = int.Parse(codigo.ToString());
                obj.cat_codigo_key = int.Parse(codigo.ToString());
            }
            obj = CatclienteBLL.GetByPK(obj);

            Cuenta cuenta = new Cuenta();
            cuenta.cue_empresa = obj.cat_empresa;
            cuenta.cue_empresa_key = obj.cat_empresa;
     
            
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Catcliente GetObjeto(object objeto)
        //{
        //    Catcliente obj = new Catcliente();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object codigokey = null;
        //        object empresa = null;
        //        object empresakey = null;
        //        object tipo = null;
        //        object id = null;
        //        object nombre = null;
               
                
        //        object activo = null;
               
        //        tmp.TryGetValue("cat_codigo", out codigo);
        //        tmp.TryGetValue("cat_codigo_key", out codigokey);
        //        tmp.TryGetValue("cat_empresa", out empresa);
        //        tmp.TryGetValue("cat_empresa_key", out empresakey);
        //        tmp.TryGetValue("cat_id", out id);
        //        tmp.TryGetValue("cat_tipo", out tipo);
              
        //        tmp.TryGetValue("cat_nombre", out nombre);
              
        //        tmp.TryGetValue("cat_estado", out activo);
                
        //        if (codigo != null && !codigo.Equals(""))
        //        {
        //            obj.cat_codigo = int.Parse(codigo.ToString());
        //        }
        //        if (codigokey != null && !codigokey.Equals(""))
        //        {
        //            obj.cat_codigo_key = int.Parse(codigo.ToString());
        //        }
        //        if (empresa != null && !empresa.Equals(""))
        //        {
        //            obj.cat_empresa = int.Parse(empresa.ToString());
        //        }
        //        if (empresakey != null && !empresakey.Equals(""))
        //        {
        //            obj.cat_empresa_key = int.Parse(empresakey.ToString());
        //        }       
        //        obj.cat_id = (string)id;           
        //        obj.cat_nombre = (string)nombre;             
        //        obj.cat_tipo = Convert.ToInt32(tipo);
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
            return ShowObject(new Catcliente());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Catcliente obj = new Catcliente(objeto);
            obj.cat_codigo_key = obj.cat_codigo;
            obj.cat_empresa_key = obj.cat_empresa;
      
            if (obj.cat_codigo_key > 0)
            {
                if (CatclienteBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (CatclienteBLL.Insert(obj) > 0)
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
            Catcliente obj = new Catcliente(objeto);
            obj.cat_codigo_key = obj.cat_codigo;
            obj.cat_empresa_key = obj.cat_empresa;
  
            if (CatclienteBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}