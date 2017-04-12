<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Quotation_Maint.aspx.vb" Inherits="QuotationMaint" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <center id="imgWait" style="display:none;">Please wait...<img id="Img1" src="images/spinner.gif" runat="server" /></center>
        <asp:Panel Width="25%" runat="server" ID="pnlSearch" Visible="true" CssClass="grey_border">
            <table width="100% border=0">
                <tr><td style="width:400px">Search List of Items</td></tr>
            </table>
        </asp:Panel>
        <br />    
        <div>
            <div style="float:left;width:600px;height:200px; border:[][1][]">
                <table width="100%">
                <tr>
                    <td class="lbl_style" style ="width:150px">Quotation Date</td>
                    <td><asp:TextBox ID="ch_date" runat="server" Width="200px" CssClass="txtbox_style"></asp:TextBox></td>
                 </tr>
                 <tr>
                    <td class="lbl_style" style ="width:70px">Item Name</td>
                    <td><asp:TextBox ID="item_nameTextBox" runat="server" Width="350px" CssClass="txtbox_style"></asp:TextBox></td>
                    <td><asp:Button  ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click" CssClass="blue_button" Width="100px" /></td>
                </tr>
                </table>
                <br />
                <br />
                <br />
        <div>
                <asp:Panel ID="pnlQuotationList"  Width="100%" runat="server" Visible="FALSE">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
                <ContentTemplate>
                    <asp:GridView ID="grdQuotationList" runat="server" AllowPaging="false" AllowSorting="False"  CssClass="requests_list"
                    AutoGenerateColumns="false" SkinID="Professional" PageSize="150" RowStyle-CssClass ="row"
                    AlternatingRowStyle-CssClass ="row odd">
                    <HeaderStyle CssClass="heading" />                             
                    <Columns>
                        <asp:TemplateField HeaderText="Product" >
                        <ItemTemplate>
                            <asp:Label ID="lblproduct" runat="server" Width = "300px" Text='<%# Eval("product") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                        <asp:textbox ID="txtqty" visible='<%#Not CBool(IsInEditMode)%>' runat="server" Width = "75px" Text='<%# Eval("qty") %>' />
                            <asp:comparevalidator id="cvtxtqty" runat="server"  controltovalidate="txtqty" operator="DataTypeCheck" Display = "Dynamic" 
                                type="Integer" ErrorMessage="Numeric Required" ></asp:comparevalidator> 
                            <asp:RequiredFieldValidator ID="rfvtxtqty" runat="server" ControlToValidate="txtqty"  Display="Dynamic"
                                ValidationGroup="validateInsert" ErrorMessage="Quantity Required"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                        </asp:TemplateField>                                                                                                                                                                                                          

                        <asp:TemplateField HeaderText="Rate">
                        <ItemTemplate>
                        <asp:textbox ID="txtsellpr" runat="server" Width = "75px" Text='<%# Eval("sell_pr") %>' visible='<%#Not CBool(IsInEditMode)%>'></asp:textbox>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="OverRide Rate">
                        <ItemTemplate>
                        <asp:textbox ID="txttosellpr" runat="server" Width = "75px" Text='<%# Eval("over_pr") %>' visible='<%#Not CBool(IsInEditMode)%>'></asp:textbox>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="delete_CheckBox" runat="server" visible='<%#Not CBool(IsInEditMode)%>'/>
                            <input id="hdnISChanged" runat="server" type="hidden" />                    
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                        <asp:Label ID="lblsellpr" runat="server" Width = "75px" Text='<%# Eval("sell_pr") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                        <asp:Label ID="lbliprowid" runat="server" Width = "75px" Text='<%# Eval("ip_rowid") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                        <asp:Label ID="lblitemrowid" runat="server" Width = "75px" Text='<%# Eval("item_rowid") %>'></asp:Label>
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
                <table width="50%"><tr><td>
                <asp:Button ID="btnSave" Text="Submit" runat="server" OnClick="btnSave_Click" CssClass="blue_button" Visible="true" Width="100px"/></td>
                <td><asp:Button ID="btnPrint" Text="Print" runat="server" CssClass="blue_button"  Visible="true" Width="100px"/></td>
                <td><asp:LinkButton ID="btnBack" Text="Return to Quotation List" runat="server" Width="150px"></asp:LinkButton></td></tr></table>
        </div>         
            </div> 
              
            <div style="float:right;height:500px; border:[][1][]">
                <asp:Panel ID="pnlResult" runat="server" Visible="False" Width ="30%" CssClass="grey_border">
                    <table width="100%"><tr><td>Product List</td></tr>
                    </table>
                </asp:Panel>         

                <asp:Panel ID="pnlreviewed"  Width="100%" runat="server" Visible="FALSE">
                <asp:UpdatePanel ID="updItemPanel" runat="server"> 
                <ContentTemplate>
                    <asp:GridView ID="grdProductList" runat="server"  CssClass="requests_list"
                    AutoGenerateColumns="false" DataKeyNames="item_rowid" SkinID="Professional" PageSize="35" RowStyle-CssClass ="row"
                    AlternatingRowStyle-CssClass ="row odd" onrowcommand="grdProductList_RowCommand">
                    <HeaderStyle CssClass="heading" />                             
                    <Columns>
                        <asp:TemplateField HeaderText="" Visible="false"  >
                        <ItemTemplate>
                        <asp:label Text='<%#Left(Eval("item_rowid").ToString(),12) %>' Width ="75px" runat="server" ID="lblitemrowid" ></asp:label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" >
                        <ItemTemplate>
                        <asp:LinkButton Text="Add" Width = "50px" runat="server" ID="lnkadd_item" CommandName="Add"></asp:LinkButton>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="  " >
                        <ItemTemplate>
                        <asp:Label ID="lblproduct" runat="server" Width = "300px" Text='<%# Eval("product") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Avl Quantity">
                        <ItemTemplate>
                        <asp:Label ID="lblqty" runat="server" Width = "75px" Text='<%# Eval("qty") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                        <asp:Label ID="lblsellpr" runat="server" Width = "75px" Text='<%# Eval("sell_pr") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                        <asp:Label ID="lbliprowid" runat="server" Width = "75px" Text='<%# Eval("ip_rowid") %>'></asp:Label>
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
        </div>

    </div>   
</asp:Content>