
var positionClass = "toast-top-right";
var showDuration = "1000";
var hideDuration = "1000";
var timeOut = "5000";
var extendedTimeOut = "1000";
var showEasing = "swing";
var hideEasing = "linear";
var showMethod = "fadeIn";
var hideMethod = "fadeOut";

var toastCount = 0;


function ShowSuccess(titulo, mensaje) {
    ShowToast(titulo, mensaje, "success", positionClass, "", "", "", "", "", "", "", "", true, false);
}

function ShowError(titulo, mensaje) {
    ShowToast(titulo, mensaje, "error", positionClass, "", "", "", "", "", "", "", "", true, false);
}



function ShowToast(titulo, mensaje, tipo, posicion, show, hide, timeout, extimeout, showeasing, hideeasing, showmethod, hidemethod, closebutton, debug) {

    var shortCutFunction = tipo;
    var msg = mensaje;
    var title = titulo || '';
    var showDuration = show;
    var hideDuration = hide;
    var timeOut = timeout;
    var extendedTimeOut = extimeout;
    var showEasing = showeasing;
    var hideEasing = hideeasing;
    var showMethod = showmethod;
    var hideMethod = hidemethod;
    var toastIndex = toastCount++;

    toastr.options = {
        closeButton: closebutton,
        debug:debug,
        positionClass: tipo || 'toast-top-right',
        onclick: null
    };


    if (showDuration!="") {
        toastr.options.showDuration = showDuration;
    }

    if (hideDuration!="") {
        toastr.options.hideDuration = hideDuration;
    }

    if (timeOut != "") {
        toastr.options.timeOut = timeOut;
    }

    if (extendedTimeOut != "") {
        toastr.options.extendedTimeOut = extendedTimeOut;
    }

    if (showEasing != "") {
        toastr.options.showEasing = showEasing;
    }

    if (hideEasing != "") {
        toastr.options.hideEasing = hideEasing;
    }

    if (showMethod != "") {
        toastr.options.showMethod = showMethod;
    }

    if (hideMethod != "") {
        toastr.options.hideMethod = hideMethod;
    }

//    if (!msg) {
//        msg = getMessage();
//    }

   // $("#toastrOptions").text("Command: toastr[" + shortCutFunction + "](\"" + msg + (title ? "\", \"" + title : '') + "\")\n\ntoastr.options = " + JSON.stringify(toastr.options, null, 2));

    var $toast = toastr[shortCutFunction](mensaje, titulo); // Wire up an event handler to a button in the toast, if it exists
    $toastlast = $toast;
    if ($toast.find('#okBtn').length) {
        $toast.delegate('#okBtn', 'click', function () {
            alert('you clicked me. i was toast #' + toastIndex + '. goodbye!');
            $toast.remove();
        });
    }
    if ($toast.find('#surpriseBtn').length) {
        $toast.delegate('#surpriseBtn', 'click', function () {
            alert('Surprise! you clicked me. i was toast #' + toastIndex + '. You could perform an action here.');
        });
    }

//    $('#clearlasttoast').click(function () {
//        toastr.clear($toastlast);
//    });

}




//$('#showtoast').click(function () {


//   

//    

//    if ($('#addBehaviorOnToastClick').prop('checked')) {
//        toastr.options.onclick = function () {
//            alert('You can perform some custom action after a toast goes away');
//        };
//    }

//  

//    
//});
//$('#cleartoasts').click(function () {
//    toastr.clear();
//});