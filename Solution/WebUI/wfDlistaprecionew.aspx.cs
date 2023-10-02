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
    public partial class wfDlistaprecionew : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "cpr_nombre";
        protected static string WhereClause = "";
        protected static WhereParams parametros;

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
            tdatos.rows[0].cells[1].valor = new Input { id = "cmbPRODUCTO_S", clase = Css.medium, placeholder = "Producto" }.ToString();
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
        public static string GetDetalle()
        {
            pageIndex = 1;
            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tddatos";
            tdatos.invoice = true;
            tdatos.clase = "scrolltable";
            tdatos.AddColumn("Nombre Producto", "", "", "");
            tdatos.AddColumn("Lista Precio", "", "", "");
            tdatos.AddColumn("Ruta", "", "", "");
            tdatos.AddColumn("ID Almacen", "", "", "");
            tdatos.AddColumn("Fecha Inicio", "", "", "");
            tdatos.AddColumn("Fecha Fin", "", "", "");
            tdatos.AddColumn("Unidad Medida", "", "", "");
            tdatos.AddColumn("Precio", "", "", "");
            tdatos.AddColumn("Activo", "", "", "");          
            tdatos.editable = false;           
            html.AppendLine(tdatos.ToString());
            return html.ToString();
        }

        [WebMethod]
        public static string GetDetalleData(object objeto)
        {           
            SetWhereClause(new Dlistaprecio(objeto));       
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            List<Dlistaprecio> lst = DlistaprecioBLL.GetAllByPage(parametros, "dlpr_producto", desde, hasta);            
            StringBuilder html = new StringBuilder();
            foreach (Dlistaprecio item in lst)
            {
                ArrayList array = new ArrayList();
                array.Add(item.dlpr_nombreproducto);
                array.Add(item.dlpr_nombrelistaprecio);
                array.Add(item.dlpr_nombreruta);
                array.Add(item.dlpr_idalmacen);
                array.Add(item.dlpr_fecha_ini);
                array.Add(item.dlpr_fecha_fin);
                array.Add(item.dlpr_nombreumedida);
                array.Add(item.dlpr_precio);
                array.Add(Conversiones.LogicToString(item.dlpr_estado));     
                
                string strid = "{\"dlpr_listaprecio\":\"" + item.dlpr_listaprecio + "\", \"dlpr_empresa\":\"" + item.dlpr_empresa + "\", \"dlpr_codigo\":\"" + item.dlpr_codigo + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));   
            }           
            return html.ToString();
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
                valores.Add("%" + obj.dlpr_nombreproducto + "%");
                contador++;
            }
            parametros.valores = valores.ToArray();
        }

        [WebMethod]
        public static string GetDlistaPrecio(object objeto)
        {
            Dlistaprecio dlistaprecio = new Dlistaprecio(objeto);
            dlistaprecio.dlpr_empresa_key = dlistaprecio.dlpr_empresa;
            dlistaprecio.dlpr_codigo_key = dlistaprecio.dlpr_codigo;
            dlistaprecio.dlpr_listaprecio_key = dlistaprecio.dlpr_listaprecio;
            dlistaprecio = DlistaprecioBLL.GetByPK(dlistaprecio);
            StringBuilder html = new StringBuilder();     
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span11\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(9, 2);
            tdatos.rows[0].cells[0].valor = "Almacen:";
            tdatos.rows[0].cells[1].valor = new Select { id = "cmbALMACEN_P", diccionario = Dictionaries.GetAlmacen(), withempty = true, valor = dlistaprecio.dlpr_almacen, habilitado = (dlistaprecio.dlpr_empresa > 0) ? false : true }.ToString();
            tdatos.rows[1].cells[0].valor = "Ruta:";
            tdatos.rows[1].cells[1].valor = new Select { id = "cmbRUTA_P", diccionario = Dictionaries.GetRuta(), withempty = true, valor = dlistaprecio.dlpr_ruta, habilitado = (dlistaprecio.dlpr_empresa > 0) ? false : true }.ToString();
            tdatos.rows[2].cells[0].valor = "Lista Precios:";
            tdatos.rows[2].cells[1].valor = new Select { id = "cmbLISTA_P", diccionario = Dictionaries.GetListaprecio(),withempty = true, valor = dlistaprecio.dlpr_listaprecio, habilitado = (dlistaprecio.dlpr_empresa > 0) ? false : true }.ToString();
            tdatos.rows[3].cells[0].valor = "Producto:";
            tdatos.rows[3].cells[1].valor = new Input() { id = "txtIDPRO", placeholder = "ID", autocomplete = "GetProductoObj", valor = dlistaprecio.dlpr_nombreproducto, clase = Css.blocklevel, habilitado = (dlistaprecio.dlpr_empresa > 0) ? false : true }.ToString() + new Input() { id = "cmbPRODUCTO", valor = dlistaprecio.dlpr_producto, visible = false }.ToString();
            tdatos.rows[4].cells[0].valor = "Unidad medida:";
            tdatos.rows[4].cells[1].valor =new Select() { id = "cmbUMEDIDA", placeholder = "Medida", diccionario = Dictionaries.GetUmedida(), clase = Css.blocklevel }.ToString();
            tdatos.rows[5].cells[0].valor = "Fecha Inicio:";
            tdatos.rows[5].cells[1].valor = new Input { id = "cmbFECHA_INI", datepicker = true, datetimevalor = (dlistaprecio.dlpr_fecha_ini.HasValue) ? dlistaprecio.dlpr_fecha_ini.Value : DateTime.Now, clase = Css.large ,obligatorio=true}.ToString();
            tdatos.rows[6].cells[0].valor = "Fecha fin:";
            tdatos.rows[6].cells[1].valor = new Input { id = "cmbFECHA_FIN", datepicker = true, datetimevalor = (dlistaprecio.dlpr_fecha_fin.HasValue) ? dlistaprecio.dlpr_fecha_fin.Value : DateTime.Now, clase = Css.large, obligatorio = true }.ToString();
            tdatos.rows[7].cells[0].valor = "Precio ($):";
            tdatos.rows[7].cells[1].valor = new Input { id = "txtPRECIO", valor = Formatos.CurrencyFormatAll(dlistaprecio.dlpr_precio), clase = Css.medium, obligatorio = true }.ToString();
            tdatos.rows[8].cells[0].valor = "Activo";
            tdatos.rows[8].cells[1].valor = new Check { id = "chkESTADO",valor = dlistaprecio.dlpr_estado }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_P", visible = false, valor = dlistaprecio.dlpr_codigo }.ToString());
            html.AppendLine(" </div><!--span11-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Dlistaprecio  obj = new Dlistaprecio(objeto);
            if (obj.dlpr_codigo > 0)
            {
                return UpdateComprobante(obj);
            }
            else
            {
                return InsertComprobante(obj);
            }
        }


        public static Dlistaprecio CopyObj(Dlistaprecio obj)
        {
            Dlistaprecio c = new Dlistaprecio();
            c.dlpr_almacen = obj.dlpr_almacen;
            c.dlpr_codigo = obj.dlpr_codigo;
            c.dlpr_codigo_key = obj.dlpr_codigo_key;
            c.dlpr_empresa = obj.dlpr_empresa;
            c.dlpr_empresa_key = obj.dlpr_empresa_key;
            c.dlpr_estado = obj.dlpr_estado;
            c.dlpr_fecha_fin = obj.dlpr_fecha_fin;
            c.dlpr_fecha_ini = obj.dlpr_fecha_ini;
            c.dlpr_idalmacen = obj.dlpr_idalmacen;
            c.dlpr_listaprecio = obj.dlpr_listaprecio;
            c.dlpr_listaprecio_key = obj.dlpr_listaprecio_key;
            c.dlpr_nombreproducto = obj.dlpr_nombreproducto;
            c.dlpr_nombreumedida = obj.dlpr_nombreumedida;
            c.dlpr_precio = obj.dlpr_precio;
            c.dlpr_producto = obj.dlpr_producto;
            c.dlpr_ruta = obj.dlpr_ruta;
            c.dlpr_umedida = obj.dlpr_umedida;
            return c;

        }

        public static string InsertComprobante(Dlistaprecio obj)
        {

            List<Dlistaprecio> lista = new List<Dlistaprecio>();
            lista.Add(obj);

            if (obj.dlpr_almacen.Value < 1)
            {
                List<Dlistaprecio> det = new List<Dlistaprecio>();
                lista.Remove(obj);
                Dictionary<string, string> dicalmacenes = Dictionaries.GetAlmacen();
                foreach (KeyValuePair<string, string> dic in dicalmacenes)
                {
                    Dlistaprecio c = CopyObj(obj);
                    c.dlpr_almacen = int.Parse(dic.Key);
                    det.Add(c);
                }
                lista.Clear();
                lista.AddRange(det);
            }

            if (obj.dlpr_ruta.Value < 1)
            {
                List<Dlistaprecio> det = new List<Dlistaprecio>();
                Dictionary<string, string> dicruta = Dictionaries.GetRuta();
                foreach (Dlistaprecio item in lista)
                {
                    foreach (KeyValuePair<string, string> dic in dicruta)
                    {
                        Dlistaprecio c = CopyObj(item);
                        c.dlpr_ruta = int.Parse(dic.Key);
                        det.Add(c);
                    }

                }
                lista.Clear();
                lista.AddRange(det);
            }
            if (obj.dlpr_listaprecio < 1)
            {
                List<Dlistaprecio> det = new List<Dlistaprecio>();
                Dictionary<string, string> diclista = Dictionaries.GetListaprecio();
                foreach (Dlistaprecio item in lista)
                {
                    foreach (KeyValuePair<string, string> dic in diclista)
                    {
                        Dlistaprecio c = CopyObj(item);
                        c.dlpr_listaprecio = int.Parse(dic.Key);
                        det.Add(c);
                    }

                }
                lista.Clear();
                lista.AddRange(det);
            }





            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                foreach (Dlistaprecio item in lista)
                {
                    DlistaprecioBLL.Insert(transaction, item);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return "-1";
            }
            return "OK";
        }


        public static string UpdateComprobante(Dlistaprecio obj)
        {

            obj.dlpr_empresa_key = obj.dlpr_empresa;
            obj.dlpr_listaprecio_key = obj.dlpr_listaprecio;
            obj.dlpr_codigo_key = obj.dlpr_codigo;

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                DlistaprecioBLL.Update(transaction, obj);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return "-1";
            }
            return "OK";
        }

        [WebMethod]
        public static string RemoveObjects(object objeto)
        {
           

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                Array array = (Array)objeto;
                foreach (Object item in array)
                {
                    if (item != null)
                    {
                        Dlistaprecio obj = new Dlistaprecio(item);
                        DlistaprecioBLL.Delete(transaction, obj);
                     }                    
                }
                
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return "-1";
            }
            return "OK";
            
        }
    }
}