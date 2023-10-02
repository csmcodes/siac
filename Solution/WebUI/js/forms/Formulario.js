var formname = "wfFormulario.aspx";
var menuoption = "Formulario";
function SetForm() {
    CleanForm();
}
function GetJSONObject() {
    var obj = {};    
    obj["for_codigo"] = $("#txtCODIGO").val();
    obj["for_codigo_key"] = $("#txtCODIGO_key").val();     
    obj["for_id"] = $("#txtID").val();
    obj["for_nombre"] = $("#txtNOMBRE").val();
    obj["for_descripcion"] = $("#txtDESCRIPCION").val();
    obj["for_ayuda"] = $("#txtAYUDA").val();
    obj["for_clase"] = $("#txtCLASE").val();
    obj["for_modulo"] = $("#cmbMODULO").val();
    obj["for_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {    
    $("#txtCODIGO").val( obj["for_codigo"]);
    $("#txtCODIGO_key").val(obj["for_codigo_key"]);    
    $("#txtID").val(obj["for_id"]);
    $("#txtNOMBRE").val(obj["for_nombre"]);
    $("#txtDESCRIPCION").val( obj["for_descripcion"]);
    $("#txtAYUDA").val(obj["for_ayuda"]);
    $("#txtCLASE").val(obj["for_clase"]);
    $("#cmbMODULO").val(obj["for_modulo"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["for_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["for_codigo"] = $("#txtCODIGO_S").val();
    obj["for_nombre"] = $("#txtNOMBRE_S").val();
    obj["for_id"] = $("#txtID_S").val();     
    if ($("#cmbESTADO_S").val() != "")
        obj["for_estado"] = parseInt($("#cmbESTADO_S").val());
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
    $("#txtDESCRIPCION").val("");
    $("#txtAYUDA").val("");
    $("#txtCLASE").val("");
    $("#cmbMODULO").val("");
    $("#chkESTADO").prop("checked", true);
}