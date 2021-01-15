Imports System
Imports System.Collections.Generic

Partial Public Class PARTICIPANT
    Public Property ID_PARTICIPANT As Long
    Public Property NOM As String
    Public Property PRENOM As String
    Public Property FONCTION As String
    Public Property EMAIL As String
    Public Property TELEPHONE As String

    'Public Overridable Property CONVOCATION As ICollection(Of CONVOCATION) = New HashSet(Of CONVOCATION)
    Public Overridable Property RESERVATION As ICollection(Of RESERVATION) = New HashSet(Of RESERVATION)
End Class
