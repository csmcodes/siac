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

namespace WebUI
{
    public partial class wfBusquedaComprobantes : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "com_doctran";
        protected static string WhereClause = "";
        protected static WhereParams parametros;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
            }
        }

        [WebMethod]
        public static string DeleteAutorizacion(object objeto)
        {

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
               
                Comprobante obj = new Comprobante(objeto);
            obj.com_codigo_key = obj.com_codigo;
            obj.com_empresa_key = obj.com_empresa;
            obj = ComprobanteBLL.GetByPK(obj);
            Autoriza item = new Autoriza();
            if (obj.com_aut_tipo.HasValue)
            {
                item.aut_cco_comproba_key = obj.com_codigo;
                item.aut_empresa_key = obj.com_empresa;
                item.aut_secuencia_key = AutorizaBLL.GetMax("aut_secuencia", new WhereParams("aut_cco_comproba={0} and aut_empresa={1} and aut_tipo={2}", obj.com_codigo, obj.com_empresa, obj.com_aut_tipo.Value));
                item = AutorizaBLL.GetByPK(item);
                 transaction.BeginTransaction();
                if (AutorizaBLL.Delete(transaction,item) > 0)
                {
                    obj.com_codigo_key = obj.com_codigo;
                    obj.com_empresa_key = obj.com_empresa;
                    obj.com_aut_tipo= null;
                    ComprobanteBLL.Update(transaction,obj);


                transaction.Commit();
                }
            }
            }
            catch
            {
                transaction.Rollback();
                return "-1";
            }
            return "OK";  





            
               
        }

        [WebMethod]
        public static string GetCabecera()
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span4\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(5, 2);
            tdatos.rows[0].cells[0].valor = "Periodo";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtPERIODO_S", placeholder = "Nro Documento", clase = Css.small }.ToString();
            tdatos.rows[1].cells[0].valor = "Mes:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtMES_S", placeholder = "Mes", clase = Css.small }.ToString();
            tdatos.rows[2].cells[0].valor = "Fecha";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtFECHA_S", placeholder = "Fecha", clase = Css.small, datepicker = true, datetimevalor = DateTime.Now }.ToString();
            tdatos.rows[3].cells[0].valor = "Sigla:";
            tdatos.rows[3].cells[1].valor = new Select { id = "cmbCTIPOCOM_S", clase = Css.large, diccionario = Dictionaries.GetCtipocom(), withempty = true }.ToString();
            tdatos.rows[4].cells[0].valor = "Almacen:";
            tdatos.rows[4].cells[1].valor = new Select { id = "cmbALMACEN_S", clase = Css.large, diccionario = Dictionaries.GetAlmacen(), withempty = true }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span4\">");
            HtmlTable tdtra = new HtmlTable();
            tdtra.CreteEmptyTable(4, 2);
            tdtra.rows[0].cells[0].valor = "Serie:";
            tdtra.rows[0].cells[1].valor = new Input { id = "txtSERIE_S", placeholder = "Serie", clase = Css.small }.ToString();
            tdtra.rows[1].cells[0].valor = "Numero:";
            tdtra.rows[1].cells[1].valor = new Input { id = "txtNUMERO_S", placeholder = "Numero", clase = Css.small }.ToString();
            tdtra.rows[2].cells[0].valor = "Estado:";
            tdtra.rows[2].cells[1].valor = new Select { id = "cmbESTADO_S", clase = Css.large, diccionario = Dictionaries.GetEstadoComprobante(), withempty = true }.ToString();
            tdtra.rows[3].cells[0].valor = "Tipo Doc:";
            tdtra.rows[3].cells[1].valor = new Select { id = "cmbTIPODOC_S", clase = Css.large, diccionario = Dictionaries.GetTipodoc(), withempty = true }.ToString();
            html.AppendLine(tdtra.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span4\">");
            HtmlTable tdright = new HtmlTable();
            tdright.CreteEmptyTable(4, 2);
            tdright.rows[0].cells[0].valor = "Concepto:";
            tdright.rows[0].cells[1].valor = new Input { id = "txtCONCEPTO_S", placeholder = "Concepto", clase = Css.large }.ToString();
            tdright.rows[1].cells[0].valor = "Referencia:";
            tdright.rows[1].cells[1].valor = new Input { id = "txtREFERENCIA_S", placeholder = "Referencia", clase = Css.large }.ToString();
            tdright.rows[2].cells[0].valor = "Autorizado:";
            tdright.rows[2].cells[1].valor = new Select { id = "cmbAUTORIZ_S", clase = Css.large, diccionario = Dictionaries.GetAutorizacionesComprobante(), withempty = true }.ToString();
            tdright.rows[3].cells[0].valor = "Contabilizacion:";
            tdright.rows[3].cells[1].valor = new Select { id = "cmbCONTABILZA_S", clase = Css.large, diccionario = Dictionaries.GetEstado() }.ToString();
            html.AppendLine(tdright.ToString());
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
            tdatos.AddColumn("Comprobante ", "", "");
            tdatos.AddColumn("Periodo", "", "", "");
            tdatos.AddColumn("Fecha", "", "", "");
            tdatos.AddColumn("Concepto", "", "", "");
            tdatos.AddColumn("Referencia ", "", "");
            tdatos.AddColumn("Estado", "", "", "");
            tdatos.AddColumn("Documento", "", "", "");
            tdatos.AddColumn("Modulo", "", "", "");
            tdatos.AddColumn("Autorizacion", "", "", "");
            tdatos.AddColumn("Anulado", "", "", "");
            tdatos.editable = false;
            html.AppendLine(tdatos.ToString());
            return html.ToString();
        }
        [WebMethod]
        public static string GetDetalleData(object objeto)
        {
            SetWhereClause(new Comprobante(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            List<Comprobante> lista = ComprobanteBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            StringBuilder html = new StringBuilder();
            foreach (Comprobante item in lista)
            {
                ArrayList array = new ArrayList();
                array.Add(item.com_doctran);
                array.Add(item.com_periodo);
                array.Add(item.com_fecha);
                array.Add(item.com_concepto);
                array.Add(item.com_ctipocomid);
                switch (item.com_estado)
                {
                    case 0:
                        array.Add("Proceso");
                        break;
                    case 1:
                        array.Add("Guardado");
                        break;
                    case 2:
                        array.Add("Mayorizado");
                        break;
                    case 3:
                        array.Add("Por Autorizar");
                        break;
                    case 9:
                        array.Add("Eliminado");
                        break;
                }

                array.Add(item.com_tipo_doc);
                array.Add(item.com_modulo_id);
                switch (item.com_aut_tipo)
                {
                    case 1:
                        array.Add("Modificar");
                        break;
                    case 2:
                        array.Add("Eliminar");
                        break;
                    case 3:
                        array.Add("Duplicar");
                        break;
                    case 4:
                        array.Add("Cambiar fecha");
                        break;
                    case 5:
                        array.Add("Reimprimir");
                        break;
                    default:
                        array.Add("");
                        break;

                }

                string strid = item.com_codigo.ToString();
                html.AppendLine(HtmlElements.TablaRow(array, strid));
            }
            return html.ToString();
        }
        [WebMethod]
        public static string GetPie(object objeto)
        {
            Comprobante obj = new Comprobante(objeto);
            obj.com_codigo_key = obj.com_codigo;
            obj.com_empresa_key = obj.com_empresa;
            obj = ComprobanteBLL.GetByPK(obj);
            Autoriza item = new Autoriza();

            if (obj.com_estado == Constantes.cEstadoEliminado) {
                return null;
            }
            if (obj.com_aut_tipo.HasValue)
            {

                item.aut_cco_comproba_key = obj.com_codigo;
                item.aut_empresa_key = obj.com_empresa;
                item.aut_secuencia_key = AutorizaBLL.GetMax("aut_secuencia", new WhereParams("aut_cco_comproba={0} and aut_empresa={1} and aut_tipo={2}", obj.com_codigo, obj.com_empresa, obj.com_aut_tipo.Value));
                item = AutorizaBLL.GetByPK(item);
            }








            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span5\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(4, 2);
            tdatos.rows[0].cells[0].valor = "Autorizado para";
            tdatos.rows[0].cells[1].valor = new Input { id = "cmbTIPO_P", placeholder = "Autorizado para", valor = item.aut_tipo, clase = Css.small, habilitado = false }.ToString();
            tdatos.rows[1].cells[0].valor = "Usuario Autorizado:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtAUTORIZA_P", placeholder = "Usuario Autorizado", valor = item.aut_usuario, clase = Css.small, habilitado = false }.ToString();
            tdatos.rows[2].cells[0].valor = "Autorizado por";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtAUT_POR_P", placeholder = "Autorizado por", valor = item.aut_usu_autoriza, clase = Css.small, habilitado = false }.ToString();
            tdatos.rows[3].cells[0].valor = "Codigo";
            tdatos.rows[3].cells[1].valor = new Input { id = "txtCODIGO_P", placeholder = "Codigo", valor = obj.com_codigo, clase = Css.small, habilitado = false }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span5\">");
            HtmlTable tdright = new HtmlTable();
            if (!obj.com_aut_tipo.HasValue)
            {
                tdright.CreteEmptyTable(2, 2);
                tdright.rows[1].cells[0].valor = "Autorizar:";
                tdright.rows[1].cells[1].valor = new Boton { click = "AutorizarObj();return false;", valor = "Autorizar" }.ToString();
            }
            else if (obj.com_aut_tipo.HasValue)
            {
                tdright.CreteEmptyTable(3, 2);
                tdright.rows[1].cells[0].valor = "Procesar:";
                tdright.rows[1].cells[1].valor = new Boton { click = "ProcesarObj();return false;", valor = "Procesar" }.ToString();
                tdright.rows[2].cells[0].valor = "Eliminar autorizacion:";
                tdright.rows[2].cells[1].valor = new Boton { click = "DeleteObj();return false;", valor = "Eliminar autorizacion:" }.ToString();
            }
            tdright.rows[0].cells[0].valor = "Fecha Autorizacion:";
            tdright.rows[0].cells[1].valor = new Input { id = "txtFECHA_P", placeholder = "Fecha Autorizacion", valor = item.aut_fecha, clase = Css.large, habilitado = false }.ToString();


            html.AppendLine(tdright.ToString());
            html.AppendLine(" </div><!--span6-->");
            return html.ToString();
        }



        public static void SetWhereClause(Comprobante obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (obj.com_periodo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_periodo = {" + contador + "} ";
                valores.Add(obj.com_periodo);
                contador++;
            }
            if (obj.com_mes > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_mes = {" + contador + "} ";
                valores.Add(obj.com_mes);
                contador++;
            }
            if (obj.com_ctipocom > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_ctipocom = {" + contador + "} ";
                valores.Add(obj.com_ctipocom);
                contador++;
            }
            if (obj.com_almacen > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_almacen = {" + contador + "} ";
                valores.Add(obj.com_almacen);
                contador++;
            }
            if (obj.com_serie > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_serie = {" + contador + "} ";
                valores.Add(obj.com_serie);
                contador++;
            }
            if (obj.com_numero > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_numero = {" + contador + "} ";
                valores.Add(obj.com_numero);
                contador++;
            }
            if (obj.com_estado > -1)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_estado = {" + contador + "} ";
                valores.Add(obj.com_estado);
                contador++;
            }
            if (obj.com_tipodoc > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_tipodoc = {" + contador + "} ";
                valores.Add(obj.com_tipodoc);
                contador++;
            }
            if (obj.com_aut_tipo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_aut_tipo = {" + contador + "} ";
                valores.Add(obj.com_aut_tipo);
                contador++;
            }
            if (obj.com_nocontable > -1)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_nocontable != {" + contador + "} ";
                valores.Add(obj.com_nocontable);
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.com_concepto))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_concepto like {" + contador + "} ";
                valores.Add("%" + obj.com_concepto + "%");
                contador++;
            }
            if (obj.com_ref_comprobante > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_ref_comprobante = {" + contador + "} ";
                valores.Add(obj.com_ref_comprobante);
                contador++;
            }
            if (obj.com_fecha > DateTime.MinValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_fecha between {" + contador + "} ";
                valores.Add(obj.com_fecha);
                contador++;
                parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
                valores.Add(obj.com_fecha.AddDays(1));
                contador++;
            }
            parametros.valores = valores.ToArray();
        }
    }
}