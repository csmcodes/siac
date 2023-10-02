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
    public partial class wfDiario : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "dcab_nombre";
        protected static string WhereClause = "";
        protected static WhereParams parametros;

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
                txtorigen.Text = (Request.QueryString["origen"] != null) ? Request.QueryString["origen"].ToString() : "";
                txtcodigocompref.Text = (Request.QueryString["codigocompref"] != null) ? Request.QueryString["codigocompref"].ToString() : "-1";


                pageIndex = 1;
                pageSize = 20;

            }
        }


      


        [WebMethod]
        public static string GetCabecera(object objeto)
        {


            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);

            bool habilitado = true;
            if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;

            comprobante.ccomdoc = new Ccomdoc();
            comprobante.ccomdoc.cdoc_empresa = comprobante.com_empresa;
            comprobante.ccomdoc.cdoc_empresa_key = comprobante.com_empresa;
            comprobante.ccomdoc.cdoc_comprobante = comprobante.com_codigo;
            comprobante.ccomdoc.cdoc_comprobante_key = comprobante.com_codigo;
            comprobante.ccomdoc = CcomdocBLL.GetByPK(comprobante.ccomdoc);

            comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);

            Persona persona = new Persona();
            persona.per_empresa = comprobante.com_empresa;
            persona.per_empresa_key = comprobante.com_empresa;
            if (comprobante.com_codclipro.HasValue)
            {
                persona.per_codigo = comprobante.com_codclipro.Value;
                persona.per_codigo_key = comprobante.com_codclipro.Value;
                persona = PersonaBLL.GetByPK(persona);
            }

            Centro centro = new Centro();
            centro.cen_empresa = comprobante.com_empresa;
            centro.cen_empresa_key = comprobante.com_empresa;
            if (comprobante.com_centro.HasValue)
            {
                centro.cen_codigo = comprobante.com_centro.Value;
                centro.cen_codigo_key = comprobante.com_centro.Value;
                centro = CentroBLL.GetByPK(centro);
            }
            else
            {
                centro = Constantes.GetSinCentro();  
            }

            

         
            List<Tab> tabs = new List<Tab>();

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span12\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(2, 2);
            tdatos.rows[0].cells[0].valor = "Concepto:";
            tdatos.rows[0].cells[1].valor = new Textarea { id = "txtCONCEPTO", valor = comprobante.com_concepto, obligatorio = true, clase = Css.xxlarge, placeholder = "Concepto", habilitado=habilitado }.ToString();
            tdatos.rows[1].cells[0].valor = "Centro:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtIDCEN", autocomplete = "GetCentroObj", clase = Css.small, valor = centro.cen_id, habilitado = false }.ToString() + " " + new Input { id = "txtCENTRO", clase = Css.large, habilitado = false, valor = centro.cen_nombre}.ToString() + " " + new Input { id = "txtCODCEN", visible = false, valor = centro.cen_codigo}.ToString();             
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span12-->");
            
            
            html.AppendLine("</div><!--row-fluid-->");

         

            //tabs.Add(new Tab("tab1", "Datos Comprobante", html.ToString()));            
            //tabs.Add(new Tab("tab2", "&nbsp;", ""));

            //return new Tabs { id = "tabcomprobante", tabs = tabs }.ToString();
            return html.ToString();



        }





        [WebMethod]
        public static string GetDetalle(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);
            bool habilitado = true;
            if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;

            comprobante.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa={0} and dco_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "dco_secuencia");
            List<Modulo> lstmodulos = ModuloBLL.GetAll("", "");  


            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdinvoice";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";

            tdatos.AddColumn("Id", "width10", "", new Input() { id = "txtIDCUE", placeholder = "CTA", autocomplete = "GetCuentaObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODCUE", visible = false }.ToString());
            tdatos.AddColumn("Cuenta", "width20", "", new Input() { id = "txtCUENTA", placeholder = "DESCRIPCION", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("Cliente/Prov", "width10", "", new Input() { id = "txtIDPER", autocomplete = "GetPersonaObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODPER", visible = false }.ToString());
            tdatos.AddColumn("Nombres", "width20", "", new Input() { id = "txtNOMBRES", placeholder = "CLIENTE/PROVEEDOR", habilitado = false , clase = Css.blocklevel }.ToString());
            tdatos.AddColumn("Modulo", "width10", "", new Input() { id = "txtMODULO", placeholder = "MOD", habilitado=false, clase = Css.blocklevel }.ToString()+ new Input() { id = "txtCODMOD", visible = false }.ToString());            
            tdatos.AddColumn("Débito", "width10", Css.right, new Input() { id = "txtDEBE", placeholder = "DEBE", clase = Css.blocklevel + Css.amount, numeric = true }.ToString());            
            tdatos.AddColumn("Crédito", "width10", Css.right, new Input() { id = "txtHABER", placeholder = "HABER", clase = Css.blocklevel + Css.amount, numeric = true}.ToString());
            tdatos.AddColumn("D/C", "width5", Css.center, new Input() { id = "txtDC", clase = Css.blocklevel, habilitado = false}.ToString());            
            tdatos.AddColumn("", "width5", Css.center, new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());

            
            tdatos.editable = habilitado;

            foreach (Dcontable item in comprobante.contables)
            {

                Modulo mod = lstmodulos.Find(delegate(BusinessObjects.Modulo m) { return m.mod_codigo == item.dco_cuentamodulo; }); 

                HtmlRow row = new HtmlRow();

                row.data = "data-codcue='" + item.dco_cuenta + "' " +
                    " data-codper='" + item.dco_cliente + "' " +
                    " data-codmod='"+mod.mod_codigo+"' " +
                    " data-concepto='" + item.dco_concepto + "' " +
                    " data-idalmacen'" + item.dco_almacenid + "' " +
                    " data-nombrealmacen'" + item.dco_almacennombre + "' " +
                    " data-codalmacen'" + item.dco_almacen + "' " +
                    " data-idcentro'" + item.dco_centroid + "' " +
                    " data-nombrecentro'" + item.dco_centronombre+ "' " +
                    " data-codcentro'" + item.dco_centro+ "' " +
                    " data-idtransacc'" + item.dco_transacc+ "' " +//FALTA EL CAMPO
                    " data-nombretransacc'" + item.dco_transacc+ "' " +//FALTA EL CAMPO
                    " data-codtransacc'" + item.dco_transacc+ "' " +                                        
                    " data-ddocomproba= '"+item.dco_ddo_comproba+"' " +
                    " data-doctran= '" + item.dco_doctran+ "' " +
                    //" data-ddotransacc= '" + item.dco_ddo_transacc+ "' " +
                    " data-nropago= '" + item.dco_nropago+ "' " +
                    " data-fechavence= '" + item.dco_fecha_vence+ "' " +                    
                    "";
                row.removable = true;

                row.cells.Add(new HtmlCell { valor = item.dco_cuentaid });//ID CUENTA
                row.cells.Add(new HtmlCell { valor = item.dco_cuentanombre });//NOMBRE CUENTA
                row.cells.Add(new HtmlCell { valor = item.dco_clienteid});
                row.cells.Add(new HtmlCell { valor = item.dco_clienteapellidos +" " + item.dco_clientenombres });

                

                row.cells.Add(new HtmlCell { valor = mod.mod_id});
                if (item.dco_debcre == Constantes.cDebito)
                {
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.dco_valor_nac), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(0), clase = Css.right });
                }
                else
                {                    
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(0), clase = Css.right });
                    row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.dco_valor_nac), clase = Css.right });
                }
                row.cells.Add(new HtmlCell { valor = item.dco_debcre, clase = Css.center});
                row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString(), clase = Css.center });
                tdatos.AddRow(row);
                //tdatos.AddRow(new HtmlRow(item.ddoc_productoid, item.ddoc_productonombre, item.ddoc_observaciones, item.ddoc_productounidad, item.ddoc_cantidad, item.ddoc_precio, item.ddoc_dscitem, item.ddoc_total, item.ddoc_productoiva) { data = "data-codpro=" + item.ddoc_producto });   

            }

            html.AppendLine(tdatos.ToString());



            return html.ToString();
        }


        [WebMethod]
        public static string GetPie(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            bool habilitado = true;
            if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;

            comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);


            StringBuilder html = new StringBuilder();


            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tddatos";
            tdatos.CreteEmptyTable(7, 2);
            tdatos.rows[0].cells[0].valor = "Concepto:";
            tdatos.rows[0].cells[1].valor = new Textarea() { id = "txtCONCEPTO_D", placeholder = "CONCEPTO", clase= Css.blocklevel, habilitado =habilitado }.ToString();

            tdatos.rows[1].cells[0].valor = "Almacen:";
            tdatos.rows[1].cells[1].valor = new Select() { id = "cmbALMACEN_D", clase = Css.large, diccionario = Dictionaries.GetAlmacen(), habilitado = habilitado }.ToString();  //new Input() { id = "txtIDALM_D", autocomplete = "GetAlmacenObj", placeholder = "ID ALM", clase = Css.mini }.ToString() + " " + new Input() { id = "txtNOMBREALM_D", placeholder = "ALMACEN", clase = Css.medium, habilitado = false }.ToString() + new Input() { id = "txtCODALM_D", visible = false }.ToString();
            tdatos.rows[2].cells[0].valor = "Centro:";
            tdatos.rows[2].cells[1].valor = new Select() { id = "cmbCENTRO_D", clase = Css.large, diccionario = Dictionaries.GetCentro(), habilitado=false }.ToString();//new Input() { id = "txtIDCEN_D", autocomplete = "GetCentroObj", placeholder = "ID CENTRO", clase = Css.mini, habilitado = false }.ToString() + " " + new Input() { id = "txtNOMBRECEN_D", placeholder = "CENTRO", clase = Css.medium, habilitado = false }.ToString() + new Input() { id = "txtCODCEN_D", visible = false }.ToString(); 
            tdatos.rows[3].cells[0].valor = "Transacción:";
            //tdatos.rows[3].cells[1].valor = new Input() { id = "txtIDTRA_D", autocomplete = "GetTransaccionObj", placeholder = "ID TRANS", clase = Css.mini, habilitado=false }.ToString() + " " + new Input() { id = "txtNOMBRETRA_D", placeholder = "TRANSACCION", clase = Css.medium, habilitado = false }.ToString() + new Input() { id = "txtCODTRA_D", visible = false }.ToString();
            tdatos.rows[3].cells[1].valor = new Select() { id = "cmbTRANSACC_D", clase = Css.large, diccionario = Dictionaries.Empty(), habilitado = habilitado }.ToString();

            tdatos.rows[4].cells[0].valor = "Referencia:";
            tdatos.rows[4].cells[1].valor = new Input() { id = "txtREF_D", placeholder = "REFERENCIA", clase = Css.medium, habilitado = habilitado }.ToString();
            tdatos.rows[5].cells[0].valor = "O. Pago:";
            tdatos.rows[5].cells[1].valor = new Input() { id = "txtOPAGO_D", placeholder = "O.PAGO", clase = Css.medium, habilitado = habilitado }.ToString();
            tdatos.rows[6].cells[0].valor = "Cuota /Vence:";
            tdatos.rows[6].cells[1].valor = new Input() { id = "txtNROPAGO_D", placeholder = "NRO PAGO", clase = Css.mini, habilitado = habilitado }.ToString() + " " + new Input() { id = "txtFECHAVENCE_D", datepicker = true, datetimevalor = DateTime.Now, clase = Css.small, habilitado = habilitado }.ToString();
            //tdatos.rows[7].cells[0].valor = "Saldo:";
            //tdatos.rows[7].cells[1].valor = new Input() { id = "txtSALDO_D",  clase = Css.medium }.ToString();

            //tdatos.rows[8].cells[0].valor = "";
            //tdatos.rows[8].cells[1].valor = "";

            html.AppendLine(tdatos.ToString());


            
            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(3, 2);
            
            tdatos1.rows[0].cells[0].valor = "TOTAL DÉBITO:";
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtTOTDEBITO", clase = Css.medium + Css.amount, habilitado = false }.ToString();
            tdatos1.rows[1].cells[0].valor = "TOTAL CRÉDITO:";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtTOTCREDITO", clase = Css.medium + Css.amount, habilitado = false}.ToString();
            tdatos1.rows[2].cells[0].valor = "DIFERENCIA:";
            tdatos1.rows[2].cells[1].valor = new Input { id = "txtDIFERENCIA", clase = Css.medium + Css.amount , habilitado = false}.ToString();
            //tdatos1.rows[3].cells[0].valor = "";
            //tdatos1.rows[3].cells[1].valor = "";

            html.AppendLine(tdatos1.ToString());

            return html.ToString();

        }

        [WebMethod]
        public static string GetModulo(object objeto)
        {
            Modulo mod = new Modulo();            
            if (objeto != null)
            {                
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object empresa = null;
                object cuenta = null;
                
                tmp.TryGetValue("empresa", out empresa);
                tmp.TryGetValue("cuenta", out cuenta);

                Cuenta cta = CuentaBLL.GetByPK(new Cuenta{ cue_empresa = int.Parse(empresa.ToString()) , cue_empresa_key = int.Parse(empresa.ToString())  , cue_codigo= int.Parse(cuenta.ToString()),cue_codigo_key=int.Parse(cuenta.ToString())});
                mod = ModuloBLL.GetByPK(new Modulo{ mod_codigo = cta.cue_modulo.Value, mod_codigo_key= cta.cue_modulo.Value});      
                 
            }
            return new JavaScriptSerializer().Serialize(mod);
        }

        [WebMethod]
        public static string GetTransacc(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object empresa = null;
                object modulo = null;
                object transacc = null;

                tmp.TryGetValue("empresa", out empresa);
                tmp.TryGetValue("modulo", out modulo);
                tmp.TryGetValue("transacc", out transacc);

                if (!string.IsNullOrEmpty(modulo.ToString()))
                    return new Select() { id = "cmbTRANSACC_D", clase = Css.large, diccionario = Dictionaries.GetTransacc(int.Parse(modulo.ToString())), valor= transacc}.ToString();
                else
                    return new Select() { id = "cmbTRANSACC_D", clase = Css.large, diccionario = Dictionaries.Empty() }.ToString();                

            }
            return "";
        }

        [WebMethod]
        public static string GetAlmacen(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object empresa = null;                
                object almacen = null;

                tmp.TryGetValue("empresa", out empresa);                
                tmp.TryGetValue("almacen", out almacen);                
                return new Select() { id = "cmbALMACEN_D", clase = Css.large, diccionario = Dictionaries.GetAlmacen(), valor = almacen}.ToString();


            }
            return "";
        }

        [WebMethod]
        public static string GetCentro(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object empresa = null;
                object centro = null;

                tmp.TryGetValue("empresa", out empresa);
                tmp.TryGetValue("centro", out centro);
                return new Select() { id = "cmbCENTRO_D", clase = Css.large, diccionario = Dictionaries.GetCentro(), valor = centro, habilitado=false}.ToString();


            }
            return "";
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object objetocomp = null;

            tmp.TryGetValue("comprobante", out objetocomp);

            Comprobante obj = new Comprobante(objetocomp);

            try
            {
                if (obj.com_codigo > 0)
                {
                    obj = CNT.update_diario(obj);
                }
                else
                    obj = CNT.save_diario(obj);
                return obj.com_codigo.ToString();                
            }
            catch (Exception ex)
            {
                return "0";
            }


            
          



        }

        public static List<Ddocumento> GetDocumentos(Comprobante obj)
        {
            DateTime fecha = obj.com_fecha;
            List<Ddocumento> lista = new List<Ddocumento>();

            decimal valor = obj.total.tot_total / (decimal)Functions.Conversiones.GetValueByType(obj.total.tot_nro_pagos.Value, typeof(decimal));
            for (int i = 0; i < obj.total.tot_nro_pagos.Value; i++)
            {
                fecha = fecha.AddDays(obj.total.tot_dias_plazo.Value);

                Ddocumento doc = new Ddocumento();
                doc.ddo_empresa = obj.com_empresa;
                doc.ddo_comprobante = obj.com_codigo;
                doc.ddo_transacc = General.GetTransacc(obj.com_tipodoc);
                doc.ddo_doctran = obj.com_doctran;
                doc.ddo_pago = i + 1;
                doc.ddo_codclipro = obj.com_codclipro;
                doc.ddo_debcre = Constantes.cCredito;
                //doc.ddo_tipo_cambio = 
                doc.ddo_fecha_emi = obj.com_fecha;
                doc.ddo_fecha_ven = fecha;
                doc.ddo_monto = valor;
                //doc.ddo_monto_ext = 
                doc.ddo_cancela = 0;
                //doc.ddo_cancela_ext =
                doc.ddo_cancelado = 0;
                doc.ddo_agente = obj.com_agente;
                //doc.ddo_cuenta = 
                doc.ddo_modulo = obj.com_modulo;

                lista.Add(doc);
            }

            return lista;
        }


        public static Comprobante InsertComprobante(Comprobante obj, ref string mensaje)
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

            //obj.com_empresa = 1;
            //obj.com_periodo = fecha.Year;
            //obj.com_tipodoc = 4;
            //obj.com_ctipocom = 2; //FACT

            obj.com_numero = dti.dti_numero.Value;
            obj.com_concepto = obj.com_concepto;
            obj.com_modulo = General.GetModulo(obj.com_tipodoc); ;
            obj.com_transacc = General.GetTransacc(obj.com_tipodoc);

            obj.com_estado = Constantes.cEstadoProceso;
            obj.com_descuadre = 0;
            obj.com_adestino = 0;
            obj.com_doctran = General.GetNumeroComprobante(obj);

            //obj.com_fecha = fecha;


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                obj.com_codigo = ComprobanteBLL.InsertIdentity(transaction, obj);
                //int codigo = ComprobanteBLL.InsertIdentity(transaction, obj);

                
                int contador = 0;
                foreach (Dcontable item in obj.contables)
                {
                    item.dco_empresa = obj.com_empresa;
                    item.dco_comprobante = obj.com_codigo;
                    item.dco_secuencia = contador;
                    DcontableBLL.Insert(transaction, item);
                    contador++;
                }




                /*foreach (Ddocumento item in obj.documentos)
                {
                    item.ddo_comprobante = obj.com_codigo;
                    DdocumentoBLL.Insert(transaction, item);
                    contador++;
                }*/


                DtipocomBLL.Update(transaction, dti);

                transaction.Commit();
                mensaje = "OK";
            }
            catch
            {
                transaction.Rollback();
                mensaje = "ERROR";
            }

            return obj;

        }

        public static string UpdateComprobante(Comprobante obj)
        {

            DateTime fecha = DateTime.Now;


            //obj.com_empresa = 1;
            //obj.com_periodo = fecha.Year;
            //obj.com_tipodoc = 4;
            //obj.com_ctipocom = 2; //FACT

            obj.com_estado = 1;
            //obj.com_anulado =
            //obj.com_fecha = fecha;


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                obj.com_empresa_key = obj.com_empresa;
                obj.com_codigo_key = obj.com_codigo;
                ComprobanteBLL.Update(transaction, obj);

                obj.ccomdoc.cdoc_empresa_key = obj.ccomdoc.cdoc_empresa;
                obj.ccomdoc.cdoc_comprobante_key = obj.ccomdoc.cdoc_comprobante;
                CcomdocBLL.Update(transaction, obj.ccomdoc);

                List<Dcomdoc> lst = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa = {0} and ddoc_comprobante = {1}", obj.com_empresa, obj.com_codigo), "");
                foreach (Dcomdoc item in lst)
                {
                    DcomdocBLL.Delete(transaction, item);
                }

                int contador = 0;
                foreach (Dcomdoc item in obj.ccomdoc.detalle)
                {
                    item.ddoc_empresa = obj.com_empresa;
                    item.ddoc_comprobante = obj.com_codigo;
                    item.ddoc_secuencia = contador;
                    DcomdocBLL.Insert(transaction, item);
                    contador++;
                }


                obj.ccomenv.cenv_empresa_key = obj.ccomenv.cenv_empresa;
                obj.ccomenv.cenv_comprobante_key = obj.ccomenv.cenv_comprobante;
                CcomenvBLL.Update(transaction, obj.ccomenv);

                obj.total.tot_empresa_key = obj.total.tot_empresa;
                obj.total.tot_comprobante_key = obj.total.tot_comprobante;
                TotalBLL.Update(transaction, obj.total);

                transaction.Commit();

            }
            catch
            {
                transaction.Rollback();
                return "ERROR";
            }

            return "OK";
        }


        [WebMethod]
        public static string CloseObject(object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object objetocomp = null;

            tmp.TryGetValue("comprobante", out objetocomp);

            Comprobante obj = new Comprobante(objetocomp);
            //obj.com_estado = Constantes.cEstadoMayorizado;

            try
            {
                if (obj.com_codigo > 0)
                {
                    obj = CNT.update_diario(obj);
                }
                else
                    obj = CNT.save_diario(obj);
                
                obj = CNT.account_diario(obj);
                return obj.com_codigo.ToString();
            }
            catch (Exception ex)
            {
                return "0";
            }


         



        }
    }
}