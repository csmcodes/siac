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
    public partial class wfPlanillaSocios : System.Web.UI.Page
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
                txttipodoc.Text = (Request.QueryString["tipodoc"] != null) ? Request.QueryString["tipodoc"].ToString() : "-1";
                txtcodigocomp.Text = (Request.QueryString["codigocomp"] != null) ? Request.QueryString["codigocomp"].ToString() : "-1";
                pageIndex = 1;
                pageSize = 20;
            }
        }

        [WebMethod]
        public static string GetNumero(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);
            return comprobante.com_doctran;

        }

        [WebMethod]
        public static string GetComprobante(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);
            return new JavaScriptSerializer().Serialize(comprobante);

        }

        [WebMethod]
        public static string GetFormInit()
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtFECHA", etiqueta = "Fecha", datepicker = true, datetimevalor = DateTime.Now, clase = Css.small }.ToString());
            html.AppendLine(new Select { id = "cmbALMACEN", diccionario = Dictionaries.GetAlmacen(), etiqueta = "Almacen", clase = Css.small }.ToString());
            html.AppendLine(new Select { id = "cmbPVENTA", etiqueta = "Punto Venta", diccionario = new Dictionary<string, string>(), clase = Css.small }.ToString());
            return html.ToString();
        }

        public static string ShowObject(Comprobante obj)
        {
            Vehiculo vehiculo = new Vehiculo();
            vehiculo.veh_empresa = obj.com_empresa;
            vehiculo.veh_empresa_key = obj.com_empresa;
            if (obj.com_vehiculo.HasValue)
            {
                vehiculo.veh_codigo = obj.com_vehiculo.Value;
                vehiculo.veh_codigo_key = obj.com_vehiculo.Value;
                vehiculo = VehiculoBLL.GetByPK(vehiculo);
            }



            Ruta ruta = new Ruta();
            ruta.rut_empresa = obj.com_empresa;
            ruta.rut_empresa_key = obj.com_empresa;
            if (obj.com_ruta.HasValue)
            {
                ruta.rut_codigo = obj.com_ruta.Value;
                ruta.rut_codigo_key = obj.com_ruta.Value;
                ruta = RutaBLL.GetByPK(ruta);
            }


            Persona socio = new Persona();
            socio.per_empresa = obj.com_empresa;
            socio.per_empresa_key = obj.com_empresa;
            if (obj.com_vehiculo.HasValue)
            {
                socio.per_codigo = vehiculo.veh_duenio.Value;
                socio.per_codigo_key = vehiculo.veh_duenio.Value;
                socio = PersonaBLL.GetByPK(socio);
            }


            Persona chofer = new Persona();
            chofer.per_empresa = obj.com_empresa;
            chofer.per_empresa_key = obj.com_empresa;
            if (obj.com_vehiculo.HasValue)
            {
                chofer.per_codigo = vehiculo.veh_chofer1.Value;
                chofer.per_codigo_key = vehiculo.veh_chofer1.Value;
                chofer = PersonaBLL.GetByPK(chofer);
            }

            Persona chofer2 = new Persona();
            chofer2.per_empresa = obj.com_empresa;
            chofer2.per_empresa_key = obj.com_empresa;
            if (obj.com_vehiculo.HasValue)
            {
                chofer2.per_codigo = vehiculo.veh_chofer2.Value;
                chofer2.per_codigo_key = vehiculo.veh_chofer2.Value;
                chofer2 = PersonaBLL.GetByPK(chofer2);
            }



            List<Tab> tabs = new List<Tab>();
            StringBuilder html2 = new StringBuilder();
            html2.AppendLine("<div class=\"row-fluid\">");
            html2.AppendLine("<div class=\"span6\">");
            HtmlTable tdtra = new HtmlTable();
            tdtra.CreteEmptyTable(1, 2);
            tdtra.rows[0].cells[0].valor = "Socio:";
            tdtra.rows[0].cells[1].valor = new Input { id = "txtIDSOC", valor = socio.per_id, autocomplete = "GetPersonaObj", clase = Css.small, obligatorio = true }.ToString() + " " + new Input { id = "txtSOCIO", valor = socio.per_nombres + " " + socio.per_apellidos, clase = Css.medium, habilitado = false }.ToString() + new Input { id = "txtCODSOCIO", valor = socio.per_codigo, visible = false }.ToString();
            html2.AppendLine(tdtra.ToString());
            html2.AppendLine(" </div><!--span6-->");

            html2.AppendLine("</div><!--row-fluid-->");
            html2.AppendLine(new Input { id = "txtESTADO", valor = obj.com_estado, clase = Css.small, habilitado = false, visible = false }.ToString());
            return html2.ToString();

        }


        [WebMethod]
        public static string LoadFacAsig(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object empresa = null;
            object vehiculo = null;
            object chofer = null;
            object socio = null;
            object ruta = null;
            object estado_ruta = null;
            object factura = null;
            object com_codigo = null;
            object id = null;
            tmp.TryGetValue("factura", out factura);
            tmp.TryGetValue("ruta", out ruta);
            tmp.TryGetValue("empresa", out empresa);
            tmp.TryGetValue("vehiculo", out vehiculo);
            tmp.TryGetValue("chofer", out chofer);
            tmp.TryGetValue("socio", out socio);
            tmp.TryGetValue("estado_ruta", out estado_ruta);
            tmp.TryGetValue("id", out id);
            tmp.TryGetValue("com_codigo", out com_codigo);
            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (empresa != null && !empresa.Equals(""))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dca_empresa = {" + contador + "} ";
                valores.Add(empresa);
                contador++;
            }



            if (socio != null && !socio.Equals(""))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cenv_socio = {" + contador + "} ";
                valores.Add(socio);
                contador++;
            }
            if (factura != null && !factura.Equals(""))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " detalle.com_numero = {" + contador + "} ";
                valores.Add(factura);
                contador++;
            }


            parametros.where += ((parametros.where != "") ? " and " : "") + "  dca_planilla IS NULL";



            parametros.valores = valores.ToArray();
            int codigoruta = (int)com_codigo;
            List<vDcancelacion> lstpersona = new List<vDcancelacion>();
            if (codigoruta > 0)
            {
                // List<Rutaxfactura> lstruta = RutaxfacturaBLL.GetAll("rfac_comprobanteruta = " + codigoruta, "");
                /* foreach (Rutaxfactura item in lstruta)
                 {
                     Ccomenv aux=new Ccomenv();
                     aux.cenv_empresa_key=item.rfac_empresa;
                     aux.cenv_empresa_key=item.rfac_empresa;
                     aux.cenv_comprobante=item.rfac_comprobantefac;
                     aux.cenv_comprobante_key=item.rfac_comprobantefac;
                     aux = CcomenvBLL.GetByPK(aux);
                     lstpersona.Add(aux);
                 }*/
            }
            else
            {
              //  lstpersona = vDcancelacionBLL.GetAll(parametros, "");
              lstpersona = vDcancelacionBLL.GetAll(new WhereParams(empresa, socio, "1=1"), "");
            }
            if (lstpersona.Count() > 0)
            {
                HtmlTable tdatos = new HtmlTable();
                tdatos.id = id.ToString();
                tdatos.invoice = true;
                tdatos.AddColumn("Fecha", "width10", "", "");
                tdatos.AddColumn("Nro Documento", "width10", "", "");
                tdatos.AddColumn("punto de venta", "width10", "", "");
                tdatos.AddColumn("Cliente", "width10", "", "");
                tdatos.AddColumn("Total", "width10", "", "");
                tdatos.editable = false;
                foreach (vDcancelacion item in lstpersona)
                {
                    HtmlRow row = new HtmlRow();
                    row.data = "data-codpro=" + item.dca_comprobante + " data-dca_pago=" + item.dca_pago + " data-dca_transacc=" + item.dca_transacc + " data-dca_doctran=" + item.doctrandetalle + " data-dca_comprobante_can=" + item.dca_comprobante_can + " data-dca_secuencia=" + item.dca_secuencia;
                    row.markable = true;
                    row.cells.Add(new HtmlCell { valor = item.fechadetalle });
                    row.cells.Add(new HtmlCell { valor = item.alm_nombre });
                    if (item.cenv_sociofac != null)
                    {
                        row.cells.Add(new HtmlCell { valor = item.nombres_remfac + " " + item.apellidos_remfac });
                        row.cells.Add(new HtmlCell { valor = item.doctrandetalle });
                    }
                    else
                    {
                        row.cells.Add(new HtmlCell { valor = item.nombres_remfac + " " + item.apellidos_remfac });
                        row.cells.Add(new HtmlCell { valor = item.doctranguia });
                    }
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.montocancela), clase = Css.right });
                    if (tdatos.id.Equals("tdatos"))
                        tdatos.AddRow(row);
                    else
                    {
                        List<Rutaxfactura> lstruta = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobantefac={0} and rfac_comprobanteruta={1} ", item.dca_comprobante, com_codigo), "");
                        if (lstruta.Count > 0)
                            tdatos.AddRow(row);
                    }
                }
                return tdatos.ToString();
            }

            else
            {
                HtmlTable tdatos = new HtmlTable();
                tdatos.id = id.ToString();
                tdatos.invoice = true;
                tdatos.AddColumn("Numero", "width10", "", "");
                tdatos.AddColumn("Remitente", "width10", "", "");
                tdatos.AddColumn("Destinatario", "width10", "", "");
                tdatos.AddColumn("Total", "width10", "", "");
                tdatos.editable = false;
                return tdatos.ToString();
            }

        }




        [WebMethod]
        public static string GetCabecera(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            comprobante.ccomdoc = new Ccomdoc();
            comprobante.ccomdoc.cdoc_empresa = comprobante.com_empresa;
            comprobante.ccomdoc.cdoc_empresa_key = comprobante.com_empresa;
            comprobante.ccomdoc.cdoc_comprobante = comprobante.com_codigo;
            comprobante.ccomdoc.cdoc_comprobante_key = comprobante.com_codigo;
            comprobante.ccomdoc = CcomdocBLL.GetByPK(comprobante.ccomdoc);

            comprobante.ccomenv = new Ccomenv();
            comprobante.ccomenv.cenv_empresa = comprobante.com_empresa;
            comprobante.ccomenv.cenv_empresa_key = comprobante.com_empresa;
            comprobante.ccomenv.cenv_comprobante = comprobante.com_codigo;
            comprobante.ccomenv.cenv_comprobante_key = comprobante.com_codigo;
            comprobante.ccomenv = CcomenvBLL.GetByPK(comprobante.ccomenv);

            comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);

            return ShowObject(comprobante);
            //return ShowObject(new Comprobante());
        }

        [WebMethod]
        public static string GetDetalle(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            comprobante.ccomdoc = new Ccomdoc();
            comprobante.ccomdoc.cdoc_empresa = comprobante.com_empresa;
            comprobante.ccomdoc.cdoc_empresa_key = comprobante.com_empresa;
            comprobante.ccomdoc.cdoc_comprobante = comprobante.com_codigo;
            comprobante.ccomdoc.cdoc_comprobante_key = comprobante.com_codigo;
            comprobante.ccomdoc = CcomdocBLL.GetByPK(comprobante.ccomdoc);

            comprobante.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa={0} and ddoc_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "ddoc_secuencia");
            StringBuilder html2 = new StringBuilder();
            html2.AppendLine("<div class=\"row-fluid\">");

            html2.AppendLine("<div>");
            html2.AppendLine(new Boton { id = "agregar", click = "MoveRigth();return false;", valor = "Seleccionar Todo" }.ToString());
            html2.AppendLine(new Boton { id = "quitar", click = "MoveLeft();return false;", valor = "Deseleccionar Todo" }.ToString());
            html2.AppendLine(" </div><!--span2-->");

            html2.AppendLine("<div class=\"span12\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdatos";
            tdatos.titulo = "Comprobantes No Asignados";
            tdatos.invoice = true;
            tdatos.AddColumn("Fecha", "width10", "", "");
            tdatos.AddColumn("Nro Documento", "width10", "", "");
            tdatos.AddColumn("punto de venta", "width10", "", "");
            tdatos.AddColumn("Cliente", "width10", "", "");
            tdatos.AddColumn("Total", "width10", "", "");

            /*     tdatos.AddColumn("Remitente", "width10", "", new Input() { id = "txtPRODUCTO", placeholder = "REMITENTE", clase = Css.blocklevel, habilitado = false }.ToString());
                 tdatos.AddColumn("Destinatario", "width10", "", new Textarea() { id = "txtOBSERVACION", placeholder = "DESTINACION", clase = Css.blocklevel }.ToString());
                 tdatos.AddColumn("Total", "width10", "", new Select() { id = "cmbUMEDIDA", placeholder = "TOTAL", diccionario = Dictionaries.GetUmedida(), clase = Css.blocklevel }.ToString() + new Input() { id = "txtFACTOR", visible = false });           
            
             */
            tdatos.editable = false;
          //  List<vDcancelacion> lstpersona = vDcancelacionBLL.GetAllFree(new WhereParams("cabecera.com_empresa={0} and  dca_planilla IS NULL", comprobante.com_empresa), "");
            List<vDcancelacion> lstpersona = vDcancelacionBLL.GetAll(new WhereParams(comprobante.com_empresa, comprobante.com_codclipro, "1=1"), "");
            foreach (vDcancelacion item in lstpersona)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-codpro=" + item.dca_comprobante + " data-dca_pago=" + item.dca_pago + " data-dca_transacc=" + item.dca_transacc + " data-dca_doctran=" + item.doctrandetalle + " data-dca_comprobante_can=" + item.dca_comprobante_can + " data-dca_secuencia=" + item.dca_secuencia;
                row.markable = true;
                row.cells.Add(new HtmlCell { valor = item.fechadetalle });
                row.cells.Add(new HtmlCell { valor = item.alm_nombre });

                if (item.cenv_sociofac != null)
                {
                    row.cells.Add(new HtmlCell { valor = item.nombres_remfac + " " + item.apellidos_remfac });
                    row.cells.Add(new HtmlCell { valor = item.doctrandetalle });
                }
                else
                {
                    row.cells.Add(new HtmlCell { valor = item.nombres_remfac + " " + item.apellidos_remfac });
                    row.cells.Add(new HtmlCell { valor = item.doctranguia });
                }
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.montocancela), clase = Css.right });

                tdatos.AddRow(row);
                //tdatos.AddRow(new HtmlRow(item.ddoc_productoid, item.ddoc_productonombre, item.ddoc_observaciones, item.ddoc_productounidad, item.ddoc_cantidad, item.ddoc_precio, item.ddoc_dscitem, item.ddoc_total, item.ddoc_productoiva) { data = "data-codpro=" + item.ddoc_producto });   

            }
            html2.AppendLine(tdatos.ToString());
            html2.AppendLine(" </div><!--span4-->");






            html2.AppendLine("</div><!--row-fluid-->");
            return html2.ToString();
        }


        [WebMethod]
        public static string GetPie(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span4\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(1, 2);
           
            tdatos.rows[0].cells[0].valor = "TOTAL:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtTOTALCOM", clase = Css.small + " total", habilitado = false }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span4-->");
            html.AppendLine("</div><!--row-fluid-->");









            return html.ToString();
        }



        [WebMethod]
        public static string GetProduct(object id)
        {
            Producto product = new Producto();
            if (id != null)
            {
                List<Producto> lst = new List<Producto>();
                lst = ProductoBLL.GetAll("pro_empresa=1 and pro_id='" + id + "'", "");
                if (lst.Count > 0)
                {
                    product = lst[0];
                    //product.dlistaprecio = DlistaprecioBLL.GetAll(new WhereParams("dlpr_producto= {0} and dlpr_listaprecio = {1} and dlpr_empresa = {2} ", product.pro_codigo, int.Parse(lista.ToString()), 1), "");
                    product.factores = FactorBLL.GetAll("fac_producto=" + product.pro_codigo, "");
                }
            }
            return new JavaScriptSerializer().Serialize(product);
        }

        [WebMethod]
        public static string GetProductPrice(object objeto)
        {
            string precio = "0";

            if (objeto != null)
            {

                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object producto = null;
                object lista = null;
                object unidad = null;
                object fecha = null;

                tmp.TryGetValue("producto", out producto);
                tmp.TryGetValue("lista", out lista);
                tmp.TryGetValue("unidad", out unidad);
                //tmp.TryGetValue("fecha", out fecha);

                List<Dlistaprecio> lst = DlistaprecioBLL.GetAll(new WhereParams("dlpr_empresa = {0} and dlpr_producto = {1} and dlpr_listaprecio = {2} and dlpr_umedida= {3} and ((dlpr_fecha_ini >= {4} and dlpr_fecha_fin IS NULL) or ({4} between dlpr_fecha_ini and dlpr_fecha_fin))", 1, int.Parse(producto.ToString()), int.Parse(lista.ToString()), int.Parse(unidad.ToString()), DateTime.Now), "");
                if (lst.Count > 0)
                    precio = lst[0].dlpr_precio.ToString();
                else
                    precio = "-1";
            }
            return precio.Replace(",", ".");
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Comprobante obj = new Comprobante(objeto);
            if (obj.com_codigo > 0)
            {
                return UpdateComprobante(obj);
            }
            else
            {
                return InsertComprobante(obj);
                
            }
        }


        [WebMethod]
        public static string GetTotales(object objeto)
        {
            Comprobante obj = new Comprobante(objeto);
            obj.total.tot_total = 0;


            foreach (Dcancelacion item in obj.cancelaciones)
            {
                Dcancelacion dcancelacion = new Dcancelacion();
                dcancelacion.dca_comprobante = item.dca_comprobante;
                dcancelacion.dca_empresa = item.dca_empresa;
                dcancelacion.dca_comprobante_can = item.dca_comprobante_can;
                dcancelacion.dca_doctran = item.dca_doctran;
                dcancelacion.dca_pago = item.dca_pago;
                dcancelacion.dca_transacc = item.dca_transacc;
                dcancelacion.dca_secuencia = item.dca_secuencia;




                dcancelacion.dca_comprobante_key = item.dca_comprobante;
                dcancelacion.dca_empresa_key = item.dca_empresa;
                dcancelacion.dca_comprobante_can_key = item.dca_comprobante_can;
                dcancelacion.dca_doctran_key = item.dca_doctran;
                dcancelacion.dca_pago_key = item.dca_pago;
                dcancelacion.dca_transacc_key = item.dca_transacc;
                dcancelacion.dca_secuencia_key = item.dca_secuencia;
                dcancelacion = DcancelacionBLL.GetByPK(dcancelacion);
                obj.total.tot_total += dcancelacion.dca_monto ?? 0;

                //    obj.total.poliValores[(int)ccom.cdoc_politica] += total.tot_total;
            }
            return new JavaScriptSerializer().Serialize(obj);
        }



        public static string InsertComprobante(Comprobante obj)
        {
            DateTime fecha = DateTime.Now;
            #region Actualiza el numero de comprobante en 1
            Dtipocom dti = new Dtipocom();
            dti.dti_empresa = obj.com_empresa;
            dti.dti_empresa_key = obj.com_empresa;
            dti.dti_periodo = fecha.Year;
            dti.dti_periodo_key = fecha.Year;
            dti.dti_ctipocom = obj.com_ctipocom;
            dti.dti_ctipocom_key = obj.com_ctipocom;
            dti.dti_almacen = obj.com_almacen.Value;
            dti.dti_almacen_key = obj.com_almacen.Value;
            dti.dti_puntoventa = obj.com_pventa.Value;
            dti.dti_puntoventa_key = obj.com_pventa.Value;
            dti = DtipocomBLL.GetByPK(dti);
            dti.dti_numero = dti.dti_numero.Value + 1;
            #endregion


            obj.com_numero = dti.dti_numero.Value;
            obj.com_concepto = "PLANILLA SOCIOS";
            obj.com_modulo = General.GetModulo(obj.com_tipodoc); ;
            obj.com_transacc = General.GetTransacc(obj.com_tipodoc);
            obj.com_nocontable = 1;//HAY QUE DEFINIR

            obj.com_descuadre = 0;
            obj.com_adestino = 0;
            obj.com_doctran = General.GetNumeroComprobante(obj);

            /* obj.com_empresa = 1;
             obj.com_periodo = fecha.Year;
             obj.com_tipodoc = 4;
             obj.com_ctipocom = 4; //FACT
             obj.com_numero = dti.dti_numero.Value;
             obj.com_modulo = 2;
             obj.com_transacc = 1;
             obj.com_nocontable = 1;
             obj.com_descuadre = 0;
             obj.com_adestino = 0;
             obj.com_doctran = General.GetNumeroComprobante(obj);
             obj.com_fecha = fecha;*/
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                int codigo = ComprobanteBLL.InsertIdentity(transaction, obj);
                obj.total.tot_comprobante = codigo;
                TotalBLL.Insert(transaction, obj.total);
                foreach (Dcancelacion item in obj.cancelaciones)
                {
                    Dcancelacion dcancelacion = new Dcancelacion();
                    dcancelacion.dca_comprobante = item.dca_comprobante;
                    dcancelacion.dca_empresa = item.dca_empresa;
                    dcancelacion.dca_comprobante_can = item.dca_comprobante_can;
                    dcancelacion.dca_doctran = item.dca_doctran;
                    dcancelacion.dca_pago = item.dca_pago;
                    dcancelacion.dca_transacc = item.dca_transacc;
                    dcancelacion.dca_secuencia = item.dca_secuencia;
                    dcancelacion.dca_comprobante_key = item.dca_comprobante;
                    dcancelacion.dca_empresa_key = item.dca_empresa;
                    dcancelacion.dca_comprobante_can_key = item.dca_comprobante_can;
                    dcancelacion.dca_doctran_key = item.dca_doctran;
                    dcancelacion.dca_pago_key = item.dca_pago;
                    dcancelacion.dca_transacc_key = item.dca_transacc;
                    dcancelacion.dca_secuencia_key = item.dca_secuencia;
                    dcancelacion = DcancelacionBLL.GetByPK(dcancelacion);
                    dcancelacion.dca_planilla = codigo;
                    DcancelacionBLL.Update(transaction, dcancelacion);
                }
                DtipocomBLL.Update(transaction, dti);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return "ERROR";
            }
            return "OK";
        }





        public static string UpdateComprobante(Comprobante obj)
        {

            DateTime fecha = DateTime.Now;
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                obj.com_empresa_key = obj.com_empresa;
                obj.com_codigo_key = obj.com_codigo;
                ComprobanteBLL.Update(transaction, obj);
                TotalBLL.Update(transaction, obj.total);
                List<Rutaxfactura> lstrutaxfact = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobanteruta = {0} ", obj.com_codigo), "");
                foreach (Rutaxfactura item in lstrutaxfact)
                {
                    RutaxfacturaBLL.Delete(transaction, item);
                    Ccomenv ccomenvio = new Ccomenv();
                    ccomenvio.cenv_comprobante = item.rfac_comprobantefac;
                    ccomenvio.cenv_empresa = item.rfac_empresa;
                    ccomenvio.cenv_comprobante_key = item.rfac_comprobantefac;
                    ccomenvio.cenv_empresa_key = item.rfac_empresa;
                    ccomenvio = CcomenvBLL.GetByPK(ccomenvio);
                    ccomenvio.cenv_vehiculo = null;
                    ccomenvio.cenv_estado_ruta = 0;
                    ccomenvio.cenv_placa = null;
                    ccomenvio.cenv_disco = null;
                    ccomenvio.cenv_socio = null;
                    ccomenvio.cenv_chofer = null;
                    ccomenvio.cenv_empresa_key = item.rfac_empresa;
                    ccomenvio.cenv_comprobante_key = item.rfac_comprobantefac;
                    ccomenvio.cenv_empresa_cho = null;
                    ccomenvio.cenv_empresa_soc = null;
                    ccomenvio.cenv_empresa_veh = null;
                    CcomenvBLL.Update(transaction, ccomenvio);
                }
                transaction.Commit();
                transaction = new BLL();
                transaction.CreateTransaction();
                transaction.BeginTransaction();

                foreach (Rutaxfactura item in obj.rutafactura)
                {
                    Ccomenv ccomenvio = new Ccomenv();
                    ccomenvio.cenv_comprobante = item.rfac_comprobantefac;
                    ccomenvio.cenv_empresa = item.rfac_empresa;
                    ccomenvio.cenv_comprobante_key = item.rfac_comprobantefac;
                    ccomenvio.cenv_empresa_key = item.rfac_empresa;
                    ccomenvio = CcomenvBLL.GetByPK(ccomenvio);
                    ccomenvio.cenv_vehiculo = obj.com_vehiculo;
                    ccomenvio.cenv_empresa_cho = item.rfac_empresa;
                    ccomenvio.cenv_empresa_des = item.rfac_empresa;
                    ccomenvio.cenv_empresa_rem = item.rfac_empresa;
                    ccomenvio.cenv_empresa_soc = item.rfac_empresa;
                    ccomenvio.cenv_empresa_veh = item.rfac_empresa;
                    ccomenvio.cenv_comprobante_key = item.rfac_comprobantefac;
                    ccomenvio.cenv_empresa_key = item.rfac_empresa;
                    ccomenvio.cenv_estado_ruta = item.rfac_estado;
                    Vehiculo vehiculo = new Vehiculo();
                    vehiculo.veh_codigo = (int)obj.com_vehiculo;
                    vehiculo.veh_empresa = item.rfac_empresa;
                    vehiculo.veh_codigo_key = (int)obj.com_vehiculo;
                    vehiculo.veh_empresa_key = item.rfac_empresa;
                    vehiculo = VehiculoBLL.GetByPK(vehiculo);
                    ccomenvio.cenv_socio = (int)vehiculo.veh_duenio;
                    ccomenvio.cenv_placa = vehiculo.veh_placa;
                    ccomenvio.cenv_disco = vehiculo.veh_disco;
                    ccomenvio.cenv_chofer = obj.com_codclipro;
                    CcomenvBLL.Update(transaction, ccomenvio);
                    item.rfac_comprobanteruta = obj.com_codigo;
                    item.rfac_comprobanteruta_key = obj.com_codigo;
                    RutaxfacturaBLL.Insert(transaction, item);
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return "ERROR";
            }
            return "OK";
        }



    }
}