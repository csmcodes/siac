<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FAC.aspx.cs" Inherits="WebUI.templates.FAC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="FAC.css"/>


    
    <script type="text/javascript" src="../js/prefixfree.min.js"  ></script>
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>        
    <script type="text/javascript" src="../js/modernizr.min.js"></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../js/general.js"></script>    
    <script type="text/javascript" src="../js/methods.js"></script>    
    <script type="text/javascript" src="../js/functions.js"></script>
    <script type="text/javascript" src="FAC.js"></script>
</head>
<body>
    
   <div class='numero'>numero</div>
   <div class='numero copy' >numero</div>
   <div class="fechacrea">fechacrea</div>
   


   <div class = "labelizq fecha">FECHA:</div>
   <div class = "dataizq fecha">fecha</div>
   
   <div class = "labelizq cliente">CLIENTE:</div>
   <div class = "dataizq cliente">cliente</div>

    <div class = "labelizq ruc" >RUC:</div>
   <div class = "dataizq ruc">ruc</div>

    <div class = "labelizq direccion">DIRECCION:</div>
   <div class = "dataizq direccion">direccion</div>

    <div class = "labelizq telefono">TELEFONO:</div>
   <div class = "dataizq telefono">telefono</div>



   <div class = "labelder destinatario">DESTINATARIO:</div>
   <div class = "datader destinatario">destinatario</div>
   
   <div class = "labelder direcciondes" >DIRECCION:</div>
   <div class = "datader direcciondes">direcciondes</div>

    <div class = "labelder ciudaddes">CIUDAD:</div>
   <div class = "datader ciudaddes">ciudaddes</div>

    <div class = "labelder remitente" >REMITENTE:</div>
   <div class = "datader remitente" >remitente</div>


    <div class = "details cantidad">cantidad</div>
    <div class = "details descripcion">descripcion</div>
    <div class = "details peso">peso</div>
    <div class = "details valorunitario">valor unit</div>
    <div class = "details valortotal">valortotal</div>
          
                
                
    <div class = "labeldownizq bultos">TOTAL BULTOS:</div>
    <div class = "datadownizq bultos">bultos</div>
   
    <div class = "labeldownizq declarado">VALOR DECLARADO:</div>
    <div class = "datadownizq declarado">declarado</div>

    <div class = "labeldownizq guia" >GUIA REMISION:</div>
    <div class = "datadownizq guia medium">guia</div>    
    
    <div class = "labeldownizq lugar" >LUGAR ENTREGA:</div>
    <div class = "datadownizq lugar large">lugar</div>      

    <div class = "labeldownizq1  seguro" >SEGURO:</div>
    <div class = "datadownizq1 seguro">seguro</div>    
    
    <div class = "labeldownizq1 politica" >POLITICA:</div>
    <div class = "datadownizq1 politica">politica</div>      


    <div class = "labeldownder subtotal0">SUBTOTAL 0%:</div>
    <div class = "datadownder subtotal0">subtotal0</div>

    <div class = "labeldownder subtotal12">SUBTOTAL 12%:</div>
    <div class = "datadownder subtotal12">subtotal12</div>

    <div class = "labeldownder subtotal">SUBTOTAL:</div>
    <div class = "datadownder subtotal">subtotal</div>

    <div class = "labeldownder iva">IVA 12%:</div>
    <div class = "datadownder iva">iva</div>

    <div class = "labeldownder total">VALOR TOTAL:</div>
    <div class = "datadownder total">total</div>
  
 
    <div class = "usuario">usuario</div>
    <div class = "propietario">propietario</div>
    <div class = "conductor">conductor</div>

    <div class="area">
    </div>
    
    <form id="form1" runat="server">
    
   <div style="display:none">
    <asp:TextBox ID="txtcodigo" runat="server"></asp:TextBox>
   </div>
    
    </form>
    
</body>
</html>
