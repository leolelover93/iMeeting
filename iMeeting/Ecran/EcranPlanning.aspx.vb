Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Collections.Generic
Public Class EcranPlanning
    Inherits System.Web.UI.Page
    Public Enum jrdelasem
        Lundi
        Mardi
        Mercredi
        Jeudi
        Vendredi
    End Enum
    'Public Function PeriodeWeek()
    '    Dim dt = New DateTime()
    '    dt = DateTime.Now
    '    Dim jours As String = dt.DayOfWeek.ToString()

    '    Dim listDays = New List(Of jrdelasem)
    '    Dim posDay As Integer = -listDays.IndexOf(jours)
    '    Dim leLundi As Integer = dt.DayOfYear + posDay
    '    '//DateTime lundi = DateTime.Now ;
    '    dt.AddDays(-dt.DayOfYear + 1 + leLundi)

    '    Dim interval As String = "Semaine du  " + dt.AddDays(-dt.DayOfYear + leLundi).ToLongDateString() + " au " + dt.AddDays(-dt.DayOfYear + leLundi + 4).ToLongDateString()
    '    Dim dtf As DateTime = dt.AddDays(-dt.DayOfYear + leLundi)
    '    'MessageBox.Show(interval);
    '    'MessageBox.Show(dtf.ToShortDateString());
    '    Return (interval)

    'End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'PeriodeWeek()
    End Sub

    Protected Sub Unnamed1_Click(sender As Object, e As EventArgs)
        'PeriodeWeek()
    End Sub
End Class