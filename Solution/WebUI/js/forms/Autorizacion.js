//Archivo:          CalculoPrecio.js
//Descripción:      Contiene las funciones comunes para la interfaz de calculo precios
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Diciembre 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfAutorizacion.aspx";
var menuoption = "Autorizacion";

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
                MarkResult(data);
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
    setComproba();
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
    obj["aut_usu_autoriza"] =$("#txtAUT_POR_S").val();
    obj["aut_fecha"] = $("#txtFECHA_S").val();
    obj["aut_usu_modifica"] = $("#txtMOD_POR_S").val();
    obj["aut_usu_fecha"] = $("#txtFECHA_P_MOD_S").val();
    obj["aut_usuario"] = $("#txtAUTORIZA_S").val();
    obj["aut_tipo"] = parseInt($("#cmbTIPO_S").val());
   obj["aut_cco_comproba"]=$("#txtcodigocomp").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDetalleData", jsonText, 2);
}

function LoadDetalleDataResult(data) {
    if (data != "") {
        $('#tddatos').append(data.d);
    }
   
}

function setComproba() {
    var codigocomp = $("#txtcodigocomp").val();
    if (codigocomp >= 0) {
        var id = {};
        id["aut_cco_comproba"] = codigocomp;
        id["aut_empresa"] = parseInt(empresasigned["emp_codigo"]);
        CallDlistaPrecio(id);
    }
}

function SetForm() {
    $("#txtAUT_POR_S").on("change", LoadDetalle);
    $("#txtFECHA_S").on("change", LoadDetalle);
    $("#txtMOD_POR_S").on("change", LoadDetalle);
    $("#txtFECHA_P_MOD_S").on("change", LoadDetalle);
    $("#txtAUTORIZA_S").on("change", LoadDetalle);
    $("#cmbTIPO_S").on("change", LoadDetalle);
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


function Edit(obj) {
   var id = $(obj).data("id");
   CallDlistaPrecio(id); 
}

function CallDlistaPrecio(id) {
    var obj = {};
    obj["aut_usuario"] = usuariosigned["usr_id"];
    if (id != null) {
        obj["aut_empresa"] = id.aut_empresa;
        obj["aut_secuencia"] = id.aut_secuencia;
        obj["aut_cco_comproba"] = id.aut_cco_comproba;        
    }
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDlistaPrecio", jsonText, 3);
}

function CallDlistaPrecioResult(data) {
    if (data != "") {
        CallPopUp2("modDListaPrecio", "Detalle ListaPrecio", data.d);
        SetFormCalculo();
    }
    SetAutocompleteById("txtCOMPROBA_FIN_P");
    SetAutocompleteById("txtCOMPROBA_P");
    $(".fecha").datepicker({
        
        dateFormat: "dd/mm/yy"
    });


    /*$(".fecha").datepicker();
 //   $(".fecha").datepicker('setDate', new Date());
    $(".fecha").datepicker("option", "dateFormat", "dd/mm/yy");*/


}

function SetFormCalculo() {
}


function GetDlistaPrecioObj() {
    var obj = {};
    obj["aut_cco_comproba"] = $("#cmbCOMPROBANTE").val();
    obj["aut_cco_comproba_key"] = $("#cmbCOMPROBANTE").val();
    obj["aut_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["aut_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    obj["aut_secuencia"] =  $("#txtCODIGO_P").val();
    obj["aut_secuencia_key"] = $("#txtCODIGO_P").val();
    obj["aut_tipo"] = parseInt($("#cmbTIPO_P").val());
    obj["aut_estado"] = $("#txtAUTORIZA_P").val();
    obj["aut_usuario"] = $("#txtAUTORIZA_P").val();
    obj["aut_fecha"] = $("#txtFECHA_P").val();
    obj["aut_usu_autoriza"] = usuariosigned["usr_id"];
    obj["aut_usu_modifica"] = $("#txtMOD_POR_P").val();
    if ($("#txtFECHA_P_MOD_P").val()!="")
        obj["aut_usu_fecha"] = $("#txtFECHA_P_MOD_P").val();
    obj = SetAuditoria(obj);
    return obj;
}

function CallDListaPrecioOK() {    
    SaveObj();
    return true;
}

function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCOMPROBA_FIN_P") {
        return {
            label: item.pro_id + "," + item.pro_nombre,
            value: item.pro_nombre,
            info: item
        }
    }

    if (idobj == "txtCOMPROBA_P") {
        return {
            value: item.com_doctran,
            info: item

        }
    }

}

function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCOMPROBA_FIN_P") {
        $("#txtCOMPROBA_FIN_P").val(item.info.pro_nombre);
        $("#cmbCOMPROBANTEFIN").val(item.info.pro_codigo);
     //   $("#txtPRODUCTO").val(item.info.pro_nombre);
     //   LoadProduct();
        //alert(ui.item.label);
    }
    if (idobj == "txtCOMPROBA_P") {
        $("#txtCOMPROBA_P").val(item.info.com_doctran);
        $("#cmbCOMPROBANTE").val(item.info.com_codigo);
        //   $("#txtPRODUCTO").val(item.info.pro_nombre);
        //   LoadProduct();
        //alert(ui.item.label);
    }
}

////////////////////////////////
//FUNCIONES de Selección y Deselección
function Mark(obj) {
    // var tr = $(obj).closest('table').attr('id'); ;
    var id = $(obj).data("id");
    var compr = {};  


    if (id["aut_cco_comproba"] != null) {
        compr["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        compr["com_codigo"] = parseInt(id["aut_cco_comproba"]);
    }
    var jsonText = JSON.stringify({ objeto: compr });
    CallServer("ws/Metodos.asmx/GetFormulario", jsonText,6);
    //GetTotales();
}
function MarkResult(data) {
    if (data != "") {
        window.location = data.d;
    };
    /*$(".fecha").datepicker();
    //   $(".fecha").datepicker('setDate', new Date());
    $(".fecha").datepicker("option", "dateFormat", "dd/mm/yy");*/
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
            obj["aut_empresa"] = id.aut_empresa;
            obj["aut_secuencia"] = id.aut_secuencia;
            obj["aut_cco_comproba"] = id.aut_cco_comproba;   
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

