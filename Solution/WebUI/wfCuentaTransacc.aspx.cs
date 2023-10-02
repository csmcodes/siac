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
    public partial class wfCuentaTransacc : System.Web.UI.Page
    {
        protected static int? tclipro;
        protected static int? modulo;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txttclipro.Text = (Request.QueryString["tclipro"] != null) ? Request.QueryString["tclipro"].ToString() : Constantes.cCliente + "";
                tclipro = Convert.ToInt32(txttclipro.Text);
                txtcodmod.Text = (Request.QueryString["modulo"] != null) ? Request.QueryString["modulo"].ToString() : -1+"";
                modulo = Convert.ToInt32(txtcodmod.Text);
            }
        }
        [WebMethod]
        public static string GetCabecera()
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Select { id = "cmbMODULO_C", etiqueta = "Modulo", valor=modulo,clase = Css.large, diccionario = Dictionaries.GetModulos(),habilitado=false }.ToString());
            return html.ToString();
        }
        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Cuetransacc());
        }
        [WebMethod]
        public static string GetTransacc(object objeto)
        {
            StringBuilder html = new StringBuilder();
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object ctr_modulo = null;
            tmp.TryGetValue("ctr_modulo", out ctr_modulo);
            html.AppendLine(new Select { id = "cmbTRANSACC", etiqueta = null, valor = "", clase = Css.large, diccionario = Dictionaries.GetTransacc(Convert.ToInt32(ctr_modulo)) }.ToString());
            return html.ToString();
        }



        [WebMethod]
        public static string GetDetalle(object id)
        {
            StringBuilder html = new StringBuilder();
            List<Cuetransacc> lista = new List<Cuetransacc>();
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object ctr_modulo = null;
            object ctr_empresa = null;
            object ctr_tipo = null;
            tmp.TryGetValue("ctr_modulo", out ctr_modulo);
            tmp.TryGetValue("ctr_empresa", out ctr_empresa);
            tmp.TryGetValue("ctr_tipo", out ctr_tipo);
            lista = CuetransaccBLL.GetAll(new WhereParams("ctr_modulo = {0} and ctr_empresa = {1}  and ctr_tipo={2}", int.Parse(ctr_modulo.ToString()), int.Parse(ctr_empresa.ToString()), int.Parse(ctr_tipo.ToString())), "");
            foreach (Cuetransacc item in lista)
            {
                ArrayList array = new ArrayList();
                array.Add("");
                array.Add(item.ctr_transaccnombre);
                array.Add(item.ctr_catclinombre);
                array.Add(item.ctr_cuenombre);  
                array.Add(Conversiones.LogicToString(item.ctr_estado));
                string strid = "{\"ctr_categoria\":\"" + item.ctr_categoria + "\", \"ctr_empresa\":\"" + item.ctr_empresa + "\", \"ctr_transacc\":\"" + item.ctr_transacc + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));
            }
            return html.ToString();
        }

        public static string ShowData(Cuetransacc obj)
        {
            ArrayList array = new ArrayList();
            array.Add("");
            array.Add(obj.ctr_transaccnombre);
            array.Add(obj.ctr_catclinombre);
            array.Add(obj.ctr_cuenombre);         
            array.Add(Conversiones.LogicToString(obj.ctr_estado));
            string strid = "{\"ctr_categoria\":\"" + obj.ctr_categoria + "\", \"ctr_empresa\":\"" + obj.ctr_empresa + "\", \"ctr_transacc\":\"" + obj.ctr_transacc + "\"}";//ID COMPUESTO
            return HtmlElements.TablaRow(array, strid);
        }
        [WebMethod]


        public static string ShowObject(Cuetransacc obj)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtEMPRESA_key", valor = obj.ctr_empresa_key, visible = false }.ToString());
            html.AppendLine(new Select { id = "cmbCATEGORIA", etiqueta = "Categoria Cliente", valor = obj.ctr_categoria.ToString(), clase = Css.large, diccionario = Dictionaries.GetCatpersona(), obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtCATEGORIA_key", valor = obj.ctr_categoria_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Select { id = "cmbMODULO", etiqueta = "Modulo", valor = obj.ctr_modulo.ToString(), clase = Css.large, diccionario = Dictionaries.GetModulos(), obligatorio = true ,habilitado=false}.ToString());
            html.AppendLine(new Select { id = "cmbTRANSACC", etiqueta = "Transacc", valor = obj.ctr_transacc.ToString(), clase = Css.large, diccionario = Dictionaries.GetTransacc(Convert.ToInt32(obj.ctr_modulo)), obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtTRANSACC_key", valor = obj.ctr_transacc_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCUENTA", etiqueta = "Cuenta Caja", valor = obj.ctr_cuenombre, autocomplete = "GetCuentaObj", obligatorio = true, clase = Css.medium, placeholder = "Cuenta Caja" }.ToString() + " " + new Input { id = "txtCODCCUENTA", visible = false, valor = obj.ctr_cuenta }.ToString());
          //  html.AppendLine(new Select { id = "cmbCUENTA", etiqueta = "Cuenta", valor = obj.ctr_cuenta.ToString(), clase = Css.large, diccionario = Dictionaries.GetCuentaMovi(), obligatorio = true }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.ctr_estado }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }

    
        [WebMethod]
        public static string AddObject()
        {
            return ShowObject(new Cuetransacc());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object ctr_categoria = null;
            object ctr_transacc = null;
            object ctr_empresa = null;
            tmp.TryGetValue("ctr_categoria", out ctr_categoria);
            tmp.TryGetValue("ctr_transacc", out ctr_transacc);
            tmp.TryGetValue("ctr_empresa", out ctr_empresa);
            Cuetransacc obj = new Cuetransacc();
            obj.ctr_categoria = Convert.ToInt32(ctr_categoria);
            obj.ctr_categoria_key = Convert.ToInt32(ctr_categoria);
            obj.ctr_empresa = Convert.ToInt32(ctr_empresa);
            obj.ctr_empresa_key = Convert.ToInt32(ctr_empresa);
            obj.ctr_transacc = Convert.ToInt32(ctr_transacc);
            obj.ctr_transacc_key = Convert.ToInt32(ctr_transacc);            
            obj = CuetransaccBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


    


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Cuetransacc obj =new  Cuetransacc(objeto);
            if (obj.ctr_empresa_key > 0)
            {
                if (CuetransaccBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (CuetransaccBLL.Insert(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }

        }

        [WebMethod]
        public static string DeleteObjects(object objetos)
        {
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            transaction.BeginTransaction();
            foreach (object item in (Array)objetos)
            {
                Cuetransacc obj = new Cuetransacc(item);
                CuetransaccBLL.Delete(transaction, obj);
            }
            transaction.Commit();
            return "OK";

        }
    }
}