var formname = "wfUsuario.aspx";
var menuoption = "Usuario";

function SetForm() {
    CleanForm();
    SetAutocomplete();
    $(".hora").timepicker({        
        showMeridian: false
    });
   
}

function GetJSONObject() {
    var obj = {};
    obj["usr_id"] = $("#txtID").val();
    obj["usr_id_key"] = $("#txtID_key").val();
    obj["usr_password"] = $("#txtPASSWORD").val();
    obj["usr_nombres"] = $("#txtNOMBRES").val();
    obj["usr_perfil"] = $("#cmbPERFIL").val();
    obj["usr_mail"] = $("#txtMAIL").val();
    obj["usr_descripcionperfil"] = $("#cmbPERFIL option:selected").text();
    obj["usr_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#txtID").val(obj["usr_id"]);
    $("#txtID_key").val(obj["usr_id_key"]);
    $("#txtPASSWORD").val(obj["usr_password"]);
    $("#txtNOMBRES").val(obj["usr_nombres"]);
    $("#txtMAIL").val(obj["usr_mail"]);
    $("#cmbPERFIL").val(obj["usr_perfil"]);

    $("#chkESTADO").prop("checked", SetCheckValue(obj["usr_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Modificado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["usr_id"] = $("#txtID_S").val();
    obj["usr_nombres"] = $("#txtNOMBRES_S").val();
    obj["usr_perfil"] = $("#txtPERFIL_S").val();
    
    if ($("#cmbESTADO_S").val() != "")
        obj["usr_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
    $("#txtID_S").val("");
    $("#txtNOMBRES_S").val("");
    $("#txtPERFIL_S").val("");
    $("#cmbESTADO_S").val("");
    ReloadData();
}

function CleanForm() {
    $("#txtID").val("");
    $("#txtID_key").val("");
    $("#txtMAIL").val("");
    $("#txtNOMBRES").val("");
    $("#txtPASSWORD").val("");
    $("#cmbPERFIL").val("");
    $("#chkESTADO").prop("checked", true);
}

function PrintObj() {

window.location.href = "wfAdmUsuario.aspx?codigo=" + $("#txtID").val();

}
