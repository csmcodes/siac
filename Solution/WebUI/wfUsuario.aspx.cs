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
using HtmlObjects;
namespace WebUI
{
    public partial class wfUsuario : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "usr_nombres";
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


        public static string ShowData(Usuario obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.usr_nombres }, descripcion = new string[] { obj.usr_descripcionperfil }, logico = new LogicItem[] { new LogicItem("Activos", obj.usr_estado) } }.ToString();              
        }

        public static string ShowObject(Usuario obj)
        {                                   
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id ="txtID", etiqueta = "Id", placeholder = "Id usuario", valor = obj.usr_id, clase = Css.small}.ToString());
            html.AppendLine(new Input{ id ="txtID_key" , valor = obj.usr_id_key, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtMAIL", etiqueta = "Mail", placeholder = "Mail", valor = obj.usr_mail, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRES", etiqueta = "Nombres", placeholder = "Nombres", valor = obj.usr_nombres, clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbPERFIL", etiqueta = "Perfil",  valor = obj.usr_perfil, clase = Css.large, diccionario = Dictionaries.GetPerfil() }.ToString());
            //html.AppendLine(new Input { id = "txtPERFIL", etiqueta = "Perfil", valor = obj.usr_perfil, clase = Css.large, autocomplete = "GetPerfil", info = true }.ToString());
            html.AppendLine(new Input { id = "txtPASSWORD", etiqueta = "Password", placeholder = "Password", valor = obj.usr_password, clase = Css.medium, password = true }.ToString());
            html.AppendLine(new Input { id = "txtFECHA", etiqueta = "Fecha", placeholder = "Fecha Nac", datetimevalor = DateTime.Now , clase = Css.small, datepicker= true}.ToString());            
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo", valor = obj.usr_estado}.ToString());
            html.AppendLine(new Boton { click = "PrintObj();return false;", valor = "Configuracion" }.ToString());
            //html.AppendLine(new Input { id = "txtTIME", etiqueta = "Hora" , timepicker = true, datetimevalor = DateTime.Now, clase = Css.mini }.ToString());  
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }

       
        public static void SetWhereClause(Usuario obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (!string.IsNullOrEmpty(obj.usr_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " usr_id like {" + contador + "} ";
                valores.Add("%" + obj.usr_id + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.usr_nombres))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " usr_nombres like {" + contador + "} ";
                valores.Add("%" + obj.usr_nombres + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.usr_perfil))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " usr_perfil like {" + contador + "} ";
                valores.Add("%" + obj.usr_perfil + "%");
                contador++;
            }

            if (obj.usr_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " usr_estado = {" + contador + "} ";
                valores.Add(obj.usr_estado.Value);
                contador++;
            }
            parametros.valores = valores.ToArray();            
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Usuario(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
           
            StringBuilder html = new StringBuilder();
            List<Usuario> lst = UsuarioBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Usuario obj in lst)
            {
                html.AppendLine(new HtmlList { id = obj.usr_id, content = ShowData(obj) }.ToString());
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
            return ShowObject(new Usuario());
        }

        [WebMethod]
        public static string GetObject(string id)
        {
            Usuario obj = new Usuario();
            obj.usr_id = id;
            obj.usr_id_key = id;            
            obj = UsuarioBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
            //return ShowObject(obj);
        }




        [WebMethod]
        public static string GetSearch()
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class='pull-left'>");
            html.AppendLine(new Input { id = "txtID_S", placeholder = "Id", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE_S", placeholder = "Nombre", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtPERFIL_S", placeholder = "Perfil", clase = Css.large }.ToString());            
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
            return ShowObject(new Usuario()); 
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {            
            
            Usuario obj = new Usuario(objeto);
            
            if (!string.IsNullOrEmpty(obj.usr_id_key))
            {
                obj.usr_id_key = obj.usr_id; 
                if (UsuarioBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (UsuarioBLL.Insert(obj) > 0)
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
            Usuario obj = new Usuario(objeto);
            obj.usr_id_key = obj.usr_id; 
            if (UsuarioBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";



        }
    }
}
