var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";
var rgbcolor = "rgb(176, 196, 222)"
var formname = "wfPeriodos.aspx";
var menuoption = "Periodos";

function StopPropagation(event) {
    if (!event) var event = window.event;
    event.cancelBubble = true;
    if (event.stopPropagation) event.stopPropagation();
}

//Codigo ejecutado cuando el document esta listo
$(document).ready(function () {
    $('body').css('background', 'transparent');
    LoadFiltros();
});

function ServerResult(data, retorno) {
    if (retorno == 0) {
        LoadFiltrosResult(data);
    }
    if (retorno == 1) {
        ActualizaResult(data);
    }
    if (retorno == 2)
        Actualiza();
}




function LoadFiltros() {
    var obj = {};
    //obj["uxe_usuario"] = usuariosigned["usr_id"];
    //obj["uxe_empresa"] = empresasigned["emp_codigo"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(formname + "/GetFiltros", jsonText, 0)
}



function LoadFiltrosResult(data) {
    if (data != "") {
        $('#comcabecera').html(data.d);
        //LoadDetalle();
    }
    SetForm(); //Depende de cada js.        
}

function SetForm() {

    $(".fecha").datepicker({
        dateFormat: "dd/mm/yy"
    });
  


}


function Actualiza() {

    var obj = {};
    obj["empresa"] = empresasigned["emp_codigo"];
    obj["periodo"] = $("#txtPERIODO").val();
    obj["usuario"] = usuariosigned["usr_id"];
    obj["audit"] = GetQueryStringParams("audit");
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(formname + "/GetPeriodos", jsonText, 1);

}



function ActualizaResult(data) {
    if (data != "") {
        $('#comdetalle').html(data.d);
    }
}


function OpenClose(mes, estado)
{
    var obj = {};
    obj["empresa"] = empresasigned["emp_codigo"];
    obj["periodo"] = $("#txtPERIODO").val();
    obj["mes"] = mes;
    obj["estado"] = estado;
    obj["usuario"] = usuariosigned["usr_id"];
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(formname + "/OpenClosePeriodo", jsonText, 2);
    
}
