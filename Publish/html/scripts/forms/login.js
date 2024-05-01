//CODIGO DE LA VENTANA DE LOGIN

var Login = function () {

    var handleLogin = function () {
        $('.login-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            rules: {
                username: {
                    required: true
                },
                password: {
                    required: true
                },
                remember: {
                    required: false
                }
            },

            messages: {
                username: {
                    required: "Nombre de usuario requerido."
                },
                password: {
                    required: "Contraseña requerida."
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit   
                $('.alert-danger', $('.login-form')).show();
            },

            highlight: function (element) { // hightlight error inputs
                $(element)
	                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement: function (error, element) {
                error.insertAfter(element.closest('.input-icon'));
            },

            submitHandler: function (form) {

                CallLogin();
                //form.submit();
            }
        });

        $('.login-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.login-form').validate().form()) {
                    CallLogin();
                    //  $('.login-form').submit();
                }
                return false;
            }
        });
    }

    var handleForgetPassword = function () {
        $('.forget-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",
            rules: {
                email: {
                    required: true,
                    email: true
                }
            },

            messages: {
                email: {
                    required: "Email is required."
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit   

            },

            highlight: function (element) { // hightlight error inputs
                $(element)
	                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement: function (error, element) {
                error.insertAfter(element.closest('.input-icon'));
            },

            submitHandler: function (form) {
                form.submit();
            }
        });

        $('.forget-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.forget-form').validate().form()) {
                    $('.forget-form').submit();
                }
                return false;
            }
        });

        jQuery('#forget-password').click(function () {
            jQuery('.login-form').hide();
            jQuery('.forget-form').show();
        });

        jQuery('#back-btn').click(function () {
            jQuery('.login-form').show();
            jQuery('.forget-form').hide();
        });

    }

      return {
        //main function to initiate the module
        init: function () {

            handleLogin();
            handleForgetPassword();
        }

    };

}();

function CallLogin() {

    var obj = {};
    obj["usr_id"] = $("[name='username']").val();
    obj["usr_password"] = $("[name='password']").val();
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerMethods(webservice + "Login", jsonText, 0);
}


function ServerResult(data, retorno) {
    if (data != "") {
        if (retorno == 0) {
            var obj = $.parseJSON(data.d);
            //if (data.d == "OK") {
            if (obj[0] == "OK") {
                var usr = obj[1];
                SetOnlineUser($("[name='username']").val(), GetBooleanCheckValue($("[name='remember']")));

                SetSignedUser(usr, GetBooleanCheckValue($("[name='remember']")));//PARA LA ANTERIOR VERSION

                //SetOnlineCompany(1, GetBooleanCheckValue($("[name='remember']")));
                SetOnlineUserRol(usr["usr_perfil"], GetBooleanCheckValue($("[name='remember']")));
                SetOnlineUserName(usr["usr_nombres"], GetBooleanCheckValue($("[name='remember']")));
                SelectEmpresa($("[name='username']").val());
                //window.location.href = indexpage;
            }
            else {
                Metronic.alert({
                    container: "#bootstrap_alerts_demo",
                    place: "append", // append or prepent in container 
                    type: "danger",  // alert's type
                    message: obj[0],  // alert's message
                    close: true, // make alert closable
                    reset: true, // close all previouse alerts first
                    focus: true, // auto scroll to the alert after shown
                    closeInSeconds: 5, // auto close after defined seconds
                    icon: "warning" // put icon before the message
                });

            }

        }
    }
}

function EndSelectEmpresa(codigo, nombre) {
    SetOnlineCompany(codigo, GetBooleanCheckValue($("[name='remember']")));
    SetOnlineCompanyName(nombre, GetBooleanCheckValue($("[name='remember']")));

    /*CONTRL PARA LA ANTERIOR VERSION*/
    var obj = {};
    obj["emp_codigo"] = codigo;
    obj["emp_nombre"] = nombre;
    SetEmpresa(obj, GetBooleanCheckValue($("[name='remember']")));
    SetConstantes();
    /******************************/
    
}
