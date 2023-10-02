//Archivo:          DiarioD.js
//Descripción:      Contiene las funciones para el manejo del detalle de diario  
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Enero 2014
//2014. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados
//REQUIERE:
//      txtcodigocomp --> campo con el codigo del comproabante
//      txtCODALMACEN --> codigo del almacen proveniente de la cabecera


//Funciona que invoca al servidor mediante JSON
function CallServerDiario(strurl, strdata, retorno) {
    ClearValidate();
    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (retorno == 0)
                LoadBarraDiarioResult(data);
            if (retorno == 1)
                LoadTablaDiarioResult(data);
            if (retorno == 2)
                LoadPieDiarioResult(data);
            if (retorno == 3)
                LoadModuloResult(data);
            if (retorno == 4)
                LoadTransaccResult(data);
            if (retorno == 5)
                LoadModuloPrResult(data);
            if (retorno == 6)
                LoadCuentaPersonaResult(data);
            


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


function LoadDiario() {
    LoadBarraDiario();
    LoadTablaDiario();
    
}

function LoadBarraDiario() {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = $("#txtcodigocomp").val(); //ESTE
    obj["modify"] = $("#txtmodify").val();  //modificar
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerDiario("ws/Metodos.asmx/GetBarraDiario", jsonText, 0);
}
function LoadBarraDiarioResult(data) {
    if (data != "") {
        $('#barradiario').html(data.d);
    }
    $("#cancli").on("click", { tipo: cCliente }, Afectacion);
    $("#canpro").on("click", { tipo: cProveedor }, Afectacion);
}

///////////////////////////////////////////////////////////////////////////
function LoadTablaDiario() {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = $("#txtcodigocomp").val(); //ESTE
    obj["modify"] = $("#txtmodify").val();  //modificar
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerDiario("ws/Metodos.asmx/GetTablaDiario", jsonText, 1);
}
function LoadTablaDiarioResult(data) {
    if (data != "") {
        $('#comdetallediariocontent').html(data.d);
    }
    SetAutocompleteByIdTipo("txtIDCUE_D","DIARIO");
    SetAutocompleteByIdTipo("txtIDPER_D","DIARIO");
    CleanRowDiario();
    LoadPieDiario();
}

function CleanRowDiario() {
    $("#txtCODCUE_D").val("");
    $("#txtIDCUE_D").val("");
    $("#txtCUENTA_D").val("");
    $("#txtIDPER_D").val("");
    $("#txtCODPER_D").val("");
    $("#txtNOMBRES_D").val("");
    $("#txtCODMOD_D").val("");
    $("#txtMODULO_D").val("");
    $("#txtDEBE_D").val(CurrencyFormatted(0));
    $("#txtHABER_D").val(CurrencyFormatted(0));
    $("#txtDC_D").val("");
    $("#txtIDCUE_D").select();

    $("#txtCONCEPTO_D").val("");
    $("#cmbALMACEN_D").val($("#txtCODALMACEN").val());//PROVIENE DE LA CABECERA DEL COMPROBANTE
    $("#cmbCENTRO_D").val("1");
    $("#cmbTRANSACC_D").empty();

    $("#txtREF_D").val("");
    $("#txtOPAGO_D").val("");
    $("#txtNROPAGO_D").val("");
    $("#txtFECHAVENCE_D").val("");
    $("#txtSALDO_D").val("");
    return false;
}




/////////////////////////////////////////////////////////////////////////////
function LoadPieDiario() {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = $("#txtcodigocomp").val();
    obj["modify"] = $("#txtmodify").val();  //modificar
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerDiario("ws/Metodos.asmx/GetPieDiario", jsonText, 2);

}

function LoadPieDiarioResult(data) {
    if (data != "") {
        $('#compiediariocontent').html(data.d);
    }
    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    }); //
    CalculaTotalesDiario();
    var codigocomp = $("#txtcodigocomp").val();
    /*if ($("#txtESTADO").val() == $("#txtCERRADO").val()) {
  
        var inputs = $('input, textarea, select');  
        $(inputs).each(function () {      
            $(this).prop("disabled", true);

        });  
    }*/
}


/////////////////////////////////////////////////////////////////////////////
/////////////////////////AUTOCOMPLETE FUNCTIONS/////////////////////////////
function SetAutoCompleteObjDiario(idobj, item) {
    if (idobj == "txtIDPER_D" || idobj == "txtIDPER_PR") {
        return {
            label: item.per_id + "," + item.per_ciruc + "," + item.per_apellidos + " " + item.per_nombres + "-" + item.per_razon,
            value: item.per_id,
            info: item
        }
    }   
    if (idobj == "txtIDCUE_D" || idobj == "txtIDCUE_PR") {
        return {
            label: item.cue_id + "," + item.cue_nombre,
            value: item.cue_id,
            info: item
        }
    }
}


function GetAutoCompleteObjDiario(idobj, item) {
    if (idobj == "txtIDPER_PR") {
        $("#txtCODPER_PR").val(item.info.per_codigo);
        $("#txtNOMBRES_PR").val(item.info.per_apellidos + " " + item.info.per_nombres);
        LoadCuentaPersona();
        
    }
    if (idobj == "txtIDPER_D") {
        $("#txtCODPER_D").val(item.info.per_codigo);
        $("#txtNOMBRES_D").val(item.info.per_apellidos + " " + item.info.per_nombres);
    }    
    if (idobj == "txtIDCUE_D") {
        $("#txtIDCUE_D").val(item.info.cue_id);
        $("#txtCODCUE_D").val(item.info.cue_codigo);
        $("#txtCUENTA_D").val(item.info.cue_nombre);
        LoadModulo();
    }
    if (idobj == "txtIDCUE_PR") {
        $("#txtIDCUE_PR").val(item.info.cue_id);
        $("#txtCODCUE_PR").val(item.info.cue_codigo);
        $("#txtCUENTA_PR").val(item.info.cue_nombre);
        LoadModuloPr();
    }    
}


//////////////////////////////////////////////////////////////////////

function LoadCuentaPersona() {

    var obj = {};

    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codclipro"] = $("#txtCODPER_PR").val();
    obj["com_tclipro"] = $("#txtTIPO_PR").val();

    
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerDiario("ws/Metodos.asmx/GetPersonaCuenta", jsonText, 6);
}

function LoadCuentaPersonaResult(data)
{
    $("#txtIDCUE_PR").val("");
    $("#txtCODCUE_PR").val("");
    $("#txtCUENTA_PR").val("");
     

    if (data != "") {
        if (data.d != "-1") {
            var objmod = $.parseJSON(data.d);
            if (objmod.cue_codigo > 0)
            {
            $("#txtIDCUE_PR").val(objmod.cue_id);
            $("#txtCODCUE_PR").val(objmod.cue_codigo);
            $("#txtCUENTA_PR").val(objmod.cue_nombre);
            LoadModuloPr();
            }
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert("El cliente/ proveedor no tiene cuenta asignada....", 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}


function LoadModulo() {

    var obj = {};
    obj["empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cuenta"] = $("#txtCODCUE_D").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerDiario("ws/Metodos.asmx/GetModulo", jsonText, 3);
}
function LoadModuloResult(data) {
    $("#txtCODMOD_D").val(0);
    $("#txtMODULO_D").val("");
    if (data != "") {
        if (data.d != "-1") {
            var objmod = $.parseJSON(data.d);
            $("#txtCODMOD_D").val(objmod["mod_codigo"].toString());
            $("#txtMODULO_D").val(objmod["mod_id"].toString());
            LoadTransacc("", $("#txtCODMOD_D").val());
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert("El modulo no existe", 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}


function LoadModuloPr() {

    var obj = {};
    obj["empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["cuenta"] = $("#txtCODCUE_PR").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerDiario("ws/Metodos.asmx/GetModulo", jsonText, 5);
}
function LoadModuloPrResult(data) {
    $("#txtCODMOD_PR").val(0);
    $("#txtMODULO_PR").val("");
    if (data != "") {
        if (data.d != "-1") {
            var objmod = $.parseJSON(data.d);
            $("#txtCODMOD_PR").val(objmod["mod_codigo"].toString());
            $("#txtMODULO_PR").val(objmod["mod_id"].toString());            
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert("El modulo no existe", 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}

function LoadTransacc(tra, mod) {

    var obj = {};
    obj["empresa"] = parseInt(empresasigned["emp_codigo"]);
    //obj["modulo"] = $("#txtCODMOD_D").val();
    obj["modulo"] = mod;
    obj["transacc"] = tra;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerDiario("ws/Metodos.asmx/GetTransacc", jsonText, 4);
}
function LoadTransaccResult(data) {
    if (data != "") {
        if (data.d != "") {
            var dis = false;
            if ($('#cmbTRANSACC_D').is(':disabled'))
                dis = true;
            $("#cmbTRANSACC_D").replaceWith(data.d);
            if (dis)
                $("#cmbTRANSACC_D").prop('disabled', 'disabled');
        }
        else {
            jQuery.alerts.dialogClass = 'alert-danger';
            jAlert("El módulo no posee transacciones ", 'Error', function () {
                jQuery.alerts.dialogClass = null; // reset to default
            });
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
/*********************FUNCIONES PARA CALCULO DE TOTALES **********************/

function CalculaLineaDiario() {

    var deb = $("#txtDEBE_D").val();
    var cre = $("#txtHABER_D").val();
    if (!$.isNumeric(deb))
        deb = 0;
    if (!$.isNumeric(cre))
        cre = 0;
    var debcre = "";
    if (deb > 0)
        debcre = cDebito;
    if (cre > 0)
        debcre = cCredito;
    $("#txtDC_D").val(debcre);
}

function CalculaTotalesDiario() {
    var htmltable = $("#tdinvoicediario")[0];
    var debito = 0;
    var credito = 0;
    var diferencia = 0;

    for (var r = 0; r < htmltable.rows.length; r++) {
        var c = htmltable.rows[r].cells.length - 4; //celda debito
        var hijodeb = $(htmltable.rows[r].cells[c]).children("input");
        var hijocre = $(htmltable.rows[r].cells[c + 1]).children("input");

        var deb = 0;
        var cre = 0;

        if (hijodeb.length > 0) {
            deb = $(hijodeb).val();
            cre = $(hijocre).val();
        }
        else {
            deb = $(htmltable.rows[r].cells[c]).text();
            cre = $(htmltable.rows[r].cells[c + 1]).text();
        }

        credito += ($.isNumeric(cre)) ? parseFloat(cre) : 0;
        debito += ($.isNumeric(deb)) ? parseFloat(deb) : 0;


    }

    if (credito > debito)
        diferencia = credito - debito;
    else if (debito > credito)
        diferencia = debito - credito;


    $("#txtTOTDEBITO_D").val(CurrencyFormatted(debito));
    $("#txtTOTCREDITO_D").val(CurrencyFormatted(credito));
    $("#txtDIFERENCIA_D").val(CurrencyFormatted(diferencia));

    if (diferencia > 0) {
        $("#txtDIFERENCIA_D").addClass("erroramounts");
    }
    else {
        $("#txtDIFERENCIA_D").removeClass("erroramounts");
    }
}



///////////////////////////////////////////////////////////////////////////////////////
/*********************FUNCIONES PARA AGREGAR y EDITAR UN DETALLE**********************/

function AddEditRowDiario() {
    var strdata = "data-codcue='" + $("#txtCODCUE_D").val() + "' " +
    "data-codper='" + $("#txtCODPER_D").val() + "' " +
    "data-codmod='" + $("#txtCODMOD_D").val() + "' " +
    "data-concepto='" + $("#txtCONCEPTO_D").val() + "' " +
    "data-codalmacen='" + $("#cmbALMACEN_D").val() + "' " +
    //"data-idalmacen='" + $("#txtIDALM_D").val() + "' " +
    "data-nombrealmacen='" + $("#cmbALMACEN_D option:selected").text() + "' " +
    "data-codcentro='" + $("#cmbCENTRO_D").val() + "' " +
    //"data-idcentro='" + $("#txtIDCEN_D").val() + "' " +
    "data-nombrecentro='" + $("#cmbCENTRO_D option:selected").text() + "' " +
    "data-codtransacc_can='" + $("#cmbTRANSACC_D").val() + "' " +
    "data-nombretransacc='" + $("#cmbTRANSACC_D option:selected").text() + "' " +
    "data-doctran='" + $("#txtREF_D").val() + "' " +
    "data-ddocomproba='" + $("#txtOPAGO_D").val() + "' " +    
    "data-nropago='" + $("#txtNROPAGO_D").val() + "' " +
    "data-fechavence='" + $("#txtFECHAVENCE_D").val() + "' ";    
    
    var newrow = $("<tr " + strdata + " onclick='EditDiario(this);'></tr>");
    newrow.append("<td class='' >" + $("#txtIDCUE_D").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtCUENTA_D").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtIDPER_D").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtNOMBRES_D").val() + "</td>");
    newrow.append("<td class='' >" + $("#txtMODULO_D").val() + "</td>");
    var deb = $("#txtDEBE_D").val();
    if (deb == "")
        deb = "0";
    deb = CurrencyFormatted(deb);
    var cre = $("#txtHABER_D").val();
    if (cre == "")
        cre = "0";
    cre = CurrencyFormatted(cre);
    newrow.append("<td class='right' >" + deb + "</td>");
    newrow.append("<td class='right' >" + cre + "</td>");
    newrow.append("<td class='center' >" + $("#txtDC_D").val() + "</td>");
    newrow.append("<td class='center' ><div class='removablerow' onclick='RemoveRowDiario(this)'><span class=\"icon-trash\" ></span></div></td>");


    //var newrow = $("<tr><td>Hola</td><td>chao</td><td>jaja</td><td>si</td><td>NO</td><td>BYE</td><td>7</td><td>8</td></tr>");
    var editrow = $("#tdinvoicediario").find("#editrow");
    newrow.insertBefore(editrow);

    CleanRowDiario();
    
}




function MoveToDiario(control, destino) {
    var obj = $(control).detach();
    $(destino).append(obj);
}


function SelectDiario(row) {

    var htmltable = $("#tdinvoicediario")[0];
    var contador = 0;
    for (var r = 1; r < htmltable.rows.length; r++) {
        if ($(htmltable.rows[r]).css("background-color") == rgbcolor) {
            Mark(htmltable.rows[r]);
        }
    }    
    Mark(row);
    $("#txtCONCEPTO_D").val($(row).data("concepto").toString());

    $("#cmbALMACEN_D").val($(row).data("codalmacen"));
    $("#cmbCENTRO_D").val($(row).data("codcentro"));


    LoadTransacc($(row).data("codtransacc"), $(row).data("codmod"));
   

    $("#txtREF_D").val($(row).data("doctran").toString());
    $("#txtOPAGO_D").val($(row).data("ddocomproba").toString());

    $("#txtNROPAGO_D").val($(row).data("nropago").toString());
    $("#txtFECHAVENCE_D").val($(row).data("fechavence").toString());

}

function EditDiario(row) {

    var r = $("#txtCODCUE_D")[0].parentNode.parentNode;
    var codcue = $("#txtCODCUE_D").val();
    var idcue = $("#txtIDCUE_D").val();
    var cuenta = $("#txtCUENTA_D").val();
    var codper = $("#txtCODPER_D").val();
    var idper = $("#txtIDPER_D").val();
    var nombres = $("#txtNOMBRES_D").val();

    var codmod = $("#txtCODMOD_D").val();
    var modulo = $("#txtMODULO_D").val();
    var debe = $("#txtDEBE_D").val();
    var haber = $("#txtHABER_D").val();

    if (debe == "")
        debe = "0";
    debe = CurrencyFormatted(debe);
    if (haber == "")
        haber = "0";
    haber = CurrencyFormatted(haber);


    var concepto = $("#txtCONCEPTO_D").val();
    //var idalm = $("#txtIDALM_D").val();
    var nombrealm = $("#cmbALMACEN_D option:selected").text();
    var codalm = $("#cmbALMACEN_D").val();
    //var idcen = $("#txtIDCEN_D").val();
    var nombrecen = $("#cmbCENTRO_D option:selected").text();
    var codcen = $("#cmbCENTRO_D").val();

    var codtra = $("#cmbTRANSACC_D").val();
    var nombretra = $("#cmbTRANSACC_D option:selected").text();
    //var idtra = $("#txtIDTRA_D").val();

    var ddocomproba = $("#txtOPAGO_D").val();
    var doctran = $("#txtREF_D").val();    
    var nropago = $("#txtNROPAGO_D").val();
    var fechavence = $("#txtFECHAVENCE_D").val();

    var dc = $("#txtDC_D").val();

    CleanRowDiario();

    //$("#txtCODCUE_D").val($(row).data("codcue").toString());
    $("#txtCODCUE_D").val($(row).data("codcue"));
    $("#txtIDCUE_D").val($(row.cells[0]).text()); $(row.cells[0]).text("");
    $("#txtCUENTA_D").val($(row.cells[1]).text()); $(row.cells[1]).text("");

    //$("#txtCODPER_D").val($(row).data("codper").toString());
    $("#txtCODPER_D").val($(row).data("codper"));
    $("#txtIDPER_D").val($(row.cells[2]).text()); $(row.cells[2]).text("");
    $("#txtNOMBRES_D").val($(row.cells[3]).text()); $(row.cells[3]).text("");

    //$("#txtCODMOD_D").val($(row).data("codmod").toString());
    $("#txtCODMOD_D").val($(row).data("codmod"));
    $("#txtMODULO_D").val($(row.cells[4]).text()); $(row.cells[4]).text("");

    $("#txtDEBE_D").val($(row.cells[5]).text()); $(row.cells[5]).text("");
    $("#txtHABER_D").val($(row.cells[6]).text()); $(row.cells[6]).text("");
    $("#txtDC_D").val($(row.cells[7]).text()); $(row.cells[7]).text("");


    //$("#txtCONCEPTO_D").val($(row).data("concepto").toString());
    $("#txtCONCEPTO_D").val($(row).data("concepto"));

    $("#cmbALMACEN_D").val($(row).data("codalmacen"));
    $("#cmbCENTRO_D").val($(row).data("codcentro"));


    LoadTransacc($(row).data("codtransacc_can"), $(row).data("codmod"));


    // $("#txtREF_D").val($(row).data("doctran").toString());
    $("#txtREF_D").val($(row).data("doctran"));
    //$("#txtOPAGO_D").val($(row).data("ddocomproba").toString());
    $("#txtOPAGO_D").val($(row).data("ddocomproba"));

    //$("#txtNROPAGO_D").val($(row).data("nropago").toString());
    $("#txtNROPAGO_D").val($(row).data("nropago"));
    //$("#txtFECHAVENCE_D").val($(row).data("fechavence").toString());
    $("#txtFECHAVENCE_D").val($(row).data("fechavence"));



    $("#txtIDCUE_D").focus();

    MoveToDiario($("#txtIDCUE_D"), $(row.cells[0]));
    MoveToDiario($("#txtCODCUE_D"), $(row.cells[0]));
    MoveToDiario($("#txtCUENTA_D"), $(row.cells[1]));

    MoveToDiario($("#txtIDPER_D"), $(row.cells[2]));
    MoveToDiario($("#txtCODPER_D"), $(row.cells[2]));
    MoveToDiario($("#txtNOMBRES_D"), $(row.cells[3]));

    MoveToDiario($("#txtCODMOD_D"), $(row.cells[4]));
    MoveToDiario($("#txtMODULO_D"), $(row.cells[4]));

    MoveToDiario($("#txtDEBE_D"), $(row.cells[5]));
    MoveToDiario($("#txtHABER_D"), $(row.cells[6]));
    MoveToDiario($("#txtDC_D"), $(row.cells[7]));




    $(r).data("codcue", codcue);
    $(r).data("codper", codper);
    $(r).data("codmod", codmod);



    $(r).data("concepto", concepto);
    //$(r).data("idalmacen", idalm);
    $(r).data("nombrealmacen", nombrealm);
    $(r).data("codalmacen", codalm);
    //$(r).data("idcentro", idcen);
    $(r).data("nombrecentro", nombrecen);
    $(r).data("codcentro", codcen);
    //$(r).data("idtransacc", idtra);
    $(r).data("nombretransacc", nombretra);
    $(r).data("codtransacc_can", codtra);
    $(r).data("ddocomproba", ddocomproba);
    $(r).data("doctran", doctran);
    ///$(r).data("ddotransacc", ddotransacc);
    $(r).data("nropago", nropago);
    $(r).data("fechavence", fechavence);


    $(r.cells[0]).text(idcue);
    $(r.cells[1]).text(cuenta);
    $(r.cells[2]).text(idper);
    $(r.cells[3]).text(nombres);
    $(r.cells[4]).text(modulo);
    $(r.cells[5]).text(debe);
    $(r.cells[6]).text(haber);
    $(r.cells[7]).text(dc);

    $(r).attr('onclick', 'EditDiario(this)');
    $(row).removeAttr('onclick');

    $("#txtIDCUE_D").select();

}


/***************************FUNCIONES DE PRECARGADO**************************************/

function Afectacion(event) {
    var obj = {};
    obj["tipo"] = event.data.tipo;
    CallPrevAfectacion(obj);
}

function SetPrevAfectacion(obj) {
    obj["com_tipodoc"] = parseInt($("#txttipodoc").val());
    CallAfectacion(obj)
}


function SetAfectacion(obj) {
    //AQUI SE CARGA AUTOMATICAMENTE EL DETALLE CONTABLE
    for (var i = 1; i < obj.length; i++) {

        var monto = parseFloat(obj[i]["dca_monto"].toString());
        if (monto > 0) {

            var strdata = "data-codcue='" + $("#txtCODCUE_PR").val() + "' " +
            "data-codper='" + $("#txtCODPER_PR").val() + "' " +
            "data-codmod='" + $("#txtCODMOD_PR").val() + "' " +
            "data-concepto='' " +
            "data-codalmacen='" + $("#txtCODALMACEN").val() + "' " +
                    //"data-idalmacen='" + $("#txtIDALM_D").val() + "' " +
            "data-nombrealmacen='' " +
            "data-codcentro='1' " +
                    //"data-idcentro='" + $("#txtIDCEN_D").val() + "' " +
            "data-nombrecentro='' " +
            "data-codtransacc='" + obj[i]["dca_transacc"] + "' " +
            "data-codtransacc_can='" + obj[i]["dca_transacc_can"] + "' " +
            "data-nombretransacc='' " +
            "data-doctran='" + obj[i]["dca_doctran"] + "' " +
            "data-ddocomproba='" + obj[i]["dca_comprobante"] + "' " +
            "data-nropago='" + obj[i]["dca_pago"] + "' " +
            "data-fechavence='' ";

            var newrow = $("<tr " + strdata + " onclick='EditDiario(this);'></tr>");
            newrow.append("<td class='' > " + $("#txtIDCUE_PR").val() + " </td>");
            newrow.append("<td class='' > " + $("#txtCUENTA_PR").val() + "</td>");
            newrow.append("<td class='' >" + $("#txtIDPER_PR").val() + "</td>");
            newrow.append("<td class='' >" + $("#txtNOMBRES_PR").val() + "</td>");
            newrow.append("<td class='' >" + $("#txtMODULO_PR").val() + "</td>");

            var debcre = obj[i]["dca_debcre"];
            var deb = "0";
            var cre = "0";
            if (debcre == cDebito)
            //deb = monto;                
                cre = monto;
            if (debcre == cCredito)
                deb = monto;
            //cre = monto;
            
            deb = CurrencyFormatted(deb);
            cre = CurrencyFormatted(cre);

            newrow.append("<td class='right' >" + deb + "</td>");
            newrow.append("<td class='right' >" + cre + "</td>");
            newrow.append("<td class='center' >" + ((debcre == cDebito) ? cCredito : cDebito) + "</td>");
            newrow.append("<td class='center' ><div class='removablerow' onclick='RemoveRowDiario(this)'><span class=\"icon-trash\" ></span></div></td>");


            //var newrow = $("<tr><td>Hola</td><td>chao</td><td>jaja</td><td>si</td><td>NO</td><td>BYE</td><td>7</td><td>8</td></tr>");
            var editrow = $("#tdinvoicediario").find("#editrow");
            newrow.insertBefore(editrow);
        }
        
    }

    CalculaTotalesDiario(); 
    //recibocreated = true;
    //objdrecibo = obj;
    //afectacionobj = obj;
    //SaveObj();    
}

///////////////////////////////////////////////////////////////////////////
/***********************SAVE FUNCTIONS ***********************************/


function GetDetalleDiario() {
    var detalle = new Array();
    var htmltable = $("#tdinvoicediario")[0];
    for (var r = 1; r < htmltable.rows.length; r++) {
        var obj = GetDcontableObj(htmltable.rows[r]);
        if (obj != null)
            detalle[r] = obj;
    }
    return detalle;
}

function GetDcontableObj(row) {
    var obj = {};
    obj["dco_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["dco_comprobante"] = $("#txtcodigocomp").val();

    var hijocodpro = $(row.cells[0]).children("input");
    if (hijocodpro.length > 0) {
        if ($.isNumeric($("#txtCODCUE_D").val())) {
            obj["dco_cuenta"] = parseInt($("#txtCODCUE_D").val());
            obj["dco_centro"] = $("#cmbCENTRO_D").val();
            obj["dco_transacc"] = $("#cmbTRANSACC_D").val();
            obj["dco_debcre"] = $("#txtDC_D").val();

            var deb = parseFloat($("#txtDEBE_D").val());
            var cre = parseFloat($("#txtHABER_D").val());
            if (deb > 0) {
                obj["dco_valor_nac"] = deb;
                obj["dco_valor_ext"] = deb;
            }
            if (cre > 0) {
                obj["dco_valor_nac"] = cre;
                obj["dco_valor_ext"] = cre;
            }

            //obj["dco_tipo_cambio"] = ;
            obj["dco_concepto"] = $("#txtCONCEPTO_D").val();
            obj["dco_almacen"] = $("#cmbALMACEN_D").val();
            obj["dco_cliente"] = $("#txtCODPER_D").val();
            //obj["dco_agente"] = ;
            obj["dco_doctran"] = $("#txtREF_D").val();
            obj["dco_nropago"] = $("#txtNROPAGO_D").val();
            obj["dco_fecha_vence"] = $("#txtFECHAVENCE_D").val();
            obj["dco_ddo_comproba"] = $("#txtOPAGO_D").val();
            obj["dco_ddo_transacc"] = $(row).data("codtransacc");
            //obj["dco_ddo_transacc"] = $("#cmbTRANSACC_D").val();
            //obj["dco_producto"] = ;
            //obj["dco_bodega"] = ;

        }
        else
            return null;
    }
    else {
        if ($.isNumeric($(row).data("codcue").toString())) {
            obj["dco_cuenta"] = parseInt($(row).data("codcue").toString());
            //obj["dco_centro"] = parseInt($(row).data("codcentro").toString());
            obj["dco_centro"] = parseInt($(row).data("codcentro"));
            obj["dco_transacc"] = $(row).data("codtransacc_can");
            obj["dco_debcre"] = $(row.cells[7]).text();


            var deb = parseFloat($(row.cells[5]).text());
            var cre = parseFloat($(row.cells[6]).text());
            if (deb > 0) {
                obj["dco_valor_nac"] = deb;
                obj["dco_valor_ext"] = deb;
            }
            if (cre > 0) {
                obj["dco_valor_nac"] = cre;
                obj["dco_valor_ext"] = cre;
            }

            obj["dco_concepto"] = $(row).data("concepto");
            obj["dco_almacen"] = $(row).data("codalmacen");
            obj["dco_cliente"] = $(row).data("codper");
            //obj["dco_agente"] = ;
            obj["dco_doctran"] = $(row).data("doctran");
            obj["dco_nropago"] = $(row).data("nropago");
            obj["dco_fecha_vence"] = $(row).data("fechavence");
            obj["dco_ddo_comproba"] = $(row).data("ddocomproba");
            obj["dco_ddo_transacc"] = $(row).data("codtransacc");



            /*//obj["dco_tipo_cambio"] = ;
            obj["dco_concepto"] = ;
            obj["dco_almacen"] = ;
            if ($(row).data("codper")) {
            obj["dco_cliente"] = $(row).data("codper").toString();
            }
            //obj["dco_agente"] = ;
            obj["dco_doctran"] = ;
            obj["dco_nropago"] = ;
            obj["dco_fecha_vence"] = ;
            obj["dco_ddo_comproba"] = ;
            obj["dco_ddo_transacc"] = ;
            //obj["dco_producto"] = ;
            //obj["dco_bodega"] = ;*/
        }
        else
            return null;

    }
    obj = SetAuditoria(obj);
    return obj;
}


///////////////////////////////////////////////////////////////////////////////////////
//////////////////FUNCIONES DE VALIDACION DE DETALLE DE DIARIO/////////////////////////



function ValidateDiario() {
    var retorno = true;
    var htmltable = $("#tdinvoicediario")[0];
    var mensajehtml = "";
    if (htmltable.rows.length < 3) {
        retorno = false;
        mensajehtml += "Es necesario ingresar al menos un detalle al comprobante<br>";
    }
    if (!retorno) {
        jQuery.alerts.dialogClass = 'alert-danger';
        jAlert(mensajehtml, 'Error', function () {
            jQuery.alerts.dialogClass = null; // reset to default
        });
    }
    return retorno;
}





////////////////////////////////////////////////////////////////////////////////////////
/****************************FUNCIONES MANEJO DE TECLAS*******************************/

function RowUpDiario() {
    var r = $("#txtCODCUE_D")[0].parentNode.parentNode;
    if ($(r).prev().length > 0)
        EditDiario($(r).prev()[0]);
}

function RowDownDiario() {
    var r = $("#txtCODCUE_D")[0].parentNode.parentNode;
    if ($(r).next().length > 0)
        EditDiario($(r).next()[0]);
    else {
        if ($("#txtCODCUE_D").val() != "")
            AddEditRowDiario();
    }
}


function IsOpenDiario(id) {
    return $("#" + id).autocomplete('widget').is(':visible');
}



$(window).keydown(function (event) {
    var id = document.activeElement.id;
    var code = event.keyCode;
    //var char = event.char;
    var char = String.fromCharCode(event.which);
    //$("#mensaje").html(id + " " + code + " " + char);

    if (id == "txtIDCUE_D" || id == "txtIDPER_D" || id == "txtDEBE_D" || id == "txtHABER_D") {

        if ((id == "txtIDCUE_D" || id == "txtIDPER_D") && IsOpenDiario(id))
            code = -1;
        if (code == 38) { //Flecha Arriba
            RowUpDiario();
        }        
        if (code == 40) { //ENTER O Flecha ABAJO            
            RowDownDiario();
        }
        if (code == 13) {
            if (id != "txtIDCUE_D" || id != "txtIDPER_D")
                RowDownDiario();
        }
    }


    if (!IsNumericDiario(id, event))
        event.preventDefault();

    if (code == 13) //ENTER
        event.preventDefault();
    event.stopPropagation();

});

$(window).keyup(function (event) {
    var id = document.activeElement.id;
    var code = event.keyCode
    MustCalculateDiario(id);
  

});


function IsNumericDiario(id, event) {
    var num = $("#" + id).data("numeric");
    if (num == "si") {
        var keyCode = ('which' in event) ? event.which : event.keyCode;
        var controlKeys = [8, 9, 13, 35, 36, 37, 39, 110, 190];
        var isControlKey = controlKeys.join(",").match(new RegExp(keyCode));
        if (!isControlKey) {
            isNumeric = (keyCode >= 48 /* KeyboardEvent.DOM_VK_0 */ && keyCode <= 57 /* KeyboardEvent.DOM_VK_9 */) ||
                        (keyCode >= 96 /* KeyboardEvent.DOM_VK_NUMPAD0 */ && keyCode <= 105 /* KeyboardEvent.DOM_VK_NUMPAD9 */);
            //modifiers = (event.altKey || event.ctrlKey || event.shiftKey);
            return isNumeric;
        }
       
    }
    return true;
}


function MustCalculateDiario(id) {
    switch (id) {
        case "txtDEBE_D":
        case "txtHABER_D":
            CalculaLineaDiario();
            CalculaTotalesDiario();
            break;

    }

}



function RemoveRowDiario(btn) {
    var row = $(btn).parents('tr');
    var hascontrols = ($(row).find("input").length > 0) ? true : false;
    if ($(row)[0].id != "editrow" && !hascontrols) {
        jConfirm('¿Está seguro que desea eliminar el registro?', 'Eliminar', function (r) {
            if (r) {
                $(btn).parents('tr').fadeOut(function () {
                    $(this).remove();
                    CalculaTotalesDiario();
                });
            }
        });
    }
    else
        CleanRowDiario();
    CalculaTotalesDiario();
    StopPropagation();
}

//FUNCIONES DE MARCADO Y DESMARCADO
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
