var formname = "wfBanco.aspx";
var menuoption = "Banco";
function SetForm() {
    CleanForm();
    SetAutocompleteById("txtCUENTA");
}
function GetJSONObject() {
    var obj = {};
    obj["ban_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["ban_codigo"] = parseInt($("#txtCODIGO").val());
    obj["ban_codigo_key"] = parseInt($("#txtCODIGO_key").val());
    obj["ban_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    obj["ban_id"] = $("#txtID").val();
    obj["ban_nombre"] = $("#txtNOMBRE").val();
    obj["ban_tipo"] = parseInt($("#cmbTIPO").val());
    obj["ban_cuenta"] = parseInt($("#txtCODCCUENTA").val());
    obj["ban_numero"] = parseInt($("#txtNUMERO").val());
    obj["ban_nro_cheque"] = parseInt($("#txtNUMEROCHEQUE").val());
    obj["ban_ultcsc"] = $("#txtULTCSC").val();
    obj["ban_codimp"] = $("#txtCODIMP").val();
    obj["ban_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {

    $("#txtCODIGO").val(obj["ban_codigo"]);
    $("#txtCODIGO_key").val(obj["ban_codigo_key"]);
    $("#txtCUENTA").val(obj["ban_nombrecuenta"]);
    $("#txtID").val(obj["ban_id"]);
    $("#txtNOMBRE").val(obj["ban_nombre"]);
    $("#txtCODCCUENTA").val(obj["ban_cuenta"]);
    $("#txtNUMERO").val(obj["ban_numero"]);
    $("#txtNUMEROCHEQUE").val(obj["ban_nro_cheque"]);
    $("#txtULTCSC").val(GetDateStringValue(obj["ban_ultcsc"]));
    $("#txtCODIMP").val(obj["ban_codimp"]);
    $("#cmbTIPO").val(obj["ban_tipo"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["ban_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["ban_codigo"] = parseInt($("#txtCODIGO_S").val());
    obj["ban_nombre"] = $("#txtNOMBRE_S").val();
    obj["ban_id"] = $("#txtID_S").val();
    obj["ban_empresa"] = parseInt(empresasigned["emp_codigo"]); 
    if ($("#cmbESTADO_S").val() != "")
        obj["ban_estado"] = parseInt($("#cmbESTADO_S").val());
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
    $("#txtCUENTA").val("");
    
    $("#txtID").val("");
    $("#txtNOMBRE").val("");

    $("#cmbTIPO").val("");
    $("#cmbCUENTA").val("");
    $("#txtNUMERO").val("");
    $("#txtNUMEROCHEQUE").val("");
    $("#txtULTCSC").val("");
    $("#txtCODIMP").val("");
    $("#chkESTADO").prop("checked", true);
}
function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCUENTA") {
        return {
            label: item.cue_id + "," + item.cue_nombre,
            value: item.cue_nombre,
            info: item
        }
    }

}
function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCUENTA") {
        $("#txtCODCCUENTA").val(item.info.cue_codigo);
    }

}
