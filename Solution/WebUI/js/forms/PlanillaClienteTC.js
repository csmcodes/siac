//Archivo:          PlanillaClienteTC.js
//Descripción:      Contiene las funciones comunes para la interfaz de planilla de clientes propios para TransCombustibles
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Abril 2015
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfPlanillaClienteTC.aspx";
var menuoption = "PlanillaClienteTC";

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
    $("#close").on("click", SaveObj);
    $("#save").on("click", SaveObj);
    $("#print").on("click", PrintObj);
    $("#invo").on("click", InvoiceObj);


    var codigocomp = $("#txtcodigocomp").val();
    if (codigocomp <= 0) {
        $("#print").css({ 'display': 'none' });
        $("#close").css({ 'display': 'none' });
        $("#invo").css({ 'display': 'none' });
    }
    $("#help").css({ 'display': 'none' });
    $("#despachar").css({ 'display': 'none' });

    $("#barracomp").children("ul").append("<li><div class=\"btn\" id=\"liquidar\"><i class=\"iconfa-lock\"></i> &nbsp; Liquidar </div></li>");
    $("#barracomp").children("ul").append("<li><div class=\"btn\" id=\"valores\"><i class=\"iconfa-lock\"></i> &nbsp; Valores Socios</div></li>");
    $("#liquidar").on("click", Liquidar);
    $("#valores").on("click", Valores);

    LoadCabecera();
    LoadPie();
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
                CallGuiasResult(data);
            /*if (retorno == 4)
            GetPriceResult(data);*/
            if (retorno == 5)
                SaveObjResult(data);
            if (retorno == 6)
                InvoiceObjResult(data);


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
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = $("#txtcodigocomp").val();
    var jsonText = JSON.stringify({ objeto: obj });

    if ($("#comcabeceracontent").length > 0) {
        CallServer(formname + "/GetCabecera", jsonText, 0);
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
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = $("#txtcodigocomp").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDetalle", jsonText, 1);


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
    obj["com_codigo"] = $("#txtcodigocomp").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetPie", jsonText, 2);
}

function LoadPieResult(data) {
    if (data != "") {
        $('#compiecontent').html(data.d);
    }
}

function SetForm() {
    //Tratamiento para los elementos de la cabecera
    SetAutocompleteById("txtIDPER");


}

function SetFormDetalle() {
    //Tratamiento para los elemntos del detalle    
    $("#adddet").on("click", CallEnvios);
    $("#deldet").on("click", QuitGuias);
    $("#alldet").on("click", { tabla: "tddatos" }, SelectAllRows);
    $("#nonedet").on("click", { tabla: "tddatos" }, CleanSelectedRows);
    if ($("#txtESTADO").val() == $("#txtCERRADO").val()) {
        var inputs = $('input, textarea, select');
        $(inputs).each(function () {
            $(this).prop("disabled", true);

        });
    }
    CalculaTotales();
}




function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtIDPER") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_id,
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

}


function GetTotalObj() {
    var obj = {};

    obj["tot_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["tot_comprobante"] = $("#txtcodigocomp").val();

    obj["tot_porc_impuesto"] = ($.isNumeric($("#txtIVAPORCENTAJE").val())) ? parseInt($("#txtIVAPORCENTAJE").val()) : null;
    //obj["tot_descuento1"] = ($.isNumeric($("#txtDESCIVA").val())) ? parseFloat($("#txtDESCIVA").val()) : null;
    //obj["tot_descuento2"] = ($.isNumeric($("#txtDESCUENTOIVA").val())) ? parseFloat($("#txtDESCUENTOIVA").val()) : null;
    obj["tot_desc1_0"] = ($.isNumeric($("#txtDESCUENTO").val())) ? parseFloat($("#txtDESCUENTO").val()) : null;
    //obj["tot_desc2_0"] = ($.isNumeric($("#txtDESCUENTO0").val())) ? parseFloat($("#txtDESCUENTO0").val()) : null;

    obj["tot_timpuesto"] = ($.isNumeric($("#txtIVA").val())) ? parseFloat($("#txtIVA").val()) : null;
    obj["tot_tseguro"] = ($.isNumeric($("#txtSEGURO").val())) ? parseFloat($("#txtSEGURO").val()) : null;
    obj["tot_transporte"] = ($.isNumeric($("#txtTRANSPORTE").val())) ? parseFloat($("#txtTRANSPORTE").val()) : null;

    var subtotal0 = ($.isNumeric($("#txtSUBTOTAL0").val())) ? parseFloat($("#txtSUBTOTAL0").val()) : null;
    obj["tot_subtot_0"] = subtotal0 - obj["tot_transporte"];
    var subtotal = ($.isNumeric($("#txtSUBTOTALIVA").val())) ? parseFloat($("#txtSUBTOTALIVA").val()) : null;
    obj["tot_subtotal"] = subtotal - obj["tot_tseguro"];

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

    var now = new Date();

    var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHACOMP").val());
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
    obj["com_codclipro"] = parseInt($("#txtCODPER").val());
    obj["nombres"] = $("#txtNOMBRES").val();

    obj["com_agente"] = parseInt($("#txtCODVEN").val());
    obj["planillas"] = GetDetalle();
    obj["total"] = GetTotalObj();
    obj = SetAuditoria(obj);
    return obj;

}
//FUNCIONES de Selección y Deselección


function Mark(obj) {
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
function EndAsignarSocio(id) {
    LoadDetalle();
}


//FUNCIONES PARA LLAMAR POPUP DE GUIAS

////FUNCIONES PARA LLAMAR POPUP DE BUSQUEDA DE COMPROBANTES

function CallEnvios() {
    var obj = GetComprobanteObj();
    CallBuscaComprobantes(obj);
}




/*function CallGuias() {
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
$("#alldet_P").on("click", { tabla: "tddatos_P" }, SelectAllRows);
$("#nonedet_P").on("click", { tabla: "tddatos_P" }, CleanSelectedRows);
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
obj["subtotal0"] = row.cells[3].innerText;
obj["subtotaliva"] = row.cells[4].innerText;
obj["valor"] = row.cells[5].innerText;    
//obj["valor"] = parseFloat(row.cells[5].innerText);    
return obj;
}


function CallGuiasOK() {
var objs = GetDetalleGuias();
SetGuias(objs);
}*/
/////////////////////////////////////////


function SetEnvios(objs) {

    for (var i = 0; i < objs.length; i++) {

        var obj = objs[i];
        //var row = $("<tr ></tr>")
        var row = $("<tr data-comprobante='" + obj["comprobante"] + "' onclick='Mark(this);'></tr>");
        //row.append("<td>" + GetDateValue(obj["fecha"]) + "</td>");
        row.append("<td>" + obj["fecha"] + "</td>");
        row.append("<td>" + obj["socio"] + "</td>");
        row.append("<td>" + obj["doctran"] + "</td>");
        row.append("<td class='right' >" + obj["subtotal0"] + "</td>");
        row.append("<td class='right' >" + obj["subtotaliva"] + "</td>");
        row.append("<td class='right' >" + obj["iva"] + "</td>");
        row.append("<td class='right' >" + obj["seguro"] + "</td>");
        row.append("<td class='right' >" + obj["transporte"] + "</td>");
        row.append("<td class='right' >" + obj["total"] + "</td>");
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
    var subtot0 = 0;
    var subtotal = 0;
    var desc = 0;
    var iva = 0;
    var seguro = 0;
    var transporte = 0;

    for (var r = 0; r < htmltable.rows.length; r++) {
        var c = htmltable.rows[r].cells.length - 1; //celda valor
        var sub0 = 0;
        var sub = 0;
        var tot = 0;
        var iv = 0;
        var ds = 0;
        var seg = 0;
        var tra = 0;

        sub0 = htmltable.rows[r].cells[c - 6].innerText;
        sub = htmltable.rows[r].cells[c - 5].innerText;
        ds = htmltable.rows[r].cells[c - 4].innerText;
        iv = htmltable.rows[r].cells[c - 3].innerText;
        seg = htmltable.rows[r].cells[c - 2].innerText;
        tra = htmltable.rows[r].cells[c - 1].innerText;
        tot = htmltable.rows[r].cells[c].innerText;

        transporte += ($.isNumeric(tra)) ? parseFloat(tra) : 0;
        seguro += ($.isNumeric(seg)) ? parseFloat(seg) : 0;
        desc += ($.isNumeric(ds)) ? parseFloat(ds) : 0;
        iva += ($.isNumeric(iv)) ? parseFloat(iv) : 0;
        subtot0 += ($.isNumeric(sub0)) ? parseFloat(sub0) : 0;
        subtotal += ($.isNumeric(sub)) ? parseFloat(sub) : 0;
        //total  += parseFloat(sub0)  + parseFloat(sub)  + parseFloat(iv) + parseFloat(seg)+ parseFloat(tra);
        //total += ($.isNumeric(tot)) ? parseFloat(tot) : 0;

    }

    var porcentajeiva = ($.isNumeric($("#txtIVAPORCENTAJE").val())) ? parseFloat($("#txtIVAPORCENTAJE").val()) : 0;

    iva = (subtotal + seguro) * (porcentajeiva / 100);

    //total = subtot0 + subtotal + iva + seguro + transporte;//   parseFloat(sub0) + parseFloat(sub) + parseFloat(iv) + parseFloat(seg) + parseFloat(tra);

    subtot0 += transporte;
    subtotal += seguro;
    total = subtot0 + subtotal + iva;



    $("#txtREGISTROS").val(htmltable.rows.length - 1);
    $("#txtDESCUENTO").val(CurrencyFormatted(desc));
    $("#txtIVA").val(CurrencyFormatted(iva));
    $("#txtSEGURO").val(CurrencyFormatted(seguro));
    $("#txtTRANSPORTE").val(CurrencyFormatted(transporte));
    $("#txtSUBTOTAL0").val(CurrencyFormatted(subtot0));
    $("#txtSUBTOTALIVA").val(CurrencyFormatted(subtotal));
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
    if (!saving) {
        if (ValidateForm()) {
            var obj = GetComprobanteObj();
            if ($(this)[0].id == "save")
                obj["com_estado"] = parseInt($("#txtCREADO").val());
            else if ($(this)[0].id == "close")
                obj["com_estado"] = parseInt($("#txtCERRADO").val());

            $("#save").attr("disabled", true);
            saving = true;

            var jsonText = JSON.stringify({ objeto: obj });
            CallServer(formname + "/SaveObject", jsonText, 5);
        }
    }
}


function PrintObj() {
    window.location = "wfPlanillaClientePrint.aspx?codigocomp=" + $("#txtcodigocomp").val();


}


function ValidaPlanilla() {

    var fac = false;
    var htmltable = $("#tddatos")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        if (htmltable.rows[r].cells[2].innerText.indexOf("FAC") >= 0) {
            fac = true;
            break;
        }
    }

    if (fac) {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert('No se puede generar factura de otra factura...', 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
        return false;
    }
    return true;

}

function InvoiceObj() {
    if (ValidaPlanilla()) {
        var obj = {};
        obj["tpd_codigo"] = 4; //FACTURA;
        var jsonText = JSON.stringify({ objeto: obj });
        CallServer("ws/Metodos.asmx/GetFormularioByTipoDoc", jsonText, 6);
    }
}

function InvoiceObjResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        var planilla = $("#txtcodigocomp").val();
        window.location.href = obj.for_id + "?tipodoc=4&origen=LGC&codigocompref=" + planilla + "&codigoempresa=" + empresasigned["emp_codigo"];
    }


}

function SaveObjResult(data) {



    $("#save").attr("disabled", false);
    saving = false;


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


function Liquidar() {
    var id = $("#txtcodigocomp").val();
    window.location.href = "wfCancelacion.aspx?tipodoc=6&origen=LGC&codigocompref=" + id + "&codigoempresa=" + empresasigned["emp_codigo"];
}

function Valores() {
    var obj = GetComprobanteObj();
    CallValoresSocios(obj);

}
