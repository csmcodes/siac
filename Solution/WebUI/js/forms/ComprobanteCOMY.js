//Archivo:          ComprobanteCOMY.js
//Descripción:      Contiene las funciones comunes para la interfaz de gestion de comprobantes (Facturas, Guias) de COMYTRANS
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Enero 2016
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfComprobanteCOMY.aspx";
var menuoption = "ComprobanteCOMY";

var recibocreated = false;
var objdrecibo = null;
var objcancelacion = null;

var saving = false;

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
    // $("#save").on("click", Despachar);
    //Despachar()
    $("#print").on("click", PrintObj);
    $("#close").on("click", SaveObj);
    $("#contabilizacion").on("click", ContObj);

    var codigocomp = $("#txtcodigocomp").val();
    if (codigocomp <= 0) {
        $("#contabilizacion").css({ 'display': 'none' });
        $("#print").css({ 'display': 'none' });
        $("#close").css({ 'display': 'none' });
        $("#parent-selector :input").attr("disabled", true);

    }

    //$("#print").on("click", { titulo: "ImpFact", parametros:"parameter1="+codigocomp, reporte: 'FAC' }, PrintReport);

    //  $("#print").on("click", PrintObj);
    // $("#print").on("click", Despachar);
    $("#despachar").on("click", Despachar);
    $("#invo").css({ 'display': 'none' });

    LoadCabecera();
    LoadPie();
    GetPrintVersion();
    GetPrintHTML();
    GetPrintFormat();
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
                LoadDetalleResult(data);
            if (retorno == 2)
                LoadPieResult(data);
            if (retorno == 3)
                LoadProductResult(data);
            if (retorno == 4)
                GetPriceResult(data);
            if (retorno == 5)
                SaveObjResult(data);
            if (retorno == 6)
                GetHojasRutaResult(data);
            if (retorno == 7)
                GetDatosHojaRutaResult(data);
            if (retorno == 8)
                CloseObjResult(data);
            if (retorno == 9)
                CallDespacharResult(data);
            if (retorno == 10)
                SaveObjResultDespachar(data);
            if (retorno == 11)
                GetAllRutasResult(data);
            if (retorno == 12)
                GetDatosPoliticaResult(data);
            if (retorno == 13)
                GetPrintVersionResult(data);
            if (retorno == 14)
                GetPrintHTMLResult(data);
            if (retorno == 15)
                GetPrintFormatResult(data);
            if (retorno == 16)
                AddDestinoResult(data);
            if (retorno == "GetAgente")
                GetAgenteResult(data);

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

function GetPrintVersion() {

    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
    obj["crea_usr"] = usuariosigned["usr_id"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer("ws/Metodos.asmx/GetPrintVersion", jsonText, 13);
}


function GetPrintVersionResult(data) {
    if (data != "") {
        $('#txtprintversion').val(data.d);
    }
}

function GetPrintHTML() {

    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
    obj["crea_usr"] = usuariosigned["usr_id"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer("ws/Metodos.asmx/GetPrintHTML", jsonText, 14);
}


function GetPrintHTMLResult(data) {
    if (data != "") {
        $('#txtprinthtml').val(data.d);
    }
}

function GetPrintFormat() {

    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
    obj["crea_usr"] = usuariosigned["usr_id"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer("ws/Metodos.asmx/GetPrintFormat", jsonText, 15);
}


function GetPrintFormatResult(data) {
    if (data != "") {
        $('#txtprintformat').val(data.d);
    }
}




function LoadCabecera() {

    var origen = $("#txtorigen").val();
    if (origen == "") {
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = $("#txtcodigocomp").val();
        obj["com_almacen"] = parseInt($("#txtCODALMACEN").val());
        obj["com_pventa"] = parseInt($("#txtCODPVENTA").val());
        obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
        var jsonText = JSON.stringify({ objeto: obj });

        if ($("#comcabeceracontent").length > 0) {
            CallServer(formname + "/GetCabecera", jsonText, 0);
        }
    }
    else if (origen == "LGC") { //PLANILLA DE CLIENTES
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = $("#txtcodigocompref").val();
        obj["com_almacen"] = parseInt($("#txtCODALMACEN").val());
        obj["com_pventa"] = parseInt($("#txtCODPVENTA").val());
        obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
        var jsonText = JSON.stringify({ objeto: obj });

        if ($("#comcabeceracontent").length > 0) {
            CallServer(formname + "/GetCabeceraFromLGC", jsonText, 0);
        }
    }

}

function LoadCabeceraResult(data) {
    if (data != "") {
        $('#comcabeceracontent').html(data.d);
        LoadDetalle();
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

function LoadDetalle() {
    var origen = $("#txtorigen").val();
    if (origen == "") {
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = $("#txtcodigocomp").val();
        var jsonText = JSON.stringify({ objeto: obj });
        CallServer(formname + "/GetDetalle", jsonText, 1);
    }
    else if (origen == "LGC") { //PLANILLA DE CLIENTES
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = $("#txtcodigocompref").val();
        var jsonText = JSON.stringify({ objeto: obj });
        CallServer(formname + "/GetDetalleFromLGC", jsonText, 1);
    }

}

function LoadDetalleResult(data) {
    if (data != "") {
        $('#comdetallecontent').html(data.d);
    }
    SetFormDetalle();
    //EditableRow("detalletabla");

}

function LoadPie() {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    var now = new Date();
    var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHACOMP").val());
    obj["com_fecha"] = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate(), now.getHours(), now.getMinutes(), now.getSeconds(), now.getMilliseconds());

    var origen = $("#txtorigen").val();
    if (origen == "")
        obj["com_codigo"] = $("#txtcodigocomp").val();
    else if (origen == "LGC")  //PLANILLA DE CLIENTES
        obj["com_codigo"] = $("#txtcodigocompref").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetPie", jsonText, 2);

}

function LoadPieResult(data) {
    if (data != "") {
        $('#compiecontent').html(data.d);
    }


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

    SetAutocompleteById("txtCODCLIPRO");
    SetAutocompleteById("txtNOMBRESREM");
    SetAutocompleteById("txtNOMBRESRET");
    SetAutocompleteById("txtNOMBRESRED");
    SetAutocompleteById("txtNOMBRESDES");
    SetAutocompleteById("txtCODVEH");
    SetAutocompleteById("txtIDLIS");
    SetAutocompleteById("txtIDPOL");
    SetAutocompleteById("txtCODVEN");

    if ($("#txtCODDES").val() != "") {
        var objdes = {};
        objdes["des_empresa"] = empresasigned["emp_codigo"];
        objdes["des_persona"] = $("#txtCODDES").val();
        SetAutocompleteByIdParams("txtDIRECCIONDES", objdes);
    }
    var obj = {};
    obj["empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["almacen"] = parseInt($("#txtCODALMACEN").val());
    SetAutocompleteByIdParams("txtHOJARUTA", obj);

    //SetAutocompleteById("txtHOJARUTA");

    $("#txtALMACENPRO").on("change", complete3);
    $("#txtPVENTAPRO").on("change", complete3);
    $("#txtNUMEROPRO").on("change", complete9);
    //$("#cmbRUTA").on("change", GetHojasRuta);

    //$("#cmbRUTA").trigger("change");
    $("#cmbPOLITICA").on("change", GetDatosPolitica);
    $("#cmbPOLITICA").trigger("change");

    //$("#cmbHOJARUTA").on("change", GetDatosHojaRuta);  

}


function GetDatosPolitica() {
    var obj = {};
    obj["pol_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["pol_codigo"] = $("#cmbPOLITICA").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDatosPolitica", jsonText, 12);
}
function GetDatosPoliticaResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        $("#txtNROPAGOS").val(obj["pol_nro_pagos"]);
        $("#txtDIASPLAZO").val(obj["pol_dias_plazo"]);
        $("#txtPORCENTAJE").val(obj["pol_porc_desc"]);
        $("#txtPORCPAGOCON").val(obj["pol_porc_pago_con"]);
    }
}
function GetAllRutas() {
    var obj = {};
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetAllRutas", jsonText, 11);
}

function GetAllRutasResult(data) {
    if (data != "") {
        $("#cmbRUTA").replaceWith(data.d);
    }
}


function GetDatosHojaRuta() {
    //var hr = $("#cmbHOJARUTA").val();
    var hr = $("#txtIDHOJARUTA").val();
    if (hr != "") {
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = hr;
        var jsonText = JSON.stringify({ objeto: obj });
        CallServer(formname + "/GetDatosHojaRuta", jsonText, 7);
    }
    else
        CleanDatosHojaRuta();
}

function CleanDatosHojaRuta() {
    $("#txtFECHARUTA").val("");
    $("#txtNOMBRERUTA").val("");
    $("#txtVEHICULORUTA").val("");
    $("#txtCODVEH").val("");
    $("#txtPLACAVEH").val("");
    $("#txtDISCOVEH").val("");
    $("#txtSOCIO").val("");
    $("#txtCODSOC").val("");
    $("#txtCHOFER").val("");
    $("#txtCODCHO").val("");
}


function GetDatosHojaRutaResult(data) {
    CleanDatosHojaRuta();
    if (data != "") {
        if (data.d != "") {
            var obj = $.parseJSON(data.d);

            var comp = obj[0];
            var vehi = obj[1];
            var ruta = obj[2];
            var soci = obj[3];
            var chof = obj[4];

            $("#txtFECHARUTA").val(GetDateValue(comp["com_fecha"]));
            $("#txtNOMBRERUTA").val(ruta["rut_nombre"]);
            $("#txtCODVEH").val(vehi["veh_codigo"]);
            //$("#txtIDVEH").val(vehi["veh_id"]);
            $("#txtVEHICULORUTA").val("Placa: " + vehi["veh_placa"] + " / Disco: " + vehi["veh_disco"]);
            $("#txtPLACAVEH").val(vehi["veh_placa"]);
            $("#txtDISCOVEH").val(vehi["veh_disco"]);

            $("#txtCODSOC").val(soci["per_codigo"]);
            //$("#txtIDSOC").val(soci["per_id"]);
            $("#txtSOCIO").val(soci["per_apellidos"] + " " + soci["per_nombres"]);
            $("#txtCODCHO").val(chof["per_codigo"]);
            //$("#txtIDCHO").val(chof["per_id"]);
            $("#txtCHOFER").val(chof["per_apellidos"] + " " + chof["per_nombres"]);



        }
    }
}




function SetFormDetalle() {
    SetAutocompleteById("txtIDPRO");
    $("#cmbUMEDIDA").on("change", GetPrice);

    var codigocomp = $("#txtcodigocomp").val();
    if ($("#txtESTADO").val() == $("#txtCERRADO").val()) {
        var inputs = $('input, textarea, select');
        $(inputs).each(function () {
            $(this).prop("disabled", true);

        });
    }
}

function RemoveRow(btn) {
    var row = $(btn).parents('tr');
    var hascontrols = ($(row).find("input").length > 0) ? true : false;
    if ($(row)[0].id != "editrow" && !hascontrols) {
        jConfirm('¿Está seguro que desea eliminar el registro?', 'Eliminar', function (r) {
            if (r) {
                $(btn).parents('tr').fadeOut(function () {
                    $(this).remove();
                });
            }
        });
    }
    else
        CleanRow();
    CalculaTotales();
    StopPropagation();
}


function RemoveRowCan(btn) {
    var row = $(btn).parents('tr');
    var hascontrols = ($(row).find("input").length > 0) ? true : false;
    if ($(row)[0].id != "editrow" && !hascontrols) {
        jConfirm('¿Está seguro que desea eliminar el registro?', 'Eliminar', function (r) {
            if (r) {
                $(btn).parents('tr').fadeOut(function () {
                    $(this).remove();
                });
            }
        });
    }
    else
        CleanRowCan();
    CalculaTotalesCan();
    StopPropagation();
}


function CleanRow() {
    $("#txtCALCPRO").val("");
    $("#txtCODPRO").val("");
    $("#txtIDPRO").val("");
    $("#txtPRODUCTO").val("");
    $("#txtOBSERVACION").val("");
    $("#txtPESO").val("");
    $("#cmbUMEDIDA").children('option').removeAttr('disabled');
    $("#txtCANTIDAD").val(0);
    $("#txtPRECIO").val(0);
    $("#txtDESC").val(0);
    $("#txtTOTAL").val(0);
    $("#chkIVA").prop("checked", false);
    $("#txtIDPRO").select();
    return false;
}
function CleanRowCan() {
    $("#txtIDTIPO_P").val("");
    $("#txtCODTIPO_P").val("");
    $("#txtNOMBRETIPO_P").val("");
    $("#txtVALOR_P").val(0);
    $("#txtIDTIPO_P").select();
    return false;
}

function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCODCLIPRO") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_id,
            info: item
        }
    }
    if (idobj == "txtNOMBRESREM" || idobj == "txtNOMBRESDES" || idobj == "txtNOMBRESRET" || idobj == "txtNOMBRESRED") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_apellidos + " " + item.per_nombres,
            info: item
        }
    }
    
    if (idobj == "txtCODVEH") {
        return {
            label: item.veh_nombre,
            value: item.veh_id,
            info: item
        }
    }
    if (idobj == "txtIDPRO") {
        return {
            label: item.pro_id + "," + item.pro_nombre,
            value: item.pro_id,
            info: item
        }
    }
    if (idobj == "txtIDLIS") {
        return {
            label: item.lpr_id + "," + item.lpr_nombre,
            value: item.lpr_id,
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
    if (idobj == "txtCIRUC_P") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_ciruc,
            info: item
        }
    }
    if (idobj == "txtIDTIPO_P") {
        return {
            label: item.tpa_id + "," + item.tpa_nombre,
            value: item.tpa_id,
            info: item
        }
    }
    if (idobj == "txtHOJARUTA") {
        return {
            label: item.doctran + " " + item.razon,
            value: item.doctran,
            info: item
        }
    }
    if (idobj == "txtDIRECCIONDES") {
        return {
            label: item.des_destino,
            value: item.des_destino,
            info: item
        }
    }

    if (idobj == "txtCODVEN") {
        return {
            label: item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_codigo,
            info: item
        }
    }
}

function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCODCLIPRO") {
        $("#txtCODPER").val(item.info.per_codigo);
        $("#txtCIRUC").val(item.info.per_ciruc);
        $("#txtNOMBRES").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtRAZON").val(item.info.per_razon);
        $("#txtRUC").val(item.info.per_ciruc);
        $("#txtDIRECCION").val(item.info.per_direccion);
        $("#txtTELEFONO").val(item.info.per_telefono);
        $("#txtMAIL").val(item.info.per_mail);

        $("#txtCODLIS").val(item.info.per_listaprecio);
        $("#txtIDLIS").val(item.info.per_listaid);
        $("#txtLISTA").val(item.info.per_listanombre);


        $("#cmbPOLITICA").val(item.info.per_politica);
        $("#cmbPOLITICA").trigger("change");
        /*$("#txtCODPOL").val(item.info.per_politica);
        $("#txtIDPOL").val(item.info.per_politicaid);
        $("#txtPOLITICA").val(item.info.per_politicanombre);        
        $("#txtNROPAGOS").val(item.info.per_politicanropagos);
        $("#txtDIASPLAZO").val(item.info.per_politicadiasplazo);
        $("#txtPORCENTAJE").val(item.info.per_politicadesc);
        $("#txtPORCPAGOCON").val(item.info.per_politicaporpagocon);*/


        //$("#txtCODVEN").val(item.info.per_agenteid);
        //$("#txtVENDEDOR").val(item.info.per_agentenombre);

        //AUTOMATICAMENTE IGUALA EL CLIENTE CON EL REMITENTE
        $("#txtCODREM").val(item.info.per_codigo);
        $("#txtIDREM").val(item.info.per_id);
        $("#txtCIRUCREM").val(item.info.per_ciruc);
        $("#txtNOMBRESREM").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtDIRECCIONREM").val(item.info.per_direccion);
        $("#txtTELEFONOREM").val(item.info.per_telefono);


        $("#txtCODRED").val(item.info.per_codigo);
        $("#txtIDRED").val(item.info.per_id);
        $("#txtCIRUCRED").val(item.info.per_ciruc);
        $("#txtNOMBRESRED").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtDIRECCIONRED").val(item.info.per_direccion);
        $("#txtTELEFONORED").val(item.info.per_telefono);

        //AUTOMATICAMENTE IGUALA EL CLIENTE AL RETIRO
        // $("#txtCODRET").val(item.info.per_codigo);
        // $("#txtIDRET").val(item.info.per_id);
        //$("#txtCIRUCRET").val(item.info.per_ciruc);
        //   $("#txtNOMBRESRET").val(item.info.per_apellidos + " " + item.info.per_nombres);
        //$("#txtDIRECCIONRET").val(item.info.per_direccion);
        //$("#txtTELEFONORET").val(item.info.per_telefono);

        //alert(ui.item.label);
        GetAgente(item.info.per_agente);
    }
    if (idobj == "txtNOMBRESREM") {
        $("#txtCODREM").val(item.info.per_codigo);
        $("#txtCIRUCREM").val(item.info.per_ciruc);
        $("#txtNOMBRESREM").val(item.info.per_apellidos + " " + item.info.per_nombres);
        //$("#txtAPELLIDOSREM").val(item.info.per_apellidos);
        $("#txtDIRECCIONREM").val(item.info.per_direccion);
        $("#txtTELEFONOREM").val(item.info.per_telefono);
        //alert(ui.item.label);
    }
    if (idobj == "txtNOMBRESRET") {
        $("#txtCODRET").val(item.info.per_codigo);
        $("#txtCIRUCRET").val(item.info.per_ciruc);
        $("#txtNOMBRESRET").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtDIRECCIONRET").val(item.info.per_direccion);
        $("#txtTELEFONORET").val(item.info.per_telefono);
        //alert(ui.item.label);
    }
    if (idobj == "txtNOMBRESRED") {
        $("#txtCODRED").val(item.info.per_codigo);
        $("#txtCIRUCRED").val(item.info.per_ciruc);
        $("#txtNOMBRESRED").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtDIRECCIONRED").val(item.info.per_direccion);
        $("#txtTELEFONORED").val(item.info.per_telefono);
        //alert(ui.item.label);
    }
    if (idobj == "txtNOMBRESDES") {
        $("#txtCODDES").val(item.info.per_codigo);
        $("#txtCIRUCDES").val(item.info.per_ciruc);
        $("#txtNOMBRESDES").val(item.info.per_apellidos + " " + item.info.per_nombres);
        // $("#txtAPELLIDOSDES").val(item.info.per_apellidos);
        $("#txtDIRECCIONDES").val(item.info.per_direccion);
        $("#txtTELEFONODES").val(item.info.per_telefono);
        //alert(ui.item.label);

        var objdes = {};
        objdes["des_empresa"] = empresasigned["emp_codigo"];
        objdes["des_persona"] = $("#txtCODDES").val();
        SetAutocompleteByIdParams("txtDIRECCIONDES", objdes);

    }
    if (idobj == "txtCODVEH") {
        $("#txtPLACA").val(item.info.veh_placa);
        $("#txtDISCO").val(item.info.veh_disco);
        //alert(ui.item.label);
    }
    if (idobj == "txtIDPRO") {
        $("#txtIDPRO").val(item.info.pro_id);
        $("#txtCODPRO").val(item.info.pro_codigo);
        $("#txtPRODUCTO").val(item.info.pro_nombre);
        $("#txtCALCPRO").val(item.info.pro_calcula);
        $("#txtTOTPRO").val(item.info.pro_total);
        LoadProduct();
        //alert(ui.item.label);
    }
    if (idobj == "txtIDLIS") {
        $("#txtLISTA").val(item.info.lpr_nombre);
        $("#txtCODLIS").val(item.info.lpr_codigo);
    }
    if (idobj == "txtIDPOL") {
        $("#txtPOLITICA").val(item.info.pol_nombre);
        $("#txtCODPOL").val(item.info.pol_codigo);
        $("#txtPORCENTAJE").val(item.info.pol_porc_desc);
        $("#txtNROPAGOS").val(item.info.pol_nro_pagos);
        $("#txtDIASPLAZO").val(item.info.pol_dias_plazo);
        $("#txtPORCPAGOCON").val(item.info.pol_porc_pago_con);


    }
    if (idobj == "txtCIRUC_P") {
        $("#txtCODIGO_P").val(item.info.per_codigo);
        $("#txtCIRUC_P").val(item.info.per_ciruc);
        $("#txtNOMBRES_P").val(item.info.per_nombres);
        $("#txtAPELLIDOS_P").val(item.info.per_apellidos);
        $("#txtRAZON_P").val(item.info.per_razon);
        $("#txtDIRECCION_P").val(item.info.per_direccion);
        $("#txtTELEFONO_P").val(item.info.per_telefono);
        $("#txtMAIL_P").val(item.info.per_mail);
        $("#cmbTIPOID_P").val(item.info.per_tipoid);
        
        //avilible_save();
    }
    if (idobj == "txtIDTIPO_P") {
        $("#txtCODTIPO_P").val(item.info.tpa_codigo);
        $("#txtNOMBRETIPO_P").val(item.info.tpa_nombre);
        $("#txtIDTIPO_P").val(item.info.tpa_id);
        ShowCampos();
    }
    if (idobj == "txtHOJARUTA") {
        $("#txtIDHOJARUTA").val(item.info.codigo);
        GetDatosHojaRuta();
        //ShowCampos();
    }

    if (idobj == "txtDIRECCIONDES") {
        $("#txtDIRECCIONDES").val(item.info.des_destino);
        //GetDatosHojaRuta();
        //ShowCampos();
    }
    if (idobj == "txtCODVEN") {
        $("#txtCODVEN").val(item.info.per_codigo);        
        $("#txtVENDEDOR").val(item.info.per_apellidos + " " + item.info.per_nombres);
    }
}

function GetAgente(codigo) {
    var obj = {};
    obj["per_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["per_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetAgente", jsonText, "GetAgente");
}

function GetAgenteResult(data) {
    if (data != null) {
        var obj = $.parseJSON(data.d);
        $("#txtCODVEN").val(obj.per_codigo);
        $("#txtVENDEDOR").val(obj.per_razon);

    }
}


function ShowCampos() {

    var htmltable = $("#tddatos_P")[0];
    var total = 0;
    for (var r = 0; r < htmltable.rows.length; r++) {
        $(htmltable.rows[r]).hide();
    }


    var id = $("#txtIDTIPO_P").val();
    if (id == "TP001") {//EFECTIVO
        $(htmltable.rows[7]).show(); //CTA
    }
    if (id == "TP002") {//CHEQUE
        $(htmltable.rows[0]).show(); //EMISOR
        $(htmltable.rows[2]).show(); //NRO CUENTA
        $(htmltable.rows[6]).show(); //FECHA VEN
        $(htmltable.rows[7]).show(); //CTA
    }
    if (id == "TP003") {//TARJETA
        $(htmltable.rows[0]).show(); //EMISOR
        $(htmltable.rows[1]).show(); //NRO DOCUMENTO
        $(htmltable.rows[6]).show(); //FECHA VEN
        $(htmltable.rows[7]).show(); //CTA

    }
    if (id == "TP004") {//DEPOSITO
        $(htmltable.rows[3]).show(); //BANCO
        $(htmltable.rows[1]).show(); //NRO DOCUMENTO
        $(htmltable.rows[2]).show(); //NRO CUENTA
        $(htmltable.rows[6]).show(); //FECHA VEN


    }
}


function LoadProduct(item) {

    var obj = {};
    obj["producto"] = $("#txtIDPRO").val();
    obj["almacen"] = $("#txtCODALMACEN").val();
    obj["lista"] = $("#txtCODLIS").val();
    obj["ruta"] = $("#cmbRUTA").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetProduct", jsonText, 3);
}


///////////////////////////////////////////////////////////////////////////////

function SetCalculoPrecio(cell, objs) {
    var html = "<table>";
    for (i = 0; i < objs.length; i++) {
        var obj = {};
        obj["nombre"] = objs[i]["cpr_nombre"];
        obj["indice"] = objs[i]["cpr_indice"];
        obj["valor"] = objs[i]["cpr_valor"];
        obj["peso"] = objs[i]["cpr_peso"];
        obj["valordig"] = "";
        $(cell).data("cp" + objs[i]["cpr_codigo"], $.parseJSON(JSON.stringify(obj)));
        html += "<tr><td>" + objs[i]["cpr_nombre"] + "</td><td><input type='text' class='calculo' placeholder='" + objs[i]["cpr_nombre"] + "' class='calculo' id='cp" + objs[i]["cpr_codigo"] + "' /></td></tr>";
    }
    html += "</table>";
    $(cell).html(html);
}


////////////////////////////////////////////////////////////////////////


function SaveCalculoPrecioValues(cell) {
    $.each($(cell).data(), function (i, e) {
        e.valordig = $("#" + i).val();
    });
}

///////////////////////////////////////////////////////////////////////


function GetHtmlCalculoPrecioValues(cell, edit) {
    var html = "<table>";
    var has = false;
    $.each($(cell).data(), function (i, e) {
        if (edit)
            //html += "<tr><td>" + e.nombre + "</td><td><input type='text' placeholder='" + e.nombre + "' class='input-mini' id='cp" + e.valor + "' /></td></tr>";
            html += "<tr><td>" + e.nombre + "</td><td><input type='text'  placeholder='" + e.nombre + "' class='calculo' id='" + i + "' value='" + e.valordig + "' /></td></tr>";
        else
            html += "<tr><td>" + e.nombre + "</td><td>" + e.valordig + "</td></tr>";
        has = true;
    });

    html += "</table>";
    if (!has)
        html = "";
    return html;
}



///////////////////////////////////////////////////////////////////////////

function GetHtmlDataCalculoPrecio(cell) {
    var data = "";
    $.each($(cell).data(), function (i, e) {
        if ($("#" + i))
            e.valordig = $("#" + i).val();
        //data += ' data-' + i + '= "' + JSON.stringify(e) + '" ';
        data += " data-" + i + "='" + JSON.stringify(e) + "'";
    });
    return data;
}

/////////////////////////////////////////////////////////////


function LoadProductResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        $("#txtCODPRO").val(obj["pro_codigo"]);
        $("#txtPRODUCTO").val(obj["pro_nombre"]);
        $("#txtPESO").val(0);
        $("#txtCANTIDAD").val(1);
        $("#txtDESC").val(0);

        var r = $("#txtCODPRO")[0].parentNode.parentNode;
        SetCalculoPrecio(r.cells[3], obj["tproducto"]["calculaprecio"]);



        if (obj["factores"] != null) {
            var opciones = $("#cmbUMEDIDA").children("option");
            $("#cmbUMEDIDA").children('option').attr('disabled', 'disabled');
            //$("#cmbUMEDIDA").children('option').css('display', 'none');
            for (i = 0; i < obj["factores"].length; i++) {
                $("#cmbUMEDIDA option[value='" + obj["factores"][i]["fac_unidad"].toString() + "']").removeAttr("disabled");
                if (SetCheckValue(obj["factores"][i]["fac_default"])) {
                    $("#cmbUMEDIDA option[value='" + obj["factores"][i]["fac_unidad"].toString() + "']").attr('selected', 'selected');
                    $("#txtFACTOR").val(obj["factores"][i]["fac_factor"].toString());
                }
            }
            if ($("#txtCALCPRO").val() != "1")
                GetPrice();
            else {
                $("#txtPRECIO").val(CurrencyFormatted(0));
            }
            if ($("#txtTOTPRO").val() == "1")
            {
                $("#txtPRECIO").attr("disabled",true);
                $("#txtTOTAL").attr("disabled", false);
            }
            else
            {
                $("#txtPRECIO").attr("disabled", false);
                $("#txtTOTAL").attr("disabled", true);
            }
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert("El producto no tiene factores asignados", 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }

        $("#chkIVA").prop("checked", SetCheckValue(obj["pro_iva"]));
        CalculaLinea();
        //AddEditRow();
        //CalculaLinea();               
    }
}


function GetPrice() {

    var obj = {};
    obj["producto"] = $("#txtCODPRO").val();
    obj["almacen"] = $("#txtCODALMACEN").val();
    obj["lista"] = $("#txtCODLIS").val();
    obj["unidad"] = $("#cmbUMEDIDA").val();
    obj["ruta"] = $("#cmbRUTA").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetProductPrice", jsonText, 4);
}
function GetPriceResult(data) {
    $("#txtPRECIO").val(0);
    if (data != "") {
        if (data.d != "-1") {
            $("#txtPRECIO").val(CurrencyFormatted(data.d));
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert("El producto no tiene precio asignado en la lista de precios seleccionada", 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
    CalculaLinea();
}


/*********************FUNCIONES PARA CALCULO DE TOTALES **********************/


function CalculoPrecio() {
    var valorcalculo = 0;
    var r = $("#txtCODPRO")[0].parentNode.parentNode;
    $.each($(r.cells[3]).data(), function (i, e) {
        if ($("#" + i)) {
            if ($.isNumeric($("#" + i).val()))
                e.valordig = $("#" + i).val();
            else
                e.valordig = "0";
        }

        valorcalculo += ((parseFloat(e.valordig.toString()) * parseFloat(e.valor.toString())) / parseFloat(e.indice.toString()) * (parseFloat(e.peso) / 100));

    });
    $("#txtPRECIO").val(CurrencyFormatted(valorcalculo));
}

function CalculaLinea() {
    if ($("#txtCALCPRO").val() == "1") {
        CalculoPrecio();
    }
    var cant = $("#txtCANTIDAD").val();
    if (!$.isNumeric(cant))
        cant = 0;
    var prec = $("#txtPRECIO").val();
    if (!$.isNumeric(prec))
        prec = 0;
    var desc = $("#txtDESC").val();
    if (!$.isNumeric(desc))
        desc = 0;
    var subtotal = parseFloat(cant) * parseFloat(prec)
    var subtotaldesc = subtotal * (parseFloat(desc) / 100);
    var total = subtotal - subtotaldesc
    $("#txtTOTAL").val(CurrencyFormatted(total));
    CalculaTotales();
}

function CalculaTotal()
{
    var cant = $("#txtCANTIDAD").val();
    if (!$.isNumeric(cant))
        cant = 0;
    var total = $("#txtTOTAL").val();
    if (!$.isNumeric(total))
        total = 0;
    
    var precio = parseFloat(total) / parseFloat(cant);
    $("#txtPRECIO").val(CurrencyFormatted(precio));
    //$("#txtTOTAL").val(CurrencyFormatted(total));
    CalculaTotales();
}

function CalculaTotales() {
    var htmltable = $("#tdinvoice")[0];
    var subtotal0 = 0;
    var subtotalIVA = 0;
    var desc0 = 0;
    var descIVA = 0;
    var totbul = 0;

    for (var r = 0; r < htmltable.rows.length; r++) {
        var c = htmltable.rows[r].cells.length - 6; //celda cantidad
        var hijocan = $(htmltable.rows[r].cells[c]).children("input");
        var hijopre = $(htmltable.rows[r].cells[c + 1]).children("input");
        var hijodes = $(htmltable.rows[r].cells[c + 2]).children("input");
        var hijotot = $(htmltable.rows[r].cells[c + 3]).children("input");
        var hijoiva = $(htmltable.rows[r].cells[c + 4]).children("input");
        var cant = 0;
        var prec = 0;
        var desc = 0;
        var valor = 0;


        var iva = false;
        if (hijotot.length > 0) {
            cant = $(hijocan).val();
            prec = $(hijopre).val();
            desc = $(hijodes).val();
            valor = $(hijotot).val();
            iva = $(hijoiva).is(':checked') ? true : false;
        }
        else {
            cant = $(htmltable.rows[r].cells[c]).text();
            prec = $(htmltable.rows[r].cells[c + 1]).text();
            desc = $(htmltable.rows[r].cells[c + 2]).text();
            valor = $(htmltable.rows[r].cells[c + 3]).text();
            iva = ($(htmltable.rows[r].cells[c + 4]).text() == "SI") ? true : false;
        }

        subtotalIVA += ($.isNumeric(valor)) ? ((iva) ? parseFloat(valor) : 0) : 0;
        subtotal0 += ($.isNumeric(valor)) ? ((!iva) ? parseFloat(valor) : 0) : 0;

        var subtotal = parseFloat(cant) * parseFloat(prec)
        var subtotaldesc = subtotal * (parseFloat(desc) / 100);

        descIVA += ($.isNumeric(desc)) ? ((iva) ? subtotaldesc : 0) : 0;
        desc0 += ($.isNumeric(desc)) ? ((!iva) ? subtotaldesc : 0) : 0;

        //descIVA += ($.isNumeric(desc)) ? ((iva) ? parseFloat(desc) : 0) : 0;

        //desc0 += ($.isNumeric(desc)) ? ((!iva) ? parseFloat(desc) : 0) : 0;
        totbul += ($.isNumeric(cant)) ? parseFloat(cant) : 0;
    }




    var descuentoiva = ($.isNumeric($("#txtDESCUENTOIVA").val())) ? parseFloat($("#txtDESCUENTOIVA").val()) : 0;
    var descuento0 = ($.isNumeric($("#txtDESCUENTO0").val())) ? parseFloat($("#txtDESCUENTO0").val()) : 0;

    var porcentajeiva = ($.isNumeric($("#txtIVAPORCENTAJE").val())) ? parseFloat($("#txtIVAPORCENTAJE").val()) : 0;
    var valordeclarado = ($.isNumeric($("#txtVALORDECLARADO").val())) ? parseFloat($("#txtVALORDECLARADO").val()) : 0;
    var porcentajeseguro = ($.isNumeric($("#txtPORCSEGURO").val())) ? parseFloat($("#txtPORCSEGURO").val()) : 0;

    
    var seguro = (valordeclarado * (porcentajeseguro / 100)).toFixed(2);
    var transporte = ($.isNumeric($("#txtVDOMICILIO").val())) ? parseFloat($("#txtVDOMICILIO").val()) : 0;

    var origen = $("#txtorigen").val();
    if (origen == "LGC") {
        seguro = ($.isNumeric($("#txtSEGURO").val())) ? parseFloat($("#txtSEGURO").val()) : 0;
        transporte = ($.isNumeric($("#txtTRANSPORTE").val())) ? parseFloat($("#txtTRANSPORTE").val()) : 0;
    }

    var valorIVA = (subtotalIVA - descIVA - descuentoiva) * (porcentajeiva / 100);//NO SUMA SEGURO PARA COMYTRANS

    subtotal0 += transporte + desc0;
    subtotalIVA += descIVA;
    //subtotalIVA += seguro;

    //var total = (subtotal0 - desc0 - descuento0) + (subtotalIVA - descIVA - descuentoiva) + valorIVA + seguro + transporte;
    var total = (subtotal0 - desc0 - descuento0) + (subtotalIVA - descIVA - descuentoiva) + valorIVA;

    $("#txtTOTBULTOS").val(CurrencyFormatted(totbul));
    $("#txtSUBTOTAL0").val(CurrencyFormatted(subtotal0));
    $("#txtSUBTOTALIVA").val(CurrencyFormatted(subtotalIVA));
    $("#txtDESC0").val(CurrencyFormatted(desc0));
    $("#txtDESCIVA").val(CurrencyFormatted(descIVA));
    $("#txtIVA").val(CurrencyFormatted(valorIVA));
    $("#txtSEGURO").val(CurrencyFormatted(seguro));
    $("#txtTRANSPORTE").val(CurrencyFormatted(transporte));
    $("#txtTOTALCOM").val(CurrencyFormatted(total));


}

function CalculaTotalesCan() {
    var htmltable = $("#tdpago_P")[0];
    var total = 0;


    for (var r = 0; r < htmltable.rows.length; r++) {
        var c = htmltable.rows[r].cells.length - 2; //celda cantidad
        var hijoval = $(htmltable.rows[r].cells[c]).children("input");
        var valor = 0;

        if (hijoval.length > 0) {
            valor = $(hijoval).val();
        }
        else {
            valor = $(htmltable.rows[r].cells[c]).text();
        }

        total += ($.isNumeric(valor)) ? parseFloat(valor) : 0;

    }

    $("#txtTOTALCOM_P").val(CurrencyFormatted(total));
}

/*********************FUNCIONES PARA AGREGAR y EDITAR UN DETALLE**********************/

function AddEditRow() {

    var r = $("#txtCODPRO")[0].parentNode.parentNode;

    var newrow = $("<tr data-codpro='" + $("#txtCODPRO").val() + "' data-calcpro='" + $("#txtCALCPRO").val() + "' data-totpro='" + $("#txtTOTPRO").val() + "' onclick='Edit(this);'></tr>");
    newrow.append("<td class='' >" + $("#txtIDPRO").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtPRODUCTO").val() + "</td>");
    newrow.append("<td class='' style='overflow:hidden; width:300px;'>" + $("#txtOBSERVACION").val() + "</td>");

    //newrow.append("<td class='right' >" + $("#txtPESO").val() + "</td>");
    //var cellc = $("<td class='right' ></td>");
    //CopyCalculoPrecio(r.cells[3], cellc);//COPIA LOS VALORES DE LA CELDA A LA NUEVA CELDA
    //SaveCalculoPrecioValues(cellc); //GUARDA LOS VALORES INGRESADOS EN LOS DETALLES DE CALCULO PRECIO
    //$(cellc).html(GetHtmlCalculoPrecioValues(cellc, false)); //MUESTRA EN HTML DE LECTURA LOS VALORES DE CALCULO PRECIO GUARDADOS
    //newrow.append(cellc);

    var htmldata = GetHtmlDataCalculoPrecio(r.cells[3]);
    var htmlval = GetHtmlCalculoPrecioValues(r.cells[3], false)

    newrow.append("<td class='right' " + htmldata + " >" + htmlval + "</td>");

    newrow.append("<td class='' data-coduni='" + $("#cmbUMEDIDA").val() + "'>" + $("#cmbUMEDIDA option:selected").text() + "</td>");
    newrow.append("<td class='center' >" + $("#txtCANTIDAD").val() + "</td>");
    newrow.append("<td class='right' >" + $("#txtPRECIO").val() + "</td>");
    newrow.append("<td class='right' >" + $("#txtDESC").val() + "</td>");
    newrow.append("<td class='right' >" + CurrencyFormatted($("#txtTOTAL").val()) + "</td>");
    newrow.append("<td class='center' >" + ($("#chkIVA").is(':checked') ? "SI" : "NO") + "</td>");
    newrow.append("<td class='center' ><div class='removablerow' onclick='RemoveRow(this)'><span class=\"icon-trash\" ></span></div></td>");


    //var newrow = $("<tr><td>Hola</td><td>chao</td><td>jaja</td><td>si</td><td>NO</td><td>BYE</td><td>7</td><td>8</td></tr>");
    var editrow = $("#tdinvoice").find("#editrow");
    newrow.insertBefore(editrow);

    $("#txtCODPRO").val("");
    $("#txtCALCPRO").val("");
    $("#txtTOTPRO").val("");
    $("#txtIDPRO").val("");
    $("#txtPRODUCTO").val("");
    $("#txtOBSERVACION").val("");
    //$("#txtPESO").val("");
    r.cells[3].innerHTML = "";
    $(r.cells[3]).removeData();

    $("#cmbUMEDIDA").children('option').removeAttr('disabled');
    $("#txtCANTIDAD").val(0);
    $("#txtPRECIO").val(0);
    $("#txtDESC").val(0);
    $("#txtTOTAL").val(0);
    $("#chkIVA").prop("checked", false);
    $("#txtIDPRO").select();
    $("#txtPRECIO").attr("disabled", false);
    $("#txtTOTAL").attr("disabled", true);

}

function AddEditRowCan() {

    var strdata = "data-codtipo='" + $("#txtCODTIPO_P").val() + "' " +
    "data-emisor='" + $("#txtEMISOR_P").val() + "' " +
    "data-nrodocumento='" + $("#txtNRODOCUMENTO_P").val() + "' " +
    "data-nrocuenta='" + $("#txtNROCUENTA_P").val() + "' " +
    "data-banco='" + $("#cmbBANCO_P").val() + "' " +
    "data-nrocheque='" + $("#txtNROCHEQUE_P").val() + "' " +
    "data-beneficiario='" + $("#txtBENEFICIARIO_P").val() + "' " +
    "data-fecha='" + $("#txtFECHAVENCE_P").val() + "' " +
    "data-cuenta='" + $("#txtIDCUENTA_P").val() + "' " +
    "data-cuentanombre='" + $("#txtNOMBRECUENTA_P").val() + "' ";

    //var newrow = $("<tr data-codtipo='" + $("#txtCODTIPO").val() + "'  onclick='Edit(this);'></tr>");
    var newrow = $("<tr " + strdata + " onclick='EditCan(this);'></tr>");
    newrow.append("<td class='' >" + $("#txtIDTIPO_P").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtNOMBRETIPO_P").val() + "</td>");
    newrow.append("<td class='right' >" + $("#txtVALOR_P").val() + "</td>");
    newrow.append("<td class='center' ><div class='removablerow' onclick='RemoveRowCan(this)'><span class=\"icon-trash\" ></span></div></td>");

    var editrow = $("#tdpago_P").find("#editrow");
    newrow.insertBefore(editrow);

    $("#txtCODTIPO_P").val("");
    $("#txtIDTIPO_P").val("");
    $("#txtNOMBRETIPO_P").val("");
    $("#txtVALOR_P").val(0);
    $("#txtIDTIPO_P").select();

    $("#txtEMISOR_P").val("");
    $("#txtNRODOCUMENTO_P").val("");
    $("#txtNROCUENTA_P").val("");
    $("#cmbBANCO_P").val("");
    $("#txtNROCHEQUE_P").val("");
    $("#txtBENEFICIARIO_P").val("");
    $("#txtFECHAVENCE_P").val("");
    $("#txtIDCUENTA_P").val("");
    $("#txtNOMBRECUENTA_P").val("");


}




function MoveTo(control, destino) {
    var obj = $(control).detach();
    $(destino).append(obj);
}


function Edit(row) {

    var r = $("#txtCODPRO")[0].parentNode.parentNode;

    SaveCalculoPrecioValues(r.cells[3]);

    var codpro = $("#txtCODPRO").val();
    var calcpro = $("#txtCALCPRO").val();
    var totpro = $("#txtTOTPRO").val();
    var idpro = $("#txtIDPRO").val();
    var producto = $("#txtPRODUCTO").val();
    var observacion = $("#txtOBSERVACION").val();

    //var peso = $("#txtPESO").val();
    var caract = GetHtmlCalculoPrecioValues(r.cells[3], false);

    var coduni = $("#cmbUMEDIDA").val();
    var unidad = $("#cmbUMEDIDA option:selected").text();
    var cantidad = $("#txtCANTIDAD").val();
    var precio = $("#txtPRECIO").val();
    var desc = $("#txtDESC").val();
    var total = $("#txtTOTAL").val();
    var iva = ($("#chkIVA").is(':checked') ? "SI" : "NO");

    $("#txtCALCPRO").val($(row).data("calcpro").toString());
    $("#txtTOTPRO").val($(row).data("totpro").toString());
    $("#txtCODPRO").val($(row).data("codpro").toString());
    $("#txtIDPRO").val($(row.cells[0]).text()); $(row.cells[0]).text("");
    $("#txtPRODUCTO").val($(row.cells[1]).text()); $(row.cells[1]).text("");
    $("#txtOBSERVACION").val($(row.cells[2]).text()); $(row.cells[2]).text("");
    //$("#txtPESO").val(row.cells[3].innerText); 
    row.cells[3].innerHTML = GetHtmlCalculoPrecioValues(row.cells[3], true)


    $("#cmbUMEDIDA option[value='" + $(row.cells[4]).data("coduni") + "']").attr('selected', 'selected'); $(row.cells[4]).text("");
    $("#txtCANTIDAD").val($(row.cells[5]).text()); $(row.cells[5]).text("");
    $("#txtPRECIO").val($(row.cells[6]).text()); $(row.cells[6]).text("");
    $("#txtDESC").val($(row.cells[7]).text()); $(row.cells[7]).text("");
    $("#txtTOTAL").val(CurrencyFormatted($(row.cells[8]).text())); $(row.cells[8]).text("");
    $("#chkIVA").prop("checked", (($(row.cells[9]).text() == "SI") ? true : false)); $(row.cells[9]).text("");
    $("#txtIDPRO").focus();

    if ($("#txtTOTPRO").val() == "1")
    {
        $("#txtPRECIO").attr("disabled", true);
        $("#txtTOTAL").attr("disabled", false);
    }
    else
    {
        $("#txtPRECIO").attr("disabled", false);
        $("#txtTOTAL").attr("disabled", true);
    }


    MoveTo($("#txtIDPRO"), $(row.cells[0]));
    MoveTo($("#txtCODPRO"), $(row.cells[0]));
    MoveTo($("#txtCALCPRO"), $(row.cells[0]));
    MoveTo($("#txtTOTPRO"), $(row.cells[0]));
    MoveTo($("#txtPRODUCTO"), $(row.cells[1]));
    MoveTo($("#txtOBSERVACION"), $(row.cells[2]));
    //MoveTo($("#txtPESO"), $(row.cells[3]));
    MoveTo($("#cmbUMEDIDA"), $(row.cells[4]));
    MoveTo($("#txtCANTIDAD"), $(row.cells[5]));
    MoveTo($("#txtPRECIO"), $(row.cells[6]));
    MoveTo($("#txtDESC"), $(row.cells[7]));
    MoveTo($("#txtTOTAL"), $(row.cells[8]));
    MoveTo($("#chkIVA"), $(row.cells[9]));




    $(r).data("codpro", codpro);
    $(r).data("calcpro", calcpro);
    $(r).data("totpro", totpro);
    $(r.cells[0]).text(idpro);
    $(r.cells[1]).text(producto);
    $(r.cells[2]).text(observacion);
    $(r.cells[3]).text(caract);

    $(r.cells[4]).data("coduni", coduni);
    $(r.cells[4]).text(unidad);
    $(r.cells[5]).text(cantidad);
    $(r.cells[6]).text(precio);
    $(r.cells[7]).text(desc);
    $(r.cells[8]).text(CurrencyFormatted(total));
    $(r.cells[9]).text(iva);

    $(r).attr('onclick', 'Edit(this)');
    $(row).removeAttr('onclick');

    $("#txtIDPRO").select();

   

}


function EditCan(row) {

    var r = $("#txtCODTIPO_P")[0].parentNode.parentNode;
    var cod = $("#txtCODTIPO_P").val();
    var id = $("#txtIDTIPO_P").val();
    var nombre = $("#txtNOMBRETIPO_P").val();
    var valor = $("#txtVALOR_P").val();

    var emisor = $("#txtEMISOR_P").val();

    var nrodocumento = $("#txtNRODOCUMENTO_P").val();
    var nrocuenta = $("#txtNROCUENTA_P").val();
    var banco = $("#cmbBANCO_P").val();
    var nrocheque = $("#txtNROCHEQUE_P").val();
    var beneficiario = $("#txtBENEFICIARIO_P").val();
    var fecha = $("#txtFECHAVENCE_P").val();
    var cuenta = $("#txtIDCUENTA_P").val();
    var cuentanombre = $("#txtNOMBRECUENTA_P").val();


    $("#txtCODTIPO_P").val($(row).data("codtipo").toString());
    $("#txtIDTIPO_P").val($(row.cells[0]).text()); $(row.cells[0]).text("");
    $("#txtNOMBRETIPO_P").val($(row.cells[1]).text()); $(row.cells[1]).text("");
    $("#txtVALOR_P").val($(row.cells[2]).text()); $(row.cells[2]).text("");
    $("#txtIDTIPO_P").focus();

    $("#txtEMISOR_P").val($(row).data("emisor").toString());
    $("#txtNRODOCUMENTO_P").val($(row).data("nrodocumento").toString());
    $("#txtNROCUENTA_P").val($(row).data("nrocuenta").toString());
    $("#cmbBANCO_P").val($(row).data("banco").toString());
    $("#txtNROCHEQUE_P").val($(row).data("nrocheque").toString());
    $("#txtBENEFICIARIO_P").val($(row).data("beneficiario").toString());
    $("#txtFECHAVENCE_P").val($(row).data("fecha").toString());
    $("#txtIDCUENTA_P").val($(row).data("cuenta").toString());
    $("#txtNOMBRECUENTA_P").val($(row).data("cuentanombre").toString());





    MoveTo($("#txtIDTIPO_P"), $(row.cells[0]));
    MoveTo($("#txtCODTIPO_P"), $(row.cells[0]));
    MoveTo($("#txtNOMBRETIPO_P"), $(row.cells[1]));
    MoveTo($("#txtVALOR_P"), $(row.cells[2]));


    $(r).data("codtipo", cod);
    $(r).data("emisor", emisor);
    $(r).data("nrodocumento", nrodocumento);
    $(r).data("nrocuenta", nrocuenta);
    $(r).data("banco", banco);
    $(r).data("nrocheque", nrocheque);
    $(r).data("beneficiario", beneficiario);
    $(r).data("fecha", fecha);
    $(r).data("cuenta", cuenta);
    $(r).data("cuentanombre", cuentanombre);



    $(r.cells[0]).text(id);
    $(r.cells[1]).text(nombre);
    $(r.cells[2]).text(valor);


    $(r).attr('onclick', 'EditCan(this)');
    $(row).removeAttr('onclick');

    $("#txtIDTIPO_P").select();
    ShowCampos();


}

/***********************SAVE FUNCTIONS ***********************************/

function GetTotalObj() {
    var obj = {};
    obj["tot_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["tot_comprobante"] = $("#txtcodigocomp").val();

    obj["tot_impuesto"] = 2; //Impuesto Venta
    //obj["tot_servicio"] = ??

    obj["tot_porc_desc"] = ($.isNumeric($("#txtPORCENTAJE").val())) ? parseFloat($("#txtPORCENTAJE").val()) : null;
    obj["tot_dias_plazo"] = ($.isNumeric($("#txtDIASPLAZO").val())) ? parseInt($("#txtDIASPLAZO").val()) : null;
    obj["tot_nro_pagos"] = ($.isNumeric($("#txtNROPAGOS").val())) ? parseInt($("#txtNROPAGOS").val()) : null;
    //obj["tot_transportista"] = ($.isNumeric($("#txtPORCENTAJE").val()))?
    obj["tot_porc_impuesto"] = ($.isNumeric($("#txtIVAPORCENTAJE").val())) ? parseInt($("#txtIVAPORCENTAJE").val()) : null;
    //obj["tot_porc_servicio"] =
    obj["tot_porc_seguro"] = ($.isNumeric($("#txtPORCSEGURO").val())) ? parseFloat($("#txtPORCSEGURO").val()) : null;





    obj["tot_timpuesto"] = ($.isNumeric($("#txtIVA").val())) ? parseFloat($("#txtIVA").val()) : null;
    //obj["tot_tservicio"] =
    //obj["tot_tseguro"] = ($.isNumeric($("#txtSEGURO").val())) ? parseFloat($("#txtSEGURO").val()) : null;//NO SEGURO PARA COMYTRANS
    obj["tot_transporte"] = ($.isNumeric($("#txtTRANSPORTE").val())) ? parseFloat($("#txtTRANSPORTE").val()) : null;
    //obj["tot_ice"] =
    //obj["tot_financia"] =
    var subtotal = ($.isNumeric($("#txtSUBTOTALIVA").val())) ? parseFloat($("#txtSUBTOTALIVA").val()) : null;
    obj["tot_subtotal"] = subtotal - obj["tot_tseguro"];

    obj["tot_descuento1"] = ($.isNumeric($("#txtDESCIVA").val())) ? parseFloat($("#txtDESCIVA").val()) : null;
    obj["tot_descuento2"] = ($.isNumeric($("#txtDESCUENTOIVA").val())) ? parseFloat($("#txtDESCUENTOIVA").val()) : null;

    var subtotal0 = ($.isNumeric($("#txtSUBTOTAL0").val())) ? parseFloat($("#txtSUBTOTAL0").val()) : null;
    obj["tot_subtot_0"] = subtotal0 - obj["tot_transporte"];

    obj["tot_desc1_0"] = ($.isNumeric($("#txtDESC0").val())) ? parseFloat($("#txtDESC0").val()) : null;
    obj["tot_desc2_0"] = ($.isNumeric($("#txtDESCUENTO0").val())) ? parseFloat($("#txtDESCUENTO0").val()) : null;
    obj["tot_total"] = ($.isNumeric($("#txtTOTALCOM").val())) ? parseFloat($("#txtTOTALCOM").val()) : null;
    //obj["tot_anticipo"] =
    //obj["tot_paga"] =
    obj["tot_vseguro"] = ($.isNumeric($("#txtVALORDECLARADO").val())) ? parseFloat($("#txtVALORDECLARADO").val()) : null;
    obj = SetAuditoria(obj);

    return obj;
}
function GetDcalculoprecioObj(cell) {
    var detalle = new Array();
    $.each($(cell).data(), function (i, e) {
        var obj = {};
        obj["dcpr_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["dcpr_comprobante"] = $("#txtcodigocomp").val();
        //obj["dcpr_dcomdoc"] = ;
        obj["dcpr_nombre"] = e.nombre;
        obj["dcpr_indice"] = e.indice;
        obj["dcpr_valor"] = e.valor;
        obj["dcpr_peso"] = e.peso;
        obj["dcpr_indicedigitado"] = (e.valordig.replace(".", ","));
        obj = SetAuditoria(obj);
        detalle[detalle.length] = obj;
    });

    return detalle;
}

function GetDetalle() {
    var detalle = new Array();
    var htmltable = $("#tdinvoice")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var obj = GetDcomdocObj(htmltable.rows[r]);
        if (obj != null)
            detalle[r] = obj;
    }
    return detalle;
}

function GetDcomdocObj(row) {
    var obj = {};
    obj["ddoc_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["ddoc_comprobante"] = $("#txtcodigocomp").val();

    var hijocodpro = $(row.cells[0]).children("input");
    if (hijocodpro.length > 0) {
        if ($.isNumeric($("#txtCODPRO").val())) {
            obj["ddoc_producto"] = parseInt($("#txtCODPRO").val());
            obj["ddoc_observaciones"] = $("#txtOBSERVACION").val();
            obj["ddoc_peso"] = parseFloat($("#txtPESO").val());
            obj["ddco_udigitada"] = parseInt($("#cmbUMEDIDA").val());
            obj["ddoc_cantidad"] = parseFloat($("#txtCANTIDAD").val());
            obj["ddoc_precio"] = parseFloat($("#txtPRECIO").val());
            obj["ddoc_dscitem"] = parseFloat($("#txtDESC").val());
            obj["ddoc_total"] = parseFloat($("#txtTOTAL").val());
            obj["ddoc_grabaiva"] = $("#chkIVA").is(':checked') ? 1 : 0;
        }
        else
            return null;
    }
    else {
        if ($.isNumeric($(row).data("codpro").toString())) {
            obj["ddoc_producto"] = parseInt($(row).data("codpro").toString());
            obj["ddoc_observaciones"] = $(row.cells[2]).text();
            obj["ddoc_peso"] = parseFloat($(row.cells[3]).text());
            obj["ddco_udigitada"] = parseInt($(row.cells[4]).data("coduni"));
            obj["ddoc_cantidad"] = parseFloat($(row.cells[5]).text());
            obj["ddoc_precio"] = parseFloat($(row.cells[6]).text());
            obj["ddoc_dscitem"] = parseFloat($(row.cells[7]).text());
            obj["ddoc_total"] = parseFloat($(row.cells[8]).text());
            obj["ddoc_grabaiva"] = (($(row.cells[9]).text() == "SI") ? 1 : 0);
        }
        else
            return null;
    }
    obj["detallecalculo"] = GetDcalculoprecioObj(row.cells[3]);
    obj = SetAuditoria(obj);
    return obj;
}




function GetCcomdocObj() {
    var obj = {};
    obj["cdoc_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cdoc_comprobante"] = $("#txtcodigocomp").val();

    if ($("#txtNROAUT").length > 0)
        obj["cdoc_acl_nroautoriza"] = $("#txtNROAUT").val();//GUARDA AUTORIZACION PARA FACTURAS

    //obj["cdoc_politica"] = parseInt($("#txtCODPOL").val());
    obj["cdoc_politica"] = parseInt($("#cmbPOLITICA").val());
    obj["cdoc_listaprecio"] = parseInt($("#txtCODLIS").val());
    obj["cdoc_nombre"] = $("#txtNOMBRES").val();
    obj["cdoc_direccion"] = $("#txtDIRECCION").val();
    obj["cdoc_telefono"] = $("#txtTELEFONO").val();
    obj["cdoc_ced_ruc"] = $("#txtRUC").val();
    obj["detalle"] = GetDetalle();
    obj = SetAuditoria(obj);
    return obj;
}
function GetCcomenvObj() {
    //var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHARET").val()); // $("#txtFECHA_P").datepicker("getDate");
    var obj = {};
    obj["cenv_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cenv_comprobante"] = $("#txtcodigocomp").val();

    obj["cenv_empresa_rem"] = parseInt(empresasigned["emp_codigo"]);
    obj["cenv_remitente"] = parseInt($("#txtCODREM").val());
    obj["cenv_nombres_rem"] = $("#txtNOMBRESREM").val();
    obj["cenv_direccion_rem"] = $("#txtDIRECCIONREM").val();
    obj["cenv_telefono_rem"] = $("#txtTELEFONOREM").val();
    obj["cenv_ciruc_rem"] = $("#txtCIRUCREM").val();

    obj["cenv_nombres_cho"] = $("#txtCHOFER").val();//IMPRESION DEL NOMBRE DE CHOFER
    obj["cenv_nombres_soc"] = $("#txtSOCIO").val();//IMPRESION DEL NOMBRE DE SOCIO

    obj["cenv_empresa_des"] = parseInt(empresasigned["emp_codigo"]);
    obj["cenv_destinatario"] = parseInt($("#txtCODDES").val());
    obj["cenv_nombres_des"] = $("#txtNOMBRESDES").val();
    obj["cenv_direccion_des"] = $("#txtDIRECCIONDES").val();
    obj["cenv_telefono_des"] = $("#txtTELEFONODES").val();
    obj["cenv_ciruc_des"] = $("#txtCIRUCDES").val();

    if ($("#txtCODSOC").val() != "")
        obj["cenv_socio"] = parseInt($("#txtCODSOC").val());
    if ($("#txtCODCHO").val() != "")
        obj["cenv_chofer"] = parseInt($("#txtCODCHO").val());
    if ($("#txtCODVEH").val() != "")
        obj["cenv_vehiculo"] = parseInt($("#txtCODVEH").val());

    obj["cenv_placa"] = $("#txtPLACAVEH").val();
    obj["cenv_disco"] = $("#txtDISCOVEH").val();





    obj["cenv_ruta"] = $("#cmbRUTA").val();
    obj["cenv_estado_ruta"] = 0; //0 estado de ruta no asignado, 1 asignado pero puede cambiar, 2 estado asignado definitivo
    obj["cenv_observacion"] = $("#txtENTREGADES").val();
    if (parseInt($("#txtNUMEROPRO").val()) != 0) {
        obj["cenv_guia1"] = $("#txtALMACENPRO").val();
        obj["cenv_guia2"] = $("#txtPVENTAPRO").val();
        obj["cenv_guia3"] = $("#txtNUMEROPRO").val();
    }
    obj = SetAuditoria(obj);
    return obj;
}

function GetPlanillaCompObj() {
    var obj = {};
    //obj["pco_comprobante_fac"] =
    obj["pco_comprobante_pla"] = parseInt($("#txtcodigocompref").val());
    obj["pco_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj = SetAuditoria(obj);
    return obj;
}

function GetComprobanteObj() {
    //var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHACOMP").val()); // $("#txtFECHA_P").datepicker("getDate");

    var now = new Date();
    //var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHA_P").val());

    var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHACOMP").val());
    //obj["fecha"] = $("#txtFECHA_P").datepicker("getDate") ;            

    var almacen = parseInt($("#txtCODALMACEN").val());
    var pventa = parseInt($("#txtCODPVENTA").val());

    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = $("#txtcodigocomp").val();
    obj["com_tipodoc"] = parseInt($("#txttipodoc").val());

    obj["com_ctipocom"] = parseInt($("#txtCTIPOCOM").val());  //3 REC
    obj["com_numero"] = $("#txtNUMERO").val();
    obj["com_fecha"] = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate(), now.getHours(), now.getMinutes(), now.getSeconds(), now.getMilliseconds());
    obj["com_doctran"] = $("#numerocomp").html();
    obj["com_nocontable"] = parseInt($("#txtnocontable").val());
    obj["com_periodo"] = currentDate.getFullYear();
    obj["com_almacen"] = almacen;
    obj["com_pventa"] = pventa;
    obj["com_concepto"] = $("#txtOBSERVACIONES").val();
    obj["com_codclipro"] = parseInt($("#txtCODPER").val());
    obj["com_agente"] = parseInt($("#txtCODVEN").val());
    obj["com_token"] = $("#txtTOKEN").val();//NUEVO CONTROL
    obj["ccomdoc"] = GetCcomdocObj();
    obj["ccomenv"] = GetCcomenvObj();
    obj["total"] = GetTotalObj();
    if ($("#txtorigen").val() == "LGC")
        obj["planillacomp"] = GetPlanillaCompObj();
    obj = SetAuditoria(obj);
    return obj;
    //var jsonText = JSON.stringify({ objeto: obj });
    //return jsonText;
}

function GetRutaxFacturaObj() {
    var obj = {};
    //if ($("#cmbHOJARUTA").length > 0) {
    if ($("#txtIDHOJARUTA").val() != "" && $("#txtIDHOJARUTA").val() != null && $("#txtIDHOJARUTA").val() != undefined) {
        obj["rfac_empresa"] = parseInt(empresasigned["emp_codigo"]);
        //obj["rfac_comprobanteruta"] = $("#cmbHOJARUTA").val();
        obj["rfac_comprobanteruta"] = $("#txtIDHOJARUTA").val();
        obj["rfac_comprobantefac"] = 0;
        obj["rfac_estado"] = 1;
    }
    obj = SetAuditoria(obj);
    return obj;
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


    var detalle = GetDetalle();
    if (detalle.length == 0) {
        retorno = false;
        mensajehtml += "Es necesario ingresar al menos un detalle al comprobante<br>";
    }

    /*var htmltable = $("#tdinvoice")[0];
    if (htmltable.rows.length < 3) {
     
    }*/

    if ($("#txtnocontable").val() == "1")
        recibocreated = true;


    if (!retorno) {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert(mensajehtml, 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
    return retorno;
}



function Recibo() {
    var porpagocon = parseFloat($("#txtPORCPAGOCON").val());
    if (porpagocon > 0) {
        var valorpago = parseFloat($("#txtTOTALCOM").val()) * (porpagocon / 100);
        CallRecibo(empresasigned["emp_codigo"], usuariosigned["usr_id"], valorpago, $("#cmbPOLITICA").val());
    }
    else {
        SetRecibo(null);
    }
}

function SetRecibo(obj) {
    recibocreated = true;
    objdrecibo = obj;
    SaveObj();
}





function SaveObj() {
    if (!saving) {
        if (ValidateForm()) {
            if (!recibocreated) {
                Recibo();
            }
            else {
                var obj = {};
                obj["comprobante"] = GetComprobanteObj();
                obj["drecibo"] = objdrecibo;
                obj["rutaxfactura"] = GetRutaxFacturaObj();
                var jsonText = JSON.stringify({ objeto: obj });
                //var jsonText = GetComprobanteObj(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
                //jConfirm('¿Está seguro que desea guardar el comprobante? \n Luego no se podra modificar', 'Guardar', function (r) {
                //    if (r)
                $("#save").attr("disabled", true);
                saving = true;
                CallServer(formname + "/SaveObject", jsonText, 5);

                //});
                //  CallServer(formname + "/SaveObject", jsonText, 5);
            }
        }
    }
}

function CloseObj() {
    var obj = {};
    obj["comprobante"] = GetComprobanteObj();
    obj["drecibo"] = objdrecibo;
    obj["rutaxfactura"] = GetRutaxFacturaObj();
    var jsonText = JSON.stringify({ objeto: obj });

    CallServer(formname + "/CloseObject", jsonText, 8);
}

function CloseObjResult(data) {
    if (data.d != "-1") {
        $("#txtcodigocomp").val(data.d);
        jQuery.alerts.dialogClass = 'alert-success';
        jAlert('Comprobante mayorizado exitosamente...', 'Éxito', function () {
            jQuery.alerts.dialogClass = null; // reset to default
            window.location = formname + "?codigocomp=" + data.d;
        });


    }
    else {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert('Se ha producido un error al mayorizar el comprobante...', 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
}

function ContObj() {
    window.location = "wfDiario.aspx?codigocomp=" + $("#txtcodigocomp").val() + "&tipodoc=" + $("#txttipodoc").val();
}

function PrintObj() {

    var opciones = "toolbar=no, scrollbars=no, resizable=yes, top=50, left=100, width=800, height=600";
    var printversion = $("#txtprintversion").val();

    if (printversion == "2")//FORMATO HTML
        window.open("./print/" + $("#txtprinthtml").val() + "?codigo=" + $("#txtcodigocomp").val() + "&usuario=" + usuariosigned["usr_id"] + "&empresa=" + empresasigned["emp_codigo"] + "&tipodoc=" + $("#txttipodoc").val(), "Imp", opciones);
    else if (printversion == "3")//FORMATO HTML
        window.open("./print/" + $("#txtprinthtml").val() + "&codigo=" + $("#txtcodigocomp").val() + "&usuario=" + usuariosigned["usr_id"] + "&empresa=" + empresasigned["emp_codigo"] + "&tipodoc=" + $("#txttipodoc").val(), "Imp", opciones);
    else //FORMATO PDF
        window.open("wfComprobantePrint.aspx?codigo=" + $("#txtcodigocomp").val() + "&usuario=" + usuariosigned["usr_id"] + "&empresa=" + empresasigned["emp_codigo"] + "&tipodoc=" + $("#txttipodoc").val() + "&formato=" + $("#txtprintformat").val(), "Imp", opciones);

    //jsWebClientPrint.print("useDefaultPrinter=no&printerName=\\\\Gtec-pc\\EPSON L200 Series&filetype=PDF");



    //window.open("wfComprobantePrint.aspx?codigo=" + $("#txtcodigocomp").val(), "Imp", opciones);

    //window.open("./Templates/FAC.aspx?codigo=" + $("#txtcodigocomp").val(), "Imp", opciones);

    //window.location = "wfComprobantePrint.aspx?codigocomp=" +$("#txtcodigocomp").val();
}

function SaveObjResult(data) {
    $("#save").attr("disabled", false);
    saving = false;
    if (data != "") {
        //if (data.d != "ERROR") {
        if (data.d != "-1") {
            jQuery.alerts.dialogClass = 'alert-success';
            jAlert('Comprobante guardado exitosamente...', 'Éxito', function () {
                $("#txtcodigocomp").val(data.d);
                PrintObj();
                window.location = window.location;
                //window.location.reload();
                /*jQuery.alerts.dialogClass = null; // reset to default

                jConfirm('¿Desea imprimir el comprobante este momento?', 'Pregunta', function (r) {
                    if (r) {
                        $("#txtcodigocomp").val(data.d);
                        PrintObj();
                    }
                    location.reload();
                });*/
                //location.reload();
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
/****************************FUNCIONES MANEJO DE TECLAS*******************************/

function RowUp() {
    var r = $("#txtCODPRO")[0].parentNode.parentNode;
    if ($(r).prev().length > 0)
        Edit($(r).prev()[0]);
}

function RowDown() {
    var r = $("#txtCODPRO")[0].parentNode.parentNode;
    if ($(r).next().length > 0)
        Edit($(r).next()[0]);
    else {
        if ($("#txtCODPRO").val() != "")
            AddEditRow();
    }
}

function RowUpCan() {
    var r = $("#txtCODTIPO_P")[0].parentNode.parentNode;
    if ($(r).prev().length > 0)
        EditCan($(r).prev()[0]);
}

function RowDownCan() {
    var r = $("#txtCODTIPO_P")[0].parentNode.parentNode;
    if ($(r).next().length > 0)
        EditCan($(r).next()[0]);
    else {
        if ($("#txtCODTIPO_P").val() != "")
            AddEditRowCan();
    }
}

function IsOpen(id) {
    return $("#" + id).autocomplete('widget').is(':visible');
}



$(window).keydown(function (event) {
    var id = document.activeElement.id;
    var code = event.keyCode;

    //var char = event.char;
    var char = String.fromCharCode(event.which);
    //$("#mensaje").html(id + " " + code + " " + char);

    if (id == "txtIDPRO" || id == "txtOBSERVACION" || id == "txtPRECIO" || id == "txtCANTIDAD" || id == "txtDESC" || id == "txtPESO" || id == "txtTOTAL" || $("#" + id).hasClass('calculo')) {

        if (id == "txtIDPRO" && IsOpen(id))
            code = -1;

        //        if (code == 37) {// Flecha Izquierda
        //            CellLeft(this);
        //        }
        if (code == 38) { //Flecha Arriba
            RowUp();
        }
        //        if (code == 39) { //Flecha Derecha
        //            CellRight(this);
        //        }
        if (code == 40) { //ENTER O Flecha ABAJO            
            RowDown();
        }
        if (code == 13) {
            if (id != "txtIDPRO")
                RowDown();
        }
    }
    if (id == "txtIDTIPO_P" || id == "txtVALOR_P") {

        if (id == "txtIDTIPO_P" && IsOpen(id))
            code = -1;

        if (code == 38) { //Flecha Arriba
            RowUpCan();
        }
        if (code == 40) { //ENTER O Flecha ABAJO            
            RowDownCan();
        }
        if (code == 13) {
            if (id != "txtIDTIPO_P")
                RowDownCan();
        }
    }

    if (!IsNumeric(id, event))
        event.preventDefault();

    if (code == 13) //ENTER
        event.preventDefault();

});

$(window).keyup(function (event) {
    var id = document.activeElement.id;
    var code = event.keyCode
    MustCalculate(id);

});


function IsNumeric(id, event) {
    var num = $("#" + id).data("numeric");
    if (num == "si") {
        var keyCode = ('which' in event) ? event.which : event.keyCode;
        var controlKeys = [8, 9, 13, 35, 36, 37, 39, 110, 190];
        var isControlKey = controlKeys.join(",").match(new RegExp(keyCode));
        if (!isControlKey) {
            isNumeric = (keyCode >= 48 /* KeyboardEvent.DOM_VK_0 */ && keyCode <= 57 /* KeyboardEvent.DOM_VK_9 */) ||
                        (keyCode >= 96 /* KeyboardEvent.DOM_VK_NUMPAD0 */ && keyCode <= 105 /* KeyboardEvent.DOM_VK_NUMPAD9 */);
            //modifiers = (event.altKey || event.ctrlKey || event.shiftKey);
            return isNumeric;
        }
    }
    return true;
}

/*function IsNumeric(id, code, char) {
    var num = $("#" + id).data("numeric");
    if (num == "si") {
        var controlKeys = [8, 9, 13, 35, 36, 37, 39];
        var isControlKey = controlKeys.join(",").match(new RegExp(code));
        if (!isControlKey) {
            var valor = $("#" + id).val();
            if (char == ".")
                char += "0";
            valor += char;
            if (!$.isNumeric(valor))
                return false;
        }


    }
    return true;
} */


function MustCalculate(id) {
    switch (id) {
        case "txtPRECIO":
        case "txtCANTIDAD":
        case "txtDESC":     
            CalculaLinea();
            break;
        case "txtTOTAL":
            CalculaTotal();
            break;
        case "txtVALORDECLARADO":
        case "txtPORCSEGURO":
        case "txtVDOMICILIO":
        case "txtDESCUENTO0":
        case "txtDESCUENTOIVA":
        case "txtPORCENTAJE":
            CalculaTotales();
            break;
        case "txtVALOR_P":
            CalculaTotalesCan();
            break;

    }
    if ($("#" + id).hasClass('calculo'))
        CalculaLinea();

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
        $("#txtMAIL").val(obj.per_mail);

        $("#txtCODLIS").val(obj.per_listaprecio);
        $("#txtIDLIS").val(obj.per_listaid);
        $("#txtLISTA").val(obj.per_listanombre);

        $("#cmbPOLITICA").val(obj.per_politica);
        $("#cmbPOLITICA").trigger("change");
        /*$("#txtCODPOL").val(obj.per_politica);
        $("#txtIDPOL").val(obj.per_politicaid);
        $("#txtPOLITICA").val(obj.per_politicanombre);
        $("#txtNROPAGOS").val(obj.per_politicanropagos);
        $("#txtDIASPLAZO").val(obj.per_politicadiasplazo);
        $("#txtPORCENTAJE").val(obj.per_politicadesc);
        $("#txtPORCPAGOCON").val(obj.per_politicaporpagocon);*/


        $("#txtPORCENTAJE").val(obj.per_politicadesc);
        $("#txtCODVEN").val(obj.per_agenteid);
        $("#txtVENDEDOR").val(obj.per_agentenombre);

        //AUTOMATICAMENTE IGUALA EL REMITENTE CON EL CLIENTE
        $("#txtIDREM").val(obj.per_id);
        $("#txtCIRUCREM").val(obj.per_ciruc);
        $("#txtCODREM").val(obj.per_codigo);
        $("#txtNOMBRESREM").val(obj.per_apellidos + " " + obj.per_nombres);
        $("#txtDIRECCIONREM").val(obj.per_direccion);
        $("#txtTELEFONOREM").val(obj.per_telefono);

        // PARA RETIROS
        $("#txtIDRET").val(obj.per_id);
        $("#txtCODRET").val(obj.per_codigo);
        $("#txtCIRUCRET").val(obj.per_ciruc);
        $("#txtNOMBRESRET").val(obj.per_apellidos + " " + obj.per_nombres);
        $("#txtDIRECCIONRET").val(obj.per_direccion);
        $("#txtTELEFONORET").val(obj.per_telefono);

        // FIN DE RETIROS

        $("#txtIDRED").val(obj.per_id);
        $("#txtCODRED").val(obj.per_codigo);
        $("#txtCIRUCRED").val(obj.per_ciruc);
        $("#txtNOMBRESRED").val(obj.per_apellidos + " " + obj.per_nombres);
        $("#txtDIRECCIONRED").val(obj.per_direccion);
        $("#txtTELEFONORED").val(obj.per_telefono);




    }
    if (id == "btncallrem") {
        $("#txtIDREM").val(obj.per_id);
        $("#txtCODREM").val(obj.per_codigo);
        $("#txtCIRUCREM").val(obj.per_ciruc);
        $("#txtNOMBRESREM").val(obj.per_apellidos + " " + obj.per_nombres);
        $("#txtDIRECCIONREM").val(obj.per_direccion);
        $("#txtTELEFONOREM").val(obj.per_telefono);

    }

    if (id == "btncallret") {
        $("#txtIDRET").val(obj.per_id);
        $("#txtCODRET").val(obj.per_codigo);
        $("#txtCIRUCRET").val(obj.per_ciruc);
        $("#txtNOMBRESRET").val(obj.per_apellidos + " " + obj.per_nombres);
        $("#txtDIRECCIONRET").val(obj.per_direccion);
        $("#txtTELEFONORET").val(obj.per_telefono);

    }

    if (id == "btncalldes") {
        $("#txtIDDES").val(obj.per_id);
        $("#txtCODDES").val(obj.per_codigo);
        $("#txtCIRUCDES").val(obj.per_ciruc);
        $("#txtNOMBRESDES").val(obj.per_apellidos + " " + obj.per_nombres);
        $("#txtDIRECCIONDES").val(obj.per_direccion);
        $("#txtTELEFONODES").val(obj.per_telefono);

        var objdes = {};
        objdes["des_empresa"] = empresasigned["emp_codigo"];
        objdes["des_persona"] = $("#txtCODDES").val();
        SetAutocompleteByIdParams("txtDIRECCIONDES", objdes);
    }

    if (id == "btncalldepa") {
        $("#txtIDRED").val(obj.per_id);
        $("#txtCODRED").val(obj.per_codigo);
        $("#txtCIRUCRED").val(obj.per_ciruc);
        $("#txtNOMBRESRED").val(obj.per_apellidos + " " + obj.per_nombres);
        $("#txtDIRECCIONRED").val(obj.per_direccion);
        $("#txtTELEFONORED").val(obj.per_telefono);

    }
}


function asigRem() {
    $("#txtIDREM").val($("#txtCODCLIPRO").val());
    $("#txtCODREM").val($("#txtCODPER").val());
    $("#txtCIRUCREM").val($("#txtCIRUC").val());
    $("#txtNOMBRESREM").val($("#txtNOMBRES").val());
    $("#txtDIRECCIONREM").val($("#txtDIRECCION").val());
    $("#txtTELEFONOREM").val($("#txtTELEFONO").val());


}

function asigRet() {
    $("#txtIDRET").val($("#txtCODCLIPRO").val());
    $("#txtCODRET").val($("#txtCODPER").val());
    $("#txtCIRUCRET").val($("#txtCIRUC").val());
    $("#txtNOMBRESRET").val($("#txtNOMBRES").val());
    $("#txtDIRECCIONRET").val($("#txtDIRECCION").val());
    $("#txtTELEFONORET").val($("#txtTELEFONO").val());
}

function asigDes() {
    $("#txtIDDES").val($("#txtCODCLIPRO").val());
    $("#txtCODDES").val($("#txtCODPER").val());
    $("#txtCIRUCDES").val($("#txtCIRUC").val());
    $("#txtNOMBRESDES").val($("#txtNOMBRES").val());
    $("#txtDIRECCIONDES").val($("#txtDIRECCION").val());
    $("#txtTELEFONODES").val($("#txtTELEFONO").val());

    var objdes = {};
    objdes["des_empresa"] = empresasigned["emp_codigo"];
    objdes["des_persona"] = $("#txtCODDES").val();
    SetAutocompleteByIdParams("txtDIRECCIONDES", objdes);
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
        $("#txtMAIL").val("");

        $("#txtCODLIS").val("");
        $("#txtIDLIS").val("");
        $("#txtLISTA").val("");

        $("#cmbPOLITICA").val("");
        $("#cmbPOLITICA").trigger("change");
        /*$("#txtCODPOL").val("");
        $("#txtIDPOL").val("");
        $("#txtPOLITICA").val("");*/

        $("#txtPORCENTAJE").val("");
        $("#txtCODVEN").val("");
        $("#txtVENDEDOR").val("");
    }
    if (id == "btncleanrem") {
        $("#txtIDREM").val("");
        $("#txtCODREM").val("");
        $("#txtCIRUCREM").val("");
        $("#txtNOMBRESREM").val("");
        $("#txtDIRECCIONREM").val("");
        $("#txtTELEFONOREM").val("");

    }

    if (id == "btncleanret") {
        $("#txtIDRET").val("");
        $("#txtCODRET").val("");
        $("#txtCIRUCRET").val("");
        $("#txtNOMBRESRET").val("");
        $("#txtDIRECCIONRET").val("");
        $("#txtTELEFONORET").val("");

    }
    if (id == "btncleanred") {
        $("#txtIDRED").val("");
        $("#txtCODRED").val("");
        $("#txtCIRUCRED").val("");
        $("#txtNOMBRESRED").val("");
        $("#txtDIRECCIONRED").val("");
        $("#txtTELEFONORED").val("");
        $("#txtOBSERVACIONRED").val("");

    }
    if (id == "btncleandes") {
        $("#txtIDDES").val("");
        $("#txtCODDES").val("");
        $("#txtCIRUCDES").val("");
        $("#txtNOMBRESDES").val("");
        $("#txtDIRECCIONDES").val("");
        $("#txtTELEFONODES").val("");

    }
}
function Despachar() {

    CallDespachar($("#txtcodigocomp").val());

}


function AddDestino()
{
    if ($("#txtCODDES").val()!="" && $("#txtCODDES").val()!=null)
    {
        var destino = $("#txtDIRECCIONDES").val();
        if (destino!="")
        {
            var obj = {};
            obj["des_empresa"] = empresasigned["emp_codigo"];
            obj["des_persona"] = $("#txtCODDES").val()
            obj["des_destino"] = destino
            obj = SetAuditoria(obj);
            var jsonText = JSON.stringify({ objeto: obj });
            CallServer(formname + "/AddDestino", jsonText, 16);
        }
                
    }
    else
    {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert('No se puede agregar destinos si el destinatario no esta registrado en la base de datos...', 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
}


function DelDestino() {
    if ($("#txtCODDES").val() != "" && $("#txtCODDES").val() != null) {
        var destino = $("#txtDIRECCIONDES").val();
        if (destino != "") {
            var obj = {};
            obj["des_empresa"] = empresasigned["emp_codigo"];
            obj["des_persona"] = $("#txtCODDES").val()
            obj["des_destino"] = destino
            obj = SetAuditoria(obj);
            var jsonText = JSON.stringify({ objeto: obj });
            CallServer(formname + "/DelDestino", jsonText, 16);
        }

    }
    else {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert('No se puede agregar destinos si el destinatario no esta registrado en la base de datos...', 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
}


function AddDestinoResult(data) {
    if (data.d != "OK") {

        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert('Se ha producido un error al gestionar el destino...', 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
}