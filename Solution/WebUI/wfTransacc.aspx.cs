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

    public partial class wfTransacc : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "tra_secuencia";
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


        public static string ShowData(Transacc obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.tra_nombremodulo, "-", obj.tra_id }, descripcion = new string[] { }, logico = new LogicItem[] { new LogicItem("Activos", obj.tra_estado) } }.ToString();
        }

        public static string ShowObject(Transacc obj)
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtSECUENCIA", etiqueta = "Secuencia", placeholder = "Secuencia", valor = obj.tra_secuencia.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtSECUENCIA_key", valor = obj.tra_secuencia_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Select { id = "cmbMODULO", etiqueta = "Modulo", valor = obj.tra_modulo.ToString(), clase = Css.large, diccionario = Dictionaries.GetModulos() }.ToString());
            html.AppendLine(new Input { id = "txtMODULO_key", valor = obj.tra_modulo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.tra_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.tra_nombre, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.tra_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Transacc obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();

            if (obj.tra_secuencia > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tra_secuencia = {" + contador + "} ";
                valores.Add(obj.tra_secuencia);
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.tra_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tra_nombre like {" + contador + "} ";
                valores.Add("%" + obj.tra_nombre + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.tra_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tra_id like {" + contador + "} ";
                valores.Add("%" + obj.tra_id + "%");
                contador++;
            }
            if (obj.tra_modulo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tra_modulo = {" + contador + "} ";
                valores.Add(obj.tra_modulo);
                contador++;

            }

            if (obj.tra_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tra_estado = {" + contador + "} ";
                valores.Add(obj.tra_estado.Value);
                contador++;

            }
            parametros.valores = valores.ToArray();





        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause( new Transacc(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Transacc> lst = TransaccBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Transacc obj in lst)
            {
                string id = "{\"tra_secuencia\":\"" + obj.tra_secuencia + "\", \"tra_modulo\":\"" + obj.tra_modulo + "\"}";//ID COMPUESTO              
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
            return ShowObject(new Transacc());
        }


        [WebMethod]
        public static string GetObject(object id)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object secuencia = null;
            object modulo = null;

            tmp.TryGetValue("tra_secuencia", out secuencia);
            tmp.TryGetValue("tra_modulo", out modulo);

            Transacc obj = new Transacc();
            if (modulo != null && !modulo.Equals(""))
            {
                obj.tra_modulo = int.Parse(modulo.ToString());
                obj.tra_modulo_key = int.Parse(modulo.ToString());
            }
            if (secuencia != null && !secuencia.Equals(""))
            {
                obj.tra_secuencia = int.Parse(secuencia.ToString());
                obj.tra_secuencia_key = int.Parse(secuencia.ToString());
            }

            obj = TransaccBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Transacc GetObjeto(object objeto)
        //{
        //    Transacc obj = new Transacc();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object secuencia = null;
        //        object modulo = null;
        //        object modulokey = null;
        //        object secuenciakey = null;
        //        object id = null;
        //        object nombre = null;
        //        object nombremodulo = null;
        //        object activo = null;

        //        tmp.TryGetValue("tra_secuencia", out secuencia);
        //        tmp.TryGetValue("tra_modulo", out modulo);
        //        tmp.TryGetValue("tra_secuencia_key", out secuenciakey);
        //        tmp.TryGetValue("tra_modulo_key", out modulokey);
        //        tmp.TryGetValue("tra_id", out id);
        //        tmp.TryGetValue("tra_nombre", out nombre);
        //        tmp.TryGetValue("tra_nombremodulo", out nombremodulo);
        //        tmp.TryGetValue("tra_estado", out activo);
        //        if (modulo != null && !modulo.Equals(""))
        //        {
        //            obj.tra_modulo = int.Parse(modulo.ToString());
        //        }
        //        if (secuencia != null && !secuencia.Equals(""))
        //        {
        //            obj.tra_secuencia = int.Parse(secuencia.ToString());
        //        }
        //        if (modulokey != null && !modulokey.Equals(""))
        //        {

        //            obj.tra_modulo_key = int.Parse(modulokey.ToString());
        //        }
        //        if (secuenciakey != null && !secuenciakey.Equals(""))
        //        {

        //            obj.tra_secuencia_key = int.Parse(secuenciakey.ToString());
        //        }

        //        obj.tra_id = (string)id;
        //        obj.tra_nombre = (string)nombre;

        //        obj.tra_estado = (int?)activo;
        //        obj.crea_usr = "admin";
        //        obj.crea_fecha = DateTime.Now;
        //        obj.mod_usr = "admin";
        //        obj.mod_fecha = DateTime.Now;


        //        obj.tra_nombremodulo = (string)nombremodulo;
        //    }

        //    return obj;
        //}


        [WebMethod]
        public static string GetSearch()
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class='pull-left'>");
            //     html.AppendLine(HtmlElements.Input("Id", "txtSECUENCIA_S", "", HtmlElements.small, false));


            html.AppendLine(new Input { id = "txtNOMBRE_S", placeholder = "Nombre", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtID_S", placeholder = "Id", clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbMODULO_S", clase = Css.medium, diccionario = Dictionaries.GetModulos(), withempty = true }.ToString());
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
            return ShowObject(new Transacc());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Transacc obj = new Transacc(objeto);
            obj.tra_secuencia_key = obj.tra_secuencia;
            obj.tra_modulo_key = obj.tra_modulo;

            if (obj.tra_modulo_key > 0)
            {
                if (TransaccBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (TransaccBLL.Insert(obj) > 0)
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
           
            Transacc obj = new Transacc(objeto);
            obj.tra_secuencia_key = obj.tra_secuencia;
            obj.tra_modulo_key = obj.tra_modulo;
           
            if (TransaccBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}