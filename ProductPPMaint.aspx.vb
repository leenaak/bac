Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.Data.Sql
Imports System.Data.Common
Partial Class ProductPPMaint
    Inherits System.Web.UI.Page
    Private isEditMode As Boolean = False

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

            GetSellPrice()
            Dim ds As DataSet
            ds = UserManager.GetProuct_DetailsbyId(Request.QueryString("item_rowid"))
            lblitem_name.Text = ds.Tables(0).Rows(0).Item("item_name")
            txtitem_make.Text = ds.Tables(0).Rows(0).Item("make") & " " & ds.Tables(0).Rows(0).Item("volume") & " " & ds.Tables(0).Rows(0).Item("unit_type")
        Catch ex As Exception
            'lblErrorMsg.Text = Utils.ShowError(ex)
        End Try
    End Sub

    Private Sub GetSellPrice()
        Try
            dsItemSP.SelectCommand = "SELECT `item_master`.`item_name`, `item_tran`.`item_rowid`, `item_tran`.`item_tran_rowid`, " & _
                                     "`item_tran`.`purchase_price`" & _
                                     "FROM `bac_invent`.`item_tran` INNER JOIN `bac_invent`.`item_master` " & _
                                     "ON (`item_tran`.`item_rowid` = `item_master`.`item_rowid`) " & _
                                     "where `item_tran`.`item_rowid` = '" & Request.QueryString("item_rowid") & "' and `item_tran`.`tran_type`='P' order by `item_tran`.`tran_date` desc"
        Catch ex As Exception
            Response.Write(Utils.ShowError(ex))
        End Try
    End Sub


    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            Dim user As String = "USER"
            Dim SQLString As String

            SQLString = "UPDATE `bac_invent`.`item_tran` SET `purchase_price` = '" & txtAsell_price.Text & "', `tran_date`='" & Today() & "' " & _
                                 " WHERE item_rowid ='" & Request.QueryString("item_rowid") & "'"
            dsItemSP.UpdateCommand = SQLString
            dsItemSP.Update()
            GetSellPrice()
            Response.Redirect("~/ProductList.aspx")
        Catch ex As Exception
            Response.Write(Utils.ShowError(ex))
        End Try

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim l As LinkButton = DirectCast(e.Row.FindControl("lnkDelete"), LinkButton)
            l.Attributes.Add("onclick", "javascript:return " & "confirm('Are you sure you want to delete this record " & DataBinder.Eval(e.Row.DataItem, "item_tran_rowid") & "')")
        End If
    End Sub
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName = "Delete" Then
            ' get the categoryID of the clicked row
            Dim ip_rowid As Integer = Convert.ToInt32(e.CommandArgument)
            ' Delete the record 
            ' Implement this on your own :) 
            DeleteRecordByID(ip_rowid)
        End If
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim ip_rowid As Integer = CInt(grdProductSellPr.DataKeys(e.RowIndex).Value)
        DeleteRecordByID(ip_rowid)
    End Sub

    Protected Sub GridView1_RowDeleted(ByVal sender As Object, ByVal e As GridViewDeletedEventArgs)

    End Sub

    Protected Sub DeleteRecordByID(ByVal ip_rowid As String)
        Dim strsql As String
        Try
            strsql = "DELETE from item_tran where item_tran_rowid= " + ip_rowid + ""
            dsItemSP.DeleteCommand = strsql
        Catch ex As Exception
        End Try

    End Sub
End Class
