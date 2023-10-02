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
    public partial class wfEgresoTransferencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txttipodoc.Text = (Request.QueryString["tipodoc"] != null) ? Request.QueryString["tipodoc"].ToString() : "-1";

                Tipodoc tdoc = TipodocBLL.GetByPK(new Tipodoc { tpd_codigo = int.Parse(txttipodoc.Text), tpd_codigo_key = int.Parse(txttipodoc.Text) });
                if (tdoc.tpd_nocontable.HasValue)
                    txtnocontable.Text = tdoc.tpd_nocontable.Value.ToString();
                else
                    txtnocontable.Text = "0";


                txtcodigocomp.Text = (Request.QueryString["codigocomp"] != null) ? Request.QueryString["codigocomp"].ToString() : "-1";
                txtcodigocompref.Text = (Request.QueryString["codigocompref"] != null) ? Request.QueryString["codigocompref"].ToString() : "-1";


            }
        }


        [WebMethod]
        public static string GetCabecera(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);


            Cmovinv cmovinv = new Cmovinv();

            cmovinv.cmo_empresa= comprobante.com_empresa;
            cmovinv.cmo_empresa_key = comprobante.com_empresa;
            cmovinv.cmo_cco_comproba= comprobante.com_codigo;
            cmovinv.cmo_cco_comproba_key= comprobante.com_codigo;
            cmovinv = CmovinvBLL.GetByPK(cmovinv);


            Persona persona = new Persona();
            persona.per_empresa = comprobante.com_empresa;
            persona.per_empresa_key = comprobante.com_empresa;
            if (cmovinv.cmo_empleado.HasValue)
            {
                persona.per_codigo = cmovinv.cmo_empleado.Value;
                persona.per_codigo_key = cmovinv.cmo_empleado.Value;
                persona = PersonaBLL.GetByPK(persona);
            }

            Bodega bodegaini = new Bodega();
            bodegaini.bod_empresa = comprobante.com_empresa;
            bodegaini.bod_empresa_key = comprobante.com_empresa;
            if (cmovinv.cmo_bodini.HasValue)
            {
                bodegaini.bod_codigo = cmovinv.cmo_bodini.Value;
                bodegaini.bod_codigo_key = cmovinv.cmo_bodini.Value;
                bodegaini = BodegaBLL.GetByPK(bodegaini);
            }

            Bodega bodegafin = new Bodega();
            bodegafin.bod_empresa = comprobante.com_empresa;
            bodegafin.bod_empresa_key = comprobante.com_empresa;
            if (cmovinv.cmo_bodfin.HasValue)
            {
                bodegafin.bod_codigo = cmovinv.cmo_bodfin.Value;
                bodegafin.bod_codigo_key = cmovinv.cmo_bodfin.Value;
                bodegafin= BodegaBLL.GetByPK(bodegafin);
            }

            //CARGA FACTURA DE DONDE SE GENERA GUIA


            bool habilitado = true;
            if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;


            List<Tab> tabs = new List<Tab>();


            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(2, 2);
            //tdatos.rows[0].cells[0].valor = "Bodega Inicio:";
            //tdatos.rows[0].cells[1].valor = new Input { id = "txtIDBODINI", valor = bodegaini.bod_id, autocomplete = "GetBodegaObj", clase = Css.small, obligatorio = true }.ToString() + " " + new Input { id = "txtBODEGAINI", valor = bodegaini.bod_nombre, clase = Css.large, habilitado = false, }.ToString() + " " + new Input { id = "txtCODBODINI", valor = bodegaini.bod_codigo, visible = false }.ToString();            
            tdatos.rows[0].cells[0].valor = "Bodega Destino:";            
            tdatos.rows[0].cells[1].valor = new Input { id = "txtIDBODFIN", valor = bodegafin.bod_id, autocomplete = "GetBodegaObj", clase = Css.small, obligatorio = true }.ToString() + " " + new Input { id = "txtBODEGAFIN", valor = bodegafin.bod_nombre, clase = Css.large, habilitado = false, }.ToString() + " " + new Input { id = "txtCODBODFIN", valor = bodegafin.bod_codigo, visible = false }.ToString();
            tdatos.rows[1].cells[0].valor = "Responsable:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtIDPER", clase = Css.medium, placeholder = "Persona", visible = false}.ToString() + " " + new Input { id = "txtNOMBRESPER", autocomplete = "GetPersonaObj", clase = Css.large, obligatorio = true, valor = persona.per_apellidos + " " + persona.per_nombres, habilitado = habilitado }.ToString() + new Input { id = "txtCODPER", visible = false, valor = persona.per_codigo, habilitado = habilitado }.ToString() + new Boton { small = true, id = "btncallper", tooltip = "Agregar Responsable", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cCliente + ")" }.ToString() + new Boton { small = true, id = "btncleanper", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();    
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(1, 2);
            tdatos1.rows[0].cells[0].valor = "Concepto:";            
            tdatos1.rows[0].cells[1].valor = new Textarea { id = "txtCONCEPTO", clase = Css.xlarge, valor = comprobante.com_concepto, habilitado = habilitado }.ToString();


            html.AppendLine(tdatos1.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");


            html.AppendLine(new Input { id = "txtESTADO", valor = comprobante.com_estado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCERRADO", valor = Constantes.cEstadoMayorizado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCREADO", valor = Constantes.cEstadoProceso, visible = false }.ToString());

            return html.ToString();
        }


        [WebMethod]
        public static string GetDetalle(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            List<Dmovinv> detalle = DmovinvBLL.GetAll(new WhereParams("dmo_empresa={0} and dmo_cco_comproba={1}", comprobante.com_empresa, comprobante.com_codigo), "dmo_secuencia");



            bool habilitado = true;
            if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;


           
            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdinvoice";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";
            tdatos.AddColumn("Id", "width10", "", new Input() { id = "txtIDPRO", placeholder = "ID", autocomplete = "GetProductoObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODPRO", visible = false }.ToString());
            tdatos.AddColumn("Producto", "width40", "", new Input() { id = "txtPRODUCTO", placeholder = "DESCRIPCION", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("Stock", "width10", Css.center, new Input() { id = "txtSTOCK", placeholder = "STOCK", clase = Css.blocklevel + Css.cantidades, numeric = true, habilitado=false }.ToString());
            tdatos.AddColumn("U.Medida", "width10", "", new Select() { id = "cmbUMEDIDA", placeholder = "UNIDAD", diccionario = Dictionaries.GetUmedida(), clase = Css.blocklevel }.ToString() + new Input() { id = "txtFACTOR", visible = false });            
            tdatos.AddColumn("Cantidad", "width20", Css.center, new Input() { id = "txtCANTIDAD", placeholder = "CANT", clase = Css.blocklevel + Css.cantidades, numeric = true }.ToString());            
            //tdatos.AddColumn("Peso", "width20", Css.right, new Input() { id = "txtPESO", placeholder = "PESO", clase = Css.blocklevel + Css.amount, numeric = true }.ToString());
            if (habilitado)
                tdatos.AddColumn("", "width5", Css.center, new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());

            //tdatos.editable = true;
            tdatos.editable = habilitado;

            foreach (Dmovinv item in detalle)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-codpro=" + item.dmo_producto;
                row.removable = true;

                row.cells.Add(new HtmlCell { valor = item.dmo_productoid });
                row.cells.Add(new HtmlCell { valor = item.dmo_productonombre });
                row.cells.Add(new HtmlCell { clase = Css.center });                //STOCK AL CARGAR NO MUESTRA NADA
                row.cells.Add(new HtmlCell { valor = item.dmo_udigitada});
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.dmo_cdigitada), clase = Css.center });

                //row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.drem_peso), clase = Css.right });
                if (habilitado)
                {
                    row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString(), clase = Css.center });
                    row.clickevent = "Edit(this)";
                }


                tdatos.AddRow(row);
                //tdatos.AddRow(new HtmlRow(item.ddoc_productoid, item.ddoc_productonombre, item.ddoc_observaciones, item.ddoc_productounidad, item.ddoc_cantidad, item.ddoc_precio, item.ddoc_dscitem, item.ddoc_total, item.ddoc_productoiva) { data = "data-codpro=" + item.ddoc_producto });   

            }

            html.AppendLine(tdatos.ToString());



            return html.ToString();
        }




        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object objetocomp = null;
            object objetocmov= null;
            object objetodmov= null;
            tmp.TryGetValue("comprobante", out objetocomp);
            tmp.TryGetValue("cmovinv", out objetocmov);
            tmp.TryGetValue("dmovinv", out objetodmov);

            Comprobante comprobante = new Comprobante(objetocomp);
            Cmovinv cmovinv = new Cmovinv(objetocmov);


            List<Dmovinv> dmovinv = new List<Dmovinv>();
            if (objetodmov != null)
            {
                Array array = (Array)objetodmov;
                foreach (Object item in array)
                {
                    if (item != null)
                        dmovinv.Add(new Dmovinv(item));
                }
            }


            if (comprobante.com_codigo < 0)
            {
                return InsertComprobante(comprobante, cmovinv, dmovinv);
            }
            else
            {
                return UpdateComprobante(comprobante, cmovinv, dmovinv);
            }
            return "";

        }

        public static string InsertComprobante(Comprobante obj, Cmovinv cmovinv, List<Dmovinv> dmovinv)
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
            dti.dti_puntoventa = obj.com_bodega.Value;
            dti.dti_puntoventa_key = obj.com_bodega.Value;
            dti = DtipocomBLL.GetByPK(dti);
            dti.dti_numero = dti.dti_numero.Value + 1;
            #endregion

            obj.com_numero = dti.dti_numero.Value;
            if (obj.com_concepto == null)
                obj.com_concepto = "TRANSFERENCIA DIRECTA " + obj.com_fecha.ToShortDateString();

            obj.com_modulo = General.GetModulo(obj.com_tipodoc); ;
            obj.com_transacc = General.GetTransacc(obj.com_tipodoc);
            obj.com_nocontable = 0;
            obj.com_descuadre = 0;
            obj.com_adestino = 0;
            obj.com_doctran = General.GetNumeroComprobante(obj);
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                obj.com_codigo = ComprobanteBLL.InsertIdentity(transaction, obj);

                cmovinv.cmo_cco_comproba = obj.com_codigo;
                cmovinv.cmo_transacc = obj.com_transacc;
                CmovinvBLL.Insert(transaction, cmovinv);

                int secuencia = 1;
                foreach (Dmovinv item in dmovinv)
                {
                    item.dmo_cco_comproba = obj.com_codigo;
                    item.dmo_secuencia = secuencia;
                    secuencia++;

                    item.dmo_catproducto = 0;
                    item.dmo_transaccion = cmovinv.cmo_transacc;
                    item.dmo_debcre = Constantes.cCredito;

                    item.dmo_cant_consigna = 0;
                    item.dmo_cant_transito= 0;
                    item.dmo_centro = 0;
                    item.dmo_pdigitada = 0;


                    DmovinvBLL.Insert(transaction, item);
                }


                DtipocomBLL.Update(transaction, dti);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return "-1";
            }
            return obj.com_codigo.ToString();
        }

        public static string UpdateComprobante(Comprobante obj, Cmovinv cmovinv, List<Dmovinv> dmovinv)
        {

            DateTime fecha = DateTime.Now;

            obj.com_empresa_key = obj.com_empresa;
            obj.com_codigo_key = obj.com_codigo;
            Comprobante objU = ComprobanteBLL.GetByPK(obj);
            objU.com_empresa_key = objU.com_empresa;
            objU.com_codigo_key = objU.com_codigo;

            objU.com_fecha = obj.com_fecha;
            objU.com_periodo = obj.com_fecha.Year;
            objU.com_codclipro = obj.com_codclipro;
            objU.com_agente = obj.com_agente;
            objU.mod_usr = obj.mod_usr;
            objU.mod_fecha = obj.mod_fecha;
            objU.com_concepto = !string.IsNullOrEmpty(obj.com_concepto) ? obj.com_concepto : "TRANSFERENCIA DIRECTA " + obj.com_fecha.ToShortDateString();
            objU.com_estado = obj.com_estado;//ACTUALIZA EL ESTADO DEL COMPROBANTE
            


            Dtipocom dti = new Dtipocom();
            if (string.IsNullOrEmpty(objU.com_doctran))
            {
                objU.com_modulo = General.GetModulo(obj.com_tipodoc); ;
                objU.com_transacc = General.GetTransacc(obj.com_tipodoc);
                objU.com_descuadre = 0;
                objU.com_adestino = 0;
                objU.com_doctran = obj.com_doctran;
                objU.com_numero = obj.com_numero;
                dti = General.GetDtipocom(objU.com_empresa, objU.com_fecha.Year, objU.com_ctipocom, objU.com_almacen.Value, objU.com_pventa.Value);
                dti.dti_numero = dti.dti_numero + 1;

                if (objU.com_numero < dti.dti_numero)
                {
                    objU.com_numero = dti.dti_numero.Value;
                    objU.com_doctran = General.GetNumeroComprobante(objU);
                }

            }


            Cmovinv cmodel = CmovinvBLL.GetByPK(new Cmovinv { cmo_empresa = obj.com_empresa, cmo_empresa_key = obj.com_empresa, cmo_cco_comproba = obj.com_codigo, cmo_cco_comproba_key = obj.com_codigo });


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
               

                ComprobanteBLL.Update(transaction, objU);
                CmovinvBLL.Delete(transaction, cmodel);

                cmovinv.cmo_cco_comproba = objU.com_codigo;
                cmovinv.cmo_transacc = objU.com_transacc;
                CmovinvBLL.Insert(transaction, cmovinv);

                List<Dmovinv> lst = DmovinvBLL.GetAll(new WhereParams("dmo_empresa ={0} and dmo_cco_comproba = {1} ", obj.com_empresa, obj.com_codigo), "");
                foreach (Dmovinv item in lst)
                {
                    DmovinvBLL.Delete(transaction, item);
                }

                int secuencia = 1;
                foreach (Dmovinv item in dmovinv)
                {
                    item.dmo_cco_comproba = obj.com_codigo;
                    item.dmo_secuencia = secuencia;
                    secuencia++;

                    item.dmo_catproducto = 0;
                    item.dmo_transaccion = cmovinv.cmo_transacc;
                    item.dmo_debcre = Constantes.cCredito;

                    item.dmo_cant_consigna = 0;
                    item.dmo_cant_transito = 0;
                    item.dmo_centro = 0;
                    item.dmo_pdigitada = 0;


                    DmovinvBLL.Insert(transaction, item);
                }

                if (dti.dti_numero.HasValue)
                    DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return "-1";
            }
            return obj.com_codigo.ToString();
        }
    }
}