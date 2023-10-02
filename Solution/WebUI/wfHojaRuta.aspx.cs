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
    public partial class wfHojaRuta : System.Web.UI.Page
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
                txtcodigocomp.Text = (Request.QueryString["codigocomp"] != null) ? Request.QueryString["codigocomp"].ToString() : "-1";
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


            Vehiculo vehiculo = new Vehiculo();
            vehiculo.veh_empresa = comprobante.com_empresa;
            vehiculo.veh_empresa_key = comprobante.com_empresa;
            if (comprobante.com_vehiculo.HasValue)
            {
                vehiculo.veh_codigo = comprobante.com_vehiculo.Value;
                vehiculo.veh_codigo_key = comprobante.com_vehiculo.Value;
                vehiculo = VehiculoBLL.GetByPK(vehiculo);
            }



            Ruta ruta = new Ruta();
            ruta.rut_empresa = comprobante.com_empresa;
            ruta.rut_empresa_key = comprobante.com_empresa;
            if (comprobante.com_ruta.HasValue)
            {
                ruta.rut_codigo = comprobante.com_ruta.Value;
                ruta.rut_codigo_key = comprobante.com_ruta.Value;
                ruta = RutaBLL.GetByPK(ruta);
            }


            Persona socio = new Persona();
            socio.per_empresa = comprobante.com_empresa;
            socio.per_empresa_key = comprobante.com_empresa;
            if (comprobante.com_vehiculo.HasValue)
            {
                socio.per_codigo = vehiculo.veh_duenio.Value;
                socio.per_codigo_key = vehiculo.veh_duenio.Value;
                socio = PersonaBLL.GetByPK(socio);
            }


            Persona chofer = new Persona();
            chofer.per_empresa = comprobante.com_empresa;
            chofer.per_empresa_key = comprobante.com_empresa;
            if (comprobante.com_vehiculo.HasValue)
            {
                chofer.per_codigo = vehiculo.veh_chofer1.Value;
                chofer.per_codigo_key = vehiculo.veh_chofer1.Value;
                chofer = PersonaBLL.GetByPK(chofer);
            }

            Persona chofer2 = new Persona();
            chofer2.per_empresa = comprobante.com_empresa;
            chofer2.per_empresa_key = comprobante.com_empresa;
            if (comprobante.com_vehiculo.HasValue)
            {
                chofer2.per_codigo = vehiculo.veh_chofer2.Value;
                chofer2.per_codigo_key = vehiculo.veh_chofer2.Value;
                chofer2 = PersonaBLL.GetByPK(chofer2);
            }



            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Vehiculo:";
           
            if (vehiculo.veh_id==null)
                tdatos.rows[0].cells[1].valor = new Input { id = "txtIDVEH", valor = vehiculo.veh_id, autocomplete = "GetvVehiculoObj", clase = Css.small, obligatorio = true }.ToString() + " " + new Input { id = "txtPLACA", valor = null, clase = Css.large, habilitado = false, }.ToString() + " " + new Input { id = "txtCODVEHICULO", valor = vehiculo.veh_codigo, visible = false }.ToString();
            else
                 tdatos.rows[0].cells[1].valor = new Input { id = "txtIDVEH", valor = vehiculo.veh_id, autocomplete = "GetvVehiculoObj", clase = Css.small, obligatorio = true }.ToString() + " " + new Input { id = "txtPLACA", valor = "Placa: "+vehiculo.veh_placa+" Disco: "+vehiculo.veh_disco, clase = Css.large, habilitado = false, }.ToString() + " " + new Input { id = "txtCODVEHICULO", valor = vehiculo.veh_codigo, visible = false }.ToString();
            tdatos.rows[1].cells[0].valor = "Socio:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtIDSOC", valor = socio.per_id, clase = Css.small, habilitado = false }.ToString() + " " + new Input { id = "txtSOCIO", valor = socio.per_nombres + " " + socio.per_apellidos, clase = Css.large, habilitado = false }.ToString() + new Input { id = "txtCODSOCIO", valor = socio.per_codigo, visible = false }.ToString();
            tdatos.rows[2].cells[0].valor = "Chofer:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtIDCHO", valor = chofer.per_id, clase = Css.small, autocomplete = "GetClienteObj" }.ToString() + " " + new Input { id = "txtCHOFER", clase = Css.large, obligatorio = true, valor = chofer.per_nombres + " " + chofer.per_apellidos, habilitado = false }.ToString() + new Input { id = "txtCODCHOFER", valor = chofer.per_codigo, visible = false }.ToString() + new Boton { small = true, id = "btncallcho", tooltip = "Agregar Chofer", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cCliente + ")" }.ToString() + new Boton { small = true, id = "btncleancho", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");

            HtmlTable tddes = new HtmlTable();
            tddes.CreteEmptyTable(3, 2);
            tddes.rows[0].cells[0].valor = "Hora Salida:";
            tddes.rows[0].cells[1].valor = new Input { id = "txtHOURSAL", valor = "" + DateTime.Now.Hour + ":" + DateTime.Now.Minute, clase = Css.mini, obligatorio = true }.ToString();
            tddes.rows[1].cells[0].valor = "Ruta:";

            tddes.rows[1].cells[1].valor = new Select { id = "cmbRUTA", clase = Css.medium, diccionario = Dictionaries.GetRutaByAlmacen(comprobante.com_empresa, comprobante.com_almacen.Value), valor = ruta.rut_codigo}.ToString() + new Boton { small = true, id = "btnallruta", tooltip = "Todas las rutas", clase = "iconsweets-refresh", click = "GetAllRutas()" }.ToString();
            //tddes.rows[1].cells[1].valor = new Select { id = "cmbRUTA", valor = ruta.rut_id, autocomplete = "GetRutaObj", clase = Css.small, obligatorio = true }.ToString() + " " + new Input { id = "txtNOMBRERUT", valor = null, clase = Css.large, habilitado = false }.ToString() + new Input { id = "txtCODRUTA", valor = ruta.rut_codigo, visible = false }.ToString();


            /*if (ruta.rut_id == null)
                tddes.rows[1].cells[1].valor = new Input { id = "txtIDRUT", valor = ruta.rut_id, autocomplete = "GetRutaObj", clase = Css.small, obligatorio = true }.ToString() + " " + new Input { id = "txtNOMBRERUT", valor =null, clase = Css.large, habilitado = false }.ToString() + new Input { id = "txtCODRUTA", valor = ruta.rut_codigo, visible = false }.ToString();
            else
                tddes.rows[1].cells[1].valor = new Input { id = "txtIDRUT", valor = ruta.rut_id, autocomplete = "GetRutaObj", clase = Css.small, obligatorio = true }.ToString() + " " + new Input { id = "txtNOMBRERUT", valor = ruta.rut_nombre + ", " + ruta.rut_origen + " - " + ruta.rut_destino, clase = Css.large, habilitado = false }.ToString() + new Input { id = "txtCODRUTA", valor = ruta.rut_codigo, visible = false }.ToString();*/


           
            tddes.rows[2].cells[0].valor = "Observacion:";
            tddes.rows[2].cells[1].valor = new Textarea() { id = "txtCONCEPTO", placeholder = "OBSERVACIÓN", clase = Css.blocklevel }.ToString();            
            html.AppendLine(tddes.ToString());
            html.AppendLine(" </div><!--span6-->");

            html.AppendLine("</div><!--row-fluid-->");
            html.AppendLine(new Input { id = "txtESTADO", valor = comprobante.com_estado, visible = false }.ToString());
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
            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tddatos";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";
            tdatos.AddColumn("Comprobante", "width10", "", "");
            tdatos.AddColumn("Remitente", "width20", "", "");
            tdatos.AddColumn("Destinatario", "width20", "", "");
            tdatos.AddColumn("Valor", "width5", "");   
            tdatos.editable = false;
            List<vHojadeRuta> lst = vHojadeRutaBLL.GetAll(new WhereParams("cabecera.com_codigo={0}", comprobante.com_codigo), "cabecera.com_fecha");
            foreach (vHojadeRuta item in lst)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-comprobante=" + item.codigodetalle;
                if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                    row.markable = true;
                row.cells.Add(new HtmlCell { valor = new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.codigodetalle + ")" }.ToString() + " " + item.doctrandetalle });
                row.cells.Add(new HtmlCell { valor = item.nombreremitente });
                row.cells.Add(new HtmlCell { valor = item.apellidoremitente });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.totaldetalle), clase = Css.right }); 
                tdatos.AddRow(row);
            }
            html.AppendLine(tdatos.ToString());
            return html.ToString();
        }


        [WebMethod]
        public static string GetDetalleAct(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            
            WhereParams parametros = new WhereParams("cabecera.com_codigo={0}", comprobante.com_codigo);            
            foreach (Rutaxfactura rxf in comprobante.rutafactura)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " detalle.com_codigo != " + rxf.rfac_comprobantefac;
            }

            List<vHojadeRuta> lst = vHojadeRutaBLL.GetAll(parametros, "cabecera.com_fecha");
            
            StringBuilder html = new StringBuilder();
            
            foreach (vHojadeRuta item in lst)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-comprobante=" + item.codigodetalle;
                if (comprobante.com_estado != Constantes.cEstadoMayorizado)
                    row.markable = true;
                row.cells.Add(new HtmlCell { valor = new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.codigodetalle + ")" }.ToString() + " " + item.doctrandetalle });
                row.cells.Add(new HtmlCell { valor = item.nombreremitente });
                row.cells.Add(new HtmlCell { valor = item.apellidoremitente });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.totaldetalle), clase = Css.right });
                html.AppendLine(row.ToString());
            }            
            return html.ToString();
        }
         [WebMethod]
        public static string GetPorcentaje(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);            
            Ruta ruta = new Ruta();
            ruta.rut_codigo_key = comprobante.com_ruta ?? 0;
            ruta.rut_empresa_key = comprobante.com_empresa;
            ruta = RutaBLL.GetByPK(ruta);


            return new JavaScriptSerializer().Serialize(ruta);

        }
         [WebMethod]
         public static string GetAllRutas()
         {
             return new Select { id = "cmbRUTA", clase = Css.medium, diccionario = Dictionaries.GetRuta() }.ToString();
         }


         [WebMethod]
         public static string GetGuiasNumero()
         {
             
             StringBuilder html = new StringBuilder();
             html.AppendLine("<div id=\"barracomp\">");
             html.AppendLine("<ul class=\"list-nostyle list-inline\">");
             html.AppendFormat("<li>Fecha: {0}</li>", new Input { id = "txtFECHA_GUI", datepicker = true, datetimevalor = DateTime.Now, clase = Css.small }.ToString());
             html.AppendFormat("<li>Numero: {0}</li>", new Input { id = "txtNUMERO_GUI", clase = Css.small }.ToString());
             html.AppendLine("<li><div class=\"btn\" id=\"alldet_P\"><i class=\"iconfa-check\"></i> &nbsp; Seleccionar Todos</div></li>");
             html.AppendLine("<li><div class=\"btn\" id=\"nonedet_P\"><i class=\"iconfa-check-empty\"></i> &nbsp; Limpiar Selección</div></li>");
             html.AppendLine("</ul>");
             html.AppendLine("</div>");
             html.AppendLine("<div class=\"row-fluid\">");
             html.AppendLine("<div id='detallegui' class=\"span11\">");
             html.AppendLine(" </div><!--span11-->");
             html.AppendLine("</div><!--row-fluid-->");


             return html.ToString();
         }

         public static void SetWhereClauseNumero(vComprobante obj, Usuario usr, int tipodocfac, int tipodocgui)
         {
             int contador = 0;
             parametros = new WhereParams();
             List<object> valores = new List<object>();
             if (!string.IsNullOrEmpty(obj.crea_usr))
             {
                 parametros.where += ((parametros.where != "") ? " and " : "") + " c.crea_usr = {" + contador + "} ";
                 valores.Add(obj.crea_usr);
                 contador++;
             }

             if (obj.periodo.HasValue)
             {
                 if (obj.periodo.Value > 0)
                 {
                     parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_periodo = {" + contador + "} ";
                     valores.Add(obj.periodo.Value);
                     contador++;
                 }
             }
             if (obj.mes.HasValue)
             {
                 if (obj.mes.Value > 0)
                 {
                     parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_mes = {" + contador + "} ";
                     valores.Add(obj.mes);
                     contador++;
                 }
             }
             if (obj.ctipocom.HasValue)
             {
                 if (obj.ctipocom.Value > 0)
                 {
                     parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_ctipocom = {" + contador + "} ";
                     valores.Add(obj.ctipocom);
                     contador++;
                 }
             }
             if (obj.almacen.HasValue)
             {
                 if (obj.almacen.Value > 0)
                 {
                     parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                     valores.Add(obj.almacen);
                     contador++;
                 }
             }
             if (obj.pventa.HasValue)
             {
                 if (obj.pventa.Value > 0)
                 {
                     parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                     valores.Add(obj.pventa);
                     contador++;
                 }
             }
             if (obj.numero.HasValue)
             {
                 parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_numero = {" + contador + "} ";
                 valores.Add(obj.numero);
                 contador++;
             }
             if (obj.estado.HasValue)
             {
                 if (obj.estado.Value > -1)
                 {
                     parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
                     valores.Add(obj.estado);
                     contador++;
                 }

             }
             if (obj.tipodoc.HasValue)
             {
                 parametros.where += ((parametros.where != "") ? " and " : "") + " (c.com_tipodoc = 13 or c.com_tipodoc = 4) ";
             }

             if (!string.IsNullOrEmpty(obj.politica))
             {
                 parametros.where += ((parametros.where != "") ? " and " : "") + " pol_nombre LIKE {" + contador + "} ";
                 valores.Add("%" + obj.politica + "%");
                 contador++;
             }
             /*if (obj.com_aut_tipo > 0)
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
             }*/
             if (!string.IsNullOrEmpty(obj.concepto))
             {
                 parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_concepto like {" + contador + "} ";
                 valores.Add("%" + obj.concepto + "%");
                 contador++;
             }
             /*if (obj.com_ref_comprobante > 0)
             {
                 parametros.where += ((parametros.where != "") ? " and " : "") + " com_ref_comprobante = {" + contador + "} ";
                 valores.Add(obj.com_ref_comprobante);
                 contador++;
             }*/
             if (obj.fecha.Value > DateTime.MinValue)
             {
                 parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
                 valores.Add(obj.fecha);
                 contador++;
                 parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
                 valores.Add(obj.fecha.Value.AddDays(1));
                 contador++;
             }

             if (!string.IsNullOrEmpty(obj.nombres))//CLIENTE O PROVEEDOR
             {

                 //parametros.where += ((parametros.where != "") ? " and " : "") + " (p.per_ciruc ILIKE {" + contador + "} or p.per_nombres ILIKE {" + contador + "} or p.per_apellidos ILIKE{" + contador + "}) ";
                 parametros.where += ((parametros.where != "") ? " and " : "") + " (p.per_ciruc ILIKE {" + contador + "} or p.per_nombres ILIKE {" + contador + "} or p.per_apellidos ILIKE{" + contador + "} or e.cenv_ciruc_rem ILIKE {" + contador + "} or e.cenv_nombres_rem ILIKE {" + contador + "} or e.cenv_apellidos_rem ILIKE{" + contador + "} or e.cenv_ciruc_des ILIKE {" + contador + "} or e.cenv_nombres_des ILIKE {" + contador + "} or e.cenv_apellidos_des ILIKE{" + contador + "} or p1.per_apellidos ILIKE {" + contador + "} or p1.per_nombres ILIKE{" + contador + "}) ";
                 valores.Add("%" + obj.nombres + "%");
                 contador++;
             }

             //if (obj.total.HasValue)
             //{                
             //    parametros.where += ((parametros.where != "") ? " and " : "") + " tot_total "+obj.operador+" {" + contador + "} ";
             //    valores.Add(obj.total);
             //    contador++;
             //}

             if (!string.IsNullOrEmpty(obj.estadoenvio))//ESTADO ENVIO
             {
                 if (obj.estadoenvio == "1") //POR COBRAR
                 {
                     parametros.where += ((parametros.where != "") ? " and " : "") + " SUM(ddo_monto) > SUM(ddo_cancela) and c.com_tipodoc= " + tipodocfac;
                 }
                 if (obj.estadoenvio == "2") //POR DESPACHAR
                 {
                     parametros.where += ((parametros.where != "") ? " and " : "") + " SUM(ddo_monto) = SUM(ddo_cancela) and e.cenv_despachado_ret IS NULL";
                 }
                 if (obj.estadoenvio == "3")//DESPACHADOS
                 {
                     parametros.where += ((parametros.where != "") ? " and " : "") + " e.cenv_despachado_ret = 1 ";
                 }
             }

             if (!string.IsNullOrEmpty(obj.nombres_rem)) //REMITENTE
             {
                 parametros.where += ((parametros.where != "") ? " and " : "") + " (e.cenv_ciruc_rem ILIKE {" + contador + "} or e.cenv_nombres_rem ILIKE {" + contador + "} or e.cenv_apellidos_rem ILIKE{" + contador + "}) ";
                 valores.Add("%" + obj.nombres_rem + "%");
                 contador++;
             }

             if (!string.IsNullOrEmpty(obj.nombres_des)) //DESTINATARIO
             {
                 parametros.where += ((parametros.where != "") ? " and " : "") + " (e.cenv_ciruc_des ILIKE {" + contador + "} or e.cenv_nombres_des ILIKE {" + contador + "} or e.cenv_apellidos_des ILIKE{" + contador + "}) ";
                 valores.Add("%" + obj.nombres_des + "%");
                 contador++;
             }
             if (!string.IsNullOrEmpty(obj.nombres_soc)) //SOCIO
             {
                 parametros.where += ((parametros.where != "") ? " and " : "") + " (p1.per_apellidos ILIKE {" + contador + "} or p1.per_nombres ILIKE{" + contador + "}) ";
                 valores.Add("%" + obj.nombres_soc + "%");
                 contador++;
             }

             if (!string.IsNullOrEmpty(obj.placa)) //VEHICULO
             {
                 parametros.where += ((parametros.where != "") ? " and " : "") + " (e.cenv_placa ILIKE {" + contador + "} or e.cenv_disco ILIKE {" + contador + "}) ";
                 valores.Add("%" + obj.placa + "%");
                 contador++;
             }

             /*if (obj.ruta.HasValue) //RUTA
             {
                 parametros.where += ((parametros.where != "") ? " and " : "") + " (e.cenv_ruta = {" + contador + "}) ";
                 valores.Add(obj.ruta);
                 contador++;
             }*/

             if (!string.IsNullOrEmpty(usr.usr_id))
             {
                 parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc IN (SELECT udo_tipodoc FROM usrdoc WHERE udo_usuario = {" + contador + "}) ";
                 valores.Add(usr.usr_id);
                 contador++;
             }

             parametros.valores = valores.ToArray();
         }
         [WebMethod]
         public static string GetGuiasDataNumero(object objeto)
         {

             Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
             object comprob = null;
             object filtros = null;
             tmp.TryGetValue("comprob", out comprob);
             tmp.TryGetValue("filtros", out filtros);


             Comprobante comp = new Comprobante(comprob);



             int tipodocfac = Constantes.cFactura.tpd_codigo;
             int tipodocgui = Constantes.cGuia.tpd_codigo;
             SetWhereClauseNumero(new vComprobante(filtros), new Usuario(filtros), tipodocfac, tipodocgui);
             int desde = 0;
             int hasta = 20;
             pageIndex++;

             string whereguias = "";
             foreach (Rutaxfactura rxf in comp.rutafactura)
             {
                 parametros.where += ((parametros.where != "") ? " and " : "") + " com_codigo != " + rxf.rfac_comprobantefac;
             }


             List<vComprobante> lista = vComprobanteBLL.GetAllByPage(parametros, "t.com_fecha DESC", desde, hasta);




             // List<Ccomenv> lst = CcomenvBLL.GetAll(new WhereParams("cenv_empresa={0} and  cenv_ruta ={1} and cenv_estado_ruta={2} and cenv_socio ={2}" + whereguias, comprobante.com_empresa, comprobante.com_ruta, 0), "com_fecha");
             StringBuilder html = new StringBuilder();

             if (lista.Count > 0)
             {

                 HtmlTable tdatos = new HtmlTable();

                 tdatos.id = "tddatos_P";
                 tdatos.invoice = true;

                 tdatos.AddColumn("Comprobante", "", "", "");
                 tdatos.AddColumn("Remitente", "", "", "");
                 tdatos.AddColumn("Destinatario", "", "", "");
                 tdatos.AddColumn("Valor", "", "");

                 //tdatos.editable = true;
                 //foreach (Ccomenv item in lst)
                 foreach (vComprobante item in lista)
                 {
                     HtmlRow row = new HtmlRow();
                     row.markable = true;
                     row.data = "data-comprobante=" + item.codigo;
                     //row.cells.Add(new HtmlCell { valor = "<a href='#' onclick='ViewComprobante(" + item.cenv_comprobante + ");' >" + item.cenv_doctran + "</a>" });
                     row.cells.Add(new HtmlCell { valor = new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.codigo + ")" }.ToString() + " " + item.doctran });
                     row.cells.Add(new HtmlCell { valor = item.nombres_rem });
                     row.cells.Add(new HtmlCell { valor = item.nombres_des });
                     row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.total), clase = Css.right });

                     tdatos.AddRow(row);

                 }

                 html.AppendLine(tdatos.ToString());
             }
             else
             {
                 html.AppendLine("El comprobante indicado no existe o se encuentra ya asignado a una hoja de ruta");
             }

             return html.ToString();




         }


         





         [WebMethod]
        public static string GetGuias(object objeto)
        {   
            Comprobante comprobante = new Comprobante(objeto);            
            


            StringBuilder html = new StringBuilder();
            html.AppendLine("<div id=\"barracomp\">");
            html.AppendLine("<ul class=\"list-nostyle list-inline\">");
            html.AppendFormat("<li>Fecha: {0}</li>", new Input { id="txtFECHA_GUI", datepicker= true, datetimevalor= DateTime.Now, clase =Css.small}.ToString());
            html.AppendFormat("<li>Numero: {0}</li>",new Input{id="txtNUMERO_GUI", clase=Css.small}.ToString());
            html.AppendLine("<li><div class=\"btn\" id=\"alldet_P\"><i class=\"iconfa-check\"></i> &nbsp; Seleccionar Todos</div></li>");
            html.AppendLine("<li><div class=\"btn\" id=\"nonedet_P\"><i class=\"iconfa-check-empty\"></i> &nbsp; Limpiar Selección</div></li>");
            html.AppendLine("</ul>");
            html.AppendLine("</div>");




            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div id='detallegui' class=\"span11\">");

           

            html.AppendLine(" </div><!--span11-->");
            html.AppendLine("</div><!--row-fluid-->");


            return html.ToString();
        }

        public static void SetWhereClause(vComprobante obj, Usuario usr, int tipodocfac, int tipodocgui)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (!string.IsNullOrEmpty(obj.crea_usr))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.crea_usr = {" + contador + "} ";
                valores.Add(obj.crea_usr);
                contador++;
            }

            if (obj.periodo.HasValue)
            {
                if (obj.periodo.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_periodo = {" + contador + "} ";
                    valores.Add(obj.periodo.Value);
                    contador++;
                }
            }
            if (obj.mes.HasValue)
            {
                if (obj.mes.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_mes = {" + contador + "} ";
                    valores.Add(obj.mes);
                    contador++;
                }
            }
            if (obj.ctipocom.HasValue)
            {
                if (obj.ctipocom.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_ctipocom = {" + contador + "} ";
                    valores.Add(obj.ctipocom);
                    contador++;
                }
            }
            if (obj.almacen.HasValue)
            {
                if (obj.almacen.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                    valores.Add(obj.almacen);
                    contador++;
                }
            }
            if (obj.pventa.HasValue)
            {
                if (obj.pventa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                    valores.Add(obj.pventa);
                    contador++;
                }
            }
            if (obj.numero.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_numero = {" + contador + "} ";
                valores.Add(obj.numero);
                contador++;
            }
            if (obj.estado.HasValue)
            {
                if (obj.estado.Value > -1)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
                    valores.Add(obj.estado);
                    contador++;
                }

            }
            if (obj.tipodoc.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (c.com_tipodoc = 13 or c.com_tipodoc = 4) ";                
            }

            if (!string.IsNullOrEmpty(obj.politica))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " pol_nombre LIKE {" + contador + "} ";
                valores.Add("%" + obj.politica + "%");
                contador++;
            }
            /*if (obj.com_aut_tipo > 0)
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
            }*/
            if (!string.IsNullOrEmpty(obj.concepto))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_concepto like {" + contador + "} ";
                valores.Add("%" + obj.concepto + "%");
                contador++;
            }
            /*if (obj.com_ref_comprobante > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_ref_comprobante = {" + contador + "} ";
                valores.Add(obj.com_ref_comprobante);
                contador++;
            }*/
            if (obj.fecha.Value > DateTime.MinValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
                valores.Add(obj.fecha);
                contador++;
                parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
                valores.Add(obj.fecha.Value.AddDays(1));
                contador++;
            }

            if (!string.IsNullOrEmpty(obj.nombres))//CLIENTE O PROVEEDOR
            {

                //parametros.where += ((parametros.where != "") ? " and " : "") + " (p.per_ciruc ILIKE {" + contador + "} or p.per_nombres ILIKE {" + contador + "} or p.per_apellidos ILIKE{" + contador + "}) ";
                parametros.where += ((parametros.where != "") ? " and " : "") + " (p.per_ciruc ILIKE {" + contador + "} or p.per_nombres ILIKE {" + contador + "} or p.per_apellidos ILIKE{" + contador + "} or e.cenv_ciruc_rem ILIKE {" + contador + "} or e.cenv_nombres_rem ILIKE {" + contador + "} or e.cenv_apellidos_rem ILIKE{" + contador + "} or e.cenv_ciruc_des ILIKE {" + contador + "} or e.cenv_nombres_des ILIKE {" + contador + "} or e.cenv_apellidos_des ILIKE{" + contador + "} or p1.per_apellidos ILIKE {" + contador + "} or p1.per_nombres ILIKE{" + contador + "}) ";
                valores.Add("%" + obj.nombres + "%");
                contador++;
            }

            //if (obj.total.HasValue)
            //{                
            //    parametros.where += ((parametros.where != "") ? " and " : "") + " tot_total "+obj.operador+" {" + contador + "} ";
            //    valores.Add(obj.total);
            //    contador++;
            //}

            if (!string.IsNullOrEmpty(obj.estadoenvio))//ESTADO ENVIO
            {
                if (obj.estadoenvio == "1") //POR COBRAR
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " SUM(ddo_monto) > SUM(ddo_cancela) and c.com_tipodoc= " + tipodocfac;
                }
                if (obj.estadoenvio == "2") //POR DESPACHAR
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " SUM(ddo_monto) = SUM(ddo_cancela) and e.cenv_despachado_ret IS NULL";
                }
                if (obj.estadoenvio == "3")//DESPACHADOS
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " e.cenv_despachado_ret = 1 ";
                }
            }

            if (!string.IsNullOrEmpty(obj.nombres_rem)) //REMITENTE
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (e.cenv_ciruc_rem ILIKE {" + contador + "} or e.cenv_nombres_rem ILIKE {" + contador + "} or e.cenv_apellidos_rem ILIKE{" + contador + "}) ";
                valores.Add("%" + obj.nombres_rem + "%");
                contador++;
            }

            if (!string.IsNullOrEmpty(obj.nombres_des)) //DESTINATARIO
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (e.cenv_ciruc_des ILIKE {" + contador + "} or e.cenv_nombres_des ILIKE {" + contador + "} or e.cenv_apellidos_des ILIKE{" + contador + "}) ";
                valores.Add("%" + obj.nombres_des + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.nombres_soc)) //SOCIO
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (p1.per_apellidos ILIKE {" + contador + "} or p1.per_nombres ILIKE{" + contador + "}) ";
                valores.Add("%" + obj.nombres_soc + "%");
                contador++;
            }

            if (!string.IsNullOrEmpty(obj.placa)) //VEHICULO
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (e.cenv_placa ILIKE {" + contador + "} or e.cenv_disco ILIKE {" + contador + "}) ";
                valores.Add("%" + obj.placa + "%");
                contador++;
            }

            /*if (obj.ruta.HasValue) //RUTA
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (e.cenv_ruta = {" + contador + "}) ";
                valores.Add(obj.ruta);
                contador++;
            }*/

            if (!string.IsNullOrEmpty(usr.usr_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc IN (SELECT udo_tipodoc FROM usrdoc WHERE udo_usuario = {" + contador + "}) ";
                valores.Add(usr.usr_id);
                contador++;
            }

            parametros.valores = valores.ToArray();
        }
        [WebMethod]
        public static string GetGuiasData(object objeto)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object comprob = null;
            object filtros = null;
            tmp.TryGetValue("comprob", out comprob);
            tmp.TryGetValue("filtros", out filtros);


            Comprobante comp = new Comprobante(comprob);

            

            int tipodocfac = Constantes.cFactura.tpd_codigo;
            int tipodocgui = Constantes.cGuia.tpd_codigo;
            SetWhereClause(new vComprobante(filtros), new Usuario(filtros), tipodocfac, tipodocgui);
            int desde = 0;
            int hasta = 999;
            pageIndex++;

            string whereguias = "";
            foreach (Rutaxfactura rxf in comp.rutafactura)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_codigo != " + rxf.rfac_comprobantefac;                
            }
            

            List<vComprobante> lista = vComprobanteBLL.GetAllByPage(parametros, "t.com_fecha DESC", desde, hasta);




           // List<Ccomenv> lst = CcomenvBLL.GetAll(new WhereParams("cenv_empresa={0} and  cenv_ruta ={1} and cenv_estado_ruta={2} and cenv_socio ={2}" + whereguias, comprobante.com_empresa, comprobante.com_ruta, 0), "com_fecha");
            StringBuilder html = new StringBuilder();


            HtmlTable tdatos = new HtmlTable();

            tdatos.id = "tddatos_P";
            tdatos.invoice = true;

            tdatos.AddColumn("Comprobante", "", "", "");
            tdatos.AddColumn("Remitente", "", "", "");
            tdatos.AddColumn("Destinatario", "", "", "");
            tdatos.AddColumn("Valor", "", "");

            //tdatos.editable = true;
           //foreach (Ccomenv item in lst)
            foreach (vComprobante item in lista)
            
            {
               HtmlRow row = new HtmlRow();
               row.markable = true;
               row.data = "data-comprobante=" + item.codigo;
               //row.cells.Add(new HtmlCell { valor = "<a href='#' onclick='ViewComprobante(" + item.cenv_comprobante + ");' >" + item.cenv_doctran + "</a>" });
               row.cells.Add(new HtmlCell { valor = new Boton { small = true, id = "btnview", tooltip = "Ver comprobante", clase = "iconsweets-magnifying", click = "ViewComprobante(" + item.codigo + ")" }.ToString()+ " " + item.doctran });                
               row.cells.Add(new HtmlCell { valor = item.nombres_rem  });
               row.cells.Add(new HtmlCell { valor = item.nombres_des });
               row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.total), clase = Css.right });

               tdatos.AddRow(row);

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
            comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);
            Ruta ruta = new Ruta();
            ruta.rut_codigo_key = comprobante.com_ruta??0;
            ruta.rut_empresa_key = comprobante.com_empresa;
            ruta = RutaBLL.GetByPK(ruta); 
            StringBuilder html = new StringBuilder();      
            HtmlTable tdatos1 = new HtmlTable();

            tdatos1.CreteEmptyTable(7, 2);
            tdatos1.rows[0].cells[0].valor = "Registros:";
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtREGISTROS", clase = Css.medium + Css.amount, habilitado = false, valor = 0 }.ToString();
            tdatos1.rows[1].cells[0].valor = "TOTAL BULTOS:";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtTOTALBULTOS", clase = Css.medium + Css.amount, habilitado = false }.ToString();
            tdatos1.rows[2].cells[0].valor = "TOTAL SEGURO:";
            tdatos1.rows[2].cells[1].valor = new Input { id = "txtTOTALSEG", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_tseguro) }.ToString();
            tdatos1.rows[3].cells[0].valor = "TOTAL IVA:";
            tdatos1.rows[3].cells[1].valor = new Input { id = "txtTOTALIMP", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_timpuesto) }.ToString();
            tdatos1.rows[4].cells[0].valor = "TOTAL DOMICILIO:";
            tdatos1.rows[4].cells[1].valor = new Input { id = "txtTOTALDOMICILIO", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_transporte) }.ToString();
            tdatos1.rows[5].cells[0].valor = new Input { id = "txtPORCENTAJEVISUAL", habilitado = false, valor = "PORCENTAJE %" + Formatos.CurrencyFormat(ruta.rut_porcentaje) + ":" }.ToString() + new Input { id = "txtPORCENTAJE", habilitado = false, valor = Formatos.CurrencyFormat(ruta.rut_porcentaje), visible = false }.ToString(); ;           
            //tdatos1.rows[5].cells[1].valor = new Input { id = "txtPORCENTAJEVALOR", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_total * ruta.rut_porcentaje/100) }.ToString();
            tdatos1.rows[5].cells[1].valor = new Input { id = "txtPORCENTAJEVALOR", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat((comprobante.total.tot_subtot_0+comprobante.total.tot_subtotal) * ruta.rut_porcentaje / 100) }.ToString();
            tdatos1.rows[6].cells[0].valor = "TOTAL:";
            tdatos1.rows[6].cells[1].valor = new Input { id = "txtTOTALCOM", clase = Css.medium + Css.totalamount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_total) }.ToString();
            List<Politica> lst = PoliticaBLL.GetAll("", "");
            html.AppendLine(tdatos1.ToString());
            HtmlTable tpoliticas = new HtmlTable();
            tpoliticas.CreteEmptyTable(lst.Count, 2);

            int cont = 0;

            foreach (Politica item in lst)
            {
                tpoliticas.rows[cont].cells[0].valor = item.pol_nombre;
                tpoliticas.rows[cont].cells[1].valor = new Input { id = "txt" + item.pol_id, clase = Css.medium + Css.amount, habilitado = false }.ToString();
                cont += 1;
            }
            html.AppendLine(tpoliticas.ToString());
           
            return html.ToString();

        }

        
        [WebMethod]
        public static string GetTotales(object objeto)
        {
            Comprobante obj = new Comprobante(objeto);
            obj.total.tot_total = 0;
            obj.total.tot_transporte = 0;
            obj.total.tot_tseguro = 0;
            obj.total.tot_cantidad = 0;
            obj.total.tot_timpuesto = 0;



            List<Politica> lst = PoliticaBLL.GetAll("", "");
            obj.total.poliNombres = new Object[lst.Count];
            obj.total.poliValores = new Decimal[lst.Count];
            int cont = 0;
            foreach (Politica pol in lst)
            {
                obj.total.poliNombres[cont] = "txt" + pol.pol_id;
                cont += 1;
            }
            foreach (Rutaxfactura item in obj.rutafactura)
            {
                Total total = new Total();
                total.tot_comprobante = item.rfac_comprobantefac;
                total.tot_empresa = item.rfac_empresa;
                total.tot_comprobante_key = item.rfac_comprobantefac;
                total.tot_empresa_key = item.rfac_empresa;
                total = TotalBLL.GetByPK(total);
                obj.total.tot_total += total.tot_total;
                obj.total.tot_transporte += total.tot_transporte;
                obj.total.tot_tseguro += total.tot_tseguro ?? 0;
                obj.total.tot_timpuesto += total.tot_timpuesto;
                List<Dcomdoc> lstrutaxfact = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa = {0}  and ddoc_comprobante = {1}", item.rfac_empresa, item.rfac_comprobantefac), "");
                foreach (Dcomdoc dcom in lstrutaxfact)
                {
                    obj.total.tot_cantidad += dcom.ddoc_cantidad;
                }


                Ccomdoc ccom = new Ccomdoc();
                ccom.cdoc_comprobante = item.rfac_comprobantefac;
                ccom.cdoc_empresa = item.rfac_empresa;
                ccom.cdoc_comprobante_key = item.rfac_comprobantefac;
                ccom.cdoc_empresa_key = item.rfac_empresa;
                ccom = CcomdocBLL.GetByPK(ccom);
                cont = 0;
                foreach (Politica pol in lst)
                {
                    if (ccom.cdoc_politica == pol.pol_codigo)
                    {
                        obj.total.poliValores[cont] += total.tot_total;
                    }
                    cont += 1;
                }
                //    obj.total.poliValores[(int)ccom.cdoc_politica] += total.tot_total;
            }
            return new JavaScriptSerializer().Serialize(obj);
        }


       





        [WebMethod]
        public static string SaveObject(object objeto)
        {            
            Comprobante obj = new Comprobante(objeto);
            Vehiculo veh = new Vehiculo(objeto);
            Vehiculo vehu = VehiculoBLL.GetByPK(new Vehiculo { veh_empresa = veh.veh_empresa, veh_empresa_key = veh.veh_empresa, veh_codigo = veh.veh_codigo, veh_codigo_key = veh.veh_codigo });
            vehu.veh_chofer1 = veh.veh_chofer1;
            vehu.veh_empresa_key = veh.veh_empresa;
            vehu.veh_codigo_key = veh.veh_codigo;
            VehiculoBLL.Update(vehu);

            if (obj.com_codigo > 0)
            {
                return UpdateComprobante(obj);
                
            }
            else
            {
                return InsertComprobante(obj);
            }
        }

        public static string InsertComprobante(Comprobante obj)
        {
            DateTime fecha = DateTime.Now;
            #region Actualiza el numero de comprobante en 1
            Dtipocom dti = General.GetDtipocom(obj.com_empresa, obj.com_fecha.Year, obj.com_ctipocom, obj.com_almacen.Value, obj.com_pventa.Value);
            //dti.dti_numero = dti.dti_numero.Value + 1;
            obj.com_numero = General.GetNumeroLibre(dti).dti_numero.Value;
            #endregion
            Vehiculo veh = VehiculoBLL.GetByPK(new Vehiculo { veh_empresa = obj.com_empresa, veh_empresa_key = obj.com_empresa, veh_codigo = obj.com_vehiculo.Value, veh_codigo_key = obj.com_vehiculo.Value });            
            if (obj.com_concepto==null)
                obj.com_concepto = "HOJA DE RUTA " + veh.veh_placa + " " + veh.veh_disco + " " + obj.com_fecha.ToShortDateString();

            obj.com_modulo = General.GetModulo(obj.com_tipodoc); ;
            obj.com_transacc = General.GetTransacc(obj.com_tipodoc);
            obj.com_nocontable = 1;//HAY QUE DEFINIR           
            obj.com_descuadre = 0;
            obj.com_adestino = 0;
            obj.com_doctran = General.GetNumeroComprobante(obj);           
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                obj.com_codigo = ComprobanteBLL.InsertIdentity(transaction, obj);
                obj.total.tot_comprobante = obj.com_codigo;
                TotalBLL.Insert(transaction, obj.total);
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
                    ccomenvio.cenv_estado_ruta = obj.com_estado;
                    Vehiculo vehiculo = new Vehiculo();
                    vehiculo.veh_codigo = (int)obj.com_vehiculo;
                    vehiculo.veh_empresa = item.rfac_empresa;
                    vehiculo.veh_codigo_key = (int)obj.com_vehiculo;
                    vehiculo.veh_empresa_key = item.rfac_empresa;
                    vehiculo = VehiculoBLL.GetByPK(vehiculo);
                    ccomenvio.cenv_socio = (int)vehiculo.veh_duenio;
                    ccomenvio.cenv_nombres_soc = (string.IsNullOrEmpty(ccomenvio.cenv_nombres_soc)) ? vehiculo.veh_apellidoduenio + " " + vehiculo.veh_nombreduenio : ccomenvio.cenv_nombres_soc;
                    ccomenvio.cenv_placa = vehiculo.veh_placa;
                    ccomenvio.cenv_disco = vehiculo.veh_disco;
                    ccomenvio.cenv_chofer = (int)vehiculo.veh_chofer1;
                    ccomenvio.cenv_nombres_cho = (string.IsNullOrEmpty(ccomenvio.cenv_nombres_cho)) ? vehiculo.veh_apellidochofer1 + " " + vehiculo.veh_nombrechofer1 : ccomenvio.cenv_nombres_cho;
                    CcomenvBLL.Update(transaction, ccomenvio);
                    item.rfac_comprobanteruta = obj.com_codigo;
                    item.rfac_comprobanteruta_key = obj.com_codigo;
                    item.rfac_estado = obj.com_estado;
                    RutaxfacturaBLL.Insert(transaction, item);             
                }
                DtipocomBLL.Update(transaction, dti);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return "-1";
            }
            return obj.com_codigo.ToString();
        }


        public static string UpdateComprobante(Comprobante obj)
        {

            DateTime fecha = DateTime.Now;

            obj.com_empresa_key = obj.com_empresa;
            obj.com_codigo_key = obj.com_codigo;
            Comprobante objU = ComprobanteBLL.GetByPK(obj);
            objU.com_empresa_key = objU.com_empresa;
            objU.com_codigo_key = objU.com_codigo;

            Vehiculo veh = VehiculoBLL.GetByPK(new Vehiculo { veh_empresa = obj.com_empresa, veh_empresa_key = obj.com_empresa, veh_codigo = obj.com_vehiculo.Value, veh_codigo_key = obj.com_vehiculo.Value });

            objU.com_fecha = obj.com_fecha;
            objU.com_periodo = obj.com_fecha.Year;
            objU.com_codclipro = obj.com_codclipro;
            objU.com_agente = obj.com_agente;
            objU.mod_usr = obj.mod_usr;
            objU.mod_fecha = obj.mod_fecha;
            objU.com_concepto = !string.IsNullOrEmpty(obj.com_concepto) ? obj.com_concepto : "HOJA DE RUTA " + veh.veh_placa + " " + veh.veh_disco + " " + obj.com_fecha.ToShortDateString();

            objU.com_estado = obj.com_estado;//ACTUALIZA EL ESTADO DEL COMPROBANTE
            objU.com_vehiculo = obj.com_vehiculo;
            objU.com_ruta = obj.com_ruta;

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


                List<Rutaxfactura> lst = RutaxfacturaBLL.GetAll(new WhereParams("rfac_empresa ={0} and rfac_comprobanteruta = {1} ", obj.com_empresa, obj.com_codigo), "");
                List<Ccomenv> lst2 = new List<Ccomenv>();

                foreach (Rutaxfactura item in lst)
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
                    lst2.Add(ccomenvio);
                }
                
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
                    ccomenvio.cenv_estado_ruta = obj.com_estado;
                    Vehiculo vehiculo = new Vehiculo();
                    vehiculo.veh_codigo = (int)obj.com_vehiculo;
                    vehiculo.veh_empresa = item.rfac_empresa;
                    vehiculo.veh_codigo_key = (int)obj.com_vehiculo;
                    vehiculo.veh_empresa_key = item.rfac_empresa; 
                    vehiculo = VehiculoBLL.GetByPK(vehiculo);
                    ccomenvio.cenv_socio = (int)vehiculo.veh_duenio;
                    ccomenvio.cenv_nombres_soc = (string.IsNullOrEmpty(ccomenvio.cenv_nombres_soc)) ? vehiculo.veh_apellidoduenio + " " + vehiculo.veh_nombreduenio : ccomenvio.cenv_nombres_soc;
                    ccomenvio.cenv_placa = vehiculo.veh_placa;
                    ccomenvio.cenv_disco = vehiculo.veh_disco;
                 //   ccomenvio.cenv_chofer = obj.com_codclipro;
                    ccomenvio.cenv_chofer = (int)vehiculo.veh_chofer1;
                    ccomenvio.cenv_nombres_cho = (string.IsNullOrEmpty(ccomenvio.cenv_nombres_cho)) ? vehiculo.veh_apellidochofer1 + " " + vehiculo.veh_nombrechofer1 : ccomenvio.cenv_nombres_cho;
                    lst2.Add(ccomenvio);
                    item.rfac_comprobanteruta = obj.com_codigo;
                    item.rfac_comprobanteruta_key = obj.com_codigo;
                    item.rfac_estado = obj.com_estado;
                    RutaxfacturaBLL.Insert(transaction, item);
                }

                foreach (Ccomenv item in lst2)
                {
                    CcomenvBLL.Update(transaction, item);
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


        [WebMethod]
        public static  string GetHojasRuta(object objeto)
        {
            if (objeto != null)
            {

                Comprobante obj = new Comprobante(objeto);

                StringBuilder html = new StringBuilder();
                html.AppendLine("<div class=\"row-fluid\">");
                html.AppendLine("<div class=\"span9\">");
                HtmlTable td= new HtmlTable();
                td.CreteEmptyTable(5, 2);
                td.rows[0].cells[0].valor = "Hoja de Ruta:";
                td.rows[0].cells[1].valor = new Select { id = "cmbHOJARUTA_P", withempty = true, clase = Css.large, diccionario = Dictionaries.GetHojasRutaByEmpresaAlmacen(obj.com_empresa, obj.com_almacen.Value) }.ToString();

                td.rows[1].cells[0].valor = "Fecha:";
                td.rows[1].cells[1].valor = new Input { id = "txtFECHARUTA_P", clase = Css.medium, habilitado = false }.ToString();

                td.rows[2].cells[0].valor = "Vehiculo:";
                td.rows[2].cells[1].valor = new Input { id = "txtCODVEH_P", clase = Css.medium, visible = false }.ToString() + new Input { id = "txtIDVEH_P", clase = Css.medium, habilitado = false }.ToString() + " " + new Input { id = "txtVEHICULO_P", clase = Css.large, habilitado = false }.ToString();
                
                td.rows[3].cells[0].valor = "Socio:";
                td.rows[3].cells[1].valor = new Input { id = "txtCODSOC_P", clase = Css.medium, visible = false }.ToString() + new Input { id = "txtIDSOC_P", clase = Css.medium, habilitado = false }.ToString() + " " + new Input { id = "txtSOCIO_P", clase = Css.large, habilitado = false }.ToString();

                td.rows[4].cells[0].valor = "Chofer:";
                td.rows[4].cells[1].valor = new Input { id = "txtCODCHO_P", clase = Css.medium, visible = false }.ToString() + new Input { id = "txtIDCHO_P", clase = Css.medium, habilitado = false }.ToString() + " " + new Input { id = "txtCHOFER_P", clase = Css.large, habilitado = false }.ToString();
                
                html.AppendLine(td.ToString());
                html.AppendLine(" </div><!--span9-->");
                html.AppendLine("</div><!--row-fluid-->");

                return html.ToString();
            }
            return "";
        }

        [WebMethod]
        public static string GetDatosHojaRuta(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);

            Vehiculo vehiculo = new Vehiculo();
            vehiculo.veh_empresa = comprobante.com_empresa;
            vehiculo.veh_empresa_key = comprobante.com_empresa;
            if (comprobante.com_vehiculo.HasValue)
            {
                vehiculo.veh_codigo = comprobante.com_vehiculo.Value;
                vehiculo.veh_codigo_key = comprobante.com_vehiculo.Value;
                vehiculo = VehiculoBLL.GetByPK(vehiculo);
            }

            Persona socio = new Persona();
            socio.per_empresa = comprobante.com_empresa;
            socio.per_empresa_key = comprobante.com_empresa;
            if (comprobante.com_vehiculo.HasValue)
            {
                socio.per_codigo = vehiculo.veh_duenio.Value;
                socio.per_codigo_key = vehiculo.veh_duenio.Value;
                socio = PersonaBLL.GetByPK(socio);
            }


            Persona chofer = new Persona();
            chofer.per_empresa = comprobante.com_empresa;
            chofer.per_empresa_key = comprobante.com_empresa;
            if (comprobante.com_vehiculo.HasValue)
            {
                chofer.per_codigo = vehiculo.veh_chofer1.Value;
                chofer.per_codigo_key = vehiculo.veh_chofer1.Value;
                chofer = PersonaBLL.GetByPK(chofer);
            }

            /*Persona chofer2 = new Persona();
            chofer2.per_empresa = comprobante.com_empresa;
            chofer2.per_empresa_key = comprobante.com_empresa;
            if (comprobante.com_vehiculo.HasValue)
            {
                chofer2.per_codigo = vehiculo.veh_chofer2.Value;
                chofer2.per_codigo_key = vehiculo.veh_chofer2.Value;
                chofer2 = PersonaBLL.GetByPK(chofer2);
            }*/

            vHojadeRuta hoja = new vHojadeRuta();
            hoja.codigocabecera = comprobante.com_codigo;
            hoja.doctrancabecera = comprobante.com_doctran;
            hoja.fechacabecera = comprobante.com_fecha;
            hoja.codigovehiculo = vehiculo .veh_codigo;
            hoja.idvehiculo = vehiculo .veh_id;
            hoja.placavehiculo = vehiculo.veh_placa;
            hoja.discovehiculo = vehiculo.veh_disco;
            hoja.codigosocio = socio.per_codigo;
            hoja.idsocio = socio.per_id;
            hoja.nombresocio = socio.per_nombres;
            hoja.apellidosocio = socio.per_apellidos;
            hoja.codigochofer = chofer.per_codigo;
            hoja.idchofer= chofer.per_id;
            hoja.nombrechofer = chofer.per_nombres;
            hoja.apellidochofer= chofer.per_apellidos;
        

            return new JavaScriptSerializer().Serialize(hoja);


        }


        [WebMethod]
        public static string MoveObject(object objeto)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object objetoorigen = null;
            object objetodestino = null;
            
            tmp.TryGetValue("origen", out objetoorigen);
            tmp.TryGetValue("destino", out objetodestino);
            
            

            Comprobante objorg = new Comprobante(objetoorigen);

            objorg.com_empresa_key = objorg.com_empresa;
            objorg.com_codigo_key = objorg.com_codigo;
            objorg = ComprobanteBLL.GetByPK(objorg);

            objorg.total = new Total();
            objorg.total.tot_empresa = objorg.com_empresa;
            objorg.total.tot_empresa_key = objorg.com_empresa;
            objorg.total.tot_comprobante = objorg.com_codigo;
            objorg.total.tot_comprobante_key = objorg.com_codigo;
            objorg.total = TotalBLL.GetByPK(objorg.total);
           
            Comprobante comprobante = new Comprobante(objetodestino);

            List<Rutaxfactura> lst = comprobante.rutafactura; 
            
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);
            
            comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);

           


           
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {


                transaction.BeginTransaction();


                //////Elimina las facturas y guias existentes en el comprobante de origen
              /*  string where = "";
                foreach (Rutaxfactura item in comprobante.rutafactura)
                {
                    where += ((where != "") ? " or " : "") + "rfac_comprobantefac = " + item.rfac_comprobantefac.ToString();
                }
                List<Rutaxfactura> lst = RutaxfacturaBLL.GetAll(new WhereParams("rfac_empresa ={0} and rfac_comprobanteruta = {1} and (" + where + ")", objorg.com_empresa, objorg.com_codigo), "");
               */
                foreach (Rutaxfactura item in lst)
                {
                    Rutaxfactura r = item;
                    item.rfac_comprobanteruta = objorg.com_codigo;
                    RutaxfacturaBLL.Delete(transaction, r);
                }
                ///////////////////////////////
                

                ////Agrega las facturas y guias seleccionadas al comprobante de destino
                
                foreach (Rutaxfactura item in lst)
                {
                    Total total = new Total();
                    total.tot_comprobante = item.rfac_comprobantefac;
                    total.tot_empresa = item.rfac_empresa;
                    total.tot_comprobante_key = item.rfac_comprobantefac;
                    total.tot_empresa_key = item.rfac_empresa;
                    total = TotalBLL.GetByPK(total);
 
                    Ccomenv ccomenvio = new Ccomenv();
                    ccomenvio.cenv_comprobante = item.rfac_comprobantefac;
                    ccomenvio.cenv_empresa = item.rfac_empresa;
                    ccomenvio.cenv_comprobante_key = item.rfac_comprobantefac;
                    ccomenvio.cenv_empresa_key = item.rfac_empresa;
                    ccomenvio = CcomenvBLL.GetByPK(ccomenvio);
                    ccomenvio.cenv_vehiculo = comprobante.com_vehiculo;
                    ccomenvio.cenv_empresa_cho = item.rfac_empresa;
                    ccomenvio.cenv_empresa_des = item.rfac_empresa;
                    ccomenvio.cenv_empresa_rem = item.rfac_empresa;
                    ccomenvio.cenv_empresa_soc = item.rfac_empresa;
                    ccomenvio.cenv_empresa_veh = item.rfac_empresa;
                    ccomenvio.cenv_comprobante_key = item.rfac_comprobantefac;
                    ccomenvio.cenv_empresa_key = item.rfac_empresa;
                    ccomenvio.cenv_estado_ruta = comprobante.com_estado;
                    Vehiculo vehiculo = new Vehiculo();
                    vehiculo.veh_codigo = (int)comprobante.com_vehiculo;
                    vehiculo.veh_empresa = item.rfac_empresa;
                    vehiculo.veh_codigo_key = (int)comprobante.com_vehiculo;
                    vehiculo.veh_empresa_key = item.rfac_empresa;
                    vehiculo = VehiculoBLL.GetByPK(vehiculo);

                    ccomenvio.cenv_socio = (int)vehiculo.veh_duenio;
                    ccomenvio.cenv_placa = vehiculo.veh_placa;
                    ccomenvio.cenv_disco = vehiculo.veh_disco;
                    ccomenvio.cenv_chofer = (int)vehiculo.veh_chofer1;
                    CcomenvBLL.Update(transaction, ccomenvio);
                    item.rfac_comprobanteruta = comprobante.com_codigo;
                    item.rfac_comprobanteruta_key = comprobante.com_codigo;
                    item.rfac_estado = comprobante.com_estado;
                    RutaxfacturaBLL.Insert(transaction, item);
                    

                    comprobante.total.tot_tseguro = comprobante.total.tot_tseguro + total.tot_tseguro;
                    comprobante.total.tot_timpuesto = comprobante.total.tot_timpuesto + total.tot_timpuesto;
                    comprobante.total.tot_transporte = comprobante.total.tot_transporte + total.tot_transporte;
                    comprobante.total.tot_total = comprobante.total.tot_total + total.tot_total;
                    TotalBLL.Update(transaction, comprobante.total);

                    objorg.total.tot_tseguro = objorg.total.tot_tseguro - total.tot_tseguro;
                    objorg.total.tot_timpuesto = objorg.total.tot_timpuesto - total.tot_timpuesto;
                    objorg.total.tot_transporte = objorg.total.tot_transporte - total.tot_transporte;
                    objorg.total.tot_total = objorg.total.tot_total - total.tot_total;
                }
                TotalBLL.Update(transaction, objorg.total);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex.Message;
            }
            return "OK";
            
        }

    }
}