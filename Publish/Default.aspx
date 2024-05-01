<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebUI.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" href="css/micse.css" type="text/css" />        
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
    <script type="text/javascript" src="js/forms/Index.js"></script>    
</head>
<body>
    <div class="mainwrapper">
        <div class="header">
        <div class="logo">
            <a href="Default.aspx"><img src="images/micse/logomicse80.png" alt="" /></a>
        </div>
        <div class="headerinner">
            <ul class="headmenu">
                <li class="odd">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <span class="count">5</span>
                        <span class="head-icon head-message"></span>
                        <span class="headmenu-label">Mensajes</span>
                    </a>
                    <ul class="dropdown-menu">
                        <li class="nav-header">Mensajes</li>
                        <li><a href=""><span class="icon-envelope"></span> Nuevo mensaje de <strong>Director </strong> <small class="muted"> - hace 19 horas </small></a></li>
                        <li><a href=""><span class="icon-envelope"></span> Nuevo mensaje de <strong>Director </strong> <small class="muted"> - hace 2 días</small></a></li>
                        <li><a href=""><span class="icon-envelope"></span> Nuevo mensaje de <strong>Maria Perez</strong> <small class="muted"> - hace 3 días</small></a></li>
                        <li><a href=""><span class="icon-envelope"></span> Nuevo mensaje de <strong>Vicepresidencia</strong> <small class="muted"> - hace 1 semana</small></a></li>
                        <li><a href=""><span class="icon-envelope"></span> Nuevo mensaje de <strong>Director Regional</strong> <small class="muted"> - hace 1 semana</small></a></li>
                        <li class="viewmore"><a href="messages.html">Ver mas mensajes</a></li>
                    </ul>
                </li>
                <li>
                    <a class="dropdown-toggle" data-toggle="dropdown" data-target="#">
                    <span class="count">10</span>
                    <span class="head-icon head-event"></span>
                    <span class="headmenu-label">Eventos</span>
                    </a>
                    <ul class="dropdown-menu newusers">
                        <li class="nav-header">Eventos</li>
                        <li>
                            <a href="">
                                <img src="images/photos/thumb1.png" alt="" class="userthumb" />
                                <strong>Draniem Daamul</strong>
                                <small>April 20, 2013</small>
                            </a>
                        </li>
                        <li>
                            <a href="">
                                <img src="images/photos/thumb2.png" alt="" class="userthumb" />
                                <strong>Shamcey Sindilmaca</strong>
                                <small>April 19, 2013</small>
                            </a>
                        </li>
                        <li>
                            <a href="">
                                <img src="images/photos/thumb3.png" alt="" class="userthumb" />
                                <strong>Nusja Paul Nawancali</strong>
                                <small>April 19, 2013</small>
                            </a>
                        </li>
                        <li>
                            <a href="">
                                <img src="images/photos/thumb4.png" alt="" class="userthumb" />
                                <strong>Rose Cerona</strong>
                                <small>April 18, 2013</small>
                            </a>
                        </li>
                        <li>
                            <a href="">
                                <img src="images/photos/thumb5.png" alt="" class="userthumb" />
                                <strong>John Doe</strong>
                                <small>April 16, 2013</small>
                            </a>
                        </li>
                    </ul>
                </li>
                <li class="odd">
                    <a class="dropdown-toggle" data-toggle="dropdown" data-target="#">
                    <span class="count">1</span>
                    <span class="head-icon head-archive"></span>
                    <span class="headmenu-label">Archivos</span>
                    </a>
                    <ul class="dropdown-menu">
                        <li class="nav-header">Archivos</li>
                        <li><a href=""><span class="icon-align-left"></span> New Reports from <strong>Products</strong> <small class="muted"> - 19 hours ago</small></a></li>
                        <li><a href=""><span class="icon-align-left"></span> New Statistics from <strong>Users</strong> <small class="muted"> - 2 days ago</small></a></li>
                        <li><a href=""><span class="icon-align-left"></span> New Statistics from <strong>Comments</strong> <small class="muted"> - 3 days ago</small></a></li>
                        <li><a href=""><span class="icon-align-left"></span> Most Popular in <strong>Products</strong> <small class="muted"> - 1 week ago</small></a></li>
                        <li><a href=""><span class="icon-align-left"></span> Most Viewed in <strong>Blog</strong> <small class="muted"> - 1 week ago</small></a></li>
                        <li class="viewmore"><a href="charts.html">View More Statistics</a></li>
                    </ul>
                </li>
                <li>
                    <div class="userloggedinfo">
                        <img id="imgusr" src="images/micse/Demostracion.jpg" style ="height :90px" alt="" />
                        <div class="userinfo">
                            <h5 id="datosusr"></h5>
                            <ul>
                                <li><a href="" id ="datosemp"></a></li>
                                <li><a href="editprofile.html">Configuración cuenta</a></li>                                
                                <li><a href="wfLogin.aspx">Cerrar Sesión</a></li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li class="right">
                    <img src="images/micse/avanza.png" alt="" />

                </li>
            </ul><!--headmenu-->
        </div>
    </div>
        <div class="leftpanel">
        
        <div class="leftmenu">        
            <ul id="sysmenu" class="nav nav-tabs nav-stacked">
            	
            </ul>
        </div><!--leftmenu-->
</div> 
        <div class="rightpanel">
        
        <ul class="breadcrumbs">
            <li id="hidemenu"><a href="javascript:HideMenu();"><i class="iconfa-arrow-left"></i></a><span>&nbsp;</span></li>
            <li><a href="dashboard.html"><i class="iconfa-arrow-up"></i></a><span>&nbsp;</span></li>
            <li><a href="dashboard.html"><i class="iconfa-home"></i></a> <span class="separator"></span></li>
            <li>Inicio</li>
            <li class="right">
                    <a href="" data-toggle="dropdown" class="dropdown-toggle"><i class="icon-tint"></i> Color Skins</a>
                    <ul class="dropdown-menu pull-right skin-color">
                        <li><a href="default">Default</a></li>
                        <li><a href="navyblue">Navy Blue</a></li>
                        <li><a href="palegreen">Pale Green</a></li>
                        <li><a href="red">Red</a></li>
                        <li><a href="green">Green</a></li>
                        <li><a href="brown">Brown</a></li>
                        <li><a href="micse">MICSE</a></li>
                    </ul>
            </li>
        </ul>
        
       <form id="form1" runat="server">
                <iframe id="icontent" src="wfMuro.aspx" style="width:100%;height:800px";>
                </iframe>
            
                
            
    
        </form>
        
        </div><!--rightpanel-->
    </div> 


    
</body>
</html>
