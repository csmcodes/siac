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
    public partial class wfPlanillaCliente : System.Web.UI.Page
    {

        protected static int pageIndex;
        protected static int pageSize;
        protected static int estado_cancelado=2;
        protected static string OrderByClause = "dcab_nombre";
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
            //Comprobante comprobante = GetComprobanteObj(objeto);
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
            html.AppendLine(new Input { id = "txtFECHA", etiqueta = "Fecha",datepicker = true, datetimevalor = DateTime.Now, clase = Css.small}.ToString());
            html.AppendLine(new Select{ id = "cmbALMACEN", diccionario = Dictionaries.GetAlmacen(), etiqueta = "Almacen", clase = Css.small}.ToString());
            html.AppendLine(new Select { id = "cmbPVENTA", etiqueta = "Punto Venta", diccionario = new Dictionary<string,string>(), clase = Css.small }.ToString());
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
            tdtra.rows[0].cells[0].valor = "Cliente:";
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
         //   tmp.TryGetValue("ruta", out ruta);
            tmp.TryGetValue("empresa", out empresa);
        //    tmp.TryGetValue("vehiculo", out vehiculo);
        //    tmp.TryGetValue("chofer", out chofer);
            tmp.TryGetValue("socio", out socio);
            tmp.TryGetValue("estado_ruta", out estado_ruta);
          tmp.TryGetValue("id", out id);
            tmp.TryGetValue("com_codigo", out com_codigo);
            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (empresa != null && !empresa.Equals(""))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_empresa = {" + contador + "} ";
                valores.Add(empresa);
                contador++;
            }
            if (ruta != null && !ruta.Equals(""))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cenv_ruta = {" + contador + "} ";
                valores.Add(ruta);
                contador++;
            }
            if (vehiculo != null && !vehiculo.Equals(""))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cenv_vehiculo = {" + contador + "} ";
                valores.Add(vehiculo);
                contador++;
            }
            if (chofer != null && !chofer.Equals(""))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cenv_chofer = {" + contador + "} ";
                valores.Add(chofer);
                contador++;
            }
            if (socio != null && !socio.Equals(""))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_codclipro = {" + contador + "} ";
                valores.Add(socio);
                contador++;
            }
            if (factura != null && !factura.Equals(""))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_numero = {" + contador + "} ";
                valores.Add(factura);
                contador++;
            }
            if (estado_ruta != null && !estado_ruta.Equals(""))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_estado <> {" + contador + "} ";
                valores.Add(estado_cancelado);
                contador++;
            }




            parametros.where += ((parametros.where != "") ? " and " : "") + " com_tipodoc = {" + contador + "} ";
                valores.Add(13);
                contador++;
          



            parametros.where += ((parametros.where != "") ? " and " : "") + " (com_codigo+com_empresa) not in (select plc_comprobante+plc_empresa from planillacli)";
          
            valores.Add(2);
            contador++;
            parametros.valores = valores.ToArray();
            int codigoruta = (int)com_codigo ;
            List<vPlanillas> lstpersona = new List<vPlanillas>();
            if (codigoruta > 0)
            {
               /* List<Rutaxfactura> lstruta = RutaxfacturaBLL.GetAll("rfac_comprobanteruta = " + codigoruta, "");
                foreach (Rutaxfactura item in lstruta)
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
                lstpersona = vPlanillasBLL.GetAll(parametros, "");
            }
            if (lstpersona.Count() > 0)
            {
                HtmlTable tdatos = new HtmlTable();
                tdatos.id = id.ToString();                
                tdatos.invoice = true;
                tdatos.AddColumn("emision", "width10", "", "");              
                tdatos.AddColumn("documento", "width10", "", "");               
                tdatos.AddColumn("Socio", "width10", "", "");               
                tdatos.AddColumn("Total", "width10", "", "");
                tdatos.editable = false;
                foreach (vPlanillas item in lstpersona)
                {
                    HtmlRow row = new HtmlRow();
                   
                    row.data = "data-codpro=" + item.com_codigo ;
                    row.removable = true;
                    row.markable = true;
                    row.cells.Add(new HtmlCell { valor = item.com_fecha });                    
                    row.cells.Add(new HtmlCell { valor = item.com_doctran });                                  
                    row.cells.Add(new HtmlCell { valor = item.per_nombres + " " + item.per_apellidos });
                    row.cells.Add(new HtmlCell { valor = item.tot_total });
                   
                    if (tdatos.id.Equals("tdatos"))
                        tdatos.AddRow(row);
                    else
                    {
                        List<Rutaxfactura> lstruta = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobantefac={0} and rfac_comprobanteruta={1}", item.com_codigo, com_codigo), "");
                        if (lstruta.Count>0)
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
                tdatos.AddColumn("emision", "width10", "", "");
                tdatos.AddColumn("documento", "width10", "", "");
                tdatos.AddColumn("Socio", "width10", "", "");
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
            html2.AppendLine("<div class=\"span4\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdatos";
            tdatos.titulo = "Comprobantes No Asignados";
            tdatos.invoice = true;
            tdatos.AddColumn("emision", "width10", "", "");
            tdatos.AddColumn("documento", "width10", "", "");
            tdatos.AddColumn("Socio", "width10", "", "");
            tdatos.AddColumn("Total", "width10", "", "");

            /*     tdatos.AddColumn("Remitente", "width10", "", new Input() { id = "txtPRODUCTO", placeholder = "REMITENTE", clase = Css.blocklevel, habilitado = false }.ToString());
                 tdatos.AddColumn("Destinatario", "width10", "", new Textarea() { id = "txtOBSERVACION", placeholder = "DESTINACION", clase = Css.blocklevel }.ToString());
                 tdatos.AddColumn("Total", "width10", "", new Select() { id = "cmbUMEDIDA", placeholder = "TOTAL", diccionario = Dictionaries.GetUmedida(), clase = Css.blocklevel }.ToString() + new Input() { id = "txtFACTOR", visible = false });           
            
             */
            tdatos.editable = false;

            List<vPlanillas> lstpersona = vPlanillasBLL.GetAll(new WhereParams("com_empresa={0} and  com_estado <>{1}   and  com_tipodoc={2} and (com_codigo+com_empresa) not in (select plc_comprobante+plc_empresa from planillacli)", comprobante.com_empresa, estado_cancelado,13), "");
            foreach (vPlanillas item in lstpersona)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-codpro=" + item.com_codigo;
                row.removable = true;
                row.markable = true;
                row.cells.Add(new HtmlCell { valor = item.com_fecha });
                row.cells.Add(new HtmlCell { valor = item.com_doctran });
                row.cells.Add(new HtmlCell { valor = item.per_nombres + " " + item.per_apellidos });
                row.cells.Add(new HtmlCell { valor = item.tot_total });
                tdatos.AddRow(row);
                //tdatos.AddRow(new HtmlRow(item.ddoc_productoid, item.ddoc_productonombre, item.ddoc_observaciones, item.ddoc_productounidad, item.ddoc_cantidad, item.ddoc_precio, item.ddoc_dscitem, item.ddoc_total, item.ddoc_productoiva) { data = "data-codpro=" + item.ddoc_producto });   

            }
            html2.AppendLine(tdatos.ToString());
            html2.AppendLine(" </div><!--span4-->");


            html2.AppendLine("<div class=\"span4\">");
            html2.AppendLine(new Boton { id = "agregar", click = "MoveRigth();return false;", valor = "> " }.ToString());
            html2.AppendLine(new Boton { id = "quitar", click = "MoveLeft();return false;", valor = "<" }.ToString());
            html2.AppendLine(" </div><!--span2-->");


     
            html2.AppendLine("<div class=\"span4\">");
            HtmlTable tdes = new HtmlTable();
            tdes.id = "tdes";
            tdes.titulo = "Comprobantes Asignados";
            tdes.invoice = true;
            /*      tdes.AddColumn("Numero", "width10", "", "");
                  tdes.AddColumn("Remitente", "width10", "", new Input() { id = "txtPRODUCTO", placeholder = "REMITENTE", clase = Css.blocklevel, habilitado = false }.ToString());
                  tdes.AddColumn("Destinatario", "width10", "", new Textarea() { id = "txtOBSERVACION", placeholder = "DESTINACION", clase = Css.blocklevel }.ToString());
                  tdes.AddColumn("Total", "width10", "", new Select() { id = "cmbUMEDIDA", placeholder = "TOTAL", diccionario = Dictionaries.GetUmedida(), clase = Css.blocklevel }.ToString() + new Input() { id = "txtFACTOR", visible = false });
      */
            tdatos.AddColumn("emision", "width10", "", "");
            tdatos.AddColumn("documento", "width10", "", "");
            tdatos.AddColumn("Socio", "width10", "", "");
            tdatos.AddColumn("Total", "width10", "", "");


            tdes.editable = false;
            html2.AppendLine(tdes.ToString());
            html2.AppendLine(" </div><!--span5-->");
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
            List<Politica> lst = PoliticaBLL.GetAll("", "");               
            HtmlTable tpoliticas = new HtmlTable();
            

          
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
            return new JavaScriptSerializer().Serialize(product );            
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

            foreach (Planillacli item in obj.planillas)
            {
                Total ddocumento = new Total();
                ddocumento.tot_comprobante = item.plc_comprobante;
                ddocumento.tot_empresa = item.plc_empresa;

                ddocumento.tot_comprobante_key = item.plc_comprobante;
                ddocumento.tot_empresa_key = item.plc_empresa;

                ddocumento = TotalBLL.GetByPK(ddocumento);
                obj.total.tot_total += (ddocumento.tot_total);
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

            Persona per = PersonaBLL.GetByPK(new Persona { per_empresa = obj.com_empresa, per_empresa_key = obj.com_empresa, per_codigo = obj.com_codclipro.Value, per_codigo_key = obj.com_codclipro.Value });  

            obj.com_numero = dti.dti_numero.Value;
            obj.com_concepto = "PLANILLA CLIENTE " + per.per_apellidos + " " + per.per_nombres;
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
                int contador = 0;
                foreach (Planillacli item in obj.planillas)
                {
                    item.plc_comprobante_pla = codigo;
                    item.plc_comprobante_pla_key = codigo;
                  
                    PlanillacliBLL.Insert(transaction, item);
                    contador++;
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

            obj.com_empresa_key = obj.com_empresa;
            obj.com_codigo_key = obj.com_codigo;
            Comprobante objU = ComprobanteBLL.GetByPK(obj);
            objU.com_empresa_key = objU.com_empresa;
            objU.com_codigo_key = objU.com_codigo;

            Persona per = PersonaBLL.GetByPK(new Persona { per_empresa = obj.com_empresa, per_empresa_key = obj.com_empresa, per_codigo = obj.com_codclipro.Value, per_codigo_key = obj.com_codclipro.Value });

            objU.com_fecha = obj.com_fecha;
            objU.com_periodo = obj.com_fecha.Year;
            objU.com_codclipro = obj.com_codclipro;
            objU.com_agente = obj.com_agente;
            objU.mod_usr = obj.mod_usr;
            objU.mod_fecha = obj.mod_fecha;
            objU.com_concepto = !string.IsNullOrEmpty(obj.com_concepto) ? obj.com_concepto : "PLANILLA CLIENTE " + per.per_apellidos + " " + per.per_nombres;
            objU.com_estado = obj.com_estado;//ACTUALIZA EL ESTADO DEL COMPROBANTE

            Dtipocom dti = new Dtipocom();
            if (string.IsNullOrEmpty(objU.com_doctran))
            {
                objU.com_modulo = General.GetModulo(obj.com_tipodoc); ;
                objU.com_transacc = General.GetTransacc(obj.com_tipodoc);
                objU.com_nocontable = 1;//HAY QUE DEFINIR
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
            Total totaldel = TotalBLL.GetByPK(new Total { tot_empresa = obj.com_empresa, tot_empresa_key = obj.com_empresa, tot_comprobante = obj.com_codigo, tot_comprobante_key = obj.com_codigo });

            
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                ComprobanteBLL.Update(transaction, objU);

                obj.total.tot_empresa_key = obj.total.tot_empresa;
                obj.total.tot_comprobante_key = obj.total.tot_comprobante;

                TotalBLL.Delete(transaction, totaldel);
                TotalBLL.Insert(transaction, obj.total);
                List<Planillacli> lstrutaxfact = PlanillacliBLL.GetAll(new WhereParams("plc_comprobante_pla = {0} ", obj.com_codigo), "");
                foreach (Planillacli item in lstrutaxfact)
                {
                    PlanillacliBLL.Delete(transaction, item);
                }
                transaction.Commit();
                transaction = new BLL();
                transaction.CreateTransaction();
                transaction.BeginTransaction();

                int contador = 0;
                foreach (Planillacli item in obj.planillas)
                {
                    item.plc_comprobante_pla = obj.com_codigo;
                    item.plc_comprobante_pla_key =obj.com_codigo;
                   
                    PlanillacliBLL.Insert(transaction, item);
                    contador++;
                }
                if (dti.dti_numero.HasValue)
                    DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE
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