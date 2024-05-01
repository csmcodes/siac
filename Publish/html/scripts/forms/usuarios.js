var Usuarios = function () {

    return {

        //main function to initiate the module
        init: function () {

            GetFilters();
            SetListadoHead();            

        }

    };

}();


function ServerResult(data, retorno) {
    if (data != "") {
        if (retorno == "Filter") {
            SetFilters(data.d)

        }
        if (retorno == "List") {
            $("#tdlistado>tbody").html(data.d);
        }
        if (retorno == "Edit") {
            var obj = $.parseJSON(data.d);
            BootBoxDialog(obj[0], obj[1], "medium");
        }
        if (retorno == "Save") {
            GetListadoBody();
        }
        if (retorno == "Remove") {
            if (data.d != "OK")
                BootBoxAlert(data.d);
            else
                GetListadoBody();
        }

    }
}


function GetFilters() {
    
    CallServerMethods(webservice + "GetFiltrosUsuario", JsonObjString(JsonObj()), "Filter");
}

function SetFilters(html) {
    $(".booking-search").html(html);
    SetPlugins();

    $("#txtusuario_f").on("change", GetListadoBody);

    GetListadoBody();

}

function SetListadoHead() {
    $("#tdlistado>thead").html("<tr><th ></th><th>Id</th><th>Nombres</th><th>Mail</th><th>Perfil</th><th>Activo</th></tr>");
}


function GetListadoBody() {

    $("#tdlistado>tbody").html("");
    var obj = JsonObj();    
    obj["usr_id"] = $("#txtusuario_f").val();    
    CallServerMethods(webservice + "GetUsuarios", JsonObjString(obj), "List");

}


function AddNew() {
    CallServerMethods(webservice + "EditUsuario", JsonObjString(JsonObj()), "Edit");
}

function Edit(data)
{
    var obj = JsonObj();
    obj["usr_id"] = data["id"];        
    CallServerMethods(webservice + "EditUsuario", JsonObjString(obj), "Edit");
}




function BootBoxAccept()
{
    if (ValidateForm("formusr")) {
        var obj = JsonObj();
        obj["uxe_empresa"] = GetOnlineCompany();
        obj["uxe_usuario"] = $("#txtid_usr").val();

        obj["usr_id"] = $("#txtid_usr").val();
        obj["usr_password"] = $("#txtpassword_usr").val();
        obj["usr_nombres"] = $("#txtnombres_usr").val();
        obj["usr_mail"] = $("#txtemail_usr").val();
        obj["usr_perfil"] = $("#cmbperfil_usr").val();
        obj["usr_estado"] = GetCheckValue($("#chkestado_usr"));

        CallServerMethods(webservice + "SaveUsuario", JsonObjString(obj), "Save");
    }
    else
        return false;
 
}


function Remove(data) {

    BootBoxConfirm("¿Está seguro que desea eliminar el usuario?",data);
}


function BootBoxSi(data)
{
    var obj = JsonObj();
    obj["uxe_empresa"] = GetOnlineCompany();
    obj["uxe_usuario"] = data["id"];
    obj["usr_id"] = data["id"];

    CallServerMethods(webservice + "RemoveUsuario", JsonObjString(obj), "Remove");
}



