

$(document).ready(function () {

    $('.leftmenu .dropdown > a').click(function () {
        if (!$(this).next().is(':visible'))
            $(this).next().slideDown('fast');
        else
            $(this).next().slideUp('fast');
        return false;
    });


    if ($.uniform)
        $('input:checkbox, input:radio, .uniform-file').uniform();

    if ($('.widgettitle .close').length > 0) {
        $('.widgettitle .close').click(function () {
            $(this).parents('.widgetbox').fadeOut(function () {
                $(this).hide();
            });
        });
    }


    // add menu bar for phones and tablet
    $('<div class="topbar"><a class="barmenu">' +
		    '</a></div>').insertBefore('.mainwrapper');

    $('.topbar .barmenu').click(function () {

        var lwidth = '260px';
        if ($(window).width() < 340) {
            lwidth = '240px';
        }

        if (!$(this).hasClass('open')) {
            $('.rightpanel, .headerinner, .topbar').css({ marginLeft: lwidth }, 'fast');
            $('.logo, .leftpanel').css({ marginLeft: 0 }, 'fast');
            $(this).addClass('open');
        } else {
            $('.rightpanel, .headerinner, .topbar').css({ marginLeft: 0 }, 'fast');
            $('.logo, .leftpanel').css({ marginLeft: '-' + lwidth }, 'fast');
            $(this).removeClass('open');
        }
    });

    // show/hide left menu
    $(window).resize(function () {
        if (!$('.topbar').is(':visible')) {
            $('.rightpanel, .headerinner').css({ marginLeft: '260px' });
            $('.logo, .leftpanel').css({ marginLeft: 0 });
        } else {
            $('.rightpanel, .headerinner').css({ marginLeft: 0 });
            $('.logo, .leftpanel').css({ marginLeft: '-260px' });
        }
    });

    // dropdown menu for profile image
    $('.userloggedinfo img').click(function () {
        if ($(window).width() < 480) {
            var dm = $('.userloggedinfo .userinfo');
            if (dm.is(':visible')) {
                dm.hide();
            } else {
                dm.show();
            }
        }
    });

    // change skin color
    $('.skin-color a').click(function () { return false; });
    $('.skin-color a').hover(function () {
        var s = $(this).attr('href');
        if ($('#skinstyle').length > 0) {
            if (s != 'default') {
                $('#skinstyle').attr('href', 'css/style.' + s + '.css');
                $.cookie('skin-color', s, { path: '/' });
            } else {
                $('#skinstyle').remove();
                $.cookie("skin-color", '', { path: '/' });
            }
        } else {
            if (s != 'default') {
                $('head').append('<link id="skinstyle" rel="stylesheet" href="css/style.' + s + '.css" type="text/css" />');
                $.cookie("skin-color", s, { path: '/' });
            }
        }
        return false;
    });
    var c = "navyblue";
    $('head').append('<link id="skinstyle" rel="stylesheet" href="css/style.' + c + '.css" type="text/css" />');

    // load selected skin color from cookie
    /*if ($.cookie('skin-color')) {
    var c = $.cookie('skin-color');
    if (c) {
    $('head').append('<link id="skinstyle" rel="stylesheet" href="css/style.' + c + '.css" type="text/css" />');
    $.cookie("skin-color", c, { path: '/' });
    }
    }*/


    // expand/collapse boxes
    if ($('.minimize').length > 0) {

        $('.minimize').click(function () {
            if (!$(this).hasClass('collapsed')) {
                $(this).addClass('collapsed');
                $(this).html("&#43;");
                $(this).parents('.widgetbox')
										      .css({ marginBottom: '20px' })
												.find('.widgetcontent')
												.hide();
            } else {
                $(this).removeClass('collapsed');
                $(this).html("&#8211;");
                $(this).parents('.widgetbox')
										      .css({ marginBottom: '0' })
												.find('.widgetcontent')
												.show();
            }
            return false;
        });

    }

    $(document).tooltip();


    empresasigned = GetEmpresa();
    usuariosigned = GetSignedUser();
    GetConstantes();

});



function CallServerGen(strurl, strdata, retorno) {
    $.ajax({
        type: "POST",
        url: strurl,
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (retorno == 0)
                LoadMenuResult(data);
            if (retorno == 2)
                SetConstantesResult(data);




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

function Valida_CIRUC(ruc) {
    if (ruc.length == 10)
        ruc = ruc + "001";
    if (ruc.length != 13)
        return false;
    var caracteres = [];
    for (var i = 0; i < ruc.length; i++) {
        caracteres[i] = parseInt(ruc.substring(i, i + 1));
    }
    var coeficientes = [];
    var divisor = 10;
    switch (caracteres[2]) {
        case 9:
            coeficientes = new Array(4, 3, 2, 7, 6, 5, 4, 3, 2);
            divisor = 11;
            break;
        case 6:
            coeficientes = new Array(3, 2, 7, 6, 5, 4, 3, 2);
            divisor = 11;
            break;
        case 0:
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
            coeficientes = new Array(2, 1, 2, 1, 2, 1, 2, 1, 2);
            divisor = 10;
            break;
    }
    var suma = 0;
    var contador = 0;
    for (var cof in coeficientes) {
        var valor = coeficientes[cof] * caracteres[cof];
        //   contador = contador+1;
        if (valor > 9)
            if (caracteres[2] <= 5) {
                valor = sumaDig(valor);

            }
        suma = suma + valor;
    }
    if ((caracteres[10] != 0) || (caracteres[11] != 0) || (caracteres[12] != 1))
    { return false; }
    var residuo = suma % divisor;
    if (residuo == 0 && parseInt(caracteres[9]) == 0)
        return true;
    residuo = divisor - residuo;
    if (residuo == parseInt(caracteres[9]))
        return true;
    return false;
}
function sumaDig(numero) {
    var strNumero = numero + "";
    var numDig = strNumero.Length;
    var digitos = [];
    for (var i = 0; i < strNumero.length; i++) {
        digitos[i] = parseInt(strNumero.substring(i, i + 1));
    }
    var suma = 0;
    for (var dig in digitos) {
        suma = suma + parseInt(digitos[dig]);
    }
    return suma;
}


function FullScreen() {
    HideMenu();
    HideHeader(); 
}


function HideMenu() {
    var obj = $("#hidemenu");

    if ($(obj).hasClass('open')) {
        $('.rightpanel, .headerinner').css({ marginLeft: '260px' });
        $('.logo, .leftpanel').css({ marginLeft: 0 });
        $(obj).removeClass('open');
        $(obj).find("i").removeClass('iconfa-arrow-right');
        $(obj).find("i").addClass('iconfa-arrow-left');
        $('body').css('background', 'url(../images/leftpanelbg.png) repeat-y 0 0');
    } else {
        $('.rightpanel, .headerinner').css({ marginLeft: 0 });
        $('.logo, .leftpanel').css({ marginLeft: '-260px' });
        $(obj).addClass('open');
        $(obj).find("i").removeClass('iconfa-arrow-left');
        $(obj).find("i").addClass('iconfa-arrow-right');
        $('body').css('background', 'transparent');
    }
    



}


function HideHeader() {
    var obj = $("#hidehead");

    if ($(obj).hasClass('open')) {
        $(".header").show();
        //$('.rightpanel, .headerinner').css({ marginLeft: '260px' });
        //$('.logo, .leftpanel').css({ marginLeft: 0 });
        $(obj).removeClass('open');
        $(obj).find("i").removeClass('iconfa-arrow-down');
        $(obj).find("i").addClass('iconfa-arrow-up');
    } else {
        $(".header").hide();
        //$('.rightpanel, .headerinner').css({ marginLeft: 0 });
        //$('.logo, .leftpanel').css({ marginLeft: '-260px' });
        $(obj).addClass('open');
        $(obj).find("i").removeClass('iconfa-arrow-up');
        $(obj).find("i").addClass('iconfa-arrow-down');
    }
    
}



function SelectOptionMenu(menuoption, url) {
    
    $(".active").removeClass('active');
    if (url.indexOf(".html") >= 0)
        url = "html/" + url;
    //OpenMenuOption($("#" + menuoption));
    $("#" + menuoption).addClass('active');
    loadIframe(url);


}


var iframeName = "icontent";
function loadIframe(url) {
    var $iframe = $('#' + iframeName);
    if ($iframe.length) {
        $iframe.attr('src', url);
    }
}

/*function GetSignedUser() {
    var obj = $.parseJSON($.cookie('usrdata'));
    $("#datosusr").html(obj["usr_nombres"] + "<small>- " + obj["MAIL"] + "</small>");

}*/



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

function GetDateStringValue(value) {
    if (value != null) {

        var dateValue = parseInt(value.replace(/\/Date\((\d+)\)\//g, "$1"));
        var date = new Date(dateValue);

        //var date = new Date(parseInt(value.substr(6)));
        //var date = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
        
        var twoDigitMonth = date.getUTCMonth() + 1 + ""; if (twoDigitMonth.length == 1) twoDigitMonth = "0" + twoDigitMonth;
        var twoDigitDate = date.getUTCDate() + ""; if (twoDigitDate.length == 1) twoDigitDate = "0" + twoDigitDate;
        var currentDate = twoDigitDate + "/" + twoDigitMonth + "/" + date.getUTCFullYear();

        return currentDate;
        //return eval(value.slice(1, -1));
    }
    return "";
}

function complete3(event) {
    var id = event.target.id;
    var value = pad($("#" + id).val(), 3)
    $("#" + id).val(value);
}
function complete9(event) {
    var id = event.target.id;
    var value = pad($("#" + id).val(), 9)
    $("#" + id).val(value);
}
function pad(str, max) {
    str = str.toString();
    return str.length < max ? pad("0" + str, max) : str;
}

function CurrencyFormatted(amount) {
    var i = parseFloat(amount);
    if (isNaN(i)) { i = 0.00; }
    var minus = '';
    if (i < 0) { minus = '-'; }
    i = Math.abs(i);
    i = parseInt((i + .005) * 100);
    i = i / 100;
    s = new String(i);
    if (s.indexOf('.') < 0) { s += '.00'; }
    if (s.indexOf('.') == (s.length - 2)) { s += '0'; }
    s = minus + s;
    return s;
}

function CurrencyFormattedAll(amount) {
    return amount.toString().replace(',', '.');

}

function DateFormatted(d) {
    var day = d.getDate();
    var month = d.getMonth() + 1;
    var year = d.getFullYear();
    if (day < 10) {
        day = "0" + day;
    }
    if (month < 10) {
        month = "0" + month;
    }
    var date = day + "/" + month + "/" + year;
    return date;
}

function CleanString(s) {

    s = s.replace(/[\r\n]+/, '');
    s = s.trim();
    return s;


}


function BalanceFormat(valor) {
    valor = Math.round(valor * 100) / 100;
    var negativo = false;
    if (valor < 0) {
        valor = valor * -1;
        negativo = true;
    }

    var num = valor.toString();
    var rx = /(\d+)(\d{3})/;
    var retorno =  String(num).replace(/^\d+/, function (w) {
        while (rx.test(w)) {
            w = w.replace(rx, '$1,$2');
        }
        return w;
    });
    if (negativo)
        retorno = "-" + retorno;
    return retorno;
}

/////////////////////////////////////////////////////////////////////
////////////////////LOCAL STORAGE////////////////////////////////////

var empresasigned;
var usuariosigned;
var cDebito;
var cCredito;
var cCliente;
var cProveedor;
var cSocio;
var cChofer;
var cAyudante;


function ClearStorage() {
    sessionStorage.clear();
    localStorage();
}


function SetStorage(obj, id, persistent) {
    var jsonText = JSON.stringify(obj);
    if (persistent)
        localStorage[id] = jsonText;
    else
        sessionStorage[id] = jsonText;
}

function GetStorage(id) {
    var jsonText = sessionStorage.getItem(id);
    if (jsonText == null)
        jsonText = localStorage.getItem(id);
    var obj = $.parseJSON(jsonText);
    return obj;
}


function SetEmpresa(obj, persistent) {

    SetStorage(obj, "empresa", persistent);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerGen("ws/Metodos.asmx/SetSignedEmpresa", jsonText, 1);
}

function GetEmpresa() {
    return GetStorage("empresa");
}

function SetOnlineCompany(obj, persistent) {
    SetStorage(obj, "companyser", persistent);

}

function SetOnlineUser(obj, persistent) {
    SetStorage(obj, "usuarioser", persistent);
}

function SetSignedUser(obj, persistent) {
    SetStorage(obj, "usuario", persistent);

    //var jsonText = JSON.stringify({ objeto: obj });
    //CallServerGen("ws/Metodos.asmx/SetSignedUsuario", jsonText, 1);
}


function GetSignedUser() {
    return GetStorage("usuario");
}


function SetConstantes() {
    var obj = {};
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerGen("ws/Metodos.asmx/GetConstantes", jsonText, 2);
}


function SetConstantesResult(data) {
    if (data != "") {
        var cons = $.parseJSON(data.d);
        SetStorage(cons["cCredito"], "cCredito", false);
        SetStorage(cons["cDebito"], "cDebito", false);
        SetStorage(cons["cCliente"], "cCliente", false);
        SetStorage(cons["cProveedor"], "cProveedor", false);
        SetStorage(cons["cSocio"], "cSocio", false);
        SetStorage(cons["cChofer"], "cChofer", false);
        SetStorage(cons["cAyudante"], "cAyudante", false);
    }
    window.location.href = "Index.aspx";

}

function GetConstantes() {
    cCredito = parseInt(GetStorage("cCredito"));
    cDebito = parseInt(GetStorage("cDebito"));
    cCliente = parseInt(GetStorage("cCliente"));
    cProveedor = parseInt(GetStorage("cProveedor"));
    cSocio = parseInt(GetStorage("cSocio"));
    cChofer = parseInt(GetStorage("cChofer"));
    cAyudante = parseInt(GetStorage("cAyudante"));

}


function SetAuditoria(obj) {

    obj["crea_usr"] = usuariosigned["usr_id"];
    obj["crea_fecha"] = new Date($.now());
    obj["mod_usr"] = usuariosigned["usr_id"];
    obj["mod_fecha"] = new Date($.now());
    return obj;

}

