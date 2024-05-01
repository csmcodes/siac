//Archivo:          Comprobante.js
//Descripción:      Contiene las funciones comunes para la interfaz de gestion de comprobantes (Facturas, Guias, Notas de credito, etc)
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Septiembre 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfRetencion.aspx";
var menuoption = "Retencion";

var recibocreated = false;
var objdrecibo = null;
var removing = false;
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
    $("#print").on("click", PrintObj);
  //  $("#close").on("click", SaveObj);
    $("#contabilizacion").on("click", ContObj);
    $("#close").on("click", CloseObj);

    

    var codigocomp = $("#txtcodigocomp").val();
    if (codigocomp <= 0) {
        $("#contabilizacion").css({ 'display': 'none' });
        $("#print").css({ 'display': 'none' });
        $("#close").css({ 'display': 'none' });
        
    }

    $("#invo").css({ 'display': 'none' });

    LoadCabecera();
    GetPrintVersion();

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
            if (retorno == 13)
                GetPrintVersionResult(data);
            if (retorno == "ELEC")
                GenerarElectronicoResult(data);

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
    else if (origen == "OBL") { //Desde Obligacion
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = $("#txtcodigocompref").val();
        var jsonText = JSON.stringify({ objeto: obj });

        if ($("#comcabeceracontent").length > 0) {
            CallServer(formname + "/GetCabeceraFromOBL", jsonText, 0);
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
    else if (origen == "OBL") { //PLANILLA DE CLIENTES
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = $("#txtcodigocompref").val();
        var jsonText = JSON.stringify({ objeto: obj });
        CallServer(formname + "/GetDetalleFromOBL", jsonText, 1);
    }

}

function LoadDetalleResult(data) {
    if (data != "") {
        $('#comdetallecontent').html(data.d);
    }
    LoadPie();
    SetFormDetalle(); 
    //EditableRow("detalletabla");

}

function LoadPie() {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    var origen = $("#txtorigen").val();
    if (origen == "")
        obj["com_codigo"] = $("#txtcodigocomp").val();
    else if (origen == "OBL")  //PLANILLA DE CLIENTES
        obj["com_codigo"] = $("#txtcodigocompref").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetPie", jsonText, 2);

}

function LoadPieResult(data) {
    if (data != "") {
        $('#compiecontent').html(data.d);
    }

    CalculaTotales();
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
    SetAutocompleteById("txtIDREM");
    SetAutocompleteById("txtIDDES");
    SetAutocompleteById("txtCODVEH");
    SetAutocompleteById("txtIDLIS");
    SetAutocompleteById("txtIDPOL");

    $("#cmbRUTA").on("change", GetHojasRuta);

    if ($("#txtESTADOCOMP").val() == "PROCESO")
        $("#txtFECHACOMP").prop('disabled', '');



}


function GetHojasRuta() {

    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_ruta"] = $("#cmbRUTA").val();    
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetHojasRuta", jsonText, 6);
}
function GetHojasRutaResult(data) {
    if (data != "") {
        $("#cmbHOJARUTA").replaceWith(data.d);
        $("#cmbHOJARUTA").on("change", GetDatosHojaRuta);        
    }
}


function GetDatosHojaRuta() {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = $("#cmbHOJARUTA").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDatosHojaRuta", jsonText, 7);
}


function GetDatosHojaRutaResult(data) {
    $("#txtFECHARUTA").val("");
    $("#txtIDVEH").val("");
    $("#txtCODVEH").val("");
    $("#txtVEHICULO").val("");
    $("#txtPLACAVEH").val("");
    $("#txtDISCOVEH").val("");

    $("#txtIDSOC").val("");
    $("#txtCODSOC").val("");
    $("#txtSOCIO").val("");
    $("#txtIDCHO").val("");
    $("#txtCODCHO").val("");
    $("#txtCHOFER").val("");
    if (data != "") {
        if (data.d != "") {
            var obj = $.parseJSON(data.d);
            $("#txtFECHARUTA").val(GetDateValue(obj["fechacabecera"]));            
            $("#txtCODVEH").val(obj["codigovehiculo"]);
            $("#txtIDVEH").val(obj["idvehiculo"]);
            $("#txtVEHICULO").val("Placa: " + obj["placavehivulo"] + " / Disco: " + obj["discovehiculo"]);
            $("#txtPLACAVEH").val(obj["placavehivulo"]);
            $("#txtDISCOVEH").val(obj["discovehiculo"]);

            $("#txtCODSOC").val(obj["codigosocio"]);
            $("#txtIDSOC").val(obj["idsocio"]);
            $("#txtSOCIO").val(obj["apellidosocio"] + " " + obj["nombresocio"]);
            $("#txtCODCHO").val(obj["codigochofer"]);
            $("#txtIDCHO").val(obj["idchofer"]);
            $("#txtCHOFER").val(obj["apellidochofer"] + " " + obj["nombrechofer"]);

        }
    }
}




function SetFormDetalle() {
    SetAutocompleteById("txtIDIMP");
    //   $("#cmbCONCEPTO").on("change", GetPrice);
    if ($("#txtESTADO").val() == $("#txtCERRADO").val()) {
        var inputs = $('input, textarea, select');
        $(inputs).each(function () {
            $(this).prop("disabled", true);

        });  

    }
    
}

function RemoveRow(btn) {
    removing = true;

    var row = $(btn).parents('tr');    
    var hascontrols = ($(row).find("input").length > 0) ? true : false;
    if ($(row)[0].id != "editrow" && !hascontrols) {
        jConfirm('¿Está seguro que desea eliminar el registro?', 'Eliminar', function (r) {
            if (r) {
                $(btn).parents('tr').fadeOut(function () {
                    $(this).remove();
                    CalculaTotales();
                });
            }
            removing = false;
        });
    }
    else {
        CleanRow();
        removing = false;
    }
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
    $("#txtCODIMP").val("");
    $("#txtIDIMP").val("");
    $("#txtIMPUESTO").val("");
    $("#txtIMPCUENTA").val("");
    
    $("#txtOBSERVACION").val("");
    $("#txtPESO").val("");
    $("#cmbCONCEPTO").children('option').removeAttr('disabled');
    $("#txtCANTIDAD").val(0);
    $("#txtPRECIO").val(0);
    $("#txtDESC").val(0);
    $("#txtTOTAL").val(0);
    $("#chkIVA").prop("checked", false);
    $("#txtIDIMP").select();
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
    if (idobj == "txtCODCLIPRO" || idobj == "txtIDREM" || idobj == "txtIDDES" ) {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " +item.per_nombres+"-"+item.per_razon,
            value: item.per_id,
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
    if (idobj == "txtIDIMP") {
        return {
            label: item.imp_id+ "," + item.imp_nombre,
            value: item.imp_id,
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
}

function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCODCLIPRO") {
        $("#txtCODPER").val(item.info.per_codigo);
        $("#txtCIRUC").val(item.info.per_ciruc);
        $("#txtNOMBRES").val(item.info.per_apellidos+ " " + item.info.per_nombres);
        $("#txtRAZON").val(item.info.per_razon);
        $("#txtRUC").val(item.info.per_ciruc);
        $("#txtDIRECCION").val(item.info.per_direccion);
        $("#txtTELEFONO").val(item.info.per_telefono);

        $("#txtCODLIS").val(item.info.per_listaprecio);
        $("#txtIDLIS").val(item.info.per_listaid);
        $("#txtLISTA").val(item.info.per_listanombre);

        $("#txtCODPOL").val(item.info.per_politica);
        $("#txtIDPOL").val(item.info.per_politicaid);
        $("#txtPOLITICA").val(item.info.per_politicanombre);        
        $("#txtNROPAGOS").val(item.info.per_politicanropagos);
        $("#txtDIASPLAZO").val(item.info.per_politicadiasplazo);
        $("#txtPORCENTAJE").val(item.info.per_politicadesc);
        $("#txtPORCPAGOCON").val(item.info.per_politicaporpagocon);

        
        $("#txtCODVEN").val(item.info.per_agenteid);
        $("#txtVENDEDOR").val(item.info.per_agentenombre);

        //AUTOMATICAMENTE IGUALA EL CLIENTE CON EL REMITENTE
        $("#txtCODREM").val(item.info.per_codigo);
        $("#txtIDREM").val(item.info.per_id);
        $("#txtCIRUCREM").val(item.info.per_ciruc);
        $("#txtNOMBRESREM").val(item.info.per_apellidos + " " + item.info.per_nombres);        
        $("#txtDIRECCIONREM").val(item.info.per_direccion);
        $("#txtTELEFONOREM").val(item.info.per_telefono);

        //alert(ui.item.label);
    }
    if (idobj == "txtIDREM") {
        $("#txtCODREM").val(item.info.per_codigo);
        $("#txtCIRUCREM").val(item.info.per_ciruc);
        $("#txtNOMBRESREM").val(item.info.per_apellidos+" " +item.info.per_nombres);
        //$("#txtAPELLIDOSREM").val(item.info.per_apellidos);
        $("#txtDIRECCIONREM").val(item.info.per_direccion);
        $("#txtTELEFONOREM").val(item.info.per_telefono);
        //alert(ui.item.label);
    }
    if (idobj == "txtIDDES") {
        $("#txtCODDES").val(item.info.per_codigo);
        $("#txtCIRUCDES").val(item.info.per_ciruc);
        $("#txtNOMBRESDES").val(item.info.per_apellidos+" "+item.info.per_nombres);
       // $("#txtAPELLIDOSDES").val(item.info.per_apellidos);
        $("#txtDIRECCIONDES").val(item.info.per_direccion);
        $("#txtTELEFONODES").val(item.info.per_telefono);
        //alert(ui.item.label);
    }
    if (idobj == "txtCODVEH") {
        $("#txtPLACA").val(item.info.veh_placa);
        $("#txtDISCO").val(item.info.veh_disco);        
        //alert(ui.item.label);
    }
    if (idobj == "txtIDIMP") {
        $("#txtIDIMP").val(item.info.imp_id);
        $("#txtCODIMP").val(item.info.imp_codigo);
        $("#txtIMPUESTO").val(item.info.imp_nombre);
        $("#txtPORCTJRETENCION").val(item.info.imp_porcentaje);
        $("#txtIMPCUENTA").val(item.info.imp_cuenta);
      //  LoadProduct(); 
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
        avilible_save();
    }
    if (idobj == "txtIDTIPO_P") {
        $("#txtCODTIPO_P").val(item.info.tpa_codigo);
        $("#txtNOMBRETIPO_P").val(item.info.tpa_nombre);
        $("#txtIDTIPO_P").val(item.info.tpa_id);
        ShowCampos();
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
        $(htmltable.rows[5]).show(); //FECHA VEN
        $(htmltable.rows[7]).show(); //CTA
    }
    if (id == "TP003") {//TARJETA
        $(htmltable.rows[0]).show(); //EMISOR
        $(htmltable.rows[1]).show(); //NRO DOCUMENTO
        $(htmltable.rows[5]).show(); //FECHA VEN
        $(htmltable.rows[7]).show(); //CTA

    }
    if (id == "TP004") {//DEPOSITO
        $(htmltable.rows[3]).show(); //BANCO
        $(htmltable.rows[1]).show(); //NRO DOCUMENTO
        $(htmltable.rows[2]).show(); //NRO CUENTA
        $(htmltable.rows[5]).show(); //FECHA VEN


    }
}


function LoadProduct(item) {

    var id = $("#txtIDIMP").val();
    var jsonText = JSON.stringify({ id: id });
    CallServer(formname + "/GetProduct", jsonText, 3);
}

function LoadProductResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        $("#txtCODIMP").val(obj["imp_codigo"]);
        $("#txtIMPUESTO").val(obj["imp_nombre"]);
        $("#txtPESO").val(0);
        $("#txtCANTIDAD").val(1);
        $("#txtDESC").val(0);
        if (obj["factores"] != null) {
            var opciones = $("#cmbCONCEPTO").children("option");
            $("#cmbCONCEPTO").children('option').attr('disabled', 'disabled');
            //$("#cmbCONCEPTO").children('option').css('display', 'none');
            for (i = 0; i < obj["factores"].length; i++) {
                $("#cmbCONCEPTO option[value='" + obj["factores"][i]["fac_unidad"].toString() + "']").removeAttr("disabled");
                if (SetCheckValue(obj["factores"][i]["fac_default"])) {
                    $("#cmbCONCEPTO option[value='" + obj["factores"][i]["fac_unidad"].toString() + "']").attr('selected', 'selected');
                    $("#txtFACTOR").val(obj["factores"][i]["fac_factor"].toString());
                }
            }
            GetPrice();
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert("El producto no tiene factores asignados", 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });     
        }
        
        $("#chkIVA").prop("checked", SetCheckValue(obj["imp_iva"]));
        CalculaLinea();
        //AddEditRow();
        //CalculaLinea();   
                    
    }
}


function GetPrice() {

    var obj = {};
    obj["producto"] = $("#txtCODIMP").val();
    obj["lista"] = $("#txtCODLIS").val();
    obj["unidad"] = $("#cmbCONCEPTO").val();
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

function CalculaLinea() {
    var base = $("#txtPORCTJRETENCION").val();
    if (!$.isNumeric(base))
        base = 0;
    var porcen = $("#txtBASE").val();
    if (!$.isNumeric(porcen))
        porcen = 0;
    var total = parseFloat(porcen) * parseFloat(base)/100;
  

    $("#txtTOTAL").val(CurrencyFormatted(total));
    CalculaTotales(); 
}

function CalculaTotales() {
    var htmltable = $("#tdinvoice")[0];
    var subtotal0 = 0;
    var subtotalIVA = 0;
    var desc0 = 0;
    var descIVA = 0;

    for (var r = 0; r < htmltable.rows.length; r++) 
    {
        var c = htmltable.rows[r].cells.length - 4; //celda cantidad
        var hijoimp= $(htmltable.rows[r].cells[c]).children("input");
        var hijobas= $(htmltable.rows[r].cells[c + 1]).children("input");       
        var hijotot = $(htmltable.rows[r].cells[c + 2]).children("input");        
        var cant = 0;
        var prec = 0;       
        var valor = 0;           

        
        if (hijotot.length > 0) {
            cant = $(hijoimp).val();
            prec = $(hijobas).val();          
            valor = $(hijotot).val();              
        }
        else {
            cant = $(htmltable.rows[r].cells[c]).text();
            prec = $(htmltable.rows[r].cells[c + 1]).text();
            valor = $(htmltable.rows[r].cells[c + 2]).text();
        }
        subtotal0 += ($.isNumeric(valor)) ? parseFloat(valor): 0;        
    }
    var total = subtotal0;    
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
//    var newrow = $("<tr data-codpro='" + $("#txtCODIMP").val() + "', data-cueimp='" + $("#txtIMPCUENTA").val() + "' onclick='Edit(this);'></tr>");
    var newrow = $("<tr data-codpro='" + $("#txtCODIMP").val() + "' onclick='Edit(this);'></tr>");
    newrow.append("<td class='' data-cueimp='" + $("#txtIMPCUENTA").val() + "'>" + $("#txtIDIMP").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtIMPUESTO").val() + "</td>");
 //   newrow.append("<td class='' >" + $("#txtOBSERVACION").val() + "</td>");
 //   newrow.append("<td class='right' >" + $("#txtPESO").val() + "</td>");
    newrow.append("<td class='' data-concod='" + $("#cmbCONCEPTO").val() + "'>" + $("#cmbCONCEPTO option:selected").text() + "</td>");
   // newrow.append("<td class='center' >" + $("#txtCOMPROBANTE").val() + "</td>");
    newrow.append("<td class='right' >" + $("#txtPORCTJRETENCION").val() + "</td>");
    newrow.append("<td class='right' >" + $("#txtBASE").val() + "</td>");
    newrow.append("<td class='right' >" + $("#txtTOTAL").val() + "</td>");
    //newrow.append("<td class='center' >" + ($("#chkIVA").is(':checked') ? "SI" : "NO") + "</td>");
    newrow.append("<td class='center' ><div class='removablerow' onclick='RemoveRow(this)'><span class=\"icon-trash\" ></span></div></td>");


    //var newrow = $("<tr><td>Hola</td><td>chao</td><td>jaja</td><td>si</td><td>NO</td><td>BYE</td><td>7</td><td>8</td></tr>");
    var editrow = $("#tdinvoice").find("#editrow");
    newrow.insertBefore(editrow);

    $("#txtCODIMP").val("");
    $("#txtIDIMP").val("");
    $("#txtIMPUESTO").val("");
    $("#txtIMPCUENTA").val("");
    $("#txtOBSERVACION").val("");
    $("#cmbCONCEPTO").children('option').removeAttr('disabled');
    $("#txtCOMPROBANTE").val("");
    $("#txtPORCTJRETENCION").val(""); ;
    $("#txtBASE").val("");
    $("#txtTOTAL").val("");    
    $("#txtIDIMP").select();
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
    if (!removing) {
        CalculaLinea();
        var r = $("#txtCODIMP")[0].parentNode.parentNode;
        var codpro = $("#txtCODIMP").val();
        var cueimp = $("#txtIMPCUENTA").val();
        var idpro = $("#txtIDIMP").val();
        var producto = $("#txtIMPUESTO").val();

        var caract = "";

        var concod = $("#cmbCONCEPTO").val();
        var unidad = $("#cmbCONCEPTO option:selected").text();
        var comprobante = $("#txtCOMPROBANTE").val();
        var precio = $("#txtPORCTJRETENCION").val();
        var desc = $("#txtBASE").val();
        var total = $("#txtTOTAL").val();


        $("#txtCODIMP").val($(row).data("codpro").toString());
        $("#txtIDIMP").val($(row.cells[0]).text()); $(row.cells[0]).text("");
        $("#txtIMPUESTO").val($(row.cells[1]).text()); $(row.cells[1]).text("");
        $("#txtIMPCUENTA").val($(row.cells[0]).data("cueimp").toString());

        $("#cmbCONCEPTO option[value='" + $(row.cells[2]).data("concod") + "']").attr('selected', 'selected'); $(row.cells[2]).text("");

        //   $("#txtCOMPROBANTE").val(row.cells[3].innerText); row.cells[3].innerText = "";
        $("#txtPORCTJRETENCION").val($(row.cells[3]).text()); $(row.cells[3]).text("");
        $("#txtBASE").val($(row.cells[4]).text()); $(row.cells[4]).text("");
        $("#txtTOTAL").val($(row.cells[5]).text()); $(row.cells[5]).text("");

        $("#txtIDIMP").focus();

        MoveTo($("#txtIDIMP"), $(row.cells[0]));
        MoveTo($("#txtCODIMP"), $(row.cells[0]));
        MoveTo($("#txtIMPCUENTA"), $(row.cells[0]));
        MoveTo($("#txtIMPUESTO"), $(row.cells[1]));
        MoveTo($("#cmbCONCEPTO"), $(row.cells[2]));
        MoveTo($("#txtCOMPROBANTE"), $(row.cells[3]));
        MoveTo($("#txtPORCTJRETENCION"), $(row.cells[3]));
        MoveTo($("#txtBASE"), $(row.cells[4]));
        MoveTo($("#txtTOTAL"), $(row.cells[5]));





        $(r).data("codpro", codpro);

        $(r.cells[0]).data("cueimp", cueimp);
        $(r.cells[0]).text(idpro);
        $(r.cells[1]).text(producto);
        $(r.cells[2]).data("concod", concod);
        $(r.cells[2]).text(unidad);
        $(r.cells[3]).text(comprobante);
        $(r.cells[3]).text(precio);
        $(r.cells[4]).text(desc);
        $(r.cells[5]).text(total);


        $(r).attr('onclick', 'Edit(this)');
        $(row).removeAttr('onclick');

        $("#txtIDIMP").select();
    }
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

    obj["tot_impuesto"] = ($.isNumeric($("#txtCODIMP").val())) ? parseFloat($("#txtCODIMP").val()) : null; //Impuesto Venta
    //obj["tot_servicio"] = ??

 //   obj["tot_porc_desc"] = ($.isNumeric($("#txtPORCENTAJE").val()))?parseFloat($("#txtPORCENTAJE").val()):null;
 //   obj["tot_dias_plazo"] = ($.isNumeric($("#txtDIASPLAZO").val())) ? parseInt($("#txtDIASPLAZO").val()) : null;
//    obj["tot_nro_pagos"] = ($.isNumeric($("#txtNROPAGOS").val())) ? parseInt($("#txtNROPAGOS").val()) : null; 
    //obj["tot_transportista"] = ($.isNumeric($("#txtPORCENTAJE").val()))?
//    obj["tot_porc_impuesto"] =($.isNumeric($("#txtIVAPORCENTAJE").val()))?parseInt($("#txtIVAPORCENTAJE").val()):null;
    //obj["tot_porc_servicio"] =
   /* obj["tot_porc_seguro"] = ($.isNumeric($("#txtPORCSEGURO").val())) ? parseInt($("#txtPORCSEGURO").val()) : null; 
    obj["tot_subtotal"] = ($.isNumeric($("#txtSUBTOTALIVA").val()))?parseFloat($("#txtSUBTOTALIVA").val()):null;
    obj["tot_descuento1"] = ($.isNumeric($("#txtDESCIVA").val()))?parseFloat($("#txtDESCIVA").val()):null;
    obj["tot_descuento2"] =($.isNumeric($("#txtDESCUENTOIVA").val()))?parseFloat($("#txtDESCUENTOIVA").val()):null;
    obj["tot_subtot_0"] =($.isNumeric($("#txtSUBTOTAL0").val()))?parseFloat($("#txtSUBTOTAL0").val()):null;
    obj["tot_desc1_0"] =($.isNumeric($("#txtDESC0").val()))?parseFloat($("#txtDESC0").val()):null;
    obj["tot_desc2_0"] =($.isNumeric($("#txtDESCUENTO0").val()))?parseFloat($("#txtDESCUENTO0").val()):null;

    obj["tot_timpuesto"] = ($.isNumeric($("#txtIVA").val())) ? parseFloat($("#txtIVA").val()) : null; 
    //obj["tot_tservicio"] =
    obj["tot_tseguro"] = ($.isNumeric($("#txtSEGURO").val())) ? parseFloat($("#txtSEGURO").val()) : null; 
    obj["tot_transporte"] =($.isNumeric($("#txtTRANSPORTE").val()))?parseFloat($("#txtTRANSPORTE").val()):null;
    //obj["tot_ice"] =
*/    //obj["tot_financia"] =
    obj["tot_total"] = ($.isNumeric($("#txtTOTALCOM").val())) ? parseFloat($("#txtTOTALCOM").val()) : null;
    //obj["tot_anticipo"] =
    //obj["tot_paga"] =
//    obj["tot_vseguro"] = ($.isNumeric($("#txtVALORDECLARADO").val())) ? parseFloat($("#txtVALORDECLARADO").val()) : null;
    obj = SetAuditoria(obj);
    return obj;
}

function GetDetalle() {
    var detalle = new Array();
    var htmltable = $("#tdinvoice")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var obj = GetDretencionObj(htmltable.rows[r]);
        if (obj != null)
            detalle[r] = obj;
    }
    return detalle;
}

function GetDretencionObj(row) {
    var obj = {};
    obj["ddoc_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["ddoc_comprobante"] = $("#txtcodigocomp").val(); 

    var hijocodpro = $(row.cells[0]).children("input");
    if (hijocodpro.length > 0) {
        if ($.isNumeric($("#txtCODIMP").val())) {
            obj["drt_impuesto"] = parseInt($("#txtCODIMP").val());
            obj["drt_cuenta"] = parseInt($("#txtIMPCUENTA").val());
            obj["drt_valor"] = $("#txtBASE").val();
            obj["drt_porcentaje"] = $("#txtPORCTJRETENCION").val();
            obj["drt_total"] = $("#txtTOTAL").val();
            obj["drt_con_codigo"] = $("#cmbCONCEPTO").val();
            obj["drt_factura"] = $("#txtNUMCOMPROBANTE").val();
        }
        return null;
    }
    else {

        obj["drt_impuesto"] = parseInt($(row).data("codpro").toString());
        obj["drt_cuenta"] = parseInt($(row.cells[0]).data("cueimp"));
        obj["drt_valor"] = parseFloat($(row.cells[4]).text());
        obj["drt_porcentaje"] = parseFloat($(row.cells[3]).text());
        obj["drt_total"] = parseFloat($(row.cells[5]).text());
        obj["drt_con_codigo"] = parseInt($(row.cells[2]).data("concod"));
        obj["drt_factura"] = $("#txtNUMCOMPROBANTE").val();



    }
    obj = SetAuditoria(obj);
    return obj;
}


function GetCcomdocObj() {
    var obj = {};
    obj["cdoc_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cdoc_comprobante"] = $("#txtcodigocomp").val(); 

 //   obj["cdoc_politica"] = parseInt($("#txtCODPOL").val());
    obj["cdoc_listaprecio"] = parseInt($("#txtCODLIS").val());
    obj["cdoc_nombre"] = $("#txtNOMBRES").val();
    obj["cdoc_direccion"] = $("#txtDIRECCION").val();
    obj["cdoc_telefono"] = $("#txtTELEFONO").val();
    obj["cdoc_ced_ruc"] = $("#txtRUC").val();
    obj["cdoc_factura"] = $("#txtFACTURAOBL").val();
    obj["cdoc_aut_factura"] = $("#txtNUMCOMPROBANTE").val();
    obj["cdoc_formapago"] = $("#cmbTIPCOM").val();
    //   obj["detalle"] = GetDetalle(); 
    obj = SetAuditoria(obj);
    return obj;
}
/*

function GetDretencionObj() {
    var obj = {};
    obj["drt_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["drt_comprobante"] = $("#txtcodigocomp").val();
    obj["drt_impuesto"] = parseInt($("#txtCODIMP").val());    
    obj["drt_valor"] = $("#txtBASE").val();
    obj["drt_porcentaje"] = $("#txtPORCTJRETENCION").val();    
    obj["drt_total"] = $("#txtTOTAL").val();  
    obj["drt_con_codigo"] = $("#cmbCONCEPTO").val();
    obj["drt_factura"] = $("#txtCOMPROBANTE").val();

    obj["drt_cuenta_transacc"] = $("#txtRUC").val();
    obj["drt_cuenta"] = parseInt($("#txtCODLIS").val());
    obj["drt_debcre"] = $("#txtTELEFONO").val();
  //  obj["detalle"] = GetDetalle();
    return obj;
}

*/





function GetCcomenvObj() {
    var obj = {};
    obj["cenv_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cenv_comprobante"] = $("#txtcodigocomp").val();

    obj["cenv_empresa_rem"] = parseInt(empresasigned["emp_codigo"]);
    obj["cenv_remitente"] = parseInt($("#txtCODREM").val());
    obj["cenv_nombres_rem"] = $("#txtNOMBRESREM").val();
    obj["cenv_direccion_rem"] = $("#txtDIRECCIONREM").val();
    obj["cenv_telefono_rem"] = $("#txtTELEFONOREM").val();

    obj["cenv_empresa_des"] = parseInt(empresasigned["emp_codigo"]);
    obj["cenv_destinatario"] =parseInt($("#txtCODDES").val());
    obj["cenv_nombres_des"] = $("#txtNOMBRESDES").val();
    obj["cenv_direccion_des"] = $("#txtDIRECCIONDES").val();
    obj["cenv_telefono_des"] = $("#txtTELEFONODES").val();

    if ($("#txtCODSOC").val()!="")
        obj["cenv_socio"] = parseInt($("#txtCODSOC").val());
    if ($("#txtCODCHO").val() != "")
        obj["cenv_chofer"] = parseInt($("#txtCODCHO").val());
    if ($("#txtCODVEH").val() != "")
        obj["cenv_vehiculo"] = parseInt($("#txtCODVEH").val());


    obj["cenv_ruta"] = $("#cmbRUTA").val();
    obj["cenv_estado_ruta"] = 0; //0 estado de ruta no asignado, 1 asignado pero puede cambiar, 2 estado asignado definitivo
    obj["cenv_observacion"] = $("#txtENTREGADES").val();
    return obj;
}

function GetPlanillaCompObj() {
    var obj = {};
    //obj["pco_comprobante_fac"] =
    obj["pco_comprobante_pla"] = parseInt($("#txtcodigocompref").val());
    obj["pco_empresa"] = parseInt(empresasigned["emp_codigo"]);

    return obj;
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
    obj["com_concepto"] = $("#txtOBSERVACIONES").val();
    obj["com_pventa"] = pventa;
    obj["com_codclipro"] = parseInt($("#txtCODPER").val());
    obj["com_agente"] = parseInt($("#txtCODVEN").val());
    obj["retenciones"] = GetDetalle();
    obj["ccomdoc"] = GetCcomdocObj();
    obj["total"] = GetTotalObj();
    if ($("#txtorigen").val()=="LGC")
        obj["planillacomp"] = GetPlanillaCompObj();
    obj = SetAuditoria(obj);
    return obj;
    //var jsonText = JSON.stringify({ objeto: obj });
    //return jsonText;
}

function GetRutaxFacturaObj() {
    var obj = {};
    if ($("#cmbHOJARUTA").length > 0) {
        obj["rfac_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["rfac_comprobanteruta"] = $("#cmbHOJARUTA").val();
        obj["rfac_comprobantefac"] = 0;
        obj["rfac_estado"] = 1;
    }
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
            mensajehtml += "El campo <b>"+  $(this).attr('placeholder') + "</b> es obligatorio <br>";  
        }
    });

    
    var htmltable = $("#tdinvoice")[0];
    if (htmltable.rows.length < 3) {
        retorno = false;
        mensajehtml += "Es necesario ingresar al menos un detalle al comprobante<br>";
    }

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
        var valorpago = parseFloat($("#txtTOTALCOM").val()) * (porpagocon /100);
        CallRecibo(1, "admin", valorpago);
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

            var obj = {};
            obj["comprobante"] = GetComprobanteObj();

            obj["drecibo"] = objdrecibo;
            obj["rutaxfactura"] = GetRutaxFacturaObj();
            if ($(this)[0].id == "save")
                obj["comprobante"]["com_estado"] = parseInt($("#txtCREADO").val());
            else if ($(this)[0].id == "close")
                obj["comprobante"]["com_estado"] = parseInt($("#txtCERRADO").val());
            var jsonText = JSON.stringify({ objeto: obj });
            jConfirm('¿Está seguro que desea guardar la nota ? \n Luego no se podra modificar', 'Guardar', function (r) {
                if (r) {
                    $("#save").attr("disabled", true);
                    saving = true;
                    CallServer(formname + "/SaveObject", jsonText, 5);
                }
            });
        }
    }
}

function CloseObj() {
    var obj = {};
    obj["comprobante"] = GetComprobanteObj();
    //   obj["drecibo"] = objdrecibo;
    //  obj["rutaxfactura"] = GetRutaxFacturaObj();
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

    var printversion = $("#txtprintversion").val();
    var opciones = "toolbar=no, scrollbars=no, resizable=yes, top=50, left=100, width=800, height=600";
    if (printversion == "1") {//0 version reportviewer, 1: Version PDF itextsharp
        
        window.open("wfRetencionPrint.aspx?codigo=" + $("#txtcodigocomp").val() + "&usuario=" + usuariosigned["usr_id"] + "&empresa=" + empresasigned["emp_codigo"] + "&tipodoc=" + $("#txttipodoc").val(), "Imp", opciones);
    }
    else
        window.open("wfDretencionPrint.aspx?codigocomp=" + $("#txtcodigocomp").val() + "&usuario=" + usuariosigned["usr_id"] + "&empresa=" + empresasigned["emp_codigo"] + "&tipodoc=" + $("#txttipodoc").val(), "Imp", opciones);
        //window.location = "wfDretencionPrint.aspx?codigocomp=" + $("#txtcodigocomp").val();
}

function SaveObjResult(data) {

    $("#save").attr("disabled", false);
    saving = false;
    if (data != "") {

        var obj = $.parseJSON(data.d);


        if (obj[0] != "ERROR") {
        //if (data.d != "-1") {

            $("#txtcodigocomp").val(obj[1]["com_codigo"]);
            
            jQuery.alerts.dialogClass = 'alert-success';
            jAlert('Comprobante guardado exitosamente...', 'Éxito', function () {

                if (obj[1]["com_estado"] == 2)//MAYORIZADO
                {
                    jQuery.alerts.dialogClass = null; // reset to default
                    jConfirm('¿Desea generar electrónico este momento?', 'Pregunta', function (r) {
                        if (r) {
                            GenerarElectronico($("#txtcodigocomp").val());
                        }
                        else
                            window.location = formname + "?codigocomp=" + $("#txtcodigocomp").val();
                        //location.reload();
                    });
                }
                else
                    window.location = formname + "?codigocomp=" + $("#txtcodigocomp").val();


                
               
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
    var r = $("#txtCODIMP")[0].parentNode.parentNode;
    if ($(r).prev().length > 0)
        Edit($(r).prev()[0]);            
}

function RowDown() {
    var r = $("#txtCODIMP")[0].parentNode.parentNode;
    if ($(r).next().length > 0)
        Edit($(r).next()[0]);
    else {
        if ($("#txtCODIMP").val() != "")
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
    return $("#"+id).autocomplete('widget').is(':visible');
}



$(window).keydown(function (event) {
    var id = document.activeElement.id;
    var code = event.keyCode;
    //var char = event.char;
    var char = String.fromCharCode(event.which);
    //$("#mensaje").html(id + " " + code + " " + char);

    if (id == "txtIDIMP" || id == "txtOBSERVACION" || id == "txtBASE" || id == "txtDESC" || id == "txtPESO") {

        if (id == "txtIDIMP" && IsOpen(id))
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
            if (id != "txtIDIMP")
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
        var controlKeys = [8, 9, 13, 35, 36, 37, 39,110,190];
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
        case "txtCANTIDAD":
        case "txtBASE":
            CalculaLinea();
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

        $("#txtCODLIS").val(obj.per_listaprecio);
        $("#txtIDLIS").val(obj.per_listaid);
        $("#txtLISTA").val(obj.per_listanombre);

        $("#txtCODPOL").val(obj.per_politica);
        $("#txtIDPOL").val(obj.per_politicaid);
        $("#txtPOLITICA").val(obj.per_politicanombre);

        $("#txtPORCENTAJE").val(obj.per_politicadesc);
        $("#txtCODVEN").val(obj.per_agenteid);
        $("#txtVENDEDOR").val(obj.per_agentenombre);

        //AUTOMATICAMENTE IGUALA EL REMITENTE CON EL CLIENTE
        $("#txtIDREM").val(obj.per_id);
        $("#txtCODREM").val(obj.per_codigo);
        $("#txtNOMBRESREM").val(obj.per_apellidos + " " + obj.per_nombres);
        $("#txtDIRECCIONREM").val(obj.per_direccion);
        $("#txtTELEFONOREM").val(obj.per_telefono);
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

        $("#txtCODPOL").val("");
        $("#txtIDPOL").val("");
        $("#txtPOLITICA").val("");

        $("#txtPORCENTAJE").val("");
        $("#txtCODVEN").val("");
        $("#txtVENDEDOR").val("");
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



function GenerarElectronico(cod) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = cod;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer("ws/Metodos.asmx/GenerarElectronico", jsonText, "ELEC");

}

function GenerarElectronicoResult(data) {
    if (data != "") {
        if (data.d == "ok") {
            jQuery.alerts.dialogClass = 'alert-info';
            jAlert('Se ha generado y enviado el comprobante electrónico correctamente', 'Información', function () {
                jQuery.alerts.dialogClass = null; // reset to default
                window.location = formname + "?codigocomp=" + $("#txtcodigocomp").val();
            });
        }
        else {



            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al generar el comprobante de electrónico...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
       
    }
}