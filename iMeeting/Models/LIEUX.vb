Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations.Schema

Partial Public Class LIEUX
    Public Property ID_LIEU As Long
    Public Property ID_BAT As Long
    Public Property LIBELLE As String
    Public Property EMPLACEMENT As String
    Public Property CAPACITE As Long
    Public Property TYPE_LIEU As Integer
    Public Property DETAILS As String
    Public Property DATE_CREATION As Date
    Public Property Etat As Integer = 0

    <ForeignKey("ID_BAT")>
    Public Overridable Property BATIMENT As BATIMENT
    Public Overridable Property RESERVATION As ICollection(Of RESERVATION) = New HashSet(Of RESERVATION)

End Class
