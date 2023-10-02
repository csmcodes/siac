var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfValoresSocios.aspx";
var menuoption = "ValoresSocios";

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
    //obj["uxe_usuario"] = usuariosigned["usr_id"];
    //obj["uxe_empresa"] = empresasigned["emp_codigo"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(formname + "/GetCabecera", jsonText, 0)
}



function LoadCabeceraResult(data) {
    if (data != "") {
        $('#comcabecera').html(data.d);
        LoadDetalle();
    }
    //SetForm(); //Depende de cada js.        
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
    $("#cmbVALOR").on("change", LoadDetalle); //operador
    $("#cmbESTADOENVIO").on("change", LoadDetalle); //estadoenvio
    $("#txtREMITENTE").on("change", LoadDetalle);
    $("#txtDESTINATARIO").on("change", LoadDetalle);
    $("#txtSOCIO").on("change", LoadDetalle);
    $("#txtVEHICULO").on("change", LoadDetalle);

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
    }
    SetFormDetalle();
}

function SetFormDetalle() {
   
}



function ViewValores(socio)
{
    if (socio == undefined || socio == null)
        socio = "";
    var url = "./reports/wfReportPrint.aspx?report=VALSOCIOSINPLANILLA&empresa=" + parseInt(empresasigned["emp_codigo"]) + "&parameter1=" + socio + "&parameter2=" + usuariosigned["usr_id"];
    var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=yes,scrollbars=yes";
    window.open(url, "Reporte", feautures);
}


function Planillar(id,vehiculo) {

    if (vehiculo!=null)
        window.location.href = "wfPlanillaSocioNew.aspx?tipodoc=7&origen=VS&codsocio=" + id + "&vehiculo=" + vehiculo + "&codigoempresa=" + empresasigned["emp_codigo"];
    else
        window.location.href = "wfPlanillaSocioNew.aspx?tipodoc=7&origen=VS&codsocio=" + id + "&codigoempresa=" + empresasigned["emp_codigo"];
    //window.location.href = "wfCancelacion.aspx?tipodoc=6&origen=FAC&codigocompref=" + id + "&codigoempresa=" + empresasigned["emp_codigo"];
}





function EndDespachar(id) {
    LoadDetalle();
}



function ValidaReporte() {
    var mensaje = "";

    if ($("#txtPERIODO").val() == "" && $("#txtMES").val() == "" && $("#txtFECHA").val() == "")
        mensaje = "Debe seleccionar un periodo, mes o fecha para generar el reporte";



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
        ////////////////////////////////////////////////////
        obj["crea_usr"] = $("#cmbUSUARIO").val();
        obj["usr_id"] = usuariosigned["usr_id"];



        var querystring = $.param(obj);
        var url = "wfListaComprobantesPrint.aspx?report=CDC&" + querystring;
        //var url = "/wfprintComprobante.aspx?report=CDC&parameter1=" + obj["periodo"] + "&parameter2=" + obj["mes"] + "&parameter3=" + obj["fecha"] + "&parameter4=" + obj["almacen"] + "&parameter5=" + obj["pventa"] + "&parameter6=" + obj["numero"] + "&parameter7=" + obj["estado"] + "&parameter8=" + obj["tipodoc"] + "&parameter9=" + obj["concepto"] + "&parameter10=" + politica + "&parameter11=" + nombres + "&parameter12=" + placa + "&parameter13=" + total + "&parameter14=" + operador + "&parameter15=" + estadoenvio + "&parameter16=" + crea_usr + "&parameter17=" + usr_id;
        var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=yes,scrollbars=yes";
        window.open(url, "Reporte", feautures);
    }

}
