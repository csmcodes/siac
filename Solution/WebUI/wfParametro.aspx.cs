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
    public partial class wfParametro : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "par_nombre";
        protected static string WhereClause = "";
        protected static WhereParams parametros ; 
           

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
                 
            }
        }


        public static string ShowData(Parametro obj)
        {
      
            return new HtmlObjects.ListItem { titulo = new string[] { obj.par_nombre }, descripcion = new string[] { obj.par_valor }, logico = new LogicItem[] { new LogicItem("Activos", obj.par_estado) } }.ToString();              

        }

        public static string ShowObject(Parametro obj)
        {

            StringBuilder html = new StringBuilder();       
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.par_nombre, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE_key", valor = obj.par_nombre_key, visible = false }.ToString());
            html.AppendLine(new Textarea { id = "txtDESCRIPCION", etiqueta = "Descripción", placeholder = "Descripción", valor = obj.par_descripcion, cols = 5, rows = 5, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtTIPO", etiqueta = "Tipo", placeholder = "Nombre", valor = obj.par_tipo, clase = Css.large }.ToString());
            html.AppendLine(new Textarea { id = "txtVALOR", etiqueta = "Valor", placeholder = "Valor", valor = obj.par_valor, cols=5 ,rows=5,clase = Css.large }.ToString());            
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.par_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
           
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Parametro obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();

            

            if (!string.IsNullOrEmpty(obj.par_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " par_nombre like {" + contador + "} ";
                valores.Add("%" + obj.par_nombre + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.par_valor))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " par_valor like {" + contador + "} ";
                valores.Add("%" + obj.par_valor + "%");
                contador++;
            }

            if (obj.par_estado.HasValue)
            {

                parametros.where += ((parametros.where != "") ? " and " : "") + " par_estado = {" + contador + "} ";
                valores.Add(obj.par_estado.Value);
                contador++;

            }

            parametros.valores = valores.ToArray(); 
           
                       
          


           


        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Parametro(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Parametro> lst = ParametroBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Parametro obj in lst)
            {
                
                html.AppendLine(new HtmlList { id = obj.par_nombre, content = ShowData(obj) }.ToString());
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
            return ShowObject(new Parametro());
        }


        [WebMethod]
        public static string GetObject(string id)
        {
            Parametro obj = new Parametro();
            obj.par_nombre = id;
            obj.par_nombre_key = id;
            obj = ParametroBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
            //return ShowObject(obj);

        }


        //public static Parametro GetObjeto(object objeto)
        //{
        //    Parametro obj = new Parametro();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object nombre = null;
        //        object nombrekey = null;
        //        object descripcion = null;
        //        object tipo = null;
        //        object valor = null;
               
        //        object activo = null;
        //        tmp.TryGetValue("par_nombre", out nombre);
        //        tmp.TryGetValue("par_nombre_key", out nombrekey);
        //        tmp.TryGetValue("par_descripcion", out descripcion);
        //        tmp.TryGetValue("par_tipo", out tipo);
        //        tmp.TryGetValue("par_valor", out valor);              
        //        tmp.TryGetValue("par_estado", out activo);
        //        obj.par_nombre = (string)nombre;
        //        obj.par_nombre_key = (string)nombrekey;
        //        obj.par_descripcion = (string)descripcion;
        //        obj.par_valor = (string)valor;
        //        obj.par_tipo = (string)tipo;
        //        obj.par_estado = (int?)activo;
        //        obj.crea_usr = "admin";
        //        obj.crea_fecha = DateTime.Now;
        //        obj.mod_usr = "admin";
        //        obj.mod_fecha = DateTime.Now;

        //    }

        //    return obj;
        //}


        [WebMethod]
        public static string GetSearch()
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class='pull-left'>");
            html.AppendLine(new Input { id = "txtVALOR_S", placeholder = "Valor", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE_S", placeholder = "Nombre", clase = Css.large }.ToString());            
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
            return ShowObject(new Parametro());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Parametro obj = new Parametro(objeto);
      //      obj.par_nombre_key = obj.par_nombre;
            if (obj.par_nombre_key != "")
            {
                if (ParametroBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (ParametroBLL.Insert(obj) > 0)
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
            Parametro obj = new Parametro(objeto);
            obj.par_nombre_key = obj.par_nombre;
            


            if (ParametroBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";

        }
    }
}