
var webreport = "reports/";

function PrintReport(event) {

    var titulo = event.data.titulo;
    var parametros = event.data.parametros;
    var reporte = event.data.reporte;
    var autoprint = event.data.autoprint;
    Print(titulo, parametros, reporte, autoprint);
   
}


function Print(titulo, parametros, reporte, autoprint) {
    var opciones = "toolbar=no, scrollbars=no, resizable=yes, top=50, left=100, width=800, height=600";
    if (titulo == "")
        titulo = "_blank";

    var url = webreport + "repPrint.aspx" + "?report=" + reporte + "&autoprint=" + autoprint + "&" + parametros;
    window.open(url, titulo, opciones);
}
