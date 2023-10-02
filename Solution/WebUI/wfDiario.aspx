<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfDiario.aspx.cs" Inherits="WebUI.wfDiario" %>

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
    <script type="text/javascript" src="js/bootstrap-timepicker.min.js"></script>
    <script type="text/javascript" src="js/jquery.jgrowl.js"></script>
    <script type="text/javascript" src="js/jquery.alerts.js"></script>        
    <script type="text/javascript" src="js/jquery.cookie.js"></script> 
    <script type="text/javascript" src="js/flot/jquery.flot.min.js"></script>
    <script type="text/javascript" src="js/flot/jquery.flot.resize.min.js"></script>
    <script type="text/javascript" src="js/responsive-tables.js"></script>
    <script type="text/javascript" src="js/chosen.jquery.min.js"></script>
    <script type="text/javascript" src="js/general.js"></script>    
    <script type="text/javascript" src="js/popup.js"></script>
    <script type="text/javascript" src="js/functions.js"></script>    

    <script type="text/javascript" src="js/forms/autocomplete.js"></script>        
   
    <script type="text/javascript" src="js/forms/DiarioD.js"></script>
     <script type="text/javascript" src="js/forms/Diario.js"></script>
</head>
<body>
  <form id="form1" runat="server">

     <div class="maincontent">
            <div class="maincontentinner">
                                
                 
                <div id="area">
                          <div style = "display :none">
                                        <asp:TextBox ID="txttipodoc" runat="server"></asp:TextBox>                                        
                                        <asp:TextBox ID="txtcodigocomp" runat="server"></asp:TextBox>                                        
                                        <asp:TextBox ID="txtnocontable" runat="server"></asp:TextBox>                
                                        <asp:TextBox ID="txtorigen" runat="server"></asp:TextBox>                
                                        <asp:TextBox ID="txtcodigocompref" runat="server"></asp:TextBox>                                        

                                    </div>              

                    <div id="comcabecera">     
                        
                        <div id ="comcab" class="stdform">                                                     
                         </div>                                                       
                         <div id ="comcabeceracontent" class="stdform">                                                     
                         </div>                                                       
                    </div>                          
                    <div id="comdetallediario" >   
                        <div id="barradiario">
                                
                        </div>
                        <div class="row-fluid">                        
                                <div class="span9">     
                                    <div id="comdetallediariocontent" class='stdform'>                                                      
                                    </div>                                       
                                 </div><!--span6-->
                                 <div class="span3">     
                                    <div id="compiediariocontent" >
                                    </div>
                                 </div><!--span6-->
                         </div><!--row-fluid-->                                                
                            
                </div>
                

                <div aria-hidden="false" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" class="modal hide fade in" id="myModal">
                    <div class="modal-header">
                        <button aria-hidden="true" data-dismiss="modal" class="close" type="button">&times;</button>
                        <h3 id="myModalLabel">Ayuda Usuario</h3>
                    </div>
                    <div class="modal-body" style ="width :80%;height:800px;">                        
                        Aqui va la ayuda de la pantalla en HTML
                    </div>
                    <div class="modal-footer">
                        <button data-dismiss="modal" class="btn">Cerrar</button>                        
                    </div>
                </div><!--#myModal-->
              


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
    </div>

    </form>
</body>
</html>
