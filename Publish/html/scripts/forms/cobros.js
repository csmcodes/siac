///Interfaz para registrar cobros
///util para el registro rapido, y para el cobro por parte del socio.
///Desarrollado por: CSM
///Fecha: 16-06-2020

var Cobros = function () {

    return {

        //main function to initiate the module
        init: function () {

            GetForm();
            SetListadoHead();

        }

    };

}();

jQuery(document).ready(function () {
    Cobros.init();
});

function ServerResult(data, retorno) {
    if (data != "") {
        if (retorno == "Form") {
            SetFilters(data.d)

        }
        if (retorno == "Data") {
            $("#tdlistado>tbody").html(data.d);
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

    CallServerMethods(webservice + "GetFormCobros", JsonObjString(JsonObj()), "Form");
}

function SetFilters(html) {
    $(".contenido").html(html);
    SetPlugins();    
    

    $("#cmbalmacen_f").on("change", { id: "cmbalmacen_f" }, GetPuntoVenta);
    $("#btnsearch").on("click", GetData);
    GetData();

}

function GetPuntoVenta(event) {
    var obj = JsonObj();
    obj["pve_empresa"] = GetOnlineCompany();
    obj["pve_almacen"] = $(this).val();
    CallServerMethods(webservice + "GetPuntosAlmacen", JsonObjString(obj), "GetPuntos");
}


function GetData() {

    $("#tdlistado>tbody").html("");
    var obj = JsonObj();
    obj["empresa"] = GetOnlineCompany();
    obj["almacen"] = $("#cmbalmacen_f").val();
    obj["pventa"] = $("#cmbpventa_f").val();
    obj["numero"] = $("#txtnumero_f").val();
    //obj["bodega"] = $("#cmbbodega_f").val();
    obj["desde"] = $("#txtfechadesde").val();
    obj["hasta"] = $("#txtfechahasta").val();    
    obj["estado"] = "2"; //Solo mayorizados
    obj["estadocobro"] = $("#cmbestado_f").val();
    obj["socio"] = $("#cmbsocio_f").val();
    obj["nombres"] = $("#txtpersona_f").val();
    //obj["detalle"] = $("#txtdetalle_f").val();
    //obj["placa"] = $("#txtplaca_f").val();

    CallServerMethods(webservice + "GetDataCobros", JsonObjString(obj), "Data");

}


function Cobrar(codigo) {
    var obj = JsonObj();
    obj["com_empresa"] = GetOnlineCompany();
    obj["com_codigo"] = codigo;
    EditCobro(obj);
}


function Detalle(codigo) {
    var obj = JsonObj();
    obj["com_empresa"] = GetOnlineCompany();
    obj["com_codigo"] = codigo;
    DetalleCobro(obj);
}


function Opciones(event) {
    var opcion = event.target;
    var cod = $(opcion.parentNode.parentNode.parentNode.parentNode.parentNode).data("codigo");
    //alert(opcion.id+" " + cod);
    if (opcion.id == "INF")
        Informacion(cod);//Implementado en bootbox.js
    if (opcion.id == "ELECTR")
        Electronico(cod);//Implemenado en server.js
    if (opcion.id == "COBRO")
        Cobrar(cod); // Implementado en server.js
    if (opcion.id == "PAGO")
        Pagar(cod); // Implementado en server.js
    if (opcion.id == "ANUL")
        CallAnular(cod) // Implementado en server.js
    if (opcion.id == "RET")
        Retencion(cod); // Implementado en server.js
    if (opcion.id == "GUIREM")
        GuiaRemision(cod); // Implementado en server.js
    if (opcion.id == "MODIF")
        CallModificar(cod);// Implementado en server.js



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