﻿var formname = "wfAutpersona.aspx";
var menuoption = "Autpersona";
function SetForm() {
    CleanForm();
    SetAutocompleteById("txtCODCLIPRO");
}

function GetJSONObject() {
    var obj = {};
    obj["ape_val_fecha"] = $("#cmbFECHA").val();
    obj["ape_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["ape_nro_autoriza"] = $("#txtNRO_AUTORIZA").val();
    obj["ape_fac1"] =  $("#txtFAC1").val();
    obj["ape_fac2"] =  $("#txtFAC2").val();
    obj["ape_retdato"] = parseInt($("#cmbRETDATO").val());
    obj["ape_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    obj["ape_nro_autoriza_key"] = $("#txtNRO_AUTORIZA_key").val();
    obj["ape_fac1_key"] =  $("#txtFAC1_key").val();
    obj["ape_fac2_key"] =  $("#txtFAC2_key").val();
    obj["ape_retdato_key"] = parseInt($("#txtRETDATO_key").val());
    obj["ape_tablacoa"] = parseInt($("#cmbTABLACOA").val());
    obj["ape_persona"] = parseInt($("#cmbPERSONA").val()); //parseInt(empresasigned["emp_informante"]);
    obj["ape_tclipro"] = parseInt($("#txttclipro").val());    
    obj["ape_fact1"] =  $("#txtFAC1").val();
    obj["ape_fact2"] =  $("#txtFAC2").val();
    obj["ape_fact3"] =  $("#txtFACT3").val();
    obj["ape_fac3"] =  $("#txtFAC3").val();
    obj["ape_estado"] = GetCheckValue($("#chkESTADO"));
    obj["ape_nombrepersona"] = $("#txtCODCLIPRO option:selected").text();
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    CleanForm();
    $("#txtNRO_AUTORIZA").val(obj["ape_nro_autoriza"]);
    $("#txtFAC1").val(obj["ape_fac1"]);
    $("#txtFAC2").val(obj["ape_fac2"]);
    $("#cmbRETDATO").val(obj["ape_retdato"]);
    $("#txtNRO_AUTORIZA_key").val(obj["ape_nro_autoriza_key"]);
    $("#txtFAC1_key").val(obj["ape_fac1_key"]);
    $("#txtCODCLIPRO").val(obj["ape_nombrepersona"]);
    $("#txtFAC2_key").val(obj["ape_fac2_key"]);
    $("#txtRETDATO_key").val(obj["ape_retdato_key"]);
    $("#cmbTABLACOA").val(obj["ape_tablacoa"]);
    $("#cmbPERSONA").val(obj["ape_persona"]);
    $("#cmbTCLIPRO").val(obj["ape_tclipro"]);
    $("#txtFAC1").val(obj["ape_fact1"]);
    $("#txtFAC2").val(obj["ape_fact2"]);
    $("#txtFACT3").val(obj["ape_fact3"]);
    $("#txtFAC3").val(obj["ape_fac3"]);
    $("#cmbPERSONA").val(obj["ape_persona"])
    $("#chkESTADO").prop("checked", SetCheckValue(obj["ape_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
    $("#cmbFECHA").val(GetDateStringValue(obj["ape_val_fecha"]));

}

function GetJSONSearch() {

    var obj = {};
   
    obj["ape_nro_autoriza"] = $("#txtNRO_AUTORIZA_S").val();
    obj["ape_fac1"] = $("#txtFAC1_S").val();
    obj["ape_fac2"] = $("#txtFAC2_S").val();
    obj["ape_tclipro"] = parseInt($("#txttclipro").val());
    obj["ape_empresa"] = empresasigned["emp_codigo"];
    if ($("#cmbESTADO_S").val() != "")
        obj["tra_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
    $("#cmbESTADO_S").val("");
    $("#txtNRO_AUTORIZA_S").val("");
    $("#txtFAC1_S").val("");
    $("#txtFAC2_S").val("");
    
    ReloadData();
}

function CleanForm() {
    $("#txtNRO_AUTORIZA").val("");
    $("#txtFAC1").val("");
    $("#txtCODCLIPRO").val("");
    $("#txtFAC2").val("");
    $("#cmbRETDATO").val("");
    $("#txtNRO_AUTORIZA_key").val("");
    $("#txtFAC1_key").val("");
    $("#txtFAC2_key").val("");
    $("#txtRETDATO_key").val("");
    $("#cmbTABLACOA").val("");
    $("#cmbPERSONA").val("");
    $("#cmbTCLIPRO").val("");
    $("#txtFAC1").val("");
    $("#txtFAC3").val("");
    $("#txtFAC2").val("");
    $("#txtFACT3").val("");
    $("#cmbPERSONA").val("");
    $("#txtCODCLIPRO").val("");
    
    var dt = new Date();
    $("#cmbFECHA").val(dt.toLocaleString());
    $("#chkESTADO").prop("checked", true);
}



function SetAutoCompleteObj(idobj, item) { 
    if (idobj == "txtCODCLIPRO") {
        return {
            label: item.per_id + "," + item.per_nombres + " " + item.per_apellidos,
            value: item.per_nombres + " " + item.per_apellidos,
            info: item
        }
    }

}
function GetAutoCompleteObj(idobj, item) {   
    if (idobj == "txtCODCLIPRO") {
        $("#cmbPERSONA").val(item.info.per_codigo);
   
    }

}
