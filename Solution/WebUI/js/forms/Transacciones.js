var formname = "wfTransacciones.aspx";
var menuoption = "Transacciones";

//Archivo:          Common.js
//Descripción:      Contiene las funciones comunes para las interfaces
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Julio 2013
//2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados


var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";



//Codigo ejecutado cuando el document esta listo
$(document).ready(function () { 
    LoadList();     //Carga el listado 
    LoadSearch();   //Carga los parametros de busqueda   
    $("#listadocontent").on("scroll", scroll);
    $("#search").on("click", ShowBusqueda); //Boton "Buscar" de la barra
    $("#list").on("click", ShowList);       //Boton "Listado" de la barra
    $("#refresh").on("click", ReloadData);  //Boton "Refrescar" de la sección de parametros
    $("#reflist").on("click", ReloadData);  //Boton "Refrescar" de la sección de parametros
   
   
    $("#edit").css({ 'display': 'none' });
    $("#list").css({ 'display': 'none' });
    $("#busqueda").css({ 'display': 'none' });

   
 

    //SetMenuOption();
    $('body').css('background', 'transparent');



});


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


function SetMenuOption() {

    $(".active").removeClass('active');
    OpenMenuOption($("#" + menuoption));
    $("#" + menuoption).addClass('active');
}

function HasDynamicScroll() {
    if (formname == "wfArmaMenu.aspx" || formname == "wfCuenta.aspx" || formname == "wfCentro.aspx" || formname == "wfTproducto.aspx")
        return false;
    return true;
}


//Funcion que controla el scroll dinamico del listado
function scroll() {
    if (HasDynamicScroll()) {
        if ($("#listadocontent")[0].scrollHeight - $("#listadocontent").scrollTop() <= $("#listadocontent").outerHeight()) {
            LoadList();
        }
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
                LoadListResult(data);
            if (retorno == 1)
                LoadSearchResult(data);
           
            if (retorno == 3)
                ShowObjResult(data);
            if (retorno == 4)
                SelectObjResult(data);

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


function ReloadData() {
    $('#listadocontent').empty();
    var jsonText = GetJSONSearch(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
    CallServer(formname + "/ReloadData", jsonText, 0);
}

function LoadList() {
    if ($("#listadocontent").length > 0) {
        $('#cargando').html('<img src="images/loaders/loader10.gif">');
        var jsonText = GetJSONSearch(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
        CallServer(formname + "/GetData", jsonText, 0);
    }
}

function LoadListResult(data) {
    if (data != "") {
        $('#listadocontent').append(data.d);
    }
    $('#cargando').empty();
}


function LoadSearch() {
    if ($("#busquedacontent").length > 0) {
        CallServer(formname + "/GetSearch", "{}", 1);
    }
}

function LoadSearchResult(data) {
    if (data != "") {
        $('#busquedacontent').html(data.d);
    }
}







function ShowObj(obj) {
    var id = $(obj).data("id");
    var jsonText = JSON.stringify({ id: id });
    CallServer(formname + "/GetObject", jsonText, 3)

}

function ShowObjResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
   //     SetJSONObject(obj); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM
    }
}

function StopPropagation(event) {
    if (!event) var event = window.event;
    event.cancelBubble = true;
    if (event.stopPropagation) event.stopPropagation();

}

function Select(obj) {
    var rows = $("#listadocontent li");
    $(rows).css("background-color", "");
    $(obj).css("background-color", color);
    selectobj = $(obj).children();
    var id = $(selectobj).data("id");
    var jsonText = JSON.stringify({ objeto: id });

    CallServer(formname + "/SelectObj", jsonText, 4);
}

function SelectObjResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        var id = $(selectobj).data("id");
        if (obj.for_id.indexOf("?")>0)
            window.location = obj.for_id + "&tipodoc=" + id.udo_tipodoc;
            else
                window.location = obj.for_id + "?tipodoc=" + id.udo_tipodoc;
    }
}



/*********************FUNCIONES PARA OCULTAR Y MOSTRAR SECCIONES**********************/


function ShowList() {
    $("#datos").animate({
        width: "100%"
    }, 100, function () {
        $('#listado').show("fast");
    });
    $("#list").css({ 'display': 'none' })

}

function ShowBusqueda() {
    $('#busqueda').show("fast");
}




/****************************FUNCIONES BASICAS*******************************/

function GetCheckValue(obj) {
    if ($(obj).length > 0) {
        if ($(obj)[0].checked)
            return 1;
        else
            return 0;
    }
    //return null;
}

function SetCheckValue(value) {
    if (value != null) {
        if (value.toString() == "1")
            return true;
    }
    return false;
    //return null;
}

function GetDateValue(value) {
    if (value != null) {
        var date = new Date(parseInt(value.substr(6)));
        return date.toLocaleString();
        //return eval(value.slice(1, -1));
    }
    return "";
}



function ClearValidate() {
    var controles = $("#datoscontent").find('[data-obligatorio="True"]');
    $("#datoscontent").find('[data-obligatorio="True"]').each(function () {
        $(this.parentNode).removeClass('obligatorio')
        $(this.parentNode).children(".obligatorio").remove();
    });
}




function SetForm() {
    CleanForm();
    HideEdit();
    
}




function GetJSONSearch() {
    var obj = {};
  
    obj["udo_idtipodoc"] = $("#txtNOMBRE_S").val();
    obj["udo_usuario"] = usuariosigned["usr_id"];
    if ($("#cmbESTADO_S").val() != "")
        obj["udo_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
    
    $("#cmbESTADO_S").val("");   
    $("#txtNOMBRE_S").val("");    
    ReloadData();
}
