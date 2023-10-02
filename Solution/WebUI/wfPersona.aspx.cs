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
using HtmlObjects;
using Functions;
using Packages;

namespace WebUI
{
    public partial class wfPersona : System.Web.UI.Page
    {
        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "per_nombres";
        protected static string WhereClause = "";
        protected static WhereParams parametros = new WhereParams();
        protected static int? tclipro;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageIndex = 1;
                pageSize = 20;
                txttclipro.Text = (Request.QueryString["tclipro"] != null) ? Request.QueryString["tclipro"].ToString() : -1 + "";
                tclipro = Convert.ToInt32(txttclipro.Text);
            }
        }

        public static string ShowData(Persona obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.per_nombres, "-", obj.per_apellidos }, descripcion = new string[] { obj.per_ciruc.ToString(), }, logico = new LogicItem[] { new LogicItem("Activos", obj.per_estado) } }.ToString();
        }

        public static string ShowObject(Persona obj)
        {
            Persona agente = new Persona();
            if (obj.per_agente.HasValue)
                agente = PersonaBLL.GetByPK(new Persona { per_empresa = obj.per_empresa, per_empresa_key = obj.per_empresa, per_codigo = obj.per_agente.Value, per_codigo_key = obj.per_agente.Value });



            List<Tab> tabs = new List<Tab>();
            StringBuilder html = new StringBuilder();
            html.AppendLine(new Input { id = "txtCODIGO", valor = obj.per_codigo.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.per_codigo_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.per_id, clase = Css.large ,habilitado=false}.ToString());
            html.AppendLine(new Input { id = "txtCIRUC", etiqueta = "RUC / CI", placeholder = "RUC / CI", valor = obj.per_ciruc, clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbTIPOID", etiqueta = "Tipo Identificación", valor = obj.per_tipoid, clase = Css.large, diccionario = Dictionaries.GetTipoIdentificacion() }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRES", etiqueta = "Nombres", placeholder = "Nombres", valor = obj.per_nombres, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtAPELLIDOS", etiqueta = "Apellidos", placeholder = "Apellidos", valor = obj.per_apellidos, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtDIRECCION", etiqueta = "Dirección", placeholder = "Dirección", valor = obj.per_direccion, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtTELEFONO", etiqueta = "Teléfono", placeholder = "Teléfono", valor = obj.per_telefono, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtCELULAR", etiqueta = "Celular", placeholder = "Celular", valor = obj.per_celular, clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtMAIL", etiqueta = "Mail", placeholder = "Mail", valor = obj.per_mail, clase = Css.large }.ToString());
            html.AppendLine(new Textarea { id = "txtOBSERVACION", etiqueta = "Observación", placeholder = "Observación", valor = obj.per_observacion, clase = Css.large }.ToString());
            if (tclipro == -1)
                html.AppendLine(new Select { id = "cmbTIPO", etiqueta = "Tipo", placeholder = "Tipo", diccionario = Dictionaries.GetTipoPersonas(), multiselect = true, obligatorio = true }.ToString());
            else
                html.AppendLine(new Select { id = "cmbTIPO", etiqueta = "Tipo", placeholder = "Tipo", diccionario = Dictionaries.GetTipoPersonas(), multiselect = true, obligatorio = true, valor = tclipro, habilitado = false }.ToString());
            html.AppendLine(new Select { id = "cmbGENERO", etiqueta = "Genero", placeholder = "Genero", diccionario = Dictionaries.GetGenero() }.ToString());
            html.AppendLine(new Select { id = "cmbCPERSONA", etiqueta = "Cpersona", placeholder = "Cpersona", diccionario = Dictionaries.GetCpersona() }.ToString());
            html.AppendLine(new Select { id = "cmbTPERSONA", etiqueta = "Tpersona", placeholder = "Tpersona", diccionario = Dictionaries.GetTpersona() }.ToString());
            html.AppendLine(new Select { id = "cmbLISTAPRECIO", etiqueta = "Lista de Precios", placeholder = "Lista de Precios", diccionario = Dictionaries.GetListaprecio() }.ToString());
           html.AppendLine(new Select { id = "cmbPOLITICA", etiqueta = "Politica", placeholder = "Politica", diccionario = Dictionaries.GetPolitica() }.ToString());
            html.AppendLine(new Select { id = "cmbRETIVA", etiqueta = "RET IVA", placeholder = "RET IVA", diccionario = Dictionaries.GetImpuesto() }.ToString());
            html.AppendLine(new Select { id = "cmbRETFUENTE", etiqueta = "RET FUENTE", placeholder = "RET FUENTE", diccionario = Dictionaries.GetImpuesto() }.ToString());

            //Datos de agente
            html.AppendLine(new Input { id = "txtAGENTE", valor = agente.per_codigo.ToString(), visible= false }.ToString());
            html.AppendLine(new Input { id = "txtAGENTENOMBRE", etiqueta = "Agente", autocomplete = "GetPersonaObj", placeholder = "Agente", valor = agente.per_razon, clase = Css.large }.ToString());



            html.AppendLine(new Input { id = "txtBLOQUEO", etiqueta = "Bloqueo", placeholder = "Bloqueo", valor = obj.per_bloqueo.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtTARJETA", etiqueta = "Tarjeta", placeholder = "Tarjeta", valor = obj.per_tarjeta.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtCUPO", etiqueta = "Cupo", placeholder = "Cupo", valor = obj.per_cupo.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtILIMITADO", etiqueta = "Ilimitado", placeholder = "Ilimitado", valor = obj.per_ilimitado.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtIMPUESTO", etiqueta = "Impuesto", placeholder = "Impuesto", valor = obj.per_impuesto.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Boton { click = "Configurar();return false;", valor = "Autorizaciones" }.ToString());

            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.per_estado }.ToString());
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            StringBuilder html2 = new StringBuilder();
            html2.AppendLine(new Select { id = "cmbPAIS", etiqueta = "Pais", valor = obj.per_pais.ToString(), clase = Css.medium, diccionario = Dictionaries.GetPaises() }.ToString());
            html2.AppendLine(new Select { id = "cmbPROVINCIA", etiqueta = "Provincia", valor = obj.per_provincia.ToString(), clase = Css.medium, diccionario = Dictionaries.Empty() }.ToString());
            html2.AppendLine(new Select { id = "cmbCANTON", etiqueta = "Canton", valor = obj.per_canton.ToString(), clase = Css.medium, diccionario = Dictionaries.Empty() }.ToString());
            html2.AppendLine(new Select { id = "cmbPARROQUIA", etiqueta = "Parroquia", valor = obj.per_parroquia.ToString(), clase = Css.medium, diccionario = Dictionaries.Empty() }.ToString());
            if (tclipro == Constantes.cCliente || tclipro == -1)
            {
                html2.AppendLine(new Select { id = "txtCATCLIENTE", etiqueta = "Categoria Cliente", valor = obj.per_contribuyente, clase = Css.medium, withempty = true, diccionario = Dictionaries.GetCatpersona(Constantes.cCliente) }.ToString());
                html2.AppendLine(new Select { id = "txtPOLCLIENTE", etiqueta = "Politica Cliente", valor = obj.per_contribuyente, clase = Css.medium, withempty = true, diccionario = Dictionaries.GetPolitica(Constantes.cCliente) }.ToString());
            } if (tclipro == Constantes.cProveedor || tclipro == -1)
            {
                html2.AppendLine(new Select { id = "txtCATPROVEEDOR", etiqueta = "Categoria Proveedor", valor = obj.per_contribuyente, clase = Css.medium, withempty = true, diccionario = Dictionaries.GetCatpersona(Constantes.cProveedor) }.ToString());
                html2.AppendLine(new Select { id = "txtPOLPROVEEDOR", etiqueta = "Politica Proveedor", valor = obj.per_contribuyente, clase = Css.medium, withempty = true, diccionario = Dictionaries.GetPolitica(Constantes.cProveedor) }.ToString());
            }
            html2.AppendLine(new Select { id = "txtCONTRIBUYENTE", etiqueta = "Contribuyente", valor = obj.per_contribuyente, clase = Css.medium, diccionario = Dictionaries.GetContribuyente() }.ToString());
            html2.AppendLine(new Check { id = "chkCONTRIBUYENTEESPECIAL", etiqueta = "Contribuyente Especial ", valor = obj.per_contribuyente_especial }.ToString());
            html2.AppendLine(new Input { id = "txtCONTACTO", etiqueta = "Contacto", placeholder = "Contacto", valor = obj.per_contacto, clase = Css.large }.ToString());
            html2.AppendLine(new Input { id = "txt_CONTACTODIRECCION", etiqueta = "Dirección  Contacto", placeholder = "Dirección  Contacto", valor = obj.per_contacto_direccion, clase = Css.large }.ToString());
            html2.AppendLine(new Input { id = "txtCONTACTOTELEFONO", etiqueta = "Teléfono Contacto", placeholder = "Teléfono Contacto", valor = obj.per_contacto_telefono, clase = Css.large }.ToString());
            html2.AppendLine(new Input { id = "txtRAZON", etiqueta = "Razón", placeholder = "Razón", valor = obj.per_razon, clase = Css.large }.ToString());
            html2.AppendLine(new Input { id = "txtREPRESENTANTELEGAL", etiqueta = "Representante Legal", placeholder = "Representante Legal", valor = obj.per_representantelegal, clase = Css.large }.ToString());
            html2.AppendLine(new Input { id = "txtPAGINAWEB", etiqueta = "Pagina Web", placeholder = "Pagina Web", valor = obj.per_paginaweb, clase = Css.large }.ToString());
            html2.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            if (obj.chofer == null)
            { obj.chofer = new Chofer(); }
            StringBuilder html4 = new StringBuilder();
            html4.AppendLine(new Input { id = "txtPUNTOSLICENCIA", etiqueta = "Puntos Licencia", placeholder = "Puntos Licencia", valor = obj.chofer.cho_puntoslicencia.ToString(), clase = Css.large }.ToString());
            html4.AppendLine(new Input { id = "txtTIPOLICENCIA", etiqueta = "Tipo Licencia", placeholder = "Tipo Licencia", valor = obj.chofer.cho_tipolicencia, clase = Css.large, obligatorio = true }.ToString());
            html4.AppendLine(new Input { id = "txtTIPOSANGRE", etiqueta = "Tipo Sangre", placeholder = "Tipo Sangre", valor = obj.chofer.cho_tiposangre, clase = Css.large, obligatorio = true }.ToString());
            html4.AppendLine(new Input { id = "txtNROLICENCIA", etiqueta = "Numero Licencia", placeholder = "Numero Licencia", valor = obj.chofer.cho_nrolicencia, clase = Css.large, obligatorio = true }.ToString());
            if (tclipro == Constantes.cChofer || tclipro == -1)
            {
                html4.AppendLine(new Select { id = "txtCATCHOFER", etiqueta = "Categoria", valor = obj.per_contribuyente, clase = Css.medium, withempty = true, diccionario = Dictionaries.GetCatpersona(Constantes.cChofer) }.ToString());
                html4.AppendLine(new Select { id = "txtPOLCHOFER", etiqueta = "Politica", valor = obj.per_contribuyente, clase = Css.medium, withempty = true, diccionario = Dictionaries.GetPolitica(Constantes.cChofer) }.ToString());
            }
            html4.AppendLine(new Input { id = "cmbEMISIONLICENCIA", placeholder = "Fecha Emision Licencia", etiqueta = "Fecha Emision Licencia", datepicker = true, datetimevalor = (obj.chofer.cho_emisionlicencia.HasValue) ? obj.chofer.cho_emisionlicencia.Value : DateTime.Now, clase = Css.large, obligatorio = true }.ToString());
            html4.AppendLine(new Input { id = "cmbRENOVACION", placeholder = "Fecha Renovacion Licencia", etiqueta = "Fecha Renovacion Licencia", datepicker = true, datetimevalor = (obj.chofer.cho_renovacion.HasValue) ? obj.chofer.cho_renovacion.Value : DateTime.Now, clase = Css.large, obligatorio = true }.ToString());
            html4.AppendLine(new Input { id = "cmbCADUCIDADLICENCIA", placeholder = "Fecha Caducidad Licencia", etiqueta = "Fecha Caducidad Licencia", datepicker = true, datetimevalor = (obj.chofer.cho_caducidadlicencia.HasValue) ? obj.chofer.cho_caducidadlicencia.Value : DateTime.Now, clase = Css.large, obligatorio = true }.ToString());
            html4.AppendLine(new Select { id = "cmbPAISEMISION", etiqueta = "Pais Emision Licencia", valor = obj.chofer.cho_paisemision.ToString(), clase = Css.large, diccionario = Dictionaries.GetPaises() }.ToString());
            html4.AppendLine(new Select { id = "cmbPROVINCIAEMISION", etiqueta = "Provincia Emision Licencia", valor = obj.chofer.cho_provinciaemision.ToString(), clase = Css.large, diccionario = Dictionaries.Empty() }.ToString());
            html4.AppendLine(new Select { id = "cmbCANTONEMISION", etiqueta = "Canton Emision Licencia", valor = obj.chofer.cho_cantonemision.ToString(), clase = Css.large, diccionario = Dictionaries.Empty() }.ToString());
            html4.AppendLine(new Select { id = "cmbPAISRENOVACION", etiqueta = "Pais Renovacion Licencia", valor = obj.chofer.cho_paisrenovacion.ToString(), clase = Css.large, diccionario = Dictionaries.GetPaises() }.ToString());
            html4.AppendLine(new Select { id = "cmbPROVINCIARENOVACION", etiqueta = "Provincia Renovacion Licencia", valor = obj.chofer.cho_provinciarenovacion.ToString(), clase = Css.large, diccionario = Dictionaries.Empty() }.ToString());
            html4.AppendLine(new Select { id = "cmbCANTONRENOVACION", etiqueta = "Canton Renovacion Licencia", valor = obj.chofer.cho_cantonrenovacion.ToString(), clase = Css.large, diccionario = Dictionaries.Empty() }.ToString());
            html4.AppendLine(new Textarea { id = "cmbOBSERVACIONLICENCIA", etiqueta = "Observacion Licencia", placeholder = "Observacion Licencia", valor = obj.chofer.cho_observacionlicencia, cols = 5, rows = 5, clase = Css.large }.ToString());
            html4.AppendLine(new Auditoria { creausr = obj.chofer.crea_usr, creafecha = obj.chofer.crea_fecha, modusr = obj.chofer.mod_usr, modfecha = obj.chofer.mod_fecha }.ToString());
            if (obj.socio == null)
            { obj.socio = new Socioempleado(); }
            StringBuilder html3 = new StringBuilder();
            html3.AppendLine(new Input { id = "txtFOTO", etiqueta = "Foto", placeholder = "Foto", valor = obj.socio.soc_foto, clase = Css.large }.ToString());
            html3.AppendLine(new Input { id = "cmbFECHANACIMIENTO", placeholder = "Fecha Nacimiento", etiqueta = "Fecha Nacimiento", datepicker = true, datetimevalor = (obj.socio.soc_fechanacimiento.HasValue) ? obj.socio.soc_fechanacimiento.Value : DateTime.Now, clase = Css.large, obligatorio = true }.ToString());
            html3.AppendLine(new Select { id = "cmbPAISNACIMIENTO", etiqueta = "Pais Nacimiento", valor = obj.socio.soc_paisnacimiento.ToString(), clase = Css.large, diccionario = Dictionaries.GetPaises() }.ToString());
            html3.AppendLine(new Select { id = "cmbPROVINCIANACIMIENTO", etiqueta = "Provincia Nacimiento", valor = obj.socio.soc_provincianacimiento.ToString(), clase = Css.large, diccionario = Dictionaries.Empty() }.ToString());
            html3.AppendLine(new Select { id = "cmbCANTONNACIMIENTO", etiqueta = "Canton Nacimiento", valor = obj.socio.soc_cantonnacimiento.ToString(), clase = Css.large, diccionario = Dictionaries.Empty() }.ToString());
            if (tclipro == Constantes.cSocio || tclipro == -1)
            {
                html3.AppendLine(new Select { id = "txtCATSOCIO", etiqueta = "Categoria", valor = obj.per_contribuyente, clase = Css.medium, withempty = true, diccionario = Dictionaries.GetCatpersona(Constantes.cSocio) }.ToString());
                html3.AppendLine(new Select { id = "txtPOLSOCIO", etiqueta = "Politica", valor = obj.per_contribuyente, clase = Css.medium, withempty = true, diccionario = Dictionaries.GetPolitica(Constantes.cSocio) }.ToString());
            }
            html3.AppendLine(new Select { id = "cmbESTADOCIVIL", etiqueta = "Estado Civil", valor = obj.socio.soc_estadocivil.ToString(), clase = Css.large, diccionario = Dictionaries.GetEstadoCivil() }.ToString());
            html3.AppendLine(new Select { id = "cmbCODIGOCONYUGE", etiqueta = "Conyuge", valor = obj.socio.soc_codigoconyuge.ToString(), clase = Css.large, diccionario = Dictionaries.GetPersonas() }.ToString());         
            html3.AppendLine(new Input { id = "txtCARGAS", etiqueta = "Cargas", placeholder = "Cargas", valor = obj.socio.soc_cargas.ToString(), clase = Css.large }.ToString());
            html3.AppendLine(new Input { id = "txtINSCRIPCION", etiqueta = "Numero Inscripcion", placeholder = "Numero Inscripcion", valor = obj.socio.soc_inscripcion.ToString(), clase = Css.large }.ToString());
            html3.AppendLine(new Input { id = "cmbFECHAINSCRIPCION", etiqueta = "Fecha Inscripcion", datepicker = true, datetimevalor = (obj.socio.soc_fechainscripcion.HasValue) ? obj.socio.soc_fechainscripcion.Value : DateTime.Now, clase = Css.large }.ToString());
            html3.AppendLine(new Input { id = "cmbFECHASALIDA", etiqueta = "Fecha Salida", datepicker = true, datetimevalor = (obj.socio.soc_fechasalida.HasValue) ? obj.socio.soc_fechasalida.Value : DateTime.Now, clase = Css.large }.ToString());
            html3.AppendLine(new Textarea { id = "txtRAZONSALIDA", etiqueta = "Razon Salida", placeholder = "Razon Salida", valor = obj.socio.soc_razonsalida, cols = 5, rows = 5, clase = Css.large }.ToString());
            html3.AppendLine(new Input { id = "txtLUGARTRABAJO", etiqueta = "Lugar de Trabajo", placeholder = "Lugar de Trabajo", valor = obj.socio.soc_lugartrabajo, clase = Css.large }.ToString());
            html3.AppendLine(new Select { id = "cmbCARGOTRABAJO", etiqueta = "Cargo Trabajo", valor = obj.socio.soc_cargotrabajo.ToString(), clase = Css.large, diccionario = Dictionaries.GetCargos() }.ToString());
            html3.AppendLine(new Select { id = "cmbDEPARTAMENTO", etiqueta = "Departamento Trabajo", valor = obj.socio.soc_departamento.ToString(), clase = Css.large, diccionario = Dictionaries.GetDepartamentos() }.ToString());
            html3.AppendLine(new Input { id = "txtDIRECCIONTRABAJO", etiqueta = "Direccion Trabajo", placeholder = "Direccion Trabajo", valor = obj.socio.soc_direcciontrabajo, clase = Css.large }.ToString());
            html3.AppendLine(new Input { id = "txtTELEFONOTRABAJO", etiqueta = "Telefono Trabajo", placeholder = "Telefono Trabajo", valor = obj.socio.soc_telefonotrabajo, clase = Css.large }.ToString());
            html3.AppendLine(new Input { id = "txtFAXTRABAJO", etiqueta = "Fax Trabajo", placeholder = "Fax Trabajo", valor = obj.socio.soc_faxtrabajo, clase = Css.large }.ToString());
            html3.AppendLine(new Input { id = "txtNROIESS", etiqueta = "Numero IESS", placeholder = "Numero IESS", valor = obj.socio.soc_nroiess, clase = Css.large }.ToString());
            html3.AppendLine(new Select { id = "cmbBANCO", etiqueta = "Emisor", valor = obj.socio.soc_banco.ToString(), clase = Css.large, diccionario = Dictionaries.GetEmisores() }.ToString());
            html3.AppendLine(new Select { id = "cmbTIPOCUENTA", etiqueta = "Tipo Cuenta", valor = obj.socio.soc_tipocuenta, clase = Css.large, diccionario = Dictionaries.GetTipoCuenta() }.ToString());
            html3.AppendLine(new Input { id = "txtNROCUENTA", etiqueta = "Numero Cuenta", placeholder = "Numero Cuenta", valor = obj.socio.soc_nrocuenta, clase = Css.large }.ToString());
            html3.AppendLine(new Select { id = "cmbNIVELINSTRUCCION", etiqueta = "Nivel Instruccion", valor = obj.socio.soc_nivelinstruccion.ToString(), clase = Css.large, diccionario = Dictionaries.GetInstruccion() }.ToString());
            html3.AppendLine(new Input { id = "txtPOSTGRADO", etiqueta = "Postrgrado", placeholder = "Postrgrado", valor = obj.socio.soc_postgrado, clase = Css.large }.ToString());
            html3.AppendLine(new Input { id = "txtDOCTORADO", etiqueta = "Doctorado", placeholder = "Doctorado", valor = obj.socio.soc_doctorado, clase = Css.large }.ToString());
            html3.AppendLine(new Input { id = "txtRECONOCIMIENTOS", etiqueta = "Reconocimientos", placeholder = "Reconocimientos", valor = obj.socio.soc_reconocimientos, clase = Css.large }.ToString());
            html3.AppendLine(new Input { id = "txtTITULOS", etiqueta = "Titulo", placeholder = "Titulo", valor = obj.socio.soc_titulos, clase = Css.large }.ToString());
            html3.AppendLine(new Select { id = "cmbPROFESION", etiqueta = "Nivel Profesion", valor = obj.socio.soc_profesion.ToString(), clase = Css.large, diccionario = Dictionaries.GetProfesiones() }.ToString());
            html3.AppendLine(new Input { id = "cmbFECHAGRADO", etiqueta = "Fecha Graduacion", datepicker = true, datetimevalor = (obj.socio.soc_fechagrado.HasValue) ? obj.socio.soc_fechagrado.Value : DateTime.Now, clase = Css.large }.ToString());
            html3.AppendLine(new Input { id = "txtINSTITUCION", etiqueta = "Intitucion Graduacion", placeholder = "Intitucion Graduacion", valor = obj.socio.soc_institucion, clase = Css.large }.ToString());
            html3.AppendLine(new Input { id = "txtCONADIS", etiqueta = "Numero CONADIS", placeholder = "Numero CONADIS", valor = obj.socio.soc_conadis, clase = Css.large }.ToString());
            html3.AppendLine(new Auditoria { creausr = obj.socio.crea_usr, creafecha = obj.socio.crea_fecha, modusr = obj.socio.mod_usr, modfecha = obj.socio.mod_fecha }.ToString());
            Tab tab1 = new Tab();
            tab1.id = "tab1";
            tab1.titulo = "Datos Personales";
            tab1.html = html.ToString();
            tabs.Add(tab1);
            if (tclipro == Constantes.cCliente || tclipro == Constantes.cProveedor || tclipro == -1)
            {
                Tab tab2 = new Tab();
                tab2.id = "tab2";
                if (tclipro == Constantes.cCliente)
                    tab2.titulo = "Cliente";
                else if (tclipro == Constantes.cProveedor)
                    tab2.titulo = "Proveedor";
                else
                    tab2.titulo = "Cliente/Proveedor";
                tab2.html = html2.ToString();
                tabs.Add(tab2);
            }
            if (tclipro == Constantes.cSocio || tclipro == -1)
            {
                Tab tab3 = new Tab();
                tab3.id = "tab3";
                if (tclipro == Constantes.cSocio)
                    tab3.titulo = "Socio";
                else if (tclipro == -1)
                    tab3.titulo = "Socio/Empleado";
                else
                    tab3.titulo = "Empleado";
                tab3.html = html3.ToString();
                tabs.Add(tab3);
            }
            if (tclipro == Constantes.cChofer || tclipro == -1)
            {
                Tab tab4 = new Tab();
                tab4.id = "tab4";
                tab4.titulo = "Chofer";
                tab4.html = html4.ToString();
                tabs.Add(tab4);
            }
            return new Tabs { id = "tabpersona", tabs = tabs }.ToString();
        }


        public static void SetWhereClause(Persona obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (!string.IsNullOrEmpty(obj.per_ciruc))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " per_ciruc like {" + contador + "} ";
                valores.Add("%" + obj.per_ciruc + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.per_nombres))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + "( per_nombres like {" + contador + "} ";
                valores.Add("%" + obj.per_nombres + "%");
                contador++;
                parametros.where += ((parametros.where != "") ? " or " : "") + " per_apellidos like {" + contador + "}) ";
                valores.Add("%" + obj.per_nombres + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.per_razon))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " per_razon like {" + contador + "} ";
                valores.Add("%" + obj.per_razon + "%");
                contador++;
            }
            if (obj.per_estado.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " per_estado = {" + contador + "} ";
                valores.Add(obj.per_estado.Value);
                contador++;
            }
            if (obj.per_empresa > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " per_empresa = {" + contador + "} ";
                valores.Add(obj.per_empresa);
                contador++;
            }
            if (tclipro > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " pxt_tipo = {" + contador + "} ";
                valores.Add(tclipro);
                contador++;
            }
            parametros.valores = valores.ToArray();
        }



        [WebMethod]
        public static string GetData(object objeto)
        {
            SetWhereClause(GetObjeto(objeto));
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            StringBuilder html = new StringBuilder();
            List<Persona> lst = vPersonaBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            foreach (Persona obj in lst)
            {
                string id = "{\"per_codigo\":\"" + obj.per_codigo + "\", \"per_empresa\":\"" + obj.per_empresa + "\"}";//ID COMPUESTO             
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
            return ShowObject(new Persona());
        }


        [WebMethod]
        public static string GetObject(object id)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object codigo = null;
            object empresa = null;
            tmp.TryGetValue("per_codigo", out codigo);
            tmp.TryGetValue("per_empresa", out empresa);
            Persona obj = new Persona();
            if (empresa != null && !empresa.Equals(""))
            {
                obj.per_empresa = int.Parse(empresa.ToString());
                obj.per_empresa_key = int.Parse(empresa.ToString());
            }
            if (codigo != null && !codigo.Equals(""))
            {
                obj.per_codigo = int.Parse(codigo.ToString());
                obj.per_codigo_key = int.Parse(codigo.ToString());
            }
            obj = PersonaBLL.GetByPK(obj);

            if (obj.per_agente.HasValue)
            {
                Persona agente = PersonaBLL.GetByPK(new Persona { per_empresa = obj.per_empresa, per_empresa_key = obj.per_empresa, per_codigo = obj.per_agente.Value, per_codigo_key = obj.per_agente.Value });
                obj.per_agentenombre = agente.per_razon;
            }

            if (tclipro == -1)
                obj.tipos = PersonaxtipoBLL.GetAll("pxt_persona=" + obj.per_codigo, "");
            else
            {
                Personaxtipo aux = new Personaxtipo();
                aux.pxt_empresa_key = obj.per_empresa;
                aux.pxt_persona_key = obj.per_codigo;
                aux.pxt_tipo_key = tclipro.Value;
                aux = PersonaxtipoBLL.GetByPK(aux);
                obj.tipos = new List<Personaxtipo>();
                obj.tipos.Add(aux);
            }
            obj.chofer = ChoferBLL.GetByPK(new Chofer { cho_empresa_key = obj.per_empresa, cho_persona_key = obj.per_codigo });
            obj.socio = SocioempleadoBLL.GetByPK(new Socioempleado { soc_empresa_key = obj.per_empresa, soc_persona_key = obj.per_codigo });
            return new JavaScriptSerializer().Serialize(obj);
            //return ShowObject(obj);
        }

        public static Persona GetObjeto(object objeto)
        {
            Persona obj = new Persona();
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object codigo = null;
                object codigokey = null;
                object empresa = null;
                object empresakey = null;
                object ciruc = null;
                object id = null;
                object tipoid = null;
                object tipo = null;
                object pxt = null;
                object nombres = null;
                object apellidos = null;
                object direccion = null;
                object telefono = null;
                object celular = null;
                object mail = null;
                object observacion = null;
                object pais = null;
                object provincia = null;
                object canton = null;
                object parroquia = null;
                object contribuyente = null;
                object contribuyente_especial = null;
                object contacto = null;
                object contacto_direccion = null;
                object contacto_telefono = null;
                object razon = null;
                object representantelegal = null;
                object paginaweb = null;
                object estado = null;
                object per_genero = null;
                object per_cpersona = null;
                object per_tpersona = null;
                object per_listaprecio = null;
                object per_politica = null;
                object per_retiva = null;
                object per_retfuente = null;
                object per_agente = null;
                object per_bloqueo = null;
                object per_tarjeta = null;
                object per_cupo = null;
                object per_ilimitado = null;
                object per_impuesto = null;
                object socio = null;
                object chofer = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                tmp.TryGetValue("per_codigo", out codigo);
                tmp.TryGetValue("per_codigo_key", out codigokey);
                tmp.TryGetValue("per_empresa", out empresa);
                tmp.TryGetValue("per_empresa_key", out empresakey);
                tmp.TryGetValue("per_ciruc", out ciruc);
                tmp.TryGetValue("per_id", out id);
                tmp.TryGetValue("per_tipoid", out tipoid);
                tmp.TryGetValue("per_nombres", out nombres);
                tmp.TryGetValue("per_apellidos", out apellidos);
                tmp.TryGetValue("per_direccion", out direccion);
                tmp.TryGetValue("per_telefono", out telefono);
                tmp.TryGetValue("per_celular", out celular);
                tmp.TryGetValue("per_mail", out mail);
                tmp.TryGetValue("per_observacion", out observacion);
                tmp.TryGetValue("per_pais", out pais);
                tmp.TryGetValue("per_provincia", out provincia);
                tmp.TryGetValue("per_canton", out canton);
                tmp.TryGetValue("per_parroquia", out parroquia);
                tmp.TryGetValue("per_contribuyente", out contribuyente);
                tmp.TryGetValue("per_contribuyente_especial", out contribuyente_especial);
                tmp.TryGetValue("per_contacto", out contacto);
                tmp.TryGetValue("per_contacto_direccion", out contacto_direccion);
                tmp.TryGetValue("per_contacto_telefono", out contacto_telefono);
                tmp.TryGetValue("per_razon", out razon);
                tmp.TryGetValue("per_representantelegal", out representantelegal);
                tmp.TryGetValue("per_paginaweb", out paginaweb);
                tmp.TryGetValue("per_genero", out per_genero);
                tmp.TryGetValue("per_cpersona", out per_cpersona);
                tmp.TryGetValue("per_tpersona", out per_tpersona);
                tmp.TryGetValue("per_listaprecio", out per_listaprecio);
                tmp.TryGetValue("per_politica", out per_politica);
                tmp.TryGetValue("per_retiva", out per_retiva);
                tmp.TryGetValue("per_retfuente", out per_retfuente);
                tmp.TryGetValue("per_agente", out per_agente);
                tmp.TryGetValue("per_bloqueo", out per_bloqueo);
                tmp.TryGetValue("per_tarjeta", out per_tarjeta);
                tmp.TryGetValue("per_cupo", out per_cupo);
                tmp.TryGetValue("per_ilimitado", out per_ilimitado);
                tmp.TryGetValue("per_impuesto", out per_impuesto);
                tmp.TryGetValue("tipos", out tipo);
                tmp.TryGetValue("pxt", out pxt);
                tmp.TryGetValue("per_estado", out estado);
                tmp.TryGetValue("socio", out socio);
                tmp.TryGetValue("chofer", out chofer);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                if (codigo != null && !codigo.Equals(""))
                {
                    obj.per_codigo = int.Parse(codigo.ToString());
                }
                if (codigokey != null && !codigokey.Equals(""))
                {
                    obj.per_codigo_key = int.Parse(codigokey.ToString());
                }
                if (empresa != null && !empresa.Equals(""))
                {
                    obj.per_empresa = int.Parse(empresa.ToString());
                }
                if (empresakey != null && !empresakey.Equals(""))
                {
                    obj.per_empresa_key = int.Parse(empresakey.ToString());
                }
                obj.per_ciruc = (string)ciruc;
                obj.per_id = (string)id;
                obj.per_tipoid = (string)tipoid;
                //obj.per_tipo = (string)tipo;
                if (tipo != null)
                {
                    List<Personaxtipo> lista = new List<Personaxtipo>();
                    foreach (object item in (object[])tipo)
                    {
                        Personaxtipo t = new Personaxtipo();
                        t.pxt_persona = obj.per_codigo;
                        t.pxt_persona_key = obj.per_codigo_key;
                        t.pxt_tipo = int.Parse(item.ToString());
                        t.pxt_tipo_key = int.Parse(item.ToString());
                        Dictionary<string, object> tmp_aux = (Dictionary<string, object>)pxt;
                        if (t.pxt_tipo == Constantes.cCliente)
                        {
                            object cliente = null;
                            tmp_aux.TryGetValue("cliente", out cliente);
                            object[] clientearry = ((System.Collections.IEnumerable)cliente).Cast<object>().ToArray();
                            t.pxt_cat_persona = (Int32)Conversiones.GetValueByType(clientearry[0].ToString(), typeof(Int32));
                            t.pxt_politicas = (Int32)Conversiones.GetValueByType(clientearry[1].ToString(), typeof(Int32));

                        }
                        if (t.pxt_tipo == Constantes.cProveedor)
                        {
                            object proveedor = null;
                            tmp_aux.TryGetValue("proveedor", out proveedor);
                            object[] proveedorarry = ((System.Collections.IEnumerable)proveedor).Cast<object>().ToArray();
                            t.pxt_cat_persona = (Int32)Conversiones.GetValueByType(proveedorarry[0].ToString(), typeof(Int32));
                            t.pxt_politicas = (Int32)Conversiones.GetValueByType(proveedorarry[1].ToString(), typeof(Int32));
                        }
                        if (t.pxt_tipo == Constantes.cSocio)
                        {
                            object socio_pxt = null;
                            tmp_aux.TryGetValue("socio", out socio_pxt);
                            object[] socioarray = ((System.Collections.IEnumerable)socio_pxt).Cast<object>().ToArray();
                            t.pxt_cat_persona = (Int32)Conversiones.GetValueByType(socioarray[0].ToString(), typeof(Int32));
                            t.pxt_politicas = (Int32)Conversiones.GetValueByType(socioarray[1].ToString(), typeof(Int32));
                        }
                        if (t.pxt_tipo == Constantes.cChofer)
                        {
                            object chofer_pxt = null;
                            tmp_aux.TryGetValue("chofer", out chofer_pxt);
                            object[] choferarray = ((System.Collections.IEnumerable)chofer_pxt).Cast<object>().ToArray();
                            t.pxt_cat_persona = (Int32)Conversiones.GetValueByType(choferarray[0].ToString(), typeof(Int32));
                            t.pxt_politicas = (Int32)Conversiones.GetValueByType(choferarray[1].ToString(), typeof(Int32));
                        }
                        if (t.pxt_cat_persona == 0)
                            t.pxt_cat_persona = null;
                        if (t.pxt_politicas == 0)
                            t.pxt_politicas = null;
                        t.pxt_empresa = obj.per_empresa;
                        t.pxt_empresa_key = obj.per_empresa;
                        t.pxt_estado =Constantes.cEstadoGrabado;                     
                        t.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                        t.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                        t.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                        t.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));


                        lista.Add(t);
                    }
                    obj.tipos = lista;
                }
                obj.per_razon = (string)razon;
                obj.per_telefono = (string)telefono;
                obj.per_mail = (string)mail;
                obj.per_nombres = (string)nombres;
                obj.per_apellidos = (string)apellidos;
                obj.per_direccion = (string)direccion;
                obj.per_celular = (string)celular;
                obj.per_observacion = (string)observacion;
                obj.per_pais = Convert.ToInt32(pais);
                obj.per_provincia = Convert.ToInt32(provincia);
                obj.per_canton = Convert.ToInt32(canton);
                obj.per_parroquia = Convert.ToInt32(parroquia);
                obj.per_contribuyente = (string)contribuyente;
                obj.per_contribuyente_especial = Convert.ToInt32(contribuyente_especial);
                obj.per_contacto = (string)contacto;
                obj.per_contacto_direccion = (string)contacto_direccion;
                obj.per_contacto_telefono = (string)contacto_telefono;
                obj.per_representantelegal = (string)representantelegal;
                obj.per_paginaweb = (string)paginaweb;
                obj.per_estado = (int?)estado;
                obj.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                obj.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                obj.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                obj.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                obj.per_genero = (string)per_genero;
                obj.per_cpersona = Convert.ToInt32(per_cpersona);
                obj.per_tpersona = Convert.ToInt32(per_tpersona);
                obj.per_listaprecio = Convert.ToInt32(per_listaprecio);
                obj.per_politica = Convert.ToInt32(per_politica);
                obj.per_retiva = Convert.ToInt32(per_retiva);
                obj.per_retfuente = Convert.ToInt32(per_retfuente);
                if (tclipro == Constantes.cSocio || tclipro == -1)
                {
                    obj.socio = new Socioempleado(socio);
                }
                if (tclipro == Constantes.cChofer || tclipro == -1)
                {
                    obj.chofer = new Chofer(chofer);
                }
                if (per_agente != null && !per_agente.Equals(""))
                    obj.per_agente = Convert.ToInt32(per_agente);
                if (per_bloqueo != null && !per_bloqueo.Equals(""))
                    obj.per_bloqueo = Convert.ToInt32(per_bloqueo);
                if (per_tarjeta != null && !per_tarjeta.Equals(""))
                    obj.per_tarjeta = Convert.ToInt32(per_tarjeta);
                if (per_cupo != null && !per_cupo.Equals(""))
                    obj.per_cupo = Convert.ToInt32(per_cupo);
                if (per_ilimitado != null && !per_ilimitado.Equals(""))
                    obj.per_ilimitado = Convert.ToInt32(per_ilimitado);
                if (per_impuesto != null && !per_impuesto.Equals(""))
                    obj.per_impuesto = Convert.ToInt32(per_impuesto);
            }
            return obj;
        }


        [WebMethod]
        public static string GetSearch()
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class='pull-left'>");
            html.AppendLine(new Input { id = "txtCIRUC_S", placeholder = "RUC / CI", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtRAZON_S", placeholder = "Razon", clase = Css.large }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE_S", placeholder = "Nombres", clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "cmbESTADO_S", clase = Css.medium, diccionario = Dictionaries.GetEstado() }.ToString());
            html.AppendLine("</div>");
            html.AppendLine("<div class='pull-right'>");
            html.AppendLine(new Boton { refresh = true }.ToString());
            html.AppendLine(new Boton { clean = true }.ToString());
            html.AppendLine("</div>");
            return html.ToString();
        }

        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Persona());
        }


        [WebMethod]
        public static string SaveObject(object objeto)
        {
            Persona obj = GetObjeto(objeto);
            if (Functions.Validaciones.valida_cedularuc(obj.per_ciruc))
            {
                //Obtener persona con la cedula igual
                if (string.IsNullOrEmpty(obj.per_razon))
                {
                    obj.per_razon = obj.per_nombres + " " + obj.per_apellidos;
                }
                BLL transaction = new BLL();
                transaction.CreateTransaction();
                try
                {
                    transaction.BeginTransaction();
                    if (obj.per_codigo_key > 0)//UPDATE
                    {
                        PersonaBLL.Update(transaction, obj);
                        if (tclipro == -1)
                        {
                            List<Personaxtipo> lst = PersonaxtipoBLL.GetAll("pxt_persona =" + obj.per_codigo, "");
                            foreach (Personaxtipo objaux in lst)
                            {
                                PersonaxtipoBLL.Delete(transaction, objaux);
                                if (objaux.pxt_tipo == Constantes.cChofer || objaux.pxt_tipo == Constantes.cAyudante)
                                    ChoferBLL.Delete(transaction, obj.chofer);
                                if (objaux.pxt_tipo == Constantes.cSocio)
                                    SocioempleadoBLL.Delete(transaction, obj.socio);
                            }
                        }
                        else
                        {
                            Personaxtipo aux = new Personaxtipo();
                            aux.pxt_empresa_key = obj.per_empresa;
                            aux.pxt_persona_key = obj.per_codigo;
                            aux.pxt_tipo_key = tclipro.Value;
                            aux = PersonaxtipoBLL.GetByPK(aux);
                            PersonaxtipoBLL.Delete(transaction, aux);
                            if (aux.pxt_tipo == Constantes.cChofer || aux.pxt_tipo == Constantes.cAyudante)
                                ChoferBLL.Delete(transaction, obj.chofer);
                            if (aux.pxt_tipo == Constantes.cSocio)
                                SocioempleadoBLL.Delete(transaction, obj.socio);
                        }
                        foreach (Personaxtipo item in obj.tipos)
                        {
                            item.pxt_persona = obj.per_codigo;
                            item.pxt_persona_key = obj.per_codigo;
                            PersonaxtipoBLL.Insert(transaction, item);
                            if (item.pxt_tipo == Constantes.cChofer || item.pxt_tipo == Constantes.cAyudante)
                                ChoferBLL.Insert(transaction, obj.chofer);
                            if (item.pxt_tipo == Constantes.cSocio)
                                SocioempleadoBLL.Insert(transaction, obj.socio);
                        }
                    }
                    else
                    {

                        Empresa emp = new Empresa();
                        emp.emp_codigo_key = obj.per_empresa;
                        emp = EmpresaBLL.GetByPK(emp);
                        Usuario usr = new Usuario();
                        usr.usr_id_key = obj.crea_usr;
                        usr = UsuarioBLL.GetByPK(usr);

                        obj.per_id = General.GetIdPersona(emp, usr);
                        int codigo = PersonaBLL.InsertIdentity(transaction, obj);
                        foreach (Personaxtipo item in obj.tipos)
                        {
                            item.pxt_persona = codigo;
                            item.pxt_persona_key = codigo;
                            if (item.pxt_tipo == Constantes.cChofer || item.pxt_tipo == Constantes.cAyudante)
                            {
                                obj.chofer.cho_persona = codigo;
                                obj.chofer.cho_persona_key = codigo;
                            }
                            if (item.pxt_tipo == Constantes.cSocio)
                            {
                                obj.socio.soc_persona = codigo;
                                obj.socio.soc_persona_key = codigo;
                            }

                            PersonaxtipoBLL.Insert(transaction, item);
                            if (item.pxt_tipo == Constantes.cChofer || item.pxt_tipo == Constantes.cAyudante)
                                ChoferBLL.Insert(transaction, obj.chofer);
                            if (item.pxt_tipo == Constantes.cSocio)
                                SocioempleadoBLL.Insert(transaction, obj.socio);
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return "ERROR";
                }
                if (obj.per_codigo_key > 0)
                    return ShowData(obj);
                else
                    return "OK";
            }
            else
                throw new ArgumentException("Cédula/RUC incorrecto");
        }

        [WebMethod]
        public static string DeleteObject(object objeto)
        {
            Persona obj = GetObjeto(objeto);
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                List<Personaxtipo> lst = PersonaxtipoBLL.GetAll("pxt_persona =" + obj.per_codigo, "");
                foreach (Personaxtipo objaux in lst)
                {
                    PersonaxtipoBLL.Delete(transaction, objaux);
                }
                List<Socioempleado> lstso = SocioempleadoBLL.GetAll("soc_persona =" + obj.per_codigo, "");
                foreach (Socioempleado objaux in lstso)
                {
                    SocioempleadoBLL.Delete(transaction, objaux);
                }
                List<Chofer> lstcho = ChoferBLL.GetAll("cho_persona =" + obj.per_codigo, "");
                foreach (Chofer objaux in lstcho)
                {
                    ChoferBLL.Delete(transaction, objaux);
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return "ERROR";
            }
            if (PersonaBLL.Delete(obj) > 0)
            {
                return "OK";
            }
            else
                return "ERROR";
        }
    }
}