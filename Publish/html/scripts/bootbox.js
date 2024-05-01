var webservice = "/ws/Metodos2.asmx/";

function CallServerBootBox(strurl, strdata, retorno) {
    //ClearValidate();
    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            switch (retorno) {
                case "SELEMPRESA":
                    SelectEmpresaResult(data);
                    break;
                case "EDITCUE":
                    EditCuentaResult(data);
                    break;
                case "SAVECUE":
                    SaveCuentaResult(data);
                    break;
                case "REMOVECUE":
                    RemoveCuentaResult(data);
                    break;
                case "EDITCOB":
                    EditCobroResult(data);
                    break;
                case "DETCOB":
                    DetalleCobroResult(data);
                    break;
                case "SAVECOB":
                    SaveCobroResult(data);
                    break;
                case "DETCS":
                    DetalleCancelaSocioResult(data);
                    break;
                default:
                    

            }
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

function SelectEmpresa(usuario) {
    var obj = {};
    obj["usr_id"] = usuario;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerBootBox(webservice + "SelectEmpresasUsuario", jsonText, "SELEMPRESA");
}

function SelectEmpresaResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        if (obj[0] == "html")
        {
            bootbox.dialog({
                message:obj[1],
                title: "Seleccione Empresa",
                size: "medium",
                buttons: {
                    aceptar:
                    {
                        label: "Aceptar",
                        className: "btn-primary",
                        callback: function () {
                            var codigo = $("#cmbempresa_sel").val();
                            var nombre = $("#cmbempresa_sel option:selected").text();
                            return SelectEmpresaOK(codigo, nombre);                            
                        }
                    },
                    cancelar:
                    {
                        label: "Cancelar",
                        className: "btn-default",
                    }
                }
            });
        }            
        else {
            SelectEmpresaOK(obj[1], obj[2]);
        }
    }
}

function SelectEmpresaOK(codigo, nombre) {

    try {
        EndSelectEmpresa(codigo, nombre);
    }
    catch (err) {
    }


}


//////////////////FUNCION EDITAR NODO TREE///////////////////////////////

var dialognodo;
var tree;
var nodo;
var padre;

function EditCuenta(tr, node, parent) {

    tree = tr;
    nodo = node;
    padre = parent;
    var obj = JsonObj();
    obj["cue_empresa"] = GetOnlineCompany();
    if (nodo.data != null && nodo.data != undefined)
        obj["cue_codigo"] = nodo.data.codigo;
    obj["cue_reporta"] = padre.data.codigo;
    CallServerBootBox(webservice + "EditCuenta", JsonObjString(obj), "EDITCUE");


}


function EditCuentaResult(data) {
    dialognodo = bootbox.dialog({
        message: data.d,
        title: "Edición Cuenta",
        buttons: {
            aceptar:
            {
                label: "Aceptar",
                className: "btn-primary",
                callback: function () {
                    return SaveCuenta();
                }
            },
            cancelar:
            {
                label: "Cancelar",
                className: "btn-default",
            }
        }
    });
    dialognodo.bind('shown.bs.modal', function () {
        SetPlugins();
    });
}


function SaveCuenta() {
    var obj = JsonObj();
    obj["cue_empresa"] = GetOnlineCompany();
    obj["cue_codigo"] = $("#editcta").data("codigo");
    obj["cue_id"] = $("#txtID_CUE").val();
    obj["cue_nombre"] = $("#txtNOMBRE_CUE").val();
    obj["cue_genero"] = $("#cmbGENERO_CUE").val();
    obj["cue_modulo"] = $("#cmbMODULO_CUE").val();
    obj["cue_movimiento"] = GetCheckValue($("#chkMOVIMIENTO_CUE"));
    obj["cue_visualiza"] = GetCheckValue($("#chkVISUALIZA_CUE"));
    obj["cue_negrita"] = GetCheckValue($("#chkNEGRITA_CUE"));
    obj["cue_reporta"] = padre.data.codigo;
    obj["cue_estado"] = $("#cmbESTADO_CUE").val();

    CallServerBootBox(webservice + "SaveCuenta", JsonObjString(obj), "SAVECUE");
    return false;
}

function SaveCuentaResult(data) {
    var obj = $.parseJSON(data.d);

    if (obj[0] == "OK") {
        nodo.data["codigo"] = obj[1]["cue_codigo"];
        var textnodo = $("#txtID_CUE").val() + "." + $("#txtNOMBRE_CUE").val();
        tree.rename_node(nodo, textnodo);
        tree.deselect_all(true);
        tree.select_node(nodo);
        dialognodo.modal("hide");
    }
}


function RemoveCuenta(tr, node, parent) {
    tree = tr;
    nodo = node;
    padre = parent;

    bootbox.dialog({
        message: "¿Está seguro que desea eliminar la cuenta?",
        //title: title,
        //size: size,
        buttons: {
            si:
            {
                label: "Si",
                className: "btn-primary",
                callback: function () {
                    var obj = JsonObj();
                    obj["cue_empresa"] = GetOnlineCompany();
                    if (nodo.data != null && nodo.data != undefined)
                        obj["cue_codigo"] = nodo.data.codigo;
                    CallServerBootBox(webservice + "RemoveCuenta", JsonObjString(obj), "REMOVECUE");
                }
            },
            no:
            {
                label: "No",
                className: "btn-default",
            }
        }
    });




}

function RemoveCuentaResult(data) {

    if (data.d == "OK") {
        tree.delete_node(nodo);
    }
    else
        BootBoxAlert(data.d);
}

/////////////////////////////////////////////////////////////////////
///////////////// FUNCION EDIT AUTORIZACION PERSONA/////////////////

var dialogcobro;

function EditCobro(obj) {
    CallServerBootBox(webservice + "EditDetalleCobro", JsonObjString(obj), "EDITCOB");

}

function EditCobroResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        dialogcobro = bootbox.dialog({
            message: obj[1],
            title: obj[0],
            size: "large",
            buttons: {
                refresh:
                {
                    label: "<i class='fa fa-refresh'/>",
                    className: "btn-default",
                    callback: function () {
                        //CleanAutPersona();
                        return false;
                    }
                },
                aceptar:
                {
                    label: "Aceptar",
                    className: "btn-primary",
                    callback: function () {
                        return SaveCobro();
                    }
                },
                cancelar:
                {
                    label: "Cancelar",
                    className: "btn-default",
                }
            }
        });

        dialogcobro.bind('shown.bs.modal', function () {
            SetFormEditCobro();
        });




    }
}

function SetFormEditCobro() {
    //$("#txtnro_aut").focus();
    //$("#txtnro_aut").select();
    SetPlugins();
    $(".cobro").on("keyup", CalculaCobro);
    $(".cobro").on("change", CalculaCobro);
    $(".IVA").on("change", CalculaRetIva);
    $(".RENTA").on("change", CalculaRetRenta);
    $(".seltipo").on("click", SelectTipo);
    $(".addtipo").on("click", AddTipo);
    CalculaCobro();

}


function AddTipo() {
    var row = $(this.parentNode).clone();
    $(row[0].cells[0]).html("");
    $(row).find(".cobro").on("keyup", CalculaCobro);
    $(row).find(".cobro").on("change", CalculaCobro);
    $(row).find(".seltipo").on("click", SelectTipo);
    $(this.parentNode).after(row);


}

function SelectTipo() {
    var pendiente = GetFloat($("#txtPENDIENTE_P").val());
    $(this.parentNode).find(".cobro").val(CurrencyFormatted(pendiente))
    CalculaCobro();

}

function CalculaRetIva() {
    var porcret = GetFloat($("#tdpago").find(".IVA").val());
    var iva = GetFloat($("#txtIVA_P").val());

    var valor = iva * (porcret / 100);

    $(".valorIVA").val(CurrencyFormatted(valor));
    CalculaCobro();

}

function CalculaRetRenta() {
    var porcret = GetFloat($("#tdpago").find(".RENTA").val());
    var subtotal = GetFloat($("#txtSUBTOTAL_P").val());

    var valor = subtotal * (porcret / 100);

    $(".valorRENTA").val(CurrencyFormatted(valor));
    CalculaCobro();

}

function CalculaCobro() {

    var monto = GetFloat($("#txtTOTAL_P").val());
    var pagado = GetFloat($("#txtPAGADO_P").val());
    var saldo = monto - pagado;

    var entregado = 0;
    var pendiente = 0;
    var cambio = 0;

    $.each($(".cobro"), function (index, obj) {

        var valor = GetFloat(obj.value)
        entregado += valor;
        if (valor.toFixed(2) > 0)
            $(obj).addClass("pendiente");
        else
            $(obj).removeClass("pendiente");


    });

    pendiente = saldo - entregado;
    if (pendiente.toFixed(2) < 0) {
        cambio = pendiente * -1;
        pendiente = 0;
    }
    $("#txtPENDIENTE_P").val(CurrencyFormatted(pendiente));
    $("#txtENTREGADO_P").val(CurrencyFormatted(entregado));
    $("#txtCAMBIO_P").val(CurrencyFormatted(cambio));

    if (pendiente.toFixed(2) > 0)
        $("#txtPENDIENTE_P").addClass("pendiente");
    else
        $("#txtPENDIENTE_P").removeClass("pendiente");

    if (cambio.toFixed(2) > 0)
        $("#txtCAMBIO_P").addClass("cambio");
    else
        $("#txtCAMBIO_P").removeClass("cambio");
}


function GetCobroObj() {

    var obj = JsonObj();
    obj["com_empresa"] = GetOnlineCompany();
    obj["com_codigo"] = $("#editpago").data("comprobante");
    return obj;
}

function GetDetalleCobro() {

    var detalle = Array();

    $.each($(".cobro"), function (index, obj) {

        var valor = GetFloat(obj.value)
        if (valor > 0) {
            var row = obj.parentNode.parentNode;

            var det = JsonObj();
            det["dre_empresa"] = GetOnlineCompany();
            det["dre_id"] = $(row).data("id");
            det["dre_tipo"] = $(row).data("nombre");
            det["dre_emisor"] = $(row).find(".emisor").val();
            det["dre_banco"] = $(row).find(".banco").val();
            det["dre_nro"] = $(row).find(".nro").val();
            det["dre_porcentaje"] = $(row).find(".porcentaje").val();
            det["dre_valor"] = valor;
            detalle[detalle.length] = det;
        }
    });
    return detalle
}

function SaveCobro() {
    var obj = JsonObj();
    obj["comprobante"] = GetCobroObj();
    obj["detalle"] = GetDetalleCobro();

    //$("#save").attr("disabled", true);
    //saving = true;
    CallServerBootBox(webservice + "SaveCobro", JsonObjString(obj), "SAVECOB");
}

function SaveCobroResult(data) {

    if (data != "") {
        var obj = $.parseJSON(data.d);
        var row = $("#tdlistado").find('tr[data-codigo="' + obj[1] + '"]');
        row.replaceWith(obj[0]);
    }
}

///////////////////////  DETALLE COBROS ////////////////////////////////////////////////////////////////////////////////


var dialogdetcobro;

function DetalleCobro(obj) {
    CallServerBootBox(webservice + "GetDetalleCobros", JsonObjString(obj), "DETCOB");

}

function DetalleCobroResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        dialogcobro = bootbox.dialog({
            message: obj[1],
            title: obj[0],
            size: "large",
            buttons: {
                aceptar:
                {
                    label: "Aceptar",
                    className: "btn-primary",
                    callback: function () {
                        //return SaveCobro();
                    }
                },
                cancelar:
                {
                    label: "Cancelar",
                    className: "btn-default",
                }
            }
        });

        dialogcobro.bind('shown.bs.modal', function () {
            //SetFormEditCobro();
        });

    }
}
//////////////////////////////////////////////////////////////////////

///////////////////////  DETALLE CANCELA SOCIOS ////////////////////////////////////////////////////////////////////////////////


var dialogdetcancelasocio;

function DetalleCancelaSocio(obj) {
    CallServerBootBox(webservice + "GetCancelacionesSocio", JsonObjString(obj), "DETCS");

}

function DetalleCancelaSocioResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        dialogdetcancelasocio = bootbox.dialog({
            message: obj[1],
            title: obj[0],
            size: "large",
            buttons: {               
                cancelar:
                {
                    label: "Cancelar",
                    className: "btn-default",
                }
            }
        });

        dialogdetcancelasocio.bind('shown.bs.modal', function () {
            //SetFormEditCobro();
        });

    }
}
//////////////////////////////////////////////////////////////////////


function BootBoxAlert(mensaje)
{
    bootbox.alert(mensaje);
}


function BootBoxDialog(title,html,size,id)
{
    bootbox.dialog({
        message:html,
        title: title,
        size: size,
        buttons: {
            aceptar:
            {
                label: "Aceptar",
                className: "btn-primary",
                callback: function () {
                    try{
                        return BootBoxAccept(id);
                    }
                    catch(err)
                    {

                    }
                    
                }
            },
            cancelar:
            {
                label: "Cancelar",
                className: "btn-default",
            }
        }
    });

    SetPlugins();

}



function BootBoxConfirm(ask,data, id) {
    bootbox.dialog({
        message: ask,
        //title: title,
        //size: size,
        buttons: {
            si:
            {
                label: "Si",
                className: "btn-primary",
                callback: function () {
                    try {
                        BootBoxSi(data,id);
                    }
                    catch (err) {

                    }

                }
            },
            no:
            {
                label: "No",
                className: "btn-default",
            }
        }
    });
}