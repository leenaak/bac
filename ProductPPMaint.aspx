<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ProductPPMaint.aspx.vb" Inherits="ProductPPMaint" %>
<%@ Register TagPrefix="custom" Namespace="myControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <asp:Panel Width="30%" runat="server" ID="pnlClientCriteria" Visible="True" CssClass="grey_border">
            <table width="100% border=0">
                <tr><td style="width:400px">Item Purchase Price</td></tr>
            </table>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div>
         <asp:Panel ID="pnlreviewed"  Width="100%" runat="server" Visible="TRUE">
            <asp:UpdatePanel ID="updItemPanel" runat="server"> 
                <ContentTemplate>
                    <table width="100% border=0">
                    <tr><td>
                    <asp:GridView ID="grdProductSellPr" runat="server" DataSourceID="dsItemSP"  AllowPaging="true" AllowSorting="False"  CssClass="requests_list"
                            AutoGenerateColumns="false" DataKeyNames="item_tran_rowid" SkinID="Professional" PageSize="10" RowStyle-CssClass ="row"
                             AlternatingRowStyle-CssClass ="rowodd" OnRowCommand="GridView1_RowCommand" OnRowDeleted="GridView1_RowDeleted" OnRowDeleting="GridView1_RowDeleting" >
                            <HeaderStyle CssClass="heading" />                             
                        <Columns>
                           <asp:TemplateField HeaderText="Purchase Price" >
                                <ItemTemplate>
                                    <asp:Label ID="lblpurchase_pr" runat="server" Width = "150px"  Text='<%# Eval("purchase_price") %>'></asp:Label>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Delete" HeaderStyle-Font-Underline="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" Width = "150px" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("item_tran_rowid") %>'></asp:LinkButton>
                                </ItemTemplate>
                           </asp:TemplateField>                           
                        </Columns>
                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="Numeric"  Position="Bottom"   />  
                        <PagerStyle BackColor="Lavender" ForeColor="LightSlateGray"  />
                    </asp:GridView>
                </td></tr>
                <tr><td>
               <asp:Label ID="Label1" runat="server" Text=""></asp:Label></td></tr>
               </table>
            </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel><br />
       <asp:Panel Width="30%" runat="server" ID="Panel1" Visible="True" CssClass="grey_border">
                <table width="100% border=0">
                    <tr><td style="width:400px">Update Purchase Price</td></tr>
                </table>
        </asp:Panel>        
    <table width="100%" border="1">
        <tr>
        <td align = "center" ><asp:Label ID="lblname" Text="ITEM NAME: " runat="server" Width="150px"></asp:Label></td><td align = "center" ><asp:Label ID="lblitem_name"  visible="True" Enabled="false" CssClass="txtbox_style" Width="250px" Height="100px" runat="server" ></asp:Label></td></tr>
        </tr>
        <tr>
        <td align = "center" ><asp:Label ID="lblmake" Text="MAKE: " runat="server" Width="150px"></asp:Label></td><td align = "center" ><asp:Label ID="txtitem_make"  visible="True" Enabled="false" CssClass="txtbox_style" Width="250px" runat="server" TextMode="MultiLine"></asp:Label></td></tr>
        </tr>        
        <tr><td align = "center" ><asp:Label ID="Label2" Text="PURCHASE PRICE: " runat="server" Width="150px"></asp:Label></td><td align = "center" ><asp:Textbox ID="txtAsell_price"  visible="True" CssClass="txtbox_style" Width="150px" runat="server" Text=''  MaxLength="4"/></td></tr>
        <tr>
        <td>
            <asp:Button id = "btnSubmit" runat = "server" Text="Submit" OnClick = "btnSubmit_click" CssClass="blue_button" Width="100px"/>
        </td>
        <td>
            <asp:Button id = "btnCancel" runat = "server" Text="Back"  CssClass="blue_button" PostBackUrl="~/ProductList.aspx" Width="100px"/>       
        </td>
        </tr>
     </table> 
      <asp:Label ID = "lblErrorMsg" runat = "server" text="" Width="100px" />
</div>
</ContentTemplate>
</asp:UpdatePanel>
        <asp:SqlDataSource ID="dsItemSP" runat="server" 
            ConnectionString="<%$ ConnectionStrings:cnAdmin %>" 
            ProviderName="<%$ ConnectionStrings:cnAdmin.ProviderName %>"
            SelectCommand="SELECT `item_master`.`item_name`, `item_tran`.`purchase_price` FROM `bac_invent`.`item_tran`
                           INNER JOIN `bac_invent`.`item_master` 
                           ON (`item_tran`.`item_rowid` = `item_master`.`item_rowid`) where `tran_type`='P'">
       </asp:SqlDataSource></asp:Content>