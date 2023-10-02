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
    public partial class wfBanco: System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "ban_codigo";
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


        public static string ShowData(Banco obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.ban_id, "-", obj.ban_nombre }, descripcion = new string[] { }, logico = new LogicItem[] { new LogicItem("Activos", obj.ban_estado) } }.ToString();
        }

        public static string ShowObject(Banco obj)
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.ban_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.ban_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.ban_id, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.ban_nombre, clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbTIPO", etiqueta = "Tipo", valor = obj.ban_tipo.ToString(), clase = Css.large, diccionario = Dictionaries.GetTipoBanco() }.ToString());

            html.AppendLine(new Input { id = "txtCUENTA", etiqueta = "Cuenta Caja", valor = obj.ban_nombrecuenta, autocomplete = "GetCuentaObj", obligatorio = true, clase = Css.medium, placeholder = "Cuenta Caja" }.ToString() + " " + new Input { id = "txtCODCCUENTA", visible = false, valor = obj.ban_cuenta }.ToString());
        //    html.AppendLine(new Select { id = "cmbCUENTA", etiqueta = "Cuenta", valor = obj.ban_cuenta.ToString(), clase = Css.large, diccionario = Dictionaries.GetCuentaMovi() }.ToString());
            html.AppendLine(new Input { id = "txtNUMERO", etiqueta = "Numero", placeholder = "Numero", valor = obj.ban_numero.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNUMEROCHEQUE", etiqueta = "Numero cheque", placeholder = "Numero cheque", valor = obj.ban_nro_cheque.ToString(), clase = Css.large }.ToString());

            html.AppendLine(new Input { id = "txtULTCSC", etiqueta = "ULTCSC", datepicker = true, datetimevalor = (obj.ban_ultcsc.HasValue) ? obj.ban_ultcsc.Value : DateTime.Now, clase = Css.large,obligatorio=true }.ToString());
            html.AppendLine(new Input { id = "txtCODIMP", etiqueta = "Codimp", placeholder = "Codimp", valor = obj.ban_codimp, clase = Css.large }.ToString());          
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.ban_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Banco obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();

            if (obj.ban_codigo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " ban_codigo = {" + contador + "} ";
                valores.Add(obj.ban_codigo);
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.ban_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " ban_nombre like {" + contador + "} ";
                valores.Add("%" + obj.ban_nombre + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.ban_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " ban_id like {" + contador + "} ";
                valores.Add("%" + obj.ban_id + "%");
                contador++;
            }
           
            if (obj.ban_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " ban_estado = {" + contador + "} ";
                valores.Add(obj.ban_estado.Value);
                contador++;

            }
            if (obj.ban_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " ban_empresa = {" + contador + "} ";
                valores.Add(obj.ban_empresa);
                contador++;

            }
            parametros.valores = valores.ToArray();





        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Banco(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Banco> lst = BancoBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Banco obj in lst)
            {
                string id = "{\"ban_codigo\":\"" + obj.ban_codigo + "\", \"ban_empresa\":\"" + obj.ban_empresa + "\"}";//ID COMPUESTO
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
            return ShowObject(new Banco());
        }


        [WebMethod]
        public static string GetObject(object id)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("ban_codigo", out codigo);
            tmp.TryGetValue("ban_empresa", out empresa);

            Banco obj = new Banco();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.ban_empresa = int.Parse(empresa.ToString());
                obj.ban_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.ban_codigo = int.Parse(codigo.ToString());
                obj.ban_codigo_key = int.Parse(codigo.ToString());
            }

            obj = BancoBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Banco GetObjeto(object objeto)
        //{
        //    Banco obj = new Banco();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object empresa = null;
        //        object empresakey = null;
        //        object codigokey = null;
        //        object id = null;
        //        object nombre = null;
        //        object activo = null;
        //        object tipo = null;
        //        object cuenta = null;
        //        object numero = null;
        //        object nro_cheque = null;
        //        object ultcsc = null;
        //        object codimp = null;


        //        tmp.TryGetValue("ban_codigo", out codigo);
        //        tmp.TryGetValue("ban_empresa", out empresa);
        //        tmp.TryGetValue("ban_codigo_key", out codigokey);
        //        tmp.TryGetValue("ban_empresa_key", out empresakey);
        //        tmp.TryGetValue("ban_id", out id);
        //        tmp.TryGetValue("ban_nombre", out nombre);
        //        tmp.TryGetValue("ban_tipo", out tipo);
        //        tmp.TryGetValue("ban_cuenta", out cuenta);
        //        tmp.TryGetValue("ban_nro_cheque", out nro_cheque);
        //        tmp.TryGetValue("ban_ultcsc", out ultcsc);
        //        tmp.TryGetValue("ban_codimp", out codimp);
        //        tmp.TryGetValue("ban_numero", out numero);
        //        tmp.TryGetValue("ban_estado", out activo);
        //        if (empresa != null && !empresa.Equals(""))
        //        {
        //            obj.ban_empresa = int.Parse(empresa.ToString());
        //        }
        //        if (empresakey != null && !empresakey.Equals(""))
        //        {
        //            obj.ban_empresa_key = int.Parse(empresakey.ToString());
        //        }
        //        if (codigo != null && !codigo.Equals(""))
        //        {
        //            obj.ban_codigo = int.Parse(codigo.ToString());
        //        }
        //        if (codigokey != null && !codigokey.Equals(""))
        //        {
        //            obj.ban_codigo_key = int.Parse(codigokey.ToString());
        //        }

        //        obj.ban_id = (string)id;
        //        obj.ban_nombre = (string)nombre;
        //       obj.ban_tipo  =	 Convert.ToInt32(tipo);
        //         obj.ban_cuenta  = Convert.ToInt32(cuenta );
        //         obj.ban_numero = Convert.ToInt32(numero );
        //         obj.ban_nro_cheque  = Convert.ToInt32(nro_cheque );
        //         obj.ban_ultcsc = Convert.ToDateTime(ultcsc);
        //         obj.ban_codimp = (string)codimp;
               
        //        obj.ban_estado = (int?)activo;
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
            return ShowObject(new Banco());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Banco obj = new Banco(objeto);
            obj.ban_codigo_key = obj.ban_codigo;
            obj.ban_empresa_key = obj.ban_empresa;

            if (obj.ban_codigo_key > 0)
            {
                if (BancoBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (BancoBLL.Insert(obj) > 0)
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
            Banco obj = new Banco(objeto);
            obj.ban_codigo_key = obj.ban_codigo;
            obj.ban_empresa_key = obj.ban_empresa;

            if (BancoBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}