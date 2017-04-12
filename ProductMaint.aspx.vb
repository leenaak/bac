Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.Data.Sql
Imports System.Data.Common

'Imports System.Data.SqlClient

Partial Class ProductMaint
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'If Session("emp_group_id") = 99 Or Session("emp_group_id") = 20 Then
            '    'attached trimmed menu for clients!
            DirectCast(Master.FindControl("xml"), OboutInc.SlideMenu.SlideMenu).XmlPath = "BacMenu.xml"
            'pnlResult.Visible = False
            'pnlreviewed.Visible = False
            'Else
            '    'Redirect to No access page.
            '    Session("emp_group_id") = ""
            '    Session("emp_id") = ""
            '    Response.Redirect("login.aspx")
            'End If

            GetDefaults()

        Catch ex As Exception
            'lblErrorMsg.Text = Utils.ShowError(ex)
        End Try
    End Sub

    Private Sub GetDefaults()

        Try
            If Request.QueryString("functionAU") = "A" Then

                dsItemEntry.SelectCommand = "SELECT '' as item_rowid, '' as item_name, '' as make, '' as volume, '' as unit_type, '' as vat, '' as stock_trigger_qty, '' as nett_item"
                DirectCast(rptProductMaint.FindControl("ctl00$btnInsert"), Button).CommandName = "Insert"
            Else
                '                dsItemEntry.SelectCommand = "SELECT `item_rowid`, `item_name`, `make`, `volume`, `unit_type`, `vat`, `stock_trigger_qty`, (case when nett_item = 'Y' then 'Y' else 'N' end) as nett_item  from `item_master` where `item_rowid` = " & Request.QueryString("item_rowid")
                dsItemEntry.SelectCommand = "SELECT `item_rowid`, `item_name`, `make`, `volume`, `unit_type`, `vat`, `stock_trigger_qty`, nett_item  from `item_master` where `item_rowid` = " & Request.QueryString("item_rowid")
                DirectCast(rptProductMaint.FindControl("ctl00$btnInsert"), Button).CommandName = "Update"
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Edit_Record(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs)

        Dim user As String = "USER"
        Dim nett_item As String
        If Trim(TryCast(rptProductMaint.FindControl("ctl00$chknett"), CheckBox).Checked) Then
            nett_item = "Y"
        Else
            nett_item = "N"
        End If

        Dim SQLString As String
        If e.CommandName = "Insert" Then
            SQLString = "INSERT INTO `Item_Master` (`item_name`, `make`, `volume`, `unit_type`, `vat`, `updated_by`, `transaction_date`,  " & _
                                 " `stock_trigger_qty`, nett_item) VALUES (" & _
                                 "'" & TryCast(rptProductMaint.FindControl("ctl00$txtitem_name"), TextBox).Text & "', " & _
                                 "'" & TryCast(rptProductMaint.FindControl("ctl00$txtmake"), TextBox).Text & "', " & _
                                 "'" & TryCast(rptProductMaint.FindControl("ctl00$txtvolume"), TextBox).Text & "', " & _
                                 "'" & TryCast(rptProductMaint.FindControl("ctl00$txtunit_type"), TextBox).Text & "', " & _
                                 "'" & TryCast(rptProductMaint.FindControl("ctl00$ddlvat"), DropDownList).Text & "', " & _
                                 "'" & user & "', " & _
                                 "CURDATE()" & ", " & _
                                 "'" & TryCast(rptProductMaint.FindControl("ctl00$txtstock_trigger_qty"), TextBox).Text & "', " & _
                                 "'" & nett_item & "') "
            dsItemEntry.InsertCommand = SQLString
            dsItemEntry.Insert()
            lblMessage.Text = "Record Inserted"
        Else
            SQLString = "UPDATE `Item_Master` SET " & _
                             "`item_name` = '" & Trim(TryCast(rptProductMaint.FindControl("ctl00$txtitem_name"), TextBox).Text) & "', " & _
                             "`make` = '" & Trim(TryCast(rptProductMaint.FindControl("ctl00$txtmake"), TextBox).Text) & "', " & _
                             "`volume` = '" & Trim(TryCast(rptProductMaint.FindControl("ctl00$txtvolume"), TextBox).Text) & "', " & _
                             "`unit_type` = '" & Trim(TryCast(rptProductMaint.FindControl("ctl00$txtunit_type"), TextBox).Text) & "', " & _
                             "`vat` = '" & Trim(TryCast(rptProductMaint.FindControl("ctl00$ddlvat"), DropDownList).Text) & "', " & _
                             "`updated_by` = '" & user & "', " & _
                             "`transaction_date` = CURDATE()" & ", " & _
                             "`stock_trigger_qty` = '" & Trim(TryCast(rptProductMaint.FindControl("ctl00$txtstock_trigger_qty"), TextBox).Text) & "', " & _
                             "`nett_item` = '" & nett_item & "' " & _
                             "where `item_rowid` = " & Trim(TryCast(rptProductMaint.FindControl("ctl00$lblitem_rowid"), Label).Text)
            dsItemEntry.UpdateCommand = SQLString
            dsItemEntry.Update()
            lblMessage.Text = "Record Updated"
        End If
    End Sub



End Class
