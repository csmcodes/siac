var formname = "wfRetdato.aspx";
var menuoption = "Retdato";
function SetForm() {
    CleanForm();
    
}
function GetJSONObject() {
    var obj = {};
    obj["rtd_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["rtd_tablacoa"] = parseInt($("#cmbTABLACOA").val());
    obj["rtd_codigo"] = parseInt($("#txtCODIGO").val());
    obj["rtd_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    obj["rtd_tablacoa_key"] = parseInt($("#txtTABLACOA_key").val());
    obj["rtd_codigo_key"] = parseInt($("#txtCODIGO_key").val());
    obj["rtd_id"] = $("#txtID").val();
    obj["rtd_campo"] = $("#txtCAMPO").val();
    obj["rtd_estado"] = GetCheckValue($("#chkESTADO"));
    obj["rtd_nombremodulo"] = $("#cmbMODULO option:selected").text();
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#cmbTABLACOA").val(obj["rtd_tablacoa"]);
    $("#txtCODIGO").val(obj["rtd_codigo"]);
    $("#txtTABLACOA_key").val(obj["rtd_tablacoa_key"]);
    $("#txtCODIGO_key").val(obj["rtd_codigo_key"]);
    $("#txtID").val(obj["rtd_id"]);
    $("#txtCAMPO").val(obj["rtd_campo"]);   
    $("#chkESTADO").prop("checked", SetCheckValue(obj["rtd_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};    
    obj["rtd_campo"] = $("#txtCAMPO_S").val();
    obj["rtd_id"] = $("#txtID_S").val();
    obj["rtd_empresa"] = parseInt(empresasigned["emp_codigo"]);
    if ($("#cmbESTADO_S").val() != "")
        obj["rtd_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {    
    $("#cmbESTADO_S").val(""); 
    $("#txtCAMPO_S").val("");
    $("#txtID_S").val("");   
    ReloadData();
}

function CleanForm() {
    $("#cmbTABLACOA").val("");
    $("#txtCODIGO").val("");
    $("#txtTABLACOA_key").val("");
    $("#txtCODIGO_key").val("");
    $("#txtID").val("");
    $("#txtCAMPO").val("");
    $("#chkESTADO").prop("checked", true);
}