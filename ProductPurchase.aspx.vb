Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.Data.Sql
Imports System.Data.Common

'Imports System.Data.SqlClient

Partial Class ProductPurchase
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
            dsItemEntry.SelectCommand = "SELECT `item_rowid`, `item_name`, `make`, concat(`volume`, ' ', `unit_type` ) as volume from `item_master` where `item_rowid` = " & Request.QueryString("item_rowid")
            DirectCast(rptProductMaint.FindControl("ctl00$btnInsert"), Button).CommandName = "Insert"
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Edit_Record(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs)

        Dim user As String = "USER"
        Dim SQLString As String
        If e.CommandName = "Insert" Then
            SQLString = "INSERT INTO item_tran (item_rowid, tran_type, purchase_price , qty , updated_by,  " & _
                                 " tran_date ) VALUES (" & _
                                 "" & TryCast(rptProductMaint.FindControl("ctl00$lblitem_rowid"), Label).Text & ", " & _
                                 "'" & TryCast(rptProductMaint.FindControl("ctl00$ddlpur_type"), DropDownList).SelectedItem.Text & "', " & _
                                 "" & TryCast(rptProductMaint.FindControl("ctl00$txtpurchase_price"), TextBox).Text & ", " & _
                                 "" & TryCast(rptProductMaint.FindControl("ctl00$txtpur_qty"), TextBox).Text & ", " & _
                                 "'" & user & "', " & _
                                 "CURDATE() )" & " "
            dsItemEntry.InsertCommand = SQLString
            dsItemEntry.Insert()
            lblMessage.Text = "Record Inserted"
            Response.Redirect("~/ProductList.aspx")
        End If
    End Sub

    Protected Sub ddlpur_type_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlTrans As DropDownList
        ddlTrans = rptProductMaint.FindControl("ctl00$ddlpur_type")
        If ddlTrans.SelectedValue = "BF" Then
            TryCast(rptProductMaint.FindControl("ctl00$txtpurchase_price"), TextBox).Text = "0"
            TryCast(rptProductMaint.FindControl("ctl00$lblpurprice"), Label).Visible = False
            TryCast(rptProductMaint.FindControl("ctl00$txtpurchase_price"), TextBox).Visible = False
        End If
    End Sub


    'Protected Sub rptProductMaint_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptProductMaint.ItemDataBound
    '    Dim ddlTrans As DropDownList
    '    ddlTrans = e.Item.FindControl("ddlpur_type")
    '    ddlTrans.AutoPostBack = True
    '    AddHandler ddlTrans.SelectedIndexChanged, AddressOf ddlpur_type_SelectedIndexChanged

    'End Sub
End Class
