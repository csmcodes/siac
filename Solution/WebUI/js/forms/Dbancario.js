﻿ //Archivo:          Dnotacre.js
//Descripción:      Contiene las funciones comunes para la interfaz de gestion de Dnotacrees de deudas 
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Octubre 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfDbancario.aspx";
var menuoption = "Dbancario";

var afectacionobj = null;


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
    $("#contabilizacion").on("click", ContObj);
    $("#close").on("click", SaveObj);
    var codigocomp = $("#txtcodigocomp").val();


    if (codigocomp <= 0) {
        $("#contabilizacion").css({ 'display': 'none' });
        $("#print").css({ 'display': 'none' });
        $("#close").css({ 'display': 'none' });                
        $("#invo").css({ 'display': 'none' });
    }
    $("#despachar").css({ 'display': 'none' });
    $("#invo").css({ 'display': 'none' });
    $("#view").css({ 'display': 'none' });
    $("#view").on("click", ViewObj);
    
    LoadCabecera();
    LoadPie();
    LoadDiario();
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
                GetCuentaResult(data);
            if (retorno == 8)
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
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = $("#txtcodigocomp").val();
    obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
    obj["com_ctipocom"] = parseInt($("#cmbSIGLA_P").val());  //3 REC 

    obj["modify"] = $("#txtmodify").val();  //modificar
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
    obj["modify"] = $("#txtmodify").val();  //modificar
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDetalle", jsonText, 1);
}

function LoadDetalleResult(data) {
    if (data != "") {
        $('#comdetallecontent').html(data.d);
     //   LoadDetalleDiario();
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
    if ($("#txtESTADO").val() != $("#txtCREADO").val()) {
        $("#txtDESCUENTO0").prop("disabled", true);
        $("#txtDESCUENTOIVA").prop("disabled", true);
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
    SetAutocompleteById("txtCODCOM");
    if ($("#txtESTADO").val() == $("#txtCERRADO").val()) {
        if ($("#txtmodify").val()!="true")
            $("#save").css({ 'display': 'none' });
       
        $("#adddet").css({ 'display': 'none' });
        $("#deldet").css({ 'display': 'none' });
        $("#movedet").css({ 'display': 'none' });
    }
    
}

function SetFormDetalle() {
    SetAutocompleteById("txtIDBANCO");
    $("#txtDEBITO").on("change", CalculaTotales);
    $("#txtCREDITO").on("change", CalculaTotales);
    CalculaTotales();
    
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



function CleanRow() {
    $("#txtIDBANCO").val("");
    $("#txtCODTIPO").val("");
    $("#txtNOMBRETIPO").val("");
    $("#txtVALOR").val(0);
    $("#txtIDBANCO").select();
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

    if (idobj == "txtIDBANCO") {
        return {
            label: item.ban_id + "," + item.ban_nombre + "," + item.ban_cuenta,
            value: item.ban_id,
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
    if (idobj == "txtCODCOM") {
        return {
            label: item.com_doctran ,
            value: item.com_doctran,
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

        /*$("#txtCODLIS").val(item.info.per_listaprecio);
        $("#txtIDLIS").val(item.info.per_listaid);
        $("#txtLISTA").val(item.info.per_listanombre);

        $("#txtCODPOL").val(item.info.per_politica);
        $("#txtIDPOL").val(item.info.per_politicaid);
        $("#txtPOLITICA").val(item.info.per_politicanombre);
        $("#txtNROPAGOS").val(item.info.per_politicanropagos);
        $("#txtDIASPLAZO").val(item.info.per_politicadiasplazo);
        $("#txtPORCENTAJE").val(item.info.per_politicadesc);


        $("#txtCODVEN").val(item.info.per_agenteid);
        $("#txtVENDEDOR").val(item.info.per_agentenombre);

        //AUTOMATICAMENTE IGUALA EL CLIENTE CON EL REMITENTE
        $("#txtCODREM").val(item.info.per_codigo);
        $("#txtIDREM").val(item.info.per_id);
        $("#txtCIRUCREM").val(item.info.per_ciruc);
        $("#txtNOMBRESREM").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtDIRECCIONREM").val(item.info.per_direccion);
        $("#txtTELEFONOREM").val(item.info.per_telefono);*/

        //alert(ui.item.label);
    }

    if (idobj == "txtIDBANCO") {
        $("#txtCODBANCO").val(item.info.ban_codigo);
        $("#txtNOMBREBANCO").val(item.info.ban_nombre);
        $("#txtIDBANCO").val(item.info.ban_id);
        $("#txtCUENTA").val(item.info.ban_nombrecuenta);
       // ShowCampos();
    }
    if (idobj == "txtCODCOM") {
        $("#txtCODCOM").val(item.info.com_doctran);
        $("#txtCOMPROBANTE").val(item.info.com_codigo);      
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





/*********************FUNCIONES PARA CALCULO DE TOTALES **********************/


function CalculaTotales() {
    var htmltable = $("#tdinvoice")[0];
    var debito  = 0;
    var credito = 0;
    var desc0 = 0;
    var descIVA = 0;


    for (var r = 0; r < htmltable.rows.length; r++) {
        var c = htmltable.rows[r].cells.length - 3; //celda cantidad
       
        var hijodeb = $(htmltable.rows[r].cells[c ]).children("input");
        var hijocre = $(htmltable.rows[r].cells[c+1]).children("input");
        var cant = 0;
        var prec = 0;
        var desc = 0;
        var deb = 0;
        var cre = 0;
        var valor = 0;

        if (hijodeb.length > 0) {
            deb = $(hijodeb).val();           
        }
        else {
            deb = $(htmltable.rows[r].cells[c]).text();
        }

        if (hijocre.length > 0) {
            cre = $(hijocre).val();
        }
        else {
            cre = $(htmltable.rows[r].cells[c + 1]).text();
        }
        debito += ($.isNumeric(deb)) ? parseFloat(deb) : 0;

        credito += ($.isNumeric(cre)) ? parseFloat(cre) : 0;


        //descIVA += ($.isNumeric(desc)) ? ((iva) ? parseFloat(desc) : 0) : 0;

        //desc0 += ($.isNumeric(desc)) ? ((!iva) ? parseFloat(desc) : 0) : 0;

    }
    $("#txtTOTCREDITO").val(CurrencyFormatted(debito));
    $("#txtTOTDEBITO").val(CurrencyFormatted(credito));
}


/*********************FUNCIONES PARA AGREGAR y EDITAR UN DETALLE**********************/

function AddEditRow() {
    GetCuenta();
    //var newrow = $("<tr data-codcue='" + $("#txtCODBANCO").val() + "' data-beneficiario='" + $("#txtBENEFICIARIO").val() + "'  onclick='Edit(this);'></tr>");
    var newrow = $("<tr data-codcue='" + $("#txtCODBANCO").val() + "' onclick='Edit(this);'></tr>");
    //var newrow = $("<tr " + strdata + " onclick='Edit(this);'></tr>");

    newrow.append("<td class='' >" + $("#txtIDBANCO").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtNOMBREBANCO").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtCUENTA").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtDOCUMENTO").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtBENEFICIARIO").val() + "</td>");
    newrow.append("<td class='right' >" + $("#txtDEBITO").val() + "</td>");
     newrow.append("<td class='right' >" + $("#txtCREDITO").val() + "</td>");
    
    newrow.append("<td class='center' ><div class='removablerow' onclick='RemoveRow(this)'><span class=\"icon-trash\" ></span></div></td>");

    var editrow = $("#editrow");
    newrow.insertBefore(editrow);

    $("#txtCODBANCO").val("");
    $("#txtIDBANCO").val("");
    $("#txtNOMBREBANCO").val("");
    $("#txtCUENTA").val("");
   $("#txtNOMBREBANCO").val("");
    $("#txtDOCUMENTO").val("");
    $("#txtDEBITO").val(0);
    $("#txtCREDITO").val(0);
    $("#txtIDBANCO").select();
    $("#txtBENEFICIARIO").val("");
    
}


function MoveTo(control, destino) {
    var obj = $(control).detach();
    $(destino).append(obj);
}


/*function EditAfec(row) {
    var valor = parseFloat(row.cells[8].innerText);
    row.cells[8].innerText = "";
    var inputvalor = $("<input type='text' style='width:auto;' value='" + valor + "'/>");
    $(row.cells[8]).append(inputvalor);
}*/



function Edit(row) {
    GetCuenta();
    var r = $("#txtCODBANCO")[0].parentNode.parentNode;
    var cod = $("#txtCODBANCO").val();
    var id = $("#txtIDBANCO").val();
    var nombre = $("#txtNOMBREBANCO").val();
    var cuenta = $("#txtCUENTA").val();
    var documento = $("#txtDOCUMENTO").val();
    var debito = $("#txtDEBITO").val();
    var credito = $("#txtCREDITO").val();
    var beneficiario = $("#txtBENEFICIARIO").val();


    $("#txtCODBANCO").val($(row).data("codcue").toString());
    $("#txtIDBANCO").val($(row.cells[0]).text()); $(row.cells[0]).text("");
    $("#txtNOMBREBANCO").val($(row.cells[1]).text()); $(row.cells[1]).text("");
    $("#txtCUENTA").val($(row.cells[2]).text()); $(row.cells[2]).text("");
    $("#txtDOCUMENTO").val($(row.cells[3]).text()); $(row.cells[3]).text("");
    $("#txtBENEFICIARIO").val($(row.cells[4]).text()); $(row.cells[4]).text("");
    $("#txtDEBITO").val($(row.cells[5]).text()); $(row.cells[5]).text("");
    $("#txtCREDITO").val($(row.cells[6]).text()); $(row.cells[6]).text("");
        
    
    $("#txtIDBANCO").focus();




    MoveTo($("#txtIDBANCO"), $(row.cells[0]));
    MoveTo($("#txtCODBANCO"), $(row.cells[0]));
    MoveTo($("#txtNOMBREBANCO"), $(row.cells[1]));
    MoveTo($("#txtCUENTA"), $(row.cells[2]));
    MoveTo($("#txtDOCUMENTO"), $(row.cells[3]));
    MoveTo($("#txtBENEFICIARIO"), $(row.cells[4]));
    MoveTo($("#txtDEBITO"), $(row.cells[5]));
    MoveTo($("#txtCREDITO"), $(row.cells[6]));
    //MoveTo($("#txtCREDITO"), $(row.cells[5]));
 
    //$(r).data("beneficiario", beneficiario);
    $(r).data("codcue", cod);
    $(r.cells[0]).text(id);
    $(r.cells[1]).text(nombre);
    $(r.cells[2]).text(cuenta);
    $(r.cells[3]).text(documento);
    $(r.cells[4]).text(beneficiario);
    $(r.cells[5]).text(debito);
    $(r.cells[6]).text(credito);
    $(r).attr('onclick', 'Edit(this)');
    $(row).removeAttr('onclick');
    $("#txtIDBANCO").select();
   // ShowCampos();

}

function ShowCampos() {
    var htmltable = $("#tddatos")[0];
    var total = 0;

    for (var r = 0; r < htmltable.rows.length; r++) {
        $(htmltable.rows[r]).hide();
    }


    var id = $("#txtIDBANCO").val();
    if (id == "TP001") {//EFECTIVO
        $(htmltable.rows[7]).show(); //CTA
    }
    if (id == "TP002") {//CHEQUE
        $(htmltable.rows[0]).show();//EMISOR
        $(htmltable.rows[2]).show(); //NRO CUENTA
        $(htmltable.rows[6]).show(); //FECHA VEN
        $(htmltable.rows[7]).show();//CTA
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



/***********************SAVE FUNCTIONS ***********************************/

function GetTotalObj() {
    var obj = {};
    obj["tot_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["tot_comprobante"] = $("#txtcodigocomp").val();

    //obj["tot_impuesto"] = 8; //Impuesto Venta    

    obj["tot_porc_desc"] = ($.isNumeric($("#txtPORCENTAJE").val())) ? parseFloat($("#txtPORCENTAJE").val()) : null;
    obj["tot_dias_plazo"] = ($.isNumeric($("#txtDIASPLAZO").val())) ? parseInt($("#txtDIASPLAZO").val()) : null;
    obj["tot_nro_pagos"] = ($.isNumeric($("#txtNROPAGOS").val())) ? parseInt($("#txtNROPAGOS").val()) : null;
    obj["tot_porc_impuesto"] = ($.isNumeric($("#txtIVAPORCENTAJE").val())) ? parseInt($("#txtIVAPORCENTAJE").val()) : null;
    obj["tot_subtotal"] = ($.isNumeric($("#txtSUBTOTALIVA").val())) ? parseFloat($("#txtSUBTOTALIVA").val()) : null;
    obj["tot_descuento1"] = ($.isNumeric($("#txtDESCIVA").val())) ? parseFloat($("#txtDESCIVA").val()) : null;
    obj["tot_descuento2"] = ($.isNumeric($("#txtDESCUENTOIVA").val())) ? parseFloat($("#txtDESCUENTOIVA").val()) : null;
    obj["tot_subtot_0"] = ($.isNumeric($("#txtSUBTOTAL0").val())) ? parseFloat($("#txtSUBTOTAL0").val()) : null;
    obj["tot_desc1_0"] = ($.isNumeric($("#txtDESC0").val())) ? parseFloat($("#txtDESC0").val()) : null;
    obj["tot_desc2_0"] = ($.isNumeric($("#txtDESCUENTO0").val())) ? parseFloat($("#txtDESCUENTO0").val()) : null;

    obj["tot_timpuesto"] = ($.isNumeric($("#txtIVA").val())) ? parseFloat($("#txtIVA").val()) : null;
    //obj["tot_tservicio"] =
    obj["tot_total"] = ($.isNumeric($("#txtTOTALCOM").val())) ? parseFloat($("#txtTOTALCOM").val()) : null;

    return obj;
}

function GetDetalle() {
    var detalle = new Array();
    var htmltable = $("#tdinvoice")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var obj = GetDnotacreObj(htmltable.rows[r]);
        if (obj != null)
            detalle[r] = obj;
    }
    return detalle;
}

function GetDnotacreObj(row) {
    var obj = {};
    obj["dnc_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["dnc_comprobante"] = $("#txtcodigocomp").val();

    var hijocodpro = $(row.cells[0]).children("input");
    if (hijocodpro.length > 0) {
        if ($.isNumeric($("#txtCODTIPO").val())) {
            obj["dnc_tiponc"] = parseInt($("#txtCODTIPO").val());
            obj["dfp_tipopagoid"] = $("#txtIDBANCO").val();
            obj["dfp_tipopagonombre"] = $("#txtNOMBRETIPO").val();
            obj["dnc_valor"] = parseFloat($("#txtVALOR").val());
            obj["dnc_cheque"] = $("#chkIVA").is(':checked') ? 1 : 0;

        }
        else
            return null;
    }
    else {
        if ($.isNumeric($(row).data("codtipo").toString())) {
            obj["dnc_tiponc"] = parseInt($(row).data("codtipo").toString());
            obj["dfp_tipopagoid"] = $(row.cells[0]).text();
            obj["dfp_tipopagonombre"] = $(row.cells[1]).text();
            obj["dnc_valor"] = parseFloat($(row.cells[2]).text());
            obj["dnc_cheque"] = (($(row.cells[3]).text() == "SI") ? 1 : 0);
        }
        else
            return null;

      
    }

  
    return obj;
}



function GetCcomdocObj() {
    var obj = {};
    obj["cdoc_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cdoc_comprobante"] = $("#txtcodigocomp").val();

    //   obj["cdoc_politica"] = parseInt($("#txtCODPOL").val());
 //   obj["cdoc_listaprecio"] = parseInt($("#txtCODLIS").val());
    obj["cdoc_nombre"] = $("#txtNOMBRES").val();
    obj["cdoc_direccion"] = $("#txtDIRECCION").val();
    obj["cdoc_telefono"] = $("#txtTELEFONO").val();
    obj["cdoc_ced_ruc"] = $("#txtRUC").val();
    obj["cdoc_factura"] = $("#txtCOMPROBANTE").val();
    //   obj["detalle"] = GetDetalle(); 
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
    obj["com_pventa"] = pventa;
    obj["com_concepto"] = $("#txtCONCEPTO").val();
    obj["com_centro"] = $("#txtCODCEN").val();
    //obj["com_codclipro"] = parseInt($("#txtCODPER").val());
    obj["bancario"] = GetDetalleBancario();
    obj["contables"] = GetDetalleDiario();
    obj = SetAuditoria(obj);
    return obj;
}
function GetDetalleBancario() {
    var detalle = new Array();
    var htmltable = $("#tdinvoice")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var obj = GetDBancarioObj(htmltable.rows[r]);
        if (obj != null)
            detalle[r] = obj;
    }
    return detalle;
}


function GetDBancarioObj(row) {
    var obj = {};
    obj["dban_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["dban_cco_comproba"] = $("#txtcodigocomp").val();
    var hijocodpro = $(row.cells[0]).children("input");
    if (hijocodpro.length > 0) {
        if ($.isNumeric($("#txtCODBANCO").val())) {
            obj["dban_banco"] = parseInt($("#txtCODBANCO").val());
            obj["dban_documento"] = $("#txtDOCUMENTO").val();       
          
            var deb = parseFloat($("#txtDEBITO").val());
            var cre = parseFloat($("#txtCREDITO").val());
            if (deb > 0) {
                obj["dban_valor_nac"] = deb;
                obj["dban_valor_ext"] = deb;
                obj["dban_debcre"] = cDebito;
            }
            if (cre > 0) {
                obj["dban_valor_nac"] = cre;
                obj["dban_valor_ext"] = cre;
                obj["dban_debcre"] = cCredito;
            }
            //obj["dco_tipo_cambio"] = ;
            obj["dban_beneficiario"] = $("#txtBENEFICIARIO").val();

            obj["dban_transacc"] = 1;
            obj["dban_conciliacion"] = 1;
            //obj["dco_agente"] = ;
          
 
            //obj["dco_producto"] = ;
            //obj["dco_bodega"] = ;
        }
        return null;
    }
    else {

        obj["dban_banco"] = parseInt($(row).data("codcue").toString());
        //obj["dco_centro"] = ;
        obj["dban_transacc"] = 1;
        obj["dban_conciliacion"] = 1;
        obj["dban_documento"] = $(row.cells[3]).text();
        var deb = parseFloat($(row.cells[5]).text());
        var cre = parseFloat($(row.cells[6]).text());
        if (deb > 0) {
            obj["dban_valor_nac"] = deb;
            obj["dban_valor_ext"] = deb;
            obj["dban_debcre"] = cDebito;
        }
        if (cre > 0) {
            obj["dban_valor_nac"] = cre;
            obj["dban_valor_ext"] = cre;
            obj["dban_debcre"] = cCredito;
        }
        //obj["dco_tipo_cambio"] = ;
        //obj["dco_concepto"] = ;
        //obj["dco_almacen"] = ;
        obj["dban_beneficiario"] = $(row.cells[4]).text();
        //if ($(row).data("beneficiario")) {
        
            //obj["dban_beneficiario"] = $(row).data("beneficiario").toString();
        //}
        //obj["dco_agente"] = ;
        //obj["dco_doctran"] = ;
        //obj["dco_nropago"] = ;
        //obj["dco_fecha_vence"] = ;
        //obj["dco_ddo_comproba"] = ;
        //obj["dco_ddo_transacc"] = ;
        //obj["dco_producto"] = ;
        //obj["dco_bodega"] = ;
        obj = SetAuditoria(obj);
    }
    return obj;
}

function GetCuenta() {
    if ($("#txtCODBANCO").val() != "") {
        var obj = {};
        obj["dban_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["dban_cco_comproba"] = $("#txtcodigocomp").val();
        obj["dban_banco"] = parseInt($("#txtCODBANCO").val());
        obj["dban_documento"] = $("#txtDOCUMENTO").val();

        var deb = parseFloat($("#txtDEBITO").val());
        var cre = parseFloat($("#txtCREDITO").val());
        if (deb > 0) {
            obj["dban_valor_nac"] = deb;
            obj["dban_valor_ext"] = deb;
            obj["dban_debcre"] = cDebito;
        }
        if (cre > 0) {
            obj["dban_valor_nac"] = cre;
            obj["dban_valor_ext"] = cre;
            obj["dban_debcre"] = cCredito;
        }
        //obj["dco_tipo_cambio"] = ;
        obj["dban_beneficiario"] = $("#txtBENEFICIARIO").val();

        obj["dban_transacc"] = 1;
        obj["dban_conciliacion"] = 1;
        //obj["dco_agente"] = ;


        //obj["dco_producto"] = ;
        //obj["dco_bodega"] = ;

        var jsonText = JSON.stringify({ objeto: obj });
        CallServer(formname + "/GetCuentas", jsonText, 6);

    }

    /*if ($("#txtCODBANCO").val() != "") {
        var obj = {};
        obj["ban_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["ban_codigo"] = parseInt($("#txtCODBANCO").val())
        obj["ban_debito"] = parseFloat($("#txtDEBITO").val())
        obj["ban_credito"] = parseFloat($("#txtCREDITO").val())
        var jsonText = JSON.stringify({ objeto: obj });
        CallServer(formname + "/GetCuentas", jsonText, 6);
    }*/
}

function GetCuentaResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);

        var strdata = "data-codcue='" + obj["dco_cuenta"] + "' " +
    "data-codper='" + obj["dco_cliente"] + "' " +
    "data-codmod='" + obj["dco_cuentamodulo"] + "' " +
    "data-concepto='" + obj["dco_concepto"] + "' " +
    "data-codalmacen='" + $("#txtCODALMACEN").val() + "' " +
    //"data-codalmacen='" + obj["dco_almacen"] + "' " +
        //"data-idalmacen='" + $("#txtIDALM_D").val() + "' " +
    "data-nombrealmacen='" + obj["dco_almacennombre"] + "' " +
    "data-codcentro='" + obj["dco_centro"] + "' " +
        //"data-idcentro='" + $("#txtIDCEN_D").val() + "' " +
    "data-nombrecentro='" + obj["dco_centronombre"] + "' " +
    "data-codtransacc='" + obj["dco_transacc"] + "' " +
    "data-nombretransacc='' " +
    "data-doctran='" + obj["dco_doctran"] + "' " +
    "data-ddocomproba='" + obj["dco_ddo_comproba"] + "' " +
    "data-ddotransacc='" + obj["dco_ddo_transacc"] + "' " +
    "data-nropago='" + obj["dco_nropago"] + "' " +
    "data-fechavence='" + obj["dco_fecha_vence"] + "' ";    

        
        /*var strdata ="data-codcue='" + obj["dco_cuenta"] + "' " +
    "data-codper='" + $("#txtCODPER_D").val() + "' " +
     "data-codmod='" + obj["dco_cuentamodulo"] + "' "+
        "data-concepto='" + $("#txtCONCEPTO_D").val() + "' " +
    "data-codalmacen='" + $("#cmbALMACEN_D").val() + "' " +
        //"data-idalmacen='" + $("#txtIDALM_D").val() + "' " +
    "data-nombrealmacen='" + $("#cmbALMACEN_D option:selected").text() + "' " +
    "data-codcentro='" + $("#cmbCENTRO_D").val() + "' " +
        //"data-idcentro='" + $("#txtIDCEN_D").val() + "' " +
    "data-nombrecentro='" + $("#cmbCENTRO_D option:selected").text() + "' " +
    "data-codtransacc='" + $("#cmbTRANSACC_D").val() + "' " +
    "data-nombretransacc='" + $("#cmbTRANSACC_D option:selected").text() + "' " +
    "data-doctran='" + $("#txtREF_D").val() + "' " +
    "data-ddocomproba='" + $("#txtOPAGO_D").val() + "' " +
    "data-nropago='" + $("#txtNROPAGO_D").val() + "' " +
    "data-fechavence='" + $("#txtFECHAVENCE_D").val() + "' ";   */

        var newrow = $("<tr " + strdata + " onclick='EditDiario(this);'></tr>");
        newrow.append("<td class='' >" + obj["dco_cuentaid"] + "</td>");
        newrow.append("<td class='' >" + obj["dco_cuentanombre"] + "</td>");
        newrow.append("<td class='' >" + "" + "</td>");
        newrow.append("<td class='' >" + "" + "</td>");
        newrow.append("<td class='' >" + obj["dco_clienteid"] + "</td>");
        if (obj["dco_debcre"] == cDebito) {
            newrow.append("<td class='right' >" + CurrencyFormatted(obj["dco_valor_nac"]) + "</td>");
            newrow.append("<td class='right' >" + CurrencyFormatted(0) + "</td>");
        } else {
            newrow.append("<td class='right' >" + CurrencyFormatted(0) + "</td>");
            newrow.append("<td class='right' >" + CurrencyFormatted(obj["dco_valor_nac"]) + "</td>");

        }
        newrow.append("<td class='center' >" + obj["dco_debcre"] + "</td>");
        newrow.append("<td class='center' ><div class='removablerow' onclick='RemoveRowDiario(this)'><span class=\"icon-trash\" ></span></div></td>");
        //var newrow = $("<tr><td>Hola</td><td>chao</td><td>jaja</td><td>si</td><td>NO</td><td>BYE</td><td>7</td><td>8</td></tr>");
        var editrow = $("#tdinvoicediario").find("#editrow");
        newrow.insertBefore(editrow);
        CleanRowDiario();
        /*
        $("#txtCODCUE_D").val(obj["dco_cuenta"]);
        $("#txtCUENTA_D").val(obj["dco_cuentanombre"]);
        $("#txtCODMOD_D").val(obj["dco_cuentamodulo"]);
        $("#txtIDCUE_D").val(obj["dco_cuentaid"]);
        if (obj["dco_debcre"] == cDebito) {
        $("#txtDEBE_D").val(obj["dco_valor_nac"]);
        $("#txtHABER_D").val(0);
        } else {

        $("#txtDEBE_D").val(0);
        $("#txtHABER_D").val(obj["dco_valor_nac"]);
        }

        $("#txtDC_D").val(obj["dco_debcre"]);
        }*/
        //    CallServer(formname + "/SaveObject", jsonText, 5);
        CalculaTotalesDiario();
    }
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
    var htmltable = $("#tdinvoice")[0];
    if (htmltable.rows.length < 3) {
        retorno = false;
        mensajehtml += "Es necesario ingresar al menos un detalle al comprobante<br>";
    }

    if (!retorno) {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert(mensajehtml, 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
    return retorno;
}


/*function Afectacion(obj) {
    CallAfectacion(obj);   
}

function SetAfectacion(obj) {   
    afectacionobj = obj;
    SaveObj();
}

function AfectacionGuias(iddocumento) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = parseInt(iddocumento);    
    CallAfectacionGuias(obj);
}

function SetAfectacionGuias(obj) {
    afectacionobj = obj;
    //SaveObj();
}*/




function SaveObj() {
    if (ValidateForm()) {
        var compobj = GetComprobanteObj();
       
            var obj = {};
            obj["recibo"] = compobj;
            obj["afectacion"] = afectacionobj;
            if ($(this)[0].id == "save")
                obj["com_estado"] = parseInt($("#txtCREADO").val());
            else if ($(this)[0].id == "close")
                obj["com_estado"] = parseInt($("#txtCERRADO").val()); 
            var jsonText = JSON.stringify({ objeto: obj });
            jConfirm('¿Está seguro que desea guardar la nota ? \n Luego no se podra modificar', 'Guardar', function (r) {
                CallServer(formname + "/SaveObject", jsonText, 5);

            });
        }
   
}


function CloseObj() {
    var obj = {};
    obj["recibo"] = GetComprobanteObj();
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
    var opciones = "toolbar=no, scrollbars=no, resizable=yes, top=50, left=100, width=800, height=600";
    window.open("wfDcontablePrint.aspx?codigocomp=" + $("#txtcodigocomp").val()+ "&empresa=" + parseInt(empresasigned["emp_codigo"]),"",opciones);

    //window.location = "wfDcontablePrint.aspx?codigocomp=" + $("#txtcodigocomp").val();
    //window.location = "wfDBancarioPrint.aspx?codigocomp=" + $("#txtcodigocomp").val();
}

function SaveObjResult(data) {
    if (data != "") {
        if (data.d != "-1") {
            jQuery.alerts.dialogClass = 'alert-success';
            jAlert('Comprobante guardado exitosamente...', 'Éxito', function () {
                $("#txtcodigocomp").val(data.d);
                PrintObj();
                location.reload();
            });

        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al guardar el Detalle bancario...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}


function ViewObj() {
    var compobj = GetComprobanteObj();
    CallConsultaDeudas(compobj);
}
/****************************FUNCIONES MANEJO DE TECLAS*******************************/

function RowUp() {
   
    var r = $("#txtCODBANCO")[0].parentNode.parentNode;
    if ($(r).prev().length > 0)
        Edit($(r).prev()[0]);
  //  AddEditRowDiario();
}

function RowDown() {
    
    var r = $("#txtCODBANCO")[0].parentNode.parentNode;
    if ($(r).next().length > 0)
        Edit($(r).next()[0]);
    else {
        if ($("#txtCODBANCO").val() != "")
            AddEditRow();
    }
//    AddEditRowDiario();

}



function IsOpen(id) {
    return $("#" + id).autocomplete('widget').is(':visible');
}

$(window).keydown(function (event) {
    var id = document.activeElement.id;
    var code = event.keyCode;
    var char = event.char;
    $("#mensaje").html(id + " " + code);

    if (id == "txtIDBANCO" || id == "txtDEBITO"|| id == "txtCREDITO") {

        if (id == "txtIDBANCO" && IsOpen(id))
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
            if (id != "txtIDBANCO")
                RowDown();
        }
    }
    if (!IsNumeric(id,event))
        event.preventDefault();

    if (code == 13) //ENTER
        event.preventDefault();

});

$(window).keyup(function (event) {
    var id = document.activeElement.id;
    var code = event.keyCode
    MustCalculate(id);
    if (document.activeElement.parentNode.parentNode.parentNode.parentNode.id == "tdafec_P")
        CalculaAfectacion();

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
        //if (!isNumeric)
        //    alert(keyCode);
    }
    return true;
}




function MustCalculate(id) {
    switch (id) {
        case "txtDEBITO":        
            CalculaTotales();
            break;
        case "txtCREDITO":
            CalculaTotales();
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
        /*$("#txtCODLIS").val(obj.per_listaprecio);
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
        $("#txtTELEFONOREM").val(obj.per_telefono);*/
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
        /*$("#txtCODLIS").val("");
        $("#txtIDLIS").val("");
        $("#txtLISTA").val("");
        $("#txtCODPOL").val("");
        $("#txtIDPOL").val("");
        $("#txtPOLITICA").val("");
        $("#txtPORCENTAJE").val("");
        $("#txtCODVEN").val("");
        $("#txtVENDEDOR").val("");*/
    }
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
