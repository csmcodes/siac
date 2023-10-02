﻿var formname = "wfArmaMenu.aspx";
var menuoption = "wfArmaMenu";

$(document).ready(function () {
    $("#addopt").on("click", AddOption);
    $("#addchildopt").on("click", AddChildOption);

  });

function SetForm() {
    CleanForm();
}

function GetJSONObject() {
    var obj = {};
    obj["men_id"] = $("#txtCODIGO").val();
    obj["men_id_key"] = $("#txtCODIGO_key").val();
    obj["men_nombre"] = $("#txtNOMBRE").val();
    obj["men_formulario"] = $("#txtFORMULARIO").val();
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#txtCODIGO").val(obj["men_id"]);
    $("#txtCODIGO_key").val(obj["men_id_key"]);
    $("#txtNOMBRE").val(obj["men_nombre"]);
    $("#txtFORMULARIO").val(obj["men_formulario"]);
    $("#txtIMAGEN").val(obj["men_imagen"]);
    $("#txtPADRE").val(obj["men_padre"]);
    $("#txtORDEN").val(obj["men_orden"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["men_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Modificado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["FORMULARIO"] = $("#txtFORMULARIO_S").val();  
    obj["NOMBRE"] = $("#txtNOMBRE_S").val();
    if ($("#cmbESTADO_S").val() != "")
        obj["ESTADO"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
    $("#txtFORMULARIO_S").val(""); 
    $("#txtNOMBRE_S").val("");
    $("#cmbESTADO_S").val("");
    ReloadData();
}

function CleanForm() {
    $("#txtCODIGO").val("");
    $("#txtCODIGO_key").val("");
    $("#txtNOMBRE").val("");
    $("#txtFORMULARIO").val("");
    $("#txtIMAGEN").val("");
    $("#txtPADRE").val("");
    $("#txtORDEN").val("");
    $("#chkESTADO").prop("checked", true);
}


//Funciona que invoca al servidor mediante JSON
function CallServer1(strurl, strdata, retorno) {
    ClearValidate();
    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (retorno == 0)
                AddOptionResult(data);
            if (retorno == 1)
                SaveOptionResult(data);           

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


function AddOption() {

    CallServer1(formname + "/AddOption", "{}", 0);

}
function AddOptionResult(data) {
    if (data != "") {
        ReloadData();
        //var obj = $.parseJSON(data.d);
        //SetJSONObject(obj); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM
    }
}

function AddChildOption() {
    if (selectobj != null) {
        var id = $(selectobj).data("id");
        var jsonText = JSON.stringify({ id: id });
        CallServer1(formname + "/AddChildOption", jsonText, 0)
    }

}


function SaveOption() {
    if (ValidateForm()) {
        var jsonText = GetJSONObject(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
        CallServer1(formname + "/SaveObject", jsonText, 1);
    }

}

function SaveOptionResult(data) {
    if (data != "") {
        if (data.d == "OK"){
            jQuery.jGrowl("Registro actualizado con éxito");
            ReloadData();
        }        
    }
}