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
using Functions;
using Packages;

namespace WebUI
{
    public partial class wfAutorizacion : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "aut_cco_comproba,aut_secuencia";
        protected static string WhereClause = "";
        protected static WhereParams parametros;
        protected static long codigo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
                txtcodigocomp.Text = (Request.QueryString["codigocomp"] != null) ? Request.QueryString["codigocomp"].ToString() : "-1";
                codigo = (Int64)Conversiones.GetValueByType(txtcodigocomp.Text, typeof(Int64));
            }
        }
        
        [WebMethod]
        public static string GetCabecera()
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Usuario Aut:";
            tdatos.rows[0].cells[1].valor = new Select { id = "txtAUTORIZA_S", diccionario = Dictionaries.GetUsuario(), withempty = true }.ToString();
            tdatos.rows[1].cells[0].valor = "Autorizado Para:";
            tdatos.rows[1].cells[1].valor = new Select { id = "cmbTIPO_S", diccionario = Dictionaries.GetAutorizacionesComprobante(), withempty = true }.ToString();
            tdatos.rows[2].cells[0].valor = "Atutorizado Por";
            tdatos.rows[2].cells[1].valor = new Select { id = "txtAUT_POR_S", diccionario = Dictionaries.GetUsuario(), withempty = true }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdtra = new HtmlTable();
            tdtra.CreteEmptyTable(3, 2);
            tdtra.rows[0].cells[0].valor = "Fecha Autorizacion:";
            tdtra.rows[0].cells[1].valor = new Input { id = "txtFECHA_S", datepicker = true, clase = Css.medium, obligatorio = true }.ToString();
            tdtra.rows[1].cells[0].valor = "Modificado:";
            tdtra.rows[1].cells[1].valor = new Select { id = "txtMOD_POR_S", diccionario = Dictionaries.GetUsuario(), withempty = true }.ToString();
            tdtra.rows[2].cells[0].valor = "Fecha Modificacion:";
            tdtra.rows[2].cells[1].valor = new Input { id = "txtFECHA_P_MOD_S", datepicker = true, clase = Css.medium, obligatorio = true }.ToString();
            html.AppendLine(tdtra.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }

        [WebMethod]
        public static string GetDetalle()
        {
            pageIndex = 1;
            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tddatos";
            tdatos.invoice = true;
            tdatos.clase = "scrolltable";
            tdatos.AddColumn("Comprobante ", "", "", "");
            tdatos.AddColumn("Autorizacion", "", "", "");
            tdatos.AddColumn("Tipo", "", "", "");
            tdatos.AddColumn("Comprobante Final", "", "", "");
            tdatos.AddColumn("Autorizado Por", "", "", "");
            tdatos.AddColumn("Fecha ", "", "", "");
            tdatos.AddColumn("Modificado", "", "", "");
            tdatos.AddColumn("Fecha Modificacion", "", "", "");
            tdatos.editable = true;           
            html.AppendLine(tdatos.ToString());
            return html.ToString();
        }

        [WebMethod]
        public static string GetDetalleData(object objeto)
        {           
            SetWhereClause(new Autoriza(objeto));       
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            List<Autoriza> lst = AutorizaBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);            
            StringBuilder html = new StringBuilder();
            foreach (Autoriza item in lst)
            {
                List<HtmlCols> array = new List<HtmlCols>();
                HtmlRow row = new HtmlRow();
                row.id = "{\"aut_cco_comproba\":\"" + item.aut_cco_comproba + "\", \"aut_empresa\":\"" + item.aut_empresa + "\", \"aut_secuencia\":\"" + item.aut_secuencia + "\"}";//ID COMPUESTO
                row.removable = true;
                row.editable = true;
                row.clickevent = "";
                array.Add(new HtmlCols { titulo = "Comprobante" });
                array.Add(new HtmlCols { titulo = "Autorizacion" });
                array.Add(new HtmlCols { titulo = "Tipo" });
                array.Add(new HtmlCols { titulo = "Comprobante Final" });
                array.Add(new HtmlCols { titulo = "Autorizado Por" });
                array.Add(new HtmlCols { titulo = "Fecha" });
                array.Add(new HtmlCols { titulo = "Modificado" });
                array.Add(new HtmlCols { titulo = "Fecha Modificacion" });
                row.cols = array;

                Comprobante comp = new Comprobante();
                comp.com_codigo_key = item.aut_cco_comproba;
                comp.com_empresa_key = item.aut_empresa_key;
                comp = ComprobanteBLL.GetByPK(comp);

                row.cells.Add(new HtmlCell { valor = comp.com_doctran, clase = Css.left });
                row.cells.Add(new HtmlCell { valor = item.aut_usuario, clase = Css.left });
                switch (item.aut_tipo)
                {
                    case 1:                        
                        row.cells.Add(new HtmlCell { valor = "Modificar", clase = Css.left });
                        break;
                    case 2:
                        row.cells.Add(new HtmlCell { valor = "Eliminar", clase = Css.left });                       
                        break;
                    case 3:
                        row.cells.Add(new HtmlCell { valor = "Duplicar", clase = Css.left });                        
                        break;
                    case 4:
                        row.cells.Add(new HtmlCell { valor = "Cambiar Fecha", clase = Css.left });                        
                        break;
                    case 5:
                        row.cells.Add(new HtmlCell { valor = "Reimprimir", clase = Css.left });                       
                        break;
                    default:
                        row.cells.Add(new HtmlCell { valor = "", clase = Css.left });
                       
                        break;

                }




               // row.cells.Add(new HtmlCell { valor = item.aut_tipo, clase = Css.left });
                row.cells.Add(new HtmlCell { valor = comp.com_doctran, clase = Css.left });
                row.cells.Add(new HtmlCell { valor = item.aut_usu_autoriza, clase = Css.left });
                row.cells.Add(new HtmlCell { valor = item.aut_fecha, clase = Css.left });
                row.cells.Add(new HtmlCell { valor = item.aut_usu_modifica, clase = Css.left });
                row.cells.Add(new HtmlCell { valor = item.aut_usu_fecha, clase = Css.left });
                html.AppendLine(row.ToString());
            }           
            return html.ToString();
        }

        public static void SetWhereClause(Autoriza obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (obj.aut_cco_comproba > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " aut_cco_comproba = {" + contador + "} ";
                valores.Add(obj.aut_cco_comproba);
                contador++;
            }
            if (obj.aut_tipo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " aut_tipo = {" + contador + "} ";
                valores.Add(obj.aut_tipo);
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.aut_usuario))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " aut_usuario like  {" + contador + "} ";
                valores.Add(obj.aut_usuario);
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.aut_usu_autoriza))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " aut_usu_autoriza like  {" + contador + "} ";
                valores.Add(obj.aut_usu_autoriza);
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.aut_usu_modifica))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " aut_usu_modifica like  {" + contador + "} ";
                valores.Add(obj.aut_usu_modifica);
                contador++;
            }
            if (obj.aut_usu_fecha > DateTime.MinValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " aut_usu_fecha between {" + contador + "} ";
                valores.Add(obj.aut_usu_fecha);
                contador++;

                parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
                valores.Add(obj.aut_usu_fecha.Value.AddDays(1));
                contador++;

              
            }
            if (obj.aut_fecha>DateTime.MinValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " aut_fecha between  {" + contador + "} ";
                valores.Add(obj.aut_fecha);
                contador++;
                parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
                valores.Add(obj.aut_fecha.AddDays(1));
                contador++;
            }
            parametros.valores = valores.ToArray();
        }

        [WebMethod]
        public static string GetDlistaPrecio(object objeto)
        {
            
            Autoriza autoriza = new Autoriza(objeto);
            autoriza.aut_empresa_key = autoriza.aut_empresa;
            autoriza.aut_cco_comproba_key = autoriza.aut_cco_comproba;
            autoriza.aut_secuencia_key = autoriza.aut_secuencia;
            string idusuario = autoriza.aut_usuario;

            autoriza = AutorizaBLL.GetByPK(autoriza);

            //Usuario usr = (Usuario)HttpContext.Current.Session["usuario"];



            Comprobante comp = new Comprobante();
            comp.com_codigo_key = autoriza.aut_cco_comproba;
            comp.com_empresa_key = autoriza.aut_empresa_key;
            comp = ComprobanteBLL.GetByPK(comp);

            if (string.IsNullOrEmpty(autoriza.aut_usu_autoriza))  
               autoriza.aut_usu_autoriza= idusuario ;
            
            StringBuilder html = new StringBuilder();     
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span11\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(8, 2);
            tdatos.editable = true;
            
            tdatos.rows[0].cells[0].valor = "Comprobante Final:";
            tdatos.rows[0].cells[1].valor = new Input() { id = "txtCOMPROBA_P", placeholder = "Comprobante", autocomplete = "GetComprobanteObj", valor = comp.com_doctran, clase = Css.blocklevel ,habilitado=(autoriza.aut_cco_comproba>0)?false:true}.ToString() + new Input() { id = "cmbCOMPROBANTE", valor = comp.com_codigo, visible = false }.ToString();


            tdatos.rows[1].cells[0].valor = "Autorizado:";
            tdatos.rows[1].cells[1].valor = new Select { id = "txtAUTORIZA_P", diccionario = Dictionaries.GetUsuario(), withempty = true, valor = autoriza.aut_usuario, habilitado = true }.ToString();
        
            tdatos.rows[2].cells[0].valor = "Tipo:";
            tdatos.rows[2].cells[1].valor = new Select { id = "cmbTIPO_P", diccionario = Dictionaries.GetAutorizacionesComprobante(), withempty = true, valor = autoriza.aut_tipo, habilitado =  true }.ToString();
            
            tdatos.rows[3].cells[0].valor = "Comprobante Final:";
            tdatos.rows[3].cells[1].valor = new Input() { id = "txtCOMPROBA_FIN_P", placeholder = "Comprobante Final", autocomplete = "GetComprobanteObj", valor = comp.com_doctran, clase = Css.blocklevel, habilitado = false }.ToString() + new Input() { id = "cmbCOMPROBANTEFIN", valor = comp.com_codigo, visible = false }.ToString();
            tdatos.rows[4].cells[0].valor = "Aut. Por:";
            tdatos.rows[4].cells[1].valor = new Select() { id = "txtAUT_POR_P", placeholder = "Aut. Por", diccionario = Dictionaries.GetUsuario(), valor = autoriza.aut_usu_autoriza, withempty = true, clase = Css.blocklevel, habilitado = false }.ToString();
            tdatos.rows[5].cells[0].valor = "Fecha:";
            tdatos.rows[5].cells[1].valor = new Input { id = "txtFECHA_P", valor = (autoriza.aut_fecha == DateTime.MinValue) ? DateTime.Now : autoriza.aut_fecha, clase = Css.medium, obligatorio = true, habilitado = false }.ToString();
            tdatos.rows[6].cells[0].valor = "Modificado:";
            tdatos.rows[6].cells[1].valor = new Select { id = "txtMOD_POR_P", diccionario = Dictionaries.GetUsuario(), withempty = true, valor = autoriza.aut_usu_modifica, clase = Css.large, obligatorio = true, habilitado = false }.ToString();
            tdatos.rows[7].cells[0].valor = "Fecaha Mod:";
            tdatos.rows[7].cells[1].valor = new Input { id = "txtFECHA_P_MOD_P", valor = autoriza.aut_usu_fecha, clase = Css.medium, obligatorio = true, habilitado = false }.ToString();
          
            html.AppendLine(tdatos.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_P", visible = false, valor = autoriza.aut_secuencia }.ToString());
            html.AppendLine(" </div><!--span11-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Autoriza obj = new Autoriza(objeto);
            if (obj.aut_secuencia > 0)
            {
                return UpdateComprobante(obj);
            }
            else
            {
                return InsertComprobante(obj);
            }
        }


        public static Dlistaprecio CopyObj(Dlistaprecio obj)
        {
            Dlistaprecio c = new Dlistaprecio();
            c.dlpr_almacen = obj.dlpr_almacen;
            c.dlpr_codigo = obj.dlpr_codigo;
            c.dlpr_codigo_key = obj.dlpr_codigo_key;
            c.dlpr_empresa = obj.dlpr_empresa;
            c.dlpr_empresa_key = obj.dlpr_empresa_key;
            c.dlpr_estado = obj.dlpr_estado;
            c.dlpr_fecha_fin = obj.dlpr_fecha_fin;
            c.dlpr_fecha_ini = obj.dlpr_fecha_ini;
            c.dlpr_idalmacen = obj.dlpr_idalmacen;
            c.dlpr_listaprecio = obj.dlpr_listaprecio;
            c.dlpr_listaprecio_key = obj.dlpr_listaprecio_key;
            c.dlpr_nombreproducto = obj.dlpr_nombreproducto;
            c.dlpr_nombreumedida = obj.dlpr_nombreumedida;
            c.dlpr_precio = obj.dlpr_precio;
            c.dlpr_producto = obj.dlpr_producto;
            c.dlpr_ruta = obj.dlpr_ruta;
            c.dlpr_umedida = obj.dlpr_umedida;
            return c;
        }




        public static string InsertComprobante(Autoriza obj)
        {  
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                if (obj.aut_secuencia == 0)
                {
                    obj.aut_secuencia = AutorizaBLL.GetMax("aut_secuencia", new WhereParams("aut_cco_comproba={0} and aut_empresa={1} ", obj.aut_cco_comproba, obj.aut_empresa))+1;                
                }                    
                AutorizaBLL.Insert(transaction, obj);
                Comprobante item = new Comprobante();
                item.com_codigo_key= obj.aut_cco_comproba;
                item.com_empresa_key = obj.aut_empresa;
                item = ComprobanteBLL.GetByPK(item);
                item.com_codigo_key = obj.aut_cco_comproba;
                item.com_empresa_key = obj.aut_empresa;
                item.com_aut_tipo = obj.aut_tipo;

                item = General.Autoriza_comp(item);
                if (obj.aut_tipo == 2)
                    item.com_estado = 9;
               ComprobanteBLL.Update(item);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return "-1";
            }
            return "OK";
        }


        public static string UpdateComprobante(Autoriza obj)
        {
            obj.aut_empresa_key = obj.aut_empresa;
            obj.aut_cco_comproba_key = obj.aut_cco_comproba;
            obj.aut_secuencia_key = obj.aut_secuencia;
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                AutorizaBLL.Update(transaction, obj);
                Comprobante item = new Comprobante();
                item.com_codigo_key = obj.aut_cco_comproba;
                item.com_empresa_key = obj.aut_empresa;
                item = ComprobanteBLL.GetByPK(item);
                item.com_codigo_key = obj.aut_cco_comproba;
                item.com_empresa_key = obj.aut_empresa;
                item.com_aut_tipo = obj.aut_tipo;
                item = General.Autoriza_comp(item);
                if (obj.aut_tipo == 2)
                    item.com_estado = 9;
                ComprobanteBLL.Update(item);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return "-1";
            }
            return "OK";
        }

        [WebMethod]
        public static string RemoveObjects(object objeto)
        {
           BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                Array array = (Array)objeto;
                foreach (Object item in array)
                {
                    if (item != null)
                    {
                        Autoriza obj = new Autoriza(item);
                        AutorizaBLL.Delete(transaction, obj);
                     }                    
                }                
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return "-1";
            }
            return "OK";            
        }
    }
}