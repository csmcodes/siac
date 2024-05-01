$(document).ready(function () {
        
    $("#datosusr").html(usuariosigned["usr_nombres"] + "<small>- " + usuariosigned["usr_mail"] + "</small>");
    $("#datosemp").html("<b>" + empresasigned["emp_nombre"] + "<b>");

    LoadMenu();

});


function CallServerIndex(strurl, strdata, retorno) {
    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (retorno == 0)
                LoadMenuResult(data);


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


function LoadMenu() {
    if (usuariosigned) {
        var jsonText = JSON.stringify({ objeto: usuariosigned });

        CallServerIndex("ws/Metodos.asmx/GetMenuByUser", jsonText, 0);
    }
    //var jsonText = "{}"; //FILTROS POR EL USUARIO PARA DETERMINAR ACCESOS
    //CallServerGen("ws/Metodos.asmx/GetMenu", jsonText, 0);

}

function LoadMenuResult(data) {
    if (data != "") {
        $('#sysmenu').html(data.d);
    }
    $('.leftmenu .dropdown > a').click(function () {
        if (!$(this).next().is(':visible'))
            $(this).next().slideDown('fast');
        else
            $(this).next().slideUp('fast');
        return false;
    });
}