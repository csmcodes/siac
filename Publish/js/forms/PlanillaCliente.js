//Archivo:          Comprobante.js
//Descripción:      Contiene las funciones comunes para la interfaz de gestion de comprobantes (Facturas, Guias, Notas de credito, etc)
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Septiembre 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfPlanillaCliente.aspx";
var menuoption = "PlanillaCliente";
var asig = 0;

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
    $("#save").on("click", SaveObj);       //Boton "Guardar" de la barra
    $("#print").on("click", PrintObj);
    var codigocomp = $("#txtcodigocomp").val();
    if (codigocomp <= 0) {
        $("#print").css({ 'display': 'none' });
    }
 
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
                LoadProductResult(data);
            if (retorno == 4)
                GetPriceResult(data);
            if (retorno == 5)
                GetNumeroComprobanteResult(data);
            if (retorno == 6)
                LoadFormInicialResult(data);
            if (retorno == 7)
                LoadPuntoVentaResult(data);
            if (retorno == 8)
                LoadFacAsigResult(data);
            if (retorno == 9)
                LoadFacPenResult(data);
            if (retorno == 10)
                SaveObjResult(data);
            if (retorno == 11)
                GetTotalesResult(data);
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

function LoadFacAsig() {
    var obj = {};
    obj["empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["vehiculo"] =  parseInt($("#txtCODVEHICULO").val());
    obj["chofer"] = parseInt( $("#txtCODCHOFER").val());
    obj["socio"] =  parseInt($("#txtCODSOCIO").val());
    obj["ruta"] = parseInt( $("#txtCODRUTA").val());
    obj["id"] = "tdes";
    obj["estado_ruta"] = asig;
    obj["com_codigo"] =  parseInt($("#txtcodigocomp").val());
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/LoadFacAsig", jsonText, 8);
}

function LoadFacPen() {
    var obj = {};
    obj["empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["estado_ruta"] = 0;
    obj["ruta"] = parseInt($("#txtCODRUTA").val());
    obj["factura"] = parseInt($("#txtFAC").val());
    obj["socio"] = parseInt($("#txtCODSOCIO").val());
    obj["id"] = "tdatos";
    obj["com_codigo"] = 0;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/LoadFacAsig", jsonText, 9);
}

function LoadFacPenResult(data) {
    if (data != "") {
        $("#tdatos").replaceWith(data.d);
    }
    var numerocomp = $("#txtcodigocomp").val();

        LoadFacAsig();

}


function LoadFacAsigResult(data) {
    if (data != "") {
        $("#tdes").replaceWith(data.d);
    }
    GetTotales();
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
   

}

function PrintObj() {
    window.location = "wfHojaRutaPrint.aspx";
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
    SetAutocompleteById("txtCODREM");
    SetAutocompleteById("txtCODDES");
    SetAutocompleteById("txtIDVEH");
    SetAutocompleteById("txtIDRUT");
    SetAutocompleteById("txtIDSOC");
    SetAutocompleteById("txtIDCHO");
    $("#txtFECHASAL").val($("#txtFECHA").val());
      
}

function SetFormDetalle() {
    SetAutocompleteById("txtIDPRO");
    $("#cmbUMEDIDA").on("change", GetPrice);
    $("#txtCANTIDAD").on("change", CalculaLinea);
    $("#txtIDSOC").on("change", LoadFacPen);
    $("#txtIDCHO").on("change", LoadFacAsig);
    $("#txtIDRUT").on("change", LoadFacPen);
    $("#txtFAC").on("change", LoadFacPen);
    GetTotales();
     var numerocomp = $("#txtcodigocomp").val();

     if (numerocomp != "-1") {
         LoadFacPen();
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
    $("#txtCODPRO").val("");
    $("#txtIDPRO").val("");
    $("#txtPRODUCTO").val("");
    $("#txtOBSERVACION").val("");
    $("#cmbUMEDIDA").children('option').removeAttr('disabled');
    $("#txtCANTIDAD").val(0);
    $("#txtPRECIO").val(0);
    $("#txtDESC").val(0);
    $("#txtTOTAL").val(0);
    $("#chkIVA").prop("checked", false);
    $("#txtIDPRO").select();
    return false;
}

function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCODCLIPRO" || idobj == "txtCODREM" || idobj == "txtCODDES") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_id,
            info: item
        }
    }

    if (idobj == "txtIDSOC") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_id,
            info: item
        }
    }

    if (idobj == "txtIDCHO") {
        return {
            label: item.cho_nrolicencia + "," + item.cho_nombrechofer + " " + item.cho_apellidochofer,
            value: item.cho_nrolicencia,
            info: item
        }
    }

    if (idobj == "txtIDRUT") {
        return {
            label: item.rut_id + "," + item.rut_origen + "" + item.rut_destino,
            value: item.rut_id,
            info: item
        }
    }

    if (idobj == "txtIDPRO") {
        return {
            label: item.pro_id + "," + item.pro_nombre,
            value: item.pro_id,
            info: item
        }
    }
}

function GetAutoCompleteObj(idobj, item) {
    var numerocomp = $("#txtcodigocomp").val();
    
    if (idobj == "txtCODCLIPRO") {
        $("#txtCIRUC").val(item.info.per_ciruc);
        $("#txtNOMBRES").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtRAZON").val(item.info.per_razon);
        $("#txtRUC").val(item.info.per_ciruc);
        $("#txtDIRECCION").val(item.info.per_direccion);
        $("#txtTELEFONO").val(item.info.per_telefono);
        $("#txtCODLIS").val(item.info.per_listaprecio);
        $("#txtIDLIS").val(item.info.per_listaid);
        $("#txtLISTA").val(item.info.per_listanombre);
        $("#txtCODPOL").val(item.info.per_politicaid);
        $("#txtPOLITICA").val(item.info.per_politicanombre);
        $("#txtPORCENTAJE").val(item.info.per_politicadesc);
        $("#txtCODVEN").val(item.info.per_agenteid);
        $("#txtVENDEDOR").val(item.info.per_agentenombre);
    }

    if (idobj == "txtCODREM") {
        $("#txtCIRUCREM").val(item.info.per_ciruc);
        $("#txtNOMBRESREM").val(item.info.per_nombres);
        $("#txtAPELLIDOSREM").val(item.info.per_apellidos);
        $("#txtDIRECCIONREM").val(item.info.per_direccion);
        $("#txtTELEFONOREM").val(item.info.per_telefono);
    }

    if (idobj == "txtCODDES") {
        $("#txtCIRUCDES").val(item.info.per_ciruc);
        $("#txtNOMBRESDES").val(item.info.per_nombres);
        $("#txtAPELLIDOSDES").val(item.info.per_apellidos);
        $("#txtDIRECCIONDES").val(item.info.per_direccion);
        $("#txtTELEFONODES").val(item.info.per_telefono);
    }

    if (idobj == "txtIDVEH") {
        $("#txtPLACA").val(item.info.veh_placa);
        $("#txtCODVEHICULO").val(item.info.veh_codigo);
        $("#txtDISCO").val(item.info.veh_disco);
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

    if (idobj == "txtIDSOC") {
        $("#txtSOCIO").val(item.info.per_nombres + " " + item.info.per_apellidos);
        $("#txtCODSOCIO").val(item.info.per_codigo);
        LoadFacPen();
    }



    if (idobj == "txtIDCHO") {
        $("#txtCHOFER").val(item.info.cho_nombrechofer + " " + item.info.cho_apellidochofer);
        $("#txtCODCHOFER").val(item.info.cho_persona);
        LoadFacAsig();
    }

    if (idobj == "txtIDRUT") {
        $("#txtNOMBRERUT").val(item.info.rut_nombre);
        $("#txtNOMBRERUT").val(item.info.rut_nombre);
        $("#txtDESTINO").val(item.info.rut_destino);
        $("#txtORIGEN").val(item.info.rut_origen);
        $("#txtCODRUTA").val(item.info.rut_codigo);
        LoadFacPen();
    }

    if (idobj == "txtIDPRO") {
        LoadProduct();
    }

}

function disablefiltros() {
asig = parseInt($("#txtESTADO").val());
    if (asig == 2) {
        $('#quitar').prop('disabled', true);
        $('#agregar').prop('disabled', true);
        $("#txtIDRUT").prop('disabled', true);
        $("#txtIDVEH").prop('disabled', true);
        $("#txtHOURSAL").prop('disabled', true);
        $("#txtCONCEPTO").prop('disabled', true);
        $("#assign").prop('disabled', true);
        $("#save").prop('disabled', true);
       
    }
}




function GetTotales() {
    disablefiltros();
    var jsonText = GetComprobanteObj(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
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
        if (obj["total"]["poliValores"]!=null)
        for (i = 0; i < obj["total"]["poliValores"].length; i++) {

                $("#" + obj["total"]["poliNombres"][i]).val(obj["total"]["poliValores"][i]);
       }
        
    }
}

function LoadProduct(item) {
    var id = $("#txtIDPRO").val();
    var jsonText = JSON.stringify({ id: id });
    CallServer(formname + "/GetProduct", jsonText, 3);
}

function LoadProductResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        $("#txtCODPRO").val(obj["pro_codigo"]);
        $("#txtPRODUCTO").val(obj["pro_nombre"]);
        $("#txtCANTIDAD").val(1);
        $("#txtDESC").val(0);
        if (obj["factores"] != null) {
            var opciones = $("#cmbUMEDIDA").children("option");
            $("#cmbUMEDIDA").children('option').attr('disabled', 'disabled');           
            for (i = 0; i < obj["factores"].length; i++) {
                $("#cmbUMEDIDA option[value='" + obj["factores"][i]["fac_unidad"].toString() + "']").removeAttr("disabled");
                if (SetCheckValue(obj["factores"][i]["fac_default"])) {
                    $("#cmbUMEDIDA option[value='" + obj["factores"][i]["fac_unidad"].toString() + "']").attr('selected', 'selected');
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
        $("#chkIVA").prop("checked", SetCheckValue(obj["pro_iva"]));
        CalculaLinea();         
    }
}


function GetPrice() {
    var obj = {};
    obj["producto"] = $("#txtCODPRO").val();
    obj["lista"] = $("#txtCODLIS").val();
    obj["unidad"] = $("#cmbUMEDIDA").val();
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
    var cant = $("#txtCANTIDAD").val();
    if (!$.isNumeric(cant))
        cant = 0;
    var prec = $("#txtPRECIO").val();
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
        var c = htmltable.rows[r].cells.length - 3;
        var hijodes = $(htmltable.rows[r].cells[c]).children("input");
        var hijotot = $(htmltable.rows[r].cells[c + 1]).children("input");
        var hijoiva = $(htmltable.rows[r].cells[c + 2]).children("input");
        var desc = 0;
        var valor = 0;
        var iva = false;
        if (hijotot.length > 0) {
            desc = $(hijodes).val();
            valor = $(hijotot).val();
            iva = $(hijoiva).is(':checked') ? true : false;
        }
        else {
            desc = $(htmltable.rows[r].cells[c]).text();
            valor = $(htmltable.rows[r].cells[c + 1]).text();
            iva = ($(htmltable.rows[r].cells[c + 2]).text() == "SI") ? true : false;
        }
        subtotalIVA += ($.isNumeric(valor)) ? ((iva) ? parseFloat(valor) : 0) : 0;
        subtotal0 += ($.isNumeric(valor)) ? ((!iva) ? parseFloat(valor) : 0) : 0;
        descIVA += ($.isNumeric(desc)) ? ((iva) ? parseFloat(desc) : 0) : 0;
        desc0 += ($.isNumeric(desc)) ? ((!iva) ? parseFloat(desc) : 0) : 0;
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
    var newrow = $("<tr data-codpro='" + $("#txtCODPRO").val() + "' onclick='Edit(this);'></tr>");
    newrow.append("<td class='' >" + $("#txtIDPRO").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtPRODUCTO").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtOBSERVACION").val() + "</td>");
    newrow.append("<td class='' data-coduni='" + $("#cmbUMEDIDA").val() + "'>" + $("#cmbUMEDIDA option:selected").text() + "</td>");
    newrow.append("<td class='center' >" + $("#txtCANTIDAD").val() + "</td>");
    newrow.append("<td class='right' >" + $("#txtPRECIO").val() + "</td>");
    newrow.append("<td class='right' >" + $("#txtDESC").val() + "</td>");
    newrow.append("<td class='right' >" + $("#txtTOTAL").val() + "</td>");
    newrow.append("<td class='center' >" + ($("#chkIVA").is(':checked') ? "SI" : "NO") + "</td>");
    newrow.append("<td class='center' ><div class='removablerow' onclick='RemoveRow(this)'><span class=\"icon-trash\" ></span></div></td>");  
    var editrow = $("#editrow");
    newrow.insertBefore(editrow);
    $("#txtCODPRO").val("");
    $("#txtIDPRO").val("");
    $("#txtPRODUCTO").val("");
    $("#txtOBSERVACION").val("");
    $("#cmbUMEDIDA").children('option').removeAttr('disabled');
    $("#txtCANTIDAD").val(0);
    $("#txtPRECIO").val(0);
    $("#txtDESC").val(0);
    $("#txtTOTAL").val(0);
    $("#chkIVA").prop("checked", false);
    $("#txtIDPRO").select();
}


function MoveTo(control, destino) {
    var obj = $(control).detach();
    $(destino).append(obj);
}

function MoveRigth() {
    var rows = $("#tdatos tr");
    for (var r = 1; r < rows.length; r++) {
        var obj = rows[r];
        if ($(obj).css("background-color") == rgbcolor) {
            MoveTo(obj, "#tdes")
            $(obj).css("background-color", "");
            $(obj).children("td").css("background-color", "")
        }
    }
    GetTotales();
}

function MoveLeft() {
    var rows = $("#tdes tr");
    for (var r = 1; r < rows.length; r++) {
        var obj = rows[r];
        if ($(obj).css("background-color") == rgbcolor) {
            MoveTo(obj, "#tdatos")
            $(obj).css("background-color", "");
            $(obj).children("td").css("background-color", "")
        }
    }
    GetTotales();
}


function Edit(row) {
/*
    var r = $("#txtCODPRO")[0].parentNode.parentNode;
    var codpro = $("#txtCODPRO").val();
    var idpro = $("#txtIDPRO").val();
    var producto = $("#txtPRODUCTO").val();
    var observacion = $("#txtOBSERVACION").val();
    var coduni = $("#cmbUMEDIDA").val();
    var unidad = $("#cmbUMEDIDA option:selected").text();
    var cantidad = $("#txtCANTIDAD").val();
    var precio = $("#txtPRECIO").val();
    var desc = $("#txtDESC").val();
    var total = $("#txtTOTAL").val();
    var iva = ($("#chkIVA").is(':checked') ? "SI" : "NO");


    $("#txtCODPRO").val($(row).data("codpro").toString());
    $("#txtIDPRO").val(row.cells[0].innerText); row.cells[0].innerText = "";
    $("#txtPRODUCTO").val(row.cells[1].innerText); row.cells[1].innerText = "";
    $("#txtOBSERVACION").val(row.cells[2].innerText); row.cells[2].innerText = "";

    $("#cmbUMEDIDA option[value='" + $(row.cells[3]).data("coduni") + "']").attr('selected', 'selected'); row.cells[3].innerText = "";

    $("#txtCANTIDAD").val(row.cells[4].innerText); row.cells[4].innerText = "";
    $("#txtPRECIO").val(row.cells[5].innerText); row.cells[5].innerText = "";
    $("#txtDESC").val(row.cells[6].innerText); row.cells[6].innerText = "";
    $("#txtTOTAL").val(row.cells[7].innerText); row.cells[7].innerText = "";
    $("#chkIVA").prop("checked", ((row.cells[8].innerText == "SI") ? true : false)); row.cells[8].innerText = "";
    $("#txtIDPRO").focus();

    MoveTo($("#txtIDPRO"), $(row.cells[0]));
    MoveTo($("#txtCODPRO"), $(row.cells[0]));
    MoveTo($("#txtPRODUCTO"), $(row.cells[1]));
    MoveTo($("#txtOBSERVACION"), $(row.cells[2]));
    MoveTo($("#cmbUMEDIDA"), $(row.cells[3]));
    MoveTo($("#txtCANTIDAD"), $(row.cells[4]));
    MoveTo($("#txtPRECIO"), $(row.cells[5]));
    MoveTo($("#txtDESC"), $(row.cells[6]));
    MoveTo($("#txtTOTAL"), $(row.cells[7]));
    MoveTo($("#chkIVA"), $(row.cells[8]));




    $(r).data("codpro", codpro);
    r.cells[0].innerText = idpro;
    r.cells[1].innerText = producto;
    r.cells[2].innerText = observacion;
    $(r.cells[3]).data("coduni", coduni);
    r.cells[3].innerText = unidad;
    r.cells[4].innerText = cantidad;
    r.cells[5].innerText = precio;
    r.cells[6].innerText = desc;
    r.cells[7].innerText = total;
    r.cells[8].innerText = iva;

    $(r).attr('onclick', 'Edit(this)');
    $(row).removeAttr('onclick');

    $("#txtIDPRO").select();
    */
}


function Mark(obj) {
    var tr = $(obj).closest('table').attr('id'); ;
    CleanSelect(tr);   
    if ($(obj).css("background-color") == rgbcolor) {
        $(obj).css("background-color", "");
        $(obj).children("td").css("background-color", "");
        $('agregar').prop('disabled', false);
    }
    else {
        $(obj).css("background-color", color);
        $(obj).children("td").css("background-color", color);
    }
}


/*********************FUNCIONES PARA OCULTAR Y MOSTRAR SECCIONES**********************/

function HideCabecera() {
    $("#comcabecera").hide('fast', function () {
        var cambiosCSS1 =
                {
                    width: "100%"
                };
        $("#comdetalle").css(cambiosCSS1);
        $("#cab").css({ 'display': '' })
    });
}

function ShowCabecera() {
    $("#comdetalle").animate({
        width: "50%"
    }, 100, function () {
        $('#comcabecera').show("fast");
        $("#cab").css({ 'display': 'none' })
    });
}

function HideDetalle() {
    $("#comdetalle").hide('fast', function () {
        var cambiosCSS1 =
                {
                    width: "100%"
                };
        $("#comcabecera").css(cambiosCSS1);
        $("#det").css({ 'display': '' })
    });
}

function ShowDetalle() {
    $("#comcabecera").animate({
        width: "49%"
    }, 100, function () {
        $('#comdetalle').show("fast");
    });
    $("#det").css({ 'display': 'none' })

}


/***********************SAVE FUNCTIONS ***********************************/

function ClearValidate() {
    var controles = $("#comcabeceracontent").find('[data-obligatorio="True"]');
    $("#comcabeceracontent").find('[data-obligatorio="True"]').each(function () {
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


    var htmltable = $("#tdes")[0];
    if (htmltable.rows.length < 2) {
        retorno = false;
        mensajehtml += "Es necesario ingresar al menos una factura  a la planilla <br>";
    }


    if (!retorno) {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert(mensajehtml, 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
    return retorno;
}
function CleanSelect(table) {
    if ("tdatos" != table) {
        var rows = $("#tdatos tr");
        $(rows).css("background-color", "");
        $(rows).children("td").css("background-color", "")
        if (asig != 2) {
            $('#quitar').prop('disabled', false);
            $('#agregar').prop('disabled', true);
        }
    }
    if ("tdes" != table) {
        var rows = $("#tdes tr");
        $(rows).css("background-color", "");
        $(rows).children("td").css("background-color", "")
        if (asig != 2) {
            $('#quitar').prop('disabled', true);
            $('#agregar').prop('disabled', false);
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
    var htmltable = $("#tdes")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var obj = GetDcomdocObj(htmltable.rows[r]);
        if (obj != null)
            detalle[r] = obj;
    }
    return detalle;
}


function GetDcomdocObj(row) {
    var obj = {};
    obj["plc_comprobante"] = parseInt($(row).data("codpro").toString());
    obj["plc_comprobante_key"] = parseInt($(row).data("codpro").toString());
    obj["plc_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["plc_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    obj = SetAuditoria(obj);
    return obj;

}



function GetCcomdocObj() {
    var obj = {};
    obj["cdoc_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cdoc_comprobante"] = $("#txtcodigocomp").val();
    obj["cdoc_politica"] = parseInt($("#txtCODPOL").val());
    obj["cdoc_listaprecio"] = parseInt($("#txtCODLIS").val());
    obj["cdoc_nombre "] = $("#txtNOMBRES").val();
    obj["cdoc_ced_ruc"] = $("#txtRUC").val();
    obj["detalle"] = GetDetalle();
    return obj;
}
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
    obj["cenv_destinatario"] = parseInt($("#txtCODDES").val());
    obj["cenv_nombres_des"] = $("#txtNOMBRESDES").val();
    obj["cenv_direccion_des"] = $("#txtDIRECCIONDES").val();
    obj["cenv_telefono_des"] = $("#txtTELEFONODES").val();
    obj["cenv_ruta"] = $("#cmbRUTA").val();
    obj["cenv_estado_ruta"] = 0; //0 estado de ruta no asignado, 1 asignado pero puede cambiar, 2 estado asignado definitivo
    obj["cenv_observacion"] = $("#txtENTREGADES").val();
    return obj;
}

function GetComprobanteObj() {
    var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHACOMP").val()); // $("#txtFECHA_P").datepicker("getDate");
   
    var almacen = parseInt($("#txtCODALMACEN").val());
    var pventa = parseInt($("#txtCODPVENTA").val());
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = $("#txtcodigocomp").val();
    obj["com_tipodoc"] = $("#txttipodoc").val();
    obj["com_ctipocom"] = parseInt($("#txtCTIPOCOM").val());  //3 REC
    obj["com_numero"] = $("#txtNUMERO").val();
    obj["com_fecha"] = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate(), now.getHours(), now.getMinutes(), now.getSeconds(), now.getMilliseconds());
    obj["com_doctran"] = $("#numerocomp").html();
    obj["com_periodo"] = currentDate.getFullYear();
    obj["com_almacen"] = almacen;
    obj["com_pventa"] = pventa;
    obj["com_codclipro"] = parseInt($("#txtCODSOCIO").val());    
    obj["com_agente"] = parseInt($("#txtCODVEN").val());
    obj["com_concepto"] = $("#txtCONCEPTO").val();
    obj["com_ruta"] = parseInt($("#txtCODRUTA").val());
    obj["com_vehiculo"] = parseInt($("#txtCODVEHICULO").val());
    obj["com_socio"] = parseInt($("#txtCODSOCIO").val());
    obj["com_estado"] = asig;
    obj["planillas"] = GetDetalle();
    obj["total"] = GetTotalObj();
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}






function SaveObj() {
    if (!saving) {
        if (ValidateForm()) {
            asig = 2;
            $("#txtESTADO").val(asig);
            var fecha = $("#txtFECHACOMP").val() + " " + $("#txtHOURSAL").val();
            $("#txtFECHACOMP").val(fecha);
            var jsonText = GetComprobanteObj(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   

            $("#save").attr("disabled", true);
            saving = true;

            CallServer(formname + "/SaveObject", jsonText, 10);
        }
    }
}


function AsigObj() {
    if (!saving) {
        if (ValidateForm()) {
            asig = 1;
            $("#txtESTADO").val(asig);
            var fecha = $("#txtFECHACOMP").val() + " " + $("#txtHOURSAL").val();
            $("#txtFECHACOMP").val(fecha);
            var jsonText = GetComprobanteObj(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM
            $("#save").attr("disabled", false);
            saving = false;
            CallServer(formname + "/SaveObject", jsonText, 10);
        }
    }
}


function SaveObjResult(data) {
    $("#save").attr("disabled", false);
    saving = false;

    if (data != "") {
        if (data.d == "OK") {
            jQuery.alerts.dialogClass = 'alert-success';
            jAlert('Comprobante guardado exitosamente...', 'Éxito', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al guardar el comprobante...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
        disablefiltros();

    }
}	

/****************************FUNCIONES MANEJO DE TECLAS*******************************/

function RowUp() {
    var r = $("#txtCODPRO")[0].parentNode.parentNode;
    if ($(r).prev().length > 0)
        Edit($(r).prev()[0]);
}

function RowDown() {
    var r = $("#txtCODPRO")[0].parentNode.parentNode;
    if ($(r).next().length > 0)
        Edit($(r).next()[0]);
    else {
        if ($("#txtCODPRO").val() != "")
            AddEditRow();
    }
}


function IsOpen(id) {
    return $("#" + id).autocomplete('widget').is(':visible');
}

$(window).keydown(function (event) {
    var id = document.activeElement.id;
    var code = event.keyCode;
    var char = event.char;
    $("#mensaje").html(id + " " + code);

    if (id == "txtIDPRO" || id == "txtOBSERVACION" || id == "txtCANTIDAD" || id == "txtDESC") {
        if (id == "txtIDPRO" && IsOpen(id))
            code = -1;

        if (code == 38) { //Flecha Arriba
            RowUp();
        }
      
        if (code == 13 || code == 40) { //ENTER O Flecha ABAJO
            RowDown();
        }
    }
    if (!IsNumeric(id, code, char))
        event.preventDefault();
});

$(window).keyup(function (event) {
    var id = document.activeElement.id;
    var code = event.keyCode
    MustCalculate(id);

});

function IsNumeric(id, code, char) {
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
}


function MustCalculate(id) {
    switch (id) {
        case "txtCANTIDAD":
        case "txtDESC":
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

    }

}