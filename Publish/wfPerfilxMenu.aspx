<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfPerfilxMenu.aspx.cs" Inherits="WebUI.wfPerfilxMenu" %>

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
    <script type="text/javascript" src="js/forms/PerfilxMenu.js"></script>    
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
                        <li><div class="btn" id="edit" href="#EditModal" data-toggle="modal"><i class="iconsweets-create"></i>&nbsp; Editar</div></li>
                        
                    </ul>
                 
                </div>
            </div>
           <div class="pageicon"><span class="iconfa-cogs"></span></div>
            <div class="pagetitle">
                <h5>Asignación de Menus por Perfil</h5>
                <h1>Menus Perfil</h1>
            </div>
        </div><!--pageheader-->
     <div class="maincontent">
            <div class="maincontentinner">
                 <div id="area">
                    <div id="masterdetail" class="widgetbox pull-left">      
                        <h4 class="widgettitle"><span class="icon-th-list icon-white"></span>Perfil</h4>
                        <div class="widgetcontent">
                            <div id="masterdetailcontent" class='stdform'>
                                <div id="cabecera">
                                    
                                </div>                                
                                <div id="detalle">                                    
                                    <div class="headtitle">
                                        <div class="btn-group">
                                            <button data-toggle="dropdown" class="btn dropdown-toggle">Opciones <span class="caret"></span></button>
                                            <ul class="dropdown-menu">
                                              <li><a id ="addnew" href="#"><i class="iconsweets-documents"></i> &nbsp;Agregar nuevo</a></li>
                                              <li><a id ="remove" href="#"><i class="iconsweets-trashcan"></i> &nbsp;Eliminar seleccionados</a></li>                                                                                
                                            </ul>
                                        </div>
                                        <h4 class="widgettitle"><span class="icon-th-list icon-white"></span>Menus Asignados&nbsp;</h4>
                                    </div>                                       
                                    <table id="detalletabla"  class="table table-bordered responsive">
                                        <thead>  
                                            <tr>
                                                <th></th> 
                                                <th>Menu</th>                                                
                                                <th>Agregar</th>
                                                <th>Modificar</th>
                                                <th>Eliminar</th>
                                                <th>Activo</th>                                            
                                            </tr>                                          
                                        </thead>
                                        <tbody id="detallebody" >
                                            
                                        </tbody>
                                    </table>
                                </div>                                                                                     
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


                 <div aria-hidden="false" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" class="modal hide fade in" id="EditModal">
                    <div class="modal-header">
                        <button aria-hidden="true" data-dismiss="modal" class="close" type="button">&times;</button>
                        <h3 id="H1">Edición</h3>
                    </div>
                    <div id="formmodal" class="modal-body" style ="width :100%;height:800px;">                        
                        Aqui va el form
                    </div>
                    <div class="modal-footer">
                        <button id="saveedit" data-dismiss="modal" class="btn btn-primary btn-rounded">Aceptar</button> 
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
