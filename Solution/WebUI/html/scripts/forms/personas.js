var Personas = function () {

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
            BootBoxDialog(obj[0], obj[1], "large","Persona");
            GetListadoIngresos();
        }

        if (retorno == "Ingr")
        {
            $("#tdingresos>tbody").html(data.d);
        }

        if (retorno == "EditIngr") {
            var obj = $.parseJSON(data.d);
            BootBoxDialog(obj[0], obj[1], "medium","Ingreso");            
        }
        if (retorno == "Save") {
            GetListadoBody();
        }
        if (retorno == "SaveIngr")
        {
            GetListadoIngresos();
        }
        if (retorno == "Remove") {
            if (data.d != "OK")
                BootBoxAlert(data.d,"Persona");
            else
                GetListadoBody();
        }
        if (retorno == "RemoveIngr") {
            if (data.d != "OK")
                BootBoxAlert(data.d, "Ingreso");
            else
                GetListadoIngresos();
        }

    }
}


function GetFilters() {

    CallServerMethods(webservice + "GetFiltrosPersona", JsonObjString(JsonObj()), "Filter");
}

function SetFilters(html) {
    $(".booking-search").html(html);
    SetPlugins();

    $("#txtpersona_f").on("change", GetListadoBody);
    $("#cmbestado_f").on("change", GetListadoBody);

    GetListadoBody();

}

function SetListadoHead() {
    $("#tdlistado>thead").html("<tr><th ></th><th>Código</th><th>CI/RUC</th><th>Nombres</th><th>Activo</th></tr>");
}


function GetListadoBody() {

    $("#tdlistado>tbody").html("");
    var obj = JsonObj();
    obj["per_empresa"] = GetOnlineCompany();
    obj["per_id"] = $("#txtpersona_f").val();
    if ($("#cmbestado_f").val() != "" && $("#cmbestado_f").val() != null && $("#cmbestado_f").val()!=undefined)
        obj["per_estado"] = $("#cmbestado_f").val();
    CallServerMethods(webservice + "GetPersonas", JsonObjString(obj), "List");

}


function AddNew() {
    CallServerMethods(webservice + "EditPersona", JsonObjString(JsonObj()), "Edit");
}

function Edit(data) {
    var obj = JsonObj();
    obj["per_empresa"] = GetOnlineCompany();
    obj["per_codigo"] = data["codigo"];
    CallServerMethods(webservice + "EditPersona", JsonObjString(obj), "Edit");
}

///////////////////*Listado Ingresos*///////////////////////////

function EditIngreso(data)
{
    var obj = JsonObj();
    obj["ing_empresa"] = GetOnlineCompany();
    obj["ing_persona"] = data["persona"];
    obj["ing_ingreso"] = data["ingreso"];
    CallServerMethods(webservice + "EditIngreso", JsonObjString(obj), "EditIngr");
}

function GetListadoIngresos()
{
    $("#tdingresos>tbody").html("");
    var obj = JsonObj();
    obj["ing_empresa"] = GetOnlineCompany();
    obj["ing_persona"] = $("#editdata").data("codigo");
    CallServerMethods(webservice + "GetIngresos", JsonObjString(obj), "Ingr");
}

function AddNewIng() {
    var obj = JsonObj();
    obj["ing_empresa"] = GetOnlineCompany();
    obj["ing_persona"] = $("#editdata").data("codigo");
    CallServerMethods(webservice + "EditIngreso", JsonObjString(obj), "EditIngr");
}

function RemoveIngreso(data) {

    BootBoxConfirm("¿Está seguro que desea eliminar el ingreso?", data,"Ingreso");
}

///////////////////*Fin listado ingreso*///////////////////
function BootBoxAccept(id) {
    if (id == "Persona") {
        if (ValidateForm("formper")) {
            var obj = JsonObj();
            obj["per_empresa"] = GetOnlineCompany();
            obj["per_codigo"] = $("#editdata").data("codigo");
            obj["per_id"] = $("#txtid_per").val();
            obj["per_tipoid"] = $("#cmbtipoid_per").val();
            obj["per_ciruc"] = $("#txtciruc_per").val();
            obj["per_apellidos"] = $("#txtapellidos_per").val();
            obj["per_nombres"] = $("#txtnombres_per").val();
            obj["per_estado"] = GetCheckValue($("#chkestado_per"));
            CallServerMethods(webservice + "SavePersona", JsonObjString(obj), "Save");
        }
        else
            return false;
    }
    if (id="Ingreso")
    {
        if (ValidateForm("forming")) {
            var obj = JsonObj();
            obj["ing_empresa"] = GetOnlineCompany();
            obj["ing_persona"] = $("#editdataing").data("persona");
            obj["ing_ingreso"] = $("#txtingreso_ing").val();
            obj["ing_salida"] = $("#txtsalida_ing").val();
            obj["ing_observacion"] = $("#txtobservacion_ing").val();
            CallServerMethods(webservice + "SaveIngreso", JsonObjString(obj), "SaveIngr");
        }
        else
            return false;
    }

}


function Remove(data) {

    BootBoxConfirm("¿Está seguro que desea eliminar la persona?", data, "Persona");
}


function BootBoxSi(data,id) {
    var obj = JsonObj();
    if (id == "Persona") {
        obj["per_empresa"] = GetOnlineCompany();
        obj["per_codigo"] = data["codigo"];

        CallServerMethods(webservice + "RemovePersona", JsonObjString(obj), "Remove");
    }
    if (id == "Ingreso") {
        obj["ing_empresa"] = GetOnlineCompany();
        obj["ing_persona"] = data["persona"];
        obj["ing_ingreso"] = data["ingreso"];

        CallServerMethods(webservice + "RemoveIngreso", JsonObjString(obj), "RemoveIngr");
    }
}



