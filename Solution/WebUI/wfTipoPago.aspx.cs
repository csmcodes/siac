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
    public partial class wfTipopago : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "tpa_codigo";
        protected static string WhereClause = "";
        protected static int? tclipro; 
        protected static WhereParams parametros = new WhereParams();

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


        public static string ShowData(Tipopago obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.tpa_id, "-", obj.tpa_nombre }, descripcion = new string[] { }, logico = new LogicItem[] { new LogicItem("Activos", obj.tpa_estado) } }.ToString();
        }

        public static string ShowObject(Tipopago obj)
        {
            Persona persona = new Persona();
            int modulo = 0;
            persona.per_empresa = obj.tpa_empresa;
            persona.per_empresa_key = obj.tpa_empresa;
            if (obj.tpa_codclipro.HasValue)
            {
                persona.per_codigo = obj.tpa_codclipro.Value;
                persona.per_codigo_key = obj.tpa_codclipro.Value;
                persona = PersonaBLL.GetByPK(persona);
            }

            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.tpa_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.tpa_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.tpa_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.tpa_nombre, clase = Css.large, obligatorio = true }.ToString());
            
        //    html.AppendLine(new Select { id = "cmbTCLIPRO", etiqueta = "Tclipro", valor = obj.tpa_tclipro.ToString(), clase = Css.large, diccionario = Dictionaries.Empty() }.ToString());
            html.AppendLine(new Select { id = "cmbCONTABILIZA", etiqueta = "Contabiliza", valor = obj.tpa_contabiliza.ToString(), clase = Css.large, diccionario = Dictionaries.GetContabilizacion() }.ToString());
           // html.AppendLine(new Check { id = "chkCONTABILIZA", etiqueta = "Contabiliza ", valor = obj.tpa_contabiliza }.ToString());
     //       html.AppendLine(new Input { id = "txtCUENTA", etiqueta = "Cuenta", placeholder = "Cuenta", valor = obj.tpa_cuenta, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtCUENTA", etiqueta = "Cuenta Caja", valor = obj.tpa_nombrecuenta, autocomplete = "GetCuentaObj", obligatorio = true, clase = Css.medium, placeholder = "Cuenta Caja" }.ToString() + " " + new Input { id = "txtCODCCUENTA", visible = false, valor = obj.tpa_cuenta }.ToString());
         //   html.AppendLine(new Select { id = "cmbCODCLIPRO", etiqueta = "Cliente", valor = obj.tpa_codclipro.ToString(), clase = Css.large, diccionario = Dictionaries.GetPersonas() }.ToString());

            if (tclipro == Constantes.cProveedor)
            {
                html.AppendLine(new Input { id = "txtCODCLIPRO", etiqueta = "Proveedor", valor = persona.per_nombres+" "+persona.per_apellidos, autocomplete = "GetProveedorObj",  clase = Css.medium, placeholder = "Proveedor" }.ToString() + " " + new Input { id = "cmbCODCLIPRO", visible = false, valor = obj.tpa_codclipro }.ToString());
                modulo = 6;
            }
            else
            {
                html.AppendLine(new Input { id = "txtCODCLIPRO", etiqueta = "Cliente", valor = persona.per_nombres + " " + persona.per_apellidos, autocomplete = "GetClienteObj", clase = Css.medium, placeholder = "Cliente" }.ToString() + " " + new Input { id = "cmbCODCLIPRO", visible = false, valor = obj.tpa_codclipro }.ToString());
                modulo = 5;
            }


            //html.AppendLine(new Select { id = "cmbTRANSACC", etiqueta = "Trnasacc", valor = obj.tpa_transacc.ToString(), clase = Css.large, diccionario =  Dictionaries.GetTransacc(modulo) }.ToString());
            html.AppendLine(new Select { id = "cmbTRANSACC", etiqueta = "Trnasacc", valor = obj.tpa_transacc.ToString(), clase = Css.large, diccionario = Dictionaries.GetTransaccion() }.ToString());

            html.AppendLine(new Input { id = "txtDETALLE", etiqueta = "Detalle", placeholder = "Detalle", valor = obj.tpa_detalle, clase = Css.large }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.tpa_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Tipopago obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();

            if (obj.tpa_codigo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tpa_codigo = {" + contador + "} ";
                valores.Add(obj.tpa_codigo);
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.tpa_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tpa_nombre like {" + contador + "} ";
                valores.Add("%" + obj.tpa_nombre + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.tpa_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tpa_id like {" + contador + "} ";
                valores.Add("%" + obj.tpa_id + "%");
                contador++;
            }

            if (obj.tpa_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tpa_estado = {" + contador + "} ";
                valores.Add(obj.tpa_estado.Value);
                contador++;

            }


            if (obj.tpa_tclipro>0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tpa_tclipro = {" + contador + "} ";
                valores.Add(obj.tpa_tclipro);
                contador++;

            }
            if (obj.tpa_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tpa_empresa = {" + contador + "} ";
                valores.Add(obj.tpa_empresa);
                contador++;

            }


            parametros.valores = valores.ToArray();





        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Tipopago(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Tipopago> lst = TipopagoBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Tipopago obj in lst)
            {
                string id = "{\"tpa_codigo\":\"" + obj.tpa_codigo + "\", \"tpa_empresa\":\"" + obj.tpa_empresa + "\"}";//ID COMPUESTO

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
            return ShowObject(new Tipopago());
        }


        [WebMethod]
        public static string GetObject(object id)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("tpa_codigo", out codigo);
            tmp.TryGetValue("tpa_empresa", out empresa);

            Tipopago obj = new Tipopago();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.tpa_empresa = int.Parse(empresa.ToString());
                obj.tpa_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.tpa_codigo = int.Parse(codigo.ToString());
                obj.tpa_codigo_key = int.Parse(codigo.ToString());
            }

            obj = TipopagoBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        /* public static Tipopago GetObjeto(object objeto)
         {
             Tipopago obj = new Tipopago();
             if (objeto != null)
             {
                 Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                 object codigo = null;
                 object empresa= null;
                 object empresakey = null;
                 object codigokey= null;
                 object id= null;
                 object nombre= null;
                 object subfijo= null;
                 object gerente= null;
                 object pais= null;
                 object provincia= null;
                 object canton= null;
                 object direccion= null;
                 object telefono1= null;
                 object telefono2= null;
                 object telefono3= null;
                 object ruc= null;
                 object fax= null;
                 object cliente_def= null;
                 object centro= null;
                 object matriz= null;
                 object cuentacaja = null;
                 object activo = null;

                 tmp.TryGetValue("tpa_codigo", out codigo);
                 tmp.TryGetValue("tpa_empresa", out empresa);
                 tmp.TryGetValue("tpa_codigo_key", out codigokey);
                 tmp.TryGetValue("tpa_empresa_key", out empresakey);
                 tmp.TryGetValue("tpa_id", out id);
                 tmp.TryGetValue("tpa_nombre", out nombre);
                 tmp.TryGetValue("tpa_subfijo", out subfijo);
                 tmp.TryGetValue("tpa_gerente", out gerente);
                 tmp.TryGetValue("tpa_pais", out pais);
                 tmp.TryGetValue("tpa_provincia", out provincia);
                 tmp.TryGetValue("tpa_canton", out canton);
                 tmp.TryGetValue("tpa_direccion", out direccion);
                 tmp.TryGetValue("tpa_telefono1", out telefono1);
                 tmp.TryGetValue("tpa_telefono2", out telefono2);
                 tmp.TryGetValue("tpa_telefono3", out telefono3);
                 tmp.TryGetValue("tpa_ruc", out ruc);
                 tmp.TryGetValue("tpa_fax", out fax);
                 tmp.TryGetValue("tpa_cliente_def", out cliente_def);
                 tmp.TryGetValue("tpa_centro", out centro);
                 tmp.TryGetValue("tpa_matriz", out matriz);
                 tmp.TryGetValue("tpa_cuentacaja", out cuentacaja);
                 tmp.TryGetValue("tpa_estado", out activo);
                 if (empresa != null && !empresa.Equals(""))
                 {
                     obj.tpa_empresa = int.Parse(empresa.ToString());                  
                 }
                 if (empresakey != null && !empresakey.Equals(""))
                 {                    
                     obj.tpa_empresa_key = int.Parse(empresakey.ToString());
                 }
                 if (codigo != null && !codigo.Equals(""))
                 {
                     obj.tpa_codigo = int.Parse(codigo.ToString());                   
                 }
                 if (codigokey != null && !codigokey.Equals(""))
                 {                   
                     obj.tpa_codigo_key = int.Parse(codigokey.ToString());
                 }

                 obj.tpa_id = (string)id;
                 obj.tpa_nombre = (string)nombre;
                 obj.tpa_subfijo = (string)subfijo;
                 obj.tpa_gerente = (string)gerente;
                 obj.tpa_pais = Convert.ToInt32(pais);
                 obj.tpa_provincia = Convert.ToInt32(provincia);
                 obj.tpa_canton = Convert.ToInt32(canton);
                 obj.tpa_direccion = (string)direccion;
                 obj.tpa_telefono1 = (string)telefono1;
                 obj.tpa_telefono2 = (string)telefono2;
                 obj.tpa_telefono3 = (string)telefono3;
                 obj.tpa_ruc = (string)ruc;
                 obj.tpa_fax = (string)fax;
                 obj.tpa_cliente_def = Convert.ToInt32( cliente_def);
                 obj.tpa_centro = Convert.ToInt32(centro);
                 obj.tpa_matriz = (int?)matriz;
                 if (obj.tpa_matriz > 0)
                 {
                     parametros = new WhereParams("tpa_matriz = {0} and tpa_codigo != {1}", obj.tpa_matriz, obj.tpa_codigo);
                     if (TipopagoBLL.GetRecordCount(parametros, "") > 0)
                     {
                         obj.tpa_matriz =0;
                     }
                 }
                
                 obj.tpa_cuentacaja = Convert.ToInt32(cuentacaja);
                 obj.tpa_estado = (int?)activo;
                 obj.crea_usr = "admin";
                 obj.crea_fecha = DateTime.Now;
                 obj.mod_usr = "admin";
                 obj.mod_fecha = DateTime.Now;
             }

             return obj;
         }
         */

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
            return ShowObject(new Tipopago());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Tipopago obj = new Tipopago(objeto);

            if (obj.tpa_codigo_key > 0)
            {
                if (TipopagoBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (TipopagoBLL.Insert(obj) > 0)
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
            Tipopago obj = new Tipopago(objeto);
            if (TipopagoBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}