<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfMuro.aspx.cs" Inherits="WebUI.wfMuro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" href="css/style.default.css" type="text/css" />        
    <link rel="stylesheet" href="css/responsive-tables.css"/>
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
    <script type="text/javascript" src="js/chosen.jquery.min.js"></script>
    <script type="text/javascript" src="js/functions.js"></script>
    <script type="text/javascript" src="js/general.js"></script>    
    <script type="text/javascript" src="js/forms/Muro.js"></script>    
    
    <!--[if lte IE 8]><script language="javascript" type="text/javascript" src="js/excanvas.min.js"></script><![endif]-->    
</head>
<body>    
 <div class="pageheader">
            <form action="results.html" method="post" class="searchbar">
                <input type="text" name="keyword" placeholder="Buscar, escriba y presione enter..." />
            </form>
            <div class="pageicon"><span class="iconfa-home"></span></div>
            <div class="pagetitle">
                <h5>Social</h5>
                <h1>Muro</h1>
            </div>
        </div><!--pageheader-->
     <div class="maincontent">
            <div class="maincontentinner">
                <div class="row-fluid">
                    <div id="dashboard-left" class="span8">
                        
                       
                        <ul class="userlist">
                            <li>                        
                            <div>
                                <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                <div class="uinfo">
                                    <h5>Administrador</h5>
                                    <span class="pos">Sectores Estratégicos en la Feria Socio País</span>
                                    <span>Agregó una nueva foto hoy a las 10:45 am</span>
                                </div>
                            </div>
                            <br />
                            <div class ="fotomuro"><img src ="images/micse/foto1.jpg" /></div>      
                                <br />
                                <ul class="list-unordered comentarios">
                                <li>                        
                                <div>
                                    <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                    <div class="uinfo">
                                        <h5>Director Regional</h5>
                                        <span class="pos">Vamos por buen camino</span>
                                        <span>Hoy a las 10:48 am</span>
                                    </div>
                                </div>                                
                                </li>
                                <li>                        
                                <div>
                                    <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                    <div class="uinfo">
                                        <h5>Maria Perez</h5>
                                        <span class="pos">Yo estuve ahi, excelente organización</span>
                                        <span>Hoy a las 10:47 am</span>
                                    </div>
                                </div>                                
                                </li>
                                <li>                        
                                <div>
                                    <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                    <div class="uinfo">
                                        <h5>Administrador</h5>
                                        <span class="pos">
                                            <input type ="text" class="newcomment" placeholder ="Escriba un comentario..." />
                                        </span>
                                        
                                    </div>
                                </div>                                
                                </li>
                                </ul>

                            
                            
                                              
                            </li>
                            <li>
                            <div>
                                <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                <div class="uinfo">
                                    <h5>Director Regional</h5>
                                    <span class="pos">Sectores Estratégicos en la Feria Socio País</span>
                                    <span>Agregó una nueva foto hoy a las 10:35 am</span>
                                </div>
                            </div>
                            <br />
                            <div class ="fotomuro"><img src ="images/micse/foto2.jpg" /></div>     
                            
                                <br />
                                <ul class="list-unordered comentarios">
                                <li>                        
                                <div>
                                    <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                    <div class="uinfo">
                                        <h5>Juan Ramirez</h5>
                                        <span class="pos">Bravooo!!!!</span>
                                        <span>Hoy a las 10:36 am</span>
                                    </div>
                                </div>                                
                                </li>                                                                
                                <li>                        
                                <div>
                                    <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                    <div class="uinfo">
                                        <h5>Administrador</h5>
                                        <span class="pos">
                                            <input type ="text" class="newcomment" placeholder ="Escriba un comentario..." />
                                        </span>
                                        
                                    </div>
                                </div>                                
                                </li>
                                </ul>
                              
                            </li>
                            <li>
                                <div>
                                <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                <div class="uinfo">
                                    <h5>Administrador</h5>
                                    <span class="pos">Declaraciones de nuestro señor Presidente</span>
                                    <span>Compartió un video ayer a las 15:48 </span>
                                </div>
                            </div>
                            <br />
                            <iframe width="560" height="315" src="//www.youtube.com/embed/0Enjb2-4ReA" frameborder="0" allowfullscreen></iframe>
                                <br />
                                <ul class="list-unordered comentarios">
                                <li>                        
                                <div>
                                    <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                    <div class="uinfo">
                                        <h5>Director Austro</h5>
                                        <span class="pos"> Bienvenido Sr. Ministro</span>
                                        <span>Ayer a las 16:31</span>
                                    </div>
                                </div>                                
                                </li>
                                <li>                        
                                <div>
                                    <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                    <div class="uinfo">
                                        <h5>Maria Perez</h5>
                                        <span class="pos">Seguimos adelante...</span>
                                        <span>Ayer a las 16:15 </span>
                                    </div>
                                </div>                                
                                </li>
                                <li>                        
                                <div>
                                    <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                    <div class="uinfo">
                                        <h5>Administrador</h5>
                                        <span class="pos">
                                            <input type ="text" class="newcomment" placeholder ="Escriba un comentario..." />
                                        </span>
                                        
                                    </div>
                                </div>                                
                                </li>
                                </ul>


                            </li>
                            <li>
                                <div>
                                <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                <div class="uinfo">
                                    <h5>Viceprecidencia</h5>
                                    <span class="pos">El vicepresidente de Ecuador, Jorge Glas, reconoció la postura que asumieron los jefes de Estado asistentes en la XLV Cumbre del Mercosur (Mercosur) respecto al secuestro de la comitiva presidencial de Evo Morales por parte un grupo de naciones europeas.</span>
                                    <span>El 17 de Octubre del 2013 a las  8:11 am</span>
                                </div>
                                <br />
                                <ul class="list-unordered comentarios">                                
                                <li>                        
                                <div>
                                    <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                    <div class="uinfo">
                                        <h5>Administrador</h5>
                                        <span class="pos">
                                            <input type ="text" class="newcomment" placeholder ="Escriba un comentario..." />
                                        </span>
                                        
                                    </div>
                                </div>                                
                                </li>
                                </ul>
                            </div>                                                        
                            </li>
                            <li>
                                <div>
                                <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                <div class="uinfo">
                                    <h5>Juan Perez</h5>
                                    <span class="pos">Comunidad del Milenio Playas de Cuyabeno</span>
                                    <span>Agregó una nueva foto el 15 de Octubre del 2013 a las  18:24 </span>
                                </div>
                            </div>
                            <br />
                            <div class ="fotomuro"><img src ="images/micse/foto4.jpg" /></div>    
                            <br />
                                <ul class="list-unordered comentarios">
                                <li>                        
                                <div>
                                    <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                    <div class="uinfo">
                                        <h5>Mario Lopez</h5>
                                        <span class="pos">Ese dia la temperatura estaba muy alta..que calor jaja</span>
                                        <span>El 15 de Octubre a las 19:48 </span>
                                    </div>
                                </div>                                
                                </li>                               
                                <li>                        
                                <div>
                                    <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                    <div class="uinfo">
                                        <h5>Administrador</h5>
                                        <span class="pos">
                                            <input type ="text" class="newcomment" placeholder ="Escriba un comentario..." />
                                        </span>
                                        
                                    </div>
                                </div>                                
                                </li>
                                </ul> 
                            </li>
                            <li>
                                <div>
                                <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                <div class="uinfo">
                                    <h5>Raul Gonzales</h5>
                                    <span class="pos">Creo que esto deberiamos conocer todos los funcionarios publicos <a href="http://www.eldiario.ec/noticias-manabi-ecuador/109268-servidor-publico/">http://www.eldiario.ec/noticias-manabi-ecuador/109268-servidor-publico/</a> .</span>
                                    <span>El 10 de Octubre del 2013 a las  11:18 am</span>
                                </div>
                                <br />
                                    <ul class="list-unordered comentarios">
                                <li>                        
                                <div>
                                    <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                    <div class="uinfo">
                                        <h5>Maria Perez</h5>
                                        <span class="pos">Gracias por el link...</span>
                                        <span>Ayer a las 12:56 </span>
                                    </div>
                                </div>                                
                                </li>
                                <li>                                                                       
                                <div>
                                    <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                    <div class="uinfo">
                                        <h5>Administrador</h5>
                                        <span class="pos">
                                            <input type ="text" class="newcomment" placeholder ="Escriba un comentario..." />
                                        </span>
                                        
                                    </div>
                                </div>                                
                                </li>
                                </ul>
                            </div>                                                        
                            </li>
                            <li>
                                <div>
                                <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                <div class="uinfo">
                                    <h5>Presidencia</h5>
                                    <span class="pos">180 alcaldes expresan su respaldo a explotación del ITT</span>
                                    <span>Agregó una nueva foto el 10 de Octubre del 2013 a las  17:10 </span>
                                </div>
                            </div>
                            <br />
                            <div class ="fotomuro"><img src ="images/micse/foto5.jpg" /></div>     
                            <br />
                                <ul class="list-unordered comentarios">
                                <li>                        
                                <div>
                                    <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                    <div class="uinfo">
                                        <h5>Director Regional</h5>
                                        <span class="pos">Por un Ecuador libre de pobreza</span>
                                        <span>El 11 de Octubre a las 10:11 am</span>
                                    </div>
                                </div>                                
                                </li>
                                <li>                        
                                <div>
                                    <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                    <div class="uinfo">
                                        <h5>Viceprecidencia</h5>
                                        <span class="pos">Estamos contigo presidente...</span>
                                        <span>El 10 de Octubre a las 18:22 </span>
                                    </div>
                                </div>                                
                                </li>
                                <li>                        
                                <div>
                                    <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                    <div class="uinfo">
                                        <h5>Administrador</h5>
                                        <span class="pos">
                                            <input type ="text" class="newcomment" placeholder ="Escriba un comentario..." />
                                        </span>
                                        
                                    </div>
                                </div>                                
                                </li>
                                </ul>
                            </li>
                         </ul>                          
                                                                                                                                          
                        
                        <br />
                        
                        
                    </div><!--span8-->
                    
                    <div id="dashboard-right" class="span4">
                        
                        <h5 class="subtitle">Avisos</h5>
                        
                        <div class="divider15"></div>
                        
                        <div class="alert alert-block">
                              <button data-dismiss="alert" class="close" type="button">&times;</button>
                              <h4>Recuerde!</h4>
                              <p style="margin: 8px 0">Todos los funcionarios publicos deben presentar la declaración juramentada de sus bienes hasta este mes de Octubre</p>
                        </div><!--alert-->

                        <div class="alert alert-error">
                              <button data-dismiss="alert" class="close" type="button">&times;</button>
                              <h4>Pendiente!</h4>
                              <p>Presentar informe de avances hasta el día 21 de este mes.</p>
                            </div><!--alert-->
                        <div class="alert alert-info">
                              <button data-dismiss="alert" class="close" type="button">&times;</button>
                              <strong>Celebración!</strong> Hoy celebramos el cumpleaños de nuestro compañero Juan Perez, felicidades...
                            </div><!--alert-->
                        <br />
                        
                        <h4 class="widgettitle"><span class="icon-comment icon-white"></span> Noticias</h4>
                        <div class="widgetcontent nopadding">
                            <ul class="commentlist">
                                <li>
                                    <img src="images/micse/not1.jpg" alt="" class="pull-left" />
                                    <div class="comment-info">
                                        <h4><a href="">Los guayaquileños se informan sobre los proyectos estratégicos en la Feria Socio País</a></h4>
                                        <h5>08 de Octubre de 2013 - 20h53</h5>
                                        <p>Dentro de los festejos por los 193 años de Independencia de Guayaquil, el Ministerio Coordinador de los Sectores Estratégicos, los ministerios coordinados y las empresas adscritas participan en la Feria Socio País, que este año se lleva a cabo en el Recinto Ferial de Durán.</p>                                        
                                    </div>
                                </li>
                                <li>
                                    <img src="images/micse/not2.jpg" alt="" class="pull-left" />
                                    <div class="comment-info">
                                        <h4><a href="">Moradores de Intag respaldan al presidente Rafael Correa para que desarrolle el proyecto Llurimagua</a></h4>
                                        <h5>06 de Octubre de 2013 - 17h04</h5>
                                        <p>El presidente Rafael Correa, durante el Enlace Ciudadano, desde Peguche, provincia de Imbabura; recibió el respaldo de cientos de moradores de 21 comunidades de las parroquias: García Moreno, Peñaherrera, Apuela, Plaza Gutiérrez, Vacas Galindo y 6 de Julio de Cuellaje, para que se desarrolle el proyecto minero Llurimagua, a cargo de la Empresa Nacional Minera (Enami EP).</p>
                                    </div>
                                </li>
                                <li>
                                    <img src="images/micse/not3.jpg" alt="" class="pull-left" />
                                    <div class="comment-info">
                                        <h4><a href="">“La Comunidad del Milenio Playas de Cuyabeno, un ejemplo de la nueva Amazonía”</a></h4>
                                        <h5>01 de Octubre de 2013 - 13h49</h5>
                                        <p>La primera Comunidad del Milenio se inauguró en Playas de Cuyabeno. Decenas de representantes de las nacionalidades Waorani, Shuar, Achuar, Cofán, Secoya, Zápara, Sionas, Shiwiar y colonos de la provincia de Sucumbíos, llegaron hasta este punto en el que al menos 392 ciudadanos vivirán.</p>
                                        
                                    </div>
                                </li>
                                <li><a href="">Ver mas noticias</a></li>
                            </ul>
                        </div>
                                                
                        
                        <div class="tabbedwidget tab-primary">
                            <ul>
                                <li><a href="#tabs-1"><span class="iconfa-user"></span></a></li>
                                <li><a href="#tabs-2"><span class="iconfa-star"></span></a></li>
                                <li><a href="#tabs-3"><span class="iconfa-comments"></span></a></li>
                            </ul>
                            <div id="tabs-1" class="nopadding">
                                <h5 class="tabtitle">Usuarios conectados</h5>
                                <ul class="userlist">
                                    <li>
                                        <div>
                                            <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                            <div class="uinfo">
                                                <h5>Maria Perez</h5>
                                                <span class="pos">Directora Seccional</span>
                                                <span>Conectado desde: 04/20/2013 8:40PM</span>
                                            </div>
                                        </div>
                                    </li>
                                    <li>
                                        <div>
                                            <img src="images/photos/thumb2.png" alt="" class="pull-left" />
                                            <div class="uinfo">
                                                <h5>Juan Lopez</h5>
                                                <span class="pos">Director Regional </span>
                                                <span>Conectado desde: 04/20/2013 3:30PM</span>
                                            </div>
                                        </div>
                                    </li>
                                    <li>
                                        <div>
                                            <img src="images/photos/thumb3.png" alt="" class="pull-left" />
                                            <div class="uinfo">
                                                <h5>Administrador</h5>
                                                <span class="pos">Administrador Sistema</span>
                                                <span>Conectado desde: 04/19/2013 1:30AM</span>
                                            </div>
                                        </div>
                                    </li>
                                    <li>
                                        <div>
                                            <img src="images/photos/thumb4.png" alt="" class="pull-left" />
                                            <div class="uinfo">
                                                <h5>Pedro Lazo</h5>
                                                <span class="pos">Sistemas MICSE</span>
                                                <span>Conectado desde: 04/19/2013 11:30AM</span>
                                            </div>
                                        </div>
                                    </li>
                                   
                                </ul>
                            </div>
                            <div id="tabs-2" class="nopadding">
                                <h5 class="tabtitle">Favoritos</h5>
                                <ul class="userlist userlist-favorites">
                                                                        <li>
                                        <div>
                                            <img src="images/photos/thumb3.png" alt="" class="pull-left" />
                                            <div class="uinfo">
                                                <h5>Administrador</h5>
                                                <p class="link">
                                                    <a href=""><i class="iconfa-envelope"></i> Mensaje</a>                                                    
                                                </p>
                                            </div>
                                        </div>
                                    </li>
                                    <li>
                                        <div>
                                            <img src="images/photos/thumb4.png" alt="" class="pull-left" />
                                            <div class="uinfo">
                                                <h5>Director Regional</h5>
                                                <p class="link">
                                                    <a href=""><i class="iconfa-envelope"></i> Mensaje</a>                                                    
                                                </p>
                                            </div>
                                        </div>
                                    </li>
                                    <li>
                                        <div>
                                            <img src="images/photos/thumb5.png" alt="" class="pull-left" />
                                            <div class="uinfo">
                                                <h5>MICSE</h5>
                                                <p class="link">
                                                    <a href=""><i class="iconfa-envelope"></i> Mensaje</a>
                                                    
                                                </p>
                                            </div>
                                        </div>
                                    </li>
                                    <li>
                                        <div>
                                            <img src="images/photos/thumb1.png" alt="" class="pull-left" />
                                            <div class="uinfo">
                                                <h5>Maria Perez</h5>
                                                <p class="link">
                                                    <a href=""><i class="iconfa-envelope"></i> Mensaje</a>
                                                    
                                                </p>
                                            </div>
                                        </div>
                                    </li>
                                    <li>
                                        <div>
                                            <img src="images/photos/thumb2.png" alt="" class="pull-left" />
                                            <div class="uinfo">
                                                <h5>Fernando Rios</h5>
                                                <p class="link">
                                                    <a href=""><i class="iconfa-envelope"></i> Mensaje</a>
                                                    
                                                </p>
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div id="tabs-3" class="nopadding">
                                <h5 class="tabtitle">Conversasiones</h5>
                                <ul class="userlist">
                                    <li>
                                        <div>
                                            <img src="images/photos/thumb4.png" alt="" class="pull-left" />
                                            <div class="uinfo">
                                                <h5>Maria Perez</h5>
                                                <p class="par">Yo te envie los documentos el dia de ayer, revisalos...</p>
                                            </div>
                                        </div>
                                    </li>
                                    <li>
                                        <div>
                                            <img src="images/photos/thumb5.png" alt="" class="pull-left" />
                                            <div class="uinfo">
                                                <h5>MICSE</h5>
                                                <p class="par">Ok entonces yo envio esos datos </p>
                                            </div>
                                        </div>
                                    </li>
                                   
                                    <li>
                                        <div>
                                            <img src="images/photos/thumb3.png" alt="" class="pull-left" />
                                            <div class="uinfo">
                                                <h5>Administrador</h5>
                                                <p class="par">Pero debe existir alguna manera de ingresar esos datos...</p>
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div><!--tabbedwidget-->
                        
                        <br />
                                                
                    </div><!--span4-->
                </div><!--row-fluid-->
                
               <div class="footer">
                    <div class="footer-left">
                        <span>&copy; 2013. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados.</span>
                    </div>
                    <div class="footer-right">
                        <span>Desarrrollado por: <a href="http://gtec.com.ec/">GTEC</a></span>
                    </div>
                </div><!--footer-->
                
            </div><!--maincontentinner-->
        </div><!--maincontent-->
</body>
</html>
