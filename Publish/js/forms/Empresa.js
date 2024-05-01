var formname = "wfEmpresa.aspx";
var menuoption = "Empresa";
function SetForm() {
    CleanForm();
}
function GetJSONObject() {
    var obj = {};
    obj["emp_codigo"] = $("#txtCODIGO").val();
    obj["emp_codigo_key"] = $("#txtCODIGO_key").val();
    obj["emp_imp_venta"] = $("#cmbIMP_VENTA").val();
    obj["emp_imp_compra"] = $("#cmbIMP_COMPRA").val();
    obj["emp_informante"] = $("#cmbINFORMANTE").val();
    obj["emp_id"] = $("#txtID").val();
    obj["emp_nombre"] = $("#txtNOMBRE").val();
    obj["emp_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#txtCODIGO").val(obj["emp_codigo"]);
    $("#txtCODIGO_key").val(obj["emp_codigo_key"]);
    $("#txtID").val(obj["emp_id"]);
    $("#txtNOMBRE").val(obj["emp_nombre"]);
    $("#cmbIMP_VENTA").val(obj["emp_imp_venta"]);
    $("#cmbIMP_COMPRA").val(obj["emp_imp_compra"]);
    $("#cmbINFORMANTE").val(obj["emp_informante"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["emp_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Modificado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["emp_nombre"] = $("#txtNOMBRE_S").val();
    obj["emp_id"] = $("#txtID_S").val();
    if ($("#cmbESTADO_S").val() != "")
        obj["emp_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {   
    $("#txtNOMBRE_S").val("");
    $("#txtID_S").val("");
    $("#cmbESTADO_S").val("");
    ReloadData();
}

function CleanForm() {
    $("#txtCODIGO").val("");
    $("#txtCODIGO_key").val("");
    $("#txtID").val("");
    $("#txtNOMBRE").val("");
    $("#cmbIMP_VENTA").val("");
    $("#cmbIMP_COMPRA").val("");
    $("#cmbINFORMANTE").val("");
    $("#chkESTADO").prop("checked", true);
}