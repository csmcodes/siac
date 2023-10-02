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
    public partial class wfUsuarioEmpresa : System.Web.UI.Page
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
           
            html.AppendLine(new Select { id = "cmbEMPRESA", etiqueta = "Empresa", clase = Css.large, diccionario = Dictionaries.GetEmpresa() }.ToString());

            return html.ToString();
        }
        [WebMethod]
        public static string GetDetalle(object id)
        {
            StringBuilder html = new StringBuilder();
            List<Usuarioxempresa> lista = new List<Usuarioxempresa>();
            lista = UsuarioxempresaBLL.GetAll("uxe_empresa = " + id.ToString(), "");
            foreach (Usuarioxempresa item in lista)
            {
                ArrayList array = new ArrayList();
                array.Add("");
                array.Add(item.uxe_usuario);
                array.Add(item.uxe_nombreusuario);               
                array.Add(Conversiones.LogicToString(item.uxe_estado));
                string strid = "{\"uxe_empresa\":\"" + item.uxe_empresa + "\", \"uxe_usuario\":\"" + item.uxe_usuario + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));
            }

            return html.ToString();
        }
        [WebMethod]
        public static string GetPuntoventa(object objeto)
        {
            StringBuilder html = new StringBuilder();
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object almacen = null;
            tmp.TryGetValue("almacen", out almacen);
            html.AppendLine(new Select { id = "cmbPUNTODEVENTA", etiqueta = null, valor = "", clase = Css.large, diccionario = Dictionaries.GetPuntoVenta(Convert.ToInt32(almacen)) }.ToString());
            return html.ToString();
        }


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
/*

        public static void SetWhereClause(UsuarioxEmpresa obj)
        {
            WhereClause = "";
            /*     if (!string.IsNullOrEmpty(obj.CODIGO))
                     WhereClause += " CODIGO like '%" + obj.CODIGO + "%'";*/
      /*      if (!string.IsNullOrEmpty(obj.uxe_usuario))
                WhereClause += ((WhereClause != "") ? " AND " : "") + " uxe_usuario like '%" + obj.uxe_usuario + "%'";
            if (obj.uxe_empresa > 0)
                WhereClause += ((WhereClause != "") ? " AND " : "") + " uxe_empresa = " + obj.uxe_empresa;
            if (obj.uxe_estado.HasValue)
                WhereClause += ((WhereClause != "") ? " AND " : "") + " uxe_estado = " + obj.uxe_estado.Value;
        }

    

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
        }

        [WebMethod]
        public static string ReloadData(object objeto)
        {
            pageIndex = 1;
            return GetData(objeto);
        }

        */

        [WebMethod]
        public static string AddObject()
        {
            return ShowObject(new Usuarioxempresa());
        }


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


        //public static Usuarioxempresa GetObjeto(object objeto)
        //{
        //    Usuarioxempresa obj = new Usuarioxempresa();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object empresa = null;
        //        object empresakey = null;
        //        object usuario = null;
        //        object usuariokey = null;
        //        object activo = null;
        //        object almacen = null;
        //        object puntoventa = null;
        //        object empresapuntoventa = null;
                


        //        object nombreempresa = null;
        //        object nombreusuario = null;
        //        tmp.TryGetValue("uxe_empresa", out empresa);
        //        tmp.TryGetValue("uxe_empresa_key", out empresakey);
        //        tmp.TryGetValue("uxe_usuario", out usuario);
        //        tmp.TryGetValue("uxe_usuario_key", out usuariokey);
        //        tmp.TryGetValue("uxe_estado", out activo);

        //        tmp.TryGetValue("uxe_almacen", out almacen);
        //        tmp.TryGetValue("uxe_puntoventa", out puntoventa);
        //        tmp.TryGetValue("uxe_empresapuntoventa", out empresapuntoventa);

        //        tmp.TryGetValue("uxe_nombreempresa", out nombreempresa);
        //        tmp.TryGetValue("uxe_nombreusuario", out nombreusuario);
        //        if (empresa != null && !empresa.Equals(""))
        //        {
        //            obj.uxe_empresa = int.Parse(empresa.ToString());
        //        }
        //        if (empresakey != null && !empresakey.Equals(""))
        //        {
        //            obj.uxe_empresa_key = int.Parse(empresakey.ToString());
        //        }
        //        obj.uxe_usuario = (string)usuario;
        //        obj.uxe_usuario_key = (string)usuariokey;
        //        obj.uxe_nombreempresa = (string)nombreempresa;
        //        obj.uxe_nombreusuario = (string)nombreusuario;
        //        obj.uxe_almacen = Convert.ToInt32(almacen);
        //        obj.uxe_puntoventa = Convert.ToInt32(puntoventa);
        //        obj.uxe_empresapuntoventa = Convert.ToInt32(empresapuntoventa);
        //        obj.uxe_estado = (int?)activo;
        //        obj.crea_usr = "admin";
        //        obj.crea_fecha = DateTime.Now;
        //        obj.mod_usr = "admin";
        //        obj.mod_fecha = DateTime.Now;

        //    }

        //    return obj;
        //}
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
        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Usuarioxempresa());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Usuarioxempresa obj = new Usuarioxempresa(objeto);
           //obj.uxe_empresa_key=obj.uxe_empresa;
            //obj.uxe_usuario_key=obj.uxe_usuario;

            /*if (string.obj.uxe_usuario_key != "")
            {
                if (UsuarioxempresaBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {*/
                if (UsuarioxempresaBLL.Insert(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            //}

        }

        [WebMethod]
        public static string DeleteObjects(object objetos)
        {
           
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            transaction.BeginTransaction();


            foreach (object item in (Array)objetos)
            {
                Usuarioxempresa obj = new Usuarioxempresa(item);
                obj.uxe_empresa_key = obj.uxe_empresa;
                obj.uxe_usuario_key = obj.uxe_usuario;
                UsuarioxempresaBLL.Delete(transaction, obj);

            }
            transaction.Commit();





            return "OK";

        }
    }
}