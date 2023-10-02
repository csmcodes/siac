﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfEstadoCuenta.aspx.cs" Inherits="WebUI.wfEstadoCuenta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title></title>  
    <link rel="stylesheet" href="css/style.default.css" type="text/css" />        
    <link rel="stylesheet" href="css/responsive-tables.css" />
     <link rel="stylesheet" href="css/Estilo1.css" />   
    

    <script type="text/javascript" src="js/prefixfree.min.js"  ></script>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery-ui.js"></script>       
    <script type="text/javascript" src="js/modernizr.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/jquery.jgrowl.js"></script>
    <script type="text/javascript" src="js/jquery.alerts.js"></script>
    <script type="text/javascript" src="js/jquery.cookie.js"></script>    
    <!--<script type="text/javascript" src="js/jquery.uniform.min.js"></script>-->
    <script type="text/javascript" src="js/flot/jquery.flot.min.js"></script>
    <script type="text/javascript" src="js/flot/jquery.flot.resize.min.js"></script>
    <script type="text/javascript" src="js/responsive-tables.js"></script>
     <script type="text/javascript" src="js/general.js"></script>    
    <script type="text/javascript" src="js/functions.js"></script>
    <script type="text/javascript" src="js/popup.js"></script>
    <script type="text/javascript" src="js/forms/autocomplete.js"></script>        
    <script type="text/javascript" src="js/forms/EstadoCuenta.js"></script>
    <!--[if lte IE 8]><script language="javascript" type="text/javascript" src="js/excanvas.min.js"></script><![endif]-->    
</head>
<body>
    <form id="form1" runat="server">
    <div class="pageheader">
            <div class="searchbar">               
                <div id="barra">                    
                    <ul class="list-nostyle list-inline">
                        <li><div class="btn" id="help" href="#myModal" data-toggle="modal"><i class="iconsweets-info"></i> &nbsp; Ayuda</div></li>                        
                    </ul>
                 
                </div>
            </div>
           <div class="pageicon"><span class="iconfa-cogs"></span></div>
            <div class="pagetitle">
                <h5>Listado Estado Cuenta</h5>
                <h1>Estado Cuenta</h1>
            </div>
        </div><!--pageheader-->
     <div class="maincontent">
            <div class="maincontentinner">
                 <div id="area">
                    <div style = "display :none"> 
                        <asp:TextBox ID="txtdebcre" runat="server"></asp:TextBox> 
                        <asp:TextBox ID="txtcodigoper" runat="server"></asp:TextBox> 
                    </div> 
                      <div id="comcabecera">     
                         <ul class="list-nostyle list-inline">
                        
                        <li><div class="btn" onclick="window.location.reload(true);"><i class="iconfa-refresh"></i>&nbsp;</div></li>
                        <li><div class="btn" id="printestado"><i class="iconfa-print"></i>&nbsp; Imprimir</div></li>
                        
                    </ul>
                        <div id ="comcab" class="stdform">                                                     
                         </div>                                                       
                         <div id ="comcabeceracontent" class="stdform">                                                     
                         </div>                                                       
                    </div>                          
                    <div id="comdetalle" >   
                         
                        <div class="row-fluid">                                                    
                                <div class="span9">     
                                    <div id="comdetallecontent" class='stdform'>                                                      
                                    </div>                                       
                                 </div><!--span6-->
                                 <div class="span3">     
                                    <div id="compiecontent" >
                                    </div>
                                 </div><!--span6-->
                         </div><!--row-fluid-->                                 
                            
                </div>             



                    <div id="cargando"></div>  
                </div>
            </div>
                <div class="footer">
                    <div class="footer-left">
                        <span>&copy; 2017. SIAC Sistemas Inteligentes.</span>
                    </div>
                    <div class="footer-right">
                        <span>Desarrrollado por: <a href="http://www.siac.com.ec/">SIAC</a></span>
                    </div>
                </div><!--footer-->
                
            </div><!--maincontentinner-->
    </form>
</body>
</html>