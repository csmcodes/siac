
var webservice = "ws/Metodos.asmx/";

//Funciona que invoca al servidor mediante JSON
function CallServerPopup(strurl, strdata, retorno) {
    //ClearValidate();
    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //if (retorno == 0)
            //    CallPersonaResult(data);
            if (retorno == 1)
                CallDocumentosResult(data);
            if (retorno == 2)
                $("#tddocs>tbody").html(data.d);
            if (retorno == 3)
                AddNewDocumentResult(data);
            if (retorno == 4)
                $("#btnrefresh").click();
            if (retorno == 5)
                GetClienteResult(data);
            if (retorno == 6)
               GetProductoResult(data);

            if (retorno == 7)
                $("#tdmarco>tbody").html(data.d);
            if (retorno == 8)
                CallEditAccesoResult(data);
            if (retorno == 9) {
                GetFiltroNivelesAccesoResult(data);
            }
            if (retorno == 10) {
                RefreshFiltersResult(data);
            }
            if (retorno == 11)
                GetAccesosProyecto();

            if (retorno == "EditUser")
                EditUserResult(data);
            if (retorno == "SaveUser")
                SaveUsuarioResult(data);
            if (retorno == "SetPass")
                SetPasswordResult(data);
            if (retorno == "SavePass")
                SavePasswordResult(data);
            if (retorno == 15)
                EmpresasUsuarioResult(data);
            if (retorno == 17)
                SelectEmpresaResult(data);


            if (retorno == 20)
                CallEditResultadoResult(data);
            if (retorno == 21)
                CallEditActividadResult(data);
            if (retorno == 22)
                SaveEditResultadoResult(data);
            if (retorno == 23)
                SaveEditActividadResult(data);
            if (retorno == 24)
                CallEditProyectoResult(data);
            if (retorno == 25)
                SaveEditProyectoResult(data);
            if (retorno == 26)
                GetFiltroNivelesGeneralResult(data);
            if (retorno == 27)
                RefreshFiltersGenResult(data);
            if (retorno == "EditCompania")
                AddCompaniaResult(data);
            if (retorno == "SaveCompania")
                SaveCompaniaResult(data);
            if (retorno == "EditOficina")
                AddOficinaResult(data);
            if (retorno == "SaveOficina")
                SaveOficinaResult(data);
            if (retorno == "ConfigCompania")
                ConfigCompaniaResult(data);
            if (retorno == "ModificarEnvio")
                ModificarEnviosResult(data);
            if (retorno == "SaveEnvio")
                SaveModificarEnviosResult(data);
            if (retorno == "EditComision")
                AddComisionResult(data);
            if (retorno == "SaveComision")
                SaveComisionResult(data);
            if (retorno == "CalculaComision")
                CalculaComisionResult(data);
            if (retorno == "RunCalculaComision")
                RunCalculaComisionResult(data);
            if (retorno == "UsersOffice")
                GetUsuariosOficinaResult(data);
            if (retorno == "EditOfficeUser")
                EditOfficeUserResult(data);
            if (retorno == "SaveOfficeUser")
                SaveOfficeUserResult(data);
            if (retorno == "RemoveOfficeUser")
                RemoveOfficeUserResult(data);

            if (retorno == "EditTransf")
                EditTransferenciaResult(data);
            if (retorno == "SaveTransf")
                SaveTransferenciaResult(data);
            if (retorno == "GetIdTrans")
                GetIdTransferenciaResult(data);

            if (retorno == "EditCancel")
                EditCancelacionResult(data);
            if (retorno == "SaveCancel")
                SaveCancelacionResult(data);
            if (retorno == "GetIdCancel")
                GetIdCancelacionResult(data);


            if (retorno == "ReadImpFile")
                ReadImpFileResult(data);
            if (retorno == "SaveImpFile")
                SaveImpFileResult(data);
            if (retorno == "GenEstadoCuenta")
                GenerarEstadoCuentaResult(data);
            if (retorno == "GetEnvioView")
                ViewEnvioResult(data);

            if (retorno == "GetPagosPen")
                GetPagosPendientesResult(data);
            if (retorno == "GetPagosAfe")
                GetPagosAfectadosResult(data);
            if (retorno == "GetCantones")
                GetCantonesResult(data);
            if (retorno == "GetParroquias")
                GetParroquiasResult(data);
            if (retorno == "GetECOfiCom")
                ViewEstadoCuentaOficinaCompaniasResult(data);
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

function PopUpOk() {
    var id = $(this)[0].id.replace("btnok", "");
    if (id == "modAddDocument") {
        var data = $("#documentdata").data();
        SaveDocument(data["empresa"], data["proyecto"], data["nivel"], data["nivelvalor"], data["periodo"], data["resultado"], data["actividad"], data["variable"], data["indicador"])
        KillPopUp(id);
    }
    if (id == "modGetCliente") {
        //REFRESH MENU
        KillPopUp(id);
        window.location.reload();
    }

    if (id == "modGetProducto") {
        AddProduct()
        KillPopUp(id);        
    }
    if (id == "modAcceso") {
        var data = $("#accesodata").data();
        SaveAcceso(data["empresa"], data["proyecto"]);
        KillPopUp(id);
    }
    if (id == "modAddUsr") {
        var data = $("#usrdata").data();
        SaveUsuario(data["empresa"]);
        KillPopUp(id);
    }
    if (id == "modSelEmp") {
        var codigo = $("#cmbempresa_sel").val();
        var nombre = $("#cmbempresa_sel option:selected").text();
        var usuario = $("#cmbempresa_sel option:selected").data("usuario");
        var perfil = $("#cmbempresa_sel option:selected").data("perfil");
        var nombres = $("#cmbempresa_sel option:selected").data("nombres");
        SelectEmpresaOK(codigo, nombre, usuario.toString(), perfil, nombres);
        KillPopUp(id);
    }
    if (id == "modEditPro")
    {
        SaveEditProyecto();        
        KillPopUp(id);
    }
    if (id == "modEditRes") {
        SaveEditResultado();        
        KillPopUp(id);
    }
    if (id == "modEditAct") {
        SaveEditActividad();
        KillPopUp(id);
    }
    if (id == "modEditUsr")
    {
        SaveUsuario();
        KillPopUp(id);
    }
    if (id == "modSetPass") {
        if (SavePassword())
            KillPopUp(id);
    }
    if (id == "modEditCompania") {
        //var data = $("#usrdata").data();
        SaveCompania();
        KillPopUp(id);
    }
    if (id == "modEditOficina") {
        //var data = $("#usrdata").data();
        SaveOficina();
        KillPopUp(id);
    }
    if (id == "modEditEnvio") {
        //var data = $("#usrdata").data();
        SaveModificarEnvios();
        KillPopUp(id);
    }
    if (id == "modEditComision") {
        
        SaveComision();
        KillPopUp(id);
    }

    if (id == "modCalculaComision") {

        RunCalculaComision();
        KillPopUp(id);
    }
    if (id=="modUsersOffice")
    {
        KillPopUp(id);
    }
    if (id == "modEditOfficeUser") {
        SaveOfficeUser();
        KillPopUp(id);
    }
    if (id == "modEditTransf")
    {
        SaveTransferencia();
        KillPopUp(id);
    }
    if (id == "modEditCancel") {
        GetPagosPendientes($("#editdata").data("codigo"));
        //KillPopUp(id);
    }
    if (id == "modPagosPen") {
        GetCancelaciones();
        //KillPopUp(id);
    }



    if (id == "modReadImpFile") {
        SaveImpFile();
        KillPopUp(id);
    }
    if (id=="modGenerarEstadoCuenta")
    {
        RunGenerarEstadoCuenta();
        KillPopUp(id);
    }

}

function PopUpCancel() {
    var id = $(this)[0].id.replace("btncancel", "");    
    if (id == "modGetCliente") {
        SetOnlineCustomer(null,false);
        SetOnlineCustomerName(null,false);
    }
    KillPopUp(id)    
}




function CallPopUp(idpopup, titulo, clase, clase1, htmlcontent) {
    KillPopUp(idpopup);

    var popup = $('<div class="modal fade ' + clase + '" id="' + idpopup + '" tabindex="-1" data-backdrop="static" data-keyboard="false"><div class="modal-dialog ' + clase1 + '"><div class="modal-content"> <div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button><h4 class="modal-title">' + titulo + '</h4></div><div class="modal-body">' + htmlcontent + '</div><div class="modal-footer"><button type="button"  id="btnok' + idpopup + '"  class="btn blue">Aceptar</button><button type="button" id="btncancel' + idpopup + '" class="btn default" data-dismiss="modal">Cancelar</button></div></div></div></div>');
    $("#popups").append(popup);
    $('#' + idpopup).modal('show');
    $("#btnok" + idpopup).on("click", PopUpOk);
    $("#btncancel" + idpopup).on("click", PopUpCancel);
}


function CallPopUpRemove(idpopup, titulo, clase, clase1, htmlcontent) {
    KillPopUp(idpopup);

    var popup = $('<div class="modal fade ' + clase + '" id="' + idpopup + '" tabindex="-1" data-backdrop="static" data-keyboard="false"><div class="modal-dialog ' + clase1 + '"><div class="modal-content"> <div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button><h4 class="modal-title">' + titulo + '</h4></div><div class="modal-body">' + htmlcontent + '</div><div class="modal-footer"><button type="button"  id="btnok' + idpopup + '"  class="btn blue">Aceptar</button><button type="button" id="btncancel' + idpopup + '" class="btn red" data-dismiss="modal">Quitar</button></div></div></div></div>');
    $("#popups").append(popup);
    $('#' + idpopup).modal('show');
    $("#btnok" + idpopup).on("click", PopUpOk);
    $("#btncancel" + idpopup).on("click", PopUpCancel);
}


function CallPopUpButton(idpopup, titulo, clase, clase1, htmlcontent, boton) {
    KillPopUp(idpopup);
    
    var popup = $('<div class="modal fade ' + clase + '" id="' + idpopup + '" tabindex="-1" data-backdrop="static" data-keyboard="false"><div class="modal-dialog ' + clase1 + '"><div class="modal-content"> <div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button><h4 class="modal-title">' + titulo + '</h4></div><div class="modal-body">' + htmlcontent + '</div><div class="modal-footer"><button type="button"  id="btnok' + idpopup + '"  class="btn blue">' + boton + '</button><button type="button" id="btncancel' + idpopup + '" class="btn default" data-dismiss="modal">Cancelar</button></div></div></div></div>');
    $("#popups").append(popup);
    $('#' + idpopup).modal('show');
    $("#btnok" + idpopup).on("click", PopUpOk);
    $("#btncancel" + idpopup).on("click", PopUpCancel);
}

function CallPopUpExtraButton(idpopup, titulo, clase, clase1, htmlcontent, boton) {
    KillPopUp(idpopup);

    var popup = $('<div class="modal fade ' + clase + '" id="' + idpopup + '" tabindex="-1" data-backdrop="static" data-keyboard="false"><div class="modal-dialog ' + clase1 + '"><div class="modal-content"> <div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button><h4 class="modal-title">' + titulo + '</h4></div><div class="modal-body">' + htmlcontent + '</div><div class="modal-footer">' + boton + '<button type="button"  id="btnok' + idpopup + '"  class="btn blue">Aceptar</button><button type="button" id="btncancel' + idpopup + '" class="btn default" data-dismiss="modal">Cancelar</button></div></div></div></div>');
    $("#popups").append(popup);
    $('#' + idpopup).modal('show');
    $("#btnok" + idpopup).on("click", PopUpOk);
    $("#btncancel" + idpopup).on("click", PopUpCancel);
}




function KillPopUp(idpopup) {
    var popup = $("#" + idpopup);
    $(popup).remove();
}




//POPUP CALL DOCUMENTOS

function CallDocumentos(empresa, proyecto, nivel, nivelvalor, periodo, resultado, actividad, variable, indicador) {
    var obj = {};
    obj["doc_empresa"] = empresa;
    obj["doc_proyecto"] = proyecto;
    obj["doc_nivel"] = nivel;
    obj["doc_nivelvalor"] = nivelvalor;
    obj["doc_periodo"] = periodo;
    obj["doc_resultado"] = resultado;
    obj["doc_actividad"] = actividad;
    obj["doc_variable"] = variable;
    obj["doc_indicador"] = indicador;
    
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice+ "GetDocumentos", jsonText, 1);
}



function CallDocumentosResult(data) {
    if (data != "") {
        CallPopUp("modDocumentos", "Documentos", "bs-modal-lg", "modal-lg", data.d);
    }
}

function OpenDocument(doc) {
    window.open("wfAbrirDocumento.aspx?doc=" + doc, "", "toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=500,top=50,left=50");
}

function RemoveDocument(doc) {
    var obj = {};
    obj["doc_codigo"] = doc;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "RemoveDocument", jsonText, 4);
}
function AddNewDocument(empresa, proyecto, nivel, nivelvalor, periodo, resultado, actividad, variable, indicador) {
    var obj = {};
    obj["doc_empresa"] = empresa;
    obj["doc_proyecto"] = proyecto;
    obj["doc_nivel"] = nivel;
    obj["doc_nivelvalor"] = nivelvalor;
    obj["doc_periodo"] = periodo;
    obj["doc_resultado"] = resultado;
    obj["doc_actividad"] = actividad;
    obj["doc_variable"] = variable;
    obj["doc_indicador"] = indicador;
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "NewDocument", jsonText, 3);
}

function AddNewDocumentResult(data) {
    if (data != "") {
        CallPopUp("modAddDocument", "Agregar Documento","","", data.d);



    }
}
//function AddNewDocument() {
//    CleanFormDocument();
//    $('#modAddDoc').modal('show');
//}

function CleanFormDocument() {
    $("#txtdescripcion_doc").val("");
    $("#inputfiledoc").val("");
}

function SaveDocument(empresa, proyecto, nivel, nivelvalor, periodo, resultado, actividad, variable, indicador) {
    var inputfile = "inputfiledoc";    
    var descripcion = $("#txtdescripcion_doc").val();

    ajaxFileUpload(inputfile, descripcion, empresa, proyecto, nivel, nivelvalor, periodo, resultado, actividad, variable, indicador, GetOnlineUser());
}


function UploadResult(result) {    
    $("#btnrefresh").click();

} function RefreshDocuments(empresa, proyecto, nivel, nivelvalor, periodo, resultado, actividad, variable, indicador) {
    var obj = {};
    obj["doc_empresa"] = empresa;
    obj["doc_proyecto"] = proyecto;
    obj["doc_nivel"] = nivel;
    obj["doc_nivelvalor"] = nivelvalor;
    obj["doc_periodo"] = periodo;
    obj["doc_resultado"] = resultado;
    obj["doc_actividad"] = actividad;
    obj["doc_variable"] = variable;
    obj["doc_indicador"] = indicador;

    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetDocumentosData", jsonText, 2);
}


///////////POP UP GET CLIENTE////////////////////////



function GetCliente() {
    var obj = {};
    obj["codigo_empresa"] = GetOnlineCompany();
    obj["codigo_usuario"] = GetOnlineUser();
    obj["codigo_cliente"] = GetOnlineCustomer();
    obj["razonsocial_cliente"] = GetOnlineCustomerName();
  
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetCliente", jsonText, 5);
}


function GetClienteResult(data) {
    if (data != "") {
        CallPopUpRemove("modGetCliente", "Cliente", "", "", data.d);
        var obj = {};
        obj["codigo_empresa"] = GetOnlineCompany();
        obj["codigo_usuario"] = GetOnlineUser();
        //obj["codigo_cliente"] = GetOnlineCustomer();


        $("#txtcliente_p").typeahead(null, {
                displayKey: 'razonsocial_cliente' ,
                hint: (Metronic.isRTL() ? false : true),
                source: function (query, process) {
                     obj["filterKey"] = query;
                    return $.ajax({
                        type: "POST",
                        url: "ws/Metodos.asmx/GetClientes",
                        //data: JSON.stringify({query: query }),
                        data:  JSON.stringify({ parametros: obj}), 
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success:
                        function (data) {
                            lst = data.d; 
                            return process(lst);
                       }
                    });
                },
                matcher: function (item) {
                    return true;
                },
                sorter: function (items) {
                    return items;
                }
                ,
                highlighter: function (id) {
                    return "<b>"+id+"</b>";
//                    var country = _.find(countries, function (c) {
//                        return c.Id == id;
//                    });
//                    return country.Name + " Population: " + country.Population; 
                },
                onselect: function(obj) { 
                    alert(obj) 
                }
                ,
                updater: function (id) {
                    var country = _.find(countries, function (c) {
                        return c.Id == id;
                    });
                    return country.Name;
                }
            });


            $('#txtcliente_p').bind('typeahead:selected', function(obj, datum, name) {      
                    //alert(JSON.stringify(obj)); // object
                    // outputs, e.g., {"type":"typeahead:selected","timeStamp":1371822938628,"jQuery19105037956037711017":true,"isTrigger":true,"namespace":"","namespace_re":null,"target":{"jQuery19105037956037711017":46},"delegateTarget":{"jQuery19105037956037711017":46},"currentTarget":
                    //alert(JSON.stringify(datum)); // contains datum value, tokens and custom fields
                    SetOnlineCustomer(datum["codigo_cliente"],false);
                    SetOnlineCustomerName(datum["razonsocial_cliente"],false);

                    
                    //alert(JSON.stringify(name)); // contains dataset name
                    // outputs, e.g., "my_dataset"

            });
        








      
        //SetAutocompleteByIdParams("txtcalidad_mar", obj);
    }
}





function RemoveMarco(row) {
     bootbox.dialog({
        message: "¿Está seguro que desea continuar?",
        title: "Pregunta",
        buttons: {
            aceptar: 
            {
                label: "Si",
                className: "btn-primary",
                callback: function () {
                   $(row).remove();
                }
            },
            cancelar:
            {
                label: "No",
                className: "btn-default",
            }            
        }
    });



//    var obj = {};
//    obj["mar_codigo"] = marco;
//    var jsonText = JSON.stringify({ objeto: obj });
//    CallServerPopup(webservice + "RemoveMarco", jsonText, 6);
}


//function RefreshMarco(empresa, proyecto, resultado, actividad) {    
//    var obj = {};
//    obj["mar_empresa"] = empresa;
//    obj["mar_proyecto"] = proyecto;
//    obj["mar_resultado"] = resultado;
//    obj["mar_actividad"] = $.isNumeric(actividad) ? actividad : null;
//    var jsonText = JSON.stringify({ objeto: obj });
//    CallServerPopup(webservice + "GetMarcoData", jsonText, 7);
//}




function GetMarcoObj(row) {
    var obj = {};
    obj["mar_empresa"] = $(row).data("empresa");
    obj["mar_proyecto"] = $(row).data("proyecto");
    obj["mar_resultado"] = $(row).data("resultado");
    obj["mar_actividad"] = $.isNumeric($(row).data("actividad")) ? $(row).data("actividad") : null;
    obj["mar_indicador"] = row.cells[1].innerText;
    obj["mar_medios"] = row.cells[2].innerText;
    obj["mar_riesgos"] = row.cells[3].innerText;
     obj["mar_cantidad"] =  $(row).data("cantidad");
    obj["mar_calidad"] =  $(row).data("calidad");
    obj["mar_unidad"] =  $(row).data("unidad");
    obj["mar_desde"] =  $(row).data("desde");
    obj["mar_hasta"] =  $(row).data("hasta");
    SetAuditoria(obj);
    obj = SetAuditoria(obj);
    return obj;
}
function GetDetalleMarco(table) {
    var detalle = new Array();
    var htmltable = $("#" + table)[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var obj = GetMarcoObj(htmltable.rows[r]);
        if (obj != null)
            detalle[r] = obj;
    }
    return detalle;
}


//POPUP GET PRODUCTO

function GetProducto(id) {
    var obj = {};
    obj["codigo_empresa"] = GetOnlineCompany();
    obj["codigo_producto"] = id;
    obj["codigo_cliente"] = GetOnlineCustomer();
  
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetProducto", jsonText, 6);
}

function GetProductoResult(data) {
    if (data != "") {
        CallPopUp("modGetProducto", "Producto", "", "", data.d);
    }
}

function AddProduct()
{
    var codigo = $("#codigo_producto").html();
    var referencia = $("#referencia").html();
    var descripcion = $("#descripcion").html();
    var cantidad = $("#txtcantidad_pro").val();

    var normal = $("#normal").val();
    var mayor = $("#mayor").val();

    var precio;
    if ($('#normal').prop('checked'))
        precio = normal;
    else
        precio = mayor;
    
    SetPedido(codigo, referencia, descripcion, cantidad,precio);
    GetPedidoPreview();
}




//POPUP EDIT ACCESO

function CallEditAcceso(empresa, proyecto, usuario) {
    var obj = {};
    obj["acc_empresa"] = empresa;
    obj["acc_proyecto"] = proyecto;
    obj["acc_usuario"] = usuario;
    
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "EditAcceso", jsonText, 8);
}



function CallEditAccesoResult(data) {
    if (data != "") {
        CallPopUp("modAcceso", "Editar Acceso Usuario", "bs-modal-lg", "modal-lg", data.d);
        

        $('#txtusuario_acc').typeahead({
            hint: true,
            highlight: true,
            minLength: 1
        },
            {
                name: 'states',
                displayKey: 'value',
                source: function (request, response) {
                    $.ajax({
                        url: "ws/Metodos.asmx/GetCustomers",
                        data: "{ 'prefix': '" + request + "', 'empresa':'" + GetOnlineCompany() + "', 'perfil':'" + GetOnlineUserRol() + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {                                
                                return {
                                    value: item.usr_nombres,
                                    id: item.usr_id,
                                    item: item
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                }
            }).bind('typeahead:selected', function(obj, datum) {
                changeTypeahead(obj, datum);
            }).bind('typeahead:autocompleted', function(obj, datum) {
               changeTypeahead(obj, datum);
            });

       


        
        //$("#cmbtiponivel_acc").on("change", GetFiltroNivelesAcceso)
        //GetFiltroNivelesAcceso();
        

    }
}

function changeTypeahead(obj, datum) {
    $("#usuario").data("codigo",datum.item.usr_id);        
    $("#usuarioperfil").html(datum.item.usr_perfil);        
            
}




function GetFiltroNivelesAcceso() {

    var obj = {};
    obj["niv_empresa"] = GetOnlineCompany();
    obj["niv_tipo"] = $("#cmbtiponivel_acc").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetFiltroNiveles", jsonText, 9);

}

function GetFiltroNivelesAccesoResult(data) {
    $("#dynamicfilters_acc").html(data.d);

    var filtro = $("#dynamicfilters_acc").find("[data-padre='']");

    var valor = GetAccesoTree($(filtro).data("codigo"));
    $(filtro).val(valor);
    $(filtro).change();
}











function GetFiltroNivelesGeneral() {

    var obj = {};
    obj["niv_empresa"] = GetOnlineCompany();
    obj["niv_tipo"] = $("#cmbtiponivel").val();
    obj["tipo"] = "1";
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetFiltroNiveles", jsonText, 26);

}

function GetFiltroNivelesGeneralResult(data) {
    $("#dynamicfilters_gen").html(data.d);

    var filtro = $("#dynamicfilters_gen").find("[data-padre='']");

    var valor = GetGenTree($(filtro).data("codigo"));
    $(filtro).val(valor);
    $(filtro).change();
}

function RefreshFiltersGen(opt) {

    var addfilter = $(opt.parentNode).find("input");
    if (addfilter.length > 0) {
        $(addfilter).val("");
        if (opt.selectedIndex > 0)
            $(addfilter).val(opt.options[opt.selectedIndex].text);
    }
    var codigo = $(opt).data("codigo");
    var filtro = $("#dynamicfilters_gen").find("[data-padre='" + codigo + "']");
    if (filtro.length > 0) {

        var obj = {};
        obj["nva_empresa"] = GetOnlineCompany();
        obj["nva_nivel"] = $(filtro).data("codigo");
        obj["nva_nivelpadre"] = codigo;
        obj["nva_padre"] = $(opt).val();
        obj["nva_codigo"] = GetGenTree($(filtro).data("codigo"));
           
        var jsonText = JSON.stringify({ objeto: obj });
        CallServerPopup(webservice + "GetNivelValores", jsonText, 27);

    }
}

function RefreshFiltersGenResult(data) {
    if (data.d != "") {
        var obj = $.parseJSON(data.d);
        var filtro = $("#dynamicfilters_gen").find("[data-codigo='" + obj[0] + "']");
        
        $(filtro).html(obj[1]);
        $(filtro).change();
    }


}

function GetFiltroNivelGen() {

    var filtro = null;
    $("#dynamicfilters_gen").find("select").each(function (index) {        
        if (this.value!="")
            filtro = this;                    
    });
    return filtro;
}

function GetGenTree(nivel) {
    var data = $("#dynamicfilters_gen").data("tree");
    var array = new Array();
    array = data.split('|');
    for (var i = 0; i < array.length; i++) {
        var arraynv = new Array();
        arraynv = array[i].split(',');
        if (nivel == arraynv[0])
            return arraynv[1];
    }
    return null;
}











function GetAccesoTree(nivel) {
    var data = $("#accesodata").data("tree");
    var array = new Array();
    array = data.split('|');
    for (var i = 0; i < array.length; i++) {
        var arraynv = new Array();
        arraynv = array[i].split(',');
        if (nivel == arraynv[0])
            return arraynv[1];
    }
    return null;
}


function RefreshFilters(opt) {

    var addfilter = $(opt.parentNode).find("input");
    if (addfilter.length > 0) {
        $(addfilter).val("");
        if (opt.selectedIndex > 0)
            $(addfilter).val(opt.options[opt.selectedIndex].text);
    }
    var codigo = $(opt).data("codigo");
    var filtro = $("#dynamicfilters_acc").find("[data-padre='" + codigo + "']");
    if (filtro.length > 0) {

        var obj = {};
        obj["nva_empresa"] = GetOnlineCompany();
        obj["nva_nivel"] = $(filtro).data("codigo");
        obj["nva_nivelpadre"] = codigo;
        obj["nva_padre"] = $(opt).val();
        obj["nva_codigo"] = GetAccesoTree($(filtro).data("codigo"));
           
        var jsonText = JSON.stringify({ objeto: obj });
        CallServerPopup(webservice + "GetNivelValores", jsonText, 10);

    }
}

function RefreshFiltersResult(data) {
    if (data.d != "") {
        var obj = $.parseJSON(data.d);
        var filtro = $("#dynamicfilters_acc").find("[data-codigo='" + obj[0] + "']");
        
        $(filtro).html(obj[1]);
        $(filtro).change();
    }


}

function GetFiltroNivelAcceso() {

    var filtro = null;
    $("#dynamicfilters_acc").find("select").each(function (index) {
        if (this.value != "")
            filtro = this;
    });
    return filtro;
}

function SaveAcceso(empresa, proyecto) {

    var obj = {};
    obj["acc_empresa"] = empresa;
    obj["acc_proyecto"] = proyecto;
    obj["acc_usuario"] = $("#usuario").data("codigo");
    obj["acc_tiponivel"] = $("#cmbtiponivel_acc").val();

    var filtro = GetFiltroNivelAcceso();

    obj["acc_nivel"] = $(filtro).data("codigo");
    obj["acc_nivelvalor"] = $(filtro).val();

    obj["acc_estado"] = $("#txtriesgos_mar").val();
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SaveAcceso", jsonText, 11);

}








///////////POP UP USUARIO////////////////////////
function AddNewUser(empresa, usuario) {
    var obj = {};
    obj["uxe_empresa"] = empresa;
    obj["uxe_usuario"] = usuario;    
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "EditUsuario", jsonText, "EditUser");
}

function EditUserResult(data) {
    if (data != "") {
        CallPopUp("modEditUsr", "Edición Usuario", "", "", data.d);
        $(".make-switch").bootstrapSwitch();  
        SetTypeAheadTemplate("txtid_usr", "usr_id", "GetUsuario", "<div><strong>{{usr_id}}</strong> – {{usr_nombres}}</div>");

    }
}


function EditUser(empresa, usuario) {
    var obj = {};
    obj["uxe_empresa"] = empresa;
    obj["uxe_usuario"] = usuario;
    obj["usr_id"] = usuario;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "EditUsuario", jsonText, "EditUser");
}


function SaveUsuario() {

    var obj = {};
    obj["uxe_empresa"] = GetOnlineCompany();
    obj["uxe_usuario"] = $("#txtid_usr").val();
    

    obj["usr_id"] = $("#txtid_usr").val();
    obj["usr_password"] = $("#txtpassword_usr").val();
    obj["usr_nombres"] = $("#txtnombres_usr").val();
    obj["usr_mail"] = $("#txtemail_usr").val();
    obj["usr_perfil"] = $("#cmbperfil_usr").val();
    obj["usr_oficina"] = $("#cmboficina_usr").val();
    obj["usr_impresion"] = $("#txtimpresion_usr").val();
    obj["usr_estado"] = GetCheckValue($("#chkestado_usr"));
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SaveUsuario", jsonText, "SaveUser");

}

function SaveUsuarioResult(data) {
     if (data.d != "") {
        var obj = $.parseJSON(data.d);        
        try {
            EndEditUser(obj);
        }
        catch (err) {            
        } 
    }
    
}



///////////POP UP EMPRESAS USUARIO////////////////////////
///////Acceso solo para el usuario root//////////////////
function EmpresasUsuario(usuario) {
    var obj = {};
    obj["usr_id"] = usuario;

    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetEmpresasUsuario", jsonText, 15);
}

function EmpresasUsuarioResult(data) {
    if (data != "") {
        CallPopUp("modEmpUsr", "Empresas Usuario", "", "", data.d);
        $(".make-switch").bootstrapSwitch();


    }
}

function SaveUsuarioEmpresa(usuario, empresa, chk) {
    var obj = {};
    obj["uxe_empresa"] = empresa;
    obj["uxe_usuario"] = usuario;
    obj["uxe_estado"] = GetCheckValue($(chk));
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });


    CallServerPopup(webservice + "SaveUsuarioEmpresa", jsonText, 16);
}



function SelectEmpresa(usuario) {
    var obj = {};
    obj["usuario"] = usuario;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SelectEmpresasUsuario", jsonText, 17);
}

function SelectEmpresaResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        if (obj[0] == "html")
            CallPopUp("modSelEmp", "Seleccione Empresa", "", "", obj[1]);
        else {
            SelectEmpresaOK(obj[1], obj[2]);
        }
    }
}

function SelectEmpresaOK(codigo, nombre, usuario, perfil, nombres) {

    try {
        EndSelectEmpresa(codigo, nombre, usuario, perfil, nombres);
    }
    catch (err) {
    }


}



///////////POP UP EIDT PROYECTO /////////////////////
function CallEditProyecto(empresa, proyecto) {
    var obj = {};
    obj["pro_empresa"] = empresa;
    obj["pro_codigo"] = proyecto;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetProyecto", jsonText, 24);
}

function CallEditProyectoResult(data) {
    if (data != "") {
        CallPopUp("modEditPro", "Edición Proyecto", "bs-modal-lg", "modal-lg", data.d);        
        $('.date-picker').datepicker({
                    rtl: Metronic.isRTL(),
                    format: 'dd/mm/yyyy',
                    orientation: "left",
                    autoclose: true
                });
        $("#tipos").on("change", GetFiltroNivelesGeneral)
        GetFiltroNivelesGeneral();
    }
}

function SaveEditProyecto() {

    var obj = {};
    obj["pro_codigo"] = $("#txtcodigo").val();
    obj["pro_empresa"] = GetOnlineCompany();
    obj["pro_nombre"] = $("#txtnombre").val();
    obj["pro_descripcion"] = $("#txtdescripcion").val();
    obj["pro_general"] = $("#txtgeneral").val();
    obj["pro_especifico"] = $("#txtespecifico").val();
    obj["pro_inicio"] = $("#txtinicio").val();
    obj["pro_fin"] = $("#txtfin").val();
    obj["pro_padre"] = $("#cmbproyectos").val();
    obj["pro_estado"] = GetCheckValue($("#chkactivo"));

    var filtro = GetFiltroNivelGen();
    obj["pro_nivel"] = $(filtro).data("codigo");
    obj["pro_nivelvalor"] = $(filtro).val();

    SetAuditoria(obj);
      
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SaveProyecto", jsonText, 25);

}


function SaveEditProyectoResult(data) {
     if (data.d != "") {
        if (data.d=="ERROR")
        {
            ShowError("Error","No se puede guardar el resultado...");
        }
        else
        {            
            //ShowSuccess("Éxito","El resultado se ha ingresado");
            var obj = $.parseJSON(data.d);
            try {
                EndEditProyecto(obj);
            }
            catch (err) {            
            }            
        }                
    }
    
}






///////////POP UP EDIT RESULTADO////////////////////////


function CallEditResultado(empresa, proyecto, resultado) {
    var obj = {};
    obj["res_empresa"] = empresa;
    obj["res_proyecto"] = proyecto;
    obj["res_codigo"] = resultado;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "EditResultado", jsonText, 20);
}

function CallEditResultadoResult(data) {
    if (data != "") {
        CallPopUp("modEditRes", "Edición Resultado", "bs-modal-lg", "modal-lg", data.d);                 
        if ($.datepicker) {
                $('.date-picker').datepicker({
                    rtl: Metronic.isRTL(),
                    format: 'dd/mm/yyyy',
                    orientation: "left",
                    autoclose: true
                });        
            }         

    }
}

function SaveEditResultado() {

    

    var obj = {};
    obj["res_empresa"] = $("#editresultado").data("empresa");
    obj["res_proyecto"] = $("#editresultado").data("proyecto");
    obj["res_codigo"] = $("#editresultado").data("resultado");

    obj["res_descripcion"] = $("#txtdescripcion_r").val();
    obj["res_estado"] = GetCheckValue($("#chkestado_r"));
    obj["detallemarco"] = GetDetalleMarco("tdmarco");

    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SaveResultado", jsonText, 22);

}


function SaveEditResultadoResult(data) {
     if (data.d != "") {
        if (data.d=="ERROR")
        {
            ShowError("Error","No se puede guardar el resultado...");
        }
        else
        {
            
            //ShowSuccess("Éxito","El resultado se ha ingresado");
            var obj = $.parseJSON(data.d);
            try {
                EndEditResultado(obj);
            }
            catch (err) {            
            }            
        }                
    }
    
}


///////////POP UP EDIT ACTIVIDAD////////////////////////
function CallEditActividad(empresa, proyecto, resultado, actividad) {
    var obj = {};
    obj["act_empresa"] = empresa;
    obj["act_proyecto"] = proyecto;
    obj["act_resultado"] = resultado;
    obj["act_codigo"] = actividad;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "EditActividad", jsonText, 21);
}

function CallEditActividadResult(data) {
    if (data != "") {
        CallPopUp("modEditAct", "Edición Actividad", "bs-modal-lg", "modal-lg", data.d);

    }
}


function SaveEditActividad() {

    

    var obj = {};
    obj["act_empresa"] = $("#editactividad").data("empresa");
    obj["act_proyecto"] = $("#editactividad").data("proyecto");
    obj["act_resultado"] = $("#editactividad").data("resultado");
    obj["act_codigo"] = $("#editactividad").data("actividad");

    obj["act_descripcion"] = $("#txtdescripcion_a").val();
    obj["act_meta"] = $("#txtmeta_a").val();
    obj["act_estado"] = GetCheckValue($("#chkestado_a"));
    obj["detallemarco"] = GetDetalleMarco("tdmarco");

    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SaveActividad", jsonText, 23);

}


function SaveEditActividadResult(data) {
     if (data.d != "") {
        if (data.d=="ERROR")
        {
            ShowError("Error","No se puede guardar la actividad...");
        }
        else
        {
            
            //ShowSuccess("Éxito","El resultado se ha ingresado");
            var obj = $.parseJSON(data.d);
            try {
                EndEditActividad(obj);
            }
            catch (err) {            
            }            
        }                
    }
    
}


/*
function EditUser(empresa, usuario) {
    var obj = {};
    obj["uxe_empresa"] = empresa;
    obj["uxe_usuario"] = usuario;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "AddUser", jsonText, 12);
}


function SaveUsuario(empresa) {

    var obj = {};
    obj["uxe_empresa"] = empresa;
    obj["uxe_usuario"] = $("#txtid_usr").val();

    obj["usr_id"] = $("#txtid_usr").val();
    obj["usr_password"] = $("#txtpassword_usr").val();
    obj["usr_nombres"] = $("#txtnombres_usr").val();
    obj["usr_mail"] = $("#txtmail_usr").val();
    obj["usr_perfil"] = $("#cmbperfil_usr").val();
    obj["usr_estado"] = GetCheckValue($("#chkestado_usr"));
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SaveUsuario", jsonText, 13);

}

function SaveUsuarioResult(data) {
    if (data.d != "") {
        var obj = $.parseJSON(data.d);
        $("#usuario").data("codigo", obj["usr_id"]);
        $("#txtusuario_acc").val(obj["usr_nombres"]);
        $("#usuarioperfil").html(obj["usr_perfil"]);
        try {
            EndEditUser(obj);
        }
        catch (err) {
        }
    }

}
*/


function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtcalidad_mar") {
        return {
            label: item.cal_texto,
            value: item.cal_valor,
            info: item
        }
    }
}

function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtcalidad_mar") {
        //$("#txtIDHOJARUTA").val(item.info.codigo);        
    }
}


/////////////////NUEVOS POPUPS/////////////////////

///////////POP UP COMPAÑIA////////////////////////
function AddCompania(empresa) {
    var obj = {};
    obj["com_empresa"] = empresa;
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "EditCompania", jsonText, "EditCompania");
}

function AddCompaniaResult(data) {
    if (data != "") {
        CallPopUp("modEditCompania", "Edición Compañía", "", "", data.d);
        $(".make-switch").bootstrapSwitch();


    }
}


function EditCompania(empresa, codigo) {
    var obj = {};
    obj["com_empresa"] = empresa;
    obj["com_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "EditCompania", jsonText, "EditCompania");
}



function SaveCompania() {

    var obj = {};
    obj["com_empresa"] = GetOnlineCompany();
    obj["com_codigo"] = $("#editdata").data("codigo");
    
    obj["com_id"] = $("#txtid_com").val();
    obj["com_nombre"] = $("#txtnombre_com").val();
    obj["com_responsable"] = $("#txtresponsable_com").val();
    obj["com_estado"] = GetCheckValue($("#chkestado_com"));
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SaveCompania", jsonText, "SaveCompania");

}

function SaveCompaniaResult(data) {
    if (data.d != "") {
        var obj = $.parseJSON(data.d);
        //$("#usuario").data("codigo", obj["usr_id"]);
        //$("#txtusuario_acc").val(obj["usr_nombres"]);
        //$("#usuarioperfil").html(obj["usr_perfil"]);
        try {
            EndEditRow(obj);
        }
        catch (err) {
        }
    }

}



///////////POP UP CONFIG COMPAÑIA////////////////////////

function ConfigCompania(empresa, codigo) {
    var obj = {};
    obj["com_empresa"] = empresa;
    obj["com_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "ConfigCompania", jsonText, "ConfigCompania");
}

function ConfigCompaniaResult(data) {
    if (data != "") {
        CallPopUp("modConfigCompania", "Configuración Compañía", "", "", data.d);
        $(".make-switch").bootstrapSwitch();
    }
}


function SaveConfigCompania() {

    var obj = {};
    obj["com_empresa"] = GetOnlineCompany();
    obj["com_codigo"] = $("#editdata").data("codigo");

    obj["com_id"] = $("#txtid_com").val();
    obj["com_nombre"] = $("#txtnombre_com").val();
    obj["com_responsable"] = $("#txtresponsable_com").val();
    obj["com_estado"] = GetCheckValue($("#chkestado_com"));
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SaveCompania", jsonText, "SaveCompania");

}
/*
function SaveConfigCompaniaResult(data) {
    if (data.d != "") {
        var obj = $.parseJSON(data.d);
        //$("#usuario").data("codigo", obj["usr_id"]);
        //$("#txtusuario_acc").val(obj["usr_nombres"]);
        //$("#usuarioperfil").html(obj["usr_perfil"]);
        try {
            EndConfigCompania(obj);
        }
        catch (err) {
        }
    }

}*/
//CARGA CANTON  PARROQUIA

function GetCantones(provincia, nombre, valor)
{
    var obj = {};
    obj["ubi_id"] = provincia;
    obj["nombre"]= nombre;
    obj["valor"] = valor;
    
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetCantonesProvincia", jsonText, "GetCantones");
}

function GetCantonesResult(data)
{
    if (data != "") {
        var obj = $.parseJSON(data.d);
        $("#" + obj[0]).html(obj[1]);
        EndChangeProvincia();
    }
}

function GetParroquias(canton, nombre, valor) {
    var obj = {};
    obj["ubi_id"] = canton;
    obj["nombre"] = nombre;
    obj["valor"] = valor;

    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetParroquiasCanton", jsonText, "GetParroquias");
}

function GetParroquiasResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        $("#" + obj[0]).html(obj[1]);
        EndChangeCanton();
    }
}




///////////POP UP OFICINA////////////////////////
function AddOficina(empresa) {
    var obj = {};
    obj["ofi_empresa"] = empresa;
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "EditOficina", jsonText, "EditOficina");
}

function AddOficinaResult(data) {
    if (data != "") {
        CallPopUp("modEditOficina", "Edición Oficina", "", "", data.d);
        $(".make-switch").bootstrapSwitch();
        SetTypeAhead("txtgrupo_ofi", "grupo", "GetListOficinasGrupos");
        $("#cmbprovincia_ofi").on("change", ChangeProvincia);
        ChangeProvincia();

    }
}

function ChangeProvincia()
{
    var prov = $("#cmbprovincia_ofi").val();
    var canton = $("#cmbcanton_ofi").data("valor");
    GetCantones(prov, "cmbcanton_ofi", canton);
}
function EndChangeProvincia()
{
    $("#cmbcanton_ofi").on("change", ChangeCanton);
    ChangeCanton();
}


function ChangeCanton() {
    var canton = $("#cmbcanton_ofi").val();
    var parroquia = $("#cmbparroquia_ofi").data("valor");
    GetParroquias(canton, "cmbparroquia_ofi", parroquia);
}

function EndChangeCanton()
{

}


function EditOficina(empresa, codigo) {
    var obj = {};
    obj["ofi_empresa"] = empresa;
    obj["ofi_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "EditOficina", jsonText, "EditOficina");
}



function SaveOficina() {

    var obj = {};
    obj["ofi_empresa"] = GetOnlineCompany();
    obj["ofi_codigo"] = $("#editdata").data("codigo");

    obj["ofi_id"] = $("#txtid_ofi").val();
    obj["ofi_nombre"] = $("#txtnombre_ofi").val();
    obj["ofi_responsable"] = $("#txtresponsable_ofi").val();

    obj["ofi_provincia"] = $("#cmbprovincia_ofi").val();
    obj["ofi_canton"] = $("#cmbcanton_ofi").val();
    obj["ofi_parroquia"] = $("#cmbparroquia_ofi").val();
    obj["ofi_parroquianombre"] = $("#cmbparroquia_ofi option:selected").text();
    obj["ofi_zip"] = $("#txtzipcode_ofi").val();
    obj["ofi_grupo"] = $("#txtgrupo_ofi").val();


    obj["ofi_estado"] = GetCheckValue($("#chkestado_ofi"));
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SaveOficina", jsonText, "SaveOficina");

}

function SaveOficinaResult(data) {
    if (data.d != "") {
        var obj = $.parseJSON(data.d);
        //$("#usuario").data("codigo", obj["usr_id"]);
        //$("#txtusuario_acc").val(obj["usr_nombres"]);
        //$("#usuarioperfil").html(obj["usr_perfil"]);
        try {
            EndEditRow(obj);
        }
        catch (err) {
        }
    }

}



///////////POP UP MOFICAR ENVIO////////////////////////
function ModicarEnvios(envios) {
    var jsonText = JSON.stringify({ objeto: envios});
    CallServerPopup(webservice + "ModificarEnvio", jsonText, "ModificarEnvio");
}

function ModificarEnviosResult(data) {
    if (data != "") {


        bootbox.dialog({
            message: data.d,
            title: "Edición Datos Envio",            
            buttons: {
                aceptar:
                {
                    label: "Guardar",
                    className: "btn-primary",
                    callback: function () {
                        return SaveModificarEnvios();
                    }
                },
                cancelar:
                {
                    label: "Cancelar",
                    className: "btn-default",
                }
            }
        });

        //CallPopUp("modEditEnvio", "Edición Datos Envio", "", "", data.d);
        
        SetTypeAhead("txtcompania_p", "com_nombre", "GetListCompania");
        SetTypeAhead("txtoficina_p", "ofi_nombre", "GetListOficinas");


    }
}


function SaveModificarEnvios() {

    var obj = {};
    obj["env_empresa"] = GetOnlineCompany();
    obj["env_codigo"] = $("#editdata").data("codigo");
    obj["env_compania"] = $("#txtcompania_p").data("codigo");
    obj["env_companianombre"] = $("#txtcompania_p").val();
    obj["env_oficina"] = $("#txtoficina_p").data("codigo");
    obj["env_oficinanombre"] = $("#txtoficina_p").val();
    obj["env_nombresben"] = $("#txtnombresben_p").val();
    obj["env_telefonoben"] = $("#txttelefonoben_p").val();
    obj["env_bancoben"] = $("#txtbanco_p").val();
    obj["env_cuentaben"] = $("#txtcuenta_p").val();

    obj["env_estado"] = $("#cmbestado_p").val();

    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SaveEnvio", jsonText, "SaveEnvio");

}

function SaveModificarEnviosResult(data) {
    if (data.d != "") {
        var obj = $.parseJSON(data.d);
       
        try {
            EndModificarEnvios(obj);
        }
        catch (err) {
        }
    }

}


///////////POP UP COMISION////////////////////////
function AddComision(empresa,compania, oficina) {
    var obj = {};
    obj["com_empresa"] = empresa;
    obj["com_compania"] = compania;
    obj["com_oficina"] = oficina;
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "EditComision", jsonText, "EditComision");
}

function AddComisionResult(data) {
    if (data != "") {
        CallPopUp("modEditComision", "Edición Comisión", "", "", data.d);
        $(".make-switch").bootstrapSwitch();
        $('.date-picker').datepicker({
            rtl: Metronic.isRTL(),
            format: 'dd/mm/yyyy',
            orientation: "left",
            autoclose: true
        });


    }
}


function EditComision(empresa, codigo, compania, oficina) {
    var obj = {};
    obj["com_empresa"] = empresa;
    obj["com_codigo"] = codigo;
    obj["com_compania"] = compania;
    obj["com_oficina"] = oficina;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "EditComision", jsonText, "EditComision");
}



function SaveComision() {

    var obj = {};
    obj["com_empresa"] = GetOnlineCompany();
    obj["com_codigo"] = $("#editdata").data("codigo");

    obj["com_compania"] = $("#txtcompania_com").val();
    obj["com_oficina"] = $("#txtoficina_com").val();
    obj["com_tipo"] = $("#cmbtipo_com").val();
    obj["com_inicio"] = $("#txtinicio_com").val();
    obj["com_fin"] = $("#txtfin_com").val();
    obj["com_desde"] = $("#txtdesde_com").val();
    obj["com_hasta"] = $("#txthasta_com").val();
    obj["com_valor"] = $("#txtvalor_com").val();
    obj["com_estado"] = GetCheckValue($("#chkestado_com"));
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SaveComision", jsonText, "SaveComision");

}

function SaveComisionResult(data) {
    if (data.d != "") {
        var obj = $.parseJSON(data.d);
        //$("#usuario").data("codigo", obj["usr_id"]);
        //$("#txtusuario_acc").val(obj["usr_nombres"]);
        //$("#usuarioperfil").html(obj["usr_perfil"]);
        try {
            EndEditRow(obj);
        }
        catch (err) {
        }
    }

}


//POP UP CALCULA COMISION/////////////////////

///////////POP UP COMISION////////////////////////
function CalculaComision(empresa, compania, oficina) {
    var obj = {};
    obj["com_empresa"] = empresa;
    obj["com_compania"] = compania;
    obj["com_oficina"] = oficina;
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "CalculaComision", jsonText, "CalculaComision");
}

function CalculaComisionResult(data) {
    if (data != "") {
        CallPopUpButton("modCalculaComision", "Calculo Comisión", "", "", data.d,"Calcular");
        $(".make-switch").bootstrapSwitch();
        $('.date-picker').datepicker({
            rtl: Metronic.isRTL(),
            format: 'dd/mm/yyyy',
            orientation: "left",
            autoclose: true
        });
        $('#cmbcompania_c').select2({
            placeholder: "Seleccione compañias...",
            allowClear: true,
            language: "es"
        });
        $('#cmboficina_c').select2({
            placeholder: "Seleccione oficinas...",
            allowClear: true,
            language: "es"
        });


    }
}



function ShowLoading()
{
    $("#loading").show();
}
function HideLoading()
{
    $("#loading").hide();
} 


function RunCalculaComision() {

    ShowLoading();
    var obj = {};
    obj["com_empresa"] = GetOnlineCompany();
    
    obj["com_compania"] = $("#cmbcompania_c").val();
    obj["com_oficina"] = $("#cmboficina_c").val();
    obj["com_inicio"] = $("#txtdesde_c").val();
    obj["com_fin"] = $("#txthasta_c").val();
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "RunCalculaComision", jsonText, "RunCalculaComision");

}

function RunCalculaComisionResult(data) {
    HideLoading();
    alert(data.d);
    

}


///////////POP UP GET USUARIOS OFICINA////////////////////////
function GetUsuariosOficina(empresa, oficina) {
    var obj = {};
    obj["ofi_empresa"] = empresa;
    obj["ofi_codigo"] = oficina;
    
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetUsuariosOficina", jsonText, "UsersOffice");
}

function GetUsuariosOficinaResult(data) {
    if (data != "") {
        CallPopUp("modUsersOffice", "Usuarios Oficina", "", "", data.d);        
    }
}

///////////POP UP SET PASSWORD ////////////////////////
function SetPassword(usuario) {
    var obj = {};
    obj["usr_id"] = usuario;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SetPassword", jsonText, "SetPass");
}

function SetPasswordResult(data) {
    if (data != "") {
        CallPopUp("modSetPass", "Contraseña Usuario", "", "", data.d);
    }
}

function ValidaPassword()
{
    if ($("#txtpassactual_usr").val() != $("#txtpassword_usr").val())
        return "La contraseña actual no coincide";


    if ($("#txtpassnuevo_usr").val() != $("#txtpassconfirma_usr").val())
        return "Las contraseñas nuevas no coinciden";
    
    return "ok";
}

function SavePassword() {
    var valida = ValidaPassword();
    if (valida =="ok") {
        var obj = {};
        obj["usr_id"] = $("#txtid_usr").val();
        obj["usr_password"] = $("#txtpassnuevo_usr").val();
        SetAuditoria(obj);
        var jsonText = JSON.stringify({ objeto: obj });
        CallServerPopup(webservice + "SavePassword", jsonText, "SavePass");
        return true;
    }
    else
    {
        bootbox.alert(valida);
        return false;
    }

}

function SavePasswordResult(data) {
    if (data.d != "") {
        bootbox.alert(data.d);        
    }

}

///////////////POPUP ADD USER OFFICE///////////////////////

function AddOfficeUser(empresa,oficina)
{
    var obj = {};
    obj["uso_empresa"] = empresa;
    obj["uso_oficina"] = oficina;

    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "EditUsuario", jsonText, "EditOfficeUser");

}

function EditOfficeUserResult(data) {
    if (data != "") {
        CallPopUp("modEditOfficeUser", "Usuario Oficina", "", "", data.d);

        SetTypeAheadTemplate("txtid_usr", "usr_id", "GetUsuario", "<div><strong>{{usr_id}}</strong> – {{usr_nombres}}</div>");
        
        $(".make-switch").bootstrapSwitch();
    }
}

function SaveOfficeUser() {
    var obj = {};

    obj["uso_empresa"] = GetOnlineCompany();
    obj["uso_oficina"] = $("#editdata").data("oficina");
    obj["uso_usuario"] = $("#txtid_usr").val();
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SaveOfficeUsuario", jsonText, "SaveOfficeUser");

}

function SaveOfficeUserResult(data) {
    if (data.d != "") {
        var obj = $.parseJSON(data.d);
        $("#tdlistado_usr tbody").append("<tr data-id='" + obj["uso_usuario"] + "'><td><a href=\"#\" onclick=\"RemoveOfficeUser('" + obj["uso_empresa"] + "','" + obj["uso_oficina"] + "','" + obj["uso_usuario"] + "');\" class=\"btn btn-icon-only btn-circle default\"><i class=\"fa fa-eraser\"></i></a></td><td>" + obj["uso_usuario"] + "</td><td>" + obj["uso_usuarionombres"] + "</td></tr>");
    }

}

function RemoveOfficeUser(empresa, oficina, usuario)
{
    bootbox.dialog({
        message: "¿Está seguro que desea continuar?",
        title: "Pregunta",
        buttons: {
            aceptar:
            {
                label: "Si",
                className: "btn-primary",
                callback: function () {
                    var obj = {};
                    obj["uso_empresa"] = empresa;
                    obj["uso_oficina"] = oficina;
                    obj["uso_usuario"] = usuario;
                    SetAuditoria(obj);
                    var jsonText = JSON.stringify({ objeto: obj });
                    CallServerPopup(webservice + "RemoveOfficeUsuario", jsonText, "RemoveOfficeUser");
                }
            },
            cancelar:
            {
                label: "No",
                className: "btn-default",
            }
        }
    });

  

}

function RemoveOfficeUserResult(data) {
    if (data.d != "") {
        $('#tdlistado_usr tr[data-id="' + data.d + '"]').remove();        
    }

}


//function SaveUser() {
//    var obj = {};

//    obj["uso_empresa"] = GetOnlineCompany();
//    obj["uso_oficina"] = $("#editdata").data("oficina");
//    obj["uso_usuario"] = $("#txtid_usr").val();


    
//    obj["usr_id"] = $("#txtid_usr").val();
//    obj["usr_nombres"] = $("#txtnombres_usr").val();
//    obj["usr_password"] = $("#txtpassword_usr").val();
//    obj["usr_mail"] = $("#txtmail_usr").val();
//    obj["usr_perfil"] = $("#cmbperfil_usr").val();
//    obj["usr_estado"] = GetCheckValue($("#chkestado_usr"));
//    SetAuditoria(obj);
//    var jsonText = JSON.stringify({ objeto: obj });
//    CallServerPopup(webservice + "SaveUsuario", jsonText, "SaveUser");

//}

//function SaveUserResult(data) {
//    if (data.d != "") {
//        var obj = $.parseJSON(data.d);
////        $("#tdlistado_usr tbody").append("<tr><td><a href=\"#\" onclick=\"EditRow('" + obj["usr_id"] + "');\" class=\"btn btn-icon-only btn-circle default\"><i class=\"fa fa-edit\"></i></a><a href=\"#\" onclick=\"RemoveRow('" + obj["usr_id"] + "');\" class=\"btn btn-icon-only btn-circle default\"><i class=\"fa fa-eraser\"></i></a></td><td>" + obj["usr_id"] + "</td><td>" + obj["usr_nombres"] + "</td></tr>");
//    }

//}


///////////POP UP TRANSFERENCIA////////////////////////
function AddTransferencia() {
    var obj = {};
    obj["tra_empresa"] = GetOnlineCompany();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "EditTransferencia", jsonText, "EditTransf");
}

function EditTransferenciaResult(data) {
    if (data != "") {
        CallPopUp("modEditTransf", "Edición Transferencia", "", "", data.d);
        $(".make-switch").bootstrapSwitch();
        $('.date-picker').datepicker({
            rtl: Metronic.isRTL(),
            format: 'dd/mm/yyyy',
            orientation: "left",
            autoclose: true
        });
        $("#cmboficina_tra").on("change", GetIdTransferencia);


    }
}


function EditTransferencia(codigo) {
    var obj = {};
    obj["tra_empresa"] = GetOnlineCompany();
    obj["tra_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "EditTransferencia", jsonText, "EditTransf");
}



function SaveTransferencia() {

    var obj = {};
    obj["tra_empresa"] = GetOnlineCompany();
    obj["tra_codigo"] = $("#editdata").data("codigo");

    obj["tra_fecha"] = $("#txtfecha_tra").val();
    obj["tra_oficina"] = $("#cmboficina_tra").val();
    obj["tra_tipo"] = $("#cmbtipo_tra").val();
    obj["tra_comprobante"] = $("#txtcomprobante_tra").val();
    obj["tra_concepto"] = $("#txtconcepto_tra").val();
    obj["tra_valor"] = $("#txtvalor_tra").val();
    obj["tra_estado"] = GetCheckValue($("#chkestado_tra"));
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SaveTransferencia", jsonText, "SaveTransf");

}

function SaveTransferenciaResult(data) {
    if (data.d != "") {
        var obj = $.parseJSON(data.d);
        try {
            EndEditRow(obj);
        }
        catch (err) {
        }
    }

}



function GetIdTransferencia() {

    var obj = {};
    obj["tra_empresa"] = GetOnlineCompany();
    obj["tra_oficina"] = $("#cmboficina_tra").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetIdTransferencia", jsonText, "GetIdTrans");

}

function GetIdTransferenciaResult(data) {
    if (data.d != "") {
        var obj = $.parseJSON(data.d);
        //$("#txtnumero").val(obj[0]);
        $("#txtid_tra").val(obj[1]);
    }

}


///////////POP UP CANCELACION////////////////////////
function AddCancelacion() {
    var obj = {};
    obj["can_empresa"] = GetOnlineCompany();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "EditCancelacion", jsonText, "EditCancel");
}

function EditCancelacionResult(data) {
    if (data != "") {
        CallPopUp("modEditCancel", "Edición Cancelación", "", "", data.d);
        $(".make-switch").bootstrapSwitch();
        $('.date-picker').datepicker({
            rtl: Metronic.isRTL(),
            format: 'dd/mm/yyyy',
            orientation: "left",
            autoclose: true
        });
        $("#cmbcompania_can").on("change", GetIdCancelacion);
        var cod = $("#editdata").data("codigo");
        if (cod != null && cod != undefined && cod != "")
            $("#btnokmodEditCancel").hide();


    }
}


function EditCancelacion(codigo) {
    var obj = {};
    obj["can_empresa"] = GetOnlineCompany();
    obj["can_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "EditCancelacion", jsonText, "EditCancel");
}





function SaveCancelacion(detalle) {
    

    var obj = {};
    obj["can_empresa"] = GetOnlineCompany();
    obj["can_codigo"] = $("#editdata").data("codigo");

    obj["can_fecha"] = $("#txtfecha_can").val();
    obj["can_compania"] = $("#cmbcompania_can").val();
    obj["can_tipo"] = $("#cmbtipo_can").val();
    obj["can_comprobante"] = $("#txtcomprobante_can").val();
    obj["can_concepto"] = $("#txtconcepto_can").val();
    obj["can_valor"] = $("#txtvalor_can").val();
    obj["can_estado"] = GetCheckValue($("#chkestado_can"));
    obj["detalle"] = detalle;
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SaveCancelacion", jsonText, "SaveCancel");

}

function SaveCancelacionResult(data) {
    if (data.d != "") {
        KillPopUp("modEditCancel");
        var obj = $.parseJSON(data.d);
        try {
            EndEditRow(obj);
        }
        catch (err) {
        }
    }

}



function GetIdCancelacion() {

    var obj = {};
    obj["can_empresa"] = GetOnlineCompany();
    obj["can_compania"] = $("#cmbcompania_can").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetIdCancelacion", jsonText, "GetIdCancel");

}

function GetIdCancelacionResult(data) {
    if (data.d != "") {
        var obj = $.parseJSON(data.d);
        //$("#txtnumero").val(obj[0]);
        $("#txtid_can").val(obj[1]);
    }

}


////////////POP UP PAGOS PENDIENTES //////////////////////

function GetPagosPendientes(codigo) {
    var obj = {};
    obj["can_empresa"] = GetOnlineCompany();
    obj["can_compania"] = $("#cmbcompania_can").val();
    obj["can_id"] = $("#txtid_can").val();
    obj["can_valor"] = $("#txtvalor_can").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetPagosPendientes", jsonText, "GetPagosPen");
}

function GetPagosPendientesResult(data) {
    if (data != "") {

        bootbox.dialog({
            message: data.d,
            title: "Pagos Pendientes",
            size: "large",
            buttons: {
                aceptar:
                {
                    label: "Guardar",
                    className: "btn-primary",
                    callback: function () {
                        return GetCancelaciones();
                    }
                },
                cancelar:
                {
                    label: "Cancelar",
                    className: "btn-default",                    
                }
            }
        });



        //CallPopUp("modPagosPen", "Pagos Pendientes", "", "", data.d);

        $(".valorpago").on("change", CalculaCancelacion);
    }
}

function CalculaCancelacion()
{

    var htmltable = $("#tdpagos")[0];
    var total = 0;

    for (var r = 1; r < htmltable.rows.length; r++) {
        var row = htmltable.rows[r];
        //var valor = ($.isNumeric($(row.cells[6]).children("input").val())) ? parseFloat($(row.cells[6]).children("input").val()) : 0;
        var valor = GetFloat($(row.cells[6]).children("input").val());

        total += valor;
    
    }

    var monto = GetFloat($("#txtvalor_canp").val());
    var saldo = monto - total;
    $("#txtsuma_canp").val(CurrencyFormatted(total));
    $("#txtsaldo_canp").val(CurrencyFormatted(saldo));

}


function GetCancelaciones()
{
    var save = true;
    var error = "";

    if (GetFloat($("#txtsaldo_canp").val()) >= 0) {

        var detalle = new Array();
        var htmltable = $("#tdpagos")[0];
        for (var r = 1; r < htmltable.rows.length; r++) {
            var maximo = GetFloat($(htmltable.rows[r]).data("saldo"));
            var obj = GetCancelacion(htmltable.rows[r]);
            if (obj != null) {
                if (obj["dca_valor"] > 0) {
                    if (obj["dca_valor"] <= maximo)//CONTROLA QUE NO SE PAGUE MAS DEL TOTAL
                        detalle[r] = obj;
                    else {
                        save = false;
                        error += obj["dca_idpago"] + " El valor no debe ser mayor al saldo (" + maximo + ")<br/>";
                    }
                }
            }
        }
    }
    else
    {
        save = false;
        error = "El valor excede al total transferido (Saldo:" + $("#txtsaldo_canp").val() + ")";
    }
    if (save)
        SaveCancelacion(detalle);        
    else
        bootbox.alert(error);
    return save;

}


function GetCancelacion(row) {
    var obj = {};
    obj["dca_empresa"] = GetOnlineCompany();
    //obj["dca_cancelacion"] = $(row).data("comprobante").toString();
    //obj["dca_idcancelacion"] = $(row).data("transac").toString();    
    obj["dca_pago"] = $(row).data("codigo");
    obj["dca_idpago"] = $(row.cells[0]).text();
    obj["dca_valor"] = GetFloat($(row.cells[6]).children("input").val());
    //obj["dca_valor"] = parseFloat($(row.cells[6]).children("input").val().replace(",","."));
    return obj;
}

function SendVal(row, valor)
{
    $(row.cells[6]).children("input").val(valor.replace(",", "."));
    CalculaCancelacion();
    //var valor = ($.isNumeric($(row.cells[6]).children("input").val())) ? parseFloat($(row.cells[6]).children("input").val()) : 0;
}


////////////POP UP PAGOS AFECTADOS //////////////////////

function GetPagosAfectados(codigo) {
    var obj = {};
    obj["can_empresa"] = GetOnlineCompany();
    obj["can_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetPagosAfectados", jsonText, "GetPagosAfe");
}

function GetPagosAfectadosResult(data) {
    if (data != "") {
        bootbox.dialog({
            message: data.d,
            title: "Pagos Afectados",
            size: "large",
            buttons: {                
                aceptar:
                {
                    label: "Aceptar",
                    className: "btn-default",
                }
            }
        });

    }
}


///////////POP UP READ IMPORT FILE////////////////////////
function ReadImpFile(codigo) {
    var obj = {};
    obj["imp_empresa"] = GetOnlineCompany();
    obj["imp_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "ReadImpFile", jsonText, "ReadImpFile");
}

function ReadImpFileResult(data) {
    if (data != "") {
        CallPopUpExtraButton("modReadImpFile", "Edición Archivo", "", "", data.d, "<button type='button'  onclick='ProcessImpFile()'  class='btn green'>Procesar</button>");
        $(".make-switch").bootstrapSwitch();

    }
}


function ProcessImpFile() {
    var obj = {};
    obj["imp_empresa"] = GetOnlineCompany();
    obj["imp_codigo"] = $("#divdata").data("codigo");
    var detalle = new Array();
    var htmltable = $("#tdimpfile")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var row = htmltable.rows[r];
        var objd = {};
        objd["cve_confirmacion"] = $(row.cells[0]).text();
        var estado = GetCheckValue($(row).find("input:checkbox"));
        objd["status"] = estado ? "S" : "N";
        objd["fec_cancelacion"] = $(row.cells[2]).text();
        objd["razon"] = $(row).find("input:text").val();
        detalle[r] = objd;
    }
    obj["file"] = detalle;
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "ProcessImpFile", jsonText, "SaveImpFile");
}

function SaveImpFile()
{
    var obj = {};
    obj["imp_empresa"] = GetOnlineCompany();
    obj["imp_codigo"] = $("#divdata").data("codigo");
    

    var detalle = new Array();
    var htmltable = $("#tdimpfile")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var row = htmltable.rows[r];
        var objd = {};
        objd["cve_confirmacion"] = $(row.cells[0]).text();
        var estado = GetCheckValue($(row).find("input:checkbox"));
        objd["status"] = estado ? "S" : "N";
        objd["fec_cancelacion"] = $(row.cells[2]).text();
        objd["razon"] = $(row).find("input:text").val();
        detalle[r] = objd;
    }
    obj["file"] = detalle;
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "SaveImpFile", jsonText, "SaveImpFile");
}

function SaveImpFileResult(data) {
    if (data.d != "") {
        KillPopUp("modReadImpFile");
    }

}



///////////POP UP GENERAR ESTADO CUENTA////////////////////////
function GenerarEstadoCuenta(empresa, compania) {
    var obj = {};
    obj["imp_empresa"] = empresa;
    obj["imp_compania"] = compania;        
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GenEstadoCuenta", jsonText, "GenEstadoCuenta");
}

function GenerarEstadoCuentaResult(data) {
    if (data != "") {
        CallPopUpButton("modGenerarEstadoCuenta", "Generar Estado Cuenta", "", "", data.d, "Generar");
        $(".make-switch").bootstrapSwitch();
        $('.date-picker').datepicker({
            rtl: Metronic.isRTL(),
            format: 'dd/mm/yyyy',
            orientation: "left",
            autoclose: true
        });
        //$('#cmbcompania_c').select2({
        //    placeholder: "Seleccione compañias...",
        //    allowClear: true,
        //    language: "es"
        //});       


    }
}

function RunGenerarEstadoCuenta() {

    ShowLoading();
    var obj = {};
    obj["imp_empresa"] = GetOnlineCompany();

    obj["imp_compania"] = $("#cmbcompania_c").val();
    obj["imp_fechadesde"] = $("#txtdesde_c").val();
    obj["imp_fechahasta"] = $("#txthasta_c").val();
    SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "RunGenerarEstadoCuenta", jsonText, "RunGenerarEstadoCuenta");

}

function RunCalculaComisionResult(data) {
    HideLoading();
    alert(data.d);


}


///////////POP UP ENVIO PREVIEW  ////////////////////////
function ViewEnvio(empresa, codigo) {
    var obj = {};
    obj["env_empresa"] = empresa;
    obj["env_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetEnvioView", jsonText, "GetEnvioView");
}

function ViewEnvioInvo(empresa, compania, invoice) {
    var obj = {};
    obj["env_empresa"] = empresa;
    obj["env_compania"] = compania;
    obj["env_numero3"] = invoice;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetEnvioView", jsonText, "GetEnvioView");
}

function ViewEnvioResult(data) {
    if (data != "") {

        var obj = $.parseJSON(data.d);
        var env = obj[0];

        bootbox.dialog({
            message: obj[1],
            title: env["env_id"] + " Ref:"+env["env_numero2"]+" Invoice:"+env["env_numero3"],
            size: "large",
            buttons: {               
                aceptar:
                {
                    label: "Ok",
                    className: "btn-default",
                }
            }
        });

        //CallPopUp("modViewEnvio", env["id"], "", "", obj[1]);

    }
}


///////////POP UP DETALLE ESTADO CUENTA OFICINA COMPANIA ////////////////////////
function ViewEstadoCuentaOficinaCompanias(empresa, oficina, fecha, grupo) {
    var obj = {};
    obj["ofi_empresa"] = empresa;
    obj["ofi_codigo"] = oficina;
    obj["fecha"] = fecha;
    obj["group"] = grupo;

    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPopup(webservice + "GetEstadoCuentaOficinaCompanias", jsonText, "GetECOfiCom");
}



function ViewEstadoCuentaOficinaCompaniasResult(data) {
    if (data != "") {
        
        bootbox.dialog({
            message:data.d,
            title: "Detalle Estado Cuenta Oficina Compañías",
            size: "large",
            buttons: {
                aceptar:
                {
                    label: "Ok",
                    className: "btn-default",
                }
            }
        });
        $(".make-switch").bootstrapSwitch();

    }
}