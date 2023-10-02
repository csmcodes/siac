<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfPruebas.aspx.cs" Inherits="WebUI.wfPruebas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" href="css/style.default.css" type="text/css" />        
    <link rel="stylesheet" href="css/responsive-tables.css">
     <link rel="stylesheet" href="css/Estilo1.css" />
     <link rel="stylesheet" href="css/defaultTheme.css" />
      <script type="text/javascript" src="js/prefixfree.min.js"  ></script>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery-ui.js"></script>        
    <script type="text/javascript" src="js/jquery.fixedheadertable.js"></script>        
     <script type ="text/javascript" >

         $(document).ready(function () {

             LoadList();
             $('body').css('background', 'transparent');

             //SetScrollableTable("tdafec_P");
         });

         function CallServer(strurl, strdata, retorno) {             
             $.ajax({
                 type: "POST",
                 url: strurl,
                 data: strdata,
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (data) {
                     if (retorno == 0)
                         LoadListResult(data);                    

                 },
                 error: function (XMLHttpRequest, textStatus, errorThrown) {
                     var errorData = $.parseJSON(XMLHttpRequest.responseText);                     
                 }

             })
         }


         function LoadList() {
                 var jsonText = ""; //TOCA DETERMINAR DEPENDIENDO DE CADA FORM   
                 CallServer("wfPruebas.aspx/GetTabla", jsonText, 0);
         }

         function LoadListResult(data) {
             if (data != "") {
                 $('#con').append(data.d);

                 $('#datos').fixedHeaderTable({
                        height: '400',
	                   footer: true,
	                    altClass: 'odd',
                  });
                 //FitTable("tddatos");
                 //SetFooter("tddatos");
             }             
         }

         function SetFooter(id) {
             $("#" + id + "_f1").html("TOTAL");
         }

         function FitTable(id) {
             var rowh = $("#" + id).find("thead > tr")
             var rowb = $("#" + id).find("tbody > tr");
             var rowf = $("#" + id).find("tfoot > tr")

             $("#" + id).find("tbody > tr").eq(0).each(function () {
                 $(this).children("td").each(function () {
                     var ancho = $(this).width();                                          
                     
                     //var ancho = $(this)[0].clientWidth-15;

                     var indice = $(this).index();
                     
                     var th = $(rowh).children("th").eq(indice);
                     //$(th)[0].clientWidth = ancho+"px";
                     $(th).css("width", ancho + "px");
                     var tf = $(rowf).children("th").eq(indice);
                     //$(tf)[0].clientWidth = ancho +"px";
                     $(tf).css("width", ancho + "px");

                     //var tb = $(rowb).children("td").eq(indice);
                     //$(tb).css("width", ancho + "px");

                 }); // next td
             });         // next tr
            
         }
         function SetScrollableTable(id) {
             var contenedor = $("#" + id).parent("div");

             $(contenedor).on("scroll", scrollTable);
         }

         function scrollTable() {
             $(this).find("thead").css("top", $(this).scrollTop())
             var bot = $(this).scrollTop() + $(this).height()
             $(this).find("tfoot").css("bottom", bot)
            //alert($(this).scrollTop()); 
         }
     </script>       
     
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <div id ="con" class="span6" >
            

        </div>
    
    </div>
    </form>
</body>
</html>
