var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfReporteDetalles.aspx";
var menuoption = "ReporteDetalles";

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
    //$("#cmbALMACEN").on("change", LoadPuntoVenta);  //Opción "Cerrar" del combo de opciones de la sección de edición    
    //$("#cmbALMACEN").trigger("change");

    SetAutocompleteById("txtCODCLIPRO");

    //$("#cmbUSUARIO").on("change", LoadReporte);
    // $("#txtDESDE").on("change", LoadReporte);
    // $("#txtHASTA").on("change", LoadReporte);


}

function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCODCLIPRO") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_id,
            info: item
        }
    }
   
}

function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCODCLIPRO") {
        $("#txtCODPER").val(item.info.per_codigo);
        $("#txtCIRUC").val(item.info.per_ciruc);
        $("#txtNOMBRES").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtRAZON").val(item.info.per_razon);
        $("#txtRUC").val(item.info.per_ciruc);
       
    }
   
}




function LoadReporte() {
    var desde= $("#txtFECHADESDE").val(); //.datepicker("getDate");
    var hasta = $("#txtFECHAHASTA").val(); //.datepicker("getDate");
    var cliente = $("#txtCODPER").val();
    var ruta = $("#cmbRUTA").val();
    var politica = $("#cmbPOLITICA").val();
    var usr = usuariosigned["usr_id"];
    var numero = $("#txtNUMERO").val();
    var url = "./reports/wfReportPrint.aspx?report=DETGUI&empresa=" + parseInt(empresasigned["emp_codigo"]) + "&parameter1=" + desde + "&parameter2=" + hasta + "&parameter3=" + cliente + "&parameter4=" + ruta + "&parameter5=" + usr + "&parameter6=" + numero + "&parameter7=" + politica;
    var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=0,scrollbars=0";
    window.open(url, "Reporte", feautures);
    //loadIframeReport(url);
}

function LoadReporteTotales() {
    var desde = $("#txtDESDE").val(); //.datepicker("getDate");
    var hasta = $("#txtHASTA").val(); //.datepicker("getDate");
    var socio = $("#cmbUSUARIO").val();
    var almacen = $("#cmbALMACEN").val();
    var pventa = $("#cmbPVENTA").val();
    if (pventa == null)
        pventa = "";
    var stralmacen = $("#cmbALMACEN option:selected").text();
    var strpventa = $("#cmbPVENTA option:selected").text();
    var usr = usuariosigned["usr_id"];
    var url = "./reports/wfReportPrint.aspx?report=VENSOT&empresa=" + parseInt(empresasigned["emp_codigo"]) + "&parameter1=" + desde + "&parameter2=" + hasta + "&parameter3=" + socio + "&parameter4=" + usr + "&parameter5=" + almacen + "&parameter6=" + pventa + "&parameter7=" + stralmacen + "&parameter8=" + strpventa;
    var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=0,scrollbars=0";
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


function CleanFiltros()
{
    $("#txtDESDE").val(""); //.datepicker("getDate");
    $("#txtHASTA").val(""); //.datepicker("getDate");    
    $("#cmbALMACEN").val("");
    $("#cmbPVENTA").val("");
    
    $("#txtCODCLIPRO").val("");
    $("#txtCODPER").val("");
    $("#txtCIRUC").val("");
    $("#txtNOMBRES").val("");
    $("#txtRAZON").val("");
    $("#txtRUC").val("");
}