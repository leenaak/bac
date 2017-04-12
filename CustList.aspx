<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CustList.aspx.vb" Inherits="CustList" MasterPageFile="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 1029px">
    <center id="imgWait" style="display:none;">Please wait...<img id="Img1" src="images/spinner.gif" runat="server" /></center>
        <asp:Panel Width="25%" runat="server" ID="pnlSearch" Visible="true" CssClass="grey_border">
            <table width="100% border=0">
                <tr><td style="width:400px" >Search Customer</td></tr>
            </table>
        </asp:Panel>
        <br />
        <table>
                <tr>
                    <td class="lbl_style" style ="width:150px">File Number</td>
                    <td><asp:TextBox ID="filenumTextBox" runat="server" Width="300px" CssClass="txtbox_style"></asp:TextBox></td>
                </tr>

                <tr>
                    <td class="lbl_style" style ="width:150px">Customer Name</td>
                    <td><asp:TextBox ID="nameTextBox" runat="server" Width="300px" CssClass="txtbox_style" ></asp:TextBox></td>
                    <td><asp:Button  ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click" CssClass="blue_button" /></td>
                </tr>
        </table>
        <asp:LinkButton Id="btnAdd" runat="server" Text="Add New" PostBackUrl="~/CustMaint.aspx?functionAU=A" ></asp:LinkButton>
        <br />
        <br />
        <br />
        
        <asp:Panel ID="pnlResult" runat="server" Visible="False" Width ="30%" CssClass="grey_border">
<%--        BackColor="#78AAD3" ForeColor="White" BorderStyle="Solid" --%>
         <table width="100%"><tr><td>Customer List</td></tr>
         </table></asp:Panel>         

        <br />
         <asp:Panel ID="pnlreviewed"  Width="100%" runat="server" Visible="FALSE">
            <asp:UpdatePanel ID="updItemPanel" runat="server"> 
                <ContentTemplate>
                    <asp:GridView ID="grdCustomerList" runat="server" AllowPaging="true" AllowSorting="False"  CssClass="requests_list"
                            AutoGenerateColumns="false" DataKeyNames="cust_rowid" SkinID="Professional" PageSize="10" RowStyle-CssClass ="row"
                             AlternatingRowStyle-CssClass ="row odd">
                            <HeaderStyle CssClass="heading" />                             
                        <Columns>
                           <asp:TemplateField HeaderText="" >
                                <ItemTemplate><asp:HyperLink Text="Edit" Width = "40px" runat="server" ID="lnkedit_item" NavigateUrl='<%# String.Format("~/CustMaint.aspx?cust_rowid={0}&functionAU=U", Eval("cust_rowid"))%>'></asp:HyperLink></ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="" >
                                <ItemTemplate><asp:HyperLink Text="Challan" Width = "60px" runat="server" ID="lnkchallan" NavigateUrl='<%# String.Format("~/ChallanList.aspx?cust_rowid={0}", Eval("cust_rowid"))%>'></asp:HyperLink></ItemTemplate>
                           </asp:TemplateField>                           
                           <asp:TemplateField HeaderText="" >
                                <ItemTemplate><asp:HyperLink Text="Invoice" Width = "60px" runat="server" ID="lnkinvoice" NavigateUrl='<%# String.Format("~/InvoiceList.aspx?cust_rowid={0}", Eval("cust_rowid"))%>'></asp:HyperLink></ItemTemplate>
                           </asp:TemplateField>                           
                           <asp:TemplateField HeaderText="" >
                                <ItemTemplate><asp:HyperLink Text="Quotation" Width = "65px" runat="server" ID="lnkquotation" NavigateUrl='<%# String.Format("~/QuotationList.aspx?cust_rowid={0}", Eval("cust_rowid"))%>'></asp:HyperLink></ItemTemplate>
                           </asp:TemplateField>
                           
                           <asp:TemplateField HeaderText="No " >
                                <ItemTemplate><asp:Label ID="lblcustno" runat="server" Width = "75px" Text='<%# Eval("cust_no") %>'></asp:Label></ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Name " >
                                <ItemTemplate><asp:Label ID="lblname" runat="server" Width = "225px"  Text='<%# Eval("cust_name") %>'></asp:Label></ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Address" >
                                <ItemTemplate><asp:Label ID="lbladdress" runat="server" Width = "300px" Text='<%# Eval("address") %>'></asp:Label></ItemTemplate>
                           </asp:TemplateField>
<%--                           <asp:TemplateField HeaderText="Contact" >
                                <ItemTemplate><asp:Label ID="lblcontact" runat="server" Width = "100px" Text='<%# Eval("contact") %>'></asp:Label></ItemTemplate>
                           </asp:TemplateField>
--%>                           <asp:TemplateField HeaderText="Phone" >
                                <ItemTemplate><asp:Label ID="lblphone" runat="server" Width = "150px" Text='<%# Eval("phone") %>'></asp:Label></ItemTemplate>
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