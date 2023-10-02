var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfListadoPersonas.aspx";
var menuoption = "VentasSocios";

function StopPropagation(event) {
    if (!event) var event = window.event;
    event.cancelBubble = true;
    if (event.stopPropagation) event.stopPropagation();
}

//Codigo ejecutado cuando el document esta listo
$(document).ready(function () {
   
    $('body').css('background', 'transparent');
    $("#guardar").on("click", SaveObj);
    $("#adddet").on("click", AddObj);
    $("#lista").on("click", Volver);
    $("#detalle").css({ 'display': 'none' });
    $("#busquedacontent").css({ 'display': 'none' });
    $("#guardar").css({ 'display': 'none' });
    $("#lista").css({ 'display': 'none' });
    LoadFiltros();
    LoadForm();
});

function ServerResult(data, retorno) {
    if (retorno == 0) {
        LoadFiltrosResult(data);
    }
    if (retorno == 1) {
        LoadDetalleResult(data);
    }
    if (retorno == 2) {
        LoadDetalleDataResult(data);
    }
     if (retorno == 3) {
        DeleteObjsResult(data);
    }
    if (retorno == 4) {
        LoadFormResult(data);
    }
    if (retorno == 5) {
        ShowObjResult(data);
    }
    if (retorno == 6) {
        SaveObjResult(data);
    }
   
}




function LoadFiltros() {
    var obj = {};
    //obj["uxe_usuario"] = usuariosigned["usr_id"];
    //obj["uxe_empresa"] = empresasigned["emp_codigo"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(formname + "/GetFiltros", jsonText, 0)
}

function LoadForm() {
    CallServerMethods(formname + "/GetForm", "{}", 4);


}
function LoadFormResult(data) {
    if (data != "") {


        $('#busquedacontent').html(data.d);


    }

    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha
    $(".chzn-select").chosen();
    // tabbed widget
    $(".tabbedwidget").tabs();

}

function LoadFiltrosResult(data) {
    if (data != "") {
        $('#comcabecera').html(data.d);
        $("#txtIDB").on("change", LoadDetalleData);
        $("#txtNombre").on("change", LoadDetalleData);
        LoadDetalleData();
      
    }
    SetForm(); //Depende de cada js.        
}

function SetForm() {

    $(".fecha").datepicker({
        dateFormat: "dd/mm/yy"
    });
    //$("#cmbTABLA").on("change", LoadTabla);  //Opción "Cerrar" del combo de opciones de la sección de edición    
    $("#cmbTABLA").trigger("change");
    //$("#cmbUSUARIO").on("change", LoadReporte);
    // $("#txtDESDE").on("change", LoadReporte);
    // $("#txtHASTA").on("change", LoadReporte);


}

var iframeReport = "ireport";
function loadIframeReport(url) {
    var $iframe = $('#' + iframeReport);
    if ($iframe.length) {
        $iframe.attr('src', url);
    }
}
function LoadDetalle() {
    var obj = {};
  
    obj["empresa"] = parseInt(empresasigned["emp_codigo"]);
   
   
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(formname + "/LoadDetalle", jsonText, 1);
}

function LoadDetalleResult(data) {

 


      

  
    SetFormDetalle();

}
function SetFormDetalle() {
    //Tratamiento para los elemntos del detalle    
   // $("#adddet").on("click", CallGuias);
   // $("#deldet").on("click", QuitGuias);
    //   $("#alldet").on("click", { tabla: "tddatos" }, SelectAllRows);
    //   $("#nonedet").on("click", { tabla: "tddatos" }, CleanSelectedRows);
   
    LoadDetalleData();
}
function LoadDetalleData() {
    var obj = {};

    obj["empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["nombre"] = $("#txtNombre").val();
    obj["id"] = $("#txtIDB").val();
    
    var jsonText = JSON.stringify({ objeto: obj });

    CallServerMethods(formname + "/LoadData", jsonText, 2);

}


function LoadDetalleDataResult(data) {
    if (data != "") {
        $('#comdetallecontent').html(data.d);
    }
    Scroll();
}





function RemoveRow(btn) {
    
    var row = $(btn).parents('tr'); 
    jConfirm('¿Está seguro que desea eliminar el registro?', 'Eliminar', function (r) {
        if (r) {
            $(btn).parents('tr').fadeOut(function () {

                var obj = {};
                obj["per_empresa"] = parseInt((row).data("empresa").toString());
                obj["per_codigo"] = (row).data("persona").toString();

                var jsonText = JSON.stringify({ objeto: obj });
                CallServerMethods(formname + "/Delete", jsonText, 3)
              //  $(this).remove();
            });
        }
    });
    //}
    //else
      //  CleanRow();

}
function DeleteObjsResult(data) {
    if (data != "") {
        if (data.d == "OK") {
           
                
              
                    $(this).remove();
                }
           

            LoadDetalleData();
        }

    }

    function EditRow(btn) {
        var row = $(btn).parents('tr'); 
        $("#comdetallecontent").css({ 'display': 'none' });
        $('#busquedacontent').show("fast");
        $("#adddet").css({ 'display': 'none' });
        $('#guardar').show("fast");
        $('#lista').show("fast");
        var obj = {};

        obj["per_empresa"] = parseInt((row).data("empresa").toString());

        obj["per_codigo"] = (row).data("persona").toString();

        ShowObj(obj);


    }
   


    function ShowObj(obj) {
       
        var jsonText = JSON.stringify({ id: obj });
        CallServerMethods(formname + "/GetObject", jsonText, 5)

    }


    function ShowObjResult(data) {
        if (data != "") {
            var obj = $.parseJSON(data.d);
            
            SetJSONObject(obj); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM
        }
    }

    function SetJSONObject(obj) {
    
        $("#txtCODIGO").val(obj["per_codigo"]);
     
        $("#txtCODIGO_key").val(obj["per_codigo_key"]);
        $("#txtCIRUC").val(obj["per_ciruc"]);
        $("#txtID").val(obj["per_id"]);
        $("#cmbTIPOID").val(obj["per_tipoid"]);
        $("#txtNOMBRES").val(obj["per_nombres"]);
        $("#txtAPELLIDOS").val(obj["per_apellidos"]);
        $("#txtDIRECCION").val(obj["per_direccion"]);
        $("#txtTELEFONO").val(obj["per_telefono"]);
        $("#txtCELULAR").val(obj["per_celular"]);
        $("#txtMAIL").val(obj["per_mail"]);
        $("#txtOBSERVACION").val(obj["per_observacion"]);
        $("#cmbPAIS").val(obj["per_pais"]);
        $("#cmbPROVINCIA").val(obj["per_provincia"]);
        $("#cmbCANTON").val(obj["per_canton"]);
        $("#cmbPARROQUIA").val(obj["per_parroquia"]);
        $("#txtCONTRIBUYENTE").val(obj["per_contribuyente"]);
        $("#chkCONTRIBUYENTEESPECIAL").prop("checked", SetCheckValue(obj["per_contribuyente_especial"]));
        $("#txtCONTACTO").val(obj["per_contacto"]);
        $("#txt_CONTACTODIRECCION").val(obj["per_contacto_direccion"]);
        $("#txtCONTACTOTELEFONO").val(obj["per_contacto_telefono"]);
        $("#txtRAZON").val(obj["per_razon"]);
        $("#cmbREPRESENTANTELEGAL").val(obj["per_representantelegal"]);
        $("#txtPAGINAWEB").val(obj["per_paginaweb"]);
        $("#chkESTADO").prop("checked", SetCheckValue(obj["per_estado"]));
        var objtipos = new Array();
        if (obj["tipos"].length == 0)
            objtipos[0] = 4;
        for (i = 0; i < obj["tipos"].length; i++) {
            objtipos[i] = obj["tipos"][i]["pxt_tipo"].toString();
            var aux = parseInt(objtipos[i]);
            switch (aux) {
                case cCliente:
                    $("#txtCATCLIENTE").val(obj["tipos"][i]["pxt_cat_persona"]);
                    $("#txtPOLCLIENTE").val(obj["tipos"][i]["pxt_politicas"]);
                    break;
                case cProveedor:
                    $("#txtCATPROVEEDOR").val(obj["tipos"][i]["pxt_cat_persona"]);
                    $("#txtPOLPROVEEDOR").val(obj["tipos"][i]["pxt_politicas"]);
                    break;
                case cChofer:
                    $("#txtCATCHOFER").val(obj["tipos"][i]["pxt_cat_persona"]);
                    $("#txtPOLCHOFER").val(obj["tipos"][i]["pxt_politicas"]);
                    break;
                case cSocio:
                    $("#txtCATSOCIO").val(obj["tipos"][i]["pxt_cat_persona"]);
                    $("#txtPOLSOCIO").val(obj["tipos"][i]["pxt_politicas"]);
                    break;
            }
        }

        $("#cmbTIPO").val(objtipos);
        $('#cmbTIPO').trigger('liszt:updated');
        $("#cmbGENERO").val(obj["per_genero"]);
        $("#cmbCPERSONA").val(obj["per_cpersona"]);
        $("#cmbTPERSONA").val(obj["per_tpersona"]);
        $("#cmbLISTAPRECIO").val(obj["per_listaprecio"]);
        $("#cmbPOLITICA").val(obj["per_politica"]);
        $("#cmbRETIVA").val(obj["per_retiva"]);
        $("#cmbRETFUENTE").val(obj["per_retfuente"]);
        $("#txtAGENTE").val(obj["per_agente"]);
        $("#txtBLOQUEO").val(obj["per_bloqueo"]);
        $("#txtTARJETA").val(obj["per_tarjeta"]);
        $("#txtCUPO").val(obj["per_cupo"]);
        $("#txtILIMITADO").val(obj["per_ilimitado"]);
        $("#txtIMPUESTO").val(obj["per_impuesto"]);
        SetJSONObjectSocio(obj["socio"]);
        SetJSONObjectChofer(obj["chofer"]);
        //    $("#chkCHO_ESTADO").prop("checked", SetCheckValue(obj["chofer"]["cho_estado"]);

        $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
        $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
    }


    function SetJSONObjectChofer(obj) {
        $("#txtNROLICENCIA").val(obj["cho_nrolicencia"]);
        $("#txtPUNTOSLICENCIA").val(obj["cho_puntoslicencia"]);
        $("#txtTIPOSANGRE").val(obj["cho_tiposangre"]);
        $("#cmbEMISIONLICENCIA").val(GetDateStringValue(obj["cho_emisionlicencia"]));
        $("#cmbRENOVACION").val(GetDateStringValue(obj["cho_renovacion"]));
        $("#cmbCADUCIDADLICENCIA").val(GetDateStringValue(obj["cho_caducidadlicencia"]));
        $("#txtTIPOLICENCIA").val(obj["cho_tipolicencia"]);
        $("#cmbPAISEMISION").val(obj["cho_paisemision"]);
        $("#cmbPROVINCIAEMISION").val(obj["cho_provinciaemision"]);
        $("#cmbCANTONEMISION").val(obj["cho_cantonemision"]);
        $("#cmbPAISRENOVACION").val(obj["cho_paisrenovacion"]);
        $("#cmbPROVINCIARENOVACION").val(obj["cho_provinciarenovacion"]);
        $("#cmbCANTONRENOVACION").val(obj["cho_cantonrenovacion"]);
        $("#cmbOBSERVACIONLICENCIA").val(obj["cho_observacionlicencia"]);
    }

    function SetJSONObjectSocio(obj) {

        $("#cmbFECHANACIMIENTO").val(GetDateStringValue(obj["soc_fechanacimiento"]));
        $("#cmbPAISNACIMIENTO").val(obj["soc_paisnacimiento"]);
        $("#cmbPROVINCIANACIMIENTO").val(obj["soc_provincianacimiento"]);
        $("#cmbCANTONNACIMIENTO").val(obj["soc_cantonnacimiento"]);
        $("#cmbESTADOCIVIL").val(obj["soc_estadocivil"]);
        $("#txtCARGAS").val(obj["soc_cargas"]);
        $("#txtINSCRIPCION").val(obj["soc_inscripcion"]);
        $("#cmbFECHAINSCRIPCION").val(GetDateStringValue(obj["soc_fechainscripcion"]));
        $("#cmbFECHASALIDA").val(GetDateStringValue(obj["soc_fechasalida"]));
        $("#txtRAZONSALIDA").val(obj["soc_razonsalida"]);
        $("#txtLUGARTRABAJO").val(obj["soc_lugartrabajo"]);
        $("#cmbCARGOTRABAJO").val(obj["soc_cargotrabajo"]);
        $("#cmbDEPARTAMENTO").val(obj["soc_departamento"]);
        $("#txtDIRECCIONTRABAJO").val(obj["soc_direcciontrabajo"]);
        $("#txtTELEFONOTRABAJO").val(obj["soc_telefonotrabajo"]);
        $("#txtFAXTRABAJO").val(obj["soc_faxtrabajo"]);
        $("#txtNROIESS").val(obj["soc_nroiess"]);
        $("#cmbBANCO").val(obj["soc_banco"]);
        $("#cmbTIPOCUENTA").val(obj["soc_tipocuenta"]);
        $("#txtNROCUENTA").val(obj["soc_nrocuenta"]);
        $("#cmbNIVELINSTRUCCION").val(obj["soc_nivelinstruccion"]);
        $("#txtPOSTGRADO").val(obj["soc_postgrado"]);
        $("#txtDOCTORADO").val(obj["soc_doctorado"]);
        $("#txtRECONOCIMIENTOS").val(obj["soc_reconocimientos"]);
        $("#txtTITULOS").val(obj["soc_titulos"]);
        $("#cmbPROFESION").val(obj["soc_profesion"]);
        $("#cmbFECHAGRADO").val(GetDateStringValue(obj["soc_fechagrado"]));
        $("#txtINSTITUCION").val(obj["soc_institucion"]);
        $("#txtCONADIS").val(obj["soc_conadis"]);

        $("#cmbCODIGOCONYUGE").val(obj["soc_codigoconyuge"]);

    }

    function SaveObj() {
        
        if (ValidateFormTab()) {

            var jsonText = GetJSONObject(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
            
            CallServerMethods(formname + "/SaveObject", jsonText, 6);
        }



       
    }


    function SaveObjResult(data) {
        if (data != "") {
            if (data.d != "OK" && data.d.toString() != "ERROR" && data.d.toString() != "ERRORC") {
                jQuery.jGrowl("Registro actualizado con éxito");
                CleanForm(); 
                $("#busquedacontent").css({ 'display': 'none' });
                $('#comdetallecontent').show("fast");
                $('#adddet').show("fast");
                $("#guardar").css({ 'display': 'none' });
                $("#lista").css({ 'display': 'none' });


            }
            if (data.d.toString() == "ERRORC") {
              
                jQuery.alerts.dialogClass = 'alert-danger';
                jAlert('Cedula incorecta...', 'Error', function () {
                    jQuery.alerts.dialogClass = null; // reset to default
                });

            }

            else {
               //TOCA DETERMINAR DEPENDIENDO DE CADA FORM  
                LoadDetalleData();
                jQuery.alerts.dialogClass = 'alert-danger';
                jAlert('Se ha producido un error al guardar la persona...', 'Error', function () {
                    jQuery.alerts.dialogClass = null; // reset to default
                });
            }
        }
    }




    function CleanForm() {
        $("#txtCODIGO").val("");
        $("#txtCODIGO_key").val("");


        $("#txtCIRUC").val("");
        $("#txtID").val("");
        $("#cmbTIPOID").val("");
        $("#txtNOMBRES").val("");
        $("#txtAPELLIDOS").val("");
        $("#txtDIRECCION").val("");
        $("#txtTELEFONO").val("");
        $("#txtCELULAR").val("");
        $("#txtMAIL").val("");
        $("#txtOBSERVACION").val("");
        $("#cmbPAIS").val("");
        $("#cmbPROVINCIA").val("");
        $("#cmbCANTON").val("");
        $("#txtCONTRIBUYENTE").val("");
        $("#chkCONTRIBUYENTEESPECIAL").prop("checked", false);
        $("#txtCONTACTO").val("");
        $("#txt_CONTACTODIRECCION").val("");
        $("#txtCONTACTOTELEFONO").val("");
        $("#txtRAZON").val("");
        $("#cmbREPRESENTANTELEGAL").val("");
        $("#txtPAGINAWEB").val("");
        $("#cmbPARROQUIA").val("");

        var tclicodpro = $("#txttclipro").val();
        var objtipos = new Array();
        if (tclicodpro > 0) {
            objtipos[0] = tclicodpro;
        } else {
            objtipos[0] = 4;
        }

        $("#cmbTIPO").val(objtipos);
        $('#cmbTIPO').trigger('liszt:updated');
        $("#cmbGENERO").val("");
        $("#cmbCPERSONA").val("");
        $("#cmbTPERSONA").val("");
        $("#cmbLISTAPRECIO").val("");
        $("#cmbPOLITICA").val("");
        $("#cmbRETIVA").val("");
        $("#cmbRETFUENTE").val("");
        $("#txtAGENTE").val("");
        $("#txtBLOQUEO").val("");
        $("#txtTARJETA").val("");
        $("#txtCUPO").val("");
        $("#txtILIMITADO").val("");
        $("#txtIMPUESTO").val("");
        $("#chkESTADO").prop("checked", true);

        $("#txtCATCLIENTE").val("");
        $("#txtPOLCLIENTE").val("");
        $("#txtCATPROVEEDOR").val("");
        $("#txtPOLPROVEEDOR").val("");
        $("#txtCATCHOFER").val("");
        $("#txtPOLCHOFER").val("");
        $("#txtCATSOCIO").val("");
        $("#txtPOLSOCIO").val("");
        CleanChoferForm();
        CleanSocioForm();
    }


    function CleanChoferForm() {
        $("#txtNROLICENCIA").val("");
        $("#txtPUNTOSLICENCIA").val("");
        $("#txtTIPOSANGRE").val("");
        $("#cmbEMISIONLICENCIA").val("");
        $("#cmbRENOVACION").val("");
        $("#cmbCADUCIDADLICENCIA").val("");
        $("#txtTIPOLICENCIA").val("");
        $("#cmbPAISEMISION").val("");
        $("#cmbPROVINCIAEMISION").val("");
        $("#cmbCANTONEMISION").val("");
        $("#cmbPAISRENOVACION").val("");
        $("#cmbPROVINCIARENOVACION").val("");
        $("#cmbCANTONRENOVACION").val("");
        $("#cmbOBSERVACIONLICENCIA").val("");
    }



    function CleanSocioForm() {
        $("#txtCODIGO").val("");
        $("#txtCODIGO_key").val("");
        $("#txtCODIGO").val("");
        $("#cmbFECHANACIMIENTO").val("");
        $("#cmbPAISNACIMIENTO").val("");
        $("#cmbPROVINCIANACIMIENTO").val("");
        $("#cmbCANTONNACIMIENTO").val("");
        $("#cmbESTADOCIVIL").val("");
        $("#txtCARGAS").val("");
        $("#txtINSCRIPCION").val("");
        $("#cmbFECHAINSCRIPCION").val("");
        $("#cmbFECHASALIDA").val("");
        $("#txtRAZONSALIDA").val("");
        $("#txtLUGARTRABAJO").val("");
        $("#cmbCARGOTRABAJO").val("");
        $("#cmbDEPARTAMENTO").val("");
        $("#txtDIRECCIONTRABAJO").val("");
        $("#txtTELEFONOTRABAJO").val("");
        $("#txtFAXTRABAJO").val("");
        $("#txtNROIESS").val("");
        $("#cmbBANCO").val("");
        $("#cmbTIPOCUENTA").val("");
        $("#txtNROCUENTA").val("");
        $("#cmbNIVELINSTRUCCION").val("");
        $("#txtPOSTGRADO").val("");
        $("#txtDOCTORADO").val("");
        $("#txtRECONOCIMIENTOS").val("");
        $("#txtTITULOS").val("");
        $("#cmbPROFESION").val("");
        $("#cmbFECHAGRADO").val("");
        $("#txtINSTITUCION").val("");
        $("#txtCONADIS").val("");
        $("#cmbCODIGOCONYUGE").val("");




    }



    function GetJSONObject() {
        var obj = {};
           
        obj["per_codigo"] = $("#txtCODIGO").val();
        obj["per_codigo_key"] = $("#txtCODIGO_key").val();
        obj["per_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["per_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
        obj["per_ciruc"] = $("#txtCIRUC").val();
        obj["per_id"] = $("#txtID").val();
        obj["per_tipoid"] = $("#cmbTIPOID").val();
        obj["per_nombres"] = $("#txtNOMBRES").val();
        obj["per_apellidos"] = $("#txtAPELLIDOS").val();
        obj["per_direccion"] = $("#txtDIRECCION").val();
        obj["per_telefono"] = $("#txtTELEFONO").val();
        obj["per_celular"] = $("#txtCELULAR").val();
        obj["per_mail"] = $("#txtMAIL").val();
        obj["per_observacion"] = $("#txtOBSERVACION").val();
        obj["per_pais"] = $("#cmbPAIS").val();
        obj["per_provincia"] = $("#cmbPROVINCIA").val();
        obj["per_canton"] = $("#cmbCANTON").val();
        obj["per_parroquia"] = $("#cmbPARROQUIA").val();
        obj["per_contribuyente"] = $("#txtCONTRIBUYENTE").val();
        obj["per_contribuyente_especial"] = GetCheckValue($("#chkCONTRIBUYENTEESPECIAL"));
        obj["per_contacto"] = $("#txtCONTACTO").val();
        obj["per_contacto_direccion"] = $("#txt_CONTACTODIRECCION").val();
        obj["per_contacto_telefono"] = $("#txtCONTACTOTELEFONO").val();
        obj["per_razon"] = $("#txtRAZON").val();
        obj["per_representantelegal"] = $("#cmbREPRESENTANTELEGAL").val();
        obj["per_paginaweb"] = $("#txtPAGINAWEB").val();
        obj["per_estado"] = GetCheckValue($("#chkESTADO"));
        obj["tipos"] = $("#cmbTIPO").val();
        obj["pxt"] = GetPXTObj();
       
        obj["per_genero"] = $("#cmbGENERO").val();
        obj["per_cpersona"] = $("#cmbCPERSONA").val();
        obj["per_tpersona"] = $("#cmbTPERSONA").val();
        obj["per_listaprecio"] = $("#cmbLISTAPRECIO").val();
        obj["per_politica"] = $("#cmbPOLITICA").val();
        obj["per_retiva"] = $("#cmbRETIVA").val();
        obj["per_retfuente"] = $("#cmbRETFUENTE").val();
        obj["per_agente"] = $("#txtAGENTE").val();
        obj["per_bloqueo"] = $("#txtBLOQUEO").val();
        obj["per_tarjeta"] = $("#txtTARJETA").val();
        obj["per_cupo"] = $("#txtCUPO").val();
        obj["per_ilimitado"] = $("#txtILIMITADO").val();
        obj["per_impuesto"] = $("#txtIMPUESTO").val();
        obj["chofer"] = GetChoferObj();
        obj["socio"] = GetSocioObj();
        
        obj = SetAuditoria(obj);

        var jsonText = JSON.stringify({ objeto: obj });
      
            return jsonText;




        }

        function GetPXTObj() {
            var objTipos = {};
            var objCliente = new Array();
            objCliente[0] = $("#txtCATCLIENTE").val();
            objCliente[1] = $("#txtPOLCLIENTE").val();
            var objProveedor = new Array();
            objProveedor[0] = $("#txtCATPROVEEDOR").val();
            objProveedor[1] = $("#txtPOLPROVEEDOR").val();
            var objChofer = new Array();
            objChofer[0] = $("#txtCATCHOFER").val();
            objChofer[1] = $("#txtPOLCHOFER").val();
            var objSocio = new Array();
            objSocio[0] = $("#txtCATSOCIO").val();
            objSocio[1] = $("#txtPOLSOCIO").val();

            objTipos["cliente"] = objCliente;
            objTipos["proveedor"] = objProveedor;
            objTipos["socio"] = objSocio;
            objTipos["chofer"] = objChofer;
            objTipos = SetAuditoria(objTipos);
            return objTipos;
        }


        function GetChoferObj() {
            var obj = {};
            obj["cho_persona"] = $("#txtCODIGO").val();
            obj["cho_persona_key"] = $("#txtCODIGO_key").val();
            obj["cho_empresa"] = parseInt(empresasigned["emp_codigo"]);
            obj["cho_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
            obj["cho_nrolicencia"] = $("#txtNROLICENCIA").val();
            obj["cho_puntoslicencia"] = $("#txtPUNTOSLICENCIA").val();
            obj["cho_tiposangre"] = $("#txtTIPOSANGRE").val();
            obj["cho_emisionlicencia"] = $("#cmbEMISIONLICENCIA").val();
            obj["cho_renovacion"] = $("#cmbRENOVACION").val();
            obj["cho_caducidadlicencia"] = $("#cmbCADUCIDADLICENCIA").val();
            obj["cho_tipolicencia"] = $("#txtTIPOLICENCIA").val();
            obj["cho_paisemision"] = $("#cmbPAISEMISION").val();
            obj["cho_provinciaemision"] = $("#cmbPROVINCIAEMISION").val();
            obj["cho_cantonemision"] = $("#cmbCANTONEMISION").val();
            obj["cho_paisrenovacion"] = $("#cmbPAISRENOVACION").val();
            obj["cho_provinciarenovacion"] = $("#cmbPROVINCIARENOVACION").val();
            obj["cho_cantonrenovacion"] = $("#cmbCANTONRENOVACION").val();
            obj["cho_observacionlicencia"] = $("#cmbOBSERVACIONLICENCIA").val();
            obj["cho_estado"] = GetCheckValue($("#chkESTADO"));
            obj = SetAuditoria(obj);
            return obj;
        }

        function GetSocioObj() {
            var obj = {};
            obj["soc_persona"] = $("#txtCODIGO").val();
            obj["soc_persona_key"] = $("#txtCODIGO_key").val();
            obj["soc_empresa"] = parseInt(empresasigned["emp_codigo"]);
            obj["soc_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
            obj["soc_foto"] = $("#txtCODIGO").val();
            obj["soc_fechanacimiento"] = $("#cmbFECHANACIMIENTO").val();
            obj["soc_paisnacimiento"] = $("#cmbPAISNACIMIENTO").val();
            obj["soc_provincianacimiento"] = $("#cmbPROVINCIANACIMIENTO").val();
            obj["soc_cantonnacimiento"] = $("#cmbCANTONNACIMIENTO").val();
            obj["soc_estadocivil"] = $("#cmbESTADOCIVIL").val();
            obj["soc_cargas"] = $("#txtCARGAS").val();
            obj["soc_inscripcion"] = $("#txtINSCRIPCION").val();
            obj["soc_fechainscripcion"] = $("#cmbFECHAINSCRIPCION").val();
            obj["soc_fechasalida"] = $("#cmbFECHASALIDA").val();
            obj["soc_razonsalida"] = $("#txtRAZONSALIDA").val();
            obj["soc_lugartrabajo"] = $("#txtLUGARTRABAJO").val();
            obj["soc_cargotrabajo"] = $("#cmbCARGOTRABAJO").val();
            obj["soc_departamento"] = $("#cmbDEPARTAMENTO").val();
            obj["soc_direcciontrabajo"] = $("#txtDIRECCIONTRABAJO").val();
            obj["soc_telefonotrabajo"] = $("#txtTELEFONOTRABAJO").val();
            obj["soc_faxtrabajo"] = $("#txtFAXTRABAJO").val();
            obj["soc_nroiess"] = $("#txtNROIESS").val();
            obj["soc_banco"] = $("#cmbBANCO").val();
            obj["soc_tipocuenta"] = $("#cmbTIPOCUENTA").val();
            obj["soc_nrocuenta"] = $("#txtNROCUENTA").val();
            obj["soc_nivelinstruccion"] = $("#cmbNIVELINSTRUCCION").val();
            obj["soc_postgrado"] = $("#txtPOSTGRADO").val();
            obj["soc_doctorado"] = $("#txtDOCTORADO").val();
            obj["soc_reconocimientos"] = $("#txtRECONOCIMIENTOS").val();
            obj["soc_titulos"] = $("#txtTITULOS").val();
            obj["soc_profesion"] = $("#cmbPROFESION").val();
            obj["soc_fechagrado"] = $("#cmbFECHAGRADO").val();
            obj["soc_institucion"] = $("#txtINSTITUCION").val();
            obj["soc_conadis"] = $("#txtCONADIS").val();
            obj["soc_empresaconyuge"] = parseInt(empresasigned["emp_codigo"]);
            obj["soc_codigoconyuge"] = $("#cmbCODIGOCONYUGE").val();
            obj["soc_estado"] = GetCheckValue($("#chkESTADO"));
            obj = SetAuditoria(obj);
            return obj;
        }





        function ValidateFormChofer() {
          
            var retorno = true;
            var controles = $("#tab4").find('[data-obligatorio="True"]');
            var mensajehtml = "";
            $("#tab4").find('[data-obligatorio="True"]').each(function () {
                $(this.parentNode).removeClass('obligatorio')
                $(this.parentNode).children(".obligatorio").remove();
                if ($(this).val() == "") {
                    retorno = false;
                    var padre = $(this.parentNode);
                    $(this.parentNode).append("<span class='obligatorio'>! Obligatorio</span>");
                    $(this.parentNode).addClass('obligatorio')
                    mensajehtml += "El campo <b>" + $(this).attr('placeholder') + "</b> es obligatorio <br>";
                }

            });
            if (!retorno) {
                jQuery.alerts.dialogClass = 'alert-danger';
                jAlert(mensajehtml, 'Error', function () {
                    jQuery.alerts.dialogClass = null; // reset to default
                });
            }
            return retorno;
        }

        function ValidateFormSocio() {
            var retorno = true;
            var controles = $("#tab3").find('[data-obligatorio="True"]');
            var mensajehtml = "";
            $("#tab3").find('[data-obligatorio="True"]').each(function () {
                $(this.parentNode).removeClass('obligatorio')
                $(this.parentNode).children(".obligatorio").remove();
                if ($(this).val() == "") {
                    retorno = false;
                    var padre = $(this.parentNode);
                    $(this.parentNode).append("<span class='obligatorio'>! Obligatorio</span>");
                    $(this.parentNode).addClass('obligatorio')
                    mensajehtml += "El campo <b>" + $(this).attr('placeholder') + "</b> es obligatorio <br>";
                }

            });
            if (!retorno) {
                jQuery.alerts.dialogClass = 'alert-danger';
                jAlert(mensajehtml, 'Error', function () {
                    jQuery.alerts.dialogClass = null; // reset to default
                });
            }
            return retorno;
        }


        function ValidateFormCliente() {
           
            var retorno = true;
            var controles = $("#tab2").find('[data-obligatorio="True"]');
            var mensajehtml = "";
            $("#tab2").find('[data-obligatorio="True"]').each(function () {
              
                $(this.parentNode).removeClass('obligatorio')
                $(this.parentNode).children(".obligatorio").remove();
                if ($(this).val() == "") {
                    retorno = false;
                    var padre = $(this.parentNode);
                    $(this.parentNode).append("<span class='obligatorio'>! Obligatorio</span>");
                    $(this.parentNode).addClass('obligatorio')
                    mensajehtml += "El campo <b>" + $(this).attr('placeholder') + "</b> es obligatorio <br>";
                }

            });
            if (!retorno) {
                jQuery.alerts.dialogClass = 'alert-danger';
                jAlert(mensajehtml, 'Error', function () {
                    jQuery.alerts.dialogClass = null; // reset to default
                });
            }
            return retorno;
        }

        function ValidateFormTab() {
            var flag = true;

            var tipos = $("#cmbTIPO").val();
           
            for (i = 0; i < tipos.length; i++) {
                var aux = parseInt(tipos[i]);
                switch (aux) {
                    case 4:
                        if (!ValidateFormCliente())
                            flag = false;
                        break;
                    case 5:
                        if (!ValidateFormCliente())
                            flag = false;
                        break;
                    case 6:
                        if (!ValidateFormSocio())
                            flag = false;
                        break;
                    case 7:
                        if (!ValidateFormChofer())
                            flag = false;
                        break;
                    case 8:
                        if (!ValidateFormChofer())
                            flag = false;
                        break;
                }
            }
            return flag;


        }


        function ClearValidate() {
            var controles = $("#busquedacontent").find('[data-obligatorio="True"]');
            $("#busquedacontent").find('[data-obligatorio="True"]').each(function () {
                $(this.parentNode).removeClass('obligatorio')
                $(this.parentNode).children(".obligatorio").remove();
            });
        }



        function AddObj() {
            ClearValidate();
            CleanForm();
            $("#comdetallecontent").css({ 'display': 'none' });
            $('#busquedacontent').show("fast");
            $("#adddet").css({ 'display': 'none' });
            $('#guardar').show("fast");
            $('#lista').show("fast");
            
           
             //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
        }
        function Volver() {
            ClearValidate();
            CleanForm();
            $("#busquedacontent").css({ 'display': 'none' });
            $('#comdetallecontent').show("fast");
            $("#guardar").css({ 'display': 'none' });
            $("#lista").css({ 'display': 'none' });
            $('#adddet').show("fast");
            


            //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
        }


        function Configurar() {

            window.location.href = "wfPersonaAutorizacion.aspx?codigo=" + $("#txtCODIGO").val();

        }



        function scroll() {
          
            if ($("#comdetallecontent")[0].scrollHeight - $("#comdetallecontent").scrollTop() <= $("#comdetallecontent").outerHeight()) {
                LoadDetalleData();
            }
        }

        function Scroll() {

            $("#comdetallecontent").on("scroll", scroll);
        }



       