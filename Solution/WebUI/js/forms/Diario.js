﻿//Archivo:          Diario.js
//Descripción:      Contiene las funciones comunes para la interfaz de gestion de diarios contables
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Diciembre 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfDiario.aspx";
var menuoption = "Diario";

function StopPropagation(event) {
    if (!event) var event = window.event;
    event.cancelBubble = true;
    if (event.stopPropagation) event.stopPropagation();
}

//Codigo ejecutado cuando el document esta listo
$(document).ready(function () {

    $("#comcabecera").find('.widgettitle .close').click(function () {
        $(this).parents('.widgetbox').fadeOut(function () {
            $(this).hide('fast', HideCabecera());
        });
    });

    $("#comdetalle").find('.widgettitle .close').click(function () {
        $(this).parents('.widgetbox').fadeOut(function () {
            $(this).hide('fast', HideDetalle());
        });
    });

    $('body').css('background', 'transparent');

    GetCabeceraComprobante($("#txtcodigocomp").val(), $("#txttipodoc").val()); //FUNCION DEFINIDA EN FUNCTIONS.JS

    
});


//Funcion que recupera la cabecera de un comprobante nuevo o de la bd
function GetCabeceraComprobanteResult(data) {
    var obj = JSON.parse(data);
    $("#txtcodigocomp").val(obj[0]);

    $('#comcab').html(obj[1]);
    $("#save").on("click", SaveObj);
    $("#print").on("click", PrintObj);
    $("#close").on("click", CloseObj);
    var codigocomp = $("#txtcodigocomp").val();
    if (codigocomp <= 0) {
        $("#print").css({ 'display': 'none' });
        
    }

    $("#invo").css({ 'display': 'none' });
    $("#contabilizacion").css({ 'display': 'none' });
    LoadCabecera();    
}



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
                LoadCabeceraResult(data);
            if (retorno == 1)
                SaveObjResult(data);
            if (retorno == 2)
                CloseObjResult(data);
         


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




function LoadCabecera() {

    var origen = $("#txtorigen").val();
    if (origen == "") {
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = $("#txtcodigocomp").val();
        var jsonText = JSON.stringify({ objeto: obj });

        if ($("#comcabeceracontent").length > 0) {
            CallServer(formname + "/GetCabecera", jsonText, 0);
        }
    }
}

function LoadCabeceraResult(data) {
    if (data != "") {
        $('#comcabeceracontent').html(data.d);
        LoadDiario();
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


function EditableRow(idtabla) {
    var columnas = $("#" + idtabla).find("th");
    var newrow = "<tr>";
    for (var i = 0; i < columnas.length; i++) {
        newrow += "<td><input type='text' value='" + $(columnas[i])[0].innerHTML + "'/></td>";
    }
    newrow += "</tr>";
    $("#" + idtabla).append(newrow);

}


function SetForm() {

    //SetAutocompleteById("txtCODCLIPRO");    

}







function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtIDPER" || idobj == "txtIDPER_PR") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_id,
            info: item
        }
    }
    if (idobj == "txtIDPOL") {
        return {
            label: item.pol_id + "," + item.pol_nombre,
            value: item.pol_id,
            info: item
        }
    }
    if (idobj == "txtIDALM_D") {
        return {
            label: item.alm_id + "," + item.alm_nombre,
            value: item.alm_id,
            info: item
        }
    }

}

function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtIDPER_PR") {
        $("#txtCODPER_PR").val(item.info.per_codigo);
        $("#txtNOMBRES_PR").val(item.info.per_apellidos+" " +item.info.per_nombres);        

    }
    if (idobj == "txtIDPER") {
        $("#txtCODPER").val(item.info.per_codigo);
        $("#txtNOMBRES").val(item.info.per_apellidos + " " + item.info.per_nombres);        
    }
    if (idobj == "txtIDPOL") {
        $("#txtPOLITICA").val(item.info.pol_nombre);
        $("#txtCODPOL").val(item.info.pol_codigo);
        $("#txtPORCENTAJE").val(item.info.pol_porc_desc);
        $("#txtNROPAGOS").val(item.info.pol_nro_pagos);
        $("#txtDIASPLAZO").val(item.info.pol_dias_plazo);
        $("#txtPORCPAGOCON").val(item.info.pol_porc_pago_con);


    }
    if (idobj == "txtIDCUE") {
        $("#txtIDCUE").val(item.info.cue_id);
        $("#txtCODCUE").val(item.info.cue_codigo);
        $("#txtCUENTA").val(item.info.cue_nombre);
        LoadModulo();
        //alert(ui.item.label);
    }
    if (idobj == "txtIDALM_D") {
        $("#txtIDALM_D").val(item.info.alm_id);
        $("#txtCODALM_D").val(item.info.alm_codigo);
        $("#txtNOMBREALM_D").val(item.info.alm_nombre);
        //$("#txtCUENTA").val(item.info.cue_nombre);
        
    }
}





/***********************SAVE FUNCTIONS ***********************************/

function GetComprobanteObj() {

    var now = new Date();
    
    var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHACOMP").val()); // $("#txtFECHA_P").datepicker("getDate");
    var almacen = parseInt($("#txtCODALMACEN").val());
    var pventa = parseInt($("#txtCODPVENTA").val());

    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = $("#txtcodigocomp").val();
    obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
    obj["com_ctipocom"] = parseInt($("#txtCTIPOCOM").val());  //2 FACT
    //obj["com_fecha"] = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate(), now.getHours(), now.getMinutes(), now.getSeconds(), now.getMilliseconds());
    obj["com_numero"] = $("#txtNUMERO").val();
    obj["com_fecha"] = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate(), now.getHours(), now.getMinutes(), now.getSeconds(), now.getMilliseconds());
    obj["com_doctran"] = $("#numerocomp").html();    
    obj["com_nocontable"] = parseInt($("#txtnocontable").val());
    obj["com_periodo"] = currentDate.getFullYear();
    obj["com_almacen"] = almacen;
    obj["com_pventa"] = pventa;
    obj["com_concepto"] = $("#txtCONCEPTO").val();
    obj["com_centro"] = $("#txtCODCEN").val();
    //obj["com_codclipro"] = parseInt($("#txtCODPER").val());
    //obj["com_agente"] = parseInt($("#txtCODVEN").val());
    obj["contables"] = GetDetalleDiario();
    obj = SetAuditoria(obj);
    return obj;
}



function FillWith(caracter, largo, valor) {
    var cantidad = largo - valor.length
    if (cantidad > 0) {
        var fill = "";
        for (var i = 0; i < cantidad; i++) {
            fill = fill + caracter;
        }
        valor = fill + valor;
    }
    return valor;
}

function ClearValidate() {
    var controles = $("#comcabecera").find('[data-obligatorio="True"]');
    $("#comcabecera").find('[data-obligatorio="True"]').each(function () {
        $(this.parentNode).removeClass('obligatorio')
        $(this.parentNode).children(".obligatorio").remove();
    });
}

function ValidateForm() {
    var retorno = true;
    var controles = $("#comcabecera").find('[data-obligatorio="True"]');
    var mensajehtml = "";
    $("#comcabecera").find('[data-obligatorio="True"]').each(function () {
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

    retorno = ValidateDiario();
    return retorno;
}







function SaveObj() {
    if (ValidateForm()) {
        var obj = {};
        obj["comprobante"] = GetComprobanteObj();
        //obj["drecibo"] = objdrecibo;
        //obj["rutaxfactura"] = GetRutaxFacturaObj();
        var jsonText = JSON.stringify({ objeto: obj });
        //var jsonText = GetComprobanteObj(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
        CallServer(formname + "/SaveObject", jsonText, 1);

    }
}


function PrintObj() {
    window.location = "wfDcontablePrint.aspx?codigocomp=" + $("#txtcodigocomp").val() + "&empresa=" + parseInt(empresasigned["emp_codigo"]);
}

function SaveObjResult(data) {
    if (data != "") {
        var cod = parseInt(data.d);
        if (cod > 0) {
            //if (data.d == "OK") {
            jQuery.alerts.dialogClass = 'alert-success';
            jAlert('Comprobante guardado exitosamente...', 'Éxito', function () {
                jQuery.alerts.dialogClass = null; // reset to default                
                location.reload();
            });

        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al guardar el comprobante...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}


function CloseObj() {
    if (ValidateForm()) {
        var obj = {};
        obj["comprobante"] = GetComprobanteObj();
        var jsonText = JSON.stringify({ objeto: obj });
        CallServer(formname + "/CloseObject", jsonText, 2);

    }
}

function CloseObjResult(data) {
    if (data != "") {
        var cod = parseInt(data.d);
        if (cod > 0) {
            jQuery.alerts.dialogClass = 'alert-success';
            jAlert('Comprobante guardado exitosamente...', 'Éxito', function () {
                jQuery.alerts.dialogClass = null; // reset to default                
                window.location = formname + "?codigocomp=" + data.d;
            });

        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al guardar el comprobante...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}




function GetCallPersona(obj, id) {
    if (id == "btncallper") {
        $("#txtCODCLIPRO").val(obj.per_id);
        $("#txtCODPER").val(obj.per_codigo);
        $("#txtCIRUC").val(obj.per_ciruc);
        $("#txtNOMBRES").val(obj.per_apellidos + " " + obj.per_nombres);
        $("#txtRAZON").val(obj.per_razon);
        $("#txtRUC").val(obj.per_ciruc);
        $("#txtDIRECCION").val(obj.per_direccion);
        $("#txtTELEFONO").val(obj.per_telefono);

        $("#txtCODPOL").val(obj.per_politica);
        $("#txtIDPOL").val(obj.per_politicaid);
        $("#txtPOLITICA").val(obj.per_politicanombre);

    }
    if (id == "btncallrem") {
        $("#txtIDREM").val(obj.per_id);
        $("#txtCODREM").val(obj.per_codigo);
        $("#txtNOMBRESREM").val(obj.per_apellidos + " " + obj.per_nombres);
        $("#txtDIRECCIONREM").val(obj.per_direccion);
        $("#txtTELEFONOREM").val(obj.per_telefono);

    }
    if (id == "btncalldes") {
        $("#txtIDDES").val(obj.per_id);
        $("#txtCODDES").val(obj.per_codigo);
        $("#txtNOMBRESDES").val(obj.per_apellidos + " " + obj.per_nombres);
        $("#txtDIRECCIONDES").val(obj.per_direccion);
        $("#txtTELEFONODES").val(obj.per_telefono);

    }
}

function CleanPersona(id) {
    if (id == "btncleanper") {
        $("#txtCODCLIPRO").val("");
        $("#txtCODPER").val("");
        $("#txtCIRUC").val("");
        $("#txtNOMBRES").val("");
        $("#txtRAZON").val("");
        $("#txtRUC").val("");
        $("#txtDIRECCION").val("");
        $("#txtTELEFONO").val("");

        $("#txtCODLIS").val("");
        $("#txtIDLIS").val("");
        $("#txtLISTA").val("");
    }
    if (id == "btncleanrem") {
        $("#txtIDREM").val("");
        $("#txtCODREM").val("");
        $("#txtNOMBRESREM").val("");
        $("#txtDIRECCIONREM").val("");
        $("#txtTELEFONOREM").val("");

    }
    if (id == "btncleandes") {
        $("#txtIDDES").val("");
        $("#txtCODDES").val("");
        $("#txtNOMBRESDES").val("");
        $("#txtDIRECCIONDES").val("");
        $("#txtTELEFONODES").val("");

    }
}