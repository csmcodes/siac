var formname = "wfMensaje.aspx";
var menuoption = "Mensaje";


var selectobj = null; //contiene el objeto seleccionado en el listado
var color = "LightSteelBlue";



//Codigo ejecutado cuando el document esta listo
$(document).ready(function () {

   
    LoadForm();     //Carga el formulario
    $("#listadocontent").on("scroll", scroll);
    $("#add").on("click", AddObj);          //Boton "Nuevo" de la barra
    $("#newedit").on("click", AddObj);      //Opción "Nuevo" del combo de opciones de la sección de edición
    $("#saveedit").on("click", SaveObj);    //Opción "Guardar" del combo de opciones de la sección de edición
    $("#deledit").on("click", AskDelete);   //Opción "Eliminar" del combo de opciones de la sección de edición
   
    $("#edit").on("click", ShowEdit);       //Boton "Editar" de la barra
   
    $("#refresh").on("click", ReloadData);  //Boton "Refrescar" de la sección de parametros
    $("#reflist").on("click", ReloadData);  //Boton "Refrescar" de la sección de parametros

    $("#edit").css({ 'display': 'none' });
    $("#list").css({ 'display': 'none' });
    $("#busqueda").css({ 'display': 'none' });

    //Codigo ejecutado cuando se preciona el botón "X" de la sección de edición
    $("#datos").find('.widgettitle .close').click(function () {
        $(this).parents('.widgetbox').fadeOut(function () {
            $(this).hide('fast', HideEdit());
        });
    });

    //Codigo ejecutado cuando se preciona el botón "X" de la sección de listado
    $("#listado").find('.widgettitle .close').click(function () {
        $(this).parents('.widgetbox').fadeOut(function () {
            $(this).hide('fast', HideList());
        });
    });

    //SetMenuOption();
    $('body').css('background', 'transparent');



});


function LoadDestino() {
    var obj = {};
    obj["empresa"] =parseInt(empresasigned["emp_codigo"]);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer(formname + "/GetDestino", jsonText, 6);
}

function LoadDestinoResult(data) {
    if (data != "") {
       $("#cmbDESTINO").removeAttr("style", "").removeClass("chzn-done").data("chosen", null).next().remove();       
       $("#cmbDESTINO").replaceWith(data.d);
       if ($("#cmbDESTINO option").length == 0)
            $("#cmbDESTINO").attr("disabled", true);
       else
            $("#cmbDESTINO").attr("disabled", false);
        $(".chzn-select").chosen();
    }

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
           
            if (retorno == 2)
                LoadFormResult(data);
            if (retorno == 3)
                ShowObjResult(data);
            if (retorno == 4)
                DeleteObjResult(data);
            if (retorno == 5)
                SaveObjResult(data);
            if (retorno == 6)
                LoadDestinoResult(data);

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







function LoadForm() {
    if ($("#datoscontent").length > 0) {
        CallServer(formname + "/GetForm", "{}", 2);
    }
}

function LoadFormResult(data) {
    if (data != "") {
        $('#datoscontent').html(data.d);
    }
    $(".fecha").datepicker({

        dateFormat: "dd/mm/yy"
    }); //Setea campos de tipo fecha
    // Select with Search
    $(".chzn-select").chosen();
    // tabbed widget
    $(".tabbedwidget").tabs();
 
    LoadDestino();


}




function ShowObj(obj) {
    var id = $(obj).data("id");
    var jsonText = JSON.stringify({ id: id });
    CallServer(formname + "/GetObject", jsonText, 3)

}

function ShowObjResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        SetJSONObject(obj); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM
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
    ShowObj(selectobj);
    StopPropagation();
}

function AddObj() {
    ClearValidate();
    ShowEdit();
    var rows = $("#listadocontent li");
    $(rows).css("background-color", "");
    selectobj = null;
    CleanForm(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
}



/*************************DELETE FUNCTIONS *******************************/
function AskDelete() {

    jConfirm('¿Está seguro que desea eliminar el registro?', 'Eliminar', function (r) {
        if (r)
            DeleteObj();

    });
}

function DeleteObj() {
    var jsonText = GetJSONObject(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
    CallServer(formname + "/DeleteObject", jsonText, 4);

}


function DeleteObjResult(data) {
    if (data != "") {
        jQuery.jGrowl("Registro eliminado con éxito");
        CleanForm(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
        ReloadData();
    }
}


/***********************SAVE FUNCTIONS ***********************************/

function ClearValidate() {
    var controles = $("#datoscontent").find('[data-obligatorio="True"]');
    $("#datoscontent").find('[data-obligatorio="True"]').each(function () {
        $(this.parentNode).removeClass('obligatorio')
        $(this.parentNode).children(".obligatorio").remove();
    });
}

function ValidateForm() {
    var retorno = true;
    var controles = $("#datoscontent").find('[data-obligatorio="True"]');
    $("#datoscontent").find('[data-obligatorio="True"]').each(function () {
        $(this.parentNode).removeClass('obligatorio')
        $(this.parentNode).children(".obligatorio").remove();
        if ($(this).val() == "") {
            retorno = false;
            var padre = $(this.parentNode);
            $(this.parentNode).append("<span class='obligatorio'>! Obligatorio</span>");
            $(this.parentNode).addClass('obligatorio')
        }

    });
    return retorno;
}


function SaveObj() {
    if (ValidateForm()) {
        var jsonText = GetJSONObject(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
        CallServer(formname + "/SaveObject", jsonText, 5);
    }

}

function SaveObjResult(data) {
    if (data != "") {
        if (data.d != "OK" && data.d.toString() != "ERROR") {
            jQuery.jGrowl("Registro actualizado con éxito");
            $(selectobj).html(data.d);
        }
        else {
            CleanForm(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM  
            ReloadData();
        }
    }
}


/*********************FUNCIONES PARA OCULTAR Y MOSTRAR SECCIONES**********************/


function ShowEdit() {

    $("#listado").animate({
        width: "100%"
    }, 100, function () {
        $('#datos').show("fast");
        $("#edit").css({ 'display': 'none' })
    });

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








function GetJSONObject() {
    var obj = {};
    obj["msj_empresa"] = parseInt(empresasigned["emp_codigo"]);
    obj["msj_codigo"] = $("#txtCODIGO").val();
    obj["msj_codigo_key"] = $("#txtCODIGO_key").val();    
    obj["msj_empresa_key"] =parseInt(empresasigned["emp_codigo"]);
    obj["msj_usuarioenvia"] = $("#txtUSUARIOENVIA").val();
    obj["msj_mensaje"] = $("#txtMENSAJE").val();
    obj["msj_estadoenvio"] = $("#txtESTADOENVIA").val();    
    obj["msj_id"] = $("#txtID").val();
    obj["msj_asunto"] = $("#txtASUNTO").val();
    obj["msj_fechacreacion"] = $("#cmbFECHACREACION").val();
    obj["msj_fechaenvio"] = $("#cmbFECHAENNVIO").val();
    obj["msj_estado"] = GetCheckValue($("#chkESTADO"));
    obj["destino"] = $("#cmbDESTINO").val();
    obj = SetAuditoria(obj);
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}

function SetJSONObject(obj) {
   
    $("#txtCODIGO").val(obj["msj_codigo"]);
    $("#txtCODIGO_key").val(obj["msj_codigo_key"]);
 
    $("#txtUSUARIOENVIA").val(obj["msj_usuarioenvia"]);
    $("#txtMENSAJE").val(obj["msj_mensaje"]);
    $("#txtESTADOENVIA").val(obj["msj_estadoenvio"]);
    $("#txtID").val(obj["msj_id"]);
    $("#txtASUNTO").val(obj["msj_asunto"]);
    $("#cmbFECHACREACION").val(GetDateValue(obj["msj_fechacreacion"]));
    $("#cmbFECHAENNVIO").val(GetDateValue(obj["msj_fechaenvio"]));
    $("#chkESTADO").prop("checked", SetCheckValue(obj["msj_estado"]));
    $("#lblcrea").html("Creado por: " + obj["crea_usr"] + " " + GetDateValue(obj["crea_fecha"]));
    $("#lblmod").html("Creado por: " + obj["mod_usr"] + " " + GetDateValue(obj["mod_fecha"]));

    var objtipos = new Array();
    for (i = 0; i < obj["destino"].length; i++)
    { objtipos[i] = obj["destino"][i]["msd_usuario"].toString(); }

    $("#cmbDESTINO").val(objtipos);
    $('#cmbDESTINO').trigger('liszt:updated');

}

function GetJSONSearch() {
    var obj = {};
    obj["msj_codigo"] = $("#txtCODIGO_S").val();
    obj["msj_nombre"] = $("#txtNOMBRE_S").val();
    obj["msj_id"] = $("#txtID_S").val();
   
    if ($("#cmbESTADO_S").val() != "")
        obj["msj_estado"] = parseInt($("#cmbESTADO_S").val());
    var jsonText = JSON.stringify({ objeto: obj });
    return jsonText;
}


function CleanSearch() {
    
    $("#cmbESTADO_S").val("");
    $("#txtCODIGO_S").val("");
    $("#txtNOMBRE_S").val("");
    $("#txtID_S").val("");
 
    ReloadData();
}

function CleanForm() {
    
    $("#txtCODIGO").val("");
    $("#txtCODIGO_key").val("");
    
    $("#txtUSUARIOENVIA").val("");
    $("#txtMENSAJE").val("");
    $("#txtESTADOENVIA").val("");
    $("#txtID").val("");
    $("#txtASUNTO").val("");
    $("#cmbFECHACREACION").val("");
    $("#cmbFECHAENNVIO").val("");
    
    $("#cmbDESTINO").val("");
    $('#cmbDESTINO').trigger('liszt:updated'); 
    $("#chkESTADO").prop("checked", true);
}