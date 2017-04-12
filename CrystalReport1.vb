Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine


Public Class CrystalReport1
    Inherits ReportClass

    Public Sub New()
    End Sub

    Public Overrides Property ResourceName() As String
        Get
            Return "DelChallan.rpt"
        End Get
        ' Do nothing
        Set(ByVal value As String)
        End Set
    End Property

    Public Overrides Property NewGenerator() As Boolean
        Get
            Return True
        End Get
        ' Do nothing
        Set(ByVal value As Boolean)
        End Set
    End Property

    Public Overrides Property FullResourceName() As String
        Get
            Return "DelChallan.rpt"
        End Get
        ' Do nothing
        Set(ByVal value As String)
        End Set
    End Property

End Class
