//Archivo:          CalculoPrecio.js
//Descripción:      Contiene las funciones comunes para la interfaz de calculo precios
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Diciembre 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfDetalleCuenta.aspx";
var menuoption = "DetalleCuenta";

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


//Funcion que invoca al servidor mediante JSON
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
                CalTotalResult(data);
            if (retorno == 7)
                GetPieResult(data);
            if (retorno == 8)
                LoadFormResult(data);
            if (retorno == 9)
                SaveEditar(data);
            if (retorno == 10)
                LoadFormResult2(data);
            if (retorno == 11)
                LoadFormResultEditar(data);
            if (retorno == 12)
                LoadFormResultEditar(data);
            if (retorno == 13)
                LoadFormResultEditar(data);
            if (retorno == 14)
                LoadFormResultEliminar(data);
            if (retorno == 15)
                LoadFormResultSave(data);
            if (retorno == 16)
                SaveObjCuentaResult(data);
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
    $("#print").on("click", PrintObj);
    var obj = {};
    obj["emp_codigo"] = parseInt(empresasigned["emp_codigo"]);
    var jsonText = JSON.stringify({ objeto: obj });   
    CallServer(formname + "/GetCabecera", jsonText, 0);
}

function LoadCabeceraResult(data) {
    if (data != "") {
        $('#comcabeceracontent').html(data.d);

        if (parseInt($("#txtdebcre").val()) == 1) {
            $($("h1")[0]).text('Balance de Cuentas');
            $($("h5")[0]).text('Balance de Cuentas');
        }
        if (parseInt($("#txtdebcre").val()) == 2) {
            $($("h1")[0]).text('Estado de Resultados');
            $($("h5")[0]).text('Estado de Resultados');
        }
      //  GetPie();
        LoadDetalle();
    }
    $('.fecha').datepicker({ dateFormat: 'dd/mm/yy' }).val();
    SetForm(); //Depende de cada js.
}




function LoadDetalle() {
    $('#comdetallecontent').empty();
    var jsonText = JSON.stringify({});
    CallServer(formname + "/GetDetalle", jsonText, 1);
}

function LoadDetalleResult(data) {
    if (data != "") {
        $('#comdetallecontent').html(data.d);
    //    CalTotal();
        LoadDetalleData();

    }
    SetFormDetalle();
    //EditableRow("detalletabla");
}






function GetCabeceraData() {
    var obj = {};
    obj["cuenta"] = GetCuentaData();

    obj["empresa"] = parseInt(empresasigned["emp_codigo"]);
    return obj;
}

function GetCuentaData() {
    var obj = {};
    obj["cue_id"] = $("#id_S").val();
    obj["cue_nombre"] = $("#cuenta_S").val();
    obj["cue_nivel"] = parseInt($("#nivel_C").val());
    return obj;
}

var isloading = false;
function LoadDetalleData() {
    if (!isloading) {
        var obj = GetCabeceraData();
        var jsonText = JSON.stringify({ objeto: obj });
        isloading = true;
        ShowLoading();
        CallServer(formname + "/GetDetalleData", jsonText, 2);
    }
}

function LoadDetalleDataResult(data) {
    if (data != "") {
        $('#tddatos').append(data.d);
        isloading = false;
        HideLoading();
    }

}


function SetForm() {
    SetAutocompleteById("cuenta_S");
    SetAutocompleteById("id_S");
    $("#id_S").on("change", LoadDetalle);
    $("#cuenta_S").on("change", LoadDetalle);
    $("#nivel_C").on("change", LoadDetalle);
  
}

function SetFormDetalle() {
    //Tratamiento para los elemntos del detalle  
    $("#addnew").on("click", LoadForm);
    $("#comdetallecontent").on("scroll", scroll);
  /*  $("#adddet").on("click", CallDlistaPrecio);
    $("#deldet").on("click", QuitSelected);
    $("#alldet").on("click", { tabla: "tddatos" }, SelectAllRows);
    $("#nonedet").on("click", { tabla: "tddatos" }, CleanSelectedRows);
    $("#comdetallecontent").on("scroll", scroll);*/
    //$("#comdetallecontent").on("scroll", scroll);
}

function scroll() {
    if ($("#comdetallecontent")[0].scrollHeight - $("#comdetallecontent").scrollTop() <= $("#comdetallecontent").outerHeight()) {
        LoadDetalleData();
    }
}


function PrintObj() {
    window.location = "wfBalanceGeneralPrint.aspx?codalmacen=" + parseInt($("#cmbALMACEN_S").val()) + "&fechacort=" + $("#cmbFECHA_C").val() + "&nivel=" + parseInt($("#cmbNIVEL_S").val()) + "&movimiento=" + parseInt($("#chkMOVIMIENTO_S").val()) + "&modulo=" + parseInt($("#cmbMODULO_S").val()) + "&debcre=" + parseInt($("#txtdebcre").val());
}

function Edit(obj) {
    var id = $(obj).data("id");
    //CallDlistaPrecio(id); 
 //   CallServer(formname + "/GetForm", "{}", 8);
}




    /*$(".fecha").datepicker();
    //   $(".fecha").datepicker('setDate', new Date());
    $(".fecha").datepicker("option", "dateFormat", "dd/mm/yy");*/




function SetFormCalculo() {
}




function CallDListaPrecioOK() {
    SaveObj();
    return true;
}





////////////////////////////////
//FUNCIONES de Selección y Deselección
function Select(obj) {
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
            Select(htmltable.rows[r]);
        }
    }
}

function CleanSelectedRows(event) {
    var htmltable = $("#" + event.data.tabla)[0];
    var contador = 0;
    for (var r = 1; r < htmltable.rows.length; r++) {
        if ($(htmltable.rows[r]).css("background-color") == rgbcolor) {
            Select(htmltable.rows[r]);
        }
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

function SetAutoCompleteObj(idobj, item) {
    if (idobj == "cuenta_S") {
        return {
            label: item.cue_nombre,
            value: item.cue_nombre,
            info: item
        }
    }
    if (idobj == "id_S") {
        return {
            label: item.cue_id,
            value: item.cue_id,

            info: item
        }
    }
}
/*function SetAutoCompleteObj(idobj, item) {
    if (idobj == "id_S") {
        return {
            label: item.cue_id,
            value: item.cue_id,
           
            info: item
        }
    }
}*/
function GetAutoCompleteObj(idobj, item) {
    if (idobj == "cuenta_S") {
      //  $("#id_S").val(item.info.cue_codigo);
        //     $("#txtNOMBREBANCO").val(item.info.ban_nombre);
        $("#cuenta_S").val(item.info.cue_nombre);
        //  $("#txtCUENTA").val(item.info.ban_cuenta);
        // ShowCampos();
        LoadDetalle();
    }
    if (idobj == "id_S") {
        $("#id_S").val(item.info.cue_codigo);
        //     $("#txtNOMBREBANCO").val(item.info.ban_nombre);
        LoadDetalle();
    }
}

/*function GetAutoCompleteObj(idobj, item) {
    if (idobj == "id_S") {
        $("#id_S").val(item.info.cue_codigo);
        //     $("#txtNOMBREBANCO").val(item.info.ban_nombre);
        LoadDetalle();
    }
}
*/
function LoadFormResult(data) {
    if (data != "") {
        CallPopUp2("modDetalleCuenta", "Detalle cuenta", data.d);



    }

    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    });
}
function CallDetalleCuentaOK() {
    SaveObjEditarCuenta();
    return true;
}
function GetJSONObject() {
    var obj = {};
    obj["cue_codigo"] = $("#txtCODIGO").val();
    obj["cue_codigo_key"] = $("#txtCODIGO_key").val();
    obj["cue_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cue_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    obj["cue_id"] = $("#txtID").val();
    obj["cue_nombre"] = $("#txtNOMBRE").val();
    obj["cue_modulo"] = $("#cmbMODULO").val();
    obj["cue_genero"] = $("#txtGENERO").val();
    obj["cue_movimiento"] = GetCheckValue($("#chkMOVIMIENTO"));
    obj["cue_reporta"] = $("#txtREPORTA").val();
    obj["cue_orden"] = $("#txtORDEN").val();
    obj["cue_visualiza"] = GetCheckValue($("#chkVISUALIZA"));
    obj["cue_negrita"] = GetCheckValue($("#chkNEGRITA"));
    obj["cue_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SaveObjEditarCuenta() {
   
        var jsonText = GetJSONObject(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
        CallServer(formname + "/SaveObjectEditar", jsonText, 9);
    

}
function SaveEditar(data) {
    if (data != "") {
        if (data.d == "OK") {
            jQuery.jGrowl("Registro actualizado con éxito");
            LoadDetalle();
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al guardar los Detalles...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default

            });
        }
    }
}

function Agregarhijo(id) {
    var id = $(selectobj).data("id");
    var jsonText = JSON.stringify({ id: id });

    CallServer(formname + "/AddChildOption", jsonText, 10);
}

function LoadFormResult2(data) {
    if (data != "") {
        CallPopUp("modDetalleCuenta", "Detalle cuenta", data.d);



    }


}



function Editar(id) {
    var obj = {};
    obj["cue_codigo"] = parseInt(id);
    obj["cue_codigo_key"] = parseInt(id);
    obj["cue_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cue_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetFormEditar", jsonText, 11);
}

function LoadFormResultEditar(data) {
    if (data != "") {
        CallPopUp("modDetalleCuenta", "Detalle cuenta", data.d);



    }


}
function Agregarhijo(id) {
    var obj = {};
    obj["cue_codigo"] = parseInt(id);
    obj["cue_codigo_key"] = parseInt(id);
    obj["cue_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cue_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    var jsonText = JSON.stringify({ objeto: obj });

    CallServer(formname + "/AddChildOption", jsonText, 13);
}

function AddOptionResult(data) {
    if (data != "") {
        CallPopUp2("modDetalleCuenta", "Detalle cuenta", data.d);



    }
}


function Eliminar(id) {
    var obj = {};
    obj["cue_codigo"] = parseInt(id);
    obj["cue_codigo_key"] = parseInt(id);
    obj["cue_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cue_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    var jsonText = JSON.stringify({ objeto: obj });

    CallServer(formname + "/DeleteObject", jsonText, 14);
}


function LoadFormResultEliminar(data) {
    if (data != "") {
        if (data.d == "OK") {
            jQuery.jGrowl("Registro eliminado con éxito");
            LoadDetalle();
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al eliminar el registro..', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default

            });
        }
    }
}

function LoadForm() {
    CallServer(formname + "/GetFormS", "{}", 15);
}

function LoadFormResultSave(data) {
    if (data != "") {
        CallPopUp2("modDetalleCuentaSave", "Detalle cuenta", data.d);



    }
}

function CallCuentaSaveOK() {
    SaveObjCuenta();
    return true;
}

function SaveObjCuenta() {

    var jsonText = GetJSONObject(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
    CallServer(formname + "/SaveObject", jsonText, 16);


}


function SaveObjCuentaResult(data) {
    if (data != "") {
        if (data.d == "OK") {
            jQuery.jGrowl("Registro ingresado con éxito");
            LoadDetalle();
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al guardar los Detalles...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default

            });
        }
    }
}