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

    public partial class wfAutpersona : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "ape_nro_autoriza";
        protected static string WhereClause = "";
        protected static WhereParams parametros = new WhereParams();
        protected static int? tclipro; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
                txttclipro.Text = (Request.QueryString["tclipro"] != null) ? Request.QueryString["tclipro"].ToString() : Constantes.cCliente + "";
                tclipro = Convert.ToInt32(txttclipro.Text);
            }
        }


        public static string ShowData(Autpersona obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] {obj.ape_nombrepersona+" " + obj.ape_fac1, "-", obj.ape_fac2,"-", obj.ape_fac3.ToString(),"/", obj.ape_fact1, "-", obj.ape_fact2,"-", obj.ape_fact3.ToString()}, descripcion = new string[] {"Aut. SRI:", obj.ape_nro_autoriza," / Valido hasta",obj.ape_val_fecha+"" }, logico = new LogicItem[] { new LogicItem("Activos", obj.ape_estado) } }.ToString();
        }

        public static string ShowObject(Autpersona obj)
        {
            Persona persona = new Persona();
            persona.per_empresa = obj.ape_empresa;
            persona.per_empresa_key = obj.ape_empresa;
            if (obj.ape_persona.HasValue)
            {
                persona.per_codigo = obj.ape_persona.Value;
                persona.per_codigo_key = obj.ape_persona.Value;
                persona = PersonaBLL.GetByPK(persona);
            }
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtNRO_AUTORIZA", etiqueta = "Nro Autorizacion", placeholder = "Nro Autorizacion", valor = obj.ape_nro_autoriza, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNRO_AUTORIZA_key", valor = obj.ape_nro_autoriza_key, visible = false }.ToString());
     //       html.AppendLine(new Input { id = "txtFAC1", etiqueta = "Almacen", placeholder = "Almacen", valor = obj.ape_fac1, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtFAC1_key", valor = obj.ape_fac1_key, visible = false }.ToString());
      //      html.AppendLine(new Input { id = "txtFAC2", etiqueta = "Punto de Vente", placeholder = "Punto de Vente", valor = obj.ape_fac2, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtFAC2_key", valor = obj.ape_fac2_key, visible = false }.ToString());
            html.AppendLine(new Input { id = "cmbFECHA", etiqueta = "Valido hasta", datepicker = true, datetimevalor = (obj.ape_val_fecha.HasValue) ? obj.ape_val_fecha.Value : DateTime.Now, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtFAC1", etiqueta = "Almacen", valor= obj.ape_fac1, clase = Css.mini}.ToString());
            html.AppendLine(new Input { id = "txtFAC2", etiqueta = "Punto Venta", valor=obj.ape_fac2, clase = Css.mini}.ToString());
            html.AppendLine(new Input { id = "txtFAC3", etiqueta = "Desde", placeholder = "Desde", valor = obj.ape_fac3, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtFACT3", etiqueta = "Hasta", placeholder = "Hasta", valor = obj.ape_fact3, clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbRETDATO", etiqueta = "Retdato", valor = obj.ape_retdato.ToString(), clase = Css.large, diccionario = Dictionaries.GetRetdato() }.ToString());
            html.AppendLine(new Input { id = "txtRETDATO_key", valor = obj.ape_retdato_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Select { id = "cmbTABLACOA", etiqueta = "Tablacoa", valor = obj.ape_tablacoa.ToString(), clase = Css.large, diccionario = Dictionaries.GetTablacoa() }.ToString());
        //    html.AppendLine(new Select { id = "cmbPERSONA", etiqueta = "Persona", valor = obj.ape_persona.ToString(), clase = Css.large, diccionario = Dictionaries.GetPersonas() }.ToString());
            if (tclipro == Constantes.cProveedor)
            {
                html.AppendLine(new Input { id = "txtCODCLIPRO", etiqueta = "Proveedor", valor = persona.per_nombres + " " + persona.per_apellidos, autocomplete = "GetProveedorObj", clase = Css.medium, placeholder = "Proveedor" }.ToString() + " " + new Input { id = "cmbPERSONA", visible = false, valor = obj.ape_persona }.ToString());            
            }
            else
            {
                html.AppendLine(new Input { id = "txtCODCLIPRO", etiqueta = "Cliente", valor = persona.per_nombres + " " + persona.per_apellidos, autocomplete = "GetClienteObj", clase = Css.medium, placeholder = "Cliente" }.ToString() + " " + new Input { id = "cmbPERSONA", visible = false, valor = obj.ape_persona }.ToString());               
            }            
         //   html.AppendLine(new Select { id = "cmbTCLIPRO", etiqueta = "tclipro", valor = obj.ape_tclipro.ToString(), clase = Css.large, diccionario = Dictionaries.Empty() }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.ape_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Autpersona obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (!string.IsNullOrEmpty(obj.ape_fac1))            
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " ape_fac1 like {" + contador + "} ";
                valores.Add("%" + obj.ape_fac1 + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.ape_fac2))            
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " ape_fac2 like {" + contador + "} ";
                valores.Add("%" + obj.ape_fac2 + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.ape_nro_autoriza))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " ape_nro_autoriza like {" + contador + "} ";
                valores.Add("%" + obj.ape_nro_autoriza + "%");
                contador++;
            }
            if (obj.ape_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " ape_estado = {" + contador + "} ";
                valores.Add(obj.ape_estado.Value);
                contador++;
            }
            if (obj.ape_tclipro > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " ape_tclipro = {" + contador + "} ";
                valores.Add(obj.ape_tclipro);
                contador++;

            }
            if (obj.ape_empresa>0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " ape_empresa = {" + contador + "} ";
                valores.Add(obj.ape_empresa);
                contador++;
            }
            parametros.valores = valores.ToArray();
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Autpersona(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Autpersona> lst = AutpersonaBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Autpersona obj in lst)
            {
                string id = "{\"ape_empresa\":\"" + obj.ape_empresa + "\", \"ape_nro_autoriza\":\"" + obj.ape_nro_autoriza + "\", \"ape_fac1\":\"" + obj.ape_fac1 + "\", \"ape_fac2\":\"" + obj.ape_fac2 + "\", \"ape_retdato\":\"" + obj.ape_retdato + "\"}";//ID COMPUESTO              
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
            return ShowObject(new Autpersona());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object ape_nro_autoriza = null;
            object ape_fac1 = null;
            object ape_fac2 = null;
            object ape_retdato = null;
            object ape_empresa = null;
            tmp.TryGetValue("ape_nro_autoriza", out ape_nro_autoriza);
            tmp.TryGetValue("ape_fac1", out ape_fac1);
            tmp.TryGetValue("ape_fac2", out ape_fac2);
            tmp.TryGetValue("ape_retdato", out ape_retdato);
            tmp.TryGetValue("ape_empresa", out ape_empresa);
            Autpersona obj = new Autpersona();
            if (ape_nro_autoriza != null && !ape_nro_autoriza.Equals(""))
            {
                obj.ape_nro_autoriza = (string)ape_nro_autoriza;
                obj.ape_nro_autoriza_key = (string)ape_nro_autoriza;
            }
            if (ape_fac1 != null && !ape_fac1.Equals(""))
            {
                obj.ape_fac1 = ape_fac1.ToString();
                obj.ape_fac1_key = ape_fac1.ToString();
            }
            if (ape_fac2 != null && !ape_fac2.Equals(""))
            {

                obj.ape_fac2 = ape_fac2.ToString();
                obj.ape_fac2_key = ape_fac2.ToString();
            }

            if (ape_retdato != null && !ape_retdato.Equals(""))
            {
                obj.ape_retdato = int.Parse(ape_retdato.ToString());
                obj.ape_retdato_key = int.Parse(ape_retdato.ToString());
            }

            if (ape_empresa != null && !ape_empresa.Equals(""))
            {
                obj.ape_empresa = int.Parse(ape_empresa.ToString());
                obj.ape_empresa_key = int.Parse(ape_empresa.ToString());
            }
            obj = AutpersonaBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }

        /*
        public static Autpersona GetObjeto(object objeto)
        {
            Autpersona obj = new Autpersona();
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

                tmp.TryGetValue("ape_secuencia", out secuencia);
                tmp.TryGetValue("ape_modulo", out modulo);
                tmp.TryGetValue("ape_secuencia_key", out secuenciakey);
                tmp.TryGetValue("ape_modulo_key", out modulokey);
                tmp.TryGetValue("ape_id", out id);
                tmp.TryGetValue("ape_nombre", out nombre);
                tmp.TryGetValue("ape_nombremodulo", out nombremodulo);
                tmp.TryGetValue("ape_estado", out activo);
                if (modulo != null && !modulo.Equals(""))
                {
                    obj.ape_modulo = int.Parse(modulo.ToString());
                }
                if (secuencia != null && !secuencia.Equals(""))
                {
                    obj.ape_secuencia = int.Parse(secuencia.ToString());
                }
                if (modulokey != null && !modulokey.Equals(""))
                {

                    obj.ape_modulo_key = int.Parse(modulokey.ToString());
                }
                if (secuenciakey != null && !secuenciakey.Equals(""))
                {

                    obj.ape_secuencia_key = int.Parse(secuenciakey.ToString());
                }

                obj.ape_id = (string)id;
                obj.ape_nombre = (string)nombre;

                obj.ape_estado = (int?)activo;
                obj.crea_usr = "admin";
                obj.crea_fecha = DateTime.Now;
                obj.mod_usr = "admin";
                obj.mod_fecha = DateTime.Now;


                obj.ape_nombremodulo = (string)nombremodulo;
            }

            return obj;
        }*/


        [WebMethod]
        public static string GetSearch()
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class='pull-left'>");
            //     html.AppendLine(HtmlElements.Input("Id", "txtSECUENCIA_S", "", HtmlElements.small, false));
            html.AppendLine(new Input { id = "txtNRO_AUTORIZA_S", placeholder = "Nro Autorizacion", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtFAC1_S", placeholder = "fac1", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtFAC2_S", placeholder = "fac2", clase = Css.large }.ToString());
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
            return ShowObject(new Autpersona());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Autpersona obj = new Autpersona(objeto);

            if (!string.IsNullOrEmpty(obj.ape_nro_autoriza_key))
            {
                if (AutpersonaBLL.Update(obj) > 0)
                {
                    obj=AutpersonaBLL.GetByPK(obj);
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (AutpersonaBLL.Insert(obj) > 0)
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
            Autpersona obj = new Autpersona(objeto);
            if (AutpersonaBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}