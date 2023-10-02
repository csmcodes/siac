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
using System.IO;
using System.Text;
using System.Reflection;
namespace WebUI
{
 
    public partial class wfAdmUsuario : System.Web.UI.Page
    {
        

        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "cpr_nombre";
        protected static string WhereClause = "";
        protected static WhereParams parametros;
        static string codigo= null ;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 codigo = Request.QueryString["codigo"];
                pageIndex = 1;
                pageSize = 10;
                
            }
        }
        
        [WebMethod]
        public static string GetCabecera()
        {
         //  SetWhereClause(new Usuario(objeto));
           List<Usuario> lst = UsuarioBLL.GetAll("usr_id = " + "'" +codigo + "'", "");
            
           
           
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(2, 2);
            tdatos.rows[0].cells[0].valor = "Usuario:";
            tdatos.rows[0].cells[1].valor = new Input { id = "cmbUSUARIO_S", clase = Css.medium, placeholder = "producto1", valor = lst[0].usr_id ,habilitado =false}.ToString();
            tdatos.rows[1].cells[0].valor = "Nombre:";
            tdatos.rows[1].cells[1].valor = new Input { id = "cmbNOMBRE_S", clase = Css.medium, placeholder = "producto1", valor = lst[0].usr_nombres, habilitado = false }.ToString();
            
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }
        // devuelve la cabezera de la tabla usuarioempresa
        [WebMethod]
        public static string GetDetalle()
        {
            pageIndex = 1;
            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tddatos";
            tdatos.invoice = true;
            tdatos.clase = "scrolltable";
            tdatos.AddColumn("Empresa", "", "", "");
            tdatos.AddColumn("Almacen", "", "", "");
            tdatos.AddColumn("Punto venta", "", "", "");
                    
            tdatos.editable = false;           
            html.AppendLine(tdatos.ToString());
            return html.ToString();
        }


        // devuelve las cabezeras de la tabla usrdoc
        [WebMethod]

        public static string GetDetalleUsr(object id)
        {

            StringBuilder html = new StringBuilder();
            List<Usrdoc> lista = new List<Usrdoc>();
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object usuario = null;
            object empresa = null;
            tmp.TryGetValue("udo_usuario", out usuario);
            tmp.TryGetValue("udo_empresa", out empresa);
            lista = UsrdocBLL.GetAll(new WhereParams("udo_usuario = {0} and udo_empresa = {1} ", usuario, int.Parse(empresa.ToString())), "");


            foreach (Usrdoc item in lista)
            {
                ArrayList array = new ArrayList();
                array.Add("");
                array.Add(item.udo_idtipodoc);
                array.Add(item.udo_idctipocom);
                array.Add(item.udo_nivel_aprb);
                array.Add(item.udo_estado);
                string strid = "{\"udo_usuario\":\"" + item.udo_usuario + "\", \"udo_empresa\":\"" + item.udo_empresa + "\", \"udo_tipodoc\":\"" + item.udo_tipodoc + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));
            }

            return html.ToString();


        }

        // fin del metodo getdetalleusr
        [WebMethod]
        public static string GetDetalleData(object objeto) // llena la tabla con datos
        {           
            SetWhereClause(new Usuarioxempresa(objeto));       
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;

            //List<Usuarioxempresa> lst = UsuarioxempresaBLL.GetAllByPage(parametros, "uxe_usuario", desde, hasta);
            List<Usuarioxempresa> lst = UsuarioxempresaBLL.GetAllByPage("usr_id = " + "'" + codigo + "'", "uxe_usuario", desde, hasta);
            StringBuilder html = new StringBuilder();
            foreach (Usuarioxempresa item in lst)
            {
                ArrayList array = new ArrayList();
                array.Add(item.uxe_nombreempresa);
                array.Add(item.uxe_nombrealmacen);
                array.Add(item.uxe_nombrepuntoventa);


                string strid = "{\"uxe_usuario\":\"" + item.uxe_usuario + "\", \"uxe_empresa\":\"" + item.uxe_empresa + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));   
            }           
            return html.ToString();
        }

        //hace la busqueda del usuario segun el id


        public static void SetWhereClause(Usuarioxempresa obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
          
            if (!string.IsNullOrEmpty(obj.uxe_nombreusuario))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (pro_nombre like  {" + contador + "} or pro_id like  {" + contador + "}  )";
                valores.Add("%" + obj.uxe_nombreusuario + "%");
                contador++;
            }
            parametros.valores = valores.ToArray();
        }

        [WebMethod]
        public static string GetAdmUsuario(object objeto)
        {
           
            Usuarioxempresa idusuario = new Usuarioxempresa(objeto);
            idusuario.uxe_usuario_key = idusuario.uxe_usuario;

            StringBuilder html = new StringBuilder();     
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span11\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Empresa:";
            tdatos.rows[0].cells[1].valor = new Select { id = "cmbEMPRESA_P", valor = idusuario.uxe_empresa,clase = Css.large, diccionario = Dictionaries.GetEmpresa(), withempty = true, habilitado = true }.ToString();
            tdatos.rows[1].cells[0].valor = "Almacen:";
            tdatos.rows[1].cells[1].valor = new Select { id = "cmbALMACEN_P", valor = idusuario.uxe_almacen, diccionario = Dictionaries.GetAlmacen(), withempty = true, habilitado = true }.ToString();
            tdatos.rows[2].cells[0].valor = "Punto de venta:";
            tdatos.rows[2].cells[1].valor = new Select { id = "cmbPVENTA_P", diccionario = Dictionaries.Empty() }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_P", visible = false, valor = codigo }.ToString());
            html.AppendLine(" </div><!--span11-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }

        [WebMethod]
        public static String SaveObject(object objeto)
        {
            Usuarioxempresa obj = new Usuarioxempresa(objeto);
            try
            {
                if (UsuarioxempresaBLL.Insert(obj) > 0)
                {
                    return "OK";

                }
            }
            catch (Exception ex)
            {


            }
            return "";
        }
        //metodo que devuelva la cabeceras de edicion 



        [WebMethod]


        public static string ShowObject(Usrdoc obj)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span11\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(4,2);
            tdatos.rows[0].cells[0].valor = "Tipo Documento";
            tdatos.rows[0].cells[1].valor = new Select { id = "cmbTIPODOC", valor = obj.udo_tipodoc.ToString(), clase = Css.large, diccionario = Dictionaries.GetTipodoc() }.ToString();
            tdatos.rows[1].cells[0].valor = "Nivel Aprobación";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtNIVELAPROBACION", placeholder = "Nivel Aprovacion", valor = obj.udo_nivel_aprb.ToString(), clase = Css.large }.ToString();
            tdatos.rows[2].cells[0].valor = "Sigla";
            tdatos.rows[2].cells[1].valor = new Select { id = "cmbCTIPOCOM", valor = obj.udo_ctipocom.ToString(), clase = Css.large, diccionario = Dictionaries.Empty() }.ToString();
            tdatos.rows[3].cells[0].valor = "Activo";
            tdatos.rows[3].cells[1].valor = new Check { id = "chkESTADO", valor = obj.udo_estado }.ToString();
            html.AppendLine(tdatos.ToString());
           // html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            html.AppendLine(new Input { id = "txtEMPRESA_key", valor = obj.udo_empresa_key, visible = false }.ToString());
             html.AppendLine(new Input { id = "txtUSUARIO_key", valor = obj.udo_usuario_key, visible = false }.ToString());
             html.AppendLine(new Input { id = "txtTIPODOC_key", valor = obj.udo_tipodoc_key.ToString(), visible = false }.ToString());
            html.AppendLine(" </div><!--span11-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }




        //fin de metodo de ediccion  

        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Usrdoc());
        }

        //metodo que guarda los datos del usuario 
        [WebMethod]
        public static string SaveObjectUsr(object objeto)
        {

            Usrdoc obj = new Usrdoc(objeto);
            obj.udo_empresa_key = obj.udo_empresa;
            obj.udo_tipodoc_key = obj.udo_tipodoc;
            obj.udo_usuario_key = obj.udo_usuario;
           /* if (obj.udo_empresa_key > 0)
            {
                if (UsrdocBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {*/
            try{
                if (UsrdocBLL.Insert(obj) > 0)
                {
                    return ShowData(obj);


                }
              
                  
            }
            catch (Exception ex)
            {
                return "ERROR";

            }
            return "";
        //    }

        }


        public static string ShowData(Usrdoc obj)
        {
            ArrayList array = new ArrayList();
            array.Add("");
            array.Add(obj.udo_idtipodoc);
            array.Add(obj.udo_idctipocom);
            array.Add(obj.udo_nivel_aprb);
            array.Add(obj.udo_estado);
          
            string strid = "{\"udo_usuario\":\"" + obj.udo_usuario + "\", \"udo_empresa\":\"" + obj.udo_empresa + "\", \"udo_tipodoc\":\"" + obj.udo_tipodoc + "\"}";//ID COMPUESTO
            return HtmlElements.TablaRow(array, strid);
        }


        //metodo que devuelve las siglas dependiendo lo selecionado

        [WebMethod]
        public static string GetSiglas(object objeto)
        {
            StringBuilder html = new StringBuilder();
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object tipodoc = null;
            tmp.TryGetValue("udo_tipodoc", out tipodoc);
            html.AppendLine(new Select { id = "cmbCTIPOCOM", etiqueta = null, valor = "", clase = Css.large, diccionario = Dictionaries.GetCtipocom(Convert.ToInt32(tipodoc)) }.ToString());
            return html.ToString();
        }

        //fin del metodo GetSiglas

        //metodo para eliminar datos de la tablas usrdoc


        [WebMethod]
        public static string DeleteObjects(object objetos)
        {
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            transaction.BeginTransaction();
            foreach (object item in (Array)objetos)
            {
                Usrdoc obj = new Usrdoc(item);
                obj.udo_empresa_key = obj.udo_empresa;
                obj.udo_tipodoc_key = obj.udo_tipodoc;
                obj.udo_usuario_key = obj.udo_usuario;
                UsrdocBLL.Delete(transaction, obj);
            }
            transaction.Commit();
            return "OK";

        }


        [WebMethod]
        public static string RemoveObjects(object objeto)
        {
           

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                Array array = (Array)objeto;
                foreach (Object item in array)
                {
                    if (item != null)
                    {
                      
                        Usuarioxempresa obj = new Usuarioxempresa(item);
                        UsuarioxempresaBLL.Delete(transaction,obj);
                     }                    
                }
                
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return "-1";
            }
            return "OK";
            
        }
    }
}