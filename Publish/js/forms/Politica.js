var formname = "wfPolitica.aspx";
var menuoption = "Politica";
function SetForm() {
    CleanForm();
}
function GetJSONObject() {
    var obj = {};
    obj["pol_empresa"] =parseInt(empresasigned["emp_codigo"]);
    obj["pol_codigo"] = $("#txtCODIGO").val();
    obj["pol_codigo_key"] = $("#txtCODIGO_key").val();    
    obj["pol_empresa_key"] =parseInt(empresasigned["emp_codigo"]);
    obj["pol_id"] = $("#txtID").val();
    obj["pol_nombre"] = $("#txtNOMBRE").val();
    obj["pol_monto_ini"] = $("#txtMONTO_INI").val();
    obj["pol_monto_fin"] = $("#txtMONTO_FIN").val();
    obj["pol_porc_desc"] = $("#txtPORC_DESC").val();
    obj["pol_promocion"] = $("#txtPROMOCION").val();
    obj["pol_porc_pago_con"] = $("#txtPORC_PAGO_CON").val();
    obj["pol_porc_finanacia"] = $("#txtPORC_FINANACIA").val();
    obj["pol_nro_pagos"] = $("#txtNRO_PAGOS").val();
    obj["pol_dias_plazo"] = $("#txtDIAS_PLAZO").val();
    obj["pol_estado"] = GetCheckValue($("#chkESTADO"));
    obj["pol_tclipro"] = parseInt($("#txttclipro").val());
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
   
    $("#txtCODIGO").val( obj["pol_codigo"]);
    $("#txtCODIGO_key").val(obj["pol_codigo_key"]);
   
    $("#txtID").val(obj["pol_id"]);
    $("#txtNOMBRE").val(obj["pol_nombre"]);
    $("#txtMONTO_INI").val(obj["pol_monto_ini"]);
    $("#txtMONTO_FIN").val( obj["pol_monto_fin"]);
    $("#txtPORC_DESC").val(obj["pol_porc_desc"]);
    $("#txtPROMOCION").val(obj["pol_promocion"]);
    $("#txtPORC_PAGO_CON").val(obj["pol_porc_pago_con"]);
    $("#txtPORC_FINANACIA").val(obj["pol_porc_finanacia"]);
    $("#txtNRO_PAGOS").val(obj["pol_nro_pagos"]);
    $("#txtDIAS_PLAZO").val(obj["pol_dias_plazo"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["pol_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["pol_codigo"] = $("#txtCODIGO_S").val();
    obj["pol_nombre"] = $("#txtNOMBRE_S").val();
    obj["pol_id"] = $("#txtID_S").val();
    obj["pol_tclipro"] = parseInt($("#txttclipro").val()); 
    obj["pol_empresa"] = parseInt(empresasigned["emp_codigo"]);
    if ($("#cmbESTADO_S").val() != "")
        obj["pol_estado"] = parseInt($("#cmbESTADO_S").val());
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
    $("#txtMONTO_INI").val("");
    $("#txtMONTO_FIN").val("");
    $("#txtPORC_DESC").val("");
    $("#txtPROMOCION").val("");
    $("#txtPORC_PAGO_CON").val("");
    $("#txtPORC_FINANACIA").val("");
    $("#txtNRO_PAGOS").val("");
    $("#txtDIAS_PLAZO").val("");
    $("#chkESTADO").prop("checked", true);
}