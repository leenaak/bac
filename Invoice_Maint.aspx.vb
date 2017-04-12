Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Math


Partial Class InvoiceMaint
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
                BindChallan()
                'If Session("ChallanRCnt") > 0 Then
                '    btnSave.Visible = True
                'Else
                '    btnSave.Visible = False
                'End If
                '            pnlResult.Visible = False
                '            pnlreviewed.Visible = False
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

    Private Sub BindChallan()
        Try
            Dim dt As New System.Data.DataTable
            Dim m_challan_no As String = ""
            Dim i As Integer = 0
            Dim challan_no(10) As Integer

            For i = 0 To Session("ChallantoInvC") - 1
                If i = 0 Then
                    m_challan_no = m_challan_no & "'" & Session("ChallantoInv")(i) & "'"
                Else
                    m_challan_no = m_challan_no & ",'" & Session("ChallantoInv")(i) & "'"
                End If


            Next
            '1','2'


            dt = UserManager.GetChallanItemsM(m_challan_no)
            grdInvoiceSplitList.DataSource = dt
            grdInvoiceSplitList.DataBind()
            pnlChallanList.Visible = True
            Session("Challan") = dt
            Session("ChallanRCnt") = dt.Rows.Count
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
            Dim i, j As Integer

            Dim invsplit As Integer
            Dim invsplitf As String
            Dim vat As Decimal
            Dim discount As Decimal

            Dim vattotal As Decimal
            Dim itemtotal As Decimal
            Dim invsplittotalwithD As Decimal
            Dim invsplittotalwithoutD As Decimal

            Dim row As GridViewRow
            Dim invarrsplit(9, 5) As Decimal
            Dim arrcnt As Integer = 0
            lblMessage.Text = " "

            For i = 0 To (grdInvoiceSplitList.Rows.Count - 1)
                grdInvoiceSplitList.SelectedIndex = i
                row = grdInvoiceSplitList.SelectedRow
                invsplit = CInt(TryCast(row.FindControl("invoice_splitdddl"), DropDownList).SelectedValue)
                vat = CDec(TryCast(row.FindControl("lblvat"), Label).Text)
                itemtotal = CDec(TryCast(row.FindControl("lblsellpr"), Label).Text) * CInt(TryCast(row.FindControl("lblqty"), Label).Text)
                vattotal = Round((vat / 100) * itemtotal)

                If i = 0 Then
                    invarrsplit(arrcnt, 0) = invsplit
                    invarrsplit(arrcnt, 2) = itemtotal
                    invarrsplit(arrcnt, 3) = vattotal
                    invarrsplit(arrcnt, 5) = vat
'                    If TryCast(row.FindControl("discount_ddl"), DropDownList).SelectedValue = "Y" Then
                    If TryCast(row.FindControl("lbldiscount"), Label).Text = "Y" Then
                        invarrsplit(arrcnt, 1) = itemtotal
                    Else
                        invarrsplit(arrcnt, 4) = itemtotal
                    End If
                    arrcnt = arrcnt + 1
                Else
                    invsplitf = "N"
                    For j = 0 To arrcnt
                        If invsplit = invarrsplit(j, 0) Then
                            invsplitf = "Y"
                            '                            If vat = invarrsplit(j, 1) Then
                            invarrsplit(j, 2) = itemtotal + invarrsplit(j, 2)
                            invarrsplit(j, 3) = vattotal + invarrsplit(j, 3)
                            invarrsplit(j, 5) = vat
                            '                    If TryCast(row.FindControl("discount_ddl"), DropDownList).SelectedValue = "Y" Then
                            If TryCast(row.FindControl("lbldiscount"), Label).Text = "Y" Then
                                invarrsplit(j, 1) = itemtotal + invarrsplit(j, 1)
                            Else
                                invarrsplit(j, 4) = itemtotal + invarrsplit(j, 4)
                            End If
                            Exit For
                            'Else
                            '    lblMessage.Text = "Vat not same for Invoice Split "
                            '    Exit Sub
                            'End If
                        End If
                    Next
                    If invsplitf = "N" Then
                        invarrsplit(arrcnt, 0) = invsplit
                        invarrsplit(arrcnt, 2) = itemtotal
                        invarrsplit(arrcnt, 3) = vattotal
                        invarrsplit(arrcnt, 5) = vat
                        '                    If TryCast(row.FindControl("discount_ddl"), DropDownList).SelectedValue = "Y" Then
                        If TryCast(row.FindControl("lbldiscount"), Label).Text = "Y" Then
                            invarrsplit(arrcnt, 1) = itemtotal
                        Else
                            invarrsplit(arrcnt, 4) = itemtotal
                        End If

                        arrcnt = arrcnt + 1
                    End If
                End If
            Next

            For i = 0 To (grdInvoiceSplitList.Rows.Count - 1)
                grdInvoiceSplitList.SelectedIndex = i
                row = grdInvoiceSplitList.SelectedRow
                invsplit = CInt(TryCast(row.FindControl("invoice_splitdddl"), DropDownList).SelectedValue)
                UserManager.Iinv_split_det("I", Session("ChallantoInv")(0), invsplit, TryCast(row.FindControl("lblitemtranrowid"), Label).Text, _
                                            TryCast(row.FindControl("lbldiscount"), Label).Text)
            Next

            For i = 0 To arrcnt - 1
                UserManager.Iinv_split("I", Session("ChallantoInv")(0), invarrsplit(i, 0), invarrsplit(i, 4), invarrsplit(i, 1), invarrsplit(i, 3), invarrsplit(i, 2), _
                                       "", "", "", "", "", "", CDate("2011/7/2"), invarrsplit(i, 5))
            Next

            For i = 0 To Session("ChallantoInvC") - 1
                UserManager.Ucust_fin("U", Session("ChallantoInv")(0), "P", Session("ChallantoInv")(i))
            Next

            Response.Redirect("~/Invoice_Split_DetN.aspx?invoice_no_mstr=" & Session("ChallantoInv")(0))
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Dim url As String = "~/InvoiceList.aspx?cust_rowid=" & Session("cust_rowid")
        Response.Redirect(url)
    End Sub

    Protected Sub btnDisSplit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim i, j As Integer

            Dim invsplit As Integer
            Dim invsplitf As String
            Dim vat As Decimal
            Dim discount As Decimal

            Dim vattotal As Decimal
            Dim itemtotal As Decimal
            Dim invsplittotalwithD As Decimal
            Dim invsplittotalwithoutD As Decimal

            Dim row As GridViewRow
            Dim invarrsplit(9, 5) As Decimal
            Dim arrcnt As Integer = 0
            lblMessage.Text = " "

            For i = 0 To (grdInvoiceSplitList.Rows.Count - 1)
                grdInvoiceSplitList.SelectedIndex = i
                row = grdInvoiceSplitList.SelectedRow
                invsplit = CInt(TryCast(row.FindControl("invoice_splitdddl"), DropDownList).SelectedValue)
                vat = CDec(TryCast(row.FindControl("lblvat"), Label).Text)
                itemtotal = CDec(TryCast(row.FindControl("lblsellpr"), Label).Text) * CInt(TryCast(row.FindControl("lblqty"), Label).Text)
                vattotal = Round((vat / 100) * itemtotal)

                If i = 0 Then
                    invarrsplit(arrcnt, 0) = invsplit
                    invarrsplit(arrcnt, 2) = itemtotal
                    invarrsplit(arrcnt, 3) = vattotal
                    invarrsplit(arrcnt, 5) = vat
                    '                    If TryCast(row.FindControl("discount_ddl"), DropDownList).SelectedValue = "Y" Then
                    If TryCast(row.FindControl("lbldiscount"), Label).Text = "Y" Then
                        invarrsplit(arrcnt, 1) = itemtotal
                    Else
                        invarrsplit(arrcnt, 4) = itemtotal
                    End If
                    arrcnt = arrcnt + 1
                Else
                    invsplitf = "N"
                    For j = 0 To arrcnt
                        If invsplit = invarrsplit(j, 0) Then
                            invsplitf = "Y"
                            '                            If vat = invarrsplit(j, 1) Then
                            invarrsplit(j, 2) = itemtotal + invarrsplit(j, 2)
                            invarrsplit(j, 3) = vattotal + invarrsplit(j, 3)
                            invarrsplit(j, 5) = vat
                            '                    If TryCast(row.FindControl("discount_ddl"), DropDownList).SelectedValue = "Y" Then
                            If TryCast(row.FindControl("lbldiscount"), Label).Text = "Y" Then
                                invarrsplit(j, 1) = itemtotal + invarrsplit(j, 1)
                            Else
                                invarrsplit(j, 4) = itemtotal + invarrsplit(j, 4)
                            End If

                            Exit For
                            '                        Else
                            '                            lblMessage.Text = "Vat not same for Invoice Split "
                            '                            Exit Sub
                            '                       End If
                        End If
                    Next
                    If invsplitf = "N" Then
                        invarrsplit(arrcnt, 0) = invsplit
                        invarrsplit(arrcnt, 2) = itemtotal
                        invarrsplit(arrcnt, 3) = vattotal
                        invarrsplit(arrcnt, 5) = vat
                        '                    If TryCast(row.FindControl("discount_ddl"), DropDownList).SelectedValue = "Y" Then
                        If TryCast(row.FindControl("lbldiscount"), Label).Text = "Y" Then
                            invarrsplit(arrcnt, 1) = itemtotal
                        Else
                            invarrsplit(arrcnt, 4) = itemtotal
                        End If

                        arrcnt = arrcnt + 1
                    End If
                End If
            Next

            Dim dt As New System.Data.DataTable()
            Dim dcol As New DataColumn()
            Dim drowadd As DataRow

            drowadd = dt.NewRow()
            dcol = New DataColumn("inv_split_no", GetType(System.String))
            dt.Columns.Add(dcol)
            dcol = New DataColumn("inv_subtotal_wo_dis", GetType(System.String))
            dt.Columns.Add(dcol)
            dcol = New DataColumn("inv_subtotal_w_dis", GetType(System.String))
            dt.Columns.Add(dcol)
            dcol = New DataColumn("inv_vat_per", GetType(System.String))
            dt.Columns.Add(dcol)
            dcol = New DataColumn("inv_vat_amt", GetType(System.String))
            dt.Columns.Add(dcol)
            dcol = New DataColumn("inv_total", GetType(System.String))
            dt.Columns.Add(dcol)

            For i = 0 To arrcnt - 1
                'UserManager.Iinv_split("I", Request.QueryString("challan_no"), invarrsplit(i, 0), invarrsplit(i, 1), invarrsplit(i, 4), invarrsplit(i, 3), invarrsplit(i, 2), _
                '                       "", "", "", "", "", "", CDate("2011/7/2"), invarrsplit(i, 5))

                If i = 0 Then
                Else
                    drowadd = dt.NewRow()
                End If
                drowadd("inv_split_no") = invarrsplit(i, 0)
                drowadd("inv_subtotal_wo_dis") = invarrsplit(i, 1)
                drowadd("inv_subtotal_w_dis") = invarrsplit(i, 4)
                drowadd("inv_vat_per") = invarrsplit(i, 5)
                drowadd("inv_vat_amt") = invarrsplit(i, 3)
                drowadd("inv_total") = invarrsplit(i, 2)
                dt.Rows.Add(drowadd)

            Next

            grdSplitDet.DataSource = dt
            grdSplitDet.DataBind()
            pnlSplitList.Visible = True
            'UserManager.Ucust_fin("U", Request.QueryString("challan_no"), "P")

            'Response.Redirect("~/Invoice_Split_DetN.aspx?invoice_no_mstr=" & Request.QueryString("challan_no"))
        Catch ex As Exception

        End Try

    End Sub

End Class
