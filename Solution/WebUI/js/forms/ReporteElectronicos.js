﻿var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfReporteElectronicos.aspx";
var menuoption = "ReporteElectronicos";

function StopPropagation(event) {
    if (!event) var event = window.event;
    event.cancelBubble = true;
    if (event.stopPropagation) event.stopPropagation();
}

//Codigo ejecutado cuando el document esta listo
$(document).ready(function () {
    $('body').css('background', 'transparent');
    LoadFiltros();
});

function ServerResult(data, retorno) {
    if (retorno == 0) {
        LoadFiltrosResult(data);
    }
    if (retorno == 1) {
        LoadPuntoVentaResult(data);
    }
    if (retorno == 2)
        alert(data.d);
}




function LoadFiltros() {
    var obj = {};
    //obj["uxe_usuario"] = usuariosigned["usr_id"];
    //obj["uxe_empresa"] = empresasigned["emp_codigo"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(formname + "/GetFiltros", jsonText, 0)
}



function LoadFiltrosResult(data) {
    if (data != "") {
        $('#comcabecera').html(data.d);
        //LoadDetalle();
    }
    SetForm(); //Depende de cada js.        
}

function SetForm() {

    $(".fecha").datepicker({
        dateFormat: "dd/mm/yy"
    });
    $("#cmbALMACEN").on("change", LoadPuntoVenta);  //Opción "Cerrar" del combo de opciones de la sección de edición    
    $("#cmbALMACEN").trigger("change");
    //$("#cmbUSUARIO").on("change", LoadReporte);
    // $("#txtDESDE").on("change", LoadReporte);
    // $("#txtHASTA").on("change", LoadReporte);


}


function LoadReporte() {
    var desde = $("#txtDESDE").val(); //.datepicker("getDate");
    var hasta = $("#txtHASTA").val(); //.datepicker("getDate");
    var almacen = $("#cmbALMACEN").val();
    var pventa = $("#cmbPVENTA").val();
    if (pventa == null)
        pventa = "";
    var stralmacen = $("#cmbALMACEN option:selected").text();
    var strpventa = $("#cmbPVENTA option:selected").text();
    var tipodoc = $("#cmbTIPO").val();
    var estado = $("#cmbESTADO").val();
    var usr = usuariosigned["usr_id"];

    var url = "./reports/wfReportPrint.aspx?report=ELECTRONICOS&empresa=" + parseInt(empresasigned["emp_codigo"]) + "&parameter1=" + desde + "&parameter2=" + hasta + "&parameter3=" + almacen + "&parameter4=" + pventa + "&parameter5=" + stralmacen + "&parameter6=" + strpventa + "&parameter7=" + tipodoc + "&parameter8=" + estado + "&parameter9=" + usr;
    var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=yes,scrollbars=yes";
    window.open(url, "Reporte", feautures);
    //loadIframeReport(url);
}



var iframeReport = "ireport";
function loadIframeReport(url) {
    var $iframe = $('#' + iframeReport);
    if ($iframe.length) {
        $iframe.attr('src', url);
    }
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
    }
}

function Verificar()
{
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_almacen"] = $("#cmbALMACEN").val();
    obj["com_pventa"] = $("#cmbPVENTA").val();
    obj["com_tipodoc"] = $("#cmbTIPO").val();
    obj["com_estadoelec"] = $("#cmbESTADO").val();
    obj["crea_usr"] = usuariosigned["usr_id"];

    obj["desde"] = $("#txtDESDE").val();
    obj["hasta"] = $("#txtHASTA").val();

    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(formname + "/VerificarComprobantes", jsonText, 2);
}