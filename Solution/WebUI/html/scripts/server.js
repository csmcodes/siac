//Archivo:          server.js
//Descripción:      Contiene los metodos para conectarse con el servidor y traer datos de BD
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            Mayo  2014
//2014. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados

//Funciona que invoca al servidor mediante JSON


var webservice = "/ws/Metodos2.asmx/";

function CallServerMethods(strurl, strdata, retorno, loading) {


    if (loading??true)
        ShowLoading();
    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (loading ?? true)
                QuitLoading();
            try {

                switch(retorno)
                {
                    case "MENU":
                        $(".page-sidebar-menu").html(data.d);
                        break;
                    case "PAGETITLE":
                        $(".page-title").html(data.d);
                        break;
                    case "PAGENAV":
                        $(".page-breadcrumb").html(data.d);
                        break;
                    case "PAGEOPC":
                        $(".page-toolbar").find("ul").html(data.d);
                        break;
                    default:
                        ServerResult(data, retorno);

                }                
                    
            }
            catch (err) {
                Message("error", err, null, null, null);
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            if (loading ?? true)
                QuitLoading();
            //var errorData = $.parseJSON(XMLHttpRequest.responseText);
            alert(errorThrown);
            
        }

    })
}



function GetMenu(menuoption) {

    var obj = JsonObj();    
    obj["usr_id"] = GetOnlineUserRol();
    obj["men_id"] = menuoption;    
    CallServerMethods(webservice + "GetMenu", JsonObjString(obj), "MENU");

    GetPageTitle(menuoption);
    GetPageNav(menuoption);
    GetPageOpc(menuoption);
}





function GetPageTitle(id)
{
    var obj = JsonObj();
    obj["men_id"] = id;    
    CallServerMethods(webservice + "GetPageTitle", JsonObjString(obj), "PAGETITLE");
}


function GetPageNav(id) {
    var obj = JsonObj();
    obj["men_id"] = id;
    CallServerMethods(webservice + "GetPageNav", JsonObjString(obj), "PAGENAV");
}

function GetPageOpc(id)
{
    var obj = JsonObj();
    obj["men_id"] = id;
    CallServerMethods(webservice + "GetPageOpc", JsonObjString(obj), "PAGEOPC");
}


function ShowLoading() {
    $("body").append("<div class='loading'>Cargando...</div>");
    $(".loading").show("fade in");
}

function QuitLoading() {
    $(".loading").hide("fade out");
    $(".loading").remove();
    
}