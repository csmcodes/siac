var formname = "wfUmedida.aspx";
var menuoption = "Umedida";
function SetForm() {
    CleanForm();
}
function GetJSONObject() {
    var obj = {};
    obj["umd_empresa"] =parseInt(empresasigned["emp_codigo"]);
    obj["umd_codigo"] = $("#txtCODIGO").val();
    obj["umd_codigo_key"] = $("#txtCODIGO_key").val();    
    obj["umd_empresa_key"] =parseInt(empresasigned["emp_codigo"]);
    obj["umd_id"] = $("#txtID").val();
    obj["umd_nombre"] = $("#txtNOMBRE").val();
    obj["umd_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {

    $("#txtCODIGO").val( obj["umd_codigo"]);
    $("#txtCODIGO_key").val(obj["umd_codigo_key"]);
  
    $("#txtID").val(obj["umd_id"]);
    $("#txtNOMBRE").val(obj["umd_nombre"]);   
    $("#chkESTADO").prop("checked", SetCheckValue(obj["umd_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["umd_codigo"] = $("#txtCODIGO_S").val();
    obj["umd_nombre"] = $("#txtNOMBRE_S").val();
    obj["umd_id"] = $("#txtID_S").val();
    obj["umd_empresa"] = parseInt(empresasigned["emp_codigo"]); 
    if ($("#cmbESTADO_S").val() != "")
        obj["umd_estado"] = parseInt($("#cmbESTADO_S").val());
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