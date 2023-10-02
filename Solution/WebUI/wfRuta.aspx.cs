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
using Functions;

namespace WebUI
{
    public partial class wfRuta : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "rut_nombre";
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


        public static string ShowData(Ruta obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.rut_nombre }, descripcion = new string[] { obj.rut_origen, "-", obj.rut_destino }, logico = new LogicItem[] { new LogicItem("Activos", obj.rut_estado) } }.ToString();
        }

        public static string ShowObject(Ruta obj)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.rut_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.rut_codigo_key.ToString(), visible = false }.ToString());
            //html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.rut_id, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.rut_nombre, clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbORIGEN", etiqueta = "Origen", placeholder = "Origen", valor = obj.rut_origen, clase = Css.large, diccionario = Dictionaries.GetCantones() }.ToString());
            html.AppendLine(new Select { id = "cmbDESTINO", etiqueta = "Destino", placeholder = "Destino", valor = obj.rut_destino, clase = Css.large, diccionario = Dictionaries.GetCantones() }.ToString());
            html.AppendLine(new Input { id = "txtKILOMETROS", etiqueta = "Kilómetros", placeholder = "Kilómetros", valor = obj.rut_kilometros.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtTIEMPO", etiqueta = "Duración", placeholder = "Duración", valor = obj.rut_duracion.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.rut_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Ruta obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (!string.IsNullOrEmpty(obj.rut_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " rut_nombre like {" + contador + "} ";
                valores.Add("%" + obj.rut_nombre + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.rut_origen))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " rut_origen like {" + contador + "} ";
                valores.Add("%" + obj.rut_origen + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.rut_destino))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " rut_destino like {" + contador + "} ";
                valores.Add("%" + obj.rut_destino + "%");
                contador++;
            }
            if (obj.rut_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " rut_estado = {" + contador + "} ";
                valores.Add(obj.rut_estado.Value);
                contador++;
            }
            if (obj.rut_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " rut_empresa = {" + contador + "} ";
                valores.Add(obj.rut_empresa);
                contador++;
            }
            parametros.valores = valores.ToArray();
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Ruta(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Ruta> lst = RutaBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Ruta obj in lst)
            {
                string id = "{\"rut_codigo\":\"" + obj.rut_codigo + "\", \"rut_empresa\":\"" + obj.rut_empresa + "\"}";//ID COMPUESTO
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
            return ShowObject(new Ruta());
        }

        [WebMethod]
        public static string GetObject(object id)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("rut_codigo", out codigo);
            tmp.TryGetValue("rut_empresa", out empresa);
            Ruta obj = new Ruta();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.rut_empresa = int.Parse(empresa.ToString());
                obj.rut_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.rut_codigo = int.Parse(codigo.ToString());
                obj.rut_codigo_key = int.Parse(codigo.ToString());
            }
            obj = RutaBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Ruta GetObjeto(object objeto)
        //{
        //    Ruta obj = new Ruta();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object codigokey = null;
        //        object empresa = null;
        //        object empresakey = null;
        //        object origen = null;
        //        object id = null;
        //        object nombre = null;
        //        object kilometros = null;
        //        object destino = null;
        //        object activo = null;
        //        object tiempo = null;
        //        tmp.TryGetValue("rut_codigo", out codigo);
        //        tmp.TryGetValue("rut_codigo_key", out codigokey);
        //        tmp.TryGetValue("rut_empresa", out empresa);
        //        tmp.TryGetValue("rut_empresa_key", out empresakey);
        //        tmp.TryGetValue("rut_id", out id);
        //        tmp.TryGetValue("rut_origen", out origen);
        //        tmp.TryGetValue("rut_destino", out destino);
        //        tmp.TryGetValue("rut_nombre", out nombre);
        //        tmp.TryGetValue("rut_kilometros", out kilometros);
        //        tmp.TryGetValue("rut_estado", out activo);
        //        tmp.TryGetValue("rut_duracion", out tiempo);
        //        if (codigo != null && !codigo.Equals(""))
        //        {
        //            obj.rut_codigo = int.Parse(codigo.ToString());
        //        }
        //        if (codigokey != null && !codigokey.Equals(""))
        //        {
        //            obj.rut_codigo_key = int.Parse(codigo.ToString());
        //        }
        //        if (empresa != null && !empresa.Equals(""))
        //        {
        //            obj.rut_empresa = int.Parse(empresa.ToString());
        //        }
        //        if (empresakey != null && !empresakey.Equals(""))
        //        {
        //            obj.rut_empresa_key = int.Parse(empresakey.ToString());
        //        }
        //        obj.rut_origen = (string)origen;
        //        obj.rut_id = (string)id;
        //        obj.rut_destino = (string)destino;
        //        obj.rut_nombre = (string)nombre;
        //        if (kilometros != null && !kilometros.Equals(""))
        //        {
        //            obj.rut_kilometros = Convert.ToDecimal(kilometros);
        //        }
        //        if (tiempo != null && !tiempo.Equals(""))
        //        {
        //            obj.rut_duracion = Convert.ToDecimal(tiempo);
        //        }
        //        obj.rut_estado = (int?)activo;
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
            html.AppendLine(new Input { id = "cmbORIGEN_S", placeholder = "Origen", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "cmbDESTINO_S", placeholder = "Destino", clase = Css.large }.ToString());
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
            return ShowObject(new Ruta());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Ruta obj = new Ruta(objeto);
            obj.rut_codigo_key = obj.rut_codigo;
            obj.rut_empresa_key = obj.rut_empresa;
            obj.rut_nombre = obj.rut_origen + " - " + obj.rut_destino;
            if (obj.rut_codigo_key > 0)
            {
                if (RutaBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (RutaBLL.Insert(obj) > 0)
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
            Ruta obj = new Ruta(objeto);
            obj.rut_codigo_key = obj.rut_codigo;
            obj.rut_empresa_key = obj.rut_empresa;
         
            if (RutaBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}