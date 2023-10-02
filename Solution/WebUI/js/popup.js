//Archivo:          Popup.js
//Descripción:      Contiene funciones de llamadas a popups comunes
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Octubre  2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados


//Funciona que invoca al servidor mediante JSON
function CallServerPopup(strurl, strdata, retorno) {
    //ClearValidate();
    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (retorno == 0)
                CallPersonaResult(data);
            if (retorno == 1)
                SavePersonaResult(data);
            if (retorno == 2)
                CallSelectEmpresaResult(data);
            if (retorno == 3)
                CallComprobanteResult(data);
            if (retorno == 4)
                LoadPuntoVentaResult(data);
            if (retorno == 5)
                CallReciboResult(data);
            if (retorno == 6)
                CallAfectacionResult(data);
            if (retorno == 7)
                CallAfectacionGuiasResult(data);
            if (retorno == 8)
                CallConsultaDeudasResult(data);
            if (retorno == 9)
                CallPrevAfectacionResult(data);
            if (retorno == 10)
                CallBuscaComprobantesResult(data);
            if (retorno == 11)
                LoadPVentaBusquedaComprobanteResult(data);
            if (retorno == 12)
                LoadDataBusquedaComprobanteResult(data);
            if (retorno == 13)
                CallBuscaCancelacionResult(data);
            if (retorno == 14)
                LoadDataBusquedaCancelacionResult(data);
            if (retorno == 15)
                CallValoresSociosResult(data);
            if (retorno == 16)
                SaveValoresSociosResult(data);
            if (retorno == 20)
                CallElectronicoResult(data);
            if (retorno == "GETNUM")
                GetNumeroResult(data);
            if (retorno == "VALNUM")
                ValidaNumeroResult(data);

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


function CallPopUp(idpopup, titulo, htmlcontent) {
    KillPopUp(idpopup);
    var btnpopup = $('<div id="btn' + idpopup + '" href="#' + idpopup + '" data-toggle="modal">Llama</div>')
    var popup = $('<div aria-hidden="false" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" class="modal hide fade in" id="' + idpopup + '"><div class="modal-header"><button aria-hidden="true" data-dismiss="modal" class="close" type="button">&times;</button><h3 id="H1">' + titulo + '</h3></div><div id="form' + idpopup + '" class="modal-body" style ="width :80%;height:auto;">' + htmlcontent + '</div><div class="modal-footer"><button id="btnok' + idpopup + '" data-dismiss="modal" class="btn btn-primary btn-rounded">Aceptar</button><button id="btncancel' + idpopup + '" data-dismiss="modal" class="btn">Cancelar</button></div></div>');
    $("#area").append(btnpopup);
    $("#area").append(popup);

 

    //$("#btn" + idpopup).on("click", PopUpStart);
    $("#btn" + idpopup).trigger("click");
    $("#btnok" + idpopup).on("click", PopUpOk);
    $("#btncancel" + idpopup).on("click", PopUpCancel);

}


function CallPopUpSave(idpopup, titulo, htmlcontent) {
    
    KillPopUp2(idpopup);
    var divbg = $("<div class='bgpopup' id='bg" + idpopup + "' ></div>");
    var divcon = $("<div class='midpopup modal' id='con" + idpopup + "' ><div class='modal-header'><button aria-hidden='true' data-dismiss='modal' class='close' type='button' onclick=ClosePopUp2('" + idpopup + "')>&times;</button><h3 id='H1'>" + titulo + "</h3></div> <div class='detpopup row-fluid modal-body'>" + htmlcontent + "</div><div class='modal-footer'><div id='msg" + idpopup + "'></div><button id='btnok" + idpopup + "' class='btn btn-primary btn-rounded'>Aceptar</button><button id='btncancel" + idpopup + "' data-dismiss='modal' class='btn'>Cancelar</button></div>");
    //var ifra = $("<iframe src='wfCancelacion.aspx' name='framepop' id='framepop' ></iframe>")
    $('body').append(divbg);
    $('body').append(divcon);

    //$('body', window.parent.document).append(divbg);
    //$('body', window.parent.document).append(divcon);    
    $("#btnok" + idpopup).on("click", PopUpOk);
    $("#btncancel" + idpopup).on("click", PopUpCancel);



}

function CallPopUp2(idpopup, titulo, htmlcontent) {
    KillPopUp2(idpopup);  
    var divbg = $("<div class='bgpopup' id='bg" + idpopup + "' ></div>");
    var divcon = $("<div class='conpopup modal' id='con" + idpopup + "' ><div class='modal-header'><button aria-hidden='true' data-dismiss='modal' class='close' type='button' onclick=ClosePopUp2('"+idpopup+"')>&times;</button><h3 id='H1'>" + titulo + "</h3></div> <div class='detpopup row-fluid modal-body'>" + htmlcontent + "</div><div class='modal-footer'><div id='msg"+idpopup+"'></div><button id='btnok" + idpopup + "' data-dismiss='modal' class='btn btn-primary btn-rounded'>Aceptar</button><button id='btncancel" + idpopup + "' data-dismiss='modal' class='btn'>Cancelar</button></div>");
    //var ifra = $("<iframe src='wfCancelacion.aspx' name='framepop' id='framepop' ></iframe>")
    $('body').append(divbg);
    $('body').append(divcon);
   
    //$('body', window.parent.document).append(divbg);
    //$('body', window.parent.document).append(divcon);    
    $("#btnok" + idpopup).on("click", PopUpOk);
    $("#btncancel" + idpopup).on("click", PopUpCancel);

}

function CallPopUp3(idpopup, titulo, htmlcontent) {
    KillPopUp2(idpopup);
    var divbg = $("<div class='bgpopup' id='bg" + idpopup + "' ></div>");
    var divcon = $("<div class='conpopup modal' id='con" + idpopup + "' ><div class='modal-header'><button aria-hidden='true' data-dismiss='modal' class='close' type='button' onclick=ClosePopUp2('" + idpopup + "')>&times;</button><h3 id='H1'>" + titulo + "</h3></div> <div class='detpopup row-fluid modal-body'>" + htmlcontent + "</div><div class='modal-footer'><div id='msg" + idpopup + "'></div><button id='btnok" + idpopup + "' onclick=ClosePopUp2('" + idpopup + "') data-dismiss='modal' class='btn btn-primary btn-rounded'>Aceptar</button></div>");
    //var ifra = $("<iframe src='wfCancelacion.aspx' name='framepop' id='framepop' ></iframe>")
    $('body').append(divbg);
    $('body').append(divcon);

}

function CallPopUp4(idpopup, titulo, htmlcontent) {
    KillPopUp(idpopup);
    var btnpopup = $('<div id="btn' + idpopup + '" href="#' + idpopup + '" data-toggle="modal">Llama</div>')
    var popup = $('<div aria-hidden="false" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" class="modal hide fade in" id="' + idpopup + '"><div class="modal-header"><button aria-hidden="true" data-dismiss="modal" class="close" type="button">&times;</button><h3 id="H1">' + titulo + '</h3></div><div id="form' + idpopup + '" class="modal-body" style ="width :80%;height:auto;">' + htmlcontent + '</div><div class="modal-footer"><button id="btnok' + idpopup + '" data-dismiss="modal" class="btn btn-primary btn-rounded">Aceptar</button></div></div>');
    $("#area").append(btnpopup);
    $("#area").append(popup);
    //$("#btn" + idpopup).on("click", PopUpStart);
    $("#btn" + idpopup).trigger("click");    

}


function CallPopUpBC(idpopup, titulo, htmlcontent) {
    KillPopUp2(idpopup);
    var divbg = $("<div class='bgpopup' id='bg" + idpopup + "' onclick=ClosePopUp2('" + idpopup + "')></div>");
    var divcon = $("<div class='conpopup modal' id='con" + idpopup + "' ><div class='modal-header'><button aria-hidden='true' data-dismiss='modal' class='close' type='button' onclick=ClosePopUp2('" + idpopup + "')>&times;</button><h3 id='H1'>" + titulo + "</h3></div> <div class='detpopup row-fluid modal-body'>" + htmlcontent + "</div><div class='modal-footer'><div id='msg" + idpopup + "'></div><button id='btnaddall" + idpopup + "' data-dismiss='modal' class='btn btn-primary btn-rounded'>Agregar Todos</button><button id='btnaddsel" + idpopup + "' data-dismiss='modal' class='btn btn-primary btn-rounded'>Agregar Seleccion</button><button id='btncancel" + idpopup + "' data-dismiss='modal' class='btn'>Cerrar</button></div>");
    //var ifra = $("<iframe src='wfCancelacion.aspx' name='framepop' id='framepop' ></iframe>")
    $('body').append(divbg);
    $('body').append(divcon);

    //$('body', window.parent.document).append(divbg);
    //$('body', window.parent.document).append(divcon);    
    $("#btnaddsel" + idpopup).on("click", AddSelectedBC);
    $("#btnaddall" + idpopup).on("click", AddAllBC);
    $("#btncancel" + idpopup).on("click", PopUpCancel);

}

function CallPopUpBCan(idpopup, titulo, htmlcontent) {
    KillPopUp2(idpopup);
    var divbg = $("<div class='bgpopup' id='bg" + idpopup + "' onclick=ClosePopUp2('" + idpopup + "')></div>");
    var divcon = $("<div class='conpopup modal' id='con" + idpopup + "' ><div class='modal-header'><button aria-hidden='true' data-dismiss='modal' class='close' type='button' onclick=ClosePopUp2('" + idpopup + "')>&times;</button><h3 id='H1'>" + titulo + "</h3></div> <div class='detpopup row-fluid modal-body'>" + htmlcontent + "</div><div class='modal-footer'><div id='msg" + idpopup + "'></div><button id='btnaddall" + idpopup + "' data-dismiss='modal' class='btn btn-primary btn-rounded'>Agregar Todos</button><button id='btnaddsel" + idpopup + "' data-dismiss='modal' class='btn btn-primary btn-rounded'>Agregar Seleccion</button><button id='btncancel" + idpopup + "' data-dismiss='modal' class='btn'>Cerrar</button></div>");
    //var ifra = $("<iframe src='wfCancelacion.aspx' name='framepop' id='framepop' ></iframe>")
    $('body').append(divbg);
    $('body').append(divcon);

    //$('body', window.parent.document).append(divbg);
    //$('body', window.parent.document).append(divcon);    
    $("#btnaddsel" + idpopup).on("click", AddSelectedBCan);
    $("#btnaddall" + idpopup).on("click", AddAllBCan);
    $("#btncancel" + idpopup).on("click", PopUpCancel);

}


function ClosePopUp2(idpopup) {
    $("#con" + idpopup).hide();
    $("#bg" + idpopup).hide();
}




function KillPopUp2(idpopup) {
    var bgpopup = $("#bg" + idpopup);
    var popup = $("#con" + idpopup);
    $(bgpopup).remove();
    $(popup).remove();
}

function KillPopUp(idpopup) {
    var btnpopup = $("#btn" + idpopup);
    var popup = $("#" + idpopup);
    $(btnpopup).remove();
    $(popup).remove();
}



function PopUpOk() {
    var id = $(this)[0].id.replace("btnok", "");
    if (id == "modPersona") {
        SavePersona();     
    }
    if (id == "modEmpresa") {
        SelectEmpresa();
    }
    if (id == "modComprobante") {
        CallComprobanteOK();
    }
    if (id == "modDatosUsuario") {
        CallAdUusarioOK()
        ClosePopUp2(id);
    }
    if (id == "modDatosUsuarioUsr") {
        CallAdUusarioUsrOK()
        ClosePopUp2(id);
    }
    if (id == "modPersonaAutorizacion") {
        CallPersonaAutorizacionOK()
        ClosePopUp2(id);
    }
    if (id == "modDetalleCuenta") {
        CallDetalleCuentaOK()
        ClosePopUp(id);
    }
    if (id == "modDetalleCuentaSave") {
        CallCuentaSaveOK()
        ClosePopUp2(id);
    }
    if (id == "modDespachar") {
       
        CallDespacharOk();
            
       // KillPopUp(id);
    }
    if (id == "modAsignarSocio") {

        CallAsignarSocioOk();

        // KillPopUp(id);
    }
    if (id == "modAsignarFacturaPlanilla") {

        CallAsignarFacturaPlanillaOk();

        // KillPopUp(id);
    }
    if (id == "modModificar") {
       
        CallModificarOk();
            
       // KillPopUp(id);
    }
    if (id == "modModificarDatos") {

        CallModificarDatosOk();

        // KillPopUp(id);
    }
    if (id == "modModificarPago") {

        CallModificarPagoOk();

        // KillPopUp(id);
    }

    if (id == "modInformacion") {

        CallInformacionOk();

        // KillPopUp(id);
    }
    if (id == "modAnular") {

        CallAnularOk();

        // KillPopUp(id);
    }
    if (id == "modActivar") {

        CallActivarOk();

        // KillPopUp(id);
    }
    if (id == "modRecibo") {
        ClosePopUp2(id); 
        CallReciboOK();
    }
    if (id == "modAfectacion") {        
        if (CallAfectacionOK())
            ClosePopUp2(id);
    }
    if (id == "modPrevAfectacion") {
        ClosePopUp2(id);
        CallPrevAfectacionOK();
            
    }
    if (id == "modGuias") {
        ClosePopUp2(id);
        CallGuiasOK();
    }

    if (id == "modEnvios") {
        ClosePopUp2(id);
        CallEnviosOK();
    }
    if (id == "modEnviosNum") {
        ClosePopUp2(id);
        CallEnviosNumOK();
    }
    if (id == "modCancelaciones") {
        ClosePopUp2(id);
        CallCancelacionesOK();
    }
    if (id == "modAfectacionGuias") {
        ClosePopUp2(id);
        CallAfectacionGuiasOK();
    }
    if (id == "modHojas") {
        ClosePopUp2(id);
        CallHojasOK();
    }
    if (id == "modDeudas") {
        ClosePopUp2(id);
    }
    if (id == "modCalculo") {
        if (CallCalculoPrecioOK())
            ClosePopUp2(id);
    }
    if (id == "modDListaPrecio") {
        if (CallDListaPrecioOK())
            ClosePopUp2(id);
    }
    
    if (id == "modValoresSocios") {
        if (CallValoresSociosOK())
            ClosePopUp2(id);
    }
    //if (id == "modDatosUsuario") {
    //    //AQUI VA CUANDO OK 
    //}
    
    
}

function PopUpCancel() {
    var id = $(this)[0].id.replace("btncancel", "");
    if (id == "modRecibo" || id == "modAfectacion" || id == "modGuias" || id == "modDatosUsuario" || id == "modDatosUsuarioUsr" || id == "modBuscaComprobante" || id == "modBuscaCancelacion" || id == "modDetalleCuenta" || id == "modDetalleCuentaSave" || id == "modPersona") {
        ClosePopUp2(id);        
    }    
}


//POPUP CALL SELECT EMPRESA

function CallSelectEmpresa(idusuario) {
    var obj = idusuario;    
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup("ws/Metodos.asmx/SelectEmpresa", jsonText, 2);
}

function CallSelectEmpresaResult(data) {
    if (data != "") {
      CallPopUp("modEmpresa", "Empresas", data.d);        
    }
}

function SelectEmpresa() {
    try {
        var obj = {};
        obj["emp_codigo"] = parseInt($("#cmbempresa").val());
        obj["emp_nombre"] = $("#cmbempresa option:selected").text();
        GetCallSelectEmpresa(obj);
    }
    catch (err) {
        alert("Función GetCallSelectEmpresa no definida " + err.Message);
    }



}

/////////////////////////

//POPUP CALL COMPROBANTE

function CallComprobante(empresa, usuario, tipodoc) {
    var obj = {};
    obj["empresa"] = empresa;
    obj["usuario"] = usuario;
    obj["tipodoc"] = tipodoc;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup("ws/Metodos.asmx/CrearComprobante", jsonText, 3);
}

function CallComprobanteResult(data) {
    if (data != "") {
        CallPopUp("modComprobante", "Comprobante", data.d);
        SetFormComprbante(); 
    }
}

function SetFormComprbante() {
    $(".fecha").datepicker({
        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha    
    $("#cmbALMACEN_P").on("change", LoadPuntoVenta);  //Opción "Cerrar" del combo de opciones de la sección de edición    
    LoadPuntoVenta();
        //$("#cmbALMACEN_P").trigger("change");    
}

function LoadPuntoVenta() {
    var obj = {};
    //obj["pais"] = $("#cmbPAIS").val();
    if ($("#cmbPVENTA_P").length > 0) {
        obj["id"] = "cmbPVENTA_P";
        obj["empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["almacen"] = $("#cmbALMACEN_P").val();
        obj["usuario"] = usuariosigned["usr_id"];
        var jsonText = JSON.stringify({ objeto: obj });
        CallServerPopup("ws/Metodos.asmx/GetPuntoVenta", jsonText, 4);
    }
    if ($("#cmbBODEGA_P").length > 0) {
        obj["id"] = "cmbBODEGA_P";
        obj["empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["almacen"] = $("#cmbALMACEN_P").val();
        obj["usuario"] = usuariosigned["usr_id"];
        var jsonText = JSON.stringify({ objeto: obj });
        CallServerPopup("ws/Metodos.asmx/GetBodega", jsonText, 4);
    }
}

function LoadPuntoVentaResult(data) {
    if (data != "") {
        if ($("#cmbPVENTA_P").length > 0) {
            $("#cmbPVENTA_P").replaceWith(data.d);
        }
        if ($("#cmbBODEGA_P").length > 0) {
            $("#cmbBODEGA_P").replaceWith(data.d);
        }
    }
    GetNumero();
}

function GetNumero() {
    var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHA_P").val());
    var obj = {};
    obj["com_empresa"] = empresasigned["emp_codigo"]; //Obtener de variable global
    //obj["com_codigo"] = objcomp["comprobante"];       
    obj["com_periodo"] = currentDate.getFullYear();
    obj["com_fecha"] = currentDate;    
    obj["com_tipodoc"] = $("#txtTIPODOC_P").val();
    obj["com_ctipocom"] = $("#cmbSIGLA_P").val();
    obj["com_almacen"] = $("#cmbALMACEN_P").val();
    if ($("#cmbPVENTA_P").length > 0)
        obj["com_pventa"] = $("#cmbPVENTA_P").val();
    if ($("#cmbBODEGA_P").length > 0) 
        obj["com_bodega"] = $("#cmbBODEGA_P").val();
    obj = SetAuditoria(obj);    
    if ($("#txtNUMERO_P").length > 0) {              
        var jsonText = JSON.stringify({ objeto: obj });
        CallServerPopup("ws/Metodos.asmx/GetNumeroComprobante", jsonText, "GETNUM");
    }
}

function GetNumeroResult(data) {
    if (data != "") {
        if ($("#txtNUMERO_P").length > 0) {
            $("#txtNUMERO_P").val(data.d);
        }
    }
}


function ValidaNumero() {
    var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHA_P").val());
    var obj = {};
    obj["com_empresa"] = empresasigned["emp_codigo"]; //Obtener de variable global    
    obj["com_periodo"] = currentDate.getFullYear();
    obj["com_fecha"] = currentDate;
    obj["com_tipodoc"] = $("#txtTIPODOC_P").val();
    obj["com_ctipocom"] = $("#cmbSIGLA_P").val();
    obj["com_almacen"] = $("#cmbALMACEN_P").val();
    if ($("#cmbPVENTA_P").length > 0)
        obj["com_pventa"] = $("#cmbPVENTA_P").val();
    if ($("#cmbBODEGA_P").length > 0)
        obj["com_bodega"] = $("#cmbBODEGA_P").val();    
    //obj = SetAuditoria(obj);
    if ($("#txtNUMERO_P").length > 0) {
        obj["com_numero"] = $("#txtNUMERO_P").val();
        var jsonText = JSON.stringify({ objeto: obj });
        CallServerPopup("ws/Metodos.asmx/ValidaNumeroComprobante", jsonText, "VALNUM");
    }
    else
        CallComprobanteOK(true);
}

function ValidaNumeroResult(data) {
    if (data.d == "ok") {
        CallComprobanteOK(true);
    }
    else {
        alert(data.d);
    }
}



function CallComprobanteOK(valida) {

    if (!valida)
        ValidaNumero();
    else {


        try {
            var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHA_P").val());

            var obj = {};
            //obj["fecha"] = $("#txtFECHA_P").datepicker("getDate") ;        
            obj["fecha"] = new Date(currentDate.getFullYear() + "/" + (currentDate.getMonth() + 1) + "/" + currentDate.getDate() + " " + $("#txtHORA_P").val());
            //obj["fecha"] = new Date(currentDate.getFullYear(), currentDate.getMonth() + 1, currentDate.getDate()+  );
            obj["almacen"] = $("#cmbALMACEN_P").val();
            obj["almacennombre"] = $("#cmbALMACEN_P option:selected").text();
            if ($("#cmbPVENTA_P").length > 0) {
                obj["pventa"] = $("#cmbPVENTA_P").val();
                obj["pventanombre"] = $("#cmbPVENTA_P option:selected").text();
                if (obj["pventa"] == "" && obj["pventa"] == null || obj["pventa"] == undefined) {
                    alert("Debe seleccionar un punto de venta");
                    return;
                }
            }
            if ($("#cmbBODEGA_P").length > 0) {
                obj["bodega"] = $("#cmbBODEGA_P").val();
                obj["bodeganombre"] = $("#cmbBODEGA_P option:selected").text();
            }
            if ($("#txtNUMERO_P").length > 0) {
                obj["numero"] = $("#txtNUMERO_P").val();
            }
            obj["ctipocom"] = $("#cmbSIGLA_P").val() //parseInt($("#txtCTIPOCOM_P").val());
            obj["tipodoc"] = $("#txtTIPODOC_P").val() //parseInt($("#txtCTIPOCOM_P").val());
            SetComprobante(obj);
        }
        catch (err) {
            alert("Función SetComprobante no definida");
        }

    }

}


//////////////////////


//POPUP CALL PERSONA

function CallPersona(id,tipo ) {
    var obj = {};
    obj["id"] = id;
    obj["tclipro"] = tipo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup("ws/Metodos.asmx/AddPersona", jsonText, 0);
}

function CallPersonaResult(data) {
    if (data != "") {
        CallPopUpSave("modPersona", "Agregar Cliente", data.d);
        SetFormPersona(); 
    }
}


function SetFormPersona() {
    $("#txtCIRUC_P").select();
    SetAutocompleteById("txtCIRUC_P");
    //$("#txtCIRUC_P").keyup("change", avilible_save);
   
}

//function avilible_save() {
//    if (Valida_CIRUC($("#txtCIRUC_P").val())) {
//    //    $("#btnok" + idpopup).on("click", PopUpOk);
//        $("#btnokmodPersona").prop("disabled", false);
//    }
//    else {
//        $("#btnokmodPersona").prop("disabled", true);
//     }
//}

   

function GetPersonaObject() {
    var obj = {};
    var persona = {};
    var pxt = {};
    persona["per_codigo"] = $("#txtCODIGO_P").val();
    //obj["per_codigo_key"] = $("#txtCODIGO_key").val();
    persona["per_empresa"] = parseInt(empresasigned["emp_codigo"]);
    //obj["per_empresa_key"] = $("#txtEMPRESA_key").val();
    persona["per_ciruc"] = $("#txtCIRUC_P").val();
    persona["per_id"] = $("#txtID_P").val();
    persona["per_tipoid"] = $("#cmbTIPOID_P").val();
    persona["per_nombres"] = $("#txtNOMBRES_P").val();
    persona["per_apellidos"] = $("#txtAPELLIDOS_P").val();
    persona["per_razon"] = $("#txtRAZON_P").val();
    persona["per_direccion"] = $("#txtDIRECCION_P").val();
    persona["per_telefono"] = $("#txtTELEFONO_P").val();
    persona["per_mail"] = $("#txtMAIL_P").val();
    pxt["pxt_tipo"] = $("#txtTCLIPRO").val();
    persona = SetAuditoria(persona);
    obj["persona"] = persona;
    obj["pxt"] = pxt;
    obj["almacen"] = $("#txtCODALMACEN").val();
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function CleanPersonaObject() {
    $("#txtCODIGO_P").val("");        
    $("#txtCIRUC_P").val("");
    $("#txtID_P").val("");
    $("#cmbTIPOID_P").val("");
    $("#txtNOMBRES_P").val("");
    $("#txtAPELLIDOS_P").val("");
    $("#txtRAZON_P").val("");
    $("#txtDIRECCION_P").val("");
    $("#txtTELEFONO_P").val("");
    $("#txtMAIL_P").val("");

    $("#txtNOMBRES_P").prop("disabled", false);
    $("#txtAPELLIDOS_P").prop("disabled", false);
    $("#txtRAZON_P").prop("disabled", false);
    $("#txtDIRECCION_P").prop("disabled", false);
    $("#txtTELEFONO_P").prop("disabled", false);
    $("#txtMAIL_P").prop("disabled", false);
    $("#cmbTIPOID_P").prop("disabled", false);
    
}
function SavePersona() {
    var personaobj = GetPersonaObject();
    CallServerPopup("ws/Metodos.asmx/SavePersona", personaobj, 1);
}

function SavePersonaResult(data) {
    if (data != "") {
        if (data.d.toString().indexOf("ERROR") >= 0) { //!= "ERROR") {
            alert(data.d);
        }
        else
        {
            ClosePopUp2("modPersona");
            try {
                var id = $("#txtIDCONTROL_P").val();
                var obj = $.parseJSON(data.d);
                GetCallPersona(obj,id);
            }
            catch (err) {
                alert("Persona agregada exitosamente... Función SetNewPersona no definida");
            }
                
        }
        //else {            
        //    jQuery.alerts.dialogClass = 'alert-danger';
        //    jAlert("No se pudo agregar la persona...", 'Error', function () {
        //        jQuery.alerts.dialogClass = null; // reset to default
        //    });
        //}
    }
}

///////////////////////////////

//POPUP CALL RECIBO

function CallRecibo(empresa, usuario, total,politica) {
    var obj = {};
    obj["empresa"] = empresa;
    obj["usuario"] = usuario;
    obj["total"] = total;
    obj["politica"] = politica;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup("ws/Metodos.asmx/CrearRecibo", jsonText, 5);
}

function CallReciboResult(data) {
    if (data != "") {
        //CallPopUp("modRecibo", "Pago", data.d);
        CallPopUp2("modRecibo", "Cancelación", data.d);
        SetFormRecibo();
    }
}

//POPUT CALL DESPACHAR



function SetFormRecibo() {
        
    SetAutocompleteById("txtIDTIPO_P");
    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha
    //$("#cmbALMACEN_P").on("change", LoadPuntoVenta);  //Opción "Cerrar" del combo de opciones de la sección de edición    
    //$("#cmbALMACEN_P").trigger("change");
}


function GetDetalleRecibo() {
    var detalle = new Array();
    var htmltable = $("#tdpago_P")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var obj = GetDreciboObj(htmltable.rows[r]);
        if (obj != null)
            detalle[r] = obj;
    }
    return detalle;
}

function GetDreciboObj(row) {
    var obj = {};
    obj["dfp_empresa"] = parseInt(empresasigned["emp_codigo"]);
    //obj["dfp_comprobante"] = $("#txtcodigocomp").val();

    var hijocodpro = $(row.cells[0]).children("input");
    if (hijocodpro.length > 0) {
        if ($.isNumeric($("#txtCODTIPO_P").val())) {
            obj["dfp_tipopago"] = parseInt($("#txtCODTIPO_P").val());
            obj["dfp_tipopagoid"] = $("#txtIDTIPO_P").val();
            obj["dfp_tipopagonombre"] = $("#txtNOMBRETIPO_P").val();
            obj["dfp_monto"] = parseFloat($("#txtVALOR_P").val());
            obj["dfp_nro_documento"] = $("#txtNRODOCUMENTO_P").val();
            obj["dfp_nro_cuenta"] = $("#txtNROCUENTA_P").val();
            obj["dfp_emisor"] = $("#txtEMISOR_P").val();
            obj["dfp_debcre"] = 1; // DEBITO CREDITO??
            //obj["dfp_tarjeta"] = ;
            obj["dfp_banco"] = parseInt($("#txtNROCHEQUE_P").val());
            obj["dfp_nro_cheque"] = $("#txtNROCHEQUE_P").val();
            obj["dfp_beneficiario"] = $("#txtBENEFICIARIO_P").val();

            var venceDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHAVENCE_P").val());

            obj["dfp_fecha_ven"] = venceDate;
            //obj["dfp_cuenta"] = 
            //obj["dfp_ref_comprobante"] = ;

        }        
    }
    else {

        obj["dfp_tipopago"] = parseInt($(row).data("codtipo").toString());
        obj["dfp_tipopagoid"] = $(row.cells[0]).text();
        obj["dfp_tipopagonombre"] = $(row.cells[1]).text();
        obj["dfp_monto"] = parseFloat($(row.cells[2]).text());

        obj["dfp_nro_documento"] = $(row).data("nrodocumento").toString();
        obj["dfp_nro_cuenta"] = $(row).data("nrocuenta").toString();
        obj["dfp_emisor"] = $(row).data("emisor").toString();
        obj["dfp_debcre"] = 1; // DEBITO CREDITO??
        //obj["dfp_tarjeta"] = ;
        obj["dfp_banco"] = parseInt($(row).data("banco").toString());
        obj["dfp_nro_cheque"] = $(row).data("nrocheque").toString();
        obj["dfp_beneficiario"] = $(row).data("beneficiario").toString();

        var venceDate = $.datepicker.parseDate("dd/mm/yy", $(row).data("fecha").toString());

        obj["dfp_fecha_ven"] = venceDate;
        //obj["dfp_cuenta"] = 
        //obj["dfp_ref_comprobante"] = ;
    }


    return obj;
}

function ValidaRecibo() {    
    var mensaje = "";
    var totalpago = parseFloat($("#txtTOTAL_P").val());
    var totalrec = parseFloat($("#txtTOTALCOM_P").val());
    //if (totalrec < totalpago) {
    //    mensaje += "El valor de cancelación no puede ser menor al total del comprobante<br>";        
    //}

    if (mensaje != "") {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert(mensaje, 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
        return false;
    }
    else
        return true;

}

function CallReciboOK() {
    try {

        if (ValidaRecibo()) {
            var obj = GetDetalleRecibo();
            SetRecibo(obj);
        }
    }
    catch (err) {
        alert("Función SetPago no definida");
    }



}


///////////////////////////////

//POPUP CALL PREV AFECTACION
function CallPrevAfectacion(obj) {
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup("ws/Metodos.asmx/GetPrevAfectaciones", jsonText, 9);
}

function CallPrevAfectacionResult(data) {
    if (data != "") {
        //CallPopUp("modRecibo", "Pago", data.d);
        CallPopUp2("modPrevAfectacion", "Datos Cancelación", data.d);
        SetFormPrevAfectacion();
    }
}
function SetFormPrevAfectacion() {
    SetAutocompleteByIdTipo("txtIDPER_PR","DIARIO");
    SetAutocompleteByIdTipo("txtIDCUE_PR","DIARIO");
}

function CallPrevAfectacionOK() {
    try {

        var objtot = {};        
        objtot["tot_empresa"] = parseInt(empresasigned["emp_codigo"]);
        objtot["tot_total"] = $("#txtVALOR_PR").val().replace(".",","); 
        
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codclipro"] = $("#txtCODPER_PR").val();
        obj["com_tclipro"] = $("#txtTIPO_PR").val();
        obj["total"] = objtot; 

            
            
        SetPrevAfectacion(obj);        
    }
    catch (err) {
        alert("Función SetPrevAfectacion no definida");
    }
}

///////////////////////////////

//POPUP CALL AFECTACION

function CallAfectacion(obj) {
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup("ws/Metodos.asmx/GetAfectaciones", jsonText, 6);
}
function CallAfectacionFromFAC(compcan, compref) {
    var obj = {};
    obj["compcan"] = compcan;
    obj["compref"] = compref;

    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup("ws/Metodos.asmx/GetAfectacionesFromFAC", jsonText, 6);
}
function CallAfectacionFromLGC(compcan, compref) {
    var obj = {};
    obj["compcan"] = compcan;
    obj["compref"] = compref;

    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup("ws/Metodos.asmx/GetAfectacionesFromLGC", jsonText, 6);
}
function CallAfectacionResult(data) {
    if (data != "") {
        //CallPopUp("modRecibo", "Pago", data.d);
        CallPopUp2("modAfectacion", "Deudas", data.d);
        SetFormAfectacion();
    }
}

function SetFormAfectacion() {
    SetAutocompleteById("txtCODCLIPRO_P");
    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha
    //FitScrollableTables();
    CalculaAfectacion();
}

function CalculaAfectacion() {
    var htmltable = $("#tdafec_P")[0];
    var total = 0;


    for (var r = 1; r < htmltable.rows.length; r++) {
        var c = htmltable.rows[r].cells.length - 1; //celda cantidad
        //var valor = $(htmltable.rows[r].cells[c]).data("valor");

        var hijoval = $(htmltable.rows[r].cells[c]).children("input");
        var valor = 0;

        if (hijoval.length > 0) {
            valor = $(hijoval).val();
        }
        total += ($.isNumeric(valor)) ? parseFloat(valor) : 0;

    }

    var saldo = parseFloat($("#txtVALOR_P").val()) - total;
    $("#txtSALDO_P").val(CurrencyFormatted(saldo));
    $("#tdafec_P_f8").html(CurrencyFormatted(total));



}


function GetDetalleAfectacion() {
    var detalle = new Array();
    var htmltable = $("#tdafec_P")[0];
    for (var r = 1; r < htmltable.rows.length-1; r++) {
        var obj = GetDcancelacionObj(htmltable.rows[r]);
        if (obj != null)
            detalle[r] = obj;
    }
    return detalle;
}

function GetDcancelacionObj(row) {
    try {
        var obj = {};

        obj["dca_empresa"] = empresasigned["emp_codigo"];
        obj["dca_comprobante"] = $(row).data("comprobante").toString();
        obj["dca_transacc"] = $(row).data("transac").toString();
        obj["dca_doctran"] = CleanString($(row).data("doctran").toString());
        obj["dca_pago"] = $(row).data("pago").toString();
        obj["dca_transacc_can"] = 7;




        /*obj["dca_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["dca_comprobante"] = parseInt($(row).data("comprobante").toString());
        obj["dca_transacc"] = parseInt($(row).data("transac").toString());
        obj["dca_doctran"] = CleanString($(row.cells[0]).text());
        obj["dca_pago"] = parseInt($(row.cells[2]).text());
        obj["dca_transacc_can"] = 7;*/

        //obj["dca_comprobante_can"] = parseFloat(row.cells[2].innerText);    
        //obj["dca_secuencia"] = $(row).data("nrodocumento").toString();

        obj["dca_debcre"] = parseInt($("#txtDEBCRE_P").val()); //parseInt(row.cells[1].innerText);

        //obj["dca_transacc_can"] = parseInt(row.cells[1].innerText);

        obj["dca_monto"] = parseFloat($(row.cells[10]).children("input").val());
        obj["dca_monto_ext"] = parseFloat($(row.cells[10]).children("input").val());
        //if ($(row).data("subtotal")!= 'undefined');
        if ($(row).get(0).hasAttribute("data-subtotal"))
            obj["tot_subtotal"] = parseFloat($(row).data("subtotal").toString());
        return obj;
    }
    catch (err) {
        return null
    }
}

function ValidaAfectacion() {
    var mensaje = "";
    var saldo = parseFloat($("#txtSALDO_P").val());
    var obliga = $("#txtOBLIGA_P").val()
    if (saldo < 0) {
        mensaje += "La suma de afectaciones supera el valor del comprobante de recibo <br />";
    }
    //else if (saldo > 0) {
    //    if (obliga =="S")
    //        mensaje += "La suma de afectaciones es menor al valor del comprobante de recibo, debe distribuir el valor total del recibo <br />";
    //}

    var htmltable = $("#tdafec_P")[0];
    for (var r = 1; r < htmltable.rows.length - 1; r++) {
        var saldo = parseFloat($(htmltable.rows[r].cells[9]).text());
        var monto = parseFloat($(htmltable.rows[r].cells[10]).children("input").val());
        if (monto > saldo)
            mensaje += "No se puede cancelar un monto mayor al saldo (" + monto + "/" + saldo + ") <br />";
    }

    if (mensaje != "") {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert(mensaje, 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });

        return false;
    }
    else
        return true;

}

function CallAfectacionOK() {
    try {

        if (ValidaAfectacion()) {
            var obj = GetDetalleAfectacion();
            SetAfectacion(obj);
            return true;
        }
        else
            return false;
    }
    catch (err) {
        alert("Función SetAfectacion no definida");
    }



}


function CleanValores() {

    var detalle = new Array();
    var htmltable = $("#tdafec_P")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var row = htmltable.rows[r];
        var calccell = 0;
        if (!$(row.cells[10]).children("input").is(':disabled'))
            $(row.cells[10]).children("input").val(CurrencyFormatted(calccell));
        $(row.cells[10]).data("valor", calccell);
    }
    CalculaAfectacion();
}


function CalcPorcentaje() {

    var porcentaje = ($.isNumeric($("#txtDESCPORCENTAJE_P").val())) ? parseFloat($("#txtDESCPORCENTAJE_P").val()) : 0;
    var columna = $("#cmbDESCPORCENTAJE_P").val();

    if ($.isNumeric(porcentaje)) {

        var detalle = new Array();
        var htmltable = $("#tdafec_P")[0];
        for (var r = 1; r < htmltable.rows.length; r++) {
            var row = htmltable.rows[r];
            var cell = 7; //Columna Monto
            if (columna == "0")//Subtotal0
                cell = 4
            if (columna == "1")//Subtotal IVA
                cell = 5
            if (columna == "2")//IVA
                cell = 6
            if (columna == "3")//TOTAL o MONTO
                cell = 7

            //var valorcell = row.cells[cell].innerText;
            var valorcell = $(row.cells[cell]).data("valor");

            var calccell = ($.isNumeric(valorcell) ? parseFloat(valorcell) : 0) * (parseFloat(porcentaje) / 100);

            var totcell = ($.isNumeric($(row.cells[10]).children("input").val()) ? parseFloat($(row.cells[10]).children("input").val()) : 0) + calccell;



            //if (!$(row.cells[10]).children("input").is(':disabled'))
            $(row.cells[10]).children("input").val(CurrencyFormatted(totcell));
            $(row.cells[10]).data("valor",totcell);
        }
    }
    CalculaAfectacion();

}
function CalcValor() {


    var valordes = ($.isNumeric($("#txtDESCVALOR_DS").val())) ? parseFloat($("#txtDESCVALOR_DS").val()) : 0; ;

    var detalle = new Array();
    var htmltable = $("#td_DS")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var row = htmltable.rows[r];
        if (!$(row.cells[10]).children("input").is(':disabled'))
            $(row.cells[10]).children("input").val(CurrencyFormatted(valordes));
    }
}







///////////////////////////////

//POPUP CALL AFECTACION GUIAS

function CallAfectacionGuias(obj) {
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup("ws/Metodos.asmx/GetAfectacionesGuias", jsonText, 7);
}

function CallAfectacionGuiasResult(data) {
    if (data != "") {
        //CallPopUp("modRecibo", "Pago", data.d);
        CallPopUp2("modAfectacionGuias", "Guias Documento", data.d);
        SetFormAfectacionGuias();
    }
}

function SetFormAfectacionGuias() {

    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha
    FitScrollableTables();
    
}


function GetDetalleAfectacionGuias() {
    var detalle = new Array();
    var htmltable = $("#tdafec_P")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var obj = GetDcancelacionObj(htmltable.rows[r]);
        if (obj != null)
            detalle[r] = obj;
    }
    return detalle;
}

function GetDcancelacionGuiasObj(row) {
    var obj = {};
    obj["dca_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["dca_comprobante"] = parseInt($(row).data("comprobante").toString());
    obj["dca_transacc"] = parseInt($(row).data("transac").toString());
    obj["dca_doctran"] = $(row.cells[0]).text();
    obj["dca_pago"] = parseInt($(row.cells[1]).text());
    //obj["dca_comprobante_can"] = parseFloat(row.cells[2].innerText);    
    //obj["dca_secuencia"] = $(row).data("nrodocumento").toString();
    obj["dca_debcre"] = parseInt($(row.cells[1]).text());
    obj["dca_transacc_can"] = parseInt($(row.cells[1]).text());
    obj["dca_monto"] = parseFloat($(row.cells[8]).children("input").val());

    return obj;
}

function ValidaAfectacionGuias() {
    var mensaje = "";
    var saldo = parseFloat($("#txtSALDO_P").val());
    if (saldo < 0) {
        mensaje += "La suma de afectaciones supera el valor del comprobante de recibo";
    }
    /*var totalpago = parseFloat($("#txtTOTAL_P").val());
    var totalrec = parseFloat($("#txtTOTALCOM_P").val());
    if (totalrec < totalpago) {
    mensaje += "El valor de cancelación no puede ser menor al total del comprobante<br>";
    }*/

    if (mensaje != "") {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert(mensaje, 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });

        return false;
    }
    else
        return true;

}

function CallAfectacionGuiasOK() {
    try {

        if (ValidaAfectacion()) {
            var obj = GetDetalleAfectacion();
            SetAfectacionGuias(obj);
        }
        else
            return false;
    }
    catch (err) {
        alert("Función SetAfectacion no definida");
    }



}

///////////////////////////////////////////////////////////
//POP UP CONSULTA DEUDAS/////////////////////////////////////
function CallConsultaDeudas(obj) {
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup("ws/Metodos.asmx/GetDeudas", jsonText, 8);
}

function CallConsultaDeudasResult(data) {
    if (data != "") {
        //CallPopUp("modRecibo", "Pago", data.d);
        CallPopUp2("modDeudas", "Deudas", data.d);        
    }
}


///////////////////////////////////////////////////////////


///////////////POPUP CALL VALORES SOCIOS
function CallValoresSocios(obj) {
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup("ws/Metodos.asmx/GetValoresSocios", jsonText, 15);
}

function CallValoresSociosResult(data) {
    if (data != "") {
        //CallPopUp("modRecibo", "Pago", data.d);
        CallPopUp2("modValoresSocios", "Valores Socios", data.d);
        $("#td_DS").find("input").on("change", TotalesValores);
        TotalesValores();
        //SetFormPrevAfectacion();
    }
}
/*function SetFormPrevAfectacion() {
    SetAutocompleteByIdTipo("txtIDPER_PR", "DIARIO");
    SetAutocompleteByIdTipo("txtIDCUE_PR", "DIARIO");
}*/

function CallValoresSociosOK() {
    try {

        var obj = GetDetalleValores();
        var jsonText = JSON.stringify({ objeto: obj });
        CallServerPopup("ws/Metodos.asmx/SaveValoresSocios", jsonText, 16);
    }
    catch (err) {
        alert(err);
    }
}




function SaveValoresSociosResult(data) {
    if (data != "") {
        if (data.d == "OK") {
            ClosePopUp2("modValoresSocios");
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert("No se pudo guardar los valores de socios...", 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}

function GetDetalleValores() {
    var detalle = new Array();
    var htmltable = $("#td_DS")[0];
    for (var r = 1; r < htmltable.rows.length-1; r++) {
        var obj = GetValoresObj(htmltable.rows[r]);
        if (obj != null) {
           // if (obj["dca_monto_pla"] > obj["dca_monto"])
           //     throw "No se puede asignar un valor (" + obj["dca_monto_pla"] + ") mayor al cancelado (" + obj["dca_monto"]+")";
           // else
                detalle[r] = obj;
        }
    }
    return detalle;
}

function GetValoresObj(row) {
    var obj = {};
    obj["dca_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["dca_comprobante"] = $(row).data("comprobante").toString();
    obj["dca_transacc"] = $(row).data("transac").toString();
    obj["dca_doctran"] = $(row.cells[0]).text();
    obj["dca_pago"] = parseInt($(row.cells[2]).text());
    obj["dca_comprobante_can"] = $(row).data("comprobantecan").toString();
    obj["dca_secuencia"] = $(row).data("secuencia").toString();
    obj["dca_monto"] = parseFloat($(row.cells[6]).text()); ;
    //obj["dca_debcre"] = parseInt(row.cells[1].innerText);
    //obj["dca_transacc_can"] = parseInt(row.cells[1].innerText);
    obj["dca_monto_pla"] = parseFloat($(row.cells[8]).children("input").val()); 
    return obj;
}


function CalcularValores() {
 
    var detalle = new Array();
    var htmltable = $("#td_DS")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var row = htmltable.rows[r];
        var valor = parseFloat($(row.cells[7]).text());
        var porcentajedes = ($.isNumeric($(row.cells[9]).children("input").val())) ? parseFloat($(row.cells[9]).children("input").val()) : 0;
        var valordes = ($.isNumeric($(row.cells[10]).children("input").val())) ? parseFloat($(row.cells[10]).children("input").val()) : 0;
        var valorpla = valor - (valor * (porcentajedes / 100)) - valordes;
        if (!$(row.cells[8]).children("input").is(':disabled'))
            $(row.cells[8]).children("input").val(CurrencyFormatted(valorpla));
    }
    TotalesValores();
}
function AddPorcentaje() {

    var porcentaje = ($.isNumeric($("#txtDESCPORCENTAJE_DS").val())) ? parseFloat($("#txtDESCPORCENTAJE_DS").val()) : 0; ;


    var detalle = new Array();
    var htmltable = $("#td_DS")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var row = htmltable.rows[r];
        if (!$(row.cells[9]).children("input").is(':disabled'))
            $(row.cells[9]).children("input").val(CurrencyFormatted(porcentaje));
    }

}
function AddValor() {


    var valordes = ($.isNumeric($("#txtDESCVALOR_DS").val())) ? parseFloat($("#txtDESCVALOR_DS").val()) : 0; ;

    var detalle = new Array();
    var htmltable = $("#td_DS")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var row = htmltable.rows[r];
        if (!$(row.cells[10]).children("input").is(':disabled'))
            $(row.cells[10]).children("input").val(CurrencyFormatted(valordes));
    }
}


function TotalesValores() {
    var htmltable = $("#td_DS")[0];
    var total = 0;
    var totalpla = 0;
    var totaldec = 0;
    var totalval = 0;

    for (var r = 1; r < htmltable.rows.length-1; r++) {
        var row = htmltable.rows[r];
        var valor = parseFloat($(row.cells[7]).text());
        var valorpla  = ($.isNumeric($(row.cells[8]).children("input").val())) ? parseFloat($(row.cells[8]).children("input").val()) : 0;
        var porcentajedes = ($.isNumeric($(row.cells[9]).children("input").val())) ? parseFloat($(row.cells[9]).children("input").val()) : 0;
        var valordes = ($.isNumeric($(row.cells[10]).children("input").val())) ? parseFloat($(row.cells[10]).children("input").val()) : 0;

        total += valor;
        totalpla += valorpla;
        totaldec += porcentajedes;
        totalval += valordes;

    }

    $("#td_DS_f7").html(CurrencyFormatted(total));
    $("#td_DS_f8").html(CurrencyFormatted(totalpla));
    $("#td_DS_f9").html(CurrencyFormatted(totaldec));
    $("#td_DS_f10").html(CurrencyFormatted(totalval));
}

///////////////////////////////



//POP UP HOJAS DE RUTA/////////////////////////////////////

function CallHojasOK() {
    try {

       // if (ValidaAfectacion()) {
            
            var obj = $("#cmbHOJARUTA_P").val();
            SetHojaRutaDestino(obj);
        //}
        //else
          //  return false;
    }
    catch (err) {
        alert("Función SetHojaRutaDestino no definida");
    }

}



/////////////////////////

//POPUP BUSQUEDA COMPROBANTES

function CallBuscaComprobantes(comprobante) {
    var jsonText = JSON.stringify({ objeto: comprobante });
    CallServerPopup("ws/Metodos.asmx/BuscarComprobantes", jsonText, 10);
}

function CallBuscaComprobantesResult(data) {
    if (data != "") {
        CallPopUpBC("modBuscaComprobante", "Busqueda Comprobantes", data.d);
        SetFormBusquedaComprbante();
    }
}

function SetFormBusquedaComprbante() {
    $("#alldet_P").on("click", { tabla: "tddatos_P" }, SelectAllRows);
    $("#nonedet_P").on("click", { tabla: "tddatos_P" }, CleanSelectedRows);
    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha
    $("#cmbALMACEN_B").on("change", LoadPVentaBusquedaComprobante);  //Opción "Cerrar" del combo de opciones de la sección de edición    
    $("#cmbALMACEN_B").trigger("change");

//    $("#txtFECHA_B").on("change", LoadDataBusquedaComprobante);
    $("#txtNUMERO_B").on("change", LoadDataBusquedaComprobante);
//    $("#cmbPVENTA_B").on("change", LoadDataBusquedaComprobante);
//    $("#txtNUMERO_B").on("change", LoadDataBusquedaComprobante);    

}

function LoadPVentaBusquedaComprobante() {
    var obj = {};
    //obj["pais"] = $("#cmbPAIS").val();
    obj["id"] = "cmbPVENTA_B";
    obj["empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["almacen"] = $("#cmbALMACEN_B").val();
    obj["usuario"] = usuariosigned["usr_id"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup("ws/Metodos.asmx/GetPuntoVentaEmpty", jsonText, 11);
}

function LoadPVentaBusquedaComprobanteResult(data) {
    if (data != "") {
        $("#cmbPVENTA_B").replaceWith(data.d);
        //$("#cmbPVENTA_B").on("change", LoadDataBusquedaComprobante);
        //$("#cmbPVENTA_B").trigger("change");
    }
}

var isloadingBC = false;

function LoadDataBusquedaComprobante() {
    if (!isloadingBC) {
     
        
    
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["rutafactura"] = GetDetalle();
        obj["planillas"] = GetDetalle();
        obj["com_almacen"] = $("#cmbALMACEN_B").val();
        obj["com_pventa"] = $("#cmbPVENTA_B").val();        
        obj["com_fecha"] = $("#txtFECHA_B").val();
        obj["com_numero"] = parseInt($("#txtNUMERO_B").val());
        obj["com_tipodoc"] = $("#txtTIPODOC_B").val();
        obj["com_ruta"] = parseInt($("#cmbRUTA_B").val());
        obj["nombres"] = $("#txtCLIENTE_B").val();
        obj["socio"] = $("#txtSOCIO_B").val();
        obj["tipo"] = $("#cmbTIPODOC_B").val();
        obj["hasta"] = $("#txtFECHAH_B").val();
       

        var jsonText = JSON.stringify({ objeto: obj });
        isloadingBC = true;
        //ShowLoading();
        CallServerPopup("ws/Metodos.asmx/BuscarComprobantesData", jsonText, 12);
    }
}

function LoadDataBusquedaComprobanteResult(data) {
    if (data != "") {
        $('#detallegui').html(data.d);
        var tabla = $("#detallegui").find("table");
        var filas = 0;
        if (tabla.length > 0)
            filas = $(tabla)[0].rows.length - 1;
        $('#msg_B').html(filas +" Registros encontrados...");
    }
    isloadingBC = false;
}

function AddSelectedBC() {
    var objs = GetDetalleBC(true);
    SetEnvios(objs);
    LoadDataBusquedaComprobante();
}

function GetDetalleBC(selected) {
    var detalle = new Array();
    var htmltable = $("#tddatos_P")[0];
    var contador = 0;
    var add = true;
    for (var r = 1; r < htmltable.rows.length; r++) {
        if (selected)
        {
            if ($(htmltable.rows[r]).css("background-color") == rgbcolor)
                add = true;
            else
                add = false;
        }
        if (add) {
            var obj = GetComprobanteBC(htmltable.rows[r]);
            if (obj != null) {
                detalle[contador] = obj;
                contador++;
            }
        }
    }
    return detalle;
}

function GetComprobanteBC(row) {
    var obj = {};
    obj["comprobante"] = parseInt($(row).data("comprobante").toString());
    obj["doctran"] = $(row.cells[0]).html();
    obj["remitente"] = $(row.cells[1]).text();
    obj["destinatario"] = $(row.cells[2]).text();
    obj["valor"] = parseFloat($(row.cells[3]).text());

    if (row.cells.length > 4) {
        //PARA PLANILLA DE CLIENTE
        obj["fecha"] = $(row.cells[0]).text();
        obj["socio"] = $(row.cells[1]).text();
        obj["doctran"] = $(row.cells[2]).text();
        obj["subtotal0"] = parseFloat($(row.cells[3]).text());
        obj["subtotaliva"] = parseFloat($(row.cells[4]).text());
        obj["iva"] = parseFloat($(row.cells[5]).text());
        obj["seguro"] = parseFloat($(row.cells[6]).text());
        obj["transporte"] = parseFloat($(row.cells[7]).text());
        obj["total"] = parseFloat($(row.cells[8]).text());
    }
    return obj;
}

function AddAllBC() {
    var objs = GetDetalleBC(false);
    SetEnvios(objs);
    LoadDataBusquedaComprobante();
}


//////////////////////



/////////////////////////

//POPUP BUSQUEDA CANCELACIONES

function CallBuscaCancelacion(comprobante) {
    var jsonText = JSON.stringify({ objeto: comprobante });
    CallServerPopup("ws/Metodos.asmx/BuscarCancelaciones", jsonText, 13);
}

function CallBuscaCancelacionResult(data) {
    if (data != "") {
        CallPopUpBCan("modBuscaCancelacion", "Busqueda Cancelaciones", data.d);
        SetFormBusquedaCancelacion();
    }
}

function SetFormBusquedaCancelacion() {
    $("#alldet_P").on("click", { tabla: "tddatos_P" }, SelectAllRows);
    $("#nonedet_P").on("click", { tabla: "tddatos_P" }, CleanSelectedRows);
    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha
    $("#cmbALMACEN_B").on("change", LoadPVentaBusquedaComprobante);  //Opción "Cerrar" del combo de opciones de la sección de edición    
    $("#cmbALMACEN_B").trigger("change");

    //    $("#txtFECHA_B").on("change", LoadDataBusquedaComprobante);
    $("#txtNUMERO_B").on("change", LoadDataBusquedaCancelacion);
    //    $("#cmbPVENTA_B").on("change", LoadDataBusquedaComprobante);
    //    $("#txtNUMERO_B").on("change", LoadDataBusquedaComprobante);    

}




function LoadDataBusquedaCancelacion() {
    if (!isloadingBC) {



        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["cancelaciones"] = GetDetalle();
        obj["com_almacen"] = $("#cmbALMACEN_B").val();
        obj["com_pventa"] = $("#cmbPVENTA_B").val();
        obj["com_numero"] = parseInt($("#txtNUMERO_B").val());
        obj["com_codclipro"] = parseInt($("#txtCODPER").val());
        

        var jsonText = JSON.stringify({ objeto: obj });
        isloadingBC = true;
        //ShowLoading();
        CallServerPopup("ws/Metodos.asmx/BuscarCancelacionesData", jsonText, 14);
    }
}

function LoadDataBusquedaCancelacionResult(data) {
    if (data != "") {
        $('#detallegui').html(data.d);
        var tabla = $("#detallegui").find("table");
        var filas = 0;
        if (tabla.length > 0)
            filas = $(tabla)[0].rows.length - 1;
        $('#msg_B').html(filas + " Registros encontrados...");
    }
    isloadingBC = false;
}

function AddSelectedBCan() {
    var objs = GetDetalleBCan(true);
    SetCancelaciones(objs);
    LoadDataBusquedaCancelacion();
}

function GetDetalleBCan(selected) {
    var detalle = new Array();
    var htmltable = $("#tddatos_P")[0];
    var contador = 0;
    var add = true;
    for (var r = 1; r < htmltable.rows.length; r++) {
        if (selected) {
            if ($(htmltable.rows[r]).css("background-color") == rgbcolor)
                add = true;
            else
                add = false;
        }
        if (add) {
            var obj = GetComprobanteBCan(htmltable.rows[r]);
            if (obj != null) {
                detalle[contador] = obj;
                contador++;
            }
        }
    }
    return detalle;
}

function GetComprobanteBCan(row) {
    var obj = {};
    obj["dca_comprobante"] = parseInt($(row).data("comprobante").toString());   
    obj["dca_empresa"] = parseInt(empresasigned["emp_codigo"]);        
    obj["dca_transacc"] = parseInt($(row).data("dca_transacc").toString());    
    obj["dca_doctran"] = $(row).data("dca_doctran").toString();   
    obj["dca_pago"] = parseInt($(row).data("dca_pago").toString());    
    obj["dca_comprobante_can"] = parseInt($(row).data("dca_comprobante_can").toString());    
    obj["dca_secuencia"] = parseInt($(row).data("dca_secuencia").toString());
    obj["fecha"] = $(row.cells[0]).text();
    obj["factura"] = $(row.cells[1]).text();
    obj["guia"] = $(row.cells[2]).text();
    obj["cancelacion"] = $(row.cells[3]).text();
    obj["monto"] = $(row.cells[4]).text();
    obj["cancelado"] = $(row.cells[5]).text();
    obj["saldo"] = parseFloat($(row.cells[6]).text());

    return obj;
}

function AddAllBCan() {
    var objs = GetDetalleBCan(false);
    SetCancelaciones(objs);
    LoadDataBusquedaCancelacion();
}


//////////////////////


//////////////////////////// CALL ELECTRONICO///////////////////////////
//Funcion que obtiene datos del comprobante electronico/////////////////


function CallElectronico(codigo) {
    var obj = {};
    obj["com_empresa"] = empresasigned["emp_codigo"];
    obj["com_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup("ws/Metodos.asmx/GetElectronicoData", jsonText, 20);
}


function CallElectronicoResult(data)
{
    if (data!=null)
    {
        var obj = $.parseJSON(data.d);

        $("#txtESTADOELEC").val(obj[0]);
        $("#txtMENSAJEELEC").val(obj[1]);
    }
}