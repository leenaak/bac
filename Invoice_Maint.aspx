<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Invoice_Maint.aspx.vb" Inherits="InvoiceMaint" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <center id="imgWait" style="display:none;">Please wait...<img id="Img1" src="images/spinner.gif" runat="server" /></center>
        <asp:Panel Width="25%" runat="server" ID="pnlSearch" Visible="true" CssClass="grey_border">
<%--        BackColor="#78AAD3" ForeColor="White" BorderStyle="Solid">--%>
            <table width="100% border=0">
                <tr><td style="width:400px">Invoice Details</td></tr>
            </table>
        </asp:Panel>
        <br />
    
        <div>
            <asp:Panel ID="pnlChallanList"  Width="100%" runat="server" Visible="FALSE">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
            <ContentTemplate>
                <asp:GridView ID="grdInvoiceSplitList" runat="server" AllowPaging="true" AllowSorting="False"  CssClass="requests_list"
                AutoGenerateColumns="false" SkinID="Professional" PageSize="200" RowStyle-CssClass ="row"
                AlternatingRowStyle-CssClass ="row odd">
                <HeaderStyle CssClass="heading" />                             
                <Columns>
                    <asp:TemplateField HeaderText="Challan" >
                    <ItemTemplate>
                        <asp:Label ID="lblchallanno" runat="server" Width = "60px" Text='<%# Eval("challan_no") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Split" >
                    <ItemTemplate>
                    <asp:DropDownList ID="invoice_splitdddl" visible='<%#Not CBool(IsInEditMode)%>' Width = "100px" runat="server" Font-Bold="true" Font-Names="Arial Black">
                        <asp:ListItem Value = "Select Split"></asp:ListItem>
                        <asp:ListItem Value = "1"></asp:ListItem>
                        <asp:ListItem Value = "2"></asp:ListItem>
                        <asp:ListItem Value = "3"></asp:ListItem>
                        <asp:ListItem Value = "4"></asp:ListItem>
                        <asp:ListItem Value = "5"></asp:ListItem>
                        <asp:ListItem Value = "6"></asp:ListItem>
                        <asp:ListItem Value = "7"></asp:ListItem>
                        <asp:ListItem Value = "8"></asp:ListItem>
                        <asp:ListItem Value = "9"></asp:ListItem>
                        <asp:ListItem Value = "10"></asp:ListItem>
                    </asp:DropDownList>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <%--visible='<%#Not CBool(IsInEditMode)%>'--%>
<%--                    <asp:TemplateField HeaderText="Discount" >
                    <ItemTemplate>
                    <asp:DropDownList ID="discount_ddl" visible='<%#Not CBool(IsInEditMode)%>' Width = "75px" runat="server" Font-Bold="true" Font-Names="Arial">
                        <asp:ListItem Value = "Discount"></asp:ListItem>
                        <asp:ListItem Value = "Y"></asp:ListItem>
                        <asp:ListItem Value = "N"></asp:ListItem>
                    </asp:DropDownList>
                    </ItemTemplate>
                    </asp:TemplateField>
--%>
                    <asp:TemplateField HeaderText="Nett" >
                    <ItemTemplate>
                        <asp:Label ID="lbldiscount" runat="server" Width = "40px" Text='<%# Eval("nett_item") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Product" >
                    <ItemTemplate>
                        <asp:Label ID="lblproduct" runat="server" Width = "500px" Text='<%# Eval("product") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblqty" runat="server" Width = "75px" Text='<%# Eval("qty") %>' ></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>                                                                                                                                                                                                          

                    <asp:TemplateField HeaderText="VAT">
                    <ItemTemplate>
                    <asp:label ID="lblvat" runat="server" Width = "75px" Text='<%# Eval("vat") %>' ></asp:label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    
                    <asp:TemplateField HeaderText="Rate">
                    <ItemTemplate>
                    <asp:label ID="lblsellpr" runat="server" Width = "75px" Text='<%# Eval("sell_pr") %>' ></asp:label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Make">
                    <ItemTemplate>
                    <asp:label ID="lblmake" runat="server" Width = "150px" Text='<%# Eval("make") %>' ></asp:label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="txtsellpr" runat="server" Width = "75px" Text='<%# Eval("sell_pr") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lbliprowid" runat="server" Width = "75px" Text='<%# Eval("ip_rowid") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblitemtranrowid" runat="server" Width = "75px" Text='<%# Eval("item_tran_rowid") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="Numeric"  Position="Bottom"   />  
                <PagerStyle BackColor="Lavender" ForeColor="LightSlateGray"  />
                </asp:GridView>
            <br />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            <asp:label ID="lblMessage" MaxLength="5" CssClass="lbl_style" width="300px" runat="server" Text='' />
            </ContentTemplate>
            </asp:UpdatePanel>
            </asp:Panel>                
        </div>
        <div>
        <asp:Panel Width="25%" runat="server" ID="PanelSplitDet" CssClass="grey_border">
<%--        BackColor="#78AAD3" ForeColor="White" BorderStyle="Solid">--%>
            <table width="100% border=0">
                <tr><td style="width:400px">Invoice Split Details</td></tr>
            </table>
        </asp:Panel>

            <asp:Panel ID="pnlSplitList"  Width="100%" runat="server" Visible="True">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server"> 
            <ContentTemplate>
                <asp:GridView ID="grdSplitDet" runat="server" AllowPaging="True"  CssClass="requests_list"
                AutoGenerateColumns="False" SkinID="Professional" >
                <HeaderStyle CssClass="heading" />                             
                    <AlternatingRowStyle CssClass="row odd" />
                    <Columns>
                        <asp:TemplateField HeaderText="Split" meta:resourceKey="TemplateFieldResource4">
                            <ItemTemplate>
                                <asp:Label ID="lblinv_split_no" runat="server" 
                                    meta:resourceKey="lblinv_split_noResource1" Text='<%# Eval("inv_split_no") %>' 
                                    Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub Total Nett"> 
                            <ItemTemplate>
                                <asp:Label ID="lblinv_subtotal_wo_dis" runat="server" 
                                    Text='<%# Eval("inv_subtotal_wo_dis") %>' 
                                    Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub Total Discount" >
                            <ItemTemplate>
                                <asp:Label ID="lblinv_subtotal_w_dis" runat="server" 
                                    Text='<%# Eval("inv_subtotal_w_dis") %>' 
                                    Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="Sub Total" 
                            meta:resourceKey="TemplateFieldResource10">
                            <ItemTemplate>
                                <asp:Label ID="lblinv_total" runat="server" 
                                    meta:resourceKey="lblinv_totalResource1" Text='<%# Eval("inv_total") %>' 
                                    Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                <PagerSettings FirstPageText="First Page" LastPageText="Last Page"   />  
                <PagerStyle BackColor="Lavender" ForeColor="LightSlateGray"  />
                    <RowStyle CssClass="row" />
                </asp:GridView>
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            <asp:label ID="Label3" MaxLength="5" CssClass="lbl_style" width="300px" runat="server" Text='' />

            </ContentTemplate>
            </asp:UpdatePanel>
            </asp:Panel>                

        <div>
        
        </div>

        <div>
            <asp:Button ID="btnSave" Text="Submit" runat="server" OnClick="btnSave_Click" CssClass="blue_button" Visible="true" Width="100px"/>
            <asp:Button ID="btnDispSplits" Text="Dis Split" runat="server" OnClick="btnDisSplit_Click" CssClass="blue_button" Visible="true" Width="100px"/>
            <asp:Button ID="btnBack" Text="Back" runat="server" CssClass="blue_button" Visible="true" Width="100px"/>
        </div>
        </div> 
    </div>   
</asp:Content>