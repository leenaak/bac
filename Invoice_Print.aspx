<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Invoice_Print.aspx.vb" Inherits="Invoice_Print" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <%-- <asp:LinkButton ID="btnPrint" runat="server" Text="Print" ></asp:LinkButton>--%>
        <table border="1"><tr><td align="left">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ClientTarget="Auto" DisplayPage="true"
         BestFitPage="True" />
        </td></tr></table>
    </div>
    </form>
</body>
</html>
