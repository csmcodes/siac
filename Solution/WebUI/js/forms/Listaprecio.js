var formname = "wfListaprecio.aspx";
var menuoption = "Listaprecio";
function SetForm() {
    //   SetPaisProvinciaCanton("cmbPAIS", "cmbPROVINCIA", "cmbCANTON");
    CleanForm();
}
function GetJSONObject() {
    var obj = {};
    obj["lpr_empresa"] =parseInt(empresasigned["emp_codigo"]);
    obj["lpr_codigo"] = $("#txtCODIGO").val();
    obj["lpr_codigo_key"] = $("#txtCODIGO_key").val();    
    obj["lpr_empresa_key"] =parseInt(empresasigned["emp_codigo"]);
    obj["lpr_id"] = $("#txtID").val();
    obj["lpr_nombre"] = $("#txtNOMBRE").val();    
    obj["lpr_moneda"] = $("#cmbMONEDA").val();
    obj["lpr_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    
    $("#txtCODIGO").val( obj["lpr_codigo"]);
    $("#txtCODIGO_key").val(obj["lpr_codigo_key"]);
   
    $("#txtID").val(obj["lpr_id"]);
    $("#txtNOMBRE").val(obj["lpr_nombre"]);    
    $("#cmbMONEDA").val(obj["lpr_moneda"]);   
    $("#chkESTADO").prop("checked", SetCheckValue(obj["lpr_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["lpr_codigo"] = $("#txtCODIGO_S").val();
    obj["lpr_nombre"] = $("#txtNOMBRE_S").val();
    obj["lpr_id"] = $("#txtID_S").val();
    obj["lpr_empresa"] = parseInt(empresasigned["emp_codigo"]);
    if ($("#cmbESTADO_S").val() != "")
        obj["lpr_estado"] = parseInt($("#cmbESTADO_S").val());
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
    $("#cmbMONEDA").val("");    
    $("#chkESTADO").prop("checked", true);
}