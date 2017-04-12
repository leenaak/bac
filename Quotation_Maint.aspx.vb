Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data
Imports System.IO

Partial Class QuotationMaint
    Inherits System.Web.UI.Page
    Public Shared isSort As Boolean = False
    Public Shared isAscend As Boolean = False
    Private isEditMode As Boolean = False
    Private Const ASCENDING As String = " ASC"
    Private Const DESCENDING As String = " DESC"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then

                'If Session("emp_group_id") = 99 Or Session("emp_group_id") = 20 Then
                '    'attached trimmed menu for clients!
                DirectCast(Master.FindControl("xml"), OboutInc.SlideMenu.SlideMenu).XmlPath = "BacMenu.xml"
                BindQuotation(Request.QueryString("quotation_no"))
                Session("quotation_no") = Request.QueryString("quotation_no")
                'If Session("QuotationRCnt") > 0 Then
                '    btnSave.Visible = True
                'Else
                '    btnSave.Visible = False
                'End If
                '            pnlResult.Visible = False
                '            pnlreviewed.Visible = False
                'Else
                '    'Redirect to No access page.
                '    Session("emp_group_id") = ""
                '    Session("emp_id") = ""
                '    Response.Redirect("login.aspx")
                'End If
            End If

        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try
    End Sub

    Protected Property IsInEditMode() As Boolean
        Get
            Return Me.isEditMode
        End Get

        Set(ByVal value As Boolean)
            Me.isEditMode = value
        End Set
    End Property

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            BindVProduct("", "")
        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try

    End Sub

    Private Sub BindQuotation(ByVal quotation_no As String)
        Try
            Dim dt As New System.Data.DataTable
            dt = UserManager.GetQuotation(quotation_no)
            grdQuotationList.DataSource = dt
            grdQuotationList.DataBind()
            pnlQuotationList.Visible = True
            Session("Quotation") = dt
            Session("QuotationRCnt") = dt.Rows.Count
            'Dim chdate As Date = dt.Rows(0).Item("ch_created_date")
            'Quotation Date
            'ch_date.Text = chdate 'Day(chdate) & "/" & Month(chdate) & "/" & Year(chdate)
        Catch ex As Exception

        End Try
    End Sub


    Private Sub BindVProduct(ByVal sortExpr As String, ByVal sortDir As String)
        Try
            Dim dtview As DataView
            Dim ds As DataSet
            dtview = New DataView()
            grdProductList.DataSource = Nothing

            ds = UserManager.GetProduct_ForCh(item_nameTextBox.Text)
            dtview = ds.Tables(0).DefaultView
            If sortExpr <> "" Then
                dtview.Sort = String.Format(sortExpr, sortDir)
            End If

            grdProductList.DataSource = dtview
            grdProductList.DataBind()
            If grdProductList.Rows.Count > 0 Then
                pnlResult.Visible = True
                pnlreviewed.Visible = True
            Else
                lblErrorMsg.Text = "No matching records found"
            End If
        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try
    End Sub
    Private Sub BindVQuotation(ByVal sortExpr As String, ByVal sortDir As String)
        Try
            Dim quote_nos As String
            Dim dtview As DataView
            Dim ds As DataSet
            Dim dt As New System.Data.DataTable
            dtview = New DataView()
            grdQuotationList.DataSource = Nothing

            quote_nos = Request.QueryString("quotation_no")
            dt = UserManager.GetQuotation(quote_nos)
            ds = dt.DataSet
            dtview = ds.Tables(0).DefaultView
            If sortExpr <> "" Then
                dtview.Sort = String.Format(sortExpr, sortDir)
            End If

            grdProductList.DataSource = dtview
            grdProductList.DataBind()
            If grdProductList.Rows.Count > 0 Then
                pnlResult.Visible = True
                pnlreviewed.Visible = True
            Else
                lblErrorMsg.Text = "No matching records found"
            End If
        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try
    End Sub
    Protected Sub grdProductList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdProductList.PageIndexChanging
        Try
            grdProductList.PageIndex = e.NewPageIndex
            BindVProduct("", "")
        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try

    End Sub


    Protected Sub grdProductList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Dim dt As New System.Data.DataTable()
        Dim dcol As New DataColumn()
        Dim dcol1 As New DataColumn()
        Dim quotationcount As Integer
        Dim row As GridViewRow
        Dim drowadd As DataRow

        row = DirectCast(DirectCast(e.CommandSource, LinkButton).Parent.Parent, GridViewRow)

        quotationcount = Session("QuotationRCnt")
        If quotationcount = 0 Then
            drowadd = dt.NewRow()
            dcol = New DataColumn("product", GetType(System.String))
            dt.Columns.Add(dcol)
            dcol = New DataColumn("qty", GetType(System.String))
            dt.Columns.Add(dcol)
            dcol = New DataColumn("sell_pr", GetType(System.String))
            dt.Columns.Add(dcol)
            dcol = New DataColumn("over_pr", GetType(System.String))
            dt.Columns.Add(dcol)
            dcol = New DataColumn("ip_rowid", GetType(System.String))
            dt.Columns.Add(dcol)
            dcol = New DataColumn("item_rowid", GetType(System.String))
            dt.Columns.Add(dcol)
        Else
            dt = Session("Quotation")
            drowadd = dt.NewRow()
        End If

        drowadd("product") = TryCast(row.FindControl("lblproduct"), Label).Text
        drowadd("qty") = "0"
        drowadd("sell_pr") = TryCast(row.FindControl("lblsellpr"), Label).Text
        drowadd("over_pr") = "0.00"
        drowadd("ip_rowid") = TryCast(row.FindControl("lbliprowid"), Label).Text
        drowadd("item_rowid") = TryCast(row.FindControl("lblitemrowid"), Label).Text
        dt.Rows.Add(drowadd)

        Session("Quotation") = dt
        Session("QuotationRCnt") = dt.Rows.Count
        grdQuotationList.DataSource = dt
        grdQuotationList.DataBind()
        btnSave.Visible = True
        lblMessage.Text = " "

    End Sub


    Protected Sub grdProductList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProductList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim DropDownList1 = DirectCast(e.Row.FindControl("ddlpayment_vendor"), DropDownList)
            Dim hiddenField = DirectCast(e.Row.FindControl("hdnISChanged"), HtmlInputHidden)
            'DropDownList1.Attributes.Add("onchange", "document.getElementById('" + hiddenField.ClientID + "').value=1")
        End If

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Response.Redirect("~/ProductList.aspx")
        Catch ex As Exception
            Response.Write(Utils.ShowError(ex))
        End Try
    End Sub


    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim report As New ReportDocument
        Dim ds As New DataSet

        ds = UserManager.GetProduct_ForCh(item_nameTextBox.Text)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer
            Dim row As GridViewRow
            Dim dt As New System.Data.DataTable()
            Dim ds As DataSet
            ds = New DataSet
            Dim quotation_no As Integer
            Dim quotation_date As Date
            Dim cha_date As String


            quotation_date = ch_date.Text
            cha_date = Day(quotation_date) & "/" & Month(quotation_date) & "/" & Year(quotation_date)
            lblMessage.Text = " "
            If Session("quotation_no") = 0 Then
                ds = UserManager.GetMaxQuotation()
                quotation_no = ds.Tables(0).Rows(0).Item("maxquotation_no")
                UserManager.IDQuotation_M(quotation_no, Session("cust_rowid"))
                Session("quotation_no") = quotation_no
            Else
                quotation_no = Request.QueryString("quotation_no")
                If quotation_no = 0 Then
                    quotation_no = Session("quotation_no")
                End If
            End If

            'Delete quotation from table
            If quotation_no <> 0 Then
                UserManager.IDQuotation("D", "", "", "", "", quotation_no, cha_date)
            End If

            For i = 0 To (grdQuotationList.Rows.Count - 1)
                grdQuotationList.SelectedIndex = i
                row = grdQuotationList.SelectedRow
                If TryCast(row.FindControl("delete_CheckBox"), CheckBox).Checked Then
                Else
                    If CInt(TryCast(row.FindControl("txttosellpr"), TextBox).Text) = 0 Then
                        UserManager.IDQuotation("I", TryCast(row.FindControl("lblitemrowid"), Label).Text, TryCast(row.FindControl("lbliprowid"), Label).Text, _
                                    TryCast(row.FindControl("txtqty"), TextBox).Text, "0", _
                                    quotation_no, cha_date)
                    Else
                        UserManager.IDQuotation("I", TryCast(row.FindControl("lblitemrowid"), Label).Text, TryCast(row.FindControl("lbliprowid"), Label).Text, _
                                    TryCast(row.FindControl("txtqty"), TextBox).Text, TryCast(row.FindControl("txttosellpr"), TextBox).Text, _
                                    quotation_no, cha_date)
                    End If
                    lblMessage.Text = "Quotation Updated Successfully"
                End If
            Next
            BindVProduct("", "")
            BindQuotation(Session("quotation_no"))
        Catch ex As Exception

        End Try


    End Sub


    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        'Return to Quotation List
        Response.Redirect("~/QuotationList.aspx?cust_rowid=" & Session("cust_rowid"))
    End Sub

    Protected Sub btnPrint_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'Dim url As String = "Quotation_Print.aspx"
        'Dim fullURL As String = "window.open('" & url & "', '_blank', 'height=1000,width=1000,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,titlebar=no' );"
        'ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)


        Dim MyDA As New MySqlClient.MySqlDataAdapter()
        Dim MyCommand As New MySqlClient.MySqlCommand()

        Dim Myconstring As New MySqlClient.MySqlConnection()
        Myconstring.ConnectionString = "server=localhost;User Id=root;password=1907;Persist Security Info=True;database=bac_invent"
        MyCommand.Connection = Myconstring
        MyCommand.CommandText = "Select * from cust_master a, cust_qtn b where b.qtn_no = " & Session("quotation_no") & " and b.cust_rowid = a.cust_rowid"
        MyDA = New MySqlClient.MySqlDataAdapter()
        MyDA.SelectCommand = MyCommand

        Dim myDS As New DataSet1()
        'This is our DataSet created at Design Time      
        MyDA.Fill(myDS, "cust_master")

   
        'You have to use the same name as that of your Dataset that you created during design time
        ' This is the Crystal Report file created at Design Time
        Dim MyDA1 As New MySqlClient.MySqlDataAdapter()
        MyCommand.CommandText = "SELECT concat(c.item_name, '-', c.make, '-', c.volume, ' ', c.unit_type) as product, (-1 * b.qty)  as qty, if(c.nett_item='Y', 'Nett', ' ') as item_nett , " & _
                                " if(b.purchase_price = 0, d.sell_pr, b.purchase_price) as sell_pr, b.ip_rowid as ip_rowid, b.item_rowid,a.qtn_created_date,  " & _
                                " IF(e.mvat=0,' - ',e.mvat) AS vat FROM cust_qtn a, qtn_tran b, item_master c, item_price d, tbl_vat e  " & _
                                " where a.qtn_no = b.qtn_no and b.item_rowid = c.item_rowid and b.ip_rowid = d.ip_rowid AND c.vattype = e.vattype " & _
                                " and a.qtn_no = " & Session("quotation_no") & " AND CURDATE() BETWEEN e.stdate AND e.endate order by vat asc"

        MyDA1.SelectCommand = MyCommand
        MyDA1.Fill(myDS, "item_master")

        'myDS.Tables.Add("item_master")
        Dim report As New CrystalReport4
        report.SetDataSource(myDS)
        report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "E:\BAC_Soft\Pdf\qtn.pdf")
        'report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, "E:\BAC_Soft\Pdf\qtn.xls")
        Response.ContentType = "application/pdf'"
        'Response.ContentType = "application/xls"
        Dim file__1 As New FileInfo("E:\BAC_Soft\Pdf\qtn.pdf")
        'Dim file__1 As New FileInfo("E:\BAC_Soft\Pdf\qtn.xls")
        Dim len As Integer = CInt(file__1.Length), bytes As Integer
        Response.AppendHeader("content-length", len.ToString())
        Response.AddHeader("Content-Disposition", "attachment;filename=" + "E:\BAC_Soft\Pdf\qtn.pdf")
        'Response.AddHeader("Content-Disposition", "attachment;filename=" + "E:\BAC_Soft\Pdf\qtn.xls")
        Dim buffer As Byte() = New Byte(1023) {}
        Dim outStream As Stream = Response.OutputStream
        Using stream As Stream = File.OpenRead("E:\BAC_Soft\Pdf\qtn.pdf")
            'Using stream As Stream = File.OpenRead("E:\BAC_Soft\Pdf\qtn.xls")
            While len > 0 AndAlso (InlineAssignHelper(bytes, stream.Read(buffer, 0, buffer.Length))) > 0
                outStream.Write(buffer, 0, bytes)
                len -= bytes
            End While
        End Using
        ' Set the SetDataSource property of the Report to the Dataset
        'CrystalReportViewer1.ReportSource = report
        ' Set the Crystal Report Viewer's property to the oRpt Report object that we created
        'CrystalReportViewer1.Visible = True
        'report.PrintOptions.PrinterName = "HPLaserJ"
        'print all pages if 0,0
        'report.PrintToPrinter(1, False, 0, 0)
        'report.Clone()


    End Sub

    Protected Sub grdQuotationList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdQuotationList.PageIndexChanging
        Try
            grdQuotationList.PageIndex = e.NewPageIndex
            BindVQuotation("", "")
        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try
    End Sub

    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
        target = value
        Return value
    End Function
End Class
