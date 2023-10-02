var Cuadres = function () {

    return {

        //main function to initiate the module
        init: function () {

            GetForm();
        }

    };

}();

jQuery(document).ready(function () {
    Cuadres.init();
});



function ServerResult(data, retorno) {
    if (data != "") {
        if (retorno == "Form") {
            SetFilters(data.d)

        }
        if (retorno == "Data") {
            var obj = $.parseJSON(data.d);
            $("#lblmensaje").html(obj[0]);
            $("#tdlistado>tbody").html(obj[1]);
            $(".opciones").on("click", Opciones);
        }
        if (retorno == "Edit") {
            var obj = $.parseJSON(data.d);
            BootBoxDialog(obj[0], obj[1], "medium");
            $("#cmbagencia_pro").on("change", { id: "cmbgrupo_pro" }, GetGrupos);

            $("#cmbdepartamento_pro").on("change", GetPersonas);

            $("#cmbagencia_pro").trigger('change');

        }
        if (retorno == "Save") {
            GetListadoBody();
        }
        if (retorno == "Remove") {
            if (data.d != "OK")
                BootBoxAlert(data.d);
            else
                GetListadoBody();
        }
        if (retorno == "GetPuntos") {
            $("#cmbpventa_f").html(data.d);
        }
        if (retorno == "GetPersonas") {
            var obj = $.parseJSON(data.d);
            $("#" + obj[0]).replaceWith(obj[1]);
        }


        if (retorno == "EditDetalle") {
            var obj = $.parseJSON(data.d);
            BootBoxDialog(obj[0], obj[1], "large");
            //DETALLE DE PROGRAMACION

        }
        if (retorno == "GetForm")
            CallFormularioResult(data);
        if (retorno == "Electronic")
            alert(data.d);
    }
}


function GetForm() {

    CallServerMethods(webservice + "GetFormCuadres", JsonObjString(JsonObj()), "Form");
}

function SetFilters(html) {
    $(".contenido").html(html);
    SetPlugins();
    $("#btnsearch").on("click", GetData);
    

}


function GetData() {

    $("#tdlistado>tbody").html("");
    var obj = JsonObj();
    obj["desde"] = $("#txtfechadesde").val();
    obj["hasta"] = $("#txtfechahasta").val();
   
    CallServerMethods(webservice + "GetComprobantesCuadre", JsonObjString(obj), "Data");

}


///////////////////////////ELECTRONICOS///////////////////

function GenerarElectronico(cod) {
    var obj = JsonObj();
    obj["com_empresa"] = obj.empresa;
    obj["com_codigo"] = cod;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "GenerarElectronico", jsonText, "ELEC");

}

function GenerarElectronicoResult(data) {
    if (data != "") {
        if (data.d == "ok") {
            BootBoxAlert("Se ha generado y enviado el comprobante electrónico correctamente");
        }
        else {
            BootBoxAlert("ERROR: Se ha producido un error al generar el comprobante de electrónico...");

        }
    }
}


function CallFormulario(id) {
    var obj = JsonObj();
    if (id != null) {
        obj["com_empresa"] = obj.empresa;
        obj["com_codigo"] = parseInt(id);
    }
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "GetFormulario", jsonText, "GetForm");
}

function CallFormularioResult(data) {
    if (data != "") {
        window.open("../"+data.d,"_blank");
    };
}
