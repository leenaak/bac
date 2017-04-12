Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb

Partial Class CustList
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
            If Session("FromSomewhere") = "Y" Then
                Session("FromSomewhere") = "N"
                BindVCustomer("", "")
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
            Session("csrch_txt") = nameTextBox.Text
            Session("cfile_num") = filenumTextBox.Text
            lblErrorMsg.Text = ""
            BindVCustomer("", "")
        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try

    End Sub


    Private Sub BindVCustomer(ByVal sortExpr As String, ByVal sortDir As String)
        Try
            Dim dtview As DataView
            Dim ds As DataSet
            dtview = New DataView()
            grdCustomerList.DataSource = Nothing

            If nameTextBox.Text <> "" Or filenumTextBox.Text <> "" Then
                ds = UserManager.GetCustomer_Details(nameTextBox.Text, filenumTextBox.Text)
            Else
                ds = UserManager.GetCustomer_Details(Session("csrch_txt"), Session("cfile_num"))
            End If
            dtview = ds.Tables(0).DefaultView
            If sortExpr <> "" Then
                dtview.Sort = String.Format(sortExpr, sortDir)
            End If

            grdCustomerList.DataSource = dtview
            grdCustomerList.DataBind()
            If grdCustomerList.Rows.Count > 0 Then
                pnlResult.Visible = True
                pnlreviewed.Visible = True
            Else
                lblErrorMsg.Text = "No matching records found"
            End If
        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try
    End Sub
    Protected Sub grdCustomerList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdCustomerList.PageIndexChanging
        Try
            grdCustomerList.PageIndex = e.NewPageIndex
            BindVCustomer("", "")
        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try

    End Sub

    Protected Sub grdCustomerList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCustomerList.RowDataBound
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
        If nameTextBox.Text = "" Then
            ds = UserManager.GetProduct_Details("")
        Else
            ds = UserManager.GetProduct_Details(nameTextBox.Text)
        End If
        Dim dataTable As DataTable = ds.Tables(0)
        If dataTable IsNot Nothing Then
            Dim dataView As New DataView(dataTable)
            dataView.Sort = sortExpression & direction
            grdCustomerList.DataSource = dataView
            grdCustomerList.DataBind()
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

End Class
