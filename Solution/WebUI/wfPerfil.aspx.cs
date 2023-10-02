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
    public partial class wfPerfil : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "per_id";
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


        public static string ShowData(Perfil obj)
        {
          
            return new HtmlObjects.ListItem { titulo = new string[] { obj.per_descripcion }, descripcion = new string[] { obj.per_id.ToString() }, logico = new LogicItem[] { new LogicItem("Activos", obj.per_estado) } }.ToString();              

        }

        public static string ShowObject(Perfil obj)
        {
                                   
            StringBuilder html = new StringBuilder();

            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.per_id, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtID_key", valor = obj.per_id_key, visible = false }.ToString());           
            html.AppendLine(new Input { id = "txtDESCRIPCION", etiqueta = "Descripción", placeholder = "Descripción", valor = obj.per_descripcion, clase = Css.large }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.per_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
         
        }

       
        public static void SetWhereClause(Perfil obj)
        {

            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();

            if (!string.IsNullOrEmpty(obj.per_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " per_id like {" + contador + "} ";
                valores.Add("%" + obj.per_id + "%");
                contador++;
            }

            if (!string.IsNullOrEmpty(obj.per_descripcion))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " per_descripcion like {" + contador + "} ";
                valores.Add("%" + obj.per_descripcion + "%");
                contador++;
            }
            if (obj.per_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " per_estado = {" + contador + "} ";
                valores.Add(obj.per_estado.Value);
                contador++;

            }

            parametros.valores = valores.ToArray(); 
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Perfil(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Perfil> lst = PerfilBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Perfil obj in lst)
            {
               
                html.AppendLine(new HtmlList { id = obj.per_id, content = ShowData(obj) }.ToString());
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
            return ShowObject(new Perfil());
        }


        [WebMethod]
        public static string GetObject(string id)
        {
            Perfil obj = new Perfil();
            obj.per_id = id;
            obj.per_id_key = id;
            obj = PerfilBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
           
        }


        //public static Perfil GetObjeto(object objeto)
        //{
        //    Perfil obj = new Perfil();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object login = null;
        //        object loginkey = null;
        //        object password = null;
       
        //        object activo = null;
        //        tmp.TryGetValue("per_id", out login);
        //        tmp.TryGetValue("per_id_key", out loginkey);
        //        tmp.TryGetValue("per_descripcion", out password);
            
        //        tmp.TryGetValue("per_estado", out activo);
        //        obj.per_id = (string)login;
        //        obj.per_id_key = (string)loginkey;
        //       obj.per_descripcion = (string)password;
            
        //        obj.per_estado = (int?)activo;
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
            html.AppendLine(new Input { id = "txtID_S", placeholder = "Id", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtDESCRIPCION_S", placeholder = "Descripción", clase = Css.large }.ToString());
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
            return ShowObject(new Perfil()); 
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Perfil obj = new Perfil(objeto);
            obj.per_id_key = obj.per_id;

            if (obj.per_id_key != "")
            {
                if (PerfilBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (PerfilBLL.Insert(obj) > 0)
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
            Perfil obj = new Perfil(objeto);
            obj.per_id_key = obj.per_id;
            if (PerfilBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";



        }
    }
}