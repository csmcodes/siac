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
    public partial class wfDlistaprecio : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static WhereParams parametros = new WhereParams();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
            }
        }
        [WebMethod]
        public static string GetCabecera()
        {
            StringBuilder html = new StringBuilder();

          html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">"); 
            HtmlTable tdatos = new HtmlTable();            
            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Producto:";
            tdatos.rows[0].cells[1].valor = new Input { id = "cmbPRODUCTO_S", autocomplete = "GetPersonaObj", obligatorio = true, clase = Css.medium, placeholder = "Destinatario" }.ToString() + " "+ new Input { id = "txtCODDES", visible = false,  }.ToString();           
            tdatos.rows[1].cells[0].valor = "Lista Precio:";
            tdatos.rows[1].cells[1].valor = new Select { id = "cmbLISTAPRECIO_S", clase = Css.large, diccionario = Dictionaries.GetListaprecio(), withempty = true }.ToString();
         
            tdatos.rows[2].cells[0].valor = "Ruta";
            tdatos.rows[2].cells[1].valor = new Select { id = "cmbRUTA_S", clase = Css.large, diccionario = Dictionaries.GetRuta(), withempty = true }.ToString();
         
             html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");

            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdtra = new HtmlTable();
            tdtra.CreteEmptyTable(2, 2);
            tdtra.rows[0].cells[0].valor = "Almacen:";
            tdtra.rows[0].cells[1].valor = new Select { id = "cmbALMACEN_S", clase = Css.large, diccionario = Dictionaries.GetAlmacen(), withempty = true }.ToString();
            tdtra.rows[1].cells[0].valor = "Unidad:";
            tdtra.rows[1].cells[1].valor = new Select { id = "cmbUNIDAD_S", clase = Css.large, diccionario = Dictionaries.GetUmedida(), withempty = true }.ToString();
          
            html.AppendLine(tdtra.ToString());
            html.AppendLine(" </div><!--span6-->");





     html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }

        [WebMethod]
        public static string ReloadData(object id)
        {
            pageIndex = 1;
            return GetDetalle(id);
        }

        public static void SetWhereClause(Dlistaprecio obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (obj.dlpr_listaprecio > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dlpr_listaprecio = {" + contador + "} ";
                valores.Add(obj.dlpr_listaprecio);
                contador++;
            }
            if (obj.dlpr_ruta > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dlpr_ruta = {" + contador + "} ";
                valores.Add(obj.dlpr_ruta);
                contador++;
            }
            if (obj.dlpr_almacen > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dlpr_almacen = {" + contador + "} ";
                valores.Add(obj.dlpr_almacen);
                contador++;
            }
            if (obj.dlpr_umedida > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dlpr_umedida = {" + contador + "} ";
                valores.Add(obj.dlpr_umedida);
                contador++;
            }
            if (obj.dlpr_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dlpr_empresa = {" + contador + "} ";
                valores.Add(obj.dlpr_empresa);
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.dlpr_nombreproducto))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (pro_nombre like  {" + contador + "} or pro_id like  {" + contador + "}  )";
                valores.Add( "%"+obj.dlpr_nombreproducto + "%");
                contador++;
            }         
            parametros.valores = valores.ToArray();
        }





        [WebMethod]
        public static string GetDetalle(object id)
        {
            SetWhereClause(new Dlistaprecio(id));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
        
            List<Dlistaprecio> lst = DlistaprecioBLL.GetAllByPage(parametros, "dlpr_producto", desde, hasta);
            StringBuilder html = new StringBuilder();


           
            
            foreach (Dlistaprecio item in lst)
            {
                   ArrayList array = new ArrayList();
                   array.Add("");                  
                   array.Add(item.dlpr_nombreproducto);
                   array.Add(item.dlpr_listaprecio);
                   array.Add(item.dlpr_ruta);
                   array.Add(item.dlpr_idalmacen);
                   array.Add(item.dlpr_fecha_ini);
                   array.Add(item.dlpr_fecha_fin);
                   array.Add(item.dlpr_nombreumedida);
                   array.Add(new Input() { id = "txtPRECIO", clase = Css.mini + Css.amount, valor = item.dlpr_precio }.ToString());
                   array.Add(new Check() { id = "chkESTADO", clase = Css.blocklevel + Css.cantidades, valor = item.dlpr_estado }.ToString());
                   array.Add(  new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());
                   string strid = "{\"dlpr_listaprecio\":\"" + item.dlpr_listaprecio + "\", \"dlpr_empresa\":\"" + item.dlpr_empresa + "\", \"dlpr_codigo\":\"" + item.dlpr_codigo + "\"}";//ID COMPUESTO

                   html.AppendLine(HtmlElements.TablaRow(array, strid));             
            }


            ArrayList arraynew = new ArrayList();
            arraynew.Add("");
            arraynew.Add(new Input() { id = "txtIDPRO", placeholder = "ID", autocomplete = "GetProductoObj", clase = Css.blocklevel }.ToString() + new Input() { id = "cmbPRODUCTO", visible = false }.ToString());
            arraynew.Add(new Select { id = "cmbLISTAPRECIO_S", clase = Css.large, diccionario = Dictionaries.GetListaprecio() }.ToString());
            arraynew.Add(new Select { id = "cmbRUTA", clase = Css.large, diccionario = Dictionaries.GetRuta() }.ToString());
            arraynew.Add(new Select { id = "cmbALMACEN", clase = Css.large, diccionario = Dictionaries.GetAlmacen() }.ToString());
            arraynew.Add(new Input { id = "cmbFECHA_INI", datepicker = true, datetimevalor = DateTime.Now, clase = Css.large }.ToString());
            arraynew.Add(new Input { id = "cmbFECHA_FIN",  datepicker = true, datetimevalor = DateTime.Now, clase = Css.large }.ToString());
            arraynew.Add(new Select() { id = "cmbUMEDIDA", placeholder = "Medida", diccionario = Dictionaries.GetUmedida(), clase = Css.blocklevel }.ToString());
            arraynew.Add(new Input() { id = "txtPRECIO", clase = Css.mini + Css.amount, valor = 0 }.ToString());
            arraynew.Add(new Check() { id = "chkESTADO", clase = Css.blocklevel + Css.cantidades, valor = 0 }.ToString());
            arraynew.Add(new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());
            string strid2 = "editrow";
            html.AppendLine(HtmlElements.TablaRow(arraynew,strid2)); 

            return html.ToString();
        }

        public static string ShowData(Dlistaprecio obj)
        {
            ArrayList array = new ArrayList();
            array.Add("");
            array.Add(obj.dlpr_idalmacen);
            array.Add(obj.dlpr_nombreproducto);
            array.Add(obj.dlpr_nombreumedida);
            array.Add(obj.dlpr_fecha_ini);
            array.Add(obj.dlpr_fecha_fin);
            array.Add(obj.dlpr_precio);
            string strid = "{\"dlpr_listaprecio\":\"" + obj.dlpr_listaprecio + "\", \"dlpr_empresa\":\"" + obj.dlpr_empresa + "\", \"dlpr_codigo\":\"" + obj.dlpr_codigo + "\"}";//ID COMPUESTO
            return HtmlElements.TablaRow(array, strid);
        }
        [WebMethod]


        public static string ShowObject(Dlistaprecio obj)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.dlpr_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.dlpr_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Select { id = "cmbALMACEN", etiqueta = "Almacen", valor = obj.dlpr_almacen.ToString(), clase = Css.large, diccionario = Dictionaries.GetIDAlmacen() }.ToString());
            html.AppendLine(new Select { id = "cmbPRODUCTO", etiqueta = "Productos", valor = obj.dlpr_producto.ToString(), clase = Css.large, diccionario = Dictionaries.GetProducto() }.ToString());
            html.AppendLine(new Select { id = "cmbUMEDIDA", etiqueta = "Unidad de medida", valor = obj.dlpr_umedida.ToString(), clase = Css.large, diccionario = Dictionaries.GetUmedida() }.ToString());           
            html.AppendLine(new Select { id = "cmbRUTA", etiqueta = "Ruta", valor = obj.dlpr_ruta.ToString(), clase = Css.large, diccionario = Dictionaries.GetRuta() }.ToString());
       
            html.AppendLine(new Input { id = "cmbFECHA_INI", etiqueta = "Fecha inicio", datepicker = true, datetimevalor = DateTime.Now, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "cmbFECHA_FIN", etiqueta = "Fecha fin", datepicker = true, datetimevalor = DateTime.Now, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtPRECIO", etiqueta = "Precio", placeholder = "Precio", valor = obj.dlpr_precio.ToString(), clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.dlpr_estado }.ToString());
           
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        [WebMethod]
        public static string AddObject()
        {
            return ShowObject(new Dlistaprecio());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object listaprecio = null;
            object empresa = null;
            tmp.TryGetValue("dlpr_listaprecio", out listaprecio);
            tmp.TryGetValue("dlpr_codigo", out codigo);
            tmp.TryGetValue("dlpr_empresa", out empresa);
            Dlistaprecio obj = new Dlistaprecio();            
            obj.dlpr_listaprecio = Convert.ToInt32(listaprecio);
            obj.dlpr_listaprecio_key = Convert.ToInt32(listaprecio);
            obj.dlpr_empresa = Convert.ToInt32(empresa);
            obj.dlpr_empresa_key = Convert.ToInt32(empresa);
            obj.dlpr_codigo = Convert.ToInt32(codigo);
            obj.dlpr_codigo_key = Convert.ToInt32(codigo);
            obj = DlistaprecioBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        




        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Dlistaprecio());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Dlistaprecio obj = new Dlistaprecio(objeto);
            if (obj.dlpr_codigo_key > 0)
            {
                if (DlistaprecioBLL.Update(obj) > 0)
                {
                    return "OK";
                }
                else
                    return "-1";
            }
            else
            {
                if (DlistaprecioBLL.Insert(obj) > 0)
                {
                    return "OK";
                }
                else
                    return "-1";
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
                Dlistaprecio obj = new Dlistaprecio(item);
                DlistaprecioBLL.Delete(transaction, obj);
            }
            transaction.Commit();
            return "OK";

        }
    }
}