var formname = "wfCatalogo.aspx";
var menuoption = "Catalogo";

function SetForm() {
    CleanForm();
    SetAutocompleteById("txtCODPADRE");
}
function GetJSONObject() {
    var obj = {};
    obj["cat_codigo"] = parseInt($("#txtCODIGO").val());
    obj["cat_codigo_key"] = parseInt($("#txtCODIGO_key").val());
    obj["cat_tipo"] = $("#txtTIPO").val();
    obj["cat_nombre"] = $("#txtNOMBRE").val();
    obj["cat_padre"] = parseInt($("#cmbPADRE").val());
    obj["cat_padre_nombre"] = $("#txtCODPADRE").val();
    obj["cat_estado"] = GetCheckValue($("#chkESTADO")); 
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#txtCODIGO").val(obj["cat_codigo"]);
    $("#txtCODIGO_key").val(obj["cat_codigo_key"]);
    $("#txtTIPO").val(obj["cat_tipo"] );
    $("#txtNOMBRE").val(obj["cat_nombre"]);
    $("#txtCODPADRE").val(obj["cat_padre_nombre"]);
    $("#cmbPADRE").val(obj["cat_padre"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["cat_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Modificado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["cat_nombre"] = $("#txtNOMBRE_S").val();
    obj["cat_tipo"] = $("#txtTIPO_S").val();
    if ($("#cmbESTADO_S").val() != "")
        obj["cat_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
    $("#txtNOMBRE_S").val("");
    $("#txtTIPO_S").val("");
    $("#cmbESTADO_S").val("");
    ReloadData();
}

function CleanForm() {
    $("#txtCODIGO").val("");
    $("#txtCODIGO_key").val("");
    $("#txtTIPO").val(""); ;
    $("#txtCODPADRE").val("");
    $("#txtNOMBRE").val("");
    $("#cmbPADRE").val("");
    $("#txtCODPADRE").val("");
    $("#txtNOMBRE").val("");
    $("#chkESTADO").prop("checked", true);
}
function SetAutoCompleteObj(idobj, item) {

    if (idobj == "txtCODPADRE") {
        return {
            label: item.cat_nombre + "," + item.cat_tipo,
            value: item.cat_nombre,
            info: item
        }
    }

}
function GetAutoCompleteObj(idobj, item) {
 
    if (idobj == "txtCODPADRE") {
        $("#cmbPADRE").val(item.info.cat_codigo);
    }

}
