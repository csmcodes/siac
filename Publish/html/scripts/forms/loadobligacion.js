var LoadObligacion = function () {

    return {

        //main function to initiate the module
        init: function () {

            //GetFilters();
        }

    };

}();

jQuery(document).ready(function () {
    LoadObligacion.init();
});



function LoadXML()
{
    var obj = JsonObj();
    XmlFileUpload("txtfiles", obj);
}



function XmlUploadResult(result) {
    
    //GetCargaElectronico(result);
    $("#tdlistado>tbody").html(result);
    

}


function ImportXML() {
    var obj = JsonObj();
    XmlFileImport("txtfiles", obj);
}

function GetCargaElectronico(id)
{
    var obj = JsonObj();
    obj["eca_empresa"] = GetOnlineCompany();
    obj["eca_id"] = id;
    CallServerMethods(webservice + "GetCargaElectronico", JsonObjString(obj), "Carga",false);
}

function SetCargaElectronico(obj)
{

    var res = "<div> Carga: " + obj["eca_id"] + " Inicio: " + obj["eca_inicio"] + " Fin: " + obj["eca_fin"]+"</div>";
    res += "<div> Registros: " + obj["eca_registros"] + "<br> Cargados:" + obj["eca_descargados"] + " <br> Creados:" + obj["eca_creados"] + "<br>Errores:" + obj["eca_error"] + "</div>";

    $(".resultados").html(res);

    if (obj["eca_estado"] == 0) {
        setTimeout(() => {
            GetCargaElectronico(obj["eca_id"])
        }, "1000")

    }

}



function ServerResult(data, retorno) {
    if (data != "") {
        if (retorno == "Filter") {
            SetFilters(data.d)
        }
        if (retorno == "List") {
            $("#tdlistado>tbody").html(data.d);
        }
        if (retorno == "GetPuntos") {
            $("#cmbpventa_rep").html(data.d);
        }
        if (retorno == "Carga") {
            var obj = $.parseJSON(data.d);
            SetCargaElectronico(obj);
        }

    }
}


function GetFilters() {
    var obj = JsonObj();
    CallServerMethods(webservice + "GetLoadObligacion", JsonObjString(obj), "Filter");
}

function SetFilters(html) {
    $(".booking-search").html(html);
    SetPlugins();
    //$("#cmbalmacen_rep").on("change", { id: "cmbalmacen_rep" }, GetPuntoVenta);
}

function GetPuntoVenta(event) {
    var obj = JsonObj();
    obj["pve_empresa"] = GetOnlineCompany();
    obj["pve_almacen"] = $(this).val();

    CallServerMethods(webservice + "GetPuntosAlmacen", JsonObjString(obj), "GetPuntos");


}



function Report() {


    var report = $("#cmbreporte_rep").val();
    var desde = $("#txtdesde_rep").val(); //.datepicker("getDate");
    var horadesde = $("#txtdesdehora_rep").val();
    if (horadesde != "")
        desde += " " + horadesde;

    var hasta = $("#txthasta_rep").val(); //.datepicker("getDate");
    var horahasta = $("#txthastahora_rep").val();
    if (horahasta != "")
        hasta += " " + horahasta;


    var almacen = $("#cmbalmacen_rep").val();
    var pventa = $("#cmbpventa_rep").val();
    if (pventa == null)
        pventa = "";
    var stralmacen = $("#cmbalmacen_rep option:selected").text();
    var strpventa = $("#cmbpventa_rep option:selected").text();
    var usr = GetOnlineUser();
    var tipo = $("#cmbtipo_rep").val();

    var url = "../../reports/wfReportPrint.aspx?report=" + report + "&empresa=" + GetOnlineCompany() + "&parameter1=" + desde + "&parameter2=" + hasta + "&parameter3=" + almacen + "&parameter4=" + pventa + "&parameter5=" + stralmacen + "&parameter6=" + strpventa + "&parameter7=" + usr + "&parameter8=" + tipo;
    var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=yes,scrollbars=yes";
    window.open(url, "Reporte", feautures);


}