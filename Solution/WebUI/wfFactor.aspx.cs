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
    public partial class wfFactor : System.Web.UI.Page
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
            html.AppendLine(new Select { id = "cmbPRODUCTO_S", etiqueta = "Producto", clase = Css.large, diccionario = Dictionaries.GetProducto() }.ToString());
          
            return html.ToString();
        }
        [WebMethod]
        public static string GetDetalle(object id)
        {
            StringBuilder html = new StringBuilder();
            List<Factor> lista = new List<Factor>();
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object producto = null;
            object empresa = null;
            tmp.TryGetValue("fac_producto", out producto);
            tmp.TryGetValue("fac_empresa", out empresa);
            lista = FactorBLL.GetAll(new WhereParams("fac_producto = {0} and fac_empresa = {1} ", Convert.ToInt32(producto), Convert.ToInt32(empresa)), "");
            //lista = FactorBLL.GetAll("bxv_vehiculo =" + vehiculo + " AND bxv_fecha>= ' " + fecha1 + "'" + " AND bxv_fecha<= ' " + fecha2 + "'", "");
            //lista = FactorBLL.GetAll("bxv_vehiculo =" + id , "");
            foreach (Factor item in lista)
            {
                ArrayList array = new ArrayList();
                array.Add("");
                array.Add(item.fac_nombreunidad);
                array.Add(item.fac_factor);               
                array.Add(Conversiones.LogicToString(item.fac_default));
                string strid = "{\"fac_producto\":\"" + item.fac_producto + "\", \"fac_empresa\":\"" + item.fac_empresa + "\", \"fac_unidad\":\"" + item.fac_unidad + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));
            }

            return html.ToString();
        }

        public static string ShowData(Factor obj)
        {
            ArrayList array = new ArrayList();
            array.Add("");
            array.Add(obj.fac_nombreunidad);
            array.Add(obj.fac_factor);
            array.Add(Conversiones.LogicToString(obj.fac_default));
            string strid = "{\"fac_producto\":\"" + obj.fac_producto + "\", \"fac_empresa\":\"" + obj.fac_empresa + "\", \"fac_unidad\":\"" + obj.fac_unidad + "\"}";//ID COMPUESTO
            return HtmlElements.TablaRow(array, strid);
        }
        [WebMethod]


        public static string ShowObject(Factor obj)
        {

            StringBuilder html = new StringBuilder();
           
            html.AppendLine(new Select { id = "cmbUMEDIDA", etiqueta = "unidad de medida", valor = obj.fac_unidad.ToString(), clase = Css.large, diccionario = Dictionaries.GetUmedida() }.ToString());
            html.AppendLine(new Input { id = "txtUMEDIDA_key", valor = obj.fac_unidad_key.ToString(), visible = false }.ToString());
           html.AppendLine(new Select { id = "cmbPRODUCTO", etiqueta = "Producto", valor = obj.fac_producto.ToString(), clase = Css.large, diccionario = Dictionaries.GetProducto() }.ToString());
            html.AppendLine(new Input { id = "txtFACTOR", etiqueta = "Factor", placeholder = "Factor", valor = obj.fac_factor.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Check { id = "chkDEFAULT", etiqueta = "default ", valor = obj.fac_default }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "activo ", valor = obj.fac_estado }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        [WebMethod]
        public static string AddObject()
        {
            return ShowObject(new Factor());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object unidad = null;
            object producto = null;
            object empresa = null;
            tmp.TryGetValue("fac_producto", out producto);
            tmp.TryGetValue("fac_unidad", out unidad);
            tmp.TryGetValue("fac_empresa", out empresa);
            Factor obj = new Factor();

            obj.fac_producto = Convert.ToInt32(producto);
            obj.fac_producto_key = Convert.ToInt32(producto);
            obj.fac_empresa = Convert.ToInt32(empresa);
            obj.fac_empresa_key = Convert.ToInt32(empresa);
            obj.fac_unidad = Convert.ToInt32(unidad);
            obj.fac_unidad_key = Convert.ToInt32(unidad);
            obj = FactorBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Factor GetObjeto(object objeto)
        //{
        //    Factor obj = new Factor();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object unidad = null;
        //        object unidadkey = null;
        //        object empresa = null;
        //        object empresakey = null;
        //        object producto = null;
        //        object productokey = null;
        //        object factor = null;
        //        object predeterminado = null;
        //        object activo = null;
                  
        //              object nombreunidad = null;
        //        tmp.TryGetValue("fac_unidad", out unidad);
        //        tmp.TryGetValue("fac_unidad_key", out unidadkey);
        //        tmp.TryGetValue("fac_empresa", out empresa);
        //        tmp.TryGetValue("fac_empresa_key", out empresakey);
        //        tmp.TryGetValue("fac_producto", out producto);
        //        tmp.TryGetValue("fac_producto_key", out productokey);
        //        tmp.TryGetValue("fac_factor", out factor);
        //        tmp.TryGetValue("fac_default", out predeterminado);
        //        tmp.TryGetValue("fac_estado", out activo);
        //        tmp.TryGetValue("fac_nombreunidad", out nombreunidad);
        //        if (unidad != null && !unidad.Equals(""))
        //        {
        //            obj.fac_unidad = Convert.ToInt32(unidad);
        //        }
        //        if (unidadkey != null && !unidadkey.Equals(""))
        //        {
        //            obj.fac_unidad_key = Convert.ToInt32(unidadkey);
        //        }
                
        //        obj.fac_empresa = Convert.ToInt32(empresa);
        //        obj.fac_empresa_key = Convert.ToInt32(empresakey);
        //        obj.fac_producto = Convert.ToInt32(producto);
        //        obj.fac_producto_key = Convert.ToInt32(productokey);
        //        obj.fac_factor =Convert.ToInt32(factor);  
                
        //        obj.fac_default = (int?)predeterminado;
        //        if (obj.fac_default > 0)
        //        {
        //            WhereParams parametros = new WhereParams("fac_default = {0} and fac_producto = {1} and fac_unidad!= {2}", obj.fac_default, obj.fac_producto, obj.fac_unidad);
        //            if (FactorBLL.GetRecordCount(parametros, "") > 0)
        //            {
        //                obj.fac_default = 0;
        //            }
        //        }


        //        obj.fac_nombreunidad = (string)nombreunidad;

        //        obj.fac_estado = (int?)activo;
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
            return ShowObject(new Factor());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Factor obj = new Factor(objeto);
            obj.fac_empresa_key = obj.fac_empresa;
            obj.fac_producto_key = obj.fac_producto;
            obj.fac_unidad_key = obj.fac_unidad;

            if (obj.fac_unidad_key > 0)
            {
                if (FactorBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (FactorBLL.Insert(obj) > 0)
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
                Factor obj = new Factor(item);
                obj.fac_empresa_key = obj.fac_empresa;
                obj.fac_producto_key = obj.fac_producto;
                obj.fac_unidad_key = obj.fac_unidad;
                FactorBLL.Delete(transaction, obj);

            }
            transaction.Commit();
            return "OK";

        }
    }
}