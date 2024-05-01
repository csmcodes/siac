<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfReportPrint.aspx.cs" Inherits="WebUI.reports.wfReportPrint" UICulture="es-EC" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Impresion Reporte</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
       
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="500px"  Width="100%"  Style="display: table !important; margin: 0px; overflow: scroll !important;" ZoomMode="PageWidth" SizeToReportContent="True" KeepSessionAlive="true">
        
    </rsweb:ReportViewer>
   <asp:Panel ID="Panel1" runat="server">
   </asp:Panel>
    <div style="display: none">
          
        <asp:TextBox ID="txtparameter1" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtparameter2" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtparameter3" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtparameter4" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtparameter5" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtparameter6" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtparameter7" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtparameter8" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtparameter9" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtparameter10" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtparameter11" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtparameter12" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtparameter13" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtparameter14" runat="server"></asp:TextBox>

    </div>
        
    </form>
</body>

</html>
