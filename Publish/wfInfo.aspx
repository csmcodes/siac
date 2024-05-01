<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfInfo.aspx.cs" Inherits="WebUI.wfInfo" %>

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
    <script type="text/javascript" src="js/jquery.cookie.js"></script> 
    <script type="text/javascript" src="js/general.js"></script>    
    <script type="text/javascript" src="js/forms/Info.js"></script>    
</head>
<body>
    <form id="form1" runat="server">
   <div class="errortitle">
        <h4 id="msg" class="animate0 fadeInUp">.</h4>
        <span class="animate1 bounceIn">4</span>
        <span class="animate2 bounceIn">0</span>
        <span class="animate3 bounceIn">4</span>
        <div class="errorbtns animate4 fadeInUp">
            <a onclick="history.back()" class="btn btn-primary btn-large">Ir a página anterior</a>            
        </div>
    </div>
    <div id="controles" style ="display :none">
        <asp:TextBox ID="txtmensaje" runat="server"></asp:TextBox>
    </div> 
    </form>
</body>
</html>
