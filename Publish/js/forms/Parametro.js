var formname = "wfParametro.aspx";
var menuoption = "Parametro";
function SetForm() {
    CleanForm();
}
function GetJSONObject() {
    var obj = {};
    obj["par_nombre"] = $("#txtNOMBRE").val();
    obj["par_nombre_key"] = $("#txtNOMBRE_key").val();
    obj["par_descripcion"] = $("#txtDESCRIPCION").val();
    obj["par_tipo"] = $("#txtTIPO").val();
    obj["par_valor"] = $("#txtVALOR").val();
    obj["par_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#txtNOMBRE").val(obj["par_nombre"]);
    $("#txtNOMBRE_key").val(obj["par_nombre_key"]);
    $("#txtDESCRIPCION").val(obj["par_descripcion"]);
    $("#txtTIPO").val(obj["par_tipo"]);
    $("#txtVALOR").val(obj["par_valor"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["par_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["par_nombre"] = $("#txtNOMBRE_S").val();
    obj["par_valor"] = $("#txtVALOR_S").val();
    if ($("#cmbESTADO_S").val() != "")
        obj["par_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
    $("#txtNOMBRE_S").val("");
    $("#txtVALOR_S").val("");
    $("#cmbESTADO_S").val("");
    ReloadData();
}

function CleanForm() {
    $("#txtNOMBRE").val("");
    $("#txtNOMBRE_key").val("");
    $("#txtDESCRIPCION").val("");
    $("#txtTIPO").val("");
    $("#txtVALOR").val(""); 
    $("#chkESTADO").prop("checked", true);
}