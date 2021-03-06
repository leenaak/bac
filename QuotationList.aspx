<%@ Page Language="VB" AutoEventWireup="false" CodeFile="QuotationList.aspx.vb" Inherits="QuotationList" MasterPageFile="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 1029px">
    <center id="imgWait" style="display:none;">Please wait...<img id="Img1" src="images/spinner.gif" runat="server" /></center>
        <asp:Panel Width="25%" runat="server" ID="pnlSearch" Visible="true" CssClass="grey_border">
            <table width="100% border=0">
                <tr><td style="width:400px">Quotation </td></tr>
            </table>
        </asp:Panel>
        <br />
        <asp:LinkButton Id="btnAdd" runat="server" Text="Add New" PostBackUrl="~/Quotation_Maint.aspx?functionAU=A"></asp:LinkButton>
        &nbsp;&nbsp;&nbsp;
        <asp:LinkButton Id="LinkButton1" runat="server" Text="Back" OnClick="btnCancel_Click" ></asp:LinkButton>
        <br />
        <br />
        <br />
        
        <asp:Panel ID="pnlResult" runat="server" Visible="False" Width ="30%" CssClass="grey_border">
         <table width="100%"><tr><td>Quotation List</td></tr>
         </table></asp:Panel>         

        <br />
         <asp:Panel ID="pnlreviewed"  Width="100%" runat="server" Visible="FALSE">
            <asp:UpdatePanel ID="updItemPanel" runat="server"> 
                <ContentTemplate>
                    <asp:GridView ID="grdQuotationList" runat="server" AllowPaging="true" AllowSorting="False"  CssClass=requests_list"
                            AutoGenerateColumns="false" DataKeyNames="cust_qtn_rowid" SkinID="Professional" PageSize="10" RowStyle-CssClass ="row"
                             AlternatingRowStyle-CssClass ="row odd">
                            <HeaderStyle CssClass="heading" />                             
                        <Columns>
                           <asp:TemplateField HeaderText="" >
                                <ItemTemplate><asp:HyperLink Text="Edit" Width = "50px" runat="server" ID="lnkedit_item" NavigateUrl='<%# String.Format("~/Quotation_Maint.aspx?quotation_no={0}",Eval("qtn_no"))%>'></asp:HyperLink></ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Quotation No " >
                                <ItemTemplate><asp:Label ID="lblquotationno" runat="server" Width = "250px"  Text='<%# Eval("qtn_no") %>'></asp:Label></ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="" Visible="false" >
                                <ItemTemplate><asp:Label ID="lblcust_no" runat="server" Width = "250px"  Text='<%# Eval("cust_rowid") %>'></asp:Label></ItemTemplate>
                           </asp:TemplateField>
                        </Columns>
                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="Numeric"  Position="Bottom"   />  
                        <PagerStyle BackColor="Lavender" ForeColor="LightSlateGray"  />
                    </asp:GridView>
                <br />
                <asp:LinkButton Id="btncancel" runat="server" Text="Back" OnClick="btnCancel_Click" ></asp:LinkButton>
               <asp:Label ID="lblErrorMsg" runat="server" Text=""></asp:Label>
            </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        </div>   
</asp:Content>