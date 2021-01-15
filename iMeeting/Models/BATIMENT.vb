Imports System
Imports System.Collections.Generic

Partial Public Class BATIMENT
    Public Property ID_BAT As Long
    Public Property LIBELLE As String
    Public Property POSITION As String
    Public Property NBRE_PIECE As Integer?
    Public Property DETAILS As String
    Public Property DATE_CREATION As Date

    Public Overridable Property PT_AFFICHAGE As ICollection(Of PT_AFFICHAGE) = New HashSet(Of PT_AFFICHAGE)
    Public Overridable Property SERVICE As ICollection(Of SERVICE) = New HashSet(Of SERVICE)
    Public Overridable Property LIEUX As ICollection(Of LIEUX) = New HashSet(Of LIEUX)

End Class
