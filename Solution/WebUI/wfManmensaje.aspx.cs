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
    public partial class wfManmensaje : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "msj_codigo";
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


        public static string ShowData(Mensaje obj)
        {




            return new HtmlObjects.ListItem { titulo = new string[] { obj.msj_usuarioenvia }, descripcion = new string[] { obj.msj_asunto }, logico = new LogicItem[] { } }.ToString();
        }

        public static string ShowObject(Mensaje obj)
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.msj_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.msj_codigo_key.ToString(), visible = false }.ToString());
           html.AppendLine(new Input { id = "txtID",  placeholder = "Id", valor = obj.msj_id, clase = Css.large, visible = false }.ToString());
            html.AppendLine(new Select { id = "cmbDESTINO", etiqueta = "Para", placeholder = "Para", diccionario = Dictionaries.GetUsuario(), multiselect = true}.ToString());
            html.AppendLine(new Input { id = "txtASUNTO", etiqueta = "Asunto", placeholder = "Asunto", valor = obj.msj_asunto, clase = Css.large }.ToString());
            html.AppendLine(new Textarea { id = "txtMENSAJE", etiqueta = "Mensaje", placeholder = "Mensaje", valor = obj.msj_mensaje, rows = 15, clase = Css.large, obligatorio = true }.ToString());
    //      html.AppendLine(new Input { id = "cmbFECHACREACION",  datepicker = true, datetimevalor = DateTime.Now, clase = Css.large, visible = false}.ToString());
            html.AppendLine(new Input { id = "cmbFECHAENNVIO", datepicker = true, datetimevalor = (obj.msj_fechaenvio.HasValue) ? obj.msj_fechaenvio.Value : DateTime.Now, clase = Css.large, visible = false }.ToString()); 
            html.AppendLine(new Input { id = "txtUSUARIOENVIA", valor = obj.msj_usuarioenvia, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtESTADOENVIA", valor = obj.msj_estadoenvio, visible = false }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.msj_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }

        [WebMethod]
        public static string GetDestino(object objeto)
        {
            StringBuilder html = new StringBuilder();
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object empresa = null;
            tmp.TryGetValue("empresa", out empresa);            
            html.AppendLine(new Select { id = "cmbDESTINO", placeholder = "Para", diccionario = Dictionaries.GetUsuarioxEmpresa(Convert.ToInt32(empresa)), multiselect = true, obligatorio = true }.ToString());            
            return html.ToString();

        }

        public static void SetWhereClause(Mensaje obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();

            

            if (!string.IsNullOrEmpty(obj.msj_usuarioenvia))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " msj_usuarioenvia like {" + contador + "} ";
                valores.Add("%" + obj.msj_usuarioenvia + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.msj_asunto))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " msj_asunto like {" + contador + "} ";
                valores.Add("%" + obj.msj_asunto + "%");
                contador++;
            }
            
            if (obj.msj_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " msj_estado = {" + contador + "} ";
                valores.Add(obj.msj_estado.Value);
                contador++;

            }
            parametros.valores = valores.ToArray();





        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Mensaje(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Mensaje> lst = MensajeBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Mensaje obj in lst)
            {
                string id = "{\"msj_codigo\":\"" + obj.msj_codigo + "\", \"msj_empresa\":\"" + obj.msj_empresa + "\"}";//ID COMPUESTO

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
            return ShowObject(new Mensaje());
        }


        [WebMethod]
        public static string GetObject(object id)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("msj_codigo", out codigo);
            tmp.TryGetValue("msj_empresa", out empresa);

            Mensaje obj = new Mensaje();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.msj_empresa = int.Parse(empresa.ToString());
                obj.msj_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.msj_codigo = int.Parse(codigo.ToString());
                obj.msj_codigo_key = int.Parse(codigo.ToString());
            }

            obj = MensajeBLL.GetByPK(obj);
            obj.destino = MensajedestinoBLL.GetAll("msd_mensaje=" + obj.msj_codigo + "and msd_empresa=" + obj.msj_empresa, "");
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Mensaje GetObjeto(object objeto)
        //{
        //    Mensaje obj = new Mensaje();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object empresa = null;
        //        object empresakey = null;
        //        object codigokey = null;
        //        object id = null;
        //        object usuarioenvia = null;
                
        //        object mensaje = null;
        //        object estadoenvio = null;
        //        object asunto = null;
        //        object fechacreacion = null;
        //        object fechaenvio = null;                
        //        object activo = null;
        //        object destino = null;
        //        tmp.TryGetValue("msj_codigo", out codigo);
        //        tmp.TryGetValue("msj_empresa", out empresa);
        //        tmp.TryGetValue("msj_codigo_key", out codigokey);
        //        tmp.TryGetValue("msj_empresa_key", out empresakey);
        //        tmp.TryGetValue("msj_id", out id);
        //        tmp.TryGetValue("msj_usuarioenvia", out usuarioenvia);
        //        tmp.TryGetValue("msj_mensaje", out mensaje);
        //        tmp.TryGetValue("msj_estadoenvio", out estadoenvio);
        //        tmp.TryGetValue("msj_asunto", out asunto);
        //        tmp.TryGetValue("msj_fechacreacion", out fechacreacion);
        //        tmp.TryGetValue("msj_fechaenvio", out fechaenvio);               
        //        tmp.TryGetValue("msj_estado", out activo);
        //        tmp.TryGetValue("destino", out destino);
        //        if (empresa != null && !empresa.Equals(""))
        //        {
        //            obj.msj_empresa = int.Parse(empresa.ToString());
        //        }
        //        if (empresakey != null && !empresakey.Equals(""))
        //        {
        //            obj.msj_empresa_key = int.Parse(empresakey.ToString());
        //        }
        //        if (codigo != null && !codigo.Equals(""))
        //        {
        //            obj.msj_codigo = int.Parse(codigo.ToString());
        //        }
        //        if (codigokey != null && !codigokey.Equals(""))
        //        {
        //            obj.msj_codigo_key = int.Parse(codigokey.ToString());
        //        }
        //        if (destino != null)
        //        {
        //            List<Mensajedestino> lista = new List<Mensajedestino>();

        //            foreach (object item in (object[])destino)
        //            {
        //                Mensajedestino t = new Mensajedestino();
        //                t.msd_mensaje = obj.msj_codigo;
        //                t.msd_mensaje_key = obj.msj_codigo_key;
        //                t.msd_usuario =item.ToString();
        //                t.msd_usuario_key = item.ToString();
        //                t.msd_empresa = obj.msj_empresa;
        //                t.msd_empresa_key = obj.msj_empresa;
        //                t.msd_estado = 1;
        //                t.crea_usr = "admin";
        //                t.crea_fecha = DateTime.Now;
        //                t.mod_usr = "admin";
        //                t.mod_fecha = DateTime.Now;
        //                lista.Add(t);
        //            }
        //            obj.destino = lista;
        //        }
        //        obj.msj_id = (string)id;
               
        //        obj.msj_usuarioenvia = (string)usuarioenvia;
                
        //        obj.msj_mensaje = (string)mensaje;
        //        obj.msj_estadoenvio = (string)estadoenvio;
        //        obj.msj_asunto = (string)asunto;
        //        obj.msj_fechacreacion = DateTime.Now;
        //        if (fechaenvio != null && !fechaenvio.Equals(""))
        //        {
        //            obj.msj_fechaenvio = Convert.ToDateTime(fechaenvio.ToString());
        //        }

               
                
               
        //        obj.msj_estado = (int?)activo;
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
            html.AppendLine(new Input { id = "txtASUNTO_S", placeholder = "Asunto", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtUSUARIOENVIA_S", placeholder = "Usuario envia", clase = Css.large }.ToString());           
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
            return ShowObject(new Mensaje());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Mensaje obj = new Mensaje(objeto);
            obj.msj_codigo_key = obj.msj_codigo;
            obj.msj_empresa_key = obj.msj_empresa;
           
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try{
            transaction.BeginTransaction();
            if (obj.msj_codigo_key > 0)
            {

                
                List<Mensajedestino> lst = MensajedestinoBLL.GetAll("msd_mensaje =" + obj.msj_codigo, "");
                foreach (Mensajedestino objaux in lst)
                {
                    MensajedestinoBLL.Delete(transaction,objaux);
                }
                MensajeBLL.Update(transaction, obj);
                    foreach (Mensajedestino item in obj.destino)
                    {
                        item.msd_mensaje = obj.msj_codigo;
                        item.msd_mensaje_key = obj.msj_codigo;
                        MensajedestinoBLL.Insert(transaction,item);
                    }
                   
          
               
                 
            }
            else
            {

                int codigo = MensajeBLL.InsertIdentity(transaction, obj);
                    foreach (Mensajedestino item in obj.destino)
                    {
                        item.msd_mensaje = codigo;
                        item.msd_mensaje_key = codigo;
                        MensajedestinoBLL.Insert(transaction,item);
                    } 
                   
               
            }
            transaction.Commit();

            }
            catch
            {
                transaction.Rollback();
                return "ERROR";
            }
             if (obj.msj_codigo_key > 0)
                return ShowData(obj);
            else
                return "OK";

        }

        [WebMethod]
        public static string DeleteObject(object objeto)
        {
            Mensaje obj = new Mensaje(objeto);
            obj.msj_codigo_key = obj.msj_codigo;
            obj.msj_empresa_key = obj.msj_empresa;

            List<Mensajedestino> lst = MensajedestinoBLL.GetAll("msd_mensaje =" + obj.msj_codigo, "");
            foreach (Mensajedestino objaux in lst)
            {
                MensajedestinoBLL.Delete(objaux);
            }

            if (MensajeBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}