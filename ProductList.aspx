<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProductList.aspx.vb" Inherits="ProductList" MasterPageFile="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 1029px;">
    <center id="imgWait" style="display:none;">Please wait...<img id="Img1" src="images/spinner.gif" runat="server" /></center>
        <asp:Panel Width="15%" runat="server" ID="pnlSearch" Visible="true" CssClass="grey_border">
            <table width="100% border=0">
                <tr><td style="width:400px">Search List of Items</td></tr>
            </table>
        </asp:Panel>
        <br />
        <table>
                <tr>
                    <td class="lbl_style" style ="width:100px">Item Name</td>
                    <td><asp:TextBox ID="item_nameTextBox" runat="server" Width="300px" CssClass="txtbox_style"></asp:TextBox></td>
                    <td><asp:Button  ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click" CssClass="blue_button" /></td>
                </tr>
        </table>
        <asp:LinkButton Id="btnAdd" runat="server" Text="Add New" PostBackUrl="~/ProductMaint.aspx?functionAU=A" ></asp:LinkButton>
        <br />
        <br />
        <br />
        
        <asp:Panel ID="pnlResult" runat="server" Visible="False" Width ="30%" CssClass="grey_border">
<%--        BackColor="#78AAD3" ForeColor="White" BorderStyle="Solid" --%>
         <table width="100%"><tr><td>Product List</td></tr>
         </table></asp:Panel>         

        <br />
         <asp:Panel ID="pnlreviewed"  Width="100%" runat="server" Visible="FALSE">
            <asp:UpdatePanel ID="updItemPanel" runat="server"> 
                <ContentTemplate>
                    <asp:GridView ID="grdProductList" runat="server" AllowPaging="true" AllowSorting="False"  CssClass="requests_list"
                            AutoGenerateColumns="false" DataKeyNames="item_rowid" SkinID="Professional" PageSize="50" RowStyle-CssClass ="row"
                             AlternatingRowStyle-CssClass ="row odd">
                            <HeaderStyle CssClass="heading" />                             
                        <Columns>
                           <asp:TemplateField HeaderText="" >
                                <ItemTemplate>
                                    <asp:HyperLink Text="Edit" Width = "50px" runat="server" ID="lnkedit_item" NavigateUrl='<%# String.Format("~/ProductMaint.aspx?item_rowid={0}&functionAU=U", Eval("item_rowid"))%>'></asp:HyperLink>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="" >
                                <ItemTemplate>
                                    <asp:HyperLink Text="Sell Price" Width = "100px" runat="server" ID="lnksell_pr" NavigateUrl='<%# String.Format("~/ProductSPMaint.aspx?item_rowid={0}", Eval("item_rowid"))%>'></asp:HyperLink>
                                </ItemTemplate>
                           </asp:TemplateField>                           
                           <asp:TemplateField HeaderText="" >
                                <ItemTemplate>
                                    <asp:HyperLink Text="Purchase" Width = "100px" runat="server" ID="lnksell_pur" NavigateUrl='<%# String.Format("~/ProductPurchase.aspx?item_rowid={0}", Eval("item_rowid"))%>'></asp:HyperLink>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="" >
                                <ItemTemplate>
                                    <asp:HyperLink Text="Purchase Price" Width = "100px" runat="server" ID="lnkpur_pr" NavigateUrl='<%# String.Format("~/ProductPPMaint.aspx?item_rowid={0}", Eval("item_rowid"))%>'></asp:HyperLink>
                                </ItemTemplate>
                           </asp:TemplateField>                           
                           <asp:TemplateField HeaderText="Name" SortExpression="item_name" HeaderStyle-Font-Underline="true">
                                <ItemTemplate>
                                    <asp:Label ID="item_nameTextBox" runat="server" Width = "250px" Text='<%# Eval("item_name") %>'></asp:Label>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Make" SortExpression="make" >
                                <ItemTemplate>
                                    <asp:Label ID="item_makeTextBox" runat="server" Width = "175px" Text='<%# Eval("make") %>'></asp:Label>
                                </ItemTemplate>
                           </asp:TemplateField>
<%--                           <asp:TemplateField HeaderText="Volume" >
                                <ItemTemplate>
                                    <asp:Label ID="volumeTextBox" runat="server" Width = "100px" Text='<%# Eval("volume") %>'></asp:Label>
                                </ItemTemplate>
                           </asp:TemplateField>                                                              
                           <asp:TemplateField HeaderText="Unit" >
                                <ItemTemplate>
                                    <asp:Label ID="unit_typeTextBox" runat="server" Width = "100px" Text='<%# Eval("unit_type") %>'></asp:Label>                                    
                                </ItemTemplate>
                           </asp:TemplateField>--%>                                                      
                           <asp:TemplateField HeaderText="Nett" SortExpression="nett_item" HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="nettitem" runat="server" Width = "100px" Text='<%# Eval("nett_item") %>'></asp:Label>
                                </ItemTemplate>
                           </asp:TemplateField>                                                                                                                                                                                                          
                           <asp:TemplateField HeaderText="Avl Qty" SortExpression="qty" HeaderStyle-Font-Underline="true">
                                <ItemTemplate>
                                    <asp:Label ID="avlqtyTextBox" runat="server" Width = "100px" Text='<%# Eval("qty") %>'></asp:Label>
                                </ItemTemplate>
                           </asp:TemplateField>
                        </Columns>
                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="Numeric"  Position="Bottom"   />  
                        <PagerStyle BackColor="Lavender" ForeColor="LightSlateGray"  />
                    </asp:GridView>
                <br />
               <asp:Label ID="lblErrorMsg" runat="server" Text=""></asp:Label>
            </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        </div>   
</asp:Content>