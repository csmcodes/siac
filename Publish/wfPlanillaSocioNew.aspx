<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfPlanillaSocioNew.aspx.cs" Inherits="WebUI.wfPlanillaSocioNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
    <script type="text/javascript" src="js/functions.js"></script>
    <script type="text/javascript" src="js/popup.js"></script>
    <script type="text/javascript" src="js/forms/autocomplete.js"></script>        
    <script type="text/javascript" src="js/forms/PlanillaSocioNew.js"></script>
    <!--[if lte IE 8]><script language="javascript" type="text/javascript" src="js/excanvas.min.js"></script><![endif]-->    
</head>
<body>
    <form id="form1" runat="server">

     <div class="maincontent">
            <div class="maincontentinner">                                              
               <div id="area">
                          <div style = "display :none">
                                        <asp:TextBox ID="txttipodoc" runat="server"></asp:TextBox>        
                                        <asp:TextBox ID="txtcodigocomp" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtcodsocio" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtorigen" runat="server"></asp:TextBox>     
                                        <asp:TextBox ID="txtvehiculo" runat="server"></asp:TextBox>     
                                    </div>              

                     <div id="comcabecera">     
                        
                        <div id ="comcab" class="stdform">                                                     
                         </div>                                                       
                         <div id ="comcabeceracontent" class="stdform">                                                     
                         </div>                                                       
                    </div>                          
                    <div id="comdetalle" >   
                             <div id="barracomp">
                                <ul class="list-nostyle list-inline">
                                    <li><div class="btn" id="adddet"><i class="iconfa-plus"></i> &nbsp; Agregar</div></li>
                                    <li><div class="btn" id="deldet"><i class="iconfa-remove"></i> &nbsp; Quitar</div></li>
                                    <!--<li><div class="btn" id="movedet"><i class="iconfa-signin"></i> &nbsp; Mover</div></li>-->
                                    <li><div class="btn" id="alldet"><i class="iconfa-check"></i> &nbsp; Seleccionar Todos</div></li>
                                    <li><div class="btn" id="nonedet"><i class="iconfa-check-empty"></i> &nbsp; Limpiar Selección</div></li>
                                    
                                </ul>
                            </div>
                        
                        <div class="row-fluid">                                                    
                                <div class="span9">     
                                    <div id="comdetallecontent" class='stdform'>                                                      
                                    </div>                                       
                                 </div><!--span6-->
                                 <div class="span3">     
                                    <div id="compiecontent" class="stdform">
                                    </div>
                                 </div><!--span6-->
                         </div><!--row-fluid-->                                 
                            
                </div>
                              


                <div id="cargando"></div>  
            </div>
                <div class="footer">
                    <div class="footer-left">
                        <span>&copy; 2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados.</span>
                    </div>
                    <div class="footer-right">
                        <span>Desarrrollado por: <a href="http://gtec.com.ec/">GTEC</a></span>
                    </div>
                </div><!--footer-->
                
            </div><!--maincontentinner-->
    </form>
</body>
</html>
