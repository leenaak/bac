Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data
Imports System.Runtime.Serialization
Imports CrystalDecisions.Shared
Imports System
Imports System.Drawing
Imports CrystalDecisions.Reporting

Partial Class CashMemo_Print
    Inherits System.Web.UI.Page
    Dim report As New CrystalReport3
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
        ds.Tables(1).TableName = "CashMemo"
        dt = UserManager.GetChMemoAddr(Session("InvoiceNo"))
        ds.Tables.Add(dt.Copy())
        ds.Tables(2).TableName = "Cust_Details"


        dt = UserManager.GetInvPrintC(rowid)
        Dim i As Integer
        Dim chno As String = ""

        For i = 0 To dt.Rows.Count - 1
            chno = chno & " " & dt.Rows(i).Item(0)
            If i = dt.Rows.Count - 1 Then
            Else
                chno = chno & ","
            End If
        Next

        drowadd = dt1.NewRow()
        dcol = New DataColumn("ChallanNo", GetType(System.String))
        dt1.Columns.Add(dcol)
        drowadd("ChallanNo") = chno
        dt1.Rows.Add(drowadd)
        ds.Tables.Add(dt1.Copy())
        ds.Tables(3).TableName = "ChallanNo"
        report.SetDataSource(ds)
        'report.PrintOptions.PrinterName = ""

        ' Set the SetDataSource property of the Report to the Dataset
        CrystalReportViewer1.ReportSource = report
        ' Set the Crystal Report Viewer's property to the oRpt Report object that we created
        CrystalReportViewer1.Visible = True
        report.Clone()

    End Sub

    Protected Sub btnConvert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConvert.Click

        Try
            report.ExportToDisk(ExportFormatType.PortableDocFormat, "e:\BAC_Soft\Pdf\Test.pdf")

        Catch Ex As Exception
            txtMsg.Text = Ex.ToString()
            'MsgBox(Ex.ToString)
        End Try
    End Sub
End Class

'rptDocument = New ReportDocument()
'rptDocument.Load(strReportPath, OpenReportMethod.OpenReportByDefault)
'Dim printsettings As New PrinterSettings()
'Dim str As String = printsettings.PrinterNamerptDocument.PrintOptions.PrinterName = "hp LaserJet 1010 HB"
'rptDocument.SetDataSource(dsGateRecipt)
'rptDocument.PrintOptions.PaperOrientation = PaperOrientation.LandscaperptDocument.PrintToPrinter(1, False, 0, 0)