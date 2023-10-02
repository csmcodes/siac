var LoadObligacion = function () {

    return {

        //main function to initiate the module
        init: function () {

            GetLastCargas();
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
    
    GetLastCargas();
    //$("#tdlistado>tbody").html(result);
    

}


function ImportXML() {
    var obj = JsonObj();
    XmlFileImport("txtfiles", obj);
}
var showing = false;

function ShowCargaElectronico(data) {

    var dialogcarga = bootbox.dialog({
        message: "<div class='resultadoelectronico'></div>",
        title: "Carga Electrónico",
        buttons: {
            cancelar:
            {
                label: "Cancelar",
                className: "btn-default",
                callback: function () {
                    CloseShow();
                }
            }
        },
        onEscape: function () {
            CloseShow();
        }
    });
    dialogcarga.bind('shown.bs.modal', function () {
        showing = true;
        GetCargaElectronico(data["id"]);

    });
}

function CloseShow() {
    showing = false;
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
    var electronico = obj[0];
    var html = obj[1];    
    $(".resultadoelectronico").html(html);

    if (electronico["eca_estado"] == 0 && showing) {
        setTimeout(() => {
            GetCargaElectronico(electronico["eca_id"])
        }, "500")

    }

}



function GetCargaDetalleOK(data) {
    var obj = JsonObj();
    obj["eca_empresa"] = GetOnlineCompany();
    obj["eca_id"] = data["id"];
    obj["tipo"] = "OK";
    CallServerMethods(webservice + "GetDetalleElectronico", JsonObjString(obj), "Detalle", false);
}

function GetCargaDetalleERROR(data) {
    var obj = JsonObj();
    obj["eca_empresa"] = GetOnlineCompany();
    obj["eca_id"] = data["id"];
    obj["tipo"] = "ERROR";
    CallServerMethods(webservice + "GetDetalleElectronico", JsonObjString(obj), "Detalle", false);
}



function GetCargaDetalleResult(data) {

    bootbox.dialog({
        message: data.d,
        size: "large",
        title: "Carga Electrónico",
        buttons: {
            cancelar:
            {
                label: "Cancelar",
                className: "btn-default",
            }
        }
    });
}



function AnulateCarga(data) {
    if (confirm("Esta seguro que desea anular la carga de elctronicos?")) {
        var obj = JsonObj();
        obj["eca_empresa"] = GetOnlineCompany();
        obj["eca_id"] = data["id"];
        CallServerMethods(webservice + "AnulateCargaElectronico", JsonObjString(obj), "Anulate", false);
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
        if (retorno == "LastCargas") {
            SetLastCargas(data.d)
        }
        if (retorno == "Detalle") {
            GetCargaDetalleResult(data)
        }
        if (retorno == "Anulate") {
            alert(data.d);
        }

    }
}


function GetLastCargas() {
    var obj = JsonObj();
    //obj["top"]=50
    CallServerMethods(webservice + "GetLastCargas", JsonObjString(obj), "LastCargas");
}

function SetLastCargas(html) {
    $("#tdlistado").html(html);    
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

