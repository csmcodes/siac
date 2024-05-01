<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfPrintJS.aspx.cs" Inherits="WebUI.print.wfPrintJS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>

        jsPrintSetup.setPrinter('Adobe PDF');
        // set portrait orientation
        jsPrintSetup.setOption('orientation', jsPrintSetup.kPortraitOrientation);
        // set top margins in millimeters
        //jsPrintSetup.setOption('marginTop', 15);
        //jsPrintSetup.setOption('marginBottom', 15);
        //jsPrintSetup.setOption('marginLeft', 20);
        //jsPrintSetup.setOption('marginRight', 10);
        // set page header
        jsPrintSetup.setOption('headerStrLeft', 'ESTA SI');
        jsPrintSetup.setOption('headerStrCenter', '');
        jsPrintSetup.setOption('headerStrRight', '&PT');
        // set empty page footer
        jsPrintSetup.setOption('footerStrLeft', '');
        jsPrintSetup.setOption('footerStrCenter', 'A5');
        jsPrintSetup.setOption('footerStrRight', 'CSM');


        jsPrintSetup.definePaperSize(78, 78, "A5 Rotated 210 x 148 mm", "Custom_Paper", "Custom PAPER", 210, 148,
        jsPrintSetup.kPaperSizeMillimeters);
        jsPrintSetup.setPaperSizeData(78);

       

        //jsPrintSetup.setPaperSizeUnit(8);
        // clears user preferences always silent print value
        // to enable using 'printSilent' option
        //jsPrintSetup.clearSilentPrint();
        // Suppress print dialog (for this context only)
        //jsPrintSetup.setOption('printSilent', 1);
        // Do Print 
        // When print is submitted it is executed asynchronous and
        // script flow continues after print independently of completetion of print process! 
        jsPrintSetup.print();
        // next commands
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="content" runat ="server" >
    
    </div>
    </form>
</body>
</html>
