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
    public partial class wfGproducto : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "gpr_nombre";
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


        public static string ShowData(Gproducto obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.gpr_nombre }, descripcion = new string[] {  }, logico = new LogicItem[] { new LogicItem("Activos", obj.gpr_estado) } }.ToString();
        }

        public static string ShowObject(Gproducto obj)
        {   Cuenta cuenta = new Cuenta();
            cuenta.cue_empresa = obj.gpr_empresa;
            cuenta.cue_empresa_key = obj.gpr_empresa;
            if (obj.gpr_cta_costo.HasValue)
            {
                cuenta.cue_codigo = obj.gpr_cta_costo.Value;
                cuenta.cue_codigo_key = obj.gpr_cta_costo.Value;
                cuenta = CuentaBLL.GetByPK(cuenta);
            }

            Cuenta cuenta2 = new Cuenta();
            cuenta2.cue_empresa = obj.gpr_empresa;
            cuenta2.cue_empresa_key = obj.gpr_empresa;
            if (obj.gpr_cta_inv.HasValue)
            {
                cuenta2.cue_codigo = obj.gpr_cta_inv.Value;
                cuenta2.cue_codigo_key = obj.gpr_cta_inv.Value;
                cuenta2 = CuentaBLL.GetByPK(cuenta2);
            }





            Cuenta cuenta3 = new Cuenta();
            cuenta3.cue_empresa = obj.gpr_empresa;
            cuenta3.cue_empresa_key = obj.gpr_empresa;
            if (obj.gpr_cta_des.HasValue)
            {
                cuenta3.cue_codigo = obj.gpr_cta_des.Value;
                cuenta3.cue_codigo_key = obj.gpr_cta_des.Value;
                cuenta3 = CuentaBLL.GetByPK(cuenta3);
            }

            Cuenta cuenta4 = new Cuenta();
            cuenta4.cue_empresa = obj.gpr_empresa;
            cuenta4.cue_empresa_key = obj.gpr_empresa;
            if (obj.gpr_cta_dev.HasValue)
            {
                cuenta4.cue_codigo = obj.gpr_cta_dev.Value;
                cuenta4.cue_codigo_key = obj.gpr_cta_dev.Value;
                cuenta4 = CuentaBLL.GetByPK(cuenta4);
            }
            Cuenta cuenta5 = new Cuenta();
            cuenta5.cue_empresa = obj.gpr_empresa;
            cuenta5.cue_empresa_key = obj.gpr_empresa;
            if (obj.gpr_cta_venta.HasValue)
            {
                cuenta5.cue_codigo = obj.gpr_cta_venta.Value;
                cuenta5.cue_codigo_key = obj.gpr_cta_venta.Value;
                cuenta5 = CuentaBLL.GetByPK(cuenta5);
            }

          




            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.gpr_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.gpr_codigo_key.ToString(), visible = false }.ToString());            
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.gpr_nombre, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.gpr_id, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtCUENTACOS", etiqueta = "Cuenta Costo", valor = cuenta.cue_nombre, autocomplete = "GetCuentaObj", obligatorio = false, clase = Css.medium, placeholder = "Cuenta Costo" }.ToString() + " " + new Input { id = "txtCODCUENTACOS", visible = false, valor = obj.gpr_cta_costo }.ToString());
            html.AppendLine(new Input { id = "txtCUENTAINV", etiqueta = "Cuenta Inventario", valor = cuenta2.cue_nombre, autocomplete = "GetCuentaObj", obligatorio = false, clase = Css.medium, placeholder = "Cuenta Inventario" }.ToString() + " " + new Input { id = "txtCODCUENTAINV", visible = false, valor = obj.gpr_cta_inv }.ToString());
            html.AppendLine(new Input { id = "txtCUENTADES", etiqueta = "Cuenta Descuento", valor = cuenta3.cue_nombre, autocomplete = "GetCuentaObj", obligatorio = false, clase = Css.medium, placeholder = "Cuenta Descuento" }.ToString() + " " + new Input { id = "txtCODCUENTADES", visible = false, valor = obj.gpr_cta_des }.ToString());
            html.AppendLine(new Input { id = "txtCUENTADEV", etiqueta = "Cuenta Devolucion", valor = cuenta4.cue_nombre, autocomplete = "GetCuentaObj", obligatorio = false, clase = Css.medium, placeholder = "Cuenta Devolucion" }.ToString() + " " + new Input { id = "txtCODCUENTADEV", visible = false, valor = obj.gpr_cta_dev }.ToString());
            html.AppendLine(new Input { id = "txtCUENTAVENTA", etiqueta = "Cuenta Venta", valor = cuenta5.cue_nombre, autocomplete = "GetCuentaObj", obligatorio = false, clase = Css.medium, placeholder = "Cuenta Venta" }.ToString() + " " + new Input { id = "txtCODCUENTAVENTA", visible = false, valor = obj.gpr_cta_venta }.ToString());
            
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.gpr_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Gproducto obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (!string.IsNullOrEmpty(obj.gpr_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " gpr_nombre like {" + contador + "} ";
                valores.Add("%" + obj.gpr_nombre + "%");
                contador++;
            }

            if (obj.gpr_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " gpr_estado = {" + contador + "} ";
                valores.Add(obj.gpr_estado.Value);
                contador++;
            }
            if (obj.gpr_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " gpr_empresa = {" + contador + "} ";
                valores.Add(obj.gpr_empresa);
                contador++;
            }
            parametros.valores = valores.ToArray();
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Gproducto(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Gproducto> lst = GproductoBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Gproducto obj in lst)
            {
                string id = "{\"gpr_codigo\":\"" + obj.gpr_codigo + "\", \"gpr_empresa\":\"" + obj.gpr_empresa + "\"}";//ID COMPUESTO
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
            return ShowObject(new Gproducto());
        }

        [WebMethod]
        public static string GetObject(object id)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("gpr_codigo", out codigo);
            tmp.TryGetValue("gpr_empresa", out empresa);
            Gproducto obj = new Gproducto();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.gpr_empresa = int.Parse(empresa.ToString());
                obj.gpr_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.gpr_codigo = int.Parse(codigo.ToString());
                obj.gpr_codigo_key = int.Parse(codigo.ToString());
            }
            obj = GproductoBLL.GetByPK(obj);
            Cuenta cuenta = new Cuenta();
            cuenta.cue_empresa = obj.gpr_empresa;
            cuenta.cue_empresa_key = obj.gpr_empresa;
            if (obj.gpr_cta_costo.HasValue)
            {
                cuenta.cue_codigo = obj.gpr_cta_costo.Value;
                cuenta.cue_codigo_key = obj.gpr_cta_costo.Value;
                cuenta = CuentaBLL.GetByPK(cuenta);
            }
            obj.gpr_nombrecta_costo = cuenta.cue_nombre;
            Cuenta cuenta2 = new Cuenta();
            cuenta2.cue_empresa = obj.gpr_empresa;
            cuenta2.cue_empresa_key = obj.gpr_empresa;
            if (obj.gpr_cta_inv.HasValue)
            {
                cuenta2.cue_codigo = obj.gpr_cta_inv.Value;
                cuenta2.cue_codigo_key = obj.gpr_cta_inv.Value;
                cuenta2 = CuentaBLL.GetByPK(cuenta2);
            }
            obj.gpr_nombrecta_inv = cuenta2.cue_nombre;

            Cuenta cuenta3 = new Cuenta();
            cuenta3.cue_empresa = obj.gpr_empresa;
            cuenta3.cue_empresa_key = obj.gpr_empresa;
            if (obj.gpr_cta_des.HasValue)
            {
                cuenta3.cue_codigo = obj.gpr_cta_des.Value;
                cuenta3.cue_codigo_key = obj.gpr_cta_des.Value;
                cuenta3 = CuentaBLL.GetByPK(cuenta3);
            }
            obj.gpr_nombrecta_des = cuenta3.cue_nombre;
            Cuenta cuenta4 = new Cuenta();
            cuenta4.cue_empresa = obj.gpr_empresa;
            cuenta4.cue_empresa_key = obj.gpr_empresa;
            if (obj.gpr_cta_dev.HasValue)
            {
                cuenta4.cue_codigo = obj.gpr_cta_dev.Value;
                cuenta4.cue_codigo_key = obj.gpr_cta_dev.Value;
                cuenta4 = CuentaBLL.GetByPK(cuenta4);
            }
            obj.gpr_nombrecta_dev = cuenta4.cue_nombre;
            Cuenta cuenta5 = new Cuenta();
            cuenta5.cue_empresa = obj.gpr_empresa;
            cuenta5.cue_empresa_key = obj.gpr_empresa;
            if (obj.gpr_cta_venta.HasValue)
            {
                cuenta5.cue_codigo = obj.gpr_cta_venta.Value;
                cuenta5.cue_codigo_key = obj.gpr_cta_venta.Value;
                cuenta5 = CuentaBLL.GetByPK(cuenta5);
            }
            obj.gpr_nombrecta_venta = cuenta5.cue_nombre;


            return new JavaScriptSerializer().Serialize(obj);
        }


        public static Gproducto GetObjeto(object objeto)
        {
            Gproducto obj = new Gproducto();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object codigo = null;
                object codigokey = null;
                object empresa = null;
                object empresakey = null;
                
                object id = null;
                object nombre = null;
             
                object gpr_cta_costo = null;
                object gpr_cta_inv = null;
                object gpr_cta_des = null;
                object gpr_cta_dev = null;
                object gpr_cta_venta = null;
                object activo = null;
               
                tmp.TryGetValue("gpr_codigo", out codigo);
                tmp.TryGetValue("gpr_codigo_key", out codigokey);
                tmp.TryGetValue("gpr_empresa", out empresa);
                tmp.TryGetValue("gpr_empresa_key", out empresakey);
                tmp.TryGetValue("gpr_id", out id);

                tmp.TryGetValue("gpr_cta_costo", out gpr_cta_costo);
                tmp.TryGetValue("gpr_cta_inv", out gpr_cta_inv);
                tmp.TryGetValue("gpr_cta_des", out gpr_cta_des);
                tmp.TryGetValue("gpr_cta_dev", out gpr_cta_dev);
                tmp.TryGetValue("gpr_cta_venta", out gpr_cta_venta);

                tmp.TryGetValue("gpr_nombre", out nombre);
              
                tmp.TryGetValue("gpr_estado", out activo);

                if (codigo != null && !codigo.Equals(""))
                {
                    obj.gpr_codigo = int.Parse(codigo.ToString());
                }
                if (codigokey != null && !codigokey.Equals(""))
                {
                    obj.gpr_codigo_key = int.Parse(codigo.ToString());
                }
                if (empresa != null && !empresa.Equals(""))
                {
                    obj.gpr_empresa = int.Parse(empresa.ToString());
                }
                if (empresakey != null && !empresakey.Equals(""))
                {
                    obj.gpr_empresa_key = int.Parse(empresakey.ToString());
                }       
                obj.gpr_id = (string)id;           
                obj.gpr_nombre = (string)nombre;
                if (gpr_cta_costo != null && !gpr_cta_costo.Equals(""))
                {
                    obj.gpr_cta_costo = Convert.ToInt32(gpr_cta_costo);
                }
                if (gpr_cta_inv != null && !gpr_cta_inv.Equals(""))
                {
                    obj.gpr_cta_inv = Convert.ToInt32(gpr_cta_inv);
                }

                if (gpr_cta_des != null && !gpr_cta_des.Equals(""))
                {
                    obj.gpr_cta_des = Convert.ToInt32(gpr_cta_des);
                }
                if (gpr_cta_dev != null && !gpr_cta_dev.Equals(""))
                {
                    obj.gpr_cta_dev = Convert.ToInt32(gpr_cta_dev);
                }
                if (gpr_cta_venta != null && !gpr_cta_venta.Equals(""))
                {
                    obj.gpr_cta_venta = Convert.ToInt32(gpr_cta_venta);
                }
               





                obj.gpr_estado = (int?)activo;
                obj.crea_usr = "admin";
                obj.crea_fecha = DateTime.Now;
                obj.mod_usr = "admin";
                obj.mod_fecha = DateTime.Now;
            }
            return obj;
        }


        [WebMethod]
        public static string GetSearch()
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class='pull-left'>");
            html.AppendLine(new Input { id = "txtNOMBRE_S", placeholder = "Nombre", clase = Css.large }.ToString());
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
            return ShowObject(new Gproducto());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Gproducto obj = new Gproducto(objeto);
            obj.gpr_codigo_key = obj.gpr_codigo;
            obj.gpr_empresa_key = obj.gpr_empresa;
      
            if (obj.gpr_codigo_key > 0)
            {
                if (GproductoBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (GproductoBLL.Insert(obj) > 0)
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
            Gproducto obj =  new Gproducto(objeto);
            obj.gpr_codigo_key = obj.gpr_codigo;
            obj.gpr_empresa_key = obj.gpr_empresa;


            if (GproductoBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}