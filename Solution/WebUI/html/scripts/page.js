//Archivo:          page.js
//Descripción:      Contiene los metodos para cargar las partes HTML reutilizables de cada pagina
//                  como el Header, Menu y Footer
//Desarrollador:    Cristhian Sanmartin M.
//Fecha:            08/02/2017
//SIAC Sistemas Inteligentes


function CallServerPage(strurl, strdata, retorno) {


    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            try {

                switch (retorno) {
                    case "MENU":
                        //$(".page-sidebar-menu").html(data.d);
                        var obj = $.parseJSON(data.d)
                        //$(".page-sidebar-menu").html(obj[0]);
                        $(".page-title").html(obj[1]);
                        $(".page-breadcrumb").html(obj[2]);
                        $(".page-toolbar").find("ul").html(obj[3]);
                        break;
                }

            }
            catch (err) {
                Message("error", err, null, null, null);
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //var errorData = $.parseJSON(XMLHttpRequest.responseText);
            alert(errorThrown);

        }

    })
}


function LoadPageParts() {
    LoadLinksRel();//CARGA LAS REFERENCIAS A HOJAS DE ESTILOS ESPECIALES
    LoadHeader();
    LoadFooter();
    LoadMenu();
        
}



function LoadHeader()
{
    $.get("header.html", function (data) {
        $(".page-header").append(data);
        $(".username").html(GetOnlineUserName() + "-" + GetOnlineCompanyName());
    });
    
}


function LoadLinksRel()
{
    var $head = $("head");
    var $headlinklast = $head.find("link[rel='stylesheet']:last");
    
    $head.append("<link href='theme/assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css' rel='stylesheet' type='text/css' />");
    $head.append("<link href='theme/assets/global/plugins/typeaheadold/typeahead.css' rel='stylesheet' type='text/css'>");
    $head.append("<link href='theme/assets/global/plugins/select2/css/select2.min.css' rel='stylesheet' type='text/css'>");
    $head.append("<link href='theme/assets/global/plugins/select2/css/select2-bootstrap.min.css' rel='stylesheet' type='text/css'>");
    $head.append("<link href='theme/assets/global/plugins/bootstrap-toastr/toastr.min.css' rel='stylesheet' type='text/css'>");
    $head.append("<link href='theme/assets/global/plugins/bootstrap-table/bootstrap-table.min.css' rel='stylesheet' type='text/css' />");
    $head.append("<link href='theme/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css' rel='stylesheet' type='text/css' />");
    $head.append("<link rel='stylesheet' type='text/css' href='theme/assets/global/plugins/jstree/dist/themes/default/style.min.css' />");
    $head.append("<link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css'>");
    $head.append("<link href='css/estilos.css' rel='stylesheet' />");
    //LINK PARA EL ICONO DE SIAC EN LA PAGINA
    $head.append("<link rel='shortcut icon' type='image/x-icon' href='http://siac.gtec.com.ec/wp-content/uploads/2017/01/favicon.png'>");


    //var linkElement = "<link rel='shortcut icon' type='image/x-icon' href='http://siac.gtec.com.ec/wp-content/uploads/2017/01/favicon.png'>";
    //if ($headlinklast.length) {
    //    $headlinklast.after(linkElement);
    //}
    //else {
    //    $head.append(linkElement);
    //}
}

function LoadFooter() {
    $.get("footer.html", function (data) {
        $(".page-footer").append(data);
    });

}

function LoadMenu()
{
    var obj = JsonObj();
    var querystring = window.location.search.substring(1);
    var page = document.location.pathname.match(/[^\/]+$/)[0];   
    obj["page"] = page;
    obj["query"] = querystring;
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerPage(webservice+ "GetMenuByUser", jsonText, "MENU");

}


LoadPageParts();