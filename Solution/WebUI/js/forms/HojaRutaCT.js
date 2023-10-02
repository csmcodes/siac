//Archivo:          HojaRuta.js
//Descripción:      Contiene las funciones comunes para la interfaz de hoja de ruta
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Noviembre 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfHojaRutaCT.aspx";
var menuoption = "HojaRutaCT";

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

    $("#invo").css({ 'display': 'none' });

    var codigocomp = $("#txtcodigocomp").val();
    if (codigocomp <= 0) {
        $("#print").css({ 'display': 'none' });
        $("#close").css({ 'display': 'none' });
    }
    //$("#print").on("click", { titulo: "ImpHRut", parametros: "parameter1=" + codigocomp, reporte: 'RUT' }, PrintReport);
    $("#print").on("click", PrintObj);
    $("#printdet").on("click", PrintDetObj);
    $("#printtic").on("click", PrintTicObj);
    LoadCabecera();


}


function PrintObj() {


    var opciones = "toolbar=no, scrollbars=no, resizable=yes, top=50, left=100, width=800, height=600";
    window.open("wfHojaRutaCTPrint.aspx?codigocomp=" + $("#txtcodigocomp").val(), "Imp", opciones);

    //window.location = "wfComprobantePrint.aspx?codigocomp=" +$("#txtcodigocomp").val();
}

function PrintDetObj() {
    var empresa = parseInt(empresasigned["emp_codigo"]);
    var idusuario = usuariosigned["usr_id"];// "admin";
    var opciones = "toolbar=no, scrollbars=no, resizable=yes, top=50, left=100, width=800, height=600";

    //window.location = "wfHojaRutaPrint.aspx?empresa=" + empresa +"&usuario="+idusuario+"&codigocomp=" + $("#txtcodigocomp").val() + "&detalle=1";
    window.open("wfHojaRutaPrint.aspx?empresa=" + empresa + "&usuario=" + idusuario + "&codigocomp=" + $("#txtcodigocomp").val() + "&detalle=1", "ImpHRDET", opciones);



}


function PrintTicObj() {
    var empresa = parseInt(empresasigned["emp_codigo"]);
    var idusuario = usuariosigned["usr_id"];// "admin";
    var opciones = "toolbar=no, scrollbars=no, resizable=yes, top=50, left=100, width=800, height=600";

    window.open("wfHojaRutaPrint.aspx?empresa=" + empresa + "&usuario=" + idusuario + "&codigocomp=" + $("#txtcodigocomp").val() + "&detalle=2", "ImpHRDET", opciones);



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
                CallEnviosResult(data);
            if (retorno == 4)
                CallHojasResult(data);
            if (retorno == 5)
                SaveObjResult(data);
            if (retorno == 6)
                GetDatosHojaRutaResult(data);
            if (retorno == 7)
                MoveObjResult(data);
            if (retorno == 11)
                GetTotalesResult(data);
            if (retorno == 12)
                GetPorcentajeResult(data);
            if (retorno == 13)
                LoadDetalleEnviosResult(data);
            if (retorno == 14)
                CallEnviosNumResult(data);
            if (retorno == 15)
                GetAllRutasResult(data);
            if (retorno == 16)
                ActDetalleResult(data);


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
    obj["com_almacen"] = parseInt($("#txtCODALMACEN").val());
    obj["com_pventa"] = parseInt($("#txtCODPVENTA").val());
    obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
    var jsonText = JSON.stringify({ objeto: obj });

    if ($("#comcabeceracontent").length > 0) {
        CallServer(formname + "/GetCabecera", jsonText, 0);
    }
}

function LoadCabeceraResult(data) {
    if (data != "") {
        $('#comcabeceracontent').html(data.d);

    }
    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha
    // Select with Search
    $(".chzn-select").chosen();
    // tabbed widget
    $(".tabbedwidget").tabs();
    SetForm(); //Depende de cada js.
    LoadDetalle();
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
    LoadPie();
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
        CalculaTotales();
    }
}


function GetAllRutas() {
    var obj = {};
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetAllRutas", jsonText, 15);
}

function GetAllRutasResult(data) {
    if (data != "") {
        $("#cmbRUTA").replaceWith(data.d);
    }
}


function SetForm() {
    //Tratamiento para los elementos de la cabecera
    SetAutocompleteById("txtIDRUT");
    SetAutocompleteById("txtIDVEH");
    SetAutocompleteById("txtIDCHO");

    if ($("#txtESTADO").val() == $("#txtCERRADO").val()) {
        $("#save").css({ 'display': 'none' });
        $("#close").css({ 'display': 'none' });
        $("#adddetnum").css({ 'display': 'none' });
        $("#adddet").css({ 'display': 'none' });
        $("#deldet").css({ 'display': 'none' });
        $("#movedet").css({ 'display': 'none' })
        $("#actdet").css({ 'display': 'none' });
    }
    //    $("#txtIDRUT").on("change", GetPorcentaje);
}

function GetPorcentaje() {

    var obj = GetComprobanteObj();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetPorcentaje", jsonText, 12);
}



function GetPorcentajeResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        $("#txtPORCENTAJE").val(obj["rut_porcentaje"]);
        $("#txtPORCENTAJEVISUAL").val("PORCENTAJE   %" + CurrencyFormatted(obj["rut_porcentaje"]) + ":");
        CalculaTotales();
    }
}


function SetFormDetalle() {
    //Tratamiento para los elemntos del detalle        
    $("#adddet").on("click", CallEnvios);
    $("#deldet").on("click", QuitEnvios);
    $("#movedet").on("click", CallHojas);
    $("#actdet").on("click", ActDetalle);

    $("#alldet").on("click", { tabla: "tddatos" }, SelectAllRows);
    $("#nonedet").on("click", { tabla: "tddatos" }, CleanSelectedRows);
    if ($("#txtESTADO").val() == $("#txtCERRADO").val()) {
        var inputs = $('input, textarea, select,button');
        $(inputs).each(function () {
            $(this).prop("disabled", true);

        });
    }
}


function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtIDRUT") {
        return {
            label: item.rut_id + "," + item.rut_nombre,
            value: item.rut_id,
            info: item
        }
    }
    if (idobj == "txtIDVEH") {
        return {
            label: item.veh_nombre + "," + item.veh_disco + "," + item.veh_placa + "," + item.veh_nombreduenio + "," + item.veh_apellidoduenio,
            value: item.veh_id,
            info: item
        }

    }



    if (idobj == "txtIDCHO") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_id,//item.per_apellidos + " " + item.per_nombres,
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
}


function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtIDVEH") {
        $("#txtPLACA").val("Placa: " + item.info.veh_placa + ", Disco:" + item.info.veh_disco);
        $("#txtCODVEHICULO").val(item.info.veh_codigo);
        $("#txtSOCIO").val(item.info.veh_nombreduenio + " " + item.info.veh_apellidoduenio);
        $("#txtIDSOC").val(item.info.veh_idduenio);
        $("#txtCODSOCIO").val(item.info.veh_duenio);
        $("#txtIDCHO").val(item.info.veh_idchofer1);
        $("#txtCHOFER").val(item.info.veh_nombrechofer1 + " " + item.info.veh_apellidochofer1);
        $("#txtCODCHOFER").val(item.info.veh_chofer1);
        $("#txtIDCHO2").val(item.info.veh_idchofer2);
        $("#txtCHOFER2").val(item.info.veh_nombrechofer2 + " " + item.info.veh_apellidochofer2);
        $("#txtCODCHOFER2").val(item.info.veh_chofer2);
    }

    if (idobj == "txtIDRUT") {
        $("#txtNOMBRERUT").val(item.info.rut_nombre + ", " + item.info.rut_origen + " - " + item.info.rut_destino);
        $("#txtCODRUTA").val(item.info.rut_codigo);
        GetPorcentaje();
    }

    if (idobj == "txtIDCHO") {

        //$("#txtIDCHO").val(item.per_id);
        $("#txtCHOFER").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtCODCHOFER").val(item.info.per_codigo);
        //alert(ui.item.label);
    }
    if (idobj == "txtCIRUC_P") {
        $("#txtCODIGO_P").val(item.info.per_codigo);
        $("#txtCIRUC_P").val(item.info.per_ciruc);
        $("#txtNOMBRES_P").val(item.info.per_nombres);
        $("#txtAPELLIDOS_P").val(item.info.per_apellidos);
        $("#txtRAZON_P").val(item.info.per_razon);
        $("#txtDIRECCION_P").val(item.info.per_direccion);
        $("#txtTELEFONO_P").val(item.info.per_telefono);
        avilible_save();
    }
}

function GetTotales() {


    var obj = GetComprobanteObj();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetTotales", jsonText, 11);
    //}
}

function GetTotalesResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        $("#txtTOTALCOM").val(obj["total"]["tot_total"]);
        $("#txtTOTALDOMICILIO").val(obj["total"]["tot_transporte"]);
        $("#txtTOTALSEG").val(obj["total"]["tot_tseguro"]);
        $("#txtTOTALIMP").val(obj["total"]["tot_timpuesto"]);
        $("#txtTOTALBULTOS").val(obj["total"]["tot_cantidad"]);
        if (obj["total"]["poliValores"] != null)
            for (i = 0; i < obj["total"]["poliValores"].length; i++) {
                $("#" + obj["total"]["poliNombres"][i]).val(obj["total"]["poliValores"][i]);
            }
    }
}




function GetTotalObj() {
    var obj = {};
    obj["tot_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["tot_comprobante"] = $("#txtcodigocomp").val();
    obj["tot_tseguro"] = ($.isNumeric($("#txtTOTALSEG").val())) ? parseFloat($("#txtTOTALSEG").val()) : null;
    obj["tot_timpuesto"] = ($.isNumeric($("#txtTOTALIMP").val())) ? parseFloat($("#txtTOTALIMP").val()) : null;
    obj["tot_transporte"] = ($.isNumeric($("#txtTOTALDOMICILIO").val())) ? parseFloat($("#txtTOTALDOMICILIO").val()) : null;
    obj["tot_total"] = ($.isNumeric($("#txtTOTALCOM").val())) ? parseFloat($("#txtTOTALCOM").val()) : null;
    obj = SetAuditoria(obj);
    return obj;
}


function GetDetalle() {
    var detalle = new Array();
    var htmltable = $("#tddatos")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var obj = GetHojarutaObj(htmltable.rows[r]);
        if (obj != null)
            detalle[r] = obj;
    }
    return detalle;
}

function GetHojarutaObj(row) {

    if ($(row).data("comprobante")) {
        var obj = {};
        obj["rfac_comprobantefac"] = parseInt($(row).data("comprobante").toString());
        obj["rfac_comprobantefac_key"] = parseInt($(row).data("comprobante").toString());
        obj["rfac_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["rfac_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
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
    obj["com_codclipro"] = parseInt($("#txtCODSOCIO").val());
    obj["com_agente"] = parseInt($("#txtCODVEN").val());
    obj["com_concepto"] = $("#txtCONCEPTO").val();
    obj["rutafactura"] = GetDetalle();
    obj["total"] = GetTotalObj();
    //obj["com_ruta"] = parseInt($("#txtCODRUTA").val());
    obj["com_ruta"] = parseInt($("#cmbRUTA").val());
    obj["com_vehiculo"] = parseInt($("#txtCODVEHICULO").val());

    obj["veh_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["veh_codigo"] = parseInt($("#txtCODVEHICULO").val());
    obj["veh_chofer1"] = parseInt($("#txtCODCHOFER").val());

    //obj["per_nombres"] = $("#txtCHOFER").val();

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


////FUNCIONES PARA LLAMAR POPUP DE BUSQUEDA DE COMPROBANTES

function CallEnvios() {
    var obj = GetComprobanteObj();
    CallBuscaComprobantes(obj);
}


/////////////////////////////////////////

///////////FUNCIONES PARA MOVER DETALLES///////////////


function CallHojas() {
    var obj = GetComprobanteObj();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetHojasRuta", jsonText, 4);
}


function CallHojasResult(data) {
    if (data != "") {
        CallPopUp2("modHojas", "Hojas de Ruta Disponibles", data.d);
        SetFormHojas();
    }
}


function SetFormHojas() {
    $("#cmbHOJARUTA_P").on("change", GetDatosHojaRuta);
}

function GetDatosHojaRuta() {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = $("#cmbHOJARUTA_P").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDatosHojaRuta", jsonText, 6);
}


function GetDatosHojaRutaResult(data) {
    $("#txtFECHARUTA_P").val("");
    $("#txtIDVEH_P").val("");
    $("#txtCODVEH_P").val("");
    $("#txtVEHICULO_P").val("");
    $("#txtPLACAVEH_P").val("");
    $("#txtDISCOVEH_P").val("");

    $("#txtIDSOC_P").val("");
    $("#txtCODSOC_P").val("");
    $("#txtSOCIO_P").val("");
    $("#txtIDCHO_P").val("");
    $("#txtCODCHO_P").val("");
    $("#txtCHOFER_P").val("");
    if (data != "") {
        if (data.d != "") {
            var obj = $.parseJSON(data.d);
            $("#txtFECHARUTA_P").val(GetDateValue(obj["fechacabecera"]));
            $("#txtCODVEH_P").val(obj["codigovehiculo"]);
            $("#txtIDVEH_P").val(obj["idvehiculo"]);
            $("#txtVEHICULO_P").val("Placa: " + obj["placavehivulo"] + " / Disco: " + obj["discovehiculo"]);
            $("#txtPLACAVEH_P").val(obj["placavehivulo"]);
            $("#txtDISCOVEH_P").val(obj["discovehiculo"]);

            $("#txtCODSOC_P").val(obj["codigosocio"]);
            $("#txtIDSOC_P").val(obj["idsocio"]);
            $("#txtSOCIO_P").val(obj["apellidosocio"] + " " + obj["nombresocio"]);
            $("#txtCODCHO_P").val(obj["codigochofer"]);
            $("#txtIDCHO_P").val(obj["idchofer"]);
            $("#txtCHOFER_P").val(obj["apellidochofer"] + " " + obj["nombrechofer"]);

        }
    }
}

function SetHojaRutaDestino(hoja) {
    var detalle = new Array();
    var htmltable = $("#tddatos")[0];
    var rows = $("#tddatos").find("> tbody > tr ");
    var r = 0;
    $("#tddatos").find("> tbody > tr ").each(function () {
        if ($(this).css("background-color") == rgbcolor) {
            var objd = {};
            objd["rfac_comprobanteruta"] = parseInt(hoja.toString());
            objd["rfac_comprobanteruta_key"] = parseInt(hoja.toString());
            objd["rfac_comprobantefac"] = parseInt($(this).data("comprobante").toString());
            objd["rfac_comprobantefac_key"] = parseInt($(this).data("comprobante").toString());
            objd["rfac_empresa"] = parseInt(empresasigned["emp_codigo"]);
            objd["rfac_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
            detalle[r] = objd;
            r++;
        }
    });

    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = parseInt(hoja);
    //obj["com_ruta"] = parseInt($("#txtCODRUTA").val());
    obj["com_ruta"] = parseInt($("#cmbRUTA").val());
    obj["rutafactura"] = detalle;

    var o = {};
    o["origen"] = GetComprobanteObj();
    o["destino"] = obj;

    var jsonText = JSON.stringify({ objeto: o });
    CallServer(formname + "/MoveObject", jsonText, 7);
}

function MoveObjResult(data) {
    if (data != "") {
        if (data.d == "OK") {
            jQuery.alerts.dialogClass = 'alert-success';
            jAlert('Comprobantes movidos exitosamente...', 'Éxito', function () {
                jQuery.alerts.dialogClass = null; // reset to default                
                LoadDetalle();
                LoadPie();
            });

        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al mover los comprobantes...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}



/////////////////////////////////////////////////////


function SetEnvios(objs) {

    for (var i = 0; i < objs.length; i++) {

        var obj = objs[i];
        //var row = $("<tr ></tr>")
        var row = $("<tr data-comprobante='" + obj["comprobante"] + "' onclick='Mark(this);'></tr>");
        row.append("<td>" + obj["doctran"] + "</td>");
        row.append("<td>" + obj["remitente"] + "</td>");
        row.append("<td>" + obj["destinatario"] + "</td>");
        row.append("<td class='right' >" + obj["valor"] + "</td>");
        $("#tddatos").find("> tbody").append(row);
    }
    CalculaTotales();
}


function QuitEnvios() {
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
    var imp = parseFloat($("#txtTOTALIMP").val());
    var seg = parseFloat($("#txtTOTALSEG").val());
    var dom = parseFloat($("#txtTOTALDOMICILIO").val());

    for (var r = 0; r < htmltable.rows.length; r++) {
        var c = htmltable.rows[r].cells.length - 1; //celda valor
        var valor = 0;
        valor = $(htmltable.rows[r].cells[c]).text();
        total += ($.isNumeric(valor)) ? parseFloat(valor) : 0;

    }
    $("#txtREGISTROS").val(htmltable.rows.length - 1);
    var porcentaje = parseFloat($("#txtPORCENTAJE").val());
    $("#txtPORCENTAJEVALOR").val(CurrencyFormatted(porcentaje * (total - imp - seg - dom) / 100));

    $("#txtTOTALCOM").val(CurrencyFormatted(total));
    GetTotales();
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


var actualizado = false;

function ActDetalle() {
    var obj = GetComprobanteObj();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDetalleAct", jsonText, 16);


}

function ActDetalleResult(data) {
    if (data != "") {
        $('#tddatos').append(data.d);
    }
    CalculaTotales();
    actualizado = true;
    SaveObj();
}

function SaveObj() {
    if (!saving) {
        if (ValidateForm()) {
            //        if (!actualizado) {
            //            ActDetalle();           
            //        }
            //        else {
            actualizado = false;
            var obj = GetComprobanteObj();
            if ($(this)[0].id == "save")
                obj["com_estado"] = parseInt($("#txtCREADO").val());
            else if ($(this)[0].id == "close")
                obj["com_estado"] = parseInt($("#txtCERRADO").val());
            var jsonText = JSON.stringify({ objeto: obj });
            $("#save").attr("disabled", true);
            saving = true;
            CallServer(formname + "/SaveObject", jsonText, 5);
            //}
        }
    }
}


function PrintObj() {
    window.location = "wfHojaRutaCTPrint.aspx?codigocomp=" + $("#txtcodigocomp").val();



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

function CleanPersona(id) {
    if (id == "btncleancho") {
        $("#txtIDCHO").val("");
        $("#txtCHOFER").val("");
        $("#txtCODCHOFER").val("");
    }
}


function GetCallPersona(obj, id) {


    if (id == "btncallcho") {

        $("#txtIDCHO").val(obj.per_id);
        $("#txtCHOFER").val(obj.per_apellidos + " " + obj.per_nombres);
        $("#txtCODCHOFER").val(obj.per_codigo);


        //$("#txtCHOFER").val(obj.per_apellidos + " " + obj.per_nombres);


    }
}