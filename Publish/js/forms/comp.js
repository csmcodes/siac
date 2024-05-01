$(document).ready(function () {
    var espacio = GetQueryStringParams("space");

    if (espacio != "") {
        document.querySelector('style').textContent += "@media print { body { letter-spacing:" + espacio + "pt; }}"
    }
    GetComprobante()
});


function GetComprobante() {

    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = GetQueryStringParams("codigo")
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods("../" + webservice + "GetComprobante", jsonText, 0);
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
    var espacio = GetQueryStringParams("space");
    var maxlength = 50;
    if (espacio!="")
        maxlength = 40;

    $(".numero").html(obj["com_doctran"]);
    $(".numerogui").find(".textobig").html(obj["com_doctran"]);

    $(".almacen").find(".texto").html(obj["com_almacennombre"].toUpperCase() + " " + usuariosigned["usr_id"]);
    $(".clave").find(".texto").html(obj["com_claveelec"]);
    
    $(".fechacrea").find(".texto").html(new Date().toLocaleString());
    //$(".usuario").find(".texto").html(usuariosigned["usr_id"]);
 
 
    $(".origen").find(".texto").html(obj["ccomenv"]["cenv_rutaorigen"]);
    $(".fecha").find(".texto").html(obj["com_fechastr"]);
    $(".destino").find(".texto").html(obj["ccomenv"]["cenv_rutadestino"]);


    var nombresrem = obj["ccomenv"]["cenv_apellidos_rem"] + " " + obj["ccomenv"]["cenv_nombres_rem"];
    var direccionrem = obj["ccomenv"]["cenv_direccion_rem"]

    if (nombresrem.length > maxlength)
        nombresrem = nombresrem.substring(0, maxlength);
    if (direccionrem.length > maxlength)
        direccionrem = direccionrem.substring(0, maxlength);

    $(".nombresrem").find(".texto").html(nombresrem);
    $(".rucrem").find(".texto").html(obj["ccomenv"]["cenv_ciruc_rem"]);
    $(".direccionrem").find(".texto").html(direccionrem);
    $(".telefonorem").find(".texto").html(obj["ccomenv"]["cenv_telefono_rem"]);

    var nombresdes = obj["ccomenv"]["cenv_apellidos_des"] + " " + obj["ccomenv"]["cenv_nombres_des"];
    var direcciondes = obj["ccomenv"]["cenv_direccion_des"];

    if (nombresdes.length > maxlength)
        nombresdes = nombresdes.substring(0, maxlength);
    if (direcciondes.length > maxlength)
        direcciondes = direcciondes.substring(0, maxlength);

    $(".nombresdes").find(".texto").html(nombresdes);
    $(".rucdes").find(".texto").html(obj["ccomenv"]["cenv_ciruc_des"]);
    $(".direcciondes").find(".texto").html(direcciondes);
    $(".telefonodes").find(".texto").html(obj["ccomenv"]["cenv_telefono_des"]);

    var totalcantidad = 0;
    for (var i = 0; i < obj["ccomdoc"]["detalle"].length; i++) {
        var detalle = obj["ccomdoc"]["detalle"][i];
        totalcantidad += detalle["ddoc_cantidad"];
        $(".cantidad").find(".boxbody").append(detalle["ddoc_cantidad"] + "<br>");
        //var observaciones = detalle["ddoc_productonombre"] + " " + detalle["ddoc_observaciones"] + "<br>";
        var observaciones = detalle["ddoc_observaciones"] + "<br>";
        $(".contenido").find(".boxbody").append(observaciones);
        $(".valoru").find(".boxbody").append(CurrencyFormatted(detalle["ddoc_precio"]) + "<br>");
        $(".flete").find(".boxbody").append(CurrencyFormatted(detalle["ddoc_total"]) + "<br>");

    }
    $(".bultos").find(".texto").html(totalcantidad);    
    $(".guia").find(".texto").html(obj["ccomenv"]["cenv_guia1"] + "-" + obj["ccomenv"]["cenv_guia2"] + "-" + obj["ccomenv"]["cenv_guia3"]);
    $(".politica").find(".texto").html(obj["ccomdoc"]["cdoc_politicanombre"]);
    $(".monto").find(".texto").html(CurrencyFormatted(obj["total"]["tot_total"]));


    var vseguro = 0;
    if (obj["total"]["tot_vseguro"] != null && obj["total"]["tot_vseguro"] != undefined)
        vseguro = parseFloat(obj["total"]["tot_vseguro"]);
    var porcseguro = 0;
    if (obj["total"]["tot_porc_seguro"] != null && obj["total"]["tot_porc_seguro"] != undefined)
        porcseguro = parseFloat(obj["total"]["tot_porc_seguro"]);

    var valorseguro = 0;
    if (vseguro > 0 && porcseguro > 0)
        valorseguro = vseguro * (porcseguro / 100);
    $(".declarado").find(".texto").html(CurrencyFormatted(obj["total"]["tot_vseguro"]));
    $(".seguro").find(".texto").html(CurrencyFormatted(valorseguro));    
    //$(".transporte").find(".texto").html(CurrencyFormatted(obj["total"]["tot_transporte"]));

    $(".transporte").html(CurrencyFormatted(obj["total"]["tot_transporte"]));

    var subtotal0 = parseFloat(obj["total"]["tot_subtot_0"].toString()) + parseFloat(obj["total"]["tot_transporte"].toString());
    var subtotal = parseFloat(obj["total"]["tot_subtot_0"].toString()) + parseFloat(obj["total"]["tot_transporte"].toString()) + parseFloat(obj["total"]["tot_subtotal"].toString());
    $(".subtotal").html(CurrencyFormatted(subtotal));
    $(".subtotal0").html(CurrencyFormatted(subtotal0));
    //$(".subttotal12").html(CurrencyFormatted(obj["total"]["tot_subtotal"]));
    $(".iva").html(CurrencyFormatted(obj["total"]["tot_timpuesto"]));      
    $(".total").html(CurrencyFormatted(obj["total"]["tot_total"]));
    $(".lbliva").html("IVA " + CurrencyFormatted(obj["total"]["tot_porc_impuesto"]) + "%:");

    if (obj["ccomenv"]["cenv_nombres_soc"]!="" && obj["ccomenv"]["cenv_nombres_soc"]!=null)
        $(".propietario").html(obj["ccomenv"]["cenv_nombres_soc"]);
    //$("#hojaruta").html(obj["com_doctran"]);
    // $("#conductor").html(obj["ccomenv"]["cenv_nombres_cho"]);
    // $("#nombreusuario").html(obj["crea_usrnombres"]);    

    if (obj["com_doctran"].toString().indexOf("GUI") >= 0) {        
        $(".numero").hide();
    }
    else {        
        $(".numerogui").hide();
        //$(".propietario").css("border-top", "0px dashed Black");
        $(".remitente").hide();
        $(".recibi").hide();
    }

    if (obj["com_claveelec"]!="" && obj["com_claveelec"]!=null)
    {
        $(".recibi").show();
        $(".numerogui").show();
        $(".remitente").show();

    }

}


function Print() {
    //jsPrintSetup.setPrinter('\\gtec-pc\EPSON L200 Series');
    //jsPrintSetup.setPrinter('EPSON L200 Series');
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