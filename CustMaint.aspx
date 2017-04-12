<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="~/CustMaint.aspx.vb"  Inherits="CustMaint" %>
<%--<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>--%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register TagPrefix="custom" Namespace="myControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel Width="50%" runat="server" ID="pnlClientCriteria" Visible="True" CssClass="grey_border">
       <table width="100%">
            <tr><td><b>Add/Edit Customer Details</b></td></tr></table>
       </asp:Panel>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
   <asp:repeater ID="rptCustMaint" runat="server" DataSourceID="dsCustEntry" OnItemCommand="Edit_Record">
        <ItemTemplate>
        <div Class="requests_list">
         <table style="width:600px">
            <tr style="height:10px"></tr>
            <tr>
                <td class="lbl_style" style="width:140px">Item Row Id</td>
                <td class="lbl_style" ><asp:Label ID="lblcust_rowid" CssClass="txtbox_style" runat="server" Text='<%# Bind("cust_rowid") %>' /></td>
            </tr>
            <tr style="height:10px"></tr>
            <tr>    
                <td class="lbl_style">Name</td>
                <td><asp:textbox ID="txtcust_name" CssClass="txtbox_style" MaxLength="50" width="300px"  runat="server" Text='<%# Bind("cust_name") %>' />
                    <asp:RequiredFieldValidator ID="rfvtxtcust_name" runat="server" ControlToValidate="txtcust_name" 
                        ValidationGroup="validateInsert" ErrorMessage="Name Required" Display="Dynamic"></asp:RequiredFieldValidator></td>           
            </tr>
            <tr style="height:10px"></tr>
            <tr>    
                <td class="lbl_style">Number</td>
                <td><asp:textbox ID="txtcust_no" CssClass="txtbox_style" MaxLength="50" width="300px"  runat="server" Text='<%# Bind("cust_no") %>' />
                    <asp:RequiredFieldValidator ID="rfvtxtcust_no" runat="server" ControlToValidate="txtcust_no" 
                        ValidationGroup="validateInsert" ErrorMessage="Name Required" Display="Dynamic"></asp:RequiredFieldValidator></td>           
            </tr>
            <tr style="height:10px"></tr>            <tr>
                <td class="lbl_style">Address</td>
                <td><asp:TextBox ID="txtaddress1" MaxLength="45" CssClass="txtbox_style" width="300px" runat="server" Text='<%# Bind("address1") %>' />
                    <asp:RequiredFieldValidator ID="rfvtxtaddress1" runat="server" ControlToValidate="txtaddress1" 
                        ValidationGroup="validateInsert" ErrorMessage="Name Required" Display="Dynamic"></asp:RequiredFieldValidator></td>           
            </tr>
            <tr>
                <td class="lbl_style"></td>
                <td><asp:TextBox ID="txtaddress2" MaxLength="45" CssClass="txtbox_style" width="300px" runat="server" Text='<%# Bind("address2") %>' /></td>         
            </tr>
            <tr>
                <td class="lbl_style">City</td>
                <td><asp:TextBox ID="txtcity" MaxLength="45" CssClass="txtbox_style" width="300px" runat="server" Text='<%# Bind("city") %>' /></td>
                    <asp:RequiredFieldValidator ID="rfvtxtcity" runat="server" ControlToValidate="txtcity" 
                        ValidationGroup="validateInsert" ErrorMessage="City Required" Display="Dynamic"></asp:RequiredFieldValidator></td>           
            </tr>
            <tr>
                <td class="lbl_style">District</td>
                <td><asp:TextBox ID="txtdistrict" MaxLength="45" CssClass="txtbox_style" width="300px" runat="server" Text='<%# Bind("district") %>' /></td>         
                    <asp:RequiredFieldValidator ID="rfvtxtdistrict" runat="server" ControlToValidate="txtdistrict" 
                        ValidationGroup="validateInsert" ErrorMessage="District Required" Display="Dynamic"></asp:RequiredFieldValidator></td>           
            </tr>
            <tr>
                <td class="lbl_style">State</td>
                <td><asp:TextBox ID="txtstate" MaxLength="45" CssClass="txtbox_style" width="300px" runat="server" Text='<%# Bind("state") %>' /></td>         
                    <asp:RequiredFieldValidator ID="rfvtxtstate" runat="server" ControlToValidate="txtstate" 
                        ValidationGroup="validateInsert" ErrorMessage="State Required" Display="Dynamic"></asp:RequiredFieldValidator></td>           
            </tr>
            <tr>
                <td class="lbl_style">Pin Code</td>
                <td><asp:TextBox ID="txtpin_code" MaxLength="6" width="60px" CssClass="txtbox_style" runat="server" Text='<%# Bind("pin_code") %>' />
                    <asp:RequiredFieldValidator ID="rfvtxtpin_code" runat="server" ControlToValidate="txtpin_code" 
                        ValidationGroup="validateInsert" ErrorMessage="Pin Code Required" Display="Dynamic"></asp:RequiredFieldValidator>           
                    <asp:comparevalidator id="cvtxtpin_code" runat="server" controltovalidate="txtpin_code" operator="DataTypeCheck" 
                        type="Integer" Display = "Dynamic" ErrorMessage="Pin Code Numeric" ></asp:comparevalidator></td>                                       
            </tr>
            <tr style="height:10px"></tr>
            <tr>
                <td class="lbl_style">Primary Contact</td>
                <td><asp:TextBox ID="txt_contact" MaxLength="45" CssClass="txtbox_style" width="300px" runat="server" Text='<%# Bind("contact") %>' /></td>
            </tr>
            <tr style="height:10px"></tr>
            <tr>
                <td class="lbl_style">Phone </td>
                <td><asp:TextBox ID="txtstd_code" MaxLength="5" CssClass="txtbox_style" width="40px" runat="server" Text='<%# Bind("std_code") %>' />
                    <asp:TextBox ID="txtphone" MaxLength="10" CssClass="txtbox_style" width="80px" runat="server" Text='<%# Bind("ph_no") %>' /></td>
                    <asp:RequiredFieldValidator ID="rfvtxtphone" runat="server" ControlToValidate="txtphone" 
                        ValidationGroup="validateInsert" ErrorMessage="Phone Required" Display="Dynamic"></asp:RequiredFieldValidator></td>           
            </tr>
            <tr style="height:10px"></tr>
        </table>
        <br />
        </div>
        <asp:Button ID="btnInsert" CssClass="blue_button" runat="server" Text="Submit"  />
        <asp:Button ID="btnCancel" CssClass="blue_button" runat="server" Text="Cancel" ToolTip="Cancel Operation" PostBackUrl="~/ProductList.aspx"  />
        </ItemTemplate>
    </asp:repeater>
    <asp:TextBox ID="lblMessage" MaxLength="5" CssClass="lbl_style" width="300px" runat="server" Text='' />
    </ContentTemplate>
    </asp:UpdatePanel>
        <asp:SqlDataSource ID="dsCustEntry" runat="server" 
            ConnectionString="<%$ ConnectionStrings:cnAdmin %>" 
            ProviderName="<%$ ConnectionStrings:cnAdmin.ProviderName %>" >
       </asp:SqlDataSource>
    </asp:Content>