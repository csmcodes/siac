var formname = "wfCatcliente.aspx";
var menuoption = "Catcliente";
function SetForm() {
    CleanForm();
}
function GetJSONObject() {
    var obj = {};
    obj["cat_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cat_codigo"] = parseInt($("#txtCODIGO").val());
    obj["cat_codigo_key"] = parseInt($("#txtCODIGO_key").val());
    obj["cat_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    obj["cat_id"] = $("#txtID").val();
    obj["cat_nombre"] = $("#txtNOMBRE").val();
 //   obj["cat_tipo"] = parseInt($("#cmbTIPO").val());
    obj["cat_tipo"] = parseInt($("#txttclipro").val());
    obj["cat_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {

    $("#txtCODIGO").val(obj["cat_codigo"]);
    $("#txtCODIGO_key").val(obj["cat_codigo_key"]);

    $("#txtID").val(obj["cat_id"]);
    $("#txtNOMBRE").val(obj["cat_nombre"]);
   
    $("#cmbTIPO").val(obj["cat_tipo"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["cat_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["cat_codigo"] = parseInt($("#txtCODIGO_S").val());
    obj["cat_nombre"] = $("#txtNOMBRE_S").val();
    obj["cat_id"] = $("#txtID_S").val();
    obj["cat_tipo"] = parseInt($("#txttclipro").val()); 
    obj["cat_empresa"] = parseInt(empresasigned["emp_codigo"]); 
    if ($("#cmbESTADO_S").val() != "")
        obj["cat_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {

    $("#cmbESTADO_S").val("");
    $("#txtCODIGO_S").val("");
    $("#txtNOMBRE_S").val("");
    $("#txtGERENTE_S").val("");
    ReloadData();
}

function CleanForm() {

    $("#txtCODIGO").val("");
    $("#txtCODIGO_key").val("");
    $("#txtID").val("");
    $("#txtNOMBRE").val("");
    $("#cmbTIPO").val("");    
    $("#chkESTADO").prop("checked", true);
}