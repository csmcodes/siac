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
    public partial class wfPolitica  : System.Web.UI.Page

    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "pol_codigo";
        protected static string WhereClause = "";
        protected static int? tclipro; 
        protected static WhereParams parametros = new WhereParams();

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


        public static string ShowData(Politica  obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.pol_id, "-", obj.pol_nombre }, descripcion = new string[] { obj.pol_porc_desc.ToString() }, logico = new LogicItem[] { new LogicItem("Activos", obj.pol_estado) } }.ToString();              
        }

        public static string ShowObject(Politica  obj)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.pol_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.pol_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.pol_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.pol_nombre, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtMONTO_INI", etiqueta = "Monto Inicial", placeholder = "Monto Inicial", valor = obj.pol_monto_ini.ToString(), clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtMONTO_FIN", etiqueta = "Monto final", placeholder = "Monto final", valor = obj.pol_monto_fin.ToString(), clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtPORC_DESC", etiqueta = "Porcentaje Descuento", placeholder = "Porcentaje Descuento", valor = obj.pol_porc_desc.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtPROMOCION", etiqueta = "Promocion", placeholder = "Promocion", valor = obj.pol_promocion.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtPORC_PAGO_CON", etiqueta = "Porcentaje pago contado", placeholder = "Porcentaje pago contado", valor = obj.pol_porc_pago_con.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtPORC_FINANACIA", etiqueta = "Porcentaje Financiacion", placeholder = "Porcentaje Financiacion", valor = obj.pol_porc_finanacia.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNRO_PAGOS", etiqueta = "Numeros pagos", placeholder = "Numeros pagos", valor = obj.pol_nro_pagos.ToString(), clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtDIAS_PLAZO", etiqueta = "Dias plazo", placeholder = "Dias plazo", valor = obj.pol_dias_plazo.ToString(), clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.pol_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Politica  obj)
        {
            int contador = 0;
            parametros = new WhereParams(); 
            List<object> valores= new List<object>();  

            if (obj.pol_codigo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " pol_codigo = {" + contador + "} ";
                valores.Add(obj.pol_codigo);  
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.pol_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " pol_nombre like {" + contador + "} ";
                valores.Add("%" + obj.pol_nombre + "%");
                contador++;                
            }
            if (!string.IsNullOrEmpty(obj.pol_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " pol_id like {" + contador + "} ";
                valores.Add("%" + obj.pol_id + "%");
                contador++;                                
            }
            
            if (obj.pol_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " pol_estado = {" + contador + "} ";
                valores.Add(obj.pol_estado.Value);
                contador++;
                
            }

            if (obj.pol_tclipro.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " pol_tclipro = {" + contador + "} ";
                valores.Add(obj.pol_tclipro.Value);
                contador++;

            }

            if (obj.pol_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " pol_empresa = {" + contador + "} ";
                valores.Add(obj.pol_empresa);
                contador++;

            }
            parametros.valores = valores.ToArray(); 



     
            
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Politica(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Politica > lst = PoliticaBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Politica  obj in lst)
            {
                string id = "{\"pol_codigo\":\"" + obj.pol_codigo + "\", \"pol_empresa\":\"" + obj.pol_empresa + "\"}";//ID COMPUESTO
           
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
            return ShowObject(new Politica ());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("pol_codigo", out codigo);
            tmp.TryGetValue("pol_empresa", out empresa);

            Politica  obj = new Politica ();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.pol_empresa = int.Parse(empresa.ToString());
                obj.pol_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.pol_codigo = int.Parse(codigo.ToString());
                obj.pol_codigo_key = int.Parse(codigo.ToString());
            }

            obj = PoliticaBLL.GetByPK(obj);
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Politica  GetObjeto(object objeto)
        //{
        //    Politica  obj = new Politica ();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object empresa= null;
        //        object empresakey = null;
        //        object codigokey= null;
        //        object id= null;
        //        object nombre= null;
        //        object monto_ini = null;
        //        object monto_fin = null;
        //        object porc_desc = null;
        //        object promocion = null;
        //        object porc_pago_con = null;
        //        object porc_finanacia = null;
        //        object nro_pagos = null;
        //        object dias_plazo = null;
        //        object pol_tclipro = null;
                
        //        object activo = null;

        //        tmp.TryGetValue("pol_codigo", out codigo);
        //        tmp.TryGetValue("pol_empresa", out empresa);
        //        tmp.TryGetValue("pol_codigo_key", out codigokey);
        //        tmp.TryGetValue("pol_empresa_key", out empresakey);
        //        tmp.TryGetValue("pol_id", out id);
        //        tmp.TryGetValue("pol_tclipro", out pol_tclipro);
        //        tmp.TryGetValue("pol_nombre", out nombre);
        //        tmp.TryGetValue("pol_monto_ini", out monto_ini);
        //        tmp.TryGetValue("pol_monto_fin", out monto_fin);
        //        tmp.TryGetValue("pol_porc_desc", out porc_desc);
        //        tmp.TryGetValue("pol_promocion", out promocion);
        //        tmp.TryGetValue("pol_porc_pago_con", out porc_pago_con);
        //        tmp.TryGetValue("pol_porc_finanacia", out porc_finanacia);
        //        tmp.TryGetValue("pol_nro_pagos", out nro_pagos);
        //        tmp.TryGetValue("pol_dias_plazo", out dias_plazo);
               


                
        //        tmp.TryGetValue("pol_estado", out activo);
        //        if (empresa != null && !empresa.Equals(""))
        //        {
        //            obj.pol_empresa = int.Parse(empresa.ToString());
        //            obj.pol_empresa_key = int.Parse(empresa.ToString());
        //        }
        //        if (codigo != null && !codigo.Equals(""))
        //        {
        //            obj.pol_codigo = int.Parse(codigo.ToString());
        //            obj.pol_codigo_key = int.Parse(codigo.ToString());
        //        }

        //        obj.pol_id = (string)id;
        //        obj.pol_nombre = (string)nombre;
        //        obj.pol_monto_ini = Convert.ToDecimal(monto_ini);
        //        obj.pol_monto_fin = Convert.ToDecimal(monto_fin);
        //        obj.pol_porc_desc = Convert.ToDecimal(porc_desc);
        //        obj.pol_promocion = Convert.ToDecimal(promocion);
        //        obj.pol_porc_pago_con = Convert.ToDecimal(porc_pago_con);
        //        obj.pol_porc_finanacia = Convert.ToDecimal(porc_finanacia);
        //        obj.pol_nro_pagos = Convert.ToInt32(nro_pagos);
        //        obj.pol_dias_plazo = Convert.ToInt32(dias_plazo);
        //        obj.pol_tclipro = Convert.ToInt32(pol_tclipro);
        //        obj.pol_estado = (int?)activo;
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
            return ShowObject(new Politica ());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Politica obj = new Politica(objeto);
            obj.pol_codigo_key = obj.pol_codigo;
            obj.pol_empresa_key = obj.pol_empresa;
            
            if (obj.pol_codigo_key > 0)
            {
                if (PoliticaBLL.Update(obj) > 0)
                {
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (PoliticaBLL.Insert(obj) > 0)
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
            Politica  obj = new Politica(objeto);
            obj.pol_codigo_key = obj.pol_codigo;
            obj.pol_empresa_key = obj.pol_empresa;
         
            if (PoliticaBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}