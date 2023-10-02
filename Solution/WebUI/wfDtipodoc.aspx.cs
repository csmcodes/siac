
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
    public partial class wfDtipodoc : System.Web.UI.Page
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
            html.AppendLine(new Select { id = "cmbTIPODOC_S", etiqueta = "Tipo Documento", clase = Css.large, diccionario = Dictionaries.GetTipodoc() }.ToString());
           
            return html.ToString();
        }
        [WebMethod]
        public static string GetDetalle(object id)
        {
            StringBuilder html = new StringBuilder();
            List<Dtipodoc> lista = new List<Dtipodoc>();
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;

            object tipodoc = null;
            object empresa = null;

            tmp.TryGetValue("dtp_tipodoc", out tipodoc);
            tmp.TryGetValue("dtp_empresa", out empresa);
            lista = DtipodocBLL.GetAll(new WhereParams("dtp_tipodoc = {0} and dtp_empresa = {1} ", int.Parse(tipodoc.ToString()), int.Parse(empresa.ToString())), "");
            foreach (Dtipodoc item in lista)
            {
                ArrayList array = new ArrayList();
                array.Add("");

                array.Add(item.dtp_ctipocomid);
                array.Add(Conversiones.LogicToString(item.dtp_estado));
                
                string strid = "{\"dtp_tipodoc\":\"" + item.dtp_tipodoc + "\", \"dtp_empresa\":\"" + item.dtp_empresa + "\", \"dtp_ctipocom\":\"" + item.dtp_ctipocom + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));
            }

            return html.ToString();
        }

        public static string ShowData(Dtipodoc obj)
        {
            ArrayList array = new ArrayList();
            array.Add("");

            array.Add(obj.dtp_ctipocomid);
            array.Add(Conversiones.LogicToString(obj.dtp_estado));
            string strid = "{\"dtp_tipodoc\":\"" + obj.dtp_tipodoc + "\", \"dtp_empresa\":\"" + obj.dtp_empresa + "\", \"dtp_ctipocom\":\"" + obj.dtp_ctipocom + "\"}";//ID COMPUESTO
            return HtmlElements.TablaRow(array, strid);
        }
        [WebMethod]


        public static string ShowObject(Dtipodoc obj)
        {

            StringBuilder html = new StringBuilder();
          //  html.AppendLine(new Input { id = "txtCODIGO", valor = obj.dtp_tipodoc.ToString(), visible = false }.ToString());
            //
            html.AppendLine(new Select { id = "cmbCTIPOCOM", etiqueta = "Sigla", valor = obj.dtp_ctipocom.ToString(), clase = Css.large, diccionario = Dictionaries.GetCtipocom() }.ToString());
            html.AppendLine(new Input { id = "txtTIPODOC_key", valor = obj.dtp_tipodoc_key.ToString(), visible = false }.ToString());
            
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.dtp_estado }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        [WebMethod]
        public static string AddObject()
        {
            return ShowObject(new Dtipodoc());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object tipodoc = null;
            object empresa = null;
            tmp.TryGetValue("dtp_tipodoc", out tipodoc);
            tmp.TryGetValue("dtp_ctipocom", out codigo);
            tmp.TryGetValue("dtp_empresa", out empresa);
            Dtipodoc obj = new Dtipodoc();

            obj.dtp_tipodoc = Convert.ToInt32(tipodoc);
            obj.dtp_tipodoc_key = Convert.ToInt32(tipodoc);
            obj.dtp_empresa = Convert.ToInt32(empresa);
            obj.dtp_empresa_key = Convert.ToInt32(empresa);
            obj.dtp_ctipocom = Convert.ToInt32(codigo);
            obj.dtp_ctipocom_key = Convert.ToInt32(codigo);
            obj = DtipodocBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Dtipodoc GetObjeto(object objeto)
        //{
        //    Dtipodoc obj = new Dtipodoc();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object codigokey = null;
        //        object empresa = null;
        //        object empresakey = null;
        //        object tipodoc = null;
        //        object tipodockey = null;
                             
        //        object activo = null;
        //        object idctipocom = null;
        //        tmp.TryGetValue("dtp_ctipocom", out codigo);
        //        tmp.TryGetValue("dtp_ctipocom_key", out codigokey);
        //        tmp.TryGetValue("dtp_empresa", out empresa);
        //        tmp.TryGetValue("dtp_empresa_key", out empresakey);
        //        tmp.TryGetValue("dtp_tipodoc", out tipodoc);
        //        tmp.TryGetValue("dtp_tipodoc_key", out tipodockey);
        //       tmp.TryGetValue("dtp_estado", out activo);

        //       tmp.TryGetValue("dtp_idctipocom", out idctipocom);
        //       if (tipodoc != null && !tipodoc.Equals(""))
        //        {

        //            obj.dtp_tipodoc = Convert.ToInt32(tipodoc);
        //        }
        //        if (tipodockey != null && !tipodockey.Equals(""))
        //        {

        //            obj.dtp_tipodoc_key = Convert.ToInt32(tipodockey);
        //        } 
              
        //        obj.dtp_empresa = Convert.ToInt32(empresa);
        //        obj.dtp_empresa_key = Convert.ToInt32(empresakey);
        //        obj.dtp_ctipocom = Convert.ToInt32(codigo);
        //        obj.dtp_ctipocom_key = Convert.ToInt32(codigokey);
        //        obj.dtp_ctipocomid = (string)(idctipocom); 
        //        obj.dtp_estado = (int?)activo;
        //        obj.crea_usr = "admin";
        //        obj.crea_fecha = DateTime.Now;
        //        obj.mod_usr = "admin";
        //        obj.mod_fecha = DateTime.Now;
        //    }
        //    return obj;
        //}




        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Dtipodoc());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Dtipodoc obj = new Dtipodoc(objeto);

            if (DtipodocBLL.Insert(obj) > 0)
            {
                return ShowData(obj);
            }
            else
                return "ERROR";

           /* obj.dtp_ctipocom_key = obj.dtp_ctipocom;
            obj.dtp_empresa_key = obj.dtp_empresa;
            obj.dtp_tipodoc_key = obj.dtp_tipodoc;
            if (obj.dtp_tipodoc_key > 0)
            {
                if (DtipodocBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (DtipodocBLL.Insert(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }*/

        }

        [WebMethod]
        public static string DeleteObjects(object objetos)
        {
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            transaction.BeginTransaction();
            foreach (object item in (Array)objetos)
            {
                Dtipodoc obj = new Dtipodoc(item);
                obj.dtp_ctipocom_key = obj.dtp_ctipocom;
                obj.dtp_empresa_key = obj.dtp_empresa;
                obj.dtp_tipodoc_key = obj.dtp_tipodoc;
                DtipodocBLL.Delete(transaction, obj);

            }
            transaction.Commit();
            return "OK";

        }
    }
}