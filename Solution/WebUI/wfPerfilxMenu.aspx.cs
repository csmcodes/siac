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
    public partial class wfPerfilxMenu : System.Web.UI.Page
    {
        
      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                
            }
        }
        [WebMethod]
        public static string GetCabecera()
        {

            StringBuilder html = new StringBuilder();
         
            html.AppendLine(new Select { id = "cmbPERFIL", etiqueta = "Perfil", clase = Css.large, diccionario = Dictionaries.GetPerfil() }.ToString());
            return html.ToString();
        }
        [WebMethod]
        public static string GetDetalle(object id)
        {
            StringBuilder html = new StringBuilder();
            List<Perfilxmenu> lista = new List<Perfilxmenu>();
            lista = PerfilxmenuBLL.GetAll("pxm_perfil like '%" + id.ToString() + "%'", "");
            foreach (Perfilxmenu item in lista)
            {
                ArrayList array = new ArrayList();
                array.Add("");
                array.Add(item.pxm_nombremenu);
                array.Add(Conversiones.LogicToString(item.pxm_agregar));
                array.Add(Conversiones.LogicToString(item.pxm_modificar));
                array.Add(Conversiones.LogicToString(item.pxm_eliminar));
                array.Add(Conversiones.LogicToString(item.pxm_estado));
                string strid = "{\"pxm_perfil\":\"" + item.pxm_perfil + "\", \"pxm_menu\":\"" + item.pxm_menu + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));
            }

            return html.ToString();
        }

      public static string ShowData(Perfilxmenu obj)
        {

            ArrayList array = new ArrayList();
            array.Add("");
            array.Add(obj.pxm_nombremenu);
            array.Add(Conversiones.LogicToString(obj.pxm_agregar));
            array.Add(Conversiones.LogicToString(obj.pxm_modificar));
            array.Add(Conversiones.LogicToString(obj.pxm_eliminar));
            array.Add(Conversiones.LogicToString(obj.pxm_estado));
            string strid = "{\"pxm_perfil\":\"" + obj.pxm_perfil + "\", \"pxm_menu\":\"" + obj.pxm_menu + "\"}";//ID COMPUESTO
            return HtmlElements.TablaRow(array, strid);
        }
        [WebMethod]
      

        public static string ShowObject(Perfilxmenu obj)
        {

            StringBuilder html = new StringBuilder();


            html.AppendLine(new Input { id = "txtMENU_key", valor = obj.pxm_menu_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Select { id = "cmbMENU", etiqueta = "Menu", valor = obj.pxm_menu.ToString(), clase = Css.large, diccionario = Dictionaries.GetMenu("user"), obligatorio = true }.ToString());
            html.AppendLine(new Check { id = "chkAGREGAR", etiqueta = "Agregar ", valor = obj.pxm_agregar }.ToString());
            html.AppendLine(new Check { id = "chkMODIFICAR", etiqueta = "Modificar ", valor = obj.pxm_modificar }.ToString());
            html.AppendLine(new Check { id = "chkELIMINAR", etiqueta = "Eliminar ", valor = obj.pxm_eliminar }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.pxm_estado }.ToString());
         //   html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());

            return html.ToString();
        }


        [WebMethod]
        public static string AddObject()
        {
            return ShowObject(new Perfilxmenu());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object menu = null;
            object perfil = null;
            tmp.TryGetValue("pxm_menu", out menu);
            tmp.TryGetValue("pxm_perfil", out perfil);
            Perfilxmenu obj = new Perfilxmenu();
            if (menu != null && !menu.Equals(""))
            {
                obj.pxm_menu = Convert.ToInt32(menu.ToString());
                obj.pxm_menu_key = Convert.ToInt32(menu.ToString());
            }

            obj.pxm_perfil = (string)perfil;
            obj.pxm_perfil_key = (string)perfil;
            obj = PerfilxmenuBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Perfilxmenu GetObjeto(object objeto)
        //{
        //    Perfilxmenu obj = new Perfilxmenu();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object menu = null;
        //        object menukey = null;
        //        object perfil = null;
        //        object perfilkey = null;
        //        object agregar = null;
        //        object modificar = null;
        //        object eliminar = null;
        //        object activo = null;
        //        object nombremenu = null;
        //        object descripcionperfil = null;
        //        tmp.TryGetValue("pxm_menu", out menu);
        //        tmp.TryGetValue("pxm_menu_key", out menukey);
        //        tmp.TryGetValue("pxm_perfil", out perfil);
        //        tmp.TryGetValue("pxm_perfil_key", out perfilkey);
        //        tmp.TryGetValue("pxm_agregar", out agregar);
        //        tmp.TryGetValue("pxm_modificar", out modificar);
        //        tmp.TryGetValue("pxm_eliminar", out eliminar);
        //        tmp.TryGetValue("pxm_estado", out activo);
        //        tmp.TryGetValue("pxm_nombremenu", out nombremenu);
        //        tmp.TryGetValue("pxm_descripcionperfil", out descripcionperfil);
        //        if (menu != null && !menu.Equals(""))
        //        {
        //            obj.pxm_menu = Convert.ToInt32(menu.ToString());
        //        }
        //        if (menukey != null && !menukey.Equals(""))
        //        {
        //            obj.pxm_menu_key = Convert.ToInt32(menukey.ToString());
        //        }
        //        obj.pxm_perfil = (string)perfil;
        //        obj.pxm_perfil_key = (string)perfilkey;
        //        obj.pxm_agregar = Convert.ToInt32(agregar);
        //        obj.pxm_modificar = Convert.ToInt32(modificar);
        //        obj.pxm_eliminar = Convert.ToInt32(eliminar);
        //        obj.pxm_estado = (int?)activo;
        //        obj.crea_usr = "admin";
        //        obj.crea_fecha = DateTime.Now;
        //        obj.mod_usr = "admin";
        //        obj.mod_fecha = DateTime.Now;
        //        obj.pxm_nombremenu = (string)nombremenu;
        //        obj.pxm_descripcionperfil = (string)descripcionperfil;
        //    }
        //    return obj;
        //}




        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Perfilxmenu());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Perfilxmenu obj = new Perfilxmenu(objeto);
            obj.pxm_menu_key=obj.pxm_menu;
            obj.pxm_perfil_key = obj.pxm_perfil;
            /*if (obj.pxm_menu_key > 0)
            {
                if (PerfilxmenuBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {*/
                if (PerfilxmenuBLL.Insert(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }

        

        [WebMethod]
        public static string DeleteObjects(object objetos)
        {
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            transaction.BeginTransaction();


            foreach (object item in (Array)objetos)
            {
                Perfilxmenu obj = new Perfilxmenu(item);
                obj.pxm_menu_key = obj.pxm_menu;
                obj.pxm_perfil_key = obj.pxm_perfil;
                PerfilxmenuBLL.Delete(transaction, obj);

            }
            transaction.Commit();
            return "OK";

        }
    }
}