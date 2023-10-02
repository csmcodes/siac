//Archivo:          CalculoPrecio.js
//Descripción:      Contiene las funciones comunes para la interfaz de calculo precios
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Diciembre 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfEstadoCuentanew.aspx";
var menuoption = "wfEstadoCuentanew";

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
                CallGuiasResult(data);
            /*if (retorno == 4)
            GetPriceResult(data);*/
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
function GetCabeceraComprobante(debcre) {
    if (debcre >= 0) {
        $("#txtDEBCRE").val(debcre);
        LoadDetalle();
    }
}
function LoadCabecera() {
    var jsonText = JSON.stringify({});
    CallServer(formname + "/GetCabecera", jsonText, 0);
}

function LoadCabeceraResult(data) {
    if (data != "") {
        $('#comcabeceracontent').html(data.d);
        $("#cmbNOMBRE").on("change", LoadDetalle);
        $("#cmbALMACEN").on("change", LoadDetalle);
        $("#cmbCORTE").on("change", LoadDetalle);
        GetCabeceraComprobante($("#txtdebcre").val()); //FUNCION DEFINIDA EN FUNCTIONS.JS

    }
    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha
    SetForm(); //Depende de cada js.
}

function LoadDetalle() {
    var obj = {};
  
    var jsonText = JSON.stringify({ id: obj });
    CallServer(formname + "/GetDetalle", jsonText, 1);


}

function LoadDetalleResult(data) {
    if (data != "") {
        $('#comdetallecontent').html(data.d);
        
    }
    SetFormDetalle();
    //EditableRow("detalletabla");

}

function LoadDetalleData() {
    var obj = {};
    obj["per_codigo"] = parseInt($("#txtCODPROVEE").val());
    obj["com_almacen"] = parseInt($("#cmbALMACEN").val());
    obj["ddo_fecha_emi"] = $("#cmbCORTE").val();
    if (obj["ddo_fecha_emi"] == "") {
        obj["ddo_fecha_emi"] = new Date().toLocaleString();
    }
    obj["ddo_debcre"] = parseInt($("#txtdebcre").val());
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDetalleData", jsonText, 2);
}

function LoadDetalleDataResult(data) {
    if (data != "") {
        $('#tddatos').append(data.d);        
    }    

}


function SetForm() {
 SetAutocompleteById("cmbNOMBRE");
}

function SetFormDetalle() {
    //Tratamiento para los elemntos del detalle    
    $("#adddet").on("click", CallGuias);
    $("#deldet").on("click", QuitGuias);
 //   $("#alldet").on("click", { tabla: "tddatos" }, SelectAllRows);
 //   $("#nonedet").on("click", { tabla: "tddatos" }, CleanSelectedRows);
    LoadDetalleData(); 
}


function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtIDPER") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_id,
            info: item
        }
    }
    if (idobj == "cmbNOMBRE") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_apellidos + " " + item.per_nombres,
            info: item
        }
    }
}



function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtIDPER") {
        $("#txtCODPER").val(item.info.per_codigo);
        $("#txtCIRUC").val(item.info.per_ciruc);
        $("#txtNOMBRES").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtRAZON").val(item.info.per_razon);
        $("#txtRUC").val(item.info.per_ciruc);
        $("#txtDIRECCION").val(item.info.per_direccion);
        $("#txtTELEFONO").val(item.info.per_telefono);

    }
     if (idobj == "cmbNOMBRE") {
         $("#cmbNOMBRE").val(item.info.per_apellidos + " " + item.info.per_nombres);
         $("#txtCODPROVEE").val(item.info.per_codigo);
        
        LoadDetalle();

    }

}


function GetTotalObj() {
    var obj = {};
    obj["tot_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["tot_comprobante"] = $("#txtcodigocomp").val();
    obj["tot_total"] = ($.isNumeric($("#txtTOTALCOM").val())) ? parseFloat($("#txtTOTALCOM").val()) : null;
    obj = SetAuditoria(obj);
    return obj;
}


function GetDetalle() {
    var detalle = new Array();
    var htmltable = $("#tddatos")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var obj = GetPlanillacliObj(htmltable.rows[r]);
        if (obj != null)
            detalle[r] = obj;
    }
    return detalle;
}

function GetPlanillacliObj(row) {

    if ($(row).data("comprobante")) {
        var obj = {};
        obj["plc_comprobante"] = parseInt($(row).data("comprobante").toString());
        obj["plc_comprobante_key"] = parseInt($(row).data("comprobante").toString());
        obj["plc_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["plc_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
        obj = SetAuditoria(obj);
        return obj;
    }
    else
        return null;

}

function GetComprobanteObj() {
    var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHACOMP").val()); // $("#txtFECHA_P").datepicker("getDate");
    var almacen = parseInt($("#txtCODALMACEN").val());
    var pventa = parseInt($("#txtCODPVENTA").val());

    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = $("#txtcodigocomp").val();
    obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
    obj["com_ctipocom"] = parseInt($("#cmbSIGLA_P").val());  //2 FACT
    obj["com_fecha"] = currentDate;
    obj["com_doctran"] = $("#numerocomp").html();
    obj["com_nocontable"] = parseInt($("#txtnocontable").val());
    obj["com_periodo"] = currentDate.getFullYear();
    obj["com_almacen"] = almacen;
    obj["com_pventa"] = pventa;
    obj["com_codclipro"] = parseInt($("#txtCODPER").val());
    obj["com_agente"] = parseInt($("#txtCODVEN").val());
    obj["planillas"] = GetDetalle();
    obj["total"] = GetTotalObj();
    obj = SetAuditoria(obj);
    return obj;

}
//FUNCIONES de Selección y Deselección
function Edit(obj) {
    //var id = $(obj).data("id");

    Select(obj);
}

function Select(obj) {
    var id = $(obj).data("id");
    var jsonText = JSON.stringify({ id: id });
    if ($("#cmbNOMBRE").val() == "") {

        $("#cmbNOMBRE").val(id.per_nombres);
    }
    window.location = "wfDEstadoCuentanew.aspx?debcre=" + $("#txtdebcre").val() + "&codpersona=" + id.per_codigo + "&codalmacen=" + parseInt($("#cmbALMACEN").val()) + "&fechacort=" + $("#cmbCORTE").val() + " &nombrepe=" + $("#cmbNOMBRE").val();
}

function SelectAllRows(event) {
    var htmltable = $("#" + event.data.tabla)[0];
    var contador = 0;
    for (var r = 1; r < htmltable.rows.length; r++) {
        if ($(htmltable.rows[r]).css("background-color") != rgbcolor) {
            Mark(htmltable.rows[r]);
        }
    }
}

function CleanSelectedRows(event) {
    var htmltable = $("#" + event.data.tabla)[0];
    var contador = 0;
    for (var r = 1; r < htmltable.rows.length; r++) {
        if ($(htmltable.rows[r]).css("background-color") == rgbcolor) {
            Mark(htmltable.rows[r]);
        }
    }
}

///////////////////////////////////////////////////////



//FUNCIONES PARA LLAMAR POPUP DE GUIAS

function CallGuias() {
    var obj = GetComprobanteObj();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetGuias", jsonText, 3);
}

function CallGuiasResult(data) {
    if (data != "") {
        CallPopUp2("modGuias", "Guías Disponibles", data.d);
        SetFormGuias();
    }
}
function SetFormGuias() {
  //  $("#alldet_P").on("click", { tabla: "tddatos_P" }, SelectAllRows);
 //   $("#nonedet_P").on("click", { tabla: "tddatos_P" }, CleanSelectedRows);
    $(".fecha").datepicker({
        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha

}
function GetDetalleGuias() {
    var detalle = new Array();
    var htmltable = $("#tddatos_P")[0];
    var contador = 0;
    for (var r = 1; r < htmltable.rows.length; r++) {
        if ($(htmltable.rows[r]).css("background-color") == rgbcolor) {
            var obj = GetGuiasObj(htmltable.rows[r]);
            if (obj != null) {
                detalle[contador] = obj;
                contador++;
            }
        }
    }
    return detalle;
}

function GetGuiasObj(row) {
    var obj = {};
    obj["comprobante"] = parseInt($(row).data("comprobante").toString());
    obj["fecha"] = row.cells[0].innerText;
    obj["socio"] = row.cells[1].innerText;
    obj["doctran"] = row.cells[2].innerText;
    obj["valor"] = parseFloat(row.cells[3].innerText);
    return obj;
}


function CallGuiasOK() {
    var objs = GetDetalleGuias();
    SetGuias(objs);
}
/////////////////////////////////////////


function SetGuias(objs) {

    for (var i = 0; i < objs.length; i++) {
        var obj = objs[i];
        //var row = $("<tr ></tr>")
        var row = $("<tr data-comprobante='" + obj["comprobante"] + "' onclick='Select(this);'></tr>");
        row.append("<td>" + obj["fecha"] + "</td>");
        row.append("<td>" + obj["socio"] + "</td>");
        row.append("<td>" + obj["doctran"] + "</td>");
        row.append("<td class='right' >" + obj["valor"] + "</td>");
        $("#tddatos").find("> tbody").append(row);
    }
    CalculaTotales();
}


function QuitGuias() {
    var htmltable = $("#tddatos")[0];
    var rows = $("#tddatos").find("> tbody > tr ");
    $("#tddatos").find("> tbody > tr ").each(function () {
        if ($(this).css("background-color") == rgbcolor)
            $(this).remove();
    });
    CalculaTotales();
}


function CalculaTotales() {
    var htmltable = $("#tddatos")[0];
    var total = 0;
    for (var r = 0; r < htmltable.rows.length; r++) {
        var c = htmltable.rows[r].cells.length - 1; //celda valor
        var valor = 0;
        valor = htmltable.rows[r].cells[c].innerText;
        total += ($.isNumeric(valor)) ? parseFloat(valor) : 0;
    }
    $("#txtREGISTROS").val(htmltable.rows.length - 1);
    $("#txtTOTALCOM").val(CurrencyFormatted(total));
}


//FUNCIONES PARA GUARDAR SAVE

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
        var obj = GetComprobanteObj();
        if ($(this)[0].id == "save")
            obj["com_estado"] = parseInt($("#txtCREADO").val());
        else if ($(this)[0].id == "close")
            obj["com_estado"] = parseInt($("#txtCERRADO").val());
        var jsonText = JSON.stringify({ objeto: obj });
        CallServer(formname + "/SaveObject", jsonText, 5);
    }
}


function PrintObj() {
    window.location = "wfPlanillaClientePrint.aspx?codigocomp=" + $("#txtcodigocomp").val();
}

function InvoiceObj() {
    var planilla = $("#txtcodigocomp").val();
    window.location = "wfComprobante.aspx?tipodoc=4&origen=LGC&codigocompref=" + planilla;
}

function SaveObjResult(data) {
    if (data != "") {
        if (data.d != "-1") {
            $("#txtcodigocomp").val(data.d);
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

