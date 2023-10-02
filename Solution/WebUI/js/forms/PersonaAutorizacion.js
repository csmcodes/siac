//Archivo:          PersonaAutorizacion.js
//Descripción:      Contiene las funciones comunes para la interfaz de AdmUsuario
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            junio 2014
//2014. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfPersonaAutorizacion.aspx";
var menuoption = "PersonaAutorizacion";

function StopPropagation(event) {
    if (!event) var event = window.event;
    event.cancelBubble = true;
    if (event.stopPropagation) event.stopPropagation();
}

//Codigo ejecutado cuando el document esta listo
$(document).ready(function () {
    $('body').css('background', 'transparent');
    LoadCabecera();

    //  recuperarDato()
});


//Funciona que invoca al servidor mediante JSON
function CallServer(strurl, strdata, retorno) {
    ClearValidate();
    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (retorno == 0)
                LoadCabeceraResult(data);
            if (retorno == 1)
                LoadDetalleResult(data);
            if (retorno == 2)
                LoadDetalleDataResult(data);
            if (retorno == 3)
                ShowObjResult(data);
            if (retorno == 4)
                SaveObjResult(data);
            if (retorno == 5)
                RemoveObjectsResult(data);
            if (retorno == 6)
                LoadPuntoVentaResult2(data);
            if (retorno == 7)
                LoadCabeceraResultUsr(data);
            if (retorno == 8)
                LoadFormResult(data);
            if (retorno == 9)
                LoadSiglaResult(data);
            if (retorno == 10)
                SaveObjResultPea(data);
            if (retorno == 11)
                DeleteObjsResult(data);


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

function LoadCabecera(codigo) {


    var jsonText = JSON.stringify({});
    CallServer(formname + "/GetCabecera", jsonText, 0);

}

function LoadCabeceraResult(data) {
    if (data != "") {
        $('#comcabeceracontent').html(data.d);
        GetDetalleusr();
       SetFormDetalle();
    }
   
   // SetForm(); //Depende de cada js.
}

function LoadDetalle() {
    $('#comdetallecontent2').empty();
    var jsonText = JSON.stringify({});
    CallServer(formname + "/GetDetalle", jsonText, 1);
}

function LoadDetalleResult(data) {
    if (data != "") {
        $('#comdetallecontent2').html(data.d);
        LoadDetalleData();

    }
    SetFormDetalle();
    //EditableRow("detalletabla");
}








// Inicio de la funcion getdetalleusr
function GetDetalleusr() {
    var obj = {};
    obj["udo_usuario"] = $("#cmbID_S").val();
    obj["udo_empresa"] = parseInt(empresasigned["emp_codigo"]);
    var jsonText = JSON.stringify({ id: obj });
    CallServer(formname + "/GetDetallePersonaAu", jsonText, 7);


}
//fin de la funcion getdetalleusr

function LoadCabeceraResultUsr(data) {
    if (data != "") {
        $('#comdetalle').html(data.d);
        
    }
}

function SetForm() {
    $("#cmbPRODUCTO_S").keyup("change", LoadDetalle);
    $("#cmbLISTAPRECIO_S").on("change", LoadDetalle);
    $("#cmbRUTA_S").on("change", LoadDetalle);
    $("#cmbALMACEN_S").on("change", LoadDetalle);
    $("#cmbUNIDAD_S").on("change", LoadDetalle);

}

function SetFormDetalle() {
    //Tratamiento para los elemntos del detalle    
 
    $("#addnew").on("click", LoadForm);    //Opción "Guardar" del combo de opciones de la sección de edición
    //  $("#saveedit").on("click", SaveObjUsr);  
    $("#remove").on("click", DeleteObjs);    //Opción "Eliminar" del combo de opciones de la sección de edición
    $("#modi").on("click", Modificar); //Opcion para modificar 
    //   $("#alldet").on("click", { tabla: "tddatos" }, SelectAllRows);
    // $("#nonedet").on("click", { tabla: "tddatos" }, CleanSelectedRows);
    $("#comdetallecontent2").on("scroll", scroll);
}

function scroll() {
    if ($("#comdetallecontent2")[0].scrollHeight - $("#comdetallecontent2").scrollTop() <= $("#comdetallecontent2").outerHeight()) {
        LoadDetalleData();
    }
}

// cunado se hace click en un dato de la tabla
function Edit(obj) {
    var id = $(obj).data("id");
    // CallDlistaPrecio(id);
    selectobj = obj;
    Select(obj);
    
}

/// funcion de selecion y deseleccion de un objeto

function Select(obj) {
    if ($(obj).css("background-color") == rgbcolor) {
        $(obj).css("background-color", "");
        $(obj).children("td").css("background-color", "");
        $('agregar').prop('disabled', false);
    }
    else {
        $(obj).css("background-color", color);
        $(obj).children("td").css("background-color", color);
    }
}
// fin de selecion de objeto
function ShowObj(obj) {
    var id = $(obj).data("id");
    var jsonText = JSON.stringify({ id: id });
    CallServer(formname + "/GetForm", jsonText, 3)
}


function ShowObjResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        CleanForm();
        SetJSONObject(obj); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM
        DisableControls(true);
        $("#edit").trigger('click'); //MUESTRA EL DIV CON EL FORM
        CleanSelect();

    }
}

function CallDlistaPrecio(id) {
    var obj = {};
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetAdmUsuario", jsonText, 3);
}

function CallAdmUsuarioResult(data) {
    if (data != "") {
        CallPopUp2("modDatosUsuario", "Datos Usuario", data.d);
        $("#cmbALMACEN_P").on("change", LoadPuntoVenta2);  //Opción "Cerrar" del combo de opciones de la sección de edición    
        $("#cmbALMACEN_P").trigger("change");


    }

    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    });
}




function GetAdmUusarioObj() {
    var obj = {};
    obj["uxe_usuario"] = $("#txtCODIGO_P").val();
    obj["uxe_empresa"] = parseInt($("#cmbEMPRESA_P").val());
    obj["uxe_empresa_key"] = parseInt($("#cmbEMPRESA_P").val());
    obj["uxe_almacen"] = parseInt($("#cmbALMACEN_P").val());
    obj["uxe_puntoventa"] = parseInt($("#cmbPVENTA_P").val());
    obj["uxe_empresapuntoventa"] = parseInt($("#cmbEMPRESA_P").val());
    obj["uxe_estado"] = ("1");
    obj = SetAuditoria(obj);
    return obj;
}

function CallAdUusarioOK() {
    SaveObj();
    return true;
}

function CallAdUusarioUsrOK() {
    SaveObjUsr();
    return true;
}






function ClearValidate() {
    var controles = $("#comcabecera").find('[data-obligatorio="True"]');
    $("#comcabecera").find('[data-obligatorio="True"]').each(function () {
        $(this.parentNode).removeClass('obligatorio')
        $(this.parentNode).children(".obligatorio").remove();
    });
}

function ValidateForm() {
    var retorno = true;
    var controles = $("#comcabecera").find('[data-obligatorio="True"]');
    var mensajehtml = "";
    $("#comcabecera").find('[data-obligatorio="True"]').each(function () {
        $(this.parentNode).removeClass('obligatorio')
        $(this.parentNode).children(".obligatorio").remove();
        if ($(this).val() == "") {
            retorno = false;
            var padre = $(this.parentNode);
            $(this.parentNode).append("<span class='obligatorio'>! Obligatorio</span>");
            $(this.parentNode).addClass('obligatorio')
            mensajehtml += "El campo <b>" + $(this).attr('placeholder') + "</b> es obligatorio <br>";
        }
    });

    /*var htmltable = $("#tddatos")[0];
    if (htmltable.rows.length < 2) {
    retorno = false;
    mensajehtml += "Es necesario ingresar al menos un detalle al comprobante<br>";
    }*/

    if (!retorno) {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert(mensajehtml, 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
    return retorno;
}





function AddObj() {
    //ClearValidate();
    // DisableControls(false);
    $("#edit").trigger('click');
    selectobj = null;
    CleanForm(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
}
function DisableControls(disable) {
    $("##cmbUSUARIO_S").attr("disabled", disable);

}



function LoadForm() {
    CallServer(formname + "/GetForm", "{}", 8);
}


function LoadFormResult(data) {
    if (data != "") {
        CallPopUp2("modPersonaAutorizacion", "Autorizaciones", data.d);
        


    }

    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    });
}

function LoadSiglaResult(data) {
    if (data != "") {
        $("#cmbCTIPOCOM").replaceWith(data.d);
        if ($("#cmbCTIPOCOM option").length == 0)
            $("#cmbCTIPOCOM").attr("disabled", true);
        else
            $("#cmbCTIPOCOM").attr("disabled", false);
    }
}


// funcion para guardar las opciones usrdoc
function SaveObjPea() {
    if (ValidateForm()) {
        var jsonText = GetJSONObject(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
        CallServer(formname + "/SaveObject", jsonText, 10);
    }

}
//function para guardar usrdoc
function SaveObjResultPea(data) {
    if (data != "") {
        if (data.d == "OK" || data.d != "ERROR") {
            /* if (selectobj != null) {
            jQuery.jGrowl("Registro actualizado con éxito");
            $(selectobj).replaceWith(data.d);
            }
            else {
            jQuery.jGrowl("Registro ingresado con éxito");
            $('#detallebody').append(data.d);
            }*/
            //   LoadDetalle();
            GetDetalleusr();
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al guardar la Autorizacion...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
        /*else {
        CleanForm(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM  
        ReloadData();
        }*/
    }
}

function GetJSONObject() {
    var obj = {};
    obj["ape_val_fecha"] = $("#cmbFECHA").val();
    obj["ape_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["ape_nro_autoriza"] = $("#txtNRO_AUTORIZA").val();
    obj["ape_fac1"] = $("#txtFAC1").val();
    obj["ape_fac2"] = $("#txtFAC2").val();
    obj["ape_retdato"] = parseInt($("#cmbRETDATO").val());
    obj["ape_empresa_key"] = parseInt(empresasigned["emp_codigo"]);
    obj["ape_nro_autoriza_key"] = $("#txtNRO_AUTORIZA_key").val();
    obj["ape_fac1_key"] = $("#txtFAC1_key").val().toString();
    obj["ape_fac2_key"] = $("#txtFAC2_key").val().toString();
    obj["ape_retdato_key"] = parseInt($("#txtRETDATO_key").val());
    obj["ape_tablacoa"] = parseInt($("#cmbTABLACOA").val());
    obj["ape_persona"] = parseInt($("#cmbPERSONA").val()); //parseInt(empresasigned["emp_informante"]);
    obj["ape_tclipro"] = parseInt($("#txttclipro").val());
    obj["ape_fact1"] = $("#txtFAC1").val();
    obj["ape_fact2"] = $("#txtFAC2").val();
    obj["ape_fact3"] = $("#txtFACT3").val();
    obj["ape_fac3"] = $("#txtFAC3").val();
    obj["ape_estado"] = GetCheckValue($("#chkESTADO"));
   
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
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
            CallServer(formname + "/DeleteObject", jsonText, 11)
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

            GetDetalleusr();
        }
    }
}



function CallPersonaAutorizacionOK() {
    SaveObjPea();
    return true;
}

function Modificar() {

   
            var objetos = [];
            $("#detallebody").find('tr').each(function () {
                if ($(this).css("background-color") != "transparent") {
                    objetos.push($(this).data("id"));
                }
            });
            var jsonText = JSON.stringify({ objetos: objetos });
            CallServer(formname + "/GetFormM", jsonText, 8)
        }
    

function EditAutorizacion(row)
{
    var data = $(row).data();

    var obj = {};
    obj["ape_empresa"] = empresasigned["emp_codigo"];
    obj["ape_nro_autoriza"] = $(row).data("nroautoriza").toString();
    obj["ape_fac1"] = $(row).data("fac1").toString();
    obj["ape_fac2"] = $(row).data("fac2").toString();
    obj["ape_retdato"] = $(row).data("retdato");
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/EditAutorizacion", jsonText, 8)

}

function DeleteAutorizacion(row) {

    jConfirm('¿Está seguro que desea eliminar el registro seleccionado?', 'Eliminar', function (r) {
        if (r) {
            var obj = {};
            obj["ape_empresa"] = empresasigned["emp_codigo"];
            obj["ape_nro_autoriza"] = $(row).data("nroautoriza").toString();
            obj["ape_fac1"] = $(row).data("fac1").toString();
            obj["ape_fac2"] = $(row).data("fac2").toString();
            obj["ape_retdato"] = $(row).data("retdato");
            var jsonText = JSON.stringify({ objeto: obj });
            CallServer(formname + "/DeleteAutorizacion", jsonText, 10)
        }
    });

   

}



function AddAutorizacion() {
    
    var obj = {};
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/EditAutorizacion", jsonText, 8)

}

   

