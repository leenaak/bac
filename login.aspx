<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="_Login"  MasterPageFile="~/MasterPage.master"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--    <h1>Administrator Login</h1>
--%>
    <h2>Welcome to the Bokil and Company</h2>
    <asp:Login ID="Login1" runat="server" UserNameLabelText="Employee Login:" 
        DisplayRememberMe="False" 
        UserNameRequiredErrorMessage="Employee Name is required.">
        <TextBoxStyle Width="220px" />
    </asp:Login>
</asp:Content>