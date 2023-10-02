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
    public partial class wfCtipocom : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "cti_codigo";
        protected static string WhereClause = "";
        protected static WhereParams parametros;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
            }
        }


        public static string ShowData(Ctipocom obj)
        {
            ArrayList array = new ArrayList();
            string aux="";
            if (obj.cti_tipo==0)
                 aux="Tipo: Secuencial";
            if (obj.cti_tipo==1)
                aux="Tipo: Periodo";
            array.Add(aux);
            return new HtmlObjects.ListItem { titulo = new string[] { obj.cti_id, "-", obj.cti_nombre }, descripcion = new string[] { aux }, logico = new LogicItem[] { new LogicItem("Activos", obj.cti_estado) } }.ToString();              
          
        }

        public static string ShowObject(Ctipocom obj)
        {

            StringBuilder html = new StringBuilder();            
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.cti_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.cti_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.cti_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.cti_nombre, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Select { id = "cmbTIPO", etiqueta = "Tipo", valor = obj.cti_tipo.ToString(), clase = Css.large, diccionario = Dictionaries.GetTipoCtipocom(), obligatorio = true }.ToString());
            html.AppendLine(new Check { id = "chkAUTORIZA", etiqueta = "Autoriza", valor = obj.cti_autoriza }.ToString());
            html.AppendLine(new Select { id = "cmbRETDATO", etiqueta = "Retdato", valor = obj.cti_retdato.ToString(), clase = Css.large, diccionario = Dictionaries.GetRetdato() }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo", valor = obj.cti_estado }.ToString());
            html.AppendLine(new Boton {click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Ctipocom obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();



            if (obj.cti_codigo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cti_codigo = {" + contador + "} ";
                valores.Add(obj.cti_codigo);
                contador++;
            }

            if (!string.IsNullOrEmpty(obj.cti_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cti_nombre like {" + contador + "} ";
                valores.Add("%" + obj.cti_nombre + "%");
                contador++;

            }

            if (!string.IsNullOrEmpty(obj.cti_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cti_id like {" + contador + "} ";
                valores.Add("%" + obj.cti_id + "%");
                contador++;

            }
            if (obj.cti_estado.HasValue)
            {

                parametros.where += ((parametros.where != "") ? " and " : "") + " cti_estado = {" + contador + "} ";
                valores.Add(obj.cti_estado.Value);
                contador++;

            }
            if (obj.cti_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cti_empresa = {" + contador + "} ";
                valores.Add(obj.cti_empresa);
                contador++;

            }
            parametros.valores = valores.ToArray();

        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Ctipocom(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Ctipocom> lst = CtipocomBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Ctipocom obj in lst)
            {
                string id = "{\"cti_codigo\":\"" + obj.cti_codigo + "\", \"cti_empresa\":\"" + obj.cti_empresa + "\"}";//ID COMPUESTO
          
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
            return ShowObject(new Ctipocom());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            

            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("cti_codigo", out codigo);
            tmp.TryGetValue("cti_empresa", out empresa);

            Ctipocom obj = new Ctipocom();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.cti_empresa = int.Parse(empresa.ToString());
                obj.cti_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.cti_codigo = int.Parse(codigo.ToString());
                obj.cti_codigo_key = int.Parse(codigo.ToString());
            }

            obj = CtipocomBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);










        }


       


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
            html.AppendLine(new Boton { refresh=true }.ToString());
            html.AppendLine(new Boton { clean = true }.ToString());
            html.AppendLine("</div>");
            return html.ToString();
        }

        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Ctipocom());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Ctipocom obj= new Ctipocom(objeto);
            obj.cti_codigo_key = obj.cti_codigo;
            obj.cti_empresa_key = obj.cti_empresa;
            if (obj.cti_codigo_key > 0)
            {
                if (CtipocomBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (CtipocomBLL.Insert(obj) > 0)
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
            Ctipocom obj = new Ctipocom(objeto);
            obj.cti_codigo_key = obj.cti_codigo;
            obj.cti_empresa_key = obj.cti_empresa;
            if (CtipocomBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";

        }
    }
}