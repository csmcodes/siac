﻿//Archivo:          CalculoPrecio.js
//Descripción:      Contiene las funciones comunes para la interfaz de calculo precios
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Diciembre 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfDbalancebancario.aspx";
var menuoption = "Dbalancebancario";

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
                LoadDetalleInicialResult(data);
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
    var obj = {};
    obj["emp_codigo"] = parseInt(empresasigned["emp_codigo"]);
    var jsonText = JSON.stringify({ objeto: obj });   
    CallServer(formname + "/GetCabecera", jsonText, 0);
}

function LoadCabeceraResult(data) {
    if (data != "") {
        $('#comcabeceracontent').html(data.d);
        LoadDetalle();
        GetPie();
    }
    $('.fecha').datepicker({ dateFormat: 'dd/mm/yy' }).val();
    SetForm(); //Depende de cada js.
}

function LoadDetalle() {
    $('#comdetallecontent').empty();
    var jsonText = JSON.stringify({});
    CallServer(formname + "/GetDetalle", jsonText, 1);
}

function LoadDetalleResult(data) {
    if (data != "") {
        $('#comdetallecontent').html(data.d);
        LoadDetalleInicial();
        LoadDetalleData();
    }
    SetFormDetalle();
   
    //EditableRow("detalletabla");
}

function GetCabeceraData() {
    var obj = {};
    obj["dbancario"] = GeBancoData();
    obj["salban"] = GetSalbanData();
    obj["emp_codigo"] = parseInt(empresasigned["emp_codigo"]);
    return obj;
}


function GeBancoData() {
    var obj = {};
    obj["dban_banco"] = parseInt($("#txtCODBANCO").val());
    obj["dban_beneficiario"] = $("#cmbBENEFICIARIO_C").val();
    obj["dban_fechacsc"] = $("#cmbFECHAINI_C").val();
    obj["dban_fechaant"] = $("#cmbFECHAFIN_C").val();

    return obj;
}
function GetSalbanData() {
    var obj = {};
    obj["slb_almacen"] = parseInt($("#cmbALMACEN_S").val());
    obj["slb_transacc"] = parseInt($("#cmbTRANSACION_S").val());
    return obj;
}


function LoadDetalleInicial() {
    var obj = GetCabeceraData();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDetalleTotal", jsonText, 6);
}

function LoadDetalleInicialResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);       
        $('#txtSALDOINICIAL_S').val(obj[0]);
        $("#txtTOTALCOM").val(obj[1]);    
    }
    //EditableRow("detalletabla");
}

function LoadDetalleData() {
    var obj = GetCabeceraData();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDetalleData", jsonText, 2);
}

function LoadDetalleDataResult(data) {
    if (data != "") {
        $('#tddatos').append(data.d);
    }
   
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
function SetForm() {
    SetAutocompleteById("txtIDBANCO");

    $("#cmbBENEFICIARIO_C").keyup("change", LoadDetalle);
    $("#cmbTRANSACION_S").on("change", LoadDetalle);
    $("#cmbALMACEN_S").on("change", LoadDetalle);
    $("#cmbFECHAINI_C").on("change", LoadDetalle);
    $("#cmbFECHAFIN_C").on("change", LoadDetalle);

    $("#print").on("click", PrintReporte);
   
}

function SetFormDetalle() {
    //Tratamiento para los elemntos del detalle    
    $("#adddet").on("click", CallDlistaPrecio);
    $("#deldet").on("click", QuitSelected);
    $("#alldet").on("click", { tabla: "tddatos" }, SelectAllRows);
    $("#nonedet").on("click", { tabla: "tddatos" }, CleanSelectedRows);
    $("#comdetallecontent").on("scroll", scroll);
}

function scroll() {
    if ($("#comdetallecontent")[0].scrollHeight - $("#comdetallecontent").scrollTop() <= $("#comdetallecontent").outerHeight()) {
        LoadDetalleData();
    }
}


function Edit(obj) {
   var id = $(obj).data("id");
   CallDlistaPrecio(id); 
}

function CallDlistaPrecio(id) {
    var compr = {};
    if (id != null) {
        compr["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        compr["com_codigo"] = parseInt(id["dban_cco_comproba"]);
    }
    var jsonText = JSON.stringify({ objeto: compr });
    CallServer("ws/Metodos.asmx/GetFormulario", jsonText, 3);
}

function CallDlistaPrecioResult(data) {
    if (data != "") {
        window.location = data.d;
    };

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
    return obj;
}

function CallDListaPrecioOK() {    
    SaveObj();
    return true;
}

function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtIDBANCO") {
        return {
            label: item.ban_id + "," + item.ban_nombre + "," + item.ban_cuenta,
            value: item.ban_nombre,
            info: item
        }
    }
}

function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtIDBANCO") {
        $("#txtCODBANCO").val(item.info.ban_codigo);
   //     $("#txtNOMBREBANCO").val(item.info.ban_nombre);
        $("#txtIDBANCO").val(item.info.ban_nombre);
      //  $("#txtCUENTA").val(item.info.ban_cuenta);
        // ShowCampos();
        LoadDetalle();
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





function PrintReporte() {
    var desde = $("#cmbFECHAINI_C").val(); //.datepicker("getDate");
    var hasta = $("#cmbFECHAFIN_C").val(); //.datepicker("getDate");
    var banco = $("#txtCODBANCO").val();
    var strbanco = $("#txtIDBANCO").val();
    var almacen = $("#cmbALMACEN_S").val();
    var benef = $("#cmbBENEFICIARIO_C").val();
    var tipo = $("#cmbTRANSACION_S").val();

    var url = "./reports/wfReportPrint.aspx?report=DETALLEBAN&empresa=" + parseInt(empresasigned["emp_codigo"]) + "&parameter1=" + desde + "&parameter2=" + hasta + "&parameter3=" + banco + "&parameter4=" + strbanco+ "&parameter5=" + tipo+ "&parameter6=" + almacen;
    var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=0,scrollbars=0";
    window.open(url, "Reporte", feautures);
    //loadIframeReport(url);
}
