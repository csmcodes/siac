﻿using System;
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
    public partial class wfBusquedaObligaciones : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "com_doctran";
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
            array.Add("Fecha");
            array.Add("Numero");            
            array.Add("Proveedor");
            array.Add("Subtotal");
            array.Add("Iva");
            array.Add("Total");              

            html.AppendLine(HtmlElements.HeadRow(array));
            return html.ToString();
        }


        //public static Persona GetObjeto(object objeto)
        //{
        //    Persona obj = new Persona();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object id = null;
        //        object nombres = null;
        //        object ciruc = null;
        //        tmp.TryGetValue("per_id", out id);
        //        tmp.TryGetValue("per_nombres", out nombres);
        //        tmp.TryGetValue("per_ciruc", out ciruc);    
        //        obj.per_id = (string)id;
        //        obj.per_nombres = (string)nombres;
        //        obj.per_ciruc = (string)ciruc;
        //        obj.crea_usr = "admin";
        //        obj.crea_fecha = DateTime.Now;
        //        obj.mod_usr = "admin";
        //        obj.mod_fecha = DateTime.Now;
        //    }
        //    return obj;
        //}

        public static void SetWhereClause()
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();          
            parametros.where += ((parametros.where != "") ? " and " : "") + " com_tipodoc= {" + contador + "} ";
               valores.Add(Constantes.cObligacion.tpd_codigo );
               contador++;
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
            SetWhereClause();
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Comprobante> lista = ComprobanteBLL .GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Comprobante item in lista)
            {
                Total tot = new Total();
                tot.tot_comprobante=item.com_codigo;
                tot.tot_empresa = item.com_empresa;
                tot.tot_comprobante_key = item.com_codigo;
                tot.tot_empresa_key = item.com_empresa;
                tot = TotalBLL.GetByPK(tot);
                ArrayList array = new ArrayList();
                array.Add("");
                array.Add(item.com_fecha);
                array.Add(item.com_doctran);                
                array.Add(item.com_nombresocio);
                array.Add(tot.tot_subtot_0+tot.tot_subtotal);
                array.Add(tot.tot_timpuesto);
                array.Add(tot.tot_total);
                /*if (item.com_estado==Constantes.cEstadoMayorizado)
                    array.Add("");
                else*/
                    array.Add(new Boton { removerow = true, tooltip = "Generar Retencion" }.ToString());
                
                string strid = item.com_codigo.ToString();
                html.AppendLine(HtmlElements.TablaRowBusqueda(array, strid));
            }
            return html.ToString();
        }
    }
}