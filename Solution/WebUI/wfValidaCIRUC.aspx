<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfValidaCIRUC.aspx.cs" Inherits="WebUI.wfValidaCIRUC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
    <div>
    <asp:TextBox ID="txtciruc" runat="server"></asp:TextBox>    
        <asp:Button ID="btnvalidar" runat="server" Text="Validar" 
            onclick="btnvalidar_Click" />
        <asp:Label ID="lblmensaje" runat="server" Text=""></asp:Label>
    
    </div>
    </form>
</body>
</html>
