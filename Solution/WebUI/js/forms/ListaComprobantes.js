var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfListaComprobantes.aspx";
var menuoption = "ListaComprobantes";

function StopPropagation(event) {
    if (!event) var event = window.event;
    event.cancelBubble = true;
    if (event.stopPropagation) event.stopPropagation();
}

//Codigo ejecutado cuando el document esta listo
$(document).ready(function () {
    $('body').css('background', 'transparent');
    LoadCabecera();
});

function ServerResult(data, retorno) {
    if (retorno == 0) {
        LoadCabeceraResult(data);
    }
    if (retorno == 1) {
        LoadPuntoVentaResult(data);
    }
    if (retorno == 2) {
        LoadDetalleResult(data);
    }
    if (retorno == 3) {
        LoadDetalleDataResult(data);
    }
    if (retorno == 4) {
        CallFormularioResult(data);
    }
    if (retorno == 5) {
        ModificarPagoResult(data);
    }
    if (retorno == "AfectaDeudas")
    {
        AfectaDeudasResult(data);
    }
    if (retorno == "RET")
        GeneraRetencionResult(data);
    if (retorno == "ELEC")
        GenerarElectronicoResult(data);

}


function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtNOMBRESRED") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_apellidos + " " + item.per_nombres,
            info: item
        }
    }
   
}

function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtNOMBRESRED") {
        $("#txtCODRED").val(item.info.per_codigo);
        $("#txtCIRUCRED").val(item.info.per_ciruc);
        $("#txtNOMBRESRED").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtDIRECCIONRED").val(item.info.per_direccion);
        $("#txtTELEFONORED").val(item.info.per_telefono);
    }
    
}



function LoadCabecera() {
    var obj = {};
    obj["uxe_usuario"] = usuariosigned["usr_id"];
    obj["uxe_empresa"] = empresasigned["emp_codigo"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(formname + "/GetCabecera",jsonText, 0)    
}



function LoadCabeceraResult(data) {
    if (data != "") {
        $('#comcabecera').html(data.d);
        //LoadDetalle();
    }
    SetForm(); //Depende de cada js.        
}

function SetForm() {

    $("#repo").on("click", LoadReporte);  //GENERAR REPORTE    

    $(".fecha").datepicker({
        dateFormat: "dd/mm/yy"
    });
    // Select with Search    
    $(".chzn-select").chosen();
    

    $("#cmbUSUARIO").on("change", LoadDetalle);
    $("#txtPERIODO").on("change", LoadDetalle);
    $("#txtMES").on("change", LoadDetalle);
    $("#txtFECHA").on("change", LoadDetalle);
    $("#cmbTIPODOC").on("change", LoadDetalle);        
    $("#txtNUMERO").on("change", LoadDetalle);
    $("#cmbESTADO").on("change", LoadDetalle);
    $("#cmbPOLITICA").on("change", LoadDetalle);
        
    $("#cmbALMACEN").on("change", LoadPuntoVenta);  //Opción "Cerrar" del combo de opciones de la sección de edición    
    $("#cmbALMACEN").trigger("change");

    $("#txtPERSONA").on("change", LoadDetalle);
    $("#txtVALOR").on("change", LoadDetalle);
    $("#cmbVALOR").on("change", LoadDetalle);//operador
    $("#cmbESTADOENVIO").on("change", LoadDetalle);//estadoenvio
    $("#txtREMITENTE").on("change", LoadDetalle);
    $("#txtDESTINATARIO").on("change", LoadDetalle);
    $("#txtSOCIO").on("change", LoadDetalle);
    $("#txtVEHICULO").on("change", LoadDetalle);
    $("#txtDETALLE").on("change", LoadDetalle);
   
}

function LoadPuntoVenta() {
    var obj = {};
    obj["id"] = "cmbPVENTA";
    obj["empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["almacen"] = $("#cmbALMACEN").val();
    obj["usuario"] = usuariosigned["usr_id"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(formname + "/GetPuntoVenta", jsonText, 1);
}

function LoadPuntoVentaResult(data) {
    if (data != "") {
        $("#cmbPVENTA").replaceWith(data.d);
        $("#cmbPVENTA").on("change", LoadDetalle);
    }
    LoadDetalle();

}

function LoadDetalle() {    
    var jsonText = JSON.stringify({});
    CallServerMethods(formname + "/GetDetalle", jsonText, 2);
}

function LoadDetalleResult(data) {
    if (data != "") {        
        $('#comdetallecontent').html(data.d);
        LoadDetalleData();
    }
    SetFormDetalle();    
}

function SetFormDetalle() {        
    $("#comdetallecontent").on("scroll", scroll);
}

function scroll() {
    if ($("#comdetallecontent")[0].scrollHeight - $("#comdetallecontent").scrollTop() <= $("#comdetallecontent").outerHeight()) {
        LoadDetalleData();
    }
}

//var isloading = false;
function LoadDetalleData() {
    if (!loading) {
        var obj = {};
        obj["periodo"] = parseInt($("#txtPERIODO").val());
        obj["mes"] = parseInt($("#txtMES").val());
        obj["fecha"] = $("#txtFECHA").val();
        //obj["com_ctipocom"] = parseInt($("#cmbCTIPOCOM").val());
        obj["almacen"] = parseInt($("#cmbALMACEN").val());
        obj["pventa"] = parseInt($("#cmbPVENTA").val());
        obj["numero"] = parseInt($("#txtNUMERO").val());
        var estado = $("#cmbESTADO option:selected").text();
        if (estado.length > 0)
            obj["estado"] = parseInt($("#cmbESTADO").val());
        else
            obj["estado"] = -1;
        //obj["tipodoc"] = parseInt($("#cmbTIPODOC").val());
        obj["tipos"] = $("#cmbTIPODOC").val();
        obj["concepto"] = $("#txtCONCEPTO").val();

        obj["politica"] = $("#cmbPOLITICA option:selected").text();


        obj["nombres"] = $("#txtPERSONA").val();
        //obj["nombres_rem"] = $("#txtREMITENTE").val();
        //obj["nombres_des"] = $("#txtDESTINATARIO").val();
        //obj["nombres_soc"] = $("#txtSOCIO").val();
        obj["placa"] = $("#txtVEHICULO").val();
        
        obj["total"] = $("#txtVALOR").val();

        obj["operador"] = $("#cmbVALOR").val();
        obj["estadoenvio"] = $("#cmbESTADOENVIO").val();
        obj["detalle"] = $("#txtDETALLE").val();
        ////////////////////////////////////////////////////
        obj["crea_usr"] = $("#cmbUSUARIO").val();
        obj["usr_id"] = usuariosigned["usr_id"];
        obj["empresa"] = empresasigned["emp_codigo"];
        //obj["com_ref_comprobante"] = $("#txtREFERENCIA_S").val();
        //obj["com_aut_tipo"] = parseInt($("#cmbAUTORIZ_S").val());


        //estado = $("#cmbCONTABILZA_S option:selected").text();
        //if (estado != "--Activo--")
        //    obj["com_nocontable"] = parseInt($("#cmbCONTABILZA_S").val());
        //else
        //    obj["com_nocontable"] = -1;

        var jsonText = JSON.stringify({ objeto: obj });
        //isloading = true;
        ShowLoading();
        CallServerMethods(formname + "/GetDetalleData", jsonText, 3);
    }
}

function LoadDetalleDataResult(data) {
    if (data != "") {
        $('#tddatos').append(data.d);
        //isloading = false;
        HideLoading();
    }

}


function AsignarSocio(id) {

    CallAsignarSocio(id);
}

function EndAsignarSocio(id) {
    LoadDetalle();
}


function Despachar(id) {

    CallDespachar(id);
}

function EndDespachar(id) {
    LoadDetalle();
}

function AsignarFacturaPlanilla(id) {

    CallAsignarFacturaPlanilla(id);
}

function EndAsignarFacturaPlanilla(id) {
    LoadDetalle();
}

function Mayorizar(id) {
    CallMayorizar(id);
}
function EndMayorizar(id) {
    LoadDetalle();
}

function Recontabilizar(id) {
    CallRecontabilizar(id);
}
function EndRecontabilizar(id) {
    LoadDetalle();
}


function Modificar(id) {
    CallModificar(id);
}

function EndModificar(id) {
    LoadDetalle();
}

function ModificarDatos(id) {
    CallModificarDatos(id);
}

function EndModificarDatos(id) {
    LoadDetalle();
}

function ModificarPago(id) {
   /* var compr = {};
    if (id != null) {
        compr["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        compr["com_codigo"] = parseInt(id);
    }
    var jsonText = JSON.stringify({ objeto: compr });
    CallServerMethods(webservice + "GetFormulario", jsonText, 5);*/
    CallModificarPago(id);
}


function AfectaDeudas(codigo) {
    var obj = {};
    obj["com_empresa"] = empresasigned["emp_codigo"];
    obj["com_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "GetAfectaDeudas", jsonText, "AfectaDeudas");
    //CallDespacharResult


}


function AfectaDeudasResult(data) {
    if (data != "") {
        
        bootbox.dialog({
            message: data.d,
            title: "Afectaciones Deudassss",
            size: "large",
            buttons: {
                aceptar:
                {
                    label: "Guardar",
                    className: "btn-primary",
                    callback: function () {
                        //return SaveModificarEnvios();
                    }
                },
                cancelar:
                {
                    label: "Cancelar",
                    className: "btn-default",
                }
            }
        });

        //CallPopUp2("modAfectaDeudas", "Afectaciones ", data.d);
    }
}


/*function ModificarPagoResult(data) {
    if (data != "") {
        window.location = data.d+"&modify=true";
    }
}*/
function EndModificarPago(id) {
    LoadDetalle();
}

function Anular(id) {
    CallAnular(id);
}

function EndAnular(id) {
    LoadDetalle();
}

function Activar(id) {
    CallActivar(id)
}
function EndActivar(id) {
    LoadDetalle();
}
function Informacion(id) {

    CallInformacion(id);
}
function EndInformacion(id) {
   // LoadDetalle();
}

function Cobrar(id) {
    window.location.href = "wfCancelacion.aspx?tipodoc=6&origen=FAC&codigocompref=" + id + "&codigoempresa=" + empresasigned["emp_codigo"];
}

function GuiaRemision(id)
{
    window.location.href = "wfGuiaRemision.aspx?tipodoc=28&codigocompref=" + id + "&codigoempresa=" + empresasigned["emp_codigo"];
}

function Liquidar(id) {
    window.location.href = "wfCancelacion.aspx?tipodoc=6&origen=LGC&codigocompref=" + id + "&codigoempresa=" + empresasigned["emp_codigo"];
}

function Valores(id) {
    if (id != null) {
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = parseInt(id);
        CallValoresSocios(obj);
    }        
}

function CallFormulario(id) {
    var compr = {};
    if (id != null) {
        compr["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        compr["com_codigo"] = parseInt(id);
    }
    var jsonText = JSON.stringify({ objeto: compr });
    CallServerMethods(webservice + "GetFormulario", jsonText, 4);
}

function CallFormularioResult(data) {
    if (data != "") {
        window.location = data.d;
    };
}

function ViewAuditoria(id) {
    CallAuditoria(id);
}


function ValidaReporte() {
    var mensaje = "";

    if ($("#txtPERIODO").val() == "" && $("#txtMES").val() == "" && $("#txtFECHA").val()=="")
        mensaje ="Debe seleccionar un periodo, mes o fecha para generar el reporte";



    if (mensaje != "") {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert(mensaje, 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
        return false;
    }
    return true;
}

function LoadReporte() {

    if (ValidaReporte()) {

        var obj = {};
        obj["periodo"] = parseInt($("#txtPERIODO").val());
        obj["mes"] = parseInt($("#txtMES").val());
        obj["fecha"] = $("#txtFECHA").val();
        obj["almacen"] = parseInt($("#cmbALMACEN").val());
        obj["pventa"] = parseInt($("#cmbPVENTA").val());
        obj["numero"] = parseInt($("#txtNUMERO").val());
        var estado = $("#cmbESTADO option:selected").text();
        if (estado.length > 0)
            obj["estado"] = parseInt($("#cmbESTADO").val());
        else
            obj["estado"] = -1;
        //obj["tipodoc"] = parseInt($("#cmbTIPODOC").val());

        var tipos = $("#cmbTIPODOC").val();
        var strtipos = "";

        for (var i = 0; i < tipos.length; i++) {
            if (tipos[i].toString() != "")
                strtipos += ((strtipos != "") ? "," : "") + tipos[i].toString();
        }

        obj["tipos"] = strtipos; //  $("#cmbTIPODOC").val();
        obj["concepto"] = $("#txtCONCEPTO").val();
        obj["politica"] = $("#cmbPOLITICA option:selected").text();
        obj["nombres"] = $("#txtPERSONA").val();
        obj["placa"] = $("#txtVEHICULO").val();
        obj["total"] = $("#txtVALOR").val();

        obj["operador"] = $("#cmbVALOR").val();
        obj["estadoenvio"] = $("#cmbESTADOENVIO").val();
        obj["detalle"] = $("#txtDETALLE").val();
        ////////////////////////////////////////////////////
        obj["crea_usr"] = $("#cmbUSUARIO").val();
        obj["usr_id"] = usuariosigned["usr_id"];
        obj["empresa"] = empresasigned["emp_codigo"];


        var querystring = $.param(obj);
        var url = "wfListaComprobantesPrint.aspx?report=CDC&" + querystring;
        //var url = "/wfprintComprobante.aspx?report=CDC&parameter1=" + obj["periodo"] + "&parameter2=" + obj["mes"] + "&parameter3=" + obj["fecha"] + "&parameter4=" + obj["almacen"] + "&parameter5=" + obj["pventa"] + "&parameter6=" + obj["numero"] + "&parameter7=" + obj["estado"] + "&parameter8=" + obj["tipodoc"] + "&parameter9=" + obj["concepto"] + "&parameter10=" + politica + "&parameter11=" + nombres + "&parameter12=" + placa + "&parameter13=" + total + "&parameter14=" + operador + "&parameter15=" + estadoenvio + "&parameter16=" + crea_usr + "&parameter17=" + usr_id;
        var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=yes,scrollbars=yes";
        window.open(url, "Reporte", feautures);
    }    

}



function GeneraRetencion(cod) {

    window.location.href = "wfRetencion.aspx?tipodoc=16&origen=OBL&codigocompref=" + cod + "&codigoempresa=" + empresasigned["emp_codigo"];

    /*var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = cod;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods("ws/Metodos.asmx/GeneraRetencion", jsonText, "RET");*/

}

function GeneraRetencionResult(data) {
    if (data != "") {
        var cod = parseInt(data.d);
        if (cod > 0) {
            window.location = "wfRetencion.aspx?codigocomp=" + cod;
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al generar el comprobante de retención...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}



///////////////////////////ELECTRONICOS///////////////////

function GenerarElectronico(cod) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = cod;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods("ws/Metodos.asmx/GenerarElectronico", jsonText, "ELEC");

}

function GenerarElectronicoResult(data) {
    if (data != "") {
        if (data.d == "ok")
        {
            jQuery.alerts.dialogClass = 'alert-info';
            jAlert('Se ha generado y enviado el comprobante electrónico correctamente', 'Información', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
        else
        {

        
        
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al generar el comprobante de electrónico...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}