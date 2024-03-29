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
using Functions;
using Packages;


namespace WebUI
{
    public partial class wfComprobanteTC : System.Web.UI.Page
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

                ValidaOrigen();

                pageIndex = 1;
                pageSize = 20;

            }

        }

        private void ValidaOrigen()
        {
            //Empresa emp = (Empresa)Session["empresa"]; 
            if (txtorigen.Text == "LGC")
            {
                int codigoemp = int.Parse(Request.QueryString["codigoempresa"]);
                List<Planillacomprobante> lst = PlanillacomprobanteBLL.GetAll(new WhereParams("pco_empresa={0} and pco_comprobante_pla={1}", codigoemp, Convert.ToInt64(txtcodigocompref.Text)), "");
                if (lst.Count > 0)
                {
                    txtorigen.Text = "";

                    txtcodigocomp.Text = lst[0].pco_comprobante_fac.ToString();
                    //Response.Redirect("wfInfo.aspx?msg=La planilla ya posee una factura generada");
                }
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
            html.AppendLine(new Input { id = "txtFECHA", etiqueta = "Fecha", datepicker = true, datetimevalor = DateTime.Now, clase = Css.small }.ToString());
            html.AppendLine(new Select { id = "cmbALMACEN", diccionario = Dictionaries.GetAlmacen(), etiqueta = "Almacen", clase = Css.small }.ToString());
            html.AppendLine(new Select { id = "cmbPVENTA", etiqueta = "Punto Venta", diccionario = new Dictionary<string, string>(), clase = Css.small }.ToString());
            return html.ToString();
        }


        [WebMethod]
        public static string GetHojasRuta(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            return new Select { id = "cmbHOJARUTA", withempty = true, clase = Css.large, diccionario = (comprobante.com_ruta.HasValue) ? Dictionaries.GetHojasRutaByRuta(comprobante.com_ruta.Value) : Dictionaries.Empty() }.ToString();
        }
        [WebMethod]
        public static string GetAllRutas()
        {
            return new Select { id = "cmbRUTA", clase = Css.medium, diccionario = Dictionaries.GetRuta() }.ToString();
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

            ArrayList datos = new ArrayList();
            datos.Add(comprobante);
            datos.Add(vehiculo);
            datos.Add(ruta);
            datos.Add(socio);
            datos.Add(chofer);

            return new JavaScriptSerializer().Serialize(datos);

            /*List<vHojadeRuta> hojas = vHojadeRutaBLL.GetAll(new WhereParams("rfac_empresa={0} and rfac_comprobanteruta={1}",comprobante.com_empresa,comprobante.com_codigo), "");
            if (hojas.Count > 0)
                return new JavaScriptSerializer().Serialize(hojas[0]);
            else
                return "";*/
        }
        [WebMethod]
        public static string GetDatosPolitica(object objeto)
        {
            Politica politica = new Politica(objeto);
            politica.pol_empresa_key = politica.pol_empresa;
            politica.pol_codigo_key = politica.pol_codigo;

            politica = PoliticaBLL.GetByPK(politica);
            return new JavaScriptSerializer().Serialize(politica);
        }

        public static string ShowObject(Comprobante obj)
        {

            Persona persona = new Persona();
            persona.per_empresa = obj.com_empresa;
            persona.per_empresa_key = obj.com_empresa;
            if (obj.com_codclipro.HasValue)
            {
                persona.per_codigo = obj.com_codclipro.Value;
                persona.per_codigo_key = obj.com_codclipro.Value;
                persona = PersonaBLL.GetByPK(persona);
            }


            Persona agente = new Persona();
            agente.per_empresa = obj.com_empresa;
            agente.per_empresa_key = obj.com_empresa;
            if (obj.com_agente.HasValue)
            {
                agente.per_codigo = obj.com_agente.Value;
                agente.per_codigo_key = obj.com_agente.Value;
                agente = PersonaBLL.GetByPK(agente);
            }


            bool habilitado = true;
            if (obj.com_estado == Constantes.cEstadoEliminado || obj.com_estado == Constantes.cEstadoMayorizado)
                habilitado = false;


            List<Tab> tabs = new List<Tab>();


            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Cliente:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtCODCLIPRO", valor = persona.per_id, autocomplete = "GetClienteObj", obligatorio = true, clase = Css.medium, placeholder = "Cliente", habilitado = habilitado }.ToString() + " " + new Input { id = "txtNOMBRES", clase = Css.large, habilitado = false, valor = persona.per_apellidos + " " + persona.per_nombres }.ToString() + new Input { id = "txtCODPER", visible = false, valor = persona.per_codigo }.ToString() + new Boton { small = true, id = "btncallper", tooltip = "Agregar cliente", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cCliente + ")" }.ToString() + new Boton { small = true, id = "btncleanper", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString() + new Boton { small = true, id = "btncallRem", tooltip = "Asignar a Remitente", clase = "iconsweets-mail", click = "asigRem()" }.ToString() + new Boton { small = true, id = "btncallDestinatario", tooltip = "Asignar a destinatario", clase = "iconsweets-postcard", click = "asigDes()" }.ToString();
            tdatos.rows[1].cells[0].valor = "CI-RUC/Razón Social:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtRUC", clase = Css.medium, habilitado = false, valor = persona.per_ciruc }.ToString() + " " + new Input { id = "txtRAZON", clase = Css.large, habilitado = false, valor = persona.per_razon }.ToString();
            tdatos.rows[2].cells[0].valor = "Teléfono/Dirección:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtTELEFONO", clase = Css.medium, habilitado = false, valor = persona.per_telefono }.ToString() + " " + new Input { id = "txtDIRECCION", clase = Css.large, habilitado = false, valor = persona.per_direccion }.ToString();
            //tdatos.rows[3].cells[0].valor = "Ubicación:";
            //tdatos.rows[3].cells[1].valor = new Input { id = "txtUBICA", clase = Css.large, habilitado = false }.ToString();





            Listaprecio lista = new Listaprecio();

            lista.lpr_empresa = obj.com_empresa;
            lista.lpr_empresa_key = obj.com_empresa;
            if (obj.ccomdoc.cdoc_listaprecio.HasValue)
            {
                lista.lpr_codigo = obj.ccomdoc.cdoc_listaprecio.Value;
                lista.lpr_codigo_key = obj.ccomdoc.cdoc_listaprecio.Value;
                lista = ListaprecioBLL.GetByPK(lista);
            }


            Politica politica = new Politica();

            politica.pol_empresa = obj.com_empresa;
            politica.pol_empresa_key = obj.com_empresa;
            if (obj.ccomdoc.cdoc_politica.HasValue)
            {
                politica.pol_codigo = obj.ccomdoc.cdoc_politica.Value;
                politica.pol_codigo_key = obj.ccomdoc.cdoc_politica.Value;
                politica = PoliticaBLL.GetByPK(politica);
            }

            //FALTA CARGAR VENDEDOR

            obj.com_tclipro = Constantes.cCliente;


            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(3, 2);
            tdatos1.rows[0].cells[0].valor = "Lista Precio:";
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtIDLIS", autocomplete = "GetListaObj", clase = Css.small, valor = lista.lpr_id, habilitado = false }.ToString() + " " + new Input { id = "txtLISTA", clase = Css.large, habilitado = false, valor = lista.lpr_nombre }.ToString() + " " + new Input { id = "txtCODLIS", visible = false, valor = lista.lpr_codigo }.ToString();
            //tdatos1.rows[0].cells[1].valor = new Select { id = "cmbLISTAPRECIO", diccionario = Dictionaries.GetListaprecio(), clase = Css.medium}.ToString();
            tdatos1.rows[1].cells[0].valor = "Política Venta:";
            //tdatos1.rows[1].cells[1].valor = new Input { id = "txtIDPOL", autocomplete = "GetPoliticaObj", clase = Css.small, valor = politica.pol_id, obligatorio = true, placeholder = "Política", habilitado = habilitado }.ToString() + " " + new Input { id = "txtPOLITICA", clase = Css.large, habilitado = false, valor = politica.pol_nombre }.ToString() + " " + new Input { id = "txtPORCENTAJE", clase = Css.mini, numeric = true, valor = politica.pol_porc_desc, habilitado = false }.ToString() + "% Desc" + new Input { id = "txtCODPOL", visible = false, valor = politica.pol_codigo }.ToString() + new Input { id = "txtNROPAGOS", visible = false, valor = politica.pol_nro_pagos }.ToString() + new Input { id = "txtDIASPLAZO", visible = false, valor = politica.pol_dias_plazo }.ToString() + new Input { id = "txtPORCPAGOCON", visible = false, valor = politica.pol_porc_pago_con }.ToString();
            tdatos1.rows[1].cells[1].valor = new Select { id = "cmbPOLITICA", diccionario = Dictionaries.GetPolitica(obj), clase = Css.medium, valor = politica.pol_codigo }.ToString() + " " + new Input { id = "txtPORCENTAJE", clase = Css.mini, numeric = true, valor = politica.pol_porc_desc, habilitado = false }.ToString() + "% Desc" + new Input { id = "txtNROPAGOS", visible = false, valor = politica.pol_nro_pagos }.ToString() + new Input { id = "txtDIASPLAZO", visible = false, valor = politica.pol_dias_plazo }.ToString() + new Input { id = "txtPORCPAGOCON", visible = false, valor = politica.pol_porc_pago_con }.ToString();

            tdatos1.rows[2].cells[0].valor = "Agente:";
            //tdatos1.rows[2].cells[1].valor = new Input { id = "txtCODVEN", autocomplete = "GetPersonaObj", clase = Css.small }.ToString() + " " + new Input { id = "txtVENDEDOR", clase = Css.large, habilitado = false }.ToString();
            tdatos1.rows[2].cells[1].valor = new Input { id = "txtIDAGE", valor = agente.per_id, autocomplete = "GetPersonaObj", clase = Css.medium, placeholder = "Agente", habilitado = habilitado }.ToString() + " " + new Input { id = "txtNOMBRESAGE", clase = Css.large, habilitado = false, valor = agente.per_apellidos + " " + agente.per_nombres }.ToString() + new Input { id = "txtCODAGE", visible = false, valor = agente.per_codigo }.ToString() + new Boton { small = true, id = "btncallage", tooltip = "Agregar Agente", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cCliente + ")" }.ToString() + new Boton { small = true, id = "btncleanage", tooltip = "Limpiar datos agente", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString() ;
            //tdatos1.rows[2].cells[1].valor = new Input { id = "txtIDVEN", clase = Css.medium, placeholder = "Vendedor", visible = false, habilitado = habilitado }.ToString() + " " + new Input { id = "txtNOMBRESVEN", autocomplete = "GetPersonaObj", clase = Css.large, valor = obj.ccomenv.cenv_nombres_rem, habilitado = habilitado }.ToString() + new Input { id = "txtCODVEN", visible = false, valor = obj.ccomenv.cenv_remitente, habilitado = habilitado }.ToString() + new Boton { small = true, id = "btncallven", tooltip = "Agregar Vendedor", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cCliente + ")" }.ToString() + new Boton { small = true, id = "btncleanven", tooltip = "Limpiar datos vendedor", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();

            //tdatos1.rows[2].cells[0].valor = "Bodega:";
            //tdatos1.rows[2].cells[1].valor = new Input { id = "txtCODBOD", autocomplete = "GetBodegaObj", clase = Css.small }.ToString() + " " + new Input { id = "txtBODEGA", clase = Css.large, habilitado = false }.ToString();



            html.AppendLine(tdatos1.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");


            html.AppendLine(new Input { id = "txtESTADO", valor = obj.com_estado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCERRADO", valor = Constantes.cEstadoMayorizado, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCREADO", valor = Constantes.cEstadoProceso, visible = false }.ToString());

            /*Persona remitente = new Persona();
            remitente.per_empresa = obj.com_empresa;
            remitente.per_empresa_key = obj.com_empresa;
            if (obj.ccomenv.cenv_remitente.HasValue)
            {
                remitente.per_codigo = obj.ccomenv.cenv_remitente.Value;
                remitente.per_codigo_key = obj.ccomenv.cenv_remitente.Value;
                remitente = PersonaBLL.GetByPK(remitente);
            }*/



            StringBuilder html1 = new StringBuilder();
            html1.AppendLine("<div class=\"row-fluid\">");
            html1.AppendLine("<div class=\"span6\">");
            HtmlTable tdrem = new HtmlTable();
            tdrem.CreteEmptyTable(2, 2);
            tdrem.rows[0].cells[0].valor = "Remitente:";
            tdrem.rows[0].cells[1].valor = new Input { id = "txtIDREM", clase = Css.medium, placeholder = "Remitente", visible = false, habilitado = habilitado }.ToString() + " " + new Input { id = "txtNOMBRESREM", autocomplete = "GetPersonaObj", clase = Css.large, obligatorio = true, valor = obj.ccomenv.cenv_nombres_rem, habilitado = habilitado }.ToString() + new Input { id = "txtCODREM", visible = false, valor = obj.ccomenv.cenv_remitente, habilitado = habilitado }.ToString() + new Boton { small = true, id = "btncallrem", tooltip = "Agregar Remitente", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cCliente + ")" }.ToString() + new Boton { small = true, id = "btncleanrem", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();
            tdrem.rows[1].cells[0].valor = "Teléfono/Dirección:";
            tdrem.rows[1].cells[1].valor = new Input { id = "txtTELEFONOREM", clase = Css.medium, valor = obj.ccomenv.cenv_telefono_rem, habilitado = habilitado }.ToString() + " " + new Input { id = "txtDIRECCIONREM", clase = Css.large, valor = obj.ccomenv.cenv_direccion_rem, habilitado = habilitado }.ToString();
            html1.AppendLine(tdrem.ToString());
            html1.AppendLine(" </div><!--span6-->");


            //formulario para agregar retiro


            StringBuilder htmlr = new StringBuilder();
            htmlr.AppendLine("<div class=\"row-fluid\">");
            htmlr.AppendLine("<div class=\"span6\">");
            HtmlTable tdret = new HtmlTable();
            tdret.CreteEmptyTable(3, 2);
            tdret.rows[0].cells[0].valor = "Retira:";
            tdret.rows[0].cells[1].valor = new Input { id = "txtIDRET", clase = Css.medium, placeholder = "Retira", visible = false }.ToString() + " " + new Input { id = "txtNOMBRESRET", autocomplete = "GetPersonaObj", clase = Css.large, valor = obj.ccomenv.cenv_nombres_ret, habilitado = false }.ToString() + new Input { id = "txtCODRET", visible = false, valor = obj.ccomenv.cenv_retira }.ToString();
            tdret.rows[1].cells[0].valor = "Cedula";
            tdret.rows[1].cells[1].valor = new Input { id = "txtCIRUCRET", clase = Css.medium, valor = obj.ccomenv.cenv_ciruc_ret, habilitado = false }.ToString();
            tdret.rows[2].cells[0].valor = "Teléfono/Dirección:";
            tdret.rows[2].cells[1].valor = new Input { id = "txtTELEFONORET", clase = Css.medium, valor = obj.ccomenv.cenv_telefono_ret, habilitado = false }.ToString() + " " + new Input { id = "txtDIRECCIONRET", clase = Css.large, valor = obj.ccomenv.cenv_direccion_ret, habilitado = false }.ToString();
            htmlr.AppendLine(tdret.ToString());
            htmlr.AppendLine(" </div><!--span6-->");



            // fin de la tabla retiro


            // segunda tabla de retiro

            htmlr.AppendLine("<div class=\"span6\">");
            HtmlTable tdretd = new HtmlTable();
            tdretd.CreteEmptyTable(3, 2);
            tdretd.rows[0].cells[0].valor = "Fecha/Hora:";
            tdretd.rows[0].cells[1].valor = new Input { id = "txtFECHARET", clase = Css.medium, valor = obj.ccomenv.cenv_fecha_ret, habilitado = false }.ToString();
            tdretd.rows[1].cells[0].valor = "Observacion";
            tdretd.rows[1].cells[1].valor = new Textarea { id = "txtOBSERVACIONRET", clase = Css.xxlarge, valor = obj.ccomenv.cenv_observaciones_ret, habilitado = false }.ToString();
            tdretd.rows[2].cells[0].valor = "Despachado";
            tdretd.rows[2].cells[1].valor = new Input { id = "txtDESPACHADORET", clase = Css.mini, valor = (obj.ccomenv.cenv_despachado_ret == 1) ? "Si" : "No", habilitado = false }.ToString();
            htmlr.AppendLine(tdretd.ToString());
            htmlr.AppendLine(" </div><!--span6-->");
            htmlr.AppendLine("</div><!--row-fluid-->");


            // fin de la tabla de retiro

            /*Persona destinatario = new Persona();
            destinatario.per_empresa = obj.com_empresa;
            destinatario.per_empresa_key = obj.com_empresa;
            if (obj.ccomenv.cenv_destinatario.HasValue)
            {
                destinatario.per_codigo = obj.ccomenv.cenv_destinatario.Value;
                destinatario.per_codigo_key = obj.ccomenv.cenv_destinatario.Value;
                destinatario = PersonaBLL.GetByPK(destinatario);
            }*/

            html1.AppendLine("<div class=\"span6\">");
            HtmlTable tddes = new HtmlTable();
            tddes.CreteEmptyTable(3, 2);
            tddes.rows[0].cells[0].valor = "Destinatario:";
            tddes.rows[0].cells[1].valor = new Input { id = "txtIDDES", clase = Css.medium, placeholder = "Destinatario", visible = false }.ToString() + " " + new Input { id = "txtNOMBRESDES", autocomplete = "GetPersonaObj", clase = Css.large, obligatorio = true, valor = obj.ccomenv.cenv_nombres_des, habilitado = habilitado }.ToString() + new Input { id = "txtCODDES", visible = false, valor = obj.ccomenv.cenv_destinatario }.ToString() + new Boton { small = true, id = "btncalldes", tooltip = "Agregar Destinatario", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cCliente + ")" }.ToString() + new Boton { small = true, id = "btncleandes", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();
            tddes.rows[1].cells[0].valor = "Teléfono/Dirección:";
            tddes.rows[1].cells[1].valor = new Input { id = "txtTELEFONODES", clase = Css.medium, habilitado = habilitado, valor = obj.ccomenv.cenv_telefono_des }.ToString() + " " + new Input { id = "txtDIRECCIONDES", clase = Css.large, habilitado = habilitado, valor = obj.ccomenv.cenv_direccion_des }.ToString();
            tddes.rows[2].cells[0].valor = "Destino/Entregar en:";
            tddes.rows[2].cells[1].valor = new Select { id = "cmbRUTA", clase = Css.medium, diccionario = Dictionaries.GetRuta(), valor = obj.ccomenv.cenv_ruta, habilitado = habilitado }.ToString() + ((habilitado) ? new Boton { small = true, id = "btnallruta", tooltip = "Todas las rutas", clase = "iconsweets-refresh", click = "GetAllRutas()" }.ToString() : "") + " " + new Input { id = "txtENTREGADES", clase = Css.large, valor = obj.ccomenv.cenv_observacion, habilitado = habilitado }.ToString();
            //tddes.rows[2].cells[1].valor = new Input{ id = "txtUBICACIONDES", clase = Css.medium}.ToString()+ " " +new Input { id = "txtENTREGADES", clase = Css.large }.ToString();            
            html1.AppendLine(tddes.ToString());
            html1.AppendLine(" </div><!--span6-->");
            html1.AppendLine("</div><!--row-fluid-->");




            /* Vehiculo vehiculo = new  Vehiculo();
             vehiculo.veh_empresa = obj.com_empresa;
             vehiculo.veh_empresa_key = obj.com_empresa;
             if (obj.ccomenv.cenv_vehiculo.HasValue)
             {
                 vehiculo.veh_codigo = obj.ccomenv.cenv_vehiculo.Value;
                 vehiculo.veh_codigo_key = obj.ccomenv.cenv_vehiculo.Value;
                 vehiculo = VehiculoBLL.GetByPK(vehiculo);
             }

             Persona socio = new Persona();
             socio.per_empresa = obj.com_empresa;
             socio.per_empresa_key = obj.com_empresa;
             if (obj.ccomenv.cenv_socio.HasValue)
             {
                 socio.per_codigo = obj.ccomenv.cenv_socio.Value;
                 socio.per_codigo_key = obj.ccomenv.cenv_socio.Value;
                 socio = PersonaBLL.GetByPK(socio);
             }

             Persona chofer = new Persona();
             chofer.per_empresa = obj.com_empresa;
             chofer.per_empresa_key = obj.com_empresa;
             if (obj.ccomenv.cenv_chofer.HasValue)
             {
                 chofer.per_codigo = obj.ccomenv.cenv_chofer.Value;
                 chofer.per_codigo_key = obj.ccomenv.cenv_chofer.Value;
                 chofer = PersonaBLL.GetByPK(chofer);
             }
             */


            StringBuilder html2 = new StringBuilder();
            html2.AppendLine("<div class=\"row-fluid\">");
            List<vHojadeRuta> hojas = vHojadeRutaBLL.GetAll(new WhereParams("rfac_empresa={0} and rfac_comprobantefac={1}", obj.com_empresa, obj.com_codigo), "");

            html2.AppendLine("<div class=\"span6\">");
            HtmlTable tdtra = new HtmlTable();
            tdtra.CreteEmptyTable(3, 2);


            tdtra.rows[0].cells[0].valor = "Hoja de Ruta:";
            tdtra.rows[0].cells[1].valor = new Select { id = "cmbHOJARUTA", clase = Css.large, diccionario = Dictionaries.GetHojasRutaByEmpresaAlmacen(obj.com_empresa, obj.com_almacen.Value), valor = (hojas.Count > 0) ? hojas[0].codigocabecera.Value.ToString() : "", withempty = true, habilitado = habilitado }.ToString() + " " + new Input { id = "txtFECHARUTA", clase = Css.medium, valor = (hojas.Count > 0) ? hojas[0].fechacabecera.Value.ToString() : "", habilitado = false }.ToString();
            tdtra.rows[1].cells[0].valor = "Ruta:";
            tdtra.rows[1].cells[1].valor = new Input { id = "txtNOMBRERUTA", clase = Css.large, habilitado = false, valor = (hojas.Count > 0) ? hojas[0].nombreruta : "" }.ToString();
            tdtra.rows[2].cells[0].valor = "Vehiculo:";
            tdtra.rows[2].cells[1].valor = new Input { id = "txtVEHICULORUTA", clase = Css.large, habilitado = false, valor = (hojas.Count > 0 && hojas[0].placavehiculo != null) ? "Placa: " + hojas[0].placavehiculo + " / Disco: " + hojas[0].discovehiculo : "" }.ToString() + new Input { id = "txtCODVEH", clase = Css.medium, valor = (hojas.Count > 0 && hojas[0].codigovehiculo != null) ? hojas[0].codigovehiculo.Value.ToString() : "", visible = false }.ToString() + new Input { id = "txtPLACAVEH", valor = (hojas.Count > 0 && hojas[0].placavehiculo != null) ? hojas[0].placavehiculo.ToString() : "", visible = false }.ToString() + new Input { id = "txtDISCOVEH", valor = (hojas.Count > 0 && hojas[0].discovehiculo != null) ? hojas[0].discovehiculo.ToString() : "", visible = false }.ToString();

            //if (hojas.Count > 0)
            //{

            //    tdtra.rows[0].cells[0].valor = "Hoja de Ruta:";
            //    tdtra.rows[0].cells[1].valor = new Input { id = "txtHOJARUTA", clase = Css.large, valor = hojas[0].doctrancabecera, habilitado = false }.ToString();
            //    tdtra.rows[1].cells[0].valor = "Fecha/Hora:";
            //    tdtra.rows[1].cells[1].valor = new Input { id = "txtFECHARUTA", clase = Css.medium, valor = hojas[0].fechacabecera, habilitado = false }.ToString();
            //    tdtra.rows[2].cells[0].valor = "Guia de Remisión:";


            //}
            //else
            //{




            //    tdtra.rows[0].cells[0].valor = "Hoja de Ruta:";
            //    tdtra.rows[0].cells[1].valor = new Select { id = "cmbHOJARUTA", clase = Css.large, diccionario = Dictionaries.Empty(), valor = obj.ccomenv.cenv_ruta, withempty = true, habilitado = habilitado }.ToString();
            //    tdtra.rows[1].cells[0].valor = "Fecha/Hora:";
            //    tdtra.rows[1].cells[1].valor = new Input { id = "txtFECHARUTA", clase = Css.medium, habilitado = false }.ToString();
            //    tdtra.rows[2].cells[0].valor = "Guia de Remisión:";


            //}

            //    if (obj.ccomenv.cenv_guia1 != null && !obj.ccomenv.cenv_guia1.Equals(""))
            //        tdtra.rows[2].cells[1].valor = new Input { id = "txtALMACENPRO", clase = Css.mini, valor = obj.ccomenv.cenv_guia1, habilitado = habilitado }.ToString() + "-" + new Input { id = "txtPVENTAPRO", clase = Css.mini, valor = obj.ccomenv.cenv_guia2, habilitado = habilitado }.ToString() + "-" + new Input { id = "txtNUMEROPRO", clase = Css.small, valor = obj.ccomenv.cenv_guia3, habilitado = habilitado, largo = 9 }.ToString();
            //else
            //        tdtra.rows[2].cells[1].valor = new Input { id = "txtALMACENPRO", clase = Css.mini, valor = "001", habilitado = habilitado }.ToString() + "-" + new Input { id = "txtPVENTAPRO", clase = Css.mini, valor = "001", habilitado = habilitado }.ToString() + "-" + new Input { id = "txtNUMEROPRO", clase = Css.small, habilitado = habilitado, largo = 9 }.ToString();
            html2.AppendLine(tdtra.ToString());
            html2.AppendLine(" </div><!--span6-->");



            html2.AppendLine("<div class=\"span6\">");
            HtmlTable tdtra1 = new HtmlTable();
            tdtra1.CreteEmptyTable(3, 2);


            tdtra1.rows[0].cells[0].valor = "Guia Remisión:";
            tdtra1.rows[0].cells[1].valor = new Input { id = "txtALMACENPRO", clase = Css.mini, valor = (string.IsNullOrEmpty(obj.ccomenv.cenv_guia1)) ? "001" : obj.ccomenv.cenv_guia1, habilitado = habilitado }.ToString() + "-" + new Input { id = "txtPVENTAPRO", clase = Css.mini, valor = (string.IsNullOrEmpty(obj.ccomenv.cenv_guia2)) ? "001" : obj.ccomenv.cenv_guia2, habilitado = habilitado }.ToString() + "-" + new Input { id = "txtNUMEROPRO", clase = Css.small, valor = obj.ccomenv.cenv_guia3, habilitado = habilitado, largo = 9 }.ToString() + " Viajes:" + new Input { id = "txtGUIAS", clase = Css.mini, valor = obj.ccomenv.cenv_guias, habilitado = habilitado}.ToString();

            tdtra1.rows[1].cells[0].valor = "Socio:";
            tdtra1.rows[1].cells[1].valor = new Input { id = "txtSOCIO", clase = Css.large, habilitado = true, valor = obj.ccomenv.cenv_nombres_soc }.ToString() + new Input { id = "txtCODSOC", clase = Css.medium, valor = (hojas.Count > 0 && hojas[0].codigosocio != null) ? hojas[0].codigosocio.Value.ToString() : "", visible = false }.ToString();
            tdtra1.rows[2].cells[0].valor = "Chofer:";
            tdtra1.rows[2].cells[1].valor = new Input { id = "txtCHOFER", clase = Css.large, habilitado = true, valor = obj.ccomenv.cenv_nombres_cho }.ToString() + new Input { id = "txtCODCHO", clase = Css.medium, valor = (hojas.Count > 0 && hojas[0].codigochofer != null) ? hojas[0].codigochofer.Value.ToString() : "", visible = false }.ToString();

            //tdtra1.rows[0].cells[0].valor = "Vehiculo:";
            //tdtra1.rows[0].cells[1].valor = new Input { id = "txtCODVEH", clase = Css.medium, valor = vehiculo.veh_codigo, visible = false }.ToString() + new Input { id = "txtIDVEH", clase = Css.medium, valor = vehiculo.veh_id, habilitado = false }.ToString() + " " + new Input { id = "txtVEHICULO", clase = Css.large, habilitado = false, valor = vehiculo.veh_placa + " " + vehiculo.veh_disco + " " + vehiculo.veh_anio }.ToString() + new Input { id = "txtPLACAVEH", valor = vehiculo.veh_placa, visible = false }.ToString() + new Input { id = "txtDISCOVEH",  valor = vehiculo.veh_disco, visible = false }.ToString();
            //tdtra1.rows[1].cells[0].valor = "Socio:";
            //tdtra1.rows[1].cells[1].valor = new Input { id = "txtCODSOC", clase = Css.medium, valor = socio.per_codigo, visible = false }.ToString() + new Input { id = "txtIDSOC", clase = Css.medium, valor = socio.per_id, habilitado = false }.ToString() + " " + new Input { id = "txtSOCIO", clase = Css.large, habilitado = false, valor = socio.per_apellidos + " " + socio.per_nombres }.ToString();
            //tdtra1.rows[2].cells[0].valor = "Chofer:";
            //tdtra1.rows[2].cells[1].valor = new Input { id = "txtCODCHO", clase = Css.medium, valor = chofer.per_codigo, visible = false }.ToString() + new Input { id = "txtIDCHO", clase = Css.medium, valor = chofer.per_id, habilitado = false }.ToString() + " " + new Input { id = "txtCHOFER", clase = Css.large, habilitado = true, valor = chofer.per_apellidos + " " + chofer.per_nombres }.ToString();
            html2.AppendLine(tdtra1.ToString());
            html2.AppendLine(" </div><!--span6-->");
            html2.AppendLine("</div><!--row-fluid-->");

            //FALTA CARGAR LOS VALORES ADICIONALES

            string[] arrayporcseguro = Constantes.cPorcSeguro.Split('|');
            decimal valorporcseguro = 0;

            if (arrayporcseguro.Length > 0)
                valorporcseguro = decimal.Parse(arrayporcseguro[0]);
            if (arrayporcseguro.Length > 1)
            {
                if (obj.com_almacen.HasValue && obj.com_pventa.HasValue)
                {
                    for (int i = 1; i < arrayporcseguro.Length; i++)
                    {
                        string[] arrayvalores = arrayporcseguro[i].Split(';');

                        if (obj.com_almacen.Value.ToString() == arrayvalores[0] && obj.com_pventa.Value.ToString() == arrayvalores[1])
                        {
                            valorporcseguro = decimal.Parse(arrayvalores[2]);
                            break;
                        }

                    }
                }

            }


            StringBuilder html3 = new StringBuilder();
            html3.AppendLine("<div class=\"row-fluid\">");
            html3.AppendLine("<div class=\"span6\">");
            HtmlTable tdseg = new HtmlTable();
            tdseg.CreteEmptyTable(2, 2);
            tdseg.rows[0].cells[0].valor = "Valor declarado/ Porcentaje:";
            tdseg.rows[0].cells[1].valor = new Input { id = "txtVALORDECLARADO", clase = Css.small, numeric = true, valor = Formatos.CurrencyFormat(obj.total.tot_vseguro), habilitado = habilitado }.ToString() + " " + new Input { id = "txtPORCSEGURO", clase = Css.mini, numeric = true, valor = Formatos.CurrencyFormat((obj.total.tot_porc_seguro.HasValue) ? obj.total.tot_porc_seguro : valorporcseguro), habilitado = false }.ToString() + "%";
            tdseg.rows[1].cells[0].valor = "Entrega domicilio:";
            tdseg.rows[1].cells[1].valor = new Input { id = "txtVDOMICILIO", clase = Css.small, numeric = true, valor = Formatos.CurrencyFormat(obj.total.tot_transporte), habilitado = habilitado }.ToString();
            html3.AppendLine(tdseg.ToString());
            html3.AppendLine(" </div><!--span6-->");
            html3.AppendLine("</div><!--row-fluid-->");



            StringBuilder htmlobs = new StringBuilder();
            htmlobs.AppendLine("<div class=\"row-fluid\">");
            htmlobs.AppendLine("<div class=\"span12\">");
            HtmlTable tdobs = new HtmlTable();
            tdobs.CreteEmptyTable(1, 2);
            tdobs.rows[0].cells[0].valor = "Observaciónes:";
            tdobs.rows[0].cells[1].valor = new Textarea { id = "txtOBSERVACIONES", clase = Css.xxlarge, valor = obj.com_concepto, habilitado = habilitado }.ToString();
            htmlobs.AppendLine(tdobs.ToString());
            htmlobs.AppendLine(" </div><!--span12-->");
            htmlobs.AppendLine("</div><!--row-fluid-->");



            tabs.Add(new Tab("tab1", "Datos Comprobante", html.ToString()));
            tabs.Add(new Tab("tab2", "Remitente/Destinatario", html1.ToString()));
            tabs.Add(new Tab("tab3", "Transportación", html2.ToString()));
            tabs.Add(new Tab("tab4", "Seguro & Domicilio", html3.ToString()));
            tabs.Add(new Tab("tab5", "Retiro", htmlr.ToString()));
            tabs.Add(new Tab("tab6", "Observacion", htmlobs.ToString()));
            tabs.Add(new Tab("tab7", "&nbsp;", ""));


            return new Tabs { id = "tabcomprobante", tabs = tabs }.ToString();





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
        public static string GetCabeceraFromLGC(object objeto)
        {


            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);

            bool habilitado = true;
            //if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
            //    habilitado = false;

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

            Persona agente = new Persona();
            agente.per_empresa = comprobante.com_empresa;
            agente.per_empresa_key = comprobante.com_empresa;
            if (comprobante.com_agente.HasValue)
            {
                agente.per_codigo = comprobante.com_agente.Value;
                agente.per_codigo_key = comprobante.com_agente.Value;
                agente = PersonaBLL.GetByPK(agente);
            }

            List<Tab> tabs = new List<Tab>();
           
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Cliente:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtCODCLIPRO", valor = persona.per_id, autocomplete = "GetPersonaObj", obligatorio = true, clase = Css.medium, placeholder = "Cliente", habilitado = habilitado }.ToString() + " " + new Input { id = "txtNOMBRES", clase = Css.large, habilitado = false, valor = persona.per_apellidos + " " + persona.per_nombres }.ToString() + new Input { id = "txtCODPER", visible = false, valor = persona.per_codigo }.ToString() + new Boton { small = true, id = "btncallper", tooltip = "Agregar cliente", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cFactura + ")" }.ToString() + new Boton { small = true, id = "btncleanper", tooltip = "Limpiar datos persona", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();
            tdatos.rows[1].cells[0].valor = "CI-RUC/Razón Social:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtRUC", clase = Css.medium, habilitado = false, valor = persona.per_ciruc }.ToString() + " " + new Input { id = "txtRAZON", clase = Css.large, habilitado = false, valor = persona.per_razon }.ToString();
            tdatos.rows[2].cells[0].valor = "Teléfono/Dirección:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtTELEFONO", clase = Css.medium, habilitado = false, valor = persona.per_telefono }.ToString() + " " + new Input { id = "txtDIRECCION", clase = Css.large, habilitado = false, valor = persona.per_direccion }.ToString();
            //tdatos.rows[3].cells[0].valor = "Ubicación:";
            //tdatos.rows[3].cells[1].valor = new Input { id = "txtUBICA", clase = Css.large, habilitado = false }.ToString();



            Politica politica = new Politica();

            politica.pol_empresa = comprobante.com_empresa;
            politica.pol_empresa_key = comprobante.com_empresa;
            if (persona.per_politica.HasValue)
            {
                politica.pol_codigo = persona.per_politica.Value;
                politica.pol_codigo_key = persona.per_politica.Value;
                politica = PoliticaBLL.GetByPK(politica);
            }
            comprobante.com_tclipro = Constantes.cCliente;
            //FALTA CARGAR VENDEDOR

            List<vPlanillaTotal> guias = vPlanillaTotalBLL.GetAllN(new WhereParams("c.com_tipodoc={0} and c.com_codigo={1} ", 12, comprobante.com_codigo), " cantidad");
            List<vPlanillaTotal> nguias = vPlanillaTotalBLL.GetAllNP(new WhereParams("c.com_tipodoc={0} and c.com_codigo={1} ", 12, comprobante.com_codigo), " numero ");
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");

            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(3, 2);
            tdatos1.rows[0].cells[0].valor = "Lista Precio:";
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtIDLIS", autocomplete = "GetListaObj", clase = Css.small, valor = persona.per_listaid, habilitado = false }.ToString() + " " + new Input { id = "txtLISTA", clase = Css.large, habilitado = false, valor = persona.per_listanombre }.ToString() + " " + new Input { id = "txtCODLIS", visible = false, valor = persona.per_listaprecio }.ToString();
            //tdatos1.rows[0].cells[1].valor = new Select { id = "cmbLISTAPRECIO", diccionario = Dictionaries.GetListaprecio(), clase = Css.medium}.ToString();
            tdatos1.rows[1].cells[0].valor = "Política Venta:";
            //tdatos1.rows[1].cells[1].valor = new Input { id = "txtIDPOL", autocomplete = "GetPoliticaObj", clase = Css.small, valor = politica.pol_id, obligatorio = true, placeholder = "Política", habilitado = habilitado }.ToString() + " " + new Input { id = "txtPOLITICA", clase = Css.large, habilitado = false, valor = politica.pol_nombre }.ToString() + " " + new Input { id = "txtPORCENTAJE", clase = Css.mini, numeric = true, valor = politica.pol_porc_desc, habilitado = false }.ToString() + "% Desc" + new Input { id = "txtCODPOL", visible = false, valor = politica.pol_codigo }.ToString() + new Input { id = "txtNROPAGOS", visible = false, valor = politica.pol_nro_pagos }.ToString() + new Input { id = "txtDIASPLAZO", visible = false, valor = politica.pol_dias_plazo }.ToString() + new Input { id = "txtPORCPAGOCON", visible = false, valor = politica.pol_porc_pago_con }.ToString();
            tdatos1.rows[1].cells[1].valor = new Select { id = "cmbPOLITICA", diccionario = Dictionaries.GetPolitica(comprobante), clase = Css.medium }.ToString() + " " + new Input { id = "txtPORCENTAJE", clase = Css.mini, numeric = true, valor = politica.pol_porc_desc, habilitado = false }.ToString() + "% Desc" + new Input { id = "txtNROPAGOS", visible = false, valor = politica.pol_nro_pagos }.ToString() + new Input { id = "txtDIASPLAZO", visible = false, valor = politica.pol_dias_plazo }.ToString() + new Input { id = "txtPORCPAGOCON", visible = false, valor = politica.pol_porc_pago_con }.ToString();
            //tdatos1.rows[1].cells[1].valor = new Select { id = "cmbPOLITICA", diccionario = Dictionaries.GetPolitica(), clase = Css.medium }.ToString() ;

            tdatos1.rows[2].cells[0].valor = "Agente:";
            tdatos1.rows[2].cells[1].valor = new Input { id = "txtIDAGE", valor = agente.per_id, autocomplete = "GetPersonaObj", clase = Css.medium, placeholder = "Agente", habilitado = habilitado }.ToString() + " " + new Input { id = "txtNOMBRESAGE", clase = Css.large, habilitado = false, valor = agente.per_apellidos + " " + agente.per_nombres }.ToString() + new Input { id = "txtCODAGE", visible = false, valor = agente.per_codigo }.ToString() + new Boton { small = true, id = "btncallage", tooltip = "Agregar Agente", clase = "iconsweets-user", click = "CallPersona(this.id," + Constantes.cCliente + ")" }.ToString() + new Boton { small = true, id = "btncleanage", tooltip = "Limpiar datos agente", clase = "iconsweets-refresh", click = "CleanPersona(this.id)" }.ToString();
            //tdatos1.rows[2].cells[1].valor = new Input { id = "txtCODVEN", autocomplete = "GetPersonaObj",  valor= guias[0].numero,clase = Css.small, habilitado = false }.ToString() + " " + new Input { id = "txtVENDEDOR", clase = Css.large, habilitado = false }.ToString();

            html.AppendLine(tdatos1.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");

            //tdatos1.rows[2].cells[0].valor = "Bodega:";
            //tdatos1.rows[2].cells[1].valor = new Input { id = "txtCODBOD", autocomplete = "GetBodegaObj", clase = Css.small }.ToString() + " " + new Input { id = "txtBODEGA", clase = Css.large, habilitado = false }.ToString();

            List<vPlanillaTotal> vehiculo = vPlanillaTotalBLL.GetAllS(new WhereParams("c.com_tipodoc={0} and c.com_codigo={1}   group by ve.veh_codigo,per_codigo,ve.veh_placa,p.per_nombres,p.per_apellidos", 12, comprobante.com_codigo), "ve.veh_codigo");
            string placas = "";
            foreach (vPlanillaTotal item in vehiculo)
            {

                placas = placas+" "+item.veh_placa;


            }
            if (!string.IsNullOrEmpty(placas))
                placas = "PLACAS: " + placas;
            StringBuilder htmlobs = new StringBuilder();
            htmlobs.AppendLine("<div class=\"row-fluid\">");
            htmlobs.AppendLine("<div class=\"span12\">");
            HtmlTable tdobs = new HtmlTable();
            tdobs.CreteEmptyTable(2, 2);
            tdobs.rows[0].cells[0].valor = "Observaciónes:";
            tdobs.rows[0].cells[1].valor = new Textarea { id = "txtOBSERVACIONES", clase = Css.xxlarge, valor = placas, habilitado = habilitado }.ToString();
            tdobs.rows[1].cells[0].valor = "Viajes:";
            tdobs.rows[1].cells[1].valor = new Input{ id = "txtGUIAS", clase = Css.mini, valor = nguias[0].numero }.ToString();


            htmlobs.AppendLine(tdobs.ToString());
            htmlobs.AppendLine(" </div><!--span12-->");
            htmlobs.AppendLine("</div><!--row-fluid-->");


            tabs.Add(new Tab("tab1", "Datos Comprobante", html.ToString()));
            tabs.Add(new Tab("tab2", "Observaciones", htmlobs.ToString()));


            return new Tabs { id = "tabcomprobante", tabs = tabs }.ToString();
          //  return html.ToString();


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

            comprobante.ccomdoc = new Ccomdoc();
            comprobante.ccomdoc.cdoc_empresa = comprobante.com_empresa;
            comprobante.ccomdoc.cdoc_empresa_key = comprobante.com_empresa;
            comprobante.ccomdoc.cdoc_comprobante = comprobante.com_codigo;
            comprobante.ccomdoc.cdoc_comprobante_key = comprobante.com_codigo;
            comprobante.ccomdoc = CcomdocBLL.GetByPK(comprobante.ccomdoc);

            comprobante.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa={0} and ddoc_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "ddoc_secuencia");

            List<Dcalculoprecio> detallecalculo = DcalculoprecioBLL.GetAll(new WhereParams("dcpr_empresa={0} and dcpr_comprobante={1}", comprobante.com_empresa, comprobante.com_codigo), "dcpr_secuencia");


            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdinvoice";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";

            tdatos.AddColumn("Id", "width10", "", new Input() { id = "txtIDPRO", placeholder = "ID", autocomplete = "GetProductoObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODPRO", visible = false }.ToString() + new Input() { id = "txtCALCPRO", visible = false }.ToString());
            tdatos.AddColumn("Producto", "width20", "", new Input() { id = "txtPRODUCTO", placeholder = "DESCRIPCION", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("Observación", "width20", "", new Textarea() { id = "txtOBSERVACION", placeholder = "OBSERVACIÓN", clase = Css.blocklevel }.ToString());
            //tdatos.AddColumn("Peso(lbs)", "width5", Css.right, new Input() { id = "txtPESO", placeholder = "Peso", clase = Css.blocklevel + Css.amount }.ToString());
            tdatos.AddColumn("Caracter.", "width10", "", "");
            tdatos.AddColumn("U.Medida", "width10", "", new Select() { id = "cmbUMEDIDA", placeholder = "Medida", diccionario = Dictionaries.GetUmedida(), clase = Css.blocklevel }.ToString() + new Input() { id = "txtFACTOR", visible = false });
            tdatos.AddColumn("Cant.", "width5", Css.center, new Input() { id = "txtCANTIDAD", placeholder = "CANT", clase = Css.blocklevel + Css.cantidades, numeric = true }.ToString());
            tdatos.AddColumn("Precio", "width10", Css.right, new Input() { id = "txtPRECIO", placeholder = "PRECIO", clase = Css.blocklevel + Css.amount, numeric = true }.ToString());
            tdatos.AddColumn("Desc.", "width5", Css.right, new Input() { id = "txtDESC", placeholder = "DESC", clase = Css.blocklevel + Css.amount}.ToString());
            tdatos.AddColumn("TOTAL", "width10", Css.right, new Input() { id = "txtTOTAL", placeholder = "TOTAL", clase = Css.blocklevel + Css.amount, habilitado = true }.ToString());
            tdatos.AddColumn("IVA", "width5", Css.center, new Check() { id = "chkIVA", clase = Css.blocklevel + Css.cantidades, habilitado = false, valor = 0 }.ToString());
            if (habilitado)
                tdatos.AddColumn("", "width5", Css.center, new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());

            //tdatos.editable = true;
            tdatos.editable = habilitado;

            foreach (Dcomdoc item in comprobante.ccomdoc.detalle)
            {
                item.detallecalculo = detallecalculo.FindAll(delegate(BusinessObjects.Dcalculoprecio c) { return c.dcpr_dcomdoc == item.ddoc_secuencia; });
                string calculo = "";
                string datacal = "";
                foreach (Dcalculoprecio dc in item.detallecalculo)
                {
                    calculo += dc.dcpr_nombre + ":" + dc.dcpr_indicedigitado + "<br/>";
                    datacal += " data-cp" + dc.dcpr_secuencia + " = '{\"nombre\":\"" + dc.dcpr_nombre + "\", \"indice\":\"" + dc.dcpr_indice + "\", \"valor\":\"" + dc.dcpr_valor + "\", \"peso\":\"" + dc.dcpr_peso + "\", \"valordig\":\"" + dc.dcpr_indicedigitado + "\"}' ";

                }


                HtmlRow row = new HtmlRow();
                row.data = "data-codpro=" + item.ddoc_producto + " data-calcpro=" + item.ddoc_productocalcula;
                row.removable = true;

                row.cells.Add(new HtmlCell { valor = item.ddoc_productoid });
                row.cells.Add(new HtmlCell { valor = item.ddoc_productonombre });
                row.cells.Add(new HtmlCell { valor = item.ddoc_observaciones });
                //row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddoc_peso), clase = Css.right});
                row.cells.Add(new HtmlCell { valor = calculo, data = datacal });
                row.cells.Add(new HtmlCell { valor = item.ddoc_productounidad, data = "data-coduni=" + item.ddco_udigitada });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddoc_cantidad), clase = Css.center });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormatAll(item.ddoc_precio), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddoc_dscitem), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.ddoc_total), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = ((item.ddoc_productoiva.HasValue) ? ((item.ddoc_productoiva.Value == 1) ? "SI" : "NO") : "NO"), clase = Css.center });
                //if (comprobante.com_estado != Constantes.cEstadoMayorizado)
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
        public static string GetDetalleFromLGC(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            bool habilitado = true;
            //if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
            //    habilitado = false;

            comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);

            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdinvoice";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";

            tdatos.AddColumn("Id", "width10", "", new Input() { id = "txtIDPRO", placeholder = "ID", autocomplete = "GetProductoObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODPRO", visible = false }.ToString() + new Input() { id = "txtCALCPRO", visible = false }.ToString());
            tdatos.AddColumn("Producto", "width20", "", new Input() { id = "txtPRODUCTO", placeholder = "DESCRIPCION", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("Observación", "width20", "", new Textarea() { id = "txtOBSERVACION", placeholder = "OBSERVACIÓN", clase = Css.blocklevel }.ToString());
            tdatos.AddColumn("Peso(lbs)", "width5", Css.right, new Input() { id = "txtPESO", placeholder = "Peso", clase = Css.blocklevel + Css.amount }.ToString());
            tdatos.AddColumn("U.Medida", "width10", "", new Select() { id = "cmbUMEDIDA", placeholder = "Medida", diccionario = Dictionaries.GetUmedida(), clase = Css.blocklevel }.ToString() + new Input() { id = "txtFACTOR", visible = false });
            tdatos.AddColumn("Cant.", "width5", Css.center, new Input() { id = "txtCANTIDAD", placeholder = "CANT", clase = Css.blocklevel + Css.cantidades, numeric = true }.ToString());
            tdatos.AddColumn("Precio", "width10", Css.right, new Input() { id = "txtPRECIO", placeholder = "PRECIO", clase = Css.blocklevel + Css.amount, habilitado = false }.ToString());
            tdatos.AddColumn("Desc.%", "width5", Css.right, new Input() { id = "txtDESC", placeholder = "DESC", clase = Css.blocklevel + Css.amount }.ToString());
            tdatos.AddColumn("TOTAL", "width10", Css.right, new Input() { id = "txtTOTAL", placeholder = "TOTAL", clase = Css.blocklevel + Css.amount, habilitado = false }.ToString());
            tdatos.AddColumn("IVA", "width5", Css.center, new Check() { id = "chkIVA", clase = Css.blocklevel + Css.cantidades, habilitado = false, valor = 1 }.ToString());
            if (habilitado)
                tdatos.AddColumn("", "width5", Css.center, new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());


            tdatos.editable = habilitado;

            Producto pro = Constantes.GetProductoPlanilla();
            //Producto pro = ProductoBLL.GetByPK(new Producto { pro_empresa = comprobante.com_empresa, pro_empresa_key = comprobante.com_empresa, pro_codigo = 21, pro_codigo_key = 21 });
            Umedida umed = UmedidaBLL.GetByPK(new Umedida { umd_empresa = comprobante.com_empresa, umd_empresa_key = comprobante.com_empresa, umd_codigo = pro.pro_unidad.Value, umd_codigo_key = pro.pro_unidad.Value });

            HtmlRow row = new HtmlRow();
            row.data = "data-codpro=" + pro.pro_codigo + " data-calcpro=''";
            row.removable = true;

            row.cells.Add(new HtmlCell { valor = pro.pro_id });
            row.cells.Add(new HtmlCell { valor = pro.pro_nombre });
            row.cells.Add(new HtmlCell { valor = comprobante.com_doctran });
            row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(0), clase = Css.right });
            row.cells.Add(new HtmlCell { valor = umed.umd_nombre, data = "data-coduni=" + pro.pro_unidad });
            row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(1), clase = Css.center });
            row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormatAll(comprobante.total.tot_subtot_0 + comprobante.total.tot_subtotal), clase = Css.right });
            row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(0), clase = Css.right });
            row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(comprobante.total.tot_subtot_0 + comprobante.total.tot_subtotal), clase = Css.right });
            row.cells.Add(new HtmlCell { valor = ((pro.pro_iva.HasValue) ? ((pro.pro_iva.Value == 1) ? "SI" : "NO") : "NO"), clase = Css.center });
            //if (comprobante.com_estado != Constantes.cEstadoMayorizado)
            if (habilitado)
            {
                row.cells.Add(new HtmlCell { valor = new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString(), clase = Css.center });
                row.clickevent = "Edit(this)";
            }
            tdatos.AddRow(row);
            //tdatos.AddRow(new HtmlRow(item.ddoc_productoid, item.ddoc_productonombre, item.ddoc_observaciones, item.ddoc_productounidad, item.ddoc_cantidad, item.ddoc_precio, item.ddoc_dscitem, item.ddoc_total, item.ddoc_productoiva) { data = "data-codpro=" + item.ddoc_producto });   



            html.AppendLine(tdatos.ToString());



            return html.ToString();
        }




        [WebMethod]
        public static string GetDetalleFromLGC1(object objeto)
        {

            Comprobante comprobante = new Comprobante(objeto);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;

            comprobante = ComprobanteBLL.GetByPK(comprobante);

            bool habilitado = true;
            //if (comprobante.com_estado == Constantes.cEstadoEliminado || comprobante.com_estado == Constantes.cEstadoMayorizado)
            //    habilitado = false;

            comprobante.total = new Total();
            comprobante.total.tot_empresa = comprobante.com_empresa;
            comprobante.total.tot_empresa_key = comprobante.com_empresa;
            comprobante.total.tot_comprobante = comprobante.com_codigo;
            comprobante.total.tot_comprobante_key = comprobante.com_codigo;
            comprobante.total = TotalBLL.GetByPK(comprobante.total);

            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tdinvoice";
            tdatos.invoice = true;
            //tdatos.titulo = "Factura";

            tdatos.AddColumn("Id", "width10", "", new Input() { id = "txtIDPRO", placeholder = "ID", autocomplete = "GetProductoObj", clase = Css.blocklevel }.ToString() + new Input() { id = "txtCODPRO", visible = false }.ToString() + new Input() { id = "txtCALCPRO", visible = false }.ToString());
            tdatos.AddColumn("Producto", "width20", "", new Input() { id = "txtPRODUCTO", placeholder = "DESCRIPCION", clase = Css.blocklevel, habilitado = false }.ToString());
            tdatos.AddColumn("Observación", "width20", "", new Textarea() { id = "txtOBSERVACION", placeholder = "OBSERVACIÓN", clase = Css.blocklevel }.ToString());
            tdatos.AddColumn("", "width5", Css.right, new Input() { id = "txtPESO", placeholder = "Peso", clase = Css.blocklevel + Css.amount }.ToString());
            tdatos.AddColumn("U.Medida", "width10", "", new Select() { id = "cmbUMEDIDA", placeholder = "Medida", diccionario = Dictionaries.GetUmedida(), clase = Css.blocklevel }.ToString() + new Input() { id = "txtFACTOR", visible = false });
            tdatos.AddColumn("Cant.", "width5", Css.center, new Input() { id = "txtCANTIDAD", placeholder = "CANT", clase = Css.blocklevel + Css.cantidades, numeric = true }.ToString());
            tdatos.AddColumn("Precio", "width10", Css.right, new Input() { id = "txtPRECIO", placeholder = "PRECIO", clase = Css.blocklevel + Css.amount, habilitado = false }.ToString());
            tdatos.AddColumn("", "width5", Css.right, new Input() { id = "txtDESC", placeholder = "DESC", clase = Css.blocklevel + Css.amount }.ToString());
            tdatos.AddColumn("TOTAL", "width10", Css.right, new Input() { id = "txtTOTAL", placeholder = "TOTAL", clase = Css.blocklevel + Css.amount, habilitado = false }.ToString());
            tdatos.AddColumn("IVA", "width5", Css.center, new Check() { id = "chkIVA", clase = Css.blocklevel + Css.cantidades, habilitado = false, valor = 1 }.ToString());
            if (habilitado)
                tdatos.AddColumn("", "width5", Css.center, new Boton { removerow = true, tooltip = "Eliminar registro" }.ToString());


            tdatos.editable = habilitado;

           // Producto pro = Constantes.GetProductoPlanilla();
            //Producto pro = ProductoBLL.GetByPK(new Producto { pro_empresa = comprobante.com_empresa, pro_empresa_key = comprobante.com_empresa, pro_codigo = 21, pro_codigo_key = 21 });
           // Umedida umed = UmedidaBLL.GetByPK(new Umedida { umd_empresa = comprobante.com_empresa, umd_empresa_key = comprobante.com_empresa, umd_codigo = pro.pro_unidad.Value, umd_codigo_key = pro.pro_unidad.Value });
            List<vPlanillaTotal> totales = vPlanillaTotalBLL.GetAll(new WhereParams("c.com_tipodoc={0} and c.com_codigo={1} and dlpr_estado={2}  group by p.pro_codigo,p.pro_nombre,p.pro_id,cen.cenv_ruta,r.rut_nombre,p.pro_unidad,dlpr_precio,p.pro_codigo", 12, comprobante.com_codigo, 1), "p.pro_codigo");
           
            foreach (vPlanillaTotal item in totales)
            {
                HtmlRow row = new HtmlRow();
                row.data = "data-codpro=" + item.pro_codigo + " data-calcpro=''";
                row.removable = true;

                row.cells.Add(new HtmlCell { valor = item.pro_id });
                row.cells.Add(new HtmlCell { valor = item.producto });
                row.cells.Add(new HtmlCell { valor = item.ruta });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(0), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = item.pro_unidad, data = "data-coduni=" + item.pro_unidad });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.cantidad) });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormatAll(item.precio), clase = Css.right });
                row.cells.Add(new HtmlCell { valor = 0, clase = Css.right });
                row.cells.Add(new HtmlCell { valor = Formatos.CurrencyFormat(item.total + item.descuento), clase = Css.right });
                row.cells.Add(new HtmlCell { valor =  "NO", clase = Css.center });
                //if (comprobante.com_estado != Constantes.cEstadoMayorizado)
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
        public static string GetPie(object objeto)
        {
            Comprobante comprobante = new Comprobante(objeto);
            DateTime fecha = comprobante.com_fecha;
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
            comprobante.total.tot_subtot_0 += comprobante.total.tot_transporte;
            comprobante.total.tot_subtotal += (comprobante.total.tot_tseguro.HasValue) ? comprobante.total.tot_tseguro.Value : 0;

            StringBuilder html = new StringBuilder();

            HtmlTable tdatos2 = new HtmlTable();
            tdatos2.CreteEmptyTable(2, 2);
            tdatos2.rows[0].cells[0].valor = "SEGURO:";
            tdatos2.rows[0].cells[1].valor = new Input { id = "txtSEGURO", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_tseguro) }.ToString();
            tdatos2.rows[1].cells[0].valor = "TRANSPORTE:";
            tdatos2.rows[1].cells[1].valor = new Input { id = "txtTRANSPORTE", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_transporte) }.ToString();

            html.AppendLine(tdatos2.ToString());

            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(4, 3);
            tdatos.rows[0].cells[0].valor = "";
            tdatos.rows[0].cells[1].valor = "0%";
            tdatos.rows[0].cells[1].clase = Css.center;
            tdatos.rows[0].cells[2].valor = "IVA";
            tdatos.rows[0].cells[2].clase = Css.center;

            tdatos.rows[1].cells[0].valor = "Subtotal:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtSUBTOTAL0", clase = Css.small + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_subtot_0) }.ToString();
            tdatos.rows[1].cells[2].valor = new Input { id = "txtSUBTOTALIVA", clase = Css.small + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_subtotal) }.ToString();
            tdatos.rows[2].cells[0].valor = "Desc:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtDESC0", clase = Css.small + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_desc1_0) }.ToString();
            tdatos.rows[2].cells[2].valor = new Input { id = "txtDESCIVA", clase = Css.small + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_descuento1) }.ToString();
            tdatos.rows[3].cells[0].valor = "Descuento:";
            tdatos.rows[3].cells[1].valor = new Input { id = "txtDESCUENTO0", clase = Css.small + Css.amount, numeric = true, valor = Formatos.CurrencyFormat(comprobante.total.tot_desc2_0), habilitado = habilitado }.ToString();
            tdatos.rows[3].cells[2].valor = new Input { id = "txtDESCUENTOIVA", clase = Css.small + Css.amount, numeric = true, valor = Formatos.CurrencyFormat(comprobante.total.tot_descuento2), habilitado = habilitado }.ToString();
            html.AppendLine(tdatos.ToString());

            HtmlTable tdatos1 = new HtmlTable();
            tdatos1.CreteEmptyTable(2, 2);
            decimal valoriva = Constantes.GetValorIVA(fecha);
            if (comprobante.total.tot_porc_impuesto.HasValue)
                valoriva = comprobante.total.tot_porc_impuesto.Value;

            tdatos1.rows[0].cells[0].valor = "IVA " + valoriva + "%:";
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtIVAPORCENTAJE", visible = false, valor = valoriva.ToString().Replace(",", ".") }.ToString() + new Input { id = "txtIVA", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_timpuesto) }.ToString();
            tdatos1.rows[1].cells[0].valor = "TOTAL:";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtTOTALCOM", clase = Css.medium + Css.totalamount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_total) }.ToString();

            /*tdatos1.rows[0].cells[0].valor = "IVA "+valoriva+"%:"; 
            tdatos1.rows[0].cells[1].valor = new Input { id = "txtIVAPORCENTAJE",visible = false , valor = valoriva.ToString() }.ToString() + new Input { id = "txtIVA", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_timpuesto) }.ToString();
            tdatos1.rows[1].cells[0].valor = "SEGURO:";
            tdatos1.rows[1].cells[1].valor = new Input { id = "txtSEGURO", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_tseguro) }.ToString();
            tdatos1.rows[2].cells[0].valor = "TRANSPORTE:";
            tdatos1.rows[2].cells[1].valor = new Input { id = "txtTRANSPORTE", clase = Css.medium + Css.amount, habilitado = false, valor = Formatos.CurrencyFormat(comprobante.total.tot_transporte)  }.ToString();
            tdatos1.rows[3].cells[0].valor = "TOTAL:";            
            tdatos1.rows[3].cells[1].valor = new Input { id = "txtTOTALCOM", clase = Css.medium  + Css.totalamount, habilitado = false, valor= Formatos.CurrencyFormat(comprobante.total.tot_total) }.ToString();*/

            html.AppendLine(tdatos1.ToString());

            return html.ToString();

        }

        [WebMethod]
        public static string GetProduct(object objeto)
        {
            Producto product = new Producto();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object id = null;
                object almacen = null;
                object lista = null;
                object ruta = null;

                tmp.TryGetValue("producto", out id);
                tmp.TryGetValue("almacen", out almacen);
                tmp.TryGetValue("lista", out lista);
                tmp.TryGetValue("ruta", out ruta);

                List<Producto> lst = new List<Producto>();
                lst = ProductoBLL.GetAll("pro_empresa=1 and pro_id='" + id.ToString() + "'", "");
                if (lst.Count > 0)
                {
                    product = lst[0];
                    product.factores = FactorBLL.GetAll("fac_producto=" + product.pro_codigo, "");
                    product.tproducto = TproductoBLL.GetByPK(new Tproducto { tpr_empresa = product.pro_empresa, tpr_empresa_key = product.pro_empresa, tpr_codigo = product.pro_tproducto.Value, tpr_codigo_key = product.pro_tproducto.Value });
                    product.tproducto.calculaprecio = CalculoprecioBLL.GetAll(new WhereParams("cpr_empresa ={0} and cpr_tproducto={1} and cpr_listaprecio = {2} and cpr_almacen={3} and cpr_ruta={4}", product.pro_empresa, product.pro_tproducto, int.Parse(lista.ToString()), int.Parse(almacen.ToString()), int.Parse(ruta.ToString())), "");
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
                object almacen = null;
                object ruta = null;

                tmp.TryGetValue("producto", out producto);
                tmp.TryGetValue("lista", out lista);
                tmp.TryGetValue("almacen", out almacen);
                tmp.TryGetValue("unidad", out unidad);
                tmp.TryGetValue("ruta", out ruta);
                //tmp.TryGetValue("fecha", out fecha);

                List<Dlistaprecio> lst = DlistaprecioBLL.GetAll(new WhereParams("dlpr_empresa = {0} and dlpr_producto = {1} and dlpr_listaprecio = {2} and dlpr_umedida= {3} and dlpr_almacen={5} and dlpr_ruta ={6} and ((dlpr_fecha_ini >= {4} and dlpr_fecha_fin IS NULL) or ({4} between dlpr_fecha_ini and dlpr_fecha_fin))", 1, int.Parse(producto.ToString()), int.Parse(lista.ToString()), int.Parse(unidad.ToString()), DateTime.Now, int.Parse(almacen.ToString()), int.Parse(ruta.ToString())), "");
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
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object objetocomp = null;
            object objetodrec = null;
            object objetorfac = null;
            tmp.TryGetValue("comprobante", out objetocomp);
            tmp.TryGetValue("drecibo", out objetodrec);
            tmp.TryGetValue("rutaxfactura", out objetorfac);

            Comprobante obj = new Comprobante(objetocomp);

            int? socio = obj.ccomenv.cenv_socio;

            List<Drecibo> detalle = new List<Drecibo>();
            if (objetodrec != null)
            {
                Array array = (Array)objetodrec;
                foreach (Object item in array)
                {
                    if (item != null)
                        detalle.Add(new Drecibo(item));
                }
            }

            Rutaxfactura rfac = new Rutaxfactura(objetorfac);

            try
            {
                if (obj.com_codigo > 0)
                {

                    FAC.update_planilla(obj);
                    obj = FAC.update_factura(obj, rfac);

                }
                else
                    obj = FAC.save_factura(obj, rfac);

                if (obj.com_tipodoc == Constantes.cFactura.tpd_codigo)
                    obj = FAC.account_factura(obj);
                if (detalle.Count > 0)
                {
                    Comprobante can = FAC.save_cancelacion_factura(obj, detalle, DateTime.Now);

                    //if (obj.ccomdoc.cdoc_politica == 4 && socio.HasValue)//FLETE PAGADO
                    //    FAC.account_recibosocio(can, socio.Value);
                    //else
                    FAC.account_recibo(can);
                    //FATLA CONTABILZIAR CANCELACION
                }
                //NUEVO CODIGO PARA FACTURACION ELECTRONICA
                Packages.Electronico.GenerateElectronico(obj);

                return obj.com_codigo.ToString();
            }
            catch (Exception ex)
            {
                ExceptionHandling.Log.AddExepcion(ex);
                return obj.com_codigo.ToString();
            }







        }

        public static List<Ddocumento> GetDocumentos(Comprobante obj)
        {
            DateTime fecha = obj.com_fecha;
            List<Ddocumento> lista = new List<Ddocumento>();
            if (obj.planillacomp.pco_comprobante_pla > 0)
            {
                List<vGuias> lst = vGuiasBLL.GetAll(new WhereParams("plc_empresa={0} and plc_comprobante_pla={1}", obj.com_empresa, obj.planillacomp.pco_comprobante_pla), "");
                int contadorpago = 1;
                foreach (vGuias item in lst)
                {
                    decimal valor = item.tot_total / (decimal)Functions.Conversiones.GetValueByType(obj.total.tot_nro_pagos.Value, typeof(decimal));
                    for (int i = 0; i < obj.total.tot_nro_pagos.Value; i++)
                    {
                        fecha = fecha.AddDays(obj.total.tot_dias_plazo.Value);
                        Ddocumento doc = new Ddocumento();
                        doc.ddo_empresa = obj.com_empresa;
                        doc.ddo_comprobante = obj.com_codigo;
                        doc.ddo_transacc = 1;
                        doc.ddo_doctran = obj.com_doctran;
                        doc.ddo_pago = contadorpago;
                        doc.ddo_codclipro = obj.com_codclipro;
                        doc.ddo_debcre = Constantes.cDebito;
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
                        doc.ddo_comprobante_guia = item.com_codigo;
                        lista.Add(doc);
                        contadorpago++;
                    }
                }

            }
            else
            {

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
                    doc.ddo_debcre = Constantes.cDebito;
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
            }
            return lista;
        }


        public static Comprobante InsertComprobante(Comprobante obj, Rutaxfactura rfac, ref string mensaje)
        {

            Comprobante hr = new Comprobante();
            if (rfac.rfac_comprobanteruta > 0)
            {
                hr.com_empresa = obj.com_empresa;
                hr.com_empresa_key = obj.com_empresa;
                hr.com_codigo = rfac.rfac_comprobanteruta;
                hr.com_codigo_key = rfac.rfac_comprobanteruta;
                hr = ComprobanteBLL.GetByPK(hr);
                hr.total = new Total();
                hr.total.tot_empresa = hr.com_empresa;
                hr.total.tot_empresa_key = hr.com_empresa;
                hr.total.tot_comprobante = hr.com_codigo;
                hr.total.tot_comprobante_key = hr.com_codigo;
                hr.total = TotalBLL.GetByPK(hr.total);
            }







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
           // if (obj.com_nocontable == 1)
                //obj.com_concepto = "GUIA DE VENTA " + obj.ccomdoc.cdoc_nombre;
           // else
                //obj.com_concepto = "FACTURA DE VENTA " + obj.ccomdoc.cdoc_nombre;
            obj.com_modulo = General.GetModulo(obj.com_tipodoc); ;
            obj.com_transacc = General.GetTransacc(obj.com_tipodoc);
            obj.com_centro = Constantes.GetSinCentro().cen_codigo;
            obj.com_estado = 1;
            obj.com_descuadre = 0;
            obj.com_adestino = 0;
            obj.com_doctran = General.GetNumeroComprobante(obj);
            obj.com_tclipro = Constantes.cCliente;
            //obj.com_fecha = fecha;


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                obj.com_codigo = ComprobanteBLL.InsertIdentity(transaction, obj);
                //int codigo = ComprobanteBLL.InsertIdentity(transaction, obj);

                obj.ccomdoc.cdoc_empresa = obj.com_empresa;
                obj.ccomdoc.cdoc_comprobante = obj.com_codigo;
                CcomdocBLL.Insert(transaction, obj.ccomdoc);

                int contador = 0;
                foreach (Dcomdoc item in obj.ccomdoc.detalle)
                {
                    item.ddoc_empresa = obj.com_empresa;
                    item.ddoc_comprobante = obj.com_codigo;
                    item.ddoc_secuencia = contador;
                    DcomdocBLL.Insert(transaction, item);

                    foreach (Dcalculoprecio dc in item.detallecalculo)
                    {
                        dc.dcpr_dcomdoc = contador;
                        dc.dcpr_comprobante = obj.com_codigo;
                        DcalculoprecioBLL.Insert(transaction, dc);
                    }
                    contador++;
                }




                //ASIGNA LA FACTURA A LA PLANILLA DE CLIENTES
                if (obj.planillacomp.pco_comprobante_pla > 0)
                {
                    obj.planillacomp.pco_comprobante_fac = obj.com_codigo;
                    PlanillacomprobanteBLL.Insert(transaction, obj.planillacomp);
                }
                ////////////////////////////////////////////////////////////////
                else
                {
                    obj.ccomenv.cenv_empresa = obj.com_empresa;
                    obj.ccomenv.cenv_comprobante = obj.com_codigo;
                    CcomenvBLL.Insert(transaction, obj.ccomenv);
                }

                obj.total.tot_comprobante = obj.com_codigo;
                TotalBLL.Insert(transaction, obj.total);


                foreach (Ddocumento item in obj.documentos)
                {
                    item.ddo_comprobante = obj.com_codigo;
                    DdocumentoBLL.Insert(transaction, item);
                    contador++;
                }


                DtipocomBLL.Update(transaction, dti);
                if (rfac.rfac_comprobanteruta > 0)
                {
                    rfac.rfac_comprobantefac = obj.com_codigo;
                    RutaxfacturaBLL.Insert(transaction, rfac);
                    hr.total.tot_tseguro = hr.total.tot_tseguro + obj.total.tot_tseguro;
                    hr.total.tot_timpuesto = hr.total.tot_timpuesto + obj.total.tot_timpuesto;
                    hr.total.tot_transporte = hr.total.tot_transporte + obj.total.tot_transporte;
                    hr.total.tot_total = hr.total.tot_total + obj.total.tot_total;
                    TotalBLL.Update(transaction, hr.total);
                }
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
            object objetodrec = null;
            object objetorfac = null;
            tmp.TryGetValue("comprobante", out objetocomp);
            tmp.TryGetValue("drecibo", out objetodrec);
            tmp.TryGetValue("rutaxfactura", out objetorfac);

            Comprobante obj = new Comprobante(objetocomp);

            List<Drecibo> detalle = new List<Drecibo>();
            if (objetodrec != null)
            {
                Array array = (Array)objetodrec;
                foreach (Object item in array)
                {
                    if (item != null)
                        detalle.Add(new Drecibo(item));
                }

            }

            Rutaxfactura rfac = new Rutaxfactura(objetorfac);

            try
            {


                obj = FAC.account_factura(obj);
                return obj.com_codigo.ToString();
            }
            catch (Exception ex)
            {
                return "-1";
            }


         




        }
    }
}