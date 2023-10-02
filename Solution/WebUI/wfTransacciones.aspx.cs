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
    public partial class wfTransacciones : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "udo_tipodoc";
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


        public static string ShowData(Usrdoc obj)
        {  
            return new HtmlObjects.ListItem { titulo = new string[] { obj.udo_nombretipodoc.ToString() }, descripcion = new string[] { }, logico = new LogicItem[] {} }.ToString();
        }

 /*       public static string ShowObject(Usrdoc obj)
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.udo_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.udo_codigo_key.ToString(), visible = false }.ToString());
           html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.udo_id, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.udo_nombre, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtSUBFIJO", etiqueta = "Subfijo", placeholder = "Subfijo", valor = obj.udo_subfijo, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtGERENTE", etiqueta = "Gerente", placeholder = "Gerente", valor = obj.udo_gerente, clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbPAIS", etiqueta = "Pais", valor = obj.udo_pais.ToString(), clase = Css.large, diccionario = Dictionaries.GetPaises() }.ToString());
            html.AppendLine(new Select { id = "cmbPROVINCIA", etiqueta = "Provincia", valor = obj.udo_provincia.ToString(), clase = Css.large, diccionario = Dictionaries.Empty() }.ToString());
            html.AppendLine(new Select { id = "cmbCANTON", etiqueta = "Canton", valor = obj.udo_canton.ToString(), clase = Css.large, diccionario = Dictionaries.Empty() }.ToString());
            html.AppendLine(new Input { id = "txtDIRECCION", etiqueta = "Direccion", placeholder = "Direccion", valor = obj.udo_direccion, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtTELEFONO1", etiqueta = "Telefono 1", placeholder = "Telefono 1", valor = obj.udo_telefono1, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtTELEFONO2", etiqueta = "Telefono 2", placeholder = "Telefono 2", valor = obj.udo_telefono2, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtTELEFONO3", etiqueta = "Telefono 3", placeholder = "Telefono 3", valor = obj.udo_telefono3, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtRUC", etiqueta = "Ruc", placeholder = "Ruc", valor = obj.udo_ruc, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtFAX", etiqueta = "Fax", placeholder = "Fax", valor = obj.udo_fax, clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbCLIENTE_DEF", etiqueta = "Cliente", valor = obj.udo_cliente_def.ToString(), clase = Css.large, diccionario = Dictionaries.GetPersonas() }.ToString());
            html.AppendLine(new Select { id = "cmbCENTRO", etiqueta = "Centro", valor = obj.udo_centro.ToString(), clase = Css.large, diccionario = Dictionaries.GetCentro() }.ToString());
            html.AppendLine(new Select { id = "cmbCUENTACAJA", etiqueta = "Cuenta Caja", valor = obj.udo_cuentacaja.ToString(), clase = Css.large, diccionario = Dictionaries.GetCuenta() }.ToString());
            html.AppendLine(new Check { id = "chkMATRIZ", etiqueta = "Matriz ", valor = obj.udo_estado }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.udo_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }

        */


        public static void SetWhereClause(Usrdoc obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();

      /*      if (obj.udo_codigo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " udo_codigo = {" + contador + "} ";
                valores.Add(obj.udo_codigo);
                contador++;
            }
        */
            //Usuario usr = (Usuario)HttpContext.Current.Session["usuario"];
            
            parametros.where += ((parametros.where != "") ? " and " : "") + " udo_usuario like {" + contador + "} ";
            valores.Add(obj.udo_usuario);
                contador++;
                if (!string.IsNullOrEmpty(obj.udo_nombretipodoc))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tpd_nombre like {" + contador + "} ";
                valores.Add("%" + obj.udo_nombretipodoc + "%");
                contador++;
            }
        
            if (obj.udo_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " udo_estado = {" + contador + "} ";
                valores.Add(obj.udo_estado.Value);
                contador++;
                      
           }
            parametros.valores = valores.ToArray();
        }

        [WebMethod]
        public static string SelectObj(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;            
            object tipodoc = null;
            tmp.TryGetValue("udo_tipodoc", out tipodoc);           

          
            
           
           Tipodoc aux = new Tipodoc();
           aux.tpd_codigo = int.Parse(tipodoc.ToString());
           aux.tpd_codigo_key = int.Parse(tipodoc.ToString());  
           aux = TipodocBLL.GetByPK(aux);
           Formulario result = new Formulario();
           result.for_codigo = aux.tpd_for_eje ?? 0;
           result.for_codigo_key = aux.tpd_for_eje ?? 0;
           result = FormularioBLL.GetByPK(result);
           return new JavaScriptSerializer().Serialize(result);
        }

        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Usrdoc(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Usrdoc> lst = UsrdocBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Usrdoc obj in lst)
            {
                string id = "{\"udo_usuario\":\"" + obj.udo_usuario + "\", \"udo_empresa\":\"" + obj.udo_empresa + "\", \"udo_tipodoc\":\"" + obj.udo_tipodoc + "\"}";//ID COMPUESTO
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



   /*     [WebMethod]
        public static string AddObject()
        {
            return ShowObject(new Usrdoc());
        }*/


        [WebMethod]
        public static string GetObject(object id)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object usuario = null;
            object empresa = null;
            object tipodoc = null;
            tmp.TryGetValue("udo_tipodoc", out tipodoc);
            tmp.TryGetValue("udo_usuario", out usuario);
            tmp.TryGetValue("udo_empresa", out empresa);

            Usrdoc obj = new Usrdoc();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.udo_empresa = int.Parse(empresa.ToString());
                obj.udo_empresa_key = int.Parse(empresa.ToString());
            }
            if (tipodoc != null && !tipodoc.Equals(""))
            {
                obj.udo_tipodoc = int.Parse(tipodoc.ToString());
                obj.udo_tipodoc_key = int.Parse(tipodoc.ToString());
            }
            if (usuario != null && !usuario.Equals(""))
            {
                obj.udo_usuario = (string)usuario;
                obj.udo_usuario_key = (string)usuario; 
            }
            obj = UsrdocBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }
      

        public static Usrdoc GetObjeto(object objeto)
        {
            Usrdoc obj = new Usrdoc();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object id = null;


                tmp.TryGetValue("udo_nombretipodoc", out id);               
                   
                obj.udo_nombretipodoc = (string)id;               
               
                obj.crea_usr = "admin";
                obj.crea_fecha = DateTime.Now;
                obj.mod_usr = "admin";
                obj.mod_fecha = DateTime.Now;
            }

            return obj;
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

     /*   [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Usrdoc());
        }
        */

     
    }
}
