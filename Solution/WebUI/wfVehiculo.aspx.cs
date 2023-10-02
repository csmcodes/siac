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
using HtmlObjects;

namespace WebUI
{
    public partial class wfVehiculo : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "veh_placa";
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


        public static string ShowData(Vehiculo obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.veh_tipovehiculo, "-", obj.veh_modelo, "-", obj.veh_placa }, descripcion = new string[] { obj.veh_nombreduenio }, logico = new LogicItem[] { new LogicItem("Activos", obj.veh_estado) } }.ToString();
        }

        public static string ShowObject(Vehiculo obj)
        {

            StringBuilder html = new StringBuilder();            
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.veh_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.veh_codigo.ToString(), visible = false }.ToString());
           html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.veh_id, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.veh_nombre, clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "txtTIPOVEHICULO", etiqueta = "Tipo Vehículo", valor = obj.veh_tipovehiculo, clase = Css.large, diccionario = Dictionaries.GetTipoVehiculos(), obligatorio = true }.ToString());
            html.AppendLine(new Select { id = "txtMODELO", etiqueta = "Modelo", valor = obj.veh_modelo, clase = Css.large, diccionario = Dictionaries.GetModeloVehiculos() }.ToString());
            html.AppendLine(new Select { id = "cmbANIO", etiqueta = "Año", valor = obj.veh_anio.ToString(), clase = Css.large, diccionario = Dictionaries.GetAnios() }.ToString());
            html.AppendLine(new Input { id = "txtPLACA", etiqueta = "Placa", placeholder = "Placa", valor = obj.veh_placa, clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbDUENIO", etiqueta = "Dueño", valor = obj.veh_duenio.ToString(), clase = Css.large, diccionario = Dictionaries.GetSocios() }.ToString());
            html.AppendLine(new Select { id = "cmbCHOFER1", etiqueta = "Chofer1", valor = obj.veh_chofer1.ToString(), clase = Css.large, diccionario = Dictionaries.GetChofer() }.ToString());
            html.AppendLine(new Select { id = "cmbCHOFER2", etiqueta = "Chofer2", valor = obj.veh_chofer2.ToString(), clase = Css.large, diccionario = Dictionaries.GetChofer() }.ToString());

            html.AppendLine(new Input { id = "txtDISCO", etiqueta = "Disco", placeholder = "Disco", valor = obj.veh_disco, clase = Css.large }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.veh_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());



            return html.ToString();
        }


        public static void SetWhereClause(Vehiculo obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (!string.IsNullOrEmpty(obj.veh_tipovehiculo))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " veh_tipovehiculo like {" + contador + "} ";
                valores.Add("%" + obj.veh_tipovehiculo + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.veh_modelo))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " veh_modelo like {" + contador + "} ";
                valores.Add("%" + obj.veh_modelo + "%");
                contador++;
            }
            if (obj.veh_duenio >0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " veh_duenio = {" + contador + "} ";
                valores.Add(obj.veh_duenio );
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.veh_placa))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " veh_placa like {" + contador + "} ";
                valores.Add("%" + obj.veh_placa + "%");
                contador++;
            }

            if (obj.veh_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " veh_estado = {" + contador + "} ";
                valores.Add(obj.veh_estado.Value);
                contador++;

            }
            if (obj.veh_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " veh_empresa = {" + contador + "} ";
                valores.Add(obj.veh_empresa);
                contador++;

            }
            parametros.valores = valores.ToArray();          
            
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Vehiculo(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Vehiculo> lst = VehiculoBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Vehiculo obj in lst)
            {
                string id = "{\"veh_codigo\":\"" + obj.veh_codigo + "\", \"veh_empresa\":\"" + obj.veh_empresa + "\"}";//ID COMPUESTO
               
                html.AppendLine(new HtmlList { id = id, content = ShowData(obj) }.ToString());
            }

            return html.ToString();
        }

        [WebMethod]
        public static string ReloadData(object objeto)
        {
            pageIndex = 1;
            return GetData(objeto);
        }



        [WebMethod]
        public static string AddObject()
        {
            return ShowObject(new Vehiculo());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
           
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("veh_codigo", out codigo);
            tmp.TryGetValue("veh_empresa", out empresa);

            Vehiculo obj = new Vehiculo();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.veh_empresa = int.Parse(empresa.ToString());
                obj.veh_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.veh_codigo = int.Parse(codigo.ToString());
                obj.veh_codigo_key = int.Parse(codigo.ToString());
            }

            obj = VehiculoBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
            //return ShowObject(obj);
            //return ShowObject(obj);

        }

        /**
        public static Vehiculo GetObjeto(object objeto)
        {
            Vehiculo obj = new Vehiculo();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object codigo = null;
                object codigokey = null;
                object empresa = null;
                object empresakey = null;
                object disco = null;
                object id = null;
                object placa = null;
                object nombre = null;
                object modelo = null;
                object tipovehiculo = null;
                object año = null;
                object activo = null;
                object dueño = null;
                object nombreduenio = null;
                object veh_chofer1 = null;
                object veh_chofer2 = null;
                tmp.TryGetValue("veh_codigo", out codigo);
                tmp.TryGetValue("veh_codigo_key", out codigokey);
                tmp.TryGetValue("veh_empresa", out empresa);
                tmp.TryGetValue("veh_empresa_key", out empresakey);
                tmp.TryGetValue("veh_disco", out disco);
                tmp.TryGetValue("veh_nombre", out nombre);
                tmp.TryGetValue("veh_id", out id);
                tmp.TryGetValue("veh_placa", out placa);
                tmp.TryGetValue("veh_anio", out año);
                tmp.TryGetValue("veh_modelo", out modelo);
                tmp.TryGetValue("veh_tipovehiculo", out tipovehiculo);
                tmp.TryGetValue("veh_duenio", out dueño);
                tmp.TryGetValue("veh_estado", out activo);
                tmp.TryGetValue("veh_nombreduenio", out nombreduenio);
                tmp.TryGetValue("veh_chofer1", out veh_chofer1);
                tmp.TryGetValue("veh_chofer2", out veh_chofer2);


                if (codigo != null && !codigo.Equals(""))
                {
                    obj.veh_codigo = int.Parse(codigo.ToString());
                }
                if (codigokey != null && !codigokey.Equals(""))
                {
                    obj.veh_codigo_key = int.Parse(codigo.ToString());
                }
                
                if (año != null && !año.Equals(""))
                {
                    obj.veh_anio = int.Parse(año.ToString());
                }               
                
                if (empresa != null && !empresa.Equals(""))
                {
                    obj.veh_empresa = int.Parse(empresa.ToString());
                }
                if (empresakey != null && !empresakey.Equals(""))
                {
                    obj.veh_empresa_key = int.Parse(empresakey.ToString());
                }

                obj.veh_chofer1 = (int?)veh_chofer1;
                obj.veh_chofer2 = (int?)veh_chofer2;              
                obj.veh_tipovehiculo = (string)tipovehiculo;
                obj.veh_placa = (string)placa;
                obj.veh_id = (string)id;
                obj.veh_nombre = (string)nombre;
                obj.veh_modelo = (string)modelo;
                obj.veh_duenio = (int?)dueño;
                obj.veh_disco = (string)disco;
                obj.veh_estado = (int?)activo;
                obj.crea_usr = "admin";
                obj.crea_fecha = DateTime.Now;
                obj.mod_usr = "admin";
                obj.mod_fecha = DateTime.Now;
                obj.veh_nombreduenio =(string) nombreduenio;



            }

            return obj;
        }
        */

        [WebMethod]
        public static string GetSearch()
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class='pull-left'>");  
            html.AppendLine(new Input { id = "txtPLACA_S", placeholder = "Placa" }.ToString());
            html.AppendLine(new Input { id = "txtTIPOVEHICULO_S", placeholder = "Tipo" }.ToString());
            html.AppendLine(new Input { id = "txtMODELO_S", placeholder = "Modelo" }.ToString());
            html.AppendLine(new Input { id = "cmbDUENIO_S", placeholder = "Dueño" }.ToString());
            html.AppendLine(new Select { id = "cmbESTADO_S", clase = Css.medium, diccionario = Dictionaries.GetEstado() }.ToString());
            html.AppendLine("</div>");
            html.AppendLine("<div class='pull-right'>");
            html.AppendLine(new Boton { refresh=true }.ToString());
            html.AppendLine(new Boton { clean = true }.ToString());
            html.AppendLine("</div>");
            return html.ToString();
        }

        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Vehiculo());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Vehiculo obj = new Vehiculo(objeto);
            if (obj.veh_codigo_key > 0)
            {
                if (VehiculoBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (VehiculoBLL.Insert(obj) > 0)
                {
                    return "OK";
                }
                else
                    return "ERROR";
            }

        }

        [WebMethod]
        public static string DeleteObject(object objeto)
        {
            Vehiculo obj = new Vehiculo(objeto);
            if (VehiculoBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";


        }
    }
}