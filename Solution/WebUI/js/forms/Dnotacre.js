 //Archivo:          Dnotacre.js
//Descripción:      Contiene las funciones comunes para la interfaz de gestion de Dnotacrees de deudas 
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Octubre 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfDnotacre.aspx";
var menuoption = "Dnotacre";

var afectacionobj = null;
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
    
    $("#contabilizacion").on("click", ContObj);
    $("#close").on("click", SaveObj);
    var codigocomp = $("#txtcodigocomp").val();


    if (codigocomp <= 0) {

        $("#contabilizacion").css({ 'display': 'none' });
        $("#print").css({ 'display': 'none' });
        $("#close").css({ 'display': 'none' });
        $("#invo").css({ 'display': 'none' });
    }
    // $("#print").on("click", { titulo: "Impresion Nota credito/debito", parametros: "parameter1=" + codigocomp, reporte: 'NOTCRE' }, PrintReport);
    $("#print").on("click",PrintObj);
    $("#invo").css({ 'display': 'none' });
    $("#view").css({ 'display': '' });
    $("#view").on("click", ViewObj);
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
                SaveObjResult(data);
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
    var now = new Date();
    var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHACOMP").val());
    obj["com_fecha"] = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate(), now.getHours(), now.getMinutes(), now.getSeconds(), now.getMilliseconds());

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

    var obj = {};
    obj["empresa"] = empresasigned["emp_codigo"];
    obj["com_codclipro"] = $("#txtCODPER").val();
    SetAutocompleteByIdParams("txtCODCOM", obj);

    //SetAutocompleteById("txtCODCOM");
    if ($("#txtESTADO").val() == $("#txtCERRADO").val()) {
        $("#save").css({ 'display': 'none' });
        $("#close").css({ 'display': 'none' });
        $("#adddet").css({ 'display': 'none' });
        $("#deldet").css({ 'display': 'none' });
        $("#movedet").css({ 'display': 'none' });
    }
}

function SetFormDetalle() {
    SetAutocompleteById("txtIDTIPO");
    $("#txtVALOR").on("change", CalculaTotales);
    $("#chkIVA").on("change", CalculaTotales);
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
    $("#txtIDTIPO").val("");
    $("#txtCODTIPO").val("");
    $("#txtNOMBRETIPO").val("");
    $("#txtVALOR").val(0);
    $("#txtIDTIPO").select();
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

    if (idobj == "txtIDTIPO") {
        return {
            label: item.tnc_id + "," + item.tnc_nombre,
            value: item.tnc_id,
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

        if (parseInt($("#txttclipro").val()) == cCliente) {
            return {
                //label: item.doctran,
                //value: item.doctran,                
                label: item.com_doctran,
                value: item.com_doctran,                
                info: item

            }
        }
        if (parseInt($("#txttclipro").val()) == cProveedor) {
            return {
                label: item.com_doctran,
                value: item.com_documento_obli + ',' + item.com_doctran,
                info: item

            }
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

        var obj = {};
        obj["com_codclipro"] = item.info.per_codigo;
        SetAutocompleteByIdParams("txtCODCOM", obj);
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

    if (idobj == "txtIDTIPO") {
        $("#txtCODTIPO").val(item.info.tnc_codigo);
        $("#txtNOMBRETIPO").val(item.info.tnc_nombre);
        $("#txtIDTIPO").val(item.info.tnc_id);
       // ShowCampos();
    }
    if (idobj == "txtCODCOM") {        

        //$("#txtCODCOM").val(item.info.doctran);
        //$("#txtCOMPROBANTE").val(item.info.codigo);      
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
    var subtotal0 = 0;
    var subtotalIVA = 0;
    var desc0 = 0;
    var descIVA = 0;


    for (var r = 0; r < htmltable.rows.length; r++) {
        var c = htmltable.rows[r].cells.length - 3; //celda cantidad
       
        var hijotot = $(htmltable.rows[r].cells[c ]).children("input");
        var hijoiva = $(htmltable.rows[r].cells[c+1]).children("input");
        var cant = 0;
        var prec = 0;
        var desc = 0;
        var valor = 0;


        var iva = false;
        if (hijotot.length > 0) {
          
            valor = $(hijotot).val();
            iva = $(hijoiva).is(':checked') ? true : false;
        }
        else {
           
            valor = $(htmltable.rows[r].cells[c]).text();
            iva = ($(htmltable.rows[r].cells[c + 1]).text() == "SI") ? true : false;
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



function CalculaAfectacion() {
    var htmltable = $("#tdafec_P")[0];
    var total = 0;


    for (var r = 1; r < htmltable.rows.length; r++) {
        var c = htmltable.rows[r].cells.length - 1; //celda cantidad
        var hijoval = $(htmltable.rows[r].cells[c]).children("input");
        var valor = 0;

        if (hijoval.length > 0) {
            valor = $(hijoval).val();
        }        
        total += ($.isNumeric(valor)) ? parseFloat(valor) : 0;

    }

    var saldo = parseFloat($("#txtVALOR_P").val()) - total;
    $("#txtSALDO_P").val(CurrencyFormatted(saldo));
    $("#tdafec_P_f8").html(CurrencyFormatted(total));
}


/*********************FUNCIONES PARA AGREGAR y EDITAR UN DETALLE**********************/

function AddEditRow() {
    var newrow = $("<tr data-codtipo='" + $("#txtCODTIPO").val() + "'  onclick='Edit(this);'></tr>");
    //var newrow = $("<tr " + strdata + " onclick='Edit(this);'></tr>");
    newrow.append("<td class='' >" + $("#txtIDTIPO").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtNOMBRETIPO").val() + "</td>");
    newrow.append("<td class='right' >" + $("#txtVALOR").val() + "</td>");
    newrow.append("<td class='center' >" + ($("#chkIVA").is(':checked') ? "SI" : "NO") + "</td>");
    newrow.append("<td class='center' ><div class='removablerow' onclick='RemoveRow(this)'><span class=\"icon-trash\" ></span></div></td>");

    var editrow = $("#editrow");
    newrow.insertBefore(editrow);

    $("#txtCODTIPO").val("");
    $("#txtIDTIPO").val("");
    $("#txtNOMBRETIPO").val("");
    $("#txtVALOR").val(0);
    $("#chkIVA").prop("checked", false);
    $("#txtIDTIPO").select();
}


function MoveTo(control, destino) {
    var obj = $(control).detach();
    $(destino).append(obj);
}


function EditAfec(row) {
    var valor = parseFloat($(row.cells[8]).text());
    $(row.cells[8]).text("");
    var inputvalor = $("<input type='text' style='width:auto;' value='" + valor + "'/>");
    $(row.cells[8]).append(inputvalor);
}

function Edit(row) {

    var r = $("#txtCODTIPO")[0].parentNode.parentNode;
    var cod = $("#txtCODTIPO").val();
    var id = $("#txtIDTIPO").val();
    var nombre = $("#txtNOMBRETIPO").val();
    var valor = $("#txtVALOR").val();
    var iva = ($("#chkIVA").is(':checked') ? "SI" : "NO");



    $("#txtCODTIPO").val($(row).data("codtipo").toString());
    $("#txtIDTIPO").val($(row.cells[0]).text()); $(row.cells[0]).text("");
    $("#txtNOMBRETIPO").val($(row.cells[1]).text()); $(row.cells[1]).text("");
    $("#txtVALOR").val($(row.cells[2]).text()); $(row.cells[2]).text("");
    $("#chkIVA").prop("checked", (($(row.cells[3]).text() == "SI") ? true : false)); $(row.cells[3]).text("");
    $("#txtIDTIPO").focus();

  


    MoveTo($("#txtIDTIPO"), $(row.cells[0]));
    MoveTo($("#txtCODTIPO"), $(row.cells[0]));
    MoveTo($("#txtNOMBRETIPO"), $(row.cells[1]));
    MoveTo($("#txtVALOR"), $(row.cells[2]));
    MoveTo($("#chkIVA"), $(row.cells[3]));


    $(r).data("codtipo", cod);


    $(r.cells[0]).text(id);
    $(r.cells[1]).text(nombre);
    $(r.cells[2]).text(valor);
    $(r.cells[3]).text(iva);
    $(r).attr('onclick', 'Edit(this)');
    $(row).removeAttr('onclick');

    $("#txtIDTIPO").select();
   // ShowCampos();

}

function ShowCampos() {

    var htmltable = $("#tddatos")[0];
    var total = 0;

    for (var r = 0; r < htmltable.rows.length; r++) {
        $(htmltable.rows[r]).hide();
    }


    var id = $("#txtIDTIPO").val();
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
    if (parseInt($("#txttclipro").val()) == cCliente)
    obj["tot_impuesto"] = 2; //Impuesto Venta    
    if (parseInt($("#txttclipro").val()) == cProveedor)
        obj["tot_impuesto"] = 1; //Impuesto Compra    
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
            obj["dfp_tipopagoid"] = $("#txtIDTIPO").val();
            obj["dfp_tipopagonombre"] = $("#txtNOMBRETIPO").val();
            obj["dnc_valor"] = parseFloat($("#txtVALOR").val());
            obj["dnc_cheque"] = $("#chkIVA").is(':checked') ? 1 : 0;
            
        }
        return null;
    }
    else {

        obj["dnc_tiponc"] = parseInt($(row).data("codtipo").toString());
        obj["dfp_tipopagoid"] = $(row.cells[0]).text();
        obj["dfp_tipopagonombre"] = $(row.cells[1]).text();
        obj["dnc_valor"] = parseFloat($(row.cells[2]).text());
        obj["dnc_cheque"] = (($(row.cells[3]).text() == "SI") ? 1 : 0);

      
    }

    obj = SetAuditoria(obj);
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
    obj = SetAuditoria(obj);
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

    obj["com_tclipro"] = $("#txtTCLIPRO").val();
    obj["com_concepto"] = $("#txtCONCEPTO").val();
    obj["com_periodo"] = currentDate.getFullYear();
    obj["com_almacen"] = almacen;
    obj["com_pventa"] = pventa;
    obj["com_codclipro"] = parseInt($("#txtCODPER").val());
    obj["com_agente"] = parseInt($("#txtCODVEN").val());
    obj["notascre"] = GetDetalle();
    obj["total"] = GetTotalObj();
    obj["ccomdoc"] = GetCcomdocObj();
    obj = SetAuditoria(obj);
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


function Afectacion(obj) {
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
}




function SaveObj() {
    if (!saving) {
        if (ValidateForm()) {
            var compobj = GetComprobanteObj();
            if (afectacionobj == null)
                Afectacion(compobj);
            else {
                var obj = {};
                obj["recibo"] = compobj;
                obj["afectacion"] = afectacionobj;
                if ($(this)[0].id == "save")
                    obj["com_estado"] = parseInt($("#txtCREADO").val());
                else if ($(this)[0].id == "close")
                    obj["com_estado"] = parseInt($("#txtCERRADO").val());
                var jsonText = JSON.stringify({ objeto: obj });
                jConfirm('¿Está seguro que desea guardar la nota ? \n Luego no se podra modificar', 'Guardar', function (r) {
                    $("#save").attr("disabled", true);
                    saving = true;
                    CallServer(formname + "/SaveObject", jsonText, 5);

                });

            }

        }
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
    window.location = "wfDnotacrePrint.aspx?codigocomp=" + $("#txtcodigocomp").val();
}

function SaveObjResult(data) {
    $("#save").attr("disabled", false);
    saving = false;
    if (data != "") {
        if (data.d == "OK") {
            jQuery.alerts.dialogClass = 'alert-success';
            jAlert('Nota Guardada guardada exitosamente...', 'Éxito', function () {
                jQuery.alerts.dialogClass = null; // reset to default
                location.reload();
            });

        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al guardar la Nota...', 'Error', function () {
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
    var r = $("#txtCODTIPO")[0].parentNode.parentNode;
    if ($(r).prev().length > 0)
        Edit($(r).prev()[0]);
}

function RowDown() {
    var r = $("#txtCODTIPO")[0].parentNode.parentNode;
    if ($(r).next().length > 0)
        Edit($(r).next()[0]);
    else {
        if ($("#txtCODTIPO").val() != "")
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

    if (id == "txtIDTIPO" || id == "txtVALOR") {

        if (id == "txtIDTIPO" && IsOpen(id))
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
            if (id != "txtIDTIPO")
                RowDown();
        }
    }
    if (!IsNumeric(id, code, char))
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
        case "txtVALOR":        
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
