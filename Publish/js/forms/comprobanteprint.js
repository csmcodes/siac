$(document).ready(function () {


    GetComprobante()

});


function GetComprobante() {

    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = GetQueryStringParams("codigo")
    var jsonText = JSON.stringify({ objeto: obj });
       
    CallServerMethods("../"+webservice + "GetComprobante", jsonText, 0);
}

function ServerResult(data, retorno) {
    if (retorno == 0) {
        if (data.d != "") {
            var obj = $.parseJSON(data.d);
            SetComprobante(obj);
            Print();
        }
    }
}


function SetComprobante(obj) {

    $("#numeroup").html(obj["com_doctran"]);
    $("#numerodown").html(obj["com_doctran"]);

    $("#fechacrea").html(new Date().toLocaleString());


    //$("#fecha").html(GetDateStringValue(obj["com_fecha"]));
    $("#fecha").html(obj["com_fechastr"]);
    $("#cliente").html(obj["ccomdoc"]["cdoc_nombre"]);
    $("#ruc").html(obj["ccomdoc"]["cdoc_ced_ruc"]);

    $("#remitente").html(obj["ccomenv"]["cenv_apellidos_rem"] + " " + obj["ccomenv"]["cenv_nombres_rem"]);
    $("#rucrem").html(obj["ccomenv"]["cenv_ciruc_rem"]);

    $("#ciudaddes").html(obj["ccomenv"]["cenv_rutadestino"]);
    $("#destinatario").html(obj["ccomenv"]["cenv_apellidos_des"] + " " + obj["ccomenv"]["cenv_nombres_des"]);
    $("#rucdes").html(obj["ccomenv"]["cenv_ciruc_des"]);
    $("#direcciondes").html(obj["ccomenv"]["cenv_direccion_des"]);
    $("#telefonodes").html(obj["ccomenv"]["cenv_telefono_des"]);
    $("#guia").html(obj["ccomenv"]["cenv_guia1"] + "-" + obj["ccomenv"]["cenv_guia2"] + "-" + obj["ccomenv"]["cenv_guia3"]);

    var totalcantidad = 0;
    for (var i = 0; i < obj["ccomdoc"]["detalle"].length; i++) {
        var detalle = obj["ccomdoc"]["detalle"][i];
        totalcantidad += detalle["ddoc_cantidad"];
        $(".cantidad").append(detalle["ddoc_cantidad"]+"<br>");
        //var observaciones = detalle["ddoc_productonombre"] + " " + detalle["ddoc_observaciones"] + "<br>";
        var observaciones = detalle["ddoc_observaciones"] + "<br>";
        $(".descripcion").append(observaciones);
        $(".valoru").append(CurrencyFormatted(detalle["ddoc_precio"]) + "<br>");
        $(".valortotal").append(CurrencyFormatted(detalle["ddoc_total"]) + "<br>");
        
    }



    var subtotal = parseFloat(obj["total"]["tot_subtot_0"].toString())+ parseFloat(obj["total"]["tot_subtotal"].toString());
    $("#subtotal").html(CurrencyFormatted(subtotal));
    $("#subtotal0").html(CurrencyFormatted(obj["total"]["tot_subtot_0"]));
    $("#subtotal12").html(CurrencyFormatted(obj["total"]["tot_subtotal"]));
    $("#iva").html(CurrencyFormatted(obj["total"]["tot_timpuesto"]));
    $("#lbliva").html("IVA " + CurrencyFormatted(obj["total"]["tot_porc_impuesto"]) + "%:");
    var vseguro = 0;
    if (obj["total"]["tot_vseguro"] != null && obj["total"]["tot_vseguro"] != undefined)
        vseguro = parseFloat(obj["total"]["tot_vseguro"]);
    var porcseguro = 0;
    if (obj["total"]["tot_porc_seguro"] != null && obj["total"]["tot_porc_seguro"] != undefined)
        porcseguro = parseFloat(obj["total"]["tot_porc_seguro"]);

    var valorseguro = 0;
    if(vseguro>0 && porcseguro>0)
        valorseguro = vseguro * (porcseguro/100);
        
    $("#valorseguro").html(CurrencyFormatted(valorseguro));
    //$("#valorseguro").html(CurrencyFormatted(obj["total"]["tot_tseguro"]));
    $("#transporte").html(CurrencyFormatted(obj["total"]["tot_transporte"]));
    $("#total").html(CurrencyFormatted(obj["total"]["tot_total"]));


    $("#totalcantidad").html("TOTAL BUL. " + totalcantidad.toString());
    $("#politica").html(obj["ccomdoc"]["cdoc_politicanombre"]);
    $("#valordeclarado").html(CurrencyFormatted(obj["total"]["tot_vseguro"]));
    $("#propietario").html(obj["ccomenv"]["cenv_nombres_soc"]);
    //$("#hojaruta").html(obj["com_doctran"]);
   // $("#conductor").html(obj["ccomenv"]["cenv_nombres_cho"]);
   // $("#nombreusuario").html(obj["crea_usrnombres"]);
    $("#usuario").html(obj["crea_usr"]);
 
    if (obj["com_doctran"].toString().indexOf("GUI") >= 0)
        $(".imprime").show();
    else
        $(".imprime").hide();
    
}


function Print() {
    //jsPrintSetup.setPrinter('\\gtec-pc\EPSON L200 Series');
    jsPrintSetup.setPrinter('EPSON L200 Series');
    // set portrait orientation
    //jsPrintSetup.setOption('orientation', jsPrintSetup.kPortraitOrientation);

    // set unwriteable margins in millimeters
    jsPrintSetup.setOption('unwriteableMarginTop', 0);
    jsPrintSetup.setOption('unwriteableMarginLeft', 0);
    jsPrintSetup.setOption('unwriteableMarginBottom', 0);
    jsPrintSetup.setOption('unwriteableMarginRight', 0);



    // set  margins in millimeters
    jsPrintSetup.setOption('marginTop', 0);
    jsPrintSetup.setOption('marginBottom', 0);
    jsPrintSetup.setOption('marginLeft', 0);
    jsPrintSetup.setOption('marginRight', 0);
    // set page header
    jsPrintSetup.setOption('headerStrLeft', '');
    jsPrintSetup.setOption('headerStrCenter', '');
    jsPrintSetup.setOption('headerStrRight', '');
    // set empty page footer
    jsPrintSetup.setOption('footerStrLeft', '');
    jsPrintSetup.setOption('footerStrCenter', '');
    jsPrintSetup.setOption('footerStrRight', '');

    //jsPrintSetup.setOption('shrinkToFit', '1');
    //jsPrintSetup.definePaperSize(78, 78, "A5 Rotated 210 x 148 mm", "Custom_Paper", "Custom PAPER", 210, 148,jsPrintSetup.kPaperSizeMillimeters);
    //jsPrintSetup.setPaperSizeData(78);
   

    jsPrintSetup.setSilentPrint(false);

    //jsPrintSetup.setPaperSizeUnit(8);
    // clears user preferences always silent print value
    // to enable using 'printSilent' option
    //jsPrintSetup.clearSilentPrint();
    // Suppress print dialog (for this context only)
    //jsPrintSetup.setOption('printSilent', 1);
    // Do Print 
    // When print is submitted it is executed asynchronous and
    // script flow continues after print independently of completetion of print process! 
    jsPrintSetup.print();
    //window.close();
    // next commands
}