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
    public partial class wfCalculoPrecio : System.Web.UI.Page
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
            tdatos.CreteEmptyTable(2, 2);
            tdatos.rows[0].cells[0].valor = "Almacen:";
            tdatos.rows[0].cells[1].valor = new Select { id = "cmbALMACEN", diccionario = Dictionaries.GetAlmacen(), withempty = true}.ToString();
            tdatos.rows[1].cells[0].valor = "Ruta:";
            tdatos.rows[1].cells[1].valor = new Select { id = "cmbRUTA", diccionario = Dictionaries.GetRuta(), withempty = true }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");

            html.AppendLine("<div class=\"span6\">");
            tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(2, 2);
            tdatos.rows[0].cells[0].valor = "Lista Precios:";
            tdatos.rows[0].cells[1].valor = new Select { id = "cmbLISTA", diccionario = Dictionaries.GetListaprecio(), withempty = true }.ToString();
            tdatos.rows[1].cells[0].valor = "Tipo Producto:";
            tdatos.rows[1].cells[1].valor = new Select { id = "cmbTPRODUCTO", diccionario = Dictionaries.GetTproducto(), withempty = true }.ToString();
            html.AppendLine(tdatos.ToString());
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

            tdatos.AddColumn("Almacen", "", "", "");
            tdatos.AddColumn("Ruta", "", "", "");
            tdatos.AddColumn("Lista Precio", "", "", "");
            tdatos.AddColumn("Tipo Producto", "", "", "");
            tdatos.AddColumn("Nombre", "", "", "");
            tdatos.AddColumn("Indice", "", "", "");
            tdatos.AddColumn("Valor", "", "", "");
            tdatos.AddColumn("Peso", "", "", "");            

            tdatos.editable = false;
           
            html.AppendLine(tdatos.ToString());
            return html.ToString();
        }

        [WebMethod]
        public static string GetDetalleData(object objeto)
        {

            Calculoprecio calculo = new Calculoprecio(objeto);

            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (calculo.cpr_listaprecio > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cpr_listaprecio = {" + contador + "} ";
                valores.Add(calculo.cpr_listaprecio);
                contador++;
            }
            if (calculo.cpr_ruta.Value > 0)
            {
                
                parametros.where += ((parametros.where != "") ? " and " : "") + " cpr_ruta = {" + contador + "} ";
                valores.Add(calculo.cpr_ruta);
                contador++;
            }
            if (calculo.cpr_almacen.Value > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cpr_almacen = {" + contador + "} ";
                valores.Add(calculo.cpr_almacen);
                contador++;
            }
            
            if (calculo.cpr_tproducto.Value >0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cpr_tproducto = {" + contador + "} ";
                valores.Add(calculo.cpr_tproducto);
                contador++;
            }

            parametros.valores = valores.ToArray();
            

            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;

            List<Calculoprecio> lst = CalculoprecioBLL.GetAllByPage(parametros, "cpr_nombre", desde, hasta);
            
            StringBuilder html = new StringBuilder();

            foreach (Calculoprecio item in lst)
            {

                ArrayList array = new ArrayList();
                array.Add(item.cpr_almacennombre.ToString());
                array.Add(item.cpr_rutanombre.ToString());
                array.Add(item.cpr_listaprecionombre);
                array.Add(item.cpr_tproductonombre);
                array.Add(item.cpr_nombre);
                array.Add(Formatos.CurrencyFormat(item.cpr_indice));
                array.Add(Formatos.CurrencyFormat(item.cpr_valor));
                array.Add(Formatos.CurrencyFormat(item.cpr_peso));
                string strid = "{\"cpr_listaprecio\":\"" + item.cpr_listaprecio + "\", \"cpr_empresa\":\"" + item.cpr_empresa + "\", \"cpr_codigo\":\"" + item.cpr_codigo + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));          

            }            

            return html.ToString();
        }


        [WebMethod]
        public static string GetCalculoPrecio(object objeto)
        {

            Calculoprecio calculo = new Calculoprecio(objeto);
            calculo.cpr_empresa_key = calculo.cpr_empresa;
            calculo.cpr_listaprecio_key = calculo.cpr_listaprecio;
            calculo.cpr_codigo_key = calculo.cpr_codigo; 

            calculo = CalculoprecioBLL.GetByPK(calculo);
   

            StringBuilder html = new StringBuilder();

      

            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span11\">");

            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(8, 2);
            tdatos.rows[0].cells[0].valor = "Almacen:";
            tdatos.rows[0].cells[1].valor = new Select { id = "cmbALMACEN_P", diccionario = Dictionaries.GetAlmacen(), withempty = true, valor = calculo.cpr_almacen, habilitado = (calculo.cpr_empresa > 0) ? false : true}.ToString();
            tdatos.rows[1].cells[0].valor = "Ruta:";
            tdatos.rows[1].cells[1].valor = new Select { id = "cmbRUTA_P", diccionario = Dictionaries.GetRuta(), withempty = true, valor = calculo.cpr_ruta, habilitado = (calculo.cpr_empresa > 0) ? false : true}.ToString();
            tdatos.rows[2].cells[0].valor = "Lista Precios:";
            tdatos.rows[2].cells[1].valor = new Select { id = "cmbLISTA_P", diccionario = Dictionaries.GetListaprecio(), withempty = true, valor = calculo.cpr_listaprecio, habilitado = (calculo.cpr_empresa > 0) ? false : true}.ToString();
            tdatos.rows[3].cells[0].valor = "Tipo Producto:";
            tdatos.rows[3].cells[1].valor = new Select { id = "cmbTPRODUCTO_P", diccionario = Dictionaries.GetTproducto(), valor = calculo.cpr_tproducto, habilitado = (calculo.cpr_empresa > 0) ? false : true}.ToString();
            tdatos.rows[4].cells[0].valor = "Nombre:";
            tdatos.rows[4].cells[1].valor = new Input { id = "txtNOMBRE_P",  valor = calculo.cpr_nombre, clase = Css.medium}.ToString();
            tdatos.rows[5].cells[0].valor = "Indice:";
            tdatos.rows[5].cells[1].valor = new Input { id = "txtINDICE_P", valor = Formatos.CurrencyFormat(calculo.cpr_indice), clase = Css.mini}.ToString();
            tdatos.rows[6].cells[0].valor = "Valor ($):";
            tdatos.rows[6].cells[1].valor = new Input { id = "txtVALOR_P", valor = Formatos.CurrencyFormat(calculo.cpr_valor), clase = Css.medium }.ToString();
            tdatos.rows[7].cells[0].valor = "Peso (%):";
            tdatos.rows[7].cells[1].valor = new Input { id = "txtPESO_P", valor = Formatos.CurrencyFormat( calculo.cpr_peso), clase = Css.medium }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(new Input {id ="txtCODIGO_P" ,visible = false , valor= calculo.cpr_codigo }.ToString());
            html.AppendLine(" </div><!--span11-->");
            html.AppendLine("</div><!--row-fluid-->");


            return html.ToString();
        }

      


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Calculoprecio obj = new Calculoprecio(objeto);
            if (obj.cpr_codigo > 0)
            {
                return UpdateComprobante(obj);
            }
            else
            {
                return InsertComprobante(obj);
            }
        }


        public static Calculoprecio CopyObj(Calculoprecio obj)
        {
            Calculoprecio c = new Calculoprecio();
            c.cpr_almacen = obj.cpr_almacen;
            c.cpr_almacennombre = obj.cpr_almacennombre;
            c.cpr_codigo = obj.cpr_codigo;
            c.cpr_codigo_key = obj.cpr_codigo_key;
            c.cpr_empresa = obj.cpr_empresa;
            c.cpr_empresa_key = obj.cpr_empresa_key;
            c.cpr_indice = obj.cpr_indice;
            c.cpr_listaprecio = obj.cpr_listaprecio;
            c.cpr_listaprecio_key = obj.cpr_listaprecio_key;
            c.cpr_listaprecionombre = obj.cpr_listaprecionombre;
            c.cpr_nombre = obj.cpr_nombre;
            c.cpr_peso = obj.cpr_peso;
            c.cpr_ruta = obj.cpr_ruta;
            c.cpr_rutanombre = obj.cpr_rutanombre;
            c.cpr_tproducto = obj.cpr_tproducto;
            c.cpr_tproductonombre = obj.cpr_tproductonombre;
            c.cpr_valor = obj.cpr_valor;
            return c;
 
        }

        public static string InsertComprobante(Calculoprecio obj)
        {

            List<Calculoprecio> lista = new List<Calculoprecio>();
            lista.Add(obj);              

            if (obj.cpr_almacen.Value < 1)
            {
                List<Calculoprecio> det = new List<Calculoprecio>(); 
                lista.Remove(obj); 
                Dictionary<string, string> dicalmacenes = Dictionaries.GetAlmacen();
                foreach (KeyValuePair<string, string> dic in dicalmacenes)
                {
                    Calculoprecio c = CopyObj(obj);
                    c.cpr_almacen = int.Parse(dic.Key);
                    det.Add(c);
                }
                lista.Clear();
                lista.AddRange(det); 
            }

            if (obj.cpr_ruta.Value < 1)
            {
                List<Calculoprecio> det = new List<Calculoprecio>(); 
                Dictionary<string, string> dicruta = Dictionaries.GetRuta();
                foreach (Calculoprecio item in lista)
                {                    
                    foreach (KeyValuePair<string, string> dic in dicruta)
                    {
                        Calculoprecio c = CopyObj(item);
                        c.cpr_ruta= int.Parse(dic.Key);
                        det.Add(c);
                    }
                    
                }
                lista.Clear();
                lista.AddRange(det); 
            }
            if (obj.cpr_listaprecio < 1)
            {
                List<Calculoprecio> det = new List<Calculoprecio>(); 
                Dictionary<string, string> diclista  = Dictionaries.GetListaprecio();
                foreach (Calculoprecio item in lista)
                {                    
                    foreach (KeyValuePair<string, string> dic in diclista)
                    {
                        Calculoprecio c = CopyObj(item);
                        c.cpr_listaprecio= int.Parse(dic.Key);
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
                foreach (Calculoprecio item in lista)
                {
                    CalculoprecioBLL.Insert(transaction, item);      
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


        public static string UpdateComprobante(Calculoprecio obj)
        {

            obj.cpr_empresa_key = obj.cpr_empresa;
            obj.cpr_listaprecio_key = obj.cpr_listaprecio;
            obj.cpr_codigo_key = obj.cpr_codigo;

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                CalculoprecioBLL.Update(transaction, obj);           
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
                        Calculoprecio obj = new Calculoprecio(item);
                        obj.cpr_empresa_key = obj.cpr_empresa;
                        obj.cpr_listaprecio_key = obj.cpr_listaprecio;
                        obj.cpr_codigo_key = obj.cpr_codigo;
                        CalculoprecioBLL.Delete(transaction, obj); 
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