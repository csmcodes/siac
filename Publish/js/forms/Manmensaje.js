var formname = "wfManmensaje.aspx";
var menuoption = "Manmensaje";
function SetForm() {
    CleanForm();
    LoadDestino();

  
    
}
function GetJSONObject() {
    var obj = {};
    obj["msj_empresa"] =parseInt(empresasigned["emp_codigo"]);
    obj["msj_codigo"] = $("#txtCODIGO").val();
    obj["msj_codigo_key"] = $("#txtCODIGO_key").val();    
    obj["msj_empresa_key"] =parseInt(empresasigned["emp_codigo"]);
    obj["msj_usuarioenvia"] = $("#txtUSUARIOENVIA").val();
    obj["msj_mensaje"] = $("#txtMENSAJE").val();
    obj["msj_estadoenvio"] = $("#txtESTADOENVIA").val();    
    obj["msj_id"] = $("#txtID").val();
    obj["msj_asunto"] = $("#txtASUNTO").val();
    obj["msj_fechacreacion"] = $("#cmbFECHACREACION").val();
    obj["msj_fechaenvio"] = $("#cmbFECHAENNVIO").val();
    obj["msj_estado"] = GetCheckValue($("#chkESTADO"));
    obj["destino"] = $("#cmbDESTINO").val();
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
 
    $("#txtCODIGO").val(obj["msj_codigo"]);
    $("#txtCODIGO_key").val(obj["msj_codigo_key"]);
   
    $("#txtUSUARIOENVIA").val(obj["msj_usuarioenvia"]);
    $("#txtMENSAJE").val(obj["msj_mensaje"]);
    $("#txtESTADOENVIA").val(obj["msj_estadoenvio"]);
    $("#txtID").val(obj["msj_id"]);
    $("#txtASUNTO").val(obj["msj_asunto"]);
    $("#cmbFECHACREACION").val(GetDateValue(obj["msj_fechacreacion"]));
    $("#cmbFECHAENNVIO").val(GetDateValue(obj["msj_fechaenvio"]));
    $("#chkESTADO").prop("checked", SetCheckValue(obj["msj_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));

    var objtipos = new Array();
    for (i = 0; i < obj["destino"].length; i++)
    { objtipos[i] = obj["destino"][i]["msd_usuario"].toString(); }

    $("#cmbDESTINO").val(objtipos);
    $('#cmbDESTINO').trigger('liszt:updated');

}

function GetJSONSearch() {
    var obj = {};
    obj["msj_asunto"] = $("#txtASUNTO_S").val();
    obj["msj_usuarioenvia"] = $("#txtUSUARIOENVIA_S").val();  
    if ($("#cmbESTADO_S").val() != "")
        obj["msj_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {

    $("#txtASUNTO_S").val("");
    $("#txtUSUARIOENVIA_S").val("");
   
    ReloadData();
}

function CleanForm() {
    
    $("#txtCODIGO").val("");
    $("#txtCODIGO_key").val("");
    
    $("#txtUSUARIOENVIA").val("");
    $("#txtMENSAJE").val("");
    $("#txtESTADOENVIA").val("");
    $("#txtID").val("");
    $("#txtASUNTO").val("");
    $("#cmbFECHACREACION").val("");
    $("#cmbFECHAENNVIO").val("");
    
    $("#cmbDESTINO").val("");
    $('#cmbDESTINO').trigger('liszt:updated'); 
    $("#chkESTADO").prop("checked", true);
}

function CallServer1(strurl, strdata, retorno) {
    ClearValidate();
    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

           
            if (retorno == 6)
                LoadDestinoResult(data);

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

function LoadDestino() {
    var obj = {};
    obj["empresa"] =parseInt(empresasigned["emp_codigo"]);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer1(formname + "/GetDestino", jsonText, 6);
}

function LoadDestinoResult(data) {
    if (data != "") {
        $("#cmbDESTINO").removeAttr("style", "").removeClass("chzn-done").data("chosen", null).next().remove();
        $("#cmbDESTINO").replaceWith(data.d);
        if ($("#cmbDESTINO option").length == 0)
            $("#cmbDESTINO").attr("disabled", true);
        else
            $("#cmbDESTINO").attr("disabled", false);
        $(".chzn-select").chosen();
    }

}