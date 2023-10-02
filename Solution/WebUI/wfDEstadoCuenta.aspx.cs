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
    public partial class wfDEstadoCuenta : System.Web.UI.Page
    {    
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "ddo_doctran,ddo_pago,ddo_fecha_ven,com_fecha";
        protected static string WhereClause = "";
        protected static WhereParams parametros = new WhereParams();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtcodpersona.Text = (Request.QueryString["codpersona"] != null) ? Request.QueryString["codpersona"].ToString() : "-1";              
                txtdebcre.Text = (Request.QueryString["debcre"] != null) ? Request.QueryString["debcre"].ToString() : "-1";
                txtfechacorte.Text = (Request.QueryString["fechacort"] != null) ? Request.QueryString["fechacort"].ToString() : "-1";
                txtcodigoalm.Text = (Request.QueryString["codalmacen"] != null) ? Request.QueryString["codalmacen"].ToString() : "-1";
            }
        }

        [WebMethod]
        public static string GetCabecera()
        {
         /*   StringBuilder html = new StringBuilder();           
            html.AppendLine(new Select { id = "cmbNOMBRE", etiqueta = "Nombre", clase = Css.large,withempty=true ,diccionario = Dictionaries.GetPersonas() }.ToString());
            html.AppendLine(new Select { id = "cmbALMACEN", etiqueta = "Alamcen", clase = Css.large,withempty=true ,diccionario = Dictionaries.GetAlmacen() }.ToString());
            html.AppendLine(new Input { id = "cmbCORTE", etiqueta = "Corte", datepicker = true, datetimevalor = DateTime.Now, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtDEBCRE", visible = false }.ToString());
         //   html.AppendLine(new Input { id = "cmbFECHA_C", etiqueta = "Fecha", datepicker = true, datetimevalor = DateTime.Now, clase = Css.large }.ToString()); 
            return html.ToString();
            */
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(2, 2);
            tdatos.rows[0].cells[0].valor = "Nombre:";
            tdatos.rows[0].cells[1].valor =new Select { id = "cmbNOMBRE", clase = Css.large,withempty=true ,diccionario = Dictionaries.GetPersonas() }.ToString();
            
           tdatos.rows[1].cells[0].valor = "Corte:";
           tdatos.rows[1].cells[1].valor = new Input { id = "cmbCORTE", datepicker = true, datetimevalor = DateTime.Now, clase = Css.large }.ToString() + " " + new Input { id = "txtDEBCRE", visible = false }.ToString();
           html.AppendLine(tdatos.ToString());
           html.AppendLine(" </div><!--span6-->");

           html.AppendLine("<div class=\"span6\">");
           HtmlTable tdatosder = new HtmlTable();
           tdatosder.CreteEmptyTable(1,2);           
           tdatosder.rows[0].cells[0].valor = "Alamcen:";
           tdatosder.rows[0].cells[1].valor = new Select { id = "cmbALMACEN", clase = Css.large, withempty = true, diccionario = Dictionaries.GetAlmacen() }.ToString();           
           html.AppendLine(tdatosder.ToString());
           html.AppendLine(" </div><!--span6-->");


           html.AppendLine("</div><!--row-fluid-->");
           return html.ToString(); 
        }

        [WebMethod]
        public static string GetDetalle(object id)
        {
            SetWhereClause(id);
            StringBuilder html = new StringBuilder();
            List<vDEstadoCuenta> lista = new List<vDEstadoCuenta>();
            lista = vDEstadoCuentaBLL.GetAll(parametros, OrderByClause);
            decimal total = 0;
            foreach (vDEstadoCuenta item in lista)
            {
                string clase = "";
                ArrayList array = new ArrayList();
                total += item.valor ?? 0;
                array.Add("");
                array.Add(item.com_fecha);
                array.Add(item.com_doctran);
                array.Add(item.ddo_pago);
                array.Add(item.ddo_fecha_ven);
                if (item.ddo_debcre == Constantes.cCredito)
                {
                    array.Add(0);
                    array.Add(item.monto);
                }
                if (item.ddo_debcre == Constantes.cDebito)
                {
                    array.Add(item.monto);
                    array.Add(0);
                    clase = "rowoscura";
                }
                array.Add(item.saldo);
                array.Add(total);
                string strid = "{\"com_codigo\":\"" + item.com_codigo + "\", \"com_empresa\":\"" + item.com_empresa + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid, clase ));
            }
            return html.ToString();
        }

       /* [WebMethod]
        public static string GetPuntoventa(object objeto)
        {
            StringBuilder html = new StringBuilder();
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object almacen = null;
            tmp.TryGetValue("almacen", out almacen);
            html.AppendLine(new Select { id = "cmbPUNTODEVENTA", etiqueta = null, valor = "", clase = Css.large, diccionario = Dictionaries.GetPuntoVenta(Convert.ToInt32(almacen)) }.ToString());
            return html.ToString();
        }
        */
        /*
        public static string ShowData(Usuarioxempresa obj)
        {
            ArrayList array = new ArrayList();
            array.Add("");
            array.Add(obj.uxe_usuario);
            array.Add(obj.uxe_nombreusuario);            
            array.Add(Conversiones.LogicToString(obj.uxe_estado));
            string strid = "{\"uxe_empresa\":\"" + obj.uxe_empresa + "\", \"uxe_usuario\":\"" + obj.uxe_usuario + "\"}";//ID COMPUESTO
            return HtmlElements.TablaRow(array, strid);
        }
        */
        /*
        public static string ShowObject(Usuarioxempresa obj)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtEMPRESA_key", valor = obj.uxe_empresa_key, visible = false }.ToString());
            html.AppendLine(new Select { id = "cmbUSUARIO", etiqueta = "Usuario", valor = obj.uxe_usuario, clase = Css.large, diccionario = Dictionaries.GetUsuario() }.ToString());
            html.AppendLine(new Input { id = "txtUSUARIO_key", valor = obj.uxe_usuario_key, visible = false }.ToString());
            html.AppendLine(new Select { id = "cmbALMACEN", etiqueta = "Almacen", valor = obj.uxe_almacen, clase = Css.large, diccionario = Dictionaries.GetIDAlmacen() }.ToString());
            html.AppendLine(new Select { id = "cmbPUNTODEVENTA", etiqueta = "Punto de Venta", valor = obj.uxe_puntoventa, clase = Css.large, diccionario = Dictionaries.Empty() }.ToString());
            html.AppendLine(new Input { id = "txtEMPRESAPUNTOVENTA", valor = obj.uxe_empresapuntoventa, visible = false }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.uxe_estado }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }
        */

        public static void SetWhereClause(object obj)
        {
            if (obj != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)obj;
                object obj_per_codigo = null;
                object obj_com_almacen = null;
                object obj_ddo_fecha_emi = null;
                object obj_ddo_debcre = null;
                tmp.TryGetValue("per_codigo", out obj_per_codigo);
                tmp.TryGetValue("com_almacen", out obj_com_almacen);
                tmp.TryGetValue("ddo_fecha_emi", out obj_ddo_fecha_emi);
                tmp.TryGetValue("ddo_debcre", out obj_ddo_debcre);
                int? per_codigo = Convert.ToInt32(obj_per_codigo);
                int? com_almacen = Convert.ToInt32(obj_com_almacen);
                DateTime? ddo_fecha_emi = Convert.ToDateTime(obj_ddo_fecha_emi);
                int? ddo_debcre = Convert.ToInt32(obj_ddo_debcre);
                int contador = 0;
                parametros = new WhereParams();
                List<object> valores = new List<object>();
                if (per_codigo > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_codclipro = {" + contador + "} ";
                    valores.Add(per_codigo);
                    contador++;
                }
                if (com_almacen > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_almacen = {" + contador + "} ";
                    valores.Add(com_almacen);
                    contador++;
                }
                if (ddo_fecha_emi.HasValue)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " ddo_fecha_emi <= {" + contador + "} ";
                    valores.Add(ddo_fecha_emi);
                    contador++;
                }
         /*       if (ddo_debcre.HasValue)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " ddo_debcre = {" + contador + "} ";
                    valores.Add(ddo_debcre);
                    contador++;
                }
                */
                parametros.valores = valores.ToArray();
            }

        }
        /*
        [WebMethod]
        public static string GetData(object objeto)
        {
            
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<UsuarioxEmpresa> lst = UsuarioxempresaBLL.GetAllByPage(WhereClause, OrderByClause, desde, hasta);
            foreach (UsuarioxEmpresa obj in lst)
            {
                string id = "{\"usuario\":\"" + obj.uxe_usuario + "\", \"empresa\":\"" + obj.uxe_empresa + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.HtmlList(id, ShowData(obj)));
            //    html.AppendLine(HtmlElements.HtmlList(obj.EMPRESA.ToString(), ShowData(obj)));
            }

            return html.ToString();
        }*/
        /*
        [WebMethod]
        public static string ReloadData(object objeto)
        {
            pageIndex = 1;
            return GetData(objeto);
        }

       

        [WebMethod]
        public static string AddObject()
        {
            return ShowObject(new Usuarioxempresa());
        }
        */
        /*
        [WebMethod]
        public static string GetObject(object id)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object empresa = null;
            object usuario = null;
            tmp.TryGetValue("uxe_empresa", out empresa);
            tmp.TryGetValue("uxe_usuario", out usuario);
            Usuarioxempresa obj = new Usuarioxempresa();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.uxe_empresa = int.Parse(empresa.ToString());
                obj.uxe_empresa_key = int.Parse(empresa.ToString());
            }
            obj.uxe_usuario = (string)usuario;
            obj.uxe_usuario_key = (string)usuario;            
            obj = UsuarioxempresaBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }
        */

   /*     public static vDEstadoCuenta GetObjeto(object objeto)
        {
            vDEstadoCuenta obj = new vDEstadoCuenta();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object per_codigo = null;
                object per_nombres = null;
                object per_apellidos = null;
                object per_cupo = null;
                object valor = null;

                tmp.TryGetValue("per_codigo", out per_codigo);
                tmp.TryGetValue("per_nombres", out per_nombres);
                tmp.TryGetValue("per_apellidos", out per_apellidos);
                tmp.TryGetValue("per_cupo", out per_cupo);
                tmp.TryGetValue("valor", out valor);


                obj.com_codigo = Convert.ToInt32(per_codigo);
                obj.com_empresa = Convert.ToInt32(per_nombres) + "";
                obj.per_apellidos = (string)per_apellidos;
                obj.per_cupo = Convert.ToDecimal(per_cupo);
                obj.valor = Convert.ToDecimal(valor);
                

            }

            return obj;
        }
        /*

        [WebMethod]
        public static string GetSearch()
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class='pull-left'>");
            //  html.AppendLine(HtmlElements.Input("Id", "txtCODIGO_S", "", HtmlElements.small, false));
            //  html.AppendFormat("<input id = '{0}' type=\"hidden\" >", "txtCODIGO_key_S");
            html.AppendLine(HtmlElements.Input("Usuario", "txtUSUARIO_S", "", HtmlElements.medium, false));
            html.AppendLine(HtmlElements.Input("Empresa", "txtEMPRESA_S", "", HtmlElements.medium, false));
            //           html.AppendLine(HtmlElements.Input("Empresa", "txtEMPRESA_S", "", HtmlElements.medium, false));
            html.AppendLine(HtmlElements.SelectBoolean("--Activo--", "cmbESTADO_S", "", HtmlElements.medium));
            html.AppendLine("</div>");
            html.AppendLine("<div class='pull-right'>");
            html.AppendLine(new Boton { refresh=true }.ToString());
            html.AppendLine(new Boton { clean = true }.ToString());
            html.AppendLine("</div>");
            return html.ToString();
        }
        */
        /*
        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Usuarioxempresa());
        }

        */
        /*
        [WebMethod]
        public static string SaveObject(object objeto)
        {
          
           

            if (obj.uxe_usuario_key != "")
            {
                if (UsuarioxempresaBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (UsuarioxempresaBLL.Insert(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }

        }

        [WebMethod]
        public static string DeleteObjects(object objetos)
        {
           
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            transaction.BeginTransaction();


            foreach (object item in (Array)objetos)
            {
                Usuarioxempresa obj = GetObjeto(item);
                UsuarioxempresaBLL.Delete(transaction, obj);

            }
            transaction.Commit();





            return "OK";



        }*/
    }
}