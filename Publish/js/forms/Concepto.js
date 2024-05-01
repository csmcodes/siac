var formname = "wfConcepto.aspx";
var menuoption = "Concepto";
function SetForm() {
    CleanForm();
    SetPaisProvinciaCanton("cmbPAIS", "cmbPROVINCIA", "cmbCANTON");
}
function GetJSONObject() {
    var obj = {};
    obj["con_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["con_codigo"] = parseInt($("#txtCODIGO").val());
    obj["con_codigo_key"] = parseInt($("#txtCODIGO_key").val());    
    obj["con_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    obj["con_id"] = $("#txtID").val();
    obj["con_nombre"] = $("#txtNOMBRE").val();
    obj["con_tipo"] = $("#txtTIPO").val();

    obj["con_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    
    $("#txtCODIGO").val( obj["con_codigo"]);
    $("#txtCODIGO_key").val(obj["con_codigo_key"]);
   
    $("#txtID").val(obj["con_id"]);
    $("#txtNOMBRE").val(obj["con_nombre"]);
    $("#txtTIPO").val(obj["con_tipo"]);
   
    $("#chkESTADO").prop("checked", SetCheckValue(obj["con_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["con_codigo"] = $("#txtCODIGO_S").val();
    obj["con_nombre"] = $("#txtNOMBRE_S").val();
    obj["con_id"] = $("#txtID_S").val();
    obj["con_empresa"] = parseInt(empresasigned["emp_codigo"]); 
    if ($("#cmbESTADO_S").val() != "")
        obj["con_estado"] = parseInt($("#cmbESTADO_S").val());
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
    $("#txtTIPO").val("");
    
    $("#chkESTADO").prop("checked", true);
}