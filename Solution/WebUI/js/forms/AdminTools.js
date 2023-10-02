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
    $("#btnfixpersona").on("click", FixPersona);
    $("#btnmayoriza").on("click", MayorizaAll);
    $("#btnreaccount").on("click", RecontabilizaAll);
    $("#btnreaccountcom").on("click", RecontabilizarComp);
    $("#btnfixcomprobantes").on("click", FixComprobantes);

    $("#btnfixdocumentos").on("click", FixDocumentos);
    $("#btnimp").on("click", ImpClientes);
    $("#btnpla").on("click", CuadrarPlanilla);
    $("#btncanfac").on("click", CanFacCobro);
    $("#btndca").on("click", CanNegativas);
    $("#btnserie").on("click", SerieDuplicados);
    $("#btngetcan").on("click", GetCancelacionesNoPlanilla);
    $("#btnremovedup").on("click", RemoveRetDuplicadas);
    $("#btnfixretdup").on("click", FixRetDuplicadas);
    $("#btnactualizadocs").on("click", ActualizarDocumentos);
    $("#btnactualizacan").on("click", ActualizarCancelaciones);
    $("#btnerrores").on("click", GetErroresPlanillas);
    
    $("#btnclosecartera").on("click", CloseCarteraCliAll);
    $("#btnclosecartera1").on("click", CloseCarteraCliNotAll);

});

function ServerResult(data, retorno) {
    if (retorno == 1) {
        alert(data.d);
    }

    if (retorno == 2)
        $("#reshtml").html(data.d);
   

}



function FixPersona() {
    var obj = {};
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "FixPersona", jsonText, 0)
}

function MayorizaAll() {
    var obj = {};
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "MayorizarAllComprobante", jsonText, 1)
}
function RecontabilizaAll() {
    var obj = {};
    obj["com_periodo"] = $("#periodo").val();
    obj["com_mes"] = $("#mes").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "RecontabilizarAllComprobante", jsonText, 1)
}

function RecontabilizarComp() {
    var obj = {};
    obj["com_periodo"] = $("#periodo").val();
    obj["com_mes"] = $("#mes").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "RecontabilizarComprobantes", jsonText, 1)
}


function FixComprobantes() {
    var obj = {};
    obj["com_periodo"] = $("#periodo").val();
    obj["com_mes"] = $("#mes").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "FixComprobantes", jsonText, 1);
}

function FixDocumentos() {

    var obj = {};
    obj["com_empresa"] = 1;
    obj["com_periodo"] = $("#periodo").val();
    obj["com_mes"] = $("#mes").val();
    var jsonText = JSON.stringify({ objeto: obj });
    alert(jsonText);
    CallServerMethods(webservice + "FixDocumentos", jsonText, 1);
}


function ImpClientes() {

    var obj = {};
    obj["com_empresa"] = 1;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "ImportClientes", jsonText, 1);
}



function CanFacCobro() {

    var obj = {};
    obj["empresa"] = 1;
    obj["periodo"] = $("#periodo").val();
    obj["mes"] = $("#mes").val();
    var jsonText = JSON.stringify({ objeto: obj });    
    CallServerMethods(webservice + "CancelFacturasCobro", jsonText, 1);
}


function CuadrarPlanilla() {
    var obj = {};
    obj["com_empresa"] = 1;
    obj["com_codigo"] = $("#codipla").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "CuadrarPlanilla", jsonText, 1);
}



function CanNegativas() {
    var obj = {};
    obj["com_empresa"] = 1;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "CancelacionesNegativas", jsonText, 1);
}



function SerieDuplicados() {
    var obj = {};
    obj["doctrans"] = $.parseJSON("[" + $("#doctrans").val() + "]");
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "SerieDuplicados", jsonText, 1);
}

function GetCancelacionesNoPlanilla() {
    var obj = {};
    obj["desde"] = $("#desde").val();
    obj["hasta"] = $("#hasta").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "GetCancelacionesNoPlanilla", jsonText, 2);
}


function RemoveRetDuplicadas() {
    var obj = {};
    obj["desde"] = $("#desde").val();
    obj["hasta"] = $("#hasta").val();
    obj["tipos"] = $("#tipos").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "RemoveRetDuplicadas", jsonText, 2);
}

function FixRetDuplicadas() {
    var obj = {};    
    obj["clave"] = $("#clave").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "FixRetDuplicadas", jsonText, 2);
}


function ActualizarDocumentos() {
    var obj = {};
    obj["desde"] = $("#desde").val();
    obj["hasta"] = $("#hasta").val();
    obj["ruc"] = $("#ruc").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "ActualizarDocumentos", jsonText, 2);
}


function GetErroresPlanillas() {
    var obj = {};
    obj["desde"] = $("#desde").val();
    obj["hasta"] = $("#hasta").val();
    //obj["ruc"] = $("#ruc").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "GetErroresPlanillas", jsonText, 2);
}



function CloseCarteraCliAll() {
    var obj = {};
    obj["desde"] = $("#desde").val();
    obj["hasta"] = $("#hasta").val();    
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "CloseCarteraCli", jsonText, 2);
}
function CloseCarteraCliNotAll() {
    var obj = {};
    obj["desde"] = $("#desde").val();
    obj["hasta"] = $("#hasta").val();
    obj["all"] = false;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "CloseCarteraCli", jsonText, 2);
}

function ActualizarCancelaciones() {
    var obj = {};
    obj["desde"] = $("#desde").val();
    obj["hasta"] = $("#hasta").val();
    obj["ruc"] = $("#ruc").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "ActualizarCancelaciones", jsonText, 2);
}