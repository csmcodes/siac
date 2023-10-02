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
    public partial class wfCuenta : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "cue_orden";
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


        public static string ShowData(Cuenta obj)
        {
            
            return "";
        }

        public static string ShowObject(Cuenta obj)
        {
            StringBuilder html = new StringBuilder();


            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.cue_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.cue_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.cue_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.cue_nombre, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Select { id = "cmbMODULO", etiqueta = "Modulo", valor = obj.cue_modulo.ToString(), clase = Css.large, diccionario = Dictionaries.GetModulos(), obligatorio = true }.ToString());
          //  html.AppendLine(new Input { id = "txtGENERO", etiqueta = "Genero", placeholder = "Genero", valor = obj.cue_genero.ToString(), clase = Css.large, obligatorio = true }.ToString());
        //    html.AppendLine(new Input { id = "", etiqueta = "Movimiento", placeholder = "Movimiento", valor = obj.cue_movimiento.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "txtGENERO", etiqueta = "Genero", valor = obj.cue_genero.ToString(), clase = Css.large, diccionario = Dictionaries.GetGenCuenta(), obligatorio = true }.ToString());



            html.AppendLine(new Check { id = "chkMOVIMIENTO", etiqueta = "Movimiento ", valor = obj.cue_movimiento }.ToString());
            html.AppendLine(new Check { id = "chkNEGRITA", etiqueta = "Visualiza ", valor = obj.cue_negrita }.ToString());
            html.AppendLine(new Check { id = "chkVISUALIZA", etiqueta = "Negrita ", valor = obj.cue_visualiza }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.cue_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Cuenta obj)
        {
            WhereClause = "";
        }

        public static string GetMenuOptions(List<Cuenta> lst, int? padre, bool addstruc)
        {

            List<Cuenta> lsthijos = lst.FindAll(delegate(Cuenta m) { return m.cue_reporta == padre; });            
            StringBuilder html = new StringBuilder();
            if (lsthijos.Count > 0)
            {
                if (addstruc)
                    html.AppendLine("<ul style=\"display: block;\">");

                foreach (Cuenta obj in lsthijos)
                {
                    bool hijos = false;
                    //List<Cuenta> lsthijos = lst.FindAll(delegate(Cuenta m) { return m.PADRE == obj.CODIGO; });
                    //WhereClause = "PADRE ="+obj.CODIGO;
                    //List<Cuenta> lsthijos = CuentaBLL.GetAll(WhereClause, OrderByClause);
                    //if (lsthijos.Count > 0)
                    //    hijos = true;
                    string htmlhijos = GetMenuOptions(lst, obj.cue_codigo, true);
                    if (htmlhijos != "")
                        hijos = true;
                    string id = "{\"cue_codigo\":\"" + obj.cue_codigo + "\", \"cue_empresa\":\"" + obj.cue_empresa + "\"}";//ID COMPUESTO

                    html.AppendLine(HtmlElements.MenuItem(id, obj.cue_nombre,"", hijos));
                    html.AppendLine(htmlhijos);

                    /*if (hijos)
                    {
                        html.AppendLine("<ul style=\"display: block;\">");
                        html.AppendLine(GetCuentaOptions(lsthijos));   
                        html.AppendLine("</ul>");
                    }*/

                }
                if (addstruc)
                    html.AppendLine("</ul>");
            }
            return html.ToString();
        }
        


        [WebMethod]
        public static string GetData(object objeto)
        {
            StringBuilder html = new StringBuilder();

            html.AppendLine("<div class=\"leftmenu\"><ul class=\"nav nav-tabs nav-stacked\">");
            html.AppendLine("<li class=\"nav-header\">Cuenta</li>");

            List<Cuenta> lst = CuentaBLL.GetAll(WhereClause, OrderByClause);
             
            //WhereClause = "PADRE IS NULL";
            //List<Cuenta> lst = CuentaBLL.GetAll(WhereClause, OrderByClause);
            html.AppendLine(GetMenuOptions(lst,null,false));  
        
            html.AppendLine("</ul></div>");           
 


           
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
            return ShowObject(new Cuenta());
        }


        [WebMethod]
        public static string GetObject(object id)
        {


            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("cue_codigo", out codigo);
            tmp.TryGetValue("cue_empresa", out empresa);

            Cuenta obj = new Cuenta();
           
            if (empresa != null && !empresa.Equals(""))
            {
                obj.cue_empresa = int.Parse(empresa.ToString());
                obj.cue_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.cue_codigo = int.Parse(codigo.ToString());
                obj.cue_codigo_key = int.Parse(codigo.ToString());
            }

           


            obj = CuentaBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
            //return ShowObject(obj);

        }

         
        [WebMethod]
        public static bool LoadHijos(object objeto)
        {
            

            Cuenta obj = new Cuenta(objeto);
            obj.cue_codigo_key = obj.cue_codigo;
            obj.cue_empresa_key = obj.cue_empresa;

            parametros = new WhereParams("cue_reporta = {0} ", obj.cue_codigo);
            if (CuentaBLL.GetRecordCount(parametros, "") > 0)
            {
                return true ;
            }
            return false;
        }

        //public static Cuenta GetObjeto(object objeto)
        //{
        //    Cuenta obj = new Cuenta();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object codigokey = null;
        //        object empresa = null;
        //        object empresakey = null;
        //        object nombre = null;
        //        object id = null;
        //        object modulo = null;
        //        object genero = null;
        //        object movimiento = null;
        //        object negrita = null;
        //        object visualiza = null;
        //        object activo = null;
        //        tmp.TryGetValue("cue_codigo", out codigo);
        //        tmp.TryGetValue("cue_codigo_key", out codigokey);
        //        tmp.TryGetValue("cue_empresa", out empresa);
        //        tmp.TryGetValue("cue_empresa_key", out empresakey);
        //        tmp.TryGetValue("cue_nombre", out nombre);


        //        tmp.TryGetValue("cue_id", out id);
        //        tmp.TryGetValue("cue_modulo", out modulo);
        //        tmp.TryGetValue("cue_genero", out genero);
        //        tmp.TryGetValue("cue_movimiento", out movimiento);
        //        tmp.TryGetValue("cue_negrita", out negrita);
        //        tmp.TryGetValue("cue_visualiza", out visualiza);

        //        tmp.TryGetValue("cue_estado", out activo);
        //        if (codigo != null)
        //        {
        //            obj.cue_codigo = int.Parse(codigo.ToString());
        //            obj.cue_codigo_key = int.Parse(codigokey.ToString());
        //        }


        //        if (empresa != null)
        //        {
        //            obj.cue_empresa = int.Parse(empresa.ToString());
        //            obj.cue_empresa_key = int.Parse(empresakey.ToString());
        //        }

        //        obj.cue_nombre = (string)nombre;
        //        obj.cue_id = (string)id;
        //        obj.cue_modulo = Convert.ToInt32(modulo);
        //        obj.cue_genero = Convert.ToInt32(genero);
               
        //            if (movimiento != null && !movimiento.Equals(""))

        //        obj.cue_movimiento = Convert.ToInt32(movimiento);
        //        obj.cue_negrita = (int?)negrita;
        //        obj.cue_visualiza = (int?)visualiza;
        //        obj.cue_estado = (int?)activo;
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
            html.AppendLine("<div class=\"pull-left\">");

           
            html.AppendLine(new Input { id = "txtNOMBRE_S", placeholder = "Nombre", clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbESTADO_S", clase = Css.medium, diccionario = Dictionaries.GetEstado() }.ToString());

            html.AppendLine("</div>");
            html.AppendLine("<div class=\"pull-right\">");
            html.AppendLine(new Boton { refresh=true }.ToString());
            html.AppendLine(new Boton { clean = true }.ToString());
            html.AppendLine("</div>");
            return html.ToString();
        }

        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Cuenta());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {

            Cuenta obj = new Cuenta(objeto);
            obj.cue_codigo_key = obj.cue_codigo;
            obj.cue_empresa_key = obj.cue_empresa;

            Cuenta opt = obj;
            obj = CuentaBLL.GetByPK(obj);
            opt.cue_orden = obj.cue_orden;
            opt.cue_reporta = obj.cue_reporta;


            if (opt.cue_movimiento > 0)
            {

                parametros = new WhereParams("cue_reporta = {0} ", opt.cue_codigo);
                if (CuentaBLL.GetRecordCount(parametros, "") > 0)
                {
                    opt.cue_movimiento = 0;
                }
            }
          


               if (CuentaBLL.Update(opt) > 0)
                {
                    //return ShowData(obj);
                    return "OK";
                }
                else
                    return "ERROR";            

        }
                [WebMethod]
        public static string DeleteObject(object objeto)
        {
            Cuenta obj = new Cuenta(objeto);
            obj.cue_codigo_key = obj.cue_codigo;
            obj.cue_empresa_key = obj.cue_empresa;

            if (CuentaBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }

     


        [WebMethod]
        public static string AddOption()
        {

            int max = CuentaBLL.GetMax("cue_codigo") + 1;
            

            Cuenta obj = new Cuenta();
            
            obj.cue_empresa = 1;
            obj.cue_modulo = 1;
            obj.cue_genero = 1;
            obj.cue_nombre = "Nueva Opción";
            obj.cue_orden= max;
            obj.cue_estado = 1;
            if (CuentaBLL.Insert(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";            

        }

        [WebMethod]
        public static string AddChildOption(object id)
        {            
            

            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("cue_codigo", out codigo);
            tmp.TryGetValue("cue_empresa", out empresa);

            Cuenta parentobj = new Cuenta();

            if (empresa != null && !empresa.Equals(""))
            {
                parentobj.cue_empresa = int.Parse(empresa.ToString());
                parentobj.cue_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                parentobj.cue_codigo = int.Parse(codigo.ToString());
                parentobj.cue_codigo_key = int.Parse(codigo.ToString());
            }      
            parentobj = CuentaBLL.GetByPK(parentobj);
           











            int max = CuentaBLL.GetMax("cue_codigo") + 1;


            Cuenta obj = new Cuenta();
            
            obj.cue_empresa = parentobj.cue_empresa;
            obj.cue_modulo = parentobj.cue_modulo;
            obj.cue_genero = parentobj.cue_genero;
            obj.cue_nombre = "Nueva Opción";
            obj.cue_orden= max;
            obj.cue_reporta = int.Parse(codigo.ToString());  
            obj.cue_estado = 1;
            obj.cue_nivel = parentobj.cue_nivel + 1;
            if (CuentaBLL.Insert(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";

        }
    }
}