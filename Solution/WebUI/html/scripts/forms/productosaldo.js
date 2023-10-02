var ProductoSaldo = function () {

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
            $("#cmbagencia_pro").on("change", { id: "cmbgrupo_pro" }, GetGrupos);

            $("#cmbdepartamento_pro").on("change", GetPersonas);

            $("#cmbagencia_pro").trigger('change');

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
        if (retorno == "GetGrupos") {
            var obj = $.parseJSON(data.d);

            $("#" + obj[0]).replaceWith(obj[1]);
            $("#cmbgrupo_pro").on("change", GetPersonas);
            $("#cmbgrupo_pro").trigger('change');

        }
        if (retorno == "GetPersonas") {
            var obj = $.parseJSON(data.d);
            $("#" + obj[0]).replaceWith(obj[1]);
        }


        if (retorno == "EditDetalle") {
            var obj = $.parseJSON(data.d);
            BootBoxDialog(obj[0], obj[1], "large");
            //DETALLE DE PROGRAMACION

        }

    }
}


function GetFilters() {

    CallServerMethods(webservice + "GetFiltrosProductoSaldo", JsonObjString(JsonObj()), "Filter");
}

function SetFilters(html) {
    $(".booking-search").html(html);
    SetPlugins();
    //$("#cmbagencia_f").on("change", { id: "cmbgrupo_f" }, GetGrupos);
    //$("#txtusuario_f").on("change", GetListadoBody);

    //GetListadoBody();

}

function SetListadoHead() {
    $("#tdlistado>thead").html("<tr><th></th><th>Id</th><th>Producto</th><th>Unidad</th><th>Cantidad</th><th>Cant. Transito</th><th>Cant. Total</th><th>Costo Unit.</th><th>Total</th></tr>");
}


function GetListadoBody() {

    $("#tdlistado>tbody").html("");
    var obj = JsonObj();
    obj["pro_empresa"] = GetOnlineCompany();
    obj["pro_agencia"] = $("#cmbagencia_f").val();
    obj["pro_grupo"] = $("#cmbgrupo_f").val();
    obj["pro_departamento"] = $("#cmbdepartamento_f").val();

    obj["crea_fecha"] = $("#txtdesde_f").val();
    obj["mod_fecha"] = $("#txthasta_f").val();

    obj["per_nombres"] = $("#txtnombres_f").val();

    CallServerMethods(webservice + "GetProgramaciones", JsonObjString(obj), "List");

}


function AddNew() {
    CallServerMethods(webservice + "EditProgramacion", JsonObjString(JsonObj()), "Edit");
}

function Edit(data) {
    var obj = JsonObj();
    obj["pro_empresa"] = GetOnlineCompany();
    obj["pro_codigo"] = data["codigo"];
    CallServerMethods(webservice + "EditProgramacion", JsonObjString(obj), "Edit");
}




function BootBoxAccept() {
    if (ValidateForm("formpro")) {
        var obj = JsonObj();
        obj["pro_empresa"] = GetOnlineCompany();
        obj["pro_codigo"] = $("#editdata").data("codigo");
        obj["pro_agencia"] = $("#cmbagencia_pro").val();
        obj["pro_grupo"] = $("#cmbgrupo_pro").val();
        obj["pro_departamento"] = $("#cmbdepartamento_pro").val();
        obj["pro_persona"] = $("#txtpersona_pro").val();
        obj["pro_descripcion"] = $("#txtdescripcion_pro").val();
        obj["pro_estado"] = GetCheckValue($("#chkestado_pro"));
        CallServerMethods(webservice + "SaveProgramacion", JsonObjString(obj), "Save");
    }
    else
        return false;

}


function Remove(data) {

    BootBoxConfirm("¿Está seguro que desea eliminar la programación?", data);
}


function BootBoxSi(data) {
    var obj = JsonObj();
    obj["pro_empresa"] = GetOnlineCompany();
    obj["pro_codigo"] = data["codigo"];

    CallServerMethods(webservice + "RemoveProgramacion", JsonObjString(obj), "Remove");
}



function GetGrupos(event) {
    var obj = JsonObj();
    obj["gru_empresa"] = GetOnlineCompany();
    obj["gru_agencia"] = $(this).val();
    obj["gru_nombre"] = event.data.id;

    obj["gru_codigo"] = $("#" + event.data.id).data("valor");

    CallServerMethods(webservice + "GetGruposAgencia", JsonObjString(obj), "GetGrupos");
}


function GetPersonas(event) {
    var obj = JsonObj();
    obj["per_empresa"] = GetOnlineCompany();
    obj["per_agencia"] = $("#cmbagencia_pro").val();
    obj["per_grupo"] = $("#cmbgrupo_pro").val();
    obj["per_departamento"] = $("#cmbdepartamento_pro").val();
    obj["per_codigo"] = $("#cmbpersona_pro").data("valor");
    obj["per_nombres"] = "cmbpersona_pro";
    CallServerMethods(webservice + "GetPersonasList", JsonObjString(obj), "GetPersonas");
}


function Detalle(data) {
    window.location.href = "programaciondetalle.html?codigo=" + data["codigo"];

    //var obj = JsonObj();
    //obj["pro_empresa"] = GetOnlineCompany();
    //obj["pro_codigo"] = data["codigo"];
    //CallServerMethods(webservice + "GetDetalleProgramacion", JsonObjString(obj), "EditDetalle");
}


