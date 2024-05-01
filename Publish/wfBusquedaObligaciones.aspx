<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfBusquedaObligaciones.aspx.cs" Inherits="WebUI.wfBusquedaObligaciones" %>

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
    <!--<script type="text/javascript" src="js/jquery.uniform.min.js"></script>-->
    <script type="text/javascript" src="js/flot/jquery.flot.min.js"></script>
    <script type="text/javascript" src="js/flot/jquery.flot.resize.min.js"></script>
    <script type="text/javascript" src="js/responsive-tables.js"></script>
    <script type="text/javascript" src="js/general.js"></script>    
    <script type="text/javascript" src="js/forms/BusquedaObligaciones.js" ></script>    
    <!--[if lte IE 8]><script language="javascript" type="text/javascript" src="js/excanvas.min.js"></script><![endif]-->    
</head>
<body>
    <form id="form1" runat="server">
    <div class="pageheader">
           
     <div class="pageicon"><span class="iconfa-user"></span></div>
            <div class="pagetitle">
                <h5>Generar Retenciones</h5>
                <h1>Generar Retenciones</h1>
            </div>
        </div><!--pageheader-->
     <div class="maincontent">
            <div class="maincontentinner">
                 <div id="area">

                    <div id="busqueda" class="widgetbox pull-left">      
                         <div class="headtitle">
                            <div class="btn-group">
                                <button data-toggle="dropdown" class="btn dropdown-toggle">Opciones <span class="caret"></span></button>
                                <ul class="dropdown-menu">
                                    <li><a id ="addcomp" href="#"><i class='iconsweets-refresh'></i>&nbsp; Nuevo</a></li>                                     
                                    <li><a id ="a2" href="#"><i class='iconsweets-refresh'></i>&nbsp; Refrescar</a></li> 
                                    <li><a id ="a1" href="#"><i class='iconsweets-pencil'></i>&nbsp; Limpiar</a></li>
                                                                   
                                </ul>
                            </div>
                            <h4 class="widgettitle"><span class="icon-search icon-white"></span>Busqueda<a class="close">&times;</a>&nbsp;</h4>
                        </div>         
                        <div class="widgetcontent">                                       
                            <div id ="busquedacontent" >                                                     
                                 <div id="busquedafiltros">
                                    
                                </div>                                
                                <table id="busquedatabla"  class="table table-bordered responsive">
                                        <thead id ="busquedahead">  
                                                                           
                                        </thead>
                                        <tbody id="busquedadetalle" >
                                            
                                        </tbody>
                                    </table>
                            </div>                               
                        </div> 
                    </div> 
                                      
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
                        <button data-dismiss="modal" class="btn btn-primary btn-rounded">Aceptar</button> 
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
    </form>
</body>
</html>
