<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ProductSPMaint.aspx.vb" Inherits="ProductSPMaint" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register TagPrefix="custom" Namespace="myControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <asp:Panel Width="30%" runat="server" ID="pnlClientCriteria" Visible="True" CssClass="grey_border">
            <table width="100% border=0">
                <tr><td style="width:400px">Item Sell Price</td></tr>
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
                            AutoGenerateColumns="false" DataKeyNames="ip_rowid" SkinID="Professional" PageSize="10" RowStyle-CssClass ="row"
                             AlternatingRowStyle-CssClass ="rowodd" OnRowCommand="GridView1_RowCommand" OnRowDeleted="GridView1_RowDeleted" OnRowDeleting="GridView1_RowDeleting" >
                            <HeaderStyle CssClass="heading" />                             
                        <Columns>
                           <asp:TemplateField HeaderText="Selling Price" >
                                <ItemTemplate>
                                    <asp:Label ID="lblsell_pr" runat="server" Width = "150px"  Text='<%# Eval("sell_pr") %>'></asp:Label>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="From" SortExpression="item_name" HeaderStyle-Font-Underline="true">
                                <ItemTemplate>
                                    <asp:Label ID="lbleff_from" runat="server" Width = "150px" Text='<%# Left(Eval("eff_from"),10) %>'></asp:Label>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Delete" HeaderStyle-Font-Underline="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" Width = "150px" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ip_rowid") %>'></asp:LinkButton>
                                </ItemTemplate>
                           </asp:TemplateField>
                           
<%--                           <asp:TemplateField HeaderText="To" SortExpression="make" >
                                <ItemTemplate>
                                    <asp:Label ID="lbleff_to" runat="server" Width = "150px" Text='<%# Left(Eval("eff_to"),10) %>'></asp:Label>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="UPDATED BY" SortExpression="updated_by" HeaderStyle-Font-Underline="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblupdt_by" runat="server" Width = "150px" Text='<%# Eval("updated_by") %>'></asp:Label>
                                </ItemTemplate>
                           </asp:TemplateField>                                                                                                                                                                                                          
--%>
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
                    <tr><td style="width:400px">Update Sell Price</td></tr>
                </table>
        </asp:Panel>        
    <table width="100%" border="1">
        <tr>
        <td align = "center" ><asp:Label ID="lblname" Text="ITEM NAME: " runat="server" Width="150px"></asp:Label></td><td align = "center" ><asp:Label ID="lblitem_name"  visible="True" Enabled="false" CssClass="txtbox_style" Width="250px" Height="100px" runat="server" ></asp:Label></td></tr>
        </tr>
        <tr>
        <td align = "center" ><asp:Label ID="lblmake" Text="MAKE: " runat="server" Width="150px"></asp:Label></td><td align = "center" ><asp:Label ID="txtitem_make"  visible="True" Enabled="false" CssClass="txtbox_style" Width="250px" runat="server" TextMode="MultiLine"></asp:Label></td></tr>
        </tr>        
        <tr><td align = "center" ><asp:Label ID="Label2" Text="SELL PRICE: " runat="server" Width="150px"></asp:Label></td><td align = "center" ><asp:Textbox ID="txtAsell_price"  visible="True" CssClass="txtbox_style" Width="150px" runat="server" Text=''  MaxLength="6"/></td></tr>
        <tr><td align ="right"><asp:Label ID="Label3" Text="EFFECTIVE FROM DATE: " runat="server" Width="200px"></asp:Label></td><td align = "center" ><asp:Textbox ID="txtAeff_from"  visible="True" CssClass="txtbox_style" Width="150px" runat="server" Text=''  MaxLength="10"/>
        <asp:ImageButton ImageAlign="AbsMiddle" Height="22px" Width="22px" runat="server" ID="btnCalEdit" AlternateText="Select a Date" ToolTip="Select a Date" ImageUrl="~/images/cal.png" />
            <cc1:CalendarExtender PopupButtonID="btnAeff_from" ID="Aeff_from_CalendarExtenderEdit" runat="server" Enabled="True" TargetControlID="txtAeff_from"></cc1:CalendarExtender>
               <asp:RequiredFieldValidator ID="rfvAeff_from" runat="server" ControlToValidate="txtAeff_from" ValidationGroup="validateEdit" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
        <td>
            <asp:Button id = "btnSubmit" runat = "server" Text="Submit"  OnClick = "btnSubmit_click" CssClass="blue_button" Width="100px"/>
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
            SelectCommand="SELECT `item_master`.`item_name`, `item_price`.`sell_pr`, `item_price`.`eff_from`
                           ,`item_price`.`eff_to`, `item_price`.`updated_by`  FROM `bac_invent`.`item_price`
                           INNER JOIN `bac_invent`.`item_master` 
                           ON (`item_price`.`item_rowid` = `item_master`.`item_rowid`)">
       </asp:SqlDataSource></asp:Content>