/////// BASIC FUNCTIONS /////////////////////////////////////

function GetBooleanCheckValue(obj) {
    var valor = GetCheckValue(obj)
    if (valor == 1)
        return true;
    else
        return false;
    //return null;
}

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

function GetCurrentDateValue() {
    
        var date = new Date();
        return date.toLocaleString();
        //return eval(value.slice(1, -1));    
}

function GetDateStringValue(value) {
    if (value != null) {
        var date = new Date(parseInt(value.substr(6)));
        var twoDigitMonth = date.getUTCMonth() + 1 + ""; if (twoDigitMonth.length == 1) twoDigitMonth = "0" + twoDigitMonth;
        var twoDigitDate = date.getUTCDate() + ""; if (twoDigitDate.length == 1) twoDigitDate = "0" + twoDigitDate;
        var currentDate = twoDigitDate + "/" + twoDigitMonth + "/" + date.getUTCFullYear();

        return currentDate;
        //return eval(value.slice(1, -1));
    }
    return "";
}


function GetPercent(total, valor) {
    var percent = (valor * 100) / total;
    if ($.isNumeric(percent))
        return Math.round(percent * 100) / 100
    else {
        return 0;
    }
}

function GetFloat(value) {
    value = value.toString().replace(",", ".");
    if ($.isNumeric(value))
        return parseFloat(value);
    return 0;
}



function Average(array) {


    var avg = 0;
    var total = 0;    
    for (var i = 0; i < array.length; i++) {
        total += array[i];
    }
    avg = total / array.length;
    return Math.round(avg * 100) / 100;


}

function DecimalFormat(value)
{
    return parseFloat(value).toFixed(2);
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

///////////////////////////STORAGE FUNCTIONS ////////////////////////////////////

function ClearStorage() {
    sessionStorage.clear();
    localStorage.clear();
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

////////////////////////////////////////////////////////////////////////////


//////////////////////////////////////////////////////////////////////
//////OBTIENE LOS VALORES DEL QUERY STRING///////////////////////////

function GetQueryStringParams(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}

//////////////////DOCUMENTOS UPLOAD////////////////////////////

function ajaxFileUpload(inputfile, descripcion, empresa, proyecto, nivel, nivelvalor, periodo, resultado, actividad, variable, indicador, crea_usr) {

    var files = $("#" + inputfile).get(0).files;    
    var data = new FormData();
    for (i = 0; i < files.length; i++) {
        data.append("file" + i, files[i]);
    }
    data.append("empresa", empresa);
    data.append("proyecto", proyecto);
    data.append("nivel", nivel);
    data.append("nivelvalor", nivelvalor);
    data.append("periodo", periodo);
    data.append("resultado", resultado);
    data.append("actividad", actividad);
    data.append("variable", actividad);
    data.append("indicador", indicador);    
    data.append("descripcion", descripcion);

    $.ajax({
        type: "POST",
        url: 'AjaxFileUploader.ashx',
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            if (result) {
                try {                    
                    UploadResult(result);
                }
                catch (err) {                    
                }
                //alert('Archivos subidos correctamente');
                //$("#"+inputfile).val('');
            }
        }
    });

}


function SetMaxLenth() {
    $('textarea[maxlength]').keyup(function () {
        //get the limit from maxlength attribute  
        var limit = parseInt($(this).attr('maxlength'));
        //get the current text inside the textarea  
        var text = $(this).val();
        //count the number of characters in the text  
        var chars = text.length;

        //check if there are more characters then allowed  
        if (chars > limit) {
            //and if there are use substr to get the text before the limit  
            var new_text = text.substr(0, limit);

            //and change the current text with the new text  
            $(this).val(new_text);
        }
    });
}


/////////////////////////SET PLUGINS//////////////////////////////

function SetPlugins()
{
    $(".make-switch").bootstrapSwitch();
    $('.date-picker').datepicker({
        rtl: false,
        format: 'dd/mm/yyyy',
        orientation: "left",
        autoclose: true,        
    });
}
//////////////////////FORM VALIDATION //////////////////////////


function ValidateForm(id)
{
    var retorno = true;
    $("#" + id).find('[data-required="true"]').each(function () {

        $(this.parentNode.parentNode).removeClass('has-error');            //form group        
        $(this.parentNode).children("i").remove();
        $(this.parentNode).removeClass('input-icon right');
        //$(this.parentNode). // div
        //$(this.parentNode).children("i").remove();
        //$(this.parentNode).children(".help-block").remove();
        if ($(this).val() == "") {
            retorno = false;            
            //$(this.parentNode).append("<span class='help-block'>! Obligatorio</span>");
            //$(this.parentNode).addClass('has-error')

            $(this.parentNode).addClass("input-icon right");
            $(this.parentNode).prepend("<i class='fa fa-warning tooltips' data-container='body' data-original-title='Valor obligatorio'></i>");                        
            $(this.parentNode.parentNode).addClass('has-error');

        }

    });
    $('.tooltips').tooltip();
    return retorno;
}



///////////////////////PAGE PARTS///////////////////////////////

function LoadHeader() {
    $.get("header.html", function (data) {
        $(".page-header").append(data);
        $(".username").html(GetOnlineUserName() + "-" + GetOnlineCompanyName());
    });

}

function LoadFooter()
{
    $.get("footer.html", function (data) {
        $(".page-footer").append(data);
    });

}

function SelectOptionMenu(menuoption, url) {
    $(".active").removeClass('active');
    if (url.indexOf(".aspx") >= 0)
        url = "../" + url;
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