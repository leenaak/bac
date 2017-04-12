<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ProductMaint.aspx.vb" Inherits="ProductMaint" %>
<%--<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>--%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register TagPrefix="custom" Namespace="myControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel Width="50%" runat="server" ID="pnlClientCriteria" Visible="True" CssClass="grey_border">
       <table width="100%">
            <tr><td><b>Add/Edit Product Details</b></td></tr></table>
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
                <td><asp:textbox ID="txtitem_name" CssClass="txtbox_style" MaxLength="100" width="310px"  Height="100" runat="server" Text='<%# Bind("item_name") %>' TextMode="MultiLine"/></td>           
            </tr>
            <tr style="height:10px"></tr>
            <tr>
                <td class="lbl_style">Make</td>
                <td><asp:TextBox ID="txtmake" MaxLength="20" CssClass="txtbox_style" width="210px" runat="server" Text='<%# Bind("make") %>' /></td>         
            </tr>
            <tr style="height:10px"></tr>
            <tr>
                <td class="lbl_style">Volume</td>
                <td><asp:TextBox ID="txtvolume" MaxLength="12" CssClass="txtbox_style" width="100px" runat="server" Text='<%# Bind("volume") %>' /></td>         
            </tr>
            <tr style="height:10px"></tr>
            <tr>
                <td class="lbl_style">Unit</td>
                <td><asp:TextBox ID="txtunit_type" MaxLength="5" CssClass="txtbox_style" width="70px" runat="server" Text='<%# Bind("unit_type") %>' /></td>         
            </tr>
            <tr style="height:10px"></tr>
            <tr>
                <td class="lbl_style">Vat</td>
                <td><asp:DropDownList ID="ddlvat" MaxLength="5"  CssClass="txtbox_style" Width="120px" runat="server">
                    <asp:ListItem Text="Select VAT" Value=""></asp:ListItem>
                    <asp:ListItem Text="VAT Exempted -0" Value="0"></asp:ListItem>
                     <%--<asp:ListItem Text="VAT 5.5%-1" Value="1"></asp:ListItem>--%>
                     <%--<asp:ListItem Text="VAT 12.5%-2" Value="2"></asp:ListItem> --%>
                    <asp:ListItem Text="VAT 6.0%-1" Value="6.0"></asp:ListItem>
                    <asp:ListItem Text="VAT 13.5%-2" Value="13.5"></asp:ListItem>                                        
                </asp:DropDownList>
                <%--<asp:TextBox ID="txtvat" MaxLength="5" CssClass="txtbox_style" width="60px" runat="server" Text='<%# Bind("vat") %>' />--%>
                 <asp:comparevalidator id="cvtxtvat" runat="server" controltovalidate="ddlVAT" operator="DataTypeCheck" 
                        type="Double"  Display = "Dynamic" ErrorMessage="Numeric Value Required" ></asp:comparevalidator></td>                                       
                </td>         
            </tr>
            <tr style="height:10px"></tr>
            <tr>
                <td class="lbl_style">Min Stock</td>
                <td><asp:TextBox ID="txtstock_trigger_qty" MaxLength="4" width="50px" CssClass="txtbox_style" runat="server" Text='<%# Bind("stock_trigger_qty") %>' />
                    <asp:comparevalidator id="cvtxtstock_trigger_qty" runat="server" controltovalidate="txtstock_trigger_qty" operator="DataTypeCheck" 
                        type="Integer" Display = "Dynamic" ErrorMessage="Pin Code Numeric" ></asp:comparevalidator></td>                                       
            </tr>
            <tr style="height:10px"></tr>
            <tr>
                <td class="lbl_style">Nett </td>
                <td><asp:CheckBox ID="chknett" CssClass ="txtbox_style " runat ="server" checked='<%# Eval("nett_item").ToString().Equals("Y") %>' /></td>
<%--                <td><asp:TextBox ID="chknett" MaxLength="4" width="50px" CssClass="txtbox_style" runat="server" Text='<%# Bind("nett_item") %>' /></td>                                       
--%>            </tr>
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

       <%--DataSourceID="dsItemEntry"--%>    
<%--      SelectCommand="Select * from [Item_Master]">--%>
<%--        OnItemInserted="dsItemEntry_OnItemInserted"
        OnItemUpdated="dsItemEntry_OnItemUpdated"
        OnItemUpdating="dsItemEntry_OnItemUpdating"
        OnItemInserting="dsItemEntry_OnItemInserting">
           <InsertParameters>
            <asp:Parameter Name="item_name" Type="String" />
            <asp:Parameter Name="make" Type="String" />
            <asp:Parameter Name="volume" Type="String" />
            <asp:Parameter Name="unit_type" Type="String" />
            <asp:Parameter Name="vat" Type="Int32" />
            <asp:Parameter Name="stock_trigger_qty" Type="Int32" />
        </InsertParameters>        
        
        
--%>