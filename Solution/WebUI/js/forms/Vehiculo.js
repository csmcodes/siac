var formname = "wfVehiculo.aspx";
var menuoption = "Vehiculo";

function SetForm() {
    CleanForm();
}
function GetJSONObject() {
    var obj = {};
    obj["veh_codigo"] = parseInt($("#txtCODIGO").val());
    obj["veh_codigo_key"] = parseInt($("#txtCODIGO_key").val());
    obj["veh_nombre"] = $("#txtNOMBRE").val();
    obj["veh_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["veh_empresa_key"] =parseInt(empresasigned["emp_codigo"]);
    obj["veh_id"] = $("#txtID").val();
    obj["veh_disco"] = $("#txtDISCO").val();
    obj["veh_placa"] = $("#txtPLACA").val(); ;
    obj["veh_anio"] = parseInt($("#cmbANIO").val());
    obj["veh_modelo"] = $("#txtMODELO").val();
    obj["veh_tipovehiculo"] = $("#txtTIPOVEHICULO").val();
    obj["veh_nombreduenio"] = $("#cmbDUENIO option:selected").text();
    obj["veh_duenio"] =parseInt( $("#cmbDUENIO").val());
    obj["veh_chofer1"] =parseInt( $("#cmbCHOFER1").val());
    obj["veh_chofer2"] = parseInt($("#cmbCHOFER2").val());
    obj["veh_estado"] = GetCheckValue($("#chkESTADO"));

    obj["veh_empresa_cho2"] = parseInt(empresasigned["emp_codigo"]);
    obj["veh_empresa_cho1"] = parseInt(empresasigned["emp_codigo"]);

    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {   
   $("#txtCODIGO").val(obj["veh_codigo"] );
   $("#txtCODIGO_key").val(obj["veh_codigo_key"]);
 
   $("#txtNOMBRE").val(obj["veh_nombre"]);
   $("#txtDISCO").val(obj["veh_disco"]);
   $("#txtID").val(obj["veh_id"]);
   $("#txtPLACA").val(obj["veh_placa"]); 
   $("#cmbANIO").val(obj["veh_anio"]);
   $("#txtMODELO").val(obj["veh_modelo"]);
   $("#txtTIPOVEHICULO").val(obj["veh_tipovehiculo"]);
   $("#cmbDUENIO").val(obj["veh_duenio"]);
   $("#cmbCHOFER1").val(obj["veh_chofer1"]);
    $("#cmbCHOFER2").val(obj["veh_chofer2"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["veh_estado"]));
  //  obj["veh_nombreduenio"] = $("#cmbDUENIO option:selected").text();
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["veh_duenio"] = $("#cmbDUENIO_S").val();
    obj["veh_placa"] = $("#txtPLACA_S").val();
    obj["veh_modelo"] = $("#txtMODELO_S").val();
    obj["veh_tipovehiculo"] = $("#txtTIPOVEHICULO_S").val();
    obj["veh_empresa"] = parseInt(empresasigned["emp_codigo"]);    
    if ($("#cmbESTADO_S").val() != "")
        obj["veh_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
    $("#cmbDUENIO_S").val("");
    $("#txtPLACA_S").val("");
    $("#txtMODELO_S").val("");
    $("#txtTIPOVEHICULO_S").val("");
    $("#cmbESTADO_S").val("");
    ReloadData();
}

function CleanForm() {
    $("#txtCODIGO").val("");
    $("#txtCODIGO_key").val("");
    
    
    $("#txtNOMBRE").val("");
    $("#txtDISCO").val("");
    $("#txtPLACA").val("");
    $("#cmbANIO").val("");
    $("#txtMODELO").val("");
    $("#txtTIPOVEHICULO").val("");
    $("#cmbDUENIO").val("");
    $("#cmbCHOFER1").val("");
    $("#cmbCHOFER2").val("");
    $("#txtID").val("");
    $("#chkESTADO").prop("checked", true);
}