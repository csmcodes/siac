var userstorageid = "usuarioser";
var usernamestorageid = "usuarionameser";
var companystorageid = "companyser";
var companynamestorageid = "companynameser";
var rolstorageid = "rolser";
var customerstorageid = "customerser"
var customerstoragename = "customernameser"

var loginpage = "login.html";
var signuppage = "signup.html";
var indexpage = "Index.html";// "/Index.aspx";
var forgotpage = "olvido.html";
var resultadospage = "resultados.html";
var onlineuser;

IsOnline();

function IsOnline() {
    var pathname = window.location.pathname;
    if (pathname.indexOf(loginpage) < 0 && pathname.indexOf(signuppage) < 0 && pathname.indexOf(forgotpage) < 0) {
        var usr = GetOnlineUser();
        $(".username").html(GetOnlineUserName()+ "-"+GetOnlineCompanyName());
        if (usr == null)
            window.location.href = loginpage;
    }

}

function EndContract() {
    ClearStorage();
    window.location.href = indexpage;
}


///////////////////////////SIGNED OBJECTS/////////////////////////////////////////

function SetOnlineCompanyName(obj, persistent) {
    SetStorage(obj, companynamestorageid, persistent);
}


function GetOnlineCompanyName() {
    return GetStorage(companynamestorageid);
}

function SetOnlineCompany(obj, persistent) {
    SetStorage(obj, companystorageid, persistent);
    
}


function GetOnlineCompany() {
    return GetStorage(companystorageid);
}


function SetOnlineUser(obj, persistent) {
    SetStorage(obj, userstorageid, persistent);    
}


function SetOnlineUserRol(obj, persistent) {
    SetStorage(obj, rolstorageid, persistent);
}

function SetOnlineUserName(obj, persistent) {
    SetStorage(obj, usernamestorageid, persistent);
}

function GetOnlineUser() {
    return GetStorage(userstorageid);
}

function GetOnlineUserRol() {
    return GetStorage(rolstorageid);
}

function GetOnlineUserName() {
    return GetStorage(usernamestorageid);
}



function SetOnlineCustomer(obj, persistent) {
    SetStorage(obj, customerstorageid, persistent);
}


function SetOnlineCustomerName(obj, persistent) {
    SetStorage(obj, customerstoragename, persistent);
}


function GetOnlineCustomer() {
    return GetStorage(customerstorageid);
}

function GetOnlineCustomerName() {
    return GetStorage(customerstoragename);
}



function SetAuditoria(obj) {
    obj["empresa"] = GetOnlineCompany();
    obj["crea_usr"] = GetOnlineUser();
    obj["crea_fecha"] = new Date($.now());
    obj["mod_usr"] = GetOnlineUser();
    obj["mod_fecha"] = new Date($.now());
    return obj;

}

function JsonObj()
{
    var obj = {};
    SetAuditoria(obj);
    return obj;
}

function JsonObjString(obj)
{
    return JSON.stringify({ objeto: obj });
}



/////////////////////////////////////////////////////////////////////
////////////////////LOCAL STORAGE VERSION 1.0///////////////////////

var empresasigned;
var usuariosigned;
var cDebito;
var cCredito;
var cCliente;
var cProveedor;
var cSocio;
var cChofer;
var cAyudante;


function SetEmpresa(obj, persistent) {

    SetStorage(obj, "empresa", persistent);
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerGen("/ws/Metodos.asmx/SetSignedEmpresa", jsonText, 1);
}

function GetEmpresa() {
    return GetStorage("empresa");
}



function SetSignedUser(obj, persistent) {
    SetStorage(obj, "usuario", persistent);  
}


function GetSignedUser() {
    return GetStorage("usuario");
}


function SetConstantes() {
    var obj = {};
    var jsonText = JSON.stringify({ objeto: obj });
    CallServerGen("/ws/Metodos.asmx/GetConstantes", jsonText, 2);
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
    window.location.href = indexpage;

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


function UpdateOnlineUser() {
    var usr = GetOnlineUser();
    if (usr == "" || usr == undefined) {
        usr = GetSignedUser();
        SetOnlineUser(usr, false);
    }
}

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