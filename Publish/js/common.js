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
    LoadForm();     //Carga el formulario
    $("#listadocontent").on("scroll", scroll);
    $("#add").on("click", AddObj);          //Boton "Nuevo" de la barra
    $("#newedit").on("click", AddObj);      //Opción "Nuevo" del combo de opciones de la sección de edición
    $("#saveedit").on("click", SaveObj);    //Opción "Guardar" del combo de opciones de la sección de edición
    $("#deledit").on("click", AskDelete);   //Opción "Eliminar" del combo de opciones de la sección de edición
    $("#search").on("click", ShowBusqueda); //Boton "Buscar" de la barra
    $("#edit").on("click", ShowEdit);       //Boton "Editar" de la barra
    $("#list").on("click", ShowList);       //Boton "Listado" de la barra
    $("#refresh").on("click", ReloadData);  //Boton "Refrescar" de la sección de parametros
    $("#reflist").on("click", ReloadData);  //Boton "Refrescar" de la sección de parametros
    $("#closeedit").on("click", HideEdit);  //Opción "Cerrar" del combo de opciones de la sección de edición
    $("#closelist").on("click", HideList);  //Opción "Cerrar" del combo de opciones de la sección de listado
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


function OpenMenuOption(obj) {
    if ($(obj).length>0) {
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
            if (retorno == 2)
                LoadFormResult(data);
            if (retorno == 3)
                ShowObjResult(data);            
            if (retorno == 4)
                DeleteObjResult(data);
            if (retorno == 5)
                SaveObjResult(data);

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
       // $('#cargando').html('<img src="images/loaders/loader10.gif">');
        // var jsonText = GetJSONSearch(); //TOCA DETERMINAR DEPENDIENDO DE CADA FORM  
        var obj = {};
        obj["per_id"] = $("#txtID").val();
        obj["per_id_key"] = $("#txtID_key").val();
        obj["per_descripcion"] = $("#txtDESCRIPCION").val();
        //   obj["NOMBRES"] = $("#txtNOMBRES").val();
        //    obj["PERFIL"] = $("#cmbPERFIL").val();
        obj["per_estado"] = GetCheckValue($("1"));
        var jsonText = JSON.stringify({ objeto: obj });
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


function LoadForm() {
    if ($("#datoscontent").length > 0) {
        //  CallServer(formname + "/GetForm", "{}", 2);
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
    SetForm();//Depende de cada js.


}




function ShowObj(obj) {
    var id = $(obj).data("id");
    var jsonText = JSON.stringify({ id: id });
    CallServer(formname + "/GetObject", jsonText, 3)

}

function ShowObjResult(data) {
    if (data != "") {
        var obj = $.parseJSON(data.d);
        SetJSONObject(obj);//TOCA DETERMINAR DEPENDIENDO DE CADA FORM
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
        CallServer(formname +"/SaveObject", jsonText, 5);
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

function HideEdit() {
    $("#datos").hide('fast', function () {
        var cambiosCSS1 =
                {
                    width: "100%"
                };
        $("#listado").css(cambiosCSS1);
        $("#edit").css({ 'display': '' })
    });   
}

function ShowEdit() {

    $("#listado").animate({
        width: "50%"
    }, 100, function () {
        $('#datos').show("fast");
        $("#edit").css({ 'display': 'none' })
    });
       
}

function HideList() {
    $("#listado").hide('fast', function () {
        var cambiosCSS1 =
                {
                    width: "100%"
                };
        $("#datos").css(cambiosCSS1);
        $("#list").css({ 'display': '' })
    });
}

function ShowList() {
    $("#datos").animate({
        width: "49%"
    }, 100, function () {
        $('#listado').show("fast");
    });
    $("#list").css({ 'display': 'none' })

}

function ShowBusqueda() {
    $('#busqueda').show("fast");  
}

