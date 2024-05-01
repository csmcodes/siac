var formname = "wfAlmacen.aspx";
var menuoption = "Almacen";
function SetForm() {
CleanForm();

    SetPaisProvinciaCanton("cmbPAIS", "cmbPROVINCIA", "cmbCANTON");
    SetAutocompleteById("txtCUENTA");
    SetAutocompleteById("txtCODCLIPRO");
  
}

function GetJSONObject() {
    var obj = {};
    
    obj["alm_empresa"] = parseInt(empresasigned["emp_codigo"]);  //parseInt( empresasigned["emp_codigo"]);
    obj["alm_codigo"] = parseInt( $("#txtCODIGO").val());
    obj["alm_codigo_key"] = parseInt( $("#txtCODIGO_key").val());
    obj["alm_empresa_key"] = parseInt(empresasigned["emp_codigo"]); //parseInt(empresasigned["emp_codigo"]);
    obj["alm_id"] = $("#txtID").val();
    obj["alm_nombre"] = $("#txtNOMBRE").val();
    obj["alm_subfijo"] = $("#txtSUBFIJO").val();
    obj["alm_gerente"] = $("#txtGERENTE").val();
    obj["alm_pais"] =  parseInt($("#cmbPAIS").val());
    obj["alm_provincia"] = parseInt($("#cmbPROVINCIA").val());
   
    obj["alm_canton"] =  parseInt($("#cmbCANTON").val());
    obj["alm_direccion"] = $("#txtDIRECCION").val();
    obj["alm_telefono1"] = $("#txtTELEFONO1").val();
    obj["alm_telefono2"] = $("#txtTELEFONO2").val();
    obj["alm_telefono3"] = $("#txtTELEFONO3").val();
    obj["alm_ruc"] = $("#txtRUC").val();
    obj["alm_fax"] = $("#txtFAX").val();
    obj["alm_cliente_def"] = parseInt($("#cmbCODCLIPRO").val());
    obj["alm_centro"] = parseInt( $("#cmbCENTRO").val());
    obj["alm_matriz"] = GetCheckValue($("#chkMATRIZ"));
    obj["alm_cuentacaja"] = parseInt( $("#txtCODCCUENTA").val());
    obj["alm_estado"] = GetCheckValue($("#chkESTADO"));
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}
function sleep(milliseconds) {
    var start = new Date().getTime();
    for (var i = 0; i < 1e7; i++) {
        if ((new Date().getTime() - start) > milliseconds) {
            break;
        }
    }
}



function SetJSONObject(obj) {
    CleanForm();
    
    $("#txtCODIGO").val( obj["alm_codigo"]);
    $("#txtCODIGO_key").val(obj["alm_codigo_key"]);
   
    $("#txtID").val(obj["alm_id"]);
    $("#txtNOMBRE").val(obj["alm_nombre"]);
    $("#txtSUBFIJO").val(obj["alm_subfijo"]);
    $("#txtGERENTE").val(obj["alm_gerente"]);
    $("#cmbPAIS").val(obj["alm_pais"]);
    //sleep(1000);
  //  setTimeout($("#cmbPROVINCIA").val(obj["alm_provincia"]), 3000);
   $("#cmbPROVINCIA").val(obj["alm_provincia"]).change();     
    $("#cmbCANTON").val(obj["alm_canton"]);
    $("#txtDIRECCION").val(obj["alm_direccion"]);
    $("#txtTELEFONO1").val(obj["alm_telefono1"]);
    $("#txtTELEFONO2").val( obj["alm_telefono2"]);
    $("#txtTELEFONO3").val(obj["alm_telefono3"]);
    $("#txtRUC").val(obj["alm_ruc"]);
    $("#txtFAX").val(obj["alm_fax"]);
    $("#cmbCODCLIPRO").val(obj["alm_cliente_def"]);
    $("#cmbCENTRO").val(obj["alm_centro"]);
    $("#txtCODCCUENTA").val(obj["alm_cuentacaja"]);
    $("#chkMATRIZ").prop("checked", SetCheckValue(obj["alm_matriz"]));
    if (obj["alm_matriz"] > 0)
        $("#chkMATRIZ").attr("disabled", false);
    else {
        LoadMatriz();
        $("#chkMATRIZ").attr("disabled", true);
    }
    $("#chkESTADO").prop("checked", SetCheckValue(obj["alm_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Modificado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
    $("#txtCUENTA").val(obj["alm_nombrecuenta"]);
}

function GetJSONSearch() {
    var obj = {};
    obj["alm_codigo"] =  parseInt($("#txtCODIGO_S").val());
    obj["alm_nombre"] = $("#txtNOMBRE_S").val();
    obj["alm_id"] = $("#txtID_S").val();
    obj["alm_ruc"] = $("#txtRUC_S").val();
    obj["alm_gerente"] = $("#txtGERENTE_S").val();
    obj["alm_empresa"] = parseInt(empresasigned["emp_codigo"]); 
    if ($("#cmbESTADO_S").val() != "")
        obj["alm_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
    
    $("#cmbESTADO_S").val("");
    $("#txtCODIGO_S").val("");
    $("#txtNOMBRE_S").val("");
    $("#txtID_S").val("");
    $("#txtRUC_S").val("");
    $("#txtGERENTE_S").val(""); 
    ReloadData();
}

function CleanForm() {
    
    $("#txtCODIGO").val("");
    $("#txtCODIGO_key").val("");
    $("#txtCUENTA").val("");
    $("#txtCODCLIPRO").val("");
    $("#txtID").val("");
    $("#txtNOMBRE").val("");
    $("#txtSUBFIJO").val("");
    $("#txtGERENTE").val("");
    $("#cmbPAIS").val("");
    $("#cmbPROVINCIA").val("");
    $("#cmbCANTON").val("");
    $("#txtDIRECCION").val("");
    $("#txtTELEFONO1").val("");
    $("#txtTELEFONO2").val("");
    $("#txtTELEFONO3").val("");
    $("#txtRUC").val("");
    $("#txtFAX").val("");
    $("#cmbCODCLIPRO").val("");
    $("#cmbCENTRO").val("");
    $("#txtCODCCUENTA").val("");
    $("#chkMATRIZ").prop("checked", false);
    $("#chkESTADO").prop("checked", false);
     LoadMatriz();
}
function LoadMatriz() {
    CallServer1(formname + "/LoadMatriz", "{}", 0);
}
function LoadMatrizResult(disable) {
    $("#chkMATRIZ").attr("disabled", disable.d);
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

//Funciona que invoca al servidor mediante JSON
function CallServer1(strurl, strdata, retorno) {
    ClearValidate(); 
    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (retorno == 0)
                LoadMatrizResult(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var errorData = $.parseJSON(XMLHttpRequest.responseText);
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert(errorData.Message, 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }

    })
}