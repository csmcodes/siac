<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfDcontablePrint.aspx.cs"
    Inherits="WebUI.wfDcontablePrint" UICulture="es-EC" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Button id="Button2"
           Text="Print"
           OnClick="print" 
           runat="server"/>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="100%" Width="100%" ZoomMode="PageWidth" SizeToReportContent="True">
    </rsweb:ReportViewer>
    <div style="display: none">
        <asp:TextBox ID="txtcodigocomp" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
