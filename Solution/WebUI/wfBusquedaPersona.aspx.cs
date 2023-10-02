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
    public partial class wfBusquedaPersona : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "per_nombres";
        protected static string WhereClause = "";
        protected static WhereParams parametros = new WhereParams();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
                //lstvehiculo = VehiculoBLL.GetAll("", "");
                //dicVehiculo = lstvehiculo.ToDictionary(p => p.veh_codigo.ToString(), p => p.veh_tipovehiculo + " " + p.veh_placa + " " + p.veh_modelo);


            }
        }

        [WebMethod]
        public static string GetFiltros()
        {

            StringBuilder html = new StringBuilder();            
            html.AppendLine(new Input { id = "txtID", placeholder = "Identificación", clase = Css.medium }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRES", placeholder = "Nombres", clase = Css.xlarge }.ToString());
            html.AppendLine(new Input { id = "txtCIRUC", placeholder = "CI o RUC", clase = Css.xlarge }.ToString());
            return html.ToString();
        }


        [WebMethod]
        public static string GetListadoHead()
        {

            StringBuilder html = new StringBuilder();
            ArrayList array = new ArrayList();
            array.Add("Id");
            array.Add("Nombres");
            array.Add("CI o RUC");
            html.AppendLine(HtmlElements.HeadRow(array));            
            return html.ToString();
        }


        public static Persona GetObjeto(object objeto)
        {
            Persona obj = new Persona();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object id = null;
                object nombres = null;
                object ciruc = null;
                object empresa = null;
                tmp.TryGetValue("per_id", out id);
                tmp.TryGetValue("per_nombres", out nombres);
                tmp.TryGetValue("per_ciruc", out ciruc);
                tmp.TryGetValue("per_empresa", out empresa);
                /*if (fecha != null && !fecha.Equals(""))
                {
                    obj.bxv_fecha = Convert.ToDateTime(fecha.ToString());
                }
                if (fechakey != null && !fechakey.Equals(""))
                {
                    obj.bxv_fecha_key = Convert.ToDateTime(fechakey.ToString());
                }*/

                obj.per_id = (string)id;
                obj.per_nombres = (string)nombres;
                obj.per_ciruc = (string)ciruc;
                // obj.bxv_imagen = Convert.ToByte(imagen);
                obj.per_empresa = (int)empresa;
                obj.crea_usr = "admin";
                obj.crea_fecha = DateTime.Now;
                obj.mod_usr = "admin";
                obj.mod_fecha = DateTime.Now;

            }
            return obj;
        }

        public static void SetWhereClause(Persona obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (!string.IsNullOrEmpty(obj.per_ciruc))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " per_ciruc like {" + contador + "} ";
                valores.Add("%" + obj.per_ciruc + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.per_nombres))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " per_nombres like {" + contador + "} ";
                valores.Add("%" + obj.per_nombres + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.per_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " per_id like {" + contador + "} ";
                valores.Add("%" + obj.per_id + "%");
                contador++;
            }
            if (obj.per_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " per_empresa = {" + contador + "} ";
                valores.Add(obj.per_empresa);
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
            SetWhereClause(new Persona(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            

            StringBuilder html = new StringBuilder();
            List<Persona> lista = PersonaBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);            
            foreach (Persona item in lista)
            {
                ArrayList array = new ArrayList();
                array.Add("");
                array.Add(item.per_id);                
                array.Add(item.per_nombres+" "+item.per_apellidos);
                array.Add(item.per_ciruc);
                //array.Add(item.bxv_imagen);

                string strid = item.per_codigo.ToString(); 
                html.AppendLine(HtmlElements.TablaRowBusqueda(array, strid));
            }

            return html.ToString();
        }

        
    }
}