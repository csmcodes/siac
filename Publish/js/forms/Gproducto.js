var formname = "wfGproducto.aspx";
var menuoption = "Gproducto";

function SetForm() {
    CleanForm();
    SetAutocompleteById("txtCUENTACOS");
    SetAutocompleteById("txtCUENTAINV");
    SetAutocompleteById("txtCUENTADES");
    SetAutocompleteById("txtCUENTADEV");
    SetAutocompleteById("txtCUENTAVENTA");
    
}
function GetJSONObject() {
    var obj = {};
    obj["gpr_codigo"] = $("#txtCODIGO").val();
    obj["gpr_codigo_key"] = $("#txtCODIGO_key").val();
    obj["gpr_empresa"] =parseInt(empresasigned["emp_codigo"]);
    obj["gpr_empresa_key"] =parseInt(empresasigned["emp_codigo"]);
    obj["gpr_id"] = $("#txtID").val();
    obj["gpr_nombre"] = $("#txtNOMBRE").val();
 
    if ($("#txtCUENTACOS").val().length > 0)
    { obj["gpr_cta_costo"] = $("#txtCODCUENTACOS").val(); }
  
    if ($("#txtCUENTAINV").val().length > 0)
    { obj["gpr_cta_inv"] = $("#txtCODCUENTAINV").val(); }

    if ($("#txtCUENTADES").val().length > 0)
    { obj["gpr_cta_des"] = $("#txtCODCUENTADES").val(); }
 
    if ($("#txtCUENTADEV").val().length > 0)
    { obj["gpr_cta_dev"] = $("#txtCODCUENTADEV").val(); }
 
    if ($("#txtCUENTAVENTA").val().length > 0)
    { obj["gpr_cta_venta"] = $("#txtCODCUENTAVENTA").val(); }

    obj["gpr_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj); 
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#txtCODIGO").val(obj["gpr_codigo"]);
    $("#txtCODIGO_key").val(obj["gpr_codigo_key"]);   
    $("#txtID").val(obj["gpr_id"]);
    $("#txtNOMBRE").val(obj["gpr_nombre"]);
    $("#txtCODCUENTACOS").val(obj["gpr_cta_costo"]);
    $("#txtCODCUENTAINV").val(obj["gpr_cta_inv"]);
    $("#txtCODCUENTADES").val(obj["gpr_cta_des"]);
    $("#txtCODCUENTADEV").val(obj["gpr_cta_dev"]);
    $("#txtCODCUENTAVENTA").val(obj["gpr_cta_venta"]);
    if (obj["gpr_cta_costo"] != null)
        $("#txtCUENTACOS").val(obj["gpr_nombrecta_costo"]);
    else
        $("#txtCUENTACOS").val(""); 
   
   
    if (obj["gpr_cta_inv"] != null)
        $("#txtCUENTAINV").val(obj["gpr_nombrecta_inv"]);
    else
        $("#txtCUENTAINV").val("");

 
 
    if (obj["gpr_cta_des"] != null)
        $("#txtCUENTADES").val(obj["gpr_nombrecta_des"]);
    else
        $("#txtCUENTADES").val("");
   
  
  
    if (obj["gpr_cta_dev"] != null)
        $("#txtCUENTADEV").val(obj["gpr_nombrecta_dev"]);
    else
        $("#txtCUENTADEV").val("");
  
   
   
   
    if (obj["gpr_cta_venta"] != null)
        $("#txtCUENTAVENTA").val(obj["gpr_nombrecta_venta"]);
    else
        $("#txtCUENTAVENTA").val("");

    $("#chkESTADO").prop("checked", SetCheckValue(obj["gpr_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["gpr_nombre"] = $("#txtNOMBRE_S").val();
    obj["gpr_cuentanc"] = $("#cmbCUENTANC_S").val();
    obj["gpr_cuentand"] = $("#cmbCUENTAND_S").val();
    obj["gpr_empresa"] = parseInt(empresasigned["emp_codigo"]);
    if ($("#cmbESTADO_S").val() != "")
        obj["gpr_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
    $("#txtCODIGO_S").val("");
    $("#txtNOMBRE_S").val("");    
    $("#cmbCUENTANC_S").val("");   
    $("#cmbCUENTAND_S").val(""); 
    $("#cmbESTADO_S").val("");
    ReloadData();
}

function CleanForm() {
    $("#txtCODIGO").val("");
    $("#txtCODIGO_key").val("");
    $("#txtID").val("");
    $("#txtNOMBRE").val("");
    $("#txtCODCUENTACOS").val("");
    $("#txtCODCUENTAINV").val("");
    $("#txtCUENTACOS").val("");
    $("#txtCUENTAINV").val("");
    $("#txtCUENTADES").val("");
    $("#txtCUENTADEV").val("");
    $("#txtCUENTAVENTA").val("");
   
    $("#txtTIEMPO").val("");
    $("#chkESTADO").prop("checked", true);
}



function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCUENTACOS") {
        return {
            label: item.cue_id + "," + item.cue_nombre,
            value: item.cue_nombre,
            info: item
        }
    }
    if (idobj == "txtCUENTAINV") {
        return {
            label: item.cue_id + "," + item.cue_nombre,
            value: item.cue_nombre,
            info: item
        }
    }
    if (idobj == "txtCUENTADES") {
        return {
            label: item.cue_id + "," + item.cue_nombre,
            value: item.cue_nombre,
            info: item
        }
    }
    if (idobj == "txtCUENTADEV") {
        return {
            label: item.cue_id + "," + item.cue_nombre,
            value: item.cue_nombre,
            info: item
        }
    }
    if (idobj == "txtCUENTAVENTA") {
        return {
            label: item.cue_id + "," + item.cue_nombre,
            value: item.cue_nombre,
            info: item
        }
    }
    
}
function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCUENTACOS") {
        $("#txtCODCUENTACOS").val(item.info.cue_codigo);
    }
    if (idobj == "txtCUENTAINV") {
        $("#txtCODCUENTAINV").val(item.info.cue_codigo);
    }
    if (idobj == "txtCUENTADES") {
        $("#txtCODCUENTADES").val(item.info.cue_codigo);
    }
    if (idobj == "txtCUENTADEV") {
        $("#txtCODCUENTADEV").val(item.info.cue_codigo);
    }
    if (idobj == "txtCUENTAVENTA") {
        $("#txtCODCUENTAVENTA").val(item.info.cue_codigo);
    }
  
}