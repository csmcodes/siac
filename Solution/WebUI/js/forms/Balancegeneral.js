﻿//Archivo:          CalculoPrecio.js
//Descripción:      Contiene las funciones comunes para la interfaz de calculo precios
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Diciembre 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfBalanceGeneral.aspx";
var menuoption = "BalanceGeneral";

function StopPropagation(event) {
    if (!event) var event = window.event;
    event.cancelBubble = true;
    if (event.stopPropagation) event.stopPropagation();
}

//Codigo ejecutado cuando el document esta listo
$(document).ready(function () {    
    $('body').css('background', 'transparent');
    LoadCabecera();
});


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
                LoadDetalleDataResult(data);
            if (retorno == 3)
                CallDlistaPrecioResult(data);
            if (retorno == 4)
                SaveObjResult(data);
            if (retorno == 5)
                RemoveObjectsResult(data);
            if (retorno == 6)
                CalTotalResult(data);
            if (retorno == 7)
                GetPieResult(data);
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
    $("#print").on("click", PrintObj);    
    var jsonText = JSON.stringify({});
    CallServer(formname + "/GetCabecera", jsonText, 0);
}

function LoadCabeceraResult(data) {
    if (data != "") {
        $('#comcabeceracontent').html(data.d);
        
        if (parseInt($("#txtdebcre").val()) == 1) {
             $($("h1")[0]).text('Balance de Cuentas');
             $($("h5")[0]).text('Balance de Cuentas');
         }
         if (parseInt($("#txtdebcre").val()) == 2) {
             $($("h1")[0]).text('Estado de Resultados');
             $($("h5")[0]).text('Estado de Resultados');
         }
        GetPie();
        LoadDetalle();
    }
    $("#barra ul").append("<li><div class=\"btn\" id=\"printsaldos\"><i class=\"iconfa-save\"></i> &nbsp; Imprimir Saldos </div></li>");
    $("#printsaldos").on("click", PrintSaldosObj);
    $('.fecha').datepicker({ dateFormat: 'dd/mm/yy' }).val();
    SetForm(); //Depende de cada js.
}


function GetPie() {
    var jsonText = JSON.stringify({});
    CallServer(formname + "/GetPie", jsonText, 7);
}

function GetPieResult(data) {
    if (data != "") {
        $('#compiecontent').html(data.d);      
    }   
}

function LoadDetalle() {
    $('#comdetallecontent').empty();
    var jsonText = JSON.stringify({});
    CallServer(formname + "/GetDetalle", jsonText, 1);
}

function LoadDetalleResult(data) {
    if (data != "") {
        $('#comdetallecontent').html(data.d);
        //CalTotal();
        LoadDetalleData();
       
    }
    SetFormDetalle();
    //EditableRow("detalletabla");
}


function CalTotal() {
    var obj = GetCabeceraData();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/CalTotales", jsonText, 6);
}

function CalTotalResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        $("#txtTOTALCOM").val(obj);  
    }
}

function GetCabeceraData() {
    var obj = {};
    obj["cuenta"] = GeBancoData();
    obj["dcontable"] = GetSalbanData();
    obj["tipo"] = $("#cmbTIPO_C").val();
    obj["todas"] = $("#chkTODAS_S").is(":checked");
    obj["saldo"] = $("#chkSALDO_S").is(":checked");
    obj["empresa"] = parseInt(empresasigned["emp_codigo"]);
    return obj;
}

function GeBancoData() {
    var obj = {};
    obj["cue_movimiento"] =  GetCheckValue($("#chkMOVIMIENTO_S"));
    obj["cue_modulo"] = parseInt($("#cmbMODULO_S").val());
    obj["cue_nivel"] = parseInt($("#cmbNIVEL_S").val());
    return obj;
}

function GetSalbanData() {
    var obj = {};
    obj["dco_centro"] = $("#cmbCENTRO_S").val();
    //obj["dco_almacen"] = parseInt($("#cmbALMACEN_S").val());
    obj["dco_almacen"] = $("#cmbALMACEN_S").val();
    obj["dco_fecha_vence"] = $("#cmbFECHA_C").val();
    return obj;
}
function LoadDetalleData() {
    var obj = GetCabeceraData();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDetalleData", jsonText, 2);
}

function LoadDetalleDataResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        $("#txtTOTALCOM").val(BalanceFormat(obj[0]));
        $('#tddatos').append(obj[1]);

        if ($("#cmbTIPO_C").val() == "m") {
            $('#tddatos tr').each(function () {
                $('th:eq(' + 2 + ')', this).hide();
                $('td:eq(' + 2 + ')', this).hide();
            });
        }
        else if ($("#cmbTIPO_C").val() == "a") {

            $('#tddatos tr').each(function () {
                $('th:eq(' + 2 + ')', this).show();
                $('td:eq(' + 2 + ')', this).show();
            });
        }

        


    }
   
}


function SetForm() {
    $("#cmbALMACEN_S").on("change", LoadDetalle);
    $("#cmbCENTRO_S").on("change", LoadDetalle);
    $("#cmbFECHA_C").on("change", LoadDetalle);
    $("#cmbNIVEL_S").on("change", LoadDetalle);
    $("#chkMOVIMIENTO_S").on("change", LoadDetalle);
    $("#chkTODAS_S").on("change", LoadDetalle);
    $("#chkSALDO_S").on("change", LoadDetalle);
    $("#cmbMODULO_S").on("change", LoadDetalle);
    $("#cmbTIPO_C").on("change", LoadDetalle);   
}

function SetFormDetalle() {
    //Tratamiento para los elemntos del detalle    
    $("#adddet").on("click", CallDlistaPrecio);
    $("#deldet").on("click", QuitSelected);
    $("#alldet").on("click", { tabla: "tddatos" }, SelectAllRows);
    $("#nonedet").on("click", { tabla: "tddatos" }, CleanSelectedRows);
    //$("#comdetallecontent").on("scroll", scroll);
}

function scroll() {
    if ($("#comdetallecontent")[0].scrollHeight - $("#comdetallecontent").scrollTop() <= $("#comdetallecontent").outerHeight()) {
        LoadDetalleData();
    }
}


//function PrintObj() {
//    var almacen = $("#cmbALMACEN_S").val();
//    var centro = $("#cmbCENTRO_S").val();
//    var fechac = $("#cmbFECHA_C").val();
//    var nivel = $("#cmbNIVEL_S").val();
//    var modulo = $("#cmbMODULO_S").val();
//    var movimiento = $("#chkMOVIMIENTO_S").val();
//    var debcre = $("#txtdebcre").val();
//    var empresa = empresasigned["emp_codigo"];


//    window.location = "wfBalanceGeneralPrint.aspx?codalmacen=" + almacen + "&fechacort=" + fechac + "&nivel=" + nivel + "&movimiento=" + movimiento + "&modulo=" + modulo + "&debcre=" + debcre + "&empresa=" + empresa; 
//    //window.location = "wfBalanceGeneralPrint.aspx?codalmacen=" + parseInt($("#cmbALMACEN_S").val()) + "&fechacort=" + $("#cmbFECHA_C").val() + "&nivel=" + parseInt($("#cmbNIVEL_S").val()) + "&movimiento=" + parseInt($("#chkMOVIMIENTO_S").val()) + "&modulo=" + parseInt($("#cmbMODULO_S").val()) + "&debcre=" + parseInt($("#txtdebcre").val()); 
//}

function PrintObj() {

    var almacen = $("#cmbALMACEN_S").val();
    var centro = $("#cmbCENTRO_S").val();
    var fechac = $("#cmbFECHA_C").val();
    var nivel = $("#cmbNIVEL_S").val();
    var modulo = $("#cmbMODULO_S").val();
    var movimiento = $("#chkMOVIMIENTO_S").val();
    var debcre = $("#txtdebcre").val();
    var empresa = empresasigned["emp_codigo"];
    var tipo = $("#cmbTIPO_C").val();
    var todas = $("#chkTODAS_S").is(":checked");
    var saldo = $("#chkSALDO_S").is(":checked");

    var url = "./reports/wfReportPrint.aspx?report=BAL&empresa=" + parseInt(empresasigned["emp_codigo"]) + "&parameter1=" + fechac + "&parameter2=" + almacen + "&parameter3=" + debcre + "&parameter4=" + empresa + "&parameter5=" + tipo + "&parameter6=" + todas + "&parameter7=0&parameter8="+saldo;
    var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=yes,scrollbars=yes";
    window.open(url, "Reporte", feautures);

}

function PrintSaldosObj() {

    var almacen = $("#cmbALMACEN_S").val();
    var centro = $("#cmbCENTRO_S").val();
    var fechac = $("#cmbFECHA_C").val();
    var nivel = $("#cmbNIVEL_S").val();
    var modulo = $("#cmbMODULO_S").val();
    var movimiento = $("#chkMOVIMIENTO_S").val();
    var debcre = $("#txtdebcre").val();
    var empresa = empresasigned["emp_codigo"];
    var tipo = $("#cmbTIPO_C").val();
    var todas = $("#chkTODAS_S").is(":checked");
    var saldo = $("#chkSALDO_S").is(":checked");

    var url = "./reports/wfReportPrint.aspx?report=BAL&empresa=" + parseInt(empresasigned["emp_codigo"]) + "&parameter1=" + fechac + "&parameter2=" + almacen + "&parameter3=" + debcre + "&parameter4=" + empresa + "&parameter5=" + tipo + "&parameter6=" + todas + "&parameter7=1&parameter8=" + saldo;
    var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=yes,scrollbars=yes";
    window.open(url, "Reporte", feautures);

}



function Edit(obj) {
   var id = $(obj).data("id");
   //CallDlistaPrecio(id); 
   window.location = "wfDbalancegeneral.aspx?codigocue=" + id["cue_codigo"];
}

function CallDlistaPrecio(id) {
    var obj = {};
    if (id != null) {
        obj["dlpr_empresa"] = id.dlpr_empresa;
        obj["dlpr_listaprecio"] = id.dlpr_listaprecio;
        obj["dlpr_codigo"] = id.dlpr_codigo;        
    }
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDlistaPrecio", jsonText, 3);
}

function CallDlistaPrecioResult(data) {
    if (data != "") {
        CallPopUp2("modDListaPrecio", "Detalle ListaPrecio", data.d);
        SetFormCalculo();
    }
    SetAutocompleteById("txtIDPRO");
    $(".fecha").datepicker({
        
        dateFormat: "dd/mm/yy"
    });


    /*$(".fecha").datepicker();
 //   $(".fecha").datepicker('setDate', new Date());
    $(".fecha").datepicker("option", "dateFormat", "dd/mm/yy");*/


}

function SetFormCalculo() {
}


function GetDlistaPrecioObj() {
    var obj = {};
    obj["dlpr_codigo"] = $("#txtCODIGO_P").val();
    obj["dlpr_codigo_key"] = $("#txtCODIGO_P").val();
    obj["dlpr_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["dlpr_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    obj["dlpr_listaprecio"] = parseInt($("#cmbLISTA_P").val());
    obj["dlpr_listaprecio_key"] = parseInt($("#cmbLISTA_P").val());
    obj["dlpr_almacen"] = $("#cmbALMACEN_P").val();
    obj["dlpr_producto"] = $("#cmbPRODUCTO").val();
    obj["dlpr_umedida"] = $("#cmbUMEDIDA").val();
    obj["dlpr_fecha_ini"] = $("#cmbFECHA_INI").val();
    obj["dlpr_fecha_fin"] = $("#cmbFECHA_FIN").val();
    obj["dlpr_precio"] = parseFloat($("#txtPRECIO").val());
    obj["dlpr_estado"] = GetCheckValue($("#chkESTADO"));
    obj["dlpr_idalmacen"] = $("#cmbALMACEN_P option:selected").text();
    obj["dlpr_nombreproducto"] = $("#txtIDPRO option:selected").text();
    obj["dlpr_nombreumedida"] = $("#cmbUMEDIDA option:selected").text();
    obj["dlpr_ruta"] = $("#cmbRUTA_P").val();
    obj = SetAuditoria(obj);
    return obj;
}

function CallDListaPrecioOK() {    
    SaveObj();
    return true;
}

function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtIDPRO") {
        return {
            label: item.pro_id + "," + item.pro_nombre,
            value: item.pro_nombre,
            info: item
        }
    }
}

function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtIDPRO") {
        $("#txtIDPRO").val(item.info.pro_nombre);
        $("#cmbPRODUCTO").val(item.info.pro_codigo);
     //   $("#txtPRODUCTO").val(item.info.pro_nombre);
     //   LoadProduct();
        //alert(ui.item.label);
    }
}

////////////////////////////////
//FUNCIONES de Selección y Deselección
function Select(obj) {
    // var tr = $(obj).closest('table').attr('id'); ;
    // CleanSelect(tr);
    if ($(obj).css("background-color") == rgbcolor) {
        $(obj).css("background-color", "");
        $(obj).children("td").css("background-color", "");
        $('agregar').prop('disabled', false);
    }
    else {
        $(obj).css("background-color", color);
        $(obj).children("td").css("background-color", color);
    }
    //GetTotales();
}

function SelectAllRows(event) {
    var htmltable = $("#" + event.data.tabla)[0];
    var contador = 0;
    for (var r = 1; r < htmltable.rows.length; r++) {
        if ($(htmltable.rows[r]).css("background-color") != rgbcolor) {
            Select(htmltable.rows[r]);
        }
    }
}

function CleanSelectedRows(event) {
    var htmltable = $("#" + event.data.tabla)[0];
    var contador = 0;
    for (var r = 1; r < htmltable.rows.length; r++) {
        if ($(htmltable.rows[r]).css("background-color") == rgbcolor) {
            Select(htmltable.rows[r]);
        }
    }
}

//////////////////////////////////////////////////////////////////////////////////
////////////////FUNCIONES PARA QUITAR LAS FILAS SELECCIONADAS/////////////////////

function QuitSelected() {
    jConfirm('¿Está seguro que desea eliminar los registros seleccionados?', 'Eliminar', function (r) {
        if (r)
            RemoveObjects();
    });
}

function RemoveObjects() {
    var detalle = new Array();
    var htmltable = $("#tddatos")[0];
    var rows = $("#tddatos").find("> tbody > tr ");
    $("#tddatos").find("> tbody > tr ").each(function () {
        if ($(this).css("background-color") == rgbcolor) {
            var id = $(this).data("id");
            var obj = {};
            obj["dlpr_empresa"] = id.dlpr_empresa;
            obj["dlpr_listaprecio"] = id.dlpr_listaprecio;
            obj["dlpr_codigo"] = id.dlpr_codigo;   
            detalle[detalle.length] = obj;
        }
    });
    var jsonText = JSON.stringify({ objeto: detalle });
    CallServer(formname + "/RemoveObjects", jsonText, 5);
}

function RemoveObjectsResult(data) {
    if (data != "") {
        if (data.d == "OK") {
            LoadDetalle();
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al eliminar los registros...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}

//////////////////////////////////////////////////////////////////////////////////
///////////////////FUNCIONES PARA GUARDAR SAVE/////////////////////////////////////

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

    /*var htmltable = $("#tddatos")[0];
    if (htmltable.rows.length < 2) {
    retorno = false;
    mensajehtml += "Es necesario ingresar al menos un detalle al comprobante<br>";
    }*/

    if (!retorno) {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert(mensajehtml, 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
    return retorno;
}

function SaveObj() {
    if (ValidateForm()) {
        var obj = GetDlistaPrecioObj();
        var jsonText = JSON.stringify({ objeto: obj });
        CallServer(formname + "/SaveObject", jsonText, 4);
    }
}

function SaveObjResult(data) {
    if (data != "") {
        if (data.d == "OK") {
            LoadDetalle();
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al guardar el registro...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}
