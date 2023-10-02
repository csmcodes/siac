<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfListaComprobantesPrint.aspx.cs" Inherits="WebUI.wfListaComprobantesPrint" UICulture="es-EC" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="500px"  Width="100%"  Style="display: table !important; margin: 0px; overflow: scroll !important;" ZoomMode="PageWidth" SizeToReportContent="True" KeepSessionAlive="true">

    </rsweb:ReportViewer>
    <div style="display: none">
        <asp:TextBox ID="txtcodigocomp" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>