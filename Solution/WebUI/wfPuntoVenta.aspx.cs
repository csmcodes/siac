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
    public partial class wfPuntoVenta : System.Web.UI.Page
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
           
            html.AppendLine(new Select { id = "cmbALMACEN", etiqueta = "Almacen", clase = Css.large, diccionario = Dictionaries.GetIDAlmacen() }.ToString());

            return html.ToString();
        }
        [WebMethod]
        public static string GetDetalle(object id)
        {
            StringBuilder html = new StringBuilder();
            List<Puntoventa> lista = new List<Puntoventa>();
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object almacen = null;
            tmp.TryGetValue("pve_almacen", out almacen);
            WhereParams parametros = new WhereParams("pve_almacen = {0}", Convert.ToInt32(almacen));
        //    parametros.Add(new WhereParams("pve_almacen", "=", int.Parse(almacen.ToString())));
           
        //    new WhereParams("pve_almacen = {0}",  int.Parse(almacen.ToString()))



            lista = PuntoventaBLL.GetAll(parametros, "");
            
            foreach (Puntoventa item in lista)
            {
                ArrayList array = new ArrayList();
                array.Add("");

                array.Add(item.pve_idalmacen);
                array.Add(item.pve_id);
                array.Add(item.pve_nombre);
                string strid = "{\"pve_almacen\":\"" + item.pve_almacen + "\", \"pve_empresa\":\"" + item.pve_empresa + "\", \"pve_secuencia\":\"" + item.pve_secuencia + "\"}";
                 
                html.AppendLine(HtmlElements.TablaRow(array, strid));
            }

            return html.ToString();
        }

        public static string ShowData(Puntoventa obj)
        {
            ArrayList array = new ArrayList();
            array.Add("");
            array.Add(obj.pve_idalmacen);
            array.Add(obj.pve_id);            
            array.Add(obj.pve_nombre);           
            string strid = "{\"pve_almacen\":\"" + obj.pve_almacen + "\", \"pve_empresa\":\"" + obj.pve_empresa + "\", \"pve_secuencia\":\"" + obj.pve_secuencia + "\"}";
            return HtmlElements.TablaRow(array, strid);
        }
        [WebMethod]


        public static string ShowObject(Puntoventa obj)
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "cmbSECUENCIA", etiqueta = "Secuencia", placeholder = "Secuencia", valor = obj.pve_secuencia.ToString(), clase = Css.large , habilitado=false}.ToString());
            html.AppendLine(new Input { id = "txtSECUENCIA_key", valor = obj.pve_secuencia_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.pve_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.pve_nombre, clase = Css.large, obligatorio = true }.ToString());          
            html.AppendLine(new Input { id = "txtRESPONSABLE", etiqueta = "Responsable", placeholder = "Responsable", valor = obj.pve_responsable, clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbCENTRO", etiqueta = "Centro", valor = obj.pve_centro.ToString(), clase = Css.large, diccionario = Dictionaries.GetCentro() }.ToString());
            html.AppendLine(new Check { id = "chkAUTOIMPRESOR", etiqueta = "Autoimpresor ", valor = obj.pve_autoimpresor}.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.pve_estado }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        [WebMethod]
        public static string AddObject()
        {
            return ShowObject(new Puntoventa());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object almacen = null;
            object empresa = null;
            object secuencia = null;
            tmp.TryGetValue("pve_almacen", out almacen);
            tmp.TryGetValue("pve_empresa", out empresa);
            tmp.TryGetValue("pve_secuencia", out secuencia);
            Puntoventa obj = new Puntoventa();

            if (almacen != null && !almacen.Equals(""))
            {
                obj.pve_almacen = Convert.ToInt32(almacen.ToString());
                obj.pve_almacen_key = Convert.ToInt32(almacen.ToString());
            }
            if (empresa != null && !empresa.Equals(""))
            {
                obj.pve_empresa = Convert.ToInt32(empresa.ToString());
                obj.pve_empresa_key = Convert.ToInt32(empresa.ToString());
            }
            if (secuencia != null && !secuencia.Equals(""))
            {
                obj.pve_secuencia = Convert.ToInt32(secuencia.ToString());
                obj.pve_secuencia_key = Convert.ToInt32(secuencia.ToString());
            }

            obj = PuntoventaBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Puntoventa GetObjeto(object objeto)
        //{
        //    Puntoventa obj = new Puntoventa();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object almacen = null;
        //        object almacenkey = null;
        //        object empresa = null;
        //        object empresakey = null;
        //        object secuencia = null;
        //        object secuenciakey = null;
        //        object id = null;
        //        object nombre = null;
        //        object responsable = null;                
        //        object centro = null;
        //        object autoimpresor = null;
        //        object activo = null;
        //        object idalmacen = null;
        //        tmp.TryGetValue("pve_almacen", out almacen);
        //        tmp.TryGetValue("pve_almacen_key", out almacenkey);
        //        tmp.TryGetValue("pve_empresa", out empresa);
        //        tmp.TryGetValue("pve_empresa_key", out empresakey);
        //        tmp.TryGetValue("pve_secuencia", out secuencia);
        //        tmp.TryGetValue("pve_secuencia_key", out secuenciakey);
        //        tmp.TryGetValue("pve_id", out id);
        //        tmp.TryGetValue("pve_nombre", out nombre);
        //        tmp.TryGetValue("pve_responsable", out responsable);
        //        tmp.TryGetValue("pve_autoimpresor", out autoimpresor);
        //        tmp.TryGetValue("pve_centro", out centro);
        //        tmp.TryGetValue("pve_estado", out activo);
        //        tmp.TryGetValue("pve_idalmacen", out idalmacen);



        //    if (almacen != null && !almacen.Equals(""))
        //    {
        //        obj.pve_almacen = Convert.ToInt32(almacen.ToString());

        //    }
        //    if (almacenkey != null && !almacenkey.Equals(""))
        //    {
        //        obj.pve_almacen_key = Convert.ToInt32(almacenkey.ToString());
        //    }
        //    if (empresa != null && !empresa.Equals(""))
        //    {
        //        obj.pve_empresa = Convert.ToInt32(empresa.ToString());

        //    }
        //    if (empresakey != null && !empresakey.Equals(""))
        //    {
        //        obj.pve_empresa_key = Convert.ToInt32(empresakey.ToString());
        //    }
        //    if (secuencia != null && !secuencia.Equals(""))
        //    {
        //        obj.pve_secuencia = Convert.ToInt32(secuencia.ToString());


        //    }
        //    if (id != null && !id.Equals(""))
        //    {
        //        obj.pve_secuencia = Convert.ToInt32(id.ToString());

        //    }
            
        //    if (secuenciakey != null && !secuenciakey.Equals(""))
        //    {
        //        obj.pve_secuencia_key = Convert.ToInt32(secuenciakey.ToString());


        //    }
        //    obj.pve_id = (string)id;
        //    obj.pve_nombre = (string)nombre;
        //    obj.pve_responsable = (string)(responsable);
        //    obj.pve_autoimpresor = Convert.ToInt32(autoimpresor);
        //    obj.pve_centro = Convert.ToInt32(centro);
        //    // obj.bxv_imagen = Convert.ToByte(imagen);
        //    obj.pve_estado = (int?)activo;
        //    obj.crea_usr = "admin";
        //    obj.crea_fecha = DateTime.Now;
        //    obj.mod_usr = "admin";
        //    obj.mod_fecha = DateTime.Now;
        //    obj.pve_idalmacen = (string)idalmacen;
        //    }
        //    return obj;
        //}




        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Puntoventa());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
           
            Puntoventa obj = new Puntoventa(objeto);
            obj.pve_almacen_key = obj.pve_almacen;
            obj.pve_empresa_key = obj.pve_empresa;
            obj.pve_secuencia_key = obj.pve_secuencia;
            if (obj.pve_id.Length != 3)
                return "ERROR";
            if (obj.pve_secuencia_key > 0)
            {
                if (PuntoventaBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (PuntoventaBLL.Insert(obj) > 0)
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
                Puntoventa obj = new Puntoventa(item);
                obj.pve_almacen_key = obj.pve_almacen;
                obj.pve_empresa_key = obj.pve_empresa;
                obj.pve_secuencia_key = obj.pve_secuencia;
                PuntoventaBLL.Delete(transaction, obj);

            }
            transaction.Commit();
            return "OK";

        }
    }
}