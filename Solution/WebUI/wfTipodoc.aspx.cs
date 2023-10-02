
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
    public partial class wfTipodoc : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "tpd_codigo";
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


        public static string ShowData(Tipodoc obj)
        {

           string aux = "";
            if (obj.tpd_nocontable == 0)
                aux = "Contable";
            if (obj.tpd_nocontable == 1)
                aux = "No Contable";

            return new HtmlObjects.ListItem { titulo = new string[] { obj.tpd_codigo.ToString(), "-", obj.tpd_nombre }, descripcion = new string[] { aux }, logico = new LogicItem[] { new LogicItem("Activos", obj.tpd_estado) } }.ToString();              

        }

        public static string ShowObject(Tipodoc obj)
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.tpd_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.tpd_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.tpd_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.tpd_nombre, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Select { id = "cmbMODULO", etiqueta = "Modulo", valor = obj.tpd_modulo.ToString(), clase = Css.large, diccionario = Dictionaries.GetModulos(), obligatorio = true }.ToString());
            html.AppendLine(new Select { id = "cmbFOR_IMP", etiqueta = "Programa Impresion", valor = obj.tpd_for_imp.ToString(), clase = Css.large, diccionario = Dictionaries.GetFormularios() }.ToString());
            html.AppendLine(new Select { id = "cmbFOR_EJE", etiqueta = "Programa Ejecucion", valor = obj.tpd_for_eje.ToString(), clase = Css.large, diccionario = Dictionaries.GetFormularios() }.ToString());
            html.AppendLine(new Select { id = "cmbFOR_CON", etiqueta = "Programa Consulta", valor = obj.tpd_for_con.ToString(), clase = Css.large, diccionario = Dictionaries.GetFormularios() }.ToString());
            html.AppendLine(new Check { id = "chkNOCONTABLE", etiqueta = "No contable ", valor = obj.tpd_nocontable }.ToString());
            html.AppendLine(new Input { id = "txtNIVELAPROVACION", etiqueta = "Nivel de aprovacion", placeholder = "Nivel de aprovacion", valor = obj.tpd_nivel_aprobacion.ToString(), clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNROCOPIAS", etiqueta = "Numero de copias", placeholder = "Numero de copias", valor = obj.tpd_nro_copias.ToString(), clase = Css.large, obligatorio = true}.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.tpd_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Tipodoc obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();

            if (obj.tpd_codigo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tpd_codigo = {" + contador + "} ";
                valores.Add(obj.tpd_codigo);
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.tpd_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tpd_nombre like {" + contador + "} ";
                valores.Add("%" + obj.tpd_nombre + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.tpd_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tpd_id like {" + contador + "} ";
                valores.Add("%" + obj.tpd_id + "%");
                contador++;
            }

            if (obj.tpd_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " tpd_estado = {" + contador + "} ";
                valores.Add(obj.tpd_estado.Value);
                contador++;

            }
            parametros.valores = valores.ToArray();
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause( new Tipodoc(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Tipodoc> lst = TipodocBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Tipodoc obj in lst)
            {
                //   string id = "{\"alm_codigo\":\"" + obj.alm_codigo + "\", \"alm_empresa\":\"" + obj.alm_empresa + "\"}";//ID COMPUESTO
           //     html.AppendLine(HtmlElements.HtmlList(obj.tpd_codigo.ToString(), ShowData(obj)));
                html.AppendLine(new HtmlList { id = obj.tpd_codigo.ToString(), content = ShowData(obj) }.ToString());

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
            return ShowObject(new Tipodoc());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            Tipodoc obj = new Tipodoc();
            obj.tpd_codigo = int.Parse(id.ToString());
            obj.tpd_codigo_key = int.Parse(id.ToString());
            obj = TipodocBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);

        }


        //public static Tipodoc GetObjeto(object objeto)
        //{
        //    Tipodoc obj = new Tipodoc();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object codigokey = null;
        //        object id = null;
        //        object nombre = null;
        //        object modulo = null;
        //        object for_imp = null;
        //        object for_eje = null;
        //        object for_con = null;
        //        object nocontable = null;
        //        object nivel_aprovacion = null;
        //        object nro_copias = null;
        //        object activo = null;
        //        tmp.TryGetValue("tpd_codigo", out codigo);
        //        tmp.TryGetValue("tpd_codigo_key", out codigokey);
        //        tmp.TryGetValue("tpd_id", out id);
        //        tmp.TryGetValue("tpd_nombre", out nombre);
        //        tmp.TryGetValue("tpd_modulo", out modulo);
        //        tmp.TryGetValue("tpd_for_imp", out for_imp);
        //        tmp.TryGetValue("tpd_for_eje", out for_eje);
        //        tmp.TryGetValue("tpd_for_con", out for_con);
        //        tmp.TryGetValue("tpd_nocontable", out nocontable);
        //        tmp.TryGetValue("tpd_nivel_aprobacion", out nivel_aprovacion);
        //        tmp.TryGetValue("tpd_nro_copias", out nro_copias);
        //        tmp.TryGetValue("tpd_estado", out activo);
        //        if (codigo != null && !codigo.Equals(""))
        //        {
        //            obj.tpd_codigo = int.Parse(codigo.ToString());
        //            obj.tpd_codigo_key = int.Parse(codigo.ToString());
        //        }
        //        obj.tpd_id = (string)id;
        //        obj.tpd_nombre = (string)nombre;
        //        obj.tpd_modulo = Convert.ToInt32(modulo);
        //        obj.tpd_for_imp = Convert.ToInt32(for_imp);
        //        obj.tpd_for_eje = Convert.ToInt32(for_eje);
        //        obj.tpd_for_con = Convert.ToInt32(for_con);
        //        obj.tpd_nocontable = Convert.ToInt32(nocontable);
        //        obj.tpd_nivel_aprobacion = Convert.ToInt32(nivel_aprovacion);
        //        obj.tpd_nro_copias = Convert.ToInt32(nro_copias);
        //        obj.tpd_estado = (int?)activo;
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
            //     html.AppendLine(HtmlElements.Input("Id", "txtCODIGO_S", "", HtmlElements.small, false));
     
            html.AppendLine(new Input { id = "txtNOMBRE_S", placeholder = "Nombre", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtID_S", placeholder = "Id", clase = Css.large }.ToString());
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
            return ShowObject(new Tipodoc());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Tipodoc obj = new Tipodoc(objeto);
            obj.tpd_codigo_key = obj.tpd_codigo;


            if (obj.tpd_codigo_key > 0)
            {
                if (TipodocBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (TipodocBLL.Insert(obj) > 0)
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
            Tipodoc obj = new Tipodoc(objeto);
            obj.tpd_codigo_key = obj.tpd_codigo;
            if (TipodocBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}