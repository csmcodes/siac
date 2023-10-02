//Archivo:          CalculoPrecio.js
//Descripción:      Contiene las funciones comunes para la interfaz de calculo precios
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Diciembre 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfBusquedaComprobantes.aspx";
var menuoption = "BusquedaComprobantes";

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
                CallDlistaPrecioResult(data);
            if (retorno == 4)
                SaveObjResult(data);
            if (retorno == 5)
                RemoveObjectsResult(data);
            if (retorno == 6)
                DeleteObjResult(data);
            if (retorno == 7)
                GetPieResult(data);
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
    var jsonText = JSON.stringify({});
    CallServer(formname + "/GetCabecera", jsonText, 0);
}

function LoadCabeceraResult(data) {
    if (data != "") {
        $('#comcabeceracontent').html(data.d);
      
        LoadDetalle();
    }
    SetForm(); //Depende de cada js.
    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    });

}

function LoadDetalle() {
    $('#comdetallecontent').empty();
    var jsonText = JSON.stringify({});
    CallServer(formname + "/GetDetalle", jsonText, 1);
}

function LoadDetalleResult(data) {
    if (data != "") {
        $('#comdetallecontent').html(data.d);
        LoadDetalleData();
    }
    SetFormDetalle();
    //EditableRow("detalletabla");
}

function LoadDetalleData() {
    var obj = {};
    obj["com_periodo"] = parseInt($("#txtPERIODO_S").val());
    obj["com_mes"] = parseInt($("#txtMES_S").val());
    obj["com_fecha"] = $("#txtFECHA_S").val();
    obj["com_ctipocom"] = parseInt($("#cmbCTIPOCOM_S").val());
    obj["com_almacen"] =parseInt( $("#cmbALMACEN_S").val());
    obj["com_serie"] =parseInt( $("#txtSERIE_S").val());
    obj["com_numero"] = parseInt($("#txtNUMERO_S").val());
    var estado = $("#cmbESTADO_S option:selected").text();
    if (estado.length>0)
        obj["com_estado"] = parseInt($("#cmbESTADO_S").val());
    else
        obj["com_estado"] =-1;
    obj["com_tipodoc"] = parseInt($("#cmbTIPODOC_S").val());
    obj["com_concepto"] = $("#txtCONCEPTO_S").val();
    obj["com_ref_comprobante"] = $("#txtREFERENCIA_S").val();
    obj["com_aut_tipo"] = parseInt($("#cmbAUTORIZ_S").val());


    estado = $("#cmbCONTABILZA_S option:selected").text();
    if (estado!= "--Activo--")
        obj["com_nocontable"] = parseInt($("#cmbCONTABILZA_S").val());
    else
        obj["com_nocontable"] = -1;


    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
   
    CallServer(formname + "/GetDetalleData", jsonText, 2);
}

function LoadDetalleDataResult(data) {
    if (data != "") {
        $('#tddatos').append(data.d);
    }

}


function SetForm() {

    $("#txtPERIODO_S").on("change", LoadDetalle);
    $("#txtMES_S").on("change", LoadDetalle);
    $("#txtFECHA_S").on("change", LoadDetalle);
    $("#cmbCTIPOCOM_S").on("change", LoadDetalle);
    $("#cmbALMACEN_S").on("change", LoadDetalle);
    $("#txtSERIE_S").on("change", LoadDetalle);
    $("#txtNUMERO_S").on("change", LoadDetalle);
    $("#cmbESTADO_S").on("change", LoadDetalle);
    $("#cmbTIPODOC_S").on("change", LoadDetalle);
    
    $("#txtCONCEPTO_S").on("change", LoadDetalle);
    $("#txtREFERENCIA_S").on("change", LoadDetalle);
    $("#cmbAUTORIZ_S").on("change", LoadDetalle);
    $("#cmbCONTABILZA_S").on("change", LoadDetalle);


}

function SetFormDetalle() {
    //Tratamiento para los elemntos del detalle    
    $("#adddet").on("click", CallDlistaPrecio);
    $("#deldet").on("click", QuitSelected);
    $("#alldet").on("click", { tabla: "tddatos" }, SelectAllRows);
    $("#nonedet").on("click", { tabla: "tddatos" }, CleanSelectedRows);
    $("#comdetallecontent").on("scroll", scroll);
}

function scroll() {
    if ($("#comdetallecontent")[0].scrollHeight - $("#comdetallecontent").scrollTop() <= $("#comdetallecontent").outerHeight()) {
        LoadDetalleData();
    }
}

function DeleteObj() {   
        var compr = {};
    var id = parseInt($("#txtCODIGO_P").val());
    if (id != null) {
        compr["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        compr["com_codigo"] = parseInt(id);
    }
    var jsonText = JSON.stringify({ objeto: compr });
    CallServer(formname + "/DeleteAutorizacion", jsonText, 6);

}

function DeleteObjResult(data) {
    if (data != "") {
        if (data.d == "OK") {
            LoadDetalle();
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al eliminar los registros...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }

}

function AutorizarObj() {

    var id = parseInt($("#txtCODIGO_P").val());
    if (id != null) {
        window.location = "wfAutorizacion.aspx?codigocomp=" + id;
    }
}

function Select(obj) {
    
     var tr = $(obj).closest('table').attr('id'); ;
     CleanSelectedRows(tr);
    if ($(obj).css("background-color") == rgbcolor) {
        $(obj).css("background-color", "");
        $(obj).children("td").css("background-color", "");
        $('agregar').prop('disabled', false);
    }
    else {
        $(obj).css("background-color", color);
        $(obj).children("td").css("background-color", color);
        GetPie(  $(obj).data("id"));
    }
    //GetTotales();
}

function CallDlistaPrecio(id) {
 /*   var obj = {};
    if (id != null) {
        obj["dlpr_empresa"] = id.dlpr_empresa;
        obj["dlpr_listaprecio"] = id.dlpr_listaprecio;
        obj["dlpr_codigo"] = id.dlpr_codigo;
    }
    var jsonText = JSON.stringify({ objeto: obj });*/
    var compr = {};
    if (id != null) {
        compr["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        compr["com_codigo"] = parseInt(id);
    }
    var jsonText = JSON.stringify({ objeto: compr });
    CallServer("ws/Metodos.asmx/GetFormulario", jsonText, 3);
}

function CallDlistaPrecioResult(data) {
    if (data != "") {
        window.location = data.d;
    };
    /*$(".fecha").datepicker();
    //   $(".fecha").datepicker('setDate', new Date());
    $(".fecha").datepicker("option", "dateFormat", "dd/mm/yy");*/
}

function SetFormCalculo() {
}


function GetDlistaPrecioObj() {
    var obj = {};
    obj["dlpr_codigo"] = $("#txtCODIGO_P").val();
    obj["dlpr_codigo_key"] = $("#txtCODIGO_P").val();
    obj["dlpr_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["dlpr_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    obj["dlpr_listaprecio"] = parseInt($("#cmbLISTA_P").val());
    obj["dlpr_listaprecio_key"] = parseInt($("#cmbLISTA_P").val());
    obj["dlpr_almacen"] = $("#cmbALMACEN_P").val();
    obj["dlpr_producto"] = $("#cmbPRODUCTO").val();
    obj["dlpr_umedida"] = $("#cmbUMEDIDA").val();
    obj["dlpr_fecha_ini"] = $("#cmbFECHA_INI").val();
    obj["dlpr_fecha_fin"] = $("#cmbFECHA_FIN").val();
    obj["dlpr_precio"] = parseFloat($("#txtPRECIO").val());
    obj["dlpr_estado"] = GetCheckValue($("#chkESTADO"));
    obj["dlpr_idalmacen"] = $("#cmbALMACEN_P option:selected").text();
    obj["dlpr_nombreproducto"] = $("#txtIDPRO option:selected").text();
    obj["dlpr_nombreumedida"] = $("#cmbUMEDIDA option:selected").text();
    obj["dlpr_ruta"] = $("#cmbRUTA_P").val();
    obj = SetAuditoria(obj);
    return obj;
}

function CallDListaPrecioOK() {
    SaveObj();
    return true;
}

function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtIDPRO") {
        return {
            label: item.pro_id + "," + item.pro_nombre,
            value: item.pro_nombre,
            info: item
        }
    }
}

function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtIDPRO") {
        $("#txtIDPRO").val(item.info.pro_nombre);
        $("#cmbPRODUCTO").val(item.info.pro_codigo);
        //   $("#txtPRODUCTO").val(item.info.pro_nombre);
        //   LoadProduct();
        //alert(ui.item.label);
    }
}

////////////////////////////////
//FUNCIONES de Selección y Deselección
function Edit(obj) {
    // var tr = $(obj).closest('table').attr('id'); ;
    // CleanSelect(tr);
/*    if ($(obj).css("background-color") == rgbcolor) {
        $(obj).css("background-color", "");
        $(obj).children("td").css("background-color", "");
        $('agregar').prop('disabled', false);
    }
    else {
        $(obj).css("background-color", color);
        $(obj).children("td").css("background-color", color);
    }*/
    //GetTotales();

    var id = $(obj).data("id");
    CallDlistaPrecio(id);
}

function SelectAllRows(event) {
    var htmltable = $("#" + event.data.tabla)[0];
    var contador = 0;
    for (var r = 1; r < htmltable.rows.length; r++) {
        if ($(htmltable.rows[r]).css("background-color") != rgbcolor) {
            Select(htmltable.rows[r]);
        }
    }
}

function CleanSelectedRows(event) {
    var htmltable = $("#" + event)[0];
    var contador = 0;
    for (var r = 1; r < htmltable.rows.length; r++) {
        if ($(htmltable.rows[r]).css("background-color") == rgbcolor) {
        //    Select(htmltable.rows[r]);
            $(htmltable.rows[r]).css("background-color", "");
            $(htmltable.rows[r]).children("td").css("background-color", "");
            $('agregar').prop('disabled', false);
        }
    }
}



function GetPie(id) {
    var compr = {};
    if (id != null) {
        compr["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        compr["com_codigo"] = parseInt(id);
    }
    var jsonText = JSON.stringify({ objeto: compr });
   
    CallServer(formname + "/GetPie", jsonText, 7);
}

function GetPieResult(data) {
    if (data != "") {
        $('#compiecontent').html(data.d);
    }
}
//////////////////////////////////////////////////////////////////////////////////
////////////////FUNCIONES PARA QUITAR LAS FILAS SELECCIONADAS/////////////////////

function QuitSelected() {
    jConfirm('¿Está seguro que desea eliminar los registros seleccionados?', 'Eliminar', function (r) {
        if (r)
            RemoveObjects();
    });
}

function RemoveObjects() {
    var detalle = new Array();
    var htmltable = $("#tddatos")[0];
    var rows = $("#tddatos").find("> tbody > tr ");
    $("#tddatos").find("> tbody > tr ").each(function () {
        if ($(this).css("background-color") == rgbcolor) {
            var id = $(this).data("id");
            var obj = {};
            obj["dlpr_empresa"] = id.dlpr_empresa;
            obj["dlpr_listaprecio"] = id.dlpr_listaprecio;
            obj["dlpr_codigo"] = id.dlpr_codigo;
            detalle[detalle.length] = obj;
        }
    });
    var jsonText = JSON.stringify({ objeto: detalle });
    CallServer(formname + "/RemoveObjects", jsonText, 5);
}

function RemoveObjectsResult(data) {
    if (data != "") {
        if (data.d == "OK") {
            LoadDetalle();
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al eliminar los registros...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}

//////////////////////////////////////////////////////////////////////////////////
///////////////////FUNCIONES PARA GUARDAR SAVE/////////////////////////////////////

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
        var obj = GetDlistaPrecioObj();
        var jsonText = JSON.stringify({ objeto: obj });
        CallServer(formname + "/SaveObject", jsonText, 4);
    }
}

function SaveObjResult(data) {
    if (data != "") {
        if (data.d == "OK") {
            LoadDetalle();
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al guardar el registro...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}

