Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Math
Imports System.Drawing
Imports MySql.Data
Imports System.Runtime.Serialization
Imports CrystalDecisions.Shared
Imports System
Imports CrystalDecisions.Reporting
Imports System.IO


Partial Class InvoiceSplitDet
    Inherits System.Web.UI.Page
    Public Shared isSort As Boolean = False
    Public Shared isAscend As Boolean = False
    Private isEditMode As Boolean = False
    Private Const ASCENDING As String = " ASC"
    Private Const DESCENDING As String = " DESC"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then

                'If Session("emp_group_id") = 99 Or Session("emp_group_id") = 20 Then
                '    'attached trimmed menu for clients!
                DirectCast(Master.FindControl("xml"), OboutInc.SlideMenu.SlideMenu).XmlPath = "BacMenu.xml"
                BindInvoiceSplit(Request.QueryString("invoice_no_mstr"))
                Dim maxinv_no As String = UserManager.getmaxinvoice()

                'ddlPrinterList.DataSource = System.Drawing.Printing.PrinterSettings.InstalledPrinters()
                'ddlPrinterList.DataBind()

                lblinv_num.Text = maxinv_no

                Session("InvoiceNo") = Request.QueryString("invoice_no_mstr")

				If Session("cust_rowid") = "229" Then

                    lblinv_num.Visible = True
                    lblinvtxt.Visible = True
                    lblinv_num.Text = maxinv_no
                    GetChMemoAddr(Request.QueryString("invoice_no_mstr"))
                    lbldetails.visible = True
                    txtaddress1.Visible = True
                    txtaddress2.Visible = True
                    txtaddress3.Visible = True
                Else
                    lbldetails.visible = False
                    txtaddress1.Visible = False
                    txtaddress2.Visible = False
                    txtaddress3.Visible = False
                End If

                'If Session("ChallanRCnt") > 0 Then
                '    btnSave.Visible = True
                'Else
                '    btnSave.Visible = False
                'End If
                'Else
                '    'Redirect to No access page.
                '    Session("emp_group_id") = ""
                '    Session("emp_id") = ""
                '    Response.Redirect("login.aspx")
                'End If
            End If

        Catch ex As Exception
            lblMessage.Text = Utils.ShowError(ex)
        End Try
    End Sub

    Protected Property IsInEditMode() As Boolean
        Get
            Return Me.isEditMode
        End Get

        Set(ByVal value As Boolean)
            Me.isEditMode = value
        End Set
    End Property

    Private Sub BindInvoiceSplit(ByVal invoice_no_mstr As String)
        Try
            Dim dt As New System.Data.DataTable
            dt = UserManager.GetInvoiceSplit(invoice_no_mstr)
            grdInvoiceSplitList.DataSource = dt
            grdInvoiceSplitList.DataBind()
            grdInvoiceSplitList.Visible = True
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetChMemoAddr(ByVal invoice_no_mstr As String)
        Try
            Dim dt As New System.Data.DataTable
            dt = UserManager.GetChMemoAddr(invoice_no_mstr)
            If dt.Rows.Count = 0 Then
                Session("CashMemoAddr") = "N"
            Else
                txtaddress1.Text = dt.Rows(0).Item(0)
                txtaddress2.Text = dt.Rows(0).Item(1)
                txtaddress3.Text = dt.Rows(0).Item(2)
                Session("CashMemoAddr") = "Y"
            End If
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Response.Redirect("~/InvoiceList.aspx")
        Catch ex As Exception
            Response.Write(Utils.ShowError(ex))
        End Try
    End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer

            Dim total As Double
            Dim vat As Double
            Dim tempfinaltotal As Double
            Dim finaltotal As Double
            Dim discount As Double

            Dim row As GridViewRow
            lblMessage.Text = " "
            Dim inv_date As String
            Dim invoice_date As String

            For i = 0 To (grdInvoiceSplitList.Rows.Count - 1)
                grdInvoiceSplitList.SelectedIndex = i
                row = grdInvoiceSplitList.SelectedRow

                discount = Round(CDec(TryCast(row.FindControl("lblinv_subtotal_w_dis"), Label).Text) * ((CDec(TryCast(row.FindControl("txtinv_discount"), TextBox).Text) / 100)))
                total = Round((CDec(TryCast(row.FindControl("lblinv_subtotal_w_dis"), Label).Text) + CDec(TryCast(row.FindControl("lblinv_subtotal_wo_dis"), Label).Text) - discount))
                vat = Round(total * (CDec(TryCast(row.FindControl("lblinv_vat_per"), Label).Text) / 100))
                tempfinaltotal = Round(total + vat + CDec(TryCast(row.FindControl("txtinv_transport"), TextBox).Text))
                finaltotal = Round(tempfinaltotal)

                inv_date = TryCast(row.FindControl("txtinv_date"), TextBox).Text
                invoice_date = Day(inv_date) & "/" & Month(inv_date) & "/" & Year(inv_date)
                UserManager.Iinv_split("U", Request.QueryString("invoice_no_mstr"), "", "", "", vat, total, TryCast(row.FindControl("txtinvoice_no"), TextBox).Text, _
                                        TryCast(row.FindControl("txtinv_split_text"), TextBox).Text, TryCast(row.FindControl("txtinv_discount"), TextBox).Text, _
                                        TryCast(row.FindControl("txtinv_transport"), TextBox).Text, finaltotal, TryCast(row.FindControl("lblinv_split_rowid"), Label).Text, inv_date, "")
            Next

            UserManager.Ucust_finI("U", Request.QueryString("invoice_no_mstr"), "Y")

            UserManager.UCashMemoAddrI(Session("CashMemoAddr"), Request.QueryString("invoice_no_mstr"), txtaddress1.Text, txtaddress2.Text, txtaddress3.Text)

            BindInvoiceSplit(Request.QueryString("invoice_no_mstr"))
            If Session("cust_rowid") = 229 Then
                GetChMemoAddr(Request.QueryString("invoice_no_mstr"))
                lbldetails.visible = True
                txtaddress1.Visible = True
                txtaddress2.Visible = True
                txtaddress3.Visible = True
            Else
                lbldetails.visible = False
                txtaddress1.Visible = False
                txtaddress2.Visible = False
                txtaddress3.Visible = False
            End If

            'Response.Redirect("~/PrintCH.aspx")
        Catch ex As Exception

        End Try


    End Sub


    Protected Sub grdInvoiceSplitList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdInvoiceSplitList.RowCommand
        Dim inv_split_rowid As String
        If e.CommandName = "Print" Then
            inv_split_rowid = e.CommandArgument
            btnPrint_Click(inv_split_rowid)
            'Response.Redirect("~/Invoice_Print.aspx?rowid=" & inv_split_rowid)
        End If
    End Sub

    Protected Sub btnPrint_Click(ByVal inv_split_rowid As String)
        Dim url As String

        If Session("cust_rowid") = 229 Then
            url = "CashMemo_Print.aspx?rowid=" & inv_split_rowid
            'Dim MyDA As New MySqlClient.MySqlDataAdapter()
            'Dim MyCommand As New MySqlClient.MySqlCommand()
            'Dim Myconstring As New MySqlClient.MySqlConnection()
            'Dim report As New CrystalReport3
            'MyDA = New MySqlClient.MySqlDataAdapter()

            'Dim rowid As String
            'rowid = Request.QueryString("rowid")

            'Dim ds As New System.Data.DataSet
            'Dim dt As New DataTable
            'Dim dt1 As New DataTable
            'Dim dcol As New DataColumn()
            'Dim drowadd As DataRow

            'dt = UserManager.GetInvPrintM(rowid)
            'ds.Tables.Add(dt.Copy())
            'ds.Tables(0).TableName = "Cust_Data"
            'dt = UserManager.GetInvPrintD(rowid)
            'ds.Tables.Add(dt.Copy())
            'ds.Tables(1).TableName = "CashMemo"
            'dt = UserManager.GetChMemoAddr(Session("InvoiceNo"))
            'ds.Tables.Add(dt.Copy())
            'ds.Tables(2).TableName = "Cust_Details"


            'dt = UserManager.GetInvPrintC(rowid)
            'Dim i As Integer
            'Dim chno As String = ""

            'For i = 0 To dt.Rows.Count - 1
            '    chno = chno & " " & dt.Rows(i).Item(0)
            '    If i = dt.Rows.Count - 1 Then
            '    Else
            '        chno = chno & ","
            '    End If
            'Next

            'drowadd = dt1.NewRow()
            'dcol = New DataColumn("ChallanNo", GetType(System.String))
            'dt1.Columns.Add(dcol)
            'drowadd("ChallanNo") = chno
            'dt1.Rows.Add(drowadd)
            'ds.Tables.Add(dt1.Copy())
            'ds.Tables(3).TableName = "ChallanNo"
            'report.SetDataSource(ds)
            'report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "E:\BAC_Soft\Pdf\cam.pdf")
            'Response.ContentType = "application/pdf"
            'Dim file__1 As New FileInfo("E:\BAC_Soft\Pdf\cam.pdf")
            'Dim len As Integer = CInt(file__1.Length), bytes As Integer
            'Response.AppendHeader("content-length", len.ToString())
            'Response.AddHeader("Content-Disposition", "attachment;filename=" + "E:\BAC_Soft\Pdf\cam.pdf")
            'Dim buffer As Byte() = New Byte(1023) {}
            'Dim outStream As Stream = Response.OutputStream
            'Using stream As Stream = File.OpenRead("E:\BAC_Soft\Pdf\cam.pdf")
            '    While len > 0 AndAlso (InlineAssignHelper(bytes, stream.Read(buffer, 0, buffer.Length))) > 0
            '        outStream.Write(buffer, 0, bytes)
            '        len -= bytes
            '    End While
            'End Using
        Else
            url = "Invoice_Print.aspx?rowid=" & inv_split_rowid
        End If
        Dim fullURL As String = "window.open('" & url & "', '_blank', 'height=1000,width=1000,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,titlebar=no' );"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)

    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Dim url As String = "~/InvoiceList.aspx?cust_rowid=" & Session("cust_rowid")
        Response.Redirect(url)
    End Sub
    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
        target = value
        Return value
    End Function

End Class
