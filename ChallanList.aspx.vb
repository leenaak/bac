﻿Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb

Partial Class ChallanList
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
            BindVChallan("", "")
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


    Private Sub BindVChallan(ByVal sortExpr As String, ByVal sortDir As String)
        Try
            Dim dtview As DataView
            Dim ds As DataSet
            dtview = New DataView()
            grdChallanList.DataSource = Nothing
            Session("cust_rowid") = Request.QueryString("cust_rowid")
            ds = UserManager.GetChallan_List(Request.QueryString("cust_rowid"))
            dtview = ds.Tables(0).DefaultView
            If sortExpr <> "" Then
                dtview.Sort = String.Format(sortExpr, sortDir)
            End If

            grdChallanList.DataSource = dtview
            grdChallanList.DataBind()
            If grdChallanList.Rows.Count > 0 Then
                pnlResult.Visible = True
                pnlreviewed.Visible = True
            Else
                lblErrorMsg.Text = "No matching records found"
            End If

            Dim dtview1 As DataView
            Dim ds1 As DataSet
            dtview1 = New DataView()
            ds1 = UserManager.GetInvoice_List(Request.QueryString("cust_rowid"))
            dtview1 = ds1.Tables(0).DefaultView
            grdChallanListI.DataSource = dtview1
            grdChallanListI.DataBind()
            If grdChallanListI.Rows.Count > 0 Then
                PanelI.Visible = True
                pnlreviewedI.Visible = True
            End If


        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try
    End Sub
    Protected Sub grdChallanList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdChallanList.PageIndexChanging
        Try
            grdChallanList.PageIndex = e.NewPageIndex
            BindVChallan("", "")
        Catch ex As Exception
            lblErrorMsg.Text = Utils.ShowError(ex)
        End Try

    End Sub

    Protected Sub grdChallanList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdChallanList.RowDataBound
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
        ds = UserManager.GetChallan_List(Request.QueryString("cust_no"))
        Dim dataTable As DataTable = ds.Tables(0)
        If dataTable IsNot Nothing Then
            Dim dataView As New DataView(dataTable)
            dataView.Sort = sortExpression & direction
            grdChallanList.DataSource = dataView
            grdChallanList.DataBind()
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
