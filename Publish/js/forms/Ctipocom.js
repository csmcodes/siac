var formname = "wfCtipocom.aspx";
var menuoption = "Ctipocom";
function SetForm() {
    CleanForm();
}
function GetJSONObject() {
    var obj = {};
    obj["cti_codigo"] = $("#txtCODIGO").val();
    obj["cti_codigo_key"] = $("#txtCODIGO_key").val();
    obj["cti_empresa"] =parseInt(empresasigned["emp_codigo"]);
    obj["cti_empresa_key"] =parseInt(empresasigned["emp_codigo"]);
    obj["cti_id"] = $("#txtID").val();
    obj["cti_nombre"] = $("#txtNOMBRE").val();
    obj["cti_tipo"] = $("#cmbTIPO").val();
    obj["cti_autoriza"] = GetCheckValue($("#chkAUTORIZA"));
    obj["cti_retdato"] = $("#cmbRETDATO").val();
    obj["cti_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#txtCODIGO").val(obj["cti_codigo"]);
    $("#txtCODIGO_key").val(obj["cti_codigo_key"]);
   
    
    $("#txtID").val(obj["cti_id"]);
    $("#txtNOMBRE").val(obj["cti_nombre"]);
    $("#cmbTIPO").val(obj["cti_tipo"]);
    $("#chkAUTORIZA").prop("checked", SetCheckValue(obj["cti_autoriza"]));
    $("#cmbRETDATO").val(obj["cti_retdato"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["cti_estado"]));    
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["cti_codigo"] = $("#txtCODIGO_S").val();
    obj["cti_nombre"] = $("#txtNOMBRE_S").val();
    obj["cti_id"] = $("#txtID_S").val();
    obj["cti_empresa"] = parseInt(empresasigned["emp_codigo"]); 
    if ($("#cmbESTADO_S").val() != "")
        obj["cti_estado"] = parseInt($("#cmbESTADO_S").val());
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
    $("#cmbTIPO").val("");
    $("#cmbRETDATO").val("");
    $("#chkAUTORIZA").prop("checked", false);   
    $("#chkESTADO").prop("checked", true);
}