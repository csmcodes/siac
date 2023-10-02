
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
    public partial class wfUsrdoc : System.Web.UI.Page
    {
        protected static bool flag = true;

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
            html.AppendLine(new Select { id = "cmbUSUARIO_S", etiqueta = "Usuario", clase = Css.large, diccionario = Dictionaries.GetUsuario() }.ToString());           
          
            return html.ToString();
        }


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
       



        [WebMethod]
        public static string GetDetalle(object id)
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
                string strid = "{\"udo_usuario\":\"" + item.udo_usuario + "\", \"udo_empresa\":\"" + item.udo_empresa + "\", \"udo_tipodoc\":\"" + item.udo_tipodoc +"\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));
            }

            return html.ToString();
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
        [WebMethod]


        public static string ShowObject(Usrdoc obj)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtEMPRESA_key", valor = obj.udo_empresa_key, visible = false }.ToString());
      //      html.AppendLine(new Select { id = "cmbUSUARIO", etiqueta = "Usuario", valor = obj.udo_usuario, clase = Css.large, diccionario = Dictionaries.GetUsuario() }.ToString());
            html.AppendLine(new Input { id = "txtUSUARIO_key", valor = obj.udo_usuario_key, visible = false }.ToString());
            html.AppendLine(new Select { id = "cmbTIPODOC", etiqueta = "Tipo Doc", valor = obj.udo_tipodoc.ToString(), clase = Css.large, diccionario = Dictionaries.GetTipodoc() }.ToString());           
            html.AppendLine(new Input { id = "txtTIPODOC_key", valor = obj.udo_tipodoc_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtNIVELAPROBACION", etiqueta = "Nivel Aprovacion", placeholder = "Nivel Aprovacion", valor = obj.udo_nivel_aprb.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbCTIPOCOM", etiqueta = "Sigla", valor = obj.udo_ctipocom.ToString(), clase = Css.large, diccionario = Dictionaries.Empty() }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.udo_estado }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }

        [WebMethod]
        public static bool DisablePeriodo()
        {
            return flag;
        }

        [WebMethod]
        public static string AddObject()
        {
            return ShowObject(new Usrdoc());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object tipodoc = null;
            object usuario = null;
            object empresa = null;            
            tmp.TryGetValue("udo_tipodoc", out tipodoc);
            tmp.TryGetValue("udo_usuario", out usuario);
            tmp.TryGetValue("udo_empresa", out empresa);     
            Usrdoc obj = new Usrdoc();
            obj.udo_tipodoc = Convert.ToInt32(tipodoc);
            obj.udo_tipodoc_key = Convert.ToInt32(tipodoc);
            obj.udo_empresa = Convert.ToInt32(empresa);
            obj.udo_empresa_key = Convert.ToInt32(empresa);
            obj.udo_usuario = (string)usuario;
            obj.udo_usuario_key = (string)usuario;            
            obj = UsrdocBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Usrdoc GetObjeto(object objeto)
        //{
        //    Usrdoc obj = new Usrdoc();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object tipodoc = null;
        //        object usuario = null;
        //        object empresa = null;
        //        object tipodockey = null;
        //        object usuariokey = null;
        //        object empresakey = null;
        //        object ctipocom = null;
        //        object nivel = null;
        //        object activo = null;
        //        object idctipocom = null;
        //        object idtipodoc = null;
        //        object nombretipodoc = null;
                   
        //        tmp.TryGetValue("udo_tipodoc", out tipodoc);
        //        tmp.TryGetValue("udo_usuario", out usuario);
        //        tmp.TryGetValue("udo_idctipocom", out idctipocom);
        //        tmp.TryGetValue("udo_idtipodoc", out idtipodoc);
        //        tmp.TryGetValue("udo_nombretipodoc", out nombretipodoc); 
        //        tmp.TryGetValue("udo_empresa", out empresa);
        //        tmp.TryGetValue("udo_tipodoc_key", out tipodockey);
        //        tmp.TryGetValue("udo_usuario_key", out usuariokey);
        //        tmp.TryGetValue("udo_empresa_key", out empresakey);
        //        tmp.TryGetValue("udo_ctipocom", out ctipocom);
        //        tmp.TryGetValue("udo_nivel_aprb", out nivel);
        //        tmp.TryGetValue("udo_estado", out activo);
              
        //        if (empresa != null && !empresa.Equals(""))
        //        {
        //            obj.udo_empresa = Convert.ToInt32(empresa);
        //        }
        //        if (empresakey != null && !empresakey.Equals(""))
        //        {
        //            obj.udo_empresa_key = Convert.ToInt32(empresakey);
        //        }
        //        if (tipodoc != null && !tipodoc.Equals(""))
        //        {
        //            obj.udo_tipodoc = Convert.ToInt32(tipodoc);
        //        }
        //        if (tipodockey != null && !tipodockey.Equals(""))
        //        {
        //            obj.udo_tipodoc_key = Convert.ToInt32(tipodockey);
        //        }
        //        obj.udo_usuario = (string)(usuario);
        //        obj.udo_usuario_key = (string)(usuariokey);
        //        obj.udo_ctipocom =Convert.ToInt32(ctipocom);
        //        obj.udo_nivel_aprb = Convert.ToInt32(nivel);
        //        obj.udo_estado = (int?)activo;
        //        obj.crea_usr = "admin";
        //        obj.crea_fecha = DateTime.Now;
        //        obj.mod_usr = "admin";
        //        obj.mod_fecha = DateTime.Now;

        //        obj.udo_idctipocom = (string)idctipocom;
        //        obj.udo_idtipodoc = (string)idtipodoc;
        //        obj.udo_nombretipodoc = (string)nombretipodoc;
        //    }
        //    return obj;

        //}




        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Usrdoc());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
          
            Usrdoc obj = new Usrdoc(objeto);
            /*obj.udo_empresa_key = obj.udo_empresa;
            obj.udo_tipodoc_key = obj.udo_tipodoc;
            obj.udo_usuario_key = obj.udo_usuario;



            if (obj.udo_empresa_key > 0)
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
                if (UsrdocBLL.Insert(obj) > 0)
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
                Usrdoc obj = new Usrdoc(item);
                obj.udo_empresa_key = obj.udo_empresa;
                obj.udo_tipodoc_key = obj.udo_tipodoc;
                obj.udo_usuario_key = obj.udo_usuario;
                UsrdocBLL.Delete(transaction, obj);
            }
            transaction.Commit();
            return "OK";

        }
    }
}