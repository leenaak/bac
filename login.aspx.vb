Imports System.Data

Partial Class _Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Hide Menu
            Me.Master.FindControl("xml").Visible = False
            'Clear the session variables
            Session.Clear()
            FormsAuthentication.SignOut()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Login1_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles Login1.Authenticate
        Try
            Dim dsReturnVal As DataSet = New DataSet
            dsReturnVal = UserManager.Validate(Login1.UserName, Login1.Password)
            If dsReturnVal.Tables(0).Rows.Count > 0 Then
                'Save in session for future use of restricting menus etc
                Session("emp_group_id") = dsReturnVal.Tables(0).Rows(0).Item(0)
                Session("emp_id") = Login1.UserName.Trim()
                Session("EmpName") = dsReturnVal.Tables(0).Rows(0).Item(2)
                Session("EmpPassword") = dsReturnVal.Tables(0).Rows(0).Item(3)
                'log in
                ' Success, create non-persistent authentication cookie.
                FormsAuthentication.SetAuthCookie(Session("emp_id"), False)
                Response.Redirect("Default.aspx")
            Else
                e.Authenticated = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class

