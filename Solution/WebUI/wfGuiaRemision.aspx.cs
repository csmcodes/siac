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
    public partial class wfGuiaRemision : System.Web.UI.Page
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

            Ccomrem ccomremaux = new Ccomrem(objeto);
            Ccomrem ccomrem = new Ccomrem();
            ccomrem.crem_empresa = comprobante.com_empresa;
            ccomrem.crem_empresa_key = comprobante.com_empresa;
            ccomrem.crem_comprobante = comprobante.com_codigo;
            ccomrem.crem_comprobante_key = comprobante.com_codigo;
            ccomrem = CcomremBLL.GetByPK(ccomrem);

            if (!ccomrem.crem_factura.HasValue)
                ccomrem.crem_factura = ccomremaux.crem_factura;

            //CARGA FACTURA DE DONDE SE GENERA GUIA


            Comprobante factura = new Comprobante();
            if (ccomrem.crem_factura.HasValue)
            {
                factura.com_empresa = comprobante.com_empresa;
                factura.com_empresa_key = comprobante.com_empresa;
                factura.com_codigo = ccomrem.crem_factura.Value;
                factura.com_codigo_key = ccomrem.crem_factura.Value;
                factura = ComprobanteBLL.GetByPK(factura);
            

                factura.ccomenv = new Ccomenv();
                factura.ccomenv.cenv_empresa = factura.com_empresa;
                factura.ccomenv.cenv_empresa_key = factura.com_empresa;
                factura.ccomenv.cenv_comprobante = factura.com_codigo;
                factura.ccomenv.cenv_comprobante_key = factura.com_codigo;
                factura.ccomenv = CcomenvBLL.GetByPK(factura.ccomenv);

                Ruta ruta = new Ruta();
                ruta.rut_empresa = factura.com_empresa;
                ruta.rut_empresa_key = factura.com_empresa;
                ruta.rut_codigo = factura.ccomenv.cenv_ruta.Value;
                ruta.rut_codigo_key = factura.ccomenv.cenv_ruta.Value;
                ruta = RutaBLL.GetByPK(ruta);


                ccomrem.crem_remitente = ccomrem.crem_remitente.HasValue ? ccomrem.crem_remitente : factura.ccomenv.cenv_remitente;
                ccomrem.crem_ciruc_rem = string.IsNullOrEmpty(ccomrem.crem_ciruc_rem) ? factura.ccomenv.cenv_ciruc_rem : ccomrem.crem_ciruc_rem;
                ccomrem.crem_nombres_rem = string.IsNullOrEmpty(ccomrem.crem_nombres_rem) ? factura.ccomenv.cenv_nombres_rem : ccomrem.crem_nombres_rem;
                ccomrem.crem_direccion_rem= string.IsNullOrEmpty(ccomrem.crem_direccion_rem) ? ruta.rut_origen+" "+ factura.ccomenv.cenv_direccion_rem : ccomrem.crem_direccion_rem;

                ccomrem.crem_destinatario= ccomrem.crem_destinatario.HasValue ? ccomrem.crem_destinatario : factura.ccomenv.cenv_destinatario;
                ccomrem.crem_ciruc_des= string.IsNullOrEmpty(ccomrem.crem_ciruc_des) ? factura.ccomenv.cenv_ciruc_des: ccomrem.crem_ciruc_des;
                ccomrem.crem_nombres_des = string.IsNullOrEmpty(ccomrem.crem_nombres_des) ? factura.ccomenv.cenv_nombres_des : ccomrem.crem_nombres_des;
                ccomrem.crem_direccion_des = string.IsNullOrEmpty(ccomrem.crem_direccion_des) ? ruta.rut_destino + " " + factura.ccomenv.cenv_direccion_des : ccomrem.crem_direccion_des;

                ccomrem.crem_chofer = ccomrem.crem_chofer.HasValue ? ccomrem.crem_chofer: factura.ccomenv.cenv_chofer;
                //ccomrem.crem_ciruc_cho= string.IsNullOrEmpty(ccomrem.crem_ciruc_cho) ? factura.ccomenv.cenv_ciruc_: ccomrem.crem_ciruc_des;
                ccomrem.crem_nombres_cho = string.IsNullOrEmpty(ccomrem.crem_nombres_cho) ? factura.ccomenv.cenv_nombres_cho: ccomrem.crem_nombres_cho;
                ccomrem.crem_placa = string.IsNullOrEmpty(ccomrem.crem_placa) ? factura.ccomenv.cenv_placa : ccomrem.crem_placa;


            }




            bool habilitado = true;
            if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;


            List<Tab> tabs = new List<Tab>();


            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Inicio Traslado:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtFECHAINI", clase = Css.medium, habilitado = habilitado, datepicker = true, datetimevalor = ccomrem.crem_trasladoini }.ToString();
            tdatos.rows[1].cells[0].valor = "Fin Traslado:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtFECHAFIN", clase = Css.medium, habilitado = habilitado, datepicker = true, datetimevalor = ccomrem.crem_trasladofin}.ToString();
            tdatos.rows[2].cells[0].valor = "Motivo:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtMOTIVO", clase = Css.medium, habilitado = habilitado, valor = ccomrem.crem_motivo }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(3, 2);
            tdatos1.rows[0].cells[0].valor = "Factura:";
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtFACTURA", clase = Css.medium, valor = factura.com_doctran, habilitado = false }.ToString() + " " + new Input { id = "txtCODFACTURA", visible = false, valor = factura.com_codigo }.ToString();            
            tdatos1.rows[1].cells[0].valor = "Fecha emisión:";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtFECHAEMISION", clase = Css.medium, valor = factura.com_fecha, habilitado = false }.ToString();
            tdatos1.rows[2].cells[0].valor = "Nro Aduana:";
            tdatos1.rows[2].cells[1].valor = new Input { id = "txtNROADUANA", clase = Css.medium, habilitado = habilitado}.ToString();
            

            html.AppendLine(tdatos1.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");


            html.AppendLine(new Input { id = "txtESTADO", valor = comprobante.com_estado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCERRADO", valor = Constantes.cEstadoMayorizado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCREADO", valor = Constantes.cEstadoProceso, visible = false }.ToString());

          


            StringBuilder html1 = new StringBuilder();
            html1.AppendLine("<div class=\"row-fluid\">");
            html1.AppendLine("<div class=\"span6\">");
            HtmlTable tdrem = new HtmlTable();
            tdrem.CreteEmptyTable(3, 2);
            tdrem.rows[0].cells[0].valor = "Remitente:";
            tdrem.rows[0].cells[1].valor = new Input { id = "txtNOMBRESREM", autocomplete = "GetPersonaObj", clase = Css.large, obligatorio = true, valor = ccomrem.crem_nombres_rem, habilitado = habilitado }.ToString() + new Input { id = "txtCODREM", visible = false, valor = ccomrem.crem_remitente}.ToString() +  new Boton { small = true, id = "btncallrem", tooltip = "Agregar Remitente", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cCliente + ")" }.ToString() + new Boton { small = true, id = "btncleanrem", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();
            tdrem.rows[1].cells[0].valor = "CI/RUC:";
            tdrem.rows[1].cells[1].valor = new Input { id = "txtCIRUCREM", habilitado = false, valor = ccomrem.crem_ciruc_rem }.ToString();
            tdrem.rows[2].cells[0].valor = "Partida (Dir.):";
            tdrem.rows[2].cells[1].valor = new Input { id = "txtDIRECCIONREM", clase = Css.large, valor = ccomrem.crem_direccion_rem, habilitado = habilitado }.ToString();
            html1.AppendLine(tdrem.ToString());
            html1.AppendLine(" </div><!--span6-->");         

            html1.AppendLine("<div class=\"span6\">");
            HtmlTable tddes = new HtmlTable();
            tddes.CreteEmptyTable(3, 2);
            tddes.rows[0].cells[0].valor = "Destinatario:";
            tddes.rows[0].cells[1].valor = new Input { id = "txtNOMBRESDES", autocomplete = "GetPersonaObj", clase = Css.large, obligatorio = true, valor = ccomrem.crem_nombres_des, habilitado = habilitado }.ToString() + new Input { id = "txtCODREM", visible = false, valor = ccomrem.crem_destinatario}.ToString() + new Boton { small = true, id = "btncalldes", tooltip = "Agregar Destinatario", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cCliente + ")" }.ToString() + new Boton { small = true, id = "btncleandes", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();
            tddes.rows[1].cells[0].valor = "CI/RUC:";
            tddes.rows[1].cells[1].valor = new Input { id = "txtCIRUCDES", habilitado = false, valor = ccomrem.crem_ciruc_des}.ToString();
            tddes.rows[2].cells[0].valor = "Llegada (Dir.):";
            tddes.rows[2].cells[1].valor = new Input { id = "txtDIRECCIONDES", clase = Css.large, valor = ccomrem.crem_direccion_des, habilitado = habilitado }.ToString();            
            html1.AppendLine(tddes.ToString());
            html1.AppendLine(" </div><!--span6-->");
            html1.AppendLine("</div><!--row-fluid-->");


            StringBuilder html2 = new StringBuilder();
            html2.AppendLine("<div class=\"row-fluid\">");
            html2.AppendLine("<div class=\"span6\">");
            HtmlTable tdtra = new HtmlTable();
            tdtra.CreteEmptyTable(3, 2);
            tdtra.rows[0].cells[0].valor = "Chofer:";
            tdtra.rows[0].cells[1].valor = new Input { id = "txtNOMBRESCHO", autocomplete = "GetPersonaObj", clase = Css.large, obligatorio = true, valor = ccomrem.crem_nombres_cho, habilitado = habilitado }.ToString() + new Input { id = "txtCODCHO", visible = false, valor = ccomrem.crem_chofer}.ToString() + new Boton { small = true, id = "btncallcho", tooltip = "Agregar Chofer", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cCliente + ")" }.ToString() + new Boton { small = true, id = "btncleancho", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();
            tdtra.rows[1].cells[0].valor = "CI/RUC:";
            tdtra.rows[1].cells[1].valor = new Input { id = "txtCIRUCCHO", habilitado = false, valor = ccomrem.crem_ciruc_cho}.ToString();
            tdtra.rows[2].cells[0].valor = "Placa:";
            tdtra.rows[2].cells[1].valor = new Input { id = "txtPLACA", clase = Css.small, valor = ccomrem.crem_placa, habilitado = habilitado }.ToString();
            
            html2.AppendLine(tdtra.ToString());
            html2.AppendLine(" </div><!--span6-->");
            html2.AppendLine("</div><!--row-fluid-->");

            


            tabs.Add(new Tab("tab1", "Datos Comprobante", html.ToString()));
            tabs.Add(new Tab("tab2", "Remitente/Destinatario", html1.ToString()));
            tabs.Add(new Tab("tab3", "Transportación", html2.ToString()));

            return new Tabs { id = "tabcomprobante", tabs = tabs }.ToString();
        }

      
        [WebMethod]
        public static string GetDetalle(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            Ccomrem ccomrem = new Ccomrem(objeto);

            Comprobante factura = new Comprobante();
            if (ccomrem.crem_factura.HasValue)
            {
                factura.com_empresa = comprobante.com_empresa;
                factura.com_empresa_key = comprobante.com_empresa;
                factura.com_codigo = ccomrem.crem_factura.Value;
                factura.com_codigo_key = ccomrem.crem_factura.Value;
                factura = ComprobanteBLL.GetByPK(factura);

                factura.ccomdoc = new Ccomdoc();
                factura.ccomdoc.cdoc_empresa = factura.com_empresa;
                factura.ccomdoc.cdoc_empresa_key = factura.com_empresa;
                factura.ccomdoc.cdoc_comprobante = factura.com_codigo;
                factura.ccomdoc.cdoc_comprobante_key = factura.com_codigo;
                factura.ccomdoc = CcomdocBLL.GetByPK(factura.ccomdoc);

                factura.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa={0} and ddoc_comprobante={1}", factura.com_empresa, factura.com_codigo), "ddoc_secuencia");

            }




            bool habilitado = true;
            if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;

           
            List<Dcomrem> detalle =  DcomremBLL.GetAll(new WhereParams("drem_empresa={0} and drem_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "drem_secuencia");
            if (detalle.Count==0)
            {
                foreach (Dcomdoc item in factura.ccomdoc.detalle)
                {
                    Dcomrem dcomren = new Dcomrem();
                    dcomren.drem_descripcion = item.ddoc_productonombre + " " + item.ddoc_observaciones;
                    dcomren.drem_cantidad = item.ddoc_cantidad;
                    detalle.Add(dcomren);
                }
                
            }
            

            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdinvoice";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";

            tdatos.AddColumn("Descripción", "width70", "", new Textarea() { id = "txtDESCRIPCION", placeholder = "DESCRIPCIÓN", clase = Css.blocklevel }.ToString());
            tdatos.AddColumn("Cantidad", "width20", Css.center, new Input() { id = "txtCANTIDAD", placeholder = "CANT", clase = Css.blocklevel + Css.cantidades, numeric = true }.ToString());
            //tdatos.AddColumn("Peso", "width20", Css.right, new Input() { id = "txtPESO", placeholder = "PESO", clase = Css.blocklevel + Css.amount, numeric = true }.ToString());
            if (habilitado)
                tdatos.AddColumn("", "width5", Css.center, new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());

            //tdatos.editable = true;
            tdatos.editable = habilitado;

            foreach (Dcomrem item in detalle)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-codpro=" + item.drem_producto ;
                row.removable = true;

                row.cells.Add(new HtmlCell { valor = item.drem_descripcion});
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.drem_cantidad), clase = Css.center });
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
            object objetocrem = null;
            object objetodrem = null;
            tmp.TryGetValue("comprobante", out objetocomp);
            tmp.TryGetValue("ccomrem", out objetocrem);
            tmp.TryGetValue("dcomrem", out objetodrem);

            Comprobante comprobante = new Comprobante(objetocomp);
            Ccomrem ccomrem = new Ccomrem(objetocrem);
            

            List<Dcomrem> dcomrem = new List<Dcomrem>();
            if (objetodrem!= null)
            {
                Array array = (Array)objetodrem;
                foreach (Object item in array)
                {
                    if (item != null)
                        dcomrem.Add(new Dcomrem(item));
                }
            }


            if (comprobante.com_codigo < 0)
            {
                return InsertComprobante(comprobante, ccomrem, dcomrem);
            }
            else
            {
                return UpdateComprobante(comprobante,ccomrem,dcomrem);
            }

        }

        public static string InsertComprobante(Comprobante obj, Ccomrem ccomrem, List<Dcomrem> dcomrem)
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
            if (obj.com_concepto == null)
                obj.com_concepto = "GUIA DE REMISIÓN " + obj.com_fecha.ToShortDateString();

            obj.com_modulo = General.GetModulo(obj.com_tipodoc); ;
            obj.com_transacc = General.GetTransacc(obj.com_tipodoc);
            obj.com_nocontable = 1;//HAY QUE DEFINIR           
            obj.com_descuadre = 0;
            obj.com_adestino = 0;
            obj.com_doctran = General.GetNumeroComprobante(obj);
            obj.com_estado = Constantes.cEstadoMayorizado;
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                obj.com_codigo = ComprobanteBLL.InsertIdentity(transaction, obj);

                ccomrem.crem_comprobante = obj.com_codigo;
                CcomremBLL.Insert(transaction, ccomrem);

                int secuencia = 1;
                foreach (Dcomrem item in dcomrem)
                {
                    item.drem_comprobante = obj.com_codigo;
                    item.drem_secuencia = secuencia;
                    secuencia++;
                    DcomremBLL.Insert(transaction, item);
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

        public static string UpdateComprobante(Comprobante obj, Ccomrem ccomrem, List<Dcomrem> dcomrem)
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
            objU.com_concepto = !string.IsNullOrEmpty(obj.com_concepto) ? obj.com_concepto : "GUIA DE REMISIÓN " + obj.com_fecha.ToShortDateString(); 
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

            Ccomrem cremdel = CcomremBLL.GetByPK(new Ccomrem { crem_empresa = obj.com_empresa, crem_empresa_key = obj.com_empresa, crem_comprobante = obj.com_codigo, crem_comprobante_key = obj.com_codigo });

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                 
                ComprobanteBLL.Update(transaction, objU);
                CcomremBLL.Delete(transaction, cremdel);

                CcomremBLL.Insert(transaction, ccomrem);

                List<Dcomrem> lst = DcomremBLL.GetAll(new WhereParams("drem_empresa ={0} and drem_comprobante = {1} ", obj.com_empresa, obj.com_codigo), "");
                foreach (Dcomrem item in lst)
                {
                    DcomremBLL.Delete(transaction, item);
                }

                int secuencia = 1;
                foreach (Dcomrem item in dcomrem)
                {
                    item.drem_comprobante = obj.com_codigo;
                    item.drem_secuencia = secuencia;
                    secuencia++;
                    DcomremBLL.Insert(transaction, item);
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