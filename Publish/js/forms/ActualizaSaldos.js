var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfActualizaSaldos.aspx";
var menuoption = "ActualizaSaldos";

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
        ActualizaResult(data);
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
    //$("#cmbUSUARIO").on("change", LoadReporte);
    // $("#txtDESDE").on("change", LoadReporte);
    // $("#txtHASTA").on("change", LoadReporte);


}


function Actualiza() {

    var obj = {};
    obj["empresa"] = empresasigned["emp_codigo"];
    obj["almacen"] = $("#cmbALMACEN").val();
    obj["pventa"] = $("#cmbPVENTA").val();
    obj["periodo"] = $("#txtPERIODO").val();
    obj["mes"] = $("#txtMES").val();
    obj["cuenta"] = $("#txtCUENTA").val();
    obj["usuario"] = usuariosigned["usr_id"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice+ "ActualizaSaldos", jsonText, 1);

}




function ActualizaResult(data) {
    if (data != "") {
        if (data.d == "OK") {
            jQuery.alerts.dialogClass = 'alert-success';
            jAlert('Saldos actualizados correctamente...', 'Éxito', function () {
            });

        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al actualizar saldos...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }        
        
    }
}

