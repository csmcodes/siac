var formname = "wfRuta.aspx";
var menuoption = "Ruta";

function SetForm() {
    CleanForm();
}
function GetJSONObject() {
    var obj = {};
    obj["rut_codigo"] = $("#txtCODIGO").val();

    obj["rut_codigo_key"] = $("#txtCODIGO_key").val();
    obj["rut_empresa"] =parseInt(empresasigned["emp_codigo"]);
    obj["rut_empresa_key"] =parseInt(empresasigned["emp_codigo"]);
    obj["rut_id"] = $("#txtID").val();
    obj["rut_nombre"] = $("#txtNOMBRE").val();
    obj["rut_origen"] = $("#cmbORIGEN  option:selected").text();
    obj["rut_destino"] = $("#cmbDESTINO option:selected").text();
    obj["rut_kilometros"] = $("#txtKILOMETROS").val();
    obj["rut_duracion"] = $("#txtTIEMPO").val();
    obj["rut_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);   
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#txtCODIGO").val(obj["rut_codigo"]);
    $("#txtCODIGO_key").val(obj["rut_codigo_key"]);
   
    $("#txtID").val(obj["rut_id"]);
    $("#txtNOMBRE").val(obj["rut_nombre"]);
  /* var origen = 'option[text=\"' + obj["rut_origen"] + '\"]';
    $("#cmbORIGEN").find(origen)
    */
 //   $("#cmbORIGEN").find('option[text="Guayaquil"]').attr("selected", "selected");
    $("#cmbORIGEN option:contains(" + obj["rut_origen"] + ")").attr("selected", "selected");
    $("#cmbDESTINO option:contains(" + obj["rut_destino"] + ")").attr("selected", "selected");
//    $("#cmbORIGEN").val(obj["rut_origen"]);
  //  $("#cmbDESTINO").val(obj["rut_destino"]);
    $("#txtKILOMETROS").val(obj["rut_kilometros"]);
     $("#txtTIEMPO").val(obj["rut_duracion"]);

     $("#chkESTADO").prop("checked", SetCheckValue(obj["rut_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
}

function GetJSONSearch() {
    var obj = {};
    obj["rut_nombre"] = $("#txtNOMBRE_S").val();
    obj["rut_origen"] = $("#cmbORIGEN_S").val();
    obj["rut_destino"] = $("#cmbDESTINO_S").val();
    obj["rut_empresa"] = parseInt(empresasigned["emp_codigo"]);
    if ($("#cmbESTADO_S").val() != "")
        obj["rut_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
    $("#txtCODIGO_S").val("");
    $("#txtNOMBRE_S").val("");
    
    $("#cmbORIGEN_S").val("");
    $("#cmbDESTINO_S").val("");
    $("#txtKILOMETROS_S").val("");
    $("#txtTIEMPO_S").val("");
    $("#cmbESTADO_S").val("");
    ReloadData();
}

function CleanForm() {
    $("#txtCODIGO").val("");
    $("#txtCODIGO_key").val("");
    
    
    $("#txtID").val("");
    $("#txtNOMBRE").val("");
    $("#cmbORIGEN").val("");
    $("#cmbDESTINO").val("");
    $("#txtKILOMETROS").val("");
    $("#txtTIEMPO").val("");
    $("#chkESTADO").prop("checked", true);
}