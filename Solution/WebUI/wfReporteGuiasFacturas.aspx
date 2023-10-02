<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfReporteGuiasFacturas.aspx.cs" Inherits="WebUI.wfReporteGuiasFacturas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

     <link rel="stylesheet" href="css/style.default.css" type="text/css" />        
    <link rel="stylesheet" href="css/responsive-tables.css">
     <link rel="stylesheet" href="css/Estilo1.css" />       

    <script type="text/javascript" src="js/prefixfree.min.js"  ></script>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery-ui.js"></script>        
    <script type="text/javascript" src="js/modernizr.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/jquery.jgrowl.js"></script>
    <script type="text/javascript" src="js/jquery.alerts.js"></script>        
    <script type="text/javascript" src="js/jquery.cookie.js"></script> 
    <script type="text/javascript" src="js/flot/jquery.flot.min.js"></script>
    <script type="text/javascript" src="js/flot/jquery.flot.resize.min.js"></script>
    <script type="text/javascript" src="js/responsive-tables.js"></script>
    <script type="text/javascript" src="js/chosen.jquery.min.js"></script>
    <script type="text/javascript" src="js/general.js"></script>    
    <script type="text/javascript" src="js/methods.js"></script>    
    <script type="text/javascript" src="js/functions.js"></script>
    <script type="text/javascript" src="js/popup.js"></script>
    <script type="text/javascript" src="js/forms/autocomplete.js"></script>        
    <script type="text/javascript" src="js/forms/ReporteGuiasFacturas.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class = "maincontent">
    <div class = "maincontentinner">
    
     <div id="area">
                    <div style = "display :none">
                        <asp:TextBox ID="txttipodoc" runat="server"></asp:TextBox>        
                        <asp:TextBox ID="txtcodigocomp" runat="server"></asp:TextBox>                                        
                    </div>              
                    <h4>REPORTE DE GUIAS Y FACTUDAS</h4>
                    <br />
                     <div id="comcabecera">     
                                                                                                 
                    </div>                          
                                                            
                </div>
                                              
            </div>
                <div class="footer">
                    <div class="footer-left">
                        <span>&copy; 2020. CSM Devs. Todos los derechos reservados.</span>
                    </div>
                    <div class="footer-right">
                        <span>Desarrrollado por: <a href="http://csmdevs.com.ec/">CSM Devs</a></span>
                    </div>
                </div><!--footer-->
                
            </div><!--maincontentinner-->

    </form>
</body>
</html>
