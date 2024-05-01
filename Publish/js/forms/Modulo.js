var formname = "wfModulo.aspx";
var menuoption = "Modulo";
function SetForm() {
    CleanForm();
}
function GetJSONObject() {
    var obj = {};
   
    obj["mod_codigo"] = $("#txtCODIGO").val();
    obj["mod_codigo_key"] = $("#txtCODIGO_key").val();   
    obj["mod_id"] = $("#txtID").val();
    obj["mod_nombre"] = $("#txtNOMBRE").val();
    obj["mod_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
   
    $("#txtCODIGO").val( obj["mod_codigo"]);
    $("#txtCODIGO_key").val(obj["mod_codigo_key"]);
    $("#txtID").val(obj["mod_id"]);
    $("#txtNOMBRE").val(obj["mod_nombre"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["mod_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["mod_codigo"] = $("#txtCODIGO_S").val();
    obj["mod_nombre"] = $("#txtNOMBRE_S").val();
    obj["mod_id"] = $("#txtID_S").val();
    if ($("#cmbESTADO_S").val() != "")
        obj["mod_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
    
    $("#cmbESTADO_S").val("");
    $("#txtCODIGO_S").val("");
    $("#txtNOMBRE_S").val("");
    $("#txtID_S").val("");
   
    ReloadData();
}

function CleanForm() {
   
    $("#txtCODIGO").val("");
    $("#txtCODIGO_key").val("");
    $("#txtID").val("");
    $("#txtNOMBRE").val("");
    $("#chkESTADO").prop("checked", true);
}