<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InvoiceList.aspx.vb" Inherits="InvoiceList" MasterPageFile="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 1029px">
    <center id="imgWait" style="display:none;">Please wait...<img id="Img1" src="images/spinner.gif" runat="server" /></center>
        <asp:Panel Width="25%" runat="server" ID="pnlInvoice" Visible="true" CssClass="grey_border">
            <table width="100% border=0">
                <tr><td style="width:400px">Invoices </td></tr>
            </table>
        </asp:Panel>

  

        <asp:Panel Width="25%" runat="server" ID="Panel1" CssClass="grey_border">
            <table width="100% border=0">
                <tr><td style="width:400px">To be Invoiced </td>
                <td><asp:LinkButton Id="LinkButton1" runat="server" Text="Back" OnClick="btnCancel_Click" ></asp:LinkButton>
               <asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </asp:Panel>
         <asp:Panel ID="pnltobeinvoice"  Width="100%" runat="server" Visible="true" >
            <asp:UpdatePanel ID="updItemPanel" runat="server"> 
                <ContentTemplate>
                    <asp:GridView ID="grdInvoiceListNC" runat="server" AllowPaging="true" AllowSorting="False"  CssClass="requests_list"
                            AutoGenerateColumns="false" SkinID="Professional" PageSize="15" RowStyle-CssClass ="row"
                             AlternatingRowStyle-CssClass ="row odd">
                            <HeaderStyle CssClass="heading" />                             
                        <Columns>
                            <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><asp:CheckBox ID="select_CheckBox" runat="server" />
<%--                                <input id="hdnISChanged" runat="server" type="hidden" />                    --%>
                                </ItemTemplate>
                            </asp:TemplateField>
<%--                           <asp:TemplateField HeaderText="" >
                                <ItemTemplate><asp:HyperLink Text="Gen Invoice" Width = "100px" runat="server" ID="lnkedit_item" NavigateUrl='<%# String.Format("~/Invoice_Maint.aspx?challan_no={0}",Eval("challan_no"))%>'></asp:HyperLink></ItemTemplate>
                           </asp:TemplateField>
--%>                           <asp:TemplateField HeaderText="Challan No " >
                                <ItemTemplate><asp:Label ID="lblchallanno" runat="server" Width = "100px"  Text='<%# Eval("challan_no") %>'></asp:Label></ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Order Ref " >
                                <ItemTemplate><asp:Label ID="lblorderref"  runat="server" Width = "100px"  Text='<%# Eval("order_ref") %>'></asp:Label></ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Challan date " >
                                <ItemTemplate><asp:Label ID="lblchallandate" runat="server" Width = "50px"  Text='<%# Left(Eval("ch_created_date"),10) %>'></asp:Label></ItemTemplate>
                           </asp:TemplateField>

                        </Columns>
                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="Numeric"  Position="Bottom"   />  
                        <PagerStyle BackColor="Lavender" ForeColor="LightSlateGray"  />
                    </asp:GridView>
                <br />
                <%--<asp:LinkButton Id="btncancel" runat="server" Text="Back" OnClick="btnCancel_Click" ></asp:LinkButton>--%>
                <asp:Label ID="lblErrorMsg" runat="server" Text="" Font-Bold=true ></asp:Label>
                <br />
                <asp:Button ID="btnSave" Text="Gen Invoice" runat="server" OnClick="btnSave_Click" CssClass="blue_button" Visible="true" Width="100px"/>
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                <br />
            </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Panel Width="25%" runat="server" ID="Panel2" Visible="true" CssClass="grey_border">
            <table width="100% border=0">
                <tr><td style="width:400px">Invoiced </td></tr>
            </table>
        </asp:Panel>
         <asp:Panel ID="Panel3"  Width="100%" runat="server" Visible="true" >
            <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
                <ContentTemplate>
                    <asp:GridView ID="grdInvoiceListC" runat="server" AllowPaging="true" AllowSorting="False"  CssClass="requests_list"
                            AutoGenerateColumns="false" SkinID="Professional" PageSize="1500" RowStyle-CssClass ="row"
                             AlternatingRowStyle-CssClass ="row odd" onrowcommand="grdInvoiceListC_RowCommand">
                            <HeaderStyle CssClass="heading" />                             
                        <Columns>
                           <asp:TemplateField HeaderText="" >
                                <ItemTemplate><asp:HyperLink Text="Details" Width = "100px" runat="server" ID="lnkdet_item" NavigateUrl='<%# String.Format("~/Invoice_Split_DetN.aspx?invoice_no_mstr={0}",Eval("invoice_no_mstr"))%>'></asp:HyperLink></ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Challan No " >
                                <ItemTemplate><asp:Label ID="lblchallanno" runat="server" Width = "100px"  Text='<%# Eval("challan_no") %>'></asp:Label></ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="" >
                                <ItemTemplate><asp:Linkbutton Text="Delete" Width = "100px" runat="server" ID="lnkdel_item"></asp:linkbutton></ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Invoice No " >
                                <ItemTemplate><asp:Label ID="lblinvoiceno" runat="server" Width = "100px"  Text='<%# Eval("invoice_no_mstr") %>'></asp:Label></ItemTemplate>
                           </asp:TemplateField>

<%--                           <asp:TemplateField HeaderText="Challan date " >
                                <ItemTemplate><asp:Label ID="lblchallandate" runat="server" Width = "50px"  Text='<%# Left(Eval("ch_created_date"),10) %>'></asp:Label></ItemTemplate>
                           </asp:TemplateField>
--%>                        </Columns>
                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="Numeric"  Position="Bottom"   />  
                        <PagerStyle BackColor="Lavender" ForeColor="LightSlateGray"  />
                    </asp:GridView>
                <br />
            </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        </div>   
</asp:Content>