var formname = "wfImpuesto.aspx";
var menuoption = "Impuesto";
function SetForm() {
    CleanForm();
    SetAutocompleteById("txtCUENTA");
}
function GetJSONObject() {
    var obj = {};
    obj["imp_empresa"] =parseInt(empresasigned["emp_codigo"]);
    obj["imp_codigo"] = $("#txtCODIGO").val();
    obj["imp_codigo_key"] = $("#txtCODIGO_key").val();    
    obj["imp_empresa_key"] =parseInt(empresasigned["emp_codigo"]);
    obj["imp_id"] = $("#txtID").val();
    obj["imp_nombre"] = $("#txtNOMBRE").val();
    obj["imp_concepto"] = $("#cmbCONCEPTO").val();
    obj["imp_cuenta"] = $("#txtCODCCUENTA").val();
    obj["imp_ivafuente"] = $("#cmbIVAFUENTE").val();
    obj["imp_servicio"] = GetCheckValue($("#chkSERVICIO"));
    obj["imp_porcentaje"] = $("#txtPORCENTAJE").val();
    obj["imp_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    
    $("#txtCODIGO").val( obj["imp_codigo"]);
    $("#txtCODIGO_key").val(obj["imp_codigo_key"]);
    $("#txtCUENTA").val(obj["imp_nombrecuenta"]);
    $("#txtID").val(obj["imp_id"]);
    $("#txtNOMBRE").val(obj["imp_nombre"]);
    $("#cmbCONCEPTO").val(obj["imp_concepto"]);
    $("#txtCODCCUENTA").val(obj["imp_cuenta"]);
    $("#cmbIVAFUENTE").val(obj["imp_ivafuente"]);
    $("#chkSERVICIO").prop("checked", SetCheckValue(obj["imp_servicio"]));
    $("#txtPORCENTAJE").val(obj["imp_porcentaje"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["imp_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["imp_codigo"] = $("#txtCODIGO_S").val();
    obj["imp_nombre"] = $("#txtNOMBRE_S").val();
    obj["imp_id"] = $("#txtID_S").val();
    obj["imp_empresa"] = parseInt(empresasigned["emp_codigo"]);  
    if ($("#cmbESTADO_S").val() != "")
        obj["imp_estado"] = parseInt($("#cmbESTADO_S").val());
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
    $("#txtCUENTA").val("");
    
    $("#txtID").val("");
    $("#txtNOMBRE").val("");
    $("#cmbCONCEPTO").val("");
    $("#txtCODCCUENTA").val("");
    $("#cmbIVAFUENTE").val("");
    $("#chkSERVICIO").prop("checked", false);
    $("#txtPORCENTAJE").val("");
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