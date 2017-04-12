<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CashMemo_Print.aspx.vb" Inherits="CashMemo_Print" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="azimuth:center;">
    <div>
       <%-- <asp:LinkButton ID="btnPrint" runat="server" Text="Print" ></asp:LinkButton>--%>
        <table border="1">
            <tr><td>
                <asp:Button id="btnConvert" runat="server" Text="Convert" />
                <asp:TextBox ID="txtMsg" runat="server" Text=""></asp:TextBox>
            </td></tr>
            <tr><td align="center">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ClientTarget="Auto" DisplayPage="true"
            BestFitPage="True" PrintMode="Pdf"/>
            </td></tr>
        </table>
    </div>
    </form>
</body>
</html>
