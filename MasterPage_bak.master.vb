Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack Then
                Session("selectedmenu") = xml.SelectedId
            Else
                xml.SelectedId = Session("selectedmenu")
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class

