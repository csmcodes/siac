var formname = "wfTipopago.aspx";
var menuoption = "Tipopago";
function SetForm() {
    CleanForm();
    SetAutocompleteById("txtCUENTA");
    SetAutocompleteById("txtCODCLIPRO");

}
function GetJSONObject() {
    var obj = {};   
    obj["tpa_empresa"] =  parseInt(empresasigned["emp_codigo"]);
    obj["tpa_codigo"] =  parseInt($("#txtCODIGO").val());
    obj["tpa_codigo_key"] = parseInt($("#txtCODIGO_key").val());
    obj["tpa_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    obj["tpa_transacc"] = parseInt($("#cmbTRANSACC").val());    
    obj["tpa_id"] = $("#txtID").val();
    obj["tpa_nombre"] = $("#txtNOMBRE").val();
    obj["tpa_contabiliza"] = parseInt($("#cmbCONTABILIZA").val());
    obj["tpa_tclipro"] = parseInt($("#txttclipro").val());
    obj["tpa_cuenta"] = parseInt($("#txtCODCCUENTA").val());
    obj["tpa_codclipro"] = parseInt($("#cmbCODCLIPRO").val());
    obj["tpa_detalle"] =  parseInt($("#txtDETALLE").val());
    obj["tpa_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    CleanForm();
    $("#txtCODIGO").val( obj["tpa_codigo"]);
    $("#txtCODIGO_key").val( obj["tpa_codigo_key"]);   
    $("#cmbTRANSACC").val(obj["tpa_transacc"]);
    $("#txtID").val( obj["tpa_id"]);
    $("#txtNOMBRE").val(obj["tpa_nombre"] );
    $("#cmbTCLIPRO").val( obj["tpa_tclipro"] );
    $("#txtCODCCUENTA").val(obj["tpa_cuenta"]);
    $("#cmbCODCLIPRO").val(obj["tpa_codclipro"]);
    $("#cmbCONTABILIZA").val(obj["tpa_contabiliza"]);
    $("#txtDETALLE").val(obj["tpa_detalle"]);

    $("#txtCUENTA").val(obj["tpa_nombrecuenta"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["tpa_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};   
    obj["tpa_nombre"] = $("#txtNOMBRE_S").val();
    obj["tpa_id"] = $("#txtID_S").val();
    obj["tpa_tclipro"] = parseInt($("#txttclipro").val()); 
    obj["tpa_empresa"] = parseInt(empresasigned["emp_codigo"]); 
    if ($("#cmbESTADO_S").val() != "")
        obj["tpa_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {    
    $("#cmbESTADO_S").val("");  
    $("#txtNOMBRE_S").val("");
    $("#txtID_S").val("");
    ReloadData();
 }

function CleanForm() {
    $("#txtCODIGO").val("");
    $("#txtCODCCUENTA").val("");
    $("#txtCODIGO_key").val("");
    $("#cmbTRANSACC").val("");
    $("#txtID").val("");
    $("#txtNOMBRE").val("");
    $("#cmbTCLIPRO").val("");
    $("#txtCUENTA").val("");
    $("#txtCODCLIPRO").val("");
    $("#cmbCODCLIPRO").val("");
    $("#txtDETALLE").val("");
    $("#cmbCONTABILIZA").val("");
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
    if (idobj == "txtCODCLIPRO") {
        return {
            label: item.per_id + "," + item.per_nombres + " " + item.per_apellidos,
            value: item.per_nombres + " " + item.per_apellidos,
            info: item
        }
    }

}
function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCUENTA") {
        $("#txtCODCCUENTA").val(item.info.cue_codigo);
    }
    if (idobj == "txtCODCLIPRO") {
        $("#cmbCODCLIPRO").val(item.info.per_codigo);
    }

}
