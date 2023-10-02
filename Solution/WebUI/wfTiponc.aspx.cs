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
    public partial class wfTiponc : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "tnc_nombre";
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


        public static string ShowData(Tiponc obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.tnc_nombre }, descripcion = new string[] {  }, logico = new LogicItem[] { new LogicItem("Activos", obj.tnc_estado) } }.ToString();
        }

        public static string ShowObject(Tiponc obj)
        {   Cuenta cuenta = new Cuenta();
            cuenta.cue_empresa = obj.tnc_empresa;
            cuenta.cue_empresa_key = obj.tnc_empresa;
            if (obj.tnc_cuentanc.HasValue)
            {
                cuenta.cue_codigo = obj.tnc_cuentanc.Value;
                cuenta.cue_codigo_key = obj.tnc_cuentanc.Value;
                cuenta = CuentaBLL.GetByPK(cuenta);
            }

            Cuenta cuenta2 = new Cuenta();
            cuenta2.cue_empresa = obj.tnc_empresa;
            cuenta2.cue_empresa_key = obj.tnc_empresa;
            if (obj.tnc_cuentand.HasValue)
            {
                cuenta2.cue_codigo = obj.tnc_cuentand.Value;
                cuenta2.cue_codigo_key = obj.tnc_cuentand.Value;
                cuenta2 = CuentaBLL.GetByPK(cuenta2);
            }           
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.tnc_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.tnc_codigo_key.ToString(), visible = false }.ToString());            
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.tnc_nombre, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.tnc_id, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtCUENTANC", etiqueta = "cuentanc", valor = cuenta.cue_nombre, autocomplete = "GetCuentaObj", obligatorio = false, clase = Css.medium, placeholder = "cuentanc" }.ToString() + " " + new Input { id = "txtCODCUENTANC", visible = false, valor = obj.tnc_cuentanc }.ToString());
            html.AppendLine(new Input { id = "txtCUENTAND", etiqueta = "cuentand", valor = cuenta2.cue_nombre, autocomplete = "GetCuentaObj", obligatorio = false, clase = Css.medium, placeholder = "cuentand" }.ToString() + " " + new Input { id = "txtCODCUENTAND", visible = false, valor = obj.tnc_cuentand }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.tnc_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Tiponc obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (!string.IsNullOrEmpty(obj.tnc_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tnc_nombre like {" + contador + "} ";
                valores.Add("%" + obj.tnc_nombre + "%");
                contador++;
            }

            if (obj.tnc_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tnc_estado = {" + contador + "} ";
                valores.Add(obj.tnc_estado.Value);
                contador++;
            }
            if (obj.tnc_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tnc_empresa = {" + contador + "} ";
                valores.Add(obj.tnc_empresa);
                contador++;
            }
            if (obj.tnc_tclipro > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tnc_tclipro = {" + contador + "} ";
                valores.Add(obj.tnc_tclipro);
                contador++;

            }
            parametros.valores = valores.ToArray();
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Tiponc(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Tiponc> lst = TiponcBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Tiponc obj in lst)
            {
                string id = "{\"tnc_codigo\":\"" + obj.tnc_codigo + "\", \"tnc_empresa\":\"" + obj.tnc_empresa + "\"}";//ID COMPUESTO
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
            return ShowObject(new Tiponc());
        }

        [WebMethod]
        public static string GetObject(object id)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("tnc_codigo", out codigo);
            tmp.TryGetValue("tnc_empresa", out empresa);
            Tiponc obj = new Tiponc();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.tnc_empresa = int.Parse(empresa.ToString());
                obj.tnc_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.tnc_codigo = int.Parse(codigo.ToString());
                obj.tnc_codigo_key = int.Parse(codigo.ToString());
            }
            obj = TiponcBLL.GetByPK(obj);
            Cuenta cuenta = new Cuenta();
            cuenta.cue_empresa = obj.tnc_empresa;
            cuenta.cue_empresa_key = obj.tnc_empresa;
            if (obj.tnc_cuentanc.HasValue)
            {
                cuenta.cue_codigo = obj.tnc_cuentanc.Value;
                cuenta.cue_codigo_key = obj.tnc_cuentanc.Value;
                cuenta = CuentaBLL.GetByPK(cuenta);
            }
            obj.tnc_nombrecuentanc = cuenta.cue_nombre;
            Cuenta cuenta2 = new Cuenta();
            cuenta2.cue_empresa = obj.tnc_empresa;
            cuenta2.cue_empresa_key = obj.tnc_empresa;
            if (obj.tnc_cuentand.HasValue)
            {
                cuenta2.cue_codigo = obj.tnc_cuentand.Value;
                cuenta2.cue_codigo_key = obj.tnc_cuentand.Value;
                cuenta2 = CuentaBLL.GetByPK(cuenta2);
            }
            obj.tnc_nombrecuentand = cuenta2.cue_nombre;
            return new JavaScriptSerializer().Serialize(obj);
        }


        


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
            return ShowObject(new Tiponc());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Tiponc obj = new Tiponc(objeto);
      
            if (obj.tnc_codigo_key > 0)
            {
                if (TiponcBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (TiponcBLL.Insert(obj) > 0)
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
            Tiponc obj = new Tiponc(objeto);
            if (TiponcBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}