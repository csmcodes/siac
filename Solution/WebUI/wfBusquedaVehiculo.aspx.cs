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
    public partial class wfBusquedaVehiculo : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "veh_codigo";
        protected static string WhereClause = "";
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
        public static string GetFiltros()
        {
            StringBuilder html = new StringBuilder();
        //    html.AppendLine(HtmlElements.LabelSelect("Socio", "txtNOMBREDUENIO", "",Dictionaries.GetSocios()));
            html.AppendLine(new Select { id = "txtNOMBREDUENIO", etiqueta = "Socio", clase = Css.large, diccionario = Dictionaries.GetSocios() }.ToString());
            html.AppendLine(new Input { id = "txtMODELO", placeholder = "Modelo", clase = Css.xlarge }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", placeholder = "Nombre", clase = Css.xlarge }.ToString());
            html.AppendLine(new Input { id = "txtID", placeholder = "Id", clase = Css.xlarge }.ToString());
            html.AppendLine(new Input { id = "txtTIPOVEHICULO", placeholder = "Tipo Vehiculo", clase = Css.xlarge }.ToString());
            html.AppendLine(new Input { id = "txtPLACA", placeholder = "Placa", clase = Css.xlarge }.ToString());
            html.AppendLine(new Input { id = "txtDISCO", placeholder = "Disco", clase = Css.xlarge }.ToString());
           
            return html.ToString();
        }


        [WebMethod]
        public static string GetListadoHead()
        {
            StringBuilder html = new StringBuilder();
            ArrayList array = new ArrayList();
            array.Add("Socio");
            array.Add("Id");
            array.Add("Nombre");
            array.Add("Modelo");
            array.Add("Tipo Vehículo");
            array.Add("Placa");
            array.Add("Disco");
            html.AppendLine(HtmlElements.HeadRow(array));
            return html.ToString();
        }


        //public static Vehiculo GetObjeto(object objeto)
        //{
        //    Vehiculo obj = new Vehiculo();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object duenio = null;
        //        object modelo = null;
        //        object tipovehiculo = null;
        //        object id = null;
        //        object placa = null;
        //        object disco = null;
        //        object nombre = null;
        //        object empresa = null;
        //        tmp.TryGetValue("veh_nombreduenio", out duenio);
        //        tmp.TryGetValue("veh_id", out id);
        //        tmp.TryGetValue("veh_modelo", out modelo);
        //        tmp.TryGetValue("veh_tipovehiculo", out tipovehiculo);
        //        tmp.TryGetValue("veh_placa", out placa);
        //        tmp.TryGetValue("veh_disco", out disco);
        //        tmp.TryGetValue("veh_nombre", out nombre);
        //        tmp.TryGetValue("veh_empresa", out empresa);
        //        obj.veh_duenio = (int?)duenio;
        //        obj.veh_empresa = (int)empresa;
        //        obj.veh_id = (string)id;
        //        obj.veh_nombre = (string)nombre;
        //        obj.veh_modelo = (string)modelo;
        //        obj.veh_tipovehiculo = (string)tipovehiculo;
        //        obj.veh_placa = (string)placa;
        //        obj.veh_disco = (string)disco;                
        //        obj.crea_usr = "admin";
        //        obj.crea_fecha = DateTime.Now;
        //        obj.mod_usr = "admin";
        //        obj.mod_fecha = DateTime.Now;                
        //    }
        //    return obj;
        //}

        public static void SetWhereClause(Vehiculo obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (obj.veh_duenio.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " veh_duenio like {" + contador + "} ";
                valores.Add("%" + obj.veh_duenio + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.veh_modelo))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " veh_modelo like {" + contador + "} ";
                valores.Add("%" + obj.veh_modelo + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.veh_tipovehiculo))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " veh_tipovehiculo like {" + contador + "} ";
                valores.Add("%" + obj.veh_tipovehiculo + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.veh_placa))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " veh_placa like {" + contador + "} ";
                valores.Add("%" + obj.veh_placa + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.veh_disco))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " veh_disco like {" + contador + "} ";
                valores.Add("%" + obj.veh_disco + "%");
                contador++;
            }

            if (!string.IsNullOrEmpty(obj.veh_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " veh_id like {" + contador + "} ";
                valores.Add("%" + obj.veh_id + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.veh_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " veh_nombre like {" + contador + "} ";
                valores.Add("%" + obj.veh_nombre + "%");
                contador++;
            }
            if (obj.veh_empresa> 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " veh_empresa = {" + contador + "} ";
                valores.Add(obj.veh_empresa);
                contador++;

            }
            parametros.valores = valores.ToArray(); 
           
        }


        [WebMethod]
        public static string ReloadDetalle(object objeto)
        {
            pageIndex = 1;
            return GetDetalle(objeto);
        }


        [WebMethod]
        public static string GetDetalle(object objeto)
        {
            SetWhereClause(new Vehiculo(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Vehiculo> lista = VehiculoBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);           
            foreach (Vehiculo item in lista)
            {
                ArrayList array = new ArrayList();
                array.Add("");
                array.Add(item.veh_nombreduenio);
                array.Add(item.veh_id);
                array.Add(item.veh_nombre); 
                array.Add(item.veh_modelo);
                array.Add(item.veh_tipovehiculo);
                array.Add(item.veh_placa);
                array.Add(item.veh_disco);
                                string strid = item.veh_codigo.ToString();
                html.AppendLine(HtmlElements.TablaRowBusqueda(array, strid));
            }

            return html.ToString();
        }


    }
}