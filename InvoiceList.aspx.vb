Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb

Partial Class InvoiceList
    Inherits System.Web.UI.Page
    Public Shared isSort As Boolean = False
    Public Shared isAscend As Boolean = False
    Private isEditMode As Boolean = False
    Private Const ASCENDING As String = " ASC"
    Private Const DESCENDING As String = " DESC"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
            'If Session("emp_group_id") = 99 Or Session("emp_group_id") = 20 Then
            '    'attached trimmed menu for clients!
                DirectCast(Master.FindControl("xml"), OboutInc.SlideMenu.SlideMenu).XmlPath = "BacMenu.xml"
            'pnltobeinvoice.Visible = False
                BindVInvoiceNC("", "")
                BindVInvoiceC("", "")
                lblErrorMsg.Text = " "
                'Else
            '    'Redirect to No access page.
            '    Session("emp_group_id") = ""
            '    Session("emp_id") = ""
            '    Response.Redirect("login.aspx")
            'End If

            End if

        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try
    End Sub

    Private Sub BindVInvoiceNC(ByVal sortExpr As String, ByVal sortDir As String)
        Try
            Dim dtview As DataView
            Dim ds As DataSet
            dtview = New DataView()
            grdInvoiceListNC.DataSource = Nothing
            Session("cust_rowid") = Request.QueryString("cust_rowid")

            ds = UserManager.GetInvoice_ListNC(Request.QueryString("cust_rowid"))
            dtview = ds.Tables(0).DefaultView
            If sortExpr <> "" Then
                dtview.Sort = String.Format(sortExpr, sortDir)
            End If
            grdInvoiceListNC.DataSource = dtview
            grdInvoiceListNC.DataBind()

        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try
    End Sub
    Private Sub BindVInvoiceC(ByVal sortExpr As String, ByVal sortDir As String)
        Try
            Dim dtview As DataView
            Dim ds As DataSet
            dtview = New DataView()
            grdInvoiceListC.DataSource = Nothing
            Session("cust_rowid") = Request.QueryString("cust_rowid")

            ds = UserManager.GetInvoice_ListC(Request.QueryString("cust_rowid"))
            dtview = ds.Tables(0).DefaultView
            If sortExpr <> "" Then
                dtview.Sort = String.Format(sortExpr, sortDir)
            End If
            grdInvoiceListC.DataSource = dtview
            grdInvoiceListC.DataBind()
        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try
    End Sub

    Protected Sub grdInvoiceListC_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdInvoiceListC.PageIndexChanging
        Try
            grdInvoiceListC.PageIndex = e.NewPageIndex
            BindVInvoiceC("", "")
        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try
    End Sub

    Protected Sub grdInvoiceListNC_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdInvoiceListNC.PageIndexChanging
        Try
            grdInvoiceListNC.PageIndex = e.NewPageIndex
            BindVInvoiceNC("", "")
        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try

    End Sub

    Protected Sub grdInvoiceListC_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdInvoiceListC.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hiddenField = DirectCast(e.Row.FindControl("hdnISChanged"), HtmlInputHidden)
        End If

    End Sub
    Protected Sub grdInvoiceListNC_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdInvoiceListNC.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim DropDownList1 = DirectCast(e.Row.FindControl("ddlpayment_vendor"), DropDownList)
            Dim hiddenField = DirectCast(e.Row.FindControl("hdnISChanged"), HtmlInputHidden)
            'DropDownList1.Attributes.Add("onchange", "document.getElementById('" + hiddenField.ClientID + "').value=1")
        End If

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Session("FromSomewhere") = "Y"
            Response.Redirect("CustList.aspx")
        Catch ex As Exception
            Response.Write(Utils.ShowError(ex))
        End Try
    End Sub

    Private Property GridViewSortDirection() As SortDirection
        Get
            If ViewState("sortDirection") Is Nothing Then
                ViewState("sortDirection") = SortDirection.Ascending
            End If
            Return ViewState("sortDirection")
        End Get
        Set(ByVal value As SortDirection)
            ViewState("sortDirection") = value
        End Set
    End Property

    Protected Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)
        Dim ds As DataSet
        ds = UserManager.GetInvoice_List(Request.QueryString("cust_no"))
        Dim dataTable As DataTable = ds.Tables(0)
        If dataTable IsNot Nothing Then
            Dim dataView As New DataView(dataTable)
            dataView.Sort = sortExpression & direction
            grdInvoiceListNC.DataSource = dataView
            grdInvoiceListNC.DataBind()
        End If
    End Sub

    Protected Sub grdCustomerList_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        isSort = True
        Dim sortExpression As String = e.SortExpression
        ViewState("SortExpression") = sortExpression
        If GridViewSortDirection = SortDirection.Ascending Then
            isAscend = True
            SortGridView(sortExpression, ASCENDING)
            GridViewSortDirection = SortDirection.Descending
        Else
            isAscend = False
            SortGridView(sortExpression, DESCENDING)
            GridViewSortDirection = SortDirection.Ascending
        End If
    End Sub

    Protected Sub grdInvoiceListC_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim row As GridViewRow

        row = DirectCast(DirectCast(e.CommandSource, LinkButton).Parent.Parent, GridViewRow)
        UserManager.Dinvoice(TryCast(row.FindControl("lblinvoiceno"), Label).Text)
        BindVInvoiceNC("", "")
        BindVInvoiceC("", "")

    End Sub
    Protected Property IsInEditMode() As Boolean
        Get
            Return Me.isEditMode
        End Get

        Set(ByVal value As Boolean)
            Me.isEditMode = value
        End Set
    End Property

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim i, j As Integer
            Dim row As GridViewRow
            Dim challan_no(10) As Integer
            Dim orderref As String
            Dim orderrefs As String

            j = 0
            orderrefs = "N"

            For i = 0 To (grdInvoiceListNC.Rows.Count - 1)
                grdInvoiceListNC.SelectedIndex = i
                row = grdInvoiceListNC.SelectedRow

                If DirectCast(row.FindControl("select_CheckBox"), CheckBox).Checked Then
                    If orderrefs = "N" Then
                        orderref = TryCast(row.FindControl("lblorderref"), Label).Text
                        orderrefs = "Y"
                    Else
                        If TryCast(row.FindControl("lblorderref"), Label).Text = orderref Then
                        Else
                            lblErrorMsg.Text = "Pl select same Order Reference"
                            Exit Sub
                        End If
                    End If

                    challan_no(j) = TryCast(row.FindControl("lblchallanno"), Label).Text
                    j = j + 1
                End If
            Next

            If j = 0 Then
                lblErrorMsg.Text = "Pl Select a Challan to Proceed"
            Else
                Session("ChallantoInv") = challan_no
                Session("ChallantoInvC") = j
                Response.Redirect("~/Invoice_Maint.aspx")
            End If

        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try
    End Sub



    Protected Sub grdInvoiceListC_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdInvoiceListC.PreRender
        Try
            Dim i As Integer = 0
            Dim previnv As String = ""
            Dim row As GridViewRow
            Dim nextrow As GridViewRow
            Dim firstrow As GridViewRow
            Dim prevrow As GridViewRow
            Dim rowindex As Integer = 1
            Dim nextrowtxt As String
            Dim rowtxtprev As String

            For i = 0 To grdInvoiceListC.Rows.Count - 2
                row = grdInvoiceListC.Rows(i)
                nextrow = grdInvoiceListC.Rows(i + 1)

                If TryCast(row.FindControl("lblinvoiceno"), Label).Text = TryCast(nextrow.FindControl("lblinvoiceno"), Label).Text Then
                    If Session("rowspan") Is Nothing Then
                        row.Cells(row.Cells.Count - 1).RowSpan = IIf(nextrow.Cells(row.Cells.Count - 1).RowSpan < 2, 2, row.Cells(row.Cells.Count - 1).RowSpan + 1)
                        row.Cells(row.Cells.Count - 2).RowSpan = IIf(nextrow.Cells(row.Cells.Count - 2).RowSpan < 2, 2, row.Cells(row.Cells.Count - 2).RowSpan + 1)
                        row.Cells(row.Cells.Count - 4).RowSpan = IIf(nextrow.Cells(row.Cells.Count - 4).RowSpan < 2, 2, row.Cells(row.Cells.Count - 4).RowSpan + 1)
                        Session("rowspan") = row.Cells(row.Cells.Count - 1).RowSpan

                    Else
                        firstrow = DirectCast(sender, GridView).Rows(i - rowindex)
                        firstrow.Cells(row.Cells.Count - 1).RowSpan = Convert.ToInt32(Session("rowspan")) + 1
                        firstrow.Cells(row.Cells.Count - 2).RowSpan = Convert.ToInt32(Session("rowspan")) + 1
                        firstrow.Cells(row.Cells.Count - 4).RowSpan = Convert.ToInt32(Session("rowspan")) + 1
                        Session("rowspan") = Convert.ToInt32(Session("rowspan")) + 1
                        rowindex += 1
                    End If
                    nextrow.Cells(row.Cells.Count - 1).Visible = False
                    nextrow.Cells(row.Cells.Count - 2).Visible = False
                    nextrow.Cells(row.Cells.Count - 4).Visible = False
                Else
                    Session("rowspan") = Nothing
                End If

            Next
            '            row.Cells(row.Cells.Count - 1).RowSpan = Convert.ToInt32(Session("rowspan"))
            Session("rowspan") = Nothing

        Catch ex As Exception

        End Try

    End Sub

End Class
