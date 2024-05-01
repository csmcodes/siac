//Archivo:          GuiaRemision.js
//Descripción:      Contiene las funciones comunes para la interfaz de gestion de guias de remision
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Septiembre 2016
//2016. SIAC. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfGuiaRemision.aspx";
var menuoption = "GuiaRemision";


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
    // $("#save").on("click", Despachar);
    //Despachar()
    $("#print").on("click", PrintObj);
    $("#close").on("click", SaveObj);
    
    $("#contabilizacion").css({ 'display': 'none' });
    

    var codigocomp = $("#txtcodigocomp").val();
    if (codigocomp <= 0) {
        
        $("#print").css({ 'display': 'none' });
        $("#close").css({ 'display': 'none' });
        $("#parent-selector :input").attr("disabled", true);

    }

    $("#invo").css({ 'display': 'none' });

    LoadCabecera();    
    GetPrintVersion();
    GetPrintHTML();
    GetPrintFormat();
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
            if (retorno == 9)
                CallDespacharResult(data);
            if (retorno == 10)
                SaveObjResultDespachar(data);
            if (retorno == 11)
                GetAllRutasResult(data);
            if (retorno == 12)
                GetDatosPoliticaResult(data);
            if (retorno == 13)
                GetPrintVersionResult(data);
            if (retorno == 14)
                GetPrintHTMLResult(data);
            if (retorno == 15)
                GetPrintFormatResult(data);

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

function GetPrintHTML() {

    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
    obj["crea_usr"] = usuariosigned["usr_id"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer("ws/Metodos.asmx/GetPrintHTML", jsonText, 14);
}


function GetPrintHTMLResult(data) {
    if (data != "") {
        $('#txtprinthtml').val(data.d);
    }
}


function GetPrintFormat() {

    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
    obj["crea_usr"] = usuariosigned["usr_id"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer("ws/Metodos.asmx/GetPrintFormat", jsonText, 15);
}


function GetPrintFormatResult(data) {
    if (data != "") {
        $('#txtprintformat').val(data.d);
    }
}


function LoadCabecera() {

        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = $("#txtcodigocomp").val();
        obj["com_almacen"] = parseInt($("#txtCODALMACEN").val());
        obj["com_pventa"] = parseInt($("#txtCODPVENTA").val());
        obj["com_tipodoc"] = parseInt($("#txttipodoc").val());

        obj["crem_factura"] = $("#txtcodigocompref").val();

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

    obj["crem_factura"] = $("#txtcodigocompref").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDetalle", jsonText, 1);

}

function LoadDetalleResult(data) {
    if (data != "") {
        $('#comdetallecontent').html(data.d);
    }
    SetFormDetalle();

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

    SetAutocompleteById("txtNOMBRESREM");
    SetAutocompleteById("txtNOMBRESDES");   

}


function SetFormDetalle() {
    
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
    StopPropagation();
}





function CleanRow() {
    $("#txtDESCRIPCION").val("");    
    $("#txtCANTIDAD").val(0);
    return false;
}


function SetAutoCompleteObj(idobj, item) {
    
    if (idobj == "txtNOMBRESREM" || idobj == "txtNOMBRESDES" ) {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_apellidos + " " + item.per_nombres,
            info: item
        }
    }
    
}

function GetAutoCompleteObj(idobj, item) {
   
    if (idobj == "txtNOMBRESREM") {
        $("#txtCODREM").val(item.info.per_codigo);
        $("#txtCIRUCREM").val(item.info.per_ciruc);
        $("#txtNOMBRESREM").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtDIRECCIONREM").val(item.info.per_direccion);        
        //alert(ui.item.label);
    }
    if (idobj == "txtNOMBRESDES") {
        $("#txtCODDES").val(item.info.per_codigo);
        $("#txtCIRUCDES").val(item.info.per_ciruc);
        $("#txtNOMBRESDES").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtDIRECCIONDES").val(item.info.per_direccion);
    }
}





/*********************FUNCIONES PARA AGREGAR y EDITAR UN DETALLE**********************/

function AddEditRow() {

    var r = $("#txtDESCRIPCION")[0].parentNode.parentNode;

    var newrow = $("<tr onclick='Edit(this);'></tr>");
    newrow.append("<td class='' style='overflow:hidden; width:300px;'>" + $("#txtDESCRIPCION").val() + "</td>");

    newrow.append("<td class='center' >" + $("#txtCANTIDAD").val() + "</td>");
    newrow.append("<td class='center' ><div class='removablerow' onclick='RemoveRow(this)'><span class=\"icon-trash\" ></span></div></td>");

    var editrow = $("#tdinvoice").find("#editrow");
    newrow.insertBefore(editrow);
    
    $("#txtDESCRIPCION").val("");
    $("#txtCANTIDAD").val(0);
}



function MoveTo(control, destino) {
    var obj = $(control).detach();
    $(destino).append(obj);
}


function Edit(row) {

    var r = $("#txtDESCRIPCION")[0].parentNode.parentNode;

    var descripcion = $("#txtDESCRIPCION").val();
    var cantidad = $("#txtCANTIDAD").val();

    $("#txtDESCRIPCION").val($(row.cells[0]).text()); $(row.cells[0]).text("");
    $("#txtCANTIDAD").val($(row.cells[1]).text()); $(row.cells[1]).text("");

    $("#txtDESCRIPCION").focus();

    MoveTo($("#txtDESCRIPCION"), $(row.cells[0]));
    MoveTo($("#txtCANTIDAD"), $(row.cells[1]));

    $(r.cells[0]).text(descripcion);
    $(r.cells[1]).text(cantidad);
    

    $(r).attr('onclick', 'Edit(this)');
    $(row).removeAttr('onclick');

    $("#txtDESCRIPCION").select();

}



/***********************SAVE FUNCTIONS ***********************************/


function GetDetalle() {
    var detalle = new Array();
    var htmltable = $("#tdinvoice")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var obj = GetDcomremObj(htmltable.rows[r]);
        if (obj != null)
            detalle[r] = obj;
    }
    return detalle;
}

function GetDcomremObj(row) {
    var obj = {};
    obj["drem_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["drem_comprobante"] = $("#txtcodigocomp").val();

    var hijocodpro = $(row.cells[0]).children("input");
    if (hijocodpro.length > 0) {

        obj["drem_descripcion"] = $("#txtDESCRIPCION").val();
        obj["drem_cantidad"] = parseFloat($("#txtCANTIDAD").val());

    }
    else {

        obj["drem_descripcion"] = $(row.cells[0]).text();
        obj["drem_cantidad"] =  parseFloat($(row.cells[1]).text());
        
    }
    obj = SetAuditoria(obj);
    return obj;
}




function GetCcomremObj() {
    var obj = {};
    obj["crem_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["crem_comprobante"] = $("#txtcodigocomp").val();

    obj["crem_factura"] = $("#txtCODFACTURA").val();
    var dateini = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHAINI").val());    
    obj["crem_trasladoini"] = dateini;
    var datefin = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHAFIN").val());
    obj["crem_trasladofin"] = datefin;    
    obj["crem_motivo"] = $("#txtMOTIVO").val();
    obj["crem_nroaduana"] = $("#txtNROADUANA").val();

    obj["crem_remitente"] = $("#txtCODREM").val();
    obj["crem_ciruc_rem"] = $("#txtCIRUCREM").val();
    obj["crem_nombres_rem"] = $("#txtNOMBRESREM").val();
    obj["crem_direccion_rem"] = $("#txtDIRECCIONREM").val();

    obj["crem_destinatario"] = $("#txtCODDES").val();
    obj["crem_ciruc_des"] = $("#txtCIRUCDES").val();
    obj["crem_nombres_des"] = $("#txtNOMBRESDES").val();
    obj["crem_direccion_des"] = $("#txtDIRECCIONDES").val();

    obj["crem_chofer"] = $("#txtCODCHO").val();
    obj["crem_ciruc_cho"] = $("#txtCIRUCCHO").val();
    obj["crem_nombres_cho"] = $("#txtNOMBRESCHO").val();
    obj["crem_placa"] = $("#txtPLACA").val();
    

    obj = SetAuditoria(obj);
    return obj;
}



function GetComprobanteObj() {
    //var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHACOMP").val()); // $("#txtFECHA_P").datepicker("getDate");

    var now = new Date();
    //var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHA_P").val());

    var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHACOMP").val());
    //obj["fecha"] = $("#txtFECHA_P").datepicker("getDate") ;            

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
    //obj["com_concepto"] = $("#txtOBSERVACIONES").val();
    //obj["com_codclipro"] = parseInt($("#txtCODPER").val());
    //obj["com_agente"] = parseInt($("#txtCODVEN").val());
    //obj["com_token"] = $("#txtTOKEN").val();//NUEVO CONTROL    
    obj = SetAuditoria(obj);
    return obj;
    //var jsonText = JSON.stringify({ objeto: obj });
    //return jsonText;
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


    var detalle = GetDetalle();
    if (detalle.length == 0) {
        retorno = false;
        mensajehtml += "Es necesario ingresar al menos un detalle al comprobante<br>";
    }

    /*var htmltable = $("#tdinvoice")[0];
    if (htmltable.rows.length < 3) {
     
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

        var obj = {};
        obj["comprobante"] = GetComprobanteObj();
        obj["ccomrem"] = GetCcomremObj();
        obj["dcomrem"] = GetDetalle();

        var jsonText = JSON.stringify({ objeto: obj });
        
        $("#save").attr("disabled", true);
        CallServer(formname + "/SaveObject", jsonText, 5);

        
    }
}


function PrintObj() {

    var opciones = "toolbar=no, scrollbars=no, resizable=yes, top=50, left=100, width=800, height=600";
    var printversion = $("#txtprintversion").val();

    window.open("wfGuiaRemisionPrint.aspx?codigo=" + $("#txtcodigocomp").val() + "&usuario=" + usuariosigned["usr_id"] + "&empresa=" + empresasigned["emp_codigo"] + "&tipodoc=" + $("#txttipodoc").val() + "&formato=" + $("#txtprintformat").val(), "Imp", opciones);

}


function SaveObjResult(data) {
    $("#save").attr("disabled", false);
    if (data != "") {
        //if (data.d != "ERROR") {
        if (data.d != "-1") {
            jQuery.alerts.dialogClass = 'alert-success';
            jAlert('Comprobante guardado exitosamente...', 'Éxito', function () {
                $("#txtcodigocomp").val(data.d);
                PrintObj();
                window.location = window.location;
                //window.location.reload();
                /*jQuery.alerts.dialogClass = null; // reset to default

                jConfirm('¿Desea imprimir el comprobante este momento?', 'Pregunta', function (r) {
                    if (r) {
                        $("#txtcodigocomp").val(data.d);
                        PrintObj();
                    }
                    location.reload();
                });*/
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
/****************************FUNCIONES MANEJO DE TECLAS*******************************/

function RowUp() {
    var r = $("#txtDESCRIPCION")[0].parentNode.parentNode;
    if ($(r).prev().length > 0)
        Edit($(r).prev()[0]);
}

function RowDown() {
    var r = $("#txtDESCRIPCION")[0].parentNode.parentNode;
    if ($(r).next().length > 0)
        Edit($(r).next()[0]);
    else {
        if ($("#txtDESCRIPCION").val() != "")
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

    if (id == "txtDESCRIPCION" || id == "txtCANTIDAD" || $("#" + id).hasClass('calculo')) {

        if (id == "txtDESCRIPCION" && IsOpen(id))
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
            //if (id != "txtDESCRIPCION")
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


function GetCallPersona(obj, id) {
   
    if (id == "btncallrem") {
        $("#txtCODREM").val(obj.per_codigo);
        $("#txtCIRUCREM").val(obj.per_ciruc);
        $("#txtNOMBRESREM").val(obj.per_apellidos + " " + obj.per_nombres);
        $("#txtDIRECCIONREM").val(obj.per_direccion);     

    }

    
    if (id == "btncalldes") {
        $("#txtCODDES").val(obj.per_codigo);
        $("#txtCIRUCDES").val(obj.per_ciruc);
        $("#txtNOMBRESDES").val(obj.per_apellidos + " " + obj.per_nombres);
        $("#txtDIRECCIONDES").val(obj.per_direccion);
    
    }

}


function asigRem() {
    $("#txtIDREM").val($("#txtCODCLIPRO").val());
    $("#txtCODREM").val($("#txtCODPER").val());
    $("#txtCIRUCREM").val($("#txtCIRUC").val());
    $("#txtNOMBRESREM").val($("#txtNOMBRES").val());
    $("#txtDIRECCIONREM").val($("#txtDIRECCION").val());
    $("#txtTELEFONOREM").val($("#txtTELEFONO").val());


}

function asigDes() {
    $("#txtIDDES").val($("#txtCODCLIPRO").val());
    $("#txtCODDES").val($("#txtCODPER").val());
    $("#txtCIRUCDES").val($("#txtCIRUC").val());
    $("#txtNOMBRESDES").val($("#txtNOMBRES").val());
    $("#txtDIRECCIONDES").val($("#txtDIRECCION").val());
    $("#txtTELEFONODES").val($("#txtTELEFONO").val());
}


function CleanPersona(id) {
    
    if (id == "btncleanrem") {
        $("#txtCODREM").val("");
        $("#txtCIRUCREM").val("");
        $("#txtNOMBRESREM").val("");
        $("#txtDIRECCIONREM").val("");
 
    }

   if (id == "btncleandes") {
        $("#txtCODDES").val("");
        $("#txtCIRUCDES").val("");
        $("#txtNOMBRESDES").val("");
        $("#txtDIRECCIONDES").val("");
   
    }
}
