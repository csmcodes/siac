
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
    public partial class wfDtipocom : System.Web.UI.Page
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
         //   html.AppendLine(new Select { id = "cmbCTIPOCOM_S", etiqueta = "Ctipocom", multiselect=true,clase = Css.large, diccionario = Dictionaries.GetCtipocom() }.ToString());
            html.AppendLine(new Select { id = "cmbCTIPOCOM_S", etiqueta = "Sigla", clase = Css.large, diccionario = Dictionaries.GetCtipocom() }.ToString());
     //       html.AppendLine(new Input { id = "IDCTIPOCOM",  etiqueta = "Sigla", autocomplete = "GetCtipocomObj", clase = Css.small, obligatorio = true }.ToString());
      //      html.AppendLine( new Input { id = "txtNOMBRERUT",  clase = Css.large, habilitado = false }.ToString() );
       //     html.AppendLine(new Input { id = "cmbCTIPOCOM_S",  visible = false }.ToString());
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



        [WebMethod]
        public static string GetDetalle(object id)
        {
            StringBuilder html = new StringBuilder();
            List<Dtipocom> lista = new List<Dtipocom>();
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object ctipocom = null;
            object empresa = null;
            tmp.TryGetValue("dti_ctipocom", out ctipocom);
            tmp.TryGetValue("dti_empresa", out empresa);
            lista = DtipocomBLL.GetAll(new WhereParams("dti_ctipocom = {0} and dti_empresa = {1} ", int.Parse(ctipocom.ToString()), int.Parse(empresa.ToString())), "");

            Ctipocom parentobj = new Ctipocom();
            parentobj.cti_empresa = Convert.ToInt32(empresa);
            parentobj.cti_empresa_key = Convert.ToInt32(empresa);
            parentobj.cti_codigo = Convert.ToInt32(ctipocom);
            parentobj.cti_codigo_key = Convert.ToInt32(ctipocom);
            parentobj = CtipocomBLL.GetByPK(parentobj);
            if (parentobj.cti_tipo == 0)
                flag = true;
            else
                flag = false;           
            foreach (Dtipocom  item in lista)
            {
                ArrayList array = new ArrayList();
                array.Add("");
                array.Add(item.dti_periodo);
                array.Add(item.dti_idalmacen+ " "+ item.dti_nombrealmacen);
                array.Add(item.dti_idpuntoventa+ " " + item.dti_nombrepuntoventa);
                array.Add(item.dti_numero);
                string strid = "{\"dti_ctipocom\":\"" + item.dti_ctipocom + "\", \"dti_empresa\":\"" + item.dti_empresa + "\", \"dti_almacen\":\"" + item.dti_almacen + "\", \"dti_periodo\":\"" + item.dti_periodo + "\", \"dti_puntoventa\":\"" + item.dti_puntoventa + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));
            }

            return html.ToString();
        }

        public static string ShowData(Dtipocom obj)
        {
            ArrayList array = new ArrayList();
            array.Add("");
            array.Add(obj.dti_periodo);
            array.Add(obj.dti_idalmacen+  " " + obj.dti_nombrealmacen);
            array.Add(obj.dti_idpuntoventa+  " " + obj.dti_nombrepuntoventa);
            array.Add(obj.dti_numero);
            string strid = "{\"dti_ctipocom\":\"" + obj.dti_ctipocom + "\", \"dti_empresa\":\"" + obj.dti_empresa + "\", \"dti_almacen\":\"" + obj.dti_almacen + "\", \"dti_periodo\":\"" + obj.dti_periodo + "\", \"dti_puntoventa\":\"" + obj.dti_puntoventa + "\"}";//ID COMPUESTO
            return HtmlElements.TablaRow(array, strid);
        }
        [WebMethod]


        public static string ShowObject(Dtipocom obj)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtEMPRESA_key", valor = obj.dti_empresa_key, visible = false }.ToString());
            html.AppendLine(new Select { id = "cmbCTIPOCOM", etiqueta = "Sigla", valor = obj.dti_ctipocom.ToString(), clase = Css.large, diccionario = Dictionaries.GetCtipocom(), obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtCTIPOCOM_key", valor = obj.dti_ctipocom_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtPERIODO", etiqueta = "Periodo", placeholder = "Periodo", valor = obj.dti_periodo.ToString(), clase = Css.large,obligatorio=true }.ToString());
            html.AppendLine(new Input { id = "txtPERIODO_key", valor = obj.dti_periodo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Select { id = "cmbALMACEN", etiqueta = "Almacen", valor = obj.dti_almacen.ToString(), clase = Css.large, diccionario = Dictionaries.GetIDAlmacen(), obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtALMACEN_key", valor = obj.dti_almacen_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Select { id = "cmbPUNTODEVENTA", etiqueta = "Punto de venta", valor = obj.dti_puntoventa.ToString(), clase = Css.large, diccionario = Dictionaries.Empty(), obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtPUNTODEVENTA_key", valor = obj.dti_puntoventa_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtNUMERO", etiqueta = "Numero", placeholder = "Numero", valor = obj.dti_numero.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.dti_estado  }.ToString());
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
            return ShowObject(new Dtipocom());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object periodo = null;
            object ctipocom = null;
            object empresa = null;
            object almacen =null;
            object puntoventa = null;
            object numero = null;
            
            tmp.TryGetValue("dti_periodo", out periodo);
            tmp.TryGetValue("dti_ctipocom", out ctipocom);
            tmp.TryGetValue("dti_empresa", out empresa);
            tmp.TryGetValue("dti_almacen", out almacen);
            tmp.TryGetValue("dti_puntoventa", out puntoventa);
            tmp.TryGetValue("dti_numero", out numero);
            
            Dtipocom obj = new Dtipocom();
            obj.dti_periodo = Convert.ToInt32(periodo);
            obj.dti_periodo_key = Convert.ToInt32(periodo);
            obj.dti_empresa = Convert.ToInt32(empresa);
            obj.dti_empresa_key = Convert.ToInt32(empresa);
            obj.dti_ctipocom = Convert.ToInt32(ctipocom);
            obj.dti_ctipocom_key = Convert.ToInt32(ctipocom);
            obj.dti_almacen = Convert.ToInt32(almacen);
            obj.dti_almacen_key = Convert.ToInt32(almacen);
            obj.dti_puntoventa = Convert.ToInt32(puntoventa);
            obj.dti_puntoventa_key = Convert.ToInt32(puntoventa);
            obj = DtipocomBLL.GetByPK(obj);
            
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Dtipocom GetObjeto(object objeto)
        //{
        //    Dtipocom obj = new Dtipocom();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object periodo = null;
        //        object ctipocom = null;
        //        object empresa = null;
        //        object almacen = null;
        //        object puntoventa = null;               
        //        object periodokey = null;
        //        object ctipocomkey = null;
        //        object empresakey = null;
        //        object almacenkey = null;
        //        object puntoventakey = null;
        //        object numero = null;
        //        object activo = null;
        //        object idalmacen = null;
        //        object idpuntoventa = null;
               
        //        tmp.TryGetValue("dti_idalmacen", out idalmacen);
        //        tmp.TryGetValue("dti_idpuntoventa", out idpuntoventa);
        //        tmp.TryGetValue("dti_periodo", out periodo);
        //        tmp.TryGetValue("dti_ctipocom", out ctipocom);
        //        tmp.TryGetValue("dti_empresa", out empresa);
        //        tmp.TryGetValue("dti_almacen", out almacen);
        //        tmp.TryGetValue("dti_puntoventa", out puntoventa);
        //        tmp.TryGetValue("dti_empresa_key", out empresakey);
        //        tmp.TryGetValue("dti_periodo_key", out periodokey);
        //        tmp.TryGetValue("dti_ctipocom_key", out ctipocomkey);
        //        tmp.TryGetValue("dti_almacen_key", out almacenkey);
        //        tmp.TryGetValue("dti_puntoventa_key", out puntoventakey);
        //        tmp.TryGetValue("dti_numero", out numero);
        //        tmp.TryGetValue("dti_estado", out activo);
                
        //        if (periodo != null && !periodo.Equals(""))
        //        {
        //            obj.dti_periodo = Convert.ToInt32(periodo);
        //        }
        //        if (periodokey != null && !periodokey.Equals(""))
        //        {
        //            obj.dti_periodo_key = Convert.ToInt32(periodokey);
        //        }
        //        if (empresa != null && !empresa.Equals(""))
        //        {
        //            obj.dti_empresa = Convert.ToInt32(empresa);
        //        }
        //        if (empresakey != null && !empresakey.Equals(""))
        //        {
        //            obj.dti_empresa_key = Convert.ToInt32(empresakey);
        //        }
        //        if (ctipocom != null && !ctipocom.Equals(""))
        //        {
        //            obj.dti_ctipocom = Convert.ToInt32(ctipocom);
        //        }
        //        if (ctipocomkey != null && !ctipocomkey.Equals(""))
        //        {
        //            obj.dti_ctipocom_key = Convert.ToInt32(ctipocomkey);
        //        }
        //        if (almacen != null && !almacen.Equals(""))
        //        {
        //            obj.dti_almacen = Convert.ToInt32(almacen);
        //        }
        //        if (almacenkey != null && !almacenkey.Equals(""))
        //        {
        //            obj.dti_almacen_key = Convert.ToInt32(almacenkey);
        //        }
        //        if (puntoventa != null && !puntoventa.Equals(""))
        //        {
        //            obj.dti_puntoventa = Convert.ToInt32(puntoventa);
        //        }
        //        if (puntoventakey != null && !puntoventakey.Equals(""))
        //        {
        //            obj.dti_puntoventa_key = Convert.ToInt32(puntoventakey);
        //        }
        //        obj.dti_numero = Convert.ToInt32(numero);
        //        obj.dti_estado = (int?)activo;
                
        //        obj.crea_usr = "admin";
        //        obj.crea_fecha = DateTime.Now;
        //        obj.mod_usr = "admin";
        //        obj.mod_fecha = DateTime.Now;
        //        obj.dti_idalmacen = (string)idalmacen;
        //        obj.dti_idpuntoventa = (string)idpuntoventa;
        //    }
        //    return obj;
        //}




        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Dtipocom());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Dtipocom obj = new Dtipocom(objeto);
            obj.dti_almacen_key = obj.dti_almacen;
            obj.dti_ctipocom_key = obj.dti_ctipocom;
            obj.dti_empresa_key = obj.dti_empresa;
            obj.dti_periodo_key = obj.dti_periodo;
            obj.dti_puntoventa_key = obj.dti_puntoventa;



            if (DtipocomBLL.Update(obj) > 0)
            {
                return ShowData(obj);
            }
            else
            {
                if (DtipocomBLL.Insert(obj) > 0)
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
                Dtipocom obj = new Dtipocom(item);
                obj.dti_almacen_key = obj.dti_almacen;
                obj.dti_ctipocom_key = obj.dti_ctipocom;
                obj.dti_empresa_key = obj.dti_empresa;
                obj.dti_periodo_key = obj.dti_periodo;
                obj.dti_puntoventa_key = obj.dti_puntoventa;
                DtipocomBLL.Delete(transaction, obj);
            }
            transaction.Commit();
            return "OK";

        }
    }
}