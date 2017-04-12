Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data
Partial Class Chalan_Print
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim MyDA As New MySqlClient.MySqlDataAdapter()
        Dim MyCommand As New MySqlClient.MySqlCommand()

        Dim Myconstring As New MySqlClient.MySqlConnection()
        Myconstring.ConnectionString = "server=localhost;User Id=root;password=1907;Persist Security Info=True;database=bac_invent"
        MyCommand.Connection = Myconstring
        MyCommand.CommandText = "Select * from cust_master a, cust_qtn b where b.qtn_no = " & Session("quotation_no") & " and b.cust_rowid = a.cust_rowid"
        MyDA = New MySqlClient.MySqlDataAdapter()
        MyDA.SelectCommand = MyCommand

        Dim myDS As New DataSet1()
        'This is our DataSet created at Design Time      
        MyDA.Fill(myDS, "cust_master")

        'Dim dt As New System.Data.DataTable("item_master")
        'dt = UserManager.GetChallanItems(Session("challan_no"))
        'You have to use the same name as that of your Dataset that you created during design time
        ' This is the Crystal Report file created at Design Time
        Dim MyDA1 As New MySqlClient.MySqlDataAdapter()
        MyCommand.CommandText = "SELECT concat(c.item_name, '-', c.make, '-', c.volume, ' ', c.unit_type) as product, (-1 * b.qty)  as qty, if(c.nett_item='Y', 'Nett', ' ') as item_nett , " & _
                                " if(b.purchase_price = 0, d.sell_pr, b.purchase_price) as sell_pr, b.ip_rowid as ip_rowid, b.item_rowid,a.qtn_created_date ,a.qtn_no ,c.vat as VAT" & _
                                " FROM cust_qtn a, qtn_tran b, item_master c, item_price d  " & _
                                " where a.qtn_no = b.qtn_no and b.item_rowid = c.item_rowid and b.ip_rowid = d.ip_rowid " & _
                                " and a.qtn_no = " & Session("quotation_no") & ""

        MyDA1.SelectCommand = MyCommand
        MyDA1.Fill(myDS, "item_master")

        'myDS.Tables.Add("item_master")
        Dim report As New CrystalReport4
        report.SetDataSource(myDS)        
        ' Set the SetDataSource property of the Report to the Dataset
        CrystalReportViewer1.ReportSource = report
        ' Set the Crystal Report Viewer's property to the oRpt Report object that we created
        CrystalReportViewer1.Visible = True
        'report.PrintOptions.PrinterName = "HPLaserJ"
        'print all pages if 0,0
        'report.PrintToPrinter(1, False, 0, 0)
        report.Clone()

    End Sub
End Class
