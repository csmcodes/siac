<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfPrint.aspx.cs" Inherits="WebUI.print.wfPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript" src="../js/prefixfree.min.js"  ></script>
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>        
    <script type="text/javascript" src="../js/modernizr.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            
            var $iframe = $('#ifPreview');
            if ($iframe.length) {
                $iframe.attr('src', '../pdf/' + $('#txtfilename').val());
            }
            jsWebClientPrint.getPrinters();
            
        });


        function GetPrinters()
        {
         jsWebClientPrint.getPrinters();
        }

        var wcppGetPrintersDelay_ms = 1; //5 sec

        function wcpGetPrintersOnSuccess(){
            <%-- Display client installed printers --%>

            var printersel =$('#txtprinter').val();
            if(arguments[0].length > 0){
                var p=arguments[0].split("|");
                var options = '';
                for (var i = 0; i < p.length; i++) {
                    if (p[i] == printersel)
                        options += '<option selected>' + p[i] + '</option>';
                    else
                        options += '<option>' + p[i] + '</option>';
                }
                $('#installedPrinters').css('visibility','visible');
                $('#installedPrinterName').html(options);
                $('#installedPrinterName').focus();
                $('#loadPrinters').hide();    
                
                if (confirm("¿Desea imprimir el documento?"))
                    Print();                                                    
            }else{
                alert("No printers are installed in your system.");
            }
        }

        function wcpGetPrintersOnFailure() {
            <%-- Do something if printers cannot be got from the client --%>
            alert("No printers are installed in your system.");
        }


        function Print()
        {

            jsWebClientPrint.print('useDefaultPrinter=no&printerName='+ $('#installedPrinterName').val()+'&fileName='+ $('#txtfilename').val()+'&path='+ $('#txtpath').val());
            window.close();
        }

    </script>

</head>
<%-- WCPP-detection meta tag for IE10 --%>      
<%= Neodynamic.SDK.Web.WebClientPrint.GetWcppDetectionMetaTag() %>
<body>
    <%-- Store User's SessionId --%>
    
    
     <select name="installedPrinterName" id="installedPrinterName"></select>
     <input type="text" id="printerName" style="display:none"></input>                                    
    <input type="button" value="..." onclick="GetPrinters();"/>        
                                    


    <input type="button" value="Imprimir" onclick="Print();"/>
    
     <iframe id="ifPreview" style="width:100%; height:500px;" frameborder="0">
     </iframe>     
    <form id="form1" runat="server">
    <div style="display:none">
        <asp:TextBox ID="txtcodigo" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtfilename" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtpath" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtprinter" runat="server"></asp:TextBox>
    </div>
    
    </form>

    <%-- Register the WebClientPrint script code --%>  
    <%=Neodynamic.SDK.Web.WebClientPrint.CreateScript(MyUtils.GetWebsiteRoot() + "print/Print.ashx")%>
</body>
</html>
