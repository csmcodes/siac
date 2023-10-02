//CODIGO wfLogin.aspx
//Desarrollado por: Cristhian Sanmartin
//Fecha:            Julio 2013
//Descripción:




$(document).ready(function () {

    $("#btnsignin").on("click", SignIn);

});


function LoadEmpresas() {
    CallServer("ws/Metodos.asmx/GetEmpresas", "{}", 1);
}
function LoadEmpresasResult(data) {
    if (data != "") {
        $("#empresa").replaceWith(data.d);
    }
}

function SignIn() {
    var id = $('#username').val()
    var jsonText = JSON.stringify({ id: id });
    CallServer("ws/Metodos.asmx/GetUsuario", jsonText, 0);
}

function SignInResult(data) {
    if (data != "") {
        var mensaje = "";
        var obj = $.parseJSON(data.d);
        var id = $('#username').val();
        var pass = $('#password').val();
        if (obj["usr_estado"] != null) {
            if (obj["usr_password"] == pass) {
                //$.cookie('usrdata', JSON.stringify(obj));
                SetSignedUser(obj);
                window.location("Default.aspx");                
            }
            else
                mensaje = "Contraseña incorrecta";
        }
        else
            mensaje = "No existe el usuario";
        $('.login-alert').children().html(mensaje);
        $('.login-alert').fadeIn();
    }

}

function verifica() {

    var u = $('#username').val();
    var p = $('#password').val();

    var obj = {};
    obj["ID"] = $("#username").val();
    obj["ID_key"] = $("#username").val();
    obj["PASSWORD"] = $("#password").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServer("ws/Metodos.asmx/VerificaUsuario", jsonText, 0);

}


function CallServer(strurl, strdata, retorno) {
    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (retorno == 0)
                SignInResult(data);
            if (retorno == 1)
                LoadEmpresasResult(data);


        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var errorData = $.parseJSON(XMLHttpRequest.responseText);
            alert(errorData.Message);
            //alert(errorData.Message);
        }

    })
}


function GetCallSelectEmpresa(obj) {
    SetEmpresa(obj);
    window.location("Index.aspx");
}

