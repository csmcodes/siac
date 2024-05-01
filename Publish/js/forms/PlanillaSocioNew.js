//Archivo:          PlanillaClienteNew.js
//Descripción:      Contiene las funciones comunes para la interfaz de planilla de clientes
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Noviembre 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfPlanillaSocioNew.aspx";
var menuoption = "PlanillaSocioNew";


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
    $("#invo").css({ 'display': 'none' });
    $("#movedet").css({ 'display': 'none' });
    
    var codigocomp = $("#txtcodigocomp").val();
    if (codigocomp <= 0) {
        $("#print").css({ 'display': 'none' });
        $("#close").css({ 'display': 'none' });
       
    }

    LoadCabecera();
    
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
                CallCancelacionesResult(data);
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
    else if (origen == "VS") { //VALORES SOCIO
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = $("#txtcodigocompref").val();
        obj["com_almacen"] = parseInt($("#txtCODALMACEN").val());
        obj["com_pventa"] = parseInt($("#txtCODPVENTA").val());
        obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
        obj["com_codclipro"] = $("#txtcodsocio").val();

        obj["vehiculo"] = $("#txtvehiculo").val();

        var jsonText = JSON.stringify({ objeto: obj });
        if ($("#comcabeceracontent").length > 0) {
            CallServer(formname + "/GetCabeceraFromSocio", jsonText, 0);
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
        obj["com_almacen"] = parseInt($("#txtCODALMACEN").val());
        obj["com_pventa"] = parseInt($("#txtCODPVENTA").val());
        obj["com_tipodoc"] = parseInt($("#txttipodoc").val());

        var jsonText = JSON.stringify({ objeto: obj });
        CallServer(formname + "/GetDetalle", jsonText, 1);
    }
    else if (origen == "VS") { //VALORES SOCIO
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = $("#txtcodigocomp").val();
        obj["com_almacen"] = parseInt($("#txtCODALMACEN").val());
        obj["com_pventa"] = parseInt($("#txtCODPVENTA").val());
        obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
        obj["com_codclipro"] = $("#txtcodsocio").val();

        obj["vehiculo"] = $("#txtvehiculo").val();

        var jsonText = JSON.stringify({ objeto: obj });
        CallServer(formname + "/GetDetalleFromSocio", jsonText, 1);
    }

}

function LoadDetalleResult(data) {
    if (data != "") {
        $('#comdetallecontent').html(data.d);
        LoadPie();
    }

    var veh = $("#tddatos>tbody>tr:first").data("vehiculo");

    $("#txtVEHICULO").val(veh);


    SetFormDetalle();
    //EditableRow("detalletabla");

}

function LoadPie() {
//    var origen = $("#txtorigen").val();
//    if (origen == "") {
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = $("#txtcodigocomp").val();
        obj["com_almacen"] = parseInt($("#txtCODALMACEN").val());
        obj["com_pventa"] = parseInt($("#txtCODPVENTA").val());
        obj["com_tipodoc"] = parseInt($("#txttipodoc").val());        
        var jsonText = JSON.stringify({ objeto: obj });
        CallServer(formname + "/GetPie", jsonText, 2);
//    }
//    else if (origen == "VS") { //VALORES SOCIO
//        var obj = {};
//        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
//        obj["com_codigo"] = $("#txtcodigocomp").val();
//        obj["com_almacen"] = parseInt($("#txtCODALMACEN").val());
//        obj["com_pventa"] = parseInt($("#txtCODPVENTA").val());
//        obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
//        obj["com_codclipro"] = $("#txtcodsocio").val();
//        var jsonText = JSON.stringify({ objeto: obj });
//        CallServer(formname + "/GetPieFromSocio", jsonText, 2);
//    }

}

function LoadPieResult(data) {
    if (data != "") {
        $('#compiecontent').html(data.d);

    }
    CalculaTotales();
}

function SetForm() {
    //Tratamiento para los elementos de la cabecera
    SetAutocompleteById("txtIDPER");
   

}

function SetFormDetalle() {
    //Tratamiento para los elemntos del detalle    
    $("#adddet").on("click", CallCancelaciones);
    $("#deldet").on("click", QuitCancelaciones);
    $("#alldet").on("click", { tabla: "tddatos" }, SelectAllRows);
    $("#nonedet").on("click", { tabla: "tddatos" }, CleanSelectedRows);

    

    if ($("#txtESTADO").val() == $("#txtCERRADO").val()) {
        var inputs = $('input, textarea, select');
        $(inputs).each(function () {
            $(this).prop("disabled", true);

        });
    }
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
    obj["tot_total"] = ($.isNumeric($("#txtTOTALCOM").val())) ? parseFloat($("#txtTOTALCOM").val()) : null;
    obj = SetAuditoria(obj);
    return obj;
}


function GetDetalle() {
    var detalle = new Array();
    var htmltable = $("#tddatos")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var obj = GetPlanillasocObj(htmltable.rows[r]);
        if (obj != null)
            detalle[r] = obj;
    }
    return detalle;
}

function GetPlanillasocObj(row) {

    if ($(row).data("comprobante")) {
        var obj = {};
      
        obj["dca_comprobante"] = parseInt($(row).data("comprobante").toString());
        obj["dca_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["dca_transacc"] = parseInt($(row).data("dca_transacc").toString());
        obj["dca_doctran"] = $(row).data("dca_doctran").toString();
        obj["dca_pago"] = parseInt($(row).data("dca_pago").toString());
        obj["dca_comprobante_can"] = parseInt($(row).data("dca_comprobante_can").toString());
        obj["dca_secuencia"] = parseInt($(row).data("dca_secuencia").toString());

        obj["dca_comprobante_key"] = parseInt($(row).data("comprobante").toString());
        obj["dca_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
        obj["dca_transacc_key"] = parseInt($(row).data("dca_transacc").toString());
        obj["dca_doctran_key"] = $(row).data("dca_doctran").toString();
        obj["dca_pago_key"] = parseInt($(row).data("dca_pago").toString());
        obj["dca_comprobante_can_key"] = parseInt($(row).data("dca_comprobante_can").toString());
        obj["dca_secuencia_key"] = parseInt($(row).data("dca_secuencia").toString());
        obj = SetAuditoria(obj);
        return obj;
    }
    else
        return null;

}


function GetRubros() {
    var detalle = new Array();
    var htmltable = $("#tdrubros")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var obj = GetRubroObj(htmltable.rows[r]);
        if (obj != null)
            detalle[r] = obj;
    }
    return detalle;
}

function GetRubroObj(row) {

    if ($(row).data("rubro")) {
        var obj = {};

        
        obj["rpl_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["rpl_rubro"] = $(row).data("rubro").toString();
        var valor = 0;
        //var row = htmltablerub.rows[r];
        var ing = $(row.cells[1]).children("input");
        if (ing.length > 0) {
            valor = ($.isNumeric($(ing).val())) ? parseFloat($(ing).val()) : 0;
        }
        var egr = $(row.cells[2]).children("input");
        if (egr.length > 0) {
            valor = ($.isNumeric($(egr).val())) ? parseFloat($(egr).val()) : 0;
        }
        obj["rpl_valor"] = valor;        
        obj = SetAuditoria(obj);
        return obj;
    }
    else
        return null;

}

function GetComprobanteObj() {
    var now = new Date();
    var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHACOMP").val()); // $("#txtFECHA_P").datepicker("getDate");
    var almacen = parseInt($("#txtCODALMACEN").val());
    var pventa = parseInt($("#txtCODPVENTA").val());

    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = $("#txtcodigocomp").val();
    obj["com_tipodoc"] = parseInt($("#txttipodoc").val());

    obj["com_ctipocom"] = parseInt($("#txtCTIPOCOM").val());  //3 REC
    //obj["com_fecha"] = currentDate;
    obj["com_numero"] = $("#txtNUMERO").val();
    obj["com_fecha"] = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate(), now.getHours(), now.getMinutes(), now.getSeconds(), now.getMilliseconds());
    obj["com_doctran"] = $("#numerocomp").html();
    obj["com_nocontable"] = parseInt($("#txtnocontable").val());
    obj["com_periodo"] = currentDate.getFullYear();
    obj["com_almacen"] = almacen;
    obj["com_pventa"] = pventa;
    obj["com_codclipro"] = parseInt($("#txtCODPER").val());
    obj["com_agente"] = parseInt($("#txtCODVEN").val());
    obj["com_concepto"] = $("#txtOBSERVACIONES").val();
    obj["cancelaciones"] = GetDetalle();
    obj["total"] = GetTotalObj();
    obj["rubros"] = GetRubros();
    obj = SetAuditoria(obj);
    return obj;

}

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


//FUNCIONES PARA LLAMAR POPUP DE Cancelaciones

function CallCancelaciones() {
    var obj = GetComprobanteObj();
    CallBuscaCancelacion(obj);
    //var obj = GetComprobanteObj(); 
    //var jsonText = JSON.stringify({ objeto: obj });
    //CallServer(formname + "/GetGuias", jsonText, 3);
}

function CallCancelacionesResult(data) {
    if (data != "") {
        CallPopUp2("modCancelaciones", "Cancelaciones Disponibles", data.d);
        SetFormCancelaciones();
    }
}
function SetFormCancelaciones() {
    $("#alldet_P").on("click", { tabla: "tddatos_P" }, SelectAllRows);
    $("#nonedet_P").on("click", { tabla: "tddatos_P" }, CleanSelectedRows);
    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha
    $("#cmbALMACEN_B").on("change", LoadPVentaBusquedaComprobante);  //Opción "Cerrar" del combo de opciones de la sección de edición    
    $("#cmbALMACEN_B").trigger("change");

    //    $("#txtFECHA_B").on("change", LoadDataBusquedaComprobante);
    $("#txtNUMERO_B").on("change", LoadDataBusquedaComprobante);
    //    $("#cmbPVENTA_B").on("change", LoadDataBusquedaComprobante);
    //    $("#txtNUMERO_B").on("change", LoadDataBusquedaComprobante);    

}
function GetDetalleCancelaciones() {
    var detalle = new Array();
    var htmltable = $("#tddatos_P")[0];
    var contador = 0;
    for (var r = 1; r < htmltable.rows.length; r++) {
        if ($(htmltable.rows[r]).css("background-color") == rgbcolor) {
            var obj = GetCancelacionesObj(htmltable.rows[r]);
            if (obj != null) {
                detalle[contador] = obj;
                contador++;
            }
        }
    }
    return detalle;
}

function GetCancelacionesObj(row) {
    var obj = {};
    obj["dca_comprobante"] = parseInt($(row).data("comprobante").toString());   
    obj["dca_empresa"] = parseInt(empresasigned["emp_codigo"]);        
    obj["dca_transacc"] = parseInt($(row).data("dca_transacc").toString());    
    obj["dca_doctran"] = $(row).data("dca_doctran").toString();   
    obj["dca_pago"] = parseInt($(row).data("dca_pago").toString());    
    obj["dca_comprobante_can"] = parseInt($(row).data("dca_comprobante_can").toString());    
    obj["dca_secuencia"] = parseInt($(row).data("dca_secuencia").toString());
    obj["Fecha"] = row.cells[0].innerText;
    obj["Documento"] = row.cells[1].innerText;
    obj["Guia"] = row.cells[2].innerText;
    obj["Pago"] = row.cells[3].innerText;
    obj["Agencia"] = row.cells[4].innerText;
    obj["Cliente"] = row.cells[5].innerText;
    obj["Monto"] = parseFloat(row.cells[6].innerText);
    obj["Cancelado"] = parseFloat(row.cells[7].innerText);
    obj["Saldo"] = parseFloat(row.cells[8].innerText); 
    return obj;
}


function CallCancelacionesOK() {
    var objs = GetDetalleCancelaciones();
    SetCancelaciones(objs);
}
/////////////////////////////////////////


function SetCancelaciones(objs) {    

    for (var  i = 0;  i < objs.length;  i++) {

        var obj = objs[i];
        //var row = $("<tr ></tr>")
        var row = $("<tr data-comprobante='" + obj["dca_comprobante"] + "' data-dca_empresa='" + obj["dca_comprobante"] + "' data-dca_pago='" + obj["dca_pago"] + "' data-dca_transacc='" + obj["dca_transacc"] + "' data-dca_doctran='" + obj["dca_doctran"] + "' data-dca_comprobante_can='" + obj["dca_comprobante_can"] + "' data-dca_secuencia='" + obj["dca_secuencia"] + "' onclick='Mark(this);'></tr>");

        row.append("<td>" + obj["fecha"] + "</td>");
        row.append("<td>" + obj["factura"] + "</td>");
        row.append("<td>" + obj["guia"] + "</td>");
        row.append("<td>" + obj["cancelacion"] + "</td>");
        row.append("<td class='right' >" + obj["monto"] + "</td>");
        row.append("<td class='right' >" + obj["cancelado"] + "</td>");
        row.append("<td class='right' >" + obj["saldo"] + "</td>");
       
        $("#tddatos").find("> tbody").append(row);
    }
    CalculaTotales(); 
}


function QuitCancelaciones() {
    var htmltable = $("#tddatos")[0];
    var rows = $("#tddatos").find("> tbody > tr ");
    $("#tddatos").find("> tbody > tr ").each(function () {
        if ($(this).css("background-color") == rgbcolor)
            $(this).remove();        
    });
    CalculaTotales(); 

}


function CalculaTotales() {
    var htmltablerub = $("#tdrubros")[0];
    var totaling = 0;
    var totalegr = 0;

    for (var r = 0; r < htmltablerub.rows.length; r++) {
        var row = htmltablerub.rows[r];
        var ing = $(row.cells[1]).children("input");
        if (ing.length > 0) {
            totaling += ($.isNumeric(ing.val())) ? parseFloat(ing.val()) : 0;
        }
        var egr = $(row.cells[2]).children("input");
        if (egr.length > 0) {
            totalegr += ($.isNumeric(egr.val())) ? parseFloat(egr.val()) : 0;
        }

    }



    var htmltable = $("#tddatos")[0];
    var total = 0;
    var totalliq = 0;

    for (var r = 0; r < htmltable.rows.length; r++) {
        var c = htmltable.rows[r].cells.length - 1; //celda valor
        var valor = 0;
        valor = htmltable.rows[r].cells[c].innerText;
//        totalliq += ($.isNumeric(valor)) ? parseFloat(valor) : 0;

//        valor = 0;
//        valor = htmltable.rows[r].cells[c-1].innerText;
        total += ($.isNumeric(valor)) ? parseFloat(valor) : 0;

    }
    $("#txtREGISTROS").val(htmltable.rows.length - 1);

    $("#txtTOTAL").val(CurrencyFormatted(total));
    $("#txtINGRESOS").val(CurrencyFormatted(totaling));
    $("#txtEGRESOS").val(CurrencyFormatted(totalegr));
    $("#txtTOTALCOM").val(CurrencyFormatted(total -totalegr+ totaling));
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
            var jsonText = JSON.stringify({ objeto: obj });

            $("#save").attr("disabled", true);
            saving = true;

            CallServer(formname + "/SaveObject", jsonText, 5);
        }
    }
}


function PrintObj() {
    //var url = "wfPlanillaSocioPrint.aspx?codigocomp=" + $("#txtcodigocomp").val()
    //var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=yes,scrollbars=yes";
    //window.open(url, "Planilla Socio", feautures);
    var opciones = "toolbar=no, scrollbars=yes, resizable=yes, top=50, left=100, width=800, height=600";
    window.open("wfPlanillaSocioPrint.aspx?codigocomp=" + $("#txtcodigocomp").val()+"&empresa=" + parseInt(empresasigned["emp_codigo"]),"PlanillaSocio", opciones);
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

$(window).keyup(function (event) {
    var id = document.activeElement.id;
    var code = event.keyCode
    var tabla = document.activeElement.parentNode.parentNode.parentNode.parentNode.id;
    if (tabla == "tdrubros")
        CalculaTotales();

});
