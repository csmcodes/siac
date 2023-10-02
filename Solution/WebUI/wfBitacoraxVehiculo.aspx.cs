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
    public partial class wfBitacoraxVehiculo : System.Web.UI.Page
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
            html.AppendLine(new Select { id = "cmbVEHICULO", etiqueta = "Vehiculo", clase = Css.medium, diccionario = Dictionaries.GetVehiculos() }.ToString());
            html.AppendLine(new Input { id = "cmbFECHA_C", etiqueta = "Fecha", datepicker = true, datetimevalor  = DateTime.Now , clase = Css.large }.ToString()); 
       return html.ToString();
        }
        [WebMethod]
        public static string GetDetalle(object id)
        {
           StringBuilder html = new StringBuilder();
            List<Bitacoraxvehiculo> lista = new List<Bitacoraxvehiculo>();
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object fecha = null;
            object vehiculo = null;
            object empresa= null;
            tmp.TryGetValue("bxv_vehiculo", out vehiculo);
            tmp.TryGetValue("bxv_fecha", out fecha);
            tmp.TryGetValue("bxv_empresa", out empresa);
            if (fecha == null || fecha.Equals(""))
            {
                fecha = DateTime.Now;
            }
           
            DateTime aux = Convert.ToDateTime(fecha);
            DateTime fecha1 = new DateTime(aux.Year, aux.Month, aux.Day, 0, 0, 0);
            DateTime fecha2 = new DateTime(aux.Year, aux.Month, aux.Day, 23, 59, 59);
            lista = BitacoraxvehiculoBLL.GetAll(new WhereParams("bxv_vehiculo = {0} and bxv_empresa = {1} and bxv_fecha between {2} and {3}", Convert.ToInt32(vehiculo), Convert.ToInt32(empresa), fecha1, fecha2), "");
            foreach (Bitacoraxvehiculo item in lista)
            {
                ArrayList array = new ArrayList();
                array.Add("");
                array.Add(item.bxv_fecha);
                array.Add(item.bxv_observacion);
                array.Add(item.bxv_imagen);
                string strid = "{\"bxv_vehiculo\":\"" + item.bxv_vehiculo + "\", \"bxv_fecha\":\"" + item.bxv_fecha + "\", \"bxv_empresa\":\"" + item.bxv_empresa + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));
            }

            return html.ToString();
        }

        public static string ShowData(Bitacoraxvehiculo obj)
        {   
            ArrayList array = new ArrayList();
            array.Add("");
            array.Add(obj.bxv_fecha);
            array.Add(obj.bxv_observacion);
            array.Add(obj.bxv_imagen);
            string strid = "{\"bxv_vehiculo\":\"" + obj.bxv_vehiculo + "\", \"bxv_fecha\":\"" + obj.bxv_fecha +  "\", \"bxv_empresa\":\"" + obj.bxv_empresa+"\"}";//ID COMPUESTO
            return HtmlElements.TablaRow(array, strid);
        }
        [WebMethod]


        public static string ShowObject(Bitacoraxvehiculo obj)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Textarea { id = "txtOBSERVACION", etiqueta = "Observacion", placeholder = "Observacion", valor = obj.bxv_observacion, cols= 5,rows= 5 ,clase= Css.large }.ToString());
            html.AppendLine(new Input { id = "txtFECHA_key", datetimevalor = obj.bxv_fecha_key, visible = false }.ToString());
            html.AppendLine(new Input { id = "cmbFECHA", etiqueta = "Fecha", datepicker = true, datetimevalor =obj.bxv_fecha, clase = Css.large }.ToString()); 
            
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        [WebMethod]
        public static string AddObject()
        {
            return ShowObject(new Bitacoraxvehiculo());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object fecha = null;
            object vehiculo = null;
            object empresa = null;
            tmp.TryGetValue("bxv_vehiculo", out vehiculo);
            tmp.TryGetValue("bxv_fecha", out fecha);
            tmp.TryGetValue("bxv_empresa", out empresa);
            Bitacoraxvehiculo obj = new Bitacoraxvehiculo();
            if (fecha != null && !fecha.Equals(""))
            {
                obj.bxv_fecha = Convert.ToDateTime(fecha);
                obj.bxv_fecha_key = Convert.ToDateTime(fecha);
            }
            obj.bxv_vehiculo = Convert.ToInt32(vehiculo);
            obj.bxv_vehiculo_key = Convert.ToInt32(vehiculo);
            obj.bxv_empresa = Convert.ToInt32(empresa);
            obj.bxv_empresa_key = Convert.ToInt32(empresa);
            obj = BitacoraxvehiculoBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Bitacoraxvehiculo GetObjeto(object objeto)
        //{
        //    Bitacoraxvehiculo obj = new Bitacoraxvehiculo();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object fecha = null;
        //        object fechakey = null;
        //        object vehiculo = null;
        //        object observacion = null;
        //        object empresa = null;
        //        object imagen = null;
        //        object activo = null;
        //        tmp.TryGetValue("bxv_fecha", out fecha);
        //        tmp.TryGetValue("bxv_fecha_key", out fechakey);
        //        tmp.TryGetValue("bxv_vehiculo", out vehiculo);
        //        tmp.TryGetValue("bxv_observacion", out observacion);
        //        tmp.TryGetValue("bxv_empresa", out empresa);
        //        tmp.TryGetValue("bxv_imagen", out imagen);
        //        tmp.TryGetValue("bxv_estado", out activo);               
        //        if (fecha != null && !fecha.Equals(""))
        //        {
        //            obj.bxv_fecha = Convert.ToDateTime(fecha.ToString());
        //        }
        //        if (fechakey != null && !fechakey.Equals(""))
        //        {
        //            obj.bxv_fecha_key = Convert.ToDateTime(fechakey.ToString());
        //        }
        //        obj.bxv_vehiculo = Convert.ToInt32(vehiculo);
        //        obj.bxv_vehiculo_key = Convert.ToInt32(vehiculo);
        //        obj.bxv_empresa = Convert.ToInt32(empresa);
        //        obj.bxv_empresa_key = Convert.ToInt32(empresa); 
        //        obj.bxv_observacion =  (string) observacion;         
        //        obj.bxv_estado = (int?)activo;
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
            return ShowObject(new Bitacoraxvehiculo());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Bitacoraxvehiculo obj = new Bitacoraxvehiculo(objeto);
            obj.bxv_empresa_key = obj.bxv_empresa;            
            obj.bxv_fecha_key = obj.bxv_fecha;
            obj.bxv_vehiculo_key = obj.bxv_vehiculo;

            if (obj.bxv_fecha_key != DateTime.MinValue)
            {
                if (BitacoraxvehiculoBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (BitacoraxvehiculoBLL.Insert(obj) > 0)
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
                Bitacoraxvehiculo obj = new Bitacoraxvehiculo(item);
                obj.bxv_empresa_key = obj.bxv_empresa;
                obj.bxv_fecha_key = obj.bxv_fecha;
                obj.bxv_vehiculo_key = obj.bxv_vehiculo;
                BitacoraxvehiculoBLL.Delete(transaction, obj);

            }
            transaction.Commit();
            return "OK";

        }
    }
}