//Archivo:          CalculoPrecio.js
//Descripción:      Contiene las funciones comunes para la interfaz de calculo precios
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Diciembre 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfCalculoPrecio.aspx";
var menuoption = "CalculoPrecio";

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
                CallCalculoPrecioResult(data);
            if (retorno == 4)
                SaveObjResult(data);
            if (retorno == 5)
                RemoveObjectsResult(data);



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
    var jsonText = JSON.stringify({});
    CallServer(formname + "/GetCabecera", jsonText, 0);
}

function LoadCabeceraResult(data) {
    if (data != "") {
        $('#comcabeceracontent').html(data.d);
        LoadDetalle();
    }
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
        LoadDetalleData();
    }
    SetFormDetalle();
    //EditableRow("detalletabla");

}

function LoadDetalleData() {
    var obj = {};
    obj["cpr_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cpr_almacen"] = $("#cmbALMACEN").val();
    obj["cpr_ruta"] = $("#cmbRUTA").val();
    obj["cpr_listaprecio"] = $("#cmbLISTA").val();
    obj["cpr_tproducto"] = $("#cmbTPRODUCTO").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDetalleData", jsonText, 2);
}

function LoadDetalleDataResult(data) {
    if (data != "") {
        $('#tddatos').append(data.d);        
    }    

}


function SetForm() {
    $("#cmbALMACEN").on("change", LoadDetalle);
    $("#cmbRUTA").on("change", LoadDetalle);
    $("#cmbLISTA").on("change", LoadDetalle);
    $("#cmbTPRODUCTO").on("change", LoadDetalle);
}

function SetFormDetalle() {
    //Tratamiento para los elemntos del detalle    
    $("#adddet").on("click", CallCalculoPrecio);
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
    CallCalculoPrecio(id); 
}

function CallCalculoPrecio(id) {
    var obj = {};
    if (id != null) {
        obj["cpr_empresa"] = id.cpr_empresa;        
        obj["cpr_listaprecio"] = id.cpr_listaprecio;        
        obj["cpr_codigo"] = id.cpr_codigo;
        
    }
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetCalculoPrecio", jsonText, 3);
}

function CallCalculoPrecioResult(data) {
    if (data != "") {
        CallPopUp2("modCalculo", "Calculo Precio", data.d);
        SetFormCalculo();
    }
}
function SetFormCalculo() {
    /*$("#alldet_P").on("click", { tabla: "tddatos_P" }, SelectAllRows);
    $("#nonedet_P").on("click", { tabla: "tddatos_P" }, CleanSelectedRows);
    $(".fecha").datepicker();
    $(".fecha").datepicker('setDate', new Date());
    $(".fecha").datepicker("option", "dateFormat", "dd/mm/yy"); //Setea campos de tipo fecha*/

}


function GetCalculoPrecioObj() {
    var obj = {};
    obj["cpr_codigo"] = $("#txtCODIGO_P").val();
    obj["cpr_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cpr_almacen"] = $("#cmbALMACEN_P").val();
    obj["cpr_ruta"] = $("#cmbRUTA_P").val();
    obj["cpr_listaprecio"] = $("#cmbLISTA_P").val();
    obj["cpr_tproducto"] = $("#cmbTPRODUCTO_P").val();
    obj["cpr_nombre"] = $("#txtNOMBRE_P").val();
    obj["cpr_indice"] = parseFloat($("#txtINDICE_P").val());
    obj["cpr_valor"] = parseFloat($("#txtVALOR_P").val());
    obj["cpr_peso"] = parseFloat($("#txtPESO_P").val());
    obj = SetAuditoria(obj); 
    return obj;
}


function CallCalculoPrecioOK() {    
    SaveObj();
    return true;    
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
            obj["cpr_empresa"] = id.cpr_empresa;
            obj["cpr_listaprecio"] = id.cpr_listaprecio;
            obj["cpr_codigo"] = id.cpr_codigo;
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
        var obj = GetCalculoPrecioObj();
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

