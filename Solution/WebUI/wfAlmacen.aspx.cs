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
    public partial class wfAlmacen : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "alm_codigo";
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


        public static string ShowData(Almacen obj)
        {

            string aux = "";
            if (obj.alm_matriz == 0)
                aux = "Sucursal";
            if (obj.alm_matriz == 1)
               aux = "Matriz";
         

            return new HtmlObjects.ListItem { titulo = new string[] { obj.alm_id,"-", obj.alm_nombre }, descripcion = new string[] { aux}, logico = new LogicItem[] { new LogicItem("Activos", obj.alm_estado) } }.ToString();
        }

        public static string ShowObject(Almacen obj)
        {
            Persona persona = new Persona();
            persona.per_empresa = obj.alm_empresa;
            persona.per_empresa_key = obj.alm_empresa;
            if (obj.alm_cliente_def.HasValue)
            {
                persona.per_codigo = obj.alm_cliente_def.Value;
                persona.per_codigo_key = obj.alm_cliente_def.Value;
                persona = PersonaBLL.GetByPK(persona);
            }

            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.alm_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.alm_codigo_key.ToString(), visible = false }.ToString());      
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.alm_id, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.alm_nombre, clase = Css.large }.ToString());           
            html.AppendLine(new Input { id = "txtSUBFIJO", etiqueta = "Subfijo", placeholder = "Subfijo", valor = obj.alm_subfijo, clase = Css.large }.ToString());           
            html.AppendLine(new Input { id = "txtGERENTE", etiqueta = "Gerente", placeholder = "Gerente", valor = obj.alm_gerente, clase = Css.large }.ToString());                    
            html.AppendLine(new Select { id = "cmbPAIS", etiqueta = "Pais", valor = obj.alm_pais.ToString(), clase = Css.large, diccionario = Dictionaries.GetPaises() }.ToString());            
            html.AppendLine(new Select { id = "cmbPROVINCIA", etiqueta = "Provincia", valor = obj.alm_provincia.ToString(), clase = Css.large, diccionario = Dictionaries.Empty() }.ToString());           
            html.AppendLine(new Select { id = "cmbCANTON", etiqueta = "Canton", valor = obj.alm_canton.ToString(), clase = Css.large, diccionario = Dictionaries.Empty() }.ToString());
            html.AppendLine(new Input { id = "txtDIRECCION", etiqueta = "Direccion", placeholder = "Direccion", valor = obj.alm_direccion, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtTELEFONO1", etiqueta = "Telefono 1", placeholder = "Telefono 1", valor = obj.alm_telefono1, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtTELEFONO2", etiqueta = "Telefono 2", placeholder = "Telefono 2", valor = obj.alm_telefono2, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtTELEFONO3", etiqueta = "Telefono 3", placeholder = "Telefono 3", valor = obj.alm_telefono3, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtRUC", etiqueta = "Ruc", placeholder = "Ruc", valor = obj.alm_ruc, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtFAX", etiqueta = "Fax", placeholder = "Fax", valor = obj.alm_fax, clase = Css.large }.ToString());
            //html.AppendLine(new Select { id = "cmbCLIENTE_DEF", etiqueta = "Cliente", valor = obj.alm_cliente_def.ToString(), clase = Css.large, diccionario = Dictionaries.GetPersonas() }.ToString());
            html.AppendLine(new Input { id = "txtCODCLIPRO", etiqueta = "Cliente", valor = persona.per_nombres + " " + persona.per_apellidos, autocomplete = "GetClienteObj", obligatorio = true, clase = Css.medium, placeholder = "Cliente" }.ToString() + " " + new Input { id = "cmbCODCLIPRO", visible = false, valor = obj.alm_cliente_def }.ToString());
            html.AppendLine(new Select { id = "cmbCENTRO", etiqueta = "Centro", valor = obj.alm_centro.ToString(), clase = Css.large, diccionario = Dictionaries.GetCentro() }.ToString());
           //  html.AppendLine(new Select { id = "cmbCUENTACAJA", etiqueta = "Cuenta Caja", valor = obj.alm_cuentacaja.ToString(), clase = Css.large, diccionario = Dictionaries.GetCuentaMovi() }.ToString());
            html.AppendLine(new Input { id = "txtCUENTA", etiqueta = "Cuenta Caja", valor = obj.alm_nombrecuenta, autocomplete = "GetCuentaObj", obligatorio = true, clase = Css.medium, placeholder = "Cuenta Caja" }.ToString() + " " + new Input { id = "txtCODCCUENTA", visible = false, valor = obj.alm_cuentacaja }.ToString());
            html.AppendLine(new Check { id = "chkMATRIZ", etiqueta = "Matriz ", valor = obj.alm_estado, habilitado = !LoadMatriz() }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.alm_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Almacen obj)
        {
            int contador = 0;
            parametros = new WhereParams(); 
            List<object> valores= new List<object>();  

            if (obj.alm_codigo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " alm_codigo = {" + contador + "} ";
                valores.Add(obj.alm_codigo);  
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.alm_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " alm_nombre like {" + contador + "} ";
                valores.Add("%" + obj.alm_nombre + "%");
                contador++;                
            }
            if (!string.IsNullOrEmpty(obj.alm_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " alm_id like {" + contador + "} ";
                valores.Add("%" + obj.alm_id + "%");
                contador++;                                
            }
            if (!string.IsNullOrEmpty(obj.alm_ruc))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " alm_ruc like {" + contador + "} ";
                valores.Add("%" + obj.alm_ruc + "%");
                contador++;                                
                
            }
            if (!string.IsNullOrEmpty(obj.alm_gerente))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " alm_gerente like {" + contador + "} ";
                valores.Add("%" + obj.alm_gerente + "%");
                contador++;                                
                
            }
            if (obj.alm_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " alm_estado = {" + contador + "} ";
                valores.Add(obj.alm_estado.Value);
                contador++;
                
            }
            if (obj.alm_empresa>0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " alm_empresa = {" + contador + "} ";
                valores.Add(obj.alm_empresa);
                contador++;

            }
            parametros.valores = valores.ToArray(); 



     
            
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Almacen(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Almacen> lst = AlmacenBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Almacen obj in lst)
            {
                string id = "{\"alm_codigo\":\"" + obj.alm_codigo + "\", \"alm_empresa\":\"" + obj.alm_empresa + "\"}";//ID COMPUESTO
               
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
            return ShowObject(new Almacen());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("alm_codigo", out codigo);
            tmp.TryGetValue("alm_empresa", out empresa);

            Almacen obj = new Almacen();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.alm_empresa = int.Parse(empresa.ToString());
                obj.alm_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.alm_codigo = int.Parse(codigo.ToString());
                obj.alm_codigo_key = int.Parse(codigo.ToString());
            }

            obj = AlmacenBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        
        [WebMethod]
        public static bool LoadMatriz()
        {
            
                parametros = new WhereParams("alm_matriz = {0} ", 1);
                if (AlmacenBLL.GetRecordCount(parametros, "") > 0)
                {
                   return true ;
                }
                return false;
            
            
        }
        [WebMethod]
        public static string GetSearch()
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class='pull-left'>");              
            html.AppendLine(new Input { id = "txtNOMBRE_S", placeholder = "Nombre", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtID_S", placeholder = "Id", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtRUC_S", placeholder = "RUC", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtGERENTE_S", placeholder = "Gerente", clase = Css.large }.ToString());
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
            return ShowObject(new Almacen());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Almacen obj = new Almacen(objeto);
            obj.alm_empresa_key = obj.alm_empresa;
            obj.alm_codigo_key = obj.alm_codigo;
            if (obj.alm_id.Length != 3)
                return "ERROR";
            if (obj.alm_codigo> 0)
            {
                if (AlmacenBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (AlmacenBLL.Insert(obj) > 0)
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
            Almacen obj = new Almacen(objeto);
            obj.alm_empresa_key = obj.alm_empresa;
            obj.alm_codigo_key = obj.alm_codigo;
            if (AlmacenBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}