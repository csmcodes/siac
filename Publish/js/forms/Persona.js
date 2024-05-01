//Archivo:          Common.js
//Descripción:      Contiene las funciones comunes para las interfaces
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Julio 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados


var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var formname = "wfPersona.aspx";
var menuoption = "Persona";



//Codigo ejecutado cuando el document esta listo
$(document).ready(function () {

    LoadList();     //Carga el listado 
    LoadSearch();   //Carga los parametros de busqueda
    LoadForm();     //Carga el formulario
    $("#listadocontent").on("scroll", scroll);
    $("#add").on("click", AddObj);          //Boton "Nuevo" de la barra
    $("#newedit").on("click", AddObj);      //Opción "Nuevo" del combo de opciones de la sección de edición
    $("#saveedit").on("click", SaveObj);    //Opción "Guardar" del combo de opciones de la sección de edición
    $("#deledit").on("click", AskDelete);   //Opción "Eliminar" del combo de opciones de la sección de edición
    $("#search").on("click", ShowBusqueda); //Boton "Buscar" de la barra
    $("#edit").on("click", ShowEdit);       //Boton "Editar" de la barra
    $("#list").on("click", ShowList);       //Boton "Listado" de la barra
    $("#refresh").on("click", ReloadData);  //Boton "Refrescar" de la sección de parametros
    $("#reflist").on("click", ReloadData);  //Boton "Refrescar" de la sección de parametros
    $("#closeedit").on("click", HideEdit);  //Opción "Cerrar" del combo de opciones de la sección de edición
    $("#closelist").on("click", HideList);  //Opción "Cerrar" del combo de opciones de la sección de listado
    $("#edit").css({ 'display': 'none' });
    $("#list").css({ 'display': 'none' });
    $("#busqueda").css({ 'display': 'none' });

    //Codigo ejecutado cuando se preciona el botón "X" de la sección de edición
    $("#datos").find('.widgettitle .close').click(function () {
        $(this).parents('.widgetbox').fadeOut(function () {
            $(this).hide('fast', HideEdit());
        });
    });

    //Codigo ejecutado cuando se preciona el botón "X" de la sección de listado
    $("#listado").find('.widgettitle .close').click(function () {
        $(this).parents('.widgetbox').fadeOut(function () {
            $(this).hide('fast', HideList());
        });
    });

    //SetMenuOption();
    $('body').css('background', 'transparent');



});


function OpenMenuOption(obj) {
    if ($(obj).length > 0) {
        var padre = $(obj)[0].parentNode.parentNode;
        if ($(padre).hasClass("dropdown")) {
            $(padre).children("ul").css("display", "block");
            OpenMenuOption(padre);
        }
        else
            $(obj).addClass("active");
    }
}


function SetMenuOption() {

    $(".active").removeClass('active');
    OpenMenuOption($("#" + menuoption));
    $("#" + menuoption).addClass('active');
}

function HasDynamicScroll() {
    if (formname == "wfArmaMenu.aspx" || formname == "wfCuenta.aspx" || formname == "wfCentro.aspx" || formname == "wfTproducto.aspx")
        return false;
    return true;
}


//Funcion que controla el scroll dinamico del listado
function scroll() {
    if (HasDynamicScroll()) {
        if ($("#listadocontent")[0].scrollHeight - $("#listadocontent").scrollTop() <= $("#listadocontent").outerHeight()) {
            LoadList();
        }
    }
}

//Funciona que invoca al servidor mediante JSON
function CallServer(strurl, strdata, retorno) {
    ClearValidate();
    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (retorno == 0)
                LoadListResult(data);
            if (retorno == 1)
                LoadSearchResult(data);
            if (retorno == 2)
                LoadFormResult(data);
            if (retorno == 3)
                ShowObjResult(data);
            if (retorno == 4)
                DeleteObjResult(data);
            if (retorno == 5)
                SaveObjResult(data);

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var errorData = $.parseJSON(XMLHttpRequest.responseText);
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert(errorData.Message, 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }

    })
}


function ReloadData() {
    $('#listadocontent').empty();
    var jsonText = GetJSONSearch(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
    CallServer(formname + "/ReloadData", jsonText, 0);
}

function LoadList() {
    if ($("#listadocontent").length > 0) {
        $('#cargando').html('<img src="images/loaders/loader10.gif">');
        var jsonText = GetJSONSearch(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
        CallServer(formname + "/GetData", jsonText, 0);
    }
}

function LoadListResult(data) {
    if (data != "") {
        $('#listadocontent').append(data.d);
    }
    $('#cargando').empty();
}


function LoadSearch() {
    if ($("#busquedacontent").length > 0) {
        CallServer(formname + "/GetSearch", "{}", 1);
    }
}

function LoadSearchResult(data) {
    if (data != "") {
        $('#busquedacontent').html(data.d);
    }
}


function LoadForm() {
    if ($("#datoscontent").length > 0) {
        CallServer(formname + "/GetForm", "{}", 2);
    }
}

function LoadFormResult(data) {
    if (data != "") {
        $('#datoscontent').html(data.d);
    }
    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha
    // Select with Search
    $(".chzn-select").chosen();
    // tabbed widget
    $(".tabbedwidget").tabs();
    SetForm(); //Depende de cada js.


}




function ShowObj(obj) {
    var id = $(obj).data("id");
    var jsonText = JSON.stringify({ id: id });
    CallServer(formname + "/GetObject", jsonText, 3)

}

function ShowObjResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        SetJSONObject(obj); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM
    }
}

function StopPropagation(event) {
    if (!event) var event = window.event;
    event.cancelBubble = true;
    if (event.stopPropagation) event.stopPropagation();

}

function Select(obj) {
    var rows = $("#listadocontent li");
    $(rows).css("background-color", "");
    $(obj).css("background-color", color);
    selectobj = $(obj).children();
    CleanForm(); 
    ShowObj(selectobj);
    StopPropagation();
}

function AddObj() {
    ClearValidate();
    ShowEdit();
    var rows = $("#listadocontent li");
    $(rows).css("background-color", "");
    selectobj = null;
    CleanForm(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
}



/*************************DELETE FUNCTIONS *******************************/
function AskDelete() {

    jConfirm('¿Está seguro que desea eliminar el registro?', 'Eliminar', function (r) {
        if (r)
            DeleteObj();

    });
}

function DeleteObj() {
    var jsonText = GetJSONObject(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
    CallServer(formname + "/DeleteObject", jsonText, 4);

}


function DeleteObjResult(data) {
    if (data != "") {
        jQuery.jGrowl("Registro eliminado con éxito");
        CleanForm(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
        ReloadData();
    }
}


/***********************SAVE FUNCTIONS ***********************************/

function ClearValidate() {
    var controles = $("#datoscontent").find('[data-obligatorio="True"]');
    $("#datoscontent").find('[data-obligatorio="True"]').each(function () {
        $(this.parentNode).removeClass('obligatorio')
        $(this.parentNode).children(".obligatorio").remove();
    });
}

function ValidateForm() {
    var retorno = true;
    var controles = $("#datoscontent").find('[data-obligatorio="True"]');
    $("#datoscontent").find('[data-obligatorio="True"]').each(function () {
        $(this.parentNode).removeClass('obligatorio')
        $(this.parentNode).children(".obligatorio").remove();
        if ($(this).val() == "") {
            retorno = false;
            var padre = $(this.parentNode);
            $(this.parentNode).append("<span class='obligatorio'>! Obligatorio</span>");
            $(this.parentNode).addClass('obligatorio')
        }

    });
    return retorno;
}



function ValidateFormChofer() {
    var retorno = true;
    var controles = $("#tab4").find('[data-obligatorio="True"]');
    var mensajehtml = "";
    $("#tab4").find('[data-obligatorio="True"]').each(function () {
        $(this.parentNode).removeClass('obligatorio')
        $(this.parentNode).children(".obligatorio").remove();
        if ($(this).val() == "") {
            retorno = false;
            var padre = $(this.parentNode);
            $(this.parentNode).append("<span class='obligatorio'>! Obligatorio</span>");
            $(this.parentNode).addClass('obligatorio')
            mensajehtml += "El campo <b>" + $(this).attr('placeholder') + "</b> es obligatorio <br>";
        }

    });
    if (!retorno) {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert(mensajehtml, 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
    return retorno;
}

function ValidateFormSocio() {
    var retorno = true;
    var controles = $("#tab3").find('[data-obligatorio="True"]');
    var mensajehtml = "";
    $("#tab3").find('[data-obligatorio="True"]').each(function () {
        $(this.parentNode).removeClass('obligatorio')
        $(this.parentNode).children(".obligatorio").remove();
        if ($(this).val() == "") {
            retorno = false;
            var padre = $(this.parentNode);
            $(this.parentNode).append("<span class='obligatorio'>! Obligatorio</span>");
            $(this.parentNode).addClass('obligatorio')
            mensajehtml += "El campo <b>" + $(this).attr('placeholder') + "</b> es obligatorio <br>";
        }

    });
    if (!retorno) {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert(mensajehtml, 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
    return retorno;
}


function ValidateFormCliente() {
    var retorno = true;
    var controles = $("#tab2").find('[data-obligatorio="True"]');
    var mensajehtml = "";
    $("#tab2").find('[data-obligatorio="True"]').each(function () {
        $(this.parentNode).removeClass('obligatorio')
        $(this.parentNode).children(".obligatorio").remove();
        if ($(this).val() == "") {
            retorno = false;
            var padre = $(this.parentNode);
            $(this.parentNode).append("<span class='obligatorio'>! Obligatorio</span>");
            $(this.parentNode).addClass('obligatorio')
            mensajehtml += "El campo <b>" + $(this).attr('placeholder') + "</b> es obligatorio <br>";
        }

    });
    if (!retorno) {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert(mensajehtml, 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
    return retorno;
}

function ValidateFormTab() {
    var flag = true;
    var tipos = $("#cmbTIPO").val();
    for (i = 0; i < tipos.length; i++) {
        var aux = parseInt(tipos[i]);
        switch (aux) {
            case 4:
                if (!ValidateFormCliente())
                    flag = false;
                break;
            case 5:
                if (!ValidateFormCliente())
                    flag = false;
                break;
            case 6:
                if (!ValidateFormSocio())
                    flag = false;
                break;
            case 7:
                if (!ValidateFormChofer())
                    flag = false;
                break;
            case 8:
                if (!ValidateFormChofer())
                    flag = false;
                break;
        }
    }
    return flag;
}



function SaveObj() {
    if (ValidateFormTab()) {
        
        var jsonText = GetJSONObject(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM         
        CallServer(formname + "/SaveObject", jsonText, 5);
    }
}

function SaveObjResult(data) {
    if (data != "") {
        if (data.d != "OK" && data.d.toString() != "ERROR") {
            jQuery.jGrowl("Registro actualizado con éxito");
            $(selectobj).html(data.d);
        }
        else {
            CleanForm(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM  
            ReloadData();
        }
    }
}


/*********************FUNCIONES PARA OCULTAR Y MOSTRAR SECCIONES**********************/

function HideEdit() {
    $("#datos").hide('fast', function () {
        var cambiosCSS1 =
                {
                    width: "100%"
                };
        $("#listado").css(cambiosCSS1);
        $("#edit").css({ 'display': '' })
    });
}

function ShowEdit() {

    $("#listado").animate({
        width: "50%"
    }, 100, function () {
        $('#datos').show("fast");
        $("#edit").css({ 'display': 'none' })
    });

}

function HideList() {
    $("#listado").hide('fast', function () {
        var cambiosCSS1 =
                {
                    width: "100%"
                };
        $("#datos").css(cambiosCSS1);
        $("#list").css({ 'display': '' })
    });
}

function ShowList() {
    $("#datos").animate({
        width: "49%"
    }, 100, function () {
        $('#listado').show("fast");
    });
    $("#list").css({ 'display': 'none' })

}

function ShowBusqueda() {
    $('#busqueda').show("fast");
}








function SetForm() {
    CleanForm();
    SetPaisProvinciaCanton("cmbPAIS", "cmbPROVINCIA", "cmbCANTON");
    SetPaisProvinciaCanton("cmbPAISEMISION", "cmbPROVINCIAEMISION", "cmbCANTONEMISION");
    SetPaisProvinciaCanton("cmbPAISRENOVACION", "cmbPROVINCIARENOVACION", "cmbCANTONRENOVACION");
    SetPaisProvinciaCanton("cmbPAISNACIMIENTO", "cmbPROVINCIANACIMIENTO", "cmbCANTONNACIMIENTO");
    //$("#txtCIRUC").on("change", validaCIRUC);
    SetAutocompleteById("txtAGENTENOMBRE");

}

function GetJSONObject() {
    var obj = {};
    obj["per_codigo"] = $("#txtCODIGO").val();
    obj["per_codigo_key"] = $("#txtCODIGO_key").val();
    obj["per_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["per_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    obj["per_ciruc"] = $("#txtCIRUC").val();
    obj["per_id"] = $("#txtID").val();
    obj["per_tipoid"] = $("#cmbTIPOID").val();
    obj["per_nombres"] = $("#txtNOMBRES").val();
    obj["per_apellidos"] = $("#txtAPELLIDOS").val();
    obj["per_direccion"] = $("#txtDIRECCION").val();
    obj["per_telefono"] = $("#txtTELEFONO").val();
    obj["per_celular"] = $("#txtCELULAR").val();
    obj["per_mail"] = $("#txtMAIL").val();
    obj["per_observacion"] = $("#txtOBSERVACION").val();
    obj["per_pais"] = $("#cmbPAIS").val();
    obj["per_provincia"] = $("#cmbPROVINCIA").val();
    obj["per_canton"] = $("#cmbCANTON").val();
    obj["per_parroquia"] = $("#cmbPARROQUIA").val();
    obj["per_contribuyente"] = $("#txtCONTRIBUYENTE").val();
    obj["per_contribuyente_especial"] = GetCheckValue($("#chkCONTRIBUYENTEESPECIAL"));
    obj["per_contacto"] = $("#txtCONTACTO").val();
    obj["per_contacto_direccion"] = $("#txt_CONTACTODIRECCION").val();
    obj["per_contacto_telefono"] = $("#txtCONTACTOTELEFONO").val();
    obj["per_razon"] = $("#txtRAZON").val();
    obj["per_representantelegal"] = $("#cmbREPRESENTANTELEGAL").val();
    obj["per_paginaweb"] = $("#txtPAGINAWEB").val();
    obj["per_estado"] = GetCheckValue($("#chkESTADO"));
    obj["tipos"] = $("#cmbTIPO").val();
    obj["pxt"] = GetPXTObj();
    obj["per_genero"] = $("#cmbGENERO").val();
    obj["per_cpersona"] = $("#cmbCPERSONA").val();
    obj["per_tpersona"] = $("#cmbTPERSONA").val();
    obj["per_listaprecio"] = $("#cmbLISTAPRECIO").val();
    obj["per_politica"] = $("#cmbPOLITICA").val();
    obj["per_retiva"] = $("#cmbRETIVA").val();
    obj["per_retfuente"] = $("#cmbRETFUENTE").val();
    obj["per_agente"] = $("#txtAGENTE").val();
    obj["per_bloqueo"] = $("#txtBLOQUEO").val();
    obj["per_tarjeta"] = $("#txtTARJETA").val();
    obj["per_cupo"] = $("#txtCUPO").val();
    obj["per_ilimitado"] = $("#txtILIMITADO").val();
    obj["per_impuesto"] = $("#txtIMPUESTO").val();
    obj["chofer"] = GetChoferObj();
    obj["socio"] = GetSocioObj();
    obj = SetAuditoria(obj);
    //if (validaCIRUC()){
         var jsonText = JSON.stringify({ objeto: obj });
        return jsonText;
    /*}
    else{
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert('Cedula o ruc incorrectos...', 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default

        });
        var jsonText = JSON.stringify({ objeto: null });
        return jsonText;
    }*/
   
}

function validaCIRUC(){
  if (Valida_CIRUC($("#txtCIRUC").val())) {
      $("#saveedit").prop("disabled", false);
      return true;
    }
    else {
        $("#saveedit").prop("disabled", true);
        return false;
     }
}



function GetPXTObj() {
    var objTipos = {};
    var objCliente = new Array();   
    objCliente[0] = $("#txtCATCLIENTE").val();
    objCliente[1] = $("#txtPOLCLIENTE").val();
    var objProveedor = new Array();  
    objProveedor[0] = $("#txtCATPROVEEDOR").val();
    objProveedor[1] = $("#txtPOLPROVEEDOR").val();
    var objChofer = new Array();   
    objChofer[0] = $("#txtCATCHOFER").val();
    objChofer[1] = $("#txtPOLCHOFER").val();
    var objSocio = new Array();    
    objSocio[0] = $("#txtCATSOCIO").val();
    objSocio[1] = $("#txtPOLSOCIO").val();

    objTipos["cliente"] = objCliente;
    objTipos["proveedor"] = objProveedor;
    objTipos["socio"] = objSocio;
    objTipos["chofer"] = objChofer;
    objTipos = SetAuditoria(objTipos);
    return objTipos;
}


function GetChoferObj() {
    var obj = {};
    obj["cho_persona"] = $("#txtCODIGO").val();
    obj["cho_persona_key"] = $("#txtCODIGO_key").val();
    obj["cho_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cho_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    obj["cho_nrolicencia"] = $("#txtNROLICENCIA").val();
    obj["cho_puntoslicencia"] = $("#txtPUNTOSLICENCIA").val();
    obj["cho_tiposangre"] = $("#txtTIPOSANGRE").val();
    obj["cho_emisionlicencia"] = $("#cmbEMISIONLICENCIA").val();
    obj["cho_renovacion"] = $("#cmbRENOVACION").val();
    obj["cho_caducidadlicencia"] = $("#cmbCADUCIDADLICENCIA").val();
    obj["cho_tipolicencia"] = $("#txtTIPOLICENCIA").val();
    obj["cho_paisemision"] = $("#cmbPAISEMISION").val();
    obj["cho_provinciaemision"] = $("#cmbPROVINCIAEMISION").val();
    obj["cho_cantonemision"] = $("#cmbCANTONEMISION").val();
    obj["cho_paisrenovacion"] = $("#cmbPAISRENOVACION").val();
    obj["cho_provinciarenovacion"] = $("#cmbPROVINCIARENOVACION").val();
    obj["cho_cantonrenovacion"] = $("#cmbCANTONRENOVACION").val();
    obj["cho_observacionlicencia"] = $("#cmbOBSERVACIONLICENCIA").val();
    obj["cho_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    return obj;
}

function GetSocioObj() {
    var obj = {};
    obj["soc_persona"] = $("#txtCODIGO").val();
    obj["soc_persona_key"] = $("#txtCODIGO_key").val();
    obj["soc_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["soc_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    obj["soc_foto"] = $("#txtCODIGO").val();
    obj["soc_fechanacimiento"] = $("#cmbFECHANACIMIENTO").val();
    obj["soc_paisnacimiento"] = $("#cmbPAISNACIMIENTO").val();
    obj["soc_provincianacimiento"] = $("#cmbPROVINCIANACIMIENTO").val();
    obj["soc_cantonnacimiento"] = $("#cmbCANTONNACIMIENTO").val();
    obj["soc_estadocivil"] = $("#cmbESTADOCIVIL").val();
    obj["soc_cargas"] = $("#txtCARGAS").val();
    obj["soc_inscripcion"] = $("#txtINSCRIPCION").val();
    obj["soc_fechainscripcion"] = $("#cmbFECHAINSCRIPCION").val();
    obj["soc_fechasalida"] = $("#cmbFECHASALIDA").val();
    obj["soc_razonsalida"] = $("#txtRAZONSALIDA").val();
    obj["soc_lugartrabajo"] = $("#txtLUGARTRABAJO").val();
    obj["soc_cargotrabajo"] = $("#cmbCARGOTRABAJO").val();
    obj["soc_departamento"] = $("#cmbDEPARTAMENTO").val();
    obj["soc_direcciontrabajo"] = $("#txtDIRECCIONTRABAJO").val();
    obj["soc_telefonotrabajo"] = $("#txtTELEFONOTRABAJO").val();
    obj["soc_faxtrabajo"] = $("#txtFAXTRABAJO").val();
    obj["soc_nroiess"] = $("#txtNROIESS").val();
    obj["soc_banco"] = $("#cmbBANCO").val();
    obj["soc_tipocuenta"] = $("#cmbTIPOCUENTA").val();
    obj["soc_nrocuenta"] = $("#txtNROCUENTA").val();
    obj["soc_nivelinstruccion"] = $("#cmbNIVELINSTRUCCION").val();
    obj["soc_postgrado"] = $("#txtPOSTGRADO").val();
    obj["soc_doctorado"] = $("#txtDOCTORADO").val();
    obj["soc_reconocimientos"] = $("#txtRECONOCIMIENTOS").val();
    obj["soc_titulos"] = $("#txtTITULOS").val();
    obj["soc_profesion"] = $("#cmbPROFESION").val();
    obj["soc_fechagrado"] = $("#cmbFECHAGRADO").val();
    obj["soc_institucion"] = $("#txtINSTITUCION").val();
    obj["soc_conadis"] = $("#txtCONADIS").val();
    obj["soc_empresaconyuge"] = parseInt(empresasigned["emp_codigo"]);
    obj["soc_codigoconyuge"] = $("#cmbCODIGOCONYUGE").val();
    obj["soc_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    return obj;
}






function SetJSONObject(obj) {
    $("#txtCODIGO").val(obj["per_codigo"]);
    $("#txtCODIGO_key").val(obj["per_codigo_key"]);
    $("#txtCIRUC").val(obj["per_ciruc"]);
    $("#txtID").val(obj["per_id"]);
    $("#cmbTIPOID").val(obj["per_tipoid"]);
    $("#txtNOMBRES").val(obj["per_nombres"]);
    $("#txtAPELLIDOS").val(obj["per_apellidos"]);
    $("#txtDIRECCION").val(obj["per_direccion"]);
    $("#txtTELEFONO").val(obj["per_telefono"]);
    $("#txtCELULAR").val(obj["per_celular"]);
    $("#txtMAIL").val(obj["per_mail"]);
    $("#txtOBSERVACION").val(obj["per_observacion"]);
    $("#cmbPAIS").val(obj["per_pais"]);
    $("#cmbPROVINCIA").val(obj["per_provincia"]);
    $("#cmbCANTON").val(obj["per_canton"]);
    $("#cmbPARROQUIA").val(obj["per_parroquia"]);
    $("#txtCONTRIBUYENTE").val(obj["per_contribuyente"]);
    $("#chkCONTRIBUYENTEESPECIAL").prop("checked", SetCheckValue(obj["per_contribuyente_especial"]));
    $("#txtCONTACTO").val(obj["per_contacto"]);
    $("#txt_CONTACTODIRECCION").val(obj["per_contacto_direccion"]);
    $("#txtCONTACTOTELEFONO").val(obj["per_contacto_telefono"]);
    $("#txtRAZON").val(obj["per_razon"]);
    $("#cmbREPRESENTANTELEGAL").val(obj["per_representantelegal"]);
    $("#txtPAGINAWEB").val(obj["per_paginaweb"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["per_estado"]));
    var objtipos = new Array();
    if (obj["tipos"].length == 0)
        objtipos[0] = 4;
    for (i = 0; i < obj["tipos"].length; i++) {
        objtipos[i] = obj["tipos"][i]["pxt_tipo"].toString();
        var aux = parseInt(objtipos[i]);
        switch (aux)
        {
        case cCliente:
            $("#txtCATCLIENTE").val(obj["tipos"][i]["pxt_cat_persona"]);
            $("#txtPOLCLIENTE").val( obj["tipos"][i]["pxt_politicas"]);
            break;
        case cProveedor:
            $("#txtCATPROVEEDOR").val(obj["tipos"][i]["pxt_cat_persona"]);
            $("#txtPOLPROVEEDOR").val( obj["tipos"][i]["pxt_politicas"]);
            break;
        case cChofer:
            $("#txtCATCHOFER").val(obj["tipos"][i]["pxt_cat_persona"]);
            $("#txtPOLCHOFER").val( obj["tipos"][i]["pxt_politicas"]);
            break;
        case cSocio:
            $("#txtCATSOCIO").val(obj["tipos"][i]["pxt_cat_persona"]);
            $("#txtPOLSOCIO").val( obj["tipos"][i]["pxt_politicas"]);
            break;     
        }    
     }

    $("#cmbTIPO").val(objtipos);
    $('#cmbTIPO').trigger('liszt:updated');
    $("#cmbGENERO").val(obj["per_genero"]);
    $("#cmbCPERSONA").val(obj["per_cpersona"]);
    $("#cmbTPERSONA").val(obj["per_tpersona"]);
    $("#cmbLISTAPRECIO").val(obj["per_listaprecio"]);
    $("#cmbPOLITICA").val(obj["per_politica"]);
    $("#cmbRETIVA").val(obj["per_retiva"]);
    $("#cmbRETFUENTE").val(obj["per_retfuente"]);
    $("#txtAGENTE").val(obj["per_agente"]);
    $("#txtAGENTENOMBRE").val(obj["per_agentenombre"]);
    $("#txtBLOQUEO").val(obj["per_bloqueo"]);
    $("#txtTARJETA").val(obj["per_tarjeta"]);
    $("#txtCUPO").val(obj["per_cupo"]);
    $("#txtILIMITADO").val(obj["per_ilimitado"]);
    $("#txtIMPUESTO").val(obj["per_impuesto"]);
    SetJSONObjectSocio(obj["socio"]);
    SetJSONObjectChofer(obj["chofer"]);   
    //    $("#chkCHO_ESTADO").prop("checked", SetCheckValue(obj["chofer"]["cho_estado"]);

    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}


function SetJSONObjectChofer(obj) {
    $("#txtNROLICENCIA").val(obj["cho_nrolicencia"]);
    $("#txtPUNTOSLICENCIA").val(obj["cho_puntoslicencia"]);
    $("#txtTIPOSANGRE").val(obj["cho_tiposangre"]);
    $("#cmbEMISIONLICENCIA").val(GetDateStringValue(obj["cho_emisionlicencia"]));
    $("#cmbRENOVACION").val(GetDateStringValue(obj["cho_renovacion"]));
    $("#cmbCADUCIDADLICENCIA").val(GetDateStringValue(obj["cho_caducidadlicencia"]));
    $("#txtTIPOLICENCIA").val(obj["cho_tipolicencia"]);
    $("#cmbPAISEMISION").val(obj["cho_paisemision"]);
    $("#cmbPROVINCIAEMISION").val(obj["cho_provinciaemision"]);
    $("#cmbCANTONEMISION").val(obj["cho_cantonemision"]);
    $("#cmbPAISRENOVACION").val(obj["cho_paisrenovacion"]);
    $("#cmbPROVINCIARENOVACION").val(obj["cho_provinciarenovacion"]);
    $("#cmbCANTONRENOVACION").val(obj["cho_cantonrenovacion"]);
    $("#cmbOBSERVACIONLICENCIA").val(obj["cho_observacionlicencia"]);
}

function SetJSONObjectSocio(obj) {
   
    $("#cmbFECHANACIMIENTO").val(GetDateStringValue(obj["soc_fechanacimiento"]));
    $("#cmbPAISNACIMIENTO").val(obj["soc_paisnacimiento"]);
    $("#cmbPROVINCIANACIMIENTO").val(obj["soc_provincianacimiento"]);
    $("#cmbCANTONNACIMIENTO").val(obj["soc_cantonnacimiento"]);
    $("#cmbESTADOCIVIL").val(obj["soc_estadocivil"]);
    $("#txtCARGAS").val(obj["soc_cargas"]);
    $("#txtINSCRIPCION").val(obj["soc_inscripcion"]);
    $("#cmbFECHAINSCRIPCION").val(GetDateStringValue(obj["soc_fechainscripcion"]));
    $("#cmbFECHASALIDA").val(GetDateStringValue(obj["soc_fechasalida"]));
    $("#txtRAZONSALIDA").val(obj["soc_razonsalida"]);
    $("#txtLUGARTRABAJO").val(obj["soc_lugartrabajo"]);
    $("#cmbCARGOTRABAJO").val(obj["soc_cargotrabajo"]);
    $("#cmbDEPARTAMENTO").val(obj["soc_departamento"]);
    $("#txtDIRECCIONTRABAJO").val(obj["soc_direcciontrabajo"]);
    $("#txtTELEFONOTRABAJO").val(obj["soc_telefonotrabajo"]);
    $("#txtFAXTRABAJO").val(obj["soc_faxtrabajo"]);
    $("#txtNROIESS").val(obj["soc_nroiess"]);
    $("#cmbBANCO").val(obj["soc_banco"]);
    $("#cmbTIPOCUENTA").val(obj["soc_tipocuenta"]);
    $("#txtNROCUENTA").val(obj["soc_nrocuenta"]);
    $("#cmbNIVELINSTRUCCION").val(obj["soc_nivelinstruccion"]);
    $("#txtPOSTGRADO").val(obj["soc_postgrado"]);
    $("#txtDOCTORADO").val(obj["soc_doctorado"]);
    $("#txtRECONOCIMIENTOS").val(obj["soc_reconocimientos"]);
    $("#txtTITULOS").val(obj["soc_titulos"]);
    $("#cmbPROFESION").val(obj["soc_profesion"]);
    $("#cmbFECHAGRADO").val(GetDateStringValue(obj["soc_fechagrado"]));
    $("#txtINSTITUCION").val(obj["soc_institucion"]);
    $("#txtCONADIS").val(obj["soc_conadis"]);

    $("#cmbCODIGOCONYUGE").val(obj["soc_codigoconyuge"]);

}

function GetJSONSearch() {
    var obj = {};
    obj["per_ciruc"] = $("#txtCIRUC_S").val();
    obj["per_razon"] = $("#txtRAZON_S").val();
    obj["per_nombres"] = $("#txtNOMBRE_S").val();
    obj["per_empresa"] = parseInt(empresasigned["emp_codigo"]);
    if ($("#cmbESTADO_S").val() != "")
        obj["per_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
    $("#txtRAZON_S").val("");
    $("#txtCIRUC_S").val("");
    $("#txtNOMBRE_S").val("");
    $("#cmbESTADO_S").val("");
    ReloadData();
}

function CleanForm() {
    $("#txtCODIGO").val("");
    $("#txtCODIGO_key").val("");


    $("#txtCIRUC").val("");
    $("#txtID").val("");
    $("#cmbTIPOID").val("");
    $("#txtNOMBRES").val("");
    $("#txtAPELLIDOS").val("");
    $("#txtDIRECCION").val("");
    $("#txtTELEFONO").val("");
    $("#txtCELULAR").val("");
    $("#txtMAIL").val("");
    $("#txtOBSERVACION").val("");
    $("#cmbPAIS").val("");
    $("#cmbPROVINCIA").val("");
    $("#cmbCANTON").val("");
    $("#txtCONTRIBUYENTE").val("");
    $("#chkCONTRIBUYENTEESPECIAL").prop("checked", false);
    $("#txtCONTACTO").val("");
    $("#txt_CONTACTODIRECCION").val("");
    $("#txtCONTACTOTELEFONO").val("");
    $("#txtRAZON").val("");
    $("#cmbREPRESENTANTELEGAL").val("");
    $("#txtPAGINAWEB").val("");
    $("#cmbPARROQUIA").val("");

    var tclicodpro = $("#txttclipro").val();
    var objtipos = new Array();
   if (tclicodpro > 0) {      
       objtipos[0] = tclicodpro;
   } else {
       objtipos[0] = 4;
   }

    $("#cmbTIPO").val(objtipos);
    $('#cmbTIPO').trigger('liszt:updated');
    $("#cmbGENERO").val("");
    $("#cmbCPERSONA").val("");
    $("#cmbTPERSONA").val("");
    $("#cmbLISTAPRECIO").val("");
    $("#cmbPOLITICA").val("");
    $("#cmbRETIVA").val("");
    $("#cmbRETFUENTE").val("");
    $("#txtAGENTE").val("");
    $("#txtAGENTENOMBRE").val("");
    $("#txtBLOQUEO").val("");
    $("#txtTARJETA").val("");
    $("#txtCUPO").val("");
    $("#txtILIMITADO").val("");
    $("#txtIMPUESTO").val("");
    $("#chkESTADO").prop("checked", true);

    $("#txtCATCLIENTE").val("");
    $("#txtPOLCLIENTE").val("");
    $("#txtCATPROVEEDOR").val("");
    $("#txtPOLPROVEEDOR").val("");
    $("#txtCATCHOFER").val("");
    $("#txtPOLCHOFER").val("");
    $("#txtCATSOCIO").val("");
    $("#txtPOLSOCIO").val("");
    CleanChoferForm();
    CleanSocioForm();
}


function CleanChoferForm() {
    $("#txtNROLICENCIA").val("");
    $("#txtPUNTOSLICENCIA").val("");
    $("#txtTIPOSANGRE").val("");
    $("#cmbEMISIONLICENCIA").val("");
    $("#cmbRENOVACION").val("");
    $("#cmbCADUCIDADLICENCIA").val("");
    $("#txtTIPOLICENCIA").val("");
    $("#cmbPAISEMISION").val("");
    $("#cmbPROVINCIAEMISION").val("");
    $("#cmbCANTONEMISION").val("");
    $("#cmbPAISRENOVACION").val("");
    $("#cmbPROVINCIARENOVACION").val("");
    $("#cmbCANTONRENOVACION").val("");
    $("#cmbOBSERVACIONLICENCIA").val("");
}



function CleanSocioForm() {
    $("#txtCODIGO").val("");
    $("#txtCODIGO_key").val("");
    $("#txtCODIGO").val("");
    $("#cmbFECHANACIMIENTO").val("");
    $("#cmbPAISNACIMIENTO").val("");
    $("#cmbPROVINCIANACIMIENTO").val("");
    $("#cmbCANTONNACIMIENTO").val("");
    $("#cmbESTADOCIVIL").val("");
    $("#txtCARGAS").val("");
    $("#txtINSCRIPCION").val("");
    $("#cmbFECHAINSCRIPCION").val("");
    $("#cmbFECHASALIDA").val("");
    $("#txtRAZONSALIDA").val("");
    $("#txtLUGARTRABAJO").val("");
    $("#cmbCARGOTRABAJO").val("");
    $("#cmbDEPARTAMENTO").val("");
    $("#txtDIRECCIONTRABAJO").val("");
    $("#txtTELEFONOTRABAJO").val("");
    $("#txtFAXTRABAJO").val("");
    $("#txtNROIESS").val("");
    $("#cmbBANCO").val("");
    $("#cmbTIPOCUENTA").val("");
    $("#txtNROCUENTA").val("");
    $("#cmbNIVELINSTRUCCION").val("");
    $("#txtPOSTGRADO").val("");
    $("#txtDOCTORADO").val("");
    $("#txtRECONOCIMIENTOS").val("");
    $("#txtTITULOS").val("");
    $("#cmbPROFESION").val("");
    $("#cmbFECHAGRADO").val("");
    $("#txtINSTITUCION").val("");
    $("#txtCONADIS").val("");
    $("#cmbCODIGOCONYUGE").val("");




}

function Configurar() {

    window.location.href = "wfPersonaAutorizacion.aspx?codigo=" + $("#txtCODIGO").val();

}



function SetAutoCompleteObj(idobj, item) {
   
    if (idobj == "txtAGENTENOMBRE" ) {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_apellidos + " " + item.per_nombres,
            info: item
        }
    }
    
}

function GetAutoCompleteObj(idobj, item) {
   
    if (idobj == "txtAGENTENOMBRE") {
        $("#txtAGENTE").val(item.info.per_codigo);
        $("#txtAGENTENOMBRE").val(item.info.per_apellidos + " " + item.info.per_nombres);
    }
    
}