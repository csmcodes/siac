//Archivo:          EstadoCuenta.js
//Descripción:      Contiene las funciones propias de la interfaz de estado de cuentas de clientes y proveedores
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Abril 2017
//2017. SIAC Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
 var formname = "wfEstadoCuenta.aspx";
var menuoption = "EstadoCuenta";


function StopPropagation(event) {
    if (!event) var event = window.event;
    event.cancelBubble = true;
    if (event.stopPropagation) event.stopPropagation();
}


//Codigo ejecutado cuando el document esta listo
$(document).ready(function () {
   
    $('body').css('background', 'transparent');
    LoadCabecera();
    $("#printestado").on("click", Print);
  
});


function GetCabeceraComprobante(debcre) {
    if (debcre >= 0)
    {
        $("#txtDEBCRE").val(debcre);
        LoadDetalle();
    }   
}




//Funciona que invoca al servidor mediante JSON
function CallServer(strurl, strdata, retorno) {
    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (retorno == "CAB")
                LoadCabeceraResult(data);
            if (retorno == "DET")
                LoadDetalleResult(data);
            if (retorno == 2)
                LoadFormResult(data);
            if (retorno == 3)
                ShowObjResult(data);
            if (retorno == 4)
                SaveObjResult(data);
            if (retorno == 5)
                DeleteObjsResult(data);
            if (retorno == 6)
                LoadPuntoventaResult(data);
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
function LoadCabecera() {

    var obj = {};
    obj["per_empresa"] = empresasigned["emp_codigo"];
    obj["per_codigo"] = $("#txtcodigoper").val();
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetCabecera", jsonText, "CAB");
}
function LoadCabeceraResult(data) {
    if (data != "") {
        $('#comcabeceracontent').html(data.d);
        $("#cmbNOMBRE").on("change", LoadDetalle);
        $("#cmbALMACEN").on("change", LoadDetalle);
        $("#txtINICIO").on("change", LoadDetalle);
        $("#txtFIN").on("change", LoadDetalle);
        $("#cmbORDEN").on("change", LoadDetalle);
        SetAutocompleteById("cmbNOMBRE");
        //GetCabeceraComprobante($("#txtdebcre").val()); //FUNCION DEFINIDA EN FUNCTIONS.JS
    }
    $('.fecha').datepicker({ dateFormat: 'dd/mm/yy' }).val();


}
function LoadDetalle() {
    var obj = {};
    obj["per_empresa"] = empresasigned["emp_codigo"];
    obj["per_codigo"] = $("#txtcodigoper").val();

    obj["alm_codigo"] = $("#cmbALMACEN").val();

    obj["inicio"] = $("#txtINICIO").val();
    obj["fin"] = $("#txtFIN").val();
    obj["orden"] = $("#cmbORDEN").val();
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDetalle", jsonText, "DET");
}

function LoadDetalleResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        $("#txtSALDOINI").val(obj[0]);
        $("#txtSALDOFIN").val(obj[1]);
        $('#comdetallecontent').html(obj[2]);
    }

}


function SetAutoCompleteObj(idobj, item) {

    if (idobj == "cmbNOMBRE") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_razon, //item.per_apellidos + " " + item.per_nombres,
            info: item
        }
    }
}

function GetAutoCompleteObj(idobj, item) {
    if (idobj == "cmbNOMBRE") {
        //$("#cmbNOMBRE").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#cmbNOMBRE").val(item.info.per_razon);
        $("#txtcodigoper").val(item.info.per_codigo);
        LoadDetalle();

    }   
}





function LoadForm() {
    CallServer(formname + "/GetForm", "{}", 2);
}
function LoadFormResult(data) {
    if (data != "") {
        $('#formmodal').html(data.d);
    }
    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha
    $("#cmbALMACEN").on("change", LoadPuntoventa);
    LoadPuntoventa();
}


function LoadPuntoventa() {
    var obj = {};
    obj["almacen"] = $("#cmbALMACEN").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetPuntoventa", jsonText, 6);
}

function LoadPuntoventaResult(data) {
    if (data != "") {
        $("#cmbPUNTODEVENTA").replaceWith(data.d);
        if ($("#cmbPUNTODEVENTA option").length == 0)
            $("#cmbPUNTODEVENTA").attr("disabled", true);
        else
            $("#cmbPUNTODEVENTA").attr("disabled", false);
    }
}


function Print() {


    var persona = $("#txtcodigoper").val();
    var almacen = $("#cmbALMACEN").val();
    var desde = $("#txtINICIO").val();
    var hasta = $("#txtFIN").val();
    var orden = $("#cmbORDEN").val();
    var nombres = $("#cmbNOMBRE").val();
    var debcre = $("#txtdebcre").val();

    var url = "./reports/wfReportPrint.aspx?report=ESTADOCUENTA&empresa=" + parseInt(empresasigned["emp_codigo"]) + "&parameter1=" + desde + "&parameter2=" + hasta + "&parameter3=" + almacen + "&parameter4=" + persona + "&parameter5=" + nombres + "&parameter6=" + debcre + "&parameter7=" + orden;
    var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=yes,scrollbars=yes";
    window.open(url, "Reporte", feautures);

}



/****************************FUNCIONES BASICAS*******************************/
function GetCheckValue(obj) {
    if ($(obj).length > 0) {
        if ($(obj)[0].checked)
            return 1;
        else
            return 0;
    }
    //return null;
}
function SetCheckValue(value) {
    if (value != null) {
        if (value.toString() == "1")
            return true;
    }
    return false;
    //return null;
}
function GetDateValue(value) {
    if (value != null) {
        var date = new Date(parseInt(value.substr(6)));
        return date.toLocaleString();
        //return eval(value.slice(1, -1));
    }
    return "";
}
function Edit(obj) {
    //var id = $(obj).data("id");
  
    Select(obj);
}

function CleanSelect() {
    var rows = $("#detallebody tr");
    $(rows).css("background-color", "");
}
function ShowObj(obj) {
    var id = $(obj).data("id");
    var jsonText = JSON.stringify({ id: id });
    CallServer(formname + "/GetObject", jsonText, 3)
}
function DisableControls(disable) {
    $("#cmbUSUARIO").attr("disabled", disable);
}
function ShowObjResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        SetJSONObject(obj); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM
        DisableControls(true);
        $("#edit").trigger('click'); //MUESTRA EL DIV CON EL FORM
        CleanSelect();
    }
}

function Select(obj) {
    var id = $(obj).data("id");
    var jsonText = JSON.stringify({ id: id });
    window.location = "wfDEstadoCuenta.aspx?debcre=" + $("#txtdebcre").val() + "&codpersona=" + id.per_codigo + "&codalmacen=" + parseInt($("#cmbALMACEN").val()) +"&fechacort=" + $("#cmbCORTE").val();   
}

function GetJSONObject() {
    var obj = {};
    obj["per_id"] = $("#cmbUSUARIO").val();
    obj["per_nombres"] = $("#txtUSUARIO_key").val();
    obj["per_apellidos"] = parseInt($("#cmbEMPRESA").val());
    obj["per_cupo"] = GetCheckValue($("#chkESTADO"));

    obj["valor"] = $("#cmbALMACEN").val();
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
    $("#txtEMPRESA_key").val(obj["uxe_empresa_key"]);
    $("#cmbUSUARIO").val(obj["uxe_usuario"]);
    $("#txtUSUARIO_key").val(obj["uxe_usuario_key"]);
    $("#cmbALMACEN").val(obj["uxe_almacen"]);   
    $("#txtEMPRESAPUNTOVENTA").val( obj["uxe_empresapuntoventa"]);
    $("#chkESTADO").prop("checked", SetCheckValue(obj["uxe_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Modificado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));
    LoadPuntoventa();
    $("#cmbPUNTODEVENTA").val(obj["uxe_puntoventa"]);
}

function GetJSONSearch() {
    var obj = {};
    obj["uxe_empresa"] = parseInt($("#cmbEMPRESA").val());    
    obj["uxe_usuario"] = $("#txtUSUARIO_S").val();
    if ($("#cmbESTADO_S").val() != "")
        obj["uxe_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function CleanForm() {

    $("#cmbNOMBRE").val("");
    $("#cmbALMACEN").val("");
    $("#cmbCORTE").val("");
  
    $("#txtEMPRESA_key").val(""); 
    $("#cmbUSUARIO").val("");
    $("#txtUSUARIO_key").val("");       
    $("#chkESTADO").prop("checked", true);
}

function ClearValidate() {
    var controles = $("#datoscontent").find('[data-obligatorio="True"]');
    $("#datoscontent").find('[data-obligatorio="True"]').each(function () {
        $(this.parentNode).removeClass('obligatorio')
        $(this.parentNode).children(".obligatorio").remove();
    });
}
function ValidateForm() {
    var retorno = true;
    /* var controles = $("#datoscontent").find('[data-obligatorio="True"]');
    $("#datoscontent").find('[data-obligatorio="True"]').each(function () {
    $(this.parentNode).removeClass('obligatorio')
    $(this.parentNode).children(".obligatorio").remove();
    if ($(this).val() == "") {
    retorno = false;
    var padre = $(this.parentNode);
    $(this.parentNode).append("<span class='obligatorio'>! Obligatorio</span>");
    $(this.parentNode).addClass('obligatorio')
    }

    });*/
    return retorno;
}
function SaveObj() {
    if (ValidateForm()) {
        var jsonText = GetJSONObject(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
        CallServer(formname + "/SaveObject", jsonText, 4);
    }

}
function SaveObjResult(data) {
    if (data != "") {
        if (data.d != "OK" && data.d.toString() != "ERROR") {
            if (selectobj != null) {
                jQuery.jGrowl("Registro actualizado con éxito");
                $(selectobj).replaceWith(data.d);
            }
            else {
                jQuery.jGrowl("Registro ingresado con éxito");
                $('#detallebody').append(data.d);
            }
        }
        /*else {
        CleanForm(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM  
        ReloadData();
        }*/
    }
}
function AddObj() {
    ClearValidate();
    DisableControls(false);
    $("#edit").trigger('click');
    selectobj = null;
    CleanForm(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
}
function DeleteObjs() {
    jConfirm('¿Está seguro que desea eliminar los registros seleccionados?', 'Eliminar', function (r) {
        if (r) {
            var objetos = [];
            $("#detallebody").find('tr').each(function () {
                if ($(this).css("background-color") != "transparent") {
                    objetos.push($(this).data("id"));
                }
            });
            var jsonText = JSON.stringify({ objetos: objetos });
            CallServer(formname + "/DeleteObjects", jsonText, 5)
        }
    });
}
function DeleteObjsResult(data) {
    if (data != "") {
        if (data.d == "OK") {
            $("#detallebody").find('tr').each(function () {
                if ($(this).css("background-color") != "transparent") {
                    $(this).remove();
                }
            });
        }
    }
}