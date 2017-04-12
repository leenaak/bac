Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.Data.Sql
Imports System.Data.Common
Partial Class ProductSPMaint
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
            dsItemSP.SelectCommand = "SELECT `item_master`.`item_name`, `item_price`.`item_rowid`, `item_price`.`ip_rowid`, " & _
                                     "`item_price`.`sell_pr`, `item_price`.`eff_from`,`item_price`.`eff_to`, `item_price`.`updated_by`" & _
                                     " FROM `bac_invent`.`item_price`INNER JOIN `bac_invent`.`item_master` " & _
                                     "ON (`item_price`.`item_rowid` = `item_master`.`item_rowid`) where `item_price`.`item_rowid` = " & Request.QueryString("item_rowid")
        Catch ex As Exception
            Response.Write(Utils.ShowError(ex))
        End Try
    End Sub


    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            Dim user As String = "USER"
            Dim strdate As Date
            Dim SQLString As String
            Dim strday As String
            Dim strmonth As String
            Dim stryear As String
            Dim sttemp As String
            Dim prevdate As String

            strdate = CDate(txtAeff_from.Text)
            strday = DatePart(DateInterval.Day, strdate)
            strmonth = DatePart(DateInterval.Month, strdate)
            stryear = DatePart(DateInterval.Year, strdate)
            sttemp = stryear & "/" & strmonth & "/" & strday
            If (strday - 1) = 0 Then
                If (strmonth - 1) = 0 Then
                    prevdate = stryear & "/" & 12 & "/" & 31
                Else
                    prevdate = stryear & "/" & (strmonth - 1) & "/" & 30
                End If

            Else

                prevdate = stryear & "/" & strmonth & "/" & (strday - 1)
            End If

            'Close the Old Sell Price
            Dim ds As New DataSet
            Dim ip_rowid As Integer

            SQLString = "Select Max(ip_rowid) as iprowid From item_price Where item_rowid=" & Request.QueryString("item_rowid")
            ds = UserManager.getdata(SQLString)
            If IsDBNull(ds.Tables(0).Rows(0).Item("iprowid")) Then
            Else
                ip_rowid = ds.Tables(0).Rows(0).Item("iprowid")
                SQLString = "Update item_price Set eff_to='" & prevdate & "' where ip_rowid=" & ip_rowid
                UserManager.ExecQuery(SQLString)
            End If


            'Update the New Sell Price
            SQLString = "INSERT INTO `Item_price` (`item_rowid`, `sell_pr`, `eff_from`, `eff_to`, `updated_by`, `transaction_date` " & _
                                 " ) VALUES (" & _
                                       Request.QueryString("item_rowid") & " , " & _
                                       txtAsell_price.Text & " , " & _
                                 "'" & sttemp & "', " & _
                                 "'9999-12-31' ," & _
                                 "'" & user & "', " & _
                                 "CURDATE() )"
            dsItemSP.InsertCommand = SQLString
            dsItemSP.Insert()
            GetSellPrice()
            Response.Redirect("~/ProductList.aspx")
        Catch ex As Exception
            Response.Write(Utils.ShowError(ex))
        End Try

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim l As LinkButton = DirectCast(e.Row.FindControl("lnkDelete"), LinkButton)
            l.Attributes.Add("onclick", "javascript:return " & "confirm('Are you sure you want to delete this record " & DataBinder.Eval(e.Row.DataItem, "ip_rowid") & "')")
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
            strsql = "DELETE from item_price where ip_rowid= " + ip_rowid + ""
            dsItemSP.DeleteCommand = strsql
        Catch ex As Exception
        End Try

    End Sub
End Class
