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

    public partial class wfRetdato : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "rtd_codigo";
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


        public static string ShowData(Retdato obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.rtd_campo, "-", obj.rtd_id }, descripcion = new string[] { }, logico = new LogicItem[] { new LogicItem("Activos", obj.rtd_estado) } }.ToString();
        }

        public static string ShowObject(Retdato obj)
        {

            StringBuilder html = new StringBuilder();

            html.AppendLine(new Select { id = "cmbTABLACOA", etiqueta = "Tablacoa", valor = obj.rtd_tablacoa.ToString(), clase = Css.large, diccionario = Dictionaries.GetTablacoa() }.ToString());
            html.AppendLine(new Input { id = "txtTABLACOA_key", valor = obj.rtd_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.rtd_tablacoa_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.rtd_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.rtd_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtCAMPO", etiqueta = "Campo", placeholder = "Campo", valor = obj.rtd_campo, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.rtd_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Retdato obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();


            if (!string.IsNullOrEmpty(obj.rtd_campo))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " rtd_campo like {" + contador + "} ";
                valores.Add("%" + obj.rtd_campo + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.rtd_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " rtd_id like {" + contador + "} ";
                valores.Add("%" + obj.rtd_id + "%");
                contador++;
            }

            if (obj.rtd_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " rtd_estado = {" + contador + "} ";
                valores.Add(obj.rtd_estado.Value);
                contador++;

            }
            if (obj.rtd_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " rtd_empresa = {" + contador + "} ";
                valores.Add(obj.rtd_empresa);
                contador++;

            }
            parametros.valores = valores.ToArray();





        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Retdato(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Retdato> lst = RetdatoBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Retdato obj in lst)
            {
                string id = "{\"rtd_empresa\":\"" + obj.rtd_empresa + "\", \"rtd_tablacoa\":\"" + obj.rtd_tablacoa + "\", \"rtd_codigo\":\"" + obj.rtd_codigo + "\"}";//ID COMPUESTO              
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
            return ShowObject(new Retdato());
        }


        [WebMethod]
        public static string GetObject(object id)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object tablacoa = null;
            object empresa = null;
            tmp.TryGetValue("rtd_codigo", out codigo);
            tmp.TryGetValue("rtd_tablacoa", out tablacoa);
            tmp.TryGetValue("rtd_empresa", out empresa);
            Retdato obj = new Retdato();
            if (codigo != null && !codigo.Equals(""))
            {
                obj.rtd_codigo = int.Parse(codigo.ToString());
                obj.rtd_codigo_key = int.Parse(codigo.ToString());
            }
            if (tablacoa != null && !tablacoa.Equals(""))
            {
                obj.rtd_tablacoa = int.Parse(tablacoa.ToString());
                obj.rtd_tablacoa_key = int.Parse(tablacoa.ToString());
            }
            if (empresa != null && !empresa.Equals(""))
            {
                obj.rtd_empresa = int.Parse(empresa.ToString());
                obj.rtd_empresa_key = int.Parse(empresa.ToString());
            }

            obj = RetdatoBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }

        /*
        public static Retdato GetObjeto(object objeto)
        {
            Retdato obj = new Retdato();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object secuencia = null;
                object modulo = null;
                object modulokey = null;
                object secuenciakey = null;
                object id = null;
                object nombre = null;
                object nombremodulo = null;
                object activo = null;

                tmp.TryGetValue("rtd_secuencia", out secuencia);
                tmp.TryGetValue("rtd_modulo", out modulo);
                tmp.TryGetValue("rtd_secuencia_key", out secuenciakey);
                tmp.TryGetValue("rtd_modulo_key", out modulokey);
                tmp.TryGetValue("rtd_id", out id);
                tmp.TryGetValue("rtd_nombre", out nombre);
                tmp.TryGetValue("rtd_nombremodulo", out nombremodulo);
                tmp.TryGetValue("rtd_estado", out activo);
                if (modulo != null && !modulo.Equals(""))
                {
                    obj.rtd_modulo = int.Parse(modulo.ToString());
                }
                if (secuencia != null && !secuencia.Equals(""))
                {
                    obj.rtd_secuencia = int.Parse(secuencia.ToString());
                }
                if (modulokey != null && !modulokey.Equals(""))
                {

                    obj.rtd_modulo_key = int.Parse(modulokey.ToString());
                }
                if (secuenciakey != null && !secuenciakey.Equals(""))
                {

                    obj.rtd_secuencia_key = int.Parse(secuenciakey.ToString());
                }

                obj.rtd_id = (string)id;
                obj.rtd_nombre = (string)nombre;

                obj.rtd_estado = (int?)activo;
                obj.crea_usr = "admin";
                obj.crea_fecha = DateTime.Now;
                obj.mod_usr = "admin";
                obj.mod_fecha = DateTime.Now;


                obj.rtd_nombremodulo = (string)nombremodulo;
            }

            return obj;
        }
*/

        [WebMethod]
        public static string GetSearch()
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class='pull-left'>");
            //     html.AppendLine(HtmlElements.Input("Id", "txtSECUENCIA_S", "", HtmlElements.small, false));


            html.AppendLine(new Input { id = "txtNOMBRE_S", placeholder = "Nombre", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtID_S", placeholder = "Id", clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbESTADO_S", clase = Css.medium, diccionario = Dictionaries.GetEstado() }.ToString());


            html.AppendLine("</div>");
            html.AppendLine("<div class='pull-right'>");
            html.AppendLine(new Boton { refresh = true }.ToString());
            html.AppendLine(new Boton { clean = true }.ToString());
            html.AppendLine("</div>");
            return html.ToString();
        }

        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Retdato());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Retdato obj = new Retdato(objeto);

            if (obj.rtd_codigo_key > 0)
            {
                if (RetdatoBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (RetdatoBLL.Insert(obj) > 0)
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
            Retdato obj = new Retdato(objeto);
            if (RetdatoBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}