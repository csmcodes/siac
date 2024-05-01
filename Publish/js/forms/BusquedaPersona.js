//Archivo:          BusquedaPersona.js
//Descripción:      Contiene las funciones propias de la interfaz
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Julio 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfBusquedaPersona.aspx";
var menuoption = "BusquedaPersona";

//Codigo ejecutado cuando el document esta listo
$(document).ready(function () {
    LoadFiltros();    //Carga los filtros de busqueda
    LoadListadoHead();
    $("#busquedadetalle").on("scroll", scroll);
    $('body').css('background', 'transparent'); //Limpia el fondo de la pantalla
});



//Funciona que invoca al servidor mediante JSON
function CallServer(strurl, strdata, retorno) {
    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (retorno == 0)
                LoadFiltrosResult(data);
            if (retorno == 1)
                LoadListadoHeadResult(data);
            if (retorno == 2)
                LoadDetalleResult(data);
           
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


//Funcion que controla el scroll dinamico del listado
function scroll() {    
        if ($("#busquedadetalle")[0].scrollHeight - $("#busquedadetalle").scrollTop() <= $("#busquedadetalle").outerHeight()) {
            LoadDetalle();
        }
    
}


function LoadFiltros() {
    CallServer(formname + "/GetFiltros", "{}", 0);
}
function LoadFiltrosResult(data) {
    if (data != "") {
        $("#busquedafiltros").html(data.d);
        $("#busquedafiltros").children().on("change", ReloadDetalle);    //Op
        //var objs = $("#busquedafiltros").children();
        $(".fecha").datepicker("option", "dateFormat", "dd/mm/yy"); //Setea campos de tipo fecha
        LoadDetalle();
    }
}


function LoadListadoHead()
{
    CallServer(formname + "/GetListadoHead", "{}", 1);
}

function LoadListadoHeadResult(data)
{
    if (data != "") {
        $('#busquedahead').html(data.d);
    }
}


function GetJSONSearch() {
    var obj = {};
    obj["per_id"] = $("#txtID").val();
    obj["per_nombres"] = $("#txtNOMBRES").val();
    obj["per_ciruc"] = $("#txtCIRUC").val();
    obj["per_empresa"] = parseInt(empresasigned["emp_codigo"]); 
    return JSON.stringify({ objeto: obj });
}

function LoadDetalle() {

    var jsonText = GetJSONSearch(); 
    CallServer(formname + "/GetDetalle", jsonText, 2);
}

function LoadDetalleResult(data) {
    if (data != "") {
        $('#busquedadetalle').append(data.d);
    }
}

function ReloadDetalle() {
    $('#busquedadetalle').empty();
    var jsonText = GetJSONSearch(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
    CallServer(formname + "/ReloadDetalle", jsonText, 2);
}

function Select(obj) {
    var id = $(obj).data("id");
    var jsonText = JSON.stringify({ id: id });
    alert(id);  
}