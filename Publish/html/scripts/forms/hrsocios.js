///Interfaz para registrar cobros
///util para el registro rapido, y para el cobro por parte del socio.
///Desarrollado por: CSM
///Fecha: 16-06-2020

var HRSocios = function () {

    return {

        //main function to initiate the module
        init: function () {

            GetForm();           
        }

    };

}();

jQuery(document).ready(function () {
    HRSocios.init();
});

function ServerResult(data, retorno) {
    if (data != "") {
        if (retorno == "Form") {
            SetFilters(data.d)

        }
        if (retorno == "Data") {
            $("#tdlistado>tbody").html(data.d);            
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
        if (retorno == "GetVehiculos") {
            $("#cmbvehiculo_f").html(data.d);
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

    CallServerMethods(webservice + "GetFormHRSocios", JsonObjString(JsonObj()), "Form");
}

function SetFilters(html) {
    $(".contenido").html(html);
    SetPlugins();        
    $("#cmbalmacen_f").on("change", GetPuntoVenta);
    $("#cmbsocio_f").on("change", GetVehiculosSocio);
    $("#cmbsocio_f").trigger("change");
    $("#btnsearch").on("click", GetData);
    GetData();

}

function GetPuntoVenta(event) {
    var obj = JsonObj();
    obj["pve_empresa"] = GetOnlineCompany();
    obj["pve_almacen"] = $(this).val();
    CallServerMethods(webservice + "GetPuntosAlmacen", JsonObjString(obj), "GetPuntos");
}


function GetVehiculosSocio(event) {
    var obj = JsonObj();
    obj["veh_empresa"] = GetOnlineCompany();
    obj["veh_duenio"] = $(this).val();
    CallServerMethods(webservice + "GetVehiculosSocio", JsonObjString(obj), "GetVehiculos");
}

function GetData() {

    $("#tdlistado>tbody").html("");
    var obj = JsonObj();
    obj["empresa"] = GetOnlineCompany();
    obj["almacen"] = $("#cmbalmacen_f").val();
    obj["pventa"] = $("#cmbpventa_f").val();
    obj["numero"] = $("#txtnumero_f").val();
    obj["ruta"] = $("#cmbruta_f").val();        
    obj["desde"] = $("#txtfechadesde").val();
    obj["hasta"] = $("#txtfechahasta").val();    
    obj["vehiculo"] = $("#cmbvehiculo_f").val();        
    //obj["estado"] = "2"; //Solo mayorizados
    obj["estadocobro"] = $("#cmbestado_f").val();
    obj["socio"] = $("#cmbsocio_f").val();
    obj["chofer"] = $("#txtpersona_f").val();    
    
    CallServerMethods(webservice + "GetDataHRSocios", JsonObjString(obj), "Data");

}





function LoadReporte() {

    //if (ValidaReporte()) {

    var obj = {};


    obj["empresa"] = GetOnlineCompany();
    obj["parameter1"] = $("#txtfechadesde").val();
    obj["parameter2"] = $("#txtfechahasta").val();


    obj["parameter3"] = $("#cmbalmacen_f").val();
    obj["parameter4"] = $("#cmbpventa_f").val();
    obj["parameter5"] = $("#cmbbodega_f").val();

    obj["parameter6"] = $("#cmbalmacen_f option:selected").text();
    obj["parameter7"] = $("#cmbpventa_f option:selected").text();
    obj["parameter8"] = $("#cmbbodega_f option:selected").text();

    var tipos;
    if ($("#cmbtipodoc_f").val() != null && $("#cmbtipodoc_f").val() != undefined)
        tipos = $("#cmbtipodoc_f").val().join("|");
    obj["parameter9"] = tipos;

    var estados;
    if ($("#cmbestado_f").val() != null && $("#cmbestado_f").val() != undefined)
        estados = $("#cmbestado_f").val().join("|");
    obj["parameter10"] = estados;


    obj["parameter11"] = $("#txtnumero_f").val();
    obj["parameter12"] = $("#txtpersona_f").val();
    obj["parameter13"] = $("#txtdetalle_f").val();
    obj["parameter14"] = $("#txtplaca_f").val();



    var usuarios;
    if ($("#cmbusuario_f").val() != null && $("#cmbusuario_f").val() != undefined)
        usuarios = $("#cmbusuario_f").val().join("|");
    obj["parameter15"] = usuarios;

    obj["parameter16"] = GetOnlineUser();





    var querystring = $.param(obj);

    var url = "wfListaComprobantesPrint.aspx?report=CDC&" + querystring;


    var url = "../../reports/wfReportPrint.aspx?report=LISTADO&" + querystring;
    var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=yes,scrollbars=yes";
    window.open(url, "Reporte", feautures);


}


function Detalle(codigo) {
    var obj = JsonObj();
    obj["com_empresa"] = GetOnlineCompany();
    obj["com_codigo"] = codigo;
    window.location = "hrdetsocios.html?codigo=" + codigo;
}