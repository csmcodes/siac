//SCRIPT DE FUNCIONES GENERALES

function SetPedido(codigo, referencia, descripcion, cantidad, precio) {

    var array = GetStorage("pedido");
    if (array == null || array == undefined)
        array = new Array();

    var existe = false;
    for (var i = 0; i < array.length; i++) {

        var obj = array[i];
        if (obj["codigo"] == codigo) {
            obj["cantidad"] = obj["cantidad"] + cantidad;
            existe = true;
            break;
        }

    }
    if (!existe) {
        var obj = {};
        obj["codigo"] = codigo;
        obj["referencia"] = referencia;
        obj["descripcion"] = descripcion;
        obj["cantidad"] = cantidad;
        obj["precio"] = precio;
        array[array.length] = obj;
    }
    SetStorage(array, "pedido", false);
}

function GetPedidoPreview() {

    var array = GetStorage("pedido");
    if (array != null) {
        var detalle = "";
        for (var i = 0; i < array.length; i++) {
            var obj = array[i];
            detalle += '<li><a href="javascript:;"><span class="time">' + obj["cantidad"] + '</span><span class="details"><span class="label label-sm label-icon label-success"><i class="fa fa-plus"></i></span>' + obj["descripcion"] + '</span></a></li>';
        }

        $("#pedidocant").html(array.length);
        $("#pedidocant1").html('<span class="bold">' + array.length + ' items</span> seleccionados');
        $("#pedidodet").html(detalle);
    }
}





