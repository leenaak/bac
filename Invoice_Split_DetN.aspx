<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Invoice_Split_DetN.aspx.vb" Inherits="InvoiceSplitDet" MasterPageFile="~/MasterPage.master" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 376px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <center id="imgWait" style="display:none;">Please wait...<img id="Img1" src="images/spinner.gif" runat="server" /></center>
        
        <asp:Panel Width="25%" runat="server" ID="pnlSearch" CssClass="grey_border" 
            meta:resourcekey="pnlSearchResource1">            
            <table width="150% border=0">                
                <tr><td class="style1">Invoice Details</td>
                <td class="style1"><asp:Label ID="lblinvtxt" runat="server" Text="Last Cash Memo No:" Visible="false"></asp:Label>
                <asp:Label ID="lblinv_num" runat="server" Font-Bold="True" ForeColor="Red" Visible="false"   ></asp:Label></td>
                </tr>                
            </table>            
        </asp:Panel>
        <br />
<%--     <div class="left">
        <asp:DropDownList ID="ddlPrinterList" runat="server"></asp:DropDownList>
    </div>--%>
    <div>
            <asp:Panel ID="pnlChallanList"  Width="100%" runat="server" 
                meta:resourcekey="pnlChallanListResource1">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
            <ContentTemplate>

              <asp:GridView ID="grdInvoiceSplitList" runat="server" AllowPaging="True"  CssClass="requests_list"
                AutoGenerateColumns="False" SkinID="Professional" PageSize="25" 
                    DataKeyNames="inv_split_rowid" meta:resourceKey="grdInvoiceSplitListResource1">
                <HeaderStyle CssClass="heading" />                             
                    <AlternatingRowStyle CssClass="row odd" />
                    <Columns>
                        <asp:TemplateField meta:resourceKey="TemplateFieldResource1">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkprint_inv" runat="server" 
                                    CommandArgument='<%# Bind("inv_split_rowid") %>' CommandName="Print" 
                                    meta:resourceKey="lnkprint_invResource1" Text="Print" Width="100px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField meta:resourceKey="TemplateFieldResource2" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblinvoice_no_mstr" runat="server" 
                                    meta:resourceKey="lblinvoice_no_mstrResource1" 
                                    Text='<%# Eval("invoice_no_mstr") %>' Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField meta:resourceKey="TemplateFieldResource3" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblinv_split_rowid" runat="server" 
                                    meta:resourceKey="lblinv_split_rowidResource1" 
                                    Text='<%# Eval("inv_split_rowid") %>' Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Split" meta:resourceKey="TemplateFieldResource4">
                            <ItemTemplate>
                                <asp:Label ID="lblinv_split_no" runat="server" 
                                    meta:resourceKey="lblinv_split_noResource1" Text='<%# Eval("inv_split_no") %>' 
                                    Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice No" 
                            meta:resourceKey="TemplateFieldResource5">
                            <ItemTemplate>
                                <asp:TextBox ID="txtinvoice_no" runat="server" 
                                    meta:resourceKey="txtinvoice_noResource1" Text='<%# Eval("invoice_no") %>' 
                                    Visible="<%# Not CBool(IsInEditMode) %>" Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvinvoice_no" runat="server" 
                                    ControlToValidate="txtinvoice_no" Display="Dynamic" 
                                    ErrorMessage="Invoice No Required" meta:resourceKey="rfvinvoice_noResource1" 
                                    ValidationGroup="validateInsert"></asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice Text" 
                            meta:resourceKey="TemplateFieldResource6">
                            <ItemTemplate>
                                <asp:TextBox ID="txtinv_split_text" runat="server" 
                                    meta:resourceKey="txtinv_split_textResource1" 
                                    Text='<%# Eval("inv_split_text") %>' Visible="<%# Not CBool(IsInEditMode) %>" 
                                    Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvinv_split_text" runat="server" 
                                    ControlToValidate="txtinv_split_text" Display="Dynamic" 
                                    ErrorMessage="Invoice Text Required" 
                                    meta:resourceKey="rfvinv_split_textResource1" ValidationGroup="validateInsert"></asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice Date" 
                            meta:resourceKey="TemplateFieldResource7">
                            <ItemTemplate>
                                <asp:TextBox ID="txtinv_date" runat="server" 
                                    meta:resourceKey="txtinv_dateResource1" Text='<%# Eval("inv_date") %>' 
                                    Visible="<%# Not CBool(IsInEditMode) %>" Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtinv_date" runat="server" 
                                    ControlToValidate="txtinv_date" Display="Dynamic" 
                                    ErrorMessage="Invoice Date Required" meta:resourceKey="rfvtxtinv_dateResource1" 
                                    ValidationGroup="validateInsert"></asp:RequiredFieldValidator>
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
                        <asp:TemplateField HeaderText="Discount" 
                            meta:resourceKey="TemplateFieldResource11">
                            <ItemTemplate>
                                <asp:TextBox ID="txtinv_discount" runat="server" 
                                    meta:resourceKey="txtinv_discountResource1" Text='<%# Eval("inv_discount") %>' 
                                    Visible="<%# Not CBool(IsInEditMode) %>" Width="50px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VAT %" meta:resourceKey="TemplateFieldResource9">
                            <ItemTemplate>
                                <asp:Label ID="lblinv_vat_per" runat="server" 
                                    meta:resourceKey="lblinv_vat_perResource1" Text='<%# Eval("inv_vat_per") %>' 
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VAT " meta:resourceKey="TemplateFieldResource9">
                            <ItemTemplate>
                                <asp:Label ID="lblinv_vat_amt" runat="server" 
                                    meta:resourceKey="lblinv_vat_amtResource1" Text='<%# Eval("inv_vat_amt") %>' 
                                    Width="50px"></asp:Label>
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
                        <asp:TemplateField HeaderText="Transport" 
                            meta:resourceKey="TemplateFieldResource12">
                            <ItemTemplate>
                                <asp:TextBox ID="txtinv_transport" runat="server" 
                                    meta:resourceKey="txtinv_transportResource1" 
                                    Text='<%# Eval("inv_transport") %>' Visible="<%# Not CBool(IsInEditMode) %>" 
                                    Width="50px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Final Total"> 
                            <ItemTemplate>
                                <asp:Label ID="txtinv_final_total" runat="server" 
                                    Text='<%# Eval("inv_final_total") %>'  
                                    Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                <PagerSettings FirstPageText="First Page" LastPageText="Last Page"   />  
                <PagerStyle BackColor="Lavender" ForeColor="LightSlateGray"  />
                    <RowStyle CssClass="row" />
                </asp:GridView>
            <br />
            <asp:Label ID="Label1" runat="server" meta:resourceKey="Label1Resource1"></asp:Label>
            <asp:label ID="lblMessage" MaxLength="5" CssClass="lbl_style" width="300px" 
                    runat="server" meta:resourceKey="lblMessageResource1" />
            
            </ContentTemplate>
            </asp:UpdatePanel>
            </asp:Panel>                
            </div> 

          <div>
             <table style="width:400px" id="cashmemoaddr">
                <tr>
                    <td><asp:Label ID="lbldetails" runat="server" Text="Details" ></asp:Label></td>
                    <td><asp:TextBox ID="txtaddress1" MaxLength="45" CssClass="txtbox_style" width="400px" runat="server"/></td>           
                </tr>
                <tr>
                    <td class="lbl_style"></td>
                    <td><asp:TextBox ID="txtaddress2" MaxLength="45" CssClass="txtbox_style" width="400px" runat="server"/></td>         
                </tr>
                <tr>
                    <td class="lbl_style"></td>
                    <td><asp:TextBox ID="txtaddress3" MaxLength="45" CssClass="txtbox_style" width="400px" runat="server"/></td>         
                </tr>
                </table>
            <br />
            </div>

            <tr style="height:10px"></tr>
            <asp:Button ID="btnSave" Text="Submit" runat="server" OnClick="btnSave_Click" 
                CssClass="blue_button" meta:resourcekey="btnSaveResource1" Width="100px"/>
            <asp:Button ID="btnBack" Text="Back" runat="server" CssClass="blue_button" Width="100px"/>            
            
    </div>   
</asp:Content>