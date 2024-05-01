var formname = "wfPerfil.aspx";
var menuoption = "Perfil";

function SetForm() {
    CleanForm();
}
function GetJSONObject() {
    var obj = {};
    obj["per_id"] = $("#txtID").val();
    obj["per_id_key"] = $("#txtID_key").val();
    obj["per_descripcion"] = $("#txtDESCRIPCION").val();
 //   obj["NOMBRES"] = $("#txtNOMBRES").val();
//    obj["PERFIL"] = $("#cmbPERFIL").val();
    obj["per_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#txtID").val(obj["per_id"]);
    $("#txtID_key").val(obj["per_id_key"]);
    $("#txtDESCRIPCION").val(obj["per_descripcion"]);
 //   $("#txtNOMBRES").val(obj["NOMBRES"]);
 //   $("#cmbPERFIL").val(obj["PERFIL"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["per_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["per_id"] = $("#txtID_S").val();
  //  obj["NOMBRES"] = $("#txtNOMBRES_S").val();
    obj["per_descripcion"] = $("#txtDESCRIPCION_S").val();
    if ($("#cmbESTADO_S").val() != "")
        obj["per_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
    $("#txtID_S").val("");
  //  $("#txtNOMBRES_S").val("");
    $("#txtDESCRIPCION_S").val("");
    $("#cmbESTADO_S").val("");
    ReloadData();
}

function CleanForm() {
    $("#txtID").val("");
    $("#txtID_key").val("");
//    $("#txtNOMBRES").val("");
    $("#txtDESCRIPCION").val("");
 //   $("#cmbDESCRIPCION").val("");
    $("#chkESTADO").prop("checked", true);
}