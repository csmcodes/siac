//Archivo:          AsignarCaracteristicas.js
//Descripción:      Contiene las funciones propias de la interfaz
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Julio 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados


var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfCuentaTransacc.aspx";
var menuoption = "CuentaTransacc";

//Codigo ejecutado cuando el document esta listo
$(document).ready(function () {
    LoadCabecera();
    //LoadDetalle();
    LoadForm();     //Carga el formulario
    $("#saveedit").on("click", SaveObj);    //Opción "Guardar" del combo de opciones de la sección de edición
    $("#addnew").on("click", AddObj);    //Opción "Guardar" del combo de opciones de la sección de edición
    $("#remove").on("click", DeleteObjs);    //Opción "Eliminar" del combo de opciones de la sección de edición
    $('body').css('background', 'transparent'); //Limpia el fondo de la pantalla
});

//Funciona que invoca al servidor mediante JSON
function CallServer(strurl, strdata, retorno) {
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
                LoadFormResult(data);
            if (retorno == 3)
                ShowObjResult(data);
            if (retorno == 4)
                SaveObjResult(data);
            if (retorno == 5)
                DeleteObjsResult(data);
            if (retorno == 6)
                LoadTransaccResult(data);
            if (retorno == 7)
                LoadPeriodoResult(data);
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


function LoadTransacc() {
    var obj = {};
    obj["ctr_modulo"] = $("#cmbMODULO_C").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetTransacc", jsonText, 6);
}

function LoadTransaccResult(data) {
    if (data != "") {
        $("#cmbTRANSACC").replaceWith(data.d);
        if ($("#cmbTRANSACC option").length == 0)
            $("#cmbTRANSACC").attr("disabled", true);
        else {
            $("#cmbTRANSACC").attr("disabled", false);
            $("#cmbTRANSACC").val($("#txtTRANSACC_key").val());
        }
    }
}

function LoadCabecera() {
    CallServer(formname + "/GetCabecera", "{}", 0);
}
function LoadCabeceraResult(data) {
    if (data != "") {
        $('#cabecera').append(data.d);
        $("#cmbMODULO_C").on("change", LoadDetalle);
    }
    $('.fecha').datepicker({ dateFormat: 'dd/mm/yy' }).val();
    LoadDetalle();
}
function LoadDetalle() {
    var obj = {};
    obj["ctr_modulo"] = $("#cmbMODULO_C").val();
    obj["ctr_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["ctr_tipo"] = parseInt($("#txttclipro").val()); 
    var jsonText = JSON.stringify({ id: obj });
    CallServer(formname + "/GetDetalle", jsonText, 1);
}

function LoadDetalleResult(data) {
    if (data != "") {
        $('#detallebody').html(data.d);
    }
    LoadTransacc();
}


function LoadForm() {
    $("#cmbMODULO").val($("#cmbMODULO_C").val);
    CallServer(formname + "/GetForm", "{}", 2);
}

function LoadFormResult(data) {
    if (data != "") {
        $('#formmodal').html(data.d);
    }
    $(".fecha").datepicker({
        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha
    SetAutocompleteById("txtCUENTA");
}

function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCUENTA") {
        return {
            label: item.cue_id + "," + item.cue_nombre,
            value: item.cue_nombre,
            info: item
        }
    }

}
function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCUENTA") {
        $("#txtCODCCUENTA").val(item.info.cue_codigo);
    }

}

function StopPropagation(event) {
    if (!event) var event = window.event;
    event.cancelBubble = true;
    if (event.stopPropagation) event.stopPropagation();
}
/****************************FUNCIONES BASICAS*******************************/
function GetCheckValue(obj) {
    if ($(obj).length > 0) {
        if ($(obj)[0].checked)
            return 1;
        else
            return 0;
    }
    //return null;
}

function SetCheckValue(value) {
    if (value != null) {
        if (value.toString() == "1")
            return true;
    }
    return false;
    //return null;
}

function GetDateValue(value) {
    if (value != null) {
        var date = new Date(parseInt(value.substr(6)));
        return date.toLocaleString();
        //return eval(value.slice(1, -1));
    }
    return "";
}

function Edit(obj) {
    //var id = $(obj).data("id");
    selectobj = obj;
    ShowObj(obj);
}

function Select(obj) {
    //alert($(obj).css("background-color"));
    if ($(obj).css("background-color") == rgbcolor)
        $(obj).css("background-color", "");
    else
        $(obj).css("background-color", color);
}

function CleanSelect() {
    var rows = $("#detallebody tr");
    $(rows).css("background-color", "");
}

function ShowObj(obj) {
    var id = $(obj).data("id");
    var jsonText = JSON.stringify({ id: id });
    CallServer(formname + "/GetObject", jsonText, 3)
}

function DisableControls(disable) {
    $("#cmbMODULO").attr("disabled", disable);   
    $("#cmbCTIPOCOM").attr("disabled", disable);
}

function LoadPeriodo() {
    CallServer(formname + "/DisablePeriodo", "{}", 7);
}

function LoadPeriodoResult(disable) {
    $("#txtPERIODO").attr("disabled", disable.d);
}

function ShowObjResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        CleanForm();
        SetJSONObject(obj); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM
        DisableControls(true);
        $("#edit").trigger('click'); //MUESTRA EL DIV CON EL FORM
        CleanSelect();      
    }
}

function GetJSONObject() {
    var obj = {};
    obj["ctr_transacc"] = $("#cmbTRANSACC").val();
    obj["ctr_transacc_key"] = $("#txtTRANSACC_key").val();
    obj["ctr_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["ctr_empresa_key"] = parseInt($("#txtEMPRESA_key").val());
    obj["ctr_categoria"] = $("#cmbCATEGORIA").val();
    obj["ctr_categoria_key"] = $("#txtCATEGORIA_key").val();
    obj["ctr_tipo"] = parseInt($("#txttclipro").val());
    obj["ctr_cuenta"] = parseInt($("#txtCODCCUENTA").val());
    obj["ctr_modulo"] = parseInt($("#cmbMODULO").val());
    obj["ctr_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#cmbTRANSACC").val(obj["ctr_transacc"]);
    $("#txtTRANSACC_key").val(obj["ctr_transacc_key"]);
    $("#txtEMPRESA_key").val(obj["ctr_empresa_key"]);
    $("#cmbCATEGORIA").val(obj["ctr_categoria"]);
    $("#txtCATEGORIA_key").val(obj["ctr_categoria_key"]);
    $("#txtCODCCUENTA").val(obj["ctr_cuenta"]);
    $("#cmbMODULO").val(obj["ctr_modulo"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["ctr_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Modificado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
    $("#txtCUENTA").val(obj["ctr_cuenombre"]);
}


function CleanForm() {
    var dt = new Date();
    var obj = {};
    obj["ctr_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["ctr_ctipocom"] = $("#cmbMODULO_C").val();    
    $("#cmbTRANSACC").val("");
    $("#txtTRANSACC_key").val("");
    $("#txtEMPRESA_key").val("");
    $("#cmbCATEGORIA").val("");
    $("#txtCATEGORIA_key").val("");
    $("#txtCODCCUENTA").val("");
    $("#txtCUENTA").val("");
    $("#cmbMODULO").val($("#cmbMODULO_C").val());
    $("#chkESTADO").prop("checked", true);
    //  LoadPeriodo();
    LoadTransacc();
   
}

function ClearValidate() {
    var controles = $("#datoscontent").find('[data-obligatorio="True"]');
    $("#datoscontent").find('[data-obligatorio="True"]').each(function () {
        $(this.parentNode).removeClass('obligatorio')
        $(this.parentNode).children(".obligatorio").remove();
    });
}

function ValidateForm() {
    var retorno = true;
    /* var controles = $("#datoscontent").find('[data-obligatorio="True"]');
    $("#datoscontent").find('[data-obligatorio="True"]').each(function () {
    $(this.parentNode).removeClass('obligatorio')
    $(this.parentNode).children(".obligatorio").remove();
    if ($(this).val() == "") {
    retorno = false;
    var padre = $(this.parentNode);
    $(this.parentNode).append("<span class='obligatorio'>! Obligatorio</span>");
    $(this.parentNode).addClass('obligatorio')
    }

    });*/
    return retorno;
}
function SaveObj() {
    if (ValidateForm()) {
        var jsonText = GetJSONObject(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
        CallServer(formname + "/SaveObject", jsonText, 4);
    }

}
function SaveObjResult(data) {
    if (data != "") {
        if (data.d != "OK" && data.d.toString() != "ERROR") {
            if (selectobj != null) {
                jQuery.jGrowl("Registro actualizado con éxito");
                $(selectobj).replaceWith(data.d);
            }
            else {
                jQuery.jGrowl("Registro ingresado con éxito");
                $('#detallebody').append(data.d);
            }
            LoadDetalle();
        }
        /*else {
        CleanForm(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM  
        ReloadData();
        }*/
    }
}
function AddObj() {
    ClearValidate();
    DisableControls(true);
    $("#edit").trigger('click');
    selectobj = null;
    CleanForm(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
}
function DeleteObjs() {
    jConfirm('¿Está seguro que desea eliminar los registros seleccionados?', 'Eliminar', function (r) {
        if (r) {
            var objetos = [];
            $("#detallebody").find('tr').each(function () {
                if ($(this).css("background-color") != "transparent") {
                    objetos.push($(this).data("id"));
                }
            });
            var jsonText = JSON.stringify({ objetos: objetos });
            CallServer(formname + "/DeleteObjects", jsonText, 5)
        }
    });
}
function DeleteObjsResult(data) {
    if (data != "") {
        if (data.d == "OK") {
            $("#detallebody").find('tr').each(function () {
                if ($(this).css("background-color") != "transparent") {
                    $(this).remove();
                }
            });
            LoadDetalle();
        }
    }
}