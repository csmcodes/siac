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
    public partial class wfProducto : System.Web.UI.Page

    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "pro_codigo";
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


        public static string ShowData(Producto obj)
        {
            
            return new HtmlObjects.ListItem { titulo = new string[] { obj.pro_id, "-", obj.pro_nombre }, descripcion = new string[] { }, logico = new LogicItem[] { new LogicItem("Activos", obj.pro_estado) } }.ToString();
            
            
        }

        public static string ShowObject(Producto obj)
        {



            int numero = ProductoBLL.GetMax("pro_codigo");
            //String Nproducto = "00000000" + (numero+1);


            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.pro_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.pro_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.pro_codigo, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.pro_nombre, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Select { id = "cmbTPRODUCTO", etiqueta = "Tipo Producto", valor = obj.pro_tproducto.ToString(), clase = Css.large, diccionario = Dictionaries.GetTproducto() }.ToString());
            html.AppendLine(new Select { id = "cmbMEDIDA", etiqueta = "Medida", valor = obj.pro_unidad.ToString(), clase = Css.large, diccionario = Dictionaries.GetUmedida() }.ToString());
            html.AppendLine(new Select { id = "cmbGRUPO", etiqueta = "Grupo", valor = obj.pro_grupo.ToString(), clase = Css.large, diccionario = Dictionaries.GetGproducto() }.ToString());
            //html.AppendLine(new Input { id = "txtINVENTARIO", etiqueta = "Inventario", placeholder = "Inventario", valor = obj.pro_inventario.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Check { id = "txtIVA", etiqueta = "IVA ", valor = obj.pro_iva }.ToString());
            html.AppendLine(new Check { id = "txtCALCULA", etiqueta = "Calculo Precio ", valor = obj.pro_calcula}.ToString());
            html.AppendLine(new Check { id = "txtTOTAL", etiqueta = "Calculo TOTAL", valor = obj.pro_total}.ToString());
            html.AppendLine(new Check { id = "txtINVENTARIO", etiqueta = "Inventario ", valor = obj.pro_inventario }.ToString());                
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.pro_estado }.ToString());
            html.AppendLine(new Boton { click = "SaveObj();return false;", valor = "Guardar" }.ToString());
            html.AppendLine(new Input { id = "txtNproducto", placeholder = "Nombre", valor = numero, clase = Css.large, visible = false }.ToString());          
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


        public static void SetWhereClause(Producto obj)
        {
            int contador = 0;
            parametros = new WhereParams(); 
            List<object> valores= new List<object>();  
            if (obj.pro_codigo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " pro_codigo = {" + contador + "} ";
                valores.Add(obj.pro_codigo);  
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.pro_nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " pro_nombre like {" + contador + "} ";
                valores.Add("%" + obj.pro_nombre + "%");
                contador++;                
            }
            if (!string.IsNullOrEmpty(obj.pro_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " pro_id like {" + contador + "} ";
                valores.Add("%" + obj.pro_id + "%");
                contador++;                                
            }
            
            if (obj.pro_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " pro_estado = {" + contador + "} ";
                valores.Add(obj.pro_estado.Value);
                contador++;                
            }
            if (obj.pro_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " pro_empresa = {" + contador + "} ";
                valores.Add(obj.pro_empresa);
                contador++;

            }
            parametros.valores = valores.ToArray(); 
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(new Producto(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();

            List<Producto> lst = ProductoBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            int numero = ProductoBLL.GetMax("pro_codigo");
            html.AppendLine(new Input { id = "txtNproducto", placeholder = "Nombre", valor = numero, clase = Css.large, visible = false }.ToString());          
            foreach (Producto obj in lst)
            {
                string id = "{\"pro_codigo\":\"" + obj.pro_codigo + "\", \"pro_empresa\":\"" + obj.pro_empresa + "\"}";//ID COMPUESTO   
               
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
            return ShowObject(new Producto());
        }


        [WebMethod]
        public static string GetObject(object id)
        {            
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("pro_codigo", out codigo);
            tmp.TryGetValue("pro_empresa", out empresa);
            Producto obj = new Producto();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.pro_empresa = int.Parse(empresa.ToString());
                obj.pro_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.pro_codigo = int.Parse(codigo.ToString());
                obj.pro_codigo_key = int.Parse(codigo.ToString());
            }
            obj = ProductoBLL.GetByPK(obj);
            
            return new JavaScriptSerializer().Serialize(obj);
        }


        //public static Producto GetObjeto(object objeto)
        //{
        //    Producto obj = new Producto();
        //    if (objeto != null)
        //    {
        //        Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
        //        object codigo = null;
        //        object empresa = null;
        //        object empresakey = null;
        //        object codigokey = null;
        //        object id = null;
        //        object iva = null;
        //        object nombre = null;
        //        object tproducto = null;
        //        object inventario = null;
        //        object activo = null;
        //        object calcula = null;
        //        object unidad = null;
        //        object factores = null;
        //        object grupo = null;
        //        tmp.TryGetValue("pro_codigo", out codigo);
        //        tmp.TryGetValue("pro_empresa", out empresa);
        //        tmp.TryGetValue("pro_codigo_key", out codigokey);
        //        tmp.TryGetValue("pro_empresa_key", out empresakey);
        //        tmp.TryGetValue("pro_id", out id);
        //        tmp.TryGetValue("pro_unidad", out unidad);
        //        tmp.TryGetValue("pro_iva", out iva);
        //        tmp.TryGetValue("pro_calcula", out calcula);
        //        tmp.TryGetValue("pro_nombre", out nombre);
        //        tmp.TryGetValue("pro_grupo", out grupo);
        //        tmp.TryGetValue("pro_tproducto", out tproducto);
        //        tmp.TryGetValue("pro_inventario", out inventario);
        //        tmp.TryGetValue("pro_estado", out activo);
        //        tmp.TryGetValue("factores", out factores);
        //        if (empresa != null && !empresa.Equals(""))
        //        {
        //            obj.pro_empresa = int.Parse(empresa.ToString());
        //            obj.pro_empresa_key = int.Parse(empresa.ToString());
        //        }
        //        if (codigo != null && !codigo.Equals(""))
        //        {
        //            obj.pro_codigo = int.Parse(codigo.ToString());
        //            obj.pro_codigo_key = int.Parse(codigo.ToString());
        //        }
        //        if (unidad != null && !unidad.Equals(""))
        //        {
        //            obj.pro_unidad = int.Parse(unidad.ToString());
        //        }

        //        if (empresa != null && !empresa.Equals(""))
        //        {
        //            List<Factor> lista = new List<Factor>();




        //            Factor t = new Factor();
        //            t.fac_empresa = Convert.ToInt32(empresa);
        //            t.fac_empresa_key = Convert.ToInt32(empresa);

        //            t.fac_unidad = Convert.ToInt32(unidad);
        //            t.fac_unidad_key = Convert.ToInt32(unidad);
        //            t.fac_factor = 1;
        //            t.fac_estado = 1;
        //            t.fac_default = 1;
        //            t.crea_usr = "admin";
        //            t.crea_fecha = DateTime.Now;
        //            t.mod_usr = "admin";
        //            t.mod_fecha = DateTime.Now;
        //            lista.Add(t);

        //            obj.factores = lista;

        //        }
        //        obj.pro_id = (string)id;
        //        obj.pro_nombre = (string)nombre;
        //        obj.pro_inventario = Convert.ToInt32(inventario);
        //        obj.pro_tproducto = Convert.ToInt32(tproducto);
        //        obj.pro_iva = (int?)iva;
        //        obj.pro_grupo = (int?)grupo;
        //        obj.pro_calcula = (int?)calcula;
        //        obj.pro_estado = (int?)activo;
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
            html.AppendLine(new Input { id = "txtCODIGO_S", placeholder = "Codigo", clase = Css.large }.ToString());
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
            return ShowObject(new Producto());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
           
            Producto obj = new Producto(objeto);
            obj.pro_codigo_key = obj.pro_codigo;
            obj.pro_empresa_key = obj.pro_empresa;

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try{
            transaction.BeginTransaction();
            if (obj.pro_codigo_key > 0)
            {
                List<Factor> lst = FactorBLL.GetAll(new WhereParams("fac_producto ={0} and fac_unidad={1} ", obj.pro_codigo, obj.pro_unidad), "");
                foreach (Factor objaux in lst)
                {
                    FactorBLL.Delete(objaux);
                }
                ProductoBLL.Update(transaction, obj);

                obj.factores.ElementAt(0).fac_producto = obj.pro_codigo;
                obj.factores.ElementAt(0).fac_producto_key = obj.pro_codigo;
                FactorBLL.Insert(transaction, obj.factores.ElementAt(0));
            }
            else
            {
                int codigo = ProductoBLL.InsertIdentity(transaction, obj);
                obj.factores.ElementAt(0).fac_producto = codigo;
                obj.factores.ElementAt(0).fac_producto_key = codigo;
                FactorBLL.Insert(transaction, obj.factores.ElementAt(0));
            }
            transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return "ERROR";
            }
            if (obj.pro_codigo_key > 0)
                return ShowData(obj);

            else
               
                return "OK";

        }

        [WebMethod]
        public static string DeleteObject(object objeto)
        {    
            Producto obj = new Producto(objeto);
            obj.pro_codigo_key = obj.pro_codigo;
            obj.pro_empresa_key =obj.pro_empresa;




            List<Factor> lst = FactorBLL.GetAll("fac_producto =" + obj.pro_codigo, "");
            foreach (Factor objaux in lst)
            {
                FactorBLL.Delete(objaux);
            }

            if (ProductoBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }




    }
}