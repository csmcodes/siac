/*
function SetAutocomplete() {    
    $("#datoscontent").find("[data-autocomplete]").each(function () {
        SetAutocompleteByElement(this) 
    });
}

function SetAutocompleteByContenedor(idcontenedor) {
    $("#"+idcontenedor).find("[data-autocomplete]").each(function () {
        SetAutocompleteByElement(this)
    });
}

function SetAutocompleteByElement(obj) {
    var data = $(obj).data("autocomplete");
    var id = $(obj)[0].id;
    $("#" + id).autocomplete
    ({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                url: "ws/Metodos.asmx/" + data,
                dataType: "json",
                data: "{ 'filterKey': '" + request.term + "' }",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.label,
                            value: item.value,
                            info: item.info                             
                            //label: item.per_id + "," + item.per_ciruc + "," + item.per_razon,
                            //value: item.per_codigo
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    var errorMessage = "Ajax error: " + this.url + " : " + textStatus + " : " + errorThrown + " : " + XMLHttpRequest.statusText + " : " + XMLHttpRequest.status;
                    if (XMLHttpRequest.status != "0" || errorThrown != "abort") {
                        alert(errorMessage);
                    }
                }
            });
        },
        select: function (event, ui) {
            $("#" + id + "_info").html(ui.item.info);
            //alert(ui.item.label);
        },
        autoFocus: true,
        delay: 100
    });

}

function SetAutocompleteById(idobj) {

    SetAutocompleteByIdTipo(idobj, ""); 
}

function SetAutocompleteByIdTipo(idobj, tipo) {
    var data = $("#" + idobj).data("autocomplete");
    $("#" + idobj).autocomplete
    ({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                url: "ws/Metodos.asmx/" + data,
                dataType: "json",
                data: "{ 'filterKey': '" + request.term + "' }",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        if (tipo =="")
                            return SetAutoCompleteObj(idobj, item); //Depende de cada  llamada                       
                        else if (tipo =="DIARIO")
                            return SetAutoCompleteObjDiario(idobj, item); //Depende de cada  llamada                       
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    var errorMessage = "Ajax error: " + this.url + " : " + textStatus + " : " + errorThrown + " : " + XMLHttpRequest.statusText + " : " + XMLHttpRequest.status;
                    if (XMLHttpRequest.status != "0" || errorThrown != "abort") {
                        alert(errorMessage);
                    }
                }
            });
        },
        select: function (event, ui) {
            if (tipo == "")
                GetAutoCompleteObj(idobj, ui.item);
            else if (tipo =="DIARIO")
                GetAutoCompleteObjDiario(idobj, ui.item);

        },
        autoFocus: true,
        delay: 100
    });
} 


function SetAutocompleteByObj(obj) {
    var data = $(obj).data("autocomplete");
    var idobj = $(obj)[0].id;
    $(obj).autocomplete
    ({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                url: "ws/Metodos.asmx/" + data,
                dataType: "json",
                data: "{ 'filterKey': '" + request.term + "' }",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return SetAutoCompleteObj(idobj, item); //Depende de cada  llamada                       
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    var errorMessage = "Ajax error: " + this.url + " : " + textStatus + " : " + errorThrown + " : " + XMLHttpRequest.statusText + " : " + XMLHttpRequest.status;
                    if (XMLHttpRequest.status != "0" || errorThrown != "abort") {
                        alert(errorMessage);
                    }
                }
            });
        },
        select: function (event, ui) {
            GetAutoCompleteObj(idobj, ui.item);

        },
        autoFocus: true,
        delay: 100
    });

}




function SetAutocompleteByIdParams(idobj, params) {
    var data = $("#" + idobj).data("autocomplete");
    $("#" + idobj).autocomplete
    ({
        source: function (request, response) {
            params["filterKey"] = request.term;
            $.ajax({
                type: "POST",
                url: "ws/Metodos.asmx/" + data,
                dataType: "json",
                //data: "{ 'filterKey': '" + request.term + "','params': '"+ JSON.stringify(params)+"' }",
                data:  JSON.stringify({ parametros: params}), 
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                            return SetAutoCompleteObj(idobj, item); //Depende de cada  llamada                                               
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    var errorMessage = "Ajax error: " + this.url + " : " + textStatus + " : " + errorThrown + " : " + XMLHttpRequest.statusText + " : " + XMLHttpRequest.status;
                    if (XMLHttpRequest.status != "0" || errorThrown != "abort") {
                        alert(errorMessage);
                    }
                }
            });
        },
        select: function (event, ui) {
                GetAutoCompleteObj(idobj, ui.item);            
        },
        autoFocus: true,
        delay: 100
    });
} */


/*function SetAutocomplete(id) {

    $("#" + id).autocomplete
    ({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                url: "ws/Metodos.asmx/GetPersona",
                dataType: "json",
                data: "{ 'filterKey': '" + request.term + "' }",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.per_id + "," + item.per_ciruc+"," + item.per_razon,
                            value: item.per_codigo
                        }                        
                    }))
                },               
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    var errorMessage = "Ajax error: " + this.url + " : " + textStatus + " : " + errorThrown + " : " + XMLHttpRequest.statusText + " : " + XMLHttpRequest.status;
                    if (XMLHttpRequest.status != "0" || errorThrown != "abort") {
                        alert(errorMessage);
                    }
                }
            });
        },
        select: function (event, ui) {
            alert(ui.item.label);
        },
        delay: 100
    });
}*/

function RemoveTypeAhead(id)
{
    $("#" + id).typeahead('destroy');
    
    
    
}

function SetTypeAhead(id, displayvalue, method) {
    SetTypeAheadTemplate(id, displayvalue, method, "<div><strong>{{"+displayvalue+"}}</strong>")
}

function SetTypeAheadTemplate(id, displayvalue, method, template) {
    var obj = {};
    obj["empresa"] = parseInt(GetOnlineCompany());

    $("#" + id).typeahead(null, {
        displayKey: displayvalue,
        hint: (Metronic.isRTL() ? false : true),
        source: function (query, process) {
            obj["filterKey"] = query;
            return $.ajax({
                type: "POST",
                url: "ws/Metodos.asmx/" + method,
                //data: JSON.stringify({query: query }),
                data: JSON.stringify({ parametros: obj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success:
                function (data) {
                    lst = data.d;
                    return process(lst);
                    //                            var countryIds = _.map(countries, function(c){
                    //                                return c.Id; 
                    //                            }); 
                    //                            return process(countryIds);
                }
            });
        },
        templates: {
            empty: [
              'No se encontraron resultados',
            ].join('\n'),
            suggestion: Handlebars.compile(template)
            //suggestion: Handlebars.compile('<div><strong>{{per_razon}}</strong> – {{per_ciruc}}</div>')
        },
        matcher: function (item) {
            return true;
        },
        sorter: function (items) {
            return items;
        }
               ,
        highlighter: function (id) {
            return "<b>" + id + "</b>";
        }
    })    
    .on('typeahead:selected', function ($e, datum) {
        try{

            onAutocompleted(id,datum)
        } catch (err)
        {            
        }
        
    }
    );
}




