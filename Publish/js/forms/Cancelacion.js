//Archivo:          Cancelacion.js
//Descripción:      Contiene las funciones comunes para la interfaz de gestion de cancelaciones de deudas 
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Octubre 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfCancelacion.aspx";
var menuoption = "Cancelacion";

var afectacionobj = null;
var saving = false;

function StopPropagation(event) {
    if (!event) var event = window.event;
    event.cancelBubble = true;
    if (event.stopPropagation) event.stopPropagation();
}

//Codigo ejecutado cuando el document esta listo
$(document).ready(function () {


    $("#comcabecera").find('.widgettitle .close').click(function () {
        $(this).parents('.widgetbox').fadeOut(function () {
            $(this).hide('fast', HideCabecera());
        });
    });

    $("#comdetalle").find('.widgettitle .close').click(function () {
        $(this).parents('.widgetbox').fadeOut(function () {
            $(this).hide('fast', HideDetalle());
        });
    });

    $('body').css('background', 'transparent');

    GetCabeceraComprobante($("#txtcodigocomp").val(), $("#txttipodoc").val()); //FUNCION DEFINIDA EN FUNCTIONS.JS
});


//Funcion que recupera la cabecera de un comprobante nuevo o de la bd
function GetCabeceraComprobanteResult(data) {
    var obj = JSON.parse(data);
    $("#txtcodigocomp").val(obj[0]);

    $('#comcab').html(obj[1]);


    //$('#comcab').html(html);
    $("#save").on("click", SaveObj);
    $("#print").on("click", PrintObj);
    $("#contabilizacion").on("click", ContObj);
    $("#close").on("click", SaveObj);
    var codigocomp = $("#txtcodigocomp").val();
    if (codigocomp <= 0) {
        $("#contabilizacion").css({ 'display': 'none' });
        $("#print").css({ 'display': 'none' });
        $("#close").css({ 'display': 'none' });
    }

    $("#invo").css({ 'display': 'none' });
    $("#view").css({ 'display': '' });
    $("#view").on("click", ViewObj);
    LoadCabecera();
    LoadPie();
}



function OpenMenuOption(obj) {
    if ($(obj).length > 0) {
        var padre = $(obj)[0].parentNode.parentNode;
        if ($(padre).hasClass("dropdown")) {
            $(padre).children("ul").css("display", "block");
            OpenMenuOption(padre);
        }
        else
            $(obj).addClass("active");
    }
}


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
                LoadPieResult(data);
            if (retorno == 3)
                LoadProductResult(data);
            if (retorno == 4)
                GetPriceResult(data);
            if (retorno == 5)
                SaveObjResult(data);
            if (retorno == 8)
                CloseObjResult(data);

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            if (retorno == 5 || retorno == 8)
                afectacionobj = null;
            var errorData = $.parseJSON(XMLHttpRequest.responseText);
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert(errorData.Message, 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }

    })
}




function LoadCabecera() {
    var origen = $("#txtorigen").val();
    if (origen == "") {
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = $("#txtcodigocomp").val();
        obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
        obj["com_ctipocom"] = parseInt($("#cmbSIGLA_P").val());  //3 REC    
        var jsonText = JSON.stringify({ objeto: obj });
        if ($("#comcabeceracontent").length > 0) {
            CallServer(formname + "/GetCabecera", jsonText, 0);
        }
    }
    else if (origen == "FAC") { //FACTURA
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = $("#txtcodigocompref").val();
        obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
        obj["com_ctipocom"] = parseInt($("#cmbSIGLA_P").val());  //3 REC    
        var jsonText = JSON.stringify({ objeto: obj });
        if ($("#comcabeceracontent").length > 0) {
            CallServer(formname + "/GetCabeceraFromFAC", jsonText, 0);
        }
    }
    else if (origen == "LGC") { //PLANILLA SOCIO
        var obj = {};
        obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        obj["com_codigo"] = $("#txtcodigocompref").val();
        obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
        obj["com_ctipocom"] = parseInt($("#cmbSIGLA_P").val());  //3 REC    
        var jsonText = JSON.stringify({ objeto: obj });
        if ($("#comcabeceracontent").length > 0) {
            CallServer(formname + "/GetCabeceraFromLGC", jsonText, 0);
        }
    }

}

function LoadCabeceraResult(data) {
    if (data != "") {
        $('#comcabeceracontent').html(data.d);
        LoadDetalle();
    }
    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha



    // Select with Search
    $(".chzn-select").chosen();
    // tabbed widget
    $(".tabbedwidget").tabs();
    SetForm(); //Depende de cada js.
}

function LoadDetalle() {
     var origen = $("#txtorigen").val();
     if (origen == "") {
         var obj = {};
         obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
         obj["com_codigo"] = $("#txtcodigocomp").val();
         var jsonText = JSON.stringify({ objeto: obj });
         CallServer(formname + "/GetDetalle", jsonText, 1);
     }
     else if (origen == "FAC") { //FACTURA
         var obj = {};
         obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
         obj["com_codigo"] = $("#txtcodigocompref").val();
         var jsonText = JSON.stringify({ objeto: obj });
         CallServer(formname + "/GetDetalleFromFAC", jsonText, 1);
     }
     else if (origen == "LGC") { //FACTURA
         var obj = {};
         obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
         obj["com_codigo"] = $("#txtcodigocompref").val();
         var jsonText = JSON.stringify({ objeto: obj });
         CallServer(formname + "/GetDetalleFromLGC", jsonText, 1);
     }
}

function LoadDetalleResult(data) {
    if (data != "") {
        $('#comdetallecontent').html(data.d);
    }
    SetFormDetalle();
    //EditableRow("detalletabla");

}

function LoadPie() {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    var origen = $("#txtorigen").val();
    if (origen == "") {
        obj["com_codigo"] = $("#txtcodigocomp").val();
        var jsonText = JSON.stringify({ objeto: obj });
        CallServer(formname + "/GetPie", jsonText, 2);
    }
    else if (origen == "FAC" || origen == "LGC")  //FACTURA
    {
        obj["com_codigo"] = $("#txtcodigocompref").val();
        var jsonText = JSON.stringify({ objeto: obj });
        CallServer(formname + "/GetPieFrom", jsonText, 2);
    }
    

}

function LoadPieResult(data) {
    if (data != "") {
        $('#compiecontent').html(data.d);
    }


}

function EditableRow(idtabla) {
    var columnas = $("#" + idtabla).find("th");
    var newrow = "<tr>";
    for (var i = 0; i < columnas.length; i++) {
        newrow += "<td><input type='text' value='" + $(columnas[i])[0].innerHTML + "'/></td>";
    }
    newrow += "</tr>";
    $("#" + idtabla).append(newrow);

}


function SetForm() {

    SetAutocompleteById("txtCODCLIPRO");

}

function SetFormDetalle() {
    SetAutocompleteById("txtIDTIPO");
    $("#txtVALOR").on("change", CalculaTotales);
    /*if ($("#txtESTADO").val() == $("#txtCERRADO").val()) {
        var inputs = $('input, textarea, select');
        $(inputs).each(function () {
            $(this).prop("disabled", true);

        });
    }*/
    
}

function RemoveRow(btn) {
    var row = $(btn).parents('tr');
    var hascontrols = ($(row).find("input").length > 0) ? true : false;
    if ($(row)[0].id != "editrow" && !hascontrols) {
        jConfirm('¿Está seguro que desea eliminar el registro?', 'Eliminar', function (r) {
            if (r) {
                $(btn).parents('tr').fadeOut(function () {
                    $(this).remove();
                });
            }
        });
    }
    else
        CleanRow();
    CalculaTotales();
    StopPropagation();
}

function CleanRow() {
    $("#txtIDTIPO").val("");
    $("#txtCODTIPO").val("");
    $("#txtNOMBRETIPO").val("");
    $("#txtVALOR").val(0);
    $("#txtIDTIPO").select();
    return false;
}

function SetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCODCLIPRO") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_id,
            info: item
        }
    }

    if (idobj == "txtIDTIPO") {
        return {
            label: item.tpa_id + "," + item.tpa_nombre,
            value: item.tpa_id,
            info: item
        }
    }
    if (idobj == "txtCIRUC_P") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_ciruc,
            info: item
        }
    }
    if (idobj == "txtNOMBRESRED") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_apellidos + " " + item.per_nombres,
            info: item
        }
    }
}

function GetAutoCompleteObj(idobj, item) {
    if (idobj == "txtCODCLIPRO") {
        $("#txtCODPER").val(item.info.per_codigo);
        $("#txtCIRUC").val(item.info.per_ciruc);
        $("#txtNOMBRES").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtRAZON").val(item.info.per_razon);
        $("#txtRUC").val(item.info.per_ciruc);
        $("#txtDIRECCION").val(item.info.per_direccion);
        $("#txtTELEFONO").val(item.info.per_telefono);

        /*$("#txtCODLIS").val(item.info.per_listaprecio);
        $("#txtIDLIS").val(item.info.per_listaid);
        $("#txtLISTA").val(item.info.per_listanombre);

        $("#txtCODPOL").val(item.info.per_politica);
        $("#txtIDPOL").val(item.info.per_politicaid);
        $("#txtPOLITICA").val(item.info.per_politicanombre);
        $("#txtNROPAGOS").val(item.info.per_politicanropagos);
        $("#txtDIASPLAZO").val(item.info.per_politicadiasplazo);
        $("#txtPORCENTAJE").val(item.info.per_politicadesc);


        $("#txtCODVEN").val(item.info.per_agenteid);
        $("#txtVENDEDOR").val(item.info.per_agentenombre);

        //AUTOMATICAMENTE IGUALA EL CLIENTE CON EL REMITENTE
        $("#txtCODREM").val(item.info.per_codigo);
        $("#txtIDREM").val(item.info.per_id);
        $("#txtCIRUCREM").val(item.info.per_ciruc);
        $("#txtNOMBRESREM").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtDIRECCIONREM").val(item.info.per_direccion);
        $("#txtTELEFONOREM").val(item.info.per_telefono);*/

        //alert(ui.item.label);
    }
    if (idobj == "txtNOMBRESRED") {
        $("#txtCODRED").val(item.info.per_codigo);
        $("#txtCIRUCRED").val(item.info.per_ciruc);
        $("#txtNOMBRESRED").val(item.info.per_apellidos + " " + item.info.per_nombres);
        $("#txtDIRECCIONRED").val(item.info.per_direccion);
        $("#txtTELEFONORED").val(item.info.per_telefono);
    }
    if (idobj == "txtIDTIPO") {
        $("#txtCODTIPO").val(item.info.tpa_codigo);
        $("#txtNOMBRETIPO").val(item.info.tpa_nombre);
        $("#txtIDTIPO").val(item.info.tpa_id);
        ShowCampos();
    }
    if (idobj == "txtCIRUC_P") {
        $("#txtCODIGO_P").val(item.info.per_codigo);
        $("#txtCIRUC_P").val(item.info.per_ciruc);
        $("#txtNOMBRES_P").val(item.info.per_nombres);
        $("#txtAPELLIDOS_P").val(item.info.per_apellidos);
        $("#txtRAZON_P").val(item.info.per_razon);
        $("#txtDIRECCION_P").val(item.info.per_direccion);
        $("#txtTELEFONO_P").val(item.info.per_telefono);
        avilible_save();
    }
}





/*********************FUNCIONES PARA CALCULO DE TOTALES **********************/


function CalculaTotales() {
    var htmltable = $("#tdinvoice")[0];
    var total = 0;
    

    for (var r = 0; r < htmltable.rows.length; r++) {
        var c = htmltable.rows[r].cells.length - 2; //celda cantidad
        var hijoval = $(htmltable.rows[r].cells[c]).children("input");
        var valor = 0;

        if (hijoval.length > 0) {
            valor = $(hijoval).val();
        }
        else {
            valor = $(htmltable.rows[r].cells[c]).text();
        }

        total += ($.isNumeric(valor)) ? parseFloat(valor) : 0;        
        
    }
    
    $("#txtTOTALCOM").val(CurrencyFormatted(total));
}


/*

function CalculaAfectacion() {
    var htmltable = $("#tdafec_P")[0];
    var total = 0;


    for (var r = 1; r < htmltable.rows.length; r++) {
        var c = htmltable.rows[r].cells.length - 1; //celda cantidad
        var hijoval = $(htmltable.rows[r].cells[c]).children("input");
        var valor = 0;

        if (hijoval.length > 0) {
            valor = $(hijoval).val();
        }        
        total += ($.isNumeric(valor)) ? parseFloat(valor) : 0;

    }

    var saldo = parseFloat($("#txtVALOR_P").val()) - total;
    $("#txtSALDO_P").val(CurrencyFormatted(saldo));
    $("#tdafec_P_f8").html(CurrencyFormatted(total));
}
*/

/*********************FUNCIONES PARA AGREGAR y EDITAR UN DETALLE**********************/

function AddEditRow() {

    var strdata = "data-codtipo='" + $("#txtCODTIPO").val() + "' " +
    "data-emisor='" + $("#txtEMISOR").val() + "' " +
    "data-nrodocumento='" + $("#txtNRODOCUMENTO").val() + "' " +
    "data-nrocuenta='" + $("#txtNROCUENTA").val() + "' " +
    "data-banco='" + $("#cmbBANCO").val() + "' " +
    "data-nrocheque='" + $("#txtNROCHEQUE").val() + "' " +
    "data-beneficiario='" + $("#txtBENEFICIARIO").val() + "' " +
    "data-fecha='" + $("#txtFECHAVENCE").val() + "' " +
    "data-cuenta='" + $("#txtIDCUENTA").val() + "' "+
    "data-cuentanombre='" + $("#txtNOMBRECUENTA").val() + "' ";

    //var newrow = $("<tr data-codtipo='" + $("#txtCODTIPO").val() + "'  onclick='Edit(this);'></tr>");
    

    var newrow = $("<tr "+strdata +" onclick='Edit(this);'></tr>");
    newrow.append("<td class='' >" + $("#txtIDTIPO").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtNOMBRETIPO").val() + "</td>");
    newrow.append("<td class='right' >" + $("#txtVALOR").val() + "</td>");
    newrow.append("<td class='center' ><div class='removablerow' onclick='RemoveRow(this)'><span class=\"icon-trash\" ></span></div></td>");

    
    var editrow = $("#editrow");
    newrow.insertBefore(editrow);

    $("#txtCODTIPO").val("");
    $("#txtIDTIPO").val("");
    $("#txtNOMBRETIPO").val("");
    $("#txtVALOR").val(0);
    $("#txtIDTIPO").select();

    $("#txtEMISOR").val("");
    $("#txtNRODOCUMENTO").val("");
    $("#txtNROCUENTA").val("");
    $("#cmbBANCO").val("");
    $("#txtNROCHEQUE").val("");
    $("#txtBENEFICIARIO").val("");
    $("#txtFECHAVENCE").val("");
    $("#txtIDCUENTA").val("");
    $("#txtNOMBRECUENTA").val("");
    


}


function MoveTo(control, destino) {
    var obj = $(control).detach();
    $(destino).append(obj);
}


function EditAfec(row) {
    var valor = parseFloat($(row.cells[8]).text());
    $(row.cells[8]).text("");
    var inputvalor = $("<input type='text' style='width:auto;' value='" + valor + "'/>");
    $(row.cells[8]).append(inputvalor);
}

function SelectRow(row) {
    var htmltable = $("#tdinvoice")[0];
    var contador = 0;
    for (var r = 1; r < htmltable.rows.length; r++) {
        if ($(htmltable.rows[r]).css("background-color") == rgbcolor) {
            Mark(htmltable.rows[r]);
        }
    }

    Mark(row);
    $("#txtEMISOR").val($(row).data("emisor"));
    $("#txtNRODOCUMENTO").val($(row).data("nrodocumento"));
    $("#txtNROCUENTA").val($(row).data("nrocuenta"));
    $("#cmbBANCO").val($(row).data("banco"));
    $("#txtNROCHEQUE").val($(row).data("nrocheque"));
    $("#txtBENEFICIARIO").val($(row).data("beneficiario"));
    $("#txtFECHAVENCE").val($(row).data("fecha"));
    $("#txtIDCUENTA").val(($(row).data("cuenta") != null) ? $(row).data("cuenta").toString() : "");
    $("#txtNOMBRECUENTA").val(($(row).data("cuentanombre") != null) ? $(row).data("cuentanombre").toString() : "");
}

function Edit(row) {

    var r = $("#txtCODTIPO")[0].parentNode.parentNode;
    var cod = $("#txtCODTIPO").val();
    var id = $("#txtIDTIPO").val();
    var nombre = $("#txtNOMBRETIPO").val();
    var valor = $("#txtVALOR").val();

    var emisor = $("#txtEMISOR").val();

    var emisor = $("#txtEMISOR").val();
    var nrodocumento = $("#txtNRODOCUMENTO").val();
    var nrocuenta = $("#txtNROCUENTA").val();
    var banco = $("#cmbBANCO").val();
    var nrocheque = $("#txtNROCHEQUE").val();
    var beneficiario = $("#txtBENEFICIARIO").val();
    var fecha = $("#txtFECHAVENCE").val();
    var cuenta = $("#txtIDCUENTA").val();
    var cuentanombre = $("#txtNOMBRECUENTA").val();


    $("#txtCODTIPO").val($(row).data("codtipo").toString());
    $("#txtIDTIPO").val($(row.cells[0]).text()); $(row.cells[0]).text("");
    $("#txtNOMBRETIPO").val($(row.cells[1]).text()); $(row.cells[1]).text("");  
    $("#txtVALOR").val($(row.cells[2]).text()); $(row.cells[2]).text("");
    $("#txtIDTIPO").focus();

    $("#txtEMISOR").val($(row).data("emisor").toString());
    $("#txtNRODOCUMENTO").val($(row).data("nrodocumento").toString());
    $("#txtNROCUENTA").val($(row).data("nrocuenta").toString());

    /*$("#cmbBANCO").val($(row).data("banco").toString());
    $("#txtNROCHEQUE").val($(row).data("nrocheque").toString());
    $("#txtBENEFICIARIO").val($(row).data("beneficiario").toString());
    $("#txtFECHAVENCE").val($(row).data("fecha").toString());
    $("#txtIDCUENTA").val(($(row).data("cuenta")!=null)?$(row).data("cuenta").toString():"");
    $("#txtNOMBRECUENTA").val(($(row).data("cuentanombre")!=null)?$(row).data("cuentanombre").toString():"");
    */

    $("#cmbBANCO").val($(row).data("banco"));
    $("#txtNROCHEQUE").val($(row).data("nrocheque"));
    $("#txtBENEFICIARIO").val($(row).data("beneficiario"));
    $("#txtFECHAVENCE").val($(row).data("fecha"));
    $("#txtIDCUENTA").val(($(row).data("cuenta") != null) ? $(row).data("cuenta").toString() : "");
    $("#txtNOMBRECUENTA").val(($(row).data("cuentanombre") != null) ? $(row).data("cuentanombre").toString() : "");
    


    MoveTo($("#txtIDTIPO"), $(row.cells[0]));
    MoveTo($("#txtCODTIPO"), $(row.cells[0]));
    MoveTo($("#txtNOMBRETIPO"), $(row.cells[1]));
    MoveTo($("#txtVALOR"), $(row.cells[2]));


    $(r).data("codtipo", cod);
    $(r).data("emisor", emisor);
    $(r).data("nrodocumento", nrodocumento);
    $(r).data("nrocuenta", nrocuenta);
    $(r).data("banco", banco);
    $(r).data("nrocheque", nrocheque);
    $(r).data("beneficiario", beneficiario);
    $(r).data("fecha", fecha);
    $(r).data("cuenta", cuenta);
    $(r).data("cuentanombre", cuentanombre);



    $(r.cells[0]).text(id);
    $(r.cells[1]).text(nombre);
    $(r.cells[2]).text(valor);
    
    $(r).attr('onclick', 'Edit(this)');
    $(row).removeAttr('onclick');

    $("#txtIDTIPO").select();
    ShowCampos(); 

}

function ShowCampos() {

    var htmltable = $("#tddatos")[0];
    var total = 0;

    /*for (var r = 0; r < htmltable.rows.length; r++) {
        $(htmltable.rows[r]).hide();
    }

    var nom = $("#txtNOMBRETIPO").val();

    var id = $("#txtIDTIPO").val();
    //if (id == "TP001" || ) {//EFECTIVO
    if (nom.indexOf("EFECTIVO")>=0){
        $(htmltable.rows[7]).show(); //CTA
    }
    //if (id == "TP002") {//CHEQUE
    if (nom.indexOf("CHEQUE")>=0){
        $(htmltable.rows[0]).show();//EMISOR
        $(htmltable.rows[2]).show(); //NRO CUENTA
        $(htmltable.rows[6]).show(); //FECHA VEN
        $(htmltable.rows[7]).show();//CTA
    }
    //if (id == "TP003") {//TARJETA
    if (nom.indexOf("TARJETA")>=0){
        $(htmltable.rows[0]).show(); //EMISOR
        $(htmltable.rows[1]).show(); //NRO DOCUMENTO
        $(htmltable.rows[6]).show(); //FECHA VEN
        $(htmltable.rows[7]).show(); //CTA

    }
    //if (id == "TP004") {//DEPOSITO
    if (nom.indexOf("DESPOSITO")>=0){
        $(htmltable.rows[3]).show(); //BANCO
        $(htmltable.rows[1]).show(); //NRO DOCUMENTO
        $(htmltable.rows[2]).show(); //NRO CUENTA
        $(htmltable.rows[6]).show(); //FECHA VEN
        

    }*/
}



/***********************SAVE FUNCTIONS ***********************************/

function GetTotalObj() {
    var obj = {};
    obj["tot_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["tot_comprobante"] = $("#txtcodigocomp").val();


    obj["tot_total"] = ($.isNumeric($("#txtTOTALCOM").val())) ? parseFloat($("#txtTOTALCOM").val()) : null;
   

    return obj;
}

function GetDetalle() {
    var detalle = new Array();
    var htmltable = $("#tdinvoice")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var obj = GetDreciboObj(htmltable.rows[r]);
        if (obj != null)
            detalle[r] = obj;
    }
    return detalle;
}

function GetDreciboObj(row) {
    var obj = {};
    obj["dfp_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["dfp_comprobante"] = $("#txtcodigocomp").val();

    var hijocodpro = $(row.cells[0]).children("input");
    if (hijocodpro.length > 0) {
        if ($.isNumeric($("#txtCODTIPO").val())) {
            obj["dfp_tipopago"] = parseInt($("#txtCODTIPO").val());
            obj["dfp_tipopagoid"] = $("#txtIDTIPO").val();
            obj["dfp_tipopagonombre"] = $("#txtNOMBRETIPO").val();
            obj["dfp_monto"] = parseFloat($("#txtVALOR").val());
            obj["dfp_nro_documento"] = $("#txtNRODOCUMENTO").val();
            obj["dfp_nro_cuenta"] = $("#txtNROCUENTA").val();
            obj["dfp_emisor"] = $("#txtEMISOR").val();
            //obj["dfp_debcre"] = 1; // DEBITO CREDITO??
            //obj["dfp_tarjeta"] = ;
            obj["dfp_banco"] = $("#cmbBANCO").val();
            obj["dfp_nro_cheque"] = $("#txtNROCHEQUE").val();
            obj["dfp_beneficiario"] = $("#txtBENEFICIARIO").val();

            var venceDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHAVENCE").val());

            obj["dfp_fecha_ven"] = venceDate;
            //obj["dfp_cuenta"] = 
            //obj["dfp_ref_comprobante"] = ;

        }
        else
            return null;
    }
    else {
        if ($.isNumeric($(row).data("codtipo").toString())) {
            obj["dfp_tipopago"] = parseInt($(row).data("codtipo"));
            obj["dfp_tipopagoid"] = $(row.cells[0]).text();
            obj["dfp_tipopagonombre"] = $(row.cells[1]).text();
            obj["dfp_monto"] = parseFloat($(row.cells[2]).text());

            obj["dfp_nro_documento"] = $(row).data("nrodocumento");
            obj["dfp_nro_cuenta"] = $(row).data("nrocuenta");
            obj["dfp_emisor"] = $(row).data("emisor");
            //obj["dfp_debcre"] = 1; // DEBITO CREDITO??
            //obj["dfp_tarjeta"] = ;
            if ($(row).data("banco")!=null)
                obj["dfp_banco"] = parseInt($(row).data("banco").toString());
            obj["dfp_nro_cheque"] = $(row).data("nrocheque");
            obj["dfp_beneficiario"] = $(row).data("beneficiario");

            if ($(row).data("fecha") != null) {
                var venceDate = $.datepicker.parseDate("dd/mm/yy", $(row).data("fecha").toString());
                obj["dfp_fecha_ven"] = venceDate;
            }
            obj = SetAuditoria(obj);
        }
        else
            return null;
        //obj["dfp_cuenta"] = 
        //obj["dfp_ref_comprobante"] = ;
    }

  
    return obj;
}





function GetComprobanteObj() {
    //var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHACOMP").val()); // $("#txtFECHA_P").datepicker("getDate");

    var now = new Date();
    var currentDate = $.datepicker.parseDate("dd/mm/yy", $("#txtFECHACOMP").val());

    var almacen = parseInt($("#txtCODALMACEN").val());
    var pventa = parseInt($("#txtCODPVENTA").val());

    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = $("#txtcodigocomp").val();
    obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
    obj["com_ctipocom"] = parseInt($("#txtCTIPOCOM").val());  //3 REC
    //obj["com_fecha"] = currentDate;
    obj["com_numero"] = $("#txtNUMERO").val();
    obj["com_fecha"] = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate(), now.getHours(), now.getMinutes(), now.getSeconds(), now.getMilliseconds());
    obj["com_doctran"] = $("#numerocomp").html();
    obj["com_concepto"] = $("#txtCONCEPTO").val();

    obj["com_periodo"] = currentDate.getFullYear();
    obj["com_almacen"] = almacen;
    obj["com_pventa"] = pventa;
    obj["com_codclipro"] = parseInt($("#txtCODPER").val());
    obj["com_agente"] = parseInt($("#txtCODVEN").val());
    obj["recibos"] = GetDetalle();
    obj = SetAuditoria(obj);
    obj["total"] = GetTotalObj();
    return obj;
    //var jsonText = JSON.stringify({ objeto: obj });
    //return jsonText;
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


    var detalle = GetDetalle();
    if (detalle.length == 0) {
        retorno = false;
        mensajehtml += "Es necesario ingresar al menos un detalle al comprobante<br>";
    }
    
    /*var htmltable = $("#tdinvoice")[0];
    if (htmltable.rows.length < 3) {
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


function Afectacion(obj) {

    
    var origen = $("#txtorigen").val();
    if (origen == "") {
        CallAfectacion(obj);
    }
    else if (origen == "FAC")  //FACTURA
    {
        var objref = {};
        objref["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        objref["com_codigo"] = $("#txtcodigocompref").val();
        CallAfectacionFromFAC(obj, objref);
    }
    else if (origen == "LGC")  //PLANILLA DE CLIENTES
    {
        var objref = {};
        objref["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
        objref["com_codigo"] = $("#txtcodigocompref").val();
        CallAfectacionFromLGC(obj, objref);
    }


}

function SetMontosPlanilla() {

    var origen = $("#txtorigen").val();
    if (origen == "LGC")  //SOLO ENVIA MONTO PLANILLA CUANDO SE CANCELA DESDE UNA LGC
    {
        var porcplanilla = ($.isNumeric($("#txtPORCENTAJEPLA").val())) ? parseFloat($("#txtPORCENTAJEPLA").val()) : 0;
        var valplanilla = ($.isNumeric($("#txtVALORPLA").val())) ? parseFloat($("#txtVALORPLA").val()) : 0;
        //if (planilla > 0) {
        for (var i = 0; i < afectacionobj.length; i++) {
            if (afectacionobj[i] != undefined) {
                var monto = afectacionobj[i]["dca_monto"];
                var porcmonto = monto * (porcplanilla / 100);
                var montopla = monto - porcmonto - valplanilla;
                //NUEVO CODIGO PARA EL CALCULO A PARTIR DEL SUBTOTAL                
                var subtotal = afectacionobj[i]["tot_subtotal"];
                if (subtotal != null) {
                    if (monto > subtotal) {
                        porcmonto = subtotal * (porcplanilla / 100);
                        montopla = subtotal - porcmonto - valplanilla;
                    }
                }
                afectacionobj[i]["dca_monto_pla"] = montopla;           
            }
        }
    }


    
  

}

function SetAfectacion(obj) {
    //recibocreated = true;
    //objdrecibo = obj;
    afectacionobj = obj;
    SetMontosPlanilla();
    SaveObj();
}






function SaveObj() {
    if (!saving) {
        if (ValidateForm()) {
            var compobj = GetComprobanteObj();
            if (afectacionobj == null)
                Afectacion(compobj);
            else {
                var obj = {};
                obj["recibo"] = compobj;
                obj["afectacion"] = afectacionobj;
                //            if ($(this)[0].id == "save")
                //                obj["comprobante"]["com_estado"] = parseInt($("#txtCREADO").val());
                //            else if ($(this)[0].id == "close")
                //                obj["comprobante"]["com_estado"] = parseInt($("#txtCERRADO").val()); 
                var jsonText = JSON.stringify({ objeto: obj });
                jConfirm('¿Está seguro que desea guardar la cancelacion ? \n Luego no se podra modificar', 'Guardar', function (r) {
                    if (r) {
                        $("#save").attr("disabled", true);
                        saving = true;
                        CallServer(formname + "/SaveObject", jsonText, 5);
                    }
                    else {

                        afectacionobj = null;

                    }
                });

            }

        }
    }
}

function CloseObj() {
    var obj = {};
    obj["recibo"] = GetComprobanteObj();
    //   obj["drecibo"] = objdrecibo;
    //  obj["rutaxfactura"] = GetRutaxFacturaObj();
    var jsonText = JSON.stringify({ objeto: obj });

    CallServer(formname + "/CloseObject", jsonText, 8);
}

function CloseObjResult(data) {
    if (data.d != "-1") {
        $("#txtcodigocomp").val(data.d);
        jQuery.alerts.dialogClass = 'alert-success';
        jAlert('Comprobante mayorizado exitosamente...', 'Éxito', function () {
            jQuery.alerts.dialogClass = null; // reset to default
            window.location = formname + "?codigocomp=" + data.d;
        });


    }
    else {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert('Se ha producido un error al mayorizar el comprobante...', 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
}
function ContObj() {
    window.location = "wfDiario.aspx?codigocomp=" + $("#txtcodigocomp").val() + "&tipodoc=" + $("#txttipodoc").val();
}
function PrintObj() {

    //window.location = "wfReciboPrint.aspx?codigocomp=" + $("#txtcodigocomp").val();
    var opciones = "toolbar=no, scrollbars=no, resizable=yes, top=50, left=100, width=800, height=600";
    window.open("wfDcontablePrint.aspx?codigocomp=" + $("#txtcodigocomp").val() + "&empresa=" + parseInt(empresasigned["emp_codigo"]), "", opciones);
}

function SaveObjResult(data) {
    $("#save").attr("disabled", false);
    saving = false;
    if (data != "") {

        if (data.d != "-1") {
            //$("#txtcodigocomp").val(data.d);

            jQuery.alerts.dialogClass = 'alert-success';
            jAlert('Cancelación guardada exitosamente...', 'Éxito', function () {
                jQuery.alerts.dialogClass = null; // reset to default
                //window.location = formname + "?codigocomp=" + data.d;
                $("#txtcodigocomp").val(data.d);

                if ($("#txtorigen").val() == "FAC") {


                    jConfirm('¿Desea despachar el comprobante este momento?', 'Pregunta', function (r) {
                        if (r) {

                            CallDespachar($("#txtcodigocompref").val());
                        }
                        else {
                            PrintObj();
                            location.reload();
                        }
                    });
                }
                else {
                    PrintObj();
                    location.reload();

                }
            });


        }
        else {
            afectacionobj = null;
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert('Se ha producido un error al guardar el comprobante...', 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
       
    }
}


function ViewObj() {
    var compobj = GetComprobanteObj();
    CallConsultaDeudas(compobj);
}
/****************************FUNCIONES MANEJO DE TECLAS*******************************/

function RowUp() {
    var r = $("#txtCODTIPO")[0].parentNode.parentNode;
    if ($(r).prev().length > 0)
        Edit($(r).prev()[0]);
}

function RowDown() {
    var r = $("#txtCODTIPO")[0].parentNode.parentNode;
    if ($(r).next().length > 0)
        Edit($(r).next()[0]);
    else {
        if ($("#txtCODTIPO").val() != "")
            AddEditRow();
    }
}


function IsOpen(id) {
    return $("#" + id).autocomplete('widget').is(':visible');
}

$(window).keydown(function (event) {
    var id = document.activeElement.id;
    var code = event.keyCode;
    var char = event.char;
    $("#mensaje").html(id + " " + code);

    if (id == "txtIDTIPO" || id == "txtVALOR") {

        if (id == "txtIDTIPO" && IsOpen(id))
            code = -1;

        //        if (code == 37) {// Flecha Izquierda
        //            CellLeft(this);
        //        }
        if (code == 38) { //Flecha Arriba
            RowUp();
        }
        //        if (code == 39) { //Flecha Derecha
        //            CellRight(this);
        //        }
        if (code == 40) { //ENTER O Flecha ABAJO            
            RowDown();
        }
        if (code == 13) {
            if (id != "txtIDTIPO")
                RowDown();
        }
    }
    if (!IsNumeric(id, code, char))
        event.preventDefault();

    if (code == 13) //ENTER
        event.preventDefault();

});

$(window).keyup(function (event) {
    var id = document.activeElement.id;
    var code = event.keyCode
    MustCalculate(id);
    if (document.activeElement.parentNode.parentNode.parentNode.parentNode.id == "tdafec_P") {
        $(document.activeElement.parentNode).data("valor",document.activeElement.value);
        CalculaAfectacion();
    }

});

function IsNumeric(id, code, char) {
    var num = $("#" + id).data("numeric");
    if (num == "si") {
        var controlKeys = [8, 9, 13, 35, 36, 37, 39];
        var isControlKey = controlKeys.join(",").match(new RegExp(code));
        if (!isControlKey) {
            var valor = $("#" + id).val();
            if (char == ".")
                char += "0";
            valor += char;
            if (!$.isNumeric(valor))
                return false;
        }


    }
    return true;
}


function MustCalculate(id) {
    switch (id) {
        case "txtVALOR":        
            CalculaTotales();
            break;

    }

}

function GetCallPersona(obj, id) {
    if (id == "btncallper") {
        $("#txtCODCLIPRO").val(obj.per_id);
        $("#txtCODPER").val(obj.per_codigo);
        $("#txtCIRUC").val(obj.per_ciruc);
        $("#txtNOMBRES").val(obj.per_apellidos + " " + obj.per_nombres);
        $("#txtRAZON").val(obj.per_razon);
        $("#txtRUC").val(obj.per_ciruc);
        $("#txtDIRECCION").val(obj.per_direccion);
        $("#txtTELEFONO").val(obj.per_telefono);

        /*$("#txtCODLIS").val(obj.per_listaprecio);
        $("#txtIDLIS").val(obj.per_listaid);
        $("#txtLISTA").val(obj.per_listanombre);

        $("#txtCODPOL").val(obj.per_politica);
        $("#txtIDPOL").val(obj.per_politicaid);
        $("#txtPOLITICA").val(obj.per_politicanombre);

        $("#txtPORCENTAJE").val(obj.per_politicadesc);
        $("#txtCODVEN").val(obj.per_agenteid);
        $("#txtVENDEDOR").val(obj.per_agentenombre);

        //AUTOMATICAMENTE IGUALA EL REMITENTE CON EL CLIENTE
        $("#txtIDREM").val(obj.per_id);
        $("#txtCODREM").val(obj.per_codigo);
        $("#txtNOMBRESREM").val(obj.per_apellidos + " " + obj.per_nombres);
        $("#txtDIRECCIONREM").val(obj.per_direccion);
        $("#txtTELEFONOREM").val(obj.per_telefono);*/
    }
    
}

function CleanPersona(id) {
    if (id == "btncleanper") {
        $("#txtCODCLIPRO").val("");
        $("#txtCODPER").val("");
        $("#txtCIRUC").val("");
        $("#txtNOMBRES").val("");
        $("#txtRAZON").val("");
        $("#txtRUC").val("");
        $("#txtDIRECCION").val("");
        $("#txtTELEFONO").val("");

        /*$("#txtCODLIS").val("");
        $("#txtIDLIS").val("");
        $("#txtLISTA").val("");

        $("#txtCODPOL").val("");
        $("#txtIDPOL").val("");
        $("#txtPOLITICA").val("");

        $("#txtPORCENTAJE").val("");
        $("#txtCODVEN").val("");
        $("#txtVENDEDOR").val("");*/
    }
}

//FUNCIONES de Selección y Deselección


function Mark(obj) {
    // var tr = $(obj).closest('table').attr('id'); ;
    // CleanSelect(tr);
    if ($(obj).css("background-color") == rgbcolor) {
        $(obj).css("background-color", "");
        $(obj).children("td").css("background-color", "");
        $('agregar').prop('disabled', false);
    }
    else {
        $(obj).css("background-color", color);
        $(obj).children("td").css("background-color", color);
    }
    //GetTotales();
}

function SelectAllRows(event) {
    var htmltable = $("#" + event.data.tabla)[0];
    var contador = 0;
    for (var r = 1; r < htmltable.rows.length; r++) {
        if ($(htmltable.rows[r]).css("background-color") != rgbcolor) {
            Mark(htmltable.rows[r]);
        }
    }
}

function CleanSelectedRows(event) {
    var htmltable = $("#" + event.data.tabla)[0];
    var contador = 0;
    for (var r = 1; r < htmltable.rows.length; r++) {
        if ($(htmltable.rows[r]).css("background-color") == rgbcolor) {
            Mark(htmltable.rows[r]);
        }
    }
}
