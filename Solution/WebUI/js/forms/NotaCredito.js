﻿//Archivo:          NotaCredito.js
//Descripción:      Contiene las funciones comunes para la interfaz de Notas de Credito Proveedor
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Abril 2015
//2015. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfNotaCredito.aspx";
var menuoption = "NotaCredito";

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
    $("#close").on("click", SaveObj);
    $("#contabilizacion").on("click", ContObj);
    var codigocomp = $("#txtcodigocomp").val();
    if (codigocomp <= 0) {
        $("#contabilizacion").css({ 'display': 'none' });
        $("#print").css({ 'display': 'none' });
        $("#close").css({ 'display': 'none' });

    }

    $("#invo").css({ 'display': 'none' });

    LoadCabecera();
    LoadPie();
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
                GetAutorizacionesResult(data);
            if (retorno == 4)
                GetAutorizacionDataResult(data);
            if (retorno == 5)
                SaveObjResult(data);
            if (retorno == 6)
                GetHojasRutaResult(data);
            if (retorno == 7)
                GetDatosHojaRutaResult(data);
            if (retorno == 8)
                GeneraRetencionResult(data);
            if (retorno == 9)
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

    var origen = $("#txtorigen").val();
    if (origen == "") {
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = $("#txtcodigocomp").val();
        obj["com_almacen"] = parseInt($("#txtCODALMACEN").val());
        obj["com_pventa"] = parseInt($("#txtCODPVENTA").val());
        obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
        obj["com_ctipocom"] = parseInt($("#cmbSIGLA_P").val());  //2 FACT
        var jsonText = JSON.stringify({ objeto: obj });

        if ($("#comcabeceracontent").length > 0) {
            CallServer(formname + "/GetCabecera", jsonText, 0);
        }
    }
    //    else if (origen == "LGC") { //PLANILLA DE CLIENTES
    //        var obj = {};
    //        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    //        obj["com_codigo"] = $("#txtcodigocompref").val();
    //        var jsonText = JSON.stringify({ objeto: obj });

    //        if ($("#comcabeceracontent").length > 0) {
    //            CallServer(formname + "/GetCabeceraFromLGC", jsonText, 0);
    //        }
    //    }

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
    //    else if (origen == "LGC") { //PLANILLA DE CLIENTES
    //        var obj = {};
    //        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    //        obj["com_codigo"] = $("#txtcodigocompref").val();
    //        var jsonText = JSON.stringify({ objeto: obj });
    //        CallServer(formname + "/GetDetalleFromLGC", jsonText, 1);
    //    }

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
    //    else if (origen == "LGC")  //PLANILLA DE CLIENTES
    //        obj["com_codigo"] = $("#txtcodigocompref").val();
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
    SetAutocompleteById("txtIDPOL");
    $("#cmbAUTORIZACION").on("change", GetAutorizaciones);
    $("#txtNUMEROPRO").on("change", complete9);
}

/////////////////////////////////////////////////////////////////////////////////
////////Funciones para manejo de Autorizaciones Proveedor ///////////////////////

function GetAutorizaciones() {

    var obj = GetComprobanteObj();

    /*var obj = {};
    obj["ape_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["ape_persona"] = parseInt($("#txtCODPER").val());*/
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetAutorizaciones", jsonText, 3);
}

function GetAutorizacionesResult(data) {
    if (data != "") {
        $("#cmbAUTORIZACION").replaceWith(data.d);
        if ($("#cmbAUTORIZACION option").length > 0) {
            $("#cmbAUTORIZACION").on("change", GetAutorizacionData);
            $("#cmbAUTORIZACION").trigger("change");
        }
        else {
            $("#txtFECHAAUT").val("");
            $("#txtDESDEAUT").val("");
            $("#txtHASTAAUT").val("");
            $("#txtALMACENPRO").val("");
            $("#txtPVENTAPRO").val("");
            $("#txtNUMEROPRO").val("");
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('El proveedor no posee autorizaciones válidas...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}

function GetAutorizacionData() {

    var obj = GetComprobanteObj();
    //var obj = {};
    obj["ape_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["ape_persona"] = parseInt($("#txtCODPER").val());
    //obj["ape_nro_autoriza"] = $("#cmbAUTORIZACION").val();
    var valor = $("#cmbAUTORIZACION").val().split('-');
    obj["ape_nro_autoriza"] = valor[0];
    obj["ape_fact1"] = valor[1];
    obj["ape_fact2"] = valor[2];
    obj["ape_retdato"] = valor[3];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetAutorizacionData", jsonText, 4);
}
function GetAutorizacionDataResult(data) {
    $("#txtFECHAAUT").val("");
    $("#txtDESDEAUT").val("");
    $("#txtHASTAAUT").val("");
    $("#txtALMACENPRO").val("");
    $("#txtPVENTAPRO").val("");
    $("#txtNUMEROPRO").val("");
    if (data != "") {
        if (data.d != "") {
            var obj = $.parseJSON(data.d);
            $("#txtFECHAAUT").val(GetDateValue(obj["ape_val_fecha"]));
            $("#txtALMACENPRO").val(obj["ape_fac1"]);
            $("#txtPVENTAPRO").val(obj["ape_fac2"]);
            $("#txtTABCOAUT").val(obj["ape_tablacoa"]);
            $("#txtRETAUT").val(obj["ape_retdato"]);
            $("#txtDESDEAUT").val(obj["ape_fac3"]);
            $("#txtHASTAAUT").val(obj["ape_fact3"]);
            //$("#txtNUMEROPRO").val("000000000");          
        }
    }
}




function SetFormDetalle() {
    SetAutocompleteById("txtIDCUE");
    $("#chkIVA").on("change", CalculaLinea);

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



function CleanRow() {
    $("#txtCODCUE").val("");
    $("#txtIDCUE").val("");
    $("#txtCUENTA").val("");
    $("#txtOBSERVACION").val("");
    $("#txtCANTIDAD").val(0);
    $("#txtVALOR").val(0);
    $("#txtDESC").val(0);
    $("#txtTOTAL").val(0);
    $("#chkIVA").prop("checked", false);
    $("#txtIDCUE").select();
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
    if (idobj == "txtIDPOL") {
        return {
            label: item.pol_id + "," + item.pol_nombre,
            value: item.pol_id,
            info: item
        }
    }
    if (idobj == "txtIDCUE") {
        return {
            label: item.cue_id + "," + item.cue_nombre,
            value: item.cue_id,
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

        $("#txtCODPOL").val(item.info.per_politica);
        $("#txtIDPOL").val(item.info.per_politicaid);
        $("#txtPOLITICA").val(item.info.per_politicanombre);
        $("#txtNROPAGOS").val(item.info.per_politicanropagos);
        $("#txtDIASPLAZO").val(item.info.per_politicadiasplazo);
        $("#txtPORCENTAJE").val(item.info.per_politicadesc);
        $("#txtPORCPAGOCON").val(item.info.per_politicaporpagocon);

        $("#txtTELEFONO").val(item.info.per_telefono);
        GetAutorizaciones();
    }
    if (idobj == "txtIDPOL") {
        $("#txtPOLITICA").val(item.info.pol_nombre);
        $("#txtCODPOL").val(item.info.pol_codigo);
        $("#txtPORCENTAJE").val(item.info.pol_porc_desc);
        $("#txtNROPAGOS").val(item.info.pol_nro_pagos);
        $("#txtDIASPLAZO").val(item.info.pol_dias_plazo);
        $("#txtPORCPAGOCON").val(item.info.pol_porc_pago_con);


    }
    if (idobj == "txtIDCUE") {
        $("#txtIDCUE").val(item.info.cue_id);
        $("#txtCODCUE").val(item.info.cue_codigo);
        $("#txtCUENTA").val(item.info.cue_nombre);
        //LoadProduct();
        //alert(ui.item.label);
    }
}




/*********************FUNCIONES PARA CALCULO DE TOTALES **********************/

function CalculaLinea() {
    var cant = $("#txtCANTIDAD").val();
    if (!$.isNumeric(cant))
        cant = 0;
    var prec = $("#txtVALOR").val();
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

function CalculaTotales() {
    var htmltable = $("#tdinvoice")[0];
    var subtotal0 = 0;
    var subtotalIVA = 0;
    var desc0 = 0;
    var descIVA = 0;


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
            cant = htmltable.rows[r].cells[c].innerText;
            prec = htmltable.rows[r].cells[c + 1].innerText;
            desc = htmltable.rows[r].cells[c + 2].innerText;
            valor = htmltable.rows[r].cells[c + 3].innerText;
            iva = (htmltable.rows[r].cells[c + 4].innerText == "SI") ? true : false;
        }

        subtotalIVA += ($.isNumeric(valor)) ? ((iva) ? parseFloat(valor) : 0) : 0;
        subtotal0 += ($.isNumeric(valor)) ? ((!iva) ? parseFloat(valor) : 0) : 0;

        var subtotal = parseFloat(cant) * parseFloat(prec)
        var subtotaldesc = subtotal * (parseFloat(desc) / 100);

        descIVA += ($.isNumeric(desc)) ? ((iva) ? subtotaldesc : 0) : 0;
        desc0 += ($.isNumeric(desc)) ? ((!iva) ? subtotaldesc : 0) : 0;

        //descIVA += ($.isNumeric(desc)) ? ((iva) ? parseFloat(desc) : 0) : 0;

        //desc0 += ($.isNumeric(desc)) ? ((!iva) ? parseFloat(desc) : 0) : 0;

    }
    var descuentoiva = ($.isNumeric($("#txtDESCUENTOIVA").val())) ? parseFloat($("#txtDESCUENTOIVA").val()) : 0;
    var descuento0 = ($.isNumeric($("#txtDESCUENTO0").val())) ? parseFloat($("#txtDESCUENTO0").val()) : 0;

    var porcentajeiva = ($.isNumeric($("#txtIVAPORCENTAJE").val())) ? parseFloat($("#txtIVAPORCENTAJE").val()) : 0;

    var valorIVA = (subtotalIVA - descIVA - descuentoiva) * (porcentajeiva / 100);

    var valordeclarado = ($.isNumeric($("#txtVALORDECLARADO").val())) ? parseFloat($("#txtVALORDECLARADO").val()) : 0;
    var porcentajeseguro = ($.isNumeric($("#txtPORCSEGURO").val())) ? parseFloat($("#txtPORCSEGURO").val()) : 0;


    var seguro = valordeclarado * (porcentajeseguro / 100);
    var transporte = ($.isNumeric($("#txtVDOMICILIO").val())) ? parseFloat($("#txtVDOMICILIO").val()) : 0;

    var total = (subtotal0 - desc0 - descuento0) + (subtotalIVA - descIVA - descuentoiva) + valorIVA + seguro + transporte;

    $("#txtSUBTOTAL0").val(CurrencyFormatted(subtotal0));
    $("#txtSUBTOTALIVA").val(CurrencyFormatted(subtotalIVA));
    $("#txtDESC0").val(CurrencyFormatted(desc0));
    $("#txtDESCIVA").val(CurrencyFormatted(descIVA));
    $("#txtIVA").val(CurrencyFormatted(valorIVA));
    $("#txtSEGURO").val(CurrencyFormatted(seguro));
    $("#txtTRANSPORTE").val(CurrencyFormatted(transporte));
    $("#txtTOTALCOM").val(CurrencyFormatted(total));
}



/*********************FUNCIONES PARA AGREGAR y EDITAR UN DETALLE**********************/

function AddEditRow() {
    var newrow = $("<tr data-codcue='" + $("#txtCODCUE").val() + "' onclick='Edit(this);'></tr>");
    newrow.append("<td class='' >" + $("#txtIDCUE").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtCUENTA").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtOBSERVACION").val() + "</td>");
    newrow.append("<td class='center' >" + $("#txtCANTIDAD").val() + "</td>");
    newrow.append("<td class='right' >" + $("#txtVALOR").val() + "</td>");
    newrow.append("<td class='right' >" + $("#txtDESC").val() + "</td>");
    newrow.append("<td class='right' >" + $("#txtTOTAL").val() + "</td>");
    newrow.append("<td class='center' >" + ($("#chkIVA").is(':checked') ? "SI" : "NO") + "</td>");
    newrow.append("<td class='center' ><div class='removablerow' onclick='RemoveRow(this)'><span class=\"icon-trash\" ></span></div></td>");


    //var newrow = $("<tr><td>Hola</td><td>chao</td><td>jaja</td><td>si</td><td>NO</td><td>BYE</td><td>7</td><td>8</td></tr>");
    var editrow = $("#tdinvoice").find("#editrow");
    newrow.insertBefore(editrow);

    $("#txtCODCUE").val("");
    $("#txtIDCUE").val("");
    $("#txtCUENTA").val("");
    $("#txtOBSERVACION").val("");
    $("#txtCANTIDAD").val(0);
    $("#txtVALOR").val(0);
    $("#txtDESC").val(0);
    $("#txtTOTAL").val(0);
    $("#chkIVA").prop("checked", false);
    $("#txtIDCUE").select();
}




function MoveTo(control, destino) {
    var obj = $(control).detach();
    $(destino).append(obj);
}


function Edit(row) {

    var r = $("#txtCODCUE")[0].parentNode.parentNode;
    var codpro = $("#txtCODCUE").val();
    var idpro = $("#txtIDCUE").val();
    var producto = $("#txtCUENTA").val();
    var observacion = $("#txtOBSERVACION").val();
    var cantidad = $("#txtCANTIDAD").val();
    var valor = $("#txtVALOR").val();
    var desc = $("#txtDESC").val();
    var total = $("#txtTOTAL").val();
    var iva = ($("#chkIVA").is(':checked') ? "SI" : "NO");


    $("#txtCODCUE").val($(row).data("codcue").toString());
    $("#txtIDCUE").val(row.cells[0].innerText); row.cells[0].innerText = "";
    $("#txtCUENTA").val(row.cells[1].innerText); row.cells[1].innerText = "";
    $("#txtOBSERVACION").val(row.cells[2].innerText); row.cells[2].innerText = "";

    $("#txtCANTIDAD").val(row.cells[3].innerText); row.cells[3].innerText = "";
    $("#txtVALOR").val(row.cells[4].innerText); row.cells[4].innerText = "";
    $("#txtDESC").val(row.cells[5].innerText); row.cells[5].innerText = "";
    $("#txtTOTAL").val(row.cells[6].innerText); row.cells[6].innerText = "";
    $("#chkIVA").prop("checked", ((row.cells[7].innerText == "SI") ? true : false)); row.cells[7].innerText = "";
    $("#txtIDPRO").focus();

    MoveTo($("#txtIDCUE"), $(row.cells[0]));
    MoveTo($("#txtCODCUE"), $(row.cells[0]));
    MoveTo($("#txtCUENTA"), $(row.cells[1]));
    MoveTo($("#txtOBSERVACION"), $(row.cells[2]));
    MoveTo($("#txtCANTIDAD"), $(row.cells[3]));
    MoveTo($("#txtVALOR"), $(row.cells[4]));
    MoveTo($("#txtDESC"), $(row.cells[5]));
    MoveTo($("#txtTOTAL"), $(row.cells[6]));
    MoveTo($("#chkIVA"), $(row.cells[7]));




    $(r).data("codcue", codpro);
    r.cells[0].innerText = idpro;
    r.cells[1].innerText = producto;
    r.cells[2].innerText = observacion;
    r.cells[3].innerText = cantidad;
    r.cells[4].innerText = valor;
    r.cells[5].innerText = desc;
    r.cells[6].innerText = total;
    r.cells[7].innerText = iva;

    $(r).attr('onclick', 'Edit(this)');
    $(row).removeAttr('onclick');

    $("#txtIDCUE").select();

}




/***********************SAVE FUNCTIONS ***********************************/

function GetTotalObj() {
    var obj = {};
    obj["tot_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["tot_comprobante"] = $("#txtcodigocomp").val();

    //obj["tot_impuesto"] = 8; //Impuesto Venta    
    //obj["tot_impuesto"] = 1; //Impuesto Compra


    obj["tot_impuesto"] = $("#cmbIMPUESTO").val();
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
    obj = SetAuditoria(obj);
    return obj;
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
        if ($.isNumeric($("#txtCODCUE").val())) {
            obj["ddoc_cuenta"] = parseInt($("#txtCODCUE").val());
            obj["ddoc_observaciones"] = $("#txtOBSERVACION").val();
            obj["ddco_udigitada"] = 1;
            obj["ddoc_cantidad"] = parseFloat($("#txtCANTIDAD").val());
            obj["ddoc_precio"] = parseFloat($("#txtVALOR").val());
            obj["ddoc_dscitem"] = parseFloat($("#txtDESC").val());
            obj["ddoc_total"] = parseFloat($("#txtTOTAL").val());
            obj["ddoc_grabaiva"] = $("#chkIVA").is(':checked') ? 1 : 0;
        }
        else
            return null;
    }
    else {
        if ($.isNumeric($(row).data("codcue").toString())) {
            obj["ddoc_cuenta"] = parseInt($(row).data("codcue").toString());
            obj["ddoc_observaciones"] = row.cells[2].innerText;
            obj["ddco_udigitada"] = 1;
            obj["ddoc_cantidad"] = parseFloat(row.cells[3].innerText);
            obj["ddoc_precio"] = parseFloat(row.cells[4].innerText);
            obj["ddoc_dscitem"] = parseFloat(row.cells[5].innerText);
            obj["ddoc_total"] = parseFloat(row.cells[6].innerText);
            obj["ddoc_grabaiva"] = ((row.cells[7].innerText == "SI") ? 1 : 0);
        }
        else
            return null;
    }
    obj = SetAuditoria(obj);
    return obj;
}


function GetCcomdocObj() {
    var obj = {};
    obj["cdoc_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cdoc_comprobante"] = $("#txtcodigocomp").val();
    obj["cdoc_politica"] = parseInt($("#txtCODPOL").val());

    if ($("#cmbAUTORIZACION").val() != null) {
        var valor = $("#cmbAUTORIZACION").val().split('-');
        obj["cdoc_acl_nroautoriza"] = valor[0];
    }
    obj["cdoc_aut_factura"] = $("#txtALMACENPRO").val() + "-" + $("#txtPVENTAPRO").val() + "-" + $("#txtNUMEROPRO").val();
    var autDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHAENTPRO").val());
    obj["cdoc_aut_fecha"] = autDate;
    obj["cdoc_nombre"] = $("#txtNOMBRES").val();
    obj["cdoc_direccion"] = $("#txtDIRECCION").val();
    obj["cdoc_telefono"] = $("#txtTELEFONO").val();
    obj["cdoc_ced_ruc"] = $("#txtRUC").val();
    obj["cdoc_acl_retdato"] = $("#txtRETAUT").val();
    obj["cdoc_acl_tablacoa"] = $("#txtTABCOAUT").val();
    obj["cdoc_observeaciones"] = $("#txtCOMPROBANTE").val();

    obj["detalle"] = GetDetalle();
    obj = SetAuditoria(obj);
    return obj;
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
    //obj["com_fecha"] = currentDate;
    obj["com_numero"] = $("#txtNUMERO").val();
    obj["com_fecha"] = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate(), now.getHours(), now.getMinutes(), now.getSeconds(), now.getMilliseconds());
    obj["com_doctran"] = $("#numerocomp").html();    
    obj["com_nocontable"] = parseInt($("#txtnocontable").val());
    obj["com_periodo"] = currentDate.getFullYear();
    obj["com_almacen"] = almacen;
    obj["com_pventa"] = pventa;
    obj["com_codclipro"] = parseInt($("#txtCODPER").val());
    obj["com_concepto"] = $("#txtOBSERVACIONES").val();
    obj["com_agente"] = parseInt($("#txtCODVEN").val());
    obj["ccomdoc"] = GetCcomdocObj();
    obj["total"] = GetTotalObj();
    obj = SetAuditoria(obj);
    return obj;
}



function FillWith(caracter, largo, valor) {
    var cantidad = largo - valor.length
    if (cantidad > 0) {
        var fill = "";
        for (var i = 0; i < cantidad; i++) {
            fill = fill + caracter;
        }
        valor = fill + valor;
    }
    return valor;
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

    if (retorno)
        $("#txtNUMEROPRO").val(FillWith("0", 9, $("#txtNUMEROPRO").val()));

    var num = parseInt($("#txtNUMEROPRO").val());
    var desde = parseInt($("#txtDESDEAUT").val());
    var hasta = parseInt($("#txtHASTAAUT").val());

    if ((desde > num) || (hasta < num)) {
        retorno = false;
        mensajehtml += "El numero no se encuentra en la autorización " + desde + " a " + hasta;
    }


    var detalle = GetDetalle();
    if (detalle.length == 0) {
        retorno = false;
        mensajehtml += "Es necesario ingresar al menos un detalle al comprobante<br>";
    }


    /*var htmltable = $("#tdinvoice")[0];
    if (htmltable.rows.length < 3) {
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


function Afectacion(obj) {


    var origen = $("#txtorigen").val();
    if (origen == "") {
        CallAfectacion(obj);
    }



}


function SetAfectacion(obj) {
    //recibocreated = true;
    //objdrecibo = obj;
    afectacionobj = obj;
    SaveObj();
}




function SaveObj() {
    if (ValidateForm()) {
        var compobj = GetComprobanteObj();
        if (afectacionobj == null)
            Afectacion(compobj);
        else {
            var obj = {};
            obj["comprobante"] = compobj;
            obj["afectacion"] = afectacionobj;

            var jsonText = JSON.stringify({ objeto: obj });
            jConfirm('¿Está seguro que desea guardar la obligacion ? \n Luego no se podra modificar', 'Guardar', function (r) {
                if (r) {
                    CallServer(formname + "/SaveObject", jsonText, 5);
                }
                else {

                    afectacionobj = null;

                }
            });

        }

    }
}





//function SaveObj() {
//    if (ValidateForm()) {        
//            var obj = {};
//            obj["comprobante"] = GetComprobanteObj();
//            //obj["drecibo"] = objdrecibo;
//            //obj["rutaxfactura"] = GetRutaxFacturaObj();
//            var jsonText = JSON.stringify({ objeto: obj });
//            //var jsonText = GetComprobanteObj(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
//            jConfirm('¿Está seguro que desea guardar la obligacion ? \n Luego no se podra modificar', 'Guardar', function (r) {
//                CallServer(formname + "/SaveObject", jsonText, 5);

//            });
//        
//        
//    }
//}
function ContObj() {
    window.location = "wfDiario.aspx?codigocomp=" + $("#txtcodigocomp").val() + "&tipodoc=" + $("#txttipodoc").val();
}

//function PrintObj() {
//    window.location = "wfComprobantePrint.aspx?codigocomp=" + $("#txtcodigocomp").val();
//}

function PrintObj() {

    //window.location = "wfReciboPrint.aspx?codigocomp=" + $("#txtcodigocomp").val();
    var opciones = "toolbar=no, scrollbars=no, resizable=yes, top=50, left=100, width=800, height=600";
    window.open("wfDcontablePrint.aspx?codigocomp=" + $("#txtcodigocomp").val() + "&empresa=" + parseInt(empresasigned["emp_codigo"]), "", opciones);
}
function SaveObjResult(data) {
    if (data != "") {
        var cod = parseInt(data.d);
        if (cod > 0) {
            //if (data.d == "OK") {
            jQuery.alerts.dialogClass = 'alert-success';
            jAlert('Comprobante guardado exitosamente...', 'Éxito', function () {
                jQuery.alerts.dialogClass = null; // reset to default

                
                        location.reload();
                
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


function CloseObj() {
    if (ValidateForm()) {
        var obj = {};
        obj["comprobante"] = GetComprobanteObj();
        var jsonText = JSON.stringify({ objeto: obj });
        jConfirm('¿Está seguro que desea mayorizar la obligacion ? \n Luego no se podra modificar', 'Guardar', function (r) {
            CallServer(formname + "/CloseObject", jsonText, 9);

        });


    }
}

function CloseObjResult(data) {
    if (data != "") {
        var cod = parseInt(data.d);
        if (cod > 0) {
            //if (data.d == "OK") {
            jQuery.alerts.dialogClass = 'alert-success';
            jAlert('Comprobante guardado exitosamente...', 'Éxito', function () {
                jQuery.alerts.dialogClass = null; // reset to default
                window.location = formname + "?codigocomp=" + data.d;
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



function GeneraRetencion(cod) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = cod;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer("ws/Metodos.asmx/GeneraRetencion", jsonText, 8);

}

function GeneraRetencionResult(data) {
    if (data != "") {
        var cod = parseInt(data.d);
        if (cod > 0) {
            window.location = "wfRetencion.aspx?codigocomp=" + cod;
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al generar el comprobante de retención...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}

/****************************FUNCIONES MANEJO DE TECLAS*******************************/

function RowUp() {
    var r = $("#txtCODCUE")[0].parentNode.parentNode;
    if ($(r).prev().length > 0)
        Edit($(r).prev()[0]);
}

function RowDown() {
    var r = $("#txtCODCUE")[0].parentNode.parentNode;
    if ($(r).next().length > 0)
        Edit($(r).next()[0]);
    else {
        if ($("#txtCODCUE").val() != "")
            AddEditRow();
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

    if (id == "txtIDCUE" || id == "txtOBSERVACION" || id == "txtCANTIDAD" || id == "txtDESC" || id == "txtVALOR") {

        if (id == "txtIDCUE" && IsOpen(id))
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
            if (id != "txtIDCUE")
                RowDown();
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
        case "txtCANTIDAD":
        case "txtDESC":
        case "txtVALOR":
            CalculaLinea();
            break;
        case "txtDESCUENTO0":
        case "txtDESCUENTOIVA":
        case "txtPORCENTAJE":
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

        $("#txtCODPOL").val(obj.per_politica);
        $("#txtIDPOL").val(obj.per_politicaid);
        $("#txtPOLITICA").val(obj.per_politicanombre);

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

