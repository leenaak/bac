Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine

Partial Class ChallanMaint
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
                BindChallan(Request.QueryString("challan_no"))
                Session("challan_no") = Request.QueryString("challan_no")
                'If Session("ChallanRCnt") > 0 Then
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

    Private Sub BindChallan(ByVal challan_no As String)
        Try
            Dim dt As New System.Data.DataTable
            dt = UserManager.GetChallan(challan_no)
            grdChallanList.DataSource = dt
            grdChallanList.DataBind()
            pnlChallanList.Visible = True
            Session("Challan") = dt
            Session("ChallanRCnt") = dt.Rows.Count
            Dim chdate As Date = dt.Rows(0).Item("ch_created_date")
            'Challan Date
            ch_date.Text = chdate 'Day(chdate) & "/" & Month(chdate) & "/" & Year(chdate)
            txtorderref.Text = dt.Rows(0).Item("order_ref")
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
        Dim challancount As Integer
        Dim row As GridViewRow
        Dim drowadd As DataRow

        row = DirectCast(DirectCast(e.CommandSource, LinkButton).Parent.Parent, GridViewRow)
        If CInt(TryCast(row.FindControl("lblqty"), Label).Text) > 0 Then

            challancount = Session("ChallanRCnt")
            If challancount = 0 Then
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
                dcol = New DataColumn("avl_qty", GetType(System.String))
                dt.Columns.Add(dcol)
            Else
                dt = Session("Challan")
                drowadd = dt.NewRow()
            End If

            drowadd("product") = TryCast(row.FindControl("lblproduct"), Label).Text
            drowadd("qty") = "0"
            drowadd("sell_pr") = TryCast(row.FindControl("lblsellpr"), Label).Text
            drowadd("over_pr") = "0.00"
            drowadd("ip_rowid") = TryCast(row.FindControl("lbliprowid"), Label).Text
            drowadd("item_rowid") = TryCast(row.FindControl("lblitemrowid"), Label).Text
            drowadd("avl_qty") = TryCast(row.FindControl("lblqty"), Label).Text
            dt.Rows.Add(drowadd)

            Session("Challan") = dt
            Session("ChallanRCnt") = dt.Rows.Count
            grdChallanList.DataSource = dt
            grdChallanList.DataBind()
            btnSave.Visible = True
            lblMessage.Text = " "
        Else
            lblMessage.Text = "Qty not available"
        End If

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
            Dim challan_no As Integer
            Dim challan_date As Date
            Dim cha_date As String

            For i = 0 To (grdChallanList.Rows.Count - 1)
                grdChallanList.SelectedIndex = i
                row = grdChallanList.SelectedRow
                If CInt(TryCast(row.FindControl("lblavlqty"), Label).Text) = 0 Then
                Else
                    If CInt(TryCast(row.FindControl("txtqty"), TextBox).Text) > CInt(TryCast(row.FindControl("lblavlqty"), Label).Text) Then
                        lblMessage.Text = "Quantity not available for " & TryCast(row.FindControl("lblproduct"), Label).Text
                        Exit Sub
                    End If
                End If
            Next


            challan_date = ch_date.Text
            cha_date = Day(challan_date) & "/" & Month(challan_date) & "/" & Year(challan_date)
            lblMessage.Text = " "
            If Session("challan_no") = 0 Then
                ds = UserManager.GetMaxChallan()
                challan_no = ds.Tables(0).Rows(0).Item("maxchallan_no")
                UserManager.IUDChallan_M("I", challan_no, Session("cust_rowid"), txtorderref.Text)
                Session("challan_no") = challan_no
            Else
                challan_no = Request.QueryString("challan_no")
                If challan_no = 0 Then
                    challan_no = Session("challan_no")
                End If
                UserManager.IUDChallan_M("U", challan_no, Session("cust_rowid"), txtorderref.Text)
            End If

            'Delete challan from table
            If challan_no <> 0 Then
                UserManager.IDChallan("D", "", "", "", "", challan_no, cha_date)
            End If

            For i = 0 To (grdChallanList.Rows.Count - 1)
                grdChallanList.SelectedIndex = i
                row = grdChallanList.SelectedRow
                If TryCast(row.FindControl("delete_CheckBox"), CheckBox).Checked Then
                Else
                    If CInt(TryCast(row.FindControl("txttosellpr"), TextBox).Text) = 0 Then
                        UserManager.IDChallan("I", TryCast(row.FindControl("lblitemrowid"), Label).Text, TryCast(row.FindControl("lbliprowid"), Label).Text, _
                                          TryCast(row.FindControl("txtqty"), TextBox).Text, "0", _
                                         challan_no, cha_date)
                    Else
                        UserManager.IDChallan("I", TryCast(row.FindControl("lblitemrowid"), Label).Text, TryCast(row.FindControl("lbliprowid"), Label).Text, _
                                             TryCast(row.FindControl("txtqty"), TextBox).Text, TryCast(row.FindControl("txttosellpr"), TextBox).Text, _
                                            challan_no, cha_date)
                    End If
                    lblMessage.Text = "Challan Updated Successfully"
                End If
            Next
            BindVProduct("", "")
            BindChallan(Session("challan_no"))
        Catch ex As Exception

        End Try


    End Sub


    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        'Return to Challan List
        Response.Redirect("~/ChallanList.aspx?cust_rowid=" & Session("cust_rowid"))
    End Sub

    Protected Sub btnPrint_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim url As String = "Challan_Print.aspx"
        Dim fullURL As String = "window.open('" & url & "', '_blank', 'height=1000,width=1000,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,titlebar=no' );"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)
    End Sub
End Class
