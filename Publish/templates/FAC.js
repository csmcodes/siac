var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "FAC.aspx";
var menuoption = "FAC";


//Codigo ejecutado cuando el document esta listo
$(document).ready(function () {


    LoadComprobante();

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
});





function ServerResult(data, retorno) {
    if (retorno == 0) {
        LoadComprobanteResult(data);
    }
}

function LoadComprobante() {
    var obj = {};
    obj["com_codigo"] = $("#txtcodigo").val();
    obj["com_empresa"] = empresasigned["emp_codigo"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(formname + "/GetComprobante", jsonText, 0)
}



function LoadComprobanteResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        $(".numero").html(obj["com_doctran"]);
        $(".fechacrea").html(GetDateValue(obj["crea_fecha"]));
        $(".dataizq.fecha").html(GetDateValue(obj["com_fecha"]));
        $(".dataizq.cliente").html(obj["ccomdoc"]["cdoc_nombre"]);
        $(".dataizq.ruc").html(obj["ccomdoc"]["cdoc_ced_ruc"]);
        $(".dataizq.direccion").html(obj["ccomdoc"]["cdoc_direccion"]);
        $(".dataizq.telefono").html(obj["ccomdoc"]["cdoc_telefono"]);

        $(".datader.destinatario").html(obj["ccomenv"]["cenv_apellidos_des"] + " " + obj["ccomenv"]["cenv_nombres_des"]);
        $(".datader.direcciondes").html(obj["ccomenv"]["cenv_direccion_des"]);
        $(".datader.ciudaddes").html(obj["ccomenv"]["cenv_rutadestino"]);
        $(".datader.remitente").html(obj["ccomenv"]["cenv_apellidos_rem"] + " " + obj["ccomenv"]["cenv_nombres_rem"]);

        $(".propietario").html(obj["ccomenv"]["cenv_nombres_soc"]);
        $(".conductor").html(obj["ccomenv"]["cenv_nombres_cho"]);

        var totalcantidad = 0;

        $(".cantidad").html("");
        $(".descripcion").html("");
        $(".peso").html("");
        $(".valorunitario").html("");
        $(".valortotal").html("");


        for (i = 0; i < obj["ccomdoc"]["detalle"].length; i++) {
            var item = obj["ccomdoc"]["detalle"][i];

            totalcantidad += item["ddoc_cantidad"];
            $(".cantidad").append(item["ddoc_cantidad"].toString() + "<br/>");

            var observaciones = item["ddoc_productonombre"] + " " + item["ddoc_observaciones"];
            $(".descripcion").append(observaciones + "<br/>");

            var peso = "";
            var preciou = item["ddoc_precio"];
            for (var d = 0; d < item["detallecalculo"].length; d++) {
                var dc = item["detallecalculo"][d];
                peso += dc["dcpr_indicedigitado"];
                if (dc["dcpr_valor"] != null)
                    preciou = dc["dcpr_valor"];
            }
            $(".peso").append(peso + "<br/>");
            $(".valorunitario").append(preciou.toFixed(2) + "<br/>");
            $(".valortotal").append(item["ddoc_total"].toFixed(2) + "<br/>");

        }




        $(".datadownizq.bultos").html(totalcantidad);
        $(".datadownizq.declarado").html((obj["total"]["tot_vseguro"] != null) ? obj["total"]["tot_vseguro"].toFixed(2) : "0.00");
        $(".datadownizq.guia").html(obj["ccomenv"]["cenv_guia1"] + "-" + obj["ccomenv"]["cenv_guia2"] + "-" + obj["ccomenv"]["cenv_guia3"]);
        $(".datadownizq.lugar").html(obj["ccomenv"]["cenv_observacion"]);
        $(".datadownizq1.politica").html(obj["ccomdoc"]["cdoc_politicanombre"]);
        $(".datadownizq1.seguro").html((obj["total"]["tot_tseguro"] != null) ? obj["total"]["tot_tseguro"].toFixed(2) : "0.00");

        var subtotal = obj["total"]["tot_subtot_0"] + obj["total"]["tot_subtotal"];

        $(".datadownder.subtotal0").html(obj["total"]["tot_subtot_0"].toFixed(2));
        $(".datadownder.subtotal12").html(obj["total"]["tot_subtotal"].toFixed(2));
        $(".datadownder.subtotal").html(subtotal.toFixed(2));
        $(".datadownder.iva").html(obj["total"]["tot_timpuesto"].toFixed(2));
        $(".datadownder.total").html(obj["total"]["tot_total"].toFixed(2));



        Print(obj["impresora"]);

       // window.onfocus = function () { window.close(); }         

        //window.print();
        
    }


    function Print(printer) {
        jsPrintSetup.setPrinter(printer);
        jsPrintSetup.setSilentPrint(0);
        jsPrintSetup.setOption('headerStrLeft', '');
        jsPrintSetup.setOption('headerStrCenter', '');
        jsPrintSetup.setOption('headerStrRight', '');
        jsPrintSetup.setOption('footerStrLeft', '');
        jsPrintSetup.setOption('footerStrCenter', '');
        jsPrintSetup.setOption('footerStrRight', '');
        
        //jsPrintSetup.print();
        //var list = jsPrintSetup.getPaperSizeList();
//        jsPrintSetup.undefinePaperSize(101);
//        jsPrintSetup.definePaperSize(101, 101, "Custom", "Custom_Paper", "Custom PAPER", 2160, 1400, jsPrintSetup.kPaperSizeMillimeters);
//        jsPrintSetup.setPaperSizeData(101);
        // here window is current frame!
        jsPrintSetup.print();
        window.close();
        //setInterval(function () { window.close() }, 1000)

        
        //jsPrintSetup.printWindow(window);
        
    }
}
