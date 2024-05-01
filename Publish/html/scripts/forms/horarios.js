var Horarios = function () {

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
            BootBoxDialog(obj[0], obj[1], "large");
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

    CallServerMethods(webservice + "GetFiltrosHorario", JsonObjString(JsonObj()), "Filter");
}

function SetFilters(html) {
    $(".booking-search").html(html);
    SetPlugins();

    $("#txthorario_f").on("change", GetListadoBody);
    $("#cmbestado_f").on("change", GetListadoBody);

    GetListadoBody();

}

function SetListadoHead() {
    $("#tdlistado>thead").html("<tr><th ></th><th>Id</th><th>Horario</th><th>Descripción</th><th>Activo</th></tr>");
}


function GetListadoBody() {

    $("#tdlistado>tbody").html("");
    var obj = JsonObj();
    obj["hor_empresa"] = GetOnlineCompany();
    obj["hor_id"] = $("#txthorario_f").val();
    if ($("#cmbestado_f").val() != "" && $("#cmbestado_f").val() != null && $("#cmbestado_f").val() != undefined)
        obj["hor_estado"] = $("#cmbestado_f").val();
    CallServerMethods(webservice + "GetHorarios", JsonObjString(obj), "List");

}


function AddNew() {
    CallServerMethods(webservice + "EditHorario", JsonObjString(JsonObj()), "Edit");
}

function Edit(data) {
    var obj = JsonObj();
    obj["hor_empresa"] = GetOnlineCompany();
    obj["hor_codigo"] = data["codigo"];
    CallServerMethods(webservice + "EditHorario", JsonObjString(obj), "Edit");
}




function BootBoxAccept() {

    if (id == "Horario") {
        if (ValidateForm("formhor")) {
            var obj = JsonObj();
            obj["hor_empresa"] = GetOnlineCompany();
            obj["hor_codigo"] = $("#editdata").data("codigo");
            obj["hor_id"] = $("#txtid_hor").val();
            obj["hor_nombre"] = $("#txtnombre_hor").val();
            obj["hor_descripcion"] = $("#txtdescripcion_hor").val();
            obj["hor_estado"] = GetCheckValue($("#chkestado_hor"));
            CallServerMethods(webservice + "SaveHorario", JsonObjString(obj), "Save");
        }
        else
            return false;
    }
    //if (id = "HorarioDetalle") {
    //    if (ValidateForm("forming")) {
    //        var obj = JsonObj();
    //        obj["ing_empresa"] = GetOnlineCompany();
    //        obj["ing_persona"] = $("#editdataing").data("persona");
    //        obj["ing_ingreso"] = $("#txtingreso_ing").val();
    //        obj["ing_salida"] = $("#txtsalida_ing").val();
    //        obj["ing_observacion"] = $("#txtobservacion_ing").val();
    //        CallServerMethods(webservice + "SaveIngreso", JsonObjString(obj), "SaveIngr");
    //    }
    //    else
    //        return false;
    //}


}


function Remove(data) {

    BootBoxConfirm("¿Está seguro que desea eliminar el horario?", data);
}


function BootBoxSi(data) {
    var obj = JsonObj();
    obj["per_id"] = data["id"];

    CallServerMethods(webservice + "RemovePerfil", JsonObjString(obj), "Remove");
}



