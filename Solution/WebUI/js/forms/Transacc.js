var formname = "wfTransacc.aspx";
var menuoption = "Transacc";
function SetForm() {
    CleanForm();
    
}
function GetJSONObject() {
    var obj = {};
    obj["tra_modulo"] = $("#cmbMODULO").val();
    obj["tra_secuencia"] = $("#txtSECUENCIA").val();
    obj["tra_secuencia_key"] = $("#txtSECUENCIA_key").val();    
    obj["tra_modulo_key"] = $("#txtMODULO_key").val();
    obj["tra_id"] = $("#txtID").val();
    obj["tra_nombre"] = $("#txtNOMBRE").val();
    obj["tra_estado"] = GetCheckValue($("#chkESTADO"));
    obj["tra_nombremodulo"] = $("#cmbMODULO option:selected").text();
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#cmbMODULO").val(obj["tra_modulo"]);
    $("#txtSECUENCIA").val( obj["tra_secuencia"]);
    $("#txtSECUENCIA_key").val(obj["tra_secuencia_key"]);
    $("#txtMODULO_key").val(obj["tra_modulo_key"]);
    $("#txtID").val(obj["tra_id"]);
    $("#txtNOMBRE").val(obj["tra_nombre"]);   
    $("#chkESTADO").prop("checked", SetCheckValue(obj["tra_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["tra_secuencia"] = $("#txtSECUENCIA_S").val();
    obj["tra_nombre"] = $("#txtNOMBRE_S").val();
    obj["tra_id"] = $("#txtID_S").val();
    obj["tra_modulo"] = $("#cmbMODULO_S").val();
    if ($("#cmbESTADO_S").val() != "")
        obj["tra_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
    
    $("#cmbESTADO_S").val("");
    $("#txtSECUENCIA_S").val("");
    $("#txtNOMBRE_S").val("");
    $("#txtID_S").val("");
    $("#cmbMODULO_S").val("");
    ReloadData();
}

function CleanForm() {
    $("#cmbMODULO").val("");
    $("#txtSECUENCIA").val("");
    $("#txtSECUENCIA_key").val("");
    $("#txtMODULO_key").val("");
    $("#txtID").val("");
    $("#txtNOMBRE").val("");
    $("#chkESTADO").prop("checked", true);
}