var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfCierres.aspx";
var menuoption = "Cierres";

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
        CerrarResult(data);
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
    }
    SetForm(); //Depende de cada js.        
}

function SetForm() {

    $(".fecha").datepicker({
        dateFormat: "dd/mm/yy"
    });


    SetAutocompleteById("txtCUENTA");
}

function Cerrar()
{
    var obj = {};

    obj["empresa"] = empresasigned["emp_codigo"];
    obj["periodo"] = $("#txtPERIODO").val();
    obj["cierre"] = $("#txtCODCUENTA").val();
    obj["fecha"] = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHA").val());
    obj["usuario"] = usuariosigned["usr_id"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "CierreAutomatico", jsonText, 1);

}

function ConfirmaCerrar() {
    jConfirm('¿Está seguro que desea cerrar las cuentas del periodo indicado?', 'Pregunta', function (r) {
        Cerrar();
    });

}




function CerrarResult(data) {
    if (data != "") {
        if (data.d == "ERROR") {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al ejecutar Cierre Automático...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
            
        }
        else {

            jQuery.alerts.dialogClass = 'alert-success';
            jAlert('Cierre Automático ejecutado correctamente...\n ' + data.d, 'Éxito', function () {
            });

            
        }

    }
}


function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCUENTA") {
        return {
            label: item.cue_id + "," + item.cue_nombre,
            value: item.cue_id + " " + item.cue_nombre,
            info: item
        }
    }

}

function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCUENTA") {
        $("#txtCODCUENTA").val(item.info.cue_codigo);
      
    }

 
}
