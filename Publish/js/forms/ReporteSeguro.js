var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfReporteSeguro.aspx";
var menuoption = "ReporteSeguro";

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
    $("#cmbALMACEN").on("change", LoadPuntoVenta);  //Opción "Cerrar" del combo de opciones de la sección de edición    
    $("#cmbALMACEN").trigger("change");
    SetAutocompleteById("txtREMITENTE");

}

function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtREMITENTE") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_apellidos + " " + item.per_nombres,
            info: item
        }
    }
}



function GetAutoCompleteObj(idobj, item) {

    if (idobj == "txtREMITENTE") {
        $("#txtREMITENTE").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtCODREM").val(item.info.per_codigo);     
    }

}

function LoadReporte() {
    var desde = $("#txtDESDE").val(); //.datepicker("getDate");
    var hasta = $("#txtHASTA").val(); //.datepicker("getDate");
    var remitente = $("#txtCODREM").val();
    var destinatario= $("#txtDESTINATARIO").val();
    var almacen = $("#cmbALMACEN").val();
    var pventa = $("#cmbPVENTA").val();
    if (pventa == null)
        pventa = "";
    var stralmacen = $("#cmbALMACEN option:selected").text();
    var strpventa = $("#cmbPVENTA option:selected").text();
    var usr = usuariosigned["usr_id"];
    var url = "./reports/wfReportPrint.aspx?report=SEGURO&empresa=" + parseInt(empresasigned["emp_codigo"]) + "&parameter1=" + desde + "&parameter2=" + hasta + "&parameter3=" + remitente + "&parameter4=" + destinatario + "&parameter5=" + usr + "&parameter6=" + almacen + "&parameter7=" + pventa + "&parameter8=" + stralmacen + "&parameter9=" + strpventa;
    var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=0,scrollbars=0";
    window.open(url, "Reporte", feautures);
    //loadIframeReport(url);
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
