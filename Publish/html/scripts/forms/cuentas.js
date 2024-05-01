var Cuentas = function () {

    return {

        //main function to initiate the module
        init: function () {



            UpdateOnlineUser();


            GetCuentas();

        }

    };

}();

jQuery(document).ready(function () {
    Cuentas.init();
});


function ServerResult(data, retorno) {
    if (data != "") {
        if (retorno == "Tree") {
            $(".contenido").html(data.d);
            SetTree();

        }
        /*if (retorno == "Edit") {
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
        if (retorno == "GetPuntos") {
            $("#cmbpemision_f").html(data.d);
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
        if (retorno == "GetForm")
            CallFormularioResult(data);
        if (retorno == "Electronic")
            alert(data.d);*/
    }
}


function GetCuentas() {

    var obj = JsonObj();
    CallServerMethods(webservice + "GetCuentasTree", JsonObjString(obj), "Tree");
}

function SetTree() {

    //$('#tree').jstree();
    $('#tree').jstree({
        "core": {
            // so that create works
            "check_callback": true
        },
        "plugins": ["contextmenu"],
        "contextmenu": {
            "items": function ($node) {
                var tree = $("#tree").jstree(true);
                return {
                    "Create": {
                        "separator_before": false,
                        "separator_after": false,
                        "label": "Crear",
                        "action": function (obj) {
                            var idnewnode = tree.create_node($node, { "data": { codigo: 0 } });
                            var newnode = tree.get_node(idnewnode);

                            //node.data.codigo = 0;
                            EditCuenta(tree, newnode, $node);

                            //$node = 
                            //tree.rename_node($node,"nuevo");
                            //tree.edit($node);
                        }
                    },
                    "Rename": {
                        "separator_before": false,
                        "separator_after": false,
                        "label": "Editar",
                        "action": function (obj) {
                            var parent = tree.get_node($node.parent)
                            EditCuenta(tree, $node, parent);
                            //tree.edit($node);
                        }
                    },
                    "Remove": {
                        "separator_before": false,
                        "separator_after": false,
                        "label": "Eliminar",
                        "action": function (obj) {
                            var parent = tree.get_node($node.parent)
                            RemoveCuenta(tree, $node, parent);
                        }
                    },
                    "Cut": {
                        "separator_before": true,
                        "separator_after": false,
                        "label": "Cortar",
                        "action": function (obj) { tree.cut($node); }
                    },
                    "Paste": {
                        "label": "Pegar",
                        "action": function (obj) { tree.paste($node); }
                    }



                };
            }
        }
    });
    $('#tree').jstree('open_all');
}




function PrintSaldosObj() {

    var almacen = "";
    var centro = "";
    var fechac = "";
    var nivel = "";
    var modulo = "";
    var movimiento = "";
    var debcre = "3";
    var empresa = GetOnlineCompany();
    var tipo = "a";
    var todas = true;
    var saldo = true;

    var url = "../reports/wfReportPrint.aspx?report=PLANCTA&empresa=" + empresa + "&parameter1=" + fechac + "&parameter2=" + almacen + "&parameter3=" + debcre + "&parameter4=" + empresa + "&parameter5=" + tipo + "&parameter6=" + todas + "&parameter7=1&parameter8=" + saldo;
    var feautures = "top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',toolbar=0 ,location=0,directories=0,status=0,menubar=0,resizable=yes,scrolling=yes,scrollbars=yes";
    window.open(url, "Reporte", feautures);

}