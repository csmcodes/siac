var formname = "wfTiponc.aspx";
var menuoption = "Tiponc";

function SetForm() {
    CleanForm();
    SetAutocompleteById("txtCUENTANC");
    SetAutocompleteById("txtCUENTAND");
}
function GetJSONObject() {
    var obj = {};
    obj["tnc_codigo"] = $("#txtCODIGO").val();
    obj["tnc_codigo_key"] = $("#txtCODIGO_key").val();
    obj["tnc_empresa"] =parseInt(empresasigned["emp_codigo"]);
    obj["tnc_empresa_key"] =parseInt(empresasigned["emp_codigo"]);
    obj["tnc_id"] = $("#txtID").val();
    obj["tnc_nombre"] = $("#txtNOMBRE").val();
    if ($("#txtCUENTANC").val().length > 0)
       { obj["tnc_cuentanc"] = $("#txtCODCUENTANC").val();}
    if ($("#txtCUENTAND").val().length > 0)
       { obj["tnc_cuentand"] = $("#txtCODCUENTAND").val(); }
       obj["tnc_estado"] = GetCheckValue($("#chkESTADO"));
       obj["tnc_tclipro"] = parseInt($("#txttclipro").val());
   obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#txtCODIGO").val(obj["tnc_codigo"]);
    $("#txtCODIGO_key").val(obj["tnc_codigo_key"]);   
    $("#txtID").val(obj["tnc_id"]);
    $("#txtNOMBRE").val(obj["tnc_nombre"]);
    $("#txtCODCUENTANC").val(obj["tnc_cuentanc"]);
    $("#txtCODCUENTAND").val(obj["tnc_cuentand"]);
    if (obj["tnc_cuentanc"] != null)
        $("#txtCUENTANC").val(obj["tnc_nombrecuentanc"]);

    else
        $("#txtCUENTANC").val("");
    if (obj["tnc_cuentand"] != null)
        $("#txtCUENTAND").val(obj["tnc_nombrecuentand"]);
    else
        $("#txtCUENTAND").val("");
     $("#chkESTADO").prop("checked", SetCheckValue(obj["tnc_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["tnc_nombre"] = $("#txtNOMBRE_S").val();
    obj["tnc_cuentanc"] = $("#cmbCUENTANC_S").val();
    obj["tnc_cuentand"] = $("#cmbCUENTAND_S").val();
    obj["tnc_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["tnc_tclipro"] = parseInt($("#txttclipro").val()); 
    if ($("#cmbESTADO_S").val() != "")
        obj["tnc_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
    $("#txtCODIGO_S").val("");
    $("#txtNOMBRE_S").val("");    
    $("#cmbCUENTANC_S").val("");
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
    $("#txtCODCUENTANC").val("");
    $("#txtCODCUENTAND").val("");
    $("#txtCUENTANC").val("");
    $("#txtCUENTAND").val("");

    $("#txtKILOMETROS").val("");
    $("#txtTIEMPO").val("");
    $("#chkESTADO").prop("checked", true);
}



function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCUENTANC") {
        return {
            label: item.cue_id + "," + item.cue_nombre,
            value: item.cue_nombre,
            info: item
        }
    }
    if (idobj == "txtCUENTAND") {
        return {
            label: item.cue_id + "," + item.cue_nombre,
            value: item.cue_nombre,
            info: item
        }
    }
}
function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCUENTANC") {
        $("#txtCODCUENTANC").val(item.info.cue_codigo);
    }
    if (idobj == "txtCUENTAND") {
        $("#txtCODCUENTAND").val(item.info.cue_codigo);  
    }
}