var CerrarHR = function () {

    return {

        //main function to initiate the module
        init: function () {

            GetFilters();
            

        }

    };

}();

jQuery(document).ready(function () {
    CerrarHR.init();
});

function ServerResult(data, retorno) {
    if (data != "") {
        if (retorno == "Filter") {
            SetFilters(data.d)

        }
        if (retorno == "List") {
            $("#tdlistado>tbody").html(data.d);
        }
        if (retorno == "Edit") {
            var obj = $.parseJSON(data.d);
            BootBoxDialog(obj[0], obj[1], "medium");
            $("#cmbagencia_pro").on("change", { id: "cmbgrupo_pro" }, GetGrupos);

            $("#cmbdepartamento_pro").on("change", GetPersonas);

            $("#cmbagencia_pro").trigger('change');

        }
        if (retorno == "GetPuntos") {
            $("#cmbpventa_f").html(data.d);
        }


        if (retorno == "Cerrar") {
            var lst = $.parseJSON(data.d);

            for (var i = 0; i < lst.length; i++) {

                var comp = lst[i];
                var row = $("#tdlistado").find("tr[data-codigo='" + comp["com_codigo"] + "']");
                if (row != null) {
                    if (comp["com_estado"] == 2) {

                        row.find("#btnclose").replaceWith("<a id='btnopen' class='btn btn-circle  btn-sm blue' onclick='CambiarEstado(" + comp["com_codigo"] + ",1)'><i class='fa fa fa-check'></i></a>");
                        row.find(".estadonom").html("MAYORIZADO");
                    }
                    if (comp["com_estado"] == 1) {
                        row.find("#btnopen").replaceWith("<a id='btnclose' class='btn btn-circle  btn-sm red' onclick='CambiarEstado(" + comp["com_codigo"] + ",2)'><i class='fa fa fa-close'></i></a>");
                        row.find(".estadonom").html("GRABADO");
                    }
                }
                

            }
            
        }
        if (retorno == "Remove") {
            if (data.d != "OK")
                BootBoxAlert(data.d);
            else
                GetListadoBody();
        }
       

    }
}


function GetFilters() {

    CallServerMethods(webservice + "GetFiltrosCerrarHR", JsonObjString(JsonObj()), "Filter");
}

function SetFilters(html) {
    $(".booking-search").html(html);
    SetPlugins();
    $('#cmbestado_f').select2({
        placeholder: "Seleccione estado...",
        allowClear: true,
        language: "es"
    });
    
    $("#cmbalmacen_f").on("change", { id: "cmbalmacen_f" }, GetPuntoVenta);
    //$("#txtusuario_f").on("change", GetListadoBody);
    $("#btnsearch").on("click", GetListadoBody);
    //GetListadoBody();

}

function GetPuntoVenta(event) {
    var obj = JsonObj();
    obj["pve_empresa"] = GetOnlineCompany();
    obj["pve_almacen"] = $(this).val();

    CallServerMethods(webservice + "GetPuntosAlmacen", JsonObjString(obj), "GetPuntos");


}

function SetListadoHead() {
    var htmlhead = "<tr> " +
        "<th style='width:5%'></th> " +
        "<th style='width:10%'>Comprobante</th> " +
        "<th style='width:10%'>Fecha</th> " +
        "<th style='width:20%'>Persona</th> " +
        "<th style='width:20%'>Concepto</th> " +
        "<th style='width:10%'>Datos</th> " +
        "<th style='width:20%'>Electrónico</th> " +
        "<th style='width:5%'>Total</th> " +
        "</tr> ";
    $("#tdlistado>thead").html(htmlhead);
}

function GetListadoBody() {

    $("#tdlistado>tbody").html("");
    var obj = JsonObj();
    obj["empresa"] = GetOnlineCompany();
    obj["almacen"] = $("#cmbalmacen_f").val();
    obj["pventa"] = $("#cmbpventa_f").val();
    obj["numero"] = $("#txtnumero_f").val();
   

    obj["desde"] = $("#txtfechadesde").val();
    obj["hasta"] = $("#txtfechahasta").val();
    //obj["tipo"] = $("#cmbtipodoc_f").val();
    obj["estados"] = $("#cmbestado_f").val();
    //obj["usuario"] = $("#cmbusuario_f").val();
    obj["persona"] = $("#txtpersona_f").val();
    //obj["detalle"] = $("#txtdetalle_f").val();
    //obj["placa"] = $("#txtplaca_f").val();

    CallServerMethods(webservice + "GetListadoCerrarHR", JsonObjString(obj), "List");

}


function CambiarEstado(codigo,estado) {
    var obj = JsonObj();
    obj["com_empresa"] = GetOnlineCompany();
    obj["com_codigo"] = codigo;
    obj["estado"] = estado;

    obj["empresa"] = GetOnlineCompany();
    obj["almacen"] = $("#cmbalmacen_f").val();
    obj["pventa"] = $("#cmbpventa_f").val();
    obj["numero"] = $("#txtnumero_f").val();

    obj["desde"] = $("#txtfechadesde").val();
    obj["hasta"] = $("#txtfechahasta").val();
    obj["estados"] = $("#cmbestado_f").val();
    obj["persona"] = $("#txtpersona_f").val();


    CallServerMethods(webservice + "CambiarEstadoHR", JsonObjString(obj), "Cerrar");
}






