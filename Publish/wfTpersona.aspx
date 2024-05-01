<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfTpersona.aspx.cs" Inherits="WebUI.wfTpersona" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title></title>
  
    <link rel="stylesheet" href="css/style.default.css" type="text/css" />        
    <link rel="stylesheet" href="css/responsive-tables.css">
     <link rel="stylesheet" href="css/Estilo1.css" />   
    

    <script type="text/javascript" src="js/prefixfree.min.js"  ></script>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery-ui.js"></script>    
    <script type="text/javascript" src="js/login.js"></script>
    <script type="text/javascript" src="js/modernizr.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/jquery.jgrowl.js"></script>
    <script type="text/javascript" src="js/jquery.alerts.js"></script>
    <script type="text/javascript" src="js/jquery.cookie.js"></script>    
    <!--<script type="text/javascript" src="js/jquery.uniform.min.js"></script>-->
    <script type="text/javascript" src="js/flot/jquery.flot.min.js"></script>
    <script type="text/javascript" src="js/flot/jquery.flot.resize.min.js"></script>
    <script type="text/javascript" src="js/responsive-tables.js"></script>
    <script type="text/javascript" src="js/chosen.jquery.min.js"></script>
    <script type="text/javascript" src="js/general.js"></script>    
    <script type="text/javascript" src="js/forms/Tpersona.js"></script>    
    <script type="text/javascript" src="js/common.js"></script>
    <!--[if lte IE 8]><script language="javascript" type="text/javascript" src="js/excanvas.min.js"></script><![endif]-->    
</head>
<body>
    <form id="form1" runat="server">
     <div class="pageheader">
            <div class="searchbar">               
                <div id="barra">                    
                    <ul class="list-nostyle list-inline">
                        <li><div class="btn" id="help" href="#myModal" data-toggle="modal"><i class="iconsweets-info"></i> &nbsp; Ayuda</div></li>
                        <li><div class="btn" id="search"><i class="iconsweets-magnifying"></i> &nbsp; Buscar </div></li>
                        <li><div class="btn" id="add"><i class=" iconsweets-documents"></i>&nbsp; Nuevo</div></li>
                        <li><div class="btn" id="list"><i class="iconsweets-list"></i>&nbsp; Listado</div></li>
                        <li><div class="btn" id="edit"><i class="iconsweets-create"></i>&nbsp; Editar</div></li>
                        
                    </ul>
                 
                </div>
            </div>
           <div class="pageicon"><span class="iconfa-tasks"></span></div>
            <div class="pagetitle">
                <h5>Gestión de Tipos de Persona</h5>
                <h1>Tipos de Persona</h1>
            </div>
        </div><!--pageheader-->
     <div class="maincontent">
            <div class="maincontentinner">
                 <div id="busqueda" class="widgetbox">
                     <h4 class="widgettitle"><span class="icon-filter icon-white"></span>Busqueda<a class="close">&times;</a>  </h4>
                    <div id="busquedacontent" class="widgetcontent">                                                
                    </div>                    
                </div> 
                 
                <div id="area">
                    
                    <div id="listado" class="widgetbox pull-left">      
                         <div class="headtitle">
                            <div class="btn-group">
                                <button data-toggle="dropdown" class="btn dropdown-toggle">Opciones <span class="caret"></span></button>
                                <ul class="dropdown-menu">
                                  <li><a id ="addopt" href="#">Agregar Opción</a></li>
                                  <li><a id ="addchildopt" href="#">Agregar Hijo</a></li>                                  
                                  <li><a id ="reflist" href="#">Refrescar listado</a></li>                                                                    
                                </ul>
                            </div>
                            <h4 class="widgettitle"><span class="icon-th-list icon-white"></span>Listado<a class="close">&times;</a>&nbsp;</h4>
                        </div>         
                        <div class="widgetcontent">                                       
                            <div id ="listadocontent">                                                     
                               


                            </div>                               
                        </div> 
                    </div> 
                         
                    <div id="datos" class="widgetbox pull-right">   
                       <div class="headtitle">
                            <div class="btn-group">
                                <button data-toggle="dropdown" class="btn dropdown-toggle">Opciones <span class="caret"></span></button>
                                <ul class="dropdown-menu">
                                  <li><a id ="newedit" href="#">Nuevo</a></li>
                                  <li><a id ="saveedit" href="#">Guardar</a></li>
                                  <li><a id ="deledit" href="#">Eliminar</a></li>                                                                    
                                  <li class="divider"></li>                                  
                                  <li><a id ="closeedit" href="#">Cerrar edición</a></li>
                                </ul>
                            </div>
                            <h4 class="widgettitle"><span class="icon-edit icon-white"></span>Edición<a class="close">&times;</a>&nbsp;</h4>
                        </div>         
                        <div class="widgetcontent">
                            <div id="datoscontent" class='stdform'>                                                                                     
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
                        <button data-dismiss="modal" class="btn">Cerrar</button>                        
                    </div>
                </div><!--#myModal-->
                <div id="cargando"></div>  
                
            </div><!--maincontentinner-->
                </form>
</body>
</html>
