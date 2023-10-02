var Perfiles = function () {

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

    CallServerMethods(webservice + "GetFiltrosPerfil", JsonObjString(JsonObj()), "Filter");
}

function SetFilters(html) {
    $(".booking-search").html(html);
    SetPlugins();

    $("#txtusuario_f").on("change", GetListadoBody);

    GetListadoBody();

}

function SetListadoHead() {
    $("#tdlistado>thead").html("<tr><th ></th><th>Id</th><th>Descripción</th><th>Activo</th></tr>");
}


function GetListadoBody() {

    $("#tdlistado>tbody").html("");
    var obj = JsonObj();
    obj["per_id"] = $("#txtperfil_f").val();
    CallServerMethods(webservice + "GetPerfiles", JsonObjString(obj), "List");

}


function AddNew() {
    CallServerMethods(webservice + "EditPerfil", JsonObjString(JsonObj()), "Edit");
}

function Edit(data) {
    var obj = JsonObj();
    obj["per_id"] = data["id"];
    CallServerMethods(webservice + "EditPerfil", JsonObjString(obj), "Edit");
}




function BootBoxAccept() {
    if (ValidateForm("formper")) {
        var obj = JsonObj();
        obj["per_id"] = $("#txtid_per").val();
        obj["per_descripcion"] = $("#txtdescripcion_per").val();
        obj["per_estado"] = GetCheckValue($("#chkestado_per"));
        CallServerMethods(webservice + "SavePerfil", JsonObjString(obj), "Save");
    }
    else
        return false;

}


function Remove(data) {

    BootBoxConfirm("¿Está seguro que desea eliminar el perfil?", data);
}


function BootBoxSi(data) {
    var obj = JsonObj();
    obj["per_id"] = data["id"];

    CallServerMethods(webservice + "RemovePerfil", JsonObjString(obj), "Remove");
}



