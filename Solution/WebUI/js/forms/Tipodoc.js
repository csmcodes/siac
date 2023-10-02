var formname = "wfTipodoc.aspx";
var menuoption = "Tipodoc";
function SetForm() {
    CleanForm();
  
}
function GetJSONObject() {
    var obj = {};
    obj["tpd_codigo"] = $("#txtCODIGO").val();
    obj["tpd_codigo_key"] = $("#txtCODIGO_key").val();
    obj["tpd_id"] = $("#txtID").val();
    obj["tpd_nombre"] = $("#txtNOMBRE").val();
    obj["tpd_modulo"] = $("#cmbMODULO").val();
    obj["tpd_for_imp"] = $("#cmbFOR_IMP").val();
    obj["tpd_for_eje"] = $("#cmbFOR_EJE").val();
    obj["tpd_for_con"] = $("#cmbFOR_CON").val();
    obj["tpd_nocontable"] = GetCheckValue($("#chkNOCONTABLE"));
    obj["tpd_nivel_aprobacion"] = $("#txtNIVELAPROVACION").val();
    obj["tpd_nro_copias"] = $("#txtNROCOPIAS").val();
    obj["tpd_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#txtCODIGO").val(obj["tpd_codigo"]);
    $("#txtCODIGO_key").val(obj["tpd_codigo_key"] );
    $("#txtID").val(obj["tpd_id"]);
    $("#txtNOMBRE").val(obj["tpd_nombre"]);
    $("#cmbMODULO").val(obj["tpd_modulo"]);
    $("#cmbFOR_IMP").val(obj["tpd_for_imp"]);
    $("#cmbFOR_EJE").val(obj["tpd_for_eje"]);
    $("#cmbFOR_CON").val(obj["tpd_for_con"]);  
    $("#txtNIVELAPROVACION").val(obj["tpd_nivel_aprobacion"]);
    $("#txtNROCOPIAS").val(obj["tpd_nro_copias"]);
    $("#chkNOCONTABLE").prop("checked", SetCheckValue(obj["tpd_nocontable"]));
    $("#chkESTADO").prop("checked", SetCheckValue(obj["tpd_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["tpd_codigo"] = $("#txtCODIGO_S").val();
    obj["tpd_nombre"] = $("#txtNOMBRE_S").val();
    obj["tpd_id"] = $("#txtID_S").val();    
    if ($("#cmbESTADO_S").val() != "")
        obj["tpd_estado"] = parseInt($("#cmbESTADO_S").val());
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
    $("#cmbMODULO").val("");
    $("#cmbFOR_IMP").val("");
    $("#cmbFOR_EJE").val("");
    $("#cmbFOR_CON").val("");
    $("#txtNIVELAPROVACION").val("");
    $("#txtNROCOPIAS").val("");
    $("#chkNOCONTABLE").prop("checked", false);
    $("#chkESTADO").prop("checked", true);
   
}