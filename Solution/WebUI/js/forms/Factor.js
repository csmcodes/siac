﻿//Archivo:          AsignarCaracteristicas.js
//Descripción:      Contiene las funciones propias de la interfaz
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Julio 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados


var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfFactor.aspx";
var menuoption = "Factor";

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
    CallServer(formname + "/GetCabecera", "{}", 0);
}
function LoadCabeceraResult(data) {
    if (data != "") {
        $('#cabecera').append(data.d);
        $("#cmbPRODUCTO_S").on("change", LoadDetalle);
      
        LoadDetalle();
    }
    $('.fecha').datepicker({ dateFormat: 'dd/mm/yy' }).val();

}
function LoadDetalle() {
    var obj = {};
    obj["fac_producto"] = $("#cmbPRODUCTO_S").val();
    obj["fac_empresa"] =  parseInt(empresasigned["emp_codigo"]); 
    var jsonText = JSON.stringify({ id: obj });
    CallServer(formname + "/GetDetalle", jsonText, 1);
}
function LoadDetalleResult(data) {
    if (data != "") {
        $('#detallebody').html(data.d);

    }
}
function LoadForm() {
    CallServer(formname + "/GetForm", "{}", 2);
}
function LoadFormResult(data) {
    if (data != "") {
        $('#formmodal').html(data.d);
    }
    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha
   
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
    
    $("#cmbPRODUCTO").attr("disabled", disable);
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
    obj["fac_unidad"] = $("#cmbUMEDIDA").val();
    obj["fac_unidad_key"] = $("#txtUMEDIDA_key").val();  
    obj["fac_empresa"] =parseInt(empresasigned["emp_codigo"]);
    obj["fac_empresa_key"] =parseInt(empresasigned["emp_codigo"]);
    obj["fac_producto"] = $("#cmbPRODUCTO").val();
    obj["fac_producto_key"] = $("#cmbPRODUCTO").val();
    obj["fac_factor"] = $("#txtFACTOR").val();
    obj["fac_default"] = GetCheckValue($("#chkDEFAULT"));
    obj["fac_estado"] = GetCheckValue($("#chkESTADO"));

    obj["fac_nombreunidad"] = $("#cmbUMEDIDA option:selected").text();
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#cmbUMEDIDA").val(obj["fac_unidad"]);
    $("#txtUMEDIDA_key").val(obj["fac_unidad_key"]);   
       
    $("#cmbPRODUCTO").val(obj["fac_producto"]); 
    $("#txtFACTOR").val(obj["fac_factor"]);
    $("#chkDEFAULT").prop("checked", SetCheckValue(obj["fac_default"]));
    $("#chkESTADO").prop("checked", SetCheckValue(obj["fac_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Modificado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}


function CleanForm() {
    var dt = new Date();
    var obj = {};
    obj["fac_empresa"] =parseInt(empresasigned["emp_codigo"]); 
    obj["fac_producto"] = $("#cmbPRODUCTO_S").val();
  
    $("#cmbPRODUCTO").val(obj["fac_producto"]);
    $("#cmbUMEDIDA").val("");
    $("#txtUMEDIDA_key").val("");
    $("#txtFACTOR").val(""); 
    $("#chkDEFAULT").prop("checked", false);    
    $("#chkESTADO").prop("checked", true);
   
   
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