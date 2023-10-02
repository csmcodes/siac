<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfEstadocuentaPrint.aspx.cs"
    Inherits="WebUI.wfEstadocuentaPrint" UICulture="es-EC" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>   
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="14cm" Width="19cm">
    </rsweb:ReportViewer>
    <div style="display: none">
        <asp:TextBox ID="txtcodpersona" runat="server"></asp:TextBox>               
        <asp:TextBox ID="txtcodigoalm" runat="server"></asp:TextBox> 
        <asp:TextBox ID="txtfechacorte" runat="server"></asp:TextBox> 
        <asp:TextBox ID="txtdebcre" runat="server"></asp:TextBox>
     
    </div>
    </form>
</body>
</html>
