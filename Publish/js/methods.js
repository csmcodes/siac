//Descripción:      Contiene los metodos para traer información de la BD
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Abril  2014
//2014. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

//Funciona que invoca al servidor mediante JSON



var webservice = "ws/Metodos.asmx/";
var call;

function CallServerMethods(strurl, strdata, retorno) {


    call = $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            try {
                ServerResult(data, retorno);
            }
            catch (err) {
                alert("Función ServerResult no definida");
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            if (textStatus != "abort") {
                var errorData = $.parseJSON(XMLHttpRequest.responseText);
                jQuery.alerts.dialogClass = 'alert-danger';
                jAlert(errorData.Message, 'Error', function () {
                    jQuery.alerts.dialogClass = null; // reset to default
                });
            }
        }

    })
}


function CancelCall() {
    call.abort();
    call = null;
    HideLoading();
}