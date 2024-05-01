var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfReporteCuentasPor.aspx";
var menuoption = "ReporteCuentasPor";

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
    if (retorno == "DESC")
    {
        GetDescuadresResult(data);
    }
}




function LoadFiltros() {
    var obj = {};
    obj["tipo"] = GetQueryStringParams("tipo");
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
    // Select with Search    
    $(".chzn-select").chosen();
    $("#cmbALMACEN").on("change", LoadPuntoVenta);  //Opción "Cerrar" del combo de opciones de la sección de edición    
    $("#cmbALMACEN").trigger("change");
    //$("#cmbUSUARIO").on("change", LoadReporte);
    // $("#txtDESDE").on("change", LoadReporte);
    // $("#txtHASTA").on("change", LoadReporte);


}


function LoadReporte(tipo) {
    var desde = $("#txtDESDE").val(); //.datepicker("getDate");
    var hasta = $("#txtHASTA").val(); //.datepicker("getDate");
    var almacen = $("#cmbALMACEN").val();
    var pventa = $("#cmbPVENTA").val();
    if (pventa == null)
        pventa = "";
    var stralmacen = $("#cmbALMACEN option:selected").text();
    var strpventa = $("#cmbPVENTA option:selected").text();
    var usr = usuariosigned["usr_id"];

    var cli = $("#txtPERSONA").val();
    var tip = GetQueryStringParams("tipo");

    var ctas = $("#cmbCUENTAS").val();
    var tcxc = $("#cmbTIPO").val();
    var pol="";

    if ($("#cmbPOLITICA").length>0)
        pol = $("#cmbPOLITICA").val();

    var strctas = ctas.join("|");

    var report = "CUENTASPOR";
    if (tipo == 2)
        report = "CUENTASPORVEN";
    if (tipo == 3)
        report = "CUENTASPORVENCON";
    if (tipo == 4)
        report = "CUENTASPORDET";


    //var url = "./reports/wfReportPrint.aspx?report=CUENTASPOR&empresa=" + parseInt(empresasigned["emp_codigo"]) + "&parameter1=" + desde + "&parameter2=" + hasta + "&parameter3=" + almacen + "&parameter4=" + pventa + "&parameter5=" + stralmacen + "&parameter6=" + strpventa + "&parameter7=" + usr + "&parameter8=" + cli + "&parameter9=" + tip + "&parameter10="+strctas+"&parameter11="+tcxc;    
    var url = "./reports/wfReportPrint.aspx?report=" + report + "&empresa=" + parseInt(empresasigned["emp_codigo"]) + "&parameter1=" + desde + "&parameter2=" + hasta + "&parameter3=" + almacen + "&parameter4=" + pventa + "&parameter5=" + stralmacen + "&parameter6=" + strpventa + "&parameter7=" + usr + "&parameter8=" + cli + "&parameter9=" + tip + "&parameter10=" + strctas + "&parameter11=" + tcxc + "&parameter12=" + pol;    
    var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=yes,scrollbars=yes";
    window.open(url, "Reporte", feautures);
}


function LoadReporteDocAnexo() {
    var desde = $("#txtDESDE").val(); //.datepicker("getDate");
    var hasta = $("#txtHASTA").val(); //.datepicker("getDate");
    var almacen = $("#cmbALMACEN").val();
    var pventa = $("#cmbPVENTA").val();
    if (pventa == null)
        pventa = "";
    var stralmacen = $("#cmbALMACEN option:selected").text();
    var strpventa = $("#cmbPVENTA option:selected").text();
    var usr = usuariosigned["usr_id"];

    var cli = $("#txtPERSONA").val();
    var tip = GetQueryStringParams("tipo");

    var ctas = $("#cmbCUENTAS").val();
    var pol = $("#cmbPOLITICA").val();

    var strctas = ctas.join("|");

    var url = "./reports/wfReportPrint.aspx?report=CUENTASPORDOCANEXO&empresa=" + parseInt(empresasigned["emp_codigo"]) + "&parameter1=" + desde + "&parameter2=" + hasta + "&parameter3=" + almacen + "&parameter4=" + pventa + "&parameter5=" + stralmacen + "&parameter6=" + strpventa + "&parameter7=" + usr + "&parameter8=" + cli + "&parameter9=" + tip + "&parameter10=" + strctas;
    var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=yes,scrollbars=yes";
    window.open(url, "Reporte", feautures);
}
function LoadReporteAnexo() {
    var desde = $("#txtDESDE").val(); //.datepicker("getDate");
    var hasta = $("#txtHASTA").val(); //.datepicker("getDate");
    var almacen = $("#cmbALMACEN").val();
    var pventa = $("#cmbPVENTA").val();
    if (pventa == null)
        pventa = "";
    var stralmacen = $("#cmbALMACEN option:selected").text();
    var strpventa = $("#cmbPVENTA option:selected").text();
    var usr = usuariosigned["usr_id"];

    var cli = $("#txtPERSONA").val();
    var tip = GetQueryStringParams("tipo");

    var ctas = $("#cmbCUENTAS").val();
    var pol = $("#cmbPOLITICA").val();

    var strctas = ctas.join("|");

    var url = "./reports/wfReportPrint.aspx?report=CUENTASPORANEXO&empresa=" + parseInt(empresasigned["emp_codigo"]) + "&parameter1=" + desde + "&parameter2=" + hasta + "&parameter3=" + almacen + "&parameter4=" + pventa + "&parameter5=" + stralmacen + "&parameter6=" + strpventa + "&parameter7=" + usr + "&parameter8=" + cli + "&parameter9=" + tip + "&parameter10=" + strctas;
    var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=yes,scrollbars=yes";
    window.open(url, "Reporte", feautures);
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


function GetDescuadres()
{
    var obj = {};
    obj["empresa"] = empresasigned["emp_codigo"];
    obj["desde"] = $("#txtDESDE").val(); //.datepicker("getDate");
    obj["hasta"] = $("#txtHASTA").val(); //.datepicker("getDate");
    obj["almacen"] = $("#cmbALMACEN").val();
    obj["pventa"] = $("#cmbPVENTA").val();
    obj["crea_usr"]= usuariosigned["usr_id"];
    obj["persona"] = $("#txtPERSONA").val();
    obj["tipo"]=  GetQueryStringParams("tipo");     
    var ctas = $("#cmbCUENTAS").val();
    obj["cuentas"] = ctas.join("|");

    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(formname + "/GetDescuadres", jsonText, "DESC");
}


function GetDescuadresResult(data)
{
    if (data.d!=null)
    {
        alert(data.d);
    }
}



