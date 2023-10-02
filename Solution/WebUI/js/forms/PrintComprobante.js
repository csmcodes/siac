//Archivo:          PrintComprobante.js
//Descripción:      Carga un comprobante para impresion
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Abril 2014
//2014. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados



//Codigo ejecutado cuando el document esta listo
$(document).ready(function () {

    var codigo = GetQueryStringParams('codigo');
    LoadComprobante(codigo);

});


function LoadComprobante(codigo) {
    var obj = {};
    obj["com_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["com_codigo"] = codigo;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "GetComprobante", jsonText, 0);

}

function ServerResult(data, retorno) {
    if (data != "") {
        if (retorno == 0) {
            var obj = $.parseJSON(data.d);
            ShowComprobante(obj);
        }
        
    }
}

function ShowComprobante(obj) {
    $("#numero").text(obj["com_doctran"]);
    //CABECERA
    $("#fecha").text(GetDateValue(obj["com_fecha"]));    
    $("#cliente").text(obj["ccomdoc"]["cdoc_nombre"]);
    $("#ruc").text(obj["ccomdoc"]["cdoc_ced_ruc"]);
    $("#direccioncliente").text(obj["ccomdoc"]["cdoc_direccion"]);

    $("#destinatario").text(obj["ccomenv"]["cenv_apellidos_des"] + " " + obj["ccomenv"]["cenv_nombres_des"]);
    $("#direcciondestino").text(obj["ccomenv"]["cenv_direccion_des"]);
    $("#ciudadestino").text(obj["ccomenv"]["cenv_rutadestino"]);
    $("#remitente").text(obj["ccomenv"]["cenv_apellidos_rem"] + " " + obj["ccomenv"]["cenv_nombres_rem"]);

    var cantbultos = 0;
    //DETALLE

    for (var i = 0; i < obj["ccomdoc"]["detalle"].length; i++) {
        cantbultos += parseInt(obj["ccomdoc"]["detalle"][i]["ddoc_cantidad"]); 

        var fila = "<tr>";
        fila += "<td>" + obj["ccomdoc"]["detalle"][i]["ddoc_cantidad"] + "<td>";
        fila += "<td>" + obj["ccomdoc"]["detalle"][i]["ddoc_productonombre"] + " " + obj["ccomdoc"]["detalle"][i]["ddoc_observaciones"] + "<td>";
        fila += "<td><td>";
        fila += "<td>" + obj["ccomdoc"]["detalle"][i]["ddoc_precio"] + "<td>";
        fila += "<td>" + obj["ccomdoc"]["detalle"][i]["ddoc_total"] + "<td>";
        fila += "</tr>";

        $("#detalle").append(fila);

    }

    //PIE
    $("#bultos").text(cantbultos);
    $("#valordeclarado").text(obj["total"]["tot_vseguro"]);
    $("#valorseguro").text(obj["total"]["tot_tseguro"]);
    $("#guia").text(obj["ccomenv"]["cenv_guia1"] + "-" + obj["ccomenv"]["cenv_guia2"] + "-" + obj["ccomenv"]["cenv_guia3"]);
    $("#entrega").text(obj["ccomenv"]["cenv_observacion"]);
    $("#politica").text(obj["ccomdoc"]["cdoc_politicanombre"]);


    $("#subtotal0").text(obj["total"]["tot_subtot_0"]);
    $("#subtotal12").text(obj["total"]["tot_subtotal"]);

    var valorsubtotal = parseFloat(obj["total"]["tot_subtot_0"]) + parseFloat(obj["total"]["tot_subtotal"]);  
    $("#subtotal").text(valorsubtotal);
    $("#valoriva").text(obj["total"]["tot_timpuesto"]);
    $("#total").text(obj["total"]["tot_total"]);



   
}