<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Report.aspx.vb" Inherits="iMeeting.Report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <asp:Label runat="server" Text="Rapport envoyé par mail avec succès!" ID="lblMail" Visible="False"></asp:Label>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="500px" Width="100%" SizeToReportContent="True" ZoomMode="PageWidth"></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
