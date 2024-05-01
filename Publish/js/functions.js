//Archivo:          Functions.js
//Descripción:      Contiene Funciones generales 
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Agosto  2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

//Funciona que invoca al servidor mediante JSON
function CallServerFunction(strurl, strdata, retorno) {


    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (retorno == 0)
                LoadProvinviciaResult(data);
            if (retorno == 1)
                LoadCantonResult(data);
            if (retorno == 2)
                LoadCabeceraComprobanteResult(data);
            if (retorno == 3)
                ViewComprobanteResult(data);
            if (retorno == 4)
                CallDespacharResult(data);
            if (retorno == 5)
                SaveObjResultDespachar(data);
            if (retorno == 6)
                CallModificarResult(data);
            if (retorno == 7)
                SaveObjResultModificar(data);
            if (retorno == 8)
                CallAuditoriaResult(data);
            if (retorno == 9)
                CallAnularResult(data);
            if (retorno == 10)
                SaveObjResultAnular(data);
            if (retorno == 11)
                CallModificarPagoResult(data);
            if (retorno == 12)
                SaveObjResultModificarPago(data);
            if (retorno == 13)
                CallActivarResult(data);
            if (retorno == 14)
                SaveObjResultActivar(data);
            if (retorno == 15)
               CallMayorizarResult(data);
            if (retorno == 16)
                CallModificarDatosResult(data);
            if (retorno == 17)
                SaveObjResultModificarDatos(data);
            if (retorno == 18)
                CallAsignarSocioResult(data);
            if (retorno == 19)
                SaveObjResultAsignarSocio(data);
            if (retorno == 20)                
                CallAsignarFacturaPlanillaResult(data);
            if (retorno == 21)
                SaveObjResultAsignarFacturaPlanilla(data);
            if (retorno == 22)
                CallInformacionResult(data);
            if (retorno == 23)
                CallRecontabilizarResult(data);
            if (retorno == 24)
                DelFacPlaResult(data);
            if (retorno == 25)
                RefFacPlaResult(data);
            if (retorno == 26)
                CallAfectaDeudasResult(data);
            if (retorno == 30)
                ElectronicRideResult(data);

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


function SetPaisProvinciaCanton(idselectpais, idselectprovincia, idselectcanton) {


    $("#" + idselectpais).data("select", idselectprovincia);
    $("#" + idselectprovincia).data("select", idselectcanton);
    $("#" + idselectpais).on("change", {id:idselectpais}, LoadProvinvicia);
    $("#" + idselectpais).trigger("change");
      
}

function LoadProvinvicia(event) {
    var id = event.data.id;
    var data = $("#" + id).data("select");
    var obj = {};
    //obj["pais"] = $("#cmbPAIS").val();
    obj["id"] = data.toString();
    obj["pais"] = $("#"+id).val();
    obj["provincia"] = "";
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerFunction("ws/Metodos.asmx/GetProvincia", jsonText, 0);
}
function LoadProvinviciaResult(data) {
    if (data != "") {
        var select = $(data.d);
        var id = $(select)[0].id;
        var idselectcanton = $("#" + id).data("select");
        var valor = $("#" + id).val();

        $("#" + id).replaceWith(data.d);
        $("#" + id).on("change", {id: idselectcanton, idprovincia: id}, LoadCanton);

       if ($("#" + id +" option").length == 0)
            $("#" + id).attr("disabled", true);
        else
            $("#" + id).attr("disabled", false);
        $("#" + id).data("select", idselectcanton);

        $("#" + id).trigger("change");

    }
}
function LoadCanton(event) {
    var id = event.data.id;
    var idprovincia = event.data.idprovincia;
    var data = $("#" + id).data("select");
    var obj = {};
    obj["id"] = id;
    obj["provincia"] = $("#"+ idprovincia).val();
    obj["canton"] = "";
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerFunction("ws/Metodos.asmx/GetCanton", jsonText, 1);
}
function LoadCantonResult(data) {
    if (data != "") {

        var select = $(data.d);
        var id = $(select)[0].id;

        $("#"+id).replaceWith(data.d);
        if ($("#"+ id+" option").length == 0)
            $("#"+id).attr("disabled", true);
        else
            $("#"+id).attr("disabled", false);

    }
}





//////////////FUNCIONES PARA CABECERA DE COMPROBANTE////////////////////////////

function GetCabeceraComprobante(codigocomp, tipodoc) {
    if (codigocomp >= 0)
    {
        var obj = {};
        obj["comprobante"] = codigocomp;
        obj["tipodoc"] = tipodoc;        
        LoadCabeceraComprobante(obj);
    }
    else {
        var empresa = parseInt(empresasigned["emp_codigo"]);
        var idusuario = usuariosigned["usr_id"];// "admin";
        CallComprobante(empresa, idusuario, tipodoc);//FUNCION DEFINIDA EN POPUP.JS
    }
}

//FUNCION NECESARIA PARA OBTENER EL OBJETO RESULTADO DEL POPUP DE COMPROBANTE
function SetComprobante(obj) {
    LoadCabeceraComprobante(obj);
}



function LoadCabeceraComprobante(objcomp) {
    var obj = {};
    obj["com_empresa"] = empresasigned["emp_codigo"]; //Obtener de variable global
    obj["com_codigo"] = objcomp["comprobante"];
    if (objcomp["fecha"] != undefined) {
        var currentDate = objcomp["fecha"];
        obj["com_periodo"] = currentDate.getFullYear();
        obj["com_fecha"] = currentDate;
    }

    obj["com_tipodoc"] = objcomp["tipodoc"]; 
    obj["com_ctipocom"] = objcomp["ctipocom"];  
    obj["com_almacen"] = objcomp["almacen"];
    obj["com_pventa"] = objcomp["pventa"];
    obj["com_bodega"] = objcomp["bodega"];
    obj["com_numero"] = objcomp["numero"];
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerFunction("ws/Metodos.asmx/GetCabeceraComprobante", jsonText, 2);
}

function LoadCabeceraComprobanteResult(data)
{
     if (data != "") {
         try {
             GetCabeceraComprobanteResult(data.d);
         }
         catch (err) {
             alert("Función GetCabeceraComprobanteResult no definida");
         }
           
    }
 }


 function FitScrollableTables() {
     $(".scrolltable").each(function () {

         //var rowh = $(this).find("thead > tr")
         //var rowb = $(this).find("tbody > tr");
         //var rowf = $(this).find("tfoot > tr")




         /*$(this).fixedHeaderTable({
                        height: '400',
	                   footer: true,
	                    altClass: 'odd',
                  });*/


         /*$(this).find("tbody > tr").eq(0).each(function () {
             $(this).children("td").each(function () {
                 var ancho = $(this).width() - 12;
                 var indice = $(this).index();
                 var th = $(rowh).children("th").eq(indice);
                 $(th).css("width", ancho + "px");
                 //$(th).width(ancho);
                 var tf = $(rowf).children("th").eq(indice);
                 //$(tf).width(ancho);
                 $(tf).css("width", ancho + "px");

                 // var tb = $(rowb).children("td").eq(indice);
                 // $(tb).css("width", ancho + "px");


             }); // next td
         });*/
     });

 }


 ////////////////////VIEW COMPROBANTE ////////////////////////////

 function ViewComprobante(codigo) {
     var obj = {};
     obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
     obj["com_codigo"] = codigo;
     var jsonText = JSON.stringify({ objeto: obj });
     CallServerFunction("ws/Metodos.asmx/ViewComprobante", jsonText, 3)
     StopPropagation();
 }

 function ViewComprobanteResult(data) {
     var obj = $.parseJSON(data.d);
     CallPopUp3("modViewComprobante", obj[0], obj[1]);

 }

 ////////////////////////////////////////////////////////////////

 /////////////////////IMPRESION//////////////////////////////////
 /////////////////////wfPrint////////////////////////////////////


 //function Print(titulo, parametros, reporte) {
 function Print(event) {

    var titulo = event.data.titulo;
    var parametros = event.data.parametros;
    var reporte = event.data.reporte;
     var opciones = "toolbar=no, scrollbars=no, resizable=yes, top=50, left=100, width=800, height=600";     
     if (titulo =="")
         titulo = "_blank";
     var url = event.data.url;
     //var url = "wfPrint.aspx";
     if (parametros != "")
         url += "?" + parametros; 
    window.open(url, titulo, opciones);
}




//////////////////////////////////////////////////////////////////////
//////OBTIENE LOS VALORES DEL QUERY STRING///////////////////////////

function GetQueryStringParams(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}


/////////////////VENTANA DE DESPACHO/////////////////////////////
// function despachar
function CallDespachar(codigo) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerFunction("ws/Metodos.asmx/CrearDespachar", jsonText, 4);
    //CallDespacharResult


}

function CallDespacharResult(data) {
    if (data != "") {      
        CallPopUp("modDespachar", "Despachar", data.d);
        $("#btnokmodDespachar").prop("disabled", true);
        SetFormDespachar();       
    }
}

function SetFormDespachar() {
     $("#txtNOMBRESRED").select();
     SetAutocompleteById("txtNOMBRESRED");     
     $("#txtNOMBRESRED").keyup("change", verificar);
     $("#txtFECHARED").keyup("change", verificar);
     $("#txtHORARED").keyup("change", verificar);
     $(".fecha").datepicker({
         dateFormat: "dd/mm/yy"
     }); //Setea campos de tipo fecha
 }


 function ValidateDespachar() {
    var retorno = true;
    var controles = $("#modDespachar").find('[data-obligatorio="True"]');
    var mensajehtml = "";
    $("#modDespachar").find('[data-obligatorio="True"]').each(function () {
        $(this.parentNode).removeClass('obligatorio')
        $(this.parentNode).children(".obligatorio").remove();
        if ($(this).val() == "") {
            retorno = false;
            var padre = $(this.parentNode);
            $(this.parentNode).append("<span class='obligatorio'>! </span>");
            $(this.parentNode).addClass('obligatorio')           
           // mensajehtml += "El campo <b>" + $(this).attr('placeholder') + "</b> es obligatorio <br>";
        }
    });
    return retorno;
}

function verificar() {    
    if (ValidateDespachar()) {    
        $("#btnokmodDespachar").prop("disabled", false);
    }
    else {
        $("#btnokmodDespachar").prop("disabled", true);
     }
}

function CallDespacharOk() {

    var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHARED").val());
    var comprobante = $("#txtCODIGOCOMPRED").val();
    var hora = $("#txtHORARED").val()
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = comprobante;
    obj["cenv_empresa_ret"] = parseInt(empresasigned["emp_codigo"]);
    obj["cenv_retira"] = parseInt($("#txtIDRED").val());
    obj["cenv_ciruc_ret"] = $("#txtCIRUCRED").val();
    obj["cenv_nombres_ret"] = $("#txtNOMBRESRED").val();
    obj["cenv_direccion_ret"] = $("#txtDIRECCIONRED").val();
    obj["cenv_telefono_ret"] = $("#txtTELEFONORED").val();
    obj["cenv_fecha_ret"] = currentDate.getDate() + "/" + (currentDate.getMonth() + 1) + "/" + currentDate.getFullYear() + " " + hora
    obj["cenv_observaciones_ret"] = $("#txtOBSERVACIONRED").val();

    var jsonText = JSON.stringify({ objeto: obj });
    CallServerFunction("ws/Metodos.asmx/ActualizarDespachar", jsonText, 5);
}
 

function SaveObjResultDespachar(data) {
    jQuery.alerts.dialogClass = 'alert-success';
    jAlert('El comprobante se  despachado correctamente', 'correcto', function () {
        try {
            location.reload();
            EndDespachar();
        }
        catch (err) {
        }
        jQuery.alerts.dialogClass = null; // reset to default
    });
}


/////////////////////////////////FIN DESPACHO//////////////////////////////



/////////////////VENTANA DE ASIGNACION FACTURA PLANILLA/////////////////////////////

function CallAsignarFacturaPlanilla(codigo) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerFunction("ws/Metodos.asmx/AsignarFacturaPlanilla", jsonText, 20);
    //CallDespacharResult


}

function CallAsignarFacturaPlanillaResult(data) {
    if (data != "") {
        CallPopUp("modAsignarFacturaPlanilla", "Asingar Factura Planilla", data.d);
        SetFormAsignarFacturaPlanilla();
    }
}

function SetFormAsignarFacturaPlanilla() {
    
}




function CallAsignarFacturaPlanillaOk() {
    var permite = $("#txtPERMITE_PF").val();
     if (permite.toUpperCase() == "TRUE") {

         var obj = {};
         obj["pco_empresa"] = empresasigned["emp_codigo"];
         obj["pco_comprobante_pla"] = $("#txtCODIGOPLA_PF").val();
         obj["doctranfac"] = $("#txtDOCTRANFAC_PF").val();
         obj = SetAuditoria(obj);
         var jsonText = JSON.stringify({ objeto: obj });
         CallServerFunction("ws/Metodos.asmx/SaveAsignarFacturaPlanilla", jsonText, 21);
     }
     else {
         jQuery.alerts.dialogClass = 'alert-danger';
         jAlert('La planilla ya posee una factura asignada', 'Error', function () {
             jQuery.alerts.dialogClass = null; // reset to default
         });
     }
}


function SaveObjResultAsignarFacturaPlanilla(data) {
    jQuery.alerts.dialogClass = 'alert-success';
    jAlert('El comprobante se  asigno correctamente a la planilla', 'correcto', function () {
        try {
            EndAsignarFacturaPlanilla();
        }
        catch (err) {
        }
        jQuery.alerts.dialogClass = null; // reset to default
    });
}


function DelFacPla(pla,fac) {
    if (confirm("¿Está seguro que desea eliminar la relación factura planilla?")) {
        var obj = {};
        obj["pco_empresa"] = empresasigned["emp_codigo"];
        obj["pco_comprobante_pla"] = $("#txtCODIGOPLA_PF").val();
        obj["pco_comprobante_fac"] = $("#txtCODIGOFAC_PF").val();
        var jsonText = JSON.stringify({ objeto: obj });
        CallServerFunction("ws/Metodos.asmx/DeleteAsignarFacturaPlanilla", jsonText, 24);
    }
}

function DelFacPlaResult(data)
{
    if (data.d == "OK") {
        jQuery.alerts.dialogClass = 'alert-success';
        jAlert('La relación factura planilla se eliminó correctemente', 'correcto', function () {
            try {
                EndAsignarFacturaPlanilla();
            }
            catch (err) {
            }
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
    else
        alert(data.d);

}


function RefFacPla(pla, fac) {
    if (confirm("¿Está seguro que desea reconstruir la factura de la planilla?")) {
        var obj = {};
        obj["pco_empresa"] = empresasigned["emp_codigo"];
        obj["pco_comprobante_pla"] = $("#txtCODIGOPLA_PF").val();
        obj["pco_comprobante_fac"] = $("#txtCODIGOFAC_PF").val();
        var jsonText = JSON.stringify({ objeto: obj });
        CallServerFunction("ws/Metodos.asmx/RecreateFacturaPlanilla", jsonText, 25);
    }
}

function RefFacPlaResult(data) {
    if (data.d == "OK") {
        jQuery.alerts.dialogClass = 'alert-success';
        jAlert('La factura ha sido reconstruida correctemente', 'correcto', function () {
            try {
                EndAsignarFacturaPlanilla();
            }
            catch (err) {
            }
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
    else
        alert(data.d);

}



/////////////////////////////////FIN ASIGNAR PLANILLA//////////////////////////////


/////////////////VENTANA DE ASIGNACION SOCIO/////////////////////////////
// function despachar
function CallAsignarSocio(codigo) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerFunction("ws/Metodos.asmx/AsingarSocio", jsonText, 18);
    //CallDespacharResult


}

function CallAsignarSocioResult(data) {
    if (data != "") {
        CallPopUp("modAsignarSocio", "Asignar Socio", data.d);
        //$("#btnokmodDespachar").prop("disabled", true);
        //SetFormDespachar();
    }
}

//function SetFormDespachar() {
//    $("#txtNOMBRESRED").select();
//    SetAutocompleteById("txtNOMBRESRED");
//    $("#txtNOMBRESRED").keyup("change", verificar);
//    $("#txtFECHARED").keyup("change", verificar);
//    $("#txtHORARED").keyup("change", verificar);
//    $(".fecha").datepicker({
//        dateFormat: "dd/mm/yy"
//    }); //Setea campos de tipo fecha
//}


//function ValidateDespachar() {
//    var retorno = true;
//    var controles = $("#modDespachar").find('[data-obligatorio="True"]');
//    var mensajehtml = "";
//    $("#modDespachar").find('[data-obligatorio="True"]').each(function () {
//        $(this.parentNode).removeClass('obligatorio')
//        $(this.parentNode).children(".obligatorio").remove();
//        if ($(this).val() == "") {
//            retorno = false;
//            var padre = $(this.parentNode);
//            $(this.parentNode).append("<span class='obligatorio'>! </span>");
//            $(this.parentNode).addClass('obligatorio')
//            // mensajehtml += "El campo <b>" + $(this).attr('placeholder') + "</b> es obligatorio <br>";
//        }
//    });
//    return retorno;
//}

//function verificar() {
//    if (ValidateDespachar()) {
//        $("#btnokmodDespachar").prop("disabled", false);
//    }
//    else {
//        $("#btnokmodDespachar").prop("disabled", true);
//    }
//}

function CallAsignarSocioOk() {

    var comprobante = $("#txtCODIGOCOMPASO").val();
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = comprobante;
    obj["cenv_socio"] = $("#cmbSOCIOASO").val();
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerFunction("ws/Metodos.asmx/ActualizarAsignarSocio", jsonText, 19);
}


function SaveObjResultAsignarSocio(data) {
    jQuery.alerts.dialogClass = 'alert-success';
    jAlert('El socio se asignó  correctamente', 'correcto', function () {
        try {
            EndAsignarSocio();
        }
        catch (err) {
        }
        jQuery.alerts.dialogClass = null; // reset to default
    });
}


/////////////////////////////////FIN DESPACHO//////////////////////////////


/////////////////VENTANA DE MODIFICACION DE GUIAS/////////////////////////////
// function despachar
function CallModificar(codigo) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = codigo;
    obj["com_concepto"] = $("#txtOBSERVACIONMOD").val();
    obj["usr_id"] = usuariosigned["usr_id"];
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerFunction("ws/Metodos.asmx/ModicarGuia", jsonText, 6);
    //CallDespacharResult


}

function CallModificarResult(data) {
    if (data != "") {
        CallPopUp("modModificar", "Modifcar", data.d);
    }
}



function CallModificarOk() {
    var permite = $("#txtPERMITEMOD").val();
    if (permite.toUpperCase() == "TRUE") {
        var comprobante = $("#txtCODIGOCOMPMOD").val();
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = comprobante;
        obj["com_concepto"] = $("#txtOBSERVACIONMOD").val();
        obj["usr_id"] = usuariosigned["usr_id"];
        var jsonText = JSON.stringify({ objeto: obj });
        CallServerFunction("ws/Metodos.asmx/SaveModicarGuia", jsonText, 7);
    }
    else {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert('No posee autorización para modificar el comprobante...', 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
  
}


function SaveObjResultModificar(data) {
    jQuery.alerts.dialogClass = 'alert-success';
    jAlert('El comprobante se  modifico correctamente', 'correcto', function () {
        try {
            EndModificar();
        }
        catch (err) {
        }
        jQuery.alerts.dialogClass = null; // reset to default
    });
}


/////////////////////////////////FIN MODIFICACION GUIAS//////////////////////////////

/////////////////VENTANA DE MODIFICACION DE DATOS COMPROBANTE/////////////////////////////
// function despachar
function CallModificarDatos(codigo) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = codigo;
    obj["com_concepto"] = $("#txtOBSERVACIONMOD").val();
    obj["usr_id"] = usuariosigned["usr_id"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerFunction("ws/Metodos.asmx/ModicarDatosComprobante", jsonText, 16);
    //CallDespacharResult


}

function CallModificarDatosResult(data) {
    if (data != "") {
        CallPopUp("modModificarDatos", "Modifcar Datos", data.d);
        $(".fecha").datepicker({

            dateFormat: "dd/mm/yy"
        }); //Setea campos de tipo fecha
    }
}



function CallModificarDatosOk() {
    var permite = $("#txtPERMITEMOD").val();
    if (permite.toUpperCase() == "TRUE") {
        var comprobante = $("#txtCODIGOCOMPMOD").val();
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = comprobante;
        obj["com_numero"] = $("#txtNUMEROMOD").val();
        var now = new Date();
        var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHAMOD").val());
        obj["com_fecha"] = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate(), now.getHours(), now.getMinutes(), now.getSeconds(), now.getMilliseconds());
        obj["com_concepto"] = $("#txtOBSERVACIONMOD").val();
        obj = SetAuditoria(obj);
        obj["usr_id"] = usuariosigned["usr_id"];
        var jsonText = JSON.stringify({ objeto: obj });
        CallServerFunction("ws/Metodos.asmx/SaveModicarDatosComprobante", jsonText, 17);
    }
    else {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert('No posee autorización para modificar el comprobante...', 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }

}


function SaveObjResultModificarDatos(data) {
    jQuery.alerts.dialogClass = 'alert-success';
    jAlert('El comprobante se  modifico correctamente', 'correcto', function () {
        try {
            EndModificarDatos();
        }
        catch (err) {
        }
        jQuery.alerts.dialogClass = null; // reset to default
    });
}


/////////////////////////////////FIN MODIFICACION GUIAS//////////////////////////////


/////////////////VENTANA DE MODIFICACION DE PAGOS/////////////////////////////
// function despachar
function CallModificarPago(codigo) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = codigo;
    obj["usr_id"] = usuariosigned["usr_id"];

    var jsonText = JSON.stringify({ objeto: obj });
    CallServerFunction("ws/Metodos.asmx/ModicarPago", jsonText, 11);
    //CallDespacharResult


}

function CallModificarPagoResult(data) {
    if (data != "") {
        CallPopUp("modModificarPago", "Modifcar Pago", data.d);
    }
}



function CallModificarPagoOk() {

    var permite = $("#txtPERMITEMOD").val();
    if (permite.toUpperCase() == "TRUE") {
        var comprobante = $("#txtCODIGOCOMPMOD").val();
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = comprobante;
        obj["com_concepto"] = $("#txtOBSERVACIONMOD").val();
        obj = SetAuditoria(obj);
        obj["usr_id"] = usuariosigned["usr_id"];
        var jsonText = JSON.stringify({ objeto: obj });
        CallServerFunction("ws/Metodos.asmx/SaveModicarPago", jsonText, 12);
    }
    else {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert('No posee autorización para modificar el comprobante...', 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }



    //var comprobante = $("#txtCODIGOCOMPMOD").val();
    //var obj = {};
    //obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    //obj["com_codigo"] = comprobante;
    //obj["com_concepto"] = $("#txtOBSERVACIONMOD").val();
    //obj = SetAuditoria(obj);
    //obj["usr_id"] = usuariosigned["usr_id"];
    //var jsonText = JSON.stringify({ objeto: obj });
    //CallServerFunction("ws/Metodos.asmx/SaveModicarPago", jsonText, 12);
}


function SaveObjResultModificarPago(data) {
    jQuery.alerts.dialogClass = 'alert-success';
    jAlert('El comprobante se  modifico correctamente', 'correcto', function () {
        try {
            EndModificarPago();
        }
        catch (err) {
        }
        jQuery.alerts.dialogClass = null; // reset to default
    });
}


/////////////////////////////////FIN MODIFICACION GUIAS//////////////////////////////


//////////////NUEVA OPCION DE AFECTACION DEUDAS///////////////////////////////////////






/////////////////VENTANA DE ANULACION DE COMPROBANTES/////////////////////////////
// function despachar
function CallAnular(codigo) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = codigo;
    obj["com_concepto"] = $("#txtOBSERVACIONANU").val();
    obj["usr_id"] = usuariosigned["usr_id"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerFunction("ws/Metodos.asmx/AnularComprobante", jsonText, 9);
    //CallDespacharResult


}

function CallAnularResult(data) {
    if (data != "") {
        CallPopUp("modAnular", "Anular Comprobante", data.d);
    }
}



function CallAnularOk() {
    var permite = $("#txtPERMITEANU").val();
    if (permite.toUpperCase() == "TRUE") {
        var comprobante = $("#txtCODIGOCOMPANU").val();
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = comprobante;
        obj["com_concepto"] = $("#txtOBSERVACIONANU").val();
        obj = SetAuditoria(obj);
        var jsonText = JSON.stringify({ objeto: obj });
        CallServerFunction("ws/Metodos.asmx/SaveAnularComprobante", jsonText, 10);
    }
    else {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert('No posee autorización para anular el comprobante...', 'Error', function () {           
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
    
}


function SaveObjResultAnular(data) {
    jQuery.alerts.dialogClass = 'alert-success';
    jAlert('El comprobante ha sido anulado...', 'correcto', function () {
        try {
            EndAnular();
        }
        catch (err) {
        }
        jQuery.alerts.dialogClass = null; // reset to default
    });
}


/////////////////////////////////FIN ANULAR COMPROBANTES//////////////////////////////


/////////////////VENTANA DE ACTIVACION DE COMPROBANTES/////////////////////////////
// function despachar
function CallActivar(codigo) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = codigo;
    obj["com_concepto"] = $("#txtOBSERVACIONANU").val();
    obj["usr_id"] = usuariosigned["usr_id"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerFunction("ws/Metodos.asmx/ActivarComprobante", jsonText, 13);
    //CallDespacharResult


}

function CallActivarResult(data) {
    if (data != "") {
        CallPopUp("modActivar", "Activar Comprobante", data.d);
    }
}



function CallActivarOk() {
    var permite = $("#txtPERMITEACT").val();
    if (permite.toUpperCase() == "TRUE") {
        var comprobante = $("#txtCODIGOCOMPACT").val();
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = comprobante;
        obj["com_concepto"] = $("#txtOBSERVACIONACT").val();
        obj = SetAuditoria(obj);
        var jsonText = JSON.stringify({ objeto: obj });
        CallServerFunction("ws/Metodos.asmx/SaveActivarComprobante", jsonText, 14);
    }
    else {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert('No posee autorización para activar el comprobante...', 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }

}


function SaveObjResultActivar(data) {
    jQuery.alerts.dialogClass = 'alert-success';
    jAlert('El comprobante ha sido activado...', 'correcto', function () {
        try {
            EndActivar();
        }
        catch (err) {
        }
        jQuery.alerts.dialogClass = null; // reset to default
    });
}


/////////////////////////////////FIN ANULAR COMPROBANTES//////////////////////////////


/////////////////////////////////MAYORIZAR COMPROBANTES//////////////////////////////
function CallMayorizar(codigo) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = codigo;
    obj["usr_id"] = usuariosigned["usr_id"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerFunction("ws/Metodos.asmx/MayorizarComprobante", jsonText, 15);
    //CallDespacharResult


}

function CallMayorizarResult(data) {
    if (data != "") {
        if (data.d == "OK") {
            jQuery.alerts.dialogClass = 'alert-success';
            jAlert('El comprobante ha sido mayorizado...', 'correcto', function () {
                try {
                    EndMayorizar();
                }
                catch (err) {
                }
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
        else
        {
            jQuery.alerts.dialogClass = 'alert-warning';
            jAlert(data.d, 'Error', function () {
               jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}


/////////////////////////////////RECONTABILIZAR COMPROBANTES//////////////////////////////
function CallRecontabilizar(codigo) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = codigo;
    obj["usr_id"] = usuariosigned["usr_id"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerFunction("ws/Metodos.asmx/RecontabilizarComprobante", jsonText, 23);
    //CallDespacharResult


}

function CallRecontabilizarResult(data) {
    if (data != "") {
        if (data.d == "OK") {
            jQuery.alerts.dialogClass = 'alert-success';
            jAlert('El comprobante ha sido recontabilizado...', 'correcto', function () {
                try {
                    EndRecontabilizar();
                }
                catch (err) {
                }
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
        else {
            jQuery.alerts.dialogClass = 'alert-warning';
            jAlert(data.d, 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}


/////////////////VENTANA DE AUDITORIA/////////////////////////////
// function despachar
function CallAuditoria(codigo) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerFunction("ws/Metodos.asmx/ViewAuditoria", jsonText, 8);
    //CallDespacharResult


}

function CallAuditoriaResult(data) {
    if (data != "") {
        CallPopUp4("modAuditoria", "Auditoria", data.d);
    }
}

/////////////////////////////////FIN AUDITORIA//////////////////////////////


var loading = false;
function ShowLoading() {
    $("body").append("<div id='divloading' style='width:200px;'><div style='float:left;'><img src='../images/loading.gif' ></img> Cargando...</div><div style='float:right'><input type='button' onclick='CancelCall();' value='Cancelar'></input></div></div>");
    loading = true;
}

function HideLoading() {
    $("#divloading").remove();
    loading = false;
}

function CallInformacion(codigo) {
  
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });

    CallServerFunction("ws/Metodos.asmx/Informacion", jsonText, 22);
    
   
    
}


function CallInformacionResult(data) {

    if (data != "") {

        CallPopUp2("modInformacion", "Información", data.d);
    }
   
}

function CallInformacionOk() {
  
    EndInformacion();

}



function ElectronicRide(codigo)
{
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });

    CallServerFunction("ws/Metodos.asmx/ElectronicRIDE", jsonText, 30);
}

function ElectronicRideResult(data)
{
    if (data!=null)
    {
        var opciones = "toolbar=no, scrollbars=no, resizable=yes, top=50, left=100, width=800, height=600";
        window.open(data.d, "Ride", opciones);
    }
}
