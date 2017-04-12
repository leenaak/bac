Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb

Partial Class ProductList
    Inherits System.Web.UI.Page
    Public Shared isSort As Boolean = False
    Public Shared isAscend As Boolean = False
    Private Const ASCENDING As String = " ASC"
    Private Const DESCENDING As String = " DESC"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'If Session("emp_group_id") = 99 Or Session("emp_group_id") = 20 Then
            '    'attached trimmed menu for clients!
            DirectCast(Master.FindControl("xml"), OboutInc.SlideMenu.SlideMenu).XmlPath = "BacMenu.xml"
            pnlResult.Visible = False
            pnlreviewed.Visible = False
            If Session("srch_txt") <> "" Then
                BindVPayments("", "")
            Else
            End If

            'Else
            '    'Redirect to No access page.
            '    Session("emp_group_id") = ""
            '    Session("emp_id") = ""
            '    Response.Redirect("login.aspx")
            'End If
        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Session("srch_txt") = item_nameTextBox.Text
            lblErrorMsg.Text = ""
            BindVPayments("", "")
        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try

    End Sub


    Private Sub BindVPayments(ByVal sortExpr As String, ByVal sortDir As String)
        Try
            Dim dtview As DataView
            Dim ds As DataSet
            dtview = New DataView()
            grdProductList.DataSource = Nothing

            If item_nameTextBox.Text <> "" Then
                ds = UserManager.GetProduct_Details(item_nameTextBox.Text)
            Else
                ds = UserManager.GetProduct_Details(Session("srch_txt"))
            End If
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
            BindVPayments("", "")
        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try

    End Sub

    Protected Sub grdProductList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProductList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hiddenField = DirectCast(e.Row.FindControl("hdnISChanged"), HtmlInputHidden)
        End If

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Response.Redirect("~/ProductList.aspx")
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
        If item_nameTextBox.Text = "" Then
            ds = UserManager.GetProduct_Details("")
        Else
            ds = UserManager.GetProduct_Details(item_nameTextBox.Text)
        End If
        Dim dataTable As DataTable = ds.Tables(0)
        If dataTable IsNot Nothing Then
            Dim dataView As New DataView(dataTable)
            dataView.Sort = sortExpression & direction
            grdProductList.DataSource = dataView
            grdProductList.DataBind()
        End If
    End Sub

    Protected Sub grdPcardVendorPayments_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
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

End Class
