Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.Data.Sql
Imports System.Data.Common

'Imports System.Data.SqlClient

Partial Class CustMaint
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
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

            End If

        Catch ex As Exception
            'lblErrorMsg.Text = Utils.ShowError(ex)
        End Try
    End Sub

    Private Sub GetDefaults()

        Try
            If Request.QueryString("functionAU") = "A" Then

                dsCustEntry.SelectCommand = "SELECT '' as cust_rowid, '' as cust_no, '' as cust_name, '' as address1, '' as address2, '' as city, '' as district, '' as state, '' as pin_code, '' as contact, '' as std_code, '' as ph_no"
                DirectCast(rptCustMaint.FindControl("ctl00$btnInsert"), Button).CommandName = "Insert"
            Else
                dsCustEntry.SelectCommand = "SELECT cust_rowid, cust_name, cust_no, address1, address2, city, district, state, pin_code, contact, std_code, ph_no from cust_master where cust_rowid = " & Request.QueryString("cust_rowid")
                DirectCast(rptCustMaint.FindControl("ctl00$btnInsert"), Button).CommandName = "Update"
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Edit_Record(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs)

        Dim user As String = "USER"
        Dim SQLString As String
        If Request.QueryString("functionAU") = "A" Then
            SQLString = "INSERT INTO cust_master (cust_no, cust_name, address1, address2, city, district, state, pin_code, contact, std_code, ph_no " & _
                                 " ) VALUES (" & _
                                 "'" & TryCast(rptCustMaint.FindControl("ctl00$txtcust_no"), TextBox).Text & "', " & _
                                 "'" & TryCast(rptCustMaint.FindControl("ctl00$txtcust_name"), TextBox).Text & "', " & _
                                 "'" & TryCast(rptCustMaint.FindControl("ctl00$txtaddress1"), TextBox).Text & "', " & _
                                 "'" & TryCast(rptCustMaint.FindControl("ctl00$txtaddress2"), TextBox).Text & "', " & _
                                 "'" & TryCast(rptCustMaint.FindControl("ctl00$txtcity"), TextBox).Text & "', " & _
                                 "'" & TryCast(rptCustMaint.FindControl("ctl00$txtdistrict"), TextBox).Text & "', " & _
                                 "'" & TryCast(rptCustMaint.FindControl("ctl00$txtstate"), TextBox).Text & "', " & _
                                 " " & TryCast(rptCustMaint.FindControl("ctl00$txtpin_code"), TextBox).Text & ", " & _
                                 "'" & TryCast(rptCustMaint.FindControl("ctl00$txt_contact"), TextBox).Text & "', " & _
                                 "'" & TryCast(rptCustMaint.FindControl("ctl00$txtstd_code"), TextBox).Text & "', " & _
                                 "'" & TryCast(rptCustMaint.FindControl("ctl00$txtphone"), TextBox).Text & "') "
            dsCustEntry.InsertCommand = SQLString
            dsCustEntry.Insert()
            Session("FromSomewhere") = "Y"
            lblMessage.Text = "Record Inserted"
            Response.Redirect("CustList.aspx")
        Else
            SQLString = "UPDATE cust_master SET " & _
                             "cust_no = '" & Trim(TryCast(rptCustMaint.FindControl("ctl00$txtcust_no"), TextBox).Text) & "', " & _
                             "cust_name = '" & Trim(TryCast(rptCustMaint.FindControl("ctl00$txtcust_name"), TextBox).Text) & "', " & _
                             "address1 = '" & Trim(TryCast(rptCustMaint.FindControl("ctl00$txtaddress1"), TextBox).Text) & "', " & _
                             "address2 = '" & Trim(TryCast(rptCustMaint.FindControl("ctl00$txtaddress2"), TextBox).Text) & "', " & _
                             "city = '" & Trim(TryCast(rptCustMaint.FindControl("ctl00$txtcity"), TextBox).Text) & "', " & _
                             "district = '" & Trim(TryCast(rptCustMaint.FindControl("ctl00$txtdistrict"), TextBox).Text) & "', " & _
                             "state = '" & Trim(TryCast(rptCustMaint.FindControl("ctl00$txtstate"), TextBox).Text) & "', " & _
                             "pin_code = " & Trim(TryCast(rptCustMaint.FindControl("ctl00$txtpin_code"), TextBox).Text) & ", " & _
                             "contact = '" & Trim(TryCast(rptCustMaint.FindControl("ctl00$txt_contact"), TextBox).Text) & "', " & _
                             "std_code = '" & Trim(TryCast(rptCustMaint.FindControl("ctl00$txtstd_code"), TextBox).Text) & "', " & _
                             "ph_no = '" & Trim(TryCast(rptCustMaint.FindControl("ctl00$txtphone"), TextBox).Text) & "' " & _
                             "where cust_rowid = " & Trim(TryCast(rptCustMaint.FindControl("ctl00$lblcust_rowid"), Label).Text)
            dsCustEntry.UpdateCommand = SQLString
            dsCustEntry.Update()
            lblMessage.Text = "Record Updated"
            Session("FromSomewhere") = "Y"
            Response.Redirect("CustList.aspx")
        End If
    End Sub


End Class
