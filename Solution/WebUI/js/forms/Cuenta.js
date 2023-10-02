var formname = "wfCuenta.aspx";
var menuoption = "wfCuenta";

$(document).ready(function () {
    $("#addopt").on("click", AddOption);
    $("#addchildopt").on("click", AddChildOption);

  });

function SetForm() {
    CleanForm();
}

function GetJSONObject() {
    var obj = {};
    obj["cue_codigo"] = $("#txtCODIGO").val();
    obj["cue_codigo_key"] = $("#txtCODIGO_key").val();
    obj["cue_empresa"] =parseInt(empresasigned["emp_codigo"]);
    obj["cue_empresa_key"] =parseInt(empresasigned["emp_codigo"]);
    obj["cue_id"] = $("#txtID").val();
    obj["cue_nombre"] = $("#txtNOMBRE").val();
    obj["cue_modulo"] = $("#cmbMODULO").val();
    obj["cue_genero"] = $("#txtGENERO").val();
    obj["cue_movimiento"] = GetCheckValue($("#chkMOVIMIENTO"));
    obj["cue_reporta"] = $("#txtREPORTA").val();
    obj["cue_orden"] = $("#txtORDEN").val();
    obj["cue_visualiza"] = GetCheckValue($("#chkVISUALIZA"));
    obj["cue_negrita"] =  GetCheckValue($("#chkNEGRITA"));
    obj["cue_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#txtCODIGO").val(obj["cue_codigo"]);
    $("#txtCODIGO_key").val(obj["cue_codigo_key"]);
 
   
    $("#txtID").val( obj["cue_id"]);
    $("#txtNOMBRE").val(obj["cue_nombre"]);
    $("#cmbMODULO").val(obj["cue_modulo"]);
    $("#txtGENERO").val( obj["cue_genero"]);
    $("#chkMOVIMIENTO").prop("checked", SetCheckValue(obj["cue_movimiento"]));
    $("#txtREPORTA").val(obj["cue_reporta"]);
    $("#txtORDEN").val(obj["cue_orden"]);
    $("#chkVISUALIZA").prop("checked", SetCheckValue(obj["cue_visualiza"]));
    $("#chkNEGRITA").prop("checked", SetCheckValue(obj["cue_negrita"]));
    $("#chkESTADO").prop("checked", SetCheckValue(obj["cue_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Modificado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
    LoadHijos(obj);
}
function LoadHijos(obj) {
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer1(formname + "/LoadHijos", jsonText, 2);
  
   
}
function LoadHijosResult(disable) {
    $("#chkMOVIMIENTO").attr("disabled", disable.d);
}

function GetJSONSearch() {
    var obj = {};
 
    obj["NOMBRE"] = $("#txtNOMBRE_S").val();
    if ($("#cmbESTADO_S").val() != "")
        obj["ESTADO"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
  
    $("#txtNOMBRE_S").val("");
    $("#cmbESTADO_S").val("");
    ReloadData();
}

function CleanForm() {
    $("#txtCODIGO").val("");
    $("#txtCODIGO_key").val("");
    
    
    $("#txtID").val("");
    $("#txtNOMBRE").val("");
    $("#cmbMODULO").val("");
    $("#txtGENERO").val("");
    $("#chkMOVIMIENTO").prop("checked", true);
    $("#txtREPORTA").val("");
    $("#txtORDEN").val("");
    $("#chkVISUALIZA").prop("checked", false);
    $("#chkNEGRITA").prop("checked", false);
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
            if (retorno == 2)
                LoadHijosResult(data);  

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