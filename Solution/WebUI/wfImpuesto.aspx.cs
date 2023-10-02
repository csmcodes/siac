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
    public partial class wfImpuesto : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "imp_codigo";
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


        public static string ShowData(Impuesto obj)
        {

            return new HtmlObjects.ListItem { titulo = new string[] { obj.imp_id }, descripcion = new string[] { obj.imp_nombre }, logico = new LogicItem[] { new LogicItem("Activos", obj.imp_estado) } }.ToString();              
        }

        public static string ShowObject(Impuesto obj)
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.imp_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.imp_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.imp_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.imp_nombre, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Select { id = "cmbCONCEPTO", etiqueta = "Concepto", valor = obj.imp_concepto.ToString(), clase = Css.large, diccionario = Dictionaries.GetConcepto() }.ToString());

            html.AppendLine(new Input { id = "txtCUENTA", etiqueta = "Cuenta Caja", valor = obj.imp_nombrecuenta, autocomplete = "GetCuentaObj", obligatorio = true, clase = Css.medium, placeholder = "Cuenta Caja" }.ToString() + " " + new Input { id = "txtCODCCUENTA", visible = false, valor = obj.imp_cuenta }.ToString());
        //    html.AppendLine(new Select { id = "cmbCUENTA", etiqueta = "Cuenta", valor = obj.imp_cuenta.ToString(), clase = Css.large, diccionario = Dictionaries.GetCuenta() }.ToString());         
            html.AppendLine(new Select { id = "cmbIVAFUENTE", etiqueta = "Iva Fuente", valor = obj.imp_ivafuente.ToString(), clase = Css.large, diccionario = Dictionaries.GetIvafuente() }.ToString());
            html.AppendLine(new Check { id = "chkSERVICIO", etiqueta = "Servicio ", valor = obj.imp_servicio }.ToString());
            html.AppendLine(new Input { id = "txtPORCENTAJE", etiqueta = "Porcentaje", placeholder = "Porcentaje", valor = obj.imp_porcentaje.ToString(), clase = Css.large }.ToString());            
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.imp_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Impuesto obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();

            if (obj.imp_codigo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " imp_codigo = {" + contador + "} ";
                valores.Add(obj.imp_codigo);
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.imp_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " imp_nombre like {" + contador + "} ";
                valores.Add("%" + obj.imp_nombre + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.imp_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " imp_id like {" + contador + "} ";
                valores.Add("%" + obj.imp_id + "%");
                contador++;
            }
           
            if (obj.imp_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " imp_estado = {" + contador + "} ";
                valores.Add(obj.imp_estado.Value);
                contador++;

            }
            if (obj.imp_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " imp_empresa = {" + contador + "} ";
                valores.Add(obj.imp_empresa);
                contador++;

            }
            parametros.valores = valores.ToArray();





        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Impuesto(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Impuesto> lst = ImpuestoBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Impuesto obj in lst)
            {
                string id = "{\"imp_codigo\":\"" + obj.imp_codigo + "\", \"imp_empresa\":\"" + obj.imp_empresa + "\"}";//ID COMPUESTO
               
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
            return ShowObject(new Impuesto());
        }


        [WebMethod]
        public static string GetObject(object id)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("imp_codigo", out codigo);
            tmp.TryGetValue("imp_empresa", out empresa);
            Impuesto obj = new Impuesto();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.imp_empresa = int.Parse(empresa.ToString());
                obj.imp_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.imp_codigo = int.Parse(codigo.ToString());
                obj.imp_codigo_key = int.Parse(codigo.ToString());
            }
            obj = ImpuestoBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Impuesto GetObjeto(object objeto)
        //{
        //    Impuesto obj = new Impuesto();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object empresa = null;
        //        object empresakey = null;
        //        object codigokey = null;
        //        object id = null;
        //        object nombre = null;
        //        object concepto = null;
        //        object cuenta = null;
        //        object ivafuente = null;
        //        object servicio = null;
        //        object porcentaje = null;  
        //        object activo = null;
        //        tmp.TryGetValue("imp_codigo", out codigo);
        //        tmp.TryGetValue("imp_empresa", out empresa);
        //        tmp.TryGetValue("imp_codigo_key", out codigokey);
        //        tmp.TryGetValue("imp_empresa_key", out empresakey);
        //        tmp.TryGetValue("imp_id", out id);
        //        tmp.TryGetValue("imp_nombre", out nombre);
        //        tmp.TryGetValue("imp_concepto", out concepto);
        //        tmp.TryGetValue("imp_cuenta", out cuenta);
        //        tmp.TryGetValue("imp_ivafuente", out ivafuente);
        //        tmp.TryGetValue("imp_servicio", out servicio);
        //        tmp.TryGetValue("imp_porcentaje", out porcentaje);               
        //        tmp.TryGetValue("imp_estado", out activo);
        //        if (empresa != null && !empresa.Equals(""))
        //        {
        //            obj.imp_empresa = int.Parse(empresa.ToString());
        //        }
        //        if (empresakey != null && !empresakey.Equals(""))
        //        {
        //            obj.imp_empresa_key = int.Parse(empresakey.ToString());
        //        }
        //        if (codigo != null && !codigo.Equals(""))
        //        {
        //            obj.imp_codigo = int.Parse(codigo.ToString());
        //        }
        //        if (codigokey != null && !codigokey.Equals(""))
        //        {
        //            obj.imp_codigo_key = int.Parse(codigokey.ToString());
        //        }

        //        obj.imp_id = (string)id;
        //        obj.imp_nombre = (string)nombre;
        //        obj.imp_concepto = Convert.ToInt32(concepto);
        //        obj.imp_cuenta = Convert.ToInt32(cuenta);
        //        obj.imp_ivafuente = Convert.ToInt32(ivafuente);
        //        obj.imp_servicio = Convert.ToInt32(servicio);
        //        obj.imp_porcentaje = Convert.ToDecimal(porcentaje); 
        //        obj.imp_estado = (int?)activo;
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
            html.AppendLine(new Boton { refresh=true }.ToString());
            html.AppendLine(new Boton { clean = true }.ToString());
            html.AppendLine("</div>");
            return html.ToString();
        }

        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Impuesto());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Impuesto obj = new Impuesto(objeto);
            obj.imp_codigo_key = obj.imp_codigo;
            obj.imp_empresa_key = obj.imp_empresa;
            
            if (obj.imp_codigo_key > 0)
            {
                if (ImpuestoBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (ImpuestoBLL.Insert(obj) > 0)
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
            Impuesto obj = new Impuesto(objeto);
            obj.imp_codigo_key = obj.imp_codigo;
            obj.imp_empresa_key = obj.imp_empresa;

            if (ImpuestoBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}