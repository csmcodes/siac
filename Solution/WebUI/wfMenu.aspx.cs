using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using BusinessObjects;
using Services;

namespace WebUI
{
    public partial class wfMenu : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "CODIGO";
        protected static string WhereClause = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 10;
            }
        }
        public static string ShowData(BusinessObjects.Menu obj)
        {
            return HtmlElements.ListItem( obj.men_nombre,obj.men_formulario, obj.men_estado);

        }

        public static string ShowObject(BusinessObjects.Menu obj)
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine(HtmlElements.InputHidden("txtCODIGO", obj.men_id.ToString()));
            html.AppendLine(HtmlElements.InputHidden("txtCODIGO_key", obj.men_id_key.ToString()));
            html.AppendLine(HtmlElements.LabelInput("NOMBRE", "txtNOMBRE", obj.men_nombre, HtmlElements.small, "", true));
            html.AppendLine(HtmlElements.LabelInput("FORMULARIO", "txtFORMULARIO", obj.men_formulario, HtmlElements.large, "", true));
            html.AppendLine(HtmlElements.LabelInput("IMAGEN", "txtIMAGEN", obj.men_imagen, HtmlElements.large, "", true));
            html.AppendLine(HtmlElements.LabelInput("PADRE", "txtPADRE", obj.men_padre.ToString(), HtmlElements.large, "", true));
            html.AppendLine(HtmlElements.LabelInput("ORDEN", "txtORDEN", obj.men_orden.ToString(), HtmlElements.large, "", true));
            html.AppendLine(HtmlElements.LabelCheck("Activo", "chkESTADO", obj.men_estado));
            html.AppendLine(HtmlElements.SubmitButton("Guardar", "SaveObj();return false;"));
            html.AppendLine(HtmlElements.Auditoria(obj.crea_usr, obj.crea_fecha, obj.mod_usr, obj.mod_fecha));
            return html.ToString();
        }


        public static void SetWhereClause(BusinessObjects.Menu obj)
        {
            WhereClause = "";        
            if (!string.IsNullOrEmpty(obj.men_nombre))
                WhereClause += ((WhereClause != "") ? " AND " : "") + " NOMBRE LIKE '%" + obj.men_nombre + "%'";
            if (!string.IsNullOrEmpty(obj.men_formulario))
                WhereClause += ((WhereClause != "") ? " AND " : "") + " FORMULARIO LIKE '%" + obj.men_formulario + "%'";
            if (obj.men_estado.HasValue)
                WhereClause += ((WhereClause != "") ? " AND " : "") + " ESTADO = " + obj.men_estado.Value;
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(GetObjeto(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<BusinessObjects.Menu> lst = MenuBLL.GetAllByPage(WhereClause, OrderByClause, desde, hasta);
            foreach (BusinessObjects.Menu obj in lst)
            {
                html.AppendLine(HtmlElements.HtmlList(obj.men_id.ToString(), ShowData(obj)));
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
            return ShowObject(new BusinessObjects.Menu());
        }


        [WebMethod]
        public static string GetObject(string id)
        {
            BusinessObjects.Menu obj = new BusinessObjects.Menu();
            obj.men_id = int.Parse(id.ToString());
            obj.men_id_key = int.Parse(id.ToString());
            obj = MenuBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        public static BusinessObjects.Menu GetObjeto(object objeto)
        {
            BusinessObjects.Menu obj = new BusinessObjects.Menu();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object codigo = null;
                object codigokey = null;
                object nombre = null;
                object activo = null;
                object formulario = null;
                object imagen = null;
                object padre = null;
                object orden = null;
                tmp.TryGetValue("CODIGO", out codigo);
                tmp.TryGetValue("CODIGO_key", out codigokey);
                tmp.TryGetValue("NOMBRE", out nombre);
                tmp.TryGetValue("FORMULARIO", out formulario);
                tmp.TryGetValue("IMAGEN", out imagen);
                tmp.TryGetValue("PADRE", out padre);
                tmp.TryGetValue("ORDEN", out orden);
                tmp.TryGetValue("ESTADO", out activo);
                if (codigo != null && !codigo.Equals(""))
                {
                    obj.men_id = int.Parse(codigo.ToString());
                }
                if (codigokey != null && !codigokey.Equals(""))
                {
                    obj.men_id_key = int.Parse(codigokey.ToString()); 
                }
                obj.men_nombre = (string)nombre;
                obj.men_estado = (int?)activo;
                obj.men_formulario = (string)formulario;
                obj.men_imagen = (string)imagen;
                if (padre != null && !padre.Equals(""))
                {
                    obj.men_padre = int.Parse(padre.ToString());
                }
                if (orden != null && !orden.Equals(""))
                {
                    obj.men_orden= int.Parse(orden.ToString());
                }
               
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
            html.AppendLine(HtmlElements.Input("Formulario", "txtFORMULARIO_S", "", HtmlElements.medium, false));
            html.AppendLine(HtmlElements.Input("Nombre", "txtNOMBRE_S", "", HtmlElements.medium, false));
            html.AppendLine(HtmlElements.SelectBoolean("--Activo--", "cmbESTADO_S", "", HtmlElements.medium));
            html.AppendLine("</div>");
            html.AppendLine("<div class='pull-right'>");
            html.AppendLine(HtmlElements.RefreshButton());
            html.AppendLine(HtmlElements.CleanButton());
            html.AppendLine("</div>");
            return html.ToString();
        }

        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new BusinessObjects.Menu());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            BusinessObjects.Menu obj = GetObjeto(objeto);
            if (obj.men_id_key > 0)
            {
                if (MenuBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (MenuBLL.Insert(obj) > 0)
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
            BusinessObjects.Menu obj = GetObjeto(objeto);
            if (MenuBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";


        }
    }
}