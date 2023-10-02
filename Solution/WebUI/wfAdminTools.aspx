<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfAdminTools.aspx.cs" Inherits="WebUI.wfAdminTools" %>

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
    <script type="text/javascript" src="js/flot/jquery.flot.min.js"></script>
    <script type="text/javascript" src="js/flot/jquery.flot.resize.min.js"></script>
    <script type="text/javascript" src="js/responsive-tables.js"></script>
    <script type="text/javascript" src="js/chosen.jquery.min.js"></script>
    <script type="text/javascript" src="js/general.js"></script>    
    <script type="text/javascript" src="js/functions.js"></script>
    <script type="text/javascript" src="js/popup.js"></script>
    <script type="text/javascript" src="js/methods.js"></script>          
    <script type="text/javascript" src="js/forms/AdminTools.js"></script>    
    <!--[if lte IE 8]><script language="javascript" type="text/javascript" src="js/excanvas.min.js"></script><![endif]-->    
</head>
<body>
    <form id="form1" runat="server">
        </form>
     <div class="maincontent">
       <p class='stdformbutton'><button id='btnfixpersona' class='btn btn-primary'>Fix Clientes</button></p>

       <p class='stdformbutton'><button id='btnmayoriza' class='btn btn-primary'>Mayorizar All</button></p>  
       <p class='stdformbutton'><button id='btnreaccount' class='btn btn-primary'>Recontabilizar All FAC</button></p>    
       <input type="text" id="periodo" placeholder="periodo"/><input type="text" id="mes" placeholder="mes"/>
       <p class='stdformbutton'><button id='btnfixcomprobantes' class='btn btn-primary'>Fix comprobantes</button></p>       
         <p class='stdformbutton'><button id='btnfixdocumentos' class='btn btn-primary'>Fix documentos</button></p>       
         <p class='stdformbutton'><input type="button" id='btncanfac' class='btn btn-primary' value="Cancelar Facturas Cobro" /></p>       
         <p class='stdformbutton'><button id='btnimp' class='btn btn-primary'>Importar clientes</button></p>       
         <input type="text" id="codipla" placeholder="Cod planilla"/>
         <p class='stdformbutton'><button id='btnpla' class='btn btn-primary'>Cuadrar Planilla</button></p>       
         <p class='stdformbutton'><button id='btndca' class='btn btn-primary'>Cancelaciones Negativas</button></p>       

         <p class='stdformbutton'><button id='btnreaccountcom' class='btn btn-primary'>Recontabilizar Comprobantes</button></p>    

         <p class='stdformbutton'>             
             <input type="text" id="doctrans" placeholder="array doctrans"/>
             <button id='btnserie' class='btn btn-primary'>Serie Duplicados</button>

         </p>    
         <p class='stdformbutton'>             
             <input type="text" id="desde" placeholder="desde"/>
             <input type="text" id="hasta" placeholder="hasta"/>
             <button id='btngetcan' class='btn btn-primary'>Get Cancelaciones No Planilla</button>
             <input type="text" id="tipos" placeholder="tipos"/>
             <button id='btnremovedup' class='btn btn-primary'>Remove RET Duplicadas</button>
             <input type="text" id="ruc" placeholder="RUC"/>
             <button id='btnactualizadocs' class='btn btn-primary'>Actualizar Documentos</button>
             <button id='btnactualizacan' class='btn btn-primary'>Actualizar Cancelaciones</button>
             <input type="text" id="clave" placeholder="clave elec"/>
             <button id='btnfixretdup' class='btn btn-primary'>Remove RET Duplicadas CLAVE</button>
             

         </p>    
         <p class='stdformbutton'><button id='btnerrores' class='btn btn-primary'>Errores Planillas</button>
             <button id='btnclosecartera' class='btn btn-primary'>Cerrar Cartera Cli All</button>
             <button id='btnclosecartera1' class='btn btn-primary'>Cerrar Cartera Cli Not ALL </button>
         </p>    
         <div id="reshtml" class="">

         </div>
     </div><!--maincontentinner-->
    
</body>
</html>
