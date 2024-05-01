<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfCorreo.aspx.cs" Inherits="WebUI.wfCorreo" %>

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
    <script type="text/javascript" src="js/forms/Correo.js"></script>
   
</head>
<body>
   <div class="pageheader">
            <form action="results.html" method="post" class="searchbar">
                <input type="text" name="keyword" placeholder="Buscar, escriba y presione enter..." />
            </form>
            <div class="pageicon"><span class="iconfa-envelope"></span></div>
            <div class="pagetitle">
                <h5>Correo intero</h5>
                <h1>Mensajes</h1>
            </div>
        </div><!--pageheader-->
        
        <div class="maincontent">
            <div class="maincontentinner">
                <div class="messagepanel">
                    <div class="messagehead">
                        <button id="nuevomsg" class="btn btn-success btn-large">Nuevo Mensaje</button>
                    </div><!--messagehead-->
                    <div class="messagemenu">
                        <ul>
                            <li class="back"><a><span class="iconfa-chevron-left"></span> Back</a></li>
                            <li class="active"><a href=""><span class="iconfa-inbox"></span> Recibidos</a></li>
                            <li><a href=""><span class="iconfa-plane"></span> Enviados</a></li>
                            <li><a href=""><span class="iconfa-edit"></span> Borrador</a></li>
                            <li><a href=""><span class="iconfa-trash"></span> Eliminados</a></li>
                        </ul>
                    </div>
                    <div class="messagecontent">
                        <div class="messageleft">
                            <form class="messagesearch">
                                <input type="text" class="input-block-level" placeholder="Buscar mensaje..." />
                            </form>
                            <ul class="msglist">
                                <li class="selected" onclick ="ShowMessage(1)">
                                    <div class="thumb"><img src="images/photos/thumb1.png" alt="" /></div>
                                    <div class="summary">
                                        <span class="date pull-right"><small>April 03, 2013</small></span>
                                        <h4>Director Regional</h4>
                                        <p><strong>Comunicado Minis...</strong> - A todos los funcion...</p>
                                    </div>
                                </li>
                                <li class="unread" onclick ="ShowMessage(2)">
                                    <div class="thumb"><img src="images/photos/thumb2.png" alt="" /></div>
                                    <div class="summary">
                                        <span class="date pull-right"><small>April 03, 2013</small></span>
                                        <h4>Director Regional</h4>
                                        <p><strong>Invitación almu..</strong> - El dia 20 del..</p>
                                    </div>
                                </li>
                                <li onclick ="ShowMessage(3)">
                                    <div class="thumb"><img src="images/photos/thumb3.png" alt="" /></div>
                                    <div class="summary">
                                        <span class="date pull-right"><small>April 03, 2013</small></span>
                                        <h4>Maria Perez</h4>
                                        <p><strong>Documentos soli..</strong> - Estimado Juan te ...</p>
                                    </div>
                                </li>
                                <li onclick ="ShowMessage(4)">
                                    <div class="thumb"><img src="images/photos/thumb4.png" alt="" /></div>
                                    <div class="summary">
                                        <span class="date pull-right"><small>April 03, 2013</small></span>
                                        <h4>MICSE</h4>
                                        <p><strong>Información imp..</strong> - Estimados compañer...</p>
                                    </div>
                                </li>
                                <li class="unread" onclick ="ShowMessage(5)">
                                    <div class="thumb"><img src="images/photos/thumb5.png" alt="" /></div>
                                    <div class="summary">
                                        <span class="date pull-right"><small>April 03, 2013</small></span>
                                        <h4>Administrador</h4>
                                        <p><strong>Actualización sis..</strong> - El dia de mañan..</p>
                                    </div>
                                </li>
                                <li onclick ="ShowMessage(6)">
                                    <div class="thumb"><img src="images/photos/thumb6.png" alt="" /></div>
                                    <div class="summary">
                                        <span class="date pull-right"><small>April 03, 2013</small></span>
                                        <h4>Juan Aviles</h4>
                                        <p><strong>Datos sectores..</strong> - Juan te envio la inf.</p>
                                    </div>
                                </li>
                                <li onclick ="ShowMessage(7)">
                                    <div class="thumb"><img src="images/photos/thumb7.png" alt="" /></div>
                                    <div class="summary">
                                        <span class="date pull-right"><small>April 03, 2013</small></span>
                                        <h4>Veronica Leal</h4>
                                        <p><strong>Algo que debes..</strong> - El dia 10 de ener..</p>
                                    </div>
                                </li>
                                <li class="unread" onclick ="ShowMessage(8)">
                                    <div class="thumb"><img src="images/photos/thumb1.png" alt="" /></div>
                                    <div class="summary">
                                        <span class="date pull-right"><small>April 03, 2013</small></span>
                                        <h4>Jorge Torres</h4>
                                        <p><strong>Solicitud cont..</strong> - Solicito por favo..</p>
                                    </div>
                                </li>
                             
                                <li onclick ="ShowMessage(9)">
                                    <div class="thumb"><img src="images/photos/thumb7.png" alt="" /></div>
                                    <div class="summary">
                                        <span class="date pull-right"><small>April 03, 2013</small></span>
                                        <h4>Director</h4>
                                        <p><strong>Bienvenido Funcio..</strong> - Reciba mi mas..</p>
                                    </div>
                                </li>
                            </ul>
                        </div><!--messageleft-->
                        <div class="messageright">
                            <div class="messageview">
                                
                                <div class="btn-group pull-right">
                                    <button data-toggle="dropdown" class="btn dropdown-toggle">Acciones <span class="caret"></span></button>
                                    <ul class="dropdown-menu">
                                        <li><a href="#">Reponder</a></li>                                        
                                        <li><a href="#">Eliminar Mensaje</a></li>
                                        <li><a href="#">Imprimir Message</a></li>
                                        <li><a href="#">Marcar como no leido</a></li>
                                    </ul>
                                </div>
                                <div id="msgcontent">
                                <h1 class="subject">Comunicado Ministerio Coordinador de Sectores Estratégicos</h1>
                                <div class="msgauthor">
                                    <div class="thumb"><img src="images/photos/thumb1.png" alt="" /></div>
                                    <div class="authorinfo">
                                        <span class="date pull-right">April 03, 2012</span>
                                        <h5><strong>Director Regional</strong> <span>directorreg@sectoresestrategicos.gob.ec</span></h5>
                                        <span class="to">to admin@sectoresestrategicos.gob.ec</span>
                                    </div><!--authorinfo-->
                                </div><!--msgauthor-->
                                <div class="msgbody">
                                    <p>A todos los funcionarios publicos, se les comunica que</p>
                                    <p>El día de hoy miércoles 14 de noviembre de 2012, a las 11h00 (hora de Río de Janeiro), con la 
presencia del Ministro de Coordinación de los Sectores Estratégicos, Dr. Rafael Poveda Bonilla; y, 
del Embajador de la República del Ecuador en la República Federativa de Brasil, Horacio Sevilla, 
se suscribió el contrato para el financiamiento del Proyecto Hidroeléctrico Manduriacu entre la 
República del Ecuador y el Banco Nacional de Desarrollo de Brasil –BNDES-, por un valor de 90,2 
millones de dólares equivalentes al 72% del valor total del proyecto que asciende a 124,8 millones 
de dólares. 
El proyecto Hidroeléctrico Manduriacu está situado entre las provincias de Imbabura y Pichincha, 
es desarrollado por la empresa pública CELEC EP y es ejecutado por la constructora Odebrecht; 
tendrá una capacidad instalada de 60 MW, registra en la actualidad un 12,2% de avance en la 
ejecución de la obra y tiene su arranque de operaciones programado para el último trimestre de 
2014. 
En el año 2015, el Proyecto Hidroeléctrico Manduriacu evitará la emisión de 1,88 millones de 
toneladas de CO2 y la importación de combustibles para generación eléctrica por 24 millones de 
dólares. 
El Ministro Rafael Poveda expresó que “La suscripción de este instrumento ocurre dentro del 
marco de fortalecimiento de las relaciones entre Ecuador y Brasil y de profundización de los 
marcos de cooperación bilateral, abriendo nuevas oportunidades para ambos países.” </p>

<p>
DIRECCIÓN DE COMUNICACIÓN SOCIAL 
MINISTERIO DE COORDINACIÓN 
DE LOS SECTORES ESTRATÉGICOS 
Contacto: 02 2260670 ext. 420 - 425
</p>
                                </div><!--msgbody-->
                                
                                <div class="msgauthor">
                                    <div class="thumb"><img src="images/photos/thumb10.png" alt="" /></div>
                                    <div class="authorinfo">
                                        <span class="date pull-right">April 03, 2012</span>
                                        <h5><strong>Draneim Daamul</strong> <span>myemail@mydomain.com</span></h5>
                                        <span class="to">to his@hisdomain.com</span>
                                    </div><!--authorinfo-->
                                </div><!--msgauthor-->
                                <div class="msgbody">
                                    <p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas</p>
                                    
                                    <p>It aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt.</p>
                                    <p>- Draneim</p>
                                </div><!--msgbody-->
                                
                                <div class="msgauthor">
                                    <div class="thumb"><img src="images/photos/thumb1.png" alt="" /></div>
                                    <div class="authorinfo">
                                        <span class="date pull-right">April 03, 2012</span>
                                        <h5><strong>Leevanjo Sarce</strong> <span>hisemail@hisdomain.com</span></h5>
                                        <span class="to">to me@mydomain.com</span>
                                    </div><!--authorinfo-->
                                </div><!--msgauthor-->
                                <div class="msgbody">
                                    <p>It aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt.</p>
                                </div><!--msgbody-->
                                
                                <div class="msgauthor">
                                    <div class="thumb"><img src="images/photos/thumb10.png" alt="" /></div>
                                    <div class="authorinfo">
                                        <span class="date pull-right">April 03, 2012</span>
                                        <h5><strong>Draneim Daamul</strong> <span>myemail@mydomain.com</span></h5>
                                        <span class="to">to his@hisdomain.com</span>
                                    </div><!--authorinfo-->
                                </div><!--msgauthor-->
                                <div class="msgbody">                                    
                                    <p>Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur?</p>
                                </div><!--msgbody-->
                                </div>
                            </div><!--messageview-->
                            
                            <div class="msgreply">
                                <div class="thumb"><img src="images/photos/thumb1.png" alt="" /></div>
                                <div class="reply">
                                    <textarea placeholder="Type something here to reply"></textarea>
                                </div><!--reply-->
                            </div><!--messagereply-->
                            
                        </div><!--messageright-->
                    </div><!--messagecontent-->
                </div><!--messagepanel-->
                
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
