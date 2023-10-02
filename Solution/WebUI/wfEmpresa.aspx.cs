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
using Packages;
namespace WebUI
{
    public partial class wfEmpresa : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "emp_codigo";
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


        public static string ShowData(Empresa obj)
        {
       
                  return new HtmlObjects.ListItem { titulo = new string[] { obj.emp_nombre }, descripcion = new string[] {  }, logico = new LogicItem[] { new LogicItem("Activos", obj.emp_estado) } }.ToString();              
        }

        public static string ShowObject(Empresa obj)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.emp_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.emp_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "RUC / CI", placeholder = "RUC / CI", valor = obj.emp_id, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.emp_nombre, clase = Css.large }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.emp_estado }.ToString());
            html.AppendLine(new Select { id = "cmbIMP_VENTA", etiqueta = "Impuesto Venta", valor = obj.emp_imp_venta.ToString(), clase = Css.large, diccionario = Dictionaries.GetImpuesto() }.ToString());
            html.AppendLine(new Select { id = "cmbIMP_COMPRA", etiqueta = "Impuesto Compra", valor = obj.emp_imp_compra.ToString(), clase = Css.large, diccionario = Dictionaries.GetImpuesto() }.ToString());
            html.AppendLine(new Select { id = "cmbINFORMANTE", etiqueta = "Informante", valor = obj.emp_informante.ToString(), clase = Css.large, diccionario = Dictionaries.GetPersonas() }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();

        }


        public static void SetWhereClause(Empresa obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();



            if (obj.emp_codigo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " emp_codigo = {" + contador + "} ";
                valores.Add(obj.emp_codigo);
                contador++;
            }

            if (!string.IsNullOrEmpty(obj.emp_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " emp_nombre like {" + contador + "} ";
                valores.Add("%" + obj.emp_nombre + "%");
                contador++;

            }

            if (!string.IsNullOrEmpty(obj.emp_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " emp_id like {" + contador + "} ";
                valores.Add("%" + obj.emp_id + "%");
                contador++;

            }
            if (obj.emp_estado.HasValue)
            {

                parametros.where += ((parametros.where != "") ? " and " : "") + " emp_estado = {" + contador + "} ";
                valores.Add(obj.emp_estado.Value);
                contador++;

            }
            parametros.valores = valores.ToArray(); 
    
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
         //  decimal saldo = General.SaldoCuenta("m", 3, 1, 1,1, 1, 10,5,DateTime.Now);

            SetWhereClause(new Empresa(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Empresa> lst = EmpresaBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Empresa obj in lst)
            {
                html.AppendLine(new HtmlList { id = obj.emp_codigo.ToString(), content = ShowData(obj) }.ToString());
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
            return ShowObject(new Empresa());
        }


        [WebMethod]
        public static string GetObject(string id)
        {
            Empresa obj = new Empresa();
            obj.emp_codigo = int.Parse(id.ToString());
            obj.emp_codigo_key = int.Parse(id.ToString());
            obj = EmpresaBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);          
        }


        public static Empresa GetObjeto(object objeto)
        {
            Empresa obj = new Empresa();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object codigo = null;
                object codigokey = null;
                object nombre = null;
                object id = null;
                object imp_compra = null;
                object imp_venta = null;
                object emp_informante = null;
                object activo = null;
                tmp.TryGetValue("emp_codigo", out codigo);
                tmp.TryGetValue("emp_codigo_key", out codigokey);
                tmp.TryGetValue("emp_id", out id);
                tmp.TryGetValue("emp_imp_compra", out imp_compra);
                tmp.TryGetValue("emp_imp_venta", out imp_venta);
                tmp.TryGetValue("emp_informante", out emp_informante);
                tmp.TryGetValue("emp_nombre", out nombre);
                tmp.TryGetValue("emp_estado", out activo);
                if (codigo != null && !codigo.Equals("") )
                {
                    obj.emp_codigo = int.Parse(codigo.ToString());

                }
                if (codigokey != null && !codigokey.Equals(""))
                {
                    obj.emp_codigo_key = int.Parse(codigokey.ToString()); ;

                }
                obj.emp_nombre = (string)nombre;
                obj.emp_id = (string)id;
                obj.emp_imp_compra = Convert.ToInt32(imp_compra); 
                obj.emp_imp_venta = Convert.ToInt32(imp_venta);
                obj.emp_informante = Convert.ToInt32(emp_informante); 
                obj.emp_estado = (int?)activo;
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
       
            html.AppendLine(new Input { id = "txtNOMBRE_S", placeholder = "Desripcion", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtID_S", placeholder = "RUC/CI", clase = Css.large }.ToString());            
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
            return ShowObject(new Empresa());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Empresa obj = new Empresa(objeto);
            obj.emp_codigo_key = obj.emp_codigo;
            if (obj.emp_codigo_key > 0)
            {
                if (EmpresaBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (EmpresaBLL.Insert(obj) > 0)
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
            Empresa obj = new Empresa(objeto);
            obj.emp_codigo_key = obj.emp_codigo;
            if (EmpresaBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";

        }
    }
}