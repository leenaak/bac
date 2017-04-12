<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ProductPurchase.aspx.vb" Inherits="ProductPurchase" %>
<%--<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>--%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register TagPrefix="custom" Namespace="myControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel Width="50%" runat="server" ID="pnlClientCriteria" Visible="True" CssClass="grey_border">
       <table width="100%">
            <tr><td><b>Enter Quantity Purchased</b></td></tr></table>
       </asp:Panel>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
   <asp:repeater ID="rptProductMaint" runat="server" DataSourceID="dsItemEntry" OnItemCommand="Edit_Record">
        <ItemTemplate>
        <div Class="requests_list">
         <table style="width:600px">
            <tr style="height:10px"></tr>
            <tr>
                <td class="lbl_style" style="width:140px">Item Row Id</td>
                <td class="lbl_style" ><asp:Label ID="lblitem_rowid" CssClass="txtbox_style" runat="server" Text='<%# Bind("item_rowid") %>' /></td>
            </tr>
            <tr style="height:10px"></tr>
            <tr>    
                <td class="lbl_style">Item Name</td>
                <td class="lbl_style"><asp:Label ID="lblitem_name" CssClass="txtbox_style" width="310px"  runat="server" Text='<%# Bind("item_name") %>' /></td>           
            </tr>
            <tr style="height:10px"></tr>
            <tr>
                <td class="lbl_style">Make</td>
                <td class="lbl_style"><asp:Label ID="lblmake" CssClass="txtbox_style" width="210px" runat="server" Text='<%# Bind("make") %>' /></td>         
            </tr>
            <tr style="height:10px"></tr>
            <tr>
                <td class="lbl_style">Volume</td>
                <td class="lbl_style"><asp:Label ID="lblvolume" CssClass="txtbox_style" width="100px" runat="server" Text='<%# Bind("volume") %>' /></td>         
            </tr>
            <tr style="height:10px"></tr>
            <tr style="height:10px"></tr>
            <tr>
                 <td class="lbl_style">Purchase Type</td>
                 <td class="lbl_style"><asp:DropDownList ID="ddlpur_type" visible="True" runat="server" OnSelectedIndexChanged="ddlpur_type_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value = "Select BF/Pur"></asp:ListItem>
                        <asp:ListItem Value = "BF"></asp:ListItem>
                        <asp:ListItem Value = "P"></asp:ListItem>
                    </asp:DropDownList>
                  </td>         
            </tr>
            <tr style="height:10px"></tr>
            <tr>
                <td class="lbl_style">Purchase Quantity</td>
                <td><asp:TextBox ID="txtpur_qty" MaxLength="8" width="80px" CssClass="txtbox_style" runat="server" Text='' />
                    <asp:comparevalidator id="cvtxtpur_qty" runat="server" controltovalidate="txtpur_qty" operator="DataTypeCheck" 
                        type="Integer" Display = "Dynamic" ErrorMessage="Quantity Numeric" ></asp:comparevalidator></td>                                       
            </tr>
            <tr style="height:10px"></tr>
            <tr>
                <td class="lbl_style"><asp:Label ID="lblpurprice" runat="server"  Text="Purchase Price"></asp:Label></td>
                <td><asp:TextBox ID="txtpurchase_price" MaxLength="6" width="50px" CssClass="txtbox_style" runat="server" Text='' />
                    <asp:comparevalidator id="Comparevalidator1" runat="server" controltovalidate="txtpurchase_price" operator="DataTypeCheck" 
                        type="Double" Display = "Dynamic" ErrorMessage="Price Numeric" ></asp:comparevalidator></td>                                       
            </tr>
            <tr style="height:10px"></tr>
            </table>
                <br />
        </div>
        <asp:Button ID="btnInsert" CssClass="blue_button" runat="server" CommandName="Insert" Text="Submit"  />
        <asp:Button ID="btnCancel" CssClass="blue_button" runat="server" Text="Cancel" ToolTip="Cancel Operation" PostBackUrl="~/ProductList.aspx"  />
        </ItemTemplate>
    </asp:repeater>
    <asp:TextBox ID="lblMessage" MaxLength="5" CssClass="lbl_style" width="300px" runat="server" Text='' />
    </ContentTemplate>
    </asp:UpdatePanel>
        <asp:SqlDataSource ID="dsItemEntry" runat="server" 
            ConnectionString="<%$ ConnectionStrings:cnAdmin %>" 
            ProviderName="<%$ ConnectionStrings:cnAdmin.ProviderName %>" >
       </asp:SqlDataSource>
    </asp:Content>