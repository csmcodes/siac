<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfDocuments.aspx.cs" Inherits="WebUI.wfDocuments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css/style.default.css" type="text/css" />
<link rel="stylesheet" href="css/isotope.css" type="text/css" />
<script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
<script type="text/javascript" src="js/jquery-migrate-1.1.1.min.js"></script>
<script type="text/javascript" src="js/jquery-ui-1.9.2.min.js"></script>
<script type="text/javascript" src="js/modernizr.min.js"></script>
<script type="text/javascript" src="js/bootstrap.min.js"></script>
<script type="text/javascript" src="js/jquery.isotope.min.js"></script>
<script type="text/javascript" src="js/jquery.colorbox-min.js"></script>
<script type="text/javascript" src="js/jquery.cookie.js"></script>
    <script type="text/javascript" src="js/general.js"></script>    
    <script type="text/javascript" src="js/forms/Documents.js"></script>
</head>
<body>
         <div class="pageheader">
            <form action="results.html" method="post" class="searchbar">
               <input type="text" name="keyword" placeholder="Buscar, escriba y presione enter..." />
            </form>
            <div class="pageicon"><span class="iconfa-cloud"></span></div>
            <div class="pagetitle">
                <h5>Administrar Documentos, Fotos, Videos &amp; mas</h5>
                <h1>Nube</h1>
            </div>
        </div><!--pageheader-->
        
        <div class="maincontent">
            <div class="maincontentinner">
                <div class="mediamgr">
            	<div class="mediamgr_left">
                	<div class="mediamgr_head">
                    	<ul class="mediamgr_menu">
                        	<li><a class="btn prev prev_disabled"><span class="icon-chevron-left"></span></a></li>
                            <li><a class="btn next"><span class="icon-chevron-right"></span></a></li>
                            <li class="marginleft15"><a class="btn selectall"><span class="icon-check"></span> Seleccionar Todos</a></li>
                            <li class="marginleft15 newfoldbtn"><a class="btn newfolder" title="Agregar nueva carpeta"><span class="icon-folder-open"></span></a></li>
                            <li class="marginleft5 trashbtn"><a class="btn trash" title="Eliminar"><span class="icon-trash"></span></a></li>
                            <li class="marginleft15 filesearch">
                            	<form>
                            		<input type="text" id="filekeyword" class="filekeyword" placeholder="Buscar archivo aqui" />
                                </form>
                            </li>
                            <li class="right newfilebtn"><a href="" class="btn btn-primary">Subir nuevo archivo</a></li>
                        </ul>
                        <span class="clearall"></span>
                    </div><!--mediamgr_head-->
                    
                    <div class="mediamgr_category">
                    <ul id="mediafilter">
                        	<li class="current"><a href="all">Todos</a></li>
                            <li><a href="image">Imagenes</a></li>
                            <li><a href="video">Videos</a></li>
                            <li><a href="audio">Audio</a></li>
                            <li><a href="doc">Documentos</a></li>
                            <li class="right"><span class="pagenuminfo">Mostrando 1 - 20 de 69</span></li>
                        </ul>
                    </div><!--mediamgr_category-->
                    
                    <div class="mediamgr_content">
						
                        
                    	
                        <ul id="medialist" class="listfile">
                        	<li class="image">
                              <a href="ajax/image.html"><img src="images/thumbs/image1.png" alt="" /></a>
                            	<span class="filename">Image1.jpg</span>
                           </li>
                        	<li class="image">
                                <a href="ajax/image.html"><img src="images/thumbs/image2.png" alt="" /></a>
                            	<span class="filename">Image2.jpg</span>
                            </li>
                            <li class="doc">
                                <a href="ajax/doc.html"><img src="images/thumbs/doc.png" alt="" /></a>
                            	<span class="filename">Tutorial1.pdf</span>
                            </li>
                            <li class="video">
                                <a href="ajax/video.html"><img src="images/thumbs/video.png" alt="" /></a>
                            	<span class="filename">Movie1.avi</span>
                            </li>
                            <li class="audio">
                                <a href="ajax/audio.html"><img src="images/thumbs/audio.png" alt="" /></a>
                            	<span class="filename">Song1.mp3</span>
                            </li>
                            <li class="doc">
                                <a href="ajax/doc.html"><img src="images/thumbs/doc.png" alt="" /></a>
                            	<span class="filename">Tutorial2.pdf</span>
                            </li>
                            <li class="doc">
                                <a href="ajax/doc.html"><img src="images/thumbs/doc.png" alt="" /></a>
                            	<span class="filename">Tutorial3.pdf</span>
                            </li>
                            <li class="image">
                                <a href="ajax/image.html"><img src="images/thumbs/image3.png" alt="" /></a>
                            	<span class="filename">Image1.jpg</span>
                            </li>
                        	<li class="image">
                                <a href="ajax/image.html"><img src="images/thumbs/image4.png" alt="" /></a>
                            	<span class="filename">Image2.jpg</span>
                            </li>
                            <li class="doc">
                                <a href="ajax/doc.html" data-rel="doc"><img src="images/thumbs/doc.png" alt="" /></a>
                            	<span class="filename">Tutoria4.pdf</span>
                            </li>
                            <li class="video">
                                <a href="ajax/video.html"><img src="images/thumbs/video.png" alt="" /></a>
                            	<span class="filename">Movie1.avi</span>
                            </li>
                            <li class="audio">
                                <a href="ajax/audio.html"><img src="images/thumbs/audio.png" alt="" /></a>
                            	<span class="filename">Song1.mp3</span>
                            </li>
                            <li class="doc">
                                <a href="ajax/doc.html"><img src="images/thumbs/doc.png" alt="" /></a>
                            	<span class="filename">Tutorial5.pdf</span>
                            </li>
                            <li class="doc">
                                <a href="ajax/doc.html"><img src="images/thumbs/doc.png" alt="" /></a>
                            	<span class="filename">Tutorial6.pdf</span>
                            </li>
                            <li class="image">
                                <a href="ajax/image.html"><img src="images/thumbs/image5.png" alt="" /></a>
                            	<span class="filename">Image1.jpg</span>
                            </li>
                        	<li class="image">
                                <a href="ajax/image.html"><img src="images/thumbs/image6.png" alt="" /></a>
                            	<span class="filename">Image2.jpg</span>
                            </li>
                            <li class="doc">
                                <a href="ajax/doc.html"><img src="images/thumbs/doc.png" alt="" /></a>
                            	<span class="filename">Tutorial.pdf</span>
                            </li>
                            <li class="video">
                                <a href="ajax/video.html"><img src="images/thumbs/video.png" alt="" /></a>
                            	<span class="filename">Movie1.avi</span>
                            </li>
                            <li class="audio">
                                <a href="ajax/audio.html"><img src="images/thumbs/audio.png" alt="" /></a>
                            	<span class="filename">Song1.mp3</span>
                            </li>
                            <li class="doc">
                                <a href="ajax/doc.html"><img src="images/thumbs/doc.png" alt="" /></a>
                            	<span class="filename">Tutorial1.pdf</span>
                            </li>
                        </ul>
                        
                        <br class="clearall" />
                        
                    </div><!--mediamgr_content-->
                    
                </div><!--mediamgr_left -->
                
                <div class="mediamgr_right">
                	<div class="mediamgr_rightinner">
                        <h4>Browse Files</h4>
                        <ul class="menuright">
                        	<li class="current"><a href="">All Files</a></li>
                            <li><a href="">Deleted Files</a></li>
                            <li><a href="">Recently Added</a></li>
                            <li><a href="">Recently Viewed</a></li>
                            <li><a href="">New Folder</a></li>
                            <li><a href="">New Folder(2)</a></li>
                        </ul>
                    </div><!-- mediamgr_rightinner -->
                </div><!-- mediamgr_right -->
                <br class="clearall" />
            </div><!--mediamgr-->
            
            <div class="footer">
                    <div class="footer-left">
                        <span>&copy; 2013. Shamcey Admin Template. All Rights Reserved.</span>
                    </div>
                    <div class="footer-right">
                        <span>Designed by: <a href="http://themepixels.com/">ThemePixels</a></span>
                    </div>
                </div><!--footer-->
                
            </div><!--maincontentinner-->
        </div><!--maincontent-->
</body>
</html>
