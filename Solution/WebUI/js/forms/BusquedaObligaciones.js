//Archivo:          BusquedaComprobantes.js
//Descripción:      Contiene las funciones propias de la interfaz
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Septiembre 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfBusquedaObligaciones.aspx";
var menuoption = "BusquedaObligaciones";

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
            if (retorno == 8)
                GeneraRetencionResult(data);
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


function LoadListadoHead() {
    CallServer(formname + "/GetListadoHead", "{}", 1);
}

function LoadListadoHeadResult(data) {
    if (data != "") {
        $('#busquedahead').html(data.d);
    }
}


function GetJSONSearch() {
    var obj = {};
    obj["per_id"] = $("#txtID").val();
    obj["per_nombres"] = $("#txtNOMBRES").val();
    obj["per_ciruc"] = $("#txtCIRUC").val();
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
  // alert(id);
 //   window.location = "wfObligacion.aspx?codigocomp=" + id;

}

function RemoveRow(btn) {
    var row = $(btn).parents('tr');
    var hascontrols = ($(row).find("input").length > 0) ? true : false;
    if ($(row)[0].id != "editrow" && !hascontrols) {
      //  jConfirm('¿Está seguro que desea eliminar el registro?', 'Eliminar', function (r) {
      //      if (r) {
             //   $(btn).parents('tr').fadeOut(function () {
                   // $(this).remove();
            //    });
       //     }
            //});
        var id = $(row).data("id");
     //   var cod = JSON.stringify({ id: id });
        jConfirm('¿Desea generar el comprobante de retención este momento?', 'Pregunta', function (r) {
            if (r)
                GeneraRetencion(id);
            else
                location.reload();
        });    
     
    }
    else
        CleanRow();
 //   CalculaTotales();
//    StopPropagation();
}




function GeneraRetencion(cod) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = cod;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer("ws/Metodos.asmx/GeneraRetencion", jsonText, 8);

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
