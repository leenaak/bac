Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data
Partial Class Invoice_Print
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim MyDA As New MySqlClient.MySqlDataAdapter()
        Dim MyCommand As New MySqlClient.MySqlCommand()
        Dim Myconstring As New MySqlClient.MySqlConnection()

        MyDA = New MySqlClient.MySqlDataAdapter()

        Dim rowid As String
        rowid = Request.QueryString("rowid")

        Dim ds As New System.Data.DataSet
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dcol As New DataColumn()
        Dim drowadd As DataRow

        dt = UserManager.GetInvPrintM(rowid)
        ds.Tables.Add(dt.Copy())
        ds.Tables(0).TableName = "Cust_Data"
        dt = UserManager.GetInvPrintD(rowid)
        ds.Tables.Add(dt.Copy())
        ds.Tables(1).TableName = "Invoice"

        dt = UserManager.GetInvPrintC(rowid)
        Dim i As Integer
        Dim chno As String = ""
		Dim order_ref As String
		
        For i = 0 To dt.Rows.Count - 1
            chno = chno & " " & dt.Rows(i).Item(0)
            If i = dt.Rows.Count - 1 Then
            Else
                chno = chno & ","
            End If
        Next
		
        If dt.Rows.Count > 0 Then
            order_ref = dt.Rows(0).Item(1)
        Else
            order_ref = ""
        End If
		
        drowadd = dt1.NewRow()
        dcol = New DataColumn("ChallanNo", GetType(System.String))
        dt1.Columns.Add(dcol)
        drowadd("ChallanNo") = chno
		
        dcol = New DataColumn("order_ref", GetType(System.String))
        dt1.Columns.Add(dcol)
        drowadd("order_ref") = order_ref
		
        dt1.Rows.Add(drowadd)
        ds.Tables.Add(dt1.Copy())
        ds.Tables(2).TableName = "ChallanNo"


        Dim report As New CrystalReport2
        report.SetDataSource(ds)

        ' Set the SetDataSource property of the Report to the Dataset
        CrystalReportViewer1.ReportSource = report

        'report.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA5
        ' Set the Crystal Report Viewer's property to the oRpt Report object that we created
        CrystalReportViewer1.Visible = True
        report.Clone()

    End Sub
End Class
