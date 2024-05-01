var formname = "wfProducto.aspx";
var menuoption = "Producto";
function SetForm() {
    CleanForm();
}
function GetJSONObject() {
    var obj = {};
    obj["pro_empresa"] =parseInt(empresasigned["emp_codigo"]);
    obj["pro_codigo"] = $("#txtCODIGO").val();
    obj["pro_codigo_key"] = $("#txtCODIGO_key").val();
    obj["pro_empresa_key"] =parseInt(empresasigned["emp_codigo"]);
    obj["pro_id"] = $("#txtID").val();   
    obj["pro_nombre"] = $("#txtNOMBRE").val();
    obj["pro_tproducto"] = parseInt($("#cmbTPRODUCTO").val());
    obj["pro_unidad"] = parseInt($("#cmbMEDIDA").val());
    obj["pro_grupo"] = parseInt($("#cmbGRUPO").val());
    obj["pro_calcula"] = GetCheckValue($("#txtCALCULA"));
    obj["pro_total"] = GetCheckValue($("#txtTOTAL"));
    obj["pro_iva"] = GetCheckValue($("#txtIVA"));
    obj["pro_inventario"] = GetCheckValue($("#txtINVENTARIO"));
    obj["pro_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
  
    $("#txtCODIGO").val( obj["pro_codigo"]);
    $("#txtCODIGO_key").val(obj["pro_codigo_key"]);
   
    $("#txtID").val(obj["pro_id"]);
    $("#cmbMEDIDA").val(obj["pro_unidad"] );
    $("#txtNOMBRE").val(obj["pro_nombre"]);
    $("#cmbTPRODUCTO").val(obj["pro_tproducto"]);
    $("#cmbGRUPO").val(obj["pro_grupo"]);
    
    $("#txtIVA").prop("checked", SetCheckValue(obj["pro_iva"]));
    $("#txtINVENTARIO").prop("checked", SetCheckValue(obj["pro_inventario"]));
    $("#txtCALCULA").prop("checked", SetCheckValue(obj["pro_calcula"]));
    $("#txtTOTAL").prop("checked", SetCheckValue(obj["pro_total"]));
    $("#chkESTADO").prop("checked", SetCheckValue(obj["pro_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["pro_codigo"] = $("#txtCODIGO_S").val();
    obj["pro_nombre"] = $("#txtNOMBRE_S").val();
    obj["pro_id"] = $("#txtID_S").val();
    obj["pro_empresa"] = parseInt(empresasigned["emp_codigo"]);
    if ($("#cmbESTADO_S").val() != "")
        obj["pro_estado"] = parseInt($("#cmbESTADO_S").val());
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
    var numero = parseInt($("#txtNproducto").val());
    numero = numero + 1;
    numero = "0000000" +numero ;
    $("#txtCODIGO").val("");
    $("#txtCODIGO_key").val("");
    $("#cmbGRUPO").val("");
    $("#txtCALCULA").prop("checked", false);
    $("#txtTOTAL").prop("checked", false);
    $("#txtID").val(numero);
    $("#cmbMEDIDA").val("");
    $("#txtIVA").prop("checked", false);
    $("#txtNOMBRE").val("");
    $("#cmbTPRODUCTO").val("");
    $("#txtINVENTARIO").prop("checked", false);
    $("#chkESTADO").prop("checked", true);
}