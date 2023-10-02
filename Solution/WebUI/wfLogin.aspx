<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfLogin.aspx.cs" Inherits="WebUI.wfLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

     <link rel="stylesheet" href="css/Login.css" type="text/css" />
     <script type="text/javascript" src="js/prefixfree.min.js"  ></script>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery-ui.js"></script>     
    <script type="text/javascript" src="js/modernizr.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/jquery.jgrowl.js"></script>
    <script type="text/javascript" src="js/jquery.alerts.js"></script>
    <script type="text/javascript" src="js/jquery.cookie.js"></script>             
    <script type="text/javascript" src="js/general.js"></script>
    <script type="text/javascript" src="js/popup.js"></script>
    <script type="text/javascript" src="js/forms/login.js"></script>

</head>
<body >

        <div id ="area" >
                    
                    <div id="fondo">                        
                    </div>
                    <div id="logo">                                                
                    </div>
                    <div id="ecu">                                                
                    </div>
                    <div id="datos">
                         <div class="inputwrapper animate1 bounceIn">
                            <input type="text" name="username" id="username" placeholder="Ingrese nombre de usuario" />            
                        </div>
                        <div class="inputwrapper animate2 bounceIn">
                            <input type="password" name="password" id="password" placeholder="Ingrese contraseña" />
                        </div>           
                        <div class="inputwrapper animate4 bounceIn">                
                            <button id ="btnsignin" name="submit">Iniciar Sesion</button>
                        </div>
                        <div class="inputwrapper animate5 bounceIn">
                            <label><input type="checkbox" class="remember" name="signin" />Mantener la sesión iniciada</label>
                        </div>
                         <div class="inputwrapper login-alert">
                                <div class="alert alert-error">Nobre de usuario o contraseña invalida</div>
                            </div>
                    </div>
                 	
      
         </div>
   <div class="loginfooter">
    <p>&copy; 2016. Gestión Tecnológica GTEC Cía. Ltda. Todos los derechos reservados.</p>
    </div>
</body>
</html>
